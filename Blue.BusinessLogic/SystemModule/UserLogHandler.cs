//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserLogHandler.cs
// 描述：UserLog 业务处理类
// 作者：ChenJie 
// 编写日期：2016/8/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;

namespace Blue.BusinessLogic.SystemModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.UserLog.
    /// </summary>
    public class UserLogHandler : IUserLogHandler
    {
        #region 工厂类实例
        
        private static readonly IUserLog dalUserLog = SystemDataAccessFactory.CreateUserLog(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserLogHandler()
		{
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 userlog 表中插入一条新记录
		/// </summary>
		/// <param name="userLogInfo"></param>
		/// <returns></returns>
		public decimal Insert(UserLogInfo userLogInfo)
		{
            //自动增加的关键字的值
			decimal userLogId = 0;
            
			// 验证输入
			if (userLogInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                userLogId = dalUserLog.Insert(userLogInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return userLogId;
		}
        
        /// <summary>
		/// 获得 UserLogInfo 对象
		/// </summary>
		///<param name="logId"></param>
		/// <returns> UserLogInfo 对象</returns>
		public UserLogInfo GetModelInfo(decimal logId)
		{			
			UserLogInfo  userLogInfo = null;
            
			// 验证输入
			if(logId < 0)
            {
				return null;
            }

            try
            {
                userLogInfo =  dalUserLog.GetModelInfo(logId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return userLogInfo;
		}        
        
        /// <summary>
		/// 更新 UserLogInfo 对象
		/// </summary>
		/// <param name="userLogInfo">UserLogInfo 对象</param>
		public void Update(UserLogInfo userLogInfo)
		{	
            // 验证输入
            if (userLogInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalUserLog.Update(userLogInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 UserLogInfo 对象
		/// </summary>
		///<param name="logId"></param>
		/// <returns> UserLogInfo 对象</returns>
		public void Delete(decimal logId)
		{		
            // 验证输入
			if(logId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalUserLog.Delete(logId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 UserLogInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserLogInfo 对象列表</returns>
		public IList<UserLogInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<UserLogInfo>  userLogInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                userLogInfos = dalUserLog.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return userLogInfos;
		}               
        
        /// <summary>
		/// 获得 UserLog 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>UserLogInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalUserLog.GetTotalCount(whereConditons);
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
        /// 根据条件按月统计日志数量
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetStaticsByMonth(decimal userId, LogTitle logTitle)
        {
            Dictionary<int, int> statics = null;

            DateTime firstDay = new DateTime(DateTime.Now.Year, 1, 1);
            IList <WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("UserLog", "UserId", "UserId", DbType.Decimal, userId,
                  DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("UserLog", "LogEnumName", "LogEnumName", DbType.Byte, (byte)logTitle,
                  DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("UserLog", "LogDate", "LogDate", DbType.DateTime, firstDay,
                  DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            try
            {
                statics = dalUserLog.GetStaticsByMonth(whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return statics;
        }

        /// <summary>
        /// 按编号批量删除日志
        /// </summary>
        /// <param name="logIds"></param>
        public void Delete(IList<decimal> logIds)
        {
            try
            {
                dalUserLog.Delete(logIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 按条件删除日志
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                count = dalUserLog.Delete(whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得以表 UserLog 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                ds = dalUserLog.GetPageRecordOfMultiTables(startPosition, count, whereConditons, ref totalCount);
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
