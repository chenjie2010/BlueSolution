//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserAccount.cs
// 描述: UserAccount 数据层访问类
// 作者：ChenJie 
// 编写日期：2011/4/28
// Copyright 2011
//-----------------------------------------------------------------------------------------
using System;
using System.Drawing;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;


namespace Blue.WebAPI
{
    /// <summary>
    /// UserAccount 表的数据层访问类
    /// </summary>
    public class UserAccount
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserAccount()
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
        /// 获得 UserAccountInfo 对象
        /// </summary>
        ///<param name="userName">用户名</param>
        /// <returns> UserAccountInfo 对象</returns>
        public UserAccountInfo GetModelInfo(string userName)
        {
            UserAccountInfo userAccountInfo = null;
            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserId, UserTypeId, DepID, UserPwd, UserActualName, ");
            sb.Append("EmailAddress, UserIdentity, LastLogonTime, LastLogonIP, PhotoSuffix, IsLockedOut, ");
            sb.Append("DataFieldPower, Creater, CreationTime, Modifier, ModificationTime, ");
            sb.Append("Notes ");
            sb.Append("FROM UserAccount ");
            sb.Append("WHERE UserName = @UserName");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal depID = DataConvertionHelper.GetDecimal(dataReader[2]);                            
                            //string userPwd = DataConvertionHelper.GetString(dataReader[3]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[4]);
                            string emailAddress = DataConvertionHelper.GetString(dataReader[5]);
                            string userIdentity = DataConvertionHelper.GetString(dataReader[6]);
                            DateTime lastLogonTime = DataConvertionHelper.GetDateTime(dataReader[7]);
                            string lastLogonIP = DataConvertionHelper.GetString(dataReader[8]);
                            string photoSuffix = DataConvertionHelper.GetString(dataReader[9]);
                            bool isLockedOut = DataConvertionHelper.GetBoolean(dataReader[10]);
                            int dataFieldPower = DataConvertionHelper.GetInt(dataReader[11]);
                            string creater = DataConvertionHelper.GetString(dataReader[12]);
                            DateTime creationTime = DataConvertionHelper.GetDateTime(dataReader[13]);
                            string modifier = DataConvertionHelper.GetString(dataReader[14]);
                            DateTime modificationTime = DataConvertionHelper.GetDateTime(dataReader[15]);
                            string notes = DataConvertionHelper.GetString(dataReader[16]);
                            //创建 UserAccountInfo 对象
                            userAccountInfo = new UserAccountInfo(userId, userTypeId, depID, userName, string.Empty,
                            userActualName, emailAddress, userIdentity, lastLogonTime, lastLogonIP, photoSuffix,
                            isLockedOut, dataFieldPower, creater, creationTime, modifier,
                            modificationTime, notes);
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

            return userAccountInfo;
        }

        #endregion

        #region 实现自定义接口

        #region 实现新增接口

