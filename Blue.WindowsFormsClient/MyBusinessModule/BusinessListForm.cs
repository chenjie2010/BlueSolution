using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsControls;
using Blue.Model.BusinessModule;
using Blue.Model.DataFilledModule;
using Blue.WindowsFormsClient.Common;
using Blue.WCFContracts.DataFilledModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WindowsFormsClient.MyWorkModule;

namespace Blue.WindowsFormsClient.MyBusinessModule
{
    public partial class BusinessListForm : Form
    {
        #region 契约接口

        private readonly ICustomDataContract customDataContract;
        private readonly ICustomWorkflowInstanceContract customWorkflowInstanceContract;
        private readonly ICustomWorkflowContract customWorkflowContract;
        private readonly IUserAccountContract userAccountContract;
        private readonly IWorkflowInstanceStepContract workflowInstanceStepContract;
        private readonly ICustomWorkflowProcessContract customWorkflowProcessContract;
        #endregion

        #region 属性

        /// <summary>
        /// 业务对象
        /// </summary>
        public ExtendedCustomBusinessInfo ExtendedCustomBusinessInfo
        {
            get;
            set;
        }


        /// <summary>
        /// 工作流对象
        /// </summary>
        public CustomWorkflowInfo CustomWorkflowInfo
        {
            get;
            set;
        }

        public bool AllLoaded
        {
            get;
            set;
        }

        public InstanceStatus InstanceStatus
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BusinessListForm()
        {
            InitializeComponent();
            customWorkflowInstanceContract = BusinessChannelFactory.CreateCustomWorkflowInstanceContract();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            customWorkflowContract = BusinessChannelFactory.CreateCustomWorkflowContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
            workflowInstanceStepContract = BusinessChannelFactory.CreateWorkflowInstanceStepContract();
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbInstanceState, typeof(InstanceStatus));
            AllLoaded = false;
            InstanceStatus = InstanceStatus.None;            
        }

        #endregion

