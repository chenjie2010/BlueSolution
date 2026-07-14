//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SystemConfigKeyName.cs
// 描述： 系统配置关键字与值对
// 作者：ChenJie 
// 编写日期：2018/07/02
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 系统配置关键字与值对
    /// </summary>
    [Description("系统配置关键字与值对")]
    public enum SystemConfigKeyName
    {
        /// <summary>
        /// 默认系统名称
        /// </summary>
        [Description("默认系统名称")]
        DefaultSystemName = 0,

        /// <summary>
        /// 标签信息类： 单位标签信息
        /// </summary>
        [Description("单位名称|单位编码|单位性质")]
        DepartmentLabelInfo = 1,

        /// <summary>
        /// 对象的属性分类：单位属性
        /// </summary>
        [Description("学院（文科）,0|学院（理科）,1|学院（工科）,2|学院（医科）,3|科研机构,4|业务单位,5|党群,6|行政,7|附设机构,8")]
        DepartmentProperty = 2,

        /// <summary>
        /// 对象的属性分类：证件类型
        /// </summary>
        [Description("身份证,0|护照,1|其他,10")]
        IdentityType = 3,

        /// <summary>
        /// 启用密码复杂校验机制
        /// </summary>
        [Description("启用密码复杂校验机制")]
        PasswordValidated = 4,

        /// <summary>
        /// 启用用户登录错误锁定机制
        /// </summary>
        [Description("启用用户登录错误锁定机制")]
        LogonLocked = 5,

        /// <summary>
        /// 用户名标签信息
        /// </summary>
        [Description("用户名标签信息")]
        UserNameLabelInfo = 6,

        /// <summary>
        /// Web系统自动注销时间
        /// </summary>
        [Description("Web系统自动注销时间")]
        WebTimeout = 7,

        /// <summary>
        /// 启用自定义的邮件地址发送密码
        /// </summary>
        [Description("SMTP启用自定义的邮件地址发送密码")]
        EnableUserEmail = 11,

        /// <summary>
        /// 发送密码的SMTP地址
        /// </summary>
        [Description("SMTP地址")]
        EmailSMTP = 12,

        /// <summary>
        /// 发送密码的邮件名称
        /// </summary>
        [Description("发送密码的邮件名称地址")]
        EmailUserName = 13,

        /// <summary>
        /// 发送密码的邮件密码
        /// </summary>
        [Description("发送密码的邮件密码")]
        EmailPassword = 14,

        /// <summary>
        /// 备用SMTP地址
        /// </summary>
        [Description("备用SMTP地址")]
        AlternativeEmailSMTP = 15,

        /// <summary>
        /// 备用的发送密码的邮件名称
        /// </summary>
        [Description("SMTP备用的发送密码的邮件名称")]
        AlternativeEmailUserName = 16,

        /// <summary>
        /// 备用的发送密码的邮件密码
        /// </summary>
        [Description("SMTP备用的发送密码的邮件密码")]
        AlternativeEmailPassword = 17,      

        /// <summary>
        /// 远程链接用户名
        /// </summary>
        [Description("远程链接用户名")]
        RemoteUserName = 21,

        /// <summary>
        /// 远程链接密码
        /// </summary>
        [Description("远程链接密码")]
        RemotePassword = 22,

        /// <summary>
        /// 是否启用第三方登录
        /// </summary>
        [Description("是否启用第三方登录")]
        EnableAccess = 31,

        /// <summary>
        /// 本系统Web地址
        /// </summary>
        [Description("本系统Web地址")]
        WebAddress = 32,

        /// <summary>
        /// 校验关键字
        /// </summary>
        [Description("校验关键字")]
        UniqueCodeName = 33,

        /// <summary>
        /// 第三方校验地址
        /// </summary>
        [Description("第三方校验地址")]
        SSOValidationAddress = 34,

        /// <summary>
        /// 接口关键字
        /// </summary>
        [Description("接口关键字")]
        AccessTokenName = 35,

        /// <summary>
        /// 第三方接口地址
        /// </summary>
        [Description("第三方接口地址")]
        InterfaceAddress = 36,

        /// <summary>
        /// 客户端编号
        /// </summary>
        [Description("客户端编号")]
        SSOClientId = 37,

        /// <summary>
        /// 客户端密码 
        /// </summary>
        [Description("客户端密码")]
        SSOPassword = 38,

        /// <summary>
        /// 第三方注销地址 
        /// </summary>
        [Description("第三方注销地址")]
        Logout = 39,

        /// <summary>
        /// 是否启用记录日志
        /// </summary>
        [Description("是否启用记录日志")]
        EnableLog = 41,

        /// <summary>
        /// 日志记录级别
        /// </summary>
        [Description("日志记录级别是否启用记录日志")]
        LogLevel = 42,

        /// <summary>
        /// 启用自动备份
        /// </summary>
        [Description("启用自动备份")]
        AutoBackup = 71,

        /// <summary>
        /// 备份周期
        /// </summary>
        [Description("备份周期")]
        Period = 72,

        /// <summary>
        /// 备份时间
        /// </summary>
        [Description("备份时间")]
        BackupDateTime = 73,

        /// <summary>
        /// 数据范围
        /// </summary>
        [Description("数据范围")]
        DataRange = 74,




    }
}
