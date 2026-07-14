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
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.UserModule;
using Blue.WindowsFormsClient;
using DevExpress.XtraEditors.Controls;
using Blue.WindowsFormsClient.SystemManagementModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class UserNameForm : Form
    {
        #region 契约接口

        private IUserAccountContract userAccountContract = null;

        #endregion

        #region 属性

        /// <summary>
        /// 获得用户信息
        /// </summary>
        public GetCommonUserInfoDelegate GetCommonUserInfo
        {
            get;
            set;
        }

        #endregion


        #region 构造函数

        public UserNameForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserNameForm_Load(object sender, EventArgs e)
        {
            if (CurrentUser.Instance.UserAdded)
            {
                string textAdded = UserEnumHelper.GetEnumText(MenuAuthority.UserAdded);
                ImageComboBoxItem itmAdded = new ImageComboBoxItem(textAdded, MenuAuthority.UserAdded, 0);
                icmbUserManagement.Properties.Items.Add(itmAdded);
            }
            if (CurrentUser.Instance.UserEdited)
            {
                string textEdited = UserEnumHelper.GetEnumText(MenuAuthority.UserEdited);
                ImageComboBoxItem itmEdited = new ImageComboBoxItem(textEdited, MenuAuthority.UserEdited, 1);
                icmbUserManagement.Properties.Items.Add(itmEdited);
            }
            icmbUserManagement.SelectedIndex = 0;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            MenuAuthority menuAuthority = (MenuAuthority)icmbUserManagement.EditValue;
            switch(menuAuthority)
            {
                case MenuAuthority.UserAdded:
                    if (CurrentUser.Instance.UserAdded)
                    {
                        UserDataForm userDataForm = new UserDataForm();
                        userDataForm.ShowDialog();
                    }
                    break;

                case MenuAuthority.UserEdited:
                    if (CurrentUser.Instance.UserEdited)
                    {
                        UserListForm frmUserList = new UserListForm();
                        frmUserList.GetIdentifier = (userId) =>
                        {
                            if (userId > 0)
                            {
                                SingleUserForm frmSingleUser = new SingleUserForm();
                                frmSingleUser.UserId = userId;
                                frmSingleUser.ShowDialog();
                            }
                            else
                            {
                                MessageBox.Show("用户信息不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        };
                        frmUserList.ShowDialog();
                    }
                    break;
            }  
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 用户选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkUser_Click(object sender, EventArgs e)
        {
           
        }

        #endregion
    }
}
