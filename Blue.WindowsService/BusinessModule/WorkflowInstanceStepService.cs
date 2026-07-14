//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：WorkflowInstanceStepService.cs
// 描述：WorkflowInstanceStep 操作服务类
// 作者：ChenJie 
// 编写日期：2018/4/23
// Copyright 2018
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
    /// 操作服务类，对于的表： dbo.WorkflowInstanceStep.
    /// </summary>
    public class WorkflowInstanceStepService : IWorkflowInstanceStepContract
    {
        #region 业务实例

        private static readonly IWorkflowInstanceStepHandler workflowInstanceStepHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<IWorkflowInstanceStepHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public WorkflowInstanceStepService()
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 workflowinstancestep 表中插入一条新记录
        /// </summary>
        /// <param name="workflowInstanceStepInfo"></param>
        /// <returns></returns>
        public decimal Insert(WorkflowInstanceStepInfo workflowInstanceStepInfo)
        {
            return workflowInstanceStepHandler.Insert(workflowInstanceStepInfo);
        }

        /// <summary>
        /// 获得 WorkflowInstanceStepInfo 对象
        /// </summary>
        ///<param name="stepId">步骤编号</param>
        /// <returns> WorkflowInstanceStepInfo 对象</returns>
        public WorkflowInstanceStepInfo GetModelInfo(decimal stepId)
        {
            return workflowInstanceStepHandler.GetModelInfo(stepId);
        }

        /// <summary>
        /// 更新 WorkflowInstanceStepInfo 对象
        /// </summary>
        /// <param name="workflowInstanceStepInfo">WorkflowInstanceStepInfo 对象</param>
        public void Update(WorkflowInstanceStepInfo workflowInstanceStepInfo)
        {
            workflowInstanceStepHandler.Update(workflowInstanceStepInfo);
        }

        /// <summary>
        /// 删除 WorkflowInstanceStepInfo 对象
        /// </summary>
        ///<param name="stepId">步骤编号</param>
        /// <returns> WorkflowInstanceStepInfo 对象</returns>
        public void Delete(decimal stepId)
        {
            workflowInstanceStepHandler.Delete(stepId);
        }

        /// <summary>
        /// 获得 WorkflowInstanceStepInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>WorkflowInstanceStepInfo 对象列表</returns>
        public IList<WorkflowInstanceStepInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return workflowInstanceStepHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 WorkflowInstanceStep 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>WorkflowInstanceStepInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return workflowInstanceStepHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得工作流审核人数据
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal instanceId)
        {
            return workflowInstanceStepHandler.GetPageRecord(instanceId);
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
            return workflowInstanceStepHandler.GetNextReviewerId(processId, reviewerIds, workflowHandlerMode);
        }

        /// <summary>
        /// 获得驳回对象列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRejectTargets(decimal stepId)
        {
            return workflowInstanceStepHandler.GetRejectTargets(stepId);
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
            workflowInstanceStepHandler.RejectWorkflowInstance(stepId, logIds, processId, instanceId, workflowInstanceLogInfos);
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
            return workflowInstanceStepHandler.GetWorkflowInstanceAudited(startPosition, count, whereConditons, ref totalCount);
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
            return workflowInstanceStepHandler.GetWorkflowInstanceUnaudited(startPosition, count, whereConditons, ref totalCount);
        }

        #endregion
    }
}
