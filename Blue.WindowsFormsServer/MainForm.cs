using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Configuration;
using System.ServiceProcess;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.BusinessLogic;

namespace Blue.WindowsFormsServer
{
    public partial class MainForm : Form
    {
        #region 私有变量

        private ServiceController controller;
        private readonly string serviceName;
        private string machineCode = string.Empty;
        private ProgressForm frmProgress;
        private bool connectedStatus = false; /* 数据库的连接状态 */
        private bool connectedStatusOnServer = false; /* 通过服务器端配置文件获得数据库的连接状态 */
        private bool connectedStatusOnWeb = false; /* 通过Web 端配置文件获得数据库的连接状态 */
        private WindowsServiceStep currentWindowsServiceStep = WindowsServiceStep.None;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            serviceName = AppSettingHelper.WindowsServiceName;
            controller = new ServiceController(serviceName);
            txtServerName.Text = controller.DisplayName;
            txtStatus.Text = GetStatus(controller.Status);
            if (controller.Status == ServiceControllerStatus.Running)
            {
                SetControlStates(WindowsServiceStep.Start);
                txtStartedTime.Text = AppSettingHelper.WindowsServiceStartTime;
                tmUpdate.Start();
            }
            else
            {
                SetControlStates(WindowsServiceStep.Stop);
                txtStartedTime.Text = string.Empty;
            }
            txtBackupTime.Text = AppSettingHelper.LastestBackupTime;
            chklInterface.Checked = AppSettingHelper.EnableInterface;
            /* 显示注册信息 */
            ShowUserRegisterInfo();
            txtServerVersion.Text = CryptographyHelper.Decrypt(AppSettingHelper.ServerVersion);
            txtReleasedDate.Text = AppSettingHelper.ServerReleasedDate;
            string versionConsistency = CryptographyHelper.Decrypt(AppSettingHelper.CompatibleClientVersions);
            if (!string.IsNullOrWhiteSpace(versionConsistency))
            {
                string[] serverInfos = versionConsistency.Split('|');
                foreach (string serverInfo in serverInfos)
                {
                    lbcClientVersions.Items.Add(serverInfo);
                }
            }
            meWarning.Text = "欢迎进入服务器端工具界面！";
            if (!string.IsNullOrWhiteSpace(AppSettingHelper.WindowsServiceException) || !string.IsNullOrWhiteSpace(AppSettingHelper.WebAPIException))
            {
                meException.Text = string.Format("Windows服务异常信息：{0}；\r\n Web API 异常信息：{1}。", AppSettingHelper.WindowsServiceException, AppSettingHelper.WebAPIException);
            }
            else
            {
                meException.Text = string.Empty;
            }
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                string domainName = AppSettingHelper.DomainName;
                if (domainName.ToLower().Equals("scu"))
                {
                    lblCompany.Text = AppSettingHelper.ScuCompanyName;
                }
                else
                {
                    lblCompany.Text = AppSettingHelper.DefaultCompanyName;
                }
                if (!string.IsNullOrWhiteSpace(DataAccessHelper.DefaultConnectionString))
                {
                    bgwServerDataShow.RunWorkerAsync();
                }           
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 启动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (AppSettingHelper.EnableInterface != chklInterface.Checked)
                {
                    AppSettingHelper.EnableInterface = chklInterface.Checked;
                }
                AppSettingHelper.WindowsServiceStartTime = DateTime.Now.ToString();
                EnableWindowsService(WindowsServiceStep.Start);                
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 停止
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定关闭服务吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    AppSettingHelper.WindowsServiceStartTime = DateTime.Now.ToString();
                    EnableWindowsService(WindowsServiceStep.Stop);
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 重启
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRestart_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定重新启动服务吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    AppSettingHelper.WindowsServiceStartTime = DateTime.Now.ToString();
                    EnableWindowsService(WindowsServiceStep.Restart);
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExit_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            this.Close();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            {
                controller.Close();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 更新运行时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tmUpdate_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtStartedTime.Text.Trim()))
            {
                TimeSpan interval = DateTime.Now.Subtract(Convert.ToDateTime(txtStartedTime.Text.Trim()));
                txtRunningTime.Text = string.Format("{0}天{1}小时{2}分{3}秒", interval.Days, interval.Hours, interval.Minutes, interval.Seconds);
            }
        }

        /// <summary>
        /// 浏览 WEB 路径
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowser_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string directory = folderBrowser.SelectedPath;
                if (!string.IsNullOrWhiteSpace(directory))
                {
                    string appSettingFullFileName = AppSettingHelper.GetAppSettingFullFileName(directory);
                    if (File.Exists(appSettingFullFileName))
                    {
                        AppSettingHelper.WebDirectory = directory;
                        txtPath.Text = directory;
                        string keyName = AppSettingHelper.GetAppSettingByName(appSettingFullFileName, AppSettingHelper.DomainNameCaption);
                        txtKeyName.Text = keyName;
                        AppSettingHelper.DomainName = keyName;
                    }
                    else
                    {
                        MessageBox.Show(string.Format("WEB目录（{0}）不正确，请重新设置！", directory), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("WEB目录不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }
            
        /// <summary>
        /// 保存关键字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnKeyName_Click(object sender, EventArgs e)
        {
            try
            {
                string keyName = txtKeyName.Text.Trim();
                if (string.IsNullOrWhiteSpace(keyName))
                {
                    txtKeyName.Focus();
                    MessageBox.Show("关键字名称不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                } 
                AppSettingHelper.DomainName = keyName;
                if (!string.IsNullOrWhiteSpace(AppSettingHelper.WebDirectory))
                {                    
                    /* 在WEB程序的配置文件中保存关键字 */
                    string appSettingFullFileName = AppSettingHelper.GetAppSettingFullFileName(AppSettingHelper.WebDirectory);
                    if (File.Exists(appSettingFullFileName))
                    {
                        AppSettingHelper.SetAppSettingByName(appSettingFullFileName, AppSettingHelper.DomainNameCaption, keyName);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("WEB目录（{0}）不正确，请重新设置！", AppSettingHelper.WebDirectory), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format(" 保存失败：{0}！", ex.Message), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 保存数据库连接设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSetting_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(address))
            {
                txtAddress.Focus();
                MessageBox.Show("数据库地址不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(userName))
            {
                txtUserName.Focus();
                MessageBox.Show("用户名不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                txtPassword.Focus();
                MessageBox.Show("密码不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                StringBuilder sb = new StringBuilder();
                /* 1. 更新服务器端数据库连接 */
                bool resultOnSever = DataAccessHelper.UpdateConnectionStrings(address, userName, password, DatabaseType.SQLServer);
                if (resultOnSever)
                {
                    sb.Append("服务器端设置保存成功；");
                }
                else
                {
                    sb.Append("服务器端设置保存失败（建议重新安装）；");
                }

                /* 2. 更新 Web 端数据库连接 */
                if (!string.IsNullOrWhiteSpace(AppSettingHelper.WebDirectory))
                {
                    string dataAccessFullFileName = DataAccessHelper.GetDataAccessFullFileName(AppSettingHelper.WebDirectory);
                    bool resultOnWeb = DataAccessHelper.UpdateConnectionStrings(dataAccessFullFileName, address, userName, password, DatabaseType.SQLServer);
                    if (resultOnWeb)
                    {
                        sb.Append("WEB 端设置保存成功。");
                    }
                    else
                    {
                        sb.Append("WEB 端设置保存失败（建议重新安装）。");
                    }
                }
                else
                {
                    sb.Append("WEB 安装目录尚未配置。");
                }
                txtConnectionStatus.Text =sb.ToString();
                Cursor = Cursors.Default;
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageBox.Show("数据库连接设置保存错误！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestDatabase_Click(object sender, EventArgs e)
        {
            string address = txtAddress.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();

            if (string.IsNullOrWhiteSpace(address))
            {
                txtAddress.Focus();
                MessageBox.Show("数据库地址不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(userName))
            {
                txtUserName.Focus();
                MessageBox.Show("用户名不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(address))
            {
                txtPassword.Focus();
                MessageBox.Show("密码不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            connectedStatus = false;
            connectedStatusOnServer = false;
            connectedStatusOnWeb = false;
            try
            {
                if (!bgwConnection.CancellationPending)
                {
                    bgwConnection.RunWorkerAsync();
                    frmProgress = new ProgressForm();
                    frmProgress.CancelToTest = delegate ()
                    {
                        bgwConnection.CancelAsync();
                        txtConnectionStatus.Text = "测试数据库连接操作取消！";
                    };
                    frmProgress.Text = "正在测试数据库连接，请稍后.......";
                    frmProgress.ShowDialog();
                }
                else
                {
                    MessageBox.Show("取消测试数据库链接正在进行中，请稍后重试！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception exception)
            {
                bgwConnection.CancelAsync();
                CloseProgressForm(frmProgress);
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 测试数据库连接操作后台线程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwConnection_DoWork(object sender, DoWorkEventArgs e)
        {
            string address = txtAddress.Text.Trim();
            string userName = txtUserName.Text.Trim();
            string password = txtPassword.Text.Trim();
            try
            {
                connectedStatus = CommonBusinessHelper.TestDatabaseConnection(address, userName, password);
            }
            catch
            {
                CloseProgressForm(frmProgress);
                bgwConnection.CancelAsync();
            }
        }

        /// <summary>
        /// 测试数据库连接完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwConnection_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CloseProgressForm(frmProgress);
            if (connectedStatus)
            {
                txtConnectionStatus.Text = "数据库连接测试成功！";
            }
            else
            {
                MessageBox.Show("数据库连接测试失败！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 显示服务器端界面的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwServerDataShow_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                /* 1. 设置 Web 端目录和数据自动备份目录 */
                txtPath.Text = AppSettingHelper.WebDirectory;
                txtKeyName.Text = AppSettingHelper.DomainName;
                txtBackupDir.Text = AppSettingHelper.DataBackupDirectory;

                if (!string.IsNullOrWhiteSpace(AppSettingHelper.WebDirectory))
                {
                    /* 2. 从 Web 端的配置文件获取关键字 */
                    string appSettingFullFileName = AppSettingHelper.GetAppSettingFullFileName(AppSettingHelper.WebDirectory);
                    if (File.Exists(appSettingFullFileName))
                    {
                        string webKeyName = AppSettingHelper.GetAppSettingByName(appSettingFullFileName, AppSettingHelper.DomainNameCaption);
                        if (!webKeyName.Equals(AppSettingHelper.DomainName))
                        {
                            AppSettingHelper.DomainName = webKeyName;
                            txtKeyName.Text = webKeyName;
                        }
                    }

                    /* 3.1 根据 Web 端的数据库配置文件测试数据库连接 */
                    string dataAccessFullFileName = DataAccessHelper.GetDataAccessFullFileName(AppSettingHelper.WebDirectory);
                    if (File.Exists(dataAccessFullFileName))
                    {
                        string connectionString = DataAccessHelper.GetDefaultConnectionStringByFile(dataAccessFullFileName);
                        connectedStatusOnWeb = CommonBusinessHelper.TestDatabaseConnection(connectionString);
                    }
                }

                /* 3.2 根据服务器端的数据库配置文件测试数据库连接 */
                connectedStatusOnServer = CommonBusinessHelper.TestDatabaseConnection();

            }
            catch (Exception exception)
            {
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwServerDataShow_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            StringBuilder sb = new StringBuilder();

            /* 1.1 更新服务器端数据库连接状态 */
            if (connectedStatusOnServer)
            {
                sb.Append("服务器端配置连接成功；");
            }
            else
            {
                sb.Append("服务器端配置连接失败（建议重新设置）；");
            }

            /* 1.2 更新 Web 端数据库连接状态 */
            if (!string.IsNullOrWhiteSpace(AppSettingHelper.WebDirectory))
            {
                if (connectedStatusOnWeb)
                {
                    sb.Append("Web 端配置连接成功。");
                }
                else
                {
                    sb.Append("Web 端配置连接失败（建议重新配置WEB目录）。");
                }
            }
            else
            {
                sb.Append("Web 端尚未配置。");
            }
            txtConnectionStatus.Text = sb.ToString();
        }

        /// <summary>
        /// 打开注册码界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (fpnlRegisteredCode.FlyoutPanelState.IsActive)
            {
                return;
            }
            meRegisteredCode.Text = string.Empty;
            fpnlRegisteredCode.ShowBeakForm(btnRegister.PointToScreen(new Point(0, 0)));
        }

        /// <summary>
        /// 关闭注册码界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpnlRegisteredCode_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            fpnlRegisteredCode.HideBeakForm();
        }

        /// <summary>
        /// 软件注册
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSumbit_Click(object sender, EventArgs e)
        {
            try
            {
                string serialNumber = meRegisteredCode.Text.Trim();
                if (string.IsNullOrWhiteSpace(serialNumber))
                {
                    meRegisteredCode.Focus();
                    MessageBox.Show("序列号不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!ValidateSerial(serialNumber))
                {
                    meRegisteredCode.Focus();
                    MessageBox.Show("序列号无法在该平台上注册！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                AppSettingHelper.ServerRegisterCode = serialNumber;
                ShowUserRegisterInfo();
                fpnlRegisteredCode.HideBeakForm();
                MessageBox.Show("注册成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                MessageBox.Show("对不起，注册失败！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 获取机器码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleRegister_OpenLink(object sender, DevExpress.XtraEditors.Controls.OpenLinkEventArgs e)
        {
            try
            {
                machineCode = string.Empty;
                if (!bgwMachineCode.CancellationPending)
                {
                    bgwMachineCode.RunWorkerAsync();
                    frmProgress = new ProgressForm();
                    frmProgress.CancelToTest = delegate ()
                    {
                        bgwMachineCode.CancelAsync();
                        txtConnectionStatus.Text = "获取机器码操作取消！";
                    };
                    frmProgress.Text = "正在获取机器码，请稍后.......";
                    frmProgress.ShowDialog();
                    frmProgress.BringToFront();
                }
                else
                {
                    MessageBox.Show("取消获取机器码操作正在进行中，请稍后重试！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                CloseProgressForm(frmProgress);
                bgwMachineCode.CancelAsync();
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 获取机器码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMachineCode_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                machineCode = CryptographyHelper.Encrypt(UserComputer.Instance.DiskId);
            }
            catch
            {
                CloseProgressForm(frmProgress);
                bgwMachineCode.CancelAsync();
            }
        }

        /// <summary>
        /// 显示机器码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwMachineCode_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            CloseProgressForm(frmProgress);
            if (string.IsNullOrWhiteSpace(machineCode))
            {
                MessageBox.Show("对不起，获得机器码失败！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            meMachineCode.Text = machineCode;
            if (fpnlMachineCode.FlyoutPanelState.IsActive)
            {
                return;
            }
            fpnlMachineCode.ShowBeakForm(hleRegister.PointToScreen(new Point(hleRegister.Width / 2, 0)));
        }
        
        /// <summary>
        /// 隐藏弹出机器码窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpnlMachineCode_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            //关闭按钮
            if (e.Button.GroupIndex == 0)
            {
                fpnlMachineCode.HideBeakForm();
            }
        }

        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwEnbaleWindowsService_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                connectedStatus = CommonBusinessHelper.TestDatabaseConnection();
            }
            catch
            {
                CloseProgressForm(frmProgress);
                bgwEnbaleWindowsService.CancelAsync();
            }
        }

        /// <summary>
        /// 测试完成后执行服务启动操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bgwEnbaleWindowsService_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                if (!connectedStatus)
                {
                    txtConnectionStatus.Text = " 数据库连接失败！";
                    if (currentWindowsServiceStep == WindowsServiceStep.Start)
                    {
                        Cursor = Cursors.Default;
                        CloseProgressForm(frmProgress);
                        meWarning.Text = "数据库连接测试失败，请设置后再启动服务!";
                        return;
                    }
                    else
                    {
                        currentWindowsServiceStep = WindowsServiceStep.Stop;
                    }
                }
                switch (currentWindowsServiceStep)
                {
                    case WindowsServiceStep.Start:
                        tmUpdate.Start();
                        meWarning.Text = "Windows 服务正在执行启动操作......";
                        ActionHandler(WindowsServiceStep.Start);
                        SetControlStates(WindowsServiceStep.Start);
                        meWarning.Text = "操作完成！";
                        break;

                    case WindowsServiceStep.Restart:
                        tmUpdate.Stop();
                        meWarning.Text = "Windows 服务正在执行重启操作......";
                        ActionHandler(WindowsServiceStep.Restart);
                        SetControlStates(WindowsServiceStep.Restart);
                        tmUpdate.Start();
                        meWarning.Text = "操作完成！";
                        break;

                    case WindowsServiceStep.Stop:
                        tmUpdate.Stop();
                        meWarning.Text = "Windows 服务正在执行停止操作......";
                        ActionHandler(WindowsServiceStep.Stop);
                        SetControlStates(WindowsServiceStep.Stop);
                        if (connectedStatus)
                        {
                            meWarning.Text = "操作完成！";
                        }
                        else
                        {
                            meWarning.Text = "数据库连接测试失败，服务已停止！";
                        }
                        break;
                }
                CloseProgressForm(frmProgress);
            }
            catch(Exception exception)
            {
                CloseProgressForm(frmProgress);
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }            
        }

        /// <summary>
        /// 提三方接口设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chklInterface_CheckedChanged(object sender, EventArgs e)
        {
            AppSettingHelper.EnableInterface = chklInterface.Checked;
        }

        /// <summary>
        /// 设置数据自动备份目录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBackupDir_Click(object sender, EventArgs e)
        {
            if (folderBrowser.ShowDialog() == DialogResult.OK)
            {
                string directory = folderBrowser.SelectedPath;
                if (!string.IsNullOrWhiteSpace(directory) && Directory.Exists(directory))
                {
                    AppSettingHelper.DataBackupDirectory = directory;
                    txtBackupDir.Text = directory;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(directory))
                    {
                        MessageBox.Show("数据自动备份目录不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!File.Exists(directory))
                    {
                        MessageBox.Show(string.Format("数据自动备份目录（{0}）不正确，请重新设置！", directory), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        #endregion

        #region 私有方法 


        /// <summary>
        /// 关闭进度条窗口
        /// </summary>
        /// <param name="form"></param>
        private void CloseProgressForm(ProgressForm form)
        {
            if(form != null && !form.IsDisposed)
            {
                form.Dispose();
            }
        }      

        /// <summary>
        /// 获得 DataAccess.config 路径
        /// </summary>
        /// <param name="directory"></param>
        /// <returns></returns>
        private string GetDataAceessPath(string directory)
        {           
            StringBuilder sb = new StringBuilder();
            sb.Append(directory);
            if (!directory.EndsWith(@"\"))
            {
                sb.Append(@"\");
            }
            sb.Append(@"EnterpriseLibrary\SystemConfigFiles\DataAccess.config");

            return sb.ToString();
        }


        /// <summary>
        /// 显示注册信息
        /// </summary>
        private void ShowUserRegisterInfo()
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                bool sucess = false;
                string registerInfo = CryptographyHelper.Decrypt(AppSettingHelper.ServerRegisterCode);
                if (!string.IsNullOrWhiteSpace(registerInfo))
                {                    
                    /* 硬盘序列号|单位名称|软件版本|关键字|注册日期 */
                    string[] infos = registerInfo.Split('|');
                    if (infos != null && infos.Length == 5 && infos[0].Equals(UserComputer.Instance.DiskId))
                    {
                        sb.AppendFormat("注册单位：{0}\r\n", infos[1]);
                        sb.AppendFormat("软件版本：{0}\r\n", UserEnumHelper.GetEnumText((SoftwareVersion)(Convert.ToInt32(infos[2]))));
                        sb.AppendFormat("关键字：{0}\r\n", infos[3]);
                        sb.AppendFormat("注册日期：{0}\r\n", Convert.ToDateTime(infos[4]).ToString("d"));                        
                        sucess = true;
                    }
                }
                if (!sucess)
                {
                    sb.Append("该服务端处于未注册状态，请注册！\r\n");                    
                }
            }
            catch
            {
                sb.Clear();
                sb.Append("该服务端注册状态异常，请重新注册或者与管理员联系！\r\n");
            }
            txtRegisteredInfo.Text = sb.ToString();
        }

        /// <summary>
        /// 验证序列号是否正确
        /// </summary>
        /// <param name="registerCode"></param>
        /// <returns></returns>
        private bool ValidateSerial(string registerCode)
        {
            bool result = false;

            try
            {
                string registerInfo = CryptographyHelper.Decrypt(registerCode);
                if (!string.IsNullOrWhiteSpace(registerInfo))
                {
                    /* 硬盘序列号|单位名称|软件版本|关键字|注册日期 */
                    string[] infos = registerInfo.Split('|');
                    if (infos[0].Equals(UserComputer.Instance.DiskId))
                    {
                        result = true;
                    }
                }
            }
            catch
            {
            }

            return result;
        }

        /// <summary>
        /// Windows 服务处理方法
        /// </summary>
        /// <param name="windowsServiceStep"></param>
        private void WindowsServiceHandler(WindowsServiceStep windowsServiceStep)
        {
            try
            {
                switch (windowsServiceStep)
                {
                    case WindowsServiceStep.Start:
                        Start(controller);
                        break;

                    case WindowsServiceStep.Stop:
                        Stop(controller);
                        break;

                    case WindowsServiceStep.Restart:
                        Stop(controller);
                        Start(controller);
                        break;
                }
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        /// <summary>
        /// 设置控件的状态
        /// </summary>
        /// <param name="windowsServiceStep"></param>
        private void SetControlStates(WindowsServiceStep windowsServiceStep)
        {
            switch (windowsServiceStep)
            {
                case WindowsServiceStep.Start:
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    txtStartedTime.Text = AppSettingHelper.WindowsServiceStartTime;
                    break;

                case WindowsServiceStep.Stop:
                    btnStart.Enabled = true;
                    btnStop.Enabled = false;
                    txtStartedTime.Text = string.Empty;
                    txtRunningTime.Text = "0天0小时0分0秒";
                    break;

                case WindowsServiceStep.Restart:
                    btnStart.Enabled = false;
                    btnStop.Enabled = true;
                    txtStartedTime.Text = AppSettingHelper.WindowsServiceStartTime;
                    break;
            }
        }

        /// <summary>
        /// 获得服务的名称
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        private string GetStatus(ServiceControllerStatus serviceControllerStatus)
        {
            string status = string.Empty;

            switch (serviceControllerStatus)
            {
                case ServiceControllerStatus.ContinuePending:
                    status = "即将继续......";
                    break;

                case ServiceControllerStatus.Paused:
                    status = "暂停";
                    break;

                case ServiceControllerStatus.PausePending:
                    status = "即将暂停......";
                    break;

                case ServiceControllerStatus.Running:
                    status = "正在运行";
                    break;

                case ServiceControllerStatus.StartPending:
                    status = "正在启动......";
                    break;

                case ServiceControllerStatus.Stopped:
                    status = "停止状态";
                    break;

                case ServiceControllerStatus.StopPending:
                    status = "正在停止......";
                    break;
            }

            return status;
        }

        /// <summary>
        /// 异步操作处理方法
        /// </summary>
        /// <param name="windowsServiceStep"></param>
        private void ActionHandler(WindowsServiceStep windowsServiceStep)
        {
            WindowsServiceHandlerDelegate windowsServiceHandlerDelegate = new WindowsServiceHandlerDelegate(WindowsServiceHandler);
            IAsyncResult result = windowsServiceHandlerDelegate.BeginInvoke(windowsServiceStep, null, null);
            while (!result.IsCompleted)
            {
                Application.DoEvents();
                txtStatus.Text = GetStatus(controller.Status);
                Thread.Sleep(20);
            }
            txtStatus.Text = GetStatus(controller.Status);
        }

        /// <summary>
        /// 开启服务
        /// </summary>
        /// <param name="serviceController"></param>
        private void Start(ServiceController serviceController)
        {
            if (serviceController.Status != ServiceControllerStatus.Running)
            {
                serviceController.Start();
                serviceController.WaitForStatus(ServiceControllerStatus.Running);
            }
        }

        /// <summary>
        /// 关闭服务
        /// </summary>
        /// <param name="serviceController"></param>
        private void Stop(ServiceController serviceController)
        {
            if (serviceController.Status != ServiceControllerStatus.Stopped)
            {
                serviceController.Stop();
                serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
            }
        }

        /// <summary>
        /// Windows 服务相关操作
        /// </summary>
        /// <param name="windowsServiceStep"></param>
        private void EnableWindowsService(WindowsServiceStep windowsServiceStep)
        {
            try
            {
                currentWindowsServiceStep = windowsServiceStep;
                if (!bgwEnbaleWindowsService.CancellationPending)
                {
                    bgwEnbaleWindowsService.RunWorkerAsync();
                    Cursor = Cursors.WaitCursor;
                    frmProgress = new ProgressForm();
                    frmProgress.CancelToTest = delegate ()
                     {
                         CloseProgressForm(frmProgress);
                         bgwEnbaleWindowsService.CancelAsync();
                         meWarning.Text = "操作已取消！";
                     };
                    frmProgress.Text = string.Format("正在{0}，请稍后.......", UserEnumHelper.GetEnumText(windowsServiceStep));
                    frmProgress.ShowDialog();
                    //frmProgress.BringToFront();
                }
                else
                {
                    MessageBox.Show(string.Format("取消{0}操作正在进行中，请稍后重试！", UserEnumHelper.GetEnumText(windowsServiceStep)), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                bgwEnbaleWindowsService.CancelAsync();
                CloseProgressForm(frmProgress);
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion        
    }
}
