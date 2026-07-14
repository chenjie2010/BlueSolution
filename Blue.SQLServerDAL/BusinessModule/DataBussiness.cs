//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：DataBussiness.cs
// 描述：DataBussiness 数据层访问类
// 作者：ChenJie 
// 编写日期：2019/05/17
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.BusinessDesignerModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// 数据业务访问类
    /// </summary>
    public class DataBussiness : IDataBussiness
    {
        /// <summary>
        /// 根据表的编号查看未填数据的用户
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="auditedStatus"></param>
        /// <param name="currentState"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetUsersWithoutData(decimal tableId, int startPosition, int count, IList<WhereConditon> whereConditons, byte auditedStatus, byte currentState, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                CustomTable customTable = new CustomTable();
                byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                string dataSurceName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
                string tablePhysicalName = string.Format("{0}.dbo.{1}", dataSurceName, customTable.GetTablePhysicalName(tableId));
                string dataFileNames = @"UserAccount.UserName, UserAccount.UserActualName, CustomDepartment.DepName, UserType.UserTypeName";
                IList<TableLink> tableLinks = new List<TableLink>();
                //tableLinks.Add(DataFieldHelper.GetTableLink(tablePhysicalName, SystemDataField.UserActualName));
                tableLinks.Add(new TableLink("UserType", "UserTypeId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomDepartment", "DepId", TableJoin.InnerJoin));

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("(SELECT RecordId, UserId FROM {0} WHERE ", tablePhysicalName);
                if (auditedStatus < byte.MaxValue)
                {
                    sb.AppendFormat("AuditedStatus = {0}", auditedStatus);
                }
                if (currentState < byte.MaxValue)
                {
                    if (auditedStatus < byte.MaxValue)
                    {
                        sb.Append(" AND ");
                    }
                    sb.AppendFormat("CurrentState = {0}", currentState);
                }
                sb.Append(")");                
                tableLinks.Add(new TableLink("UserAccount", "UserId", TableJoin.LeftOuterJoin, sb.ToString(), "UserId", "A"));
                whereConditons.Add(new WhereConditon("A", "RecordId", "RecordId",
                    DbType.Decimal, null, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                ds = DataAccessHandler.GetPageRecord(db, "UserAccount", "UserId", dataFileNames, false, true, tableLinks, startPosition,
                    count, whereConditons, ref totalCount);

                ds.Tables[0].Columns[0].Caption = "用户名";
                ds.Tables[0].Columns[1].Caption = "姓名";
                ds.Tables[0].Columns[2].Caption = "单位名称";
                ds.Tables[0].Columns[3].Caption = "用户类型";
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 根据表的编号查看未填数据的用户
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetUsersWithoutData(decimal tableId, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                CustomTable customTable = new CustomTable();                
                byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                string dataSurceName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
                string tablePhysicalName = string.Format("{0}.dbo.{1}", dataSurceName, customTable.GetTablePhysicalName(tableId));
                string dataFileNames = @"UserAccount.UserName, UserAccount.UserActualName, CustomDepartment.DepName, UserType.UserTypeName";
                IList<TableLink> tableLinks = new List<TableLink>();
                //tableLinks.Add(DataFieldHelper.GetTableLink(tablePhysicalName, SystemDataField.UserActualName));
                tableLinks.Add(new TableLink("UserType", "UserTypeId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomDepartment", "DepId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink(tablePhysicalName, "UserId", TableJoin.LeftOuterJoin));

                whereConditons.Add(new WhereConditon(tablePhysicalName, "RecordId", "RecordId",
                    DbType.Decimal, null, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                ds = DataAccessHandler.GetPageRecord(db, "UserAccount", "UserId", dataFileNames, false, true, tableLinks, startPosition,
                    count, whereConditons, ref totalCount);

                ds.Tables[0].Columns[0].Caption = "用户名";
                ds.Tables[0].Columns[1].Caption = "姓名";
                ds.Tables[0].Columns[2].Caption = "单位名称";
                ds.Tables[0].Columns[3].Caption = "用户类型";
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得用户编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public decimal GetUserId(decimal tableId, decimal recordId)
        {
            decimal userId = 0;

            CustomTable customTable = new CustomTable();
            string physicalName = customTable.GetTablePhysicalName(tableId);
            byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string auditedStatusName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
            string sqlUpdate = string.Format("SELECT {0} FROM {1} WHERE {2} = @{2} ", userIdName, physicalName, auditedStatusName, recordIdName);

            try
            {
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    dbBusiness.AddInParameter(dbCommand, recordIdName, DbType.Decimal, recordId);
                    userId = DataConvertionHelper.GetDecimal(dbBusiness.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userId;
        }

        /// <summary>
        /// 针对行复制或是列替换来查询数据
        /// </summary>
        /// <param name="sourceTableId">源数据表编号</param>
        /// <param name="destinationTableId">目的数据表编号</param>
        /// <param name="rowColCopyType">复制类型</param>
        /// <param name="dataFieldRelations">字段关系</param>
        /// <param name="customDataFieldNames">自定义字段关系</param>
        /// <param name="whereClause">查询条件</param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns>获得查询的记录</returns>
        public DataSet GetQueriedData(decimal sourceTableId, decimal destinationTableId, RowColCopyType rowColCopyType, IDictionary<decimal, decimal> dataFieldRelations,
            IDictionary<decimal, string> customDataFieldNames, string whereClause, int startPosition, int count, ref int totalCount)
        {
            DataSet ds = null;

            CustomTable customTable = new CustomTable();
            CustomDataField customDataField = new CustomDataField();
            IDictionary<string, string> physicalAndLogcialNames = new Dictionary<string, string>();
            byte destDataWarehouseId = customTable.GetDataWarehouseId(sourceTableId);
            byte sourceDataWarehouseId = customTable.GetDataWarehouseId(destinationTableId);
            string destDatabaseName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)destDataWarehouseId);
            string sourceDatabaseName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)sourceDataWarehouseId);            
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (rowColCopyType == RowColCopyType.DuplicateRow)
            {
                CustomTableInfo sourceTable = customTable.GetModelInfo(sourceTableId);
                foreach (KeyValuePair<decimal, decimal> kvp in dataFieldRelations)
                {
                    CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(kvp.Key);                    
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                    if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        sb.Append(customDataFieldInfo.PhysicalName);
                    }
                    else
                    {
                        CustomExpression customExpression = new CustomExpression();
                        IList<CommonNode> commonNodes = customExpression.GetCommonNodes(kvp.Key);
                        string expressionDataFieldName = customDataField.GetExpressionDataFieldName(string.Empty, customDataFieldInfo.ExpressionText, commonNodes);
                        sb.AppendFormat("{0} AS {1}", expressionDataFieldName, customDataFieldInfo.PhysicalName);
                    }
                    sb.Append(", ");
                    physicalAndLogcialNames.Add(customDataFieldInfo.PhysicalName, customDataFieldInfo.LogicalName);
                }
                int index = 0;
                foreach (KeyValuePair<decimal, string> kvp in customDataFieldNames)
                {
                    sb.AppendFormat("{0} AS 自定义源值_{1}", kvp.Value, index);
                    sb.Append(", ");
                    index++;
                }
                sb.Remove(sb.Length - 2, 2);
                sb.Append(" FROM ");
                sb.Append(sourceTable.PhysicalName);
                if (!string.IsNullOrEmpty(whereClause))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereClause);
                }
            }
            else
            {
                string sourceTablePhysicalName = customTable.GetTablePhysicalName(sourceTableId);
                IDictionary<SystemTable, TableLink> systemTables = new Dictionary<SystemTable, TableLink>();
                foreach (KeyValuePair<decimal, decimal> kvp in dataFieldRelations)
                {
                    CustomDataFieldInfo sourceCustomDataFieldInfo = null;
                    if (kvp.Key > 0)
                    {
                        sourceCustomDataFieldInfo = customDataField.GetModelInfo(kvp.Key);
                    }
                    else if (kvp.Key < 0)
                    {
                        SystemDataField systemDataField = (SystemDataField)(Convert.ToByte(kvp.Key * -1));
                        sourceCustomDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(sourceTableId, sourceTablePhysicalName, systemDataField);
                        switch (systemDataField)
                        {
                            case SystemDataField.DepName:
                            case SystemDataField.DepCode:
                            case SystemDataField.DepFstAdditionalCode:
                            case SystemDataField.DepScdAdditionalCode:
                            case SystemDataField.DepProperty:
                            case SystemDataField.DepValue:
                                if (!systemTables.ContainsKey(SystemTable.Department))
                                {                                    
                                    if (!systemTables.ContainsKey(SystemTable.User))
                                    {
                                        systemTables.Add(SystemTable.User, new TableLink( sourceTablePhysicalName, "[Blue].[dbo].[UserAccount]", "UserId", TableJoin.InnerJoin));
                                    }
                                    systemTables.Add(SystemTable.Department, new TableLink("[Blue].[dbo].[UserAccount]", "[Blue].[dbo].[CustomDepartment]", "DepId", TableJoin.InnerJoin));
                                }
                                break;

                            case SystemDataField.UserTypeName:
                            case SystemDataField.UserTypeCode:
                                if (!systemTables.ContainsKey(SystemTable.Department))
                                {
                                    if (!systemTables.ContainsKey(SystemTable.User))
                                    {
                                        systemTables.Add(SystemTable.User, new TableLink(sourceTablePhysicalName, "[Blue].[dbo].[UserAccount]", "UserId", TableJoin.InnerJoin));
                                    }
                                    systemTables.Add(SystemTable.UserType, new TableLink("[Blue].[dbo].[UserAccount]", "[Blue].[dbo].[UserType]", "UserTypeId", TableJoin.InnerJoin));
                                }
                                break;
                        }
                        
                    }
                    else
                    {
                        continue;
                    }
                    CustomDataFieldInfo destCustomDataFieldInfo = customDataField.GetModelInfo(kvp.Value);
                    sb.Append(destCustomDataFieldInfo.PhysicalName);
                    physicalAndLogcialNames.Add(destCustomDataFieldInfo.PhysicalName, destCustomDataFieldInfo.LogicalName);
                    sb.Append(", ");
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)sourceCustomDataFieldInfo.DataFieldProperty;
                    if (dataFieldProperty == DataFieldProperty.PhysicalDataField || dataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
                    {
                        sb.Append(sourceCustomDataFieldInfo.PhysicalName);
                    }
                    else
                    {
                        CustomExpression customExpression = new CustomExpression();
                        IList<CommonNode> commonNodes = customExpression.GetCommonNodes(kvp.Key);
                        string expressionDataFieldName = customDataField.GetExpressionDataFieldName(string.Empty, sourceCustomDataFieldInfo.ExpressionText, commonNodes);
                        sb.AppendFormat("{0} AS {1}", expressionDataFieldName, sourceCustomDataFieldInfo.PhysicalName);
                    }
                    sb.Append(", ");
                    int pos = sourceCustomDataFieldInfo.PhysicalName.IndexOf('.');
                    string physicalName = string.Empty;
                    if (pos > 0)
                    {
                        physicalName = sourceCustomDataFieldInfo.PhysicalName.Substring(pos + 2, sourceCustomDataFieldInfo.PhysicalName.Length - pos - 3);
                    }
                    else
                    {
                        physicalName = sourceCustomDataFieldInfo.PhysicalName;
                    }
                    physicalAndLogcialNames.Add(physicalName, sourceCustomDataFieldInfo.LogicalName);
                }
                int idx = 1;
                foreach (KeyValuePair<decimal, string> kvp in customDataFieldNames)
                {
                    CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(kvp.Key);
                    sb.Append(customDataFieldInfo.PhysicalName);
                    sb.Append(", ");
                    physicalAndLogcialNames.Add(customDataFieldInfo.PhysicalName, customDataFieldInfo.LogicalName);
                    sb.AppendFormat("{0} AS 自定义源值_{1}", kvp.Value, idx);
                    sb.Append(", ");
                    idx++;
                }
                sb.Remove(sb.Length - 2, 2);                
                sb.Append(" FROM ");
                if (systemTables.Count > 0)
                {
                    IList<TableLink> tables = new List<TableLink>();
                    foreach (KeyValuePair<SystemTable, TableLink> keyValue in systemTables)
                    {
                        tables.Add(keyValue.Value);
                    }
                    sb.Append(DataAccessHandler.GetTableNames(sourceTablePhysicalName, tables));
                }
                else
                {
                    if (sourceDataWarehouseId != destDataWarehouseId)
                    {
                        sb.AppendFormat("{0}.dbo.{1}", sourceDatabaseName, sourceTablePhysicalName);
                    }
                    else
                    {
                        sb.Append(sourceTablePhysicalName);
                    }
                }

                if (sourceTableId != destinationTableId)
                {
                    string destinationTablePhysicalName = customTable.GetTablePhysicalName(destinationTableId);
                    sb.Append(" INNER JOIN ");
                    if (sourceDataWarehouseId != destDataWarehouseId)
                    {
                        sb.AppendFormat("{0}.dbo.{1}", destDatabaseName, destinationTablePhysicalName);
                    }
                    else
                    {
                        sb.Append(destinationTablePhysicalName);
                    }
                    sb.Append(" ON ");
                    if (sourceDataWarehouseId != destDataWarehouseId)
                    {
                        sb.AppendFormat("{0}.dbo.{1}", sourceDatabaseName, sourceTablePhysicalName);
                    }
                    else
                    {
                        sb.Append(sourceTablePhysicalName);
                    }
                    sb.Append(".UserId = ");
                    if (sourceDataWarehouseId != destDataWarehouseId)
                    {
                        sb.AppendFormat("{0}.dbo.{1}", destDatabaseName, destinationTablePhysicalName);
                    }
                    else
                    {
                        sb.Append(destinationTablePhysicalName);
                    }
                    sb.Append(".UserId");
                }
                if (!string.IsNullOrEmpty(whereClause))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereClause);
                }
            }
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)sourceDataWarehouseId));
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    //第一步：建立临时表和插入数据                       
                    StringBuilder sbInsert = new StringBuilder();
                    sbInsert.AppendFormat("SELECT IDENTITY(int, 1, 1) AS RecordSerial, * INTO #TEMP FROM ({0}) AS T", sb);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sbInsert.ToString()))
                    {
                        totalCount = db.ExecuteNonQuery(dbCommand, transaction);
                    }
                    //第二步：查询语句
                    StringBuilder sbSelect = new StringBuilder();
                    sbSelect.Append("SELECT * FROM #TEMP ");
                    if (count > 0)
                    {
                        sbSelect.Append("WHERE RecordSerial >= @RecordSerialFst AND RecordSerial <= @RecordSerialScd ");
                    }

                    using (DbCommand dbCommandFst = db.GetSqlStringCommand(sbSelect.ToString()))
                    {
                        if (count > 0)
                        {
                            db.AddInParameter(dbCommandFst, "RecordSerialFst", DbType.Int32, startPosition + 1);
                            db.AddInParameter(dbCommandFst, "RecordSerialScd", DbType.Int32, startPosition + count);
                        }
                        ds = db.ExecuteDataSet(dbCommandFst, transaction);
                    }
                    ds.Tables[0].Columns.Remove("RecordSerial");

                    //第三步：删除临时表   
                    string sqlDelete = "DROP TABLE #TEMP";
                    using (DbCommand dbCommandScd = db.GetSqlStringCommand(sqlDelete))
                    {
                        db.ExecuteNonQuery(dbCommandScd, transaction);
                    }
                    transaction.Commit();

                    foreach (DataColumn dc in ds.Tables[0].Columns)
                    {
                        if (physicalAndLogcialNames.ContainsKey(dc.ColumnName))
                        {
                            dc.Caption = physicalAndLogcialNames[dc.ColumnName];
                        }
                    }
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return ds;
        }

        /// <summary>
        /// 批量复制数据
        /// </summary>
        /// <param name="sourceTableId">源数据表编号</param>
        /// <param name="destinationTableId">目的数据表编号</param>
        /// <param name="dataFieldRelations">字段关系</param>
        /// <param name="customDataFieldNames">自定义字段关系</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns>导入的记录数</returns>
        public int Import(decimal sourceTableId, decimal destinationTableId, IDictionary<decimal, decimal> dataFieldRelations,
            IDictionary<decimal, string> customDataFieldNames, string whereClause)
        {
            int count = 0;

            CustomTable customTable = new CustomTable();
            CustomDataField customDataField = new CustomDataField();
            CustomTableInfo sourceTable = customTable.GetModelInfo(sourceTableId);
            CustomTableInfo destinationTable = customTable.GetModelInfo(destinationTableId);
            byte destDataWarehouseId = customTable.GetDataWarehouseId(sourceTableId);
            byte sourceDataWarehouseId = customTable.GetDataWarehouseId(destinationTableId);
            string dataSourceName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)destDataWarehouseId);
            string sourceName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)sourceDataWarehouseId);

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO ");
            sb.AppendFormat("{0}.dbo.{1}", dataSourceName, destinationTable.PhysicalName);
            sb.Append("(UserId, UserName, DepId, UserTypeId, RecordSorting, CreationTime, ModificationTime, AuditedStatus, CurrentState, IsDeleted");
            foreach (KeyValuePair<decimal, decimal> kvp in dataFieldRelations)
            {
                sb.Append(", ");
                sb.Append(customDataField.GetPhysicalName(kvp.Value));
            }
            foreach (KeyValuePair<decimal, string> kvp in customDataFieldNames)
            {
                sb.Append(", ");
                sb.Append(customDataField.GetPhysicalName(kvp.Key));
            }
            sb.Append(") SELECT UserId, UserName, DepId, UserTypeId, RecordSorting, CreationTime, @ModificationTime, AuditedStatus, @CurrentState, @IsDeleted");
            DateTime dateTime = DateTime.Parse(AppSettingHelper.YearMonthDay);
            foreach (KeyValuePair<decimal, decimal> kvp in dataFieldRelations)
            {
                sb.Append(", ");
                CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(kvp.Key);
                if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                {
                    sb.Append(customDataFieldInfo.PhysicalName);
                }
                else
                {
                    CustomExpression customExpression = new CustomExpression();
                    IList<CommonNode> commonNodes = customExpression.GetCommonNodes(kvp.Key);
                    string expressionDataFieldName = customDataField.GetExpressionDataFieldName(string.Empty, customDataFieldInfo.ExpressionText, commonNodes);
                    sb.Append(expressionDataFieldName);
                }
            }
            foreach (KeyValuePair<decimal, string> kvp in customDataFieldNames)
            {
                sb.Append(", ");
                sb.Append(kvp.Value);
            }
            sb.Append(" FROM ");
            sb.AppendFormat("{0}.dbo.{1}", sourceName, sourceTable.PhysicalName);
            if (!string.IsNullOrEmpty(whereClause))
            {
                sb.Append(" WHERE ");
                sb.Append(whereClause);
            }
            try
            {
                Database db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)destDataWarehouseId));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ModificationTime", DbType.DateTime, dateTime);
                    db.AddInParameter(dbCommand, "CurrentState", DbType.Byte, (byte)CurrentState.History);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, false);
                    count = db.ExecuteNonQuery(dbCommand);
                }
                UpdateSorting(db, destinationTable.PhysicalName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 字段数据替换
        /// </summary>
        /// <param name="sourceTableId">源数据表编号</param>
        /// <param name="destinationTableId">目的数据表编号</param>
        /// <param name="dataFieldRelations">字段关系</param>
        /// <param name="customDataFieldNames">自定义字段关系，key是源字段，value是目的字段</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns>被替换的记录数目</returns>
        public int Update(decimal sourceTableId, decimal destinationTableId, IDictionary<decimal, decimal> dataFieldRelations,
            IDictionary<decimal, string> customDataFieldNames, string whereClause)
        {
            CustomTable customTable = new CustomTable();
            CustomDataField customDataField = new CustomDataField();
            int count = 0;
            StringBuilder sb = new StringBuilder();
            string destinationTablePhysicalName = customTable.GetTablePhysicalName(destinationTableId);
            byte destDataWarehouseId = customTable.GetDataWarehouseId(sourceTableId);
            byte sourceDataWarehouseId = customTable.GetDataWarehouseId(destinationTableId);
            string destDatabaseName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)destDataWarehouseId);
            string sourceDatabaseName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)sourceDataWarehouseId);

            sb.AppendFormat("UPDATE {0} SET {1} = @{1}", destinationTablePhysicalName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime));
            foreach (KeyValuePair<decimal, decimal> kvp in dataFieldRelations)
            {
                sb.Append(", ");
                sb.Append(customDataField.GetPhysicalName(kvp.Value));
                sb.Append(" = ");
                CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(kvp.Key);
                if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                {
                    sb.Append(customDataFieldInfo.PhysicalName);
                }
                else
                {
                    CustomExpression customExpression = new CustomExpression();
                    IList<CommonNode> commonNodes = customExpression.GetCommonNodes(kvp.Key);
                    string expressionDataFieldName = customDataField.GetExpressionDataFieldName(string.Empty, customDataFieldInfo.ExpressionText, commonNodes);
                    sb.Append(expressionDataFieldName);
                }
            }
            foreach (KeyValuePair<decimal, string> kvp in customDataFieldNames)
            {
                sb.Append(", ");
                sb.Append(customDataField.GetPhysicalName(kvp.Key));
                sb.Append(" = ");
                sb.Append(kvp.Value);
            }
            if (sourceTableId != destinationTableId)
            {
                string sourceTablePhysicalName = customTable.GetTablePhysicalName(sourceTableId);
                sb.Append(" FROM ");
                if (sourceDataWarehouseId != destDataWarehouseId)
                {
                    sb.AppendFormat("{0}.dbo.{1}", sourceDatabaseName, sourceTablePhysicalName);
                }
                else
                {
                    sb.Append(sourceTablePhysicalName);
                }
                sb.Append(" INNER JOIN ");
                if (sourceDataWarehouseId != destDataWarehouseId)
                {
                    sb.AppendFormat("{0}.dbo.{1}", destDatabaseName, destinationTablePhysicalName);
                }
                else
                {
                    sb.Append(destinationTablePhysicalName);
                }
                sb.Append(" ON ");
                if (sourceDataWarehouseId != destDataWarehouseId)
                {
                    sb.AppendFormat("{0}.dbo.{1}", sourceDatabaseName, sourceTablePhysicalName);
                }
                else
                {
                    sb.Append(sourceTablePhysicalName);
                }
                sb.Append(".UserId = ");
                if (sourceDataWarehouseId != destDataWarehouseId)
                {
                    sb.AppendFormat("{0}.dbo.{1}", destDatabaseName, destinationTablePhysicalName);
                }
                else
                {
                    sb.Append(destinationTablePhysicalName);
                }
                sb.Append(".UserId");
            }
            if (!string.IsNullOrEmpty(whereClause))
            {
                sb.Append(" WHERE ");
                sb.Append(whereClause);
            }
            try
            {
                Database db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)destDataWarehouseId));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime), DbType.DateTime, DateTime.Now);
                    count = db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            return count;
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="transaction"></param>
        public void UpdateSorting(Database db, string tablePhysicalName)
        {
            /* 1.获得每个用户的记录排序最大值 */
            IDictionary<decimal, int> userIdAndSotring = new Dictionary<decimal, int>();
            DateTime dateTime = DateTime.Parse(AppSettingHelper.YearMonthDay);
            string sqlSelect = string.Format("SELECT {0}, MAX({1}) AS Sorting FROM {2} GROUP BY {0} HAVING {0} IN (SELECT {0} FROM {2} WHERE {3} = @ConditioalDateTime)",
                DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordSorting),
                tablePhysicalName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime));
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "ConditioalDateTime", DbType.DateTime, dateTime);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        userIdAndSotring.Add(DataConvertionHelper.GetDecimal(dataReader[0]), DataConvertionHelper.GetInt(dataReader[1]));
                    }
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                }
            }
            /* 2.更新记录 */
            try
            {
                string sqlUpdate = string.Format("SELECT {0}, {1}, {2} FROM {3} WHERE {4} = @ConditioalDateTime",
                     DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId),
                     DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordSorting), tablePhysicalName,
                     DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    dbCommand.Connection = db.CreateConnection();
                    db.AddInParameter(dbCommand, "ConditioalDateTime", DbType.DateTime, dateTime);
                    DbDataAdapter adapter = db.GetDataAdapter();
                    adapter.SelectCommand = dbCommand;
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    foreach (DataRow dr in dt.Rows)
                    {
                        decimal userId = (decimal)dr[1];
                        int sorting = 1;
                        if (userIdAndSotring.ContainsKey(userId))
                        {
                            sorting = userIdAndSotring[userId] + 1;
                            userIdAndSotring[userId]++;
                        }
                        dr[2] = sorting;
                    }
                    if (dt.Rows.Count > 0)
                    {
                        SqlCommandBuilder sqlBulider = new SqlCommandBuilder((SqlDataAdapter)adapter);
                        //执行更新
                        adapter.Update(dt.GetChanges());
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
        /// 删除上传的文件
        /// </summary>
        /// <param name="dataFieldName"></param>
        /// <param name="fileName"></param>
        public void DeleteUploadFiles(string dataFieldName, string fileName)
        {
            try
            {
                StringBuilder sbPath = new StringBuilder();
                sbPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
                if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
                {
                    sbPath.Append(@"\");
                }
                sbPath.AppendFormat(@"{0}\{1}\", AppSettingHelper.DefaultSubDirOfUploadFiles, dataFieldName);
                sbPath.Append(fileName);
                if (File.Exists(sbPath.ToString()))
                {
                    File.Delete(sbPath.ToString());
                }
            }
            catch { }
        }

        /// <summary>
        /// 上传文件复制
        /// </summary>
        /// <param name="sourceDataFieldName"></param>
        /// <param name="destDataFieldName"></param>
        /// <param name="fileName"></param>
        public void CopyUploadFiles(string sourceDataFieldName, string destDataFieldName, string fileName)
        {
            try
            {
                StringBuilder sbSourcePath = new StringBuilder();
                StringBuilder sbDestPath = new StringBuilder();
                sbSourcePath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
                sbDestPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
                if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
                {
                    sbSourcePath.Append(@"\");
                    sbDestPath.Append(@"\");
                }
                sbSourcePath.AppendFormat(@"{0}\{1}\", AppSettingHelper.DefaultSubDirOfUploadFiles, sourceDataFieldName);
                sbDestPath.AppendFormat(@"{0}\{1}\", AppSettingHelper.DefaultSubDirOfUploadFiles, destDataFieldName);
                sbSourcePath.Append(fileName);
                if (!Directory.Exists(sbDestPath.ToString()))
                {
                    Directory.CreateDirectory(sbDestPath.ToString());
                }
                sbDestPath.Append(fileName);
                if (File.Exists(sbSourcePath.ToString()))
                {
                    File.Copy(sbSourcePath.ToString(), sbDestPath.ToString(), true);
                }
            }
            catch { }
        }

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetTableRecordCount(decimal tableId, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                CustomTable customTable = new CustomTable();
                byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                string key = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
                SqlDatabase db = DataAccessHelper.GetDatabase(key);
                string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT COUNT(1) FROM {0} ", tablePhysicalName);
                if (whereConditons != null && whereConditons.Count > 0)
                {
                    string condition = DataAccessHandler.GetConditionSentence(whereConditons);
                    sb.AppendFormat("WHERE {0} ", condition);
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if (whereConditons != null && whereConditons.Count > 0)
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    count = (int)db.ExecuteScalar(dbCommand);
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
        /// 获得表 CustomTable 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="tableId">表的编号</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(decimal tableId, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                CustomTable customTable = new CustomTable();
                string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
                CustomDataField customDataField = new CustomDataField();
                IList<CustomDataFieldInfo> customDataFieldInfos = customDataField.GetModelInfos(tableId);
                IDictionary<string, string> captions = new Dictionary<string, string>(customDataFieldInfos.Count + 1);
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}, ", DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.RecordId));
                sb.AppendFormat("{0}.{1}, ", tablePhysicalName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.UserName));
                sb.AppendFormat("{0}, ", DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.UserActualName));
                captions.Add(DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.UserActualName), UserEnumHelper.GetEnumText(SystemDataField.UserActualName));
                sb.AppendFormat("{0}, ", DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.DepName));
                captions.Add(DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.DepName), UserEnumHelper.GetEnumText(SystemDataField.DepName));
                sb.AppendFormat("{0}, ", DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.UserTypeName));
                captions.Add(DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.UserTypeName), UserEnumHelper.GetEnumText(SystemDataField.UserTypeName));
                sb.AppendFormat("{0}, ", DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.AuditedStatus));
                sb.Append(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.CurrentState));

                foreach (CustomDataFieldInfo customDataFieldInfo in customDataFieldInfos)
                {
                    sb.Append(", ");
                    if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        sb.Append(customDataFieldInfo.PhysicalName);
                    }
                    else
                    {
                        string expressionDataFieldName = customDataField.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId);
                        sb.AppendFormat("{0} AS {1}", expressionDataFieldName, customDataFieldInfo.PhysicalName);
                    }
                    captions.Add(customDataFieldInfo.PhysicalName, customDataFieldInfo.LogicalName);
                }
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(DataFieldHelper.GetTableLink(tablePhysicalName, SystemDataField.UserActualName));
                tableLinks.Add(DataFieldHelper.GetTableLink(tablePhysicalName, SystemDataField.UserTypeName));
                tableLinks.Add(DataFieldHelper.GetTableLink(tablePhysicalName, SystemDataField.DepName));
                string name = DataAccessHandler.GetTableNames(tablePhysicalName, tableLinks);
                byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                ds = DataAccessHandler.GetPageRecord(db, tablePhysicalName, recordIdName, sb.ToString(), false, false, tableLinks, startPosition,
                count, whereConditons, ref totalCount);
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.ExtendedProperties.Add(dataColumn.ColumnName, captions[dataColumn.ColumnName]);
                    dataColumn.Caption = captions[dataColumn.ColumnName];
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
        /// 获得表 CustomTable 的分页数据集
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="extendedCustomDataFieldInfos"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal tableId, IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos, int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;

            try
            {
                CustomTable customTable = new CustomTable();
                string tablePhysicalName = customTable.GetTablePhysicalName(tableId);                
                IDictionary<string, string> captions = new Dictionary<string, string>(extendedCustomDataFieldInfos.Count + 1);
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}.{1}, ", tablePhysicalName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.UserName));
                sb.AppendFormat("{0}, ", DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.AuditedStatus));
                sb.Append(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.CurrentState));

                foreach (ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
                {
                    sb.Append(", ");
                    if ((DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        sb.Append(extendedCustomDataFieldInfo.PhysicalName);
                    }
                    else
                    {
                        sb.AppendFormat("{0} AS {1}", extendedCustomDataFieldInfo.PhysicalName, extendedCustomDataFieldInfo.Name);
                    }
                    captions.Add(extendedCustomDataFieldInfo.Name, extendedCustomDataFieldInfo.LogicalName);
                }                
                byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                ds = DataAccessHandler.GetPageRecord(db, tablePhysicalName, sb.ToString(), false, null, startPosition, count, whereConditons, sortingCondtions);
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.ExtendedProperties.Add(dataColumn.ColumnName, captions[dataColumn.ColumnName]);
                    dataColumn.Caption = captions[dataColumn.ColumnName];
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
        /// 更新当前状态查询条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="where"></param>
        /// <param name="currentState"></param>
        /// <returns></returns>
        public int UpdateCurrentState(decimal tableId, string where, CurrentState currentState)
        {
            int count = 0;

            try
            {
                string currentStateName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState);
                CustomTable customTable = new CustomTable();
                string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
                CustomDataField customDataField = new CustomDataField();
                byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                string update = string.Format("UPDATE {0} SET {1} = @{1} WHERE {2}", tablePhysicalName, currentStateName, where);
                using (DbCommand dbCommand = db.GetSqlStringCommand(update))
                {
                    db.AddInParameter(dbCommand, currentStateName, DbType.Byte, (byte)currentState);
                    count = db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }            

        /// <summary>
        /// 获得表 CustomTable 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="tableId">表的编号</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="where">查询字段条件</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecordByTableId(decimal tableId, int startPosition, int count, string where, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                CustomTable customTable = new CustomTable();
                string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
                CustomDataField customDataField = new CustomDataField();
                IList<CustomDataFieldInfo> customDataFieldInfos = customDataField.GetModelInfos(tableId);
                IDictionary<string, string> captions = new Dictionary<string, string>(customDataFieldInfos.Count + 1);
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}, ", DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.RecordId));
                sb.AppendFormat("{0}.{1}, ", tablePhysicalName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.UserName));
                sb.AppendFormat("{0}, ", DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.UserActualName));
                captions.Add(DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.UserActualName), UserEnumHelper.GetEnumText(SystemDataField.UserActualName));
                sb.AppendFormat("{0}, ", DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.DepName));
                captions.Add(DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.DepName), UserEnumHelper.GetEnumText(SystemDataField.DepName));
                sb.AppendFormat("{0}, ", DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.UserTypeName));
                captions.Add(DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.UserTypeName), UserEnumHelper.GetEnumText(SystemDataField.UserTypeName));
                sb.AppendFormat("{0}, ", DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.AuditedStatus));
                sb.Append(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.CurrentState));

                foreach (CustomDataFieldInfo customDataFieldInfo in customDataFieldInfos)
                {
                    sb.Append(", ");
                    if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        sb.Append(customDataFieldInfo.PhysicalName);
                    }
                    else
                    {
                        string expressionDataFieldName = customDataField.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId);
                        sb.AppendFormat("{0} AS {1}", expressionDataFieldName, customDataFieldInfo.PhysicalName);
                    }
                    captions.Add(customDataFieldInfo.PhysicalName, customDataFieldInfo.LogicalName);
                }
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(DataFieldHelper.GetTableLink(tablePhysicalName, SystemDataField.UserActualName));
                tableLinks.Add(DataFieldHelper.GetTableLink(tablePhysicalName, SystemDataField.UserTypeName));
                tableLinks.Add(DataFieldHelper.GetTableLink(tablePhysicalName, SystemDataField.DepName));
                string name = DataAccessHandler.GetTableNames(tablePhysicalName, tableLinks);
                byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                StringBuilder select = new StringBuilder();
                select.AppendFormat("SELECT COUNT(1) FROM {0} ", tablePhysicalName);
                if (!string.IsNullOrEmpty(where))
                {
                    select.AppendFormat("WHERE {0}", where);
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(select.ToString()))
                {
                    totalCount = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                }
                StringBuilder sbSelect = new StringBuilder();
                //构造查询语句
                if (startPosition == 0)
                {
                    sbSelect.AppendFormat("SELECT TOP {0} {1} FROM {2} ", count, sb, name); 
                    if (!string.IsNullOrEmpty(where))
                    {
                        sbSelect.AppendFormat(" WHERE {0} ", where);
                    }
                    sbSelect.AppendFormat(" ORDER BY {0} ASC",  recordIdName);                    
                }
                else
                {
                    sbSelect.AppendFormat("SELECT TOP {0} {1} FROM {2} WHERE ", count, sb, name);
                    if (!string.IsNullOrEmpty(where))
                    {
                        sbSelect.AppendFormat("{0} AND ", where);
                    }
                    sbSelect.AppendFormat(" {0} > (SELECT MAX({0}) FROM (SELECT TOP {1} {0} FROM {2} ", recordIdName, startPosition, name);
                   
                    if (!string.IsNullOrEmpty(where))
                    {
                        sbSelect.AppendFormat(" WHERE {0} ", where);
                    }
                    sbSelect.AppendFormat(" ORDER BY {0} ASC) AS T) ORDER BY {0} ASC", recordIdName);                    
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sbSelect.ToString()))
                {
                    ds = db.ExecuteDataSet(dbCommand);
                }
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.ExtendedProperties.Add(dataColumn.ColumnName, captions[dataColumn.ColumnName]);
                    dataColumn.Caption = captions[dataColumn.ColumnName];
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
        /// 获得系统表分页数据集
        /// </summary>
        /// <param name="systemTableName">系统表</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>数据集</returns>
        public DataSet GetAuthorizedData(SystemTable systemTableName, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                SqlDatabase db = DataAccessHelper.GetDatabase();
                if (sortingCondtions == null || sortingCondtions.Count == 0)
                {
                    ds = DataAccessHandler.GetPageRecord(db, SystemTableHelper.GetSystemTablePhysicalName(systemTableName),
                        SystemTableHelper.GetSystemTableKeyName(systemTableName), "*", false, true, startPosition,
                        count, whereConditons, ref totalCount);
                }
                else
                {
                    ds = DataAccessHandler.GetPageRecord(db, SystemTableHelper.GetSystemTablePhysicalName(systemTableName),
                        SystemTableHelper.GetSystemTableKeyName(systemTableName), "*", false, true, startPosition,
                        count, whereConditons, sortingCondtions, ref totalCount);
                }                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

    }
}
