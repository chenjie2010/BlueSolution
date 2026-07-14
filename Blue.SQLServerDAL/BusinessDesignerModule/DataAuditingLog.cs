//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingLog.cs
// 描述: DataAuditingLog 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using Blue.SQLServerDAL.BusinessModule;
using Blue.SQLServerDAL.SystemModule;
using Blue.SQLServerDAL.UserModule;

namespace Blue.SQLServerDAL.BusinessDesignerModule
{
    /// <summary>
    /// DataAuditingLog 表的数据层访问类
    /// </summary>
    public class DataAuditingLog : IDataAuditingLog
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public DataAuditingLog()
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 DataAuditingLog 表中插入一条新记录
		/// </summary>
		/// <param name="dataAuditingLogInfo">dataAuditingLogInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(DataAuditingLogInfo dataAuditingLogInfo)
		{
			//自动增加的关键字的值
			decimal dataAuditingLogId = 0;
			
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();

			try
            {
                dataAuditingLogId = Insert(dataAuditingLogInfo, db, null);
            }
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return dataAuditingLogId;
		}

        /// <summary>
		/// 获得 DataAuditingLogInfo 对象
		/// </summary>
		///<param name="auditingLogId">审核日志编号</param>
		/// <returns> DataAuditingLogInfo 对象</returns>
		public DataAuditingLogInfo GetModelInfo(decimal auditingLogId)
		{			
			DataAuditingLogInfo  dataAuditingLogInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("AuditingLogId", "AuditingLogId", DbType.Decimal, auditingLogId, DataFieldCondition.Equal));

            //创建集合对象
            IList<DataAuditingLogInfo>  dataAuditingLogInfos = GetModelInfos(whereConditons, null, true);
            if (dataAuditingLogInfos != null && dataAuditingLogInfos.Count > 0)
            {
                dataAuditingLogInfo = dataAuditingLogInfos[0];
            }

