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
using AppFramework.WinFormsLibrary.EventArgument;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class QueryForm : TreeLayerForm
    {

        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomQueyContract customQueyContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomViewContract customViewContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomBusinessContract customBusinessContract;

        #endregion

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly QueryModule queryModule;

        #endregion

        #region 构造函数

        public QueryForm()
        {
            InitializeComponent();

            SettingCaption = "查询字段设置";
            SettingEnabled = false;

            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customQueyContract = BusinessChannelFactory.CreateCustomQueyContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customViewContract = BusinessChannelFactory.CreateCustomViewContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customBusinessContract = BusinessChannelFactory.CreateCustomBusinessContract();

            IList<UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { LayerName = "查询名称：", LayerCodeName = "查询编码：", CommonNodeContract = customGroupContract };
            userControls.Add(groupModule);

            queryModule = new QueryModule() { CustomQueyContract = customQueyContract, CustomTableContract = customTableContract, CustomViewContract = customViewContract };
            userControls.Add(queryModule);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 3;  /*  允许最大层次 */
            Tip = "树形层：(1) 分组大类 (2) 分组小类 (3) 查询名称 ";
            NullValuePrompt = "请输入查询名称";
            AddControls(userControls);
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            CombinedNodeType combinedNodeType = GetNodeType(e.Node.Level);

            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                if (commonNode.NodeId > 0)
                {
                    groupModule.Visible = false;
                    queryModule.Visible = true;
                    TreeNodeShow = queryModule;
                }
                else
                {
                    groupModule.Visible = true;
                    queryModule.Visible = false;
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
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 查询之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = customQueyContract;
        }

        /// <summary>
        /// 创建节点前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            CombinedNodeType combinedNodeType = GetNodeType(e.TreeNode.Level + 1);
            SetCommonNodeContract(combinedNodeType);
            SetParametersOnPanel(combinedNodeType);
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
        {
            if (!CurrentQueriedState)
            {
                Cursor = Cursors.WaitCursor;
                SetParametersOnPanel(GetNodeType(e.TreeNode.Level));
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
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
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
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
                            MessageBox.Show(string.Format("{0}节点下有{1}个查询小类，请删除这些查询小类。", commonNode.NodeName, count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case CombinedNodeType.ChildCategory:
                        /* 2. 不允许删除有查询属于该分组的节点 */
                        count = customQueyContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个查询属于节点[{1}]，请先删除这些查询。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case CombinedNodeType.Leaf:
                        count = customBusinessContract.GetTotalCount(commonNode.NodeId, BusinessMenu.DataQuery);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个业务使用了该查询[{1}]，请先删除这些业务。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customQueyContract.Delete(commonNode.NodeId);
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
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void QueryForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CombinedNodeType nodeType = GetNodeType(e.TreeNode.Level);
            if (nodeType == CombinedNodeType.Leaf)
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                QuerySettingForm frmQuerySetting = new QuerySettingForm();
                frmQuerySetting.CustomQueyContract = customQueyContract;
                frmQuerySetting.CustomViewContract = customViewContract;
                frmQuerySetting.CustomTableContract = customTableContract;
                frmQuerySetting.CustomDataFieldContract = customDataFieldContract;
                frmQuerySetting.DataQueriedId = commonNode.NodeId;
                frmQuerySetting.LoadData();
                frmQuerySetting.ShowDialog();                
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
                        GroupType = (byte)GroupType.Query,
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
                        nodeId = customGroupContract.Insert(customGroupInfo);
                        name = customGroupInfo.GroupName;
                        value = customGroupInfo.GroupCode;
                    }
                    break;

                case CombinedNodeType.ChildCategory:                    
                    result = queryModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomQueyInfo customQueyInfo = queryModule.GetModelInfo();
                        customQueyInfo.GroupId = commonNode.NodeId;
                        if (customQueyContract.IsExistIdenticalName(commonNode.NodeId, customQueyInfo.DataQueriedName, (byte)GroupType.Query))
                        {
                            verifyResult = string.Format("同一分类下查询业务名称不允许重复, 查询业务名称[{0}]已存在。", customQueyInfo.DataQueriedName);
                            return false;
                        }
                        nodeId = customQueyContract.Insert(customQueyInfo);
                        name = customQueyInfo.DataQueriedName;
                        value = customQueyInfo.DataQueriedName;
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
                Cursor = Cursors.Default;
            }

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
                        Notes = groupCommonNode.Notes,
                    };
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        customGroupContract.Update(customGroupInfo);
                        name = customGroupInfo.GroupName;
                    }
                    break;

                case CombinedNodeType.Leaf:
                    result = queryModule.ValidateModelInfo(out verifyResult);                                        
                    if (result)
                    {
                        CustomQueyInfo customQueyInfo = queryModule.GetModelInfo();
                        CustomQueyInfo oldCustomDataQueyInfo = customQueyContract.GetModelInfo(commonNode.NodeId);
                        customQueyInfo.GroupId = oldCustomDataQueyInfo.GroupId;
                        if (!customQueyInfo.DataQueriedName.Equals(oldCustomDataQueyInfo.DataQueriedName) 
                            && customQueyContract.IsExistIdenticalName(commonNode.NodeId, customQueyInfo.DataQueriedName, (byte)GroupType.Query))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一分类下查询业务名称不允许重复, 查询业务名称[{0}]已存在。", customQueyInfo.DataQueriedName);
                            return false;
                        }
                        customQueyContract.Update(customQueyInfo);
                        name = customQueyInfo.DataQueriedName;
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
        /// 获得查询节点查询业务类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private CombinedNodeType GetNodeType(int level)
        {
            CombinedNodeType combinedNodeType = CombinedNodeType.Root;

            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 查询业务 */
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
                    CommonNodeContract = customQueyContract;
                    break;
            }
        }

        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="combinedNodeType"></param>
        private void SetParametersOnPanel(CombinedNodeType combinedNodeType)
        {
            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 查询业务 */
            switch (combinedNodeType)
            {
                case CombinedNodeType.Root:
                case CombinedNodeType.ParentCategory:
                case CombinedNodeType.ChildCategory:
                    TreeNodeShow = groupModule;
                    groupModule.Visible = true;
                    queryModule.Visible = false;
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
                    TreeNodeShow = queryModule;
                    queryModule.Visible = true;
                    groupModule.Visible = false;
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
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "数据查询业务", string.Empty, false, (byte)GroupType.Query));
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
                result = false;
            }

            return result;
        }
        
        #endregion  

    }
}
