//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： EnumItem.cs
// 描述： 枚举项
// 作者：ChenJie 
// 编写日期：2016/08/25
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 枚举项
    /// </summary>
    [Serializable]
    public class EnumItem
    {
        #region 内部成员变量

        private string _text;
        private byte _value;

        #endregion

        #region 属性

        /// <summary>
        /// 文本
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        /// <summary>
        /// 值
        /// </summary>
        public byte Value
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
        public EnumItem()
        {
            _text = string.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public EnumItem(string text, byte value)
        {
            _text = text;
            _value = value;

        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 获得名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _text;
        }

        #endregion
    }
}
