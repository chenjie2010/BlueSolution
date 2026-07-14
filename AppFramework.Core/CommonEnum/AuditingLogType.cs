//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AuditingLogType.cs
// 描述： 审核日志类型
// 作者：ChenJie 
// 编写日期：2018-10-20
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 审核日志类型
    /// </summary>
    [Description("审核日志类型")]
    public enum AuditingLogType
    {
        /// <summary>
        ///  信息增加申请
        /// </summary>
        [Description("信息增加申请")]
        Add = 0,

        /// <summary>
        ///  信息变更申请
        /// </summary>
        [Description("信息变更申请")]
        Edit = 1
    }
}
