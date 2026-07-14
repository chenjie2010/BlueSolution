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
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.MyBusinessModule;

namespace Blue.WindowsFormsClient
{
    public partial class CommonBusinessControl : UserControl
    {
        #region 属性

        /// <summary>
        /// 工作流契约
        /// </summary>
        public ICustomWorkflowContract CustomWorkflowContract
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

        #endregion


        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonBusinessControl()
        {
            InitializeComponent();
        }

        #endregion

        private void CommonBusinessControl_Load(object sender, EventArgs e)
        {

            if (CurrentUser.Instance.CatalogMenuIds.ContainsKey((byte)BusinessMenu.CommonBusiness))
            {
                IList<CustomMenuInfo> customMenuInfos = CurrentUser.Instance.CatalogMenuIds[(byte)BusinessMenu.CommonBusiness];
                if (customMenuInfos != null && customMenuInfos.Count > 0)
                {
                    BusinessMainControl businessMainControl = new BusinessMainControl()
                    {
                        CustomWorkflowInstanceContract = CustomWorkflowInstanceContract,
                        Dock = DockStyle.Fill
                    };
                    pnlMain.Controls.Add(businessMainControl);
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
                            BusinessMenu menuBusinessType = (BusinessMenu)extendedCustomBusinessInfo.BusinessMenu;
                            switch (menuBusinessType)
                            {
                                case BusinessMenu.MyWork:
                                case BusinessMenu.CommonBusiness:
                                    CustomWorkflowInfo customWorkflowInfo = CustomWorkflowContract.GetModelInfo(extendedCustomBusinessInfo.WorkflowId);
                                    WorkflowType workflowType = (WorkflowType)customWorkflowInfo.WorkflowType;
                                    WorkflowBusinessInfo workflowBusinessInfo = new WorkflowBusinessInfo(extendedCustomBusinessInfo.BusinessId, extendedCustomBusinessInfo.BusinessName, extendedCustomBusinessInfo.BusinessIntro,
                                                workflowType, extendedCustomBusinessInfo.BusinessEnabled, extendedCustomBusinessInfo.InitializedDate, extendedCustomBusinessInfo.ExpiredDate,
                                                extendedCustomBusinessInfo.EnableHelp, customWorkflowInfo.WorkflowEnabled);
                                    switch (workflowType)
                                    {
                                        case WorkflowType.Common:
                                            MyBusinessModule.CommonBusinessControl commonBusinessControl = new MyBusinessModule.CommonBusinessControl()
                                            {
                                                ExtendedCustomBusinessInfo = extendedCustomBusinessInfo,
                                                CustomWorkflowInfo = customWorkflowInfo,
                                                CustomWorkflowInstanceContract = CustomWorkflowInstanceContract,
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
                                                    businessMainControl.Visible = true;
                                                }
                                            };
                                            commonBusinessControl.SetDataOnControls(workflowBusinessInfo);
                                            pnlMain.Controls.Add(commonBusinessControl);
                                            break;

                                        case WorkflowType.Single:
                                            SingleBusinessControl singleBusinessControl = new SingleBusinessControl()
                                            {
                                                CustomWorkflowInfo = customWorkflowInfo,
                                                ExtendedCustomBusinessInfo = extendedCustomBusinessInfo,
                                                CustomWorkflowInstanceContract = CustomWorkflowInstanceContract,
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
                                                    businessMainControl.Visible = true;
                                                }
                                            };
                                            singleBusinessControl.SetDataOnControls(workflowBusinessInfo);
                                            pnlMain.Controls.Add(singleBusinessControl);
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
    }
}
