//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： UserToolAction.cs
// 描述： 用户工具操作枚举
// 作者：ChenJie 
// 编写日期：2017-09-23
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 用户工具操作枚举
    /// </summary>
    [Description("用户工具操作枚举")]
    public enum UserToolAction
    {
        /// <summary>
        ///  批量用户导入
        /// </summary>
        [Description("批量用户导入")]
        UsersImported = 0,

        /// <summary>
        /// 批量用户删除
        /// </summary>
        [Description("批量用户删除")]
        UserDeleted = 1,

        /// <summary>
        ///  批量更新用户名
        /// </summary>
        [Description("批量更新用户名")]
        UserName = 10,

        /// <summary>
        /// 批量更新姓名
        /// </summary>
        [Description("批量更新姓名")]
        UserActualName = 11,

        /// <summary>
        /// 批量更新身份证号
        /// </summary>
        [Description("批量更新身份证号")]
        UserIdentity = 12,

        /// <summary>
        /// 批量更新电话号码
        /// </summary>
        [Description("批量更新电话号码")]
        TelephoneNumber = 13,

        /// <summary>
        /// 批量更新用户类型名称
        /// </summary>
        [Description("批量更新用户类型名称")]
        UserTypeName = 14,

        /// <summary>
        /// 批量更新用户单位名称
        /// </summary>
        [Description("批量更新用户单位名称")]
        DepName = 15,

        /// <summary>
        /// 批量更新用户密码
        /// </summary>
        [Description("批量更新用户密码")]
        Password = 16,

        /// <summary>
        /// 批量更新用户邮件地址
        /// </summary>
        [Description("批量更新用户邮件地址")]
        Email = 17
    }
}
