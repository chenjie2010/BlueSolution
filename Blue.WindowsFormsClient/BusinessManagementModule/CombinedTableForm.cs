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
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary.EventArgument;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class CombinedTableForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICombinedTableContract combinedTableContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomFormContract customFormContract;
        private readonly IDataAuditingContract dataAuditingContract;

        #endregion

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly CombinedTableModule combinedTableModule;

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>

        public CombinedTableForm()
        {
            InitializeComponent();

            SettingCaption = "组合字段设置";
            SettingEnabled = false;

            ExchangeItemVisible = DevExpress.XtraBars.BarItemVisibility.Never;

            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            combinedTableContract = BusinessChannelFactory.CreateCombinedTableContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customFormContract = BusinessChannelFactory.CreateCustomFormContract();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();

            IList<UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { LayerName = "分组名称：", LayerCodeName = "分组编码：", CommonNodeContract = customGroupContract };
            userControls.Add(groupModule);

            combinedTableModule = new CombinedTableModule() { CombinedTableContract = combinedTableContract, CustomTableContract = customTableContract };
            userControls.Add(combinedTableModule);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 3;  /*  允许最大层次 */
            Tip = "提示：组合表必须是主表或者主从表。";
            NullValuePrompt = "请输入组合表名称查询";
            AddControls(userControls);
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombinedTableForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombinedTableForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            CombinedNodeType combinedNodeType = GetNodeType(e.Node.Level);
            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                if (commonNode.NodeId > 0)
                {
                    groupModule.Visible = false;
                    combinedTableModule.Visible = true;
                    TreeNodeShow = combinedTableModule;
                }
                else
                {
                    groupModule.Visible = true;
                    combinedTableModule.Visible = false;
                    TreeNodeShow = groupModule;
                }                
            }
            else
            {
                SetCommonNodeContract(combinedNodeType);
                SetParametersOnPanel(combinedNodeType);
            }
            if (combinedNodeType == CombinedNodeType.Leaf)
            {
                SettingEnabled = true;
            }
            else
            {
                SettingEnabled = false;
            }
        }

        /// <summary>
        /// 查询之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombinedTableForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = combinedTableContract;
        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombinedTableForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombinedTableForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            CombinedNodeType combinedNodeType = GetNodeType(e.TreeNode.Level + 1);
            SetCommonNodeContract(combinedNodeType);
            SetParametersOnPanel(combinedNodeType);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombinedTableForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                CombinedNodeType combinedNodeType = GetNodeType(e.TreeNode.Level);
                int count = 0;
                switch (combinedNodeType)
                {
                    case CombinedNodeType.ParentCategory:
                        /* 1.检查是否存在子节点，不允许删除有子节点的节点 */
                        count = customGroupContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("{0}节点下有{1}个组合表小类，请删除这些组合表小类。", commonNode.NodeName, count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case CombinedNodeType.ChildCategory:
                        /* 2. 不允许删除有组合表属于该分组的节点 */
                        count = combinedTableContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个组合表属于该节点[{1}]，请先删除这些组合表。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case CombinedNodeType.Leaf:
                        count = customFormContract.GetTotalCountByCombinedTableId(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个填报业务的窗体使用了该组合表[{1}]，请先删除这些填报业务的窗体。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        count = dataAuditingContract.GetTotalCountByCombinedTableId(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个个人信息更新的使用了该组合表[{1}]，请先删除这些填报业务的窗体。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        combinedTableContract.Delete(commonNode.NodeId);
                        break;
                }
                DeleteNode();
                MessageBox.Show(string.Format("节点[{0}]删除成功。", commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombinedTableForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            bool result = false;
            string verifyResult = string.Empty;
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            CombinedNodeType combinedNodeType = GetNodeType(e.TreeNode.Level);
            Cursor = Cursors.WaitCursor;
            switch (e.EditState)
            {
                case EditState.Add:
                    result = AddTreeNode(commonNode, combinedNodeType, ref verifyResult);
                    break;

                case EditState.Edit:
                    result = EditTreeNode(commonNode, combinedNodeType, ref verifyResult);
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

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombinedTableForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
        {
            if (!CurrentQueriedState)
            {
                Cursor = Cursors.WaitCursor;
                SetParametersOnPanel(GetNodeType(e.TreeNode.Level));
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CombinedTableForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CombinedNodeType nodeType = GetNodeType(e.TreeNode.Level);
            if (nodeType == CombinedNodeType.Leaf)
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                CommonListItemsForm frmCommonListItems = new CommonListItemsForm();
                frmCommonListItems.Text = "字段选择";
                frmCommonListItems.ToolTip = "字段列表";
                frmCommonListItems.CreateItmes = delegate (ListBoxControl lstItems)
                {
                    CheckedSelectedItemsForm frmCheckedSelectedItems = new CheckedSelectedItemsForm();
                    frmCheckedSelectedItems.MultiNodeSelected = delegate (IList<CommonNode> selectedNodes)
                    {
                        lstItems.Items.AddRange(selectedNodes.ToArray());
                    };

                    CombinedTableInfo combinedTableInfo = combinedTableContract.GetModelInfo(commonNode.NodeId);
                    IList<CommonNode> hasCommonNodes = combinedTableContract.GetDataFields(commonNode.NodeId);
                    IList<decimal> tableIds = new List<decimal>();                    
                    /* 关联表 */
                    IList<CommonNode> tableCommonNodes = combinedTableContract.GetTables(commonNode.NodeId);
                    foreach (var tableCommonNode in tableCommonNodes)
                    {
                        tableIds.Add(tableCommonNode.NodeId);
                    }
                    IList<CommonNode> dataFieldCommonNodes = GetDataFieldCommonNodes(tableIds, hasCommonNodes);
                    frmCheckedSelectedItems.LoadAndSetCommonNodes(dataFieldCommonNodes);
                    frmCheckedSelectedItems.ShowDialog();
                };
                frmCommonListItems.GetItems = delegate (IList<CommonNode> nodes)
                {
                    IList<CombinedDataFieldInfo> combinedDataFieldInfos = new List<CombinedDataFieldInfo>();
                    int sorting = 1;
                    foreach (var obj in nodes)
                    {
                        combinedDataFieldInfos.Add(new CombinedDataFieldInfo(commonNode.NodeId, obj.NodeId, sorting++));
                    }
                    combinedTableContract.UpdateDataFields(commonNode.NodeId, combinedDataFieldInfos);
                };
                IList<CommonNode> commonNodes = combinedTableContract.GetDataFields(commonNode.NodeId);
                frmCommonListItems.LoadItems(commonNodes);
                frmCommonListItems.ShowDialog();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获得未加入的字段名称列表
        /// </summary>
        /// <param name="tableIds"></param>
        /// <param name="hasCommonNodes"></param>
        /// <returns></returns>
        private IList<CommonNode> GetDataFieldCommonNodes(IList<decimal> tableIds, IList<CommonNode> hasCommonNodes)
        {
            IList<CommonNode> dataFieldCommonNodes = new List<CommonNode>(); 
                       
            foreach (var tableId in tableIds)
            {
                IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodes(tableId, DataFieldFilter.PhysicalFieldAndKeyLogicalField);
                string primaryTableName = customTableContract.GetTableLogicalName(tableId);
                foreach (var obj in commonNodes)
                {
                    bool exist = false;
                    foreach (var node in hasCommonNodes)
                    {
                        if (obj.NodeId == node.NodeId)
                        {
                            exist = true;
                            break;
                        }
                    }
                    if (!exist)
                    {
                        obj.NodeName = string.Format("[{0}][{1}]", primaryTableName, obj.NodeName);
                        dataFieldCommonNodes.Add(obj);
                    }
                }
            }

            return dataFieldCommonNodes;
        }

        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="associationNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddTreeNode(CommonNode commonNode, CombinedNodeType combinedNodeType, ref string verifyResult)
        {
            bool result = false;
            decimal nodeId = 0;
            string name = string.Empty;
            string value = string.Empty;
            string tip = string.Empty;

            switch (combinedNodeType)
            {
                case CombinedNodeType.Root:
                case CombinedNodeType.ParentCategory:
                    ExtendedCommonNode extendedCommonNode = groupModule.GetModelInfo();
                    CustomGroupInfo customGroupInfo = new CustomGroupInfo()
                    {
                        UserId = decimal.MinValue,
                        ParentGroupId = decimal.MinValue,
                        GroupName = extendedCommonNode.NodeName,
                        GroupCode = extendedCommonNode.NodeCode,
                        GroupType = (byte)GroupType.CombinedTable,
                        Notes = extendedCommonNode.Notes,
                        IsLeaf = true
                        
                    };
                    if (combinedNodeType == CombinedNodeType.ParentCategory)
                    {
                        customGroupInfo.ParentGroupId = commonNode.NodeId;
                    }
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        if (customGroupContract.IsExistIdenticalName(commonNode.NodeId, customGroupInfo.GroupName,(byte)GroupType.CombinedTable))
                        {
                            verifyResult = string.Format("同一大类下分组名称不允许重复, 分组名称[{0}]已存在。", customGroupInfo.GroupName);
                            return false;
                        }
                        nodeId = customGroupContract.Insert(customGroupInfo);
                        name = customGroupInfo.GroupName;
                        value = customGroupInfo.GroupCode;
                    }
                    break;

                case CombinedNodeType.ChildCategory:
                    result = combinedTableModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CombinedTableInfo combinedTableInfo = combinedTableModule.GetModelInfo();
                        IList<CombinedTableRelationInfo> combinedTableRelationInfos = combinedTableModule.GetCombinedTableRelationInfos();
                        combinedTableInfo.GroupId = commonNode.NodeId;
                        if (combinedTableContract.IsExistIdenticalName(commonNode.NodeId, combinedTableInfo.CombinedTableName))
                        {
                            verifyResult = string.Format("同一分类下的组合表名称不允许重复, 组合表名称[{0}]已存在。", combinedTableInfo.CombinedTableName);
                            return false;
                        }
                        nodeId = combinedTableContract.Insert(combinedTableInfo, combinedTableRelationInfos);
                        name = combinedTableInfo.CombinedTableName;
                        value = combinedTableInfo.CombinedTableCode;
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
                AddNode(tn);                
            }
            Cursor = Cursors.Default;

            return result;
        }

        /// <summary>
        /// 编辑节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="combinedNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool EditTreeNode(CommonNode commonNode, CombinedNodeType combinedNodeType, ref string verifyResult)
        {
            bool result = false;
            string name = string.Empty;

            //针对查询后修改数据节点的处理
            if (combinedNodeType == CombinedNodeType.Root && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId))
            {
                combinedNodeType = CombinedNodeType.Leaf;
            }

            switch (combinedNodeType)
            {
                case CombinedNodeType.ParentCategory:
                case CombinedNodeType.ChildCategory:
                    ExtendedCommonNode groupCommonNode = groupModule.GetModelInfo();
                    CustomGroupInfo oldCustomGroupInfo = customGroupContract.GetModelInfo(commonNode.NodeId);
                    CustomGroupInfo customGroupInfo = new CustomGroupInfo()
                    {
                        GroupId = commonNode.NodeId,
                        GroupName = groupCommonNode.NodeName,
                        GroupCode = groupCommonNode.NodeCode,
                        Notes = groupCommonNode.Notes                        
                    };
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        if (!customGroupInfo.GroupName.Equals(oldCustomGroupInfo.GroupName) && customGroupContract.IsExistIdenticalName(commonNode.NodeId, groupCommonNode.NodeName, (byte)GroupType.CombinedTable))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一大类下分组名称不允许重复, 分组名称[{ 0}]已存在。", customGroupInfo.GroupName);
                            return false;
                        }
                        customGroupContract.Update(customGroupInfo);
                        name = customGroupInfo.GroupName;
                    }
                    break;

                case CombinedNodeType.Leaf:
                    result = combinedTableModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CombinedTableInfo combinedTableInfo = combinedTableModule.GetModelInfo();
                        IList<CombinedTableRelationInfo> combinedTableRelationInfos = combinedTableModule.GetCombinedTableRelationInfos();
                        CombinedTableInfo oldCombinedTableInfo = combinedTableContract.GetModelInfo(commonNode.NodeId);
                        combinedTableInfo.GroupId = oldCombinedTableInfo.GroupId;
                        if (!combinedTableInfo.CombinedTableName.Equals(oldCombinedTableInfo.CombinedTableName) 
                            && combinedTableContract.IsExistIdenticalName(commonNode.NodeId, combinedTableInfo.CombinedTableName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一分类下的组合表名称不允许重复, 组合表名称[{0}]已存在。", oldCombinedTableInfo.CombinedTableName);
                            return false;
                        }
                        combinedTableContract.Update(combinedTableInfo, combinedTableRelationInfos);
                        name = combinedTableInfo.CombinedTableName;
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
        /// 获得数据库节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private CombinedNodeType GetNodeType(int level)
        {
            CombinedNodeType combinedNodeType = CombinedNodeType.Root;

            /* (1) 数据仓库 (2) 分组大类 (3) 分组小类 (4) 视图 */
            switch (level)
            {
                case 0:
                    combinedNodeType = CombinedNodeType.Root;
                    break;

                case 1:
                    combinedNodeType = CombinedNodeType.ParentCategory;
                    break;

                case 2:
                    combinedNodeType = CombinedNodeType.ChildCategory;
                    break;

                case 3:
                    combinedNodeType = CombinedNodeType.Leaf;
                    break;
            }

            return combinedNodeType;
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="combinedNodeType"></param>
        private void SetCommonNodeContract(CombinedNodeType combinedNodeType)
        {
            /* (1) 数据仓库 (2) 分组大类 (3) 分组小类 (4) 视图 (5) 逻辑字段 */
            switch (combinedNodeType)
            {
                case CombinedNodeType.Root:
                    CommonNodeContract = null;
                    break;

                case CombinedNodeType.ParentCategory:
                case CombinedNodeType.ChildCategory:
                    CommonNodeContract = customGroupContract;
                    break;

                case CombinedNodeType.Leaf:
                    CommonNodeContract = combinedTableContract;
                    break;
            }
        }

        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="combinedNodeType"></param>
        private void SetParametersOnPanel(CombinedNodeType combinedNodeType)
        {
            /* (1) 数据仓库 (2) 分组大类 (3) 分组小类 (4) 视图 (5) 逻辑字段 */
            switch (combinedNodeType)
            {
                case CombinedNodeType.Root:
                case CombinedNodeType.ParentCategory:
                case CombinedNodeType.ChildCategory:
                    TreeNodeShow = groupModule;
                    groupModule.Visible = true;
                    combinedTableModule.Visible = false;
                    switch (combinedNodeType)
                    {
                        case CombinedNodeType.Root:
                            groupModule.LayerName = "业务名称";
                            groupModule.LayerCodeName = "业务编码";
                            break;

                        case CombinedNodeType.ParentCategory:
                            groupModule.LayerName = "大类名称";
                            groupModule.LayerCodeName = "大类编码";
                            break;

                        case CombinedNodeType.ChildCategory:
                            groupModule.LayerName = "小类名称";
                            groupModule.LayerCodeName = "小类编码";
                            break;
                    }
                    break;

                case CombinedNodeType.Leaf:
                    TreeNodeShow = combinedTableModule;
                    combinedTableModule.Visible = true;
                    groupModule.Visible = false;
                    break;
            }
        }

        #endregion

        #region 重写虚拟化方法

        /// <summary>
        /// 获得节点的祖先节点信息
        /// </summary>
        /// <param name="commonNode"></param>
        /// <returns></returns>
        protected override string GetToolTipText(CommonNode commonNode)
        {
            IList<string> names = customGroupContract.GetHierarchicalNamesOfNode(commonNode.ParentNodeId);
            StringBuilder sb = new StringBuilder();
            foreach (string name in names)
            {
                sb.AppendFormat("[{0}]", name);
            }
            sb.AppendFormat("[{0}]", commonNode.NodeName);

            return sb.ToString();
        }

        /// <summary>
        /// 初始化属性节点
        /// </summary>
        protected override void InitFirstLevelNodes()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "组合表结构", string.Empty, false, (byte)GroupType.CombinedTable));
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
            CombinedNodeType movedTreeNodeType = GetNodeType(movedTreeNode.Level);
            CombinedNodeType targeNodeTreeNodeType = GetNodeType(targeNode.Level);
            if (movedTreeNodeType == CombinedNodeType.Leaf && targeNodeTreeNodeType == CombinedNodeType.ChildCategory)
            {
                return true;
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
            bool result = true;

            CombinedNodeType combinedNodeType = GetNodeType(level);

            if (combinedNodeType == CombinedNodeType.Leaf)
            {
                return result = false;
            }

            return result;
        }

        #endregion
    }
}
