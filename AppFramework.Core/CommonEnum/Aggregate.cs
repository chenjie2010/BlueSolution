//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSorting.cs
// 描述: CustomSorting 枚举类
// 作者：ChenJie 
// 编写日期：2011-02-25
// Copyright 2011
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 聚集
    /// </summary>
    [Description("聚集")]
    public enum Aggregate
    {
        /// <summary>
        /// 分组
        /// </summary>
        [Description("分组")]
        GroupBy = 0,

        /// <summary>
        /// 总和
        /// </summary>
        [Description("总和")]
        Sum = 1,

        /// <summary>
        /// 平均值
        /// </summary>
        [Description("平均值")]
        Avg = 2,

        /// <summary>
        /// 最小值
        /// </summary>
        [Description("最小值")]
        Min = 3,

        /// <summary>
        /// 最大值
        /// </summary>
        [Description("最大值")]
        Max = 4,

        /// <summary>
        /// 计数
        /// </summary>
        [Description("计数")]
        Count = 5

        ///// <summary>
        ///// 仅保留总和相同中的唯一记录
        ///// </summary>
        //SumDistinct = 6,

        ///// <summary>
        ///// 仅保留平均值相同中的唯一记录
        ///// </summary>
        //AvgDistinct = 7,

        ///// <summary>
        ///// 
        ///// </summary>
        //MinDistinct = 8,

        ///// <summary>
        ///// 
        ///// </summary>
        //MaxDistinct = 9,

        ///// <summary>
        ///// 
        ///// </summary>
        //CountDistinct = 10       
    }
}
