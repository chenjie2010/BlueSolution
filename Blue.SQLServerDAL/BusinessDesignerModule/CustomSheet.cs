//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSheet.cs
// 描述: CustomSheet 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.SQLServerDAL.BusinessDesignerModule
{
    /// <summary>
    /// CustomSheet 表的数据层访问类
    /// </summary>
    public class CustomSheet : CommonNodeDataAccess, ICustomSheet
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomSheet() : base("CustomSheet", "SheetId", "ReportId", "SheetName", "SheetCode", false, true)
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomSheet 表中插入一条新记录
        /// </summary>
        /// <param name="customSheetInfo">customSheetInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomSheetInfo customSheetInfo)
        {
            //自动增加的关键字的值
            decimal sheetId = 0;
            
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            try
            {
                customSheetInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomSheet", "Sorting", "ReportId", customSheetInfo.ReportId, 0) + 1;
                sheetId = Insert(customSheetInfo, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return sheetId;
        }

        /// <summary>
		/// 获得 CustomSheetInfo 对象
		/// </summary>
		///<param name="sheetId">样表编号</param>
		/// <returns> CustomSheetInfo 对象</returns>
		public CustomSheetInfo GetModelInfo(decimal sheetId)
        {
            CustomSheetInfo customSheetInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("SheetId", "SheetId", DbType.Decimal, sheetId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomSheetInfo> customSheetInfos = GetModelInfos(whereConditons, null, true);
            if (customSheetInfos != null && customSheetInfos.Count > 0)
            {
                customSheetInfo = customSheetInfos[0];
            }

            return customSheetInfo;
        }

        /// <summary>
        /// 更新 CustomSheetInfo 对象
        /// </summary>
        /// <param name="customSheetInfo">CustomSheetInfo 对象</param>
        public void Update(CustomSheetInfo customSheetInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomSheet SET ReportId = @ReportId, SheetName = @SheetName, SheetCode = @SheetCode, ");
            sb.Append("SheetDescription = @SheetDescription, ApprovalNumber = @ApprovalNumber, ");
            sb.Append("Notes = @Notes ");
            sb.Append("WHERE SheetId = @SheetId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, customSheetInfo.SheetId);
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, customSheetInfo.ReportId);
                    db.AddInParameter(dbCommand, "SheetName", DbType.String, customSheetInfo.SheetName);
                    db.AddInParameter(dbCommand, "SheetCode", DbType.String, customSheetInfo.SheetCode);
                    db.AddInParameter(dbCommand, "SheetDescription", DbType.String, customSheetInfo.SheetDescription);
                    db.AddInParameter(dbCommand, "ApprovalNumber", DbType.Int32, customSheetInfo.ApprovalNumber);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customSheetInfo.Notes);
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
        ///  删除 CustomSheetInfo 对象
        /// </summary>
        ///<param name="sheetId">样表编号</param>
        public void Delete(decimal sheetId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE CellStyle FROM CellStyle INNER JOIN CustomCell ON CustomCell.CellId = CellStyle.CellId ");
            sb.Append("INNER JOIN CustomSheet ON CustomCell.SheetId = CustomSheet.SheetId WHERE CustomSheet.SheetId = @SheetId; ");
            sb.Append("DELETE CustomCell FROM CustomCell INNER JOIN CustomSheet ON CustomCell.SheetId = CustomSheet.SheetId ");
            sb.Append("WHERE CustomSheet.SheetId = @SheetId; ");
            sb.Append("DELETE FROM CustomSheet WHERE SheetId = @SheetId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, sheetId);
                    //执行删除操作
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
        /// 获得 CustomSheetInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomSheetInfo 对象列表</returns>
        public IList<CustomSheetInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomSheet 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomSheetInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomSheet ", "SheetId", false, whereConditons);
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
        /// 获得 CustomSheetInfo 对象的列表
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public IList<CustomSheetInfo> GetModelInfos(decimal reportId)
        {
            //创建集合对象
            IList<CustomSheetInfo> customSheetInfos = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("ReportId", "ReportId", DbType.Decimal, reportId, DataFieldCondition.Equal));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));
            customSheetInfos = GetModelInfos(whereConditons, sortingCondtions, false);

            return customSheetInfos;
        }

        /// <summary>
        /// 批文编号自动加1
        /// </summary>
        /// <param name="sheetId"></param>
        public void AutoIncreaseApprovalNumber(decimal sheetId)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("IF EXISTS(SELECT 1 FROM CustomSheet WHERE SheetId = @SheetId AND ApprovalNumber IS NOT NULL) ");
            sb.Append("BEGIN UPDATE CustomSheet SET ApprovalNumber = ApprovalNumber + 1 WHERE SheetId = @SheetId ");
            sb.Append("END ELSE BEGIN UPDATE CustomSheet SET ApprovalNumber = 1 WHERE SheetId = @SheetId END");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, DataConvertionHelper.SetDecimal(sheetId));
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
        /// 更新边距
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="customMargin"></param>
        public void UpdateMargin(decimal sheetId, CustomMargin customMargin)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomSheet SET MarginTop = @MarginTop, MarginBottom = @MarginBottom, MarginLeft = @MarginLeft, ");
            sb.Append("MarginRight = @MarginRight WHERE SheetId = @SheetId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, DataConvertionHelper.SetDecimal(sheetId));
                    db.AddInParameter(dbCommand, "MarginTop", DbType.Int32, DataConvertionHelper.SetInt(customMargin.Top));
                    db.AddInParameter(dbCommand, "MarginBottom", DbType.Int32, DataConvertionHelper.SetInt(customMargin.Bottom));
                    db.AddInParameter(dbCommand, "MarginLeft", DbType.Int32, DataConvertionHelper.SetInt(customMargin.Left));
                    db.AddInParameter(dbCommand, "MarginRight", DbType.Int32, DataConvertionHelper.SetInt(customMargin.Right));
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
        /// 获得边距
        /// </summary>
        /// <param name="sheetId"></param>
        public CustomMargin GetMargin(decimal sheetId)
        {
            CustomMargin customMargin = null;
            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT MarginTop, MarginBottom, MarginLeft, MarginRight FROM CustomSheet ");
            sb.Append("WHERE SheetId = @SheetId");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, sheetId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            int top = DataConvertionHelper.GetInt(dataReader[0], 0);
                            int bottom = DataConvertionHelper.GetInt(dataReader[1], 0);
                            int left = DataConvertionHelper.GetInt(dataReader[2], 0);
                            int right = DataConvertionHelper.GetInt(dataReader[3], 0);
                            //创建 CustomMargin 对象
                            customMargin = new CustomMargin(top, bottom, left, right);
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

            return customMargin;
        }

        /// <summary>
        /// 更新样表的行列数
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        public void Update(decimal sheetId, int rowCount, int columnCount)
        {
            Dictionary<decimal, RowAndCol> rowAndCols = new Dictionary<decimal, RowAndCol>();
            rowAndCols.Add(sheetId, new RowAndCol(rowCount, columnCount));
            UpdateRowAndCol(rowAndCols);
        }

        /// <summary>
        /// 获得样表的行列数
        /// </summary>
        /// <param name="sheetId"></param>
        /// <returns></returns>
        public RowAndCol GetRowAndColCountBySheetId(decimal sheetId)
        {
            return GetRowAndColCount(sheetId, null);
        }

        /// <summary>
        /// 获得表套的样表行列数
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public IList<RowAndCol> GetRowAndColCount(decimal reportId)
        {
            //创建集合对象
            IList<RowAndCol> rowAndCols = new List<RowAndCol>();

            //查询语句
            string sqlSelect = "SELECT SheetRowCount, SheetColCount FROM CustomSheet WHERE ReportId = @ReportId ORDER BY Sorting ASC";
           
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, reportId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            int sheetRowCount = DataConvertionHelper.GetInt(dataReader[0]);
                            int sheetColCount = DataConvertionHelper.GetInt(dataReader[1]);
                            rowAndCols.Add(new RowAndCol(sheetRowCount, sheetColCount));
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

            return rowAndCols;
        }

        /// <summary>
        /// 导入 Excel 格式的文件
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="sheetNames"></param>
        public void Insert(decimal reportId, IList<CustomSheetInfo> sheetNames)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomSheet", "Sorting", "ReportId", reportId, 0) + 1;
            CustomReport customReport = new CustomReport();
            string reportCode = customReport.GetNodeCodeByNodeId(reportId);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {                    
                    for (int i = 0; i < sheetNames.Count; i++)
                    {
                        string sheetCode = UserDataHelper.GetTreeCode(reportCode, i);                
                        Insert(new CustomSheetInfo(0, reportId, sheetNames[i].SheetName, sheetCode, string.Empty,
                                    sheetNames[i].SheetRowCount, sheetNames[i].SheetColCount, 0, 0, 0, 0, 1, sorting + i, string.Empty), db, transaction);
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
        /// 获得报表文件
        /// </summary>
        /// <param name="reportId">报表编号</param>
        /// <returns></returns>
        public byte[] DownloadReportFile(decimal reportId)
        {
            byte[] data = null;

            string defaultDir = GetDefaultSubDirOfReportingFiles();
            StringBuilder sbPath = new StringBuilder();
            sbPath.AppendFormat(@"{0}\Report_{1}.xlsx", defaultDir, reportId);
            if (File.Exists(sbPath.ToString()))
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
        /// 删除报表文件
        /// </summary>
        /// <param name="reportId"></param>
        public void DeleteReportFile(decimal reportId)
        {
            string defaultDir = GetDefaultSubDirOfReportingFiles();
            if (!Directory.Exists(defaultDir))
            {
                Directory.CreateDirectory(defaultDir.ToString());
            }
            StringBuilder sbPath = new StringBuilder();
            sbPath.AppendFormat(@"{0}\Report_{1}.xlsx", defaultDir, reportId);
            try
            {
                if (File.Exists(sbPath.ToString()))
                {
                    File.Delete(sbPath.ToString());
                }
            }
            catch { }
        }

        /// <summary>
        /// 保存报表文件
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="reportType"></param>
        /// <param name="data"></param>
        /// <param name="rowAndCols"></param>
        public void UploadReportFile(decimal reportId, byte[] data, Dictionary<decimal, RowAndCol> rowAndCols)
        {
            string defaultDir = GetDefaultSubDirOfReportingFiles();
            if (!Directory.Exists(defaultDir))
            {
                Directory.CreateDirectory(defaultDir.ToString());
            }
            StringBuilder sbPath = new StringBuilder();
            sbPath.AppendFormat(@"{0}\Report_{1}.xlsx", defaultDir, reportId);
            try
            {
                if (File.Exists(sbPath.ToString()))
                {
                    File.Delete(sbPath.ToString());
                }
                using (FileStream fileStream = new FileStream(sbPath.ToString(), FileMode.CreateNew, FileAccess.Write))
                {
                    fileStream.Write(data, 0, (int)data.Length);
                    fileStream.Close();
                }
            }
            catch { }
            UpdateRowAndCol(rowAndCols);
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="sheetNames"></param>
        public void UpdateSheetSorting(decimal reportId, IList<string> sheetNames)
        {            
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            string update = "UPDATE CustomSheet SET Sorting = @Sorting WHERE ReportId = @ReportId AND SheetName = @SheetName";
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    for (int i = 0; i < sheetNames.Count; i++)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(update))
                        {
                            db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, reportId);
                            db.AddInParameter(dbCommand, "SheetName", DbType.String, sheetNames[i]);
                            db.AddInParameter(dbCommand, "Sorting", DbType.Int32, i+1);
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

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 删除 CustomSheetInfo 对象
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(decimal reportId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE CellStyle FROM CellStyle INNER JOIN CustomCell ON CustomCell.CellId = CellStyle.CellId ");
            sb.Append("INNER JOIN CustomSheet ON CustomCell.SheetId = CustomSheet.SheetId WHERE CustomSheet.ReportId = @ReportId; ");
            sb.Append("DELETE CustomCell FROM CustomCell INNER JOIN CustomSheet ON CustomCell.SheetId = CustomSheet.SheetId ");
            sb.Append("WHERE CustomSheet.ReportId = @ReportId; ");
            sb.Append("DELETE FROM CustomSheet WHERE ReportId = @ReportId");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, reportId);
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
        /// 获得样表的行列数
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public RowAndCol GetRowAndColCount(decimal sheetId, DbTransaction transaction)
        {
            RowAndCol rowAndCol = null;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT SheetRowCount, SheetColCount FROM CustomSheet WHERE SheetId = @SheetId");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, sheetId);
                    if (transaction != null)
                    {
                        using (IDataReader dataReader = db.ExecuteReader(dbCommand, transaction))
                        {
                            if (dataReader.Read())
                            {
                                int sheetRowCount = DataConvertionHelper.GetInt(dataReader[0]);
                                int sheetColCount = DataConvertionHelper.GetInt(dataReader[1]);
                                rowAndCol = new RowAndCol(sheetRowCount, sheetColCount);
                            }
                            if (dataReader != null)
                            {
                                dataReader.Close();
                            }
                        }
                    }
                    else
                    {
                        using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                        {
                            if (dataReader.Read())
                            {
                                int sheetRowCount = DataConvertionHelper.GetInt(dataReader[0]);
                                int sheetColCount = DataConvertionHelper.GetInt(dataReader[1]);
                                rowAndCol = new RowAndCol(sheetRowCount, sheetColCount);
                            }
                            if (dataReader != null)
                            {
                                dataReader.Close();
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

            return rowAndCol;
        }

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 CustomSheetInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomSheetInfo 对象列表</returns>
        private IList<CustomSheetInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomSheetInfo> customSheetInfos = new List<CustomSheetInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CustomSheet");

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
                            decimal sheetId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal reportId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string sheetName = DataConvertionHelper.GetString(dataReader[2]);
                            string sheetCode = DataConvertionHelper.GetString(dataReader[3]);
                            string sheetDescription = DataConvertionHelper.GetString(dataReader[4]);
                            int sheetRowCount = DataConvertionHelper.GetInt(dataReader[5]);
                            int sheetColCount = DataConvertionHelper.GetInt(dataReader[6]);
                            int marginTop = DataConvertionHelper.GetInt(dataReader[7]);
                            int marginBottom = DataConvertionHelper.GetInt(dataReader[8]);
                            int marginLeft = DataConvertionHelper.GetInt(dataReader[9]);
                            int marginRight = DataConvertionHelper.GetInt(dataReader[10]);
                            int approvalNumber = DataConvertionHelper.GetInt(dataReader[11]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[12]);
                            string notes = DataConvertionHelper.GetString(dataReader[13]);
                            //将创建 CustomSheetInfo 对象加入集合中
                            customSheetInfos.Add(new CustomSheetInfo(sheetId, reportId, sheetName, sheetCode, sheetDescription,
                            sheetRowCount, sheetColCount, marginTop, marginBottom, marginLeft,
                            marginRight, approvalNumber, sorting, notes));
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

            return customSheetInfos;
        }


        /// <summary>
        /// 获得 CustomSheetInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomSheetInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomSheet");
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
        /// 获得表 CustomSheet 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomSheet ", "SheetId", "*", false, false, startPosition,
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
        /// 获得以表 CustomSheet 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomSheet ", "SheetId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomSheet 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomSheet ", "SheetId", "*", false, false, startPosition,
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
        /// 获得以表 CustomSheet 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomSheet ", "SheetId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomSheetInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomSheet");
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
        /// 向 CustomSheet 表中插入一条新记录
        /// </summary>
        /// <param name="customSheetInfo">customSheetInfo 对象</param>
        /// <param name="db">数据库</param>
        /// <param name="transaction">事务</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomSheetInfo customSheetInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal sheetId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomSheet(ReportId, SheetName, SheetCode, SheetDescription, SheetRowCount, ");
            sb.Append("SheetColCount, MarginTop, MarginBottom, MarginLeft, MarginRight, ");
            sb.Append("ApprovalNumber, Sorting, Notes)");
            sb.Append("VALUES (@ReportId, @SheetName, @SheetCode, @SheetDescription, @SheetRowCount, ");
            sb.Append("@SheetColCount, @MarginTop, @MarginBottom, @MarginLeft, @MarginRight, ");
            sb.Append("@ApprovalNumber, @Sorting, @Notes);");
            sb.Append("SET @SheetId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "SheetId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, customSheetInfo.ReportId);
                    db.AddInParameter(dbCommand, "SheetName", DbType.String, customSheetInfo.SheetName);
                    db.AddInParameter(dbCommand, "SheetCode", DbType.String, customSheetInfo.SheetCode);
                    db.AddInParameter(dbCommand, "SheetDescription", DbType.String, customSheetInfo.SheetDescription);
                    db.AddInParameter(dbCommand, "SheetRowCount", DbType.Int32, customSheetInfo.SheetRowCount);
                    db.AddInParameter(dbCommand, "SheetColCount", DbType.Int32, customSheetInfo.SheetColCount);
                    db.AddInParameter(dbCommand, "MarginTop", DbType.Int32, customSheetInfo.MarginTop);
                    db.AddInParameter(dbCommand, "MarginBottom", DbType.Int32, customSheetInfo.MarginBottom);
                    db.AddInParameter(dbCommand, "MarginLeft", DbType.Int32, customSheetInfo.MarginLeft);
                    db.AddInParameter(dbCommand, "MarginRight", DbType.Int32, customSheetInfo.MarginRight);
                    db.AddInParameter(dbCommand, "ApprovalNumber", DbType.Int32, customSheetInfo.ApprovalNumber);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customSheetInfo.Sorting);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customSheetInfo.Notes);
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
                    sheetId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@SheetId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return sheetId;
        }

        /// <summary>
        /// 更新样表的行列数
        /// </summary>
        /// <param name="rowAndCols"></param>
        private void UpdateRowAndCol(Dictionary<decimal, RowAndCol> rowAndCols)
        {
            if (rowAndCols == null || rowAndCols.Count == 0)
            {
                return;
            }
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomSheet SET SheetRowCount = @SheetRowCount, SheetColCount = @SheetColCount ");
            sb.Append("WHERE SheetId = @SheetId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (KeyValuePair<decimal, RowAndCol> rowAndCol in rowAndCols)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            //给参数赋值
                            db.AddInParameter(dbCommand, "SheetId", DbType.Decimal, rowAndCol.Key);
                            db.AddInParameter(dbCommand, "SheetRowCount", DbType.Int32, rowAndCol.Value.Row);
                            db.AddInParameter(dbCommand, "SheetColCount", DbType.Int32, rowAndCol.Value.Col);
                            if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                            {
                                throw new Exception("删除失败！");
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
        /// 获得表套文件的默认目录
        /// </summary>
        /// <returns></returns>
        private string GetDefaultSubDirOfReportingFiles()
        {
            StringBuilder sbPath = new StringBuilder();
            sbPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
            if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
            {
                sbPath.Append(@"\");
            }
            sbPath.Append(AppSettingHelper.DefaultSubDirOfReportingFiles);

            return sbPath.ToString();
        }

        #endregion

        #endregion
    }
}
