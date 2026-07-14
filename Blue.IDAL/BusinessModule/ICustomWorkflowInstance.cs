//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomWorkflowInstance.cs
// 描述：CustomWorkflowInstance 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
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
    /// CustomWorkflowInstance 接口
    /// </summary>
    public interface ICustomWorkflowInstance : IPrincipalTable<CustomWorkflowInstanceInfo>
    {
        #region 接口

        /// <summary>
        /// 按照条件归档
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        void ArchiveWorkflowInstance(IList<WhereConditon> whereConditons, bool isArchived, string archivedUserName, string archivedName);

        /// <summary>
        /// 批量归档
        /// </summary>
        /// <param name="instanceIds"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        void ArchiveWorkflowInstance(IList<decimal> instanceIds, bool isArchived, string archivedUserName, string archivedName);

        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        void ArchiveWorkflowInstance(decimal instanceId, bool isArchived, string archivedUserName, string archivedName);

        /// <summary>
        /// 初始化工作流实例
        /// </summary>
        /// <param name="instanceId"></param>
        void InitWorkWorkflowInstance(decimal instanceId);

        /// <summary>
        /// 获得工作流实例的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        InstanceStatus GetInstanceStatus(decimal instanceId);

        /// <summary>
        /// 终止工作流
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        AbortedResult AbortWorkflowInstance(decimal instanceId);

        /// <summary>
        /// 获得表 WorkflowInstance 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetWorkflowInstances(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 是否允许工作流撤回
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        WithdrawedResult IsUserWorkflowInstanceWithDrawed(decimal instanceId);

        /// <summary>
        /// 发起人撤回工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        WithdrawedResult UserWithdrawWorkflowInstance(decimal instanceId, WorkflowInstanceLogInfo workflowInstanceLogInfo);

        /// <summary>
        /// 获得工作流归档的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        bool GetInstanceArchivedStatus(decimal instanceId);

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        AbortedResult AbortWorkflowInstance(decimal stepId, string comment);

        /// <summary>
        /// 是否允许工作流撤回
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        WithdrawedResult IsWorkflowInstanceWithDrawed(decimal stepId);

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        WithdrawedResult WithdrawWorkflowInstance(decimal stepId, WorkflowInstanceLogInfo workflowInstanceLogInfo);

        /// <summary>
        /// 处理工作流提交的数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="dicRecordEntities"></param>
        /// <returns></returns>
        InstanceSet Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, WorkflowInstanceLogInfo workflowInstanceLogInfo, Dictionary<decimal, List<RecordEntity>> dicRecordEntities);

        /// <summary>
        /// 处理工作流提交的数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        InstanceItem Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, WorkflowInstanceLogInfo workflowInstanceLogInfo, IList<RecordEntity> recordEntities);

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfos"></param>
        /// <param name="workflowInstanceStepInfos"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        InstanceSet Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, decimal stepId, IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos,
            IList<WorkflowInstanceStepInfo> workflowInstanceStepInfos, Dictionary<decimal, List<RecordEntity>> dicRecordEntities);

        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workflowId"></param>
        /// <param name="instanceStatus"></param>
        /// <returns></returns>
        int GetWorkflowInstanceCount(decimal userId, decimal workflowId, InstanceStatus instanceStatus);

        /// <summary>
        /// 获得表 CustomWorkflowInstance 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 获得工作流实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        IList<CustomWorkflowInstanceInfo> GetModelInfos(decimal userId, decimal workflowId);

        #endregion
    }
}