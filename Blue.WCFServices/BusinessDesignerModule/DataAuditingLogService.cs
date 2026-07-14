//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingLogService.cs
// 描述: DataAuditingLog 操作服务类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessDesignerModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.DataAuditingLog.
    /// </summary>
    public class DataAuditingLogService : IDataAuditingLogContract
    {
        #region 业务实例
        
        private static readonly IDataAuditingLogHandler dataAuditingLogHandler = BusinessLogicContainer.Instance.BusinessDesignerModuleContainer.Resolve<IDataAuditingLogHandler>();

        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public DataAuditingLogService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 DataAuditingLog 表中插入一条新记录
		/// </summary>
		/// <param name="dataAuditingLogInfo"></param>
		/// <returns></returns>
		public decimal Insert(DataAuditingLogInfo dataAuditingLogInfo)
		{
            return dataAuditingLogHandler.Insert(dataAuditingLogInfo);
		}
        
        /// <summary>
		/// 获得 DataAuditingLogInfo 对象
		/// </summary>
		///<param name="auditingLogId">审核日志编号</param>
		/// <returns> DataAuditingLogInfo 对象</returns>
		public DataAuditingLogInfo GetModelInfo(decimal auditingLogId)
		{	
            return dataAuditingLogHandler.GetModelInfo(auditingLogId);           
		}		
		
        /// <summary>
		/// 更新 DataAuditingLogInfo 对象
		/// </summary>
		/// <param name="dataAuditingLogInfo">DataAuditingLogInfo 对象</param>
		public void Update(DataAuditingLogInfo dataAuditingLogInfo)
		{	          
            dataAuditingLogHandler.Update(dataAuditingLogInfo);
        }	
  
        /// <summary>
		/// 删除 DataAuditingLogInfo 对象
		/// </summary>
		///<param name="auditingLogId">审核日志编号</param>
		/// <returns> DataAuditingLogInfo 对象</returns>
		public void Delete(decimal auditingLogId)
		{	
            dataAuditingLogHandler.Delete(auditingLogId);
        }
        
        /// <summary>
        /// 获得 DataAuditingLogInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataAuditingLogInfo 对象列表</returns>
        public IList<DataAuditingLogInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return dataAuditingLogHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 DataAuditingLog 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> DataAuditingLogInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return dataAuditingLogHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 终审完成
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="commment"></param>
        public void CompleteBusiness(decimal dataAuditingLogId, decimal userId, string commment)
        {
            dataAuditingLogHandler.CompleteBusiness(dataAuditingLogId, userId, commment);
        }

        /// <summary>
        /// 提交到下一步
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="userId"></param>
        /// <param name="nextReviewerId"></param>
        /// <param name="auditingAction"></param>
        /// <param name="commment"></param>
        public void SubmitBusinessToNextStep(decimal dataAuditingLogId, AuditingStatus auditingStatus,
            decimal userId, decimal nextReviewerId, AuditingAction auditingAction, string commment)
        {
            dataAuditingLogHandler.SubmitBusinessToNextStep(dataAuditingLogId, auditingStatus, userId, nextReviewerId, auditingAction, commment);
        }

        /// <summary>
        /// 获得待审核记录数
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetDataAuditingCount(IList<WhereConditon> whereConditons)
        {
            return dataAuditingLogHandler.GetDataAuditingCount(whereConditons);
        }

        /// <summary>
        /// 获得待审核数据
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public DataSet GetDataAuditing(int startPosition, int count, IList<WhereConditon> whereConditons)
        {
            return dataAuditingLogHandler.GetDataAuditing(startPosition, count, whereConditons);
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="commment"></param>
        public void Reject(decimal dataAuditingLogId, decimal userId, AuditingStatus auditingStatus, string commment)
        {
            dataAuditingLogHandler.Reject(dataAuditingLogId, userId, auditingStatus, commment);
        }

        /// <summary>
        /// 撤回
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="auditingStatus"></param>
        public void WithDraw(decimal dataAuditingLogId, decimal userId, AuditingStatus auditingStatus)
        {
            dataAuditingLogHandler.WithDraw(dataAuditingLogId, userId, auditingStatus);
        }

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
        public DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            return dataAuditingLogHandler.GetPageRecord(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
        }
        #endregion
    }
}
