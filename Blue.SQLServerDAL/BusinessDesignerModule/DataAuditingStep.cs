//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingStep.cs
// 描述: DataAuditingStep 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/10/19
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
using AppFramework.Core;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.SQLServerDAL.BusinessDesignerModule
{
    /// <summary>
    /// DataAuditingStep 表的数据层访问类
    /// </summary>
    public class DataAuditingStep : IDataAuditingStep
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public DataAuditingStep()
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 DataAuditingStep 表中插入一条新记录
		/// </summary>
		/// <param name="dataAuditingStepInfo">dataAuditingStepInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(DataAuditingStepInfo dataAuditingStepInfo)
		{
			//自动增加的关键字的值
			decimal dataAuditingStepId = 0;
			
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
                dataAuditingStepId = Insert(dataAuditingStepInfo, db, null);

            }
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return dataAuditingStepId;
		}

        /// <summary>
		/// 获得 DataAuditingStepInfo 对象
		/// </summary>
		///<param name="stepId">步骤编号</param>
		/// <returns> DataAuditingStepInfo 对象</returns>
		public DataAuditingStepInfo GetModelInfo(decimal stepId)
		{			
			DataAuditingStepInfo  dataAuditingStepInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("StepId", "StepId", DbType.Decimal, stepId, DataFieldCondition.Equal));

            //创建集合对象
            IList<DataAuditingStepInfo>  dataAuditingStepInfos = GetModelInfos(whereConditons, null, true);
            if (dataAuditingStepInfos != null && dataAuditingStepInfos.Count > 0)
            {
                dataAuditingStepInfo = dataAuditingStepInfos[0];
            }

            return dataAuditingStepInfo;            
		}
        
        /// <summary>
		/// 更新 DataAuditingStepInfo 对象
		/// </summary>
		/// <param name="dataAuditingStepInfo">DataAuditingStepInfo 对象</param>
		public void Update(DataAuditingStepInfo dataAuditingStepInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE DataAuditingStep SET UserId = @UserId, AuditingLogId = @AuditingLogId, ParentUserId = @ParentUserId, ");
			sb.Append("AuditingAction = @AuditingAction, AuditingTime = @AuditingTime ");
			sb.Append("WHERE StepId = @StepId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "StepId", DbType.Decimal, dataAuditingStepInfo.StepId);
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, dataAuditingStepInfo.UserId);
					db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, dataAuditingStepInfo.AuditingLogId);
					db.AddInParameter(dbCommand, "ParentUserId", DbType.Decimal, dataAuditingStepInfo.ParentUserId);
					db.AddInParameter(dbCommand, "AuditingAction", DbType.Byte, dataAuditingStepInfo.AuditingAction);
					db.AddInParameter(dbCommand, "AuditingTime", DbType.DateTime, dataAuditingStepInfo.AuditingTime);
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
		///  删除 DataAuditingStepInfo 对象
		/// </summary>
	    ///<param name="stepId">步骤编号</param>
		public void Delete(decimal stepId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM DataAuditingStep ");
			sb.Append("WHERE StepId = @StepId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "StepId", DbType.Decimal, stepId);
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
		/// 获得 DataAuditingStepInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataAuditingStepInfo 对象列表</returns>
		public IList<DataAuditingStepInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 DataAuditingStep 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>DataAuditingStepInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "DataAuditingStep ", "StepId", false, whereConditons);
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
        /// 获得日志流程
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public DataSet GetSteps(decimal auditingLogId)
        {
            DataSet ds = null;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT DataAuditingStep.StepId, A.UserName, A.UserActualName, AuditingAction, AuditingTime, B.UserName AS B_UserName, B.UserActualName AS B_UserActualName, Comment FROM DataAuditingStep ");
            sb.Append("INNER JOIN DataAuditingLog ON DataAuditingStep.AuditingLogId = DataAuditingLog.AuditingLogId ");
            sb.Append("INNER JOIN UserAccount A ON DataAuditingStep.ParentUserId = A.UserId ");
            sb.Append("LEFT JOIN UserAccount B ON DataAuditingStep.UserId = B.UserId ");
            sb.Append("WHERE DataAuditingLog.AuditingLogId = @AuditingLogId ORDER BY AuditingLogTime DESC ");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, auditingLogId);
                ds = db.ExecuteDataSet(dbCommand);
            }
            ds.Tables[0].Columns["B_UserName"].Caption = "下一步处理人";
            ds.Tables[0].Columns["B_UserActualName"].Caption = "处理人姓名";
            foreach (DataColumn dataColumn in ds.Tables[0].Columns)
            {
                string caption = ColumnCaptionHelper.GetColumnCaption(dataColumn.ColumnName);
                if (!string.IsNullOrWhiteSpace(caption))
                {
                    dataColumn.Caption = caption;
                }
            }

            return ds;
        }

        /// <summary>
        /// 获得最新提交人
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public CommonNode GetLastestSubmitter(decimal auditingLogId)
        {
            CommonNode commonNode = new CommonNode();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TOP 1 UserAccount.UserId, UserName, UserActualName FROM DataAuditingStep ");
            sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = DataAuditingStep.ParentUserId ");
            sb.Append(" WHERE AuditingLogId = @AuditingLogId ORDER BY DataAuditingStep.AuditingTime DESC ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, auditingLogId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userName = DataConvertionHelper.GetString(dataReader[1]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[2]);
                            commonNode.NodeId = userId;
                            commonNode.NodeName = userName;
                            commonNode.NodeCode = userActualName;
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

            return commonNode;
        }

        /// <summary>
        /// 获得最新审核人
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public CommonNode GetLastestReviewer(decimal auditingLogId)
        {
            CommonNode commonNode = new CommonNode();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT TOP 1 UserAccount.UserId, UserName, UserActualName FROM DataAuditingStep ");
            sb.Append("INNER JOIN UserAccount ON UserAccount.UserId = DataAuditingStep.UserId ");
             sb.Append(" WHERE AuditingLogId = @AuditingLogId ORDER BY DataAuditingStep.AuditingTime DESC ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, auditingLogId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userName = DataConvertionHelper.GetString(dataReader[1]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[2]);
                            commonNode.NodeId = userId;
                            commonNode.NodeName = userName;
                            commonNode.NodeCode = userActualName;
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

            return commonNode;
        }

        /// <summary>
        /// 获得以表 DataAuditingStep 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetDataAuditingSteps(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string dataFileNames = @"DataAuditingStep.AuditingLogId, AuditingLogName, UserName, UserActualName, AuditingLogTime, AuditingStatus, AuditingAction, AuditingTime, Comment";
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("DataAuditingLog", "AuditingLogId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("DataAuditingLog", "ParentUserId", TableJoin.InnerJoin, "UserAccount", "UserId"));
                ds = DataAccessHandler.GetPageRecord(db, "DataAuditingStep", "StepId", dataFileNames, false, false, tableLinks, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
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

        /// <summary>
        /// 向 DataAuditingStep 表中插入一条新记录
        /// </summary>
        /// <param name="dataAuditingStepInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public decimal Insert(DataAuditingStepInfo dataAuditingStepInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal dataAuditingStepId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO DataAuditingStep(UserId, AuditingLogId, ParentUserId, AuditingAction, ");
            sb.Append("AuditingTime, Comment)");
            sb.Append("VALUES (@UserId, @AuditingLogId, @ParentUserId, @AuditingAction, ");
            sb.Append("@AuditingTime, @Comment);");
            sb.Append("SET @StepId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "StepId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingStepInfo.UserId));
                    db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, dataAuditingStepInfo.AuditingLogId);
                    db.AddInParameter(dbCommand, "ParentUserId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataAuditingStepInfo.ParentUserId));
                    db.AddInParameter(dbCommand, "AuditingAction", DbType.Byte, dataAuditingStepInfo.AuditingAction);
                    db.AddInParameter(dbCommand, "AuditingTime", DbType.DateTime, DateTime.Now);
                    db.AddInParameter(dbCommand, "Comment", DbType.String, dataAuditingStepInfo.Comment);
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
                    dataAuditingStepId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@StepId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataAuditingStepId;
        }

        /// <summary>
        /// 删除 DataAuditingStepInfo 对象
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Delete(decimal auditingLogId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataAuditingStep ");
            sb.Append("WHERE AuditingLogId = @AuditingLogId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "AuditingLogId", DbType.Decimal, auditingLogId);
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
        /// 获得 DataAuditingStepInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>DataAuditingStepInfo 对象列表</returns>
        private IList<DataAuditingStepInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<DataAuditingStepInfo>  dataAuditingStepInfos = new List<DataAuditingStepInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM DataAuditingStep");
            
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
                            decimal stepId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal auditingLogId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal parentUserId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            byte auditingAction = DataConvertionHelper.GetByte(dataReader[4]);
                            DateTime auditingTime = DataConvertionHelper.GetDateTime(dataReader[5]);
                            string comment = DataConvertionHelper.GetString(dataReader[6]);
                            //将创建 DataAuditingStepInfo 对象加入集合中
                            dataAuditingStepInfos.Add(new DataAuditingStepInfo(stepId, userId, auditingLogId, parentUserId, auditingAction,
                            auditingTime, comment));
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
            
			return dataAuditingStepInfos;
		}
        
		
		/// <summary>
		/// 获得 DataAuditingStepInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>DataAuditingStepInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM DataAuditingStep");
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
        /// 获得表 DataAuditingStep 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataAuditingStep ", "StepId", "*", false, false, startPosition, 
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
        /// 获得以表 DataAuditingStep 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataAuditingStep ", "StepId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 DataAuditingStep 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "DataAuditingStep ", "StepId", "*", false, false, startPosition, 
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
        /// 删除满足条件的所有  DataAuditingStepInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM DataAuditingStep");
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
