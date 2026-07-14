//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldCategory.cs
// 描述： 物理字段分类枚举
// 作者：ChenJie 
// 编写日期：2016/09/23
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 物理字段分类枚举
    /// </summary>
    public enum PhysicalDataFieldCategory
    {
        /// <summary>
        /// 基础数据类型：PhysicalDataFieldType 的值在 1 - 9 之间
        /// </summary>
        [Description("基础数据类型")]
        Basic = 10,

        /// <summary>
        /// 字符串类型：PhysicalDataFieldType 的值在 21 - 29 之间
        /// </summary>
        [Description("字符串类型")]
        String = 30,

        /// <summary>
        /// 日期类型：PhysicalDataFieldType 的值在 41 - 49 之间
        /// </summary>
        [Description("日期类型")]
        Date = 50,

        /// <summary>
        /// 枚举类型：PhysicalDataFieldType 的值在 61 - 89 之间
        /// </summary>
        [Description("枚举类型")]
        Enum = 90,

        /// <summary>
        /// 枚举类型：PhysicalDataFieldType 的值在 91 - 119 之间
        /// </summary>
        [Description("枚举依赖")]
        Dependency = 120,

        /// <summary>
        /// 关联类型： PhysicalDataFieldType 的值在 121 - 129 之间
        /// </summary>
        [Description("关联类型")]
        Association = 130,

        /// <summary>
        /// 附件类型：PhysicalDataFieldType 的值在 131 - 139 之间
        /// </summary>
        [Description("附件类型")]
        Attachement = 140
    }
}
