//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： LogicalDataFieldType.cs
// 描述： 逻辑字段类型
// 作者：ChenJie 
// 编写日期：2016/09/22
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 逻辑字段类型
    /// </summary>
    [Description("逻辑字段类型")]
    public enum LogicalDataFieldType
    {
        /// <summary>
        /// 数字类型表达式类型
        /// </summary>
        [Description("数字类型表达式类型")]
        DigitExpression = 1,

        /// <summary>
        /// 字符串表达式类型
        /// </summary>
        [Description("字符串表达式类型")]
        StringExpression = 2,

        /// <summary>
        /// 日期表达式类型
        /// </summary>
        [Description("日期表达式类型")]
        DateTimeExpression = 3,       

        /// <summary>
        /// 自定义条形码类型
        /// </summary>
        [Description("自定义条形码类型")]
        OneDimCode = 21,

        /// <summary>
        /// 用户名条形码类型
        /// </summary>
        [Description("用户名条形码类型")]
        UserName = 22,

        /// <summary>
        /// 二维码类型
        /// </summary>
        [Description("二维码类型")]
        TwoDimCode = 23,

        /// <summary>
        /// 空白名称
        /// </summary>
        [Description("空白名称")]
        Empty = 40
    }
}
