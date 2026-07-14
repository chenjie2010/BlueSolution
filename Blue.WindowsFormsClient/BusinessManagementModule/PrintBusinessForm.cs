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

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class PrintBusinessForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomPrintContract customPrintContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICombinedTableContract combinedTableContract;
        private readonly ICustomRoleContract customRoleContract;

        #endregion

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly PrintModule printModule;

        #endregion

        #region 控件方法

        /// <summary>
        /// 构造函数
        /// </summary>

        public PrintBusinessForm()
        {
            InitializeComponent();

            SettingCaption = "读取字段设置";
            SettingEnabled = false;
            CustomItemCaption = "输入字段设置";
            CustomItemEnabled = false;
            ExchangeItemCaption = "内容设置";
            ExchangeItemEnabled = false;
            CustomItemVisible = DevExpress.XtraBars.BarItemVisibility.Always;
            ExchangeItemVisible = DevExpress.XtraBars.BarItemVisibility.Always;

            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customPrintContract = BusinessChannelFactory.CreateCustomPrintContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            combinedTableContract = BusinessChannelFactory.CreateCombinedTableContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();

            IList<UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { LayerName = "分组名称：", LayerCodeName = "分组编码：", CommonNodeContract = customGroupContract };
            userControls.Add(groupModule);

            printModule = new PrintModule() { CustomGroupContract = customGroupContract, CustomTableContract = customTableContract, CombinedTableContract  = combinedTableContract,
                CustomPrintContract = customPrintContract, CustomRoleContract = customRoleContract};
            userControls.Add(printModule);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 2;  /*  允许最大层次 */
            Tip = "创建打印成功后请进行字段设置。";
            NullValuePrompt = "请输入打印名称查询";
            AddControls(userControls);
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintBusinessForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintBusinessForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeType treeNodeType = GetNodeType(e.Node.Level);
            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                if (commonNode.NodeId > 0)
                {
                    groupModule.Visible = false;
                    printModule.Visible = true;
                    TreeNodeShow = printModule;
                }
                else
                {
                    groupModule.Visible = true;
                    printModule.Visible = false;
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
                ExchangeItemEnabled = true;
            }
            else
            {
                SettingEnabled = false;
                CustomItemEnabled = false;
                ExchangeItemEnabled = false;
            }
        }

        /// <summary>
        /// 查询之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintBusinessForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = customPrintContract;
        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintBusinessForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintBusinessForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
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
        private void PrintBusinessForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
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
                        count = customPrintContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个打印属于该节点[{1}]，请先删除这些打印。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case TreeNodeType.Leaf:
                        count = customRoleContract.GetRoleCountByPrintId(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个角色拥有该打印[{1}]的权限，请先解除角色的权限。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customPrintContract.Delete(commonNode.NodeId);
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
        private void PrintBusinessForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
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
        private void PrintBusinessForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
        {
            if (!CurrentQueriedState)
            {
                Cursor = Cursors.WaitCursor;
                SetParametersOnPanel(GetNodeType(e.TreeNode.Level));
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 读取字段设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintBusinessForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            TreeNodeType nodeType = GetNodeType(e.TreeNode.Level);
            if (nodeType == TreeNodeType.Leaf)
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                SetDataFields(DataFieldPrintType.DefalutValue, commonNode);                
            }
        }        

        /// <summary>
        /// 输入字段设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintBusinessForm_OnCustomItemClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                TreeNodeType nodeType = GetNodeType(e.TreeNode.Level);
                if (nodeType == TreeNodeType.Leaf)
                {
                    CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                    SetDataFields(DataFieldPrintType.CustomInput, commonNode);
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
        /// 内容设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PrintBusinessForm_OnExchangeClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                TreeNodeType nodeType = GetNodeType(e.TreeNode.Level);
                if (nodeType == TreeNodeType.Leaf)
                {
                    Cursor = Cursors.WaitCursor;
                    CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                    PrintTextForm frmPrintText = new PrintTextForm();
                    frmPrintText.AttachmentCategory = AttachmentCategory.PrintBusiness;
                    frmPrintText.HtmlText = customPrintContract.GetPrintContent(commonNode.NodeId);
                    frmPrintText.GetRichTextAndAttachments = (richText, upLoadFileInfos) =>
                    {
                        customPrintContract.UpdatePrintContent(commonNode.NodeId, richText, upLoadFileInfos);
                    };
                    frmPrintText.PrintId = commonNode.NodeId;
                    Cursor = Cursors.Default;
                    frmPrintText.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
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
                        UserId = decimal.MinValue,
                        ParentGroupId = decimal.MinValue,
                        GroupName = extendedCommonNode.NodeName,
                        GroupCode = extendedCommonNode.NodeCode,
                        GroupType = (byte)GroupType.Print,
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
                    result = printModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomPrintInfo customPrintInfo = printModule.GetModelInfo();
                        customPrintInfo.GroupId = commonNode.NodeId;
                        if (customPrintContract.IsExistIdenticalName(commonNode.NodeId, customPrintInfo.PrintName))
                        {
                            verifyResult = string.Format("同一分类下的打印名称不允许重复, 打印名称[{0}]已存在。", customPrintInfo.PrintName);
                            return false;
                        }
                        nodeId = customPrintContract.Insert(customPrintInfo);
                        name = customPrintInfo.PrintName;
                        value = customPrintInfo.PrintCode;
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
                    result = printModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomPrintInfo customPrintInfo = printModule.GetModelInfo();
                        CustomPrintInfo oldCustomPrintInfo = customPrintContract.GetModelInfo(commonNode.NodeId);
                        customPrintInfo.GroupId = oldCustomPrintInfo.GroupId;
                        if (!customPrintInfo.PrintName.Equals(oldCustomPrintInfo.PrintName)
                            && customPrintContract.IsExistIdenticalName(commonNode.NodeId, customPrintInfo.PrintName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一分类下的打印名称不允许重复, 打印名称[{0}]已存在。", oldCustomPrintInfo.PrintName);
                            return false;
                        }
                        customPrintContract.Update(customPrintInfo);
                        name = customPrintInfo.PrintName;
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
                    CommonNodeContract = customPrintContract;
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
                    printModule.Visible = false;
                    switch (treeNodeType)
                    {
                        case TreeNodeType.Root:
                            groupModule.LayerName = "业务名称";
                            groupModule.LayerCodeName = "业务编码";
                            break;

                        case TreeNodeType.Category:
                            groupModule.LayerName = "分类名称";
                            groupModule.LayerCodeName = "分类编码";
                            break;
                    }
                    break;

                case TreeNodeType.Leaf:
                    TreeNodeShow = printModule;
                    printModule.Visible = true;
                    groupModule.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// 字段设置
        /// </summary>
        /// <param name="dataFieldPrintType"></param>
        private void SetDataFields(DataFieldPrintType dataFieldPrintType, CommonNode commonNode)
        {
            IList<CommonNode> commonNodes = customPrintContract.GetDataFields(commonNode.NodeId, (byte)dataFieldPrintType);
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
                CustomPrintInfo customPrintInfo = customPrintContract.GetModelInfo(commonNode.NodeId);
                FormType formType = (FormType)customPrintInfo.TableType;
                IList<CommonNode> nodes = null;
                switch (formType)
                {
                    case FormType.CombinedTable:
                        nodes = combinedTableContract.GetDataFields(customPrintInfo.CombinedTableId);
                        break;

                    case FormType.Table:
                        nodes = customDataFieldContract.GetCommonNodes(customPrintInfo.TableId, DataFieldFilter.PhysicalFieldAndKeyLogicalField);
                        break;
                }
                frmCheckedSelectedItems.LoadAndSetCommonNodes(nodes);
                frmCheckedSelectedItems.ShowDialog();
            };
            frmCommonListItems.GetItems = delegate (IList<CommonNode> nodes)
            {
                IList<CustomPrintAndDataFieldInfo> customPrintAndDataFieldInfos = new List<CustomPrintAndDataFieldInfo>();
                int sorting = 1;
                foreach (var obj in nodes)
                {
                    int index = customPrintAndDataFieldInfos.FindIndex(customPrintAndDataFieldInfo => customPrintAndDataFieldInfo.DataFieldId == obj.NodeId);
                    if (index < 0)
                    {
                        customPrintAndDataFieldInfos.Add(new CustomPrintAndDataFieldInfo(commonNode.NodeId, obj.NodeId, (byte)dataFieldPrintType, sorting++));
                    }
                }
                customPrintContract.UpdateDataFields(commonNode.NodeId, (byte)dataFieldPrintType, customPrintAndDataFieldInfos);
            };
            frmCommonListItems.LoadItems(commonNodes);
            frmCommonListItems.ShowDialog();
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
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "打印结构", string.Empty, false, (byte)GroupType.Print));
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
