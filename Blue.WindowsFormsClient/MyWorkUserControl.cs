using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraNavBar;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient.MyBusinessModule;
using Blue.WindowsFormsClient.MyWorkModule;

namespace Blue.WindowsFormsClient
{
    public partial class MyWorkUserControl : UserControl
    {
        #region 契约接口

        private ICustomWorkflowContract customWorkflowContract;
        private ICustomWorkflowInstanceContract customWorkflowInstanceContract;
        private IWorkflowInstanceStepContract workflowInstanceStepContract;
        private ICustomWorkflowProcessContract customWorkflowProcessContract;
        private ICustomDataContract customDataContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MyWorkUserControl()
        {
            InitializeComponent();
            customWorkflowContract = BusinessChannelFactory.CreateCustomWorkflowContract();
            customWorkflowInstanceContract = BusinessChannelFactory.CreateCustomWorkflowInstanceContract();
            workflowInstanceStepContract = BusinessChannelFactory.CreateWorkflowInstanceStepContract();
            customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();       
        }

        #endregion

        #region 控件加载方法

        /// <summary>
        /// 控件加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MyWorkUserControl_Load(object sender, EventArgs e)
        {
            xtcWorkflow.SelectedTabPageIndex = 0;
            devWorkflow.Tag = false;
            SetControlsVisible(true);
            LoadData(false);
        }

        /// <summary>
        /// 正在处理的工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mbiProcessing_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            lblTimeSumbitted.Text = "提交时间：";
            gcWorkflow.Text = "待处理工作流";
            xtcWorkflow.SelectedTabPageIndex = 0;
            devWorkflow.Tag = false;
            SetControlsVisible(true);
            LoadData(false);
        }

