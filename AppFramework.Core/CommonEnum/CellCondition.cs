using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 单元格条件类型
    /// </summary>
    [Description("单元格条件类型")]
    public enum CellCondition
    {
        /// <summary>
        /// 字段显示类型
        /// </summary>
        [Description("显示类型")]
        Show = 0,

        /// <summary>
        /// 字段条件类型
        /// </summary>
        [Description("条件类型")]
        Condition = 1
    }
}
