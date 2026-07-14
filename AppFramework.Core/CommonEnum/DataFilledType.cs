//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataFilledType.cs
// 描述： 填报类型
// 作者：ChenJie 
// 编写日期：2018/02/02
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 填报类型
    /// </summary>
    [Description("填报类型")]
    public enum DataFilledType
    {
        /// <summary>
        /// 通用填报
        /// </summary>
        [Description("通用填报")]
        Common = 1,

        /// <summary>
        /// 单次填报
        /// </summary>
        [Description("单次填报")]
        Single = 2
    }
}
