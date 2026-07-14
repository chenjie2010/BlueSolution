using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 统计单元格数据类型
    /// </summary>
    [Description("统计单元格数据类型")]
    public enum StatisticCellType
    {
        /// <summary>
        /// 统计数据类型
        /// </summary>
        [Description("统计数据类型")]
        OnlyData = 0,

        /// <summary>
        /// 数据值类型
        /// </summary>
        [Description("数据值类型")]
        OnlyValue = 1,

        /// <summary>
        /// 明细类型
        /// </summary>
        [Description("明细类型")]
        Detail = 2,

        /// <summary>
        /// 行条件
        /// </summary>
        [Description("行条件")]
        RowCondition = 3,

        /// <summary>
        /// 列条件
        /// </summary>
        [Description("列条件")]
        ColumnCondition = 4,

        /// <summary>
        /// 行列条件
        /// </summary>
        [Description("行列条件")]
        RowAndColumnCondition = 5,

        /// <summary>
        /// 全局条件
        /// </summary>
        [Description("全局条件")]
        GlobalCondition = 6
    }
}