            return dataAuditingLogInfo;            
		}
        
        /// <summary>
		/// 更新 DataAuditingLogInfo 对象
		/// </summary>
		/// <param name="dataAuditingLogInfo">DataAuditingLogInfo 对象</param>
		public void Update(DataAuditingLogInfo dataAuditingLogInfo)
		{
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE DataAuditingLog SET DataAuditingId = @DataAuditingId, UserId = @UserId, ParentUserId = @ParentUserId, AuditingLogName = @AuditingLogName, ");
            sb.Append("AuditingLogType = @AuditingLogType, AuditingStatus = @AuditingStatus, AuditingLogTime = @AuditingLogTime, LogDescription = @LogDescription ");
            sb.Append("WHERE AuditingLogId = @AuditingLogId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, dataAuditingLogInfo.AuditingLogId);
                    db.AddInParameter(dbCommand, "DataAuditingId", DbType.Decimal, dataAuditingLogInfo.DataAuditingId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, dataAuditingLogInfo.UserId);
                    db.AddInParameter(dbCommand, "ParentUserId", DbType.Decimal, dataAuditingLogInfo.ParentUserId);
                    db.AddInParameter(dbCommand, "AuditingLogName", DbType.String, dataAuditingLogInfo.AuditingLogName);
                    db.AddInParameter(dbCommand, "AuditingLogType", DbType.Byte, dataAuditingLogInfo.AuditingLogType);
                    db.AddInParameter(dbCommand, "AuditingStatus", DbType.Byte, dataAuditingLogInfo.AuditingStatus);
                    db.AddInParameter(dbCommand, "AuditingLogTime", DbType.DateTime, dataAuditingLogInfo.AuditingLogTime);
                    db.AddInParameter(dbCommand, "LogDescription", DbType.String, dataAuditingLogInfo.LogDescription);
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
		///  删除 DataAuditingLogInfo 对象
		/// </summary>
	    ///<param name="auditingLogId">审核日志编号</param>
		public void Delete(decimal auditingLogId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM DataAuditingLog ");
			sb.Append("WHERE AuditingLogId = @AuditingLogId");
                        
            List<decimal> tableIds = new List<decimal>();
            CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
            DataAuditing dataAuditing = new DataAuditing();
            DataAuditingInfo dataAuditingInfo = dataAuditing.GetParentDataAuditingInfoByLogId(auditingLogId);
            
            FormType formType = (FormType)dataAuditingInfo.TableType;
            switch (formType)
            {
                case FormType.Table:
                    tableIds.Add(dataAuditingInfo.TableId);
                    break;

                case FormType.CombinedTable:
                    IList<decimal> secondIds = combinedTableRelation.GetSecondIds(dataAuditingInfo.CombinedTableId);
                    tableIds.AddRange(secondIds);
                    break;

                default:
                    throw new ArgumentException("不支持该属性。");
            }
            CustomTable customTable = new CustomTable();
            foreach (var tableId in tableIds)
            {
                string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
                string sqlDeleted = string.Format("DELETE FROM {0} WHERE BusinessId = @BusinessId", tablePhysicalName);
                SqlDatabase dbBlue = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                using (DbCommand dbCommand = dbBlue.GetSqlStringCommand(sqlDeleted))
                {
                    dbBlue.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, auditingLogId);
                    //执行删除操作
                    dbBlue.ExecuteNonQuery(dbCommand);
                }
            }

            DataAuditingStep dataAuditingStep = new DataAuditingStep();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    dataAuditingStep.Delete(auditingLogId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, auditingLogId);
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
		/// 获得 DataAuditingLogInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataAuditingLogInfo 对象列表</returns>
		public IList<DataAuditingLogInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 DataAuditingLog 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>DataAuditingLogInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "DataAuditingLog ", "AuditingLogId", false, whereConditons);
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
        /// 根据条件统计信息变更数量
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public Dictionary<byte, int> GetStaticsByAuditingStatus(IList<WhereConditon> whereConditons)
        {
            Dictionary<byte, int> statics = new Dictionary<byte, int>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT  COUNT(AuditingLogId), AuditingStatus FROM DataAuditingLog");
            if (whereConditons != null && whereConditons.Count > 0)
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            sb.Append(" GROUP BY AuditingStatus");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
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
                            int count = DataConvertionHelper.GetInt(dataReader[0]);
                            byte auditingStatus = DataConvertionHelper.GetByte(dataReader[1]);
                            statics.Add(auditingStatus, count);
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

            return statics;
        }


        /// <summary>
        /// 终审完成
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="commment"></param>
        public void CompleteBusiness(decimal dataAuditingLogId, decimal userId, string commment)
        {
            DataAuditingStep dataAuditingStep = new DataAuditingStep();
            CommonNode commonNode = dataAuditingStep.GetLastestSubmitter(dataAuditingLogId);
            DataAuditingStepInfo dataAuditingStepInfo = new DataAuditingStepInfo(0, decimal.MinValue, dataAuditingLogId,
                userId, (byte)AuditingAction.Compelete, DateTime.Now, commment);
            DataAuditingLogInfo dataAuditingLogInfo = GetModelInfo(dataAuditingLogId);
            DataAuditing dataAuditing = new DataAuditing();
            DataAuditingInfo dataAuditingInfo = dataAuditing.GetParentDataAuditingInfo(dataAuditingLogInfo.DataAuditingId);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    try
                    {
                        dataAuditingStep.Insert(dataAuditingStepInfo, db, transaction);
                        UpdateAuditingStatus(dataAuditingLogId, (byte)AuditingStatus.Completed, db, transaction);
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    CustomTable customTable = new CustomTable();
                    List<decimal> tableIds = new List<decimal>();
                    string tablePhysicalName = string.Empty;
                    Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
                    FormType formType = (FormType)dataAuditingInfo.TableType;
                    DataAuditingAndDataField dataAuditingAndDataField = new DataAuditingAndDataField();
                    /* 个人信息变更：字段是变更业务字段与权限字段的交集 */
                    List <CommonNode> dataFields = dataAuditingAndDataField.GetDataFields(dataAuditingLogInfo.DataAuditingId);
                    switch (formType)
                    {
                        case FormType.Table:                            
                            tablePhysicalName = customTable.GetTablePhysicalName(dataAuditingInfo.TableId);
                            tableIds.Add(dataAuditingInfo.TableId);
                            break;

                        case FormType.CombinedTable:                            
                            CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
                            IList<decimal> secondIds = combinedTableRelation.GetSecondIds(dataAuditingInfo.CombinedTableId);
                            tableIds.AddRange(secondIds);
                            tablePhysicalName = customTable.GetTablePhysicalName(secondIds[0]);
                            //dataFields = combinedDataField.GetDataFields(dataAuditingInfo.CombinedTableId);
                            break;
                    }
                    RoleAndTable roleAndTable = new RoleAndTable();
                    CustomDataField customDataField = new CustomDataField();
                    IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = roleAndTable.GetAuthorizedExtendedCustomDataFieldInfos(userId, tableIds, DataAuthorityType.Auditing);
                    foreach (var extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
                    {
                        if ((dataFields.FindIndex(dataField => dataField.NodeId == extendedCustomDataFieldInfo.DataFieldId) < 0))
                        {
                            continue;
                        }
                        DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
                        string expressionText = string.Empty;
                        if (dataFieldProperty == DataFieldProperty.LogicalDataField)
                        {
                            LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                            if (logicalDataFieldType == LogicalDataFieldType.DigitExpression || logicalDataFieldType == LogicalDataFieldType.StringExpression
                                || logicalDataFieldType == LogicalDataFieldType.DateTimeExpression)
                            {
                                expressionText = customDataField.GetDataFieldLogicalExpression(extendedCustomDataFieldInfo.DataFieldId);
                            }
                        }
                        dataFieldNameRelations.Add(extendedCustomDataFieldInfo.PhysicalName,
                                new CommonDataFieldInfo(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.TableId, extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.LogicalName,
                                expressionText, dataFieldProperty, extendedCustomDataFieldInfo.DataFieldType));
                    }                   
                    UserAccount userAccount = new UserAccount();
                    CommonUserInfo commonUserInfo = userAccount.GetCommonUserInfo(dataAuditingLogInfo.UserId);
                    AuditingLogType auditingLogType = (AuditingLogType)dataAuditingLogInfo.AuditingLogType;
                    byte dataWarehouseId = 0;
                    switch (formType)
                    {
                        case FormType.Table:
                            DataTableType dataTableType = customTable.GetDataTableType(dataAuditingInfo.TableId);
                            dataWarehouseId = customTable.GetDataWarehouseId(dataAuditingInfo.TableId);
                            SqlDatabase dbBlue = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                            DataTable dataTable = customTable.GetMirrorRowData(dataAuditingInfo.TableId, 0, dataFieldNameRelations, dataAuditingLogId, false);
                            switch (auditingLogType)
                            {
                                case AuditingLogType.Add:
                                    if (dataTable != null && dataTable.Rows.Count > 0)
                                    {
                                        decimal tragetRecordId = DataConvertionHelper.GetDecimal(dataTable.Rows[0]["RecordId"]);
                                        IList<object> dataFieldValues = new List<object>();
                                        foreach (var dataFieldNameRelation in dataFieldNameRelations)
                                        {
                                            DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldNameRelation.Value.DataFieldProperty;
                                            if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                                            {
                                                object obj = dataTable.Rows[0][dataFieldNameRelation.Value.PhysicalName];
                                                dataFieldValues.Add(obj);
                                            }
                                        }
                                        AddRecord(dbBlue, dataAuditingInfo.TableId, tablePhysicalName, dataFieldNameRelations, dataFieldValues, commonUserInfo, tragetRecordId);
                                    }
                                    break;

                                case AuditingLogType.Edit:
                                    if (dataTable != null && dataTable.Rows.Count > 1)
                                    {
                                        decimal recordId = DataConvertionHelper.GetDecimal(dataTable.Rows[0]["RecordId"]);
                                        decimal tragetRecordId = DataConvertionHelper.GetDecimal(dataTable.Rows[1]["RecordId"]);
                                        IList<object> dataFieldValues = new List<object>();
                                        foreach (var dataFieldNameRelation in dataFieldNameRelations)
                                        {
                                            DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldNameRelation.Value.DataFieldProperty;
                                            if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                                            {
                                                object obj = dataTable.Rows[1][dataFieldNameRelation.Value.PhysicalName];
                                                dataFieldValues.Add(obj);
                                            }
                                        }
                                        UpdateRecord(dbBlue, dataAuditingInfo.TableId, tablePhysicalName, recordId, dataFieldValues, dataFieldNameRelations, tragetRecordId);
                                    }
                                    break;
                            }
                            break;

                        case FormType.CombinedTable:
                            CombinedTable combinedTable = new CombinedTable();
                            Dictionary<decimal, DataRowItem> dataRowItems = combinedTable.GetMirrorRowData(dataAuditingInfo.CombinedTableId, dataFieldNameRelations, dataAuditingLogId, false);
                            foreach (var dataRowItem in dataRowItems)
                            {
                                Dictionary<string, CommonDataFieldInfo> fieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
                                tablePhysicalName = customTable.GetTablePhysicalName(dataRowItem.Key);
                                dataWarehouseId = customTable.GetDataWarehouseId(dataRowItem.Key);
                                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                                switch (auditingLogType)
                                {
                                    case AuditingLogType.Add:
                                        if (dataRowItem.Value.TragetRow != null && dataRowItem.Value.TragetRow.Rows.Count > 0)
                                        {
                                            decimal tragetRecordId = DataConvertionHelper.GetDecimal(dataRowItem.Value.TragetRow.Rows[0]["RecordId"]);
                                            IList<object> dataFieldValues = new List<object>();
                                            foreach (var dataFieldNameRelation in dataFieldNameRelations)
                                            {
                                                DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldNameRelation.Value.DataFieldProperty;
                                                if (dataFieldProperty == DataFieldProperty.PhysicalDataField && dataRowItem.Value.TragetRow.Columns.Contains(dataFieldNameRelation.Value.PhysicalName))
                                                {
                                                    fieldNameRelations.Add(dataFieldNameRelation.Key, dataFieldNameRelation.Value);
                                                    object obj = dataRowItem.Value.TragetRow.Rows[0][dataFieldNameRelation.Value.PhysicalName];
                                                    dataFieldValues.Add(obj);
                                                }
                                            }
                                            if (dataFieldValues.Count > 0)
                                            {
                                                AddRecord(dbBusiness, dataAuditingInfo.TableId, tablePhysicalName, fieldNameRelations, dataFieldValues, commonUserInfo, tragetRecordId);
                                            }
                                        }
                                        break;

                                    case AuditingLogType.Edit:
                                        if (dataRowItem.Value.SourceRow != null && dataRowItem.Value.SourceRow.Rows.Count > 0 
                                            && dataRowItem.Value.TragetRow != null && dataRowItem.Value.TragetRow.Rows.Count > 0)
                                        {
                                            decimal targetRecordId = DataConvertionHelper.GetDecimal(dataRowItem.Value.TragetRow.Rows[0]["RecordId"]);
                                            decimal recordId = DataConvertionHelper.GetDecimal(dataRowItem.Value.SourceRow.Rows[0]["RecordId"]);
                                            IList<object> dataFieldValues = new List<object>();
                                            foreach (var dataFieldNameRelation in dataFieldNameRelations)
                                            {
                                                DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldNameRelation.Value.DataFieldProperty;
                                                if (dataFieldProperty == DataFieldProperty.PhysicalDataField && dataRowItem.Value.TragetRow.Columns.Contains(dataFieldNameRelation.Value.PhysicalName))
                                                {
                                                    fieldNameRelations.Add(dataFieldNameRelation.Key, dataFieldNameRelation.Value);
                                                    object obj = dataRowItem.Value.TragetRow.Rows[0][dataFieldNameRelation.Value.PhysicalName];
                                                    dataFieldValues.Add(obj);
                                                }
                                            }
                                            if (dataFieldValues.Count > 0)
                                            {
                                                UpdateRecord(dbBusiness, dataAuditingInfo.TableId, tablePhysicalName, recordId, dataFieldValues, fieldNameRelations, targetRecordId);
                                            }
                                        }
                                        break;
                                }
                            }
                            break;
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
        /// 提交到下一步
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="userId"></param>
        /// <param name="nextReviewerId"></param>
        /// <param name="auditingAction"></param>
        /// <param name="commment"></param>
        public void SubmitBusinessToNextStep(decimal dataAuditingLogId, AuditingStatus auditingStatus, 
            decimal userId, decimal nextReviewerId, AuditingAction auditingAction, string commment)
        {
            DataAuditingStep dataAuditingStep = new DataAuditingStep();
            CommonNode commonNode = dataAuditingStep.GetLastestSubmitter(dataAuditingLogId);
            DataAuditingStepInfo dataAuditingStepInfo = new DataAuditingStepInfo(0, nextReviewerId, dataAuditingLogId,
                userId, (byte)auditingAction, DateTime.Now, commment);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    dataAuditingStep.Insert(dataAuditingStepInfo, db, transaction);
                    UpdateAuditingStatus(dataAuditingLogId, (byte)auditingStatus, db, transaction);
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
        /// 获得待审核记录数
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetDataAuditingCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(1) FROM DataAuditingLog ");
            sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = DataAuditingLog.UserId ");
            sb.Append("INNER JOIN DataAuditingStep ON DataAuditingLog.AuditingLogId = DataAuditingStep.AuditingLogId ");
            sb.Append("INNER JOIN (SELECT AuditingLogId, MAX(StepId)AS StepId FROM DataAuditingStep GROUP BY AuditingLogId) A ON A.StepId = DataAuditingStep.StepId ");
            if (whereConditons != null && whereConditons.Count > 0)
            {
                sb.Append("WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
            }

            return count;
        }

        /// <summary>
        /// 获得待审核数据
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public DataSet GetDataAuditing(int startPosition, int count, IList<WhereConditon> whereConditons)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();            
            sb.Append("SELECT DataAuditingLog.AuditingLogId, AuditingLogName, UserName, UserActualName, AuditingLogType, AuditingStatus, LogDescription, AuditingLogTime FROM DataAuditingLog ");
            sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = DataAuditingLog.UserId ");
            sb.Append("INNER JOIN DataAuditingStep ON DataAuditingLog.AuditingLogId = DataAuditingStep.AuditingLogId ");
            sb.Append("INNER JOIN (SELECT AuditingLogId, MAX(StepId)AS StepId FROM DataAuditingStep GROUP BY AuditingLogId) A ON A.StepId = DataAuditingStep.StepId ");
            if (whereConditons != null && whereConditons.Count > 0)
            {
                sb.Append("WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }            
            sb.AppendFormat(" ORDER BY AuditingLogTime OFFSET {0} ROW  FETCH NEXT {1} ROW ONLY", startPosition, count);
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                ds = db.ExecuteDataSet(dbCommand);
            }

            return ds;
        }

        /// <summary>
        /// 获得关联的审核编号
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public decimal GetDataAuditingId(decimal auditingLogId)
        {
            decimal dataAuditingId = decimal.MinValue;

            //查询语句
            string sqlSelect = "SELECT DataAuditingId FROM DataAuditingLog WHERE AuditingLogId = @AuditingLogId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, DataConvertionHelper.SetDecimal(auditingLogId));
                dataAuditingId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
            }

            return dataAuditingId;
        }

        /// <summary>
        /// 获得审核描述
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public string GetLogDescription(decimal auditingLogId)
        {
            string logDescription = string.Empty;

            //查询语句
            string sqlSelect = "SELECT LogDescription FROM DataAuditingLog WHERE AuditingLogId = @AuditingLogId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, DataConvertionHelper.SetDecimal(auditingLogId));
                logDescription = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
            }

            return logDescription;
        }

        /// <summary>
        /// 驳回
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="commment"></param>
        public void Reject(decimal dataAuditingLogId, decimal userId, AuditingStatus auditingStatus, string commment)
        {
            DataAuditingStep dataAuditingStep = new DataAuditingStep();
            CommonNode commonNode = dataAuditingStep.GetLastestSubmitter(dataAuditingLogId);
            DataAuditingStepInfo dataAuditingStepInfo = new DataAuditingStepInfo(0, userId, dataAuditingLogId,
                commonNode.NodeId, (byte)AuditingAction.Reject, DateTime.Now, commment);
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    dataAuditingStep.Insert(dataAuditingStepInfo, db, transaction);
                    UpdateAuditingStatus(dataAuditingLogId, (byte)auditingStatus, db, transaction);
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
        /// 撤回
        /// </summary>
        /// <param name="dataAuditingLogId"></param>
        /// <param name="userId"></param>
        /// <param name="auditingStatus"></param>
        public void WithDraw(decimal dataAuditingLogId, decimal userId, AuditingStatus auditingStatus)
        {
            DataAuditingStep dataAuditingStep = new DataAuditingStep();
            DataAuditingStepInfo dataAuditingStepInfo = new DataAuditingStepInfo(0, userId, dataAuditingLogId,
                userId, (byte)AuditingAction.WithDraw, DateTime.Now, string.Empty);
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    dataAuditingStep.Insert(dataAuditingStepInfo, db, transaction);
                    UpdateAuditingStatus(dataAuditingLogId, (byte)auditingStatus, db, transaction);
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
        /// 获得表 DataAuditingLog 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                string dataFileNames = @"DataAuditingLog.AuditingLogId, DataAuditingLog.DataAuditingId, DataAuditingLog.AuditingLogName, DataAuditingLog.LogDescription, DataAuditingLog.AuditingStatus, DataAuditingLog.AuditingLogTime";
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("DataAuditing", "DataAuditingId", TableJoin.InnerJoin));
                ds = DataAccessHandler.GetPageRecord(db, "DataAuditingLog ", "AuditingLogId", dataFileNames, false, false, tableLinks, startPosition,
                    count, whereConditons, ref totalCount);                
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
        /// 向 DataAuditingLog 表中插入一条新记录
        /// </summary>
        /// <param name="dataAuditingLogInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public decimal Insert(DataAuditingLogInfo dataAuditingLogInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal dataAuditingLogId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO DataAuditingLog(DataAuditingId, UserId, ParentUserId, AuditingLogName, AuditingLogType, ");
            sb.Append("AuditingStatus, AuditingLogTime, LogDescription)");
            sb.Append("VALUES (@DataAuditingId, @UserId, @ParentUserId, @AuditingLogName, @AuditingLogType, ");
            sb.Append("@AuditingStatus, @AuditingLogTime, @LogDescription);");
            sb.Append("SET @AuditingLogId = SCOPE_IDENTITY()");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "AuditingLogId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "DataAuditingId", DbType.Decimal, dataAuditingLogInfo.DataAuditingId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, dataAuditingLogInfo.UserId);
                    db.AddInParameter(dbCommand, "ParentUserId", DbType.Decimal, dataAuditingLogInfo.ParentUserId);
                    db.AddInParameter(dbCommand, "AuditingLogName", DbType.String, dataAuditingLogInfo.AuditingLogName);
                    db.AddInParameter(dbCommand, "AuditingLogType", DbType.Byte, dataAuditingLogInfo.AuditingLogType);
                    db.AddInParameter(dbCommand, "AuditingStatus", DbType.Byte, dataAuditingLogInfo.AuditingStatus);
                    db.AddInParameter(dbCommand, "AuditingLogTime", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "LogDescription", DbType.String, dataAuditingLogInfo.LogDescription);
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
                    dataAuditingLogId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@AuditingLogId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataAuditingLogId;
        }

        /// <summary>
        /// 更新申请信息
        /// </summary>
        /// <param name="dataAuditingLogInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void UpdateAuditingInfo(DataAuditingLogInfo dataAuditingLogInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();

            sb.Append("UPDATE DataAuditingLog SET  AuditingLogName = @AuditingLogName, AuditingStatus = @AuditingStatus, AuditingLogTime = @AuditingLogTime, LogDescription = @LogDescription ");
            sb.Append("WHERE AuditingLogId = @AuditingLogId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, dataAuditingLogInfo.AuditingLogId);
                    db.AddInParameter(dbCommand, "AuditingLogName", DbType.String, dataAuditingLogInfo.AuditingLogName);
                    db.AddInParameter(dbCommand, "AuditingStatus", DbType.Byte, dataAuditingLogInfo.AuditingStatus);
                    db.AddInParameter(dbCommand, "AuditingLogTime", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "LogDescription", DbType.String, dataAuditingLogInfo.LogDescription);
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

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 DataAuditingLogInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>DataAuditingLogInfo 对象列表</returns>
        private IList<DataAuditingLogInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<DataAuditingLogInfo>  dataAuditingLogInfos = new List<DataAuditingLogInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM DataAuditingLog");
            
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
                            decimal auditingLogId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal dataAuditingId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal parentUserId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            string auditingLogName = DataConvertionHelper.GetString(dataReader[4]);
                            byte auditingLogType = DataConvertionHelper.GetByte(dataReader[5]);
                            byte auditingStatus = DataConvertionHelper.GetByte(dataReader[6]);
                            DateTime auditingLogTime = DataConvertionHelper.GetDateTime(dataReader[7]);
                            string logDescription = DataConvertionHelper.GetString(dataReader[8]);
                            //将创建 DataAuditingLogInfo 对象加入集合中
                            dataAuditingLogInfos.Add(new DataAuditingLogInfo(auditingLogId, dataAuditingId, userId, parentUserId, auditingLogName,
                            auditingLogType, auditingStatus, auditingLogTime, logDescription));
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
            
			return dataAuditingLogInfos;
		}
        
		
		/// <summary>
		/// 获得 DataAuditingLogInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>DataAuditingLogInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM DataAuditingLog");
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
        /// 获得表 DataAuditingLog 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataAuditingLog ", "AuditingLogId", "*", false, false, startPosition, 
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
        /// 获得以表 DataAuditingLog 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataAuditingLog ", "AuditingLogId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得以表 DataAuditingLog 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataAuditingLog ", "AuditingLogId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  DataAuditingLogInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataAuditingLog");
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
        /// 增加记录
        /// </summary>
        /// <param name="dbBlue"></param>
        /// <param name="tableId"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="dataFieldValues"></param>
        /// <param name="commonUserInfo"></param>
        /// <param name="tragetRecordId"></param>
        private void AddRecord(SqlDatabase dbBlue, decimal tableId, string tablePhysicalName, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations,
            IList<object> dataFieldValues, CommonUserInfo commonUserInfo, decimal tragetRecordId)
        {
            CustomTable customTable = new CustomTable();
            DataAuditing dataAuditing = new DataAuditing();
            DataTableType dataTableType = customTable.GetDataTableType(tableId);
            /* 1.更新当前历史状态 */
            if (dataTableType == DataTableType.MasterSlaveTable)
            {
                dataAuditing.UpdateCurrentStateByUserId(commonUserInfo.UserId, (byte)CurrentState.History, tablePhysicalName, dbBlue, null);
            }

            /* 2. 增加记录 */
            StringBuilder sb = new StringBuilder();
            StringBuilder sbValue = new StringBuilder();
            int recordSorting = DataAccessHandler.GetNextRecordSorting(dbBlue, tablePhysicalName, commonUserInfo.UserId);
            sb.AppendFormat("INSERT INTO {0} (UserId, UserName, UserTypeId, DepId, RecordSorting, AuditedStatus, CurrentState, CreationTime, ModificationTime, IsDeleted", tablePhysicalName);
            sbValue.Append("VALUES (@UserId, @UserName, @UserTypeId, @DepId, @RecordSorting, @AuditedStatus, @CurrentState, @CreationTime, @ModificationTime, @IsDeleted");
            foreach (var dataFieldNameRelation in dataFieldNameRelations)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldNameRelation.Value.DataFieldProperty;
                if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                {
                    sb.AppendFormat(", {0}", dataFieldNameRelation.Value.PhysicalName);
                    sbValue.AppendFormat(", @{0}", dataFieldNameRelation.Value.PhysicalName);
                }
            }
            sb.AppendFormat(") {0});SET @RecordId = SCOPE_IDENTITY()", sbValue);
            using (DbCommand cmd = dbBlue.GetSqlStringCommand(sb.ToString()))
            {
                //给参数赋值
                dbBlue.AddOutParameter(cmd, "RecordId", DbType.Decimal, 10);
                dbBlue.AddInParameter(cmd, "RecordSorting", DbType.Int32, recordSorting);
                dbBlue.AddInParameter(cmd, "AuditedStatus", DbType.Byte, (byte)AuditedStatus.Auditing);
                dbBlue.AddInParameter(cmd, "CurrentState", DbType.Byte, (byte)CurrentState.Current);
                dbBlue.AddInParameter(cmd, "IsDeleted", DbType.Boolean, false);
                dbBlue.AddInParameter(cmd, "UserId", DbType.Decimal, commonUserInfo.UserId);
                dbBlue.AddInParameter(cmd, "UserName", DbType.String, commonUserInfo.UserName);
                dbBlue.AddInParameter(cmd, "UserTypeId", DbType.Decimal, commonUserInfo.UserTypeId);
                dbBlue.AddInParameter(cmd, "DepId", DbType.Decimal, commonUserInfo.DepId);
                dbBlue.AddInParameter(cmd, "CreationTime", DbType.DateTime, DateTime.Now);
                dbBlue.AddInParameter(cmd, "ModificationTime", DbType.DateTime, DateTime.Now);
                int index = 0;
                foreach (var dataFieldNameRelation in dataFieldNameRelations)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldNameRelation.Value.DataFieldProperty;
                    if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)dataFieldNameRelation.Value.DataFieldType;
                        DbType basedDataType = DataFieldHelper.GetDbType(physicalDataFieldType);
                        DataAccessHandler.AddInParameterWithConstraint(dbBlue, cmd, dataFieldNameRelation.Value.PhysicalName, basedDataType, dataFieldValues[index++]);
                    }
                }
                //执行插入操作
                if (dbBlue.ExecuteNonQuery(cmd) != 1)
                {
                    throw new Exception("插入失败。");
                }
            }

            /* 3.删除源记录 */
            string sqlDelete = string.Format("DELETE FROM {0} WHERE RecordId = @RecordId", tablePhysicalName);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, tragetRecordId);
                db.ExecuteNonQuery(dbCommand);
            }
        }

        /// <summary>
        /// 更新记录
        /// </summary>
        /// <param name="dbBlue"></param>
        /// <param name="tableId"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="recordId"></param>
        /// <param name="dataFieldValues"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="tragetRecordId"></param>
        private void UpdateRecord(SqlDatabase dbBlue, decimal tableId, string tablePhysicalName, decimal recordId, IList<object> dataFieldValues,
            Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal tragetRecordId)
        {
            DataAuditing dataAuditing = new DataAuditing();
            /* 1. 更新记录 */
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} SET ModificationTime = @ModificationTime", tablePhysicalName);
            foreach (var dataFieldNameRelation in dataFieldNameRelations)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldNameRelation.Value.DataFieldProperty;
                if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                {
                    sb.AppendFormat(", {0} = @{0}", dataFieldNameRelation.Value.PhysicalName);
                }
            }
            sb.Append(" WHERE RecordId = @RecordId");
            using (DbCommand cmd = dbBlue.GetSqlStringCommand(sb.ToString()))
            {
                //给参数赋值
                dbBlue.AddInParameter(cmd, "RecordId", DbType.Decimal, recordId);
                dbBlue.AddInParameter(cmd, "ModificationTime", DbType.DateTime, DateTime.Now);
                int index = 0;
                foreach (var dataFieldNameRelation in dataFieldNameRelations)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldNameRelation.Value.DataFieldProperty;
                    if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)dataFieldNameRelation.Value.DataFieldType;
                        DbType basedDataType = DataFieldHelper.GetDbType(physicalDataFieldType);
                        object obj = dataFieldValues[index++];
                        if (physicalDataFieldType == PhysicalDataFieldType.DocAttachment || physicalDataFieldType == PhysicalDataFieldType.PDFAttachment
                            || physicalDataFieldType == PhysicalDataFieldType.PicAttachment)
                        {
                            string upLoadSourceFileName = DataConvertionHelper.GetString(obj);                            
                            CustomTable customTable = new CustomTable();
                            string oldFileName = DataConvertionHelper.GetString(customTable.GetDataFiledValueByRecordId(tragetRecordId, dataFieldNameRelation.Value.DataFieldId));  
                            /* 删除源文件 */
                            if (!string.IsNullOrWhiteSpace(oldFileName) && !oldFileName.Equals(upLoadSourceFileName))
                            {
                                int count = 0;
                                //查询语句
                                string sqlSelect = string.Format("SELECT COUNT(1) FROM {0} WHERE {1} = @{1}", tablePhysicalName, dataFieldNameRelation.Value.PhysicalName);
                                using (DbCommand dbCommand = dbBlue.GetSqlStringCommand(sqlSelect))
                                {
                                    //给参数赋值
                                    dbBlue.AddInParameter(dbCommand, dataFieldNameRelation.Value.PhysicalName, DbType.String, oldFileName);
                                    count = DataConvertionHelper.GetInt(dbBlue.ExecuteScalar(dbCommand));
                                }
                                if (count <= 1)
                                {
                                    dataAuditing.DeleteUploadFiles(dataFieldNameRelation.Value.PhysicalName, oldFileName);
                                }
                            }
                        }
                        DataAccessHandler.AddInParameterWithConstraint(dbBlue, cmd, dataFieldNameRelation.Value.PhysicalName, basedDataType, obj);
                    }
                }
                //执行更新操作
                if (dbBlue.ExecuteNonQuery(cmd) != 1)
                {
                    throw new Exception("更新失败。");
                }
            }

            /* 2.删除源记录 */
            string sqlDelete = string.Format("DELETE FROM {0} WHERE RecordId = @RecordId", tablePhysicalName);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, tragetRecordId);
                db.ExecuteNonQuery(dbCommand);
            }
        }

        /// <summary>
        /// 更新申请状态
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <param name="auditingStatus"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void UpdateAuditingStatus(decimal auditingLogId, byte auditingStatus, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            string sqlUpdated = "UPDATE DataAuditingLog SET AuditingStatus = @AuditingStatus WHERE AuditingLogId = @AuditingLogId";
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdated))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, auditingLogId);
                    db.AddInParameter(dbCommand, "AuditingStatus", DbType.Byte, auditingStatus);

                    //执行更新操作             
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

        #endregion
        
		#endregion		
    }
}
