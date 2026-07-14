//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowProcess.cs
// 描述：CustomWorkflowProcess 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/12/1
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
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.GeneralAffairModule;
using Blue.SQLServerDAL.UserModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomWorkflowProcess 表的数据层访问类
    /// </summary>
    public class CustomWorkflowProcess : CommonNodeDataAccess, ICustomWorkflowProcess
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflowProcess() : base("CustomWorkflowProcess", "ProcessId", "WorkflowId", "ProcessName", "ProcessCode", false, true, "ProcessType")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomWorkflowProcess 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowProcessInfo">customWorkflowProcessInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomWorkflowProcessInfo customWorkflowProcessInfo)
        {
            //自动增加的关键字的值
            decimal customWorkflowProcessId = 0;

            try
            {
                customWorkflowProcessId = Insert(customWorkflowProcessInfo, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowProcessId;
        }

        /// <summary>
		/// 获得 CustomWorkflowProcessInfo 对象
		/// </summary>
		///<param name="processId">流程编号</param>
		/// <returns> CustomWorkflowProcessInfo 对象</returns>
		public CustomWorkflowProcessInfo GetModelInfo(decimal processId)
        {
            CustomWorkflowProcessInfo customWorkflowProcessInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ProcessId", "ProcessId", DbType.Decimal, processId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomWorkflowProcessInfo> customWorkflowProcessInfos = GetModelInfos(whereConditons, null, true);
            if (customWorkflowProcessInfos != null && customWorkflowProcessInfos.Count > 0)
            {
                customWorkflowProcessInfo = customWorkflowProcessInfos[0];
            }

            return customWorkflowProcessInfo;
        }

        /// <summary>
        /// 更新 CustomWorkflowProcessInfo 对象
        /// </summary>
        /// <param name="customWorkflowProcessInfo">CustomWorkflowProcessInfo 对象</param>
        public void Update(CustomWorkflowProcessInfo customWorkflowProcessInfo)
        {            
            try
            {
                Update(customWorkflowProcessInfo, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        ///  删除 CustomWorkflowProcessInfo 对象
        /// </summary>
        ///<param name="processId">流程编号</param>
        public void Delete(decimal processId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomWorkflowProcess ");
            sb.Append("WHERE ProcessId = @ProcessId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
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
        /// 获得 CustomWorkflowProcessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowProcessInfo 对象列表</returns>
        public IList<CustomWorkflowProcessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomWorkflowProcess 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomWorkflowProcessInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomWorkflowProcess ", "ProcessId", false, whereConditons);
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
        /// 获得条件范围
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public ConditionType GetConditionType(decimal processId)
        {
            ConditionType conditionType = ConditionType.Table;

            try
            {
                string sqlSelect = "SELECT ConditionType FROM CustomWorkflowProcess WHERE ProcessId = @ProcessId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                    conditionType = (ConditionType)DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return conditionType;
        }

        /// <summary>
        /// 获得工作流程的节点分类
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public byte GetProcessCategory(decimal stepId)
        {
            byte processCategory = 0;

            try
            {
                string sqlSelect = "SELECT ProcessCategory FROM CustomWorkflowProcess INNER JOIN WorkflowInstanceStep ON CustomWorkflowProcess.ProcessId = WorkflowInstanceStep.ProcessId WHERE WorkflowInstanceStep.StepId = @StepId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "StepId", DbType.Decimal, DataConvertionHelper.SetDecimal(stepId));
                    processCategory = DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return processCategory;
        }


        /// <summary>
        /// 获得工作流程的节点设置属性
        /// </summary>
        /// <param name="stepId"></param>
        /// <returns></returns>
        public long GetProcessSetting(decimal stepId)
        {
            long processSetting = 0;

            try
            {
                string sqlSelect = "SELECT ProcessSetting FROM CustomWorkflowProcess INNER JOIN WorkflowInstanceStep ON CustomWorkflowProcess.ProcessId = WorkflowInstanceStep.ProcessId WHERE WorkflowInstanceStep.StepId = @StepId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "StepId", DbType.Decimal, DataConvertionHelper.SetDecimal(stepId));
                    processSetting = DataConvertionHelper.GetLong(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return processSetting;
        }

        /// <summary>
        /// 获得工作流节点
        /// </summary>
        /// <param name="workflowId"></param>
        /// <param name="processCategory"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal workflowId, byte processCategory)
        {
            IList<CommonNode> commonNodes = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("WorkflowId", "WorkflowId", DbType.Decimal, workflowId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("ProcessCategory", "ProcessCategory", DbType.Byte, processCategory, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            commonNodes = GetCommonNodesByWhereConditon(whereConditons);

            return commonNodes;
        }

        /// <summary>
        /// 通过角色查看工作流参与的数量
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public int GetWorkflowProcessCountByRoleId(decimal roleId)
        {
            int count = 0;

            try
            {
                string sqlSelect = "SELECT COUNT(1) FROM CustomWorkflowProcess WHERE RoleId = @RoleId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(roleId));
                    count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
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
        /// 获得工作流节点编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public decimal GetWorkflowId(decimal processId)
        {
            decimal workflowId = decimal.MinValue;

            try
            {
                string sqlSelect = "SELECT WorkflowId FROM CustomWorkflowProcess WHERE ProcessId = @ProcessId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, DataConvertionHelper.SetDecimal(processId));
                    workflowId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return workflowId;
        }
        
        /// <summary>
        /// 更新根节点
        /// </summary>
        /// <param name="processId"></param>
        public void UpdateWorkflowRootNode(decimal processId)
        {
            decimal workflowId = GetWorkflowId(processId);

            string sqlUpdate1 = "UPDATE CustomWorkflowProcess SET IsRootNode = @IsRootNode WHERE WorkflowId = @WorkflowId AND ProcessId != @ProcessId";
            string sqlUpdate2 = "UPDATE CustomWorkflowProcess SET IsRootNode = @IsRootNode WHERE ProcessId = @ProcessId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate1))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, DataConvertionHelper.SetDecimal(workflowId));
                        db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, DataConvertionHelper.SetDecimal(processId));
                        db.AddInParameter(dbCommand, "IsRootNode", DbType.Boolean, false);
                        //执行插入操作
                        db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate2))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, DataConvertionHelper.SetDecimal(processId));
                        db.AddInParameter(dbCommand, "IsRootNode", DbType.Boolean, true);
                        //执行插入操作
                        db.ExecuteNonQuery(dbCommand, transaction);
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
        /// 获得工作流根节点
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public CommonNode GetWorkflowRootNode(decimal workflowId)
        {
            CommonNode commonNode = null;

            try
            {
                string sqlSelect = "SELECT TOP 1 ProcessId, ProcessName, ProcessCode FROM CustomWorkflowProcess WHERE WorkflowId = @WorkflowId AND ProcessCategory = @ProcessCategory AND IsRootNode = @IsRootNode ORDER BY Sorting";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, DataConvertionHelper.SetDecimal(workflowId));
                    db.AddInParameter(dbCommand, "ProcessCategory", DbType.Byte, (byte)WorkflowProcessCategory.Begin);                    
                    db.AddInParameter(dbCommand, "IsRootNode", DbType.Boolean, true);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal processId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string processName = DataConvertionHelper.GetString(dataReader[1]);
                            string processCode = DataConvertionHelper.GetString(dataReader[2]);
                            commonNode = new CommonNode(processId, workflowId, processName, processCode, true);
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

            return commonNode;
        }

        /// <summary>
        /// 获得单位间动态节点的审核用户
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="depIds"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetNextReviewers(decimal processId, IList<decimal> depIds)
        {
            Dictionary<decimal, decimal> nextReviewers = new Dictionary<decimal, decimal>();
                        
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT RoleAndUser.UserId FROM RoleAndUser ");
            sb.Append("INNER JOIN CustomWorkflowProcess ON CustomWorkflowProcess.RoleId = RoleAndUser.RoleId ");
            sb.Append("INNER JOIN  UserAccount ON UserAccount.UserId = RoleAndUser.UserId ");
            sb.Append("WHERE ProcessId = @ProcessId AND UserAccount.DepId = @DepId ");
            CustomWorkflowProcessInfo customWorkflowProcessInfo = GetModelInfo(processId);
            WorkflowHandlerMode workflowHandlerMode = (WorkflowHandlerMode)customWorkflowProcessInfo.HandlerMode;
            WorkflowInstanceStep workflowInstanceStep = new WorkflowInstanceStep();

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            foreach (var depId in depIds)
            {
                IList<decimal> reviewers = new List<decimal>();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, processId);
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, depId);

                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            reviewers.Add(DataConvertionHelper.GetDecimal(dataReader[0]));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                    if (reviewers.Count > 1)
                    {
                        if (workflowHandlerMode == WorkflowHandlerMode.InTurn || workflowHandlerMode == WorkflowHandlerMode.UserSelected)
                        {
                            decimal nextReviewerId = workflowInstanceStep.GetNextReviewerId(customWorkflowProcessInfo.ProcessId, reviewers, workflowHandlerMode);
                            nextReviewers.Add(depId, nextReviewerId);
                        }
                        else
                        {
                            nextReviewers.Add(depId, reviewers[0]);
                        }
                    }
                    else if (reviewers.Count == 1)
                    {
                        nextReviewers.Add(depId, reviewers[0]);
                    }
                }
            }



            return nextReviewers;
        }

        /// <summary>
        /// 获得动态节点的审核用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<decimal> GetGlobalNextReviewers(decimal userId, decimal processId, int top)
        {
            IList<decimal> nextReviewers = new List<decimal>();

            CustomWorkflowProcessInfo customWorkflowProcessInfo = GetModelInfo(processId);
            UserAccount userAccount = new UserAccount();
            ConditionType conditionType = (ConditionType)customWorkflowProcessInfo.ConditionType;            
            string physicalName = string.Empty;
            SqlDatabase db = null;
            IList<string> dataFieldNames = null;
            switch (conditionType)
            {
                case ConditionType.Table:
                    CustomTable customTable = new CustomTable();
                    byte dataWarehouseId = customTable.GetDataWarehouseId(customWorkflowProcessInfo.TableId);
                    db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                    physicalName = customTable.GetTablePhysicalName(customWorkflowProcessInfo.TableId);
                    CustomDataField customDataField = new CustomDataField();
                    dataFieldNames = customDataField.GetRoleConditionDataFieldNames(customWorkflowProcessInfo.TableId);
                    break;

                case ConditionType.View:
                    CustomView customView = new CustomView();
                    db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)customView.GetDataWarehouseId(customWorkflowProcessInfo.ViewId)));
                    physicalName = customView.GetViewPhysicalName(customWorkflowProcessInfo.ViewId);
                    dataFieldNames = customView.GetRoleConditionDataFieldNames(customWorkflowProcessInfo.ViewId);
                    break;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT TOP {0} {1}.UserId FROM {1} ", top, physicalName);
            sb.AppendFormat("INNER JOIN [Blue].[dbo].[RoleAndUser] ON {0}.UserId = [Blue].[dbo].[RoleAndUser].UserId ", physicalName);
            if (dataFieldNames.Count > 0)
            {
                sb.AppendFormat("INNER JOIN {0} A ON  ");
                foreach (var dataFieldName in dataFieldNames)
                {
                    sb.AppendFormat("A.{0} = {1}.{0} AND ", dataFieldName, physicalName);
                }
                sb.Remove(sb.Length - 5, 5);
                sb.AppendFormat(" WHERE A.UserId = @UserId AND [Blue].[dbo].[RoleAndUser].RoleId = @RoleId", physicalName);
            }
            else
            {
                sb.AppendFormat(" WHERE [Blue].[dbo].[RoleAndUser].RoleId = @RoleId", physicalName);
            }
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowProcessInfo.RoleId));
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        nextReviewers.Add(DataConvertionHelper.GetDecimal(dataReader[0]));
                    }
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                }
            }

            return nextReviewers;
        }

        /// <summary>
        /// 获得单位内动态节点的审核用户
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="top"></param>
        /// <returns></returns>
        public IList<decimal> GetNextReviewers(decimal userId, decimal processId, int top)
        {
            IList<decimal> nextReviewers = new List<decimal>();

            CustomWorkflowProcessInfo customWorkflowProcessInfo = GetModelInfo(processId);
            UserAccount userAccount = new UserAccount();
            decimal depId = userAccount.GetDepIdByUserId(userId);
            ConditionType conditionType = (ConditionType)customWorkflowProcessInfo.ConditionType;
            StringBuilder sb = new StringBuilder();
            string physicalName = string.Empty;
            SqlDatabase db = null;
            switch (conditionType)
            {
                case ConditionType.Table:
                    CustomTable customTable = new CustomTable();
                    byte dataWarehouseId = customTable.GetDataWarehouseId(customWorkflowProcessInfo.TableId);
                    db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                    physicalName = customTable.GetTablePhysicalName(customWorkflowProcessInfo.TableId);
                    break;

                case ConditionType.View:
                    CustomView customView = new CustomView();
                    db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)customView.GetDataWarehouseId(customWorkflowProcessInfo.ViewId)));
                    physicalName = customView.GetViewPhysicalName(customWorkflowProcessInfo.ViewId);
                    break;
            }

            sb.AppendFormat("SELECT TOP {0} {1}.UserId FROM {1} ", top, physicalName);
            sb.AppendFormat("INNER JOIN [Blue].[dbo].[RoleAndUser] ON {0}.UserId = [Blue].[dbo].[RoleAndUser].UserId ", physicalName);
            sb.AppendFormat("WHERE {0}.DepId = @DepId AND [Blue].[dbo].[RoleAndUser].RoleId = @RoleId", physicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "DepId", DbType.Decimal, depId);
                db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowProcessInfo.RoleId));
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        nextReviewers.Add(DataConvertionHelper.GetDecimal(dataReader[0]));
                    }
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                }
            }

            return nextReviewers;
        }

        /// <summary>
        /// 获取下一步条件节点的联系人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="processId"></param>
        /// <param name="commonDataFields"></param>
        /// <returns></returns>
        public Dictionary<decimal, Dictionary<decimal, string>> GetNextReviewers(decimal userId, decimal processId, Dictionary<decimal, CommonDataField> commonDataFields)
        {
            IList<CustomWorkflowProcessInfo> workflowProcessInfos = new List<CustomWorkflowProcessInfo>();

            WorkflowProcessAndDataField workflowProcessAndDataField = new WorkflowProcessAndDataField();
            IList<CustomWorkflowProcessInfo> customWorkflowProcessInfos = GetModelInfos(processId);
            CustomDataField customDataField = new CustomDataField();
            CustomWorkflowProcessInfo elseWorkflowProcessInfo = null;
            foreach (var customWorkflowProcessInfo in customWorkflowProcessInfos)
            {
                bool allow = true;
                NextTableRelation nextRelation = NextTableRelation.None;
                IList<WorkflowProcessAndDataFieldInfo> workflowProcessAndDataFieldInfos = workflowProcessAndDataField.GetModelInfos(customWorkflowProcessInfo.ProcessId);
                if (workflowProcessAndDataFieldInfos == null || workflowProcessAndDataFieldInfos.Count == 0)
                {
                    elseWorkflowProcessInfo = customWorkflowProcessInfo;
                }
                foreach (var workflowProcessAndDataFieldInfo in workflowProcessAndDataFieldInfos)
                {
                    BasedDataType dataFieldBase = BasedDataType.Boolean;
                    bool reuslt = false;
                    switch (nextRelation)
                    {
                        case NextTableRelation.And:
                            allow = allow && reuslt;
                            break;

                        case NextTableRelation.Or:
                            allow = allow | reuslt;
                            break;

                        case NextTableRelation.None:
                            allow = reuslt;
                            break;
                    }
                    if (commonDataFields.ContainsKey(workflowProcessAndDataFieldInfo.DataFieldId))
                    {
                        CommonDataField commonDataField = commonDataFields[workflowProcessAndDataFieldInfo.DataFieldId];
                        reuslt = CheckCondition(workflowProcessAndDataFieldInfo, commonDataField.DataFieldDataType, commonDataField.DataFieldValue);                                             
                    }
                    else
                    {
                        CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(workflowProcessAndDataFieldInfo.DataFieldId);
                        DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                        switch (dataFieldProperty)
                        {
                            case DataFieldProperty.LogicalDataField:
                                LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)customDataFieldInfo.DataFieldType;
                                dataFieldBase = DataFieldHelper.GetBasedDataType(logicalDataFieldType);
                                break;

                            case DataFieldProperty.PhysicalDataField:
                                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                                dataFieldBase = DataFieldHelper.GetBasedDataType(physicalDataFieldType);
                                break;
                        }
                        object dataFieldValue = null;
                        DbType dbType = DataFieldHelper.GetDbType(dataFieldBase);
                        reuslt = CheckCondition(workflowProcessAndDataFieldInfo, dbType, dataFieldValue);
                    }
                    nextRelation = (NextTableRelation)workflowProcessAndDataFieldInfo.NextRelation;                    
                }
                if (allow)
                {
                    workflowProcessInfos.Add(customWorkflowProcessInfo);
                }
            }
            if (workflowProcessInfos.Count == 0)
            {
                workflowProcessInfos.Add(elseWorkflowProcessInfo);
            }
                                    
            return GetNextReviewers(userId, workflowProcessInfos);
        }

        /// <summary>
        ///获取下一步处理联系人
        /// </summary>
        /// <param name="userId">发起人</param>
        ///  /// <param name="processId"></param>
        /// <returns></returns>
        public Dictionary<decimal, Dictionary<decimal, string>> GetNextReviewers(decimal userId, decimal processId)
        {
            IList<CustomWorkflowProcessInfo> customWorkflowProcessInfos = GetModelInfos(processId);

            return GetNextReviewers(userId, customWorkflowProcessInfos);
        }
        
        /// <summary>
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            string workflowName = GetWorkflowName(nodeId);
            string processName = GetProcessName(nodeId);
            names.Add(workflowName);
            names.Add(processName);
            
            return names;
        }
                
        /// <summary>
        /// 获得工作流程的节点类型
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public byte GetProcessType(decimal processId)
        {
            byte processType = 0;

            try
            {
                string sqlSelect = "SELECT ProcessType FROM CustomWorkflowProcess WHERE ProcessId = @ProcessId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, DataConvertionHelper.SetDecimal(processId));
                    processType = DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return processType;
        }

        /// <summary>
        /// 向 CustomWorkflowProcess 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowProcessInfo">customWorkflowProcessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomWorkflowProcessInfo customWorkflowProcessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //自动增加的关键字的值
            decimal customWorkflowProcessId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomWorkflowProcess(UserId, WorkflowId, RoleId, ViewId, TableId, ");
            sb.Append("ProcessName, ProcessCode, DataInputMode, ProcessCategory, ProcessType, ConditionType, HandlerType, ");
            sb.Append("HandlerMode, ProcessSetting, EnableHelp, HelpContent, LeftDays, IsRootNode, ToolTip, Sorting, Notes)");
            sb.Append("VALUES (@UserId, @WorkflowId, @RoleId, @ViewId, @TableId, ");
            sb.Append("@ProcessName, @ProcessCode, @DataInputMode,  @ProcessCategory, @ProcessType, @ConditionType, @HandlerType, ");
            sb.Append("@HandlerMode, @ProcessSetting, @EnableHelp, @HelpContent, @LeftDays, @IsRootNode, @ToolTip, @Sorting, @Notes);");
            sb.Append("SET @ProcessId = SCOPE_IDENTITY()");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customWorkflowProcessInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomWorkflowProcess", "Sorting", "WorkflowId", customWorkflowProcessInfo.WorkflowId, 0) + 1;

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "ProcessId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowProcessInfo.UserId));
                        db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, customWorkflowProcessInfo.WorkflowId);
                        db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowProcessInfo.RoleId));
                        db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowProcessInfo.ViewId));
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowProcessInfo.TableId));
                        db.AddInParameter(dbCommand, "ProcessName", DbType.String, customWorkflowProcessInfo.ProcessName);
                        db.AddInParameter(dbCommand, "ProcessCode", DbType.String, customWorkflowProcessInfo.ProcessCode);
                        db.AddInParameter(dbCommand, "DataInputMode", DbType.Byte, customWorkflowProcessInfo.DataInputMode);
                        db.AddInParameter(dbCommand, "ProcessCategory", DbType.Byte, customWorkflowProcessInfo.ProcessCategory);
                        db.AddInParameter(dbCommand, "ProcessType", DbType.Byte, customWorkflowProcessInfo.ProcessType);
                        db.AddInParameter(dbCommand, "ConditionType", DbType.Byte, customWorkflowProcessInfo.ConditionType);
                        db.AddInParameter(dbCommand, "HandlerType", DbType.Byte, customWorkflowProcessInfo.HandlerType);
                        db.AddInParameter(dbCommand, "HandlerMode", DbType.Byte, customWorkflowProcessInfo.HandlerMode);
                        db.AddInParameter(dbCommand, "ProcessSetting", DbType.Int64, customWorkflowProcessInfo.ProcessSetting);
                        db.AddInParameter(dbCommand, "EnableHelp", DbType.Boolean, customWorkflowProcessInfo.EnableHelp);
                        db.AddInParameter(dbCommand, "HelpContent", DbType.String, customWorkflowProcessInfo.HelpContent);
                        db.AddInParameter(dbCommand, "LeftDays", DbType.Int32, customWorkflowProcessInfo.LeftDays);
                        db.AddInParameter(dbCommand, "IsRootNode", DbType.Boolean, customWorkflowProcessInfo.IsRootNode); 
                        db.AddInParameter(dbCommand, "ToolTip", DbType.String, customWorkflowProcessInfo.ToolTip);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customWorkflowProcessInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customWorkflowProcessInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customWorkflowProcessId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@ProcessId"].Value, 0);
                    }
                    CustomWorkflow customWorkflow = new CustomWorkflow();
                    customWorkflow.UpdateLeafOfParentNode(customWorkflowProcessInfo.WorkflowId, false, db, transaction);

                    /* 插入附件 */
                    if (upLoadFileInfos != null && upLoadFileInfos.Count > 0)
                    {
                        PriavteAttachment messageAttachment = new PriavteAttachment();
                        messageAttachment.Insert(customWorkflowProcessId, (byte)AttachmentCategory.WorkflowProcess, upLoadFileInfos, db, transaction);
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

            return customWorkflowProcessId;
        }

        /// <summary>
        /// 更新 CustomWorkflowProcessInfo 对象
        /// </summary>
        /// <param name="customWorkflowProcessInfo">CustomWorkflowProcessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        public void Update(CustomWorkflowProcessInfo customWorkflowProcessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomWorkflowProcess SET UserId = @UserId, WorkflowId = @WorkflowId, RoleId = @RoleId, ");
            sb.Append("ViewId = @ViewId, TableId = @TableId, ProcessName = @ProcessName, ProcessCode = @ProcessCode, ");
            sb.Append("DataInputMode = @DataInputMode, ProcessCategory = @ProcessCategory, ProcessType = @ProcessType, ConditionType = @ConditionType, ");
            sb.Append("HandlerType = @HandlerType, HandlerMode = @HandlerMode, ProcessSetting = @ProcessSetting, EnableHelp = @EnableHelp, ");
            sb.Append("HelpContent = @HelpContent, LeftDays = @LeftDays, ToolTip = @ToolTip, Notes = @Notes ");
            sb.Append("WHERE ProcessId = @ProcessId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, customWorkflowProcessInfo.ProcessId);
                        db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowProcessInfo.UserId));
                        db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, customWorkflowProcessInfo.WorkflowId);
                        db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowProcessInfo.RoleId));
                        db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowProcessInfo.ViewId));
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowProcessInfo.TableId));
                        db.AddInParameter(dbCommand, "ProcessName", DbType.String, customWorkflowProcessInfo.ProcessName);
                        db.AddInParameter(dbCommand, "ProcessCode", DbType.String, customWorkflowProcessInfo.ProcessCode);
                        db.AddInParameter(dbCommand, "DataInputMode", DbType.Byte, customWorkflowProcessInfo.DataInputMode);
                        db.AddInParameter(dbCommand, "ProcessCategory", DbType.Byte, customWorkflowProcessInfo.ProcessCategory);
                        db.AddInParameter(dbCommand, "ProcessType", DbType.Byte, customWorkflowProcessInfo.ProcessType);
                        db.AddInParameter(dbCommand, "ConditionType", DbType.Byte, customWorkflowProcessInfo.ConditionType);
                        db.AddInParameter(dbCommand, "HandlerType", DbType.Byte, customWorkflowProcessInfo.HandlerType);
                        db.AddInParameter(dbCommand, "HandlerMode", DbType.Byte, customWorkflowProcessInfo.HandlerMode);
                        db.AddInParameter(dbCommand, "ProcessSetting", DbType.Int64, customWorkflowProcessInfo.ProcessSetting);
                        db.AddInParameter(dbCommand, "EnableHelp", DbType.Boolean, customWorkflowProcessInfo.EnableHelp);
                        db.AddInParameter(dbCommand, "HelpContent", DbType.String, customWorkflowProcessInfo.HelpContent);
                        db.AddInParameter(dbCommand, "LeftDays", DbType.Int32, customWorkflowProcessInfo.LeftDays);
                        db.AddInParameter(dbCommand, "IsRootNode", DbType.Boolean, customWorkflowProcessInfo.IsRootNode);
                        db.AddInParameter(dbCommand, "ToolTip", DbType.String, customWorkflowProcessInfo.ToolTip);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customWorkflowProcessInfo.Notes);
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }
                    PriavteAttachment messageAttachment = new PriavteAttachment();
                    messageAttachment.Update(customWorkflowProcessInfo.ProcessId, (byte)AttachmentCategory.WorkflowProcess, upLoadFileInfos, db, transaction);
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
        /// 获得数据填报编号
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public decimal GetDatalId(decimal processId)
        {
            decimal dataId = decimal.MinValue;
            try
            {
                string sqlSelect = "SELECT DataId FROM CustomWorkflowProcess INNER JOIN CustomWorkflow ON CustomWorkflowProcess.WorkflowId = CustomWorkflow.WorkflowId WHERE ProcessId = @ProcessId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, DataConvertionHelper.SetDecimal(processId));
                    dataId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataId;
        }

        /// <summary>
        /// 获得工作流名称
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public string GetWorkflowName(decimal processId)
        {
            string workflowName = string.Empty;

            try
            {
                string sqlSelect = "SELECT WorkflowName FROM CustomWorkflowProcess INNER JOIN CustomWorkflow ON CustomWorkflowProcess.WorkflowId = CustomWorkflow.WorkflowId WHERE ProcessId = @ProcessId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, DataConvertionHelper.SetDecimal(processId));
                    workflowName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return workflowName;
        }

        /// <summary>
        /// 获得节点名称
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public string GetProcessName(decimal processId)
        {
            string processName = string.Empty;
            try
            {
                string sqlSelect = "SELECT ProcessName FROM CustomWorkflowProcess WHERE ProcessId = @ProcessId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ProcessId", DbType.Decimal, DataConvertionHelper.SetDecimal(processId));
                    processName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return processName;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomWorkflowProcessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomWorkflowProcessInfo 对象列表</returns>
        private IList<CustomWorkflowProcessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomWorkflowProcessInfo> customWorkflowProcessInfos = new List<CustomWorkflowProcessInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomWorkflowProcess");
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
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal workflowId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal viewId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            string processName = DataConvertionHelper.GetString(dataReader[6]);
                            string processCode = DataConvertionHelper.GetString(dataReader[7]);
                            byte dataInputMode = DataConvertionHelper.GetByte(dataReader[8]);
                            byte processCategory = DataConvertionHelper.GetByte(dataReader[9]);
                            byte processType = DataConvertionHelper.GetByte(dataReader[10]);
                            byte conditionType = DataConvertionHelper.GetByte(dataReader[11]);
                            byte handlerType = DataConvertionHelper.GetByte(dataReader[12]);
                            byte handlerMode = DataConvertionHelper.GetByte(dataReader[13]);
                            long processSetting = DataConvertionHelper.GetLong(dataReader[14]);
                            bool enableHelp = DataConvertionHelper.GetBoolean(dataReader[15]);
                            string helpContent = DataConvertionHelper.GetString(dataReader[16]);
                            int leftDays = DataConvertionHelper.GetInt(dataReader[17]);
                            bool isRootNode = DataConvertionHelper.GetBoolean(dataReader[18]);
                            string toolTip = DataConvertionHelper.GetString(dataReader[19]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[20]);
                            string notes = DataConvertionHelper.GetString(dataReader[21]);
                            //将创建 CustomWorkflowProcessInfo 对象加入集合中
                            customWorkflowProcessInfos.Add(new CustomWorkflowProcessInfo(processId, userId, workflowId, roleId, viewId,
                            tableId, processName, processCode, dataInputMode, processCategory,
                            processType, conditionType, handlerType, handlerMode, processSetting,
                            enableHelp, helpContent, leftDays, isRootNode, toolTip,
                            sorting, notes));
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

            return customWorkflowProcessInfos;
        }

        /// <summary>
        /// 获得 CustomWorkflowProcessInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowProcessInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomWorkflowProcess");
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
        /// 获得表 CustomWorkflowProcess 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomWorkflowProcess ", "ProcessId", "*", false, false, startPosition,
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
        /// 获得以表 CustomWorkflowProcess 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomWorkflowProcess ", "ProcessId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomWorkflowProcess 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomWorkflowProcess ", "ProcessId", "*", false, false, startPosition,
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
        /// 获得以表 CustomWorkflowProcess 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomWorkflowProcess ", "ProcessId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomWorkflowProcessInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomWorkflowProcess");
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
        /// 检查条件
        /// </summary>
        /// <param name="workflowProcessAndDataFieldInfo"></param>
        /// <param name="dbType"></param>
        /// <param name="dataFieldValue"></param>
        /// <returns></returns>
        private bool CheckCondition(WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo, DbType dbType, object dataFieldValue)
        {
            bool result = false;

            DataFieldCondition fstCondition = (DataFieldCondition)workflowProcessAndDataFieldInfo.FstCondition;
            DataFieldCondition scdCondition = (DataFieldCondition)workflowProcessAndDataFieldInfo.ScdCondition;

            switch (dbType)
            {
                case DbType.Boolean:
                    switch(fstCondition)
                    {
                        case DataFieldCondition.Equal:
                            if (workflowProcessAndDataFieldInfo.BoolValue == DataConvertionHelper.GetBoolean(dataFieldValue))
                            {
                                result = true;
                            }                            
                            break;

                        case DataFieldCondition.Not:
                            if (workflowProcessAndDataFieldInfo.BoolValue != DataConvertionHelper.GetBoolean(dataFieldValue))
                            {
                                result = true;
                            }
                            break;
                    }
                    break;

                case DbType.DateTime:
                    DateTime dtDataFieldValue = DataConvertionHelper.GetDateTime(dataFieldValue);
                    switch (fstCondition)
                    {
                        case DataFieldCondition.More:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstTimeValue) || 
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstTimeValue)
                                && !DataConvertionHelper.IsNullValue(dtDataFieldValue) 
                                && (dtDataFieldValue > workflowProcessAndDataFieldInfo.FstTimeValue)))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.MoreOrEqual:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstTimeValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstTimeValue)
                                && !DataConvertionHelper.IsNullValue(dtDataFieldValue)
                                && (dtDataFieldValue >= workflowProcessAndDataFieldInfo.FstTimeValue)))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.Less:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdTimeValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdTimeValue)
                                && !DataConvertionHelper.IsNullValue(dtDataFieldValue)
                                && (dtDataFieldValue < workflowProcessAndDataFieldInfo.ScdTimeValue)))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.LessOrEqual:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdTimeValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdTimeValue)
                                && !DataConvertionHelper.IsNullValue(dtDataFieldValue)
                                && (dtDataFieldValue <= workflowProcessAndDataFieldInfo.ScdTimeValue)))
                            {
                                result = true;
                            }
                            break;
                    }
                    break;

                case DbType.Decimal:
                    decimal dcDataFieldValue = DataConvertionHelper.GetDecimal(dataFieldValue);
                    switch (fstCondition)
                    {
                        case DataFieldCondition.More:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstDecimalValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstDecimalValue)
                                && !DataConvertionHelper.IsNullValue(dcDataFieldValue)
                                && (dcDataFieldValue > workflowProcessAndDataFieldInfo.FstDecimalValue)))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.MoreOrEqual:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstDecimalValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstDecimalValue)
                                && !DataConvertionHelper.IsNullValue(dcDataFieldValue)
                                && (dcDataFieldValue >= workflowProcessAndDataFieldInfo.FstDecimalValue)))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.Less:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdDecimalValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdDecimalValue)
                                && !DataConvertionHelper.IsNullValue(dcDataFieldValue)
                                && (dcDataFieldValue < workflowProcessAndDataFieldInfo.ScdDecimalValue)))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.LessOrEqual:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdDecimalValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdDecimalValue)
                                && !DataConvertionHelper.IsNullValue(dcDataFieldValue)
                                && (dcDataFieldValue <= workflowProcessAndDataFieldInfo.ScdDecimalValue)))
                            {
                                result = true;
                            }
                            break;
                    }
                    break;

                case DbType.Int32:
                    Int32 intDataFieldValue = DataConvertionHelper.GetInt(dataFieldValue);
                    switch (fstCondition)
                    {
                        case DataFieldCondition.More:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstIntegerValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstIntegerValue)
                                && !DataConvertionHelper.IsNullValue(intDataFieldValue)
                                && (intDataFieldValue > workflowProcessAndDataFieldInfo.FstIntegerValue)))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.MoreOrEqual:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstIntegerValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstIntegerValue)
                                && !DataConvertionHelper.IsNullValue(intDataFieldValue)
                                && (intDataFieldValue >= workflowProcessAndDataFieldInfo.FstIntegerValue)))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.Less:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdIntegerValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdIntegerValue)
                                && !DataConvertionHelper.IsNullValue(intDataFieldValue)
                                && (intDataFieldValue < workflowProcessAndDataFieldInfo.ScdIntegerValue)))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.LessOrEqual:
                            if (DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdIntegerValue) ||
                                (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdIntegerValue)
                                && !DataConvertionHelper.IsNullValue(intDataFieldValue)
                                && (intDataFieldValue <= workflowProcessAndDataFieldInfo.ScdIntegerValue)))
                            {
                                result = true;
                            }
                            break;
                    }
                    break;

                case DbType.String:
                    string stringDataFieldValue = DataConvertionHelper.GetString(dataFieldValue);
                    switch (fstCondition)
                    {
                        case DataFieldCondition.Equal:
                            if (!string.IsNullOrWhiteSpace(stringDataFieldValue) &&
                                !string.IsNullOrWhiteSpace(workflowProcessAndDataFieldInfo.StringValue) && 
                                stringDataFieldValue.Equals(workflowProcessAndDataFieldInfo.StringValue))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.StartWith:
                            if (!string.IsNullOrWhiteSpace(stringDataFieldValue) &&
                                !string.IsNullOrWhiteSpace(workflowProcessAndDataFieldInfo.StringValue) &&
                                stringDataFieldValue.StartsWith(workflowProcessAndDataFieldInfo.StringValue))
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.Like:
                            if (!string.IsNullOrWhiteSpace(stringDataFieldValue) &&
                                !string.IsNullOrWhiteSpace(workflowProcessAndDataFieldInfo.StringValue) &&
                                stringDataFieldValue.IndexOf(workflowProcessAndDataFieldInfo.StringValue) >= 0)
                            {
                                result = true;
                            }
                            break;

                        case DataFieldCondition.Not:
                            if (!stringDataFieldValue.Equals(workflowProcessAndDataFieldInfo.StringValue))
                            {
                                result = true;
                            }
                            break;
                    }
                    break;
            }

            return result;
        }


        /// <summary>
        /// 获取下一步处理联系人
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="customWorkflowProcessInfos"></param>
        /// <returns></returns>
        private Dictionary<decimal, Dictionary<decimal, string>> GetNextReviewers(decimal userId, IList<CustomWorkflowProcessInfo> customWorkflowProcessInfos)
        {
            Dictionary<decimal, Dictionary<decimal, string>> nextReviewers = new Dictionary<decimal, Dictionary<decimal, string>>();

            foreach (CustomWorkflowProcessInfo customWorkflowProcessInfo in customWorkflowProcessInfos)
            {
                Dictionary<decimal, string> reviewers = new Dictionary<decimal, string>();
                nextReviewers.Add(customWorkflowProcessInfo.ProcessId, reviewers);
                WorkflowHandlerType workflowHandlerType = (WorkflowHandlerType)customWorkflowProcessInfo.HandlerType;
                UserAccount userAccount = new UserAccount();
                StringPairValue stringPairValue = null;
                IList<CommonUserInfo> commonUserInfos = new List<CommonUserInfo>();
                switch (workflowHandlerType)
                {
                    case WorkflowHandlerType.Sponsor:
                        if (!reviewers.ContainsKey(userId))
                        {
                            stringPairValue = userAccount.GetUserNameInfoByUserId(userId);
                            reviewers.Add(userId, string.Format("{0}[{1}]", stringPairValue.First, stringPairValue.Second));
                        }
                        break;

                    case WorkflowHandlerType.DepRole:
                        decimal depId = userAccount.GetDepIdByUserId(userId);
                        commonUserInfos = userAccount.GetCommonUserInfos(customWorkflowProcessInfo.RoleId, depId);
                        foreach (CommonUserInfo commonUserInfo in commonUserInfos)
                        {
                            if (!reviewers.ContainsKey(commonUserInfo.UserId))
                            {
                                reviewers.Add(commonUserInfo.UserId, string.Format("{0}[{1}]", commonUserInfo.UserName, commonUserInfo.UserActualName));
                            }
                        }
                        break;

                    case WorkflowHandlerType.ManagementRole:
                        decimal currentDepId = userAccount.GetDepIdByUserId(userId);
                        Dictionary<decimal, string> managementUsers = userAccount.GetManagementUsers(customWorkflowProcessInfo.RoleId, currentDepId);
                        foreach (var managementUser in managementUsers)
                        {
                            reviewers.Add(managementUser.Key, managementUser.Value);
                        }
                        break;

                    case WorkflowHandlerType.Role:
                        commonUserInfos = userAccount.GetCommonUserInfos(customWorkflowProcessInfo.RoleId);
                        foreach (CommonUserInfo commonUserInfo in commonUserInfos)
                        {
                            if (!reviewers.ContainsKey(commonUserInfo.UserId))
                            {
                                reviewers.Add(commonUserInfo.UserId, string.Format("{0}[{1}]", commonUserInfo.UserName, commonUserInfo.UserActualName));
                            }
                        }
                        break;

                    case WorkflowHandlerType.User:
                        if (!reviewers.ContainsKey(customWorkflowProcessInfo.UserId))
                        {
                            stringPairValue = userAccount.GetUserNameInfoByUserId(customWorkflowProcessInfo.UserId);
                            reviewers.Add(customWorkflowProcessInfo.UserId, string.Format("{0}[{1}]", stringPairValue.First, stringPairValue.Second));
                        }
                        break;
                }
            }

            return nextReviewers;
        }

        /// <summary>
        /// 根据节点父编号获取节点列表
        /// </summary>
        /// <param name="parentProcessId"></param>
        /// <returns></returns>
        private IList<CustomWorkflowProcessInfo> GetModelInfos(decimal parentProcessId)
        {
            //创建集合对象
            IList<CustomWorkflowProcessInfo> customWorkflowProcessInfos = new List<CustomWorkflowProcessInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomWorkflowProcess.* FROM CustomWorkflowProcess ");
            sb.Append("INNER JOIN CustomWorkflowMap ON CustomWorkflowMap.ProcessId = CustomWorkflowProcess.ProcessId  WHERE CustomWorkflowMap.ParentProcessId = @ParentProcessId ");
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ParentProcessId", DbType.Decimal, parentProcessId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal processId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal workflowId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal viewId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            string processName = DataConvertionHelper.GetString(dataReader[6]);
                            string processCode = DataConvertionHelper.GetString(dataReader[7]);
                            byte dataInputMode = DataConvertionHelper.GetByte(dataReader[8]);
                            byte processCategory = DataConvertionHelper.GetByte(dataReader[9]);
                            byte processType = DataConvertionHelper.GetByte(dataReader[10]);
                            byte conditionType = DataConvertionHelper.GetByte(dataReader[11]);
                            byte handlerType = DataConvertionHelper.GetByte(dataReader[12]);
                            byte handlerMode = DataConvertionHelper.GetByte(dataReader[13]);
                            long processSetting = DataConvertionHelper.GetLong(dataReader[14]);
                            bool enableHelp = DataConvertionHelper.GetBoolean(dataReader[15]);
                            string helpContent = DataConvertionHelper.GetString(dataReader[16]);
                            int leftDays = DataConvertionHelper.GetInt(dataReader[17]);
                            bool isRootNode = DataConvertionHelper.GetBoolean(dataReader[18]);
                            string toolTip = DataConvertionHelper.GetString(dataReader[19]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[20]);
                            string notes = DataConvertionHelper.GetString(dataReader[21]);
                            //将创建 CustomWorkflowProcessInfo 对象加入集合中
                            customWorkflowProcessInfos.Add(new CustomWorkflowProcessInfo(processId, userId, workflowId, roleId, viewId,
                            tableId, processName, processCode, dataInputMode, processCategory,
                            processType, conditionType, handlerType, handlerMode, processSetting,
                            enableHelp, helpContent, leftDays, isRootNode, toolTip,
                            sorting, notes));
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

            return customWorkflowProcessInfos;
        }

        #endregion

        #endregion
    }
}
