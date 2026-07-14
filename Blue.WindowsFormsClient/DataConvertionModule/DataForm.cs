using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core.ClientConfig;
using AppFramework.WinFormsLibrary.EventArgument;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.DataConvertionModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.BusinessModule;
using Blue.Model.DataConvertionModule;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class DataForm : Form
    {
        #region 私有变量

        private int testTimer = 0;
        private readonly NetworkConnection networkConnection = null;

        #endregion

        #region 属性

        /// <summary>
        /// 远程服务器对象
        /// </summary>
        public RemoteServer RemoteServerValue
        {
            get;
            set;
        }

        /// <summary>
        /// 确认操作
        /// </summary>
        public UpdateDataTableDelegate UpdateDataTable
        {
            get;
            set;
        }

        #endregion

        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ISystemConfigContract systemConfigContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomGroupContract customGroupContract;
        private readonly IDataBussinessContract dataBussinessContract;
        private readonly IDataRelationContract dataRelationContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomTableContract customTableContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly IDataAuditingContract dataAuditingContract;
        private IRemoteServerContract remoteServerContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataForm()
        {
            InitializeComponent();
            networkConnection = new NetworkConnection();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            dataBussinessContract = BusinessChannelFactory.CreateDataBussinessContract();
            dataRelationContract = DataConvertionChannelFactory.CreateDataRelationContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();            
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataForm_Load(object sender, EventArgs e)
        {
            ImageComboBoxItem item = new ImageComboBoxItem(string.Empty, -1, 0);
            icmbAudit.Properties.Items.Add(item);
            UserControlHelper.InitImageComboBoxEdit(icmbAudit, typeof(AuditedStatus));
            cmbQueriedUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(customGroupContract, userTypeContract);
            cmbQueriedUserType.InitalizeTreeView();

            cmbQueriedDepartment.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
            cmbQueriedDepartment.InitalizeTreeView();

            UserControlHelper.InitRepositoryItemImageComboBox(ricmbCombine, typeof(DataFieldInnerRealtion));
            ricmbCombine.EditValueChanged += new EventHandler(ricmbCombine_EditValueChanged);
         
            gcCondition.DataSource = CreateDataTable();
        }

        /// <summary>
        /// 刷新数据窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ricmbCombine_EditValueChanged(object sender, EventArgs e)
        {
            gridView.PostEditor();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                popupMenu.ShowPopup(Control.MousePosition);
            }
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dataTable = gcCondition.DataSource as DataTable;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["Selected"] = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmReverse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dataTable = gcCondition.DataSource as DataTable;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["Selected"] = !((bool)dataRow["Selected"]);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dataTable = gcCondition.DataSource as DataTable;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["Selected"] = false;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 创建数据表
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("DataFieldId", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("DataFieldName", Type.GetType("System.String"));
            dataTable.Columns.Add("Selected", Type.GetType("System.Boolean"));
            dataTable.Columns.Add("Condition", Type.GetType("System.String"));
            dataTable.Columns.Add("Combine", Type.GetType("System.Byte"));

            return dataTable;
        }

        /// <summary>
        /// 获取信息
        /// </summary>
        private void Test()
        {
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
                pbcWaittingBar.Visible = true;
                lblTimerValue.Visible = true;
                hlnkCancel.Visible = true;
                pbcWaittingBar.Properties.Stopped = false;
                networkConnection.CancelTest = false;
                testTimer = 0;
                networkConnection.TimerHandler = new TimerHandler(TestTimerHandler);
                bool result = networkConnection.TestRemoteConnection(remoteAddress, remoteUserName, remotePassword);
                pbcWaittingBar.Properties.Stopped = true;
                pbcWaittingBar.Visible = false;
                lblTimerValue.Visible = false;
                hlnkCancel.Visible = false;
                Cursor = Cursors.Default;
                if (result)
                {
                    beTable.Enabled = true;
                    MessageBox.Show("与远程服务器连接测试成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    beTable.Enabled = false;
                    MessageBox.Show("与服务器连接测试失败。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch
            {
                pbcWaittingBar.Visible = false;
                lblTimerValue.Visible = false;
                hlnkCancel.Visible = false;
                Cursor = Cursors.Default;
                MessageBox.Show("无法连接到远程服务器，获取失败！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
        /// 清除系统条件
        /// </summary>
        private void ClearSystemCondition()
        {
            txtUserName.Text = string.Empty;
            cmbQueriedUserType.SelectedNode = null;
            cmbQueriedUserType.SelectedNode = null;
            icmbAudit.SelectedIndex = 0;
            deStartTime.EditValue = null;
            deEndTime.EditValue = null;
            deFromTime.EditValue = null;
            deToTime.EditValue = null;
        }
        
        /// <summary>
        /// 清除字段条件
        /// </summary>
        private void ClearDataFieldCondition()
        {
            DataTable dataTable = gcCondition.DataSource as DataTable;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                dataRow["Condition"] = string.Empty;
            }
        }

        /// <summary>
        /// 获得查询条件
        /// </summary>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons()
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            string userName = txtUserName.Text.Trim();
            if (!string.IsNullOrWhiteSpace(userName))
            {
                whereConditons.Add(new WhereConditon("UserName", "UserName",
                    DbType.String, userName, DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (icmbAudit.SelectedIndex > 0)
            {
                byte auditedStatus = Convert.ToByte(icmbAudit.EditValue);
                whereConditons.Add(new WhereConditon("AuditedStatus", "AuditedStatus", DbType.Byte,
                    auditedStatus, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            /* 用户类型 */
            CommonNode userTypeCommonNode = cmbQueriedUserType.Value as CommonNode;
            if (userTypeCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("UserTypeId", "UserTypeId", DbType.Decimal, userTypeCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            /* 单位类型 */
            CommonNode departmentCommonNode = cmbQueriedDepartment.Value as CommonNode;
            if (departmentCommonNode != null)
            {
                whereConditons.Add(new WhereConditon("DepId", "DepId", DbType.Decimal, departmentCommonNode.NodeId,
                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deStartTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("CreationTime", "CreationTime0", DbType.DateTime,
                    deStartTime.DateTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deEndTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("CreationTime", "CreationTime1", DbType.DateTime,
                    deEndTime.DateTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deFromTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("ModificationTime", "ModificationTime0", DbType.DateTime,
                    deFromTime.DateTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deToTime.DateTime))
            {
                whereConditons.Add(new WhereConditon("ModificationTime", "ModificationTime1", DbType.DateTime,
                    deToTime.DateTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            return whereConditons;
        }

        #endregion

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerfiy_Click(object sender, EventArgs e)
        {
            Test();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkCancel_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            networkConnection.CancelTest = true;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiConfirm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string remoteAddress = txtRemoteAddress.Text.Trim();
            string remoteUserName = txtRemoteUserName.Text.Trim();
            string remotePassword = txtRemotePassword.Text.Trim();
            if (beTable.Tag == null)
            {
                MessageBox.Show("请先选择数据表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode commonNode = beTable.Tag as CommonNode;
            DataTable dataTable = gcCondition.DataSource as DataTable;
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("没有选择任何字段名称, 请选择！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                string userName = txtUserName.Text;
                IList<WhereConditon> whereConditons = GetWhereConditons();
                StringBuilder sb = new StringBuilder();
                if (remoteServerContract == null)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("请重新校验。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                IList<decimal> dataFieldIds = new List<decimal>(dataTable.Rows.Count);
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    bool selected = (bool)dataRow["Selected"];
                    if (!selected)
                    {
                        continue;
                    }
                    dataFieldIds.Add((decimal)dataRow["DataFieldId"]);
                }
                DataTable dt = remoteServerContract.GetTableData(remoteUserName, remotePassword, commonNode.NodeId, dataFieldIds, string.Empty, whereConditons);
                if (dt == null)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("请重新获取！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UpdateDataTable?.Invoke(dt);
                Cursor = Cursors.Default;
                this.Close();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }      

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemove_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (MessageBox.Show("确定清除字段条件？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                ClearDataFieldCondition();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnQuery_Click(object sender, EventArgs e)
        {
            string remoteAddress = txtRemoteAddress.Text.Trim();
            string remoteUserName = txtRemoteUserName.Text.Trim();
            string remotePassword = txtRemotePassword.Text.Trim();
            if (beTable.Tag == null)
            {
                MessageBox.Show("请先选择数据表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode commonNode = beTable.Tag as CommonNode;
            DataTable dataTable = gcCondition.DataSource as DataTable;
            if (dataTable.Rows.Count == 0)
            {
                MessageBox.Show("没有选择任何字段名称, 请选择！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                string userName = txtUserName.Text;
                IList<WhereConditon> whereConditons = GetWhereConditons();
                StringBuilder sb = new StringBuilder();
                if (remoteServerContract == null)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("请重新校验。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }                
                int count = remoteServerContract.GetRecordCount(remoteUserName, remotePassword, commonNode.NodeId, string.Empty, whereConditons);
                lblResult.Text = string.Format("查询结果：{0}条。", count);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }            
        }

        /// <summary>
        /// 清除系统条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnClear_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定清除系统条件？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                ClearSystemCondition();
            }
        }

        /// <summary>
        /// 选择表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beTable_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            try
            {
                string remoteAddress = txtRemoteAddress.Text.Trim();
                string remoteUserName = txtRemoteUserName.Text.Trim();
                string remotePassword = txtRemotePassword.Text.Trim();
                if (string.IsNullOrWhiteSpace(remoteAddress))
                {
                    MessageBox.Show("远程交换地址不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(remoteUserName))
                {
                    MessageBox.Show("用户名不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (string.IsNullOrWhiteSpace(remotePassword))
                {
                    MessageBox.Show("密码不能为空。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Cursor = Cursors.WaitCursor;
                try
                {
                    remoteServerContract = RemoteChannelFactory.CreateRemoteServerContract(remoteAddress, CurrentConfig.Instance.Port);
                    RemoteTableItemsForm frmRemoteTableItems = new RemoteTableItemsForm();
                    frmRemoteTableItems.Text = "数据表选择";
                    frmRemoteTableItems.ToolTip = "提示：只能选择数据表的节点。";
                    frmRemoteTableItems.RemoteServerContract = remoteServerContract;
                    frmRemoteTableItems.UserName = remoteUserName;
                    frmRemoteTableItems.Password = remotePassword;
                    frmRemoteTableItems.NodeSelected = delegate (CommonNode node)
                    {
                        if (node != null)
                        {
                            beTable.Text = node.NodeName;
                            beTable.Tag = node;
                            IList<CommonNode> commonNodes = remoteServerContract.GetCommonNodesByTableId(remoteUserName, remotePassword, node.NodeId, DataFieldFilter.PhysicalFieldAndLogicalField);
                            DataTable dataTable = gcCondition.DataSource as DataTable;
                            dataTable.Rows.Clear();
                            if (commonNodes != null)
                            {
                                foreach (CommonNode childNode in commonNodes)
                                {
                                    DataRow dataRow = dataTable.NewRow();
                                    dataRow["DataFieldId"] = childNode.NodeId;
                                    dataRow["DataFieldName"] = childNode.NodeName;
                                    dataRow["Selected"] = true;
                                    dataRow["Combine"] = 0;
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                        else
                        {
                            beTable.Text = string.Empty;
                            beTable.Tag = null;
                        }
                    };
                    Cursor = Cursors.Default;
                    frmRemoteTableItems.ShowDialog();
                }
                catch
                {
                    Cursor = Cursors.Default;
                    beTable.Text = "远程服务器链接失败。";
                    beTable.Tag = null;
                    MessageBox.Show("远程服务器链接失败。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribtnCondition_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (beTable.Tag == null)
            {
                MessageBox.Show("请先选择数据表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //CommonNode commonNode = cmbTable.SelectedNode.Tag as CommonNode;
            //DatabaseNodeType exportTableType = (DatabaseNodeType)commonNode.NodeType;
            //if (exportTableType != DatabaseNodeType.CustomTable)
            //{
            //    MessageBox.Show("请选择正确的本地表类型！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //decimal dataFieldId = DataConvertionHelper.GetDecimal(gridView.GetFocusedRowCellValue("DataFieldId"));
            //QueryConditionForm frmQueryCondition = new QueryConditionForm();
            //frmQueryCondition.IsValidate = true;
            //frmQueryCondition.TableName = customTableContract.GetTablePhysicalName(commonNode.NodeId);
            //frmQueryCondition.CustomDataFieldInfo = customDataFieldContract.GetModeInfo(dataFieldId);
            //if (gridView.FocusedValue != null)
            //{
            //    frmQueryCondition.QueryCondition = gridView.FocusedValue.ToString();
            //}
            //else
            //{
            //    frmQueryCondition.QueryCondition = string.Empty;
            //}
            //frmQueryCondition.UdateTextCallback = (where) =>
            //{
            //    gridView.SetFocusedValue(where);
            //};
            //frmQueryCondition.ShowDialog();
        }
    }
}
