//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AuditingStatus.cs
// 描述： 附件类型
// 作者：ChenJie 
// 编写日期：2018-10-19
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 审核状态类型
    /// </summary>
    [Description("审核状态类型")]
    public enum AuditingStatus
    {
        /// <summary>
        ///  待提交状态
        /// </summary>
        [Description("待提交状态")]
        None = 0,
        
        /// <summary>
        ///  待初审
        /// </summary>
        [Description("待初审")]
        Auditing = 1,

        /// <summary>
        ///  待分配
        /// </summary>
        [Description("待分配")]
        Allocating = 2,

        /// <summary>
        ///  待终审
        /// </summary>
        [Description("待终审")]
        Audited = 3,

        /// <summary>
        ///  完成
        /// </summary>
        [Description("完成")]
        Completed = 4
    }
}
