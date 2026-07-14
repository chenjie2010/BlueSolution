//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDataField.cs
// 描述：CustomDataField 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/9/11
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
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
using Blue.SQLServerDAL.BusinessDesignerModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomDataField 表的数据层访问类
    /// </summary>
    public class CustomDataField : CommonNodeDataAccess, ICustomDataField
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomDataField() : base("CustomDataField", "DataFieldId", "TableId", "LogicalName", "DataFieldCode", false, true, "DataFieldProperty")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomDataField 表中插入一条新记录
        /// </summary>
        /// <param name="customDataFieldInfo">customDataFieldInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomDataFieldInfo customDataFieldInfo)
        {
            //自动增加的关键字的值
            decimal customDataFieldId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customDataFieldInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomDataField", "Sorting", "TableId", customDataFieldInfo.TableId, 0) + 1;
            customDataFieldInfo.PhysicalName = GetDataFieldPhysicalName(customDataFieldInfo.TableId, customDataFieldInfo.Sorting);
            customDataFieldId = InsertCustomDataFieldInfo(customDataFieldInfo, db, null);

            return customDataFieldId;
        }

        /// <summary>
		/// 获得 CustomDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldId">字段编号</param>
		/// <returns> CustomDataFieldInfo 对象</returns>
		public CustomDataFieldInfo GetModelInfo(decimal dataFieldId)
        {
            CustomDataFieldInfo customDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("DataFieldId", "DataFieldId", System.Data.DbType.Decimal, dataFieldId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomDataFieldInfo> customDataFieldInfos = GetModelInfos(whereConditons, null, true);
            if (customDataFieldInfos != null && customDataFieldInfos.Count > 0)
            {
                customDataFieldInfo = customDataFieldInfos[0];
            }

            return customDataFieldInfo;
        }

        /// <summary>
		/// 获得 CustomDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldCode">字段编码</param>
		/// <returns> CustomDataFieldInfo 对象</returns>
		public CustomDataFieldInfo GetModelInfoByCode(string dataFieldCode)
        {
            CustomDataFieldInfo customDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("DataFieldCode", "DataFieldCode", DbType.String, dataFieldCode, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomDataFieldInfo> customDataFieldInfos = GetModelInfos(whereConditons, null, true);
            if (customDataFieldInfos != null && customDataFieldInfos.Count > 0)
            {
                customDataFieldInfo = customDataFieldInfos[0];
            }

            return customDataFieldInfo;
        }

        /// <summary>
        /// 更新 CustomDataFieldInfo 对象
        /// </summary>
        /// <param name="customDataFieldInfo">CustomDataFieldInfo 对象</param>
        public void Update(CustomDataFieldInfo customDataFieldInfo)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    UpdateCustomDataFieldInfo(customDataFieldInfo, transaction);
                    CustomExpression customExpression = new CustomExpression();
                    customExpression.Delete(customDataFieldInfo.DataFieldId, transaction);
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
        ///  删除 CustomDataFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        public void Delete(decimal dataFieldId)
        {
            /* 1. 验证：如果表中的物理字段被其它表的逻辑字段关联，则必须先删除其它表的逻辑字段 */
            int count = GetRelatedDataFieldCount(dataFieldId);
            if (count > 0)
            {
                throw new Exception(string.Format("共有其它表的{0}个字段关联该物理字段，请先删除或是修改这些字段的关联关系！", count));
            }
            CustomDataFieldInfo customDataFieldInfo = GetModelInfo(dataFieldId);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    /* 1. 删除物理字段 */
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                    if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        CustomTable customTable = new CustomTable();
                        int dataWarehouseId = customTable.GetDataWarehouseId(customDataFieldInfo.TableId);
                        string tablePhysicalName = customTable.GetTablePhysicalName(customDataFieldInfo.TableId);
                        DeleteDataField(dataWarehouseId, tablePhysicalName, customDataFieldInfo.PhysicalName);
                    }

                    /* 2. 删除字段及其对应关系 */
                    string[] sqlDeletes = new string[]{
                    "DELETE FROM CustomExpression WHERE ParentDataFieldId = @DataFieldId",
                    "DELETE FROM DataFieldRelationship WHERE ParentDataFieldId = @DataFieldId",
                    "DELETE FROM RoleAndDataField WHERE DataFieldId = @DataFieldId",
                    "DELETE FROM CombinedDataField WHERE DataFieldId = @DataFieldId",
                    "DELETE FROM DataAuditingAndDataField WHERE DataFieldId = @DataFieldId",
                    "DELETE FROM CustomDataField WHERE DataFieldId = @DataFieldId"};
                    foreach (string sqlDelete in sqlDeletes)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                        {
                            db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataFieldId));
                            //执行删除操作
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
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDataFieldInfo 对象列表</returns>
        public IList<CustomDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomDataField ", "DataFieldId", false, whereConditons);
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
        /// 验证自定义字段
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="customDataFieldName"></param>
        /// <returns></returns>
        public bool VerifyCustomDataFieldName(decimal tableId, string customDataFieldName)
        {
            bool success = false;

            CustomTable customTable = new CustomTable();
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
            byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            try
            {
                string sqlSelect = string.Format("SELECT TOP 0 {0} FROM {1}", customDataFieldName, tablePhysicalName);
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.ExecuteNonQuery(dbCommand);
                    success = true;
                }
            }
            catch
            { }

            return success;
        }

        /// <summary>
        /// 刷新基本类型
        /// </summary>
        public void RefreshBasedDataType()
        {
            IDictionary<decimal, PairValues<byte>> dataFields = new Dictionary<decimal, PairValues<byte>>();
            string select = "SELECT DataFieldId, DataFieldProperty, DataFieldType FROM CustomDataField";
            string update = "UPDATE CustomDataField SET BasedDataType = @BasedDataType WHERE DataFieldId = @DataFieldId";


            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(select))
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            byte dataFieldProperty = DataConvertionHelper.GetByte(dataReader[1]);
                            byte dataFieldType = DataConvertionHelper.GetByte(dataReader[2]);
                            dataFields.Add(dataFieldId, new PairValues<byte>(dataFieldProperty, dataFieldType));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }

                foreach (KeyValuePair<decimal, PairValues<byte>> dataField in dataFields)
                {
                    byte basedDataType = 0;
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)dataField.Value.First;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.PhysicalDataField:
                            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)dataField.Value.Second;
                            if (physicalDataFieldType == PhysicalDataFieldType.Association || physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation
                                || physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                            {
                                AssociatedDataField associatedDataField = new AssociatedDataField();
                                decimal associatedDataFieldId = GetAssociatedDataFieldId(dataField.Key);
                                basedDataType = (byte)associatedDataField.GetBasedDataType(associatedDataFieldId);
                            }
                            else
                            {
                                basedDataType = (byte)DataFieldHelper.GetBasedDataType(physicalDataFieldType);
                            }
                            break;

                        case DataFieldProperty.LogicalDataField:
                            LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)dataField.Value.Second;
                            basedDataType = (byte)DataFieldHelper.GetBasedDataType(logicalDataFieldType);
                            break;

                    }
                    using (DbCommand dbCommand = db.GetSqlStringCommand(update))
                    {
                        db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataField.Key);
                        db.AddInParameter(dbCommand, "BasedDataType", DbType.Byte, basedDataType);
                       db.ExecuteScalar(dbCommand);
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
        /// 获得指定字段的附件路径
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFilePath(string dataFieldName, string fileName)
        {
            StringBuilder sbPath = new StringBuilder();
            sbPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
            if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
            {
                sbPath.Append(@"\");
            }
            sbPath.AppendFormat(@"{0}\{1}\", AppSettingHelper.DefaultSubDirOfUploadFiles, dataFieldName);            
            sbPath.Append(fileName);

            return sbPath.ToString();
        }

        /// <summary>
        /// 获得指定字段的附件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetFileData(string dataFieldName, string fileName)
        {
            byte[] data = null;

            StringBuilder sbPath = new StringBuilder();
            sbPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
            if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
            {
                sbPath.Append(@"\");
            }
            sbPath.AppendFormat(@"{0}\{1}\", AppSettingHelper.DefaultSubDirOfUploadFiles, dataFieldName);
            if (!Directory.Exists(sbPath.ToString()))
            {
                Directory.CreateDirectory(sbPath.ToString());
            }
            sbPath.Append(fileName);
            if (System.IO.File.Exists(sbPath.ToString()))
            {
                using (FileStream fs = new FileStream(sbPath.ToString(), FileMode.Open, FileAccess.Read))
                {
                    BinaryReader r = new BinaryReader(fs);
                    data = r.ReadBytes((int)fs.Length);
                }
            }

            return data;
        }

        /// <summary>
        /// 获得表的字段设置的个数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetDataFieldCountUnderSetting(decimal tableId, byte pos)
        {
            int count = 0;

            //查询语句
            Int64 dataFieldSetting = 1L << pos;
            string sqlSelect = string.Format("SELECT COUNT(1) FROM CustomDataField WHERE TableId = @TableId AND DataFieldSetting & {0} > 0", dataFieldSetting);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {                
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
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
        /// 查询该物理字段被其它字段关联(枚举关联或是逻辑字段关联)的总数
        /// </summary>
        /// <param name="parentDataFieldId">物理字段的编号</param>
        /// <returns></returns>
        public int GetRelatedDataFieldCount(decimal parentDataFieldId)
        {
            int count = 0;

            //查询语句
            string[] sqlSelects = new string[] {"SELECT COUNT(1) FROM CustomDataField WHERE ParentDataFieldId = @ParentDataFieldId ",
                "SELECT COUNT(1) FROM CustomExpression WHERE DataFieldId = @ParentDataFieldId"};

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                foreach (string sqlSelect in sqlSelects)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                    {
                        db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, parentDataFieldId);
                        count += DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                    }
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
        /// 查询该表下物理字段被其它字段关联(枚举关联或是逻辑字段关联)的总数
        /// </summary>
        /// <param name="tableId">物理表的编号</param>
        /// <returns></returns>
        public int GetRelatedDataFieldCountByTableId(decimal tableId)
        {
            int count = 0;

            //查询语句
            string[] sqlSelects = new string[] {"SELECT COUNT(1) FROM CustomDataField A INNER JOIN CustomDataField B ON B.DataFieldId = A.ParentDataFieldId WHERE B.TableId = @TableId AND  A.TableId != @TableId ",
                "SELECT COUNT(1) FROM CustomExpression INNER JOIN CustomDataField A ON A.DataFieldId = CustomExpression.ParentDataFieldId INNER JOIN  CustomDataField B  ON B.DataFieldId = CustomExpression.ParentDataFieldId WHERE A.TableId = @TableId AND B.TableId != @TableId"};

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                foreach (string sqlSelect in sqlSelects)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                    {
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                        count += DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                    }
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
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal tableId)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT A.LogicalName, A.DataFieldCode, A.DataFieldType, A.DataFieldLength,  A.RegexExpression, ");
                sb.Append("CONCAT(CustomEnum.EnumCode, AssociatedDataField.DataFieldCode) AS CommonCode, B.DataFieldCode, A.RequiredDataField, A.AutoComplete, ");
                sb.Append("A.IndexCreated, A.DataFieldSetting, A.HelpEnabled, A.HelpContent,A.Tooltip, A.Notes FROM CustomDataField A ");
                sb.Append("LEFT JOIN CustomDataField B ON A.ParentDataFieldId = B.DataFieldId ");
                sb.Append("LEFT JOIN CustomEnum ON  A.EnumId = CustomEnum.EnumId ");
                sb.Append("LEFT JOIN AssociatedDataField ON  A.AssociatedDataFieldId = AssociatedDataField.AssociatedDataFieldId ");
                sb.Append("WHERE A.TableId = @TableId AND A.DataFieldProperty = @DataFieldProperty");
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    db.AddInParameter(dbCommand, "DataFieldProperty", DbType.Byte, (byte)DataFieldProperty.PhysicalDataField);

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
        /// 批量插入物理字段
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="customDataFieldInfos"></param>
        /// <param name="enumCodeRelation"></param>
        /// <param name="secondaryCodeRelation"></param>
        public void Insert(decimal tableId, List<CustomDataFieldInfo> customDataFieldInfos, Dictionary<string, string> enumCodeRelation,
            Dictionary<string, IList<string>> secondaryCodeRelation)
        {
            try
            {
                //获得系统数据库对象
                Dictionary<string, decimal> dataFieldCodeAndIds = new Dictionary<string, decimal>();
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        foreach (var customDataFieldInfo in customDataFieldInfos)
                        {
                            customDataFieldInfo.DataFieldId = InsertCustomDataFieldInfo(customDataFieldInfo, db, transaction);
                            if (!dataFieldCodeAndIds.ContainsKey(customDataFieldInfo.DataFieldCode))
                            {
                                dataFieldCodeAndIds.Add(customDataFieldInfo.DataFieldCode, customDataFieldInfo.DataFieldId);
                            }
                        }
                        /* 更新枚举依赖类型字段的父编号 */
                        foreach (var keyValue in enumCodeRelation)
                        {
                            if (dataFieldCodeAndIds.ContainsKey(keyValue.Value))
                            {
                                decimal parentId = dataFieldCodeAndIds[keyValue.Value];
                                decimal dataFieldId = customDataFieldInfos.Find(s => s.DataFieldCode == keyValue.Key).DataFieldId;
                                if (dataFieldId > 0)
                                {
                                    UpdateParentDataFieldId(dataFieldId, parentId, db, transaction);
                                }
                            }
                        }
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }               
                    /* 在该主关联类型字段创建之前，更新所有依赖该字段的次关联类型字段的父编号 */
                    foreach (var keyValue in secondaryCodeRelation)
                    {
                        if (dataFieldCodeAndIds.ContainsKey(keyValue.Key))
                        {
                            decimal parentId = dataFieldCodeAndIds[keyValue.Key];
                            foreach (var secondaryCode in keyValue.Value)
                            {
                                CustomDataFieldInfo customDataFieldInfo = customDataFieldInfos.Find(s => s.DataFieldCode == secondaryCode);
                                if (customDataFieldInfo != null)
                                {
                                    UpdateParentDataFieldId(customDataFieldInfo.DataFieldId, parentId, db, null);
                                }
                                else
                                {
                                    decimal dataFieldId = GetNodeIdByNodeCode(secondaryCode);
                                    if (dataFieldId > 0)
                                    {
                                        UpdateParentDataFieldId(dataFieldId, parentId, db, null);

                                    }
                                }
                            }

                        }
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
        /// 获得关联字段被关联的物理字段信息表
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public DataSet GetDataFieldsConnected(decimal associatedDataFieldId)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDatabase.DatabaseName, CustomCategory.CategoryName, CustomTable.LogicalName, ");
            sb.Append("CustomDataField.LogicalName AS CustomDataField_LogicalName FROM CustomDataField ");
            sb.Append("INNER JOIN CustomTable ON CustomDataField.TableId = CustomTable.TableId ");
            sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("INNER JOIN  CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("WHERE CustomDataField.AssociatedDataFieldId = @AssociatedDataFieldId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, associatedDataFieldId);
                    ds = db.ExecuteDataSet(dbCommand);
                }
                ds.Tables[0].Columns["DatabaseName"].Caption = "数据库名称";
                ds.Tables[0].Columns["CategoryName"].Caption = "分组名称";
                ds.Tables[0].Columns["LogicalName"].Caption = "数据表名称";
                ds.Tables[0].Columns["CustomDataField_LogicalName"].Caption = "字段名称";                
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }        

        /// <summary>
        /// 获得关联字段的个数
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public int GetDataFieldCountConnected(decimal associatedDataFieldId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("AssociatedDataFieldId", "AssociatedDataFieldId", DbType.Decimal, associatedDataFieldId, DataFieldCondition.Equal));

            return GetTotalCount(whereConditons);
        }

        /// <summary>
        /// 获取字段类型属于该枚举的字段个数
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public int GetDataFieldCountByEnumId(decimal enumId)
        {
            int count = 0;

            //查询语句
            string sqlSelect = "SELECT COUNT(1) FROM CustomDataField WHERE EnumId = @EnumId ";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, enumId);
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
        /// 获得枚举字段编号
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public decimal GetEnumId(decimal dataFieldId)
        {
            decimal enumId = 0;

            try
            {
                string sqlSelect = "SELECT EnumId FROM CustomDataField WHERE DataFieldId = @DataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataFieldId));
                    enumId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumId;
        }

        /// <summary>
        /// 获得关联字段编号
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public decimal GetParentDataFieldId(decimal dataFieldId)
        {
            decimal parentDataFieldId = 0;

            try
            {
                string sqlSelect = "SELECT ParentDataFieldId FROM CustomDataField WHERE DataFieldId = @DataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataFieldId));
                    parentDataFieldId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return parentDataFieldId;
        }

        /// <summary>
        /// 获得枚举类型的字段属性
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodesByEnumId(decimal enumId)
        {
            //创建集合对象
            IList<CommonNode> commonNodes = new List<CommonNode>();

            string sqlSelect = "SELECT DataFieldId, TableId, LogicalName, PhysicalName, DataFieldType FROM CustomDataField WHERE EnumId = @EnumId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, enumId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[2]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[3]);
                            byte dataFieldType = DataConvertionHelper.GetByte(dataReader[4]);
                            commonNodes.Add(new CommonNode(dataFieldId, tableId, logicalName, physicalName, dataFieldType));
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

        /// <summary>
        /// 获得枚举类型的物理字段信息表
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public DataSet GetDataFieldsByEnumId(decimal enumId)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDatabase.DatabaseName, CustomCategory.CategoryName, CustomTable.LogicalName, ");
            sb.Append("CustomDataField.LogicalName AS CustomDataField_LogicalName FROM CustomDataField ");
            sb.Append("INNER JOIN CustomTable ON CustomDataField.TableId = CustomTable.TableId ");
            sb.Append("INNER JOIN CustomCategory ON CustomCategory.CategoryId = CustomTable.CategoryId ");
            sb.Append("INNER JOIN  CustomDatabase ON CustomDatabase.DatabaseId = CustomCategory.DatabaseId ");
            sb.Append("WHERE CustomDataField.EnumId = @EnumId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, enumId);
                    ds = db.ExecuteDataSet(dbCommand);
                }
                ds.Tables[0].Columns["DatabaseName"].Caption = "数据库名称";
                ds.Tables[0].Columns["CategoryName"].Caption = "分组名称";
                ds.Tables[0].Columns["LogicalName"].Caption = "数据表名称";
                ds.Tables[0].Columns["CustomDataField_LogicalName"].Caption = "字段名称";
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表达式类型字段组合名称
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public string GetDataFieldLogicalExpression(decimal dataFieldId)
        {
            string expressionDataFieldName = string.Empty;

            try
            {
                CustomDataFieldInfo customDataFieldInfo = GetModelInfo(dataFieldId);
                CustomTable customTable = new CustomTable();
                string tablePhysicalName = customTable.GetTablePhysicalName(customDataFieldInfo.TableId);
                if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.LogicalDataField)
                {
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)customDataFieldInfo.DataFieldType;
                    switch (logicalDataFieldType)
                    {
                        case LogicalDataFieldType.DigitExpression:
                        case LogicalDataFieldType.StringExpression:
                        case LogicalDataFieldType.DateTimeExpression:
                            CustomExpression customExpression = new CustomExpression();
                            IList<CommonNode> commonNodes =  customExpression.GetCommonNodes(dataFieldId);
                            expressionDataFieldName = GetExpressionDataFieldName(tablePhysicalName, customDataFieldInfo.ExpressionText, commonNodes);
                            break;

                        case LogicalDataFieldType.OneDimCode:
                            expressionDataFieldName = GetPhysicalName(customDataFieldInfo.ParentDataFieldId);
                            break;

                        case LogicalDataFieldType.UserName:
                            expressionDataFieldName = string.Format("{0}.{1}", tablePhysicalName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName));
                            break;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return expressionDataFieldName;
        }

        /// <summary>
        /// 获得 CommonNode 对象
        /// </summary>
        /// <param name="physicalName"></param>
        /// <returns></returns>
        public CommonNode GetCommonNode(string physicalName)
        {
            CommonNode commonNode = null;

            try
            {
                string sqlSelect = "SELECT DataFieldId, TableId, LogicalName, DataFieldProperty FROM CustomDataField WHERE PhysicalName = @PhysicalName";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "PhysicalName", DbType.String, physicalName);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[2]);
                            byte dataFieldProperty = DataConvertionHelper.GetByte(dataReader[3]);

                            commonNode = new CommonNode(dataFieldId, tableId, logicalName, physicalName, true, dataFieldProperty);
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
		/// 获得 CustomDataFieldInfo 对象
		/// </summary>
		///<param name="physicalName">字段物理名称</param>
		/// <returns> CustomDataFieldInfo 对象</returns>
		public CustomDataFieldInfo GetModelInfo(string physicalName)
        {
            CustomDataFieldInfo customDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("PhysicalName", "PhysicalName", System.Data.DbType.String, physicalName, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomDataFieldInfo> customDataFieldInfos = GetModelInfos(whereConditons, null, true);
            if (customDataFieldInfos != null && customDataFieldInfos.Count > 0)
            {
                customDataFieldInfo = customDataFieldInfos[0];
            }

            return customDataFieldInfo;
        }

        /// <summary>
		/// 向 CustomDataField 表中插入一条新记录
		/// </summary>
		/// <param name="customDataFieldInfo">customDataFieldInfo 对象</param>
        /// <param name="customExpressionInfos">表达式字段</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomDataFieldInfo customDataFieldInfo, IList<CustomExpressionInfo> customExpressionInfos)
        {
            //自动增加的关键字的值
            decimal customDataFieldId = 0;

            CustomExpression customExpression = new CustomExpression();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customDataFieldInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomDataField", "Sorting", "TableId", customDataFieldInfo.TableId, 0) + 1;
            customDataFieldInfo.PhysicalName = GetDataFieldPhysicalName(customDataFieldInfo.TableId, customDataFieldInfo.Sorting);            
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    customDataFieldId = InsertCustomDataFieldInfo(customDataFieldInfo, db, transaction);
                    customExpression.Insert(customDataFieldId, customExpressionInfos, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customDataFieldId;
        }

        /// <summary>
		/// 更新 CustomDataFieldInfo 对象
		/// </summary>
		/// <param name="customDataFieldInfo">CustomDataFieldInfo 对象</param>
		public void Update(CustomDataFieldInfo customDataFieldInfo, IList<CustomExpressionInfo> customExpressionInfos)
        {
            CustomExpression customExpression = new CustomExpression();

            bool update = false;

            IList<CustomExpressionInfo> expressionInfos = customExpression.GetModelInfos(customDataFieldInfo.DataFieldId);
            if (expressionInfos.Count == customExpressionInfos.Count)
            {
                for (int idx = 0; idx < expressionInfos.Count; idx++)
                {
                    if ((expressionInfos[idx].ParentDataFieldId != customExpressionInfos[idx].ParentDataFieldId)
                        || (expressionInfos[idx].Sorting != customExpressionInfos[idx].Sorting))
                    {
                        update = true;
                        break;
                    }
                }
            }
            else
            {
                update = true;
            }

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    UpdateCustomDataFieldInfo(customDataFieldInfo, transaction);
                    if (update)
                    {
                        customExpression.Update(customDataFieldInfo.DataFieldId, customExpressionInfos, transaction);
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
        /// 验证表达式类型
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="expressionText">表达式文本</param>
        /// <param name="commonNodes">字段列表</param>
        /// <returns>是否通过验证</returns>
        public bool VerifyExpression(decimal tableId, string expressionText, IList<CommonNode> commonNodes)
        {
            bool result = true;

            try
            {
                CustomTable customTable = new CustomTable();
                string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
                byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                string expressionDataFieldName = GetExpressionDataFieldName(tablePhysicalName, expressionText, commonNodes);
                Dictionary<string, TableLink> systemTableLinks = new Dictionary<string, TableLink>();
                bool hasSystemDataField = false;
                foreach (CommonNode commonNode in commonNodes)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)commonNode.NodeType;
                    if (dataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
                    {
                        SystemDataField systemLogicalDataField = (SystemDataField)commonNode.NodeId;
                        string systemTablePhysicalName = DataFieldHelper.GetSystemTablePhysicalName(systemLogicalDataField);
                        if (!systemTableLinks.ContainsKey(systemTablePhysicalName))
                        {
                            systemTableLinks.Add(systemTablePhysicalName, DataFieldHelper.GetTableLink(tablePhysicalName, systemLogicalDataField));
                        }
                        hasSystemDataField = true;
                    }
                }
                IList<TableLink> tableLinks = new List<TableLink>();
                if (hasSystemDataField)
                {
                    /* 增加系统用户表的链接关系 */
                    string userAccountTableName = DataFieldHelper.GetSystemTablePhysicalName(SystemDataField.UserName);
                    if (!systemTableLinks.ContainsKey(userAccountTableName))
                    {
                        tableLinks.Add(new TableLink("[Blue].[dbo].[UserAccount]", "UserId", TableJoin.InnerJoin));
                    }
                }
                foreach (KeyValuePair<string, TableLink> kyeValue in systemTableLinks)
                {
                    tableLinks.Add(kyeValue.Value);
                }

                string name = DataAccessHandler.GetTableNames(tablePhysicalName, tableLinks);
                string sql = string.Format("SELECT COUNT({0}) FROM {1}", expressionDataFieldName, name);

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sql))
                {
                    DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                }
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 验证 WHERE 条件
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool ValidateWhereExpression(CustomDataFieldInfo customDataFieldInfo, string whereExpression)
        {
            bool success = false;

            CustomTable customTable = new CustomTable();
            string tablePhysicalName = string.Empty;
            if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
            {
                SystemDataField systemDataField = (SystemDataField)Convert.ToByte(customDataFieldInfo.DataFieldId);
                if (systemDataField != SystemDataField.UserTypeName && systemDataField != SystemDataField.DepName)
                {
                    tablePhysicalName = DataFieldHelper.GetSystemTablePhysicalName(systemDataField);
                }
            }
            SqlDatabase db = null;
            if (string.IsNullOrWhiteSpace(tablePhysicalName))
            {
                tablePhysicalName = customTable.GetTablePhysicalName(customDataFieldInfo.TableId);
                byte dataWarehouseId = customTable.GetDataWarehouseId(customDataFieldInfo.TableId);
                db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            }
            else
            {
                db = DataAccessHelper.GetDatabase();
            }
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT TOP 0 * FROM ");
                sb.Append(tablePhysicalName);
                if (!string.IsNullOrEmpty(whereExpression))
                {
                    sb.Append(" WHERE ");
                    sb.Append(whereExpression);
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.ExecuteNonQuery(dbCommand);
                    success = true;
                }
            }
            catch
            { }

            return success;
        }

        /// <summary>
        /// 获得组合后的表达式字段名称
        /// </summary>
        /// <param name="expressionText"></param>
        /// <param name="expressionText"></param>
        /// <returns></returns>
        public string GetExpressionDataFieldName(string tablePhysicalName, string expressionText, IList<CommonNode> commonNodes)
        {
            string dataFieldPhysicalName = string.Empty;
            StringBuilder sbText = new StringBuilder();
            int index = 0;
            sbText.Append(expressionText);
            foreach (CommonNode commonNode in commonNodes)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)commonNode.NodeType;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                        dataFieldPhysicalName = DataFieldHelper.GetSystemLogicalDataFieldName(tablePhysicalName, (SystemDataField)commonNode.NodeId);
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        dataFieldPhysicalName = GetPhysicalName(commonNode.NodeId);
                        break;

                    default:
                        throw new ArgumentException("逻辑字段只能够由系统字段和物理字段构成。");
                }
                if (string.IsNullOrWhiteSpace(tablePhysicalName))
                {
                    sbText.Replace(string.Format("{{{0}}}", index), dataFieldPhysicalName);
                }
                else
                {
                    sbText.Replace(string.Format("{{{0}}}", index), string.Format("{0}.{1}", tablePhysicalName, dataFieldPhysicalName));
                }
                index++;
            }

            return sbText.ToString();
        }

        /// <summary>
        /// 获得字段的帮助内容
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的帮助内容</returns>
        public string GetHelpContent(decimal dataFieldId)
        {
            string helpContent = string.Empty;

            try
            {
                string sqlSelect = "SELECT HelpContent FROM CustomDataField WHERE DataFieldId = @DataFieldId AND HelpEnabled = @HelpEnabled";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataFieldId));
                    db.AddInParameter(dbCommand, "HelpEnabled", DbType.Boolean, true);
                    helpContent = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return helpContent;
        }

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetLogicalName(decimal dataFieldId)
        {
            string logicalName = string.Empty;

            try
            {
                string sqlSelect = "SELECT LogicalName FROM CustomDataField WHERE DataFieldId = @DataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataFieldId));
                    logicalName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return logicalName;
        }

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="physicalName">字段物理名称</param>
        /// <returns> 字段的物理名称</returns>
        public string GetLogicalName(string physicalName)
        {
            string logicalName = string.Empty;

            try
            {
                string sqlSelect = "SELECT LogicalName FROM CustomDataField WHERE PhysicalName = @PhysicalName";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "PhysicalName", DbType.String, physicalName);
                    logicalName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return logicalName;
        }

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public IList<string> GetLogicalNames(IList<decimal> dataFieldIds)
        {
            IList<string> logicalNames = new List<string>();

            try
            {
                IList<WhereConditon> whereConditons = DataAccessHandler.GetWhereConditons(dataFieldIds, "CustomDataField", "DataFieldId");
                string where = DataAccessHandler.GetWhereSentence(whereConditons);

                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT LogicalName FROM CustomDataField ");
                if (!string.IsNullOrWhiteSpace(where))
                {
                    sb.AppendFormat("WHERE {0}", where);
                }
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            logicalNames.Add(DataConvertionHelper.GetString(dataReader[0]));
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

            return logicalNames;
        }

        /// <summary>
        /// 获得完整的字段逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetFullLogicalName(decimal dataFieldId)
        {
            string logicalName = string.Empty;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT CustomDatabase.DatabaseName, CustomCategory.CategoryName, CustomTable.LogicalName, CustomDataField.LogicalName FROM CustomDataField ");
                sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = CustomDataField.TableId ");
                sb.Append("INNER JOIN CustomCategory ON CustomTable.CategoryId = CustomCategory.CategoryId ");
                sb.Append("INNER JOIN CustomDatabase ON CustomDatabase.DatabaseId =  CustomCategory.DatabaseId ");
                sb.Append(" WHERE DataFieldId = @DataFieldId");

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataFieldId));
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            string databaseName = DataConvertionHelper.GetString(dataReader[0]);
                            string categoryName = DataConvertionHelper.GetString(dataReader[1]);
                            string tableLogicalName = DataConvertionHelper.GetString(dataReader[2]);
                            string dataFieldLogicalName = DataConvertionHelper.GetString(dataReader[3]);
                            logicalName = string.Format("[{0}][{1}][{2}][{3}]", databaseName, categoryName, tableLogicalName, dataFieldLogicalName);
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

            return logicalName;
        }

        /// <summary>
        /// 获得逻辑字段表达式的值
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetExpressionText(decimal dataFieldId)
        {
            string expressionText = string.Empty;

            try
            {
                string sqlSelect = "SELECT ExpressionText FROM CustomDataField WHERE DataFieldId = @DataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataFieldId));
                    expressionText = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return expressionText;
        }

        /// <summary>
        /// 获得字段类型
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public byte GetDataFieldType(decimal dataFieldId)
        {
            byte dataFieldType = 0;

            try
            {
                string sqlSelect = "SELECT DataFieldType FROM CustomDataField WHERE  DataFieldId = @DataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.String, dataFieldId);
                    dataFieldType = Convert.ToByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldType;
        }

        /// <summary>
        /// 获得表编号
        /// </summary>
        ///<param name="dataFieldId"></param>
        /// <returns>表编号 </returns>
        public decimal GetTableId(decimal dataFieldId)
        {
            decimal tableId = 0;

            try
            {
                string sqlSelect = "SELECT TableId FROM CustomDataField WHERE  DataFieldId = @DataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.String, dataFieldId);
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
        /// 获得字段的物理名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetPhysicalName(decimal dataFieldId)
        {
            string dataFieldPhysicalName = string.Empty;

            try
            {
                string sqlSelect = "SELECT PhysicalName FROM CustomDataField WHERE DataFieldId = @DataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataFieldId));
                    dataFieldPhysicalName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldPhysicalName;
        }

        /// <summary>
        /// 根据数据类型获得字段列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="basedDataType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal tableId, BasedDataType basedDataType)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("TableId", "TableId", DbType.Decimal, tableId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("BasedDataType", "BasedDataType", DbType.Byte, (byte)basedDataType, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            commonNodes = GetCommonNodesByWhereConditon(whereConditons);

            return commonNodes;
        }

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal tableId, DataFieldFilter dataFieldFilter)
        {
            IList<CommonNode> commonNodes = null;

            IList<WhereConditon> whereConditons = DataFieldHelper.GetWhereConditons(tableId, dataFieldFilter);

            commonNodes = GetCommonNodesByWhereConditon(whereConditons);

            return commonNodes;
        }

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal parentDataFieldId, bool inTheSameTable)
        {
            IList<CommonNode> commonNodes = null;

            decimal tableId = GetTableId(parentDataFieldId);
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            DataFieldCondition dataFieldCondition = DataFieldCondition.Equal;
            if (!inTheSameTable)
            {
                dataFieldCondition = DataFieldCondition.Not;
            }
            whereConditons.Add(new WhereConditon("TableId", "TableId", DbType.Decimal, tableId, dataFieldCondition, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("ParentDataFieldId", "ParentDataFieldId", DbType.Decimal, parentDataFieldId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            commonNodes = GetCommonNodesByWhereConditon(whereConditons);

            return commonNodes;
        }

        /// <summary>
        /// 根据父节点编号条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodesByParentDataFieldId(decimal parentDataFieldId)
        {
            IList<CommonNode> commonNodes = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("ParentDataFieldId", "ParentDataFieldId", DbType.Decimal, parentDataFieldId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            commonNodes = GetCommonNodesByWhereConditon(whereConditons);

            return commonNodes;
        }

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal tableId, byte dataFieldType)
        {
            IList<CommonNode> commonNodes = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("TableId", "TableId", DbType.Decimal, tableId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType", DbType.Byte, dataFieldType, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            commonNodes = GetCommonNodesByWhereConditon(whereConditons);

            return commonNodes;
        }

        /// <summary>
        /// 获得关联字段
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetModelsByAssociatedDataFieldId(decimal associatedDataFieldId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("AssociatedDataFieldId", "AssociatedDataFieldId", DbType.Decimal, associatedDataFieldId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得字段的关联字段编号
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public decimal GetAssociatedDataFieldId(decimal dataFieldId)
        {
            decimal associatedDataFieldId = 0;

            try
            {
                string sqlSelect = "SELECT AssociatedDataFieldId FROM CustomDataField WHERE DataFieldId = @DataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataFieldId));
                    associatedDataFieldId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return associatedDataFieldId;
        }


        /// <summary>
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> parentNames = new List<string>();

            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDataField.LogicalName, CustomTable.LogicalName, CategoryName FROM CustomDataField ");
            sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = CustomDataField.TableId ");
            sb.Append("INNER JOIN CustomCategory ON CustomTable.CategoryId = CustomCategory.CategoryId ");
            sb.Append("WHERE DataFieldId = @DataFieldId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, nodeId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            string dataFieldName = DataConvertionHelper.GetString(dataReader[0]);
                            string tableName = DataConvertionHelper.GetString(dataReader[1]);
                            string categoryName = DataConvertionHelper.GetString(dataReader[2]);
                            parentNames.Add(categoryName);
                            parentNames.Add(tableName);
                            parentNames.Add(dataFieldName);
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


            return parentNames;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得角色条件字段名称集合
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public IList<string> GetRoleConditionDataFieldNames(decimal tableId)
        {
            IList<string> dataFieldNames = null;

            string sqlSelect = "SELECT PhysicalName FROM CustomDataField WHERE TableId = @TableId AND DataFieldSetting & @DataFieldSetting > 0";
            long dataFieldSettingValue = 1L;
            int pos = (byte)DataFieldSetting.RoleUnderCondition;
            if(pos > 0)
            {
                dataFieldSettingValue = dataFieldSettingValue << pos;
            }

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
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
        /// 获得物理字段名称
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public IList<string> GetPhysicalNames(decimal tableId)
        {
            IList<string> names = new List<string>();

            string sqlSelect = "SELECT PhysicalName FROM CustomDataField WHERE TableId = @TableId ORDER BY Sorting";

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            string physicalName = DataConvertionHelper.GetString(dataReader[0]);                           
                            names.Add(physicalName);
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
        /// 向 CustomDataField 表中批量复制记录并创建字段
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="customDataFieldInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <param name="dbBusiness"></param>
        /// <param name="tablePhysicalName"></param>
		public void CopyCustomDataFieldInfos(decimal tableId, IList<CustomDataFieldInfo> customDataFieldInfos, SqlDatabase db, DbTransaction transaction,
            SqlDatabase dbBusiness, string tablePhysicalName)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomDataField(AssociatedDataFieldId, TableId, ParentDataFieldId, EnumId, LogicalName, ");
            sb.Append("PhysicalName, DataFieldCode, DataFieldProperty, DataFieldType, DataFieldLength, BasedDataType, ");
            sb.Append("RegexExpression, ExpressionText, DataFieldSetting, RequiredDataField, AutoComplete, ");
            sb.Append("IndexCreated, HelpEnabled, Tooltip, HelpContent, Sorting, Notes)");
            sb.Append("VALUES (@AssociatedDataFieldId, @TableId, @ParentDataFieldId, @EnumId, @LogicalName, ");
            sb.Append("@PhysicalName, @DataFieldCode, @DataFieldProperty, @DataFieldType, @DataFieldLength, @BasedDataType, ");
            sb.Append("@RegexExpression, @ExpressionText, @DataFieldSetting, @RequiredDataField, @AutoComplete, ");
            sb.Append("@IndexCreated, @HelpEnabled, @HelpContent, @Tooltip, @Sorting, @Notes);");
            sb.Append("SET @DataFieldId = SCOPE_IDENTITY()");

            try
            {
                CustomExpression customExpression = new CustomExpression();
                Dictionary<decimal, IList<CustomExpressionInfo>> customExpressionInfos = new Dictionary<decimal, IList<CustomExpressionInfo>>();
                foreach (CustomDataFieldInfo customDataFieldInfo in customDataFieldInfos)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                    if (dataFieldProperty == DataFieldProperty.LogicalDataField)
                    {
                        IList<CustomExpressionInfo> expressionInfos = customExpression.GetModelInfos(customDataFieldInfo.DataFieldId);
                        customExpressionInfos.Add(customDataFieldInfo.DataFieldId, expressionInfos);
                    }
                    customDataFieldInfo.TableId = tableId;
                }

                /* 字段旧编号与新编号对应关系 */
                Dictionary<decimal, decimal> dataFieldIdMap = new Dictionary<decimal, decimal>();
                /* 字段旧编号与其父编号对应关系*/
                Dictionary<decimal, decimal> dataFieldIdAndParentIdMap = new Dictionary<decimal, decimal>();
                foreach (var customDataFieldInfo in customDataFieldInfos)
                {
                    if (customDataFieldInfo.ParentDataFieldId > 0)
                    {
                        dataFieldIdAndParentIdMap.Add(customDataFieldInfo.DataFieldId, customDataFieldInfo.ParentDataFieldId);
                    }
                    customDataFieldInfo.PhysicalName = GenerateDataFieldPhysicalName(tableId, customDataFieldInfo.Sorting);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "DataFieldId", DbType.Decimal, 8);
                        db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataFieldInfo.AssociatedDataFieldId));
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, customDataFieldInfo.TableId);
                        db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataFieldInfo.ParentDataFieldId));
                        db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataFieldInfo.EnumId));
                        db.AddInParameter(dbCommand, "LogicalName", DbType.String, customDataFieldInfo.LogicalName);
                        db.AddInParameter(dbCommand, "PhysicalName", DbType.String, customDataFieldInfo.PhysicalName);
                        db.AddInParameter(dbCommand, "DataFieldCode", DbType.String, customDataFieldInfo.DataFieldCode);
                        db.AddInParameter(dbCommand, "DataFieldProperty", DbType.Byte, customDataFieldInfo.DataFieldProperty);
                        db.AddInParameter(dbCommand, "DataFieldType", DbType.Byte, customDataFieldInfo.DataFieldType);
                        db.AddInParameter(dbCommand, "DataFieldLength", DbType.Int32, DataConvertionHelper.SetInt(customDataFieldInfo.DataFieldLength));
                        db.AddInParameter(dbCommand, "BasedDataType", DbType.Byte, customDataFieldInfo.BasedDataType);
                        db.AddInParameter(dbCommand, "RegexExpression", DbType.String, customDataFieldInfo.RegexExpression);
                        db.AddInParameter(dbCommand, "ExpressionText", DbType.String, customDataFieldInfo.ExpressionText);
                        db.AddInParameter(dbCommand, "DataFieldSetting", DbType.Int64, DataConvertionHelper.SetLong(customDataFieldInfo.DataFieldSetting));
                        db.AddInParameter(dbCommand, "RequiredDataField", DbType.Boolean, customDataFieldInfo.RequiredDataField);
                        db.AddInParameter(dbCommand, "AutoComplete", DbType.Boolean, customDataFieldInfo.AutoComplete);
                        db.AddInParameter(dbCommand, "IndexCreated", DbType.Boolean, customDataFieldInfo.IndexCreated);
                        db.AddInParameter(dbCommand, "HelpEnabled", DbType.Boolean, customDataFieldInfo.HelpEnabled);
                        db.AddInParameter(dbCommand, "HelpContent", DbType.String, customDataFieldInfo.HelpContent);
                        db.AddInParameter(dbCommand, "Tooltip", DbType.String, customDataFieldInfo.Tooltip);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customDataFieldInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customDataFieldInfo.Notes);
                        //执行插入操作
                        int count = db.ExecuteNonQuery(dbCommand, transaction);                        
                        if (count != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        decimal customDataFieldId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@DataFieldId"].Value, 0);
                        dataFieldIdMap.Add(customDataFieldInfo.DataFieldId, customDataFieldId);
                        if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                        {
                            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                            if (physicalDataFieldType == PhysicalDataFieldType.Association 
                                || physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation 
                                || physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                            {
                                AssociatedDataField associatedDataField = new AssociatedDataField();
                                BasedDataType basedDataType = associatedDataField.GetBasedDataType(customDataFieldInfo.AssociatedDataFieldId);
                                physicalDataFieldType = DataFieldHelper.GetPhysicalDataFieldType(basedDataType);
                                customDataFieldInfo.DataFieldLength = associatedDataField.GetDataLength(customDataFieldInfo.AssociatedDataFieldId);
                            }
                            CreateDataField(dbBusiness, tablePhysicalName, customDataFieldInfo.PhysicalName, physicalDataFieldType, customDataFieldInfo.DataFieldLength);
                        }
                    }
                }
                /* 关联字段 */
                foreach (KeyValuePair<decimal, decimal> keyValue in dataFieldIdAndParentIdMap)
                {
                    if(dataFieldIdMap.ContainsKey(keyValue.Value))
                    {
                        UpdateParentDataFieldId(dataFieldIdMap[keyValue.Key], dataFieldIdMap[keyValue.Value], db, transaction);
                    }
                }
                /* 表达式 */
                IList<CustomExpressionInfo> newExpressionInfos = new List<CustomExpressionInfo>();
                foreach (var keyValue in customExpressionInfos)
                {
                    if (dataFieldIdMap.ContainsKey(keyValue.Key))
                    {
                        decimal newDataFieldId = dataFieldIdMap[keyValue.Key];
                        foreach (CustomExpressionInfo customExpressionInfo in keyValue.Value)
                        {
                            if (dataFieldIdMap.ContainsKey(customExpressionInfo.DataFieldId))
                            {
                                customExpressionInfo.DataFieldId = dataFieldIdMap[customExpressionInfo.DataFieldId];
                                customExpressionInfo.ParentDataFieldId = newDataFieldId;
                                newExpressionInfos.Add(customExpressionInfo);                                
                            }
                        }
                    }                        
                }
                if(newExpressionInfos.Count > 0)
                {
                    customExpression.Insert(newExpressionInfos, db, transaction);
                }                
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除表的字段记录及字段关联关系
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void DeleteRecords(decimal tableId, SqlDatabase db, DbTransaction transaction)
        {
            /* 2. 删除字段及其对应关系 */
            string[] sqlDeletes = new string[]{
                    "DELETE CustomExpression FROM CustomExpression INNER JOIN CustomDataField ON CustomDataField.DataFieldId = CustomExpression.ParentDataFieldId WHERE TableId = @TableId",
                    "DELETE DataFieldRelationship FROM DataFieldRelationship INNER JOIN CustomDataField ON CustomDataField.DataFieldId = DataFieldRelationship.ParentDataFieldId WHERE TableId = @TableId",
                    "DELETE FROM CustomDataField WHERE TableId = @TableId"};
            foreach (string sqlDelete in sqlDeletes)
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(tableId));
                    //执行删除操作
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
        }

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetModelInfos(decimal tableId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("TableId", "TableId", DbType.Decimal, tableId, DataFieldCondition.Equal));            
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetModelInfos(decimal tableId, DataFieldFilter dataFieldFilter)
        {
            IList<WhereConditon> whereConditons = DataFieldHelper.GetWhereConditons(tableId, dataFieldFilter);
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDataFieldInfo 对象列表</returns>
        public IList<CustomDataFieldInfo> GetModelInfos(IList<decimal> tableIds)
        {
            IList<WhereConditon> whereConditons = DataAccessHandler.GetWhereConditons(tableIds, "CustomDataField", "TableId");
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 判断字段在表中是否存在 
        /// </summary>        
        /// <param name="db">物理数据库对象</param>
        /// <param name="physcialDataTableName">数据库表的物理名称</param>
        /// <param name="physcialDataFieldName">字段名称</param>
        /// <returns>字段在表中是否存在 </returns>
        public bool IsExistPhyscialDataField(SqlDatabase db, string physcialDataTableName, string physcialDataFieldName)
        {
            bool exist = false;

            //查询语句
            string sqlSelect = "SELECT COUNT(Name) FROM syscolumns WHERE id =object_id(@PhyscialDataTableName) AND Name = @PhyscialDataFieldName";
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "PhyscialDataTableName", DbType.String, physcialDataTableName);
                    db.AddInParameter(dbCommand, "PhyscialDataFieldName", DbType.String, physcialDataFieldName);
                    int count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand), 0);
                    if (count > 0)
                    {
                        exist = true;
                    }
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
        /// 生成物理字段名称
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        public string GenerateDataFieldPhysicalName(decimal tableId, int sorting)
        {
            return string.Format("df_{0}_{1}", tableId, sorting);
        }
        
        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
		/// 获得 CustomDataFieldInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomDataFieldInfo 对象列表</returns>
		private IList<CustomDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomDataFieldInfo> customDataFieldInfos = new List<CustomDataFieldInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomDataField");
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
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal enumId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal parentDataFieldId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal associatedDataFieldId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[5]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[6]);
                            string dataFieldCode = DataConvertionHelper.GetString(dataReader[7]);
                            byte dataFieldProperty = DataConvertionHelper.GetByte(dataReader[8]);
                            byte dataFieldType = DataConvertionHelper.GetByte(dataReader[9]);
                            int dataFieldLength = DataConvertionHelper.GetInt(dataReader[10]);
                            byte basedDataType = DataConvertionHelper.GetByte(dataReader[11]);
                            string regexExpression = DataConvertionHelper.GetString(dataReader[12]);
                            string expressionText = DataConvertionHelper.GetString(dataReader[13]);
                            long dataFieldSetting = DataConvertionHelper.GetLong(dataReader[14]);
                            bool requiredDataField = DataConvertionHelper.GetBoolean(dataReader[15]);
                            bool autoComplete = DataConvertionHelper.GetBoolean(dataReader[16]);
                            bool indexCreated = DataConvertionHelper.GetBoolean(dataReader[17]);
                            bool helpEnabled = DataConvertionHelper.GetBoolean(dataReader[18]);
                            string helpContent = DataConvertionHelper.GetString(dataReader[19]);
                            string tooltip = DataConvertionHelper.GetString(dataReader[20]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[21]);
                            string notes = DataConvertionHelper.GetString(dataReader[22]);
                            //将创建 CustomDataFieldInfo 对象加入集合中
                            customDataFieldInfos.Add(new CustomDataFieldInfo(dataFieldId, enumId, parentDataFieldId, associatedDataFieldId, tableId,
                            logicalName, physicalName, dataFieldCode, dataFieldProperty, dataFieldType,
                            dataFieldLength, basedDataType, regexExpression, expressionText, dataFieldSetting, requiredDataField,
                            autoComplete, indexCreated, helpEnabled, helpContent, tooltip,
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

            return customDataFieldInfos;
        }

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDataFieldInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomDataField");
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
        /// 获得表 CustomDataField 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomDataField ", "DataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 CustomDataField 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomDataField ", "DataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomDataField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomDataField ", "DataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 CustomDataField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomDataField ", "DataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomDataFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomDataField");
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
        /// 更新字段的父编号
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="parentDataFieldId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void UpdateParentDataFieldId(decimal dataFieldId, decimal parentDataFieldId, SqlDatabase db, DbTransaction transaction)
        {
            string sqlUpdate = "UPDATE CustomDataField SET ParentDataFieldId = @ParentDataFieldId WHERE DataFieldId = @DataFieldId";
            try
            {               
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);                   
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(parentDataFieldId));
                    //执行更新操作
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
        /// 创建字段
        /// </summary>
        /// <param name="dataWarehouseId">数据仓库编号</param>
        /// <param name="physcialDataTableName">表的物理名称</param>
        /// <param name="dataFieldPhysicalName">字段的物理名称</param>
        /// <param name="physicalDataFieldType">字段的类型</param>
        /// <param name="dataFieldLength">对于部分字段类型的字段的长度</param>
        private void CreateDataFieldById(int dataWarehouseId, string physcialDataTableName, string dataFieldPhysicalName, PhysicalDataFieldType physicalDataFieldType, int dataFieldLength)
        {            
            try
            {
                /* 创建字段 */
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                CreateDataField(db, physcialDataTableName, dataFieldPhysicalName, physicalDataFieldType, dataFieldLength);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 创建字段
        /// </summary>
        /// <param name="db">数据仓库</param>
        /// <param name="physcialDataTableName">表的物理名称</param>
        /// <param name="dataFieldPhysicalName">字段的物理名称</param>
        /// <param name="physicalDataFieldType">字段的类型</param>
        /// <param name="dataFieldLength">对于部分字段类型的字段的长度</param>
        private void CreateDataField(SqlDatabase db, string physcialDataTableName, string dataFieldPhysicalName, PhysicalDataFieldType physicalDataFieldType, int dataFieldLength)
        {
            //创建语句
            StringBuilder sb = new StringBuilder();
            sb.Append("ALTER TABLE ");
            sb.Append(physcialDataTableName);
            sb.Append(" ADD ");
            sb.Append(dataFieldPhysicalName);
            sb.Append(DataFieldHelper.GetDataTypeString(physicalDataFieldType, dataFieldLength));
            sb.Append(" NULL");
                        
            try
            {
                /* 创建业务日志表字段 */
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                DataAccessHandler.DeleteDataField(dbBusiness, physcialDataTableName, dataFieldPhysicalName);                
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sb.ToString()))
                {
                    dbBusiness.ExecuteNonQuery(dbCommand);
                }

                /* 创建业务字段 */
                DataAccessHandler.DeleteDataField(db, physcialDataTableName, dataFieldPhysicalName);
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
        /// 删除字段
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="physcialDataTableName"></param>
        /// <param name="dataFieldPhysicalName"></param>
        private void DeleteDataField(int dataWarehouseId, string physcialDataTableName, string dataFieldPhysicalName)
        {
            try
            {
                /* 删除业务字段 */
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                DataAccessHandler.DeleteDataField(db, physcialDataTableName, dataFieldPhysicalName);

                /* 删除业务日志字段 */
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                DataAccessHandler.DeleteDataField(dbBusiness, physcialDataTableName, dataFieldPhysicalName);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="physcialDataTableName"></param>
        /// <param name="dataFieldPhysicalName"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="dataFieldLength"></param>
        private void UpdateDataField(int dataWarehouseId, string physcialDataTableName, string dataFieldPhysicalName, PhysicalDataFieldType physicalDataFieldType, int dataFieldLength)
        {
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                StringBuilder sb = new StringBuilder();
                sb.Append("ALTER TABLE ");
                sb.Append(physcialDataTableName);
                sb.Append(" ALTER COLUMN ");
                sb.Append(dataFieldPhysicalName);
                sb.Append(DataFieldHelper.GetDataTypeString(physicalDataFieldType, dataFieldLength));
                sb.Append(" NULL");

                /* 更新业务日志字段 */
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sb.ToString()))
                {
                    dbBusiness.ExecuteNonQuery(dbCommand);
                }

                /* 更新业务字段 */
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
        /// 向 CustomDataField 表中插入一条新记录
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
		private decimal InsertCustomDataFieldInfo(CustomDataFieldInfo customDataFieldInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal customDataFieldId = 0;

            customDataFieldInfo.PhysicalName = GenerateDataFieldPhysicalName(customDataFieldInfo.TableId, customDataFieldInfo.Sorting);
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomDataField(AssociatedDataFieldId, TableId, ParentDataFieldId, EnumId, LogicalName, ");
            sb.Append("PhysicalName, DataFieldCode, DataFieldProperty, DataFieldType, DataFieldLength, BasedDataType, ");
            sb.Append("RegexExpression, ExpressionText, DataFieldSetting, RequiredDataField, AutoComplete, ");
            sb.Append("IndexCreated, HelpEnabled, HelpContent, Tooltip, Sorting, Notes)");
            sb.Append("VALUES (@AssociatedDataFieldId, @TableId, @ParentDataFieldId, @EnumId, @LogicalName, ");
            sb.Append("@PhysicalName, @DataFieldCode, @DataFieldProperty, @DataFieldType, @DataFieldLength, @BasedDataType, ");
            sb.Append("@RegexExpression, @ExpressionText, @DataFieldSetting, @RequiredDataField, @AutoComplete, ");
            sb.Append("@IndexCreated, @HelpEnabled, @HelpContent, @Tooltip, @Sorting, @Notes);");
            sb.Append("SET @DataFieldId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "DataFieldId", DbType.Decimal, 8);
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataFieldInfo.AssociatedDataFieldId));
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, customDataFieldInfo.TableId);
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataFieldInfo.ParentDataFieldId));
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataFieldInfo.EnumId));
                    db.AddInParameter(dbCommand, "LogicalName", DbType.String, customDataFieldInfo.LogicalName);
                    db.AddInParameter(dbCommand, "PhysicalName", DbType.String, customDataFieldInfo.PhysicalName);
                    db.AddInParameter(dbCommand, "DataFieldCode", DbType.String, customDataFieldInfo.DataFieldCode);
                    db.AddInParameter(dbCommand, "DataFieldProperty", DbType.Byte, customDataFieldInfo.DataFieldProperty);
                    db.AddInParameter(dbCommand, "DataFieldType", DbType.Byte, customDataFieldInfo.DataFieldType);
                    db.AddInParameter(dbCommand, "DataFieldLength", DbType.Int32, DataConvertionHelper.SetInt(customDataFieldInfo.DataFieldLength));
                    db.AddInParameter(dbCommand, "BasedDataType", DbType.Byte, customDataFieldInfo.BasedDataType);
                    db.AddInParameter(dbCommand, "RegexExpression", DbType.String, customDataFieldInfo.RegexExpression);
                    db.AddInParameter(dbCommand, "ExpressionText", DbType.String, customDataFieldInfo.ExpressionText);
                    db.AddInParameter(dbCommand, "DataFieldSetting", DbType.Int64, DataConvertionHelper.SetLong(customDataFieldInfo.DataFieldSetting));
                    db.AddInParameter(dbCommand, "RequiredDataField", DbType.Boolean, customDataFieldInfo.RequiredDataField);
                    db.AddInParameter(dbCommand, "AutoComplete", DbType.Boolean, customDataFieldInfo.AutoComplete);
                    db.AddInParameter(dbCommand, "IndexCreated", DbType.Boolean, customDataFieldInfo.IndexCreated);
                    db.AddInParameter(dbCommand, "HelpEnabled", DbType.Boolean, customDataFieldInfo.HelpEnabled);
                    db.AddInParameter(dbCommand, "HelpContent", DbType.String, customDataFieldInfo.HelpContent);
                    db.AddInParameter(dbCommand, "Tooltip", DbType.String, customDataFieldInfo.Tooltip);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customDataFieldInfo.Sorting);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customDataFieldInfo.Notes);

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
                    customDataFieldId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@DataFieldId"].Value, 0);

                    if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        CustomTable customTable = new CustomTable();
                        string tablePhysicalName = customTable.GetTablePhysicalName(customDataFieldInfo.TableId);
                        int dataWarehouseId = customTable.GetDataWarehouseId(customDataFieldInfo.TableId);
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                        if (physicalDataFieldType == PhysicalDataFieldType.Association ||
                            physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation ||
                            physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                        {
                            AssociatedDataField associatedDataField = new AssociatedDataField();
                            BasedDataType basedDataType = associatedDataField.GetBasedDataType(customDataFieldInfo.AssociatedDataFieldId);
                            physicalDataFieldType = DataFieldHelper.GetPhysicalDataFieldType(basedDataType);
                            customDataFieldInfo.DataFieldLength = associatedDataField.GetDataLength(customDataFieldInfo.AssociatedDataFieldId);
                        }
                        else if (physicalDataFieldType == PhysicalDataFieldType.ScdAdditionalDecimal ||
                            physicalDataFieldType == PhysicalDataFieldType.FstAdditionalDecimal)
                        {
                            customDataFieldInfo.DataFieldLength = 2;
                        }
                        CreateDataFieldById(dataWarehouseId, tablePhysicalName, customDataFieldInfo.PhysicalName, physicalDataFieldType, customDataFieldInfo.DataFieldLength);
                    }
                }
            }
            catch (Exception exception)
            {

                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldId;
        }

        /// <summary>
        /// 更新 CustomDataFieldInfo 对象， 不允许修改属性
        /// </summary>
        /// <param name="customDataFieldInfo">CustomDataFieldInfo 对象</param>
        /// <param name="transaction">事务</param>
        public void UpdateCustomDataFieldInfo(CustomDataFieldInfo customDataFieldInfo, DbTransaction transaction)
        {

            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomDataField SET AssociatedDataFieldId = @AssociatedDataFieldId, TableId = @TableId, ParentDataFieldId = @ParentDataFieldId, ");
            sb.Append("EnumId = @EnumId, LogicalName = @LogicalName, DataFieldCode = @DataFieldCode, DataFieldProperty = @DataFieldProperty, DataFieldType = @DataFieldType, ");
            sb.Append("DataFieldLength = @DataFieldLength, BasedDataType = @BasedDataType, RegexExpression = @RegexExpression, ExpressionText = @ExpressionText, ");
            sb.Append("DataFieldSetting = @DataFieldSetting, RequiredDataField = @RequiredDataField, AutoComplete = @AutoComplete, ");
            sb.Append("IndexCreated = @IndexCreated, HelpEnabled = @HelpEnabled, HelpContent = @HelpContent, Tooltip = @Tooltip, Notes = @Notes WHERE DataFieldId = @DataFieldId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                CustomDataFieldInfo oldCustomDataFieldInfo = GetModelInfo(customDataFieldInfo.DataFieldId);
                /* 字段属性相同时，字段类型的调整；不允许修改属性*/
                if (oldCustomDataFieldInfo.DataFieldProperty != customDataFieldInfo.DataFieldProperty)
                {
                    throw new ArgumentOutOfRangeException("字段属性不允许修改");
                }
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                {
                    CustomTable customTable = new CustomTable();
                    string tablePhysicalName = customTable.GetTablePhysicalName(customDataFieldInfo.TableId);
                    int dataWarehouseId = customTable.GetDataWarehouseId(customDataFieldInfo.TableId);
                    customDataFieldInfo.PhysicalName = GetPhysicalName(customDataFieldInfo.DataFieldId);
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                    if (oldCustomDataFieldInfo.DataFieldType != customDataFieldInfo.DataFieldType)
                    {
                        if (physicalDataFieldType == PhysicalDataFieldType.Association
                            || physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation
                            || physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                        {
                            AssociatedDataField associatedDataField = new AssociatedDataField();
                            BasedDataType basedDataType = associatedDataField.GetBasedDataType(customDataFieldInfo.AssociatedDataFieldId);
                            physicalDataFieldType = DataFieldHelper.GetPhysicalDataFieldType(basedDataType);
                            customDataFieldInfo.DataFieldLength = associatedDataField.GetDataLength(customDataFieldInfo.AssociatedDataFieldId);
                        }
                        UpdateDataField(dataWarehouseId, tablePhysicalName, customDataFieldInfo.PhysicalName, physicalDataFieldType, customDataFieldInfo.DataFieldLength);
                    }
                    else
                    {
                        if (((physicalDataFieldType == PhysicalDataFieldType.Decimal) || (physicalDataFieldType == PhysicalDataFieldType.ArbitraryString)
                            || (physicalDataFieldType == PhysicalDataFieldType.ExtendedArbitraryString) || (physicalDataFieldType == PhysicalDataFieldType.NumeralString)
                            || (physicalDataFieldType == PhysicalDataFieldType.CharString) || (physicalDataFieldType == PhysicalDataFieldType.MixedString))
                            && (oldCustomDataFieldInfo.DataFieldLength != customDataFieldInfo.DataFieldLength))
                        {
                            UpdateDataField(dataWarehouseId, tablePhysicalName, customDataFieldInfo.PhysicalName, physicalDataFieldType, customDataFieldInfo.DataFieldLength);
                        }
                    }
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, customDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataFieldInfo.AssociatedDataFieldId));
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, customDataFieldInfo.TableId);
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataFieldInfo.ParentDataFieldId));
                    db.AddInParameter(dbCommand, "EnumId", DbType.Decimal, DataConvertionHelper.SetDecimal(customDataFieldInfo.EnumId));
                    db.AddInParameter(dbCommand, "LogicalName", DbType.String, customDataFieldInfo.LogicalName);
                    db.AddInParameter(dbCommand, "DataFieldCode", DbType.String, customDataFieldInfo.DataFieldCode);
                    db.AddInParameter(dbCommand, "DataFieldProperty", DbType.Byte, customDataFieldInfo.DataFieldProperty);
                    db.AddInParameter(dbCommand, "DataFieldType", DbType.Byte, customDataFieldInfo.DataFieldType);                    
                    db.AddInParameter(dbCommand, "DataFieldLength", DbType.Int32, DataConvertionHelper.SetInt(customDataFieldInfo.DataFieldLength));
                    db.AddInParameter(dbCommand, "BasedDataType", DbType.Byte, customDataFieldInfo.BasedDataType);
                    db.AddInParameter(dbCommand, "RegexExpression", DbType.String, customDataFieldInfo.RegexExpression);
                    db.AddInParameter(dbCommand, "ExpressionText", DbType.String, customDataFieldInfo.ExpressionText);
                    db.AddInParameter(dbCommand, "DataFieldSetting", DbType.Int64, DataConvertionHelper.SetLong(customDataFieldInfo.DataFieldSetting));
                    db.AddInParameter(dbCommand, "RequiredDataField", DbType.Boolean, customDataFieldInfo.RequiredDataField);
                    db.AddInParameter(dbCommand, "AutoComplete", DbType.Boolean, customDataFieldInfo.AutoComplete);
                    db.AddInParameter(dbCommand, "IndexCreated", DbType.Boolean, customDataFieldInfo.IndexCreated);
                    db.AddInParameter(dbCommand, "HelpEnabled", DbType.Boolean, customDataFieldInfo.HelpEnabled);
                    db.AddInParameter(dbCommand, "HelpContent", DbType.String, customDataFieldInfo.HelpContent);
                    db.AddInParameter(dbCommand, "Tooltip", DbType.String, customDataFieldInfo.Tooltip);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customDataFieldInfo.Notes);

                    //执行更新操作
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
        /// 获得新的字段物理名称
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="sorting"></param>
        /// <returns></returns>
        private string GetDataFieldPhysicalName(decimal tableId, int sorting)
        {
            string dataFieldPhysicalName = string.Empty;

            CustomTable customTable = new CustomTable();
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
            byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
            SqlDatabase db = DataWarehouseHelper.GetDatabaseByDataWarehouseId(dataWarehouseId);
            do
            {
                dataFieldPhysicalName = GenerateDataFieldPhysicalName(tableId, sorting);
                sorting++;
            } while (IsExistPhyscialDataField(db, tablePhysicalName, dataFieldPhysicalName));

            return dataFieldPhysicalName;
        }
        
        #endregion

        #endregion
    }
}
