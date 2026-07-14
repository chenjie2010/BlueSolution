//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingStepHandler.cs
// 描述: DataAuditingStep 业务处理类
// 作者：ChenJie 
// 编写日期：2018/10/19
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
    /// 业务层处理类，对于的表： dbo.DataAuditingStep.
    /// </summary>
    public class DataAuditingStepHandler : IDataAuditingStepHandler
    {
        #region 工厂类实例
        
        private static readonly IDataAuditingStep dalDataAuditingStep = BusinessDesignerDataAccessFactory.CreateDataAuditingStep(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public DataAuditingStepHandler()
		{  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 dataauditingstep 表中插入一条新记录
		/// </summary>
		/// <param name="dataAuditingStepInfo"></param>
		/// <returns></returns>
		public decimal Insert(DataAuditingStepInfo dataAuditingStepInfo)
		{
            //自动增加的关键字的值
			decimal dataAuditingStepId = 0;
            
			// 验证输入
			if (dataAuditingStepInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                dataAuditingStepId = dalDataAuditingStep.Insert(dataAuditingStepInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return dataAuditingStepId;
		}
        
        /// <summary>
		/// 获得 DataAuditingStepInfo 对象
		/// </summary>
		///<param name="stepId">步骤编号</param>
		/// <returns> DataAuditingStepInfo 对象</returns>
		public DataAuditingStepInfo GetModelInfo(decimal stepId)
		{			
			DataAuditingStepInfo  dataAuditingStepInfo = null;
            
			// 验证输入
			if(stepId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataAuditingStepInfo =  dalDataAuditingStep.GetModelInfo(stepId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return dataAuditingStepInfo;
		}        
        
        /// <summary>
		/// 更新 DataAuditingStepInfo 对象
		/// </summary>
		/// <param name="dataAuditingStepInfo">DataAuditingStepInfo 对象</param>
		public void Update(DataAuditingStepInfo dataAuditingStepInfo)
		{	
            // 验证输入
            if (dataAuditingStepInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalDataAuditingStep.Update(dataAuditingStepInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 DataAuditingStepInfo 对象
		/// </summary>
		///<param name="stepId">步骤编号</param>
		/// <returns> DataAuditingStepInfo 对象</returns>
		public void Delete(decimal stepId)
		{		
            // 验证输入
			if(stepId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalDataAuditingStep.Delete(stepId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 DataAuditingStepInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataAuditingStepInfo  对象列表</returns>
		public IList<DataAuditingStepInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<DataAuditingStepInfo>  dataAuditingStepInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                dataAuditingStepInfos = dalDataAuditingStep.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return dataAuditingStepInfos;
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
                count = dalDataAuditingStep.GetTotalCount(whereConditons);
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
        /// 获得以表 DataAuditingStep 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetDataAuditingSteps(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                ds = dalDataAuditingStep.GetDataAuditingSteps(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得日志流程
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public DataSet GetSteps(decimal auditingLogId)
        {
            DataSet ds = null;

            // 验证输入
            if (auditingLogId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                ds = dalDataAuditingStep.GetSteps(auditingLogId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得最新提交人
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public CommonNode GetLastestSubmitter(decimal auditingLogId)
        {
            CommonNode commonNode = null;

            // 验证输入
            if (auditingLogId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNode = dalDataAuditingStep.GetLastestSubmitter(auditingLogId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNode;
        }

        /// <summary>
        /// 获得最新审核人
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public CommonNode GetLastestReviewer(decimal auditingLogId)
        {
            CommonNode commonNode = null;

            // 验证输入
            if (auditingLogId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNode = dalDataAuditingStep.GetLastestReviewer(auditingLogId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNode;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
