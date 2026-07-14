using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class CscadeEnumForm : Form
    {
        #region 私有常量

        private const int MAX_LEVLE = 4;

        private const int MIN_LEVLE = 2;

        #endregion

        #region 成员变量

        private int _level = MIN_LEVLE;

        #endregion

        #region 属性

        public ICustomEnumContract CustomEnumContract
        {
            get;
            set;
        }

        public decimal EnumId
        {
            get;
            set;
        }

        /// <summary>
        /// 枚举层级
        /// </summary>
        public int Level
        {
            get
            {
                return _level;
            }
            set
            {
                if (_level <= MAX_LEVLE && _level >= MIN_LEVLE)
                {
                    _level = value;

                    switch (_level)
                    {
                        case MIN_LEVLE:
                            cmbFirst.Width = 270;
                            cmbSecond.Width = 270;
                            cmbSecond.Location = new Point(287, 44);
                            cmbThird.Visible = false;
                            cmbFourth.Visible = false;
                            break;

                        case 3:
                            cmbFirst.Width = 180;
                            cmbSecond.Width = 180;
                            cmbThird.Width = 180;
                            cmbSecond.Location = new Point(190, 44);
                            cmbThird.Location = new Point(380, 44);
                            cmbThird.Visible = true;
                            cmbFourth.Visible = false;
                            break;

                        case MAX_LEVLE:
                            cmbThird.Visible = true;
                            cmbFourth.Visible = true;
                            break;
                    }
                }

            }
        }

        /// <summary>
        /// 选择的文本内容
        /// </summary>
        public string SelectedText
        {
            get
            {
                return meToolTip.Text;
            }
            set
            {
                meToolTip.Text = value;
            }
        }

        /// <summary>
        /// 级联节点选择
        /// </summary>
        public CscadeNodeSelectedDelegate CscadeNodeSelected
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CscadeEnumForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CscadeEnumForm_Load(object sender, EventArgs e)
        {
            LoadItems(cmbFirst, EnumId);
        }

        /// <summary>
        /// 移除操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            switch (_level)
            {
                case 3:
                    cmbThird.EditValue = null;
                    cmbThird.Properties.Items.Clear();
                    break;

                case MAX_LEVLE:
                    cmbFourth.EditValue = null;
                    cmbFourth.Properties.Items.Clear();
                    cmbThird.EditValue = null;
                    cmbThird.Properties.Items.Clear();
                    break;
            }
            cmbSecond.EditValue = null;
            cmbSecond.Properties.Items.Clear();
            cmbFirst.EditValue = null;
            meToolTip.Text = string.Empty;
            CscadeNodeSelected?.Invoke(null);
            this.Close();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            bool result = true;
            IList<CommonNode> commonNodes = null;
            switch (_level)
            {
                case MIN_LEVLE:
                    commonNodes = GetSecondSelectedText(ref result);                    
                    break;

                case 3:
                    commonNodes = GetThirdSelectedText(ref result);
                    break;

                case MAX_LEVLE:
                    commonNodes = GetFourthSelectedText(ref result);
                    break;
            }
            if (commonNodes == null)
            {
                MessageBox.Show("枚举子节点级别不一致性，请联系管理员检查。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (commonNodes.Count < _level && result)
            {
                MessageBox.Show(string.Format("当前共有{0}级选项，已选择{1}级选项，请检查。", _level, commonNodes.Count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CscadeNodeSelected?.Invoke(commonNodes);
            this.Close();
        }

        /// <summary>
        /// 文本内容变化时标签宽度同时改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void meToolTip_TextChanged(object sender, EventArgs e)
        {
            meToolTip.Width = (meToolTip.Text.Length + Level) * 15;
        }

        /// <summary>
        /// 第一个下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFirst_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmbFourth.EditValue = null;
            cmbThird.EditValue = null;
            cmbSecond.EditValue = null;
            cmbFourth.Properties.Items.Clear();
            cmbThird.Properties.Items.Clear();
            cmbSecond.Properties.Items.Clear();
            if (cmbFirst.EditValue != null)
            {
                CommonNode commonNode = cmbFirst.EditValue as CommonNode;
                if (commonNode != null)
                {
                    meToolTip.Text = commonNode.NodeName;
                    LoadItems(cmbSecond, commonNode.NodeId);
                }
            }            
        }

        /// <summary>
        /// 第二个下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbSecond_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool result = true;

            cmbFourth.EditValue = null;
            cmbThird.EditValue = null;
            cmbFourth.Properties.Items.Clear();
            cmbThird.Properties.Items.Clear();
            IList<CommonNode> commonNodes = GetSecondSelectedText(ref result);
            meToolTip.Text = UserControlHelper.GetCleanFormattedName(commonNodes);
        }

        /// <summary>
        /// 第三个下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbThird_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool result = true;
            IList<CommonNode> commonNodes = GetThirdSelectedText(ref result);
            meToolTip.Text = UserControlHelper.GetCleanFormattedName(commonNodes);
            cmbFourth.EditValue = null;
            cmbFourth.Properties.Items.Clear();
        }

        /// <summary>
        /// 第四个下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFourth_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool result = true;
            IList<CommonNode> commonNodes = GetFourthSelectedText(ref result);
            meToolTip.Text = UserControlHelper.GetCleanFormattedName(commonNodes);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 第二个下拉框选择的结果
        /// </summary>
        /// <returns></returns>
        private IList<CommonNode> GetSecondSelectedText(ref bool result)
        {
            result = true;
            IList<CommonNode> commonNodes = new List<CommonNode>();

            if (cmbFirst.EditValue != null)
            {
                CommonNode commonNode = cmbFirst.EditValue as CommonNode;
                if (commonNode != null && cmbSecond.EditValue != null)
                {
                    commonNodes.Add(commonNode);
                    CommonNode secondCommonNode = cmbSecond.EditValue as CommonNode;
                    if (secondCommonNode != null)
                    {
                        commonNodes.Add(secondCommonNode);
                        LoadItems(cmbThird, secondCommonNode.NodeId);
                    }
                    else
                    {
                        if(cmbSecond.Properties.Items.Count > 0)
                        {
                            result = false;
                        }
                    }               
                }
            }

            return commonNodes;
        }

        /// <summary>
        /// 第三个下拉框选择的结果
        /// </summary>
        /// <returns></returns>
        private IList<CommonNode> GetThirdSelectedText(ref bool result)
        {
            result = true;
            IList<CommonNode> commonNodes = new List<CommonNode>();

            if (cmbFirst.EditValue != null)
            {
                CommonNode commonNode = cmbFirst.EditValue as CommonNode;
                if (commonNode != null && cmbSecond.EditValue != null)
                {
                    commonNodes.Add(commonNode);
                    CommonNode secondCommonNode = cmbSecond.EditValue as CommonNode;
                    if (secondCommonNode != null && cmbThird.EditValue != null)
                    {
                        commonNodes.Add(secondCommonNode);
                        CommonNode thirdCommonNode = cmbThird.EditValue as CommonNode;
                        if (thirdCommonNode != null)
                        {
                            commonNodes.Add(thirdCommonNode);
                            LoadItems(cmbFourth, commonNode.NodeId);
                        }
                        else
                        {
                            if (cmbThird.Properties.Items.Count > 0)
                            {
                                result = false;
                            }
                        }
                    }
                    else
                    {
                        if (cmbSecond.Properties.Items.Count > 0)
                        {
                            result = false;
                        }
                    }
                }
            }

            return commonNodes;
        }

        /// <summary>
        /// 第四个下拉框选择的结果
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        private IList<CommonNode> GetFourthSelectedText(ref bool result)
        {
            result = true;
            IList<CommonNode> commonNodes = new List<CommonNode>();

            if (cmbFirst.EditValue != null)
            {
                CommonNode commonNode = cmbFirst.EditValue as CommonNode;
                if (commonNode != null && cmbSecond.EditValue != null)
                {
                    commonNodes.Add(commonNode);
                    CommonNode secondCommonNode = cmbSecond.EditValue as CommonNode;
                    if (secondCommonNode != null && cmbThird.EditValue != null)
                    {
                        commonNodes.Add(secondCommonNode);
                        CommonNode thirdCommonNode = cmbThird.EditValue as CommonNode;
                        if (thirdCommonNode != null && cmbFourth.EditValue != null)
                        {
                            commonNodes.Add(thirdCommonNode);
                            CommonNode fourthCommonNode = cmbFourth.EditValue as CommonNode;
                            if (fourthCommonNode != null)
                            {
                                commonNodes.Add(fourthCommonNode);
                            }
                            else
                            {
                                if (cmbFourth.Properties.Items.Count > 0)
                                {
                                    result = false;
                                }
                            }
                        }
                        else
                        {
                            if (cmbThird.Properties.Items.Count > 0)
                            {
                                result = false;
                            }
                        }
                    }
                    else
                    {
                        if (cmbSecond.Properties.Items.Count > 0)
                        {
                            result = false;
                        }
                    }
                }
            }

            return commonNodes;
        }

        /// <summary>
        /// 下拉框加载方法
        /// </summary>
        /// <param name="comboBoxEdit"></param>
        /// <param name="enumId"></param>
        private void LoadItems(DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit, decimal enumId)
        {
            IList<CommonNode> commonNodes = CustomEnumContract.GetChildNodes(enumId);
            if (commonNodes != null && commonNodes.Count > 0)
            {
                comboBoxEdit.Properties.Items.AddRange(commonNodes.ToArray());
            }
        }

        #endregion                
    }
}
