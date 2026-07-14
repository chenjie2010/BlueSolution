using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表的审核权限
    /// </summary>
    public enum GridViewAuthority
    {
        /// <summary>
        /// 查看
        /// </summary>
        [Description("查看")]
        View = 1,

        /// <summary>
        /// 增加
        /// </summary>
        [Description("增加")]
        Add = 2,

        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        Edit = 3,

        /// <summary>
        /// 批量编辑
        /// </summary>
        [Description("批量编辑")]
        BatchEdit = 4,

        /// <summary>
        /// 完全编辑
        /// </summary>
        [Description("完全编辑")]
        CompletelyEdit = 5,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 6,

        /// <summary>
        /// 批量删除
        /// </summary>
        [Description("批量删除")]
        BatchDelete = 7,

        /// <summary>
        /// 完全删除
        /// </summary>
        [Description("完全删除")]
        CompletelyDelete = 8,

        /// <summary>
        /// 审核
        /// </summary>
        [Description("审核")]
        Auditing = 9,

        /// <summary>
        /// 批量审核
        /// </summary>
        [Description("批量审核")]
        BatchAuditing = 10,

        /// <summary>
        /// 完全审核
        /// </summary>
        [Description("完全审核")]
        CompletelyAuditing = 11,

        /// <summary>
        /// 设置主从状态
        /// </summary>
        [Description("设置主从状态")]
        MasterSlave = 12,

        /// <summary>
        /// 移动
        /// </summary>
        [Description("移动")]
        Move = 13,

        /// <summary>
        /// 刷新
        /// </summary>
        [Description("刷新")]
        Refresh = 14,

        /// <summary>
        /// 导入数据
        /// </summary>
        [Description("导入数据")]
        Import = 15,

        /// <summary>
        /// 导出数据
        /// </summary>
        [Description("导出数据")]
        Export = 16
    }
}
