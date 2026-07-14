//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IdentificationType.cs
// 描述： 证件类型
// 作者：ChenJie 
// 编写日期：2016/08/05
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 特定字段权限
    /// </summary>
    [Description("特定字段权限")]
    public enum SystemDataFieldPermission
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Description("真实姓名")]
        UserActualName = 0,

        /// <summary>
        /// 用户照片
        /// </summary>
        [Description("用户照片")]
        Photo = 1,

        /// <summary>
        /// 按角色群发邮件功能
        /// </summary>
        [Description("按角色群发邮件功能")]
        MailInGroup = 2,

        /// <summary>
        /// 第三方接口
        /// </summary>
        [Description("第三方接口")]
        Interface = 3,

        /// <summary>
        /// 邮件地址
        /// </summary>
        [Description("邮件地址")]
        Mail = 4,

        /// <summary>
        /// 电话号码
        /// </summary>
        [Description("电话号码")]
        Phone = 5

    }
}
