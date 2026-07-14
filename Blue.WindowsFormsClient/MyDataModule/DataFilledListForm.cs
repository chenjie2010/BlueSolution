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
using Blue.Model.BusinessModule;
using Blue.Model.DataFilledModule;
using Blue.WindowsFormsClient.Common;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.DataFilledModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WinBusinessLogic.UserReport;

namespace Blue.WindowsFormsClient.MyDataModule
{
    public partial class DataFilledListForm : Form
    {
        #region 契约接口

        private readonly ICustomDataContract customDataContract;
        private readonly IBusinessInstanceContract businessInstanceContract;
        private readonly ICustomBusinessContract customBusinessContract;
        private readonly IUserAccountContract userAccountContract;

        private ICustomTableContract customTableContract;
        private ICustomDataFieldContract customDataFieldContract;
        private ICustomReportContract customReportContract;
        private ICustomSheetContract customSheetContract;
        private ICustomCellContract customCellContract;
        private ISystemConfigContract systemConfigContract;
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
        /// 数据填充对象
        /// </summary>
        public CustomDataInfo CustomDataInfo
        {
            get;
            set;
        }

        public bool AllLoaded
        {
            get;
            set;
        }

        public DataSumbittedState DataSumbittedState
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataFilledListForm()
        {
            InitializeComponent();
            customBusinessContract = BusinessChannelFactory.CreateCustomBusinessContract();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            businessInstanceContract = DataFilledChannelFactory.CreateBusinessInstanceContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();

            UserControlHelper.InitCheckedComboBoxEditItems(ccmbInstanceState, typeof(InstanceState));
            AllLoaded = false;
            DataSumbittedState = DataSumbittedState.Drfat;            
        }

        #endregion

        #region 控件加载方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFilledListForm_Load(object sender, EventArgs e)
        {
            DataFilledType dataFilledType = (DataFilledType)CustomDataInfo.DataFilledType;
            if (dataFilledType == DataFilledType.Single)
            {
                lblInstanceState.Visible = false;
                ccmbInstanceState.Visible = false;
                txtInstanceName.Width = 480;
            }
            if (!AllLoaded)
            {
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem checkedListBoxItem in ccmbInstanceState.Properties.Items)
                {
                    EnumItem enumItem = checkedListBoxItem.Value as EnumItem;
                    InstanceState instanceState = (InstanceState)enumItem.Value;
                    switch(DataSumbittedState)
                    {
                        case DataSumbittedState.Drfat:
                            if(instanceState == InstanceState.None)
                            {
                                checkedListBoxItem.CheckState = CheckState.Checked;
                            }
                            break;

                        case DataSumbittedState.Review:
                            if (instanceState == InstanceState.InitReview || instanceState == InstanceState.FinalReview)
                            {
                                checkedListBoxItem.CheckState = CheckState.Checked;
                            }
                            break;

                        case DataSumbittedState.Completed:
                            if (instanceState == InstanceState.Completed)
                            {
                                checkedListBoxItem.CheckState = CheckState.Checked;
                            }
                            break;
                    }
                }
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
        /// 设置审核状态，显示文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDataFilled_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AudittedStatus")
            {
                bool audittedStatus = Convert.ToBoolean(e.Value);
                if (audittedStatus)
                {
                    e.DisplayText = "审核正常";
                }
                else
                {
                    e.DisplayText = "被驳回[查看]";
                }
            }
        }

