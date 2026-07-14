//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ReportCategory.cs
// 描述： 报表分类
// 作者：ChenJie 
// 编写日期：2017-12-13
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表格类型
    /// </summary>
    public enum ReportCategory
    {
        /// <summary>
        /// 查询报表：个人报表，统计报表
        /// </summary>
        [Description("查询报表")]
        Query = 0,

        /// <summary>
        /// 复表
        /// </summary>
        [Description("复表")]
        Input = 1
    }
}
