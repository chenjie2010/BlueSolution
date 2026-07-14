//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： KeyValueItem.cs
// 描述： 键值对项
// 作者：ChenJie 
// 编写日期：2016-08-25
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFramework.Core
{
    /// <summary>
    /// 键值对项
    /// </summary>
    public class KeyValueItem
    {
        #region 定义私有变量

        private decimal _key;
        private decimal _value;

        #endregion

        #region 属性

        /// <summary>
        /// 键
        /// </summary>
        public decimal Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        public decimal Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public KeyValueItem()
        {
            _key = decimal.MinValue;
            _value = decimal.MinValue;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public KeyValueItem(decimal key, decimal value)
        {
            _key = key;
            _value = value;
        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 等于
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == this)
                return true;

            if (!(obj is KeyValueItem))
                return false;

            var keyValueItem = (KeyValueItem)obj;

            return _key == keyValueItem.Key && _value == keyValueItem.Value;
        }

        /// <summary>
        /// Hash值
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return string.Format("{0}_{1}", _key, _value).GetHashCode();
        }

        #endregion
    }
}
