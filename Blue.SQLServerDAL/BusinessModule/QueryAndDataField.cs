//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: QueryAndDataField.cs
// 描述: QueryAndDataField 数据层访问类
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
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// QueryAndDataField 表的数据层访问类
    /// </summary>
    public class QueryAndDataField : IQueryAndDataField
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public QueryAndDataField()
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 QueryAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="queryAndDataFieldInfo">queryAndDataFieldInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(QueryAndDataFieldInfo queryAndDataFieldInfo)
        {
            //自动增加的关键字的值
            decimal queryAndDataFieldId = 0;
            
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            Insert(queryAndDataFieldInfo, db, null);

            return queryAndDataFieldId;
        }

        /// <summary>
		/// 获得 QueryAndDataFieldInfo 对象
		/// </summary>
		///<param name="queryAndDataFieldId">查询字段编号</param>
		/// <returns> QueryAndDataFieldInfo 对象</returns>
		public QueryAndDataFieldInfo GetModelInfo(decimal queryAndDataFieldId)
        {
            QueryAndDataFieldInfo queryAndDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("QueryAndDataFieldId", "QueryAndDataFieldId", DbType.Decimal, queryAndDataFieldId, DataFieldCondition.Equal));

            //创建集合对象
            IList<QueryAndDataFieldInfo> queryAndDataFieldInfos = GetModelInfos(whereConditons, null, true);
            if (queryAndDataFieldInfos != null && queryAndDataFieldInfos.Count > 0)
            {
                queryAndDataFieldInfo = queryAndDataFieldInfos[0];
            }

            return queryAndDataFieldInfo;
        }

        /// <summary>
        /// 更新 QueryAndDataFieldInfo 对象
        /// </summary>
        /// <param name="queryAndDataFieldInfo">QueryAndDataFieldInfo 对象</param>
        public void Update(QueryAndDataFieldInfo queryAndDataFieldInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE QueryAndDataField SET DataFieldId = @DataFieldId, UserQueryId = @UserQueryId, DataFieldProperty = @DataFieldProperty, ");
            sb.Append("SystemDataField = @SystemDataField, DataFieldType = @DataFieldType, IsOutput = @IsOutput, ");
            sb.Append("CustomAggregate = @CustomAggregate, SortingType = @SortingType, Condition = @Condition, ");
            sb.Append("DataFieldInnerRealtion = @DataFieldInnerRealtion, Sorting = @Sorting ");
            sb.Append("WHERE QueryAndDataFieldId = @QueryAndDataFieldId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "QueryAndDataFieldId", DbType.Decimal, queryAndDataFieldInfo.QueryAndDataFieldId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, queryAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "UserQueryId", DbType.Decimal, queryAndDataFieldInfo.UserQueryId);
                    db.AddInParameter(dbCommand, "DataFieldProperty", DbType.Byte, queryAndDataFieldInfo.DataFieldProperty);
                    db.AddInParameter(dbCommand, "SystemDataField", DbType.Byte, queryAndDataFieldInfo.SystemDataField);
                    db.AddInParameter(dbCommand, "DataFieldType", DbType.Byte, queryAndDataFieldInfo.DataFieldType);
                    db.AddInParameter(dbCommand, "IsOutput", DbType.Boolean, queryAndDataFieldInfo.IsOutput);
                    db.AddInParameter(dbCommand, "CustomAggregate", DbType.Byte, queryAndDataFieldInfo.CustomAggregate);
                    db.AddInParameter(dbCommand, "SortingType", DbType.Byte, queryAndDataFieldInfo.SortingType);
                    db.AddInParameter(dbCommand, "Condition", DbType.String, queryAndDataFieldInfo.Condition);
                    db.AddInParameter(dbCommand, "DataFieldInnerRealtion", DbType.Byte, queryAndDataFieldInfo.DataFieldInnerRealtion);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, queryAndDataFieldInfo.Sorting);
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
        ///  删除 QueryAndDataFieldInfo 对象
        /// </summary>
        ///<param name="queryAndDataFieldId">查询字段编号</param>
        public void Delete(decimal queryAndDataFieldId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM QueryAndDataField ");
            sb.Append("WHERE QueryAndDataFieldId = @QueryAndDataFieldId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "QueryAndDataFieldId", DbType.Decimal, queryAndDataFieldId);
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
        /// 获得 QueryAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>QueryAndDataFieldInfo 对象列表</returns>
        public IList<QueryAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 QueryAndDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>QueryAndDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "QueryAndDataField ", "QueryAndDataFieldId", false, whereConditons);
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
        /// 获得 QueryAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="userQueryId"></param>
        /// <returns></returns>
        public IList<QueryAndDataFieldInfo> GetModelInfos(decimal userQueryId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>(2);
            whereConditons.Add(new WhereConditon("UserQueryId", "UserQueryId",DbType.Decimal, userQueryId,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions);


        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 删除相关字段
        /// </summary>
        /// <param name="userQueryId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(decimal userQueryId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM QueryAndDataField ");
            sb.Append("WHERE UserQueryId = @UserQueryId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserQueryId", DbType.Decimal, userQueryId);
                    //执行删除操作
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 向 QueryAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="queryAndDataFieldInfo">queryAndDataFieldInfo 对象</param>
        /// <param name="db">数据库对象</param>
        /// <param name="transaction">事物管理</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(QueryAndDataFieldInfo queryAndDataFieldInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal queryAndDataFieldId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO QueryAndDataField(DataFieldId, UserQueryId, TableId, DataFieldProperty, SystemDataField, DataFieldType, ");
            sb.Append("IsOutput, CustomAggregate, SortingType, Condition, DataFieldInnerRealtion, ");
            sb.Append("Sorting)");
            sb.Append("VALUES (@DataFieldId, @UserQueryId, @TableId, @DataFieldProperty, @SystemDataField, @DataFieldType, ");
            sb.Append("@IsOutput, @CustomAggregate, @SortingType, @Condition, @DataFieldInnerRealtion, ");
            sb.Append("@Sorting);");
            sb.Append("SET @QueryAndDataFieldId = SCOPE_IDENTITY()");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "QueryAndDataFieldId", DbType.Decimal, 12);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(queryAndDataFieldInfo.DataFieldId));
                    db.AddInParameter(dbCommand, "UserQueryId", DbType.Decimal, queryAndDataFieldInfo.UserQueryId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(queryAndDataFieldInfo.TableId));
                    db.AddInParameter(dbCommand, "DataFieldProperty", DbType.Byte, queryAndDataFieldInfo.DataFieldProperty);
                    db.AddInParameter(dbCommand, "SystemDataField", DbType.Byte, queryAndDataFieldInfo.SystemDataField);
                    db.AddInParameter(dbCommand, "DataFieldType", DbType.Byte, queryAndDataFieldInfo.DataFieldType);
                    db.AddInParameter(dbCommand, "IsOutput", DbType.Boolean, queryAndDataFieldInfo.IsOutput);
                    db.AddInParameter(dbCommand, "CustomAggregate", DbType.Byte, queryAndDataFieldInfo.CustomAggregate);
                    db.AddInParameter(dbCommand, "SortingType", DbType.Byte, queryAndDataFieldInfo.SortingType);
                    db.AddInParameter(dbCommand, "Condition", DbType.String, queryAndDataFieldInfo.Condition);
                    db.AddInParameter(dbCommand, "DataFieldInnerRealtion", DbType.Byte, queryAndDataFieldInfo.DataFieldInnerRealtion);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, queryAndDataFieldInfo.Sorting);
                    //执行插入操作
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
                    queryAndDataFieldId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@QueryAndDataFieldId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return queryAndDataFieldId;
        }

        #endregion

            #region 私有方法

            #region 默认私有方法	

            /// <summary>
            /// 获得 QueryAndDataFieldInfo 对象的列表
            /// </summary>	
            /// <param name="whereConditons">查询字段条件的集合</param>
            /// <param name="sortingCondtions">排序字段条件的集合</param>
            /// <param name="onlyOne">第一条记录</param>
            /// <returns>QueryAndDataFieldInfo 对象列表</returns>
        private IList<QueryAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<QueryAndDataFieldInfo>  queryAndDataFieldInfos = new List<QueryAndDataFieldInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM QueryAndDataField");
            
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
							decimal queryAndDataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
							decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
							decimal userQueryId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            byte dataFieldProperty = DataConvertionHelper.GetByte(dataReader[4]);
							byte systemDataField = DataConvertionHelper.GetByte(dataReader[5]);
							byte dataFieldType = DataConvertionHelper.GetByte(dataReader[6]);
							bool isOutput = DataConvertionHelper.GetBoolean(dataReader[7]);
							byte customAggregate = DataConvertionHelper.GetByte(dataReader[8]);
							byte sortingType = DataConvertionHelper.GetByte(dataReader[9]);
							string condition = DataConvertionHelper.GetString(dataReader[10]);
							byte dataFieldInnerRealtion = DataConvertionHelper.GetByte(dataReader[11]);
							int sorting = DataConvertionHelper.GetInt(dataReader[12]);
							//将创建 QueryAndDataFieldInfo 对象加入集合中
							queryAndDataFieldInfos.Add(new QueryAndDataFieldInfo(queryAndDataFieldId, dataFieldId, userQueryId, tableId, dataFieldProperty, systemDataField, 
							dataFieldType, isOutput, customAggregate, sortingType, condition, 
							dataFieldInnerRealtion, sorting));							
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
            
			return queryAndDataFieldInfos;
		}
        
		
		/// <summary>
		/// 获得 QueryAndDataFieldInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>QueryAndDataFieldInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM QueryAndDataField");
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
        /// 获得表 QueryAndDataField 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "QueryAndDataField", "QueryAndDataFieldId", "*", false, false, startPosition, 
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
        /// 获得以表 QueryAndDataField 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "QueryAndDataField ", "QueryAndDataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 QueryAndDataField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "QueryAndDataField ", "QueryAndDataFieldId", "*", false, false, startPosition, 
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
        /// 获得以表 QueryAndDataField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "QueryAndDataField ", "QueryAndDataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  QueryAndDataFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM QueryAndDataField");
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
