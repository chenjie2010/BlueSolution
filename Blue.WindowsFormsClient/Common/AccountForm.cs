using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.Model.UserModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.Common
{
    /// <summary>
    /// 个人信息修改
    /// </summary>
    public partial class AccountForm : Form
    {
        #region 私有变量

        private readonly DevExpress.Utils.ToolTipControllerShowEventArgs toolTipArgs;
        private readonly bool passwordValidated;

        #endregion

        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ISystemConfigContract systemConfigContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AccountForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();

            toolTipArgs = tlpPhoto.CreateShowArgs();
            toolTipArgs.IconType = DevExpress.Utils.ToolTipIconType.Information;
            toolTipArgs.IconSize = DevExpress.Utils.ToolTipIconSize.Small;
            toolTipArgs.ImageIndex = 0;

            passwordValidated = Convert.ToBoolean(DataConvertionHelper.GetConvertedInt(systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.PasswordValidated), 0));
            if (passwordValidated)
            {
                lblPasswordValidation.Text = string.Format("提示：新密码必须由数字、字母和特殊符号组成，长度范围为{0}位-{1}位。",
                    AppSettingHelper.DefaultPaswordMinLength, AppSettingHelper.DefaultPaswordMaxLength);                
            }
            else
            {
                lblPasswordValidation.Text = "提示：新密码必须由数字、字母和特殊符号组成。";
            }
            upfPhoto.Filter = "JPEG(*.JPG;*.JPEG)|*.JPG;*.JPEG|所有文件(*.*)|*.*";
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountForm_Load(object sender, EventArgs e)
        {
            ShowDataOnControls(CurrentUser.Instance.UserId);
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string password = txtPassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(password))
            {
                txtPassword.Focus();
                MessageBox.Show("当前密码不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string newPassword = txtNewPassword.Text.Trim();
            string confrimedPwd = txtConfirmedPassword.Text.Trim();
            if (!string.IsNullOrWhiteSpace(confrimedPwd) && string.IsNullOrWhiteSpace(newPassword))
            {
                txtNewPassword.Focus();
                MessageBox.Show("新密码不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(newPassword) && string.IsNullOrWhiteSpace(confrimedPwd))
            {
                txtConfirmedPassword.Focus();
                MessageBox.Show("确认密码不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(newPassword) && !newPassword.Equals(confrimedPwd))
            {
                txtNewPassword.Focus();
                MessageBox.Show("新密码和确认密码不一致,请重新输入。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                if (newPassword.Length > AppSettingHelper.DefaultPaswordMaxLength || newPassword.Length < AppSettingHelper.DefaultPaswordMinLength)
                {
                    txtNewPassword.Focus();
                    MessageBox.Show(string.Format("新密码长度范围：{0}位-{1}位。", AppSettingHelper.DefaultPaswordMinLength, AppSettingHelper.DefaultPaswordMaxLength),
                        "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (passwordValidated && !UserDataHelper.ValidatePasowdString(newPassword))
                {
                    MessageBox.Show("新密码必须由数字、字母和特殊符号构成。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }                
            }

            string userActualName = txtUserActualName.Text.Trim();
            if (string.IsNullOrWhiteSpace(userActualName))
            {
                txtUserActualName.Focus();
                MessageBox.Show("用户姓名不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (userActualName.Length > 128)
            {
                txtUserActualName.Focus();
                MessageBox.Show("用户姓名长度不能超过128位。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string emailAddress = txtEmailAddress.Text.Trim();
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                txtEmailAddress.Focus();
                MessageBox.Show("电子邮件不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (emailAddress.Length > 64)
            {
                txtEmailAddress.Focus();
                MessageBox.Show("电子邮件长度不能超过64位。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string telephoneNumber = txtTelephoneNumber.Text.Trim();
            if (string.IsNullOrWhiteSpace(telephoneNumber))
            {
                txtTelephoneNumber.Focus();
                MessageBox.Show("手机号码不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (telephoneNumber.Length > 16)
            {
                txtTelephoneNumber.Focus();
                MessageBox.Show("手机号码长度不能超过16位。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(upfPhoto.FileName))
            {
                bool success = FileFormatHelper.VerfiyJPGFormat(upfPhoto.FileName);
                if (!success)
                {
                    upfPhoto.Focus();
                    MessageBox.Show("图片格式只能为：JPG。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                upfPhoto.Focus();
                MessageBox.Show("上传照片不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            byte[] imageData = null;
            if (File.Exists(upfPhoto.FileName))
            {
                imageData = upfPhoto.CustomData;
                if (imageData.Length < AppSettingHelper.DefaultUserPhotoMinLength || imageData.Length > AppSettingHelper.DefaultUserPhotoMaxLength)
                {
                    MessageBox.Show(string.Format("图片大小不符合要求（大小不能小于{0}K或是大于{1}MB）。", AppSettingHelper.DefaultUserPhotoMinLength / 1024,
                                                AppSettingHelper.DefaultUserPhotoMaxLength / (1024 * 1024)), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            string photoSuffixName = string.Empty;
            if (!string.IsNullOrWhiteSpace(upfPhoto.FileName))
            {
                photoSuffixName = upfPhoto.FileName.Substring(upfPhoto.FileName.LastIndexOf('.') + 1).ToUpper();
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                UserValidator userValidator = new UserValidator();
                bool result = userValidator.Validate(CurrentUser.Instance.UserName, password);
                if (!result)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("密码错误，无法修改。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UserAccountInfo oldUserAccountInfo = userAccountContract.GetModelInfo(CurrentUser.Instance.UserId);
                if (oldUserAccountInfo == null)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("该用户不存在，请联系管理员。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool userActualNameUpdated = AuthorityHelper.CheckAuthority(oldUserAccountInfo.DataFieldAuthority, Convert.ToByte(SystemDataFieldPermission.UserActualName));
                if (!userActualNameUpdated)
                {
                    userActualName = string.Empty;
                }
                bool photoUpdated = AuthorityHelper.CheckAuthority(oldUserAccountInfo.DataFieldAuthority, Convert.ToByte(SystemDataFieldPermission.Photo));
                if (!photoUpdated)
                {
                    imageData = null;
                    photoSuffixName = string.Empty;
                }

                bool emailAddressUpdated = AuthorityHelper.CheckAuthority(oldUserAccountInfo.DataFieldAuthority, Convert.ToByte(SystemDataFieldPermission.Mail));
                if (!emailAddressUpdated)
                {
                    emailAddress = string.Empty;
                }

                bool telephoneNumberUpdated = AuthorityHelper.CheckAuthority(oldUserAccountInfo.DataFieldAuthority, Convert.ToByte(SystemDataFieldPermission.Phone));
                if (!telephoneNumberUpdated)
                {
                    telephoneNumber = string.Empty;
                }
                userAccountContract.Update(CurrentUser.Instance.UserName, newPassword, userActualName, emailAddress, telephoneNumber, imageData, photoSuffixName);
                Cursor = Cursors.Default;
                MessageBox.Show("修改成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }


        /// <summary>
        /// 关闭窗口，放弃更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 快捷键操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AccountForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this.GetNextControl(this.ActiveControl, true).Select();
            }
        }

        /// <summary>
        /// 照片预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upfPhoto_OnBrowseClick(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(upfPhoto.FileName))
                {
                    peUser.Image = Image.FromFile(upfPhoto.FileName);
                }
                else
                {
                    peUser.Image = null;
                }
            }
            catch (Exception ex)
            {
                peUser.Image = null;
                MessageBox.Show(string.Format("加载照片失败：{0}", ex.Message), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 照片上传控件鼠标按下时提示无权限修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upfPhoto_MouseDown(object sender, MouseEventArgs e)
        {
            if (upfPhoto.ReadOnly)
            {
                toolTipArgs.Title = "无法编辑原因：";
                toolTipArgs.ToolTip = "无编辑照片的权限";
                tlpPhoto.ShowHint(toolTipArgs, upfPhoto.PointToScreen(new Point(e.X, e.Y)));
            }
        }

        /// <summary>
        /// 鼠标按下时提示无权限修改姓名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtUserActualName_MouseDown(object sender, MouseEventArgs e)
        {
            if (upfPhoto.ReadOnly)
            {
                toolTipArgs.Title = "无法编辑原因：";
                toolTipArgs.ToolTip = "无编辑姓名的权限";
                tlpPhoto.ShowHint(toolTipArgs, txtUserActualName.PointToScreen(new Point(e.X, e.Y)));
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载默认数据到窗体左侧的控件中
        /// </summary>
        /// <param name="userId">节点编号</param>
        private void ShowDataOnControls(decimal userId)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UserAccountInfo userAccountInfo = userAccountContract.GetModelInfo(userId);
                if (userAccountInfo != null)
                {
                    txtUserName.Text = userAccountInfo.UserName;
                    txtPassword.Text = string.Empty;
                    txtNewPassword.Text = string.Empty;
                    txtConfirmedPassword.Text = string.Empty;
                    txtUserActualName.Text = userAccountInfo.UserActualName;
                    txtEmailAddress.Text = userAccountInfo.EmailAddress;
                    txtTelephoneNumber.Text = userAccountInfo.TelephoneNumber;

                    byte[] data = userAccountContract.DownLoadPhoto(userAccountInfo.UserName);
                    if (data != null)
                    {
                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            Image img = Image.FromStream(ms);
                            peUser.Image = img;
                        }
                        if (!string.IsNullOrWhiteSpace(userAccountInfo.PhotoSuffixName))
                        {
                            upfPhoto.FileName = string.Format("{0}.{1}", userAccountInfo.UserName, userAccountInfo.PhotoSuffixName);
                        }
                        else
                        {
                            string photoSuffixName = userAccountContract.AutoGetPhotoSuffixName(userAccountInfo.UserName);
                            upfPhoto.FileName = string.Format("{0}.{1}", userAccountInfo.UserName, photoSuffixName);
                        }
                    }
                    else
                    {
                        peUser.Image = null;
                        upfPhoto.FileName = string.Empty;
                    }
                    bool userActualNameUpdated = AuthorityHelper.CheckAuthority(userAccountInfo.DataFieldAuthority, Convert.ToByte(SystemDataFieldPermission.UserActualName));
                    if (!userActualNameUpdated)
                    {
                        txtUserActualName.ReadOnly = true;
                    }
                    bool photoUpdated = AuthorityHelper.CheckAuthority(userAccountInfo.DataFieldAuthority, Convert.ToByte(SystemDataFieldPermission.Photo));
                    if (!photoUpdated)
                    {
                        upfPhoto.ReadOnly = true;
                    }
                    bool emailAddressUpdated = AuthorityHelper.CheckAuthority(userAccountInfo.DataFieldAuthority, Convert.ToByte(SystemDataFieldPermission.Mail));
                    if (!emailAddressUpdated)
                    {
                        txtEmailAddress.ReadOnly = true;
                    }

                    bool telephoneNumberUpdated = AuthorityHelper.CheckAuthority(userAccountInfo.DataFieldAuthority, Convert.ToByte(SystemDataFieldPermission.Phone));
                    if (!telephoneNumberUpdated)
                    {
                        txtTelephoneNumber.ReadOnly = true;
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }


        #endregion
    }
}
