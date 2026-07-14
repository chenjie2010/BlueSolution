//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: SystemMenu.cs
// 描述: 系统管理员业务菜单枚举
// 作者：ChenJie 
// 编写日期：2016/08/02
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace Blue.CustomLibrary
{
    /// <summary>
    /// 系统管理员业务菜单枚举
    /// </summary>
    [Description("系统管理员业务菜单枚举")]
    public enum SystemMenu
    {
        /// <summary>
        /// 帐号维护
        /// </summary>
        [Description("帐号维护")]
        SysAccount = 0,

        /// <summary>
        /// 系统设置
        /// </summary>
        [Description("系统设置")]
        SysSetting = 1,

        /// <summary>
        /// 业务架构
        /// </summary>
        [Description("业务架构")]
        SysArchitecture = 2,

        /// <summary>
        /// 业务设计
        /// </summary>
        [Description("业务设计")]
        SysDesigner = 3,

        /// <summary>
        /// 业务管理
        /// </summary>
        [Description("业务管理")]
        SysManagement = 4,

        /// <summary>
        /// 业务定制
        /// </summary>
        [Description("业务定制")]
        SysBusiness = 5,

        /// <summary>
        /// 业务数据
        /// </summary>
        [Description("业务数据")]
        SysData = 6,
        
        /// <summary>
        /// 数据处理
        /// </summary>
        [Description("数据处理")]
        SysProcessing = 7,

        /// <summary>
        /// 帮助
        /// </summary>
        [Description("帮助")]
        SysHelp = 8
    }
}
