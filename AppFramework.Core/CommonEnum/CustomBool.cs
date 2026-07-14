//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CustomBool.cs
// 描述： 自定义 Bool 类型
// 作者：ChenJie 
// 编写日期：2016/09/29
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 自定义 Bool 类型枚举
    /// </summary>
    [Description("自定义 Bool 类型枚举")]
    public enum CustomBool
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 是
        /// </summary>
        [Description("是")]
        True = 1,

        /// <summary>
        /// 否
        /// </summary>
        [Description("否")]
        False = 2       

    }
}
