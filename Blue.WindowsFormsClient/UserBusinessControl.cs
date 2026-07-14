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
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;
using Blue.WindowsFormsClient.MyQueryModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient
{
    public partial class UserBusinessControl : UserControl
    {
        public byte MenuType
        {
            get;
            set;
        }

        public UserBusinessControl()
        {
            InitializeComponent();
        }

        private void UserBusinessControl_Load(object sender, EventArgs e)
        {
            if (CurrentUser.Instance.CatalogMenuIds.ContainsKey(MenuType))
            {
                IList<CustomMenuInfo> customMenuInfos = CurrentUser.Instance.CatalogMenuIds[MenuType];
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
                        nbcData.Groups.Add(navBarGroup);
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
                            BusinessControl businessControl = new BusinessControl()
                            {
                                Dock = DockStyle.Fill
                            };
                            pnlMain.Controls.Add(businessControl);
                            NavBarItem navBarItem = new NavBarItem()
                            {
                                Tag = extendedCustomBusinessInfo,
                                Caption = extendedCustomBusinessInfo.BusinessName,
                                LargeImage = bussinessImage
                            };
                            navBarItem.LinkClicked += (sd, arg) =>
                            {
                                
                            };
                            navBarGroup.ItemLinks.Add(navBarItem);
                        }
                    }
                }
            }
        }
    }
}
