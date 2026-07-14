//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowInstanceHandler.cs
// 描述：CustomWorkflowInstance 业务处理类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomWorkflowInstance.
    /// </summary>
    public class CustomWorkflowInstanceHandler : ICustomWorkflowInstanceHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomWorkflowInstance dalCustomWorkflowInstance = BusinessDataAccessFactory.CreateCustomWorkflowInstance();
        private static readonly IWorkflowInstanceStep dalWorkflowInstanceStep = BusinessDataAccessFactory.CreateWorkflowInstanceStep();
        private static readonly IWorkflowInstanceLog dalWorkflowInstanceLog = BusinessDataAccessFactory.CreateWorkflowInstanceLog();
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowInstanceHandler()
		{
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customworkflowinstance 表中插入一条新记录
		/// </summary>
		/// <param name="customWorkflowInstanceInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomWorkflowInstanceInfo customWorkflowInstanceInfo)
		{
            //自动增加的关键字的值
			decimal customWorkflowInstanceId = 0;
            
			// 验证输入
			if (customWorkflowInstanceInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                customWorkflowInstanceId = dalCustomWorkflowInstance.Insert(customWorkflowInstanceInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customWorkflowInstanceId;
		}
        
        /// <summary>
		/// 获得 CustomWorkflowInstanceInfo 对象
		/// </summary>
		///<param name="instanceId">工作流实例编号</param>
		/// <returns> CustomWorkflowInstanceInfo 对象</returns>
		public CustomWorkflowInstanceInfo GetModelInfo(decimal instanceId)
		{			
			CustomWorkflowInstanceInfo  customWorkflowInstanceInfo = null;
            
			// 验证输入
			if(instanceId < 0)
            {
				return null;
            }

            try
            {
                customWorkflowInstanceInfo =  dalCustomWorkflowInstance.GetModelInfo(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customWorkflowInstanceInfo;
		}        
        
        /// <summary>
		/// 更新 CustomWorkflowInstanceInfo 对象
		/// </summary>
		/// <param name="customWorkflowInstanceInfo">CustomWorkflowInstanceInfo 对象</param>
		public void Update(CustomWorkflowInstanceInfo customWorkflowInstanceInfo)
		{	
            // 验证输入
            if (customWorkflowInstanceInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalCustomWorkflowInstance.Update(customWorkflowInstanceInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomWorkflowInstanceInfo 对象
		/// </summary>
		///<param name="instanceId">工作流实例编号</param>
		/// <returns> CustomWorkflowInstanceInfo 对象</returns>
		public void Delete(decimal instanceId)
		{		
            // 验证输入
			if(instanceId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomWorkflowInstance.Delete(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 CustomWorkflowInstanceInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowInstanceInfo 对象列表</returns>
		public IList<CustomWorkflowInstanceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomWorkflowInstanceInfo>  customWorkflowInstanceInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customWorkflowInstanceInfos = dalCustomWorkflowInstance.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customWorkflowInstanceInfos;
		}               
        
        /// <summary>
		/// 获得 CustomWorkflowInstance 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomWorkflowInstanceInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomWorkflowInstance.GetTotalCount(whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
		}

        #endregion

        #region 自定义方法        

        /// <summary>
        /// 获得工作流归档的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public bool GetInstanceArchivedStatus(decimal instanceId)
        {
            bool isArchived = false;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("工作流实例编号不能小于或是等于0。");
            }

            try
            {
                isArchived = dalCustomWorkflowInstance.GetInstanceArchivedStatus(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isArchived;
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
            try
            {
                dalCustomWorkflowInstance.ArchiveWorkflowInstance(whereConditons, isArchived, archivedUserName, archivedName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            try
            {
                dalCustomWorkflowInstance.ArchiveWorkflowInstance(instanceIds, isArchived, archivedUserName, archivedName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("工作流实例编号不能小于或是等于0。");
            }

            try
            {
                dalCustomWorkflowInstance.ArchiveWorkflowInstance(instanceId, isArchived, archivedUserName, archivedName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 初始化工作流实例
        /// </summary>
        /// <param name="instanceId"></param>
        public void InitWorkWorkflowInstance(decimal instanceId)
        {
            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("工作流实例编号不能小于或是等于0。");
            }

            try
            {
                dalCustomWorkflowInstance.InitWorkWorkflowInstance(instanceId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 终止工作流
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public AbortedResult AbortWorkflowInstance(decimal instanceId)
        {
            AbortedResult abortedResult = AbortedResult.Success;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("工作流实例编号不能小于或是等于0。");
            }

            try
            {
                abortedResult = dalCustomWorkflowInstance.AbortWorkflowInstance(instanceId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return abortedResult;
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
            DataSet dataSet = null;

            try
            {
                dataSet = dalCustomWorkflowInstance.GetWorkflowInstances(startPosition, count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataSet;
        }

        /// <summary>
        /// 是否允许工作流撤回
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public WithdrawedResult IsUserWorkflowInstanceWithDrawed(decimal instanceId)
        {
            WithdrawedResult withdrawedResult = WithdrawedResult.Success;

            try
            {
                withdrawedResult = dalCustomWorkflowInstance.IsUserWorkflowInstanceWithDrawed(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return withdrawedResult;
        }

        /// <summary>
        /// 发起人撤回工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        public WithdrawedResult UserWithdrawWorkflowInstance(decimal instanceId, WorkflowInstanceLogInfo workflowInstanceLogInfo)
        {
            WithdrawedResult withdrawedResult = WithdrawedResult.Success;

            try
            {
                withdrawedResult = dalCustomWorkflowInstance.UserWithdrawWorkflowInstance(instanceId, workflowInstanceLogInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return withdrawedResult;
        }        

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="stepId">处理步骤编号</param>
        /// <returns> 最新的审核意见</returns>
        public Dictionary<string, string> GetComments(decimal stepId)
        {
            Dictionary<string, string> comments = null;

            try
            {
                comments = dalWorkflowInstanceLog.GetComments(stepId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return comments;
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
            WorkflowInstanceLogInfo workflowInstanceLogInfo = null;

            try
            {
                workflowInstanceLogInfo = dalWorkflowInstanceLog.GetDraftLog(processId, instanceId, parentUserId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return workflowInstanceLogInfo;
        }

        /// <summary>
        /// 获得日志的父用户编号
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public decimal GetParentUserId(decimal logId)
        {
            decimal parentUserId = 0;

            try
            {
                parentUserId = dalWorkflowInstanceLog.GetParentUserId(logId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return parentUserId;
        }

        /// <summary>
        /// 获得工作流实例的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public InstanceStatus GetInstanceStatus(decimal instanceId)
        {
            InstanceStatus instanceStatus = InstanceStatus.None;

            try
            {
                instanceStatus = dalCustomWorkflowInstance.GetInstanceStatus(instanceId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceStatus;
        }

        /// <summary>
        /// 获得流程编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public decimal GetProcessId(decimal logId)
        {
            decimal processId = 0;

            try
            {
                processId = dalWorkflowInstanceLog.GetProcessId(logId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return processId;
        }

        /// <summary>
        /// 获得日志的用户编号
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public decimal GetUserId(decimal logId)
        {
            decimal userId = 0;

            try
            {
                userId = dalWorkflowInstanceLog.GetUserId(logId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userId;
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核人编号
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核人信息</returns>
        public Dictionary<decimal, string> GetLastestReviewers(decimal instanceId)
        {
            Dictionary<decimal, string> lastestReviewers = null;

            try
            {
                lastestReviewers = dalWorkflowInstanceStep.GetLastestReviewers(instanceId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return lastestReviewers;
        }

        /// <summary>
        /// 是否允许工作流撤回
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public WithdrawedResult IsWorkflowInstanceWithDrawed(decimal stepId)
        {
            WithdrawedResult withdrawedResult = WithdrawedResult.Success;

            // 验证输入
            if (stepId <= 0)
            {
                throw new ArgumentException("工作流实例编号或步骤编号不能小于或是等于0。");
            }

            try
            {
                withdrawedResult = dalCustomWorkflowInstance.IsWorkflowInstanceWithDrawed(stepId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return withdrawedResult;
        }

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        public WithdrawedResult WithdrawWorkflowInstance(decimal stepId, WorkflowInstanceLogInfo workflowInstanceLogInfo)
        {
            WithdrawedResult withdrawedResult = WithdrawedResult.Success;

            // 验证输入
            if (stepId <= 0)
            {
                throw new ArgumentException("工作流实例编号或步骤编号不能小于或是等于0。");
            }

            try
            {
                withdrawedResult = dalCustomWorkflowInstance.WithdrawWorkflowInstance(stepId, workflowInstanceLogInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return withdrawedResult;
        }

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public AbortedResult AbortWorkflowInstance(decimal stepId, string comment)
        {
            AbortedResult abortedResult = AbortedResult.Success;

            // 验证输入
            if (stepId <= 0)
            {
                throw new ArgumentException("步骤编号不能小于或是等于0。");
            }

            try
            {
                abortedResult = dalCustomWorkflowInstance.AbortWorkflowInstance(stepId, comment);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return abortedResult;
        }

        /// <summary>
        /// 处理工作流提交的数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public InstanceItem Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, WorkflowInstanceLogInfo workflowInstanceLogInfo, IList<RecordEntity> recordEntities)
        {
            InstanceItem instanceItem = null;

            try
            {
                instanceItem = dalCustomWorkflowInstance.Process(customWorkflowInstanceInfo, workflowInstanceLogInfo, recordEntities);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceItem;
        }

        /// <summary>
        /// 处理工作流提交的数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="dicRecordEntities"></param>
        /// <returns></returns>
        public InstanceSet Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, WorkflowInstanceLogInfo workflowInstanceLogInfo, Dictionary<decimal, List<RecordEntity>> dicRecordEntities)
        {
            InstanceSet instanceSet = null;

            try
            {
                instanceSet = dalCustomWorkflowInstance.Process(customWorkflowInstanceInfo, workflowInstanceLogInfo, dicRecordEntities);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceSet;
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
        public InstanceSet Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, decimal stepId, IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos,
            IList<WorkflowInstanceStepInfo> workflowInstanceStepInfos, Dictionary<decimal, List<RecordEntity>> dicRecordEntities)
        {
            InstanceSet instanceItem = null;

            try
            {
                instanceItem = dalCustomWorkflowInstance.Process(customWorkflowInstanceInfo, stepId, workflowInstanceLogInfos, workflowInstanceStepInfos, dicRecordEntities);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceItem;
        }
        
        /// <summary>
        /// 获得工作流实例的日志对象
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public WorkflowInstanceStepInfo GetWorkflowInstanceStepInfo(decimal stepId)
        {
            WorkflowInstanceStepInfo workflowInstanceStepInfo = null;

            // 验证输入
            if (stepId <= 0)
            {
                throw new ArgumentException("不能更新空对象.");
            }

            try
            {
                workflowInstanceStepInfo = dalWorkflowInstanceStep.GetModelInfo(stepId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return workflowInstanceStepInfo;
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
            int count = 0;

            try
            {
                count = dalCustomWorkflowInstance.GetWorkflowInstanceCount(userId, workflowId, instanceStatus);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }        

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核意见</returns>
        public string GetLastestComment(decimal instanceId)
        {
            string lastestComment = string.Empty;

            try
            {
                lastestComment = dalWorkflowInstanceLog.GetLastestComment(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return lastestComment;
        }

        /// <summary>
        /// 获得数据填报的处理流程
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal instanceId)
        {
            DataSet dataSet = null;

            try
            {
                dataSet = dalWorkflowInstanceLog.GetPageRecord(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataSet;
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
            DataSet dataSet = null;

            try
            {
                dataSet = dalCustomWorkflowInstance.GetPageRecord(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataSet;
        }

        /// <summary>
        /// 获得工作流实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public IList<CustomWorkflowInstanceInfo> GetModelInfos(decimal userId, decimal workflowId)
        {
            IList<CustomWorkflowInstanceInfo> customWorkflowInstanceInfos = null;

            try
            {
                customWorkflowInstanceInfos = dalCustomWorkflowInstance.GetModelInfos(userId, workflowId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowInstanceInfos;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
