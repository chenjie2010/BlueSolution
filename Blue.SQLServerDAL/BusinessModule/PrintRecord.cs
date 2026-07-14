//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: PrintRecord.cs
// 描述: PrintRecord 数据层访问类
// 作者：ChenJie 
// 编写日期：2022/11/13
// Copyright 2022
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
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// PrintRecord 表的数据层访问类
    /// </summary>
    public class PrintRecord : IPrintRecord
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public PrintRecord()
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 PrintRecord 表中插入一条新记录
        /// </summary>
        /// <param name="printRecordInfo">printRecordInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(PrintRecordInfo printRecordInfo)
        {
            //自动增加的关键字的值
            decimal printRecordId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO PrintRecord(PrintId, UserId, PrintTime, PrintType, PrintAddition, ");
            sb.Append("Commments)");
            sb.Append("VALUES (@PrintId, @UserId, @PrintTime, @PrintType, @PrintAddition, ");
            sb.Append("@Commments);");
            sb.Append("SET @PrintRecordId = SCOPE_IDENTITY()");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "PrintRecordId", DbType.Decimal, 8);
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printRecordInfo.PrintId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, printRecordInfo.UserId);
                    db.AddInParameter(dbCommand, "PrintTime", DbType.DateTime, printRecordInfo.PrintTime);
                    db.AddInParameter(dbCommand, "PrintType", DbType.Byte, printRecordInfo.PrintType);
                    db.AddInParameter(dbCommand, "PrintAddition", DbType.Int32, printRecordInfo.PrintAddition);
                    db.AddInParameter(dbCommand, "Commments", DbType.String, printRecordInfo.Commments);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                    printRecordId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@PrintRecordId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return printRecordId;
        }

        /// <summary>
		/// 获得 PrintRecordInfo 对象
		/// </summary>
		///<param name="printRecordId">用户打印编号</param>
		/// <returns> PrintRecordInfo 对象</returns>
		public PrintRecordInfo GetModelInfo(decimal printRecordId)
        {
            PrintRecordInfo printRecordInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("PrintRecordId", "PrintRecordId", DbType.Decimal, printRecordId, DataFieldCondition.Equal));

            //创建集合对象
            IList<PrintRecordInfo> printRecordInfos = GetModelInfos(whereConditons, null, true);
            if (printRecordInfos != null && printRecordInfos.Count > 0)
            {
                printRecordInfo = printRecordInfos[0];
            }

            return printRecordInfo;
        }

        /// <summary>
        /// 更新 PrintRecordInfo 对象
        /// </summary>
        /// <param name="printRecordInfo">PrintRecordInfo 对象</param>
        public void Update(PrintRecordInfo printRecordInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE PrintRecord SET PrintId = @PrintId, UserId = @UserId, PrintTime = @PrintTime, ");
            sb.Append("PrintType = @PrintType, PrintAddition = @PrintAddition, Commments = @Commments ");
            sb.Append("WHERE PrintRecordId = @PrintRecordId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "PrintRecordId", DbType.Decimal, printRecordInfo.PrintRecordId);
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printRecordInfo.PrintId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, printRecordInfo.UserId);
                    db.AddInParameter(dbCommand, "PrintTime", DbType.DateTime, printRecordInfo.PrintTime);
                    db.AddInParameter(dbCommand, "PrintType", DbType.Byte, printRecordInfo.PrintType);
                    db.AddInParameter(dbCommand, "PrintAddition", DbType.Int32, printRecordInfo.PrintAddition);
                    db.AddInParameter(dbCommand, "Commments", DbType.String, printRecordInfo.Commments);
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
        ///  删除 PrintRecordInfo 对象
        /// </summary>
        ///<param name="printRecordId">用户打印编号</param>
        public void Delete(decimal printRecordId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM PrintRecord ");
            sb.Append("WHERE PrintRecordId = @PrintRecordId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "PrintRecordId", DbType.Decimal, printRecordId);
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
        /// 获得 PrintRecordInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>PrintRecordInfo 对象列表</returns>
        public IList<PrintRecordInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 PrintRecord 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>PrintRecordInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "PrintRecord ", "PrintRecordId", false, whereConditons);
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
        /// 获得以表 PrintRecord 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                //----------------for example ---------------------------------- 
                string dataFileNames = @"PrintRecordId, PrintName, UserName, UserActualName, PrintTime";
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserAccount", "UserId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomPrint", "PrintId", TableJoin.InnerJoin));                
                ds =  DataAccessHandler.GetPageRecord(db, "PrintRecord ", "PrintRecordId", dataFileNames, false, false, tableLinks, startPosition, 
                    count, whereConditons, sortingCondtions, ref totalCount);                 
               //-------------------------------------------------------------------
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }



        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 PrintRecordInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>PrintRecordInfo 对象列表</returns>
        private IList<PrintRecordInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<PrintRecordInfo> printRecordInfos = new List<PrintRecordInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM PrintRecord");

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
                            decimal printRecordId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal printId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            DateTime printTime = DataConvertionHelper.GetDateTime(dataReader[3]);
                            byte printType = DataConvertionHelper.GetByte(dataReader[4]);
                            int printAddition = DataConvertionHelper.GetInt(dataReader[5]);
                            string commments = DataConvertionHelper.GetString(dataReader[6]);
                            //将创建 PrintRecordInfo 对象加入集合中
                            printRecordInfos.Add(new PrintRecordInfo(printRecordId, printId, userId, printTime, printType,
                            printAddition, commments));
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

            return printRecordInfos;
        }


        /// <summary>
        /// 获得 PrintRecordInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>PrintRecordInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM PrintRecord");
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
        /// 获得表 PrintRecord 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "PrintRecord", "PrintRecordId", "*", false, false, startPosition,
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
        /// 获得以表 PrintRecord 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "PrintRecord ", "PrintRecordId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 PrintRecord 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "PrintRecord ", "PrintRecordId", "*", false, false, startPosition,
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
        /// 删除满足条件的所有  PrintRecordInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM PrintRecord");
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

        #endregion

        #endregion
    }
}
