//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserLog.cs
// 描述：UserLog 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/8/28
// Copyright 2016
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
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// UserLog 表的数据层访问类
    /// </summary>
    public class UserLog : IUserLog
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserLog()
		{
		}

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 UserLog 表中插入一条新记录
        /// </summary>
        /// <param name="userLogInfo">userLogInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(UserLogInfo userLogInfo)
        {
            //自动增加的关键字的值
            decimal userLogId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO UserLog(UserId, LogClass, BusinessName, LogEnumName, LogAction, ");
            sb.Append("LogLevel, LogDate)");
            sb.Append("VALUES (@UserId, @LogClass, @BusinessName, @LogEnumName, @LogAction, ");
            sb.Append("@LogLevel, @LogDate);");
            sb.Append("SET @LogId = SCOPE_IDENTITY()");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "LogId", DbType.Decimal, 8);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userLogInfo.UserId));
                    db.AddInParameter(dbCommand, "LogClass", DbType.Byte, DataConvertionHelper.SetByte(userLogInfo.LogClass));
                    db.AddInParameter(dbCommand, "BusinessName", DbType.String, userLogInfo.BusinessName);
                    db.AddInParameter(dbCommand, "LogEnumName", DbType.Int32, DataConvertionHelper.SetInt(userLogInfo.LogEnumName));
                    db.AddInParameter(dbCommand, "LogAction", DbType.Byte, DataConvertionHelper.SetByte(userLogInfo.LogAction));
                    db.AddInParameter(dbCommand, "LogLevel", DbType.Byte, DataConvertionHelper.SetByte(userLogInfo.LogLevel));
                    db.AddInParameter(dbCommand, "LogDate", DbType.DateTime, DataConvertionHelper.SetDateTime(userLogInfo.LogDate));
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                    userLogId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@LogId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
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
            UserLogInfo userLogInfo = null;
            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserId, LogClass, BusinessName, LogEnumName, LogAction, ");
            sb.Append("LogLevel, LogDate ");
            sb.Append("FROM UserLog ");
            sb.Append("WHERE LogId = @LogId");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "LogId", DbType.Decimal, DataConvertionHelper.SetDecimal(logId));
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            byte logClass = DataConvertionHelper.GetByte(dataReader[1]);
                            string businessName = DataConvertionHelper.GetString(dataReader[2]);
                            int logEnumName = DataConvertionHelper.GetInt(dataReader[3]);
                            byte logAction = DataConvertionHelper.GetByte(dataReader[4]);
                            byte logLevel = DataConvertionHelper.GetByte(dataReader[5]);
                            DateTime logDate = DataConvertionHelper.GetDateTime(dataReader[6]);
                            //创建 UserLogInfo 对象
                            userLogInfo = new UserLogInfo(logId, userId, logClass, businessName, logEnumName,
                            logAction, logLevel, logDate);
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

            return userLogInfo;
        }

        /// <summary>
        /// 更新 UserLogInfo 对象
        /// </summary>
        /// <param name="userLogInfo">UserLogInfo 对象</param>
        public void Update(UserLogInfo userLogInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserLog SET UserId = @UserId, LogClass = @LogClass, BusinessName = @BusinessName, ");
            sb.Append("LogEnumName = @LogEnumName, LogAction = @LogAction, LogLevel = @LogLevel, ");
            sb.Append("LogDate = @LogDate ");
            sb.Append("WHERE LogId = @LogId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "LogId", DbType.Decimal, DataConvertionHelper.SetDecimal(userLogInfo.LogId));
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userLogInfo.UserId));
                    db.AddInParameter(dbCommand, "LogClass", DbType.Byte, DataConvertionHelper.SetByte(userLogInfo.LogClass));
                    db.AddInParameter(dbCommand, "BusinessName", DbType.String, userLogInfo.BusinessName);
                    db.AddInParameter(dbCommand, "LogEnumName", DbType.Int32, DataConvertionHelper.SetInt(userLogInfo.LogEnumName));
                    db.AddInParameter(dbCommand, "LogAction", DbType.Byte, DataConvertionHelper.SetByte(userLogInfo.LogAction));
                    db.AddInParameter(dbCommand, "LogLevel", DbType.Byte, DataConvertionHelper.SetByte(userLogInfo.LogLevel));
                    db.AddInParameter(dbCommand, "LogDate", DbType.DateTime, DataConvertionHelper.SetDateTime(userLogInfo.LogDate));
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
        ///  删除 UserLogInfo 对象
        /// </summary>
        ///<param name="logId"></param>
        public void Delete(decimal logId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserLog ");
            sb.Append("WHERE LogId = @LogId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "LogId", DbType.Decimal, DataConvertionHelper.SetDecimal(logId));
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
        /// 获得 UserLogInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserLogInfo 对象列表</returns>
        public IList<UserLogInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 UserLog 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserLogInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "UserLog ", "LogId", false, whereConditons);
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
        /// 根据条件按月统计日志数量
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public Dictionary<int, int> GetStaticsByMonth(IList<WhereConditon> whereConditons)
        {
            Dictionary<int, int> statics = new Dictionary<int, int>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(LogId), DATEPART(mm, LogDate) FROM UserLog");
            if (whereConditons != null && whereConditons.Count > 0)
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            sb.Append(" GROUP BY DATEPART(mm, LogDate)");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
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
                            int logId = DataConvertionHelper.GetInt(dataReader[0]);
                            int motnth = DataConvertionHelper.GetInt(dataReader[1]);
                            statics.Add(motnth, logId);
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
            
            return statics;
        }

        /// <summary>
        /// 按编号批量删除日志
        /// </summary>
        /// <param name="logIds"></param>
        public void Delete(IList<decimal> logIds)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserLog ");
            sb.Append("WHERE ");
            for (int i = 0; i < logIds.Count; i++)
            {
                sb.AppendFormat("LogId = @LogId_{0} OR ", i);
            }
            sb.Remove(sb.Length - 4, 4);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    for (int i = 0; i < logIds.Count; i++)
                    {
                        db.AddInParameter(dbCommand, string.Format("LogId_{0}", i), DbType.Decimal, logIds[i]);
                    }
                    //执行删除操作
                    if (db.ExecuteNonQuery(dbCommand) != logIds.Count)
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
        /// 按条件删除日志
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserLog ");
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            else
            {
                throw new ArgumentNullException("批量删除的条件不许未空，即不允许删除该表中所有的数据.");
            }

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    count = (int)db.ExecuteNonQuery(dbCommand);
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

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                
                string dataFileNames = @"LogId, LogClass, LogEnumName, BusinessName, UserName, UserActualName, LogDate";
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserAccount", "UserId", TableJoin.InnerJoin));             
                ds =  DataAccessHandler.GetPageRecord(db, "UserLog ", "LogId", dataFileNames, false, true, tableLinks, startPosition, 
                    count, whereConditons, ref totalCount);
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.Caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
		/// 获得 UserLogInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>UserLogInfo 对象列表</returns>
		private IList<UserLogInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<UserLogInfo> userLogInfos = new List<UserLogInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM UserLog");

            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            if ((sortingCondtions != null) && (sortingCondtions.Count > 0))
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
                            decimal logId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            byte logClass = DataConvertionHelper.GetByte(dataReader[2]);
                            string businessName = DataConvertionHelper.GetString(dataReader[3]);
                            int logEnumName = DataConvertionHelper.GetInt(dataReader[4]);
                            byte logAction = DataConvertionHelper.GetByte(dataReader[5]);
                            byte logLevel = DataConvertionHelper.GetByte(dataReader[6]);
                            DateTime logDate = DataConvertionHelper.GetDateTime(dataReader[7]);
                            //将创建 UserLogInfo 对象加入集合中
                            userLogInfos.Add(new UserLogInfo(logId, userId, logClass, businessName, logEnumName,
                            logAction, logLevel, logDate));
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

            return userLogInfos;
        }

        /// <summary>
        /// 获得 UserLogInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserLogInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM UserLog");
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
        /// 获得表 UserLog 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        private DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {

                ds = DataAccessHandler.GetPageRecord(db, "UserLog", "LogId", "*", false, false, startPosition,
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
        /// 获得表 UserLog 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserLog ", "LogId", "*", false, false, startPosition, 
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
        /// 获得以表 UserLog 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserLog ", "LogId", "*", false, false, tableLinks, startPosition, 
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
        
        #endregion
        
        #region 自定义私有方法
        
        #endregion
        
		#endregion		
    }
}
