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
using Blue.WindowsFormsClient.MyPersonDataModule;

namespace Blue.WindowsFormsClient
{
    public partial class PersonalDataControl : UserControl
    {
        #region 契约接口

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public PersonalDataControl()
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
        private void PersonalDataControl_Load(object sender, EventArgs e)
        {
            PersonalDataMainControl personalDataMainControl = new PersonalDataMainControl()
            {
                Dock = DockStyle.Fill
            };
            pnlMain.Controls.Add(personalDataMainControl);
            PersonalDataInstanceControl personalDataInstanceControl = new PersonalDataInstanceControl()
            {
                Dock = DockStyle.Fill,
                Visible = false,
                GoBack = () =>
                {
                    Visible = false;
                    personalDataMainControl.Visible = true;
                }
            };
            pnlMain.Controls.Add(personalDataInstanceControl);

            if (CurrentUser.Instance.CatalogMenuIds.ContainsKey((byte)BusinessMenu.PersonalData))
            {
                IList<CustomMenuInfo> customMenuInfos = CurrentUser.Instance.CatalogMenuIds[(byte)BusinessMenu.PersonalData];
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
                        ncAudting.Groups.Add(navBarGroup);
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
                            NavBarItem navBarItem = new NavBarItem()
                            {
                                Tag = extendedCustomBusinessInfo,
                                Caption = extendedCustomBusinessInfo.BusinessName,
                                LargeImage = bussinessImage
                            };
                            navBarItem.LinkClicked += (sd, arg) =>
                            {
                                ExtendedCustomBusinessInfo customBusinessInfo = (ExtendedCustomBusinessInfo)navBarItem.Tag;
                                personalDataMainControl.Visible = false;
                                personalDataInstanceControl.Visible = true;
                                personalDataInstanceControl.Title = extendedCustomBusinessInfo.BusinessName;
                                personalDataInstanceControl.ExtendedCustomBusinessInfo = extendedCustomBusinessInfo;
                                personalDataInstanceControl.LoadData();
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
