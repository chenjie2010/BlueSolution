//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldProperty.cs
// 描述： 字段属性枚举
// 作者：ChenJie 
// 编写日期：2016/09/20
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 字段属性
    /// </summary>
    [Description("字段属性")]
    public enum DataFieldProperty
    {
        /// <summary>
        /// 系统字段
        /// </summary>
        [Description("系统字段")]
        SystemPhysicalDataField = 1,

        /// <summary>
        /// 物理字段
        /// </summary>
        [Description("物理字段")]
        PhysicalDataField = 2,

        /// <summary>
        /// 逻辑字段
        /// </summary>
        [Description("逻辑字段")]
        LogicalDataField = 3
    }
}
