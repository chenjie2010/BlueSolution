//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IdentificationType.cs
// 描述： 证件类型
// 作者：ChenJie 
// 编写日期：2016/08/05
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 包括类型
    /// </summary>
    [Description("包括类型")]
    public enum DataRangeBoundaryType
    {
        /// <summary>
        /// 包括
        /// </summary>
        Inclusive = 1,
       
        /// <summary>
        /// 不包括
        /// </summary>
        Exclusive = 2
    }
}
