//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WorkflowRoleType.cs
// 描述： 工作流角色类型
// 作者：ChenJie 
// 编写日期：2017/12/06
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流角色类型
    /// </summary>
    [Description("工作流角色类型")]
    public enum WorkflowRoleType
    {
        /// <summary>
        /// 单位角色
        /// </summary>
        [Description("单位角色")]
        Allow = 0,

        /// <summary>
        /// 指定角色
        /// </summary>
        [Description("指定角色")]
        Role = 1,

        /// <summary>
        /// 指定用户
        /// </summary>
        [Description("指定用户")]
        User = 2,

        /// <summary>
        /// 自定义用户
        /// </summary>
        [Description("自定义用户")]
        CustomUser = 3
    }
}
