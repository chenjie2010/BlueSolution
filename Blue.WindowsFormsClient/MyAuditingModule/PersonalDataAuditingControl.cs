using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.MyAuditingModule
{
    public partial class PersonalDataAuditingControl : UserControl
    {
        #region 私有变量
        

        #endregion

        #region 契约接口

        private readonly IDataAuditingContract dataAuditingContract;
        private readonly IDataAuditingStepContract dataAuditingStepContract;
        private readonly IDataAuditingLogContract dataAuditingLogContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICombinedTableContract combinedTableContract;

        #endregion

        #region 属性

        /// <summary>
        /// 当前状态
        /// </summary>
        public InfoStatus CurrentInfoStatus
        {
            get;
            set;
        }

        /// <summary>
        /// 业务名称
        /// </summary>
        public string BussinessName
        {
            set
            {
                gcBusiness.Text = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PersonalDataAuditingControl()
        {
            InitializeComponent();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();
            dataAuditingStepContract = BusinessDesignerChannelFactory.CreateDataAuditingStepContract();
            dataAuditingLogContract = BusinessDesignerChannelFactory.CreateDataAuditingLogContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            combinedTableContract = BusinessChannelFactory.CreateCombinedTableContract();
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSteps_ButtonClick(object sender, FlyoutPanelButtonClickEventArgs e)
        {
            fpSteps.HideBeakForm();
        }

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonalDataAuditingControl_Load(object sender, EventArgs e)
        {
            switch (CurrentInfoStatus)
            {
                case InfoStatus.InfoAllocating:
                    btnAudit.Visible = false;
                    btnAllocate.Visible = true;
                    break;

                case InfoStatus.InfoAuditing:
                case InfoStatus.InfoAudited:
                    btnAllocate.Visible = false;
                    btnAudit.Visible = true;
                    break;
            }
            LoadData();
        }
        
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkFresh_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 审核通过
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAudit_Click(object sender, EventArgs e)
        {
            try
            {
                if (devExpressGrid.FocusedRowHandle < 0)
                {
                    MessageBox.Show("请先选择需要审核的信息更新申请。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Cursor = Cursors.WaitCursor;
                decimal nextReviewerId = decimal.MinValue;
                decimal auditingLogId = Convert.ToDecimal(devExpressGrid.DataKeyValues.Value);
                DataAuditingLogInfo dataAuditingLogInfo = dataAuditingLogContract.GetModelInfo(auditingLogId);
                switch (CurrentInfoStatus)
                {
                    case InfoStatus.InfoAuditing:
                        DataAuditingInfo dataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingLogInfo.DataAuditingId);
                        AuditingStatus newAuditingStatus = AuditingStatus.None;
                        if (dataAuditingInfo.AllocationStatus)
                        {
                            newAuditingStatus = AuditingStatus.Allocating;
                            nextReviewerId = dataAuditingInfo.UserId;
                            SubmitBusinessToNextStep(dataAuditingLogInfo, nextReviewerId, (AuditingStatus)dataAuditingLogInfo.AuditingStatus, newAuditingStatus,
                            AuditingAction.Pass, string.Empty);
                        }
                        else if (dataAuditingInfo.FinalReviewStatus)
                        {
                            newAuditingStatus = AuditingStatus.Audited;
                            Dictionary<decimal, string> finalReviewers = dataAuditingContract.GetFinalReviewers(dataAuditingInfo.DataAuditingId);
                            if (finalReviewers.Count == 1)
                            {
                                nextReviewerId = finalReviewers.First().Key;
                                SubmitBusinessToNextStep(dataAuditingLogInfo, nextReviewerId, (AuditingStatus)dataAuditingLogInfo.AuditingStatus, newAuditingStatus,
                                    AuditingAction.Pass, string.Empty);
                            }
                            else
                            {
                                DropDownSelectedItemsForm frmDropDownSelectedItems = new DropDownSelectedItemsForm();
                                IList<CommonNode> commonNodes = new List<CommonNode>();
                                foreach (var finalReviewer in finalReviewers)
                                {
                                    commonNodes.Add(new CommonNode(finalReviewer.Key, finalReviewer.Value));
                                }
                                frmDropDownSelectedItems.LoadCommonNodes(commonNodes);
                                frmDropDownSelectedItems.NodeSelected = delegate (CommonNode node)
                                {
                                    if (node == null)
                                    {
                                        return;
                                    }
                                    SubmitBusinessToNextStep(dataAuditingLogInfo, node.NodeId, (AuditingStatus)dataAuditingLogInfo.AuditingStatus, newAuditingStatus,
                                        AuditingAction.Pass, string.Empty);
                                };
                                frmDropDownSelectedItems.ShowDialog();
                            }
                        }
                        else
                        {
                            throw new ArgumentException("不支持下一步操作。");
                        }
                        break;

                    case InfoStatus.InfoAudited:
                        CommonNode commonNode = dataAuditingStepContract.GetLastestReviewer(dataAuditingLogInfo.AuditingLogId);
                        if (commonNode.NodeId == CurrentUser.Instance.UserId && (AuditingStatus)dataAuditingLogInfo.AuditingStatus == AuditingStatus.Audited)
                        {
                            dataAuditingLogContract.CompleteBusiness(dataAuditingLogInfo.AuditingLogId, CurrentUser.Instance.UserId, string.Empty);
                        }
                        else
                        {
                            MessageBox.Show("不能处理当前的信息更新申请：（1）信息更新申请已经被处理（2）处理人发生变化。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;

                    default:
                        throw new ArgumentException("不支持该属性。");
                }
                devExpressGrid.CurrentPageIndex = 0;
                LoadData();
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
        /// 分配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAllocate_Click(object sender, EventArgs e)
        {
            if (devExpressGrid.FocusedRowHandle < 0)
            {
                MessageBox.Show("请先选择需要分配的信息更新申请。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal nextReviewerId = decimal.MinValue;
            decimal auditingLogId = Convert.ToDecimal(devExpressGrid.DataKeyValues.Value);
            DataAuditingLogInfo dataAuditingLogInfo = dataAuditingLogContract.GetModelInfo(auditingLogId);
            //DataAuditingInfo dataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingLogInfo.DataAuditingId);
            Dictionary<decimal, string> finalReviewers = dataAuditingContract.GetFinalReviewers(dataAuditingLogInfo.DataAuditingId);
            if (finalReviewers.Count == 1)
            {
                nextReviewerId = finalReviewers.First().Key;
                SubmitBusinessToNextStep(dataAuditingLogInfo, nextReviewerId, (AuditingStatus)dataAuditingLogInfo.AuditingStatus, AuditingStatus.Audited,
                AuditingAction.Allocating, string.Empty);
            }
            else
            {
                DropDownSelectedItemsForm frmDropDownSelectedItems = new DropDownSelectedItemsForm();
                IList<CommonNode> commonNodes = new List<CommonNode>();
                foreach (var finalReviewer in finalReviewers)
                {
                    commonNodes.Add(new CommonNode(finalReviewer.Key, finalReviewer.Value));
                }
                frmDropDownSelectedItems.LoadCommonNodes(commonNodes);
                frmDropDownSelectedItems.NodeSelected = delegate (CommonNode node)
                {
                    if (node == null)
                    {
                        return;
                    }
                    SubmitBusinessToNextStep(dataAuditingLogInfo, node.NodeId, (AuditingStatus)dataAuditingLogInfo.AuditingStatus, AuditingStatus.Audited, 
                        AuditingAction.Allocating, string.Empty);
                };
                frmDropDownSelectedItems.ShowDialog();
            }
            devExpressGrid.CurrentPageIndex = 0;
            LoadData();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReject_Click(object sender, EventArgs e)
        {
            try
            {
                if (devExpressGrid.FocusedRowHandle < 0)
                {
                    MessageBox.Show("请先选择需要驳回的信息更新申请。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CommentForm frmCommnet = new CommentForm()
                {
                    Text = "驳回",
                    Capition = "驳回原因"
                };
                frmCommnet.TextSumbittedHandler = (text) =>
                {
                    decimal auditingLogId = Convert.ToDecimal(devExpressGrid.DataKeyValues.Value);
                    DataAuditingLogInfo dataAuditingLogInfo = dataAuditingLogContract.GetModelInfo(auditingLogId);
                    CommonNode commonNode = dataAuditingStepContract.GetLastestReviewer(auditingLogId);
                    if (commonNode.NodeId == CurrentUser.Instance.UserId)
                    {
                        AuditingStatus auditingStatus = (AuditingStatus)dataAuditingLogInfo.AuditingStatus;
                        if (auditingStatus == AuditingStatus.None || auditingStatus == AuditingStatus.Completed)
                        {
                            MessageBox.Show(string.Format("不能驳回{0}的信息更新申请。", UserEnumHelper.GetEnumText(auditingStatus)), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (MessageBox.Show("确认驳回该信息更新申请？", "警告信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            Cursor = Cursors.WaitCursor;
                            DataAuditingInfo dataAuditingInfo = dataAuditingContract.GetModelInfoByLogId(auditingLogId);
                            AuditingStatus newAuditingStatus = AuditingStatus.None;
                            switch (auditingStatus)
                            {
                                case AuditingStatus.Audited:
                                    if (dataAuditingInfo.AllocationStatus)
                                    {
                                        newAuditingStatus = AuditingStatus.Allocating;
                                    }
                                    else if (dataAuditingInfo.InitReviewStatus)
                                    {
                                        newAuditingStatus = AuditingStatus.Auditing;
                                    }
                                    else
                                    {
                                        newAuditingStatus = AuditingStatus.None;
                                    }
                                    break;

                                case AuditingStatus.Allocating:
                                    if (dataAuditingInfo.InitReviewStatus)
                                    {
                                        newAuditingStatus = AuditingStatus.Auditing;
                                    }
                                    else
                                    {
                                        newAuditingStatus = AuditingStatus.None;
                                    }
                                    break;

                                case AuditingStatus.Auditing:
                                    newAuditingStatus = AuditingStatus.None;
                                    break;
                            }
                            dataAuditingLogContract.Reject(auditingLogId, commonNode.NodeId, newAuditingStatus, text);
                            devExpressGrid.CurrentPageIndex = 0;
                            LoadData();
                            Cursor = Cursors.Default;
                        }
                    }
                };
                frmCommnet.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 查看日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkView_Click(object sender, EventArgs e)
        {
            if (devExpressGrid.FocusedRowHandle >= 0)
            {
                decimal auditingLogId = Convert.ToDecimal(devExpressGrid.DataKeyValues.Value);
                byte auditingStatus = Convert.ToByte(devExpressGrid.FocusedDataRow["AuditingStatus"]);
                ShowLog(auditingLogId, (AuditingStatus)auditingStatus);
            }
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            devExpressGrid.CurrentPageIndex = 0;
            LoadData();
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkClean_Click(object sender, EventArgs e)
        {
            txtInstanceName.Text = string.Empty;
            deTimeFrom.EditValue = null;
            deDateTimeTo.EditValue = null;
            devExpressGrid.CurrentPageIndex = 0;
            LoadData();
        }
        
        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 自定义显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AuditingLogType")
            {
                AuditingLogType auditingLogType = (AuditingLogType)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(auditingLogType);
            }
            else if (e.Column.FieldName == "AuditingStatus")
            {
                AuditingStatus auditingStatus = (AuditingStatus)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(auditingStatus);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            AuditingStatus auditingStatus = AuditingStatus.None;
            switch (CurrentInfoStatus)
            {
                case InfoStatus.InfoAuditing:
                    auditingStatus = AuditingStatus.Auditing;
                    break;

                case InfoStatus.InfoAllocating:
                    auditingStatus = AuditingStatus.Allocating;
                    break;

                case InfoStatus.InfoAudited:
                    auditingStatus = AuditingStatus.Audited;
                    break;
            }
            whereConditons.Add(new WhereConditon("DataAuditingLog", "AuditingStatus", "AuditingStatus", DbType.Byte, (byte)auditingStatus, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("DataAuditingStep", "UserId", "UserId", DbType.Decimal, CurrentUser.Instance.UserId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            string instanceName = txtInstanceName.Text;
            if (!string.IsNullOrWhiteSpace(instanceName))
            {
                instanceName = Regex.Replace(instanceName, " {1,}", "%");
                whereConditons.Add(new WhereConditon("AuditingLogName", "AuditingLogName", DbType.String,
                    instanceName, DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            DateTime dateTimeFrom = deTimeFrom.DateTime;
            if (!DataConvertionHelper.IsNullValue(dateTimeFrom))
            {
                whereConditons.Add(new WhereConditon("TimeSumbitted", "TimeSumbitted_0", DbType.Date, dateTimeFrom,
                               DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            DateTime dateTimeTo = deDateTimeTo.DateTime;
            if (!DataConvertionHelper.IsNullValue(dateTimeTo))
            {
                if (!DataConvertionHelper.IsNullValue(dateTimeFrom) && dateTimeTo >= dateTimeFrom)
                {
                    MessageBox.Show("提交的开始时间不能大于结束时间。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                whereConditons.Add(new WhereConditon("TimeSumbitted", "TimeSumbitted_1", DbType.Date, dateTimeTo,
                           DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            devExpressGrid.RecordCount = dataAuditingLogContract.GetDataAuditingCount(whereConditons);
            devExpressGrid.DataSource = dataAuditingLogContract.GetDataAuditing(devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, 
                devExpressGrid.PageSize, whereConditons).Tables[0];
            InsertViewColumn();
        }

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
                    lblName.Text = "草稿状态待提交。";
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
        /// 提交到下一步
        /// </summary>
        /// <param name="dataAuditingLogInfo"></param>
        /// <param name="nextReviewerId"></param>
        /// <param name="currentAuditingStatus"></param>
        /// <param name="newAuditingStatus"></param>
        /// <param name="auditingAction"></param>
        /// <param name="comment"></param>
        private void SubmitBusinessToNextStep(DataAuditingLogInfo dataAuditingLogInfo, decimal nextReviewerId, AuditingStatus currentAuditingStatus, AuditingStatus newAuditingStatus,
            AuditingAction auditingAction, string comment)
        {
            AuditingStatus auditingStatus = (AuditingStatus)dataAuditingLogInfo.AuditingStatus;
            CommonNode commonNode = dataAuditingStepContract.GetLastestReviewer(dataAuditingLogInfo.AuditingLogId);
            if (commonNode.NodeId == CurrentUser.Instance.UserId && auditingStatus == currentAuditingStatus)
            {
                dataAuditingLogContract.SubmitBusinessToNextStep(dataAuditingLogInfo.AuditingLogId, newAuditingStatus, CurrentUser.Instance.UserId,
                    nextReviewerId, auditingAction, string.Empty);
            }
            else
            {
                MessageBox.Show("不能处理当前的信息更新申请：（1）信息更新申请已经被处理（2）处理人发生变化。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        /// <summary>
        /// 插入查看列
        /// </summary>
        /// <returns></returns>
        private void InsertViewColumn()
        {
            GridColumn gcView = devExpressGrid.Columns.Add();
            gcView.Caption = "查看";
            gcView.Name = "View";
            gcView.Width = 40;
            gcView.MinWidth = 40;
            gcView.VisibleIndex = 0;
            gcView.Visible = true;
            gcView.Fixed = FixedStyle.Left;
            gcView.OptionsColumn.ReadOnly = true;
            gcView.OptionsFilter.AllowAutoFilter = false;
            gcView.OptionsFilter.AllowFilter = false;
            gcView.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gcView.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gcView.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            gcView.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit = new RepositoryItemHyperLinkEdit();
            repositoryItemHyperLinkEdit.NullText = "查看";
            gcView.ColumnEdit = repositoryItemHyperLinkEdit;
            repositoryItemHyperLinkEdit.Click += (sender, e) =>
            {
                if (devExpressGrid.FocusedRowHandle >= 0)
                {
                    decimal auditingLogId = Convert.ToDecimal(devExpressGrid.DataKeyValues.Value);
                    ShowDataComparisonForm(auditingLogId);
                }
            };
        }

        /// <summary>
        /// 展示数据对比
        /// </summary>
        /// <param name="auditingLogId"></param>
        private void ShowDataComparisonForm(decimal auditingLogId)
        {
            DataAuditingLogInfo dataAuditingLogInfo = dataAuditingLogContract.GetModelInfo(auditingLogId);
            DataAuditingInfo dataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingLogInfo.DataAuditingId);
            DataAuditingInfo parentDataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingInfo.ParentDataAuditingId);
            string tablePhysicalName = string.Empty;
            Dictionary<string, CommonDataFieldInfo> systemDataFieldNameRelations = null;
            IList<CommonNode> dataFields = dataAuditingContract.GetDataFields(dataAuditingInfo.DataAuditingId);
            List<decimal> tableIds = new List<decimal>();
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            FormType formType = (FormType)parentDataAuditingInfo.TableType;
            switch (formType)
            {
                case FormType.Table:
                    tablePhysicalName = customTableContract.GetTablePhysicalName(parentDataAuditingInfo.TableId);
                    tableIds.Add(parentDataAuditingInfo.TableId);
                    break;

                case FormType.CombinedTable:
                    IList<CommonNode> commonNodeInfos = combinedTableContract.GetTables(parentDataAuditingInfo.CombinedTableId);
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
            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = customRoleContract.GetAuthorizedExtendedCustomDataFieldInfos(CurrentUser.Instance.UserId, tableIds, DataAuthorityType.Auditing);
            Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
            systemDataFieldNameRelations = DataFieldHelper.GetSystemDataFieldInfo(tablePhysicalName, dataAuditingInfo.SystemDataFieldAuthority);
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in systemDataFieldNameRelations)
            {
                dataFieldNameRelations.Add(keyValue.Key, keyValue.Value);
            }
            IList<ExtendedCustomDataFieldInfo> customDataFieldInfos = new List<ExtendedCustomDataFieldInfo>();
            foreach (var dataField in dataFields)
            {
                int pos = extendedCustomDataFieldInfos.FindIndex(customDataFieldInfo => dataField.NodeId == customDataFieldInfo.DataFieldId);
                if (pos < 0)
                {
                    continue;
                }
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = extendedCustomDataFieldInfos[pos];
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
                customDataFieldInfos.Add(extendedCustomDataFieldInfo);
                dataFieldNameRelations.Add(extendedCustomDataFieldInfo.PhysicalName,
                        new CommonDataFieldInfo(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.TableId, extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.LogicalName,
                        expressionText, dataFieldProperty, extendedCustomDataFieldInfo.DataFieldType));
            }
            //foreach (var extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
            //{
            //    if (dataFields.FindIndex(dataField => dataField.NodeId == extendedCustomDataFieldInfo.DataFieldId) < 0)
            //    {
            //        continue;
            //    }
            //    DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
            //    string expressionText = string.Empty;
            //    if (dataFieldProperty == DataFieldProperty.LogicalDataField)
            //    {
            //        LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
            //        if (logicalDataFieldType == LogicalDataFieldType.DigitExpression || logicalDataFieldType == LogicalDataFieldType.StringExpression
            //            || logicalDataFieldType == LogicalDataFieldType.DateTimeExpression)
            //        {
            //            expressionText = customDataFieldContract.GetDataFieldLogicalExpression(extendedCustomDataFieldInfo.DataFieldId);
            //        }
            //    }
            //    customDataFieldInfos.Add(extendedCustomDataFieldInfo);
            //    dataFieldNameRelations.Add(extendedCustomDataFieldInfo.PhysicalName,
            //            new CommonDataFieldInfo(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.TableId, extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.LogicalName,
            //            expressionText, dataFieldProperty, extendedCustomDataFieldInfo.DataFieldType));
            //}
            DataComparisonForm frmDataComparison = new DataComparisonForm();
            AuditingLogType auditingLogType = (AuditingLogType)dataAuditingLogInfo.AuditingLogType;            
            frmDataComparison.UserId = dataAuditingLogInfo.UserId;
            frmDataComparison.CreateControls(customDataFieldInfos);
            switch (formType)
            {
                case FormType.Table:
                    if (auditingLogType == AuditingLogType.Add)
                    {
                        frmDataComparison.LeftText = "新增数据";
                        frmDataComparison.RightText = "无";
                    }
                    DataTable dataTable = customTableContract.GetMirrorRowData(parentDataAuditingInfo.TableId, dataAuditingInfo.SystemDataFieldAuthority, dataFieldNameRelations, auditingLogId, false);
                    frmDataComparison.LoadTableData(dataTable);
                    break;

                case FormType.CombinedTable:
                    if (auditingLogType == AuditingLogType.Add)
                    {
                        frmDataComparison.LeftText = "无";
                        frmDataComparison.RightText = "新增数据";
                    }
                    Dictionary<decimal, DataRowItem> dataRowItems = combinedTableContract.GetMirrorRowData(parentDataAuditingInfo.CombinedTableId, dataFieldNameRelations, auditingLogId, false);
                    frmDataComparison.LoadCombinedTableData(dataRowItems);
                    break;
            }
            frmDataComparison.ShowDialog();
        }

        #endregion

    }
}
