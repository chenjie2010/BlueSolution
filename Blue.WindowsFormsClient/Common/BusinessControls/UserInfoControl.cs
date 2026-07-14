using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.Model.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.Common.BusinessControls
{
    public partial class UserInfoControl : UserControl
    {
        #region 私有变量


        #endregion

        #region 属性

        /// <summary>
        /// 用户契约
        /// </summary>
        public IUserAccountContract UserAccountContract
        {
            get;
            set;
        }

        /// <summary>
        /// 用户类型契约
        /// </summary>
        public IUserTypeContract UserTypeContract
        {
            get;
            set;
        }

        /// <summary>
        /// 单位契约
        /// </summary>
        public ICustomDepartmentContract CustomDepartmentContract
        {
            get;
            set;
        }

        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        /// <summary>
        /// 角色契约
        /// </summary>
        public ICustomRoleContract CustomRoleContract
        {
            get;
            set;
        }

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
        public UserInfoControl()
        {
            InitializeComponent();
            cmbUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(CustomGroupContract, UserTypeContract);
            cmbUserType.InitalizeTreeView();
            cmbDepartment.TreeDropdownHandler = new TreeDropdownItems(CustomDepartmentContract);
            cmbDepartment.InitalizeTreeView();
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserInfoControl_Load(object sender, EventArgs e)
        {

        }

        #endregion

        #region 接口方法

        public void GetUserInfo()
        {
            string newUserName = txtNewUserName.Text.Trim();
            txtNewUserActualName.Text.Trim();
            decimal depId = decimal.MinValue;
            decimal userTypeId = decimal.MinValue;
            if(cmbDepartment.Value != null)
            {
                CommonNode departmentCommonNode = cmbDepartment.Value as CommonNode;
                depId = departmentCommonNode.NodeId;
            }
            if (cmbDepartment.Value != null)
            {
                CommonNode userTypeCommonNode = cmbUserType.Value as CommonNode;
                userTypeId = userTypeCommonNode.NodeId;
            }
            IList<decimal> roleIds = new List<decimal>();
            if (btxtNewRole.Tag != null)
            {
                IList<CommonNode> roleCommonNodes = btxtNewRole.Tag as IList<CommonNode>;
                roleIds = CommonObjHelper.GetCommonNodeIds(roleCommonNodes);
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
                CommonUserInfo commonUserInfo = UserAccountContract.GetCommonUserInfo(userId);
                if (commonUserInfo != null)
                {
                    txtUserName.Text = commonUserInfo.UserName;
                    txtUserActualName.Text = commonUserInfo.UserActualName;
                    txtDepartment.Text = commonUserInfo.DepName;
                    txtUserType.Text = commonUserInfo.UserTypeName;
                    txtRole.Text = string.Empty;
                    IList<CommonNode> roleCommonNodes = UserAccountContract.GetRoles(commonUserInfo.UserId);
                    txtRole.Text = CommonObjHelper.GetCommonNodeNamesWithComma(roleCommonNodes);
                    byte[] data = UserAccountContract.DownLoadPhoto(commonUserInfo.UserName);
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
        /// 角色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtNewRole_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            IList<CommonNode> commonNodes = btxtNewRole.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new RoleMultiSelectedItems(CustomGroupContract, CustomRoleContract, (byte)GroupType.Role, true);
            frmMultiSelectedItems.Text = "角色选择";
            frmMultiSelectedItems.GetTreeNodeListDelegate = GetRoleList;
            frmMultiSelectedItems.OperationTip = "提示：请为该用户选择角色。";
            frmMultiSelectedItems.SetTokenEidtValues(commonNodes);
            frmMultiSelectedItems.ShowDialog();
        }

        #endregion

    }
}
