//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RemoteDataAndField.cs
// 描述: RemoteDataAndField 数据层访问类
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
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Core;
using Blue.IDAL.DataConvertionModule;
using Blue.Model.DataConvertionModule;

namespace Blue.SQLServerDAL.DataConvertionModule
{
    /// <summary>
    /// RemoteDataAndField 表的数据层访问类
    /// </summary>
    public class RemoteDataAndField : CorrelatedTableDataAcess, IRemoteDataAndField
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public RemoteDataAndField() : base("RemoteDataAndField", "RemoteDataId", "DataFieldId")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 RemoteDataAndField 表中插入一条新记录
        /// </summary>
        /// <param name="remoteDataAndFieldInfo">remoteDataAndFieldInfo 对象</param>
        public void Insert(RemoteDataAndFieldInfo remoteDataAndFieldInfo)
        {
            
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                Insert(remoteDataAndFieldInfo, db, null);                
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
		/// 获得 RemoteDataAndFieldInfo 对象
		/// </summary>
		///<param name="dataFieldId">字段编号</param>
		///<param name="remoteDataId">远程数据交换编号</param>
		/// <returns> RemoteDataAndFieldInfo 对象</returns>
		public RemoteDataAndFieldInfo GetModelInfo(decimal dataFieldId, decimal remoteDataId)
        {
            RemoteDataAndFieldInfo remoteDataAndFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("DataFieldId", "DataFieldId", DbType.Decimal, dataFieldId, DataFieldCondition.Equal));

            //创建集合对象
            IList<RemoteDataAndFieldInfo> remoteDataAndFieldInfos = GetModelInfos(whereConditons, null, true);
            if (remoteDataAndFieldInfos != null && remoteDataAndFieldInfos.Count > 0)
            {
                remoteDataAndFieldInfo = remoteDataAndFieldInfos[0];
            }

            return remoteDataAndFieldInfo;
        }

