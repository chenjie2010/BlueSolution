//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserType.cs
// 描述：UserType 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/8/19
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
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Core;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.SQLServerDAL.BusinessModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// UserType 表的数据层访问类
    /// </summary>
    public class UserType : CommonNodeDataAccess, IUserType
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserType() : base("UserType", "UserTypeId", "GroupId", "UserTypeName", "UserTypeCode", false, true)
        {

        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 向 UserType 表中插入一条新记录
        /// </summary>
        /// <param name="userTypeInfo">userTypeInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(UserTypeInfo userTypeInfo)
        {
            //自动增加的关键字的值
            decimal userTypeId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO UserType(GroupId, UserTypeName, UserTypeCode, FirstCode, SecondCode, IsSystemUserType, ");
            sb.Append("IsVisibleForInterface, Sorting, Notes, CreatedTime, UpdatedTime)");
            sb.Append("VALUES (@GroupId, @UserTypeName, @UserTypeCode, @FirstCode, @SecondCode, @IsSystemUserType, ");
            sb.Append("@IsVisibleForInterface, @Sorting, @Notes, @CreatedTime, @UpdatedTime);");
            sb.Append("SET @UserTypeId = SCOPE_IDENTITY()");
            DateTime time = DateTime.Now;
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddOutParameter(dbCommand, "UserTypeId", DbType.Decimal, 8);
                        db.AddInParameter(dbCommand, "GroupId", DbType.Decimal, DataConvertionHelper.SetDecimal(userTypeInfo.GroupId));
                        db.AddInParameter(dbCommand, "UserTypeName", DbType.String, userTypeInfo.UserTypeName);
                        db.AddInParameter(dbCommand, "UserTypeCode", DbType.String, userTypeInfo.UserTypeCode);
                        db.AddInParameter(dbCommand, "FirstCode", DbType.String, userTypeInfo.FirstCode);
                        db.AddInParameter(dbCommand, "SecondCode", DbType.String, userTypeInfo.SecondCode);
                        db.AddInParameter(dbCommand, "IsSystemUserType", DbType.Boolean, userTypeInfo.IsSystemUserType);
                        db.AddInParameter(dbCommand, "IsVisibleForInterface", DbType.Boolean, userTypeInfo.IsVisibleForInterface);
                        userTypeInfo.Sorting = DataAccessHandler.GetMaxValueOfDataField(db, "UserType", "Sorting", "GroupId", userTypeInfo.GroupId, 0) + 1;
                        db.AddInParameter(dbCommand, "Sorting", DbType.Int32, userTypeInfo.Sorting);
                        db.AddInParameter(dbCommand, "Notes", DbType.String, userTypeInfo.Notes);
                        db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, time);
                        db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, time);
                        //执行插入操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("插入失败！");
                        }
                        userTypeId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@UserTypeId"].Value, 0);
                    }
                    CustomGroup customGroup = new CustomGroup();
                    customGroup.UpdateLeafOfParentNode(userTypeInfo.GroupId, false, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return userTypeId;
        }

        /// <summary>
		/// 获得 UserTypeInfo 对象
		/// </summary>
		///<param name="userTypeId">用户类型编号</param>
		/// <returns> UserTypeInfo 对象</returns>
		public UserTypeInfo GetModelInfo(decimal userTypeId)
        {
            UserTypeInfo userTypeInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("UserTypeId", "UserTypeId", System.Data.DbType.Decimal, userTypeId, DataFieldCondition.Equal));

            //创建集合对象
            IList<UserTypeInfo> userTypeInfos = GetModelInfos(whereConditons, null, true);
            if (userTypeInfos != null && userTypeInfos.Count > 0)
            {
                userTypeInfo = userTypeInfos[0];
            }

            return userTypeInfo;
        }

        /// <summary>
        /// 更新 UserTypeInfo 对象
        /// </summary>
        /// <param name="userTypeInfo">UserTypeInfo 对象</param>
        public void Update(UserTypeInfo userTypeInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserType SET UserTypeName = @UserTypeName, UserTypeCode = @UserTypeCode, ");
            sb.Append("FirstCode = @FirstCode, SecondCode = @SecondCode, IsSystemUserType = @IsSystemUserType, ");
            sb.Append("IsVisibleForInterface = @IsVisibleForInterface, Notes = @Notes, UpdatedTime = @UpdatedTime ");
            sb.Append("WHERE UserTypeId = @UserTypeId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, userTypeInfo.UserTypeId);
                    db.AddInParameter(dbCommand, "UserTypeName", DbType.String, userTypeInfo.UserTypeName);
                    db.AddInParameter(dbCommand, "UserTypeCode", DbType.String, userTypeInfo.UserTypeCode);
                    db.AddInParameter(dbCommand, "FirstCode", DbType.String, userTypeInfo.FirstCode);
                    db.AddInParameter(dbCommand, "SecondCode", DbType.String, userTypeInfo.SecondCode);
                    db.AddInParameter(dbCommand, "IsSystemUserType", DbType.Boolean, userTypeInfo.IsSystemUserType);
                    db.AddInParameter(dbCommand, "IsVisibleForInterface", DbType.Boolean, userTypeInfo.IsVisibleForInterface);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, userTypeInfo.Notes);
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DateTime.Now);
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
        ///  删除 UserTypeInfo 对象
        /// </summary>
        ///<param name="userTypeId">用户类型编号</param>
        public void Delete(decimal userTypeId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserType ");
            sb.Append("WHERE UserTypeId = @UserTypeId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            bool updateLeaf = true;
            decimal groupId = GetParentNodeId(userTypeId);
            int count = GetTotalCountOfChildNode(groupId);
            if (count > 1)
            {
                updateLeaf = false;
            }
            UserTypeScope userTypeScope = new UserTypeScope();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    userTypeScope.DeleteBySecondForeignKey(userTypeId, db, transaction);
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, userTypeId);
                        //执行删除操作
                        if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                        {
                            throw new Exception("删除失败！");
                        }
                    }                    
                    if (updateLeaf)
                    {
                        CustomGroup customGroup = new CustomGroup();
                        customGroup.UpdateLeafOfParentNode(groupId, true, db, transaction);
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
        /// 获得 UserTypeInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserTypeInfo 对象列表</returns>
        public IList<UserTypeInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 UserType 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserTypeInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "UserType ", "UserTypeId", false, whereConditons);
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
        /// 根据系统条件获得用户类型
        /// </summary>
        /// <param name="isSystemUserType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(bool isSystemUserType)
        {
            IList<CommonNode> commonNodes = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();            
            whereConditons.Add(new WhereConditon("IsSystemUserType", "IsSystemUserType", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            commonNodes = GetCommonNodesByWhereConditon(whereConditons);

            return commonNodes;
        }

        /// <summary>
        /// 获得用户类型数量
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public int GetUserTypeCount(DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                if (!DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UpdatedTime", "UpdatedTime_0", DbType.DateTime, fromUpdatedTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And));
                }
                if (!DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UpdatedTime", "UpdatedTime_1", DbType.DateTime, toUpdatedTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And));
                }
                //whereConditons.Add(new WhereConditon("IsSystemUserType", "IsSystemUserType", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("IsVisibleForInterface", "IsVisibleForInterface", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                count = DataAccessHandler.GetRecordCount(db, "UserType", whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得用户类型分页数据
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public DataTable GetUserTypeData(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                if (!DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UpdatedTime", "UpdatedTime_0", DbType.DateTime, fromUpdatedTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And));
                }
                if (!DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UpdatedTime", "UpdatedTime_1", DbType.DateTime, toUpdatedTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And));
                }
                //whereConditons.Add(new WhereConditon("IsSystemUserType", "IsSystemUserType", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("IsVisibleForInterface", "IsVisibleForInterface", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                string dataFileNames = "UserTypeId, UserTypeName, UserTypeCode, CreatedTime, UpdatedTime";
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("UpdatedTime", CustomSorting.Descending));
                ds = DataAccessHandler.GetPageRecord(db, "UserType", dataFileNames, false, null, pos,
                    pageSize, whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds.Tables[0];
        }

        /// <summary>
        /// 获得接口可见标记位
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        public bool GetIsVisibleForInterface(decimal userTypeId)
        {
            bool isVisibleForInterface = false;

            string sqlSelect = "SELECT IsVisibleForInterface FROM UserType WHERE UserTypeId = @UserTypeId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, userTypeId);
                    isVisibleForInterface = DataConvertionHelper.GetBoolean(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isVisibleForInterface;
        }

        /// <summary>
        /// 获得系统标记位
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>
        public bool GetIsSystemUserType(decimal userTypeId)
        {
            bool isSystemUserType = false;

            string sqlSelect = "SELECT IsSystemUserType FROM UserType WHERE UserTypeId = @UserTypeId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, userTypeId);
                    isSystemUserType = DataConvertionHelper.GetBoolean(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return isSystemUserType;
        }

        /// <summary>
        /// 获得用户类型编号和用户类型名称的对应集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, decimal> GetNameAndUserTypeIds()
        {
            Dictionary<string, decimal> userTypeIdAndNames = new Dictionary<string, decimal>();

            //查询语句
            string sqlSelect = "SELECT UserTypeId, UserTypeName FROM UserType";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userTypeName = DataConvertionHelper.GetString(dataReader[1]);
                            //将创建 UserAndRoleInfo 对象加入集合中
                            userTypeIdAndNames.Add(userTypeName, userTypeId);
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

            return userTypeIdAndNames;
        }

        /// <summary>
        /// 获得用户类型编号和用户类型名称的对应集合
        /// </summary>
        /// <returns></returns>
        public Dictionary<decimal, string> GetUserTypeIdAndNames()
        {
            Dictionary<decimal, string> userTypeIdAndNames = new Dictionary<decimal, string>();

            //查询语句
            string sqlSelect = "SELECT UserTypeId, UserTypeName FROM UserType";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userTypeName = DataConvertionHelper.GetString(dataReader[1]);
                            //将创建 UserAndRoleInfo 对象加入集合中
                            userTypeIdAndNames.Add(userTypeId, userTypeName);
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

            return userTypeIdAndNames;
        }

        #endregion

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
		/// 获得 UserTypeInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>UserTypeInfo 对象列表</returns>
		private IList<UserTypeInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<UserTypeInfo> userTypeInfos = new List<UserTypeInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM UserType");
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
                            decimal groupId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal parentUserTypeId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string userTypeName = DataConvertionHelper.GetString(dataReader[2]);
                            string userTypeCode = DataConvertionHelper.GetString(dataReader[3]);
                            string firstCode = DataConvertionHelper.GetString(dataReader[4]);
                            string secondCode = DataConvertionHelper.GetString(dataReader[5]);
                            bool isSystemUserType = DataConvertionHelper.GetBoolean(dataReader[6]);
                            bool isVisibleForInterface = DataConvertionHelper.GetBoolean(dataReader[7]);
                            int sorting = DataConvertionHelper.GetInt(dataReader[8]);
                            string notes = DataConvertionHelper.GetString(dataReader[9]);
                            DateTime createdTime = DataConvertionHelper.GetDateTime(dataReader[10]);
                            DateTime updatedTime = DataConvertionHelper.GetDateTime(dataReader[11]);
                            //将创建 UserTypeInfo 对象加入集合中
                            userTypeInfos.Add(new UserTypeInfo(groupId, parentUserTypeId, userTypeName, userTypeCode, firstCode,
                            secondCode, isSystemUserType, isVisibleForInterface, sorting, notes, createdTime, updatedTime));
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

            return userTypeInfos;
        }

        /// <summary>
        /// 获得 UserTypeInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserTypeInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM UserType");
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
        /// 获得表 UserType 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "UserType ", "UserTypeId", "*", false, false, startPosition,
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
        /// 获得以表 UserType 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserType ", "UserTypeId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 UserType 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "UserType ", "UserTypeId", "*", false, false, startPosition,
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
        /// 获得以表 UserType 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserType ", "UserTypeId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  UserTypeInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserType");
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
