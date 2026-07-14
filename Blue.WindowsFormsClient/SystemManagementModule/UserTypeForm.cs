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
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class UserTypeForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly IUserAccountContract userAccountContract;

        #endregion       

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly UserTypeModule userTypeModule;

        #endregion

        #region 窗体和控件的方法        

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserTypeForm()
        {
            InitializeComponent();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();

            IList<UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { LayerName = "分类名称：", LayerCodeName = "分类编码：" };
            TreeNodeShow = groupModule;
            userControls.Add(groupModule);

            userTypeModule = new UserTypeModule() { UserTypeContract = userTypeContract };
            userControls.Add(userTypeModule);

            /* 初始化属性 */
            SettingVisible = DevExpress.XtraBars.BarItemVisibility.Never;
            ExchangeItemVisible = DevExpress.XtraBars.BarItemVisibility.Never;
            NullValuePrompt = "请输入用户类型名称或者用户类型编码";
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 2;  /*  允许最大层次 */
            SetDefaultTip();           
            AddControls(userControls);
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 节点选择前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeType treeNodeType = GetSimpleNodeTypeNode(e.Node.Level);
            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                /* 查询节点：分类节点和用户类型节点 */
                if (commonNode.ParentNodeId > 0)
                {
                    groupModule.Visible = false;
                    userTypeModule.Visible = true;
                    TreeNodeShow = userTypeModule;
                }
                else
                {
                    groupModule.Visible = true;
                    userTypeModule.Visible = false;
                    TreeNodeShow = groupModule;
                }
            }
            else
            {
                SetCommonNodeContract(treeNodeType);
                SetParametersOnPanel(treeNodeType);
            }
        }

        /// <summary>
        /// 查询之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = userTypeContract;
        }


        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetSimpleNodeTypeNode(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            TreeNodeType treeNodeType = GetSimpleNodeTypeNode(e.TreeNode.Level + 1);
            SetCommonNodeContract(treeNodeType);
            SetParametersOnPanel(treeNodeType);
        }

        /// <summary>
        /// 编辑前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeForm_OnBeforeEditClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CheckBeforeOperation(e, true);
        }

        /// <summary>
        /// 删除前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeForm_OnBeforeDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CheckBeforeOperation(e, false);
        }
      
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                int count = 0;
                switch (e.TreeNode.Level)
                {
                    case 1:
                        /* 1.检查是否存在子节点，不允许删除有子节点的节点 */
                        count = userTypeContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("{0}节点下有{1}个用户类型，请删除这些用户类型。", commonNode.NodeName, count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case 2:
                        /* 2. 检查是否有用户属于该节点定义的用户类型, 不允许删除有用户属于该用户类型的节点 */
                        count = userAccountContract.GetUserCountByUserTypeId(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个用户属于[{1}]，请先删除这些用户。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        userTypeContract.Delete(commonNode.NodeId);
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
        private void UserTypeForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            try
            {
                bool result = false;
                string verifyResult = string.Empty;
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                TreeNodeType treeNodeType = GetSimpleNodeTypeNode(e.TreeNode.Level);
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
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
        {
            if (!CurrentQueriedState)
            {
                Cursor = Cursors.WaitCursor;
                SetParametersOnPanel(GetSimpleNodeTypeNode(e.TreeNode.Level));
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 节点选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserTypeForm_OnAfterSelectOfTreeView(object sender, TreeViewEventArgs e)
        {
            bool show = true;
            if (e.Node != null)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                if (e.Node.Level == 0)
                {
                    SetActiveStatesOfControls(MenuItemState.Disabled);
                    Tip = string.Format("提示信息：{0}是根节点，不能编辑或删除。", commonNode.NodeName);
                    show = false;
                }
                else if (e.Node.Level == 1)
                {
                    MaxMinValue enumValue = UserEnumHelper.GetMaxAndMinValue(typeof(UserProperty));
                    if (commonNode.NodeId >= enumValue.Min && commonNode.NodeId <= enumValue.Max)
                    {
                        SetActiveStatesOfControls(MenuItemState.AllowToAdd);
                        Tip = string.Format("提示信息：{0}是默认用户类型分类，不能编辑或删除。", commonNode.NodeName);
                        show = false;
                    }                   
                }
            }

            if(show)
            {
                SetDefaultTip();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="treeNode"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddTreeNode(CommonNode commonNode, TreeNodeType treeNodeType, ref string verifyResult)
        {
            bool result = false;
            decimal nodeId = 0;
            string name = string.Empty;
            string value = string.Empty;

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
                        GroupType = (byte)GroupType.UserType,
                        Notes = extendedCommonNode.Notes,
                        IsLeaf = true

                    };
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        if (customGroupContract.IsExistIdenticalName(commonNode.NodeId, customGroupInfo.GroupName, (byte)GroupType.Role))
                        {
                            verifyResult = string.Format("用户类型分类名称不允许重复, 用户类型分类名称[{0}]已存在。", customGroupInfo.GroupName);
                            return false;
                        }
                        nodeId = customGroupContract.Insert(customGroupInfo);
                        name = customGroupInfo.GroupName;
                        value = customGroupInfo.GroupCode;
                    }
                    break;

                case TreeNodeType.Category:
                    UserTypeInfo userTypeInfo = userTypeModule.GetModelInfo();
                    userTypeInfo.GroupId = commonNode.NodeId;
                    result = ValidationHelper.Validate<UserTypeInfo>(userTypeInfo, out verifyResult);
                    if (result)
                    {
                        if (userTypeContract.IsExistIdenticalName(commonNode.NodeId, userTypeInfo.UserTypeName))
                        {
                            verifyResult = string.Format("同一分类下的用户类型名称不允许重复, 用户类型名称[{0}]已存在。", userTypeInfo.UserTypeName);
                            return false;
                        }
                        nodeId = userTypeContract.Insert(userTypeInfo);
                        name = userTypeInfo.UserTypeName;
                        value = userTypeInfo.UserTypeCode;
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
                        Notes = groupCommonNode.Notes
                    };
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        if (!customGroupInfo.GroupName.Equals(oldCustomGroupInfo.GroupName) && customGroupContract.IsExistIdenticalName(commonNode.NodeId, groupCommonNode.NodeName, (byte)GroupType.Role))
                        {
                            verifyResult = string.Format("同一用户类型分类名称不允许重复, 用户类型分类名称[{ 0}]已存在。", customGroupInfo.GroupName);
                            return false;
                        }
                        customGroupContract.Update(customGroupInfo);
                        name = customGroupInfo.GroupName;
                    }
                    break;

                case TreeNodeType.Leaf:
                    UserTypeInfo userTypeInfo = userTypeModule.GetModelInfo();
                    result = ValidationHelper.Validate<UserTypeInfo>(userTypeInfo, out verifyResult);
                    if (result)
                    {
                        UserTypeInfo oldUserTypeInfo = userTypeContract.GetModelInfo(userTypeInfo.UserTypeId);
                        userTypeInfo.GroupId = oldUserTypeInfo.GroupId;
                        if (!userTypeInfo.UserTypeName.Equals(oldUserTypeInfo.UserTypeName) &&
                                userTypeContract.IsExistIdenticalName(userTypeInfo.GroupId, userTypeInfo.UserTypeName))
                        {
                            verifyResult =  string.Format("同一分类下的用户类型名称不允许重复, 用户类型名称[{0}]已存在。", userTypeInfo.UserTypeName);
                            return false;
                        }
                        if (!userTypeInfo.UserTypeCode.Equals(oldUserTypeInfo.UserTypeCode) &&
                                        userTypeContract.IsExistIdenticalCode(userTypeInfo.UserTypeCode))
                        {
                            verifyResult = string.Format("该用户类型的用户类型编码[{0}]已存在", userTypeInfo.UserTypeCode);
                            return false;
                        }
                        userTypeContract.Update(userTypeInfo); ;
                        name = userTypeInfo.UserTypeName;
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
        /// 获得树形结构节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private TreeNodeType GetSimpleNodeTypeNode(int level)
        {
            TreeNodeType treeNodeType = TreeNodeType.Root;

            /* 第一层为根节点 第二层为用户类型分类节点，第三层为用户类型节点 */
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
            /* 第一层为根节点 第二层为用户类型分类节点，第三层为用户类型节点 */
            switch (treeNodeType)
            {
                case TreeNodeType.Root:
                    CommonNodeContract = null;
                    break;

                case TreeNodeType.Category:
                    CommonNodeContract = customGroupContract;
                    break;

                case TreeNodeType.Leaf:
                    CommonNodeContract = userTypeContract;
                    break;
            }
        }

        /// <summary>
        /// 设置右侧面板相关参数
        /// </summary>
        /// <param name="simplseNodeType"></param>
        private void SetParametersOnPanel(TreeNodeType simplseNodeType)
        {            
            /* 第一层为根节点 第二层为用户类型分类节点，第三层为用户类型节点 */
            switch (simplseNodeType)
            {
                case TreeNodeType.Root:
                    groupModule.Visible = true;
                    userTypeModule.Visible = false;
                    TreeNodeShow = groupModule;
                    break;

                case TreeNodeType.Category:
                    groupModule.Visible = true;
                    userTypeModule.Visible = false;
                    TreeNodeShow = groupModule;
                    break;

                case TreeNodeType.Leaf:
                    groupModule.Visible = false;
                    userTypeModule.Visible = true;
                    TreeNodeShow = userTypeModule;
                    break;
            }
        }

        /// <summary>
        /// 删除或是编辑前检查
        /// </summary>
        /// <param name="e"></param>
        /// <param name="edit"></param>
        private void CheckBeforeOperation(TreeNodeItemClickEventArgs e, bool edit)
        {
            string tip = string.Empty;
            if (edit)
            {
                tip = "编辑";
            }
            else
            {
                tip = "删除";
            }
            if (e.TreeNode == null)
            {
                e.Cancel = true;
                Cursor = Cursors.Default;
                MessageBox.Show(string.Format("请先选择需要{0}的节点。", tip), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (e.TreeNode.Level == 1)
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                MaxMinValue enumValue = UserEnumHelper.GetMaxAndMinValue(typeof(UserProperty));
                if (commonNode.NodeId >= enumValue.Min && commonNode.NodeId <= enumValue.Max)
                {
                    e.Cancel = true;
                    Cursor = Cursors.Default;
                    MessageBox.Show(string.Format("{0}节点是系统默认用户类型分类，不能{1}。", commonNode.NodeName, tip), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 设置默认提示
        /// </summary>
        private void SetDefaultTip()
        {
            Tip = string.Format("提示信息：树形结构不能超过 {0} 层。", MaxLevel + 1);
        }

        #endregion

        #region 重写虚拟化方法

        /// <summary>
        /// 初始化属性节点
        /// </summary>
        protected override void InitFirstLevelNodes()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "用户分类", string.Empty, false, (byte)GroupType.UserType));
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
           TreeNodeType movedTreeNodeSimpleNodeType = GetSimpleNodeTypeNode(movedTreeNode.Level);
            TreeNodeType targeNodeSimpleNodeType = GetSimpleNodeTypeNode(targeNode.Level);
            if (movedTreeNodeSimpleNodeType == TreeNodeType.Leaf && targeNodeSimpleNodeType == TreeNodeType.Category)
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

            TreeNodeType treeNodeType = GetSimpleNodeTypeNode(level);

            if (treeNodeType == TreeNodeType.Leaf)
            {
                return result = false;
            }

            return result;
        }

        #endregion

       
    }
}
