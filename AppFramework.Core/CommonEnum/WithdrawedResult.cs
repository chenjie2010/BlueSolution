//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WithdrawedResult.cs
// 描述： 工作流撤回结果
// 作者：ChenJie 
// 编写日期：2018-06-25
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流撤回结果
    /// </summary>
    [Description("工作流撤回结果")]
    public enum WithdrawedResult
    {
        /// <summary>
        ///  成功
        /// </summary>
        [Description("成功")]
        Success = 0,

        /// <summary>
        ///  已终止
        /// </summary>
        [Description("已终止")]
        Fail_Aborted = 1,

        /// <summary>
        ///  已完成
        /// </summary>
        [Description("已完成")]
        Fail_Completed = 2,

        /// <summary>
        ///  下一步已审核
        /// </summary>
        [Description("下一步已审核")]
        Fail_Passed = 3,

        /// <summary>
        ///  已被其他审核人驳回
        /// </summary>
        [Description("已被其他审核人驳回")]
        Fail_Rejected = 4,

        /// <summary>
        ///  非本人操作
        /// </summary>
        [Description("非本人操作")]
        Fail_Illegal = 5,

        /// <summary>
        ///  已归档
        /// </summary>
        [Description("已归档")]
        Fail_Archived = 6,

        /// <summary>
        ///  未知原因
        /// </summary>
        [Description("未知原因")]
        Fail_Unknown = 10

    }
}
