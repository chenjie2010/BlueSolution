//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MessageType.cs
// 描述： 消息类型
// 作者：ChenJie 
// 编写日期：2016-08-015
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 消息类型
    /// </summary>
    [Description("消息类型")]
    public enum MessageType
    {
        /// <summary>
        /// 通知
        /// </summary>
        [Description("通知")]
        Notice = 0,

        /// <summary>
        /// 系统消息
        /// </summary>
        [Description("系统消息")]
        SystemMessage = 1
        
    }
}