        /// <summary>
        /// 显示最新的审核意见
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDataFilled_OnRowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Column.FieldName.Equals("AudittedStatus"))
            {
                decimal instanceId = Convert.ToDecimal(grdDataFilled.DataKeyValues.Value);
                string lastestComment = businessInstanceContract.GetLastestComment(instanceId);
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
                fpComment.ShowBeakForm(grdDataFilled.DevExpressGridControl.PointToScreen(new Point(e.X, e.Y)));

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
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 插入行
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
            grdDataFilled.DevExpressGridControl.RepositoryItems.Add(repositoryItemHyperLinkEdit);
            repositoryItemHyperLinkEdit.Click += (sender, e) =>
             {
                 if (grdDataFilled.FocusedDataRow == null) return;
                 decimal instanceId = Convert.ToDecimal(grdDataFilled.DataKeyValues.Value);
                 BusinessInstanceInfo businessInstanceInfo = businessInstanceContract.GetModelInfo(instanceId);
                 if (customReportContract == null)
                 {
                     customTableContract = BusinessChannelFactory.CreateCustomTableContract();
                     customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
                     customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
                     customSheetContract = BusinessDesignerChannelFactory.CreateCustomSheetContract();
                     customCellContract = BusinessDesignerChannelFactory.CreateCustomCellContract();
                     systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
                 }
                 if (CustomDataInfo.ReportId > 0 && instanceId > 0)
                 {
                     CustomReport customReport = new CustomReport(customReportContract, customSheetContract, customCellContract,
                               customTableContract, customDataFieldContract, systemConfigContract);
                     customReport.GenerateExcel(CustomDataInfo.ReportId, businessInstanceInfo.ParentUserId);
                 }
                 else
                 {
                     MessageBox.Show("该业务不提供报表下载。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                 }
             };

            return gcEdit;
        }

        /// <summary>
        /// 显示数据填报窗体
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="parentUserId"></param>
        /// <param name="readOnly"></param>
        private void ShowDataFilledInstanceForm(decimal instanceId, decimal parentUserId, bool readOnly)
        {
            try
            {

                Cursor = Cursors.WaitCursor;
                DataFilledInstanceForm frmDataTemplateTab = new DataFilledInstanceForm()
                {
                    ParentUserId = parentUserId,
                    Text = ExtendedCustomBusinessInfo.BusinessName,
                    CustomDataInfo = CustomDataInfo,
                    InstanceId = instanceId,
                    FormReadOnly = readOnly
                };
                frmDataTemplateTab.CloseForm = () => {
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
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkClean_Click(object sender, EventArgs e)
        {
            txtInstanceName.Text = string.Empty;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbInstanceState);
            deTimeFrom.EditValue = null;
            deDateTimeTo.EditValue = null;
            grdDataFilled.CurrentPageIndex = 0;
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
        private void grdDataFilled_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            grdDataFilled.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 数据填报行类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDataFilled_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {            
            InstanceState instanceState = (InstanceState)Convert.ToByte(grdDataFilled.GetRowCellValue(e.RowHandle, "InstanceState"));
            switch (instanceState)
            {
                case InstanceState.InitReview:
                case InstanceState.FinalReview:
                    e.Appearance.ForeColor = Color.LightGray;
                    break;

                case InstanceState.Completed:
                    e.Appearance.ForeColor = Color.DarkGray;
                    break;
            }
        }

        /// <summary>
        /// 行变换时数据填报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdDataFilled_OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if(e.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(grdDataFilled.DataKeyValues.Value);
                BusinessInstanceInfo businessInstanceInfo = businessInstanceContract.GetModelInfo(instanceId);
                InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
                if (instanceState == InstanceState.None)
                {
                    btnEdit.Enabled = true;
                    btnDelete.Enabled = true;
                }
                else
                {
                    btnEdit.Enabled = false;
                    btnDelete.Enabled = false;
                }
                if (instanceState == InstanceState.InitReview || ((instanceState == InstanceState.FinalReview) && !CustomDataInfo.IsInitReview))
                {
                    btnWithdraw.Enabled = true;
                }
                else
                {
                    btnWithdraw.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 窗体按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnView_Click(object sender, EventArgs e)
        {
            if (grdDataFilled.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(grdDataFilled.DataKeyValues.Value);
                decimal parentUserId = Convert.ToDecimal(grdDataFilled.DataKeyValues["ParentUserId"]);
                if (instanceId > 0)
                {
                    ShowDataFilledInstanceForm(instanceId, parentUserId, true);
                }
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, EventArgs e)
        {
            decimal instanceId = Convert.ToDecimal(grdDataFilled.DataKeyValues.Value);
            decimal parentUserId = Convert.ToDecimal(grdDataFilled.DataKeyValues["ParentUserId"]);
            BusinessInstanceInfo businessInstanceInfo = businessInstanceContract.GetModelInfo(instanceId);
            if (businessInstanceInfo != null)
            {
                InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
                if (instanceState != InstanceState.None)
                {
                    LoadData();
                    MessageBox.Show("业务状态发生变化，无法编辑。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    ShowDataFilledInstanceForm(instanceId, parentUserId, false);
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                decimal instanceId = Convert.ToDecimal(grdDataFilled.DataKeyValues.Value);
                BusinessInstanceInfo businessInstanceInfo = businessInstanceContract.GetModelInfo(instanceId);
                if (businessInstanceInfo != null)
                {
                    InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
                    if (instanceState != InstanceState.None)
                    {
                        LoadData();
                        MessageBox.Show("业务状态发生变化，无法删除。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        /* 删除 */
                        if (MessageBox.Show("确认删除该填报？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            Cursor = Cursors.WaitCursor;
                            businessInstanceContract.Delete(instanceId);
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
        /// 撤回提交的业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认撤回该业务？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                decimal instanceId = Convert.ToDecimal(grdDataFilled.DataKeyValues.Value);
                bool result = businessInstanceContract.WithDrawBussinessInstance(CurrentUser.Instance.UserId, instanceId);
                LoadData();
                if (result)
                {
                    MessageBox.Show("撤回成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("下一步审核已完成，无法撤回。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkView_Click(object sender, EventArgs e)
        {
            if (grdDataFilled.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(grdDataFilled.DataKeyValues.Value);
                DataSet ds = businessInstanceContract.GetPageRecord(instanceId);
                gcSteps.DataSource = ds.Tables[0];
                gvSteps.Columns["TimeReviewed"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gvSteps.Columns["TimeReviewed"].DisplayFormat.FormatString = "f";
                gvSteps.BestFitColumns();
                gvSteps.Columns["CommentReviewed"].MinWidth = 150;
                BusinessInstanceInfo businessInstanceInfo = businessInstanceContract.GetModelInfo(instanceId);
                InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
                switch (instanceState)
                {
                    case InstanceState.None:
                        lblName.Text = "草稿状态待提交。";
                        lblReviewer.Visible = false;
                        break;

                    case InstanceState.InitReview:
                    case InstanceState.FinalReview:
                        CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(businessInstanceInfo.ReviewerId);
                        if (commonUserInfo != null)
                        {
                            lblReviewer.Text = string.Format("{0}({1}, {2})", commonUserInfo.UserName,
                                commonUserInfo.UserActualName, commonUserInfo.DepName);
                        }
                        lblName.Text = "下一步审核人：";
                        lblReviewer.Visible = true;
                        break;

                    case InstanceState.Completed:
                        lblName.Text = "审核已完成。";
                        lblReviewer.Visible = false;
                        break;

                    case InstanceState.InitReviewAborted:
                        lblName.Text = "初审时终止。";
                        lblReviewer.Visible = false;
                        break;

                    case InstanceState.FinalReviewAborted:
                        lblName.Text = "终审时终止。";
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
            //whereConditons.Add(new WhereConditon("IsArchived", "IsArchived", DbType.Boolean, false, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("DataId", "DataId", DbType.Decimal, CustomDataInfo.DataId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, CurrentUser.Instance.UserId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            string instanceName = txtInstanceName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(instanceName))
            {
                instanceName = string.Format("%{0}%", instanceName);
                whereConditons.Add(new WhereConditon("InstanceName", "InstanceName", DbType.String,
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
            DataFilledType dataFilledType = (DataFilledType)CustomDataInfo.DataFilledType;
            if (dataFilledType == DataFilledType.Common)
            {
                Int64 instanceStateValue = UserControlHelper.GetCheckedComboBoxEditItems(ccmbInstanceState);
                IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(InstanceState));
                IList<InstanceState> instanceStates = new List<InstanceState>();
                foreach (EnumItem enumItem in enumItems)
                {
                    bool result = AuthorityHelper.CheckAuthority(instanceStateValue, enumItem.Value);
                    if (result)
                    {
                        instanceStates.Add((InstanceState)enumItem.Value);
                    }
                }
                for (int idx = 0; idx < instanceStates.Count; idx++)
                {
                    if (idx == 0)
                    {
                        if (instanceStates.Count == 1)
                        {
                            whereConditons.Add(new WhereConditon("InstanceState", string.Format("InstanceState_{0}", idx), DbType.Byte, (byte)instanceStates[idx],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                        }
                        else
                        {
                            whereConditons.Add(new WhereConditon("InstanceState", string.Format("InstanceState_{0}", idx), DbType.Byte, (byte)instanceStates[idx],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                        }
                    }
                    else if (idx == instanceStates.Count - 1)
                    {
                        whereConditons.Add(new WhereConditon("InstanceState", string.Format("InstanceState_{0}", idx), DbType.Byte, (byte)instanceStates[idx],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    }
                    else
                    {
                        whereConditons.Add(new WhereConditon("InstanceState", string.Format("InstanceState_{0}", idx), DbType.Byte, (byte)instanceStates[idx],
                                DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    }
                }
            }
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("TimeSumbitted", CustomSorting.Ascending));

            int totalCount = 0;
            DataTable dt = businessInstanceContract.GetPageRecord(grdDataFilled.PageSize * grdDataFilled.CurrentPageIndex,
                grdDataFilled.PageSize, whereConditons, sortingCondtions, ref totalCount).Tables[0];
            grdDataFilled.DataSource = dt;
            grdDataFilled.RecordCount = totalCount;
            grdDataFilled.DevExpressGridView.BestFitColumns();
            grdDataFilled.Columns["InstanceName"].MinWidth = 300;
            grdDataFilled.Columns["LastTimeHandled"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grdDataFilled.Columns["TimeSumbitted"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            grdDataFilled.Columns["LastTimeHandled"].DisplayFormat.FormatString = "f";
            grdDataFilled.Columns["LastTimeHandled"].MinWidth = 80;
            grdDataFilled.Columns["TimeSumbitted"].DisplayFormat.FormatString = "f";
            grdDataFilled.Columns["TimeSumbitted"].MinWidth = 80;
            grdDataFilled.Columns["InstanceState"].DisplayFormat.FormatString = "d";
            grdDataFilled.Columns["InstanceState"].DisplayFormat.Format = new CustomFormatter();
            RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit = new RepositoryItemHyperLinkEdit();
            grdDataFilled.Columns["AudittedStatus"].ColumnEdit = repositoryItemHyperLinkEdit;
            grdDataFilled.DevExpressGridControl.RepositoryItems.Add(repositoryItemHyperLinkEdit);
            /* 下载报表 */
            GridColumn gcReport = InsertColumn("下载报表", 60);
            grdDataFilled.Columns.Add(gcReport);
        }
        
        #endregion
    }
}
