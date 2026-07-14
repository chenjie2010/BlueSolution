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
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WindowsFormsClient.MyReportModule;

namespace Blue.WindowsFormsClient
{
    public partial class ReportControl : UserControl
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomReportContract customReportContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReportControl()
        {
            InitializeComponent();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
        }

        #endregion

        #region 控件加载方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportControl_Load(object sender, EventArgs e)
        {
            ReportMainPanelControl reportMainPanelControl = new ReportMainPanelControl()
            {
                Dock = DockStyle.Fill
            };
            pnlMain.Controls.Add(reportMainPanelControl);
            /* 1. 报表查询 */            
            if (CurrentUser.Instance.CatalogMenuIds.ContainsKey((byte)BusinessMenu.Report))
            {
                IList<CustomMenuInfo> customMenuInfos = CurrentUser.Instance.CatalogMenuIds[(byte)BusinessMenu.Report];
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
                        ncReport.Groups.Add(navBarGroup);
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
                            if (businessMenu == BusinessMenu.Report)
                            {
                                ReportInstanceControl reportInstanceControl = new ReportInstanceControl()
                                {
                                    Tag = extendedCustomBusinessInfo,
                                    ExtendedCustomBusinessInfo = extendedCustomBusinessInfo,
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
                                        reportMainPanelControl.Visible = true;
                                    }
                                };
                                pnlMain.Controls.Add(reportInstanceControl);
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
