using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Design;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraBars;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Utils;

namespace AppFramework.WinFormsControls
{
    public partial class SearchLookUpEditWithGrid : UserControl
    {
        #region 定义私有变量 
        

        #endregion

        #region 定义成员变量 

        /// <summary>
        /// 关键字列
        /// </summary>
        private string[] _dataKeyNames;

        private int _currentPageSize = 0;
        private int _pageCount = 0;
        private int _newPageIndex = 0;
        private int _currentPageIndex = 0;
        private int _pageSize = 50;
        private int _recordCount = 0;

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

        /// <summary>
        /// 值字段名称
        /// </summary>
        public string ValueMember
        {
            get;
            set;
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
                    if (!gridView.OptionsView.ColumnAutoWidth)
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
                    for (int i = 0; i < gridView.Columns.Count; i++)
                    {
                        gridView.Columns[i].OptionsFilter.AllowAutoFilter = false;
                        gridView.Columns[i].OptionsFilter.AllowFilter = false;
                        gridView.Columns[i].AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gridView.Columns[i].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
                        gridView.Columns[i].OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
                    }
                }
            }
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
        /// 文本框样式
        /// </summary>
        [
        Description("文本框样式"),
        Category("自定义杂项"),
        DefaultValue(TextEditStyles.Standard),
        ]
        public TextEditStyles TextEditStyle
        {
            get
            {
                return popupContainerEdit.Properties.TextEditStyle;
            }
            set
            {
                popupContainerEdit.Properties.TextEditStyle = value;                             
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
        /// GridView 每页显示的记录数
        /// </summary>
        [
        Description("每页显示的记录数"),
        Category("自定义杂项"),
        DefaultValue("50"),
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

        #region 定义"搜索"事件

        /// <summary>
        /// 定义"搜索"事件
        /// </summary>
        private event EventHandler<StringEventArgs> _ValueSearched;

        /// <summary>
        /// 定义"搜索"事件访问器
        /// </summary>
        [
        Description(@"点击""搜索""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<StringEventArgs> ValueSearched
        {
            add
            {
                _ValueSearched += value;
            }
            remove
            {
                _ValueSearched -= value;
            }
        }

        /// <summary>
        /// 定义"搜索"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void OnValueSearched(StringEventArgs e)
        {
            if (_ValueSearched != null) _ValueSearched(this, e);
        }

        #endregion


        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public SearchLookUpEditWithGrid()
        {
            InitializeComponent();
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 行点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (((e.Button & MouseButtons.Left) != 0) && FocusedDataRow != null)
            {
                if (FocusedDataRow != null && FocusedDataRow.Table.Columns.Contains(ValueMember))
                {
                    EditValue = FocusedDataRow[ValueMember];
                }
                else
                {
                    EditValue = null;
                }
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
            RowEvent rowEvent = GetRowEvent();
            FocusedRowChanged(e);
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
        /// 按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dataNavigator_ButtonClick(object sender, NavigatorButtonClickEventArgs e)
        {
            switch (e.Button.ButtonType)
            {
                case NavigatorButtonType.First:
                    TrunToFirstPage();
                    break;

                case NavigatorButtonType.PrevPage:
                    TrunToPrevPage();
                    break;

                case NavigatorButtonType.NextPage:
                    TrunToNextPage();
                    break;

                case NavigatorButtonType.Last:
                    TrunToLastPage();
                    break;
            }
        }
        
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchControl_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            Serach();
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void searchControl_EditValueChanged(object sender, EventArgs e)
        {
            Serach();
        }

        /// <summary>
        /// 行号
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
        /// 双击隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_DoubleClick(object sender, EventArgs e)
        {
            if (popupContainerControl.OwnerEdit != null)
            {
                popupContainerControl.OwnerEdit.ClosePopup();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 搜索
        /// </summary>
        private void Serach()
        {
            string content = searchControl.Text.Trim();
            StringEventArgs stringEventArgs = new StringEventArgs(content);
            OnValueSearched(stringEventArgs);
        }

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
            dataNavigator.TextStringFormat = string.Format("第{0}页，当前页{1}条。 共{2}条，{3}页。", CurrentPageIndex + 1, CurrentPageSize, RecordCount, PageCount);
            EnableTurnToPage();            
        }

        /// <summary>
        /// 切换翻页按钮状态
        /// </summary>
        private void EnableTurnToPage()
        {
            if (_currentPageIndex > 0)
            {                
                dataNavigator.Buttons.First.Enabled = true;
                dataNavigator.Buttons.PrevPage.Enabled = true;
            }
            else
            {
                dataNavigator.Buttons.First.Enabled = false;
                dataNavigator.Buttons.PrevPage.Enabled = false;
            }
            if (_currentPageIndex < PageCount - 1)
            {
                dataNavigator.Buttons.NextPage.Enabled = true;
                dataNavigator.Buttons.Last.Enabled = true;
            }
            else
            {
                dataNavigator.Buttons.NextPage.Enabled = false;
                dataNavigator.Buttons.Last.Enabled = false;
            }
        }
        
        /// <summary>
        /// 首页
        /// </summary>
        private void TrunToFirstPage()
        {
            _newPageIndex = 0;
            CustomGridViewPageEventArgs gridViewPageEvent = new CustomGridViewPageEventArgs(NewPageIndex);
            PageIndexChanged(gridViewPageEvent);
        }

        /// <summary>
        /// 前一页
        /// </summary>
        private void TrunToPrevPage()
        {
            _newPageIndex = _currentPageIndex - 1;
            CustomGridViewPageEventArgs gridViewPageEvent = new CustomGridViewPageEventArgs(NewPageIndex);
            PageIndexChanged(gridViewPageEvent);
        }

        /// <summary>
        /// 下一页
        /// </summary>
        private void TrunToNextPage()
        {
            _newPageIndex = _currentPageIndex + 1;
            CustomGridViewPageEventArgs gridViewPageEvent = new CustomGridViewPageEventArgs(NewPageIndex);
            PageIndexChanged(gridViewPageEvent);
        }

        /// <summary>
        /// 尾页
        /// </summary>
        private void TrunToLastPage()
        {
            _newPageIndex = _pageCount - 1;
            CustomGridViewPageEventArgs gridViewPageEvent = new CustomGridViewPageEventArgs(NewPageIndex);
            PageIndexChanged(gridViewPageEvent);
        }

        #endregion
        
    }
}
