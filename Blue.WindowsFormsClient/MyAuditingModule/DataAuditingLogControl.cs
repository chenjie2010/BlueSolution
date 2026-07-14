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
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.MyAuditingModule
{
    public partial class DataAuditingLogControl : UserControl
    {
        #region 契约接口

        private readonly IDataAuditingContract dataAuditingContract;
        private readonly IDataAuditingStepContract dataAuditingStepContract;
        private readonly IDataAuditingLogContract dataAuditingLogContract;

        #endregion

        #region 属性
        

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataAuditingLogControl()
        {
            InitializeComponent();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();
            dataAuditingStepContract = BusinessDesignerChannelFactory.CreateDataAuditingStepContract();
            dataAuditingLogContract = BusinessDesignerChannelFactory.CreateDataAuditingLogContract();
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(AuditingAction));
            enumItems.RemoveAt(0);
            enumItems.RemoveAt(0);
            ccmbAuditingAction.Properties.Items.AddRange(enumItems.ToArray());
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonalDataAuditingControl_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 行选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnRowClick(object sender, RowEvent e)
        {
            if (devExpressGrid.FocusedRowHandle >= 0)
            {
                decimal auditingLogId = Convert.ToDecimal(devExpressGrid.DataKeyValues.Value);
                DataAuditingLogInfo dataAuditingLogInfo = dataAuditingLogContract.GetModelInfo(auditingLogId);
                CommonNode commonNode = dataAuditingStepContract.GetLastestSubmitter(auditingLogId);
                if (commonNode.NodeId == CurrentUser.Instance.UserId)
                {
                    AuditingStatus auditingStatus = (AuditingStatus)dataAuditingLogInfo.AuditingStatus;
                    if (auditingStatus == AuditingStatus.None || auditingStatus == AuditingStatus.Completed)
                    {
                        btnWithdraw.Enabled = false;
                    }
                    else
                    {
                        btnWithdraw.Enabled = true;
                    }
                }
            }
        }

        /// <summary>
        /// 撤回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkBluk.Checked)
                {
                    if (devExpressGrid.MultiSelectedValues.Count == 0)
                    {
                        MessageBox.Show("请先选择需要撤回的信息更新申请。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (MessageBox.Show("确认批量撤回信息更新申请？", "警告信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;
                        progressPanel.Text = "批量撤回信息更新申请正在处理中...";
                        progressPanel.Show();
                        int count = 0;
                        foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                        {
                            decimal auditingLogId = Convert.ToDecimal(rowEvent.Value);
                            DataAuditingLogInfo dataAuditingLogInfo = dataAuditingLogContract.GetModelInfo(auditingLogId);
                            CommonNode commonNode = dataAuditingStepContract.GetLastestSubmitter(auditingLogId);
                            if (commonNode.NodeId == CurrentUser.Instance.UserId)
                            {
                                AuditingStatus auditingStatus = (AuditingStatus)dataAuditingLogInfo.AuditingStatus;
                                if (auditingStatus == AuditingStatus.None || auditingStatus == AuditingStatus.Auditing || auditingStatus == AuditingStatus.Completed)
                                {
                                    continue;
                                }
                                DataAuditingInfo dataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingLogInfo.DataAuditingId);
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
                                        break;

                                    case AuditingStatus.Allocating:
                                        if (dataAuditingInfo.InitReviewStatus)
                                        {
                                            newAuditingStatus = AuditingStatus.Auditing;
                                        }
                                        break;
                                }
                                dataAuditingLogContract.WithDraw(auditingLogId, commonNode.NodeId, newAuditingStatus);
                                count++;
                            }
                        }
                        devExpressGrid.CurrentPageIndex = 0;
                        LoadData();
                        progressPanel.Hide();
                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("批量选择信息更新申请共{0}个，成功撤回{1}个。", devExpressGrid.MultiSelectedValues.Count, count), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (devExpressGrid.FocusedRowHandle >= 0)
                    {
                        decimal auditingLogId = Convert.ToDecimal(devExpressGrid.DataKeyValues.Value);
                        DataAuditingLogInfo dataAuditingLogInfo = dataAuditingLogContract.GetModelInfo(auditingLogId);
                        CommonNode commonNode = dataAuditingStepContract.GetLastestSubmitter(auditingLogId);
                        if (commonNode.NodeId == CurrentUser.Instance.UserId)
                        {
                            AuditingStatus auditingStatus = (AuditingStatus)dataAuditingLogInfo.AuditingStatus;
                            if (auditingStatus == AuditingStatus.None || auditingStatus == AuditingStatus.Auditing || auditingStatus == AuditingStatus.Completed)
                            {
                                MessageBox.Show(string.Format("不能撤回{0}的信息更新申请。", UserEnumHelper.GetEnumText(auditingStatus)), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (MessageBox.Show("确认撤回该信息更新申请？", "警告信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                            {
                                Cursor = Cursors.WaitCursor;
                                DataAuditingInfo dataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingLogInfo.DataAuditingId);
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
                                        break;

                                    case AuditingStatus.Allocating:
                                        if (dataAuditingInfo.InitReviewStatus)
                                        {
                                            newAuditingStatus = AuditingStatus.Auditing;
                                        }
                                        break;
                                }
                                dataAuditingLogContract.WithDraw(auditingLogId, commonNode.NodeId, newAuditingStatus);
                                devExpressGrid.CurrentPageIndex = 0;
                                LoadData();
                                Cursor = Cursors.Default;
                            }
                        }
                    }
                }
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
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbAuditingAction);
            deTimeFrom.EditValue = null;
            deDateTimeTo.EditValue = null;
            devExpressGrid.CurrentPageIndex = 0;
            LoadData();
        }

        /// <summary>
        /// 关闭流程窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSteps_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            fpSteps.HideBeakForm();
        }

        /// <summary>
        /// 日志状态审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AuditingAction")
            {
                AuditingAction auditingAction = (AuditingAction)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(auditingAction);
            }
            else if (e.Column.FieldName == "AuditingStatus")
            {
                AuditingStatus auditingStatus = (AuditingStatus)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(auditingStatus);
            }
        }

        /// <summary>
        /// 审核动作
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

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            UserControlHelper.GetWhereConditons(ccmbAuditingAction, whereConditons, "DataAuditingStep", "AuditingAction");
            if (whereConditons.Count == 0)
            {
                whereConditons.Add(new WhereConditon("AuditingAction", "AuditingAction_0", DbType.Byte, (byte)AuditingAction.None, DataFieldCondition.Not, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("AuditingAction", "AuditingAction_1", DbType.Byte, (byte)AuditingAction.Sumbitted, DataFieldCondition.Not, DataFieldInnerRealtion.And));
            }
            whereConditons.Add(new WhereConditon("DataAuditingStep", "ParentUserId", "ParentUserId", DbType.Decimal, CurrentUser.Instance.UserId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            string instanceName = txtInstanceName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(instanceName))
            {
                instanceName = string.Format("%{0}%", instanceName);
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

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("AuditingTime", CustomSorting.Descending));
            int totalCount = 0;
            DataTable dataTable = dataAuditingStepContract.GetDataAuditingSteps(devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex,
                devExpressGrid.PageSize, whereConditons, sortingCondtions, ref totalCount).Tables[0];
            devExpressGrid.DataSource = dataTable;
            devExpressGrid.RecordCount = totalCount;
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

        #endregion        
        
    }
}
