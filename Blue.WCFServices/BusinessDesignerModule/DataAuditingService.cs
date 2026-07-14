//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingService.cs
// 描述: DataAuditing 操作服务类
// 作者：ChenJie 
// 编写日期：2018/9/7
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessDesignerModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.DataAuditing.
    /// </summary>
    public class DataAuditingService : CommonNodeServices, IDataAuditingContract
    {
        #region 业务实例

        private static readonly IDataAuditingHandler dataAuditingHandler = BusinessLogicContainer.Instance.BusinessDesignerModuleContainer.Resolve<IDataAuditingHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataAuditingService() : base(dataAuditingHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 DataAuditing 表中插入一条新记录
        /// </summary>
        /// <param name="dataAuditingInfo"></param>
        /// <returns></returns>
        public decimal Insert(DataAuditingInfo dataAuditingInfo)
        {
            return dataAuditingHandler.Insert(dataAuditingInfo);
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        ///<param name="dataAuditingId"></param>
        /// <returns> DataAuditingInfo 对象</returns>
        public DataAuditingInfo GetModelInfo(decimal dataAuditingId)
        {
            return dataAuditingHandler.GetModelInfo(dataAuditingId);
        }

        /// <summary>
        /// 更新 DataAuditingInfo 对象
        /// </summary>
        /// <param name="dataAuditingInfo">DataAuditingInfo 对象</param>
        public void Update(DataAuditingInfo dataAuditingInfo)
        {
            dataAuditingHandler.Update(dataAuditingInfo);
        }

        /// <summary>
        /// 删除 DataAuditingInfo 对象
        /// </summary>
        ///<param name="dataAuditingId"></param>
        /// <returns> DataAuditingInfo 对象</returns>
        public void Delete(decimal dataAuditingId)
        {
            dataAuditingHandler.Delete(dataAuditingId);
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataAuditingInfo 对象列表</returns>
        public IList<DataAuditingInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return dataAuditingHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 DataAuditing 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> DataAuditingInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return dataAuditingHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 保存上传的文件
        /// </summary>
        /// <param name="upLoadFileInfo"></param>
        /// <param name="subDir"></param>
        public void SaveUploadFiles(UpLoadFileInfo upLoadFileInfo, string subDir)
        {
            dataAuditingHandler.SaveUploadFiles(upLoadFileInfo, subDir);
        }

        /// <summary>
        /// 通过组合表编号查询个人信息更新的数量
        /// </summary>
        /// <param name="combinedTableId">组合表编号</param>
        /// <returns>记录数目</returns>
        public int GetTotalCountByCombinedTableId(decimal combinedTableId)
        {
            return dataAuditingHandler.GetTotalCountByCombinedTableId(combinedTableId);
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public DataAuditingInfo GetModelInfoByLogId(decimal auditingLogId)
        {
            return dataAuditingHandler.GetModelInfoByLogId(auditingLogId);
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public DataAuditingInfo GetParentDataAuditingInfoByLogId(decimal auditingLogId)
        {
            return dataAuditingHandler.GetParentDataAuditingInfoByLogId(auditingLogId);
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingId"></param>
        /// <returns></returns>
        public DataAuditingInfo GetParentDataAuditingInfo(decimal auditingId)
        {
            return dataAuditingHandler.GetParentDataAuditingInfo(auditingId);
        }

        /// <summary>
        /// 获取当前初审的评审人列表
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetInitReviewers(decimal dataAuditingId, decimal userId)
        {
            return dataAuditingHandler.GetInitReviewers(dataAuditingId, userId);
        }

        /// <summary>
        /// 获取当前终审的评审人列表
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetFinalReviewers(decimal dataAuditingId)
        {
            return dataAuditingHandler.GetFinalReviewers(dataAuditingId);
        }

        /// <summary>
        /// 获得组合表的字段
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDataAuditings(decimal parentDataAuditingId)
        {
            return dataAuditingHandler.GetDataAuditings(parentDataAuditingId);
        }

        /// <summary>
        /// 获得组合表的字段
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDataFields(decimal dataAuditingId)
        {
            return dataAuditingHandler.GetDataFields(dataAuditingId);
        }

        /// <summary>
        /// 更新组合表的字段集合
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <param name="dataAuditingAndDataFieldInfos"></param>
        public void UpdateDataFields(decimal dataAuditingId, IList<DataAuditingAndDataFieldInfo> dataAuditingAndDataFieldInfos)
        {
            dataAuditingHandler.UpdateDataFields(dataAuditingId, dataAuditingAndDataFieldInfos);
        }

        /// <summary>
        /// 完全更新记录(多表联合查询使用)
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordId">记录编号</param>
        /// <param name="commonDataFields">被更新的字段</param>        
        /// <param name="queryBuilder">查询条件</param>
        /// <param name="whereConditons">附加查询条件</param>
        public void Update(decimal tableId, decimal recordId, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons)
        {
            dataAuditingHandler.Update(tableId, recordId, commonDataFields, relaitonDataFields, queryBuilder, whereConditons);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordId">记录编号</param>
        /// <param name="commonDataFields">更新的字段集合</param>
        /// <param name="relaitonDataFields">关联的字段集合</param>
        public void Update(decimal tableId, decimal recordId, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields)
        {
            dataAuditingHandler.Update(tableId, recordId, commonDataFields, relaitonDataFields);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordIds">记录编号集合</param>
        /// <param name="commonDataFields">更新的字段集合</param>
        /// <param name="relaitonDataFields">关联的字段集合</param>
        public void Update(decimal tableId, IList<decimal> recordIds, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields)
        {
            dataAuditingHandler.Update(tableId, recordIds, commonDataFields, relaitonDataFields);
        }

        /// <summary>
        /// 更新当前表中的字段（不更新其他表的关联字段和联动字段）
        /// </summary>
        /// <param name="recordEntity"></param>
        /// <param name="whereConditons"></param>
        public void Update(RecordEntity recordEntity, IList<WhereConditon> whereConditons)
        {
            dataAuditingHandler.Update(recordEntity, whereConditons);
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="recordEntities"></param>
        public void Process(decimal userId, IList<RecordEntity> recordEntities)
        {
            dataAuditingHandler.Process(userId, recordEntities);
        }

        /// <summary>
        /// 更新数据表与字段
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        public void Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities)
        {
            dataAuditingHandler.Process(commonUserInfo, recordEntities);
        }

        /// <summary>
        /// 处理记录并记录日志
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="description"></param>
        /// <param name="dataAuditingStepInfo"></param>
        public void ProcessWithLog(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, DataAuditingLogInfo dataAuditingLogInfo, DataAuditingStepInfo dataAuditingStepInfo)
        {
            dataAuditingHandler.ProcessWithLog(commonUserInfo, recordEntities, dataAuditingLogInfo, dataAuditingStepInfo);
        }

        /// <summary>
        /// 处理记录并记录日志
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="dataAuditingLogInfo"></param>
        /// <param name="dataAuditingStepInfo"></param>
        public void Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, DataAuditingLogInfo dataAuditingLogInfo, DataAuditingStepInfo dataAuditingStepInfo)
        {
            dataAuditingHandler.Process(commonUserInfo, recordEntities, dataAuditingLogInfo, dataAuditingStepInfo);
        }

        /// <summary>
        /// 更新主从表(启用业务模式)中的当前状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="instanceId"></param>
        public void UpdateCurretStateByInstanceId(decimal tableId, decimal recordId, decimal instanceId)
        {
            dataAuditingHandler.UpdateCurretStateByInstanceId(tableId, recordId, instanceId);
        }

        /// <summary>
        /// 更新主从表(未启用业务模式)中的当前状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="userId"></param>
        public void UpdateCurretStateByUserId(decimal tableId, decimal recordId, decimal userId)
        {
            dataAuditingHandler.UpdateCurretStateByUserId(tableId, recordId, userId);
        }

        #endregion
    }
}
