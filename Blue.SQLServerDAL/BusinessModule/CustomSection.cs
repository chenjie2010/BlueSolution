//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSection.cs
// 描述: CustomSection 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/8/13
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
    /// CustomSection 表的数据层访问类
    /// </summary>
    public class CustomSection : CommonNodeDataAccess, ICustomSection
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomSection() : base("CustomSection", "SectionId", "DataId", "SectionName", "SectionCode")
        {
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 CustomSection 表中插入一条新记录
		/// </summary>
		/// <param name="customSectionInfo">customSectionInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomSectionInfo customSectionInfo)
		{
			//自动增加的关键字的值
			decimal customSectionId= 0;
			//生成插入语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("INSERT INTO CustomSection(DataId, SectionName, SectionCode, IsLeaf, Sorting, ");
			sb.Append("Notes)");
			sb.Append("VALUES (@DataId, @SectionName, @SectionCode, @IsLeaf, @Sorting, ");
			sb.Append("@Notes);");
			sb.Append("SET @SectionId = SCOPE_IDENTITY()");

			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
            customSectionInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomSection", "Sorting", "DataId", customSectionInfo.DataId, 0) + 1;

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "SectionId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "DataId", DbType.Decimal, customSectionInfo.DataId);
                        db.AddInParameter(dbCommand, "SectionName", DbType.String, customSectionInfo.SectionName);
                        db.AddInParameter(dbCommand, "SectionCode", DbType.String, customSectionInfo.SectionCode);
                        db.AddInParameter(dbCommand, "IsLeaf", DbType.Boolean, true);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customSectionInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customSectionInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customSectionId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@SectionId"].Value, 0);
                    }
                    CustomData customData = new CustomData();
                    customData.UpdateLeafOfParentNode(customSectionInfo.DataId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
            
			return customSectionId;
		}

        /// <summary>
		/// 获得 CustomSectionInfo 对象
		/// </summary>
		///<param name="sectionId">窗体分组编号</param>
		/// <returns> CustomSectionInfo 对象</returns>
		public CustomSectionInfo GetModelInfo(decimal sectionId)
		{			
			CustomSectionInfo  customSectionInfo = null;
			//生成选择语句
			StringBuilder sb = new StringBuilder();		
			sb.Append("SELECT DataId, SectionName, SectionCode, IsLeaf, Sorting, ");
			sb.Append("Notes ");
			sb.Append("FROM CustomSection ");
			sb.Append("WHERE SectionId = @SectionId");
			try
			{
				//获得系统数据库对象
				SqlDatabase db = DataAccessHelper.GetDatabase();
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "SectionId", DbType.Decimal, DataConvertionHelper.SetDecimal(sectionId));
					using (IDataReader dataReader = db.ExecuteReader(dbCommand))
					{
						if (dataReader.Read())
						{
							decimal dataId = DataConvertionHelper.GetDecimal(dataReader[0]);
							string sectionName = DataConvertionHelper.GetString(dataReader[1]);
							string sectionCode = DataConvertionHelper.GetString(dataReader[2]);
							bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[3]);
							int sorting = DataConvertionHelper.GetInt(dataReader[4]);
							string notes = DataConvertionHelper.GetString(dataReader[5]);
							//创建 CustomSectionInfo 对象
							customSectionInfo = new CustomSectionInfo(sectionId, dataId, sectionName, sectionCode, isLeaf, 
							sorting, notes);
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
            
			return customSectionInfo;
		}
        
        /// <summary>
		/// 更新 CustomSectionInfo 对象
		/// </summary>
		/// <param name="customSectionInfo">CustomSectionInfo 对象</param>
		public void Update(CustomSectionInfo customSectionInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE CustomSection SET DataId = @DataId, SectionName = @SectionName, SectionCode = @SectionCode, Notes = @Notes ");
			sb.Append("WHERE SectionId = @SectionId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "SectionId", DbType.Decimal, customSectionInfo.SectionId);
					db.AddInParameter(dbCommand, "DataId", DbType.Decimal, customSectionInfo.DataId);
					db.AddInParameter(dbCommand, "SectionName", DbType.String, customSectionInfo.SectionName);
					db.AddInParameter(dbCommand, "SectionCode", DbType.String, customSectionInfo.SectionCode);
					db.AddInParameter(dbCommand, "Notes", DbType.String, customSectionInfo.Notes);
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
		///  删除 CustomSectionInfo 对象
		/// </summary>
	    ///<param name="sectionId">窗体分组编号</param>
		public void Delete(decimal sectionId)
		{
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomSection ");
            sb.Append("WHERE SectionId = @SectionId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    CustomForm customForm = new CustomForm();
                    customForm.Delete(sectionId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "SectionId", DbType.Decimal, DataConvertionHelper.SetDecimal(sectionId));
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
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
        }       
        
        /// <summary>
		/// 获得 CustomSectionInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomSectionInfo 对象列表</returns>
		public IList<CustomSectionInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 CustomSection 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomSectionInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomSection ", "SectionId", false, whereConditons);
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
        /// 获得所有的窗体
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public IList<CustomSectionInfo> GetModelInfos(decimal dataId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("DataId", "DataId", DbType.Decimal, dataId, DataFieldCondition.Equal));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 删除 CustomSectionInfo 对象
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
		public void Delete(decimal dataId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomSection ");
            sb.Append("WHERE DataId = @DataId");
           
            try
            {
                IList<decimal> childNodeIds = GetCommonNodeIds(dataId, db, transaction);
                CustomForm customForm = new CustomForm();
                foreach (var childNodeId in childNodeIds)
                {
                    customForm.Delete(childNodeId, db, transaction);
                }
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "DataId", DbType.Decimal, DataConvertionHelper.SetDecimal(dataId));
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
        /// 获得 CustomSectionInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomSectionInfo 对象列表</returns>
        private IList<CustomSectionInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CustomSectionInfo>  customSectionInfos = new List<CustomSectionInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CustomSection");
            
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
							decimal sectionId = DataConvertionHelper.GetDecimal(dataReader[0]);
							decimal dataId = DataConvertionHelper.GetDecimal(dataReader[1]);
							string sectionName = DataConvertionHelper.GetString(dataReader[2]);
							string sectionCode = DataConvertionHelper.GetString(dataReader[3]);
							bool isLeaf = DataConvertionHelper.GetBoolean(dataReader[4]);
							int sorting = DataConvertionHelper.GetInt(dataReader[5]);
							string notes = DataConvertionHelper.GetString(dataReader[6]);
							//将创建 CustomSectionInfo 对象加入集合中
							customSectionInfos.Add(new CustomSectionInfo(sectionId, dataId, sectionName, sectionCode, isLeaf, 
							sorting, notes));							
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
            
			return customSectionInfos;
		}
        
		
		/// <summary>
		/// 获得 CustomSectionInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CustomSectionInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomSection");
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
        /// 获得表 CustomSection 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomSection ", "SectionId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomSection 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomSection ", "SectionId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomSection 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomSection ", "SectionId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomSection 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomSection ", "SectionId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomSectionInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomSection");
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
