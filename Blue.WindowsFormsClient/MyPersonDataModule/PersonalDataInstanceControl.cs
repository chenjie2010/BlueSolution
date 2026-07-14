using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using Blue.Model.DataFilledModule;
using Blue.WindowsFormsClient.Common;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WinBusinessLogic.UserReport;

namespace Blue.WindowsFormsClient.MyPersonDataModule
{
    public partial class PersonalDataInstanceControl : UserControl
    {
        #region 私有常量

        /* 第 0 列为多选列,第 1 列为审核状态列 */
        private const int AUDITING_GRID_VIEW_FIXED_COLUMN_COUNT = 2;

        #endregion

        #region 私有变量

        private DataAuditingInfo currentDataAuditingInfo;
        private ExtendedCustomBusinessInfo extendedCustomBusinessInfo = null;

        #endregion

        #region 契约接口

        private readonly IDataAuditingContract dataAuditingContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICombinedTableContract combinedTableContract;
        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly IDataAuditingStepContract dataAuditingStepContract;
        private readonly IDataAuditingLogContract dataAuditingLogContract;
        private ICustomReportContract customReportContract;
        private ICustomSheetContract customSheetContract;
        private ICustomCellContract customCellContract;
        private ISystemConfigContract systemConfigContract;

        #endregion

        #region 属性

        /// <summary>
        /// 标题
        /// </summary>
        public string Title
        {
            get
            {
                return gcTop.Text;
            }
            set
            {
                gcTop.Text = value;
            }
        }

        /// <summary>
        /// 返回主界面
        /// </summary>
        public GoBackDelegate GoBack
        {
            get;
            set;
        }

