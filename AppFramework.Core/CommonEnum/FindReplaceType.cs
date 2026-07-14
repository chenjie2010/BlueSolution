using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 查找替换类型
    /// </summary>
    [Description("查找替换类型")]
    public enum FindReplaceType
    {
        /// <summary>
        /// 查找
        /// </summary>
        [Description("查找")]
        Find =0 ,

        /// <summary>
        /// 替换
        /// </summary>
        [Description("替换")]
        Replace = 1
    }
}
