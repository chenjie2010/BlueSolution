//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomCell.cs
// 描述: CustomCell 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.CustomLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.BusinessModule;

namespace Blue.SQLServerDAL.BusinessDesignerModule
{
    /// <summary>
    /// CustomCell 表的数据层访问类
    /// </summary>
    public class CustomCell : ICustomCell
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomCell()
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 CustomCell 表中插入一条新记录
		/// </summary>
		/// <param name="customCellInfo">customCellInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomCellInfo customCellInfo)
		{
			//自动增加的关键字的值
			decimal customCellId = 0;
			
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
                Insert(customCellInfo, db, null);
			}
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customCellId;
		}

        /// <summary>
		/// 获得 CustomCellInfo 对象
		/// </summary>
		///<param name="cellId">CellId</param>
		/// <returns> CustomCellInfo 对象</returns>
		public CustomCellInfo GetModelInfo(decimal cellId)
		{			
			CustomCellInfo  customCellInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("CellId", "CellId", DbType.Decimal, cellId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomCellInfo>  customCellInfos = GetModelInfos(whereConditons, true);
            if (customCellInfos != null && customCellInfos.Count > 0)
            {
                customCellInfo = customCellInfos[0];
            }

            return customCellInfo;
		}
        
        /// <summary>
		/// 更新 CustomCellInfo 对象
		/// </summary>
		/// <param name="customCellInfo">CustomCellInfo 对象</param>
		public void Update(CustomCellInfo customCellInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE CustomCell SET TableId = @TableId, SheetId = @SheetId, RowIndex = @RowIndex, ColIndex = @ColIndex, ");
			sb.Append("CellType = @CellType, CellProperty = @CellProperty, ConditionText = @ConditionText, ");
			sb.Append("TemplateText = @TemplateText, ExtendRows = @ExtendRows, ExtendCols = @ExtendCols ");
			sb.Append("WHERE CellId = @CellId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "CellId", DbType.Decimal, customCellInfo.CellId);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customCellInfo.TableId));
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, customCellInfo.SheetId);
					db.AddInParameter(dbCommand, "RowIndex", DbType.Int32, customCellInfo.RowIndex);
					db.AddInParameter(dbCommand, "ColIndex", DbType.Int32, customCellInfo.ColIndex);
					db.AddInParameter(dbCommand, "CellType", DbType.Byte, customCellInfo.CellType);
					db.AddInParameter(dbCommand, "CellProperty", DbType.Int64, customCellInfo.CellProperty);
					db.AddInParameter(dbCommand, "ConditionText", DbType.String, customCellInfo.ConditionText);
					db.AddInParameter(dbCommand, "TemplateText", DbType.String, customCellInfo.TemplateText);
					db.AddInParameter(dbCommand, "ExtendRows", DbType.Int32, customCellInfo.ExtendRows);
					db.AddInParameter(dbCommand, "ExtendCols", DbType.Int32, customCellInfo.ExtendCols);
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
		///  删除 CustomCellInfo 对象
		/// </summary>
	    ///<param name="cellId">CellId</param>
		public void Delete(decimal cellId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM CustomCell ");
			sb.Append("WHERE CellId = @CellId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "CellId", DbType.Decimal, cellId);
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
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomCellInfo 对象列表</returns>
        public IList<CustomCellInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, false);
		}        
        
        /// <summary>
		/// 获得 CustomCell 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomCellInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomCell ", "CellId", false, whereConditons);
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
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="cellType"></param>
        /// <returns></returns>
        public IList<CustomCellInfo> GetModelInfos(decimal sheetId, byte cellType)
        {
            //创建集合对象
            IList<CustomCellInfo> customCellInfos = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("SheetId", "SheetId", DbType.Decimal, sheetId,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("CellType", "CellType", DbType.Byte, cellType,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            customCellInfos = GetModelInfos(whereConditons, false);

            return customCellInfos;
        }

        /// <summary>
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>
        /// <param name="sheetId"></param>
        /// <returns></returns>
        public IList<CustomCellInfo> GetModelInfos(decimal sheetId)
        {
            //创建集合对象
            IList<CustomCellInfo> customCellInfos = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("SheetId", "SheetId", DbType.Decimal, sheetId, DataFieldCondition.Equal));
            customCellInfos = GetModelInfos(whereConditons, false);

            return customCellInfos;
        }

        /// <summary>
        /// 获得 CustomCellInfo 对象
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public CustomCellInfo GetModelInfo(decimal sheetId, int row, int col)
        {
            CustomCellInfo customCellInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("SheetId", "SheetId", DbType.Decimal, sheetId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("RowIndex", "RowIndex", DbType.Int32, row, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            whereConditons.Add(new WhereConditon("ColIndex", "ColIndex", DbType.Int32, col, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            //创建集合对象
            IList<CustomCellInfo> customCellInfos = GetModelInfos(whereConditons, true);
            if (customCellInfos != null && customCellInfos.Count > 0)
            {
                customCellInfo = customCellInfos[0];
            }

            return customCellInfo;
        }

        /// <summary>
        /// 删除 CustomCellInfo 对象
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        public int Delete(decimal sheetId, int row, int col)
        {
            int count = 0;

            decimal cellId = GetCellId(sheetId, row, col);
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomCell ");
            sb.Append("WHERE SheetId = @SheetId AND RowIndex = @RowIndex AND ColIndex = @ColIndex");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    CellStyle cellStyle = new CellStyle();
                    cellStyle.Delete(cellId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, DataConvertionHelper.SetDecimal(sheetId));
                        db.AddInParameter(dbCommand, "RowIndex", DbType.Decimal, DataConvertionHelper.SetInt(row));
                        db.AddInParameter(dbCommand, "ColIndex", DbType.Decimal, DataConvertionHelper.SetInt(col));
                        //执行删除操作
                        count = db.ExecuteNonQuery(dbCommand, transaction);
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

            return count;
        }

        /// <summary>
        /// 获得表套所属的数据仓库编号
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal cellId)
        {
            byte dataWarehouseId = 0;

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DataWarehouseId FROM CustomCell ");
            sb.Append("INNER JOIN CustomSheet ON CustomCell.SheetId = CustomSheet.SheetId ");
            sb.Append("INNER JOIN CustomReport ON CustomReport.ReportId = CustomSheet.ReportId ");
            sb.Append("WHERE CellId = @CellId ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CellId", DbType.Decimal, DataConvertionHelper.SetDecimal(cellId));
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
        /// 验证条件是否正确
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="template"></param>
        /// <param name="condition"></param>
        /// <param name="tableLinks"></param>
        /// <returns></returns>
        public bool VaildateCellCondition(decimal tableId, string template, string condition, List<TableLink> tableLinks)
        {
            bool success = false;

            CustomTable customTable = new CustomTable();
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
            string tableName = tablePhysicalName;       
            if (tableLinks != null  && tableLinks.Count > 0)
            {
                tablePhysicalName = DataAccessHandler.GetTableNames(tablePhysicalName, tableLinks);
            }
            StringBuilder sb = new StringBuilder();
            if (string.IsNullOrWhiteSpace(template))
            {
                sb.AppendFormat("SELECT TOP 0 {0}.UserId FROM {1} ", tableName, tablePhysicalName);
            }
            else
            {
                sb.AppendFormat("SELECT {0} FROM {1} ", template, tablePhysicalName);
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                sb.AppendFormat(" WHERE {0}", condition);
            }
            try
            {
                //获得系统数据库对象
                byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.ExecuteScalar(dbCommand);
                }
                success = true;
            }
            catch { }

            return success;
        }

        /// <summary>
        /// 获得单元格范围的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public DataTable GetCellRangeData(decimal cellId, byte dataWarehouseId, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            DataTable cellRangeData = null;

            try
            {
                CellConditionItem cellConditionItem = GetQueryClauses(cellId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
                if (cellConditionItem == null)
                {
                    return null;
                }

                /* 创建 Select 语句 */
                CellStyle cellStyle = new CellStyle();
                IList<CommonNode> commonNodes = cellStyle.GetCommonNodes(cellId, CellCondition.Show);
                CustomDataField customDataField = new CustomDataField();
                StringBuilder sbSelect = new StringBuilder();
                Dictionary<string, CustomDataFieldInfo> customDataFieldInfos = new Dictionary<string, CustomDataFieldInfo>(commonNodes.Count);
                sbSelect.Append("SELECT ");
                foreach (var commonNode in commonNodes)
                {
                    CustomDataFieldInfo customDataFieldInfo = null;
                    string key = string.Empty;
                    if ((DataFieldProperty)commonNode.NodeType == DataFieldProperty.SystemPhysicalDataField)
                    {
                        SystemDataField systemDataField = (SystemDataField)commonNode.NodeId;
                        customDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(commonNode.ParentNodeId, commonNode.NodeCode, systemDataField);
                        key = string.Format("{0}_{1}", customDataFieldInfo.TableId, customDataFieldInfo.DataFieldId);
                    }
                    else
                    {
                        customDataFieldInfo = customDataField.GetModelInfo(commonNode.NodeId);
                        key = customDataFieldInfo.DataFieldId.ToString();
                    }
                    customDataFieldInfos.Add(key, customDataFieldInfo);
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.SystemPhysicalDataField:
                        case DataFieldProperty.PhysicalDataField:
                            sbSelect.AppendFormat("{0}, ", customDataFieldInfo.PhysicalName);
                            break;

                        case DataFieldProperty.LogicalDataField:
                            sbSelect.AppendFormat("{0}, ", customDataField.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId));
                            break;
                    }
                }
                sbSelect.Remove(sbSelect.Length - 2, 2);
                sbSelect.AppendFormat(" FROM {0} ", cellConditionItem.FromClause);
                if (string.IsNullOrWhiteSpace(cellConditionItem.WhereClause))
                {
                    sbSelect.AppendFormat(" WHERE {0}", cellConditionItem.WhereClause);
                }
                try
                {
                    //获得系统数据库对象
                    SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sbSelect.ToString()))
                    {
                        cellRangeData = db.ExecuteDataSet(dbCommand).Tables[0];
                    }
                }
                catch { }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return cellRangeData;
        }

        /// <summary>
        /// 获得行扩展数据
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetExtendedRowTextData(decimal cellId, byte dataWarehouseId, string condition, string templateText)
        {
            Dictionary<decimal, string> textData = new Dictionary<decimal, string>();

            CustomTable customTable = new CustomTable();
            CellStyle cellStyle = new CellStyle();
            CustomCellInfo customCellInfo = GetModelInfo(cellId);
            string mainTableName = customTable.GetTablePhysicalName(customCellInfo.TableId);
            IList<CommonNode> commonNodes = cellStyle.GetCommonNodes(cellId, CellCondition.Show);
            CustomDataField customDataField = new CustomDataField();
            StringBuilder sbExpression = new StringBuilder(customCellInfo.TemplateText);
            StringBuilder sbReplace = new StringBuilder();
            int index = 0;
            string dataFieldPhysicalName = string.Empty;
            Dictionary<string, TableLink> systemTableLinks = new Dictionary<string, TableLink>();
            foreach (CommonNode node in commonNodes)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)node.NodeType;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemDataField systemDataField = (SystemDataField)node.NodeId;
                        dataFieldPhysicalName = DataFieldHelper.GetSystemLogicalDataFieldName(mainTableName, systemDataField);
                        string systemTablePhysicalName = DataFieldHelper.GetSystemTablePhysicalName(systemDataField);
                        if (!systemTableLinks.ContainsKey(systemTablePhysicalName))
                        {
                            systemTableLinks.Add(systemTablePhysicalName, DataFieldHelper.GetTableLink(mainTableName, systemDataField));
                        }
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        dataFieldPhysicalName = node.NodeCode;
                        break;

                    case DataFieldProperty.LogicalDataField:
                        dataFieldPhysicalName = customDataField.GetDataFieldLogicalExpression(node.NodeId);
                        break;
                }
                sbReplace.Append("{");
                sbReplace.Append(index);
                sbReplace.Append("}");
                sbExpression.Replace(sbReplace.ToString(), dataFieldPhysicalName);
                sbReplace.Clear();
                index++;
            }
            if (systemTableLinks != null && systemTableLinks.Count > 0)
            {
                mainTableName = DataAccessHandler.GetTableNames(mainTableName, systemTableLinks.Values.ToList<TableLink>());
            }
            StringBuilder sbSelect = new StringBuilder();
            if (sbExpression.Length > 0)
            {
                sbSelect.AppendFormat("SELECT {0}.DepId, {1} FROM {0} ", mainTableName, sbExpression);
            }
            else
            {
                sbSelect.AppendFormat("SELECT {0}.DepId, {1} FROM {0} ", mainTableName, dataFieldPhysicalName);
            }

            /* 2. 创建 Where 语句 */
            if (customCellInfo.TableId > 0 && !string.IsNullOrWhiteSpace(condition))
            {
                sbSelect.AppendFormat("WHERE ({0}) ORDER BY DepCode", condition);
            }

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
            using (DbCommand dbCommand = db.GetSqlStringCommand(sbSelect.ToString()))
            {
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    while (dataReader.Read())
                    {
                        decimal depId = DataConvertionHelper.GetDecimal(dataReader[0]);
                        string text = DataConvertionHelper.GetString(dataReader[1]);
                        if (!textData.ContainsKey(depId))
                        {
                            textData.Add(depId, text);
                        }
                    }
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                }
            }

            return textData;
        }

        /// <summary>
        /// 获得行单元格的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <param name="whereConditons"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public string GetRowCellText(decimal cellId, byte dataWarehouseId, string condition, string templateText, IList<WhereConditon> whereConditons,
            IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            string text = string.Empty;

            try
            {
                string mainTableName = string.Empty;
                CustomTable customTable = new CustomTable();
                CellStyle cellStyle = new CellStyle();
                CustomCellInfo customCellInfo = GetModelInfo(cellId);                
              
                /* 1. 创建 Where 语句 */
                StringBuilder sbWhere = new StringBuilder();
                /* 1.1 本单元格条件*/
                if (customCellInfo.TableId > 0)
                {
                    if (!string.IsNullOrWhiteSpace(condition))
                    {
                        if (sbWhere.Length > 0)
                        {
                            sbWhere.Append(" AND ");
                        }
                        sbWhere.AppendFormat("({0})", condition);
                    }
                    mainTableName = customTable.GetTablePhysicalName(customCellInfo.TableId);
                }
                string where = DataAccessHandler.GetWhereSentence(whereConditons);
                if (!string.IsNullOrWhiteSpace(where))
                {
                    if (sbWhere.Length > 0)
                    {
                        sbWhere.Append(" AND ");
                    }
                    sbWhere.Append(where);
                }                

                /* 管理的用户类型 */
                if (relatedUserTypeCommonNodes != null && relatedUserTypeCommonNodes.Count > 0)
                {
                    if (sbWhere.Length > 0)
                    {
                        sbWhere.Append(" AND ");
                    }
                    sbWhere.Append("(");
                    foreach (CommonNode commonNode in relatedUserTypeCommonNodes)
                    {

                        sbWhere.AppendFormat(" {0}.UserTypeId = {1} OR", mainTableName, commonNode.NodeId);
                    }
                    sbWhere.Remove(sbWhere.Length - 3, 3);
                    sbWhere.Append(")");
                }

                /* 管理的单位 */
                if (relatedDepartmentCommonNodes != null && relatedDepartmentCommonNodes.Count > 0)
                {
                    if (sbWhere.Length > 0)
                    {
                        sbWhere.Append(" AND ");
                    }
                    sbWhere.Append("(");
                    foreach (CommonNode commonNode in relatedDepartmentCommonNodes)
                    {

                        sbWhere.AppendFormat(" {0}.DepID = {1} OR", mainTableName, commonNode.NodeId);
                    }
                    sbWhere.Remove(sbWhere.Length - 3, 3);
                    sbWhere.Append(")");
                }

                /* 3. 创建 Select 语句 */
                IList<CommonNode> commonNodes = cellStyle.GetCommonNodes(cellId, CellCondition.Show);
                CustomDataField customDataField = new CustomDataField();
                StringBuilder sbExpression = new StringBuilder(customCellInfo.TemplateText);
                StringBuilder sbReplace = new StringBuilder();
                int index = 0;
                string dataFieldPhysicalName = string.Empty;
                Dictionary<string, TableLink> systemTableLinks = new Dictionary<string, TableLink>();
                foreach (CommonNode node in commonNodes)
                {
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)node.NodeType;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.SystemPhysicalDataField:
                            SystemDataField systemDataField = (SystemDataField)node.NodeId;
                            dataFieldPhysicalName = DataFieldHelper.GetSystemLogicalDataFieldName(mainTableName, systemDataField);
                            string systemTablePhysicalName = DataFieldHelper.GetSystemTablePhysicalName(systemDataField);
                            if (!systemTableLinks.ContainsKey(systemTablePhysicalName))
                            {
                                systemTableLinks.Add(systemTablePhysicalName, DataFieldHelper.GetTableLink(mainTableName, systemDataField));
                            }
                            break;

                        case DataFieldProperty.PhysicalDataField:
                            dataFieldPhysicalName = node.NodeCode;
                            break;

                        case DataFieldProperty.LogicalDataField:
                            dataFieldPhysicalName = customDataField.GetDataFieldLogicalExpression(node.NodeId);
                            break;
                    }
                    sbReplace.Append("{");
                    sbReplace.Append(index);
                    sbReplace.Append("}");
                    sbExpression.Replace(sbReplace.ToString(), dataFieldPhysicalName);
                    sbReplace.Clear();
                    index++;
                }
                StringBuilder sbSelect = new StringBuilder();
                if (sbExpression.Length > 0)
                {
                    sbSelect.AppendFormat("SELECT {0} FROM ", sbExpression);
                }
                else
                {
                    sbSelect.AppendFormat("SELECT {0} FROM ", dataFieldPhysicalName);
                }
                if (systemTableLinks != null && systemTableLinks.Count > 0)
                {
                    mainTableName = DataAccessHandler.GetTableNames(mainTableName, systemTableLinks.Values.ToList<TableLink>());
                }
                sbSelect.Append(mainTableName);
                if (sbWhere.Length > 0)
                {
                    sbSelect.Append(" WHERE ");
                    sbSelect.Append(sbWhere.ToString());
                }
                if (!string.IsNullOrWhiteSpace(mainTableName))
                {
                    try
                    {
                        //获得系统数据库对象
                        SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sbSelect.ToString()))
                        {
                            DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                            text = DataConvertionHelper.GetConvertedString(db.ExecuteScalar(dbCommand));
                        }
                    }
                    catch { text = "计算出错"; }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return text;
        }

        /// <summary>
        /// 获得统计数据的详情
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public DataTable GetRowCellStatiscsDetail(decimal cellId, byte dataWarehouseId, IList<WhereConditon> whereConditons,
            IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            DataTable detail = null;

            try
            {
                CellStyle cellStyle = new CellStyle();
                CellConditionItem cellConditionItem = GetQueryClauses(cellId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
                if (cellConditionItem == null)
                {
                    return null;
                }

                /* 3. 创建 Select 语句 */
                StringBuilder sbSelect = new StringBuilder();
                sbSelect.AppendFormat("SELECT {0}.UserName FROM ", cellConditionItem.TableName);
                sbSelect.Append(cellConditionItem.FromClause);
                foreach (WhereConditon whereConditon in whereConditons)
                {
                    whereConditon.DataTableName = cellConditionItem.TableName;
                }
                string where = DataAccessHandler.GetWhereSentence(whereConditons);
                if (!string.IsNullOrWhiteSpace(cellConditionItem.WhereClause) || !string.IsNullOrWhiteSpace(where))
                {
                    sbSelect.Append(" WHERE ");
                    if (!string.IsNullOrWhiteSpace(cellConditionItem.WhereClause))
                    {
                        sbSelect.Append(cellConditionItem.WhereClause);
                    }
                    if (!string.IsNullOrWhiteSpace(where))
                    {
                        if (!string.IsNullOrWhiteSpace(cellConditionItem.WhereClause))
                        {
                            sbSelect.Append(" AND ");
                        }
                        sbSelect.Append(cellConditionItem.WhereClause);
                    }
                }

                try
                {
                    //获得系统数据库对象
                    SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sbSelect.ToString()))
                    {
                        detail = db.ExecuteDataSet(dbCommand).Tables[0];
                    }
                }
                catch { }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return detail;
        }

        /// <summary>
        /// 获得行单元格的统计值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public string GetRowCellStatiscsText(decimal cellId, byte dataWarehouseId, IList<WhereConditon> whereConditons,
            IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            string text = string.Empty;

            try
            {
                CellConditionItem cellConditionItem = GetQueryClauses(cellId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
                if (cellConditionItem == null)
                {
                    return string.Empty;
                }

                /* 创建 Select 语句 */
                CellStyle cellStyle = new CellStyle();
                IList<CommonNode> commonNodes = cellStyle.GetCommonNodes(cellId, CellCondition.Show);
                CustomDataField customDataField = new CustomDataField();
                StringBuilder sbExpression = new StringBuilder(cellConditionItem.DataFileNames);
                StringBuilder sbReplace = new StringBuilder();
                int index = 0;
                foreach (CommonNode node in commonNodes)
                {
                    string dataFieldPhysicalName = string.Empty;
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)node.NodeType;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.SystemPhysicalDataField:
                            SystemDataField systemDataField = (SystemDataField)node.NodeId;
                            dataFieldPhysicalName = DataFieldHelper.GetSystemLogicalDataFieldName(cellConditionItem.TableName, systemDataField);
                            break;

                        case DataFieldProperty.PhysicalDataField:
                            dataFieldPhysicalName = node.NodeCode;
                            break;

                        case DataFieldProperty.LogicalDataField:
                            dataFieldPhysicalName = customDataField.GetDataFieldLogicalExpression(node.NodeId);
                            break;
                    }
                    sbReplace.Append("{");
                    sbReplace.Append(index);
                    sbReplace.Append("}");
                    sbExpression.Replace(sbReplace.ToString(), dataFieldPhysicalName);
                    sbReplace.Clear();
                    index++;
                }
                StringBuilder sb = new StringBuilder();
                if (sb.Length > 0)
                {
                    sb.AppendFormat("SELECT {0} FROM ", sbExpression);
                }
                else
                {
                    sb.AppendFormat("SELECT COUNT({0}.RecordId) FROM ", cellConditionItem.TableName);
                }
                sb.Append(cellConditionItem.FromClause);
                foreach (WhereConditon whereConditon in whereConditons)
                {
                    whereConditon.DataTableName = cellConditionItem.TableName;
                }
                string where = DataAccessHandler.GetWhereSentence(whereConditons);
                if (!string.IsNullOrWhiteSpace(cellConditionItem.WhereClause) || !string.IsNullOrWhiteSpace(where))
                {
                    sb.Append(" WHERE ");
                    if (!string.IsNullOrWhiteSpace(cellConditionItem.WhereClause))
                    {
                        sb.Append(cellConditionItem.WhereClause);
                    }
                    if (!string.IsNullOrWhiteSpace(where))
                    {
                        if (!string.IsNullOrWhiteSpace(cellConditionItem.WhereClause))
                        {
                            sb.Append(" AND ");
                        }
                        sb.Append(cellConditionItem.WhereClause);
                    }
                }
                try
                {
                    //获得系统数据库对象
                    SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        text = DataConvertionHelper.GetConvertedString(db.ExecuteScalar(dbCommand));
                    }
                }
                catch { text = "计算出错"; }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return text;
        }

        /// <summary>
        /// 获得单元格的统计值的详情（含自定义字段）
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public DataTable GetCellDetail(decimal cellId, byte dataWarehouseId, IList<decimal> dataFieldIds, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            DataTable detail = null;

            try
            {
                CellStyle cellStyle = new CellStyle();
                CellConditionItem cellConditionItem = GetQueryClauses(cellId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
                if (cellConditionItem == null)
                {
                    return null;
                }
                string sourceDatabaseName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);
                IList<decimal> dataTableIds = new List<decimal>();
                IList<string> dataFieldCaptions = new List<string>();
                CustomTable customTable = new CustomTable();
                CustomDataField customDataField = new CustomDataField();
                /* 创建 Select 语句 */
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT {0}.UserName, UserAccount.UserActualName, CustomDepartment.DepName, UserType.UserTypeName", cellConditionItem.TableName);                 
                foreach (decimal dataFieldId in dataFieldIds)
                {
                    CustomDataFieldInfo customDataFieldInfo = customDataField.GetModelInfo(dataFieldId);

                    string dataFieldPhysicalName = string.Empty;
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.SystemPhysicalDataField:
                            //SystemDataField systemDataField = (SystemDataField)customDataFieldInfo.;
                            //dataFieldPhysicalName = DataFieldHelper.GetSystemLogicalDataFieldName(cellConditionItem.TableName, systemDataField);
                            break;

                        case DataFieldProperty.PhysicalDataField:
                            dataFieldPhysicalName = customDataFieldInfo.PhysicalName;
                            sb.AppendFormat(", {0}", customDataFieldInfo.PhysicalName);
                            break;

                        case DataFieldProperty.LogicalDataField:
                            dataFieldPhysicalName = customDataField.GetDataFieldLogicalExpression(dataFieldId);
                            sb.AppendFormat(", {0} AS {1}", dataFieldPhysicalName, customDataFieldInfo.PhysicalName);
                            break;
                    }
                    if (!dataTableIds.Contains(customDataFieldInfo.TableId))
                    {
                        dataTableIds.Add(customDataFieldInfo.TableId);
                    }
                    dataFieldCaptions.Add(customDataFieldInfo.LogicalName);
                }

                sb.Append(" FROM UserAccount INNER JOIN CustomDepartment ON UserAccount.DepId = CustomDepartment.DepId ");
                sb.Append("INNER JOIN UserType ON UserAccount.UserTypeId = UserType.UserTypeId ");
                foreach (string tableName in cellConditionItem.TableNames)
                {
                    sb.AppendFormat("INNER JOIN {0}.dbo.{1} ON {1}.UserId = UserAccount.UserId ",
                        sourceDatabaseName, tableName);
                }
                foreach (decimal dataTableId in dataTableIds)
                {
                    string tablePhysicalName = customTable.GetTablePhysicalName(dataTableId);
                    if (!tablePhysicalName.Equals(cellConditionItem.TableName, StringComparison.InvariantCultureIgnoreCase))
                    {
                        byte customDataWarehouseId = customTable.GetDataWarehouseId(dataTableId);
                        string databaseName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)customDataWarehouseId);
                        sb.AppendFormat("LEFT JOIN {0}.dbo.{1} ON {1}.UserId = {2}.UserId ", databaseName, tablePhysicalName, 
                            cellConditionItem.TableName);
                    }
                }
                sb.AppendFormat("WHERE {0}", cellConditionItem.WhereClause);
                try
                {
                    //获得系统数据库对象
                    SqlDatabase db = DataAccessHelper.GetDatabase();
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        detail = db.ExecuteDataSet(dbCommand).Tables[0];
                    }
                    detail.Columns[0].Caption = "用户名";
                    detail.Columns[1].Caption = "姓名";
                    detail.Columns[2].Caption = "单位名称";
                    detail.Columns[3].Caption = "用户类型";
                    for (int i = 4; i < detail.Columns.Count; i++)
                    {
                        detail.Columns[i].Caption = dataFieldCaptions[i-4];
                    }
                }
                catch { }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return detail;
        }

        /// <summary>
        /// 获得单元格的统计值的详情
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public DataTable GetCellDetail(decimal cellId, byte dataWarehouseId, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            DataTable detail = null;

            try
            {
                CellStyle cellStyle = new CellStyle();
                CellConditionItem cellConditionItem = GetQueryClauses(cellId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
                if (cellConditionItem == null)
                {
                    return null;
                }
                string sourceDatabaseName = DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId);

                /* 创建 Select 语句 */
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("SELECT {0}.UserName, UserAccount.UserActualName, CustomDepartment.DepName, UserType.UserTypeName FROM UserAccount ", cellConditionItem.TableName);
                sb.Append("INNER JOIN CustomDepartment ON UserAccount.DepId = CustomDepartment.DepId ");
                sb.Append("INNER JOIN UserType ON UserAccount.UserTypeId = UserType.UserTypeId ");
                foreach (string tableName in cellConditionItem.TableNames)
                {
                    sb.AppendFormat("INNER JOIN {0}.dbo.{1} ON {1}.UserId = UserAccount.UserId ",
                        sourceDatabaseName, tableName);
                }
                if (!string.IsNullOrWhiteSpace(cellConditionItem.WhereClause))
                {
                    sb.AppendFormat("WHERE {0}", cellConditionItem.WhereClause);
                }

                try
                {
                    //获得系统数据库对象
                    SqlDatabase db = DataAccessHelper.GetDatabase();
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        detail = db.ExecuteDataSet(dbCommand).Tables[0];
                    }
                }
                catch { }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return detail;
        }

        /// <summary>
        /// 获得单元格的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="dataWarehouseId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        public TextIntValue GetCellText(decimal cellId, byte dataWarehouseId, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            string text = string.Empty;
            int digit = 0;

            try
            {
                CellStyle cellStyle = new CellStyle();
                CellConditionItem cellConditionItem = GetQueryClauses(cellId, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
                if (cellConditionItem == null)
                {
                    return new TextIntValue(text, digit);
                }
                /* 创建 Select 语句 */
                IList<CommonNode> commonNodes = cellStyle.GetCommonNodes(cellId, CellCondition.Show);
                CustomDataField customDataField = new CustomDataField();
                StringBuilder sbExpression = new StringBuilder(cellConditionItem.DataFileNames);
                StringBuilder sbReplace = new StringBuilder();                
                int index = 0;
                foreach (CommonNode node in commonNodes)
                {
                    string dataFieldPhysicalName = string.Empty;
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)node.NodeType;
                    switch (dataFieldProperty)
                    {
                        case DataFieldProperty.SystemPhysicalDataField:
                            SystemDataField systemDataField = (SystemDataField)node.NodeId;
                            dataFieldPhysicalName = DataFieldHelper.GetSystemLogicalDataFieldName(cellConditionItem.TableName, systemDataField);
                            break;

                        case DataFieldProperty.PhysicalDataField:
                            dataFieldPhysicalName = node.NodeCode;
                            break;

                        case DataFieldProperty.LogicalDataField:
                            dataFieldPhysicalName = customDataField.GetDataFieldLogicalExpression(node.NodeId);
                            break;
                    }
                    sbReplace.Append("{");
                    sbReplace.Append(index);
                    sbReplace.Append("}");
                    sbExpression.Replace(sbReplace.ToString(), dataFieldPhysicalName);
                    sbReplace.Clear();
                    index++;
                }
                StringBuilder sb = new StringBuilder();
                if (sbExpression.Length > 0)
                {
                    digit = 2;
                    sb.AppendFormat("SELECT {0} FROM ", sbExpression);
                }
                else
                {
                    digit = 0;
                    sb.AppendFormat("SELECT COUNT({0}.RecordId) FROM ", cellConditionItem.TableName);
                }
                sb.Append(cellConditionItem.FromClause);
                if (!string.IsNullOrWhiteSpace(cellConditionItem.WhereClause))
                {
                    sb.AppendFormat(" WHERE {0}", cellConditionItem.WhereClause);
                }
                try
                {
                    //获得系统数据库对象
                    SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        text = DataConvertionHelper.GetConvertedString(db.ExecuteScalar(dbCommand));
                    }
                }
                catch { text = "计算出错"; }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return new TextIntValue(text, digit);
        }

        /// <summary>
        /// 获得单元格的值
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <returns></returns>
        public string GetCellText(decimal cellId, decimal userId, decimal tableId, string condition, string templateText)
        {
            string text = string.Empty;

            CellStyle cellStyle = new CellStyle();
            IList<CommonNode> commonNodes = cellStyle.GetCommonNodes(cellId, CellCondition.Show);
            if (commonNodes.Count == 0)
            {
                return "未设置显示字段。";
            }

            Dictionary<string, CustomDataFieldInfo> customDataFieldInfos = new Dictionary<string, CustomDataFieldInfo>();
            CustomTable customTable = new CustomTable();
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);            
            CustomDataField customDataField = new CustomDataField();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            foreach (var commonNode in commonNodes)
            {
                CustomDataFieldInfo customDataFieldInfo = null;
                string key = string.Empty;
                if ((DataFieldProperty)commonNode.NodeType == DataFieldProperty.SystemPhysicalDataField)
                {
                    SystemDataField systemDataField = (SystemDataField)commonNode.NodeId;
                    customDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(commonNode.ParentNodeId, tablePhysicalName, systemDataField);
                    key = string.Format("{0}_{1}", tableId, customDataFieldInfo.DataFieldId);
                }
                else
                {
                    customDataFieldInfo = customDataField.GetModelInfo(commonNode.NodeId);
                    key = customDataFieldInfo.DataFieldId.ToString();
                }
                customDataFieldInfos.Add(key, customDataFieldInfo);
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                    case DataFieldProperty.PhysicalDataField:
                        sb.AppendFormat("{0}, ", customDataFieldInfo.PhysicalName);
                        break;

                    case DataFieldProperty.LogicalDataField:
                        sb.AppendFormat("{0}, ", customDataField.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId));
                        break;
                }
            }
            sb.Remove(sb.Length - 2, 2);
            IList<CommonNode> systemCommonNodes = cellStyle.GetSystemCommonNodes(cellId);
            Dictionary<string, TableLink> systemTableLinks = GetSystemTableLinks(tablePhysicalName, systemCommonNodes);
            if (systemTableLinks != null && systemTableLinks.Count > 0)
            {
               string physicalName = DataAccessHandler.GetTableNames(tablePhysicalName, systemTableLinks.Values.ToList<TableLink>());
                sb.AppendFormat(" FROM {0} WHERE {1}.UserId = @UserId ", physicalName, tablePhysicalName);
            }
            else
            {
                sb.AppendFormat(" FROM {0} WHERE UserId = @UserId ", tablePhysicalName);
            }
            if (!string.IsNullOrWhiteSpace(condition))
            {
                sb.AppendFormat("AND ({0}) ", condition);
            }
            try
            {
                StringBuilder sbText = new StringBuilder();
                //获得系统数据库对象
                byte dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                bool empty = true;
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            empty = false;
                            for (int i = 0; i < commonNodes.Count; i++)
                            {
                                if (dataReader[i] == null || dataReader[i] == DBNull.Value)
                                {
                                    continue;
                                }
                                CustomDataFieldInfo customDataFieldInfo = null;
                                string key = string.Empty;
                                if ((DataFieldProperty)commonNodes[i].NodeType == DataFieldProperty.SystemPhysicalDataField)
                                {
                                    key = string.Format("{0}_{1}", tableId, commonNodes[i].NodeId);
                                }
                                else
                                {
                                    key = commonNodes[i].NodeId.ToString();
                                }
                                customDataFieldInfo = customDataFieldInfos[key];
                                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                                string value = string.Empty;
                                switch (dataFieldProperty)
                                {
                                    case DataFieldProperty.LogicalDataField:
                                        value = DataConvertionHelper.GetConvertedString(dataReader[i]);
                                        break;

                                    case DataFieldProperty.PhysicalDataField:
                                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                                        switch (physicalDataFieldType)
                                        {
                                            case PhysicalDataFieldType.Boolean:
                                                value = DataConvertionHelper.GetBoolean(dataReader[i]) ? "是" : "否";
                                                break;

                                            case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                                                value = DataConvertionHelper.GetDateTime(dataReader[i]).ToString("G");
                                                break;

                                            case PhysicalDataFieldType.YearAndMonthAndDay:
                                                value = DataConvertionHelper.GetDateTime(dataReader[i]).ToString("yyyy-MM-dd");
                                                break;

                                            case PhysicalDataFieldType.YearAndMonth:
                                                value = DataConvertionHelper.GetDateTime(dataReader[i]).ToString("y");
                                                break;

                                            case PhysicalDataFieldType.MonthAndDay:
                                                value = DataConvertionHelper.GetDateTime(dataReader[i]).ToString("m");
                                                break;

                                            case PhysicalDataFieldType.Time:
                                                value = DataConvertionHelper.GetDateTime(dataReader[i]).ToString("HH:mm:ss");
                                                break;

                                            default:
                                                value = DataConvertionHelper.GetConvertedString(dataReader[i]);
                                                break;
                                        }
                                        break;

                                    case DataFieldProperty.SystemPhysicalDataField:
                                        SystemDataField systemDataField = (SystemDataField)customDataFieldInfo.DataFieldId;
                                        switch (systemDataField)
                                        {
                                            case SystemDataField.CurrentState:
                                                value = UserEnumHelper.GetEnumText((CurrentState)DataConvertionHelper.GetByte(dataReader[i]));
                                                break;

                                            case SystemDataField.AuditedStatus:
                                                value = UserEnumHelper.GetEnumText((AuditedStatus)DataConvertionHelper.GetByte(dataReader[i]));
                                                break;
                                                
                                            default:
                                                value = DataConvertionHelper.GetConvertedString(dataReader[i]);
                                                break;
                                        }
                                        break;
                                }
                                if (!string.IsNullOrWhiteSpace(templateText))
                                {
                                    templateText = templateText.Replace(string.Format("{{{0}}}", i), value);
                                }
                                else
                                {
                                    sbText.AppendFormat("{0} ", value);
                                }
                            }
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
                if (!empty)
                {
                    if (!string.IsNullOrWhiteSpace(templateText))
                    {
                        text = templateText;
                    }
                    else
                    {
                        text = sbText.ToString();
                    }
                }
            }
            catch
            {
                text = "错误单元格！";
            }

            return text;
        }

        /// <summary>
        /// 获得扩展行列数据
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="condition"></param>
        /// <param name="templateText"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public DataSet GetExtendRowColData(decimal cellId, decimal userId, decimal tableId, string condition)
        {
            DataSet ds = null;

            CustomCellInfo customCellInfo = GetModelInfo(cellId);
            CellStyle cellStyle = new CellStyle();
            IList<CommonNode> commonNodes = cellStyle.GetCommonNodes(cellId, CellCondition.Show);
            if (commonNodes.Count == 0)
            {
                return null;
            }
            Dictionary<string, CustomDataFieldInfo> customDataFieldInfos = new Dictionary<string, CustomDataFieldInfo>();
            CustomTable customTable = new CustomTable();
            string tablePhysicalName = customTable.GetTablePhysicalName(tableId);
            CustomDataField customDataField = new CustomDataField();
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            foreach (var commonNode in commonNodes)
            {
                CustomDataFieldInfo customDataFieldInfo = null;
                string key = string.Empty;
                if ((DataFieldProperty)commonNode.NodeType == DataFieldProperty.SystemPhysicalDataField)
                {
                    SystemDataField systemDataField = (SystemDataField)commonNode.NodeId;
                    customDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(commonNode.ParentNodeId, tablePhysicalName, systemDataField);
                    key = string.Format("{0}_{1}", customDataFieldInfo.TableId, customDataFieldInfo.DataFieldId);
                }
                else
                {
                    customDataFieldInfo = customDataField.GetModelInfo(commonNode.NodeId);
                    key = customDataFieldInfo.DataFieldId.ToString();
                }
                customDataFieldInfos.Add(key, customDataFieldInfo);
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                    case DataFieldProperty.PhysicalDataField:
                        sb.AppendFormat("{0}, ", customDataFieldInfo.PhysicalName);
                        break;

                    case DataFieldProperty.LogicalDataField:
                        sb.AppendFormat("{0}, ", customDataField.GetDataFieldLogicalExpression(customDataFieldInfo.DataFieldId));
                        break;
                }
            }
            sb.Remove(sb.Length - 2, 2);
            IList<CommonNode> systemCommonNodes = cellStyle.GetSystemCommonNodes(cellId);
            Dictionary<string, TableLink> systemTableLinks = GetSystemTableLinks(tablePhysicalName, systemCommonNodes);
            if (systemTableLinks != null && systemTableLinks.Count > 0)
            {
                string physicalName = DataAccessHandler.GetTableNames(tablePhysicalName, systemTableLinks.Values.ToList<TableLink>());
                sb.AppendFormat(" FROM {0} ", physicalName);
            }
            else
            {
                sb.AppendFormat(" FROM {0} ", tablePhysicalName);
            }
            if ((userId > 0) || !string.IsNullOrWhiteSpace(condition))
            {
                sb.Append("WHERE ");
                if (userId > 0)
                {
                    sb.Append("UserId = @UserId ");
                }
                if (!string.IsNullOrWhiteSpace(condition))
                {
                    if (userId > 0)
                    {
                        sb.Append("AND ");
                    }
                    sb.AppendFormat("({0}) ", condition);
                }
            }
            sb.AppendFormat("ORDER BY {0}.RecordSorting", tablePhysicalName);
            try
            {
                StringBuilder sbText = new StringBuilder();
                //获得系统数据库对象
                int dataWarehouseId = customTable.GetDataWarehouseId(tableId);
                SqlDatabase db = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    if (userId > 0)
                    {
                        db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
                    }
                    ds = db.ExecuteDataSet(dbCommand);
                }
            }
            catch { }

            return ds;
        }

        /// <summary>
        /// 获得单元格列表
        /// </summary>
        /// <param name="sheetId"></param>
        /// <returns></returns>
        public IList<CustomCellInfo> GetCustomCellInfos(decimal sheetId)
        {
            //创建集合对象
            IList<CustomCellInfo> customSheetStyleInfos = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("SheetId", "SheetId", DbType.Decimal, sheetId,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));

            customSheetStyleInfos = GetModelInfos(whereConditons, null);

            return customSheetStyleInfos;
        }

        /// <summary>
        /// 获得单元格列表
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="cellType"></param>
        /// <returns></returns>
        public IList<CustomCellInfo> GetCustomCellInfos(decimal sheetId, byte cellType)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("SheetId", "SheetId", DbType.Decimal, sheetId,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("CellType", "CellType", DbType.Byte, (byte)cellType,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            return GetModelInfos(whereConditons, null);
        }

        /// <summary>
        /// 批量保存单元格的字段条件
        /// </summary>
        /// <param name="cellStyleIds"></param>
        /// <param name="tableId"></param>
        /// <param name="reportCellType"></param>
        /// <param name="number"></param>
        /// <param name="dataFieldConditions"></param>
        public void SaveDataFieldCondition(IList<decimal> cellStyleIds, decimal tableId, byte cellType, int number, IList<CommonNode> dataFieldConditions)
        {
            if (cellStyleIds.Count == 0)
            {
                throw new ArgumentNullException("参数错误");
            }
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    BasicCellType basicCellType = (BasicCellType)cellType;
                    StringBuilder update = new StringBuilder();
                    update.Append("UPDATE CustomCell SET TableId = @TableId, CellType = @CellType ");
                    switch (basicCellType)
                    {
                        case BasicCellType.ExtendRow:
                        case BasicCellType.ExtendRowByCondtion:
                            update.Append(", ExtendRows = @ExtendRows ");
                            break;

                        case BasicCellType.ExtendCol:
                        case BasicCellType.ExtendColByCondtion:
                            update.Append(", ExtendCols = @ExtendCols ");
                            break;
                    }
                    update.Append("WHERE ");
                    for (int i = 0; i < cellStyleIds.Count; i++)
                    {
                        update.Append(string.Format("CellId = @CellStyleId_{0} OR ", i));
                    }
                    update.Remove(update.Length - 4, 4);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(update.ToString()))
                    {
                        //给参数赋值
                        for (int i = 0; i < cellStyleIds.Count; i++)
                        {
                            db.AddInParameter(dbCommand, string.Format("CellStyleId_{0}", i), DbType.Decimal, DataConvertionHelper.SetDecimal(cellStyleIds[i]));
                        }
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                        db.AddInParameter(dbCommand, "CellType", DbType.Byte, cellType);
                        switch (basicCellType)
                        {
                            case BasicCellType.ExtendRow:
                            case BasicCellType.ExtendRowByCondtion:
                                db.AddInParameter(dbCommand, "ExtendRows", DbType.Byte, DataConvertionHelper.SetInt(number));
                                break;

                            case BasicCellType.ExtendCol:
                            case BasicCellType.ExtendColByCondtion:
                                db.AddInParameter(dbCommand, "ExtendCols", DbType.Byte, DataConvertionHelper.SetInt(number));
                                break;

                        }
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }
                    CellStyle cellStyle = new CellStyle();
                    /* 1. 单元格字段条件  */
                    cellStyle.Delete(cellStyleIds, CellCondition.Show, db, transaction);
                    foreach (decimal cellId in cellStyleIds)
                    {
                        for (int i = 0; i < dataFieldConditions.Count; i++)
                        {
                            DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldConditions[i].NodeType;
                            int sorting = DataConvertionHelper.GetConvertedInt(dataFieldConditions[i].ParentNodeId);
                            if (dataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
                            {
                                cellStyle.Insert(new CellStyleInfo(0, decimal.MinValue, cellId, (byte)CellCondition.Show, 0, DataConvertionHelper.GetConvertedByte(dataFieldConditions[i].NodeId), sorting), db, transaction);
                            }
                            else
                            {
                                cellStyle.Insert(new CellStyleInfo(0, dataFieldConditions[i].NodeId, cellId, (byte)CellCondition.Show, 0, byte.MinValue, sorting), db, transaction);
                            }
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
        /// 保存单元格的字段条件
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="tableId"></param>
        /// <param name="cellType"></param>
        /// <param name="number"></param>
        /// <param name="dataFieldConditions"></param>
        public void SaveDataFieldCondition(decimal cellId, decimal tableId, byte cellType, int number, IList<CommonNode> dataFieldConditions)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    BasicCellType basicCellType = (BasicCellType)cellType;
                    StringBuilder update = new StringBuilder();
                    update.Append("UPDATE CustomCell SET TableId = @TableId, CellType = @CellType ");
                    switch (basicCellType)
                    {
                        case BasicCellType.ExtendRow:
                        case BasicCellType.ExtendRowByCondtion:
                            update.Append(", ExtendRows = @ExtendRows ");
                            break;

                        case BasicCellType.ExtendCol:
                        case BasicCellType.ExtendColByCondtion:
                            update.Append(", ExtendCols = @ExtendCols ");
                            break;
                    }
                    update.Append("WHERE CellId = @CellId");
                    using (DbCommand dbCommand = db.GetSqlStringCommand(update.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "CellId", DbType.Decimal, cellId);
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, tableId);
                        db.AddInParameter(dbCommand, "CellType", DbType.Byte, cellType);
                        switch (basicCellType)
                        {
                            case BasicCellType.ExtendRow:
                            case BasicCellType.ExtendRowByCondtion:
                                db.AddInParameter(dbCommand, "ExtendRows", DbType.Byte, DataConvertionHelper.SetInt(number));
                                break;

                            case BasicCellType.ExtendCol:
                            case BasicCellType.ExtendColByCondtion:
                                db.AddInParameter(dbCommand, "ExtendCols", DbType.Byte, DataConvertionHelper.SetInt(number));
                                break;

                        }
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }
                    CellStyle cellStyle = new CellStyle();
                    /* 1. 单元格字段条件  */
                    cellStyle.Delete(cellId, CellCondition.Condition, db, transaction);
                    for (int i = 0; i < dataFieldConditions.Count; i++)
                    {
                        DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldConditions[i].NodeType;
                        int sorting = DataConvertionHelper.GetConvertedInt(dataFieldConditions[i].ParentNodeId);
                        if (dataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
                        {
                            cellStyle.Insert(new CellStyleInfo(0, decimal.MinValue, cellId, (byte)CellCondition.Condition, 0, DataConvertionHelper.GetConvertedByte(dataFieldConditions[i].NodeId), sorting), db, transaction);
                        }
                        else
                        {
                            cellStyle.Insert(new CellStyleInfo(0, dataFieldConditions[i].NodeId, cellId, (byte)CellCondition.Condition, 0, byte.MinValue, sorting), db, transaction);
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
        /// 保存单元格的字段条件
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="tableId"></param>
        /// <param name="condition"></param>
        /// <param name="template"></param>
        /// <param name="cellType"></param>
        /// <param name="number"></param>
        /// <param name="dataFieldConditions"></param>
        /// <param name="dataFieldShows"></param>
        public void SaveDataFieldCondition(decimal cellId, decimal tableId, string condition, string template,
            byte cellType, int number, IList<CommonNode> dataFieldConditions, IList<CommonNode> dataFieldShows)
        {
            BasicCellType basicCellType = (BasicCellType)cellType;
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                   
                    StringBuilder sb = new StringBuilder();
                    sb.Append("UPDATE CustomCell SET TableId = @TableId, ConditionText = @ConditionText, TemplateText = @TemplateText, CellType = @CellType");
                        switch (basicCellType)
                        {
                            case BasicCellType.ExtendRow:
                            case BasicCellType.ExtendRowByCondtion:
                                sb.Append(", ExtendRows = @ExtendRows");
                                break;
                                
                            case BasicCellType.ExtendCol:
                            case BasicCellType.ExtendColByCondtion:
                                sb.Append(", ExtendCols = @ExtendCols");
                                break;
                        }                    
                    sb.Append(" WHERE CellId = @CellId");
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "CellId", DbType.Decimal, DataConvertionHelper.SetDecimal(cellId));
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(tableId));
                        db.AddInParameter(dbCommand, "ConditionText", DbType.String, condition);
                        db.AddInParameter(dbCommand, "TemplateText", DbType.String, template);
                        db.AddInParameter(dbCommand, "CellType", DbType.Byte, DataConvertionHelper.SetByte(cellType));
                        
                            switch (basicCellType)
                            {
                                case BasicCellType.ExtendRow:
                                case BasicCellType.ExtendRowByCondtion:
                                    db.AddInParameter(dbCommand, "ExtendRows", DbType.Byte, DataConvertionHelper.SetInt(number));
                                    break;

                                case BasicCellType.ExtendCol:
                                case BasicCellType.ExtendColByCondtion:
                                    db.AddInParameter(dbCommand, "ExtendCols", DbType.Byte, DataConvertionHelper.SetInt(number));
                                    break;
                            }
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }
                    CellStyle cellStyle = new CellStyle();
                    IList<CommonNode> conditionalCommonNodes = cellStyle.GetCommonNodes(cellId, CellCondition.Condition);
                    IList<CommonNode> showedCommonNodes = cellStyle.GetCommonNodes(cellId, CellCondition.Show);
                    bool delete = false;
                    /* 1. 单元格字段条件  */
                    if (conditionalCommonNodes.Count == dataFieldConditions.Count)
                    {
                        for (int i = 0; i < conditionalCommonNodes.Count; i++)
                        {
                            /* 系统字段还是普通的自定义字段*/
                            if (conditionalCommonNodes[i].NodeId == dataFieldConditions[i].NodeId)
                            {
                                delete = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        delete = true;
                    }
                    if (delete)
                    {
                        if (conditionalCommonNodes.Count > 0)
                        {
                            cellStyle.Delete(cellId, CellCondition.Condition, db, transaction);
                        }
                        for (int i = 0; i < dataFieldConditions.Count; i++)
                        {
                            DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldConditions[i].NodeType;
                            if (dataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
                            {
                                cellStyle.Insert(new CellStyleInfo(0, decimal.MinValue, cellId, (byte)CellCondition.Condition, 0, DataConvertionHelper.GetConvertedByte(dataFieldConditions[i].NodeId), i), db, transaction);
                            }
                            else
                            {
                                cellStyle.Insert(new CellStyleInfo(0, dataFieldConditions[i].NodeId, cellId, (byte)CellCondition.Condition, 0, byte.MinValue, i), db, transaction);
                            }
                        }
                    }

                    /* 2. 单元格字段显示  */
                    delete = false;
                    if (showedCommonNodes.Count == dataFieldShows.Count)
                    {
                        for (int i = 0; i < showedCommonNodes.Count; i++)
                        {
                            if (showedCommonNodes[i].NodeId != dataFieldShows[i].NodeId)
                            {
                                delete = true;
                                break;
                            }
                        }
                    }
                    else
                    {
                        delete = true;
                    }
                    if (delete)
                    {
                        if (showedCommonNodes.Count > 0)
                        {
                            cellStyle.Delete(cellId, CellCondition.Show, db, transaction);
                        }
                        for (int i = 0; i < dataFieldShows.Count; i++)
                        {
                            DataFieldProperty dataFieldProperty = (DataFieldProperty)dataFieldShows[i].NodeType;                          
                            if (dataFieldProperty == DataFieldProperty.SystemPhysicalDataField)
                            {
                                cellStyle.Insert(new CellStyleInfo(0, decimal.MinValue, cellId, (byte)CellCondition.Show, 0, DataConvertionHelper.GetConvertedByte(dataFieldShows[i].NodeId), i), db, transaction);
                            }
                            else
                            {
                                cellStyle.Insert(new CellStyleInfo(0, dataFieldShows[i].NodeId, cellId, (byte)CellCondition.Show, 0, byte.MinValue, i), db, transaction);
                            }
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
        /// 清除单元格的字段条件
        /// </summary>
        /// <param name="cellId"></param>
        public void ClearDataFieldCondition(decimal cellId)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    CellStyle cellStyle = new CellStyle();
                    cellStyle.Delete(cellId, db, transaction);
                    string update = "UPDATE CustomCell SET TableId = NULL, ConditionText = NULL, TemplateText = NULL, CellType = 0, ExtendRows = NULL, ExtendCols = NULL WHERE CellId = @CellId";
                    using (DbCommand dbCommand = db.GetSqlStringCommand(update))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "CellId", DbType.Decimal, DataConvertionHelper.SetDecimal(cellId));
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
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
        /// 是否包含行列扩展的数据单元格
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="cellRowAndCol"></param>
        /// <param name="insertOrDelete"></param>
        /// <returns></returns>
        public bool IncludeExtendDataCell(decimal sheetId, int start, int count, CellRowAndCol cellRowAndCol, bool insertOrDelete)
        {
            bool include = false;

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CellId, RowIndex, ColIndex, ExtendRows, ExtendCols FROM CustomCell ");
            sb.Append("WHERE SheetId = @SheetId AND (CellType = @CellType_0 OR CellType = @CellType_1)");
            CellStyle cellStyle = new CellStyle();
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                /* 1. 是否包括行扩展的数据单元格 */
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, sheetId);
                    db.AddInParameter(dbCommand, "CellType_0", DbType.Byte, (byte)BasicCellType.ExtendRow);
                    db.AddInParameter(dbCommand, "CellType_1", DbType.Byte, (byte)BasicCellType.ExtendRowByCondtion);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal cellId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            int row = DataConvertionHelper.GetInt(dataReader[1]);
                            int col = DataConvertionHelper.GetInt(dataReader[2]);
                            int extendRows = DataConvertionHelper.GetInt(dataReader[3]);
                            int cols = cellStyle.GetCommonNodes(cellId, CellCondition.Show).Count;
                            switch (cellRowAndCol)
                            {
                                case CellRowAndCol.Row:
                                    if (insertOrDelete)
                                    {
                                        row++;
                                    }
                                    if (start > (row + extendRows - 1) || (start + count - 1) < row)
                                    {
                                        include = false;
                                    }
                                    else
                                    {
                                        include = true;
                                        break;
                                    }
                                    break;

                                case CellRowAndCol.Col:
                                    if (insertOrDelete)
                                    {
                                        col++;
                                    }
                                    if (start > (col + cols - 1) || (start + count - 1) < col)
                                    {
                                        include = false;
                                    }
                                    else
                                    {
                                        include = true;
                                        break;
                                    }
                                    break;
                            }
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }

                /* 2. 是否包括列扩展的数据单元格 */
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, DataConvertionHelper.SetDecimal(sheetId));
                    db.AddInParameter(dbCommand, "CellType_0", DbType.Byte, (byte)BasicCellType.ExtendCol);
                    db.AddInParameter(dbCommand, "CellType_1", DbType.Byte, (byte)BasicCellType.ExtendColByCondtion);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal cellId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            int row = DataConvertionHelper.GetInt(dataReader[1]);
                            int col = DataConvertionHelper.GetInt(dataReader[2]);
                            int extendCols = DataConvertionHelper.GetInt(dataReader[4]);
                            int rows = cellStyle.GetCommonNodes(cellId, CellCondition.Show).Count;
                            switch (cellRowAndCol)
                            {
                                case CellRowAndCol.Row:
                                    if (insertOrDelete)
                                    {
                                        row++;
                                    }
                                    if (start > (row + rows - 1) || (start + count - 1) < row)
                                    {
                                        include = false;
                                    }
                                    else
                                    {
                                        include = true;
                                        break;
                                    }
                                    break;

                                case CellRowAndCol.Col:
                                    if (insertOrDelete)
                                    {
                                        col++;
                                    }
                                    if (start > (col + extendCols - 1) || (start + count - 1) < col)
                                    {
                                        include = false;
                                    }
                                    else
                                    {
                                        include = true;
                                        break;
                                    }
                                    break;
                            }
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

            switch (cellRowAndCol)
            {
                case CellRowAndCol.Row:
                    sb.Append("RowIndex >= @StartRow AND RowIndex < @EndRow");
                    break;

                case CellRowAndCol.Col:
                    sb.Append("ColIndex >= @StartCol AND ColIndex < @EndCol ");
                    break;
            }

            return include;
        }

        /// <summary>
        /// 是否包含数据单元格
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <returns></returns>
        public bool IncludeDataCell(decimal sheetId, int row, int col, int rowCount, int colCount)
        {
            bool include = false;

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT count(1) FROM CustomCell WHERE SheetId = @SheetId AND ");
            sb.Append("RowIndex >= @StartRow AND RowIndex < @EndRow AND ColIndex >= @StartCol AND ColIndex < @EndCol ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, sheetId);
                    db.AddInParameter(dbCommand, "StartRow", DbType.Int32, row);
                    db.AddInParameter(dbCommand, "EndRow", DbType.Int32, (row + rowCount));
                    db.AddInParameter(dbCommand, "StartCol", DbType.Int32, col);
                    db.AddInParameter(dbCommand, "EndCol", DbType.Int32, (col + colCount));
                    int count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                    if (count > 0)
                    {
                        include = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return include;
        }

        /// <summary>
        /// 是否包含数据单元格
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="cellRowAndCol"></param>
        /// <returns></returns>
        public bool IncludeDataCell(decimal sheetId, int start, int count, CellRowAndCol cellRowAndCol)
        {
            bool include = false;

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT count(1) FROM CustomCell WHERE SheetId = @SheetId AND ");
            switch (cellRowAndCol)
            {
                case CellRowAndCol.Row:
                    sb.Append("RowIndex >= @StartRow AND RowIndex < @EndRow");
                    break;

                case CellRowAndCol.Col:
                    sb.Append("ColIndex >= @StartCol AND ColIndex < @EndCol ");
                    break;
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, DataConvertionHelper.SetDecimal(sheetId));
                    switch (cellRowAndCol)
                    {
                        case CellRowAndCol.Row:
                            db.AddInParameter(dbCommand, "StartRow", DbType.Int32, start);
                            db.AddInParameter(dbCommand, "EndRow", DbType.Int32, start + count);
                            break;

                        case CellRowAndCol.Col:
                            db.AddInParameter(dbCommand, "StartCol", DbType.Int32, start);
                            db.AddInParameter(dbCommand, "EndCol", DbType.Int32, start + count);
                            break;
                    }
                    int result = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                    if (result > 0)
                    {
                        include = true;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return include;
        }

        /// <summary>
        /// 更新数据单元格的行与列
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="start"></param>
        /// <param name="count"></param>
        /// <param name="cellRowAndCol"></param>
        public void UpdateDataCellRowAndCol(decimal sheetId, int start, int count, CellRowAndCol cellRowAndCol)
        {

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomCell SET ");
            switch (cellRowAndCol)
            {
                case CellRowAndCol.Row:
                    sb.Append("RowIndex = RowIndex + @RowCount WHERE SheetId = @SheetId AND RowIndex >= @RowIndex");
                    break;

                case CellRowAndCol.Col:
                    sb.Append("ColIndex = ColIndex + @ColCount WHERE SheetId = @SheetId AND ColIndex >= @ColIndex");
                    break;
            }
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, sheetId);
                    switch (cellRowAndCol)
                    {
                        case CellRowAndCol.Row:
                            db.AddInParameter(dbCommand, "RowIndex", DbType.Int32, start);
                            db.AddInParameter(dbCommand, "RowCount", DbType.Int32, count);
                            break;

                        case CellRowAndCol.Col:
                            db.AddInParameter(dbCommand, "ColIndex", DbType.Int32, start);
                            db.AddInParameter(dbCommand, "ColCount", DbType.Int32, count);
                            break;
                    }
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
        /// 更新 CustomCellInfo 对象的条件
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="conditionText"></param>
        public void Update(decimal cellId, string conditionText)
        {
            //生成更新语句
            string update = "UPDATE CustomCell SET ConditionText = @ConditionText WHERE CellId = @CellId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(update))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CellId", DbType.Decimal, cellId);
                    db.AddInParameter(dbCommand, "ConditionText", DbType.String, conditionText);
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
            sb.Append("DELETE CellStyle FROM CellStyle INNER JOIN CustomCell ");
            sb.Append("ON CellStyle.CellId = CustomCell.CellId ");
            sb.Append("WHERE CustomCell.TableId = @TableId ");
            sb.Append("DELETE FROM CustomCell ");
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
        /// 向 CustomCell 表中插入一条新记录
        /// </summary>
        /// <param name="customCellInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public decimal Insert(CustomCellInfo customCellInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal customCellId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomCell(TableId, SheetId, RowIndex, ColIndex, CellType, CellProperty, ");
            sb.Append("ConditionText, TemplateText, ExtendRows, ExtendCols)");
            sb.Append("VALUES (@TableId, @SheetId, @RowIndex, @ColIndex, @CellType, @CellProperty, ");
            sb.Append("@ConditionText, @TemplateText, @ExtendRows, @ExtendCols);");
            sb.Append("SET @CellId = SCOPE_IDENTITY()");
           
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "CellId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customCellInfo.TableId));
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, customCellInfo.SheetId);
                    db.AddInParameter(dbCommand, "RowIndex", DbType.Int32, customCellInfo.RowIndex);
                    db.AddInParameter(dbCommand, "ColIndex", DbType.Int32, customCellInfo.ColIndex);
                    db.AddInParameter(dbCommand, "CellType", DbType.Byte, customCellInfo.CellType);
                    db.AddInParameter(dbCommand, "CellProperty", DbType.Int64, customCellInfo.CellProperty);
                    db.AddInParameter(dbCommand, "ConditionText", DbType.String, customCellInfo.ConditionText);
                    db.AddInParameter(dbCommand, "TemplateText", DbType.String, customCellInfo.TemplateText);
                    db.AddInParameter(dbCommand, "ExtendRows", DbType.Int32, customCellInfo.ExtendRows);
                    db.AddInParameter(dbCommand, "ExtendCols", DbType.Int32, customCellInfo.ExtendCols);
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
                    customCellId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@CellId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customCellId;
        }

        /// <summary>
        ///  删除 CustomCellInfo 对象
        /// </summary>
        ///<param name="cellId">单元格编号</param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(decimal cellId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            string delete = "DELETE FROM CustomCell WHERE CellId = @CellId";

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(delete))
                {
                    db.AddInParameter(dbCommand, "CellId", DbType.Decimal, cellId);
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

        /// <summary>
        ///  删除 CustomCellInfo 对象，供其他函数调用
        /// </summary>
        ///<param name="sheetId">样表编号</param>
        /// <param name="transaction"></param>
        public void DeleteBySheetId(decimal sheetId, DbTransaction transaction)
        {
            if (transaction == null)
            {
                throw new ArgumentException("事务不能为空。");
            }

            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomCell ");
            sb.Append("WHERE SheetId = @SheetId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                CellStyle cellStyle = new CellStyle();
                IList<CustomCellInfo> customCellInfos = GetCustomCellInfos(sheetId);
                foreach (CustomCellInfo customCellInfo in customCellInfos)
                {
                    cellStyle.Delete(customCellInfo.CellId, db, transaction);
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, DataConvertionHelper.SetDecimal(sheetId));
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
        /// 获得 CustomCellInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomCellInfo 对象列表</returns>
        private IList<CustomCellInfo> GetModelInfos(IList<WhereConditon> whereConditons, bool onlyOne)
		{
			//创建集合对象
			IList<CustomCellInfo>  customCellInfos = new List<CustomCellInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CustomCell");
            
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
					using (IDataReader dataReader = db.ExecuteReader(dbCommand))
					{                        
						while (dataReader.Read())
						{
                            decimal cellId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal sheetId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            int rowIndex = DataConvertionHelper.GetInt(dataReader[3]);
                            int colIndex = DataConvertionHelper.GetInt(dataReader[4]);
                            byte cellType = DataConvertionHelper.GetByte(dataReader[5]);
                            long cellProperty = DataConvertionHelper.GetLong(dataReader[6]);
                            string conditionText = DataConvertionHelper.GetString(dataReader[7]);
                            string templateText = DataConvertionHelper.GetString(dataReader[8]);
                            int extendRows = DataConvertionHelper.GetInt(dataReader[9]);
                            int extendCols = DataConvertionHelper.GetInt(dataReader[10]);
                            //将创建 CustomCellInfo 对象加入集合中
                            customCellInfos.Add(new CustomCellInfo(cellId, tableId, sheetId, rowIndex, colIndex,
                            cellType, cellProperty, conditionText, templateText, extendRows,
                            extendCols));
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
            
			return customCellInfos;
		}
        
		
		/// <summary>
		/// 获得 CustomCellInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CustomCellInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomCell");
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
        /// 获得表 CustomCell 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomCell ", "CellId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomCell 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomCell ", "CellId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomCell 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomCell ", "CellId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomCell 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomCell ", "CellId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomCellInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomCell");
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
        /// 获得查询语句相关句子
        /// </summary>
        /// <param name="cellId"></param>
        /// <param name="relatedUserTypeCommonNodes"></param>
        /// <param name="relatedDepartmentCommonNodes"></param>
        /// <returns></returns>
        private CellConditionItem GetQueryClauses(decimal cellId, IList<CommonNode> relatedUserTypeCommonNodes, IList<CommonNode> relatedDepartmentCommonNodes)
        {
            CellConditionItem cellConditionItem = new CellConditionItem();

            StringBuilder sbFrom = new StringBuilder();
            StringBuilder sbWhere = new StringBuilder();
            CellStyle cellStyle = new CellStyle();
            CustomCellInfo customCellInfo = GetModelInfo(cellId);
            IList<CustomCellInfo> customCellInfos = GetSpecialCellInfos(customCellInfo);
            CustomTable customTable = new CustomTable();
            List<CustomCellInfo> newCustomCellInfos = new List<CustomCellInfo>();
            newCustomCellInfos.Add(customCellInfo);
            newCustomCellInfos.AddRange(customCellInfos.ToArray());
            Dictionary<decimal, string> tableNames = new Dictionary<decimal, string>();
            foreach (CustomCellInfo cellInfo in newCustomCellInfos)
            {
                if (!string.IsNullOrWhiteSpace(cellInfo.ConditionText))
                {
                    if (sbWhere.Length > 0)
                    {
                        sbWhere.Append(" AND ");
                    }
                    sbWhere.AppendFormat("({0})", cellInfo.ConditionText);
                }
                if (cellInfo.TableId > 0 && !tableNames.ContainsKey(cellInfo.TableId))
                {
                    string tablePhysicalName = customTable.GetTablePhysicalName(cellInfo.TableId);
                    tableNames.Add(cellInfo.TableId, tablePhysicalName);
                    cellConditionItem.TableNames.Add(tablePhysicalName);
                }
            }
            if (tableNames.Count == 0)
            {
                return null;
            }
            string mainTableName = tableNames.Values.First();
            IList<CommonNode> systemCommonNodes = cellStyle.GetSystemCommonNodes(newCustomCellInfos);
            Dictionary<string, TableLink> systemTableLinks = GetSystemTableLinks(mainTableName, systemCommonNodes);
            if (systemTableLinks != null && systemTableLinks.Count > 0)
            {
                string physicalName = DataAccessHandler.GetTableNames(mainTableName, systemTableLinks.Values.ToList<TableLink>());
                sbFrom.Append(physicalName);
            }
            else
            {
                sbFrom.Append(mainTableName);
            }
            if (tableNames.Count > 1)
            {
                int idx = 0;
                foreach (var tableName in tableNames.Values)
                {
                    if (idx++ > 0)
                    {
                        sbFrom.AppendFormat(" INNER JOIN {1} ON {0}.UserId = {1}.UserId ", mainTableName, tableName);
                    }
                }
            }
            /* 管理的用户类型 */
            if (relatedUserTypeCommonNodes != null && relatedUserTypeCommonNodes.Count > 0)
            {
                if (sbWhere.Length > 0)
                {
                    sbWhere.Append(" AND ");
                }
                sbWhere.Append("(");
                foreach (CommonNode commonNode in relatedUserTypeCommonNodes)
                {

                    sbWhere.AppendFormat(" {0}.UserTypeId = {1} OR", mainTableName, commonNode.NodeId);
                }
                sbWhere.Remove(sbWhere.Length - 3, 3);
                sbWhere.Append(")");
            }

            /* 管理的单位 */
            if (relatedDepartmentCommonNodes != null && relatedDepartmentCommonNodes.Count > 0)
            {
                if (sbWhere.Length > 0)
                {
                    sbWhere.Append(" AND ");
                }
                sbWhere.Append("(");
                foreach (CommonNode commonNode in relatedDepartmentCommonNodes)
                {

                    sbWhere.AppendFormat(" {0}.DepID = {1} OR", mainTableName, commonNode.NodeId);
                }
                sbWhere.Remove(sbWhere.Length - 3, 3);
                sbWhere.Append(")");
            }
            cellConditionItem.TableName = mainTableName;
            cellConditionItem.DataFileNames = customCellInfo.TemplateText;
            cellConditionItem.FromClause = sbFrom.ToString();
            cellConditionItem.WhereClause = sbWhere.ToString();

            return cellConditionItem;
        }

        /// <summary>
        /// 获得系统链接
        /// </summary>
        /// <param name="tablePhysicalName"></param>
        /// <param name="systemCommonNodes"></param>
        /// <returns></returns>
        private Dictionary<string, TableLink> GetSystemTableLinks(string tablePhysicalName, IList<CommonNode> systemCommonNodes)
        {
            Dictionary<string, TableLink> systemTableLinks = new Dictionary<string, TableLink>();

            if (systemCommonNodes != null && systemCommonNodes.Count > 0)
            {
                foreach (var commonNode in systemCommonNodes)
                {
                    SystemDataField systemLogicalDataField = (SystemDataField)commonNode.NodeId;
                    string systemTablePhysicalName = DataFieldHelper.GetSystemTablePhysicalName(systemLogicalDataField);
                    if (!string.IsNullOrWhiteSpace(systemTablePhysicalName) && !systemTableLinks.ContainsKey(systemTablePhysicalName))
                    {
                        TableLink tableLink = DataFieldHelper.GetTableLink(tablePhysicalName, systemLogicalDataField);
                        if (tableLink != null)
                        {
                            systemTableLinks.Add(systemTablePhysicalName, tableLink);
                        }
                    }
                }
            }

            return systemTableLinks;
        }


        /// <summary>
        /// 获得非数据类型的单元格列表
        /// </summary>
        /// <param name="customSheetStyleInfo"></param>
        /// <returns></returns>
        private IList<CustomCellInfo> GetSpecialCellInfos(CustomCellInfo customCellInfo)
        {
            /* WHERE CustomSheetId = @CustomSheetId AND (Row = @Row AND CellType = 0) OR (Col = @Col AND CellType = 1) OR ((Row = @Row OR Col = @Col) AND CellType = 3) OR CellType = 4) */
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("SheetId", "SheetId", DbType.Decimal, customCellInfo.SheetId,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));

            if (!AuthorityHelper.CheckAuthority(customCellInfo.CellProperty, (byte)DataFieldShowProperty.RowConditionNotIncluded))
            {
                whereConditons.Add(new WhereConditon("RowIndex", "Row_1", DbType.Int32, customCellInfo.RowIndex,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 2));
                whereConditons.Add(new WhereConditon("CellType", "CellType_1", DbType.Byte, DataConvertionHelper.GetConvertedByte(StatisticCellType.RowCondition),
                                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.RightBracket, 1));
            }

            if (!AuthorityHelper.CheckAuthority(customCellInfo.CellProperty, (byte)DataFieldShowProperty.ColumnConditionNotIncluded))
            {
                whereConditons.Add(new WhereConditon("ColIndex", "Col_1", DbType.Int32, customCellInfo.ColIndex,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.LeftBracket, 1));
                whereConditons.Add(new WhereConditon("CellType", "CellType_2", DbType.Byte, DataConvertionHelper.GetConvertedByte(StatisticCellType.ColumnCondition),
                                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.RightBracket, 1));
            }

            if (!AuthorityHelper.CheckAuthority(customCellInfo.CellProperty, (byte)DataFieldShowProperty.RowAndColumnConditionNotIncluded))
            {
                whereConditons.Add(new WhereConditon("RowIndex", "Row_2", DbType.Int32, customCellInfo.RowIndex,
                               DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.LeftBracket, 2));
                whereConditons.Add(new WhereConditon("ColIndex", "Col_2", DbType.Int32, customCellInfo.ColIndex,
                                   DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                whereConditons.Add(new WhereConditon("CellType", "CellType_3", DbType.Byte, DataConvertionHelper.GetConvertedByte(StatisticCellType.RowAndColumnCondition),
                                   DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.RightBracket, 1));
            }

            if (!AuthorityHelper.CheckAuthority(customCellInfo.CellProperty, (byte)DataFieldShowProperty.GlobalConditionNotIncluded))
            {
                whereConditons.Add(new WhereConditon("CellType", "CellType_4", DbType.Byte, DataConvertionHelper.GetConvertedByte(StatisticCellType.GlobalCondition),
                               DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
            }

            return GetModelInfos(whereConditons, null);
        }

        /// <summary>
        /// 获得单元格编号
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private decimal GetCellId(decimal sheetId, int row, int col)
        {
            decimal cellId = 0;
            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CellId FROM CustomCell ");
            sb.Append("WHERE SheetId = @SheetId AND RowIndex = @RowIndex AND ColIndex = @ColIndex");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, sheetId);
                    db.AddInParameter(dbCommand, "RowIndex", DbType.Decimal, row);
                    db.AddInParameter(dbCommand, "ColIndex", DbType.Decimal, col);
                    cellId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return cellId;
        }

        /// <summary>
        /// 获得单元格模板
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        private string GetTemplateText(decimal cellId)
        {
            string templateText = string.Empty;

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TemplateText FROM CustomCell ");
            sb.Append("WHERE CellId = @CellId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CellId", DbType.Decimal, cellId);
                    templateText = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return templateText;
        }

        /// <summary>
        /// 获得报表页编号
        /// </summary>
        /// <param name="cellId"></param>
        /// <returns></returns>
        private decimal GetSheetId(decimal cellId)
        {
            decimal sheetId = 0;

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT SheetId FROM CustomCell ");
            sb.Append("WHERE CellId = @CellId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "CellId", DbType.Decimal, cellId);
                    sheetId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return sheetId;
        }

        #endregion

        #endregion
    }
}
