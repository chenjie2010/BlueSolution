//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ConfigCategory.cs
// 描述： 系统配置分类
// 作者：ChenJie 
// 编写日期：2018-07-02
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 系统配置分类
    /// </summary>
    public enum SystemConfigCategory
    {        
        /// <summary>
        /// 系统参数设置
        /// </summary>
        [Description("系统参数设置")]
        Parameter = 0,

        /// <summary>
        /// 邮件账号设置
        /// </summary>
        [Description("邮件账号设置")]
        Mail = 1,

        /// <summary>
        /// 平台授权管理
        /// </summary>
        [Description("平台授权管理")]
        Platform = 2,

        /// <summary>
        /// 单点登录管理
        /// </summary>
        [Description("单点登录管理")]
        Interface = 3,

        /// <summary>
        /// 日志记录
        /// </summary>
        [Description("日志记录")]
        Log = 4,

        /// <summary>
        /// 注册管理
        /// </summary>
        [Description("注册管理")]
        Regisiter = 5,

        /// <summary>
        /// 密码查询
        /// </summary>
        [Description("密码查询")]
        Password = 6,

        /// <summary>
        /// 数据备份
        /// </summary>
        [Description("数据备份")]
        Backup = 7
    }
}
