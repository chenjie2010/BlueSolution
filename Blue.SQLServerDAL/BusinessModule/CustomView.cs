//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomView.cs
// 描述：CustomView 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/10/13
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
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomView 表的数据层访问类
    /// </summary>
    public class CustomView : CommonNodeDataAccess, ICustomView
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomView() : base("CustomView", "ViewId", "GroupId", "ViewName", "ViewCode", false, true)
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomView 表中插入一条新记录
        /// </summary>
        /// <param name="customViewInfo">customViewInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomViewInfo customViewInfo)
        {
            return Insert(customViewInfo, null);
        }

        /// <summary>
        /// 获得 CustomViewInfo 对象
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> CustomViewInfo 对象</returns>
        public CustomViewInfo GetModelInfo(decimal viewId)
        {
            CustomViewInfo customViewInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ViewId", "ViewId", System.Data.DbType.Decimal, viewId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomViewInfo> customViewInfos = GetModelInfos(whereConditons, null, true);
            if (customViewInfos != null && customViewInfos.Count > 0)
            {
                customViewInfo = customViewInfos[0];
            }

            return customViewInfo;
        }

        /// <summary>
        /// 更新 CustomViewInfo 对象
        /// </summary>
        /// <param name="customViewInfo">CustomViewInfo 对象</param>
        public void Update(CustomViewInfo customViewInfo)
        {
            Update(customViewInfo, null);
        }

        /// <summary>
        ///  删除 CustomViewInfo 对象
        /// </summary>
        ///<param name="viewId">视图编号</param>
        public void Delete(decimal viewId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomView ");
            sb.Append("WHERE ViewId = @ViewId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            string viewName = GetViewPhysicalName(viewId);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    CustomViewAndDataField customViewAndDataField = new CustomViewAndDataField();
                    CustomViewAndTable customViewAndTable = new CustomViewAndTable();
                    /* 1. 删除与视图有关系的字段记录 */
                    customViewAndDataField.Delete(viewId, db, transaction);
                    /* 2. 删除与视图有关系的表记录*/
                    customViewAndTable.Delete(viewId, db, transaction);
                    /* 3. 删除记录 */
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, viewId);                        
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
                        }
                    }                    
                    transaction.Commit();
                    /* 4. 删除视图 */
                    DeleteView(viewName);
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
        /// 获得 CustomViewInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomViewInfo 对象列表</returns>
        public IList<CustomViewInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomView 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomViewInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomView ", "ViewId", false, whereConditons);
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
        /// 获得角色条件字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<string> GetRoleConditionDataFieldNames(decimal viewId)
        {
            IList<string> dataFieldNames = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDataField.PhysicalName FROM CustomDataField ");
            sb.Append("INNER JOIN CustomViewAndDataField ON CustomViewAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE ViewId = @ViewId AND DataFieldSetting & @DataFieldSetting > 0 ");
            long dataFieldSettingValue = 1L;
            int pos = (byte)DataFieldSetting.RoleUnderCondition;
            if (pos > 0)
            {
                dataFieldSettingValue = dataFieldSettingValue << pos;
            }

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, viewId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            string physicalName = DataConvertionHelper.GetString(dataReader[0]);
                            dataFieldNames.Add(physicalName);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldNames;
        }

        /// <summary>
        /// 获得系统字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public Int64 GetSystemDataFields(decimal viewId)
        {
            Int64 systemDataFields = 0;

            try
            {
                string sqlSelect = "SELECT SystemDataFields FROM CustomView WHERE ViewId = @ViewId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(viewId));
                    systemDataFields = DataConvertionHelper.GetLong(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return systemDataFields;
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="viewId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="userAccount"></param>
        /// <param name="departmentProperty"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetViewData(decimal viewId, Int64 systemLogicalDataFields, bool userAccount, bool departmentProperty, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations,
            int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            if (dataFieldNameRelations.Count == 0) return null;

            byte dataWarehouseId = GetDataWarehouseId(viewId);
            string viewPhysicalName = GetViewPhysicalName(viewId);
            string mainTableName = GetTablePhysicalName(viewId);
            string recordIdName = String.Format("{0}_{1}", mainTableName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId));
            StringBuilder sb = new StringBuilder();
            sb.Append(recordIdName);
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
            if (userAccount)
            {
                systemLogicalDataFields = systemLogicalDataFields | (1L << (byte)SystemDataField.UserActualName);
            }
            if (departmentProperty)
            {
                systemLogicalDataFields = systemLogicalDataFields | (1L << (byte)SystemDataField.DepProperty);
            }
            IList<TableLink> tableLinks = DataFieldHelper.GetSystemTableLinks(viewPhysicalName, systemLogicalDataFields);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            DataSet ds = null;
            if (sortingCondtions == null || sortingCondtions.Count == 0)
            {
                ds = DataAccessHandler.GetPageRecord(db, viewPhysicalName, sb.ToString(), false, tableLinks, startPosition, count, whereConditons, recordIdName, ref totalCount);
            }
            else
            {
                ds = DataAccessHandler.GetPageRecord(db, viewPhysicalName, sb.ToString(), false, tableLinks, startPosition, count, whereConditons, sortingCondtions, ref totalCount);
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
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal viewId)
        {
            byte dataWarehouseId = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DataWarehouseId FROM CustomDatabase INNER JOIN CustomCategory ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("INNER JOIN CustomTable ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("INNER JOIN CustomView ON CustomTable.TableId = CustomView.TableId ");
            sb.Append("WHERE ViewId = @ViewId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, viewId);
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
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            CustomViewInfo customViewInfo = GetModelInfo(nodeId);
            if (customViewInfo != null)
            {
                CustomGroup customGroup = new CustomGroup();
                IList<string> parentNames = customGroup.GetHierarchicalNamesOfNode(customViewInfo.GroupId);
                foreach (var parentName in parentNames)
                {
                    names.Add(parentName);
                }
                names.Add(customViewInfo.ViewName);
            }

            return names;
        }

        /// <summary>
        /// 获得视图的物理表类型
        /// 
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 主物理表类型</returns>
        public DataTableType GetMainTableType(decimal viewId)
        {
            DataTableType tableType = DataTableType.PrimaryTable;

            try
            {
                string sqlSelect = "SELECT CustomTable.TableType FROM CustomView INNER JOIN CustomTable ON CustomView.TableId = CustomTable.TableId WHERE ViewId = @ViewId";


                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(viewId));
                    tableType = (DataTableType)DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
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
        /// 向 CustomView 表中插入一条新记录
        /// </summary>
        /// <param name="customViewInfo">customViewInfo 对象</param>
        /// <param name="customViewAndTableInfos">视图与表的关系</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomViewInfo customViewInfo, IList<CustomViewAndTableInfo> customViewAndTableInfos)
        {
            //自动增加的关键字的值
            decimal customViewId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customViewInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomView", "Sorting", "GroupId", customViewInfo.GroupId, 0) + 1;
            customViewInfo.PhysicalName = string.Format("vw_{0}_{1}", customViewInfo.GroupId, customViewInfo.Sorting);

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomView(TableId, GroupId, ViewName, ViewCode, PhysicalName, ViewProperty, SystemDataFields, IsLeaf, ");
            sb.Append("ToolTip, Sorting, Notes)");
            sb.Append("VALUES (@TableId, @GroupId, @ViewName, @ViewCode, @PhysicalName, @ViewProperty, @SystemDataFields, @IsLeaf, ");
            sb.Append("@ToolTip, @Sorting, @Notes);");
            sb.Append("SET @ViewId = SCOPE_IDENTITY()");
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "ViewId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, customViewInfo.TableId);
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customViewInfo.GroupId);
                        db.AddInParameter(dbCommand, "ViewName", DbType.String, customViewInfo.ViewName);
                        db.AddInParameter(dbCommand, "ViewCode", DbType.String, customViewInfo.ViewCode);
                        db.AddInParameter(dbCommand, "PhysicalName", DbType.String, customViewInfo.PhysicalName);
                        db.AddInParameter(dbCommand, "ViewProperty", DbType.Byte, customViewInfo.ViewProperty);
                        db.AddInParameter(dbCommand, "SystemDataFields", DbType.Int64, customViewInfo.SystemDataFields);
                        db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, customViewInfo.IsLeaf);
                        db.AddInParameter(dbCommand, "ToolTip", DbType.String, customViewInfo.ToolTip);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customViewInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customViewInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customViewId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@ViewId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(customViewInfo.GroupId, false, db, transaction);
                    foreach (var obj in customViewAndTableInfos)
                    {
                        obj.ViewId = customViewId;
                    }
                    CustomViewAndTable customViewAndTable = new CustomViewAndTable();
                    customViewAndTable.Insert(customViewAndTableInfos, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customViewId;
        }

        /// <summary>
        /// 更新一条新记录
        /// </summary>
        /// <param name="customViewInfo"></param>
        /// <param name="customViewAndTableInfos"></param>
        public void Update(CustomViewInfo customViewInfo, IList<CustomViewAndTableInfo> customViewAndTableInfos)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomView SET TableId = @TableId, ViewName = @ViewName, ViewCode = @ViewCode, ");
            sb.Append("ViewProperty = @ViewProperty, SystemDataFields = @SystemDataFields, ToolTip = @ToolTip,  Notes = @Notes ");
            sb.Append("WHERE ViewId = @ViewId");

            CustomViewAndTable customViewAndTable = new CustomViewAndTable();
            IList<CustomViewAndTableInfo> oldCustomViewAndTableInfos = customViewAndTable.GetModelInfos(customViewInfo.ViewId);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            Int64 systemDataFields = GetSystemDataFields(customViewInfo.ViewId);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, customViewInfo.ViewId);
                        db.AddInParameter(dbCommand, "ViewName", DbType.String, customViewInfo.ViewName);
                        db.AddInParameter(dbCommand, "ViewCode", DbType.String, customViewInfo.ViewCode);
                        db.AddInParameter(dbCommand, "ViewProperty", DbType.Byte, customViewInfo.ViewProperty);
                        db.AddInParameter(dbCommand, "SystemDataFields", DbType.Int64, customViewInfo.SystemDataFields);
                        db.AddInParameter(dbCommand, "ToolTip", DbType.String, customViewInfo.ToolTip);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customViewInfo.Notes);
                        //执行更新操作
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                        bool created = false;
                        /* 1. 不存在则插入，存在则更新 */
                        foreach (var customViewAndTableInfo in customViewAndTableInfos)
                        {
                            bool find = false;
                            foreach (var oldCustomViewAndTableInfo in oldCustomViewAndTableInfos)
                            {
                                if (customViewAndTableInfo.ViewId == oldCustomViewAndTableInfo.ViewId && customViewAndTableInfo.TableId == oldCustomViewAndTableInfo.TableId)
                                {
                                    find = true;
                                    if (customViewAndTableInfo.Sorting != oldCustomViewAndTableInfo.Sorting)
                                    {
                                        customViewAndTable.Update(customViewAndTableInfo, db, transaction);
                                    }
                                    break;
                                }
                            }
                            if (!find)
                            {
                                customViewAndTableInfo.ViewId = customViewInfo.ViewId;
                                customViewAndTable.Insert(customViewAndTableInfo, db, transaction);
                                created = true;
                            }
                        }
                        /* 2. 存在则忽略，不存在则删除*/
                        foreach (var oldCustomViewAndTableInfo in oldCustomViewAndTableInfos)
                        {
                            bool find = false;
                            foreach (var customViewAndTableInfo in customViewAndTableInfos)
                            {
                                if (customViewAndTableInfo.ViewId == oldCustomViewAndTableInfo.ViewId && customViewAndTableInfo.TableId == oldCustomViewAndTableInfo.TableId)
                                {
                                    find = true;
                                    break;
                                }
                            }
                            if (!find)
                            {
                                customViewAndTable.Delete(oldCustomViewAndTableInfo, db, transaction);
                                created = true;
                            }
                        }                        
                        transaction.Commit();
                        if (systemDataFields != customViewInfo.SystemDataFields)
                        {
                            created = true;
                        }
                        if (created)
                        {
                            CustomViewAndDataField customViewAndDataField = new CustomViewAndDataField();
                            IList<CustomViewAndDataFieldInfo> customViewAndDataFieldInfos = customViewAndDataField.GetModelInfos(customViewInfo.ViewId);
                            DeleteView(customViewInfo.PhysicalName);
                            CreatePhysicalView(customViewInfo, customViewAndTableInfos, customViewAndDataFieldInfos);
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
        }

        #endregion

        #endregion

        #region 公有方法


        /// <summary>
        /// 删除 CustomCellInfo 对象
        /// </summary>
        /// <param name="sectionId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void DeleteByTableId(decimal tableId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE CustomViewAndTable FROM CustomViewAndTable INNER JOIN CustomView ");
            sb.Append("ON CustomViewAndTable.ViewId = CustomView.ViewId ");
            sb.Append("WHERE CustomView.TableId = @TableId; ");
            sb.Append("DELETE CustomViewAndDataField FROM CustomViewAndDataField INNER JOIN CustomView ");
            sb.Append("ON CustomViewAndDataField.ViewId = CustomView.ViewId ");
            sb.Append("WHERE CustomView.TableId = @TableId; ");
            sb.Append("DELETE FROM CustomView ");
            sb.Append("WHERE TableId = @TableId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    //执行删除操作
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="customViewInfo"></param>
        /// <param name="customViewAndTableInfos"></param>
        /// <param name="customViewAndDataFieldInfos"></param>

        public void CreatePhysicalView(CustomViewInfo customViewInfo, IList<CustomViewAndTableInfo> customViewAndTableInfos,
            IList<CustomViewAndDataFieldInfo> customViewAndDataFieldInfos)
        {
            StringBuilder sb = new StringBuilder();
            StringBuilder physicalDataFiledNames = new StringBuilder();

            CustomTable customTable = new CustomTable();
            string mainTablePhysicalName = customTable.GetTablePhysicalName(customViewInfo.TableId);
            physicalDataFiledNames.AppendFormat("{0}.{1} AS {0}_{1}, {0}.UserId, {0}.UserName, {0}.DepId, {0}.UserTypeId,{0}.RecordSorting, {0}.AuditedStatus, {0}.CurrentState ", mainTablePhysicalName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId));
            IList<TableLink> tableTableLinks = new List<TableLink>();
            if (customViewInfo.SystemDataFields > 0)
            {
                List<string> systemTablePhysicalNames = new List<string>();
                List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemDataField));
                foreach (EnumItem enumItem in enumItems)
                {
                    bool result = AuthorityHelper.CheckAuthority(customViewInfo.SystemDataFields, enumItem.Value);
                    if (result)
                    {
                        SystemDataField systemLogicalDataField = (SystemDataField)enumItem.Value;
                        string systemTablePhysicalName = DataFieldHelper.GetSystemTablePhysicalName(systemLogicalDataField);
                        if (!string.IsNullOrWhiteSpace(systemTablePhysicalName) && 
                            !systemTablePhysicalNames.Contains(systemTablePhysicalName))
                        {
                            systemTablePhysicalNames.Add(systemTablePhysicalName);
                            tableTableLinks.Add(DataFieldHelper.GetTableLink(mainTablePhysicalName, systemLogicalDataField));
                        }
                    }
                }
            }        

            /* 业务表的系统字段 SystemPhysicalDataField.RecordId 和 业务表关系构建 */            
            string lastTableName = mainTablePhysicalName;
            foreach (var obj in customViewAndTableInfos)
            {
                string tablePhysicalName = customTable.GetTablePhysicalName(obj.TableId);
                physicalDataFiledNames.AppendFormat(", {0}.{1} AS {0}_{1}", tablePhysicalName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId));
                TableJoin tableJoin = (TableJoin)obj.TableJoin;
                string primaryDataFieldName = DataFieldHelper.GetLinkedPhysicalDataFieldName((DataFieldRelation)obj.PrimaryDataField);
                TableRelation tableRelation = (TableRelation)obj.TableRelation;
                switch (tableRelation)
                {
                    case TableRelation.Previous:
                        tableTableLinks.Add(new TableLink(lastTableName, primaryDataFieldName, tableJoin, tablePhysicalName, primaryDataFieldName));
                        break;

                    case TableRelation.Primary:
                        tableTableLinks.Add(new TableLink(mainTablePhysicalName, primaryDataFieldName, tableJoin, tablePhysicalName, primaryDataFieldName));
                        break;
                }
                lastTableName = tablePhysicalName;
            }
            
            /* 业务表对应的字段 */
            CustomDataField customDataField = new CustomDataField();
            Dictionary<string, string> associationTableNames = new Dictionary<string, string>();
            if (customViewAndDataFieldInfos.Count > 0)
            {
                physicalDataFiledNames.Append(", ");
            }
            foreach (var customViewAndDataFieldInfo in customViewAndDataFieldInfos)
            {
                CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(customViewAndDataFieldInfo.DataFieldId);
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.PhysicalDataField:
                        physicalDataFiledNames.AppendFormat("{0}, ", customDataFieldInfo.PhysicalName);
                        break;

                    case DataFieldProperty.LogicalDataField:
                        physicalDataFiledNames.AppendFormat("{0} AS {1}, ", customDataField.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId),
                                    customDataFieldInfo.PhysicalName);

                        break;
                }
            }
            if (physicalDataFiledNames.Length > 0)
            {
                physicalDataFiledNames.Remove(physicalDataFiledNames.Length - 2, 2);
            }
            string tableTables = DataAccessHandler.GetTableNames(mainTablePhysicalName, tableTableLinks);
            sb.AppendFormat("SELECT {0} FROM {1} ", physicalDataFiledNames, tableTables);
            try
            {
                string[] sqlViews = new string[] { string.Format("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[{0}]') AND OBJECTPROPERTY(id, N'IsView') = 1) DROP VIEW {0}",
                    customViewInfo.PhysicalName), string.Format("CREATE VIEW {0} AS {1}", customViewInfo.PhysicalName, sb) };

                byte dataWarehouseId = customTable.GetDataWarehouseId(customViewInfo.TableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                foreach (string sqlView in sqlViews)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlView))
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

        /// <summary>
        /// 获得视图的主物理表名
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 物理表名</returns>
        public string GetTablePhysicalName(decimal viewId)
        {
            string tablePhysicalName = string.Empty;

            try
            {
                string sqlSelect = "SELECT CustomTable.PhysicalName FROM CustomView INNER JOIN CustomTable ON CustomView.TableId = CustomTable.TableId WHERE ViewId = @ViewId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(viewId));
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
        /// 获得视图的主表编号
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 视图的主表编号</returns>
        public decimal GetTableId(decimal viewId)
        {
            decimal tableId = decimal.MinValue;

            try
            {
                string sqlSelect = "SELECT TableId FROM CustomView WHERE ViewId = @ViewId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(viewId));
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
        /// 获得视图物理名称
        /// </summary>
        ///<param name="viewId">视图编号</param>
        /// <returns> 视图物理名称</returns>
        public string GetViewPhysicalName(decimal viewId)
        {
            string physicalName = string.Empty;

            try
            {
                string sqlSelect = "SELECT PhysicalName FROM CustomView WHERE ViewId = @ViewId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(viewId));
                    physicalName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return physicalName;
        }

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomViewInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomViewInfo 对象列表</returns>
        private IList<CustomViewInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomViewInfo> customViewInfos = new List<CustomViewInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomView");
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
                            decimal viewId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            string viewName = DataConvertionHelper.GetString(dataReader[3]);
                            string viewCode = DataConvertionHelper.GetString(dataReader[4]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[5]);
                            byte viewProperty = DataConvertionHelper.GetByte(dataReader[6]);
                            long systemDataFields = DataConvertionHelper.GetLong(dataReader[7]);
                            bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[8]);
                            string toolTip = DataConvertionHelper.GetString(dataReader[9]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[10]);
                            string notes = DataConvertionHelper.GetString(dataReader[11]);
                            //将创建 CustomViewInfo 对象加入集合中
                            customViewInfos.Add(new CustomViewInfo(viewId, tableId, groupId, viewName, viewCode,
                            physicalName, viewProperty, systemDataFields, isLeaf, toolTip,
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

            return customViewInfos;
        }

        /// <summary>
        /// 获得 CustomViewInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomViewInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomView");
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
        /// 获得表 CustomView 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomView ", "ViewId", "*", false, false, startPosition,
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
        /// 获得以表 CustomView 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomView ", "ViewId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomView 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomView ", "ViewId", "*", false, false, startPosition,
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
        /// 获得以表 CustomView 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomView ", "ViewId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomViewInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomView");
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
        /// 删除视图
        /// </summary>
        /// <param name="viewName"></param>
        private void DeleteView(string viewName)
        {
            if (string.IsNullOrWhiteSpace(viewName))
            {
                return;
            }
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[{0}]') AND OBJECTPROPERTY(id, N'IsView') = 1) ", viewName);
            sb.AppendFormat("DROP VIEW {0}", viewName);

            try
            {
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sb.ToString()))
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

        #endregion

        #endregion
    }
}
