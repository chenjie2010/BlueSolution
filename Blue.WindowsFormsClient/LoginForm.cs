using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using AppFramework.Core;
using AppFramework.Core.ClientConfig;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.WCFContracts;

namespace Blue.WindowsFormsClient
{
    public partial class LoginForm : Form
    {
        #region 私有常量

        //用户图片的路径
        private const string DEFAULT_POHTO_PAHT = @"\Resources\Images\Login_Default_User.png";

        //系统管理员用户类型编号(创建数据库时用户类型默认的值)
        private const int ADMINISTRATOR_USER_TYPE_ID = 2;

        #endregion

        #region 私有变量

        private SplashScreenForm frmSplashScreen = null;
        private MainForm frmMain = null;
        private SystemMainForm frmSystemMainForm = null;

        private NetworkConnection networkConnection;
        private UserValidator userValidator;
        private AsynResult asynResult;
        private LoginedStep currentLoginedStep;
        private string userName = string.Empty;
        private string passowrd = string.Empty;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public LoginForm()
        {
            InitializeComponent();
            cmbUserName.Properties.AutoComplete = false;
            cmbUserName.Properties.ValidateOnEnterKey = false;
            ddbStatus.Tag = UserLogonState.Online;
            networkConnection = new NetworkConnection();
            userValidator = new UserValidator();
            asynResult = AsynResult.Succeed;
            currentLoginedStep = LoginedStep.TestConnection;
        }

        #endregion

        #region 窗体方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Load(object sender, EventArgs e)
        {
            cmbUserName.Focus();
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmOnline_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetLogonStateProperties(e.Item.ImageIndex, UserLogonState.Online);
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmLeave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetLogonStateProperties(e.Item.ImageIndex, UserLogonState.Leave);
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmBusy_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetLogonStateProperties(e.Item.ImageIndex, UserLogonState.Busy);
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmDoNotDisturb_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetLogonStateProperties(e.Item.ImageIndex, UserLogonState.DoNotDisturb);
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmHidden_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetLogonStateProperties(e.Item.ImageIndex, UserLogonState.Hiding);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            Logon();
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            cmbUserName.SelectedItem = null;
            cmbUserName.SelectedText = string.Empty;
            txtPassword.Text = string.Empty;
            chkAutoLogin.Checked = false;
            chkRemeber.Checked = false;
            ddbStatus.ImageIndex = 0;
            ddbStatus.Tag = UserLogonState.Online;
            cmbUserName.Focus();
        }

        /// <summary>
        /// 用户设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSetting_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UserSettingForm frmUserSetting = new UserSettingForm();
                frmUserSetting.RemoveUserListCallbackHandler = RemoveUserList;
                frmUserSetting.ConnectionSucess = btnLogin.Enabled;
                Cursor = Cursors.Default;
                frmUserSetting.ShowDialog();
                btnLogin.Enabled = frmUserSetting.ConnectionSucess;
                cmbUserName.Focus();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 在密码框里回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (btnLogin.Enabled)
                {
                    Logon();
                    e.Handled = true;// 指示 KeyPress 事件已处理，去掉 Windows 缺省的叮当声。
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                SendKeys.Send("{TAB}");// 发送“TAB”键，切换到下一控件。
                e.Handled = true;// 指示 KeyPress 事件已处理，去掉 Windows 缺省的叮当声。
            }
        }

        /// <summary>
        /// 选择用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbUserName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(Application.StartupPath);
                sb.Append(DEFAULT_POHTO_PAHT);

                peUser.Image = Image.FromFile(sb.ToString());

