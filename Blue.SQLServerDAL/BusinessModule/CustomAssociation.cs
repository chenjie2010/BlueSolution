//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomAssociation.cs
// 描述：CustomAssociation 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/10/3
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.DataFieldLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomAssociation 表的数据层访问类
    /// </summary>
    public class CustomAssociation : CommonNodeDataAccess, ICustomAssociation
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomAssociation() : base("CustomAssociation", "AssociationId", "GroupId", "AssociationName", "AssociationCode", true, true)
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomAssociation 表中插入一条新记录
        /// </summary>
        /// <param name="customAssociationInfo">customAssociationInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomAssociationInfo customAssociationInfo)
        {
            //自动增加的关键字的值
            decimal customAssociationId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customAssociationInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomAssociation", "Sorting", "GroupId", customAssociationInfo.GroupId, 0) + 1;
            customAssociationInfo.PhysicalName = string.Format("association_{0}_{1}", customAssociationInfo.GroupId, customAssociationInfo.Sorting);
            
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomAssociation(GroupId, AssociationName, AssociationCode, PhysicalName, ShowMode, SuperAssociationEnabled, ");
            sb.Append("IsLeaf, Sorting, Notes)");
            sb.Append("VALUES (@GroupId, @AssociationName, @AssociationCode, @PhysicalName, @ShowMode, @SuperAssociationEnabled, ");
            sb.Append("@IsLeaf, @Sorting, @Notes);");
            sb.Append("SET @AssociationId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "AssociationId", DbType.Decimal, 8);
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customAssociationInfo.GroupId);
                        db.AddInParameter(dbCommand, "AssociationName", DbType.String, customAssociationInfo.AssociationName);
                        db.AddInParameter(dbCommand, "AssociationCode", DbType.String, customAssociationInfo.AssociationCode);
                        db.AddInParameter(dbCommand, "PhysicalName", DbType.String, customAssociationInfo.PhysicalName);
                        db.AddInParameter(dbCommand, "ShowMode", DbType.Byte, customAssociationInfo.ShowMode);
                        db.AddInParameter(dbCommand, "SuperAssociationEnabled", DbType.Boolean, customAssociationInfo.SuperAssociationEnabled);
                        db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, true);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customAssociationInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customAssociationInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customAssociationId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@AssociationId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(customAssociationInfo.GroupId, false, db, transaction);
                    CreatePhysicalTable(customAssociationInfo.PhysicalName);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customAssociationId;
        }

        /// <summary>
		/// 获得 CustomAssociationInfo 对象
		/// </summary>
		///<param name="associationId">关联编号</param>
		/// <returns> CustomAssociationInfo 对象</returns>
		public CustomAssociationInfo GetModelInfo(decimal associationId)
        {
            CustomAssociationInfo customAssociationInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("AssociationId", "AssociationId", System.Data.DbType.Decimal, associationId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomAssociationInfo> customAssociationInfos = GetModelInfos(whereConditons, null, true);
            if (customAssociationInfos != null && customAssociationInfos.Count > 0)
            {
                customAssociationInfo = customAssociationInfos[0];
            }

            return customAssociationInfo;
        }

        /// <summary>
        /// 更新 CustomAssociationInfo 对象
        /// </summary>
        /// <param name="customAssociationInfo">CustomAssociationInfo 对象</param>
        public void Update(CustomAssociationInfo customAssociationInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomAssociation SET AssociationName = @AssociationName, AssociationCode = @AssociationCode, SuperAssociationEnabled = @SuperAssociationEnabled, ");
            sb.Append("ShowMode = @ShowMode, Notes = @Notes WHERE AssociationId = @AssociationId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, customAssociationInfo.AssociationId);
                    db.AddInParameter(dbCommand, "AssociationName", DbType.String, customAssociationInfo.AssociationName);
                    db.AddInParameter(dbCommand, "AssociationCode", DbType.String, customAssociationInfo.AssociationCode);
                    db.AddInParameter(dbCommand, "SuperAssociationEnabled", DbType.Boolean, customAssociationInfo.SuperAssociationEnabled);
                    db.AddInParameter(dbCommand, "ShowMode", DbType.Byte, customAssociationInfo.ShowMode);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customAssociationInfo.Notes);
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
        /// 重置关联表
        /// </summary>
        /// <param name="associationId"></param>
        public void ResetTable(decimal associationId)
        {
            try
            {                
                string physicalName = GetPhysicalName(associationId);
                AssociatedDataField associatedDataField = new AssociatedDataField();
                Dictionary<string, string> names = associatedDataField.GetNames(associationId);
                if (names.Count > 0)
                {
                    SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);

                    /* 1. 删除记录 */
                    SqlDatabase db = DataAccessHelper.GetDatabase();
                    associatedDataField.Delete(associationId, db, null);

                    /* 2. 删除表中业务对应的物理字段 */
                    foreach (var name in names)
                    {
                        DataAccessHandler.DeleteDataField(dbBusiness, physicalName, name.Key);
                    }

                    /* 3. 清空业务记录 */
                    string sql = string.Format("TRUNCATE TABLE {0}", physicalName);                    
                    using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sql))
                    {
                        dbBusiness.ExecuteNonQuery(dbCommand);
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
        /// 该表是否有字段属于物理字段的关联字段类型
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public bool HasDataFieldConnected(decimal associationId)
        {
            bool result = false;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(1) FROM CustomDataField ");
            sb.Append("INNER JOIN AssociatedDataField ON CustomDataField.AssociatedDataFieldId = AssociatedDataField.AssociatedDataFieldId ");
            sb.Append("WHERE AssociationId = @AssociationId ");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, associationId);
                int count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                result = count > 0;
            }

            return result;
        }

        /// <summary>
        ///  删除 CustomAssociationInfo 对象
        /// </summary>
        ///<param name="associationId">关联编号</param>
        public void Delete(decimal associationId)
        {
            /* 删除表 */
            StringBuilder sbBusiness = new StringBuilder();
            sbBusiness.Append("DROP TABLE  ");
            sbBusiness.Append(GetPhysicalName(associationId));
            //获得通用业务数据库对象
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sbBusiness.ToString()))
            {
                dbBusiness.ExecuteNonQuery(dbCommand);
            }

            AssociatedDataField associatedDataField = new AssociatedDataField();
            /* 生成删除记录 */
            string sqlDelete = "DELETE FROM CustomAssociation WHERE AssociationId = @AssociationId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                    {
                        db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, associationId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
                        }
                    }
                    associatedDataField.Delete(associationId, db, transaction);
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
        /// 获得 CustomAssociationInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomAssociationInfo 对象列表</returns>
        public IList<CustomAssociationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomAssociation 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomAssociationInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomAssociation ", "AssociationId", false, whereConditons);
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
        /// 该表中的字段属于物理字段的关联字段类型的个数
        /// </summary>
        /// <param name="associationCode"></param>
        /// <returns></returns>
        public int GetDataFieldCountConnected(string associationCode)
        {
            int count = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(1) FROM CustomDataField INNER JOIN AssociatedDataField ON AssociatedDataField.AssociatedDataFieldId = CustomDataField.AssociatedDataFieldId ");
            sb.Append("INNER JOIN CustomAssociation ON CustomAssociation.AssociationId = AssociatedDataField.AssociationId ");
            sb.Append("WHERE AssociationCode = @AssociationCode  ");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                db.AddInParameter(dbCommand, "AssociationCode", DbType.String, associationCode);
                count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));               
            }

            return count;
        }

        /// <summary>
        /// 导入业务数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="dataTable"></param>
        public void ImportDataTable(decimal associationId, DataTable dataTable)
        {
            try
            {
                string tablePhysicalName = GetPhysicalName(associationId);
                string connectionString = DataAccessHelper.GetConnectionString(DataWarehouseHelper.BusinessDatabaseName);
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connectionString, SqlBulkCopyOptions.UseInternalTransaction);
                sqlBulkCopy.DestinationTableName = tablePhysicalName;
                sqlBulkCopy.BulkCopyTimeout = 15;
                foreach (DataColumn dataColumn in dataTable.Columns)
                {
                    sqlBulkCopy.ColumnMappings.Add(dataColumn.ColumnName, dataColumn.ColumnName);
                }
                sqlBulkCopy.WriteToServer(dataTable);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(IList<decimal> groupIds)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT AssociationId, AssociationName, AssociationCode FROM CustomAssociation ");
                if (groupIds.Count > 0)
                {
                    sb.Append("WHERE ");
                    for (int index = 0; index < groupIds.Count; index++)
                    {
                        sb.AppendFormat("GroupId = @GroupId_{0} OR ", index);
                    }
                    sb.Remove(sb.Length - 4, 4);
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    int index = 0;
                    foreach (decimal groupId in groupIds)
                    {
                        db.AddInParameter(dbCommand, string.Format("GroupId_{0}", index), DbType.Decimal, groupId);
                        index++;
                    }
                    ds = db.ExecuteDataSet(dbCommand);
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
        /// 是否是超大关联
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public bool GetSuperAssociationEnabled(decimal associationId)
        {
            bool superAssociationEnabled = false;
            
            string sqlSelect = "SELECT SuperAssociationEnabled FROM CustomAssociation WHERE AssociationId = @AssociationId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, DataConvertionHelper.SetDecimal(associationId));
                    superAssociationEnabled = DataConvertionHelper.GetBoolean(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return superAssociationEnabled;
        }

        /// <summary>
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            CustomAssociationInfo customAssociationInfo = GetModelInfo(nodeId);
            if (customAssociationInfo != null)
            {
                CustomGroup customGroup = new CustomGroup();
                IList<string> parentNames = customGroup.GetHierarchicalNamesOfNode(customAssociationInfo.GroupId);
                foreach (var parentName in parentNames)
                {
                    names.Add(parentName);
                }
                names.Add(customAssociationInfo.AssociationName);
            }

            return names;
        }

        /// <summary>
        /// 在关联表中增加记录
        /// </summary>
        /// <param name="associationId">关联编号</param>
        /// <param name="commonDataFields"></param>
        /// <returns></returns>
        public decimal Insert(decimal associationId, IList<CommonDataField> commonDataFields)
        {
            decimal recordId = 0;

            //获得通用业务数据库对象
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
            string physicalName = GetPhysicalName(associationId);
            int sorting = DataAccessHandler.GetMaxValueOfDataField(dbBusiness, physicalName, "RecordSorting", string.Empty, 0, 0) + 1;

            //生成插入语句
            StringBuilder sbInsert = new StringBuilder();            
            sbInsert.Append("INSERT INTO ");
            sbInsert.Append(physicalName);
            sbInsert.Append("(");
            StringBuilder sbDataFieldName = new StringBuilder();
            StringBuilder sbDataFieldParamName = new StringBuilder();
            foreach (CommonDataField commonDataField in commonDataFields)
            {
                sbDataFieldName.Append(commonDataField.DataFieldName);
                sbDataFieldName.Append(", ");
                sbDataFieldParamName.Append("@");
                sbDataFieldParamName.Append(commonDataField.DataFieldParameterName);
                sbDataFieldParamName.Append(", ");
            }
            sbDataFieldName.Append(" RecordSorting");
            sbDataFieldParamName.Append(" @RecordSorting");
            sbInsert.Append(sbDataFieldName.ToString());
            sbInsert.Append(") VALUES (");
            sbInsert.Append(sbDataFieldParamName.ToString());
            sbInsert.Append(");SET @RecordId = SCOPE_IDENTITY()");
            
            try
            {
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sbInsert.ToString()))
                {
                    //给参数赋值
                    dbBusiness.AddOutParameter(dbCommand, "RecordId", DbType.Decimal, 12);
                    dbBusiness.AddInParameter(dbCommand, "RecordSorting", DbType.Int32, sorting);
                    DataAccessHandler.AddInParameter(dbBusiness, dbCommand, commonDataFields);
                    //执行插入操作
                    if (dbBusiness.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                    recordId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@RecordId"].Value, 0);
                }
            }
            catch (Exception exception)
            {

                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return recordId;
        }

        /// <summary>
        /// 更新关联表中的记录
        /// </summary>
        /// <param name="associationId">关联编号</param>
        /// <param name="recordId">关联表记录编号</param>
        /// <param name="commonDataFields"></param>
        public void Update(decimal associationId, decimal recordId, IList<CommonDataField> commonDataFields)
        {
            try
            {
                /* 1.更新记录 */
                //获得系统数据库对象
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                StringBuilder sbUpdate = new StringBuilder();
                string physicalName = GetPhysicalName(associationId);
                sbUpdate.Append("UPDATE ");
                sbUpdate.Append(physicalName);
                sbUpdate.Append(" SET ");
                foreach (CommonDataField commonDataField in commonDataFields)
                {
                    sbUpdate.Append(commonDataField.DataFieldName);
                    sbUpdate.Append(" = ");
                    sbUpdate.Append("@");
                    sbUpdate.Append(commonDataField.DataFieldParameterName);
                    sbUpdate.Append(", ");
                }
                sbUpdate.Remove(sbUpdate.Length - 2, 2);
                sbUpdate.Append(" WHERE RecordId = @RecordId");
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sbUpdate.ToString()))
                {
                    //给参数赋值
                    dbBusiness.AddInParameter(dbCommand, "RecordId", DbType.Decimal, DataConvertionHelper.SetDecimal(recordId));
                    DataAccessHandler.AddInParameter(dbBusiness, dbCommand, commonDataFields);
                    //执行插入操作
                    if (dbBusiness.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                }

                /* 2.获得原数据行 */
                DataTable dt = GetAssociationData(associationId, recordId);
                if (dt == null || dt.Rows.Count == 0) return;
                AssociatedDataField associatedDataField = new AssociatedDataField();
                AssociatedDataFieldInfo keyAssociatedDataFieldInfo = associatedDataField.GetKeyAssociatedDataFieldInfo(associationId);
                if (keyAssociatedDataFieldInfo == null)
                {
                    throw new ArgumentException("关联不包含关键字。");
                }
                object keyValue = null;
                if (dt.Columns.Contains(keyAssociatedDataFieldInfo.PhysicalName))
                {
                    keyValue = dt.Rows[0][keyAssociatedDataFieldInfo.PhysicalName];
                }
                if (keyValue == null || keyValue == DBNull.Value) return;

                /* 3. 更新值发生改变的对应的数据库的字段 */              
                /* 先更新非关键字段，再更新关键字段，因为非关键字段的更新依赖关键字段的值 */
                foreach (CommonDataField commonDataField in commonDataFields)
                {
                    if (commonDataField.DataFieldId != keyAssociatedDataFieldInfo.AssociatedDataFieldId)
                    {
                        UpdateBussinessData(associationId, commonDataField, keyAssociatedDataFieldInfo, dt, keyValue);
                    }
                }
                foreach (CommonDataField commonDataField in commonDataFields)
                {
                    if (commonDataField.DataFieldId == keyAssociatedDataFieldInfo.AssociatedDataFieldId)
                    {
                        UpdateBussinessData(associationId, commonDataField, keyAssociatedDataFieldInfo, dt, keyValue);
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
        /// 更新排序
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        /// <param name="movedDriection"></param>
        public void UpdateRecordSorting(decimal associationId, decimal recordId, MovedDriection movedDriection)
        {
            //获得系统数据库对象
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
            string tableName = GetPhysicalName(associationId);
            string dataFieldIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            using (DbConnection connection = dbBusiness.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    string sqlFirstSelect = string.Empty;
                    string sqlSecondSelect = string.Empty;
                    string sqlFirstUpdate = string.Empty;
                    string sqlSecondUpdate = string.Empty;
                    int sorting = 0;
                    int otherSorting = 0;

                    /* 获得父节点编号和当前节点的排序值 */
                    sqlFirstSelect = string.Format("SELECT RecordSorting FROM {0} WHERE {1} = @{1}", tableName, dataFieldIdName);
                    using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlFirstSelect))
                    {
                        //给参数赋值
                        dbBusiness.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, recordId);
                        sorting = Convert.ToInt32(dbBusiness.ExecuteScalar(dbCommand));
                    }

                    switch (movedDriection)
                    {
                        case MovedDriection.Top:
                            string top = string.Format("UPDATE {0} SET RecordSorting = RecordSorting + 1 WHERE RecordSorting < @RecordSorting ", tableName);
                            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(top))
                            {
                                //给参数赋值
                                dbBusiness.AddInParameter(dbCommand, "RecordSorting", DbType.Int32, sorting);
                                dbBusiness.ExecuteNonQuery(dbCommand, transaction);
                            }

                            sqlFirstUpdate = string.Format("UPDATE {0} SET RecordSorting = 1 WHERE {1} = @{1}", tableName, dataFieldIdName);
                            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                dbBusiness.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, recordId);
                                if (dbBusiness.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }
                            break;

                        case MovedDriection.Previous:
                        case MovedDriection.Next:
                            decimal otherRecordId = 0;
                            StringBuilder sb = new StringBuilder();
                            sb.AppendFormat("SELECT TOP 1 {0}, RecordSorting FROM {1} WHERE {0} != @{0} AND RecordSorting ", dataFieldIdName, tableName);
                            if (movedDriection == MovedDriection.Previous)
                            {
                                sb.Append(" < ");
                            }
                            else
                            {
                                sb.Append(" > ");
                            }
                            sb.Append("@RecordSorting ORDER BY RecordSorting ");
                            if (movedDriection == MovedDriection.Previous)
                            {
                                sb.Append("DESC ");
                            }
                            else
                            {
                                sb.Append("ASC ");
                            }
                            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sb.ToString()))
                            {
                                //给参数赋值
                                dbBusiness.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, recordId);
                                dbBusiness.AddInParameter(dbCommand, "RecordSorting", DbType.Int32, sorting);
                                using (IDataReader dataReader = dbBusiness.ExecuteReader(dbCommand))
                                {
                                    if (dataReader.Read())
                                    {
                                        otherRecordId = DataConvertionHelper.GetDecimal(dataReader[0]);
                                        otherSorting = DataConvertionHelper.GetInt(dataReader[1]);
                                    }
                                    if (dataReader != null)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }

                            sqlFirstUpdate = string.Format("UPDATE {0} SET RecordSorting = @RecordSorting WHERE {1} = @{1}", tableName, dataFieldIdName);
                            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                dbBusiness.AddInParameter(dbCommand, "RecordSorting", DbType.Decimal, otherSorting);
                                dbBusiness.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, recordId);
                                if (dbBusiness.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }
                            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlFirstUpdate))
                            {
                                //给参数赋值
                                dbBusiness.AddInParameter(dbCommand, "RecordSorting", DbType.Decimal, sorting);
                                dbBusiness.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, otherRecordId);
                                if (dbBusiness.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }
                            break;

                        case MovedDriection.Bottom:
                            string bottom = string.Format("SELECT MAX(RecordSorting) FROM {0} ", tableName);
                            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(bottom.ToString()))
                            {
                                otherSorting = DataConvertionHelper.GetInt(dbBusiness.ExecuteScalar(dbCommand));
                            }
                            bottom  = string.Format("UPDATE {0} SET RecordSorting = RecordSorting - 1 WHERE RecordSorting > @RecordSorting", tableName);
                            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(bottom))
                            {
                                //给参数赋值
                                dbBusiness.AddInParameter(dbCommand, "RecordSorting", DbType.Int32, sorting);
                                dbBusiness.ExecuteNonQuery(dbCommand, transaction);
                            }

                            bottom = string.Format("UPDATE {0} SET RecordSorting = @RecordSorting WHERE {1} = @{1}", tableName, dataFieldIdName);
                            using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(bottom))
                            {
                                //给参数赋值
                                dbBusiness.AddInParameter(dbCommand, "RecordSorting", DbType.Decimal, otherSorting);
                                dbBusiness.AddInParameter(dbCommand, dataFieldIdName, DbType.Decimal, recordId);
                                if (dbBusiness.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }

                            break;
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
        /// 删除关联表的记录
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        public void Delete(decimal associationId, decimal recordId)
        {
            StringBuilder sbDelete = new StringBuilder();
            string physicalName = GetPhysicalName(associationId);
            sbDelete.Append("DELETE FROM ");
            sbDelete.Append(physicalName);
            sbDelete.Append(" WHERE RecordId = @RecordId");

            //获得系统数据库对象
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
            try
            {
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sbDelete.ToString()))
                {
                    //给参数赋值
                    dbBusiness.AddInParameter(dbCommand, "RecordId", DbType.Decimal, DataConvertionHelper.SetDecimal(recordId));
                    //执行插入操作
                    if (dbBusiness.ExecuteNonQuery(dbCommand) != 1)
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
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetAssociationData(decimal associationId, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                string physicalName = GetPhysicalName(associationId);
                string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                AssociatedDataField associatedDataField = new AssociatedDataField();
                Dictionary<string, string> names = associatedDataField.GetNames(associationId);
                //获得业务数据库对象
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("RecordSorting", CustomSorting.Ascending));
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0} ", key);
                foreach (var name in names)
                {
                    sb.AppendFormat(", {0}", name.Key);
                }
                ds = DataAccessHandler.GetPageRecord(dbBusiness, physicalName, key, sb.ToString(), false, false, startPosition, count, whereConditons, sortingCondtions, ref totalCount);
                foreach (DataColumn dc in ds.Tables[0].Columns)
                {
                    if (names.ContainsKey(dc.ColumnName))
                    {
                        dc.Caption = names[dc.ColumnName];
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
        /// 获得指定列数据
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public DataTable GetAssociationColumnData(decimal associatedDataFieldId)
        {
            DataTable data = null;

            try
            {
                AssociatedDataField associatedDataField = new AssociatedDataField();
                string dataFieldName = associatedDataField.GetPhysicalName(associatedDataFieldId);
                string tablePhysicalName = associatedDataField.GetTablePhysicalName(associatedDataFieldId);
                string select = string.Format("SELECT {0} FROM {1}", dataFieldName, tablePhysicalName);                
                //获得业务数据库对象
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(select))
                {
                    data = dbBusiness.ExecuteDataSet(dbCommand).Tables[0];
                }                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;
        }

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public DataTable GetAssociationDataWithSortingDataField(decimal associationId)
        {
            return GetAssociationData(associationId, true);
        }

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public DataTable GetAssociationData(decimal associationId)
        {
            return GetAssociationData(associationId, false);
        }

        /// <summary>
        /// 根据关联编号对于的表获得相应的记录行
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public DataTable GetAssociationData(decimal associationId, decimal recordId)
        {
            DataTable data = null;

            try
            {
                string physicalName = GetPhysicalName(associationId);
                AssociatedDataField associatedDataField = new AssociatedDataField();
                Dictionary<string, string> names = associatedDataField.GetNames(associationId);
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT ");
                foreach (KeyValuePair<string, string> keyValue in names)
                {
                    sb.AppendFormat("{0}, ", keyValue.Key);
                }
                sb.Remove(sb.Length - 2, 2);
                sb.AppendFormat(" FROM {0} ", physicalName);
                sb.AppendFormat("WHERE RecordId = @RecordId ", physicalName);

                //获得业务数据库对象
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sb.ToString()))
                {
                    dbBusiness.AddInParameter(dbCommand, "RecordId", DbType.Decimal, DataConvertionHelper.SetDecimal(recordId));
                    data = dbBusiness.ExecuteDataSet(dbCommand).Tables[0];
                }
                //foreach (DataColumn dc in data.Columns)
                //{
                //    if (names.ContainsKey(dc.ColumnName))
                //    {
                //        dc.Caption = names[dc.ColumnName];
                //    }
                //}
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得关联的物理名称
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public string GetPhysicalName(decimal associationId)
        {
            string physicalName = string.Empty;

            string sqlSelect = "SELECT PhysicalName FROM CustomAssociation WHERE AssociationId = @AssociationId";

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, DataConvertionHelper.SetDecimal(associationId));
                    physicalName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return physicalName;
        } 

        /// <summary>
        /// 更新节点是否为叶子节点的状态
        /// </summary>        
        /// <param name="nodeId">编号</param>
        /// <param name="isLeaf">是否为叶子节点</param>
        /// <param name="db">数据库对象</param>
        /// <param name="transaction">事务对象</param>
        public void UpdateLeaf(decimal nodeId, bool isLeaf, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            string sqlUpdate = "UPDATE CustomAssociation SET IsLeaf = @IsLeaf WHERE AssociationId = @AssociationId AND IsLeaf != @IsLeaf";
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, DataConvertionHelper.SetDecimal(nodeId));
                    db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, isLeaf);
                    //执行更新操作
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }        

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomAssociationInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomAssociationInfo 对象列表</returns>
        private IList<CustomAssociationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomAssociationInfo> customAssociationInfos = new List<CustomAssociationInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomAssociation");
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
                            decimal associationId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string associationName = DataConvertionHelper.GetString(dataReader[2]);
                            string associationCode = DataConvertionHelper.GetString(dataReader[3]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[4]);
                            byte showMode = DataConvertionHelper.GetByte(dataReader[5]);
                            bool superAssociationEnabled = DataConvertionHelper.GetBoolean(dataReader[6]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[7]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[8]);
                            string notes = DataConvertionHelper.GetString(dataReader[9]);
                            //将创建 CustomAssociationInfo 对象加入集合中
                            customAssociationInfos.Add(new CustomAssociationInfo(associationId, groupId, associationName, associationCode, physicalName,
                            showMode, superAssociationEnabled, isLeaf, sorting, notes));
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

            return customAssociationInfos;
        }

        /// <summary>
        /// 获得 CustomAssociationInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomAssociationInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomAssociation");
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
        /// 获得表 CustomAssociation 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomAssociation ", "AssociationId", "*", false, false, startPosition,
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
        /// 获得以表 CustomAssociation 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomAssociation ", "AssociationId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomAssociation 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomAssociation ", "AssociationId", "*", false, false, startPosition,
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
        /// 获得以表 CustomAssociation 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomAssociation ", "AssociationId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomAssociationInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomAssociation");
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
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="hasSortingDataField"></param>
        /// <returns></returns>
        private DataTable GetAssociationData(decimal associationId, bool hasSortingDataField)
        {
            DataTable data = null;

            try
            {
                string physicalName = GetPhysicalName(associationId);
                AssociatedDataField associatedDataField = new AssociatedDataField();
                Dictionary<string, string> names = associatedDataField.GetNames(associationId);
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT RecordId");
                if (hasSortingDataField)
                {
                    sb.Append(", RecordSorting");
                }
                foreach (KeyValuePair<string, string> keyValue in names)
                {
                    sb.AppendFormat(", {0}", keyValue.Key);
                }
                sb.AppendFormat(" FROM {0} ", physicalName);
                sb.Append("ORDER BY RecordSorting");

                //获得业务数据库对象
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sb.ToString()))
                {
                    data = dbBusiness.ExecuteDataSet(dbCommand).Tables[0];
                }
                foreach (DataColumn dc in data.Columns)
                {
                    if (names.ContainsKey(dc.ColumnName))
                    {
                        dc.Caption = names[dc.ColumnName];
                    }
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;
        }

        /// <summary>
        /// 生成物理表,由调用者保证事务的完整性
        /// </summary>
        /// <param name="physcialDataTableName"></param>
        private void CreatePhysicalTable(string physcialDataTableName)
        {            
            string sqCreation = string.Format("CREATE TABLE {0} (RecordId decimal(10,0) identity(1,1) primary key, RecordSorting int NOT NULL)", physcialDataTableName);            
            try
            {                
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                DataAccessHandler.DeletePhysicalTable(db, physcialDataTableName);
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqCreation))
                {
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新业务数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="commonDataField"></param>
        /// <param name="keyAssociatedDataFieldInfo"></param>
        /// <param name="dt"></param>
        /// <param name="keyValue"></param>
        private void UpdateBussinessData(decimal associationId, CommonDataField commonDataField, AssociatedDataFieldInfo keyAssociatedDataFieldInfo, DataTable dt, object keyValue)
        {
            if (dt.Columns.Contains(commonDataField.DataFieldName))
            {
                CustomTable customTable = new CustomTable();
                CustomDataField customDataField = new CustomDataField();
                AssociatedDataField associatedDataField = new AssociatedDataField();
                bool change = false;
                object originalValue = dt.Rows[0][commonDataField.DataFieldName];
                DbType dataFieldDataType = (DbType)commonDataField.DataFieldDataType;
                switch (dataFieldDataType)
                {
                    case DbType.Boolean:
                        if (DataConvertionHelper.GetBoolean(originalValue) != DataConvertionHelper.GetBoolean(commonDataField.DataFieldValue))
                        {
                            change = true;
                        }
                        break;

                    case DbType.Int32:
                        if (DataConvertionHelper.GetInt(originalValue) != DataConvertionHelper.GetInt(commonDataField.DataFieldValue))
                        {
                            change = true;
                        }
                        break;

                    case DbType.Decimal:
                        if (DataConvertionHelper.GetDecimal(originalValue) != DataConvertionHelper.GetDecimal(commonDataField.DataFieldValue))
                        {
                            change = true;
                        }
                        break;

                    case DbType.String:
                        if (!DataConvertionHelper.GetString(originalValue).Equals(DataConvertionHelper.GetString(commonDataField.DataFieldValue)))
                        {
                            change = true;
                        }
                        break;

                    case DbType.DateTime:
                        if (DataConvertionHelper.GetDateTime(originalValue) != DataConvertionHelper.GetDateTime(commonDataField.DataFieldValue))
                        {
                            change = true;
                        }
                        break;

                    default:
                        throw new ArgumentException();
                }
                if (change)
                {
                    AssociatedDataFieldInfo associatedDataFieldInfo = associatedDataField.GetModelInfo(commonDataField.DataFieldId);
                    IList<CustomDataFieldInfo> customDataFieldInfos = customDataField.GetModelsByAssociatedDataFieldId(associationId);
                    foreach (var customDataFieldInfo in customDataFieldInfos)
                    {
                        string tablePhysicalName = customTable.GetTablePhysicalName(customDataFieldInfo.TableId);
                        byte dataWarehouseId = customTable.GetDataWarehouseId(customDataFieldInfo.TableId);
                        SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("UPDATE {0} SET {1} = @DataFieldValue_0", tablePhysicalName, commonDataField.DataFieldName);
                        /* 非关联的关键字则需要与关键字关联起来更新 */
                        AssociatedDataFieldCategory dataFieldCategory = (AssociatedDataFieldCategory)associatedDataFieldInfo.DataFieldCategory;
                        switch (dataFieldCategory)
                        {
                            case AssociatedDataFieldCategory.PrimaryDataField:
                                sb.AppendFormat(" WHERE {0} = @DataFieldValue_1 ", customDataFieldInfo.PhysicalName);
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                                {
                                    //给参数赋值 
                                    DataAccessHandler.AddInParameter(db, dbCommand, "DataFieldValue_0", dataFieldDataType, commonDataField.DataFieldValue);
                                    DataAccessHandler.AddInParameter(db, dbCommand, "DataFieldValue_1", dataFieldDataType, dt.Rows[0][customDataFieldInfo.PhysicalName]);
                                }
                                break;

                            case AssociatedDataFieldCategory.AssociatedDataField:
                                CustomDataFieldInfo parentCustomDataFieldInfo = customDataField.GetModelInfo(customDataFieldInfo.ParentDataFieldId);
                                if (parentCustomDataFieldInfo.TableId == customDataFieldInfo.TableId)
                                {
                                    sb.AppendFormat("WHERE {0} = @DataFieldValue_1 AND {1} = @{1} ", customDataFieldInfo.PhysicalName, parentCustomDataFieldInfo.PhysicalName);
                                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                                    {
                                        //给参数赋值 
                                        DataAccessHandler.AddInParameter(db, dbCommand, "DataFieldValue_0", dataFieldDataType, commonDataField.DataFieldValue);
                                        DataAccessHandler.AddInParameter(db, dbCommand, "DataFieldValue_1", dataFieldDataType, dt.Rows[0][customDataFieldInfo.PhysicalName]);
                                    }
                                }
                                else
                                {
                                    string keyTablePhysicalName = customTable.GetTablePhysicalName(parentCustomDataFieldInfo.TableId);
                                    sb.AppendFormat("WHERE {0} = @DataFieldValue_1 AND UserId IN (SELECT UserId FROM {1} ", customDataFieldInfo.PhysicalName, keyTablePhysicalName);
                                    sb.AppendFormat(" WHERE {0} = @{0})", parentCustomDataFieldInfo.PhysicalName);
                                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                                    {
                                        //给参数赋值 
                                        DataAccessHandler.AddInParameter(db, dbCommand, "DataFieldValue_0", dataFieldDataType, commonDataField.DataFieldValue);
                                        DataAccessHandler.AddInParameter(db, dbCommand, "DataFieldValue_1", dataFieldDataType, dt.Rows[0][customDataFieldInfo.PhysicalName]);
                                        if (dataFieldCategory == AssociatedDataFieldCategory.AssociatedDataField)
                                        {
                                            BasedDataType basedDataType = (BasedDataType)keyAssociatedDataFieldInfo.BasedDataType;
                                            DataAccessHandler.AddInParameter(db, dbCommand, parentCustomDataFieldInfo.PhysicalName, DataFieldHelper.GetDbType(basedDataType), keyValue);
                                            db.ExecuteNonQuery(dbCommand);
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}
