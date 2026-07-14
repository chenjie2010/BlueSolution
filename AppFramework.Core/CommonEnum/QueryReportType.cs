//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： QueryReportType.cs
// 描述： 查询报表类型
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
    public enum QueryReportType
    {
        /// <summary>
        /// 基本报表
        /// </summary>
        [Description("基本报表")]
        Basic = 1,

        /// <summary>
        /// 统计报表
        /// </summary>
        [Description("统计报表")]
        Statistics = 2 
    }
}
