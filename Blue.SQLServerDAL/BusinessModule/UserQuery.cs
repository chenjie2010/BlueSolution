//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserQuery.cs
// 描述: UserQuery 数据层访问类
// 作者：ChenJie 
// 编写日期：2019/6/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.DataAccessLibrary;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// UserQuery 表的数据层访问类
    /// </summary>
    public class UserQuery : CommonNodeDataAccess, IUserQuery
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserQuery() : base("UserQuery", "UserQueryId", "GroupId", "UserQueryName", "UserQueryCode", false, true, "RecommendType", "UserId")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 UserQuery 表中插入一条新记录
        /// </summary>
        /// <param name="userQueryInfo">userQueryInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(UserQueryInfo userQueryInfo)
        {
            //自动增加的关键字的值
            decimal userQueryId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            userQueryInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "UserQuery", "Sorting", "GroupId", userQueryInfo.GroupId, 0) + 1;
            userQueryId = Insert(userQueryInfo, db, null);

            return userQueryId;
        }

        /// <summary>
		/// 获得 UserQueryInfo 对象
		/// </summary>
		///<param name="userQueryId">查询编号</param>
		/// <returns> UserQueryInfo 对象</returns>
		public UserQueryInfo GetModelInfo(decimal userQueryId)
        {
            UserQueryInfo userQueryInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("UserQueryId", "UserQueryId", DbType.Decimal, userQueryId, DataFieldCondition.Equal));

            //创建集合对象
            IList<UserQueryInfo> userQueryInfos = GetModelInfos(whereConditons, null, true);
            if (userQueryInfos != null && userQueryInfos.Count > 0)
            {
                userQueryInfo = userQueryInfos[0];
            }

            return userQueryInfo;
        }

        /// <summary>
        /// 更新 UserQueryInfo 对象
        /// </summary>
        /// <param name="userQueryInfo">UserQueryInfo 对象</param>
        public void Update(UserQueryInfo userQueryInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserQuery SET GroupId = @GroupId, UserId = @UserId, UserQueryName = @UserQueryName, ");
            sb.Append("UserQueryCode = @UserQueryCode, QueryShowType = @QueryShowType, RecommendType = @RecommendType, ");
            sb.Append("TableNameRelation = @TableNameRelation, IsGroup = @IsGroup, IsDistinct = @IsDistinct, ");
            sb.Append("EneableCondition = @EneableCondition, Notes = @Notes ");
            sb.Append("WHERE UserQueryId = @UserQueryId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserQueryId", DbType.Decimal, userQueryInfo.UserQueryId);
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, userQueryInfo.GroupId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userQueryInfo.UserId);
                    db.AddInParameter(dbCommand, "UserQueryName", DbType.String, userQueryInfo.UserQueryName);
                    db.AddInParameter(dbCommand, "UserQueryCode", DbType.String, userQueryInfo.UserQueryCode);
                    db.AddInParameter(dbCommand, "QueryShowType", DbType.Byte, userQueryInfo.QueryShowType);
                    db.AddInParameter(dbCommand, "RecommendType", DbType.Byte, userQueryInfo.RecommendType);
                    db.AddInParameter(dbCommand, "TableNameRelation", DbType.Int64, userQueryInfo.TableNameRelation);
                    db.AddInParameter(dbCommand, "IsGroup", DbType.Boolean, userQueryInfo.IsGroup);
                    db.AddInParameter(dbCommand, "IsDistinct", DbType.Boolean, userQueryInfo.IsDistinct);
                    db.AddInParameter(dbCommand, "EneableCondition", DbType.Boolean, userQueryInfo.EneableCondition);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, userQueryInfo.Notes);
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
        ///  删除 UserQueryInfo 对象
        /// </summary>
        ///<param name="userQueryId">查询编号</param>
        public void Delete(decimal userQueryId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserQuery ");
            sb.Append("WHERE UserQueryId = @UserQueryId");

            QueryAndDataField queryAndDataField = new QueryAndDataField();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    queryAndDataField.Delete(userQueryId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "UserQueryId", DbType.Decimal, userQueryId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
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

        /// <summary>
        /// 获得 UserQueryInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserQueryInfo 对象列表</returns>
        public IList<UserQueryInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 UserQuery 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserQueryInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "UserQuery ", "UserQueryId", false, whereConditons);
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
		/// 向 UserQuery 表中插入一条新记录和查询字段集合
		/// </summary>
		/// <param name="userQueryInfo">userQueryInfo 对象</param>
        /// <param name="queryAndDataFieldInfos">字段列表</param>
		/// <returns>自动增加的关键字的值</returns>
        public decimal Insert(UserQueryInfo userQueryInfo, IList<QueryAndDataFieldInfo> queryAndDataFieldInfos)
        {
            decimal userQueryId = decimal.MinValue;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            userQueryInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "UserQuery", "Sorting", "GroupId", userQueryInfo.GroupId, 0) + 1;
   
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    userQueryId = Insert(userQueryInfo, db, null);
                    if (queryAndDataFieldInfos != null && queryAndDataFieldInfos.Count > 0)
                    {
                        int sorting = 0;
                        QueryAndDataField queryAndDataField = new QueryAndDataField();
                        foreach (QueryAndDataFieldInfo queryAndDataFieldInfo in queryAndDataFieldInfos)
                        {
                            queryAndDataFieldInfo.UserQueryId = userQueryId;
                            queryAndDataFieldInfo.Sorting = sorting++;
                            queryAndDataField.Insert(queryAndDataFieldInfo, db, transaction);
                        }
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(userQueryInfo.GroupId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return userQueryId;
        }
        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 UserQueryInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>UserQueryInfo 对象列表</returns>
        private IList<UserQueryInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<UserQueryInfo> userQueryInfos = new List<UserQueryInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM UserQuery");

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
                            decimal userQueryId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            string userQueryName = DataConvertionHelper.GetString(dataReader[3]);
                            string userQueryCode = DataConvertionHelper.GetString(dataReader[4]);
                            byte queryShowType = DataConvertionHelper.GetByte(dataReader[5]);
                            byte recommendType = DataConvertionHelper.GetByte(dataReader[6]);
                            long tableNameRelation = DataConvertionHelper.GetLong(dataReader[7]);
                            bool isGroup = DataConvertionHelper.GetBoolean(dataReader[8]);
                            bool isDistinct = DataConvertionHelper.GetBoolean(dataReader[9]);
                            bool eneableCondition = DataConvertionHelper.GetBoolean(dataReader[10]);
                            string notes = DataConvertionHelper.GetString(dataReader[11]);
                            DateTime createdTime = DataConvertionHelper.GetDateTime(dataReader[12]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[13]);
                            //将创建 UserQueryInfo 对象加入集合中
                            userQueryInfos.Add(new UserQueryInfo(userQueryId, groupId, userId, userQueryName, userQueryCode,
                            queryShowType, recommendType, tableNameRelation, isGroup, isDistinct,
                            eneableCondition, notes, createdTime, sorting));
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

            return userQueryInfos;
        }


        /// <summary>
        /// 获得 UserQueryInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserQueryInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM UserQuery");
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
        /// 获得表 UserQuery 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "UserQuery", "UserQueryId", "*", false, false, startPosition,
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
        /// 获得以表 UserQuery 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserQuery ", "UserQueryId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 UserQuery 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "UserQuery ", "UserQueryId", "*", false, false, startPosition,
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
        /// 获得以表 UserQuery 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserQuery ", "UserQueryId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  UserQueryInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserQuery");
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
        
        /// <summary>
        /// 向 UserQuery 表中插入一条新记录
        /// </summary>
        /// <param name="userQueryInfo">userQueryInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(UserQueryInfo userQueryInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal userQueryId = 0;
           
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO UserQuery(GroupId, UserId, UserQueryName, UserQueryCode, QueryShowType, ");
            sb.Append("RecommendType, TableNameRelation, IsGroup, IsDistinct, EneableCondition, ");
            sb.Append("Notes, CreatedTime, Sorting)");
            sb.Append("VALUES (@GroupId, @UserId, @UserQueryName, @UserQueryCode, @QueryShowType, ");
            sb.Append("@RecommendType, @TableNameRelation, @IsGroup, @IsDistinct, @EneableCondition, ");
            sb.Append("@Notes, @CreatedTime, @Sorting);");
            sb.Append("SET @UserQueryId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "UserQueryId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, userQueryInfo.GroupId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userQueryInfo.UserId);
                    db.AddInParameter(dbCommand, "UserQueryName", DbType.String, userQueryInfo.UserQueryName);
                    db.AddInParameter(dbCommand, "UserQueryCode", DbType.String, userQueryInfo.UserQueryCode);
                    db.AddInParameter(dbCommand, "QueryShowType", DbType.Byte, userQueryInfo.QueryShowType);
                    db.AddInParameter(dbCommand, "RecommendType", DbType.Byte, userQueryInfo.RecommendType);
                    db.AddInParameter(dbCommand, "TableNameRelation", DbType.Int64, userQueryInfo.TableNameRelation);
                    db.AddInParameter(dbCommand, "IsGroup", DbType.Boolean, userQueryInfo.IsGroup);
                    db.AddInParameter(dbCommand, "IsDistinct", DbType.Boolean, userQueryInfo.IsDistinct);
                    db.AddInParameter(dbCommand, "EneableCondition", DbType.Boolean, userQueryInfo.EneableCondition);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, userQueryInfo.Notes);
                    db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, userQueryInfo.Sorting);
                    //执行插入操作
                    int count = 0;
                    if (transaction != null)
                    {
                        count = db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        count = db.ExecuteNonQuery(dbCommand);
                    }
                    if (count != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                    userQueryId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@UserQueryId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userQueryId;
        }
        #endregion

        #endregion
    }
}
