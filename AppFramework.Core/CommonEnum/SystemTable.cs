//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: SystemTable.cs
// 描述: 系统表
// 作者：ChenJie 
// 编写日期：2019/05/17
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 系统表
    /// </summary>
    [Description("系统表")]
    public enum SystemTable
    {
        /// <summary>
        /// 用户类型
        /// </summary>
        [Description("用户类型")]
        UserType = 1,

        /// <summary>
        /// 用户单位
        /// </summary>
        [Description("用户单位")]
        Department = 2,

        /// <summary>
        /// 用户列表
        /// </summary>
        [Description("用户列表")]
        User = 3,

        /// <summary>
        /// 枚举类型
        /// </summary>
        [Description("枚举类型")]
        EnumType = 4,

        /// <summary>
        /// 关联类型
        /// </summary>
        [Description("关联类型")]
        Association = 5
    }

}
