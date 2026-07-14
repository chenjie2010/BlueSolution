//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingLogHandler.cs
// 描述: DataAuditingLog 业务处理类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.CustomLibrary;
using Blue.DALFactory;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;

namespace Blue.BusinessLogic.BusinessDesignerModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.DataAuditingLog.
    /// </summary>
    public class DataAuditingLogHandler : IDataAuditingLogHandler
    {
        #region 工厂类实例
        
        private static readonly IDataAuditingLog dalDataAuditingLog = BusinessDesignerDataAccessFactory.CreateDataAuditingLog(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public DataAuditingLogHandler()
		{  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 dataauditinglog 表中插入一条新记录
		/// </summary>
		/// <param name="dataAuditingLogInfo"></param>
		/// <returns></returns>
		public decimal Insert(DataAuditingLogInfo dataAuditingLogInfo)
		{
            //自动增加的关键字的值
			decimal dataAuditingLogId = 0;
            
			// 验证输入
			if (dataAuditingLogInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                dataAuditingLogId = dalDataAuditingLog.Insert(dataAuditingLogInfo);                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return dataAuditingLogId;
		}
        
        /// <summary>
		/// 获得 DataAuditingLogInfo 对象
		/// </summary>
		///<param name="auditingLogId">审核日志编号</param>
		/// <returns> DataAuditingLogInfo 对象</returns>
		public DataAuditingLogInfo GetModelInfo(decimal auditingLogId)
		{			
			DataAuditingLogInfo  dataAuditingLogInfo = null;
            
			// 验证输入
			if(auditingLogId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataAuditingLogInfo =  dalDataAuditingLog.GetModelInfo(auditingLogId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return dataAuditingLogInfo;
		}        
        
        /// <summary>
		/// 更新 DataAuditingLogInfo 对象
		/// </summary>
		/// <param name="dataAuditingLogInfo">DataAuditingLogInfo 对象</param>
		public void Update(DataAuditingLogInfo dataAuditingLogInfo)
		{	
            // 验证输入
            if (dataAuditingLogInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalDataAuditingLog.Update(dataAuditingLogInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 DataAuditingLogInfo 对象
		/// </summary>
		///<param name="auditingLogId">审核日志编号</param>
		/// <returns> DataAuditingLogInfo 对象</returns>
		public void Delete(decimal auditingLogId)
		{		
            // 验证输入
			if(auditingLogId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalDataAuditingLog.Delete(auditingLogId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 DataAuditingLogInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataAuditingLogInfo  对象列表</returns>
		public IList<DataAuditingLogInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<DataAuditingLogInfo>  dataAuditingLogInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                dataAuditingLogInfos = dalDataAuditingLog.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return dataAuditingLogInfos;
		}               
        
        /// <summary>
		/// 获得 CustomSheet 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomSheetInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalDataAuditingLog.GetTotalCount(whereConditons);
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
        /// 根据条件统计信息变更数量
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public Dictionary<byte, int> GetStaticsByAuditingStatus(decimal userId)
        {
            Dictionary<byte, int> statics = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("DataAuditingLog", "UserId", "UserId", DbType.Decimal, userId,
                  DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            try
            {
                statics = dalDataAuditingLog.GetStaticsByAuditingStatus(whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return statics;
        }
        
        /// <summary>
        /// 获得审核描述
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public string GetLogDescription(decimal auditingLogId)
        {
            string logDescription = string.Empty;

            // 验证输入
            if (auditingLogId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                logDescription = dalDataAuditingLog.GetLogDescription(auditingLogId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return logDescription;
        }

        /// <summary>
        /// 终审完成
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="commment"></param>
        public void CompleteBusiness(decimal dataAuditingLogId, decimal userId, string commment)
        {
            // 验证输入
            if (dataAuditingLogId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalDataAuditingLog.CompleteBusiness(dataAuditingLogId, userId, commment);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            // 验证输入
            if (dataAuditingLogId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalDataAuditingLog.SubmitBusinessToNextStep(dataAuditingLogId, auditingStatus, 
                    userId, nextReviewerId, auditingAction, commment);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
    
        /// <summary>
        /// 获得待审核记录数
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetDataAuditingCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                count = dalDataAuditingLog.GetDataAuditingCount(whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
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
            DataSet ds = null;

            try
            {
                ds = dalDataAuditingLog.GetDataAuditing(startPosition, count, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
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
            // 验证输入
            if (dataAuditingLogId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalDataAuditingLog.Reject(dataAuditingLogId, userId, auditingStatus, commment);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 撤回
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="auditingStatus"></param>
        public void WithDraw(decimal dataAuditingLogId, decimal userId, AuditingStatus auditingStatus)
        {
            // 验证输入
            if (dataAuditingLogId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalDataAuditingLog.WithDraw(dataAuditingLogId, userId, auditingStatus);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            DataSet ds = null;

            try
            {
                ds = dalDataAuditingLog.GetPageRecord(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
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
