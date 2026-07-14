//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MailPriority.cs
// 描述： 邮件类型
// 作者：ChenJie 
// 编写日期：2017-09-15
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 邮件优先级
    /// </summary>
    [Description("优先级")]
    public enum MailPriority
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,

        /// <summary>
        /// 重要
        /// </summary>
        [Description("重要")]
        Importance = 1,

        /// <summary>
        /// 关键
        /// </summary>
        [Description("关键")]
        Critical = 2
    }
}
