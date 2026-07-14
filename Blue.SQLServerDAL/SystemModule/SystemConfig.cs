//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：SystemConfig.cs
// 描述：SystemConfig 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/8/19
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
using AppFramework.Core;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// SystemConfig 表的数据层访问类
    /// </summary>
    public class SystemConfig : ISystemConfig
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public SystemConfig()
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 SystemConfig 表中插入一条新记录
        /// </summary>
        /// <param name="systemConfigInfo">systemConfigInfo 对象</param>
        public void Insert(SystemConfigInfo systemConfigInfo)
        {
            //自动增加的关键字的值
            decimal systemConfigId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO SystemConfig(SystemConfigName, SystemConfigValue, SystemConfigCategory, UpdatedTime)");
            sb.Append("VALUES (@SystemConfigName, @SystemConfigValue, @SystemConfigCategory, @UpdatedTime);");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SystemConfigName", DbType.Int32, systemConfigInfo.SystemConfigName);
                    db.AddInParameter(dbCommand, "SystemConfigValue", DbType.String, systemConfigInfo.SystemConfigValue);
                    db.AddInParameter(dbCommand, "SystemConfigCategory", DbType.Byte, Convert.ToByte(systemConfigInfo.SystemConfigCategory));
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, systemConfigInfo.UpdatedTime);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                    systemConfigId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@SystemConfigName"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
        }

        /// <summary>
		/// 获得 SystemConfigInfo 对象
		/// </summary>
		///<param name="systemConfigKeyName">系统配置名称</param>
		/// <returns> SystemConfigInfo 对象</returns>
		public SystemConfigInfo GetModelInfo(SystemConfigKeyName systemConfigKeyName)
        {
            SystemConfigInfo systemConfigInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("SystemConfigName", "SystemConfigName", System.Data.DbType.Int32, Convert.ToInt32(systemConfigKeyName), DataFieldCondition.Equal));

            //创建集合对象
            IList<SystemConfigInfo> systemConfigInfos = GetModelInfos(whereConditons, null, true);
            if (systemConfigInfos != null && systemConfigInfos.Count > 0)
            {
                systemConfigInfo = systemConfigInfos[0];
            }

            return systemConfigInfo;
        }

        /// <summary>
        /// 更新 SystemConfigInfo 对象
        /// </summary>
        /// <param name="systemConfigInfo">SystemConfigInfo 对象</param>
        public void Update(SystemConfigInfo systemConfigInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE SystemConfig SET SystemConfigValue = @SystemConfigValue, SystemConfigCategory = @SystemConfigCategory, UpdatedTime = @UpdatedTime ");
            sb.Append("WHERE SystemConfigName = @SystemConfigName");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SystemConfigName", DbType.Int32, systemConfigInfo.SystemConfigName);
                    db.AddInParameter(dbCommand, "SystemConfigValue", DbType.String, systemConfigInfo.SystemConfigValue);
                    db.AddInParameter(dbCommand, "SystemConfigCategory", DbType.Byte, Convert.ToByte(systemConfigInfo.SystemConfigCategory));
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, systemConfigInfo.UpdatedTime);
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
        ///  删除 SystemConfigInfo 对象
        /// </summary>
        ///<param name="systemConfigName">系统配置名称</param>
        public void Delete(int systemConfigName)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM SystemConfig ");
            sb.Append("WHERE SystemConfigName = @SystemConfigName");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "SystemConfigName", DbType.Int32, systemConfigName);
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
        /// 获得 SystemConfigInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>SystemConfigInfo 对象列表</returns>
        public IList<SystemConfigInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 SystemConfig 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>SystemConfigInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "SystemConfig ", "SystemConfigName", false, whereConditons);
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

        /// <summary>
        /// 更新系统设置
        /// </summary>
        /// <param name="systemConfigInfos"></param>
        public void UpdateSystemConfigInfos(Dictionary<int, SystemConfigInfo> systemConfigInfos)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("IF EXISTS(SELECT SystemConfigName FROM SystemConfig WHERE SystemConfigName = @SystemConfigName) ");
            sb.Append("BEGIN UPDATE SystemConfig SET SystemConfigValue = @SystemConfigValue, SystemConfigCategory = @SystemConfigCategory, UpdatedTime = @UpdatedTime WHERE SystemConfigName = @SystemConfigName ");
            sb.Append("END ELSE ");
            sb.Append("BEGIN INSERT INTO SystemConfig(SystemConfigName, SystemConfigValue, SystemConfigCategory, UpdatedTime) ");
            sb.Append("VALUES (@SystemConfigName, @SystemConfigValue, @SystemConfigCategory, @UpdatedTime) END");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (KeyValuePair<int, SystemConfigInfo> keyValue in systemConfigInfos)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            //给参数赋值
                            db.AddInParameter(dbCommand, "SystemConfigName", DbType.Int32, keyValue.Value.SystemConfigName);
                            db.AddInParameter(dbCommand, "SystemConfigValue", DbType.String, keyValue.Value.SystemConfigValue);
                            db.AddInParameter(dbCommand, "SystemConfigCategory", DbType.Byte, Convert.ToByte(keyValue.Value.SystemConfigCategory));
                            db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DateTime.Now);
                            //执行插入操作
                            if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                            {
                                throw new Exception("更新失败！");
                            }
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
        /// 获得系统配置
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, SystemConfigInfo> GetSystemConfigInfos()
        {
            Dictionary<int, SystemConfigInfo> systemConfigInfos = new Dictionary<int, SystemConfigInfo>();

            //查询语句
            string sqlSelect = "SELECT * FROM SystemConfig";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            int systemConfigName = DataConvertionHelper.GetInt(dataReader[0]);
                            string systemConfigValue = DataConvertionHelper.GetString(dataReader[1]);
                            SystemConfigCategory systemConfigCategory = (SystemConfigCategory)DataConvertionHelper.GetByte(dataReader[2]);
                            DateTime updatedTime = DataConvertionHelper.GetDateTime(dataReader[3]);
                            //将创建 SystemConfigInfo 对象加入集合中
                            systemConfigInfos.Add(systemConfigName, new SystemConfigInfo(systemConfigName, systemConfigValue, systemConfigCategory, updatedTime));
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

            return systemConfigInfos;
        }

        /// <summary>
        /// 通过系统关键字查询对应的值
        /// </summary>
        /// <param name="systemConfigKeyName"></param>
        /// <returns></returns>
        public string GetSystemConfigValue(SystemConfigKeyName systemConfigKeyName)
        {
            string systemConfigValue = string.Empty;

            string sqlSelect = "SELECT SystemConfigValue FROM SystemConfig WHERE SystemConfigName = @SystemConfigName";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "SystemConfigName", DbType.Int32, Convert.ToInt32(systemConfigKeyName));
                    systemConfigValue = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return systemConfigValue;

        }

        #region 实现新增接口

            #endregion

            #endregion

        #region 公有方法

            #endregion

        #region 私有方法

            #region 默认私有方法

            /// <summary>
            /// 获得 SystemConfigInfo 对象的列表
            /// </summary>	
            /// <param name="whereConditons">查询字段条件的集合</param>
            /// <param name="sortingCondtions">排序字段条件的集合</param>
            /// <param name="onlyOne">第一条记录</param>
            /// <returns>SystemConfigInfo 对象列表</returns>
        public IList<SystemConfigInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<SystemConfigInfo> systemConfigInfos = new List<SystemConfigInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM SystemConfig");
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
                            int systemConfigName = DataConvertionHelper.GetInt(dataReader[0]);
                            string systemConfigValue = DataConvertionHelper.GetString(dataReader[1]);
                            SystemConfigCategory systemConfigCategory = (SystemConfigCategory)DataConvertionHelper.GetByte(dataReader[2]);
                            DateTime updatedTime = DataConvertionHelper.GetDateTime(dataReader[3]);
                            //将创建 SystemConfigInfo 对象加入集合中
                            systemConfigInfos.Add(new SystemConfigInfo(systemConfigName, systemConfigValue, systemConfigCategory, updatedTime));
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

            return systemConfigInfos;
        }

        /// <summary>
        /// 获得 SystemConfigInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>SystemConfigInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM SystemConfig");
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
        /// 获得表 SystemConfig 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "SystemConfig ", "SystemConfigName", "*", false, false, startPosition,
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
        /// 获得以表 SystemConfig 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "SystemConfig ", "SystemConfigName", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 SystemConfig 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "SystemConfig ", "SystemConfigName", "*", false, false, startPosition,
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
        /// 获得以表 SystemConfig 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "SystemConfig ", "SystemConfigName", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  SystemConfigInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM SystemConfig");
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
