//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AbortedResult.cs
// 描述： 工作流终止结果
// 作者：ChenJie 
// 编写日期：2018-06-25
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流终止结果
    /// </summary>
    [Description("工作流终止结果")]
    public enum AbortedResult
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
        ///  未知原因
        /// </summary>
        [Description("未知原因")]
        Fail_Unknown = 3

    }
}
