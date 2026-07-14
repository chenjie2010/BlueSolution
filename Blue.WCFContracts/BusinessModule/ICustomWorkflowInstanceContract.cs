//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomWorkflowInstanceContract.cs
// 描述： CustomWorkflowInstance 契约层接口
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// CustomWorkflowInstance 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomWorkflowInstanceContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomWorkflowInstanceContract : IPrincipalContracts<CustomWorkflowInstanceInfo>
    {
        #region 自定义接口
        
        /// <summary>
        /// 按照条件归档
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        [OperationContract(Name = "ArchiveWorkflowInstances")]
        void ArchiveWorkflowInstance(IList<WhereConditon> whereConditons, bool isArchived, string archivedUserName, string archivedName);

        /// <summary>
        /// 批量归档
        /// </summary>
        /// <param name="instanceIds"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        [OperationContract(Name = "ArchiveWorkflowInstancesByIds")]
        void ArchiveWorkflowInstance(IList<decimal> instanceIds, bool isArchived, string archivedUserName, string archivedName);

        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        [OperationContract(Name = "ArchiveWorkflowInstance")]
        void ArchiveWorkflowInstance(decimal instanceId, bool isArchived, string archivedUserName, string archivedName);

        /// <summary>
        /// 初始化工作流实例
        /// </summary>
        /// <param name="instanceId"></param>
        [OperationContract(Name = "InitWorkWorkflowInstance")]
        void InitWorkWorkflowInstance(decimal instanceId);

        /// <summary>
        /// 获得工作流实例的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetInstanceStatus")]
        InstanceStatus GetInstanceStatus(decimal instanceId);

        /// <summary>
        /// 终止工作流
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        [OperationContract(Name = "AbortWorkflowInstanceByInstanceId")]
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
        [OperationContract(Name = "GetWorkflowInstances")]
        DataSet GetWorkflowInstances(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 是否允许工作流撤回
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [OperationContract(Name = "IsUserWorkflowInstanceWithDrawed")]
        WithdrawedResult IsUserWorkflowInstanceWithDrawed(decimal instanceId);

        /// <summary>
        /// 发起人撤回工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        [OperationContract(Name = "UserWithdrawWorkflowInstance")]
        WithdrawedResult UserWithdrawWorkflowInstance(decimal instanceId, WorkflowInstanceLogInfo workflowInstanceLogInfo);

        /// <summary>
        /// 获得工作流归档的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetInstanceArchivedStatus")]
        bool GetInstanceArchivedStatus(decimal instanceId);

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="stepId">处理步骤编号</param>
        /// <returns> 最新的审核意见</returns>
        [OperationContract(Name = "GetComments")]
        Dictionary<string, string> GetComments(decimal stepId);

        /// <summary>
        /// 获得草稿日志
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="instanceId"></param>
        /// <param name="parentUserId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDraftLog")]
        WorkflowInstanceLogInfo GetDraftLog(decimal processId, decimal instanceId, decimal parentUserId);

        /// <summary>
        /// 获得日志的父用户编号
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetParentUserId")]
        decimal GetParentUserId(decimal logId);

        /// <summary>
        /// 获得流程编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetProcessId")]
        decimal GetProcessId(decimal logId);

        /// <summary>
        /// 获得日志的用户编号
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetUserId")]
        decimal GetUserId(decimal logId);

        /// <summary>
        /// 根据当前实例编号，获得最新的审核人编号
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核人信息</returns>
        [OperationContract(Name = "GetLastestReviewers")]
        Dictionary<decimal, string> GetLastestReviewers(decimal instanceId);

        /// <summary>
        /// 是否允许工作流撤回
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        [OperationContract(Name = "IsWorkflowInstanceWithDrawed")]
        WithdrawedResult IsWorkflowInstanceWithDrawed(decimal stepId);

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        [OperationContract(Name = "WithdrawWorkflowInstance")]
        WithdrawedResult WithdrawWorkflowInstance(decimal stepId, WorkflowInstanceLogInfo workflowInstanceLogInfo);

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        [OperationContract(Name = "AbortWorkflowInstance")]
        AbortedResult AbortWorkflowInstance(decimal stepId, string comment);

        /// <summary>
        /// 处理工作流提交的数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        [ServiceKnownType(typeof(UpLoadFileInfo))]
        [ServiceKnownType(typeof(CommonDataField))]
        [ServiceKnownType(typeof(CommonDataFieldValue))]
        [ServiceKnownType(typeof(RecordItem))]
        [OperationContract(Name = "ProcessByTable")]
        InstanceItem Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, WorkflowInstanceLogInfo workflowInstanceLogInfo, IList<RecordEntity> recordEntities);

        /// <summary>
        /// 处理工作流提交的数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="dicRecordEntities"></param>
        /// <returns></returns>
        [ServiceKnownType(typeof(UpLoadFileInfo))]
        [ServiceKnownType(typeof(CommonDataField))]
        [ServiceKnownType(typeof(CommonDataFieldValue))]
        [ServiceKnownType(typeof(RecordItem))]
        [OperationContract(Name = "ProcessByForm")]
        InstanceSet Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, WorkflowInstanceLogInfo workflowInstanceLogInfo, Dictionary<decimal, List<RecordEntity>> dicRecordEntities);
        
        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfos"></param>
        /// <param name="workflowInstanceStepInfos"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        [ServiceKnownType(typeof(UpLoadFileInfo))]
        [ServiceKnownType(typeof(CommonDataField))]
        [ServiceKnownType(typeof(CommonDataFieldValue))]
        [ServiceKnownType(typeof(RecordItem))]
        [OperationContract(Name = "ProcessWithEvents")]
        InstanceSet Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, decimal stepId, IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos,
            IList<WorkflowInstanceStepInfo> workflowInstanceStepInfos, Dictionary<decimal, List<RecordEntity>> dicRecordEntities);
        
        /// <summary>
        /// 获得工作流实例的日志对象
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        /// 
        [OperationContract(Name = "GetWorkflowInstanceStepInfo")]
        WorkflowInstanceStepInfo GetWorkflowInstanceStepInfo(decimal stepId);

        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workflowId"></param>
        /// <param name="instanceStatus"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetWorkflowInstanceCount")]
        int GetWorkflowInstanceCount(decimal userId, decimal workflowId, InstanceStatus instanceStatus);

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核意见</returns>
        [OperationContract(Name = "GetLastestComment")]
        string GetLastestComment(decimal instanceId);

        /// <summary>
        /// 获得数据填报的处理流程
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetPageRecordByInstanceId")]
        DataSet GetPageRecord(decimal instanceId);

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
        [OperationContract(Name = "GetPageRecord")]
        DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 获得工作流实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfosByWorkflowdId")]
        IList<CustomWorkflowInstanceInfo> GetModelInfos(decimal userId, decimal workflowId);

        #endregion
    }
}