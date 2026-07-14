//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RowEvent.cs
// 描述: RowEvent 选择行类
// 作者：ChenJie 
// 编写日期：2016-08-22
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;

namespace AppFramework.WinFormsControls
{
    /// <summary>
    /// 选择行类
    /// </summary>
    public class RowEvent : EventArgs
    {
        #region 内部成员变量

        private int _rowHandle;
        private IOrderedDictionary _keyTable;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="rowHandle"></param>
        /// <param name="keyTable"></param>
        public RowEvent(int rowHandle, IOrderedDictionary keyTable)
        {
            _rowHandle = rowHandle;
            _keyTable = keyTable;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 行数
        /// </summary>
        public int RowHandle
        {
            get
            {
                return _rowHandle;
            }
        }

        public object this[string name]
        {
            get
            {
                return _keyTable[name];
            }
        }

        public object this[int index]
        {
            get
            {
                return _keyTable[index];
            }
        }

        public object Value
        {
            get
            {
                if (_keyTable.Count > 0)
                {
                    return _keyTable[0];
                }
                return null;
            }
        }

        public virtual IOrderedDictionary Values
        {
            get
            {
                if (_keyTable is OrderedDictionary)
                {
                    return ((OrderedDictionary)_keyTable).AsReadOnly();
                }
                if (this._keyTable is ICloneable)
                {
                    return (IOrderedDictionary)((ICloneable)_keyTable).Clone();
                }
                OrderedDictionary dictionary = new OrderedDictionary();
                foreach (DictionaryEntry entry in _keyTable)
                {
                    dictionary.Add(entry.Key, entry.Value);
                }
                return dictionary.AsReadOnly();
            }
        }

        #endregion
    }
}
