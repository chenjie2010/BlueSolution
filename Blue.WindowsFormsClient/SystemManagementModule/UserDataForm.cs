using System;
using System.Collections.Generic;
using System.IO;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using Blue.Model.SystemModule;
using Blue.Model.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class UserDataForm : Form
    {
        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ISystemConfigContract systemConfigContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomGroupContract customGroupContract;

        #endregion

        #region 私有 变量

        private readonly bool passwordValidated;

        #endregion

        #region 属性

        /// <summary>
        /// 用户编号
        /// </summary>
        public decimal UserId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserDataForm()
        {
            InitializeComponent();

            userAccountContract = UserChannelFactory.CreateUserAccount();
            systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();

            IList<EnumItem> departmentProperties = SystemConfigHelper.GetDepartmentPorperty();
            ccmbDepartmentRange.Properties.Items.AddRange(departmentProperties.ToArray());
            cmbUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(customGroupContract, userTypeContract);
            cmbUserType.InitalizeTreeView();

            cmbDepartment.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
            cmbDepartment.InitalizeTreeView();

            IList<EnumItem> identificationTypes = SystemConfigHelper.GetIdentificationType();
            InitIdentificationType(identificationTypes);

            IList<EnumItem> systemDataFieldPermissions = UserEnumHelper.GetEnumItems(typeof(SystemDataFieldPermission));
            ccmbAuthority.Properties.Items.AddRange(systemDataFieldPermissions.ToArray());

            passwordValidated = Convert.ToBoolean(DataConvertionHelper.GetConvertedInt(systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.PasswordValidated), 0));
            upfPhoto.SkinName = "Blue";
            upfPhoto.Filter = "JPEG(*.JPG;*.JPEG)|*.JPG;*.JPEG|所有文件(*.*)|*.*";
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserDataForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string userName = txtUserName.Text.Trim();
            string userActualName = txtUserActualName.Text.Trim();
            string userPwd = txtUserPwd.Text.Trim();
            string userConfrimPwd = txtConfirmedUserPwd.Text.Trim();
            string emailAddress = txtEmailAddress.Text.Trim();
            string telephoneNumber = txtTelephoneNumber.Text.Trim();
            byte identificationType = Convert.ToByte(icmbIdentificationType.EditValue);
            string userIdentity = txtUserIdentity.Text.Trim();

            if (!string.IsNullOrWhiteSpace(userName) && userName.Contains('@'))
            {
                txtUserName.Focus();
                MessageBox.Show("用户名中不能含有非法字符@。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(emailAddress) && !UserDataHelper.MatchEmail(emailAddress))
            {
                txtEmailAddress.Focus();
                MessageBox.Show("请输入正确的电子邮件地址。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if (!string.IsNullOrWhiteSpace(telephoneNumber) && !UserDataHelper.MatchMobilePhoneNumber(telephoneNumber))
            //{
            //    txtTelephoneNumber.Focus();
            //    MessageBox.Show("请输入正确的手机号码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if (((IdentificationType)identificationType == IdentificationType.Identification) && !string.IsNullOrWhiteSpace(userIdentity)
                && !UserDataHelper.MatchIdentity(userIdentity))
            {

                txtUserIdentity.Focus();
                MessageBox.Show("请输入正确的身份证号码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CommonNode userTypeCommonNode = cmbUserType.Value as CommonNode;
            CommonNode departmentCommonNode = cmbDepartment.Value as CommonNode;
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

            string notes = txtNotes.Text.Trim();
            bool isLockedOut = chkLocked.Checked;

            Int64 dataFieldAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbAuthority);

            IList<CommonNode> userTypeCommonNodes = btxtUserTypeRange.Tag as IList<CommonNode>;
            IList<decimal> userTypeIds = CommonObjHelper.GetCommonNodeIds(userTypeCommonNodes);

            IList<CommonNode> depCommonNodes = btxtDepartmentRange.Tag as IList<CommonNode>;
            IList<decimal> departmentIds = CommonObjHelper.GetCommonNodeIds(depCommonNodes);
            Int64 deparmentAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDepartmentRange);

            IList<CommonNode> roleCommonNodes = btxtRole.Tag as IList<CommonNode>;
            IList<decimal> roleIds = CommonObjHelper.GetCommonNodeIds(roleCommonNodes);

            string address = CurrentUser.Instance.CurrentIPAdress.ToString();
            if (string.IsNullOrWhiteSpace(address))
            {
                address = "127.0.0.1";
            }

            if (userName.Length > AppSettingHelper.DefaultUserNameMaxLength || userName.Length < AppSettingHelper.DefaultUserNameMinLength)
            {
                txtUserName.Focus();
                MessageBox.Show(string.Format("用户名长度不符合要求({0}~{1}个字符）：", AppSettingHelper.DefaultUserNameMinLength, AppSettingHelper.DefaultUserNameMaxLength),
                    "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(userPwd) && string.IsNullOrWhiteSpace(userConfrimPwd))
            {
                txtUserPwd.Focus();
                MessageBox.Show("密码不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(userConfrimPwd) && string.IsNullOrWhiteSpace(userPwd))
            {
                txtConfirmedUserPwd.Focus();
                MessageBox.Show("确认密码不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!string.IsNullOrWhiteSpace(userPwd))
            {
                if (!userPwd.Equals(userConfrimPwd))
                {
                    txtUserPwd.Focus();
                    MessageBox.Show("密码和确认密码不一直,请重新输入。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (passwordValidated && !UserDataHelper.ValidatePasowdString(userPwd))
                {
                    MessageBox.Show("密码必须由数字、字母和特殊符号构成。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (userTypeCommonNode == null)
            {
                MessageBox.Show("用户类型不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (departmentCommonNode == null)
            {
                txtUserPwd.Focus();
                MessageBox.Show("单位不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            try
            {
                Cursor = Cursors.WaitCursor;
                string verifyResult = string.Empty;
                bool result = false;
                bool existUserName = true;
                bool existUserIdentity = true;
                if (string.IsNullOrWhiteSpace(userPwd))
                {
                    txtUserPwd.Focus();
                    Cursor = Cursors.Default;
                    MessageBox.Show("用户密码不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (userPwd.Length < AppSettingHelper.DefaultUserNameMinLength || userPwd.Length > AppSettingHelper.DefaultUserNameMaxLength)
                {
                    txtUserPwd.Focus();
                    Cursor = Cursors.Default;
                    MessageBox.Show(string.Format("用户密码长度范围在{0}位～{1}位。", AppSettingHelper.DefaultUserNameMinLength, AppSettingHelper.DefaultUserNameMaxLength),
                        "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                existUserName = userAccountContract.IsExistUserName(userName);
                if (existUserName)
                {
                    txtUserName.Focus();
                    Cursor = Cursors.Default;
                    MessageBox.Show("用户名已存在。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!string.IsNullOrWhiteSpace(userIdentity))
                {
                    existUserIdentity = userAccountContract.IsExistIdentity(ValidationMode.UserIdentity, userIdentity);
                    if (existUserIdentity)
                    {
                        txtUserIdentity.Focus();
                        Cursor = Cursors.Default;
                        MessageBox.Show("证件号码已存在。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (!string.IsNullOrWhiteSpace(telephoneNumber))
                {
                    existUserIdentity = userAccountContract.IsExistIdentity(ValidationMode.MobilePhone, telephoneNumber);
                    if (existUserIdentity)
                    {
                        txtTelephoneNumber.Focus();
                        Cursor = Cursors.Default;
                        MessageBox.Show("手机号码已存在。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                if (!string.IsNullOrWhiteSpace(emailAddress))
                {
                    existUserIdentity = userAccountContract.IsExistIdentity(ValidationMode.Email, emailAddress);
                    if (existUserIdentity)
                    {
                        txtEmailAddress.Focus();
                        Cursor = Cursors.Default;
                        MessageBox.Show("电子邮件已存在。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                UserAccountInfo userAccountInfo = new UserAccountInfo(0, departmentCommonNode.NodeId, userTypeCommonNode.NodeId, userName, userPwd, userActualName, emailAddress, identificationType, userIdentity,
                    telephoneNumber, DateTime.Now, address, photoSuffixName, isLockedOut, dataFieldAuthority, deparmentAuthority, Guid.NewGuid(), notes, 0, DateTime.Now, DateTime.Now);
                result = ValidationHelper.Validate<UserAccountInfo>(userAccountInfo, out verifyResult);
                if (result)
                {
                    userAccountContract.Insert(userAccountInfo, imageData, userTypeIds, departmentIds, roleIds);
                    
                    Cursor = Cursors.Default;
                    MessageBox.Show("增加用户成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                if (!result)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(verifyResult, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
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
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void upfPhoto_OnBrowseClick(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(upfPhoto.FileName))
                {
                    Image img = Image.FromFile(upfPhoto.FileName);
                    if (img != null)
                    {
                        MemoryStream ms = new MemoryStream();
                        img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                        peUser.EditValue = ms.ToArray();
                        //peUser.Image = img;
                    }
                }
                else
                {
                    ClearUserPhoto();
                }
            }
            catch (Exception ex)
            {
                ClearUserPhoto();
                MessageBox.Show(string.Format("加载照片失败：{0}", ex.Message), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 用户类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtUserTypeRange_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            IList<CommonNode> commonNodes = btxtUserTypeRange.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new UserTypeMultiSelectedItems(customGroupContract, userTypeContract, (byte)GroupType.UserType, true);
            frmMultiSelectedItems.Text = "用户类型选择";
            frmMultiSelectedItems.GetTreeNodeListDelegate = GetUserTypeNodeList;
            frmMultiSelectedItems.OperationTip = "提示：如果该用户管理所有用户类型，则不需要选择任何用户类型。当用户类型为空时，自动管理所有用户类型。";
            frmMultiSelectedItems.SetTokenEidtValues(commonNodes);
            frmMultiSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 单位选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtDepartmentRange_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            IList<CommonNode> commonNodes = btxtDepartmentRange.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new MultiSelectedItems(customDepartmentContract, false);
            frmMultiSelectedItems.Text = "单位选择";
            frmMultiSelectedItems.OnlyLeafSelected = false;
            frmMultiSelectedItems.GetTreeNodeListDelegate = GetDepartmentNodeList;
            frmMultiSelectedItems.OperationTip = "提示：如果该用户管理所有单位，则不需要选择任何单位。当单位为空时，自动管理所有单位。";
            frmMultiSelectedItems.SetTokenEidtValues(commonNodes);
            frmMultiSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 角色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtRole_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            IList<CommonNode> commonNodes = btxtRole.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new RoleMultiSelectedItems(customGroupContract, customRoleContract, (byte)GroupType.Role, true);
            frmMultiSelectedItems.Text = "角色选择";
            frmMultiSelectedItems.GetTreeNodeListDelegate = GetRoleList;
            frmMultiSelectedItems.OperationTip = "提示：请为该用户选择角色。";
            frmMultiSelectedItems.SetTokenEidtValues(commonNodes);
            frmMultiSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 获得选择的单位
        /// </summary>
        /// <param name="commonNodes"></param>
        private void GetDepartmentNodeList(IList<CommonNode> commonNodes)
        {
            btxtDepartmentRange.Tag = commonNodes;
            btxtDepartmentRange.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
        }

        /// <summary>
        /// 获得选择的角色
        /// </summary>
        /// <param name="commonNodes"></param>
        private void GetRoleList(IList<CommonNode> commonNodes)
        {
            btxtRole.Tag = commonNodes;
            btxtRole.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
        }

        /// <summary>
        /// 获得选择的用户类型
        /// </summary>
        /// <param name="commonNodes"></param>
        private void GetUserTypeNodeList(IList<CommonNode> commonNodes)
        {
            btxtUserTypeRange.Tag = commonNodes;
            btxtUserTypeRange.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
        }

        /// <summary>
        /// 清空用户图片
        /// </summary>
        private void ClearUserPhoto()
        {
            peUser.Image = null;
            peUser.EditValue = null;
            upfPhoto.FileName = string.Empty;
        }

        /// <summary>
        /// 初始化证件属性
        /// </summary>
        /// <param name="identificationTypes"></param>
        public void InitIdentificationType(IList<EnumItem> identificationTypes)
        {
            for (int i = 0; i < identificationTypes.Count; i++)
            {
                int imageIndx = (i > 2) ? 2 : i;
                ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(identificationTypes[i].Text, (byte)identificationTypes[i].Value, imageIndx);
                icmbIdentificationType.Properties.Items.Add(imageComboBoxItem);
            }
            icmbIdentificationType.SelectedIndex = 0;
        }

        #endregion

    }
}
