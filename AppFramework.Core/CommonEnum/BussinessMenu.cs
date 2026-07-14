//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： BussinessMenu.cs
// 描述： 角色业务类型
// 作者：ChenJie 
// 编写日期：2019-08-17
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 角色业务类型
    /// </summary>
    [Description("角色业务类型")]
    public enum BussinessMenu
    {
        /// <summary>
        /// 业务菜单
        /// </summary>
        [Description("业务菜单")]
        Business = 1,

        /// <summary>
        /// 业务功能
        /// </summary>
        [Description("业务功能")]
        Function = 2
    }
}
