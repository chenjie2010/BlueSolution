//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserAndPrintHandler.cs
// 描述: UserAndPrint 业务处理类
// 作者：ChenJie 
// 编写日期：2022/11/13
// Copyright 2022
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.CustomLibrary;
using Blue.DALFactory;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.UserAndPrint.
    /// </summary>
    public class UserAndPrintHandler : IUserAndPrintHandler
    {
        #region 工厂类实例
        
        private static readonly IUserAndPrint dalUserAndPrint = BusinessModuleDataAccessFactory.CreateUserAndPrint(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserAndPrintHandler()
		{  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 userandprint 表中插入一条新记录
		/// </summary>
		/// <param name="userAndPrintInfo"></param>
		/// <returns></returns>
		public decimal Insert(UserAndPrintInfo userAndPrintInfo)
		{
            //自动增加的关键字的值
			decimal userAndPrintId = 0;
            
			// 验证输入
			if (userAndPrintInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                userAndPrintId = dalUserAndPrint.Insert(userAndPrintInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return userAndPrintId;
		}
        
        /// <summary>
		/// 获得 UserAndPrintInfo 对象
		/// </summary>
		///<param name="printId">数据打印编号</param>
		///<param name="userId">用户编号</param>
		/// <returns> UserAndPrintInfo 对象</returns>
		public UserAndPrintInfo GetModelInfo(decimal printId, decimal userId)
		{			
			UserAndPrintInfo  userAndPrintInfo = null;
            
			// 验证输入
			if(printId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                userAndPrintInfo =  dalUserAndPrint.GetModelInfo(printId, userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return userAndPrintInfo;
		}        
        
        /// <summary>
		/// 更新 UserAndPrintInfo 对象
		/// </summary>
		/// <param name="userAndPrintInfo">UserAndPrintInfo 对象</param>
		public void Update(UserAndPrintInfo userAndPrintInfo)
		{	
            // 验证输入
            if (userAndPrintInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalUserAndPrint.Update(userAndPrintInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 UserAndPrintInfo 对象
		/// </summary>
		///<param name="printId">数据打印编号</param>
		///<param name="userId">用户编号</param>
		/// <returns> UserAndPrintInfo 对象</returns>
		public void Delete(decimal printId, decimal userId)
		{		
            // 验证输入
			if(printId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalUserAndPrint.Delete(printId, userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 UserAndPrintInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserAndPrintInfo  对象列表</returns>
		public IList<UserAndPrintInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<UserAndPrintInfo>  userAndPrintInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                userAndPrintInfos = dalUserAndPrint.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return userAndPrintInfos;
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
                count = dalUserAndPrint.GetTotalCount(whereConditons);
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
        
        #endregion
		
		#region 私有方法
		
		#endregion
    }
}
