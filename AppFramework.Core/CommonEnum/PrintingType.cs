using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 打印类型
    /// </summary>
    public enum PrintingType
    {
        /// <summary>
        /// 预览
        /// </summary>
        [Description("预览")]
        Preview = 0,

        /// <summary>
        /// 打印
        /// </summary>
        [Description("打印")]
        Print = 1

    }
}
