//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：AssociatedDataField.cs
// 描述：AssociatedDataField 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/10/3
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Core;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// AssociatedDataField 表的数据层访问类
    /// </summary>
    public class AssociatedDataField : CommonNodeDataAccess, IAssociatedDataField
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public AssociatedDataField() : base("AssociatedDataField", "AssociatedDataFieldId", "AssociationId", "LogicalName", "DataFieldCode", false, true, "DataFieldCategory")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 AssociatedDataField 表中插入一条新记录
        /// </summary>
        /// <param name="associatedDataFieldInfo">associatedDataFieldInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(AssociatedDataFieldInfo associatedDataFieldInfo)
        {
            //自动增加的关键字的值
            decimal associatedDataFieldId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            associatedDataFieldInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "AssociatedDataField", "Sorting", "AssociationId", associatedDataFieldInfo.AssociationId, 0) + 1;
            associatedDataFieldInfo.PhysicalName = string.Format("ac_{0}_{1}", associatedDataFieldInfo.AssociationId, associatedDataFieldInfo.Sorting);
            CustomAssociation customAssociation = new CustomAssociation();
            string tableName = customAssociation.GetPhysicalName(associatedDataFieldInfo.AssociationId);

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO AssociatedDataField(AssociationId, LogicalName, PhysicalName, DataFieldCode, BasedDataType, ");
            sb.Append("DataLength, DataFieldCategory, IsHierarchal, Sorting, Notes)");
            sb.Append("VALUES (@AssociationId, @LogicalName, @PhysicalName, @DataFieldCode, @BasedDataType, ");
            sb.Append("@DataLength, @DataFieldCategory, @IsHierarchal, @Sorting, @Notes);");
            sb.Append("SET @AssociatedDataFieldId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, 8);
                        db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, associatedDataFieldInfo.AssociationId);
                        db.AddInParameter(dbCommand, "LogicalName", DbType.String, associatedDataFieldInfo.LogicalName);
                        db.AddInParameter(dbCommand, "PhysicalName", DbType.String, associatedDataFieldInfo.PhysicalName);
                        db.AddInParameter(dbCommand, "DataFieldCode", DbType.String, associatedDataFieldInfo.DataFieldCode);
                        db.AddInParameter(dbCommand, "BasedDataType", DbType.Byte, associatedDataFieldInfo.BasedDataType);
                        db.AddInParameter(dbCommand, "DataLength", DbType.Int32, associatedDataFieldInfo.DataLength);
                        db.AddInParameter(dbCommand, "DataFieldCategory", DbType.Byte, associatedDataFieldInfo.DataFieldCategory);
                        db.AddInParameter(dbCommand, "IsHierarchal", DbType.Boolean, associatedDataFieldInfo.IsHierarchal);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, associatedDataFieldInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, associatedDataFieldInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        associatedDataFieldId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@AssociatedDataFieldId"].Value, 0);
                        customAssociation.UpdateLeaf(associatedDataFieldInfo.AssociationId, false, db, transaction);                        
                        CreatePhysicalDataField(tableName, associatedDataFieldInfo.PhysicalName, (BasedDataType)associatedDataFieldInfo.BasedDataType, associatedDataFieldInfo.DataLength);
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

            return associatedDataFieldId;
        }
        
        /// <summary>
		/// 获得 AssociatedDataFieldInfo 对象
		/// </summary>
		///<param name="associatedDataFieldId">关联字段编号</param>
		/// <returns> AssociatedDataFieldInfo 对象</returns>
		public AssociatedDataFieldInfo GetModelInfo(decimal associatedDataFieldId)
        {
            AssociatedDataFieldInfo associatedDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("AssociatedDataFieldId", "AssociatedDataFieldId", System.Data.DbType.Decimal, associatedDataFieldId, DataFieldCondition.Equal));

            //创建集合对象
            IList<AssociatedDataFieldInfo> associatedDataFieldInfos = GetModelInfos(whereConditons, null, true);
            if (associatedDataFieldInfos != null && associatedDataFieldInfos.Count > 0)
            {
                associatedDataFieldInfo = associatedDataFieldInfos[0];
            }

            return associatedDataFieldInfo;
        }

        /// <summary>
        /// 获得 AssociatedDataFieldInfo 对象
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public AssociatedDataFieldInfo GetKeyAssociatedDataFieldInfo(decimal associationId)
        {
            AssociatedDataFieldInfo associatedDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("AssociationId", "AssociationId", DbType.Decimal, associationId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("DataFieldCategory", "DataFieldCategory", DbType.Byte, (byte)AssociatedDataFieldCategory.AssociatedDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                        
            //创建集合对象
            IList<AssociatedDataFieldInfo> associatedDataFieldInfos = GetModelInfos(whereConditons, null, true);
            if (associatedDataFieldInfos != null && associatedDataFieldInfos.Count > 0)
            {
                associatedDataFieldInfo = associatedDataFieldInfos[0];
            }

            return associatedDataFieldInfo;
        }

        /// <summary>
        /// 更新 AssociatedDataFieldInfo 对象
        /// </summary>
        /// <param name="associatedDataFieldInfo">AssociatedDataFieldInfo 对象</param>
        public void Update(AssociatedDataFieldInfo associatedDataFieldInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE AssociatedDataField SET AssociationId = @AssociationId, LogicalName = @LogicalName, ");
            sb.Append("DataFieldCode = @DataFieldCode, BasedDataType = @BasedDataType, DataLength = @DataLength, ");
            sb.Append("DataFieldCategory = @DataFieldCategory, IsHierarchal = @IsHierarchal, Notes = @Notes ");
            sb.Append("WHERE AssociatedDataFieldId = @AssociatedDataFieldId");

            AssociatedDataFieldInfo oldAssociatedDataFieldInfo = GetModelInfo(associatedDataFieldInfo.AssociatedDataFieldId);            
            if (oldAssociatedDataFieldInfo.BasedDataType != associatedDataFieldInfo.BasedDataType)
            {
                CustomAssociation customAssociation = new CustomAssociation();
                string physcialDataTableName = customAssociation.GetPhysicalName(associatedDataFieldInfo.AssociationId);
                UpdateDataField(physcialDataTableName, oldAssociatedDataFieldInfo.PhysicalName,
                    (BasedDataType)associatedDataFieldInfo.BasedDataType, associatedDataFieldInfo.DataLength);
            }

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, associatedDataFieldInfo.AssociatedDataFieldId);
                    db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, associatedDataFieldInfo.AssociationId);
                    db.AddInParameter(dbCommand, "LogicalName", DbType.String, associatedDataFieldInfo.LogicalName);
                    db.AddInParameter(dbCommand, "DataFieldCode", DbType.String, associatedDataFieldInfo.DataFieldCode);
                    db.AddInParameter(dbCommand, "BasedDataType", DbType.Byte, associatedDataFieldInfo.BasedDataType);
                    db.AddInParameter(dbCommand, "DataLength", DbType.Int32, associatedDataFieldInfo.DataLength);
                    db.AddInParameter(dbCommand, "DataFieldCategory", DbType.Byte, associatedDataFieldInfo.DataFieldCategory);
                    db.AddInParameter(dbCommand, "IsHierarchal", DbType.Boolean, associatedDataFieldInfo.IsHierarchal);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, associatedDataFieldInfo.Notes);
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
        ///  删除 AssociatedDataFieldInfo 对象
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        public void Delete(decimal associatedDataFieldId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM AssociatedDataField ");
            sb.Append("WHERE AssociatedDataFieldId = @AssociatedDataFieldId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string physcialDataTableName = GetTablePhysicalName(associatedDataFieldId);
                string dataFieldPhysicalName = GetPhysicalName(associatedDataFieldId);
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
                DataAccessHandler.DeleteDataField(dbBusiness, physcialDataTableName, dataFieldPhysicalName);
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, associatedDataFieldId);
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
        /// 获得 AssociatedDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AssociatedDataFieldInfo 对象列表</returns>
        public IList<AssociatedDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 AssociatedDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>AssociatedDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "AssociatedDataField ", "AssociatedDataFieldId", false, whereConditons);
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
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetLogicalName(decimal associatedDataFieldId)
        {
            string logicalName = string.Empty;

            try
            {
                string sqlSelect = "SELECT LogicalName FROM CustomDataField WHERE AssociatedDataFieldId = @AssociatedDataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(associatedDataFieldId));
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
        /// 获得表的物理字段
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetTablePhysicalName(decimal associatedDataFieldId)
        {
            string physicalName = string.Empty;

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT CustomAssociation.PhysicalName FROM CustomAssociation INNER JOIN AssociatedDataField ");
                sb.Append("ON CustomAssociation.AssociationId = AssociatedDataField.AssociationId ");
                sb.Append("WHERE AssociatedDataFieldId = @AssociatedDataFieldId ");
                
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(associatedDataFieldId));
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

        /// <summary>
        /// 获得字段的物理字段
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetPhysicalName(decimal associatedDataFieldId)
        {
            string physicalName = string.Empty;

            try
            {
                string sqlSelect = "SELECT PhysicalName FROM AssociatedDataField WHERE AssociatedDataFieldId = @AssociatedDataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(associatedDataFieldId));
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

        /// <summary>
        /// 通过关联字段的类型获得关联表中关联字段个数
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="DataFieldCategory"></param>
        /// <returns></returns>
        public int GetAssociatedDataFieldCount(decimal associationId, AssociatedDataFieldCategory dataFieldCategory)
        {
            int count = 0;

            try
            {
                string sqlSelect = "SELECT COUNT(1) FROM AssociatedDataField WHERE AssociationId =  @AssociationId AND DataFieldCategory = @DataFieldCategory";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, associationId);
                    db.AddInParameter(dbCommand, "DataFieldCategory", DbType.Byte, (byte)dataFieldCategory);
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
        /// 获得字段的长度
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的长度</returns>
        public int GetDataLength(decimal associatedDataFieldId)
        {
            int dataLength = 0;

            try
            {
                string sqlSelect = "SELECT DataLength FROM AssociatedDataField WHERE AssociatedDataFieldId = @AssociatedDataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(associatedDataFieldId));
                    dataLength = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataLength;
        }
       
        /// <summary>
        /// 关联表的字段名称关系
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public Dictionary<string, string> GetDataFieldNameRelation(decimal dataFieldId)
        {
            Dictionary<string, string> dataFieldNameRelation = new Dictionary<string, string>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT CustomDataField.PhysicalName, AssociatedDataField.PhysicalName FROM CustomDataField ");
                sb.Append("INNER JOIN AssociatedDataField ON CustomDataField.AssociatedDataFieldId = AssociatedDataField.AssociatedDataFieldId ");
                sb.Append("WHERE DataFieldId = @DataFieldId OR(ParentDataFieldId = @DataFieldId AND DataFieldProperty = @DataFieldProperty AND DataFieldType = @DataFieldType)");

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
                    db.AddInParameter(dbCommand, "DataFieldProperty", DbType.Byte, (byte)DataFieldProperty.PhysicalDataField);
                    db.AddInParameter(dbCommand, "DataFieldType", DbType.Byte, (byte)PhysicalDataFieldType.SecondaryAssociation);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            string customDataFieldPhysicalName = DataConvertionHelper.GetString(dataReader[0]);
                            string associatedDataFieldPhysicalName = DataConvertionHelper.GetString(dataReader[1]);
                            dataFieldNameRelation.Add(customDataFieldPhysicalName, associatedDataFieldPhysicalName);
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


            return dataFieldNameRelation;
        }
        
        /// <summary>
        /// 通过关联字段获得关联表编号
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public BasedDataType GetBasedDataType(decimal associatedDataFieldId)
        {
            BasedDataType basedDataType = BasedDataType.String;

            try
            {
                string sqlSelect = "SELECT BasedDataType FROM AssociatedDataField WHERE AssociatedDataFieldId = @AssociatedDataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(associatedDataFieldId));               
                    basedDataType = (BasedDataType)DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return basedDataType;
        }        

        /// <summary>
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            CommonNode commonNode = GetCommonNode(nodeId);
            if (commonNode != null)
            {
                CustomAssociation customAssociation = new CustomAssociation();
                IList<string> parentNames = customAssociation.GetHierarchicalNamesOfNode(commonNode.ParentNodeId);
                foreach (string parentName in parentNames)
                {
                    names.Add(parentName);
                }
                names.Add(commonNode.NodeName);
            }            

            return names;
        }

        /// <summary>
        /// 通过关联字段获得关联表编号
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public decimal GetAssociationId(decimal associatedDataFieldId)
        {
            decimal associationId = 0;

            try
            {
                string sqlSelect = "SELECT AssociationId FROM AssociatedDataField WHERE AssociatedDataFieldId = @AssociatedDataFieldId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "AssociatedDataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(associatedDataFieldId));
                    associationId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return associationId;
        }

        /// <summary>
        /// 获得字段列表
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public IList<AssociatedDataFieldInfo> GetModelInfos(decimal associationId)
        {
            //创建集合对象
            IList<AssociatedDataFieldInfo> associatedDataFieldInfos = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("AssociationId", "AssociationId", System.Data.DbType.Decimal, associationId, DataFieldCondition.Equal));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));
            associatedDataFieldInfos = GetModelInfos(whereConditons, sortingCondtions, false);

            return associatedDataFieldInfos;
        }

        #endregion

        #endregion

        #region 公有方法        

        /// <summary>
        /// 获得字段的属性信息
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public List<BasedDataFieldInfo> GetDataFieldProperties(decimal associationId)
        {
            List<BasedDataFieldInfo> basedDataFieldProperties = new List<BasedDataFieldInfo>();

            string sqlSelect = "SELECT AssociatedDataFieldId, LogicalName, PhysicalName, BasedDataType, DataFieldCategory FROM AssociatedDataField WHERE AssociationId = @AssociationId ORDER BY Sorting";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, associationId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal associatedDataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[1]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[2]);
                            byte basedDataType = DataConvertionHelper.GetByte(dataReader[3]);   
                            byte dataFieldCategory = DataConvertionHelper.GetByte(dataReader[4]);
                            basedDataFieldProperties.Add(new BasedDataFieldInfo(associatedDataFieldId, logicalName, physicalName, (BasedDataType)basedDataType, dataFieldCategory));
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return basedDataFieldProperties;
        }

        /// <summary>
        /// 获得物理字段与逻辑字段名称
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetNames(decimal associationId)
        {
            Dictionary<string, string> names = new Dictionary<string, string>();

            string sqlSelect = "SELECT PhysicalName, LogicalName FROM AssociatedDataField WHERE AssociationId = @AssociationId ORDER BY Sorting";

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, associationId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            string physicalName = DataConvertionHelper.GetString(dataReader[0]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[1]);
                            names.Add(physicalName, logicalName);
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
        ///  删除所有的 AssociatedDataFieldInfo 对象(物理表由调用者删除)
        /// </summary>
        ///<param name="associationId">关联字段编号</param>
        public void Delete(decimal associationId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM AssociatedDataField ");
            sb.Append("WHERE AssociationId = @AssociationId");
            
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "AssociationId", DbType.Decimal, associationId);
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
        /// 获得 AssociatedDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>AssociatedDataFieldInfo 对象列表</returns>
        private IList<AssociatedDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<AssociatedDataFieldInfo> associatedDataFieldInfos = new List<AssociatedDataFieldInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM AssociatedDataField");
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
                            decimal associatedDataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal associationId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[2]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[3]);
                            string dataFieldCode = DataConvertionHelper.GetString(dataReader[4]);
                            byte basedDataType = DataConvertionHelper.GetByte(dataReader[5]);
                            int dataLength = DataConvertionHelper.GetInt(dataReader[6]);
                            byte dataFieldCategory = DataConvertionHelper.GetByte(dataReader[7]);
                            bool isHierarchal = DataConvertionHelper.GetBoolean(dataReader[8]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[9]);
                            string notes = DataConvertionHelper.GetString(dataReader[10]);
                            //将创建 AssociatedDataFieldInfo 对象加入集合中
                            associatedDataFieldInfos.Add(new AssociatedDataFieldInfo(associatedDataFieldId, associationId, logicalName, physicalName, dataFieldCode,
                            basedDataType, dataLength, dataFieldCategory, isHierarchal, sorting, notes));
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

            return associatedDataFieldInfos;
        }

        /// <summary>
        /// 获得 AssociatedDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AssociatedDataFieldInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM AssociatedDataField");
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
        /// 获得表 AssociatedDataField 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "AssociatedDataField ", "AssociatedDataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 AssociatedDataField 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "AssociatedDataField ", "AssociatedDataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 AssociatedDataField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "AssociatedDataField ", "AssociatedDataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 AssociatedDataField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "AssociatedDataField ", "AssociatedDataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  AssociatedDataFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM AssociatedDataField");
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
        /// 创建物理字段
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="physicalDataFeildName"></param>
        /// <param name="dataFieldBase"></param>
        /// <param name="dataFieldLength"></param>
        private void CreatePhysicalDataField(string tableName, string physicalDataFeildName, BasedDataType dataFieldBase, int dataFieldLength)
        {
            
            //创建语句
            StringBuilder sb = new StringBuilder();
            sb.Append("ALTER TABLE ");
            sb.Append(tableName);
            sb.Append(" ADD ");
            sb.Append(physicalDataFeildName);
            sb.Append(DataFieldHelper.GetDataTypeString(dataFieldBase, dataFieldLength));
            sb.Append(" NULL");

            //获得业务数据库对象
            SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);
            try
            {
                DataAccessHandler.DeleteDataField(dbBusiness, tableName, physicalDataFeildName);
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

        /// <summary>
        /// 更新字段
        /// </summary>
        /// <param name="physcialDataTableName"></param>
        /// <param name="dataFieldPhysicalName"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="dataFieldLength"></param>
        private void UpdateDataField(string physcialDataTableName, string dataFieldPhysicalName, BasedDataType basedDataType, int dataFieldLength)
        {
            try
            {
                //获得业务数据库对象
                SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.BusinessDatabaseName);

                StringBuilder sb = new StringBuilder();
                sb.Append("ALTER TABLE ");
                sb.Append(physcialDataTableName);
                sb.Append(" ALTER COLUMN ");
                sb.Append(dataFieldPhysicalName);
                sb.Append(DataFieldHelper.GetDataTypeString(basedDataType, dataFieldLength));
                sb.Append(" NULL");
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
