using System;
using System.ComponentModel;

namespace AppFramework.WinFormsControls
{
    /// <summary>
    /// 用户操作
    /// </summary>
    [Description("用户操作")]
    public enum UserAction
    {
        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        View = 0,

        /// <summary>
        /// 保存
        /// </summary>
        [Description("保存")]
        Save = 1,
        
        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 2
    }
}
