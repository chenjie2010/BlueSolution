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
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;
using Blue.WindowsFormsClient.MyAuditingModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;

namespace Blue.WindowsFormsClient
{
    public partial class DataAuditingControl : UserControl
    {
        #region 契约接口

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataAuditingControl()
        {
            InitializeComponent();
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataAuditingControl_Load(object sender, EventArgs e)
        {
            LoadControlsData();
        }

        /// <summary>
        /// 个人数据审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiPersonalData_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                Control control = pnlMain.Tag as Control;
                if (nbiPersonalData.Tag == null)
                {
                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                    progressPanel.Show();
                    control.Visible = false;
                    PersonalAuditingControl personalAuditingControl = new PersonalAuditingControl()
                    {
                        Name = "PersonalAuditing",
                        Dock = DockStyle.Fill
                    };
                    nbiPersonalData.Tag = personalAuditingControl;
                    pnlMain.Tag = personalAuditingControl;
                    pnlMain.Controls.Add(personalAuditingControl);
                    progressPanel.Hide();
                    Cursor = Cursors.Default;
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    PersonalAuditingControl personalAuditingControl = nbiPersonalData.Tag as PersonalAuditingControl;
                    if (!control.Name.Equals(personalAuditingControl.Name))
                    {
                        control.Visible = false;
                        personalAuditingControl.Visible = true;
                        pnlMain.Tag = personalAuditingControl;
                    }
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 分组数据审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiGroup_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                Control control = pnlMain.Tag as Control;
                if (nbiGroup.Tag == null)
                {
                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                    progressPanel.Show();
                    control.Visible = false;
                    GroupAdutingControl groupAdutingControl = new GroupAdutingControl()
                    {
                        Name = "GroupAduting",
                        Dock = DockStyle.Fill
                    };
                    nbiGroup.Tag = groupAdutingControl;
                    pnlMain.Tag = groupAdutingControl;
                    pnlMain.Controls.Add(groupAdutingControl);
                    progressPanel.Hide();
                    Cursor = Cursors.Default;
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    GroupAdutingControl groupAdutingControl = nbiGroup.Tag as GroupAdutingControl;
                    if (!control.Name.Equals(groupAdutingControl.Name))
                    {
                        control.Visible = false;
                        groupAdutingControl.Visible = true;
                        pnlMain.Tag = groupAdutingControl;
                    }
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 待初审
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiInfoAuditing_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowPersonalDataAuditingControl(nbiInfoAuditing, InfoStatus.InfoAuditing, "InfoAuditing");
        }

        /// <summary>
        /// 待分配
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiInfoAllocating_LinkPressed(object sender, NavBarLinkEventArgs e)
        {
            ShowPersonalDataAuditingControl(nbiInfoAllocating, InfoStatus.InfoAllocating, "InfoAllocating");
        }

        /// <summary>
        /// 更新待终审
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiInfoAudited_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            ShowPersonalDataAuditingControl(nbiInfoAudited, InfoStatus.InfoAudited, "InfoAudited");
        }

        /// <summary>
        /// 审核日志
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiInfoAuditedLog_LinkClicked(object sender, NavBarLinkEventArgs e)
        {
            try
            {
                Control control = pnlMain.Tag as Control;
                if (nbiInfoAuditedLog.Tag == null)
                {
                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                    progressPanel.Show();
                    control.Visible = false;
                    DataAuditingLogControl dataAuditingLogControl = new DataAuditingLogControl()
                    {
                        Name = "DataAuditingLogControl",
                        Dock = DockStyle.Fill
                    };
                    nbiInfoAuditedLog.Tag = dataAuditingLogControl;
                    pnlMain.Tag = dataAuditingLogControl;
                    pnlMain.Controls.Add(dataAuditingLogControl);
                    progressPanel.Hide();
                    Cursor = Cursors.Default;
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    DataAuditingLogControl dataAuditingLogControl = nbiInfoAuditedLog.Tag as DataAuditingLogControl;
                    if (!control.Name.Equals(dataAuditingLogControl.Name))
                    {
                        control.Visible = false;
                        dataAuditingLogControl.Visible = true;
                        pnlMain.Tag = dataAuditingLogControl;
                    }
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }        

        /// <summary>
        /// 统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiInfoStatistics_LinkClicked(object sender, NavBarLinkEventArgs e)
        {

        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 刷新控件数据
        /// </summary>
        public void ResreshControlsData()
        {
            pnlMain.Controls.Clear();
            LoadControlsData();
        }

        #endregion        

        #region 私有方法

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="navBarItem"></param>
        /// <param name="infoStatus"></param>
        /// <param name="controlName"></param>
        private void ShowPersonalDataAuditingControl(NavBarItem navBarItem, InfoStatus infoStatus, string controlName)
        {
            try
            {
                Control control = pnlMain.Tag as Control;
                if (navBarItem.Tag == null)
                {
                    Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                    progressPanel.Show();
                    control.Visible = false;
                    PersonalDataAuditingControl dataAuditingControl = new PersonalDataAuditingControl()
                    {
                        CurrentInfoStatus = infoStatus,
                        BussinessName = navBarItem.Caption,
                        Name = controlName,
                        Dock = DockStyle.Fill
                    };
                    navBarItem.Tag = dataAuditingControl;
                    pnlMain.Tag = dataAuditingControl;
                    pnlMain.Controls.Add(dataAuditingControl);
                    progressPanel.Hide();
                    Cursor = Cursors.Default;
                }
                else
                {
                    Cursor = Cursors.WaitCursor;
                    PersonalDataAuditingControl personalDataAuditingControl = navBarItem.Tag as PersonalDataAuditingControl;
                    if (!control.Name.Equals(personalDataAuditingControl.Name))
                    {
                        control.Visible = false;
                        personalDataAuditingControl.Visible = true;
                        pnlMain.Tag = personalDataAuditingControl;
                    }
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }            
        }

        /// <summary>
        /// 加载控件数据
        /// </summary>
        private void LoadControlsData()
        {
            /* 0. 主界面 */
            AuditingMainPanelControl auditingMainPanelControl = new AuditingMainPanelControl()
            {
                Dock = DockStyle.Fill
            };
            pnlMain.Tag = auditingMainPanelControl;
            pnlMain.Controls.Add(auditingMainPanelControl);

            /* 1. 通用数据审核 */
            if (CurrentUser.Instance.PersonalAduting || CurrentUser.Instance.GroupAduting)
            {
                nbgAuditing.Visible = true;
                nbiPersonalData.Visible = CurrentUser.Instance.PersonalAduting;
                nbiGroup.Visible = CurrentUser.Instance.GroupAduting;
            }
            else
            {
                nbgAuditing.Visible = false;
            }

            /* 2. 个人信息更新申请 */
            nbgInfoUpdated.Visible = CurrentUser.Instance.InfoAduting;

            /* 3. 自定义数据审核 */
            if (CurrentUser.Instance.CatalogMenuIds.ContainsKey((byte)BusinessMenu.DataAuditing))
            {
                IList<CustomMenuInfo> customMenuInfos = CurrentUser.Instance.CatalogMenuIds[(byte)BusinessMenu.DataAuditing];
                if (customMenuInfos != null && customMenuInfos.Count > 0)
                {
                    AuditingInstanceControl auditingInstanceControl = new AuditingInstanceControl()
                    {
                        Visible = false,
                        Dock = DockStyle.Fill,
                        GoBack = () =>
                        {
                            Control control = pnlMain.Tag as Control;
                            control.Visible = false;
                            pnlMain.Tag = auditingMainPanelControl;
                            auditingMainPanelControl.Visible = true;
                        }
                    };
                    pnlMain.Controls.Add(auditingInstanceControl);
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
                        nbcAuditing.Groups.Add(navBarGroup);
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
                                Control control = pnlMain.Tag as Control;
                                if (control == null || !auditingInstanceControl.Name.Equals(control.Name))
                                {
                                    if (control != null)
                                    {
                                        control.Visible = false;
                                    }
                                    pnlMain.Tag = auditingInstanceControl;
                                    auditingInstanceControl.Visible = true;
                                }
                                auditingInstanceControl.ExtendedCustomBusinessInfo = extendedCustomBusinessInfo;
                                //auditingInstanceControl.LoadDataFields();
                            };
                            navBarGroup.ItemLinks.Add(navBarItem);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 切换面板
        /// </summary>
        /// <param name="panelName"></param>
        private void SwitchPanel(string panelName)
        {
            foreach (Control control in pnlMain.Controls)
            {
                control.Visible = control.Name.Equals("PersonalAuditing") ? true : false;
            }
        }


        #endregion        
    }
}
