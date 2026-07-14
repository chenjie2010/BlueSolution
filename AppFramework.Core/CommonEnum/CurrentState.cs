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
    public enum CurrentState
    {
        /// <summary>
        ///  既往
        /// </summary>
        [Description("既往")]
        History = 0,

        /// <summary>
        ///  当前
        /// </summary>
        [Description("当前")]
        Current = 1

    }
}
