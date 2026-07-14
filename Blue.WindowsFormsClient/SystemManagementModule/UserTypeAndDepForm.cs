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
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class UserTypeAndDepForm : Form
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomInterfaceContract customInterfaceContract;
        private readonly IUserAccountContract userAccountContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;

        #endregion

        #region 属性

        /// <summary>
        /// 接口编号
        /// </summary>
        public decimal InterfaceId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserTypeAndDepForm()
        {
            InitializeComponent();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customInterfaceContract = SystemChannelFactory.CreateCustomInterfaceContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
        }

        #endregion

        #region 控件方法

        /// <summary>
        ///  控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeAndDepForm_Load(object sender, EventArgs e)
        {
            IList<CommonNode> userTypeCommonNodes = customInterfaceContract.GetUserTypes(InterfaceId);
            btxtUserTypeRange.Tag = userTypeCommonNodes;
            btxtUserTypeRange.Text = CommonObjHelper.GetCommonNodeNamesWithComma(userTypeCommonNodes);

            IList<CommonNode> departmentCommonNodes = customInterfaceContract.GetDepartments(InterfaceId);
            btxtDepartmentRange.Tag = departmentCommonNodes;
            btxtDepartmentRange.Text = CommonObjHelper.GetCommonNodeNamesWithComma(departmentCommonNodes);

        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IList<CommonNode> userTypeCommonNodes = btxtUserTypeRange.Tag as IList<CommonNode>;
                IList<decimal> userTypeIds = CommonObjHelper.GetCommonNodeIds(userTypeCommonNodes);

                IList<CommonNode> depCommonNodes = btxtDepartmentRange.Tag as IList<CommonNode>;
                IList<decimal> departmentIds = CommonObjHelper.GetCommonNodeIds(depCommonNodes);
                customInterfaceContract.UpdateConditions(InterfaceId, userTypeIds, departmentIds);
                Cursor = Cursors.Default;
                this.Close();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
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
        /// 选择用户类型范围
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtUserTypeRange_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            IList<CommonNode> commonNodes = btxtUserTypeRange.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new UserTypeMultiSelectedItems(customGroupContract, userTypeContract, (byte)GroupType.UserType, true);
            frmMultiSelectedItems.Text = "用户类型选择";
            frmMultiSelectedItems.GetTreeNodeListDelegate = GetUserTypeNodeList;
            frmMultiSelectedItems.OperationTip = "提示：请选择用户类型。";
            frmMultiSelectedItems.SetTokenEidtValues(commonNodes);
            frmMultiSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 选择用户单位范围
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtDepartmentRange_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            IList<CommonNode> commonNodes = btxtDepartmentRange.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new MultiSelectedItems(customDepartmentContract, false);
            frmMultiSelectedItems.Text = "单位选择";
            frmMultiSelectedItems.OnlyLeafSelected = false;
            frmMultiSelectedItems.GetTreeNodeListDelegate = GetDepartmentNodeList;
            frmMultiSelectedItems.OperationTip = "提示：请选择单位。";
            frmMultiSelectedItems.SetTokenEidtValues(commonNodes);           
            frmMultiSelectedItems.ShowDialog();
        }

        #endregion

        #region 私有方法

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
        /// 获得选择的用户类型
        /// </summary>
        /// <param name="commonNodes"></param>
        private void GetUserTypeNodeList(IList<CommonNode> commonNodes)
        {
            btxtUserTypeRange.Tag = commonNodes;
            btxtUserTypeRange.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
        }

        #endregion
    }
}
