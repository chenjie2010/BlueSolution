//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RowArrayEvent.cs
// 描述: RowArrayEvent 选择多行类
// 作者：ChenJie 
// 编写日期：2007-6-3
// Copyright 2007
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Specialized;

namespace AppFramework.WinFormsControls
{
    /// <summary>
    /// 选择多行类
    /// </summary>
    public class RowArrayEvent : EventArgs, ICollection, IEnumerable
    {
        private ArrayList _keys;

        public RowArrayEvent(ArrayList keys)
        {
            _keys = keys;
        }

        public void CopyTo(RowEvent[] array, int index)
        {
            CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return _keys.GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            IEnumerator enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
            {
                array.SetValue(enumerator.Current, index++);
            }
        }

        public int Count
        {
            get
            {
                return _keys.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool IsSynchronized
        {
            get
            {
                return false;
            }
        }

        public RowEvent this[int index]
        {
            get
            {
                return (_keys[index] as RowEvent);
            }
        }

        public object SyncRoot
        {
            get
            {
                return this;
            }
        }
    }
}