        /// <summary>
        /// 获得用户名和真实姓名列表
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public Dictionary<string, string> GetUserNames(IList<WhereConditon> whereConditons)
        {
            Dictionary<string, string> userNames = new Dictionary<string, string>();

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserName, UserActualName FROM UserAccount");
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
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            userNames.Add(DataConvertionHelper.GetString(dataReader[0]), DataConvertionHelper.GetString(dataReader[1]));
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

            return userNames;
        }


        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="dataFieldPhysicalNames">自定义字段名称</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecordOfMultiTablesWithUserInfo(int startPosition, int count, IList<WhereConditon> whereConditons, string dataFieldPhysicalNames, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UserAccount.UserId, UserAccount.UserName, UserAccount.UserActualName, UserAccount.UserIdentity, CustomDepartment.DepName, UserType.UserTypeName");

                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("CustomDepartment", "DepID", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("UserType", "UserTypeId", TableJoin.InnerJoin));
                               
                ds = DataAccessHandler.GetPageRecord(db, "UserAccount ", "UserId", sb.ToString(), false, true, tableLinks, startPosition,
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
        /// 通过用户名获得用户唯一标示符
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户唯一标示符</returns>
        public Guid GetUniqueUserIdentity(string userName)
        {
            Guid uniqueUserIdentity = Guid.Empty;

            //查询语句
            string sqlSelect = "SELECT UniqueUserIdentity FROM UserAccount WHERE UserName = @UserName";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    object obj = db.ExecuteScalar(dbCommand);
                    if (obj != null && obj != DBNull.Value)
                    {
                        uniqueUserIdentity = (Guid)obj;
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return uniqueUserIdentity;
        }        

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 UserAccountInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserAccountInfo 对象列表</returns>
        private IList<UserAccountInfo> GetModelInfos(IList<WhereConditon> whereConditons)
        {
            //创建集合对象
            IList<UserAccountInfo> userAccountInfos = new List<UserAccountInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM UserAccount");
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
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal depID = DataConvertionHelper.GetDecimal(dataReader[2]);
                            string userName = DataConvertionHelper.GetString(dataReader[3]);
                            string userPwd = DataConvertionHelper.GetString(dataReader[4]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[5]);
                            string emailAddress = DataConvertionHelper.GetString(dataReader[6]);
                            string userIdentity = DataConvertionHelper.GetString(dataReader[7]);
                            DateTime lastLogonTime = DataConvertionHelper.GetDateTime(dataReader[8]);
                            string lastLogonIP = DataConvertionHelper.GetString(dataReader[9]);
                            string photoSuffix = DataConvertionHelper.GetString(dataReader[10]);
                            bool isLockedOut = DataConvertionHelper.GetBoolean(dataReader[11]);
                            int dataFieldPower = DataConvertionHelper.GetInt(dataReader[12]);
                            string creater = DataConvertionHelper.GetString(dataReader[13]);
                            DateTime creationTime = DataConvertionHelper.GetDateTime(dataReader[14]);
                            string modifier = DataConvertionHelper.GetString(dataReader[15]);
                            DateTime modificationTime = DataConvertionHelper.GetDateTime(dataReader[16]);
                            string notes = DataConvertionHelper.GetString(dataReader[17]);
                            //将创建 UserAccountInfo 对象加入集合中
                            userAccountInfos.Add(new UserAccountInfo(userId, userTypeId, depID, userName, userPwd,
                            userActualName, emailAddress, userIdentity, lastLogonTime, lastLogonIP, photoSuffix,
                            isLockedOut, dataFieldPower, creater, creationTime, modifier, modificationTime, notes));
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

            return userAccountInfos;
        }

        /// <summary>
        /// 获得 UserAccountInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserAccountInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM UserAccount");
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
        /// 获得 UserAccount 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserAccountInfo 记录的数目</returns>
        private int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "UserAccount ", "UserId", false, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得表 UserAccount 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "UserAccount ", "UserId", "*", false, false, startPosition,
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
        /// 获得表 UserAccount 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds = DataAccessHandler.GetPageRecord(db, "UserAccount ", "UserId", "*", false, false, startPosition,
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
        /// 删除满足条件的所有  UserAccountInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        public int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserAccount");
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

        /// <summary>
        /// 向 UserAccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo">userAccountInfo 对象</param>
        /// <param name="encrypt">是否加密</param>
        /// <param name="transaction">事务</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(UserAccountInfo userAccountInfo, bool encrypt, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal userAccountId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO UserAccount(UserTypeId, DepID, UserName, UserPwd, UserActualName, ");
            sb.Append("EmailAddress, UserIdentity, LastLogonTime, ");
            sb.Append("LastLogonIP, PhotoSuffix, IsLockedOut, DataFieldPower, Creater, CreationTime, ");
            sb.Append("Modifier, ModificationTime, Notes, UniqueUserIdentity)");
            sb.Append("VALUES (@UserTypeId, @DepID, @UserName, @UserPwd, @UserActualName, ");
            sb.Append("@EmailAddress, @UserIdentity, @LastLogonTime, ");
            sb.Append("@LastLogonIP, @PhotoSuffix, @IsLockedOut, @DataFieldPower, @Creater, @CreationTime, ");
            sb.Append("@Modifier, @ModificationTime, @Notes, newid());");
            sb.Append("SET @UserId = SCOPE_IDENTITY()");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "UserId", DbType.Decimal, 8);
                    db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, DataConvertionHelper.SetDecimal(userAccountInfo.UserTypeId));
                    db.AddInParameter(dbCommand, "DepID", DbType.Decimal, DataConvertionHelper.SetDecimal(userAccountInfo.DepID));
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userAccountInfo.UserName);
                    if (encrypt)
                    {
                        userAccountInfo.UserPwd = CryptographyHelper.Encrypt(userAccountInfo.UserPwd);
                    }
                    db.AddInParameter(dbCommand, "UserPwd", DbType.String, userAccountInfo.UserPwd);
                    db.AddInParameter(dbCommand, "UserActualName", DbType.String, userAccountInfo.UserActualName);
                    if (string.IsNullOrWhiteSpace(userAccountInfo.UserIdentity))
                    {
                        db.AddInParameter(dbCommand, "UserIdentity", DbType.String, DBNull.Value);
                    }
                    else
                    {
                        db.AddInParameter(dbCommand, "UserIdentity", DbType.String, userAccountInfo.UserIdentity);
                    }
                    db.AddInParameter(dbCommand, "EmailAddress", DbType.String, userAccountInfo.EmailAddress);
                    db.AddInParameter(dbCommand, "LastLogonTime", DbType.DateTime, DataConvertionHelper.SetDateTime(userAccountInfo.LastLogonTime));
                    db.AddInParameter(dbCommand, "LastLogonIP", DbType.String, userAccountInfo.LastLogonIP);
                    db.AddInParameter(dbCommand, "PhotoSuffix", DbType.String, userAccountInfo.PhotoSuffix);
                    db.AddInParameter(dbCommand, "IsLockedOut", DbType.Boolean, userAccountInfo.IsLockedOut);
                    db.AddInParameter(dbCommand, "DataFieldPower", DbType.Int32, DataConvertionHelper.SetInt(userAccountInfo.DataFieldPower));
                    db.AddInParameter(dbCommand, "Creater", DbType.String, userAccountInfo.Creater);
                    db.AddInParameter(dbCommand, "CreationTime", DbType.DateTime, DataConvertionHelper.SetDateTime(userAccountInfo.CreationTime));
                    db.AddInParameter(dbCommand, "Modifier", DbType.String, userAccountInfo.Modifier);
                    db.AddInParameter(dbCommand, "ModificationTime", DbType.DateTime, DataConvertionHelper.SetDateTime(userAccountInfo.ModificationTime));
                    db.AddInParameter(dbCommand, "Notes", DbType.String, userAccountInfo.Notes);
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
                    userAccountId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@UserId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userAccountId;
        }


        /// <summary>
        /// 向 UserAccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo">userAccountInfo 对象</param>
        /// <param name="encrypt">是否加密</param>
        /// <param name="transaction">事务</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal InsertByMD5(UserAccountInfo userAccountInfo, bool encrypt, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal userAccountId = 0;
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO UserAccount(UserTypeId, DepID, UserName, UserPwd, UserActualName, ");
            sb.Append("EmailAddress, UserIdentity, LastLogonTime, ");
            sb.Append("LastLogonIP, PhotoSuffix, IsLockedOut, DataFieldPower, Creater, CreationTime, ");
            sb.Append("Modifier, ModificationTime, Notes, UniqueUserIdentity)");
            sb.Append("VALUES (@UserTypeId, @DepID, @UserName, @UserPwd, @UserActualName, ");
            sb.Append("@EmailAddress, @UserIdentity, @LastLogonTime, ");
            sb.Append("@LastLogonIP, @PhotoSuffix, @IsLockedOut, @DataFieldPower, @Creater, @CreationTime, ");
            sb.Append("@Modifier, @ModificationTime, @Notes, newid());");
            sb.Append("SET @UserId = SCOPE_IDENTITY()");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "UserId", DbType.Decimal, 8);
                    db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, DataConvertionHelper.SetDecimal(userAccountInfo.UserTypeId));
                    db.AddInParameter(dbCommand, "DepID", DbType.Decimal, DataConvertionHelper.SetDecimal(userAccountInfo.DepID));
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userAccountInfo.UserName);
                    if (encrypt)
                    {
                        userAccountInfo.UserPwd = CryptographyHelper.GetMD5(userAccountInfo.UserPwd);
                    }
                    db.AddInParameter(dbCommand, "UserPwd", DbType.String, userAccountInfo.UserPwd);
                    db.AddInParameter(dbCommand, "UserActualName", DbType.String, userAccountInfo.UserActualName);
                    if (string.IsNullOrWhiteSpace(userAccountInfo.UserIdentity))
                    {
                        db.AddInParameter(dbCommand, "UserIdentity", DbType.String, DBNull.Value);
                    }
                    else
                    {
                        db.AddInParameter(dbCommand, "UserIdentity", DbType.String, userAccountInfo.UserIdentity);
                    }
                    db.AddInParameter(dbCommand, "EmailAddress", DbType.String, userAccountInfo.EmailAddress);
                    db.AddInParameter(dbCommand, "LastLogonTime", DbType.DateTime, DataConvertionHelper.SetDateTime(userAccountInfo.LastLogonTime));
                    db.AddInParameter(dbCommand, "LastLogonIP", DbType.String, userAccountInfo.LastLogonIP);
                    db.AddInParameter(dbCommand, "PhotoSuffix", DbType.String, userAccountInfo.PhotoSuffix);
                    db.AddInParameter(dbCommand, "IsLockedOut", DbType.Boolean, userAccountInfo.IsLockedOut);
                    db.AddInParameter(dbCommand, "DataFieldPower", DbType.Int32, DataConvertionHelper.SetInt(userAccountInfo.DataFieldPower));
                    db.AddInParameter(dbCommand, "Creater", DbType.String, userAccountInfo.Creater);
                    db.AddInParameter(dbCommand, "CreationTime", DbType.DateTime, DataConvertionHelper.SetDateTime(userAccountInfo.CreationTime));
                    db.AddInParameter(dbCommand, "Modifier", DbType.String, userAccountInfo.Modifier);
                    db.AddInParameter(dbCommand, "ModificationTime", DbType.DateTime, DataConvertionHelper.SetDateTime(userAccountInfo.ModificationTime));
                    db.AddInParameter(dbCommand, "Notes", DbType.String, userAccountInfo.Notes);
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
                    userAccountId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@UserId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userAccountId;
        }

        #endregion

        #region 自定义私有方法
        

        #endregion

        #endregion

        #endregion
    }
}