//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataRelationProperty.cs
// 描述： 交换属性
// 作者：ChenJie 
// 编写日期：2018/10/26
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 交换属性
    /// </summary>
    [Description("交换属性")]
    public enum RemoteProperty
    {
        /// <summary>
        /// 导入用户
        /// </summary>
        [Description("导入用户")]
        UserImported = 0     
    }
}
