//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomInterface.cs
// 描述: CustomInterface 数据层访问类
// 作者：ChenJie 
// 编写日期：2018/10/25
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
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.SQLServerDAL.BusinessModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// CustomInterface 表的数据层访问类
    /// </summary>
    public class CustomInterface : CommonNodeDataAccess, ICustomInterface
    {
        #region 构造函数

        /// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomInterface() : base("CustomInterface", "InterfaceId", "GroupId", "InterfaceName", "InterfaceCode", false, true)
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 CustomInterface 表中插入一条新记录
        /// </summary>
        /// <param name="customInterfaceInfo">customInterfaceInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomInterfaceInfo customInterfaceInfo)
        {
            //自动增加的关键字的值
            decimal customInterfaceId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            int sorting = DataAccessHandler.GetMaxValueOfDataField(db, "CustomInterface", "Sorting", "GroupId", customInterfaceInfo.GroupId, 0) + 1;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO CustomInterface(UserId, CombinedTableId, TableId, GroupId, InterfaceName, ");
            sb.Append("InterfaceCode, InterfaceIdentifier, TableType, UserTypeContained, DepContained, ");
            sb.Append("Actived, Sorting, Notes)");
            sb.Append("VALUES (@UserId, @CombinedTableId, @TableId, @GroupId, @InterfaceName, ");
            sb.Append("@InterfaceCode, @InterfaceIdentifier, @TableType, @UserTypeContained, @DepContained, ");
            sb.Append("@Actived, @Sorting, @Notes);");
            sb.Append("SET @InterfaceId = SCOPE_IDENTITY()");
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "InterfaceId", DbType.Decimal, 10);
                        db.AddInParameter(dbCommand, "UserId", DbType.Decimal, customInterfaceInfo.UserId);
                        db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customInterfaceInfo.CombinedTableId));
                        db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customInterfaceInfo.TableId));
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customInterfaceInfo.GroupId);
                        db.AddInParameter(dbCommand, "InterfaceName", DbType.String, customInterfaceInfo.InterfaceName);
                        db.AddInParameter(dbCommand, "InterfaceCode", DbType.String, customInterfaceInfo.InterfaceCode);
                        db.AddInParameter(dbCommand, "InterfaceIdentifier", DbType.String, customInterfaceInfo.InterfaceIdentifier);
                        db.AddInParameter(dbCommand, "TableType", DbType.Byte, customInterfaceInfo.TableType);
                        db.AddInParameter(dbCommand, "UserTypeContained", DbType.Boolean, customInterfaceInfo.UserTypeContained);
                        db.AddInParameter(dbCommand, "DepContained", DbType.Boolean, customInterfaceInfo.DepContained);
                        db.AddInParameter(dbCommand, "Actived", DbType.Boolean, customInterfaceInfo.Actived);
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, customInterfaceInfo.Notes);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        customInterfaceId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@InterfaceId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(customInterfaceInfo.GroupId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return customInterfaceId;
        }

        /// <summary>
		/// 获得 CustomInterfaceInfo 对象
		/// </summary>
		///<param name="interfaceId">接口编号</param>
		/// <returns> CustomInterfaceInfo 对象</returns>
		public CustomInterfaceInfo GetModelInfo(decimal interfaceId)
        {
            CustomInterfaceInfo customInterfaceInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("InterfaceId", "InterfaceId", DbType.Decimal, interfaceId, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomInterfaceInfo> customInterfaceInfos = GetModelInfos(whereConditons, null, true);
            if (customInterfaceInfos != null && customInterfaceInfos.Count > 0)
            {
                customInterfaceInfo = customInterfaceInfos[0];
            }

            return customInterfaceInfo;
        }

        /// <summary>
        /// 更新 CustomInterfaceInfo 对象
        /// </summary>
        /// <param name="customInterfaceInfo">CustomInterfaceInfo 对象</param>
        public void Update(CustomInterfaceInfo customInterfaceInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE CustomInterface SET UserId = @UserId, CombinedTableId = @CombinedTableId, TableId = @TableId, ");
            sb.Append("GroupId = @GroupId, InterfaceName = @InterfaceName, InterfaceCode = @InterfaceCode, ");
            sb.Append("InterfaceIdentifier = @InterfaceIdentifier, TableType = @TableType, UserTypeContained = @UserTypeContained, ");
            sb.Append("DepContained = @DepContained, Actived = @Actived, Notes = @Notes ");
            sb.Append("WHERE InterfaceId = @InterfaceId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "InterfaceId", DbType.Decimal, customInterfaceInfo.InterfaceId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, customInterfaceInfo.UserId);
                    db.AddInParameter(dbCommand, "CombinedTableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customInterfaceInfo.CombinedTableId));
                    db.AddInParameter(dbCommand, "TableId", DbType.Decimal, DataConvertionHelper.SetDecimal(customInterfaceInfo.TableId));
                    db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, customInterfaceInfo.GroupId);
                    db.AddInParameter(dbCommand, "InterfaceName", DbType.String, customInterfaceInfo.InterfaceName);
                    db.AddInParameter(dbCommand, "InterfaceCode", DbType.String, customInterfaceInfo.InterfaceCode);
                    db.AddInParameter(dbCommand, "InterfaceIdentifier", DbType.String, customInterfaceInfo.InterfaceIdentifier);
                    db.AddInParameter(dbCommand, "TableType", DbType.Byte, customInterfaceInfo.TableType);
                    db.AddInParameter(dbCommand, "UserTypeContained", DbType.Boolean, customInterfaceInfo.UserTypeContained);
                    db.AddInParameter(dbCommand, "DepContained", DbType.Boolean, customInterfaceInfo.DepContained);
                    db.AddInParameter(dbCommand, "Actived", DbType.Boolean, customInterfaceInfo.Actived);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, customInterfaceInfo.Notes);
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
        ///  删除 CustomInterfaceInfo 对象
        /// </summary>
        ///<param name="interfaceId">接口编号</param>
        public void Delete(decimal interfaceId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomInterface ");
            sb.Append("WHERE InterfaceId = @InterfaceId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "InterfaceId", DbType.Decimal, interfaceId);
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
        /// 获得 CustomInterfaceInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomInterfaceInfo 对象列表</returns>
        public IList<CustomInterfaceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 CustomInterface 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomInterfaceInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "CustomInterface ", "InterfaceId", false, whereConditons);
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
		/// 获得 CustomInterfaceInfo 对象
		/// </summary>
		///<param name="interfaceIdentifier">标识符编号</param>
		/// <returns> CustomInterfaceInfo 对象</returns>
		public CustomInterfaceInfo GetModelInfo(string interfaceIdentifier)
        {
            CustomInterfaceInfo customInterfaceInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("InterfaceIdentifier", "InterfaceIdentifier", DbType.String, interfaceIdentifier, DataFieldCondition.Equal));

            //创建集合对象
            IList<CustomInterfaceInfo> customInterfaceInfos = GetModelInfos(whereConditons, null, true);
            if (customInterfaceInfos != null && customInterfaceInfos.Count > 0)
            {
                customInterfaceInfo = customInterfaceInfos[0];
            }

            return customInterfaceInfo;
        }

        /// <summary>
        /// 标识符是否已经存在
        /// </summary>
        /// <param name="interfaceIdentifier"></param>
        /// <returns></returns>
        public bool IsExistedIdentifier(string interfaceIdentifier)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("InterfaceIdentifier", "InterfaceIdentifier", DbType.String, interfaceIdentifier, DataFieldCondition.Equal));
            int count = GetTotalCount(whereConditons);

            return count > 0;
        }

        /// <summary>
        /// 更新条件
        /// </summary>
        /// <param name="interfaceId"></param>
        /// <param name="userTypeIds"></param>
        /// <param name="departmentIds"></param>
        public void UpdateConditions(decimal interfaceId, IList<decimal> userTypeIds, IList<decimal> departmentIds)
        {

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            CustomInterfaceAndUserType customInterfaceAndUserType = new CustomInterfaceAndUserType();
            IList<decimal> oldUserTypeIds = customInterfaceAndUserType.GetSecondIds(interfaceId);
            CustomInterfaceAndDep customInterfaceAndDep = new CustomInterfaceAndDep();
            IList<decimal> oldDepartmentIds = customInterfaceAndDep.GetSecondIds(interfaceId);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    /* 1. 更新用户类型 */
                    CommonAccessHelper.Update(customInterfaceAndUserType, interfaceId, userTypeIds, oldUserTypeIds, db, transaction);

                    /* 2. 更新用户单位 */
                    CommonAccessHelper.Update(customInterfaceAndDep, interfaceId, departmentIds, oldDepartmentIds, db, transaction);
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

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 CustomInterfaceInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>CustomInterfaceInfo 对象列表</returns>
        private IList<CustomInterfaceInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<CustomInterfaceInfo> customInterfaceInfos = new List<CustomInterfaceInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM CustomInterface");

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
                            decimal interfaceId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal combinedTableId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            decimal tableId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[4]);
                            string interfaceName = DataConvertionHelper.GetString(dataReader[5]);
                            string interfaceCode = DataConvertionHelper.GetString(dataReader[6]);
                            string interfaceIdentifier = DataConvertionHelper.GetString(dataReader[7]);
                            byte tableType = DataConvertionHelper.GetByte(dataReader[8]);
                            bool userTypeContained = DataConvertionHelper.GetBoolean(dataReader[9]);
                            bool depContained = DataConvertionHelper.GetBoolean(dataReader[10]);
                            bool actived = DataConvertionHelper.GetBoolean(dataReader[11]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[12]);
                            string notes = DataConvertionHelper.GetString(dataReader[13]);
                            //将创建 CustomInterfaceInfo 对象加入集合中
                            customInterfaceInfos.Add(new CustomInterfaceInfo(interfaceId, userId, combinedTableId, tableId, groupId,
                            interfaceName, interfaceCode, interfaceIdentifier, tableType, userTypeContained,
                            depContained, actived, sorting, notes));
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

            return customInterfaceInfos;
        }


        /// <summary>
        /// 获得 CustomInterfaceInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomInterfaceInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM CustomInterface");
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
        /// 获得表 CustomInterface 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomInterface ", "InterfaceId", "*", false, false, startPosition,
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
        /// 获得以表 CustomInterface 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomInterface ", "InterfaceId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 CustomInterface 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "CustomInterface ", "InterfaceId", "*", false, false, startPosition,
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
        /// 获得以表 CustomInterface 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "CustomInterface ", "InterfaceId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  CustomInterfaceInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM CustomInterface");
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
