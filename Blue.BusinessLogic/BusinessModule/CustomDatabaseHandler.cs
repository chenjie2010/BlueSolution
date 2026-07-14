//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDatabaseHandler.cs
// 描述：CustomDatabase 业务处理类
// 作者：ChenJie 
// 编写日期：2016/9/11
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomDatabase.
    /// </summary>
    public class CustomDatabaseHandler : CommonNodeBusiness, ICustomDatabaseHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomDatabase dalCustomDatabase = BusinessDataAccessFactory.CreateCustomDatabase(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomDatabaseHandler() : base(dalCustomDatabase)
        {
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customdatabase 表中插入一条新记录
		/// </summary>
		/// <param name="customDatabaseInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomDatabaseInfo customDatabaseInfo)
		{
            //自动增加的关键字的值
			decimal customDatabaseId = 0;
            
			// 验证输入
			if (customDatabaseInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                customDatabaseId = dalCustomDatabase.Insert(customDatabaseInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customDatabaseId;
		}
        
        /// <summary>
		/// 获得 CustomDatabaseInfo 对象
		/// </summary>
		///<param name="databaseId">数据库编号</param>
		/// <returns> CustomDatabaseInfo 对象</returns>
		public CustomDatabaseInfo GetModelInfo(decimal databaseId)
		{			
			CustomDatabaseInfo  customDatabaseInfo = null;
            
			// 验证输入
			if(databaseId < 0)
            {
				return null;
            }

            try
            {
                customDatabaseInfo =  dalCustomDatabase.GetModelInfo(databaseId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customDatabaseInfo;
		}        
        
        /// <summary>
		/// 更新 CustomDatabaseInfo 对象
		/// </summary>
		/// <param name="customDatabaseInfo">CustomDatabaseInfo 对象</param>
		public void Update(CustomDatabaseInfo customDatabaseInfo)
		{	
            // 验证输入
            if (customDatabaseInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalCustomDatabase.Update(customDatabaseInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomDatabaseInfo 对象
		/// </summary>
		///<param name="databaseId">数据库编号</param>
		/// <returns> CustomDatabaseInfo 对象</returns>
		public void Delete(decimal databaseId)
		{		
            // 验证输入
			if(databaseId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomDatabase.Delete(databaseId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 CustomDatabaseInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDatabaseInfo 对象列表</returns>
		public IList<CustomDatabaseInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomDatabaseInfo>  customDatabaseInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customDatabaseInfos = dalCustomDatabase.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customDatabaseInfos;
		}               
        
        /// <summary>
		/// 获得 CustomDatabase 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomDatabaseInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomDatabase.GetTotalCount(whereConditons);
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
        /// 获得数据库的逻辑名称
        /// </summary>
        ///<param name="databaseId">数据库编号</param>
        /// <returns> 数据库的逻辑名称</returns>
        public string GetDatabaseName(decimal databaseId)
        {
            string databaseName = string.Empty;

            try
            {
                databaseName = dalCustomDatabase.GetDatabaseName(databaseId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return databaseName;
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(byte dataWarehouseId)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomDatabase.GetPageRecord(dataWarehouseId);
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
