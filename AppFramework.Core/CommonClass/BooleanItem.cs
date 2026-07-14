//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： BooleanItem.cs
// 描述： Boolean 描述项
// 作者：ChenJie 
// 编写日期：2017/09/15
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// Boolean 描述项
    /// </summary>
    [Serializable]
    public class BooleanItem
    {
        #region 内部成员变量

        private string _text;
        private bool _value;

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
        public bool Value
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
        public BooleanItem()
        {
            _text = string.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="value"></param>
        public BooleanItem(string text, bool value)
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
