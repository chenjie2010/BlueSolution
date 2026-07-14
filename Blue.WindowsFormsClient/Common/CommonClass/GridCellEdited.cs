using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.Common
{
    /// <summary>
    /// 单元格编辑类
    /// </summary>
    public class GridCellEdited
    {
        #region 私有变量

        private RelaitonDataBusiness relationDataBusiness = null;
        private Dictionary<decimal, DataTable> associationData = null;
        #endregion

        #region 契约接口

        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly IDataAuditingContract dataAuditingContract;

        #endregion        

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldContract"></param>
        /// <param name="enumContract"></param>
        /// <param name="departmentContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="astdDataFieldContract"></param>
        /// <param name="auditingContract"></param>
        public GridCellEdited(ICustomDataFieldContract dataFieldContract, ICustomEnumContract enumContract, ICustomDepartmentContract departmentContract,
            ICustomAssociationContract associationContract, IAssociatedDataFieldContract astdDataFieldContract, IDataAuditingContract auditingContract)
        {
            customDataFieldContract = dataFieldContract;
            customEnumContract = enumContract;
            customDepartmentContract = departmentContract;
            customAssociationContract = associationContract;
            associatedDataFieldContract = astdDataFieldContract;
            dataAuditingContract = auditingContract;
            associationData = new Dictionary<decimal, DataTable>();
            relationDataBusiness = new RelaitonDataBusiness(dataFieldContract, customAssociationContract, customEnumContract,
                    associatedDataFieldContract, customDepartmentContract);
        }

        #endregion

        /// <summary>
        /// 设置物理类型字段的编辑内容
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="customDataFieldInfo"></param>
        public void SetColumnEdit(DevExpressGrid devExpressGrid, ExtendedCustomDataFieldInfo customDataFieldInfo)
        {
            if (devExpressGrid.FocusedColumn == null || devExpressGrid.FocusedColumn.ColumnEdit != null)
            {
                return;
            }

            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
            int width = DataFieldHelper.GetControlWidth(physicalDataFieldType);
            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                case PhysicalDataFieldType.YearAndMonthAndDay:
                case PhysicalDataFieldType.YearAndMonth:
                case PhysicalDataFieldType.MonthAndDay:
                    RepositoryItemDateEdit repositoryItemDateEdit = new RepositoryItemDateEdit();
                    repositoryItemDateEdit.LookAndFeel.SkinName = "Money Twins";
                    repositoryItemDateEdit.LookAndFeel.UseDefaultLookAndFeel = false;
                    repositoryItemDateEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    repositoryItemDateEdit.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                            repositoryItemDateEdit.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
                            repositoryItemDateEdit.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                            repositoryItemDateEdit.DisplayFormat.FormatString = "G";
                            repositoryItemDateEdit.EditFormat.FormatString = "G";
                            repositoryItemDateEdit.EditMask = "G";
                            break;

                        case PhysicalDataFieldType.YearAndMonthAndDay:
                            repositoryItemDateEdit.DisplayFormat.FormatString = "d";
                            repositoryItemDateEdit.EditFormat.FormatString = "d";
                            repositoryItemDateEdit.EditMask = "d";
                            break;

                        case PhysicalDataFieldType.YearAndMonth:
                            repositoryItemDateEdit.DisplayFormat.FormatString = "y";
                            repositoryItemDateEdit.EditFormat.FormatString = "y";
                            repositoryItemDateEdit.EditMask = "y";
                            break;

                        case PhysicalDataFieldType.MonthAndDay:
                            repositoryItemDateEdit.DisplayFormat.FormatString = "m";
                            repositoryItemDateEdit.EditFormat.FormatString = "m";
                            repositoryItemDateEdit.EditMask = "m";
                            break;
                    }
                    devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemDateEdit;
                    break;

                case PhysicalDataFieldType.Time:
                    RepositoryItemTimeEdit repositoryItemTimeEdit = new RepositoryItemTimeEdit();
                    repositoryItemTimeEdit.Mask.EditMask = "HH:mm:ss";
                    repositoryItemTimeEdit.LookAndFeel.SkinName = "Money Twins";
                    repositoryItemTimeEdit.LookAndFeel.UseDefaultLookAndFeel = false;
                    repositoryItemTimeEdit.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                    devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemTimeEdit;
                    break;

                case PhysicalDataFieldType.Boolean:
                    RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit();
                    repositoryItemCheckEdit.LookAndFeel.SkinName = "Money Twins";
                    repositoryItemCheckEdit.LookAndFeel.UseDefaultLookAndFeel = false;
                    devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemCheckEdit;
                    break;

                case PhysicalDataFieldType.DropdownListEnum:
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.DepartmentDropdownListEnum:
                    RepositoryItemComboBox repositoryItemComboBox = new RepositoryItemComboBox();
                    repositoryItemComboBox.LookAndFeel.SkinName = "Money Twins";
                    repositoryItemComboBox.LookAndFeel.UseDefaultLookAndFeel = false;
                    repositoryItemComboBox.Tag = customDataFieldInfo;
                    repositoryItemComboBox.SelectedIndexChanged += (sdr, arg) =>
                    {
                        ComboBoxEdit comboBoxEdit = sdr as ComboBoxEdit;
                        CommonNode commonNode = comboBoxEdit.SelectedItem as CommonNode;
                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = commonNode;
                        if (physicalDataFieldType == PhysicalDataFieldType.DropdownListEnum || physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                        {
                            devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, commonNode.NodeName);
                        }
                        else
                        {
                            object obj = customEnumContract.GetEnumData(commonNode.NodeId, physicalDataFieldType);
                            devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, obj);
                        }
                    };
                    IList<CommonNode> enumCommonNodes = null;
                    if (physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                    {
                        enumCommonNodes = customDepartmentContract.GetChildNodes(decimal.One);
                    }
                    else
                    {
                        enumCommonNodes = customEnumContract.GetChildNodes(customDataFieldInfo.EnumId);
                    }
                    repositoryItemComboBox.Items.AddRange(enumCommonNodes.ToArray());
                    devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemComboBox;
                    break;

                case PhysicalDataFieldType.CscadeEnum:
                    RepositoryItemButtonEdit buttonEdit = new RepositoryItemButtonEdit();
                    buttonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                    buttonEdit.Tag = customDataFieldInfo;
                    buttonEdit.ButtonPressed += (sender, e) =>
                    {
                        int level = customEnumContract.GetMaxLevel(customDataFieldInfo.EnumId);
                        CscadeEnumForm frmCscadeEnumForm = new CscadeEnumForm()
                        {
                            CustomEnumContract = customEnumContract,
                            EnumId = customDataFieldInfo.EnumId,
                            Level = level,
                            SelectedText = string.Empty
                        };
                        frmCscadeEnumForm.CscadeNodeSelected = (commonNodes) =>
                        {
                            if (commonNodes != null && commonNodes.Count > 0)
                            {
                                devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = commonNodes;
                                devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, UserControlHelper.GetCleanFormattedName(commonNodes));
                            }
                        };
                        frmCscadeEnumForm.ShowDialog();
                    };
                    devExpressGrid.FocusedColumn.ColumnEdit = buttonEdit;
                    break;

                case PhysicalDataFieldType.Association:
                    decimal associatedId = associatedDataFieldContract.GetAssociationId(customDataFieldInfo.AssociatedDataFieldId);
                    CustomAssociationInfo associationInfo = customAssociationContract.GetModelInfo(associatedId);
                    string content = string.Empty;
                    SearchLookUpEditWithGrid searchLookUpEditWithGrid = new SearchLookUpEditWithGrid();
                    searchLookUpEditWithGrid.Width = width;
                    searchLookUpEditWithGrid.ValueMember = associatedDataFieldContract.GetPhysicalName(customDataFieldInfo.AssociatedDataFieldId);
                    searchLookUpEditWithGrid.TextEditStyle = TextEditStyles.Standard;
                    searchLookUpEditWithGrid.Tag = customDataFieldInfo;
                    searchLookUpEditWithGrid.OnGridBeforePopup += (sdr, arg) =>
                    {
                        if (searchLookUpEditWithGrid.DataSource == null)
                        {
                            LoadAssociatedData(searchLookUpEditWithGrid, associationInfo.AssociationId, string.Empty);
                        }
                    };
                    searchLookUpEditWithGrid.ValueSearched += (sdr, arg) =>
                    {
                        LoadAssociatedData(searchLookUpEditWithGrid, associationInfo.AssociationId, arg.Content);
                        content = arg.Content;
                    };
                    searchLookUpEditWithGrid.OnPageIndexChanged += (sdr, arg) =>
                    {
                        searchLookUpEditWithGrid.CurrentPageIndex = arg.NewPageIndex;
                        LoadAssociatedData(searchLookUpEditWithGrid, associationInfo.AssociationId, content);
                    };
                    searchLookUpEditWithGrid.EditValueChanged += (sdr, arg) =>
                    {
                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, searchLookUpEditWithGrid.EditValue);
                    };
                    devExpressGrid.FocusedColumn.ColumnEdit = GetRepositoryItemPopupContainerEdit(searchLookUpEditWithGrid);
                    break;

                case PhysicalDataFieldType.PrimaryAssociation:
                    decimal associationId = associatedDataFieldContract.GetAssociationId(customDataFieldInfo.AssociatedDataFieldId);
                    CustomAssociationInfo customAssociationInfo = customAssociationContract.GetModelInfo(associationId);
                    if (customAssociationInfo.SuperAssociationEnabled)
                    {
                        RepositoryItemButtonEdit repositoryItemButtonEdit = new RepositoryItemButtonEdit();
                        repositoryItemButtonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                        repositoryItemButtonEdit.Tag = customDataFieldInfo;
                        repositoryItemButtonEdit.ButtonPressed += (sender, e) =>
                        {
                            AssociatedDataForm frmAssociatedData = new AssociatedDataForm()
                            {
                                AssociationId = customAssociationInfo.AssociationId
                            };
                            frmAssociatedData.DataRowConfrimed = (dataRow) =>
                            {
                                string dataFieldPhysicalName = frmAssociatedData.GetDataFieldPhysicalName(customDataFieldInfo.AssociatedDataFieldId);
                                if (!string.IsNullOrWhiteSpace(dataFieldPhysicalName))
                                {
                                    if (dataRow != null)
                                    {
                                        string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = dataRow[recordIdName];
                                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, dataRow[dataFieldPhysicalName]);
                                    }
                                    else
                                    {
                                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = null;
                                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, null);
                                    }
                                }
                            };
                            frmAssociatedData.ShowDialog();
                        };
                        devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemButtonEdit;
                    }
                    else
                    {
                        AssociationShowMode associationShowMode = (AssociationShowMode)customAssociationInfo.ShowMode;
                        string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                        string key2 = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordSorting);
                        switch (associationShowMode)
                        {
                            case AssociationShowMode.Hierarchicy:
                                LookUpEditWithGrid lookUpEditWithGrid = new LookUpEditWithGrid();
                                lookUpEditWithGrid.Width = width;
                                lookUpEditWithGrid.Tag = customDataFieldInfo;
                                lookUpEditWithGrid.ValueMember = associatedDataFieldContract.GetPhysicalName(customDataFieldInfo.AssociatedDataFieldId);
                                lookUpEditWithGrid.ShowSearch = true;
                                lookUpEditWithGrid.EditValueCleaned += (sender, e) =>
                                {
                                    devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = null;
                                    devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, null);
                                };
                                lookUpEditWithGrid.OnGridBeforePopup += (sender, e) =>
                                {
                                    if (lookUpEditWithGrid.DataSource == null)
                                    {
                                        lookUpEditWithGrid.DataKeyNames = new string[] { key, key2 };
                                        lookUpEditWithGrid.SortedKeyName = key;
                                        IList<string> groupKeyNames = new List<string>();
                                        IList<AssociatedDataFieldInfo> associatedDataFieldInfos = associatedDataFieldContract.GetModelInfos(customAssociationInfo.AssociationId);
                                        foreach (AssociatedDataFieldInfo associatedDataFieldInfo in associatedDataFieldInfos)
                                        {
                                            if (associatedDataFieldInfo.IsHierarchal)
                                            {
                                                groupKeyNames.Add(associatedDataFieldInfo.PhysicalName);
                                            }
                                        }
                                        if (groupKeyNames.Count > 0)
                                        {
                                            lookUpEditWithGrid.GroupKeyNames = groupKeyNames.ToArray();
                                        }
                                        lookUpEditWithGrid.DataSource = customAssociationContract.GetAssociationDataWithSortingDataField(customAssociationInfo.AssociationId);
                                    }
                                };
                                lookUpEditWithGrid.OnRowDoubleClick += (sender, e) =>
                                {
                                    if (lookUpEditWithGrid.FocusedDataRow != null)
                                    {
                                        string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = lookUpEditWithGrid.FocusedDataRow[recordIdName];
                                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, lookUpEditWithGrid.EditValue);
                                    }
                                };
                                devExpressGrid.FocusedColumn.ColumnEdit = GetRepositoryItemPopupContainerEdit(lookUpEditWithGrid);
                                break;

                            case AssociationShowMode.Table:
                                string physicalName = associatedDataFieldContract.GetPhysicalName(customDataFieldInfo.AssociatedDataFieldId);
                                DataTable data = null;
                                if (associationData.ContainsKey(customAssociationInfo.AssociationId))
                                {
                                    data = associationData[customAssociationInfo.AssociationId];
                                }
                                else
                                {
                                    data = relationDataBusiness.GetAssociationData(customAssociationInfo.AssociationId);
                                    associationData.Add(customAssociationInfo.AssociationId, data);
                                }
                                RepositoryItemGridLookUpEdit lookUpEdit = new RepositoryItemGridLookUpEdit();
                                lookUpEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                                lookUpEdit.Tag = customDataFieldInfo;
                                lookUpEdit.NullText = null;
                                lookUpEdit.PopupSizeable = false;
                                lookUpEdit.ShowFooter = true;
                                lookUpEdit.DisplayMember = physicalName;
                                lookUpEdit.ValueMember = physicalName;
                                lookUpEdit.DataSource = data;
                                lookUpEdit.View.PopulateColumns(data);
                                lookUpEdit.View.Columns[key].Visible = false;
                                foreach (GridColumn column in lookUpEdit.View.Columns)
                                {
                                    if (column.ColumnType == typeof(DateTime))
                                    {
                                        column.DisplayFormat.FormatType = FormatType.Custom;
                                        column.DisplayFormat.FormatString = "yyyy-MM-dd";
                                    }
                                }
                                lookUpEdit.EditValueChanging += (sdr, arg) =>
                                {
                                    DataRowView dataRow = lookUpEdit.GetRowByKeyValue(arg.NewValue) as DataRowView;
                                    if (dataRow != null)
                                    {
                                        string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = dataRow[recordIdName];
                                    }
                                    else
                                    {
                                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = null;
                                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, null);
                                    }
                                };
                                devExpressGrid.FocusedColumn.ColumnEdit = lookUpEdit;
                                break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.TreeViewEnumValue:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    bool superEnumEnabled = customEnumContract.GetSuperEnumEnabled(customDataFieldInfo.EnumId);
                    if (superEnumEnabled)
                    {
                        RepositoryItemButtonEdit repositoryItemButtonEdit = new RepositoryItemButtonEdit();
                        repositoryItemButtonEdit.TextEditStyle = TextEditStyles.DisableTextEditor;
                        repositoryItemButtonEdit.Tag = customDataFieldInfo;
                        repositoryItemButtonEdit.ButtonPressed += (sender, e) =>
                        {
                            TreeSelectedItemsForm frmTreeSelectedItems = new TreeSelectedItemsForm()
                            {
                                Text = customDataFieldInfo.LogicalName,
                                CommonNodeContract = customEnumContract,
                                ParentNodeId = customDataFieldInfo.EnumId,
                                OnlySelectedLeaf = true,
                                ShowSearch = true
                            };
                            frmTreeSelectedItems.NodeSelected = (node) =>
                            {
                                devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = node;
                                if (physicalDataFieldType == PhysicalDataFieldType.TreeViewEnum || physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                                {
                                    devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, node.NodeName);
                                }
                                else
                                {
                                    object obj = customEnumContract.GetEnumData(node.NodeId, physicalDataFieldType);
                                    devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, obj);
                                }
                            };
                            frmTreeSelectedItems.NodeRemoved = () =>
                            {
                                devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = null;
                                devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, string.Empty);
                            };
                            frmTreeSelectedItems.ShowDialog();
                        };
                        devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemButtonEdit;
                    }
                    else
                    {
                        TreeDropdownList treeDropdownList = new TreeDropdownList();
                        treeDropdownList.SkinName = "Blue";
                        treeDropdownList.Width = width;
                        treeDropdownList.OnlySelectedLeaf = true;
                        treeDropdownList.Tag = customDataFieldInfo;
                        treeDropdownList.TreeDropdownHandler = new TreeDropdownItems(customEnumContract, customDataFieldInfo.EnumId);
                        treeDropdownList.Value = DataConvertionHelper.GetString(devExpressGrid.FocusedValue);
                        treeDropdownList.BeforeControlPopup += (sender, e) =>
                        {
                            if (treeDropdownList.TreeView.Nodes.Count == 0)
                            {
                                treeDropdownList.TreeDropdownHandler = new TreeDropdownItems(customEnumContract, customDataFieldInfo.EnumId);
                                treeDropdownList.InitalizeTreeView();
                            }
                        };
                        treeDropdownList.AfterTreeNodeSelect += (sender, e) =>
                        {
                            CommonNode commonNode = treeDropdownList.SelectedNode;
                            devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = commonNode;
                            if (physicalDataFieldType == PhysicalDataFieldType.TreeViewEnum || physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                            {
                                devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, commonNode.NodeName);
                            }
                            else
                            {
                                object obj = customEnumContract.GetEnumData(commonNode.NodeId, physicalDataFieldType);
                                devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, obj);
                            }
                        };
                        treeDropdownList.OnNodeRemoved += (sender, e) =>
                        {
                            devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = null;
                            devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, string.Empty);
                        };
                        devExpressGrid.FocusedColumn.ColumnEdit = GetRepositoryItemPopupContainerEdit(treeDropdownList);
                    }
                    break;

                case PhysicalDataFieldType.DepartmentTreeViewEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                    TreeDropdownList ddlDepartmentTreeView = new TreeDropdownList();
                    ddlDepartmentTreeView.SkinName = "Blue";
                    ddlDepartmentTreeView.Width = width;
                    ddlDepartmentTreeView.OnlySelectedLeaf = false;
                    ddlDepartmentTreeView.Tag = customDataFieldInfo;
                    ddlDepartmentTreeView.BeforeControlPopup += (sender, e) =>
                    {
                        if (ddlDepartmentTreeView.TreeView.Nodes.Count == 0)
                        {
                            if (physicalDataFieldType == PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot)
                            {
                                ddlDepartmentTreeView.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
                            }
                            else
                            {
                                ddlDepartmentTreeView.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract, decimal.One);
                            }
                            if (ddlDepartmentTreeView.Value != null)
                            {
                                string value = DataConvertionHelper.GetString(ddlDepartmentTreeView.Value);
                                if (!string.IsNullOrWhiteSpace(value))
                                {
                                    CommonItemList<decimal, CommonNode> commonItems = null;
                                    if (physicalDataFieldType == PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot)
                                    {
                                        commonItems = customDepartmentContract.GetTreeviewCommonNodesWithRoot(value);
                                        ddlDepartmentTreeView.LoadMatchedNodes(commonItems.CommonList);
                                    }
                                    else
                                    {
                                        commonItems = customDepartmentContract.GetTreeviewCommonNodes(value);
                                        ddlDepartmentTreeView.LoadMatchedNodes(decimal.One, commonItems.CommonList);
                                    }
                                    if (commonItems.Value > 0)
                                    {
                                        ddlDepartmentTreeView.SelectedNode = new CommonNode(commonItems.Value, commonItems.Text);
                                    }
                                    else
                                    {
                                        ddlDepartmentTreeView.SelectedNode = null;
                                    }
                                }
                            }
                            else
                            {
                                ddlDepartmentTreeView.InitalizeTreeView();
                            }
                        }
                    };
                    ddlDepartmentTreeView.AfterTreeNodeSelect += (sender, e) =>
                    {
                        CommonNode commonNode = ddlDepartmentTreeView.SelectedNode;
                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = commonNode;
                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, commonNode.NodeName);
                    };
                    devExpressGrid.FocusedColumn.ColumnEdit = GetRepositoryItemPopupContainerEdit(ddlDepartmentTreeView);
                    break;

                case PhysicalDataFieldType.MultiSelectedEnum:
                    RepositoryItemCheckedComboBoxEdit repositoryItemCheckedComboBoxEdit = new RepositoryItemCheckedComboBoxEdit();
                    repositoryItemCheckedComboBoxEdit.LookAndFeel.SkinName = "Money Twins";
                    repositoryItemCheckedComboBoxEdit.LookAndFeel.UseDefaultLookAndFeel = false;
                    repositoryItemCheckedComboBoxEdit.SelectAllItemVisible = false;
                    repositoryItemCheckedComboBoxEdit.ShowButtons = false;
                    repositoryItemCheckedComboBoxEdit.PopupSizeable = false;
                    IList<CommonNode> multiEnumCommonNodes = customEnumContract.GetChildNodes(customDataFieldInfo.EnumId);
                    repositoryItemCheckedComboBoxEdit.Items.AddRange(multiEnumCommonNodes.ToArray());
                    repositoryItemCheckedComboBoxEdit.EditValueChanged += (sdr, arg) =>
                    {
                        devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag = multiEnumCommonNodes;
                    };
                    devExpressGrid.FocusedColumn.ColumnEdit = repositoryItemCheckedComboBoxEdit;
                    break;

                case PhysicalDataFieldType.DocAttachment:
                case PhysicalDataFieldType.PicAttachment:
                case PhysicalDataFieldType.PDFAttachment:
                    DevExpressUpdatedUploadFile devExpressUploadFile = new DevExpressUpdatedUploadFile();
                    devExpressUploadFile.Width = width;
                    if (physicalDataFieldType == PhysicalDataFieldType.DocAttachment)
                    {
                        devExpressUploadFile.DocType = DocType.DocAttachment;
                        devExpressUploadFile.Filter = AppSettingHelper.DefaultDocFormat;
                    }
                    else if (physicalDataFieldType == PhysicalDataFieldType.PicAttachment)
                    {
                        devExpressUploadFile.DocType = DocType.PicAttachment;
                        devExpressUploadFile.Filter = AppSettingHelper.DefaultPictureFormat;
                    }
                    else
                    {
                        devExpressUploadFile.DocType = DocType.PDFAttachment;
                        devExpressUploadFile.Filter = AppSettingHelper.DefaultPDFFormat;
                    }
                    devExpressUploadFile.Load += (sender, e) =>
                    {
                        string fileName = DataConvertionHelper.GetString(devExpressGrid.FocusedValue);
                        devExpressUploadFile.TextContent = fileName;
                    };
                    devExpressUploadFile.OnViewLinkClicked += (sender, e) =>
                    {
                        if ((e.UserAction != UserAction.Delete) && (devExpressUploadFile.CustomData == null || devExpressUploadFile.CustomData.Length == 0)
                                && !string.IsNullOrWhiteSpace(devExpressUploadFile.FileName))
                        {                            
                            byte[] data = customDataFieldContract.GetFileData(devExpressGrid.FocusedColumn.FieldName, devExpressUploadFile.FileName);
                            devExpressUploadFile.CustomData = data;
                        }
                    };
                    devExpressUploadFile.SkinName = "Blue";
                    devExpressUploadFile.OnTextChanged += (sdr, arg) =>
                    {
                        devExpressGrid.SetFocusedRowCellValue(devExpressGrid.FocusedColumn.FieldName, devExpressUploadFile.FileName);                        
                    };
                    RepositoryItemPopupContainerEdit popupContainerEdit = GetRepositoryItemPopupContainerEdit(devExpressUploadFile);
                    popupContainerEdit.BeforePopup += (sender, e) =>
                    {
                        string fileName = DataConvertionHelper.GetString(devExpressGrid.FocusedValue);
                        devExpressUploadFile.TextContent = fileName;
                        devExpressUploadFile.CustomData = null;
                    };
                    devExpressGrid.FocusedColumn.ColumnEdit = popupContainerEdit;
                    break;
            }
        }

        /// <summary>
        /// 值变化
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="e"></param>
        /// <param name="dataFieldEditedState"></param>
        /// <param name="extendedCustomDataFieldInfo"></param>
        /// <param name="tableId"></param>
        /// <param name="tableName"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="warning"></param>
        /// <param name=""></param>
        /// <returns></returns>
        public bool DevExpressGridCellValueChanged(DevExpressGrid devExpressGrid, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e,
            DataFieldEditedState dataFieldEditedState, ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo, decimal tableId, string tableName,
            QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, ref string warning, ref bool refresh)
        {
            bool result = true;

            CommonNode commonNode = devExpressGrid.Tag as CommonNode;
            e.Column.OptionsColumn.AllowEdit = false;
            e.Column.OptionsColumn.ReadOnly = true;
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            decimal recordId = decimal.MinValue;
            switch (dataFieldEditedState)
            {
                case DataFieldEditedState.Edit:
                    if (e.RowHandle < 0)
                    {
                        warning = "请选选择记录。";
                        return false;
                    }
                    break;
                case DataFieldEditedState.BathcEdit:
                    if (devExpressGrid.MultiSelectedValues.Count == 0)
                    {
                        warning = "未进行批量选择，请先批量选择。";
                        return false;
                    }
                    break;
            }
            /* 1.验证是否允许为空 */
            if (extendedCustomDataFieldInfo.RequiredDataField && ((e.Value == null) || string.IsNullOrWhiteSpace(e.Value.ToString())))
            {
                warning = string.Format("{0}不能为空！", extendedCustomDataFieldInfo.LogicalName);
                return false;
            }
            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
            DbType dbType = DataFieldHelper.GetDbType(physicalDataFieldType);
            object dataFieldValue = null;
            IList<CommonDataField> relationDataFields = new List<CommonDataField>();
            IList<CommonDataField> commonDataFields = new List<CommonDataField>();
            if (e.Value != null)
            {
                string stringValue = e.Value.ToString();
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.Boolean:
                        dataFieldValue = DataConvertionHelper.GetConvertedBoolean(e.Value);
                        break;

                    case PhysicalDataFieldType.Int32:
                        if (!string.IsNullOrEmpty(stringValue))
                        {
                            //整数 
                            Regex regexInt32 = new Regex(@"^-?\d+$");
                            if (!regexInt32.IsMatch(stringValue))
                            {
                                warning = string.Format("{0}不能为非整数。", extendedCustomDataFieldInfo.LogicalName);
                                return false;
                            }
                            //超过范围转换失败                                
                            if (DataConvertionHelper.IsNullValue(DataConvertionHelper.GetConvertedInt(stringValue)))
                            {
                                warning = string.Format("{0}的整数值的超出了整数限制范围(-2147483648~2147483647)。", extendedCustomDataFieldInfo.LogicalName);
                                return false;
                            }
                            dataFieldValue = DataConvertionHelper.GetConvertedInt(e.Value);
                        }
                        break;

                    case PhysicalDataFieldType.Decimal:
                        if (!string.IsNullOrEmpty(stringValue))
                        {
                            //浮点数 
                            Regex regexDecimal = new Regex(@"^(-?\d+)(\.\d+)?$");
                            if (!regexDecimal.IsMatch(stringValue))
                            {
                                warning = string.Format("{0}不能为非实数。", extendedCustomDataFieldInfo.LogicalName);
                                return false;
                            }
                            int pos = stringValue.IndexOf('.');
                            if ((pos > 0 && (stringValue.Length - pos - 1) > extendedCustomDataFieldInfo.DataFieldLength) || stringValue.Length > 12)
                            {
                                warning = string.Format("{0}的实数长度限制的范围(0~12位)，小数位长度不能超过{1}。", extendedCustomDataFieldInfo.LogicalName, extendedCustomDataFieldInfo.DataFieldLength);
                                return false;
                            }
                            dataFieldValue = DataConvertionHelper.GetConvertedDecimal(stringValue);
                        }
                        break;

                    case PhysicalDataFieldType.ArbitraryString:
                    case PhysicalDataFieldType.ExtendedArbitraryString:
                    case PhysicalDataFieldType.NumeralString:
                    case PhysicalDataFieldType.CharString:
                    case PhysicalDataFieldType.MixedString:
                    case PhysicalDataFieldType.EncryptedString:
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.NumeralString:
                                if (!string.IsNullOrEmpty(stringValue))
                                {
                                    //整数 
                                    Regex numeralRegex = new Regex(@"^-?\d+$");
                                    if (!numeralRegex.IsMatch(stringValue))
                                    {
                                        warning = string.Format("{0}只能为数字组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                        return false;
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.CharString:
                                if (!string.IsNullOrEmpty(stringValue))
                                {
                                    //由26个英文字母组成的字符串 
                                    Regex charRegex = new Regex(@"^[A-Za-z]+$");
                                    if (!charRegex.IsMatch(stringValue))
                                    {
                                        warning = string.Format("{0}只能为由26个英文字母组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                        return false;
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.MixedString:
                                if (!string.IsNullOrEmpty(stringValue))
                                {
                                    //由数字和26个英文字母组成的字符串  
                                    Regex mixedRegex = new Regex(@"^[A-Za-z0-9]+$");
                                    if (!mixedRegex.IsMatch(stringValue))
                                    {
                                        warning = string.Format("{0}只能为由数字和26个英文字母组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                        return false;
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.ExtendedArbitraryString:
                            case PhysicalDataFieldType.ArbitraryString:
                            case PhysicalDataFieldType.EncryptedString:
                                if (!string.IsNullOrWhiteSpace(extendedCustomDataFieldInfo.RegexExpression))
                                {
                                    Regex regex = new Regex(extendedCustomDataFieldInfo.RegexExpression);
                                    if (!regex.IsMatch(stringValue))
                                    {
                                        warning = string.Format("{0}不符合要求的格式。", extendedCustomDataFieldInfo.LogicalName);
                                        return false;
                                    }
                                }
                                break;
                            default:
                                break;
                        }
                        if (physicalDataFieldType == PhysicalDataFieldType.EncryptedString)
                        {
                            dataFieldValue = CryptographyHelper.Encrypt(stringValue);
                        }
                        else
                        {
                            dataFieldValue = stringValue;
                        }
                        break;

                    case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                    case PhysicalDataFieldType.YearAndMonthAndDay:
                    case PhysicalDataFieldType.YearAndMonth:
                    case PhysicalDataFieldType.MonthAndDay:
                        DateTime currentDateTime = DataConvertionHelper.GetConvertedDateTime(e.Value, DateTime.MinValue);
                        if (!DataConvertionHelper.IsNullValue(currentDateTime))
                        {
                            switch (physicalDataFieldType)
                            {
                                case PhysicalDataFieldType.YearAndMonth:
                                    DateTime dtValue = DateTime.Parse(string.Format("{0}-{1}-01", currentDateTime.Year, currentDateTime.Month));
                                    if (DataConvertionHelper.IsNullValue(dtValue) || dtValue <= SqlDateTime.MinValue.Value || dtValue >= SqlDateTime.MaxValue.Value)
                                    {
                                        warning = string.Format("{0}选择错误。", extendedCustomDataFieldInfo.LogicalName);
                                        return false;
                                    }
                                    dataFieldValue = dtValue;
                                    break;

                                case PhysicalDataFieldType.YearAndMonthAndDay:
                                    dataFieldValue = DateTime.Parse(currentDateTime.ToShortDateString());
                                    break;

                                case PhysicalDataFieldType.MonthAndDay:
                                    dataFieldValue = DateTime.Parse(string.Format("{0}-{1}-{2}", AppSettingHelper.Year, currentDateTime.Month, currentDateTime.Day));
                                    break;

                                default:
                                    dataFieldValue = currentDateTime;
                                    break;
                            }
                        }
                        break;

                    case PhysicalDataFieldType.Time:
                        DateTime currentTime = DataConvertionHelper.GetConvertedDateTime(e.Value, DateTime.MinValue);
                        if (!DataConvertionHelper.IsNullValue(currentTime))
                        {
                            dataFieldValue = DateTime.Parse(string.Format("{0} {1}", AppSettingHelper.YearMonthDay, currentTime.ToLongTimeString()));
                        }
                        break;

                    case PhysicalDataFieldType.MultiSelectedEnum:
                    case PhysicalDataFieldType.CscadeEnum:
                        IList<CommonNode> nodes = customDataFieldContract.GetCommonNodesByParentDataFieldId(extendedCustomDataFieldInfo.DataFieldId);
                        foreach (CommonNode node in nodes)
                        {
                            CommonDataField enumCommonDataField = GetMultiEnumDependencyValue(devExpressGrid, node.NodeId, physicalDataFieldType, relationDataFields);
                            commonDataFields.Add(enumCommonDataField);
                        }
                        dataFieldValue = stringValue;
                        break;

                    case PhysicalDataFieldType.DropdownListEnum:
                    case PhysicalDataFieldType.DepartmentDropdownListEnum:
                    case PhysicalDataFieldType.DepartmentTreeViewEnum:
                    case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                    case PhysicalDataFieldType.TreeViewEnum:
                        dataFieldValue = stringValue;
                        IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodesByParentDataFieldId(extendedCustomDataFieldInfo.DataFieldId);
                        foreach (CommonNode node in commonNodes)
                        {
                            CommonDataField enumCommonDataField = GetEnumDependencyValue(devExpressGrid, node.NodeId, physicalDataFieldType, relationDataFields);
                            commonDataFields.Add(enumCommonDataField);
                        }
                        break;

                    case PhysicalDataFieldType.DropdownListEnumValue:
                    case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                    case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                    case PhysicalDataFieldType.TreeViewEnumValue:
                    case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                        dataFieldValue = stringValue;
                        break;

                    case PhysicalDataFieldType.Association:
                        BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                        dbType = DataFieldHelper.GetDbType(basedDataType);
                        dataFieldValue = e.Value;
                        break;

                    case PhysicalDataFieldType.PrimaryAssociation:
                        BasedDataType dataType = associatedDataFieldContract.GetBasedDataType(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                        dbType = DataFieldHelper.GetDbType(dataType);
                        dataFieldValue = e.Value;
                        decimal associationId = associatedDataFieldContract.GetAssociationId(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                        IList<CommonNode> associatedNodes = customDataFieldContract.GetCommonNodesByParentDataFieldId(extendedCustomDataFieldInfo.DataFieldId);
                        foreach (CommonNode node in associatedNodes)
                        {
                            if (node.ParentNodeId == extendedCustomDataFieldInfo.TableId)
                            {
                                CommonDataField associatedCommonDataField = GetAssociatedValue(devExpressGrid, node.NodeId, associationId, relationDataFields);
                                commonDataFields.Add(associatedCommonDataField);
                            }
                            else
                            {
                                CommonDataField associatedCommonDataField = GetAssociatedValue(devExpressGrid, node.NodeId, associationId, relationDataFields);
                                relationDataFields.Add(associatedCommonDataField);
                            }
                        }
                        break;

                    case PhysicalDataFieldType.DocAttachment:
                    case PhysicalDataFieldType.PicAttachment:
                    case PhysicalDataFieldType.PDFAttachment:
                        if (!string.IsNullOrWhiteSpace(stringValue))
                        {
                            if (Path.IsPathRooted(stringValue))
                            {
                                byte[] data = null;
                                switch (physicalDataFieldType)
                                {
                                    case PhysicalDataFieldType.DocAttachment:
                                    case PhysicalDataFieldType.PDFAttachment:
                                        if (physicalDataFieldType == PhysicalDataFieldType.DocAttachment)
                                        {
                                            if (!FileFormatHelper.VerfiyDocFormat(stringValue))
                                            {
                                                warning = "文件格式只能为：pdf(*.pdf), rar(*.rar, *.zip), doc(*.doc,*.docx), xls(*.xls,*.xlsx) 或者 ppt(*.ppt) 类型。";
                                                return false;
                                            }
                                        }
                                        else
                                        {
                                            if (!FileFormatHelper.VerfiyPDFFormat(stringValue))
                                            {
                                                warning = "文件格式只能为：pdf(*.pdf) 类型。";
                                                return false;
                                            }
                                        }
                                        using (FileStream fs = new FileStream(stringValue, FileMode.Open, FileAccess.Read))
                                        {
                                            BinaryReader r = new BinaryReader(fs);
                                            data = r.ReadBytes((int)fs.Length);
                                        }
                                        if (data.Length > AppSettingHelper.DefaultDocSize)
                                        {
                                            warning = string.Format("{0}的文件大小不能超过 {1} MB.", extendedCustomDataFieldInfo.LogicalName, AppSettingHelper.DefaultDocSize / (1024 * 1024));
                                            return false;
                                        }
                                        break;

                                    case PhysicalDataFieldType.PicAttachment:
                                        if (!FileFormatHelper.VerfiyImageFormat(stringValue))
                                        {
                                            warning = "图片格式只能为：JPEG(*.JPG;*.JPEG)，GIF(*.GIF), PNG(*.PNG) 或者 BMP(*.BMP)。";
                                            return false;
                                        }
                                        using (MemoryStream ms = new MemoryStream())
                                        {
                                            ImageFormat imageFormat = FileFormatHelper.GetImageFormat(stringValue.Substring(stringValue.LastIndexOf('.') + 1).ToUpper());
                                            Image img = Image.FromFile(stringValue);
                                            img.Save(ms, imageFormat);
                                            data = ms.ToArray();
                                        }
                                        if (data.Length > AppSettingHelper.DefaultPictureSize)
                                        {
                                            warning = string.Format("{0}的图片大小不能超过 {1} MB.", extendedCustomDataFieldInfo.LogicalName, AppSettingHelper.DefaultPictureSize / (1024 * 1024));
                                            return false;
                                        }
                                        break;
                                }
                                if (data.Length == 0)
                                {
                                    warning = "文件大小不能为0。";
                                    return false;
                                }
                                else
                                {
                                    dataFieldValue = new UpLoadFileInfo(Path.GetFileName(stringValue), string.Empty, data);
                                }
                            }
                        }
                        else
                        {
                            dataFieldValue = new UpLoadFileInfo(string.Empty, string.Empty, null);
                        }
                        break;

                    default:
                        throw new ArgumentException("不支持修改该类型的数据。");
                }
            }
            CommonDataField commonDataField = new CommonDataField(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.PhysicalName, dataFieldValue, dbType);
            commonDataFields.Add(commonDataField);
            if (commonDataFields.Count > 1 || dataFieldEditedState == DataFieldEditedState.BathcEdit 
                || dataFieldEditedState == DataFieldEditedState.CompleteEdit)
            {
                refresh = true;
            }
            string key = string.Format("{0}_RecordId", tableName);
            switch (dataFieldEditedState)
            {
                case DataFieldEditedState.Edit:
                    recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[key]);
                    dataAuditingContract.Update(tableId, recordId, commonDataFields, relationDataFields);
                    break;

                case DataFieldEditedState.BathcEdit:
                    IList<decimal> recordIds = new List<decimal>();
                    foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                    {
                        recordIds.Add(DataConvertionHelper.GetDecimal(rowEvent.Values[key]));
                    }
                    dataAuditingContract.Update(tableId, recordIds, commonDataFields, relationDataFields);
                    break;

                case DataFieldEditedState.CompleteEdit:
                    recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[key]);
                    dataAuditingContract.Update(tableId, recordId, commonDataFields, relationDataFields, queryBuilder, whereConditons);
                    break;
            }

            return result;
        }

        #region 私有方法

        /// <summary>
        /// 获得多选枚举依赖值
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="relationDataFields"></param>
        /// <returns></returns>
        private CommonDataField GetMultiEnumDependencyValue(DevExpressGrid devExpressGrid, decimal dataFieldId, PhysicalDataFieldType physicalDataFieldType, IList<CommonDataField> relationDataFields)
        {
            IList<CommonNode> commonNodes = devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag as IList<CommonNode>;

            return relationDataBusiness.GetMultiEnumDependencyValue(commonNodes, dataFieldId, physicalDataFieldType, relationDataFields);
        }

        /// <summary>
        /// 获得关联的值
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="associationId"></param>
        /// <param name="relationDataFields"></param>
        /// <returns></returns>
        private CommonDataField GetAssociatedValue(DevExpressGrid devExpressGrid, decimal dataFieldId, decimal associationId, IList<CommonDataField> relationDataFields)
        {
            object tag = devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag;
            decimal recordId = decimal.MinValue;
            if (tag != null)
            {
                recordId = DataConvertionHelper.GetConvertedDecimal(tag, decimal.MinValue);
            }
            return relationDataBusiness.GetAssociatedValue(recordId, dataFieldId, associationId, relationDataFields);
        }

        /// <summary>
        /// 获得单选枚举依赖值
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="relationDataFields"></param>
        /// <returns></returns>
        private CommonDataField GetEnumDependencyValue(DevExpressGrid devExpressGrid, decimal dataFieldId, PhysicalDataFieldType physicalDataFieldType, IList<CommonDataField> relationDataFields)
        {
            CommonNode commonNode = devExpressGrid.Columns[devExpressGrid.FocusedColumn.FieldName].Tag as CommonNode;

            return relationDataBusiness.GetEnumDependencyValue(commonNode, dataFieldId, physicalDataFieldType, relationDataFields);
        }

        /// <summary>
        /// 加载关联数据
        /// </summary>
        /// <param name="searchLookUpEditWithGrid"></param>
        /// <param name="associationId"></param>
        /// <param name="content"></param>
        private void LoadAssociatedData(SearchLookUpEditWithGrid searchLookUpEditWithGrid, decimal associationId, string content)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            if (!string.IsNullOrWhiteSpace(content))
            {
                IList<AssociatedDataFieldInfo> associatedDataFieldInfos = associatedDataFieldContract.GetModelInfos(associationId);
                foreach (AssociatedDataFieldInfo associatedDataFieldInfo in associatedDataFieldInfos)
                {
                    BasedDataType basedDataType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                    if (basedDataType == BasedDataType.String)
                    {
                        whereConditons.Add(new WhereConditon(associatedDataFieldInfo.PhysicalName, associatedDataFieldInfo.PhysicalName, DbType.String, string.Format("%{0}%", content),
                          DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    }
                }
            }
            int totalCount = 0;
            searchLookUpEditWithGrid.DataSource = customAssociationContract.GetAssociationData(associationId, searchLookUpEditWithGrid.PageSize * searchLookUpEditWithGrid.CurrentPageIndex,
                searchLookUpEditWithGrid.PageSize, whereConditons, ref totalCount).Tables[0];
            searchLookUpEditWithGrid.RecordCount = totalCount;
            string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            searchLookUpEditWithGrid.Columns[key].Visible = false;
        }

        /// <summary>
        /// 获得弹出框界面
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private RepositoryItemPopupContainerEdit GetRepositoryItemPopupContainerEdit(Control control)
        {
            PopupContainerControl containerControl = new PopupContainerControl();
            containerControl.Width = control.Width;
            containerControl.Height = control.Height;
            containerControl.Controls.Add(control);

            RepositoryItemPopupContainerEdit popupContainerEdit = new RepositoryItemPopupContainerEdit();
            popupContainerEdit.AutoHeight = false;
            popupContainerEdit.PopupSizeable = false;
            popupContainerEdit.ShowPopupCloseButton = false;
            popupContainerEdit.PopupControl = containerControl;
            popupContainerEdit.LookAndFeel.SkinName = "Money Twins";
            popupContainerEdit.LookAndFeel.UseDefaultLookAndFeel = false;
            
            return popupContainerEdit;
        }

        #endregion
    }
}
