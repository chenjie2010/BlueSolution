//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserConfig.cs
// 描述: UserConfig 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/6/26
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
using Blue.IDAL.UserModule;
using Blue.Model.UserModule;

namespace Blue.SQLServerDAL.UserModule
{
    /// <summary>
    /// UserConfig 表的数据层访问类
    /// </summary>
    public class UserConfig : IUserConfig
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserConfig()
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 UserConfig 表中插入一条新记录
		/// </summary>
		/// <param name="userConfigInfo">userConfigInfo 对象</param>
		public void Insert(UserConfigInfo userConfigInfo)
		{
			//生成插入语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("INSERT INTO UserConfig(UserId, UserConfigName, UserConfigValue, UpdatedTime)");
			sb.Append("VALUES (@UserId, @UserConfigName, @UserConfigValue, @UpdatedTime)");

			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userConfigInfo.UserId);
                    db.AddInParameter(dbCommand, "UserConfigName", DbType.Int32, userConfigInfo.UserConfigName);
                    db.AddInParameter(dbCommand, "UserConfigValue", DbType.String, userConfigInfo.UserConfigValue);
					db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DataConvertionHelper.SetDateTime(userConfigInfo.UpdatedTime));
					//执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
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
		/// 获得 UserConfigInfo 对象
		/// </summary>
		///<param name="userId">用户编号</param>
		///<param name="userConfigName">用户配置名称</param>
		/// <returns> UserConfigInfo 对象</returns>
		public UserConfigInfo GetModelInfo(decimal userId, int userConfigName)
		{			
			UserConfigInfo  userConfigInfo = null;
			//生成选择语句
			StringBuilder sb = new StringBuilder();		
			sb.Append("SELECT UserConfigValue, UpdatedTime ");
			sb.Append("FROM UserConfig ");
			sb.Append("WHERE UserId = @UserId AND UserConfigName = @UserConfigName");
			try
			{
				//获得系统数据库对象
				SqlDatabase db = DataAccessHelper.GetDatabase();
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
					db.AddInParameter(dbCommand, "UserConfigName", DbType.Int32, DataConvertionHelper.SetInt(userConfigName));
					using (IDataReader dataReader = db.ExecuteReader(dbCommand))
					{
						if (dataReader.Read())
						{
							string userConfigValue = DataConvertionHelper.GetString(dataReader[0]);
							DateTime updatedTime = DataConvertionHelper.GetDateTime(dataReader[1]);
							//创建 UserConfigInfo 对象
							userConfigInfo = new UserConfigInfo(userId, userConfigName, userConfigValue, updatedTime);
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
            
			return userConfigInfo;
		}
        
        /// <summary>
		/// 更新 UserConfigInfo 对象
		/// </summary>
		/// <param name="userConfigInfo">UserConfigInfo 对象</param>
		public void Update(UserConfigInfo userConfigInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE UserConfig SET UserConfigValue = @UserConfigValue, UpdatedTime = @UpdatedTime ");
			sb.Append("WHERE UserId = @UserId AND UserConfigName = @UserConfigName");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userConfigInfo.UserId));
					db.AddInParameter(dbCommand, "UserConfigName", DbType.Int32, DataConvertionHelper.SetInt(userConfigInfo.UserConfigName));
					db.AddInParameter(dbCommand, "UserConfigValue", DbType.String, userConfigInfo.UserConfigValue);
					db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DataConvertionHelper.SetDateTime(userConfigInfo.UpdatedTime));
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
		///  删除 UserConfigInfo 对象
		/// </summary>
	    ///<param name="userId">用户编号</param>
		///<param name="userConfigName">用户配置名称</param>
		public void Delete(decimal userId, int userConfigName)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM UserConfig ");
			sb.Append("WHERE UserId = @UserId AND UserConfigName = @UserConfigName");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
					db.AddInParameter(dbCommand, "UserConfigName", DbType.Int32, DataConvertionHelper.SetInt(userConfigName));
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
        /// 获得 UserConfigInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserConfigInfo 对象列表</returns>
        public IList<UserConfigInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
		/// 获得 UserConfig 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>UserConfigInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "UserConfig ", "UserId", false, whereConditons);
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

        /// <summary>
        /// 获得用户的个人信息设置
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<UserConfigInfo> GetModelInfos(decimal userId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal));
                        
            return GetModelInfos(whereConditons, null, false);
        }

        /// <summary>
        /// 更新用户设置
        /// </summary>
        /// <param name="userConfigInfos"></param>
        public void UpdateUserConfigInfos(Dictionary<int, UserConfigInfo> userConfigInfos)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("IF EXISTS(SELECT UserId FROM UserConfig WHERE UserId = @UserId AND UserConfigName = @UserConfigName) ");
            sb.Append("BEGIN UPDATE UserConfig SET UserConfigValue = @UserConfigValue, UpdatedTime = @UpdatedTime WHERE UserId = @UserId AND UserConfigName = @UserConfigName ");
            sb.Append("END ELSE ");
            sb.Append("BEGIN INSERT INTO UserConfig(UserId, UserConfigName, UserConfigValue, UpdatedTime) ");
            sb.Append("VALUES (@UserId, @UserConfigName, @UserConfigValue, @UpdatedTime) END");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (KeyValuePair<int, UserConfigInfo> keyValue in userConfigInfos)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            //给参数赋值
                            db.AddInParameter(dbCommand, "UserId", DbType.Decimal, keyValue.Value.UserId);
                            db.AddInParameter(dbCommand, "UserConfigName", DbType.Int32, keyValue.Value.UserConfigName);
                            db.AddInParameter(dbCommand, "UserConfigValue", DbType.String, keyValue.Value.UserConfigValue);
                            db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DateTime.Now);
                            //执行插入操作
                            if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                            {
                                throw new Exception("更新失败！");
                            }
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 UserConfigInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>UserConfigInfo 对象列表</returns>
        private IList<UserConfigInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<UserConfigInfo> userConfigInfos = new List<UserConfigInfo>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append(" * FROM UserConfig ");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append("WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            if ((sortingCondtions != null) && (sortingCondtions.Count > 0))
            {
                sb.Append("ORDER BY ");
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
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            int userConfigName = DataConvertionHelper.GetInt(dataReader[1]);
                            string userConfigValue = DataConvertionHelper.GetString(dataReader[2]);
                            DateTime updatedTime = DataConvertionHelper.GetDateTime(dataReader[3]);
                            //将创建 UserConfigInfo 对象加入集合中
                            userConfigInfos.Add(new UserConfigInfo(userId, userConfigName, userConfigValue, updatedTime));
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

            return userConfigInfos;
        }

        /// <summary>
        /// 获得 UserConfigInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserConfigInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM UserConfig");
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
        /// 获得表 UserConfig 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserConfig ", "UserId", "*", false, false, startPosition, 
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
        /// 获得以表 UserConfig 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserConfig ", "UserId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 UserConfig 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserConfig ", "UserId", "*", false, false, startPosition, 
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
        /// 获得以表 UserConfig 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserConfig ", "UserId", "*", false, false, tableLinks, startPosition, 
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
		/// 删除满足条件的的 UserConfigInfo 对象
		/// </summary>
	    /// <param name="userId">用户编号</param>
		/// <returns>返回删除的记录数目数目</returns>
		private int Delete(decimal userId)
		{
			int count = 0; 
			//删除语句
			string sqlDelete = "DELETE FROM UserConfig WHERE UserId = @UserId";
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
				{
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
					//执行删除操作
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
        
        /// <summary>
        /// 删除满足条件的所有  UserConfigInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserConfig");
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
