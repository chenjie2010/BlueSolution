//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SystemPhysicalDataField.cs
// 描述： 系统物理字段枚举
// 作者：ChenJie 
// 编写日期：2017/09/28
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 系统物理字段枚举
    /// 系统物理字段：业务表下所有的系统字段
    /// </summary>
    public enum SystemPhysicalDataField
    {
        /// <summary>
        /// 记录编号
        /// </summary>
        [Description("RecordId decimal(10,0) identity(1,1) primary key")]
        RecordId = 1,

        /// <summary>
        /// 用户编号
        /// </summary>
        [Description("UserId decimal(10,0) NOT NULL")]
        UserId = 2,

        /// <summary>
        /// 用户编号
        /// </summary>
        [Description("UserName nvarchar(32) NOT NULL")]
        UserName = 3,

        /// <summary>
        /// 单位编号
        /// </summary>
        [Description("DepId decimal(10,0) NOT NULL")]
        DepId = 4,

        /// <summary>
        /// 用户类型编号
        /// </summary>
        [Description("UserTypeId decimal(10,0) NOT NULL")]
        UserTypeId = 5,

        /// <summary>
        /// 业务编号
        /// </summary>
        [Description("BusinessId decimal(10,0) NULL")]
        BusinessId = 6,

        /// <summary>
        /// 业务外键编号
        /// </summary>
        [Description("BusinessForeignId decimal(10,0) NULL")]
        BusinessForeignId = 7,

        /// <summary>
        /// 业务备用编号
        /// </summary>
        [Description("BusinessAlternativeId decimal(10,0) NULL")]
        BusinessAlternativeId = 8,

        /// <summary>
        /// 记录排序
        /// </summary>
        [Description("RecordSorting int NOT NULL")]
        RecordSorting = 9,

        /// <summary>
        /// 审核状态
        /// </summary>
        [Description("AuditedStatus tinyint NOT NULL")]
        AuditedStatus = 10,

        /// <summary>
        /// 当前既往状态
        /// </summary>
        [Description("CurrentState tinyint NOT NULL")]
        CurrentState = 11,

        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("CreationTime datetime NOT NULL")]
        CreationTime = 12,

        /// <summary>
        /// 修改时间
        /// </summary>
        [Description("ModificationTime datetime NOT NULL")]
        ModificationTime = 13,

        /// <summary>
        /// 记录修改人
        /// </summary>
        [Description("ModifiedByUserName nvarchar(32) null")]
        ModifiedByUserName = 14,

        /// <summary>
        /// 是否删除
        /// </summary>
        [Description("IsDeleted bit NOT NULL")]
        IsDeleted = 15
    }     
}
