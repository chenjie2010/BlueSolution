//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomExpression.cs
// 描述：CustomExpression 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/9/11
// Copyright 2016
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
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Core;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.SQLServerDAL.BusinessModule
{
    /// <summary>
    /// CustomExpression 表的数据层访问类
    /// </summary>
    public class CustomExpression : ICustomExpression
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomExpression()
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomExpression 表中插入一条新记录
        /// </summary>
        /// <param name="customExpressionInfo">customExpressionInfo 对象</param>
        public void Insert(CustomExpressionInfo customExpressionInfo)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomExpression", "Sorting", "ParentDataFieldId", customExpressionInfo.ParentDataFieldId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomExpression(ParentDataFieldId, DataFieldId)");
            sb.Append("VALUES (@ParentDataFieldId, @DataFieldId);");
            
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, customExpressionInfo.ParentDataFieldId);
                    db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                    db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, customExpressionInfo.DataFieldId);

                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
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
		/// 获得 CustomExpressionInfo 对象
		/// </summary>
		///<param name="parentDataFieldId">字段编号</param>
		///<param name="sorting">排序</param>
		/// <returns> CustomExpressionInfo 对象</returns>
		public CustomExpressionInfo GetModelInfo(decimal parentDataFieldId, int sorting)
        {
            CustomExpressionInfo customExpressionInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("ParentDataFieldId", "ParentDataFieldId", System.Data.DbType.Decimal, parentDataFieldId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("Sorting", "Sorting", System.Data.DbType.Int32, sorting, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomExpressionInfo> customExpressionInfos = GetModelInfos(whereConditons, null, true);
            if (customExpressionInfos != null && customExpressionInfos.Count > 0)
            {
                customExpressionInfo = customExpressionInfos[0];
            }

            return customExpressionInfo;
        }

        /// <summary>
        ///  删除 CustomExpressionInfo 对象
        /// </summary>
        ///<param name="parentDataFieldId">字段编号</param>
        ///<param name="sorting">排序</param>
        public void Delete(decimal parentDataFieldId, int sorting)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomExpression ");
            sb.Append("WHERE ParentDataFieldId = @ParentDataFieldId AND Sorting = @Sorting");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, parentDataFieldId);
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
        /// 获得 CustomExpressionInfo 对象的列表
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns></returns>
        public IList<CustomExpressionInfo> GetModelInfos(decimal parentDataFieldId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("ParentDataFieldId", "ParentDataFieldId", DbType.Decimal, parentDataFieldId, DataFieldCondition.Equal));
            IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
            sortingCondtions.Add(new SortingCondtion("Sorting", CustomSorting.Ascending));

            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomExpression 表中记录的数目
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns>CustomExpressionInfo 记录的数目</returns>
        public int GetTotalCount(decimal parentDataFieldId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("ParentDataFieldId", "ParentDataFieldId", DbType.Decimal, parentDataFieldId, DataFieldCondition.Equal));

            return GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        #region 实现新增接口   

        /// <summary>
        /// 获得表达式相关的字段节点列表
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal parentDataFieldId)
        {
            IList<CommonNode> commonNdes = new List<CommonNode>();
            //生成查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CustomDataField.DataFieldId, LogicalName, PhysicalName FROM CustomExpression ");
            sb.Append("LEFT JOIN CustomDataField ON CustomExpression.DataFieldId = CustomDataField.DataFieldId ");
            sb.Append("WHERE CustomExpression.ParentDataFieldId = @ParentDataFieldId ORDER BY CustomExpression.Sorting  ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, parentDataFieldId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string logicalName = DataConvertionHelper.GetString(dataReader[1]);
                            string physicalName = DataConvertionHelper.GetString(dataReader[2]);
                            commonNdes.Add(new CommonNode(dataFieldId, parentDataFieldId, logicalName, physicalName, true, (byte)DataFieldProperty.PhysicalDataField));
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


            return commonNdes;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 插入表达式
        /// </summary>
        /// <param name="customExpressionInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Insert(IList<CustomExpressionInfo> customExpressionInfos, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomExpression(ParentDataFieldId, Sorting, DataFieldId)");
            sb.Append("VALUES (@ParentDataFieldId, @Sorting, @DataFieldId);");

            try
            {
                foreach (CustomExpressionInfo customExpressionInfo in customExpressionInfos)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, customExpressionInfo.ParentDataFieldId);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, customExpressionInfo.Sorting);
                        db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(customExpressionInfo.DataFieldId));
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
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
        /// 插入表达式
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <param name="customExpressionInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Insert(decimal parentDataFieldId, IList<CustomExpressionInfo> customExpressionInfos, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomExpression(ParentDataFieldId, Sorting, DataFieldId)");
            sb.Append("VALUES (@ParentDataFieldId, @Sorting, @DataFieldId);");

            try
            {
                int sorting = 1;
                foreach (CustomExpressionInfo customExpressionInfo in customExpressionInfos)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, parentDataFieldId);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting++);
                        db.AddInParameter(dbCommand, "DataFieldId", DbType.Decimal, DataConvertionHelper.SetDecimal(customExpressionInfo.DataFieldId));
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
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
        /// 更新表达式
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <param name="customExpressionInfos"></param>
        /// <param name="transaction"></param>
        public void Update(decimal parentDataFieldId, IList<CustomExpressionInfo> customExpressionInfos, DbTransaction transaction)
        {
            //生成删除语句
             string sqlDelete = "DELETE FROM CustomExpression WHERE ParentDataFieldId = @ParentDataFieldId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, parentDataFieldId);
                    //执行删除操作
                    db.ExecuteNonQuery(dbCommand, transaction);
                }
                Insert(parentDataFieldId, customExpressionInfos, db, transaction);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 CustomExpression 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomExpressionInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomExpression ", "ParentDataFieldId", false, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 删除满足条件的的 CustomExpressionInfo 对象
        /// </summary>
        /// <param name="parentDataFieldId">字段编号</param>
        /// <param name="transaction">事务</param>
        /// <returns>返回删除的记录数目数目</returns>
        public int Delete(decimal parentDataFieldId, DbTransaction transaction)
        {
            int count = 0;
            //删除语句
            string sqlDelete = "DELETE FROM CustomExpression WHERE ParentDataFieldId = @ParentDataFieldId";
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                {
                    db.AddInParameter(dbCommand, "ParentDataFieldId", DbType.Decimal, parentDataFieldId);
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

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 CustomExpressionInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomExpressionInfo 对象列表</returns>
        private IList<CustomExpressionInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomExpressionInfo> customExpressionInfos = new List<CustomExpressionInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM CustomExpression");
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
                            decimal parentDataFieldId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[1]);
                            decimal dataFieldId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            //将创建 CustomExpressionInfo 对象加入集合中
                            customExpressionInfos.Add(new CustomExpressionInfo(dataFieldId, sorting, parentDataFieldId));
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

            return customExpressionInfos;
        }

        /// <summary>
        /// 获得 CustomExpressionInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomExpressionInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomExpression");
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
        /// 获得表 CustomExpression 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomExpression ", "ParentDataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 CustomExpression 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomExpression ", "ParentDataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomExpression 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomExpression ", "ParentDataFieldId", "*", false, false, startPosition,
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
        /// 获得以表 CustomExpression 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomExpression ", "ParentDataFieldId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomExpressionInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomExpression");
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
