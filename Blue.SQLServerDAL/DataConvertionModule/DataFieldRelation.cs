//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataFieldRelation.cs
// 描述: DataFieldRelation 数据层访问类
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
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.DataConvertionModule;
using Blue.Model.DataConvertionModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.BusinessModule;

namespace Blue.SQLServerDAL.DataConvertionModule
{
    /// <summary>
    /// DataFieldRelation 表的数据层访问类
    /// </summary>
    public class DataFieldRelation : IDataFieldRelation
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public DataFieldRelation()
		{
		}

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 DataFieldRelation 表中插入一条新记录
        /// </summary>
        /// <param name="dataFieldRelationInfo"></param>
        public void Insert(DataFieldRelationInfo dataFieldRelationInfo)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            try
            {
                Insert(dataFieldRelationInfo, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 DataFieldRelationInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="relationId">关系编号</param>
        /// <returns> DataFieldRelationInfo 对象</returns>
        public DataFieldRelationInfo GetModelInfo(decimal dataFieldId, decimal parentDataFieldId, decimal relationId)
		{			
			DataFieldRelationInfo  dataFieldRelationInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("DataFieldId", "DataFieldId", DbType.Decimal, dataFieldId, DataFieldCondition.Equal));

            //创建集合对象
            IList<DataFieldRelationInfo>  dataFieldRelationInfos = GetModelInfos(whereConditons, null, true);
            if (dataFieldRelationInfos != null && dataFieldRelationInfos.Count > 0)
            {
                dataFieldRelationInfo = dataFieldRelationInfos[0];
            }

            return dataFieldRelationInfo;            
		}
        
        /// <summary>
		/// 更新 DataFieldRelationInfo 对象
		/// </summary>
		/// <param name="dataFieldRelationInfo">DataFieldRelationInfo 对象</param>
		public void Update(DataFieldRelationInfo dataFieldRelationInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE DataFieldRelation SET RelationTime = @RelationTime ");
			sb.Append("WHERE DataFieldId = @DataFieldId AND ParentDataFieldId = @ParentDataFieldId AND RelationId = @RelationId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldRelationInfo.DataFieldId);
					db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, dataFieldRelationInfo.ParentDataFieldId);
					db.AddInParameter(dbCommand, "RelationId", DbType.Decimal, dataFieldRelationInfo.RelationId);
					db.AddInParameter(dbCommand, "RelationTime", DbType.DateTime, dataFieldRelationInfo.RelationTime);
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
		///  删除 DataFieldRelationInfo 对象
		/// </summary>
	    ///<param name="dataFieldId">字段编号</param>
		///<param name="parentDataFieldId">字段编号</param>
		///<param name="relationId">关系编号</param>
		public void Delete(decimal dataFieldId, decimal parentDataFieldId, decimal relationId)
		{
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
                Delete(dataFieldId, parentDataFieldId, relationId, db, null);
            }
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }		
		}       
        
