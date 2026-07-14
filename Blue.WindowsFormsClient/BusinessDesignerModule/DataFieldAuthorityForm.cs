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
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;
using AppFramework.WinFormsLibrary;

namespace Blue.WindowsFormsClient.SystemDesignerModule
{
    public partial class DataFieldAuthorityForm : Form
    {

        #region  私有变量

        private Dictionary<decimal, CustomFormAndDataFieldInfo> dicCustomFormAndDataFieldInfo;

        #endregion

        #region 契约接口

        private readonly ICustomFormContract customFormContract;

        #endregion

        #region 属性

        /// <summary>
        /// 表格编号
        /// </summary>
        public decimal FormId
        {
            get;
            set;
        }

        /// <summary>
        /// 表格权限
        /// </summary>
        public Int64 TableAuthority
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataFieldAuthorityForm()
        {
            InitializeComponent();
            customFormContract = BusinessChannelFactory.CreateCustomFormContract();
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbTableAuthority, typeof(TableAuthority));
            UserControlHelper.InitImageComboBoxEdit(icmbBatchSetting, typeof(DataFieldAuthority));
            UserControlHelper.InitRepositoryItemImageComboBox(rcmbDataFieldAuthority, typeof(DataFieldAuthority));            
            dicCustomFormAndDataFieldInfo = new Dictionary<decimal, CustomFormAndDataFieldInfo>();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFieldAuthorityForm_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbTableAuthority, TableAuthority);
                DataSet ds = customFormContract.GetDataFiledAuthority(FormId);
                gcDataFields.DataSource = ds.Tables[0];
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 字段权限批量设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbBatchSetting_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < gvDataFields.DataRowCount; i++)
            {
                decimal dataFieldId = DataConvertionHelper.GetDecimal(gvDataFields.GetDataRow(i)["DataFieldId"]);
                byte dataFieldAuthority = DataConvertionHelper.GetConvertedByte(icmbBatchSetting.EditValue);
                gvDataFields.GetDataRow(i)["DataFieldAuthority"] = dataFieldAuthority;
                GridViewCellValueChanged(dataFieldId, dataFieldAuthority);
            }
        }

        /// <summary>
        /// 单元格的值发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDataFields_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            decimal dataFieldId = DataConvertionHelper.GetDecimal(gvDataFields.GetDataRow(e.RowHandle)["DataFieldId"]);
            GridViewCellValueChanged(dataFieldId, DataConvertionHelper.GetByte(e.Value));
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IList<CustomFormAndDataFieldInfo> customFormAndDataFieldInfos = new List<CustomFormAndDataFieldInfo>();
                foreach (KeyValuePair<decimal, CustomFormAndDataFieldInfo> keyValue in dicCustomFormAndDataFieldInfo)
                {
                    customFormAndDataFieldInfos.Add(keyValue.Value);
                }
                TableAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbTableAuthority);
                customFormContract.Update(FormId, TableAuthority, customFormAndDataFieldInfos);
                Cursor = Cursors.Default;
                MessageBox.Show("设置成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }            
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApply_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 字段权限单元格的值发生变化
        /// </summary>
        /// <param name="dataAuthority"></param>
        /// <param name="dataFieldId"></param>
        private void GridViewCellValueChanged(decimal dataFieldId, byte dataFieldAuthority)
        {
            CustomFormAndDataFieldInfo customFormAndDataFieldInfo = null;
            if (dicCustomFormAndDataFieldInfo.ContainsKey(dataFieldId))
            {
                customFormAndDataFieldInfo = dicCustomFormAndDataFieldInfo[dataFieldId];
            }
            if (customFormAndDataFieldInfo == null)
            {
                customFormAndDataFieldInfo = new CustomFormAndDataFieldInfo(FormId, dataFieldId, dataFieldAuthority);
                dicCustomFormAndDataFieldInfo.Add(dataFieldId, customFormAndDataFieldInfo);
            }
            else
            {
                customFormAndDataFieldInfo.DataFieldAuthority = dataFieldAuthority;
            }
        }

        #endregion
    }
}
