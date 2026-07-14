//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CombinedTableRelation.cs
// 描述: CombinedTableRelation 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/8/15
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
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CombinedTableRelation 表的数据层访问类
    /// </summary>
    public class CombinedTableRelation : CorrelatedTableDataAcess, ICombinedTableRelation
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CombinedTableRelation() : base("CombinedTableRelation", "CombinedTableId", "TableId", "Sorting")
        {
		}

        #endregion

        #region 实现默认接口       

        /// <summary>
		/// 获得 CombinedTableRelationInfo 对象
		/// </summary>
		///<param name="combinedTableId"></param>
		///<param name="tableId">表编号</param>
		/// <returns> CombinedTableRelationInfo 对象</returns>
		public CombinedTableRelationInfo GetModelInfo(decimal combinedTableId, decimal tableId)
		{			
			CombinedTableRelationInfo  combinedTableRelationInfo = null;
			//生成选择语句
			StringBuilder sb = new StringBuilder();		
			sb.Append("SELECT Sorting ");
			sb.Append("FROM CombinedTableRelation ");
			sb.Append("WHERE CombinedTableId = @CombinedTableId AND TableId = @TableId");
			try
			{
				//获得系统数据库对象
				SqlDatabase db = DataAccessHelper.GetDatabase();
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableId);
					db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
					using (IDataReader dataReader = db.ExecuteReader(dbCommand))
					{
						if (dataReader.Read())
						{
							int sorting = DataConvertionHelper.GetInt(dataReader[0]);
							//创建 CombinedTableRelationInfo 对象
							combinedTableRelationInfo = new CombinedTableRelationInfo(combinedTableId, tableId, sorting);
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
            
			return combinedTableRelationInfo;
		}
        
        /// <summary>
		/// 更新 CombinedTableRelationInfo 对象
		/// </summary>
		/// <param name="combinedTableRelationInfo">CombinedTableRelationInfo 对象</param>
		public void Update(CombinedTableRelationInfo combinedTableRelationInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE CombinedTableRelation SET Sorting = @Sorting ");
			sb.Append("WHERE CombinedTableId = @CombinedTableId AND TableId = @TableId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableRelationInfo.CombinedTableId);
					db.AddInParameter(dbCommand, "TableId", DbType.Decimal, combinedTableRelationInfo.TableId);
					db.AddInParameter(dbCommand, "Sorting", DbType.Int32, combinedTableRelationInfo.Sorting);
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
		///  删除 CombinedTableRelationInfo 对象
		/// </summary>
	    ///<param name="combinedTableId"></param>
		///<param name="tableId">表编号</param>
		public void Delete(decimal combinedTableId, decimal tableId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM CombinedTableRelation ");
			sb.Append("WHERE CombinedTableId = @CombinedTableId AND TableId = @TableId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableId);
					db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
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
		/// 获得 CombinedTableRelationInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CombinedTableRelationInfo 对象列表</returns>
		public IList<CombinedTableRelationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 CombinedTableRelation 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CombinedTableRelationInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CombinedTableRelation ", "CombinedTableId", false, whereConditons);
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
        /// 获得不同类型的表的数量
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataTableType"></param>
        /// <returns></returns>
        public int GetTableCountByTableType(decimal combinedTableId, DataTableType dataTableType)
        {
            int count = 0;

            try
            {
                string sqlSelect = "SELECT COUNT(1) FROM CombinedTableRelation INNER JOIN CustomTable ON CombinedTableRelation.TableId = CustomTable.TableId WHERE CombinedTableId = @CombinedTableId AND TableType = @TableType";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableId);
                    db.AddInParameter(dbCommand, "TableType", DbType.Byte, (byte)dataTableType);
                    count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
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
        /// 获得组合表列表
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public IList<CombinedTableRelationInfo> GetModelInfos(decimal combinedTableId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("CombinedTableId", "CombinedTableId", DbType.Decimal, combinedTableId, DataFieldCondition.Equal));

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 根据组合表的信息
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetTables(decimal combinedTableId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomTable.TableId, CustomDatabase.DataWarehouseId, CustomDatabase.DatabaseName, CustomCategory.CategoryName,  ");
            sb.Append("CustomTable.LogicalName, CustomTable.PhysicalName, CustomTable.TableType FROM CombinedTableRelation ");
            sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = CombinedTableRelation.TableId ");
            sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("WHERE CombinedTableId = @CombinedTableId ORDER BY CombinedTableRelation.Sorting ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            byte dataWarehouseId = DataConvertionHelper.GetByte(dataReader[1]);
                            string databaseName = DataConvertionHelper.GetString(dataReader[2]);
                            string categoryName = DataConvertionHelper.GetString(dataReader[3]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[4]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[5]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[6]);
                            commonNodes.Add(new CommonNode(tableId, combinedTableId, string.Format("[{0}][{1}][{2}][{3}]",
                                UserEnumHelper.GetEnumText((DataWarehouse)dataWarehouseId), databaseName, categoryName, logicalName),
                                physicalName, true, tableType));
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

            return commonNodes;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 插入组合表与表的关系
        /// </summary>
        /// <param name="combinedTableRelationInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Insert(IList<CombinedTableRelationInfo> combinedTableRelationInfos, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CombinedTableRelation(CombinedTableId, TableId, Sorting)");
            sb.Append("VALUES (@CombinedTableId, @TableId, @Sorting)");

            try
            {
                int sorting = 1;
                foreach (var obj in combinedTableRelationInfos)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, obj.CombinedTableId);
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, obj.TableId);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting++);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
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
		/// 向 CombinedTableRelation 表中插入一条新记录
		/// </summary>
		/// <param name="combinedTableRelationInfo">combinedTableRelationInfo 对象</param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
		public void Insert(CombinedTableRelationInfo combinedTableRelationInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CombinedTableRelation(CombinedTableId, TableId, Sorting)");
            sb.Append("VALUES (@CombinedTableId, @TableId, @Sorting)");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "TableId", DbType.Int32, combinedTableRelationInfo.TableId);
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Int32, combinedTableRelationInfo.CombinedTableId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, combinedTableRelationInfo.Sorting);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
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
        /// 更新 CombinedTableRelationInfo 对象
        /// </summary>
        /// <param name="combinedTableRelationInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
		public void Update(CombinedTableRelationInfo combinedTableRelationInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CombinedTableRelation SET Sorting = @Sorting ");
            sb.Append("WHERE CombinedTableId = @CombinedTableId AND TableId = @TableId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableRelationInfo.CombinedTableId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, combinedTableRelationInfo.TableId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, combinedTableRelationInfo.Sorting);
                    //执行更新操作
                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
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
        /// 删除 CombinedTableRelationInfo 对象
        /// </summary>
        /// <param name="combinedTableRelationInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
		public void Delete(CombinedTableRelationInfo combinedTableRelationInfo, SqlDatabase db, DbTransaction transaction)
        {
            //删除语句
            string sqlDelete = "DELETE FROM CombinedTableRelation WHERE CombinedTableId = @CombinedTableId AND TableId = @TableId";

            try
            {
                CombinedDataField combinedDataField = new CombinedDataField();
                combinedDataField.DeleteByTableId(combinedTableRelationInfo.TableId, db, transaction);
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableRelationInfo.CombinedTableId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, combinedTableRelationInfo.TableId);
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

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 CombinedTableRelationInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CombinedTableRelationInfo 对象列表</returns>
        private IList<CombinedTableRelationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CombinedTableRelationInfo>  combinedTableRelationInfos = new List<CombinedTableRelationInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CombinedTableRelation");
            
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
							decimal combinedTableId = DataConvertionHelper.GetDecimal(dataReader[0]);
							decimal tableId = DataConvertionHelper.GetDecimal(dataReader[1]);
							int sorting = DataConvertionHelper.GetInt(dataReader[2]);
							//将创建 CombinedTableRelationInfo 对象加入集合中
							combinedTableRelationInfos.Add(new CombinedTableRelationInfo(combinedTableId, tableId, sorting));							
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
            
			return combinedTableRelationInfos;
		}
        
		
		/// <summary>
		/// 获得 CombinedTableRelationInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CombinedTableRelationInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CombinedTableRelation");
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
        /// 获得表 CombinedTableRelation 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedTableRelation ", "CombinedTableId", "*", false, false, startPosition, 
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
        /// 获得以表 CombinedTableRelation 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedTableRelation ", "CombinedTableId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CombinedTableRelation 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedTableRelation ", "CombinedTableId", "*", false, false, startPosition, 
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
        /// 获得以表 CombinedTableRelation 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedTableRelation ", "CombinedTableId", "*", false, false, tableLinks, startPosition, 
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
		/// 删除满足条件的的 CombinedTableRelationInfo 对象
		/// </summary>
	    /// <param name="combinedTableId"></param>
		/// <returns>返回删除的记录数目数目</returns>
		private int Delete(decimal combinedTableId)
		{
			int count = 0; 
			//删除语句
			string sqlDelete = "DELETE FROM CombinedTableRelation WHERE CombinedTableId = @CombinedTableId";
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
				{
					db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableId);
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
        /// 删除满足条件的所有  CombinedTableRelationInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CombinedTableRelation");
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
