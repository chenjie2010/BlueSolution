//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: LogAction.cs
// 描述: 日志动作枚举类
// 作者：ChenJie 
// 编写日期：2018-07-05
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 日志动作
    /// </summary>
    [Description("日志动作")]
    public enum LogAction
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 增加
        /// </summary>
        [Description("增加")]
        Add = 1,

        /// <summary>
        /// 编辑
        /// </summary>
        [Description("编辑")]
        Edit = 2,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 3
    }
}
