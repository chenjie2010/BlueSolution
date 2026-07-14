//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MailBoxType.cs
// 描述： 邮箱类型
// 作者：ChenJie 
// 编写日期：2017-09-08
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 邮件状态
    /// </summary>
    public enum MailState
    {
        /// <summary>
        /// 未读
        /// </summary>
        [Description("未读")]
        New = 0,

        /// <summary>
        /// 已读
        /// </summary>
        [Description("已读")]
        HasAlreadyRead = 1,

        /// <summary>
        /// 待办理
        /// </summary>
        [Description("待办理")]
        Pending = 2,

        /// <summary>
        /// 收件箱
        /// </已办结>
        [Description("已办结")]
        Completed = 3

    }
}
