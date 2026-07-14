using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 导入数据源
    /// </summary>
    [Description("导入数据源")]
    public enum CustomDataSource
    {
        /// <summary>
        /// 本地Excel文件
        /// </summary>
        [Description("本地Excel文件")]
        ExcelFile = 0,

        ///  远程数据源
        /// </summary>
        [Description("远程数据源")]
        Remote = 1
    }
}
