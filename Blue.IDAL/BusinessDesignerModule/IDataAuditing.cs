//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataAuditing.cs
// 描述: DataAuditing 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/9/7
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.BusinessDesignerModule;

namespace Blue.IDAL.BusinessDesignerModule
{
    /// <summary>
    /// DataAuditing 接口
    /// </summary>
    public interface IDataAuditing : ICommonNode, IPrincipalTable<DataAuditingInfo>
    {
        #region 接口

        /// <summary>
        /// 保存上传的文件
        /// </summary>
        /// <param name="upLoadFileInfo"></param>
        /// <param name="subDir"></param>
        void SaveUploadFiles(UpLoadFileInfo upLoadFileInfo, string subDir);

        /// <summary>
        /// 通过组合表编号查询个人信息更新的数量
        /// </summary>
        /// <param name="combinedTableId">组合表编号</param>
        /// <returns>记录数目</returns>
        int GetTotalCountByCombinedTableId(decimal combinedTableId);

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        DataAuditingInfo GetModelInfoByLogId(decimal auditingLogId);

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        DataAuditingInfo GetParentDataAuditingInfoByLogId(decimal auditingLogId);

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingId"></param>
        /// <returns></returns>
        DataAuditingInfo GetParentDataAuditingInfo(decimal auditingId);

        /// <summary>
        /// 获得个人信息审核依赖的个人信息编号
        /// </summary>
        ///<param name="dataAuditingId">个人信息审核编号</param>
        /// <returns> 个人信息审核依赖的个人信息编号</returns>
        decimal GetParentDataAuditingId(decimal dataAuditingId);

        /// <summary>
        /// 获得关联的信息变更对象编号
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        IList<decimal> GetDataAuditingIds(decimal parentDataAuditingId);

        /// <summary>
        /// 获得关联的信息变更对象
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        IList<CommonNode> GetDataAuditings(decimal parentDataAuditingId);

        /// <summary>
        /// 处理记录并记录日志
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="description"></param>
        /// <param name="dataAuditingStepInfo"></param>
        List<RecordItem> ProcessWithLog(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, DataAuditingLogInfo dataAuditingLogInfo, DataAuditingStepInfo dataAuditingStepInfo);

        /// <summary>
        /// 处理记录并记录日志
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="dataAuditingLogInfo"></param>
        /// <param name="dataAuditingStepInfo"></param>
        /// <returns></returns>
        List<RecordItem> Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, DataAuditingLogInfo dataAuditingLogInfo, DataAuditingStepInfo dataAuditingStepInfo);

        /// <summary>
        /// 获取当前初审的评审人列表
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Dictionary<decimal, string> GetInitReviewers(decimal dataAuditingId, decimal userId);

        /// <summary>
        /// 获取当前终审的评审人列表
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        Dictionary<decimal, string> GetFinalReviewers(decimal dataAuditingId);

        /// <summary>
        /// 完全更新记录(多表联合查询使用)
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordId">记录编号</param>
        /// <param name="commonDataFields">被更新的字段</param>        
        /// <param name="queryBuilder">查询条件</param>
        /// <param name="whereConditons">附加查询条件</param>
        void Update(decimal tableId, decimal recordId, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordId">记录编号</param>
        /// <param name="commonDataFields">更新的字段集合</param>
        /// <param name="relaitonDataFields">关联的字段集合</param>
        void Update(decimal tableId, decimal recordId, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordIds">记录编号集合</param>
        /// <param name="commonDataFields">更新的字段集合</param>
        /// <param name="relaitonDataFields">关联的字段集合</param>
        void Update(decimal tableId, IList<decimal> recordIds, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields);

        /// <summary>
        /// 更新当前表中的字段（不更新其他表的关联字段和联动字段）
        /// </summary>
        /// <param name="recordEntity"></param>
        /// <param name="whereConditons"></param>
        void Update(RecordEntity recordEntity, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dicRecordEntities"></param>
        /// <returns></returns>
        List<RecordItem> Process(decimal userId, IList<RecordEntity> recordEntities);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="dicRecordEntities"></param>
        /// <returns></returns>
        List<RecordItem> Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="dicRecordEntities"></param>
        /// <returns></returns>
        Dictionary<decimal, List<RecordItem>> Process(CommonUserInfo commonUserInfo, Dictionary<decimal, List<RecordEntity>> dicRecordEntities);

        /// <summary>
        /// 更新主从表(启用业务模式)中的当前状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="instanceId"></param>
        void UpdateCurretStateByInstanceId(decimal tableId, decimal recordId, decimal instanceId);

        /// <summary>
        /// 更新主从表(未启用业务模式)中的当前状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="userId"></param>
        void UpdateCurretStateByUserId(decimal tableId, decimal recordId, decimal userId);

        #endregion
    }
}