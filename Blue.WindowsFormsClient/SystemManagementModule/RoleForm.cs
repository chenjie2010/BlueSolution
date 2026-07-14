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
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class RoleForm : TreeLayerForm
    {
        #region 契约接口
        
        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomMenuContract customMenuContract;
        private readonly ICustomBusinessContract customBusinessContract;
        private readonly ICustomWorkflowProcessContract customWorkflowProcessContract;
        #endregion

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly RoleModule roleModule;

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>

        public RoleForm()
        {
            InitializeComponent();

            SettingCaption = "权限设置(&P)";
            SettingEnabled = false;
            SettingTip = "设置角色的权限";

            /* 导入导出不可用 */
            ExchangeItemVisible = DevExpress.XtraBars.BarItemVisibility.Never;

            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customMenuContract = BusinessChannelFactory.CreateCustomMenuContract();
            customBusinessContract = BusinessChannelFactory.CreateCustomBusinessContract();
            customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
            IList<UserControl> userControls = new List<UserControl>();

            groupModule = new TreeLayerModule() { LayerName = "分类名称：", LayerCodeName = "分类编码："};
            userControls.Add(groupModule);

            roleModule = new RoleModule() { CustomRoleContract = customRoleContract };
            userControls.Add(roleModule);


            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 2;  /*  允许最大层次 */
            Tip = "";
            NullValuePrompt = "请输入角色名称查询";
            AddControls(userControls);
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoleForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoleForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeType treeNodeType = GetNodeType(e.Node.Level);

            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                groupModule.Visible = false;
                roleModule.Visible = true;
                TreeNodeShow = roleModule;
            }
            else
            {
                SetCommonNodeContract(treeNodeType);
                SetParametersOnPanel(treeNodeType);                
            }
            if (treeNodeType == TreeNodeType.Leaf)
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
        private void RoleForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = customMenuContract;
        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoleForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RoleForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
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
        private void RoleForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                int count = 0;
                switch (e.TreeNode.Level)
                {
                    case 1:
                        /* 1.检查是否存在子节点，不允许删除有子节点的节点 */
                        count = customRoleContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("{0}节点下有{1}个角色，请删除这些角色。", commonNode.NodeName, count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case 2:
                        /* 2. 检查是否有工作流步骤属于该节点, 不允许删除参与了工作流步骤的节点 */
                        count = customWorkflowProcessContract.GetWorkflowProcessCountByRoleId(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个工作流步骤属于[{1}]，请先删除这些关系。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customRoleContract.Delete(commonNode.NodeId);
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
        private void RoleForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
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
        private void RoleForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
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
        private void RoleForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            TreeNodeType nodeType = GetNodeType(e.TreeNode.Level);
            if (nodeType == TreeNodeType.Leaf)
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                RoleSettingForm frmRoleSetting = new RoleSettingForm();
                frmRoleSetting.RoleId = commonNode.NodeId;
                frmRoleSetting.ShowDialog();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="associationNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddTreeNode(CommonNode commonNode, TreeNodeType treeNodeType, ref string verifyResult)
        {
            bool result = false;
            decimal nodeId = 0;
            string name = string.Empty;
            string value = string.Empty;            

            /* (1) 根节点 (2) 角色分类 (3) 角色  */
            switch (treeNodeType)
            {
                case TreeNodeType.Root:
                    ExtendedCommonNode extendedCommonNode = groupModule.GetModelInfo();
                    CustomGroupInfo customGroupInfo = new CustomGroupInfo()
                    {
                        UserId = decimal.MinValue,
                        ParentGroupId = decimal.MinValue,
                        GroupName = extendedCommonNode.NodeName,
                        GroupCode = extendedCommonNode.NodeCode,
                        GroupType = (byte)GroupType.Role,
                        Notes = extendedCommonNode.Notes,
                        IsLeaf = true

                    };
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        if (customGroupContract.IsExistIdenticalName(commonNode.NodeId, customGroupInfo.GroupName, (byte)GroupType.Role))
                        {
                            verifyResult = string.Format("角色分类名称不允许重复, 角色分类名称[{0}]已存在。", customGroupInfo.GroupName);
                            return false;
                        }
                        nodeId = customGroupContract.Insert(customGroupInfo);
                        name = customGroupInfo.GroupName;
                        value = customGroupInfo.GroupCode;
                    }
                    break;

                case TreeNodeType.Category:
                    result = roleModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomRoleInfo customRoleInfo = roleModule.GetModelInfo();
                        customRoleInfo.GroupId = commonNode.NodeId;
                        if (customRoleContract.IsExistIdenticalName(commonNode.NodeId, customRoleInfo.RoleName))
                        {
                            verifyResult = string.Format("同一角色分类下的角色名称不允许重复, 角色名称[{0}]已存在。", customRoleInfo.RoleName);
                            return false;
                        }
                        nodeId = customRoleContract.Insert(customRoleInfo);
                        name = customRoleInfo.RoleName;
                        value = customRoleInfo.RoleCode;
                    }
                    break;
            }

            if (result)
            {
                TreeNode tn = new TreeNode
                {
                    Text = name,
                    Tag = new CommonNode(nodeId, commonNode.NodeId, name, value, true, commonNode.NodeType)
                };
                AddNode(tn);
            }

            return result;
        }

        /// <summary>
        /// 编辑节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="treeNodeType"></param>
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
                        GroupType = (byte)GroupType.Role,
                        Notes = groupCommonNode.Notes
                    };
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        if (!customGroupInfo.GroupName.Equals(oldCustomGroupInfo.GroupName) && customGroupContract.IsExistIdenticalName(commonNode.NodeId, groupCommonNode.NodeName, (byte)GroupType.Role))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一角色分类名称不允许重复, 角色分类名称[{ 0}]已存在。", customGroupInfo.GroupName);
                            return false;
                        }
                        customGroupContract.Update(customGroupInfo);
                        name = customGroupInfo.GroupName;
                    }
                    break;

                case TreeNodeType.Leaf:
                    result = roleModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomRoleInfo customRoleInfo = roleModule.GetModelInfo();
                        CustomRoleInfo oldCustomRoleInfo = customRoleContract.GetModelInfo(commonNode.NodeId);
                        customRoleInfo.GroupId = oldCustomRoleInfo.GroupId;
                        if (!customRoleInfo.RoleName.Equals(oldCustomRoleInfo.RoleName) && customBusinessContract.IsExistIdenticalName(commonNode.NodeId, customRoleInfo.RoleName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一角色分类下的角色名称不允许重复, 角色名称[{0}]已存在。", customRoleInfo.RoleName);
                            return false;
                        }
                        customRoleContract.Update(customRoleInfo);
                        name = customRoleInfo.RoleName;
                    }
                    break;
            }
            if (result && !commonNode.NodeName.Equals(name))
            {
                ModifyNode(name);
            }

            return result;
        }

        /// <summary>
        /// 获得数据库节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private TreeNodeType GetNodeType(int level)
        {
            TreeNodeType treeNodeType = TreeNodeType.Root;

            /* (1) 根节点 (2) 角色分类 (3) 角色  */
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
            /* (1) 根节点 (2) 角色分类 (3) 角色  */
            switch (treeNodeType)
            {
                case TreeNodeType.Root:
                    CommonNodeContract = null;
                    break;

                case TreeNodeType.Category:
                    CommonNodeContract = customGroupContract;
                    break;

                case TreeNodeType.Leaf:
                    CommonNodeContract = customRoleContract;
                    break;
            }
        }

        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="treeNodeType"></param>
        private void SetParametersOnPanel(TreeNodeType treeNodeType)
        {
            /* (1) 根节点 (2) 角色分类 (3) 角色  */
            switch (treeNodeType)
            {
                case TreeNodeType.Root:
                case TreeNodeType.Category:
                    TreeNodeShow = groupModule;
                    groupModule.Visible = true;
                    roleModule.Visible = false;
                    break;                    

                case TreeNodeType.Leaf:
                    TreeNodeShow = roleModule;
                    groupModule.Visible = false;
                    roleModule.Visible = true;
                    break;
            }
        }

        #endregion

        #region 重写虚拟化方法

        /// <summary>
        /// 初始化属性节点
        /// </summary>
        protected override void InitFirstLevelNodes()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "角色列表", string.Empty, false, (byte)GroupType.Role));

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