        #region 窗体和控件加载方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BusinessListForm_Load(object sender, EventArgs e)
        {
            WorkflowType workflowType = (WorkflowType)CustomWorkflowInfo.WorkflowType;
            if (workflowType == WorkflowType.Single)
            {
                lblInstanceState.Visible = false;
                ccmbInstanceState.Visible = false;
                txtInstanceName.Width = 480;
            }
            if (!AllLoaded)
            {
                Int64 authority = 1L;
                if (InstanceStatus != InstanceStatus.None)
                {
                    authority = authority << (byte)InstanceStatus;
                }
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbInstanceState, authority);
            }
            LoadData();

        }
        
        /// <summary>
        /// 关闭上一步审核意见提示框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpComment_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            this.fpComment.HideBeakForm();
        }

        /// <summary>
        /// 显示最新的审核意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdWorkflowInstance_OnRowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName.Equals("InstanceStatus"))
            {
                decimal instanceId = Convert.ToDecimal(grdWorkflowInstance.DataKeyValues.Value);
                string lastestComment = customWorkflowInstanceContract.GetLastestComment(instanceId);
                if(string.IsNullOrWhiteSpace(lastestComment))
                {
                    lblLastestComment.Text = "无审核意见";
                }
                else
                {
                    lblLastestComment.Text = "上一步审核意见：";
                }
                meLastestComment.Text = lastestComment;
                if (fpComment.FlyoutPanelState.IsActive)
                {
                    return;
                }
                fpComment.ShowBeakForm(grdWorkflowInstance.DevExpressGridControl.PointToScreen(new Point(e.X, e.Y)));
            }
        }

        /// <summary>
        /// 执行动作的文本表示
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
            else if (e.Column.FieldName == "ReviewedStatus")
            {
                ReviewedStatus reviewedStatus = (ReviewedStatus)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(reviewedStatus);
            }            
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 插入列
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        private GridColumn InsertColumn(string caption, int width)
        {
            GridColumn gcEdit = new GridColumn();
            gcEdit.Caption = caption;
            gcEdit.Width = width;
            gcEdit.MinWidth = width;
            gcEdit.Visible = true;
            gcEdit.Fixed = FixedStyle.Right;
            gcEdit.OptionsColumn.ReadOnly = true;
            gcEdit.OptionsFilter.AllowAutoFilter = false;
            gcEdit.OptionsFilter.AllowFilter = false;
            gcEdit.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            gcEdit.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit = new RepositoryItemHyperLinkEdit();
            repositoryItemHyperLinkEdit.NullText = caption;
            gcEdit.ColumnEdit = repositoryItemHyperLinkEdit;
            grdWorkflowInstance.DevExpressGridControl.RepositoryItems.Add(repositoryItemHyperLinkEdit);
            repositoryItemHyperLinkEdit.Click += (sender, e) =>
             {
                 if (grdWorkflowInstance.FocusedDataRow == null) return;
                 decimal instanceId = Convert.ToDecimal(grdWorkflowInstance.DataKeyValues.Value);
                 CustomWorkflowInstanceInfo customWorkflowInstanceInfo = customWorkflowInstanceContract.GetModelInfo(instanceId);                 
             };

            return gcEdit;
        }

        /// <summary>
        /// 显示工作流窗体
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="readOnly"></param>
        private void ShowWorlflowForm(decimal instanceId, bool readOnly)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CustomDataInfo customDataInfo = customDataContract.GetModelInfo(CustomWorkflowInfo.DataId);
                WorkflowInstanceForm frmDataTemplateTab = new WorkflowInstanceForm()
                {
                    ParentUserId = CurrentUser.Instance.UserId,
                    Text = ExtendedCustomBusinessInfo.BusinessName,
                    CustomWorkflowInfo = CustomWorkflowInfo,
                    CustomDataInfo = customDataInfo,
                    FormReadOnly = readOnly,
                    InstanceId = instanceId
                };
                frmDataTemplateTab.CloseForm = () =>
                {
                    LoadData();
                };
                Cursor = Cursors.Default;
                frmDataTemplateTab.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }            
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadData();
        }       

        /// <summary>
        /// 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkClean_Click(object sender, EventArgs e)
        {
            txtInstanceName.Text = string.Empty;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbInstanceState);
            deTimeFrom.EditValue = null;
            deDateTimeTo.EditValue = null;
            grdWorkflowInstance.CurrentPageIndex = 0;
            LoadData();
        }                
      
        /// <summary>
        /// 设置不再加载全部数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ccmbInstanceState_EditValueChanged(object sender, EventArgs e)
        {
            AllLoaded = false;
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdWorkflowInstance_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            grdWorkflowInstance.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 行样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdWorkflowInstance_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            InstanceStatus instanceStatus = (InstanceStatus)Convert.ToByte(grdWorkflowInstance.GetRowCellValue(e.RowHandle, "InstanceStatus"));
            switch (instanceStatus)
            {
                case InstanceStatus.Review:
                    e.Appearance.ForeColor = Color.LightGray;
                    break;

                case InstanceStatus.ReviewAborted:
                    e.Appearance.ForeColor = Color.LightBlue;
                    break;

                case InstanceStatus.Completed:
                    e.Appearance.ForeColor = Color.DarkGray;
                    break;
            }
        }

        /// <summary>
        /// 行选择发生变化时对操作按钮（编辑，删除，撤回）进行设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdWorkflowInstance_OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                SetButtonsStates();
            }
        }

        /// <summary>
        /// 查看工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (grdWorkflowInstance.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(grdWorkflowInstance.DataKeyValues.Value);
                if (instanceId > 0)
                {
                    ShowWorlflowForm(instanceId, true);
                }
            }  
        }

        /// <summary>
        /// 编辑工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            decimal instanceId = Convert.ToDecimal(grdWorkflowInstance.DataKeyValues.Value);
            CustomWorkflowInstanceInfo customWorkflowInstanceInfo = customWorkflowInstanceContract.GetModelInfo(instanceId);
            if (customWorkflowInstanceInfo != null)
            {
                InstanceStatus instanceStatus = (InstanceStatus)customWorkflowInstanceInfo.InstanceStatus;
                if (instanceStatus != InstanceStatus.None)
                {
                    LoadData();
                    MessageBox.Show("工作流状态发生变化，无法编辑。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    ShowWorlflowForm(instanceId, false);
                }
            }
        }

        /// <summary>
        /// 删除工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {                
                decimal instanceId = Convert.ToDecimal(grdWorkflowInstance.DataKeyValues.Value);
                CustomWorkflowInstanceInfo customWorkflowInstanceInfo = customWorkflowInstanceContract.GetModelInfo(instanceId);
                if (customWorkflowInstanceInfo != null)
                {
                    InstanceStatus instanceStatus = (InstanceStatus)customWorkflowInstanceInfo.InstanceStatus;
                    if (instanceStatus != InstanceStatus.None)
                    {
                        LoadData();
                        MessageBox.Show("工作流状态发生变化，无法删除。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        /* 删除 */
                        if (MessageBox.Show("确认删除该工作流？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            Cursor = Cursors.WaitCursor;
                            customWorkflowInstanceContract.Delete(instanceId);
                            LoadData();
                            Cursor = Cursors.Default;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 撤回提交的工作流
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认撤回该工作流？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                decimal instanceId = Convert.ToDecimal(grdWorkflowInstance.DataKeyValues.Value);
                string instanceName = DataConvertionHelper.GetString(grdWorkflowInstance.GetRowCellValue(grdWorkflowInstance.FocusedRowHandle, "InstanceName"));
                ReviewedCommentForm frmReviewedComment = new ReviewedCommentForm()
                {
                    Text = instanceName,
                    CommnetName = "撤回审核意见",
                    GetComment = (comment) =>
                    {
                        CommonNode commonNode = customWorkflowProcessContract.GetWorkflowRootNode(CustomWorkflowInfo.WorkflowId);
                        WorkflowInstanceLogInfo workflowInstanceLogInfo = new WorkflowInstanceLogInfo()
                        {
                            ProcessId = commonNode.NodeId,
                            InstanceId = instanceId,
                            UserId = decimal.MinValue,
                            ParentUserId = CurrentUser.Instance.UserId,
                            ReviewedAction = (byte)ReviewedAction.WithDraw,
                            Comment = comment,
                            TimeReviewed = DateTime.Now
                        };
                        WithdrawedResult withdrawedResult = customWorkflowInstanceContract.UserWithdrawWorkflowInstance(instanceId, workflowInstanceLogInfo);
                        LoadData();
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

        /// <summary>
        /// 查看工作流的审核流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkView_Click(object sender, EventArgs e)
        {
            if (grdWorkflowInstance.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(grdWorkflowInstance.DataKeyValues.Value);
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
                        lblName.Text = "审核终止。";
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
        /// 隐藏处理流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSteps_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            fpSteps.HideBeakForm();
        }

        /// <summary>
        /// 添加行号
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

        #region 公共方法

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData()
        {
            Application.DoEvents();
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("IsArchived", "IsArchived", DbType.Boolean, false, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("WorkflowId", "WorkflowId", DbType.Decimal, CustomWorkflowInfo.WorkflowId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, CurrentUser.Instance.UserId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            string instanceName = txtInstanceName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(instanceName))
            {
                instanceName = string.Format("%{0}%", instanceName);
                whereConditons.Add(new WhereConditon("InstanceName", "UserName", DbType.String,
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
            DataFilledType dataFilledType = (DataFilledType)CustomWorkflowInfo.WorkflowType;
            if (dataFilledType == DataFilledType.Common)
            {
                Int64 instanceStateValue = UserControlHelper.GetCheckedComboBoxEditItems(ccmbInstanceState);
                IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(InstanceState));
                IList<InstanceStatus> instanceStatus = new List<InstanceStatus>();
                foreach (EnumItem enumItem in enumItems)
                {
                    bool result = AuthorityHelper.CheckAuthority(instanceStateValue, enumItem.Value);
                    if (result)
                    {
                        instanceStatus.Add((InstanceStatus)enumItem.Value);
                    }
                }
                for (int idx = 0; idx < instanceStatus.Count; idx++)
                {
                    if (idx == 0)
                    {
                        if (instanceStatus.Count == 1)
                        {
                            whereConditons.Add(new WhereConditon("InstanceStatus", string.Format("InstanceStatus_{0}", idx), DbType.Byte, (byte)instanceStatus[idx],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                        }
                        else
                        {
                            whereConditons.Add(new WhereConditon("InstanceStatus", string.Format("InstanceStatus_{0}", idx), DbType.Byte, (byte)instanceStatus[idx],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                        }
                    }
                    else if (idx == instanceStatus.Count - 1)
                    {
                        whereConditons.Add(new WhereConditon("InstanceStatus", string.Format("InstanceStatus_{0}", idx), DbType.Byte, (byte)instanceStatus[idx],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    }
                    else
                    {
                        whereConditons.Add(new WhereConditon("InstanceStatus", string.Format("InstanceStatus_{0}", idx), DbType.Byte, (byte)instanceStatus[idx],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    }
                }
            }
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("TimeSumbitted", CustomSorting.Descending));

            int totalCount = 0;
            totalCount = 0;
            DataTable dt = customWorkflowInstanceContract.GetPageRecord(grdWorkflowInstance.PageSize * grdWorkflowInstance.CurrentPageIndex,
                grdWorkflowInstance.PageSize, whereConditons, sortingCondtions, ref totalCount).Tables[0];
            grdWorkflowInstance.DataSource = dt;
            grdWorkflowInstance.RecordCount = totalCount;
            //grdWorkflowInstance.DevExpressGridView.BestFitColumns();            
            grdWorkflowInstance.Columns["TimeModified"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grdWorkflowInstance.Columns["TimeSumbitted"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;           
            grdWorkflowInstance.Columns["TimeModified"].DisplayFormat.FormatString = "f";
            grdWorkflowInstance.Columns["TimeSumbitted"].DisplayFormat.FormatString = "f";

            IList<EnumItem> instanceStatuses = UserEnumHelper.GetEnumItems(typeof(InstanceStatus));
            RepositoryItemImageComboBox repositoryItemImageComboBox = UserControlHelper.GetImageComboBoxOnColumnEdit(instanceStatuses, icInstanceStatus);
            grdWorkflowInstance.DevExpressGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemImageComboBox });
            grdWorkflowInstance.Columns["InstanceStatus"].ColumnEdit = repositoryItemImageComboBox;

            grdWorkflowInstance.Columns["InstanceName"].MinWidth = 300;
            grdWorkflowInstance.Columns["TimeModified"].MinWidth = 80;
            grdWorkflowInstance.Columns["TimeSumbitted"].MinWidth = 80;
            grdWorkflowInstance.Columns["InstanceStatus"].MinWidth = 80;
            /* 下载报表 */
            GridColumn gcReport = InsertColumn("下载报表", 60);
            grdWorkflowInstance.Columns.Add(gcReport);
        }

        /// <summary>
        /// 设置按钮的状态
        /// </summary>
        private void SetButtonsStates()
        {
            decimal instanceId = Convert.ToDecimal(grdWorkflowInstance.DataKeyValues.Value);
            CustomWorkflowInstanceInfo customWorkflowInstanceInfo = customWorkflowInstanceContract.GetModelInfo(instanceId);
            InstanceStatus instanceStatus = (InstanceStatus)customWorkflowInstanceInfo.InstanceStatus;
            if (instanceStatus == InstanceStatus.None)
            {
                btnEdit.Enabled = true;
                btnDelete.Enabled = true;
                btnWithdraw.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = false;
                btnDelete.Enabled = false;
                WithdrawedResult withdrawedResult = customWorkflowInstanceContract.IsUserWorkflowInstanceWithDrawed(instanceId);
                if ((withdrawedResult == WithdrawedResult.Success) && !customWorkflowInstanceInfo.IsArchived)
                {
                    btnWithdraw.Enabled = true;
                }
                else
                {
                    btnWithdraw.Enabled = false;
                }
            }
        }

        #endregion
    }
}
