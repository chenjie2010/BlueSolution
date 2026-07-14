//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowInstance.cs
// 描述：CustomWorkflowInstance 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.UserModule;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.SQLServerDAL.BusinessDesignerModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomWorkflowInstance 表的数据层访问类
    /// </summary>
    public class CustomWorkflowInstance : ICustomWorkflowInstance
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowInstance()
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomWorkflowInstance 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowInstanceInfo">customWorkflowInstanceInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomWorkflowInstanceInfo customWorkflowInstanceInfo)
        {
            //自动增加的关键字的值
            decimal customWorkflowInstanceId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customWorkflowInstanceId = Insert(customWorkflowInstanceInfo, false, db, null);

            return customWorkflowInstanceId;
        }

        /// <summary>
		/// 获得 CustomWorkflowInstanceInfo 对象
		/// </summary>
		///<param name="instanceId">工作流实例编号</param>
		/// <returns> CustomWorkflowInstanceInfo 对象</returns>
		public CustomWorkflowInstanceInfo GetModelInfo(decimal instanceId)
        {
            CustomWorkflowInstanceInfo customWorkflowInstanceInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("InstanceId", "InstanceId", System.Data.DbType.Decimal, instanceId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomWorkflowInstanceInfo> customWorkflowInstanceInfos = GetModelInfos(whereConditons, null, true);
            if (customWorkflowInstanceInfos != null && customWorkflowInstanceInfos.Count > 0)
            {
                customWorkflowInstanceInfo = customWorkflowInstanceInfos[0];
            }

            return customWorkflowInstanceInfo;
        }

        /// <summary>
        /// 更新 CustomWorkflowInstanceInfo 对象
        /// </summary>
        /// <param name="customWorkflowInstanceInfo">CustomWorkflowInstanceInfo 对象</param>
        public void Update(CustomWorkflowInstanceInfo customWorkflowInstanceInfo)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            Update(customWorkflowInstanceInfo, db, null);
        }

        /// <summary>
        ///  删除 CustomWorkflowInstanceInfo 对象
        /// </summary>
        ///<param name="instanceId">工作流实例编号</param>
        public void Delete(decimal instanceId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomWorkflowInstance ");
            sb.Append("WHERE InstanceId = @InstanceId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    WorkflowInstanceStep workflowInstanceStep = new WorkflowInstanceStep();
                    workflowInstanceStep.Delete(instanceId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
                        }
                        transaction.Commit();
                    }
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
        /// 获得 CustomWorkflowInstanceInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowInstanceInfo 对象列表</returns>
        public IList<CustomWorkflowInstanceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomWorkflowInstance 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomWorkflowInstanceInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomWorkflowInstance ", "InstanceId", false, whereConditons);
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
        /// 归档
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        public void ArchiveWorkflowInstance(decimal instanceId, bool isArchived, string archivedUserName, string archivedName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomWorkflowInstance SET IsArchived = @IsArchived, ArchivedUserName = @ArchivedUserName, ");
            sb.Append("ArchivedName = @ArchivedName, TimeArchived = @TimeArchived ");
            sb.Append("WHERE InstanceId = @InstanceId AND InstanceStatus = @InstanceStatus");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "IsArchived", DbType.Boolean, isArchived);
                    db.AddInParameter(dbCommand, "ArchivedUserName", DbType.String, archivedUserName);
                    db.AddInParameter(dbCommand, "ArchivedName", DbType.String, archivedName);
                    db.AddInParameter(dbCommand, "TimeArchived", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "InstanceStatus", DbType.Byte, (byte)InstanceStatus.Completed);
                    //执行更新操作
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 批量归档
        /// </summary>
        /// <param name="instanceIds"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        public void ArchiveWorkflowInstance(IList<decimal> instanceIds, bool isArchived, string archivedUserName, string archivedName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomWorkflowInstance SET IsArchived = @IsArchived, ArchivedUserName = @ArchivedUserName, ");
            sb.Append("ArchivedName = @ArchivedName, TimeArchived = @TimeArchived ");
            sb.Append("WHERE InstanceId = @InstanceId AND InstanceStatus = @InstanceStatus");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (var instanceId in instanceIds)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            //给参数赋值
                            db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                            db.AddInParameter(dbCommand, "IsArchived", DbType.Boolean, isArchived);
                            db.AddInParameter(dbCommand, "ArchivedUserName", DbType.String, archivedUserName);
                            db.AddInParameter(dbCommand, "ArchivedName", DbType.String, archivedName);
                            db.AddInParameter(dbCommand, "TimeArchived", DbType.DateTime, DateTime.Now);
                            db.AddInParameter(dbCommand, "InstanceStatus", DbType.Byte, (byte)InstanceStatus.Completed);
                            //执行更新操作
                            db.ExecuteNonQuery(dbCommand, transaction);
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
        }

        /// <summary>
        /// 按照条件归档
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="isArchived"></param>
        /// <param name="archivedUserName"></param>
        /// <param name="archivedName"></param>
        public void ArchiveWorkflowInstance(IList<WhereConditon> whereConditons, bool isArchived, string archivedUserName, string archivedName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomWorkflowInstance SET IsArchived = @IsArchived, ArchivedUserName = @ArchivedUserName, ");
            sb.Append("ArchivedName = @ArchivedName, TimeArchived = @TimeArchived WHERE ");
            if (whereConditons != null && whereConditons.Count > 0)
            {
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
                sb.Append(" AND ");
            }
            sb.Append("InstanceStatus = @InstanceStatus");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if (whereConditons != null && whereConditons.Count > 0)
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    db.AddInParameter(dbCommand, "ArchivedUserName", DbType.String, archivedUserName);
                    db.AddInParameter(dbCommand, "ArchivedName", DbType.String, archivedName);
                    db.AddInParameter(dbCommand, "TimeArchived", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "InstanceStatus", DbType.Byte, (byte)InstanceStatus.Completed);
                    //执行更新操作
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 初始化工作流实例
        /// </summary>
        /// <param name="instanceId"></param>
        public void InitWorkWorkflowInstance(decimal instanceId)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            WorkflowInstanceLog workflowInstanceLog = new WorkflowInstanceLog();
            WorkflowInstanceStep workflowInstanceStep = new WorkflowInstanceStep();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    UpdateInstanceStatus(instanceId, InstanceStatus.None, db, transaction);
                    workflowInstanceStep.Delete(instanceId, db, transaction);
                    workflowInstanceLog.Delete(instanceId, db, transaction);
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
        /// 获得表 WorkflowInstance 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetWorkflowInstances(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserAccount", "UserId", TableJoin.InnerJoin));
                ds = DataAccessHandler.GetPageRecord(db, "CustomWorkflowInstance", "InstanceId", "InstanceId, InstanceName, InstanceStatus, UserName, UserActualName, TimeSumbitted, IsArchived", false, false, tableLinks, startPosition,
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
        /// 更新工作流实例的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="instanceStatus"></param>
        public void UpdateInstanceStatus(decimal instanceId, InstanceStatus instanceStatus)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            UpdateInstanceStatus(instanceId, instanceStatus, db, null);
        }

        /// <summary>
        /// 终止工作流
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public AbortedResult AbortWorkflowInstance(decimal instanceId)
        {
            AbortedResult abortedResult = AbortedResult.Success;

            InstanceStatus instanceStatus = GetInstanceStatus(instanceId);
            switch (instanceStatus)
            {
                case InstanceStatus.ReviewAborted:
                    abortedResult = AbortedResult.Fail_Aborted;
                    break;

                case InstanceStatus.Completed:
                    abortedResult = AbortedResult.Fail_Completed;
                    break;

                case InstanceStatus.None:
                    abortedResult = AbortedResult.Fail_Unknown;
                    break;
            }

            if (abortedResult == AbortedResult.Success)
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                WorkflowInstanceLog workflowInstanceLog = new WorkflowInstanceLog();
                WorkflowInstanceStep workflowInstanceStep = new WorkflowInstanceStep();
                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        UpdateInstanceStatus(instanceId, InstanceStatus.ReviewAborted, db, transaction);
                        workflowInstanceStep.Update(instanceId, (byte)ReviewedStatus.Completed, db, transaction);
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

            return abortedResult;
        }

        /// <summary>
        /// 终止工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="comment"></param>
        /// <returns></returns>
        public AbortedResult AbortWorkflowInstance(decimal stepId, string comment)
        {
            AbortedResult abortedResult = AbortedResult.Success;

            WorkflowInstanceStep workflowInstanceStep = new WorkflowInstanceStep();
            WorkflowInstanceStepInfo workflowInstanceStepInfo = workflowInstanceStep.GetModelInfo(stepId);
            InstanceStatus instanceStatus = GetInstanceStatus(workflowInstanceStepInfo.InstanceId);
            switch (instanceStatus)
            {
                case InstanceStatus.ReviewAborted:
                    abortedResult = AbortedResult.Fail_Aborted;
                    break;

                case InstanceStatus.Completed:
                    abortedResult = AbortedResult.Fail_Completed;
                    break;

                case InstanceStatus.None:
                    abortedResult = AbortedResult.Fail_Unknown;
                    break;
            }

            if (abortedResult == AbortedResult.Success)
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                WorkflowInstanceLog workflowInstanceLog = new WorkflowInstanceLog();
                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {

                        UpdateInstanceStatus(workflowInstanceStepInfo.InstanceId, InstanceStatus.ReviewAborted, db, transaction);
                        workflowInstanceStep.Update(workflowInstanceStepInfo.InstanceId, (byte)ReviewedStatus.Completed, db, transaction);
                        WorkflowInstanceLogInfo workflowInstanceLogInfo = new WorkflowInstanceLogInfo()
                        {
                            ProcessId = workflowInstanceStepInfo.ProcessId,
                            InstanceId = workflowInstanceStepInfo.InstanceId,
                            UserId = workflowInstanceStepInfo.UserId,
                            ReviewedAction = (byte)ReviewedAction.Abort,
                            Comment = comment,
                            TimeReviewed = DateTime.Now
                        };
                        workflowInstanceLog.Insert(workflowInstanceLogInfo, db, transaction);
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

            return abortedResult;
        }

        /// <summary>
        /// 是否允许发起人撤回工作流
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public WithdrawedResult IsUserWorkflowInstanceWithDrawed(decimal instanceId)
        {
            WithdrawedResult withdrawedResult = WithdrawedResult.Success;

            WorkflowInstanceStep workflowInstanceStep = new WorkflowInstanceStep();
            Dictionary<decimal, ReviewedStatus> reviewedStatus = workflowInstanceStep.GetReviewedStatus(instanceId);
            foreach (var status in reviewedStatus)
            {
                if (status.Value == ReviewedStatus.Completed)
                {
                    withdrawedResult = WithdrawedResult.Fail_Passed;
                    break;
                }
            }

            return withdrawedResult;
        }
        
        /// <summary>
        /// 是否允许工作流撤回
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public WithdrawedResult IsWorkflowInstanceWithDrawed(decimal stepId)
        {
            WithdrawedResult withdrawedResult = WithdrawedResult.Success;

            WorkflowInstanceStep workflowInstanceStep = new WorkflowInstanceStep();
            WorkflowInstanceStepInfo workflowInstanceStepInfo = workflowInstanceStep.GetModelInfo(stepId);
            bool isArchived = GetInstanceArchivedStatus(workflowInstanceStepInfo.InstanceId);
            if (isArchived)
            {
                withdrawedResult = WithdrawedResult.Fail_Archived;
            }
            Dictionary<decimal, ReviewedStatus> reviewedStatus = workflowInstanceStep.GetReviewedStatusOfNextSteps(stepId);
            if (withdrawedResult == WithdrawedResult.Success)
            {
                foreach (KeyValuePair<decimal, ReviewedStatus> status in reviewedStatus)
                {
                    if (status.Value == ReviewedStatus.Completed)
                    {
                        withdrawedResult = WithdrawedResult.Fail_Passed;
                        break;
                    }
                }
            }

            return withdrawedResult;
        }

        /// <summary>
        /// 发起人撤回工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        public WithdrawedResult UserWithdrawWorkflowInstance(decimal instanceId, WorkflowInstanceLogInfo workflowInstanceLogInfo)
        {
            WithdrawedResult withdrawedResult = WithdrawedResult.Success;

            withdrawedResult = IsUserWorkflowInstanceWithDrawed(instanceId);
            if (withdrawedResult == WithdrawedResult.Success)
            {
                WorkflowInstanceLogAndStep workflowInstanceLogAndStep = new WorkflowInstanceLogAndStep();
                WorkflowInstanceStep workflowInstanceStep = new WorkflowInstanceStep();
                CustomWorkflowProcess customWorkflowProcess = new CustomWorkflowProcess();
                CustomWorkflowInstanceInfo customWorkflowInstanceInfo = GetModelInfo(instanceId);
                CommonNode commonNode = customWorkflowProcess.GetWorkflowRootNode(customWorkflowInstanceInfo.WorkflowId);
                WorkflowInstanceLog workflowInstanceLog = new WorkflowInstanceLog();
                IList<decimal> logIds = workflowInstanceLog.GetUserLogIds(commonNode.NodeId, instanceId, customWorkflowInstanceInfo.UserId);
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase(); using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        Dictionary<decimal, ReviewedStatus> reviewedStatus = workflowInstanceStep.GetReviewedStatus(instanceId);
                        foreach (KeyValuePair<decimal, ReviewedStatus> status in reviewedStatus)
                        {
                            workflowInstanceStep.Update(status.Key, (byte)ReviewedStatus.Pending, db, transaction);
                            workflowInstanceLogAndStep.Delete(status.Key, logIds, db, transaction);
                        }
                        UpdateTimeModified(instanceId, db, transaction);
                        UpdateInstanceStatus(instanceId, InstanceStatus.None, db, transaction);
                        workflowInstanceLog.Insert(workflowInstanceLogInfo, db, transaction);
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

            return withdrawedResult;
        }

        /// <summary>
        /// 撤回工作流实例
        /// </summary>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        public WithdrawedResult WithdrawWorkflowInstance(decimal stepId, WorkflowInstanceLogInfo workflowInstanceLogInfo)
        {
            WithdrawedResult withdrawedResult = WithdrawedResult.Success;

            WorkflowInstanceStep workflowInstanceStep = new WorkflowInstanceStep();
            WorkflowInstanceStepInfo workflowInstanceStepInfo = workflowInstanceStep.GetModelInfo(stepId);
            withdrawedResult = IsWorkflowInstanceWithDrawed(stepId);
            if (withdrawedResult == WithdrawedResult.Success)
            {
                WorkflowInstanceLogAndStep workflowInstanceLogAndStep = new WorkflowInstanceLogAndStep();
                WorkflowInstanceLog workflowInstanceLog = new WorkflowInstanceLog();
                IList<decimal> logIds = workflowInstanceLog.GetUserLogIds(workflowInstanceStepInfo.ProcessId, workflowInstanceStepInfo.InstanceId, workflowInstanceStepInfo.UserId);
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase(); using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        Dictionary<decimal, ReviewedStatus> reviewedStatus = workflowInstanceStep.GetReviewedStatusOfNextSteps(stepId);
                        foreach (KeyValuePair<decimal, ReviewedStatus> status in reviewedStatus)
                        {
                            workflowInstanceStep.Update(status.Key, (byte)ReviewedStatus.Pending, db, transaction);
                            workflowInstanceLogAndStep.Delete(status.Key, logIds, db, transaction);
                        }
                        workflowInstanceStep.Update(stepId, (byte)ReviewedStatus.Reviewing, db, transaction);
                        UpdateTimeModified(workflowInstanceStepInfo.InstanceId, db, transaction);
                        UpdateInstanceStatus(workflowInstanceStepInfo.InstanceId, InstanceStatus.Review, db, transaction);
                        workflowInstanceLog.Insert(workflowInstanceLogInfo, db, transaction);
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

            return withdrawedResult;
        }

        /// <summary>
        /// 处理工作流提交的数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public InstanceItem Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, WorkflowInstanceLogInfo workflowInstanceLogInfo, IList<RecordEntity> recordEntities)
        {
            InstanceItem instanceItem = new InstanceItem(); ;

            try
            {
                UserAccount userAccount = new UserAccount();
                CommonUserInfo commonUserInfo = userAccount.GetCommonUserInfo(customWorkflowInstanceInfo.UserId);
                if (commonUserInfo == null)
                {
                    throw new ArgumentException("用户不存在。");
                }
                instanceItem.InstanceId = Process(customWorkflowInstanceInfo, workflowInstanceLogInfo);
                DataAuditing dataAuditing = new DataAuditing();
                instanceItem.RecordItems = dataAuditing.Process(commonUserInfo, recordEntities);
                if (customWorkflowInstanceInfo.InstanceId <= 0)
                {
                    foreach (var recordEntity in recordEntities)
                    {
                        recordEntity.InstanceId = instanceItem.InstanceId;
                    }
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceItem;
        }

        /// <summary>
        /// 处理工作流提交的数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <param name="dicRecordEntities"></param>
        /// <returns></returns>
        public InstanceSet Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, WorkflowInstanceLogInfo workflowInstanceLogInfo, Dictionary<decimal, List<RecordEntity>> dicRecordEntities)
        {
            InstanceSet instanceSet = new InstanceSet();

            try
            {
                UserAccount userAccount = new UserAccount();
                CommonUserInfo commonUserInfo = userAccount.GetCommonUserInfo(customWorkflowInstanceInfo.UserId);
                if (commonUserInfo == null)
                {
                    throw new ArgumentException("用户不存在。");
                }
                instanceSet.InstanceId = Process(customWorkflowInstanceInfo, workflowInstanceLogInfo);
                DataAuditing dataAuditing = new DataAuditing();
                instanceSet.RecordItems = dataAuditing.Process(commonUserInfo, dicRecordEntities);
                if (customWorkflowInstanceInfo.InstanceId <= 0)
                {
                    foreach (var keyValue in dicRecordEntities)
                    {
                        foreach (var recordEntity in keyValue.Value)
                        {
                            recordEntity.InstanceId = customWorkflowInstanceInfo.InstanceId;
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            return instanceSet;
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="stepId"></param>
        /// <param name="workflowInstanceLogInfos"></param>
        /// <param name="workflowInstanceStepInfos"></param>
        /// <param name="recordEntities"></param>
        /// <returns></returns>
        public InstanceSet Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, decimal stepId, IList<WorkflowInstanceLogInfo> workflowInstanceLogInfos, 
            IList<WorkflowInstanceStepInfo> workflowInstanceStepInfos, Dictionary<decimal, List<RecordEntity>> dicRecordEntities)
        {
            InstanceSet instanceItem = new InstanceSet();

            UserAccount userAccount = new UserAccount();
            CommonUserInfo commonUserInfo = userAccount.GetCommonUserInfo(customWorkflowInstanceInfo.UserId);
            if (commonUserInfo == null)
            {
                throw new ArgumentException("用户不存在。");
            }
                        
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            WorkflowInstanceStep workflowInstanceStep = new WorkflowInstanceStep();
            WorkflowInstanceLog workflowInstanceLog = new WorkflowInstanceLog();
            WorkflowInstanceLogAndStep workflowInstanceLogAndStep = new WorkflowInstanceLogAndStep();
            CustomWorkflowMap customWorkflowMap = new CustomWorkflowMap();
            DataAuditing dataAuditing = new DataAuditing();
            int processCounter = 0;
            if (stepId > 0)
            {
                processCounter = workflowInstanceStep.GetProcessCounter(stepId);
            }
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (customWorkflowInstanceInfo.InstanceId > 0)
                    {
                        if (stepId <= 0)
                        {
                            UpdateInstanceStatusWithTime(customWorkflowInstanceInfo.InstanceId, (InstanceStatus)customWorkflowInstanceInfo.InstanceStatus, db, transaction);
                        }
                        else
                        {
                            UpdateInstanceStatus(customWorkflowInstanceInfo.InstanceId, (InstanceStatus)customWorkflowInstanceInfo.InstanceStatus, db, transaction);
                        }
                    }
                    else
                    {                        
                        customWorkflowInstanceInfo.InstanceId = Insert(customWorkflowInstanceInfo, true, db, transaction);
                        foreach (var keyValue in dicRecordEntities)
                        {
                            foreach (var recordEntity in keyValue.Value)
                            {
                                recordEntity.InstanceId = customWorkflowInstanceInfo.InstanceId;
                            }
                        }
                    }
                    if (stepId > 0)
                    {
                        workflowInstanceStep.Update(stepId, (byte)ReviewedStatus.Completed, db, transaction);
                    }
                    if (workflowInstanceLogInfos.Count > 0)
                    {
                        IList<decimal> logIds = new List<decimal>();
                        foreach (var workflowInstanceLogInfo in workflowInstanceLogInfos)
                        {
                            decimal logId = workflowInstanceLog.Insert(workflowInstanceLogInfo, db, transaction);
                            logIds.Add(logId);
                        }
                        if (workflowInstanceLogInfos.Count == workflowInstanceStepInfos.Count)
                        {
                            int index = 0;
                            foreach (var workflowInstanceStepInfo in workflowInstanceStepInfos)
                            {
                                workflowInstanceStepInfo.StepId = workflowInstanceStep.GetStepId(workflowInstanceStepInfo.InstanceId, workflowInstanceStepInfo.ProcessId, workflowInstanceStepInfo.UserId, db, transaction);
                                int totalNodeCount = processCounter > 0 ? processCounter : customWorkflowMap.GetTotalCountByProcessId(workflowInstanceStepInfo.ProcessId);
                                if (workflowInstanceStepInfo.StepId > 0)
                                {
                                    int currentNodeCount = workflowInstanceLogAndStep.GetTotalCountByFirstForeignKey(workflowInstanceStepInfo.StepId);
                                    if (currentNodeCount == totalNodeCount - 1)
                                    {
                                        workflowInstanceStep.Update(workflowInstanceStepInfo.StepId, (byte)ReviewedStatus.Reviewing, db, transaction);
                                    }
                                }
                                else
                                {
                                    if (totalNodeCount <= 1)
                                    {
                                        workflowInstanceStepInfo.ReviewedStatus = (byte)ReviewedStatus.Reviewing;
                                    }
                                    else
                                    {
                                        workflowInstanceStepInfo.ReviewedStatus = (byte)ReviewedStatus.Pending;
                                    }
                                    workflowInstanceStepInfo.StepId = workflowInstanceStep.Insert(workflowInstanceStepInfo, db, transaction);
                                }
                                workflowInstanceLogAndStep.Insert(new WorkflowInstanceLogAndStepInfo(workflowInstanceStepInfo.StepId, logIds[index++]), db, transaction);
                            }
                        }
                    }
                    instanceItem.InstanceId = customWorkflowInstanceInfo.InstanceId;
                    instanceItem.RecordItems = dataAuditing.Process(commonUserInfo, dicRecordEntities);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return instanceItem;
        }
              
        
        /// <summary>
        /// 获得实例个数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="workflowId"></param>
        /// <param name="instanceStatus"></param>
        /// <returns></returns>
        public int GetWorkflowInstanceCount(decimal userId, decimal workflowId, InstanceStatus instanceStatus)
        {
            int count = 0;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("WorkflowId", "WorkflowId", DbType.Decimal, workflowId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            switch (instanceStatus)
            {
                case InstanceStatus.None:
                    whereConditons.Add(new WhereConditon("InstanceStatus", "InstanceStatus", DbType.Byte, InstanceStatus.None, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                    break;

                case InstanceStatus.Review:
                    whereConditons.Add(new WhereConditon("InstanceStatus", "InstanceStatus", DbType.Byte, InstanceStatus.Review, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                    break;

                case InstanceStatus.ReviewAborted:
                    whereConditons.Add(new WhereConditon("InstanceStatus", "InstanceStatus", DbType.Byte, InstanceStatus.ReviewAborted, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                    break;

                case InstanceStatus.Completed:
                    whereConditons.Add(new WhereConditon("InstanceStatus", "InstanceStatus", DbType.Byte, InstanceStatus.Completed, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                    break;
            }

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(InstanceId) FROM CustomWorkflowInstance WHERE ");
            sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                //给参数赋值
                if ((whereConditons != null) && (whereConditons.Count > 0))
                {
                    DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                }
                count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 0);
            }

            return count;
        }               

        /// <summary>
        /// 获得表 CustomWorkflowInstance 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string dataFiledNames = "InstanceId, WorkflowId, UserId, InstanceName, InstanceStatus, TimeModified, TimeSumbitted";
                ds = DataAccessHandler.GetPageRecord(db, "CustomWorkflowInstance ", "InstanceId", dataFiledNames, false, false, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
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
        /// 获得工作流实例
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public IList<CustomWorkflowInstanceInfo> GetModelInfos(decimal userId, decimal workflowId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("IsArchived", "IsArchived", DbType.Boolean, false, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("WorkflowId", "WorkflowId", DbType.Decimal, workflowId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("TimeModified", CustomSorting.Descending));

            return GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得工作流归档的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public bool GetInstanceArchivedStatus(decimal instanceId)
        {
            bool isArchived = false;

            string sqlSelect = "SELECT IsArchived FROM CustomWorkflowInstance WHERE InstanceId = @InstanceId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    isArchived = DataConvertionHelper.GetBoolean(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isArchived;
        }

        /// <summary>
        /// 获得工作流实例的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public InstanceStatus GetInstanceStatus(decimal instanceId)
        {
            InstanceStatus instanceStatus = InstanceStatus.None;

            string sqlSelect = "SELECT InstanceStatus FROM CustomWorkflowInstance WHERE InstanceId = @InstanceId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    instanceStatus = (InstanceStatus)DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand), 0);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return instanceStatus;
        }

        #endregion

        #endregion

        #region 公有方法        

        #endregion

        #region 私有方法


        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="workflowInstanceLogInfo"></param>
        /// <returns></returns>
        private decimal Process(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, WorkflowInstanceLogInfo workflowInstanceLogInfo)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            DataAuditing dataAuditing = new DataAuditing();
            WorkflowInstanceLog workflowInstanceLog = new WorkflowInstanceLog();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (workflowInstanceLogInfo != null)
                    {
                        workflowInstanceLog.InsertOrUpdate(workflowInstanceLogInfo, db, transaction);
                    }
                    if (customWorkflowInstanceInfo.InstanceId > 0)
                    {
                        Update(customWorkflowInstanceInfo, db, transaction);
                    }
                    else
                    {
                        customWorkflowInstanceInfo.InstanceId = Insert(customWorkflowInstanceInfo, false, db, transaction);
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

            return customWorkflowInstanceInfo.InstanceId;
        }

        /// <summary>
        /// 更新工作流实例的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="instanceStatus"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void UpdateInstanceStatusWithTime(decimal instanceId, InstanceStatus instanceStatus, SqlDatabase db, DbTransaction transaction)
        {
            string sqlUpdate = "UPDATE CustomWorkflowInstance SET InstanceStatus = @InstanceStatus, TimeModified = @TimeModified, TimeSumbitted = @TimeSumbitted WHERE InstanceId = @InstanceId";

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "InstanceStatus", DbType.Byte, (byte)instanceStatus);
                    db.AddInParameter(dbCommand, "TimeModified", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "TimeSumbitted", DbType.DateTime, DateTime.Now);                    
                    //执行更新操作
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
                        throw new Exception("更新失败！");
                    }
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 更新工作流实例的状态
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="instanceStatus"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void UpdateInstanceStatus(decimal instanceId, InstanceStatus instanceStatus, SqlDatabase db, DbTransaction transaction)
        {
            string sqlUpdate = "UPDATE CustomWorkflowInstance SET InstanceStatus = @InstanceStatus, TimeModified = @TimeModified WHERE InstanceId = @InstanceId";

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "InstanceStatus", DbType.Byte, (byte)instanceStatus);
                    db.AddInParameter(dbCommand, "TimeModified", DbType.DateTime, DateTime.Now);
                    //执行更新操作
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
                        throw new Exception("更新失败！");
                    }
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 向 CustomWorkflowInstance 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowInstanceInfo"></param>
        /// <param name="sumbitted"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private decimal Insert(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, bool sumbitted, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal customWorkflowInstanceId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomWorkflowInstance(UserId, WorkflowId, ParentUserId, InstanceName, InstanceStatus, TimeCreated, ");
            if (sumbitted)
            {
                sb.Append("TimeSumbitted, ");
            }
            sb.Append("IsArchived, PageSign) VALUES (@UserId, @WorkflowId, @ParentUserId, @InstanceName, @InstanceStatus, @TimeCreated, ");           
            if (sumbitted)
            {
                sb.Append("@TimeSumbitted, ");
            }
            sb.Append("@IsArchived, @PageSign);");
            sb.Append("SET @InstanceId = SCOPE_IDENTITY()");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "InstanceId", DbType.Decimal, 6);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, customWorkflowInstanceInfo.UserId);
                    db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, customWorkflowInstanceInfo.WorkflowId);
                    db.AddInParameter(dbCommand, "ParentUserId", DbType.Decimal, customWorkflowInstanceInfo.ParentUserId);
                    db.AddInParameter(dbCommand, "InstanceName", DbType.String, customWorkflowInstanceInfo.InstanceName);
                    db.AddInParameter(dbCommand, "InstanceStatus", DbType.Byte, customWorkflowInstanceInfo.InstanceStatus);
                    db.AddInParameter(dbCommand, "TimeCreated", DbType.DateTime, DateTime.Now);
                    if (sumbitted)
                    {
                        db.AddInParameter(dbCommand, "TimeSumbitted", DbType.DateTime, DateTime.Now);
                    }
                    db.AddInParameter(dbCommand, "IsArchived", DbType.Boolean, false);
                    db.AddInParameter(dbCommand, "PageSign", DbType.Int64, customWorkflowInstanceInfo.PageSign);

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
                    customWorkflowInstanceId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@InstanceId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowInstanceId;
        }


        /// <summary>
        /// 更新 CustomWorkflowInstanceInfo 对象
        /// </summary>
        /// <param name="customWorkflowInstanceInfo">CustomWorkflowInstanceInfo 对象</param>
        private void Update(CustomWorkflowInstanceInfo customWorkflowInstanceInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomWorkflowInstance SET UserId = @UserId, WorkflowId = @WorkflowId, ParentUserId = @ParentUserId, ");
            sb.Append("InstanceName = @InstanceName, InstanceStatus = @InstanceStatus, TimeModified = @TimeModified, TimeSumbitted = @TimeSumbitted ");
            if (customWorkflowInstanceInfo.PageSign > 0)
            {
                sb.Append(", PageSign = PageSign | @PageSign ");
            }
            sb.Append("WHERE InstanceId = @InstanceId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, customWorkflowInstanceInfo.InstanceId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, customWorkflowInstanceInfo.UserId);
                    db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, customWorkflowInstanceInfo.WorkflowId);
                    db.AddInParameter(dbCommand, "ParentUserId", DbType.Decimal, customWorkflowInstanceInfo.ParentUserId);
                    db.AddInParameter(dbCommand, "InstanceName", DbType.String, customWorkflowInstanceInfo.InstanceName);
                    db.AddInParameter(dbCommand, "InstanceStatus", DbType.Byte, customWorkflowInstanceInfo.InstanceStatus);
                    db.AddInParameter(dbCommand, "TimeModified", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "TimeSumbitted", DbType.DateTime, DateTime.Now);
                    if (customWorkflowInstanceInfo.PageSign > 0)
                    {
                        db.AddInParameter(dbCommand, "PageSign", DbType.Int64, customWorkflowInstanceInfo.PageSign);
                    }
                    //执行更新操作
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
       
        #region 默认私有方法

        /// <summary>
        /// 获得 CustomWorkflowInstanceInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomWorkflowInstanceInfo 对象列表</returns>
        private IList<CustomWorkflowInstanceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomWorkflowInstanceInfo> customWorkflowInstanceInfos = new List<CustomWorkflowInstanceInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomWorkflowInstance");
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
                            decimal instanceId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal workflowId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal parentUserId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            string instanceName = DataConvertionHelper.GetString(dataReader[4]);
                            byte instanceStatus = DataConvertionHelper.GetByte(dataReader[5]);
                            DateTime timeCreated = DataConvertionHelper.GetDateTime(dataReader[6]);
                            DateTime timeModified = DataConvertionHelper.GetDateTime(dataReader[7]);
                            DateTime timeSumbitted = DataConvertionHelper.GetDateTime(dataReader[8]);
                            bool isArchived = DataConvertionHelper.GetBoolean(dataReader[9]);
                            string archivedUserName = DataConvertionHelper.GetString(dataReader[10]);
                            string archivedName = DataConvertionHelper.GetString(dataReader[11]);
                            DateTime timeArchived = DataConvertionHelper.GetDateTime(dataReader[12]);
                            long pageSign = DataConvertionHelper.GetLong(dataReader[13]);
                            //将创建 CustomWorkflowInstanceInfo 对象加入集合中
                            customWorkflowInstanceInfos.Add(new CustomWorkflowInstanceInfo(instanceId, userId, workflowId, parentUserId, instanceName,
                            instanceStatus, timeCreated, timeModified, timeSumbitted, isArchived,
                            archivedUserName, archivedName, timeArchived, pageSign));
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

            return customWorkflowInstanceInfos;
        }

        /// <summary>
        /// 获得 CustomWorkflowInstanceInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowInstanceInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomWorkflowInstance");
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
        /// 获得表 CustomWorkflowInstance 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomWorkflowInstance ", "InstanceId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomWorkflowInstance 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomWorkflowInstance ", "InstanceId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得以表 CustomWorkflowInstance 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomWorkflowInstance ", "InstanceId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomWorkflowInstanceInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomWorkflowInstance");
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
        /// 更新实例的修改时间对象
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void UpdateTimeModified(decimal instanceId, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            string sqlUpdate = "UPDATE CustomWorkflowInstance SET TimeModified = @TimeModified WHERE InstanceId = @InstanceId";
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "TimeModified", DbType.DateTime, DateTime.Now);
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
