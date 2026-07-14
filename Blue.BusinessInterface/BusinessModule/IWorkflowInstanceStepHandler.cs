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
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.BusinessInterface.BusinessModule
{
/// <summary>
    /// WorkflowInstanceStep 接口
    /// </summary>
    public interface IWorkflowInstanceStepHandler: IPrincipalBusiness<WorkflowInstanceStepInfo>
    {
        #region 接口

        /// <summary>
        /// 获得工作流审核人数据
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal instanceId);

        /// <summary>
        /// 根据条件获得下一个审核人
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="reviewerIds"></param>
        /// <param name="workflowHandlerMode"></param>
        /// <returns></returns>
        decimal GetNextReviewerId(decimal processId, IList<decimal> reviewerIds, WorkflowHandlerMode workflowHandlerMode);

        /// <summary>
        /// 获得驳回对象列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        IList<CommonNode> GetRejectTargets(decimal stepId);

        /// <summary>
        /// 驳回工作流
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="logIds"></param>
        /// <param name="processId"></param>
        /// <param name="instanceId"></param>
        /// <param name="workflowInstanceLogInfos"></param>
        void RejectWorkflowInstance(decimal stepId, IList<decimal> logIds, decimal processId, decimal instanceId,
            IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos);

        /// <summary>
        /// 获得表 WorkflowInstance  的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetWorkflowInstanceAudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 获得表 WorkflowInstance  的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetWorkflowInstanceUnaudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        #endregion
    }
}
