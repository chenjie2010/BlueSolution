//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomCategory.cs
// 描述：CustomCategory 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/9/17
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
    /// CustomCategory 表的数据层访问类
    /// </summary>
    public class CustomCategory : CommonNodeDataAccess, ICustomCategory
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomCategory(): base("CustomCategory", "CategoryId", "DatabaseId", "CategoryName", "CategoryCode")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomCategory 表中插入一条新记录
        /// </summary>
        /// <param name="customCategoryInfo">customCategoryInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomCategoryInfo customCategoryInfo)
        {
            //自动增加的关键字的值
            decimal customCategoryId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomCategory", "Sorting", "DatabaseId", customCategoryInfo.DatabaseId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomCategory(DatabaseId, CategoryName, CategoryCode, IsLeaf, ");
            sb.Append("Sorting, Notes)");
            sb.Append("VALUES (@DatabaseId, @CategoryName, @CategoryCode, @IsLeaf, ");
            sb.Append("@Sorting, @Notes);");
            sb.Append("SET @CategoryId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "CategoryId", DbType.Decimal, 8);
                        db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, customCategoryInfo.DatabaseId);
                        db.AddInParameter(dbCommand, "CategoryName", DbType.String, customCategoryInfo.CategoryName);
                        db.AddInParameter(dbCommand, "CategoryCode", DbType.String, customCategoryInfo.CategoryCode);
                        db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, true);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customCategoryInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customCategoryId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@CategoryId"].Value, 0);
                    }
                    CustomDatabase customDatabase = new CustomDatabase();
                    customDatabase.UpdateLeaf(customCategoryInfo.DatabaseId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customCategoryId;
        }

        /// <summary>
		/// 获得 CustomCategoryInfo 对象
		/// </summary>
		///<param name="categoryId">分类编号</param>
		/// <returns> CustomCategoryInfo 对象</returns>
		public CustomCategoryInfo GetModelInfo(decimal categoryId)
        {
            CustomCategoryInfo customCategoryInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("CategoryId", "CategoryId", System.Data.DbType.Decimal, categoryId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomCategoryInfo> customCategoryInfos = GetModelInfos(whereConditons, null, true);
            if (customCategoryInfos != null && customCategoryInfos.Count > 0)
            {
                customCategoryInfo = customCategoryInfos[0];
            }

            return customCategoryInfo;
        }

        /// <summary>
        /// 更新 CustomCategoryInfo 对象
        /// </summary>
        /// <param name="customCategoryInfo">CustomCategoryInfo 对象</param>
        public void Update(CustomCategoryInfo customCategoryInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomCategory SET DatabaseId = @DatabaseId, CategoryName = @CategoryName, CategoryCode = @CategoryCode, Notes = @Notes ");
            sb.Append("WHERE CategoryId = @CategoryId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CategoryId", DbType.Decimal, customCategoryInfo.CategoryId);
                    db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, customCategoryInfo.DatabaseId);
                    db.AddInParameter(dbCommand, "CategoryName", DbType.String, customCategoryInfo.CategoryName);
                    db.AddInParameter(dbCommand, "CategoryCode", DbType.String, customCategoryInfo.CategoryCode);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customCategoryInfo.Notes);
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
        ///  删除 CustomCategoryInfo 对象
        /// </summary>
        ///<param name="categoryId">分类编号</param>
        public void Delete(decimal categoryId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomCategory ");
            sb.Append("WHERE CategoryId = @CategoryId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "CategoryId", DbType.Decimal, categoryId);
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
        /// 获得 CustomCategoryInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomCategoryInfo 对象列表</returns>
        public IList<CustomCategoryInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomCategory 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomCategoryInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomCategory ", "CategoryId", false, whereConditons);
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
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="databaseIds"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(IList<decimal> databaseIds)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT CategoryId, CategoryName, CategoryCode FROM CustomCategory ");
                if (databaseIds.Count > 0)
                {
                    sb.Append("WHERE ");
                    for(int index = 0; index < databaseIds.Count; index++)
                    {
                        sb.AppendFormat("DatabaseId = @DatabaseId_{0} OR ", index);                        
                    }
                    sb.Remove(sb.Length - 4, 4);
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    int index = 0;
                    foreach (decimal databaseId in databaseIds)
                    {
                        db.AddInParameter(dbCommand, string.Format("DatabaseId_{0}", index), DbType.Decimal, databaseId);
                        index++;
                    }
                    ds = db.ExecuteDataSet(dbCommand);
                }

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

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal databaseId)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string sqlSelect = "SELECT CategoryId, CategoryName, CategoryCode FROM CustomCategory WHERE DatabaseId = @DatabaseId";
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, databaseId);
                    ds = db.ExecuteDataSet(dbCommand);
                }

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

        /// <summary>
        /// 获得数据库编号
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public decimal GetDatabaseId(decimal categoryId)
        {
            decimal databaseId = 0;

            string sqlSelect = "SELECT DatabaseId FROM CustomCategory WHERE CategoryId = @CategoryId";
           
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "CategoryId", DbType.Decimal, categoryId);
                    databaseId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return databaseId;
        }

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal categoryId)
        {
            byte dataWarehouseId = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DataWarehouseId FROM CustomDatabase INNER JOIN CustomCategory ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("WHERE CustomCategory.CategoryId = @CategoryId ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "CategoryId", DbType.Decimal, categoryId);
                    dataWarehouseId = DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataWarehouseId;
        }

        #endregion

        #endregion

        #region 公有方法
        
        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomCategoryInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomCategoryInfo 对象列表</returns>
        private IList<CustomCategoryInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomCategoryInfo> customCategoryInfos = new List<CustomCategoryInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomCategory");
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
                            decimal categoryId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal databaseId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string categoryName = DataConvertionHelper.GetString(dataReader[2]);
                            string categoryCode = DataConvertionHelper.GetString(dataReader[3]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[4]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[5]);
                            string notes = DataConvertionHelper.GetString(dataReader[6]);
                            //将创建 CustomCategoryInfo 对象加入集合中
                            customCategoryInfos.Add(new CustomCategoryInfo(categoryId, databaseId, categoryName, categoryCode, isLeaf,
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

            return customCategoryInfos;
        }

        /// <summary>
        /// 获得 CustomCategoryInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomCategoryInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomCategory");
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
        /// 获得表 CustomCategory 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomCategory ", "CategoryId", "*", false, false, startPosition,
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
        /// 获得以表 CustomCategory 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomCategory ", "CategoryId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomCategory 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomCategory ", "CategoryId", "*", false, false, startPosition,
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
        /// 获得以表 CustomCategory 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomCategory ", "CategoryId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomCategoryInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomCategory");
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
