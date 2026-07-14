//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： LogTitle.cs
// 描述： 日志标题
// 作者：ChenJie 
// 编写日期：2018-07-05
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 日志标题
    /// </summary>
    [Description("日志标题")]
    public enum LogTitle
    {
        /// <summary>
        /// 用户登录
        /// </summary>
        [Description("用户登录")]
        Login = 1,

        /// <summary>
        /// 用户登录失败
        /// </summary>
        [Description("用户登录失败")]
        LoginFailed = 2





    }
}