                if (cmbUserName.SelectedItem == null)
                {
                    ClearCurrentControlProperties();
                    return;
                }
                UserInfoConfig userConfig = cmbUserName.SelectedItem as UserInfoConfig;
                if (userConfig != null)
                {
                    chkRemeber.Checked = userConfig.RemeberPassword;
                    chkAutoLogin.Checked = userConfig.AutoLogon;
                    if (userConfig.RemeberPassword)
                    {
                        txtPassword.Text = userConfig.UserPassword;
                    }
                }
                else
                {
                    txtPassword.Text = string.Empty;
                    chkRemeber.Checked = false;
                    chkAutoLogin.Checked = false;
                }
            }
            catch { };
        }

        /// <summary>
        /// 显示界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_Shown(object sender, EventArgs e)
        {
            LoadUserConfig();
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LoginForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Dispose();
            System.Environment.Exit(System.Environment.ExitCode);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置登录状态
        /// </summary>
        /// <param name="imageIndex"></param>
        /// <param name="tag"></param>
        private void SetLogonStateProperties(int imageIndex, object tag)
        {
            ddbStatus.ImageIndex = imageIndex;
            ddbStatus.Tag = tag;
        }

        /// <summary>
        /// 登录
        /// </summary>
        private void Logon()
        {
            userName = cmbUserName.Text.Trim();
            passowrd = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(userName))
            {
                chkAutoLogin.Checked = false;
                cmbUserName.Focus();
                MessageBox.Show("用户名不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (string.IsNullOrWhiteSpace(passowrd))
            {
                chkAutoLogin.Checked = false;
                txtPassword.Focus();
                MessageBox.Show("密码不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            userName = StringConvertionHelper.ToDBC(userName);
            asynResult = AsynResult.Succeed;
            if (frmSplashScreen != null && !frmSplashScreen.IsDisposed)
            {
                frmSplashScreen.Dispose();
            }
            frmSplashScreen = new SplashScreenForm();
            frmSplashScreen.CancelLogin = false;
            frmSplashScreen.CancelToTestNetwork = new MethodInvoker(delegate ()
            {
                networkConnection.CancelTest = true;
            });
            currentLoginedStep = LoginedStep.TestConnection;

            this.Hide();
            Thread.Sleep(50);
            frmSplashScreen.Show();
            frmSplashScreen.BringToFront();

            bool state = true;
            AsynFlashCaller caller = new AsynFlashCaller(ProgressHanlder);
            IAsyncResult result = caller.BeginInvoke(out state, null, null);
            while (!result.IsCompleted)
            {
                Application.DoEvents();
                if (frmSplashScreen.CancelLogin)
                {
                    break;
                }
                Thread.Sleep(50);
            }
            caller.EndInvoke(out state, result);
            if (frmSplashScreen.CancelLogin)
            {
                asynResult = AsynResult.Cancel;
                frmSplashScreen.Close();
            }
            else
            {
                if (state)
                {
                    asynResult = AsynResult.Succeed;
                }
                else
                {
                    asynResult = AsynResult.Failed;
                    frmSplashScreen.Close();
                }
            }
            LogonCallback(asynResult, currentLoginedStep);
        }

        /// <summary>
        /// 处理线程
        /// </summary>
        private void ProgressHanlder(out bool state)
        {
            state = true;
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(LoginedStep));
            foreach (EnumItem enumItem in enumItems)
            {
                if (frmSplashScreen.CancelLogin || !state)
                {
                    break;
                }
                currentLoginedStep = (LoginedStep)enumItem.Value;
                frmSplashScreen.ProcessCommand(currentLoginedStep, null);
                try
                {
                    switch (currentLoginedStep)
                    {
                        case LoginedStep.TestConnection:
                            state = networkConnection.TestConnection(CurrentConfig.Instance.ServerAddress);
                            break;

                        case LoginedStep.CheckVersionConsistency:
                            state = userValidator.CheckVersionConsistency(CurrentConfig.Instance.ServerAddress, CryptographyHelper.Decrypt(AppSettingHelper.ClientVersion));
                            break;

                        case LoginedStep.CheckSystemTime:
                            state = userValidator.CheckSystemTime(CurrentConfig.Instance.ServerAddress);
                            break;

                        case LoginedStep.Validator:
                            state = userValidator.Validate(userName, passowrd);
                            break;
                    }
                }
                catch { }
            }
        }

        /// <summary>
        /// 回调函数
        /// </summary>
        /// <param name="asynResult"></param>
        /// <param name="logonStep"></param>
        private void LogonCallback(AsynResult asynResult, LoginedStep logonStep)
        {
            try
            {
                ICommonUtilContract commonUtilContract = CommonFactory.CreateCommonUtilContract();
                /* 版本验证 */
                //RegisterInfo registerInfo = commonUtilContract.GetRegisterInfo();
                //if (registerInfo == null)
                //{
                //    frmSplashScreen.Close();
                //    this.Show();
                //    Application.DoEvents();
                //    MessageBox.Show("注册信息读取失败，无法登录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //CurrentConfig.Instance.SoftwareVersion = registerInfo.SoftwareVersion;
                //switch (registerInfo.SoftwareVersion)
                //{
                //    case SoftwareVersion.UnKown:
                //        btnLogin.Enabled = CurrentConfig.Instance.State;
                //        this.Show();
                //        frmSplashScreen.Close();
                //        cmbUserName.Focus();
                //        Application.DoEvents();
                //        MessageBox.Show("未注册版本，请在服务器端注册！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        return;

                //    case SoftwareVersion.Trial:
                //        DateTime date = commonUtilContract.GetSystemDataTime("");
                //        int days = date.Subtract(registerInfo.RegisterTime).Days;
                //        if (days > AppSettingHelper.TrialDays)
                //        {
                //            btnLogin.Enabled = CurrentConfig.Instance.State;
                //            this.Show();
                //            cmbUserName.Focus();
                //            Application.DoEvents();
                //            MessageBox.Show("试用期已过，请安装正式版本！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //            return;
                //        }
                //        else
                //        {
                //            int left = AppSettingHelper.TrialDays - days + 1;
                //            MessageBox.Show(string.Format("试用期还剩下{0}天过期，请尽快安装正式版本。", left), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        }
                //        break;
                //}

                /* 登录验证 */
                switch (asynResult)
                {
                    case AsynResult.Cancel:
                        this.Show();
                        cmbUserName.Focus();
                        Application.DoEvents();
                        MessageBox.Show("登录操作已经取消。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case AsynResult.Failed:
                        chkAutoLogin.Checked = false;
                        UserValidator userValidator = new UserValidator();
                        switch (logonStep)
                        {
                            case LoginedStep.TestConnection:
                                userValidator.ChangeClientConfigInSysConfig(CurrentConfig.Instance.ServerAddress, false);
                                CurrentConfig.Instance.State = false;
                                btnLogin.Enabled = false;
                                this.Show();
                                cmbUserName.Focus();
                                Application.DoEvents();
                                MessageBox.Show("网络连接测试失败。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                            case LoginedStep.CheckSystemTime:
                                DateTime dateTime = commonUtilContract.GetSystemDataTime(string.Empty);
                                /* 清空通道缓存,因为当前计算机时间用户可能会在提示之后做出更改 */
                                ChannelFactoryCreator.ClearChannelFactories();
                                int span = dateTime.Subtract(DateTime.Now).Minutes;
                                if (span < 0 || span > 2)
                                {
                                    this.Show();
                                    cmbUserName.Focus();
                                    Application.DoEvents();
                                    MessageBox.Show(string.Format("你需要修改当前计算机的系统时间，以保持与服务器系统时间误差在1分钟内。(当前服务器系统时间为：{0})", dateTime),
                                        "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    MessageBox.Show("当前计算机的系统时间与服务器系统时间不一致的问题已解决，请重新登录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                break;

                            case LoginedStep.CheckVersionConsistency:
                                btnLogin.Enabled = false;
                                this.Show();
                                cmbUserName.Focus();
                                Application.DoEvents();
                                MessageBox.Show("您使用的客户端版本过低，请安装最新版本使用。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                            case LoginedStep.Validator:
                                this.Show();
                                cmbUserName.Focus();
                                Application.DoEvents();
                                MessageBox.Show("用户名或是密码验证失败。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;

                            case LoginedStep.Load:
                                break;
                        }
                        break;

                    case AsynResult.Succeed:
                        //方法一
                        string userName = cmbUserName.Text.Trim();
                        string password = txtPassword.Text.Trim();
                        UserInfoConfig userConfig = new UserInfoConfig(userName, CryptographyHelper.Encrypt(password), chkRemeber.Checked,
                                    chkAutoLogin.Checked, (UserLogonState)ddbStatus.Tag);
                        string defaultUserName = UserInfoConfigHelper.GetDefaultValue();
                        if (string.IsNullOrWhiteSpace(defaultUserName))
                        {
                            UserInfoConfigHelper.RemoveConfigInfo(userConfig);
                            UserInfoConfigHelper.AddConfigInfo(userConfig);
                            UserInfoConfigHelper.ModifyDefaultValueOfSystemConfigInfo(userName);
                        }
                        else
                        {
                            if (!defaultUserName.Equals(userConfig.UserName))
                            {
                                UserInfoConfigHelper.RemoveConfigInfo(userConfig);
                                UserInfoConfigHelper.AddConfigInfo(userConfig);
                                UserInfoConfigHelper.ModifyDefaultValueOfSystemConfigInfo(userConfig.UserName);
                            }
                            else
                            {
                                UserInfoConfig config = UserInfoConfigHelper.GetConfigInfo();
                                if (config != null && !config.Equals(userConfig))
                                {
                                    UserInfoConfigHelper.ModifyConfigInfo(userConfig);
                                }
                            }
                        }


                        //LoggingHelper.WriteLog(string.Format("用户登录：{0}, {1}, 时间：{2}", CurrentUser.Instance.UserName, CurrentUser.Instance.UserActualName, DateTime.Now));
                        LogHelper.WriteLog(LogTitle.Login, LogAction.None, LogLevel.Info, string.Format("用户登录：{0}, {1}, 时间：{2}", CurrentUser.Instance.UserName, CurrentUser.Instance.UserActualName, DateTime.Now));
                        Application.DoEvents();

                        if (CurrentUser.Instance.UserProperty == UserProperty.Administrator && CurrentUser.Instance.UserTypeId == ADMINISTRATOR_USER_TYPE_ID)
                        {
                            frmSystemMainForm = new SystemMainForm();
                            frmSplashScreen.Close();
                            Thread.Sleep(50);
                            frmSystemMainForm.Show();
                            frmSystemMainForm.BringToFront();

                        }
                        else
                        {
                            frmMain = new MainForm();
                            frmSplashScreen.Close();
                            Thread.Sleep(50);
                            frmMain.Show();
                            frmMain.BringToFront();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                frmSplashScreen.Close();
                this.Show();
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(ex);
            }
        }

        /// <summary>
        /// 移除选中的用户
        /// </summary>
        /// <param name="userConfigs"></param>
        private void RemoveUserList(IList<UserInfoConfig> userConfigs)
        {
            foreach (UserInfoConfig userConfig in userConfigs)
            {
                for (int i = cmbUserName.Properties.Items.Count - 1; i >= 0; i--)
                {
                    UserInfoConfig currentUserConfig = cmbUserName.Properties.Items[i] as UserInfoConfig;
                    if (userConfig.UserName.Equals(currentUserConfig.UserName))
                    {
                        cmbUserName.Properties.Items.RemoveAt(i);
                        break;
                    }
                }
            }
            if (cmbUserName.Properties.Items.Count > 0)
            {
                cmbUserName.SelectedIndex = 0;
            }
            else
            {
                cmbUserName.SelectedText = string.Empty;
                txtPassword.Text = string.Empty;
            }
        }

        /// <summary>
        /// 加载用户登录的配置信息
        /// </summary>
        private void LoadUserConfig()
        {
            IList<UserInfoConfig> userConfigs = UserInfoConfigHelper.GetConfigInfoList();
            string userName = string.Empty;
            if (userConfigs.Count > 0)
            {
                userName = UserInfoConfigHelper.GetDefaultValue();
                int selectedIndex = -1;
                if (userConfigs.Count > 0)
                {
                    for (int i = userConfigs.Count - 1; i >= 0; i--)
                    {
                        userConfigs[i].UserPassword = CryptographyHelper.Decrypt(userConfigs[i].UserPassword);
                        cmbUserName.Properties.Items.Add(userConfigs[i]);
                        if (selectedIndex < 0 && userConfigs[i].UserName.Equals(userName))
                        {
                            selectedIndex = userConfigs.Count - i - 1;
                        }
                    }
                    cmbUserName.SelectedIndex = selectedIndex >= 0 ? selectedIndex : 0;
                    if (selectedIndex >= 0)
                    {
                        int index = userConfigs.FindIndex(userConfig => userConfig.UserName.Equals(userName));
                        if (index >= 0)
                        {
                            if (userConfigs[index].RemeberPassword)
                            {
                                txtPassword.Text = userConfigs[index].UserPassword;
                            }
                            chkRemeber.Checked = userConfigs[index].RemeberPassword;
                            chkAutoLogin.Checked = userConfigs[index].AutoLogon;
                            if (userConfigs[index].AutoLogon)
                            {
                                Logon();
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 清除控件的值
        /// </summary>
        private void ClearCurrentControlProperties()
        {
            cmbUserName.SelectedItem = null;
            cmbUserName.SelectedText = string.Empty;
            txtPassword.Text = string.Empty;
            chkRemeber.Checked = false;
            chkAutoLogin.Checked = false;

            cmbUserName.Focus();
        }

        #endregion        
    }
}