        /// <summary>
        /// 业务对象
        /// </summary>
        public ExtendedCustomBusinessInfo ExtendedCustomBusinessInfo
        {
            get
            {
                return extendedCustomBusinessInfo;
            }
            set
            {
                extendedCustomBusinessInfo = value;
                InitData();
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PersonalDataInstanceControl()
        {
            InitializeComponent();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            combinedTableContract = BusinessChannelFactory.CreateCombinedTableContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
            customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            dataAuditingStepContract = BusinessDesignerChannelFactory.CreateDataAuditingStepContract();
            dataAuditingLogContract = BusinessDesignerChannelFactory.CreateDataAuditingLogContract();
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonalDataInstanceControl_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 下载报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkReport_Click(object sender, EventArgs e)
        {
            if (customReportContract == null)
            {
                customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
                customSheetContract = BusinessDesignerChannelFactory.CreateCustomSheetContract();
                customCellContract = BusinessDesignerChannelFactory.CreateCustomCellContract();
                systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            }
            if (currentDataAuditingInfo.ReportId > 0)
            {
                CustomReport customReport = new CustomReport(customReportContract, customSheetContract, customCellContract,
                    customTableContract, customDataFieldContract, systemConfigContract);
                customReport.GenerateExcel(currentDataAuditingInfo.ReportId, CurrentUser.Instance.UserId);
            }
            else
            {
                MessageBox.Show("该业务不提供报表下载。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 提交申请
        /// </summary>
        /// <param name="auditingLogType"></param>
        /// <param name="dataAuditingId"></param>
        /// <param name="dataRow"></param>
        private void ShowPersonDataAddedForm(AuditingLogType auditingLogType, decimal dataAuditingId, DataRow dataRow)
        {
            DataAuditingInfo dataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingId);
            PersonDataForm frmPersonData = new PersonDataForm();
            frmPersonData.Text = dataAuditingInfo.DataAuditingName;
            IList<CommonNode> dataFields = dataAuditingContract.GetDataFields(dataAuditingInfo.DataAuditingId);
            IList<decimal> tableIds = new List<decimal>();
            FormType formType = (FormType)currentDataAuditingInfo.TableType;
            switch (formType)
            {
                case FormType.Table:
                    tableIds.Add(currentDataAuditingInfo.TableId);
                    break;

                case FormType.CombinedTable:
                    IList<CommonNode> commonNodeInfos = combinedTableContract.GetTables(currentDataAuditingInfo.CombinedTableId);
                    foreach (var commonNodeInfo in commonNodeInfos)
                    {                        
                        tableIds.Add(commonNodeInfo.NodeId);
                    }
                    break;
            }
            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = customRoleContract.GetAuthorizedExtendedCustomDataFieldInfos(CurrentUser.Instance.UserId, tableIds, DataAuthorityType.Business);
            DataTableBusiness business = new DataTableBusiness(decimal.MinValue, null, false, dataAuditingInfo.SystemDataFieldAuthority, false, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                    customEnumContract, customDepartmentContract, frmPersonData.Panel, frmPersonData.MemoEditToolTip);
            IList<ExtendedCustomDataFieldInfo> customDataFieldInfos = new List<ExtendedCustomDataFieldInfo>();
            //foreach (var extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
            //{
            //    if (dataFields.FindIndex(dataField => dataField.NodeId == extendedCustomDataFieldInfo.DataFieldId) < 0)
            //    {
            //        continue;
            //    }
            //    customDataFieldInfos.Add(extendedCustomDataFieldInfo);
            //}
            foreach (var dataField in dataFields)
            {
                int pos = extendedCustomDataFieldInfos.FindIndex(extendedCustomDataFieldInfo => dataField.NodeId == extendedCustomDataFieldInfo.DataFieldId);
                if (pos < 0)
                {
                    continue;
                }
                customDataFieldInfos.Add(extendedCustomDataFieldInfos[pos]);
            }
            int multiTextBoxCount = 0;
            business.CreateControls(customDataFieldInfos, business.GetFormShowStyleSettings(FormShowStyle.SingleColumnThreeRanksCompleted), ref multiTextBoxCount);
            Dictionary<decimal, decimal> tableIdAndRecordIds = new Dictionary<decimal, decimal>();
            if (dataRow != null)
            {
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                FormType currentFormType = (FormType)currentDataAuditingInfo.TableType;
                switch (currentFormType)
                {
                    case FormType.Table:
                        business.LoadDataOnTable(currentDataAuditingInfo.TableId, dataRow, true);
                        tableIdAndRecordIds.Add(currentDataAuditingInfo.TableId, DataConvertionHelper.GetDecimal(dataRow[recordIdName], 0));
                        break;

                    case FormType.CombinedTable:
                        Dictionary<decimal, DataTable> dataRowValues = GetConvertionRow(currentDataAuditingInfo.CombinedTableId, dataRow);
                        foreach (KeyValuePair<decimal, DataTable> keyValue in dataRowValues)
                        {
                            tableIdAndRecordIds.Add(keyValue.Key, DataConvertionHelper.GetDecimal(keyValue.Value.Rows[0][recordIdName], 0));
                        }
                        business.LoadDataOnCombinedTables(dataRowValues, true);
                        break;

                    default:
                        throw new ArgumentException("不支持该枚举类型。");
                }
            }
            frmPersonData.SumbittedHandler = (reviewerId, description) =>
            {                
                RecordSet recordSet = business.GetRecordSet();
                if (auditingLogType == AuditingLogType.Edit)
                {
                    foreach (var recordEntitiy in recordSet.RecordEntities)
                    {
                        recordEntitiy.BusinessEnabled = true;
                        if (tableIdAndRecordIds.ContainsKey(recordEntitiy.TableId))
                        {
                            recordEntitiy.BusinessAlternativeId = tableIdAndRecordIds[recordEntitiy.TableId];
                        }
                    }
                }
                else
                {
                    foreach (var recordEntitiy in recordSet.RecordEntities)
                    {
                        recordEntitiy.BusinessEnabled = true;
                    }
                }
                if (!recordSet.Success)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(recordSet.Warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                AuditingStatus auditingStatus = AuditingStatus.None;
                if (dataAuditingInfo.InitReviewStatus)
                {
                    auditingStatus = AuditingStatus.Auditing;
                }
                else if (dataAuditingInfo.AllocationStatus)
                {
                    auditingStatus = AuditingStatus.Allocating;
                }
                else if (dataAuditingInfo.FinalReviewStatus)
                {
                    auditingStatus = AuditingStatus.Audited;
                }
                else
                {
                    throw new ArgumentException("必须包含一个流程且至少包含终审（初审、分配或者终审）。");
                }
                DataAuditingLogInfo dataAuditingLogInfo = new DataAuditingLogInfo()
                {
                    DataAuditingId = dataAuditingId,
                    ParentUserId = CurrentUser.Instance.UserId,
                    UserId = CurrentUser.Instance.UserId,
                    AuditingLogName = string.Format("{0}_{1}_{2}_{3}", dataAuditingInfo.DataAuditingName, CurrentUser.Instance.UserName,
                    CurrentUser.Instance.UserActualName, DateTime.Now.ToString("G")),
                    AuditingLogType = (byte)auditingLogType,
                    AuditingStatus = (byte)auditingStatus,
                    AuditingLogTime = DateTime.Now,
                    LogDescription = description
                };
                DataAuditingStepInfo dataAuditingStepInfo = new DataAuditingStepInfo()
                {
                    ParentUserId = CurrentUser.Instance.UserId,
                    UserId = reviewerId,
                    AuditingAction = (byte)AuditingAction.Sumbitted,
                    Comment = description
                };
                CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(CurrentUser.Instance.UserId);
                dataAuditingContract.Process(commonUserInfo, recordSet.RecordEntities, dataAuditingLogInfo, dataAuditingStepInfo);
                frmPersonData.Close();
                MessageBox.Show("申请信息变提交成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            };     
            Dictionary<decimal, string> reviewers = null;
            if (dataAuditingInfo.InitReviewStatus)
            {
                if (dataAuditingInfo.EnableManager)
                {
                    reviewers = userAccountContract.GetManagementUsers(dataAuditingInfo.RoleId, CurrentUser.Instance.DepId);
                }
                else
                {
                    reviewers = dataAuditingContract.GetInitReviewers(dataAuditingInfo.DataAuditingId, CurrentUser.Instance.UserId);
                }
            }
            else if (dataAuditingInfo.AllocationStatus)
            {
                reviewers = new Dictionary<decimal, string>();
                CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(dataAuditingInfo.UserId);
                reviewers.Add(commonUserInfo.UserId, string.Format("{0}[{1}]", commonUserInfo.UserName, commonUserInfo.UserActualName));
            }
            else if (dataAuditingInfo.FinalReviewStatus)
            {
                reviewers = dataAuditingContract.GetFinalReviewers(dataAuditingInfo.DataAuditingId);
            }
            else
            {
                throw new ArgumentException("必须包含一个流程且至少包含终审（初审、分配或者终审）。");
            }
            frmPersonData.LoadReviewers(reviewers);
            frmPersonData.ShowDialog();
        }

        /// <summary>
        /// 申请增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkAdd_Click(object sender, EventArgs e)
        {
            IList<CommonNode> commonNodes = dataAuditingContract.GetDataAuditings(currentDataAuditingInfo.DataAuditingId);
            DropDownSelectedItemsForm frmDropDownSelectedItems = new DropDownSelectedItemsForm();
            frmDropDownSelectedItems.LoadCommonNodes(commonNodes);
            frmDropDownSelectedItems.NodeSelected = delegate (CommonNode node)
            {
                ShowPersonDataAddedForm(AuditingLogType.Add, node.NodeId, null);
            };
            frmDropDownSelectedItems.ShowDialog();            
        }

        /// <summary>
        /// 申请更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkApply_Click(object sender, EventArgs e)
        {
            if (dataTableControl.FocusedDataRow == null)
            {
                MessageBox.Show("请先选择申请信息变更对应的数据行。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<CommonNode> commonNodes = dataAuditingContract.GetDataAuditings(currentDataAuditingInfo.DataAuditingId);
            DropDownSelectedItemsForm frmDropDownSelectedItems = new DropDownSelectedItemsForm();
            frmDropDownSelectedItems.LoadCommonNodes(commonNodes);
            frmDropDownSelectedItems.NodeSelected = delegate (CommonNode node)
            {
                ShowPersonDataAddedForm(AuditingLogType.Edit, node.NodeId, dataTableControl.FocusedDataRow);
            };
            frmDropDownSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 查看记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkView_Click(object sender, EventArgs e)
        {
            PersonalDataLogForm frmPersonalDataLog = new PersonalDataLogForm();
            frmPersonalDataLog.DataAuditingId = currentDataAuditingInfo.DataAuditingId;
            frmPersonalDataLog.ShowDialog();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkRefresh_Click(object sender, EventArgs e)
        {
            dataTableControl.LoadData();
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkBack_Click(object sender, EventArgs e)
        {
            GoBack?.Invoke();
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData()
        {
            dataTableControl.LoadData();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            currentDataAuditingInfo = dataAuditingContract.GetModelInfo(ExtendedCustomBusinessInfo.DataAuditingId);
            FormType formType = (FormType)currentDataAuditingInfo.TableType;
            dataTableControl.LoadDataHanler = (devExpressGrid) =>
            {
                LoadUserData(devExpressGrid);
            };
            dataTableControl.AddHandler = (dataRow) =>
            {
                DataTemplateTableForm frmDataTemplateTable = new DataTemplateTableForm();
                frmDataTemplateTable.Text = ExtendedCustomBusinessInfo.BusinessName;
                long systemDataFieldAuthority = Convert.ToInt64(dataTableControl.DevExpressGrid.Tag);
                DataTableBusiness business = new DataTableBusiness(null, false, systemDataFieldAuthority, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                        customEnumContract, customDepartmentContract, frmDataTemplateTable.Panel, frmDataTemplateTable.MemoEditToolTip);
                /* 加载控件 */
                IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = dataTableControl.DevExpressGrid.Data as IList<ExtendedCustomDataFieldInfo>;
                int multiTextBoxCount = 0;
                business.CreateControls(extendedCustomDataFieldInfos, business.GetFormShowStyleSettings(FormShowStyle.SingleColumnThreeRanksCompleted), ref multiTextBoxCount);
                frmDataTemplateTable.SumbittedHandler = () =>
                {
                    RecordSet recordSet = business.GetRecordSet();
                    if (!recordSet.Success)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(recordSet.Warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(CurrentUser.Instance.UserId);
                    dataAuditingContract.Process(commonUserInfo, recordSet.RecordEntities);
                    dataTableControl.LoadData();
                    frmDataTemplateTable.Close();
                };
                if (dataRow != null)
                {
                    switch (formType)
                    {
                        case FormType.Table:
                            business.LoadDataOnTable(currentDataAuditingInfo.TableId, dataRow, true);
                            break;

                        case FormType.CombinedTable:
                            Dictionary<decimal, DataTable> dataRowValues = GetConvertionRow(currentDataAuditingInfo.CombinedTableId, dataRow);
                            business.LoadDataOnCombinedTables(dataRowValues, true);
                            break;

                        default:
                            throw new ArgumentException("不支持该枚举类型。");
                    }
                }
                else
                {
                    switch (formType)
                    {
                        case FormType.Table:
                            business.SetTableCategroy(TableCategroy.Single);
                            break;

                        case FormType.CombinedTable:
                            business.SetTableCategroy(TableCategroy.Multiple);
                            break;
                    }
                }
                frmDataTemplateTable.ShowDialog();
            };
            dataTableControl.EditHandler = (dataRow, readOnly) =>
            {
                if (dataRow != null)
                {
                    string auditedStatusName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
                    if (dataRow != null && dataRow.Table.Columns.Contains(auditedStatusName))
                    {
                        AuditedStatus auditedStatus = (AuditedStatus)Convert.ToByte(dataRow[auditedStatusName]);
                        if (auditedStatus != AuditedStatus.None)
                        {
                            return;
                        }
                    }
                    DataTemplateTableForm frmDataTemplateTable = new DataTemplateTableForm();
                    frmDataTemplateTable.Text = ExtendedCustomBusinessInfo.BusinessName;
                    frmDataTemplateTable.FormReadOnly = readOnly;
                    long systemDataFieldAuthority = Convert.ToInt64(dataTableControl.DevExpressGrid.Tag);
                    DataTableBusiness business  = new DataTableBusiness(null, readOnly, systemDataFieldAuthority, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                            customEnumContract, customDepartmentContract, frmDataTemplateTable.Panel, frmDataTemplateTable.MemoEditToolTip);
                    /* 加载控件 */
                    IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = dataTableControl.DevExpressGrid.Data as IList<ExtendedCustomDataFieldInfo>;
                    int multiTextBoxCount = 0;
                    business.CreateControls(extendedCustomDataFieldInfos, business.GetFormShowStyleSettings(FormShowStyle.SingleColumnThreeRanksCompleted), ref multiTextBoxCount);
                    frmDataTemplateTable.SumbittedHandler = () =>
                    {
                        RecordSet recordSet = business.GetRecordSet();
                        if (!recordSet.Success)
                        {
                            MessageBox.Show(recordSet.Warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(CurrentUser.Instance.UserId);
                        dataAuditingContract.Process(commonUserInfo, recordSet.RecordEntities);
                        dataTableControl.LoadData();
                        frmDataTemplateTable.Close();
                    };
                    switch (formType)
                    {
                        case FormType.Table:
                            business.LoadDataOnTable(currentDataAuditingInfo.TableId, dataRow, false);
                            break;

                        case FormType.CombinedTable:
                            Dictionary<decimal, DataTable> dataRowValues = GetConvertionRow(currentDataAuditingInfo.CombinedTableId, dataRow);
                            business.LoadDataOnCombinedTables(dataRowValues, false);
                            break;

                        default:
                            throw new ArgumentException("不支持该枚举类型。");
                    }
                    frmDataTemplateTable.ShowDialog();
                }
            };
            dataTableControl.MoveRecordHandler = (userId, tableId, recordId, movedDriection) =>
            {
                switch (formType)
                {
                    case FormType.Table:
                        customTableContract.MoveRecord(userId, tableId, recordId, movedDriection);
                        break;
                }
            };
            dataTableControl.DeleteHandler = (recordIds) =>
            {
                switch (formType)
                {
                    case FormType.Table:
                        customTableContract.DeleteRecords(currentDataAuditingInfo.TableId, recordIds);
                        break;
                }
            };
            dataTableControl.SetAuthorityHandler = (devExpressGrid, btnAdd, btnDelete) =>
            {
                Int64 authority = 0;
                Int64 tableAuthority = 0;
                DataTableType dataTableType = DataTableType.PrimaryTable;
                switch (formType)
                {
                    case FormType.Table:
                        dataTableType = (DataTableType)customTableContract.GetTableType(currentDataAuditingInfo.TableId);
                        tableAuthority = customRoleContract.GetTableAuthority(CurrentUser.Instance.UserId, currentDataAuditingInfo.TableId, DataAuthorityType.Business);
                        break;

                    case FormType.CombinedTable:
                        IList<decimal> tableIds = combinedTableContract.GetTableIds(currentDataAuditingInfo.CombinedTableId);
                        tableAuthority = customRoleContract.GetTableAuthority(CurrentUser.Instance.UserId, tableIds[0], DataAuthorityType.Business);
                        break;
                }
                dataTableControl.AllowDataExported = AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Export);
                dataTableControl.AllowDataImported = AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Import);
                dataTableControl.AllowStatusSetting = (dataTableType == DataTableType.MasterSlaveTable) && AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.MasterSlave);
                if (AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Add))
                {
                    authority |= AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.Add);
                }              
                if (AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Edit))
                {
                    authority |= AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.Edit);
                }
                if ((formType == FormType.Table) && AuthorityHelper.CheckAuthority(tableAuthority, (byte)GridViewAuthority.Delete))
                {
                    authority |= AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.Delete);
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnDelete.Enabled = false;
                }
                authority |= AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.Move);
                devExpressGrid.Authority = authority;                
            };
            dataTableControl.UpdateCurretStateHandler = (recordId) =>
            {
                switch (formType)
                {
                    case FormType.Table:
                        dataAuditingContract.UpdateCurretStateByUserId(currentDataAuditingInfo.TableId, recordId, CurrentUser.Instance.UserId);
                        break;
                }
            };
            switch (formType)
            {
                case FormType.Table:
                    DataTableType dataTableType = (DataTableType)customTableContract.GetDataTableType(currentDataAuditingInfo.TableId);
                    dataTableControl.DevExpressGrid.IsMainTable = (dataTableType == DataTableType.PrimaryTable) ? true : false;
                    break;

                case FormType.CombinedTable:
                    dataTableControl.DevExpressGrid.IsMainTable = true;
                    break;
            }
            dataTableControl.SetAuthority();
        }

        /// <summary>
        /// 获得转换数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataRow"></param>
        /// <returns></returns>
        private Dictionary<decimal, DataTable> GetConvertionRow(decimal combinedTableId, DataRow dataRow)
        {
            Dictionary<decimal, DataTable> dataRowValues = new Dictionary<decimal, DataTable>();

            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            IList<CommonNode> tableInfos = combinedTableContract.GetTables(combinedTableId);
            IList<CommonNode> dataFieldInfos = combinedTableContract.GetDataFields(combinedTableId);
            foreach (var tableInfo in tableInfos)
            {
                string dataKeyName = string.Format("{0}_{1}", tableInfo.NodeCode, recordIdName);
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(recordIdName, dataRow.Table.Columns[dataKeyName].DataType);
                foreach (var dataFieldInfo in dataFieldInfos)
                {
                    if (dataFieldInfo.ParentNodeId == tableInfo.ParentNodeId && dataRow.Table.Columns.Contains(dataFieldInfo.NodeCode))
                    {
                        dataTable.Columns.Add(dataFieldInfo.NodeCode, dataRow.Table.Columns[dataFieldInfo.NodeCode].DataType);
                    }
                }
                DataRow row = dataTable.NewRow();
                row[recordIdName] = dataRow[dataKeyName];
                foreach (var dataFieldInfo in dataFieldInfos)
                {
                    if (dataFieldInfo.ParentNodeId == tableInfo.ParentNodeId && dataRow.Table.Columns.Contains(dataFieldInfo.NodeCode))
                    {
                        row[dataFieldInfo.NodeCode] = dataRow[dataFieldInfo.NodeCode];
                    }
                }
                dataTable.Rows.Add(row);
                dataRowValues.Add(tableInfo.NodeId, dataTable);
            }

            return dataRowValues;
        }

        /// <summary>
        /// 加载用户数据
        /// </summary>
        /// <param name="devExpressGrid"></param>
        private void LoadUserData(DevExpressGrid devExpressGrid)
        {
            string tablePhysicalName = string.Empty;
            Dictionary<string, CommonDataFieldInfo> systemDataFieldNameRelations = null;
            IList<CommonNode> dataFields = null;
            IList<decimal> tableIds = new List<decimal>();
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            FormType formType = (FormType)currentDataAuditingInfo.TableType;
            switch (formType)
            {
                case FormType.Table:
                    devExpressGrid.DataKeyNames = new string[] { recordIdName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId),
                        DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId) };
                    tableIds.Add(currentDataAuditingInfo.TableId);
                    tablePhysicalName = customTableContract.GetTablePhysicalName(currentDataAuditingInfo.TableId);
                    break;

                case FormType.CombinedTable:
                    IList<CommonNode> commonNodeInfos = combinedTableContract.GetTables(currentDataAuditingInfo.CombinedTableId);
                    IList<string> dataKeyNames = new List<string>();
                    foreach (var commonNodeInfo in commonNodeInfos)
                    {
                        if (string.IsNullOrWhiteSpace(tablePhysicalName))
                        {
                            tablePhysicalName = commonNodeInfo.NodeCode;
                        }
                        dataKeyNames.Add(string.Format("{0}_{1}", commonNodeInfo.NodeCode, recordIdName));
                        tableIds.Add(commonNodeInfo.NodeId);
                    }
                    devExpressGrid.DataKeyNames = dataKeyNames.ToArray();
                    dataFields = combinedTableContract.GetDataFields(currentDataAuditingInfo.CombinedTableId);
                    break;
            }            
            List <ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = customRoleContract.GetAuthorizedExtendedCustomDataFieldInfos(CurrentUser.Instance.UserId, tableIds, DataAuthorityType.Business);
            int totalCount = 0;
            Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
            systemDataFieldNameRelations = DataFieldHelper.GetSystemDataFieldInfo(tablePhysicalName, currentDataAuditingInfo.SystemDataFieldAuthority);
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in systemDataFieldNameRelations)
            {
                dataFieldNameRelations.Add(keyValue.Key, keyValue.Value);
            }
            /* 对于组合表类型的个人信息业务字段：组合表权限与角色权限的交集；
             * 对于数据表类型的个人信息业务字段：角色权限集合
             *  */
            List<ExtendedCustomDataFieldInfo> newExtendedCustomDataFieldInfos = null;
            if (formType == FormType.CombinedTable)
            {
                newExtendedCustomDataFieldInfos = new List<ExtendedCustomDataFieldInfo>(extendedCustomDataFieldInfos.Count);
                foreach (var dataField in dataFields)
                {
                    int pos = extendedCustomDataFieldInfos.FindIndex(customDataFieldInfo => dataField.NodeId == customDataFieldInfo.DataFieldId);
                    if (pos < 0)
                    {
                        continue;
                    }
                    newExtendedCustomDataFieldInfos.Add(extendedCustomDataFieldInfos[pos]);
                }
            }
            else
            {
                newExtendedCustomDataFieldInfos = extendedCustomDataFieldInfos;
            }
            foreach (ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo in newExtendedCustomDataFieldInfos)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
                string expressionText = string.Empty;
                if (dataFieldProperty == DataFieldProperty.LogicalDataField)
                {
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    if (logicalDataFieldType == LogicalDataFieldType.DigitExpression || logicalDataFieldType == LogicalDataFieldType.StringExpression
                        || logicalDataFieldType == LogicalDataFieldType.DateTimeExpression)
                    {
                        expressionText = customDataFieldContract.GetDataFieldLogicalExpression(extendedCustomDataFieldInfo.DataFieldId);
                    }
                }
                dataFieldNameRelations.Add(extendedCustomDataFieldInfo.PhysicalName,
                        new CommonDataFieldInfo(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.TableId, extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.LogicalName,
                        expressionText, dataFieldProperty, extendedCustomDataFieldInfo.DataFieldType));
            }
            devExpressGrid.Data = newExtendedCustomDataFieldInfos;
            devExpressGrid.Tag = currentDataAuditingInfo.SystemDataFieldAuthority;
            IList<WhereConditon> conditons = new List<WhereConditon>();
            conditons.Add(new WhereConditon(tablePhysicalName, "UserId", "UserId", DbType.Decimal, CurrentUser.Instance.UserId,
                                            DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            DataTable dt = null;
            switch (formType)
            {
                case FormType.Table:
                    IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                    DataTableType dataTableType = (DataTableType)customTableContract.GetDataTableType(currentDataAuditingInfo.TableId);
                    if (dataTableType == DataTableType.MasterSlaveTable)
                    {
                        sortingCondtions.Add(new SortingCondtion("CurrentState", CustomSorting.Descending));
                    }
                    sortingCondtions.Add(new SortingCondtion("RecordSorting", CustomSorting.Ascending));
                    devExpressGrid.IsMainTable = (dataTableType == DataTableType.PrimaryTable) ? true : false;
                    dt = customTableContract.GetTableData(currentDataAuditingInfo.TableId, currentDataAuditingInfo.SystemDataFieldAuthority, false, false, dataFieldNameRelations,
                                            devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, devExpressGrid.PageSize, conditons, sortingCondtions, ref totalCount);
                    break;

                case FormType.CombinedTable:
                    devExpressGrid.IsMainTable = true;
                    dt = combinedTableContract.GetCombinedTableData(currentDataAuditingInfo.CombinedTableId, false, dataFieldNameRelations, CurrentUser.Instance.UserId, decimal.MinValue);
                    totalCount = dt != null ? dt.Rows.Count : 0;
                    break;
            }
            hlnkAdd.Enabled = !devExpressGrid.IsMainTable;
            devExpressGrid.RecordCount = totalCount;
            devExpressGrid.DataSource = dt;
            devExpressGrid.ColumnAutoWidth = false;
            devExpressGrid.AppearanceCellHAlignment = HorzAlignment.Center;
            devExpressGrid.AppearanceHeaderHAlignment = HorzAlignment.Center;
            devExpressGrid.OnCustomColumnDisplayText += (sender, e) =>
            {
                SystemConfigHelper.SetColumnDisplayText(e);
            };
            foreach (GridColumn gridColumn in devExpressGrid.Columns)
            {
                if (gridColumn.VisibleIndex < AUDITING_GRID_VIEW_FIXED_COLUMN_COUNT || !gridColumn.Visible)
                {
                    continue;
                }
                if (dataFieldNameRelations.ContainsKey(gridColumn.FieldName))
                {
                    UserControlHelper.SetColumnDisplayText(gridColumn, dataFieldNameRelations[gridColumn.FieldName]);
                }
            }
            hlnkReport.Enabled = (currentDataAuditingInfo.ReportId > 0);
        }


        #endregion
        
    }
}
