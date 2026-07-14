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
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.MyBusinessModule
{
    public partial class SingleBusinessControl : UserControl
    {
        #region 私有变量

        private decimal currentInstanceId = 0;

        #endregion

        #region 契约接口

        /// <summary>
        /// 数据填报契约
        /// </summary>
        private readonly ICustomDataContract customDataContract;

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

        /// <summary>
        /// 工作流实例契约
        /// </summary>
        public ICustomWorkflowInstanceContract CustomWorkflowInstanceContract
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
        public SingleBusinessControl()
        {
            InitializeComponent();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
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

        #endregion

        #region 公有方法

        /// <summary>
        /// 在控件上加载数据
        /// </summary>
        /// <param name="dataSumbittedInfo"></param>
        public void SetDataOnControls(WorkflowBusinessInfo workflowBusinessInfo)
        {
            gcMain.Text = workflowBusinessInfo.BusinessName;
            meContent.Text = workflowBusinessInfo.BusinessIntro;
            lblInitialTimeValue.Text = DataConvertionHelper.EndowStringOfDate(workflowBusinessInfo.InitialTime);
            lblExpiredTimeValue.Text = DataConvertionHelper.EndowStringOfDate(workflowBusinessInfo.ExpiredTime);
            hlnkCondition.Enabled = workflowBusinessInfo.ConditionEnabled;
            hlnkHelp.Enabled = workflowBusinessInfo.HelpEnabled;
            btnSumbit.Enabled = workflowBusinessInfo.BusinessEnabled;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 显示统计数据
        /// </summary>
        private void ShowStatisticData()
        {
            IList<CustomWorkflowInstanceInfo> customWorkflowInstanceInfos = CustomWorkflowInstanceContract.GetModelInfos(CurrentUser.Instance.UserId, CustomWorkflowInfo.WorkflowId);
            if (customWorkflowInstanceInfos != null && customWorkflowInstanceInfos.Count > 0)
            {
                currentInstanceId = customWorkflowInstanceInfos[0].InstanceId;
                btnSumbit.Tag = customWorkflowInstanceInfos[0];
                InstanceStatus instanceStatus = (InstanceStatus)customWorkflowInstanceInfos[0].InstanceStatus;
                if (instanceStatus != InstanceStatus.None)
                {
                    lblDataStateValue.Text = UserEnumHelper.GetEnumText((InstanceStatus)customWorkflowInstanceInfos[0].InstanceStatus);
                    lblDataSumbittedStateValue.Text = "已提交";
                    btnSumbit.Text = "查看(&V)";                    
                    if (instanceStatus == InstanceStatus.Completed || instanceStatus == InstanceStatus.Review || instanceStatus == InstanceStatus.ReviewAborted)
                    {
                        hlblWithdraw.Enabled = false;
                        hlblWithdraw.Text = "无法撤回";
                    }
                    lblTimeSubmittedValue.Text = DataConvertionHelper.EndowStringOfLongTime(customWorkflowInstanceInfos[0].TimeCreated);
                }
                else
                {
                    lblDataStateValue.Text = UserEnumHelper.GetEnumText(InstanceStatus.None);
                    lblDataSumbittedStateValue.Text = "草稿状态";
                    btnSumbit.Text = "编辑(&E)";
                    hlblWithdraw.Enabled = false;
                    lblTimeSubmittedValue.Text = string.Empty;
                }
            }
            else
            {
                btnSumbit.Tag = null;
                lblDataStateValue.Text = UserEnumHelper.GetEnumText(InstanceStatus.None);
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
                bool readOnly = false;
                if (btnSumbit.Tag != null)
                {
                    CustomWorkflowInstanceInfo customWorkflowInstanceInfo = (CustomWorkflowInstanceInfo)btnSumbit.Tag;
                    InstanceStatus instanceStatus = (InstanceStatus)customWorkflowInstanceInfo.InstanceStatus;
                    if (instanceStatus != InstanceStatus.None)
                    {
                        readOnly = true;
                    }
                }
                CustomDataInfo customDataInfo = customDataContract.GetModelInfo(CustomWorkflowInfo.DataId);
                WorkflowInstanceForm frmDataTemplateTab = new WorkflowInstanceForm()
                {
                    ParentUserId = CurrentUser.Instance.UserId,
                    Text = ExtendedCustomBusinessInfo.BusinessName,
                    CustomWorkflowInfo = CustomWorkflowInfo,
                    CustomDataInfo = customDataInfo,
                    InstanceId = currentInstanceId,
                    FormReadOnly = readOnly
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

        #endregion

        /// <summary>
        /// 查看历史记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkHistory_Click(object sender, EventArgs e)
        {
            BusinessListForm businessListForm = new BusinessListForm();
            businessListForm.ExtendedCustomBusinessInfo = ExtendedCustomBusinessInfo;
            businessListForm.CustomWorkflowInfo = CustomWorkflowInfo;
            businessListForm.InstanceStatus = InstanceStatus.Completed;
            businessListForm.ShowDialog();
        }

    }
}
