//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: StringEventArgs.cs
// 描述: 字符串参数
// 作者：ChenJie 
// 编写日期：2016/08/23
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.WinFormsControls
{
    /// <summary>
    /// 字符串参数
    /// </summary>
    public class StringEventArgs : EventArgs
    {
        #region  属性

        /// <summary>
        /// 内容
        /// </summary>
        public string Content
        {
            get;
            set;
        }

        #endregion

        #region  构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="content"></param>
        public StringEventArgs(string content)
        {
            Content = content;
        }

        #endregion

    }
}
