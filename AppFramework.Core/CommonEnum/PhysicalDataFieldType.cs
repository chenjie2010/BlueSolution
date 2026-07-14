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
    /// 物理字段类型
    /// </summary>
    [Description("物理字段类型")]
    public enum PhysicalDataFieldType
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
        /// 任意字符串类型
        /// </summary>
        [Description("任意字符串类型")]
        ArbitraryString = 21,

        /// <summary>
        /// 扩展字符串类型
        /// </summary>
        [Description("扩展字符串类型")]
        ExtendedArbitraryString = 22,

        /// <summary>
        /// 数字符串类型
        /// </summary>
        [Description("数字符串类型")]
        NumeralString = 23,

        /// <summary>
        /// 字母符串类型
        /// </summary>
        [Description("字母符串类型")]
        CharString = 24,

        /// <summary>
        /// 数字字母混合类型
        /// </summary>
        [Description("数字字母混合类型")]
        MixedString = 25,

        /// <summary>
        /// 加密字符串类型
        /// </summary>
        [Description("加密字符串类型")]
        EncryptedString = 26,

        /// <summary>
        /// 年月日时间类型
        /// </summary>
        [Description("年月日时间类型")]
        YearAndMonthAndDayAndTime = 41,

        /// <summary>
        /// 年月日类型
        /// </summary>
        [Description("年月日类型")]
        YearAndMonthAndDay = 42,

        /// <summary>
        /// 年月类型
        /// </summary>
        [Description("年月类型")]
        YearAndMonth = 43,

        /// <summary>
        /// 月日类型
        /// </summary>
        [Description("月日类型")]
        MonthAndDay = 44,

        /// <summary>
        /// 时间类型
        /// </summary>
        [Description("时间类型")]
        Time = 45,

        /// <summary>
        /// 下拉枚举名称类型
        /// </summary>
        [Description("下拉枚举名称类型")]
        DropdownListEnum = 61,

        /// <summary>
        /// 树形枚举名称类型
        /// </summary>
        [Description("树形枚举名称类型")]
        TreeViewEnum = 62,

        /// <summary>
        /// 级联枚举名称类型
        /// </summary>
        [Description("级联枚举名称类型")]
        CscadeEnum = 63,

        /// <summary>
        /// 多选枚举名称类型
        /// </summary>
        [Description("多选枚举名称类型")]
        MultiSelectedEnum = 64,

        /// <summary>
        /// 单位下拉枚举名称类型(无根节点)
        /// </summary>
        [Description("单位下拉枚举名称类型(无根节点)")]
        DepartmentDropdownListEnum = 65,

        /// <summary>
        /// 单位树形枚举名称类型(无根节点)
        /// </summary>
        [Description("单位树形枚举名称类型(无根节点)")]
        DepartmentTreeViewEnum = 66,

        /// <summary>
        /// 单位树形枚举名称类型(带根节点)
        /// </summary>
        [Description("单位树形枚举名称类型(带根节点)")]
        DepartmentTreeViewEnumWithRoot = 67,

        /// <summary>
        /// 下拉枚举值类型
        /// </summary>
        [Description("下拉枚举值类型")]
        DropdownListEnumValue = 69,

        /// <summary>
        /// 下拉枚举唯一值一类型
        /// </summary>
        [Description("下拉枚举唯一值一类型")]
        DropdownListFstAdditionalCode = 70,

        /// <summary>
        /// 下拉枚举唯一值二类型
        /// </summary>
        [Description("下拉枚举唯一值二类型")]
        DropdownListScdAdditionalCode = 71,

        /// <summary>
        /// 树形枚举值类型
        /// </summary>
        [Description("树形枚举值类型")]
        TreeViewEnumValue = 75,

        /// <summary>
        /// 树形枚举唯一值一类型
        /// </summary>
        [Description("树形枚举唯一值一类型")]
        TreeViewFstAdditionalCode = 76,

        /// <summary>
        /// 树形枚举唯一值二类型
        /// </summary>
        [Description("树形枚举唯一值二类型")]
        TreeViewScdAdditionalCode = 77,        

        /// <summary>
        /// 枚举名称依赖
        /// </summary>
        [Description("枚举名称依赖")]
        EnumNameDependency = 91,

        /// <summary>
        /// 枚举值依赖
        /// </summary>
        [Description("枚举值依赖")]
        EnumValue = 92,

        /// <summary>
        /// 唯一值一依赖
        /// </summary>
        [Description("唯一值一依赖")]
        FstAdditionalCode = 93,

        /// <summary>
        /// 唯一值二依赖
        /// </summary>
        [Description("唯一值二依赖")]
        ScdAdditionalCode = 94,

        /// <summary>
        /// 附加值字符串一依赖
        /// </summary>
        [Description("附加值字符串一依赖")]
        FstAdditionalString = 95,

        /// <summary>
        /// 附加值字符串二依赖
        /// </summary>
        [Description("附加值字符串二依赖")]
        ScdAdditionalString = 96,

        /// <summary>
        /// 附加值字符串三依赖
        /// </summary>
        [Description("附加值字符串三依赖")]
        TrdAdditionalString = 97,

        /// <summary>
        /// 附加值字符串四依赖
        /// </summary>
        [Description("附加值字符串四依赖")]
        FourthAdditionalString = 98,

        /// <summary>
        /// 附加值字符串五依赖
        /// </summary>
        [Description("附加值字符串五依赖")]
        FifthAdditionalString = 99,

        /// <summary>
        /// 附加值字符串六依赖
        /// </summary>
        [Description("附加值字符串六依赖")]
        SixthAdditionalString = 100,

        /// <summary>
        /// 附加值整型一依赖
        /// </summary>
        [Description("附加值整型一依赖")]
        FstAdditionalInteger = 101,

        /// <summary>
        /// 附加值整型二依赖
        /// </summary>
        [Description("附加值整型二依赖")]
        ScdAdditionalInteger = 102,

        /// 附加值实数一依赖
        /// </summary>
        [Description("附加值实数一依赖")]
        FstAdditionalDecimal = 103,

        /// <summary>
        /// 附加值实数二依赖
        /// </summary>
        [Description("附加值实数二依赖")]
        ScdAdditionalDecimal = 104,

        /// <summary>
        /// 单位值依赖
        /// </summary>
        [Description("单位值依赖")]
        DepartmentValue = 105,

        /// <summary>
        /// 单位附加值一依赖
        /// </summary>
        [Description("单位附加值一依赖")]
        DepartmentFstAdditionalCode = 106,

        /// <summary>
        /// 单位附加值二依赖
        /// </summary>
        [Description("单位附加值二依赖")]
        DepartmentScdAdditionalCode = 107,

        /// <summary>
        /// 单关联类型
        /// </summary>
        [Description("单关联类型")]
        Association = 121,

        /// <summary>
        /// 主关联类型
        /// </summary>
        [Description("主关联类型")]
        PrimaryAssociation = 122,

        /// <summary>
        /// 次关联类型
        /// </summary>
        [Description("次关联类型")]
        SecondaryAssociation = 123,

        /// <summary>
        /// 文件附件类型
        /// </summary>
        [Description("文件附件类型")]
        DocAttachment = 131,

        /// <summary>
        /// 图片附件类型
        /// </summary>
        [Description("图片附件类型")]
        PicAttachment = 132,

        /// <summary>
        /// PDF附件类型
        /// </summary>
        [Description("PDF附件类型")]
        PDFAttachment = 133
    }
}
