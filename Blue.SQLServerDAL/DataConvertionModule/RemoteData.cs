//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RemoteData.cs
// 描述: RemoteData 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/10/27
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
    /// RemoteData 表的数据层访问类
    /// </summary>
    public class RemoteData : CommonNodeDataAccess, IRemoteData
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public RemoteData() : base("RemoteData", "RemoteDataId", "GroupId", "RemoteDataName", "RemoteDataCode", false, true, "RemoteProperty")
        {
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 RemoteData 表中插入一条新记录
		/// </summary>
		/// <param name="remoteDataInfo">remoteDataInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(RemoteDataInfo remoteDataInfo)
		{
			//自动增加的关键字的值
			decimal remoteDataId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "DataRelation", "Sorting", "GroupId", remoteDataInfo.GroupId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();			
			sb.Append("INSERT INTO RemoteData(DatabaseId, GroupId, RemoteDataName, RemoteDataCode, RemoteProperty, RemoteAddress, RemoteUserName, ");
			sb.Append("RemotePassword, RemoteDatabaseId, Sorting, Notes)");
			sb.Append("VALUES (@DatabaseId, @GroupId, @RemoteDataName, @RemoteDataCode, @RemoteProperty, @RemoteAddress, @RemoteUserName, ");
			sb.Append("@RemotePassword, @RemoteDatabaseId, @Sorting, @Notes);");
			sb.Append("SET @RemoteDataId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "RemoteDataId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, remoteDataInfo.DatabaseId);
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, remoteDataInfo.GroupId);
                        db.AddInParameter(dbCommand, "RemoteDataName", DbType.String, remoteDataInfo.RemoteDataName);
                        db.AddInParameter(dbCommand, "RemoteDataCode", DbType.String, remoteDataInfo.RemoteDataCode);
                        db.AddInParameter(dbCommand, "RemoteProperty", DbType.Int64, remoteDataInfo.RemoteProperty);
                        db.AddInParameter(dbCommand, "RemoteAddress", DbType.String, remoteDataInfo.RemoteAddress);
                        db.AddInParameter(dbCommand, "RemoteUserName", DbType.String, remoteDataInfo.RemoteUserName);
                        db.AddInParameter(dbCommand, "RemotePassword", DbType.String, remoteDataInfo.RemotePassword);
                        db.AddInParameter(dbCommand, "RemoteDatabaseId", DbType.Decimal, remoteDataInfo.RemoteDatabaseId);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, remoteDataInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, remoteDataInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        remoteDataId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@RemoteDataId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(remoteDataInfo.GroupId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
            
			return remoteDataId;
		}

        /// <summary>
		/// 获得 RemoteDataInfo 对象
		/// </summary>
		///<param name="remoteDataId">远程数据交换编号</param>
		/// <returns> RemoteDataInfo 对象</returns>
		public RemoteDataInfo GetModelInfo(decimal remoteDataId)
		{			
			RemoteDataInfo  remoteDataInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("RemoteDataId", "RemoteDataId", DbType.Decimal, remoteDataId, DataFieldCondition.Equal));

            //创建集合对象
            IList<RemoteDataInfo>  remoteDataInfos = GetModelInfos(whereConditons, null, true);
            if (remoteDataInfos != null && remoteDataInfos.Count > 0)
            {
                remoteDataInfo = remoteDataInfos[0];
            }

            return remoteDataInfo;            
		}
        
        /// <summary>
		/// 更新 RemoteDataInfo 对象
		/// </summary>
		/// <param name="remoteDataInfo">RemoteDataInfo 对象</param>
		public void Update(RemoteDataInfo remoteDataInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE RemoteData SET DatabaseId = @DatabaseId, RemoteDataName = @RemoteDataName, RemoteDataCode = @RemoteDataCode, RemoteProperty = @RemoteProperty, ");
			sb.Append("RemoteAddress = @RemoteAddress, RemoteUserName = @RemoteUserName, RemotePassword = @RemotePassword, RemoteDatabaseId = @RemoteDatabaseId, Notes = @Notes ");
			sb.Append("");
			sb.Append("WHERE RemoteDataId = @RemoteDataId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "RemoteDataId", DbType.Decimal, remoteDataInfo.RemoteDataId);
                    db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, remoteDataInfo.DatabaseId);
                    db.AddInParameter(dbCommand, "RemoteDataName", DbType.String, remoteDataInfo.RemoteDataName);
					db.AddInParameter(dbCommand, "RemoteDataCode", DbType.String, remoteDataInfo.RemoteDataCode);
					db.AddInParameter(dbCommand, "RemoteProperty", DbType.Int64, remoteDataInfo.RemoteProperty);
					db.AddInParameter(dbCommand, "RemoteAddress", DbType.String, remoteDataInfo.RemoteAddress);
					db.AddInParameter(dbCommand, "RemoteUserName", DbType.String, remoteDataInfo.RemoteUserName);
					db.AddInParameter(dbCommand, "RemotePassword", DbType.String, remoteDataInfo.RemotePassword);
                    db.AddInParameter(dbCommand, "RemoteDatabaseId", DbType.Decimal, remoteDataInfo.RemoteDatabaseId);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, remoteDataInfo.Notes);
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
		///  删除 RemoteDataInfo 对象
		/// </summary>
	    ///<param name="remoteDataId">远程数据交换编号</param>
		public void Delete(decimal remoteDataId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM RemoteData ");
			sb.Append("WHERE RemoteDataId = @RemoteDataId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "RemoteDataId", DbType.Decimal, remoteDataId);
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
		/// 获得 RemoteDataInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RemoteDataInfo 对象列表</returns>
		public IList<RemoteDataInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 RemoteData 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>RemoteDataInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "RemoteData ", "RemoteDataId", false, whereConditons);
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
		/// 获得 RemoteDataInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>RemoteDataInfo 对象列表</returns>
		private IList<RemoteDataInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<RemoteDataInfo>  remoteDataInfos = new List<RemoteDataInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM RemoteData");
            
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
                            decimal remoteDataId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal databaseId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            string remoteDataName = DataConvertionHelper.GetString(dataReader[3]);
                            string remoteDataCode = DataConvertionHelper.GetString(dataReader[4]);
                            long remoteProperty = DataConvertionHelper.GetLong(dataReader[5]);
                            string remoteAddress = DataConvertionHelper.GetString(dataReader[6]);
                            string remoteUserName = DataConvertionHelper.GetString(dataReader[7]);
                            string remotePassword = DataConvertionHelper.GetString(dataReader[8]);
                            decimal remoteDatabaseId = DataConvertionHelper.GetDecimal(dataReader[9]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[10]);
                            string notes = DataConvertionHelper.GetString(dataReader[11]);
                            //将创建 RemoteDataInfo 对象加入集合中
                            remoteDataInfos.Add(new RemoteDataInfo(remoteDataId, databaseId, groupId, remoteDataName, remoteDataCode,
                            remoteProperty, remoteAddress, remoteUserName, remotePassword, remoteDatabaseId,
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
            
			return remoteDataInfos;
		}
        
		
		/// <summary>
		/// 获得 RemoteDataInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>RemoteDataInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM RemoteData");
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
        /// 获得表 RemoteData 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RemoteData ", "RemoteDataId", "*", false, false, startPosition, 
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
        /// 获得以表 RemoteData 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RemoteData ", "RemoteDataId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 RemoteData 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RemoteData ", "RemoteDataId", "*", false, false, startPosition, 
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
        /// 获得以表 RemoteData 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RemoteData ", "RemoteDataId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  RemoteDataInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RemoteData");
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
