//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomPrint.cs
// 描述: CustomPrint 数据层访问类
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
using AppFramework.Reference.DataAccessLibrary;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.GeneralAffairModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomPrint 表的数据层访问类
    /// </summary>
    public class CustomPrint : CommonNodeDataAccess, ICustomPrint
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomPrint() : base("CustomPrint", "PrintId", "GroupId", "PrintName", "PrintCode", false, true)
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomPrint 表中插入一条新记录
        /// </summary>
        /// <param name="customPrintInfo">customPrintInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomPrintInfo customPrintInfo)
        {
            //自动增加的关键字的值
            decimal customPrintId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            customPrintInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomPrint", "Sorting", "GroupId", customPrintInfo.GroupId, 0) + 1;

            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomPrint(CombinedTableId, GroupId, TableId, PrintName, PrintCode, ");
            sb.Append("TableType, SystemDataField, PrintContent, PrintVisible, Sorting, Notes)");
            sb.Append("VALUES (@CombinedTableId, @GroupId, @TableId, @PrintName, @PrintCode, ");
            sb.Append("@TableType, @SystemDataField, @PrintContent, @PrintVisible, @Sorting, @Notes);");
            sb.Append("SET @PrintId = SCOPE_IDENTITY()");

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "PrintId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customPrintInfo.CombinedTableId));
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customPrintInfo.GroupId);
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customPrintInfo.TableId));
                        db.AddInParameter(dbCommand, "PrintName", DbType.String, customPrintInfo.PrintName);
                        db.AddInParameter(dbCommand, "PrintCode", DbType.String, customPrintInfo.PrintCode);
                        db.AddInParameter(dbCommand, "TableType", DbType.Byte, customPrintInfo.TableType);
                        db.AddInParameter(dbCommand, "SystemDataField", DbType.Int64, customPrintInfo.SystemDataField);
                        db.AddInParameter(dbCommand, "PrintContent", DbType.String, customPrintInfo.PrintContent);
                        db.AddInParameter(dbCommand, "PrintVisible", DbType.Boolean, customPrintInfo.PrintVisible);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customPrintInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customPrintInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customPrintId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@PrintId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(customPrintInfo.GroupId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customPrintId;
        }

        /// <summary>
		/// 获得 CustomPrintInfo 对象
		/// </summary>
		///<param name="printId">数据打印编号</param>
		/// <returns> CustomPrintInfo 对象</returns>
		public CustomPrintInfo GetModelInfo(decimal printId)
        {
            CustomPrintInfo customPrintInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("PrintId", "PrintId", DbType.Decimal, printId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomPrintInfo> customPrintInfos = GetModelInfos(whereConditons, null, true);
            if (customPrintInfos != null && customPrintInfos.Count > 0)
            {
                customPrintInfo = customPrintInfos[0];
            }

            return customPrintInfo;
        }

        /// <summary>
        /// 更新 CustomPrintInfo 对象
        /// </summary>
        /// <param name="customPrintInfo">CustomPrintInfo 对象</param>
        public void Update(CustomPrintInfo customPrintInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomPrint SET CombinedTableId = @CombinedTableId, GroupId = @GroupId, TableId = @TableId, ");
            sb.Append("PrintName = @PrintName, TableType = @TableType, ");
            sb.Append("SystemDataField = @SystemDataField, PrintVisible = @PrintVisible, ");
            sb.Append("Notes = @Notes WHERE PrintId = @PrintId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            byte oldTableType = GetTableType(customPrintInfo.PrintId);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    if (oldTableType != customPrintInfo.TableType)
                    {
                        CustomPrintAndDataField customPrintAndDataField = new CustomPrintAndDataField();
                        customPrintAndDataField.Delete(customPrintInfo.PrintId, db, transaction);
                    }
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, customPrintInfo.PrintId);
                        db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customPrintInfo.CombinedTableId));
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, DataConvertionHelper.SetDecimal(customPrintInfo.GroupId));
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customPrintInfo.TableId));
                        db.AddInParameter(dbCommand, "PrintName", DbType.String, customPrintInfo.PrintName);
                        db.AddInParameter(dbCommand, "TableType", DbType.Byte, customPrintInfo.TableType);
                        db.AddInParameter(dbCommand, "SystemDataField", DbType.Int64, customPrintInfo.SystemDataField);
                        db.AddInParameter(dbCommand, "PrintVisible", DbType.Boolean, customPrintInfo.PrintVisible);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customPrintInfo.Notes);
                        //执行更新操作
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
		///  删除 CustomPrintInfo 对象
		/// </summary>
	    ///<param name="printId">数据打印编号</param>
		public void Delete(decimal printId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM CustomPrint ");
			sb.Append("WHERE PrintId = @PrintId");

            CustomPrintAndDataField customPrintAndDataField = new CustomPrintAndDataField();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    customPrintAndDataField.Delete(printId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
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
		/// 获得 CustomPrintInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomPrintInfo 对象列表</returns>
		public IList<CustomPrintInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 CustomPrint 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomPrintInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomPrint ", "PrintId", false, whereConditons);
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
        /// 获得表的类型
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        public byte GetTableType(decimal printId)
        {
            byte tableType = 0;

            string sqlSelect = "SELECT TableType FROM CustomPrint WHERE PrintId = @PrintId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "PrintId", DbType.String, printId);
                    tableType = DataConvertionHelper.GetByte(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableType;
        }

        /// <summary>
        /// 更新打印内容
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="printContent"></param>
        /// <param name="upLoadFileInfos"></param>
        public void UpdatePrintContent(decimal printId, string printContent, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //生成更新语句
            string sqlUpdated = "UPDATE CustomPrint SET PrintContent = @PrintContent WHERE PrintId = @PrintId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdated))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printId);
                        db.AddInParameter(dbCommand, "PrintContent", DbType.String, printContent);
                        //执行更新操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("更新失败！");
                        }                        
                    }
                    PriavteAttachment messageAttachment = new PriavteAttachment();
                    messageAttachment.Update(printId, (byte)AttachmentCategory.PrintBusiness, upLoadFileInfos, db, transaction);
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
        /// 获得打印内容
        /// </summary>
        ///<param name="printId">打印编号</param>
        /// <returns> 打印内容</returns>
        public string GetPrintContent(decimal printId)
        {
            string printContent = string.Empty;

            try
            {
                string sqlSelect = "SELECT PrintContent FROM CustomPrint WHERE PrintId = @PrintId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, DataConvertionHelper.SetDecimal(printId));
                    printContent = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return printContent;
        }

        /// <summary>
        /// 获得打印系统字段
        /// </summary>
        ///<param name="printId">打印编号</param>
        /// <returns> 打印系统字段</returns>
        public Int64 GetSystemDataField(decimal printId)
        {
            Int64 systemDataField = 0;

            try
            {
                string sqlSelect = "SELECT SystemDataField FROM CustomPrint WHERE PrintId = @PrintId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, DataConvertionHelper.SetDecimal(printId));
                    systemDataField = DataConvertionHelper.GetLong(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return systemDataField;
        }

        /// <summary>
        /// 更新打印内容
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="printContent"></param>
        public void UpdatePrintContent(decimal printId, string printContent)
        {
            //更新语句
            string sqlUpdate = "UPDATE CustomPrint SET PrintContent = @PrintContent WHERE PrintId = @PrintId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "PrintId", DbType.Decimal, printId);
                    db.AddInParameter(dbCommand, "PrintContent", DbType.String, printContent);

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
        /// 获得满足条件的打印对象
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal groupId, bool visible)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("GroupId", "GroupId", DbType.Decimal, groupId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            whereConditons.Add(new WhereConditon("PrintVisible", "PrintVisible", DbType.Boolean, visible, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));

            return GetCommonNodesByWhereConditon(whereConditons);
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
            sb.Append("DELETE CustomPrint FROM CustomPrint WHERE TableId = @TableId;");
            
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
        /// 获得 CustomPrintInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomPrintInfo 对象列表</returns>
        private IList<CustomPrintInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<CustomPrintInfo>  customPrintInfos = new List<CustomPrintInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CustomPrint");
            
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
                            decimal combinedTableId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            string printName = DataConvertionHelper.GetString(dataReader[4]);
                            string printCode = DataConvertionHelper.GetString(dataReader[5]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[6]);
                            long systemDataField = DataConvertionHelper.GetLong(dataReader[7]);
                            string printContent = DataConvertionHelper.GetString(dataReader[8]);
                            bool printVisible = DataConvertionHelper.GetBoolean(dataReader[9]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[10]);
                            string notes = DataConvertionHelper.GetString(dataReader[11]);
                            //将创建 CustomPrintInfo 对象加入集合中
                            customPrintInfos.Add(new CustomPrintInfo(printId, combinedTableId, groupId, tableId, printName,
                            printCode, tableType, systemDataField, printContent, printVisible,
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
            
			return customPrintInfos;
		}
        
		
		/// <summary>
		/// 获得 CustomPrintInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>CustomPrintInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomPrint");
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
        /// 获得表 CustomPrint 的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomPrint ", "PrintId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomPrint 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomPrint ", "PrintId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomPrint 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomPrint ", "PrintId", "*", false, false, startPosition, 
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
        /// 获得以表 CustomPrint 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomPrint ", "PrintId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomPrintInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomPrint");
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