        /// <summary>
        /// 更新 RemoteDataAndFieldInfo 对象
        /// </summary>
        /// <param name="remoteDataAndFieldInfo">RemoteDataAndFieldInfo 对象</param>
        public void Update(RemoteDataAndFieldInfo remoteDataAndFieldInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE RemoteDataAndField SET RemoteDataFieldId = @RemoteDataFieldId ");
            sb.Append("WHERE DataFieldId = @DataFieldId AND RemoteDataId = @RemoteDataId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, remoteDataAndFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "RemoteDataId", DbType.Decimal, remoteDataAndFieldInfo.RemoteDataId);
                    db.AddInParameter(dbCommand, "RemoteDataFieldId", DbType.Decimal, remoteDataAndFieldInfo.RemoteDataFieldId);
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
        ///  删除 RemoteDataAndFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="remoteDataId">远程数据交换编号</param>
        public void Delete(decimal dataFieldId, decimal remoteDataId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RemoteDataAndField ");
            sb.Append("WHERE DataFieldId = @DataFieldId AND RemoteDataId = @RemoteDataId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
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
        /// 获得 RemoteDataAndFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RemoteDataAndFieldInfo 对象列表</returns>
        public IList<RemoteDataAndFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 RemoteDataAndField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>RemoteDataAndFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "RemoteDataAndField ", "DataFieldId", false, whereConditons);
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
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetDataFieldRelation(decimal remoteDataId, decimal destinationTableId)
        {
            Dictionary<decimal, decimal> dataFieldRelation = new Dictionary<decimal, decimal>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT RemoteDataAndField.RemoteDataFieldId, RemoteDataAndField.DataFieldId FROM RemoteDataAndField ");
            sb.Append("INNER JOIN CustomDataField ON CustomDataField.DataFieldId = RemoteDataAndField.DataFieldId ");
            sb.Append("WHERE RemoteDataAndField.RemoteDataId = @RemoteDataId AND CustomDataField.TableId = @TableId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RemoteDataId", DbType.Decimal, remoteDataId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, destinationTableId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentDataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            dataFieldRelation.Add(dataFieldId, parentDataFieldId);
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

            return dataFieldRelation;
        }


        /// <summary>
        /// 获得 RemoteDataAndFieldInfo 对象的列表
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <returns></returns>
        public IList<RemoteDataAndFieldInfo> GetModelInfos(decimal remoteDataId)
        {
            //创建集合对象
            IList<RemoteDataAndFieldInfo> remoteDataAndFieldInfos = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("RemoteDataId", "RemoteDataId", DbType.Decimal, remoteDataId, DataFieldCondition.Equal));
            remoteDataAndFieldInfos = GetModelInfos(whereConditons, null, false);

            return remoteDataAndFieldInfos;
        }

        /// <summary>
        /// 获本地的表与字段对应关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <returns></returns>
        public Dictionary<decimal, Dictionary<decimal, decimal>> GetTableRelation(decimal remoteDataId)
        {
            Dictionary<decimal, Dictionary<decimal, decimal>> tableRelation = new Dictionary<decimal, Dictionary<decimal, decimal>>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDataField.TableId, CustomDataField.DataFieldId, RemoteDataAndField.RemoteDataFieldId FROM RemoteDataAndField ");
            sb.Append("INNER JOIN CustomDataField ON RemoteDataAndField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE RemoteDataId = @RemoteDataId");
            
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RemoteDataId", DbType.Decimal, remoteDataId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal remoteDataFieldId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            Dictionary<decimal, decimal> dataFields = null;
                            if (!tableRelation.ContainsKey(tableId))
                            {
                                dataFields = new Dictionary<decimal, decimal>();
                                tableRelation.Add(tableId, dataFields);
                            }
                            else
                            {
                                dataFields = tableRelation[tableId];
                            }
                            if (!dataFields.ContainsKey(dataFieldId))
                            {
                                dataFields.Add(dataFieldId, remoteDataFieldId);
                            }
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

            return tableRelation;
        }

        /// <summary>
        /// 更新字段关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <param name="keyValueItems"></param>
        public void UpdateDataFieldRelation(decimal remoteDataId, List<KeyValueItem> keyValueItems)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    IList<RemoteDataAndFieldInfo> remoteDataAndFieldInfos = GetModelInfos(remoteDataId);
                    /* 1. 增加的部分 */
                    foreach (KeyValueItem keyValueItem in keyValueItems)
                    {
                        bool find = false;
                        foreach (RemoteDataAndFieldInfo remoteDataAndFieldInfo in remoteDataAndFieldInfos)
                        {
                            if (remoteDataAndFieldInfo.RemoteDataFieldId == keyValueItem.Key && remoteDataAndFieldInfo.DataFieldId == keyValueItem.Value)
                            {
                                find = true;
                            }

                        }
                        if (!find)
                        {
                            Insert(new RemoteDataAndFieldInfo(keyValueItem.Value, remoteDataId, keyValueItem.Key), db, transaction);
                        }
                    }
                    /* 2. 删除的部分 */
                    foreach (RemoteDataAndFieldInfo remoteDataAndFieldInfo in remoteDataAndFieldInfos)
                    {
                        bool find = false;
                        foreach (KeyValueItem keyValueItem in keyValueItems)
                        {
                            if (remoteDataAndFieldInfo.RemoteDataFieldId == keyValueItem.Key && remoteDataAndFieldInfo.DataFieldId == keyValueItem.Value)
                            {
                                find = true;
                            }
                        }
                        if (!find)
                        {
                            Delete(remoteDataAndFieldInfo.DataFieldId, remoteDataId, remoteDataAndFieldInfo.RemoteDataFieldId, db, transaction);
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

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 RemoteDataAndFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>RemoteDataAndFieldInfo 对象列表</returns>
        private IList<RemoteDataAndFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<RemoteDataAndFieldInfo> remoteDataAndFieldInfos = new List<RemoteDataAndFieldInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM RemoteDataAndField");

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
                            decimal remoteDataId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal remoteDataFieldId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            //将创建 RemoteDataAndFieldInfo 对象加入集合中
                            remoteDataAndFieldInfos.Add(new RemoteDataAndFieldInfo(dataFieldId, remoteDataId, remoteDataFieldId));
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

            return remoteDataAndFieldInfos;
        }


        /// <summary>
        /// 获得 RemoteDataAndFieldInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RemoteDataAndFieldInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM RemoteDataAndField");
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
        /// 获得表 RemoteDataAndField 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "RemoteDataAndField ", "DataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 RemoteDataAndField 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RemoteDataAndField ", "DataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 RemoteDataAndField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "RemoteDataAndField ", "DataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 RemoteDataAndField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "RemoteDataAndField ", "DataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 RemoteDataAndFieldInfo 对象
        /// </summary>
        /// <param name="dataFieldId">字段编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal dataFieldId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM RemoteDataAndField WHERE DataFieldId = @DataFieldId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
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
        /// 删除满足条件的所有  RemoteDataAndFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RemoteDataAndField");
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
        ///  删除 RemoteDataAndFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="remoteDataId">远程数据交换编号</param>
        ///<param name="remoteDataFieldId">远程字段编号</param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(decimal dataFieldId, decimal remoteDataId, decimal remoteDataFieldId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM RemoteDataAndField ");
            sb.Append("WHERE DataFieldId = @DataFieldId AND RemoteDataId = @RemoteDataId AND RemoteDataFieldId = @RemoteDataFieldId");
 
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
                    db.AddInParameter(dbCommand, "RemoteDataId", DbType.Decimal, remoteDataId);
                    db.AddInParameter(dbCommand, "RemoteDataFieldId", DbType.Decimal, remoteDataFieldId);
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
        /// 向 RemoteDataAndField 表中插入一条新记录
        /// </summary>
        /// <param name="remoteDataAndFieldInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private decimal Insert(RemoteDataAndFieldInfo remoteDataAndFieldInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal remoteDataAndFieldId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO RemoteDataAndField(DataFieldId, RemoteDataId, RemoteDataFieldId)");
            sb.Append("VALUES (@DataFieldId, @RemoteDataId, @RemoteDataFieldId)");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, remoteDataAndFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "RemoteDataId", DbType.Decimal, remoteDataAndFieldInfo.RemoteDataId);
                    db.AddInParameter(dbCommand, "RemoteDataFieldId", DbType.Decimal, remoteDataAndFieldInfo.RemoteDataFieldId);
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
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return remoteDataAndFieldId;
        }

        #endregion

        #endregion
    }
}
