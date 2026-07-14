//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflow.cs
// 描述：CustomWorkflow 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/10/9
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
using Blue.SQLServerDAL.GeneralAffairModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomWorkflow 表的数据层访问类
    /// </summary>
    public class CustomWorkflow : CommonNodeDataAccess, ICustomWorkflow
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomWorkflow() : base("CustomWorkflow", "WorkflowId", "GroupId", "WorkflowName", "WorkflowCode", true, false)
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomWorkflow 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowInfo">customWorkflowInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomWorkflowInfo customWorkflowInfo)
        {
            //自动增加的关键字的值
            decimal customWorkflowId = 0;

            try
            {
                customWorkflowId = Insert(customWorkflowInfo, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customWorkflowId;
        }

        /// <summary>
		/// 获得 CustomWorkflowInfo 对象
		/// </summary>
		///<param name="workflowId">工作流编号</param>
		/// <returns> CustomWorkflowInfo 对象</returns>
		public CustomWorkflowInfo GetModelInfo(decimal workflowId)
        {
            CustomWorkflowInfo customWorkflowInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("WorkflowId", "WorkflowId", System.Data.DbType.Decimal, workflowId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomWorkflowInfo> customWorkflowInfos = GetModelInfos(whereConditons, null, true);
            if (customWorkflowInfos != null && customWorkflowInfos.Count > 0)
            {
                customWorkflowInfo = customWorkflowInfos[0];
            }

            return customWorkflowInfo;
        }

        /// <summary>
        /// 更新 CustomWorkflowInfo 对象
        /// </summary>
        /// <param name="customWorkflowInfo">CustomWorkflowInfo 对象</param>
        public void Update(CustomWorkflowInfo customWorkflowInfo)
        {
            try
            {
                Update(customWorkflowInfo, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        ///  删除 CustomWorkflowInfo 对象
        /// </summary>
        ///<param name="workflowId">工作流编号</param>
        public void Delete(decimal workflowId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomWorkflow ");
            sb.Append("WHERE WorkflowId = @WorkflowId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, workflowId);
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
        /// 获得 CustomWorkflowInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowInfo 对象列表</returns>
        public IList<CustomWorkflowInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomWorkflow 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomWorkflowInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomWorkflow ", "WorkflowId", false, whereConditons);
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
        /// 工作流节点作为父节点的数目
        /// </summary>
        /// <param name="workflowId"></param>
        /// <returns></returns>
        public int GetParentNodeCount(decimal workflowId)
        {
            int count = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(1) FROM CustomWorkflow ");
            sb.Append("INNER JOIN CustomWorkflowProcess ON CustomWorkflowProcess.WorkflowId = CustomWorkflow.WorkflowId ");
            sb.Append("INNER JOIN CustomWorkflowMap ON CustomWorkflowMap.ParentProcessId = CustomWorkflowProcess.ProcessId ");
            sb.Append("WHERE CustomWorkflow.WorkflowId = @WorkflowId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, workflowId);
                    count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 0);
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
        /// 根据工作流实例编号获得工作流对象
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public CustomWorkflowInfo GetCustomWorkflowInfo(decimal instanceId)
        {
            CustomWorkflowInfo customWorkflowInfo = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomWorkflow.* FROM CustomWorkflow ");
            sb.Append("INNER JOIN CustomWorkflowInstance ON CustomWorkflow.WorkflowId = CustomWorkflowInstance.WorkflowId ");
            sb.Append("WHERE InstanceId = @InstanceId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal workflowId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal dataId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal parentRoleId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            string workflowName = DataConvertionHelper.GetString(dataReader[6]);
                            string workflowCode = DataConvertionHelper.GetString(dataReader[7]);
                            byte workflowType = DataConvertionHelper.GetByte(dataReader[8]);
                            byte structCategory = DataConvertionHelper.GetByte(dataReader[9]);
                            long instanceNameRule = DataConvertionHelper.GetLong(dataReader[10]);
                            string instanceNameFormat = DataConvertionHelper.GetString(dataReader[11]);
                            bool initReviewStatus = DataConvertionHelper.GetBoolean(dataReader[12]);
                            bool enableManager = DataConvertionHelper.GetBoolean(dataReader[13]);
                            bool allocationStatus = DataConvertionHelper.GetBoolean(dataReader[14]);
                            bool allowAuditing = DataConvertionHelper.GetBoolean(dataReader[15]);
                            bool finalReviewStatus = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool allowAllocation = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool enableGuidance = DataConvertionHelper.GetBoolean(dataReader[18]);
                            string guidance = DataConvertionHelper.GetString(dataReader[19]);
                            bool workflowEnabled = DataConvertionHelper.GetBoolean(dataReader[20]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[21]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[22]);
                            string notes = DataConvertionHelper.GetString(dataReader[23]);
                            //创建 CustomWorkflowInfo 对象
                            customWorkflowInfo = new CustomWorkflowInfo(workflowId, userId, groupId, dataId, roleId,
                            parentRoleId, workflowName, workflowCode, workflowType, structCategory,
                            instanceNameRule, instanceNameFormat, initReviewStatus, enableManager, allocationStatus,
                            allowAuditing, finalReviewStatus, allowAllocation, enableGuidance, guidance,
                            workflowEnabled, isLeaf, sorting, notes);
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

            return customWorkflowInfo;
        }

        /// <summary>
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            CustomWorkflowInfo customWorkflowInfo = GetModelInfo(nodeId);
            if (customWorkflowInfo != null)
            {
                CustomGroup customGroup = new CustomGroup();
                IList<string> parentNames = customGroup.GetHierarchicalNamesOfNode(customWorkflowInfo.GroupId);
                foreach (var parentName in parentNames)
                {
                    names.Add(parentName);
                }
                names.Add(customWorkflowInfo.WorkflowName);
            }

            return names;
        }

        /// <summary>
        /// 向 CustomWorkflow 表中插入一条新记录
        /// </summary>
        /// <param name="customWorkflowInfo">customWorkflowInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomWorkflowInfo customWorkflowInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //自动增加的关键字的值
            decimal customWorkflowId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            customWorkflowInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomWorkflow", "Sorting", "GroupId", customWorkflowInfo.GroupId, 0) + 1;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomWorkflow(UserId, GroupId, DataId, RoleId, ParentRoleId, ");
            sb.Append("WorkflowName, WorkflowCode, WorkflowType, StructCategory, InstanceNameRule, ");
            sb.Append("InstanceNameFormat, InitReviewStatus, EnableManager, AllocationStatus, AllowAuditing, ");
            sb.Append("FinalReviewStatus, AllowAllocation, EnableGuidance, Guidance, WorkflowEnabled, ");
            sb.Append("IsLeaf, Sorting, Notes)");
            sb.Append("VALUES (@UserId, @GroupId, @DataId, @RoleId, @ParentRoleId, ");
            sb.Append("@WorkflowName, @WorkflowCode, @WorkflowType, @StructCategory, @InstanceNameRule, ");
            sb.Append("@InstanceNameFormat, @InitReviewStatus, @EnableManager, @AllocationStatus, @AllowAuditing, ");
            sb.Append("@FinalReviewStatus, @AllowAllocation, @EnableGuidance, @Guidance, @WorkflowEnabled, ");
            sb.Append("@IsLeaf, @Sorting, @Notes);");
            sb.Append("SET @WorkflowId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "WorkflowId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowInfo.UserId));
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customWorkflowInfo.GroupId);
                        db.AddInParameter(dbCommand, "DataId", DbType.Decimal, customWorkflowInfo.DataId);
                        db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowInfo.RoleId));
                        db.AddInParameter(dbCommand, "ParentRoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowInfo.ParentRoleId));
                        db.AddInParameter(dbCommand, "WorkflowName", DbType.String, customWorkflowInfo.WorkflowName);
                        db.AddInParameter(dbCommand, "WorkflowCode", DbType.String, customWorkflowInfo.WorkflowCode);
                        db.AddInParameter(dbCommand, "WorkflowType", DbType.Byte, customWorkflowInfo.WorkflowType);
                        db.AddInParameter(dbCommand, "StructCategory", DbType.Byte, customWorkflowInfo.StructCategory);
                        db.AddInParameter(dbCommand, "InstanceNameRule", DbType.Int64, customWorkflowInfo.InstanceNameRule);
                        db.AddInParameter(dbCommand, "InstanceNameFormat", DbType.String, customWorkflowInfo.InstanceNameFormat);
                        db.AddInParameter(dbCommand, "InitReviewStatus", DbType.Boolean, customWorkflowInfo.InitReviewStatus);
                        db.AddInParameter(dbCommand, "EnableManager", DbType.Boolean, customWorkflowInfo.EnableManager);
                        db.AddInParameter(dbCommand, "AllocationStatus", DbType.Boolean, customWorkflowInfo.AllocationStatus);
                        db.AddInParameter(dbCommand, "AllowAuditing", DbType.Boolean, customWorkflowInfo.AllowAuditing);
                        db.AddInParameter(dbCommand, "FinalReviewStatus", DbType.Boolean, customWorkflowInfo.FinalReviewStatus);
                        db.AddInParameter(dbCommand, "AllowAllocation", DbType.Boolean, customWorkflowInfo.AllowAllocation);
                        db.AddInParameter(dbCommand, "EnableGuidance", DbType.Boolean, customWorkflowInfo.EnableGuidance);
                        db.AddInParameter(dbCommand, "Guidance", DbType.String, customWorkflowInfo.Guidance);
                        db.AddInParameter(dbCommand, "WorkflowEnabled", DbType.Boolean, customWorkflowInfo.WorkflowEnabled);
                        db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, true);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customWorkflowInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customWorkflowInfo.Notes);
                                               
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customWorkflowId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@WorkflowId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(customWorkflowInfo.GroupId, false, db, transaction);

                    /* 插入附件 */
                    if (upLoadFileInfos != null && upLoadFileInfos.Count > 0)
                    {
                        PriavteAttachment messageAttachment = new PriavteAttachment();
                        messageAttachment.Insert(customWorkflowId, (byte)AttachmentCategory.Workflow, upLoadFileInfos, db, transaction);
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

            return customWorkflowId;
        }

        /// <summary>
        /// 更新 CustomWorkflowInfo 对象
        /// </summary>
        /// <param name="customWorkflowInfo">CustomWorkflowInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        public void Update(CustomWorkflowInfo customWorkflowInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomWorkflow SET UserId = @UserId, GroupId = @GroupId, DataId = @DataId, ");
            sb.Append("RoleId = @RoleId, ParentRoleId = @ParentRoleId, WorkflowName = @WorkflowName, ");
            sb.Append("WorkflowCode = @WorkflowCode, WorkflowType = @WorkflowType, StructCategory = @StructCategory, ");
            sb.Append("InstanceNameRule = @InstanceNameRule, InstanceNameFormat = @InstanceNameFormat, InitReviewStatus = @InitReviewStatus, ");
            sb.Append("EnableManager = @EnableManager, AllocationStatus = @AllocationStatus, AllowAuditing = @AllowAuditing, ");
            sb.Append("FinalReviewStatus = @FinalReviewStatus, AllowAllocation = @AllowAllocation, EnableGuidance = @EnableGuidance, ");
            sb.Append("Guidance = @Guidance, WorkflowEnabled = @WorkflowEnabled, Notes = @Notes ");
            sb.Append("WHERE WorkflowId = @WorkflowId");

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
                        db.AddInParameter(dbCommand, "WorkflowId", DbType.Decimal, customWorkflowInfo.WorkflowId);
                        db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowInfo.UserId));
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customWorkflowInfo.GroupId);
                        db.AddInParameter(dbCommand, "DataId", DbType.Decimal, customWorkflowInfo.DataId);
                        db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowInfo.RoleId));
                        db.AddInParameter(dbCommand, "ParentRoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(customWorkflowInfo.ParentRoleId));
                        db.AddInParameter(dbCommand, "WorkflowName", DbType.String, customWorkflowInfo.WorkflowName);
                        db.AddInParameter(dbCommand, "WorkflowCode", DbType.String, customWorkflowInfo.WorkflowCode);
                        db.AddInParameter(dbCommand, "WorkflowType", DbType.Byte, customWorkflowInfo.WorkflowType);
                        db.AddInParameter(dbCommand, "StructCategory", DbType.Byte, customWorkflowInfo.StructCategory);
                        db.AddInParameter(dbCommand, "InstanceNameRule", DbType.Int64, customWorkflowInfo.InstanceNameRule);
                        db.AddInParameter(dbCommand, "InstanceNameFormat", DbType.String, customWorkflowInfo.InstanceNameFormat);
                        db.AddInParameter(dbCommand, "InitReviewStatus", DbType.Boolean, customWorkflowInfo.InitReviewStatus);
                        db.AddInParameter(dbCommand, "EnableManager", DbType.Boolean, customWorkflowInfo.EnableManager);
                        db.AddInParameter(dbCommand, "AllocationStatus", DbType.Boolean, customWorkflowInfo.AllocationStatus);
                        db.AddInParameter(dbCommand, "AllowAuditing", DbType.Boolean, customWorkflowInfo.AllowAuditing);
                        db.AddInParameter(dbCommand, "FinalReviewStatus", DbType.Boolean, customWorkflowInfo.FinalReviewStatus);
                        db.AddInParameter(dbCommand, "AllowAllocation", DbType.Boolean, customWorkflowInfo.AllowAllocation);
                        db.AddInParameter(dbCommand, "EnableGuidance", DbType.Boolean, customWorkflowInfo.EnableGuidance);
                        db.AddInParameter(dbCommand, "Guidance", DbType.String, customWorkflowInfo.Guidance);
                        db.AddInParameter(dbCommand, "WorkflowEnabled", DbType.Boolean, customWorkflowInfo.WorkflowEnabled);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customWorkflowInfo.Notes);
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }
                    PriavteAttachment messageAttachment = new PriavteAttachment();
                    messageAttachment.Update(customWorkflowInfo.WorkflowId, (byte)AttachmentCategory.Workflow, upLoadFileInfos, db, transaction);
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
        /// 获得 CustomWorkflowInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomWorkflowInfo 对象列表</returns>
        private IList<CustomWorkflowInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomWorkflowInfo> customWorkflowInfos = new List<CustomWorkflowInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomWorkflow");
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
                            decimal workflowId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal dataId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal roleId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            decimal parentRoleId = DataConvertionHelper.GetDecimal(dataReader[5]);
                            string workflowName = DataConvertionHelper.GetString(dataReader[6]);
                            string workflowCode = DataConvertionHelper.GetString(dataReader[7]);
                            byte workflowType = DataConvertionHelper.GetByte(dataReader[8]);
                            byte structCategory = DataConvertionHelper.GetByte(dataReader[9]);
                            long instanceNameRule = DataConvertionHelper.GetLong(dataReader[10]);
                            string instanceNameFormat = DataConvertionHelper.GetString(dataReader[11]);
                            bool initReviewStatus = DataConvertionHelper.GetBoolean(dataReader[12]);
                            bool enableManager = DataConvertionHelper.GetBoolean(dataReader[13]);
                            bool allocationStatus = DataConvertionHelper.GetBoolean(dataReader[14]);
                            bool allowAuditing = DataConvertionHelper.GetBoolean(dataReader[15]);
                            bool finalReviewStatus = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool allowAllocation = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool enableGuidance = DataConvertionHelper.GetBoolean(dataReader[18]);
                            string guidance = DataConvertionHelper.GetString(dataReader[19]);
                            bool workflowEnabled = DataConvertionHelper.GetBoolean(dataReader[20]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[21]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[22]);
                            string notes = DataConvertionHelper.GetString(dataReader[23]);
                            //将创建 CustomWorkflowInfo 对象加入集合中
                            customWorkflowInfos.Add(new CustomWorkflowInfo(workflowId, userId, groupId, dataId, roleId,
                            parentRoleId, workflowName, workflowCode, workflowType, structCategory,
                            instanceNameRule, instanceNameFormat, initReviewStatus, enableManager, allocationStatus,
                            allowAuditing, finalReviewStatus, allowAllocation, enableGuidance, guidance,
                            workflowEnabled, isLeaf, sorting, notes));
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

            return customWorkflowInfos;
        }

        /// <summary>
        /// 获得 CustomWorkflowInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomWorkflowInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomWorkflow");
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
        /// 获得表 CustomWorkflow 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomWorkflow ", "WorkflowId", "*", false, false, startPosition,
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
        /// 获得以表 CustomWorkflow 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomWorkflow ", "WorkflowId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomWorkflow 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomWorkflow ", "WorkflowId", "*", false, false, startPosition,
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
        /// 获得以表 CustomWorkflow 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomWorkflow ", "WorkflowId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomWorkflowInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomWorkflow");
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
