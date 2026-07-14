using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using AppFramework.Core.ClientConfig;
using Blue.WindowsFormsClient.Common;
using Blue.WindowsFormsClient.SystemManagementModule;
using Blue.WindowsFormsClient.DataConvertionModule;

namespace Blue.WindowsFormsClient
{
    public partial class MainPageControl : DevExpress.XtraEditors.XtraUserControl
    {
        #region 属性

        /// <summary>
        /// 用户契约
        /// </summary>
        public IUserAccountContract UserAccountContract
        {
            get;
            set;
        }

        /// <summary>
        /// 系统契约
        /// </summary>
        public ISystemPrvoiderContract SystemPrvoiderContract
        {
            get;
            set;
        }

        /// <summary>
        /// 刷新界面
        /// </summary>
        public RefreshFormDelegate RefreshForm
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public MainPageControl()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体和控件的方法              

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainPageControl_Load(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                lblUserNameValue.Text = CurrentUser.Instance.UserName;
                lblUserActualNameValue.Text = CurrentUser.Instance.UserActualName;
                lblDepNameValue.Text = CurrentUser.Instance.DepName;
                lblServerAddressValue.Text = CurrentConfig.Instance.ServerAddress;
                lblLoginedTimeValue.Text = SystemPrvoiderContract.GetServerTime().ToString();

                byte[] data = UserAccountContract.DownLoadPhoto(CurrentUser.Instance.UserName);
                if (data != null)
                {
                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        Image img = Image.FromStream(ms);
                        peUser.Image = img;
                    }
                }
                if (CurrentUser.Instance.UserAdded || CurrentUser.Instance.UserEdited)
                {
                    hlnkUserManagement.Visible = true;
                }
                else
                {
                    hlnkUserManagement.Visible = false;
                }
                hlnkDataSwap.Visible = CurrentUser.Instance.DataSwap;
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
        private void hlnkMail_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                MyMailForm frmMyMail = new MyMailForm();
                frmMyMail.ShowDialog();
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 修改个人资料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkAccount_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                AccountForm frmAccount = new AccountForm();
                frmAccount.Show();
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 用户设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkSetting_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                ClientSettingForm frmClientSetting = new ClientSettingForm();
                frmClientSetting.ShowDialog();
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
        private void hlnkMessage_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UserMessageForm frmUserMessage = new UserMessageForm();
                frmUserMessage.ShowDialog(); Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                progressPanel.Show();
                Application.DoEvents();
                RefreshForm?.Invoke();
                progressPanel.Hide();
                Application.DoEvents();
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                progressPanel.Hide();
                Cursor = Cursors.Default;
                Application.DoEvents();
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkUserManagement_Click(object sender, EventArgs e)
        {
            UserNameForm frmUserName = new UserNameForm();
            frmUserName.ShowDialog();
        }

        /// <summary>
        /// 数据转表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkDataSwap_Click(object sender, EventArgs e)
        {
            DataRelationForm frmDataRelation = new DataRelationForm();
            frmDataRelation.ShowDialog();
        }

        #endregion

        #region 私有方法

        #endregion

        private void lnkPrintReport_Click(object sender, EventArgs e)
        {
            
        }
    }
}
