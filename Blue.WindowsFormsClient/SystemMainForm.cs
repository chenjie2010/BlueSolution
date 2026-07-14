using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.ServiceModel;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Alerter;
using AppFramework.Core;
using AppFramework.Core.ClientConfig;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.WCFContracts;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.SystemModule;
using Blue.WindowsFormsClient.Common;
using Blue.WindowsFormsClient.SystemManagementModule;
using Blue.WindowsFormsClient.BusinessManagementModule;
using Blue.WindowsFormsClient.BusinessDesignerModule;
using Blue.WindowsFormsClient.DataConvertionModule;
using Blue.WindowsFormsClient.CommonHelpModule;

namespace Blue.WindowsFormsClient
{
    public partial class SystemMainForm : Form
    {
        #region 契约接口

        private IDuplexChannelContract duplexChannelContract;
        private IMutualChannelContract mutualChannelContract;
        private ISystemPrvoiderContract systemPrvoiderContract;
        private IUserAccountContract userAccountContract;
        private ISystemConfigContract systemConfigContract;
        private ICustomDepartmentContract customDepartmentContract;

        #endregion

        #region 静态变量

        private static SynchronizationContext synchronizationContext;
        private Semaphore semaphore = new Semaphore(0, ALTER_MAX_MESSAGE_COUNT);

        #endregion

        #region 常量

        /// <summary>
        /// 最大消息数目
        /// </summary>
        private static readonly int ALTER_MAX_MESSAGE_COUNT = 30;

        #endregion

        #region 私有变量

        private bool showExitingTip = true;       /* 是否提示退出当前应用程序窗体 */
        private bool normalClosedChannel = false; /* 服务通道是否是正常关闭的 */
        private Dictionary<SystemMenu, IList<BarLargeButtonItem>> dicBarLargeButtonItems = null;
        private Queue<SystemMessage> alterMessageQueue = new Queue<SystemMessage>(ALTER_MAX_MESSAGE_COUNT);

        #endregion

        #region 窗体变量

        #region  1.个人信息

        private AccountForm frmAccount = null;
        private MyMailForm frmMyMail = null;
        private UserMessageForm frmUserMessage = null;
        private ClientSettingForm frmClientSetting = null;

        #endregion

        #region  2.系统设置

        private SystemManagementForm frmSystemManagement = null;
        private SystemMessageForm frmSystemMessage = null;
        private SystemLogForm frmSystemLog = null;
        private InterfaceForm frmInterface = null;        

        #endregion

        #region  3.业务架构

        private UserForm frmUser = null;
        private RoleForm frmRole = null;
        private AuthorityForm frmAuthority = null;
        private UserTypeForm frmUserType = null;
        private DepartmentForm frmDepartment = null;
        private EnumForm frmEnum = null;
        private AssociationForm frmAssociation = null;
        private DatabaseForm frmDatabase = null;
        private CombinedTableForm frmCombinedTable = null;
        private ViewForm frmView = null;

        #endregion

        #region  4.业务设计

        private DataAuditingForm frmDataUpdated = null;
        private DataAuditingForm frmDataAuditing = null;
        private DataFillForm frmDataFill = null;
        private ReportForm frmInputReport = null;
        private WorkflowForm frmWorkflow = null;
        private QueryForm frmQuery = null;
        private ReportForm frmReport = null;
        private MenuForm frmBusiness = null;
        private ReportingDesignerForm frmReportingDesigner = null;
        private AppointmentForm frmAppointment = null;

        #endregion

        #region  5.业务管理

        private PrintBusinessForm frmPrintBusiness = null;
        private WorkflowInstanceListForm frmWorkflowInstance = null;
        private AppointmentInstanceForm frmAppointmentInstance = null;

        #endregion

        #region  6.业务定制
        

        #endregion

        #region  7.业务数据

        #endregion

        #region  8.数据处理

        private DataBackupForm frmDataBackup = null;
        private DataImportForm frmDataImport = null;
        private DataExchangeForm frmDataExchange = null;
        private RemoteDataForm frmRemoteData = null;
        private DataRelationForm frmDataRelation = null;
        private DataVerifictionForm frmVerifyData = null;

        #endregion

        #region 9.帮助

        private AboutForm frmAbout = null;

