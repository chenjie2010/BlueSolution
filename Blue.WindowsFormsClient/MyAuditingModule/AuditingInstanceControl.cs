using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Columns;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using Blue.Model.DataFilledModule;
using Blue.WindowsFormsClient.Common;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.MyAuditingModule
{
    public partial class AuditingInstanceControl : UserControl
    {
        #region 私有变量


        #endregion

        #region 私有成员变量

        private ExtendedCustomBusinessInfo extendedCustomBusinessInfo = null;

        #endregion

        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly IUserAccountContract userAccountContract;

        #endregion

        #region 属性

        /// <summary>
        /// 返回主界面
        /// </summary>
        public GoBackDelegate GoBack
        {
            get;
            set;
        }

        /// <summary>
        /// 业务对象
        /// </summary>
        public ExtendedCustomBusinessInfo ExtendedCustomBusinessInfo
        {
            get
            {
                return extendedCustomBusinessInfo;
            }
            set
            {
                extendedCustomBusinessInfo = value;
                gcTop.Text = extendedCustomBusinessInfo.BusinessName;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public AuditingInstanceControl()
        {
            InitializeComponent();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            userListControl.CustomGroupContract = customGroupContract;
            userListControl.UserTypeContract = userTypeContract;
            userListControl.CustomDepartmentContract = customDepartmentContract;
            userListControl.UserAccountContract = userAccountContract;
            userListControl.IsPhotoShowed = true;
            userListControl.IsShowCheckBox = false;
        }


        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuditingInstanceControl_Load(object sender, EventArgs e)
        {

        }

        #endregion
    }
}
