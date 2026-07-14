//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CustomSorting.cs
// 描述： CustomSorting 排序方式枚举
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 排序方式
    /// </summary>
    [Description("可见")]
    public enum CustomSorting
    {
        /// <summary>
        ///  未排序
        /// </summary>
        [Description("未排序")]
        None = 0,

        /// <summary>
        /// 按递增顺序排序
        /// </summary>
        [Description("递增")]
        Ascending = 1,

        /// <summary>
        /// 按递减顺序排序
        /// </summary>
        [Description("递减")]
        Descending = 2
    }
}
