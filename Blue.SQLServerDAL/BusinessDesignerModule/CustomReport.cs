//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomReport.cs
// 描述：CustomReport 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/10/9
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
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;
using Blue.SQLServerDAL.BusinessModule;

namespace Blue.SQLServerDAL.BusinessDesignerModule
{
    /// <summary>
    /// CustomReport 表的数据层访问类
    /// </summary>
    public class CustomReport : CommonNodeDataAccess, ICustomReport
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomReport() : base("CustomReport", "ReportId", "GroupId", "ReportName", "ReportCode", false, true, "ReportType")
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomReport 表中插入一条新记录
        /// </summary>
        /// <param name="customReportInfo">customReportInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomReportInfo customReportInfo)
        {
            //自动增加的关键字的值
            decimal customReportId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customReportInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomReport", "Sorting", "GroupId", customReportInfo.GroupId, 0) + 1;

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    customReportId = Insert(customReportInfo, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customReportId;
        }

        /// <summary>
        /// 获得 CustomReportInfo 对象
        /// </summary>
        ///<param name="reportId">报表编号编号</param>
        /// <returns> CustomReportInfo 对象</returns>
        public CustomReportInfo GetModelInfo(decimal reportId)
        {
            CustomReportInfo customReportInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ReportId", "ReportId", System.Data.DbType.Decimal, reportId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomReportInfo> customReportInfos = GetModelInfos(whereConditons, null, true);
            if (customReportInfos != null && customReportInfos.Count > 0)
            {
                customReportInfo = customReportInfos[0];
            }

            return customReportInfo;
        }

        /// <summary>
        /// 更新 CustomReportInfo 对象
        /// </summary>
        /// <param name="customReportInfo">CustomReportInfo 对象</param>
        public void Update(CustomReportInfo customReportInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomReport SET GroupId = @GroupId, ReportName = @ReportName, ReportCode = @ReportCode, ");
            sb.Append("DataWarehouseId = @DataWarehouseId, SystemDataFields = @SystemDataFields, ToolTip = @ToolTip, Notes = @Notes ");
            sb.Append("WHERE ReportId = @ReportId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, customReportInfo.ReportId);
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customReportInfo.GroupId);
                    db.AddInParameter(dbCommand, "ReportName", DbType.String, customReportInfo.ReportName);
                    db.AddInParameter(dbCommand, "ReportCode", DbType.String, customReportInfo.ReportCode);
                    db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, customReportInfo.DataWarehouseId);
                    db.AddInParameter(dbCommand, "SystemDataFields", DbType.Int64, customReportInfo.SystemDataFields);
                    db.AddInParameter(dbCommand, "ToolTip", DbType.String, customReportInfo.ToolTip);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customReportInfo.Notes);
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
        ///  删除 CustomReportInfo 对象
        /// </summary>
        ///<param name="reportId">报表编号编号</param>
        public void Delete(decimal reportId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomReport ");
            sb.Append("WHERE ReportId = @ReportId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    CustomSheet customSheet = new CustomSheet();
                    customSheet.Delete(reportId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, reportId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
                        }
                    }
                    transaction.Commit();
                    customSheet.DeleteReportFile(reportId);
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
        /// 获得 CustomReportInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomReportInfo 对象列表</returns>
        public IList<CustomReportInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomReport 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomReportInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomReport ", "ReportId", false, whereConditons);
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
        /// 根据报表类型条件获得报表节点
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tableFilter"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal groupId, ReportCategory reportCategory)
        {
            IList<CommonNode> commonNodes = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("GroupId", "GroupId", DbType.Decimal, groupId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("ReportCategory", "ReportCategory", DbType.Byte, (byte)reportCategory, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            commonNodes = GetCommonNodesByWhereConditon(whereConditons);

            return commonNodes;
        }
        /// <summary>
        /// 获得报表名称
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public string GetReportName(decimal reportId)
        {
            string reportName = string.Empty;

            //查询语句
            string sqlSelect = "SELECT ReportName FROM CustomReport WHERE ReportId = @ReportId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, reportId);
                reportName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
            }

            return reportName;
        }

        /// <summary>
        /// 获得节点和所有的上级节点的名称
        /// </summary>
        /// <param name="nodeId">节点编号</param>
        /// <returns>上级节点的名称列表</returns>
        public override IList<string> GetHierarchicalNamesOfNode(decimal nodeId)
        {
            IList<string> names = new List<string>();

            CustomReportInfo customReportInfo = GetModelInfo(nodeId);
            if (customReportInfo != null)
            {
                CustomGroup customGroup = new CustomGroup();
                IList<string> parentNames = customGroup.GetHierarchicalNamesOfNode(customReportInfo.GroupId);
                foreach (var parentName in parentNames)
                {
                    names.Add(parentName);
                }
                names.Add(customReportInfo.ReportName);
            }

            return names;
        }

        /// <summary>
        /// 样表另存
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="reportId"></param>
        /// <param name="data"></param>
        public void SheetSaveAs(decimal sheetId, decimal reportId, byte[] data)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            CustomSheet customSheet = new CustomSheet();
            CustomSheetInfo customSheetInfo = customSheet.GetModelInfo(sheetId);            
            IList<string> childNodeCodes = customSheet.GetChildNodeCodes(reportId);
            string nodeCode = string.Empty;
            string parentNodeCode = GetNodeCode(reportId);
            int index = 1;
            do
            {
                nodeCode = DataFieldHelper.GetNewCode(parentNodeCode, index);
                index++;
            } while (childNodeCodes.Contains(nodeCode));
            customSheetInfo.SheetCode = nodeCode;
            customSheetInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomSheet", "Sorting", "ReportId", customSheetInfo.ReportId, 0) + 1;
            CustomCell customCell = new CustomCell();
            IList<CustomCellInfo> customCellInfos = customCell.GetModelInfos(sheetId);
            CellStyle cellStyle = new CellStyle();
            Dictionary<decimal, IList<CellStyleInfo>> dicCellStyleInfos = new Dictionary<decimal, IList<CellStyleInfo>>();
            foreach (CustomCellInfo customCellInfo in customCellInfos)
            {
                IList<CellStyleInfo> cellStyleInfos = cellStyle.GetModelInfos(customCellInfo.CellId);
                dicCellStyleInfos.Add(customCellInfo.CellId, cellStyleInfos);
            }
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    /* 1.样表记录另存 */
                    decimal newSheetId = customSheet.Insert(customSheetInfo, db, transaction);

                    /* 2.单元格记录另存 */
                    foreach (CustomCellInfo customCellInfo in customCellInfos)
                    {
                        customCellInfo.SheetId = newSheetId;
                        decimal cellId = customCell.Insert(customCellInfo, db, transaction);
                        IList<CellStyleInfo> cellStyleInfos = dicCellStyleInfos[customCellInfo.CellId];
                        /* 3.单元格关联的字段记录另存 */
                        foreach (var cellStyleInfo in cellStyleInfos)
                        {
                            cellStyleInfo.CellId = cellId;
                            cellStyle.Insert(cellStyleInfo, db, transaction);
                        }
                    }
                    transaction.Commit();
                    /* 4.报表文件另存 */
                    customSheet.UploadReportFile(customSheetInfo.ReportId, data, null);
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
        /// 表套另存
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="groupId"></param>
        public void SaveAs(decimal reportId, decimal groupId)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomReport", "Sorting", "GroupId", groupId, 0) + 1;
            CustomSheet customSheet = new CustomSheet();
            Dictionary<decimal, RowAndCol> rowAndCols = new Dictionary<decimal, RowAndCol>();
            Dictionary<decimal, IList<CustomCellInfo>> dicCustomCellInfos = new Dictionary<decimal, IList<CustomCellInfo>>();
            Dictionary<decimal, IList<CellStyleInfo>> dicCellStyleInfos = new Dictionary<decimal, IList<CellStyleInfo>>();
            IList<CustomSheetInfo> customSheetInfos = customSheet.GetModelInfos(reportId);
            CustomCell customCell = new CustomCell();
            CellStyle cellStyle = new CellStyle();
            foreach (CustomSheetInfo customSheetInfo in customSheetInfos)
            {
                rowAndCols.Add(customSheetInfo.SheetId, customSheet.GetRowAndColCountBySheetId(customSheetInfo.SheetId));
                IList<CustomCellInfo> customCellInfos = customCell.GetModelInfos(customSheetInfo.SheetId);
                dicCustomCellInfos.Add(customSheetInfo.SheetId, customCellInfos);                
                foreach (CustomCellInfo customCellInfo in customCellInfos)
                {
                    IList<CellStyleInfo> cellStyleInfos = cellStyle.GetModelInfos(customCellInfo.CellId);
                    dicCellStyleInfos.Add(customCellInfo.CellId, cellStyleInfos);
                }
            }
            CustomReportInfo customReportInfo = GetModelInfo(reportId);
            IList<string> childNodeCodes = GetChildNodeCodes(groupId);
            CustomGroup customGroup = new CustomGroup();
            string reportCode = string.Empty;
            string parentNodeCode = customGroup.GetNodeCode(groupId);
            int idx = 1;
            do
            {
                reportCode = DataFieldHelper.GetNewCode(parentNodeCode, idx++);
            } while (childNodeCodes.Contains(reportCode));
            Dictionary <decimal, RowAndCol> newRowAndCols = new Dictionary<decimal, RowAndCol>();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    /* 1.表套记录另存 */
                    customReportInfo.ReportCode = reportCode;
                    customReportInfo.ReportName = string.Format("另存的{0}", customReportInfo.ReportName);
                    customReportInfo.GroupId = groupId;
                    customReportInfo.Sorting = sorting;
                    decimal newReportId = Insert(customReportInfo, db, transaction);
                    /* 2.样表记录另存 */
                    int index = 1;
                    foreach (CustomSheetInfo customSheetInfo in customSheetInfos)
                    {
                        string sheetCode = DataFieldHelper.GetNewCode(reportCode, index++);
                        customSheetInfo.ReportId = newReportId;
                        customSheetInfo.SheetCode = sheetCode;
                        decimal sheetId = customSheet.Insert(customSheetInfo, db, transaction);
                        newRowAndCols.Add(sheetId, rowAndCols[customSheetInfo.SheetId]);
                        /* 3.单元格记录另存 */
                        IList<CustomCellInfo> customCellInfos = dicCustomCellInfos[customSheetInfo.SheetId];
                        foreach (CustomCellInfo customCellInfo in customCellInfos)
                        {
                            customCellInfo.SheetId = sheetId;
                            /* 4.单元格关联的字段记录另存 */
                            decimal cellId = customCell.Insert(customCellInfo, db, transaction);
                            IList<CellStyleInfo> cellStyleInfos = dicCellStyleInfos[customCellInfo.CellId];
                            foreach (var cellStyleInfo in cellStyleInfos)
                            {
                                cellStyleInfo.CellId = cellId;
                                cellStyle.Insert(cellStyleInfo, db, transaction);
                            }
                        }
                    }
                    transaction.Commit();
                    /* 5.报表文件另存 */
                    byte[] data = customSheet.DownloadReportFile(reportId);
                    customSheet.UploadReportFile(newReportId, data, newRowAndCols);
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
        /// 获得表套的所属数据仓库
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal reportId)
        {
            byte dataWarehouseId = 0;

            //查询语句
            string sqlSelect = "SELECT DataWarehouseId FROM CustomReport WHERE ReportId = @ReportId";

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, reportId);
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
        
        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomReportInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomReportInfo 对象列表</returns>
        private IList<CustomReportInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomReportInfo> customReportInfos = new List<CustomReportInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomReport");
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
                            decimal reportId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string reportName = DataConvertionHelper.GetString(dataReader[2]);
                            string reportCode = DataConvertionHelper.GetString(dataReader[3]);
                            byte reportCategory = DataConvertionHelper.GetByte(dataReader[4]);
                            byte reportType = DataConvertionHelper.GetByte(dataReader[5]);
                            byte dataWarehouseId = DataConvertionHelper.GetByte(dataReader[6]);
                            long systemDataFields = DataConvertionHelper.GetLong(dataReader[7]);
                            string toolTip = DataConvertionHelper.GetString(dataReader[8]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[9]);
                            string notes = DataConvertionHelper.GetString(dataReader[10]);
                            //将创建 CustomReportInfo 对象加入集合中
                            customReportInfos.Add(new CustomReportInfo(reportId, groupId, reportName, reportCode, reportCategory,
                            reportType, dataWarehouseId, systemDataFields, toolTip, sorting, notes));
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

            return customReportInfos;
        }

