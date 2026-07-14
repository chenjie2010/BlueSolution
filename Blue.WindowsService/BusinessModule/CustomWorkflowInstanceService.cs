//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowInstanceService.cs
// 描述：CustomWorkflowInstance 操作服务类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomWorkflowInstance.
    /// </summary>
    public class CustomWorkflowInstanceService : ICustomWorkflowInstanceContract
    {
        #region 业务实例
        
        private static readonly ICustomWorkflowInstanceHandler customWorkflowInstanceHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomWorkflowInstanceHandler>();
        
        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomWorkflowInstanceService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 customworkflowinstance 表中插入一条新记录
		/// </summary>
		/// <param name="customWorkflowInstanceInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomWorkflowInstanceInfo customWorkflowInstanceInfo)
		{
            return customWorkflowInstanceHandler.Insert(customWorkflowInstanceInfo);
		}
        
        /// <summary>
		/// 获得 CustomWorkflowInstanceInfo 对象
		/// </summary>
		///<param name="instanceId">工作流实例编号</param>
		/// <returns> CustomWorkflowInstanceInfo 对象</returns>
		public CustomWorkflowInstanceInfo GetModelInfo(decimal instanceId)
		{	
            return customWorkflowInstanceHandler.GetModelInfo(instanceId);           
		}		
		
        /// <summary>
		/// 更新 CustomWorkflowInstanceInfo 对象
		/// </summary>
		/// <param name="customWorkflowInstanceInfo">CustomWorkflowInstanceInfo 对象</param>
		public void Update(CustomWorkflowInstanceInfo customWorkflowInstanceInfo)
		{	          
            customWorkflowInstanceHandler.Update(customWorkflowInstanceInfo);
        }	
  
        /// <summary>
		/// 删除 CustomWorkflowInstanceInfo 对象
		/// </summary>
		///<param name="instanceId">工作流实例编号</param>
		/// <returns> CustomWorkflowInstanceInfo 对象</returns>
		public void Delete(decimal instanceId)
		{	
            customWorkflowInstanceHandler.Delete(instanceId);
        }
        
        /// <summary>
		/// 获得 CustomWorkflowInstanceInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowInstanceInfo 对象列表</returns>
		public IList<CustomWorkflowInstanceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return customWorkflowInstanceHandler.GetModelInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 CustomWorkflowInstance 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomWorkflowInstanceInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return customWorkflowInstanceHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口
                
        /// <summary>
        /// 获得工作流归档的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public bool GetInstanceArchivedStatus(decimal instanceId)
        {
            return customWorkflowInstanceHandler.GetInstanceArchivedStatus(instanceId);
        }

        /// <summary>
        /// 按照条件归档
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        public void ArchiveWorkflowInstance(IList<WhereConditon> whereConditons, bool isArchived, string archivedUserName, string archivedName)
        {
            customWorkflowInstanceHandler.ArchiveWorkflowInstance(whereConditons, isArchived, archivedUserName, archivedName);
        }

        /// <summary>
        /// 批量归档
        /// </summary>
        /// <param name="instanceIds"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        public void ArchiveWorkflowInstance(IList<decimal> instanceIds, bool isArchived, string archivedUserName, string archivedName)
        {
            customWorkflowInstanceHandler.ArchiveWorkflowInstance(instanceIds, isArchived, archivedUserName, archivedName);
        }

        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        public void ArchiveWorkflowInstance(decimal instanceId, bool isArchived, string archivedUserName, string archivedName)
        {
            customWorkflowInstanceHandler.ArchiveWorkflowInstance(instanceId, isArchived, archivedUserName, archivedName);
        }

        /// <summary>
        /// 初始化工作流实例
        /// </summary>
        /// <param name="instanceId"></param>
        public void InitWorkWorkflowInstance(decimal instanceId)
        {
            customWorkflowInstanceHandler.InitWorkWorkflowInstance(instanceId);
        }

        /// <summary>
        /// 获得工作流实例的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public InstanceStatus GetInstanceStatus(decimal instanceId)
        {
            return customWorkflowInstanceHandler.GetInstanceStatus(instanceId);
        }

        /// <summary>
        /// 终止工作流
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public AbortedResult AbortWorkflowInstance(decimal instanceId)
        {
            return customWorkflowInstanceHandler.AbortWorkflowInstance(instanceId);
        }

        /// <summary>
        /// 获得表 WorkflowInstance 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetWorkflowInstances(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            return customWorkflowInstanceHandler.GetWorkflowInstances(startPosition, count, whereConditons, ref totalCount);
        }

        /// <summary>
        /// 是否允许发起人撤回工作流
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public WithdrawedResult IsUserWorkflowInstanceWithDrawed(decimal instanceId)
        {
            return customWorkflowInstanceHandler.IsUserWorkflowInstanceWithDrawed(instanceId);
        }

        /// <summary>
        /// 发起人撤回工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        public WithdrawedResult UserWithdrawWorkflowInstance(decimal instanceId, WorkflowInstanceLogInfo workflowInstanceLogInfo)
        {
            return customWorkflowInstanceHandler.UserWithdrawWorkflowInstance(instanceId, workflowInstanceLogInfo);
        }        

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="stepId">处理步骤编号</param>
        /// <returns> 最新的审核意见</returns>
        public Dictionary<string, string> GetComments(decimal stepId)
        {
            return customWorkflowInstanceHandler.GetComments(stepId);
        }

        /// <summary>
        /// 获得草稿日志
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="instanceId"></param>
        /// <param name="parentUserId"></param>
        /// <returns></returns>
        public WorkflowInstanceLogInfo GetDraftLog(decimal processId, decimal instanceId, decimal parentUserId)
        {
            return customWorkflowInstanceHandler.GetDraftLog(processId, instanceId, parentUserId);
        }

        /// <summary>
        /// 获得日志的父用户编号
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public decimal GetParentUserId(decimal logId)
        {
            return customWorkflowInstanceHandler.GetParentUserId(logId);
        }

        /// <summary>
        /// 获得流程编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public decimal GetProcessId(decimal logId)
        {
            return customWorkflowInstanceHandler.GetProcessId(logId);
        }

        /// <summary>
        /// 获得日志的用户编号
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public decimal GetUserId(decimal logId)
        {
            return customWorkflowInstanceHandler.GetUserId(logId);
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核人编号
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核人信息</returns>
        public Dictionary<decimal, string> GetLastestReviewers(decimal instanceId)
        {
            return customWorkflowInstanceHandler.GetLastestReviewers(instanceId);
        }

        /// <summary>
        /// 是否允许工作流撤回
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public WithdrawedResult IsWorkflowInstanceWithDrawed(decimal stepId)
        {
            return customWorkflowInstanceHandler.IsWorkflowInstanceWithDrawed(stepId);
        }

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        public WithdrawedResult WithdrawWorkflowInstance(decimal stepId, WorkflowInstanceLogInfo workflowInstanceLogInfo)
        {
            return customWorkflowInstanceHandler.WithdrawWorkflowInstance(stepId, workflowInstanceLogInfo);
        }

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public AbortedResult AbortWorkflowInstance(decimal stepId, string comment)
        {
            return customWorkflowInstanceHandler.AbortWorkflowInstance(stepId, comment);
        }

        /// <summary>
        /// 处理工作流提交的数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public decimal Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, WorkflowInstanceLogInfo workflowInstanceLogInfo, IList<RecordEntity> recordEntities)
        {
            return customWorkflowInstanceHandler.Process(customWorkflowInstanceInfo, workflowInstanceLogInfo, recordEntities);
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfos"></param>
        /// <param name="workflowInstanceStepInfos"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public decimal Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, decimal stepId, IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos,
            IList<WorkflowInstanceStepInfo> workflowInstanceStepInfos, IList<RecordEntity> recordEntities)
        { 
            return customWorkflowInstanceHandler.Process(customWorkflowInstanceInfo, stepId, workflowInstanceLogInfos, 
                workflowInstanceStepInfos, recordEntities);
        }        

        /// <summary>
        /// 获得工作流实例的日志对象
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public WorkflowInstanceStepInfo GetWorkflowInstanceStepInfo(decimal stepId)
        {
            return customWorkflowInstanceHandler.GetWorkflowInstanceStepInfo(stepId);
        }

        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workflowId"></param>
        /// <param name="instanceStatus"></param>
        /// <returns></returns>
        public int GetWorkflowInstanceCount(decimal userId, decimal workflowId, InstanceStatus instanceStatus)
        {
            return customWorkflowInstanceHandler.GetWorkflowInstanceCount(userId, workflowId, instanceStatus);
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核意见</returns>
        public string GetLastestComment(decimal instanceId)
        {
            return customWorkflowInstanceHandler.GetLastestComment(instanceId);
        }

        /// <summary>
        /// 获得数据填报的处理流程
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal instanceId)
        {
            return customWorkflowInstanceHandler.GetPageRecord(instanceId);
        }

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
        public DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            return customWorkflowInstanceHandler.GetPageRecord(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
        }

        /// <summary>
        /// 获得工作流实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public IList<CustomWorkflowInstanceInfo> GetModelInfos(decimal userId, decimal workflowId)
        {
            return customWorkflowInstanceHandler.GetModelInfos(userId, workflowId);
        }

        #endregion
    }
}
