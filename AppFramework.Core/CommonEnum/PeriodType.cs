//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： PeriodType.cs
// 描述： 时长类型
// 作者：ChenJie 
// 编写日期：2018-08-25
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 时长类型
    /// </summary>
    public enum PeriodType
    {
        /// <summary>
        /// 定时类型
        /// </summary>
        [Description("自定义")]
        Custom = 0,

        /// <summary>
        /// 5分钟
        /// </summary>
        [Description("5分钟")]
        FiveMinutes = 1,

        /// <summary>
        /// 15分钟
        /// </summary>
        [Description("15分钟")]
        FifteenMinutes = 2,

        /// <summary>
        /// 半小时
        /// </summary>
        [Description("半小时")]
        HalfAnHour = 3,

        /// <summary>
        /// 一周
        /// </summary>
        [Description("一周")]
        OneWeek = 4,

        /// <summary>
        /// 一月
        /// </summary>
        [Description("一月")]
        OneMonth = 5,

        /// <summary>
        /// 半年
        /// </summary>
        [Description("半年")]
        HalfAYear = 6,

        /// <summary>
        /// 一年
        /// </summary>
        [Description("一年")]
        Year = 7
    }
}
