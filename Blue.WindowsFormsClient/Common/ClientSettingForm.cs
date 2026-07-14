using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Core.ClientConfig;
using Blue.WCFContracts.UserModule;
using Blue.Model.UserModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class ClientSettingForm : Form
    {
        #region  私有变量

        private readonly Dictionary<int, UserConfigInfo> userConfigs = null;
        private bool autoLogonChanged = false;
        private bool isLoading = true;

        #endregion

        #region  契约接口 

        private readonly IUserConfigContract userConfigContract;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 构造函数
        /// </summary>
        public ClientSettingForm()
        {
            InitializeComponent();
            userConfigs = new Dictionary<int, UserConfigInfo>();
            userConfigContract = UserChannelFactory.CreateUserConfig();
        }

        #endregion

        #region 窗体和控件的方法 

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClientSettingForm_Load(object sender, EventArgs e)
        {
            LoadData();
            isLoading = false;
        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (autoLogonChanged)
                {
                    SetAutoLogon();
                }
                if (userConfigs.Count > 0)
                {
                    userConfigContract.UpdateUserConfigInfos(userConfigs);                    
                }                
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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

        /// <summary>
        /// 应用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            try
            {
                if (autoLogonChanged)
                {
                    SetAutoLogon();
                }
                if (userConfigs.Count > 0)
                {
                    userConfigContract.UpdateUserConfigInfos(userConfigs);
                    userConfigs.Clear();                    
                }
                btnApply.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 自动登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkAutoLogin_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                autoLogonChanged = true;
                SetControlsEnabled(true);
            }
        }

        /// <summary>
        /// 邮件提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEmailTip_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                int mailTip = (int)ClientUserSetting.Mail_Tip;
                if (userConfigs.ContainsKey(mailTip))
                {
                    userConfigs.Remove(mailTip);
                }
                string userConfigValue = chkEmailTip.Checked ? "1" : "0";
                userConfigs.Add(mailTip, new UserConfigInfo(CurrentUser.Instance.UserId, mailTip, userConfigValue, DateTime.Now));
                SetControlsEnabled(true);
            }
        }

        /// <summary>
        /// 工作流提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkWorkflow_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                int workflowTip = (int)ClientUserSetting.Workflow_Tip;
                if (userConfigs.ContainsKey(workflowTip))
                {
                    userConfigs.Remove(workflowTip);
                }
                string userConfigValue = chkWorkflow.Checked ? "1" : "0";
                userConfigs.Add(workflowTip, new UserConfigInfo(CurrentUser.Instance.UserId, workflowTip, userConfigValue, DateTime.Now));
                SetControlsEnabled(true);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            /* 自动登录，仅保存在本地 */
            UserInfoConfig config = UserInfoConfigHelper.GetConfigInfo();
            if (config != null)
            {
                chkAutoLogin.Checked = config.AutoLogon;
            }

            /* 保存在数据库中 */
            IList<UserConfigInfo> userConfigInfos = userConfigContract.GetModelInfos(CurrentUser.Instance.UserId);
            foreach (UserConfigInfo userConfigInfo in userConfigInfos)
            {
                ClientUserSetting clientUserSetting = (ClientUserSetting)userConfigInfo.UserConfigName;
                switch (clientUserSetting)
                {
                    case ClientUserSetting.Mail_Tip:
                        chkEmailTip.Checked = Convert.ToBoolean(Convert.ToInt32(userConfigInfo.UserConfigValue));
                        break;

                    case ClientUserSetting.Workflow_Tip:
                        chkWorkflow.Checked = Convert.ToBoolean(Convert.ToInt32(userConfigInfo.UserConfigValue));
                        break;
                }
            }
        }
        
        /// <summary>
        /// 设置控件属性
        /// </summary>
        /// <param name="enbaled"></param>
        private void SetControlsEnabled(bool enbaled)
        {
            btnApply.Enabled = enbaled;
            btnConfirm.Enabled = enbaled;
        }

        /// <summary>
        /// 设置自动登录属性
        /// </summary>
        private void SetAutoLogon()
        {
            UserInfoConfig config = UserInfoConfigHelper.GetConfigInfo();
            if (config != null)
            {
                config.AutoLogon = chkAutoLogin.Checked;
                UserInfoConfigHelper.ModifyConfigInfo(config);
            }
        }

        #endregion       
    }
}
