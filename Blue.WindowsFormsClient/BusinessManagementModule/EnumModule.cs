using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class EnumModule : UserControl, ITreeNodeShow
    {
        #region 属性

        /// <summary>
        /// 枚举类型契约
        /// </summary>
        public ICustomEnumContract CustomEnumContract
        {
            set; get;
        }

        /// <summary>
        /// 字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            set; get;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public EnumModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            SetActiveStatesOfControls(true);
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnumModule_Load(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// 查看与枚举相关联的字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDataFieldList_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            if (TreeNodeId > 0)
            {
                DataSet ds = CustomDataFieldContract.GetDataFieldsByEnumId(TreeNodeId);
                DataTableModeForm frmDataTableMode = new DataTableModeForm();
                frmDataTableMode.DataSource = ds;
                frmDataTableMode.ShowDialog();
            }
        }

        /// <summary>
        /// 刷新排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshSorting_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CustomEnumContract.RefreshSorting();
                MessageBox.Show("排序刷新成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion

        #region 实现 ITreeNodeShow 接口

        /// <summary>
        /// 节点编号
        /// </summary>
        public decimal TreeNodeId
        {
            get;
            set;
        }

        /// <summary>
        /// 默认编码
        /// </summary>
        public string DefaultCode
        {
            set
            {
                txtCode.Text = value;
            }
            get
            {
                return txtCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtName.ReadOnly = readOnly;
            txtEnumValue.ReadOnly = readOnly;
            txtAddFstCode.ReadOnly = readOnly;
            txtAddScdCode.ReadOnly = readOnly;
            txtFstAdditionalString.ReadOnly = readOnly;
            txtScdAdditionalString.ReadOnly = readOnly;
            txtTrdAdditionalString.ReadOnly = readOnly;
            txtFourthAdditionalString.ReadOnly = readOnly;
            txtFifthAdditionalString.ReadOnly = readOnly;
            txtSixthAdditionalString.ReadOnly = readOnly;
            txtFstAdditionalInteger.ReadOnly = readOnly;
            txtScdAdditionalInteger.ReadOnly = readOnly;
            txtFstAdditionalDecimal.ReadOnly = readOnly;
            txtScdAdditionalDecimal.ReadOnly = readOnly;
            chkSuperEnumEnabled.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!readOnly)
            {
                txtName.Focus();
            }
            lnkDataFieldList.Enabled = readOnly;
        }

        /// <summary>
        /// 设置枚举信息
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            if (commonNode != null)
            {
                TreeNodeId = commonNode.NodeId;
                CustomEnumInfo customEnumInfo = CustomEnumContract.GetModelInfo(commonNode.NodeId);
                if (customEnumInfo != null)
                {
                    txtName.Text = customEnumInfo.EnumName;
                    txtCode.Text = customEnumInfo.EnumCode;
                    txtEnumValue.Text = customEnumInfo.EnumValue;
                    txtAddFstCode.Text = customEnumInfo.FirstCode;
                    txtAddScdCode.Text = customEnumInfo.SecondCode;
                    txtNotes.Text = customEnumInfo.Notes;
                    txtFstAdditionalString.Text = customEnumInfo.FstAdditionalString;
                    txtScdAdditionalString.Text = customEnumInfo.ScdAdditionalString;
                    txtTrdAdditionalString.Text = customEnumInfo.TrdAdditionalString;
                    txtFourthAdditionalString.Text = customEnumInfo.FourthAdditionalString;
                    txtFifthAdditionalString.Text = customEnumInfo.FifthAdditionalString;
                    txtSixthAdditionalString.Text = customEnumInfo.SixthAdditionalString;
                    txtFstAdditionalInteger.Text = DataConvertionHelper.EndowStringOfInt(customEnumInfo.FstAdditionalInteger);
                    txtScdAdditionalInteger.Text = DataConvertionHelper.EndowStringOfInt(customEnumInfo.ScdAdditionalInteger);                    
                    txtFstAdditionalDecimal.Text = DataConvertionHelper.EndowStringOfDecimal(customEnumInfo.FstAdditionalDecimal);                    
                    txtScdAdditionalDecimal.Text = DataConvertionHelper.EndowStringOfDecimal(customEnumInfo.ScdAdditionalDecimal);
                    chkSuperEnumEnabled.Checked = customEnumInfo.SuperEnumEnabled;
                }
                else
                {
                    txtName.Text = commonNode.NodeName;
                    txtCode.Text = commonNode.NodeCode;
                    txtNotes.Text = string.Empty;
                    txtFstAdditionalString.Text = string.Empty;
                    txtScdAdditionalString.Text = string.Empty;
                    txtTrdAdditionalString.Text = string.Empty;
                    txtFourthAdditionalString.Text = string.Empty;
                    txtFifthAdditionalString.Text = string.Empty;
                    txtSixthAdditionalString.Text = string.Empty;
                    txtFstAdditionalInteger.Text = string.Empty;
                    txtScdAdditionalInteger.Text = string.Empty;
                    txtFstAdditionalDecimal.Text = string.Empty;
                    txtScdAdditionalDecimal.Text = string.Empty;
                    chkSuperEnumEnabled.Checked = false;
                }
            }
            else
            {
                ClearModelInfo();
            }
        }

        /// <summary>
        /// 清除界面数据
        /// </summary>
        public void ClearModelInfo()
        {
            txtName.Text = string.Empty;
            txtCode.Text = string.Empty;
            txtEnumValue.Text = string.Empty;
            txtAddFstCode.Text = string.Empty;
            txtAddScdCode.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtFstAdditionalString.Text = string.Empty;
            txtScdAdditionalString.Text = string.Empty;
            txtTrdAdditionalString.Text = string.Empty;
            txtFourthAdditionalString.Text = string.Empty;
            txtFifthAdditionalString.Text = string.Empty;
            txtSixthAdditionalString.Text = string.Empty;
            txtFstAdditionalInteger.Text = string.Empty;
            txtScdAdditionalInteger.Text = string.Empty;
            txtFstAdditionalDecimal.Text = string.Empty;
            txtScdAdditionalDecimal.Text = string.Empty;
            ClearTextOfDataFieldList();
            txtName.Focus();
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 检查输入
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        public bool CheckInputs(out string warning)
        {
            warning = string.Empty;
            string fstInt = txtFstAdditionalInteger.Text.Trim();
            string scdInt = txtScdAdditionalInteger.Text.Trim();
            string fstDecimal = txtFstAdditionalDecimal.Text.Trim();
            string scdDecimal = txtScdAdditionalDecimal.Text.Trim();
            if (!string.IsNullOrWhiteSpace(fstInt) && !DataConvertionHelper.IsInt32(fstInt))
            {
                warning = "整形一的值必须为整数。";
                return false;
            }
            if (!string.IsNullOrWhiteSpace(scdInt) && !DataConvertionHelper.IsInt32(scdInt))
            {
                warning = "整形二的值必须为整数。";
                return false;
            }
            if (!string.IsNullOrWhiteSpace(fstDecimal) && !DataConvertionHelper.IsDecimal(fstDecimal))
            {
                warning = "实数一的值必须为整数。";
                return false;
            }
            if (!string.IsNullOrWhiteSpace(scdDecimal) && !DataConvertionHelper.IsDecimal(scdDecimal))
            {
                warning = "实数二的值必须为整数。";
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取枚举信息
        /// </summary>
        /// <returns></returns>
        public CustomEnumInfo GetModelInfo()
        {
            string enumName = txtName.Text.Trim();
            string enumCode = txtCode.Text.Trim();
            string enumValue  = txtEnumValue.Text.Trim();
            string addFstCode = txtAddFstCode.Text.Trim();
            string addScdCode = txtAddScdCode.Text.Trim();
            string notes = txtNotes.Text.Trim();
            string fstAdditionalString = txtFstAdditionalString.Text.Trim();
            string scdAdditionalString = txtScdAdditionalString.Text.Trim();
            string trdAdditionalString = txtTrdAdditionalString.Text.Trim();
            string fourthAdditionalString = txtFourthAdditionalString.Text.Trim();
            string fifthAdditionalString = txtFifthAdditionalString.Text.Trim();
            string sixthAdditionalString = txtSixthAdditionalString.Text.Trim();            

            int fstAdditionalInteger = DataConvertionHelper.GetConvertedInt(txtFstAdditionalInteger.Text.Trim(), Int32.MinValue);
            int scdAdditionalInteger = DataConvertionHelper.GetConvertedInt(txtScdAdditionalInteger.Text.Trim(), Int32.MinValue);
            decimal fstAdditionalDecimal = DataConvertionHelper.GetConvertedDecimal(txtFstAdditionalDecimal.Text.Trim(), decimal.MinValue);
            decimal scdAdditionalDecimal = DataConvertionHelper.GetConvertedDecimal(txtScdAdditionalDecimal.Text.Trim(), decimal.MinValue);

            CustomEnumInfo customEnumInfo = new CustomEnumInfo()
            {
                EnumId = TreeNodeId,
                EnumName = enumName,
                EnumCode = enumCode,
                EnumValue = enumValue,
                FirstCode = addFstCode,
                SecondCode = addScdCode,
                FstAdditionalString = fstAdditionalString,
                ScdAdditionalString = scdAdditionalString,
                TrdAdditionalString = trdAdditionalString,
                FourthAdditionalString = fourthAdditionalString,
                FifthAdditionalString = fifthAdditionalString,
                SixthAdditionalString = sixthAdditionalString,
                FstAdditionalInteger = fstAdditionalInteger,
                ScdAdditionalInteger = scdAdditionalInteger,
                FstAdditionalDecimal = fstAdditionalDecimal,
                ScdAdditionalDecimal = scdAdditionalDecimal,
                SuperEnumEnabled = chkSuperEnumEnabled.Checked,
                Notes = notes
            };

            return customEnumInfo;
        }

        /// <summary>
        /// 初始化显示关联字段
        /// </summary>
        public void ClearTextOfDataFieldList()
        {
            lnkDataFieldList.Text = "该枚举暂未与字段关联";
        }

        /// <summary>
        /// 初始化显示关联字段
        /// </summary>
        /// <param name="count"></param>
        public void SetTextOfDataFieldList(int count)
        {
            lnkDataFieldList.Enabled = count > 0 ? true : false; 
            lnkDataFieldList.Text = string.Format("该枚举有{0}个字段关联", count);
        }

        #endregion
       
    }
}
