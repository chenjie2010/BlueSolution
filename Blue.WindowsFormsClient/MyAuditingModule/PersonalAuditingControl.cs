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
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraNavBar;
using DevExpress.XtraBars;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.MyAuditingModule
{
    public partial class PersonalAuditingControl : UserControl
    {
        #region 私有变量

        private IList<decimal> selectedTables = null;

        #endregion

        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly IUserAccountContract userAccountContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public PersonalAuditingControl()
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
            selectedTables = new List<decimal>();
        }

        #endregion

        #region 控件的方法

        /// <summary>
        /// 控件方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PersonalAuditingControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                TableAuditingControl tableAuditingControl = new TableAuditingControl()
                {
                    DataAuditingType = DataAuditingType.Personal,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.None
                };
                pnlControls.Controls.Add(tableAuditingControl);
                userListControl.OnRowClick += (sdr, arg) =>
                {
                    tableAuditingControl.LoadPersonalData(userListControl.SelectedUserId);
                };
                userListControl.OnDataLoad += (sdr, arg) =>
                {
                    tableAuditingControl.LoadPersonalData(userListControl.SelectedUserId);
                };
            }
        }        

        #endregion

        #region 私有方法

        #endregion        
    }
}
