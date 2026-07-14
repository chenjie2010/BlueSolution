using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Core;
using Blue.CustomLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsControls;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class DataVerifictionForm : Form
    {
        #region 私有变量

        private readonly TreeViewHandler<TreeNode> treeViewHandler;
        private IList<WhereConditon> whereConditons = null;
        private decimal tableId = 0;

        #endregion

        #region 契约接口

        private readonly ICustomDatabaseContract customDatabaseContract;
        private readonly ICustomCategoryContract customCategoryContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomExpressionContract customExpressionContract;
        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly IDataBussinessContract dataBussinessContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataVerifictionForm()
        {
            InitializeComponent();
            customDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            customCategoryContract = BusinessChannelFactory.CreateCustomCategoryContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customExpressionContract = BusinessChannelFactory.CreateCustomExpressionContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            dataBussinessContract = BusinessChannelFactory.CreateDataBussinessContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            dataTableDropdownList.CustomDatabaseContract = customDatabaseContract;
            dataTableDropdownList.CustomCategoryContract = customCategoryContract;
            dataTableDropdownList.CustomTableContract = customTableContract;
            ddpTable.CustomDatabaseContract = customDatabaseContract;
            ddpTable.CustomCategoryContract = customCategoryContract;
            ddpTable.CustomTableContract = customTableContract;            

            UserControlHelper.InitCheckedComboBoxEditItems(ccmbAuditedState, typeof(AuditedStatus));
            UserControlHelper.InitImageComboBoxEdit(icmbAuditedState, typeof(AuditedStatus));
            UserControlHelper.InitImageComboBoxEdit(icmbCurrentState, typeof(CurrentState));
            treeViewHandler = new TreeViewHandler<TreeNode>(treeView);
        }

        #endregion

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataConvertionForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            cmbUserType.TreeDropdownHandler = new UserTypeTreeDropdownList(customGroupContract, userTypeContract);
            cmbUserType.InitalizeTreeView();
            cmbDepartment.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
            cmbDepartment.InitalizeTreeView();
            dataTableDropdownList.LoadData();
            ddpTable.TableFilter = TableFilter.MasterSlaveTable;
            ddpTable.LoadData();
            InitTreeView();
            UserControlHelper.InitCheckedListBoxItems(chklstVerifyType, typeof(VerificationCategory));
            gridControlResult.DataSource = CreateDataTable();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataTableDropdownList.SelectedNode == null)
                {
                    MessageBox.Show("请先选择数据表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CommonNode commonNode = dataTableDropdownList.SelectedNode as CommonNode;                
                tableId = commonNode.NodeId;
                string tablePhysicalName =  customTableContract.GetTablePhysicalName(tableId);
                whereConditons = GetWhereConditons(tablePhysicalName);
                devExpressGrid.CurrentPageIndex = 0;
                LoadData();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearSystemCondition();
            lblProcessing.Text = "0条";
            icmbAuditedState.SelectedIndex = 0;
            devExpressGrid.DataSource = null;
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSumbit_Click(object sender, EventArgs e)
        {
            if (dataTableDropdownList.SelectedNode == null)
            {
                MessageBox.Show("请先选择数据表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode commonNode = dataTableDropdownList.SelectedNode as CommonNode;
            tableId = commonNode.NodeId;
            try
            {
                AuditedStatus auditedStatus = (AuditedStatus)Convert.ToByte(icmbAuditedState.EditValue);
                string name = UserEnumHelper.GetEnumText(auditedStatus);
                if (MessageBox.Show(string.Format("确认对满足条件的记录进行[{0}]操作吗？", name), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    string tablePhysicalName = customTableContract.GetTablePhysicalName(tableId);
                    whereConditons = GetWhereConditons(tablePhysicalName);
                    Cursor = Cursors.WaitCursor;
                    int count = customTableContract.SetAuditedStatus(commonNode.NodeId, whereConditons, auditedStatus);
                    LoadData();
                    Cursor = Cursors.Default;
                    MessageBox.Show(string.Format("共有{0}条记录被设置为[{1}]状态！", count, name), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }
        
        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            LoadData();
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridResult_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnVerify_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认进行校验操作吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                gridResult.Columns[4].Caption = "错误数据单元格信息描述";
                Check(true, "校验");
            }
        }

        /// <summary>
        /// 修正
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAmend_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确认进行自动更正操作吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                gridResult.Columns[4].Caption = "已自动更正的单元格数";
                Check(false, "自动更正");
            }
        }

        /// <summary>
        /// 自定义系统字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            SystemConfigHelper.SetColumnDisplayText(e);
        }

        /// <summary>
        /// 展开表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null && e.Node.Nodes.Count == 1)
            {
                CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                {
                    CommonNode commonNode = e.Node.Tag as CommonNode;
                    DatabaseNodeType databaseNodeType = GetNodeType(e.Node.Level + 1);
                    IList<CommonNode> commonNodes = null;
                    switch (databaseNodeType)
                    {
                        case DatabaseNodeType.Database:
                            commonNodes = customDatabaseContract.GetChildNodes(commonNode.NodeId);
                            break;

                        case DatabaseNodeType.Category:
                            commonNodes = customCategoryContract.GetChildNodes(commonNode.NodeId);
                            break;

                        case DatabaseNodeType.Table:
                            commonNodes = customTableContract.GetCommonNodes(commonNode.NodeId, TableFilter.All);
                            foreach (CommonNode node in commonNodes)
                            {
                                node.IsLeaf = true;
                            }
                            break;

                        default:
                            throw new ArgumentException("不支持该类型节点。");
                    }
                    treeViewHandler.LoadPartialNodes(e.Node, commonNodes);
                }
            }
        }

        /// <summary>
        /// 自动全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (!e.Node.IsExpanded)
            {
                e.Node.ExpandAll();
            }
            foreach (TreeNode tn in e.Node.Nodes)
            {
                tn.Checked = e.Node.Checked;
            }
        }

        /// <summary>
        /// 选择表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ddpTable_AfterTreeNodeSelect(object sender, TreeViewEventArgs e)
        {
            CommonNode commonNode = e.Node.Tag as CommonNode;
            if (commonNode == null)
            {
                return;
            }
            cmbDataField.EditValue = null;
            cmbDataField.Properties.Items.Clear();
            IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodes(commonNode.NodeId, DataFieldFilter.PhysicalFieldAndLogicalField);
            byte sourceDataWarehouseId = customTableContract.GetDataWarehouseId(commonNode.NodeId);
            txtDataField.Text = string.Empty;           
            IList<CommonNode> systemCommonNodes = DataFieldHelper.GetSystemDataFieldCommonNodes();
            for (int i = 0; i < systemCommonNodes.Count; i++)
            {
                cmbDataField.Properties.Items.Add(new CommonNode(systemCommonNodes[i].NodeId * -1, systemCommonNodes[i].NodeName));
            }
            for (int i = 0; i < commonNodes.Count; i++)
            {
                cmbDataField.Properties.Items.Add(new CommonNode(commonNodes[i].NodeId, commonNodes[i].NodeName));
            }
        }


        /// <summary>
        /// 设置条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSourceDataField_Click(object sender, EventArgs e)
        {
            if (cmbDataField.SelectedItem != null && ddpTable.SelectedNode != null)
            {
                CommonNode commonNode = (CommonNode)cmbDataField.SelectedItem;
                CustomDataFieldInfo customDataFieldInfo = null;
                QueryConditionForm frmQueryCondition = new QueryConditionForm();
                frmQueryCondition.QueryCondition = string.Empty;
                frmQueryCondition.IsValidate = true;
                if (commonNode.NodeId > 0)
                {
                    customDataFieldInfo = customDataFieldContract.GetModelInfo(commonNode.NodeId);
                    frmQueryCondition.TableName = customTableContract.GetTablePhysicalName(customDataFieldInfo.TableId);
                    if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.LogicalDataField)
                    {
                        IList<CommonNode> commonNodes = customExpressionContract.GetCommonNodes(customDataFieldInfo.DataFieldId);
                        customDataFieldInfo.PhysicalName = customDataFieldContract.GetExpressionDataFieldName(frmQueryCondition.TableName, customDataFieldInfo.ExpressionText, commonNodes);
                    }
                }
                else
                {
                    frmQueryCondition.TableName = customTableContract.GetTablePhysicalName(ddpTable.SelectedNode.NodeId);
                    SystemDataField systemDataField = (SystemDataField)Convert.ToByte(commonNode.NodeId * -1);
                    customDataFieldInfo = CommonBussinessHelper.GetExtendedCustomDataFieldInfo(ddpTable.SelectedNode.NodeId, frmQueryCondition.TableName, systemDataField);
                }
                frmQueryCondition.CustomDataFieldInfo = customDataFieldInfo;
                frmQueryCondition.UpdateTextHandler = (where) =>
                {
                    txtDataField.Text += where;
                };
                frmQueryCondition.ShowDialog();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStateQuery_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddpTable.SelectedNode == null)
                {
                    MessageBox.Show("请先选择数据表!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                devStateData.CurrentPageIndex = 0;
                string condition = txtDataField.Text.Trim();
                LoadStateData(ddpTable.SelectedNode.NodeId, condition);
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStateClear_Click(object sender, EventArgs e)
        {
            txtDataField.Text = string.Empty;
        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devStateData_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            if (ddpTable.SelectedNode != null)
            {
                devStateData.CurrentPageIndex = e.NewPageIndex;
                string condition = txtDataField.Text.Trim();
                LoadStateData(ddpTable.SelectedNode.NodeId, condition);
            }
        }

        /// <summary>
        /// 自定义列显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devStateData_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName == "AuditedStatus")
            {
                AuditedStatus auditedStatus = (AuditedStatus)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(auditedStatus);
            }
            else if (e.Column.FieldName == "CurrentState")
            {
                CurrentState currentState = (CurrentState)Convert.ToByte(e.Value);
                e.DisplayText = UserEnumHelper.GetEnumText(currentState);
            }
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateCurrentState_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddpTable.SelectedNode != null)
                {
                    CurrentState currentState = (CurrentState)Convert.ToByte(icmbCurrentState.EditValue);
                    string condition = txtDataField.Text.Trim();
                    Cursor = Cursors.WaitCursor;
                    int count = dataBussinessContract.UpdateCurrentState(ddpTable.SelectedNode.NodeId, condition, currentState);
                    Cursor = Cursors.Default;
                    MessageBox.Show(string.Format("共有{0}条记录被设置为[{1}]状态。", count, UserEnumHelper.GetEnumText(currentState)), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="condition"></param>
        private void LoadStateData(decimal tableId, string condition)
        {
            Cursor = Cursors.WaitCursor;
            int totalCount = 0;
            devStateData.DataKeyNames = new string[] { DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId) };
            devStateData.DataSource = dataBussinessContract.GetPageRecordByTableId(tableId, devStateData.PageSize * devStateData.CurrentPageIndex,
                devStateData.PageSize, condition, ref totalCount).Tables[0];
            devStateData.RecordCount = totalCount;
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 初始化树第一层节点
        /// </summary>
        private void InitTreeView()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            IList<EnumItem> dataWarehouses = UserEnumHelper.GetEnumItems(typeof(DataWarehouse));
            foreach (EnumItem enumItem in dataWarehouses)
            {
                commonNodes.Add(new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text, string.Empty, false));
            }
            treeViewHandler.InitFirstLevelNodes(commonNodes);
        }

        /// <summary>
        /// 获得数据库节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private DatabaseNodeType GetNodeType(int level)
        {
            DatabaseNodeType nodeType = DatabaseNodeType.DataWarehouse;

            /* 第一层为数据仓库，第二层为数据库，第三层为分组，第四层为数据表 */
            switch (level)
            {
                case 0:
                    nodeType = DatabaseNodeType.DataWarehouse;
                    break;

                case 1:
                    nodeType = DatabaseNodeType.Database;
                    break;

                case 2:
                    nodeType = DatabaseNodeType.Category;
                    break;

                case 3:
                    nodeType = DatabaseNodeType.Table;
                    break;
            }

            return nodeType;
        }

        /// <summary>
        /// 获得查询条件
        /// </summary>
        /// <param name="tablePhysicalName"></param>
        /// <returns></returns>
        private IList<WhereConditon> GetWhereConditons(string tablePhysicalName)
        {
            whereConditons = new List<WhereConditon>();

            string condition = txtCondition.Text;
            if (!string.IsNullOrWhiteSpace(condition))
            {
                string userName = Regex.Replace(condition, " {1,}", "%");
                whereConditons.Add(new WhereConditon("UserAccount", "UserName", "UserName", DbType.String, userName,
                   DataFieldCondition.Like, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                whereConditons.Add(new WhereConditon("UserAccount", "EmailAddress", "EmailAddress", DbType.String, userName,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                whereConditons.Add(new WhereConditon("UserAccount", "UserIdentity", "UserIdentity", DbType.String, userName,
                      DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));                
                string userActualName = Regex.Replace(condition, " {1,}", "%");
                whereConditons.Add(new WhereConditon("UserAccount", "UserActualName", "UserActualName", DbType.String, userActualName,
                   DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
            }
            UserControlHelper.GetWhereConditons(ccmbAuditedState, whereConditons, string.Empty, "AuditedStatus");            
            if (cmbUserType.SelectedNode != null)
            {
                CommonNode commonNode = cmbUserType.SelectedNode as CommonNode;
                whereConditons.Add(new WhereConditon(tablePhysicalName, "UserTypeId", "UserTypeId", DbType.Decimal,
                    commonNode.NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (cmbDepartment.SelectedNode != null)
            {
                CommonNode commonNode = cmbDepartment.SelectedNode as CommonNode;
                whereConditons.Add(new WhereConditon(tablePhysicalName, "DepID", "DepID", DbType.Decimal,
                    commonNode.NodeId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deStartTime.DateTime))
            {
                whereConditons.Add(new WhereConditon(tablePhysicalName, "CreationTime", "CreationTime0", DbType.DateTime,
                    deStartTime.DateTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deEndTime.DateTime))
            {
                whereConditons.Add(new WhereConditon(tablePhysicalName, "CreationTime", "CreationTime1", DbType.DateTime,
                    deEndTime.DateTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deFromTime.DateTime))
            {
                whereConditons.Add(new WhereConditon(tablePhysicalName, "ModificationTime", "ModificationTime0", DbType.DateTime,
                    deFromTime.DateTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            if (!DataConvertionHelper.IsNullValue(deToTime.DateTime))
            {
                whereConditons.Add(new WhereConditon(tablePhysicalName, "ModificationTime", "ModificationTime1", DbType.DateTime,
                    deToTime.DateTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }

            return whereConditons;
        }
            
        /// <summary>
        /// 数据加载
        /// </summary>
        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                int totalCount = 0;
                devExpressGrid.DataKeyNames = new string[] { DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId) };
                devExpressGrid.DataSource = customTableContract.GetTableData(tableId, devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex,
                    devExpressGrid.PageSize, whereConditons, null, ref totalCount).Tables[0];
                devExpressGrid.RecordCount = totalCount;                
                lblProcessing.Text = string.Format("{0}条", totalCount);
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
        /// 清除条件
        /// </summary>
        private void ClearSystemCondition()
        {
            txtCondition.Text = string.Empty;
            cmbUserType.SelectedNode = null;
            cmbDepartment.SelectedNode = null;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbAuditedState);
            deStartTime.EditValue = null;
            deEndTime.EditValue = null;
            deFromTime.EditValue = null;
            deToTime.EditValue = null;
        }
        
        /// <summary>
        /// 创建字段对应关系的表结构
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("CustomTableName", Type.GetType("System.String"));
            dataTable.Columns.Add("VerifyType", Type.GetType("System.String"));
            dataTable.Columns.Add("CustomDataFieldNameKey", Type.GetType("System.String"));
            dataTable.Columns.Add("CustomDataFieldNameValue", Type.GetType("System.String"));
            dataTable.Columns.Add("RecordCount", Type.GetType("System.String"));            

            return dataTable;
        }
        
        /// <summary>
        /// 校验操作
        /// </summary>
        /// <param name="selectOrUpdate"></param>
        /// <param name="description"></param>
        private void Check(bool selectOrUpdate, string description)
        {
            if (chklstVerifyType.SelectedItems.Count == 0)
            {
                MessageBox.Show("请先选择校验类型!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<CommonNode> commonNodes = new List<CommonNode>();
            foreach (TreeNode tnFirstLevel in treeView.Nodes)
            {
                foreach (TreeNode tnScdLevel in tnFirstLevel.Nodes)
                {
                    foreach (TreeNode tnTrdLevel in tnScdLevel.Nodes)
                    {
                        foreach (TreeNode treeNode in tnTrdLevel.Nodes)
                        {
                            if (!string.IsNullOrWhiteSpace(treeNode.Text) && treeNode.Checked)
                            {
                                CommonNode commonNode = treeNode.Tag as CommonNode;
                                commonNodes.Add(commonNode);
                            }
                        }
                    }
                }
            }
            if (commonNodes.Count == 0)
            {
                MessageBox.Show("请先选择校验对象!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            ProgressForm frmProgress = new ProgressForm();
            try
            {
                frmProgress.MinValue = 0;
                frmProgress.TopMost = true;
                frmProgress.MaxValue = chklstVerifyType.CheckedItems.Count * commonNodes.Count + 1;
                frmProgress.Show();
                frmProgress.Tip = string.Format("正准备{0}操作...", description);
                frmProgress.IncreaseStep();
                Application.DoEvents();
                DataTable dataTable = (DataTable)gridControlResult.DataSource;
                dataTable.Rows.Clear();
                bool exit = false;
                foreach (DevExpress.XtraEditors.Controls.CheckedListBoxItem item in chklstVerifyType.Items)
                {
                    if (item.CheckState == CheckState.Checked)
                    {
                        VerificationCategory verificationCategory = (VerificationCategory)Convert.ToByte(item.Value);
                        foreach (CommonNode commonNode in commonNodes)
                        {
                            frmProgress.Tip = string.Format("正准备{0}{1}...", description, commonNode.NodeName);
                            frmProgress.IncreaseStep();
                            Application.DoEvents();
                            IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> results = null;
                            IDictionary<SystemPhysicalDataField, CommonItemList<int, string>> userResults = null;
                            CommonItemList<int, string> userData = null;
                            switch (verificationCategory)
                            {
                                case VerificationCategory.UserInfo:
                                    userResults = customTableContract.CheckUserConsistency(commonNode.NodeId, selectOrUpdate);
                                    break;

                                case VerificationCategory.Enum:
                                    results = customTableContract.CheckEnumConsistency(commonNode.NodeId, selectOrUpdate);
                                    break;

                                case VerificationCategory.Association:
                                    results = customTableContract.CheckAssociationConsistency(commonNode.NodeId, selectOrUpdate);
                                    break;

                                case VerificationCategory.Relation:
                                    results = customTableContract.CheckRelationConsistency(commonNode.NodeId, selectOrUpdate);
                                    break;

                                case VerificationCategory.Redundancy:
                                    userData = customTableContract.CheckUserDataConsistency(commonNode.NodeId, selectOrUpdate);
                                    break;

                            }
                            if (verificationCategory == VerificationCategory.UserInfo)
                            {
                                foreach (KeyValuePair<SystemPhysicalDataField, CommonItemList<int, string>> keyValue in userResults)
                                {
                                    DataRow dr = dataTable.NewRow();                                    
                                    dr["CustomTableName"] = commonNode.NodeName;
                                    dr["VerifyType"] = UserEnumHelper.GetEnumText(verificationCategory);
                                    dr["CustomDataFieldNameKey"] = DataFieldHelper.GetPhysicalDataFieldCaption(keyValue.Key);
                                    dr["CustomDataFieldNameValue"] = string.Empty;
                                    CommonItemList<int, string> commonItemList = keyValue.Value;
                                    if (selectOrUpdate)
                                    {
                                        StringBuilder sb = new StringBuilder();
                                        if (commonItemList.Value == 0)
                                        {
                                            sb.Append("未发现错误数据单元格。");
                                        }
                                        else if (commonItemList.Value <= 100)
                                        {
                                            sb.AppendFormat("共有{0}错误数据单元格，用户分别为：", commonItemList.Value);
                                            foreach (string userName in commonItemList.CommonList)
                                            {
                                                sb.AppendFormat("{0},", userName);
                                            }
                                            sb.Remove(sb.Length - 1, 1);
                                            sb.Append("。");
                                        }
                                        else
                                        {
                                            sb.AppendFormat("共有{0}错误数据单元格，错误用户过多，只显示其中100个用户，用户分别为：", commonItemList.Value);
                                            int idx = 0;
                                            foreach (string userName in commonItemList.CommonList)
                                            {
                                                sb.AppendFormat("{0},", userName);
                                                if (idx++ > 100)
                                                {
                                                    break;
                                                }
                                            }
                                            sb.Remove(sb.Length - 1, 1);
                                            sb.Append("。");
                                        }
                                        dr["RecordCount"] = sb.ToString();
                                    }
                                    else
                                    {

                                        dr["RecordCount"] = commonItemList.Value.ToString();
                                    }
                                    dataTable.Rows.Add(dr);
                                }
                            }
                            else if (verificationCategory == VerificationCategory.Redundancy)
                            {
                                DataRow dr = dataTable.NewRow();
                                dr["CustomTableName"] = commonNode.NodeName;
                                dr["VerifyType"] = UserEnumHelper.GetEnumText(verificationCategory);
                                dr["CustomDataFieldNameKey"] = "用户不存在的记录";
                                dr["CustomDataFieldNameValue"] = string.Empty;
                                if (selectOrUpdate)
                                {
                                    StringBuilder sb = new StringBuilder();
                                    if (userData.Value == 0)
                                    {
                                        sb.Append("未发现用户不存在的记录。");
                                    }
                                    else if (userData.Value <= 100)
                                    {
                                        sb.AppendFormat("共有{0}条记录中的用户不存在，用户分别为：", userData.Value);
                                        foreach (string userName in userData.CommonList)
                                        {
                                            sb.AppendFormat("{0},", userName);
                                        }
                                        sb.Remove(sb.Length - 1, 1);
                                        sb.Append("。");
                                    }
                                    else
                                    {
                                        sb.AppendFormat("共有{0}条记录中的用户不存在，错误用户过多，只显示其中100个用户，用户分别为：", userData.Value);
                                        int idx = 0;
                                        foreach (string userName in userData.CommonList)
                                        {
                                            sb.AppendFormat("{0},", userName);
                                            if (idx++ > 100)
                                            {
                                                break;
                                            }
                                        }
                                        sb.Remove(sb.Length - 1, 1);
                                        sb.Append("。");
                                    }
                                    dr["RecordCount"] = sb.ToString();
                                }
                                else
                                {

                                    dr["RecordCount"] = userData.Value.ToString();
                                }
                                dataTable.Rows.Add(dr);
                            }
                            else
                            {
                                foreach (KeyValuePair<decimal, IDictionary<decimal, CommonItemList<int, string>>> keyValue in results)
                                {
                                    string dataFieldLogicalName = customDataFieldContract.GetLogicalName(keyValue.Key);
                                    foreach (KeyValuePair<decimal, CommonItemList<int, string>> error in keyValue.Value)
                                    {
                                        DataRow dr = dataTable.NewRow();
                                        dr["CustomTableName"] = commonNode.NodeName;
                                        dr["VerifyType"] = UserEnumHelper.GetEnumText(verificationCategory);
                                        dr["CustomDataFieldNameKey"] = dataFieldLogicalName;
                                        dr["CustomDataFieldNameValue"] = customDataFieldContract.GetLogicalName(error.Key);
                                        CommonItemList<int, string> commonItemList = error.Value;
                                        if (selectOrUpdate)
                                        {
                                            StringBuilder sb = new StringBuilder();
                                            if (commonItemList.Value == 0)
                                            {
                                                sb.Append("未发现错误数据单元格。");
                                            }
                                            else if (commonItemList.Value <= 100)
                                            {
                                                sb.AppendFormat("共有{0}错误数据单元格，用户分别为：", commonItemList.Value);
                                                foreach (string userName in commonItemList.CommonList)
                                                {
                                                    sb.AppendFormat("{0},", userName);
                                                }
                                                sb.Remove(sb.Length - 1, 1);
                                            }
                                            else
                                            {
                                                sb.AppendFormat("共有{0}错误数据单元格，错误用户过多，不再显示用户详情。", commonItemList.Value);
                                            }
                                            dr["RecordCount"] = sb.ToString();
                                        }
                                        else
                                        {

                                            dr["RecordCount"] = commonItemList.Value.ToString();
                                        }
                                        dataTable.Rows.Add(dr);
                                    }
                                }
                            }
                            if (frmProgress != null && frmProgress.Cancel)
                            {
                                exit = true;
                                break; ;
                            }
                        }
                        if (exit)
                        {
                            frmProgress.CloseFrom();
                            frmProgress = null;
                            MessageBox.Show(string.Format("{0}操作已取消！", description), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }
                frmProgress.CloseFrom();
                frmProgress = null;
                MessageBox.Show(string.Format("{0}操作完成！", description), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                if (frmProgress != null && !frmProgress.IsDisposed)
                {
                    frmProgress.CloseFrom();
                }
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }            
        }

        #endregion

    }
}
