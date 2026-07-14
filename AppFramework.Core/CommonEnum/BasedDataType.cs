//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： BasedDataType.cs
// 描述： 基础类型字段
// 作者：ChenJie 
// 编写日期：2016/09/23
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 基础类型字段
    /// </summary>
    [Description("基础类型字段")]
    public enum BasedDataType
    {
        /// <summary>
        /// 布尔类型
        /// </summary>
        [Description("布尔类型")]
        Boolean = 1,

        /// <summary>
        /// 整数类型
        /// </summary>
        [Description("整数类型")]
        Int32 = 2,

        /// <summary>
        /// 实数类型
        /// </summary>
        [Description("实数类型")]
        Decimal = 3,

        /// <summary>
        /// 字符串类型
        /// </summary>
        [Description("字符串类型")]
        String = 4,

        /// <summary>
        /// 时间类型
        /// </summary>
        [Description("时间类型")]
        DateTime = 5
    }
}
