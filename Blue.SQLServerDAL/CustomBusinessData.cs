//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomBusinessData.cs
// 描述: CustomBusinessData 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/07/18
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using AppFramework.Core;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL;
using Blue.SQLServerDAL.BusinessModule;

namespace Blue.SQLServerDAL
{
    /// <summary>
    /// CustomBusinessData 数据层访问类：自定义数据访问类
    /// </summary>
    public class CustomBusinessData : ICustomBusinessData
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomBusinessData()
        {
        }

        #endregion

        #region 接口

        /// <summary>
        /// 获得满足条件的值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        public object GetValue(string tableName, string dataFieldName, SQLServerFunction functionName)
        {
            object obj = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            switch (functionName)
            {
                case SQLServerFunction.Max:
                    sb.AppendFormat("MIN({0}) ", dataFieldName);
                    break;

                case SQLServerFunction.Min:
                    sb.AppendFormat("MAX({0}) ", dataFieldName);
                    break;
            }

            sb.AppendFormat("FROM {0} ", tableName);

            try
            {
                CustomTable customTable = new CustomTable();
                decimal tableId = customTable.GetTableId(tableName);
                //SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)customTable.GetDataWarehouseId(tableId)));
                // 过时的方法
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetOldDataSourceName((DataWarehouse)customTable.GetDataWarehouseIdByTableId(tableId)));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    obj = db.ExecuteScalar(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return obj;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        public void ImportData(decimal tableId, DataTable dataTable)
        {
            ImportData(tableId, dataTable, null);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        public void ImportData(decimal tableId, DataTable dataTable, Dictionary<string, string> dataFieldRelation)
        {
            CustomTable customTable = new CustomTable();
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);            
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)customTable.GetDataWarehouseId(tableId)));            
            try
            {
                using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(db.ConnectionString, SqlBulkCopyOptions.UseInternalTransaction))
                {
                    sqlBulkCopy.DestinationTableName = tablePhysicalName;
                    sqlBulkCopy.BulkCopyTimeout = 900;
                    if (dataFieldRelation != null)
                    {
                        foreach (KeyValuePair<string, string> keyValue in dataFieldRelation)
                        {
                            sqlBulkCopy.ColumnMappings.Add(keyValue.Key, keyValue.Value);
                        }
                    }
                    else
                    {
                        foreach (DataColumn dc in dataTable.Columns)
                        {
                            sqlBulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                        }
                    }

                    sqlBulkCopy.WriteToServer(dataTable);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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

            CustomTable customTable = new CustomTable();
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)customTable.GetDataWarehouseId(tableId)));
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);

            count = GetTableRecordCount(db, tablePhysicalName, whereConditons);

            return count;
        }

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableLinks"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetTableRecordCount(decimal tableId, IList<TableLink> tableLinks, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                CustomTable customTable = new CustomTable();
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)customTable.GetDataWarehouseId(tableId)));
                string tableName = customTable.GetTablePhysicalName(tableId);
                string tableNames = DataAccessHandler.GetTableNames(tableName, tableLinks);

                count = GetTableRecordCount(db, tableNames, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }        

        /// <summary>
        /// 清除表中所有数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int TruncateTable(decimal tableId)
        {
            int count = 0;

            CustomTable customTable = new CustomTable();
            int dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tableName = customTable.GetTablePhysicalName(tableId);

            string sqlClear = string.Format("truncate table {0} ", tableName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlClear))
            {
                count = db.ExecuteNonQuery(dbCommand);
            }

            return count;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        private int GetTableRecordCount(SqlDatabase db, string tableName, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT COUNT(1) FROM {0} ", tableName);
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

        #endregion

        #region 过时的函数

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetTableCount(decimal tableId, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            CustomTable customTable = new CustomTable();
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetOldDataSourceName((DataWarehouse)customTable.GetDataWarehouseIdByTableId(tableId)));
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);

            count = GetTableRecordCount(db, tablePhysicalName, whereConditons);

            return count;
        }

        #endregion
        
    }
}
