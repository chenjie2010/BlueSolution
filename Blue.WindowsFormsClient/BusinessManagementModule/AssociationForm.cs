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
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class AssociationForm :  TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly ICustomDataFieldContract customDataFieldContract;

        #endregion

        #region 私有常量

        private const int MAX_STRING_LENG_IN_DATA_FIELD = 4000;

        #endregion     

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly AssociationModule associationModule;
        private readonly AssociatedDataFieldModule associatedDataFieldModule;

        #endregion

        #region 窗体和控件的方法 

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssociationForm()
        {
            InitializeComponent();

            SettingCaption = "关联数据设置";
            NullValuePrompt = "请输入关联名称或者关联编码";
            ExchangeItemEnabled = false;

            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            CommonNodeContract = customAssociationContract;

            IList<UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { Visible = true, CommonNodeContract = customGroupContract };
            TreeNodeShow = groupModule;
            userControls.Add(groupModule);

            associationModule = new AssociationModule() { Visible = false, CustomAssociationContract = customAssociationContract };
            userControls.Add(associationModule);

            associatedDataFieldModule = new AssociatedDataFieldModule() { Visible = false, AssociatedDataFieldContract = associatedDataFieldContract,
                CustomDataFieldContract = customDataFieldContract };
            userControls.Add(associatedDataFieldModule);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 4;  /*  允许最大层次 */
            Tip = "提示信息：布尔类型字段不能做主关联字段，每个表仅含一个主关联字段。";
            AddControls(userControls);
        }

        private void AssociationForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 节点选择前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                /* 查询节点：分类节点和关联名称节点 */
                if (commonNode.ParentNodeId > 0)
                {
                    groupModule.Visible = false;
                    associationModule.Visible = true;
                    associatedDataFieldModule.Visible = false;
                    TreeNodeShow = associationModule;
                }
                else
                {
                    groupModule.Visible = true;
                    associationModule.Visible = false;
                    associatedDataFieldModule.Visible = false;
                    TreeNodeShow = groupModule;
                }
                SettingEnabled = false;
                ExchangeItemEnabled = false;
            }
            else
            {
                AssociationNodeType associationNodeType = GetNodeType(e.Node.Level);
                if (associationNodeType == AssociationNodeType.AssociationName)
                {
                    SettingEnabled = true;
                }
                else
                {
                    SettingEnabled = false;
                }
                if (associationNodeType == AssociationNodeType.Root || associationNodeType == AssociationNodeType.ParentCategory
                    || associationNodeType == AssociationNodeType.ChildCategory)
                {
                    ExchangeItemEnabled = true;
                }
                else
                {
                    ExchangeItemEnabled = false;
                }
                SetCommonNodeContract(associationNodeType);
                SetParametersOnPanel(associationNodeType);
            }             
        }

        /// <summary>
        /// 数据导入导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationForm_OnExchangeClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            AssociationNodeType associationNodeType = GetNodeType(e.TreeNode.Level);
            AssociationExchanged associationExchanged = new AssociationExchanged("关联数据", commonNode.NodeId, commonNode.NodeCode, 
                "GroupCode ASC", associationNodeType, customGroupContract, customAssociationContract, associatedDataFieldContract);
            DataExchangeModeForm frmDataExchangeMode = new DataExchangeModeForm()
            {
                Tip = string.Format("当前选择节点：{0}；关联分类与关联数据的导入与选择的节点无关。", commonNode.NodeName),
                DataExportedInterface = associationExchanged,
                RefreshForm = ()=>
                {
                    InitFirstLevelNodes();
                }
            };
            frmDataExchangeMode.ShowDialog();
        }

        /// <summary>
        /// 查询之前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = customAssociationContract;
        }

        /// <summary>
        /// 创建前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            AssociationNodeType asociationNodeType = GetNodeType(e.TreeNode.Level + 1);
            SetCommonNodeContract(asociationNodeType);
            SetParametersOnPanel(asociationNodeType);
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            bool result = false;
            string verifyResult = string.Empty;
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            AssociationNodeType associationNodeType = GetNodeType(e.TreeNode.Level);
            Cursor = Cursors.WaitCursor;
            switch (e.EditState)
            {
                case EditState.Add:
                    result = AddTreeNode(commonNode, associationNodeType, ref verifyResult);
                    break;

                case EditState.Edit:
                    result = EditTreeNode(commonNode, associationNodeType, ref verifyResult);
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
        private void AssociationForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
        {
            if (!CurrentQueriedState)
            {
                Cursor = Cursors.WaitCursor;
                SetParametersOnPanel(GetNodeType(e.TreeNode.Level));
                Cursor = Cursors.Default;
            }

        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            int count = 0;
            AssociationNodeType associationNodeType = GetNodeType(e.TreeNode.Level);
            switch (associationNodeType)
            {
                case AssociationNodeType.ParentCategory:
                    /* 进行业务检查，不允许删除有关联小类的节点 */
                    count = customGroupContract.GetTotalCountOfChildNode(commonNode.NodeId);                    
                    if (count > 0)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("{0}节点下有{1}个关联小类，请删除关联小类。", commonNode.NodeName, count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    customGroupContract.Delete(commonNode.NodeId);
                    break;

                case AssociationNodeType.ChildCategory:
                    count = customAssociationContract.GetTotalCountOfChildNode(commonNode.NodeId);
                    if (count > 0)
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("共有{0}个关联属于[{0}]，请先删除这些关联。", count, commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    customGroupContract.Delete(commonNode.NodeId);
                    break;

                case AssociationNodeType.AssociationName:
                    if (!customAssociationContract.HasDataFieldConnected(commonNode.NodeId))
                    {
                        customAssociationContract.Delete(commonNode.NodeId);
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("该关联下的存在关联字段正在被物理字段作为关联字段类型使用，不能删除。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;

                case AssociationNodeType.AssociationDataField:
                    count = customDataFieldContract.GetDataFieldCountConnected(commonNode.NodeId);
                    if (count == 0)
                    {
                        associatedDataFieldContract.Delete(commonNode.NodeId);
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("该关联字段正在被{0}个物理字段作为关联字段类型使用，不能删除。", count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    break;
            }
            DeleteNode();
            Cursor = Cursors.Default;
            MessageBox.Show(string.Format("节点[{0}]删除成功。", commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 设置关联值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            AssociationNodeType nodeType = GetNodeType(e.TreeNode.Level);
            if (nodeType == AssociationNodeType.AssociationName)
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                AssociationDataForm frmAssociationData = new AssociationDataForm();
                frmAssociationData.AssociationId = commonNode.NodeId;
                frmAssociationData.ShowDialog();
            }
        }


        /// <summary>
        /// 设置关联字段的物理字段个数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationForm_OnAfterSelectOfTreeView(object sender, TreeViewEventArgs e)
        {
            AssociationNodeType associationNodeType = GetNodeType(e.Node.Level);
            if (associationNodeType == AssociationNodeType.AssociationDataField)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                int count = customDataFieldContract.GetDataFieldCountConnected(commonNode.NodeId);
                associatedDataFieldModule.SeTextOfDataFieldList(count);
            }            
        }

        #endregion

        #region 私有方法


        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="associationNodeType"></param>
        private void SetCommonNodeContract(AssociationNodeType associationNodeType)
        {
            /* 第一层为根节点 第二层为大分类节点，第三为小分类节点，第四层为关联，第五层为关联字段 */
            switch (associationNodeType)
            {
                case AssociationNodeType.Root:
                    CommonNodeContract = null;
                    break;

                case AssociationNodeType.ParentCategory:
                case AssociationNodeType.ChildCategory:
                    CommonNodeContract = customGroupContract;
                    break;

                case AssociationNodeType.AssociationName:
                    CommonNodeContract = customAssociationContract;
                    break;

                case AssociationNodeType.AssociationDataField:
                    CommonNodeContract = associatedDataFieldContract;
                    break;
            }
        }

        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="associationNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddTreeNode(CommonNode commonNode, AssociationNodeType associationNodeType, ref string verifyResult)
        {
            bool result = false;
            decimal nodeId = 0;
            string name = string.Empty;
            string value = string.Empty;
            string tip = string.Empty;

            switch (associationNodeType)
            {
                case AssociationNodeType.Root:
                case AssociationNodeType.ParentCategory:
                    ExtendedCommonNode extendedCommonNode = groupModule.GetModelInfo();
                    decimal parentGroupId = decimal.MinValue;
                    if (associationNodeType == AssociationNodeType.ParentCategory)
                    {
                        parentGroupId = commonNode.NodeId;
                    }
                    CustomGroupInfo customGroupInfo = new CustomGroupInfo()
                    {
                        ParentGroupId = parentGroupId,
                        GroupName = extendedCommonNode.NodeName,
                        GroupCode = extendedCommonNode.NodeCode,
                        GroupType = (byte)GroupType.Association,
                        Notes = extendedCommonNode.Notes,
                        IsLeaf = true
                    };
                    result = AddGroupInfo(customGroupInfo, ref verifyResult);
                    if (result)
                    {
                        nodeId = customGroupContract.Insert(customGroupInfo);
                        name = customGroupInfo.GroupName;
                        value = customGroupInfo.GroupCode;
                    }
                    break;


                case AssociationNodeType.ChildCategory:
                    CustomAssociationInfo customAssociationInfo = associationModule.GetModelInfo();
                    customAssociationInfo.GroupId = commonNode.NodeId;
                    result = AddCustomAssociationInfo(customAssociationInfo, associationNodeType, ref verifyResult);
                    if (result)
                    {
                        nodeId = customAssociationContract.Insert(customAssociationInfo);
                        name = customAssociationInfo.AssociationName;
                        value = customAssociationInfo.AssociationCode;
                    }
                    break;

                case AssociationNodeType.AssociationName:
                    AssociatedDataFieldInfo associatedDataFieldInfo = associatedDataFieldModule.GetModelInfo();
                    associatedDataFieldInfo.AssociationId = commonNode.NodeId;
                    result = AddAssociatedDataFieldInfo(associatedDataFieldInfo, ref verifyResult);
                    if (result)
                    {
                        nodeId = associatedDataFieldContract.Insert(associatedDataFieldInfo);
                        name = associatedDataFieldInfo.LogicalName;
                        value = associatedDataFieldInfo.DataFieldCode;
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
        /// 增加分组信息
        /// </summary>
        /// <param name="customGroupInfo"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddGroupInfo(CustomGroupInfo customGroupInfo, ref string verifyResult)
        {
            bool result = false;

            result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
            if (result && customGroupContract.IsExistIdenticalName(customGroupInfo.ParentGroupId, customGroupInfo.GroupName))
            {
                verifyResult = string.Format("同一数据仓库下分组名称不允许重复, 分组名称[{0}]已存在。", customGroupInfo.GroupName);
                return false;
            }

            return result;
        }

        /// <summary>
        /// 增加关联
        /// </summary>
        /// <param name="customAssociationInfo"></param>
        /// <param name="associationNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddCustomAssociationInfo(CustomAssociationInfo customAssociationInfo, AssociationNodeType associationNodeType, ref string verifyResult)
        {
            bool result = false;

            result = ValidationHelper.Validate<CustomAssociationInfo>(customAssociationInfo, out verifyResult);
            if (result)
            {
                if (customAssociationContract.IsExistIdenticalName(customAssociationInfo.GroupId, customAssociationInfo.AssociationName))
                {
                    verifyResult = string.Format("同一层节点的名称不允许重复, 该层节点中节点名称[{0}]已存在", customAssociationInfo.AssociationName);
                    return false;
                }
                if (customAssociationContract.IsExistIdenticalCode(customAssociationInfo.AssociationCode))
                {
                    verifyResult = string.Format("该关联类型的关联编码[{0}]已存在", customAssociationInfo.AssociationCode);
                    return false;
                }
            }

            return result;
        }


        /// <summary>
        /// 增加关联字段
        /// </summary>
        /// <param name="associatedDataFieldInfo"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddAssociatedDataFieldInfo(AssociatedDataFieldInfo associatedDataFieldInfo, ref string verifyResult)
        {
            bool result = false;

            result = ValidationHelper.Validate<AssociatedDataFieldInfo>(associatedDataFieldInfo, out verifyResult);
            if (result)
            {
                if (associatedDataFieldContract.IsExistIdenticalName(associatedDataFieldInfo.AssociationId, associatedDataFieldInfo.LogicalName))
                {
                    verifyResult = string.Format("同一层节点的名称不允许重复, 该层节点中节点名称[{0}]已存在。", associatedDataFieldInfo.LogicalName);
                    return false;
                }
                if (associatedDataFieldContract.IsExistIdenticalCode(associatedDataFieldInfo.DataFieldCode))
                {
                    verifyResult = string.Format("该关联字段的编码[{0}]已存在。", associatedDataFieldInfo.DataFieldCode);
                    return false;
                }
                if ((AssociatedDataFieldCategory)associatedDataFieldInfo.DataFieldCategory == AssociatedDataFieldCategory.PrimaryDataField
                    && (associatedDataFieldContract.GetAssociatedDataFieldCount(associatedDataFieldInfo.AssociationId, AssociatedDataFieldCategory.PrimaryDataField) > 0))
                {
                    verifyResult = "每个关联表只能含有一个主关联字段，该表已经包含主关联字段。";
                    return false;
                }
                BasedDataType basedDatType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                switch (basedDatType)
                {
                    case BasedDataType.String:
                        if ((associatedDataFieldInfo.DataLength <= 0 || associatedDataFieldInfo.DataLength > MAX_STRING_LENG_IN_DATA_FIELD))
                        {
                            verifyResult = string.Format("该关联字段的字段长度[{0}]不符合规定的要求。", associatedDataFieldInfo.DataLength);
                            return false;
                        }
                        break;

                    case BasedDataType.Boolean:
                        if ((AssociatedDataFieldCategory)associatedDataFieldInfo.DataFieldCategory == AssociatedDataFieldCategory.PrimaryDataField)
                        {
                            verifyResult = "布尔类型字段不允许做主字段类型。";
                            return false;
                        }
                        break;

                }                             
            }

            return result;
        }
       
        /// <summary>
        /// 编辑节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="associationNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool EditTreeNode(CommonNode commonNode, AssociationNodeType associationNodeType, ref string verifyResult)
        {
            bool result = false;
            string name = string.Empty;

            //针对查询后修改数据节点的处理
            if (associationNodeType == AssociationNodeType.Root && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId))
            {
                associationNodeType = AssociationNodeType.AssociationName;
            }            
            switch (associationNodeType)
            {
                case AssociationNodeType.ParentCategory:
                case AssociationNodeType.ChildCategory:
                    ExtendedCommonNode groupCommonNode = groupModule.GetModelInfo();                    
                    CustomGroupInfo customGroupInfo = new CustomGroupInfo()
                    {
                        GroupId = commonNode.NodeId,
                        GroupName = groupCommonNode.NodeName,
                        GroupCode = groupCommonNode.NodeCode,
                        GroupType = (byte)GroupType.Association,
                        Notes = groupCommonNode.Notes
                    };
                    name = groupCommonNode.NodeName;
                    result = EditGroupInfo(customGroupInfo, ref verifyResult);
                    break;

                case AssociationNodeType.AssociationName:
                    CustomAssociationInfo customAssociationInfo = associationModule.GetModelInfo();
                    customAssociationInfo.GroupId = commonNode.ParentNodeId;
                    customAssociationInfo.AssociationId = commonNode.NodeId;
                    name = customAssociationInfo.AssociationName;
                    result = EditCustomAssociationInfo(customAssociationInfo, ref verifyResult);
                    break;

                case AssociationNodeType.AssociationDataField:
                    AssociatedDataFieldInfo associatedDataFieldInfo = associatedDataFieldModule.GetModelInfo();
                    associatedDataFieldInfo.AssociatedDataFieldId = commonNode.NodeId;
                    associatedDataFieldInfo.AssociationId = commonNode.ParentNodeId;
                    name = associatedDataFieldInfo.LogicalName;
                    result = EditAssociatedDataFieldInfo(associatedDataFieldInfo, ref verifyResult);
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
        /// 编辑分组信息
        /// </summary>
        /// <param name="customGroupInfo"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool EditGroupInfo(CustomGroupInfo customGroupInfo, ref string verifyResult)
        {
            bool result = false;

            result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
            if (result)
            {
                CustomGroupInfo oldCustomGroupInfo = customGroupContract.GetModelInfo(customGroupInfo.GroupId);
                if (!customGroupInfo.GroupName.Equals(oldCustomGroupInfo.GroupName) && customGroupContract.IsExistIdenticalName(customGroupInfo.GroupId, customGroupInfo.GroupName))
                {
                    Cursor = Cursors.Default;
                    verifyResult = string.Format("同一分类下分组名称不允许重复, 分组名称[{ 0}]已存在。", customGroupInfo.GroupName);
                    return false;
                }
                customGroupContract.Update(customGroupInfo);                
            }

            return result;
        }

        /// <summary>
        /// 编辑关联字段
        /// </summary>
        /// <param name="associatedDataFieldInfo"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool EditAssociatedDataFieldInfo(AssociatedDataFieldInfo associatedDataFieldInfo, ref string verifyResult)
        {
            bool result = false;

            result = ValidationHelper.Validate<AssociatedDataFieldInfo>(associatedDataFieldInfo, out verifyResult);
            if (result)
            {
                AssociatedDataFieldInfo oldAssociatedDataFieldInfo = associatedDataFieldContract.GetModelInfo(associatedDataFieldInfo.AssociatedDataFieldId);
                if (!associatedDataFieldInfo.LogicalName.Equals(oldAssociatedDataFieldInfo.LogicalName) &&
                                associatedDataFieldContract.IsExistIdenticalName(associatedDataFieldInfo.AssociationId, associatedDataFieldInfo.LogicalName))
                {
                    verifyResult = string.Format("同一层节点的名称不允许重复, 该层节点中节点名称[{0}]已存在", associatedDataFieldInfo.LogicalName);
                    return false;
                }
                if (!oldAssociatedDataFieldInfo.DataFieldCode.Equals(oldAssociatedDataFieldInfo.DataFieldCode) &&
                                customAssociationContract.IsExistIdenticalCode(associatedDataFieldInfo.DataFieldCode))
                {
                    verifyResult = string.Format("该关联字段的关联编码[{0}]已存在", associatedDataFieldInfo.DataFieldCode);
                    return false;
                }
                BasedDataType basedDatType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                if (basedDatType == BasedDataType.String && (associatedDataFieldInfo.DataLength <= 0 || associatedDataFieldInfo.DataLength > MAX_STRING_LENG_IN_DATA_FIELD))
                {
                    verifyResult = string.Format("该关联字段的字段长度[{0}]不符合规定的要求。", associatedDataFieldInfo.DataLength);
                    return false;
                }
                if (oldAssociatedDataFieldInfo.DataFieldCategory != associatedDataFieldInfo.DataFieldCategory
                    && (AssociatedDataFieldCategory)associatedDataFieldInfo.DataFieldCategory == AssociatedDataFieldCategory.PrimaryDataField
                    && (associatedDataFieldContract.GetAssociatedDataFieldCount(associatedDataFieldInfo.AssociationId, AssociatedDataFieldCategory.PrimaryDataField) > 0))
                {
                    verifyResult = "每个关联表只能含有一个主关联字段，该表已经包含主关联字段。";
                    return false;
                }
                associatedDataFieldContract.Update(associatedDataFieldInfo);                
            }

            return result;
        }

        /// <summary>
        /// 编辑关联类型
        /// </summary>
        /// <param name="customAssociationInfo"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool EditCustomAssociationInfo(CustomAssociationInfo customAssociationInfo, ref string verifyResult)
        {
            bool result = false;

            result = ValidationHelper.Validate<CustomAssociationInfo>(customAssociationInfo, out verifyResult);
            if (result)
            {
                CustomAssociationInfo oldCustomAssociationInfo = customAssociationContract.GetModelInfo(customAssociationInfo.AssociationId);
                if (!customAssociationInfo.AssociationName.Equals(oldCustomAssociationInfo.AssociationName) &&
                                customAssociationContract.IsExistIdenticalName(customAssociationInfo.GroupId, customAssociationInfo.AssociationName))
                {
                    verifyResult = string.Format("同一层节点的名称不允许重复, 该层节点中节点名称[{0}]已存在", customAssociationInfo.AssociationName);
                    return false;
                }
                if (!customAssociationInfo.AssociationCode.Equals(oldCustomAssociationInfo.AssociationCode) &&
                                customAssociationContract.IsExistIdenticalCode(customAssociationInfo.AssociationCode))
                {
                    verifyResult = string.Format("该关联类型的关联编码[{0}]已存在", customAssociationInfo.AssociationCode);
                    return false;
                }
                customAssociationContract.Update(customAssociationInfo);                
            }

            return result;
        }

        /// <summary>
        /// 获得树形结构节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private AssociationNodeType GetNodeType(int level)
        {
            AssociationNodeType associationNodeType = AssociationNodeType.Root;

            /* 第一层为根节点 第二层为大分类节点，第三为小分类节点，第四层为关联，第五层为关联字段 */
            switch (level)
            {
                case 0:
                    associationNodeType = AssociationNodeType.Root;
                    break;

                case 1:
                    associationNodeType = AssociationNodeType.ParentCategory;
                    break;

                case 2:
                    associationNodeType = AssociationNodeType.ChildCategory;
                    break;

                case 3:
                    associationNodeType = AssociationNodeType.AssociationName;
                    break;

                case 4:
                    associationNodeType = AssociationNodeType.AssociationDataField;
                    break;
            }

            return associationNodeType;
        }

        /// <summary>
        /// 设置右侧面板相关参数
        /// </summary>
        /// <param name="associationNodeType"></param>
        private void SetParametersOnPanel(AssociationNodeType associationNodeType)
        {
            /* 第一层为根节点 第二层为大分类节点，第三为小分类节点，第四层为关联，第五层为关联字段 */
            switch (associationNodeType)
            {
                case AssociationNodeType.Root:
                case AssociationNodeType.ParentCategory:
                case AssociationNodeType.ChildCategory:
                    groupModule.Visible = true;
                    associationModule.Visible = false;
                    associatedDataFieldModule.Visible = false;
                    TreeNodeShow = groupModule;
                    switch (associationNodeType)
                    {
                        case AssociationNodeType.Root:
                            groupModule.LayerName = "业务名称";
                            groupModule.LayerCodeName = "业务编码";
                            break;

                        case AssociationNodeType.ParentCategory:
                            groupModule.LayerName = "大类名称";
                            groupModule.LayerCodeName = "大类编码";
                            break;

                        case AssociationNodeType.ChildCategory:
                            groupModule.LayerName = "小类名称";
                            groupModule.LayerCodeName = "小类编码";
                            break;
                    }
                    break;
                
                case AssociationNodeType.AssociationName:
                    groupModule.Visible = false;
                    associationModule.Visible = true;
                    associatedDataFieldModule.Visible = false;
                    TreeNodeShow = associationModule;
                    break;

                case AssociationNodeType.AssociationDataField:
                    groupModule.Visible = false;
                    associationModule.Visible = false;
                    associatedDataFieldModule.Visible = true;
                    TreeNodeShow = associatedDataFieldModule;
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
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "关联类型结构", string.Empty, false, (byte)GroupType.Association));
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
            AssociationNodeType movedNodeType = GetNodeType(movedTreeNode.Level);
            AssociationNodeType targeNodeType = GetNodeType(targeNode.Level);
            if (movedNodeType == AssociationNodeType.AssociationName && targeNodeType == AssociationNodeType.ChildCategory)
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

            AssociationNodeType associationNodeType = GetNodeType(level);

            if (associationNodeType == AssociationNodeType.AssociationName || associationNodeType == AssociationNodeType.AssociationDataField)
            {
                return result = false;
            }

            return result;
        }

        #endregion
    }
}
