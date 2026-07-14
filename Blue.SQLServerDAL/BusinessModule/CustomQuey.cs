
//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomQuey.cs
// 描述：CustomQuey 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/10/31
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
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataAccessLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomQuey 表的数据层访问类
    /// </summary>
    public class CustomQuey : CommonNodeDataAccess, ICustomQuey
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomQuey() : base("CustomQuey", "DataQueriedId", "GroupId", "DataQueriedName", "DataQueriedCode", false, true, "DataQueriedType")
        {
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 CustomQuey 表中插入一条新记录
		/// </summary>
		/// <param name="customQueyInfo">customQueyInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomQueyInfo customQueyInfo)
		{
			//自动增加的关键字的值
			decimal customQueyId= 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customQueyInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomQuey", "Sorting", "GroupId", customQueyInfo.GroupId, 0) + 1;

            DataQueriedType dataQueriedType = (DataQueriedType)customQueyInfo.DataQueriedType;
            if (dataQueriedType == DataQueriedType.Custom)
            {
                customQueyInfo.CustomViewName = string.Format("query_{0}_{1}", customQueyInfo.GroupId, customQueyInfo.Sorting);
            }

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomQuey(GroupId, TableId, ViewId, DataQueriedName, DataQueriedCode, ");
            sb.Append("DataWarehouseId, CustomViewName, Conditions, DataQueriedType, SystemDataFields, ");
            sb.Append("SystemCondition, GroupCondition, ShowMode, DataRange, ToolTip, Sorting, Notes)");
            sb.Append("VALUES (@GroupId, @TableId, @ViewId, @DataQueriedName, @DataQueriedCode, ");
            sb.Append("@DataWarehouseId, @CustomViewName, @Conditions, @DataQueriedType, @SystemDataFields, ");
            sb.Append("@SystemCondition, @GroupCondition, @ShowMode, @DataRange, @ToolTip, @Sorting, @Notes);");
            sb.Append("SET @DataQueriedId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "DataQueriedId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customQueyInfo.GroupId);
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customQueyInfo.TableId));
                        db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(customQueyInfo.ViewId));
                        db.AddInParameter(dbCommand, "DataQueriedName", DbType.String, customQueyInfo.DataQueriedName);
                        db.AddInParameter(dbCommand, "DataQueriedCode", DbType.String, customQueyInfo.DataQueriedCode);
                        db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, customQueyInfo.DataWarehouseId);
                        db.AddInParameter(dbCommand, "CustomViewName", DbType.String, customQueyInfo.CustomViewName);
                        db.AddInParameter(dbCommand, "Conditions", DbType.String, customQueyInfo.Conditions);
                        db.AddInParameter(dbCommand, "DataQueriedType", DbType.Byte, customQueyInfo.DataQueriedType);
                        db.AddInParameter(dbCommand, "SystemDataFields", DbType.Int64, customQueyInfo.SystemDataFields);
                        db.AddInParameter(dbCommand, "SystemCondition", DbType.Int64, customQueyInfo.SystemCondition);
                        db.AddInParameter(dbCommand, "GroupCondition", DbType.Int64, customQueyInfo.GroupCondition);
                        db.AddInParameter(dbCommand, "ShowMode", DbType.Byte, customQueyInfo.ShowMode);
                        db.AddInParameter(dbCommand, "DataRange", DbType.Int64, customQueyInfo.DataRange);
                        db.AddInParameter(dbCommand, "ToolTip", DbType.String, customQueyInfo.ToolTip);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customQueyInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customQueyInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customQueyId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@DataQueriedId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(customQueyInfo.GroupId, false, db, transaction);
                    if (dataQueriedType == DataQueriedType.Custom)
                    {
                        CreateQueryView(customQueyInfo.DataWarehouseId, customQueyInfo.CustomViewName, customQueyInfo.Conditions);
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
			return customQueyId;
		}

        /// <summary>
		/// 获得 CustomQueyInfo 对象
		/// </summary>
		///<param name="dataQueriedId">数据查询编号</param>
		/// <returns> CustomQueyInfo 对象</returns>
		public CustomQueyInfo GetModelInfo(decimal dataQueriedId)
		{			
			CustomQueyInfo customQueyInfo = null;            

            IList<WhereConditon> whereConditons = new List<WhereConditon>();            
            //给参数赋值
            whereConditons.Add(new WhereConditon("DataQueriedId", "DataQueriedId", System.Data.DbType.Decimal, dataQueriedId, DataFieldCondition.Equal));
            
            //创建集合对象
			IList<CustomQueyInfo> customQueyInfos = GetModelInfos(whereConditons, null, true);
            if (customQueyInfos != null && customQueyInfos.Count > 0)
            {
                customQueyInfo = customQueyInfos[0];
            }          

            return customQueyInfo;
		}
        
        /// <summary>
		/// 更新 CustomQueyInfo 对象
		/// </summary>
		/// <param name="customQueyInfo">CustomQueyInfo 对象</param>
		public void Update(CustomQueyInfo customQueyInfo)
		{
            CustomQueyInfo oldCustomQueyInfo = GetModelInfo(customQueyInfo.DataQueriedId);

            //生成更新语句
            StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE CustomQuey SET TableId = @TableId, GroupId = @GroupId, ViewId = @ViewId, ");
			sb.Append("DataQueriedName = @DataQueriedName, DataQueriedCode = @DataQueriedCode, DataWarehouseId = @DataWarehouseId, Conditions = @Conditions, ");
			sb.Append("SystemDataFields = @SystemDataFields, SystemCondition = @SystemCondition, GroupCondition = @GroupCondition, ");
			sb.Append("ShowMode = @ShowMode, DataRange = @DataRange, ToolTip = @ToolTip,  Notes = @Notes ");
			sb.Append("WHERE DataQueriedId = @DataQueriedId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, customQueyInfo.DataQueriedId);
					db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customQueyInfo.TableId));
					db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customQueyInfo.GroupId);
					db.AddInParameter(dbCommand, "ViewId", DbType.Decimal, DataConvertionHelper.SetDecimal(customQueyInfo.ViewId));
					db.AddInParameter(dbCommand, "DataQueriedName", DbType.String, customQueyInfo.DataQueriedName);
					db.AddInParameter(dbCommand, "DataQueriedCode", DbType.String, customQueyInfo.DataQueriedCode);
                    db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, customQueyInfo.DataWarehouseId);
                    db.AddInParameter(dbCommand, "Conditions", DbType.String, customQueyInfo.Conditions);
                    db.AddInParameter(dbCommand, "SystemDataFields", DbType.Int64, customQueyInfo.SystemDataFields);
                    db.AddInParameter(dbCommand, "SystemCondition", DbType.Int64, customQueyInfo.SystemCondition);
					db.AddInParameter(dbCommand, "GroupCondition", DbType.Int64, customQueyInfo.GroupCondition);
					db.AddInParameter(dbCommand, "ShowMode", DbType.Byte, customQueyInfo.ShowMode);
					db.AddInParameter(dbCommand, "DataRange", DbType.Int64, customQueyInfo.DataRange);
					db.AddInParameter(dbCommand, "ToolTip", DbType.String, customQueyInfo.ToolTip);
					db.AddInParameter(dbCommand, "Notes", DbType.String, customQueyInfo.Notes);
					//执行更新操作
					if (db.ExecuteNonQuery(dbCommand) != 1)
					{
						throw new Exception("更新失败！");
					}
				}
                
                ///* 视图名称保存不变 */
                //FormType tableFilter = (FormType)customQueyInfo.DataQueriedType;
                //if (tableFilter == FormType.Custom && !customQueyInfo.Conditions.Equals(oldCustomQueyInfo.Conditions))
                //{
                //    CreateQueryView(customQueyInfo.DataWarehouseId, oldCustomQueyInfo.CustomViewName, customQueyInfo.Conditions);
                //}
            }
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
		}        
        
        /// <summary>
		///  删除 CustomQueyInfo 对象
		/// </summary>
	    ///<param name="dataQueriedId">数据查询编号</param>
		public void Delete(decimal dataQueriedId)
		{

			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM CustomQuey ");
			sb.Append("WHERE DataQueriedId = @DataQueriedId");

            CustomQueyAndDataField customQueyAndDataField = new CustomQueyAndDataField();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    customQueyAndDataField.DeleteBySecondForeignKey(dataQueriedId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, dataQueriedId);
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
		/// 获得 CustomQueyInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomQueyInfo 对象列表</returns>
		public IList<CustomQueyInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			return GetModelInfos(whereConditons, sortingCondtions, false);
		}               
        
        /// <summary>
		/// 获得 CustomQuey 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomQueyInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomQuey ", "DataQueriedId", false, whereConditons);
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
        /// 获得分页数据集记录数
        /// </summary>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="dataWarehouseId"></param>
        /// <returns></returns>
        public int GetAuthorizedRecordCount(QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, byte dataWarehouseId)
        {
            int totalCount = 0;

            try
            {
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                QueryClauseConstructor queryClauseConstructor = new QueryClauseConstructor(queryBuilder, 0, 0);
                string whereClause = DataAccessHandler.GetConditionSentence(whereConditons);
                totalCount = GetRecordCount(db, queryBuilder, queryClauseConstructor, whereClause, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            return totalCount;
        }

        /// <summary>
        /// 完全删除记录(多表联合查询使用)
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="tableIds"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        public void Delete(decimal dataWarehouseId, IList<decimal> tableIds, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons)
        {
            CustomTable customTable = new CustomTable();
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            QueryClauseConstructor qeryClauseConstructor = new QueryClauseConstructor(queryBuilder, 0, 0);
            string where = DataAccessHandler.GetWhereSentence(whereConditons);

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (decimal tableId in tableIds)
                    {
                        string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
                        StringBuilder sbDelete = new StringBuilder();
                        sbDelete.AppendFormat("DELETE FROM {0} WHERE RecordId IN ", tablePhysicalName);
                        sbDelete.AppendFormat("(SELECT {0}.RecordId FROM {1} ", tablePhysicalName, qeryClauseConstructor.TableClause);
                        if (qeryClauseConstructor.Where.Length > 0 || where.Length > 0)
                        {
                            sbDelete.Append(" WHERE ");
                        }
                        if (qeryClauseConstructor.Where.Length > 0)
                        {
                            sbDelete.AppendFormat("({0})", qeryClauseConstructor.Where);
                        }
                        if (where.Length > 0)
                        {
                            if (qeryClauseConstructor.Where.Length > 0)
                            {
                                sbDelete.Append(" AND ");
                            }
                            sbDelete.AppendFormat("({0})", where);
                        }
                        sbDelete.Append(")");
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sbDelete.ToString()))
                        {
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
        /// 获得分页数据集
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count)
        {
            int totalCount = 0;
            return GetAuthorizedData(dataWarehouseId, queryBuilder, whereConditons, startPosition, count, true, ref totalCount);
        }

        /// <summary>
        /// 获得分页数据集
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count, ref int totalCount)
        {
            return GetAuthorizedData(dataWarehouseId, queryBuilder, whereConditons, startPosition, count, false, ref totalCount);
        }

        /// <summary>
        /// 获得分页数据集
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="queryBuilder"></param>
        /// <param name="whereConditons"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="skippedToatlCount"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetAuthorizedData(byte dataWarehouseId, QueryBuilder queryBuilder, IList<WhereConditon> whereConditons, int startPosition, int count, 
            bool skippedToatlCount, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                QueryClauseConstructor queryClauseConstructor = new QueryClauseConstructor(queryBuilder, startPosition, count);
                string whereClause = DataAccessHandler.GetConditionSentence(whereConditons);
                if (skippedToatlCount)
                {
                    totalCount = 0;
                }
                else
                {
                    totalCount = GetRecordCount(db, queryBuilder, queryClauseConstructor, whereClause, whereConditons);
                }

                string sqlClause = string.Empty;

                /* (1)分组(单表和多表)
                 * (2)清除相同(单表和多表)
                 * (2)非分组和单表 */
                if (queryBuilder.GroupBy || queryBuilder.Distinct || queryBuilder.CurrentTableNames.Count == 1)
                {
                    sqlClause = queryClauseConstructor.BuildSqlSelectStatement(whereClause);
                    //获得系统数据库对象
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlClause))
                    {
                        if (!string.IsNullOrWhiteSpace(whereClause))
                        {
                            DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                        }
                        ds = db.ExecuteDataSet(dbCommand);
                    }
                }
                else
                {
                    string where = queryBuilder.ValidWhere ? queryClauseConstructor.Where : string.Empty;
                    string orderBy = queryClauseConstructor.GetNewOrderBy();
                    if (string.IsNullOrWhiteSpace(orderBy))
                    {
                        string recordIdName = string.Format("{0}.{1}", queryClauseConstructor.PrimaryTableName, DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId));
                        ds = DataAccessHandler.GetPageRecord(db, queryClauseConstructor.TableClause, queryClauseConstructor.Select, false, null, startPosition, count, whereConditons, where, recordIdName);
                    }
                    else
                    {
                        ds = DataAccessHandler.GetPageRecord(db, queryClauseConstructor.TableClause, queryClauseConstructor.Select, false, null, startPosition, count, whereConditons, where, orderBy);
                    }
                }
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.Caption = queryClauseConstructor.DataFieldNameRelations[dataColumn.ColumnName];
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
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal dataQueriedId)
        {
            byte dataWarehouseId = 0;

            string sqlSelect = "SELECT DataWarehouseId FROM CustomQuey WHERE DataQueriedId = @DataQueriedId ";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, dataQueriedId);
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
		/// 获得 CustomQuey 表中记录的数目
		/// </summary>
        /// <param name="viewId">视图编号</param>
		/// <returns>CustomQueyInfo 记录的数目</returns>
		public int GetTotalCountByViewId(decimal viewId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("ViewId", "ViewId", DbType.Decimal, viewId, DataFieldCondition.Equal));

            return GetTotalCount(whereConditons);
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetQueriedData(decimal dataQueriedId, int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            CustomQueyInfo customQueyInfo = GetModelInfo(dataQueriedId);
            StringBuilder sb = new StringBuilder();
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            sb.Append(recordIdName);
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)customQueyInfo.DataWarehouseId));
            DataSet ds = null;
            if (sortingCondtions == null || sortingCondtions.Count == 0)
            {
                ds = DataAccessHandler.GetPageRecord(db, customQueyInfo.CustomViewName, sb.ToString(), false, null, startPosition, count, whereConditons, recordIdName, ref totalCount);
            }
            else
            {
                ds = DataAccessHandler.GetPageRecord(db, customQueyInfo.CustomViewName, sb.ToString(), false, null, startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            CustomDataField customDataField = new CustomDataField();
            foreach (DataColumn dataColumn in ds.Tables[0].Columns)
            {
                string name = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                if(!string.IsNullOrWhiteSpace(name))
                {
                    dataColumn.Caption = name;
                }
                else
                {
                    name = customDataField.GetLogicalName(dataColumn.ColumnName);
                    if (!string.IsNullOrWhiteSpace(name))
                    {
                        dataColumn.Caption = name;
                    }
                    else
                    {
                        dataColumn.Caption = dataColumn.ColumnName;
                    }
                }
            }
            return ds.Tables[0];
        }


        /// <summary>
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            CustomQueyInfo customQueyInfo = GetModelInfo(nodeId);
            if (customQueyInfo != null)
            {
                CustomGroup customGroup = new CustomGroup();
                IList<string> parentNames = customGroup.GetHierarchicalNamesOfNode(customQueyInfo.GroupId);
                foreach (var parentName in parentNames)
                {
                    names.Add(parentName);
                }
                names.Add(customQueyInfo.DataQueriedName);
            }

            return names;
        }

        /// <summary>
        /// 获得自定义查询中的视图字段（不包含系统字段）
        /// </summary>
        /// <param name="dataQueriedId">查询编号</param>
        /// <returns></returns>
        public IList<CommonNode> GetDataFields(decimal dataQueriedId)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            
            try
            {                
                CustomQueyInfo customQueyInfo = GetModelInfo(dataQueriedId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)customQueyInfo.DataWarehouseId));

                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT TOP 0 * FROM {0}", customQueyInfo.CustomViewName);

                CustomDataField customDataField = new CustomDataField();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    DataTable dt = db.ExecuteDataSet(dbCommand).Tables[0];
                    foreach (DataColumn dc in dt.Columns)
                    {
                        CommonNode commonNode = customDataField.GetCommonNode(dc.ColumnName);
                        if (commonNode != null)
                        {
                            commonNodes.Add(commonNode);
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
        /// 验证 WHERE 条件
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool ValidateWhereSentences(decimal dataQueriedId, string whereExpression)
        {
            bool success = false;

            try
            {
                CustomQueyInfo customQueyInfo = GetModelInfo(dataQueriedId);
                if (customQueyInfo != null)
                {
                    SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)customQueyInfo.DataWarehouseId));
                    StringBuilder sb = new StringBuilder();
                    sb.AppendFormat("SELECT TOP 0 {0} FROM ", DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId));
                    FormType tableFilter = (FormType)customQueyInfo.DataQueriedType;
                    switch (tableFilter)
                    {
                        case FormType.Table:
                            CustomTable customTable = new CustomTable();
                            sb.Append(customTable.GetTablePhysicalName(customQueyInfo.TableId));
                            break;

                        //case FormType.View:
                        //    CustomView customView = new CustomView();
                        //    sb.Append(customView.GetViewPhysicalName(customQueyInfo.ViewId));
                        //    break;

                        //case FormType.Custom:
                        //    sb.Append(customQueyInfo.CustomViewName);
                        //    break;
                    }

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
            }
            catch
            { }

            return success;
        }                    

        /// <summary>
        /// 验证SQL语句是否正确
        /// </summary>
        /// <param name="dataWarehouseId">数据仓库编号</param>
        /// <param name="sql">SQL 语句</param>
        /// <returns></returns>
        public bool ValidateSQL(byte dataWarehouseId, string sql)
        {
            bool result = false;

            try
            {
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                string[] sqlClauses = new string[] { "SET PARSEONLY ON", sql, "SET PARSEONLY OFF" };
                foreach (string sqlClause in sqlClauses)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlClause))
                    {
                        db.ExecuteNonQuery(dbCommand);
                    }
                }
                result = true;
            }
            catch { }

            return result;
        }


        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void DeleteByTableId(decimal tableId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE RoleAndBusiness FROM RoleAndBusiness INNER JOIN  CustomBusiness ON RoleAndBusiness.BusinessId = CustomBusiness.BusinessId ");
            sb.Append("INNER JOIN CustomQuey ON CustomQuey.DataQueriedId = CustomBusiness.DataQueriedId  WHERE CustomQuey.TableId = @TableId; ");
            sb.Append("DELETE CustomBusiness FROM CustomBusiness INNER JOIN CustomQuey ON CustomQuey.DataQueriedId = CustomBusiness.DataQueriedId ");
            sb.Append("WHERE CustomQuey.TableId = @TableId;");
            sb.Append("DELETE CustomQuey FROM CustomQuey ");
            sb.Append("WHERE TableId = @TableId ");

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

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomQueyInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomQueyInfo 对象列表</returns>
        private IList<CustomQueyInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CustomQueyInfo>  customQueyInfos = new List<CustomQueyInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            
            sb.Append(" * FROM CustomQuey");
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
                            decimal dataQueriedId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal viewId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            string dataQueriedName = DataConvertionHelper.GetString(dataReader[4]);
                            string dataQueriedCode = DataConvertionHelper.GetString(dataReader[5]);
                            byte dataWarehouseId = DataConvertionHelper.GetByte(dataReader[6]);
                            string customViewName = DataConvertionHelper.GetString(dataReader[7]);
                            string conditions = DataConvertionHelper.GetString(dataReader[8]);
                            byte dataQueriedType = DataConvertionHelper.GetByte(dataReader[9]);
                            long systemDataFields = DataConvertionHelper.GetLong(dataReader[10]);
                            long systemCondition = DataConvertionHelper.GetLong(dataReader[11]);
                            long groupCondition = DataConvertionHelper.GetLong(dataReader[12]);
                            byte showMode = DataConvertionHelper.GetByte(dataReader[13]);
                            long dataRange = DataConvertionHelper.GetLong(dataReader[14]);
                            string toolTip = DataConvertionHelper.GetString(dataReader[15]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[16]);
                            string notes = DataConvertionHelper.GetString(dataReader[17]);
                            //将创建 CustomQueyInfo 对象加入集合中
                            customQueyInfos.Add(new CustomQueyInfo(dataQueriedId, groupId, tableId, viewId, dataQueriedName,
                            dataQueriedCode, dataWarehouseId, customViewName, conditions, dataQueriedType,
                            systemDataFields, systemCondition, groupCondition, showMode, dataRange,
                            toolTip, sorting, notes));
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
            
			return customQueyInfos;
		} 
        
        /// <summary>
		/// 获得 CustomQueyInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CustomQueyInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomQuey");
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
        /// 获得表 CustomQuey 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomQuey ", "DataQueriedId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomQuey 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomQuey ", "DataQueriedId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomQuey 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomQuey ", "DataQueriedId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomQuey 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomQuey ", "DataQueriedId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomQueyInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomQuey");
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

        /// <summary>
        /// 获得SQL语句
        /// </summary>
        ///<param name="dataQueriedId">查询编号</param>
        /// <returns> SQL语句</returns>
        public string GetConditions(decimal dataQueriedId)
        {
            string conditions = string.Empty;

            try
            {
                string sqlSelect = "SELECT Conditions FROM CustomQuey WHERE DataQueriedId = @DataQueriedId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataQueriedId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataQueriedId));
                    conditions = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return conditions;
        }

        #endregion

        #region 自定义私有方法

        /// <summary>
        /// 获得记录数
        /// </summary>
        /// <param name="db">数据库对象</param>
        /// <param name="queryBuilder"></param>
        /// <param name="queryClauseConstructor"></param>
        /// <param name="whereClause"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        private int GetRecordCount(SqlDatabase db, QueryBuilder queryBuilder, QueryClauseConstructor queryClauseConstructor,
            string whereClause, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            StringBuilder sb = new StringBuilder();
            if (queryBuilder.Distinct)
            {
                sb.Append("SELECT COUNT (*) FROM (SELECT DISTINCT ");
                foreach (QueryField qf in queryBuilder.QueryFields)
                {
                    if (qf.Output)
                    {
                        DataFieldProperty dataFieldProperty = (DataFieldProperty)qf.DataFieldProperty;
                        switch (dataFieldProperty)
                        {
                            case DataFieldProperty.PhysicalDataField:
                                sb.Append(qf.ColumnName);
                                break;

                            case DataFieldProperty.SystemPhysicalDataField:
                            case DataFieldProperty.LogicalDataField:
                                sb.AppendFormat("{0} AS {1}", qf.ColumnName, qf.Name);
                                break;

                                //case DataFieldProperty.SystemPhysicalDataField:
                                //    sb.AppendFormat("{0}.{1} AS {2}", qf.DataTableName, qf.ColumnName, qf.Name);
                                //    break;
                        }
                        sb.Append(", ");
                    }
                }
                sb.Remove(sb.Length - 2, 2);
            }
            else
            {
                sb.Append("SELECT COUNT(1)");
            }
            sb.Append(" FROM ");
            sb.Append(queryClauseConstructor.TableClause);
            if ((queryBuilder.ValidWhere && !string.IsNullOrWhiteSpace(queryClauseConstructor.Where)) || !string.IsNullOrWhiteSpace(whereClause))
            {
                sb.Append(" WHERE ");
            }
            if (queryBuilder.ValidWhere && !string.IsNullOrWhiteSpace(queryClauseConstructor.Where))
            {
                sb.Append(queryClauseConstructor.Where);
            }
            if (!string.IsNullOrWhiteSpace(whereClause))
            {
                if (queryBuilder.ValidWhere && !string.IsNullOrWhiteSpace(queryClauseConstructor.Where))
                {
                    sb.Append(" AND ");
                }
                sb.Append(whereClause);
            }
            if (queryBuilder.Distinct)
            {
                sb.Append(") A");
            }
            //获得系统数据库对象
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                if (!string.IsNullOrWhiteSpace(whereClause))
                {
                    DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                }
                count = Convert.ToInt32(db.ExecuteScalar(dbCommand));
            }

            return count;
        }

        /// <summary>
        /// 创建视图
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <param name="physicalName"></param>
        /// <param name="sql"></param>
        private void CreateQueryView(byte dataWarehouseId, string physicalName, string sql)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(physicalName))
                {
                    string[] sqlViews = new string[] { string.Format("IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id(N'[dbo].[{0}]') AND OBJECTPROPERTY(id, N'IsView') = 1) DROP VIEW {0}",
                    physicalName), string.Format("CREATE VIEW {0} AS {1}", physicalName, sql) };

                    SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                    foreach (string sqlView in sqlViews)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sqlView))
                        {
                            db.ExecuteNonQuery(dbCommand);
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
        /// 更新查询的条件
        /// </summary>
        /// <param name="dataQueriedId"></param>
        /// <param name="conditions"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void UpdateConditions(decimal dataQueriedId, string conditions, SqlDatabase db, DbTransaction transaction)
        {
            string sqlUpdated = "UPDATE CustomQuey SET Conditions = @Conditions WHERE DataQueriedId = @DataQueriedId";

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdated))
                {
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataQueriedId));
                    db.AddInParameter(dbCommand, "Conditions", DbType.String, conditions);
                    //执行更新操作
                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
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

        #endregion
    }
}
