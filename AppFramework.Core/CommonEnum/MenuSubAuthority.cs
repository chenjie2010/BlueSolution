//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MenuSubAuthority.cs
// 描述： 菜单子权限
// 作者：ChenJie 
// 编写日期：2018-10-04
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 菜单子权限
    /// </summary>
    public enum MenuSubAuthority
    {
        /// <summary>
        /// 个人数据审核
        /// </summary>
        [Description("个人数据审核")]
        PersonalAuditing = 0,

        /// <summary>
        /// 分组数据审核
        /// </summary>
        [Description("分组数据审核")]
        GroupAuditing = 1,

        /// <summary>
        /// 信息变更审核
        /// </summary>
        [Description("信息变更审核")]
        InfoAuditing = 2,

        /// <summary>
        /// 统计数据查询
        /// </summary>
        [Description("统计数据查询")]
        StatisticsQuery = 5,

        /// <summary>
        /// 数据查询导出Excel
        /// </summary>
        [Description("数据查询导出Excel")]
        ExportedExcel = 8,

        /// <summary>
        /// 数据查询导出PDF
        /// </summary>
        [Description("数据查询导出PDF")]
        ExportedPDF = 9
    }
}
