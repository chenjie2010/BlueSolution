//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： WorkflowNameRule.cs
// 描述： 工作流实例命名字段
// 作者：ChenJie 
// 编写日期：2017/12/06
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 工作流实例命名字段
    /// </summary>
    [Description("工作流实例命名字段")]
    public enum WorkflowInstanceNameRule
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        [Description("单位名称")]
        DepName = 0,

        /// <summary>
        /// 姓名
        /// </summary>
        [Description("姓名")]
        UserActualName = 1,

        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        UserName = 2,

        /// <summary>
        /// 工作流名称
        /// </summary>
        [Description("工作流名称")]
        WorkName = 3,
        
        /// <summary>
        /// 实例创建时间
        /// </summary>
        [Description("实例创建时间")]
        InstanceDate = 4
    }
}
