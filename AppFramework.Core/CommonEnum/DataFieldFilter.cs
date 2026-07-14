//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldFilter.cs
// 描述： 字段选择类型枚举
// 作者：ChenJie 
// 编写日期：2016/10/17
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 字段选择类型枚举
    /// </summary>
    [Description("字段选择类型枚举")]
    public enum DataFieldFilter
    {
        /// <summary>
        /// 全部类型字段
        /// </summary>
        [Description("全部类型字段")]
        All = 0,

        /// <summary>
        /// 显示系统字段和物理类型字段
        /// </summary>
        [Description("显示系统字段和物理类型字段")]
        SystemDataFieldAndPhysicalDataField = 1,

        /// <summary>
        /// 仅显示物理类型字段
        /// </summary>
        [Description("仅显示物理类型字段")]
        OnlyPhysicalField = 2,

        /// <summary>
        /// 显示物理字段和逻辑字段
        /// </summary>
        [Description("显示物理字段和逻辑字段")]
        PhysicalFieldAndLogicalField = 3,

        /// <summary>
        /// 仅显示物理字段中的数字类型
        /// </summary>
        [Description("仅显示物理字段中的数字类型")]
        DigtalTypeInPhysicalField = 4,

        /// <summary>
        /// 仅显示物理字段中的时间类型
        /// </summary>
        [Description("仅显示物理字段中的时间类型")]
        DateTimeTypeInPhysicalField = 5,

        /// <summary>
        /// 仅显示物理字段中的枚举类型
        /// </summary>
        [Description("仅显示物理字段中的枚举类型")]
        EnumTypeInPhysicalField = 6,

        /// <summary>
        /// 仅显示物理字段中的主关联类型
        /// </summary>
        [Description("仅显示物理字段中的主关联类型")]
        PrimaryAssociationInPhysicalField = 7,

        /// <summary>
        /// 能够生成一维码的物理字段类型
        /// </summary>
        [Description("能够生成一维码的物理字段类型")]
        OneDimCodeTypeInPhysicalField = 8,

        /// <summary>
        /// 仅显示物理字段中的单位类型
        /// </summary>
        [Description("仅显示物理字段中的单位类型")]
        OnlyDepartmentPhysicalField = 9,

        /// <summary>
        /// 显示物理字段和关键逻辑字段
        /// </summary>
        [Description("显示物理字段和关键逻辑字段")]
        PhysicalFieldAndKeyLogicalField = 10,

        /// <summary>
        /// 附件类型
        /// </summary>
        [Description("附件类型")]
        Attachement = 11,

        /// <summary>
        /// 自定义
        /// </summary>
        [Description("自定义")]
        Custom = 15
    }
}
