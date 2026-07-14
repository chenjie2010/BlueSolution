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


namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class SingleUserForm : Form
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

        private readonly IList<EnumItem> identificationTypes = null;

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
        /// 
        /// </summary>
        public SingleUserForm()
        {
            InitializeComponent();

            userAccountContract = UserChannelFactory.CreateUserAccount();
            systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();

            IList<CommonNode> commonNodes = userTypeContract.GetCommonNodes(false);
            cmbUserType.Properties.Items.AddRange(commonNodes.ToArray());
            cmbDepartment.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
            cmbDepartment.InitalizeTreeView();

            identificationTypes = SystemConfigHelper.GetIdentificationType();
            UserControlHelper.InitImageComboBoxEdit(icmbIdentificationType, identificationTypes);
        }

        #endregion

        #region 窗体加载方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SingleUserForm_Load(object sender, EventArgs e)
        {
            ShowDataOnControls(UserId);
        }

        /// <summary>
        /// 确定操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                UserAccountInfo userAccountInfo = userAccountContract.GetModelInfo(UserId);
                if (userAccountInfo == null)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("该用户不存在，无法修改信息。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                userAccountInfo.UserPwd = string.Empty;
                string newUserName = txtNewUserName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(newUserName))
                {
                    if (!userAccountInfo.UserName.Equals(newUserName))
                    {
                        bool existUserName = userAccountContract.IsExistUserName(newUserName);
                        if (existUserName)
                        {
                            txtUserName.Focus();
                            Cursor = Cursors.Default;
                            MessageBox.Show("用户名已存在。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    userAccountInfo.UserName = newUserName;
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
                if (!string.IsNullOrWhiteSpace(upfPhoto.FileName))
                {
                    userAccountInfo.PhotoSuffixName = upfPhoto.FileName.Substring(upfPhoto.FileName.LastIndexOf('.') + 1).ToUpper();
                }
                string userActualName = txtNewUserActualName.Text.Trim();
                if (!string.IsNullOrWhiteSpace(userActualName))
                {
                    userAccountInfo.UserActualName = userActualName;
                }
                string newEmailAddress = txtNewEmailAddress.Text.Trim();
                if (!string.IsNullOrWhiteSpace(newEmailAddress))
                {
                    if (!UserDataHelper.MatchEmail(newEmailAddress))
                    {
                        txtNewEmailAddress.Focus();
                        MessageBox.Show("请输入正确的电子邮件地址。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }                    
                    if (!userAccountInfo.EmailAddress.Equals(newEmailAddress))
                    {
                        bool existUserIdentity = userAccountContract.IsExistIdentity(ValidationMode.Email, newEmailAddress);
                        if (existUserIdentity)
                        {
                            txtEmailAddress.Focus();
                            Cursor = Cursors.Default;
                            MessageBox.Show("电子邮件已存在。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    userAccountInfo.EmailAddress = newEmailAddress;
                }
                string newTelephoneNumber = txtNewTelephoneNumber.Text.Trim();
                if (!string.IsNullOrWhiteSpace(newTelephoneNumber))
                {
                    if (!userAccountInfo.TelephoneNumber.Equals(newTelephoneNumber))
                    {
                        bool existUserIdentity = userAccountContract.IsExistIdentity(ValidationMode.MobilePhone, newTelephoneNumber);
                        if (existUserIdentity)
                        {
                            txtTelephoneNumber.Focus();
                            Cursor = Cursors.Default;
                            MessageBox.Show("手机号码已存在。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                    userAccountInfo.TelephoneNumber = newTelephoneNumber;
                }
                string newUserIdentity = txtNewUserIdentity.Text.Trim();
                if (!string.IsNullOrWhiteSpace(newUserIdentity))
                {
                    byte identificationType = Convert.ToByte(icmbIdentificationType.EditValue);
                    if (((IdentificationType)identificationType == IdentificationType.Identification) &&  !UserDataHelper.MatchIdentity(newUserIdentity))
                    {

                        txtUserIdentity.Focus();
                        MessageBox.Show("请输入正确的身份证号码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (!userAccountInfo.UserIdentity.Equals(newUserIdentity))
                    {
                        bool existUserIdentity = userAccountContract.IsExistIdentity(ValidationMode.UserIdentity, newUserIdentity);
                        if (existUserIdentity)
                        {
                            txtNewUserIdentity.Focus();
                            Cursor = Cursors.Default;
                            MessageBox.Show("证件号码已存在。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }                    
                    userAccountInfo.IdentificationType = identificationType;
                    userAccountInfo.UserIdentity = newUserIdentity;
                }
                if (cmbDepartment.Value != null)
                {
                    CommonNode departmentCommonNode = cmbDepartment.Value as CommonNode;
                    userAccountInfo.DepId = departmentCommonNode.NodeId;
                }
                if (cmbUserType.SelectedItem != null)
                {
                    CommonNode userTypeCommonNode = cmbUserType.SelectedItem as CommonNode;
                    userAccountInfo.UserTypeId = userTypeCommonNode.NodeId;
                }
                IList<decimal> roleIds = null;
                if (btxtNewRole.Tag != null)
                {
                    IList<CommonNode> roleCommonNodes = btxtNewRole.Tag as IList<CommonNode>;
                    roleIds = CommonObjHelper.GetCommonNodeIds(roleCommonNodes);
                }
                //else
                //{
                //    IList<CommonNode> roleCommonNodes = userAccountContract.GetRoles(UserId);
                //    foreach (var roleCommonNode in roleCommonNodes)
                //    {
                //        roleIds.Add(roleCommonNode.NodeId);
                //    }
                //}
                //IList<decimal> userTypeIds = new List<decimal>();
                //IList<decimal> departmentIds = new List<decimal>();
                userAccountContract.Update(userAccountInfo, imageData, null, null, roleIds);
                Cursor = Cursors.Default;
                MessageBox.Show("修改用户成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 角色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtNewRole_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            IList<CommonNode> commonNodes = customRoleContract.GetCommonNodes(false);
            CheckedSelectedItemsForm frmCheckedSelectedItems = new CheckedSelectedItemsForm();
            frmCheckedSelectedItems.Text = "角色选择";
            frmCheckedSelectedItems.MultiNodeSelected = delegate (IList<CommonNode> selectedNodes)
            {
                btxtNewRole.Tag = selectedNodes;
                btxtNewRole.Text = CommonObjHelper.GetCommonNodeNamesWithComma(selectedNodes);
            };
            frmCheckedSelectedItems.LoadAndSetCommonNodes(commonNodes);
            frmCheckedSelectedItems.ShowDialog();


            //MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            //frmMultiSelectedItems.MultiSelectedItemsHandler = new RoleMultiSelectedItems(customGroupContract, customRoleContract, (byte)GroupType.Role, true);
            //frmMultiSelectedItems.Text = "角色选择";
            //frmMultiSelectedItems.CommonList = btxtNewRole.Tag as IList<CommonNode>;
            //frmMultiSelectedItems.GetTreeNodeListDelegate = GetRoleList;
            //frmMultiSelectedItems.OperationTip = "提示：请为该用户选择角色。";
            //frmMultiSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 上传照片
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
        /// 加载默认数据到窗体左侧的控件中
        /// </summary>
        /// <param name="userId">节点编号</param>
        private void ShowDataOnControls(decimal userId)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(userId);
                UserAccountInfo userAccountInfo = userAccountContract.GetModelInfo(userId);
                if (commonUserInfo != null)
                {
                    txtUserName.Text = commonUserInfo.UserName;
                    txtUserActualName.Text = commonUserInfo.UserActualName;
                    txtTelephoneNumber.Text = userAccountInfo.TelephoneNumber;
                    txtEmailAddress.Text = userAccountInfo.EmailAddress;
                    int pos = identificationTypes.FindIndex(item => item.Value == userAccountInfo.IdentificationType);
                    txtUserIdentity.Text = string.Format("({0}){1}", identificationTypes[pos].Text, userAccountInfo.UserIdentity);
                    txtDepartment.Text = commonUserInfo.DepName;
                    txtUserType.Text = commonUserInfo.UserTypeName;
                    txtRole.Text = string.Empty;
                    txtDepValue.Text = commonUserInfo.DepValue;
                    IList<CommonNode> roleCommonNodes = userAccountContract.GetRoles(commonUserInfo.UserId);
                    txtRole.Text = CommonObjHelper.GetCommonNodeNamesWithComma(roleCommonNodes);
                    byte[] data = userAccountContract.DownLoadPhoto(commonUserInfo.UserName);
                    if (data != null)
                    {
                        using (MemoryStream ms = new MemoryStream(data))
                        {
                            Image img = Image.FromStream(ms);
                            peUser.Image = img;
                        }
                    }
                    else
                    {
                        peUser.Image = null;
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

        /// <summary>
        /// 获得选择的角色
        /// </summary>
        /// <param name="commonNodes"></param>
        private void GetRoleList(IList<CommonNode> commonNodes)
        {
            btxtNewRole.Tag = commonNodes;
            btxtNewRole.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
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

        #endregion
    }
}
