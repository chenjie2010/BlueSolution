//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： LogicalDataFieldCategory.cs
// 描述： 逻辑字段分类枚举
// 作者：ChenJie 
// 编写日期：2016/09/23
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 逻辑字段分类枚举
    /// </summary>
    public enum LogicalDataFieldCategory
    {
        /// <summary>
        /// 表达式类型：LogicalDataFieldType 的值在 1 - 19 之间
        /// </summary>
        [Description("表达式类型")]
        Expression = 20,

        /// <summary>
        /// 复合类型：LogicalDataFieldType 的值在 21 - 39 之间
        /// </summary>
        [Description("复合类型")]
        Combination = 40,

        /// <summary>
        /// 其他类型：LogicalDataFieldType 的值在 41 - 59 之间
        /// </summary>
        [Description("其他类型")]
        Other = 60

    }
}
