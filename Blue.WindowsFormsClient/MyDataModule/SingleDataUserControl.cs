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
using AppFramework.Reference.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.DataFilledModule;
using Blue.WindowsFormsClient.Common;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.DataFilledModule;
using Blue.WCFContracts.UserModule;

namespace Blue.WindowsFormsClient.MyDataModule
{
    public partial class SingleDataUserControl : UserControl
    {
        #region 私有变量

        private decimal currentInstanceId = 0;

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

        /// <summary>
        /// 业务实例契约
        /// </summary>
        public IBusinessInstanceContract BusinessInstanceContract
        {
            get;
            set;
        }

        /// <summary>
        /// 用户契约
        /// </summary>
        public IUserAccountContract UserAccountContract
        {
            get;
            set;
        }

    /// <summary>
    /// 返回主界面
    /// </summary>
    public GoBackDelegate GoBack
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SingleDataUserControl()
        {
            InitializeComponent();
            lblDataStateValue.Tag = InstanceState.None;
        }

        #endregion

        #region 控件加载方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleDataUserControl_Load(object sender, EventArgs e)
        {
            ShowStatisticData();
        }

        /// <summary>
        /// 填报操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSumbit_Click(object sender, EventArgs e)
        {
            ShowDataFilledInstanceForm();
        }

        private void hlnkRefresh_Click(object sender, EventArgs e)
        {
            ShowStatisticData();
        }

        /// <summary>
        /// 返回数据填报主界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkBack_Click(object sender, EventArgs e)
        {
            GoBack?.Invoke();
        }


        /// <summary>
        /// 查看历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkHistory_Click(object sender, EventArgs e)
        {
            DataFilledListForm frmDataFilledList = new DataFilledListForm();
            frmDataFilledList.ExtendedCustomBusinessInfo = ExtendedCustomBusinessInfo;
            frmDataFilledList.CustomDataInfo = CustomDataInfo;
            frmDataFilledList.DataSumbittedState = DataSumbittedState.Completed;
            frmDataFilledList.ShowDialog();
        }

