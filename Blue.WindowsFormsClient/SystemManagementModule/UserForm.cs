using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Excel;
using FarPoint.Win.Spread.CellType;
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
    public partial class UserForm : Form
    {
        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ISystemConfigContract systemConfigContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomGroupContract customGroupContract;

        #endregion

        #region 私有变量

        private EditState currentEditState = EditState.None;
        private readonly bool passwordValidated;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserForm()
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

            cmbQueriedUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(customGroupContract, userTypeContract);
            cmbQueriedUserType.InitalizeTreeView();
            cmbUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(customGroupContract, userTypeContract);
            cmbUserType.InitalizeTreeView();

            cmbQueriedDepartment.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
            cmbQueriedDepartment.InitalizeTreeView();
            cmbDepartment.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
            cmbDepartment.InitalizeTreeView();

            IList<EnumItem> identificationTypes = SystemConfigHelper.GetIdentificationType();
            InitIdentificationType(identificationTypes);

            IList<EnumItem> systemDataFieldPermissions = UserEnumHelper.GetEnumItems(typeof(SystemDataFieldPermission));
            ccmbAuthority.Properties.Items.AddRange(systemDataFieldPermissions.ToArray());

            passwordValidated = Convert.ToBoolean(DataConvertionHelper.GetConvertedInt(systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.PasswordValidated), 0));
            upfPhoto.SkinName = "Blue";
            upfPhoto.Filter = "JPEG(*.JPG;*.JPEG)|*.JPG;*.JPEG| PNG(*.PNG)|*.PNG| 所有文件(*.*)|*.*";
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            SetActiveStatesOfControls(true);
            //this.BeginInvoke(new MethodInvoker(LoadData));
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearQueriedOnControls();
            ClearDataOnControls();
            SetActiveStatesOfControls(true);
            for (int idx = grdUsers.Columns.Count - 1; idx >= 0; idx--)
            {
                grdUsers.Columns.RemoveAt(idx);
            }
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCreate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentEditState = EditState.Add;
            ClearDataOnControls();
            SetActiveStatesOfControls(false);
            txtUserName.Focus();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentEditState = EditState.Edit;
            SetActiveStatesOfControls(false);
            txtUserName.Focus();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (grdUsers.FocusedRowHandle < 0)
                {
                    MessageBox.Show("请先选择需要删除的用户。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认删除所选择的用户吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    decimal userId = DataConvertionHelper.GetDecimal(grdUsers.GetRowCellValue(grdUsers.FocusedRowHandle, "UserId"));
                    if (CurrentUser.Instance.UserId == userId)
                    {
                        MessageBox.Show("不能删除自己的帐号。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Cursor = Cursors.WaitCursor;
                    userAccountContract.Delete(userId);
                    if (grdUsers.RowCount == 1 && grdUsers.CurrentPageIndex > 0)
                    {
                        grdUsers.CurrentPageIndex--;
                    }
                    LoadData();
                    Cursor = Cursors.Default;
                    MessageBox.Show("成功删除所选择的用户。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiBatchDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (grdUsers.MultiSelectedValues.Count == 0)
                {
                    MessageBox.Show("请先选择需要批量删除的用户。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认批量删除所选择的帐号吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    IList<decimal> userIds = new List<decimal>(grdUsers.MultiSelectedValues.Count);
                    foreach (RowEvent rowEvent in grdUsers.MultiSelectedValues)
                    {
                        decimal userId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                        if (CurrentUser.Instance.UserId == userId)
                        {
                            MessageBox.Show("被批量删除帐号包含自己的账号，不允许删除。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        userIds.Add(userId);
                    }
                    Cursor = Cursors.WaitCursor;
                    userAccountContract.Delete(userIds);
                    grdUsers.CurrentPageIndex = 0;
                    LoadData();
                    Cursor = Cursors.Default;
                    MessageBox.Show("成功批量删除所选择的帐号。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Query();
            }
        }

        /// <summary>
        /// 冻结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiLock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdUsers.FocusedRowHandle < 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("请先选择需要冻结的用户。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确认冻结所选择的用户吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                IList<decimal> userIds = new List<decimal>();
                decimal userId = DataConvertionHelper.GetDecimal(grdUsers.DataKeyValues.Value);
                if (CurrentUser.Instance.UserId == userId)
                {
                    MessageBox.Show("不能冻结自己的帐号。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                userIds.Add(userId);
                userAccountContract.LockUser(userId, true);
                grdUsers.SetRowCellValue(grdUsers.FocusedRowHandle, "LockedOut", true);
                MessageBox.Show("成功冻结所选择的用户。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 解冻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiUnLock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdUsers.FocusedRowHandle < 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("请先选择需要解冻的用户。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确认解冻所选择的用户吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                IList<decimal> userIds = new List<decimal>();
                decimal userId = DataConvertionHelper.GetDecimal(grdUsers.DataKeyValues.Value);
                if (CurrentUser.Instance.UserId == userId)
                {
                    MessageBox.Show("不能解冻自己的帐号。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                userIds.Add(userId);
                userAccountContract.LockUser(userId, false);
                grdUsers.SetRowCellValue(grdUsers.FocusedRowHandle, "LockedOut", false);
                MessageBox.Show("成功解冻所选择的用户。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }            
        }

        /// <summary>
        /// 批量冻结
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiBatchLock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdUsers.MultiSelectedValues.Count == 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("请先选择需要批量冻结的用户。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确认批量冻结所选择的帐号吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                IList<decimal> userIds = new List<decimal>(grdUsers.MultiSelectedValues.Count);
                foreach (RowEvent rowEvent in grdUsers.MultiSelectedValues)
                {
                    decimal userId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                    if (CurrentUser.Instance.UserId == userId)
                    {
                        MessageBox.Show("被批量冻结帐号包含自己的账号，不允许冻结。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    userIds.Add(userId);
                }
                userAccountContract.LockUsers(userIds, true);
                foreach (RowEvent rowEvent in grdUsers.MultiSelectedValues)
                {
                    grdUsers.SetRowCellValue(rowEvent.RowHandle, "LockedOut", true);
                }
                grdUsers.ClearMultiSelectedCheckbox();
                MessageBox.Show("成功批量冻结所选择的帐号。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 批量解冻
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiBatchUnLock_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (grdUsers.MultiSelectedValues.Count == 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("请先选择需要批量解冻的用户。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确认批量解冻所选择的帐号吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                IList<decimal> userIds = new List<decimal>(grdUsers.MultiSelectedValues.Count);
                foreach (RowEvent rowEvent in grdUsers.MultiSelectedValues)
                {
                    decimal userId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                    if (CurrentUser.Instance.UserId == userId)
                    {
                        MessageBox.Show("被批量解冻帐号包含自己的账号，不允许解冻。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    userIds.Add(userId);
                }
                userAccountContract.LockUsers(userIds, false);
                foreach (RowEvent rowEvent in grdUsers.MultiSelectedValues)
                {
                    grdUsers.SetRowCellValue(rowEvent.RowHandle, "LockedOut", false);
                }
                grdUsers.ClearMultiSelectedCheckbox();
                MessageBox.Show("成功批量解冻所选择的帐号。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        
        /// <summary>
        /// 角色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtNewRole_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            IList<CommonNode> nodes = btxtRole.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new RoleMultiSelectedItems(customGroupContract, customRoleContract, (byte)GroupType.Role, true);
            frmMultiSelectedItems.Text = "角色选择";
            frmMultiSelectedItems.GetTreeNodeListDelegate = (commonNodes) =>
            {
                btxtNewRole.Tag = commonNodes;
                btxtNewRole.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
            };
            frmMultiSelectedItems.OperationTip = "提示：请为该用户选择角色。";
            frmMultiSelectedItems.SetTokenEidtValues(nodes);
            frmMultiSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 导入导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExchange_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserToolForm frmUserTool = new UserToolForm();
            frmUserTool.ShowDialog();
        }

        /// <summary>
        /// 显示数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdUsers_OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
           
        }

        /// <summary>
        /// 行点击事件，显示数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdUsers_OnRowClick(object sender, RowEvent e)
        {
            decimal userId = DataConvertionHelper.GetDecimal(grdUsers.GetRowCellValue(e.RowHandle, "UserId"));
            ShowDataOnControls(userId);
            SetActiveStatesOfControls(true);
        }

        /// <summary>
        /// 翻页操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdUsers_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            grdUsers.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 确定操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnConfirm_Click(object sender, EventArgs e)
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
            if (!string.IsNullOrWhiteSpace(telephoneNumber) && !UserDataHelper.MatchMobilePhoneNumber(telephoneNumber))
            {
                txtTelephoneNumber.Focus();
                MessageBox.Show("请输入正确的手机号码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
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
                bool success = FileFormatHelper.VerfiyJPGFormat(upfPhoto.FileName) || FileFormatHelper.VerfiyPNGFormat(upfPhoto.FileName);
                if (!success)
                {
                    upfPhoto.Focus();
                    MessageBox.Show("图片格式只能为JPG或者PNG。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                UserAccountInfo userAccountInfo = null;
                switch (currentEditState)
                {
                    case EditState.Add:
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
                        userAccountInfo = new UserAccountInfo(0, departmentCommonNode.NodeId, userTypeCommonNode.NodeId, userName, userPwd, userActualName, emailAddress, identificationType, userIdentity,
                            telephoneNumber, DateTime.Now, address, photoSuffixName, isLockedOut, dataFieldAuthority, deparmentAuthority, Guid.NewGuid(), notes, 0, DateTime.Now, DateTime.Now);
                        result = ValidationHelper.Validate<UserAccountInfo>(userAccountInfo, out verifyResult);
                        if (result)
                        {
                            userAccountContract.Insert(userAccountInfo, imageData, userTypeIds, departmentIds, roleIds);
                            if (grdUsers.RecordCount % grdUsers.PageSize == 0)
                            {
                                grdUsers.CurrentPageIndex = grdUsers.PageCount;
                            }
                            else
                            {
                                grdUsers.CurrentPageIndex = grdUsers.PageCount - 1;
                            }
                            LoadData();
                            grdUsers.FocusedRowHandle = grdUsers.CurrentPageSize - 1;
                            SetActiveStatesOfControls(true);
                            Cursor = Cursors.Default;
                            MessageBox.Show("增加用户成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case EditState.Edit:
                        if (grdUsers.FocusedRowHandle < 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("请先选择需要编辑的用户。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        decimal userId = DataConvertionHelper.GetDecimal(grdUsers.GetRowCellValue(grdUsers.FocusedRowHandle, "UserId"));
                        UserAccountInfo oldUserAccountInfo = userAccountContract.GetModelInfo(userId);
                        if (oldUserAccountInfo == null)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("该用户不存在，请重新选择。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (!oldUserAccountInfo.UserName.Equals(userName))
                        {
                            existUserName = userAccountContract.IsExistUserName(userName);
                            if (existUserName)
                            {
                                txtUserName.Focus();
                                Cursor = Cursors.Default;
                                MessageBox.Show("用户名已存在。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }                       
                        if (!string.IsNullOrWhiteSpace(userIdentity) && !oldUserAccountInfo.UserIdentity.Equals(userIdentity))
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
                        if (!string.IsNullOrWhiteSpace(telephoneNumber) && !oldUserAccountInfo.TelephoneNumber.Equals(telephoneNumber))
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
                        if (!string.IsNullOrWhiteSpace(emailAddress) && !oldUserAccountInfo.EmailAddress.Equals(emailAddress))
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
                        userAccountInfo = new UserAccountInfo(userId, departmentCommonNode.NodeId, userTypeCommonNode.NodeId, userName, userPwd, userActualName, emailAddress, identificationType, userIdentity,
                           telephoneNumber, DateTime.Now, address, photoSuffixName, isLockedOut, dataFieldAuthority, deparmentAuthority, Guid.NewGuid(), notes, 0, DateTime.Now, DateTime.Now);
                        result = ValidationHelper.Validate<UserAccountInfo>(userAccountInfo, out verifyResult);
                        if (result)
                        {
                            userAccountContract.Update(userAccountInfo, imageData, userTypeIds, departmentIds, roleIds);
                            if (grdUsers.FocusedRowHandle >= 0)
                            {
                                grdUsers.SetRowCellValue(grdUsers.FocusedRowHandle, "UserName", userName);
                                grdUsers.SetRowCellValue(grdUsers.FocusedRowHandle, "UserActualName", userActualName);
                                grdUsers.SetRowCellValue(grdUsers.FocusedRowHandle, "DepName", departmentCommonNode.NodeName);
                                grdUsers.SetRowCellValue(grdUsers.FocusedRowHandle, "UserTypeName", userTypeCommonNode.NodeName);
                                grdUsers.SetRowCellValue(grdUsers.FocusedRowHandle, "IsLockedOut", isLockedOut);
                            }
                            SetActiveStatesOfControls(true);
                            Cursor = Cursors.Default;
                            MessageBox.Show("修改用户成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    default:
                        break;
                }
                if (!result)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(verifyResult, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                currentEditState = EditState.None;
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
            decimal userId = DataConvertionHelper.GetDecimal(grdUsers.GetRowCellValue(grdUsers.FocusedRowHandle, "UserId"));
            ShowDataOnControls(userId);
            SetActiveStatesOfControls(true);
            currentEditState = EditState.None;
        }

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
            catch(Exception ex)
            {
                ClearUserPhoto();
                MessageBox.Show(string.Format("加载照片失败：{0}", ex.Message), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 导入照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiImportPhoto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UserPhotoForm frmUserPhoto = new UserPhotoForm();
            frmUserPhoto.ShowDialog();
        }
        
        /// <summary>
        /// 远程同步用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiRemoteUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RemoteUserForm frmRemoteUser = new RemoteUserForm();
            frmRemoteUser.RefreshForm += () =>
            {                
                ClearQueriedOnControls();
                ClearDataOnControls();
                SetActiveStatesOfControls(true);
                grdUsers.FocusedRowHandle = -1;
                grdUsers.CurrentPageIndex = 0;
                LoadData();
            };
            frmRemoteUser.ShowDialog();
        }

        /// <summary>
        /// 导出用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExportedUser_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = string.Format("用户导出_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            dlg.Filter = AppSettingHelper.DefaultExcelFormat;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                Cursor = Cursors.Default;
                return;
            }
            Cursor = Cursors.WaitCursor;

            int totalCount = grdUsers.RecordCount;
            if (totalCount == 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("用户记录为空，无法导出。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            IList<WhereConditon> whereConditons = GetWhereConditons();
            ProgressForm frmProgress = new ProgressForm();
            bool exit = false;
            int pageSize = 500;
            int progress = totalCount / pageSize;
            int steps = ((totalCount % pageSize) == 0) ? (totalCount / pageSize) : (totalCount / pageSize + 1);
            if (progress > 0)
            {
                frmProgress.MinValue = 0;
                frmProgress.MaxValue = steps;
                frmProgress.Show();
            }
            int maxCountPerSheet = 60000;
            int pageCount = maxCountPerSheet / pageSize;
            int sheetCount = ((totalCount % maxCountPerSheet) == 0) ? (totalCount / maxCountPerSheet) : (totalCount / maxCountPerSheet + 1);
            FpSpread fsExcel = new FpSpread();
            for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
            {
                SheetView sheetView = new SheetView(string.Format("{0}_{1}", this.Text, sheetIndex));
                if (totalCount > maxCountPerSheet)
                {
                    sheetView.RowCount = maxCountPerSheet;
                }
                else
                {
                    sheetView.RowCount = totalCount;
                }
                fsExcel.Sheets.Add(sheetView);
                DataTable tables = null;
                for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                {
                    int start = pageIndex * pageSize;
                    int pos = (sheetIndex * maxCountPerSheet) + start;
                    if (pos >= totalCount)
                    {
                        break;
                    }
                    DataTable dataTable = null;
                    if (btxtNewRole.Tag != null)
                    {
                        dataTable = userAccountContract.GetPageRecordOfMultiTablesWithRole(pos, pageSize, whereConditons, ref totalCount).Tables[0];
                    }
                    else
                    {
                        dataTable = userAccountContract.GetPageRecordOfMultiTables(pos, pageSize, whereConditons, ref totalCount).Tables[0];
                    }
                    if (dataTable != null && dataTable.Rows.Count > 0)
                    {
                        /* 移除用户编号列 */
                        dataTable.Columns.RemoveAt(0);
                        if (tables == null)
                        {
                            tables = dataTable;
                        }
                        else
                        {
                            foreach (DataRow dr in dataTable.Rows)
                            {
                                tables.ImportRow(dr);
                            }
                        }
                    }
                    if (progress > 0)
                    {
                        frmProgress.Tip = string.Format("正在导出第{0}个页面的数据，请稍后...", sheetIndex * pageCount + pageIndex + 1);
                        frmProgress.IncreaseStep();
                        if (frmProgress != null && frmProgress.Cancel)
                        {
                            exit = true;
                            break; ;
                        }
                    }
                }
                if (tables != null)
                {
                    fsExcel.Sheets[sheetIndex].DataSource = tables;
                    fsExcel.Sheets[sheetIndex].Cells[0, 0, tables.Rows.Count - 1, tables.Columns.Count - 2].CellType = new TextCellType();
                }
                if (exit)
                {
                    frmProgress.CloseFrom();
                    frmProgress = null;
                    break;
                }
            }
            if (frmProgress != null && !frmProgress.IsDisposed)
            {
                frmProgress.CloseFrom();
            }
            fsExcel.SaveExcel(dlg.FileName, ExcelSaveFlags.DataOnly | ExcelSaveFlags.SaveCustomColumnHeaders | ExcelSaveFlags.UseOOXMLFormat);
            Cursor = Cursors.Default;
            MessageBox.Show("Excel 文件导出成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            grdUsers.CurrentPageIndex = 0;
            this.BeginInvoke(new MethodInvoker(LoadData));
            SetActiveStatesOfControls(true);
        }

        /// <summary>
        /// 清除窗体左侧的控件中的数据
        /// </summary>
        private void ClearQueriedOnControls()
        {
            txtCondition.Text = string.Empty;
            chkNewLocked.Checked = false;
            cmbQueriedUserType.Value = null;
            cmbQueriedDepartment.Value = null;
            btxtNewRole.Tag = null;
            btxtNewRole.Text = string.Empty;
            txtNewNotes.Text = string.Empty;
        }
                
        /// <summary>
        /// 清除窗体左侧的控件中的数据
        /// </summary>
        private void ClearDataOnControls()
        {
            txtUserName.Text = string.Empty;
            txtUserPwd.Text = string.Empty;
            txtConfirmedUserPwd.Text = string.Empty;
            txtUserActualName.Text = string.Empty;
            txtEmailAddress.Text = string.Empty;
            txtTelephoneNumber.Text = string.Empty;
            icmbIdentificationType.SelectedIndex = 0;
            txtUserIdentity.Text = string.Empty;
            cmbUserType.SelectedNode = null;
            cmbUserType.Tag = null;
            cmbDepartment.SelectedNode = null;
            cmbDepartment.Tag = null;            
            chkLocked.Checked = false;
            txtNotes.Text = string.Empty;

            ClearUserPhoto();

            ccmbAuthority.EditValue = null;
            btxtUserTypeRange.EditValue = string.Empty;
            btxtDepartmentRange.EditValue = string.Empty;
            ccmbDepartmentRange.EditValue = null;
            btxtRole.EditValue = string.Empty;
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        private void SetActiveStatesOfControls(bool readOnly)
        {
            txtUserName.ReadOnly = readOnly;
            txtUserPwd.ReadOnly = readOnly;
            txtConfirmedUserPwd.ReadOnly = readOnly;
            txtUserActualName.ReadOnly = readOnly;
            txtEmailAddress.ReadOnly = readOnly;
            txtTelephoneNumber.ReadOnly = readOnly;
            icmbIdentificationType.Properties.ReadOnly = readOnly;
            txtUserIdentity.ReadOnly = readOnly;
            cmbUserType.ReadOnly = readOnly;
            cmbDepartment.ReadOnly = readOnly;
            upfPhoto.ReadOnly = readOnly;
            chkLocked.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            peUser.ReadOnly = readOnly;
            ccmbAuthority.ReadOnly = readOnly;
            btxtUserTypeRange.ReadOnly = readOnly;
            btxtUserTypeRange.Properties.Buttons[0].Enabled = !readOnly;
            btxtDepartmentRange.ReadOnly = readOnly;
            btxtDepartmentRange.Properties.Buttons[0].Enabled = !readOnly;
            ccmbDepartmentRange.ReadOnly = readOnly;
            btxtRole.ReadOnly = readOnly;
            btxtRole.Properties.Buttons[0].Enabled = !readOnly;

            sbtnConfirm.Enabled = !readOnly;
            sbtnCancel.Enabled = !readOnly;
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

        /// <summary>
        /// 数据加载
        /// </summary>
        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();
                progressPanel.Show();
                IList<WhereConditon> whereConditons = GetWhereConditons();
                int totalCount = 0;
                DataTable data = null;
                if (btxtNewRole.Tag != null)
                {
                    data  = userAccountContract.GetPageRecordOfMultiTablesWithRole(grdUsers.PageSize * grdUsers.CurrentPageIndex,
                        grdUsers.PageSize, whereConditons, ref totalCount).Tables[0];
                }
                else
                {
                    data = userAccountContract.GetPageRecordOfMultiTables(grdUsers.PageSize * grdUsers.CurrentPageIndex,
                       grdUsers.PageSize, whereConditons, ref totalCount).Tables[0];
                }
                if (data.Columns.Contains("EmailAddress"))
                {
                    data.Columns.Remove("EmailAddress");
                }
                if (data.Columns.Contains("TelephoneNumber"))
                {
                    data.Columns.Remove("TelephoneNumber");
                }
                grdUsers.DataSource = data;
                grdUsers.RecordCount = totalCount;
                IList<EnumItem> identificationTypes = UserEnumHelper.GetEnumItems(typeof(IdentificationType));
                RepositoryItemImageComboBox repositoryItemImageComboBox = UserControlHelper.GetImageComboBoxOnColumnEdit(identificationTypes, icIdentificationType);
                this.grdUsers.DevExpressGridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemImageComboBox });
                grdUsers.Columns["IdentificationType"].ColumnEdit = repositoryItemImageComboBox;                
                if (grdUsers.RowCount > 0)
                {
                    grdUsers.FocusedRowHandle = 0;
                    bbiEdit.Enabled = true;
                    blcDelete.Enabled = true;
                    blcLock.Enabled = true;
                    if (grdUsers.RowCount == 1)
                    {
                        decimal userId = DataConvertionHelper.GetDecimal(grdUsers.GetRowCellValue(grdUsers.FocusedRowHandle, "UserId"));
                        ShowDataOnControls(userId);
                    }
                }
                else
                {
                    bbiEdit.Enabled = false;
                    blcDelete.Enabled = false;
                    blcLock.Enabled = false;
                }
                progressPanel.Hide();
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 获得 WHERE 查询条件
        /// </summary>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons()
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            string condition = txtCondition.Text;
            if (!string.IsNullOrWhiteSpace(condition))
            {
                string userName = Regex.Replace(condition, " {1,}", "%");
                whereConditons.Add(new WhereConditon("UserAccount", "UserName", "UserName", System.Data.DbType.String, userName,
                   DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                whereConditons.Add(new WhereConditon("UserAccount", "EmailAddress", "EmailAddress", System.Data.DbType.String, userName,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("UserAccount", "UserIdentity", "UserIdentity", System.Data.DbType.String, userName,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                if (UserDataHelper.StartWithMobilePhoneNumber(condition))
                {
                    whereConditons.Add(new WhereConditon("UserAccount", "TelephoneNumber", "TelephoneNumber", System.Data.DbType.String, userName,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                }
                string userActualName = Regex.Replace(condition, " {1,}", "%");
                whereConditons.Add(new WhereConditon("UserAccount", "UserActualName", "UserActualName", System.Data.DbType.String, userActualName,
                   DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
            }

            string notes = txtNewNotes.Text.Trim();
            if (!string.IsNullOrWhiteSpace(notes))
            {
                string newNotes = Regex.Replace(notes, " {1,}", "%");
                whereConditons.Add(new WhereConditon("UserAccount", "Notes", "Notes", System.Data.DbType.String, newNotes,
                   DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            /* 用户类型 */
            CommonNode userTypeCommonNode = cmbQueriedUserType.Value as CommonNode;
            if (userTypeCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("UserAccount", "UserTypeId", "UserTypeId", System.Data.DbType.Decimal, userTypeCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            /* 单位类型 */
            CommonNode departmentCommonNode = cmbQueriedDepartment.Value as CommonNode;
            if (departmentCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("UserAccount", "DepId", "DepId", System.Data.DbType.Decimal, departmentCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            /* 角色 */
            if (btxtNewRole.Tag != null)
            {
                IList<CommonNode> commonNodes = btxtNewRole.Tag as IList<CommonNode>;
                if (commonNodes != null && commonNodes.Count > 0)
                {
                    List<decimal> nodeIds = new List<decimal>(commonNodes.Count);
                    foreach (var commonNode in commonNodes)
                    {
                        nodeIds.Add(commonNode.NodeId);
                    }
                    IList<WhereConditon> conditons = DataAccessHandler.GetWhereConditons(nodeIds, "RoleAndUser", "RoleId");
                    foreach (var conditon in conditons)
                    {
                        whereConditons.Add(conditon);
                    }
                }
            }

            /* 是否冻结 */
            if (chkNewLocked.Checked)
            {
                whereConditons.Add(new WhereConditon("UserAccount", "LockedOut", "LockedOut", System.Data.DbType.Boolean, true,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            //btxtNewRole.Text = string.Empty;

            return whereConditons;
        }

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
                    txtUserPwd.Text = string.Empty;
                    txtConfirmedUserPwd.Text = string.Empty;
                    txtUserActualName.Text = userAccountInfo.UserActualName;
                    txtEmailAddress.Text = userAccountInfo.EmailAddress;
                    txtTelephoneNumber.Text = userAccountInfo.TelephoneNumber;
                    icmbIdentificationType.EditValue = (byte)userAccountInfo.IdentificationType;
                    txtUserIdentity.Text = userAccountInfo.UserIdentity;
                    cmbUserType.Value = userTypeContract.GetCommonNode(userAccountInfo.UserTypeId);
                    cmbDepartment.Value = customDepartmentContract.GetCommonNode(userAccountInfo.DepId);
                    chkLocked.Checked = userAccountInfo.LockedOut;
                    txtNotes.Text = userAccountInfo.Notes;

                    UserControlHelper.SetCheckedComboBoxEditItems(ccmbAuthority, userAccountInfo.DataFieldAuthority);
                    UserControlHelper.SetCheckedComboBoxEditItems(ccmbDepartmentRange, userAccountInfo.DepartmentAuthority);

                    IList<CommonNode> userTypeCommonNodes = userAccountContract.GetUserTypes(userAccountInfo.UserId);
                    btxtUserTypeRange.Tag = userTypeCommonNodes;
                    btxtUserTypeRange.Text = CommonObjHelper.GetCommonNodeNamesWithComma(userTypeCommonNodes);

                    IList<CommonNode> departmentCommonNodes = userAccountContract.GetDepartments(userAccountInfo.UserId);
                    btxtDepartmentRange.Tag = departmentCommonNodes;
                    btxtDepartmentRange.Text = CommonObjHelper.GetCommonNodeNamesWithComma(departmentCommonNodes);

                    IList<CommonNode> roleCommonNodes = userAccountContract.GetRoles(userAccountInfo.UserId);
                    btxtRole.Tag = roleCommonNodes;
                    btxtRole.Text = CommonObjHelper.GetCommonNodeNamesWithComma(roleCommonNodes);

                    byte[] data = userAccountContract.DownLoadPhoto(userAccountInfo.UserName);
                    if (data != null)
                    {
                        peUser.EditValue = data;
                        //using (MemoryStream ms = new MemoryStream(data))
                        //{
                        //    Image img = Image.FromStream(ms);
                        //    peUser.Image = img;                            
                        //}
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
                        ClearUserPhoto();
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

        #endregion

        
    }
}
