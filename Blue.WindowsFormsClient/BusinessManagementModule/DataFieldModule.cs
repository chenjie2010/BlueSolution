using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary;
using Blue.WindowsFormsClient.Common;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class DataFieldModule : UserControl, ITreeNodeShow
    {
        #region 契约接口

        private readonly ICustomEnumContract customEnumContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly ICustomExpressionContract customExpressionContract;

        #endregion

        #region 私有变量

        private IList<CustomExpressionInfo> _customExpressionInfos = null;

        #endregion

        #region 属性

        /// <summary>
        /// 表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get; set;
        }

        /// <summary>
        /// 字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            get; set;
        }

        /// <summary>
        /// 字段性质
        /// </summary>
        public bool DataFieldPropertyOnReadOnly
        {
            get
            {
                return icmbDataFieldProperty.Properties.ReadOnly;
            }
            set
            {
                icmbDataFieldProperty.Properties.ReadOnly = value;
            }
        }

        /// <summary>
        /// 表的编号
        /// </summary>
        public decimal TableId
        {
            get;
            set;
        }

        /// <summary>
        /// 表达式字段列表
        /// </summary>
        public IList<CustomExpressionInfo> CustomExpressionInfos
        {
            get
            {
                return _customExpressionInfos;
            }
        }
        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataFieldModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            _customExpressionInfos = new List<CustomExpressionInfo>();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            customExpressionContract = BusinessChannelFactory.CreateCustomExpressionContract();
            SetActiveStatesOfControls(true);
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(DataFieldProperty));
            AddPropertiesToImageComboBoxEdit(icmbDataFieldProperty, enumItems);
            IList<EnumItem> physicalDataFieldProperties = UserEnumHelper.GetEnumItems(typeof(PhysicalDataFieldProperty));
            ccmbDataFieldAttributes.Properties.Items.AddRange(physicalDataFieldProperties.ToArray());
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(DataFieldSetting));
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFieldModule_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 设置帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleHelpContent_MouseClick(object sender, MouseEventArgs e)
        {
            TextForm frmText = new TextForm();
            frmText.Text = "工作流指南内容";
            frmText.AttachmentCategory = AttachmentCategory.None;
            if (txtHelpContent.Tag != null)
            {
                frmText.HtmlText = DataConvertionHelper.GetString(txtHelpContent.Tag);
            }
            else
            {
                frmText.HtmlText = string.Empty;
            }
            frmText.TextId = TreeNodeId;
            frmText.GetRichTextAndAttachments = delegate (string richText, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
            {
                txtHelpContent.Text = "[已设置]";
                txtHelpContent.Tag = richText;
            };
            frmText.ShowDialog();
        }

        /// <summary>
        /// 字段性质下拉选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbDataFieldProperty_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataFieldProperty dataFieldProperty = (DataFieldProperty)Convert.ToByte(icmbDataFieldProperty.EditValue);
            IList<CommonNode> commonNodes = DataFieldHelper.GetCommonNodes(dataFieldProperty);
            cmdDataFieldType.SelectedNode = null;
            cmdDataFieldType.TreeViewHandler.InitFullTreeNodes(commonNodes);
            InitDataFieldControlsProperties();
        }

        /// <summary>
        /// 字段类型变化后字段条件变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDataFieldType_AfterTreeNodeSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Parent == null || e.Node.Nodes.Count > 0 || cmdDataFieldType.ReadOnly)
            {
                return;
            }
            DataFieldProperty dataFieldProperty = (DataFieldProperty)Convert.ToByte(icmbDataFieldProperty.EditValue);
            byte dataFieldType = Convert.ToByte(cmdDataFieldType.SelectedNodeId);
            SetControlsProperty(dataFieldProperty, dataFieldType, false);
            txtMaxLength.Text = string.Empty;
            ceAutoComplete.Checked = false;
            txtConditionValue.Text = string.Empty;
            txtConditionValue.Tag = null;
        }

        /// <summary>
        /// 设置字段条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleDataFieldSetting_MouseClick(object sender, MouseEventArgs e)
        {
            if (icmbDataFieldProperty.EditValue != null && cmdDataFieldType.SelectedNode != null)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)Convert.ToByte(icmbDataFieldProperty.EditValue);
                if (cmdDataFieldType.SelectedNode == null)
                {
                    return;
                }
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.PhysicalDataField:
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)cmdDataFieldType.SelectedNodeId;
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.DropdownListEnum:
                            case PhysicalDataFieldType.DropdownListEnumValue:
                            case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                            case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                            case PhysicalDataFieldType.TreeViewEnum:
                            case PhysicalDataFieldType.TreeViewEnumValue:
                            case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                            case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                            case PhysicalDataFieldType.CscadeEnum:
                            case PhysicalDataFieldType.MultiSelectedEnum:
                                TreeSelectedItemsForm frmTreeSelectedItems = new TreeSelectedItemsForm();
                                frmTreeSelectedItems.CommonNodeContract = customEnumContract;
                                frmTreeSelectedItems.NodeSelected = delegate (CommonNode node)
                                {
                                    SetEnumNameOnCondtionValue(node.NodeId);
                                };
                                frmTreeSelectedItems.ShowDialog();
                                break;

                            case PhysicalDataFieldType.EnumValue:
                            case PhysicalDataFieldType.EnumNameDependency:
                            case PhysicalDataFieldType.FstAdditionalCode:
                            case PhysicalDataFieldType.ScdAdditionalCode:
                            case PhysicalDataFieldType.FstAdditionalString:
                            case PhysicalDataFieldType.ScdAdditionalString:
                            case PhysicalDataFieldType.TrdAdditionalString:
                            case PhysicalDataFieldType.FourthAdditionalString:
                            case PhysicalDataFieldType.FifthAdditionalString:
                            case PhysicalDataFieldType.SixthAdditionalString:
                            case PhysicalDataFieldType.FstAdditionalInteger:
                            case PhysicalDataFieldType.ScdAdditionalInteger:
                            case PhysicalDataFieldType.FstAdditionalDecimal:
                            case PhysicalDataFieldType.ScdAdditionalDecimal:
                            case PhysicalDataFieldType.DepartmentValue:
                            case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                            case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                                IList<CommonNode> commonNodes = null;
                                switch (physicalDataFieldType)
                                {
                                    case PhysicalDataFieldType.DepartmentValue:
                                    case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                                    case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                                        commonNodes = CustomDataFieldContract.GetCommonNodes(TableId, DataFieldFilter.OnlyDepartmentPhysicalField);
                                        break;

                                    default:
                                        commonNodes = CustomDataFieldContract.GetCommonNodes(TableId, DataFieldFilter.EnumTypeInPhysicalField);
                                        break;
                                }
                                DropDownSelectedItemsForm frmDropDownSelectedItems = new DropDownSelectedItemsForm();
                                frmDropDownSelectedItems.LoadCommonNodes(commonNodes);
                                frmDropDownSelectedItems.NodeSelected = delegate (CommonNode node)
                                {
                                    SetDataFieldNameOnCondtionValue(node.NodeId, node.NodeName);
                                };
                                frmDropDownSelectedItems.ShowDialog();
                                break;

                            case PhysicalDataFieldType.Association:
                            case PhysicalDataFieldType.PrimaryAssociation:
                                AssociatedDataFieldSelectedItemsForm frmAssociatedDataField = new AssociatedDataFieldSelectedItemsForm()
                                {
                                    AssociatedDataFieldCategory = AssociatedDataFieldCategory.PrimaryDataField
                                };
                                frmAssociatedDataField.NodeSelected = delegate (CommonNode node)
                                {
                                    SetAssociatedDataFieldOnCondtionValue(node.NodeId);
                                };
                                frmAssociatedDataField.ShowDialog();
                                break;

                            case PhysicalDataFieldType.SecondaryAssociation:
                                DataFieldItemsForm frmDataFieldItems = new DataFieldItemsForm();
                                frmDataFieldItems.DataFieldShowMode = DataFieldShowMode.Database;
                                frmDataFieldItems.DatabaseId = CustomTableContract.GetDatabaseId(TableId);
                                frmDataFieldItems.DataFieldFilter = DataFieldFilter.PrimaryAssociationInPhysicalField;
                                frmDataFieldItems.NodeSelected = delegate (CommonNode node)
                                {
                                    cmbAssociatedDataField.Properties.Items.Clear();
                                    cmbAssociatedDataField.EditValue = null;
                                    if (node != null)
                                    {
                                        SetDataFieldNameOnCondtionValue(node.NodeId);
                                        decimal associatedDataFieldId = CustomDataFieldContract.GetAssociatedDataFieldId(node.NodeId);
                                        decimal associationId = associatedDataFieldContract.GetAssociationId(associatedDataFieldId);
                                        IList<CommonNode> associatedDataFieldCommonNodes = associatedDataFieldContract.GetChildNodes(associationId, (byte)AssociatedDataFieldCategory.AssociatedDataField);
                                        cmbAssociatedDataField.Properties.Items.AddRange(associatedDataFieldCommonNodes.ToArray());
                                    }
                                };
                                frmDataFieldItems.ShowDialog();
                                break;
                        }
                        break;

                    case DataFieldProperty.LogicalDataField:
                        LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)cmdDataFieldType.SelectedNodeId;
                        switch (logicalDataFieldType)
                        {
                            case LogicalDataFieldType.OneDimCode:
                                IList<CommonNode> commonNodes = CustomDataFieldContract.GetCommonNodes(TableId, DataFieldFilter.OneDimCodeTypeInPhysicalField);
                                DropDownSelectedItemsForm frmDropDownSelectedItems = new DropDownSelectedItemsForm();
                                frmDropDownSelectedItems.LoadCommonNodes(commonNodes);
                                frmDropDownSelectedItems.NodeSelected = delegate (CommonNode node)
                                {
                                    SetDataFieldNameOnCondtionValue(node.NodeId, node.NodeName);
                                };
                                frmDropDownSelectedItems.ShowDialog();
                                break;

                            case LogicalDataFieldType.StringExpression:
                            case LogicalDataFieldType.DigitExpression:
                            case LogicalDataFieldType.DateTimeExpression:
                            case LogicalDataFieldType.TwoDimCode:
                                DataFieldFilter dataFieldFilter = DataFieldFilter.OnlyPhysicalField;
                                //switch (logicalDataFieldType)
                                //{
                                //    case LogicalDataFieldType.DigitExpression:
                                //        dataFieldFilter = DataFieldFilter.DigtalTypeInPhysicalField;
                                //        break;

                                //    case LogicalDataFieldType.DateTimeExpression:
                                //        dataFieldFilter = DataFieldFilter.DateTimeTypeInPhysicalField;
                                //        break;

                                //    case LogicalDataFieldType.StringExpression:
                                //    case LogicalDataFieldType.TwoDimCode:
                                //        dataFieldFilter = DataFieldFilter.OnlyPhysicalField;
                                //        break;
                                //}
                                /* 字段设置 */
                                IList<CommonNode> selectedCommonNodes = txtConditionValue.Tag as IList<CommonNode>;
                                ExpressionForm frmExpression = new ExpressionForm();
                                if (logicalDataFieldType == LogicalDataFieldType.TwoDimCode)
                                {
                                    frmExpression.Text = "二维码字段设置";
                                    frmExpression.Caption = "二维码内容";
                                }
                                else
                                {
                                    frmExpression.Text = "表达式设置";
                                    frmExpression.Caption = "表达式内容设置";
                                }
                                frmExpression.TableId = TableId;
                                frmExpression.DataFieldFilter = dataFieldFilter;
                                frmExpression.ExpressionText = txtConditionValue.Text;
                                frmExpression.ValidateExpressionDataFields = delegate (IList<CommonNode> nodes, string text, out string warning)
                                {
                                    warning = string.Empty;
                                    bool result = CustomDataFieldContract.VerifyExpression(TableId, text, nodes);
                                    if (!result)
                                    {
                                        warning = "验证失败，请检查";
                                    }

                                    return result;
                                };
                                frmExpression.GetExpressionDataFields = (nodes, text) =>
                                {
                                    txtConditionValue.Tag = nodes;
                                    txtConditionValue.Text = text;
                                };
                                frmExpression.CustomDataFieldContract = CustomDataFieldContract;
                                frmExpression.LoadAndSetDataFields(selectedCommonNodes);
                                frmExpression.ShowDialog();
                                break;
                        }
                        break;
                }
            }
        }

        #endregion

        #region 实现 ITreeNodeShow 接口

        /// <summary>
        /// 节点编号
        /// </summary>
        public decimal TreeNodeId
        {
            get;
            set;
        }

        /// <summary>
        /// 默认编码
        /// </summary>
        public string DefaultCode
        {
            set
            {
                txtDataFieldCode.Text = value;
            }
            get
            {
                return txtDataFieldCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtLogicalName.ReadOnly = readOnly;
            icmbDataFieldProperty.Properties.ReadOnly = readOnly;
            cmdDataFieldType.ReadOnly = readOnly;
            if (cmdDataFieldType.SelectedNode != null)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)Convert.ToByte(icmbDataFieldProperty.EditValue);
                byte dataFieldType = Convert.ToByte(cmdDataFieldType.SelectedNodeId);
                SetControlsProperty(dataFieldProperty, dataFieldType, readOnly);
            }
            else
            {
                InitDataFieldControlsProperties();
            }
            chkHelpEnabled.ReadOnly = readOnly;
            hleHelpContent.Enabled = !readOnly;
            txtToolTip.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;

            if (!txtLogicalName.ReadOnly)
            {
                txtLogicalName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            if (commonNode != null)
            {
                if (commonNode.NodeId > 0)
                {
                    TreeNodeId = commonNode.NodeId;
                    /* 物理字段与逻辑字段 */
                    CustomDataFieldInfo customDataFieldInfo = CustomDataFieldContract.GetModelInfo(commonNode.NodeId);
                    txtLogicalName.Text = customDataFieldInfo.LogicalName;
                    txtPhysicalName.Text = customDataFieldInfo.PhysicalName;
                    txtDataFieldCode.Text = customDataFieldInfo.DataFieldCode;
                    icmbDataFieldProperty.EditValue = customDataFieldInfo.DataFieldProperty;
                    cmdDataFieldType.SelectedNodeId = customDataFieldInfo.DataFieldType;
                    UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataFieldSetting, customDataFieldInfo.DataFieldSetting);
                    ceAutoComplete.Checked = customDataFieldInfo.AutoComplete;
                    chkHelpEnabled.Checked = customDataFieldInfo.HelpEnabled;
                    if (!string.IsNullOrWhiteSpace(customDataFieldInfo.HelpContent))
                    {
                        txtHelpContent.Text = "[已设置]";
                        txtHelpContent.Tag = customDataFieldInfo.HelpContent;
                    }
                    else
                    {
                        txtHelpContent.Text = string.Empty;
                        txtHelpContent.Tag = null;
                    }
                    txtToolTip.Text = customDataFieldInfo.Tooltip;
                    txtNotes.Text = customDataFieldInfo.Notes;
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.PhysicalDataField:
                            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                            foreach (CheckedListBoxItem item in ccmbDataFieldAttributes.Properties.Items)
                            {
                                EnumItem enumItem = (EnumItem)item.Value;
                                PhysicalDataFieldProperty physicalDataFieldProperty = (PhysicalDataFieldProperty)enumItem.Value;
                                switch (physicalDataFieldProperty)
                                {
                                    case PhysicalDataFieldProperty.Required:
                                        if (customDataFieldInfo.RequiredDataField)
                                        {
                                            item.CheckState = CheckState.Checked;
                                        }
                                        else
                                        {
                                            item.CheckState = CheckState.Unchecked;
                                        }
                                        break;

                                    case PhysicalDataFieldProperty.Index:
                                        if (customDataFieldInfo.IndexCreated)
                                        {
                                            item.CheckState = CheckState.Checked;
                                        }
                                        else
                                        {
                                            item.CheckState = CheckState.Unchecked;
                                        }
                                        break;
                                }
                            }
                            switch (physicalDataFieldType)
                            {
                                case PhysicalDataFieldType.Boolean:
                                case PhysicalDataFieldType.Int32:
                                    txtConditionValue.Text = string.Empty;
                                    break;

                                case PhysicalDataFieldType.Decimal:
                                    txtConditionValue.Text = string.Empty;
                                    txtMaxLength.Text = customDataFieldInfo.DataFieldLength.ToString();
                                    break;

                                case PhysicalDataFieldType.ArbitraryString:
                                case PhysicalDataFieldType.ExtendedArbitraryString:
                                case PhysicalDataFieldType.NumeralString:
                                case PhysicalDataFieldType.CharString:
                                case PhysicalDataFieldType.MixedString:
                                case PhysicalDataFieldType.EncryptedString:
                                    txtMaxLength.Text = customDataFieldInfo.DataFieldLength.ToString();
                                    if (physicalDataFieldType == PhysicalDataFieldType.ArbitraryString || physicalDataFieldType == PhysicalDataFieldType.ExtendedArbitraryString)
                                    {
                                        txtConditionValue.Text = customDataFieldInfo.RegexExpression;
                                    }
                                    else
                                    {
                                        txtConditionValue.Text = string.Empty;
                                    }
                                    ceAutoComplete.Text = "自动完成";
                                    break;

                                case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                                case PhysicalDataFieldType.YearAndMonthAndDay:
                                case PhysicalDataFieldType.YearAndMonth:
                                case PhysicalDataFieldType.MonthAndDay:
                                case PhysicalDataFieldType.Time:
                                    txtConditionValue.Text = string.Empty;
                                    break;

                                case PhysicalDataFieldType.DepartmentDropdownListEnum:
                                case PhysicalDataFieldType.DepartmentTreeViewEnum:
                                case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                                    ceAutoComplete.Text = "动态属性";
                                    txtConditionValue.Text = string.Empty;
                                    break;

                                case PhysicalDataFieldType.DropdownListEnum:
                                case PhysicalDataFieldType.DropdownListEnumValue:
                                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                                case PhysicalDataFieldType.TreeViewEnum:
                                case PhysicalDataFieldType.TreeViewEnumValue:
                                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                                case PhysicalDataFieldType.CscadeEnum:
                                case PhysicalDataFieldType.MultiSelectedEnum:
                                    CommonNode enumCommonNode = customEnumContract.GetCommonNode(customDataFieldInfo.EnumId);
                                    SetEnumNameOnCondtionValue(enumCommonNode.NodeId);
                                    break;

                                case PhysicalDataFieldType.EnumValue:
                                case PhysicalDataFieldType.EnumNameDependency:
                                case PhysicalDataFieldType.FstAdditionalCode:
                                case PhysicalDataFieldType.ScdAdditionalCode:
                                case PhysicalDataFieldType.FstAdditionalString:
                                case PhysicalDataFieldType.ScdAdditionalString:
                                case PhysicalDataFieldType.TrdAdditionalString:
                                case PhysicalDataFieldType.FourthAdditionalString:
                                case PhysicalDataFieldType.FifthAdditionalString:
                                case PhysicalDataFieldType.SixthAdditionalString:
                                case PhysicalDataFieldType.FstAdditionalInteger:
                                case PhysicalDataFieldType.ScdAdditionalInteger:
                                case PhysicalDataFieldType.FstAdditionalDecimal:
                                case PhysicalDataFieldType.ScdAdditionalDecimal:
                                case PhysicalDataFieldType.DepartmentValue:
                                case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                                case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                                    string physicalName = CustomDataFieldContract.GetLogicalName(customDataFieldInfo.ParentDataFieldId);
                                    SetDataFieldNameOnCondtionValue(customDataFieldInfo.ParentDataFieldId, physicalName);
                                    break;

                                case PhysicalDataFieldType.Association:
                                case PhysicalDataFieldType.PrimaryAssociation:
                                    SetAssociatedDataFieldOnCondtionValue(customDataFieldInfo.AssociatedDataFieldId);
                                    break;

                                case PhysicalDataFieldType.SecondaryAssociation:
                                    SetDataFieldNameOnCondtionValue(customDataFieldInfo.ParentDataFieldId);
                                    CommonNode node = associatedDataFieldContract.GetCommonNode(customDataFieldInfo.AssociatedDataFieldId);
                                    cmbAssociatedDataField.Properties.Items.Clear();
                                    IList<CommonNode> associatedDataFieldCommonNodes = associatedDataFieldContract.GetChildNodes(node.ParentNodeId);
                                    cmbAssociatedDataField.Properties.Items.AddRange(associatedDataFieldCommonNodes.ToArray());
                                    cmbAssociatedDataField.EditValue = node;
                                    break;
                            }
                            break;

                        case DataFieldProperty.LogicalDataField:
                            LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)customDataFieldInfo.DataFieldType;
                            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldAttributes);
                            switch (logicalDataFieldType)
                            {
                                case LogicalDataFieldType.StringExpression:
                                case LogicalDataFieldType.DigitExpression:
                                case LogicalDataFieldType.DateTimeExpression:
                                case LogicalDataFieldType.TwoDimCode:
                                    SetExpressionsOnCondtionValue(customDataFieldInfo.DataFieldId, customDataFieldInfo.ExpressionText);
                                    break;

                                case LogicalDataFieldType.OneDimCode:
                                    SetDataFieldNameOnCondtionValue(customDataFieldInfo.ParentDataFieldId);
                                    break;

                                case LogicalDataFieldType.Empty:
                                    txtConditionValue.Text = string.Empty;
                                    txtConditionValue.Tag = null;
                                    break;
                            }
                            break;
                    }
                    int count = CustomDataFieldContract.GetRelatedDataFieldCount(commonNode.NodeId);
                    lnkDataFieldList.Text = string.Format("该字段被{0}个字段关联", count);
                }
                else
                {
                    ClearModelInfo();
                    /* 系统字段 */
                    icmbDataFieldProperty.SelectedIndex = 0;
                    if (commonNode.NodeType > 0)
                    {
                        SystemDataField systemLogicalDataField = (SystemDataField)commonNode.NodeType;
                        BasedDataType dataFieldBase = DataFieldHelper.GetBasedDataType(systemLogicalDataField);
                        cmdDataFieldType.Value = UserEnumHelper.GetEnumText(dataFieldBase);
                    }
                    txtLogicalName.Text = commonNode.NodeName;
                }
            }
            else
            {
                ClearModelInfo();
            }
        }

        /// <summary>
        /// 清除界面数据
        /// </summary>
        public void ClearModelInfo()
        {
            txtLogicalName.Text = string.Empty;
            txtPhysicalName.Text = string.Empty;
            txtDataFieldCode.Text = string.Empty;
            icmbDataFieldProperty.SelectedIndex = 1;
            cmdDataFieldType.SelectedNode = null;
            txtMaxLength.Text = string.Empty;
            ceAutoComplete.Checked = false;
            txtConditionValue.Text = string.Empty;
            txtConditionValue.Tag = null;
            txtConditionValue.Properties.NullValuePrompt = string.Empty;
            ccmbDataFieldAttributes.EditValue = null;
            cmbAssociatedDataField.EditValue = null;
            chkHelpEnabled.Checked = false;
            txtHelpContent.Tag = null;
            txtHelpContent.Text = string.Empty;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldAttributes);
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldSetting);
            txtToolTip.Text = string.Empty;
            txtNotes.Text = string.Empty;
            if (!txtLogicalName.ReadOnly)
            {
                txtLogicalName.Focus();
            }
            hleDataFieldSetting.Text = string.Empty;
            lnkDataFieldList.Text = "该字段未与其他字段关联";
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得字段类型
        /// </summary>
        /// <returns></returns>
        public CustomDataFieldInfo GetModelInfo()
        {
            int maxLength = DataConvertionHelper.GetConvertedInt(txtMaxLength.Text.Trim(), 0);
            decimal associatedDataFieldId = decimal.MinValue;
            decimal parentDataFieldId = decimal.MinValue;
            decimal enumId = decimal.MinValue;
            byte basedDataType = (byte)BasedDataType.String;
            string regexExpression = string.Empty;
            bool requiredDataField = false;
            bool autoComplete = false;
            bool indexCreated = false;
            string conditionValue = string.Empty;
            DataFieldProperty dataFieldProperty = (DataFieldProperty)Convert.ToByte(icmbDataFieldProperty.EditValue);
            switch (dataFieldProperty)
            {
                case DataFieldProperty.PhysicalDataField:
                    List<object> checkedValues = ccmbDataFieldAttributes.Properties.Items.GetCheckedValues();
                    foreach (object obj in checkedValues)
                    {
                        EnumItem enumItem = (EnumItem)obj;
                        PhysicalDataFieldProperty physicalDataFieldProperty = (PhysicalDataFieldProperty)enumItem.Value;
                        switch (physicalDataFieldProperty)
                        {
                            case PhysicalDataFieldProperty.Required:
                                requiredDataField = true;
                                break;

                            case PhysicalDataFieldProperty.Index:
                                indexCreated = true;
                                break;
                        }
                    }
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)Convert.ToByte(cmdDataFieldType.SelectedNodeId);
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.Decimal:
                            if (maxLength <= 0 || maxLength > AppSettingHelper.DefaultDecimalDigitMaxLength)
                            {
                                throw new ArgumentException(string.Format("小数位长度范围在0到{0}之间。", AppSettingHelper.DefaultDecimalDigitMaxLength));
                            }
                            break;

                        case PhysicalDataFieldType.ArbitraryString:
                        case PhysicalDataFieldType.ExtendedArbitraryString:
                        case PhysicalDataFieldType.NumeralString:
                        case PhysicalDataFieldType.CharString:
                        case PhysicalDataFieldType.MixedString:
                        case PhysicalDataFieldType.EncryptedString:
                            if (physicalDataFieldType == PhysicalDataFieldType.ArbitraryString || physicalDataFieldType == PhysicalDataFieldType.ExtendedArbitraryString)
                            {
                                regexExpression = txtConditionValue.Text.Trim();
                            }
                            autoComplete = ceAutoComplete.Checked;
                            break;

                        case PhysicalDataFieldType.DropdownListEnum:
                        case PhysicalDataFieldType.DropdownListEnumValue:
                        case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                        case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                        case PhysicalDataFieldType.TreeViewEnum:
                        case PhysicalDataFieldType.TreeViewEnumValue:
                        case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                        case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                        case PhysicalDataFieldType.CscadeEnum:
                            if (txtConditionValue.Tag != null)
                            {
                                enumId = Convert.ToDecimal(txtConditionValue.Tag);
                            }
                            else
                            {
                                throw new ArgumentException("枚举字段不能为空。");
                            }
                            maxLength = AppSettingHelper.DefaultEnumStringLength;
                            break;

                        case PhysicalDataFieldType.DepartmentTreeViewEnum:
                        case PhysicalDataFieldType.DepartmentDropdownListEnum:
                        case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                            autoComplete = ceAutoComplete.Checked;
                            maxLength = AppSettingHelper.DefaultEnumStringLength;
                            break;

                        case PhysicalDataFieldType.EnumValue:
                        case PhysicalDataFieldType.EnumNameDependency:
                        case PhysicalDataFieldType.FstAdditionalCode:
                        case PhysicalDataFieldType.ScdAdditionalCode:
                        case PhysicalDataFieldType.FstAdditionalString:
                        case PhysicalDataFieldType.ScdAdditionalString:
                        case PhysicalDataFieldType.TrdAdditionalString:
                        case PhysicalDataFieldType.FourthAdditionalString:
                        case PhysicalDataFieldType.FifthAdditionalString:
                        case PhysicalDataFieldType.SixthAdditionalString:                       
                        case PhysicalDataFieldType.DepartmentValue:
                        case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                        case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                            if (txtConditionValue.Tag != null)
                            {
                                parentDataFieldId = Convert.ToDecimal(txtConditionValue.Tag);
                            }
                            else
                            {
                                throw new ArgumentException("依赖的枚举字段不能为空。");
                            }
                            maxLength = AppSettingHelper.DefaultEnumStringLength;
                            break;

                        case PhysicalDataFieldType.FstAdditionalInteger:
                        case PhysicalDataFieldType.ScdAdditionalInteger:
                        case PhysicalDataFieldType.FstAdditionalDecimal:
                        case PhysicalDataFieldType.ScdAdditionalDecimal:
                            if (txtConditionValue.Tag != null)
                            {
                                parentDataFieldId = Convert.ToDecimal(txtConditionValue.Tag);
                            }
                            else
                            {
                                throw new ArgumentException("依赖的枚举字段不能为空。");
                            }
                            break;

                        case PhysicalDataFieldType.MultiSelectedEnum:
                            if (txtConditionValue.Tag != null)
                            {
                                enumId = Convert.ToDecimal(txtConditionValue.Tag);
                            }
                            else
                            {
                                throw new ArgumentException("枚举字段不能为空。");
                            }
                            maxLength = AppSettingHelper.DefaultMultiEnumStringLength;
                            break;

                        case PhysicalDataFieldType.Association:
                        case PhysicalDataFieldType.PrimaryAssociation:
                            if (txtConditionValue.Tag != null)
                            {
                                associatedDataFieldId = Convert.ToDecimal(txtConditionValue.Tag);
                            }
                            else
                            {
                                if (physicalDataFieldType == PhysicalDataFieldType.Association)
                                {
                                    throw new ArgumentException("关联字段不能为空。");
                                }
                                else
                                {
                                    throw new ArgumentException("主关联字段不能为空。");
                                }
                            }
                            AssociatedDataFieldInfo associatedDataFieldInfo = associatedDataFieldContract.GetModelInfo(associatedDataFieldId);
                            if (associatedDataFieldInfo == null)
                            {
                                if (physicalDataFieldType == PhysicalDataFieldType.Association)
                                {
                                    throw new ArgumentException("关联字段不存在。");
                                }
                                else
                                {
                                    throw new ArgumentException("主关联字段不存在。");
                                }
                            }
                            basedDataType = associatedDataFieldInfo.BasedDataType;
                            maxLength = associatedDataFieldInfo.DataLength;
                            break;

                        case PhysicalDataFieldType.SecondaryAssociation:
                            if (txtConditionValue.Tag != null)
                            {
                                parentDataFieldId = Convert.ToDecimal(txtConditionValue.Tag);
                            }
                            else
                            {
                                throw new ArgumentException("主关联字段不能为空。");
                            }
                            if (cmbAssociatedDataField.EditValue != null)
                            {
                                CommonNode commonNode = cmbAssociatedDataField.EditValue as CommonNode;
                                if (commonNode != null)
                                {
                                    associatedDataFieldId = commonNode.NodeId;
                                    AssociatedDataFieldInfo dataFieldInfo = associatedDataFieldContract.GetModelInfo(associatedDataFieldId);
                                    basedDataType = dataFieldInfo.BasedDataType;
                                    maxLength = dataFieldInfo.DataLength;
                                }
                                else
                                {
                                    throw new ArgumentException("次关联字段不能为空。");
                                }
                            }
                            else
                            {
                                throw new ArgumentException("次关联字段不能为空。");
                            }
                            break;
                    }
                    if (physicalDataFieldType != PhysicalDataFieldType.Association && physicalDataFieldType != PhysicalDataFieldType.PrimaryAssociation
                        && physicalDataFieldType != PhysicalDataFieldType.SecondaryAssociation)
                    {
                        basedDataType = (byte)DataFieldHelper.GetBasedDataType(physicalDataFieldType);
                    }
                    break;

                case DataFieldProperty.LogicalDataField:
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)Convert.ToByte(cmdDataFieldType.SelectedNodeId);
                    switch (logicalDataFieldType)
                    {
                        case LogicalDataFieldType.OneDimCode:
                            if (txtConditionValue.Tag != null)
                            {
                                parentDataFieldId = Convert.ToDecimal(txtConditionValue.Tag);
                            }
                            break;

                        case LogicalDataFieldType.StringExpression:
                        case LogicalDataFieldType.DigitExpression:
                        case LogicalDataFieldType.DateTimeExpression:
                        case LogicalDataFieldType.TwoDimCode:
                            if (txtConditionValue.Tag != null)
                            {
                                IList<CommonNode> expressionCommonNodes = txtConditionValue.Tag as IList<CommonNode>;
                                _customExpressionInfos.Clear();
                                int sorting = 1;
                                foreach (CommonNode commonNode in expressionCommonNodes)
                                {
                                    _customExpressionInfos.Add(new CustomExpressionInfo(decimal.MinValue, sorting, commonNode.NodeId));
                                }
                                conditionValue = txtConditionValue.Text.Trim();
                            }
                            break;
                    }
                    basedDataType = (byte)DataFieldHelper.GetBasedDataType(logicalDataFieldType);
                    break;
            }

            CustomDataFieldInfo customDataFieldInfo = new CustomDataFieldInfo()
            {
                DataFieldId = TreeNodeId,
                AssociatedDataFieldId = associatedDataFieldId,
                TableId = TableId,
                ParentDataFieldId = parentDataFieldId,
                EnumId = enumId,
                LogicalName = txtLogicalName.Text.Trim(),
                DataFieldCode = txtDataFieldCode.Text.Trim(),
                DataFieldProperty = Convert.ToByte(icmbDataFieldProperty.EditValue),
                DataFieldType = Convert.ToByte(cmdDataFieldType.SelectedNodeId),
                DataFieldLength = maxLength,
                BasedDataType = basedDataType,
                RegexExpression = regexExpression,
                ExpressionText = conditionValue,
                RequiredDataField = requiredDataField,
                AutoComplete = autoComplete,
                IndexCreated = indexCreated,
                DataFieldSetting = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataFieldSetting),
                HelpEnabled = chkHelpEnabled.Checked,
                HelpContent = DataConvertionHelper.GetString(txtHelpContent.Tag),
                Tooltip = txtToolTip.Text.Trim(),
                Notes = txtNotes.Text.Trim()
            };

            return customDataFieldInfo;
        }

        /// <summary>
        /// 增加属性
        /// </summary>
        /// <param name="downListItems"></param>
        public void AddPropertiesToImageComboBoxEdit(ImageComboBoxEdit imageComboBoxEdit, IList<EnumItem> enumItems)
        {
            imageComboBoxEdit.Properties.Items.Clear();
            for (int i = 0; i < enumItems.Count; i++)
            {
                int imageIndex = i % 10;
                ImageComboBoxItem item = new ImageComboBoxItem(enumItems[i].Text, enumItems[i].Value, imageIndex);
                imageComboBoxEdit.Properties.Items.Add(item);
            }
            imageComboBoxEdit.SelectedIndex = 0;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置与字段类型相关的控件属性
        /// </summary>
        /// <param name="dataFieldProperty"></param>
        /// <param name="dataFieldType"></param>
        /// <param name="readOnly"></param>
        private void SetControlsProperty(DataFieldProperty dataFieldProperty, byte dataFieldType, bool readOnly)
        {
            switch (dataFieldProperty)
            {
                case DataFieldProperty.PhysicalDataField:
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)dataFieldType;
                    lblDataFieldAttributes.Text = "字段性质：";
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.Boolean:
                        case PhysicalDataFieldType.Int32:
                            InitDataFieldControlsProperties();
                            break;

                        case PhysicalDataFieldType.Decimal:
                            txtMaxLength.Visible = true;
                            txtMaxLength.ReadOnly = readOnly;
                            lblMaxLengthTip.Visible = true;
                            ceAutoComplete.Visible = false;

                            /* 字段条件*/
                            lblCondition.Enabled = false;
                            txtConditionValue.ReadOnly = true;
                            hleDataFieldSetting.Enabled = false;
                            lblConditionTip.Visible = false;
                            lblCondition.Text = "字段条件";
                            txtConditionValue.Properties.NullValuePrompt = string.Empty;
                            hleDataFieldSetting.Text = "设置...";

                            /* 字段性质 */
                            lblDataFieldAttributes.Enabled = false;
                            ccmbDataFieldAttributes.ReadOnly = true;
                            ccmbDataFieldAttributes.Visible = true;
                            lblDataFieldAttributes.Enabled = false;
                            ccmbDataFieldSetting.ReadOnly = true;
                            lblDataFieldSetting.Enabled = false;
                            cmbAssociatedDataField.Visible = false;
                            break;

                        case PhysicalDataFieldType.ArbitraryString:
                        case PhysicalDataFieldType.ExtendedArbitraryString:
                        case PhysicalDataFieldType.NumeralString:
                        case PhysicalDataFieldType.CharString:
                        case PhysicalDataFieldType.MixedString:
                        case PhysicalDataFieldType.EncryptedString:
                            /* 字符串类型 */
                            txtMaxLength.Visible = true;
                            lblMaxLengthTip.Visible = true;
                            ceAutoComplete.Visible = true;
                            ceAutoComplete.ReadOnly = readOnly;
                            txtMaxLength.ReadOnly = readOnly;
                            ceAutoComplete.Text = "自动完成";

                            /* 字段条件*/
                            if (physicalDataFieldType == PhysicalDataFieldType.ArbitraryString || physicalDataFieldType == PhysicalDataFieldType.ExtendedArbitraryString)
                            {
                                lblCondition.Enabled = true;
                                txtConditionValue.ReadOnly = readOnly;
                                hleDataFieldSetting.Enabled = !readOnly;
                            }
                            else
                            {
                                lblCondition.Enabled = false;
                                txtConditionValue.ReadOnly = true;
                                hleDataFieldSetting.Enabled = false;
                            }
                            lblConditionTip.Visible = false;
                            lblCondition.Text = "正则表达：";
                            txtConditionValue.Properties.NullValuePrompt = "正则表达式";
                            hleDataFieldSetting.Text = "验证...";

                            break;

                        case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                        case PhysicalDataFieldType.YearAndMonthAndDay:
                        case PhysicalDataFieldType.YearAndMonth:
                        case PhysicalDataFieldType.MonthAndDay:
                        case PhysicalDataFieldType.Time:
                            InitDataFieldControlsProperties();
                            break;

                        case PhysicalDataFieldType.DepartmentTreeViewEnum:
                        case PhysicalDataFieldType.DepartmentDropdownListEnum:
                        case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                            /* 字符串类型长度 */
                            txtMaxLength.Visible = false;
                            lblMaxLengthTip.Visible = false;
                            ceAutoComplete.Visible = true;
                            ceAutoComplete.ReadOnly = readOnly;
                            ceAutoComplete.Text = "动态属性";

                            /* 字段条件*/
                            lblCondition.Enabled = false;
                            txtConditionValue.ReadOnly = true;
                            hleDataFieldSetting.Enabled = false;
                            lblConditionTip.Visible = false;
                            lblCondition.Text = "字段条件";
                            txtConditionValue.Properties.NullValuePrompt = string.Empty;
                            hleDataFieldSetting.Text = "设置...";

                            /* 字段性质 */
                            lblDataFieldAttributes.Enabled = false;
                            ccmbDataFieldAttributes.ReadOnly = true;
                            ccmbDataFieldAttributes.Visible = true;
                            lblDataFieldAttributes.Enabled = false;
                            ccmbDataFieldSetting.ReadOnly = true;
                            lblDataFieldSetting.Enabled = false;
                            cmbAssociatedDataField.Visible = false;
                            break;

                        case PhysicalDataFieldType.DropdownListEnum:
                        case PhysicalDataFieldType.DropdownListEnumValue:
                        case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                        case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                        case PhysicalDataFieldType.TreeViewEnum:
                        case PhysicalDataFieldType.TreeViewEnumValue:
                        case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                        case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                        case PhysicalDataFieldType.CscadeEnum:
                        case PhysicalDataFieldType.MultiSelectedEnum:
                            /* 字符串类型长度 */
                            txtMaxLength.Visible = false;
                            lblMaxLengthTip.Visible = false;
                            ceAutoComplete.Visible = false;

                            /* 字段条件*/
                            lblCondition.Enabled = true;
                            txtConditionValue.ReadOnly = true;
                            hleDataFieldSetting.Enabled = !readOnly;
                            lblConditionTip.Visible = true;
                            lblCondition.Text = "枚举名称：";
                            txtConditionValue.Properties.NullValuePrompt = "请选择枚举";
                            hleDataFieldSetting.Text = "选择...";

                            /* 字段性质 */
                            lblDataFieldAttributes.Text = "字段性质：";
                            cmbAssociatedDataField.Visible = false;
                            ccmbDataFieldAttributes.Visible = true;
                            ccmbDataFieldAttributes.ReadOnly = readOnly;
                            break;

                        case PhysicalDataFieldType.EnumValue:
                        case PhysicalDataFieldType.EnumNameDependency:
                        case PhysicalDataFieldType.FstAdditionalCode:
                        case PhysicalDataFieldType.ScdAdditionalCode:
                        case PhysicalDataFieldType.FstAdditionalString:
                        case PhysicalDataFieldType.ScdAdditionalString:
                        case PhysicalDataFieldType.TrdAdditionalString:
                        case PhysicalDataFieldType.FourthAdditionalString:
                        case PhysicalDataFieldType.FifthAdditionalString:
                        case PhysicalDataFieldType.SixthAdditionalString:
                        case PhysicalDataFieldType.FstAdditionalInteger:
                        case PhysicalDataFieldType.ScdAdditionalInteger:
                        case PhysicalDataFieldType.FstAdditionalDecimal:
                        case PhysicalDataFieldType.ScdAdditionalDecimal:
                        case PhysicalDataFieldType.DepartmentValue:
                        case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                        case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                            /* 字符串类型 */
                            txtMaxLength.Visible = false;
                            lblMaxLengthTip.Visible = false;
                            ceAutoComplete.Visible = false;

                            /* 字段条件*/
                            lblCondition.Enabled = true;
                            txtConditionValue.ReadOnly = true;
                            hleDataFieldSetting.Enabled = !readOnly;
                            lblConditionTip.Visible = true;
                            lblCondition.Text = "枚举字段：";
                            txtConditionValue.Properties.NullValuePrompt = "请选择被依赖的枚举字段";
                            hleDataFieldSetting.Text = "选择...";
                            break;

                        case PhysicalDataFieldType.Association:
                        case PhysicalDataFieldType.PrimaryAssociation:
                            /* 字符串类型 */
                            txtMaxLength.Visible = false;
                            lblMaxLengthTip.Visible = false;
                            ceAutoComplete.Visible = false;

                            /* 字段条件*/
                            lblCondition.Enabled = true;
                            txtConditionValue.ReadOnly = true;
                            hleDataFieldSetting.Enabled = !readOnly;
                            lblConditionTip.Visible = true;
                            lblCondition.Text = "关联字段：";
                            txtConditionValue.Properties.NullValuePrompt = "请选择关联字段";
                            hleDataFieldSetting.Text = "选择...";

                            /* 字段性质 */
                            lblDataFieldAttributes.Text = "字段性质：";
                            cmbAssociatedDataField.Visible = false;
                            ccmbDataFieldAttributes.Visible = true;
                            ccmbDataFieldAttributes.ReadOnly = readOnly;
                            break;

                        case PhysicalDataFieldType.SecondaryAssociation:
                            /* 字符串类型长度 */
                            txtMaxLength.Visible = false;
                            lblMaxLengthTip.Visible = false;
                            ceAutoComplete.Visible = false;

                            /* 字段条件*/
                            lblCondition.Enabled = true;
                            txtConditionValue.ReadOnly = true;
                            hleDataFieldSetting.Enabled = !readOnly;
                            lblConditionTip.Visible = true;
                            lblCondition.Text = "关键字段：";
                            txtConditionValue.Properties.NullValuePrompt = "请选择关键字段";
                            hleDataFieldSetting.Text = "选择...";

                            /* 字段性质 */
                            lblDataFieldAttributes.Text = "关联字段：";
                            cmbAssociatedDataField.Visible = true;
                            ccmbDataFieldAttributes.Visible = false;
                            cmbAssociatedDataField.ReadOnly = readOnly;
                            break;

                        default:
                            InitDataFieldControlsProperties();
                            break;
                    }
                    /* 字段性质 */
                    ccmbDataFieldAttributes.Properties.ReadOnly = readOnly;
                    ccmbDataFieldSetting.ReadOnly = readOnly;
                    lblDataFieldAttributes.Enabled = true;
                    lblDataFieldSetting.Enabled = true;
                    break;

                case DataFieldProperty.LogicalDataField:
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)dataFieldType;
                    ccmbDataFieldAttributes.ReadOnly = true;
                    ccmbDataFieldSetting.ReadOnly = true;
                    lblDataFieldSetting.Enabled = false;
                    cmbAssociatedDataField.Visible = false;

                    /* 字符串类型 */
                    txtMaxLength.Visible = false;
                    lblMaxLengthTip.Visible = false;
                    ceAutoComplete.Visible = false;

                    switch (logicalDataFieldType)
                    {
                        case LogicalDataFieldType.OneDimCode:
                            /* 字段条件*/
                            lblCondition.Enabled = true;
                            txtConditionValue.ReadOnly = true;
                            hleDataFieldSetting.Enabled = !readOnly;
                            lblConditionTip.Visible = true;
                            lblCondition.Text = "条码字段：";
                            txtConditionValue.Properties.NullValuePrompt = "请选择数字字母型的字段";
                            hleDataFieldSetting.Text = "选择...";

                            /* 字段性质 */
                            lblDataFieldAttributes.Text = "字段性质：";
                            lblDataFieldAttributes.Enabled = false;
                            ccmbDataFieldAttributes.Visible = true;
                            break;

                        case LogicalDataFieldType.TwoDimCode:
                            /* 字段条件*/
                            lblCondition.Enabled = true;
                            txtConditionValue.ReadOnly = true;
                            hleDataFieldSetting.Enabled = !readOnly;
                            lblConditionTip.Visible = true;
                            lblCondition.Text = "条码字段：";
                            txtConditionValue.Properties.NullValuePrompt = "请选择数字字母型的字段";
                            hleDataFieldSetting.Text = "选择...";

                            /* 字段性质 */
                            lblDataFieldAttributes.Text = "字段性质：";
                            lblDataFieldAttributes.Enabled = false;
                            ccmbDataFieldAttributes.Visible = true;
                            break;

                        case LogicalDataFieldType.StringExpression:
                        case LogicalDataFieldType.DigitExpression:
                        case LogicalDataFieldType.DateTimeExpression:
                            /* 字段性质*/
                            lblCondition.Enabled = true;
                            txtConditionValue.ReadOnly = true;
                            hleDataFieldSetting.Enabled = !readOnly;
                            lblConditionTip.Visible = true;
                            lblCondition.Text = "表达式值：";
                            txtConditionValue.Properties.NullValuePrompt = "请设置表达式值";
                            hleDataFieldSetting.Text = "设置..";

                            /* 字段性质 */
                            lblDataFieldAttributes.Text = "字段性质：";
                            lblDataFieldAttributes.Enabled = false;
                            ccmbDataFieldAttributes.Visible = true;
                            break;

                        default:
                            /* 字段条件*/
                            lblCondition.Enabled = false;
                            txtConditionValue.ReadOnly = true;
                            hleDataFieldSetting.Enabled = false;
                            lblConditionTip.Visible = true;
                            lblCondition.Text = "字段条件：";
                            txtConditionValue.Properties.NullValuePrompt = string.Empty;
                            hleDataFieldSetting.Text = "选择...";
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 初始化一部分与字段类型相关的控件属性
        /// </summary>
        private void InitDataFieldControlsProperties()
        {
            /* 字符串类型 */
            txtMaxLength.Visible = false;
            lblMaxLengthTip.Visible = false;
            ceAutoComplete.Visible = false;

            /* 字段条件*/
            lblCondition.Enabled = false;
            txtConditionValue.ReadOnly = true;
            hleDataFieldSetting.Enabled = false;
            lblConditionTip.Visible = false;
            lblCondition.Text = "字段条件";
            txtConditionValue.Properties.NullValuePrompt = string.Empty;
            hleDataFieldSetting.Text = "设置...";

            /* 字段性质 */
            lblDataFieldAttributes.Enabled = false;
            ccmbDataFieldAttributes.ReadOnly = true;
            ccmbDataFieldAttributes.Visible = true;
            lblDataFieldAttributes.Enabled = false;
            ccmbDataFieldSetting.ReadOnly = true;
            lblDataFieldSetting.Enabled = false;
            cmbAssociatedDataField.Visible = false;
        }

        /// <summary>
        /// 获得日期的值
        /// </summary>
        /// <param name="date"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        private string GetDateTime(DateTime date, PhysicalDataFieldType physicalDataFieldType)
        {
            string dateValue = string.Empty;

            if (!DataConvertionHelper.IsNullValue(date))
            {
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                        dateValue = date.ToString("G");
                        break;

                    case PhysicalDataFieldType.YearAndMonth:
                        dateValue = date.ToString("y"); ;
                        break;

                    case PhysicalDataFieldType.YearAndMonthAndDay:
                        dateValue = date.ToString("d"); ;
                        break;

                    case PhysicalDataFieldType.MonthAndDay:
                        dateValue = date.ToString("m");
                        break;

                    case PhysicalDataFieldType.Time:
                        dateValue = date.ToShortTimeString(); ;
                        break;
                }
            }

            return dateValue;
        }

        /// <summary>
        /// 设置表达式名称
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="text"></param>
        private void SetExpressionsOnCondtionValue(decimal dataFieldId, string text)
        {
            if (dataFieldId > 0)
            {
                IList<CommonNode> commonNodes = customExpressionContract.GetCommonNodes(dataFieldId);
                //IList <CustomExpressionInfo> customExpressionInfos = customExpressionContract.GetModelInfos(dataFieldId);
                //IList<CommonNode> commonNodes = new List<CommonNode>();
                //foreach (CustomExpressionInfo customExpressionInfo in customExpressionInfos)
                //{
                //    DataFieldProperty dataFieldProperty = (DataFieldProperty)customExpressionInfo.DataFieldPartition;
                //    switch(dataFieldProperty)
                //    {
                //        case DataFieldProperty.SystemPhysicalDataField:                            
                //            commonNodes.Add(new CommonNode(customExpressionInfo.SystemDataFieldId, string.Empty, Convert.ToByte(dataFieldProperty)));
                //            break;

                //        case DataFieldProperty.PhysicalDataField:
                //            commonNodes.Add(new CommonNode(customExpressionInfo.DataFieldId, string.Empty, Convert.ToByte(dataFieldProperty)));
                //            break;
                //    }
                //}
                txtConditionValue.Text = text;
                txtConditionValue.Tag = commonNodes;
            }
            else
            {
                txtConditionValue.Tag = null;
                txtConditionValue.Text = string.Empty;
            }
        }

        /// <summary>
        /// 设置字段名称
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="text"></param>
        private void SetDataFieldNameOnCondtionValue(decimal dataFieldId, string text)
        {
            if (dataFieldId > 0)
            {
                txtConditionValue.Text = string.Format("[{0}]", text);
                txtConditionValue.Tag = dataFieldId;
            }
            else
            {
                txtConditionValue.Tag = null;
                txtConditionValue.Text = string.Empty;
            }
        }

        /// <summary>
        /// 设置字段名称
        /// </summary>
        /// <param name="dataFieldId"></param>
        private void SetDataFieldNameOnCondtionValue(decimal dataFieldId)
        {
            if (dataFieldId > 0)
            {
                IList<string> names = CustomDataFieldContract.GetHierarchicalNamesOfNode(dataFieldId);
                txtConditionValue.Text = UserControlHelper.GetFormattedName(names);
                txtConditionValue.Tag = dataFieldId;
            }
            else
            {
                txtConditionValue.Tag = null;
                txtConditionValue.Text = string.Empty;
            }
        }

        /// <summary>
        /// 设置枚举名称
        /// </summary>
        /// <param name="dataFieldId"></param>
        private void SetEnumNameOnCondtionValue(decimal dataFieldId)
        {
            if (dataFieldId > 0)
            {
                IList<string> names = customEnumContract.GetHierarchicalNamesOfNode(dataFieldId);
                txtConditionValue.Text = UserControlHelper.GetFormattedName(names);
                txtConditionValue.Tag = dataFieldId;
            }
            else
            {
                txtConditionValue.Tag = null;
                txtConditionValue.Text = string.Empty;
            }
        }

        /// <summary>
        /// 设置关联字段名称
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        private void SetAssociatedDataFieldOnCondtionValue(decimal associatedDataFieldId)
        {
            if (associatedDataFieldId > 0)
            {
                txtConditionValue.Text = associatedDataFieldContract.GetFullName(associatedDataFieldId);
                txtConditionValue.Tag = associatedDataFieldId;
            }
            else
            {
                txtConditionValue.Tag = null;
                txtConditionValue.Text = string.Empty;
            }
        }

        #endregion
    }
}
