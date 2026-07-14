//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： BackupPeriod.cs
// 描述： 备份周期
// 作者：ChenJie 
// 编写日期：2019/05/18
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 备份周期
    /// </summary>
    [Description("备份周期")]
    public enum BackupPeriod
    {
        /// <summary>
        /// 一天
        /// </summary>
        [Description("一天")]
        OneDay = 0,

        /// <summary>
        /// 两天
        /// </summary>
        [Description("两天")]
        TwoDays = 1,

        /// <summary>
        /// 三天
        /// </summary>
        [Description("三天")]
        ThreeDays = 2,

        /// <summary>
        /// 一周
        /// </summary>
        [Description("一周")]
        OneWeek = 3,

        /// <summary>
        /// 两周
        /// </summary>
        [Description("两周")]
        TwoWeeks = 4,

        /// <summary>
        /// 三周
        /// </summary>
        [Description("三周")]
        ThreeWeeks = 5,
        
        /// <summary>
        /// 一个月
        /// </summary>
        [Description("一个月")]
        OneMonth = 6,

        /// <summary>
        /// 五周
        /// </summary>
        [Description("五周")]
        FiveWeeks = 7,

        /// <summary>
        /// 六周
        /// </summary>
        [Description("六周")]
        SixWeeks = 8

    }
}
