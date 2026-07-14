//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： UserToolError.cs
// 描述： 用户工具操作时错误枚举
// 作者：ChenJie 
// 编写日期：2018-07-06
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 用户工具操作时错误枚举
    /// </summary>
    [Description("用户工具操作枚举")]
    public enum UserToolError
    {
        /// <summary>
        ///  无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        ///  用户名不能为空
        /// </summary>
        [Description("用户名不能为空")]
        UserNameEmpty = 1,

        /// <summary>
        /// 用户名重复
        /// </summary>
        [Description("用户名重复")]
        RepeatedUserName = 2,

        /// <summary>
        /// 用户名长度超过限制
        /// </summary>
        [Description("用户名长度超过限制")]
        UserNameLenth = 3,

        /// <summary>
        /// 用户名已存在
        /// </summary>
        [Description("用户名已存在")]
        UserNameExisted = 4,

        /// <summary>
        /// 用户名不存在
        /// </summary>
        [Description("用户名不存在")]
        UserNameNotExisted = 5,

        /// <summary>
        /// 用户类型不存在
        /// </summary>
        [Description("用户类型不存在")]
        UserTypeNotExisted = 6,
        
        /// <summary>
        /// 用户单位不存在
        /// </summary>
        [Description("用户单位不存在")]
        DepartmentNotExisted = 7,

        /// <summary>
        /// 该单元格的数据不能为空
        /// </summary>
        [Description("该单元格的数据不能为空")]
        DataEmpty = 8,

        /// <summary>
        /// 新用户名长度超过限制
        /// </summary>
        [Description("新用户名长度超过限制")]
        NewUserNameLenth = 9,

        /// <summary>
        /// 新用户名重复
        /// </summary>
        [Description("新用户名重复")]
        RepeatedNewUserName = 10,

        /// <summary>
        /// 证件号码不符合格式要求
        /// </summary>
        [Description("证件号码不符合格式要求")]
        IdentityError = 11,

        /// <summary>
        /// 证件号码重复
        /// </summary>
        [Description("证件号码重复")]
        RepeatedIdentity = 12,

        /// <summary>
        /// 密码长度超过限制
        /// </summary>
        [Description("密码长度超过限制")]
        PasswordLength = 13,

        /// <summary>
        /// 用户姓名长度超过限制
        /// </summary>
        [Description("用户姓名长度超过限制")]
        UserActualNameLength = 14,

        /// <summary>
        /// 手机号码格式不正确
        /// </summary>
        [Description("手机号码格式不正确")]
        TelephoneNumberError = 15,

        /// <summary>
        /// Email 格式不正确
        /// </summary>
        [Description("Email 格式不正确")]
        EmailError = 16
    }
}
