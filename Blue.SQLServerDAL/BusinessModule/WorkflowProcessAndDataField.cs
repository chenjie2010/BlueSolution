//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：WorkflowProcessAndDataField.cs
// 描述：WorkflowProcessAndDataField 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/4/4
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
    /// WorkflowProcessAndDataField 表的数据层访问类
    /// </summary>
    public class WorkflowProcessAndDataField : CorrelatedTableDataAcess, IWorkflowProcessAndDataField
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public WorkflowProcessAndDataField() : base("WorkflowProcessAndDataField", "ProcessId", "DataFieldId")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 WorkflowProcessAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo">workflowProcessAndDataFieldInfo 对象</param>
        public void Insert(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO WorkflowProcessAndDataField(ProcessId, DataFieldId, FstCondition, ScdCondition, BoolValue, StringValue, FstIntegerValue, ScdIntegerValue, ");
            sb.Append("FstDecimalValue, ScdDecimalValue, FstTimeValue, ScdTimeValue, NextRelation, Sorting)");
            sb.Append("VALUES (@ProcessId, @DataFieldId, @FstCondition, @ScdCondition, @BoolValue, @StringValue, @FstIntegerValue, @ScdIntegerValue, ");
            sb.Append("@FstDecimalValue, @ScdDecimalValue, @FstTimeValue, @ScdTimeValue, @NextRelation, @Sorting)");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            workflowProcessAndDataFieldInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "WorkflowProcessAndDataField", "Sorting", "ProcessId", workflowProcessAndDataFieldInfo.ProcessId, 0) + 1;
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, workflowProcessAndDataFieldInfo.ProcessId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, workflowProcessAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "FstCondition", DbType.Byte, workflowProcessAndDataFieldInfo.FstCondition);
                    db.AddInParameter(dbCommand, "ScdCondition", DbType.Byte, workflowProcessAndDataFieldInfo.ScdCondition);
                    db.AddInParameter(dbCommand, "BoolValue", DbType.Boolean, workflowProcessAndDataFieldInfo.BoolValue);
                    db.AddInParameter(dbCommand, "StringValue", DbType.String, workflowProcessAndDataFieldInfo.StringValue);
                    db.AddInParameter(dbCommand, "FstIntegerValue", DbType.Int32, DataConvertionHelper.SetInt(workflowProcessAndDataFieldInfo.FstIntegerValue));
                    db.AddInParameter(dbCommand, "ScdIntegerValue", DbType.Int32, DataConvertionHelper.SetInt(workflowProcessAndDataFieldInfo.ScdIntegerValue));
                    db.AddInParameter(dbCommand, "FstDecimalValue", DbType.Decimal, DataConvertionHelper.SetDecimal(workflowProcessAndDataFieldInfo.FstDecimalValue));
                    db.AddInParameter(dbCommand, "ScdDecimalValue", DbType.Decimal, DataConvertionHelper.SetDecimal(workflowProcessAndDataFieldInfo.ScdDecimalValue));
                    db.AddInParameter(dbCommand, "FstTimeValue", DbType.DateTime, DataConvertionHelper.SetDateTime(workflowProcessAndDataFieldInfo.FstTimeValue));
                    db.AddInParameter(dbCommand, "ScdTimeValue", DbType.DateTime, DataConvertionHelper.SetDateTime(workflowProcessAndDataFieldInfo.ScdTimeValue));
                    db.AddInParameter(dbCommand, "NextRelation", DbType.Byte, workflowProcessAndDataFieldInfo.NextRelation);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, workflowProcessAndDataFieldInfo.Sorting);
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
		/// 获得 WorkflowProcessAndDataFieldInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		///<param name="dataFieldId">字段编号</param>
		/// <returns> WorkflowProcessAndDataFieldInfo 对象</returns>
		public WorkflowProcessAndDataFieldInfo GetModelInfo(decimal processId, decimal dataFieldId)
        {
            WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ProcessId", "ProcessId", DbType.Decimal, processId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("DataFieldId", "DataFieldId", DbType.Decimal, dataFieldId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            //创建集合对象
            IList<WorkflowProcessAndDataFieldInfo> workflowProcessAndDataFieldInfos = GetModeInfos(whereConditons, null, true);
            if (workflowProcessAndDataFieldInfos != null && workflowProcessAndDataFieldInfos.Count > 0)
            {
                workflowProcessAndDataFieldInfo = workflowProcessAndDataFieldInfos[0];
            }

            return workflowProcessAndDataFieldInfo;
        }

        /// <summary>
        /// 更新 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo">WorkflowProcessAndDataFieldInfo 对象</param>
        public void Update(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE WorkflowProcessAndDataField SET FstCondition = @FstCondition, ScdCondition = @ScdCondition, BoolValue = @BoolValue, StringValue = @StringValue, ");
            sb.Append("FstIntegerValue = @FstIntegerValue, ScdIntegerValue = @ScdIntegerValue, FstDecimalValue = @FstDecimalValue, ");
            sb.Append("ScdDecimalValue = @ScdDecimalValue, FstTimeValue = @FstTimeValue, ScdTimeValue = @ScdTimeValue, NextRelation = @NextRelation ");
            sb.Append("WHERE ProcessId = @ProcessId AND DataFieldId = @DataFieldId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, workflowProcessAndDataFieldInfo.ProcessId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, workflowProcessAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "FstCondition", DbType.Byte, workflowProcessAndDataFieldInfo.FstCondition);
                    db.AddInParameter(dbCommand, "ScdCondition", DbType.Byte, workflowProcessAndDataFieldInfo.ScdCondition);
                    db.AddInParameter(dbCommand, "BoolValue", DbType.Boolean, workflowProcessAndDataFieldInfo.BoolValue);
                    db.AddInParameter(dbCommand, "StringValue", DbType.String, workflowProcessAndDataFieldInfo.StringValue);
                    db.AddInParameter(dbCommand, "FstIntegerValue", DbType.Int32, DataConvertionHelper.SetInt(workflowProcessAndDataFieldInfo.FstIntegerValue));
                    db.AddInParameter(dbCommand, "ScdIntegerValue", DbType.Int32, DataConvertionHelper.SetInt(workflowProcessAndDataFieldInfo.ScdIntegerValue));
                    db.AddInParameter(dbCommand, "FstDecimalValue", DbType.Decimal, DataConvertionHelper.SetDecimal(workflowProcessAndDataFieldInfo.FstDecimalValue));
                    db.AddInParameter(dbCommand, "ScdDecimalValue", DbType.Decimal, DataConvertionHelper.SetDecimal(workflowProcessAndDataFieldInfo.ScdDecimalValue));
                    db.AddInParameter(dbCommand, "FstTimeValue", DbType.DateTime, DataConvertionHelper.SetDateTime(workflowProcessAndDataFieldInfo.FstTimeValue));
                    db.AddInParameter(dbCommand, "ScdTimeValue", DbType.DateTime, DataConvertionHelper.SetDateTime(workflowProcessAndDataFieldInfo.ScdTimeValue));
                    db.AddInParameter(dbCommand, "NextRelation", DbType.Byte, workflowProcessAndDataFieldInfo.NextRelation);
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
        ///  删除 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        ///<param name="dataFieldId">字段编号</param>
        public void Delete(decimal processId, decimal dataFieldId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM WorkflowProcessAndDataField ");
            sb.Append("WHERE ProcessId = @ProcessId AND DataFieldId = @DataFieldId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
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
        /// 获得 WorkflowProcessAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>WorkflowProcessAndDataFieldInfo 对象列表</returns>
        public IList<WorkflowProcessAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModeInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 WorkflowProcessAndDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>WorkflowProcessAndDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "WorkflowProcessAndDataField ", "ProcessId", false, whereConditons);
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
        /// 更新记录的排序
        /// </summary>
        /// <param name="processId">移动的节点编号</param>
        /// <param name="dataFieldId">交换的移动的节点编号</param>
        /// <param name="movedDriectionOfNode">移动动作</param>
        public void UpdateSorting(decimal processId, decimal dataFieldId, MovedDriection movedDriectionOfNode)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            DataAccessHandler.UpdateSorting(db, processId, dataFieldId, "ProcessId", "DataFieldId",
                "WorkflowProcessAndDataField", movedDriectionOfNode);            
        }

        /// <summary>
        /// 数据集
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal processId)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDataField.DataFieldId, CustomDataField.DataFieldProperty, CustomDataField.DataFieldType, CustomDatabase.DatabaseName, CustomCategory.CategoryName, CustomTable.LogicalName, ");
            sb.Append("CustomDataField.LogicalName AS DataFieldLogicalName, FstCondition, ScdCondition, BoolValue, StringValue, FstIntegerValue, ");
            sb.Append("ScdIntegerValue, FstDecimalValue, ScdDecimalValue, FstTimeValue, ScdTimeValue, NextRelation FROM WorkflowProcessAndDataField ");
            sb.Append("INNER JOIN CustomDataField ON WorkflowProcessAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("INNER JOIN CustomTable ON CustomDataField.TableId = CustomTable.TableId ");
            sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("INNER JOIN  CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("WHERE ProcessId = @ProcessId ORDER BY WorkflowProcessAndDataField.Sorting");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
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
        /// 获得 WorkflowProcessAndDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public IList<WorkflowProcessAndDataFieldInfo> GetModelInfos(decimal processId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("ProcessId", "ProcessId", System.Data.DbType.Decimal, processId, DataFieldCondition.Equal));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            //创建集合对象
            IList<WorkflowProcessAndDataFieldInfo> workflowProcessAndDataFieldInfos = GetModelInfos(whereConditons, null);

            return workflowProcessAndDataFieldInfos;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>WorkflowProcessAndDataFieldInfo 对象列表</returns>
        private IList<WorkflowProcessAndDataFieldInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<WorkflowProcessAndDataFieldInfo> workflowProcessAndDataFieldInfos = new List<WorkflowProcessAndDataFieldInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM WorkflowProcessAndDataField");
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
                            decimal processId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            byte fstCondition = DataConvertionHelper.GetByte(dataReader[2]);
                            byte scdCondition = DataConvertionHelper.GetByte(dataReader[3]);
                            bool boolValue = DataConvertionHelper.GetBoolean(dataReader[4]);
                            string stringValue = DataConvertionHelper.GetString(dataReader[5]);
                            int fstIntegerValue = DataConvertionHelper.GetInt(dataReader[6]);
                            int scdIntegerValue = DataConvertionHelper.GetInt(dataReader[7]);
                            decimal fstDecimalValue = DataConvertionHelper.GetDecimal(dataReader[8]);
                            decimal scdDecimalValue = DataConvertionHelper.GetDecimal(dataReader[9]);
                            DateTime fstTimeValue = DataConvertionHelper.GetDateTime(dataReader[10]);
                            DateTime scdTimeValue = DataConvertionHelper.GetDateTime(dataReader[11]);
                            byte nextRelation = DataConvertionHelper.GetByte(dataReader[12]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[13]);
                            //将创建 WorkflowProcessAndDataFieldInfo 对象加入集合中
                            workflowProcessAndDataFieldInfos.Add(new WorkflowProcessAndDataFieldInfo(processId, dataFieldId, fstCondition, scdCondition, boolValue,
                            stringValue, fstIntegerValue, scdIntegerValue, fstDecimalValue, scdDecimalValue,
                            fstTimeValue, scdTimeValue, nextRelation, sorting));
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

            return workflowProcessAndDataFieldInfos;
        }

        /// <summary>
        /// 获得 WorkflowProcessAndDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>WorkflowProcessAndDataFieldInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM WorkflowProcessAndDataField");
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
        /// 获得表 WorkflowProcessAndDataField 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "WorkflowProcessAndDataField ", "ProcessId", "*", false, false, startPosition,
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
        /// 获得以表 WorkflowProcessAndDataField 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "WorkflowProcessAndDataField ", "ProcessId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 WorkflowProcessAndDataField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "WorkflowProcessAndDataField ", "ProcessId", "*", false, false, startPosition,
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
        /// 获得以表 WorkflowProcessAndDataField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "WorkflowProcessAndDataField ", "ProcessId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 WorkflowProcessAndDataFieldInfo 对象
        /// </summary>
        /// <param name="processId">流程编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal processId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM WorkflowProcessAndDataField WHERE ProcessId = @ProcessId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
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
        /// 删除满足条件的所有  WorkflowProcessAndDataFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM WorkflowProcessAndDataField");
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
