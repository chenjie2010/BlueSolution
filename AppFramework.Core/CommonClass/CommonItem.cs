//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonItem.cs
// 描述： 选项类
// 作者：ChenJie 
// 编写日期：2016/08/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 选项类
    /// </summary>
    [Serializable]
    public class CommonItem<T>
    {
        #region 内部成员变量

        private string _text;
        private T _value;

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
        public T Value
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
        public CommonItem()
        {
            _text = string.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public CommonItem(string text, T value)
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
