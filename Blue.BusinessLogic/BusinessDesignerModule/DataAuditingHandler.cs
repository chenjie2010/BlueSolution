//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingHandler.cs
// 描述: DataAuditing 业务处理类
// 作者：ChenJie 
// 编写日期：2018/9/7
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.CustomLibrary;
using Blue.DALFactory;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;

namespace Blue.BusinessLogic.BusinessDesignerModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.DataAuditing.
    /// </summary>
    public class DataAuditingHandler : CommonNodeBusiness, IDataAuditingHandler
    {
        #region 工厂类实例
        
        private static readonly IDataAuditing dalDataAuditing = BusinessDesignerDataAccessFactory.CreateDataAuditing();
        private static readonly IDataAuditingAndDataField dataAuditingAndDataField = BusinessDesignerDataAccessFactory.CreateDataAuditingAndDataField();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataAuditingHandler() : base(dalDataAuditing)
        {  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 dataauditing 表中插入一条新记录
		/// </summary>
		/// <param name="dataAuditingInfo"></param>
		/// <returns></returns>
		public decimal Insert(DataAuditingInfo dataAuditingInfo)
		{
            //自动增加的关键字的值
			decimal dataAuditingId = 0;
            
			// 验证输入
			if (dataAuditingInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                dataAuditingId = dalDataAuditing.Insert(dataAuditingInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return dataAuditingId;
		}
        
        /// <summary>
		/// 获得 DataAuditingInfo 对象
		/// </summary>
		///<param name="dataAuditingId"></param>
		/// <returns> DataAuditingInfo 对象</returns>
		public DataAuditingInfo GetModelInfo(decimal dataAuditingId)
		{			
			DataAuditingInfo  dataAuditingInfo = null;
            
			// 验证输入
			if(dataAuditingId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataAuditingInfo =  dalDataAuditing.GetModelInfo(dataAuditingId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return dataAuditingInfo;
		}        
        
        /// <summary>
		/// 更新 DataAuditingInfo 对象
		/// </summary>
		/// <param name="dataAuditingInfo">DataAuditingInfo 对象</param>
		public void Update(DataAuditingInfo dataAuditingInfo)
		{	
            // 验证输入
            if (dataAuditingInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalDataAuditing.Update(dataAuditingInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 DataAuditingInfo 对象
		/// </summary>
		///<param name="dataAuditingId"></param>
		/// <returns> DataAuditingInfo 对象</returns>
		public void Delete(decimal dataAuditingId)
		{		
            // 验证输入
			if(dataAuditingId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalDataAuditing.Delete(dataAuditingId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 DataAuditingInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataAuditingInfo  对象列表</returns>
		public IList<DataAuditingInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<DataAuditingInfo>  dataAuditingInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                dataAuditingInfos = dalDataAuditing.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return dataAuditingInfos;
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
                count = dalDataAuditing.GetTotalCount(whereConditons);
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
        /// 保存上传的文件
        /// </summary>
        /// <param name="upLoadFileInfo"></param>
        /// <param name="subDir"></param>
        public void SaveUploadFiles(UpLoadFileInfo upLoadFileInfo, string subDir)
        {
            try
            {
                dalDataAuditing.SaveUploadFiles(upLoadFileInfo, subDir);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 通过组合表编号查询个人信息更新的数量
        /// </summary>
        /// <param name="combinedTableId">组合表编号</param>
        /// <returns>记录数目</returns>
        public int GetTotalCountByCombinedTableId(decimal combinedTableId)
        {
            int count = 0;

            // 验证输入
            if (combinedTableId <= 0)
            {
                throw new ArgumentException("组合表的编号不能小于或是等于0。");
            }

            try
            {
                count = dalDataAuditing.GetTotalCountByCombinedTableId(combinedTableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public DataAuditingInfo GetModelInfoByLogId(decimal auditingLogId)
        {
            DataAuditingInfo dataAuditingInfo = null;

            // 验证输入
            if (auditingLogId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataAuditingInfo = dalDataAuditing.GetModelInfoByLogId(auditingLogId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataAuditingInfo;
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public DataAuditingInfo GetParentDataAuditingInfoByLogId(decimal auditingLogId)
        {
            DataAuditingInfo dataAuditingInfo = null;

            // 验证输入
            if (auditingLogId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataAuditingInfo = dalDataAuditing.GetParentDataAuditingInfoByLogId(auditingLogId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataAuditingInfo;
        }

        /// <summary>
        /// 获得 DataAuditingInfo 对象
        /// </summary>
        /// <param name="auditingId"></param>
        /// <returns></returns>
        public DataAuditingInfo GetParentDataAuditingInfo(decimal auditingId)
        {
            DataAuditingInfo dataAuditingInfo = null;

            // 验证输入
            if (auditingId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataAuditingInfo = dalDataAuditing.GetParentDataAuditingInfo(auditingId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataAuditingInfo;
        }

        /// <summary>
        /// 获得个人信息审核依赖的个人信息编号
        /// </summary>
        ///<param name="dataAuditingId">个人信息审核编号</param>
        /// <returns> 个人信息审核依赖的个人信息编号</returns>
        public decimal GetParentDataAuditingId(decimal dataAuditingId)
        {
            decimal parentDataAuditingId = decimal.MinValue;

            // 验证输入
            if (dataAuditingId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                parentDataAuditingId = dalDataAuditing.GetParentDataAuditingId(dataAuditingId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return parentDataAuditingId;
        }

        /// <summary>
        /// 获取当前初审的评审人列表
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetInitReviewers(decimal dataAuditingId, decimal userId)
        {
            Dictionary<decimal, string> reviewers = null;

            // 验证输入
            if (dataAuditingId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                reviewers = dalDataAuditing.GetInitReviewers(dataAuditingId, userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return reviewers;
        }

        /// <summary>
        /// 获取当前终审的评审人列表
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetFinalReviewers(decimal dataAuditingId)
        {
            Dictionary<decimal, string> reviewers = null;

            // 验证输入
            if (dataAuditingId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                reviewers = dalDataAuditing.GetFinalReviewers(dataAuditingId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return reviewers;
        }

        /// <summary>
        /// 获得组合表的字段
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDataAuditings(decimal parentDataAuditingId)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (parentDataAuditingId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalDataAuditing.GetDataAuditings(parentDataAuditingId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }
        
        /// <summary>
        /// 获得组合表的字段
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <returns></returns>
        public List<CommonNode> GetDataFields(decimal dataAuditingId)
        {
            List<CommonNode> commonNodes = null;

            // 验证输入
            if (dataAuditingId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dataAuditingAndDataField.GetDataFields(dataAuditingId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 更新组合表的字段集合
        /// </summary>
        /// <param name="dataAuditingId"></param>
        /// <param name="dataAuditingAndDataFieldInfos"></param>
        public void UpdateDataFields(decimal dataAuditingId, IList<DataAuditingAndDataFieldInfo> dataAuditingAndDataFieldInfos)
        {
            try
            {
                dataAuditingAndDataField.UpdateDataFields(dataAuditingId, dataAuditingAndDataFieldInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            try
            {
                dalDataAuditing.Update(tableId, recordId, commonDataFields, relaitonDataFields, queryBuilder, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            try
            {
                    dalDataAuditing.Update(tableId, recordId, commonDataFields, relaitonDataFields);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            try
            {
                dalDataAuditing.Update(tableId, recordIds, commonDataFields, relaitonDataFields);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新当前表中的字段（不更新其他表的关联字段和联动字段）
        /// </summary>
        /// <param name="recordEntity"></param>
        /// <param name="whereConditons"></param>
        public void Update(RecordEntity recordEntity, IList<WhereConditon> whereConditons)
        {
            try
            {
                dalDataAuditing.Update(recordEntity, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 处理记录并记录日志
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="dataAuditingLogInfo"></param>
        /// <param name="dataAuditingStepInfo"></param>
        public void ProcessWithLog(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, DataAuditingLogInfo dataAuditingLogInfo, DataAuditingStepInfo dataAuditingStepInfo)
        {
            try
            {
                dalDataAuditing.ProcessWithLog(commonUserInfo, recordEntities, dataAuditingLogInfo, dataAuditingStepInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 处理记录并记录日志
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        /// <param name="dataAuditingLogInfo"></param>
        public void Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities, DataAuditingLogInfo dataAuditingLogInfo, DataAuditingStepInfo dataAuditingStepInfo)
        {
            try
            {
                dalDataAuditing.Process(commonUserInfo, recordEntities, dataAuditingLogInfo, dataAuditingStepInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新数据表与字段
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="recordEntities"></param>
        public void Process(CommonUserInfo commonUserInfo, IList<RecordEntity> recordEntities)
        {
            try
            {
                dalDataAuditing.Process(commonUserInfo, recordEntities);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="recordEntities"></param>
        public void Process(decimal userId, IList<RecordEntity> recordEntities)
        {
            try
            {
                dalDataAuditing.Process(userId, recordEntities);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新主从表(启用业务模式)中的当前状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="instanceId"></param>
        public void UpdateCurretStateByInstanceId(decimal tableId, decimal recordId, decimal instanceId)
        {
            // 验证输入
            if (instanceId <= 0 || tableId <= 0 || recordId <= 0)
            {
                throw new ArgumentException("参数异常。实例参数、表格参数或者记录参数不能小于或等于0");
            }

            try
            {
                dalDataAuditing.UpdateCurretStateByInstanceId(tableId, recordId, instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新主从表(未启用业务模式)中的当前状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="userId"></param>
        public void UpdateCurretStateByUserId(decimal tableId, decimal recordId, decimal userId)
        {
            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("参数异常。实例参数、记录参数或者用户编号参数不能小于或等于0");
            }

            try
            {
                dalDataAuditing.UpdateCurretStateByUserId(tableId, recordId, userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