        /// <summary>
        /// 已经处理的工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mbiProcessed_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            lblTimeSumbitted.Text = "审核时间：";
            gcWorkflow.Text = "已处理工作流";
            xtcWorkflow.SelectedTabPageIndex = 0;
            devWorkflow.Tag = true;
            SetControlsVisible(false);
            LoadData(true);
        }

        /// <summary>
        /// 工作流统计数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiWorkflowStatistics_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            xtcWorkflow.SelectedTabPageIndex = 1;
            devWorkflow.Tag = true;
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadData(Convert.ToBoolean(devWorkflow.Tag));
        }

        /// <summary>
        /// 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkClear_Click(object sender, EventArgs e)
        {
            txtInstanceName.Text = string.Empty;
            dtStart.EditValue = null;
            dtEnd.EditValue = null;
            LoadData(Convert.ToBoolean(devWorkflow.Tag));
        }

        /// <summary>
        /// 查看工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal stepId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                decimal instanceId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[1]);
                if (instanceId > 0)
                {
                    ShowWorkflowForm(instanceId, stepId, true);
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
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal stepId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                WorkflowInstanceStepInfo workflowInstanceStepInfo = workflowInstanceStepContract.GetModelInfo(stepId);
                if (customWorkflowInstanceContract.GetInstanceArchivedStatus(workflowInstanceStepInfo.InstanceId))
                {
                    MessageBox.Show("该工作流实例已归档，无法撤回。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认撤回该业务？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    
                    string instanceName = DataConvertionHelper.GetString(devWorkflow.GetRowCellValue(devWorkflow.FocusedRowHandle, "InstanceName"));
                    ReviewedCommentForm frmReviewedComment = new ReviewedCommentForm()
                    {
                        Text = instanceName,
                        CommnetName = "撤回审核意见",
                        GetComment = (comment) =>
                        {                            
                            WorkflowInstanceLogInfo workflowInstanceLogInfo = new WorkflowInstanceLogInfo()
                            {
                                ProcessId = workflowInstanceStepInfo.ProcessId,
                                InstanceId = workflowInstanceStepInfo.InstanceId,
                                UserId = decimal.MinValue,
                                ParentUserId = CurrentUser.Instance.UserId,
                                ReviewedAction = (byte)ReviewedAction.WithDraw,
                                Comment = comment,
                                TimeReviewed = DateTime.Now
                            };
                            WithdrawedResult withdrawedResult = customWorkflowInstanceContract.WithdrawWorkflowInstance(stepId, workflowInstanceLogInfo);
                            LoadData(true);
                            if (withdrawedResult == WithdrawedResult.Success)
                            {
                                MessageBox.Show("撤回成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(string.Format("撤回失败，原因：{0}。", UserEnumHelper.GetEnumText(withdrawedResult)), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    };
                    frmReviewedComment.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 终止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal stepId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                long processSetting = customWorkflowProcessContract.GetProcessSetting(stepId);
                if (!AuthorityHelper.CheckAuthority(processSetting, (byte)WorkflowProcessSetting.AbortAllowed))
                {
                    MessageBox.Show("该业务流程不允许终止。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认终止该业务？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    string instanceName = DataConvertionHelper.GetString(devWorkflow.GetRowCellValue(devWorkflow.FocusedRowHandle, "InstanceName"));
                    ReviewedCommentForm frmReviewedComment = new ReviewedCommentForm()
                    {
                        Text = instanceName,
                        CommnetName = "终止审核意见",
                        GetComment = (comment) =>
                        {
                            AbortedResult abortedResult = customWorkflowInstanceContract.AbortWorkflowInstance(stepId, comment);
                            LoadData(false);
                            if (abortedResult == AbortedResult.Success)
                            {
                                MessageBox.Show("终止成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(string.Format("终止失败，原因：{0}。", UserEnumHelper.GetEnumText(abortedResult)), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    };
                }
            }
        }

        /// <summary>
        /// 分页处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devWorkflow_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            devWorkflow.CurrentPageIndex = e.NewPageIndex;
            bool audited = Convert.ToBoolean(devWorkflow.Tag);
            if (audited)
            {
                LoadData(false);
            }
            else
            {
                LoadData(true);
            }
        }

        /// <summary>
        /// 查看流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkView_Click(object sender, EventArgs e)
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[1]);
                DataSet ds = customWorkflowInstanceContract.GetPageRecord(instanceId);
                gcSteps.DataSource = ds.Tables[0];
                gvSteps.Columns["LogId"].Visible = false;
                gvSteps.Columns["TimeReviewed"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gvSteps.Columns["TimeReviewed"].DisplayFormat.FormatString = "f";
                gvSteps.BestFitColumns();
                gvSteps.Columns["Comment"].MinWidth = 150;
                CustomWorkflowInstanceInfo customWorkflowInstanceInfo = customWorkflowInstanceContract.GetModelInfo(instanceId);
                InstanceStatus instanceStatus = (InstanceStatus)customWorkflowInstanceInfo.InstanceStatus;
                switch (instanceStatus)
                {
                    case InstanceStatus.None:
                        lblName.Text = "草稿状态待提交。";
                        lblReviewer.Visible = false;
                        break;

                    case InstanceStatus.Review:
                        Dictionary<decimal, string> lastestReviewers = customWorkflowInstanceContract.GetLastestReviewers(customWorkflowInstanceInfo.InstanceId);
                        StringBuilder sb = new StringBuilder();
                        foreach (var lastestReviewer in lastestReviewers)
                        {
                            sb.AppendFormat("{0}, ", lastestReviewer.Value);
                        }
                        if (sb.Length > 0)
                        {
                            sb.Remove(sb.Length - 2, 2);
                            sb.AppendFormat("。");
                        }
                        lblReviewer.Text = sb.ToString();
                        lblName.Text = "下一步审核人：";
                        lblReviewer.Visible = true;
                        break;

                    case InstanceStatus.Completed:
                        lblName.Text = "审核已完成。";
                        lblReviewer.Visible = false;
                        break;

                    case InstanceStatus.ReviewAborted:
                        lblName.Text = "审核时终止。";
                        lblReviewer.Visible = false;
                        break;
                }

                if (fpSteps.FlyoutPanelState.IsActive)
                {
                    return;
                }
                fpSteps.ShowBeakForm();
            }
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAudit_Click(object sender, EventArgs e)
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal stepId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                decimal instanceId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[1]);
                if (instanceId > 0)
                {
                    ShowWorkflowForm(instanceId, stepId, false);
                }
            }
        }

        /// <summary>
        /// 自定义枚举字段显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devWorkflow_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "InstanceStatus")
            {
                InstanceStatus instanceState = (InstanceStatus)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(instanceState);
            }
        }

        /// <summary>
        /// 自定义动作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSteps_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ReviewedAction")
            {
                ReviewedAction reviewedAction = (ReviewedAction)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(reviewedAction);
            }
        }

        /// <summary>
        /// 选择行发生了变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devWorkflow_OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SetAbortButtonState();
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvSteps_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 显示工作流窗体
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="stepId"></param>
        /// <param name="readOnly"></param>
        private void ShowWorkflowForm(decimal instanceId, decimal stepId, bool readOnly)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CustomWorkflowInfo customWorkflowInfo = customWorkflowContract.GetCustomWorkflowInfo(instanceId);
                CustomDataInfo customDataInfo = customDataContract.GetModelInfo(customWorkflowInfo.DataId);
                CustomWorkflowInstanceInfo customWorkflowInstanceInfo = customWorkflowInstanceContract.GetModelInfo(instanceId);
                WorkflowInstanceForm frmWorkflowInstance = new WorkflowInstanceForm()
                {
                    ParentUserId = customWorkflowInstanceInfo.ParentUserId,
                    Text = customWorkflowInfo.WorkflowName,
                    CustomWorkflowInfo = customWorkflowInfo,
                    CustomDataInfo = customDataInfo,
                    FormReadOnly = readOnly,
                    InstanceId = instanceId,
                    StepId = stepId
                };
                frmWorkflowInstance.CloseForm = () =>
                {
                    LoadData(Convert.ToBoolean(devWorkflow.Tag));
                };
                Cursor = Cursors.Default;
                frmWorkflowInstance.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 加载已审核或未审核数据
        /// </summary>
        /// <param name="audited"></param>
        private void LoadData(bool audited)
        {
            int totalCount = 0;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            string condition = txtInstanceName.Text.Trim();
            /* 实例名称查询 */
            if (!string.IsNullOrWhiteSpace(condition))
            {
                whereConditons.Add(new WhereConditon("InstanceName", "InstanceName", DbType.String, string.Format("%{0}%", condition),
                   DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("UserName", "UserName", DbType.String, string.Format("%{0}%", condition),
                  DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("UserActualName", "UserActualName", DbType.String, string.Format("%{0}%", condition),
                  DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            DateTime start = dtStart.DateTime;
            DateTime end = dtEnd.DateTime;
           
            whereConditons.Add(new WhereConditon("WorkflowInstanceStep", "UserId", "UserId", DbType.Decimal, CurrentUser.Instance.UserId,
                                 DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));           
            if (audited)
            {
                /* 已审核的工作流 */
                if (!DataConvertionHelper.IsNullValue(start))
                {
                    whereConditons.Add(new WhereConditon("TimeReviewed", "TimeReviewed_0", DbType.DateTime, start,
                      DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                if (!DataConvertionHelper.IsNullValue(end))
                {
                    whereConditons.Add(new WhereConditon("TimeReviewed", "TimeReviewed_1", DbType.DateTime, end,
                      DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                whereConditons.Add(new WhereConditon("ReviewedStatus", "ReviewedStatus", DbType.Byte, (byte)ReviewedStatus.Completed,
                                    DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                devWorkflow.DataSource = workflowInstanceStepContract.GetWorkflowInstanceAudited(devWorkflow.PageSize * devWorkflow.CurrentPageIndex,
                        devWorkflow.PageSize, whereConditons, ref totalCount).Tables[0];
            }
            else
            {
                /* 未审核的数据填报 */
                if (!DataConvertionHelper.IsNullValue(start))
                {
                    whereConditons.Add(new WhereConditon("TimeSumbitted", "TimeSumbitted_0", System.Data.DbType.DateTime, start,
                      DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                if (!DataConvertionHelper.IsNullValue(end))
                {
                    whereConditons.Add(new WhereConditon("TimeSumbitted", "TimeSumbitted_1", System.Data.DbType.DateTime, end,
                      DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                whereConditons.Add(new WhereConditon("ReviewedStatus", "ReviewedStatus", DbType.Byte, (byte)ReviewedStatus.Reviewing,
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                devWorkflow.DataSource = workflowInstanceStepContract.GetWorkflowInstanceUnaudited(devWorkflow.PageSize * devWorkflow.CurrentPageIndex,
                    devWorkflow.PageSize, whereConditons, ref totalCount).Tables[0];
            }            
            devWorkflow.RecordCount = totalCount;
            devWorkflow.DevExpressGridView.Columns["InstanceName"].MinWidth = 200;
            if (audited)
            {
                devWorkflow.Columns["TimeReviewed"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                devWorkflow.Columns["TimeReviewed"].DisplayFormat.FormatString = "f";                
            }
            else
            {
                devWorkflow.Columns["TimeSumbitted"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                devWorkflow.Columns["TimeSumbitted"].DisplayFormat.FormatString = "f";
            }
            if (devWorkflow.RowCount > 0)
            {
                devWorkflow.FocusedRowHandle = 0;
                btnAudit.Enabled = true;                
                btnWithdraw.Enabled = true;
                btnView.Enabled = true;
                hlnkView.Enabled = true;
                SetAbortButtonState();
            }
            else
            {
                btnAudit.Enabled = false;
                btnAbort.Enabled = false;
                btnWithdraw.Enabled = false;
                btnView.Enabled = false;
                hlnkView.Enabled = false;
            }
        }

        /// <summary>
        /// 根据已审核或未审核的状态显示不同的控件
        /// </summary>
        /// <param name="visible"></param>
        private void SetControlsVisible(bool visible)
        {
            btnAudit.Visible = visible;
            btnAbort.Visible = visible;
            btnWithdraw.Visible = !visible;            
        }

        /// <summary>
        /// 设置终止按钮状态
        /// </summary>
        private void SetAbortButtonState()
        {
            if (devWorkflow.FocusedRowHandle >= 0)
            {
                decimal stepId = Convert.ToDecimal(devWorkflow.DataKeyValues.Values[0]);
                if (stepId > 0)
                {
                    long processSetting = customWorkflowProcessContract.GetProcessSetting(stepId);
                    btnAbort.Enabled = AuthorityHelper.CheckAuthority(processSetting, (byte)WorkflowProcessSetting.AbortAllowed);
                }
            }
        }


        #endregion        
    }
}
