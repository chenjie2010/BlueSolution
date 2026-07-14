//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WorkflowHandlerType.cs
// 描述： 工作流处理类型
// 作者：ChenJie 
// 编写日期：2017/12/06
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流处理类型
    /// </summary>
    [Description("工作流处理类型")]
    public enum WorkflowHandlerType
    {
        /// <summary>
        /// 发起人
        /// </summary>
        [Description("发起人")]
        Sponsor = 0,

        /// <summary>
        /// 单位角色
        /// </summary>
        [Description("单位角色")]
        DepRole = 1,

        /// <summary>
        /// 管理角色
        /// </summary>
        [Description("管理角色")]
        ManagementRole = 2,

        /// <summary>
        /// 指定角色
        /// </summary>
        [Description("指定角色")]
        Role = 3,

        /// <summary>
        /// 指定用户
        /// </summary>
        [Description("指定用户")]
        User = 4

        ///// <summary>
        ///// 自定义用户
        ///// </summary>
        //[Description("自定义用户")]
        //CustomUser = 4
    }
}
