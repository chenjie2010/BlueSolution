//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： AttachmentCategory.cs
// 描述： 附件类别
// 作者：ChenJie 
// 编写日期：2017/11/29
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.ComponentModel;

namespace AppFramework.Core
{
    /// <summary>
    /// 附件类别
    /// </summary>
    [Description("附件类别")]
    public enum AttachmentCategory
    {
        /// <summary>
        /// 无
        /// </summary>
        None = 0,

        /// <summary>
        /// 邮件
        /// </summary>
        [Description("邮件")]
        Mail = 1,

        /// <summary>
        /// 数据填报
        /// </summary>
        [Description("数据填报指南")]
        DataFilled = 2,

        /// <summary>
        /// 数据填报条件
        /// </summary>
        [Description("数据填报条件")]
        DataFilledCondition = 3,

        /// <summary>
        /// 数据表格
        /// </summary>
        [Description("数据表格")]
        DataForm = 4,

        /// <summary>
        /// 工作流
        /// </summary>
        [Description("工作流")]
        Workflow = 5,

        /// <summary>
        /// 工作流程
        /// </summary>
        [Description("工作流程")]
        WorkflowProcess = 6,

        /// <summary>
        /// 业务帮助
        /// </summary>
        [Description("业务帮助")]
        MenuBusiness = 7,

        /// <summary>
        /// 打印业务
        /// </summary>
        PrintBusiness = 8,

        /// <summary>
        /// 通知
        /// </summary>
        Notice = 9,

        /// <summary>
        /// 消息
        /// </summary>
        Message = 10

    }
}
