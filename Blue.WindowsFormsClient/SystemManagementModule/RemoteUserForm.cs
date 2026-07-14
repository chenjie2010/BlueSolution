using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using FarPoint.Win.Spread;
using FarPoint.Excel;
using FarPoint.Win.Spread.CellType;
using DevExpress.XtraPrinting;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Core.ClientConfig;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using Blue.Model.SystemModule;
using Blue.Model.UserModule;
using Blue.WCFContracts;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class RemoteUserForm : Form
    {
        #region 契约接口

        private readonly IUserAccountContract userAccountContract = null;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomGroupContract customGroupContract;
        private IRemoteServerContract remoteServerContract;

        #endregion

        #region 私有变量

        private int testTimer = 0;
        private readonly NetworkConnection networkConnection = null;
        private ProgressForm frmProgress = null;

        private DataTable localUserTable = null;
         

        #endregion

        #region 属性

        /// <summary>
        /// 刷新窗体
        /// </summary>
        public RefreshFormDelegate RefreshForm
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RemoteUserForm()
        {
            InitializeComponent();
            networkConnection = new NetworkConnection();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbAuthority, typeof(SystemDataFieldPermission));
            IList<EnumItem> departmentProperties = SystemConfigHelper.GetDepartmentPorperty();
            ccmbDepartmentRange.Properties.Items.AddRange(departmentProperties.ToArray());
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteUserForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 取消测试链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkCancel_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            progressPanel.Hide();
            networkConnection.CancelTest = true;
        }

        /// <summary>
        /// 清除初始化的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            ccmbAuthority.EditValue = null;
            btxtUserTypeRange.EditValue = string.Empty;
            btxtUserTypeRange.Tag = null;
            btxtDepartmentRange.EditValue = string.Empty;
            btxtDepartmentRange.Tag = null;
            ccmbDepartmentRange.EditValue = null;
            btxtRole.EditValue = string.Empty;
            btxtRole.Tag = null;
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnCondition_Click(object sender, EventArgs e)
        {
            grpControl.Text = "按条件获取的用户列表";
            try
            {
                devExpressGrid.DataSource = null;
                Cursor = Cursors.WaitCursor;
                string remoteUserName = txtRemoteUserName.Text.Trim();
                string remoteUserPwd = txtRemotePassword.Text.Trim();
                if (remoteServerContract == null || !remoteServerContract.ValidateUser(remoteUserName, remoteUserPwd))
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("远程交换未设置或者其用户名与密码错误。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                IList<WhereConditon> whereConditons = GetWhereConditons();
                int startPosition = 0, pageSize = 4000;
                int totalCount = remoteServerContract.GetUserCount(remoteUserName, remoteUserPwd, whereConditons);
                int pageCount = totalCount / pageSize;
                if (totalCount % pageSize != 0)
                {
                    pageCount++;
                }
                DataSet set = new DataSet();
                frmProgress = new ProgressForm();
                frmProgress.MinValue = 0;
                frmProgress.TopMost = true;
                frmProgress.MaxValue = pageCount + 1;
                frmProgress.Text = "正在进行用户对比操作.....";
                frmProgress.Show();
                bool exit = false;
                Cursor = Cursors.WaitCursor;
                for(int index = 0; index < pageCount; index++)
                {
                    GetUserNameTableOnLocalSystem();
                    frmProgress.IncreaseStep();
                    startPosition = index * pageSize;
                    DataSet ds = remoteServerContract.GetUserData(remoteUserName, remoteUserPwd, startPosition, pageSize, whereConditons, ref totalCount);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0].Clone();
                        dt.TableName = string.Format("tb_{0}", index);
                        object[] obj = new object[ds.Tables[0].Columns.Count];
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            DataRow[] drs = localUserTable.Select(string.Format("UserName = '{0}'", dataRow["UserName"]));
                            if (drs == null || drs.Length == 0)
                            {
                                dataRow.ItemArray.CopyTo(obj, 0);
                                dt.Rows.Add(obj);
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            set.Tables.Add(dt);
                        }
                        if (frmProgress != null && frmProgress.Cancel)
                        {
                            exit = true;
                            break; ;
                        }
                    }
                    frmProgress.IncreaseStep();
                    if (exit)
                    {
                        break;
                    }
                }
                if (exit)
                {
                    Cursor = Cursors.Default;
                    frmProgress.CloseFrom();
                    frmProgress = null;
                    return;
                }
                if (set.Tables.Count > 0)
                {
                    DataTable newData = GetAllDataTable(set);
                    devExpressGrid.PageSize = newData.Rows.Count;
                    devExpressGrid.DataSource = newData;
                    devExpressGrid.RecordCount = newData.Rows.Count;
                    for (int i = 6; i < devExpressGrid.Columns.Count; i++)
                    {
                        devExpressGrid.Columns[i].Visible = false;
                    }
                    Cursor = Cursors.Default;
                    if (newData.Rows.Count > 0)
                    {
                        if (frmProgress != null)
                        {
                            frmProgress.CloseFrom();
                            frmProgress = null;
                        }
                        Cursor = Cursors.Default;
                        MessageBox.Show("获取成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (frmProgress != null)
                        {
                            frmProgress.CloseFrom();
                            frmProgress = null;
                        }
                        Cursor = Cursors.Default;
                        MessageBox.Show("没有新的用户需要同步。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (frmProgress != null)
                    {
                        frmProgress.CloseFrom();
                        frmProgress = null;
                    }
                    Cursor = Cursors.Default;
                    MessageBox.Show("没有新的用户需要同步。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                if (frmProgress != null)
                {
                    frmProgress.CloseFrom();
                    frmProgress = null;
                }
                Cursor = Cursors.Default;
                MessageBox.Show("无法连接到远程服务器，获取失败。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }            
        }

        /// <summary>
        /// 批量导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiImportUsers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (devExpressGrid.MultiSelectedValues.Count == 0)
            {
                Cursor = Cursors.Default;
                MessageBox.Show("请先选择需要批量导入的用户！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Export(false);
        }

        /// <summary>
        /// 全部导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiImportAllUsers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Export(true);
        }

        /// <summary>
        /// 对比获取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiGetUsers_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                string remoteUserName = txtRemoteUserName.Text.Trim();
                string remoteUserPwd = txtRemotePassword.Text.Trim();
                if (remoteServerContract == null || !remoteServerContract.ValidateUser(remoteUserName, remoteUserPwd))
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("远程交换未设置或者其用户名与密码错误。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                int startPosition = 0, pageSize = 4000;
                int totalCount = remoteServerContract.GetUserCount(remoteUserName, remoteUserPwd, whereConditons);
                int pageCount = totalCount / pageSize;
                if (totalCount % pageSize != 0)
                {
                    pageCount++;
                }
                DataSet set = new DataSet();
                frmProgress = new ProgressForm();
                frmProgress.MinValue = 0;
                frmProgress.TopMost = true;
                frmProgress.MaxValue = pageCount + 1;
                frmProgress.Text = "正在进行用户对比操作.....";
                frmProgress.Show();
                bool exit = false;
                Cursor = Cursors.WaitCursor;
                for (int index = 0; index < pageCount; index++)
                {
                    GetUserNameTableOnLocalSystem();
                    frmProgress.IncreaseStep();
                    startPosition = index * pageSize;
                    DataSet ds = remoteServerContract.GetUserData(remoteUserName, remoteUserPwd, startPosition, pageSize, whereConditons, ref totalCount);
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = ds.Tables[0].Clone();
                        dt.TableName = string.Format("tb_{0}", index);
                        object[] obj = new object[ds.Tables[0].Columns.Count];
                        foreach (DataRow dataRow in ds.Tables[0].Rows)
                        {
                            DataRow[] drs = localUserTable.Select(string.Format("UserName = '{0}'", dataRow["UserName"]));
                            if (drs == null || drs.Length == 0)
                            {
                                dataRow.ItemArray.CopyTo(obj, 0);
                                dt.Rows.Add(obj);
                            }
                        }
                        if (dt.Rows.Count > 0)
                        {
                            set.Tables.Add(dt);
                        }
                        if (frmProgress != null && frmProgress.Cancel)
                        {
                            exit = true;
                            break; ;
                        }                        
                    }                    
                    frmProgress.IncreaseStep();
                    if (exit)
                    {
                        break;
                    }
                }
                if (exit)
                {
                    Cursor = Cursors.Default;
                    frmProgress.CloseFrom();
                    frmProgress = null;
                    return;
                }                
                if (set.Tables.Count > 0)
                {
                    DataTable newData = GetAllDataTable(set);
                    devExpressGrid.PageSize = newData.Rows.Count;
                    devExpressGrid.DataSource = newData;
                    devExpressGrid.RecordCount = newData.Rows.Count;
                    for (int i = 5; i < devExpressGrid.Columns.Count; i++)
                    {
                        devExpressGrid.Columns[i].Visible = false;
                    }
                    frmProgress.CloseFrom();
                    frmProgress = null;
                    Cursor = Cursors.Default;
                    MessageBox.Show("获取成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    frmProgress.CloseFrom();
                    frmProgress = null;
                    Cursor = Cursors.Default;
                    MessageBox.Show("没有新的用户需要同步。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                if (frmProgress != null)
                {
                    frmProgress.CloseFrom();
                }
                Cursor = Cursors.Default;
                MessageBox.Show("无法连接到远程服务器，获取失败。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 测试连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTest_Click(object sender, EventArgs e)
        {
            if (localUserTable != null)
            {
                localUserTable.Clear();
                localUserTable.Dispose();
            }
            string remoteAddress = txtRemoteAddress.Text.Trim();
            string remoteUserName = txtRemoteUserName.Text.Trim();
            string remotePassword = txtRemotePassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(remoteAddress))
            {
                txtRemoteAddress.Focus();
                MessageBox.Show("远程地址不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remoteUserName))
            {
                txtRemoteUserName.Focus();
                MessageBox.Show("用户名不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remotePassword))
            {
                txtRemotePassword.Focus();
                MessageBox.Show("密码不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                lblTimerValue.Visible = true;
                hlnkCancel.Visible = true;
                networkConnection.CancelTest = false;
                testTimer = 0;
                progressPanel.Show();
                networkConnection.TimerHandler = new TimerHandler(TestTimerHandler);
                bool result = networkConnection.TestRemoteConnection(remoteAddress, remoteUserName, remotePassword);
                progressPanel.Hide();
                lblTimerValue.Visible = false;
                hlnkCancel.Visible = false;
                Cursor = Cursors.Default;
                if (result)
                {
                    remoteServerContract = RemoteChannelFactory.CreateRemoteServerContract(remoteAddress, CurrentConfig.Instance.Port);
                    IList<CommonNode> userTypesNodes = remoteServerContract.GetUserTypes(remoteUserName, remotePassword);
                    UserControlHelper.InitCheckedComboBoxEditItems(ccmbRemoteUserTypeRange, userTypesNodes);
                    MessageBox.Show("与远程服务器连接测试成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    remoteServerContract = null;
                    MessageBox.Show("与服务器连接测试失败。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                remoteServerContract = null;
                progressPanel.Hide();
                lblTimerValue.Visible = false;
                hlnkCancel.Visible = false;
                Cursor = Cursors.Default;
                MessageBox.Show("无法连接到远程服务器，获取失败！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbRemoteUserTypeRange);
            deCreatedTime1.EditValue = null;
            deCreatedTime2.EditValue = null;
            deModifiedTime1.EditValue = null;
            deModifiedTime2.EditValue = null;
        }

        /// <summary>
        /// 角色选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtRole_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            IList<CommonNode> nodes = btxtUserTypeRange.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new RoleMultiSelectedItems(customGroupContract, customRoleContract, (byte)GroupType.Role, true);
            frmMultiSelectedItems.Text = "角色选择";
            frmMultiSelectedItems.GetTreeNodeListDelegate = (commonNodes) =>
            {
                btxtRole.Tag = commonNodes;
                btxtRole.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
            };
            frmMultiSelectedItems.OperationTip = "提示：请为该用户选择角色。";
            frmMultiSelectedItems.SetTokenEidtValues(nodes);
            frmMultiSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 用户类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtUserTypeRange_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            IList<CommonNode> nodes = btxtUserTypeRange.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new UserTypeMultiSelectedItems(customGroupContract, userTypeContract, (byte)GroupType.UserType, true);
            frmMultiSelectedItems.Text = "用户类型选择";
            frmMultiSelectedItems.GetTreeNodeListDelegate = (commonNodes) =>
            {
                btxtUserTypeRange.Tag = commonNodes;
                btxtUserTypeRange.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
            };
            frmMultiSelectedItems.OperationTip = "提示：如果该用户管理所有用户类型，则不需要选择任何用户类型。当用户类型为空时，自动管理所有用户类型。";
            frmMultiSelectedItems.SetTokenEidtValues(nodes);
            frmMultiSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 单位选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtDepartmentRange_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            IList<CommonNode> nodes = btxtDepartmentRange.Tag as IList<CommonNode>;
            MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
            frmMultiSelectedItems.MultiSelectedItemsHandler = new MultiSelectedItems(customDepartmentContract, false);
            frmMultiSelectedItems.Text = "单位选择";
            frmMultiSelectedItems.OnlyLeafSelected = false;
            frmMultiSelectedItems.GetTreeNodeListDelegate = (commonNodes) =>
            {
                btxtDepartmentRange.Tag = commonNodes;
                btxtDepartmentRange.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
            };
            frmMultiSelectedItems.OperationTip = "提示：如果该用户管理所有单位，则不需要选择任何单位。当单位为空时，自动管理所有单位。";
            frmMultiSelectedItems.SetTokenEidtValues(nodes);
            frmMultiSelectedItems.ShowDialog();
        }

        /// <summary>
        /// 用户导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = string.Format("远程用户导出_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
            dlg.Filter = AppSettingHelper.DefaultExcelFormat;
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                Cursor = Cursors.Default;
                return;
            }
            Cursor = Cursors.WaitCursor;
            try
            {
                XlsxExportOptions options = new XlsxExportOptions(TextExportMode.Value, true, false, false);
                devExpressGrid.DevExpressGridView.ExportToXlsx(dlg.FileName, options);
                Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 获得本地系统的用户名列表
        /// </summary>
        private void GetUserNameTableOnLocalSystem()
        {
            if (localUserTable == null)
            {
                int startPosition = 0, pageSize = 2000;
                int totalCount = userAccountContract.GetUserCount(null);
                int pageCount = totalCount / pageSize;
                if (totalCount % pageSize != 0)
                {
                    pageCount++;
                }
                for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                {
                    startPosition = pageIndex * pageSize;
                    DataSet ds = userAccountContract.GetUserNames(startPosition, pageSize, null);
                    if (pageIndex == 0)
                    {
                        localUserTable = ds.Tables[0];
                    }
                    else
                    {
                        localUserTable.Merge(ds.Tables[0]);
                    }
                }
            }
        }

        /// <summary>
        /// 获得查询条件
        /// </summary>
        /// <returns></returns>
        public IList<WhereConditon> GetWhereConditons()
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            string userName = txtUserName.Text;
            if (!string.IsNullOrWhiteSpace(userName))
            {
                userName = Regex.Replace(userName, " {1,}", "%");
                whereConditons.Add(new WhereConditon("UserAccount", "UserName", "UserName", DbType.String, userName,
               DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            UserControlHelper.GetWhereConditonsByCommonNodes(ccmbRemoteUserTypeRange, whereConditons, "UserAccount", "UserTypeId");            
            DateTime dt1 = deCreatedTime1.DateTime;
            if (!DataConvertionHelper.IsNullValue(dt1))
            {
                whereConditons.Add(new WhereConditon("UserAccount", "CreatedTime", "CreationTime1", DbType.DateTime, dt1,
                    DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            DateTime dt2 = deCreatedTime2.DateTime;
            if (!DataConvertionHelper.IsNullValue(dt2))
            {
                whereConditons.Add(new WhereConditon("UserAccount", "CreatedTime", "CreationTime2", DbType.DateTime, dt2,
                DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            DateTime modifiedTime1 = deModifiedTime1.DateTime;
            if (!DataConvertionHelper.IsNullValue(modifiedTime1))
            {
                whereConditons.Add(new WhereConditon("UserAccount", "UpdatedTime", "ModificationTime1", DbType.DateTime, modifiedTime1,
                    DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            DateTime modifiedTime2 = deModifiedTime2.DateTime;
            if (!DataConvertionHelper.IsNullValue(modifiedTime2))
            {
                whereConditons.Add(new WhereConditon("UserAccount", "UpdatedTime", "ModificationTime2", DbType.DateTime, modifiedTime2,
                DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            return whereConditons;
        }

        /// <summary>
        /// 网络测试过程中更新耗时数据
        /// </summary>
        /// <param name="millisecond"></param>
        private void TestTimerHandler(int millisecond)
        {
            testTimer += millisecond;
            double timer = (double)testTimer / 1000;
            lblTimerValue.Text = string.Format("时间：{0:F2} 秒", timer);
        }

        /// <summary>
        /// 获取用户数据
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public DataTable GetAllDataTable(DataSet ds)
        {
            DataTable newDataTable = ds.Tables[0].Clone();                //创建新表 克隆以有表的架构。
            object[] objArray = new object[newDataTable.Columns.Count];   //定义与表列数相同的对象数组 存放表的一行的值。
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    ds.Tables[i].Rows[j].ItemArray.CopyTo(objArray, 0);    //将表的一行的值存放数组中。
                    newDataTable.Rows.Add(objArray);                       //将数组的值添加到新表中。
                }
            }
            return newDataTable;                                           //返回新表。
        }

        /// <summary>
        /// 导入用户
        /// </summary>
        /// <param name="all"></param>
        private void Export(bool all)
        {
            string remoteAddress = txtRemoteAddress.Text.Trim();
            string remoteUserName = txtRemoteUserName.Text.Trim();
            string remoteUserPwd = txtRemotePassword.Text.Trim();
            if (string.IsNullOrWhiteSpace(remoteAddress))
            {
                txtRemoteAddress.Focus();
                MessageBox.Show("远程地址不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remoteUserName))
            {
                txtRemoteUserName.Focus();
                MessageBox.Show("用户名不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(remoteUserPwd))
            {
                txtRemotePassword.Focus();
                MessageBox.Show("密码不能为空。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (remoteServerContract == null || !remoteServerContract.ValidateUser(remoteUserName, remoteUserPwd))
            {
                Cursor = Cursors.Default;
                MessageBox.Show("远程交换未设置或者其用户名与密码错误。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable data = (DataTable)devExpressGrid.DataSource;
            IDictionary<decimal, string> userTypeIdAndNames = userTypeContract.GetUserTypeIdAndNames();
            IDictionary<string, decimal> newUserTypeIdAndNames = new Dictionary<string, decimal>(userTypeIdAndNames.Count);
            foreach (KeyValuePair<decimal, string> keyValue in userTypeIdAndNames)
            {
                if (!newUserTypeIdAndNames.ContainsKey(keyValue.Value))
                {
                    newUserTypeIdAndNames.Add(keyValue.Value, keyValue.Key);
                }
            }

            IDictionary<decimal, string> depIdAndNames = customDepartmentContract.GetDepIdAndNames();
            IDictionary<string, decimal> newDepIdAndNames = new Dictionary<string, decimal>(userTypeIdAndNames.Count);
            foreach (KeyValuePair<decimal, string> keyValue in depIdAndNames)
            {
                if (!newDepIdAndNames.ContainsKey(keyValue.Value))
                {
                    newDepIdAndNames.Add(keyValue.Value, keyValue.Key);
                }
            }
            Int64 dataFieldAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbAuthority);
            IList<CommonNode> userTypeCommonNodes = btxtUserTypeRange.Tag as IList<CommonNode>;
            IList<decimal> userTypeIds = CommonObjHelper.GetCommonNodeIds(userTypeCommonNodes);

            IList<CommonNode> depCommonNodes = btxtDepartmentRange.Tag as IList<CommonNode>;
            IList<decimal> departmentIds = CommonObjHelper.GetCommonNodeIds(depCommonNodes);
            Int64 deparmentAuthority = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDepartmentRange);

            IList<CommonNode> roleCommonNodes = btxtRole.Tag as IList<CommonNode>;
            IList<decimal> roleIds = CommonObjHelper.GetCommonNodeIds(roleCommonNodes);
            if (all)
            {
                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string userName = DataConvertionHelper.GetString(data.Rows[i]["UserName"]);
                    string depName = DataConvertionHelper.GetString(data.Rows[i]["DepName"]);
                    string userTypeName = DataConvertionHelper.GetString(data.Rows[i]["UserTypeName"]);
                    if (userAccountContract.IsExistUserName(userName))
                    {
                        MessageBox.Show(string.Format("第{0}行的用户名称在本系统已经存在，请检查。", i + 1, userName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    decimal userTypeId = 0, depId = 0;
                    if (newDepIdAndNames.ContainsKey(depName))
                    {
                        depId = newDepIdAndNames[depName];
                    }
                    if (newUserTypeIdAndNames.ContainsKey(userTypeName))
                    {
                        userTypeId = newUserTypeIdAndNames[userTypeName];
                    }
                    if (depId < 1)
                    {
                        MessageBox.Show(string.Format("第{0}行的单位名称在本系统内不存在，请检查！", i + 1), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (userTypeId < 1)
                    {
                        MessageBox.Show(string.Format("第{0}行的用户类型名称在本系统内不存在，请检查！", i + 1), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                Cursor = Cursors.WaitCursor;
                frmProgress = new ProgressForm();
                frmProgress.MinValue = 0;
                frmProgress.TopMost = true;
                frmProgress.MaxValue = data.Rows.Count;
                frmProgress.Text = "正在进行用户导入操作.....";
                frmProgress.Show();

                for (int i = 0; i < data.Rows.Count; i++)
                {
                    string depName = DataConvertionHelper.GetString(data.Rows[i]["DepName"]);
                    string userTypeName = DataConvertionHelper.GetString(data.Rows[i]["UserTypeName"]);
                    decimal userTypeId = 0, depId = 0;
                    if (newDepIdAndNames.ContainsKey(depName))
                    {
                        depId = newDepIdAndNames[depName];
                    }
                    if (newUserTypeIdAndNames.ContainsKey(userTypeName))
                    {
                        userTypeId = newUserTypeIdAndNames[userTypeName];
                    }
                    Insert(data.Rows[i], userTypeId, depId, dataFieldAuthority, userTypeIds, departmentIds, roleIds, deparmentAuthority, remoteUserName, remoteUserPwd);
                    frmProgress.IncreaseStep();
                    if (frmProgress != null && frmProgress.Cancel)
                    {
                        break; ;
                    }
                }
                frmProgress.CloseFrom();
                frmProgress = null;
                Cursor = Cursors.Default;

            }
            else
            {
                foreach (int i in devExpressGrid.SelectedRows)
                {
                    string userName = DataConvertionHelper.GetString(data.Rows[i]["UserName"]);
                    string depName = DataConvertionHelper.GetString(data.Rows[i]["DepName"]);
                    string userTypeName = DataConvertionHelper.GetString(data.Rows[i]["UserTypeName"]);
                    if (userAccountContract.IsExistUserName(userName))
                    {
                        MessageBox.Show(string.Format("第{0}行的用户名称在本系统已经存在，请检查。", i + 1, userName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    decimal userTypeId = 0, depId = 0;
                    if (newDepIdAndNames.ContainsKey(depName))
                    {
                        depId = newDepIdAndNames[depName];
                    }
                    if (newUserTypeIdAndNames.ContainsKey(userTypeName))
                    {
                        userTypeId = newUserTypeIdAndNames[userTypeName];
                    }
                    if (depId < 1)
                    {
                        MessageBox.Show(string.Format("第{0}行的单位名称在本系统内不存在，请检查。", i + 1), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (userTypeId < 1)
                    {
                        MessageBox.Show(string.Format("第{0}行的用户类型名称在本系统内不存在，请检查。", i + 1), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                Cursor = Cursors.WaitCursor;
                try
                {
                    frmProgress = new ProgressForm();
                    frmProgress.MinValue = 0;
                    frmProgress.TopMost = true;
                    frmProgress.MaxValue = devExpressGrid.SelectedRows.Count;
                    frmProgress.Text = "正在进行用户导入操作.....";
                    frmProgress.Show();
                    foreach (int i in devExpressGrid.SelectedRows)
                    {
                        string depName = DataConvertionHelper.GetString(data.Rows[i]["DepName"]);
                        string userTypeName = DataConvertionHelper.GetString(data.Rows[i]["UserTypeName"]);
                        decimal userTypeId = 0, depId = 0;
                        if (newDepIdAndNames.ContainsKey(depName))
                        {
                            depId = newDepIdAndNames[depName];
                        }
                        if (newUserTypeIdAndNames.ContainsKey(userTypeName))
                        {
                            userTypeId = newUserTypeIdAndNames[userTypeName];
                        }
                        Insert(data.Rows[i], userTypeId, depId, dataFieldAuthority, userTypeIds, departmentIds, roleIds,  deparmentAuthority, remoteUserName, remoteUserPwd);
                        frmProgress.IncreaseStep();
                        if (frmProgress != null && frmProgress.Cancel)
                        {
                            break;
                        }
                    }
                }
                finally
                {
                    frmProgress.CloseFrom();
                    frmProgress = null;
                    Cursor = Cursors.Default;
                }
            }
            localUserTable.Dispose();
            localUserTable = null;
            MessageBox.Show("导入操作完成。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 导入用户
        /// </summary>
        /// <param name="dr"></param>
        /// <param name="userTypeId"></param>
        /// <param name="depId"></param>
        /// <param name="dataFieldAuthority"></param>
        /// <param name="userTypeIds"></param>
        /// <param name="departmentIds"></param>
        /// <param name="roleIds"></param>
        /// <param name="deparmentAuthority"></param>
        /// <param name="remoteUserName"></param>
        /// <param name="remoteUserPwd"></param>
        private void Insert(DataRow dr, decimal userTypeId, decimal depId, Int64 dataFieldAuthority, 
            IList<decimal> userTypeIds, IList<decimal> departmentIds, IList<decimal> roleIds, Int64 deparmentAuthority, string remoteUserName, string remoteUserPwd)
        {
            string userName = DataConvertionHelper.GetString(dr["UserName"]);
            string userActualName = DataConvertionHelper.GetString(dr["UserActualName"]);
            string userPwd = CryptographyHelper.Decrypt(DataConvertionHelper.GetString(dr["UserPwd"]));
            string emailAddress = DataConvertionHelper.GetString(dr["EmailAddress"]);
            byte identificationType = DataConvertionHelper.GetByte(dr["IdentificationType"]);
            string userIdentity = DataConvertionHelper.GetString(dr["UserIdentity"]);
            string telephoneNumber = DataConvertionHelper.GetString(dr["TelephoneNumber"]);
            string photoSuffix = DataConvertionHelper.GetString(dr["PhotoSuffixName"]);
            DateTime createdTime = DataConvertionHelper.GetDateTime(dr["CreatedTime"]);
            DateTime updatedTime = DataConvertionHelper.GetDateTime(dr["UpdatedTime"]);
            string notes = DataConvertionHelper.GetString(dr["Notes"]);
            Guid guid = (Guid)dr["UniqueUserIdentity"];
            bool isLockedOut = DataConvertionHelper.GetBoolean(dr["LockedOut"]);
            byte[] imageData = remoteServerContract.DownLoadPhoto(remoteUserName, remoteUserPwd, userName);
            UserAccountInfo userAccountInfo = new UserAccountInfo(0, depId, userTypeId, userName, userPwd, userActualName, emailAddress, identificationType, userIdentity,
                telephoneNumber, DateTime.Now, "127.0.0.1", photoSuffix, isLockedOut, dataFieldAuthority, deparmentAuthority, guid, notes, 0, createdTime, updatedTime);
            userAccountContract.Insert(userAccountInfo, imageData, userTypeIds, departmentIds, roleIds);
        }

        #endregion
       
    }
}
