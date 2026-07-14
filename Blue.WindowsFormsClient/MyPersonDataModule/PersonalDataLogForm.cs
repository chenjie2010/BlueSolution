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
using AppFramework.Reference.DataFieldLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using Blue.Model.DataFilledModule;
using Blue.WindowsFormsClient.Common;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.MyPersonDataModule
{
    public partial class PersonalDataLogForm : Form
    {
        #region 私有变量

        private DataAuditingInfo currentDataAuditingInfo;

        #endregion

        #region 契约接口

        private readonly IDataAuditingContract dataAuditingContract;
        private readonly IDataAuditingStepContract dataAuditingStepContract;
        private readonly IDataAuditingLogContract dataAuditingLogContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICombinedTableContract combinedTableContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly IUserAccountContract userAccountContract;

        #endregion

        #region 属性

        /// <summary>
        /// 审核编号
        /// </summary>
        public decimal DataAuditingId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PersonalDataLogForm()
        {
            InitializeComponent();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();
            dataAuditingStepContract = BusinessDesignerChannelFactory.CreateDataAuditingStepContract();
            dataAuditingLogContract = BusinessDesignerChannelFactory.CreateDataAuditingLogContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            combinedTableContract = BusinessChannelFactory.CreateCombinedTableContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
            customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonalDataLogForm_Load(object sender, EventArgs e)
        {
            currentDataAuditingInfo = dataAuditingContract.GetModelInfo(DataAuditingId);
            degAuditingData.CurrentPageIndex = 0;
            long view = AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.View);
            long edit = AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.Edit);
            long delete = AuthorityHelper.GetShiftedValue(0, (byte)GridViewAuthority.Delete);
            degAuditingData.Authority = view | edit | delete;
            LoadData(false);
            degAuditedData.CurrentPageIndex = 0;
            LoadData(true);
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void degAuditingData_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            degAuditingData.CurrentPageIndex = e.NewPageIndex;
            LoadData(false);
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void degAuditedData_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            degAuditedData.CurrentPageIndex = e.NewPageIndex;
            LoadData(true);
        }

        /// <summary>
        /// 自定义状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void degAuditingData_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AuditingStatus")
            {
                AuditingStatus auditingStatus = (AuditingStatus)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(auditingStatus);
            }
        }

        /// <summary>
        /// 自定义状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void degAuditedData_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AuditingStatus")
            {
                AuditingStatus auditingStatus = (AuditingStatus)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(auditingStatus);
            }
        }

        /// <summary>
        /// 单元格样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void degAuditingData_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle >= 0 && e.Column.Name.Equals(degAuditingData.CustomEditedName))
            {
                AuditingStatus auditingStatus = (AuditingStatus)Convert.ToByte(degAuditingData.GetRowCellValue(e.RowHandle, "AuditingStatus"));
                if (auditingStatus != AuditingStatus.None)
                {
                    e.Appearance.ForeColor = Color.LightGray;
                }
            }
        }

        /// <summary>
        /// 查看流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkView_Click(object sender, EventArgs e)
        {
            if (degAuditingData.FocusedRowHandle >= 0)
            {
                decimal auditingLogId = Convert.ToDecimal(degAuditingData.DataKeyValues.Value);
                byte auditingStatus = Convert.ToByte(degAuditingData.FocusedDataRow["AuditingStatus"]);
                ShowLog(auditingLogId, (AuditingStatus)auditingStatus);
            }
        }

        /// <summary>
        /// 查看流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkViewLog_Click(object sender, EventArgs e)
        {
            if (degAuditedData.FocusedRowHandle >= 0)
            {
                decimal auditingLogId = Convert.ToDecimal(degAuditedData.DataKeyValues.Value);
                byte auditingStatus = Convert.ToByte(degAuditedData.FocusedDataRow["AuditingStatus"]);
                ShowLog(auditingLogId, (AuditingStatus)auditingStatus);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void degAuditingData_OnRowEdit(object sender, AppFramework.WinFormsControls.RowEvent e)
        {
            AuditingStatus auditingStatus = (AuditingStatus)Convert.ToByte(degAuditingData.GetRowCellValue(e.RowHandle, "AuditingStatus"));
            if (auditingStatus != AuditingStatus.None)
            {
                MessageBox.Show(string.Format("只允许对处于草稿状态的信息更新申请进行编辑。", UserEnumHelper.GetEnumText(auditingStatus)), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal auditingLogId = Convert.ToDecimal(degAuditingData.DataKeyValues.Value);
            decimal dataAuditingId = Convert.ToDecimal(degAuditingData.DataKeyValues["DataAuditingId"]);
            DataAuditingInfo dataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingId);
            ShowPersonDataEditedForm(auditingLogId, dataAuditingInfo, false);
        }

        /// <summary>
        /// 撤回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (degAuditingData.FocusedRowHandle >= 0)
            {
                decimal auditingLogId = Convert.ToDecimal(degAuditingData.DataKeyValues.Value);
                DataAuditingLogInfo dataAuditingLogInfo = dataAuditingLogContract.GetModelInfo(auditingLogId);
                AuditingStatus auditingStatus = (AuditingStatus)dataAuditingLogInfo.AuditingStatus;
                if (auditingStatus == AuditingStatus.None || auditingStatus == AuditingStatus.Completed)
                {
                    MessageBox.Show(string.Format("不能撤回{0}的信息更新申请。", UserEnumHelper.GetEnumText(auditingStatus)), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认撤回该信息更新申请？", "警告信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    dataAuditingLogContract.WithDraw(auditingLogId, CurrentUser.Instance.UserId, AuditingStatus.None);
                    LoadData(false);
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void degAuditingData_OnDeleteClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (degAuditingData.FocusedRowHandle >= 0)
            {
                decimal auditingLogId = Convert.ToDecimal(degAuditingData.DataKeyValues.Value);
                DataAuditingLogInfo dataAuditingLogInfo = dataAuditingLogContract.GetModelInfo(auditingLogId);
                AuditingStatus auditingStatus = (AuditingStatus)dataAuditingLogInfo.AuditingStatus;
                if (auditingStatus != AuditingStatus.None)
                {
                    MessageBox.Show("只能删除处于草稿状态的信息更新申请。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                dataAuditingLogContract.Delete(auditingLogId);
                LoadData(false);
            }
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSteps_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            fpSteps.HideBeakForm();
        }

        /// <summary>
        /// 自定义显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSteps_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AuditingAction")
            {
                AuditingAction auditingAction = (AuditingAction)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(auditingAction);
            }
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void degAuditingData_OnRowView(object sender, RowEvent e)
        {
            decimal auditingLogId = Convert.ToDecimal(degAuditingData.DataKeyValues.Value);
            decimal dataAuditingId = Convert.ToDecimal(degAuditingData.DataKeyValues["DataAuditingId"]);
            DataAuditingInfo dataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingId);
            ShowPersonDataEditedForm(auditingLogId, dataAuditingInfo, true);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 显示日志
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <param name="auditingStatus"></param>
        private void ShowLog(decimal auditingLogId, AuditingStatus auditingStatus)
        {
            switch (auditingStatus)
            {
                case AuditingStatus.None:
                    lblName.Text = "草稿状态，待提交。";
                    lblReviewer.Visible = false;
                    break;

                case AuditingStatus.Auditing:
                case AuditingStatus.Audited:
                case AuditingStatus.Allocating:
                    CommonNode commonNode = dataAuditingStepContract.GetLastestReviewer(auditingLogId);
                    lblReviewer.Text = string.Format("{0}({1})", commonNode.NodeCode, commonNode.NodeName);
                    lblName.Text = "下一步审核人：";
                    lblReviewer.Visible = true;
                    break;


                case AuditingStatus.Completed:
                    lblName.Text = "审核已完成。";
                    lblReviewer.Visible = false;
                    break;
            }
            gcSteps.DataSource = dataAuditingStepContract.GetSteps(auditingLogId).Tables[0];
            gvSteps.Columns["StepId"].Visible = false;
            gvSteps.Columns["AuditingTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            gvSteps.Columns["AuditingTime"].DisplayFormat.FormatString = "f";
            gvSteps.BestFitColumns();
            gvSteps.Columns["Comment"].Width = 100;            
            if (fpSteps.FlyoutPanelState.IsActive)
            {
                return;
            }
            fpSteps.ShowBeakForm();
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="completed"></param>
        private void LoadData(bool completed)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            whereConditons.Add(new WhereConditon("DataAuditing", "ParentDataAuditingId", "ParentDataAuditingId", DbType.Decimal, DataAuditingId,
   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("DataAuditingLog", "ParentUserId", "ParentUserId", DbType.Decimal, CurrentUser.Instance.UserId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            if (completed)
            {
                whereConditons.Add(new WhereConditon("DataAuditingLog", "AuditingStatus", "AuditingStatus", DbType.Byte, (byte)AuditingStatus.Completed,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            else
            {
                whereConditons.Add(new WhereConditon("DataAuditingLog", "AuditingStatus", "AuditingStatus", DbType.Byte, (byte)AuditingStatus.Completed,
                   DataFieldCondition.Not, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            sortingCondtions.Add(new SortingCondtion("AuditingLogTime", CustomSorting.Descending));
            int totalCount = 0;
            DataTable dt = dataAuditingLogContract.GetPageRecord(degAuditingData.PageSize * degAuditingData.CurrentPageIndex,
                    degAuditingData.PageSize, whereConditons, sortingCondtions, ref totalCount).Tables[0];
            if (completed)
            {
                degAuditedData.DataSource = dt;
                degAuditedData.RecordCount = totalCount;
                degAuditedData.Columns["AuditingLogName"].Width = 150;
                degAuditedData.Columns["AuditingLogTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                degAuditedData.Columns["AuditingLogTime"].DisplayFormat.FormatString = "f";
                degAuditedData.Columns["AuditingStatus"].Width = 50;
                degAuditedData.Columns["LogDescription"].Width = 120;
                degAuditedData.Columns["AuditingLogTime"].Width = 80;
            }
            else
            {
                degAuditingData.DataSource = dt;
                degAuditingData.RecordCount = totalCount;
                degAuditingData.Columns["AuditingLogName"].Width = 150;
                degAuditingData.Columns["AuditingLogTime"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                degAuditingData.Columns["AuditingLogTime"].DisplayFormat.FormatString = "f";
                degAuditingData.Columns["AuditingStatus"].Width = 50;
                degAuditingData.Columns["LogDescription"].Width = 120;
                degAuditingData.Columns["AuditingLogTime"].Width = 80;
            }
        }

        /// <summary>
        /// 编辑申请
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <param name="dataAuditingInfo"></param>
        /// <param name="readOnly"></param>
        private void ShowPersonDataEditedForm(decimal auditingLogId, DataAuditingInfo dataAuditingInfo, bool readOnly)
        {
            PersonDataForm frmPersonData = new PersonDataForm();
            frmPersonData.ReadOnly = readOnly;
            frmPersonData.Text = dataAuditingInfo.DataAuditingName;
            DataAuditingLogInfo dataAuditingLogInfo = dataAuditingLogContract.GetModelInfo(auditingLogId);
            string tablePhysicalName = string.Empty;
            DataAuditingInfo parentDataDataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingInfo.ParentDataAuditingId);
            IList<CommonNode> dataFields = dataAuditingContract.GetDataFields(dataAuditingInfo.DataAuditingId);
            IList<decimal> tableIds = new List<decimal>();
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            FormType formType = (FormType)parentDataDataAuditingInfo.TableType;
            switch (formType)
            {
                case FormType.Table:
                    tableIds.Add(parentDataDataAuditingInfo.TableId);
                    tablePhysicalName = customTableContract.GetTablePhysicalName(parentDataDataAuditingInfo.TableId);
                    break;

                case FormType.CombinedTable:
                    IList<CommonNode> commonNodeInfos = combinedTableContract.GetTables(parentDataDataAuditingInfo.CombinedTableId);
                    foreach (var commonNodeInfo in commonNodeInfos)
                    {
                        if (string.IsNullOrWhiteSpace(tablePhysicalName))
                        {
                            tablePhysicalName = commonNodeInfo.NodeCode;
                        }
                        tableIds.Add(commonNodeInfo.NodeId);
                    }
                    break;
            }
            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = customRoleContract.GetAuthorizedExtendedCustomDataFieldInfos(CurrentUser.Instance.UserId, tableIds, DataAuthorityType.Business);
            CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(dataAuditingLogInfo.UserId);            
            DataTableBusiness business = new DataTableBusiness(decimal.MinValue, commonUserInfo, readOnly, dataAuditingInfo.SystemDataFieldAuthority, customTableContract, customDataFieldContract, associatedDataFieldContract, customAssociationContract,
                    customEnumContract, customDepartmentContract, frmPersonData.Panel, frmPersonData.MemoEditToolTip);
            IList<ExtendedCustomDataFieldInfo> customDataFieldInfos = new List<ExtendedCustomDataFieldInfo>();
            foreach (var dataField in dataFields)
            {
                int pos = extendedCustomDataFieldInfos.FindIndex(extendedCustomDataFieldInfo => dataField.NodeId == extendedCustomDataFieldInfo.DataFieldId);
                if (pos < 0)
                {
                    continue;
                }
                customDataFieldInfos.Add(extendedCustomDataFieldInfos[pos]);
            }
            //foreach (var extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
            //{
            //    if (dataFields.FindIndex(dataField => dataField.NodeId == extendedCustomDataFieldInfo.DataFieldId) < 0)
            //    {
            //        continue;
            //    }
            //    customDataFieldInfos.Add(extendedCustomDataFieldInfo);
            //}
            int multiTextBoxCount = 0;
            business.CreateControls(customDataFieldInfos, business.GetFormShowStyleSettings(FormShowStyle.SingleColumnThreeRanksCompleted), ref multiTextBoxCount);
            Dictionary<decimal, decimal> tableIdAndRecordIds = new Dictionary<decimal, decimal>();
            Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
            Dictionary<string, CommonDataFieldInfo> systemDataFieldNameRelations = DataFieldHelper.GetSystemDataFieldInfo(tablePhysicalName, dataAuditingInfo.SystemDataFieldAuthority);
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in systemDataFieldNameRelations)
            {
                dataFieldNameRelations.Add(keyValue.Key, keyValue.Value);
            }
            foreach (var customDataFieldInfo in customDataFieldInfos)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                string expressionText = string.Empty;
                if (dataFieldProperty == DataFieldProperty.LogicalDataField)
                {
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)customDataFieldInfo.DataFieldType;
                    if (logicalDataFieldType == LogicalDataFieldType.DigitExpression || logicalDataFieldType == LogicalDataFieldType.StringExpression
                        || logicalDataFieldType == LogicalDataFieldType.DateTimeExpression)
                    {
                        expressionText = customDataFieldContract.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId);
                    }
                }
                dataFieldNameRelations.Add(customDataFieldInfo.PhysicalName,
                        new CommonDataFieldInfo(customDataFieldInfo.DataFieldId, customDataFieldInfo.TableId, customDataFieldInfo.PhysicalName, customDataFieldInfo.LogicalName,
                        expressionText, dataFieldProperty, customDataFieldInfo.DataFieldType));
            }
            switch (formType)
            {
                case FormType.Table:
                    DataTable dataTable = customTableContract.GetMirrorRowData(parentDataDataAuditingInfo.TableId, dataAuditingInfo.SystemDataFieldAuthority, dataFieldNameRelations, auditingLogId, true);
                    if (dataTable.Rows.Count > 0)
                    {
                        business.LoadDataOnTable(parentDataDataAuditingInfo.TableId, dataTable.Rows[0], false);
                        tableIdAndRecordIds.Add(parentDataDataAuditingInfo.TableId, DataConvertionHelper.GetDecimal(dataTable.Rows[0][recordIdName], 0));
                    }
                    break;

                case FormType.CombinedTable:
                    Dictionary<decimal, DataTable> dataRowValues = combinedTableContract.GetMirrorRowData(parentDataDataAuditingInfo.CombinedTableId, dataFieldNameRelations, auditingLogId);
                    foreach (KeyValuePair<decimal, DataTable> keyValue in dataRowValues)
                    {
                        if (keyValue.Value.Rows.Count > 0)
                        {
                            tableIdAndRecordIds.Add(keyValue.Key, DataConvertionHelper.GetDecimal(keyValue.Value.Rows[0][recordIdName], 0));
                        }
                    }
                    business.LoadDataOnCombinedTables(dataRowValues, false);
                    break;

                default:
                    throw new ArgumentException("不支持该枚举类型。");
            }
            frmPersonData.SumbittedHandler = (reviewerId, description) =>
            {
                RecordSet recordSet = business.GetRecordSet();
                foreach (var recordEntitiy in recordSet.RecordEntities)
                {
                    if (tableIdAndRecordIds.ContainsKey(recordEntitiy.TableId))
                    {
                        recordEntitiy.BusinessAlternativeId = tableIdAndRecordIds[recordEntitiy.TableId];
                    }
                }
                if (!recordSet.Success)
                {
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
                dataAuditingLogInfo.AuditingLogName = string.Format("{0}_{1}_{2}_{3}", currentDataAuditingInfo.DataAuditingName, CurrentUser.Instance.UserName,
                    CurrentUser.Instance.UserActualName, DateTime.Now.ToString("G"));
                dataAuditingLogInfo.LogDescription = description;
                dataAuditingLogInfo.AuditingStatus = (byte)auditingStatus;
                DataAuditingStepInfo dataAuditingStepInfo = new DataAuditingStepInfo()
                {
                    ParentUserId = CurrentUser.Instance.UserId,
                    UserId = reviewerId,
                    AuditingAction = (byte)AuditingAction.Sumbitted,
                    Comment = description
                };
                CommonUserInfo currentCommonUserInfo = userAccountContract.GetCommonUserInfo(CurrentUser.Instance.UserId);
                dataAuditingContract.ProcessWithLog(currentCommonUserInfo, recordSet.RecordEntities, dataAuditingLogInfo, dataAuditingStepInfo);
                LoadData(false);
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
                CommonUserInfo currentCommonUserInfo = userAccountContract.GetCommonUserInfo(dataAuditingInfo.UserId);
                reviewers.Add(currentCommonUserInfo.UserId, string.Format("{0}[{1}]", currentCommonUserInfo.UserName, currentCommonUserInfo.UserActualName));
            }
            else if (dataAuditingInfo.FinalReviewStatus)
            {
                reviewers = dataAuditingContract.GetFinalReviewers(dataAuditingInfo.DataAuditingId);
            }
            else
            {
                throw new ArgumentException("必须包含一个流程且至少包含终审（初审、分配或者终审）。");
            }
            frmPersonData.Comment = dataAuditingLogInfo.LogDescription;
            frmPersonData.LoadReviewers(reviewers);
            frmPersonData.ShowDialog();
        }

        #endregion        
    }
}
