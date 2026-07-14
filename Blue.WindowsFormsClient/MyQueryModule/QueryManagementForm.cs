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
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;
using Blue.WindowsFormsClient.BusinessManagementModule;

namespace Blue.WindowsFormsClient.MyQueryModule
{
    public partial class QueryManagementForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserQueryContract userQueryContract;

        #endregion

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly QueryStatementModule queryStatementModule;

        #endregion

        #region  属性

        /// <summary>
        /// 刷新窗口
        /// </summary>
        public RefreshFormDelegate RefreshForm
        {
            get;
            set;
        }

        #endregion

        #region 控件和窗体方法

        /// <summary>
        /// 构造函数
        /// </summary>

        public QueryManagementForm()
        {
            InitializeComponent();
            
            ExchangeItemVisible = DevExpress.XtraBars.BarItemVisibility.Never;

            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            userQueryContract = BusinessChannelFactory.CreateUserQueryContract();
            IList<UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { LayerName = "分组名称：", LayerCodeName = "分组编码：", CommonNodeContract = customGroupContract };
            userControls.Add(groupModule);

            queryStatementModule = new QueryStatementModule() { UserQueryContract = userQueryContract };
            userControls.Add(queryStatementModule);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 1;  /*  允许最大层次 */
            Tip = "该树形结构共2级。";
            NullValuePrompt = "请输入用户查询名称查询";
            AddControls(userControls);
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryManagementForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryManagementForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeType treeNodeType = GetNodeType(e.Node.Level);
            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                if (commonNode.NodeId > 0)
                {
                    groupModule.Visible = false;
                    queryStatementModule.Visible = true;
                    TreeNodeShow = queryStatementModule;
                }
                else
                {
                    groupModule.Visible = true;
                    queryStatementModule.Visible = false;
                    TreeNodeShow = groupModule;
                }                
            }
            else
            {
                SetCommonNodeContract(treeNodeType);
                SetParametersOnPanel(treeNodeType);
            }
            if (treeNodeType == TreeNodeType.Leaf)
            {
                SettingEnabled = true;
                CustomItemEnabled = true;
            }
            else
            {
                SettingEnabled = false;
                CustomItemEnabled = false;
            }
        }

        /// <summary>
        /// 查询之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryManagementForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = userQueryContract;
        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryManagementForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryManagementForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            TreeNodeType treeNodeType = GetNodeType(e.TreeNode.Level + 1);
            SetCommonNodeContract(treeNodeType);
            SetParametersOnPanel(treeNodeType);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryManagementForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                TreeNodeType treeNodeType = GetNodeType(e.TreeNode.Level);
                int count = 0;
                switch (treeNodeType)
                {
                    case TreeNodeType.Category:
                        /* 1. 不允许删除有组合表属于该分组的节点 */
                        count = userQueryContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个用户查询属于该节点[{1}]，请先删除这些用户查询。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case TreeNodeType.Leaf:
                        userQueryContract.Delete(commonNode.NodeId);
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
        private void QueryManagementForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            bool result = false;
            string verifyResult = string.Empty;
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            TreeNodeType treeNodeType = GetNodeType(e.TreeNode.Level);
            Cursor = Cursors.WaitCursor;
            switch (e.EditState)
            {
                case EditState.Add:
                    result = AddTreeNode(commonNode, treeNodeType, ref verifyResult);
                    if (result)
                    {
                        RefreshForm?.Invoke();
                    }
                    break;

                case EditState.Edit:
                    result = EditTreeNode(commonNode, treeNodeType, ref verifyResult);
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
        private void QueryManagementForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
        {
            if (!CurrentQueriedState)
            {
                Cursor = Cursors.WaitCursor;
                SetParametersOnPanel(GetNodeType(e.TreeNode.Level));
                Cursor = Cursors.Default;
            }
        }
        
        #endregion

        #region 私有方法

        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="treeNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddTreeNode(CommonNode commonNode, TreeNodeType treeNodeType, ref string verifyResult)
        {
            bool result = false;
            decimal nodeId = 0;
            string name = string.Empty;
            string value = string.Empty;
            string tip = string.Empty;

            switch (treeNodeType)
            {
                case TreeNodeType.Root:
                    ExtendedCommonNode extendedCommonNode = groupModule.GetModelInfo();
                    CustomGroupInfo customGroupInfo = new CustomGroupInfo()
                    {
                        UserId = CurrentUser.Instance.UserId,
                        ParentGroupId = decimal.MinValue,
                        GroupName = extendedCommonNode.NodeName,
                        GroupCode = extendedCommonNode.NodeCode,
                        GroupType = (byte)GroupType.QueryStatement,
                        Notes = extendedCommonNode.Notes,
                        IsLeaf = true                        
                    };                    
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        if (customGroupContract.IsExistIdenticalName(commonNode.NodeId, customGroupInfo.GroupName,(byte)GroupType.CombinedTable))
                        {
                            verifyResult = string.Format("同一分类下分组名称不允许重复, 分组名称[{0}]已存在。", customGroupInfo.GroupName);
                            return false;
                        }
                        nodeId = customGroupContract.Insert(customGroupInfo);
                        name = customGroupInfo.GroupName;
                        value = customGroupInfo.GroupCode;
                    }
                    break;

                case TreeNodeType.Category:
                    result = queryStatementModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        UserQueryInfo userQueryInfo = queryStatementModule.GetModelInfo();
                        userQueryInfo.GroupId = commonNode.NodeId;
                        if (userQueryContract.IsExistIdenticalName(commonNode.NodeId, userQueryInfo.UserQueryName))
                        {
                            verifyResult = string.Format("同一分类下的用户查询名称不允许重复, 用户查询名称[{0}]已存在。", userQueryInfo.UserQueryName);
                            return false;
                        }
                        nodeId = userQueryContract.Insert(userQueryInfo);
                        name = userQueryInfo.UserQueryName;
                        value = userQueryInfo.UserQueryCode;
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
        private bool EditTreeNode(CommonNode commonNode, TreeNodeType treeNodeType, ref string verifyResult)
        {
            bool result = false;
            string name = string.Empty;

            //针对查询后修改数据节点的处理
            if (treeNodeType == TreeNodeType.Root && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId))
            {
                treeNodeType = TreeNodeType.Leaf;
            }

            switch (treeNodeType)
            {
                case TreeNodeType.Category:
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

                case TreeNodeType.Leaf:
                    result = queryStatementModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        UserQueryInfo userQueryInfo = queryStatementModule.GetModelInfo();
                        UserQueryInfo oldUserQueryInfo = userQueryContract.GetModelInfo(commonNode.NodeId);
                        userQueryInfo.GroupId = oldUserQueryInfo.GroupId;
                        if (!userQueryInfo.UserQueryName.Equals(oldUserQueryInfo.UserQueryName)
                            && userQueryContract.IsExistIdenticalName(commonNode.NodeId, userQueryInfo.UserQueryName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一分类下的用户查询名称不允许重复, 用户查询名称[{0}]已存在。", oldUserQueryInfo.UserQueryName);
                            return false;
                        }
                        userQueryContract.Update(userQueryInfo);
                        name = userQueryInfo.UserQueryName;
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
        /// 获得节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private TreeNodeType GetNodeType(int level)
        {
            TreeNodeType treeNodeType = TreeNodeType.Root;

            /* (1) 根节点 (2) 分组 (3) 打印 */
            switch (level)
            {
                case 0:
                    treeNodeType = TreeNodeType.Root;
                    break;

                case 1:
                    treeNodeType = TreeNodeType.Category;
                    break;

                case 2:
                    treeNodeType = TreeNodeType.Leaf;
                    break;
            }

            return treeNodeType;
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="treeNodeType"></param>
        private void SetCommonNodeContract(TreeNodeType treeNodeType)
        {
            /* (1) 根节点 (2) 分组 (3) 打印 */
            switch (treeNodeType)
            {
                case TreeNodeType.Root:
                    CommonNodeContract = null;
                    break;

                case TreeNodeType.Category:
                    CommonNodeContract = customGroupContract;
                    break;

                case TreeNodeType.Leaf:
                    CommonNodeContract = userQueryContract;
                    break;
            }
        }

        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="treeNodeType"></param>
        private void SetParametersOnPanel(TreeNodeType treeNodeType)
        {
            /* (1) 根节点 (2) 分组 (3) 打印 */
            switch (treeNodeType)
            {
                case TreeNodeType.Root:
                case TreeNodeType.Category:
                    TreeNodeShow = groupModule;
                    groupModule.Visible = true;
                    queryStatementModule.Visible = false;
                    switch (treeNodeType)
                    {
                        case TreeNodeType.Root:
                            groupModule.LayerName = "分类名称";
                            groupModule.LayerCodeName = "分类编码";
                            break;

                        case TreeNodeType.Category:
                            groupModule.LayerName = "分类名称";
                            groupModule.LayerCodeName = "分类编码";
                            break;
                    }
                    break;

                case TreeNodeType.Leaf:
                    TreeNodeShow = queryStatementModule;
                    queryStatementModule.Visible = true;
                    groupModule.Visible = false;
                    break;
            }
        }


        /// <summary>
        /// 获得子节点列表
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private IList<CommonNode> GetChildNodesByNode(TreeNode node)
        {
            IList<CommonNode> commonNodes = null;

            CommonNode commonNode = node.Tag as CommonNode;
            TreeNodeType treeNodeType = GetNodeType(node.Level + 1);
            switch (treeNodeType)
            {
                case TreeNodeType.Category:
                    commonNodes = CommonNodeContract.GetChildNodes(decimal.MinValue, (byte)GroupType.QueryStatement, CurrentUser.Instance.UserId);
                    break;

                case TreeNodeType.Leaf:
                    commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId, CurrentUser.Instance.UserId);
                    break;
            }

            return commonNodes;
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
        /// 获得子列表节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <returns></returns>
        protected override IList<CommonNode> GetChildNodes(TreeNode node)
        {
            return GetChildNodesByNode(node);
        }

        /// <summary>
        /// 获得子列表节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <returns></returns>
        protected override IList<CommonNode> GetChildNodesWithType(TreeNode node)
        {
            return GetChildNodesByNode(node);
        }
        
        /// <summary>
        /// 初始化属性节点
        /// </summary>
        protected override void InitFirstLevelNodes()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "查询语句结构", string.Empty, false, (byte)GroupType.QueryStatement));
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
            TreeNodeType movedTreeNodeType = GetNodeType(movedTreeNode.Level);
            TreeNodeType targeNodeTreeNodeType = GetNodeType(targeNode.Level);
            if (movedTreeNodeType == TreeNodeType.Leaf && targeNodeTreeNodeType == TreeNodeType.Category)
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

            TreeNodeType treeNodeType = GetNodeType(level);

            if (treeNodeType == TreeNodeType.Leaf)
            {
                return result = false;
            }

            return result;
        }

        #endregion        
    }
}
