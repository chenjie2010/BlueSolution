//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataAuditingLogContract.cs
// 描述: DataAuditingLog 契约层接口
// 作者：ChenJie 
// 编写日期：2018/9/28
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
    /// DataAuditingLog 契约接口
    /// </summary>
    [ServiceContract(Name = "IDataAuditingLogContract", Namespace = "http://www.scu.edu.cn/BusinessDesignerModule/")]
    public interface IDataAuditingLogContract :  IPrincipalContracts<DataAuditingLogInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 终审完成
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="commment"></param>
        [OperationContract(Name = "CompleteBusiness")]
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
        [OperationContract(Name = "SubmitBusinessToNextStep")]
        void SubmitBusinessToNextStep(decimal dataAuditingLogId, AuditingStatus auditingStatus,
            decimal userId, decimal nextReviewerId, AuditingAction auditingAction, string commment);

        /// <summary>
        /// 获得待审核记录数
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataAuditingCount")]
        int GetDataAuditingCount(IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得待审核数据
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataAuditing")]
        DataSet GetDataAuditing(int startPosition, int count, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="commment"></param>
        [OperationContract(Name = "Reject")]
        void Reject(decimal dataAuditingLogId, decimal userId, AuditingStatus auditingStatus, string commment);

        /// <summary>
        /// 撤回
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="auditingStatus"></param>
        [OperationContract(Name = "WithDraw")]
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
        [OperationContract(Name = "GetPageRecord")]
        DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        #endregion
    }
}