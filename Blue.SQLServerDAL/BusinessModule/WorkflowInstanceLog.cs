//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: WorkflowInstanceLog.cs
// 描述: WorkflowInstanceLog 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/8/27
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
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// WorkflowInstanceLog 表的数据层访问类
    /// </summary>
    public class WorkflowInstanceLog : IWorkflowInstanceLog
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public WorkflowInstanceLog()
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 WorkflowInstanceLog 表中插入一条新记录
		/// </summary>
		/// <param name="workflowInstanceLogInfo">workflowInstanceLogInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(WorkflowInstanceLogInfo workflowInstanceLogInfo)
		{
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            return Insert(workflowInstanceLogInfo, db, null);
        }

        /// <summary>
		/// 获得 WorkflowInstanceLogInfo 对象
		/// </summary>
		///<param name="logId">日志编号</param>
		/// <returns> WorkflowInstanceLogInfo 对象</returns>
		public WorkflowInstanceLogInfo GetModelInfo(decimal logId)
		{			
			WorkflowInstanceLogInfo  workflowInstanceLogInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("LogId", "LogId", DbType.Decimal, logId, DataFieldCondition.Equal));

            //创建集合对象
            IList<WorkflowInstanceLogInfo>  workflowInstanceLogInfos = GetModelInfos(whereConditons, null, true);
            if (workflowInstanceLogInfos != null && workflowInstanceLogInfos.Count > 0)
            {
                workflowInstanceLogInfo = workflowInstanceLogInfos[0];
            }

            return workflowInstanceLogInfo;            
		}
        
        /// <summary>
		/// 更新 WorkflowInstanceLogInfo 对象
		/// </summary>
		/// <param name="workflowInstanceLogInfo">WorkflowInstanceLogInfo 对象</param>
		public void Update(WorkflowInstanceLogInfo workflowInstanceLogInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE WorkflowInstanceLog SET ProcessId = @ProcessId, InstanceId = @InstanceId, UserId = @UserId, ");
			sb.Append("ReviewedAction = @ReviewedAction, Comment = @Comment, TimeReviewed = @TimeReviewed ");
			sb.Append("WHERE LogId = @LogId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "LogId", DbType.Decimal, workflowInstanceLogInfo.LogId);
					db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, workflowInstanceLogInfo.ProcessId);
					db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, workflowInstanceLogInfo.InstanceId);
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, workflowInstanceLogInfo.UserId);
					db.AddInParameter(dbCommand, "ReviewedAction", DbType.Byte, workflowInstanceLogInfo.ReviewedAction);
					db.AddInParameter(dbCommand, "Comment", DbType.String, workflowInstanceLogInfo.Comment);
					db.AddInParameter(dbCommand, "TimeReviewed", DbType.DateTime, workflowInstanceLogInfo.TimeReviewed);
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
		///  删除 WorkflowInstanceLogInfo 对象
		/// </summary>
	    ///<param name="logId">日志编号</param>
		public void Delete(decimal logId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM WorkflowInstanceLog ");
			sb.Append("WHERE LogId = @LogId");

            WorkflowInstanceLogAndStep workflowInstanceLogAndStep = new WorkflowInstanceLogAndStep();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    workflowInstanceLogAndStep.DeleteBySecondForeignKey(logId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "LogId", DbType.Decimal, logId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
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
		/// 获得 WorkflowInstanceLogInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>WorkflowInstanceLogInfo 对象列表</returns>
		public IList<WorkflowInstanceLogInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 WorkflowInstanceLog 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>WorkflowInstanceLogInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "WorkflowInstanceLog ", "LogId", false, whereConditons);
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
        /// 获得草稿日志
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="instanceId"></param>
        /// <param name="parentUserId"></param>
        /// <returns></returns>
        public WorkflowInstanceLogInfo GetDraftLog(decimal processId, decimal instanceId, decimal parentUserId)
        {
            WorkflowInstanceLogInfo workflowInstanceLogInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ProcessId", "ProcessId", DbType.Decimal, processId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("InstanceId", "InstanceId", DbType.Decimal, instanceId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("ParentUserId", "ParentUserId", DbType.Decimal, parentUserId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("IsDraft", "IsDraft", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            
            //创建集合对象
            IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos = GetModelInfos(whereConditons, null, true);
            if (workflowInstanceLogInfos != null && workflowInstanceLogInfos.Count > 0)
            {
                workflowInstanceLogInfo = workflowInstanceLogInfos[0];
            }

            return workflowInstanceLogInfo;
        }

        /// <summary>
        /// 获得工作流实例的处理流程
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal instanceId)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT LogId, A.UserName, A.UserActualName, ReviewedAction, B.UserName AS B_UserName, B.UserActualName AS B_UserActualName, TimeReviewed, Comment FROM WorkflowInstanceLog ");
            sb.Append("INNER JOIN UserAccount A ON WorkflowInstanceLog.ParentUserId = A.UserId ");
            sb.Append("LEFT JOIN UserAccount B ON WorkflowInstanceLog.UserId = B.UserId ");
            sb.Append("WHERE InstanceId = @InstanceId AND WorkflowInstanceLog.IsDraft = @IsDraft ORDER BY WorkflowInstanceLog.TimeReviewed ASC ");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, false);
                    ds = db.ExecuteDataSet(dbCommand);
                }
                ds.Tables[0].Columns["B_UserName"].Caption = "下一步处理人用户名";
                ds.Tables[0].Columns["B_UserActualName"].Caption = "下一步处理人姓名";
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    string caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                    if (!string.IsNullOrWhiteSpace(caption))
                    {
                        dataColumn.Caption = caption;
                    }
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
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="stepId">处理步骤编号</param>
        /// <returns> 最新的审核意见</returns>
        public Dictionary<string, string> GetComments(decimal stepId)
        {
            Dictionary<string, string> comments = new Dictionary<string, string>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT DISTINCT UserName, UserActualName, Comment FROM WorkflowInstanceLog ");
                sb.Append("INNER JOIN WorkflowInstanceLogAndStep ON WorkflowInstanceLog.LogId = WorkflowInstanceLogAndStep.LogId ");
                sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = WorkflowInstanceLog.ParentUserId ");
                sb.Append("WHERE StepId = @StepId");
                
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "StepId", DbType.Decimal, stepId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            string userName = DataConvertionHelper.GetString(dataReader[0]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[1]);
                            string comment = DataConvertionHelper.GetString(dataReader[2]);
                            //将创建 CombinedDataFieldInfo 对象加入集合中
                            comments.Add(string.Format("{0}({1})", userActualName, userName),  comment);
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

            return comments;
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="instanceId">工作流实例编号</param>
        /// <returns> 最新的审核意见</returns>
        public string GetLastestComment(decimal instanceId)
        {
            string comment = string.Empty;

            try
            {
                string sqlSelect = "SELECT TOP 1 Comment FROM WorkflowInstanceLog WHERE InstanceId = @InstanceId AND IsDraft = @IsDraft ORDER BY TimeReviewed DESC";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, false);
                    comment = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return comment;
        }

        /// <summary>
        /// 获得日志的用户编号
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public decimal GetUserId(decimal logId)
        {
            decimal userId = 0;

            string sqlSelect = "SELECT UserId FROM WorkflowInstanceLog WHERE LogId = @LogId ";

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "LogId", DbType.Decimal, logId);
                    userId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userId;
        }

        /// <summary>
        /// 获得日志的父用户编号
        /// </summary>
        /// <param name="logId"></param>
        /// <returns></returns>
        public decimal GetParentUserId(decimal logId)
        {
            decimal parentUserId = 0;

            string sqlSelect = "SELECT ParentUserId FROM WorkflowInstanceLog WHERE LogId = @LogId ";

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "LogId", DbType.Decimal, logId);
                    parentUserId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return parentUserId;
        }

        /// <summary>
        /// 获得流程编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public decimal GetProcessId(decimal logId)
        {
            decimal processId = 0;

            string sqlSelect = "SELECT ProcessId FROM WorkflowInstanceLog WHERE LogId = @LogId ";

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "LogId", DbType.Decimal, logId);
                    processId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return processId;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 删除日志数据
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>

        public void Delete(decimal instanceId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            string[] sqlDeletes = new string[] { "DELETE WorkflowInstanceLogAndStep FROM WorkflowInstanceLogAndStep INNER JOIN WorkflowInstanceLog ON WorkflowInstanceLogAndStep.LogId = WorkflowInstanceLog.LogId WHERE InstanceId = @InstanceId",
                "DELETE FROM WorkflowInstanceLog WHERE InstanceId = @InstanceId" };

            try
            {
                foreach (var sqlDelete in sqlDeletes)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))

                    {
                        db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                        //执行删除操作
                        db.ExecuteNonQuery(dbCommand, transaction);
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
        /// 根据当前节点用户的日志编号列表
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="InstanceId"></param>
        /// <param name="parentUserId"></param>
        /// <returns></returns>
        public IList<decimal> GetUserLogIds(decimal processId, decimal InstanceId, decimal parentUserId)
        {
            IList<decimal> logIds = new List<decimal>();

            try
            {
                string sqlSelect = "SELECT LogId FROM WorkflowInstanceLog WHERE ProcessId = @ProcessId AND InstanceId = @InstanceId AND ParentUserId = @ParentUserId";
                
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, InstanceId);
                    db.AddInParameter(dbCommand, "ParentUserId", DbType.Decimal, parentUserId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            logIds.Add(DataConvertionHelper.GetDecimal(dataReader[0]));
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

            return logIds;
        }

        /// <summary>
        /// 插入操作日志
        /// </summary>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public decimal Insert(WorkflowInstanceLogInfo workflowInstanceLogInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal workflowInstanceLogId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO WorkflowInstanceLog(ProcessId, InstanceId, UserId, ParentUserId, ReviewedAction, Comment, ");
            sb.Append("TimeReviewed, IsDraft)");
            sb.Append("VALUES (@ProcessId, @InstanceId, @UserId, @ParentUserId, @ReviewedAction, @Comment, ");
            sb.Append("@TimeReviewed, @IsDraft);");
            sb.Append("SET @LogId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "LogId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, workflowInstanceLogInfo.ProcessId);
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, workflowInstanceLogInfo.InstanceId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(workflowInstanceLogInfo.UserId));
                    db.AddInParameter(dbCommand, "ParentUserId", DbType.Decimal, workflowInstanceLogInfo.ParentUserId);
                    db.AddInParameter(dbCommand, "ReviewedAction", DbType.Byte, workflowInstanceLogInfo.ReviewedAction);
                    db.AddInParameter(dbCommand, "Comment", DbType.String, workflowInstanceLogInfo.Comment);
                    db.AddInParameter(dbCommand, "TimeReviewed", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, workflowInstanceLogInfo.IsDraft);
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
                    workflowInstanceLogId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@LogId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return workflowInstanceLogId;
        }

        /// <summary>
        /// 插入操作日志
        /// </summary>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void InsertOrUpdate(WorkflowInstanceLogInfo workflowInstanceLogInfo, SqlDatabase db, DbTransaction transaction)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("IF EXISTS(SELECT LogId FROM WorkflowInstanceLog WHERE ProcessId = @ProcessId AND InstanceId = @InstanceId AND ParentUserId = @ParentUserId AND IsDraft = @IsDraft) ");
            sb.Append("BEGIN UPDATE WorkflowInstanceLog SET Comment = @Comment, TimeReviewed = @TimeReviewed WHERE ProcessId = @ProcessId AND InstanceId = @InstanceId AND ParentUserId = @ParentUserId AND IsDraft = @IsDraft ");
            sb.Append("END ELSE ");
            sb.Append("BEGIN INSERT INTO WorkflowInstanceLog(ProcessId, InstanceId, UserId, ParentUserId, ReviewedAction, Comment, TimeReviewed, IsDraft) ");
            sb.Append("VALUES (@ProcessId, @InstanceId, @UserId, @ParentUserId, @ReviewedAction, @Comment, @TimeReviewed, @IsDraft) END");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, workflowInstanceLogInfo.ProcessId);
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, workflowInstanceLogInfo.InstanceId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(workflowInstanceLogInfo.UserId));
                    db.AddInParameter(dbCommand, "ParentUserId", DbType.Decimal, workflowInstanceLogInfo.ParentUserId);
                    db.AddInParameter(dbCommand, "ReviewedAction", DbType.Byte, workflowInstanceLogInfo.ReviewedAction);
                    db.AddInParameter(dbCommand, "Comment", DbType.String, workflowInstanceLogInfo.Comment);
                    db.AddInParameter(dbCommand, "TimeReviewed", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, workflowInstanceLogInfo.IsDraft);
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
        }

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 WorkflowInstanceLogInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>WorkflowInstanceLogInfo 对象列表</returns>
        private IList<WorkflowInstanceLogInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<WorkflowInstanceLogInfo>  workflowInstanceLogInfos = new List<WorkflowInstanceLogInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM WorkflowInstanceLog");
            
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
                            decimal logId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal processId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal instanceId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal parentUserId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            byte reviewedAction = DataConvertionHelper.GetByte(dataReader[5]);
                            string comment = DataConvertionHelper.GetString(dataReader[6]);
                            DateTime timeReviewed = DataConvertionHelper.GetDateTime(dataReader[7]);
                            bool isDraft = DataConvertionHelper.GetBoolean(dataReader[8]);
                            //将创建 WorkflowInstanceLogInfo 对象加入集合中
                            workflowInstanceLogInfos.Add(new WorkflowInstanceLogInfo(logId, processId, instanceId, userId, parentUserId,
                            reviewedAction, comment, timeReviewed, isDraft));
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
            
			return workflowInstanceLogInfos;
		}
        
		
		/// <summary>
		/// 获得 WorkflowInstanceLogInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>WorkflowInstanceLogInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM WorkflowInstanceLog");
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
        /// 获得表 WorkflowInstanceLog 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "WorkflowInstanceLog ", "LogId", "*", false, false, startPosition, 
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
        /// 获得以表 WorkflowInstanceLog 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "WorkflowInstanceLog ", "LogId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 WorkflowInstanceLog 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "WorkflowInstanceLog ", "LogId", "*", false, false, startPosition, 
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
        /// 获得以表 WorkflowInstanceLog 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "WorkflowInstanceLog ", "LogId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  WorkflowInstanceLogInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM WorkflowInstanceLog");
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
