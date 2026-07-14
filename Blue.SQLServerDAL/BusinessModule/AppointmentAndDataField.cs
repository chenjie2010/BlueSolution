//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppointmentAndDataField.cs
// 描述: AppointmentAndDataField 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/8/24
// Copyright 2018
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
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// AppointmentAndDataField 表的数据层访问类
    /// </summary>
    public class AppointmentAndDataField : CorrelatedTableDataAcess, IAppointmentAndDataField
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public AppointmentAndDataField() : base("AppointmentAndDataField", "DataFieldId", "AppointmentId")
        {
		}

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 AppointmentAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="appointmentAndDataFieldInfo">appointmentAndDataFieldInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(AppointmentAndDataFieldInfo appointmentAndDataFieldInfo)
        {
            //自动增加的关键字的值
            decimal appointmentAndDataFieldId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO AppointmentAndDataField(FstCondition, ScdCondition, OriginalValueSkipped, OriginalStringValue, StringValue, ");
            sb.Append("OriginalFstIntegerValue, OriginalScdIntegerValue, FstIntegerValue, ScdIntegerValue, OriginalFstDecimalValue, ");
            sb.Append("OriginalScdDecimalValue, FstDecimalValue, ScdDecimalValue, OriginalFstTimeValue, OriginalScdTimeValue, ");
            sb.Append("FstTimeValue, ScdTimeValue, NextRelation)");
            sb.Append("VALUES (@FstCondition, @ScdCondition, @OriginalValueSkipped, @OriginalStringValue, @StringValue, ");
            sb.Append("@OriginalFstIntegerValue, @OriginalScdIntegerValue, @FstIntegerValue, @ScdIntegerValue, @OriginalFstDecimalValue, ");
            sb.Append("@OriginalScdDecimalValue, @FstDecimalValue, @ScdDecimalValue, @OriginalFstTimeValue, @OriginalScdTimeValue, ");
            sb.Append("@FstTimeValue, @ScdTimeValue, @NextRelation);");
            sb.Append("SET @AppointmentId = SCOPE_IDENTITY()");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "DataFieldId", DbType.Decimal, 10);
                    db.AddOutParameter(dbCommand, "AppointmentId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "FstCondition", DbType.Byte, appointmentAndDataFieldInfo.FstCondition);
                    db.AddInParameter(dbCommand, "ScdCondition", DbType.Byte, appointmentAndDataFieldInfo.ScdCondition);
                    db.AddInParameter(dbCommand, "OriginalValueSkipped", DbType.Boolean, appointmentAndDataFieldInfo.OriginalValueSkipped);
                    db.AddInParameter(dbCommand, "OriginalStringValue", DbType.String, appointmentAndDataFieldInfo.OriginalStringValue);
                    db.AddInParameter(dbCommand, "StringValue", DbType.String, appointmentAndDataFieldInfo.StringValue);
                    db.AddInParameter(dbCommand, "OriginalFstIntegerValue", DbType.Int32, appointmentAndDataFieldInfo.OriginalFstIntegerValue);
                    db.AddInParameter(dbCommand, "OriginalScdIntegerValue", DbType.Int32, appointmentAndDataFieldInfo.OriginalScdIntegerValue);
                    db.AddInParameter(dbCommand, "FstIntegerValue", DbType.Int32, appointmentAndDataFieldInfo.FstIntegerValue);
                    db.AddInParameter(dbCommand, "ScdIntegerValue", DbType.Int32, appointmentAndDataFieldInfo.ScdIntegerValue);
                    db.AddInParameter(dbCommand, "OriginalFstDecimalValue", DbType.Decimal, appointmentAndDataFieldInfo.OriginalFstDecimalValue);
                    db.AddInParameter(dbCommand, "OriginalScdDecimalValue", DbType.Decimal, appointmentAndDataFieldInfo.OriginalScdDecimalValue);
                    db.AddInParameter(dbCommand, "FstDecimalValue", DbType.Decimal, appointmentAndDataFieldInfo.FstDecimalValue);
                    db.AddInParameter(dbCommand, "ScdDecimalValue", DbType.Decimal, appointmentAndDataFieldInfo.ScdDecimalValue);
                    db.AddInParameter(dbCommand, "OriginalFstTimeValue", DbType.DateTime, appointmentAndDataFieldInfo.OriginalFstTimeValue);
                    db.AddInParameter(dbCommand, "OriginalScdTimeValue", DbType.DateTime, appointmentAndDataFieldInfo.OriginalScdTimeValue);
                    db.AddInParameter(dbCommand, "FstTimeValue", DbType.DateTime, appointmentAndDataFieldInfo.FstTimeValue);
                    db.AddInParameter(dbCommand, "ScdTimeValue", DbType.DateTime, appointmentAndDataFieldInfo.ScdTimeValue);
                    db.AddInParameter(dbCommand, "NextRelation", DbType.Byte, appointmentAndDataFieldInfo.NextRelation);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                    appointmentAndDataFieldId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@AppointmentId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return appointmentAndDataFieldId;
        }

        /// <summary>
		/// 获得 AppointmentAndDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldId">字段编号</param>
		///<param name="appointmentId">预约编号</param>
		/// <returns> AppointmentAndDataFieldInfo 对象</returns>
		public AppointmentAndDataFieldInfo GetModelInfo(decimal dataFieldId, decimal appointmentId)
        {
            AppointmentAndDataFieldInfo appointmentAndDataFieldInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("DataFieldId", "DataFieldId", DbType.Decimal, dataFieldId, DataFieldCondition.Equal));

            //创建集合对象
            IList<AppointmentAndDataFieldInfo> appointmentAndDataFieldInfos = GetModelInfos(whereConditons, null, true);
            if (appointmentAndDataFieldInfos != null && appointmentAndDataFieldInfos.Count > 0)
            {
                appointmentAndDataFieldInfo = appointmentAndDataFieldInfos[0];
            }

            return appointmentAndDataFieldInfo;
        }

        /// <summary>
        /// 更新 AppointmentAndDataFieldInfo 对象
        /// </summary>
        /// <param name="appointmentAndDataFieldInfo">AppointmentAndDataFieldInfo 对象</param>
        public void Update(AppointmentAndDataFieldInfo appointmentAndDataFieldInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE AppointmentAndDataField SET FstCondition = @FstCondition, ScdCondition = @ScdCondition, OriginalValueSkipped = @OriginalValueSkipped, ");
            sb.Append("OriginalStringValue = @OriginalStringValue, StringValue = @StringValue, OriginalFstIntegerValue = @OriginalFstIntegerValue, ");
            sb.Append("OriginalScdIntegerValue = @OriginalScdIntegerValue, FstIntegerValue = @FstIntegerValue, ScdIntegerValue = @ScdIntegerValue, ");
            sb.Append("OriginalFstDecimalValue = @OriginalFstDecimalValue, OriginalScdDecimalValue = @OriginalScdDecimalValue, FstDecimalValue = @FstDecimalValue, ");
            sb.Append("ScdDecimalValue = @ScdDecimalValue, OriginalFstTimeValue = @OriginalFstTimeValue, OriginalScdTimeValue = @OriginalScdTimeValue, ");
            sb.Append("FstTimeValue = @FstTimeValue, ScdTimeValue = @ScdTimeValue, NextRelation = @NextRelation ");
            sb.Append("WHERE DataFieldId = @DataFieldId AND AppointmentId = @AppointmentId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, appointmentAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "AppointmentId", DbType.Decimal, appointmentAndDataFieldInfo.AppointmentId);
                    db.AddInParameter(dbCommand, "FstCondition", DbType.Byte, appointmentAndDataFieldInfo.FstCondition);
                    db.AddInParameter(dbCommand, "ScdCondition", DbType.Byte, appointmentAndDataFieldInfo.ScdCondition);
                    db.AddInParameter(dbCommand, "OriginalValueSkipped", DbType.Boolean, appointmentAndDataFieldInfo.OriginalValueSkipped);
                    db.AddInParameter(dbCommand, "OriginalStringValue", DbType.String, appointmentAndDataFieldInfo.OriginalStringValue);
                    db.AddInParameter(dbCommand, "StringValue", DbType.String, appointmentAndDataFieldInfo.StringValue);
                    db.AddInParameter(dbCommand, "OriginalFstIntegerValue", DbType.Int32, appointmentAndDataFieldInfo.OriginalFstIntegerValue);
                    db.AddInParameter(dbCommand, "OriginalScdIntegerValue", DbType.Int32, appointmentAndDataFieldInfo.OriginalScdIntegerValue);
                    db.AddInParameter(dbCommand, "FstIntegerValue", DbType.Int32, appointmentAndDataFieldInfo.FstIntegerValue);
                    db.AddInParameter(dbCommand, "ScdIntegerValue", DbType.Int32, appointmentAndDataFieldInfo.ScdIntegerValue);
                    db.AddInParameter(dbCommand, "OriginalFstDecimalValue", DbType.Decimal, appointmentAndDataFieldInfo.OriginalFstDecimalValue);
                    db.AddInParameter(dbCommand, "OriginalScdDecimalValue", DbType.Decimal, appointmentAndDataFieldInfo.OriginalScdDecimalValue);
                    db.AddInParameter(dbCommand, "FstDecimalValue", DbType.Decimal, appointmentAndDataFieldInfo.FstDecimalValue);
                    db.AddInParameter(dbCommand, "ScdDecimalValue", DbType.Decimal, appointmentAndDataFieldInfo.ScdDecimalValue);
                    db.AddInParameter(dbCommand, "OriginalFstTimeValue", DbType.DateTime, appointmentAndDataFieldInfo.OriginalFstTimeValue);
                    db.AddInParameter(dbCommand, "OriginalScdTimeValue", DbType.DateTime, appointmentAndDataFieldInfo.OriginalScdTimeValue);
                    db.AddInParameter(dbCommand, "FstTimeValue", DbType.DateTime, appointmentAndDataFieldInfo.FstTimeValue);
                    db.AddInParameter(dbCommand, "ScdTimeValue", DbType.DateTime, appointmentAndDataFieldInfo.ScdTimeValue);
                    db.AddInParameter(dbCommand, "NextRelation", DbType.Byte, appointmentAndDataFieldInfo.NextRelation);
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
        ///  删除 AppointmentAndDataFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        ///<param name="appointmentId">预约编号</param>
        public void Delete(decimal dataFieldId, decimal appointmentId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM AppointmentAndDataField ");
            sb.Append("WHERE DataFieldId = @DataFieldId AND AppointmentId = @AppointmentId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
                    db.AddInParameter(dbCommand, "AppointmentId", DbType.Decimal, appointmentId);
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
        /// 获得 AppointmentAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AppointmentAndDataFieldInfo 对象列表</returns>
        public IList<AppointmentAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 AppointmentAndDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>AppointmentAndDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "AppointmentAndDataField ", "DataFieldId", false, whereConditons);
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

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 AppointmentAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>AppointmentAndDataFieldInfo 对象列表</returns>
        private IList<AppointmentAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<AppointmentAndDataFieldInfo> appointmentAndDataFieldInfos = new List<AppointmentAndDataFieldInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM AppointmentAndDataField");

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
                            decimal appointmentId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            byte fstCondition = DataConvertionHelper.GetByte(dataReader[2]);
                            byte scdCondition = DataConvertionHelper.GetByte(dataReader[3]);
                            bool originalValueSkipped = DataConvertionHelper.GetBoolean(dataReader[4]);
                            string originalStringValue = DataConvertionHelper.GetString(dataReader[5]);
                            string stringValue = DataConvertionHelper.GetString(dataReader[6]);
                            int originalFstIntegerValue = DataConvertionHelper.GetInt(dataReader[7]);
                            int originalScdIntegerValue = DataConvertionHelper.GetInt(dataReader[8]);
                            int fstIntegerValue = DataConvertionHelper.GetInt(dataReader[9]);
                            int scdIntegerValue = DataConvertionHelper.GetInt(dataReader[10]);
                            decimal originalFstDecimalValue = DataConvertionHelper.GetDecimal(dataReader[11]);
                            decimal originalScdDecimalValue = DataConvertionHelper.GetDecimal(dataReader[12]);
                            decimal fstDecimalValue = DataConvertionHelper.GetDecimal(dataReader[13]);
                            decimal scdDecimalValue = DataConvertionHelper.GetDecimal(dataReader[14]);
                            DateTime originalFstTimeValue = DataConvertionHelper.GetDateTime(dataReader[15]);
                            DateTime originalScdTimeValue = DataConvertionHelper.GetDateTime(dataReader[16]);
                            DateTime fstTimeValue = DataConvertionHelper.GetDateTime(dataReader[17]);
                            DateTime scdTimeValue = DataConvertionHelper.GetDateTime(dataReader[18]);
                            byte nextRelation = DataConvertionHelper.GetByte(dataReader[19]);
                            //将创建 AppointmentAndDataFieldInfo 对象加入集合中
                            appointmentAndDataFieldInfos.Add(new AppointmentAndDataFieldInfo(dataFieldId, appointmentId, fstCondition, scdCondition, originalValueSkipped,
                            originalStringValue, stringValue, originalFstIntegerValue, originalScdIntegerValue, fstIntegerValue,
                            scdIntegerValue, originalFstDecimalValue, originalScdDecimalValue, fstDecimalValue, scdDecimalValue,
                            originalFstTimeValue, originalScdTimeValue, fstTimeValue, scdTimeValue, nextRelation));
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

            return appointmentAndDataFieldInfos;
        }


        /// <summary>
        /// 获得 AppointmentAndDataFieldInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AppointmentAndDataFieldInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM AppointmentAndDataField");
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
        /// 获得表 AppointmentAndDataField 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "AppointmentAndDataField ", "DataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 AppointmentAndDataField 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "AppointmentAndDataField ", "DataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 AppointmentAndDataField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "AppointmentAndDataField ", "DataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 AppointmentAndDataField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "AppointmentAndDataField ", "DataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 AppointmentAndDataFieldInfo 对象
        /// </summary>
        /// <param name="dataFieldId">字段编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal dataFieldId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM AppointmentAndDataField WHERE DataFieldId = @DataFieldId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
                    //执行删除操作
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
        /// 删除满足条件的所有  AppointmentAndDataFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM AppointmentAndDataField");
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
