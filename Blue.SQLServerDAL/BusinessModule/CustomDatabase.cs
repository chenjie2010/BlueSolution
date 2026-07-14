//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDatabase.cs
// 描述：CustomDatabase 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/9/11
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
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomDatabase 表的数据层访问类
    /// </summary>
    public class CustomDatabase : CommonNodeDataAccess, ICustomDatabase
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomDatabase() : base("CustomDatabase", "DatabaseId", "DataWarehouseId", "DatabaseName", "DatabaseCode")
        {
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 CustomDatabase 表中插入一条新记录
		/// </summary>
		/// <param name="customDatabaseInfo">customDatabaseInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomDatabaseInfo customDatabaseInfo)
		{
			//自动增加的关键字的值
			decimal customDatabaseId= 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomDatabase", "Sorting", "DataWarehouseId", customDatabaseInfo.DataWarehouseId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();			
			sb.Append("INSERT INTO CustomDatabase(DatabaseName, DatabaseCode, DataWarehouseId, IsLeaf, Sorting, Notes)");
			sb.Append("VALUES (@DatabaseName, @DatabaseCode, @DataWarehouseId, @IsLeaf, @Sorting, @Notes);");
			sb.Append("SET @DatabaseId = SCOPE_IDENTITY()");

			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddOutParameter(dbCommand, "DatabaseId", DbType.Decimal,8);
					db.AddInParameter(dbCommand, "DatabaseName", DbType.String, customDatabaseInfo.DatabaseName);
					db.AddInParameter(dbCommand, "DatabaseCode", DbType.String, customDatabaseInfo.DatabaseCode);
					db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, customDatabaseInfo.DataWarehouseId);
                    db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, true);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);                    
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customDatabaseInfo.Notes);
					//执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
					customDatabaseId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@DatabaseId"].Value, 0);
				}
			}
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
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
			CustomDatabaseInfo customDatabaseInfo = null;            

            IList<WhereConditon> whereConditons = new List<WhereConditon>();            
            //给参数赋值
            whereConditons.Add(new WhereConditon("DatabaseId", "DatabaseId", System.Data.DbType.Decimal, databaseId, DataFieldCondition.Equal));
            
            //创建集合对象
			IList<CustomDatabaseInfo> customDatabaseInfos = GetModelInfos(whereConditons, null, true);
            if (customDatabaseInfos != null && customDatabaseInfos.Count > 0)
            {
                customDatabaseInfo = customDatabaseInfos[0];
            }          

            return customDatabaseInfo;
		}
        
        /// <summary>
		/// 更新 CustomDatabaseInfo 对象
		/// </summary>
		/// <param name="customDatabaseInfo">CustomDatabaseInfo 对象</param>
		public void Update(CustomDatabaseInfo customDatabaseInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE CustomDatabase SET DatabaseName = @DatabaseName, DatabaseCode = @DatabaseCode, Notes = @Notes ");
			sb.Append("WHERE DatabaseId = @DatabaseId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, customDatabaseInfo.DatabaseId);
					db.AddInParameter(dbCommand, "DatabaseName", DbType.String, customDatabaseInfo.DatabaseName);
					db.AddInParameter(dbCommand, "DatabaseCode", DbType.String, customDatabaseInfo.DatabaseCode);
					db.AddInParameter(dbCommand, "Notes", DbType.String, customDatabaseInfo.Notes);
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
		///  删除 CustomDatabaseInfo 对象
		/// </summary>
	    ///<param name="databaseId">数据库编号</param>
		public void Delete(decimal databaseId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM CustomDatabase ");
			sb.Append("WHERE DatabaseId = @DatabaseId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, databaseId);
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
		/// 获得 CustomDatabaseInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDatabaseInfo 对象列表</returns>
		public IList<CustomDatabaseInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			return GetModelInfos(whereConditons, sortingCondtions, false);
		}               
        
        /// <summary>
		/// 获得 CustomDatabase 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomDatabaseInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomDatabase ", "DatabaseId", false, whereConditons);
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
        /// 获得数据库的逻辑名称
        /// </summary>
        ///<param name="databaseId">数据库编号</param>
        /// <returns> 数据库的逻辑名称</returns>
        public string GetDatabaseName(decimal databaseId)
        {
            string databaseName = string.Empty;

            try
            {
                string sqlSelect = "SELECT DatabaseName FROM CustomDatabase WHERE DatabaseId = @DatabaseId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, databaseId);
                    databaseName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
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

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string sqlSelect = "SELECT DatabaseId, DatabaseName, DatabaseCode FROM CustomDatabase WHERE DataWarehouseId = @DataWarehouseId";
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, dataWarehouseId);
                    ds = db.ExecuteDataSet(dbCommand);
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

        /// <summary>
        /// 更新节点是否为叶子节点的状态
        /// </summary>        
        /// <param name="nodeId">编号</param>
        /// <param name="isLeaf">是否为叶子节点</param>
        /// <param name="db">数据库对象</param>
        /// <param name="transaction">事务对象</param>
        public void UpdateLeaf(decimal nodeId, bool isLeaf, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            string sqlUpdate = "UPDATE CustomDatabase SET IsLeaf = @IsLeaf WHERE DatabaseId = @DatabaseId AND IsLeaf != @IsLeaf";
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, DataConvertionHelper.SetDecimal(nodeId));
                    db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, isLeaf);
                    //执行更新操作
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomDatabaseInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomDatabaseInfo 对象列表</returns>
        private IList<CustomDatabaseInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CustomDatabaseInfo>  customDatabaseInfos = new List<CustomDatabaseInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            
            sb.Append(" * FROM CustomDatabase");
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
                            decimal databaseId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string databaseName = DataConvertionHelper.GetString(dataReader[1]);
                            string databaseCode = DataConvertionHelper.GetString(dataReader[2]);
                            byte dataWarehouseId = DataConvertionHelper.GetByte(dataReader[3]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[4]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[5]);
                            string notes = DataConvertionHelper.GetString(dataReader[6]);
                            //将创建 CustomDatabaseInfo 对象加入集合中
                            customDatabaseInfos.Add(new CustomDatabaseInfo(databaseId, databaseName, databaseCode, dataWarehouseId, isLeaf,
                            sorting, notes));
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
            
			return customDatabaseInfos;
		} 
        
        /// <summary>
		/// 获得 CustomDatabaseInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CustomDatabaseInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomDatabase");
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
        /// 获得表 CustomDatabase 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomDatabase ", "DatabaseId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomDatabase 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomDatabase ", "DatabaseId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomDatabase 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomDatabase ", "DatabaseId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomDatabase 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomDatabase ", "DatabaseId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomDatabaseInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomDatabase");
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
