using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AppFramework.WinFormsLibrary;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using AppFramework.Core;

namespace AppFramework.WinFormsControls
{
    public partial class DevExpressGrid : DevExpress.XtraEditors.XtraUserControl
    {
        #region 定义私有常量

        /// <summary>
        /// 列名
        /// </summary>
        private const string CHECKBOX_COLUMN_CAPTION_NAME = "批量选择";

        /// <summary>
        /// 列标题
        /// </summary>
        private const string CHECKBOX_COLUMN_NAME = "CustomCheckbox";

        /// <summary>
        /// 编号
        /// </summary>
        private const string EDIT_COLUMN_NAME = "CustomEditedName";

        #endregion

        #region 定义私有变量

        private IList<int> checkBoxUnBoundData = new List<int>();

        #endregion

        #region 定义成员变量 

        /// <summary>
        /// 列标题的排列方式
        /// </summary>
        private HorzAlignment _appearanceHeaderHAlignment;

        /// <summary>
        /// 列内容的排列方式
        /// </summary>
        private HorzAlignment _appearanceCellHAlignment;

        /// <summary>
        /// 是否允许导入 Excel
        /// </summary>
        private bool _exportedExcel;

        /// <summary>
        /// 是否允许导入 Excel
        /// </summary>
        private bool _importedExcel;        

        /// <summary>
        /// 是否显示 GridView 的 CheckBox
        /// </summary>
        private bool _isShowCheckBox;

        /// <summary>
        /// Checkbox 的列名称
        /// </summary>
        private string _checkboxColumnCaption;

        /// <summary>
        /// GridView 是否允许编辑
        /// </summary>
        private bool _isAllowEditing = false;

        /// <summary>
        /// GridView 是否允许列排序
        /// </summary>
        private DefaultBoolean _isAllowSorting = DefaultBoolean.False;

        /// <summary>
        /// 列的默认固定宽度
        /// </summary>
        private int _columnWidth = 0;

        /// <summary>
        /// 关键字列
        /// </summary>
        private string[] _dataKeyNames;

        /// <summary>
        /// 标题列名称
        /// </summary>
        private string[] _columnHeaderTexts;

        /// <summary>
        /// 状态列字段
        /// </summary>
        private string _stateColumnDataFieldName = string.Empty;

        /// <summary>
        /// 状态列名称
        /// </summary>
        private string _stateColumnCaption = string.Empty;

        /// <summary>
        /// 是否是主表
        /// </summary>
        private bool _isMainTable = false;

        /// <summary>
        /// 每页显示的记录数
        /// </summary>
        private int _pageSize = 50;

        /// <summary>
        /// 当前页面的索引
        /// </summary>
        private int _currentPageIndex = 0;

        /// <summary>
        /// 新的页面的索引
        /// </summary>
        private int _newPageIndex = 0;

        /// <summary>
        /// 总页数
        /// </summary>
        private int _pageCount = 0;

        /// <summary>
        /// 总记录数
        /// </summary>
        private int _recordCount = 0;

        /// <summary>
        /// 当前页记录数
        /// </summary>
        private int _currentPageSize = 0;

        /// <summary>
        /// 多选值的集合
        /// </summary>
        private IList<RowEvent> _multiSelectedValues = null;

        /// <summary>
        /// 右键菜单权限
        /// </summary>
        private long _authority;

        #endregion

        #region 定义属性

        /// <summary>
        /// 提示控件
        /// </summary>
        [DefaultValue(null)]
        [DXCategory("自定义杂项")]
        public ToolTipController ToolTipController
        {

            get
            {
                return gridControl.ToolTipController;
            }
            set
            {
                gridControl.ToolTipController = value;
            }
        }

        /// <summary>
        /// 列标题的排列方式
        /// </summary>
        [
        Description("列标题的排列方式"),
        Category("自定义杂项"),
        DefaultValue(HorzAlignment.Default),
        ]
        public HorzAlignment AppearanceHeaderHAlignment
        {
            get
            {
                
                return _appearanceHeaderHAlignment;
            }
            set
            {
                _appearanceHeaderHAlignment = value;
            }
        }

        /// <summary>
        /// 列内容的排列方式
        /// </summary>
        [
        Description("列内容的排列方式"),
        Category("自定义杂项"),
        DefaultValue(HorzAlignment.Default),
        ]
        public HorzAlignment AppearanceCellHAlignment
        {
            get
            {
                return _appearanceCellHAlignment;
            }
            set
            {
                _appearanceCellHAlignment = value;
            }
        }

        public string FootText
        {
            get;
            set;
        }

