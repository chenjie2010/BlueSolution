//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SystemMenuCategory.cs
// 描述： 系统菜单
// 作者：ChenJie 
// 编写日期：2018-09-04
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [Description("系统菜单")]
    public enum SystemMenuCategory
    {        
        /// <summary>
        /// 系统设置
        /// </summary>
        [Description("系统设置")]
        SysSetting = 0,

        /// <summary>
        /// 业务架构
        /// </summary>
        [Description("业务架构")]
        SysArchitecture = 6,

        /// <summary>
        /// 业务设计
        /// </summary>
        [Description("业务设计")]
        SysDesigner = 20,

        /// <summary>
        /// 业务管理
        /// </summary>
        [Description("业务管理")]
        SysManagement = 32,      

        /// <summary>
        /// 业务定制
        /// </summary>
        [Description("业务定制")]
        SysBusiness = 40,

        /// <summary>
        /// 业务数据
        /// </summary>
        [Description("业务数据")]
        SysData = 50,

        /// <summary>
        /// 数据处理
        /// </summary>
        [Description("数据处理")]
        SysProcessing = 56
    }
}
