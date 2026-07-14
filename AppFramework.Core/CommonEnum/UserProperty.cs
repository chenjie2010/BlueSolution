//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： UserProperty.cs
// 描述： 用户属性枚举
// 作者：ChenJie 
// 编写日期：2016/08/05
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 用户属性
    /// /教职工类型和注册人员类型两大类人员允许使用多种登录方式，因此校验这两大类的登录使用的关键字
    /// </summary>
    [Description("用户属性")]
    public enum UserProperty
    {
        /// <summary>
        /// 教职工类型
        /// </summary>
        [Description("教职工类型")]
        Staff = 1,

        /// <summary>
        /// 管理员类型
        /// </summary>
        [Description("管理员类型")]
        Administrator = 2,

        /// <summary>
        /// 注册人员类型
        /// </summary>
        [Description("注册人员类型")]
        RegisteredUser = 3,

        /// <summary>
        /// 专家类型
        /// </summary>
        [Description("专家类型")]
        Expert = 4,

        /// <summary>
        /// 自定义类型
        /// </summary>
        [Description("自定义类型")]
        Custom = 5,

        /// <summary>
        /// 其他人员类型
        /// </summary>
        [Description("其他人员类型")]
        Others = 6
    }
}
