//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： UserLogonState.cs
// 描述： 用户登录类型枚举
// 作者：ChenJie 
// 编写日期：2017/08/17
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 用户登录类型枚举
    /// </summary>
    [Description("用户登录类型枚举")]
    public enum UserLogonState
    {
        /// <summary>
        /// 在线
        /// </summary>
        [Description("在线")]
        Online = 0,

        /// <summary>
        /// 离开
        /// </summary>
        [Description("离开")]
        Leave = 1,

        /// <summary>
        /// 忙碌
        /// </summary>
        [Description("忙碌")]
        Busy = 2,

        /// <summary>
        /// 勿扰
        /// </summary>
        [Description("勿扰")]
        DoNotDisturb = 3,

        /// <summary>
        /// 隐藏
        /// </summary>
        [Description("隐藏")]
        Hiding = 4
    }
}
