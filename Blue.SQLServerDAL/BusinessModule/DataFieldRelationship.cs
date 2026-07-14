//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：DataFieldRelation.cs
// 描述：DataFieldRelation 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/4/8
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// DataFieldRelation 表的数据层访问类
    /// </summary>
    public class DataFieldRelationship : CorrelatedTableDataAcess, IDataFieldRelationship
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataFieldRelationship() : base("DataFieldRelationship", "ParentDataFieldId", "DataFieldId", "Sorting")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 DataFieldRelation 表中插入一条新记录
        /// </summary>
        /// <param name="dataFieldRelationInfo">dataFieldRelationInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(DataFieldRelationshipInfo dataFieldRelationInfo)
        {
            //自动增加的关键字的值
            decimal dataFieldRelationId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO DataFieldRelation(Sorting)");
            sb.Append("VALUES (@Sorting);");
            sb.Append("SET @DataFieldId = SCOPE_IDENTITY()");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, 8);
                    db.AddOutParameter(dbCommand, "DataFieldId", DbType.Decimal, 8);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, dataFieldRelationInfo.Sorting);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                    dataFieldRelationId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@DataFieldId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldRelationId;
        }

        /// <summary>
		/// 获得 DataFieldRelationInfo 对象
		/// </summary>
		///<param name="parentDataFieldId">字段编号</param>
		///<param name="dataFieldId">字段编号</param>
		/// <returns> DataFieldRelationInfo 对象</returns>
		public DataFieldRelationshipInfo GetModelInfo(decimal parentDataFieldId, decimal dataFieldId)
        {
            DataFieldRelationshipInfo dataFieldRelationInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ParentDataFieldId", "ParentDataFieldId", DbType.Decimal, parentDataFieldId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("DataFieldId", "DataFieldId", DbType.Decimal, dataFieldId, DataFieldCondition.Equal));

            //创建集合对象
            IList<DataFieldRelationshipInfo> dataFieldRelationInfos = GetModelInfos(whereConditons, null, true);
            if (dataFieldRelationInfos != null && dataFieldRelationInfos.Count > 0)
            {
                dataFieldRelationInfo = dataFieldRelationInfos[0];
            }

            return dataFieldRelationInfo;
        }

        /// <summary>
        /// 更新 DataFieldRelationInfo 对象
        /// </summary>
        /// <param name="dataFieldRelationInfo">DataFieldRelationInfo 对象</param>
        public void Update(DataFieldRelationshipInfo dataFieldRelationInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE DataFieldRelation SET Sorting = @Sorting ");
            sb.Append("WHERE ParentDataFieldId = @ParentDataFieldId AND DataFieldId = @DataFieldId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, dataFieldRelationInfo.ParentDataFieldId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldRelationInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, dataFieldRelationInfo.Sorting);
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
        ///  删除 DataFieldRelationInfo 对象
        /// </summary>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="dataFieldId">字段编号</param>
        public void Delete(decimal parentDataFieldId, decimal dataFieldId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataFieldRelation ");
            sb.Append("WHERE ParentDataFieldId = @ParentDataFieldId AND DataFieldId = @DataFieldId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, parentDataFieldId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
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
        /// 获得 DataFieldRelationInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataFieldRelationInfo 对象列表</returns>
        public IList<DataFieldRelationshipInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 DataFieldRelation 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>DataFieldRelationInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "DataFieldRelation ", "ParentDataFieldId", false, whereConditons);
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
        /// 更新联系字段
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <param name="dataFieldRelationshipInfos"></param>
        public void UpdateDataFields(decimal parentDataFieldId, IList<DataFieldRelationshipInfo> dataFieldRelationshipInfos)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("ParentDataFieldId", "ParentDataFieldId", DbType.Decimal, parentDataFieldId, DataFieldCondition.Equal));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));
            IList<DataFieldRelationshipInfo> oldDataFieldRelationshipInfos = GetModelInfos(whereConditons, sortingCondtions);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    /* 1. 不存在则插入，存在则更新 */
                    foreach (var dataFieldRelationshipInfo in dataFieldRelationshipInfos)
                    {
                        bool find = false;
                        foreach (var oldDataFieldRelationshipInfo in oldDataFieldRelationshipInfos)
                        {
                            if (dataFieldRelationshipInfo.DataFieldId == oldDataFieldRelationshipInfo.DataFieldId)
                            {
                                find = true;
                                if (dataFieldRelationshipInfo.Sorting != oldDataFieldRelationshipInfo.Sorting)
                                {
                                    Update(dataFieldRelationshipInfo, db, transaction);
                                }
                                break;
                            }
                        }
                        if (!find)
                        {
                            Insert(dataFieldRelationshipInfo, db, transaction);
                        }
                    }
                    /* 2. 存在则忽略，不存在则删除*/
                    foreach (var oldDataFieldRelationshipInfo in oldDataFieldRelationshipInfos)
                    {
                        bool find = false;
                        foreach (var dataFieldRelationshipInfo in dataFieldRelationshipInfos)
                        {
                            if (dataFieldRelationshipInfo.DataFieldId == oldDataFieldRelationshipInfo.DataFieldId)
                            {
                                find = true;
                                break;
                            }
                        }
                        if (!find)
                        {
                            Delete(oldDataFieldRelationshipInfo.ParentDataFieldId, oldDataFieldRelationshipInfo.DataFieldId, db, transaction);
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
        /// 获得字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRelationDataFields(decimal parentDataFieldId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            try
            {
                string sqlSelect = "SELECT CustomDataField.DataFieldId, CustomDataField.LogicalName, CustomDataField.PhysicalName FROM CustomDataField INNER JOIN DataFieldRelationship ON CustomDataField.DataFieldId = DataFieldRelationship.DataFieldId WHERE DataFieldRelationship.ParentDataFieldId = @ParentDataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(parentDataFieldId));

                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[1]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[2]);
                            commonNodes.Add(new CommonNode(dataFieldId, parentDataFieldId, logicalName, physicalName));
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
        /// 获得字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRelationDataFieldsWithFullName(decimal parentDataFieldId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT CustomDataField.DataFieldId, CustomDatabase.DatabaseName, CustomCategory.CategoryName, CustomTable.LogicalName, CustomDataField.LogicalName, CustomDataField.PhysicalName FROM CustomDataField ");
                sb.Append("INNER JOIN DataFieldRelationship ON CustomDataField.DataFieldId = DataFieldRelationship.DataFieldId ");
                sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = CustomDataField.TableId ");
                sb.Append("INNER JOIN CustomCategory ON CustomTable.CategoryId = CustomCategory.CategoryId ");
                sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId =  CustomCategory.DatabaseId ");
                sb.Append("WHERE DataFieldRelationship.ParentDataFieldId = @ParentDataFieldId");

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(parentDataFieldId));

                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string databaseName = DataConvertionHelper.GetString(dataReader[1]);
                            string categoryName = DataConvertionHelper.GetString(dataReader[2]);
                            string tableLogicalName = DataConvertionHelper.GetString(dataReader[3]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[4]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[5]);
                            logicalName = string.Format("[{0}][{1}][{2}][{3}]", databaseName, categoryName, tableLogicalName, logicalName);
                            commonNodes.Add(new CommonNode(dataFieldId, parentDataFieldId, logicalName, physicalName));
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

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 向 DataFieldRelationshipInfo 表中插入一条新记录
        /// </summary>
        /// <param name="dataFieldRelationshipInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Insert(DataFieldRelationshipInfo dataFieldRelationshipInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO DataFieldRelationship(ParentDataFieldId, DataFieldId, Sorting)");
            sb.Append("VALUES (@ParentDataFieldId, @DataFieldId, @Sorting);");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, dataFieldRelationshipInfo.ParentDataFieldId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldRelationshipInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, dataFieldRelationshipInfo.Sorting);
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
        /// 更新联系字段
        /// </summary>
        /// <param name="dataFieldRelationshipInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Update(DataFieldRelationshipInfo dataFieldRelationshipInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE DataFieldRelationship SET Sorting = @Sorting ");
            sb.Append("WHERE ParentDataFieldId = @ParentDataFieldId AND DataFieldId = @DataFieldId");
            //获得系统数据库对象
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, dataFieldRelationshipInfo.ParentDataFieldId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldRelationshipInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, dataFieldRelationshipInfo.Sorting);
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
        /// 获得 DataFieldRelationInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>DataFieldRelationInfo 对象列表</returns>
        private IList<DataFieldRelationshipInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<DataFieldRelationshipInfo> dataFieldRelationInfos = new List<DataFieldRelationshipInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM DataFieldRelationship");
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
                            decimal parentDataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[2]);
                            //将创建 DataFieldRelationInfo 对象加入集合中
                            dataFieldRelationInfos.Add(new DataFieldRelationshipInfo(parentDataFieldId, dataFieldId, sorting));
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

            return dataFieldRelationInfos;
        }

        /// <summary>
        /// 获得 DataFieldRelationInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataFieldRelationInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM DataFieldRelation");
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
        /// 获得表 DataFieldRelation 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "DataFieldRelation ", "ParentDataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 DataFieldRelation 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataFieldRelation ", "ParentDataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 DataFieldRelation 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "DataFieldRelation ", "ParentDataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 DataFieldRelation 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataFieldRelation ", "ParentDataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 DataFieldRelationInfo 对象
        /// </summary>
        /// <param name="parentDataFieldId">字段编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal parentDataFieldId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM DataFieldRelation WHERE ParentDataFieldId = @ParentDataFieldId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, parentDataFieldId);
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
        /// 删除满足条件的所有  DataFieldRelationInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataFieldRelation");
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
