//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SystemMenuAuthority.cs
// 描述： 系统菜单权限
// 作者：ChenJie 
// 编写日期：2018-09-04
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 系统菜单权限
    /// </summary>
    [Description("系统菜单权限")]
    public enum SystemMenuAuthority
    {
        /// <summary>
        /// 数据处理
        /// </summary>
        [Description("系统维护")]
        SysManagement = 0,

        /// <summary>
        /// 系统消息
        /// </summary>
        [Description("系统消息")]
        SysMessage = 1,

        /// <summary>
        /// 系统日志
        /// </summary>
        [Description("系统日志")]
        SysLog = 2,

        /// <summary>
        /// 接口管理
        /// </summary>
        [Description("接口管理")]
        Interface = 3,
        
        /// <summary>
        /// 用户管理
        /// </summary>
        [Description("用户管理")]
        User = 6,

        /// <summary>
        /// UserQuery
        /// </summary>
        [Description("角色管理")]
        Role = 7,

        /// <summary>
        /// 权限管理
        /// </summary>
        [Description("权限管理")]
        Authority = 8,

        /// <summary>
        /// 用户类型
        /// </summary>
        [Description("用户类型")]
        UserType = 9,

        /// <summary>
        /// 单位管理
        /// </summary>
        [Description("单位管理")]
        Department = 10,

        /// <summary>
        /// 枚举管理
        /// </summary>
        [Description("枚举管理")]
        Enum = 11,

        /// <summary>
        /// 关联管理
        /// </summary>
        [Description("关联管理")]
        Association = 12,

        /// <summary>
        /// 数据表管理
        /// </summary>
        [Description("数据表管理")]
        Table = 13,

        /// <summary>
        /// 组合表管理
        /// </summary>
        [Description("组合表管理")]
        CombinedTable = 14,

        /// <summary>
        /// 查询视图管理
        /// </summary>
        [Description("查询视图管理")]
        View = 15,

        /// <summary>
        /// 个人信息设计
        /// </summary>
        [Description("个人信息设计")]
        DataAuditing = 20,

        /// <summary>
        /// 个人信息变更设计
        /// </summary>
        [Description("个人信息变更设计")]
        DataUpdated = 21,

        /// <summary>
        /// 数据填报设计
        /// </summary>
        [Description("数据填报设计")]
        DataFilled = 22,

        /// <summary>
        /// 复表设计
        /// </summary>
        [Description("复表设计")]
        InputReport = 23,

        /// <summary>
        /// 工作流设计
        /// </summary>
        [Description("工作流设计")]
        WorkflowDesigner = 24,

        /// <summary>
        /// 数据查询设计
        /// </summary>
        [Description("数据查询设计")]
        Query = 25,

        /// <summary>
        /// 报表设计
        /// </summary>
        [Description("报表设计")]
        Report = 26,

        /// <summary>
        /// 业务预约设计
        /// </summary>
        [Description("业务预约设计")]
        Appointment = 27,

        /// <summary>
        /// 菜单设计
        /// </summary>
        [Description("菜单设计")]
        Menu = 28,

        /// <summary>
        /// 打印管理
        /// </summary>
        [Description("打印管理")]
        Print = 32,

        /// <summary>
        /// 工作流实例管理
        /// </summary>
        [Description("工作流实例管理")]
        WorkflowInstance = 33,

        /// <summary>
        /// 业务预约实例管理
        /// </summary>
        [Description("业务预约实例管理")]
        AppointmentInstance = 34,
        
        /// <summary>
        /// 招聘管理
        /// </summary>
        [Description("招聘管理")]
        Work = 40,

        /// <summary>
        /// 工资社保管理
        /// </summary>
        [Description("工资社保管理")]
        Salary = 41,

        /// <summary>
        /// 数据设计
        /// </summary>
        [Description("数据设计")]
        Data = 50,

        /// <summary>
        /// 数据转表
        /// </summary>
        [Description("数据转表")]
        DataProcess = 56,

        /// <summary>
        /// 数据校验
        /// </summary>
        [Description("数据校验")]
        DataVerfication = 57,

        /// <summary>
        /// 数据交换
        /// </summary>
        [Description("数据交换")]
        DataExchange = 58,

        /// <summary>
        /// 本地数据导入
        /// </summary>
        [Description("本地数据导入")]
        DataImport = 59,

        /// <summary>
        /// 远程数据导入
        /// </summary>
        [Description("远程数据导入")]
        RemoteData = 60,

        /// <summary>
        /// 数据备份
        /// </summary>
        [Description("数据备份")]
        DataBackup = 61
    }
}
