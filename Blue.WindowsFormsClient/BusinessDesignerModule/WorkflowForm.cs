using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
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
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class WorkflowForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomWorkflowContract customWorkflowContract;
        private readonly ICustomWorkflowProcessContract customWorkflowProcessContract;
        private readonly ICustomDataContract customDataContract;
        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomViewContract cusctomViewContract;
        private readonly ICustomQueyContract customQueyContract;

        #endregion

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly WorkflowModule workflowModule;
        private readonly WorkflowProcessModule workflowProcessModule;

        #endregion

        #region 构造函数

        public WorkflowForm()
        {
            InitializeComponent();

            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customWorkflowContract = BusinessChannelFactory.CreateCustomWorkflowContract();
            customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            cusctomViewContract = BusinessChannelFactory.CreateCustomViewContract();
            customQueyContract = BusinessChannelFactory.CreateCustomQueyContract();

            IList <UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { LayerName = "分组名称：", LayerCodeName = "分组编码：", CommonNodeContract = customGroupContract };
            userControls.Add(groupModule);

            workflowModule = new WorkflowModule() { CustomWorkflowContract = customWorkflowContract, CustomDataContract = customDataContract,
            CustomRoleContract = customRoleContract, UserAccountContract = userAccountContract};
            userControls.Add(workflowModule);

            workflowProcessModule = new WorkflowProcessModule() { CustomWorkflowContract = customWorkflowContract, CustomWorkflowProcessContract = customWorkflowProcessContract,
                UserAccountContract = userAccountContract, CustomRoleContract = customRoleContract, CustomTableContract = customTableContract, CustomGroupContract = customGroupContract,
                CustomViewContract = cusctomViewContract, CustomQueyContract = customQueyContract };
            userControls.Add(workflowProcessModule);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 4;  /*  允许最大层次 */
            Tip = "请在创建工作流程成功后设置关联业务权限。";
            NullValuePrompt = "请输入工作流名称查询";
            AddControls(userControls);
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowForm_Load(object sender, EventArgs e)
        {
            SettingCaption = "选择节点设置";
            SettingEnabled = false;
            Tip = "提示：工作流处于未启用状态才允许修改工作流的步骤。";
        }

        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {       
            WorkflowNodeType workflowNodeType = GetNodeType(e.Node.Level);
            CommonNode commonNode = e.Node.Tag as CommonNode;
            if (CurrentQueriedState)
            {            
                if (commonNode.NodeId > 0)
                {
                    groupModule.Visible = false;
                    workflowModule.Visible = true;
                    workflowProcessModule.Visible = false;
                    TreeNodeShow = workflowModule;
                }
                else
                {
                    groupModule.Visible = true;
                    workflowModule.Visible = false;
                    workflowProcessModule.Visible = false;
                    TreeNodeShow = groupModule;
                }
            }
            else
            {
                SetCommonNodeContract(workflowNodeType);
                SetParametersOnPanel(workflowNodeType);
            }
            switch (workflowNodeType)
            {
                case WorkflowNodeType.Workflow:
                    SettingEnabled = true;                    
                    SettingCaption = "节点关系设置";
                    break;

                case WorkflowNodeType.WorkflowProcess:
                    if (commonNode.NodeId > 0)
                    {
                        WorkflowProcessType workflowProcessType = (WorkflowProcessType)customWorkflowProcessContract.GetProcessType(commonNode.NodeId);
                        if (workflowProcessType == WorkflowProcessType.SelectiveBranch)
                        {
                            SettingEnabled = true;
                        }
                        else
                        {
                            SettingEnabled = false;
                        }
                    }
                    else
                    {
                        SettingEnabled = false;
                    }
                    SettingCaption = "选择节点设置";
                    break;

                default:
                    SettingEnabled = false;
                    break;
            }
        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            WorkflowNodeType workflowNodeType = GetNodeType(e.TreeNode.Level + 1);
            SetCommonNodeContract(workflowNodeType);
            SetParametersOnPanel(workflowNodeType);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = customWorkflowContract;
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
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
        private void WorkflowForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            bool result = false;
            string verifyResult = string.Empty;
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            WorkflowNodeType workflowNodeType = GetNodeType(e.TreeNode.Level);
            Cursor = Cursors.WaitCursor;
            switch (e.EditState)
            {
                case EditState.Add:
                    result = AddTreeNode(commonNode, workflowNodeType, ref verifyResult);
                    break;

                case EditState.Edit:
                    result = EditTreeNode(commonNode, workflowNodeType, ref verifyResult);
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
        private void WorkflowForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {

        }
        
        /// <summary>
        /// 业务权限设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                WorkflowNodeType workflowNodeType = GetNodeType(e.TreeNode.Level);
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                switch (workflowNodeType)
                {
                    case WorkflowNodeType.Workflow:
                        WorkflowMapForm frmWorkflowMap = new WorkflowMapForm();
                        frmWorkflowMap.WorkflowId = commonNode.NodeId;
                        frmWorkflowMap.ShowDialog();
                        break;

                    case WorkflowNodeType.WorkflowProcess:
                        CustomWorkflowProcessInfo customWorkflowProcessInfo = customWorkflowProcessContract.GetModelInfo(commonNode.NodeId);
                        WorkflowProcessType workflowProcessType = (WorkflowProcessType)customWorkflowProcessInfo.ProcessType;
                        if (workflowProcessType == WorkflowProcessType.SelectiveBranch)
                        {
                            WorkflowDataFieldForm frmWorkflowDataField = new WorkflowDataFieldForm();
                            frmWorkflowDataField.ProcessId = customWorkflowProcessInfo.ProcessId;
                            frmWorkflowDataField.ShowDialog();                           
                        }
                        break;

                }

            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
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
        private bool AddTreeNode(CommonNode commonNode, WorkflowNodeType workflowNodeType, ref string verifyResult)
        {
            bool result = false;
            decimal nodeId = 0;
            string name = string.Empty;
            string value = string.Empty;
            string tip = string.Empty;

            switch (workflowNodeType)
            {
                case WorkflowNodeType.Root:
                case WorkflowNodeType.ParentCategory:
                    ExtendedCommonNode extendedCommonNode = groupModule.GetModelInfo();
                    CustomGroupInfo customGroupInfo = new CustomGroupInfo()
                    {
                        UserId = decimal.MinValue,
                        ParentGroupId = decimal.MinValue,
                        GroupName = extendedCommonNode.NodeName,
                        GroupCode = extendedCommonNode.NodeCode,
                        GroupType = (byte)GroupType.Workflow,
                        Notes = extendedCommonNode.Notes
                    };
                    if (workflowNodeType == WorkflowNodeType.ParentCategory)
                    {
                        customGroupInfo.ParentGroupId = commonNode.NodeId;
                    }
                    customGroupInfo.IsLeaf = true;
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        nodeId = customGroupContract.Insert(customGroupInfo);
                        name = customGroupInfo.GroupName;
                        value = customGroupInfo.GroupCode;
                    }
                    break;

                case WorkflowNodeType.ChildCategory:
                    result = workflowModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomWorkflowInfo customWorkflowInfo = workflowModule.GetModelInfo();
                        IList<ExtendedUpLoadFileInfo> upLoadFileInfos = workflowModule.GetAttachements();
                        customWorkflowInfo.GroupId = commonNode.NodeId;
                        if (customWorkflowContract.IsExistIdenticalName(commonNode.NodeId, customWorkflowInfo.WorkflowName))
                        {
                            verifyResult = string.Format("同一分类下工作流名称不允许重复, 工作流名称[{0}]已存在。", customWorkflowInfo.WorkflowName);
                            return false;
                        }
                        if (upLoadFileInfos == null)
                        {
                            nodeId = customWorkflowContract.Insert(customWorkflowInfo);
                        }
                        else
                        {
                            nodeId = customWorkflowContract.Insert(customWorkflowInfo, upLoadFileInfos);
                        }
                        name = customWorkflowInfo.WorkflowName;
                        value = customWorkflowInfo.WorkflowCode;
                    }
                    break;

                case WorkflowNodeType.Workflow:
                    result = workflowProcessModule.ValidateModelInfo(out verifyResult);                    
                    if (result)
                    {
                        CustomWorkflowProcessInfo customWorkflowProcessInfo = workflowProcessModule.GetModelInfo();
                        IList<ExtendedUpLoadFileInfo> upLoadFileInfos = workflowProcessModule.GetAttachements();
                        customWorkflowProcessInfo.WorkflowId = commonNode.NodeId;
                        if (customWorkflowProcessContract.IsExistIdenticalName(commonNode.NodeId, customWorkflowProcessInfo.ProcessName))
                        {
                            verifyResult = string.Format("同一工作流名称下的流程名称不允许重复, 流程名称名称[{0}]已存在。", customWorkflowProcessInfo.ProcessName);
                            return false;
                        }
                        if (upLoadFileInfos == null)
                        {
                            nodeId = customWorkflowProcessContract.Insert(customWorkflowProcessInfo);
                        }
                        else
                        {
                            nodeId = customWorkflowProcessContract.Insert(customWorkflowProcessInfo, upLoadFileInfos);
                        }
                        name = customWorkflowProcessInfo.ProcessName;
                        value = customWorkflowProcessInfo.ProcessCode;
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
        /// <param name="workflowNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool EditTreeNode(CommonNode commonNode, WorkflowNodeType workflowNodeType, ref string verifyResult)
        {
            bool result = false;
            string name = string.Empty;

            //针对查询后修改数据节点的处理
            if (workflowNodeType == WorkflowNodeType.Root && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId))
            {
                workflowNodeType = WorkflowNodeType.Workflow;
            }

            switch (workflowNodeType)
            {
                case WorkflowNodeType.ParentCategory:
                case WorkflowNodeType.ChildCategory:
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

                case WorkflowNodeType.Workflow:
                    result = workflowModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomWorkflowInfo customWorkflowInfo = workflowModule.GetModelInfo();
                        CustomWorkflowInfo oldCustomWorkflowInfo = customWorkflowContract.GetModelInfo(commonNode.NodeId);
                        customWorkflowInfo.GroupId = oldCustomWorkflowInfo.GroupId;                        
                        if (!customWorkflowInfo.WorkflowName.Equals(oldCustomWorkflowInfo.WorkflowName)
                            && customWorkflowContract.IsExistIdenticalName(commonNode.NodeId, customWorkflowInfo.WorkflowName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一分类下工作流名称不允许重复, 工作流名称[{0}]已存在。", customWorkflowInfo.WorkflowName);
                            return false;
                        }
                        if ((customWorkflowInfo.StructCategory != oldCustomWorkflowInfo.StructCategory))
                        {
                            int count = customWorkflowContract.GetParentNodeCount(customWorkflowInfo.WorkflowId);
                            if (count > 0)
                            {
                                verifyResult = string.Format("工作流名称[{0}]的节点在构成关系后，不能修改工作流结构。", customWorkflowInfo.WorkflowName);
                                return false;
                            }
                        }
                        IList<ExtendedUpLoadFileInfo> upLoadFileInfos = workflowModule.GetAttachements();
                        if (upLoadFileInfos == null)
                        {
                            customWorkflowContract.Update(customWorkflowInfo);
                        }
                        else
                        {
                            customWorkflowContract.Update(customWorkflowInfo, upLoadFileInfos);
                        }
                        name = customWorkflowInfo.WorkflowName;
                    }
                    break;

                case WorkflowNodeType.WorkflowProcess:
                    CustomWorkflowProcessInfo customWorkflowProcessInfo = workflowProcessModule.GetModelInfo();
                    CustomWorkflowProcessInfo oldCustomWorkflowProcessInfo = customWorkflowProcessContract.GetModelInfo(commonNode.NodeId);
                    customWorkflowProcessInfo.WorkflowId = oldCustomWorkflowProcessInfo.WorkflowId;
                    result = ValidationHelper.Validate<CustomWorkflowProcessInfo>(customWorkflowProcessInfo, out verifyResult);
                    if (result)
                    {
                        WorkflowProcessCategory workflowProcessCategory = (WorkflowProcessCategory)customWorkflowProcessInfo.ProcessCategory;
                        if (oldCustomWorkflowProcessInfo.IsRootNode && workflowProcessCategory != WorkflowProcessCategory.Begin)
                        {
                            verifyResult = string.Format("该流程[{0}]是开始节点，不允许更改为其他节点类别。", customWorkflowProcessInfo.ProcessName);
                            return false;
                        }
                        if (!customWorkflowProcessInfo.ProcessName.Equals(oldCustomWorkflowProcessInfo.ProcessName) && customWorkflowProcessContract.IsExistIdenticalName(commonNode.NodeId, customWorkflowProcessInfo.ProcessName))
                        {
                            verifyResult = string.Format("同一工作流名称下的流程名称不允许重复, 流程名称[{0}]已存在。", customWorkflowProcessInfo.ProcessName);
                            return false;
                        }
                        IList<ExtendedUpLoadFileInfo> upLoadFileInfos = workflowProcessModule.GetAttachements();
                        if (upLoadFileInfos == null)
                        {
                            customWorkflowProcessContract.Update(customWorkflowProcessInfo);
                        }
                        else
                        {
                            customWorkflowProcessContract.Update(customWorkflowProcessInfo, upLoadFileInfos);
                        }
                        name = customWorkflowProcessInfo.ProcessName;
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
        private WorkflowNodeType GetNodeType(int level)
        {
            WorkflowNodeType workflowNodeType = WorkflowNodeType.Root;

            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 工作流 (5) 工作流步骤 */
            switch (level)
            {
                case 0:
                    workflowNodeType = WorkflowNodeType.Root;
                    break;

                case 1:
                    workflowNodeType = WorkflowNodeType.ParentCategory;
                    break;

                case 2:
                    workflowNodeType = WorkflowNodeType.ChildCategory;
                    break;

                case 3:
                    workflowNodeType = WorkflowNodeType.Workflow;
                    break;

                case 4:
                    workflowNodeType = WorkflowNodeType.WorkflowProcess;
                    break;
            }

            return workflowNodeType;
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="workflowNodeType"></param>
        private void SetCommonNodeContract(WorkflowNodeType workflowNodeType)
        {
            switch (workflowNodeType)
            {
                case WorkflowNodeType.Root:
                    CommonNodeContract = null;
                    break;

                case WorkflowNodeType.ParentCategory:
                case WorkflowNodeType.ChildCategory:
                    CommonNodeContract = customGroupContract;
                    break;

                case WorkflowNodeType.Workflow:
                    CommonNodeContract = customWorkflowContract;
                    break;

                case WorkflowNodeType.WorkflowProcess:
                    CommonNodeContract = customWorkflowProcessContract;
                    break;
            }
        }

        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="workflowNodeType"></param>
        private void SetParametersOnPanel(WorkflowNodeType workflowNodeType)
        {
            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 查询业务 */
            switch (workflowNodeType)
            {
                case WorkflowNodeType.Root:
                case WorkflowNodeType.ParentCategory:
                case WorkflowNodeType.ChildCategory:
                    TreeNodeShow = groupModule;
                    groupModule.Visible = true;
                    workflowModule.Visible = false;
                    workflowProcessModule.Visible = false;
                    switch (workflowNodeType)
                    {
                        case WorkflowNodeType.Root:
                            groupModule.LayerName = "业务名称";
                            groupModule.LayerCodeName = "业务编码";
                            break;

                        case WorkflowNodeType.ParentCategory:
                            groupModule.LayerName = "大类名称";
                            groupModule.LayerCodeName = "大类编码";
                            break;

                        case WorkflowNodeType.ChildCategory:
                            groupModule.LayerName = "小类名称";
                            groupModule.LayerCodeName = "小类编码";
                            break;
                    }
                    break;

                case WorkflowNodeType.Workflow:
                    TreeNodeShow = workflowModule;
                    workflowModule.Visible = true;
                    groupModule.Visible = false;
                    workflowProcessModule.Visible = false;
                    break;

                case WorkflowNodeType.WorkflowProcess:
                    TreeNodeShow = workflowProcessModule;
                    workflowModule.Visible = false;
                    groupModule.Visible = false;
                    workflowProcessModule.Visible = true;
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
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "工作流业务", string.Empty, false, (byte)GroupType.Workflow));
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
            WorkflowNodeType movedTreeNodeType = GetNodeType(movedTreeNode.Level);
            WorkflowNodeType targeNodeTreeNodeType = GetNodeType(targeNode.Level);
            if (movedTreeNodeType == WorkflowNodeType.Workflow && targeNodeTreeNodeType == WorkflowNodeType.ChildCategory)
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

            WorkflowNodeType workflowNodeType = GetNodeType(level);

            if (workflowNodeType == WorkflowNodeType.Workflow || workflowNodeType == WorkflowNodeType.WorkflowProcess)
            {
                return result = false;
            }

            return result;
        }

        #endregion        
    }
}
