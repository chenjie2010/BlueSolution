using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class DatabaseForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomDatabaseContract customDatabaseContract;
        private readonly ICustomCategoryContract customCategoryContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomExpressionContract customExpressionContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;

        #endregion

        #region 私有常量

        private const int MAX_STRING_LENG_IN_DATA_FIELD = 4000;
        private const int MAX_ENCRYPTED_STRING_LENG_IN_DATA_FIELD = 1024;

        #endregion

        #region  私有变量

        private readonly TreeLayerModule treeLayerModule;
        private readonly TreeLayerModule databaseModule;
        private readonly TreeLayerModule categoryModule;
        private readonly DataTableModule dataTableModule;
        private readonly DataFieldModule dataFieldModule;

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public DatabaseForm()
        {
            InitializeComponent();

            customDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            customCategoryContract = BusinessChannelFactory.CreateCustomCategoryContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customExpressionContract = BusinessChannelFactory.CreateCustomExpressionContract();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();

            IList<UserControl> userControls = new List<UserControl>();
            treeLayerModule = new TreeLayerModule() { LayerName = "仓库名称：", LayerCodeName = "仓库编码：" };
            userControls.Add(treeLayerModule);

            databaseModule = new TreeLayerModule() { LayerName = "数据库名称：", LayerCodeName = "数据库编码：", CommonNodeContract = customDatabaseContract };
            userControls.Add(databaseModule);

            categoryModule = new TreeLayerModule() { LayerName = "分类名称：", LayerCodeName = "分类编码：", CommonNodeContract = customCategoryContract };
            userControls.Add(categoryModule);

            dataTableModule = new DataTableModule() { CustomTableContract = customTableContract };
            userControls.Add(dataTableModule);

            dataFieldModule = new DataFieldModule() { CustomTableContract = customTableContract, CustomDataFieldContract = customDataFieldContract };
            userControls.Add(dataFieldModule);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            AllowChildNodesDeleted = true;
            MaxLevel = 4;  /*  允许最大层次 */
            Tip = "树形层：(1) 数据仓库 (2) 数据库 (3) 分类 (4)数据表 (5)字段 ";
            NullValuePrompt = "请输入数据表的名称查询";
            AddControls(userControls);
        }

        private void DatabaseForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            if (CurrentQueriedState)
            {
                CommonNodeContract = customTableContract;
                /* 查询结果只显示表 */
                TreeNodeShow = dataTableModule;
                treeLayerModule.Visible = false;
                databaseModule.Visible = false;
                categoryModule.Visible = false;
                dataTableModule.Visible = true;
                dataFieldModule.Visible = false;
                ExchangeItemEnabled = false;
            }
            else
            {
                DatabaseNodeType nodeType = GetNodeType(e.Node.Level);
                switch (nodeType)
                {
                    case DatabaseNodeType.DataWarehouse:
                    case DatabaseNodeType.Database:
                        case DatabaseNodeType.Table:
                        ExchangeItemEnabled = true;
                        SettingEnabled = false;
                        SettingCaption = "设置(&S)";
                        break;

                    case DatabaseNodeType.Category:
                        ExchangeItemEnabled = true;
                        SettingEnabled = true;
                        SettingCaption = "复制数据表(&C)";
                        break;

                    case DatabaseNodeType.DataField:
                        ExchangeItemEnabled = false;
                        SettingEnabled = true;
                        SettingCaption = "设置联动字段(&S)";
                        break;
                }                
                SetCommonNodeContract(nodeType);
                SetParametersOnPanel(nodeType);
            }
        }

        /// <summary>
        /// 查询之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = customTableContract;
        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
        {
            DatabaseNodeType databaseNode = GetNodeType(e.TreeNode.Level + 1);
            SetCommonNodeContract(databaseNode);
            SetParametersOnPanel(databaseNode);
            if (databaseNode == DatabaseNodeType.DataField)
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                dataFieldModule.TableId = commonNode.NodeId;                
            }
        }

        /// <summary>
        ///  字段编辑时不能改变字段属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnEditClick(object sender, TreeNodeItemClickEventArgs e)
        {
            DatabaseNodeType databaseNode = GetNodeType(e.TreeNode.Level);
            if (databaseNode == DatabaseNodeType.DataField)
            {
                CommonNode commonNode = e.TreeNode.Parent.Tag as CommonNode;
                dataFieldModule.TableId = commonNode.NodeId;
                dataFieldModule.DataFieldPropertyOnReadOnly = true;
            }
        }

        /// <summary>
        /// 删除之前检查条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnBeforeDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            DatabaseNodeType databaseNode = GetNodeType(e.TreeNode.Level);
            if (databaseNode == DatabaseNodeType.Table)
            {
                AllowChildNodesDeleted = true;
            }
            else
            {
                AllowChildNodesDeleted = false;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                DatabaseNodeType databaseNode = GetNodeType(e.TreeNode.Level);
                string warning = string.Empty;
                int count = 0;
                switch (databaseNode)
                {
                    case DatabaseNodeType.DataWarehouse:
                        warning = "数据仓库不能删除。";
                        break;

                    case DatabaseNodeType.Database:
                        count = customCategoryContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            warning = string.Format("该数据库[{0}]下共有{1}个数据表分类，请先删除这些数据表分类。", commonNode.NodeName, count);
                        }
                        else
                        {
                            customDatabaseContract.Delete(commonNode.NodeId);
                        }
                        break;

                    case DatabaseNodeType.Category:
                        count = customTableContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            warning = string.Format("该分类[{0}]下共有{1}个数据表，请先删除这些数据表。", commonNode.NodeName, count);
                        }
                        else
                        {
                            customCategoryContract.Delete(commonNode.NodeId);
                        }
                        break;

                    case DatabaseNodeType.Table:
                        count = customDataFieldContract.GetRelatedDataFieldCountByTableId(commonNode.NodeId);
                        if (count > 0)
                        {
                            warning = string.Format("该数据表[{0}]下共有{1}个字段与该数据表的字段关联，请先解除关联关系。。", commonNode.NodeName, count);
                        }
                        else
                        {
                            customTableContract.Delete(commonNode.NodeId);
                        }
                        break;

                    case DatabaseNodeType.DataField:
                        count = customDataFieldContract.GetRelatedDataFieldCount(commonNode.NodeId);
                        if (count > 0)
                        {
                            warning = string.Format("该字段[{0}]下共有{1}个字段与其关联，请先解除关联关系。。", commonNode.NodeName, count);
                        }
                        else
                        {
                            customDataFieldContract.Delete(commonNode.NodeId);
                        }
                        break;
                }
                if (string.IsNullOrWhiteSpace(warning))
                {
                    DeleteNode();
                    Cursor = Cursors.Default;
                    MessageBox.Show(string.Format("所选择的节点[[{0}]删除成功。", commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            try
            {
                bool result = false;
                string verifyResult = string.Empty;
                DatabaseNodeType databaseNode = GetNodeType(e.TreeNode.Level);
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                Cursor = Cursors.WaitCursor;
                switch (e.EditState)
                {
                    case EditState.Add:
                        result = AddTreeNode(commonNode, databaseNode, ref verifyResult);
                        break;

                    case EditState.Edit:
                        result = EditTreeNode(commonNode, databaseNode, ref verifyResult);
                        break;

                    default:
                        break;
                }
                Cursor = Cursors.Default;
                if (!result && !string.IsNullOrWhiteSpace(verifyResult))
                {
                    e.Cancel = true;
                    MessageBox.Show(verifyResult, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception exception)
            {
                e.Cancel = true;
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
        {
            if (!CurrentQueriedState)
            {
                SetCommonNodeContract(GetNodeType(e.TreeNode.Level));
                SetParametersOnPanel(GetNodeType(e.TreeNode.Level));
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                DatabaseNodeType nodeType = GetNodeType(e.TreeNode.Level);
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                if (nodeType == DatabaseNodeType.DataField)
                {
                    CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModelInfo(commonNode.NodeId);
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                    if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        CommonListItemsForm frmCommonListItems = new CommonListItemsForm();
                        frmCommonListItems.Text = "字段选择";
                        frmCommonListItems.ToolTip = "字段列表";
                        frmCommonListItems.CreateItmes = delegate (ListBoxControl lstItems)
                        {
                            DataFieldItemsForm frmDataFieldItems = new DataFieldItemsForm();
                            frmDataFieldItems.DataFieldShowMode = DataFieldShowMode.DataWarehouse;
                            frmDataFieldItems.DataWarehouseId = customTableContract.GetDataWarehouseId(customDataFieldInfo.TableId);
                            frmDataFieldItems.DataFieldFilter = DataFieldFilter.Custom;
                            frmDataFieldItems.DataFieldType = customDataFieldInfo.DataFieldType;
                            frmDataFieldItems.NodeSelected = delegate (CommonNode node)
                            {
                                bool find = false;
                                foreach (object obj in lstItems.Items)
                                {
                                    CommonNode cn = (CommonNode)obj;
                                    if (cn.NodeId == node.NodeId)
                                    {
                                        find = true;
                                        break;
                                    }
                                }
                                if (!find)
                                {
                                    node.NodeName = customDataFieldContract.GetFullLogicalName(node.NodeId);                                    
                                    lstItems.Items.Add(node);
                                }
                            };
                            frmDataFieldItems.ShowDialog();
                        };
                        frmCommonListItems.GetItems = delegate (IList<CommonNode> nodes)
                        {
                            IList<DataFieldRelationshipInfo> dataFieldRelationshipInfos = new List<DataFieldRelationshipInfo>();
                            int sorting = 1;
                            foreach (var obj in nodes)
                            {
                                dataFieldRelationshipInfos.Add(new DataFieldRelationshipInfo(commonNode.NodeId, obj.NodeId, sorting++));
                            }
                            customDataFieldContract.UpdateDataFields(commonNode.NodeId, dataFieldRelationshipInfos);
                        };
                        IList<CommonNode> commonNodes = customDataFieldContract.GetRelationDataFieldsWithFullName(commonNode.NodeId);
                        //foreach (var node in commonNodes)
                        //{
                        //    node.NodeName = customDataFieldContract.GetFullLogicalName(node.NodeId);
                        //}
                        frmCommonListItems.LoadItems(commonNodes);
                        frmCommonListItems.ShowDialog();
                    }
                }
                else if (nodeType == DatabaseNodeType.Category)
                {
                    DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
                    frmDataTableItems.TableFilter = TableFilter.All;
                    frmDataTableItems.Text = "请选择数据表";
                    frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
                    frmDataTableItems.NodeSelected = delegate (CommonNode node)
                    {
                        if (node != null && MessageBox.Show(string.Format("确认将数据表{0}复制到数据表分类({1})中？", node.NodeName, commonNode.NodeName),
                            "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            Cursor = Cursors.WaitCursor;
                            try
                            {
                                ShowProgressPanel();
                                decimal customTableId = customTableContract.CopyTable(commonNode.NodeId, node.NodeId);
                                CommonNode newCommonNode = customTableContract.GetCommonNode(customTableId);
                                TreeNode treeNode = new TreeNode()
                                {
                                    Text = newCommonNode.NodeName,
                                    Tag = newCommonNode
                                };
                                TreeNode childNode = new TreeNode { Text = string.Empty, Tag = new CommonNode() };
                                treeNode.Nodes.Add(childNode);
                                AddNode(treeNode);
                                HideProgressPanel();
                            }
                            catch (Exception ex)
                            {
                                HideProgressPanel();
                                throw ex;
                            }
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("数据表{0}复制到数据表分类({1})中完成。", node.NodeName, commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    };
                    frmDataTableItems.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 导入导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DatabaseForm_OnExchangeClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            DatabaseExchanged databaseExchanged = new DatabaseExchanged("数据库数据", commonNode.NodeId, commonNode.NodeCode,
                "TableCode ASC", customDatabaseContract, customCategoryContract, customTableContract, customDataFieldContract,
                customEnumContract, associatedDataFieldContract);
            DataExchangeModeForm frmDataExchangeMode = new DataExchangeModeForm()
            {
                Tip = string.Format("当前选择节点：{0}；数据库结构的导入与选择的节点无关。", commonNode.NodeName),
                DataExportedInterface = databaseExchanged,
                RefreshForm = () =>
                {
                    InitFirstLevelNodes();
                }
            };
            frmDataExchangeMode.ShowDialog();
        }
        #endregion

        #region 私有方法

        /// <summary>
        /// 增加节点，包括数据库、分组、表、字段等节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="databaseNode"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddTreeNode(CommonNode commonNode, DatabaseNodeType databaseNode, ref string verifyResult)
        {
            bool result = false;
            decimal nodeId = 0;
            string name = string.Empty;
            string value = string.Empty;
            string tip = string.Empty;

            switch (databaseNode)
            {
                case DatabaseNodeType.DataWarehouse:
                    ExtendedCommonNode extendedCommonNode = databaseModule.GetModelInfo();
                    CustomDatabaseInfo customDatabaseInfo = new CustomDatabaseInfo()
                    {
                        DatabaseName = extendedCommonNode.NodeName,
                        DatabaseCode = extendedCommonNode.NodeCode,
                        DataWarehouseId = Convert.ToByte(commonNode.NodeId),
                        Notes = extendedCommonNode.Notes
                    };
                    result = ValidationHelper.Validate<CustomDatabaseInfo>(customDatabaseInfo, out verifyResult);
                    if (result)
                    {
                        if (customDatabaseContract.IsExistIdenticalName(commonNode.NodeId, customDatabaseInfo.DatabaseName))
                        {
                            verifyResult = string.Format("同一数据仓库中的数据库的名称不允许重复, 数据库名称[{0}]已存在", customDatabaseInfo.DatabaseName);
                            return false;
                        }
                        nodeId = customDatabaseContract.Insert(customDatabaseInfo);
                        name = customDatabaseInfo.DatabaseName;
                        value = customDatabaseInfo.DatabaseCode;
                    }
                    break;

                case DatabaseNodeType.Database:
                    ExtendedCommonNode categoryCommonNode = categoryModule.GetModelInfo();
                    CustomCategoryInfo customCategoryInfo = new CustomCategoryInfo()
                    {
                        CategoryName = categoryCommonNode.NodeName,
                        CategoryCode = categoryCommonNode.NodeCode,
                        Notes = categoryCommonNode.Notes,
                        DatabaseId = commonNode.NodeId
                    };
                    result = ValidationHelper.Validate<CustomCategoryInfo>(customCategoryInfo, out verifyResult);
                    if (result)
                    {
                        if (customCategoryContract.IsExistIdenticalName(commonNode.NodeId, customCategoryInfo.CategoryName))
                        {
                            verifyResult = string.Format("同一数据库中分类名称不允许重复, 分类名称[{0}]已存在", customCategoryInfo.CategoryName);
                            return false;
                        }
                        nodeId = customCategoryContract.Insert(customCategoryInfo);
                        name = customCategoryInfo.CategoryName;
                        value = customCategoryInfo.CategoryCode;
                    }
                    break;

                case DatabaseNodeType.Category:
                    result = dataTableModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomTableInfo customTableInfo = dataTableModule.GetModelInfo();
                        customTableInfo.CategoryId = commonNode.NodeId;
                        if (customTableContract.IsExistIdenticalName(commonNode.NodeId, customTableInfo.LogicalName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一数据库中的表的名称不允许重复, 表的名称[{0}]已存在。", customTableInfo.LogicalName);
                            return false;
                        }
                        nodeId = customTableContract.Insert(customTableInfo);
                        name = customTableInfo.LogicalName;
                        value = customTableInfo.TableCode;
                    }            
                    break;
                    
                case DatabaseNodeType.Table:
                    CustomDataFieldInfo customDataFieldInfo = dataFieldModule.GetModelInfo();
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)Convert.ToByte(customDataFieldInfo.DataFieldProperty);
                    if (dataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
                    {
                        verifyResult = "不能创建系统字段。";
                        return false;
                    }
                    result = ValidationHelper.Validate<CustomDataFieldInfo>(customDataFieldInfo, out verifyResult);
                    if (result)
                    {
                        /* 1. 同一个表内字段名称不能重复 */
                        if (customDataFieldContract.IsExistIdenticalName(commonNode.NodeId, customDataFieldInfo.LogicalName))
                        {
                            verifyResult = string.Format("同一表中的字段名称不允许重复, 字段名[{0}]已存在。", customDataFieldInfo.LogicalName);
                            return false;
                        }
                        if (DataConvertionHelper.IsNullValue(customDataFieldInfo.DataFieldType))
                        {
                            verifyResult = "字段类型不能为空。";
                            return false;
                        }
                        bool withExpression = false;
                        switch (dataFieldProperty)
                        {
                            case DataFieldProperty.PhysicalDataField:
                                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                                if (!CheckPhysicalDataFieldValidity(customDataFieldInfo, physicalDataFieldType, ref verifyResult))
                                {
                                    return false;
                                }
                                break;

                            case DataFieldProperty.LogicalDataField:
                                LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)customDataFieldInfo.DataFieldType;
                                if (!CheckLogicalDataFieldValidity(customDataFieldInfo, logicalDataFieldType, ref verifyResult))
                                {
                                    return false;
                                }
                                if (logicalDataFieldType == LogicalDataFieldType.DateTimeExpression || logicalDataFieldType == LogicalDataFieldType.StringExpression
                                    || logicalDataFieldType == LogicalDataFieldType.DigitExpression || logicalDataFieldType == LogicalDataFieldType.TwoDimCode)
                                {
                                    withExpression = true;
                                }
                                break;
                        }
                        if (withExpression)
                        {
                            nodeId = customDataFieldContract.Insert(customDataFieldInfo, dataFieldModule.CustomExpressionInfos);
                        }
                        else
                        {
                            nodeId = customDataFieldContract.Insert(customDataFieldInfo);
                        }
                        name = customDataFieldInfo.LogicalName;
                        value = customDataFieldInfo.DataFieldCode;
                    }
                    break;
            }
            if (result)
            {
                TreeNode tn = new TreeNode
                {
                    Text = name,
                    Tag = new CommonNode(nodeId, commonNode.NodeId, name, value, true)
                };
                if (databaseNode == DatabaseNodeType.Category)
                {
                    TreeNode tnSystemFieldRoot = new TreeNode { Text = UserEnumHelper.GetEnumText(DataFieldProperty.SystemPhysicalDataField) };
                    tnSystemFieldRoot.Tag = DataFieldHelper.GetDataFieldPropertyCommonNode(DataFieldProperty.SystemPhysicalDataField);
                    tn.Nodes.Add(tnSystemFieldRoot);
                    IList<CommonNode> commonNodes = customDataFieldContract.GetChildNodes(0);
                    LoadPartialNodes(tnSystemFieldRoot, commonNodes);
                }
                AddNode(tn);
                Cursor = Cursors.Default;
            }

            return result;
        }

        /// <summary>
        /// 编辑节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="databaseNode"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool EditTreeNode(CommonNode commonNode, DatabaseNodeType databaseNode, ref string verifyResult)
        {
            bool result = false;
            string name = string.Empty;

            //针对查询后修改数据节点的处理
            if (databaseNode == DatabaseNodeType.DataWarehouse && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId))
            {
                databaseNode = DatabaseNodeType.Table;
            }

            switch (databaseNode)
            {
                case DatabaseNodeType.Database:
                    ExtendedCommonNode extendedCommonNode = databaseModule.GetModelInfo();
                    CustomDatabaseInfo oldCustomDatabaseInfo = customDatabaseContract.GetModelInfo(commonNode.NodeId);
                    CustomDatabaseInfo customDatabaseInfo = new CustomDatabaseInfo()
                    {
                        DatabaseId = commonNode.NodeId,
                        DatabaseName = extendedCommonNode.NodeName,
                        DatabaseCode = extendedCommonNode.NodeCode,
                        DataWarehouseId = Convert.ToByte(commonNode.NodeId),
                        Notes = extendedCommonNode.Notes
                    };
                    result = ValidationHelper.Validate<CustomDatabaseInfo>(customDatabaseInfo, out verifyResult);
                    if (result)
                    {
                        if (!customDatabaseInfo.DatabaseName.Equals(oldCustomDatabaseInfo.DatabaseName) && customDatabaseContract.IsExistIdenticalName(commonNode.NodeId, customDatabaseInfo.DatabaseName))
                        {
                            verifyResult = string.Format("同一数据仓库中的数据库的名称不允许重复, 数据库名称[{0}]已存在。", customDatabaseInfo.DatabaseName);
                            return false;
                        }
                        customDatabaseContract.Update(customDatabaseInfo);
                        name = customDatabaseInfo.DatabaseName;
                    }
                    break;

                case DatabaseNodeType.Category:
                    ExtendedCommonNode categoryCommonNode = categoryModule.GetModelInfo();
                    CustomCategoryInfo oldCustomCategoryInfo = customCategoryContract.GetModelInfo(commonNode.NodeId);
                    CustomCategoryInfo customCategoryInfo = new CustomCategoryInfo()
                    {
                        CategoryId = commonNode.NodeId,
                        CategoryName = categoryCommonNode.NodeName,
                        CategoryCode = categoryCommonNode.NodeCode,
                        Notes = categoryCommonNode.Notes,
                        DatabaseId = oldCustomCategoryInfo.DatabaseId
                    };
                    result = ValidationHelper.Validate<CustomCategoryInfo>(customCategoryInfo, out verifyResult);
                    if (result)
                    {
                        if (!customCategoryInfo.CategoryName.Equals(oldCustomCategoryInfo.CategoryName) && customCategoryContract.IsExistIdenticalName(commonNode.NodeId, customCategoryInfo.CategoryName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一数据库中分类名称不允许重复, 分类名称[{0}]已存在。", customCategoryInfo.CategoryName);
                            return false;
                        }
                        customCategoryContract.Update(customCategoryInfo);
                        name = customCategoryInfo.CategoryName;
                    }
                    break;

                case DatabaseNodeType.Table:
                    result = dataTableModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomTableInfo customTableInfo = dataTableModule.GetModelInfo();
                        CustomTableInfo oldCustomTableInfo = customTableContract.GetModelInfo(commonNode.NodeId);
                        customTableInfo.TableId = oldCustomTableInfo.TableId;
                        customTableInfo.CategoryId = oldCustomTableInfo.CategoryId;
                        if (!customTableInfo.LogicalName.Equals(oldCustomTableInfo.LogicalName) && customTableContract.IsExistIdenticalName(commonNode.NodeId, customTableInfo.LogicalName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一数据库中的表的名称不允许重复, 表的名称[{0}]已存在。", customTableInfo.LogicalName);
                            return false;
                        }
                        customTableContract.Update(customTableInfo);
                        name = customTableInfo.LogicalName;
                    }
                    break;

                case DatabaseNodeType.DataField:
                    CustomDataFieldInfo customDataFieldInfo = dataFieldModule.GetModelInfo();
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)Convert.ToByte(customDataFieldInfo.DataFieldProperty);
                    if (dataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
                    {
                        verifyResult = "不能创建系统字段。";
                        return false;
                    }
                    CustomDataFieldInfo oldCustomDataFieldInfo = customDataFieldContract.GetModelInfo(commonNode.NodeId);
                    customDataFieldInfo.DataFieldId = oldCustomDataFieldInfo.DataFieldId;
                    result = ValidationHelper.Validate<CustomDataFieldInfo>(customDataFieldInfo, out verifyResult);
                    if (result)
                    {
                        if (customDataFieldInfo.DataFieldProperty != oldCustomDataFieldInfo.DataFieldProperty)
                        {
                            verifyResult = "字段属性不能更改。";
                            return false;
                        }
                        if (!customDataFieldInfo.LogicalName.Equals(oldCustomDataFieldInfo.LogicalName) && customDataFieldContract.IsExistIdenticalName(oldCustomDataFieldInfo.TableId, customDataFieldInfo.LogicalName))
                        {
                            verifyResult = string.Format("同一数据表中的字段逻辑名称不允许重复, 字段的名称[{0}]已存在。", customDataFieldInfo.LogicalName);
                            return false;
                        }
                        if (DataConvertionHelper.IsNullValue(customDataFieldInfo.DataFieldType))
                        {
                            verifyResult = "字段类型不能为空。";
                            return false;
                        }
                        bool withExpression = false;                      
                        switch (dataFieldProperty)
                        {
                            case DataFieldProperty.PhysicalDataField:
                                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                                if (!CheckPhysicalDataFieldValidity(customDataFieldInfo, physicalDataFieldType, ref verifyResult))
                                {
                                    return false;
                                }
                                break;

                            case DataFieldProperty.LogicalDataField:
                                LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)customDataFieldInfo.DataFieldType;
                                if (!CheckLogicalDataFieldValidity(customDataFieldInfo, logicalDataFieldType, ref verifyResult))
                                {
                                    return false;
                                }
                                if (logicalDataFieldType == LogicalDataFieldType.DateTimeExpression || logicalDataFieldType == LogicalDataFieldType.StringExpression
                                    || logicalDataFieldType == LogicalDataFieldType.DigitExpression || logicalDataFieldType == LogicalDataFieldType.TwoDimCode)
                                {
                                    withExpression = true;
                                }
                                break;
                        }
                        if (withExpression)
                        {
                            customDataFieldContract.Update(customDataFieldInfo, dataFieldModule.CustomExpressionInfos);
                        }
                        else
                        {
                            customDataFieldContract.Update(customDataFieldInfo);
                        }
                        name = customDataFieldInfo.LogicalName;
                    }
                    break;
            }
            if (result && !commonNode.NodeName.Equals(name))
            {
                ModifyNode(name);
            }
            Cursor = Cursors.Default;
            return result;
        }

        /// <summary>
        /// 检查逻辑字段的属性
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool CheckLogicalDataFieldValidity(CustomDataFieldInfo customDataFieldInfo, LogicalDataFieldType logicalDataFieldType, ref string verifyResult)
        {
            bool result = true;

            switch (logicalDataFieldType)
            {
                case LogicalDataFieldType.StringExpression:
                case LogicalDataFieldType.DateTimeExpression:
                case LogicalDataFieldType.DigitExpression:
                    if (string.IsNullOrWhiteSpace(customDataFieldInfo.ExpressionText))
                    {
                        verifyResult = "表达式类型不能为空。";
                        return false;
                    }
                    break;
                    
                case LogicalDataFieldType.TwoDimCode:
                    if (string.IsNullOrWhiteSpace(customDataFieldInfo.ExpressionText))
                    {
                        verifyResult = "二维码对应的条码字段不能为空。";
                        return false;
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// 检查物理字段的属性
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool CheckPhysicalDataFieldValidity(CustomDataFieldInfo customDataFieldInfo, PhysicalDataFieldType physicalDataFieldType, ref string verifyResult)
        {
            bool result = true;

            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.Decimal:
                    if (customDataFieldInfo.DataFieldLength <= 0 || customDataFieldInfo.DataFieldLength > AppSettingHelper.DefaultDecimalDigitMaxLength)
                    {
                        verifyResult = string.Format("字符长度范围在[1, {0}]之间。", AppSettingHelper.DefaultDecimalDigitMaxLength);
                        return false;
                    }
                    break;

                case PhysicalDataFieldType.ArbitraryString:
                case PhysicalDataFieldType.ExtendedArbitraryString:
                case PhysicalDataFieldType.NumeralString:
                case PhysicalDataFieldType.CharString:
                case PhysicalDataFieldType.MixedString:
                    if (customDataFieldInfo.DataFieldLength <= 0 || customDataFieldInfo.DataFieldLength > MAX_STRING_LENG_IN_DATA_FIELD)
                    {
                        verifyResult = string.Format("字符长度范围在[1, {0}]之间。", MAX_STRING_LENG_IN_DATA_FIELD);
                        return false;
                    }                    
                    break;

                case PhysicalDataFieldType.EncryptedString:
                    if (customDataFieldInfo.DataFieldLength <= 0 || customDataFieldInfo.DataFieldLength > MAX_ENCRYPTED_STRING_LENG_IN_DATA_FIELD)
                    {
                        verifyResult = string.Format("字符长度范围在[1, {0}]之间。", MAX_ENCRYPTED_STRING_LENG_IN_DATA_FIELD);
                        return false;
                    }                    
                    break;

                case PhysicalDataFieldType.DropdownListEnum:
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.TreeViewEnumValue:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                case PhysicalDataFieldType.CscadeEnum:
                case PhysicalDataFieldType.MultiSelectedEnum:
                    if (DataConvertionHelper.IsNullValue(customDataFieldInfo.EnumId))
                    {
                        verifyResult = "枚举类型不能为空。";
                        return false;
                    }
                    break;

                case PhysicalDataFieldType.Association:
                case PhysicalDataFieldType.PrimaryAssociation:
                    if (DataConvertionHelper.IsNullValue(customDataFieldInfo.AssociatedDataFieldId))
                    {
                        verifyResult = "关联名称不能为空。";
                        return false;
                    }                    
                    break;

                case PhysicalDataFieldType.SecondaryAssociation:
                    if (DataConvertionHelper.IsNullValue(customDataFieldInfo.ParentDataFieldId))
                    {
                        verifyResult = "关联字段不能为空。";
                        return false;
                    }
                    if (DataConvertionHelper.IsNullValue(customDataFieldInfo.AssociatedDataFieldId))
                    {
                        verifyResult = "关联名称不能为空。";
                        return false;
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// 获得数据库节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private DatabaseNodeType GetNodeType(int level)
        {
            DatabaseNodeType nodeType = DatabaseNodeType.DataWarehouse;
            /* 第一层为数据仓库 第二层为数据库，第三层为分类，第四层为数据表，第五层为字段 */
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

                case 4:
                case 5:
                    nodeType = DatabaseNodeType.DataField;
                    break;
            }

            return nodeType;
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="databaseNode"></param>
        private void SetCommonNodeContract(DatabaseNodeType databaseNode)
        {
            /* 第一层为数据仓库 第二层为数据库，第三层为分组，第四层为数据表，第五、六层为字段 */
            switch (databaseNode)
            {
                case DatabaseNodeType.DataWarehouse:
                    CommonNodeContract = null;
                    break;

                case DatabaseNodeType.Database:
                    CommonNodeContract = customDatabaseContract;
                    break;

                case DatabaseNodeType.Category:
                    CommonNodeContract = customCategoryContract;
                    break;

                case DatabaseNodeType.Table:
                    CommonNodeContract = customTableContract;
                    break;

                case DatabaseNodeType.DataField:
                    CommonNodeContract = customDataFieldContract;
                    break;
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="databaseNode"></param>
        private void SetParametersOnPanel(DatabaseNodeType databaseNode)
        {
            /* 第一层为数据仓库 第二层为数据库，第三、四层为分组，第三、四层为分组，第五层为数据表，第六、七层为字段 */
            switch (databaseNode)
            {
                case DatabaseNodeType.DataWarehouse:
                    TreeNodeShow = treeLayerModule;
                    treeLayerModule.Visible = true;
                    databaseModule.Visible = false;
                    categoryModule.Visible = false;
                    dataTableModule.Visible = false;
                    dataFieldModule.Visible = false;
                    break;

                case DatabaseNodeType.Database:
                    TreeNodeShow = databaseModule;
                    treeLayerModule.Visible = false;
                    databaseModule.Visible = true;
                    categoryModule.Visible = false;
                    dataTableModule.Visible = false;
                    dataFieldModule.Visible = false;
                    break;

                case DatabaseNodeType.Category:
                    TreeNodeShow = categoryModule;
                    treeLayerModule.Visible = false;
                    databaseModule.Visible = false;
                    categoryModule.Visible = true;
                    dataTableModule.Visible = false;
                    dataFieldModule.Visible = false;
                    break;

                case DatabaseNodeType.Table:
                    TreeNodeShow = dataTableModule;
                    treeLayerModule.Visible = false;
                    databaseModule.Visible = false;
                    categoryModule.Visible = false;
                    dataTableModule.Visible = true;
                    dataFieldModule.Visible = false;
                    break;

                case DatabaseNodeType.DataField:
                    TreeNodeShow = dataFieldModule;
                    treeLayerModule.Visible = false;
                    databaseModule.Visible = false;
                    categoryModule.Visible = false;
                    dataTableModule.Visible = false;
                    dataFieldModule.Visible = true;
                    break;
            }
        }

        #endregion

        #region 重写虚拟化方法



        /// <summary>
        /// 通过节点的索引值来对节点的位置进行调整
        /// </summary>
        /// <param name="index">节点索引值</param>
        protected override void MoveNodeByIndex(int index)
        {
            TreeNode tnSelected = TreeViewInLayer.SelectedNode;
            DatabaseNodeType nodeType = GetNodeType(tnSelected.Level);
            if (nodeType == DatabaseNodeType.DataField)
            {
                TreeNode tnParent = tnSelected.Parent;
                tnSelected.Remove();
                if (index == 0 && tnParent.Nodes[0].Nodes.Count > 0 && tnSelected.Nodes.Count == 0)
                {
                    index++;
                }
                tnParent.Nodes.Insert(index, tnSelected);
                TreeViewInLayer.Focus();
                TreeViewInLayer.SelectedNode = tnSelected;
            }
            else
            {
                base.MoveNodeByIndex(index);
            }           
        }

        /// <summary>
        /// 初始化属性节点
        /// </summary>
        protected override void InitFirstLevelNodes()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            IList<EnumItem> dataWarehouses = UserEnumHelper.GetEnumItems(typeof(DataWarehouse));
            string[] descriptions = UserEnumHelper.GetDescription(typeof(DataWarehouse)).Split('|');
            foreach (EnumItem enumItem in dataWarehouses)
            {
                commonNodes.Add(new CommonNode(enumItem.Value, decimal.MinValue, enumItem.Text, descriptions[enumItem.Value], false));
            }
            InitTreeNodes(commonNodes);
        }

        /// <summary>
        /// 移动树形节点
        /// </summary>
        /// <param name="movedTreeNode"></param>
        /// <param name="targeNode"></param>
        /// <returns></returns>
        protected override bool IsAllowedDrag(TreeNode movedTreeNode, TreeNode targeNode)
        {
            DatabaseNodeType movedTreeNodeType = GetNodeType(movedTreeNode.Level);
            DatabaseNodeType targeNodeTreeNodeType = GetNodeType(targeNode.Level);
            if (movedTreeNodeType == DatabaseNodeType.Table && targeNodeTreeNodeType == DatabaseNodeType.Category)
            {
                CommonNode movedCommonNode = movedTreeNode.Tag as CommonNode;
                CommonNode targetNodeCommonNode = targeNode.Tag as CommonNode;
                decimal movedDatabaseId = customTableContract.GetDatabaseId(movedCommonNode.NodeId);
                decimal targetDatabaseId = customCategoryContract.GetDatabaseId(targetNodeCommonNode.NodeId);
                if (movedDatabaseId == targetDatabaseId)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 该层节点在加载时是否需要使用节点类型条件
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected override bool ContainsNodeType(int level)
        {
            return false;
        }


        #endregion
    }
}