        /// <summary>
        /// GridView 每页显示的记录数
        /// </summary>
        [
        Description("每页显示的记录数"),
        Category("自定义杂项"),
        DefaultValue("复选项"),
        ]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = value;
            }
        }

        /// <summary>
        /// GridView 的数据源，只能绑定 DataTable 或是 DataView, 或者 IList 类型的对象
        /// </summary>
        [
        Description("GridView 的数据源"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public object DataSource
        {
            get
            {
                return gridControl.DataSource;
            }
            set
            {
                checkBoxUnBoundData.Clear();
                gridControl.DataSource = value;
                int visibleIndex = 0;
                if (value != null)
                {
                    gridView.PopulateColumns();
                    if (!gridView.OptionsView.ColumnAutoWidth)
                    {
                        gridView.BestFitColumns();
                    }
                    if (_isShowCheckBox)
                    {
                        InsertCheckBoxColumn();
                        visibleIndex++;
                    }
                    else
                    {
                        RemoveCheckBoxColumn();
                    }
                    checkBoxUnBoundData.Clear();
                    foreach (string dataKeyName in DataKeyNames)
                    {
                        if (gridView.Columns[dataKeyName] != null)
                        {
                            gridView.Columns[dataKeyName].Visible = false;
                        }
                    }
                    int index = 0;
                    for (int i = 0; i < gridView.Columns.Count; i++)
                    {
                        if ((index < ColumnHeaderTexts.Length) && (gridView.Columns[i].Visible == true)
                            && !gridView.Columns[i].Name.Equals(CHECKBOX_COLUMN_NAME))
                        {
                            gridView.Columns[i].Caption = ColumnHeaderTexts[index];
                            index++;
                        }
                        if (gridView.Columns[i].Name.Equals(CHECKBOX_COLUMN_NAME))
                        {
                            gridView.Columns[i].OptionsColumn.AllowEdit = true;
                            gridView.Columns[i].OptionsColumn.ReadOnly = false;
                        }
                        else
                        {
                            if (!_isAllowEditing)
                            {
                                gridView.Columns[i].OptionsColumn.AllowEdit = false;
                                gridView.Columns[i].OptionsColumn.ReadOnly = true;
                            }
                        }
                        if (!gridView.OptionsView.ColumnAutoWidth && !gridView.Columns[i].Name.Equals(CHECKBOX_COLUMN_NAME) && _columnWidth > 0)
                        {
                            gridView.Columns[i].Width = _columnWidth;
                        }
                        gridView.Columns[i].OptionsFilter.AllowAutoFilter = false;
                        gridView.Columns[i].OptionsFilter.AllowFilter = false;
                        gridView.Columns[i].AppearanceHeader.TextOptions.HAlignment = _appearanceHeaderHAlignment;
                        gridView.Columns[i].AppearanceCell.TextOptions.HAlignment = _appearanceCellHAlignment;
                        gridView.Columns[i].OptionsColumn.AllowSort = _isAllowSorting;
                    }
                    if (!GridViewReadOnly && AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.View)))
                    {
                        InsertViewColumn(visibleIndex);
                        visibleIndex++;
                    }
                    if (!GridViewReadOnly && AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.Edit)))
                    {
                        InsertEditedColumn(visibleIndex);
                        visibleIndex++;
                    }
                    if (!string.IsNullOrWhiteSpace(_stateColumnDataFieldName))
                    {
                        GridColumn gridColumn = gridView.Columns.ColumnByFieldName(_stateColumnDataFieldName);
                        gridColumn.Fixed = FixedStyle.Left;
                        gridColumn.Caption = _stateColumnCaption;
                        gridColumn.ColumnEdit = GetRepositoryItemImageComboBox();
                        gridColumn.VisibleIndex = visibleIndex;
                    }
                }
            }
        }

        /// <summary>
        /// GridView 的记录数
        /// </summary>
        [
        Description("GridView 的记录数"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        DefaultValue(0),
        ]
        public int RecordCount
        {
            get
            {
                return _recordCount;
            }
            set
            {                
                _recordCount = value;                
                ShowFooterInfo();
            }
        }

        /// <summary>
        /// GridView 的只读状态
        /// </summary>
        [
        Description("GridView 的只读状态"),
        Category("自定义杂项"),
        DefaultValue(false),
        ]
        public bool GridViewReadOnly
        {
            get;
            set;
        }

        /// <summary>
        /// 是否显示 GridView 的 CheckBox
        /// </summary>
        [
        Description("是否显示 GridView 的 CheckBox"),
        Category("自定义杂项"),
        DefaultValue(false),
        ]
        public bool IsShowCheckBox
        {
            get
            {
                return _isShowCheckBox;
            }
            set
            {
                _isShowCheckBox = value;
                if (value)
                {
                    barSubItemSelect.Visibility = BarItemVisibility.Always;                    
                }
                else
                {
                    barSubItemSelect.Visibility = BarItemVisibility.Never;                    
                }
            }
        }

        /// <summary>
        /// GridView 的  Checkbox列名称
        /// </summary>
        [
        Description("GridView 的 选择模式，默认的是行选择"),
        Category("自定义杂项"),
        DefaultValue("复选项"),
        ]
        public string CheckboxColumnCaption
        {
            get
            {
                return _checkboxColumnCaption;
            }
            set
            {
                if (_checkboxColumnCaption == value)
                    return;
                _checkboxColumnCaption = value;
            }
        }

        /// <summary>
        /// 当前页被选择的行集合
        /// </summary>
        [
        Description("当前页被选择的行集合"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        DefaultValue(0),
        ]
        public IList<int> SelectedRows
        {
            get
            {
                return checkBoxUnBoundData;
            }
        }

        /// <summary>
        /// GridView 是否允许编辑
        /// </summary>
        [
        Description("GridView 是否允许编辑，默认不允许编辑"),
        Category("自定义杂项"),
        DefaultValue(false),
        ]
        public bool Editable
        {
            get
            {
                return _isAllowEditing;
            }
            set
            {
                if (_isAllowEditing == value)
                    return;
                _isAllowEditing = value;           
            }
        }

        /// <summary>
        /// GridView 是否允许列排序
        /// </summary>
        [
        Description("GridView 是否允许列排序"),
        Category("自定义杂项"),
        DefaultValue(DefaultBoolean.False),
        ]
        public DefaultBoolean IsAllowSorting
        {
            get
            {
                return _isAllowSorting;
            }
            set
            {
                if (_isAllowSorting == value)
                    return;
                _isAllowSorting = value;           
            }
        }

        /// <summary>
        /// 列是否自动宽度
        /// </summary>
        [
        Description("GridView 列是否自动宽度"),
        Category("自定义杂项"),
        DefaultValue(true),
        ]
        public bool ColumnAutoWidth
        {
            get
            {
                return gridView.OptionsView.ColumnAutoWidth;
            }
            set
            {
                if (value)
                {
                    gridView.BestFitColumns();
                }
                gridView.OptionsView.ColumnAutoWidth = value;                               
            }
        }     

        /// <summary>
        /// 列的默认固定宽度
        /// </summary>
        [
        Description("GridView 列固定宽度(当 ColumnAutoWidth 属性为 false 时有效，0表示采用系统默认宽度)"),
        Category("自定义杂项"),
        DefaultValue(0),
        ]
        public int ColumnWidth
        {
            get
            {
                return _columnWidth;
            }
            set
            {
                if (_columnWidth == value)
                    return;
                _columnWidth = value;
            }
        }

        [Editor("StringCollectionEditor, AppFramework.WinFormsControls, Version=1.0.2708.21050, Culture=neutral, PublicKeyToken=null", typeof(UITypeEditor)),
        Description("关键字名称集合"),
        Category("自定义杂项"),
         DefaultValue((string)null),
         TypeConverter(typeof(StringArrayConverter))]
        public string[] DataKeyNames
        {
            get
            {
                object obj2 = _dataKeyNames;
                if (obj2 != null)
                {
                    return (string[])((string[])obj2).Clone();
                }
                return new string[0];
            }
            set
            {
                if (value != null)
                {
                    _dataKeyNames = (string[])value.Clone();
                }
                else
                {
                    _dataKeyNames = null;
                }
            }
        }

        [Editor("StringCollectionEditor, AppFramework.WinFormsControls, Version=1.0.2708.21050, Culture=neutral, PublicKeyToken=null", typeof(UITypeEditor)),
       Description("列标题名称集合"),
        Category("自定义杂项"),
         DefaultValue((string)null),
         TypeConverter(typeof(StringArrayConverter))]
        public string[] ColumnHeaderTexts
        {
            get
            {
                object obj2 = _columnHeaderTexts;
                if (obj2 != null)
                {
                    return (string[])((string[])obj2).Clone();
                }
                return new string[0];
            }
            set
            {
                if (value != null)
                {
                    _columnHeaderTexts = (string[])value.Clone();
                }
                else
                {
                    _columnHeaderTexts = null;
                }
            }
        }

        /// <summary>
        /// 状态列字段
        /// </summary>
        [
        Description("多选项绑定的字段"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public string StateColumnDataFieldName
        {
            get
            {
                return _stateColumnDataFieldName;
            }
            set
            {
                _stateColumnDataFieldName = value;
            }
        }

        /// <summary>
        /// 状态列名称
        /// </summary>
        [
        Description("状态列名称"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public string StateColumnCaption
        {
            get
            {
                return _stateColumnCaption;
            }
            set
            {
                _stateColumnCaption = value;
            }
        }

        /// <summary>
        /// 是否是主表
        /// </summary>
        public bool IsMainTable
        {
            get
            {
                return _isMainTable;
            }
            set
            {
                if (_isMainTable == value)
                    return;
                _isMainTable = value;
            }
        }

        /// <summary>
        /// GridView 的 选择模式
        /// </summary>
        [
        Description("GridView 的 选择模式，默认的是行选择"),
        Category("自定义杂项"),
        DefaultValue(GridMultiSelectMode.RowSelect),
        ]
        public GridMultiSelectMode SelectionMode
        {
            get
            {
                return gridView.OptionsSelection.MultiSelectMode;
            }
            set
            {
                gridView.OptionsSelection.MultiSelectMode = value;
            }
        }
        
        /// <summary>
        /// 当前页面的索引
        /// </summary>
        [
        Description("当前页面的索引"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        DefaultValue(0),
        ]
        public int CurrentPageIndex
        {
            get
            {
                return _currentPageIndex;
            }
            set
            {
                _currentPageIndex = value;
            }
        }

        /// <summary>
        /// 新的页面的索引
        /// </summary>
        [
        Description("新的页面的索引"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        DefaultValue(0),
        ]
        public int NewPageIndex
        {
            get
            {
                return _newPageIndex;
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        [
        Description("总页数"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        DefaultValue(0),
        ]
        public int PageCount
        {
            get
            {
                return _pageCount;
            }
        }

        /// <summary>
        /// 当前页记录数
        /// </summary>
        [
        Description("当前页记录数"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        DefaultValue(0),
        ]
        public int CurrentPageSize
        {
            get
            {
                return _currentPageSize;
            }
        }

        /// <summary>
        /// 当前页面的实际行数
        /// </summary>
        [
        Description("当前页面的实际行数。"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        DefaultValue(0),
        ]
        public int RowCount
        {
            get
            {
                return gridView.RowCount;
            }
        }

        /// <summary>
        /// GridView 的列
        /// </summary>
        [
        Description("GridView 的列"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public GridColumnCollection Columns
        {
            get
            {
                return gridView.Columns;                
            }
        }

        /// <summary>
        /// 当前选择的列
        /// </summary>
        [
        Description("当前选择的行索引"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public GridColumn FocusedColumn
        {
            get
            {
                return gridView.FocusedColumn;
            }
            set
            {
                gridView.FocusedColumn = value;
            }
        }


        /// <summary>
        /// 当前选择的行索引
        /// </summary>
        [
        Description("当前选择的行索引"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public int FocusedRowHandle
        {
            get
            {
                return gridView.FocusedRowHandle;
            }
            set
            {
                gridView.FocusedRowHandle = value;
            }
        }

        /// <summary>
        /// 当前选择的数据行
        /// </summary>
        [
        Description("当前选择的行索引"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public DataRow FocusedDataRow
        {
            get
            {
                return gridView.GetFocusedDataRow();                
            }
        }

        /// <summary>
        /// 当前选择的单元格的值
        /// </summary>
        [
        Description("当前选择的单元格的值"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public object FocusedValue
        {
            get
            {
                return gridView.GetFocusedValue();
            }
        }

        /// <summary>
        /// 当前选择的行的关键字的值
        /// </summary>
        [
        Description("当前选择的行的关键字的值"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public RowEvent DataKeyValues
        {
            get
            {
                return GetRowEvent();
            }
        }
        

        /// <summary>
        /// 多选择时值的集合
        /// </summary>
        [
        Description("多选择时值的集合"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public IList<RowEvent> MultiSelectedValues
        {
            get
            {
                if (_multiSelectedValues == null)
                {
                    _multiSelectedValues = new List<RowEvent>();
                }
                else
                {
                    _multiSelectedValues.Clear();
                }                
                for (int i = 0; i < gridView.RowCount; i++)
                {
                    bool check = Convert.ToBoolean(gridView.GetRowCellValue(i, CHECKBOX_COLUMN_NAME));
                    if (check)
                    {
                        OrderedDictionary orderedDictionary = new OrderedDictionary(DataKeyNames.Length);
                        foreach (string dataKeyName in DataKeyNames)
                        {
                            orderedDictionary.Add(dataKeyName, gridView.GetRowCellValue(i, dataKeyName));
                        }
                        RowEvent rowEvent = new RowEvent(i, orderedDictionary);
                        _multiSelectedValues.Add(rowEvent);
                    }
                }

                return _multiSelectedValues;
            }
        }

        /// <summary>
        /// 右键菜单权限
        /// </summary>
        [
        Description("右键菜单权限"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public long Authority
        {
            get
            {
                return _authority;
            }
            set
            {
                _authority = value;
                SetAuthority();
            }
        }

        /// <summary>
        /// 多选列的标题名称
        /// </summary>
        [
        Description("多选列的标题名称"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public string CheckboxColumnName
        {
            get
            {
                return CHECKBOX_COLUMN_NAME;
            }
        }

        /// <summary>
        /// 编辑的标题名称
        /// </summary>
        [
        Description("编辑的标题名称"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public string CustomEditedName
        {
            get
            {
                return EDIT_COLUMN_NAME;
            }
        }

        /// <summary>
        /// 是否允许导出Excel
        /// </summary>
        [
        Description("是否允许导出")
        ]
        public bool ExportedExcel
        {
            set
            {
                _exportedExcel = value;
                if (value)
                {
                    btnItmExport.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    btnItmExport.Visibility = BarItemVisibility.Never;
                }
            }
            get
            {
                return _exportedExcel;
            }
        }

        /// <summary>
        /// 是否允许导入 Excel
        /// </summary>
        [
        Description("是否允许导入")
        ]
        public bool ImportedExcel
        {
            set
            {
                _importedExcel = value;
                if (value)
                {
                    btnItmImport.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    btnItmExport.Visibility = BarItemVisibility.Never;
                }
            }
            get
            {
                return _importedExcel;
            }
        }

        /// <summary>
        /// 附加数据
        /// </summary>
        [
        Description("附加数据"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public object Data { get; set; }

        /// <summary>
        /// GridControl 控件
        /// </summary>
        [
        Description("GridControl 控件"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public DevExpress.XtraGrid.GridControl DevExpressGridControl
        {
            get
            {
                return gridControl;
            }
        }

        /// <summary>
        /// GridView 控件
        /// </summary>
        [
        Description("GridView 控件"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public DevExpress.XtraGrid.Views.Grid.GridView DevExpressGridView
        {
            get
            {
                return gridView;
            }
        }

        #endregion

        #region 定义事件

        #region 定义"翻页"事件

        /// <summary>
        /// 定义"翻页"事件
        /// </summary>
        private event EventHandler<CustomGridViewPageEventArgs> _OnPageIndexChanged;

        /// <summary>
        /// 定义"翻页"事件访问器
        /// </summary>
        [
        Description(@"点击""翻页""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<CustomGridViewPageEventArgs> OnPageIndexChanged
        {
            add
            {
                _OnPageIndexChanged += value;
            }
            remove
            {
                _OnPageIndexChanged -= value;
            }
        }

        /// <summary>
        /// 定义"翻页"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void PageIndexChanged(CustomGridViewPageEventArgs e)
        {
            if (_OnPageIndexChanged != null) _OnPageIndexChanged(this, e);
        }
        #endregion

        #region 定义"行单点击"事件
        /// <summary>
        /// 定义"行单点击"事件
        /// </summary>
        private event EventHandler<RowEvent> _OnRowClick;

        /// <summary>
        /// 定义"行单点击"事件访问器
        /// </summary>
        [
        Description(@"点击""行单点击""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<RowEvent> OnRowClick
        {
            add
            {
                _OnRowClick += value;
            }
            remove
            {
                _OnRowClick -= value;
            }
        }

        /// <summary>
        /// 定义"行单点击"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void RowClick(RowEvent e)
        {
            if (_OnRowClick != null) _OnRowClick(this, e);
        }

        #endregion

        #region 定义"双击"事件
        /// <summary>
        /// 定义"双击"事件
        /// </summary>
        private event EventHandler<RowEvent> _OnRowDoubleClick;

        /// <summary>
        /// 定义"双击"事件访问器
        /// </summary>
        [
        Description(@"点击""双击""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<RowEvent> OnRowDoubleClick
        {
            add
            {
                _OnRowDoubleClick += value;
            }
            remove
            {
                _OnRowDoubleClick -= value;
            }
        }

        /// <summary>
        /// 定义"双击"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void RowDoubleClick(RowEvent e)
        {
            if (_OnRowDoubleClick != null) _OnRowDoubleClick(this, e);
        }
        #endregion

        #region 定义"右键弹出菜单"事件
        /// <summary>
        /// 定义"右键弹出菜单"事件
        /// </summary>
        private event EventHandler<CancelEventArgs> _OnBeforePopupMenu;

        /// <summary>
        /// 定义"右键弹出菜单"事件访问器
        /// </summary>
        [
        Description(@"点击""右键弹出菜单""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<CancelEventArgs> OnBeforePopupMenu
        {
            add
            {
                _OnBeforePopupMenu += value;
            }
            remove
            {
                _OnBeforePopupMenu -= value;
            }
        }

        /// <summary>
        /// 定义"右键弹出菜单"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void BeforePopupMenu(CancelEventArgs e)
        {
            if (_OnBeforePopupMenu != null) _OnBeforePopupMenu(this, e);
        }
        #endregion        

        #region 定义"移动记录"事件
        /// <summary>
        /// 定义"移动记录"事件
        /// </summary>
        private event EventHandler<ExtendedItemClickEventArgs> _OnRecordSortingChanged;

        /// <summary>
        /// 定义"移动记录"事件访问器
        /// </summary>
        [
        Description(@"点击""移动记录""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ExtendedItemClickEventArgs> OnRecordSortingChanged
        {
            add
            {
                _OnRecordSortingChanged += value;
            }
            remove
            {
                _OnRecordSortingChanged -= value;
            }
        }

        /// <summary>
        /// 定义"移动记录"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void RecordSortingChanged(ExtendedItemClickEventArgs e)
        {
            if (_OnRecordSortingChanged != null) _OnRecordSortingChanged(this, e);
        }
        #endregion

        #region 定义"增加"事件

        /// <summary>
        /// 定义"增加"事件
        /// </summary>
        private event EventHandler<ItemClickEventArgs> _OnAddClick;

        /// <summary>
        /// 定义"增加"事件访问器
        /// </summary>
        [
        Description("增加"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ItemClickEventArgs> OnAddClick
        {
            add
            {
                _OnAddClick += value;
            }
            remove
            {
                _OnAddClick -= value;
            }
        }

        /// <summary>
        /// 定义"增加"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void AddClick(ItemClickEventArgs e)
        {
            if (_OnAddClick != null) _OnAddClick(this, e);
        }
        #endregion

        #region 定义"编辑"事件

        /// <summary>
        /// 定义"编辑"事件
        /// </summary>
        private event EventHandler<ItemClickEventArgs> _OnEditClick;

        /// <summary>
        /// 定义"编辑"事件访问器
        /// </summary>
        [
        Description("编辑"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ItemClickEventArgs> OnEditClick
        {
            add
            {
                _OnEditClick += value;
            }
            remove
            {
                _OnEditClick -= value;
            }
        }

        /// <summary>
        /// 定义"编辑"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void EditClick(ItemClickEventArgs e)
        {
            if (_OnEditClick != null) _OnEditClick(this, e);
        }
        #endregion

        #region 定义"行查看"事件

        /// <summary>
        /// 定义"行查看"事件
        /// </summary>
        private event EventHandler<RowEvent> _OnRowView;

        /// <summary>
        /// 定义"行查看"事件访问器
        /// </summary>
        [
        Description(@"点击""行查看""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<RowEvent> OnRowView
        {
            add
            {
                _OnRowView += value;
            }
            remove
            {
                _OnRowView -= value;
            }
        }

        /// <summary>
        /// 定义"行查看"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void RowView(RowEvent e)
        {
            if (_OnRowView != null) _OnRowView(this, e);
        }

        #endregion

        #region 定义"行编辑"事件

        /// <summary>
        /// 定义"行编辑"事件
        /// </summary>
        private event EventHandler<RowEvent> _OnRowEdit;

        /// <summary>
        /// 定义"行编辑"事件访问器
        /// </summary>
        [
        Description(@"点击""行编辑""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<RowEvent> OnRowEdit
        {
            add
            {
                _OnRowEdit += value;
            }
            remove
            {
                _OnRowEdit -= value;
            }
        }

        /// <summary>
        /// 定义"行编辑"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void RowEdit(RowEvent e)
        {
            if (_OnRowEdit != null) _OnRowEdit(this, e);
        }

        #endregion

        #region 定义"批量编辑"事件

        /// <summary>
        /// 定义"批量编辑"事件
        /// </summary>
        private event EventHandler<ItemClickEventArgs> _OnBatchEditClick;

        /// <summary>
        /// 定义"批量编辑"事件访问器
        /// </summary>
        [
        Description(@"点击""批量编辑""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ItemClickEventArgs> OnBatchEditClick
        {
            add
            {
                _OnBatchEditClick += value;
            }
            remove
            {
                _OnBatchEditClick -= value;
            }
        }

        /// <summary>
        /// 定义"批量编辑"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void BatchEditClick(ItemClickEventArgs e)
        {
            if (_OnBatchEditClick != null) _OnBatchEditClick(this, e);
        }
        #endregion

        #region 定义"完全编辑"事件

        /// <summary>
        /// 定义"完全编辑"事件
        /// </summary>
        private event EventHandler<ItemClickEventArgs> _OnCompleteEditClick;

        /// <summary>
        /// 定义"完全编辑"事件访问器
        /// </summary>
        [
        Description("完全编辑"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ItemClickEventArgs> OnCompleteEditClick
        {
            add
            {
                _OnCompleteEditClick += value;
            }
            remove
            {
                _OnCompleteEditClick -= value;
            }
        }

        /// <summary>
        /// 定义"完全编辑"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void CompleteEditClick(ItemClickEventArgs e)
        {
            if (_OnCompleteEditClick != null) _OnCompleteEditClick(this, e);
        }
        #endregion

        #region 定义"删除"事件
        /// <summary>
        /// 定义"删除"事件
        /// </summary>
        private event EventHandler<ItemClickEventArgs> _OnDeleteClick;

        /// <summary>
        /// 定义"删除"事件访问器
        /// </summary>
        [
        Description("删除"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ItemClickEventArgs> OnDeleteClick
        {
            add
            {
                _OnDeleteClick += value;
            }
            remove
            {
                _OnDeleteClick -= value;
            }
        }

        /// <summary>
        /// 定义"删除"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void DeleteClick(ItemClickEventArgs e)
        {
            if (_OnDeleteClick != null) _OnDeleteClick(this, e);
        }
        #endregion

        #region 定义"批量删除"事件

        /// <summary>
        /// 定义"批量删除"事件
        /// </summary>
        private event EventHandler<ItemClickEventArgs> _OnBatchDeleteClick;

        /// <summary>
        /// 定义"批量删除"事件访问器
        /// </summary>
        [
        Description(@"点击""批量删除""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ItemClickEventArgs> OnBatchDeleteClick
        {
            add
            {
                _OnBatchDeleteClick += value;
            }
            remove
            {
                _OnBatchDeleteClick -= value;
            }
        }

        /// <summary>
        /// 定义"批量删除"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void BatchDeleteClick(ItemClickEventArgs e)
        {
            if (_OnBatchDeleteClick != null) _OnBatchDeleteClick(this, e);
        }
        #endregion

        #region 定义"完全删除"事件
        /// <summary>
        /// 定义"完全删除"事件
        /// </summary>
        private event EventHandler<ItemClickEventArgs> _OnCompleteDeleteClick;

        /// <summary>
        /// 定义"完全删除"事件访问器
        /// </summary>
        [
        Description("完全删除"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ItemClickEventArgs> OnCompleteDeleteClick
        {
            add
            {
                _OnCompleteDeleteClick += value;
            }
            remove
            {
                _OnCompleteDeleteClick -= value;
            }
        }

        /// <summary>
        /// 定义"完全删除"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void CompleteDeleteClick(ItemClickEventArgs e)
        {
            if (_OnCompleteDeleteClick != null) _OnCompleteDeleteClick(this, e);
        }
        #endregion      

        #region 定义"选择行发生变化"事件

        /// <summary>
        /// 定义"行发生变化"事件
        /// </summary>
        private event EventHandler<DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs> _OnFocusedRowChanged;

        /// <summary>
        /// 定义"行发生变化"事件访问器
        /// </summary>
        [
        Description(@"点击""行发生变化""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs> OnFocusedRowChanged
        {
            add
            {
                _OnFocusedRowChanged += value;
            }
            remove
            {
                _OnFocusedRowChanged -= value;
            }
        }

        /// <summary>
        /// 定义"行发生变化"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void FocusedRowChanged(DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (_OnFocusedRowChanged != null) _OnFocusedRowChanged(this, e);
        }

        #endregion

        #region 定义"选择行发生变化"事件

        /// <summary>
        /// 定义"行的单元格点击"事件
        /// </summary>
        private event EventHandler<RowCellClickEventArgs> _OnRowCellClick;

        /// <summary>
        /// 定义"行的单元格点击"事件访问器
        /// </summary>
        [
        Description(@"点击""行的单元格点击""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<RowCellClickEventArgs> OnRowCellClick
        {
            add
            {
                _OnRowCellClick += value;
            }
            remove
            {
                _OnRowCellClick -= value;
            }
        }

        /// <summary>
        /// 定义"行的单元格点击"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void RowCellClick(RowCellClickEventArgs e)
        {
            if (_OnRowCellClick != null) _OnRowCellClick(this, e);
        }

        #endregion        

        #region 定义"选择列发生变化"事件

        /// <summary>
        /// 定义"行发生变化"事件
        /// </summary>
        private event EventHandler<DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs> _OnFocusedColumnChanged;

        /// <summary>
        /// 定义"行发生变化"事件访问器
        /// </summary>
        [
        Description(@"点击""行发生变化""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs> OnFocusedColumnChanged
        {
            add
            {
                _OnFocusedColumnChanged += value;
            }
            remove
            {
                _OnFocusedColumnChanged -= value;
            }
        }

        /// <summary>
        /// 定义"行发生变化"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void FocusedColumnChanged(DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            if (_OnFocusedColumnChanged != null) _OnFocusedColumnChanged(this, e);
        }

        #endregion

        #region 定义"数据源发生变化"事件

        /// <summary>
        /// 定义"数据源发生变化"事件
        /// </summary>
        private event EventHandler<EventArgs> _OnDataSourceChanged;

        /// <summary>
        /// 定义"数据源发生变化"事件访问器
        /// </summary>
        [
        Description(@"点击""行发生变化""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> OnDataSourceChanged
        {
            add
            {
                _OnDataSourceChanged += value;
            }
            remove
            {
                _OnDataSourceChanged -= value;
            }
        }

        /// <summary>
        /// 定义"数据源发生变化"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void DataSourceChanged(EventArgs e)
        {
            if (_OnDataSourceChanged != null) _OnDataSourceChanged(this, e);
        }

        #endregion

        #region 定义"单元格内容正在发生变化"事件

        /// <summary>
        /// 定义"单元格内容正在发生变化"事件
        /// </summary>
        private event EventHandler<DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs> _OnCellValueChanging;

        /// <summary>
        /// 定义"单元格内容正在发生变化"事件访问器
        /// </summary>
        [
        Description(@"点击""单元格内容正在发生变化""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs> OnCellValueChanging
        {
            add
            {
                _OnCellValueChanging += value;
            }
            remove
            {
                _OnCellValueChanging -= value;
            }
        }

        /// <summary>
        /// 定义"单元格内容正在发生变化"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void CellValueChanging(DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (_OnCellValueChanging != null) _OnCellValueChanging(this, e);
        }

        #endregion                     

        #region 定义"单元格内容已经发生变化"事件

        /// <summary>
        /// 定义"单元格内容已经发生变化"事件
        /// </summary>
        private event EventHandler<DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs> _OnCellValueChanged;

        /// <summary>
        /// 定义"单元格内容已经发生变化"事件访问器
        /// </summary>
        [
        Description(@"点击""单元格内容已经发生变化""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs > OnCellValueChanged
        {
            add
            {
                _OnCellValueChanged += value;
            }
            remove
            {
                _OnCellValueChanged -= value;
            }
        }

        /// <summary>
        /// 定义"单元格内容已经发生变化"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void CellValueChanged(DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs  e)
        {
            if (_OnCellValueChanged != null) _OnCellValueChanged(this, e);
        }

        #endregion        

        #region 定义"刷新"事件

        /// <summary>
        /// 定义"刷新"事件
        /// </summary>
        private event EventHandler<ItemClickEventArgs> _OnRefresh;

        /// <summary>
        /// 定义"刷新"事件访问器
        /// </summary>
        [
        Description(@"点击""导出Excel""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ItemClickEventArgs> OnRefresh
        {
            add
            {
                _OnRefresh += value;
            }
            remove
            {
                _OnRefresh -= value;
            }
        }

        /// <summary>
        /// 定义"刷新"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void Refresh(ItemClickEventArgs e)
        {
            if (_OnRefresh != null) _OnRefresh(this, e);
        }

        #endregion

        #region 定义"导出Excel"事件

        /// <summary>
        /// 定义"导出Excel"事件
        /// </summary>
        private event EventHandler<ItemClickEventArgs> _OnExportExcel;

        /// <summary>
        /// 定义"导出Excel"事件访问器
        /// </summary>
        [
        Description(@"点击""导出Excel""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ItemClickEventArgs> OnExportExcel
        {
            add
            {
                _OnExportExcel += value;
            }
            remove
            {
                _OnExportExcel -= value;
            }
        }

        /// <summary>
        /// 定义"导出Excel"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void ExportExcel(ItemClickEventArgs e)
        {
            if (_OnExportExcel != null) _OnExportExcel(this, e);
        }

        #endregion

        #region 定义"导入 Excel"事件
        /// <summary>
        /// 定义"导入 Excel"事件
        /// </summary>
        private event EventHandler<ItemClickEventArgs> _OnImportExcel;

        /// <summary>
        /// 定义"导入 Excel"事件访问器
        /// </summary>
        [
        Description(@"点击""导入 Excel""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ItemClickEventArgs> OnImportExcel
        {
            add
            {
                _OnImportExcel += value;
            }
            remove
            {
                _OnImportExcel -= value;
            }
        }

        /// <summary>
        /// 定义"导入 Excel"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void ImportExcel(ItemClickEventArgs e)
        {
            if (_OnImportExcel != null) _OnImportExcel(this, e);
        }
        #endregion

        #region 定义"自定义列显示"事件
        /// <summary>
        /// 定义"自定义列显示"事件
        /// </summary>
        private event EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs> _OnCustomColumnDisplayText;

        /// <summary>
        /// 定义"自定义显示列"事件访问器
        /// </summary>
        [
        Description(@"点击""自定义列显示""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs> OnCustomColumnDisplayText
        {
            add
            {
                _OnCustomColumnDisplayText += value;
            }
            remove
            {
                _OnCustomColumnDisplayText -= value;
            }
        }

        /// <summary>
        /// 定义"自定义列显示"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void CustomColumnDisplayText(DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (_OnCustomColumnDisplayText != null) _OnCustomColumnDisplayText(this, e);
        }

        #endregion

        #region 定义"鼠标在表格上移动"事件
        /// <summary>
        /// 定义"鼠标在表格上移动"事件
        /// </summary>
        private event EventHandler<MouseEventArgs> _OnGridMouseMove;

        /// <summary>
        /// 定义"鼠标在表格上移动"事件访问器
        /// </summary>
        [
        Description(@"""鼠标在表格上移动""时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<MouseEventArgs> OnGridMouseMove
        {
            add
            {
                _OnGridMouseMove += value;
            }
            remove
            {
                _OnGridMouseMove -= value;
            }
        }

        /// <summary>
        /// 定义"鼠标在表格上移动"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void GridMouseMove(MouseEventArgs e)
        {
            if (_OnGridMouseMove != null) _OnGridMouseMove(this, e);
        }

        #endregion

        #region 定义"单元格内容样式"事件

        /// <summary>
        /// 定义"单元格内容样式"事件
        /// </summary>
        private event EventHandler<RowCellStyleEventArgs> _RowCellStyle;

        /// <summary>
        /// 定义"单元格内容样式"事件访问器
        /// </summary>
        [
        Description(@"点击""单元格内容样式""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<RowCellStyleEventArgs> RowCellStyle
        {
            add
            {
                _RowCellStyle += value;
            }
            remove
            {
                _RowCellStyle -= value;
            }
        }

        /// <summary>
        /// 定义"单元格内容样式"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void OnRowCellStyle(RowCellStyleEventArgs e)
        {
            if (_RowCellStyle != null) _RowCellStyle(this, e);
        }

        #endregion        

        #region 定义"行内容样式"事件

        /// <summary>
        /// 定义"行内容样式"事件
        /// </summary>
        private event EventHandler<RowStyleEventArgs> _RowStyle;

        /// <summary>
        /// 定义"行内容样式"事件访问器
        /// </summary>
        [
        Description(@"点击""行内容样式""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<RowStyleEventArgs> RowStyle
        {
            add
            {
                _RowStyle += value;
            }
            remove
            {
                _RowStyle -= value;
            }
        }

        /// <summary>
        /// 定义"行内容样式"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void OnRowStyle(RowStyleEventArgs e)
        {
            if (_RowStyle != null) _RowStyle(this, e);
        }

        #endregion

        #endregion

        #region 构造函数

        public DevExpressGrid()
        {
            InitializeComponent();
            
        }

        private void DevExpressGrid_Load(object sender, EventArgs e)
        {
            
        }       

        #endregion

        /// <summary>
        /// 设置右键菜单权限
        /// </summary>
        private void SetAuthority()
        {
            /* 1. 增加 */            
            if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.Add)))
            {
                btnItmAdd.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btnItmAdd.Visibility = BarItemVisibility.Never;
            }
            if (_isMainTable && RowCount > 0)
            {
                btnItmAdd.Enabled = false;
            }
            else
            {
                btnItmAdd.Enabled = true;
            }

            /* 2.编辑 */
            if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.Edit)))
            {
                btnItmEdit.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btnItmEdit.Visibility = BarItemVisibility.Never;
            }
            if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.BatchEdit))
                || AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.CompletelyEdit)))
            {
                barSubItemEdit.Visibility = BarItemVisibility.Always;
                if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.BatchEdit)))
                {
                    btnItmBatchEdit.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    btnItmBatchEdit.Visibility = BarItemVisibility.Never;
                }
                if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.CompletelyEdit)))
                {
                    btnItmCompleteEdit.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    btnItmCompleteEdit.Visibility = BarItemVisibility.Never;
                }
            }
            else
            {
                barSubItemEdit.Visibility = BarItemVisibility.Never;
            }

            /* 3.删除 */
            if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.Delete)))
            {
                btnItmDelete.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btnItmDelete.Visibility = BarItemVisibility.Never;
            }
            if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.BatchDelete))
                || AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.CompletelyDelete)))
            {
                barSubItemDelete.Visibility = BarItemVisibility.Always;
                if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.BatchDelete)))
                {
                    btnItmBatchDelete.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    btnItmBatchDelete.Visibility = BarItemVisibility.Never;
                }
                if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.CompletelyDelete)))
                {
                    btnItmCompleteDelete.Visibility = BarItemVisibility.Always;
                }
                else
                {
                    btnItmCompleteDelete.Visibility = BarItemVisibility.Never;
                }
            }
            else
            {
                barSubItemDelete.Visibility = BarItemVisibility.Never;
            }

            /* 4. 移动记录 */
            if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.Move)))
            {
                barSubItemMove.Visibility = BarItemVisibility.Always;
            }
            else
            {
                barSubItemMove.Visibility = BarItemVisibility.Never;
            }

            /* 记录数大于一条时，移动才操作 */
            if (gridView.FocusedRowHandle >= 0 && _currentPageSize > 1)
            {
                if (barSubItemMove.Visibility == BarItemVisibility.Always)
                {
                    barSubItemMove.Enabled = true;
                }
            }
            else
            {
                if (barSubItemMove.Visibility == BarItemVisibility.Always)
                {
                    barSubItemMove.Enabled = false;
                }
            }

            /* 5. 刷新 */
            if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.Refresh)))
            {
                btnItmExport.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btnItmExport.Visibility = BarItemVisibility.Never;
            }

            /* 6. 导出 Excel */
            if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.Export)))
            {
                btnItmExport.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btnItmExport.Visibility = BarItemVisibility.Never;
            }

            /* 7. 导入 Excel */
            if (AuthorityHelper.CheckAuthority(_authority, Convert.ToByte(GridViewAuthority.Import)))
            {
                btnItmImport.Visibility = BarItemVisibility.Always;
            }
            else
            {
                btnItmImport.Visibility = BarItemVisibility.Never;
            }
        }

        #region 私有方法

        /// <summary>
        /// 显示页面底部信息
        /// </summary>
        private void ShowFooterInfo()
        {
            if (PageSize == 0)
            {
                return;
            }
            int remainder = RecordCount % PageSize;
            if (remainder != 0)
            {
                _pageCount = RecordCount / PageSize + 1;
            }
            else
            {
                _pageCount = RecordCount / PageSize;
            }
            if (CurrentPageIndex < _pageCount - 1)
            {
                _currentPageSize = PageSize;
            }
            else
            {
                //最后一页
                if (remainder != 0)
                {
                    _currentPageSize = remainder;
                }
                else
                {
                    if (CurrentPageIndex < _pageCount)
                    {
                        _currentPageSize = PageSize;
                    }
                    else
                    {
                        _currentPageSize = 0;
                    }
                }
            }
            //panelControlFooter.Visible = false;
            //StringBuilder record = new StringBuilder();
            //record.Append("第 ");
            //record.Append(CurrentPageIndex + 1);
            //record.Append(" 页，当前页记录 ");
            //record.Append(CurrentPageSize);
            //record.Append(" 条。");  
            //record.Append("总记录 ");
            //record.Append(RecordCount);
            //record.Append(" 条，共 ");
            //record.Append(PageCount);
            //record.Append(" 页。");
            if (string.IsNullOrWhiteSpace(FootText))
            {
                lblFooter.Text = string.Format("第 {0} 页，当前页记录 {1} 条。 总记录 {2} 条，共 {3} 页。", CurrentPageIndex + 1, CurrentPageSize, RecordCount, PageCount);
            }
            else
            {
                lblFooter.Text = string.Format(FootText, CurrentPageIndex + 1, CurrentPageSize, RecordCount, PageCount);
            }
            EnableTurnToPage();
            cmbPages.Properties.Items.Clear();
            for (int i = 1; i <= PageCount; i++)
            {
                cmbPages.Properties.Items.Add(new CommonItem<int>(string.Format("第{0}页", i), i));
                cmbPages.SelectedIndex = -1;
            }
            if (CurrentPageSize > 0)
            {
                gridView.SelectRow(0);
            }
        }

        /// <summary>
        /// 切换翻页按钮状态
        /// </summary>
        private void EnableTurnToPage()
        {
            if (_currentPageIndex > 0)
            {
                sbtnFirst.Enabled = true;
                sbtnPrevious.Enabled = true; 
                btnItmFirstPage.Enabled = true;
                btnItmPreviousPage.Enabled = true;                            
               
            }
            else
            {
                sbtnFirst.Enabled = false;
                sbtnPrevious.Enabled = false;
                btnItmFirstPage.Enabled = false;
                btnItmPreviousPage.Enabled = false;    
            }
            if (_currentPageIndex < PageCount - 1)
            {
                sbtnNext.Enabled = true;
                sbtnLast.Enabled = true;
                btnItmNextPage.Enabled = true;   
                btnItmLastPage.Enabled = true;               
            }
            else
            {
                sbtnNext.Enabled = false;
                sbtnLast.Enabled = false;
                btnItmNextPage.Enabled = false;
                btnItmLastPage.Enabled = false;               
            }
            if (sbtnNext.Enabled == false &&
                sbtnLast.Enabled == false &&
                btnItmNextPage.Enabled == false &&
                btnItmLastPage.Enabled == false)
            {
                barSubItemTurnPage.Enabled = false;
            }
            else
            {
                barSubItemTurnPage.Enabled = true;
            }
        }

        /// <summary>
        /// 跳转页
        /// </summary>
        private void JumpPages()
        {
            string eventArgument = cmbPages.Text.Trim();
            if (eventArgument.StartsWith("第"))
            {
                eventArgument = eventArgument.Substring(1, eventArgument.Length - 1).Trim();
            }
            if (eventArgument.EndsWith("页"))
            {
                eventArgument = eventArgument.Substring(0, eventArgument.Length - 1).Trim();
            }
            Regex r = new Regex(@"^[0-9]*[1-9][0-9]*$");
            if (r.IsMatch(eventArgument))
            {
                int index = Int32.Parse(eventArgument) - 1;
                if (index < PageCount)
                {
                    _newPageIndex = index;
                    CustomGridViewPageEventArgs gridViewPageEvent = new CustomGridViewPageEventArgs(NewPageIndex);
                    PageIndexChanged(gridViewPageEvent);
                }
            }
        }

        /// <summary>
        /// 插入 CheckBox 列
        /// </summary>
        private void InsertCheckBoxColumn()
        {
            if (gridView.Columns.ColumnByName(CHECKBOX_COLUMN_NAME) == null)
            {
                RepositoryItemCheckEdit repositoryItemCheckEdit = new RepositoryItemCheckEdit();
                repositoryItemCheckEdit.ReadOnly = false;
                repositoryItemCheckEdit.AutoHeight = false;
                repositoryItemCheckEdit.LookAndFeel.SkinName = "Blue";
                repositoryItemCheckEdit.LookAndFeel.UseDefaultLookAndFeel = false;
                repositoryItemCheckEdit.Name = "repositoryItemCheckEdit";
                repositoryItemCheckEdit.EditValueChanged += new EventHandler(repositoryItemCheckEdit_EditValueChanged);
                GridColumn checkboxColumn = gridView.Columns.Insert(0);
             
                checkboxColumn.Name = CHECKBOX_COLUMN_NAME;
                checkboxColumn.Width = 60;
                checkboxColumn.MaxWidth = 60;
                checkboxColumn.VisibleIndex = 0;
                checkboxColumn.Visible = true;
                checkboxColumn.OptionsColumn.AllowEdit = true;
                checkboxColumn.OptionsColumn.ReadOnly = false;
                checkboxColumn.UnboundType = DevExpress.Data.UnboundColumnType.Boolean;
                checkboxColumn.ColumnEdit = repositoryItemCheckEdit;
                if (string.IsNullOrEmpty(_checkboxColumnCaption))
                {
                    _checkboxColumnCaption = CHECKBOX_COLUMN_CAPTION_NAME;
                }
                checkboxColumn.Caption = _checkboxColumnCaption;
                checkboxColumn.Fixed = FixedStyle.Left;                
            }
        }

        /// <summary>
        /// 插入查看列
        /// </summary>
        /// <param name="visibleIndex"></param>
        private void InsertViewColumn(int visibleIndex)
        {
            GridColumn gcView = gridView.Columns.Insert(visibleIndex);
            gcView.Caption = "查看";
            gcView.Name = "View";
            gcView.Width = 40;
            gcView.MinWidth = 40;
            gcView.VisibleIndex = visibleIndex;
            gcView.Visible = true;
            gcView.Fixed = FixedStyle.Left;
            gcView.OptionsColumn.ReadOnly = true;
            gcView.OptionsFilter.AllowAutoFilter = false;
            gcView.OptionsFilter.AllowFilter = false;
            gcView.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gcView.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gcView.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            gcView.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit = new RepositoryItemHyperLinkEdit();
            repositoryItemHyperLinkEdit.NullText = "查看";           
            gcView.ColumnEdit = repositoryItemHyperLinkEdit;
            repositoryItemHyperLinkEdit.SingleClick = true;
            repositoryItemHyperLinkEdit.Click += (sender, e) =>
            {
                RowEvent rowEvent = GetRowEvent();
                RowView(rowEvent);
            };
        }

        /// <summary>
        /// 插入编辑列
        /// </summary>
        /// <returns></returns>
        private void InsertEditedColumn(int visibleIndex)
        {
            GridColumn gcEdit = gridView.Columns.Insert(visibleIndex);
            gcEdit.Caption = "编辑";
            gcEdit.Name = EDIT_COLUMN_NAME;
            gcEdit.Width = 40;
            gcEdit.MinWidth = 40;
            gcEdit.VisibleIndex = visibleIndex;
            gcEdit.Visible = true;
            gcEdit.Fixed = FixedStyle.Left;
            gcEdit.OptionsColumn.ReadOnly = true;
            gcEdit.OptionsFilter.AllowAutoFilter = false;
            gcEdit.OptionsFilter.AllowFilter = false;
            gcEdit.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            gcEdit.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            gcEdit.AppearanceHeader.TextOptions.VAlignment = VertAlignment.Center;
            gcEdit.AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            RepositoryItemHyperLinkEdit repositoryItemHyperLinkEdit = new RepositoryItemHyperLinkEdit();
            repositoryItemHyperLinkEdit.NullText = "编辑";
            gcEdit.ColumnEdit = repositoryItemHyperLinkEdit;
            repositoryItemHyperLinkEdit.SingleClick = true;
            repositoryItemHyperLinkEdit.Click += (sender, e) =>
            {
                RowEvent rowEvent = GetRowEvent();
                RowEdit(rowEvent);
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void repositoryItemCheckEdit_EditValueChanged(object sender, EventArgs e)
        {
            if (_multiSelectedValues != null)
            {
                _multiSelectedValues.Clear();
                _multiSelectedValues = null;
            }
            gridView.CloseEditor();
        }

        /// <summary>
        /// 移除 CheckBox 列
        /// </summary>
        private void RemoveCheckBoxColumn()
        {
            GridColumn checkboxColumn = gridView.Columns.ColumnByName(CHECKBOX_COLUMN_NAME);
            if (checkboxColumn != null)            
            {
                gridView.Columns.Remove(checkboxColumn);
            }
        }

        #endregion

        /// <summary>
        /// 清除多选
        /// </summary>
        public void ClearMultiSelectedCheckbox()
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                bool check = Convert.ToBoolean(gridView.GetRowCellValue(i, CHECKBOX_COLUMN_NAME));
                if (check)
                {
                    gridView.SetRowCellValue(i, CHECKBOX_COLUMN_NAME, false);
                }
            } 
        }

        /// <summary>
        /// 刷新指定的行
        /// </summary>
        /// <param name="rowHandle"></param>
        public void RefreshRow(int rowHandle)
        {
            gridView.RefreshRow(rowHandle);
        }

        public void RefreshData()
        {
            gridView.RefreshData();
        }

        /// <summary>
        /// 获得行的选择项的值
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <returns></returns>
        public bool GetCheckBoxValue(int rowHandle)
        {
            return Convert.ToBoolean(gridView.GetRowCellValue(rowHandle, CHECKBOX_COLUMN_NAME));
        }

        /// <summary>
        /// 获得单元格的值
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <returns></returns>
        public object GetRowCellValue(int rowHandle, string fieldName)
        {
            return gridView.GetRowCellValue(rowHandle, fieldName);
        }

        /// <summary>
        /// 设置单元格的值
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetRowCellValue(int rowHandle, string fieldName, object value)
        {
            gridView.SetRowCellValue(rowHandle, fieldName, value);
        }

        /// <summary>
        /// 设置焦点单元格的值
        /// </summary>
        /// <param name="fieldName"></param>
        /// <param name="value"></param>
        public void SetFocusedRowCellValue(string fieldName, object value)
        {
            gridView.SetFocusedRowCellValue(fieldName, value);       
        }

        private void cmbPages_SelectedIndexChanged(object sender, EventArgs e)
        {
            JumpPages();
        }

        private void cmbPages_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                JumpPages();
            }        
        }

        /// <summary>
        /// devexpress gridcontrol上显示行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 未绑定行事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_CustomUnboundColumnData(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDataEventArgs e)
        {            
            if (e.Column.Name.Equals(CHECKBOX_COLUMN_NAME))
            {
                if (e.IsGetData)
                {
                    if(checkBoxUnBoundData.Contains(e.ListSourceRowIndex))
                    {
                        e.Value = true;
                    }                    
                    else
                    {
                        e.Value = false;
                    }
                }
                else
                {
                    
                    if (Convert.ToBoolean(e.Value))
                    {
                        if (!checkBoxUnBoundData.Contains(e.ListSourceRowIndex))
                        {
                            checkBoxUnBoundData.Add(e.ListSourceRowIndex);
                        }
                    }
                    else
                    {
                        if (checkBoxUnBoundData.Contains(e.ListSourceRowIndex))
                        {
                            checkBoxUnBoundData.Remove(e.ListSourceRowIndex);
                        }
                    }
                }
            }

        }

        /// <summary>
        /// 行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_RowClick(object sender, RowClickEventArgs e)
        {
            if (((e.Button & MouseButtons.Left) != 0) && FocusedDataRow != null)
            {
                SetAuthority();
                RowEvent rowEvent = GetRowEvent();
                RowClick(rowEvent);
            }
        }

        /// <summary>
        /// 行双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if(FocusedDataRow != null)
            {
                RowEvent rowEvent = GetRowEvent();
                RowDoubleClick(rowEvent);
            }
        }

        /// <summary>
        /// 行单元格变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {            
            OnRowCellStyle(e);
        }

        private void gridView_RowStyle(object sender, RowStyleEventArgs e)
        {
            OnRowStyle(e);
        }

        private RowEvent GetRowEvent()
        {
            RowEvent rowEvent = null;
            if (FocusedDataRow != null)
            {
                OrderedDictionary orderedDictionary = new OrderedDictionary(DataKeyNames.Length);                
                foreach (string dataKeyName in DataKeyNames)
                {
                    if (FocusedDataRow.Table.Columns.Contains(dataKeyName))
                    {
                        orderedDictionary.Add(dataKeyName, FocusedDataRow[dataKeyName]);
                    }
                }
                rowEvent = new RowEvent(gridView.FocusedRowHandle, orderedDictionary);
            }
            return rowEvent;
        }        

        private void gridView_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
                popupMenu.ShowPopup(Control.MousePosition);
        }

        /// <summary>
        /// 在表格上移动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_MouseMove(object sender, MouseEventArgs e)
        {
            GridMouseMove(e);
        }

        /// <summary>
        /// 右键弹出菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupMenu_BeforePopup(object sender, CancelEventArgs e)
        {            
            BeforePopupMenu(e);
            SetAuthority();
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnFirst_Click(object sender, EventArgs e)
        {
            TrunToFirstPage();
        }

        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnPrevious_Click(object sender, EventArgs e)
        {
            TrunToPreviousPage();
        }

        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnNext_Click(object sender, EventArgs e)
        {
            TrunToNextPage();
        }

        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnLast_Click(object sender, EventArgs e)
        {
            TrunToLastPage();
        }

        private void TrunToFirstPage()
        {
            _newPageIndex = 0;
            CustomGridViewPageEventArgs gridViewPageEvent = new CustomGridViewPageEventArgs(NewPageIndex);
            PageIndexChanged(gridViewPageEvent);
        }

        private void TrunToPreviousPage()
        {
            _newPageIndex = _currentPageIndex - 1;
            CustomGridViewPageEventArgs gridViewPageEvent = new CustomGridViewPageEventArgs(NewPageIndex);
            PageIndexChanged(gridViewPageEvent);
        }

        private void TrunToNextPage()
        {
            _newPageIndex = _currentPageIndex + 1;
            CustomGridViewPageEventArgs gridViewPageEvent = new CustomGridViewPageEventArgs(NewPageIndex);
            PageIndexChanged(gridViewPageEvent);
        }

        private void TrunToLastPage()
        {
            _newPageIndex = _pageCount - 1;
            CustomGridViewPageEventArgs gridViewPageEvent = new CustomGridViewPageEventArgs(NewPageIndex);
            PageIndexChanged(gridViewPageEvent);
        }

        #region 私有方法

        /// <summary>
        /// 获得审核状态列
        /// </summary>
        /// <returns></returns>
        private RepositoryItemImageComboBox GetRepositoryItemImageComboBox()
        {
            RepositoryItemImageComboBox repositoryItemImageComboBox = new RepositoryItemImageComboBox();
            repositoryItemImageComboBox.AutoHeight = false;
            repositoryItemImageComboBox.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.DropDown)});
            repositoryItemImageComboBox.LookAndFeel.SkinName = "Money Twins";
            repositoryItemImageComboBox.LookAndFeel.UseDefaultLookAndFeel = false;
            repositoryItemImageComboBox.Name = "repositoryItemImageComboBox";
            repositoryItemImageComboBox.SmallImages = this.icStateItems;
            UserControlHelper.InitRepositoryItemImageComboBox(repositoryItemImageComboBox, typeof(AuditedStatus));

            return repositoryItemImageComboBox;
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmSelectAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.SetRowCellValue(i, CHECKBOX_COLUMN_NAME, true);
            }
        }

        /// <summary>
        /// 反选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmReverse_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                bool check = !Convert.ToBoolean(gridView.GetRowCellValue(i, CHECKBOX_COLUMN_NAME));
                gridView.SetRowCellValue(i, CHECKBOX_COLUMN_NAME, check);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmCancel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (int i = 0; i < gridView.RowCount; i++)
            {
                gridView.SetRowCellValue(i, CHECKBOX_COLUMN_NAME, false);
            }
        }

        private void btnItmAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddClick(e);
        }

        private void btnItmEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            EditClick(e);
        }

        private void btnItmBatchEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            BatchEditClick(e);
        }

        private void btnItmCompleteEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            CompleteEditClick(e);
        }

        private void btnItmDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteClick(e);
        }

        private void btnItmBatchDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            BatchDeleteClick(e);          
        }

        private void btnItmCompleteDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            CompleteDeleteClick(e);
        }       

        private void btnItmTopRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            RecordSortingChanged(new ExtendedItemClickEventArgs(e, MovedDriection.Top));
        }

        private void btnItmPreviousRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            RecordSortingChanged(new ExtendedItemClickEventArgs(e, MovedDriection.Previous));
        }

        private void btnItmNextRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            RecordSortingChanged(new ExtendedItemClickEventArgs(e, MovedDriection.Next));
        }

        private void btnItmBottomRecord_ItemClick(object sender, ItemClickEventArgs e)
        {
            RecordSortingChanged(new ExtendedItemClickEventArgs(e, MovedDriection.Bottom));
        }

        private void btnItmFirstPage_ItemClick(object sender, ItemClickEventArgs e)
        {
            TrunToFirstPage();
        }

        private void btnItmPreviousPage_ItemClick(object sender, ItemClickEventArgs e)
        {
            TrunToPreviousPage();
        }

        private void btnItmNextPage_ItemClick(object sender, ItemClickEventArgs e)
        {
            TrunToNextPage();        
        }

        private void btnItmLastPage_ItemClick(object sender, ItemClickEventArgs e)
        {
            TrunToLastPage();
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            FocusedRowChanged(e);
        }

        private void gridControl_DataSourceChanged(object sender, EventArgs e)
        {
            DataSourceChanged(e);
        }

        private void gridView_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name.Equals(CHECKBOX_COLUMN_NAME))
            {
                return;
            }
            CellValueChanging(e);
        }
        private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.Name.Equals(CHECKBOX_COLUMN_NAME))
            {
                return;
            }
            CellValueChanged(e);
        }

        private void gridView_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            FocusedColumnChanged(e);
        }

        private void gridView_RowCellClick(object sender, RowCellClickEventArgs e)
        {
            RowCellClick(e);
        }

        private void gridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(gridView.GetFocusedDisplayText());
                e.Handled = true;
            }
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            Refresh(e);
        }

        /// <summary>
        /// 导出 Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            ExportExcel(e);
        }

        /// <summary>
        /// 导入Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            ImportExcel(e);
        }

        /// <summary>
        /// 自定义显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            CustomColumnDisplayText(e);
        }

        public void SetMenuButtons(bool enabled)
        {
            if (btnItmEdit.Visibility == BarItemVisibility.Always)
            {
                btnItmEdit.Enabled = enabled;
            }
            if (barSubItemEdit.Visibility == BarItemVisibility.Always)
            {
                barSubItemEdit.Enabled = enabled;
            }
            if (btnItmDelete.Visibility == BarItemVisibility.Always)
            {
                btnItmDelete.Enabled = enabled;
            }
            if (barSubItemDelete.Visibility == BarItemVisibility.Always)
            {
                barSubItemDelete.Enabled = enabled;
            }
        }

        #endregion
    }
}
