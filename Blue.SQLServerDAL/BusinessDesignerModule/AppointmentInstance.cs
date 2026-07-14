//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppointmentInstance.cs
// 描述: AppointmentInstance 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.SQLServerDAL.BusinessDesignerModule
{
    /// <summary>
    /// AppointmentInstance 表的数据层访问类
    /// </summary>
    public class AppointmentInstance : IAppointmentInstance
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public AppointmentInstance()
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 AppointmentInstance 表中插入一条新记录
		/// </summary>
		/// <param name="appointmentInstanceInfo">appointmentInstanceInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(AppointmentInstanceInfo appointmentInstanceInfo)
		{
			//自动增加的关键字的值
			decimal appointmentInstanceId= 0;
			//生成插入语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("INSERT INTO AppointmentInstance(AppointmentId, UserId, InstanceId, ParentInstanceId, BusinessId, ");
			sb.Append("CretatedTime)");
			sb.Append("VALUES (@AppointmentId, @UserId, @InstanceId, @ParentInstanceId, @BusinessId, ");
			sb.Append("@CretatedTime);");
			sb.Append("SET @AppInstanceId = SCOPE_IDENTITY()");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddOutParameter(dbCommand, "AppInstanceId", DbType.Decimal,10);
					db.AddInParameter(dbCommand, "AppointmentId", DbType.Decimal, appointmentInstanceInfo.AppointmentId);
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, appointmentInstanceInfo.UserId);
					db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, appointmentInstanceInfo.InstanceId);
					db.AddInParameter(dbCommand, "ParentInstanceId", DbType.Decimal, appointmentInstanceInfo.ParentInstanceId);
					db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, appointmentInstanceInfo.BusinessId);
					db.AddInParameter(dbCommand, "CretatedTime", DbType.DateTime, appointmentInstanceInfo.CretatedTime);
					//执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
					appointmentInstanceId= DataConvertionHelper.GetDecimal(dbCommand.Parameters["@AppInstanceId"].Value, 0);
				}
			}
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return appointmentInstanceId;
		}

        /// <summary>
		/// 获得 AppointmentInstanceInfo 对象
		/// </summary>
		///<param name="appInstanceId">实例编号</param>
		/// <returns> AppointmentInstanceInfo 对象</returns>
		public AppointmentInstanceInfo GetModelInfo(decimal appInstanceId)
		{			
			AppointmentInstanceInfo  appointmentInstanceInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("AppInstanceId", "AppInstanceId", DbType.Decimal, appInstanceId, DataFieldCondition.Equal));

            //创建集合对象
            IList<AppointmentInstanceInfo>  appointmentInstanceInfos = GetModelInfos(whereConditons, null, true);
            if (appointmentInstanceInfos != null && appointmentInstanceInfos.Count > 0)
            {
                appointmentInstanceInfo = appointmentInstanceInfos[0];
            }

            return appointmentInstanceInfo;            
		}
        
        /// <summary>
		/// 更新 AppointmentInstanceInfo 对象
		/// </summary>
		/// <param name="appointmentInstanceInfo">AppointmentInstanceInfo 对象</param>
		public void Update(AppointmentInstanceInfo appointmentInstanceInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE AppointmentInstance SET AppointmentId = @AppointmentId, UserId = @UserId, InstanceId = @InstanceId, ");
			sb.Append("ParentInstanceId = @ParentInstanceId, BusinessId = @BusinessId, CretatedTime = @CretatedTime ");
			sb.Append("WHERE AppInstanceId = @AppInstanceId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "AppInstanceId", DbType.Decimal, appointmentInstanceInfo.AppInstanceId);
					db.AddInParameter(dbCommand, "AppointmentId", DbType.Decimal, appointmentInstanceInfo.AppointmentId);
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, appointmentInstanceInfo.UserId);
					db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, appointmentInstanceInfo.InstanceId);
					db.AddInParameter(dbCommand, "ParentInstanceId", DbType.Decimal, appointmentInstanceInfo.ParentInstanceId);
					db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, appointmentInstanceInfo.BusinessId);
					db.AddInParameter(dbCommand, "CretatedTime", DbType.DateTime, appointmentInstanceInfo.CretatedTime);
					//执行更新操作
					if (db.ExecuteNonQuery(dbCommand) != 1)
					{
						throw new Exception("更新失败！");
					}
				}
			}
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
		}        
        
        /// <summary>
		///  删除 AppointmentInstanceInfo 对象
		/// </summary>
	    ///<param name="appInstanceId">实例编号</param>
		public void Delete(decimal appInstanceId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM AppointmentInstance ");
			sb.Append("WHERE AppInstanceId = @AppInstanceId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "AppInstanceId", DbType.Decimal, appInstanceId);
					//执行删除操作
					if (db.ExecuteNonQuery(dbCommand) != 1)
					{
						throw new Exception("删除失败！");
					}
				}
			}
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }		
		}       
        
        /// <summary>
		/// 获得 AppointmentInstanceInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AppointmentInstanceInfo 对象列表</returns>
		public IList<AppointmentInstanceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 AppointmentInstance 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>AppointmentInstanceInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "AppointmentInstance ", "AppInstanceId", false, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
		}
        
        
        #endregion        
        
        #region 实现自定义接口
        
        #region 实现新增接口
        
        #endregion
        
        #endregion
        
        #region 公有方法
        
        #endregion
        
        #region 私有方法
        
        #region 默认私有方法	
		
        /// <summary>
		/// 获得 AppointmentInstanceInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>AppointmentInstanceInfo 对象列表</returns>
		private IList<AppointmentInstanceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<AppointmentInstanceInfo>  appointmentInstanceInfos = new List<AppointmentInstanceInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM AppointmentInstance");
            
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            if((sortingCondtions != null) && (sortingCondtions.Count > 0))
            {
                sb.Append(" ORDER BY ");
                sb.Append(DataAccessHandler.GetSortingSentence(sortingCondtions));                
            }
			try
			{
				//获得系统数据库对象
				SqlDatabase db = DataAccessHelper.GetDatabase();
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
					using (IDataReader dataReader = db.ExecuteReader(dbCommand))
					{                        
						while (dataReader.Read())
						{
							decimal appInstanceId = DataConvertionHelper.GetDecimal(dataReader[0]);
							decimal appointmentId = DataConvertionHelper.GetDecimal(dataReader[1]);
							decimal userId = DataConvertionHelper.GetDecimal(dataReader[2]);
							decimal instanceId = DataConvertionHelper.GetDecimal(dataReader[3]);
							decimal parentInstanceId = DataConvertionHelper.GetDecimal(dataReader[4]);
							decimal businessId = DataConvertionHelper.GetDecimal(dataReader[5]);
							DateTime cretatedTime = DataConvertionHelper.GetDateTime(dataReader[6]);
							//将创建 AppointmentInstanceInfo 对象加入集合中
							appointmentInstanceInfos.Add(new AppointmentInstanceInfo(appInstanceId, appointmentId, userId, instanceId, parentInstanceId, 
							businessId, cretatedTime));							
						}
						if (dataReader != null)
						{
							dataReader.Close();
						}
					}
				}
			}
			catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return appointmentInstanceInfos;
		}
        
		
		/// <summary>
		/// 获得 AppointmentInstanceInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>AppointmentInstanceInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM AppointmentInstance");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
			try
			{
				//获得系统数据库对象
				SqlDatabase db = DataAccessHelper.GetDatabase();
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
					ds = db.ExecuteDataSet(dbCommand);
				}
			}
			catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return ds;
		}
        
        /// <summary>
        /// 获得表 AppointmentInstance 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int  startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;
            
            //获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {             
                ds =  DataAccessHandler.GetPageRecord(db, "AppointmentInstance ", "AppInstanceId", "*", false, false, startPosition, 
                    count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }
        
        /// <summary>
        /// 获得以表 AppointmentInstance 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;
            
            //获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            { 
                /* ----------------for example ---------------------------------- 
                string dataFileNames = @"News.NewsId, News.NewsTitle, News.IsRecommended, News.IsShowed, NewsClass.NewsClassName, NewsSubClass.NewsSubClassName";
                IList<TableLink> tableLinks = new List<TableLink>();
                //tableLinks.Add(new TableLink("NewsSubClass", TableJoin.InnerJoin, "NewsSubClassId"));
                //tableLinks.Add(new TableLink("NewsClass", TableJoin.InnerJoin, "NewsClassId"));                
                ds =  DataAccessHandler.GetPageRecord(db, "AppointmentInstance ", "AppInstanceId", "*", false, false, tableLinks, startPosition, 
                    count, whereConditons, ref totalCount);                 
               -------------------------------------------------------------------*/
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }
        
        /// <summary>
        /// 获得表 AppointmentInstance 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;
            
            //获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {             
                ds =  DataAccessHandler.GetPageRecord(db, "AppointmentInstance ", "AppInstanceId", "*", false, false, startPosition, 
                    count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }
        
        /// <summary>
        /// 获得以表 AppointmentInstance 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;
            
            //获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            { 
                /* ----------------for example ---------------------------------- 
                string dataFileNames = @"News.NewsId, News.NewsTitle, News.IsRecommended, News.IsShowed, NewsClass.NewsClassName, NewsSubClass.NewsSubClassName";
                IList<TableLink> tableLinks = new List<TableLink>();
                //tableLinks.Add(new TableLink("NewsSubClass", TableJoin.InnerJoin, "NewsSubClassId"));
                //tableLinks.Add(new TableLink("NewsClass", TableJoin.InnerJoin, "NewsClassId"));                
                ds =  DataAccessHandler.GetPageRecord(db, "AppointmentInstance ", "AppInstanceId", "*", false, false, tableLinks, startPosition, 
                    count, whereConditons, sortingCondtions, ref totalCount);                 
               -------------------------------------------------------------------*/
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }


        
        /// <summary>
        /// 删除满足条件的所有  AppointmentInstanceInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM AppointmentInstance");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            else
            {
                throw new ArgumentNullException("批量删除的条件不许未空，即不允许删除该表中所有的数据.");
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    count = db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count; 
        }
        
        #endregion
        
        #region 自定义私有方法
        
        #endregion
        
		#endregion		
    }
}
