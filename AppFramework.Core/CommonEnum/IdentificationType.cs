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
    /// 证件类型
    /// </summary>
    [Description("证件类型")]
    public enum IdentificationType
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("身份证")]
        Identification = 0,

        /// <summary>
        /// 护照
        /// </summary>
        [Description("护照")]
        Passport = 1,

        /// <summary>
        /// 其他
        /// </summary>
        [Description("其他")]
        Other = 10
    }
}
