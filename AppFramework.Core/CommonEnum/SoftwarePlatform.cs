//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SoftwarePlatform.cs
// 描述： 软件平台
// 作者：ChenJie 
// 编写日期：2016-07-28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 软件平台枚举
    /// </summary>
    [Description("软件平台枚举")]
    public enum SoftwarePlatform
    {
        /// <summary>
        /// 客户端
        /// </summary>
        [Description("客户端")]
        Client = 0,

        /// <summary>
        /// 管理端
        /// </summary>
        [Description("管理端")]
        Management = 1,

        /// <summary>
        /// 服务器端
        /// </summary>
        [Description("服务器端")]
        Server = 2,

        /// <summary>
        /// Web 端
        /// </summary>
        [Description("Web 端")]
        Web = 3
    }
}
