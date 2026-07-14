//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：BusinessInstanceStep.cs
// 描述：BusinessInstanceStep 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/3/18
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Core;
using Blue.IDAL.DataFilledModule;
using Blue.Model.DataFilledModule;

namespace Blue.SQLServerDAL.DataFilledModule
{
    /// <summary>
    /// BusinessInstanceStep 表的数据层访问类
    /// </summary>
    public class BusinessInstanceStep : IBusinessInstanceStep
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public BusinessInstanceStep()
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 BusinessInstanceStep 表中插入一条新记录
        /// </summary>
        /// <param name="businessInstanceStepInfo">businessInstanceStepInfo 对象</param>        
        public void Insert(BusinessInstanceStepInfo businessInstanceStepInfo)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                businessInstanceStepInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "BusinessInstanceStep", "Sorting", "InstanceId", businessInstanceStepInfo.InstanceId, 0) + 1;
                Insert(businessInstanceStepInfo, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
		/// 获得 BusinessInstanceStepInfo 对象
		/// </summary>
		///<param name="instanceId">实例编号</param>
		///<param name="sorting">排序</param>
		/// <returns> BusinessInstanceStepInfo 对象</returns>
		public BusinessInstanceStepInfo GetModelInfo(decimal instanceId, int sorting)
        {
            BusinessInstanceStepInfo businessInstanceStepInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("InstanceId", "InstanceId", DbType.Decimal, instanceId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("Sorting", "Sorting", DbType.Int32, sorting, DataFieldCondition.Equal));

            //创建集合对象
            IList<BusinessInstanceStepInfo> businessInstanceStepInfos = GetModeInfos(whereConditons, null, true);
            if (businessInstanceStepInfos != null && businessInstanceStepInfos.Count > 0)
            {
                businessInstanceStepInfo = businessInstanceStepInfos[0];
            }

            return businessInstanceStepInfo;
        }

        /// <summary>
        /// 更新 BusinessInstanceStepInfo 对象
        /// </summary>
        /// <param name="businessInstanceStepInfo">BusinessInstanceStepInfo 对象</param>
        public void Update(BusinessInstanceStepInfo businessInstanceStepInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE BusinessInstanceStep SET UserId = @UserId, ReviewedAction = @ReviewedAction, ActionVisible = @ActionVisible, TimeReviewed = @TimeReviewed, CommentReviewed = @CommentReviewed ");
            sb.Append("WHERE InstanceId = @InstanceId AND Sorting = @Sorting");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, businessInstanceStepInfo.InstanceId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, businessInstanceStepInfo.Sorting);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, businessInstanceStepInfo.UserId);
                    db.AddInParameter(dbCommand, "ReviewedAction", DbType.Byte, businessInstanceStepInfo.ReviewedAction);
                    db.AddInParameter(dbCommand, "TimeReviewed", DbType.DateTime, businessInstanceStepInfo.TimeReviewed);
                    db.AddInParameter(dbCommand, "CommentReviewed", DbType.String, businessInstanceStepInfo.CommentReviewed);
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
        ///  删除 BusinessInstanceStepInfo 对象
        /// </summary>
        ///<param name="instanceId">实例编号</param>
        ///<param name="sorting">排序</param>
        public void Delete(decimal instanceId, int sorting)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM BusinessInstanceStep ");
            sb.Append("WHERE InstanceId = @InstanceId AND Sorting = @Sorting");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
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
        /// 获得 BusinessInstanceStepInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>BusinessInstanceStepInfo 对象列表</returns>
        public IList<BusinessInstanceStepInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModeInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 BusinessInstanceStep 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>BusinessInstanceStepInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "BusinessInstanceStep ", "InstanceId", false, whereConditons);
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
        /// 获得数据填报的处理流程
        /// </summary>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal instanceId)
        {
            DataSet ds = null;

            StringBuilder sb = new StringBuilder();

            sb.Append("SELECT UserName, UserActualName, ReviewedAction, TimeReviewed, CommentReviewed FROM BusinessInstanceStep ");
            sb.Append("INNER JOIN UserAccount ON BusinessInstanceStep.UserId = UserAccount.UserId WHERE InstanceId = @InstanceId ORDER BY BusinessInstanceStep.Sorting");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    ds = db.ExecuteDataSet(dbCommand);
                }
                foreach (DataColumn dataColumn in ds.Tables[0].Columns)
                {
                    dataColumn.Caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
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
        /// 根据当前实例编号，获得最新的审核人编号
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核人编号</returns>
        public decimal GetLastestReviewerId(decimal instanceId)
        {
            decimal userId = decimal.MinValue;

            try
            {
                string sqlSelect = "SELECT BusinessInstanceStep.UserId FROM BusinessInstance INNER JOIN BusinessInstanceStep ON BusinessInstance.InstanceId = BusinessInstanceStep.InstanceId  WHERE BusinessInstance.InstanceId = @InstanceId AND (ReviewedAction = @ReviewedAction_0 OR ReviewedAction = @ReviewedAction_1 OR ReviewedAction = @ReviewedAction_2) ORDER BY Sorting DESC";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    db.AddInParameter(dbCommand, "ReviewedAction_0", DbType.Byte, (byte)ReviewedAction.Sumbitted);
                    db.AddInParameter(dbCommand, "ReviewedAction_1", DbType.Byte, (byte)ReviewedAction.Pass);
                    db.AddInParameter(dbCommand, "ReviewedAction_2", DbType.Byte, (byte)ReviewedAction.Reject);
                    userId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userId;
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核意见
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核意见</returns>
        public string GetLastestComment(decimal instanceId)
        {
            string comment = string.Empty;

            try
            {
                string sqlSelect = "SELECT CommentReviewed FROM BusinessInstance INNER JOIN BusinessInstanceStep ON BusinessInstance.InstanceId = BusinessInstanceStep.InstanceId  WHERE BusinessInstance.InstanceId = @InstanceId ORDER BY Sorting DESC";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    comment = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return comment;
        }

        /// <summary>
        /// 根据当前实例编号，获得最新的审核操作
        /// </summary>
        ///<param name="instanceId">字段编号</param>
        /// <returns> 最新的审核操作</returns>
        public ReviewedAction GetLastestReviewedAction(decimal instanceId)
        {
            ReviewedAction reviewedAction = ReviewedAction.None;

            try
            {
                string sqlSelect = "SELECT ReviewedAction FROM BusinessInstance INNER JOIN BusinessInstanceStep ON BusinessInstance.InstanceId = BusinessInstanceStep.InstanceId  WHERE BusinessInstance.InstanceId = @InstanceId ORDER BY Sorting DESC";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, DataConvertionHelper.SetDecimal(instanceId));
                    reviewedAction = (ReviewedAction)DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return reviewedAction;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 删除与填报相关的步骤
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int Delete(decimal instanceId, SqlDatabase db, DbTransaction transaction)
        {
            int count = 0;

            string delete = "DELETE FROM BusinessInstanceStep WHERE InstanceId = @InstanceId";
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(delete))
                {
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    //执行删除操作
                    count = db.ExecuteNonQuery(dbCommand, transaction);
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
        ///  更新动作的可见性
        /// </summary>
        ///<param name="instanceId">实例编号</param>
        ///<param name="sorting">排序</param>
        public void UpdateActionVisible(decimal instanceId, int sorting, bool actionVisible, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE BusinessInstanceStep SET ActionVisible = @ActionVisible ");
            sb.Append("WHERE InstanceId = @InstanceId AND Sorting = @Sorting");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                    db.AddInParameter(dbCommand, "ActionVisible", DbType.Boolean, actionVisible);
                    //执行删除操作
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
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 向 BusinessInstanceStep 表中插入一条新记录
        /// </summary>
        /// <param name="businessInstanceStepInfo">businessInstanceStepInfo 对象</param>        
        public void Insert(BusinessInstanceStepInfo businessInstanceStepInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO BusinessInstanceStep(InstanceId, Sorting, UserId, ReviewedAction, ActionVisible, TimeReviewed, CommentReviewed)");
            sb.Append("VALUES (@InstanceId, @Sorting, @UserId, @ReviewedAction,@ActionVisible, @TimeReviewed, @CommentReviewed);");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, businessInstanceStepInfo.InstanceId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, businessInstanceStepInfo.Sorting);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, businessInstanceStepInfo.UserId);
                    db.AddInParameter(dbCommand, "ReviewedAction", DbType.Byte, businessInstanceStepInfo.ReviewedAction);
                    db.AddInParameter(dbCommand, "ActionVisible", DbType.Boolean, businessInstanceStepInfo.ActionVisible);
                    db.AddInParameter(dbCommand, "TimeReviewed", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "CommentReviewed", DbType.String, businessInstanceStepInfo.CommentReviewed);
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
        /// 获得 BusinessInstanceStepInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>BusinessInstanceStepInfo 对象列表</returns>
        private IList<BusinessInstanceStepInfo> GetModeInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<BusinessInstanceStepInfo> businessInstanceStepInfos = new List<BusinessInstanceStepInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM BusinessInstanceStep");
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
                            decimal instanceId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[1]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            byte reviewedState = DataConvertionHelper.GetByte(dataReader[3]);
                            bool actionVisible = DataConvertionHelper.GetBoolean(dataReader[4]);
                            DateTime timeReviewed = DataConvertionHelper.GetDateTime(dataReader[5]);
                            string commentReviewed = DataConvertionHelper.GetString(dataReader[6]);
                            //将创建 BusinessInstanceStepInfo 对象加入集合中
                            businessInstanceStepInfos.Add(new BusinessInstanceStepInfo(instanceId, sorting, userId, reviewedState, actionVisible, timeReviewed,
                            commentReviewed));
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

            return businessInstanceStepInfos;
        }

        /// <summary>
        /// 获得 BusinessInstanceStepInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>BusinessInstanceStepInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM BusinessInstanceStep");
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
        /// 获得表 BusinessInstanceStep 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "BusinessInstanceStep ", "InstanceId", "*", false, false, startPosition,
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
        /// 获得以表 BusinessInstanceStep 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "BusinessInstanceStep ", "InstanceId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 BusinessInstanceStep 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "BusinessInstanceStep ", "InstanceId", "*", false, false, startPosition,
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
        /// 获得以表 BusinessInstanceStep 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "BusinessInstanceStep ", "InstanceId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的的 BusinessInstanceStepInfo 对象
        /// </summary>
        /// <param name="instanceId">实例编号</param>
        /// <returns>返回删除的记录数目数目</returns>
        private int Delete(decimal instanceId)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM BusinessInstanceStep WHERE InstanceId = @InstanceId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "InstanceId", DbType.Decimal, instanceId);
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
        /// 删除满足条件的所有  BusinessInstanceStepInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM BusinessInstanceStep");
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
