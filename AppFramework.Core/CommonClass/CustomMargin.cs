//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CustomMargin.cs
// 描述： 报表边框距离类
// 作者：ChenJie 
// 编写日期：2018/09/30
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFramework.Core
{
    /// <summary>
    /// 边距
    /// </summary>
    [Serializable]
    public class CustomMargin
    {
        #region 内部成员变量

        private int _top;
        private int _bottom;
        private int _left;
        private int _right;

        #endregion

        #region 属性

        /// <summary>
        /// 顶部
        /// </summary>
        public int Top
        {
            get
            {
                return _top;
            }
            set
            {
                _top = value;
            }
        }

        /// <summary>
        /// 底部
        /// </summary>
        public int Bottom
        {
            get
            {
                return _bottom;
            }
            set
            {
                _bottom = value;
            }
        }

        /// <summary>
        /// 左部
        /// </summary>
        public int Left
        {
            get
            {
                return _left;
            }
            set
            {
                _left = value;
            }
        }

        /// <summary>
        /// 右部
        /// </summary>
        public int Right
        {
            get
            {
                return _right;
            }
            set
            {
                _right = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CustomMargin()
        {
            _top = 0;
            _bottom = 0;
            _left = 0;
            _right = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="top"></param>
        /// <param name="bottom"></param>
        /// <param name="left"></param>
        /// <param name="right"></param>
        public CustomMargin(int top, int bottom, int left, int right)
        {
            _top = top;
            _bottom = bottom;
            _left = left;
            _right = right;
        }

        #endregion
    }
}
