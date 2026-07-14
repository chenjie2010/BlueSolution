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
    public partial class InterfaceAndDataFieldForm : Form
    {

        #region  私有变量

        private Dictionary<decimal, WorkflowProcessAndDataFieldInfo> dicWorkflowProcessAndDataFieldInfo;

        private bool isLoading = false;

        #endregion

        #region 契约接口

        private readonly ICustomWorkflowProcessContract customWorkflowProcessContract;
        private readonly ICustomDataContract customDataContract;
        private readonly ICustomFormContract customFormContract;

        #endregion

        #region 属性

        /// <summary>
        /// 工作流程编号
        /// </summary>
        public decimal ProcessId
        {
            get;
            set;
        }


        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InterfaceAndDataFieldForm()
        {
            InitializeComponent();
            //customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
            //customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            //customFormContract = BusinessChannelFactory.CreateCustomFormContract();
            //UserControlHelper.InitCheckedComboBoxEditItems(ccmbTableAuthority, typeof(TableAuthority));
            //UserControlHelper.InitImageComboBoxEdit(icmbBatchSetting, typeof(DataFieldAuthority));
            //UserControlHelper.InitRepositoryItemImageComboBox(rcmbDataFieldAuthority, typeof(DataFieldAuthority));
            //UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemLogicalDataField));
            //dicWorkflowProcessAndDataFieldInfo = new Dictionary<decimal, WorkflowProcessAndDataFieldInfo>();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InterfaceAndDataFieldForm_Load(object sender, EventArgs e)
        {
            try
            {

                Cursor = Cursors.WaitCursor;
                decimal dataId = customWorkflowProcessContract.GetDatalId(ProcessId);
                IList<CommonNode> commonNodes = customFormContract.GetChildNodes(dataId);
                lstTable.Items.AddRange(commonNodes.ToArray());
                if (lstTable.Items.Count > 0)
                {
                    lstTable.SelectedIndex = 0;
                }
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
        /// 选项变化后初始化权限
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstTable.SelectedIndex >= 0)
            {
                CommonNode commonNode = lstTable.SelectedItem as CommonNode;
                CustomFormInfo customFormInfo = customFormContract.GetModelInfo(commonNode.NodeId);
                lstTable.Tag = customFormInfo;
                Int64 tableAuthority = 0;
                Int64 systemDataFieldAuthority = 0;
                DataSet ds = null;
                TableType tableTypeValue = (TableType)customFormInfo.TableType;
                isLoading = true;
                switch (tableTypeValue)
                {
                    case TableType.View:
                        tableAuthority = customWorkflowProcessContract.GetViewAuthority(ProcessId, customFormInfo.ViewId);
                        systemDataFieldAuthority = customWorkflowProcessContract.GetViewSystemDataFieldAuthority(ProcessId, customFormInfo.ViewId);
                        ds = customWorkflowProcessContract.GetDataFiledAuthorityByViewId(ProcessId, customFormInfo.ViewId);
                        break;

                    case TableType.Table:
                        tableAuthority = customWorkflowProcessContract.GetTableAuthority(ProcessId, customFormInfo.TableId);
                        systemDataFieldAuthority = customWorkflowProcessContract.GetTableSystemDataFieldAuthority(ProcessId, customFormInfo.TableId);
                        ds = customWorkflowProcessContract.GetDataFiledAuthorityByTableId(ProcessId, customFormInfo.TableId);
                        break;

                    default:
                        throw new ArgumentException("不支持该表类型");
                }                
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbTableAuthority, tableAuthority);
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataFieldSetting, systemDataFieldAuthority);
                isLoading = false;
                gcDataFields.DataSource = ds.Tables[0];
                dicWorkflowProcessAndDataFieldInfo.Clear();
                btnApply.Enabled = false;
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
                Sumbit();
                Cursor = Cursors.Default;                
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
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Sumbit();
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
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccmbTableAuthority_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                btnApply.Enabled = true;
            }
        }

        private void ccmbDataFieldSetting_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                btnApply.Enabled = true;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 字段权限单元格的值发生变化
        /// </summary>
        /// <param name="dataAuthority"></param>
        /// <param name="dataFieldId"></param>
        private void GridViewCellValueChanged(decimal dataFieldId, byte dataFieldAuthority)
        {
            WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo = null;
            if (dicWorkflowProcessAndDataFieldInfo.ContainsKey(dataFieldId))
            {
                workflowProcessAndDataFieldInfo = dicWorkflowProcessAndDataFieldInfo[dataFieldId];
            }
            if (workflowProcessAndDataFieldInfo == null)
            {
                workflowProcessAndDataFieldInfo = new WorkflowProcessAndDataFieldInfo(ProcessId, dataFieldId, dataFieldAuthority);
                dicWorkflowProcessAndDataFieldInfo.Add(dataFieldId, workflowProcessAndDataFieldInfo);
            }
            else
            {
                workflowProcessAndDataFieldInfo.DataFieldAuthority = dataFieldAuthority;
            }
            btnApply.Enabled = true;
        }

        /// <summary>
        /// 提交
        /// </summary>
        private void Sumbit()
        {
            IList<WorkflowProcessAndDataFieldInfo> workflowProcessAndDataFieldInfos = new List<WorkflowProcessAndDataFieldInfo>();
            foreach (KeyValuePair<decimal, WorkflowProcessAndDataFieldInfo> keyValue in dicWorkflowProcessAndDataFieldInfo)
            {
                workflowProcessAndDataFieldInfos.Add(keyValue.Value);
            }
            Int64 tableAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbTableAuthority);
            Int64 systemDataFieldAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataFieldSetting);
            CustomFormInfo customFormInfo = lstTable.Tag as CustomFormInfo;
            TableType tableTypeValue = (TableType)customFormInfo.TableType;
            switch (tableTypeValue)
            {
                case TableType.View:
                    customWorkflowProcessContract.UpdateViewAndDataFieldInfos(ProcessId, customFormInfo.ViewId, tableAuthority, systemDataFieldAuthority, workflowProcessAndDataFieldInfos);
                    break;

                case TableType.Table:
                    customWorkflowProcessContract.UpdateTableAndDataFieldInfos(ProcessId, customFormInfo.TableId, tableAuthority, systemDataFieldAuthority, workflowProcessAndDataFieldInfos);
                    break;

                default:
                    throw new ArgumentException("不支持该表类型");
            }
            btnApply.Enabled = false;
        }

        #endregion
        
    }
}
