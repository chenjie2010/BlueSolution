//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: BusinessMenu.cs
// 描述: 业务管理员业务菜单枚举
// 作者：ChenJie 
// 编写日期：2018/01/15
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 业务管理员业务菜单枚举
    /// </summary>
    public enum BusinessMenu
    {
        /// <summary>
        /// 首页
        /// </summary>
        [Description("首页")]
        MainPage = 0,

        /// <summary>
        /// 个人信息
        /// </summary>
        [Description("个人信息")]
        PersonalData = 1,

        /// <summary>
        /// 信息审核
        /// </summary>
        [Description("信息审核")]
        DataAuditing = 2,

        /// <summary>
        /// 常用业务：创建工作流
        /// </summary>
        [Description("常用业务")]
        CommonBusiness = 3,

        /// <summary>
        /// 我的工作：审核工作流
        /// </summary>
        [Description("我的工作")]
        MyWork = 4,

        /// <summary>
        /// 填报业务
        /// </summary>
        [Description("填报业务")]
        UserData = 5,

        /// <summary>
        /// 填报审核
        /// </summary>
        [Description("填报审核")]
        Auditing = 6,               

        /// <summary>
        /// 数据查询
        /// </summary>
        [Description("数据查询")]
        DataQuery = 7,

        /// <summary>
        /// 报表查询
        /// </summary>
        [Description("报表查询")]
        Report = 8,

        /// <summary>
        /// 数据业务
        /// </summary>
        [Description("数据业务")]
        DataBussiness = 9,

        /// <summary>
        /// 最大值
        /// </summary>
        [Description("最大值")]
        Max = 32
    }
}
