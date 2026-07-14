//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: BindingArrayList.cs
// 描述: BindingArrayList 绑定列表类
// 作者：ChenJie 
// 编写日期：2011-8-5
// Copyright 2011
//-----------------------------------------------------------------------------------------
using System;
using System.Reflection;
using System.Collections;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 绑定列表类
    /// </summary>
    [Serializable]
    public class BindingArrayList : ArrayList, IBindingList, ITypedList, IComparer
    {
        #region 定义成员变量
        private string _name = string.Empty;
        private Type _itemType = null;
        private ConstructorInfo _ctorInfo = null;
        private PropertyDescriptorCollection _pdc = null;

        private bool _allowNew = true;
        private bool _allowEdit = true;
        private bool _allowRemove = true;
        private bool _allowSort = true;
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BindingArrayList()
        {
 
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">绑定列表名称</param>
        /// <param name="itemType">绑定列表类型</param>
        public BindingArrayList(string name, Type itemType)
        {

            _name = name;
            _itemType = itemType;
            _ctorInfo = itemType.GetConstructor(new Type[0]);
            _pdc = new PropertyDescriptorCollection(null);
            foreach (PropertyDescriptor pd in TypeDescriptor.GetProperties(itemType))
            {
                if (!pd.Attributes.Contains(BrowsableAttribute.No))
                {
                    _pdc.Add(new BindingArrayListPropertyDescriptor(this, pd));
                }
            }
        }
        #endregion

        #region 属性
        /// <summary>
        /// 允许排序
        /// </summary>
        public bool AllowSort
        {
            get { return _allowSort; }
            set { _allowSort = value; }
        }

        /// <summary>
        /// 允许移除
        /// </summary>
        public bool AllowRemove
        {
            get { return _allowRemove; }
            set { _allowRemove = value; }
        }
        #endregion

        #region 通知机制
        /// <summary>
        /// 列表变化
        /// </summary>
        /// <param name="change"></param>
        protected void OnListChanged(ListChangedType change)
        {
            OnListChanged(change, -1);
        }

        /// <summary>
        /// 列表变化
        /// </summary>
        /// <param name="change"></param>
        /// <param name="index"></param>
        protected void OnListChanged(ListChangedType change, int index)
        {
            if (ListChanged != null)
            {
                ListChangedEventArgs e = new ListChangedEventArgs(change, index, -1);
                ListChanged(this, e);
            }
        }

        /// <summary>
        /// 列表项目变化
        /// </summary>
        /// <param name="item"></param>
        protected void OnItemChanged(object item)
        {
            if (item == null)
            {
                OnListChanged(ListChangedType.Reset);
                return;
            }
            int index = IndexOf(item);
            if (index > -1)
                OnListChanged(ListChangedType.ItemChanged, index);
        }
        #endregion

        #region 接口实现方法
        #region IBindingList 实现
        /// <summary>
        /// 通知事件
        /// </summary>
        public event ListChangedEventHandler ListChanged;
        bool IBindingList.SupportsChangeNotification { get { return true; } }

        private PropertyDescriptor _sortProperty;
        private ListSortDirection _sortDirection;

        /// <summary>
        /// 是否支持排序
        /// </summary>
        public bool SupportsSorting
        {
            get { return _allowSort; }
            set { _allowSort = value; }
        }

        /// <summary>
        /// 移除排序
        /// </summary>
        void IBindingList.RemoveSort()
        {
            if (_sortProperty != null)
            {
                _sortProperty = null;
                OnListChanged(ListChangedType.Reset);
            }
        }

        /// <summary>
        /// 排序属性
        /// </summary>
        PropertyDescriptor IBindingList.SortProperty
        {
            get { return _sortProperty; }
        }

        /// <summary>
        /// 排序方向
        /// </summary>
        ListSortDirection IBindingList.SortDirection
        {
            get { return _sortDirection; }
        }
        bool IBindingList.IsSorted
        {
            get { return _sortProperty != null; }
        }
        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            _sortProperty = property;
            _sortDirection = direction;
            if (_sortProperty != null)
                Sort((IComparer)this);
        }

        /// <summary>
        /// 是否允许增加
        /// </summary>
        public bool AllowNew
        {
            get { return _allowNew && _ctorInfo != null; }
            set { _allowNew = value; }
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <returns></returns>
        object IBindingList.AddNew()
        {
            object value = _ctorInfo.Invoke(null);
            Add(value);
            return value;
        }

        /// <summary>
        /// 是否允许编辑
        /// </summary>
        public bool AllowEdit
        {
            get { return _allowEdit; }
            set { _allowEdit = value; }
        }

        //索引（不支持）
        bool IBindingList.SupportsSearching { get { return false; } }
        void IBindingList.AddIndex(PropertyDescriptor property) { throw new NotSupportedException(); }
        int IBindingList.Find(PropertyDescriptor property, object key) { throw new NotSupportedException(); }
        void IBindingList.RemoveIndex(PropertyDescriptor property) { throw new NotSupportedException(); }

        #endregion

        #region ITypedList 实现
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            return _pdc;
        }
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors)
        {
            return _name;
        }
        #endregion

        #region IComparer 实现

        int IComparer.Compare(object x, object y)
        {
            x = _sortProperty.GetValue(x);
            y = _sortProperty.GetValue(y);
            int retval = Comparer.Default.Compare(x, y);
            return (_sortDirection == ListSortDirection.Ascending)
                ? +retval
                : -retval;
        }

        #endregion
        #endregion

        #region 重载方法
        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override int Add(object value)
        {
            CheckType(value);
            int index = base.Add(value);
            OnListChanged(ListChangedType.ItemAdded, index);
            return index;
        }

        /// <summary>
        /// 增加范围
        /// </summary>
        /// <param name="c"></param>
        public override void AddRange(ICollection c)
        {
            foreach (object value in c)
                CheckType(value);
            base.AddRange(c);
            OnListChanged(ListChangedType.Reset);
        }

        /// <summary>
        /// 清除
        /// </summary>
        public override void Clear()
        {
            base.Clear();
            OnListChanged(ListChangedType.Reset);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public override void Insert(int index, object value)
        {
            CheckType(value);
            base.Insert(index, value);
            OnListChanged(ListChangedType.ItemAdded, index);
        }

        /// <summary>
        /// 插入范围
        /// </summary>
        /// <param name="index"></param>
        /// <param name="c"></param>
        public override void InsertRange(int index, ICollection c)
        {
            foreach (object value in c)
                CheckType(value);
            base.InsertRange(index, c);
            OnListChanged(ListChangedType.Reset);
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="obj"></param>
        public override void Remove(object obj)
        {
            int index = IndexOf(obj);
            base.Remove(obj);
            OnListChanged(ListChangedType.ItemDeleted, index);
        }

        /// <summary>
        /// 移除某个位置的对象
        /// </summary>
        /// <param name="index"></param>
        public override void RemoveAt(int index)
        {
            base.RemoveAt(index);
            OnListChanged(ListChangedType.ItemDeleted, index);
        }

        /// <summary>
        /// 移除范围
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public override void RemoveRange(int index, int count)
        {
            base.RemoveRange(index, count);
            OnListChanged(ListChangedType.Reset);
        }

        /// <summary>
        /// 排序
        /// </summary>
        public override void Sort()
        {
            base.Sort();
            OnListChanged(ListChangedType.Reset);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <param name="comparer"></param>
        public override void Sort(int index, int count, IComparer comparer)
        {
            base.Sort(index, count, comparer);
            OnListChanged(ListChangedType.Reset);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="comparer"></param>
        public override void Sort(IComparer comparer)
        {
            base.Sort(comparer);
            OnListChanged(ListChangedType.Reset);
        }

        /// <summary>
        /// 反转
        /// </summary>
        public override void Reverse()
        {
            base.Reverse();
            OnListChanged(ListChangedType.Reset);
        }

        /// <summary>
        /// 反转
        /// </summary>
        /// <param name="index"></param>
        /// <param name="count"></param>
        public override void Reverse(int index, int count)
        {
            base.Reverse(index, count);
            OnListChanged(ListChangedType.Reset);
        }

        /// <summary>
        /// 设置范围
        /// </summary>
        /// <param name="index"></param>
        /// <param name="c"></param>
        public override void SetRange(int index, ICollection c)
        {
            foreach (object value in c)
                CheckType(value);
            base.SetRange(index, c);
            OnListChanged(ListChangedType.Reset);
        }

        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public override object this[int index]
        {
            get { return base[index]; }
            set
            {
                if (base[index] != value)
                {
                    CheckType(value);
                    base[index] = value;
                    OnListChanged(ListChangedType.ItemChanged, index);
                }
            }
        }

        /// <summary>
        /// 检查类型
        /// </summary>
        /// <param name="value"></param>
        private void CheckType(object value)
        {
            if (value.GetType() != _itemType)
                throw new ArgumentException(string.Format("错误的项目类型，应该为 {0}.", _itemType));
        }
        #endregion

        #region 帮助类

        /// <summary>
        /// 绑定列表属性描述类
        /// </summary>
        private class BindingArrayListPropertyDescriptor : PropertyDescriptor
        {
            #region 私有成员变量
            private BindingArrayList list;
            private PropertyDescriptor pd;
            #endregion

            #region 定义构造函数
            /// <summary>
            /// 构造函数
            /// </summary>
            /// <param name="list"></param>
            /// <param name="pd"></param>
            internal BindingArrayListPropertyDescriptor(BindingArrayList list, PropertyDescriptor pd)
                : base(pd.Name, null)
            {
                this.list = list;
                this.pd = pd;
            }
            #endregion

            #region 重载方法
            /// <summary>
            /// 
            /// </summary>
            /// <param name="obj"></param>
            /// <param name="value"></param>
            public override void SetValue(object obj, object value)
            {
                // this is needed in .NET 2.0. strange...
                if (value is string)
                {
                    value = pd.Converter.ConvertFromInvariantString((string)value);
                }
                pd.SetValue(obj, value);
                list.OnItemChanged(obj);
            }
            #endregion

            public override object GetValue(object obj) { return pd.GetValue(obj); }
            public override void AddValueChanged(object obj, EventHandler handler) { pd.AddValueChanged(obj, handler); }
            public override bool CanResetValue(object obj) { return pd.CanResetValue(obj); }
            public override Type ComponentType { get { return pd.ComponentType; } }
            public override TypeConverter Converter { get { return pd.Converter; } }
            public override bool IsReadOnly { get { return pd.IsReadOnly; } }
            public override Type PropertyType { get { return pd.PropertyType; } }
            public override void ResetValue(object obj) { pd.ResetValue(obj); }
            public override bool ShouldSerializeValue(object obj) { return pd.ShouldSerializeValue(obj); }
        }

        #endregion
    }
}
