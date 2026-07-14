//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ValidationMode.cs
// 描述： 校验模式枚举
// 作者：ChenJie 
// 编写日期：2017/08/17
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 校验模式枚举
    /// </summary>
    [Description("校验模式")]
    public enum ValidationMode
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        UserName = 1,

        /// <summary>
        /// 证件模式
        /// </summary>
        [Description("证件模式")]
        UserIdentity = 2,

        /// <summary>
        /// 手机号码
        /// </summary>
        [Description("手机号码")]
        MobilePhone = 3,

        /// <summary>
        /// 电子邮件地址
        /// </summary>
        [Description("电子邮件地址")]
        Email = 4
    }
}
