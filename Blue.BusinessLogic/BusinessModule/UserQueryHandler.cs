//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserQueryHandler.cs
// 描述: UserQuery 业务处理类
// 作者：ChenJie 
// 编写日期：2019/6/10
// Copyright 2019
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
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.UserQuery.
    /// </summary>
    public class UserQueryHandler : CommonNodeBusiness, IUserQueryHandler
    {
        #region 工厂类实例
        
        private static readonly IUserQuery dalUserQuery = BusinessDataAccessFactory.CreateUserQuery();
        private static readonly IQueryAndDataField dalQueryAndDataField = BusinessDataAccessFactory.CreateQueryAndDataField();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserQueryHandler() : base(dalUserQuery)
        {  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 userquery 表中插入一条新记录
		/// </summary>
		/// <param name="userQueryInfo"></param>
		/// <returns></returns>
		public decimal Insert(UserQueryInfo userQueryInfo)
		{
            //自动增加的关键字的值
			decimal userQueryId = 0;
            
			// 验证输入
			if (userQueryInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                userQueryId = dalUserQuery.Insert(userQueryInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return userQueryId;
		}
        
        /// <summary>
		/// 获得 UserQueryInfo 对象
		/// </summary>
		///<param name="userQueryId">查询编号</param>
		/// <returns> UserQueryInfo 对象</returns>
		public UserQueryInfo GetModelInfo(decimal userQueryId)
		{			
			UserQueryInfo  userQueryInfo = null;
            
			// 验证输入
			if(userQueryId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                userQueryInfo =  dalUserQuery.GetModelInfo(userQueryId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return userQueryInfo;
		}        
        
        /// <summary>
		/// 更新 UserQueryInfo 对象
		/// </summary>
		/// <param name="userQueryInfo">UserQueryInfo 对象</param>
		public void Update(UserQueryInfo userQueryInfo)
		{	
            // 验证输入
            if (userQueryInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalUserQuery.Update(userQueryInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 UserQueryInfo 对象
		/// </summary>
		///<param name="userQueryId">查询编号</param>
		/// <returns> UserQueryInfo 对象</returns>
		public void Delete(decimal userQueryId)
		{		
            // 验证输入
			if(userQueryId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalUserQuery.Delete(userQueryId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 UserQueryInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserQueryInfo  对象列表</returns>
		public IList<UserQueryInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<UserQueryInfo>  userQueryInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                userQueryInfos = dalUserQuery.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return userQueryInfos;
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
                count = dalUserQuery.GetTotalCount(whereConditons);
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
        /// 向 UserQuery 表中插入一条新记录和查询字段集合
        /// </summary>
        /// <param name="userQueryInfo">userQueryInfo 对象</param>
        /// <param name="queryAndDataFieldInfos">字段列表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(UserQueryInfo userQueryInfo, IList<QueryAndDataFieldInfo> queryAndDataFieldInfos)
        {
            decimal userQueryId = decimal.MinValue;

            try
            {
                userQueryId = dalUserQuery.Insert(userQueryInfo, queryAndDataFieldInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userQueryId;
        }


        /// <summary>
        /// 获得 QueryAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="userQueryId"></param>
        /// <returns></returns>
        public IList<QueryAndDataFieldInfo>  GetQueryAndDataFieldInfos(decimal userQueryId)
        {
            IList<QueryAndDataFieldInfo> queryAndDataFieldInfos = null;

            // 验证输入
            if (userQueryId <= 0)
            {
                throw new ArgumentException("参数错误。");
            }

            try
            {
                queryAndDataFieldInfos = dalQueryAndDataField.GetModelInfos(userQueryId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return queryAndDataFieldInfos;
        }
        #endregion

        #region 私有方法

        #endregion
    }
}
