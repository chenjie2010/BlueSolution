//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataAuditingLogHandler.cs
// 描述: DataAuditingLog 业务处理类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessDesignerModule;

namespace Blue.BusinessInterface.BusinessDesignerModule
{
    /// <summary>
    /// DataAuditingLog 接口
    /// </summary>
    public interface IDataAuditingLogHandler : IPrincipalBusiness<DataAuditingLogInfo>
    {
        #region 接口
        
        /// <summary>
        /// 根据条件统计信息变更数量
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        Dictionary<byte, int> GetStaticsByAuditingStatus(decimal userId);

        /// <summary>
        /// 获得审核描述
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        string GetLogDescription(decimal auditingLogId);

        /// <summary>
        /// 终审完成
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="commment"></param>
        void CompleteBusiness(decimal dataAuditingLogId, decimal userId, string commment);

        /// <summary>
        /// 提交到下一步
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="userId"></param>
        /// <param name="nextReviewerId"></param>
        /// <param name="auditingAction"></param>
        /// <param name="commment"></param>
        void SubmitBusinessToNextStep(decimal dataAuditingLogId, AuditingStatus auditingStatus,
            decimal userId, decimal nextReviewerId, AuditingAction auditingAction, string commment);

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="commment"></param>
        void Reject(decimal dataAuditingLogId, decimal userId, AuditingStatus auditingStatus, string commment);

        /// <summary>
        /// 获得待审核记录数
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        int GetDataAuditingCount(IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得待审核数据
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        DataSet GetDataAuditing(int startPosition, int count, IList<WhereConditon> whereConditons);
        
        /// <summary>
        /// 撤回
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="auditingStatus"></param>
        void WithDraw(decimal dataAuditingLogId, decimal userId, AuditingStatus auditingStatus);

        /// <summary>
        /// 获得表 DataAuditingLog 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        #endregion
    }
}