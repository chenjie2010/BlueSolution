//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomViewAndTable.cs
// 描述：CustomViewAndTable 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/10/13
// Copyright 2017
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
    /// CustomViewAndTable 表的数据层访问类
    /// </summary>
    public class CustomViewAndTable : CorrelatedTableDataAcess, ICustomViewAndTable
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomViewAndTable() : base("CustomViewAndTable", "ViewId", "TableId", "TableRelation")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomViewAndTable 表中插入一条新记录
        /// </summary>
        /// <param name="customViewAndTableInfo">customViewAndTableInfo 对象</param>		
        public void Insert(CustomViewAndTableInfo customViewAndTableInfo)
        {

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomViewAndTable(ViewId, TableId,  TableRelation, TableJoin, PrimaryDataField, Sorting)");
            sb.Append("VALUES (@TableRelation, @TableJoin, @PrimaryDataField, @Sorting);");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, customViewAndTableInfo.ViewId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, customViewAndTableInfo.TableId);
                    db.AddInParameter(dbCommand, "TableRelation", DbType.Byte, customViewAndTableInfo.TableRelation);
                    db.AddInParameter(dbCommand, "TableJoin", DbType.Byte, customViewAndTableInfo.TableJoin);
                    db.AddInParameter(dbCommand, "PrimaryDataField", DbType.Byte, customViewAndTableInfo.PrimaryDataField);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customViewAndTableInfo.Sorting);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
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
		/// 获得 CustomViewAndTableInfo 对象
		/// </summary>
		///<param name="viewId">视图编号</param>
		///<param name="tableId">表编号</param>
		/// <returns> CustomViewAndTableInfo 对象</returns>
		public CustomViewAndTableInfo GetModelInfo(decimal viewId, decimal tableId)
        {
            CustomViewAndTableInfo customViewAndTableInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ViewId", "ViewId", System.Data.DbType.Decimal, viewId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("TableId", "TableId", System.Data.DbType.Decimal, tableId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomViewAndTableInfo> customViewAndTableInfos = GetModelInfos(whereConditons, null, true);
            if (customViewAndTableInfos != null && customViewAndTableInfos.Count > 0)
            {
                customViewAndTableInfo = customViewAndTableInfos[0];
            }

            return customViewAndTableInfo;
        }

        /// <summary>
        /// 更新 CustomViewAndTableInfo 对象
        /// </summary>
        /// <param name="customViewAndTableInfo">CustomViewAndTableInfo 对象</param>
        public void Update(CustomViewAndTableInfo customViewAndTableInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomViewAndTable SET TableRelation = @TableRelation, TableJoin = @TableJoin, PrimaryDataField = @PrimaryDataField, Sorting = @Sorting ");
            sb.Append("WHERE ViewId = @ViewId AND TableId = @TableId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, customViewAndTableInfo.ViewId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, customViewAndTableInfo.TableId);
                    db.AddInParameter(dbCommand, "TableRelation", DbType.Byte, customViewAndTableInfo.TableRelation);
                    db.AddInParameter(dbCommand, "TableJoin", DbType.Byte, customViewAndTableInfo.TableJoin);
                    db.AddInParameter(dbCommand, "PrimaryDataField", DbType.Byte, customViewAndTableInfo.PrimaryDataField);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customViewAndTableInfo.Sorting);
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
        ///  删除 CustomViewAndTableInfo 对象
        /// </summary>
        ///<param name="viewId">视图编号</param>
        ///<param name="tableId">表编号</param>
        public void Delete(decimal viewId, decimal tableId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomViewAndTable ");
            sb.Append("WHERE ViewId = @ViewId AND TableId = @TableId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, viewId);
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
        /// 获得 CustomViewAndTableInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomViewAndTableInfo 对象列表</returns>
        public IList<CustomViewAndTableInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomViewAndTable 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomViewAndTableInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomViewAndTable ", "ViewId", false, whereConditons);
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
        /// 根据视图的信息获得表的信息
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetTablesByViewId(decimal viewId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomTable.TableId, LogicalName, PhysicalName, TableRelation FROM CustomViewAndTable ");
            sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = CustomViewAndTable.TableId ");
            sb.Append("WHERE ViewId = @ViewId ORDER BY CustomViewAndTable.Sorting");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, viewId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[1]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[2]);
                            byte tableRelation = DataConvertionHelper.GetByte(dataReader[3]);
                            commonNodes.Add(new CommonNode(tableId, viewId, logicalName, physicalName, true, tableRelation));
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
        /// 获得视图与表对象集合
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CustomViewAndTableInfo> GetModelInfos(decimal viewId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("ViewId", "ViewId", System.Data.DbType.Decimal, viewId, DataFieldCondition.Equal));

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 插入视图与表的关系
        /// </summary>
        /// <param name="customViewAndTableInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Insert(IList<CustomViewAndTableInfo> customViewAndTableInfos, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomViewAndTable(ViewId, TableId, TableRelation, TableJoin, PrimaryDataField, Sorting)");
            sb.Append("VALUES (@ViewId, @TableId, @TableRelation, @TableJoin, @PrimaryDataField, @Sorting);");

            try
            {
                int sorting = 1;
                foreach (var obj in customViewAndTableInfos)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, obj.ViewId);
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, obj.TableId);
                        db.AddInParameter(dbCommand, "TableRelation", DbType.Byte, obj.TableRelation);
                        db.AddInParameter(dbCommand, "TableJoin", DbType.Byte, obj.TableJoin);
                        db.AddInParameter(dbCommand, "PrimaryDataField", DbType.Byte, obj.PrimaryDataField);
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
        /// 向 CustomViewAndTable 表中插入一条新记录
        /// </summary>
        /// <param name="customViewAndTableInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
		public void Insert(CustomViewAndTableInfo customViewAndTableInfo, SqlDatabase db, DbTransaction transaction)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomViewAndTable(ViewId, TableId, TableRelation, TableJoin, PrimaryDataField, Sorting)");
            sb.Append("VALUES (@ViewId, @TableId, @TableRelation, @TableJoin, @PrimaryDataField, @Sorting);");

            if (customViewAndTableInfo.Sorting <= 0)
            {
                customViewAndTableInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomViewAndTable", "Sorting", "ViewId", customViewAndTableInfo.ViewId, 0) + 1;
            }
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, customViewAndTableInfo.ViewId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, customViewAndTableInfo.TableId);
                    db.AddInParameter(dbCommand, "TableRelation", DbType.Byte, customViewAndTableInfo.TableRelation);
                    db.AddInParameter(dbCommand, "TableJoin", DbType.Byte, customViewAndTableInfo.TableJoin);
                    db.AddInParameter(dbCommand, "PrimaryDataField", DbType.Byte, customViewAndTableInfo.PrimaryDataField);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customViewAndTableInfo.Sorting);
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
        /// 更新视图与表的关系
        /// </summary>
        /// <param name="customViewAndTableInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(CustomViewAndTableInfo customViewAndTableInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomViewAndTable SET TableRelation = @TableRelation, TableJoin = @TableJoin, ");
            sb.Append("PrimaryDataField = @PrimaryDataField, Sorting = @Sorting ");
            sb.Append("WHERE ViewId = @ViewId AND TableId = @TableId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, customViewAndTableInfo.ViewId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, customViewAndTableInfo.TableId);
                    db.AddInParameter(dbCommand, "TableRelation", DbType.Byte, customViewAndTableInfo.TableRelation);
                    db.AddInParameter(dbCommand, "TableJoin", DbType.Byte, customViewAndTableInfo.TableJoin);
                    db.AddInParameter(dbCommand, "PrimaryDataField", DbType.Byte, customViewAndTableInfo.PrimaryDataField);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customViewAndTableInfo.Sorting);
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
        /// 删除视图与表的关系
        /// </summary>
        /// <param name="customViewAndTableInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(CustomViewAndTableInfo customViewAndTableInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomViewAndTable ");
            sb.Append("WHERE ViewId = @ViewId AND TableId = @TableId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, customViewAndTableInfo.ViewId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, customViewAndTableInfo.TableId);
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

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomViewAndTableInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomViewAndTableInfo 对象列表</returns>
        private IList<CustomViewAndTableInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomViewAndTableInfo> customViewAndTableInfos = new List<CustomViewAndTableInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomViewAndTable");
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
                            decimal viewId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            byte tableRelation = DataConvertionHelper.GetByte(dataReader[2]);
                            byte tableJoin = DataConvertionHelper.GetByte(dataReader[3]);
                            byte primaryDataField = DataConvertionHelper.GetByte(dataReader[4]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[5]);
                            //将创建 CustomViewAndTableInfo 对象加入集合中
                            customViewAndTableInfos.Add(new CustomViewAndTableInfo(viewId, tableId, tableRelation, tableJoin, primaryDataField, sorting));
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

            return customViewAndTableInfos;
        }

        /// <summary>
        /// 获得 CustomViewAndTableInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomViewAndTableInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomViewAndTable");
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
        /// 获得表 CustomViewAndTable 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomViewAndTable ", "ViewId", "*", false, false, startPosition,
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
        /// 获得以表 CustomViewAndTable 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomViewAndTable ", "ViewId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomViewAndTable 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomViewAndTable ", "ViewId", "*", false, false, startPosition,
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
        /// 获得以表 CustomViewAndTable 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomViewAndTable ", "ViewId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 CustomViewAndTableInfo 对象
        /// </summary>
        /// <param name="viewId">视图编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal viewId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM CustomViewAndTable WHERE ViewId = @ViewId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, viewId);
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
        /// 删除满足条件的所有  CustomViewAndTableInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomViewAndTable");
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
