//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： GroupType.cs
// 描述： 业务类型
// 作者：ChenJie 
// 编写日期：2017-10-11
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 业务类型
    /// </summary>
    [Description("业务类型")]
    public enum GroupType
    {
        /// <summary>
        /// 无
        /// </summary>
        [Description("无")]
        None = 0,

        /// <summary>
        /// 用户类型
        /// </summary>
        [Description("用户类型")]
        UserType = 1,

        /// <summary>
        /// 关联
        /// </summary>
        [Description("关联")]
        Association = 2,

        /// <summary>
        /// 组合表
        /// </summary>
        [Description("组合表")]
        CombinedTable = 3,

        /// <summary>
        /// 视图
        /// </summary>
        [Description("视图")]
        View = 4,

        /// <summary>
        /// 数据填充
        /// </summary>
        [Description("数据填充")]
        DataFill = 5,

        /// <summary>
        /// 工作流
        /// </summary>
        [Description("工作流")]
        Workflow = 6,

        /// <summary>
        /// 查询业务
        /// </summary>
        [Description("查询业务")]
        Query = 7,

        /// <summary>
        /// 查询报表业务
        /// </summary>
        [Description("查询报表业务")]
        QueryReport = 8,

        /// <summary>
        /// 录入报表设计
        /// </summary>
        [Description("录入报表业务")]
        InputReport = 9,

        /// <summary>
        /// 角色
        /// </summary>
        [Description("角色")]
        Role = 10,

        /// <summary>
        /// 预约业务
        /// </summary>
        [Description("预约业务")]
        Appointment = 11,

        /// <summary>
        /// 个人信息业务
        /// </summary>
        [Description("个人信息业务")]
        InfoAudited = 12,

        /// <summary>
        /// 个人信息变更业务
        /// </summary>
        [Description("个人信息变更业务")]
        InfoUpdated = 13,

        /// <summary>
        /// 接口
        /// </summary>
        [Description("接口")]
        Interface = 14,

        /// <summary>
        /// 打印
        /// </summary>
        [Description("打印")]
        Print = 15,

        /// <summary>
        /// 本地数据交换
        /// </summary>
        [Description("本地数据交换")]
        LocalDataExchanged = 16,

        /// <summary>
        /// 数据转表
        /// </summary>
        [Description("数据转表")]
        DataRelation = 17,

        /// <summary>
        /// 远程数据交换
        /// </summary>
        [Description("远程数据交换")]
        RemoteDataExchanged = 18,
        
        /// <summary>
        /// 用户查询语句
        /// </summary>
        [Description("用户查询语句")]
        QueryStatement = 19,
    }
}
