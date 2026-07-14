//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：WorkflowInstanceStepHandler.cs
// 描述：WorkflowInstanceStep 业务处理类
// 作者：ChenJie 
// 编写日期：2018/4/23
// Copyright 2018
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
    /// 业务层处理类，对于的表： dbo.WorkflowInstanceStep.
    /// </summary>
    public class WorkflowInstanceStepHandler : IWorkflowInstanceStepHandler
    {
        #region 工厂类实例
        
        private static readonly IWorkflowInstanceStep dalWorkflowInstanceStep = BusinessDataAccessFactory.CreateWorkflowInstanceStep(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public WorkflowInstanceStepHandler()
		{
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 workflowinstancestep 表中插入一条新记录
		/// </summary>
		/// <param name="workflowInstanceStepInfo"></param>
		/// <returns></returns>
		public decimal Insert(WorkflowInstanceStepInfo workflowInstanceStepInfo)
		{
            //自动增加的关键字的值
			decimal workflowInstanceStepId = 0;
            
			// 验证输入
			if (workflowInstanceStepInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                workflowInstanceStepId = dalWorkflowInstanceStep.Insert(workflowInstanceStepInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return workflowInstanceStepId;
		}
        
        /// <summary>
		/// 获得 WorkflowInstanceStepInfo 对象
		/// </summary>
		///<param name="stepId">步骤编号</param>
		/// <returns> WorkflowInstanceStepInfo 对象</returns>
		public WorkflowInstanceStepInfo GetModelInfo(decimal stepId)
		{			
			WorkflowInstanceStepInfo  workflowInstanceStepInfo = null;
            
			// 验证输入
			if(stepId < 0)
            {
				return null;
            }

            try
            {
                workflowInstanceStepInfo =  dalWorkflowInstanceStep.GetModelInfo(stepId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return workflowInstanceStepInfo;
		}        
        
        /// <summary>
		/// 更新 WorkflowInstanceStepInfo 对象
		/// </summary>
		/// <param name="workflowInstanceStepInfo">WorkflowInstanceStepInfo 对象</param>
		public void Update(WorkflowInstanceStepInfo workflowInstanceStepInfo)
		{	
            // 验证输入
            if (workflowInstanceStepInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalWorkflowInstanceStep.Update(workflowInstanceStepInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 WorkflowInstanceStepInfo 对象
		/// </summary>
		///<param name="stepId">步骤编号</param>
		/// <returns> WorkflowInstanceStepInfo 对象</returns>
		public void Delete(decimal stepId)
		{		
            // 验证输入
			if(stepId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalWorkflowInstanceStep.Delete(stepId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 WorkflowInstanceStepInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>WorkflowInstanceStepInfo 对象列表</returns>
		public IList<WorkflowInstanceStepInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<WorkflowInstanceStepInfo>  workflowInstanceStepInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                workflowInstanceStepInfos = dalWorkflowInstanceStep.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return workflowInstanceStepInfos;
		}               
        
        /// <summary>
		/// 获得 WorkflowInstanceStep 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>WorkflowInstanceStepInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalWorkflowInstanceStep.GetTotalCount(whereConditons);
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
        /// 获得工作流审核人数据
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal instanceId)
        {
            DataSet ds = null;

            // 验证输入
            if (instanceId <= 0)
            {
                throw new ArgumentException("工作流实例编号不能小于或是等于0。");
            }

            try
            {
                ds = dalWorkflowInstanceStep.GetPageRecord(instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 根据条件获得下一个审核人
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="reviewerIds"></param>
        /// <param name="workflowHandlerMode"></param>
        /// <returns></returns>
        public decimal GetNextReviewerId(decimal processId, IList<decimal> reviewerIds, WorkflowHandlerMode workflowHandlerMode)
        {
            decimal reviewerId = decimal.MinValue;

            // 验证输入
            if (processId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                reviewerId = dalWorkflowInstanceStep.GetNextReviewerId(processId, reviewerIds, workflowHandlerMode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return reviewerId;

        }

        /// <summary>
        /// 获得驳回对象列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRejectTargets(decimal stepId)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (stepId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalWorkflowInstanceStep.GetRejectTargets(stepId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return commonNodes;
        }

        /// <summary>
        /// 驳回工作流
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="logIds"></param>
        /// <param name="processId"></param>
        /// <param name="instanceId"></param>
        /// <param name="workflowInstanceLogInfos"></param>
        public void RejectWorkflowInstance(decimal stepId, IList<decimal> logIds, decimal processId, decimal instanceId,
            IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos)
        {
            // 验证输入
            if (stepId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalWorkflowInstanceStep.RejectWorkflowInstance(stepId, logIds, processId, instanceId, workflowInstanceLogInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得表 WorkflowInstance  的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetWorkflowInstanceAudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                ds = dalWorkflowInstanceStep.GetWorkflowInstanceAudited(startPosition, count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 WorkflowInstance  的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetWorkflowInstanceUnaudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                ds = dalWorkflowInstanceStep.GetWorkflowInstanceUnaudited(startPosition, count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
