//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataAuditingContract.cs
// 描述: DataAuditing 契约层接口
// 作者：ChenJie 
// 编写日期：2018/9/7
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessDesignerModule;

namespace Blue.WCFContracts.BusinessDesignerModule
{
    /// <summary>
    /// DataAuditing 契约接口
    /// </summary>
    [ServiceContract(Name = "IDataAuditingContract", Namespace = "http://www.scu.edu.cn/BusinessDesignerModule/")]
    public interface IDataAuditingContract : ICommonNodeContract, IPrincipalContracts<DataAuditingInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 保存上传的文件
        /// </summary>
        /// <param name="upLoadFileInfo"></param>
        /// <param name="subDir"></param>
        [OperationContract(Name = "SaveUploadFiles")]
        void SaveUploadFiles(UpLoadFileInfo upLoadFileInfo, string subDir);

        /// <summary>
        /// 通过组合表编号查询个人信息更新的数量
        /// </summary>
        /// <param name="combinedTableId">组合表编号</param>
        /// <returns>记录数目</returns>
        [OperationContract(Name = "GetTotalCountByCombinedTableId")]
        int GetTotalCountByCombinedTableId(decimal combinedTableId);

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfoByLogId")]
        DataAuditingInfo GetModelInfoByLogId(decimal auditingLogId);

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetParentDataAuditingInfoByLogId")]
        DataAuditingInfo GetParentDataAuditingInfoByLogId(decimal auditingLogId);

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetParentDataAuditingInfo")]
        DataAuditingInfo GetParentDataAuditingInfo(decimal auditingId);

        /// <summary>
        /// 处理记录并记录日志
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="description"></param>
        /// <param name="dataAuditingStepInfo"></param>
        [OperationContract(Name = "ProcessWithLog")]
        void ProcessWithLog(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, DataAuditingLogInfo dataAuditingLogInfo, DataAuditingStepInfo dataAuditingStepInfo);

        /// <summary>
        /// 获取当前初审的评审人列表
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetInitReviewers")]
        Dictionary<decimal, string> GetInitReviewers(decimal dataAuditingId, decimal userId);

        /// <summary>
        /// 获取当前终审的评审人列表
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetFinalReviewers")]
        Dictionary<decimal, string> GetFinalReviewers(decimal dataAuditingId);

        /// <summary>
        /// 获得组合表的字段
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataAuditings")]
        IList<CommonNode> GetDataAuditings(decimal parentDataAuditingId);
                
        /// <summary>
        /// 获得组合表的字段
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFields")]
        IList<CommonNode> GetDataFields(decimal dataAuditingId);

        /// <summary>
        /// 更新组合表的字段集合
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <param name="dataAuditingAndDataFieldInfos"></param>
        [OperationContract(Name = "UpdateDataFields")]
        void UpdateDataFields(decimal dataAuditingId, IList<DataAuditingAndDataFieldInfo> dataAuditingAndDataFieldInfos);

        /// <summary>
        /// 完全更新记录(多表联合查询使用)
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordId">记录编号</param>
        /// <param name="commonDataFields">被更新的字段</param>        
        /// <param name="queryBuilder">查询条件</param>
        /// <param name="whereConditons">附加查询条件</param>
        [ServiceKnownType(typeof(CommonDataField))]
        [ServiceKnownType(typeof(QueryFieldCollection))]
        [ServiceKnownType(typeof(QueryField))]
        [ServiceKnownType(typeof(UpLoadFileInfo))]
        [ServiceKnownType(typeof(DBNull))]
        [OperationContract(Name = "UpdateAllRecordsOnQuery")]
        void Update(decimal tableId, decimal recordId, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordId">记录编号</param>
        /// <param name="commonDataFields">更新的字段集合</param>
        /// <param name="relaitonDataFields">关联的字段集合</param>
        [ServiceKnownType(typeof(UpLoadFileInfo))]
        [ServiceKnownType(typeof(CommonDataField))]
        [ServiceKnownType(typeof(DBNull))]
        [OperationContract(Name = "UpdateRecordOnQuery")]
        void Update(decimal tableId, decimal recordId, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="recordIds">记录编号集合</param>
        /// <param name="commonDataFields">更新的字段集合</param>
        /// <param name="relaitonDataFields">关联的字段集合</param>
        [ServiceKnownType(typeof(UpLoadFileInfo))]
        [ServiceKnownType(typeof(CommonDataField))]
        [ServiceKnownType(typeof(DBNull))]
        [OperationContract(Name = "UpdateRecordsOnQuery")]
        void Update(decimal tableId, IList<decimal> recordIds, IList<CommonDataField> commonDataFields, IList<CommonDataField> relaitonDataFields);

        /// <summary>
        /// 更新当前表中的字段（不更新其他表的关联字段和联动字段）
        /// </summary>
        /// <param name="recordEntity"></param>
        /// <param name="whereConditons"></param>
        [ServiceKnownType(typeof(UpLoadFileInfo))]
        [ServiceKnownType(typeof(CommonDataField))]
        [ServiceKnownType(typeof(DBNull))]
        [OperationContract(Name = "UpdateByWhereConditons")]
        void Update(RecordEntity recordEntity, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="recordEntities"></param>
        [ServiceKnownType(typeof(UpLoadFileInfo))]
        [ServiceKnownType(typeof(CommonDataField))]
        [ServiceKnownType(typeof(DBNull))]
        [OperationContract(Name = "ProcessByUserId")]
        void Process(decimal userId, IList<RecordEntity> recordEntities);

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        [ServiceKnownType(typeof(UpLoadFileInfo))]
        [ServiceKnownType(typeof(CommonDataField))]
        [ServiceKnownType(typeof(DBNull))]
        [OperationContract(Name = "Process")]
        void Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities);

        /// <summary>
        /// 处理记录并记录日志
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="dataAuditingLogInfo"></param>
        /// <param name="dataAuditingStepInfo"></param>
        [ServiceKnownType(typeof(UpLoadFileInfo))]
        [ServiceKnownType(typeof(CommonDataField))]
        [ServiceKnownType(typeof(DBNull))]
        [OperationContract(Name = "ProcessWithLogAndSteps")]
        void Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, DataAuditingLogInfo dataAuditingLogInfo, DataAuditingStepInfo dataAuditingStepInfo);

        /// <summary>
        /// 更新主从表(启用业务模式)中的当前状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="instanceId"></param>
        [OperationContract(Name = "UpdateCurretStateByInstanceId")]
        void UpdateCurretStateByInstanceId(decimal tableId, decimal recordId, decimal instanceId);

        /// <summary>
        /// 更新主从表(未启用业务模式)中的当前状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="userId"></param>
        [OperationContract(Name = "UpdateCurretStateByUserId")]
        void UpdateCurretStateByUserId(decimal tableId, decimal recordId, decimal userId);

        #endregion
    }
}