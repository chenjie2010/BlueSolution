//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IWorkflowInstanceLog.cs
// 描述: WorkflowInstanceLog 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/8/27
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.IDAL.BusinessModule
{
    /// <summary>
    /// WorkflowInstanceLog 接口
    /// </summary>
    public interface IWorkflowInstanceLog : IPrincipalTable<WorkflowInstanceLogInfo>
    {
        #region 接口

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="stepId">处理步骤编号</param>
        /// <returns> 最新的审核意见</returns>
        Dictionary<string, string> GetComments(decimal stepId);

        /// <summary>
        /// 获得草稿日志
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="instanceId"></param>
        /// <param name="parentUserId"></param>
        /// <returns></returns>
        WorkflowInstanceLogInfo GetDraftLog(decimal processId, decimal instanceId, decimal parentUserId);

        /// <summary>
        /// 获得数据填报的处理流程
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal instanceId);

        /// <summary>
        /// 获得日志的父用户编号
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        decimal GetParentUserId(decimal logId);

        /// <summary>
        /// 获得流程编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        decimal GetProcessId(decimal logId);

        /// <summary>
        /// 获得日志的用户编号
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        decimal GetUserId(decimal logId);

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核意见</returns>
        string GetLastestComment(decimal instanceId);

        #endregion
    }
}