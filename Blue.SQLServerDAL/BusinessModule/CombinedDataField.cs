//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CombinedDataField.cs
// 描述: CombinedDataField 数据层访问类
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
    /// CombinedDataField 表的数据层访问类
    /// </summary>
    public class CombinedDataField : CorrelatedTableDataAcess, ICombinedDataField
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CombinedDataField() :  base("CombinedDataField", "CombinedTableId", "DataFieldId")
        {
        }

        #endregion

        #region 实现默认接口
        
        /// <summary>
		/// 获得 CombinedDataFieldInfo 对象
		/// </summary>
		///<param name="combinedTableId"></param>
		///<param name="dataFieldId">字段编号</param>
		/// <returns> CombinedDataFieldInfo 对象</returns>
		public CombinedDataFieldInfo GetModelInfo(decimal combinedTableId, decimal dataFieldId)
        {
            CombinedDataFieldInfo combinedDataFieldInfo = null;
            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT Sorting ");
            sb.Append("FROM CombinedDataField ");
            sb.Append("WHERE CombinedTableId = @CombinedTableId AND DataFieldId = @DataFieldId");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            int sorting = DataConvertionHelper.GetInt(dataReader[0]);
                            //创建 CombinedDataFieldInfo 对象
                            combinedDataFieldInfo = new CombinedDataFieldInfo(combinedTableId, dataFieldId, sorting);
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

            return combinedDataFieldInfo;
        }
        
        /// <summary>
        /// 获得 CombinedDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CombinedDataFieldInfo 对象列表</returns>
        public IList<CombinedDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CombinedDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CombinedDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CombinedDataField ", "CombinedTableId", false, whereConditons);
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
        /// 获得组合表的字段
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public List<CommonNode> GetDataFields(decimal combinedTableId)
        {
            List<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CombinedDataField.DataFieldId, CustomTable.LogicalName, CustomDataField.LogicalName, CustomDataField.PhysicalName FROM CombinedDataField ");
            sb.Append("INNER JOIN CustomDataField ON CombinedDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = CustomDataField.TableId ");
            sb.Append("WHERE CombinedTableId = @CombinedTableId ORDER BY CombinedDataField.Sorting");
                        
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
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string logicalName = string.Format("[{0}][{1}]", DataConvertionHelper.GetString(dataReader[1]), 
                                DataConvertionHelper.GetString(dataReader[2]));
                            string physicalName = DataConvertionHelper.GetString(dataReader[3]);
                            commonNodes.Add(new CommonNode(dataFieldId, combinedTableId, logicalName, physicalName));
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

        /// <summary>
        /// 更新组合表的字段集合
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="combinedDataFieldInfos"></param>
        public void UpdateDataFields(decimal combinedTableId, IList<CombinedDataFieldInfo> combinedDataFieldInfos)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("CombinedTableId", "CombinedTableId", DbType.Decimal, combinedTableId, DataFieldCondition.Equal));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));
            IList<CombinedDataFieldInfo> oldCombinedDataFieldInfos = GetModelInfos(whereConditons, sortingCondtions);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    /* 1. 不存在则插入，存在则更新 */
                    foreach (var combinedDataFieldInfo in combinedDataFieldInfos)
                    {
                        bool find = false;
                        foreach (var oldCombinedDataFieldInfo in oldCombinedDataFieldInfos)
                        {
                            if (combinedDataFieldInfo.CombinedTableId == oldCombinedDataFieldInfo.CombinedTableId && combinedDataFieldInfo.DataFieldId == oldCombinedDataFieldInfo.DataFieldId)
                            {
                                find = true;
                                if (combinedDataFieldInfo.Sorting != oldCombinedDataFieldInfo.Sorting)
                                {
                                    Update(combinedDataFieldInfo, db, transaction);
                                }
                                break;
                            }
                        }
                        if (!find)
                        {
                            Insert(combinedDataFieldInfo, db, transaction);
                        }
                    }
                    /* 2. 存在则忽略，不存在则删除*/
                    foreach (var oldCombinedDataFieldInfo in oldCombinedDataFieldInfos)
                    {
                        bool find = false;
                        foreach (var combinedDataFieldInfo in combinedDataFieldInfos)
                        {
                            if (combinedDataFieldInfo.CombinedTableId == oldCombinedDataFieldInfo.CombinedTableId && combinedDataFieldInfo.DataFieldId == oldCombinedDataFieldInfo.DataFieldId)
                            {
                                find = true;
                                break;
                            }
                        }
                        if (!find)
                        {
                            Delete(oldCombinedDataFieldInfo.CombinedTableId, oldCombinedDataFieldInfo.DataFieldId, db, transaction);
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

        /// <summary>
        /// 删除满足条件的的 CombinedDataFieldInfo 对象
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
		public int DeleteByTableId(decimal tableId, SqlDatabase db, DbTransaction transaction)
        {
            int count = 0;

            //删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE CombinedDataField FROM CombinedDataField INNER JOIN CustomDataField ");
            sb.Append("ON CombinedDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE CustomDataField.TableId = @TableId ");
            
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    //执行删除操作
                    count = db.ExecuteNonQuery(dbCommand, transaction);
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

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 CombinedDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CombinedDataFieldInfo 对象列表</returns>
        private IList<CombinedDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CombinedDataFieldInfo>  combinedDataFieldInfos = new List<CombinedDataFieldInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CombinedDataField");
            
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
							decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
							int sorting = DataConvertionHelper.GetInt(dataReader[2]);
							//将创建 CombinedDataFieldInfo 对象加入集合中
							combinedDataFieldInfos.Add(new CombinedDataFieldInfo(combinedTableId, dataFieldId, sorting));							
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
            
			return combinedDataFieldInfos;
		}
        
		
		/// <summary>
		/// 获得 CombinedDataFieldInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CombinedDataFieldInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CombinedDataField");
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
        /// 获得表 CombinedDataField 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedDataField ", "CombinedTableId", "*", false, false, startPosition, 
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
        /// 获得以表 CombinedDataField 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedDataField ", "CombinedTableId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CombinedDataField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedDataField ", "CombinedTableId", "*", false, false, startPosition, 
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
        /// 获得以表 CombinedDataField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedDataField ", "CombinedTableId", "*", false, false, tableLinks, startPosition, 
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
		/// 删除满足条件的的 CombinedDataFieldInfo 对象
		/// </summary>
	    /// <param name="combinedTableId"></param>
		/// <returns>返回删除的记录数目数目</returns>
		private int Delete(decimal combinedTableId)
		{
			int count = 0; 
			//删除语句
			string sqlDelete = "DELETE FROM CombinedDataField WHERE CombinedTableId = @CombinedTableId";
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
        /// 删除满足条件的所有  CombinedDataFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CombinedDataField");
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
        /// 向 CustomViewAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="combinedDataFieldInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Insert(CombinedDataFieldInfo combinedDataFieldInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CombinedDataField(CombinedTableId, DataFieldId, Sorting) ");
            sb.Append("VALUES (@CombinedTableId, @DataFieldId, @Sorting)");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedDataFieldInfo.CombinedTableId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, combinedDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, combinedDataFieldInfo.Sorting);
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
        /// 更新 CustomViewAndDataFieldInfo 对象
        /// </summary>
        /// <param name="combinedDataFieldInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Update(CombinedDataFieldInfo combinedDataFieldInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CombinedDataField SET Sorting = @Sorting ");
            sb.Append("WHERE CombinedTableId = @CombinedTableId AND DataFieldId = @DataFieldId");
           
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedDataFieldInfo.CombinedTableId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, combinedDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, combinedDataFieldInfo.Sorting);
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

        #endregion

        #endregion
    }
}
