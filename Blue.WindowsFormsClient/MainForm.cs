using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars.Docking2010;
using DevExpress.XtraEditors.ButtonPanel;
using DevExpress.XtraBars.Navigation;
using AppFramework.Core;
using AppFramework.Core.ClientConfig;
using Blue.CustomLibrary;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.DataFilledModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient
{
    public partial class MainForm : Form
    {
        #region 契约接口

        private readonly ISystemPrvoiderContract systemPrvoiderContract;
        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomDataContract customDataContract;
        private readonly IBusinessInstanceContract businessInstanceContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomQueyContract customQueyContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomViewContract customViewContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomWorkflowContract customWorkflowContract;
        private readonly ICustomWorkflowInstanceContract customWorkflowInstanceContract;
        private readonly ICustomMenuContract customMenuContract;

        #endregion

        #region 私有控件

        private readonly MainPageControl ucMainPage;
        private PersonalDataControl ucPersonalData;
        private DataAuditingControl ucDataAuditing;
        private CommonBusinessControl ucCommonBusiness;
        private MyWorkUserControl ucMyWork;
        private SumbittedDataUserControl ucSumbittedData;
        private AuditingUserControl ucAuditing;
        private QueryUserControl ucQuery;
        private ReportControl ucReport;
        private UserBusinessControl ucUserBusiness;

        #endregion

        #region 私有变量

        private Dictionary<decimal, NavigationPage> navigationPages = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            systemPrvoiderContract = CommonFactory.CreateSystemPrvoiderContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
            businessInstanceContract = DataFilledChannelFactory.CreateBusinessInstanceContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customQueyContract = BusinessChannelFactory.CreateCustomQueyContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customViewContract = BusinessChannelFactory.CreateCustomViewContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customWorkflowContract = BusinessChannelFactory.CreateCustomWorkflowContract();
            customWorkflowInstanceContract = BusinessChannelFactory.CreateCustomWorkflowInstanceContract();
            customMenuContract = BusinessChannelFactory.CreateCustomMenuContract();

            ucMainPage = new MainPageControl()
            {
                UserAccountContract = userAccountContract,
                SystemPrvoiderContract = systemPrvoiderContract
            };
            ucMainPage.RefreshForm = () =>
            {
                ClearPages();
                LoadPages();
            };
            ucMainPage.Dock = DockStyle.Fill;
            ngpHomePage.Controls.Add(ucMainPage);

            navigationPages = new Dictionary<decimal, NavigationPage>();
            LoadPages();
            //int interval = (150 - menuCountVisible * 20);
            //upnlMain.ButtonInterval = interval > 0 ? interval : 0;            
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            string serverAddress = CurrentConfig.Instance.ServerAddress;
            int pos = serverAddress.IndexOf('.');
            serverAddress = serverAddress.Substring(pos + 1);
            pos = serverAddress.IndexOf('.');
            serverAddress = serverAddress.Substring(pos + 1);
            switch(CurrentConfig.Instance.SoftwareVersion)
            {
                case SoftwareVersion.UnKown:
                    this.Text = string.Format("{0}_{1}_{2}_未知版本", CurrentUser.Instance.UserName, serverAddress, this.Text);
                    break;

                case SoftwareVersion.Trial:
                    this.Text = string.Format("{0}_{1}_{2}_试用版", CurrentUser.Instance.UserName, serverAddress, this.Text);
                    break;

                default:
                    this.Text = string.Format("{0}_{1}_{2}", CurrentUser.Instance.UserName, serverAddress, this.Text);
                    break;
            }            
        }

        /// <summary>
        /// 业务菜单切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upnlMain_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            byte mainMenuId = Convert.ToByte(e.Button.Properties.Tag);

            if (mainMenuId >= 0 && mainMenuId <= (byte)BusinessMenu.Max)
            {
                BusinessMenu systemBusiness = (BusinessMenu)mainMenuId;
                switch (systemBusiness)
                {
                    case BusinessMenu.MainPage:
                        ngfMain.SelectedPage = ngpHomePage;
                        break;

                    case BusinessMenu.PersonalData:
                        ngfMain.SelectedPage = ngpPersonalData;
                        break;

                    case BusinessMenu.DataAuditing:
                        ngfMain.SelectedPage = ngpDataAuditing;
                        break;

                    case BusinessMenu.MyWork:
                        ngfMain.SelectedPage = ngpMyWork;
                        break;

                    case BusinessMenu.CommonBusiness:
                        ngfMain.SelectedPage = ngpBusiness;
                        break;

                    case BusinessMenu.UserData:
                        ngfMain.SelectedPage = ngpUserData;
                        break;

                    case BusinessMenu.Auditing:
                        ngfMain.SelectedPage = ngpAuditing;
                        break;

                    case BusinessMenu.DataQuery:
                        ngfMain.SelectedPage = ngpDataQuery;
                        break;

                    case BusinessMenu.Report:
                        ngfMain.SelectedPage = ngpReport;
                        break;

                    case BusinessMenu.DataBussiness:
                        ngfMain.SelectedPage = ngpDataBussiness;
                        break;
                }
            }
            else
            {
                if (navigationPages.ContainsKey(mainMenuId))
                {
                    ngfMain.SelectedPage = navigationPages[mainMenuId];
                }
            }
        }

        /// <summary>
        /// 关闭窗体前提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("确认要退出本系统吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                    MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                this.Dispose();
                System.Environment.Exit(System.Environment.ExitCode);
            }
            else
            {
                e.Cancel = true;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 清除加载页面
        /// </summary>
        private void ClearPages()
        {
            ngpPersonalData.Controls.Clear();
            ngpDataAuditing.Controls.Clear();
            ngpDataAuditing.Controls.Clear();
            ngpBusiness.Controls.Clear();
            ngpUserData.Controls.Clear();
            ngpAuditing.Controls.Clear();
            ngpDataQuery.Controls.Clear();
            ngpReport.Controls.Clear();

            foreach (var keyValue in navigationPages)
            {
                keyValue.Value.Controls.Clear();

            }
        }

        /// <summary>
        /// 加载页面
        /// </summary>
        private void LoadPages()
        {
            int menuCountVisible = 0;
            foreach (WindowsUIButton windowsUIButton in upnlMain.Buttons)
            {
                byte mainMenuId = Convert.ToByte(windowsUIButton.Tag);
                if (mainMenuId == 0)
                {
                    continue;
                }
                if (mainMenuId > 0 && mainMenuId <= (byte)BusinessMenu.Max)
                {
                    BusinessMenu businessMenu = (BusinessMenu)mainMenuId;
                    /* 如果用户具有角色的处理工作流权限，第二项菜单“我的工作”也可见；如果具有数据填报审核的权限，则“数据填报审核”也可见 */
                    if (CurrentUser.Instance.CatalogMenuIds.ContainsKey(mainMenuId)
                        || (CurrentUser.Instance.Workflow && businessMenu == BusinessMenu.MyWork)
                        || (CurrentUser.Instance.Auditing && businessMenu == BusinessMenu.Auditing)
                        || ((CurrentUser.Instance.PersonalAduting || CurrentUser.Instance.GroupAduting || CurrentUser.Instance.InfoAduting) && businessMenu == BusinessMenu.DataAuditing)
                        || ((CurrentUser.Instance.PersonInfoQueried || CurrentUser.Instance.StatisticalDataQueried) && businessMenu == BusinessMenu.DataQuery))
                    {
                        menuCountVisible++;
                        windowsUIButton.Visible = true;
                        switch (businessMenu)
                        {
                            case BusinessMenu.PersonalData:
                                ucPersonalData = new PersonalDataControl();
                                ngpPersonalData.Controls.Add(ucPersonalData);
                                ucPersonalData.Dock = DockStyle.Fill;
                                break;

                            case BusinessMenu.DataAuditing:
                                ucDataAuditing = new DataAuditingControl();
                                ngpDataAuditing.Controls.Add(ucDataAuditing);
                                ucDataAuditing.Dock = DockStyle.Fill;
                                break;

                            case BusinessMenu.MyWork:
                                ucMyWork = new MyWorkUserControl();
                                ngpMyWork.Controls.Add(ucMyWork);
                                ucMyWork.Dock = DockStyle.Fill;
                                break;

                            case BusinessMenu.CommonBusiness:
                                ucCommonBusiness = new CommonBusinessControl()
                                {
                                    CustomWorkflowContract = customWorkflowContract,
                                    CustomWorkflowInstanceContract = customWorkflowInstanceContract
                                };
                                ngpBusiness.Controls.Add(ucCommonBusiness);
                                ucCommonBusiness.Dock = DockStyle.Fill;
                                break;

                            case BusinessMenu.UserData:
                                ucSumbittedData = new SumbittedDataUserControl()
                                {
                                    CustomDataContract = customDataContract,
                                    BusinessInstanceContract = businessInstanceContract,
                                    UserAccountContract = userAccountContract
                                };
                                ngpUserData.Controls.Add(ucSumbittedData);
                                ucSumbittedData.Dock = DockStyle.Fill;
                                break;

                            case BusinessMenu.Auditing:
                                ucAuditing = new AuditingUserControl();
                                ngpAuditing.Controls.Add(ucAuditing);
                                ucAuditing.Dock = DockStyle.Fill;
                                break;

                            case BusinessMenu.DataQuery:
                                ucQuery = new QueryUserControl()
                                {
                                    CustomGroupContract = customGroupContract,
                                    UserAccountContract = userAccountContract,
                                    UserTypeContract = userTypeContract,
                                    CustomEnumContract = customEnumContract,
                                    CustomDepartmentContract = customDepartmentContract,
                                    CustomQueyContract = customQueyContract,
                                    AssociatedDataFieldContract = associatedDataFieldContract,
                                    CustomTableContract = customTableContract,
                                    CustomDataFieldContract = customDataFieldContract,
                                    CustomViewContract = customViewContract,
                                };
                                ngpDataQuery.Controls.Add(ucQuery);
                                ucQuery.Dock = DockStyle.Fill;
                                break;

                            case BusinessMenu.Report:
                                ucReport = new ReportControl();
                                ngpReport.Controls.Add(ucReport);
                                ucReport.Dock = DockStyle.Fill;
                                break;

                            case BusinessMenu.DataBussiness:
                                break;
                        }
                    }
                    else
                    {
                        windowsUIButton.Visible = false;
                    }
                }
            }
            foreach (var keyValue in CurrentUser.Instance.CatalogMenuIds)
            {
                if (keyValue.Key > 0 && keyValue.Key <= (byte)BusinessMenu.Max)
                {
                    continue;
                }
                NavigationPage page = null;
                if (!navigationPages.ContainsKey(keyValue.Key))
                {
                    page = new NavigationPage();
                    ngfMain.Controls.Add(page);
                    ngfMain.Pages.Add(page);
                    navigationPages.Add(keyValue.Key, page);
                    CustomMenuInfo customMenuInfo = customMenuContract.GetCustomMenu(keyValue.Key);
                    if (customMenuInfo != null)
                    {
                        Image menuImage = null;
                        IconType iconType = (IconType)customMenuInfo.IconType;
                        switch (iconType)
                        {
                            case IconType.System:
                                menuImage = UserFileHelper.GetUserIcons(customMenuInfo.MenuIcon);
                                break;

                            case IconType.Custom:
                                if (!string.IsNullOrWhiteSpace(customMenuInfo.IconName))
                                {
                                    menuImage = UserFileHelper.GetMenuIcons(customMenuInfo.IconName);
                                }
                                break;
                        }
                        WindowsUIButton button = new WindowsUIButton()
                        {
                            Caption = customMenuInfo.MenuName,
                            Image = menuImage,
                            ImageLocation = DevExpress.XtraBars.Docking2010.ImageLocation.AboveText,
                            Style = ButtonStyle.PushButton,
                            IsLeft = false,
                            EnableImageTransparency = false,
                            Tag = keyValue.Key
                        };
                        upnlMain.Buttons.Add(button);
                    }
                    else
                    {
                        page = navigationPages[keyValue.Key];

                    }
                    ucUserBusiness = new UserBusinessControl();
                    ucUserBusiness.MenuType = keyValue.Key;
                    page.Controls.Add(ucUserBusiness);
                    ucUserBusiness.Dock = DockStyle.Fill;
                }
            }
        }

        #endregion
    }
}
