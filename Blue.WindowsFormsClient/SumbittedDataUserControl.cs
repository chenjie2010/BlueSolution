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
using DevExpress.XtraEditors;
using AppFramework.Core;
using Blue.CustomLibrary;
using Blue.Model.UserModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.MyDataModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.DataFilledModule;
using Blue.WCFContracts.UserModule;

namespace Blue.WindowsFormsClient
{
    public partial class SumbittedDataUserControl : UserControl
    {
        #region 属性

        /// <summary>
        /// 数据填报契约
        /// </summary>
        public ICustomDataContract CustomDataContract
        {
            get; set;
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
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SumbittedDataUserControl()
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
        private void SumbittedDataUserControl_Load(object sender, EventArgs e)
        {
            if (CurrentUser.Instance.CatalogMenuIds.ContainsKey((byte)BusinessMenu.UserData))
            {
                IList<CustomMenuInfo> customMenuInfos = CurrentUser.Instance.CatalogMenuIds[(byte)BusinessMenu.UserData];
                if (customMenuInfos != null && customMenuInfos.Count > 0)
                {
                    DataMainPanelUserControl dataMainPanelUserControl = new DataMainPanelUserControl()
                    {
                        BusinessInstanceContract = BusinessInstanceContract,
                        Dock = DockStyle.Fill
                    };
                    pnlMain.Controls.Add(dataMainPanelUserControl);
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
                            BusinessMenu businessMenu = (BusinessMenu)extendedCustomBusinessInfo.BusinessMenu;
                            switch (businessMenu)
                            {
                                case BusinessMenu.UserData:
                                    CustomDataInfo customDataInfo = CustomDataContract.GetModelInfo(extendedCustomBusinessInfo.DataId);
                                    DataFilledType dataFilledType = (DataFilledType)customDataInfo.DataFilledType;
                                    DataSumbittedInfo dataSumbittedInfo = new DataSumbittedInfo(extendedCustomBusinessInfo.BusinessId,extendedCustomBusinessInfo.BusinessName, extendedCustomBusinessInfo.BusinessIntro,
                                                dataFilledType, extendedCustomBusinessInfo.BusinessEnabled, extendedCustomBusinessInfo.InitializedDate, extendedCustomBusinessInfo.ExpiredDate, 
                                                extendedCustomBusinessInfo.EnableHelp, customDataInfo.ConditionEnabled);
                                    switch (dataFilledType)
                                    {
                                        case DataFilledType.Common:
                                            CommonDataUserControl commonDataUserControl = new CommonDataUserControl()
                                            {
                                                CustomDataInfo = customDataInfo,
                                                ExtendedCustomBusinessInfo = extendedCustomBusinessInfo,
                                                BusinessInstanceContract = BusinessInstanceContract,
                                                Tag = extendedCustomBusinessInfo,
                                                Dock = DockStyle.Fill,
                                                GoBack = () =>
                                                {
                                                    foreach (Control control in pnlMain.Controls)
                                                    {
                                                        ExtendedCustomBusinessInfo info = control.Tag as ExtendedCustomBusinessInfo;
                                                        if(info != null)
                                                        {
                                                            control.Visible = false;
                                                        }
                                                    }
                                                    dataMainPanelUserControl.Visible = true;
                                                }
                                            }; 
                                            commonDataUserControl.SetDataOnControls(dataSumbittedInfo);
                                            pnlMain.Controls.Add(commonDataUserControl);
                                            break;

                                        case DataFilledType.Single:
                                            SingleDataUserControl singleDataUserControl = new SingleDataUserControl()
                                            {
                                                CustomDataInfo = customDataInfo,
                                                ExtendedCustomBusinessInfo = extendedCustomBusinessInfo,
                                                BusinessInstanceContract = BusinessInstanceContract,
                                                UserAccountContract = UserAccountContract,
                                                Tag = extendedCustomBusinessInfo,
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
                                                    dataMainPanelUserControl.Visible = true;
                                                }
                                            };
                                            singleDataUserControl.SetDataOnControls(dataSumbittedInfo);
                                            pnlMain.Controls.Add(singleDataUserControl);
                                            break;
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
