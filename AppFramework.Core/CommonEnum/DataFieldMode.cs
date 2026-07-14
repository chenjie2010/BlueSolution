//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFieldMode.cs
// 描述： DataFieldMode 字段模式
// 作者：ChenJie 
// 编写日期：2017/11/06
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 字段模式
    /// </summary>
    public enum DataFieldMode
    {
        /// <summary>
        ///  显示
        /// </summary>
        [Description("显示")]
        Show = 1,

        /// <summary>
        /// 分组
        /// </summary>
        [Description("分组")]
        Group = 2,

    }
}
