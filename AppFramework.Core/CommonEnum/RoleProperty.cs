//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： FormType.cs
// 描述： RoleProperty
// 作者：ChenJie 
// 编写日期：2018-01-15
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public enum RoleProperty
    {
        /// 查询密码
        /// </summary>
        [Description("查询密码")]
        PassowrdQueried = 0
    }
}
