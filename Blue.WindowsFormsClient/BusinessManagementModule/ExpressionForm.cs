using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.WinFormsLibrary.Utility;
using Blue.CustomLibrary;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class ExpressionForm : Form
    {
        #region 契约属性

        /// <summary>
        /// 数据字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            set;
            get;
        }

        #endregion

        #region 赋值属性

        /// <summary>
        /// 
        /// </summary>
        public string Caption
        {
            get
            {
                return gcRight.Text;
            }
            set
            {
                gcRight.Text = value;
            }
        }

        /// <summary>
        /// 数据表编号
        /// </summary>
        public decimal TableId
        {
            get;
            set;
        }

        /// <summary>
        /// 表达式文本框内容
        /// </summary>
        public string ExpressionText
        {
            set
            {
                mtxtExpression.Text = value;
            }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public DataFieldFilter DataFieldFilter
        {
            get;
            set;
        }

        #endregion

        #region 委托属性

        /// <summary>
        /// 获得表达式字段
        /// </summary>
        public GetExpressionDataFieldDelegate GetExpressionDataFields
        {
            set;
            get;
        }

        /// <summary>
        /// 验证表达式字段
        /// </summary>
        public ValidateExpressionDataFieldDelegate ValidateExpressionDataFields
        {
            set;
            get;
        }

        #endregion

        #region 构造函数

        public ExpressionForm()
        {
            InitializeComponent();
        }
        
        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExpressionForm_Load(object sender, EventArgs e)
        {

        }
               

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string warning = string.Empty;
            bool result = ValidateExpression(ref warning);
            if (!result)
            {
                MessageBox.Show(warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                IList<CommonNode> commonNodes = new List<CommonNode>();
                foreach (object obj in lstDataFields.Items)
                {
                    CommonNode commonNode = obj as CommonNode;
                    int pos = commonNode.NodeName.IndexOf('}');
                    if (pos > 0)
                    {
                        commonNode.NodeName = commonNode.NodeName.Substring(pos + 1);
                    }
                    commonNodes.Add(commonNode);
                }
                if (GetExpressionDataFields != null)
                {
                    GetExpressionDataFields(commonNodes, mtxtExpression.Text.Trim());
                }
                this.Close();
            }            
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCheck_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string warning = string.Empty;
            bool result = ValidateExpression(ref warning);
            if (!result)
            {
                MessageBox.Show(warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show("验证成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("确定清除选择的字段和文本框中表达式的内容？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                lstDataFields.Items.Clear();
                mtxtExpression.Text = string.Empty;
            }
        }

        /// <summary>
        /// 增加字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            IList<CommonNode> commonNodes = CustomDataFieldContract.GetCommonNodes(TableId, DataFieldFilter);

            IList<CommonNode> nodes = new List<CommonNode>();
            foreach (CommonNode commonNode in commonNodes)
            {
                bool add = true;
                foreach (object obj in lstDataFields.Items)
                {
                    CommonNode node = obj as CommonNode;
                    if (node.NodeId == commonNode.NodeId && node.NodeType == commonNode.NodeType)
                    {
                        add = false;
                        break;
                    }
                }
                if (add)
                {
                    nodes.Add(commonNode);
                }
            }
            CheckedSelectedItemsForm frmCheckedSelectedItems = new CheckedSelectedItemsForm();
            frmCheckedSelectedItems.MultiNodeSelected = (selectedNodes) =>
            {
                for (int i = 0; i < selectedNodes.Count; i++)
                {
                    selectedNodes[i].NodeName = string.Format("{{{0}}}{1}", i + lstDataFields.Items.Count, selectedNodes[i].NodeName);
                }
                lstDataFields.Items.AddRange(selectedNodes.ToArray());
                lblTip.Text = string.Format(lblTip.Tag.ToString(), lstDataFields.Items.Count);
            };
            frmCheckedSelectedItems.LoadAndSetCommonNodes(nodes);
            frmCheckedSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 移除字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lstDataFields.SelectedIndex >= 0)
            {
                lstDataFields.Items.RemoveAt(lstDataFields.SelectedIndex);
                if (lstDataFields.SelectedIndex > 0)
                {
                    lstDataFields.SelectedIndex -= 1;
                }
                ListItemHandler.RefreshItemText(lstDataFields);                
                lblTip.Text = string.Format(lblTip.Tag.ToString(), lstDataFields.Items.Count);
            }
        }

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTop_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToTop(lstDataFields);
            ListItemHandler.RefreshItemText(lstDataFields);
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToPrevious(lstDataFields);
            ListItemHandler.RefreshItemText(lstDataFields);
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToNext(lstDataFields);
            ListItemHandler.RefreshItemText(lstDataFields);
        }

        /// <summary>
        /// 置底
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBottom_Click(object sender, EventArgs e)
        {
            ListItemHandler.MoveToBottom(lstDataFields);
            ListItemHandler.RefreshItemText(lstDataFields);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 验证操作
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private bool ValidateExpression(ref string warning)
        {
            /* 表达式文本合法性检查 */
            string expression = mtxtExpression.Text.Trim();
            if (string.IsNullOrWhiteSpace(expression))
            {
                mtxtExpression.Focus();
                warning = "表达式不能为空。";
                return false;
            }

            /* 检查表达式中的参数个数与选择字段的对应关系以及个数是否匹配 */
            StringBuilder sbExpression = new StringBuilder(expression);
            StringBuilder sbReplace = new StringBuilder();
            int index = 0;
            foreach (object obj in lstDataFields.Items)
            {
                CommonNode commonNode = obj as CommonNode;
                sbReplace.Append("{");
                sbReplace.Append(index);
                sbReplace.Append("}");
                sbExpression.Replace(sbReplace.ToString(), string.Empty);
                sbReplace.Clear();
                index++;
            }

            string newExpression = sbExpression.ToString();
            int start = 0;
            int end = 0;
            start = newExpression.IndexOf('{', 0);
            if (start >= 0)
            {
                end = newExpression.IndexOf('}', start + 1);
                if (end > (start + 1))
                {

                    string digtal = newExpression.Substring(start + 1, end - start - 1);
                    if (Regex.IsMatch(digtal, @"^[0-9]*$"))
                    {
                        int data = DataConvertionHelper.GetConvertedInt(digtal);
                        mtxtExpression.Focus();
                        warning = string.Format("表达式中的{{{0}}}字段并不存在", data);
                        return false;
                    }
                }
            }

            /* 验证表达式是否合法 */
            IList<CommonNode> commonNodes = new List<CommonNode>();
            foreach (object obj in lstDataFields.Items)
            {
                CommonNode commonNode = obj as CommonNode;
                commonNodes.Add(commonNode);
            }
            if (ValidateExpressionDataFields != null)
            {
                if (!ValidateExpressionDataFields(commonNodes, mtxtExpression.Text.Trim(), out warning))
                {                    
                    return false;
                }
            }

            return true;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 加载并设置字段
        /// </summary>
        /// <param name="commonNodes"></param>
        public void LoadAndSetDataFields(IList<CommonNode> commonNodes)
        {            
            if (commonNodes != null && commonNodes.Count > 0)
            {
                for (int i = 0; i < commonNodes.Count; i++)
                {
                    int pos = commonNodes[i].NodeName.LastIndexOf('}');
                    if (pos > 0)
                    {
                        commonNodes[i].NodeName = string.Format("{{{0}}}{1}", i, commonNodes[i].NodeName.Substring(pos + 1));
                    }
                    else
                    {
                        commonNodes[i].NodeName = string.Format("{{{0}}}{1}", i, commonNodes[i].NodeName);
                    }
                }
                lstDataFields.Items.AddRange(commonNodes.ToArray());
            }
            lblTip.Text = string.Format(lblTip.Tag.ToString(), lstDataFields.Items.Count);
        }

        #endregion
    }
}
