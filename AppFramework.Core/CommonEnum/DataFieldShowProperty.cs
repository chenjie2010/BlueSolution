using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 统计样表的单元格数据类型
    /// </summary>
    [Description("字段显示属性")]
    public enum DataFieldShowProperty
    {
        /// <summary>
        /// 排除全局条件
        /// </summary>
        [Description("排除全局条件")]
        GlobalConditionNotIncluded = 0,

        /// <summary>
        /// 排除行条件
        /// </summary>
        [Description("排除行条件")]
        RowConditionNotIncluded = 1,

        /// <summary>
        /// 列条件
        /// </summary>
        [Description("排除列条件")]
        ColumnConditionNotIncluded = 2,

        /// <summary>
        /// 行列条件
        /// </summary>
        [Description("排除行列条件")]
        RowAndColumnConditionNotIncluded = 3
    }
}
