using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 搜索方式
    /// </summary>
    [Description("搜索方式")]
    public enum FindType
    {
        /// <summary>
        /// 按行搜索
        /// </summary>
        [Description("按行搜索")]
        Row = 0,

        /// <summary>
        /// 按列搜索
        /// </summary>
        [Description("按列搜索")]
        Col = 1
    }
}
