using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Core.ClientConfig;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class RemoteDatabaseForm : Form
    {
        #region 私有变量

        private int testTimer = 0;
        private readonly NetworkConnection networkConnection = null;

        #endregion
        
        #region 属性

        /// <summary>
        /// 远程服务器对象
        /// </summary>
        public RemoteServer RemoteServerValue
        {
            get;
            set;
        }

        /// <summary>
        /// 确认操作
        /// </summary>
        public RemoteSereverConfrimedDelegate RemoteSereverConfrimed
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RemoteDatabaseForm()
        {
            InitializeComponent();
            networkConnection = new NetworkConnection();
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        ///  窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteDatabaseForm_Load(object sender, EventArgs e)
        {
            if (RemoteServerValue != null)
            {
                txtRemoteAddress.Text = RemoteServerValue.RemoteAddress;
                txtRemoteUserName.Text = RemoteServerValue.RemoteUserName;
                txtRemotePassword.Text = RemoteServerValue.RemotePassword;
                txtRemotePasswordConfirmed.Text = RemoteServerValue.RemotePassword;
                CommonNode commonNode = RemoteServerValue.Tag as CommonNode;
                try
                {
                    Cursor = Cursors.WaitCursor;
                    IRemoteServerContract remoteServerContract = RemoteChannelFactory.CreateRemoteServerContract(RemoteServerValue.RemoteAddress, CurrentConfig.Instance.Port);
                    btxtDatabaseName.Text = commonNode.NodeName;
                    Cursor = Cursors.Default;
                }
                catch
                {
                    btxtDatabaseName.Text = "远程服务器链接失败。";
                    Cursor = Cursors.Default;
                }
                btxtDatabaseName.Tag = commonNode;
            }
        }

        /// <summary>
        /// 测试链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            Test();
        }

        /// <summary>
        /// 取消测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkCancel_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            networkConnection.CancelTest = true;
        }

        /// <summary>
        /// 获取目标数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtDatabaseName_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                string remoteAddress = txtRemoteAddress.Text.Trim();
                string remoteUserName = txtRemoteUserName.Text.Trim();
                string remotePassword = txtRemotePassword.Text.Trim();
                if (string.IsNullOrWhiteSpace(remoteAddress))
                {
                    MessageBox.Show("远程交换地址不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(remoteUserName))
                {
                    MessageBox.Show("用户名不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(remotePassword))
                {
                    MessageBox.Show("密码不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Cursor = Cursors.WaitCursor;
                try
                {
                    IRemoteServerContract remoteServerContract = RemoteChannelFactory.CreateRemoteServerContract(remoteAddress, CurrentConfig.Instance.Port);
                    RemoteDatabaseItemsForm frmRemoteDatabaseItems = new RemoteDatabaseItemsForm();
                    frmRemoteDatabaseItems.Text = "数据库选择";
                    frmRemoteDatabaseItems.ToolTip = "提示：只能选择数据库类型的节点。";
                    frmRemoteDatabaseItems.RemoteServerContract = remoteServerContract;
                    frmRemoteDatabaseItems.UserName = remoteUserName;
                    frmRemoteDatabaseItems.Password = remotePassword;
                    frmRemoteDatabaseItems.NodeSelected = delegate (CommonNode node)
                    {
                        if (node != null)
                        {
                            btxtDatabaseName.Text = node.NodeName;
                            btxtDatabaseName.Tag = node;
                        }
                        else
                        {
                            btxtDatabaseName.Text = string.Empty;
                            btxtDatabaseName.Tag = null;
                        }
                    };
                    Cursor = Cursors.Default;
                    frmRemoteDatabaseItems.ShowDialog();
                }
                catch
                {
                    Cursor = Cursors.Default;
                    btxtDatabaseName.Text = "远程服务器链接失败。";
                    btxtDatabaseName.Tag = null;
                    MessageBox.Show("远程服务器链接失败。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string remoteAddress = txtRemoteAddress.Text.Trim();
            string remoteUserName = txtRemoteUserName.Text.Trim();
            string remotePassword = txtRemotePassword.Text.Trim();
            string remotePasswordConfirmed = txtRemotePasswordConfirmed.Text.Trim();
            if (string.IsNullOrWhiteSpace(remoteAddress))
            {
                MessageBox.Show("远程交换地址不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remoteUserName))
            {
                MessageBox.Show("用户名不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remotePassword))
            {
                MessageBox.Show("密码不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remotePasswordConfirmed))
            {
                MessageBox.Show("确认密码不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!remotePassword.Equals(remotePasswordConfirmed))
            {
                MessageBox.Show("密码和确认密码不一致。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (btxtDatabaseName.Tag == null)
            {
                MessageBox.Show("请设置目标数据库。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode commonNode = btxtDatabaseName.Tag as CommonNode;
            RemoteServer remoteServer = new RemoteServer()
            {
                RemoteAddress = txtRemoteAddress.Text.Trim(),
                RemoteUserName = txtRemoteUserName.Text.Trim(),
                RemotePassword = txtRemotePassword.Text.Trim(),
                Tag = commonNode
            };
            RemoteSereverConfrimed?.Invoke(remoteServer);
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取信息
        /// </summary>
        private void Test()
        {
            string remoteAddress = txtRemoteAddress.Text.Trim();
            string remoteUserName = txtRemoteUserName.Text.Trim();
            string remotePassword = txtRemotePassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(remoteAddress))
            {
                txtRemoteAddress.Focus();
                MessageBox.Show("远程地址不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remoteUserName))
            {
                txtRemoteUserName.Focus();
                MessageBox.Show("用户名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remotePassword))
            {
                txtRemotePassword.Focus();
                MessageBox.Show("密码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                pbcWaittingBar.Visible = true;
                lblTimerValue.Visible = true;
                hlnkCancel.Visible = true;
                pbcWaittingBar.Properties.Stopped = false;
                networkConnection.CancelTest = false;
                testTimer = 0;
                networkConnection.TimerHandler = new TimerHandler(TestTimerHandler);
                bool result = networkConnection.TestRemoteConnection(remoteAddress, remoteUserName, remotePassword);
                pbcWaittingBar.Properties.Stopped = true;
                pbcWaittingBar.Visible = false;
                lblTimerValue.Visible = false;
                hlnkCancel.Visible = false;
                Cursor = Cursors.Default;
                if (result)
                {
                    MessageBox.Show("与远程服务器连接测试成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("与服务器连接测试失败。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                pbcWaittingBar.Visible = false;
                lblTimerValue.Visible = false;
                hlnkCancel.Visible = false;
                Cursor = Cursors.Default;
                MessageBox.Show("无法连接到远程服务器，获取失败！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 网络测试过程中更新耗时数据
        /// </summary>
        /// <param name="millisecond"></param>
        private void TestTimerHandler(int millisecond)
        {
            testTimer += millisecond;
            double timer = (double)testTimer / 1000;
            lblTimerValue.Text = string.Format("时间：{0:F2} 秒", timer);
        }

        #endregion
    }
}
