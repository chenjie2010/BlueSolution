//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WindowsServiceStep.cs
// 描述： Windows 服务操作步骤
// 作者：ChenJie 
// 编写日期：2017/01/12
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// Windows 服务操作步骤
    /// </summary>
    public enum WindowsServiceStep
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 启动
        /// </summary>
        [Description("启动")]
        Start = 1,

        /// <summary>
        /// 关闭
        /// </summary>
        [Description("关闭")]
        Stop = 2,

        /// <summary>
        /// 重启
        /// </summary>
        [Description("重启")]
        Restart = 3
    }
}
