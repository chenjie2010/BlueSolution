//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DateTimeFormat.cs
// 描述： 日期显示格式
// 作者：ChenJie 
// 编写日期：2016/09/22
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 日期显示格式
    /// </summary>
    [Description("日期显示格式")]
    public enum CommonDateTimeFormat
    {
        /// <summary>
        /// 年月日时间类型
        /// </summary>
        [Description("年月日时间类型")]
        YearAndMonthAndDayAndTime = 1,

        /// <summary>
        /// 年月日类型
        /// </summary>
        [Description("年月日类型")]
        YearAndMonthAndDay = 2,

        /// <summary>
        /// 年月类型
        /// </summary>
        [Description("年月类型")]
        YearAndMonth = 3,

        /// <summary>
        /// 月日类型
        /// </summary>
        [Description("月日类型")]
        MonthAndDay = 4,

        /// <summary>
        /// 时间
        /// </summary>
        [Description("时间")]
        Time = 5
    }
}
