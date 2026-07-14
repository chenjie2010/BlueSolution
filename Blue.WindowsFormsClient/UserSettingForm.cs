using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core.ClientConfig;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;

namespace Blue.WindowsFormsClient
{
    public partial class UserSettingForm : Form
    {
        #region 内部成员变量

        private bool _connectionSucess;
        private RemoveUserListCallbackHandler _removeUserListCallbackHandler;

        #endregion

        #region 私有变量

        /// <summary>
        /// 定时器计时
        /// </summary>
        private int testTimer = 0;

        /// <summary>
        /// 网络连接测试对象
        /// </summary>
        private readonly NetworkConnection networkConnection;

        /// <summary>
        /// 地址名称发生变化
        /// </summary>
        private bool serverAddessChanged = false;

        #endregion

        #region 属性

        /// <summary>
        /// 连接是否成功
        /// </summary>
        public bool ConnectionSucess
        {
            get
            {
                return _connectionSucess;
            }
            set
            {
                _connectionSucess = value;
            }
        }

        /// <summary>
        /// 清除用户列表的回调方法
        /// </summary>
        public RemoveUserListCallbackHandler RemoveUserListCallbackHandler
        {
            set
            {
                _removeUserListCallbackHandler = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserSettingForm()
        {
            InitializeComponent();

            /* 初始化网络参数设置 */
            networkConnection = new NetworkConnection();
            txtServerAddress.Text = CurrentConfig.Instance.ServerAddress;
            testTimer = 0;
            lblTimerValue.Text = "0.00 秒";

            /* 初始化用户参数设置 */
            IList<UserInfoConfig> userConfigs = UserInfoConfigHelper.GetConfigInfoList();
            foreach (UserInfoConfig userConfig in userConfigs)
            {
                CheckedListBoxItem item = new CheckedListBoxItem(userConfig, userConfig.UserName);
                clstUser.Items.Insert(0, item);
            }
        }

        #endregion

        #region 窗口加载和控件方法

        /// <summary>
        /// 窗口加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserSettingForm_Load(object sender, EventArgs e)
        {
            lblVersion.Text = string.Format("当前客户端版本号：{0}", CryptographyHelper.Decrypt(AppSettingHelper.ClientVersion));
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                string serverAddress = txtServerAddress.Text.Trim();
                if (!serverAddress.Equals(CurrentConfig.Instance.ServerAddress) || !CurrentConfig.Instance.ServerAddress.Equals(serverAddress))
                {
                    UserValidator userValidator = new UserValidator();
                    userValidator.ChangeClientConfigInSysConfig(serverAddress, true);
                    CurrentConfig.Instance.ServerAddress = serverAddress;
                    CurrentConfig.Instance.State = true;
                }
                Cursor = Cursors.Default;
                this.Close();
            }
            catch (Exception exception)
            {
                _connectionSucess = false;
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            switch (xtcUserSetting.SelectedTabPageIndex)
            {
                case 0: /* 取消网络测试 */
                    if (btnTestConnection.Enabled)
                    {
                        txtServerAddress.Text = string.Empty;
                        txtServerAddress.Focus();
                    }
                    else
                    {
                        _connectionSucess = false;
                        networkConnection.CancelTest = true;
                        SetNetworkControlProperties(true);
                    }
                    break;

                case 1:
                    chkSelect.CheckState = CheckState.Unchecked;
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 测试连接操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestConnection_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            pbcWaittingBar.Properties.Stopped = false;

            networkConnection.CancelTest = false;
            testTimer = 0;
            lblTimerValue.Text = "0秒";
            SetNetworkControlProperties(false);

            /* 测试并修改配置文件 */
            string serverAddress = txtServerAddress.Text.Trim();
            networkConnection.TimerHandler = new TimerHandler(TestTimerHandler);
            bool result = networkConnection.TestConnection(serverAddress);
            if (!serverAddress.Equals(CurrentConfig.Instance.ServerAddress) || (result != CurrentConfig.Instance.State))
            {
                UserValidator userValidator = new UserValidator();
                userValidator.ChangeClientConfigInSysConfig(serverAddress, result);
            }
            CurrentConfig.Instance.ServerAddress = serverAddress;
            CurrentConfig.Instance.State = result;

            SetNetworkControlProperties(true);
            pbcWaittingBar.Properties.Stopped = true;
            _connectionSucess = result;
            txtServerAddress.Focus();
            Cursor = Cursors.Default;

            if (result)
            {
                lblStateValue.Text = "连接状态";
                MessageBox.Show("与服务器连接测试成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                lblStateValue.Text = "断开状态";
                MessageBox.Show("与服务器连接测试失败。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 地址名称发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtServerAddress_EditValueChanged(object sender, EventArgs e)
        {
            serverAddessChanged = true;
        }


        /// <summary>
        /// 全选或全部取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkSelect_CheckedChanged(object sender, EventArgs e)
        {
            CheckState state = chkSelect.Checked ? CheckState.Checked : CheckState.Unchecked;
            foreach (CheckedListBoxItem item in clstUser.Items)
            {
                item.CheckState = state;
            }
        }

        /// <summary>
        /// 清除选中的用户名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            if (clstUser.CheckedItems.Count == 0)
            {
                MessageBox.Show("未选择需要清除的用户名。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("确认要清除所选择的用户名吗？", "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                Cursor = Cursors.WaitCursor;
                try
                {
                    IList<UserInfoConfig> userConfigs = UserInfoConfigHelper.GetConfigInfoList();
                    string defaultUserName = UserInfoConfigHelper.GetDefaultValue();
                    IList<UserInfoConfig> removedUserConfigs = new List<UserInfoConfig>(userConfigs.Count);
                    for (int i = clstUser.Items.Count - 1; i >= 0; i--)
                    {
                        if (clstUser.Items[i].CheckState == CheckState.Checked)
                        {
                            UserInfoConfig userConfig = clstUser.Items[i].Value as UserInfoConfig;
                            UserInfoConfigHelper.RemoveConfigInfo(userConfig);
                            removedUserConfigs.Add(userConfig);
                            clstUser.Items.RemoveAt(i);
                            if (clstUser.Items.Count > 0 && !string.IsNullOrWhiteSpace(defaultUserName) && defaultUserName.Equals(userConfig.UserName))
                            {
                                UserInfoConfigHelper.ModifyDefaultValueOfSystemConfigInfo(string.Empty);
                            }
                        }
                    }
                    if (clstUser.Items.Count == 0)
                    {
                        UserInfoConfigHelper.RemoveSystemConfigInfo();
                        UserInfoConfigHelper.CreateDefaultConfigInfo();
                    }
                    if (_removeUserListCallbackHandler != null)
                    {
                        _removeUserListCallbackHandler(removedUserConfigs);
                    }
                    Cursor = Cursors.Default;
                }
                catch (Exception exception)
                {
                    Cursor = Cursors.Default;
                    WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                }
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 网络测试过程中更新耗时数据
        /// </summary>
        /// <param name="millisecond"></param>
        private void TestTimerHandler(int millisecond)
        {
            testTimer += millisecond;
            double timer = (double)testTimer / 1000;
            lblTimerValue.Text = string.Format("{0:F2} 秒", timer);
        }

        /// <summary>
        /// 设置网络测试时控件属性
        /// </summary>
        /// <param name="enable"></param>
        private void SetNetworkControlProperties(bool enable)
        {
            txtServerAddress.ReadOnly = !enable;
            btnTestConnection.Enabled = enable;
            if (enable)
            {
                btnTestConnection.Text = "测试...(&T)";
            }
            else
            {
                btnTestConnection.Text = "连接测试中...";
            }
            btnConfirm.Enabled = enable;
        }

        #endregion
    }
}
