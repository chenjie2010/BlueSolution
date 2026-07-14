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
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class MenuForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomMenuContract customMenuContract;
        private readonly ICustomBusinessContract customBusinessContract;
        private readonly ICustomDataContract customDataContract;
        private readonly ICustomWorkflowContract customWorkflowContract;
        private readonly ICustomQueyContract customQueyContract;
        private readonly ICustomReportContract customReportContract;
        private readonly IDataAuditingContract dataAuditingContract;
        private readonly ICustomRoleContract customRoleContract;

        #endregion

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly MenuModule menuModule;
        private readonly MenuBusinessModule menuBusinessModule;

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>

        public MenuForm()
        {
            InitializeComponent();

            SettingVisible = DevExpress.XtraBars.BarItemVisibility.Never;

            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customMenuContract = BusinessChannelFactory.CreateCustomMenuContract();
            customBusinessContract = BusinessChannelFactory.CreateCustomBusinessContract();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            customWorkflowContract = BusinessChannelFactory.CreateCustomWorkflowContract();
            customQueyContract = BusinessChannelFactory.CreateCustomQueyContract();
            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();

            IList<UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { LayerName = "分类名称：", LayerCodeName = "分类编码：" };
            userControls.Add(groupModule);
            menuModule = new MenuModule() { CustomMenuContract = customMenuContract };
            userControls.Add(menuModule);
            menuBusinessModule = new MenuBusinessModule()
            {
                CustomGroupContract = customGroupContract,
                CustomBusinessContract = customBusinessContract,
                CustomDataContract = customDataContract,
                CustomWorkflowContract = customWorkflowContract,
                CustomQueyContract = customQueyContract,
                CustomReportContract = customReportContract,
                DataAuditingContract = dataAuditingContract
            };
            userControls.Add(menuBusinessModule);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 3;  /*  允许最大层次 */
            Tip = "";
            NullValuePrompt = "请输入菜单名称查询";
            AddControls(userControls);
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_OnAfterSelectOfTreeView(object sender, TreeViewEventArgs e)
        {
            MenuNodeType menuNodeType = GetNodeType(e.Node.Level);
            if (menuNodeType == MenuNodeType.Category)
            {
                /* 菜单默认目录不能编辑 */
                SetActiveStatesOfControls(MenuItemState.AllowToAdd);                
            }
        }

        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            MenuNodeType menuNodeType = GetNodeType(e.Node.Level);

            if (CurrentQueriedState)
            {
                TreeNodeShow = menuBusinessModule;
                groupModule.Visible = false;
                menuModule.Visible = false;
                menuBusinessModule.Visible = true;
            }
            else
            {
                SetCommonNodeContract(menuNodeType);
                SetParametersOnPanel(menuNodeType);                
            }
            if (menuNodeType == MenuNodeType.Bussiness)
            {
                CommonNode node = e.Node.Parent.Tag as CommonNode;
                menuBusinessModule.ParentMenuType = node.NodeType;
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
        private void MenuForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = customMenuContract;
        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            MenuNodeType menuNodeType = GetNodeType(e.TreeNode.Level + 1);
            if (menuNodeType == MenuNodeType.Bussiness)
            {
                CommonNode node = e.TreeNode.Parent.Tag as CommonNode;
                menuBusinessModule.ParentMenuType = node.NodeType;
            }
            SetCommonNodeContract(menuNodeType);
            SetParametersOnPanel(menuNodeType);            
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                MenuNodeType menuNodeType = GetNodeType(e.TreeNode.Level);
                int count = 0;
                switch (menuNodeType)
                {
                    case MenuNodeType.Catalog:
                        count = customMenuContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("菜单目录{0}下有{1}个菜单分类，请删除这些菜单分类。",
                                commonNode.NodeName, count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customMenuContract.Delete(commonNode.NodeId);
                        break;

                    case MenuNodeType.Class:
                        /* 1.检查是否存在子节点，不允许删除有子节点的节点 */
                        count = customBusinessContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("菜单分类{0}节点下有{1}个业务，请删除这些业务。",
                                commonNode.NodeName, count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customMenuContract.Delete(commonNode.NodeId);
                        break;

                    case MenuNodeType.Bussiness:
                        customBusinessContract.Delete(commonNode.NodeId);
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
        private void MenuForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            bool result = false;
            string verifyResult = string.Empty;
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            MenuNodeType menuNodeType = GetNodeType(e.TreeNode.Level);
            Cursor = Cursors.WaitCursor;
            switch (e.EditState)
            {
                case EditState.Add:
                    result = AddTreeNode(commonNode, menuNodeType, ref verifyResult);
                    break;

                case EditState.Edit:
                    result = EditTreeNode(commonNode, menuNodeType, ref verifyResult);
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
        private void MenuForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
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
        /// <param name="associationNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddTreeNode(CommonNode commonNode, MenuNodeType menuNodeType, ref string verifyResult)
        {
            bool result = false;
            decimal nodeId = 0;
            string name = string.Empty;
            string value = string.Empty;
            string tip = string.Empty;

            /* (1) 菜单目录 (2) 菜单分类 (3) 菜单名称 */
            switch (menuNodeType)
            {
                case MenuNodeType.Category:
                case MenuNodeType.Catalog:
                    result = menuModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomMenuInfo customMenuInfo = menuModule.GetModelInfo();
                        if (menuNodeType == MenuNodeType.Category)
                        {
                            customMenuInfo.ParentMenuId = decimal.MinValue;
                            byte maxMenuType = customMenuContract.GetMaxMenuType();
                            if (maxMenuType > (byte)BusinessMenu.Max)
                            {
                                customMenuInfo.MenuType = maxMenuType++;
                            }
                            else
                            {
                                maxMenuType = (byte)BusinessMenu.Max + 1;
                            }
                            customMenuInfo.MenuType = maxMenuType;
                        }
                        else
                        {
                            customMenuInfo.ParentMenuId = commonNode.NodeId;
                            customMenuInfo.MenuType = commonNode.NodeType;
                        }                        
                        if (customMenuContract.IsExistIdenticalName(commonNode.NodeId, customMenuInfo.MenuName, customMenuInfo.MenuType))
                        {
                            if (menuNodeType == MenuNodeType.Category)
                            {
                                verifyResult = string.Format("同一菜单目录名称不允许重复, 菜单目录[{0}]已存在。", customMenuInfo.MenuName);
                            }
                            else
                            {
                                verifyResult = string.Format("同一菜单分类名称不允许重复, 菜单分类[{0}]已存在。", customMenuInfo.MenuName);
                            }
                            return false;
                        }
                        byte[] menuIconData = menuModule.GetIconData();
                        nodeId = customMenuContract.Insert(customMenuInfo, menuIconData);
                        name = customMenuInfo.MenuName;
                        value = customMenuInfo.MenuCode;
                    }
                    break;

                case MenuNodeType.Class:
                    result = menuBusinessModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomBusinessInfo customBusinessInfo = menuBusinessModule.GetModelInfo();
                        customBusinessInfo.MenuId = commonNode.NodeId;
                        if (customBusinessContract.IsExistIdenticalName(commonNode.NodeId, customBusinessInfo.BusinessName))
                        {
                            verifyResult = string.Format("同一菜单分类下的业务名称不允许重复, 业务名称[{0}]已存在。", customBusinessInfo.BusinessName);
                            return false;
                        }
                        byte[] busineesIconData = menuBusinessModule.GetIconData();
                        IList<ExtendedUpLoadFileInfo> upLoadFileInfos = menuBusinessModule.GetAttachements();
                        nodeId = customBusinessContract.Insert(customBusinessInfo, upLoadFileInfos, busineesIconData);
                        name = customBusinessInfo.BusinessName;
                        value = customBusinessInfo.BusinessCode;
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
            Cursor = Cursors.Default;

            return result;
        }

        /// <summary>
        /// 编辑节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="menuNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool EditTreeNode(CommonNode commonNode, MenuNodeType menuNodeType, ref string verifyResult)
        {
            bool result = false;
            string name = string.Empty;

            //针对查询后修改数据节点的处理
            if (menuNodeType == MenuNodeType.Category && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId))
            {
                menuNodeType = MenuNodeType.Bussiness;
            }

            switch (menuNodeType)
            {
                case MenuNodeType.Catalog:
                case MenuNodeType.Class:
                    result = menuModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {                        
                        CustomMenuInfo customMenuInfo = menuModule.GetModelInfo();
                        CustomMenuInfo oldCustomMenuInfo = customMenuContract.GetModelInfo(commonNode.NodeId);
                        customMenuInfo.ParentMenuId = oldCustomMenuInfo.ParentMenuId;
                        customMenuInfo.MenuType = oldCustomMenuInfo.MenuType;
                        if (!customMenuInfo.MenuName.Equals(oldCustomMenuInfo.MenuName) 
                            && customMenuContract.IsExistIdenticalName(commonNode.NodeId, customMenuInfo.MenuName, customMenuInfo.MenuType))
                        {
                            Cursor = Cursors.Default;
                            if (menuNodeType == MenuNodeType.Catalog)
                            {
                                verifyResult = string.Format("同一菜单目录名称不允许重复, 菜单目录[{0}]已存在。", customMenuInfo.MenuName);
                            }
                            else
                            {
                                verifyResult = string.Format("同一菜单分类名称不允许重复, 菜单分类[{0}]已存在。", customMenuInfo.MenuName);
                            }
                            return false;
                        }
                        byte[] menuIconData = menuModule.GetIconData();
                        customMenuContract.Update(customMenuInfo, menuIconData);
                        name = customMenuInfo.MenuName;
                    }
                    break;

                case MenuNodeType.Bussiness:
                    result = menuBusinessModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomBusinessInfo customBusinessInfo = menuBusinessModule.GetModelInfo();
                        CustomBusinessInfo oldCustomBusinessInfo = customBusinessContract.GetModelInfo(commonNode.NodeId);
                        customBusinessInfo.MenuId = oldCustomBusinessInfo.MenuId;
                        if (!customBusinessInfo.BusinessName.Equals(oldCustomBusinessInfo.BusinessName) && customBusinessContract.IsExistIdenticalName(commonNode.NodeId, customBusinessInfo.BusinessName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一菜单目录下的菜单名称不允许重复, 菜单名称[{0}]已存在。", customBusinessInfo.BusinessName);
                            return false;
                        }
                        byte[] busineesIconData = menuBusinessModule.GetIconData();
                        IList<ExtendedUpLoadFileInfo> upLoadFileInfos = menuBusinessModule.GetAttachements();
                        customBusinessContract.Update(customBusinessInfo, upLoadFileInfos, busineesIconData);
                        name = customBusinessInfo.BusinessName;
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
        private MenuNodeType GetNodeType(int level)
        {
            MenuNodeType menuNodeType = MenuNodeType.Category;

            /* (1) 菜单类型 (2) 菜单目录 (3) 菜单分类 (4) 菜单名称 */
            switch (level)
            {
                case 0:
                    menuNodeType = MenuNodeType.Category;
                    break;

                case 1:
                    menuNodeType = MenuNodeType.Catalog;
                    break;

                case 2:
                    menuNodeType = MenuNodeType.Class;
                    break;

                case 3:
                    menuNodeType = MenuNodeType.Bussiness;
                    break;
            }

            return menuNodeType;
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="menuNodeType"></param>
        private void SetCommonNodeContract(MenuNodeType menuNodeType)
        {
            /* (1) 菜单类型 (2) 菜单目录 (3) 菜单分类 (4) 菜单名称 */
            switch (menuNodeType)
            {
                case MenuNodeType.Category:
                    CommonNodeContract = null;
                    break;

                case MenuNodeType.Catalog:
                case MenuNodeType.Class:
                    CommonNodeContract = customMenuContract;
                    break;

                case MenuNodeType.Bussiness:
                    CommonNodeContract = customBusinessContract;
                    break;
            }
        }

        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="menuNodeType"></param>
        private void SetParametersOnPanel(MenuNodeType menuNodeType)
        {
            /* (1) 菜单类型 (2) 菜单目录 (3) 菜单分类 (4) 菜单名称 */
            switch (menuNodeType)
            {
                case MenuNodeType.Category:
                    TreeNodeShow = groupModule;
                    groupModule.Visible = true;
                    menuModule.Visible = false;
                    menuBusinessModule.Visible = false;
                    break;


                case MenuNodeType.Catalog:
                case MenuNodeType.Class:
                    TreeNodeShow = menuModule;
                    groupModule.Visible = false;
                    menuModule.Visible = true;
                    menuBusinessModule.Visible = false;
                    switch (menuNodeType)
                    {                      
                        case MenuNodeType.Catalog:
                            menuModule.LayerName = "菜单目录";
                            menuModule.LayerCodeName = "目录编码";
                            break;

                        case MenuNodeType.Class:
                            menuModule.LayerName = "菜单分类";
                            menuModule.LayerCodeName = "分类编码";
                            break;
                    }
                    break;

                case MenuNodeType.Bussiness:
                    TreeNodeShow = menuBusinessModule;
                    groupModule.Visible = false;
                    menuModule.Visible = false;
                    menuBusinessModule.Visible = true;
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
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "业务应用菜单", string.Empty, false));
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
            MenuNodeType movedTreeNodeType = GetNodeType(movedTreeNode.Level);
            MenuNodeType targeNodeTreeNodeType = GetNodeType(targeNode.Level);            

            if (movedTreeNodeType == MenuNodeType.Bussiness && targeNodeTreeNodeType == MenuNodeType.Class)
            {
                CommonNode movedParentCommonNode = movedTreeNode.Parent.Tag as CommonNode;
                CommonNode targetNodeCommonNode = targeNode.Tag as CommonNode;
                if (movedParentCommonNode.NodeType == targetNodeCommonNode.NodeType)
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
            bool result = true;

            MenuNodeType treeNodeType = GetNodeType(level);

            if (treeNodeType == MenuNodeType.Bussiness)
            {
                return result = false;
            }

            return result;
        }

        #endregion
    }
}
