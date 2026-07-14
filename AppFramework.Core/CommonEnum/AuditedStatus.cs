//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： 审核状态类型.cs
// 描述： 附件类型
// 作者：ChenJie 
// 编写日期：2017-09-23
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
    public enum AuditedStatus
    {
        /// <summary>
        ///  未审
        /// </summary>
        [Description("未审")]
        None = 0,

        /// <summary>
        ///  已审
        /// </summary>
        [Description("已审")]
        Auditing = 1,

        /// <summary>
        ///  终审
        /// </summary>
        [Description("终审")]
        Audited = 2
    }
}
