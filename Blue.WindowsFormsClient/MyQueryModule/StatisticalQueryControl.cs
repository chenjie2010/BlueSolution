using System;
using System.IO;
using System.Data;
using System.Data.SqlTypes;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using FarPoint.Win.Spread;
using FarPoint.Excel;
using FarPoint.Win.Spread.CellType;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.MyQueryModule
{
    public partial class StatisticalQueryControl : UserControl
    {
        #region 私有常量

        /* 第 0 列为多选列 */
        private const int AUDITING_GRID_VIEW_FIXED_COLUMN_COUNT = 1;

        #endregion

        #region 私有变量

        private QueryBuilder currentQueryBuilder = null;
        private Dictionary<decimal, Int64> gridViewAuthorities;
        private Dictionary<string, ExtendedCustomDataFieldInfo> selectedCustomDataFieldInfos = null;
        private Dictionary<decimal, CommonNode> selectedCustomTableInfos = null;
        private Dictionary<decimal, DataTable> associationDataTables = null;
        private List<WhereConditon> userWhereConditons = null;
        private Dictionary<decimal, IList<CommonNode>> dicEnumCommonNodes = null;
        private AuthorityCondition authorityCondition = null;
        private GridCellEdited gridCellEdited = null;

        private TableJoin currentTableLink = TableJoin.InnerJoin;
        private bool distinct = false;//清除相同记录，默认不清除
        private bool autoLoadNode = false;
        private DataFieldEditedState dataFieldEditState = DataFieldEditedState.None;
        private int actualPageSize = AppSettingHelper.DefaultPageSize;

        /// <summary>
        /// PDF阅读器
        /// </summary>
        private static PDFViewerForm frmPDFViewer = new PDFViewerForm();

        #endregion

        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomExpressionContract customExpressionContract = null;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly ICustomQueyContract customQueyContract;
        private readonly IDataAuditingContract dataAuditingContract;
        private readonly IUserQueryContract userQueryContract;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public StatisticalQueryControl()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customExpressionContract = BusinessChannelFactory.CreateCustomExpressionContract();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
            customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            customQueyContract = BusinessChannelFactory.CreateCustomQueyContract();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();
            userQueryContract = BusinessChannelFactory.CreateUserQueryContract();

            currentQueryBuilder = new QueryBuilder();
            gridViewAuthorities = new Dictionary<decimal, Int64>();
            selectedCustomDataFieldInfos = new Dictionary<string, ExtendedCustomDataFieldInfo>(); /* 关键字为字段的物理名称 */
            selectedCustomTableInfos = new Dictionary<decimal, CommonNode>();
            associationDataTables = new Dictionary<decimal, DataTable>();
            dicEnumCommonNodes = new Dictionary<decimal, IList<CommonNode>>();
            currentQueryBuilder.Distinct = distinct;

            gridCellEdited = new GridCellEdited(customDataFieldContract, customEnumContract, customDepartmentContract,
                customAssociationContract, associatedDataFieldContract, dataAuditingContract);
            /* 用户类型 单位类型 */
            authorityCondition = new AuthorityCondition(customDepartmentContract, userTypeContract);
            userWhereConditons = authorityCondition.GetWhereConditons(null, null);

            //UserControlHelper.InitImageComboBoxEdit(icmbTableJoin, typeof(TableJoin));
            UserControlHelper.InitRepositoryItemImageComboBox(ritmTableJoin, typeof(TableJoin));
            
            UserControlHelper.InitRepositoryItemImageComboBox(rcmbAdvancedSotring, typeof(CustomSorting));
            UserControlHelper.InitRepositoryItemImageComboBox(rcmbCustomAggregate, typeof(Aggregate));
            UserControlHelper.InitRepositoryItemImageComboBox(rcmbDataFieldRealtion, typeof(DataFieldInnerRealtion));

        }

        #endregion

        #region 控件的方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StatisticalQueryControl_Load(object sender, EventArgs e)
        {
            if (!this.DesignMode)
            {
                IList<byte> dataWarehouseIds = customRoleContract.GetDataWarehouseIds(CurrentUser.Instance.UserId, DataAuthorityType.Query);
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
                gctrlAdvanced.DataSource = currentQueryBuilder.QueryFields;
            }
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewAdvanced_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 数据仓库切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbDataWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                treeView.Nodes.Clear();
                if (icmbDataWarehouse.EditValue != null)
                {
                    byte dataWarehouseId = Convert.ToByte(icmbDataWarehouse.EditValue);
                    Dictionary<DatabaseNodeType, IList<CommonNode>> dicCommonNodes = customRoleContract.GetAuthorizedCommonNodes(CurrentUser.Instance.UserId, dataWarehouseId, DataAuthorityType.Query);
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
                                    TreeNode leafNode = new TreeNode { Text = string.Empty, Tag = new CommonNode() };
                                    tnTable.Nodes.Add(leafNode);
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
            /* 展开数据表*/
            if (e.Node.Level == 2 && string.IsNullOrEmpty(e.Node.Nodes[0].Text))
            {
                e.Node.Nodes.Clear();
                CommonNode tableCommonNode = e.Node.Tag as CommonNode;
                /* 系统字段 */
                Int64 systemDataFieldAuthority = customRoleContract.GetSystemDataFieldAuthority(CurrentUser.Instance.UserId, tableCommonNode.NodeId, (byte)DataAuthorityType.Query);
                List<ExtendedCustomDataFieldInfo> systemDataFieldInfos = CommonBussinessHelper.GetExtendedCustomDataFieldInfos(tableCommonNode.NodeId, tableCommonNode.NodeCode, systemDataFieldAuthority);
                foreach (var systemDataFieldInfo in systemDataFieldInfos)
                {
                    TreeNode treeNode = new TreeNode { Text = systemDataFieldInfo.LogicalName, ToolTipText = systemDataFieldInfo.PhysicalName, Tag = systemDataFieldInfo };
                    treeNode.ForeColor = Color.DarkGray;
                    e.Node.Nodes.Add(treeNode);
                }
                /* 物理字段与逻辑字段 */
                IList<decimal> tableIds = new List<decimal>();
                tableIds.Add(tableCommonNode.NodeId);
                IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = customRoleContract.GetAuthorizedExtendedCustomDataFieldInfos(CurrentUser.Instance.UserId, tableIds, DataAuthorityType.Query);
                foreach (ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
                {
                    TreeNode treeNode = new TreeNode { Text = extendedCustomDataFieldInfo.LogicalName, ToolTipText = extendedCustomDataFieldInfo.PhysicalName, Tag = extendedCustomDataFieldInfo };
                    e.Node.Nodes.Add(treeNode);
                }
            }
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
        /// 节点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            /* 四级：数据库，分类，表，字段 */
            if (e.Node.Level == 2)
            {
                /* 表 */
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
            else if (e.Node.Level == 3 && !autoLoadNode)
            {

                /* 字段 */
                string nodeText = e.Node.Text;
                if (string.IsNullOrEmpty(nodeText))
                {
                    return;
                }
                CommonNode tableCommonNode = e.Node.Parent.Tag as CommonNode;
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = (ExtendedCustomDataFieldInfo)e.Node.Tag;
                string alias = e.Node.Text;
                DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
                string tableName = string.Empty;
                string name = string.Empty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.LogicalDataField:
                        tableName = tableCommonNode.NodeCode;
                        extendedCustomDataFieldInfo.PhysicalName = customDataFieldContract.GetDataFieldLogicalExpression(extendedCustomDataFieldInfo.DataFieldId);
                        name = extendedCustomDataFieldInfo.Name;
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        tableName = tableCommonNode.NodeCode;
                        name = extendedCustomDataFieldInfo.Name;
                        break;

                    case DataFieldProperty.SystemPhysicalDataField:
                        //extendedCustomDataFieldInfo.PhysicalName = 
                        name = string.Format("{0}_{1}", tableCommonNode.NodeCode, extendedCustomDataFieldInfo.Name);
                        break;
                }
                QueryField qf = new QueryField(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.DataFieldType, tableCommonNode.NodeId,
                    tableCommonNode.NodeCode, tableCommonNode.NodeName, name, extendedCustomDataFieldInfo.PhysicalName, alias, extendedCustomDataFieldInfo.AuthorityType, dataFieldProperty);
                if (e.Node.Checked)
                {                    
                    if (!selectedCustomDataFieldInfos.ContainsKey(name))
                    {
                        selectedCustomDataFieldInfos.Add(name, extendedCustomDataFieldInfo);
                    }
                    currentTableLink = TableJoin.InnerJoin;
                    if (beiTableJoin.EditValue != null)
                    {
                        currentTableLink = (TableJoin)Convert.ToByte(beiTableJoin.EditValue);
                    }
                    if (!currentQueryBuilder.CurrentTableNames.Contains(tableCommonNode.NodeCode))
                    {
                        string currentTableLinkText = string.Empty;
                        switch (currentTableLink)
                        {
                            case TableJoin.InnerJoin:
                                currentTableLinkText = "Inner Join";
                                break;

                            case TableJoin.LeftOuterJoin:
                                currentTableLinkText = "Left Outer Join";
                                break;

                            case TableJoin.RightOuterJoin:
                                currentTableLinkText = "Right Outer Join";
                                break;

                            case TableJoin.FullOuterJoin:
                                currentTableLinkText = "Full Outer Join";
                                break;
                            default:
                                break;
                        }
                        currentQueryBuilder.CurrentTableNames.Add(tableCommonNode.NodeCode, currentTableLinkText);
                    }
                    if (!selectedCustomTableInfos.ContainsKey(tableCommonNode.NodeId))
                    {
                        selectedCustomTableInfos.Add(tableCommonNode.NodeId, tableCommonNode);
                    }
                    if (!currentQueryBuilder.QueryFields.Contains(qf))
                    {
                        currentQueryBuilder.QueryFields.Add(qf);
                    }
                }
                else
                {
                    bool exist = false;
                    foreach (QueryField queryField in currentQueryBuilder.QueryFields)
                    {
                        if (queryField.DataTableName.Equals(tableCommonNode.NodeCode) &&
                            ((queryField.DataFieldProperty != (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty) ||
                            (queryField.DataFieldId != extendedCustomDataFieldInfo.DataFieldId)))
                        {
                            exist = true;
                            break;
                        }
                    }
                    /* (1) 移除不存在查询字段的表名 */
                    if (!exist)
                    {
                        if (currentQueryBuilder.CurrentTableNames.Contains(tableCommonNode.NodeCode))
                        {
                            currentQueryBuilder.CurrentTableNames.Remove(tableCommonNode.NodeCode);
                        }
                        if (selectedCustomTableInfos.ContainsKey(tableCommonNode.NodeId))
                        {
                            selectedCustomTableInfos.Remove(tableCommonNode.NodeId);
                        }
                    }
                    /* (2) 移除查询的字段 */
                    currentQueryBuilder.QueryFields.Remove(qf);
                }
            }
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkProperties_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkPrint_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 数据导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkExport_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkReset_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkClean_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 分组
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bckGroupBy_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            currentQueryBuilder.GroupBy = bckGroupBy.Checked;
            gridColAggregate.Visible = currentQueryBuilder.GroupBy;
            if (gridColAggregate.Visible)
            {
                gridColAggregate.VisibleIndex = 5;
            }
        }

        /// <summary>
        /// 显示系统字段内容
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnCustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            SystemConfigHelper.SetColumnDisplayText(e);
        }

        /// <summary>
        /// 聚合单元格变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rcmbCustomAggregate_EditValueChanged(object sender, EventArgs e)
        {
            gridViewAdvanced.PostEditor();
        }

        /// <summary>
        /// 分组单元格变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rcmbDataFieldRealtion_EditValueChanged(object sender, EventArgs e)
        {
            gridViewAdvanced.PostEditor();
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rcmbAdvancedSotring_EditValueChanged(object sender, EventArgs e)
        {
            gridViewAdvanced.PostEditor();
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
        /// 浏览文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressUploadFile_OnTextChanged(object sender, EventArgs e)
        {
            DevExpressUploadFile devExpressUploadFile = sender as DevExpressUploadFile;
            devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, devExpressUploadFile.FileName);
        }

        /// <summary>
        /// 双击选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            TreeView treeView = sender as TreeView;
            if (treeView.SelectedNode.IsExpanded)
            {
                treeView.SelectedNode.ImageIndex = 1;
            }
            else
            {
                treeView.SelectedNode.ImageIndex = 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemCheckShow_CheckedChanged(object sender, EventArgs e)
        {
            gridViewAdvanced.FocusedRowHandle = -1;
        }

        /// <summary>
        /// 双击保存文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnRowDoubleClick(object sender, RowEvent e)
        {
            SaveDocAttachment();
        }

        /// <summary>
        /// 数据查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnQuery_Click(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// 多选项菜单弹出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemCheckedComboBoxEdit_BeforeShowMenu(object sender, BeforeShowMenuEventArgs e)
        {
            CheckedComboBoxEdit checkedComboBoxEdit = sender as CheckedComboBoxEdit;
            if (checkedComboBoxEdit == null)
            {
                return;
            }
            string multiSelectedValue = DataConvertionHelper.GetString(devExpressGrid.FocusedValue);
            if (!string.IsNullOrWhiteSpace(multiSelectedValue))
            {
                for (int idx = 0; idx < multiSelectedValue.Length; idx++)
                {
                    if (idx < checkedComboBoxEdit.Properties.Items.Count)
                    {
                        if (multiSelectedValue[idx].Equals('1'))
                        {
                            checkedComboBoxEdit.Properties.Items[idx].CheckState = CheckState.Checked;
                        }
                        else
                        {
                            checkedComboBoxEdit.Properties.Items[idx].CheckState = CheckState.Unchecked;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 多选完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemCheckedComboBoxEdit_EditValueChanged(object sender, EventArgs e)
        {
            CheckedComboBoxEdit checkedComboBoxEdit = sender as CheckedComboBoxEdit;
            if (checkedComboBoxEdit == null)
            {
                return;
            }
            ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = selectedCustomDataFieldInfos[devExpressGrid.FocusedColumn.FieldName];
            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
            object dataFieldValue = null;
            long multiSelectedValue = 0;
            for (int idx = 0; idx < checkedComboBoxEdit.Properties.Items.Count; idx++)
            {
                if (checkedComboBoxEdit.Properties.Items[idx].CheckState == CheckState.Checked)
                {
                    multiSelectedValue = multiSelectedValue | (1L << idx);
                }
            }
            if (multiSelectedValue > 0)
            {
                dataFieldValue = Convert.ToString(multiSelectedValue, 2);
            }

            devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = dataFieldValue;
            devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, dataFieldValue);
        }

        /// <summary>
        /// 行变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnFocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            dataFieldEditState = DataFieldEditedState.None;
            SetFocusedColumnEditable(false);
            ShowPicAttachment();
        }

        /// <summary>
        /// 列选择变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnFocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (e.PrevFocusedColumn != null && !e.PrevFocusedColumn.Name.Equals(devExpressGrid.CheckboxColumnName))
            {
                e.PrevFocusedColumn.OptionsColumn.AllowEdit = false;
                e.PrevFocusedColumn.OptionsColumn.ReadOnly = true;
                ShowPicAttachment();
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnDeleteClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认删除该记录吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    Dictionary<decimal, decimal> relations = new Dictionary<decimal, decimal>(currentQueryBuilder.CurrentTableNames.Count);
                    foreach (KeyValuePair<decimal, CommonNode> keyValue in selectedCustomTableInfos)
                    {
                        string key = string.Format("{0}_RecordId", keyValue.Value.NodeCode);
                        decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[key]);
                        customTableContract.DeleteRecord(keyValue.Value.NodeId, recordId);
                    }
                    LoadData();
                    Cursor = Cursors.Default;
                    MessageBox.Show("删除记录成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// 批量删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnBatchDeleteClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (devExpressGrid.MultiSelectedValues.Count == 0)
                {
                    MessageBox.Show("没有进行批量选择，请先选择!", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (MessageBox.Show("确认删除批量选择的记录吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    Dictionary<decimal, decimal> relations = new Dictionary<decimal, decimal>(currentQueryBuilder.CurrentTableNames.Count);
                    foreach (KeyValuePair<decimal, CommonNode> keyValue in selectedCustomTableInfos)
                    {
                        string key = string.Format("{0}_RecordId", keyValue.Value.NodeCode);
                        IList<decimal> recordIds = new List<decimal>(devExpressGrid.MultiSelectedValues.Count);
                        foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                        {
                            decimal id = DataConvertionHelper.GetDecimal(rowEvent.Values[key]);
                            if (!recordIds.Contains(id))
                            {
                                recordIds.Add(id);
                            }
                        }
                        customTableContract.DeleteRecords(keyValue.Value.NodeId, recordIds);
                    }
                    LoadData();
                    Cursor = Cursors.Default;
                    MessageBox.Show("批量删除记录成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// 完全删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnCompleteDeleteClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("确认删除当前查询出来的所有记录吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    IList<decimal> tableIds = new List<decimal>(selectedCustomTableInfos.Count);
                    foreach (KeyValuePair<decimal, CommonNode> keyValue in selectedCustomTableInfos)
                    {
                        tableIds.Add(keyValue.Value.NodeId);
                    }
                    byte dataWarehouseId = Convert.ToByte(icmbDataWarehouse.EditValue);
                    customQueyContract.Delete(dataWarehouseId, tableIds, currentQueryBuilder, userWhereConditons);
                    LoadData();
                    Cursor = Cursors.Default;
                    MessageBox.Show("完全删除记录成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        /// 右键菜单弹出来之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnBeforePopupMenu(object sender, CancelEventArgs e)
        {
            if ((devExpressGrid.FocusedColumn != null) && (devExpressGrid.FocusedColumn.VisibleIndex >= AUDITING_GRID_VIEW_FIXED_COLUMN_COUNT))
            {
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = selectedCustomDataFieldInfos[devExpressGrid.FocusedColumn.FieldName];
                decimal tableId = selectedCustomTableInfos[extendedCustomDataFieldInfo.TableId].NodeId;
                if (gridViewAuthorities.ContainsKey(tableId))
                {
                    devExpressGrid.Authority = gridViewAuthorities[tableId];
                }
                else
                {
                    Int64 authority = customRoleContract.GetTableAuthority(CurrentUser.Instance.UserId, tableId, DataAuthorityType.Query);
                    devExpressGrid.Authority = authority;
                    gridViewAuthorities.Add(tableId, authority);
                }
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
                        gridCellEdited.SetColumnEdit(devExpressGrid, extendedCustomDataFieldInfo);
                    }
                }
                else
                {
                    devExpressGrid.SetMenuButtons(false);
                }
            }
        }

        /// <summary>
        /// 单元格的值发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnCellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (!e.Column.Name.Equals(devExpressGrid.CheckboxColumnName))
                {
                    e.Column.OptionsColumn.AllowEdit = false;
                    e.Column.OptionsColumn.ReadOnly = true;
                    if (e.RowHandle >= 0)
                    {
                        if (MessageBox.Show(string.Format("确认{0}该记录吗？", UserEnumHelper.GetEnumText(dataFieldEditState)), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                        {
                            LoadData();
                            return;
                        }
                        string waring = string.Empty;
                        bool refresh = false;
                        ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = selectedCustomDataFieldInfos[devExpressGrid.FocusedColumn.FieldName];
                        decimal tableId = selectedCustomTableInfos[extendedCustomDataFieldInfo.TableId].NodeId;
                        string tableName = selectedCustomTableInfos[extendedCustomDataFieldInfo.TableId].NodeCode;                      
                        bool result = gridCellEdited.DevExpressGridCellValueChanged(devExpressGrid, e, dataFieldEditState, extendedCustomDataFieldInfo,
                            tableId, tableName, currentQueryBuilder, userWhereConditons, ref waring, ref refresh);
                        Cursor = Cursors.Default;
                        if (result)
                        {
                            if (refresh)
                            {
                                LoadData();
                            }
                            MessageBox.Show("更新成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show(waring, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                LoadData();
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnEditClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataFieldEditState = DataFieldEditedState.Edit;
            SetFocusedColumnEditable(true);
        }

        /// <summary>
        /// 批量编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnBatchEditClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataFieldEditState = DataFieldEditedState.BathcEdit;
            SetFocusedColumnEditable(true);
        }

        /// <summary>
        /// 全部编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnCompleteEditClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dataFieldEditState = DataFieldEditedState.CompleteEdit;
            SetFocusedColumnEditable(true);
        }


        /// <summary>
        /// 设置查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void itemButtonCondition_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            string key = gridViewAdvanced.GetFocusedRowCellValue("Name").ToString();

            ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = selectedCustomDataFieldInfos[key];
            QueryConditionForm frmQueryCondition = new QueryConditionForm();
            if (gridViewAdvanced.FocusedValue != null)
            {
                frmQueryCondition.QueryCondition = gridViewAdvanced.FocusedValue.ToString();
            }
            else
            {
                frmQueryCondition.QueryCondition = string.Empty;
            }
            frmQueryCondition.IsValidate = true;
            frmQueryCondition.TableName = gridViewAdvanced.GetFocusedRowCellValue("DataTableName").ToString();
            frmQueryCondition.CustomDataFieldInfo = (CustomDataFieldInfo)extendedCustomDataFieldInfo;
            frmQueryCondition.UpdateTextHandler = (where) =>
            {
                gridViewAdvanced.SetFocusedValue(where);
            };
            frmQueryCondition.ShowDialog();
        }

        /// <summary>
        /// 使得选择生效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rcmbAdvancedSotring_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridViewAdvanced.FocusedRowHandle = -1;
        }

        /// <summary>
        /// 使得选择生效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rcmbCustomAggregate_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridViewAdvanced.FocusedRowHandle = -1;
        }

        /// <summary>
        /// 使得选择生效
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rcmbDataFieldRealtion_SelectedIndexChanged(object sender, EventArgs e)
        {
            gridViewAdvanced.FocusedRowHandle = -1;
        }



        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiProperties_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryPropertiesForm frmQueryProperties = new QueryPropertiesForm();
            //(1)清除相同记录，默认不清除 (2)每页显示的数目，如果为0，则全部显示
            frmQueryProperties.SetDataQueryProperty(distinct, devExpressGrid.PageSize);
            frmQueryProperties.GetDataQueryProperty = (unique, pageSize) =>
            {
                distinct = unique;
                actualPageSize = pageSize;
                devExpressGrid.PageSize = pageSize;
                currentQueryBuilder.Distinct = unique;
            };
            frmQueryProperties.ShowDialog();
        }

        /// <summary>
        /// 打开查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiOpen_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                QueryStatementForm frmQueryStatement = new QueryStatementForm();
                frmQueryStatement.NodeSelected = (node) =>
                {
                    autoLoadNode = true;
                    ClearQuery();
                    autoLoadNode = false;
                    UserQueryInfo userQueryInfo = userQueryContract.GetModelInfo(node.NodeId);
                    if (userQueryInfo != null)
                    {
                        Dictionary<decimal, CustomTableInfo> dicCustomTableInfos = new Dictionary<decimal, CustomTableInfo>();
                        Dictionary<decimal, TreeNode> dicTreeNodes = new Dictionary<decimal, TreeNode>();
                        IList<QueryAndDataFieldInfo> queryAndDataFieldInfos = userQueryContract.GetQueryAndDataFieldInfos(node.NodeId);
                        currentQueryBuilder.QueryFields.Clear();
                        selectedCustomDataFieldInfos.Clear();
                        if (queryAndDataFieldInfos.Count > 0)
                        {
                            decimal dataFieldId = queryAndDataFieldInfos[0].DataFieldId;
                            byte dataWarehouseId = 0;
                            int dfIdx = 1;
                            do
                            {
                                if (dataFieldId > 0)
                                {
                                    decimal tbId = customDataFieldContract.GetParentNodeId(dataFieldId);
                                    icmbDataWarehouse.EditValue = customTableContract.GetDataWarehouseId(tbId);
                                    break;
                                }
                                dataFieldId = queryAndDataFieldInfos[dfIdx++].DataFieldId;
                                if (dfIdx >= queryAndDataFieldInfos.Count)
                                {
                                    break;
                                }
                            } while (dataWarehouseId < 0);
                        }
                        Int64 tableNameRelation = userQueryInfo.TableNameRelation;
                        foreach (QueryAndDataFieldInfo queryAndDataFieldInfo in queryAndDataFieldInfos)
                        {
                            CustomTableInfo customTableInfo = null;
                            decimal tableId = decimal.MinValue;
                            DataFieldProperty dataFieldProperty = (DataFieldProperty)queryAndDataFieldInfo.DataFieldProperty;
                            if (dataFieldProperty == DataFieldProperty.PhysicalDataField || dataFieldProperty == DataFieldProperty.LogicalDataField)
                            {
                                tableId = customDataFieldContract.GetParentNodeId(queryAndDataFieldInfo.DataFieldId);
                            }
                            else
                            {
                                tableId = queryAndDataFieldInfo.TableId;
                                queryAndDataFieldInfo.DataFieldId = queryAndDataFieldInfo.SystemDataField;
                            }
                            if (!dicCustomTableInfos.ContainsKey(tableId))
                            {
                                customTableInfo = customTableContract.GetModelInfo(tableId);
                                if (customTableInfo == null)
                                {
                                    continue;
                                }
                                dicCustomTableInfos.Add(tableId, customTableInfo);
                                selectedCustomTableInfos.Add(tableId, new CommonNode(customTableInfo.TableId, customTableInfo.LogicalName, customTableInfo.PhysicalName));
                                string currentTableLinkText = string.Empty;
                                if (tableNameRelation > 0)
                                {
                                    if (AuthorityHelper.CheckAuthority(tableNameRelation, (byte)TableJoin.InnerJoin))
                                    {
                                        currentTableLinkText = "Inner Join";

                                    }
                                    else if (AuthorityHelper.CheckAuthority(tableNameRelation, (byte)TableJoin.LeftOuterJoin))
                                    {
                                        currentTableLinkText = "Left Outer Join";

                                    }
                                    else if (AuthorityHelper.CheckAuthority(tableNameRelation, (byte)TableJoin.RightOuterJoin))
                                    {
                                        currentTableLinkText = "Right Outer Join";
                                    }
                                    else if (AuthorityHelper.CheckAuthority(tableNameRelation, (byte)TableJoin.FullOuterJoin))
                                    {
                                        currentTableLinkText = "Full Outer Join";
                                    }
                                    else
                                    {
                                        throw new ArgumentException("该查询有误，无法加载，请删除该查询！");
                                    }
                                    tableNameRelation = tableNameRelation >> 4;
                                }
                                currentQueryBuilder.CurrentTableNames.Add(customTableInfo.PhysicalName, currentTableLinkText);
                                /* 共四级，分别为：数据库，分类，表，字段 */
                                bool skip = false;
                                foreach (TreeNode tnFirst in treeView.Nodes)
                                {
                                    foreach (TreeNode tnSecond in tnFirst.Nodes)
                                    {
                                        foreach (TreeNode tnThird in tnSecond.Nodes)
                                        {
                                            CommonNode commonNode = (CommonNode)tnThird.Tag;
                                            if (commonNode.NodeId == tableId)
                                            {
                                                dicTreeNodes.Add(tableId, tnThird);
                                                if (tnThird.Nodes.Count == 1 && string.IsNullOrWhiteSpace(tnThird.Nodes[0].Text))
                                                {
                                                    tnThird.Expand();
                                                    if (!tnThird.Parent.IsExpanded)
                                                    {
                                                        tnThird.Parent.Expand();
                                                    }
                                                    skip = true;
                                                    break;
                                                }
                                            }
                                        }
                                        if (skip)
                                        {
                                            break;
                                        }
                                    }
                                    if (skip)
                                    {
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                customTableInfo = dicCustomTableInfos[tableId];
                            }
                            CustomDataFieldInfo customDataFieldInfo = null;
                            string name = string.Empty;
                            switch (dataFieldProperty)
                            {
                                case DataFieldProperty.LogicalDataField:
                                    customDataFieldInfo = customDataFieldContract.GetModelInfo(queryAndDataFieldInfo.DataFieldId);
                                    name = customDataFieldInfo.PhysicalName;
                                    customDataFieldInfo.PhysicalName = customDataFieldContract.GetDataFieldLogicalExpression(queryAndDataFieldInfo.DataFieldId);
                                    break;

                                case DataFieldProperty.PhysicalDataField:
                                    customDataFieldInfo = customDataFieldContract.GetModelInfo(queryAndDataFieldInfo.DataFieldId);
                                    name = customDataFieldInfo.PhysicalName;
                                    break;

                                case DataFieldProperty.SystemPhysicalDataField:
                                    SystemDataField systemDataField =(SystemDataField)queryAndDataFieldInfo.SystemDataField;
                                    customDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(customTableInfo.TableId, customTableInfo.PhysicalName, systemDataField);
                                    name = string.Format("{0}_{1}", customTableInfo.PhysicalName, DataFieldHelper.GetOnlySystemLogicalDataFieldName(systemDataField));
                                    break;
                            }
                            DataFieldAuthority dataFieldAuthority = customRoleContract.GetDataFieldAuthority(CurrentUser.Instance.UserId,
                                   DataConvertionHelper.GetConvertedByte(DataAuthorityType.Query), queryAndDataFieldInfo.DataFieldId);

                            QueryField qf = new QueryField(queryAndDataFieldInfo.DataFieldId, queryAndDataFieldInfo.DataFieldType, customTableInfo.TableId,
                                customTableInfo.PhysicalName, customTableInfo.LogicalName, name, customDataFieldInfo.PhysicalName, customDataFieldInfo.LogicalName,
                                (byte)DataFieldAuthority.ReadOnly, dataFieldProperty, queryAndDataFieldInfo.Condition, queryAndDataFieldInfo.DataFieldInnerRealtion,
                                queryAndDataFieldInfo.SortingType, queryAndDataFieldInfo.CustomAggregate);
                            qf.AuthorityType = (byte)dataFieldAuthority;
                            currentQueryBuilder.QueryFields.Add(qf);
                            selectedCustomDataFieldInfos.Add(name, new ExtendedCustomDataFieldInfo(customDataFieldInfo.DataFieldId, customDataFieldInfo.EnumId, customDataFieldInfo.ParentDataFieldId, customDataFieldInfo.AssociatedDataFieldId, customDataFieldInfo.TableId,
                                customDataFieldInfo.LogicalName, customDataFieldInfo.PhysicalName, customDataFieldInfo.DataFieldCode, customDataFieldInfo.DataFieldProperty, customDataFieldInfo.DataFieldType, customDataFieldInfo.DataFieldLength, customDataFieldInfo.BasedDataType,
                                customDataFieldInfo.RegexExpression, customDataFieldInfo.ExpressionText, customDataFieldInfo.DataFieldSetting, customDataFieldInfo.RequiredDataField, customDataFieldInfo.AutoComplete, customDataFieldInfo.IndexCreated, customDataFieldInfo.HelpEnabled,
                                customDataFieldInfo.HelpContent, customDataFieldInfo.Tooltip, customDataFieldInfo.Sorting, customDataFieldInfo.Notes, name, (byte)dataFieldAuthority));
                            if (dicTreeNodes.ContainsKey(customDataFieldInfo.TableId))
                            {
                                foreach (TreeNode tn in dicTreeNodes[customDataFieldInfo.TableId].Nodes)
                                {
                                    CustomDataFieldInfo dataFieldInfo = (CustomDataFieldInfo)tn.Tag;
                                    if (dataFieldInfo.DataFieldId == queryAndDataFieldInfo.DataFieldId &&
                                        ((byte)dataFieldProperty == dataFieldInfo.DataFieldProperty))
                                    {
                                        autoLoadNode = true;
                                        tn.Checked = true;
                                        autoLoadNode = false;
                                        break;
                                    }
                                }
                            }
                        }
                        bckGroupBy.Checked = userQueryInfo.IsGroup;
                        currentQueryBuilder.GroupBy = userQueryInfo.IsGroup;
                        currentQueryBuilder.Distinct = userQueryInfo.IsDistinct;
                        currentQueryBuilder.ValidWhere = userQueryInfo.EneableCondition;
                        gridViewAuthorities.Clear();
                        devExpressGrid.CurrentPageIndex = 0;
                        /*  加载数据 */
                        LoadData();
                    }
                };
                frmQueryStatement.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 保存查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (currentQueryBuilder.CurrentTableNames.Count == 0)
                {
                    MessageBox.Show("查询的字段不能为空，无法保存！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (currentQueryBuilder.CurrentTableNames.Count > 16)
                {
                    MessageBox.Show("多表查询的数量超过16个，无法保存！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                QuerySavedForm frmQuerySaved = new QuerySavedForm();
                frmQuerySaved.CurrentQueryBuilder = currentQueryBuilder;
                frmQuerySaved.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 重置条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiReset_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageBox.Show("确认清除字段的所有条件，仅保留选择的查询字段？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                foreach (QueryField queryField in currentQueryBuilder.QueryFields)
                {
                    queryField.Sorting = (byte)CustomSorting.None;
                    queryField.QueryDataFieldRealtion = (byte)DataFieldInnerRealtion.And;
                    queryField.Criteria = string.Empty;
                }
                gridViewAdvanced.RefreshData();
                gridViewAdvanced.RefreshEditor(true);
            }
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (currentQueryBuilder.CurrentTableNames.Count == 0)
            {
                MessageBox.Show("请先选择字段！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SaveFileDialog dlg = new SaveFileDialog();
            ExportedModeForm frmExportedMode = new ExportedModeForm();
            frmExportedMode.ExportedModeValue = 0;
            frmExportedMode.DataExported = (exportedMode) =>
            {
                switch (exportedMode)
                {
                    case ExportedMode.Excel:
                        dlg.FileName = string.Format("统计查询导出_{0}.xlsx", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                        dlg.Filter = AppSettingHelper.DefaultExcelFormat;
                        dlg.RestoreDirectory = true;
                        if (dlg.ShowDialog() != DialogResult.OK)
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                        if (currentQueryBuilder.GroupBy)
                        {
                            if (dlg.FileName.EndsWith("xlsx"))
                            {
                                XlsxExportOptions options = new XlsxExportOptions(TextExportMode.Value, true, false, false);
                                devExpressGrid.DevExpressGridControl.ExportToXlsx(dlg.FileName, options);
                            }
                            else
                            {
                                XlsExportOptions options = new XlsExportOptions(TextExportMode.Value, true, false, false);
                                devExpressGrid.DevExpressGridControl.ExportToXls(dlg.FileName, options);
                            }
                        }
                        else
                        {
                            ExportData(exportedMode, dlg.FileName);
                        }
                        break;

                    case ExportedMode.PDF:
                        dlg.FileName = string.Format("统计查询导出_{0}.pdf", DateTime.Now.ToString("yyyyMMddHHmmssfff"));
                        dlg.DefaultExt = "pdf";
                        dlg.Filter = "Adobe PDF  文件(*.pdf)|";
                        dlg.RestoreDirectory = true;
                        if (dlg.ShowDialog() != DialogResult.OK)
                        {
                            Cursor = Cursors.Default;
                            return;
                        }
                        if (currentQueryBuilder.GroupBy)
                        {
                            devExpressGrid.DevExpressGridControl.ExportToPdf(dlg.FileName);
                        }
                        else
                        {
                            ExportData(exportedMode, dlg.FileName);
                        }
                        break;
                }
            };
            frmExportedMode.ShowDialog();
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (devExpressGrid.DevExpressGridControl.IsPrintingAvailable)
                {
                    devExpressGrid.DevExpressGridControl.ShowPrintPreview();
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
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiQuery_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (currentQueryBuilder.CurrentTableNames.Count == 0)
            {
                MessageBox.Show("请先选择字段！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            gridViewAuthorities.Clear();
            devExpressGrid.CurrentPageIndex = 0;
            /*  加载数据 */
            LoadData();
        }

        /// <summary>
        /// 清除查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MessageBox.Show("确认清除选择的所有查询字段？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                ClearQuery();
            }
        }

        /// <summary>
        /// 管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            QueryManagementForm frmQueryManagement = new QueryManagementForm();
            frmQueryManagement.ShowDialog();
        }

        #endregion

        #region 私有方法


        /// <summary>
        /// 清除查询
        /// </summary>
        private void ClearQuery()
        {
            currentQueryBuilder.CurrentTableNames.Clear();
            selectedCustomTableInfos.Clear();
            currentQueryBuilder.QueryFields.Clear();
            foreach (TreeNode tnFirstLevel in treeView.Nodes)
            {
                foreach (TreeNode tnScdLevel in tnFirstLevel.Nodes)
                {
                    foreach (TreeNode tnTrdLevel in tnScdLevel.Nodes)
                    {
                        if (tnTrdLevel.Checked)
                        {
                            tnTrdLevel.Checked = false;
                        }
                        else
                        {
                            foreach (TreeNode tnFourthLevel in tnTrdLevel.Nodes)
                            {
                                if (tnFourthLevel.Checked)
                                {
                                    tnFourthLevel.Checked = false;
                                }
                            }
                        }
                    }
                    if (tnScdLevel.Checked)
                    {
                        tnScdLevel.Checked = false;
                    }
                }
                if (tnFirstLevel.Checked)
                {
                    tnFirstLevel.Checked = false;
                }
            }
            //rtxtSql.Text = string.Empty;
            devExpressGrid.DataSource = new DataTable();
            devExpressGrid.RecordCount = 0;
        }

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="exportFormat"></param>
        /// <param name="path"></param>
        private void ExportData(ExportedMode exportedMode, string path)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                /* 1 设置条件 */
                SetCondition();
                int pageSize = 500;
                byte dataWarehouseId = Convert.ToByte(icmbDataWarehouse.EditValue);
                int totalCount = customQueyContract.GetAuthorizedRecordCount(currentQueryBuilder, userWhereConditons, dataWarehouseId);
                if (totalCount == 0)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("当前查询结果为空，请重新执行查询！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if ((currentQueryBuilder.GroupBy || currentQueryBuilder.Distinct) && totalCount > 65536)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("当前查询采用了分组，或者结果记录数超过65536条，请调整条件重新执行查询！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ProgressForm frmProgress = new ProgressForm();
                bool exit = false;
                int progress = totalCount / pageSize;
                int steps = ((totalCount % pageSize) == 0) ? (totalCount / pageSize) : (totalCount / pageSize + 1);
                if (progress > 0)
                {
                    frmProgress.MinValue = 0;
                    frmProgress.MaxValue = steps;
                    frmProgress.Show();
                }
                switch (exportedMode)
                {
                    case ExportedMode.Excel:
                        int maxCountPerSheet = 60000;
                        int pageCount = maxCountPerSheet / pageSize;
                        int sheetCount = ((totalCount % maxCountPerSheet) == 0) ? (totalCount / maxCountPerSheet) : (totalCount / maxCountPerSheet + 1);
                        /* 如果是分组查询，则一次获得全部记录 */
                        if (currentQueryBuilder.GroupBy || currentQueryBuilder.Distinct)
                        {
                            sheetCount = 1;
                            pageCount = 1;
                            pageSize = 0;
                        }
                        FpSpread fsExcel = new FpSpread();
                        for (int sheetIndex = 0; sheetIndex < sheetCount; sheetIndex++)
                        {
                            SheetView sheetView = new SheetView(string.Format("{0}_{1}", this.Text, sheetIndex));
                            if (totalCount > maxCountPerSheet)
                            {
                                sheetView.RowCount = maxCountPerSheet;
                            }
                            else
                            {
                                sheetView.RowCount = totalCount;
                            }
                            fsExcel.Sheets.Add(sheetView);
                            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                            {
                                int start = pageIndex * pageSize;
                                int pos = (sheetIndex * maxCountPerSheet) + start;
                                if (pos >= totalCount)
                                {
                                    break;
                                }
                                DataTable dataTable = customQueyContract.GetAuthorizedData(dataWarehouseId, currentQueryBuilder, userWhereConditons, pos, pageSize).Tables[0];
                                int skippedCols = 0;
                                if (!currentQueryBuilder.GroupBy && !currentQueryBuilder.Distinct)
                                {
                                    skippedCols = currentQueryBuilder.CurrentTableNames.Count;
                                }
                                if (pageIndex == 0)
                                {
                                    /* 每个表的第0列关键字 RecordId 列不导出 */
                                    for (int col = skippedCols; col < dataTable.Columns.Count; col++)
                                    {
                                        fsExcel.Sheets[sheetIndex].Columns[col - skippedCols].Label = dataTable.Columns[col].Caption;
                                    }
                                }
                                TextCellType textCellType = new TextCellType();
                                textCellType.Multiline = true;
                                textCellType.MaxLength = 4000;
                                for (int col = skippedCols; col < dataTable.Columns.Count; col++)
                                {
                                    fsExcel.Sheets[sheetIndex].Columns[col - skippedCols].CellType = textCellType;
                                    for (int row = 0; row < dataTable.Rows.Count; row++)
                                    {
                                        string result = GetConvertedResult(dataTable.Columns[col].ColumnName, dataTable.Rows[row][col]);
                                        fsExcel.Sheets[sheetIndex].Cells[start + row, col - skippedCols].Text = result;
                                    }
                                }
                                if (progress > 0)
                                {
                                    frmProgress.Tip = string.Format("正在导出第{0}个页面的数据，请稍后...", sheetIndex * pageCount + pageIndex + 1);
                                    frmProgress.IncreaseStep();
                                    if (frmProgress != null && frmProgress.Cancel)
                                    {
                                        exit = true;
                                        break; ;
                                    }
                                }
                            }
                            if (exit)
                            {
                                frmProgress.CloseFrom();
                                frmProgress = null;
                                break;
                            }
                        }
                        if (frmProgress != null && !frmProgress.IsDisposed)
                        {
                            frmProgress.CloseFrom();
                        }
                        if (path.EndsWith("xlsx"))
                        {
                            fsExcel.SaveExcel(path, ExcelSaveFlags.DataOnly | ExcelSaveFlags.SaveCustomColumnHeaders | ExcelSaveFlags.UseOOXMLFormat);
                        }
                        else
                        {
                            fsExcel.SaveExcel(path, ExcelSaveFlags.DataOnly | ExcelSaveFlags.SaveCustomColumnHeaders);
                        }
                        Cursor = Cursors.Default;
                        MessageBox.Show("Excel 文件导出成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    case ExportedMode.PDF:
                        ExportedDataTableFormat exportedDataTableFormat = new ExportedDataTableFormat();
                        exportedDataTableFormat.CreateHeader = (graph, textSize) =>
                        {
                            DataTable dataTable = customQueyContract.GetAuthorizedData(dataWarehouseId, currentQueryBuilder, userWhereConditons, 0, 1).Tables[0];
                            RectangleF rect = new RectangleF(0, 0, graph.ClientPageSize.Width, textSize.Height);
                            string header = string.Format("导出时间: {0} \r\n ", DateTime.Now);
                            graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
                            graph.DrawString(header, Color.Black, rect, DevExpress.XtraPrinting.BorderSide.None);

                            float x = 0;
                            float y = textSize.Height + 1;
                            int skippedCols = 0;
                            if (!currentQueryBuilder.GroupBy && !currentQueryBuilder.Distinct)
                            {
                                skippedCols = currentQueryBuilder.CurrentTableNames.Count;
                            }
                            for (int i = skippedCols; i < dataTable.Columns.Count; i++)
                            {
                                RectangleF rect2 = new RectangleF(x, y, (graph.ClientPageSize.Width - 1) / (dataTable.Columns.Count - skippedCols), textSize.Height);

                                graph.DrawString(dataTable.Columns[i].Caption, Color.Black, rect2, DevExpress.XtraPrinting.BorderSide.All);
                                x += (graph.ClientPageSize.Width - 1) / (dataTable.Columns.Count - skippedCols);
                            }
                        };
                        exportedDataTableFormat.CreateContent = (graph, textSize) =>
                        {
                            /* 如果是分组查询，则一次获得全部记录 */
                            if (currentQueryBuilder.GroupBy || currentQueryBuilder.Distinct)
                            {
                                steps = 1;
                                pageSize = 0;
                            }
                            float y = 0;
                            for (int step = 0; step < steps; step++)
                            {
                                int pos = step * pageSize;
                                DataTable dataTable = customQueyContract.GetAuthorizedData(dataWarehouseId, currentQueryBuilder, userWhereConditons, pos, pageSize).Tables[0];                                for (int i = 0; i < dataTable.Rows.Count; i++)
                                {
                                    float x = 0;
                                    int skippedCols = 0;
                                    if (!currentQueryBuilder.GroupBy && !currentQueryBuilder.Distinct)
                                    {
                                        skippedCols = currentQueryBuilder.CurrentTableNames.Count;
                                    }
                                    for (int j = skippedCols; j < dataTable.Columns.Count; j++)
                                    {
                                        string result = GetConvertedResult(dataTable.Columns[j].ColumnName, dataTable.Rows[i][j]);
                                        RectangleF rect = new RectangleF(x, y, (graph.ClientPageSize.Width - 1) / (dataTable.Columns.Count - skippedCols), textSize.Height);
                                        graph.DrawString(result, Color.Black, rect, DevExpress.XtraPrinting.BorderSide.All);
                                        x += (graph.ClientPageSize.Width - 1) / (dataTable.Columns.Count - skippedCols);
                                    }
                                    y += textSize.Height;
                                }
                                if (progress > 0)
                                {
                                    frmProgress.Tip = string.Format("正在导出第{0}个页面的数据，请稍后...", step + 1);
                                    frmProgress.IncreaseStep();
                                    if (frmProgress != null && frmProgress.Cancel)
                                    {
                                        exit = true;
                                        break; ;
                                    }
                                }
                            }
                        };
                        exportedDataTableFormat.PrintingSystem = new PrintingSystem();
                        exportedDataTableFormat.CreateDocument();
                        exportedDataTableFormat.PrintingSystem.ExportOptions.PrintPreview.SaveMode = SaveMode.UsingSaveFileDialog;
                        exportedDataTableFormat.PrintingSystem.ExportToPdf(path);
                        if (frmProgress != null && !frmProgress.IsDisposed)
                        {
                            frmProgress.CloseFrom();
                        }
                        Cursor = Cursors.Default;
                        MessageBox.Show("PDF 文件导出成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;
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
        /// 导出数据时获得转换的结果
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetConvertedResult(string columnName, object value)
        {
            string result = string.Empty;
            if (value == null || value == DBNull.Value)
            {
                return result;
            }
            if (selectedCustomDataFieldInfos.ContainsKey(columnName))
            {
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = selectedCustomDataFieldInfos[columnName];
                DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemDataField systemDataField = (SystemDataField)extendedCustomDataFieldInfo.DataFieldId;
                        switch (systemDataField)
                        {
                            case SystemDataField.CurrentState:
                                try
                                {
                                    result = UserEnumHelper.GetEnumText((CurrentState)DataConvertionHelper.GetByte(value, 0));
                                }
                                catch
                                {
                                    result = "不存在";
                                }
                                break;

                            case SystemDataField.AuditedStatus:                                
                                try
                                {
                                    result = UserEnumHelper.GetEnumText((AuditedStatus)DataConvertionHelper.GetByte(value, 0));
                                }
                                catch
                                {
                                    result = "不存在";
                                }
                                break;

                            default:
                                result = value.ToString();
                                break;
                        }
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                            case PhysicalDataFieldType.YearAndMonthAndDay:
                            case PhysicalDataFieldType.YearAndMonth:
                            case PhysicalDataFieldType.MonthAndDay:
                            case PhysicalDataFieldType.Time:
                                if (DataConvertionHelper.GetDateTime(value, DateTime.MinValue) > DateTime.MinValue)
                                {
                                    switch (physicalDataFieldType)
                                    {

                                        case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                                            result = string.Format("{0:f}", value);
                                            break;

                                        case PhysicalDataFieldType.YearAndMonthAndDay:
                                            result = DataConvertionHelper.GetDateTime(value).ToString("yyyy-MM-dd");
                                            break;

                                        case PhysicalDataFieldType.YearAndMonth:
                                            result = string.Format("{0:Y}", value);
                                            break;

                                        case PhysicalDataFieldType.MonthAndDay:
                                            result = string.Format("{0:M}", value);
                                            break;

                                        case PhysicalDataFieldType.Time:
                                            result = string.Format("{0:T}", value);
                                            break;
                                    }
                                }
                                break;

                            default:
                                result = value.ToString();
                                break;
                        }

                        break;

                    case DataFieldProperty.LogicalDataField:
                        LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                        switch (logicalDataFieldType)
                        {
                            case LogicalDataFieldType.DateTimeExpression:
                                result = string.Format("{0:D}", value);
                                break;

                            default:
                                result = value.ToString();
                                break;
                        }
                        break;
                }
            }
            else
            {
                result = value.ToString();
            }

            return result;
        }

        /// <summary>
        /// 设置条件
        /// </summary>
        private void SetCondition()
        {
            string primaryTableName = string.Empty;
            IList<string> dataKeyNames = new List<string>(currentQueryBuilder.CurrentTableNames.Count);
            foreach (DictionaryEntry d in currentQueryBuilder.CurrentTableNames)
            {
                dataKeyNames.Add(string.Format("{0}_RecordId", d.Key));
                if (string.IsNullOrWhiteSpace(primaryTableName))
                {
                    primaryTableName = d.Key.ToString();
                }
            }
            if (userWhereConditons != null)
            {
                foreach (WhereConditon whereConditon in userWhereConditons)
                {
                    whereConditon.DataTableName = primaryTableName;
                }
            }
            if (!bckGroupBy.Checked)
            {
                devExpressGrid.DataKeyNames = dataKeyNames.ToArray();
            }
            currentQueryBuilder.ValidWhere = true;
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                SetCondition();
                int totalCount = 0;
                int pageSize = devExpressGrid.PageSize;
                /* 如果是分组查询，则不分页 */
                if (currentQueryBuilder.GroupBy || currentQueryBuilder.Distinct)
                {
                    pageSize = 0;
                }
                byte dataWarehouseId = Convert.ToByte(icmbDataWarehouse.EditValue);
                devExpressGrid.DataSource = customQueyContract.GetAuthorizedData(dataWarehouseId, currentQueryBuilder, userWhereConditons,
                        devExpressGrid.PageSize * devExpressGrid.CurrentPageIndex, pageSize, ref totalCount).Tables[0];
                if (currentQueryBuilder.GroupBy || currentQueryBuilder.Distinct)
                {
                    devExpressGrid.PageSize = totalCount;
                }
                else
                {
                    devExpressGrid.PageSize = actualPageSize;
                }
                devExpressGrid.RecordCount = totalCount;
                foreach (GridColumn gridColumn in devExpressGrid.Columns)
                {
                    if (gridColumn.VisibleIndex == 0 || !gridColumn.Visible)
                    {
                        continue;
                    }
                    try
                    {
                        if (selectedCustomDataFieldInfos.ContainsKey(gridColumn.FieldName))
                        {
                            ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = selectedCustomDataFieldInfos[gridColumn.FieldName];
                            CommonDataFieldInfo commonDataFieldInfo = new CommonDataFieldInfo(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.TableId, extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.LogicalName,
                            extendedCustomDataFieldInfo.Name, (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty, extendedCustomDataFieldInfo.DataFieldType);
                            UserControlHelper.SetColumnDisplayText(gridColumn, commonDataFieldInfo);
                        }
                    }
                    catch (Exception ex)
                    {
                        int a = 1;
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
        /// 保存文档
        /// </summary>
        private void SaveDocAttachment()
        {
            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = (IList<ExtendedCustomDataFieldInfo>)devExpressGrid.Data;
            if (devExpressGrid.FocusedColumn == null || devExpressGrid.FocusedColumn.VisibleIndex < AUDITING_GRID_VIEW_FIXED_COLUMN_COUNT || !devExpressGrid.FocusedColumn.Visible)
            {
                return;
            }
            try
            {
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = selectedCustomDataFieldInfos[devExpressGrid.FocusedColumn.FieldName];
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
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 设置当前选择列是否可以编辑
        /// </summary>
        /// <param name="allowedEdit"></param>
        private void SetFocusedColumnEditable(bool allowedEdit)
        {
            if (devExpressGrid.FocusedRowHandle >= 0 && devExpressGrid.FocusedColumn != null
                && !devExpressGrid.FocusedColumn.Name.Equals(devExpressGrid.CheckboxColumnName))
            {
                devExpressGrid.FocusedColumn.OptionsColumn.AllowEdit = allowedEdit;
                devExpressGrid.FocusedColumn.OptionsColumn.ReadOnly = !allowedEdit;
            }
        }

        /// <summary>
        /// 显示图片附件
        /// </summary>
        private void ShowPicAttachment()
        {
            IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = (IList<ExtendedCustomDataFieldInfo>)devExpressGrid.Data;
            if (devExpressGrid.FocusedColumn == null || devExpressGrid.FocusedColumn.VisibleIndex < AUDITING_GRID_VIEW_FIXED_COLUMN_COUNT || !devExpressGrid.FocusedColumn.Visible)
            {
                return;
            }            
            ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = selectedCustomDataFieldInfos[devExpressGrid.FocusedColumn.FieldName];
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
                        pictureEdit.EditValue = data;
                        int x = (Cursor.Position.X - pictureEdit.Width) < 0 ? 0 : Cursor.Position.X - pictureEdit.Width;
                        int y = (Cursor.Position.Y - pictureEdit.Height) < 0 ? 0 : Cursor.Position.Y - pictureEdit.Height;
                        Point point = new Point(x, y);
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

        #endregion

    }
}