        /// <summary>
        /// 撤回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlblWithdraw_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认撤回该业务？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (currentInstanceId > 0)
                {
                    bool result = BusinessInstanceContract.WithDrawBussinessInstance(CurrentUser.Instance.UserId, currentInstanceId);
                    if (result)
                    {
                        BusinessInstanceInfo businessInstanceInfo = BusinessInstanceContract.GetModelInfo(currentInstanceId);
                        ShowCurrentStatus(businessInstanceInfo);
                        MessageBox.Show("撤回成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("下一步审核已完成，无法撤回。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("未提交，无法撤回。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 查看流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkViewProcess_Click(object sender, EventArgs e)
        {
            if (currentInstanceId > 0)
            {
                DataSet ds = BusinessInstanceContract.GetPageRecord(currentInstanceId);
                gcSteps.DataSource = ds.Tables[0];
                gvSteps.Columns["TimeReviewed"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gvSteps.Columns["TimeReviewed"].DisplayFormat.FormatString = "f";
                gvSteps.BestFitColumns();
                gvSteps.Columns["CommentReviewed"].MinWidth = 150;
                BusinessInstanceInfo businessInstanceInfo = BusinessInstanceContract.GetModelInfo(currentInstanceId);
                InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
                switch (instanceState)
                {
                    case InstanceState.None:
                        lblName.Text = "草稿状态待提交。";
                        lblReviewer.Visible = false;
                        break;

                    case InstanceState.InitReview:
                    case InstanceState.FinalReview:
                        CommonUserInfo commonUserInfo = UserAccountContract.GetCommonUserInfo(businessInstanceInfo.ReviewerId);
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
        /// 隐藏流程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSteps_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            fpSteps.HideBeakForm();
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

        #region 公有方法

        /// <summary>
        /// 在控件上加载数据
        /// </summary>
        /// <param name="dataSumbittedInfo"></param>
        public void SetDataOnControls(DataSumbittedInfo dataSumbittedInfo)
        {
            gcMain.Text = dataSumbittedInfo.BusinessName;
            meContent.Text = dataSumbittedInfo.BusinessIntro;            
            lblInitialTimeValue.Text = DataConvertionHelper.EndowStringOfDate(dataSumbittedInfo.InitialTime);
            lblExpiredTimeValue.Text = DataConvertionHelper.EndowStringOfDate(dataSumbittedInfo.ExpiredTime);
            hlnkCondition.Enabled = dataSumbittedInfo.ConditionEnabled;
            hlnkHelp.Enabled = dataSumbittedInfo.HelpEnabled;
            btnSumbit.Enabled = dataSumbittedInfo.BusinessEnabled;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 显示统计数据
        /// </summary>
        private void ShowStatisticData()
        {
            IList<BusinessInstanceInfo> businessInstanceInfos = BusinessInstanceContract.GetModelInfos(CurrentUser.Instance.UserId, CustomDataInfo.DataId);
            if (businessInstanceInfos != null && businessInstanceInfos.Count > 0)
            {
                currentInstanceId = businessInstanceInfos[0].InstanceId;
                ShowCurrentStatus(businessInstanceInfos[0]);
            }
            else
            {
                hlnkViewProcess.Enabled = false;
                lblDataStateValue.Text = UserEnumHelper.GetEnumText(InstanceState.None);
                lblDataSumbittedStateValue.Text = "未填报";
                lblTimeSubmittedValue.Text = string.Empty;
            }            
        }

        /// <summary>
        /// 显示数据填报窗体
        /// </summary>
        private void ShowDataFilledInstanceForm()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                InstanceState instanceState = InstanceState.None;
                if (lblDataStateValue.Tag != null)
                {
                    instanceState = (InstanceState)lblDataStateValue.Tag;
                }
                DataFilledInstanceForm frmDataTemplateTab = new DataFilledInstanceForm()
                {
                    ParentUserId = CurrentUser.Instance.UserId,
                    FormReadOnly = !(instanceState == InstanceState.None),
                    Text = ExtendedCustomBusinessInfo.BusinessName,
                    CustomDataInfo = CustomDataInfo,
                    InstanceId = currentInstanceId
                };
                frmDataTemplateTab.CloseForm = () =>
                {
                    ShowStatisticData();
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
        /// 显示当前状态
        /// </summary>
        /// <param name="businessInstanceInfo"></param>
        private void ShowCurrentStatus(BusinessInstanceInfo businessInstanceInfo)
        {
            if (businessInstanceInfo != null)
            {
                hlnkViewProcess.Enabled = true;
                InstanceState instanceState = (InstanceState)businessInstanceInfo.InstanceState;
                lblDataStateValue.Tag = instanceState;
                if (instanceState != InstanceState.None)
                {
                    lblDataStateValue.Text = UserEnumHelper.GetEnumText((InstanceState)businessInstanceInfo.InstanceState);
                    lblDataSumbittedStateValue.Text = "已提交";
                    btnSumbit.Text = "查看(&V)";
                    if ((instanceState == InstanceState.FinalReview) && CustomDataInfo.IsInitReview)
                    {
                        hlblWithdraw.Enabled = false;
                        hlblWithdraw.Text = "无法撤回";
                    }
                    else
                    {
                        hlblWithdraw.Enabled = true;
                        hlblWithdraw.Text = "撤回...";
                    }
                    lblTimeSubmittedValue.Text = DataConvertionHelper.EndowStringOfLongTime(businessInstanceInfo.TimeSumbitted);
                }
                else
                {
                    lblDataStateValue.Text = UserEnumHelper.GetEnumText(InstanceState.None);
                    lblDataSumbittedStateValue.Text = "草稿状态";
                    btnSumbit.Text = "编辑(&E)";
                    hlblWithdraw.Enabled = false;
                    lblTimeSubmittedValue.Text = string.Empty;
                }
            }
            else
            {
                hlnkViewProcess.Enabled = false;
            }
        }

        #endregion
    }
}
