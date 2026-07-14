//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：WorkflowInstanceStep.cs
// 描述：WorkflowInstanceStep 数据层访问类
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
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// WorkflowInstanceStep 表的数据层访问类
    /// </summary>
    public class WorkflowInstanceStep : IWorkflowInstanceStep
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public WorkflowInstanceStep()
		{
		}

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 WorkflowInstanceStep 表中插入一条新记录
        /// </summary>
        /// <param name="workflowInstanceStepInfo">workflowInstanceStepInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(WorkflowInstanceStepInfo workflowInstanceStepInfo)
        {
            //自动增加的关键字的值
            decimal workflowInstanceStepId = 0;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                workflowInstanceStepId = Insert(workflowInstanceStepInfo, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return workflowInstanceStepId;
        }

        /// <summary>
		/// 获得 WorkflowInstanceStepInfo 对象
		/// </summary>
		///<param name="stepId">步骤编号</param>
		/// <returns> WorkflowInstanceStepInfo 对象</returns>
		public WorkflowInstanceStepInfo GetModelInfo(decimal stepId)
		{			
			WorkflowInstanceStepInfo workflowInstanceStepInfo = null;            

            IList<WhereConditon> whereConditons = new List<WhereConditon>();            
            //给参数赋值
            whereConditons.Add(new WhereConditon("StepId", "StepId", DbType.Decimal, stepId, DataFieldCondition.Equal));
            
            //创建集合对象
			IList<WorkflowInstanceStepInfo> workflowInstanceStepInfos = GetModeInfos(whereConditons, null, true);
            if (workflowInstanceStepInfos != null && workflowInstanceStepInfos.Count > 0)
            {
                workflowInstanceStepInfo = workflowInstanceStepInfos[0];
            }          

            return workflowInstanceStepInfo;
		}
        
        /// <summary>
		/// 更新 WorkflowInstanceStepInfo 对象
		/// </summary>
		/// <param name="workflowInstanceStepInfo">WorkflowInstanceStepInfo 对象</param>
		public void Update(WorkflowInstanceStepInfo workflowInstanceStepInfo)
		{					
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
                Update(workflowInstanceStepInfo, db, null);
			}
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
		}        
        
        /// <summary>
		///  删除 WorkflowInstanceStepInfo 对象
		/// </summary>
	    ///<param name="stepId">步骤编号</param>
		public void Delete(decimal stepId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM WorkflowInstanceStep ");
			sb.Append("WHERE StepId = @StepId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "StepId", DbType.Decimal, stepId);
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
		/// 获得 WorkflowInstanceStepInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>WorkflowInstanceStepInfo 对象列表</returns>
		public IList<WorkflowInstanceStepInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			return GetModeInfos(whereConditons, sortingCondtions, false);
		}               
        
        /// <summary>
		/// 获得 WorkflowInstanceStep 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>WorkflowInstanceStepInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "WorkflowInstanceStep ", "StepId", false, whereConditons);
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
        /// 获得工作流审核人数据
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal instanceId)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ProcessName, UserName, UserActualName, ReviewedStatus, TimeReviewed, ProcessCounter FROM WorkflowInstanceStep ");
            sb.Append("INNER JOIN CustomWorkflowProcess ON CustomWorkflowProcess.ProcessId = WorkflowInstanceStep.ProcessId ");
            sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = WorkflowInstanceStep.UserId ");
            sb.Append("WHERE InstanceId = @InstanceId ");
           
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
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
        /// 根据条件获得下一个审核人
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="reviewerIds"></param>
        /// <param name="workflowHandlerMode"></param>
        /// <returns></returns>
        public decimal GetNextReviewerId(decimal processId, IList<decimal> reviewerIds, WorkflowHandlerMode workflowHandlerMode)
        {
            decimal reviewerId = decimal.MinValue;

            try
            {
                IList<decimal> currentReviewerIds = new List<decimal>();
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT TOP {0} UserId FROM WorkflowInstanceStep ", reviewerIds.Count);
                sb.Append("WHERE ProcessId = @ProcessId ");
                if (workflowHandlerMode == WorkflowHandlerMode.RemainingWorkflowsAveraged || workflowHandlerMode == WorkflowHandlerMode.InTurn)
                {
                    sb.Append("AND ReviewedStatus = @ReviewedStatus ");
                }
                sb.Append("ORDER BY TimeReviewed DESC ");

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                    switch (workflowHandlerMode)
                    {
                        case WorkflowHandlerMode.InTurn:
                            db.AddInParameter(dbCommand, "ReviewedStatus", DbType.Byte, (byte)ReviewedStatus.Reviewing);
                            break;

                        case WorkflowHandlerMode.RemainingWorkflowsAveraged:
                            db.AddInParameter(dbCommand, "ReviewedStatus", DbType.Byte, (byte)ReviewedStatus.Completed);
                            break;
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            currentReviewerIds.Add(DataConvertionHelper.GetDecimal(dataReader[0]));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
                foreach (var viewerId in reviewerIds)
                {
                    if (!currentReviewerIds.Contains(viewerId))
                    {
                        reviewerId = viewerId;
                        break;
                    }
                }
                if (reviewerId <= 0)
                {
                    reviewerId = reviewerIds[0];
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }


            return reviewerId;
        }

        /// <summary>
        /// 根据实例编号获得实例的审核状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public Dictionary<decimal, ReviewedStatus> GetReviewedStatus(decimal instanceId)
        {
            Dictionary<decimal, ReviewedStatus> reviewedStatus = new Dictionary<decimal, ReviewedStatus>();

            try
            {
                string sqlSelect = "SELECT StepId, ReviewedStatus FROM WorkflowInstanceStep WHERE InstanceId = @InstanceId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal stepId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            ReviewedStatus status = (ReviewedStatus)DataConvertionHelper.GetByte(dataReader[1]);
                            reviewedStatus.Add(stepId, status);
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

            return reviewedStatus;
        }

        /// <summary>
        /// 获得工作流程的节点分类
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public decimal GetProcessId(decimal stepId)
        {
            decimal processId = 0;

            try
            {
                string sqlSelect = "SELECT ProcessId FROM WorkflowInstanceStep WHERE StepId = @StepId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "StepId", DbType.Decimal, DataConvertionHelper.SetDecimal(stepId));
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
        
        /// <summary>
        /// 获得驳回对象列表
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRejectTargets(decimal stepId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT WorkflowInstanceLogAndStep.LogId, CustomWorkflowProcess.ProcessName, UserAccount.UserName, UserAccount.UserActualName FROM WorkflowInstanceLogAndStep");
            sb.Append("INNER JOIN WorkflowInstanceLog  ON WorkflowInstanceLog.LogId = WorkflowInstanceLogAndStep.LogId ");
            sb.Append("INNER JOIN CustomWorkflowProcess ON CustomWorkflowProcess.ProcessId = WorkflowInstanceLog.ProcessId ");
            sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = WorkflowInstanceLog.UserId ");
            sb.Append("WHERE WorkflowInstanceLogAndStep.StepId = @StepId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "StepId", DbType.Decimal, stepId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal logId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string processName = DataConvertionHelper.GetString(dataReader[1]);
                            string userName = DataConvertionHelper.GetString(dataReader[2]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[3]);                            
                            commonNodes.Add(new CommonNode(logId, stepId, string.Format("{0}({1},{2})", processName, userActualName, userName)));
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
        /// 驳回工作流
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="logIds"></param>
        /// <param name="processId"></param>
        /// <param name="instanceId"></param>
        /// <param name="workflowInstanceLogInfos"></param>
        public void RejectWorkflowInstance(decimal stepId, IList<decimal> logIds, decimal processId, decimal instanceId,
            IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            CustomWorkflowProcess customWorkflowProcess = new CustomWorkflowProcess();
            WorkflowInstanceLog workflowInstanceLog = new WorkflowInstanceLog();
            List<decimal> processIds = new List<decimal>();
            List<decimal> parentProcessIds = new List<decimal>();
            foreach (var logId in logIds)
            {
                decimal parentProcessId = workflowInstanceLog.GetProcessId(logId);
                parentProcessIds.Add(parentProcessId);
                IList<decimal> childProcessIds = customWorkflowProcess.GetChildNodeIds(parentProcessId);
                processIds.AddRange(childProcessIds);
            }

            WorkflowInstanceLogAndStep workflowInstanceLogAndStep = new WorkflowInstanceLogAndStep();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {                    
                    Update(stepId, (byte)ReviewedStatus.Pending, db, transaction);
                    foreach(var childProcessId in processIds)
                    {
                        if (childProcessId != processId)
                        {
                            Update(processId, instanceId, (byte)ReviewedStatus.Pending, db, transaction);
                        }
                    }
                    foreach (var parentProcessId in parentProcessIds)
                    {
                        Update(parentProcessId, instanceId, (byte)ReviewedStatus.Reviewing, db, transaction);
                    }
                    workflowInstanceLogAndStep.Delete(stepId, logIds, db, transaction);
                    foreach (var workflowInstanceLogInfo in workflowInstanceLogInfos)
                    {
                        workflowInstanceLog.Insert(workflowInstanceLogInfo, db, transaction);
                    }                
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
                transaction.Commit();
            }
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核人编号
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核人信息</returns>
        public Dictionary<decimal, string> GetLastestReviewers(decimal instanceId)
        {
            Dictionary<decimal, string> lastestReviewers = new Dictionary<decimal, string>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT WorkflowInstanceStep.UserId, UserAccount.UserName, UserAccount.UserActualName FROM WorkflowInstanceStep ");
                sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = WorkflowInstanceStep.UserId WHERE WorkflowInstanceStep.InstanceId = @InstanceId ");
                sb.Append("AND WorkflowInstanceStep.ReviewedStatus = @ReviewedStatus ");
                sb.Append("ORDER BY WorkflowInstanceStep.TimeReviewed DESC ");
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "ReviewedStatus", DbType.Byte, (byte)ReviewedStatus.Reviewing);

                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userName = DataConvertionHelper.GetString(dataReader[1]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[2]);
                            lastestReviewers.Add(userId, string.Format("{0}({1})", userActualName, userName));
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

            return lastestReviewers;
        }        

        /// <summary>
        /// 获得表 WorkflowInstance  的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetWorkflowInstanceAudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("CustomWorkflowInstance", "InstanceId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomWorkflowInstance", "UserAccount", "UserId", TableJoin.InnerJoin));
                ds = DataAccessHandler.GetPageRecord(db, "WorkflowInstanceStep", "InstanceId", "StepId, CustomWorkflowInstance.InstanceId, InstanceName, InstanceStatus, UserName, UserActualName, TimeReviewed", false, false, tableLinks, startPosition,
                    count, whereConditons, ref totalCount);
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
        /// 获得表 WorkflowInstance  的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetWorkflowInstanceUnaudited(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("CustomWorkflowInstance", "InstanceId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomWorkflowInstance", "UserAccount", "UserId", TableJoin.InnerJoin));

                ds = DataAccessHandler.GetPageRecord(db, "WorkflowInstanceStep", "StepId", "StepId, CustomWorkflowInstance.InstanceId, InstanceName, InstanceStatus, UserName, UserActualName, TimeSumbitted", false, false, tableLinks, startPosition,
                    count, whereConditons, ref totalCount);
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

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 删除处理人数据
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
                        
		public void Delete(decimal instanceId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            string[] sqlDeletes = new string[] { "DELETE WorkflowInstanceLogAndStep FROM WorkflowInstanceLogAndStep INNER JOIN WorkflowInstanceStep ON WorkflowInstanceLogAndStep.StepId = WorkflowInstanceStep.StepId WHERE InstanceId = @InstanceId",
                "DELETE FROM WorkflowInstanceStep WHERE InstanceId = @InstanceId" };

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
        /// 获得动态节点或是选择节点计数器
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public int GetProcessCounter(decimal stepId)
        {
            int processCounter = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ProcessCounter FROM WorkflowInstanceStep INNER JOIN CustomWorkflowProcess ON WorkflowInstanceStep.ProcessId = CustomWorkflowProcess.ProcessId ");
            sb.Append("WHERE WorkflowInstanceStep.StepId = @StepId AND(ProcessType = @ProcessType_1 OR ProcessType = @ProcessType_2) ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "StepId", DbType.Decimal, stepId);
                    db.AddInParameter(dbCommand, "ProcessType_1", DbType.Byte, (byte)WorkflowProcessType.SelectiveBranch);
                    db.AddInParameter(dbCommand, "ProcessType_2", DbType.Byte, (byte)WorkflowProcessType.DynamicBranchInDeps);
                    //执行插入操作
                    processCounter = DataConvertionHelper.GetConvertedInt(db.ExecuteNonQuery(dbCommand), 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return processCounter;
        }

        /// <summary>
        /// 向 WorkflowInstanceStep 表中插入一条新记录
        /// </summary>
        /// <param name="workflowInstanceStepInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public decimal Insert(WorkflowInstanceStepInfo workflowInstanceStepInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal workflowInstanceStepId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO WorkflowInstanceStep(InstanceId, ProcessId, UserId, ReviewedStatus, TimeReviewed, ProcessCounter)");
            sb.Append("VALUES (@InstanceId, @ProcessId, @UserId, @ReviewedStatus, @TimeReviewed, @ProcessCounter)");
            sb.Append("SET @StepId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "StepId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, workflowInstanceStepInfo.InstanceId);
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, workflowInstanceStepInfo.ProcessId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, workflowInstanceStepInfo.UserId);
                    db.AddInParameter(dbCommand, "ReviewedStatus", DbType.Byte, workflowInstanceStepInfo.ReviewedStatus);
                    db.AddInParameter(dbCommand, "TimeReviewed", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "ProcessCounter", DbType.Int32, workflowInstanceStepInfo.ProcessCounter);
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
                    workflowInstanceStepId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@StepId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return workflowInstanceStepId;
        }

        /// <summary>
        /// 更新状态
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="reviewedStatus"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(decimal stepId, byte reviewedStatus, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
             string sqlUpdate = "UPDATE WorkflowInstanceStep SET ReviewedStatus = @ReviewedStatus, TimeReviewed = @TimeReviewed WHERE StepId = @StepId AND ReviewedStatus != @ReviewedStatus";

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "StepId", DbType.Decimal, stepId);
                    db.AddInParameter(dbCommand, "ReviewedStatus", DbType.Byte, reviewedStatus);
                    db.AddInParameter(dbCommand, "TimeReviewed", DbType.DateTime, DateTime.Now);
                    if (transaction != null)
                    {
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        db.ExecuteNonQuery(dbCommand);
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
        /// 更新状态
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="instanceId"></param>
        /// <param name="reviewedStatus"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(decimal processId, decimal instanceId, byte reviewedStatus, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            string sqlUpdate = "UPDATE WorkflowInstanceStep SET ReviewedStatus = @ReviewedStatus, TimeReviewed = @TimeReviewed WHERE ProcessId = @ProcessId AND InstanceId = @InstanceId AND ReviewedStatus != @ReviewedStatus";

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "ReviewedStatus", DbType.Byte, reviewedStatus);
                    db.AddInParameter(dbCommand, "TimeReviewed", DbType.DateTime, DateTime.Now);
                    if (transaction != null)
                    {
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        db.ExecuteNonQuery(dbCommand);
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
        /// 更新状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="reviewedStatus"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void UpdateReviewedStatusByInstanceId(decimal instanceId, byte reviewedStatus, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            string sqlUpdate = "UPDATE WorkflowInstanceStep SET ReviewedStatus = @ReviewedStatus, TimeReviewed = @TimeReviewed WHERE InstanceId = @InstanceId AND ReviewedStatus != @ReviewedStatus";

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "ReviewedStatus", DbType.Byte, reviewedStatus);
                    db.AddInParameter(dbCommand, "TimeReviewed", DbType.DateTime, DateTime.Now);
                    if (transaction != null)
                    {
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        db.ExecuteNonQuery(dbCommand);
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
        /// 获得下一步的审核状态
        /// </summary>
        /// <param name="parentStepId"></param>
        /// <returns></returns>
        public Dictionary<decimal, ReviewedStatus> GetReviewedStatusOfNextSteps(decimal parentStepId)
        {
            Dictionary<decimal, ReviewedStatus> nextReviewedStatus = new Dictionary<decimal, ReviewedStatus>();

            try
            {
                decimal instanceId = 0;
                decimal processId = 0;
                string sqlSelect = "SELECT InstanceId, ProcessId FROM WorkflowInstanceStep WHERE StepId = @StepId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "StepId", DbType.Decimal, parentStepId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            instanceId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            processId = DataConvertionHelper.GetDecimal(dataReader[1]);
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
                if (instanceId > 0 && processId > 0)
                {
                    CustomWorkflowMap customWorkflowMap = new CustomWorkflowMap();
                    IList<decimal> childProcessIds = customWorkflowMap.GetSecondIds(processId);
                    if (childProcessIds.Count > 0)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.Append("SELECT StepId, ReviewedStatus FROM WorkflowInstanceStep WHERE InstanceId = @InstanceId ");
                        IList<WhereConditon> whereConditons = DataAccessHandler.GetWhereConditons(childProcessIds, "WorkflowInstanceStep", "ProcessId");
                        string whereSentence = DataAccessHandler.GetWhereSentence(whereConditons);
                        if (!string.IsNullOrWhiteSpace(whereSentence))
                        {
                            sb.AppendFormat("AND {0}", whereSentence);
                        }
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                        {
                            //给参数赋值
                            db.AddInParameter(dbCommand, "StepId", DbType.Decimal, parentStepId);
                            using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                            {
                                while (dataReader.Read())
                                {
                                    decimal stepId = DataConvertionHelper.GetDecimal(dataReader[0]);
                                    byte reviewedStatus = DataConvertionHelper.GetByte(dataReader[1]);
                                    nextReviewedStatus.Add(stepId, (ReviewedStatus)reviewedStatus);
                                }
                                if (dataReader != null)
                                {
                                    dataReader.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return nextReviewedStatus;
        } 

        /// <summary>
        /// 更新 WorkflowInstanceStepInfo 对象
        /// </summary>
        /// <param name="workflowInstanceStepInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
		public void Update(WorkflowInstanceStepInfo workflowInstanceStepInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE WorkflowInstanceStep SET InstanceId = @InstanceId, ProcessId = @ProcessId, UserId = @UserId, ");
            sb.Append("ReviewedStatus = @ReviewedStatus, TimeReviewed = @TimeReviewed ");
            sb.Append("WHERE StepId = @StepId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "StepId", DbType.Decimal, workflowInstanceStepInfo.StepId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, workflowInstanceStepInfo.UserId);
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, workflowInstanceStepInfo.InstanceId);
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, workflowInstanceStepInfo.ProcessId);
                    db.AddInParameter(dbCommand, "ReviewedStatus", DbType.Byte, workflowInstanceStepInfo.ReviewedStatus);
                    db.AddInParameter(dbCommand, "TimeReviewed", DbType.DateTime, DateTime.Now);
                    int count = 0;
                    if (transaction != null)
                    {
                        count = db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    else
                    {
                        count = db.ExecuteNonQuery(dbCommand);
                    }
                    //执行更新操作
                    if (count != 1)
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
        /// 获得步骤编号
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="processId"></param>
        /// <param name="userId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public decimal GetStepId(decimal instanceId, decimal processId, decimal userId, SqlDatabase db, DbTransaction transaction)
        {
            decimal stepId = decimal.MinValue;

            string sqlSelect = "SELECT StepId FROM WorkflowInstanceStep WHERE InstanceId = @InstanceId AND ProcessId = @ProcessId AND UserId = @UserId AND ReviewedStatus != @ReviewedStatus";
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "ReviewedStatus", DbType.Byte, (byte)ReviewedStatus.Completed); 
                    stepId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand, transaction));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return stepId;
        }

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 WorkflowInstanceStepInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>WorkflowInstanceStepInfo 对象列表</returns>
        private IList<WorkflowInstanceStepInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<WorkflowInstanceStepInfo>  workflowInstanceStepInfos = new List<WorkflowInstanceStepInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            
            sb.Append(" * FROM WorkflowInstanceStep");
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
                            decimal stepId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal instanceId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal processId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            byte reviewedStatus = DataConvertionHelper.GetByte(dataReader[4]);
                            DateTime timeReviewed = DataConvertionHelper.GetDateTime(dataReader[5]);
                            int processCounter = DataConvertionHelper.GetInt(dataReader[6]);
                            //将创建 WorkflowInstanceStepInfo 对象加入集合中
                            workflowInstanceStepInfos.Add(new WorkflowInstanceStepInfo(stepId, instanceId, processId, userId, reviewedStatus,
                            timeReviewed, processCounter));
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
            
			return workflowInstanceStepInfos;
		} 
        
        /// <summary>
		/// 获得 WorkflowInstanceStepInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>WorkflowInstanceStepInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM WorkflowInstanceStep");
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
        /// 获得表 WorkflowInstanceStep 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "WorkflowInstanceStep ", "StepId", "*", false, false, startPosition, 
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
        /// 获得以表 WorkflowInstanceStep 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "WorkflowInstanceStep ", "StepId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 WorkflowInstanceStep 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "WorkflowInstanceStep ", "StepId", "*", false, false, startPosition, 
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
        /// 获得以表 WorkflowInstanceStep 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "WorkflowInstanceStep ", "StepId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  WorkflowInstanceStepInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM WorkflowInstanceStep");
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
