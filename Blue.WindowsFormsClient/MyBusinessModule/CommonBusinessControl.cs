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
    public partial class CommonBusinessControl : UserControl
    {
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
        public CommonBusinessControl()
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
        private void CommonDataUserControl_Load(object sender, EventArgs e)
        {            
            btnCreate.Visible = ExtendedCustomBusinessInfo.ThirdModeEnabled;
            ShowStatisticData();
        }        

        /// <summary>
        /// 创建工作流操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSumbit_Click(object sender, EventArgs e)
        {
            Create(false);
        }

        /// <summary>
        /// 第三方创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (ExtendedCustomBusinessInfo.ThirdModeEnabled)
            {
                Create(true);
            }
            else
            {
                MessageBox.Show("您当前没有第三方创建的权限，请与管理员联系。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }        

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkRefresh_Click(object sender, EventArgs e)
        {
            ShowStatisticData();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkTimeSubmittedValue_Click(object sender, EventArgs e)
        {
            ShowBusinessListForm(InstanceStatus.Review);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkDraftValue_Click(object sender, EventArgs e)
        {
            ShowBusinessListForm(InstanceStatus.None);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkCompletedValue_Click(object sender, EventArgs e)
        {
            ShowBusinessListForm(InstanceStatus.Completed);
        }

        /// <summary>
        /// 查看所有的状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkViewAll_Click(object sender, EventArgs e)
        {
            BusinessListForm businessListForm = new BusinessListForm()
            {
                ExtendedCustomBusinessInfo = ExtendedCustomBusinessInfo,
                CustomWorkflowInfo = CustomWorkflowInfo,
                AllLoaded = true,
                InstanceStatus = InstanceStatus.Completed
            };
            businessListForm.ShowDialog();
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
        /// 显示业务基本信息
        /// </summary>
        /// <param name="workflowBusinessInfo"></param>
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
        /// 显示数据填报窗体
        /// </summary>
        /// <param name="thirdParty"></param>
        private void ShowDataFilledInstanceForm(bool thirdParty)
        {
            try
            {
                decimal parentUserId = decimal.MinValue;
                if (thirdParty)
                {
                    UserListForm frmUserList = new UserListForm();
                    frmUserList.GetIdentifier = (userId) =>
                    {
                        parentUserId = userId;
                    };
                    frmUserList.ShowDialog();
                }
                else
                {
                    parentUserId = CurrentUser.Instance.UserId;
                }
                if (parentUserId > 0)
                {
                    Cursor = Cursors.WaitCursor;
                    CustomDataInfo customDataInfo = customDataContract.GetModelInfo(CustomWorkflowInfo.DataId);
                    WorkflowInstanceForm frmWorkflowInstance = new WorkflowInstanceForm()
                    {
                        ParentUserId = parentUserId,
                        Text = ExtendedCustomBusinessInfo.BusinessName,
                        CustomWorkflowInfo = CustomWorkflowInfo,
                        CustomDataInfo = customDataInfo,
                        InstanceId = 0
                    };
                    frmWorkflowInstance.CloseForm = () =>
                    {
                        ShowStatisticData();
                    };
                    Cursor = Cursors.Default;
                    frmWorkflowInstance.ShowDialog();
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
        /// 显示统计数据
        /// </summary>
        private void ShowStatisticData()
        {
            hlnkTimeSubmittedValue.Text = string.Format("共有{0}件",
                CustomWorkflowInstanceContract.GetWorkflowInstanceCount(CurrentUser.Instance.UserId, ExtendedCustomBusinessInfo.WorkflowId, InstanceStatus.Review));

            hlnkDraftValue.Text = string.Format("共有{0}件",
                CustomWorkflowInstanceContract.GetWorkflowInstanceCount(CurrentUser.Instance.UserId, ExtendedCustomBusinessInfo.WorkflowId, InstanceStatus.None));

            hlnkCompletedValue.Text = string.Format("共有{0}件",
                CustomWorkflowInstanceContract.GetWorkflowInstanceCount(CurrentUser.Instance.UserId, ExtendedCustomBusinessInfo.WorkflowId, InstanceStatus.Completed));
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="instanceStatus"></param>
        private void ShowBusinessListForm(InstanceStatus instanceStatus)
        {
            BusinessListForm businessListForm = new BusinessListForm()
            {
                ExtendedCustomBusinessInfo = ExtendedCustomBusinessInfo,
                CustomWorkflowInfo = CustomWorkflowInfo,
                AllLoaded = false,
                InstanceStatus = instanceStatus
            };
            businessListForm.ShowDialog();
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="thirdParty"></param>
        private void Create(bool thirdParty)
        {
            if (ExtendedCustomBusinessInfo != null)
            {
                BusinessMenu businessMenu = (BusinessMenu)ExtendedCustomBusinessInfo.BusinessMenu;
                switch (businessMenu)
                {
                    case BusinessMenu.CommonBusiness:
                        if (CustomWorkflowInfo.EnableGuidance)
                        {
                            DataGuidanceForm frmDataGuidance = new DataGuidanceForm()
                            {
                                AttachmentId = CustomWorkflowInfo.WorkflowId,
                                AttachmentCategory = AttachmentCategory.Workflow,
                                Content = CustomWorkflowInfo.Guidance,
                                BottomVisible = true
                            };
                            frmDataGuidance.DataSumbitted = (hasAlreadyRead) =>
                            {
                                if (!hasAlreadyRead) return;
                                frmDataGuidance.Hide();
                                ShowDataFilledInstanceForm(thirdParty);
                            };
                            frmDataGuidance.ShowDialog();
                        }
                        else
                        {
                            ShowDataFilledInstanceForm(thirdParty);
                        }
                        break;
                }
            }
        }

        #endregion                
    }
}
