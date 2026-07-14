//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： GroupCondition.cs
// 描述： 分组条件
// 作者：ChenJie 
// 编写日期：2017-10-30
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 展示模式
    /// </summary>
    public enum FormShowStyle
    {
        /// <summary>
        /// 单栏三列完整模式
        /// </summary>
        [Description("单栏三列完整模式")]
        SingleColumnThreeRanksCompleted = 1,

        /// <summary>
        /// 单栏两列完整模式
        /// </summary>
        [Description("单栏两列完整模式")]
        SingleColumnTwoRanksCompleted = 2,

        /// <summary>
        /// 单栏一列完整模式
        /// </summary>
        [Description("单栏一列完整模式")]
        SingleColumnOneRankCompleted = 3,

        /// <summary>
        /// 双栏一列完整模式
        /// </summary>
        [Description("双栏一列完整模式")]
        TwoColumnsOneRankCompleted = 4,

        /// <summary>
        /// 单栏三列简洁模式
        /// </summary>
        [Description("单栏三列简洁模式")]
        SingleColumnThreeRanksClean = 11,

        /// <summary>
        /// 单栏两列简洁模式
        /// </summary>
        [Description("单栏两列简洁模式")]
        SingleColumnTwoRanksClean = 12,

        /// <summary>
        /// 单栏一列简洁模式
        /// </summary>
        [Description("单栏一列简洁模式")]
        SingleColumnOneRankClean = 13,

        /// <summary>
        /// 双栏一列简洁模式
        /// </summary>
        [Description("双栏一列简洁式")]
        TwoColumnsOneRankClean = 14,        

        /// <summary>
        /// 扩展模式
        /// </summary>
        [Description("扩展模式")]
        Expand = 21,

        /// <summary>
        /// 组合
        /// </summary>
        [Description("组合")]
        Combination = 22
    }
}