        /// <summary>
		/// 获得 DataFieldRelationInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataFieldRelationInfo 对象列表</returns>
		public IList<DataFieldRelationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 DataFieldRelation 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>DataFieldRelationInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "DataFieldRelation ", "DataFieldId", false, whereConditons);
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
        /// 数据转表
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="whereConditons"></param>
        public void Import(decimal relationId, IList<WhereConditon> whereConditons)
        {            
            try
            {
                DataRelation dataRelation = new DataRelation();
                DataRelationInfo dataRelationInfo = dataRelation.GetModelInfo(relationId);
                DataRelationType dataRelationType = (DataRelationType)dataRelationInfo.DataRelationType;
                Dictionary<decimal, decimal> tableRelation = GetTableRelation(relationId);
                if (tableRelation.Count == 0)
                {
                    return;
                }               
                CustomTable customTable = new CustomTable();
                CustomDataField customDataField = new CustomDataField();
                decimal sourceDataWarehouseId = 0;
                decimal destinationDataWarehouseId = 0;
                foreach (KeyValuePair<decimal, decimal> keyValue in tableRelation)
                {
                    sourceDataWarehouseId = customTable.GetDataWarehouseId(keyValue.Key);
                    destinationDataWarehouseId = customTable.GetDataWarehouseId(keyValue.Value);
                    break;
                }
                if (sourceDataWarehouseId <= 0 || destinationDataWarehouseId <= 0)
                {
                    return;
                }
                string sourceDatabaseName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)sourceDataWarehouseId);
                string destinationDatabaseName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)destinationDataWarehouseId);
                DateTime dateTime = DateTime.Now;
                DataBussiness dataBussiness = new DataBussiness();
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase(destinationDatabaseName);
                foreach (KeyValuePair<decimal, decimal> keyValue in tableRelation)
                {
                    string sourceTableName = customTable.GetTablePhysicalName(keyValue.Key);
                    StringBuilder sb = new StringBuilder();
                    sb.Append("SELECT ");
                    IDictionary<decimal, decimal> dataFieldRelation = GetDataFieldRelationByAttachment(relationId, keyValue.Key, keyValue.Value);
                    List<string> destDataFieldNames = new List<string>();
                    foreach (KeyValuePair<decimal, decimal> keyValueDataField in dataFieldRelation)
                    {
                        string sourceDataFieldName = customDataField.GetPhysicalName(keyValueDataField.Key);
                        string destDataFieldName = customDataField.GetPhysicalName(keyValueDataField.Value);
                        sb.AppendFormat("{0}, ", sourceDataFieldName);
                        destDataFieldNames.Add(destDataFieldName);
                    }
                    if (dataFieldRelation.Count > 0)
                    {
                        sb.Remove(sb.Length - 2, 2);
                    }
                    sb.AppendFormat(" FROM {0}", sourceTableName);
                    if (whereConditons != null && whereConditons.Count > 0)
                    {
                        sb.AppendFormat(" WHERE {0}", DataAccessHandler.GetConditionSentence(whereConditons));
                    }
                    if (dataFieldRelation.Count > 0)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            //给参数赋值
                            if (whereConditons != null && whereConditons.Count > 0)
                            {
                                DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                            }
                            DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];
                            foreach (DataRow dr in dt.Rows)
                            {
                                for (int i = 0; i < dt.Columns.Count; i++)
                                {
                                    string fileName = DataConvertionHelper.GetString(dr[i]);
                                    if (dataRelationType == DataRelationType.ImportAndDelete || dataRelationType == DataRelationType.OnlyImport)
                                    {
                                        dataBussiness.CopyUploadFiles(dt.Columns[i].ColumnName, destDataFieldNames[i], fileName);
                                    }
                                    if (dataRelationType == DataRelationType.ImportAndDelete || dataRelationType == DataRelationType.OnlyDelete)
                                    {
                                        int count = 0;
                                        //查询语句
                                        string sqlSelect = string.Format("SELECT COUNT(1) FROM {0} WHERE {1} = @{1}", sourceTableName, dt.Columns[i].ColumnName);
                                        using (DbCommand cmd = db.GetSqlStringCommand(sqlSelect))
                                        {
                                            //给参数赋值
                                            db.AddInParameter(cmd, dt.Columns[i].ColumnName, DbType.String, fileName);
                                            using (IDataReader dataReader = db.ExecuteReader(cmd))
                                            {
                                                count = DataConvertionHelper.GetInt(db.ExecuteScalar(cmd));
                                            }
                                        }
                                        if (count <= 1)
                                        {
                                            dataBussiness.DeleteUploadFiles(dt.Columns[i].ColumnName, fileName);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        /* 导入对应的表的字段数据 */
                        foreach (KeyValuePair<decimal, decimal> keyValue in tableRelation)
                        {
                            string sourceTableName = customTable.GetTablePhysicalName(keyValue.Key);
                            string destinationTableName = customTable.GetTablePhysicalName(keyValue.Value);
                            if (dataRelationType == DataRelationType.ImportAndDelete || dataRelationType == DataRelationType.OnlyImport)
                            {
                                StringBuilder sqlInsert = new StringBuilder();
                                StringBuilder sb = new StringBuilder();
                                sqlInsert.Append("INSERT INTO ");
                                //不同数据库表之间的数据转换
                                if (sourceDataWarehouseId != destinationDataWarehouseId)
                                {
                                    sqlInsert.Append(destinationDatabaseName);
                                    sqlInsert.Append(".dbo.");
                                }
                                sqlInsert.Append(destinationTableName);
                                sqlInsert.Append("(UserId, UserName, DepId, UserTypeId, RecordSorting, AuditedStatus, CurrentState, CreationTime, ModificationTime, IsDeleted ");
                                IDictionary<decimal, decimal> dataFieldRelation = GetDataFieldRelation(relationId, keyValue.Key, keyValue.Value);
                                foreach (KeyValuePair<decimal, decimal> keyValueDataField in dataFieldRelation)
                                {
                                    CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(keyValueDataField.Key);
                                    string destinationDataFieldName = customDataField.GetPhysicalName(keyValueDataField.Value);
                                    sqlInsert.AppendFormat(", {0}", destinationDataFieldName);
                                    DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                                    switch (dataFieldProperty)
                                    {
                                        case DataFieldProperty.LogicalDataField:
                                            sb.AppendFormat(", {0}", customDataField.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId));
                                            break;

                                        case DataFieldProperty.PhysicalDataField:
                                            sb.AppendFormat(", {0}", customDataFieldInfo.PhysicalName);
                                            break;
                                    }
                                }
                                sqlInsert.Append(") SELECT UserId, UserName, DepId, UserTypeId, 0 AS RecordSorting, AuditedStatus, @CurrentState, @CreationTime, @ModificationTime, @IsDeleted ");
                                sqlInsert.Append(sb);
                                sqlInsert.Append(" FROM ");
                                if (sourceDataWarehouseId != destinationDataWarehouseId)
                                {
                                    sqlInsert.Append(sourceDatabaseName);
                                    sqlInsert.Append(".dbo.");
                                }
                                sqlInsert.Append(sourceTableName);
                                if ((whereConditons != null) && (whereConditons.Count > 0))
                                {
                                    sqlInsert.Append(" WHERE ");
                                    sqlInsert.Append(DataAccessHandler.GetConditionSentence(whereConditons));
                                }
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlInsert.ToString()))
                                {
                                    db.AddInParameter(dbCommand, "CreationTime", DbType.DateTime, dateTime);
                                    db.AddInParameter(dbCommand, "ModificationTime", DbType.DateTime, dateTime);
                                    db.AddInParameter(dbCommand, "CurrentState", DbType.Byte, (byte)CurrentState.History);
                                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, false);                                    
                                    if (whereConditons != null && whereConditons.Count > 0)
                                    {
                                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                                    }
                                    db.ExecuteScalar(dbCommand, transaction);
                                }
                            }
                            if (dataRelationType == DataRelationType.ImportAndDelete || dataRelationType == DataRelationType.OnlyDelete)
                            {
                                StringBuilder sqlDelete = new StringBuilder();
                                if (whereConditons != null && whereConditons.Count > 0)
                                {
                                    sqlDelete.Append("DELETE FROM ");
                                    if (sourceDataWarehouseId != destinationDataWarehouseId)
                                    {
                                        sqlDelete.Append(sourceDatabaseName);
                                        sqlDelete.Append(".dbo.");
                                    }
                                    sqlDelete.Append(sourceTableName);
                                    sqlDelete.Append(" WHERE ");
                                    sqlDelete.Append(DataAccessHandler.GetConditionSentence(whereConditons));
                                }
                                else
                                {
                                    sqlDelete.Append("TRUNCATE TABLE ");
                                    if (sourceDataWarehouseId != destinationDataWarehouseId)
                                    {
                                        sqlDelete.Append(sourceDatabaseName);
                                        sqlDelete.Append(".dbo.");
                                    }
                                    sqlDelete.Append(sourceTableName);
                                }
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete.ToString()))
                                {
                                    if (whereConditons != null && whereConditons.Count > 0)
                                    {
                                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                                    }
                                    db.ExecuteScalar(dbCommand, transaction);
                                }
                            }
                            /* 更新插入的排序记录 */
                            UpdateRecordSorting(db, destinationTableName, dateTime, transaction);
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
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 DataFieldRelationInfo 对象的列表
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        public IList<DataFieldRelationInfo> GetModelInfos(decimal relationId)
        {
            //创建集合对象
            IList<DataFieldRelationInfo> dataFieldRelationInfos = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("RelationId", "RelationId", DbType.Decimal, relationId, DataFieldCondition.Equal));
            dataFieldRelationInfos = GetModelInfos(whereConditons, null, false);

            return dataFieldRelationInfos;
        }

        /// <summary>
        /// 更新字段关系
        /// </summary>
        /// <param name="relationId"></param>
        /// <param name="keyValueItems"></param>
        public void UpdateDataFieldRelation(decimal relationId, List<KeyValueItem> keyValueItems)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    IList<DataFieldRelationInfo> dataFieldRelationInfos = GetModelInfos(relationId);
                    /* 1. 增加的部分 */
                    foreach (KeyValueItem keyValueItem in keyValueItems)
                    {
                        bool find = false;
                        foreach (DataFieldRelationInfo dataFieldRelationInfo in dataFieldRelationInfos)
                        {
                            if (dataFieldRelationInfo.DataFieldId == keyValueItem.Key && dataFieldRelationInfo.ParentDataFieldId == keyValueItem.Value)
                            {
                                find = true;
                            }

                        }
                        if (!find)
                        {
                            Insert(new DataFieldRelationInfo(keyValueItem.Key, keyValueItem.Value, relationId, DateTime.Now), db, transaction);
                        }
                    }
                    /* 2. 删除的部分 */
                    foreach (DataFieldRelationInfo dataFieldRelationInfo in dataFieldRelationInfos)
                    {
                        bool find = false;
                        foreach (KeyValueItem keyValueItem in keyValueItems)
                        {
                            if (dataFieldRelationInfo.DataFieldId == keyValueItem.Key && dataFieldRelationInfo.ParentDataFieldId == keyValueItem.Value)
                            {
                                find = true;
                            }
                        }
                        if (!find)
                        {
                            Delete(dataFieldRelationInfo.DataFieldId, dataFieldRelationInfo.ParentDataFieldId, relationId, db, transaction);
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
        /// 获得表的对应关系
        /// </summary>
        /// <param name="relationId"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetTableRelation(decimal relationId)
        {
            Dictionary<decimal, decimal> tableRelation = new Dictionary<decimal, decimal>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT C.TableId, D.TableId FROM DataFieldRelation ");
            sb.Append("INNER JOIN CustomDataField A ON A.DataFieldId = DataFieldRelation.DataFieldId ");
            sb.Append("INNER JOIN CustomDataField B ON B.DataFieldId = DataFieldRelation.ParentDataFieldId ");
            sb.Append("INNER JOIN CustomTable C ON C.TableId = A.TableId ");
            sb.Append("INNER JOIN CustomTable D ON D.TableId = B.TableId ");
            sb.Append("WHERE DataFieldRelation.RelationId = @RelationId ");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RelationId", DbType.Decimal, relationId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentTableId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            tableRelation.Add(tableId, parentTableId);
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

            return tableRelation;
        }

        /// <summary>
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="userDataRelationId"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetDataFieldRelation(decimal relationId, decimal sourceTableId, decimal destinationTableId)
        {
            Dictionary<decimal, decimal> dataFieldRelation = new Dictionary<decimal, decimal>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT DataFieldRelation.DataFieldId, DataFieldRelation.ParentDataFieldId FROM DataFieldRelation ");
            sb.Append("INNER JOIN CustomDataField A ON A.DataFieldId = DataFieldRelation.DataFieldId ");
            sb.Append("INNER JOIN CustomDataField B ON B.DataFieldId = DataFieldRelation.ParentDataFieldId ");
            sb.Append("WHERE DataFieldRelation.RelationId = @RelationId AND A.TableId = @TableId_0 AND B.TableId = @TableId_1");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RelationId", DbType.Decimal, relationId);
                    db.AddInParameter(dbCommand, "TableId_0", DbType.Decimal, sourceTableId);
                    db.AddInParameter(dbCommand, "TableId_1", DbType.Decimal, destinationTableId);                    
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentDataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            dataFieldRelation.Add(dataFieldId, parentDataFieldId);
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

            return dataFieldRelation;
        }

        /// <summary>
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="userDataRelationId"></param>
        /// <param name="sourceTableId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetDataFieldRelationByAttachment(decimal relationId, decimal sourceTableId, decimal destinationTableId)
        {
            Dictionary<decimal, decimal> dataFieldRelation = new Dictionary<decimal, decimal>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DISTINCT DataFieldRelation.DataFieldId, DataFieldRelation.ParentDataFieldId FROM DataFieldRelation ");
            sb.Append("INNER JOIN CustomDataField A ON A.DataFieldId = DataFieldRelation.DataFieldId ");
            sb.Append("INNER JOIN CustomDataField B ON B.DataFieldId = DataFieldRelation.ParentDataFieldId ");
            sb.Append("WHERE DataFieldRelation.RelationId = @RelationId AND A.TableId = @TableId_0 AND B.TableId = @TableId_1 ");
            sb.Append("AND A.DataFieldProperty = @DataFieldProperty_0 AND B.DataFieldProperty = @DataFieldProperty_1 ");
            sb.Append(" AND (A.DataFieldType = @DataFieldType_0 OR  A.DataFieldType = @DataFieldType_1 OR  A.DataFieldType = @DataFieldType_2) ");
            sb.Append(" AND (B.DataFieldType = @DataFieldType_3 OR  B.DataFieldType = @DataFieldType_4 OR  A.DataFieldType = @DataFieldType_5) ");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RelationId", DbType.Decimal, relationId);
                    db.AddInParameter(dbCommand, "TableId_0", DbType.Decimal, sourceTableId);
                    db.AddInParameter(dbCommand, "TableId_1", DbType.Decimal, destinationTableId);
                    db.AddInParameter(dbCommand, "DataFieldProperty_0", DbType.Byte, (byte)DataFieldProperty.PhysicalDataField);
                    db.AddInParameter(dbCommand, "DataFieldProperty_1", DbType.Byte, (byte)DataFieldProperty.PhysicalDataField);
                    db.AddInParameter(dbCommand, "DataFieldType_0", DbType.Byte, (byte)PhysicalDataFieldType.DocAttachment);
                    db.AddInParameter(dbCommand, "DataFieldType_1", DbType.Byte, (byte)PhysicalDataFieldType.PicAttachment);
                    db.AddInParameter(dbCommand, "DataFieldType_2", DbType.Byte, (byte)PhysicalDataFieldType.PDFAttachment);
                    db.AddInParameter(dbCommand, "DataFieldType_3", DbType.Byte, (byte)PhysicalDataFieldType.DocAttachment);
                    db.AddInParameter(dbCommand, "DataFieldType_4", DbType.Byte, (byte)PhysicalDataFieldType.PicAttachment);
                    db.AddInParameter(dbCommand, "DataFieldType_5", DbType.Byte, (byte)PhysicalDataFieldType.PDFAttachment);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentDataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            dataFieldRelation.Add(dataFieldId, parentDataFieldId);
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

            return dataFieldRelation;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        ///  删除 DataFieldRelationInfo 对象
        /// </summary>
        ///<param name="relationId">关系编号</param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(decimal relationId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataFieldRelation ");
            sb.Append("WHERE RelationId = @RelationId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RelationId", DbType.Decimal, relationId);
                    //执行删除操作
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
                        throw new Exception("删除失败。");
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
        /// 获得 DataFieldRelationInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>DataFieldRelationInfo 对象列表</returns>
        private IList<DataFieldRelationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<DataFieldRelationInfo>  dataFieldRelationInfos = new List<DataFieldRelationInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM DataFieldRelation");
            
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
							decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
							decimal parentDataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
							decimal relationId = DataConvertionHelper.GetDecimal(dataReader[2]);
							DateTime relationTime = DataConvertionHelper.GetDateTime(dataReader[3]);
							//将创建 DataFieldRelationInfo 对象加入集合中
							dataFieldRelationInfos.Add(new DataFieldRelationInfo(dataFieldId, parentDataFieldId, relationId, relationTime));							
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
            
			return dataFieldRelationInfos;
		}
        
		
		/// <summary>
		/// 获得 DataFieldRelationInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>DataFieldRelationInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM DataFieldRelation");
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
        /// 获得表 DataFieldRelation 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataFieldRelation ", "DataFieldId", "*", false, false, startPosition, 
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
        /// 获得以表 DataFieldRelation 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataFieldRelation ", "DataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 DataFieldRelation 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataFieldRelation ", "DataFieldId", "*", false, false, startPosition, 
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
        /// 获得以表 DataFieldRelation 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataFieldRelation ", "DataFieldId", "*", false, false, tableLinks, startPosition, 
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
		/// 删除满足条件的的 DataFieldRelationInfo 对象
		/// </summary>
	    /// <param name="dataFieldId">字段编号</param>
		/// <returns>返回删除的记录数目数目</returns>
		private int Delete(decimal dataFieldId)
		{
			int count = 0; 
			//删除语句
			string sqlDelete = "DELETE FROM DataFieldRelation WHERE DataFieldId = @DataFieldId";
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
				{
					db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
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
        /// 删除满足条件的所有  DataFieldRelationInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataFieldRelation");
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
        /// 更新表中记录排序
        /// </summary>
        /// <param name="db">数据库名称</param>
        /// <param name="dateTime">更新时间</param>
        /// <param name="destinationTableName">表名称</param>
        /// <param name="transaction">事务</param>
        private void UpdateRecordSorting(SqlDatabase db, string destinationTableName, DateTime dateTime, DbTransaction transaction)
        {
            StringBuilder sbSelect = new StringBuilder();
            sbSelect.Append("SELECT DISTINCT(UserId) FROM ");
            sbSelect.Append(destinationTableName);
            sbSelect.Append(" WHERE RecordSorting = 0 AND CreationTime = @CreationTime");
            IList<decimal> userIds = new List<decimal>();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sbSelect.ToString()))
            {
                db.AddInParameter(dbCommand, "CreationTime", DbType.DateTime, dateTime);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand, transaction))
                {
                    while (dataReader.Read())
                    {
                        userIds.Add((decimal)dataReader[0]);
                    }
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                }
            }
            foreach (decimal userId in userIds)
            {
                UpdateRecordSorting(userId, db, destinationTableName, dateTime, transaction);
            }
        }

        /// <summary>
        /// 更新个人记录排序
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="db"></param>
        /// <param name="destinationTableName"></param>
        /// <param name="dateTime"></param>
        /// <param name="transaction"></param>
        private void UpdateRecordSorting(decimal userId, SqlDatabase db, string destinationTableName, DateTime dateTime, DbTransaction transaction)
        {
            try
            {
                int recordSorting = 0;
                StringBuilder sbQuery = new StringBuilder();
                sbQuery.Append("SELECT MAX(RecordSorting) FROM ");
                sbQuery.Append(destinationTableName);
                sbQuery.Append(" WHERE UserId = @UserId");
                using (DbCommand dbCommand = db.GetSqlStringCommand(sbQuery.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    recordSorting = (int)db.ExecuteScalar(dbCommand, transaction);
                }

                StringBuilder sbSelect = new StringBuilder();
                sbSelect.Append("SELECT RecordId FROM  ");
                sbSelect.Append(destinationTableName);
                sbSelect.Append(" WHERE  UserId = @UserId AND RecordSorting = 0 AND CreationTime = @CreationTime");

                IList<decimal> recordIds = new List<decimal>();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sbSelect.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "CreationTime", DbType.DateTime, dateTime);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand, transaction))
                    {
                        while (dataReader.Read())
                        {
                            recordIds.Add((decimal)dataReader[0]);
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
                StringBuilder sbUpdate = new StringBuilder();
                sbUpdate.Append("UPDATE ");
                sbUpdate.Append(destinationTableName);
                sbUpdate.Append(" SET RecordSorting = @RecordSorting WHERE RecordId = @RecordId");
                foreach (decimal recordId in recordIds)
                {
                    recordSorting = recordSorting + 1;
                    using (DbCommand command = db.GetSqlStringCommand(sbUpdate.ToString()))
                    {
                        db.AddInParameter(command, "RecordSorting", DbType.Int32, recordSorting);
                        db.AddInParameter(command, "RecordId", DbType.Decimal, recordId);
                        db.ExecuteNonQuery(command, transaction);
                    }
                }
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 向 DataFieldRelation 表中插入一条新记录
        /// </summary>
        /// <param name="dataFieldRelationInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Insert(DataFieldRelationInfo dataFieldRelationInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO DataFieldRelation(DataFieldId, ParentDataFieldId, RelationId, RelationTime)");
            sb.Append("VALUES (@DataFieldId, @ParentDataFieldId, @RelationId, @RelationTime)");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldRelationInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, dataFieldRelationInfo.ParentDataFieldId);
                    db.AddInParameter(dbCommand, "RelationId", DbType.Decimal, dataFieldRelationInfo.RelationId);
                    db.AddInParameter(dbCommand, "RelationTime", DbType.DateTime, dataFieldRelationInfo.RelationTime);
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

        /// <summary>
        ///  删除 DataFieldRelationInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="relationId">关系编号</param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Delete(decimal dataFieldId, decimal parentDataFieldId, decimal relationId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataFieldRelation ");
            sb.Append("WHERE DataFieldId = @DataFieldId AND ParentDataFieldId = @ParentDataFieldId AND RelationId = @RelationId");
            
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, parentDataFieldId);
                    db.AddInParameter(dbCommand, "RelationId", DbType.Decimal, relationId);
                    //执行删除操作
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
                        throw new Exception("删除失败。");
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
