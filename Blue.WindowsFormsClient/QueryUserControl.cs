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
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;
using Blue.WindowsFormsClient.MyQueryModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;

namespace Blue.WindowsFormsClient
{
    public partial class QueryUserControl : UserControl
    {
        #region 属性

        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 用户契约
        /// </summary>
        public IUserAccountContract UserAccountContract
        {
            set; get;
        }

        /// <summary>
        /// 枚举类型契约
        /// </summary>
        public ICustomEnumContract CustomEnumContract
        {
            set; get;
        }

        /// <summary>
        /// 用户类型契约
        /// </summary>
        public IUserTypeContract UserTypeContract
        {
            get; set;
        }

        /// <summary>
        /// 单位契约
        /// </summary>
        public ICustomDepartmentContract CustomDepartmentContract
        {
            set; get;
        }

        /// <summary>
        /// 查询契约
        /// </summary>
        public  ICustomQueyContract CustomQueyContract
        {
            get; set;
        }

        /// <summary>
        /// 关联字段契约
        /// </summary>
        public IAssociatedDataFieldContract AssociatedDataFieldContract
        {
            set; get;
        }

        /// <summary>
        /// 自定义表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get;
            set;
        }

        /// <summary>
        /// 自定义视图契约
        /// </summary>
        public ICustomViewContract CustomViewContract
        {
            get;
            set;
        }

        /// <summary>
        /// 字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            set; get;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public QueryUserControl()
        {
            InitializeComponent();
        }

        #endregion

        #region 控件加载方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryUserControl_Load(object sender, EventArgs e)
        {
            QueryMainPanelControl queryMainPanelControl = new QueryMainPanelControl()
            {
                Dock = DockStyle.Fill
            };
            pnlMain.Controls.Add(queryMainPanelControl);
            /* 1. 统计数据查询 */
            if (CurrentUser.Instance.StatisticalDataQueried)
            {
                NavBarGroup navBarGroup = new NavBarGroup()
                {
                    Caption = "通用数据查询",
                    GroupStyle = NavBarGroupStyle.LargeIconsText,
                    GroupCaptionUseImage = NavBarImage.Large,
                    Expanded = true,
                    SmallImageIndex = 0
                };
                ncQuery.Groups.Add(navBarGroup);
                StatisticalQueryControl statisticalQueryControl = new StatisticalQueryControl()
                {
                    Name = "StatisticalQuery",
                    Dock = DockStyle.Fill
                };
                pnlMain.Controls.Add(statisticalQueryControl);
                NavBarItem navBarItem = new NavBarItem()
                {
                    Caption = UserEnumHelper.GetEnumText(MenuSubAuthority.StatisticsQuery),
                    LargeImageIndex = 0
                };
                navBarItem.LinkClicked += (sd, arg) =>
                {
                    foreach (Control control in pnlMain.Controls)
                    {
                        control.Visible = control.Name.Equals("StatisticalQuery") ? true : false;
                    }
                };
                navBarGroup.ItemLinks.Add(navBarItem);
            }
            if (CurrentUser.Instance.CatalogMenuIds.ContainsKey((byte)BusinessMenu.DataQuery))
            {
                IList<CustomMenuInfo> customMenuInfos = CurrentUser.Instance.CatalogMenuIds[(byte)BusinessMenu.DataQuery];
                if (customMenuInfos != null && customMenuInfos.Count > 0)
                {                    
                    foreach (CustomMenuInfo customMenuInfo in customMenuInfos)
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
                        NavBarGroup navBarGroup = new NavBarGroup()
                        {
                            Caption = customMenuInfo.MenuName,
                            GroupStyle = NavBarGroupStyle.LargeIconsText,
                            GroupCaptionUseImage = NavBarImage.Large,
                            LargeImage = menuImage
                        };
                        ncQuery.Groups.Add(navBarGroup);
                        IList<ExtendedCustomBusinessInfo> extendedCustomBusinessInfos = CurrentUser.Instance.ExtendedCustomBusinessInfos[customMenuInfo.MenuId];
                        foreach (ExtendedCustomBusinessInfo extendedCustomBusinessInfo in extendedCustomBusinessInfos)
                        {
                            Image bussinessImage = null;
                            IconType bussinessIconType = (IconType)extendedCustomBusinessInfo.IconType;
                            switch (iconType)
                            {
                                case IconType.System:
                                    bussinessImage = UserFileHelper.GetUserIcons(extendedCustomBusinessInfo.BusinessIcon);
                                    break;

                                case IconType.Custom:
                                    if (!string.IsNullOrWhiteSpace(extendedCustomBusinessInfo.IconName))
                                    {
                                        bussinessImage = UserFileHelper.GetBusinessIcons(customMenuInfo.IconName);
                                    }
                                    break;
                            }
                            BusinessMenu businessMenu = (BusinessMenu)extendedCustomBusinessInfo.BusinessMenu;
                            if (businessMenu == BusinessMenu.DataQuery)
                            {
                                QueryItemControl queryItemControl = new QueryItemControl()
                                {
                                    Tag = extendedCustomBusinessInfo,
                                    ExtendedCustomBusinessInfo = extendedCustomBusinessInfo,
                                    CustomGroupContract = CustomGroupContract,
                                    UserAccountContract = UserAccountContract,
                                    CustomQueyContract = CustomQueyContract,
                                    UserTypeContract = UserTypeContract,
                                    CustomEnumContract = CustomEnumContract,
                                    CustomDepartmentContract = CustomDepartmentContract,
                                    AssociatedDataFieldContract = AssociatedDataFieldContract,
                                    CustomTableContract = CustomTableContract,
                                    CustomViewContract = CustomViewContract,
                                    CustomDataFieldContract = CustomDataFieldContract,
                                    RowCount = 3,
                                    BackVisible = true,
                                    MaxmizeVisible = true,
                                    Dock = DockStyle.Fill,
                                    GoBack = () =>
                                    {
                                        foreach (Control control in pnlMain.Controls)
                                        {
                                            ExtendedCustomBusinessInfo info = control.Tag as ExtendedCustomBusinessInfo;
                                            if (info != null)
                                            {
                                                control.Visible = false;
                                            }
                                        }
                                        queryMainPanelControl.Visible = true;
                                    }
                                };
                                pnlMain.Controls.Add(queryItemControl);
                            }
                            NavBarItem navBarItem = new NavBarItem()
                            {
                                Tag = extendedCustomBusinessInfo,
                                Caption = extendedCustomBusinessInfo.BusinessName,
                                LargeImage = bussinessImage
                            };
                            navBarItem.LinkClicked += (sd, arg) =>
                            {
                                ExtendedCustomBusinessInfo customBusinessInfo = (ExtendedCustomBusinessInfo)navBarItem.Tag;
                                foreach (Control control in pnlMain.Controls)
                                {
                                    ExtendedCustomBusinessInfo info = control.Tag as ExtendedCustomBusinessInfo;
                                    control.Visible = ((info != null) && info.BusinessId == customBusinessInfo.BusinessId) ? true : false;
                                    if (control.Visible)
                                    {
                                        QueryItemControl queryItemControl = (QueryItemControl)control;
                                        queryItemControl.LoadDataFields();
                                    }
                                }
                            };
                            navBarGroup.ItemLinks.Add(navBarItem);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