        /// <summary>
        /// 获得 CustomReportInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomReportInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomReport");
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
        /// 获得表 CustomReport 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomReport ", "ReportId", "*", false, false, startPosition,
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
        /// 获得以表 CustomReport 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomReport ", "ReportId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomReport 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomReport ", "ReportId", "*", false, false, startPosition,
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
        /// 获得以表 CustomReport 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomReport ", "ReportId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomReportInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomReport");
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
        /// 向 CustomReport 表中插入一条新记录
        /// </summary>
        /// <param name="customReportInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public decimal Insert(CustomReportInfo customReportInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal customReportId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomReport(GroupId, ReportName, ReportCode, ReportCategory, ReportType, DataWarehouseId, ");
            sb.Append("SystemDataFields, ToolTip, Sorting, Notes)");
            sb.Append("VALUES (@GroupId, @ReportName, @ReportCode, @ReportCategory, @ReportType, @DataWarehouseId, ");
            sb.Append("@SystemDataFields, @ToolTip, @Sorting, @Notes);");
            sb.Append("SET @ReportId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "ReportId", DbType.Decimal, 8);
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customReportInfo.GroupId);
                    db.AddInParameter(dbCommand, "ReportName", DbType.String, customReportInfo.ReportName);
                    db.AddInParameter(dbCommand, "ReportCode", DbType.String, customReportInfo.ReportCode);
                    db.AddInParameter(dbCommand, "ReportCategory", DbType.Byte, customReportInfo.ReportCategory);
                    db.AddInParameter(dbCommand, "ReportType", DbType.Byte, customReportInfo.ReportType);
                    db.AddInParameter(dbCommand, "DataWarehouseId", DbType.Byte, customReportInfo.DataWarehouseId);
                    db.AddInParameter(dbCommand, "SystemDataFields", DbType.Int64, customReportInfo.SystemDataFields);
                    db.AddInParameter(dbCommand, "ToolTip", DbType.String, customReportInfo.ToolTip);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customReportInfo.Sorting);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customReportInfo.Notes);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                    customReportId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@ReportId"].Value, 0);
                }
                CustomGroup customGroup = new CustomGroup();
                customGroup.UpdateLeafOfParentNode(customReportInfo.GroupId, false, db, transaction);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customReportId;
        }

        #endregion

        #endregion
    }
}

