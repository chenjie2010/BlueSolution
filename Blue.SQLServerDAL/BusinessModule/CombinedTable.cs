//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CombinedTable.cs
// 描述: CombinedTable 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/8/15
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
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CombinedTable 表的数据层访问类
    /// </summary>
    public class CombinedTable : CommonNodeDataAccess, ICombinedTable
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CombinedTable() : base("CombinedTable", "CombinedTableId", "GroupId", "CombinedTableName", "CombinedTableCode",false, true)
        {
		}

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CombinedTable 表中插入一条新记录
        /// </summary>
        /// <param name="combinedTableInfo">combinedTableInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CombinedTableInfo combinedTableInfo)
        {
            return Insert(combinedTableInfo, null);
        }        

        /// <summary>
		/// 获得 CombinedTableInfo 对象
		/// </summary>
		///<param name="combinedTableId"></param>
		/// <returns> CombinedTableInfo 对象</returns>
		public CombinedTableInfo GetModelInfo(decimal combinedTableId)
		{			
			CombinedTableInfo  combinedTableInfo = null;
			//生成选择语句
			StringBuilder sb = new StringBuilder();		
			sb.Append("SELECT GroupId, CombinedTableName, CombinedTableCode, DataWarehouseId, IsLeaf, ");
			sb.Append("ToolTip, Sorting, Notes ");
			sb.Append("FROM CombinedTable ");
			sb.Append("WHERE CombinedTableId = @CombinedTableId");
			try
			{
				//获得系统数据库对象
				SqlDatabase db = DataAccessHelper.GetDatabase();
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableId);
					using (IDataReader dataReader = db.ExecuteReader(dbCommand))
					{
						if (dataReader.Read())
						{
							decimal groupId = DataConvertionHelper.GetDecimal(dataReader[0]);
							string combinedTableName = DataConvertionHelper.GetString(dataReader[1]);
							string combinedTableCode = DataConvertionHelper.GetString(dataReader[2]);
							byte dataWarehouseId = DataConvertionHelper.GetByte(dataReader[3]);
							bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[4]);
							string toolTip = DataConvertionHelper.GetString(dataReader[5]);
							int sorting = DataConvertionHelper.GetInt(dataReader[6]);
							string notes = DataConvertionHelper.GetString(dataReader[7]);
							//创建 CombinedTableInfo 对象
							combinedTableInfo = new CombinedTableInfo(combinedTableId, groupId, combinedTableName, combinedTableCode, dataWarehouseId, 
							isLeaf, toolTip, sorting, notes);
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
            
			return combinedTableInfo;
		}
        
        /// <summary>
		/// 更新 CombinedTableInfo 对象
		/// </summary>
		/// <param name="combinedTableInfo">CombinedTableInfo 对象</param>
		public void Update(CombinedTableInfo combinedTableInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE CombinedTable SET GroupId = @GroupId, CombinedTableName = @CombinedTableName, CombinedTableCode = @CombinedTableCode, ");
			sb.Append("DataWarehouseId = @DataWarehouseId, IsLeaf = @IsLeaf, ToolTip = @ToolTip, ");
			sb.Append("Sorting = @Sorting, Notes = @Notes ");
			sb.Append("WHERE CombinedTableId = @CombinedTableId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableInfo.CombinedTableId);
					db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, combinedTableInfo.GroupId);
					db.AddInParameter(dbCommand, "CombinedTableName", DbType.String, combinedTableInfo.CombinedTableName);
					db.AddInParameter(dbCommand, "CombinedTableCode", DbType.String, combinedTableInfo.CombinedTableCode);
					db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, combinedTableInfo.DataWarehouseId);
					db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, combinedTableInfo.IsLeaf);
					db.AddInParameter(dbCommand, "ToolTip", DbType.String, combinedTableInfo.ToolTip);
					db.AddInParameter(dbCommand, "Sorting", DbType.Int32, combinedTableInfo.Sorting);
					db.AddInParameter(dbCommand, "Notes", DbType.String, combinedTableInfo.Notes);
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
		///  删除 CombinedTableInfo 对象
		/// </summary>
	    ///<param name="combinedTableId"></param>
		public void Delete(decimal combinedTableId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM CombinedTable ");
			sb.Append("WHERE CombinedTableId = @CombinedTableId");

            CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    combinedTableRelation.Delete(combinedTableId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableId);
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
		/// 获得 CombinedTableInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CombinedTableInfo 对象列表</returns>
		public IList<CombinedTableInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 CombinedTable 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CombinedTableInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CombinedTable ", "CombinedTableId", false, whereConditons);
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
        /// 获得组合表的记录数量
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal combinedTableId, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                byte dataWarehouseId = GetDataWarehouseId(combinedTableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));

                CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
                IList<CommonNode> commonNodeInfos = combinedTableRelation.GetTables(combinedTableId);
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT COUNT(1) FROM ");
                string lastTableName = string.Empty;
                foreach (var commonNodeInfo in commonNodeInfos)
                {
                    if (string.IsNullOrWhiteSpace(lastTableName))
                    {
                        sb.Append(commonNodeInfo.NodeCode);
                    }
                    else
                    {
                        sb.AppendFormat(" INNER JOIN {0} ON {0}.UserId = {1}.UserId ", commonNodeInfo.NodeCode, lastTableName);
                    }
                    lastTableName = commonNodeInfo.NodeCode;
                }
                sb.Append("WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
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
        /// 获得组合表的记录
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public DataSet GetTableData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;

            try
            {
                if (sortingCondtions == null && sortingCondtions.Count == 0)
                {
                    throw new ArgumentException("排序条件不能为空。");
                }
                byte dataWarehouseId = GetDataWarehouseId(combinedTableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
                IList<CommonNode> commonNodeInfos = combinedTableRelation.GetTables(combinedTableId);
                if (commonNodeInfos == null || commonNodeInfos.Count == 0)
                {
                    return null;
                }
                string tableName = commonNodeInfos[0].NodeCode;
                foreach (var whereConditon in whereConditons)
                {
                    whereConditon.DataTableName = tableName;
                }
                foreach (var sortingCondtion in sortingCondtions)
                {
                    sortingCondtion.DataTableName = tableName;
                }
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);                
                string createdTime = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime);
                string updatedTime = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime);
                IList<TableLink> tableLinks = new List<TableLink>();
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}.{1} AS {1}, {0}.{2} AS {0}_{2}, {0}.{3} AS CreatedTime, {0}.{4} AS UpdatedTime", tableName, userIdName, recordIdName, createdTime, updatedTime);
                int index = 0;
                foreach (var commonNodeInfo in commonNodeInfos)
                {
                    if (index > 0)
                    {
                        sb.AppendFormat(", {0}.{1} AS {0}_{1}", commonNodeInfo.NodeCode, recordIdName);
                        tableLinks.Add(new TableLink(tableName, commonNodeInfo.NodeCode, "UserId", TableJoin.FullOuterJoin));
                    }
                    DataTableType dataTableType = (DataTableType)commonNodeInfo.NodeType;
                    if (dataTableType == DataTableType.MasterSlaveTable)
                    {
                        whereConditons.Add(new WhereConditon(commonNodeInfo.NodeCode, "CurrentState", string.Format("CurrentState_{0}", index), DbType.Byte, (byte)CurrentState.Current, DataFieldCondition.Equal,
                            DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    }
                    index++;
                }
                foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in dataFieldNameRelations)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)keyValue.Value.DataFieldProperty;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.SystemPhysicalDataField:
                        case DataFieldProperty.PhysicalDataField:
                            sb.AppendFormat(", {0}", keyValue.Value.PhysicalName);
                            break;

                        case DataFieldProperty.LogicalDataField:
                            sb.AppendFormat(", {0} AS {1}", keyValue.Value.ExpressionText, keyValue.Value.PhysicalName);
                            break;
                    }
                }
                ds = DataAccessHandler.GetPageRecord(db, tableName, sb.ToString(), false, tableLinks, startPosition,
                  count, whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }
        
        /// <summary>
        /// 获得用户在表中的记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal userId, decimal combinedTableId, bool businessEnabled, decimal instanceId)
        {
            int count = 0;

            try
            {
                byte dataWarehouseId = GetDataWarehouseId(combinedTableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));

                CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
                IList<CommonNode> commonNodeInfos = combinedTableRelation.GetTables(combinedTableId);
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT COUNT(1) FROM ");
                string lastTableName = string.Empty;
                foreach (var commonNodeInfo in commonNodeInfos)
                {
                    if (string.IsNullOrWhiteSpace(lastTableName))
                    {
                        sb.Append(commonNodeInfo.NodeCode);
                    }
                    else
                    {
                        sb.AppendFormat(" FULL OUTER JOIN {0} ON {0}.UserId = {1}.UserId ", commonNodeInfo.NodeCode, lastTableName);
                    }
                    lastTableName = commonNodeInfo.NodeCode;
                }
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                whereConditons.Add(new WhereConditon(commonNodeInfos[0].NodeCode, "UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                if (businessEnabled && instanceId > 0)
                {
                    whereConditons.Add(new WhereConditon(commonNodeInfos[0].NodeCode, "BusinessId", "BusinessId", DbType.Decimal, instanceId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                sb.Append("WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
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
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal combinedTableId)
        {
            byte dataWarehouseId = 0;

            string sqlSelect = "SELECT DataWarehouseId FROM CombinedTable WHERE CombinedTableId = @CombinedTableId";

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableId);
                    dataWarehouseId = DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataWarehouseId;
        }

        /// <summary>
        /// 获得组合表的分页数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetCombinedTableData(decimal combinedTableId, Int64 systemLogicalDataFields, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId, 
            int startPosition, int count, ref int totalCount)
        {
            DataTable dataTable = null;

            if (dataFieldNameRelations == null || dataFieldNameRelations.Count == 0)
            {
                return null;
            }

            byte dataWarehouseId = GetDataWarehouseId(combinedTableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
            IList<CommonNode> commonNodeInfos = combinedTableRelation.GetTables(combinedTableId);
            if (commonNodeInfos == null || commonNodeInfos.Count == 0)
            {
                return null;
            }
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            
            string tableName = commonNodeInfos[0].NodeCode;
            IList<TableLink> tableLinks = DataFieldHelper.GetSystemTableLinks(tableName, systemLogicalDataFields);
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon(tableName, "UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}.{1} AS {0}_{1} ", tableName, recordIdName);
            int index = 0;
            foreach(var commonNodeInfo in commonNodeInfos)
            {
                if (index > 0)
                {
                    sb.AppendFormat(", {0}.{1} AS {0}_{1}", commonNodeInfo.NodeCode, recordIdName);
                    tableLinks.Add(new TableLink(tableName, commonNodeInfo.NodeCode, "UserId", TableJoin.FullOuterJoin));
                }
                DataTableType dataTableType = (DataTableType)commonNodeInfo.NodeType;
                if (dataTableType == DataTableType.MasterSlaveTable)
                {
                    whereConditons.Add(new WhereConditon(commonNodeInfo.NodeCode, "CurrentState", string.Format("CurrentState_{0}", index), DbType.Byte, (byte)CurrentState.Current, DataFieldCondition.Equal,
                        DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                index++;
            }
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in dataFieldNameRelations)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)keyValue.Value.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                    case DataFieldProperty.PhysicalDataField:
                        sb.AppendFormat(", {0}", keyValue.Value.PhysicalName);
                        break;

                    case DataFieldProperty.LogicalDataField:
                        sb.AppendFormat(", {0} AS {1}", keyValue.Value.ExpressionText, keyValue.Value.PhysicalName);
                        break;
                }
            }

            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            /* 最后一个表的修改时间排序 */
            sortingCondtions.Add(new SortingCondtion(commonNodeInfos[commonNodeInfos.Count - 1].NodeCode, "ModificationTime", CustomSorting.Ascending));
            DataSet ds = DataAccessHandler.GetPageRecord(db, tableName, sb.ToString(), false, tableLinks, startPosition,
              count, whereConditons, sortingCondtions, ref totalCount);

            dataTable = ds.Tables[0];
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                if (dataFieldNameRelations.ContainsKey(dataColumn.ColumnName))
                {
                    dataColumn.ExtendedProperties.Add(dataColumn.ColumnName, dataFieldNameRelations[dataColumn.ColumnName]);
                    dataColumn.Caption = dataFieldNameRelations[dataColumn.ColumnName].LogicalName;
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 获得组合表的数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataTable GetCombinedTableData(decimal combinedTableId, bool businessEnabled, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId, decimal instanceId)
        {
            DataTable dataTable = null;

            if (dataFieldNameRelations == null || dataFieldNameRelations.Count == 0)
            {
                return null;
            }

            byte dataWarehouseId = GetDataWarehouseId(combinedTableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            
            CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
            IList<CommonNode> commonNodeInfos = combinedTableRelation.GetTables(combinedTableId);
            if (commonNodeInfos == null || commonNodeInfos.Count == 0)
            {
                return null;
            }
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            StringBuilder sb = new StringBuilder();
            StringBuilder sbFrom = new StringBuilder();
            sb.Append("SELECT ");
            string lastTableName = string.Empty;
            foreach (var commonNodeInfo in commonNodeInfos)
            {               
                sb.AppendFormat("{0}.{1} AS {0}_{1}, ", commonNodeInfo.NodeCode, recordIdName);
                if (sbFrom.Length == 0)
                {
                    sbFrom.Append(commonNodeInfo.NodeCode); 
                }
                else
                {
                    sbFrom.AppendFormat(" FULL OUTER JOIN {0} ON {0}.UserId = {1}.UserId ", commonNodeInfo.NodeCode, lastTableName);                    
                }
                lastTableName = commonNodeInfo.NodeCode;
            }
            sb.Remove(sb.Length - 2, 2);
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon(commonNodeInfos[0].NodeCode, "UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            if (businessEnabled && instanceId > 0)
            {
                whereConditons.Add(new WhereConditon(commonNodeInfos[0].NodeCode, "BusinessId", "BusinessId", DbType.Decimal, instanceId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in dataFieldNameRelations)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)keyValue.Value.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.PhysicalDataField:
                        sb.AppendFormat(", {0}", keyValue.Value.PhysicalName);
                        break;

                    case DataFieldProperty.LogicalDataField:
                        sb.AppendFormat(", {0} AS {1}", keyValue.Value.ExpressionText, keyValue.Value.PhysicalName);
                        break;
                }
            }
            sb.AppendFormat(" FROM {0} ", sbFrom);
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append("WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            sb.AppendFormat(" ORDER BY {0}.ModificationTime DESC", lastTableName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                if ((whereConditons != null) && (whereConditons.Count > 0))
                {
                    DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                }
                dataTable = db.ExecuteDataSet(dbCommand).Tables[0];
            }
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                if (dataFieldNameRelations.ContainsKey(dataColumn.ColumnName))
                {
                    dataColumn.ExtendedProperties.Add(dataColumn.ColumnName, dataFieldNameRelations[dataColumn.ColumnName]);
                    dataColumn.Caption = dataFieldNameRelations[dataColumn.ColumnName].LogicalName;
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="instanceId"></param>
        /// <param name="onlyTarget"></param>
        /// <returns></returns>
        public Dictionary<decimal, DataRowItem> GetMirrorRowData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal instanceId, bool onlyTarget)
        {
            Dictionary<decimal, DataRowItem> dataRowItems = new Dictionary<decimal, DataRowItem>();

            byte dataWarehouseId = GetDataWarehouseId(combinedTableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            SqlDatabase dbBlue = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            string businessAlternativeIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessAlternativeId);
            CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
            IList<decimal> tableIds = combinedTableRelation.GetSecondIds(combinedTableId);
            CustomTable customTable = new CustomTable();
            foreach (var obj in tableIds)
            {
                DataRowItem dataRowItem = new DataRowItem()
                {
                    Key = obj
                };
                StringBuilder sb = new StringBuilder();
                string physicalName = customTable.GetTablePhysicalName(obj);
                sb.AppendFormat("SELECT TOP 1 {0}, {1}", recordIdName, businessAlternativeIdName);
                foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in dataFieldNameRelations)
                {
                    if (keyValue.Value.TableId == obj)
                    {
                        DataFieldProperty dataFieldProperty = (DataFieldProperty)keyValue.Value.DataFieldProperty;
                        switch (dataFieldProperty)
                        {
                            case DataFieldProperty.PhysicalDataField:
                                sb.AppendFormat(", {0}", keyValue.Value.PhysicalName);
                                break;

                            case DataFieldProperty.LogicalDataField:
                                sb.AppendFormat(", {0} AS {1}", keyValue.Value.ExpressionText, keyValue.Value.PhysicalName);
                                break;
                        }
                    }
                }
                sb.AppendFormat(" FROM {0} ", physicalName);
                StringBuilder sbSource = new StringBuilder();
                if (!onlyTarget)
                {
                    sbSource.Append(sb);
                    sbSource.AppendFormat(" WHERE {0} =  @{0}", recordIdName);
                }
                sb.Append(" WHERE BusinessId =  @BusinessId");
                decimal recordId = decimal.MinValue;
                DataTable dataTable = null;
                using (DbCommand dbCommand = dbBlue.GetSqlStringCommand(sb.ToString()))
                {
                    dbBlue.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, instanceId);
                    dataTable = dbBlue.ExecuteDataSet(dbCommand).Tables[0];
                    if (dataTable.Rows.Count > 0)
                    {
                        dataRowItem.TragetRow = dataTable;
                    }
                }
                if (!onlyTarget && dataTable.Rows.Count > 0)
                {
                    recordId = DataConvertionHelper.GetDecimal(dataTable.Rows[0][businessAlternativeIdName]);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sbSource.ToString()))
                    {
                        db.AddInParameter(dbCommand, recordIdName, DbType.Decimal, recordId);
                        DataTable sourceDataTable = db.ExecuteDataSet(dbCommand).Tables[0];
                        if (sourceDataTable.Rows.Count > 0)
                        {
                            dataRowItem.SourceRow = sourceDataTable;
                        }
                    }
                }
                dataRowItems.Add(obj, dataRowItem);
            }

            return dataRowItems;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public Dictionary<decimal, DataTable> GetMirrorRowData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal instanceId)
        {
            Dictionary<decimal, DataTable> dataRowValues = new Dictionary<decimal, DataTable>();

            byte dataWarehouseId = GetDataWarehouseId(combinedTableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
            IList<DataTable> dataTables = new List<DataTable>();
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("BusinessId", "BusinessId", DbType.Decimal, instanceId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
            IList<decimal> tableIds = combinedTableRelation.GetSecondIds(combinedTableId);
            CustomTable customTable = new CustomTable();
            foreach (var obj in tableIds)
            {
                //whereConditons.Add(new WhereConditon("BusinessId", "BusinessId", DbType.Decimal, instanceId, DataFieldCondition.Equal,
                //        DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                DataTable table = GetLastestRecord(db, obj, dataFieldNameRelations, whereConditons);
                if (table != null && table.Rows.Count > 0)
                {
                    dataRowValues.Add(obj, table);
                }
            }

            return dataRowValues;
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public Dictionary<decimal, DataTable> GetDataFilledData(decimal combinedTableId, bool businessEnabled, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId, decimal instanceId)
        {
            Dictionary<decimal, DataTable> dataRowValues = new Dictionary<decimal, DataTable>();

            byte dataWarehouseId = GetDataWarehouseId(combinedTableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            IList<DataTable> dataTables = new List<DataTable>();
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            if (businessEnabled && instanceId > 0)
            {
                whereConditons.Add(new WhereConditon("BusinessId", "BusinessId", DbType.Decimal, instanceId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            }
            CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
            IList<decimal> tableIds = combinedTableRelation.GetSecondIds(combinedTableId);
            CustomTable customTable = new CustomTable();
            foreach (var obj in tableIds)
            {
                whereConditons.Clear();
                if (businessEnabled)
                {
                    if (instanceId <= 0)
                    {
                        continue;
                    }
                    whereConditons.Add(new WhereConditon("BusinessId", "BusinessId", DbType.Decimal, instanceId, DataFieldCondition.Equal,
                        DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

                }
                else
                {
                    whereConditons.Add(new WhereConditon("UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And,
                    DataFieldBracket.None, 0));
                }
                DataTableType tableType = customTable.GetDataTableType(obj);
                if (tableType == DataTableType.MasterSlaveTable)
                {
                    whereConditons.Add(new WhereConditon("CurrentState", "CurrentState", DbType.Byte, (byte)CurrentState.Current, DataFieldCondition.Equal,
                        DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                DataTable table = GetLastestRecord(db, obj, dataFieldNameRelations, whereConditons);
                if(table != null && table.Rows.Count > 0)
                {
                    dataRowValues.Add(obj, table);
                }
            }

            return dataRowValues;
        }
        
        /// <summary>
        /// 向 CombinedTable 表中插入一条新记录
        /// </summary>
        /// <param name="combinedTableInfo">combinedTableInfo 对象</param>
        /// <param name="combinedTableRelationInfos">关系表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CombinedTableInfo combinedTableInfo, IList<CombinedTableRelationInfo> combinedTableRelationInfos)
        {
            //自动增加的关键字的值
            decimal combinedTableId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CombinedTable", "Sorting", "GroupId", combinedTableInfo.GroupId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CombinedTable(GroupId, CombinedTableName, CombinedTableCode, DataWarehouseId, IsLeaf, ");
            sb.Append("ToolTip, Sorting, Notes)");
            sb.Append("VALUES (@GroupId, @CombinedTableName, @CombinedTableCode, @DataWarehouseId, @IsLeaf, ");
            sb.Append("@ToolTip, @Sorting, @Notes);");
            sb.Append("SET @CombinedTableId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "CombinedTableId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, combinedTableInfo.GroupId);
                        db.AddInParameter(dbCommand, "CombinedTableName", DbType.String, combinedTableInfo.CombinedTableName);
                        db.AddInParameter(dbCommand, "CombinedTableCode", DbType.String, combinedTableInfo.CombinedTableCode);
                        db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, combinedTableInfo.DataWarehouseId);
                        db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, true);
                        db.AddInParameter(dbCommand, "ToolTip", DbType.String, combinedTableInfo.ToolTip);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, combinedTableInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, combinedTableInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        combinedTableId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@CombinedTableId"].Value, 0);
                        CustomGroup customGroup = new CustomGroup();
                        customGroup.UpdateLeafOfParentNode(combinedTableInfo.GroupId, false, db, transaction);

                        foreach (var obj in combinedTableRelationInfos)
                        {
                            obj.CombinedTableId = combinedTableId;
                        }
                        CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
                        combinedTableRelation.Insert(combinedTableRelationInfos, db, transaction);
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

            return combinedTableId;
        }
        
        /// <summary>
        /// 更新组合表信息
        /// </summary>
        /// <param name="combinedTableInfo"></param>
        /// <param name="combinedTableRelationInfos"></param>
        public void Update(CombinedTableInfo combinedTableInfo, IList<CombinedTableRelationInfo> combinedTableRelationInfos)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CombinedTable SET CombinedTableName = @CombinedTableName, CombinedTableCode = @CombinedTableCode, ");
            sb.Append("DataWarehouseId = @DataWarehouseId, IsLeaf = @IsLeaf, ToolTip = @ToolTip, Notes = @Notes ");
            sb.Append("WHERE CombinedTableId = @CombinedTableId");

            CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
            IList<CombinedTableRelationInfo> oldCombinedTableRelationInfos = combinedTableRelation.GetModelInfos(combinedTableInfo.CombinedTableId);

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
                        db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, combinedTableInfo.CombinedTableId);
                        db.AddInParameter(dbCommand, "CombinedTableName", DbType.String, combinedTableInfo.CombinedTableName);
                        db.AddInParameter(dbCommand, "CombinedTableCode", DbType.String, combinedTableInfo.CombinedTableCode);
                        db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, combinedTableInfo.DataWarehouseId);
                        db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, combinedTableInfo.IsLeaf);
                        db.AddInParameter(dbCommand, "ToolTip", DbType.String, combinedTableInfo.ToolTip);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, combinedTableInfo.Notes);
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                        /* 1. 不存在则插入，存在则更新 */
                        foreach (var customViewAndTableInfo in combinedTableRelationInfos)
                        {
                            bool find = false;
                            foreach (var oldCustomViewAndTableInfo in oldCombinedTableRelationInfos)
                            {
                                if (customViewAndTableInfo.CombinedTableId == oldCustomViewAndTableInfo.CombinedTableId
                                    && customViewAndTableInfo.TableId == oldCustomViewAndTableInfo.TableId)
                                {
                                    find = true;
                                    if (customViewAndTableInfo.Sorting != oldCustomViewAndTableInfo.Sorting)
                                    {
                                        combinedTableRelation.Update(customViewAndTableInfo, db, transaction);
                                    }
                                    break;
                                }
                            }
                            if (!find)
                            {
                                customViewAndTableInfo.CombinedTableId = customViewAndTableInfo.CombinedTableId;
                                combinedTableRelation.Insert(customViewAndTableInfo, db, transaction);
                            }
                        }
                        /* 2. 存在则忽略，不存在则删除*/
                        foreach (var oldCustomViewAndTableInfo in oldCombinedTableRelationInfos)
                        {
                            bool find = false;
                            foreach (var customViewAndTableInfo in combinedTableRelationInfos)
                            {
                                if (customViewAndTableInfo.CombinedTableId == oldCustomViewAndTableInfo.CombinedTableId
                                    && customViewAndTableInfo.TableId == oldCustomViewAndTableInfo.TableId)
                                {
                                    find = true;
                                    break;
                                }
                            }
                            if (!find)
                            {
                                combinedTableRelation.Delete(oldCustomViewAndTableInfo, db, transaction);
                            }
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
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            CombinedTableInfo combinedTableInfo = GetModelInfo(nodeId);
            if (combinedTableInfo != null)
            {
                CustomGroup customGroup = new CustomGroup();
                IList<string> parentNames = customGroup.GetHierarchicalNamesOfNode(combinedTableInfo.GroupId);
                foreach (var parentName in parentNames)
                {
                    names.Add(parentName);
                }
                names.Add(combinedTableInfo.CombinedTableName);
            }

            return names;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion        

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 CombinedTableInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CombinedTableInfo 对象列表</returns>
        private IList<CombinedTableInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CombinedTableInfo>  combinedTableInfos = new List<CombinedTableInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CombinedTable");
            
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
							decimal combinedTableId = DataConvertionHelper.GetDecimal(dataReader[0]);
							decimal groupId = DataConvertionHelper.GetDecimal(dataReader[1]);
							string combinedTableName = DataConvertionHelper.GetString(dataReader[2]);
							string combinedTableCode = DataConvertionHelper.GetString(dataReader[3]);
							byte dataWarehouseId = DataConvertionHelper.GetByte(dataReader[4]);
							bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[5]);
							string toolTip = DataConvertionHelper.GetString(dataReader[6]);
							int sorting = DataConvertionHelper.GetInt(dataReader[7]);
							string notes = DataConvertionHelper.GetString(dataReader[8]);
							//将创建 CombinedTableInfo 对象加入集合中
							combinedTableInfos.Add(new CombinedTableInfo(combinedTableId, groupId, combinedTableName, combinedTableCode, dataWarehouseId, 
							isLeaf, toolTip, sorting, notes));							
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
            
			return combinedTableInfos;
		}
        
		
		/// <summary>
		/// 获得 CombinedTableInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CombinedTableInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CombinedTable");
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
        /// 获得表 CombinedTable 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedTable ", "CombinedTableId", "*", false, false, startPosition, 
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
        /// 获得以表 CombinedTable 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedTable ", "CombinedTableId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CombinedTable 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedTable ", "CombinedTableId", "*", false, false, startPosition, 
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
        /// 获得以表 CombinedTable 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CombinedTable ", "CombinedTableId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CombinedTableInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CombinedTable");
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
        /// 获取最新记录
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="dataTableType"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        private DataTable GetLastestRecord(SqlDatabase db, decimal tableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, IList<WhereConditon> whereConditons)
        {
            DataTable dataTable = null;

            string keyName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            CustomTable customTable = new CustomTable();
            string physicalName = customTable.GetTablePhysicalName(tableId);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT TOP 1 {0}", keyName);
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in dataFieldNameRelations)
            {
                if (keyValue.Value.TableId == tableId)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)keyValue.Value.DataFieldProperty;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.SystemPhysicalDataField:
                            SystemDataField systemDataField = (SystemDataField)Convert.ToByte(keyValue.Value.DataFieldId);
                            if (systemDataField == SystemDataField.UserName)
                            {
                                sb.AppendFormat(", {0}.{1}", physicalName, keyValue.Value.PhysicalName);
                            }
                            else
                            {
                                sb.AppendFormat(", {0}", keyValue.Value.PhysicalName);
                            }
                            break;

                        case DataFieldProperty.PhysicalDataField:
                            sb.AppendFormat(", {0}", keyValue.Value.PhysicalName);
                            break;

                        case DataFieldProperty.LogicalDataField:
                            sb.AppendFormat(", {0} AS {1}", keyValue.Value.ExpressionText, keyValue.Value.PhysicalName);
                            break;
                    }
                }
            }
            sb.AppendFormat(" FROM {0} ", physicalName);            
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            sb.Append(" ORDER BY ModificationTime DESC");
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {                
                if ((whereConditons != null) && (whereConditons.Count > 0))
                {
                    DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                }
                dataTable = db.ExecuteDataSet(dbCommand).Tables[0];
            }
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                if (dataFieldNameRelations.ContainsKey(dataColumn.ColumnName))
                {
                    dataColumn.ExtendedProperties.Add(dataColumn.ColumnName, dataFieldNameRelations[dataColumn.ColumnName]);
                    dataColumn.Caption = dataFieldNameRelations[dataColumn.ColumnName].LogicalName;
                }
            }
            dataTable.TableName = tableId.ToString();

            return dataTable;
        }

        #endregion

        #endregion
    }
}
