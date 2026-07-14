//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomTable.cs
// 描述：CustomTable 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/9/11
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
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
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.BusinessDesignerModule;
using Blue.SQLServerDAL.SystemModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomTable 表的数据层访问类
    /// </summary>
    public class CustomTable : CommonNodeDataAccess, ICustomTable
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomTable() : base("CustomTable", "TableId", "CategoryId", "LogicalName", "TableCode", false, false)
        {
        }

        #endregion

        #region 实现默认接口        

        /// <summary>
        /// 向 CustomTable 表中插入一条新记录
        /// </summary>
        /// <param name="customTableInfo">customTableInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomTableInfo customTableInfo)
        {
            //自动增加的关键字的值
            decimal customTableId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    customTableId = Insert(customTableInfo, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customTableId;
        }

        /// <summary>
		/// 获得 CustomTableInfo 对象
		/// </summary>
		///<param name="tableId">表编号</param>
		/// <returns> CustomTableInfo 对象</returns>
		public CustomTableInfo GetModelInfo(decimal tableId)
        {
            CustomTableInfo customTableInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("TableId", "TableId", System.Data.DbType.Decimal, tableId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomTableInfo> customTableInfos = GetModelInfos(whereConditons, null, true);
            if (customTableInfos != null && customTableInfos.Count > 0)
            {
                customTableInfo = customTableInfos[0];
            }

            return customTableInfo;
        }

        /// <summary>
        /// 更新 CustomTableInfo 对象
        /// </summary>
        /// <param name="customTableInfo">CustomTableInfo 对象</param>
        public void Update(CustomTableInfo customTableInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomTable SET LogicalName = @LogicalName, TableCode = @TableCode, TableProperty = @TableProperty, TableType = @TableType, ");
            sb.Append("SystemTable = @SystemTable, TableSetting = @TableSetting, Notes = @Notes ");
            sb.Append("WHERE TableId = @TableId");

            DataTableType oldDataTableType = GetDataTableType(customTableInfo.TableId);
            byte dataWarehouseId = GetDataWarehouseId(customTableInfo.TableId);

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
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, customTableInfo.TableId);
                        db.AddInParameter(dbCommand, "LogicalName", DbType.String, customTableInfo.LogicalName);
                        db.AddInParameter(dbCommand, "TableCode", DbType.String, customTableInfo.TableCode);
                        db.AddInParameter(dbCommand, "TableProperty", DbType.Byte, customTableInfo.TableProperty);
                        db.AddInParameter(dbCommand, "TableType", DbType.Byte, customTableInfo.TableType);
                        db.AddInParameter(dbCommand, "SystemTable", DbType.Boolean, customTableInfo.SystemTable);
                        db.AddInParameter(dbCommand, "TableSetting", DbType.Byte, customTableInfo.TableSetting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customTableInfo.Notes);
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }
                    if (oldDataTableType != (DataTableType)customTableInfo.TableType)
                    {
                        UpdateIndexOnTable(dataWarehouseId, customTableInfo.PhysicalName, (DataTableType)customTableInfo.TableType);
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
        ///  删除 CustomTableInfo 对象
        /// </summary>
        ///<param name="tableId">表编号</param>
        public void Delete(decimal tableId)
        {
            //删除语句
            string sqlDelete = "DELETE FROM CustomTable WHERE TableId = @TableId";

            CustomDataField customDataField = new CustomDataField();
            DataAuditingAndDataField dataAuditingAndDataField = new DataAuditingAndDataField();
            CombinedDataField combinedDataField = new CombinedDataField();
            RoleAndDataField roleAndDataField = new RoleAndDataField();
            RoleAndTable roleAndTable = new RoleAndTable();
            CustomViewAndDataField customViewAndDataField = new CustomViewAndDataField();
            CustomQueyAndDataField customQueyAndDataField = new CustomQueyAndDataField();
            CustomPrint customPrint = new CustomPrint();
            CustomPrintAndDataField customPrintAndDataField = new CustomPrintAndDataField();
            CustomQuey customQuey = new CustomQuey();
            CustomForm customForm = new CustomForm();
            CustomCell customCell = new CustomCell();
            CombinedTableRelation combinedTableRelation = new CombinedTableRelation();
            DataAuditing dataAuditing = new DataAuditing();
            CustomBusiness customBusiness = new CustomBusiness();
            CustomView customView = new CustomView();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string tablePhysicalName = GetTablePhysicalName(tableId);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    roleAndTable.Delete(tableId, db, transaction);
                    roleAndDataField.DeleteByTableId(tableId, db, transaction);
                    combinedDataField.DeleteByTableId(tableId, db, transaction);
                    dataAuditingAndDataField.DeleteByTableId(tableId, db, transaction);
                    customViewAndDataField.DeleteByTableId(tableId, db, transaction);
                    customQueyAndDataField.DeleteByTableId(tableId, db, transaction);
                    customDataField.DeleteRecords(tableId, db, transaction);
                    customQuey.DeleteByTableId(tableId, db, transaction);
                    customPrintAndDataField.DeleteByTableId(tableId, db, transaction);
                    customPrint.DeleteByTableId(tableId, db, transaction);
                    customForm.DeleteByTableId(tableId, db, transaction);
                    customCell.DeleteByTableId(tableId, db, transaction);
                    combinedTableRelation.DeleteBySecondForeignKey(tableId, db, transaction);
                    customBusiness.DeleteByTableId(tableId, db, transaction);
                    dataAuditing.DeleteByTableId(tableId, db, transaction);
                    customView.DeleteByTableId(tableId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                    {
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
                        }
                    }
                    DeleteLogTable(tablePhysicalName);
                    DeletePhysicalTable(dataWarehouseId, tablePhysicalName);
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
        /// 获得 CustomTableInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomTableInfo 对象列表</returns>
        public IList<CustomTableInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomTable 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomTableInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomTable ", "TableId", false, whereConditons);
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
        /// 检查枚举一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckEnumConsistency(decimal tableId, bool selectOrUpdate)
        {
            IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> results = new Dictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>>();
            
            CustomDataField customDataField = new CustomDataField();
            CustomEnum customEnum = new CustomEnum();
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tablePhysicalName = GetTablePhysicalName(tableId);
            /* 1. 枚举类型 */
            IList<CommonNode> commonNodes = customDataField.GetCommonNodes(tableId, DataFieldFilter.EnumTypeInPhysicalField);
            foreach (CommonNode commonNode in commonNodes)
            {
                IList<CommonNode> relyOnCommonNodes = customDataField.GetCommonNodesByParentDataFieldId(commonNode.NodeId);
                if (relyOnCommonNodes.Count == 0)
                {
                    continue;
                }
                IDictionary<decimal, CommonItemList<int, string>> result = new Dictionary<decimal, CommonItemList<int, string>>();
                results.Add(commonNode.NodeId, result);
                CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(commonNode.NodeId);
                IList<CommonNode> enumCommonNodes = null;
                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.DropdownListEnum:
                    case PhysicalDataFieldType.DropdownListEnumValue:
                    case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                    case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                        enumCommonNodes = customEnum.GetChildNodes(customDataFieldInfo.EnumId);
                        break;

                    case PhysicalDataFieldType.TreeViewEnum:
                    case PhysicalDataFieldType.TreeViewEnumValue:
                    case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                        string parentNodeCode = customEnum.GetEnumCode(customDataFieldInfo.EnumId);
                        enumCommonNodes = customEnum.GetChildNodesByParentNodeCode(parentNodeCode);
                        break;

                    default:
                        continue;
                }
                foreach (CommonNode relyOnCommonNode in relyOnCommonNodes)
                {
                    CustomDataFieldInfo dataFieldInfo = customDataField.GetModelInfo(relyOnCommonNode.NodeId);
                    StringBuilder sb = new StringBuilder();
                    int count = 0;
                    if (selectOrUpdate)
                    {
                        sb.AppendFormat("SELECT UserName FROM {0} ", tablePhysicalName);
                    }
                    else
                    {
                        sb.AppendFormat("UPDATE {0} SET {1} = @{1} ", tablePhysicalName, dataFieldInfo.PhysicalName);
                    }
                    sb.AppendFormat("WHERE {0} = @{0} AND ({1} != @{1} OR {1} IS NULL)", customDataFieldInfo.PhysicalName, dataFieldInfo.PhysicalName);
                    CommonItemList<int, string> commonItemList = new CommonItemList<int, string>();
                    foreach (CommonNode enumCommonNode in enumCommonNodes)
                    {
                        CustomEnumInfo customEnumInfo = customEnum.GetModelInfo(enumCommonNode.NodeId);
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            db.AddInParameter(dbCommand, customDataFieldInfo.PhysicalName, DbType.String, customEnum.GetEnumResult(customEnumInfo, (PhysicalDataFieldType)customDataFieldInfo.DataFieldType));
                            PhysicalDataFieldType dataFieldType = (PhysicalDataFieldType)dataFieldInfo.DataFieldType;
                            object value = customEnum.GetEnumResult(customEnumInfo, dataFieldType);
                            DbType type = DbType.String;
                            switch (dataFieldType)
                            {
                                case PhysicalDataFieldType.EnumNameDependency:
                                case PhysicalDataFieldType.EnumValue:
                                case PhysicalDataFieldType.FstAdditionalCode:
                                case PhysicalDataFieldType.ScdAdditionalCode:
                                case PhysicalDataFieldType.FstAdditionalString:
                                case PhysicalDataFieldType.ScdAdditionalString:
                                case PhysicalDataFieldType.TrdAdditionalString:
                                case PhysicalDataFieldType.FourthAdditionalString:
                                case PhysicalDataFieldType.FifthAdditionalString:
                                case PhysicalDataFieldType.SixthAdditionalString:;
                                    type = DbType.String;
                                    break;

                                case PhysicalDataFieldType.FstAdditionalInteger:
                                case PhysicalDataFieldType.ScdAdditionalInteger:
                                    type = DbType.Int32;
                                    value = DataConvertionHelper.SetInt(DataConvertionHelper.GetConvertedInt(value));
                                    break;

                                case PhysicalDataFieldType.FstAdditionalDecimal:
                                case PhysicalDataFieldType.ScdAdditionalDecimal:
                                    type = DbType.Decimal;
                                    value = DataConvertionHelper.SetDecimal(DataConvertionHelper.GetDecimal(value));
                                    break;                                    
                            }                            
                            db.AddInParameter(dbCommand, dataFieldInfo.PhysicalName, type, value);
                            if (selectOrUpdate)
                            {
                                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                                {
                                    while (dataReader.Read())
                                    {
                                        string userName = DataConvertionHelper.GetString(dataReader[0]);
                                        commonItemList.CommonList.Add(userName);
                                        count++;
                                    }
                                    if (dataReader != null)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            else
                            {
                                count += db.ExecuteNonQuery(dbCommand);
                            }
                        }
                    }
                    commonItemList.Value = count;
                    result.Add(relyOnCommonNode.NodeId, commonItemList);
                }
            }

            return results;
        }

        /// <summary>
        /// 检查用户数据冗余性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public CommonItemList<int, string> CheckUserDataConsistency(decimal tableId, bool selectOrUpdate)
        {
            CommonItemList<int, string> userData = new CommonItemList<int, string>();

             byte dataWarehouseId = GetDataWarehouseId(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            CustomTableInfo customTableInfo = GetModelInfo(tableId);
            string systemTableName = DataFieldHelper.GetSystemTableName(customTableInfo.PhysicalName, SystemDataField.UserActualName);
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            string userName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName);

            StringBuilder sb = new StringBuilder();
            if (selectOrUpdate)
            {
                sb.AppendFormat("SELECT {0}.{1} FROM {0} ", customTableInfo.PhysicalName, userName);                
            }
            else
            {
                sb.AppendFormat("DELETE {0} FROM {0} ", customTableInfo.PhysicalName);
            }
            sb.AppendFormat("LEFT JOIN [Blue].[dbo].[UserAccount] ON {0}.{1} = UserAccount.{1} ", customTableInfo.PhysicalName, userIdName);
            sb.AppendFormat("WHERE UserAccount.{0} IS NULL", userIdName);
            userData.Text = string.Format("{0}({1})", customTableInfo.LogicalName, customTableInfo.PhysicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                if (selectOrUpdate)
                {
                    int count = 0;
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            userData.CommonList.Add(DataConvertionHelper.GetString(dataReader[0]));
                            count++;
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                        userData.Value = count;
                    }
                }
                else
                {
                    userData.Text = "用户不存在的记录";
                    userData.Value = (int)db.ExecuteNonQuery(dbCommand);
                }
            }

            return userData;
        }

        /// <summary>
        /// 检查用户一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<SystemPhysicalDataField, CommonItemList<int, string>> CheckUserConsistency(decimal tableId, bool selectOrUpdate)
        {
            Dictionary<SystemPhysicalDataField, CommonItemList<int, string>> results = new Dictionary<SystemPhysicalDataField, CommonItemList<int, string>>();

            byte dataWarehouseId = GetDataWarehouseId(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tablePhysicalName = GetTablePhysicalName(tableId);
            string systemTableName = DataFieldHelper.GetSystemTableName(tablePhysicalName, SystemDataField.UserActualName);
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            string userName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName);
            string userTypeIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserTypeId);
            string userDepIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.DepId);

            StringBuilder sb = new StringBuilder();
            if (selectOrUpdate)
            {
                sb.AppendFormat("SELECT {0}.UserName FROM {0} INNER JOIN {1} ON {0}.{2} = {1}.{2} ", tablePhysicalName, systemTableName, userIdName);
            }
            else
            {
                sb.AppendFormat("UPDATE {0} ", tablePhysicalName);
            }
            Dictionary<SystemPhysicalDataField, string> dataFieldNames = new Dictionary<SystemPhysicalDataField, string>();
            dataFieldNames.Add(SystemPhysicalDataField.UserName, userName);
            dataFieldNames.Add(SystemPhysicalDataField.UserTypeId, userTypeIdName);
            dataFieldNames.Add(SystemPhysicalDataField.DepId, userDepIdName);
            StringBuilder sbDataField = new StringBuilder();
            foreach (KeyValuePair<SystemPhysicalDataField, string> keyValue in dataFieldNames)
            {
                sbDataField.Clear();
                sbDataField.Append(sb);
                CommonItemList<int, string> result = new CommonItemList<int, string>();
                results.Add(keyValue.Key, result);
                switch (keyValue.Key)
                {
                    case SystemPhysicalDataField.UserName:
                        if (!selectOrUpdate)
                        {
                            sbDataField.AppendFormat("SET {0}.{2} = {1}.{2} FROM {0} INNER JOIN {1} ON {0}.{3} = {1}.{3} ", tablePhysicalName, systemTableName, userName, userIdName);

                        }
                        sbDataField.AppendFormat("WHERE {0}.{2} != {1}.{2}", tablePhysicalName, systemTableName, userName);
                        break;

                    case SystemPhysicalDataField.UserTypeId:
                        if (!selectOrUpdate)
                        {
                            sbDataField.AppendFormat("SET {0}.{2} = {1}.{2} FROM {0} INNER JOIN {1} ON {0}.{3} = {1}.{3} ", tablePhysicalName, systemTableName, userTypeIdName, userIdName);
                        }
                        sbDataField.AppendFormat("WHERE {0}.{2} != {1}.{2}", tablePhysicalName, systemTableName, userTypeIdName);
                        break;

                    case SystemPhysicalDataField.DepId:
                        if (!selectOrUpdate)
                        {
                            sbDataField.AppendFormat("SET {0}.{2} = {1}.{2} FROM {0} INNER JOIN {1} ON {0}.{3} = {1}.{3} ", tablePhysicalName, systemTableName, userDepIdName, userIdName);
                        }
                        sbDataField.AppendFormat("WHERE {0}.{2} != {1}.{2}", tablePhysicalName, systemTableName, userDepIdName);
                        break;
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sbDataField.ToString()))
                {
                    if (selectOrUpdate)
                    {
                        int count = 0;
                        using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                        {
                            while (dataReader.Read())
                            {
                                result.CommonList.Add(DataConvertionHelper.GetString(dataReader[0]));
                                count++;
                            }
                            if (dataReader != null)
                            {
                                dataReader.Close();
                            }
                            result.Value = count;
                        }
                    }
                    else
                    {
                        result.Value = db.ExecuteNonQuery(dbCommand);
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 检查关联一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckAssociationConsistency(decimal tableId, bool selectOrUpdate)
        {
            IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> results = new Dictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>>();
            
            CustomDataField customDataField = new CustomDataField();
            CustomAssociation customAssociation = new CustomAssociation();
            AssociatedDataField associatedDataField = new AssociatedDataField();
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            Database db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tablePhysicalName = GetTablePhysicalName(tableId);

            /* 1. 关联类型 */
            IList<CommonNode> commonNodes = customDataField.GetCommonNodes(tableId, DataFieldFilter.PrimaryAssociationInPhysicalField);
            foreach (CommonNode commonNode in commonNodes)
            {
                CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(commonNode.NodeId);
                IList<CommonNode> relyOnCommonNodes = customDataField.GetCommonNodesByParentDataFieldId(commonNode.NodeId);
                if (relyOnCommonNodes.Count == 0)
                {
                    continue;
                }
                IDictionary<decimal, CommonItemList<int, string>> result = new Dictionary<decimal, CommonItemList<int, string>>();
                results.Add(customDataFieldInfo.DataFieldId, result);
                decimal associationId = associatedDataField.GetAssociationId(customDataFieldInfo.AssociatedDataFieldId);
                DataTable dataTable = customAssociation.GetAssociationData(associationId);
                AssociatedDataFieldInfo mainAssociatedDataFieldInfo = associatedDataField.GetModelInfo(customDataFieldInfo.AssociatedDataFieldId);
                string mainPhysicalName = mainAssociatedDataFieldInfo.PhysicalName;
                DbType mainType = DataFieldHelper.GetDataType((BasedDataType)mainAssociatedDataFieldInfo.BasedDataType);
                foreach (CommonNode node in relyOnCommonNodes)
                {
                    CustomDataFieldInfo dataFieldInfo = customDataField.GetModelInfo(node.NodeId);
                    string physicalName = dataFieldInfo.PhysicalName;
                    AssociatedDataFieldInfo associatedDataFieldInfo = associatedDataField.GetModelInfo(dataFieldInfo.AssociatedDataFieldId);
                    string associatedPhysicalName = associatedDataFieldInfo.PhysicalName;
                    DbType associatedType = DataFieldHelper.GetDataType((BasedDataType)associatedDataFieldInfo.BasedDataType);

                    StringBuilder sb = new StringBuilder();
                    int count = 0;
                    if (selectOrUpdate)
                    {
                        sb.AppendFormat("SELECT {0}.UserName FROM {0} ", tablePhysicalName);
                        if (node.ParentNodeId != customDataFieldInfo.TableId)
                        {
                            string otherPhysicalName = GetTablePhysicalName(node.ParentNodeId);
                            sb.AppendFormat("INNER JOIN {0} ON {1}.UserId = {0}.UserId ", otherPhysicalName, tablePhysicalName);
                        }
                    }
                    else
                    {
                        if (node.ParentNodeId != customDataFieldInfo.TableId)
                        {
                            string otherPhysicalName = GetTablePhysicalName(node.ParentNodeId);
                            sb.AppendFormat("UPDATE {0} SET {1} = @{1} FROM {0} INNER JOIN {2} ON {2}.UserId = {0}.UserId ",
                                otherPhysicalName, physicalName, tablePhysicalName);
                        }
                        else
                        {
                            sb.AppendFormat("UPDATE {0} SET {1} = @{1} ", tablePhysicalName, physicalName);
                        }
                    }
                    sb.AppendFormat("WHERE {0} = @{0} AND ({1} != @{1} OR {1} IS NULL)", customDataFieldInfo.PhysicalName, physicalName);
                    CommonItemList<int, string> commonItemList = new CommonItemList<int, string>();
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {                            
                            db.AddInParameter(dbCommand, customDataFieldInfo.PhysicalName, mainType, dr[mainPhysicalName]);
                            db.AddInParameter(dbCommand, physicalName, associatedType, dr[associatedPhysicalName]);
                            if (selectOrUpdate)
                            {
                                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                                {
                                    while (dataReader.Read())
                                    {
                                        string userName = DataConvertionHelper.GetString(dataReader[0]);
                                        commonItemList.CommonList.Add(userName);
                                        count++;
                                    }
                                    if (dataReader != null)
                                    {
                                        dataReader.Close();
                                    }
                                }
                            }
                            else
                            {                                
                                count += db.ExecuteNonQuery(dbCommand);
                            }
                        }
                    }
                    commonItemList.Value = count;
                    result.Add(node.NodeId, commonItemList);
                }
            }

            return results;
        }

        /// <summary>
        /// 检查联动一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckRelationConsistency(decimal tableId, bool selectOrUpdate)
        {
            IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> results = new Dictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>>();

            CustomDataField customDataField = new CustomDataField();
            DataFieldRelationship dataFieldRelationship = new DataFieldRelationship();
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            Database db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            CustomTableInfo customTableInfo = GetModelInfo(tableId);
            DataTableType dataTableType = (DataTableType)customTableInfo.TableType;
            string currentStateName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState);

            /* 1. 联动类型 */
            IList<CustomDataFieldInfo> customDataFieldInfos = customDataField.GetModelInfos(tableId);
            foreach (var customDataFieldInfo in customDataFieldInfos)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                if (dataFieldProperty != DataFieldProperty.PhysicalDataField) continue;
                bool relationship = AuthorityHelper.CheckAuthority(customDataFieldInfo.DataFieldSetting, (byte)DataFieldSetting.Correlation);
                if (relationship)
                {
                    IList<CommonNode> relationCommonNodes = dataFieldRelationship.GetRelationDataFields(customDataFieldInfo.DataFieldId);
                    if (relationCommonNodes.Count > 0)
                    {
                        IDictionary<decimal, CommonItemList<int, string>> result = new Dictionary<decimal, CommonItemList<int, string>>();
                        results.Add(customDataFieldInfo.DataFieldId, result);
                        foreach (var relationCommonNode in relationCommonNodes)
                        {
                            StringBuilder sb = new StringBuilder();
                            int count = 0;
                            decimal relationTableId = customDataField.GetTableId(relationCommonNode.NodeId);
                            string relationTableName = GetTablePhysicalName(relationTableId);
                            if (selectOrUpdate)
                            {
                                sb.AppendFormat("SELECT {0}.UserName FROM {0} ", relationTableName);
                                if (relationTableId != customDataFieldInfo.TableId)
                                {
                                    sb.AppendFormat("INNER JOIN {0} ON {1}.UserId = {0}.UserId ", customTableInfo.PhysicalName, relationTableName);
                                }
                            }
                            else
                            {
                                if (relationTableId != customDataFieldInfo.TableId)
                                {
                                    sb.AppendFormat("UPDATE {0} SET {1} = {2} FROM {0} INNER JOIN {3} ON {3}.UserId = {0}.UserId ",
                                        relationTableName, relationCommonNode.NodeCode, customDataFieldInfo.PhysicalName, customTableInfo.PhysicalName);
                                }
                                else
                                {
                                    sb.AppendFormat("UPDATE {0} SET {1} = {2} ", customTableInfo.PhysicalName, relationCommonNode.NodeCode, customDataFieldInfo.PhysicalName);
                                }
                            }
                            sb.AppendFormat("WHERE (({0} IS NOT NULL AND {1} IS NULL ) OR ({0} IS NULL AND {1} IS NOT NULL ) OR {0} != {1}) ", customDataFieldInfo.PhysicalName, relationCommonNode.NodeCode);
                            if (dataTableType == DataTableType.MasterSlaveTable)
                            {
                                sb.AppendFormat("AND {0}.{1} = @{1}", customTableInfo.PhysicalName, currentStateName);
                            }
                            CommonItemList<int, string> commonItemList = new CommonItemList<int, string>();

                            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                            {
                                if (dataTableType == DataTableType.MasterSlaveTable)
                                {
                                    db.AddInParameter(dbCommand, currentStateName, DbType.Byte, (byte)CurrentState.Current);
                                }
                                if (selectOrUpdate)
                                {
                                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                                    {
                                        while (dataReader.Read())
                                        {
                                            string userName = DataConvertionHelper.GetString(dataReader[0]);
                                            commonItemList.CommonList.Add(userName);
                                            count++;
                                        }
                                        if (dataReader != null)
                                        {
                                            dataReader.Close();
                                        }
                                    }
                                }
                                else
                                {
                                    count += db.ExecuteNonQuery(dbCommand);
                                }
                            }
                            commonItemList.Value = count;
                            result.Add(relationCommonNode.NodeId, commonItemList);
                        }
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 设置审核状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="auditedStatus"></param>
        /// <returns></returns>
        public int SetAuditedStatus(decimal tableId, IList<WhereConditon> whereConditons, AuditedStatus auditedStatus)
        {
            int count = 0;

            CustomTable customTable = new CustomTable();
            byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
            string dataFieldPhysicalName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} SET {1} = @{1} ", tablePhysicalName, dataFieldPhysicalName);            
            sb.AppendFormat("FROM {0} INNER JOIN [Blue].[dbo].[UserAccount] ON {0}.UserId = [Blue].[dbo].[UserAccount].UserId ", tablePhysicalName);
            if (whereConditons != null && whereConditons.Count > 0)
            {
                string condition = DataAccessHandler.GetConditionSentence(whereConditons);
                sb.AppendFormat("WHERE {0} ", condition);
            }
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                db.AddInParameter(dbCommand, dataFieldPhysicalName, DbType.Byte, (byte)auditedStatus);
                if (whereConditons != null && whereConditons.Count > 0)
                {
                    DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                }
                count = db.ExecuteNonQuery(dbCommand);
            }

            return count;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        public void Import(decimal tableId, DataTable dataTable)
        {
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string key = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
            string tablePhysicalName = GetTablePhysicalName(tableId);

            try
            {
                string destConnectionString = DataAccessHelper.GetConnectionString(key);
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(destConnectionString, SqlBulkCopyOptions.UseInternalTransaction);
                sqlBulkCopy.DestinationTableName = tablePhysicalName;
                sqlBulkCopy.BulkCopyTimeout = 500000000;
                foreach (DataColumn dc in dataTable.Columns)
                {
                    sqlBulkCopy.ColumnMappings.Add(dc.ColumnName, dc.ColumnName);
                }
                sqlBulkCopy.WriteToServer(dataTable);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 清除表
        /// </summary>
        /// <param name="tableId"></param>
        public void TruncatedTable(decimal tableId)
        {
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string key = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
            SqlDatabase db = DataAccessHelper.GetDatabase(key);
            string tablePhysicalName = GetTablePhysicalName(tableId);
            string truncate = string.Format("TRUNCATE TABLE {0}", tablePhysicalName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(truncate))
            {
                //给参数赋值
                db.ExecuteNonQuery(dbCommand);
            }
        }

        /// <summary>
        /// 提交更新
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="exportStyle"></param>
        /// <returns></returns>
        public int Sumbit(decimal tableId, ExportStyle exportStyle)
        {
            int count = 0;
            
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string key = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
            SqlDatabase db = DataAccessHelper.GetDatabase(key);
            string tablePhysicalName = GetTablePhysicalName(tableId);
            string creationTimeName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime);
            string modificationTimeName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime);
            StringBuilder sb = new StringBuilder();
            switch (exportStyle)
            {
                case ExportStyle.Append:
                case ExportStyle.NotUpdateAndInsert:
                case ExportStyle.UpdateAndInsert:
                    sb.AppendFormat("UPDATE {0} SET ", tablePhysicalName);
                    sb.AppendFormat("{0} = @CurrentDateTime, {1} = @CurrentDateTime ", creationTimeName, modificationTimeName);
                    sb.AppendFormat("WHERE {0} = @ConditioalDateTime", creationTimeName);
                    UpdateSorting(db, tablePhysicalName);
                    break;
                    
                case ExportStyle.UpdateAndNotInsert:
                    sb.AppendFormat("UPDATE {0} SET ", tablePhysicalName);
                    sb.AppendFormat("{0} = @CurrentDateTime ", modificationTimeName);
                    sb.AppendFormat("WHERE {0} = @ConditioalDateTime ", modificationTimeName);
                    break;
            }
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                //给参数赋值                 
                db.AddInParameter(dbCommand, "CurrentDateTime", DbType.DateTime, DateTime.Now);
                db.AddInParameter(dbCommand, "ConditioalDateTime", DbType.DateTime, DateTime.Parse(AppSettingHelper.YearMonthDay));
                count = (int)db.ExecuteNonQuery(dbCommand);
            }
            if (exportStyle == ExportStyle.UpdateAndInsert)
            {
                sb.Clear();
                sb.AppendFormat("UPDATE {0} SET ", tablePhysicalName);
                sb.AppendFormat("{0} = @CurrentDateTime ", modificationTimeName);
                sb.AppendFormat("WHERE {0} = @ConditioalDateTime", modificationTimeName);
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值                 
                    db.AddInParameter(dbCommand, "CurrentDateTime", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "ConditioalDateTime", DbType.DateTime, DateTime.Parse(AppSettingHelper.YearMonthDay));
                    count += (int)db.ExecuteNonQuery(dbCommand);
                }
            }

            return count;
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataTable">第0列为 UserId, 其他为数据列</param>
        /// <param name="currentState">主从表更新条件</param>
        /// <param name="whereCluases"></param>
        public void Update(decimal tableId, DataTable dataTable, CurrentState currentState, IList<string> whereCluases)
        {
            string creationTime = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime);
            if (dataTable.Columns.Contains(creationTime))
            {
                dataTable.Columns.Remove(creationTime);
            }
            
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string key = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
            string curretStateName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState);
            SqlDatabase db = DataAccessHelper.GetDatabase(key);
            string tablePhysicalName = GetTablePhysicalName(tableId);
            DataTableType dataTableType = (DataTableType)GetTableType(tableId);
            string userId = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("UPDATE {0} SET ", tablePhysicalName);
            foreach (DataColumn dc in dataTable.Columns)
            {
                if (!userId.Equals(dc.ColumnName))
                {
                    sb.AppendFormat("{0} = @{0}, ", dc.ColumnName);
                }
            }
            sb.Remove(sb.Length - 2, 2);
            sb.AppendFormat(" WHERE {0} = @{0} ", userId);
            if (whereCluases != null && whereCluases.Count > 0)
            {
                foreach (string whereCluase in whereCluases)
                {
                    sb.AppendFormat(" AND ({0}) ", whereCluase);
                }
            }
            if (dataTableType == DataTableType.MasterSlaveTable)
            {
                sb.AppendFormat(" AND {0} = @{0} ", curretStateName);
            }
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            foreach (DataColumn dc in dataTable.Columns)
                            {
                                db.AddInParameter(dbCommand, dc.ColumnName, DataConvertionHelper.TypeToDbType(dc.DataType), dr[dc]);
                            }
                            if (dataTableType == DataTableType.MasterSlaveTable)
                            {
                                db.AddInParameter(dbCommand, curretStateName, DbType.Byte, (byte)currentState);
                            }
                            db.ExecuteNonQuery(dbCommand, transaction);
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
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        public void Import(decimal tableId, DataTable dataTable, IDictionary<string, string> dataFieldRelation)
        {
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string key = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
            string tablePhysicalName = GetTablePhysicalName(tableId);

            try
            {                
                /* 1. 导入数据 */
                string destConnectionString = DataAccessHelper.GetConnectionString(key);
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(destConnectionString, SqlBulkCopyOptions.UseInternalTransaction);
                sqlBulkCopy.DestinationTableName = tablePhysicalName;
                sqlBulkCopy.BulkCopyTimeout = 500000000;
                foreach (KeyValuePair<string, string> keyValue in dataFieldRelation)
                {
                    sqlBulkCopy.ColumnMappings.Add(keyValue.Key, keyValue.Value);
                }
                sqlBulkCopy.WriteToServer(dataTable);

                /* 2. 修改排序 */
                SqlDatabase db = DataAccessHelper.GetDatabase(key);
                UpdateSorting(db, tablePhysicalName);

                /* 3. 修改时间 */
                string creationTimeName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime);
                string modificationTimeName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime);
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("UPDATE {0} SET ", tablePhysicalName);
                sb.AppendFormat("{0} = @CurrentDateTime, {1} = @CurrentDateTime ", creationTimeName, modificationTimeName);
                sb.AppendFormat("WHERE {0} = @ConditioalDateTime", creationTimeName);
                UpdateSorting(db, tablePhysicalName);
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值                 
                    db.AddInParameter(dbCommand, "CurrentDateTime", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "ConditioalDateTime", DbType.DateTime, DateTime.Parse(AppSettingHelper.YearMonthDay));
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
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        public void Import(decimal tableId, DataTable dataTable, IDictionary<string, string> dataFieldRelation, bool clear)
        {
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string key = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
            string tablePhysicalName = GetTablePhysicalName(tableId);

            try
            {
                if (clear)
                {
                    SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                    string sqlClear = string.Format("truncate table {0} ", tablePhysicalName);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlClear))
                    {
                        db.ExecuteNonQuery(dbCommand);
                    }
                }
                string destConnectionString = DataAccessHelper.GetConnectionString(key);
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(destConnectionString, SqlBulkCopyOptions.UseInternalTransaction);
                sqlBulkCopy.DestinationTableName = tablePhysicalName;
                sqlBulkCopy.BulkCopyTimeout = 500000000;
                foreach (KeyValuePair<string, string> keyValue in dataFieldRelation)
                {
                    sqlBulkCopy.ColumnMappings.Add(keyValue.Key, keyValue.Value);
                }
                sqlBulkCopy.WriteToServer(dataTable);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 表中是否有满足条件的记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool IsExistedRecord(decimal userId, decimal tableId)
        {
            bool exist = false;

            try
            {
                string tablePhysicalName = GetTablePhysicalName(tableId);
                byte dataWarehouseId = GetDataWarehouseId(tableId);
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                string sqlSelect = string.Format("SELECT count(1) FROM {0} WHERE UserId = @UserId", tablePhysicalName);
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    dbBusiness.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
                    exist = (int)dbBusiness.ExecuteScalar(dbCommand) > 0;
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 表中是否有满足条件的记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool IsExistedRecord(decimal userId, decimal tableId, IList<WhereConditon> whereConditons)
        {
            bool exist = false;

            try
            {
                string tablePhysicalName = GetTablePhysicalName(tableId);
                byte dataWarehouseId = GetDataWarehouseId(tableId);
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT count(1) FROM {0} WHERE UserId = @UserId", tablePhysicalName);

                string where = DataAccessHandler.GetWhereSentence(whereConditons);
                if (!string.IsNullOrWhiteSpace(where))
                {
                    sb.AppendFormat(" AND {0}", where);
                }
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    dbBusiness.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
                    //给参数赋值
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(dbBusiness, dbCommand, whereConditons);
                    }
                    exist = (int)dbBusiness.ExecuteScalar(dbCommand) > 0;
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 审核记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="auditedState">审核状态</param>
        public void Audit(decimal tableId, decimal recordId, AuditedStatus auditedStatus)
        {
            string physicalName = GetTablePhysicalName(tableId);
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string auditedStatusName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
            string sqlUpdate = string.Format("UPDATE {0} SET {1} = @{1} WHERE {2} = @{2} ", physicalName, auditedStatusName, recordIdName);

            try
            {
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    dbBusiness.AddInParameter(dbCommand, recordIdName, DbType.Decimal, recordId);
                    dbBusiness.AddInParameter(dbCommand, auditedStatusName, DbType.Byte, (byte)auditedStatus);
                    dbBusiness.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 批量审核记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordIds"></param>
        /// <param name="auditedState">审核状态</param>
        public void Audit(decimal tableId, IList<decimal> recordIds, AuditedStatus auditedStatus)
        {
            string physicalName = GetTablePhysicalName(tableId);
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string auditedStatusName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
            string sqlUpdate = string.Format("UPDATE {0} SET {1} = @{1} WHERE {2} = @{2} ", physicalName, auditedStatusName, recordIdName);

            using (DbConnection connection = dbBusiness.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (var recordId in recordIds)
                    {
                        using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlUpdate))
                        {
                            //给参数赋值
                            dbBusiness.AddInParameter(dbCommand, recordIdName, DbType.Decimal, recordId);
                            dbBusiness.AddInParameter(dbCommand, auditedStatusName, DbType.Byte, (byte)auditedStatus);
                            dbBusiness.ExecuteNonQuery(dbCommand, transaction);
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
        /// 根据审核状态条件删除表中的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="auditedStatus"></param>
        public void DeleteRecords(decimal tableId, AuditedStatus auditedStatus)
        {
        }

        /// <summary>
        /// 根据查询条件和审核状态删除表中的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="auditedStatus"></param>
        public void DeleteRecords(decimal tableId, IList<WhereConditon> whereConditons, AuditedStatus auditedStatus)
        {
            string physicalName = GetTablePhysicalName(tableId);
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string auditedStatusName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DELETE {0} FROM {0} INNER JOIN {1} ON {0}.UserId WHERE {1} = @{1} ", physicalName, DataFieldHelper.GetSystemTablePhysicalName(SystemDataField.UserActualName));
            if(whereConditons != null && whereConditons.Count > 0)
            {
                sb.AppendFormat("AND {0}", DataAccessHandler.GetConditionSentence(whereConditons));
            }            
            try
            {
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    dbBusiness.AddInParameter(dbCommand, auditedStatusName, DbType.Byte, (byte)auditedStatus);
                    DataAccessHandler.AddInParameter(dbBusiness, dbCommand, whereConditons);
                    dbBusiness.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除表中该用户的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="userId"></param>
        /// <param name="auditedStatus"></param>
        public void DeleteRecordsByUserId(decimal tableId, decimal userId, AuditedStatus auditedStatus)
        {
            string physicalName = GetTablePhysicalName(tableId);
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string auditedStatusName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
            string sqlUpdate = string.Format("DELETE FROM {0} WHERE UserId = @UserId AND {1} = @{1} ", physicalName, auditedStatusName);

            try
            {
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    dbBusiness.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    dbBusiness.AddInParameter(dbCommand, auditedStatusName, DbType.Byte, (byte)auditedStatus);
                    dbBusiness.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 重置表
        /// </summary>
        /// <param name="tableId"></param>
        public void ResetTable(decimal tableId)
        {
            try
            {
                string physicalName = GetTablePhysicalName(tableId);
                byte dataWarehouseId = GetDataWarehouseId(tableId);
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                CustomDataField customDataField = new CustomDataField();
                IList<string> names = customDataField.GetPhysicalNames(tableId);
                if (names.Count > 0)
                {
                    /* 1. 删除表中业务对应的物理字段 */
                    foreach (var name in names)
                    {
                        DataAccessHandler.DeleteDataField(dbBusiness, physicalName, name);
                    }

                    if (DataAccessHandler.IsExistPhyscialDataTable(dbBusiness, physicalName))
                    {
                        /* 2. 清空业务记录 */
                        string sql = string.Format("TRUNCATE TABLE {0}", physicalName);
                        using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sql))
                        {
                            dbBusiness.ExecuteNonQuery(dbCommand);
                        }

                        /* 3. 删除记录 */
                        SqlDatabase db = DataAccessHelper.GetDatabase();
                        customDataField.DeleteRecords(tableId, db, null);
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
        /// 复制表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public decimal CopyTable(decimal categoryId, decimal tableId)
        {
            decimal customTableId = decimal.MinValue;

            CustomCategory customCategory = new CustomCategory();
            byte dataWarehouseId = customCategory.GetDataWarehouseId(categoryId);
            CustomTableInfo customTableInfo = GetModelInfo(tableId);

            string categoryCode = customCategory.GetNodeCodeByNodeId(categoryId);
            customTableInfo.TableCode = GetNewTableCode(categoryId, categoryCode);
            customTableInfo.Sorting = 0;
            customTableInfo.CategoryId = categoryId;

            CustomDataField customDataField = new CustomDataField();
            IList<CustomDataFieldInfo> customDataFieldInfos = customDataField.GetModelInfos(tableId);
            int index = 1;
            foreach (var customDataFieldInfo in customDataFieldInfos)
            {
                customDataFieldInfo.DataFieldCode = DataFieldHelper.GetNewCode(customTableInfo.TableCode, index++);
            }

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    customTableId = Insert(customTableInfo, db, transaction);
                    customDataField.CopyCustomDataFieldInfos(customTableId, customDataFieldInfos, db, transaction, dbBusiness, customTableInfo.PhysicalName);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customTableId;
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataSet GetPageRecordByTableId(decimal tableId)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string sqlSelect = "SELECT TableId, LogicalName, TableCode, TableProperty, TableType, SystemTable, TableSetting FROM CustomTable WHERE TableId = @TableId";
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
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
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(IList<decimal> categoryIds)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT TableId, LogicalName, TableCode, TableProperty, TableType, SystemTable, TableSetting FROM CustomTable ");
                if (categoryIds.Count > 0)
                {
                    sb.Append("WHERE ");
                    for (int index = 0; index < categoryIds.Count; index++)
                    {
                        sb.AppendFormat("CategoryId = @CategoryId_{0} OR ", index);
                    }
                    sb.Remove(sb.Length - 4, 4);
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    int index = 0;
                    foreach (decimal categoryId in categoryIds)
                    {
                        db.AddInParameter(dbCommand, string.Format("CategoryId_{0}", index), DbType.Decimal, categoryId);
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
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal categoryId)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string sqlSelect = "SELECT TableId, LogicalName, TableCode, TableProperty, TableType, SystemTable, TableSetting FROM CustomTable WHERE CategoryId = @CategoryId";
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CategoryId", DbType.Decimal, categoryId);
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
        /// 重置表结构并更新表信息
        /// </summary>
        /// <param name="customTableInfo"></param>
        public void ResetAndUpdateTable(CustomTableInfo customTableInfo)
        {
            try
            {
                CustomCategory customCategory = new CustomCategory();
                byte dataWarehouseId = customCategory.GetDataWarehouseId(customTableInfo.CategoryId);
                string physicalName = GetTablePhysicalName(customTableInfo.TableId);
                DeleteLogTable(physicalName);
                DeletePhysicalTable(dataWarehouseId, physicalName);
                CreatePhysicalTable(dataWarehouseId, physicalName, (DataTableType)customTableInfo.TableType);
                Update(customTableInfo);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
        /// 获得表编号
        /// </summary>
        ///<param name="tableName">物理表名</param>
        /// <returns> 物理表名</returns>
        public Int64 GetTableSetting(decimal tableId)
        {
            Int64 tableSetting = 0;

            try
            {
                string sqlSelect = "SELECT TableSetting FROM CustomTable WHERE TableId = @TableId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    tableSetting = DataConvertionHelper.GetLong(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableSetting;
        }

        /// <summary>
        /// 根据用户编号更新排序
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        public void UpdateRecordSortingByUserId(decimal userId, decimal tableId)
        {
            UpdateRecordSorting(false, userId, tableId);
        }

        /// <summary>
        /// 根据业务编号更新排序
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="tableId"></param>
        public void UpdateRecordSortingByBusinessId(decimal businessId, decimal tableId)
        {
            UpdateRecordSorting(false, businessId, tableId);
        }

        /// <summary>
        /// 移动记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="movedDriection"></param>
        public void MoveRecord(decimal userId, decimal tableId, decimal recordId, MovedDriection movedDriection)
        {
            int recordSorting = 0;
            int dataWarehouseId = GetDataWarehouseId(tableId);
            string tablePhysicalName = GetTablePhysicalName(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));

            //查询语句
            StringBuilder sqlSelect = new StringBuilder();
            sqlSelect.Append("SELECT RecordSorting FROM ");
            sqlSelect.Append(tablePhysicalName);
            sqlSelect.Append(" WHERE RecordId = @RecordId");
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect.ToString()))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, DataConvertionHelper.SetDecimal(recordId));
                recordSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 0);
            }
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    switch (movedDriection)
                    {
                        case MovedDriection.Top:
                            StringBuilder sbTop = new StringBuilder();
                            sbTop.Append("UPDATE ");
                            sbTop.Append(tablePhysicalName);
                            sbTop.Append(" SET RecordSorting = RecordSorting + 1 WHERE UserId = @UserId AND RecordSorting < @RecordSorting");
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbTop.ToString()))
                            {
                                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                                db.AddInParameter(dbCommand, "RecordSorting", DbType.Int32, recordSorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }
                            sbTop.Clear();
                            sbTop.Append("UPDATE ");
                            sbTop.Append(tablePhysicalName);
                            sbTop.Append(" SET RecordSorting = 1 WHERE RecordId = @RecordId");
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbTop.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, DataConvertionHelper.SetDecimal(recordId));
                                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                                {
                                    throw new Exception("更新失败！");
                                }
                            }
                            break;

                        case MovedDriection.Previous:
                            StringBuilder sbPrevious = new StringBuilder();
                            sbPrevious.Append("UPDATE ");
                            sbPrevious.Append(tablePhysicalName);
                            sbPrevious.Append(" SET RecordSorting = @RecordSorting WHERE UserId = @UserId AND RecordSorting = ");
                            sbPrevious.Append("(SELECT MAX(RecordSorting) FROM ");
                            sbPrevious.Append(tablePhysicalName);
                            sbPrevious.Append(" WHERE UserId = @UserId AND RecordSorting < @RecordSorting)");
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbPrevious.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                                db.AddInParameter(dbCommand, "RecordSorting", DbType.Int32, recordSorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }
                            sbPrevious.Clear();
                            sbPrevious.Append("UPDATE ");
                            sbPrevious.Append(tablePhysicalName);
                            sbPrevious.Append(" SET RecordSorting = RecordSorting - 1 WHERE RecordId = @RecordId");
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbPrevious.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }
                            break;

                        case MovedDriection.Next:
                            StringBuilder sbNext = new StringBuilder();
                            sbNext.Append("UPDATE ");
                            sbNext.Append(tablePhysicalName);
                            sbNext.Append(" SET RecordSorting = @RecordSorting WHERE UserId = @UserId AND RecordSorting = ");
                            sbNext.Append("(SELECT MIN(RecordSorting) FROM ");
                            sbNext.Append(tablePhysicalName);
                            sbNext.Append(" WHERE UserId = @UserId AND RecordSorting > @RecordSorting)");
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbNext.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                                db.AddInParameter(dbCommand, "RecordSorting", DbType.Int32, recordSorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }
                            sbNext.Clear();
                            sbNext.Append("UPDATE ");
                            sbNext.Append(tablePhysicalName);
                            sbNext.Append(" SET RecordSorting = RecordSorting + 1 WHERE RecordId = @RecordId");
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbNext.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }
                            break;

                        case MovedDriection.Bottom:
                            int otherRecordSorting = 0;
                            StringBuilder sbBottom = new StringBuilder();
                            /* 3.1  */
                            sbBottom.Append("SELECT MAX(RecordSorting) FROM ");
                            sbBottom.Append(tablePhysicalName);
                            sbBottom.Append(" WHERE UserId = @UserId");
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
                                otherRecordSorting = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 0);
                            }

                            /* 3.2 */
                            sbBottom.Clear();
                            sbBottom.Append("UPDATE ");
                            sbBottom.Append(tablePhysicalName);
                            sbBottom.Append(" SET RecordSorting = RecordSorting - 1 WHERE UserId = @UserId AND RecordSorting > @RecordSorting");
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                                db.AddInParameter(dbCommand, "RecordSorting", DbType.Int32, recordSorting);
                                db.ExecuteNonQuery(dbCommand, transaction);
                            }

                            /* 3.3 */
                            sbBottom.Clear();
                            sbBottom.Append("UPDATE ");
                            sbBottom.Append(tablePhysicalName);
                            sbBottom.Append(" SET RecordSorting = @RecordSorting WHERE RecordId = @RecordId");
                            using (DbCommand dbCommand = db.GetSqlStringCommand(sbBottom.ToString()))
                            {
                                //给参数赋值
                                db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                                db.AddInParameter(dbCommand, "RecordSorting", DbType.Int32, otherRecordSorting);
                                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
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
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        public void DeleteRecord(decimal tableId, decimal recordId)
        {
            CustomTable customTable = new CustomTable();
            int dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);

            string sqlDelete = string.Format("DELETE FROM {0} WHERE RecordId = @RecordId", tablePhysicalName);        
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
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
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordIds"></param>
        public void DeleteRecords(decimal tableId, IList<decimal> recordIds)
        {
            CustomTable customTable = new CustomTable();
            int dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);

            StringBuilder sqlDelete = new StringBuilder();
            sqlDelete.Append("DELETE FROM ");
            sqlDelete.Append(tablePhysicalName);
            sqlDelete.Append(" WHERE ");
            int index = 0;
            foreach (decimal recordId in recordIds)
            {
                sqlDelete.AppendFormat("RecordId = @RecordId{0} OR ", index);
                index++;
            }
            sqlDelete.Remove(sqlDelete.Length - 4, 4);

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete.ToString()))
                {
                    //给参数赋值
                    index = 0;
                    foreach (decimal recordId in recordIds)
                    {
                        db.AddInParameter(dbCommand, string.Format("RecordId{0}", index), DbType.Decimal, recordId);
                        index++;
                    }
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
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal tableId, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                byte dataWarehouseId = GetDataWarehouseId(tableId);
                string tablePhysicalName = GetTablePhysicalName(tableId);
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);                
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                count = DataAccessHandler.GetRecordCount(db, tablePhysicalName, recordIdName, false, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return count;
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="where"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal tableId, string where, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                byte dataWarehouseId = GetDataWarehouseId(tableId);
                string tablePhysicalName = GetTablePhysicalName(tableId);
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                count = DataAccessHandler.GetRecordCount(db, tablePhysicalName, recordIdName, false, where, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldIds"></param>
        /// <param name="where"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public DataTable GetTableData(decimal tableId, IList<decimal> dataFieldIds, string where, IList<WhereConditon> whereConditons)
        {
            DataTable dt = null;

            try
            {
                if (DataAccessHelper.HasDangerousContents(where))
                {
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(new ArgumentException("条件包含非法字符，请检查！"));
                }
                CustomTable customTable = new CustomTable();
                int dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                string key = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
                SqlDatabase db = DataAccessHelper.GetDatabase(key);
                string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
                IList<string> dataFieldLogicalNames = new List<string>();
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT {0}", DataFieldHelper.GetOnlySystemLogicalDataFieldName(SystemDataField.UserName));
                dataFieldLogicalNames.Add(DataFieldHelper.GetLogicalDataFieldName(SystemDataField.UserName));
                CustomDataField customDataField = new CustomDataField();
                foreach (decimal dataFieldId in dataFieldIds)
                {
                    CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(dataFieldId);
                    if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        sb.AppendFormat(", {0}", customDataFieldInfo.PhysicalName);
                    }
                    else
                    {
                        CustomExpression customExpression = new CustomExpression();
                        IList<CommonNode> commonNodes = customExpression.GetCommonNodes(dataFieldId);
                        string expressionDataFieldName = customDataField.GetExpressionDataFieldName(string.Empty, customDataFieldInfo.ExpressionText, commonNodes);
                        sb.AppendFormat(", {0} AS {1}", expressionDataFieldName, customDataFieldInfo.PhysicalName);
                    }
                    dataFieldLogicalNames.Add(customDataFieldInfo.LogicalName);
                }
                sb.AppendFormat(" FROM  {0} ", tablePhysicalName);
                if (whereConditons != null && whereConditons.Count > 0)
                {
                    string condition = DataAccessHandler.GetConditionSentence(whereConditons);
                    sb.AppendFormat("WHERE {0} ", condition);
                    if (!string.IsNullOrWhiteSpace(where))
                    {
                        sb.AppendFormat("AND {0}", where);
                    }
                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(where))
                    {
                        sb.AppendFormat("WHERE {0} ", where);
                    }
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if (whereConditons != null && whereConditons.Count > 0)
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    dt = db.ExecuteDataSet(dbCommand).Tables[0];
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        dt.Columns[i].Caption = dataFieldLogicalNames[i];
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dt;
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public DataSet GetTableData(decimal tableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;

            try
            {
                if (sortingCondtions == null && sortingCondtions.Count == 0)
                {
                    throw new ArgumentException("排序条件不能为空。");
                }
                byte dataWarehouseId = GetDataWarehouseId(tableId);
                string tablePhysicalName = GetTablePhysicalName(tableId);                               
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
                string createdTime = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime);
                string updatedTime = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime);
                string recordSorting = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordSorting);

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}, {1}, {2} AS CreatedTime, {3} AS UpdatedTime, {4}", recordIdName, userIdName, createdTime, updatedTime, recordSorting);
                if (dataFieldNameRelations != null)
                {
                    foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in dataFieldNameRelations)
                    {
                        DataFieldProperty dataFieldProperty = (DataFieldProperty)keyValue.Value.DataFieldProperty;
                        switch (dataFieldProperty)
                        {
                            case DataFieldProperty.SystemPhysicalDataField:
                                SystemDataField systemDataField = (SystemDataField)Convert.ToByte(keyValue.Value.DataFieldId);
                                if (systemDataField == SystemDataField.UserName && !keyValue.Value.PhysicalName.StartsWith(tablePhysicalName))
                                {
                                    sb.AppendFormat(", {0}.{1}", tablePhysicalName, keyValue.Value.PhysicalName);
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
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                ds = DataAccessHandler.GetPageRecord(db, tablePhysicalName, sb.ToString(), false, null, startPosition, count, whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="departmentProperty"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetTableData(decimal tableId, Int64 systemLogicalDataFields, bool hasUserAccount, bool hasDepartmentProperty, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations,
            int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            if (dataFieldNameRelations.Count == 0) return null;
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string tablePhysicalName = GetTablePhysicalName(tableId);
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            string bussinessIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}, {1}.{2}, {1}.{3}", recordIdName, tablePhysicalName, userIdName, bussinessIdName);            
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in dataFieldNameRelations)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)keyValue.Value.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemDataField systemDataField = (SystemDataField)Convert.ToByte(keyValue.Value.DataFieldId);
                        if (systemDataField == SystemDataField.UserName && !keyValue.Value.PhysicalName.StartsWith(tablePhysicalName))
                        {
                            sb.AppendFormat(", {0}.{1}", tablePhysicalName, keyValue.Value.PhysicalName);
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
            if (hasUserAccount)
            {
                systemLogicalDataFields = systemLogicalDataFields | (1L << (byte)SystemDataField.UserActualName);
            }
            if (hasDepartmentProperty)
            {
                systemLogicalDataFields = systemLogicalDataFields | (1L << (byte)SystemDataField.DepProperty);
            }
            IList<TableLink> tableLinks = DataFieldHelper.GetSystemTableLinks(tablePhysicalName, systemLogicalDataFields);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            DataSet ds = null;
            if (sortingCondtions == null || sortingCondtions.Count == 0)
            {
                ds = DataAccessHandler.GetPageRecord(db, tablePhysicalName, recordIdName, sb.ToString(), false, false, tableLinks, startPosition,
               count, whereConditons, ref totalCount);
            }
            else
            {
                ds = DataAccessHandler.GetPageRecord(db, tablePhysicalName, recordIdName, sb.ToString(), false, false, tableLinks, startPosition,
              count, whereConditons, sortingCondtions, ref totalCount);
            }
            foreach (DataColumn dataColumn in ds.Tables[0].Columns)
            {
                if (dataFieldNameRelations.ContainsKey(dataColumn.ColumnName))
                {
                    dataColumn.ExtendedProperties.Add(dataColumn.ColumnName, dataFieldNameRelations[dataColumn.ColumnName]);
                    dataColumn.Caption = dataFieldNameRelations[dataColumn.ColumnName].LogicalName;
                }
            }

            return ds.Tables[0];
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="businessId"></param>
        /// <param name="onlyTarget"></param>
        /// <returns></returns>
        public DataTable GetMirrorRowData(decimal tableId, Int64 systemLogicalDataFields, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal businessId, bool onlyTarget)
        {
            DataTable dataTable = null;

            if (dataFieldNameRelations.Count == 0) return null;
            //byte dataWarehouseId = GetDataWarehouseId(tableId);
            Dictionary<string, TableLink> systemTableLinks = new Dictionary<string, TableLink>();
            string tablePhysicalName = GetTablePhysicalName(tableId);
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            string businessAlternativeIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessAlternativeId);
            string bussinessIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0}, {1}.{2}, {1}.{3}, {4}", recordIdName, tablePhysicalName, userIdName, bussinessIdName, businessAlternativeIdName);
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in dataFieldNameRelations)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)keyValue.Value.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemDataField systemDataField = (SystemDataField)Convert.ToByte(keyValue.Value.DataFieldId);
                        if (systemDataField == SystemDataField.UserName && !keyValue.Value.PhysicalName.StartsWith(tablePhysicalName))
                        {
                            sb.AppendFormat(", {0}.{1}", tablePhysicalName, keyValue.Value.PhysicalName);
                        }
                        else
                        {
                            sb.AppendFormat(", {0}", keyValue.Value.PhysicalName);
                        }
                        //string name = DataFieldHelper.GetTableLink(tablePhysicalName, (SystemDataField)Convert.ToByte(keyValue.Value.DataFieldId);
                        //sb.AppendFormat(", {0}.{1}", name, keyValue.Value.PhysicalName);
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        sb.AppendFormat(", {0}", keyValue.Value.PhysicalName);
                        break;

                    case DataFieldProperty.LogicalDataField:
                        sb.AppendFormat(", {0} AS {1}", keyValue.Value.ExpressionText, keyValue.Value.PhysicalName);
                        break;
                }
            }
            IList<TableLink> tableLinks = DataFieldHelper.GetSystemTableLinks(tablePhysicalName, systemLogicalDataFields);
            if (tableLinks != null && tableLinks.Count > 0)
            {
                string name = DataAccessHandler.GetTableNames(tablePhysicalName, tableLinks);
                sb.AppendFormat(" FROM {0} ", name);
            }
            else
            {
                sb.AppendFormat(" FROM {0} ", tablePhysicalName);
            }
            StringBuilder sbSource = new StringBuilder();
            if (!onlyTarget)
            {
                sbSource.Append(sb);
                sbSource.AppendFormat(" WHERE {0} =  @{0}", recordIdName);
            }
            sb.Append(" WHERE BusinessId =  @BusinessId");

            decimal recordId = decimal.MinValue;
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, businessId);
                dataTable = db.ExecuteDataSet(dbCommand).Tables[0];
            }
            if (!onlyTarget && dataTable.Rows.Count > 0)
            {
                recordId = DataConvertionHelper.GetDecimal(dataTable.Rows[0][businessAlternativeIdName]);
                SqlDatabase dbBlue = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)GetDataWarehouseId(tableId)));
                using (DbCommand dbCommand = dbBlue.GetSqlStringCommand(sbSource.ToString()))
                {
                    dbBlue.AddInParameter(dbCommand, recordIdName, DbType.Decimal, recordId);
                    DataTable sourceDataTable = dbBlue.ExecuteDataSet(dbCommand).Tables[0];
                    DataRow[] rows = dataTable.Select();
                    for (int i = 0; i < rows.Length; i++)
                    {
                        sourceDataTable.ImportRow(rows[i]);
                    }
                    dataTable = sourceDataTable;
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="commonDataFieldInfos"></param>
        /// <returns></returns>
        public DataTable GetTableData(decimal tableId, decimal recordId, List<CommonDataFieldInfo> commonDataFieldInfos)
        {
            DataTable dataTable = null;

            if (commonDataFieldInfos.Count == 0) return null;
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string tablePhysicalName = GetTablePhysicalName(tableId);
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            string bussinessIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0}, {1}.{2}, {1}.{3}", recordIdName, tablePhysicalName, userIdName, bussinessIdName);
            foreach (CommonDataFieldInfo commonDataFieldInfo in commonDataFieldInfos)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)commonDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemDataField systemDataField = (SystemDataField)Convert.ToByte(commonDataFieldInfo.DataFieldId);
                        if (systemDataField == SystemDataField.UserName && !commonDataFieldInfo.PhysicalName.StartsWith(tablePhysicalName))
                        {
                            sb.AppendFormat(", {0}.{1}", tablePhysicalName, commonDataFieldInfo.PhysicalName);
                        }
                        else
                        {
                            sb.AppendFormat(", {0}", commonDataFieldInfo.PhysicalName);
                        }
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        sb.AppendFormat(", {0}", commonDataFieldInfo.PhysicalName);
                        break;

                    case DataFieldProperty.LogicalDataField:
                        sb.AppendFormat(", {0} AS {1}", commonDataFieldInfo.ExpressionText, commonDataFieldInfo.PhysicalName);
                        break;
                }
            }
            sb.AppendFormat(" FROM {0} WHERE {1} = @{1}", tablePhysicalName, recordIdName);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                db.AddInParameter(dbCommand, recordIdName, DbType.Decimal, recordId);
                dataTable = db.ExecuteDataSet(dbCommand).Tables[0];
            }
            foreach (DataColumn dataColumn in dataTable.Columns)
            {
                int index = commonDataFieldInfos.FindIndex(commonDataFieldInfo => commonDataFieldInfo.PhysicalName == dataColumn.ColumnName);
                if (index > 0)
                {
                    dataColumn.ExtendedProperties.Add(dataColumn.ColumnName, commonDataFieldInfos[index]);
                    dataColumn.Caption = commonDataFieldInfos[index].LogicalName;
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 获得表的分页数据集
        /// </summary>
        /// <param name="tableId">表的编号</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>数据集</returns>
        public DataSet GetTableData(decimal tableId, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                CustomTable customTable = new CustomTable();
                string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
                CustomDataField customDataField = new CustomDataField();
                IList<CustomDataFieldInfo> customDataFieldInfos = customDataField.GetModelInfos(tableId);
                IDictionary<string, string> captions = new Dictionary<string, string>(customDataFieldInfos.Count + 1);
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
                sb.Append(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus));
                captions.Add(DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus), DataFieldHelper.GetLogicalName(SystemPhysicalDataField.AuditedStatus));

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
                if (sortingCondtions == null || sortingCondtions.Count == 0)
                {
                    ds = DataAccessHandler.GetPageRecord(db, tablePhysicalName, "RecordId", sb.ToString(), false, false, tableLinks, startPosition,
                    count, whereConditons, ref totalCount);
                }
                else
                {
                    ds = DataAccessHandler.GetPageRecord(db, tablePhysicalName, "RecordId", sb.ToString(), false, false, tableLinks, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
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
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public DataTable GetTableData(decimal tableId, Int64 systemLogicalDataFields, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, IList<WhereConditon> whereConditons)
        {
            DataTable dataTable = null;

            if (dataFieldNameRelations.Count == 0) return null;
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            Dictionary<string, TableLink> systemTableLinks = new Dictionary<string, TableLink>();
            string tablePhysicalName = GetTablePhysicalName(tableId);
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            string userIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
            string bussinessIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId);
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT {0}, {1}.{2}, {1}.{3}", recordIdName, tablePhysicalName, userIdName, bussinessIdName);
            foreach (KeyValuePair<string, CommonDataFieldInfo> keyValue in dataFieldNameRelations)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)keyValue.Value.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemDataField systemDataField = (SystemDataField)Convert.ToByte(keyValue.Value.DataFieldId);
                        if (systemDataField == SystemDataField.UserName && !keyValue.Value.PhysicalName.StartsWith(tablePhysicalName))
                        {
                            sb.AppendFormat(", {0}.{1}", tablePhysicalName, keyValue.Value.PhysicalName);
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
            IList<TableLink> tableLinks = DataFieldHelper.GetSystemTableLinks(tablePhysicalName, systemLogicalDataFields);
            if (tableLinks != null && tableLinks.Count > 0)
            {
                string name = DataAccessHandler.GetTableNames(tablePhysicalName, tableLinks);
                sb.AppendFormat(" FROM {0} ", name);
            }
            else
            {
                sb.AppendFormat(" FROM {0} ", tablePhysicalName);
            }
            if ((whereConditons != null) && (whereConditons.Count > 0))
            {
                sb.Append(" WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons));
            }
            sb.Append(" ORDER BY RecordSorting");

            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
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
        /// 获得记录的审核状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public AuditedStatus GetAuditedStatus(decimal tableId, decimal recordId)
        {
            AuditedStatus auditedStatus = AuditedStatus.None;
            
            string tablePhysicalName = GetTablePhysicalName(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)GetDataWarehouseId(tableId)));

            //查询语句
            StringBuilder sqlSelect = new StringBuilder();
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            sqlSelect.AppendFormat("SELECT {0} FROM {1} ", DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus), tablePhysicalName);
            sqlSelect.AppendFormat(" WHERE {0} = @{0}", recordIdName);
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect.ToString()))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, recordIdName, DbType.Decimal, recordId);
                auditedStatus = (AuditedStatus)DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand), 0);
            }

            return auditedStatus;
        }

        /// <summary>
        /// 获得用户在表中的记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal userId, decimal tableId, bool businessEnabled, decimal instanceId)
        {
            int count = 0;

            try
            {
                string tablePhysicalName = GetTablePhysicalName(tableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)GetDataWarehouseId(tableId)));
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT COUNT(1) FROM {0} ", tablePhysicalName);
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                whereConditons.Add(new WhereConditon(tablePhysicalName, "UserId", "UserId", DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                if (businessEnabled && instanceId > 0)
                {
                    whereConditons.Add(new WhereConditon(tablePhysicalName, "BusinessId", "BusinessId", DbType.Decimal, instanceId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                }
                sb.Append("WHERE ");
                sb.Append(DataAccessHandler.GetConditionSentence(whereConditons)); using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
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
        /// 获得单元格的值
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public object GetDataFiledValue(decimal userId, decimal dataFieldId)
        {
            object dataFiledValue = null;

            try
            {
                CustomDataField customDataField = new CustomDataField();
                CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(dataFieldId);
                string tablePhysicalName = GetTablePhysicalName(customDataFieldInfo.TableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)GetDataWarehouseId(customDataFieldInfo.TableId)));
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                string physicalDataFiledName = string.Empty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.PhysicalDataField:
                        physicalDataFiledName = customDataFieldInfo.PhysicalName;
                        break;

                    case DataFieldProperty.LogicalDataField:
                        physicalDataFiledName = string.Format("{0} AS {1}", customDataField.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId),
                                    customDataFieldInfo.PhysicalName);
                        break;

                    default:
                        throw new Exception("不支持该字段属性。");
                }
                StringBuilder sb = new StringBuilder();
                DataTableType tableType = (DataTableType)GetDataTableType(customDataFieldInfo.TableId);
                sb.AppendFormat("SELECT {0} FROM {1} WHERE UserId = @UserId ", physicalDataFiledName, tablePhysicalName);
                if (tableType == DataTableType.MasterSlaveTable)
                {
                    sb.Append("AND CurrentState = @CurrentState");                    
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    if (tableType == DataTableType.MasterSlaveTable)
                    {
                        db.AddInParameter(dbCommand, "CurrentState", DbType.Byte, (byte)CurrentState.Current);
                    }
                    dataFiledValue = db.ExecuteScalar(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFiledValue;
        }

        /// <summary>
        /// 获得单元格的值
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public object GetDataFiledValueByRecordId(decimal recordId, decimal dataFieldId)
        {
            object dataFiledValue = null;

            try
            {
                CustomDataField customDataField = new CustomDataField();
                CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(dataFieldId);
                string tablePhysicalName = GetTablePhysicalName(customDataFieldInfo.TableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)GetDataWarehouseId(customDataFieldInfo.TableId)));
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                string physicalDataFiledName = string.Empty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.PhysicalDataField:
                        physicalDataFiledName = customDataFieldInfo.PhysicalName;
                        break;

                    case DataFieldProperty.LogicalDataField:
                        physicalDataFiledName = string.Format("{0} AS {1}", customDataField.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId),
                                    customDataFieldInfo.PhysicalName);
                        break;

                    default:
                        throw new Exception("不支持该字段属性。");
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT {0} FROM {1} WHERE RecordId = @RecordId ", physicalDataFiledName, tablePhysicalName);
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);                    
                    dataFiledValue = db.ExecuteScalar(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFiledValue;
        }


        /// <summary>
        /// 获得表的类型
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataTableType GetDataTableType(decimal tableId)
        {
            DataTableType dataTableType = DataTableType.PrimaryTable;

            try
            {
                string sqlSelect = "SELECT TableType FROM CustomTable WHERE TableId = @TableId";
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(tableId));
                    dataTableType = (DataTableType)DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTableType;
        }

        /// <summary>
        /// 获得数据库下所有的表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public List<CommonNode> GetCommonNodesByDatabaseId(decimal databaseId)
        {
            List<CommonNode> commonNodes = new List<CommonNode>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TableId, CustomTable.CategoryId, LogicalName, PhysicalName, TableType FROM CustomTable ");
            sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("WHERE CustomCategory.DatabaseId = @DatabaseId ORDER BY CustomCategory.Sorting, CustomTable.Sorting ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "DatabaseId", DbType.Decimal, databaseId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal categoryId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[2]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[3]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[4]);
                            commonNodes.Add(new CommonNode(tableId, categoryId, logicalName, physicalName, tableType));
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
        /// 根据表类型条件获得表节点
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tableFilter"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal categoryId, TableFilter tableFilter)
        {
            IList<CommonNode> commonNodes = null;

            IList<WhereConditon> whereConditons = DataTableHelper.GetWhereConditons(categoryId, tableFilter);

            commonNodes = GetCommonNodesByWhereConditon(whereConditons);

            return commonNodes;
        }

        /// <summary>
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DatabaseName, CategoryName, LogicalName FROM CustomTable ");
            sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("WHERE CustomTable.TableId = @TableId ");

            try
            {
                byte dataWarehouseId = GetDataWarehouseId(nodeId);
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, nodeId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            names.Add(UserEnumHelper.GetEnumText((DataWarehouse)dataWarehouseId));
                            names.Add(DataConvertionHelper.GetString(dataReader[0]));
                            names.Add(DataConvertionHelper.GetString(dataReader[1]));
                            names.Add(DataConvertionHelper.GetString(dataReader[2]));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return names;
        }

        /// <summary>
        /// 获得数据库编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public decimal GetDatabaseId(decimal tableId)
        {
            decimal databaseId = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DatabaseId FROM CustomCategory INNER JOIN CustomTable ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("WHERE CustomTable.TableId = @TableId ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    databaseId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return databaseId;
        }

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal tableId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("TableId", "TableId", System.Data.DbType.Decimal, tableId, DataFieldCondition.Equal));

            return GetDataWarehouseId(whereConditons);
        }

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(string tableName)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("PhysicalName", "PhysicalName", System.Data.DbType.String, tableName, DataFieldCondition.Equal));

            return GetDataWarehouseId(whereConditons);
        }

        /// <summary>
        /// 获得表编号
        /// </summary>
        ///<param name="tableName">物理表名</param>
        /// <returns> 物理表名</returns>
        public decimal GetTableId(string tableName)
        {
            decimal tableId = 0;

            try
            {
                string sqlSelect = "SELECT TableId FROM CustomTable WHERE PhysicalName = @PhysicalName";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "PhysicalName", DbType.String, tableName);
                    tableId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableId;
        }

        /// <summary>
        /// 获得表的类型
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 表的逻辑名称</returns>
        public byte GetTableType(decimal tableId)
        {
            byte tableType = 0;

            try
            {
                string sqlSelect = "SELECT TableType FROM CustomTable WHERE TableId = @TableId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(tableId));
                    tableType = DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableType;
        }

        /// <summary>
        /// 获得表的逻辑名称
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 表的逻辑名称</returns>
        public string GetTableLogicalName(decimal tableId)
        {
            string tablePhysicalName = string.Empty;

            try
            {
                string sqlSelect = "SELECT LogicalName FROM CustomTable WHERE TableId = @TableId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(tableId));
                    tablePhysicalName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tablePhysicalName;
        }

        /// <summary>
        /// 获得物理表名
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 物理表名</returns>
        public string GetTablePhysicalName(decimal tableId)
        {
            string tablePhysicalName = string.Empty;

            try
            {
                string sqlSelect = "SELECT PhysicalName FROM CustomTable WHERE TableId = @TableId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(tableId));
                    tablePhysicalName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tablePhysicalName;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得表的的信息
        /// </summary>
        /// <returns>CommonNode 对象列表</returns>
        public IList<CommonNode> GetTables()
        {
            //创建集合对象
            IList<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomTable.TableId, DataWarehouseId, CustomTable.PhysicalName FROM CustomTable ");
            sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            byte dataWarehouseId = DataConvertionHelper.GetByte(dataReader[1]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[2]);
                            commonNodes.Add(new CommonNode(tableId, dataWarehouseId, physicalName));
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

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomTableInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomTableInfo 对象列表</returns>
        private IList<CustomTableInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomTableInfo> customTableInfos = new List<CustomTableInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomTable");
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
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal categoryId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[2]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[3]);
                            string tableCode = DataConvertionHelper.GetString(dataReader[4]);
                            byte tableProperty = DataConvertionHelper.GetByte(dataReader[5]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[6]);
                            bool systemTable = DataConvertionHelper.GetBoolean(dataReader[7]);
                            long tableSetting = DataConvertionHelper.GetLong(dataReader[8]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[9]);
                            string notes = DataConvertionHelper.GetString(dataReader[10]);
                            //将创建 CustomTableInfo 对象加入集合中
                            customTableInfos.Add(new CustomTableInfo(tableId, categoryId, logicalName, physicalName, tableCode, tableProperty,
                            tableType, systemTable, tableSetting, sorting, notes));
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

            return customTableInfos;
        }

        /// <summary>
        /// 获得 CustomTableInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomTableInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomTable");
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
        /// 获得表 CustomTable 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomTable ", "TableId", "*", false, false, startPosition,
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
        /// 获得以表 CustomTable 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomTable ", "TableId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomTable 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomTable ", "TableId", "*", false, false, startPosition,
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
        /// 获得以表 CustomTable 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomTable ", "TableId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomTableInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomTable");
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
        /// 更新排序
        /// </summary>
        /// <param name="businessEnable"></param>
        /// <param name="identifier"></param>
        /// <param name="tableId"></param>
        private void UpdateRecordSorting(bool businessEnable, decimal identifier, decimal tableId)
        {
            byte dataWarehouseId = GetDataWarehouseId(tableId);
            string tablePhysicalName = GetTablePhysicalName(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));

            //查询语句
            StringBuilder sqlSelect = new StringBuilder();
            sqlSelect.AppendFormat("SELECT RecordId FROM {0} ", tablePhysicalName);
            if (businessEnable)
            {
                sqlSelect.Append("WHERE BusinessId = @BusinessId ");
            }
            else
            {
                sqlSelect.Append("WHERE UserId = @UserId ");
            }
            sqlSelect.Append("ORDER BY RecordSorting, CreationTime");
            List<decimal> recordIds = new List<decimal>();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect.ToString()))
            {
                //给参数赋值
                if (businessEnable)
                {
                    db.AddInParameter(dbCommand, "BusinessId", DbType.Decimal, identifier);
                }
                else
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, identifier);
                }
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        recordIds.Add(Convert.ToDecimal(dataReader[0]));
                    }
                        if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                }                
            }
            string update = string.Format("UPDATE {0} SET RecordSorting = @RecordSorting WHERE RecordId = @RecordId", tablePhysicalName);
            int count = 1;
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (decimal recordId in recordIds)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(update))
                        {
                            db.AddInParameter(dbCommand, "RecordId", DbType.Decimal, recordId);
                            db.AddInParameter(dbCommand, "RecordSorting", DbType.Int32, count++);
                            db.ExecuteNonQuery(dbCommand, transaction);
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
        /// 更新排序
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tablePhysicalName"></param>
        /// <param name="transaction"></param>
        public void UpdateSorting(SqlDatabase db, string tablePhysicalName)
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
        /// 获得新的编码
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="categoryCode"></param>
        /// <returns></returns>
        private string GetNewTableCode(decimal categoryId, string categoryCode)
        {
            string tableCode = string.Empty;

            IList<string> childNodeCodes = GetChildNodeCodes(categoryId);
            int index = 1;
            do
            {
                tableCode = DataFieldHelper.GetNewCode(categoryCode, index);
                index++;
            } while (childNodeCodes.Contains(tableCode));

            return tableCode;
        }

        /// <summary>
        /// 插入表记录并创建表
        /// </summary>
        /// <param name="customTableInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private decimal Insert(CustomTableInfo customTableInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal customTableId = 0;

            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomTable", "Sorting", "CategoryId", customTableInfo.CategoryId, 0) + 1;
            CustomCategory customCategory = new CustomCategory();
            byte dataWarehouseId = customCategory.GetDataWarehouseId(customTableInfo.CategoryId);
            decimal tableId = 0;
            do
            {
                customTableInfo.PhysicalName = string.Format("tb_{0}_{1}", customTableInfo.CategoryId, sorting++);
                tableId = GetTableId(customTableInfo.PhysicalName);
            } while (tableId > 0);

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomTable(CategoryId, LogicalName, PhysicalName, TableCode, TableProperty, TableType, SystemTable, TableSetting, ");
            sb.Append("Sorting, Notes)");
            sb.Append("VALUES (@CategoryId, @LogicalName, @PhysicalName, @TableCode, @TableProperty, @TableType, @SystemTable, @TableSetting, ");
            sb.Append("@Sorting, @Notes);");
            sb.Append("SET @TableId = SCOPE_IDENTITY()");

            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                //给参数赋值
                db.AddOutParameter(dbCommand, "TableId", DbType.Decimal, 8);
                db.AddInParameter(dbCommand, "CategoryId", DbType.Decimal, customTableInfo.CategoryId);
                db.AddInParameter(dbCommand, "LogicalName", DbType.String, customTableInfo.LogicalName);
                db.AddInParameter(dbCommand, "PhysicalName", DbType.String, customTableInfo.PhysicalName);
                db.AddInParameter(dbCommand, "TableCode", DbType.String, customTableInfo.TableCode);
                db.AddInParameter(dbCommand, "TableProperty", DbType.Byte, customTableInfo.TableProperty);
                db.AddInParameter(dbCommand, "TableType", DbType.Byte, customTableInfo.TableType);
                db.AddInParameter(dbCommand, "SystemTable", DbType.Boolean, customTableInfo.SystemTable);
                db.AddInParameter(dbCommand, "TableSetting", DbType.Byte, customTableInfo.TableSetting);
                db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                db.AddInParameter(dbCommand, "Notes", DbType.String, customTableInfo.Notes);
                //执行插入操作
                if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                {
                    throw new Exception("插入失败！");
                }
                customTableId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@TableId"].Value, 0);
            }
            customCategory.UpdateLeafOfParentNode(customTableInfo.CategoryId, false, db, transaction);
            CreatePhysicalTable(dataWarehouseId, customTableInfo.PhysicalName, (DataTableType)customTableInfo.TableType);

            return customTableId;
        }

        /// <summary>
        /// 生成物理表,由调用者保证事务的完整性
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="physcialDataTableName"></param>
        /// <param name="dataTableType"></param>
        private void CreatePhysicalTable(byte dataWarehouseId, string physcialDataTableName, DataTableType dataTableType)
        {
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            if (DataAccessHandler.IsExistPhyscialDataTable(db, physcialDataTableName))
            {
                DataAccessHandler.DeletePhysicalTable(db, physcialDataTableName);
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("CREATE TABLE {0} (", physcialDataTableName);
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemPhysicalDataField));
            foreach (EnumItem enumItem in enumItems)
            {
                sb.AppendFormat("{0}, ", enumItem.Text);
            }
            sb.Remove(sb.Length - 2, 2);
            sb.Append(");");
            try
            {
                /* 创建日志表 */
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                if (DataAccessHandler.IsExistPhyscialDataTable(dbBusiness, physcialDataTableName))
                {
                    DataAccessHandler.DeletePhysicalTable(dbBusiness, physcialDataTableName);
                }
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sb.ToString()))
                {
                    dbBusiness.ExecuteNonQuery(dbCommand);
                }

                /* 创建表 */
                if (dataTableType == DataTableType.PrimaryTable)
                {
                    sb.AppendFormat("CREATE UNIQUE INDEX {0}_UserId_IDX on {0} (UserId ASC, BusinessId ASC); CREATE INDEX {0}_BusinessId_IDX on {0} (BusinessId ASC)", physcialDataTableName);
                }
                else
                {
                    sb.AppendFormat("CREATE INDEX {0}_UserId_IDX on {0} (UserId ASC);CREATE INDEX {0}_BusinessId_IDX on {0} (BusinessId ASC)", physcialDataTableName);
                }

                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
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
        /// 更新索引
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="physcialDataTableName"></param>
        /// <param name="newDataTableType"></param>
        private void UpdateIndexOnTable(byte dataWarehouseId, string physcialDataTableName, DataTableType newDataTableType)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("DROP INDEX {0}_UserId_IDX on {0};", physcialDataTableName);
            if (newDataTableType == DataTableType.PrimaryTable)
            {
                sb.AppendFormat("CREATE UNIQUE INDEX {0}_UserId_IDX on {0} (UserId ASC, BusinessId ASC);", physcialDataTableName);                
            }
            else
            {
                sb.AppendFormat("CREATE INDEX {0}_UserId_IDX on {0} (UserId ASC);", physcialDataTableName);
            }
            try
            {
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
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
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(IList<WhereConditon> whereConditons)
        {
            byte dataWarehouseId = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DataWarehouseId FROM CustomDatabase INNER JOIN CustomCategory ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("INNER JOIN CustomTable ON CustomCategory.CategoryId = CustomTable.CategoryId ");
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
        /// 删除日志表
        /// </summary>
        /// <param name="tablePhysicalName"></param>
        private void DeleteLogTable(string tablePhysicalName)
        {
            //生成删除语句
            string sqlDelete = string.Format("if exists (select name from sysobjects where name = '{0}') DROP TABLE {0}", tablePhysicalName);

            try
            {
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlDelete))
                {
                    dbBusiness.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除表
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="tablePhysicalName"></param>
        private void DeletePhysicalTable(byte dataWarehouseId, string tablePhysicalName)
        {
            //生成删除语句
            string sqlDelete = string.Format("if exists (select name from sysobjects where name = '{0}') DROP TABLE {0}", tablePhysicalName);

            try
            {
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
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

        #endregion

        #region 过时的函数

        /// <summary>
        /// 根据表的编号获得数据仓库的编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public int GetDataWarehouseIdByTableId(decimal tableId)
        {
            int dataWarehouseId = 0;
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT DataWarehouseId FROM CustomDatabase ");
                sb.Append("INNER JOIN CustomTable ON CustomDatabase.DatabaseId = CustomTable.DatabaseId ");
                sb.Append("WHERE CustomTable.TableId = @TableId ");

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(tableId));
                    dataWarehouseId = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataWarehouseId;
        }

        #endregion

        #endregion
    }
}
