using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using AppFramework.WinFormsControls;
using AppFramework.Reference.CustomLibrary;
using Blue.CustomLibrary;
using Blue.Model.SystemModule;
using Blue.Model.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class AuthorityForm : Form
    {
        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomGroupContract customGroupContract;

        #endregion

        #region 私有变量

        private IList<WhereConditon> whereConditons = new List<WhereConditon>();
        IList<decimal> roleIds = new List<decimal>();

        #endregion

        #region 构造函数

        public AuthorityForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
        }

        #endregion

        #region 窗体及控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthorityForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            UserControlHelper.InitImageComboBoxEdit(icmbAuthorityMethod, typeof(AuthorityMethod));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbAuthority, typeof(SystemDataFieldPermission));

            IList<CommonNode> commonNodes = customRoleContract.GetCommonNodes(false);
            chklstRole.Items.AddRange(commonNodes.ToArray());
            ccmbRole.Properties.Items.AddRange(commonNodes.ToArray());

            cmbUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(customGroupContract, userTypeContract);
            cmbUserType.InitalizeTreeView();

            cmbDepartment.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
            cmbDepartment.InitalizeTreeView();            
        }

        /// <summary>
        /// 查询操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            whereConditons.Clear();
            roleIds.Clear();
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
            /* 用户类型 */
            CommonNode userTypeCommonNode = cmbUserType.Value as CommonNode;
            if (userTypeCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("UserAccount", "UserTypeId", "UserTypeId", System.Data.DbType.Decimal, userTypeCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            /* 单位类型 */
            CommonNode departmentCommonNode = cmbDepartment.Value as CommonNode;
            if (departmentCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("UserAccount", "DepId", "DepId", System.Data.DbType.Decimal, departmentCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem checkedListBoxItem in ccmbRole.Properties.Items)
            {
                if (checkedListBoxItem.CheckState == CheckState.Checked)
                {
                    CommonNode commonNode = (CommonNode)checkedListBoxItem.Value;
                    roleIds.Add(commonNode.NodeId);
                }
            }
            IList<WhereConditon> conditons = DataAccessHandler.GetWhereConditons(roleIds, "RoleAndUser", "RoleId");
            foreach (var conditon in conditons)
            {
                whereConditons.Add(conditon);
            }
            LoadData();
        }
        
        /// <summary>
        /// 用户类型列表
        /// </summary>
        /// <returns></returns>
        private IList<decimal> GetUserTypeIds()
        {
            IList<decimal> userTypeIds = new List<decimal>();
            if (btnEditUserType.Tag != null)
            {
                IList<CommonNode> commonNodes = btnEditUserType.Tag as IList<CommonNode>;
                if (commonNodes != null && commonNodes.Count > 0)
                {
                    foreach (CommonNode commonNode in commonNodes)
                    {
                        userTypeIds.Add(commonNode.NodeId);
                    }
                }
            }

            return userTypeIds;
        }

        /// <summary>
        /// 单位列表
        /// </summary>
        private IList<decimal> GetDepartmentIds()
        {
            IList<decimal> departmentIds = new List<decimal>();
            if (btnEditDepartment.Tag != null)
            {
                IList<CommonNode> commonNodes = btnEditDepartment.Tag as IList<CommonNode>;
                if (commonNodes != null && commonNodes.Count > 0)
                {
                    foreach (CommonNode commonNode in commonNodes)
                    {
                        departmentIds.Add(commonNode.NodeId);
                    }
                }
            }

            return departmentIds;
        }

        /// <summary>
        /// 授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmAuthority_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (devExpressGrid.FocusedRowHandle >= 0)
            {
                try
                {
                    if (MessageBox.Show("确认进行授权操作？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;
                        decimal userId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[0]);
                        string userName = DataConvertionHelper.GetString(devExpressGrid.GetRowCellValue(devExpressGrid.FocusedRowHandle, "UserName"));
                        IList<decimal> userIds = new List<decimal>();
                        userIds.Add(userId);
                        AuthorityMethod authorityMethod = (AuthorityMethod)Convert.ToByte(icmbAuthorityMethod.EditValue);
                        IList<decimal> authoritiedRoleIds = GetAuthoritiedRoles();
                        IList<decimal> departmentIds = GetDepartmentIds();
                        IList<decimal> userTypeIds = GetUserTypeIds();
                        long dataFieldPower = UserControlHelper.GetCheckedComboBoxEditItems(ccmbAuthority);
                        customRoleContract.Insert(userIds, authorityMethod, dataFieldPower, authoritiedRoleIds, chkOwnDepartment.Checked, departmentIds, userTypeIds);
                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("对用户{0}授权成功！", userName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception exception)
                {
                    Cursor = Cursors.Default;
                    //记录日志, 不抛出异常, 包装异常
                    WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                }
            }
            else
            {
                MessageBox.Show("请先选择需要授权的用户！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 批量授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmBatchAuthority_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("确认进行批量授权操作吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }
            if (devExpressGrid.MultiSelectedValues.Count > 0)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    IList<decimal> userIds = new List<decimal>(devExpressGrid.MultiSelectedValues.Count);
                    foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                    {
                        decimal userId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                        userIds.Add(userId);
                    }
                    AuthorityMethod authorityMethod = (AuthorityMethod)Convert.ToByte(icmbAuthorityMethod.EditValue);
                    IList<decimal> authoritiedRoleIds = GetAuthoritiedRoles();
                    IList<decimal> departmentIds = GetDepartmentIds();
                    IList<decimal> userTypeIds = GetUserTypeIds();
                    long dataFieldPower = UserControlHelper.GetCheckedComboBoxEditItems(ccmbAuthority);
                    progressPanel.Show();
                    customRoleContract.Insert(userIds, authorityMethod, dataFieldPower, authoritiedRoleIds, chkOwnDepartment.Checked, departmentIds, userTypeIds);
                    progressPanel.Hide();
                    Cursor = Cursors.Default;
                    MessageBox.Show(string.Format("共对{0}位用户批量授权成功！", userIds.Count), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception exception)
                {
                    progressPanel.Hide();
                    Cursor = Cursors.Default;
                    //记录日志, 不抛出异常, 包装异常
                    WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                }
            }
            else
            {
                MessageBox.Show("请先选择需要批量授权的用户！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 完全授权
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmAllAuthority_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("确认进行完全授权操作吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                AuthorityMethod authorityMethod = (AuthorityMethod)Convert.ToByte(icmbAuthorityMethod.EditValue);
                IList<decimal> authoritiedRoleIds = GetAuthoritiedRoles();
                IList<decimal> departmentIds = GetDepartmentIds();
                IList<decimal> userTypeIds = GetUserTypeIds();
                long dataFieldPower = UserControlHelper.GetCheckedComboBoxEditItems(ccmbAuthority);
                progressPanel.Show();
                customRoleContract.Insert(whereConditons, roleIds, authorityMethod, dataFieldPower, authoritiedRoleIds, chkOwnDepartment.Checked, departmentIds, userTypeIds);
                progressPanel.Hide();
                Cursor = Cursors.Default;
                MessageBox.Show("完全授权成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                progressPanel.Hide();
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 清除条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkbtnClean_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtCondition.Text = string.Empty;
            cmbDepartment.Value = null;
            cmbUserType.Value = null;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbRole);            
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 选择用户类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEditUserType_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            IList<CommonNode> commonNodes = btnEditUserType.Tag as IList<CommonNode>;
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
        private void btnEditDepartment_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            IList<CommonNode> commonNodes = btnEditDepartment.Tag as IList<CommonNode>;
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
        /// 回车键查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                LoadData();
                e.Handled = true;// 指示 KeyPress 事件已处理，去掉 Windows 缺省的叮当声。
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获得授权的角色
        /// </summary>
        /// <returns></returns>
        private IList<decimal> GetAuthoritiedRoles()
        {
            IList<decimal> authoritiedRoleIds = new List<decimal>();
            foreach (object obj in chklstRole.SelectedItems)
            {
                CommonNode commonNode = (CommonNode)((DevExpress.XtraEditors.Controls.CheckedListBoxItem)obj).Value;
                authoritiedRoleIds.Add(commonNode.NodeId); 
            }

            return authoritiedRoleIds; 
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                int totalCount = 0;
                devExpressGrid.DataSource = userAccountContract.GetPageRecordOfMultiTablesWithRole(devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex,
                   devExpressGrid.PageSize, whereConditons, ref totalCount).Tables[0];
                devExpressGrid.RecordCount = totalCount;
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
        /// 获得选择的单位
        /// </summary>
        /// <param name="commonNodes"></param>
        private void GetDepartmentNodeList(IList<CommonNode> commonNodes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CommonNode commonNode in commonNodes)
            {
                sb.Append(commonNode.NodeName);
                sb.Append(", ");
            }
            if (commonNodes.Count > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            btnEditDepartment.Tag = commonNodes;
            btnEditDepartment.Text = sb.ToString();
        }

        /// <summary>
        /// 获得选择的用户类型
        /// </summary>
        /// <param name="commonNodes"></param>
        private void GetUserTypeNodeList(IList<CommonNode> commonNodes)
        {
            StringBuilder sb = new StringBuilder();
            foreach (CommonNode commonNode in commonNodes)
            {
                sb.Append(commonNode.NodeName);
                sb.Append(", ");
            }
            if (commonNodes.Count > 0)
            {
                sb.Remove(sb.Length - 2, 2);
            }
            btnEditUserType.Tag = commonNodes;
            btnEditUserType.Text = sb.ToString();
        }

        /// <summary>
        /// 仅管理自己所属的单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkOwnDepartment_CheckedChanged(object sender, EventArgs e)
        {
            if (chkOwnDepartment.Checked)
            {
                btnEditDepartment.Properties.ReadOnly = true;
            }
            else
            {
                btnEditDepartment.Properties.ReadOnly = false;
            }
        }

        #endregion
    }
}
