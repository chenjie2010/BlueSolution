//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： TableRelation.cs
// 描述： TableRelation 表链接时字段关系
// 作者：ChenJie 
// 编写日期：2017/10/25
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 表链接时字段关系
    /// </summary>
    [Description("表链接时字段关系")]
    public enum DataFieldRelation
    {    
        /// <summary>
        /// 用户编号
        /// </summary>
        [Description("用户编号")]
        UserId = 1,

        /// <summary>
        /// 用户类型
        /// </summary>
        [Description("用户类型编号")]
        UserType = 2,

        /// <summary>
        /// 单位编号
        /// </summary>
        [Description("单位编号")]
        Dpeartment = 3,

        /// <summary>
        /// 业务编号
        /// </summary>
        [Description("业务编号")]
        BuinessId = 4
    }
}
