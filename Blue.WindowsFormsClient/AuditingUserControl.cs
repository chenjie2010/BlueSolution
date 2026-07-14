using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.DataFilledModule;
using Blue.WindowsFormsClient.Common;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.DataFilledModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient
{
    public partial class AuditingUserControl : UserControl
    {
        #region 契约接口

        /// <summary>
        /// 业务实例契约
        /// </summary>
        private IBusinessInstanceContract businessInstanceContract;
        
        /// <summary>
        /// 数据填报契约
        /// </summary>
        private readonly ICustomDataContract customDataContract;

        /// <summary>
        /// 业务契约
        /// </summary>
        private readonly ICustomBusinessContract customBusinessContract;

        /// <summary>
        /// 用户契约
        /// </summary>
        private readonly IUserAccountContract userAccountContract;

        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomGroupContract customGroupContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuditingUserControl()
        {
            InitializeComponent();
            businessInstanceContract = DataFilledChannelFactory.CreateBusinessInstanceContract();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            customBusinessContract = BusinessChannelFactory.CreateCustomBusinessContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
        }

        #endregion

        #region 控件加载方法

        /// <summary>
        /// 默认加载页面--未审核信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuditingUserControl_Load(object sender, EventArgs e)
        {
            cmbUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(customGroupContract, userTypeContract);
            cmbUserType.InitalizeTreeView();

            cmbDepartment.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
            cmbDepartment.InitalizeTreeView();

            xtcAudit.SelectedTabPageIndex = 0;
            devAudit.Tag = false;
            SetControlsVisible(true);
            LoadData(false);
        }

        /// <summary>
        /// 加载未审核全部信息 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbtUnAuditted_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            lblTimeSumbitted.Text = "提交时间：";
            gcAudit.Text = "待审核数据填报";
            xtcAudit.SelectedTabPageIndex = 0;
            devAudit.Tag = false;
            SetControlsVisible(true);
            LoadData(false);
        }

        /// <summary>
        /// 加载已审核信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbtAuditted_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            lblTimeSumbitted.Text = "审核时间：";
            gcAudit.Text = "已审核数据填报";
            xtcAudit.SelectedTabPageIndex = 0;
            devAudit.Tag = true;
            SetControlsVisible(false);
            LoadData(true);
        }

        private void nbtStatistics_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            xtcAudit.SelectedTabPageIndex = 1;
            //devAudit.Tag = true;
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            LoadData(Convert.ToBoolean(devAudit.Tag));
        }

        private void hlnkClear_Click(object sender, EventArgs e)
        {
            txtInstanceName.Text = string.Empty;
            dtStart.EditValue = null;
            dtEnd.EditValue = null;
            icmbInstanceState.SelectedIndex = 0;
            LoadData(Convert.ToBoolean(devAudit.Tag));
        }

        /// <summary>
        /// 显示审核操作内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devAudit_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "ReviewedAction")
            {
                ReviewedAction reviewedAction = (ReviewedAction)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(reviewedAction);
            }
            else if (e.Column.FieldName == "InstanceState")
            {
                InstanceState instanceState = (InstanceState)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(instanceState);
            }
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devAudit_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            devAudit.CurrentPageIndex = e.NewPageIndex;
            bool audited = Convert.ToBoolean(devAudit.Tag);
            LoadData(audited);
            //if (audited)
            //{
            //    LoadData(false);
            //}
            //else
            //{
            //    LoadData(true);
            //}
        }

        private void btnView_Click(object sender, EventArgs e)
        {
           if (devAudit.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devAudit.DataKeyValues.Value);
                if (instanceId > 0)
                {
                    ShowDataFilledInstanceForm(instanceId, true);
                }
            }
        }

        private void btnAudit_Click(object sender, EventArgs e)
        {
            if (devAudit.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devAudit.DataKeyValues.Value);
                if (instanceId > 0)
                {
                    ShowDataFilledInstanceForm(instanceId, false);
                }
            }
        }

        /// <summary>
        /// 终止该业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAbort_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认终止该业务？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (devAudit.FocusedRowHandle >= 0)
                {
                    decimal instanceId = Convert.ToDecimal(devAudit.DataKeyValues.Value);
                    bool result = businessInstanceContract.AbortBussinessInstance(CurrentUser.Instance.UserId, instanceId);
                    LoadData(false);
                    if (result)
                    {
                        MessageBox.Show("终止成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("下一步审核已完成，无法终止。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        /// <summary>
        /// 撤回已提交的业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnWithdraw_Click(object sender, EventArgs e)
        {
            if (devAudit.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devAudit.DataKeyValues.Value);
                ReviewedAction reviewedAction = businessInstanceContract.GetLastestReviewedAction(instanceId);
                if (reviewedAction == ReviewedAction.Reject || reviewedAction == ReviewedAction.None)
                {
                    MessageBox.Show("驳回状态或者初始化状态不允许撤回。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认撤回该业务？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {

                    bool result = businessInstanceContract.WithDrawBussinessInstance(CurrentUser.Instance.UserId, instanceId);
                    LoadData(true);
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
        }

        private void hlnkView_Click(object sender, EventArgs e)
        {
            if (devAudit.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devAudit.DataKeyValues.Value);
                DataSet ds = businessInstanceContract.GetPageRecord(instanceId);
                gcSteps.DataSource = ds.Tables[0];
                gvSteps.Columns["TimeReviewed"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gvSteps.Columns["TimeReviewed"].DisplayFormat.FormatString = "f";
                gvSteps.BestFitColumns();
                gvSteps.Columns["CommentReviewed"].MinWidth = 150;
                BusinessInstanceInfo businessInstanceInfo = businessInstanceContract.GetModelInfo(instanceId);
                InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
                switch(instanceState)
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
        private void devAudit_OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                decimal instanceId = Convert.ToDecimal(devAudit.DataKeyValues.Value);
                if (instanceId > 0)
                {
                    InstanceState instanceState = businessInstanceContract.GetInstanceState(instanceId);
                    if (instanceState == InstanceState.InitReview || instanceState == InstanceState.FinalReview)
                    {
                        btnAudit.Enabled = true;
                        btnAbort.Enabled = true;
                    }
                    else
                    {
                        btnAudit.Enabled = false;
                        btnAbort.Enabled = false;
                    }
                }
                else
                {
                    btnAudit.Enabled = false;
                    btnAbort.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSteps_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            fpSteps.HideBeakForm();
        }

        #endregion

        #region 私有方法

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
                whereConditons.Add(new WhereConditon("InstanceName", "InstanceName", System.Data.DbType.String, string.Format("%{0}%", condition),
                   DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            /* 用户类型 */
            CommonNode userTypeCommonNode = cmbUserType.Value as CommonNode;
            if (userTypeCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("UserAccount", "UserTypeId", "UserTypeId", System.Data.DbType.Decimal, userTypeCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            /* 单位类型 */
            CommonNode departmentCommonNode = cmbDepartment.Value as CommonNode;
            if (departmentCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("UserAccount", "DepId", "DepId", System.Data.DbType.Decimal, departmentCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            DateTime start = dtStart.DateTime;
            DateTime end = dtEnd.DateTime;
            if (audited)
            {
                /* 已审核的数据填报 */
                if (!DataConvertionHelper.IsNullValue(start))
                {
                    whereConditons.Add(new WhereConditon("TimeReviewed", "TimeReviewed_0", System.Data.DbType.DateTime, start,
                      DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                if (!DataConvertionHelper.IsNullValue(end))
                {
                    whereConditons.Add(new WhereConditon("TimeReviewed", "TimeReviewed_1", System.Data.DbType.DateTime, end,
                      DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                if (icmbInstanceState.SelectedIndex > 0)
                {
                    ReviewedAction reviewedAction = (ReviewedAction)Convert.ToByte(icmbInstanceState.EditValue);
                    whereConditons.Add(new WhereConditon("ReviewedAction", "ReviewedAction", DbType.Byte, reviewedAction,
                                 DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                else
                {
                    whereConditons.Add(new WhereConditon("ReviewedAction", "ReviewedAction_0", DbType.Byte, (byte)ReviewedAction.Abort,
                              DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("ReviewedAction", "ReviewedAction_1", DbType.Byte, (byte)ReviewedAction.Pass,
                                    DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("ReviewedAction", "ReviewedAction_2", DbType.Byte, (byte)ReviewedAction.Reject,
                    DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                }
                whereConditons.Add(new WhereConditon("BusinessInstanceStep", "ActionVisible", "ActionVisible", DbType.Boolean, true,
                              DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("BusinessInstanceStep", "UserId", "UserId", DbType.Decimal, CurrentUser.Instance.UserId,
                                 DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
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
                if (icmbInstanceState.SelectedIndex > 0)
                {
                    InstanceState instanceState = (InstanceState)Convert.ToByte(icmbInstanceState.EditValue);
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState", DbType.Byte, instanceState,
                                 DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                else
                {
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState_0", DbType.Byte, (byte)InstanceState.InitReview,
                                DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("InstanceState", "InstanceState_1", DbType.Byte, (byte)InstanceState.FinalReview,
                                    DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                }
                whereConditons.Add(new WhereConditon("ReviewerId", "ReviewerId", DbType.Decimal, CurrentUser.Instance.UserId,
                                 DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            
            if (audited)
            {
                devAudit.DataKeyNames = new string[] { "InstanceId" };
                devAudit.DataSource = businessInstanceContract.GetBusinessInstanceAudited(devAudit.PageSize * devAudit.CurrentPageIndex,
                        devAudit.PageSize, whereConditons, ref totalCount).Tables[0];                
            }
            else
            {
                devAudit.DataKeyNames = new string[] { "InstanceId", "UserId", "ParentUserId", "DataId" };
                devAudit.DataSource = businessInstanceContract.GetBusinessInstanceUnaudited(devAudit.PageSize * devAudit.CurrentPageIndex,
                    devAudit.PageSize, whereConditons, ref totalCount).Tables[0];
            }
            devAudit.RecordCount = totalCount;
            devAudit.DevExpressGridView.Columns["InstanceName"].MinWidth = 200;
            if (audited)
            {
                devAudit.Columns["TimeReviewed"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                devAudit.Columns["TimeReviewed"].DisplayFormat.FormatString = "f";
                devAudit.DevExpressGridView.Columns["CommentReviewed"].MinWidth = 170;
            }
            else
            {
                devAudit.Columns["TimeSumbitted"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                devAudit.Columns["TimeSumbitted"].DisplayFormat.FormatString = "f";
            }
            if (devAudit.RowCount > 0)
            {
                devAudit.FocusedRowHandle = 0;
                btnAudit.Enabled = true;
                btnAbort.Enabled = true;
                btnWithdraw.Enabled = true;
                btnView.Enabled = true;
                hlnkView.Enabled = true;
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
            icmbInstanceState.Properties.Items.Clear();
            ImageComboBoxItem icbAll = new ImageComboBoxItem("全部", 0, 0);
            icmbInstanceState.Properties.Items.Add(icbAll);
            if (visible)
            {
                EnumItem eiInitReview = UserEnumHelper.GetEnumItem(InstanceState.InitReview);
                ImageComboBoxItem icbInitReview = new ImageComboBoxItem(eiInitReview.Text, eiInitReview.Value, 1);
                icmbInstanceState.Properties.Items.Add(icbInitReview);

                EnumItem eiFinalReview = UserEnumHelper.GetEnumItem(InstanceState.FinalReview);
                ImageComboBoxItem icbFinalReview = new ImageComboBoxItem(eiFinalReview.Text, eiFinalReview.Value, 2);
                icmbInstanceState.Properties.Items.Add(icbFinalReview);
            }
            else
            {
                EnumItem eiPass = UserEnumHelper.GetEnumItem(ReviewedAction.Pass);
                ImageComboBoxItem icbPass = new ImageComboBoxItem(eiPass.Text, eiPass.Value, 3);
                icmbInstanceState.Properties.Items.Add(icbPass);

                EnumItem eiReject = UserEnumHelper.GetEnumItem(ReviewedAction.Reject);
                ImageComboBoxItem icbReject = new ImageComboBoxItem(eiReject.Text, eiReject.Value, 4);
                icmbInstanceState.Properties.Items.Add(icbReject);

                EnumItem eiAbort = UserEnumHelper.GetEnumItem(ReviewedAction.Abort);
                ImageComboBoxItem icbAbort = new ImageComboBoxItem(eiAbort.Text, eiAbort.Value, 5);
                icmbInstanceState.Properties.Items.Add(icbAbort);
            }
            icmbInstanceState.SelectedIndex = 0;
        }

        /// <summary>
        /// 显示数据填报窗体
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="readOnly"></param>
        private void ShowDataFilledInstanceForm(decimal instanceId, bool readOnly)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                BusinessInstanceInfo businessInstanceInfo = businessInstanceContract.GetModelInfo(instanceId);
                CustomDataInfo customDataInfo = customDataContract.GetModelInfo(businessInstanceInfo.DataId);
                string businessName = customBusinessContract.GetBusinessNameByInstanceId(businessInstanceInfo.DataId);
                DataFilledInstanceForm frmDataTemplateTab = new DataFilledInstanceForm()
                {
                    ParentUserId = businessInstanceInfo.ParentUserId,
                    Text = businessName,
                    CustomDataInfo = customDataInfo,
                    InstanceId = instanceId,                    
                    FormReadOnly = readOnly
                };
                frmDataTemplateTab.CloseForm = () => {
                    LoadData(false);
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

        #endregion
        
    }
}
