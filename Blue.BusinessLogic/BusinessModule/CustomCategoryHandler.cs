//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomCategoryHandler.cs
// 描述：CustomCategory 业务处理类
// 作者：ChenJie 
// 编写日期：2016/9/17
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
    /// 业务层处理类，对于的表： dbo.CustomCategory.
    /// </summary>
    public class CustomCategoryHandler : CommonNodeBusiness, ICustomCategoryHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomCategory dalCustomCategory = BusinessDataAccessFactory.CreateCustomCategory(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomCategoryHandler() : base(dalCustomCategory)
        {
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customcategory 表中插入一条新记录
		/// </summary>
		/// <param name="customCategoryInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomCategoryInfo customCategoryInfo)
		{
            //自动增加的关键字的值
			decimal customCategoryId = 0;
            
			// 验证输入
			if (customCategoryInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                customCategoryId = dalCustomCategory.Insert(customCategoryInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customCategoryId;
		}
        
        /// <summary>
		/// 获得 CustomCategoryInfo 对象
		/// </summary>
		///<param name="categoryId">分类编号</param>
		/// <returns> CustomCategoryInfo 对象</returns>
		public CustomCategoryInfo GetModelInfo(decimal categoryId)
		{			
			CustomCategoryInfo  customCategoryInfo = null;
            
			// 验证输入
			if(categoryId < 0)
            {
				return null;
            }

            try
            {
                customCategoryInfo =  dalCustomCategory.GetModelInfo(categoryId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customCategoryInfo;
		}        
        
        /// <summary>
		/// 更新 CustomCategoryInfo 对象
		/// </summary>
		/// <param name="customCategoryInfo">CustomCategoryInfo 对象</param>
		public void Update(CustomCategoryInfo customCategoryInfo)
		{	
            // 验证输入
            if (customCategoryInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalCustomCategory.Update(customCategoryInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomCategoryInfo 对象
		/// </summary>
		///<param name="categoryId">分类编号</param>
		/// <returns> CustomCategoryInfo 对象</returns>
		public void Delete(decimal categoryId)
		{		
            // 验证输入
			if(categoryId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomCategory.Delete(categoryId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 CustomCategoryInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomCategoryInfo 对象列表</returns>
		public IList<CustomCategoryInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomCategoryInfo>  customCategoryInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customCategoryInfos = dalCustomCategory.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customCategoryInfos;
		}               
        
        /// <summary>
		/// 获得 CustomCategory 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomCategoryInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomCategory.GetTotalCount(whereConditons);
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
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="databaseIds"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(IList<decimal> databaseIds)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomCategory.GetPageRecord(databaseIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal databaseId)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomCategory.GetPageRecord(databaseId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得数据库编号
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public decimal GetDatabaseId(decimal categoryId)
        {
            decimal databaseId = 0;

            // 验证输入
            if (categoryId <= 0)
            {
                throw new ArgumentException("分类编号不能小于或是等于0.");
            }

            try
            {
                databaseId = dalCustomCategory.GetDatabaseId(categoryId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return databaseId;
        }

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal categoryId)
        {
            byte dataWarehouseId = 0;

            // 验证输入
            if (categoryId <= 0)
            {
                throw new ArgumentException("分类编号不能小于或是等于0.");
            }

            try
            {
                dataWarehouseId = dalCustomCategory.GetDataWarehouseId(categoryId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataWarehouseId;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
