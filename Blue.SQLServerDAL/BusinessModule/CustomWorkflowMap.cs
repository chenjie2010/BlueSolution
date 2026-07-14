//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowMap.cs
// 描述：CustomWorkflowMap 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/4/23
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
    /// CustomWorkflowMap 表的数据层访问类
    /// </summary>
    public class CustomWorkflowMap : CorrelatedTableDataAcess, ICustomWorkflowMap
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowMap() : base("CustomWorkflowMap", "ParentProcessId", "ProcessId", "Sorting")
        {

        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomWorkflowMap 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowMapInfo">customWorkflowMapInfo 对象</param>
        public void Insert(CustomWorkflowMapInfo customWorkflowMapInfo)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomWorkflowMap(ParentProcessId, ProcessId, Sorting)");
            sb.Append("VALUES (@ParentProcessId, @ProcessId, @Sorting);");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customWorkflowMapInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomWorkflowMap", "Sorting", "ParentProcessId", customWorkflowMapInfo.ParentProcessId, 0) + 1;
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, customWorkflowMapInfo.ParentProcessId);
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, customWorkflowMapInfo.ProcessId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customWorkflowMapInfo.Sorting);
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
		/// 获得 CustomWorkflowMapInfo 对象
		/// </summary>
		///<param name="parentProcessId">流程编号</param>
		///<param name="processId">流程编号</param>
		/// <returns> CustomWorkflowMapInfo 对象</returns>
		public CustomWorkflowMapInfo GetModelInfo(decimal parentProcessId, decimal processId)
        {
            CustomWorkflowMapInfo customWorkflowMapInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ParentProcessId", "ParentProcessId", System.Data.DbType.Decimal, parentProcessId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("ProcessId", "ProcessId", System.Data.DbType.Decimal, processId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            //创建集合对象
            IList<CustomWorkflowMapInfo> customWorkflowMapInfos = GetModeInfos(whereConditons, null, true);
            if (customWorkflowMapInfos != null && customWorkflowMapInfos.Count > 0)
            {
                customWorkflowMapInfo = customWorkflowMapInfos[0];
            }

            return customWorkflowMapInfo;
        }

        /// <summary>
        /// 更新 CustomWorkflowMapInfo 对象
        /// </summary>
        /// <param name="parentProcessId"></param>
        /// <param name="processId"></param>
        /// <param name="customWorkflowMapInfo"></param>
        /// <param name="customWorkflowMapInfo">CustomWorkflowMapInfo 对象</param>
        public void Update(decimal parentProcessId, decimal processId, CustomWorkflowMapInfo customWorkflowMapInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomWorkflowMap SET ParentProcessId = @ParentProcessId, ProcessId = @ProcessId, ");
            sb.Append("WHERE ParentProcessId = @ParentProcessId_0 AND ProcessId = @ProcessId_0");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, customWorkflowMapInfo.ParentProcessId);
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, customWorkflowMapInfo.ProcessId);
                    db.AddInParameter(dbCommand, "ParentProcessId_0", DbType.Decimal, parentProcessId);
                    db.AddInParameter(dbCommand, "ProcessId_0", DbType.Decimal, processId);
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
        ///  删除 CustomWorkflowMapInfo 对象
        /// </summary>
        ///<param name="parentProcessId">流程编号</param>
        ///<param name="processId">流程编号</param>
        public void Delete(decimal parentProcessId, decimal processId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomWorkflowMap ");
            sb.Append("WHERE ParentProcessId = @ParentProcessId AND ProcessId = @ProcessId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
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
        /// 获得 CustomWorkflowMapInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowMapInfo 对象列表</returns>
        public IList<CustomWorkflowMapInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModeInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomWorkflowMap 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomWorkflowMapInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomWorkflowMap ", "ParentProcessId", false, whereConditons);
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
        /// 获得 CustomWorkflowMap 表中记录的数目
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public int GetTotalCountByProcessId(decimal processId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            whereConditons.Add(new WhereConditon("ProcessId", "ProcessId", DbType.Decimal, processId, DataFieldCondition.Equal));

            return GetTotalCount(whereConditons);
        }

        /// <summary>
        /// 向 CustomWorkflowMap 表中插入多条记录
        /// </summary>
        /// <param name="customWorkflowMapInfos">customWorkflowMapInfos 对象列表</param>
        public void Insert(IList<CustomWorkflowMapInfo> customWorkflowMapInfos)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomWorkflowMap(ParentProcessId, ProcessId, Sorting)");
            sb.Append("VALUES (@ParentProcessId, @ProcessId, @Sorting);");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            foreach (CustomWorkflowMapInfo customWorkflowMapInfo in customWorkflowMapInfos)
            {
                customWorkflowMapInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomWorkflowMap", "Sorting", "ParentProcessId", customWorkflowMapInfo.ParentProcessId, 0) + 1;
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, customWorkflowMapInfo.ParentProcessId);
                        db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, customWorkflowMapInfo.ProcessId);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customWorkflowMapInfo.Sorting);
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
        }

        /// <summary>
        /// 根据工作流编号获取节点关系
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public IList<KeyValueItem> GetKeyValueItems(decimal workflowId)
        {
            List<KeyValueItem> keyValueItems = new List<KeyValueItem>();

            //生成查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomWorkflowMap.ParentProcessId, CustomWorkflowMap.ProcessId FROM CustomWorkflowMap ");
            sb.Append("INNER JOIN CustomWorkflowProcess ON CustomWorkflowMap.ParentProcessId = CustomWorkflowProcess.ProcessId ");
            sb.Append("WHERE CustomWorkflowProcess.WorkflowId = @WorkflowId ORDER BY CustomWorkflowMap.ParentProcessId, CustomWorkflowMap.Sorting");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, DataConvertionHelper.SetDecimal(workflowId));
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal parentProcessId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal processId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            //将创建 CustomWorkflowMapInfo 对象加入集合中
                            keyValueItems.Add(new KeyValueItem(parentProcessId, processId));
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

            return keyValueItems;
        }

        /// <summary>
        /// 更新记录的排序
        /// </summary>
        /// <param name="parentProcessId">移动的节点编号</param>
        /// <param name="processId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateSorting(decimal parentProcessId, decimal processId, MovedDriection movedDriectionOfNode)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sqlFirstSelect = string.Empty;
                    string sqlSecondSelect = string.Empty;
                    string sqlFirstUpdate = string.Empty;
                    string sqlSecondUpdate = string.Empty;
                    decimal otherProcessId = 0;
                    int sorting = 0;
                    int otherSorting = 0;

                    /* 获得父节点编号和当前节点的排序值 */
                    sqlFirstSelect = "SELECT Sorting FROM CustomWorkflowMap WHERE ParentProcessId = @ParentProcessId AND ProcessId = @ProcessId";
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstSelect))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                        db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                        sorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                    }

                    switch (movedDriectionOfNode)
                    {
                        case MovedDriection.Top:
                            sqlFirstUpdate = "UPDATE CustomWorkflowMap SET Sorting = Sorting + 1 WHERE ParentProcessId = @ParentProcessId AND Sorting < @Sorting";
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }

                            sqlFirstUpdate = "UPDATE CustomWorkflowMap SET Sorting = 1 WHERE ParentProcessId = @ParentProcessId AND ProcessId = @ProcessId";
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }
                            break;

                        case MovedDriection.Previous:
                            sqlSecondSelect = "SELECT TOP 1 ProcessId FROM CustomWorkflowMap WHERE ParentProcessId = @ParentProcessId AND Sorting < @Sorting";
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSecondSelect))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                otherProcessId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand), 0);
                            }

                            sqlFirstUpdate = "UPDATE CustomWorkflowMap SET Sorting = @Sorting WHERE ParentProcessId = @ParentProcessId AND ProcessId = @ProcessId";
                            if (otherProcessId > 0 && sorting > 0)
                            {
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                                {
                                    //给参数赋值
                                    db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                                    db.AddInParameter(dbCommand, "Sorting", DbType.Decimal, sorting);
                                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                    {
                                        throw new Exception("更新失败！");
                                    }
                                }
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                                {
                                    //给参数赋值
                                    db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                                    db.AddInParameter(dbCommand, "Sorting", DbType.Decimal, sorting - 1);
                                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                    {
                                        throw new Exception("更新失败！");
                                    }
                                }
                            }
                            break;

                        case MovedDriection.Next:
                            sqlSecondSelect = "SELECT TOP 1 ProcessId FROM CustomWorkflowMap WHERE ParentProcessId = @ParentProcessId AND Sorting > @Sorting";
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSecondSelect))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                otherProcessId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand), 0);
                            }

                            sqlFirstUpdate = "UPDATE CustomWorkflowMap SET Sorting = @Sorting WHERE ParentProcessId = @ParentProcessId AND ProcessId = @ProcessId";
                            if (otherProcessId > 0 && sorting > 0)
                            {
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                                {
                                    //给参数赋值
                                    db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                                    db.AddInParameter(dbCommand, "Sorting", DbType.Decimal, sorting);
                                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                    {
                                        throw new Exception("更新失败！");
                                    }
                                }
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                                {
                                    //给参数赋值
                                    db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                                    db.AddInParameter(dbCommand, "Sorting", DbType.Decimal, sorting + 1);
                                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                    {
                                        throw new Exception("更新失败！");
                                    }
                                }
                            }
                            break;

                        case MovedDriection.Bottom:
                            StringBuilder sbBottom = new StringBuilder();
                            sqlFirstUpdate = "SELECT MAX(Sorting) FROM CustomWorkflowMap WHERE ParentProcessId = @ParentProcessId ";
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                otherSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                            }
                            sqlFirstUpdate = "UPDATE CustomWorkflowMap SET Sorting = Sorting - 1 WHERE ParentProcessId = @ParentProcessId AND Sorting > @Sorting ";                      
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }

                            sqlFirstUpdate = "UPDATE CustomWorkflowMap SET Sorting = @Sorting WHERE ParentProcessId = @ParentProcessId AND ProcessId = @ProcessId";
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                                db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                                db.AddInParameter(dbCommand, "Sorting", DbType.Decimal, otherSorting);
                                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }

                            break;
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
        /// 根据工作流ID获得当前工作流程关系图
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public DataSet GetProcessMap(decimal workflowId)
        {
            DataSet ds = null;

            //生成查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomWorkflowMap.ParentProcessId, A.ProcessName as ParentProcessName, CustomWorkflowMap.ProcessId, B.ProcessName FROM CustomWorkflowMap ");
            sb.Append("INNER JOIN CustomWorkflowProcess A ON CustomWorkflowMap.ParentProcessId = A.ProcessId ");
            sb.Append("INNER JOIN CustomWorkflowProcess B ON  CustomWorkflowMap.ProcessId = B.ProcessId ");
            sb.Append("WHERE A.WorkflowId = @WorkflowId ORDER BY CustomWorkflowMap.ParentProcessId, CustomWorkflowMap.Sorting");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, DataConvertionHelper.SetDecimal(workflowId));
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

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomWorkflowMapInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomWorkflowMapInfo 对象列表</returns>
        private IList<CustomWorkflowMapInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomWorkflowMapInfo> customWorkflowMapInfos = new List<CustomWorkflowMapInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomWorkflowMap");
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
                            decimal parentProcessId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal processId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[2]);
                            //将创建 CustomWorkflowMapInfo 对象加入集合中
                            customWorkflowMapInfos.Add(new CustomWorkflowMapInfo(parentProcessId, processId, sorting));
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

            return customWorkflowMapInfos;
        }

        /// <summary>
        /// 获得 CustomWorkflowMapInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowMapInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomWorkflowMap");
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
        /// 获得表 CustomWorkflowMap 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomWorkflowMap ", "ParentProcessId", "*", false, false, startPosition,
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
        /// 获得以表 CustomWorkflowMap 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomWorkflowMap ", "ParentProcessId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomWorkflowMap 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomWorkflowMap ", "ParentProcessId", "*", false, false, startPosition,
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
        /// 获得以表 CustomWorkflowMap 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomWorkflowMap ", "ParentProcessId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 CustomWorkflowMapInfo 对象
        /// </summary>
        /// <param name="parentProcessId">流程编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal parentProcessId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM CustomWorkflowMap WHERE ParentProcessId = @ParentProcessId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
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
        /// 删除满足条件的所有  CustomWorkflowMapInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomWorkflowMap");
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
