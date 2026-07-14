//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: LogLevel.cs
// 描述: summary枚举类
// 作者：ChenJie 
// 编写日期：2018-07-05
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 日志级别
    /// </summary>
    [Description("日志级别")]
    public enum LogLevel
    {
        /// <summary>
        /// 调试
        /// </summary>
        [Description("调试日志")]
        Debug = 0,

        /// <summary>
        /// 信息日志
        /// </summary>
        [Description("信息日志")]
        Info = 1,

        /// <summary>
        /// 通知日志
        /// </summary>
        [Description("通知日志")]
        Notice = 2,

        /// <summary>
        /// 警告日志
        /// </summary>
        [Description("警告日志")]
        Warning = 3,

        /// <summary>
        /// 错误日志
        /// </summary>
        [Description("错误日志")]
        Error = 4,

        /// <summary>
        /// 关键日志
        /// </summary>
        [Description("关键日志")]
        Crirical = 5
    }
}
