//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataRelation.cs
// 描述: DataRelation 数据层访问类
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
using AppFramework.Reference.DataAccessLibrary;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.DataConvertionModule;
using Blue.Model.DataConvertionModule;
using Blue.SQLServerDAL.BusinessModule;

namespace Blue.SQLServerDAL.DataConvertionModule
{
    /// <summary>
    /// DataRelation 表的数据层访问类
    /// </summary>
    public class DataRelation : CommonNodeDataAccess, IDataRelation
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public DataRelation() : base("DataRelation", "RelationId", "GroupId", "RelationName", "RelationCode", false, true)
        {
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 DataRelation 表中插入一条新记录
		/// </summary>
		/// <param name="dataRelationInfo">dataRelationInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(DataRelationInfo dataRelationInfo)
		{
			//自动增加的关键字的值
			decimal dataRelationId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "DataRelation", "Sorting", "GroupId", dataRelationInfo.GroupId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();			
			sb.Append("INSERT INTO DataRelation(GroupId, DatabaseId, ParentDatabaseId, RelationName, RelationCode, ");
			sb.Append("DataRelationType, DataRelationProperty, Sorting, Notes)");
			sb.Append("VALUES (@GroupId, @DatabaseId, @ParentDatabaseId, @RelationName, @RelationCode, ");
			sb.Append("@DataRelationType, @DataRelationProperty, @Sorting, @Notes);");
			sb.Append("SET @RelationId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "RelationId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, dataRelationInfo.GroupId);
                        db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, dataRelationInfo.DatabaseId);
                        db.AddInParameter(dbCommand, "ParentDatabaseId", DbType.Decimal, dataRelationInfo.ParentDatabaseId);
                        db.AddInParameter(dbCommand, "RelationName", DbType.String, dataRelationInfo.RelationName);
                        db.AddInParameter(dbCommand, "RelationCode", DbType.String, dataRelationInfo.RelationCode);
                        db.AddInParameter(dbCommand, "DataRelationType", DbType.Byte, dataRelationInfo.DataRelationType);
                        db.AddInParameter(dbCommand, "DataRelationProperty", DbType.Byte, dataRelationInfo.DataRelationProperty);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, dataRelationInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, dataRelationInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        dataRelationId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@RelationId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(dataRelationInfo.GroupId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
            
			return dataRelationId;
		}

        /// <summary>
		/// 获得 DataRelationInfo 对象
		/// </summary>
		///<param name="relationId">关系编号</param>
		/// <returns> DataRelationInfo 对象</returns>
		public DataRelationInfo GetModelInfo(decimal relationId)
		{			
			DataRelationInfo  dataRelationInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("RelationId", "RelationId", DbType.Decimal, relationId, DataFieldCondition.Equal));

            //创建集合对象
            IList<DataRelationInfo>  dataRelationInfos = GetModelInfos(whereConditons, null, true);
            if (dataRelationInfos != null && dataRelationInfos.Count > 0)
            {
                dataRelationInfo = dataRelationInfos[0];
            }

            return dataRelationInfo;            
		}
        
        /// <summary>
		/// 更新 DataRelationInfo 对象
		/// </summary>
		/// <param name="dataRelationInfo">DataRelationInfo 对象</param>
		public void Update(DataRelationInfo dataRelationInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE DataRelation SET GroupId = @GroupId, DatabaseId = @DatabaseId, ParentDatabaseId = @ParentDatabaseId, ");
			sb.Append("RelationName = @RelationName, RelationCode = @RelationCode, DataRelationType = @DataRelationType, ");
			sb.Append("DataRelationProperty = @DataRelationProperty, Sorting = @Sorting, Notes = @Notes ");
			sb.Append("WHERE RelationId = @RelationId");
            DataRelationInfo oldDataRelationInfo = GetModelInfo(dataRelationInfo.RelationId);            
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "RelationId", DbType.Decimal, dataRelationInfo.RelationId);
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, dataRelationInfo.GroupId);
                        db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, dataRelationInfo.DatabaseId);
                        db.AddInParameter(dbCommand, "ParentDatabaseId", DbType.Decimal, dataRelationInfo.ParentDatabaseId);
                        db.AddInParameter(dbCommand, "RelationName", DbType.String, dataRelationInfo.RelationName);
                        db.AddInParameter(dbCommand, "RelationCode", DbType.String, dataRelationInfo.RelationCode);
                        db.AddInParameter(dbCommand, "DataRelationType", DbType.Byte, dataRelationInfo.DataRelationType);
                        db.AddInParameter(dbCommand, "DataRelationProperty", DbType.Byte, dataRelationInfo.DataRelationProperty);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, dataRelationInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, dataRelationInfo.Notes);
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }
                    if (oldDataRelationInfo.DatabaseId != dataRelationInfo.DatabaseId || oldDataRelationInfo.ParentDatabaseId != dataRelationInfo.ParentDatabaseId)
                    {
                        DataFieldRelation dataFieldRelation = new DataFieldRelation();
                        dataFieldRelation.Delete(dataRelationInfo.RelationId, db, transaction);
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
		///  删除 DataRelationInfo 对象
		/// </summary>
	    ///<param name="relationId">关系编号</param>
		public void Delete(decimal relationId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM DataRelation ");
			sb.Append("WHERE RelationId = @RelationId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "RelationId", DbType.Decimal, relationId);
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
		/// 获得 DataRelationInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataRelationInfo 对象列表</returns>
		public IList<DataRelationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 DataRelation 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>DataRelationInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "DataRelation ", "RelationId", false, whereConditons);
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
		/// 获得 DataRelationInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>DataRelationInfo 对象列表</returns>
		private IList<DataRelationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<DataRelationInfo>  dataRelationInfos = new List<DataRelationInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM DataRelation");
            
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
							decimal relationId = DataConvertionHelper.GetDecimal(dataReader[0]);
							decimal groupId = DataConvertionHelper.GetDecimal(dataReader[1]);
							decimal databaseId = DataConvertionHelper.GetDecimal(dataReader[2]);
							decimal parentDatabaseId = DataConvertionHelper.GetDecimal(dataReader[3]);
							string relationName = DataConvertionHelper.GetString(dataReader[4]);
							string relationCode = DataConvertionHelper.GetString(dataReader[5]);
							byte dataRelationType = DataConvertionHelper.GetByte(dataReader[6]);
							byte dataRelationProperty = DataConvertionHelper.GetByte(dataReader[7]);
							int sorting = DataConvertionHelper.GetInt(dataReader[8]);
							string notes = DataConvertionHelper.GetString(dataReader[9]);
							//将创建 DataRelationInfo 对象加入集合中
							dataRelationInfos.Add(new DataRelationInfo(relationId, groupId, databaseId, parentDatabaseId, relationName, 
							relationCode, dataRelationType, dataRelationProperty, sorting, notes));							
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
            
			return dataRelationInfos;
		}
        
		
		/// <summary>
		/// 获得 DataRelationInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>DataRelationInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM DataRelation");
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
        /// 获得表 DataRelation 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataRelation ", "RelationId", "*", false, false, startPosition, 
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
        /// 获得以表 DataRelation 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataRelation ", "RelationId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 DataRelation 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataRelation ", "RelationId", "*", false, false, startPosition, 
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
        /// 获得以表 DataRelation 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataRelation ", "RelationId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  DataRelationInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataRelation");
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
