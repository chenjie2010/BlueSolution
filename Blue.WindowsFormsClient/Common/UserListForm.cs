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
using AppFramework.Reference.EnterpriseLibrary;
using Blue.Model.UserModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class UserListForm : Form
    {
        #region 契约接口

        /// <summary>
        /// 获得用户编号
        /// </summary>
        public GetIdentifierDelegate GetIdentifier
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserListForm()
        {
            InitializeComponent();
            userListControl.UserAccountContract = UserChannelFactory.CreateUserAccount();
            userListControl.UserTypeContract = SystemChannelFactory.CreateUserTypeContract();
            userListControl.CustomDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            userListControl.CustomGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserListForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            GetIdentifier?.Invoke(userListControl.SelectedUserId);
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
