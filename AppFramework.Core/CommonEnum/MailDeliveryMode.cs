//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MailDeliveryMode.cs
// 描述： 邮件发送方式
// 作者：ChenJie 
// 编写日期：2017-09-11
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 邮件发送方式
    /// </summary>
    public enum MailDeliveryMode
    {
        /// <summary>
        /// 发送
        /// </summary>
        [Description("发送")]
        Delivery = 0,

        /// <summary>
        /// 抄送
        /// </summary>
        [Description("抄送")]
        Copy = 1,

        /// <summary>
        /// 密送
        /// </summary>
        [Description("密送")]
        Blind = 2

    }
}
