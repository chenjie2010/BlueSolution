//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： TextIntValue.cs.cs
// 描述：文本整形值类
// 作者：ChenJie 
// 编写日期：2018/11/10
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 文本整形值类
    /// </summary>
    public class TextIntValue
    {
        #region 定义私有变量

        private string _text;
        private int _digit;
        
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
        /// 数字
        /// </summary>
        public int Digit
        {
            get
            {
                return _digit;
            }
            set
            {
                _digit = value;
            }
        }        
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public TextIntValue()
        {
            _text = string.Empty;
            _digit = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="text"></param>
        /// <param name="digit"></param>
        public TextIntValue(string text, int digit)
        {
            _text = text;
            _digit = digit;
        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 获得名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}_{1}", _text, _digit);
        }

        #endregion
    }
}
