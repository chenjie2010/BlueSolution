//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomViewAndDataField.cs
// 描述：CustomViewAndDataField 数据层访问类
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
    /// CustomViewAndDataField 表的数据层访问类
    /// </summary>
    public class CustomViewAndDataField : CorrelatedTableDataAcess, ICustomViewAndDataField
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomViewAndDataField() : base("CustomViewAndDataField", "DataFieldId", "ViewId")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 获得 CustomViewAndDataFieldInfo 对象
        /// </summary>
        ///<param name="viewId">视图编号</param>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> CustomViewAndDataFieldInfo 对象</returns>
        public CustomViewAndDataFieldInfo GetModelInfo(decimal viewId, decimal dataFieldId)
        {
            CustomViewAndDataFieldInfo customViewAndDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ViewId", "ViewId", System.Data.DbType.Decimal, viewId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("DataFieldId", "DataFieldId", System.Data.DbType.Decimal, dataFieldId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomViewAndDataFieldInfo> customViewAndDataFieldInfos = GetModelInfos(whereConditons, null, true);
            if (customViewAndDataFieldInfos != null && customViewAndDataFieldInfos.Count > 0)
            {
                customViewAndDataFieldInfo = customViewAndDataFieldInfos[0];
            }

            return customViewAndDataFieldInfo;
        }

        /// <summary>
        /// 获得 CustomViewAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomViewAndDataFieldInfo 对象列表</returns>
        public IList<CustomViewAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomViewAndDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomViewAndDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomViewAndDataField ", "ViewId", false, whereConditons);
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
        /// 获得视图与字段对象集合
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CustomViewAndDataFieldInfo> GetModelInfos(decimal viewId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("ViewId", "ViewId", System.Data.DbType.Decimal, viewId, DataFieldCondition.Equal));

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 根据视图的信息获得表的信息
        /// 节点的父编号为数据表编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDataFieldsByViewId(decimal viewId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomViewAndDataField.DataFieldId, TableId, LogicalName, PhysicalName FROM CustomViewAndDataField ");
            sb.Append("INNER JOIN CustomDataField ON CustomViewAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE ViewId = @ViewId ORDER BY CustomViewAndDataField.Sorting");

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
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[2]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[3]);
                            commonNodes.Add(new CommonNode(dataFieldId, tableId, logicalName, physicalName));
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
        /// 更新视图编号与字段编号的关系
        /// </summary>
        /// <param name="viewId"></param>
        public void UpdateDataFields(decimal viewId, IList<CustomViewAndDataFieldInfo> customViewAndDataFieldInfos)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("ViewId", "ViewId", System.Data.DbType.Decimal, viewId, DataFieldCondition.Equal));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));
            IList<CustomViewAndDataFieldInfo> oldCustomViewAndDataFieldInfos = GetModelInfos(whereConditons, sortingCondtions);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    bool viewCreated = false;
                    /* 1. 不存在则插入，存在则更新 */
                    foreach (var customViewAndDataFieldInfo in customViewAndDataFieldInfos)
                    {
                        bool find = false;
                        foreach (var oldCustomViewAndDataFieldInfo in oldCustomViewAndDataFieldInfos)
                        {
                            if (customViewAndDataFieldInfo.ViewId == oldCustomViewAndDataFieldInfo.ViewId
                            && customViewAndDataFieldInfo.DataFieldId == oldCustomViewAndDataFieldInfo.DataFieldId)
                            {
                                find = true;
                                if (customViewAndDataFieldInfo.Sorting != oldCustomViewAndDataFieldInfo.Sorting)
                                {
                                    viewCreated = true;
                                    Update(customViewAndDataFieldInfo, db, transaction);
                                }
                                break;
                            }
                        }
                        if (!find)
                        {
                            viewCreated = true;
                            Insert(customViewAndDataFieldInfo, db, transaction);
                        }
                    }
                    /* 2. 存在则忽略，不存在则删除*/
                    foreach (var oldCustomViewAndDataFieldInfo in oldCustomViewAndDataFieldInfos)
                    {
                        bool find = false;
                        foreach (var customViewAndDataFieldInfo in customViewAndDataFieldInfos)
                        {
                            if (customViewAndDataFieldInfo.ViewId == oldCustomViewAndDataFieldInfo.ViewId
                            && customViewAndDataFieldInfo.DataFieldId == oldCustomViewAndDataFieldInfo.DataFieldId)
                            {
                                find = true;
                                break;
                            }
                        }
                        if (!find)
                        {
                            viewCreated = true;
                            Delete(oldCustomViewAndDataFieldInfo.DataFieldId, oldCustomViewAndDataFieldInfo.ViewId, db, transaction);
                        }
                    }
                    if (viewCreated)
                    {
                        CustomView customView = new CustomView();
                        CustomViewInfo customViewInfo = customView.GetModelInfo(viewId);
                        CustomViewAndTable customViewAndTable = new CustomViewAndTable();
                        IList<CustomViewAndTableInfo> customViewAndTableInfos = customViewAndTable.GetModelInfos(viewId);
                        customView.CreatePhysicalView(customViewInfo, customViewAndTableInfos, customViewAndDataFieldInfos);
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
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void DeleteByTableId(decimal tableId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE CustomViewAndDataField FROM CustomViewAndDataField INNER JOIN CustomDataField ");
            sb.Append("ON CustomViewAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE CustomDataField.TableId = @TableId ");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
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
        /// 获得 CustomViewAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomViewAndDataFieldInfo 对象列表</returns>
        private IList<CustomViewAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomViewAndDataFieldInfo> customViewAndDataFieldInfos = new List<CustomViewAndDataFieldInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomViewAndDataField");
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
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal viewId = DataConvertionHelper.GetDecimal(dataReader[1]);                            
                            int sorting = DataConvertionHelper.GetInt(dataReader[2]);
                            //将创建 CustomViewAndDataFieldInfo 对象加入集合中
                            customViewAndDataFieldInfos.Add(new CustomViewAndDataFieldInfo(viewId, dataFieldId, sorting));
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

            return customViewAndDataFieldInfos;
        }

        /// <summary>
        /// 获得 CustomViewAndDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomViewAndDataFieldInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomViewAndDataField");
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
        /// 获得表 CustomViewAndDataField 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomViewAndDataField ", "ViewId", "*", false, false, startPosition,
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
        /// 获得以表 CustomViewAndDataField 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomViewAndDataField ", "ViewId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomViewAndDataField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomViewAndDataField ", "ViewId", "*", false, false, startPosition,
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
        /// 获得以表 CustomViewAndDataField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomViewAndDataField ", "ViewId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 CustomViewAndDataFieldInfo 对象
        /// </summary>
        /// <param name="viewId">视图编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal viewId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM CustomViewAndDataField WHERE ViewId = @ViewId";
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
        /// 删除满足条件的所有  CustomViewAndDataFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomViewAndDataField");
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
        /// <param name="customViewAndDataFieldInfo">customViewAndDataFieldInfo 对象</param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Insert(CustomViewAndDataFieldInfo customViewAndDataFieldInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomViewAndDataField(DataFieldId, ViewId, Sorting)");
            sb.Append("VALUES (@DataFieldId, @ViewId, @Sorting);");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, customViewAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, customViewAndDataFieldInfo.ViewId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customViewAndDataFieldInfo.Sorting);
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
        /// <param name="customViewAndDataFieldInfo">CustomViewAndDataFieldInfo 对象</param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(CustomViewAndDataFieldInfo customViewAndDataFieldInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomViewAndDataField SET Sorting = @Sorting ");
            sb.Append("WHERE ViewId = @ViewId AND DataFieldId = @DataFieldId");
            //获得系统数据库对象
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, customViewAndDataFieldInfo.ViewId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, customViewAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customViewAndDataFieldInfo.Sorting);
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
