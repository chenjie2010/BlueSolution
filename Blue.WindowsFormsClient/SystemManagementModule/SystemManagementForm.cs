using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class SystemManagementForm : Form
    {
        #region  私有变量

        private bool isLoading = true;
        private readonly Dictionary<int, SystemConfigInfo> systemConfigInfosUpdated = null;
        private readonly Dictionary<int, SystemConfigInfo> systemConfigInfos = null;
        private readonly Dictionary<SystemConfigKeyName, CheckTextContentDelegate> systemConfigKeyNamesChanged = null;

        #endregion

        #region  契约接口 

        private readonly ISystemConfigContract systemConfigContract;
        private readonly IUserAccountContract userAccountContract;

        #endregion

        #region 构造函数 

        /// <summary>
        /// 构造函数
        /// </summary>
        public SystemManagementForm()
        {
            InitializeComponent();
            systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            systemConfigInfosUpdated = new Dictionary<int, SystemConfigInfo>();
            systemConfigKeyNamesChanged = new Dictionary<SystemConfigKeyName, CheckTextContentDelegate>();
            systemConfigInfos = systemConfigContract.GetSystemConfigInfos();
        }

        #endregion

        #region 窗体和控件的方法 

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SystemManagementForm_Load(object sender, EventArgs e)
        {
            LoadData(SystemConfigCategory.Parameter);
            SetButtonsEnabled(false);       
        }

        /// <summary>
        /// 界面切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtcSystem_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            SystemConfigCategory systemConfigCategory = (SystemConfigCategory)Convert.ToByte(e.Page.Tag);
            if (systemConfigCategory == SystemConfigCategory.Password)
            {
                SetButtonsEnabled(false);
            }
            else
            {
                SetButtonsEnabled(true);
            }
            LoadData(systemConfigCategory);
        }

        /// <summary>
        /// 确定操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            UpdateSystemConfigInfos();
            this.Close();
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            isLoading = true;
            SystemConfigCategory systemConfigCategory = (SystemConfigCategory)Convert.ToByte(xtcSystem.SelectedTabPage.Tag);
            SetButtonsEnabled(false);
            LoadData(systemConfigCategory);
            isLoading = false;
        }

        /// <summary>
        /// 应用操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            if (systemConfigInfosUpdated.ContainsKey((int)SystemConfigKeyName.EmailPassword))
            {
                string emailPwd = txtEmailPwd.Text.Trim();
                string emailConfirmedPwd = txtEmailConfirmedPwd.Text.Trim();
                if (!emailPwd.Equals(emailConfirmedPwd))
                {
                    MessageBox.Show("Email密码和Email确认密码不一致。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (systemConfigInfosUpdated.ContainsKey((int)SystemConfigKeyName.AlternativeEmailPassword))
            {
                string emailPwd = txtAlternativePassword.Text.Trim();
                string emailConfirmedPwd = txtAlternativePwd.Text.Trim();
                if (!emailPwd.Equals(emailConfirmedPwd))
                {
                    MessageBox.Show("备用Email的密码和备用Email的确认密码不一致。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            UpdateSystemConfigInfos();
            btnApply.Enabled = false; 
        }       

        /// <summary>
        /// 查询密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            ShowPassword();
        }

        /// <summary>
        /// 清空查询内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
            txtUserPwd.Text = string.Empty;
        }

        /// <summary>
        /// 支持回车查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserName_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                ShowPassword();
            }
        }

        /// <summary>
        /// 系统名称的值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSystemName_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtSystemName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(systemConfigValue))
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.DefaultSystemName, systemConfigValue, SystemConfigCategory.Parameter);
                }
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 用户名标签
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserNameLabel_EditValueChanged(object sender, EventArgs e)
        {
            string systemConfigValue = txtUserNameLabel.Text.Trim();
            if (!string.IsNullOrWhiteSpace(systemConfigValue))
            {
                SetSystemConfigInfosUpdated(SystemConfigKeyName.UserNameLabelInfo, systemConfigValue, SystemConfigCategory.Parameter);
            }
            SetButtonsEnabled(true);            
        }

        /// <summary>
        /// Web系统自动注销时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWebTimeout_EditValueChanged(object sender, EventArgs e)
        {
            string systemConfigValue = txtWebTimeout.Text.Trim();
            if (!string.IsNullOrWhiteSpace(systemConfigValue))
            {
                SetSystemConfigInfosUpdated(SystemConfigKeyName.WebTimeout, systemConfigValue, SystemConfigCategory.Parameter);
            }
            SetButtonsEnabled(true);            
        }

        /// <summary>
        /// 远程用户名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRemoteUserName_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtRemoteUserName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(systemConfigValue))
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.RemoteUserName, systemConfigValue, SystemConfigCategory.Platform);
                }
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 远程密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRemotePassword_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtRemotePassword.Text.Trim();
                string systemConfigValueConfirmed = txtRemotePasswordConfirmed.Text.Trim();
                if (!string.IsNullOrWhiteSpace(systemConfigValue) && systemConfigValue.Equals(systemConfigValueConfirmed))
                {
                    if (!string.IsNullOrWhiteSpace(systemConfigValue))
                    {
                        SetSystemConfigInfosUpdated(SystemConfigKeyName.RemotePassword, systemConfigValue, SystemConfigCategory.Platform);
                    }
                    SetButtonsEnabled(true);
                }
            }
        }

        /// <summary>
        /// 远程链接确认密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtRemotePasswordConfirmed_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtRemotePassword.Text.Trim();
                string systemConfigValueConfirmed = txtRemotePasswordConfirmed.Text.Trim();
                if (!string.IsNullOrWhiteSpace(systemConfigValue) && systemConfigValue.Equals(systemConfigValueConfirmed))
                {
                    if (!string.IsNullOrWhiteSpace(systemConfigValue))
                    {
                        SetSystemConfigInfosUpdated(SystemConfigKeyName.RemotePassword, systemConfigValue, SystemConfigCategory.Platform);
                    }
                    SetButtonsEnabled(true);
                }
            }
        }

        /// <summary>
        /// 单位标签信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDepartmentLabelInfo_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                SetButtonsEnabled(true);
                if (!systemConfigKeyNamesChanged.ContainsKey(SystemConfigKeyName.DepartmentLabelInfo))
                {
                    systemConfigKeyNamesChanged.Add(SystemConfigKeyName.DepartmentLabelInfo, CheckDepartmentLabelInfo);
                }
            }
        }        

        /// <summary>
        /// 单位属性变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDepartmentProperty_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                SetButtonsEnabled(true);
                if (!systemConfigKeyNamesChanged.ContainsKey(SystemConfigKeyName.DepartmentProperty))
                {
                    systemConfigKeyNamesChanged.Add(SystemConfigKeyName.DepartmentProperty, CheckDepartmentProperty);
                }               
            }
        }

        /// <summary>
        /// 证件类型变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtIdentificationType_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                SetButtonsEnabled(true);
                if (!systemConfigKeyNamesChanged.ContainsKey(SystemConfigKeyName.IdentityType))
                {
                    systemConfigKeyNamesChanged.Add(SystemConfigKeyName.IdentityType, CheckIdentityType);
                }               
            }
        }

        /// <summary>
        /// 启用密码复杂校验机制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkPasswordValidated_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = "0";
                if (chkPasswordValidated.Checked)
                {
                    systemConfigValue = "1";
                }
                SetSystemConfigInfosUpdated(SystemConfigKeyName.PasswordValidated, systemConfigValue, SystemConfigCategory.Parameter);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 启用用户登录错误锁定机制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkUserLogon_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = "0";
                if (chkUserLogon.Checked)
                {
                    systemConfigValue = "1";
                }
                SetSystemConfigInfosUpdated(SystemConfigKeyName.LogonLocked, systemConfigValue, SystemConfigCategory.Parameter);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 启用自定义Email地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableEmailAddress_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = "0";
                if (chkEnableEmailAddress.Checked)
                {
                    systemConfigValue = "1";
                }
                SetSystemConfigInfosUpdated(SystemConfigKeyName.EnableUserEmail, systemConfigValue, SystemConfigCategory.Mail);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// Email地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEmailAddress_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                SetButtonsEnabled(true);
                if (!systemConfigKeyNamesChanged.ContainsKey(SystemConfigKeyName.EmailUserName))
                {
                    systemConfigKeyNamesChanged.Add(SystemConfigKeyName.EmailUserName, CheckEmailAddress);
                }
            }
        }

        /// <summary>
        /// 备用Email地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAlternativeEmailAddress_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                SetButtonsEnabled(true);
                if (!systemConfigKeyNamesChanged.ContainsKey(SystemConfigKeyName.EmailUserName))
                {
                    systemConfigKeyNamesChanged.Add(SystemConfigKeyName.EmailUserName, CheckAlternativeEmailAddress);
                }
            }
        }

        /// <summary>
        /// Email密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtEmailPwd_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtEmailPwd.Text.Trim();
                if (!string.IsNullOrWhiteSpace(systemConfigValue))
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.EmailPassword, systemConfigValue, SystemConfigCategory.Mail);
                }
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 备用Email的密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAlternativePassword_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtAlternativePassword.Text.Trim();
                if (!string.IsNullOrWhiteSpace(systemConfigValue))
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.AlternativeEmailPassword, systemConfigValue, SystemConfigCategory.Mail);
                }
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// SMTP地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSmtpAddress_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtSmtpAddress.Text.Trim();
                if (!string.IsNullOrWhiteSpace(systemConfigValue))
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.EmailSMTP, systemConfigValue, SystemConfigCategory.Mail);
                }
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 备用Email的SMTP地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAlternativeEmailSMTP_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtAlternativeEmailSMTP.Text.Trim();
                if (!string.IsNullOrWhiteSpace(systemConfigValue))
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.AlternativeEmailSMTP, systemConfigValue, SystemConfigCategory.Mail);
                }
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 测试发送邮件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTestMail_Click(object sender, EventArgs e)
        {
            try
            {
                EamilAddressForm frmEamilAddress = new EamilAddressForm();
                frmEamilAddress.UpdateText = (text) =>
                {
                    Cursor = Cursors.WaitCursor;
                    bool enableEmailAddress = chkEnableEmailAddress.Checked;
                    string[] emailAddress = new string[4];
                    string[] emailPwd = new string[4];
                    string[] smtpAddress = new string[4];

                    if (enableEmailAddress)
                    {
                        emailAddress[0] = txtEmailAddress.Text.Trim();
                        emailPwd[0] = txtEmailPwd.Text.Trim();
                        smtpAddress[0] = txtSmtpAddress.Text.Trim();
                        emailAddress[1] = txtAlternativeEmailAddress.Text.Trim();
                        emailPwd[1] = txtAlternativePassword.Text.Trim();
                        smtpAddress[1] = txtAlternativeEmailSMTP.Text.Trim();
                    }
                    else
                    {
                        emailAddress = AppSettingHelper.DefaultEmailName.Split(',');
                        emailPwd = CryptographyHelper.Decrypt(AppSettingHelper.DefaultEmailPassword).Split(',');
                        smtpAddress = AppSettingHelper.DefaultSmtpAddress.Split(',');
                    }
                    for (int i = 0; i < 2; i++)
                    {
                        MailProcessor mailProcessor = new MailProcessor(smtpAddress[i], emailAddress[i], text,
                            "测试发送密码邮件(请勿回复)", "测试邮件，请勿回复！", emailPwd[i]);
                        mailProcessor.Send();
                    }
                    Cursor = Cursors.Default;
                    if (enableEmailAddress)
                    {
                        MessageBox.Show(string.Format("测试成功，已通过自定义的主备两个Email地址发送了两封邮件，请稍后打开{0}查看！", text), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show(string.Format("测试成功，已通过系统自带的主备两个Email地址发送了两封邮件，请稍后打开{0}查看！", text), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;

                };
                frmEamilAddress.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 是否启用第三方登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableAccess_CheckedChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = chkEnableAccess.Checked ? "1" : "0";
                SetSystemConfigInfosUpdated(SystemConfigKeyName.EnableAccess, systemConfigValue, SystemConfigCategory.Interface);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 本系统Web地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtWebAddress_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtWebAddress.Text.Trim();
                SetSystemConfigInfosUpdated(SystemConfigKeyName.WebAddress, systemConfigValue, SystemConfigCategory.Interface);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 校验关键字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUniqueCodeName_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtUniqueCodeName.Text.Trim();
                SetSystemConfigInfosUpdated(SystemConfigKeyName.UniqueCodeName, systemConfigValue, SystemConfigCategory.Interface);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 第三方校验地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSSOValidationAddress_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtSSOValidationAddress.Text.Trim();
                SetSystemConfigInfosUpdated(SystemConfigKeyName.SSOValidationAddress, systemConfigValue, SystemConfigCategory.Interface);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 接口关键字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtAccessTokenName_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtAccessTokenName.Text.Trim();
                SetSystemConfigInfosUpdated(SystemConfigKeyName.AccessTokenName, systemConfigValue, SystemConfigCategory.Interface);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 第三方接口地址
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInterfaceAddress_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtInterfaceAddress.Text.Trim();
                SetSystemConfigInfosUpdated(SystemConfigKeyName.InterfaceAddress, systemConfigValue, SystemConfigCategory.Interface);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 注销地址变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtLogout_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtLogout.Text.Trim();
                SetSystemConfigInfosUpdated(SystemConfigKeyName.Logout, systemConfigValue, SystemConfigCategory.Interface);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 客户端ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSSOClientId_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtSSOClientId.Text.Trim();
                SetSystemConfigInfosUpdated(SystemConfigKeyName.SSOClientId, systemConfigValue, SystemConfigCategory.Interface);
                SetButtonsEnabled(true);
            }
        }

        /// <summary>
        /// 客户端密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSSOPassword_EditValueChanged(object sender, EventArgs e)
        {
            if (!isLoading)
            {
                string systemConfigValue = txtSSOPassword.Text.Trim();
                SetSystemConfigInfosUpdated(SystemConfigKeyName.SSOPassword, systemConfigValue, SystemConfigCategory.Interface);
                SetButtonsEnabled(true);
            }
        }
        
        #endregion

        #region 私有方法

        /// <summary>
        /// 更新变动的值
        /// </summary>
        private void UpdateSystemConfigInfos()
        {
            foreach (var systemConfigKeyNameChanged in systemConfigKeyNamesChanged)
            {
                string warning = string.Empty;
                bool result = systemConfigKeyNameChanged.Value(ref warning);
                if (!result)
                {
                    MessageBox.Show(warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (systemConfigInfosUpdated.Count > 0)
            {
                systemConfigContract.UpdateSystemConfigInfos(systemConfigInfosUpdated);
                foreach (KeyValuePair<int, SystemConfigInfo> systemConfigInfo in systemConfigInfos)
                {
                    if (systemConfigInfosUpdated.ContainsKey(systemConfigInfo.Key))
                    {
                        systemConfigInfo.Value.SystemConfigValue = systemConfigInfosUpdated[systemConfigInfo.Key].SystemConfigValue;
                    }
                }
                systemConfigInfosUpdated.Clear();
            }
            systemConfigKeyNamesChanged.Clear();
        }
        
        /// <summary>
        /// 更新值
        /// </summary>
        /// <param name="key"></param>
        /// <param name="systemConfigValue"></param>
        /// <param name="systemConfigCategory"></param>
        private void SetSystemConfigInfosUpdated(SystemConfigKeyName systemConfigKeyName, string systemConfigValue, SystemConfigCategory systemConfigCategory)
        {
            int key = (int)systemConfigKeyName;
            if (systemConfigInfosUpdated.ContainsKey(key))
            {
                systemConfigInfosUpdated[key].SystemConfigValue = systemConfigValue;
            }
            else
            {
                systemConfigInfosUpdated.Add(key, new SystemConfigInfo(key, systemConfigValue, SystemConfigCategory.Parameter, DateTime.Now));
            }            
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="systemConfigCategory"></param>
        private void LoadData(SystemConfigCategory systemConfigCategory)
        {
            isLoading = true;
            int key = 0;
            switch (systemConfigCategory)
            {
                case SystemConfigCategory.Parameter:
                    /* 系统名称 */
                    key = (int)SystemConfigKeyName.DefaultSystemName;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtSystemName.Text = systemConfigInfos[key].SystemConfigValue;
                    }
                    else
                    {
                        txtSystemName.Text = AppSettingHelper.DefaultSystemName;
                    }
                    txtSystemName.Focus();

                    /* 单位标签信息 */
                    key = (int)SystemConfigKeyName.DepartmentLabelInfo;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtDepartmentLabelInfo.Text = systemConfigInfos[key].SystemConfigValue;
                    }
                    else
                    {
                        txtDepartmentLabelInfo.Text = UserEnumHelper.GetEnumText(SystemConfigKeyName.DepartmentLabelInfo);
                    }
                    if (systemConfigKeyNamesChanged.ContainsKey(SystemConfigKeyName.DepartmentLabelInfo))
                    {
                        systemConfigKeyNamesChanged.Remove(SystemConfigKeyName.DepartmentLabelInfo);
                    }

                    /* 单位属性 */
                    key = (int)SystemConfigKeyName.DepartmentProperty;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtDepartmentProperty.Text = systemConfigInfos[key].SystemConfigValue;
                    }
                    else
                    {
                        txtDepartmentProperty.Text = UserEnumHelper.GetEnumText(SystemConfigKeyName.DepartmentProperty);
                    }
                    if (systemConfigKeyNamesChanged.ContainsKey(SystemConfigKeyName.DepartmentProperty))
                    {
                        systemConfigKeyNamesChanged.Remove(SystemConfigKeyName.DepartmentProperty);
                    }

                    /* 证件类型 */
                    key = (int)SystemConfigKeyName.IdentityType;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtIdentificationType.Text = systemConfigInfos[key].SystemConfigValue;
                    }
                    else
                    {
                        txtIdentificationType.Text = UserEnumHelper.GetEnumText(SystemConfigKeyName.IdentityType);
                    }
                    if (systemConfigKeyNamesChanged.ContainsKey(SystemConfigKeyName.IdentityType))
                    {
                        systemConfigKeyNamesChanged.Remove(SystemConfigKeyName.IdentityType);
                    }

                    /* 启用密码复杂校验机制 */
                    key = (int)SystemConfigKeyName.PasswordValidated;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        chkPasswordValidated.Checked = Convert.ToBoolean(DataConvertionHelper.GetConvertedInt(systemConfigInfos[key].SystemConfigValue, 0));
                    }
                    else
                    {
                        chkPasswordValidated.Checked = false;
                    }

                    /* 启用用户登录错误锁定机制 */
                    key = (int)SystemConfigKeyName.LogonLocked;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        chkUserLogon.Checked = Convert.ToBoolean(DataConvertionHelper.GetConvertedInt(systemConfigInfos[key].SystemConfigValue, 0));
                    }
                    else
                    {
                        chkUserLogon.Checked = false;
                    }

                    /* 用户名称 */
                    key = (int)SystemConfigKeyName.UserNameLabelInfo;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtUserNameLabel.Text = systemConfigInfos[key].SystemConfigValue;
                    }
                    else
                    {
                        txtUserNameLabel.Text = AppSettingHelper.UserNameLabelInfo;
                    }

                    /* Web系统自动注销时间 */
                    key = (int)SystemConfigKeyName.WebTimeout;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtWebTimeout.Text = DataConvertionHelper.GetConvertedInt(systemConfigInfos[key].SystemConfigValue, 10).ToString();
                    }
                    else
                    {
                        txtWebTimeout.Text = "10";
                    }
                    break;


                case SystemConfigCategory.Mail:
                    /* 启用自定义Email地址 */
                    key = (int)SystemConfigKeyName.EnableUserEmail;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        chkEnableEmailAddress.Checked = Convert.ToBoolean(DataConvertionHelper.GetConvertedInt(systemConfigInfos[key].SystemConfigValue, 0));
                    }

                    /* Email地址 */
                    key = (int)SystemConfigKeyName.EmailUserName;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtEmailAddress.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* Email密码和Email确认密码 */
                    key = (int)SystemConfigKeyName.EmailPassword;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtEmailPwd.Text = systemConfigInfos[key].SystemConfigValue;
                        txtEmailConfirmedPwd.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* SMTP地址 */
                    key = (int)SystemConfigKeyName.EmailSMTP;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtSmtpAddress.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* 备用Email地址 */
                    key = (int)SystemConfigKeyName.AlternativeEmailUserName;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtAlternativeEmailAddress.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* 备用Email的密码和备用Email的确认密码 */
                    key = (int)SystemConfigKeyName.AlternativeEmailPassword;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtAlternativePassword.Text = systemConfigInfos[key].SystemConfigValue;
                        txtAlternativePwd.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* 备用Email的SMTP地址 */
                    key = (int)SystemConfigKeyName.AlternativeEmailSMTP;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtAlternativeEmailSMTP.Text = systemConfigInfos[key].SystemConfigValue;
                    }
                    break;

                case SystemConfigCategory.Platform:
                    /* 远程链接用户名 */
                    key = (int)SystemConfigKeyName.RemoteUserName;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtRemoteUserName.Text = systemConfigInfos[key].SystemConfigValue;
                    }
                    txtRemoteUserName.Focus();

                    /* 远程链接密码与确认密码 */
                    key = (int)SystemConfigKeyName.RemotePassword;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtRemotePassword.Text = systemConfigInfos[key].SystemConfigValue;
                        txtRemotePasswordConfirmed.Text = systemConfigInfos[key].SystemConfigValue;
                    }
                    break;

                case SystemConfigCategory.Interface:
                    /* 是否启用第三方登录 */
                    key = (int)SystemConfigKeyName.EnableAccess;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        chkEnableAccess.Checked = Convert.ToBoolean(DataConvertionHelper.GetConvertedInt(systemConfigInfos[key].SystemConfigValue, 0));
                    }
                    chkEnableAccess.Focus();

                    /* 本系统Web地址 */
                    key = (int)SystemConfigKeyName.WebAddress;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtWebAddress.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* 校验关键字 */
                    key = (int)SystemConfigKeyName.UniqueCodeName;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtUniqueCodeName.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* 第三方校验地址 */
                    key = (int)SystemConfigKeyName.SSOValidationAddress;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtSSOValidationAddress.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* 接口关键字 */
                    key = (int)SystemConfigKeyName.AccessTokenName;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtAccessTokenName.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* 第三方接口地址 */
                    key = (int)SystemConfigKeyName.InterfaceAddress;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtInterfaceAddress.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* 第三方注销地址 */
                    key = (int)SystemConfigKeyName.Logout;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtLogout.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* 客户端编号 */
                    key = (int)SystemConfigKeyName.SSOClientId;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtSSOClientId.Text = systemConfigInfos[key].SystemConfigValue;
                    }

                    /* 客户端密码 */
                    key = (int)SystemConfigKeyName.SSOPassword;
                    if (systemConfigInfos.ContainsKey(key))
                    {
                        txtSSOPassword.Text = systemConfigInfos[key].SystemConfigValue;
                    }
                    break;                    
            }
            isLoading = false;
        }

        /// <summary>
        /// 查询密码
        /// </summary>
        private void ShowPassword()
        {
            string userName = txtUserName.Text.Trim();
            if (string.IsNullOrWhiteSpace(userName))
            {
                txtUserName.Focus();
                MessageBox.Show("用户名不能为空.", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            txtUserPwd.Text = userAccountContract.GetUserPassword(userName);
        }

        /// <summary>
        /// 设置按钮的可操作属性
        /// </summary>
        /// <param name="enabled"></param>
        private void SetButtonsEnabled(bool enabled)
        {
            btnConfirm.Enabled = enabled;
            btnApply.Enabled = enabled;
        }

        /// <summary>
        /// 检查证件类型
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private bool CheckIdentityType(ref string warning)
        {
            bool result = true;

            string systemConfigValue = txtIdentificationType.Text.Trim();
            if (!string.IsNullOrWhiteSpace(systemConfigValue))
            {
                string[] labelsSplitted = systemConfigValue.Split('|');
                if (labelsSplitted.Length < 2 || labelsSplitted.Length > 11)
                {
                    warning = "证件类型数量范围为：2个~10个。";
                    result = false;
                }
                else
                {
                    foreach (var item in labelsSplitted)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            string[] items = item.Split(',');
                            if (items.Length != 2 || string.IsNullOrWhiteSpace(items[0]) || string.IsNullOrWhiteSpace(items[1])
                                || items[0].Length > 16)
                            {
                                result = false;
                                warning = string.Format("格式错误，正确的格式：{0}", UserEnumHelper.GetEnumText(SystemConfigKeyName.IdentityType));
                                break;
                            }
                            if (!UserDataHelper.MatchByte(items[1]))
                            {
                                result = false;
                                warning = "每个证件类型属性值范围为：[0, 255]。";
                                break;
                            }
                        }
                        else
                        {
                            result = false;
                            warning = "证件类型的每一项内容不能为空。";
                            break;
                        }
                    }
                }
                if (result)
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.IdentityType, systemConfigValue, SystemConfigCategory.Parameter);                   
                }
            }
            else
            {
                warning = "证件类型不能为空。";
            }

            return result;
        }

        /// <summary>
        /// 单位属性
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private bool CheckDepartmentProperty(ref string warning)
        {
            bool result = true;

            string systemConfigValue = txtDepartmentProperty.Text.Trim();
            if (!string.IsNullOrWhiteSpace(systemConfigValue))
            {
                string[] labelsSplitted = systemConfigValue.Split('|');
                if (labelsSplitted.Length < 2 || labelsSplitted.Length > 11)
                {
                    warning = "单位性质数量范围为：2个~10个。";
                    result = false;
                }
                else
                {
                    foreach (var item in labelsSplitted)
                    {
                        if (!string.IsNullOrWhiteSpace(item))
                        {
                            string[] items = item.Split(',');
                            if (items.Length != 2 || string.IsNullOrWhiteSpace(items[0]) || string.IsNullOrWhiteSpace(items[1])
                                || items[0].Length > 16)
                            {
                                result = false;
                                warning = string.Format("格式错误，正确的格式：{0}", UserEnumHelper.GetEnumText(SystemConfigKeyName.DepartmentProperty));
                                break;
                            }
                            if (!UserDataHelper.MatchByte(items[1]))
                            {
                                result = false;
                                warning = "每个单位属性值范围为：[0, 255]。";
                                break;
                            }
                        }
                        else
                        {
                            result = false;
                            warning = "单位性质的每一项内容不能为空。";
                            break;
                        }
                    }
                }
                if (result)
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.DepartmentProperty, systemConfigValue, SystemConfigCategory.Parameter);                    
                }
            }
            else
            {
                warning = "单位性质不能为空。";
            }

            return result;
        }

        /// <summary>
        /// 单位标签信息
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private bool CheckDepartmentLabelInfo(ref string warning)
        {
            bool result = true;

            string systemConfigValue = txtDepartmentLabelInfo.Text.Trim();
            if (!string.IsNullOrWhiteSpace(systemConfigValue))
            {
                string[] labelsSplitted = systemConfigValue.Split('|');
                if (labelsSplitted.Length != 3)
                {
                    warning = string.Format("格式错误，正确的格式：{0}", UserEnumHelper.GetEnumText(SystemConfigKeyName.DepartmentLabelInfo));
                    return false;
                }
                else
                {
                    foreach (var item in labelsSplitted)
                    {
                        if (string.IsNullOrWhiteSpace(item) || item.Length > 16)
                        {
                            result = false;
                            break;
                        }
                    }
                }
                if (result)
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.DepartmentLabelInfo, systemConfigValue, SystemConfigCategory.Parameter);
                }
                else
                {
                    warning = "单位标签中每项内容都不能为空，或者长度超过16位。";
                }
            }
            else
            {
                warning = "单位标签不能为空。";
            }

            return result;
        }

        /// <summary>
        /// 检查邮件地址
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private bool CheckEmailAddress(ref string warning)
        {
            bool result = true;

            string systemConfigValue = txtEmailAddress.Text.Trim();
            if (!string.IsNullOrWhiteSpace(systemConfigValue))
            {
                result = UserDataHelper.MatchEmail(systemConfigValue);
                if (result)
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.EmailUserName, systemConfigValue, SystemConfigCategory.Mail);
                }
                else
                {
                    txtEmailAddress.Focus();
                    warning = "Email地址格式不正确。";
                }
            }
            else
            {
                if (chkEnableEmailAddress.Checked)
                {
                    result = false;
                    warning = "Email地址不能为空。";
                    txtEmailAddress.Focus();
                }
                else
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.EmailUserName, string.Empty, SystemConfigCategory.Mail);
                }
            }

            return result;
        }

        /// <summary>
        /// 检查备用邮件地址
        /// </summary>
        /// <param name="warning"></param>
        /// <returns></returns>
        private bool CheckAlternativeEmailAddress(ref string warning)
        {
            bool result = true;

            string systemConfigValue = txtAlternativeEmailAddress.Text.Trim();
            if (!string.IsNullOrWhiteSpace(systemConfigValue))
            {
                result = UserDataHelper.MatchEmail(systemConfigValue);
                if (result)
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.AlternativeEmailUserName, systemConfigValue, SystemConfigCategory.Mail);
                }
                else
                {
                    txtEmailAddress.Focus();
                    warning = "备用Email地址格式不正确。";
                }
            }
            else
            {
                if (chkEnableEmailAddress.Checked)
                {
                    result = false;
                    warning = "备用Email地址不能为空。";
                    txtAlternativeEmailAddress.Focus();
                }
                else
                {
                    SetSystemConfigInfosUpdated(SystemConfigKeyName.EmailUserName, string.Empty, SystemConfigCategory.Mail);
                }
            }

            return result;
        }

        #endregion

        
    }
}
