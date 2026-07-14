//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldBracket.cs
// 描述： DataFieldBracket 括号枚举
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 查询字段是否包括括号
    /// </summary>
    public enum DataFieldBracket
    {
        /// <summary>
        /// 无
        /// </summary>
        None,

        /// <summary>
        ///  左括号
        /// </summary>
        LeftBracket,

        /// <summary>
        /// 大于或者等于
        /// </summary>
        RightBracket
    }
}
