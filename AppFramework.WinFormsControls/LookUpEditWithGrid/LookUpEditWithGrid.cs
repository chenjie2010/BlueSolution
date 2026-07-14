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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;
using DevExpress.Data;

namespace AppFramework.WinFormsControls
{
    public partial class LookUpEditWithGrid : UserControl
    {
        #region 定义成员变量 

        /// <summary>
        /// 关键字列
        /// </summary>
        private string[] _dataKeyNames;

        /// <summary>
        /// 分组名称集合
        /// </summary>
        private string[] _groupKeyNames;

        #endregion

        #region 属性

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
        Description("分组名称集合"),
        Category("自定义杂项"),
         DefaultValue((string)null),
         TypeConverter(typeof(StringArrayConverter))]
        public string[] GroupKeyNames
        {
            get
            {
                object obj2 = _groupKeyNames;
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
                    _groupKeyNames = (string[])value.Clone();
                }
                else
                {
                    _groupKeyNames = null;
                }
            }
        }

        /// <summary>
        /// 值字段名称
        /// </summary>
        [
       Description("值字段名称"),
       Category("自定义杂项"),
       Browsable(true),
       ]
        public string ValueMember
        {
            get;
            set;
        }

        /// <summary>
        /// 列自适应宽度
        /// </summary>
        [
        Description("列自适应宽度"),
        Category("自定义杂项"),
        Browsable(true),
        DefaultValue(false),
        ]
        public bool ColumnAutoWidth
        {
            get

            {
                return gridView.OptionsView.ColumnAutoWidth;
            }
            set
            {
                gridView.OptionsView.ColumnAutoWidth = value;
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
                gridControl.DataSource = value;
                if (value != null)
                {
                    gridView.PopulateColumns();
                    if (!ColumnAutoWidth)
                    {
                        gridView.BestFitColumns();
                    }
                    foreach (string dataKeyName in DataKeyNames)
                    {
                        if (gridView.Columns[dataKeyName] != null)
                        {
                            gridView.Columns[dataKeyName].Visible = false;
                        }
                    }
                    if (gridView.Columns[SortedKeyName] != null)
                    {
                        gridView.Columns[SortedKeyName].Visible = false;
                    }
                    for (int i = 0; i < gridView.Columns.Count; i++)
                    {
                        gridView.Columns[i].OptionsFilter.AllowAutoFilter = false;
                        gridView.Columns[i].OptionsFilter.AllowFilter = false;
                        gridView.Columns[i].AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                        gridView.Columns[i].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                        gridView.Columns[i].OptionsColumn.AllowSort = DefaultBoolean.False;
                    }
                    if (GroupKeyNames.Length > 0)
                    {
                        try
                        {
                            //gridView.BeginUpdate();
                            //gridView.ClearSorting();
                            for (int i = 0; i < GroupKeyNames.Length; i++)
                            {
                                gridView.Columns[GroupKeyNames[i]].SortMode = ColumnSortMode.Custom;
                                gridView.Columns[GroupKeyNames[i]].GroupIndex = i;
                            }
                            if (!string.IsNullOrWhiteSpace(SortedKeyName) && gridView.Columns.ColumnByFieldName(SortedKeyName) != null)
                            {                                
                                gridView.Columns[SortedKeyName].SortMode = ColumnSortMode.Value;
                                gridView.Columns[SortedKeyName].SortOrder = ColumnSortOrder.Ascending;
                            }
                        }
                        finally
                        {                           
                            //gridView.EndDataUpdate();
                        }
                    }                    
                }
            }
        }

        /// <summary>
        /// 排序关键字
        /// </summary>
        [
       Description("排序关键字"),
       Category("自定义杂项"),
       DefaultValue(false),
       ]
        public string SortedKeyName
        {
            get;
            set;
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        [
        Description("是否只读"),
        Category("自定义杂项"),
        DefaultValue(false),
        ]
        public bool ReadOnly
        {
            set
            {
                popupContainerEdit.Properties.ReadOnly = value;
            }
            get
            {
                return popupContainerEdit.Properties.ReadOnly;
            }
        }

        /// <summary>
        /// 是否显示搜索
        /// </summary>
        [
        Description("是否显示搜索"),
        Category("自定义杂项"),
        DefaultValue(false),
        ]
        public bool ShowSearch
        {
            set
            {
                gridView.OptionsFind.AlwaysVisible = value;
            }
            get
            {
                return gridView.OptionsFind.AlwaysVisible;
            }
        }

        [
        Description("文本"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public override string Text
        {
            set
            {
                popupContainerEdit.Text = value;
            }
            get
            {
                return popupContainerEdit.Text;
            }
        }

        [
        Description("编辑框值"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public object EditValue
        {
            set
            {
                popupContainerEdit.EditValue = value;
            }
            get
            {
                return popupContainerEdit.EditValue;
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
                return gridView.RowCount; ;
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

        #endregion

        #region 定义事件

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

        #region 定义"行双点击"事件
        /// <summary>
        /// 定义"行双点击"事件
        /// </summary>
        private event EventHandler<RowEvent> _OnRowDoubleClick;

        /// <summary>
        /// 定义"行双点击"事件访问器
        /// </summary>
        [
        Description(@"点击""行双点击""按钮时发生"),
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
        /// 定义"行双点击"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void RowDoubleClick(RowEvent e)
        {
            if (_OnRowDoubleClick != null) _OnRowDoubleClick(this, e);
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

        #region 定义"表格关闭"事件

        private event EventHandler<ClosedEventArgs> _OnEditClosed;

        /// <summary>
        /// 定义"表格关闭"事件访问器
        /// </summary>
        [
        Description("表格关闭"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ClosedEventArgs> OnEditClosed
        {
            add
            {
                _OnEditClosed += value;
            }
            remove
            {
                _OnEditClosed -= value;
            }
        }

        /// <summary>
        /// 定义"表格关闭"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void EditClosed(ClosedEventArgs e)
        {
            if (_OnEditClosed != null) _OnEditClosed(this, e);
        }

        #endregion

        #region 定义"弹出表格"事件

        private event EventHandler<EventArgs> _OnGridPopup;

        /// <summary>
        /// 定义"弹出表格"事件访问器
        /// </summary>
        [
        Description("弹出表格"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> OnGridPopup
        {
            add
            {
                _OnGridPopup += value;
            }
            remove
            {
                _OnGridPopup -= value;
            }
        }

        /// <summary>
        /// 定义"弹出表格"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void GridPopup(EventArgs e)
        {
            if (_OnGridPopup != null) _OnGridPopup(this, e);
        }

        #endregion

        #region 定义"表格弹出前"事件

        private event EventHandler<EventArgs> _OnGridBeforePopup;

        /// <summary>
        /// 定义"下拉树形结构关闭"事件访问器
        /// </summary>
        [
        Description("表格弹出前"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> OnGridBeforePopup
        {
            add
            {
                _OnGridBeforePopup += value;
            }
            remove
            {
                _OnGridBeforePopup -= value;
            }
        }

        /// <summary>
        /// 定义"表格弹出前"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void GridBeforePopup(EventArgs e)
        {
            if (_OnGridBeforePopup != null) _OnGridBeforePopup(this, e);
        }

        #endregion

        #region 定义"值发生变化"事件

        /// <summary>
        /// 定义"值发生变化"事件
        /// </summary>
        private event EventHandler<EventArgs> _EditValueChanged;

        /// <summary>
        /// 定义"值发生变化"事件访问器
        /// </summary>
        [
        Description(@"点击""值发生变化""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> EditValueChanged
        {
            add
            {
                _EditValueChanged += value;
            }
            remove
            {
                _EditValueChanged -= value;
            }
        }

        /// <summary>
        /// 定义"值发生变化"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void OnEditValueChanged(EventArgs e)
        {
            if (_EditValueChanged != null) _EditValueChanged(this, e);
        }

        #endregion

        #region 定义"值清空"事件

        /// <summary>
        /// 定义"值清空"事件
        /// </summary>
        private event EventHandler<EventArgs> _EditValueCleaned;

        /// <summary>
        /// 定义"值清空"事件访问器
        /// </summary>
        [
        Description(@"点击""值清空""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> EditValueCleaned
        {
            add
            {
                _EditValueCleaned += value;
            }
            remove
            {
                _EditValueCleaned -= value;
            }
        }

        /// <summary>
        /// 定义"值清空"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void OnEditValueCleaned(EventArgs e)
        {
            if (_EditValueCleaned != null) _EditValueCleaned(this, e);
        }

        #endregion

        #endregion

        #region 构造函数

        public LookUpEditWithGrid()
        {
            InitializeComponent();
            ShowSearch = false;
            ColumnAutoWidth = false;
        }

        #endregion

        #region 控件方法
        
        /// <summary>
        /// 加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LookUpEditWithGrid_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {           
                gridControl.Width = popupContainerEdit.Width = popupContainerControl.Width = Convert.ToInt32(Math.Floor(Width * 1.5));
            }
        }

        /// <summary>
        /// 行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (((e.Button & MouseButtons.Left) != 0) && FocusedDataRow != null)
            {                
                //if (FocusedDataRow.Table.Columns.Contains(ValueMember))
                //{
                //    EditValue = FocusedDataRow[ValueMember];
                //}
                //else
                //{
                //    EditValue = null;
                //}
                RowEvent rowEvent = GetRowEvent();
                RowClick(rowEvent);
            }
        }

        /// <summary>
        /// 聚焦行发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            //if (FocusedDataRow != null)
            //{
            //    if (FocusedDataRow.Table.Columns.Contains(ValueMember))
            //    {
            //        EditValue = FocusedDataRow[ValueMember];
            //    }
            //    else
            //    {
            //        EditValue = null;
            //    }
            //}
            RowEvent rowEvent = GetRowEvent();
            FocusedRowChanged(e);
        }

        /// <summary>
        /// 双击关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (FocusedDataRow != null)
            {
                if (FocusedDataRow.Table.Columns.Contains(ValueMember))
                {
                    EditValue = FocusedDataRow[ValueMember];
                }
                else
                {
                    EditValue = null;
                }
                RowEvent rowEvent = GetRowEvent();
                RowDoubleClick(rowEvent);
            }
            if (popupContainerControl.OwnerEdit != null)
            {
                popupContainerControl.OwnerEdit.ClosePopup();
            }
        }

        /// <summary>
        /// 失去焦点关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_Leave(object sender, EventArgs e)
        {
            if (popupContainerControl.OwnerEdit != null)
            {
                popupContainerControl.OwnerEdit.ClosePopup();
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_Closed(object sender, ClosedEventArgs e)
        {
            EditClosed(e);
        }

        /// <summary>
        /// 表格弹出事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_Popup(object sender, EventArgs e)
        {
            GridPopup(e);
        }

        /// <summary>
        /// 表格弹出前事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_BeforePopup(object sender, EventArgs e)
        {
            GridBeforePopup(e);
        }

        /// <summary>
        /// 值变化事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_EditValueChanged(object sender, EventArgs e)
        {
            OnEditValueChanged(e);
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkClear_Click(object sender, EventArgs e)
        {
            popupContainerEdit.EditValue = null;
            OnEditValueCleaned(e);
        }

        /// <summary>
        /// 自定义排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_CustomColumnSort(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnSortEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(SortedKeyName) && (GroupKeyNames.FindIndex(key => key.Equals(e.Column.FieldName)) >= 0))
            {
                int a = Convert.ToInt32(gridView.GetListSourceRowCellValue(e.ListSourceRowIndex1, SortedKeyName));
                int b = Convert.ToInt32(gridView.GetListSourceRowCellValue(e.ListSourceRowIndex2, SortedKeyName));
                e.Handled = true;
                e.Result = Comparer.Default.Compare(a, b);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获得行事件对象
        /// </summary>
        /// <returns></returns>
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

        #endregion

    }
}
