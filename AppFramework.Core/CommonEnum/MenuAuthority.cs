//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MenuAuthority.cs
// 描述： 菜单权限
// 作者：ChenJie 
// 编写日期：2018-10-04
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 菜单权限
    /// </summary>
    public enum MenuAuthority
    {
        /// <summary>
        /// 我的工作
        /// </summary>
        [Description("我的工作")]
        Workflow = 0,

        /// <summary>
        /// 填报审核
        /// </summary>
        [Description("填报审核")]
        Auditing = 1,

        /// <summary>
        /// 用户管理
        /// </summary>
        [Description("用户增加")]
        UserAdded = 2,

        /// <summary>
        /// 用户管理
        /// </summary>
        [Description("用户编辑")]
        UserEdited = 3,

        /// <summary>
        /// 数据转表
        /// </summary>
        [Description("数据转表")]
        DataSwap = 4
    }
}
