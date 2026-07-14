using System;
using System.IO;
using System.Data;
using System.Data.SqlTypes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.MyAuditingModule
{
    public partial class TableAuditingControl : UserControl
    {
        #region 常量

        /* 第 0 列为多选列,第 1 列为审核状态列 */
        private const int AUDITING_GRID_VIEW_FIXED_COLUMN_COUNT = 2;

        #endregion

        #region 私有变量

        private IList<decimal> selectedTables = null;
        private decimal selectedUserId = decimal.MinValue;
        private IList<WhereConditon> groupWhereConditons = null;
        private DataFieldEditedState dataFieldEditedState = DataFieldEditedState.None;
        private RelaitonDataBusiness relationDataBusiness = null;

        /// <summary>
        /// PDF阅读器
        /// </summary>
        private static PDFViewerForm frmPDFViewer = new PDFViewerForm();

        /// <summary>
        /// 业务所属对象
        /// </summary>
        private CommonUserInfo commonUserInfo = null;

        #endregion

        #region 属性

        /// <summary>
        /// 审核类型
        /// </summary>
        public DataAuditingType DataAuditingType
        {
            get;
            set;
        }

        #endregion

        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly ICustomWorkflowInstanceContract customWorkflowInstanceContract;
        private readonly IDataAuditingContract dataAuditingContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public TableAuditingControl()
        {
            InitializeComponent();
            if (!this.DesignMode)
            {
                userAccountContract = UserChannelFactory.CreateUserAccount();
                customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
                customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
                customTableContract = BusinessChannelFactory.CreateCustomTableContract();
                customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
                customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
                customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
                associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
                customWorkflowInstanceContract = BusinessChannelFactory.CreateCustomWorkflowInstanceContract();
                dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();
                selectedTables = new List<decimal>();
                relationDataBusiness = new RelaitonDataBusiness(customDataFieldContract, customAssociationContract, customEnumContract,
                    associatedDataFieldContract, customDepartmentContract);
            }
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
                IList<byte> dataWarehouseIds = customRoleContract.GetDataWarehouseIds(CurrentUser.Instance.UserId, DataAuthorityType.Auditing);
                List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(DataWarehouse));
                int imageIndex = 0;
                foreach (EnumItem enumItem in enumItems)
                {
                    if (dataWarehouseIds.Contains(enumItem.Value))
                    {
                        UserControlHelper.AddItemToImageComboBoxEdit(icmbDataWarehouse, enumItem, imageIndex++);
                    }
                }
                if (imageIndex > 0)
                {
                    icmbDataWarehouse.SelectedIndex = 0;
                }
                else
                {
                    icmbDataWarehouse.EditValue = null;
                }
            }
        }

        /// <summary>
        /// 节点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            if (e.Node.Level == 0 || e.Node.Level == 1)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    if (!e.Node.IsExpanded)
                    {
                        e.Node.Expand();
                    }
                    foreach (TreeNode tn in e.Node.Nodes)
                    {
                        if (tn.Checked != e.Node.Checked)
                        {
                            tn.Checked = e.Node.Checked;
                        }
                    }
                }
            }
            else
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                if (e.Node.Checked == true)
                {
                    AddTabPages(commonNode);
                }
                else
                {
                    RemoveTabPages(commonNode);
                }
            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 数据仓库的选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbDataWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (icmbDataWarehouse.EditValue != null)
                {
                    treeView.Nodes.Clear();
                    byte dataWarehouseId = Convert.ToByte(icmbDataWarehouse.EditValue);
                    Dictionary<DatabaseNodeType, IList<CommonNode>> dicCommonNodes = customRoleContract.GetAuthorizedCommonNodes(CurrentUser.Instance.UserId, dataWarehouseId, DataAuthorityType.Auditing);
                    foreach (CommonNode databaseCommonNode in dicCommonNodes[DatabaseNodeType.Database])
                    {
                        TreeNode tnDatabase = new TreeNode { Text = databaseCommonNode.NodeName, Tag = databaseCommonNode };
                        treeView.Nodes.Add(tnDatabase);
                        foreach (CommonNode categoryCommonNode in dicCommonNodes[DatabaseNodeType.Category])
                        {
                            if (categoryCommonNode.ParentNodeId != databaseCommonNode.NodeId)
                            {
                                continue;
                            }
                            TreeNode tnCategory = new TreeNode { Text = categoryCommonNode.NodeName, Tag = categoryCommonNode };
                            tnDatabase.Nodes.Add(tnCategory);
                            foreach (CommonNode tableCommonNode in dicCommonNodes[DatabaseNodeType.Table])
                            {
                                if (tableCommonNode.ParentNodeId == categoryCommonNode.NodeId)
                                {
                                    TreeNode tnTable = new TreeNode { Text = tableCommonNode.NodeName, Tag = tableCommonNode };
                                    tnCategory.Nodes.Add(tnTable);
                                }
                            }
                        }
                        tnDatabase.Expand();
                    }
                }
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
        /// 节点展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.IsExpanded)
            {
                e.Node.ImageIndex = 1;
            }
            else
            {
                e.Node.ImageIndex = 0;
            }
        }

        /// <summary>
        /// 取消审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            Audit(false);
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAudit_Click(object sender, EventArgs e)
        {
            Audit(true);
        }

        /// <summary>
        /// 获得表的类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            pcContainer.HidePopup();
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                CommonNode node = devExpressGrid.Tag as CommonNode;
                DataTableType dataTableType = (DataTableType)node.NodeType;
                hlnkCurrent.Enabled = AuthorityHelper.CheckAuthority(devExpressGrid.Authority, (byte)GridViewAuthority.MasterSlave) &&
                    (devExpressGrid.RecordCount > 0 && devExpressGrid.FocusedRowHandle >= 0 && dataTableType == DataTableType.MasterSlaveTable);
                SetAuditingButtons(devExpressGrid, node);
            }
        }

        /// <summary>
        /// 设置为当前状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkCurrent_Click(object sender, EventArgs e)
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                CommonNode commonNode = devExpressGrid.Tag as CommonNode;
                if (devExpressGrid.FocusedDataRow == null)
                {
                    MessageBox.Show("请先选择记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
                decimal userId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[userIdName]);
                if (MessageBox.Show("确定设置当前记录的状态吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                    decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[recordIdName]);
                    Cursor = Cursors.WaitCursor;
                    dataAuditingContract.UpdateCurretStateByUserId(commonNode.NodeId, recordId, userId);
                    LoadUserData(devExpressGrid);
                    Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// 获得个人数据审核查询条件
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        private IList<WhereConditon> GetPersonalWhereConditons(DevExpressGrid grid, decimal userId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            CommonNode commonNode = grid.Tag as CommonNode;
            whereConditons.Add(new WhereConditon(commonNode.NodeCode, "UserId", "UserId", DbType.Decimal, selectedUserId,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));

            return whereConditons;
        }

        /// <summary>
        /// 获得个人数据审核排序条件
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private IList<SortingCondtion> GetPersonalSortingCondtions(DevExpressGrid grid)
        {
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            CommonNode commonNode = grid.Tag as CommonNode;
            sortingCondtions.Add(new SortingCondtion(commonNode.NodeCode, "RecordSorting", CustomSorting.Ascending));

            return sortingCondtions;
        }

        /// <summary>
        /// 获得分组数据审核排序条件
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        private IList<SortingCondtion> GetGroupSortingCondtions(DevExpressGrid grid)
        {
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            CommonNode commonNode = grid.Tag as CommonNode;
            sortingCondtions.Add(new SortingCondtion(commonNode.NodeCode, "ModificationTime", CustomSorting.Ascending));

            return sortingCondtions;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 个人数据审核
        /// </summary>
        /// <param name="userId"></param>
        public void LoadPersonalData(decimal userId)
        {
            Cursor = Cursors.WaitCursor;
            selectedUserId = userId;
            foreach (DevExpress.XtraTab.XtraTabPage tabPage in xtraTabControl.TabPages)
            {
                DevExpressGrid grid = tabPage.Tag as DevExpressGrid;
                IList<WhereConditon> whereConditons = GetPersonalWhereConditons(grid, userId);
                IList<SortingCondtion> sortingCondtions = GetPersonalSortingCondtions(grid);
                LoadUserData(grid, whereConditons, sortingCondtions);
            }
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                CommonNode node = devExpressGrid.Tag as CommonNode;
                DataTableType dataTableType = (DataTableType)customTableContract.GetTableType(node.NodeId);
                hlnkCurrent.Enabled = AuthorityHelper.CheckAuthority(devExpressGrid.Authority, (byte)GridViewAuthority.MasterSlave) &&
                    (devExpressGrid.RecordCount > 0 && devExpressGrid.FocusedRowHandle >= 0 && dataTableType == DataTableType.MasterSlaveTable);
            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 分组数据审核
        /// </summary>
        /// <param name="whereConditons"></param>
        public void LoadGroupData(IList<WhereConditon> whereConditons)
        {
            Cursor = Cursors.WaitCursor;
            groupWhereConditons = whereConditons;
            foreach (DevExpress.XtraTab.XtraTabPage tabPage in xtraTabControl.TabPages)
            {
                DevExpressGrid grid = tabPage.Tag as DevExpressGrid;
                LoadUserData(grid);
            }
            Cursor = Cursors.Default;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 数据审核
        /// </summary>
        /// <param name="grid"></param>
        private void LoadUserData(DevExpressGrid grid)
        {
            Cursor = Cursors.WaitCursor;
            switch (DataAuditingType)
            {
                case DataAuditingType.Personal:
                    IList<WhereConditon> personalWhereConditons = GetPersonalWhereConditons(grid, selectedUserId);
                    IList<SortingCondtion> personalSortingCondtions = GetPersonalSortingCondtions(grid);
                    LoadUserData(grid, personalWhereConditons, personalSortingCondtions);
                    break;

                case DataAuditingType.Group:
                    CommonNode commonNode = grid.Tag as CommonNode;
                    IList<SortingCondtion> groupSortingCondtions = GetGroupSortingCondtions(grid);
                    ChangeTableNameInGroupWhereConditon(commonNode);
                    LoadUserData(grid, groupWhereConditons, groupSortingCondtions);
                    break;
            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 修改分组条件中的表的名称
        /// </summary>
        /// <param name="commonNode"></param>
        /// <returns></returns>
        private void ChangeTableNameInGroupWhereConditon(CommonNode commonNode)
        {
            if (groupWhereConditons != null)
            {
                foreach (var groupWhereConditon in groupWhereConditons)
                {
                    if (string.IsNullOrWhiteSpace(groupWhereConditon.DataTableName))
                    {
                        groupWhereConditon.DataTableName = commonNode.NodeCode;
                    }
                }
            }
        }

        /// <summary>
        /// 增加表页面
        /// </summary>
        /// <param name="commonNode"></param>
        private void AddTabPages(CommonNode commonNode)
        {
            if (!selectedTables.Contains(commonNode.NodeId))
            {
                selectedTables.Add(commonNode.NodeId);
            }
            for (int i = 0; i < xtraTabControl.TabPages.Count; i++)
            {
                DevExpressGrid expressGrid = xtraTabControl.TabPages[i].Tag as DevExpressGrid;
                CommonNode node = expressGrid.Tag as CommonNode;
                if (commonNode.NodeId == node.NodeId)
                {
                    xtraTabControl.TabPages.RemoveAt(i);
                    break;
                }
            }
            DevExpress.XtraTab.XtraTabPage tabPage = new DevExpress.XtraTab.XtraTabPage();
            DevExpressGrid devExpressGrid = new DevExpressGrid();
            devExpressGrid.DataKeyNames = new string[] { DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId),
                DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId) };
            devExpressGrid.Editable = false;
            devExpressGrid.ColumnAutoWidth = false;
            devExpressGrid.Dock = DockStyle.Fill;
            devExpressGrid.Tag = commonNode;
            devExpressGrid.OnPageIndexChanged += devExpressGrid_OnPageIndexChanged;
            devExpressGrid.OnRowClick += DevExpressGrid_OnRowClick;
            devExpressGrid.RowCellStyle += DevExpressGrid_RowCellStyle;
            devExpressGrid.OnFocusedRowChanged += devExpressGrid_OnFocusedRowChanged;
            devExpressGrid.OnFocusedColumnChanged += devExpressGrid_OnFocusedColumnChanged;
            devExpressGrid.OnRowDoubleClick += devExpressGrid_OnRowDoubleClick;
            devExpressGrid.OnAddClick += devExpressGrid_OnAddClick;
            devExpressGrid.OnEditClick += devExpressGrid_OnEditClick;
            devExpressGrid.OnRowView += DevExpressGrid_OnRowView;
            devExpressGrid.OnRowEdit += DevExpressGrid_OnRowEdit;
            devExpressGrid.OnBatchEditClick += DevExpressGrid_OnBatchEditClick;
            devExpressGrid.OnCompleteEditClick += DevExpressGrid_OnCompleteEditClick;
            devExpressGrid.OnDeleteClick += devExpressGrid_OnDeleteClick;
            devExpressGrid.OnBatchDeleteClick += devExpressGrid_OnBatchDeleteClick;
            devExpressGrid.OnCompleteDeleteClick += devExpressGrid_OnCompleteDeleteClick;
            devExpressGrid.OnRecordSortingChanged += devExpressGrid_OnRecordSortingChanged;
            devExpressGrid.OnBeforePopupMenu += DevExpressGrid_OnBeforePopupMenu;
            devExpressGrid.OnCellValueChanged += DevExpressGrid_OnCellValueChanged;
            devExpressGrid.OnRefresh += devExpressGrid_OnRefresh;
            devExpressGrid.AppearanceHeaderHAlignment = HorzAlignment.Center;
            devExpressGrid.AppearanceCellHAlignment = HorzAlignment.Center;
            devExpressGrid.IsShowCheckBox = true;
            devExpressGrid.OnCustomColumnDisplayText += (sender, e) =>
            {
                SystemConfigHelper.SetColumnDisplayText(e);
            };
            devExpressGrid.StateColumnDataFieldName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
            Int64 authority = customRoleContract.GetTableAuthority(CurrentUser.Instance.UserId, commonNode.NodeId, DataAuthorityType.Auditing);
            if (DataAuditingType == DataAuditingType.Group)
            {
                authority = AuthorityHelper.GetAuthority(authority, Convert.ToByte(GridViewAuthority.Add), false);
            }
            devExpressGrid.Authority = authority;
            devExpressGrid.IsMainTable = ((DataTableType)commonNode.NodeType == DataTableType.PrimaryTable) ? true : false;
            IList<decimal> tableIds = new List<decimal>();
            tableIds.Add(commonNode.NodeId);
            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = customRoleContract.GetAuthorizedExtendedCustomDataFieldInfos(CurrentUser.Instance.UserId, tableIds, DataAuthorityType.Auditing);
            devExpressGrid.Data = extendedCustomDataFieldInfos;
            LoadUserData(devExpressGrid);
            tabPage.Padding = new Padding(2);
            tabPage.Text = commonNode.NodeName;
            tabPage.Tag = devExpressGrid;
            xtraTabControl.TabPages.Add(tabPage);
            xtraTabControl.SelectedTabPage = tabPage;
            tabPage.Controls.Add(devExpressGrid);
        }

        /// <summary>
        /// 查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevExpressGrid_OnRowView(object sender, RowEvent e)
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                LoadFormData(devExpressGrid, GridViewAuthority.View);
            }
        }

        /// <summary>
        /// 单元格值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevExpressGrid_OnCellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                this.ParentForm.BringToFront();
                DevExpressGrid devExpressGrid = sender as DevExpressGrid;
                CommonNode commonNode = devExpressGrid.Tag as CommonNode;
                e.Column.OptionsColumn.AllowEdit = false;
                e.Column.OptionsColumn.ReadOnly = true;
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                string bussinessIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId);
                string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
                decimal recordId = decimal.MinValue;
                decimal userId = decimal.MinValue;
                decimal businessId = decimal.MinValue;
                AuditedStatus auditedStatus = AuditedStatus.None;
                switch (dataFieldEditedState)
                {
                    case DataFieldEditedState.Edit:
                        if (e.RowHandle < 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("请选选择记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[recordIdName]);
                        auditedStatus = customTableContract.GetAuditedStatus(commonNode.NodeId, recordId);
                        if (auditedStatus != AuditedStatus.None)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("该记录处于{0}状态，不能编辑。", UserEnumHelper.GetEnumText(auditedStatus)), "警告信息",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;

                    case DataFieldEditedState.BathcEdit:
                        if (devExpressGrid.MultiSelectedValues.Count == 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("未进行批量选择，请先批量选择。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        IList<decimal> recordIds = new List<decimal>(devExpressGrid.MultiSelectedValues.Count);
                        foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                        {
                            recordId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                            auditedStatus = customTableContract.GetAuditedStatus(commonNode.NodeId, recordId);
                            if (auditedStatus != AuditedStatus.None)
                            {
                                Cursor = Cursors.Default;
                                MessageBox.Show(string.Format("批量编辑的记录存在处于{0}状态，不能批量编辑。", UserEnumHelper.GetEnumText(auditedStatus)),
                                    "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            recordIds.Add(recordId);
                        }
                        break;
                }
                IList<CommonDataField> relationDataFields = new List<CommonDataField>();
                IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = (IList<ExtendedCustomDataFieldInfo>)devExpressGrid.Data;
                int index = extendedCustomDataFieldInfos.FindIndex(dataFieldInfo => dataFieldInfo.PhysicalName.Equals(e.Column.FieldName));
                if (index >= 0)
                {
                    Cursor = Cursors.WaitCursor;
                    IList<CommonDataField> commonDataFields = new List<CommonDataField>();
                    ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = extendedCustomDataFieldInfos[index];
                    /* 1.验证是否允许为空 */
                    if (extendedCustomDataFieldInfo.RequiredDataField && ((e.Value == null) || string.IsNullOrWhiteSpace(e.Value.ToString())))
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("{0}不能为空！", extendedCustomDataFieldInfo.LogicalName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    DbType dbType = DataFieldHelper.GetDbType(physicalDataFieldType);
                    string result = string.Empty;
                    bool success = true;
                    object dataFieldValue = null;
                    if (e.Value != null && e.Value != DBNull.Value)
                    {
                        string stringValue = e.Value.ToString();
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.Boolean:
                                dataFieldValue = DataConvertionHelper.GetConvertedBoolean(e.Value);
                                break;

                            case PhysicalDataFieldType.Int32:
                                if (!string.IsNullOrEmpty(stringValue))
                                {
                                    //整数 
                                    Regex regexInt32 = new Regex(@"^-?\d+$");
                                    if (!regexInt32.IsMatch(stringValue))
                                    {
                                        result = string.Format("{0}不能为非整数。", extendedCustomDataFieldInfo.LogicalName);
                                        success = false;
                                        break;
                                    }
                                    //超过范围转换失败                                
                                    if (DataConvertionHelper.IsNullValue(DataConvertionHelper.GetConvertedInt(stringValue)))
                                    {
                                        result = string.Format("{0}的整数值的超出了整数限制范围(-2147483648~2147483647)。", extendedCustomDataFieldInfo.LogicalName);
                                        success = false;
                                        break;
                                    }
                                    dataFieldValue = DataConvertionHelper.GetConvertedInt(e.Value);
                                }
                                break;

                            case PhysicalDataFieldType.Decimal:
                                if (!string.IsNullOrEmpty(stringValue))
                                {
                                    //浮点数 
                                    Regex regexDecimal = new Regex(@"^(-?\d+)(\.\d+)?$");
                                    if (!regexDecimal.IsMatch(stringValue))
                                    {
                                        result = string.Format("{0}不能为非实数。", extendedCustomDataFieldInfo.LogicalName);
                                        success = false;
                                        break;
                                    }
                                    int pos = stringValue.IndexOf('.');
                                    if ((pos > 0 && (stringValue.Length - pos - 1) > extendedCustomDataFieldInfo.DataFieldLength) || stringValue.Length > 12)
                                    {
                                        result = string.Format("{0}的实数长度限制的范围(0~12位)，小数位长度不能超过{1}。", extendedCustomDataFieldInfo.LogicalName, extendedCustomDataFieldInfo.DataFieldLength);
                                        success = false;
                                        break;
                                    }
                                    dataFieldValue = DataConvertionHelper.GetConvertedDecimal(stringValue);
                                }
                                break;

                            case PhysicalDataFieldType.ArbitraryString:
                            case PhysicalDataFieldType.ExtendedArbitraryString:
                            case PhysicalDataFieldType.NumeralString:
                            case PhysicalDataFieldType.CharString:
                            case PhysicalDataFieldType.MixedString:
                            case PhysicalDataFieldType.EncryptedString:
                                switch (physicalDataFieldType)
                                {
                                    case PhysicalDataFieldType.NumeralString:
                                        if (!string.IsNullOrEmpty(stringValue))
                                        {
                                            //整数 
                                            Regex numeralRegex = new Regex(@"^-?\d+$");
                                            if (!numeralRegex.IsMatch(stringValue))
                                            {
                                                result = string.Format("{0}只能为数字组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                                success = false;
                                                break;
                                            }
                                        }
                                        break;

                                    case PhysicalDataFieldType.CharString:
                                        if (!string.IsNullOrEmpty(stringValue))
                                        {
                                            //由26个英文字母组成的字符串 
                                            Regex charRegex = new Regex(@"^[A-Za-z]+$");
                                            if (!charRegex.IsMatch(stringValue))
                                            {
                                                result = string.Format("{0}只能为由26个英文字母组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                                success = false;
                                                break;
                                            }
                                        }
                                        break;

                                    case PhysicalDataFieldType.MixedString:
                                        if (!string.IsNullOrEmpty(stringValue))
                                        {
                                            //由数字和26个英文字母组成的字符串  
                                            Regex mixedRegex = new Regex(@"^[A-Za-z0-9]+$");
                                            if (!mixedRegex.IsMatch(stringValue))
                                            {
                                                result = string.Format("{0}只能为由数字和26个英文字母组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                                success = false;
                                                break;
                                            }
                                        }
                                        break;

                                    case PhysicalDataFieldType.ExtendedArbitraryString:
                                    case PhysicalDataFieldType.ArbitraryString:
                                    case PhysicalDataFieldType.EncryptedString:
                                        if (!string.IsNullOrWhiteSpace(extendedCustomDataFieldInfo.RegexExpression))
                                        {
                                            Regex regex = new Regex(extendedCustomDataFieldInfo.RegexExpression);
                                            if (!regex.IsMatch(stringValue))
                                            {
                                                result = string.Format("{0}不符合要求的格式。", extendedCustomDataFieldInfo.LogicalName);
                                                success = false;
                                                break;
                                            }
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                if (physicalDataFieldType == PhysicalDataFieldType.EncryptedString)
                                {
                                    dataFieldValue = CryptographyHelper.Encrypt(stringValue);
                                }
                                else
                                {
                                    dataFieldValue = stringValue;
                                }
                                break;

                            case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                            case PhysicalDataFieldType.YearAndMonthAndDay:
                            case PhysicalDataFieldType.YearAndMonth:
                            case PhysicalDataFieldType.MonthAndDay:
                                DateTime currentDateTime = Convert.ToDateTime(e.Value);
                                switch (physicalDataFieldType)
                                {
                                    case PhysicalDataFieldType.YearAndMonth:
                                        DateTime dtValue = DateTime.Parse(string.Format("{0}-{1}-01", currentDateTime.Year, currentDateTime.Month));
                                        if (DataConvertionHelper.IsNullValue(dtValue) || dtValue <= SqlDateTime.MinValue.Value || dtValue >= SqlDateTime.MaxValue.Value)
                                        {
                                            result = string.Format("{0}选择错误。", extendedCustomDataFieldInfo.LogicalName);
                                            success = false;
                                        }
                                        dataFieldValue = dtValue;
                                        break;

                                    case PhysicalDataFieldType.YearAndMonthAndDay:
                                        dataFieldValue = DateTime.Parse(currentDateTime.ToShortDateString());
                                        break;

                                    case PhysicalDataFieldType.MonthAndDay:
                                        dataFieldValue = DateTime.Parse(string.Format("{0}-{1}-{2}", AppSettingHelper.Year, currentDateTime.Month, currentDateTime.Day));
                                        break;

                                    default:
                                        dataFieldValue = currentDateTime;
                                        break;
                                }
                                break;

                            case PhysicalDataFieldType.Time:
                                DateTime currentTime = Convert.ToDateTime(e.Value);
                                dataFieldValue = DateTime.Parse(string.Format("{0} {1}", AppSettingHelper.YearMonthDay, currentTime.ToLongTimeString()));
                                break;

                            case PhysicalDataFieldType.MultiSelectedEnum:
                            case PhysicalDataFieldType.CscadeEnum:
                                IList<CommonNode> nodes = customDataFieldContract.GetCommonNodesByParentDataFieldId(extendedCustomDataFieldInfo.DataFieldId);
                                foreach (CommonNode node in nodes)
                                {
                                    CommonDataField enumCommonDataField = GetMultiEnumDependencyValue(devExpressGrid, node.NodeId, physicalDataFieldType, relationDataFields);
                                    commonDataFields.Add(enumCommonDataField);
                                }
                                dataFieldValue = stringValue;
                                break;

                            case PhysicalDataFieldType.DropdownListEnum:
                            case PhysicalDataFieldType.DropdownListEnumValue:
                            case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                            case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                            case PhysicalDataFieldType.DepartmentDropdownListEnum:
                            case PhysicalDataFieldType.DepartmentTreeViewEnum:
                            case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                            case PhysicalDataFieldType.TreeViewEnum:
                            case PhysicalDataFieldType.TreeViewEnumValue:
                            case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                            case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                                dataFieldValue = stringValue;
                                IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodesByParentDataFieldId(extendedCustomDataFieldInfo.DataFieldId);
                                foreach (CommonNode node in commonNodes)
                                {
                                    CommonDataField enumCommonDataField = GetEnumDependencyValue(devExpressGrid, node.NodeId, physicalDataFieldType, relationDataFields);
                                    commonDataFields.Add(enumCommonDataField);
                                }
                                break;

                            case PhysicalDataFieldType.Association:
                                BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                                dbType = DataFieldHelper.GetDbType(basedDataType);
                                dataFieldValue = e.Value;
                                break;

                            case PhysicalDataFieldType.PrimaryAssociation:
                                BasedDataType dataType = associatedDataFieldContract.GetBasedDataType(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                                dbType = DataFieldHelper.GetDbType(dataType);
                                dataFieldValue = e.Value;
                                decimal associationId = associatedDataFieldContract.GetAssociationId(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                                IList<CommonNode> associatedNodes = customDataFieldContract.GetCommonNodesByParentDataFieldId(extendedCustomDataFieldInfo.DataFieldId);
                                foreach (CommonNode node in associatedNodes)
                                {
                                    if (node.ParentNodeId == extendedCustomDataFieldInfo.TableId)
                                    {
                                        CommonDataField associatedCommonDataField = GetAssociatedValue(devExpressGrid, node.NodeId, associationId, relationDataFields);
                                        commonDataFields.Add(associatedCommonDataField);
                                    }
                                    else
                                    {
                                        CommonDataField associatedCommonDataField = GetAssociatedValue(devExpressGrid, node.NodeId, associationId, relationDataFields);
                                        relationDataFields.Add(associatedCommonDataField);
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.DocAttachment:
                            case PhysicalDataFieldType.PicAttachment:
                            case PhysicalDataFieldType.PDFAttachment:
                                if (!string.IsNullOrWhiteSpace(stringValue))
                                {
                                    if (Path.IsPathRooted(stringValue))
                                    {
                                        byte[] data = null;
                                        switch (physicalDataFieldType)
                                        {
                                            case PhysicalDataFieldType.DocAttachment:
                                            case PhysicalDataFieldType.PDFAttachment:
                                                if (physicalDataFieldType == PhysicalDataFieldType.DocAttachment)
                                                {
                                                    if (!FileFormatHelper.VerfiyDocFormat(stringValue))
                                                    {
                                                        result = "文件格式只能为：pdf(*.pdf), rar(*.rar, *.zip), doc(*.doc,*.docx), xls(*.xls,*.xlsx) 或者 ppt(*.ppt) 类型。";
                                                        success = false;
                                                    }
                                                }
                                                else
                                                {
                                                    if (!FileFormatHelper.VerfiyPDFFormat(stringValue))
                                                    {
                                                        result = "文件格式只能为：pdf(*.pdf) 类型。";
                                                        success = false;
                                                    }
                                                }
                                                using (FileStream fs = new FileStream(stringValue, FileMode.Open, FileAccess.Read))
                                                {
                                                    BinaryReader r = new BinaryReader(fs);
                                                    data = r.ReadBytes((int)fs.Length);
                                                }
                                                if (data.Length > AppSettingHelper.DefaultDocSize)
                                                {
                                                    result = string.Format("{0}的文件大小不能超过 {1} MB.", extendedCustomDataFieldInfo.LogicalName, AppSettingHelper.DefaultDocSize / (1024 * 1024));
                                                    success = false;
                                                }
                                                break;

                                            case PhysicalDataFieldType.PicAttachment:
                                                if (!FileFormatHelper.VerfiyImageFormat(stringValue))
                                                {
                                                    result = "图片格式只能为：JPEG(*.JPG;*.JPEG)，GIF(*.GIF), PNG(*.PNG) 或者 BMP(*.BMP)。";
                                                    success = false;
                                                }
                                                using (MemoryStream ms = new MemoryStream())
                                                {
                                                    ImageFormat imageFormat = FileFormatHelper.GetImageFormat(stringValue.Substring(stringValue.LastIndexOf('.') + 1).ToUpper());
                                                    Image img = Image.FromFile(stringValue);
                                                    img.Save(ms, imageFormat);
                                                    data = ms.ToArray();
                                                }
                                                if (data.Length > AppSettingHelper.DefaultPictureSize)
                                                {
                                                    result = string.Format("{0}的图片大小不能超过 {1} MB.", extendedCustomDataFieldInfo.LogicalName, AppSettingHelper.DefaultPictureSize / (1024 * 1024));
                                                    success = false;
                                                }
                                                break;
                                        }
                                        if (data.Length == 0)
                                        {
                                            result = "文件大小不能为0。";
                                            success = false;
                                        }
                                        else
                                        {
                                            dataFieldValue = new UpLoadFileInfo(Path.GetFileName(stringValue), string.Empty, data);
                                        }
                                    }
                                }
                                else
                                {
                                    dataFieldValue = new UpLoadFileInfo(string.Empty, string.Empty, null);
                                }
                                break;

                            default:
                                throw new ArgumentException("不支持修改该类型的数据。");
                        }
                    }
                    if (!success)
                    {
                        LoadUserData(devExpressGrid);
                        Cursor = Cursors.Default;
                        MessageBox.Show(result, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CommonDataField commonDataField = new CommonDataField(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.PhysicalName, dataFieldValue, dbType);
                    commonDataFields.Add(commonDataField);
                    bool relationship = AuthorityHelper.CheckAuthority(extendedCustomDataFieldInfo.DataFieldSetting, (byte)DataFieldSetting.Correlation);
                    CommonNode tableNode = devExpressGrid.Tag as CommonNode;
                    DataTableType dataTableType = (DataTableType)tableNode.NodeType;
                    /* 主从表只有主表记录更新会同步更新联动字段 */
                    if (dataTableType == DataTableType.MasterSlaveTable)
                    {
                        DataRow dataRow = devExpressGrid.FocusedDataRow;
                        string currentStateName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState);
                        CurrentState currentState = (CurrentState)Convert.ToByte(dataRow[currentStateName]);
                        if (currentState != CurrentState.Current)
                        {
                            relationship = false;
                        }
                    }
                    if (relationship)
                    {
                        IList<CommonNode> relationCommonNodes = customDataFieldContract.GetRelationDataFields(extendedCustomDataFieldInfo.DataFieldId);
                        foreach (CommonNode relationCommonNode in relationCommonNodes)
                        {
                            relationDataFields.Add(new CommonDataField(relationCommonNode.NodeId, relationCommonNode.NodeCode, commonDataField.DataFieldValue, commonDataField.DataFieldDataType));
                        }
                    }
                    IList<RecordEntity> recordEntities = new List<RecordEntity>();
                    switch (dataFieldEditedState)
                    {
                        case DataFieldEditedState.Edit:
                            recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[recordIdName]);
                            userId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[userIdName]);
                            businessId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[bussinessIdName]);
                            RecordEntity recordEntity = new RecordEntity(commonNode.NodeId, dataTableType, recordId, businessId, businessId > 0);
                            recordEntity.CommonDataFields.AddRange(commonDataFields);
                            if (relationDataFields.Count > 0)
                            {
                                recordEntity.RelaitonDataFields.AddRange(relationDataFields);
                            }
                            recordEntities.Add(recordEntity);
                            dataAuditingContract.Process(userId, recordEntities);
                            /* 当字段数超过1时，更新了依赖值，所以需要重新加载数据 */
                            if (dbType == DbType.Xml || dbType == DbType.Object || commonDataFields.Count > 1)
                            {
                                LoadUserData(devExpressGrid);
                            }
                            break;

                        case DataFieldEditedState.BathcEdit:
                            foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                            {
                                recordEntities.Clear();
                                recordId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                                userId = DataConvertionHelper.GetDecimal(rowEvent.Values[userIdName]);
                                businessId = DataConvertionHelper.GetDecimal(rowEvent.Values[bussinessIdName]);
                                RecordEntity entity = new RecordEntity(commonNode.NodeId, dataTableType, recordId, businessId, businessId > 0);
                                entity.CommonDataFields.AddRange(commonDataFields);
                                if (relationDataFields.Count > 0)
                                {
                                    entity.RelaitonDataFields.AddRange(relationDataFields);
                                }
                                recordEntities.Add(entity);
                                dataAuditingContract.Process(userId, recordEntities);
                            }
                            LoadUserData(devExpressGrid);
                            break;

                        case DataFieldEditedState.CompleteEdit:
                            RecordEntity completedRecordEntity = new RecordEntity(commonNode.NodeId, dataTableType);
                            completedRecordEntity.CommonDataFields.AddRange(commonDataFields);
                            IList<WhereConditon> whereConditons = null;
                            switch (DataAuditingType)
                            {
                                case DataAuditingType.Personal:
                                    whereConditons = GetPersonalWhereConditons(devExpressGrid, userId);
                                    break;

                                case DataAuditingType.Group:
                                    ChangeTableNameInGroupWhereConditon(commonNode);
                                    whereConditons = groupWhereConditons;
                                    break;

                                default:
                                    throw new ArgumentException("不支持该类型。");
                            }
                            if (relationDataFields.Count > 0)
                            {
                                completedRecordEntity.RelaitonDataFields.AddRange(relationDataFields);
                            }
                            dataAuditingContract.Update(completedRecordEntity, whereConditons);
                            LoadUserData(devExpressGrid);
                            break;
                    }
                    Cursor = Cursors.Default;
                    MessageBox.Show("更新成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 获得多选枚举依赖值
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="relationDataFields"></param>
        /// <returns></returns>
        private CommonDataField GetMultiEnumDependencyValue(DevExpressGrid devExpressGrid, decimal dataFieldId, PhysicalDataFieldType physicalDataFieldType, IList<CommonDataField> relationDataFields)
        {
            IList<CommonNode> commonNodes = devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag as IList<CommonNode>;

            return relationDataBusiness.GetMultiEnumDependencyValue(commonNodes, dataFieldId, physicalDataFieldType, relationDataFields);
        }

        /// <summary>
        /// 获得关联的值
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="associationId"></param>
        /// <param name="relationDataFields"></param>
        /// <returns></returns>
        private CommonDataField GetAssociatedValue(DevExpressGrid devExpressGrid, decimal dataFieldId, decimal associationId, IList<CommonDataField> relationDataFields)
        {
            object tag = devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag;
            decimal recordId = decimal.MinValue;
            if (tag != null)
            {
                recordId = DataConvertionHelper.GetConvertedDecimal(tag, decimal.MinValue);
            }
            return relationDataBusiness.GetAssociatedValue(recordId, dataFieldId, associationId, relationDataFields);
        }

        /// <summary>
        /// 获得单选枚举依赖值
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="relationDataFields"></param>
        /// <returns></returns>
        private CommonDataField GetEnumDependencyValue(DevExpressGrid devExpressGrid, decimal dataFieldId, PhysicalDataFieldType physicalDataFieldType, IList<CommonDataField> relationDataFields)
        {
            CommonNode commonNode = devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag as CommonNode;

            return relationDataBusiness.GetEnumDependencyValue(commonNode, dataFieldId, physicalDataFieldType, relationDataFields);
        }

        /// <summary>
        /// 移除表页面
        /// </summary>
        /// <param name="commonNode"></param>
        private void RemoveTabPages(CommonNode commonNode)
        {
            if (selectedTables.Contains(commonNode.NodeId))
            {
                selectedTables.Remove(commonNode.NodeId);
            }

            for (int i = 0; i < xtraTabControl.TabPages.Count; i++)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.TabPages[i].Tag as DevExpressGrid;
                CommonNode node = devExpressGrid.Tag as CommonNode;
                if (commonNode.NodeId == node.NodeId)
                {
                    xtraTabControl.TabPages.RemoveAt(i);
                    break;
                }
            }
        }

        /// <summary>
        /// 加载用户数据
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        private void LoadUserData(DevExpressGrid devExpressGrid, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            bool hasUserAccount = true;
            if (DataAuditingType == DataAuditingType.Personal)
            {
                hasUserAccount = false;
                if (selectedUserId <= 0)
                {
                    devExpressGrid.RecordCount = 0;
                    btnAudit.Enabled = false;
                    btnCancel.Enabled = false;
                    return;
                }
            }
            CommonNode commonNode = devExpressGrid.Tag as CommonNode;
            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = devExpressGrid.Data as IList<ExtendedCustomDataFieldInfo>;
            int focusedRowHandle = devExpressGrid.FocusedRowHandle;
            int totalCount = 0;
            Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
            Int64 systemDataFieldAuthority = customRoleContract.GetSystemDataFieldAuthority(CurrentUser.Instance.UserId, commonNode.NodeId, (byte)DataAuthorityType.Auditing);
            /* 审核状态 */
            systemDataFieldAuthority = AuthorityHelper.GetAuthority(systemDataFieldAuthority, Convert.ToByte(SystemDataField.AuditedStatus), true);


            DataTableType dataTableType = (DataTableType)commonNode.NodeType;
            /* 主从表只有主表记录更新会同步更新联动字段 */
            if (dataTableType == DataTableType.MasterSlaveTable)
            {
                /* 记录状态 */
                systemDataFieldAuthority = AuthorityHelper.GetAuthority(systemDataFieldAuthority, Convert.ToByte(SystemDataField.CurrentState), true);
            }
            Dictionary<string, CommonDataFieldInfo> systemDataFieldNameRelations = DataFieldHelper.GetSystemDataFieldInfo(commonNode.NodeCode, systemDataFieldAuthority);
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in systemDataFieldNameRelations)
            {
                dataFieldNameRelations.Add(keyValue.Key, keyValue.Value);
            }
            foreach (var extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
                string expressionText = string.Empty;
                if (dataFieldProperty == DataFieldProperty.LogicalDataField)
                {
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    if (logicalDataFieldType == LogicalDataFieldType.DigitExpression || logicalDataFieldType == LogicalDataFieldType.StringExpression
                        || logicalDataFieldType == LogicalDataFieldType.DateTimeExpression)
                    {
                        expressionText = customDataFieldContract.GetDataFieldLogicalExpression(extendedCustomDataFieldInfo.DataFieldId);
                    }
                }
                dataFieldNameRelations.Add(extendedCustomDataFieldInfo.PhysicalName,
                        new CommonDataFieldInfo(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.TableId, extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.LogicalName,
                        expressionText, dataFieldProperty, extendedCustomDataFieldInfo.DataFieldType));
            }
            devExpressGrid.DataSource = customTableContract.GetTableData(commonNode.NodeId, systemDataFieldAuthority, hasUserAccount, false, dataFieldNameRelations,
                                        devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, devExpressGrid.PageSize, whereConditons, sortingCondtions, ref totalCount);
            devExpressGrid.RecordCount = totalCount;
            foreach (GridColumn gridColumn in devExpressGrid.Columns)
            {
                if (gridColumn.VisibleIndex < AUDITING_GRID_VIEW_FIXED_COLUMN_COUNT || !gridColumn.Visible)
                {
                    continue;
                }
                if (dataFieldNameRelations.ContainsKey(gridColumn.FieldName))
                {
                    UserControlHelper.SetColumnDisplayText(gridColumn, dataFieldNameRelations[gridColumn.FieldName]);
                }
            }
            if (totalCount == 0)
            {
                btnAudit.Enabled = false;
                btnCancel.Enabled = false;
            }
            else
            {
                devExpressGrid.FocusedRowHandle = focusedRowHandle;
            }
        }

        /// <summary>
        /// 加载表格数据
        /// </summary>
        /// <param name="readOnly"></param>
        private void LoadFormData(DevExpressGrid devExpressGrid, GridViewAuthority gridViewAuthority)
        {
            CommonNode commonNode = devExpressGrid.Tag as CommonNode;
            bool readOnly = false;
            decimal userId = decimal.MinValue;
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            switch (gridViewAuthority)
            {
                case GridViewAuthority.View:
                    if (devExpressGrid.FocusedDataRow == null)
                    {
                        MessageBox.Show("请先选择记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    userId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[userIdName]);
                    readOnly = true;
                    break;

                case GridViewAuthority.Add:
                case GridViewAuthority.Edit:
                    if (gridViewAuthority == GridViewAuthority.Add)
                    {
                        if (selectedUserId <= 0)
                        {
                            MessageBox.Show("请先选择用户。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        userId = selectedUserId;
                    }
                    else
                    {
                        if (devExpressGrid.FocusedDataRow == null)
                        {
                            MessageBox.Show("请先选择记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        userId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[userIdName]);
                        string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                        decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[recordIdName]);
                        AuditedStatus auditedStatus = customTableContract.GetAuditedStatus(commonNode.NodeId, recordId);
                        if (auditedStatus != AuditedStatus.None)
                        {
                            return;
                        }
                    }
                    break;
            }
            Cursor = Cursors.WaitCursor;
            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = (IList<ExtendedCustomDataFieldInfo>)devExpressGrid.Data;
            DataTemplateTableForm frmDataTemplateTable = new DataTemplateTableForm();
            frmDataTemplateTable.Text = commonNode.NodeName;
            frmDataTemplateTable.FormReadOnly = readOnly;
            Int64 systemDataFieldAuthority = customRoleContract.GetSystemDataFieldAuthority(CurrentUser.Instance.UserId, commonNode.NodeId, (byte)DataAuthorityType.Auditing);
            if (userId > 0)
            {
                commonUserInfo = userAccountContract.GetCommonUserInfo(userId);
            }
            DataTableBusiness tableBusiness = new DataTableBusiness(commonUserInfo, readOnly, systemDataFieldAuthority, customTableContract, customDataFieldContract, associatedDataFieldContract,
                customAssociationContract, customEnumContract, customDepartmentContract, frmDataTemplateTable.Panel, frmDataTemplateTable.MemoEditToolTip);
            /* 加载控件 */
            int multiTextBoxCount = 0;
            tableBusiness.CreateControls(extendedCustomDataFieldInfos, tableBusiness.GetFormShowStyleSettings(FormShowStyle.SingleColumnThreeRanksCompleted), ref multiTextBoxCount);
            frmDataTemplateTable.SumbittedHandler = () =>
            {
                RecordSet recordSet = tableBusiness.GetRecordSet();
                if (!recordSet.Success)
                {
                    MessageBox.Show(recordSet.Warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(userId);
                dataAuditingContract.Process(commonUserInfo, recordSet.RecordEntities);
                LoadUserData(devExpressGrid);
                frmDataTemplateTable.Close();
            };
            if ((gridViewAuthority == GridViewAuthority.View || (gridViewAuthority == GridViewAuthority.Edit)) && devExpressGrid.FocusedDataRow != null)
            {
                tableBusiness.LoadDataOnTable(commonNode.NodeId, devExpressGrid.FocusedDataRow, false);
            }
            Cursor = Cursors.WaitCursor;
            frmDataTemplateTable.ShowDialog();
        }

        /// <summary>
        /// 显示图片附件
        /// </summary>
        /// <param name="devExpressGrid"></param>
        private void ShowPicAttachment(DevExpressGrid devExpressGrid)
        {
            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = (IList<ExtendedCustomDataFieldInfo>)devExpressGrid.Data;
            if (devExpressGrid.FocusedColumn == null)
            {
                return;
            }
            int index = extendedCustomDataFieldInfos.FindIndex(dataFieldInfo => dataFieldInfo.PhysicalName.Equals(devExpressGrid.FocusedColumn.FieldName));
            if (index >= 0)
            {
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = extendedCustomDataFieldInfos[index];
                if ((DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                {
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    if (physicalDataFieldType == PhysicalDataFieldType.PicAttachment)
                    {
                        string fileName = DataConvertionHelper.GetString(devExpressGrid.FocusedValue);
                        if (!string.IsNullOrWhiteSpace(fileName))
                        {
                            byte[] data = customDataFieldContract.GetFileData(extendedCustomDataFieldInfo.PhysicalName, fileName);
                            if (data == null || data.Length <= 0)
                            {
                                return;
                            }
                            using (MemoryStream ms = new MemoryStream(data))
                            {
                                pictureEdit.Image = Image.FromStream(ms);
                            }
                            int x = (Cursor.Position.X - pictureEdit.Width) < 0 ? 0 : Cursor.Position.X - pictureEdit.Width;
                            int y = (Cursor.Position.Y - pictureEdit.Height) < 0 ? 0 : Cursor.Position.Y - pictureEdit.Height;
                            Point point = new Point(x, y);
                            pcContainer.BringToFront();
                            pcContainer.ShowPopup(point);
                        }
                        else
                        {
                            pcContainer.HidePopup();
                        }
                    }
                    else
                    {
                        pcContainer.HidePopup();
                    }
                }
                else
                {
                    pcContainer.HidePopup();
                }
            }
        }

        /// <summary>
        /// 查看记录
        /// </summary>
        private void ViewRecord()
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                LoadFormData(devExpressGrid, GridViewAuthority.View);
            }
        }

        /// <summary>
        /// 保存文档
        /// </summary>
        /// <param name="devExpressGrid"></param>
        private void SaveDocAttachment(DevExpressGrid devExpressGrid)
        {
            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = (IList<ExtendedCustomDataFieldInfo>)devExpressGrid.Data;
            if (devExpressGrid.FocusedColumn == null || devExpressGrid.FocusedColumn.VisibleIndex < AUDITING_GRID_VIEW_FIXED_COLUMN_COUNT || !devExpressGrid.FocusedColumn.Visible)
            {
                return;
            }
            int index = extendedCustomDataFieldInfos.FindIndex(dataFieldInfo => dataFieldInfo.PhysicalName.Equals(devExpressGrid.FocusedColumn.FieldName));
            if (index >= 0)
            {
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = extendedCustomDataFieldInfos[index];
                if ((DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                {
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    string fileName = DataConvertionHelper.GetString(devExpressGrid.FocusedValue);
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.DocAttachment:
                            saveAttachmentFileDialog.FileName = DataConvertionHelper.GetString(devExpressGrid.FocusedValue);
                            saveAttachmentFileDialog.Filter = AppSettingHelper.DefaultFileFormat;
                            if (saveAttachmentFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                if (!string.IsNullOrWhiteSpace(fileName))
                                {
                                    byte[] data = customDataFieldContract.GetFileData(extendedCustomDataFieldInfo.PhysicalName, fileName);
                                    if (data == null || data.Length <= 0)
                                    {
                                        MessageBox.Show("文件内容为空，无法保存！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }
                                    using (FileStream fileStream = new FileStream(saveAttachmentFileDialog.FileName, FileMode.OpenOrCreate, FileAccess.Write))
                                    {
                                        fileStream.Write(data, 0, data.Length);
                                        fileStream.Close();
                                    }
                                    MessageBox.Show("保存成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("该文件不存在，无法保存！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            break;

                        case PhysicalDataFieldType.PDFAttachment:
                            Cursor = Cursors.WaitCursor;
                            byte[] pdfData = customDataFieldContract.GetFileData(extendedCustomDataFieldInfo.PhysicalName, fileName);
                            if (pdfData != null && pdfData.Length > 0)
                            {
                                using (MemoryStream ms = new MemoryStream(pdfData))
                                {
                                    frmPDFViewer.LoadDocument(ms);
                                    frmPDFViewer.ShowDialog();
                                }
                            }
                            Cursor = Cursors.Default;
                            break;
                    }
                }
            }
        }    

        /// <summary>
        /// 增加记录
        /// </summary>
        private void Add()
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                LoadFormData(devExpressGrid, GridViewAuthority.Add);
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        private void Delete(DevExpressGrid devExpressGrid)
        {
            try
            {
                string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
                decimal userId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[userIdName]);
                if (userId <= 0)
                {
                    MessageBox.Show("请选择需要删除的记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认删除该记录吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    CommonNode commonNode = devExpressGrid.Tag as CommonNode;
                    string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                    IList<decimal> recordIds = new List<decimal>();
                    decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[recordIdName]);
                    recordIds.Add(recordId);
                    AuditedStatus auditedStatus = customTableContract.GetAuditedStatus(commonNode.NodeId, recordId);
                    if (auditedStatus != AuditedStatus.None)
                    {
                        MessageBox.Show(string.Format("不能删除处于{0}状态的记录。", UserEnumHelper.GetEnumText(auditedStatus)), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Cursor = Cursors.WaitCursor;
                    customTableContract.DeleteRecords(commonNode.NodeId, recordIds);
                    LoadUserData(devExpressGrid);
                    Cursor = Cursors.Default;
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
        /// 批量删除
        /// </summary>
        /// <param name="devExpressGrid"></param>
        private void BatchDelete(DevExpressGrid devExpressGrid)
        {
            try
            {

                if (devExpressGrid.MultiSelectedValues.Count == 0)
                {
                    MessageBox.Show("没有进行批量选择，请先选择!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (MessageBox.Show("确认删除所选择的记录吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    CommonNode commonNode = devExpressGrid.Tag as CommonNode;
                    IList<decimal> recordIds = new List<decimal>(devExpressGrid.MultiSelectedValues.Count);
                    foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                    {
                        decimal recordId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                        AuditedStatus auditedStatus = customTableContract.GetAuditedStatus(commonNode.NodeId, recordId);
                        if (auditedStatus != AuditedStatus.None)
                        {
                            MessageBox.Show(string.Format("不能删除处于{0}状态的记录！", UserEnumHelper.GetEnumText(auditedStatus)), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        recordIds.Add(recordId);
                    }
                    Cursor = Cursors.WaitCursor;
                    customTableContract.DeleteRecords(commonNode.NodeId, recordIds);
                    LoadUserData(devExpressGrid);
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 全部删除
        /// </summary>
        /// <param name="devExpressGrid"></param>
        private void CompleteDelete(DevExpressGrid devExpressGrid)
        {
            try
            {
                CommonNode commonNode = devExpressGrid.Tag as CommonNode;
                switch (DataAuditingType)
                {
                    case DataAuditingType.Personal:
                        if (selectedUserId <= 0)
                        {
                            MessageBox.Show("请先选择用户。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (MessageBox.Show("确认删除当前选择用户的表中所有的记录吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            Cursor = Cursors.WaitCursor;
                            customTableContract.DeleteRecordsByUserId(commonNode.NodeId, selectedUserId, AuditedStatus.None);
                            LoadUserData(devExpressGrid);
                            Cursor = Cursors.Default;
                        }
                        break;

                    case DataAuditingType.Group:
                        if (MessageBox.Show(string.Format("确认删除当前表中所有处于{0}状态的记录吗？", UserEnumHelper.GetEnumText(AuditedStatus.None)), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            ChangeTableNameInGroupWhereConditon(commonNode);
                            customTableContract.DeleteRecords(commonNode.NodeId, groupWhereConditons, AuditedStatus.None);
                        }
                        break;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 审核
        /// </summary>
        /// <param name="auditing">审核为 True，取消审核为 False</param>
        public void Audit(bool auditing)
        {
            try
            {
                if (xtraTabControl.SelectedTabPage != null)
                {
                    DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                    if (devExpressGrid.RecordCount == 0)
                    {
                        MessageBox.Show("数据表格为空格，无法操作。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (devExpressGrid.FocusedDataRow == null)
                    {
                        MessageBox.Show("请先选择记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
                    decimal userId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[userIdName]);
                    CommonNode commonNode = devExpressGrid.Tag as CommonNode;
                    string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                    AuditedStatus targetAuditedStatus = AuditedStatus.None;
                    if (!auditing)
                    {
                        targetAuditedStatus = AuditedStatus.Auditing;
                    }
                    AuditedStatus status = auditing ? AuditedStatus.Auditing : AuditedStatus.None;
                    if (chkBlukOperation.Checked)
                    {
                        IList<decimal> recordIds = new List<decimal>();
                        foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                        {
                            decimal recordId = DataConvertionHelper.GetDecimal(rowEvent.Value);
                            AuditedStatus auditedStatus = customTableContract.GetAuditedStatus(commonNode.NodeId, recordId);
                            if (auditedStatus != targetAuditedStatus)
                            {
                                LoadUserData(devExpressGrid);
                                MessageBox.Show(string.Format("不能{0}处于{1}状态的记录。", auditing ? "审核" : "取消审核", UserEnumHelper.GetEnumText(auditedStatus)), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            recordIds.Add(recordId);
                        }
                        Cursor = Cursors.WaitCursor;
                        customTableContract.Audit(commonNode.NodeId, recordIds, status);
                        LoadUserData(devExpressGrid);
                        Cursor = Cursors.Default;
                    }
                    else
                    {
                        decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[recordIdName]);
                        AuditedStatus auditedStatus = customTableContract.GetAuditedStatus(commonNode.NodeId, recordId);
                        if (auditedStatus != targetAuditedStatus)
                        {
                            LoadUserData(devExpressGrid);
                            MessageBox.Show(string.Format("不能{0}处于{1}状态的记录。", auditing ? "审核" : "取消审核", UserEnumHelper.GetEnumText(auditedStatus)), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        Cursor = Cursors.WaitCursor;
                        customTableContract.Audit(commonNode.NodeId, recordId, status);
                        LoadUserData(devExpressGrid);
                        Cursor = Cursors.Default;
                    }
                    btnAudit.Enabled = !auditing;
                    btnCancel.Enabled = auditing;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 移动记录
        /// </summary>
        /// <param name="movedDriection"></param>
        private void MoveRecord(MovedDriection movedDriection)
        {
            if (xtraTabControl.SelectedTabPage == null)
            {
                return;
            }
            DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
            CommonNode commonNode = devExpressGrid.Tag as CommonNode;
            if (devExpressGrid.FocusedDataRow == null)
            {
                MessageBox.Show("请先选择记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[recordIdName]);
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            decimal userId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[userIdName]);
            customTableContract.MoveRecord(userId, commonNode.NodeId, recordId, movedDriection);
            int focusedRowHandle = devExpressGrid.FocusedRowHandle;
            switch (movedDriection)
            {
                case MovedDriection.Top:
                    devExpressGrid.CurrentPageIndex = 0;
                    focusedRowHandle = 0;
                    break;

                case MovedDriection.Previous:
                    if (devExpressGrid.FocusedRowHandle == 0 && devExpressGrid.CurrentPageIndex > 0)
                    {
                        devExpressGrid.CurrentPageIndex -= 1;
                        focusedRowHandle = devExpressGrid.PageSize - 1;
                    }
                    else
                    {
                        focusedRowHandle = devExpressGrid.FocusedRowHandle - 1;
                    }
                    break;

                case MovedDriection.Next:
                    if ((devExpressGrid.FocusedRowHandle == devExpressGrid.CurrentPageSize - 1)
                        && devExpressGrid.CurrentPageIndex < (devExpressGrid.PageCount - 1))
                    {
                        devExpressGrid.CurrentPageIndex += 1;
                        focusedRowHandle = 0;

                    }
                    else
                    {
                        focusedRowHandle = devExpressGrid.FocusedRowHandle + 1;
                    }
                    break;

                case MovedDriection.Bottom:
                    devExpressGrid.CurrentPageIndex = devExpressGrid.PageCount - 1;
                    focusedRowHandle = devExpressGrid.CurrentPageSize - 1;
                    break;
            }
            LoadUserData(devExpressGrid);
            devExpressGrid.FocusedRowHandle = focusedRowHandle;
        }

        /// <summary>
        /// 设置审核按钮
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="commonNode"></param>
        private void SetAuditingButtons(DevExpressGrid devExpressGrid, CommonNode commonNode)
        {
            if (devExpressGrid.FocusedRowHandle < 0)
            {
                btnAudit.Enabled = false;
                btnCancel.Enabled = false;
                return;
            }
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[recordIdName]);
            AuditedStatus auditedStatus = customTableContract.GetAuditedStatus(commonNode.NodeId, recordId);
            switch (auditedStatus)
            {
                case AuditedStatus.None:
                    btnAudit.Enabled = true;
                    btnCancel.Enabled = false;
                    break;

                case AuditedStatus.Auditing:
                    btnAudit.Enabled = false;
                    btnCancel.Enabled = true;
                    break;

                case AuditedStatus.Audited:
                    btnAudit.Enabled = false;
                    btnCancel.Enabled = false;
                    break;
            }
        }

        /// <summary>
        /// 设置当前选择列是否可以编辑
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="allowedEdit"></param>
        private void SetFocusedColumnEditable(DevExpressGrid devExpressGrid, bool allowedEdit)
        {
            if (devExpressGrid.FocusedRowHandle >= 0 && devExpressGrid.FocusedColumn != null
                && !devExpressGrid.FocusedColumn.Name.Equals(devExpressGrid.CheckboxColumnName))
            {
                devExpressGrid.FocusedColumn.OptionsColumn.AllowEdit = allowedEdit;
                devExpressGrid.FocusedColumn.OptionsColumn.ReadOnly = !allowedEdit;
            }
        }

        /// <summary>
        /// 设置物理类型字段的编辑内容
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="customDataFieldInfo"></param>
        private void SetColumnEdit(DevExpressGrid devExpressGrid, ExtendedCustomDataFieldInfo customDataFieldInfo)
        {
            if (devExpressGrid.FocusedColumn == null || devExpressGrid.FocusedColumn.ColumnEdit != null)
            {
                return;
            }

            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
            int width = DataFieldHelper.GetControlWidth(physicalDataFieldType);
            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                case PhysicalDataFieldType.YearAndMonthAndDay:
                case PhysicalDataFieldType.YearAndMonth:
                case PhysicalDataFieldType.MonthAndDay:
                    RepositoryItemDateEdit repositoryItemDateEdit = new RepositoryItemDateEdit();
                    repositoryItemDateEdit.LookAndFeel.SkinName = "Money Twins";
                    repositoryItemDateEdit.LookAndFeel.UseDefaultLookAndFeel = false;
                    repositoryItemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    repositoryItemDateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                            repositoryItemDateEdit.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
                            repositoryItemDateEdit.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                            repositoryItemDateEdit.DisplayFormat.FormatString = "G";
                            repositoryItemDateEdit.EditFormat.FormatString = "G";
                            repositoryItemDateEdit.EditMask = "G";
                            break;

                        case PhysicalDataFieldType.YearAndMonthAndDay:
                            repositoryItemDateEdit.DisplayFormat.FormatString = "d";
                            repositoryItemDateEdit.EditFormat.FormatString = "d";
                            repositoryItemDateEdit.EditMask = "d";
                            break;

                        case PhysicalDataFieldType.YearAndMonth:
                            repositoryItemDateEdit.DisplayFormat.FormatString = "y";
                            repositoryItemDateEdit.EditFormat.FormatString = "y";
                            repositoryItemDateEdit.EditMask = "y";
                            break;

                        case PhysicalDataFieldType.MonthAndDay:
                            repositoryItemDateEdit.DisplayFormat.FormatString = "m";
                            repositoryItemDateEdit.EditFormat.FormatString = "m";
                            repositoryItemDateEdit.EditMask = "m";
                            break;
                    }
                    devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemDateEdit;
                    break;

                case PhysicalDataFieldType.Time:
                    RepositoryItemTimeEdit repositoryItemTimeEdit = new RepositoryItemTimeEdit();
                    repositoryItemTimeEdit.Mask.EditMask = "HH:mm:ss";
                    repositoryItemTimeEdit.LookAndFeel.SkinName = "Money Twins";
                    repositoryItemTimeEdit.LookAndFeel.UseDefaultLookAndFeel = false;
                    repositoryItemTimeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemTimeEdit;
                    break;

                case PhysicalDataFieldType.Boolean:
                    RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit();
                    repositoryItemCheckEdit.LookAndFeel.SkinName = "Money Twins";
                    repositoryItemCheckEdit.LookAndFeel.UseDefaultLookAndFeel = false;
                    devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemCheckEdit;
                    break;

                case PhysicalDataFieldType.DropdownListEnum:
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.DepartmentDropdownListEnum:
                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                    RepositoryItemComboBox repositoryItemComboBox = new RepositoryItemComboBox();
                    repositoryItemComboBox.LookAndFeel.SkinName = "Money Twins";
                    repositoryItemComboBox.LookAndFeel.UseDefaultLookAndFeel = false;
                    repositoryItemComboBox.Tag = customDataFieldInfo;
                    repositoryItemComboBox.SelectedIndexChanged += (sdr, arg) =>
                    {
                        ComboBoxEdit comboBoxEdit = sdr as ComboBoxEdit;
                        CommonNode commonNode = comboBoxEdit.SelectedItem as CommonNode;
                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = commonNode;
                        if (physicalDataFieldType == PhysicalDataFieldType.DropdownListEnum || physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                        {
                            devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, commonNode.NodeName);
                        }
                        else
                        {
                            object obj = customEnumContract.GetEnumData(commonNode.NodeId, physicalDataFieldType);
                            devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, obj);
                        }
                    };
                    IList<CommonNode> enumCommonNodes = null;
                    if (physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                    {
                        enumCommonNodes = customDepartmentContract.GetChildNodes(decimal.One);
                    }
                    else
                    {
                        enumCommonNodes = customEnumContract.GetChildNodes(customDataFieldInfo.EnumId);
                    }
                    repositoryItemComboBox.Items.AddRange(enumCommonNodes.ToArray());
                    devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemComboBox;
                    break;

                case PhysicalDataFieldType.CscadeEnum:
                    RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
                    buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                    buttonEdit.Tag = customDataFieldInfo;
                    buttonEdit.ButtonPressed += (sender, e) =>
                    {
                        int level = customEnumContract.GetMaxLevel(customDataFieldInfo.EnumId);
                        CscadeEnumForm frmCscadeEnumForm = new CscadeEnumForm()
                        {
                            CustomEnumContract = customEnumContract,
                            EnumId = customDataFieldInfo.EnumId,
                            Level = level,
                            SelectedText = string.Empty
                        };
                        frmCscadeEnumForm.CscadeNodeSelected = (commonNodes) =>
                        {
                            if (commonNodes != null && commonNodes.Count > 0)
                            {
                                devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = commonNodes;
                                devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, UserControlHelper.GetCleanFormattedName(commonNodes));
                            }
                        };
                        frmCscadeEnumForm.ShowDialog();
                    };
                    devExpressGrid.FocusedColumn.ColumnEdit = buttonEdit;
                    break;

                case PhysicalDataFieldType.Association:
                    decimal associatedId = associatedDataFieldContract.GetAssociationId(customDataFieldInfo.AssociatedDataFieldId);
                    CustomAssociationInfo associationInfo = customAssociationContract.GetModelInfo(associatedId);
                    string content = string.Empty;
                    SearchLookUpEditWithGrid searchLookUpEditWithGrid = new SearchLookUpEditWithGrid();
                    searchLookUpEditWithGrid.Width = width;
                    searchLookUpEditWithGrid.ValueMember = associatedDataFieldContract.GetPhysicalName(customDataFieldInfo.AssociatedDataFieldId);
                    searchLookUpEditWithGrid.TextEditStyle = TextEditStyles.Standard;
                    searchLookUpEditWithGrid.Tag = customDataFieldInfo;
                    searchLookUpEditWithGrid.OnGridBeforePopup += (sdr, arg) =>
                    {
                        if (searchLookUpEditWithGrid.DataSource == null)
                        {
                            LoadAssociatedData(searchLookUpEditWithGrid, associationInfo.AssociationId, string.Empty);
                        }
                    };
                    searchLookUpEditWithGrid.ValueSearched += (sdr, arg) =>
                    {
                        LoadAssociatedData(searchLookUpEditWithGrid, associationInfo.AssociationId, arg.Content);
                        content = arg.Content;
                    };
                    searchLookUpEditWithGrid.OnPageIndexChanged += (sdr, arg) =>
                    {
                        searchLookUpEditWithGrid.CurrentPageIndex = arg.NewPageIndex;
                        LoadAssociatedData(searchLookUpEditWithGrid, associationInfo.AssociationId, content);
                    };
                    searchLookUpEditWithGrid.EditValueChanged += (sdr, arg) =>
                    {
                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, searchLookUpEditWithGrid.EditValue);
                    };
                    devExpressGrid.FocusedColumn.ColumnEdit = GetRepositoryItemPopupContainerEdit(searchLookUpEditWithGrid);
                    break;

                case PhysicalDataFieldType.PrimaryAssociation:
                    decimal associationId = associatedDataFieldContract.GetAssociationId(customDataFieldInfo.AssociatedDataFieldId);
                    CustomAssociationInfo customAssociationInfo = customAssociationContract.GetModelInfo(associationId);
                    if (customAssociationInfo.SuperAssociationEnabled)
                    {
                        RepositoryItemButtonEdit repositoryItemButtonEdit = new RepositoryItemButtonEdit();
                        repositoryItemButtonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                        repositoryItemButtonEdit.Tag = customDataFieldInfo;
                        repositoryItemButtonEdit.ButtonPressed += (sender, e) =>
                        {
                            AssociatedDataForm frmAssociatedData = new AssociatedDataForm()
                            {
                                AssociationId = customAssociationInfo.AssociationId
                            };

                            frmAssociatedData.DataRowConfrimed = (dataRow) =>
                            {
                                string dataFieldPhysicalName = frmAssociatedData.GetDataFieldPhysicalName(customDataFieldInfo.AssociatedDataFieldId);
                                if (!string.IsNullOrWhiteSpace(dataFieldPhysicalName))
                                {
                                    if (dataRow != null)
                                    {
                                        string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = dataRow[recordIdName];
                                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, dataRow[dataFieldPhysicalName]);
                                    }
                                    else
                                    {
                                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = null;
                                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, null);
                                    }
                                }
                            };
                            frmAssociatedData.ShowDialog();
                        };
                        devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemButtonEdit;
                    }
                    else
                    {
                        AssociationShowMode associationShowMode = (AssociationShowMode)customAssociationInfo.ShowMode;
                        string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                        string key2 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordSorting);
                        switch (associationShowMode)
                        {
                            case AssociationShowMode.Hierarchicy:
                                LookUpEditWithGrid lookUpEditWithGrid = new LookUpEditWithGrid();
                                lookUpEditWithGrid.Width = width;
                                lookUpEditWithGrid.Tag = customDataFieldInfo;
                                lookUpEditWithGrid.ValueMember = associatedDataFieldContract.GetPhysicalName(customDataFieldInfo.AssociatedDataFieldId);
                                lookUpEditWithGrid.ShowSearch = true;
                                lookUpEditWithGrid.EditValueCleaned += (sender, e) =>
                                {
                                    devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = null;
                                    devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, null);
                                };
                                lookUpEditWithGrid.OnGridBeforePopup += (sender, e) =>
                                {
                                    if (lookUpEditWithGrid.DataSource == null)
                                    {
                                        lookUpEditWithGrid.DataKeyNames = new string[] { key, key2 };
                                        lookUpEditWithGrid.SortedKeyName = key2;
                                        IList<string> groupKeyNames = new List<string>();
                                        IList<AssociatedDataFieldInfo> associatedDataFieldInfos = associatedDataFieldContract.GetModelInfos(customAssociationInfo.AssociationId);
                                        foreach (AssociatedDataFieldInfo associatedDataFieldInfo in associatedDataFieldInfos)
                                        {
                                            if (associatedDataFieldInfo.IsHierarchal)
                                            {
                                                groupKeyNames.Add(associatedDataFieldInfo.PhysicalName);
                                            }
                                        }
                                        if (groupKeyNames.Count > 0)
                                        {
                                            lookUpEditWithGrid.GroupKeyNames = groupKeyNames.ToArray();
                                        }
                                        lookUpEditWithGrid.DataSource = customAssociationContract.GetAssociationDataWithSortingDataField(customAssociationInfo.AssociationId);
                                    }
                                };
                                lookUpEditWithGrid.OnRowDoubleClick += (sender, e) =>
                                {
                                    if (lookUpEditWithGrid.FocusedDataRow != null)
                                    {
                                        string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = lookUpEditWithGrid.FocusedDataRow[recordIdName];
                                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, lookUpEditWithGrid.EditValue);
                                    }
                                };
                                devExpressGrid.FocusedColumn.ColumnEdit = GetRepositoryItemPopupContainerEdit(lookUpEditWithGrid);
                                break;

                            case AssociationShowMode.Table:
                                string physicalName = associatedDataFieldContract.GetPhysicalName(customDataFieldInfo.AssociatedDataFieldId);
                                DataTable data = relationDataBusiness.GetAssociationData(customAssociationInfo.AssociationId);
                                RepositoryItemGridLookUpEdit lookUpEdit = new RepositoryItemGridLookUpEdit();
                                lookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                                lookUpEdit.Tag = customDataFieldInfo;
                                lookUpEdit.NullText = null;
                                lookUpEdit.PopupSizeable = false;
                                lookUpEdit.ShowFooter = true;
                                lookUpEdit.DisplayMember = physicalName;
                                lookUpEdit.ValueMember = physicalName;
                                lookUpEdit.DataSource = data;
                                lookUpEdit.View.PopulateColumns(data);
                                lookUpEdit.View.Columns[key].Visible = false;
                                foreach (GridColumn column in lookUpEdit.View.Columns)
                                {
                                    if (column.ColumnType == typeof(DateTime))
                                    {
                                        column.DisplayFormat.FormatType = FormatType.Custom;
                                        column.DisplayFormat.FormatString = "yyyy-MM-dd";
                                    }
                                }
                                lookUpEdit.EditValueChanging += (sdr, arg) =>
                                {
                                    DataRowView dataRow = lookUpEdit.GetRowByKeyValue(arg.NewValue) as DataRowView;
                                    if (dataRow != null)
                                    {
                                        string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = dataRow[recordIdName];
                                    }
                                    else
                                    {
                                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = null;
                                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, null);
                                    }
                                };
                                devExpressGrid.FocusedColumn.ColumnEdit = lookUpEdit;
                                break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.TreeViewEnumValue:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    bool superEnumEnabled = customEnumContract.GetSuperEnumEnabled(customDataFieldInfo.EnumId);
                    if (superEnumEnabled)
                    {
                        RepositoryItemButtonEdit repositoryItemButtonEdit = new RepositoryItemButtonEdit();
                        repositoryItemButtonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                        repositoryItemButtonEdit.Tag = customDataFieldInfo;
                        repositoryItemButtonEdit.ButtonPressed += (sender, e) =>
                        {
                            TreeSelectedItemsForm frmTreeSelectedItems = new TreeSelectedItemsForm()
                            {
                                Text = customDataFieldInfo.LogicalName,
                                CommonNodeContract = customEnumContract,
                                ParentNodeId = customDataFieldInfo.EnumId,
                                OnlySelectedLeaf = true,
                                ShowSearch = true
                            };
                            frmTreeSelectedItems.NodeSelected = (node) =>
                            {
                                devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = node;
                                if (physicalDataFieldType == PhysicalDataFieldType.TreeViewEnum || physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                                {
                                    devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, node.NodeName);
                                }
                                else
                                {
                                    object obj = customEnumContract.GetEnumData(node.NodeId, physicalDataFieldType);
                                    devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, obj);
                                }
                            };
                            frmTreeSelectedItems.NodeRemoved = () =>
                            {
                                devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = null;
                                devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, string.Empty);
                            };
                            frmTreeSelectedItems.ShowDialog();
                        };
                        devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemButtonEdit;
                    }
                    else
                    {
                        TreeDropdownList treeDropdownList = new TreeDropdownList();
                        treeDropdownList.SkinName = "Blue";
                        treeDropdownList.Width = width;
                        treeDropdownList.OnlySelectedLeaf = true;
                        treeDropdownList.Tag = customDataFieldInfo;
                        treeDropdownList.TreeDropdownHandler = new TreeDropdownItems(customEnumContract, customDataFieldInfo.EnumId);
                        treeDropdownList.Value = DataConvertionHelper.GetString(devExpressGrid.FocusedValue);
                        treeDropdownList.BeforeControlPopup += (sender, e) =>
                        {
                            if (treeDropdownList.TreeView.Nodes.Count == 0)
                            {
                                treeDropdownList.TreeDropdownHandler = new TreeDropdownItems(customEnumContract, customDataFieldInfo.EnumId);
                                treeDropdownList.InitalizeTreeView();
                            }
                        };
                        treeDropdownList.AfterTreeNodeSelect += (sender, e) =>
                        {
                            CommonNode commonNode = treeDropdownList.SelectedNode;
                            devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = commonNode;
                            if (physicalDataFieldType == PhysicalDataFieldType.TreeViewEnum || physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                            {
                                devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, commonNode.NodeName);
                            }
                            else
                            {
                                object obj = customEnumContract.GetEnumData(commonNode.NodeId, physicalDataFieldType);
                                devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, obj);
                            }
                        };
                        treeDropdownList.OnNodeRemoved += (sender, e) =>
                        {
                            devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = null;
                            devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, string.Empty);
                        };
                        devExpressGrid.FocusedColumn.ColumnEdit = GetRepositoryItemPopupContainerEdit(treeDropdownList);
                    }
                    break;

                case PhysicalDataFieldType.DepartmentTreeViewEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                    TreeDropdownList ddlDepartmentTreeView = new TreeDropdownList();
                    ddlDepartmentTreeView.SkinName = "Blue";
                    ddlDepartmentTreeView.Width = width;
                    ddlDepartmentTreeView.OnlySelectedLeaf = false;
                    ddlDepartmentTreeView.Tag = customDataFieldInfo;
                    ddlDepartmentTreeView.BeforeControlPopup += (sender, e) =>
                    {
                        if (ddlDepartmentTreeView.TreeView.Nodes.Count == 0)
                        {
                            if (physicalDataFieldType == PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot)
                            {
                                ddlDepartmentTreeView.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
                            }
                            else
                            {
                                ddlDepartmentTreeView.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract, decimal.One);
                            }
                            if (ddlDepartmentTreeView.Value != null)
                            {
                                string value = DataConvertionHelper.GetString(ddlDepartmentTreeView.Value);
                                if (!string.IsNullOrWhiteSpace(value))
                                {
                                    CommonItemList<decimal, CommonNode> commonItems = null;
                                    if (physicalDataFieldType == PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot)
                                    {
                                        commonItems = customDepartmentContract.GetTreeviewCommonNodesWithRoot(value);
                                        ddlDepartmentTreeView.LoadMatchedNodes(commonItems.CommonList);
                                    }
                                    else
                                    {
                                        commonItems = customDepartmentContract.GetTreeviewCommonNodes(value);
                                        ddlDepartmentTreeView.LoadMatchedNodes(decimal.One, commonItems.CommonList);
                                    }
                                    if (commonItems.Value > 0)
                                    {
                                        ddlDepartmentTreeView.SelectedNode = new CommonNode(commonItems.Value, commonItems.Text);
                                    }
                                    else
                                    {
                                        ddlDepartmentTreeView.SelectedNode = null;
                                    }
                                }
                            }
                            else
                            {
                                ddlDepartmentTreeView.InitalizeTreeView();
                            }
                        }
                    };
                    ddlDepartmentTreeView.AfterTreeNodeSelect += (sender, e) =>
                    {
                        CommonNode commonNode = ddlDepartmentTreeView.SelectedNode;
                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = commonNode;
                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, commonNode.NodeName);
                    };
                    devExpressGrid.FocusedColumn.ColumnEdit = GetRepositoryItemPopupContainerEdit(ddlDepartmentTreeView);
                    break;

                case PhysicalDataFieldType.MultiSelectedEnum:
                    RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit = new RepositoryItemCheckedComboBoxEdit();
                    repositoryItemCheckedComboBoxEdit.LookAndFeel.SkinName = "Money Twins";
                    repositoryItemCheckedComboBoxEdit.LookAndFeel.UseDefaultLookAndFeel = false;
                    repositoryItemCheckedComboBoxEdit.SelectAllItemVisible = false;
                    repositoryItemCheckedComboBoxEdit.ShowButtons = false;
                    repositoryItemCheckedComboBoxEdit.PopupSizeable = false;
                    IList<CommonNode> multiEnumCommonNodes = customEnumContract.GetChildNodes(customDataFieldInfo.EnumId);
                    repositoryItemCheckedComboBoxEdit.Items.AddRange(multiEnumCommonNodes.ToArray());
                    repositoryItemCheckedComboBoxEdit.EditValueChanged += (sdr, arg) =>
                    {
                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = multiEnumCommonNodes;
                    };
                    devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemCheckedComboBoxEdit;
                    break;

                case PhysicalDataFieldType.DocAttachment:
                case PhysicalDataFieldType.PicAttachment:
                case PhysicalDataFieldType.PDFAttachment:
                    DevExpressUpdatedUploadFile devExpressUploadFile = new DevExpressUpdatedUploadFile();
                    devExpressUploadFile.Width = width;
                    if (physicalDataFieldType == PhysicalDataFieldType.DocAttachment)
                    {
                        devExpressUploadFile.DocType = DocType.DocAttachment;
                        devExpressUploadFile.Filter = AppSettingHelper.DefaultDocFormat;
                    }
                    else if (physicalDataFieldType == PhysicalDataFieldType.PicAttachment)
                    {
                        devExpressUploadFile.DocType = DocType.PicAttachment;
                        devExpressUploadFile.Filter = AppSettingHelper.DefaultPictureFormat;
                    }
                    else
                    {
                        devExpressUploadFile.DocType = DocType.PDFAttachment;
                        devExpressUploadFile.Filter = AppSettingHelper.DefaultPDFFormat;
                    }
                    devExpressUploadFile.OnViewLinkClicked += (sender, e) =>
                    {
                        if ((e.UserAction != UserAction.Delete) && (devExpressUploadFile.CustomData == null || devExpressUploadFile.CustomData.Length == 0)
                                && !string.IsNullOrWhiteSpace(devExpressUploadFile.FileName))
                        {
                            byte[] data = customDataFieldContract.GetFileData(devExpressGrid.FocusedColumn.FieldName, devExpressUploadFile.FileName);
                            devExpressUploadFile.CustomData = data;
                        }
                    };
                    devExpressUploadFile.SkinName = "Blue";
                    devExpressUploadFile.OnTextChanged += (sdr, arg) =>
                    {
                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, devExpressUploadFile.FileName);
                    };
                    RepositoryItemPopupContainerEdit popupContainerEdit = GetRepositoryItemPopupContainerEdit(devExpressGrid, devExpressUploadFile);
                    popupContainerEdit.BeforePopup += (sender, e) =>
                    {
                        string fileName = DataConvertionHelper.GetString(devExpressGrid.FocusedValue);
                        devExpressUploadFile.TextContent = fileName;
                        devExpressUploadFile.CustomData = null;
                    };
                    devExpressGrid.FocusedColumn.ColumnEdit = popupContainerEdit;
                    break;
            }
        }

        /// <summary>
        /// 加载关联数据
        /// </summary>
        /// <param name="searchLookUpEditWithGrid"></param>
        /// <param name="associationId"></param>
        /// <param name="content"></param>
        private void LoadAssociatedData(SearchLookUpEditWithGrid searchLookUpEditWithGrid, decimal associationId, string content)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            if (!string.IsNullOrWhiteSpace(content))
            {
                IList<AssociatedDataFieldInfo> associatedDataFieldInfos = associatedDataFieldContract.GetModelInfos(associationId);
                foreach (AssociatedDataFieldInfo associatedDataFieldInfo in associatedDataFieldInfos)
                {
                    BasedDataType basedDataType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                    if (basedDataType == BasedDataType.String)
                    {
                        whereConditons.Add(new WhereConditon(associatedDataFieldInfo.PhysicalName, associatedDataFieldInfo.PhysicalName, DbType.String, string.Format("%{0}%", content),
                          DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    }
                }
            }
            int totalCount = 0;
            searchLookUpEditWithGrid.DataSource = customAssociationContract.GetAssociationData(associationId, searchLookUpEditWithGrid.PageSize * searchLookUpEditWithGrid.CurrentPageIndex,
                searchLookUpEditWithGrid.PageSize, whereConditons, ref totalCount).Tables[0];
            searchLookUpEditWithGrid.RecordCount = totalCount;
            string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            searchLookUpEditWithGrid.Columns[key].Visible = false;
        }

        /// <summary>
        /// 获得弹出框界面
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="devExpressUploadFile"></param>
        /// <returns></returns>
        private RepositoryItemPopupContainerEdit GetRepositoryItemPopupContainerEdit(DevExpressGrid devExpressGrid, DevExpressUpdatedUploadFile devExpressUploadFile)
        {
            PopupContainerControl containerControl = new PopupContainerControl();
            containerControl.Width = devExpressUploadFile.Width;
            containerControl.Height = devExpressUploadFile.Height;
            containerControl.Controls.Add(devExpressUploadFile);

            RepositoryItemPopupContainerEdit popupContainerEdit = new RepositoryItemPopupContainerEdit();
            popupContainerEdit.AutoHeight = false;
            popupContainerEdit.PopupSizeable = false;
            popupContainerEdit.ShowPopupCloseButton = false;
            popupContainerEdit.PopupControl = containerControl;
            popupContainerEdit.LookAndFeel.SkinName = "Money Twins";
            popupContainerEdit.LookAndFeel.UseDefaultLookAndFeel = false;
            
            popupContainerEdit.BeforePopup += (sender, e) =>
            {
                string fileName = DataConvertionHelper.GetString(devExpressGrid.FocusedValue);
                devExpressUploadFile.TextContent = fileName;
            };            

            return popupContainerEdit;
        }
        
        /// <summary>
        /// 获得弹出框界面
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private RepositoryItemPopupContainerEdit GetRepositoryItemPopupContainerEdit(Control control)
        {
            PopupContainerControl containerControl = new PopupContainerControl();
            containerControl.Width = control.Width;
            containerControl.Height = control.Height;
            containerControl.Controls.Add(control);

            RepositoryItemPopupContainerEdit popupContainerEdit = new RepositoryItemPopupContainerEdit();
            popupContainerEdit.AutoHeight = false;
            popupContainerEdit.PopupSizeable = false;
            popupContainerEdit.ShowPopupCloseButton = false;
            popupContainerEdit.PopupControl = containerControl;
            popupContainerEdit.LookAndFeel.SkinName = "Money Twins";
            popupContainerEdit.LookAndFeel.UseDefaultLookAndFeel = false;
            
            return popupContainerEdit;
        }


        #endregion

        #region 表格处理方法

        #region 增加操作

        /// <summary>
        /// 增加记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnAddClick(object sender, ItemClickEventArgs e)
        {
            Add();
        }

        #endregion

        #region 编辑操作

        /// <summary>
        /// 编辑记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnEditClick(object sender, ItemClickEventArgs e)
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                SetFocusedColumnEditable(devExpressGrid, true);
                dataFieldEditedState = DataFieldEditedState.Edit;
            }
        }

        /// <summary>
        /// 批量编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevExpressGrid_OnBatchEditClick(object sender, ItemClickEventArgs e)
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                SetFocusedColumnEditable(devExpressGrid, true);
                dataFieldEditedState = DataFieldEditedState.BathcEdit;
            }
        }

        /// <summary>
        /// 完全编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevExpressGrid_OnCompleteEditClick(object sender, ItemClickEventArgs e)
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                SetFocusedColumnEditable(devExpressGrid, true);
                dataFieldEditedState = DataFieldEditedState.CompleteEdit;
            }
        }

        /// <summary>
        /// 编辑记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevExpressGrid_OnRowEdit(object sender, RowEvent e)
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
                LoadFormData(devExpressGrid, GridViewAuthority.Edit);
            }
        }

        #endregion

        #region 删除操作

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnDeleteClick(object sender, ItemClickEventArgs e)
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
                Delete(devExpressGrid);
            }
        }

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnBatchDeleteClick(object sender, ItemClickEventArgs e)
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
                BatchDelete(devExpressGrid);
            }
        }

        /// <summary>
        /// 全部删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnCompleteDeleteClick(object sender, ItemClickEventArgs e)
        {
            if (xtraTabControl.SelectedTabPage != null)
            {
                DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
                CompleteDelete(devExpressGrid);
            }
        }

        #endregion

        /// <summary>
        /// 行样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevExpressGrid_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
            string auditedStatusName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
            if (e.RowHandle >= 0)
            {
                AuditedStatus auditedStatus = AuditedStatus.None;
                if (e.Column.FieldName.Equals(auditedStatusName))
                {
                    auditedStatus = (AuditedStatus)Convert.ToByte(e.CellValue);
                    switch (auditedStatus)
                    {
                        case AuditedStatus.Auditing:
                            e.Appearance.ForeColor = Color.DarkGray;
                            break;

                        case AuditedStatus.Audited:
                            e.Appearance.ForeColor = Color.LightGray;
                            break;
                    }

                }
                else if (e.Column.FieldName.Equals(devExpressGrid.CheckboxColumnName) && devExpressGrid.Columns.ColumnByFieldName(auditedStatusName) != null)
                {
                    auditedStatus = (AuditedStatus)Convert.ToByte(devExpressGrid.GetRowCellValue(e.RowHandle, auditedStatusName));
                    switch (auditedStatus)
                    {
                        case AuditedStatus.Auditing:
                            e.Appearance.BackColor = Color.DarkGray;
                            break;

                        case AuditedStatus.Audited:
                            e.Appearance.BackColor = Color.LightGray;
                            break;
                    }
                }
                else if (e.Column.Name.Equals(devExpressGrid.CustomEditedName) && devExpressGrid.Columns.ColumnByFieldName(auditedStatusName) != null)
                {
                    auditedStatus = (AuditedStatus)Convert.ToByte(devExpressGrid.GetRowCellValue(e.RowHandle, auditedStatusName));
                    switch (auditedStatus)
                    {
                        case AuditedStatus.Auditing:
                            e.Appearance.BackColor = Color.DarkGray;
                            break;

                        case AuditedStatus.Audited:
                            e.Appearance.BackColor = Color.LightGray;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 设置记录排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnRecordSortingChanged(object sender, ExtendedItemClickEventArgs e)
        {
            MoveRecord(e.MovedDriection);
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnRefresh(object sender, ItemClickEventArgs e)
        {
            DevExpressGrid devExpressGrid = xtraTabControl.SelectedTabPage.Tag as DevExpressGrid;
            CommonNode commonNode = devExpressGrid.Tag as CommonNode;
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            if (devExpressGrid.DataKeyValues != null && devExpressGrid.DataKeyValues.Values.Contains(userIdName))
            {
                decimal userId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[userIdName]);
                customTableContract.UpdateRecordSortingByUserId(userId, commonNode.NodeId);
                LoadUserData(devExpressGrid);
            }
        }

        /// <summary>
        /// 单击行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevExpressGrid_OnRowClick(object sender, RowEvent e)
        {
            DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
            CommonNode commonNode = devExpressGrid.Tag as CommonNode;
            SetAuditingButtons(devExpressGrid, commonNode);
        }

        /// <summary>
        /// 双击行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnRowDoubleClick(object sender, RowEvent e)
        {
            DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
            SaveDocAttachment(devExpressGrid);
        }        

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, CustomGridViewPageEventArgs e)
        {
            DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            LoadUserData(devExpressGrid);
        }

        /// <summary>
        /// 行变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
            CommonNode commonNode = devExpressGrid.Tag as CommonNode;
            ShowPicAttachment(devExpressGrid);
            SetFocusedColumnEditable(devExpressGrid, false);
            SetAuditingButtons(devExpressGrid, commonNode);
        }

        /// <summary>
        /// 列变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnFocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
            if (e.PrevFocusedColumn != null && !e.PrevFocusedColumn.Name.Equals(devExpressGrid.CheckboxColumnName))
            {
                e.PrevFocusedColumn.OptionsColumn.AllowEdit = false;
                e.PrevFocusedColumn.OptionsColumn.ReadOnly = true;
            }
            ShowPicAttachment(devExpressGrid);
        }

        /// <summary>
        /// 菜单弹出前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevExpressGrid_OnBeforePopupMenu(object sender, CancelEventArgs e)
        {
            DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
            if (devExpressGrid.RecordCount == 0 || devExpressGrid.FocusedDataRow == null)
            {
                devExpressGrid.SetMenuButtons(false);
            }
            else
            {
                CommonNode commonNode = devExpressGrid.Tag as CommonNode;
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[recordIdName]);
                AuditedStatus auditedStatus = customTableContract.GetAuditedStatus(commonNode.NodeId, recordId);
                if (auditedStatus != AuditedStatus.None)
                {
                    devExpressGrid.SetMenuButtons(false);
                }
                else
                {
                    devExpressGrid.SetMenuButtons(true);
                    if ((devExpressGrid.FocusedColumn != null) && (devExpressGrid.FocusedColumn.VisibleIndex >= AUDITING_GRID_VIEW_FIXED_COLUMN_COUNT))
                    {

                        IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = (IList<ExtendedCustomDataFieldInfo>)devExpressGrid.Data;
                        int index = extendedCustomDataFieldInfos.FindIndex(customDataFieldInfo => customDataFieldInfo.PhysicalName.Equals(devExpressGrid.FocusedColumn.FieldName));
                        if (index >= 0)
                        {
                            ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = extendedCustomDataFieldInfos[index];
                            if ((DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                            {
                                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                                DataFieldAuthority dataFieldAuthority = (DataFieldAuthority)extendedCustomDataFieldInfo.AuthorityType;
                                if (DataFieldHelper.IsReadOnly(physicalDataFieldType))
                                {
                                    dataFieldAuthority = DataFieldAuthority.ReadOnly;
                                }
                                if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                                {
                                    devExpressGrid.SetMenuButtons(false);
                                }
                                else
                                {
                                    devExpressGrid.SetMenuButtons(true);
                                    SetColumnEdit(devExpressGrid, extendedCustomDataFieldInfo);
                                }
                            }
                            else
                            {
                                devExpressGrid.SetMenuButtons(false);
                            }
                        }
                        else
                        {
                            devExpressGrid.SetMenuButtons(false);
                        }
                    }
                }
            }
        }

        #endregion       
    }
}
