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
using Blue.Model.BusinessDesignerModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class DataAuditingForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly IDataAuditingContract dataAuditingContract;
        private readonly ICombinedTableContract combinedTableContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomReportContract customReportContract;

        #endregion

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly DataAuditingModule dataAuditingModule;
        private string dataAuditingName = string.Empty;

        #endregion

        #region 属性

        /// <summary>
        /// 分组类型
        /// </summary>
        public GroupType GroupType
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataAuditingForm()
        {
            InitializeComponent();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            dataAuditingContract = BusinessDesignerChannelFactory.CreateDataAuditingContract();
            combinedTableContract = BusinessChannelFactory.CreateCombinedTableContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();

            IList<UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { LayerName = "分组名称：", LayerCodeName = "分组编码：", CommonNodeContract = customGroupContract };
            userControls.Add(groupModule);

            dataAuditingModule = new DataAuditingModule()
            {
                CustomGroupContract = customGroupContract,
                DataAuditingContract = dataAuditingContract,
                CombinedTableContract = combinedTableContract,
                CustomTableContract = customTableContract,
                CustomReportContract = customReportContract,
                UserAccountContract = userAccountContract,
                CustomRoleContract = customRoleContract
            };
            userControls.Add(dataAuditingModule);
            AddControls(userControls);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 3;  /*  允许最大层次 */            
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataAuditingForm_Load(object sender, EventArgs e)
        {
            if (GroupType != GroupType.InfoAudited && GroupType != GroupType.InfoUpdated)
            {
                throw new ArgumentException("不支持该属性");
            }
            dataAuditingModule.CurrentDataAuditingType = GroupType;
            dataAuditingModule.SetLabelTitle();
            dataAuditingName = UserEnumHelper.GetEnumText(GroupType);
            NullValuePrompt = string.Format("请输入{0}名称查询", dataAuditingName);
            Text = string.Format("{0}设计", dataAuditingName);
            switch (GroupType)
            {
                case GroupType.InfoAudited:
                    Tip = string.Format("请创建{0}成功后关联信息更新业务。", dataAuditingName);
                    SettingVisible = DevExpress.XtraBars.BarItemVisibility.Never;
                    break;

                case GroupType.InfoUpdated:
                    Tip = string.Format("用户{0}的业务字段业务与权限的交集。", dataAuditingName);
                    SettingCaption = "业务字段设置";
                    SettingEnabled = false;
                    break;
            }
        }

        /// <summary>
        /// 节点选择前设置属性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataAuditingForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            CombinedNodeType combinedNodeType = GetNodeType(e.Node.Level);
            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                if (commonNode.NodeId > 0)
                {
                    groupModule.Visible = false;
                    dataAuditingModule.Visible = true;
                    TreeNodeShow = dataAuditingModule;
                }
                else
                {
                    groupModule.Visible = true;
                    dataAuditingModule.Visible = false;
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
        private void DataAuditingForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = dataAuditingContract;
        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataAuditingForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataAuditingForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
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
        private void DataAuditingForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
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
                            MessageBox.Show(string.Format("{0}节点下有{1}个{2}小类，请删除这些{2}小类。",
                                commonNode.NodeName, count, dataAuditingName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case CombinedNodeType.ChildCategory:
                        /* 2. 不允许删除有组合表属于该分组的节点 */
                        count = dataAuditingContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("共有{0}个{1}属于该节点[{2}]，请先删除这些{1}。", count, dataAuditingName, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customGroupContract.Delete(commonNode.NodeId);
                        break;

                    case CombinedNodeType.Leaf:
                        dataAuditingContract.Delete(commonNode.NodeId);
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
        private void DataAuditingForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
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
        private void DataAuditingForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
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
        private void DataAuditingForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CombinedNodeType nodeType = GetNodeType(e.TreeNode.Level);
            if (nodeType == CombinedNodeType.Leaf)
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                DataAuditingInfo dataAuditingInfo = dataAuditingContract.GetModelInfo(commonNode.NodeId);
                UpdateDataFields(dataAuditingInfo);
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
                        GroupType = (byte)GroupType,
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
                        if (customGroupContract.IsExistIdenticalName(commonNode.NodeId, customGroupInfo.GroupName, (byte)GroupType))
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
                    result = dataAuditingModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        DataAuditingInfo dataAuditingInfo = dataAuditingModule.GetModelInfo();
                        dataAuditingInfo.GroupId = commonNode.NodeId;
                        if (dataAuditingContract.IsExistIdenticalName(commonNode.NodeId, dataAuditingInfo.DataAuditingName))
                        {
                            verifyResult = string.Format("同一分类下的{2}名称不允许重复, 名称[{1}]已存在。", dataAuditingName, dataAuditingInfo.DataAuditingName);
                            return false;
                        }
                        nodeId = dataAuditingContract.Insert(dataAuditingInfo);
                        name = dataAuditingInfo.DataAuditingName;
                        value = dataAuditingInfo.DataAuditingCode;
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
                        GroupType = oldCustomGroupInfo.GroupType,
                        Notes = groupCommonNode.Notes
                    };
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        if (!customGroupInfo.GroupName.Equals(oldCustomGroupInfo.GroupName) && customGroupContract.IsExistIdenticalName(commonNode.NodeId, groupCommonNode.NodeName, (byte)GroupType.Appointment))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一大类下分组名称不允许重复, 分组名称[{0}]已存在。", customGroupInfo.GroupName);
                            return false;
                        }
                        customGroupContract.Update(customGroupInfo);
                        name = customGroupInfo.GroupName;
                    }
                    break;

                case CombinedNodeType.Leaf:
                    result = dataAuditingModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        DataAuditingInfo dataAuditingInfo = dataAuditingModule.GetModelInfo();
                        DataAuditingInfo oldDataAuditingInfo = dataAuditingContract.GetModelInfo(commonNode.NodeId);
                        dataAuditingInfo.DataAuditingId = commonNode.NodeId;
                        dataAuditingInfo.GroupId = oldDataAuditingInfo.GroupId;
                        if (!dataAuditingInfo.DataAuditingName.Equals(oldDataAuditingInfo.DataAuditingName)
                            && dataAuditingContract.IsExistIdenticalName(commonNode.NodeId, dataAuditingInfo.DataAuditingName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一分类下的{0}名称不允许重复, {0}名称[{1}]已存在。", 
                                dataAuditingName, oldDataAuditingInfo.DataAuditingName);
                            return false;
                        }
                        dataAuditingContract.Update(dataAuditingInfo);
                        name = dataAuditingInfo.DataAuditingName;
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

            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 业务 */
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
                    CommonNodeContract = dataAuditingContract;
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
                    dataAuditingModule.Visible = false;
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
                    TreeNodeShow = dataAuditingModule;
                    dataAuditingModule.Visible = true;
                    groupModule.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// 更新相关的字段
        /// </summary>
        /// <param name="dataAuditingInfo"></param>        /
        private void UpdateDataFields(DataAuditingInfo dataAuditingInfo)
        {
            IList<CommonNode> commonNodes = dataAuditingContract.GetDataFields(dataAuditingInfo.DataAuditingId);
            CommonListItemsForm frmCommonListItems = new CommonListItemsForm();            
            string tableName = string.Empty;
            DataAuditingInfo mainDataAuditingInfo = dataAuditingContract.GetModelInfo(dataAuditingInfo.ParentDataAuditingId);
            FormType formType = (FormType)mainDataAuditingInfo.TableType;
            switch (formType)
            {
                case FormType.Table:
                    tableName = customTableContract.GetTableLogicalName(mainDataAuditingInfo.TableId);
                    break;

                case FormType.CombinedTable:
                    tableName = combinedTableContract.GetNodeNameByNodeId(mainDataAuditingInfo.CombinedTableId);
                    break;

                default:
                    throw new ArgumentException("不支持该属性。");
            }
            frmCommonListItems.CreateItmes = delegate (ListBoxControl lstItems)
            {
                CheckedSelectedItemsForm frmCheckedSelectedItems = new CheckedSelectedItemsForm();
                frmCheckedSelectedItems.MultiNodeSelected = delegate (IList<CommonNode> selectedNodes)
                {
                    List<CommonNode> innerNodes = new List<CommonNode>();
                    foreach (var item in lstItems.Items)
                    {
                        CommonNode node = item as CommonNode;
                        innerNodes.Add(node);
                    }
                    foreach (var selectedNode in selectedNodes)
                    {
                        int pos = innerNodes.FindIndex(innerNode => innerNode.NodeId == selectedNode.NodeId);
                        if (pos < 0)
                        {
                            lstItems.Items.Add(selectedNode);
                        }
                    }
                };
                frmCheckedSelectedItems.SelectAllItemVisible = true;
                IList<CommonNode> nodes = null;
                switch (formType)
                {
                    case FormType.Table:
                        nodes = customDataFieldContract.GetCommonNodes(mainDataAuditingInfo.TableId, DataFieldFilter.OnlyPhysicalField);
                        break;

                    case FormType.CombinedTable:
                        nodes = combinedTableContract.GetDataFields(mainDataAuditingInfo.CombinedTableId);
                        break;

                    default:
                        throw new ArgumentException("不支持该属性。");
                }
                frmCheckedSelectedItems.LoadAndSetCommonNodes(nodes);
                frmCheckedSelectedItems.ShowDialog();
            };
            frmCommonListItems.GetItems = delegate (IList<CommonNode> nodes)
            {
                IList<DataAuditingAndDataFieldInfo> dataAuditingAndDataFieldInfos = new List<DataAuditingAndDataFieldInfo>();
                int sorting = 1;
                foreach (var obj in nodes)
                {
                    int index = dataAuditingAndDataFieldInfos.FindIndex(dataAuditingAndDataFieldInfo => dataAuditingAndDataFieldInfo.DataFieldId == obj.NodeId);
                    if (index < 0)
                    {
                        dataAuditingAndDataFieldInfos.Add(new DataAuditingAndDataFieldInfo(dataAuditingInfo.DataAuditingId, obj.NodeId, sorting++));
                    }
                }
                dataAuditingContract.UpdateDataFields(dataAuditingInfo.DataAuditingId, dataAuditingAndDataFieldInfos);
            };
            frmCommonListItems.Text = string.Format("{0}的字段选择", tableName);
            frmCommonListItems.ToolTip = string.Format("{0}的字段列表", tableName);
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
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, UserEnumHelper.GetEnumText(GroupType), string.Empty, false, (byte)GroupType));
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
