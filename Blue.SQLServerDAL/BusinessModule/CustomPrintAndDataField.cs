//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomPrintAndDataField.cs
// 描述: CustomPrintAndDataField 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/9/28
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
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomPrintAndDataField 表的数据层访问类
    /// </summary>
    public class CustomPrintAndDataField : CorrelatedTableDataAcess, ICustomPrintAndDataField
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomPrintAndDataField() : base("CustomPrintAndDataField", "PrintId", "DataFieldId")
        {
		}
        
		#endregion

        #region 实现默认接口

        /// <summary>
		/// 获得 CustomPrintAndDataFieldInfo 对象
		/// </summary>
		///<param name="printId">数据打印编号</param>
		///<param name="dataFieldId">字段编号</param>
		/// <returns> CustomPrintAndDataFieldInfo 对象</returns>
		public CustomPrintAndDataFieldInfo GetModelInfo(decimal printId, decimal dataFieldId)
		{			
			CustomPrintAndDataFieldInfo  customPrintAndDataFieldInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("PrintId", "PrintId", DbType.Decimal, printId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomPrintAndDataFieldInfo>  customPrintAndDataFieldInfos = GetModelInfos(whereConditons, null, true);
            if (customPrintAndDataFieldInfos != null && customPrintAndDataFieldInfos.Count > 0)
            {
                customPrintAndDataFieldInfo = customPrintAndDataFieldInfos[0];
            }

            return customPrintAndDataFieldInfo;            
		}
        
        /// <summary>
		/// 更新 CustomPrintAndDataFieldInfo 对象
		/// </summary>
		/// <param name="customPrintAndDataFieldInfo">CustomPrintAndDataFieldInfo 对象</param>
		public void Update(CustomPrintAndDataFieldInfo customPrintAndDataFieldInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE CustomPrintAndDataField SET Sorting = @Sorting ");
			sb.Append("WHERE PrintId = @PrintId AND DataFieldId = @DataFieldId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, customPrintAndDataFieldInfo.PrintId);
					db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, customPrintAndDataFieldInfo.DataFieldId);
					db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customPrintAndDataFieldInfo.Sorting);
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
		///  删除 CustomPrintAndDataFieldInfo 对象
		/// </summary>
	    ///<param name="printId">数据打印编号</param>
		///<param name="dataFieldId">字段编号</param>
		public void Delete(decimal printId, decimal dataFieldId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM CustomPrintAndDataField ");
			sb.Append("WHERE PrintId = @PrintId AND DataFieldId = @DataFieldId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printId);
					db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, dataFieldId);
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
		/// 获得 CustomPrintAndDataFieldInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomPrintAndDataFieldInfo 对象列表</returns>
		public IList<CustomPrintAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 CustomPrintAndDataField 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomPrintAndDataFieldInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomPrintAndDataField ", "PrintId", false, whereConditons);
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
        /// 更新表的字段集合
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <param name="customPrintAndDataFieldInfos"></param>
        public void UpdateDataFields(decimal printId, byte dataFieldPrintType, IList<CustomPrintAndDataFieldInfo> customPrintAndDataFieldInfos)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("PrintId", "PrintId", DbType.Decimal, printId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("DataFieldPrintType", "DataFieldPrintType", DbType.Byte, dataFieldPrintType, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
            
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));
            IList<CustomPrintAndDataFieldInfo> oldCustomPrintAndDataFieldInfos = GetModelInfos(whereConditons, sortingCondtions);

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    /* 1. 不存在则插入，存在则更新 */
                    foreach (var customPrintAndDataFieldInfo in customPrintAndDataFieldInfos)
                    {
                        bool find = false;
                        foreach (var oldCustomPrintAndDataFieldInfo in oldCustomPrintAndDataFieldInfos)
                        {
                            if (customPrintAndDataFieldInfo.PrintId == oldCustomPrintAndDataFieldInfo.PrintId
                                && customPrintAndDataFieldInfo.DataFieldId == oldCustomPrintAndDataFieldInfo.DataFieldId)
                            {
                                find = true;
                                if (customPrintAndDataFieldInfo.Sorting != customPrintAndDataFieldInfo.Sorting)
                                {
                                    Update(customPrintAndDataFieldInfo, db, transaction);
                                }
                                break;
                            }
                        }
                        if (!find)
                        {
                            Insert(customPrintAndDataFieldInfo, db, transaction);
                        }
                    }
                    /* 2. 存在则忽略，不存在则删除*/
                    foreach (var oldCustomPrintAndDataFieldInfo in oldCustomPrintAndDataFieldInfos)
                    {
                        bool find = false;
                        foreach (var customPrintAndDataFieldInfo in customPrintAndDataFieldInfos)
                        {
                            if (customPrintAndDataFieldInfo.PrintId == oldCustomPrintAndDataFieldInfo.PrintId
                                && customPrintAndDataFieldInfo.DataFieldId == oldCustomPrintAndDataFieldInfo.DataFieldId)
                            {
                                find = true;
                                break;
                            }
                        }
                        if (!find)
                        {
                            Delete(oldCustomPrintAndDataFieldInfo.PrintId, oldCustomPrintAndDataFieldInfo.DataFieldId, db, transaction);
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
        /// 获得打印字段
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDataFields(decimal printId, byte dataFieldPrintType)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomPrintAndDataField.DataFieldId, CustomTable.LogicalName, CustomDataField.LogicalName, CustomDataField.PhysicalName, CustomDataField.DataFieldProperty FROM CustomPrintAndDataField ");
            sb.Append("INNER JOIN CustomDataField ON CustomPrintAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("INNER JOIN CustomTable ON CustomTable.TableId = CustomDataField.TableId ");
            sb.Append("WHERE PrintId = @PrintId AND DataFieldPrintType = @DataFieldPrintType ORDER BY CustomPrintAndDataField.Sorting");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printId);
                    db.AddInParameter(dbCommand, "DataFieldPrintType", DbType.Byte, dataFieldPrintType);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string logicalName = string.Format("[{0}][{1}]", DataConvertionHelper.GetString(dataReader[1]),
                                DataConvertionHelper.GetString(dataReader[2]));
                            string physicalName = DataConvertionHelper.GetString(dataReader[3]);
                            byte dataFieldProperty = DataConvertionHelper.GetByte(dataReader[4]);
                            commonNodes.Add(new CommonNode(dataFieldId, printId, logicalName, physicalName, dataFieldProperty));
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

            return commonNodes;
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
            sb.Append("DELETE CustomPrintAndDataField FROM CustomPrintAndDataField INNER JOIN CustomDataField ");
            sb.Append("ON CustomPrintAndDataField.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE CustomDataField.TableId = @TableId ");

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
        /// 获得 CustomPrintAndDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomPrintAndDataFieldInfo 对象列表</returns>
        private IList<CustomPrintAndDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CustomPrintAndDataFieldInfo>  customPrintAndDataFieldInfos = new List<CustomPrintAndDataFieldInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CustomPrintAndDataField");
            
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
                            decimal printId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            byte dataFieldPrintType = DataConvertionHelper.GetByte(dataReader[2]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[3]);
                            //将创建 CustomPrintAndDataFieldInfo 对象加入集合中
                            customPrintAndDataFieldInfos.Add(new CustomPrintAndDataFieldInfo(printId, dataFieldId, dataFieldPrintType, sorting));
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
            
			return customPrintAndDataFieldInfos;
		}
        
		
		/// <summary>
		/// 获得 CustomPrintAndDataFieldInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CustomPrintAndDataFieldInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomPrintAndDataField");
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
        /// 获得表 CustomPrintAndDataField 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomPrintAndDataField ", "PrintId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomPrintAndDataField 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomPrintAndDataField ", "PrintId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomPrintAndDataField 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomPrintAndDataField ", "PrintId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomPrintAndDataField 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomPrintAndDataField ", "PrintId", "*", false, false, tableLinks, startPosition, 
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
		/// 删除满足条件的的 CustomPrintAndDataFieldInfo 对象
		/// </summary>
	    /// <param name="printId">数据打印编号</param>
		/// <returns>返回删除的记录数目数目</returns>
		private int Delete(decimal printId)
		{
			int count = 0; 
			//删除语句
			string sqlDelete = "DELETE FROM CustomPrintAndDataField WHERE PrintId = @PrintId";
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
				{
					db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printId);
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
        /// 删除满足条件的所有  CustomPrintAndDataFieldInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomPrintAndDataField");
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
        /// 向 CustomPrintAndDataField 表中插入一条新记录
        /// </summary>
        /// <param name="customPrintAndDataFieldInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Insert(CustomPrintAndDataFieldInfo customPrintAndDataFieldInfo, SqlDatabase db, DbTransaction transaction)
        {           
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomPrintAndDataField(PrintId, DataFieldId, DataFieldPrintType, Sorting)");
            sb.Append("VALUES (@PrintId, @DataFieldId, @DataFieldPrintType, @Sorting)");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, customPrintAndDataFieldInfo.PrintId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, customPrintAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "DataFieldPrintType", DbType.Byte, customPrintAndDataFieldInfo.DataFieldPrintType);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customPrintAndDataFieldInfo.Sorting);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
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
        /// 更新 CustomPrintAndDataFieldInfo 对象
        /// </summary>
        /// <param name="customPrintAndDataFieldInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
		public void Update(CustomPrintAndDataFieldInfo customPrintAndDataFieldInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomPrintAndDataField SET Sorting = @Sorting, DataFieldPrintType = @DataFieldPrintType ");
            sb.Append("WHERE PrintId = @PrintId AND DataFieldId = @DataFieldId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, customPrintAndDataFieldInfo.PrintId);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, customPrintAndDataFieldInfo.DataFieldId);
                    db.AddInParameter(dbCommand, "DataFieldPrintType", DbType.Byte, customPrintAndDataFieldInfo.DataFieldPrintType);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customPrintAndDataFieldInfo.Sorting);
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
