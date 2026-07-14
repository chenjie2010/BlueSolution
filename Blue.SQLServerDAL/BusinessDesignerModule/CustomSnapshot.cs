//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSnapshot.cs
// 描述: CustomSnapshot 数据层访问类
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
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.SQLServerDAL.BusinessDesignerModule
{
    /// <summary>
    /// CustomSnapshot 表的数据层访问类
    /// </summary>
    public class CustomSnapshot : ICustomSnapshot
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomSnapshot()
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 CustomSnapshot 表中插入一条新记录
		/// </summary>
		/// <param name="customSnapshotInfo">customSnapshotInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomSnapshotInfo customSnapshotInfo)
		{
			//自动增加的关键字的值
			decimal customSnapshotId= 0;
			//生成插入语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("INSERT INTO CustomSnapshot(ReportId, UserId, SnapshotName, SnapshotFile, ExpireDate, ");
			sb.Append("Sorting, Notes)");
			sb.Append("VALUES (@ReportId, @UserId, @SnapshotName, @SnapshotFile, @ExpireDate, ");
			sb.Append("@Sorting, @Notes);");
			sb.Append("SET @SnapshotId = SCOPE_IDENTITY()");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
            customSnapshotInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomSnapshot", "Sorting", "ReportId", customSnapshotInfo.ReportId, 0) + 1;

            try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddOutParameter(dbCommand, "SnapshotId", DbType.Decimal,10);
					db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, customSnapshotInfo.ReportId);
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, customSnapshotInfo.UserId);
					db.AddInParameter(dbCommand, "SnapshotName", DbType.String, customSnapshotInfo.SnapshotName);
					db.AddInParameter(dbCommand, "SnapshotFile", DbType.String, customSnapshotInfo.SnapshotFile);
                    customSnapshotInfo.SnapshotFile = string.Format(customSnapshotInfo.SnapshotFile, customSnapshotInfo.Sorting);
                    db.AddInParameter(dbCommand, "ExpireDate", DbType.DateTime, customSnapshotInfo.ExpireDate);
					db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customSnapshotInfo.Sorting);
					db.AddInParameter(dbCommand, "Notes", DbType.String, customSnapshotInfo.Notes);
					//执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
					customSnapshotId= DataConvertionHelper.GetDecimal(dbCommand.Parameters["@SnapshotId"].Value, 0);
				}
			}
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customSnapshotId;
		}

        /// <summary>
		/// 获得 CustomSnapshotInfo 对象
		/// </summary>
		///<param name="snapshotId">快照编号</param>
		/// <returns> CustomSnapshotInfo 对象</returns>
		public CustomSnapshotInfo GetModelInfo(decimal snapshotId)
		{			
			CustomSnapshotInfo  customSnapshotInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("SnapshotId", "SnapshotId", DbType.Decimal, snapshotId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomSnapshotInfo>  customSnapshotInfos = GetModelInfos(whereConditons, null, true);
            if (customSnapshotInfos != null && customSnapshotInfos.Count > 0)
            {
                customSnapshotInfo = customSnapshotInfos[0];
            }

            return customSnapshotInfo;            
		}
        
        /// <summary>
		/// 更新 CustomSnapshotInfo 对象
		/// </summary>
		/// <param name="customSnapshotInfo">CustomSnapshotInfo 对象</param>
		public void Update(CustomSnapshotInfo customSnapshotInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE CustomSnapshot SET ReportId = @ReportId, UserId = @UserId, SnapshotName = @SnapshotName, ");
			sb.Append("SnapshotFile = @SnapshotFile, ExpireDate = @ExpireDate, Sorting = @Sorting, ");
			sb.Append("Notes = @Notes ");
			sb.Append("WHERE SnapshotId = @SnapshotId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "SnapshotId", DbType.Decimal, customSnapshotInfo.SnapshotId);
					db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, customSnapshotInfo.ReportId);
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, customSnapshotInfo.UserId);
					db.AddInParameter(dbCommand, "SnapshotName", DbType.String, customSnapshotInfo.SnapshotName);
					db.AddInParameter(dbCommand, "SnapshotFile", DbType.String, customSnapshotInfo.SnapshotFile);
					db.AddInParameter(dbCommand, "ExpireDate", DbType.DateTime, customSnapshotInfo.ExpireDate);
					db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customSnapshotInfo.Sorting);
					db.AddInParameter(dbCommand, "Notes", DbType.String, customSnapshotInfo.Notes);
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
		///  删除 CustomSnapshotInfo 对象
		/// </summary>
	    ///<param name="snapshotId">快照编号</param>
		public void Delete(decimal snapshotId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM CustomSnapshot ");
			sb.Append("WHERE SnapshotId = @SnapshotId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "SnapshotId", DbType.Decimal, snapshotId);
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
		/// 获得 CustomSnapshotInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomSnapshotInfo 对象列表</returns>
		public IList<CustomSnapshotInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 CustomSnapshot 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomSnapshotInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomSnapshot ", "SnapshotId", false, whereConditons);
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
        ///  插入记录与报表文件
        /// </summary>
        /// <param name="customSnapshotInfo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public decimal Insert(CustomSnapshotInfo customSnapshotInfo, byte[] data)
        {
            //自动增加的关键字的值
            decimal customCoverSnapshotId = Insert(customSnapshotInfo);

            try
            { 
                if (data != null && data.Length > 0)
                {
                    string defaultDir = GetDefaultSubDirOfReportingSnapshotFiles();
                    if (!Directory.Exists(defaultDir))
                    {
                        Directory.CreateDirectory(defaultDir.ToString());
                    }
                    StringBuilder sbPath = new StringBuilder();
                    sbPath.AppendFormat(@"{0}\{1}.xlsx", defaultDir, customSnapshotInfo.SnapshotFile);
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
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customCoverSnapshotId;
        }

        /// <summary>
        /// 下载快照数据
        /// </summary>
        public byte[] DownloadSnapshot(decimal snapshotId)
        {
            byte[] data = null;

            string defaultDir = GetDefaultSubDirOfReportingSnapshotFiles();
            string snapshotFile = GetSnapshotFile(snapshotId);
            StringBuilder sbPath = new StringBuilder();
            sbPath.AppendFormat(@"{0}\{1}.xlsx", defaultDir, snapshotFile);
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
        /// 获得统计类型快照列表
        /// </summary>
        /// <param name="coverId"></param>
        /// <returns></returns>
        public IList<CommonItem<decimal>> GetCommonItems(decimal reportId)
        {
            IList<CommonItem<decimal>> commonItems = new List<CommonItem<decimal>>();

            try
            {
                string sqlSelect = "SELECT SnapshotId, SnapshotName FROM CustomCoverSnapshot WHERE ReportId = @ReportId";

                //获得系统数据库对象
                Database db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, reportId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal snapshotId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string snapshotName = DataConvertionHelper.GetString(dataReader[1]);
                            commonItems.Add(new CommonItem<decimal>(snapshotName, snapshotId));
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

            return commonItems;
        }

        /// <summary>
        /// 获得基础类型快照列表
        /// </summary>
        /// <param name="coverId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CommonItem<decimal>> GetCommonItems(decimal reportId, decimal userId)
        {
            IList<CommonItem<decimal>> commonItems = new List<CommonItem<decimal>>();

            try
            {
                string sqlSelect = "SELECT SnapshotId, SnapshotName FROM CustomSnapshot WHERE ReportId = @ReportId AND UserId = @UserId";

                //获得系统数据库对象
                Database db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ReportId", DbType.Decimal, reportId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal snapshotId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string snapshotName = DataConvertionHelper.GetString(dataReader[1]);
                            commonItems.Add(new CommonItem<decimal>(snapshotName, snapshotId));
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

            return commonItems;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 CustomSnapshotInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomSnapshotInfo 对象列表</returns>
        private IList<CustomSnapshotInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CustomSnapshotInfo>  customSnapshotInfos = new List<CustomSnapshotInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CustomSnapshot");
            
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
							decimal snapshotId = DataConvertionHelper.GetDecimal(dataReader[0]);
							decimal reportId = DataConvertionHelper.GetDecimal(dataReader[1]);
							decimal userId = DataConvertionHelper.GetDecimal(dataReader[2]);
							string snapshotName = DataConvertionHelper.GetString(dataReader[3]);
							string snapshotFile = DataConvertionHelper.GetString(dataReader[4]);
							DateTime expireDate = DataConvertionHelper.GetDateTime(dataReader[5]);
							int sorting = DataConvertionHelper.GetInt(dataReader[6]);
							string notes = DataConvertionHelper.GetString(dataReader[7]);
							//将创建 CustomSnapshotInfo 对象加入集合中
							customSnapshotInfos.Add(new CustomSnapshotInfo(snapshotId, reportId, userId, snapshotName, snapshotFile, 
							expireDate, sorting, notes));							
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
            
			return customSnapshotInfos;
		}
        
		
		/// <summary>
		/// 获得 CustomSnapshotInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CustomSnapshotInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomSnapshot");
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
        /// 获得表 CustomSnapshot 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomSnapshot ", "SnapshotId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomSnapshot 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomSnapshot ", "SnapshotId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomSnapshot 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomSnapshot ", "SnapshotId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomSnapshot 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomSnapshot ", "SnapshotId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomSnapshotInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomSnapshot");
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
        /// 获得快照文件名称
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <returns></returns>
        private string GetSnapshotFile(decimal snapshotId)
        {
            string snapshotFile = string.Empty;

            try
            {
                string sqlSelect = "SELECT SnapshotFile FROM CustomSnapshot WHERE SnapshotId = @SnapshotId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "SnapshotId", DbType.Decimal, DataConvertionHelper.SetDecimal(snapshotId));
                    snapshotFile = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return snapshotFile;
        }

        /// <summary>
        /// 获得表套快照文件的默认目录
        /// </summary>
        /// <returns></returns>
        private string GetDefaultSubDirOfReportingSnapshotFiles()
        {
            StringBuilder sbPath = new StringBuilder();
            sbPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
            if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
            {
                sbPath.Append(@"\");
            }
            sbPath.Append(AppSettingHelper.DefaultSubDirOfReportingSnapshotFiles);

            return sbPath.ToString();
        }

        #endregion

        #endregion
    }
}
