//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： StringPairValue.cs
// 描述： 行列类
// 作者：ChenJie 
// 编写日期：2018/11/10
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 成对的值
    /// </summary>
    public class StringPairValue
    {
        #region 定义私有变量

        private string _first;
        private string _second;
        
        #endregion

        #region 属性

        /// <summary>
        /// 值
        /// </summary>
        public string First
        {
            get
            {
                return _first;
            }
            set
            {
                _first = value;
            }
        }

        /// <summary>
        /// 列
        /// </summary>
        public string Second
        {
            get
            {
                return _second;
            }
            set
            {
                _second = value;
            }
        }        
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public StringPairValue()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        public StringPairValue(string first, string second)
        {
            _first = first;
            _second = second;
        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 获得名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("{0}_{1}", _first, _second);
        }

        #endregion
    }
}