        #endregion

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemMainForm()
        {
            InitializeComponent();
            dicBarLargeButtonItems = new Dictionary<SystemMenu, IList<BarLargeButtonItem>>((Enum.GetNames(typeof(SystemMenu)).Length));
            InitData();
            InitDataOnControl();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemMainForm_Load(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(new WaitCallback(ShowMessage));
            if (CurrentUser.Instance.UserName.Equals("system"))
            {
                string backupException = systemPrvoiderContract.GetBackupException();
                if (!string.IsNullOrWhiteSpace(backupException))
                {
                    MessageBox.Show(string.Format("服务器端备份异常，请检查。{0}", backupException), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 用户登录状态切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rsItmStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 窗体关闭中
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (showExitingTip)
            {
                if (MessageBox.Show("确认要退出本系统吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    normalClosedChannel = true; /* 正常关闭通道 */
                    //(duplexChannelContract as ICommunicationObject).Close();
                    //(mutualChannelContract as ICommunicationObject).Close();
                    this.Dispose();
                    System.Environment.Exit(System.Environment.ExitCode);
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 激活双向操作通道
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerActiveChannel_Tick(object sender, EventArgs e)
        {
            //duplexChannelContract.ActiveChannel();
            //mutualChannelContract.ActiveChannel();
        }

        #region  显示工具栏

        /// <summary>
        /// 显示“帐号维护”的工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blcItmAccount_Popup(object sender, EventArgs e)
        {
            ShowToolBar(SystemMenu.SysAccount);
        }

        /// <summary>
        /// 显示“系统设置”的工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blcItmSysSetting_Popup(object sender, EventArgs e)
        {
            ShowToolBar(SystemMenu.SysSetting);
        }

        /// <summary>
        /// 显示“业务架构”的工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blcItmSysArchitecture_Popup(object sender, EventArgs e)
        {
            ShowToolBar(SystemMenu.SysArchitecture);
        }

        /// <summary>
        /// 显示“业务设计”的工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blcItmSysDesigner_Popup(object sender, EventArgs e)
        {
            ShowToolBar(SystemMenu.SysDesigner);
        }

        /// <summary>
        /// 显示“业务管理”的工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blcItmSysManagement_Popup(object sender, EventArgs e)
        {
            ShowToolBar(SystemMenu.SysManagement);
        }

        /// <summary>
        /// 显示“业务定制”的工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blcItmSysBusiness_Popup(object sender, EventArgs e)
        {
            ShowToolBar(SystemMenu.SysBusiness);
        }

        /// <summary>
        /// 显示“业务数据”的工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blcItmSysData_Popup(object sender, EventArgs e)
        {
            ShowToolBar(SystemMenu.SysData);
        }

        /// <summary>
        /// 显示“数据处理”的工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blcItmData_Popup(object sender, EventArgs e)
        {
            ShowToolBar(SystemMenu.SysProcessing);
        }

        /// <summary>
        /// 显示“帮助”的工具栏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void blcItmHelp_Popup(object sender, EventArgs e)
        {
            ShowToolBar(SystemMenu.SysHelp);
        }

        #endregion

        #endregion

        #region 私有方法

        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitData()
        {
            /* 1. 注册系统回调接口 */
            //RegisterSystemCallBackHandler();

            /* 2. 创建契约 */
            systemPrvoiderContract = CommonFactory.CreateSystemPrvoiderContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
        }

        /// <summary>
        /// 注册系统回调接口
        /// </summary>
        public void RegisterSystemCallBackHandler()
        {
            /* 1. 无返回值的远程调用及回调 */
            DuplexSystemCallBackHandler duplexSystemCallBackHandler = new DuplexSystemCallBackHandler(PopupMessage);
            duplexChannelContract = SystemCallBackFactory.CreateDuplexChannelContract(duplexSystemCallBackHandler);
            (duplexChannelContract as ICommunicationObject).Closed += new EventHandler(SystemService_Closed);
            (duplexChannelContract as ICommunicationObject).Faulted += new EventHandler(SystemService_Faulted);
            duplexChannelContract.RegisterSystemServiceCallBack();

            /* 2. 有返回值的远程调用及回调 */
            synchronizationContext = SynchronizationContext.Current;
            MutualSystemCallBackHandler mutualSystemCallBackHandler = new MutualSystemCallBackHandler(OtherPopupMessage);
            mutualChannelContract = SystemCallBackFactory.CreateMutualChannelContract(mutualSystemCallBackHandler);
            (mutualChannelContract as ICommunicationObject).Closed += new EventHandler(SystemService_Closed);
            (mutualChannelContract as ICommunicationObject).Faulted += new EventHandler(SystemService_Faulted);
            bool result = mutualChannelContract.RegisterFlexibleSystemServiceCallBack();

            //timerActiveChannel.Start();
        }

        /// <summary>
        /// 初始化控件上的数据
        /// </summary>
        private void InitDataOnControl()
        {
            /* 1. 加载菜单 */
            //SetMenuItemVisibility();
            CrateToolBar();
            ShowToolBar(SystemMenu.SysAccount);

            /* 3. 加载用户信息 */
            string systemName = systemConfigContract.GetSystemConfigValue((int)SystemConfigKeyName.DefaultSystemName);
            if (string.IsNullOrWhiteSpace(systemName))
            {
                systemName = AppSettingHelper.DefaultSystemName;
            }
            CustomDepartmentInfo customDepartmentInfo = customDepartmentContract.GetRootDepartmentInfo();
            if (customDepartmentInfo != null)
            {
                this.Text = string.Format("{0}{1}", customDepartmentInfo.DepName, systemName);
            }
            else
            {
                throw new ArgumentException("单位管理中根节点名称有误。");
            }

            bsItmUserName.Caption = string.Format("用户名：{0}", CurrentUser.Instance.UserName);
            bsItmActualName.Caption = string.Format("姓名：{0}", CurrentUser.Instance.UserActualName);
            bsItmDataTime.Caption = string.Format("登录时间：{0}", systemPrvoiderContract.GetServerTime().ToString());
            bsItmServerAddress.Caption = string.Format("服务器地址：{0}", CurrentConfig.Instance.ServerAddress);

            int index = (int)CurrentUser.Instance.CurrentUserLogonState;
            if (index >= 0 && index < rsItmStatus.Items.Count)
            {
                beItmStatus.EditValue = rsItmStatus.Items[index].Value;
            }
        }

        /// <summary>
        /// 动态创建工具栏上的按钮
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="caption">文本</param>
        /// <param name="imageIndex">图像索引</param>
        /// <param name="beginGroup">是否启用新分组</param>
        /// <returns>按钮对象</returns>
        private BarLargeButtonItem CreateBarLargeButtonItem(string name, string caption, int imageIndex, bool beginGroup)
        {
            BarLargeButtonItem barLargeButtonItem = new BarLargeButtonItem(barManager, caption);
            barLargeButtonItem.Name = name;
            barLargeButtonItem.LargeImageIndex = imageIndex;
            barLargeButtonItem.CaptionAlignment = BarItemCaptionAlignment.Bottom;
            barLargeButtonItem.Tag = beginGroup;

            return barLargeButtonItem;
        }

        /// <summary>
        /// 创建工具栏
        /// </summary>
        private void CrateToolBar()
        {
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemMenu));
            foreach (EnumItem enumItem in enumItems)
            {
                IList<BarLargeButtonItem> barLargeButtonItems = new List<BarLargeButtonItem>();
                SystemMenu systemMenu = (SystemMenu)enumItem.Value;
                switch (systemMenu)
                {
                    case SystemMenu.SysAccount:

                        /* 1.1 个人资料*/
                        BarLargeButtonItem bbiInfo = CreateBarLargeButtonItem("bbiInfo", "个人资料", 0, false);
                        bbiInfo.ItemClick += bbiInfo_ItemClick;
                        barLargeButtonItems.Add(bbiInfo);

                        /* 1.2 基本设置 */
                        BarLargeButtonItem bbiSettin = CreateBarLargeButtonItem("bbiSettin", "用户设置", 1, false);
                        bbiSettin.ItemClick += bbiSetting_ItemClick;
                        barLargeButtonItems.Add(bbiSettin);

                        /* 1.3 用户通知与消息 */
                        BarLargeButtonItem bbiMessage = CreateBarLargeButtonItem("bbiMessage", "用户通知与消息", 2, true);
                        bbiMessage.ItemClick += bbiMessage_ItemClick;
                        barLargeButtonItems.Add(bbiMessage);

                        /* 1.4 邮件管理*/
                        BarLargeButtonItem bbiMail = CreateBarLargeButtonItem("bbiMail", "邮件管理", 3, false);
                        bbiMail.ItemClick += bbiMail_ItemClick;
                        barLargeButtonItems.Add(bbiMail);

                        /* 1.5 重新登录*/
                        BarLargeButtonItem bbiRelogin = CreateBarLargeButtonItem("bbiRelogin", "重新登录", 4, true);
                        bbiRelogin.ItemClick += bbiRelogin_ItemClick;
                        barLargeButtonItems.Add(bbiRelogin);

                        /* 1.6 退出 */
                        BarLargeButtonItem bbiExit = CreateBarLargeButtonItem("bbiExit", "退    出", 5, false);
                        bbiExit.ItemClick += bbiExit_ItemClick;
                        barLargeButtonItems.Add(bbiExit);

                        break;

                    case SystemMenu.SysSetting:

                        /* 2.1 系统维护 */
                        BarLargeButtonItem bbiSystemManagment = CreateBarLargeButtonItem("bbiSystemManagment", "系统维护", 7, false);
                        bbiSystemManagment.ItemClick += bbiSystemManagment_ItemClick;
                        barLargeButtonItems.Add(bbiSystemManagment);

                        /* 2.2 通知与消息 */
                        BarLargeButtonItem bbiSystemMessage = CreateBarLargeButtonItem("bbiSystemMessage", "通知与消息", 8, true);
                        bbiSystemMessage.ItemClick += bbiSystemMessage_ItemClick;
                        barLargeButtonItems.Add(bbiSystemMessage);

                        /* 2.3 系统日志 */
                        BarLargeButtonItem bbiLog = CreateBarLargeButtonItem("bbiLog", "系统日志", 9, false);
                        bbiLog.ItemClick += bbiLog_ItemClick;
                        barLargeButtonItems.Add(bbiLog);

                        /* 2.4 接口管理 */
                        BarLargeButtonItem bbiInterface = CreateBarLargeButtonItem("bbiInterface", "接口管理", 10, true);
                        bbiInterface.ItemClick += bbiInterface_ItemClick;
                        barLargeButtonItems.Add(bbiInterface);
                        break;

                    case SystemMenu.SysArchitecture:

                        /* 3.1 用户管理 */
                        BarLargeButtonItem bbiUser = CreateBarLargeButtonItem("bbiUser", "用户管理", 11, true);
                        bbiUser.ItemClick += bbiUser_ItemClick;
                        barLargeButtonItems.Add(bbiUser);

                        /* 3.2 角色管理 */
                        BarLargeButtonItem bbiRole = CreateBarLargeButtonItem("bbiRole", "角色管理", 12, false);
                        bbiRole.ItemClick += bbiRole_ItemClick;
                        barLargeButtonItems.Add(bbiRole);

                        /* 3.3 权限管理 */
                        BarLargeButtonItem bbiAuthority = CreateBarLargeButtonItem("bbiAuthority", "权限管理", 13, false);
                        bbiAuthority.ItemClick += bbiAuthority_ItemClick;
                        barLargeButtonItems.Add(bbiAuthority);

                        /* 3.4 用户类型管理 */
                        BarLargeButtonItem bbiUserType = CreateBarLargeButtonItem("bbiUserType", "用户类型管理", 14, true);
                        bbiUserType.ItemClick += bbiUserType_ItemClick;
                        barLargeButtonItems.Add(bbiUserType);

                        /* 3.5 单位管理 */
                        BarLargeButtonItem bbiDepartment = CreateBarLargeButtonItem("bbiDepartment", "单位管理", 15, false);
                        bbiDepartment.ItemClick += bbiDepartment_ItemClick;
                        barLargeButtonItems.Add(bbiDepartment);

                        /* 3.6 枚举管理 */
                        BarLargeButtonItem bbiEnum = CreateBarLargeButtonItem("bbiEnum", "枚举管理", 17, true);
                        bbiEnum.ItemClick += bbiEnum_ItemClick;
                        barLargeButtonItems.Add(bbiEnum);

                        /* 3.7 关联管理 */
                        BarLargeButtonItem bbiAssociation = CreateBarLargeButtonItem("bbiAssociation", "关联管理", 18, false);
                        bbiAssociation.ItemClick += bbiAssociation_ItemClick;
                        barLargeButtonItems.Add(bbiAssociation);

                        /* 3.8 数据表管理 */
                        BarLargeButtonItem bbiTable = CreateBarLargeButtonItem("bbiTable", "数据表管理", 19, false);
                        bbiTable.ItemClick += bbiTable_ItemClick;
                        barLargeButtonItems.Add(bbiTable);

                        /* 3.9 组合表管理 */
                        BarLargeButtonItem bbiCombinedTable = CreateBarLargeButtonItem("bbiCombinedTable", "组合表管理", 20, false);
                        bbiCombinedTable.ItemClick += bbiCombinedTable_ItemClick;
                        barLargeButtonItems.Add(bbiCombinedTable);                        

                        /* 3.10 视图管理 */
                        BarLargeButtonItem bbiView = CreateBarLargeButtonItem("bbiView", "查询视图管理", 21, true);
                        bbiView.ItemClick += bbiView_ItemClick;
                        barLargeButtonItems.Add(bbiView);
                        break;

                    case SystemMenu.SysDesigner:
                        /* 4.1 个人信息设计 */
                        BarLargeButtonItem bbiDataAuditing = CreateBarLargeButtonItem("bbiDataAuditing", "个人信息设计", 27, false);
                        bbiDataAuditing.ItemClick += bbiDataAuditing_ItemClick;
                        barLargeButtonItems.Add(bbiDataAuditing);

                        /* 4.2 个人信息变更设计 */
                        BarLargeButtonItem bbiDataUpdated = CreateBarLargeButtonItem("bbiDataUpdated", "个人信息变更设计", 28, false);
                        bbiDataUpdated.ItemClick += bbiDataUpdated_ItemClick;
                        barLargeButtonItems.Add(bbiDataUpdated);

                        /* 4.3 数据填报 */
                        BarLargeButtonItem bbiDataFill = CreateBarLargeButtonItem("bbiDataFill", "数据填报设计", 29, true);
                        bbiDataFill.ItemClick += bbiDataFill_ItemClick;
                        barLargeButtonItems.Add(bbiDataFill);

                        /* 4.4 复表设计 */
                        BarLargeButtonItem bbiInputReport = CreateBarLargeButtonItem("bbiInputReport", "复表设计", 25, false);
                        bbiInputReport.ItemClick += bbiInputReport_ItemClick;
                        barLargeButtonItems.Add(bbiInputReport);

                        /* 4.5 工作流设计 */
                        BarLargeButtonItem bbiWorkflowDesigner = CreateBarLargeButtonItem("bbiWorkflowDesigner", "工作流设计", 30, true);
                        bbiWorkflowDesigner.ItemClick += bbiWorkflowDesigner_ItemClick;
                        barLargeButtonItems.Add(bbiWorkflowDesigner);

                        /* 4.6 数据查询设计 */
                        BarLargeButtonItem bbiQuery = CreateBarLargeButtonItem("bbiQuery", "数据查询设计", 31, false);
                        bbiQuery.ItemClick += bbiQuery_ItemClick;
                        barLargeButtonItems.Add(bbiQuery);

                        /* 4.7 查询报表设计 */
                        BarLargeButtonItem bbiReportDesigner = CreateBarLargeButtonItem("bbiReportDesigner", "查询报表设计", 32, false);
                        bbiReportDesigner.ItemClick += bbiReportDesigner_ItemClick; ;
                        barLargeButtonItems.Add(bbiReportDesigner);

                        /* 4.8 业务预约设计 */
                        BarLargeButtonItem bbiAppointment = CreateBarLargeButtonItem("bbiAppointment", "业务预约设计", 33, true);
                        bbiAppointment.ItemClick += bbiAppointment_ItemClick;
                        barLargeButtonItems.Add(bbiAppointment);                        

                        /* 4.9 菜单设计 */
                        BarLargeButtonItem bbiMenu = CreateBarLargeButtonItem("bbiMenu", "菜单设计", 34, true);
                        bbiMenu.ItemClick += bbiMenu_ItemClick;
                        barLargeButtonItems.Add(bbiMenu);
                        break;



                    case SystemMenu.SysManagement:
                        /* 5.1 打印管理 */
                        BarLargeButtonItem bbiPrint = CreateBarLargeButtonItem("bbiPrint", "打印管理", 22, true);
                        bbiPrint.ItemClick += bbiPrint_ItemClick;
                        barLargeButtonItems.Add(bbiPrint);

                        /* 5.2 工作流实例管理 */
                        BarLargeButtonItem bbiWorkflowInstance = CreateBarLargeButtonItem("bbiWorkflowInstance", "工作流实例管理", 23, true);
                        bbiWorkflowInstance.ItemClick += bbiWorkflowInstance_ItemClick;
                        barLargeButtonItems.Add(bbiWorkflowInstance);

                        /* 5.3 业务预约实例管理 */
                        BarLargeButtonItem bbiAppointmentInstance = CreateBarLargeButtonItem("bbiAppointmentInstance", "业务预约实例管理", 24, false);
                        bbiAppointmentInstance.ItemClick += bbiAppointmentInstance_ItemClick;
                        barLargeButtonItems.Add(bbiAppointmentInstance);
                        break;

                    case SystemMenu.SysBusiness:
                        /* 6.1 招聘管理 */
                        BarLargeButtonItem bbiWork = CreateBarLargeButtonItem("bbiWork", "招聘管理", 46, true);
                        bbiWork.ItemClick += bbiWork_ItemClick;
                        barLargeButtonItems.Add(bbiWork);

                        /* 6.2 工作流实例管理 */
                        BarLargeButtonItem bbiSalary = CreateBarLargeButtonItem("bbiWorkflowInstance", "工资社保管理", 47, true);
                        bbiSalary.ItemClick += bbiSalary_ItemClick;
                        barLargeButtonItems.Add(bbiSalary);
                        break;

                    case SystemMenu.SysData:
                        /* 7.1 数据设计 */
                        BarLargeButtonItem bbiData = CreateBarLargeButtonItem("bbiData", "数据设计", 50, true);
                        bbiData.ItemClick += bbiData_ItemClick;
                        barLargeButtonItems.Add(bbiData);
                        break;

                    case SystemMenu.SysProcessing:
                        /* 8.1 数据转表 */
                        BarLargeButtonItem bbiDataSwap = CreateBarLargeButtonItem("bbiDataSwap", "数据转表", 36, false);
                        bbiDataSwap.ItemClick += bbiDataSwap_ItemClick;
                        barLargeButtonItems.Add(bbiDataSwap);

                        /* 8.2 数据校验 */
                        BarLargeButtonItem bbiVerifyData = CreateBarLargeButtonItem("bbiVerifyData", "数据校验", 37, false);
                        bbiVerifyData.ItemClick += bbiVerifyData_ItemClick;
                        barLargeButtonItems.Add(bbiVerifyData);

                        /* 8.3 数据交换 */
                        BarLargeButtonItem bbiDataExchange = CreateBarLargeButtonItem("bbiDataExchange", "数据交换", 38, false);
                        bbiDataExchange.ItemClick += bbiDataExchange_ItemClick;
                        barLargeButtonItems.Add(bbiDataExchange);

                        /* 8.4 本地数据导入 */
                        BarLargeButtonItem bbiDataImport = CreateBarLargeButtonItem("bbiDataImport", "本地数据导入", 39, true);
                        bbiDataImport.ItemClick += bbiDataImport_ItemClick;
                        barLargeButtonItems.Add(bbiDataImport);

                        /* 8.5 远程数据导入 */
                        BarLargeButtonItem bbiRemoteData = CreateBarLargeButtonItem("bbiRemoteData", "远程数据导入", 40, false);
                        bbiRemoteData.ItemClick += bbiRemoteData_ItemClick;
                        barLargeButtonItems.Add(bbiRemoteData);

                        /* 8.6 数据备份 */
                        BarLargeButtonItem bbiDataBackup = CreateBarLargeButtonItem("bbiDataBackup", "数据备份", 41, true);
                        bbiDataBackup.ItemClick += bbiDataBackup_ItemClick;
                        barLargeButtonItems.Add(bbiDataBackup);
                        break;

                    case SystemMenu.SysHelp:
                        /* 9.1 帮助主题 */
                        BarLargeButtonItem bbiTopic = CreateBarLargeButtonItem("bbiTopic", "帮助主题", 42, false);
                        bbiTopic.ItemClick += bbiTopic_ItemClick;
                        barLargeButtonItems.Add(bbiTopic);

                        /* 9.2 常见问题 */
                        BarLargeButtonItem bbiQuestion = CreateBarLargeButtonItem("bbiQuestion", "常见问题", 43, false);
                        bbiQuestion.ItemClick += bbiQuestion_ItemClick;
                        barLargeButtonItems.Add(bbiQuestion);

                        /* 9.3 检查更新 */
                        BarLargeButtonItem bbiUpgrade = CreateBarLargeButtonItem("bbiUpgrade", "检查更新", 44, true);
                        bbiUpgrade.ItemClick += bbiUpgrade_ItemClick;
                        barLargeButtonItems.Add(bbiUpgrade);

                        /* 9.4 关于本系统 */
                        BarLargeButtonItem bbiAbout = CreateBarLargeButtonItem("bbiAbout", "关于本系统", 45, false);
                        bbiAbout.ItemClick += bbiAbout_ItemClick;
                        barLargeButtonItems.Add(bbiAbout);
                        break;
                }
                dicBarLargeButtonItems.Add(systemMenu, barLargeButtonItems);
            }

            foreach (KeyValuePair<SystemMenu, IList<BarLargeButtonItem>> keyValue in dicBarLargeButtonItems)
            {
                foreach (BarLargeButtonItem barLargeButtonItem in keyValue.Value)
                {
                    barLargeButtonItem.Visibility = BarItemVisibility.Never;
                    barTools.LinksPersistInfo.Add(new LinkPersistInfo(((BarLinkUserDefines)((BarLinkUserDefines.Caption | BarLinkUserDefines.PaintStyle))),
                    barLargeButtonItem, barLargeButtonItem.Caption, (bool)barLargeButtonItem.Tag, true, true, 0, null, BarItemPaintStyle.CaptionGlyph));
                }
            }
        }
        
        #region  1.“帐号维护”菜单栏操作和工具栏操作

        /// <summary>
        /// 个人信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmAccount == null || frmAccount.IsDisposed)
                {
                    frmAccount = new AccountForm();
                }
                ShowForm(frmAccount);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 基本设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmClientSetting == null || frmClientSetting.IsDisposed)
                {
                    frmClientSetting = new ClientSettingForm();
                }
                ShowForm(frmClientSetting);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 用户通知与消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiMessage_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {                
                Cursor = Cursors.WaitCursor;
                if (frmUserMessage == null || frmUserMessage.IsDisposed)
                {
                    frmUserMessage = new UserMessageForm();
                }
                ShowForm(frmUserMessage);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 邮件管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiMail_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmMyMail == null || frmMyMail.IsDisposed)
                {
                    frmMyMail = new MyMailForm();
                }
                ShowForm(frmMyMail);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 重新登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiRelogin_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageBox.Show("确认需要重新登录吗?", "提示", MessageBoxButtons.OKCancel,
                MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                RestartApplication();
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExitApplication();
        }

        #endregion

        #region  2.“系统设置”菜单栏操作和工具栏操作

        /// <summary>
        /// 系统维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSystemManagment_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmSystemManagement == null || frmSystemManagement.IsDisposed)
                {
                    frmSystemManagement = new SystemManagementForm();
                }
                ShowForm(frmSystemManagement);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }            
        }

        /// <summary>
        /// 系统消息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSystemMessage_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmSystemMessage == null || frmSystemMessage.IsDisposed)
                {
                    frmSystemMessage = new SystemMessageForm();
                }
                ShowForm(frmSystemMessage);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 系统日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiLog_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmSystemLog == null || frmSystemLog.IsDisposed)
                {
                    frmSystemLog = new SystemLogForm();
                }
                ShowForm(frmSystemLog);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 接口管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiInterface_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmInterface == null || frmInterface.IsDisposed)
                {
                    frmInterface = new InterfaceForm();
                }
                ShowForm(frmInterface);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion

        #region  3.“业务架构”菜单栏操作和工具栏操作

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiUser_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmUser == null || frmUser.IsDisposed)
                {
                    frmUser = new UserForm();
                }
                ShowForm(frmUser);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 角色管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiRole_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmRole == null || frmRole.IsDisposed)
                {
                    frmRole = new RoleForm();
                }
                ShowForm(frmRole);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 权限管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAuthority_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmAuthority == null || frmAuthority.IsDisposed)
                {
                    frmAuthority = new AuthorityForm();
                }
                ShowForm(frmAuthority);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 用户类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiUserType_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmUserType == null || frmUserType.IsDisposed)
                {
                    frmUserType = new UserTypeForm();
                }
                ShowForm(frmUserType);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 单位管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDepartment_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmDepartment == null || frmDepartment.IsDisposed)
                {
                    frmDepartment = new DepartmentForm();
                }
                ShowForm(frmDepartment);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 枚举管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiEnum_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmEnum == null || frmEnum.IsDisposed)
                {
                    frmEnum = new EnumForm();
                }
                ShowForm(frmEnum);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }            
        }

        /// <summary>
        /// 关联管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAssociation_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmAssociation == null || frmAssociation.IsDisposed)
                {
                    frmAssociation = new AssociationForm();
                }
                ShowForm(frmAssociation);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 数据管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiTable_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmDatabase == null || frmDatabase.IsDisposed)
                {
                    frmDatabase = new DatabaseForm();
                }
                ShowForm(frmDatabase);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 组合表管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCombinedTable_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmCombinedTable == null || frmCombinedTable.IsDisposed)
                {
                    frmCombinedTable = new CombinedTableForm();
                }
                ShowForm(frmCombinedTable);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }
        
        /// <summary>
        /// 视图管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiView_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmView == null || frmView.IsDisposed)
                {
                    frmView = new ViewForm();
                }
                ShowForm(frmView);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion

        #region 4.“业务设计”菜单栏操作和工具栏操作

        /// <summary>
        /// 信息变更设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDataUpdated_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmDataUpdated == null || frmDataUpdated.IsDisposed)
                {
                    frmDataUpdated = new DataAuditingForm();
                    frmDataUpdated.GroupType = GroupType.InfoUpdated;
                }
                ShowForm(frmDataUpdated);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 信息审核设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDataAuditing_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmDataAuditing == null || frmDataAuditing.IsDisposed)
                {
                    frmDataAuditing = new DataAuditingForm();
                    frmDataAuditing.GroupType = GroupType.InfoAudited;
                }
                ShowForm(frmDataAuditing);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 数据填报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDataFill_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmDataFill == null || frmDataFill.IsDisposed)
                {
                    frmDataFill = new DataFillForm();
                }
                ShowForm(frmDataFill);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 复表设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiInputReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {                
                Cursor = Cursors.WaitCursor;
                if (frmInputReport == null || frmInputReport.IsDisposed)
                {
                    frmInputReport = new ReportForm();
                    frmInputReport.ReportCategory = ReportCategory.Input;
                    frmInputReport.DesignReportAction = (reportId, reportCategory) =>
                    {
                        frmReportingDesigner = new ReportingDesignerForm();
                        //frmReportingDesigner.MdiParent = this;
                        frmReportingDesigner.ReportId = reportId;
                        frmReportingDesigner.ReportCategory = reportCategory;
                        frmReportingDesigner.ShowDialog();
                        //ShowForm(frmReportingDesigner);
                    };
                }
                ShowForm(frmInputReport);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 工作流设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiWorkflowDesigner_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmWorkflow == null || frmWorkflow.IsDisposed)
                {
                    frmWorkflow = new WorkflowForm();
                }
                ShowForm(frmWorkflow);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 查询报表设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiReportDesigner_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmReport == null || frmReport.IsDisposed)
                {
                    frmReport = new ReportForm();
                    frmReport.ReportCategory = ReportCategory.Query;
                    frmReport.DesignReportAction = (reportId, reportCategory) =>
                    {
                        frmReportingDesigner = new ReportingDesignerForm();
                        //frmReportingDesigner.MdiParent = this;
                        frmReportingDesigner.ReportId = reportId;
                        frmReportingDesigner.ReportCategory = reportCategory;
                        frmReportingDesigner.ShowDialog();
                        //ShowForm(frmReportingDesigner);
                    };
                }
                ShowForm(frmReport);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 数据查询设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmQuery == null || frmQuery.IsDisposed)
                {
                    frmQuery = new QueryForm();
                }
                ShowForm(frmQuery);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }
        
        /// <summary>
        /// 业务预约设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAppointment_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmAppointment == null || frmAppointment.IsDisposed)
                {
                    frmAppointment = new AppointmentForm();
                }
                ShowForm(frmAppointment);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 菜单设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiMenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmBusiness == null || frmBusiness.IsDisposed)
                {
                    frmBusiness = new MenuForm();
                }
                ShowForm(frmBusiness);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }



        #endregion

        #region 5.“业务管理”菜单栏操作和工具栏操作

        /// <summary>
        /// 打印管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmPrintBusiness == null || frmPrintBusiness.IsDisposed)
                {
                    frmPrintBusiness = new PrintBusinessForm();
                }
                ShowForm(frmPrintBusiness);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 工作流实例管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiWorkflowInstance_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmWorkflowInstance == null || frmWorkflowInstance.IsDisposed)
                {
                    frmWorkflowInstance = new WorkflowInstanceListForm();
                }
                ShowForm(frmWorkflowInstance);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }

        }

        /// <summary>
        /// 业务预约实例管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAppointmentInstance_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmAppointmentInstance == null || frmAppointmentInstance.IsDisposed)
                {
                    frmAppointmentInstance = new AppointmentInstanceForm();
                }
                ShowForm(frmAppointmentInstance);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion

        #region 6.“业务定制”菜单栏操作和工具栏操作

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiWork_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSalary_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        #endregion

        #region 7.“业务数据”菜单栏操作和工具栏操作

        /// <summary>
        /// 数据设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiData_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        #endregion

        #region 8.“数据处理”菜单栏操作和工具栏操作

        /// <summary>
        /// 数据转表设计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDataSwap_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmDataRelation == null || frmDataRelation.IsDisposed)
                {
                    frmDataRelation = new DataRelationForm();
                }
                ShowForm(frmDataRelation);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 数据校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiVerifyData_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmVerifyData == null || frmVerifyData.IsDisposed)
                {
                    frmVerifyData = new DataVerifictionForm();
                }
                ShowForm(frmVerifyData);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }            
        }


        /// <summary>
        /// 数据交换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDataExchange_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmDataExchange == null || frmDataExchange.IsDisposed)
                {
                    frmDataExchange = new DataExchangeForm();
                }
                ShowForm(frmDataExchange);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 本地数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDataImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmDataImport == null || frmDataImport.IsDisposed)
                {
                    frmDataImport = new DataImportForm();
                }
                ShowForm(frmDataImport);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }


        /// <summary>
        /// 远程数据导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiRemoteData_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmRemoteData == null || frmRemoteData.IsDisposed)
                {
                    frmRemoteData = new RemoteDataForm();
                }
                ShowForm(frmRemoteData);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 数据备份
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDataBackup_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (frmDataBackup == null || frmDataBackup.IsDisposed)
                {
                    frmDataBackup = new DataBackupForm();
                }
                ShowForm(frmDataBackup);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion

        #region 9.“帮助”菜单栏操作和工具栏操作

        /// <summary>
        /// 帮助主题 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiTopic_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 常见问题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiQuestion_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 检查更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiUpgrade_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        /// <summary>
        /// 关于本系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAbout_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        #endregion

        /// <summary>
        /// 工具拦的显示
        /// </summary>
        /// <param name="systemMenu">菜单枚举</param>
        private void ShowToolBar(SystemMenu systemMenu)
        {
            this.SuspendLayout();
            foreach (KeyValuePair<SystemMenu, IList<BarLargeButtonItem>> keyValue in dicBarLargeButtonItems)
            {
                BarItemVisibility barItemVisibility = BarItemVisibility.Never;
                if (keyValue.Key == systemMenu)
                {
                    barItemVisibility = BarItemVisibility.Always;
                }
                foreach (BarLargeButtonItem barLargeButtonItem in keyValue.Value)
                {
                    barLargeButtonItem.Visibility = barItemVisibility;
                }
            }
            this.ResumeLayout(false);
        }

        /// <summary>
        /// 显示子窗体
        /// </summary>
        /// <param name="form">子窗体对象</param>
        private void ShowForm(Form form)
        {
            form.MdiParent = this;
            foreach (Form f in this.MdiChildren)
            {
                if (!f.Name.Equals(form.Name) && (f.WindowState != FormWindowState.Minimized))
                {
                    f.Tag = f.WindowState;
                    f.WindowState = FormWindowState.Minimized;
                    form.SendToBack();
                }
            }
            if (form.WindowState == FormWindowState.Minimized)
            {
                if (form.Tag != null)
                {
                    FormWindowState formWindowState = (FormWindowState)form.Tag;
                    form.WindowState = formWindowState;
                }
                else
                {
                    form.WindowState = FormWindowState.Normal;
                }
            }
            form.BringToFront();
            form.Show();
        }

        /// <summary>
        /// 弹出消息回调
        /// </summary>
        /// <param name="message"></param>
        private void OtherPopupMessage(SystemMessage message)
        {
            //synchronizationContext.Post(delegate { ; }, null);
        }

            /// <summary>
            /// 弹出消息回调
            /// </summary>
            /// <param name="systemMessage"></param>
        private void PopupMessage(SystemMessage systemMessage)
        {
            AddAlterMessage(systemMessage);
        }

        /// <summary>
        /// 将告警信息加入列表中
        /// </summary>
        /// <param name="systemMessage"></param>
        private void AddAlterMessage(SystemMessage systemMessage)
        {
            lock ((alterMessageQueue as ICollection).SyncRoot)
            {
                if (alterMessageQueue.Count > ALTER_MAX_MESSAGE_COUNT)
                {
                    return;
                }
                alterMessageQueue.Enqueue(systemMessage);
            }
            semaphore.Release();
        }

        /// <summary>
        /// 从列表中获取告警信息显示
        /// </summary>
        /// <param name="state"></param>
        private void ShowMessage(object state)
        {
            while (true)
            {
                semaphore.WaitOne();
                Thread.Sleep(2000);
                SystemMessage systemMessage = null;
                lock ((alterMessageQueue as ICollection).SyncRoot)
                {
                    systemMessage = alterMessageQueue.Dequeue();
                }
                if (systemMessage != null)
                {
                    this.BeginInvoke(new ShowAlterMessageDelegate(ShowAlterMessage), new object[] { systemMessage });
                }
            }
        }

        /// <summary>
        /// UI 线程显示消息
        /// </summary>
        /// <param name="systemMessage"></param>
        private void ShowAlterMessage(SystemMessage systemMessage)
        {
            AlertInfo alertInfo = null;
            switch (systemMessage.MessageType)
            {
                case MessageType.SystemMessage:
                    //userAndMessageContract.Update(MessageState.Read, CurrentUser.Instance.UserId, systemMessage.MessageId);
                    alertInfo = new AlertInfo(string.Format("{0}:{1}", UserEnumHelper.GetEnumText(MessageType.SystemMessage), systemMessage.MessageSendTime), 
                        systemMessage.MessageTitle, string.Empty, icMessage.Images[0], systemMessage);
                    break;

                //case MessageType.Email:
                //    alertInfo = new AlertInfo(systemMessage.MessageTitle, systemMessage.MessageContent,
                //        string.Empty, icMessage.Images[1], systemMessage);
                //    break;

                case MessageType.Notice:
                    alertInfo = new AlertInfo(systemMessage.MessageTitle, systemMessage.MessageContent,
                        string.Empty, icMessage.Images[2], systemMessage);
                    break;
            }
            if (alertInfo != null)
            {
                alertControl.Show(this.Owner, alertInfo);
            }
        }

        /// <summary>
        /// 退出应用程序
        /// </summary>
        private void ExitApplication()
        {
            this.Close();
        }

        /// <summary>
        /// 重新启动应用程序
        /// </summary>
        private void RestartApplication()
        {
            Process newProcess = new Process();
            newProcess.StartInfo.FileName = Application.ExecutablePath;
            newProcess.Start();
            System.Environment.Exit(System.Environment.ExitCode);
        }

        /// <summary>
        /// 系统服务操作错误
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemService_Faulted(object sender, EventArgs e)
        {
            if (showExitingTip)
            {
                showExitingTip = false;
                timerActiveChannel.Stop();
                MessageBox.Show("系统服务出现意外，需要退出重新登录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RestartApplication();
            }
        }

        /// <summary>
        /// 系统服务关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemService_Closed(object sender, EventArgs e)
        {
            if (!normalClosedChannel)
            {
                timerActiveChannel.Stop();
                MessageBox.Show("由于服务器端可能已关闭，系统服务出现意外，需要退出重新登录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                RestartApplication();
            }
        }

        #endregion

    }
}
