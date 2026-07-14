using System;
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
using AppFramework.Reference.CustomLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.MyAuditingModule
{
    public partial class GroupAdutingControl : UserControl
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 契约接口
        /// </summary>
        public GroupAdutingControl()
        {
            InitializeComponent();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            groupCondition.CustomGroupContract = customGroupContract;
            groupCondition.UserTypeContract = userTypeContract;
            groupCondition.CustomDepartmentContract = customDepartmentContract;            
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupAdutingControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                TableAuditingControl tableAuditingControl = new TableAuditingControl()
                {
                    DataAuditingType = DataAuditingType.Group,
                    Dock = DockStyle.Fill,
                    BorderStyle = BorderStyle.None
                };
                pnlControls.Controls.Add(tableAuditingControl);
                groupCondition.SetWhereConditonHandler += (whereConditons) =>
                {
                    tableAuditingControl.LoadGroupData(whereConditons);
                };
            }
        }
    }
}
