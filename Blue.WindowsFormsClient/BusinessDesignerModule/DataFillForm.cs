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
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class DataFillForm : TreeLayerForm
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomDataContract customDataContract;
        private readonly ICustomSectionContract customSectionContract;
        private readonly ICustomFormContract customFormContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICombinedTableContract combinedTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomViewContract customViewContract;
        private readonly ICustomQueyContract customQueyContract;
        private readonly ICustomRoleContract customRoleContract;
        private readonly ICustomBusinessContract customBusinessContract;
        private readonly ICustomReportContract customReportContract;

        #endregion

        #region  私有变量

        private readonly TreeLayerModule groupModule;
        private readonly DataFillModule dataFillModule;
        private readonly TreeLayerModule sectionModule;
        private readonly DataFormModule dataFormModule;

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>

        public DataFillForm()
        {
            InitializeComponent();

            SettingCaption = "字段关系设置";
            SettingEnabled = false;

            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customDataContract = BusinessChannelFactory.CreateCustomDataContract();
            customSectionContract = BusinessChannelFactory.CreateCustomSectionContract();
            customFormContract = BusinessChannelFactory.CreateCustomFormContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            combinedTableContract = BusinessChannelFactory.CreateCombinedTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customViewContract = BusinessChannelFactory.CreateCustomViewContract();
            customQueyContract = BusinessChannelFactory.CreateCustomQueyContract();
            customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
            customBusinessContract = BusinessChannelFactory.CreateCustomBusinessContract();
            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();

            IList<UserControl> userControls = new List<UserControl>();
            groupModule = new TreeLayerModule() { LayerName = "分组名称：", LayerCodeName = "分组编码：", CommonNodeContract = customGroupContract };
            userControls.Add(groupModule);

            sectionModule = new TreeLayerModule() { LayerName = "窗体名称：", LayerCodeName = "窗体编码：", CommonNodeContract = customSectionContract };
            userControls.Add(sectionModule);

            dataFillModule = new DataFillModule()
            { CustomDataContract = customDataContract, CustomGroupContract = customGroupContract, CustomTableContract = customTableContract,
                CustomViewContract = customViewContract, CustomDataFieldContract = customDataFieldContract, CustomQueyContract = customQueyContract,
                CustomRoleContract = customRoleContract, CustomReportContract = customReportContract
            };
            userControls.Add(dataFillModule);

            dataFormModule = new DataFormModule() {CustomGroupContract = customGroupContract, CustomFormContract = customFormContract, CustomTableContract = customTableContract,
                CombinedTableContract = combinedTableContract };
            userControls.Add(dataFormModule);

            /* 初始化属性 */
            allowedToFirstLevelNode = false; /* 不允许编辑根节点 */
            MaxLevel = 5;  /*  允许最大层次 */
            Tip = "请在创建数据填报成功后请进行字段关系设置。";
            NullValuePrompt = "请输入数据填报名称查询";
            AddControls(userControls);
        }

        /// <summary>
        /// 数据填报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFillForm_Load(object sender, EventArgs e)
        {

        }


        /// <summary>
        /// 节点选择之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFillForm_OnBeforeSelectOfTreeView(object sender, TreeViewCancelEventArgs e)
        {
            DataFillNodeType dataFillNodeType = GetNodeType(e.Node.Level);
            if (CurrentQueriedState)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                if (commonNode.NodeId > 0)
                {
                    groupModule.Visible = false;
                    dataFillModule.Visible = true;
                    dataFormModule.Visible = false;
                    TreeNodeShow = dataFillModule;
                }
                else
                {
                    groupModule.Visible = true;
                    dataFillModule.Visible = false;
                    dataFormModule.Visible = false;
                    TreeNodeShow = groupModule;
                }                
            }
            else
            {
                SetCommonNodeContract(dataFillNodeType);
                SetParametersOnPanel(dataFillNodeType);
            }
            if (dataFillNodeType == DataFillNodeType.CustomForm)
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
        private void DataFillForm_OnBeoreQueryClick(object sender, EventArgs e)
        {
            CommonNodeContract = customDataContract;
        }

        /// <summary>
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFillForm_OnBeforeTreeNodeExpand(object sender, TreeViewCancelEventArgs e)
        {
            SetCommonNodeContract(GetNodeType(e.Node.Level + 1));
        }

        /// <summary>
        /// 创建之前的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFillForm_OnBeoreCreatedClick(object sender, TreeNodeItemClickEventArgs e)
        {
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            DataFillNodeType dataFillNodeType = GetNodeType(e.TreeNode.Level + 1);
            SetCommonNodeContract(dataFillNodeType);
            SetParametersOnPanel(dataFillNodeType);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFillForm_OnDeleteClick(object sender, TreeNodeItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                DataFillNodeType dataFillNodeType = GetNodeType(e.TreeNode.Level);
                string warning = string.Empty;
                int count = 0;
                switch (dataFillNodeType)
                {
                    case DataFillNodeType.ParentCategory:
                        count = customGroupContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            warning = string.Format("该分组[{0}]下共有{1}个子分组，请先删除这些子分组。", commonNode.NodeName, count);
                        }
                        else
                        {
                            customGroupContract.Delete(commonNode.NodeId);
                        }
                        break;

                    case DataFillNodeType.ChildCategory:
                        count = customDataContract.GetTotalCountOfChildNode(commonNode.NodeId);
                        if (count > 0)
                        {
                            warning = string.Format("该子分组[{0}]下共有{1}个数据填报，请先删除这些数据填报。", commonNode.NodeName, count);
                        }
                        else
                        {
                            customGroupContract.Delete(commonNode.NodeId);
                        }
                        break;

                    case DataFillNodeType.CustomData:
                        count = customBusinessContract.GetTotalCount(commonNode.NodeId, BusinessMenu.UserData);
                        if (count > 0)
                        {
                            warning = string.Format("该数据填报[{0}]下共被{1}个业务使用，请解除该填报与这些业务的关系。", commonNode.NodeName, count);
                        }
                        else
                        {
                            customDataContract.Delete(commonNode.NodeId);
                        }
                        break;

                    case DataFillNodeType.CustomForm:
                        customSectionContract.Delete(commonNode.NodeId);
                        break;

                    case DataFillNodeType.CustomTable:
                        customFormContract.Delete(commonNode.NodeId);
                        break;                        
                }
                if (string.IsNullOrWhiteSpace(warning))
                {
                    DeleteNode();
                    Cursor = Cursors.Default;
                    MessageBox.Show(string.Format("所选择的节点[[{0}]删除成功。", commonNode.NodeName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show(warning, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFillForm_OnConfirmClick(object sender, TreeNodeEditEventArgs e)
        {
            bool result = false;
            string verifyResult = string.Empty;
            CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            DataFillNodeType dataFillNodeType = GetNodeType(e.TreeNode.Level);
            Cursor = Cursors.WaitCursor;
            switch (e.EditState)
            {
                case EditState.Add:
                    result = AddTreeNode(commonNode, dataFillNodeType, ref verifyResult);
                    break;

                case EditState.Edit:
                    result = EditTreeNode(commonNode, dataFillNodeType, ref verifyResult);
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
        private void DataFillForm_OnCancelClick(object sender, TreeNodeEditEventArgs e)
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
        private void DataFillForm_OnSettingClick(object sender, TreeNodeItemClickEventArgs e)
        {
            //DataFillNodeType nodeType = GetNodeType(e.TreeNode.Level);
            //if (nodeType == DataFillNodeType.CustomForm)
            //{
            //    CommonNode commonNode = e.TreeNode.Tag as CommonNode;
            //    DataFieldAuthorityForm frmDataFieldAuthority = new DataFieldAuthorityForm();
            //    frmDataFieldAuthority.FormId = commonNode.NodeId;
            //    frmDataFieldAuthority.TableAuthority = customFormContract.GetTableAuthority(commonNode.NodeId);
            //    frmDataFieldAuthority.ShowDialog();
            //}
        }

        /// <summary>
        /// 关系设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFillForm_OnCustomItemClick(object sender, TreeNodeItemClickEventArgs e)
        {

            DataFillNodeType nodeType = GetNodeType(e.TreeNode.Level);
            if (nodeType == DataFillNodeType.CustomForm)
            {
                CommonNode commonNode = e.TreeNode.Tag as CommonNode;
                DataFieldRelationForm frmDataFeildRelation = new DataFieldRelationForm();
                frmDataFeildRelation.ShowDialog();

                //CommonListItemsForm frmCommonListItems = new CommonListItemsForm();
                //frmCommonListItems.Text = "字段选择";
                //frmCommonListItems.ToolTip = "字段列表";
                //frmCommonListItems.CreateItmes = delegate (ListBoxControl lstItems)
                //{
                //    CheckedSelectedItemsForm frmCheckedSelectedItems = new CheckedSelectedItemsForm();
                //    frmCheckedSelectedItems.MultiNodeSelected = delegate (IList<CommonNode> selectedNodes)
                //    {
                //        lstItems.Items.AddRange(selectedNodes.ToArray());
                //    };

                //    CustomViewInfo customViewInfo = cusctomViewContract.GetModelInfo(commonNode.NodeId);
                //    IList<CommonNode> hasCommonNodes = cusctomViewContract.GetAssociatedDataFields(commonNode.NodeId);
                //    IList<decimal> tableIds = new List<decimal>();

                //    /* 1. 主表 */
                //    tableIds.Add(customViewInfo.TableId);
                //    /* 2. 从表 */
                //    IList<CustomViewAndTableInfo> customViewAndTableInfos = cusctomViewContract.GetAssociatedTables(commonNode.NodeId);
                //    foreach (var customViewAndTableInfo in customViewAndTableInfos)
                //    {
                //        tableIds.Add(customViewAndTableInfo.TableId);
                //    }
                //    IList<CommonNode> dataFieldCommonNodes = GetDataFieldCommonNodes(commonNode.NodeId, tableIds, hasCommonNodes);
                //    frmCheckedSelectedItems.LoadAndSetCommonNodes(dataFieldCommonNodes);
                //    frmCheckedSelectedItems.ShowDialog();
                //};
                //frmCommonListItems.GetItems = delegate (IList<CommonNode> nodes)
                //{
                //    IList<CustomViewAndDataFieldInfo> customViewAndDataFieldInfos = new List<CustomViewAndDataFieldInfo>();
                //    int sorting = 1;
                //    foreach (var obj in nodes)
                //    {
                //        customViewAndDataFieldInfos.Add(new CustomViewAndDataFieldInfo(commonNode.NodeId, obj.NodeId, sorting++));
                //    }
                //    cusctomViewContract.UpdateDataFields(commonNode.NodeId, customViewAndDataFieldInfos);
                //};
                //IList<CommonNode> commonNodes = cusctomViewContract.GetAssociatedDataFields(commonNode.NodeId);
                //frmCommonListItems.LoadItems(commonNodes);
                //frmCommonListItems.ShowDialog();
            }
        }

        #endregion

        #region 私有方法

        ///// <summary>
        ///// 获得未加入的字段名称列表
        ///// </summary>
        ///// <param name="viewId"></param>
        ///// <param name="tableIds"></param>
        ///// <param name="hasCommonNodes"></param>
        ///// <returns></returns>
        //private IList<CommonNode> GetDataFieldCommonNodes(decimal viewId, IList<decimal> tableIds, IList<CommonNode> hasCommonNodes)
        //{
        //    IList<CommonNode> dataFieldCommonNodes = new List<CommonNode>();            
        //    foreach (var tableId in tableIds)
        //    {
        //        IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodes(tableId, DataFieldFilter.All);
        //        string primaryTableName = customTableContract.GetTableLogicalName(tableId);
        //        foreach (var obj in commonNodes)
        //        {
        //            bool exist = false;
        //            foreach (var node in hasCommonNodes)
        //            {
        //                if (obj.NodeId == node.NodeId)
        //                {
        //                    exist = true;
        //                    break;
        //                }
        //            }
        //            if (!exist)
        //            {
        //                obj.NodeName = string.Format("[{0}][{1}]", primaryTableName, obj.NodeName);
        //                dataFieldCommonNodes.Add(obj);
        //            }
        //        }
        //    }

        //    return dataFieldCommonNodes;
        //}

        /// <summary>
        /// 增加节点
        /// </summary>
        /// <param name="commonNode"></param>
        /// <param name="associationNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool AddTreeNode(CommonNode commonNode, DataFillNodeType dataFillNodeType, ref string verifyResult)
        {
            bool result = false;
            decimal nodeId = 0;
            string name = string.Empty;
            string value = string.Empty;
            string tip = string.Empty;

            switch (dataFillNodeType)
            {
                case DataFillNodeType.Root:
                case DataFillNodeType.ParentCategory:
                    ExtendedCommonNode extendedCommonNode = groupModule.GetModelInfo();
                    CustomGroupInfo customGroupInfo = new CustomGroupInfo()
                    {
                        UserId = decimal.MinValue,
                        ParentGroupId = decimal.MinValue,
                        GroupName = extendedCommonNode.NodeName,
                        GroupCode = extendedCommonNode.NodeCode,
                        GroupType = (byte)GroupType.DataFill,
                        Notes = extendedCommonNode.Notes,
                        IsLeaf = true
                        
                    };
                    if (dataFillNodeType == DataFillNodeType.ParentCategory)
                    {
                        customGroupInfo.ParentGroupId = commonNode.NodeId;
                    }
                    result = ValidationHelper.Validate<CustomGroupInfo>(customGroupInfo, out verifyResult);
                    if (result)
                    {
                        if (customGroupContract.IsExistIdenticalName(commonNode.NodeId, customGroupInfo.GroupName, (byte)GroupType.DataFill))
                        {
                            verifyResult = string.Format("同一分类下的分组名称不允许重复, 分组名称[{0}]已存在。", customGroupInfo.GroupName);
                            return false;
                        }
                        nodeId = customGroupContract.Insert(customGroupInfo);
                        name = customGroupInfo.GroupName;
                        value = customGroupInfo.GroupCode;
                    }
                    break;

                case DataFillNodeType.ChildCategory:
                    result = dataFillModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomDataInfo customDataInfo = dataFillModule.GetModelInfo();
                        IList<ExtendedUpLoadFileInfo> upLoadFileInfos = dataFillModule.GetAttachements();
                        IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos = dataFillModule.GetConditionAttachements();
                        customDataInfo.GroupId = commonNode.NodeId;
                        if (customDataContract.IsExistIdenticalName(commonNode.NodeId, customDataInfo.DataName))
                        {
                            verifyResult = string.Format("同一分类下的数据填报名称不允许重复, 数据填报名称[{0}]已存在。", customDataInfo.DataName);
                            return false;
                        }
                        if (upLoadFileInfos == null && conditionalUpLoadFileInfos == null)
                        {
                            nodeId = customDataContract.Insert(customDataInfo);
                        }
                        else
                        {
                            nodeId = customDataContract.Insert(customDataInfo, upLoadFileInfos, conditionalUpLoadFileInfos);
                        }
                        name = customDataInfo.DataName;
                       value = customDataInfo.DataCode;
                    }
                    break;

                case DataFillNodeType.CustomData:
                    ExtendedCommonNode node = sectionModule.GetModelInfo();
                    CustomSectionInfo customSectionInfo = new CustomSectionInfo()
                    {
                        DataId = commonNode.NodeId,
                        SectionName = node.NodeName,
                        SectionCode = node.NodeCode,
                        Notes = node.Notes,
                        IsLeaf = true

                    };
                    result = ValidationHelper.Validate<CustomSectionInfo>(customSectionInfo, out verifyResult);
                    if (result)
                    {
                        if (customSectionContract.IsExistIdenticalName(commonNode.NodeId, customSectionInfo.SectionName))
                        {
                            verifyResult = string.Format("同一数据填报下的窗体名称不允许重复, 窗体名称[{0}]已存在。", customSectionInfo.SectionName);
                            return false;
                        }
                        nodeId = customSectionContract.Insert(customSectionInfo);
                        name = customSectionInfo.SectionName;
                        value = customSectionInfo.SectionCode;
                    }
                    break;

                case DataFillNodeType.CustomForm:
                    result = dataFormModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomFormInfo customFormInfo = dataFormModule.GetModelInfo();
                        IList<ExtendedUpLoadFileInfo> upLoadFileInfos = dataFormModule.GetAttachements();
                        customFormInfo.SectionId = commonNode.NodeId;
                        if (customFormContract.IsExistIdenticalName(commonNode.NodeId, customFormInfo.FormName))
                        {
                            verifyResult = string.Format("同一窗体下的数据表格名称不允许重复, 数据表格名称[{0}]已存在。", customFormInfo.FormName);
                            return false;
                        }
                        DataFilledProperty dataFilledProperty = (DataFilledProperty)customDataContract.GetDataFilledProperty(commonNode.ParentNodeId);
                        if (dataFilledProperty == DataFilledProperty.MultiUser && !customFormInfo.BusinessEnabled)
                        {
                            verifyResult = string.Format("在{0}总必须启用业务模式。", UserEnumHelper.GetEnumText(dataFilledProperty));
                            return false;
                        }
                        if (upLoadFileInfos == null)
                        {
                            nodeId = customFormContract.Insert(customFormInfo);
                        }
                        else
                        {
                            nodeId = customFormContract.Insert(customFormInfo, upLoadFileInfos);
                        }
                        name = customFormInfo.FormName;
                        value = customFormInfo.FormCode;
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
        /// <param name="dataFillNodeType"></param>
        /// <param name="verifyResult"></param>
        /// <returns></returns>
        private bool EditTreeNode(CommonNode commonNode, DataFillNodeType dataFillNodeType, ref string verifyResult)
        {
            bool result = false;
            string name = string.Empty;

            //针对查询后修改数据节点的处理
            if (dataFillNodeType == DataFillNodeType.Root && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId))
            {
                dataFillNodeType = DataFillNodeType.CustomData;
            }

            switch (dataFillNodeType)
            {
                case DataFillNodeType.ParentCategory:
                case DataFillNodeType.ChildCategory:
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
                        if (!customGroupInfo.GroupName.Equals(oldCustomGroupInfo.GroupName) && customGroupContract.IsExistIdenticalName(commonNode.NodeId, groupCommonNode.NodeName, (byte)GroupType.DataFill))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一分类下的分组名称不允许重复, 分组名称[{ 0}]已存在。", customGroupInfo.GroupName);
                            return false;
                        }
                        customGroupContract.Update(customGroupInfo);
                        name = customGroupInfo.GroupName;
                    }
                    break;

                case DataFillNodeType.CustomData:
                    result = dataFillModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomDataInfo customDataInfo = dataFillModule.GetModelInfo();
                        IList<ExtendedUpLoadFileInfo> upLoadFileInfos = dataFillModule.GetAttachements();
                        IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos = dataFillModule.GetConditionAttachements();
                        CustomDataInfo oldCustomDataInfo = customDataContract.GetModelInfo(commonNode.NodeId);
                        customDataInfo.GroupId = oldCustomDataInfo.GroupId;
                        if (!customDataInfo.DataName.Equals(oldCustomDataInfo.DataName) && customDataContract.IsExistIdenticalName(commonNode.NodeId, customDataInfo.DataName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一分类下的数据填报名称不允许重复, 数据填报名称[{0}]已存在。", customDataInfo.DataName);
                            return false;
                        }
                        if (upLoadFileInfos == null)
                        {
                            customDataContract.Update(customDataInfo);
                        }
                        else
                        {
                            customDataContract.Update(customDataInfo, upLoadFileInfos, conditionalUpLoadFileInfos);
                        }
                        name = customDataInfo.DataName;
                    }
                    break;

                case DataFillNodeType.CustomForm:
                    ExtendedCommonNode node = sectionModule.GetModelInfo();
                    CustomSectionInfo oldCustomSectionInfo = customSectionContract.GetModelInfo(commonNode.NodeId);
                    CustomSectionInfo customSectionInfo = new CustomSectionInfo()
                    {
                        SectionId = commonNode.NodeId,
                        DataId = oldCustomSectionInfo.DataId,
                        SectionName = node.NodeName,
                        SectionCode = node.NodeCode,
                        Notes = node.Notes
                    };
                    result = ValidationHelper.Validate<CustomSectionInfo>(customSectionInfo, out verifyResult);
                    if (result)
                    {
                        if (!customSectionInfo.SectionName.Equals(oldCustomSectionInfo.SectionName) && 
                            customSectionContract.IsExistIdenticalName(commonNode.NodeId, customSectionInfo.SectionName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一数据填报下的窗体名称不允许重复, 窗体名称[{ 0}]已存在。", customSectionInfo.SectionName);
                            return false;
                        }
                        customSectionContract.Update(customSectionInfo);
                        name = customSectionInfo.SectionName;
                    }
                    break;

                case DataFillNodeType.CustomTable:
                    result = dataFormModule.ValidateModelInfo(out verifyResult);
                    if (result)
                    {
                        CustomFormInfo customFormInfo = dataFormModule.GetModelInfo();
                        IList<ExtendedUpLoadFileInfo> upLoadFileInfos = dataFormModule.GetAttachements();
                        CustomFormInfo oldCustomFormInfo = customFormContract.GetModelInfo(commonNode.NodeId);
                        customFormInfo.SectionId = oldCustomFormInfo.SectionId;
                        if (!customFormInfo.FormName.Equals(oldCustomFormInfo.FormName) && customDataContract.IsExistIdenticalName(commonNode.NodeId, customFormInfo.FormName))
                        {
                            Cursor = Cursors.Default;
                            verifyResult = string.Format("同一数据填报下的数据表格名称不允许重复, 数据表格名称[{0}]已存在。", customFormInfo.FormName);
                            return false;
                        }
                        if (upLoadFileInfos == null)
                        {
                            customFormContract.Update(customFormInfo);
                        }
                        else
                        {
                            customFormContract.Update(customFormInfo, upLoadFileInfos);
                        }
                        name = customFormInfo.FormName;
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
        /// 获得数据填报业务节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private DataFillNodeType GetNodeType(int level)
        {
            DataFillNodeType dataFillNodeType = DataFillNodeType.Root;

            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 数据填报 (5)数据窗体 (6) 数据业务 */
            switch (level)
            {
                case 0:
                    dataFillNodeType = DataFillNodeType.Root;
                    break;

                case 1:
                    dataFillNodeType = DataFillNodeType.ParentCategory;
                    break;

                case 2:
                    dataFillNodeType = DataFillNodeType.ChildCategory;
                    break;

                case 3:
                    dataFillNodeType = DataFillNodeType.CustomData;
                    break;

                case 4:
                    dataFillNodeType = DataFillNodeType.CustomForm;
                    break;

                case 5:
                    dataFillNodeType = DataFillNodeType.CustomTable;
                    break;
            }

            return dataFillNodeType;
        }

        /// <summary>
        /// 设置契约
        /// </summary>
        /// <param name="dataFillNodeType"></param>
        private void SetCommonNodeContract(DataFillNodeType dataFillNodeType)
        {
            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 数据填报 (5)数据窗体 (6) 数据业务 */
            switch (dataFillNodeType)
            {
                case DataFillNodeType.Root:
                    CommonNodeContract = null;
                    break;

                case DataFillNodeType.ParentCategory:
                case DataFillNodeType.ChildCategory:
                    CommonNodeContract = customGroupContract;
                    break;

                case DataFillNodeType.CustomData:
                    CommonNodeContract = customDataContract;
                    break;

                case DataFillNodeType.CustomForm:
                    CommonNodeContract = customSectionContract;
                    break;

                case DataFillNodeType.CustomTable:
                    CommonNodeContract = customFormContract;
                    break;
            }
        }

        /// <summary>
        /// 设置面板
        /// </summary>
        /// <param name="dataFillNodeType"></param>
        private void SetParametersOnPanel(DataFillNodeType dataFillNodeType)
        {
            /* (1) 根节点 (2) 分组大类 (3) 分组小类 (4) 数据填报 (5)数据窗体 (6) 数据业务 */
            switch (dataFillNodeType)
            {
                case DataFillNodeType.Root:
                case DataFillNodeType.ParentCategory:
                case DataFillNodeType.ChildCategory:
                    TreeNodeShow = groupModule;
                    groupModule.Visible = true;
                    dataFillModule.Visible = false;
                    sectionModule.Visible = false;
                    dataFormModule.Visible = false;
                    switch (dataFillNodeType)
                    {
                        case DataFillNodeType.Root:
                            groupModule.LayerName = "填报名称";
                            groupModule.LayerCodeName = "填报编码";
                            break;

                        case DataFillNodeType.ParentCategory:
                            groupModule.LayerName = "大类名称";
                            groupModule.LayerCodeName = "大类编码";
                            break;

                        case DataFillNodeType.ChildCategory:
                            groupModule.LayerName = "小类名称";
                            groupModule.LayerCodeName = "小类编码";
                            break;
                    }
                    break;

                case DataFillNodeType.CustomData:
                    TreeNodeShow = dataFillModule;
                    dataFillModule.Visible = true;
                    sectionModule.Visible = false;
                    dataFormModule.Visible = false;
                    groupModule.Visible = false;
                    break;

                case DataFillNodeType.CustomForm:
                    TreeNodeShow = sectionModule;
                    dataFillModule.Visible = false;
                    sectionModule.Visible = true;
                    dataFormModule.Visible = false;                    
                    groupModule.Visible = false;
                    break;

                case DataFillNodeType.CustomTable:
                    TreeNodeShow = dataFormModule;
                    dataFillModule.Visible = false;
                    sectionModule.Visible = false;
                    dataFormModule.Visible = true;
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
            commonNodes.Add(new CommonNode(decimal.MinValue, decimal.MinValue, "数据填报业务", string.Empty, false, (byte)GroupType.DataFill));
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
            DataFillNodeType movedTreeNodeType = GetNodeType(movedTreeNode.Level);
            DataFillNodeType targeNodeTreeNodeType = GetNodeType(targeNode.Level);
            if (movedTreeNodeType == DataFillNodeType.CustomData && targeNodeTreeNodeType == DataFillNodeType.CustomData)
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

            DataFillNodeType dataFillNodeType = GetNodeType(level);

            if (dataFillNodeType == DataFillNodeType.CustomData || dataFillNodeType == DataFillNodeType.CustomForm 
                || dataFillNodeType == DataFillNodeType.CustomTable)
            {
                return result = false;
            }

            return result;
        }

        #endregion

    }
}
