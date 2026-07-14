//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MenuType.cs
// 描述： 菜单类型
// 作者：ChenJie 
// 编写日期：2017-12-20
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 菜单类型
    /// </summary>
    [Description("菜单类型")]
    public enum MenuType
    {
        /// <summary>
        /// 系统菜单
        /// </summary>
        [Description("系统管理菜单")]
        System = 1,

        /// <summary>
        /// 业务应用菜单
        /// </summary>
        [Description("业务应用菜单")]
        Business = 2
    }
}
