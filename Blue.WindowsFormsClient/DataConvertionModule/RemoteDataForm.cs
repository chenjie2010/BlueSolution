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
using Blue.Model.DataConvertionModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.DataConvertionModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;
using Blue.WindowsFormsClient.BusinessManagementModule;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class RemoteDataForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly IRemoteDataContract remoteDataContract;
        private readonly ICustomDatabaseContract customDatabaseContract;

        #endregion

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly RemoteDataModule remoteDataModule;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>

        public RemoteDataForm()
        {
            InitializeComponent();

            SettingCaption = "关系设置";
            SettingEnabled = false;
            CustomItemCaption = "业务操作";
            CustomItemEnabled = false;

            CustomItemVisible = DevExpress.XtraBars.BarItemVisibility.Always;
            ExchangeItemVisible = DevExpress.XtraBars.BarItemVisibility.Never;

            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customDatabaseContract = BusinessChannelFactory.CreateCustomDatabaseContract();
            remoteDataContract = DataConvertionChannelFactory.CreateRemoteDataContract();
            IList<UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { LayerName = "分组名称：", LayerCodeName = "分组编码：", CommonNodeContract = customGroupContract };
            userControls.Add(groupModule);

            remoteDataModule = new RemoteDataModule() { CustomDatabaseContract = customDatabaseContract, RemoteDataContract = remoteDataContract};
            userControls.Add(remoteDataModule);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 3;  /*  允许最大层次 */
            Tip = "创建远程交换名称成功后请进行字段设置。";
            NullValuePrompt = "请输入远程交换名称查询";
            AddControls(userControls);
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件方加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteDataForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteDataForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            CombinedNodeType combinedNodeType = GetNodeType(e.Node.Level);
            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                if (commonNode.NodeId > 0)
                {
                    groupModule.Visible = false;
                    remoteDataModule.Visible = true;
                    TreeNodeShow = remoteDataModule;
                }
                else
                {
                    groupModule.Visible = true;
                    remoteDataModule.Visible = false;
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
        private void RemoteDataForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = remoteDataContract;
        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteDataForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoteDataForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
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
        private void RemoteDataForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
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
                            MessageBox.Show(string.Format("{0}节点下有{1}个远程交换名称小类，请删除这些远程交换名称小类。", commonNode.NodeName, count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case CombinedNodeType.ChildCategory:
                        /* 2. 不允许删除有远程交换名称属于该分组的节点 */
                        count = remoteDataContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个远程交换名称称属于该节点[{1}]，请先删除这些远程交换名称。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case CombinedNodeType.Leaf:
                        remoteDataContract.Delete(commonNode.NodeId);
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
        private void RemoteDataForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
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
        private void RemoteDataForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
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
        private void RemoteDataForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CombinedNodeType nodeType = GetNodeType(e.TreeNode.Level);
            if (nodeType == CombinedNodeType.Leaf)
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                TableRelationForm frmTableRelation = new TableRelationForm();
                frmTableRelation.RelationId = commonNode.NodeId;
                frmTableRelation.DataSourceTypeValue = DataSourceType.Remote;
                frmTableRelation.ShowDialog();
            }
        }
        
        /// <summary>
        /// 自定义选项：数据交换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataRelationForm_OnCustomItemClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CombinedNodeType nodeType = GetNodeType(e.TreeNode.Level);
            if (nodeType == CombinedNodeType.Leaf)
            {
                DatatCopyForm frmDatatCopy = new DatatCopyForm();
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                frmDatatCopy.RelationId = commonNode.NodeId;
                frmDatatCopy.DataSourceTypeValue = DataSourceType.Remote;
                frmDatatCopy.ShowDialog();
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
                        GroupType = (byte)GroupType.RemoteDataExchanged,
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
                    result = remoteDataModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        RemoteDataInfo remoteDataInfo = remoteDataModule.GetModelInfo();
                        remoteDataInfo.GroupId = commonNode.NodeId;
                        if (remoteDataContract.IsExistIdenticalName(commonNode.NodeId, remoteDataInfo.RemoteDataName))
                        {
                            verifyResult = string.Format("同一分类下的数据交换名称不允许重复, 数据交换名称[{0}]已存在。", remoteDataInfo.RemoteDataName);
                            return false;
                        }
                        nodeId = remoteDataContract.Insert(remoteDataInfo);
                        name = remoteDataInfo.RemoteDataName;
                        value = remoteDataInfo.RemoteDataCode;
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
                    result = remoteDataModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        RemoteDataInfo remoteDataInfo = remoteDataModule.GetModelInfo();
                        RemoteDataInfo oldRemoteDataInfo = remoteDataContract.GetModelInfo(commonNode.NodeId);
                        remoteDataInfo.GroupId = oldRemoteDataInfo.GroupId;
                        if (!remoteDataInfo.RemoteDataName.Equals(oldRemoteDataInfo.RemoteDataName)
                            && remoteDataContract.IsExistIdenticalName(commonNode.NodeId, remoteDataInfo.RemoteDataName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一分类下的数据交换名称不允许重复, 数据交换名称[{0}]已存在。", oldRemoteDataInfo.RemoteDataName);
                            return false;
                        }
                        remoteDataContract.Update(remoteDataInfo);
                        name = remoteDataInfo.RemoteDataName;
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
        private CombinedNodeType GetNodeType(int level)
        {
            CombinedNodeType combinedNodeType = CombinedNodeType.Root;

            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 远程数据交换 */
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
            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 远程数据交换 */
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
                    CommonNodeContract = remoteDataContract;
                    break;
            }
        }

        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="combinedNodeType"></param>
        private void SetParametersOnPanel(CombinedNodeType combinedNodeType)
        {
            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 远程数据交换 */
            switch (combinedNodeType)
            {
                case CombinedNodeType.Root:
                case CombinedNodeType.ParentCategory:
                case CombinedNodeType.ChildCategory:
                    TreeNodeShow = groupModule;
                    groupModule.Visible = true;
                    remoteDataModule.Visible = false;
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
                    TreeNodeShow = remoteDataModule;
                    remoteDataModule.Visible = true;
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
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "远程数据交换结构", string.Empty, false, (byte)GroupType.RemoteDataExchanged));
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
