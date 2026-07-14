//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserAccount.cs
// 描述: UserAccount 数据层访问类
// 作者：ChenJie 
// 编写日期：2016/8/9
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL.UserModule;
using Blue.Model.UserModule;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.SQLServerDAL.SystemModule;
using Blue.SQLServerDAL.BusinessModule;

namespace Blue.SQLServerDAL.UserModule
{
    /// <summary>
    /// UserAccount 表的数据层访问类
    /// </summary>
    public class UserAccount : IUserAccount
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
        /// 向 UserAccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo">userAccountInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(UserAccountInfo userAccountInfo)
        {
            return Insert(userAccountInfo, null);
        }

        /// <summary>
		/// 获得 UserAccountInfo 对象
		/// </summary>
		///<param name="userId">用户编号</param>
		/// <returns> UserAccountInfo 对象</returns>
		public UserAccountInfo GetModelInfo(decimal userId)
        {
            return GetModeInfoByKeyword("UserId", userId, DbType.Decimal);
        }

        /// <summary>
        /// 更新 UserAccountInfo 对象
        /// </summary>
        /// <param name="userAccountInfo">UserAccountInfo 对象</param>
        public void Update(UserAccountInfo userAccountInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserAccount SET DepId = @DepId, UserTypeId = @UserTypeId, UserName = @UserName, ");
            if (!string.IsNullOrWhiteSpace(userAccountInfo.UserPwd))
            {
                sb.Append("UserPwd = @UserPwd,");
            }
            sb.Append("UserActualName = @UserActualName, ");
            sb.Append("EmailAddress = @EmailAddress, IdentificationType = @IdentificationType, UserIdentity = @UserIdentity, ");
            sb.Append("TelephoneNumber = @TelephoneNumber, LastLogonTime = @LastLogonTime, LastLogonIP = @LastLogonIP, ");
            sb.Append("PhotoSuffixName = @PhotoSuffixName, LockedOut = @LockedOut, DataFieldAuthority = @DataFieldAuthority, ");
            sb.Append("DepartmentAuthority = @DepartmentAuthority, UniqueUserIdentity = @UniqueUserIdentity, Notes = @Notes, UpdatedTime = @UpdatedTime ");
            sb.Append("WHERE UserId = @UserId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userAccountInfo.UserId);
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, userAccountInfo.DepId);
                    db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, userAccountInfo.UserTypeId);
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userAccountInfo.UserName);
                    if (!string.IsNullOrWhiteSpace(userAccountInfo.UserPwd))
                    {
                        string userPwd = CryptographyHelper.Encrypt(userAccountInfo.UserPwd);
                        db.AddInParameter(dbCommand, "UserPwd", DbType.String, userAccountInfo.UserPwd);
                    }
                    db.AddInParameter(dbCommand, "UserActualName", DbType.String, userAccountInfo.UserActualName);
                    db.AddInParameter(dbCommand, "EmailAddress", DbType.String, userAccountInfo.EmailAddress);
                    db.AddInParameter(dbCommand, "IdentificationType", DbType.Byte, Convert.ToByte(userAccountInfo.IdentificationType));
                    db.AddInParameter(dbCommand, "UserIdentity", DbType.String, userAccountInfo.UserIdentity);
                    db.AddInParameter(dbCommand, "TelephoneNumber", DbType.String, userAccountInfo.TelephoneNumber);
                    db.AddInParameter(dbCommand, "LastLogonTime", DbType.DateTime, userAccountInfo.LastLogonTime);
                    db.AddInParameter(dbCommand, "LastLogonIP", DbType.String, userAccountInfo.LastLogonIP);
                    db.AddInParameter(dbCommand, "PhotoSuffixName", DbType.String, userAccountInfo.PhotoSuffixName);
                    db.AddInParameter(dbCommand, "LockedOut", DbType.Boolean, userAccountInfo.LockedOut);
                    db.AddInParameter(dbCommand, "DataFieldAuthority", DbType.Int64, userAccountInfo.DataFieldAuthority);
                    db.AddInParameter(dbCommand, "DepartmentAuthority", DbType.Int64, userAccountInfo.DepartmentAuthority);
                    db.AddInParameter(dbCommand, "UniqueUserIdentity", DbType.Guid, userAccountInfo.UniqueUserIdentity);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, userAccountInfo.Notes);
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
        /// 更新 UserAccountInfo 对象，在导入数据中使用。
        /// </summary>
        /// <param name="userAccountInfo">UserAccountInfo 对象</param>
        public void UpdateUserAccountInfo(UserAccountInfo userAccountInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserAccount SET DepId = @DepId, UserTypeId = @UserTypeId, ");
            if (!string.IsNullOrWhiteSpace(userAccountInfo.UserPwd))
            {
                sb.Append("UserPwd = @UserPwd,");
            }
            sb.Append("UserActualName = @UserActualName, EmailAddress = @EmailAddress, IdentificationType = @IdentificationType, UserIdentity = @UserIdentity, ");
            sb.Append("TelephoneNumber = @TelephoneNumber, LastLogonTime = @LastLogonTime, UpdatedTime = @UpdatedTime ");
            sb.Append("WHERE UserName = @UserName");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, userAccountInfo.DepId);
                    db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, userAccountInfo.UserTypeId);
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userAccountInfo.UserName);
                    if (!string.IsNullOrWhiteSpace(userAccountInfo.UserPwd))
                    {
                        string userPwd = CryptographyHelper.Encrypt(userAccountInfo.UserPwd);
                        db.AddInParameter(dbCommand, "UserPwd", DbType.String, userAccountInfo.UserPwd);
                    }
                    db.AddInParameter(dbCommand, "UserActualName", DbType.String, userAccountInfo.UserActualName);
                    db.AddInParameter(dbCommand, "EmailAddress", DbType.String, userAccountInfo.EmailAddress);
                    db.AddInParameter(dbCommand, "IdentificationType", DbType.Byte, Convert.ToByte(userAccountInfo.IdentificationType));
                    db.AddInParameter(dbCommand, "UserIdentity", DbType.String, userAccountInfo.UserIdentity);
                    db.AddInParameter(dbCommand, "TelephoneNumber", DbType.String, userAccountInfo.TelephoneNumber);
                    db.AddInParameter(dbCommand, "LastLogonTime", DbType.DateTime, userAccountInfo.LastLogonTime);
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
        ///  删除 UserAccountInfo 对象
        /// </summary>
        ///<param name="userId">用户编号</param>
        public void Delete(decimal userId)
        {
            IList<decimal> userIds = new List<decimal>();
            userIds.Add(userId);
            Delete(userIds);
        }

        /// <summary>
        /// 获得 UserAccountInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserAccountInfo 对象列表</returns>
        public IList<UserAccountInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 UserAccount 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserAccountInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
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

        #endregion

        #region 实现自定义接口

        #region 实现新增接口

        /// <summary>
        /// 获得用户数
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetUserCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT COUNT(1) FROM UserAccount");
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
                    count = (int)db.ExecuteScalar(dbCommand);
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
        /// 获得用户数量
        /// </summary>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public int GetUserCount(DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            int count = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                if (!DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UserAccount", "UpdatedTime", "UpdatedTime_0", DbType.DateTime, fromUpdatedTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And));
                }
                if (!DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UserAccount", "UpdatedTime", "UpdatedTime_1", DbType.DateTime, toUpdatedTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And));
                }
                //whereConditons.Add(new WhereConditon("IsSystemUserType", "IsSystemUserType", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("UserType","IsVisibleForInterface", "IsVisibleForInterface_0", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                //whereConditons.Add(new WhereConditon("IsSystemDepartment", "IsSystemDepartment", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("CustomDepartment", "IsVisibleForInterface", "IsVisibleForInterface_1", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("UpdatedTime", CustomSorting.Descending));
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserType", "UserTypeId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomDepartment", "DepId", TableJoin.InnerJoin));
                count = DataAccessHandler.GetRecordCount(db, "UserAccount", tableLinks, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得用户分页数据
        /// </summary>
        /// <param name="pos"></param>
        /// <param name="pageSize"></param>
        /// <param name="fromUpdatedTime"></param>
        /// <param name="toUpdatedTime"></param>
        /// <returns></returns>
        public DataTable GetUserData(int pos, int pageSize, DateTime fromUpdatedTime, DateTime toUpdatedTime)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                if (!DataConvertionHelper.IsNullValue(fromUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UserAccount", "UpdatedTime", "UpdatedTime_0", DbType.DateTime, fromUpdatedTime, DataFieldCondition.MoreOrEqual, DataFieldInnerRealtion.And));
                }
                if (!DataConvertionHelper.IsNullValue(toUpdatedTime))
                {
                    whereConditons.Add(new WhereConditon("UserAccount", "UpdatedTime", "UpdatedTime_1", DbType.DateTime, toUpdatedTime, DataFieldCondition.LessOrEqual, DataFieldInnerRealtion.And));
                }
                //whereConditons.Add(new WhereConditon("IsSystemUserType", "IsSystemUserType", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("UserType", "IsVisibleForInterface", "IsVisibleForInterface_0", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                //whereConditons.Add(new WhereConditon("IsSystemDepartment", "IsSystemDepartment", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("CustomDepartment", "IsVisibleForInterface", "IsVisibleForInterface_1", DbType.Boolean, true, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

                StringBuilder dataFileNames = new StringBuilder();
                dataFileNames.Append("UserAccount.UserId, UserAccount.UserName, UserAccount.UserActualName, CustomDepartment.DepId, CustomDepartment.DepName, ");
                dataFileNames.Append("UserType.UserTypeId, UserType.UserTypeName, UserAccount.EmailAddress, UserAccount.IdentificationType, UserAccount.UserIdentity, ");
                dataFileNames.Append("UserAccount.TelephoneNumber, UserAccount.LockedOut, UserAccount.UniqueUserIdentity, UserAccount.CreatedTime, UserAccount.UpdatedTime");
                IList<SortingCondtion> sortingCondtions = new List<SortingCondtion>();
                sortingCondtions.Add(new SortingCondtion("UpdatedTime", CustomSorting.Descending));
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserType", "UserTypeId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomDepartment", "DepId", TableJoin.InnerJoin));
                ds = DataAccessHandler.GetPageRecord(db, "UserAccount", dataFileNames.ToString(), false, tableLinks, pos,
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
        /// 更新用户密码
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newPassword"></param>
        public void UpdatePassword(string userName, string newPassword)
        {
            //获得系统数据库
            SqlDatabase db = DataAccessHelper.GetDatabase();
            string sqlUpdated = "UPDATE UserAccount SET UserPwd = @UserPwd WHERE UserName = @UserName";

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdated))
                {
                    db.AddInParameter(dbCommand, "UserPwd", DbType.String, CryptographyHelper.Encrypt(newPassword));
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("更新失败！");
                    }
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="emailAddress"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="imageData"></param>
        /// <param name="photoSuffixName"></param>
        public void Update(string userName, string emailAddress, string telephoneNumber, byte[] imageData, string photoSuffixName)
        {
            //获得系统数据库
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    /* 1. 更新用户信息 */
                    StringBuilder sb = new StringBuilder();
                    sb.Append("UPDATE UserAccount SET EmailAddress = @EmailAddress, TelephoneNumber = @TelephoneNumber");
                    if ((imageData != null) && !string.IsNullOrWhiteSpace(photoSuffixName))
                    {
                        sb.Append(", PhotoSuffixName = @PhotoSuffixName");
                    }
                    sb.Append(" WHERE UserName = @UserName");
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "EmailAddress", DbType.String, emailAddress);
                        db.AddInParameter(dbCommand, "TelephoneNumber", DbType.String, telephoneNumber);
                        if ((imageData != null) && !string.IsNullOrWhiteSpace(photoSuffixName))
                        {
                            db.AddInParameter(dbCommand, "PhotoSuffixName", DbType.String, photoSuffixName);
                        }
                        db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                        //执行更新操作
                        int count = db.ExecuteNonQuery(dbCommand, transaction);
                        if (count != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }

                    /* 2. 更新图片*/
                    if ((imageData != null) && !string.IsNullOrWhiteSpace(photoSuffixName))
                    {
                        UpLoadPhoto(string.Format("{0}.{1}", userName, photoSuffixName), imageData);
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
        }

        /// <summary>
        /// 获得用户照片相对路径
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns></returns>
        public string GetWebPhotoPath(string userName)
        {
            string format = GetPhotoSuffixName(userName);
            string path = GetPhotoPath(userName, format);
            string relativePath = string.Format(@"{0}\{1}", AppSettingHelper.DefaultSubDirOfSavedPhotos, Path.GetFileName(path));

            return relativePath;
        }
        /// <summary>
        /// 获得最后登录时间
        /// </summary>
        /// <param name="userName"></param>
        public DateTime GetLastLogonTime(string userName)
        {
            DateTime dateTime = DateTime.Now;
            try
            {
                string sqlCommand = "SELECT LastLogonTime FROM UserAccount WHERE UserName = @UserName";
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand))
                {
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    dateTime = DataConvertionHelper.GetDateTime(db.ExecuteScalar(dbCommand), DateTime.Now);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dateTime;
        }

        /// <summary>
        /// 获得重试次数
        /// </summary>
        /// <param name="userName"></param>
        public int GetRetryTimes(string userName)
        {
            int retryTimes = 0;

            try
            {
                //选择语句
                string sqlSelect = "SELECT RetryTimes FROM UserAccount WHERE UserName = @UserName";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    retryTimes = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                }
            }
            catch
            { }

            return retryTimes;
        }

        /// <summary>
        /// 清空重试次数
        /// </summary>
        /// <param name="userName"></param>
        public void ClearRetryTimes(string userName)
        {
            try
            {
                string sqlCommand = "UPDATE UserAccount SET RetryTimes = 0, LastLogonTime = @LastLogonTime WHERE UserName = @UserName AND RetryTimes > 0";
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand))
                {
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    db.AddInParameter(dbCommand, "LastLogonTime", DbType.DateTime, DateTime.Now);
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newUserName"></param>
        public void UpdateUserName(string userName, string newUserName)
        {
            try
            {
                /* 更新业务数据 */
                /* 文件路径 */
                StringBuilder sbPath = new StringBuilder();
                sbPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
                if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
                {
                    sbPath.Append(@"\");
                }
                sbPath.Append(AppSettingHelper.DefaultSubDirOfUploadFiles);
                decimal userId = GetUserIdByUserName(userName);
                CustomTable customTable = new CustomTable();
                IList<CommonNode> commonNodes = customTable.GetTables();
                foreach (CommonNode commonNode in commonNodes)
                {                    
                    SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)commonNode.ParentNodeId));
                    string sqlUpdate = string.Format("UPDATE {0} SET UserName = @UserData WHERE UserName = @UserName", commonNode.NodeName);
                    using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlUpdate))
                    {
                        dbBusiness.AddInParameter(dbCommand, "UserData", DbType.String, newUserName);
                        dbBusiness.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                        dbBusiness.ExecuteNonQuery(dbCommand);
                    }
                    try
                    {
                        UpdateAttachmentNames(userId, userName, newUserName, commonNode.NodeId,
                                  commonNode.NodeName, sbPath.ToString(), dbBusiness);
                    }
                    catch (Exception exception)
                    {
                        //记录日志, 抛出异常, 不包装异常 
                        ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                    }
                }

                /* 获取照片的后缀名 */
                string format = GetPhotoSuffixName(userName);
                if (string.IsNullOrWhiteSpace(format))
                {
                    format = AutoGetPhotoSuffixName(userName);
                }
                /* 更新系统数据 */
                string sqlUser = "UPDATE UserAccount SET UserName = @UserData WHERE UserName = @UserName";
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUser))
                {
                    //给参数赋值                      
                    db.AddInParameter(dbCommand, "UserData", DbType.String, newUserName);
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    //执行更新操作
                    db.ExecuteNonQuery(dbCommand);
                }
                /* 更新照片名称 */
                UpdateUserPhotoName(userName, newUserName, format);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新用户的单位编号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="depId"></param>
        public void UpdateDepIdByUserName(string userName, decimal depId)
        {
            try
            {
                /* 更新业务数据 */
                CustomTable customTable = new CustomTable();
                IList<CommonNode> commonNodes = customTable.GetTables();
                foreach (CommonNode commonNode in commonNodes)
                {
                    SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)commonNode.ParentNodeId));
                    string sqlUpdate = string.Format("UPDATE {0} SET DepId = @DepId WHERE UserName = @UserName", commonNode.NodeName);
                    using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlUpdate))
                    {
                        dbBusiness.AddInParameter(dbCommand, "DepId", DbType.Decimal, depId);
                        dbBusiness.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                        dbBusiness.ExecuteNonQuery(dbCommand);
                    }
                }

                /* 更新系统数据 */
                string sqlSystemUpdated = "UPDATE UserAccount SET DepId = @DepId, UpdatedTime = @UpdatedTime WHERE UserName = @UserName";
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSystemUpdated))
                {
                    //给参数赋值                      
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, depId);
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DateTime.Now);
                    //执行更新操作
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新用户的用户类型编号
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userTypeId"></param>
        public void UpdateUserTypeIdByUserName(string userName, decimal userTypeId)
        {
            try
            {
                /* 更新业务数据 */
                CustomTable customTable = new CustomTable();
                IList<CommonNode> commonNodes = customTable.GetTables();
                foreach (CommonNode commonNode in commonNodes)
                {
                    SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)commonNode.ParentNodeId));
                    string sqlUpdate = string.Format("UPDATE {0} SET UserTypeId = @UserTypeId WHERE UserName = @UserName", commonNode.NodeName);

                    using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlUpdate))
                    {
                        dbBusiness.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, userTypeId);
                        dbBusiness.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                        dbBusiness.ExecuteNonQuery(dbCommand);
                    }
                }

                /* 更新系统数据 */
                string sqlSystemUpdated = "UPDATE UserAccount SET UserTypeId = @UserTypeId WHERE UserName = @UserName";
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSystemUpdated))
                {
                    //给参数赋值                      
                    db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, userTypeId);
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    //执行更新操作
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userToolAction"></param>
        /// <param name="userName"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public void UpdateUserInfo(UserToolAction userToolAction, string userName, string userData)
        {
            IList<int> failedRows = new List<int>();

            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UPDATE UserAccount SET UpdatedTime = @UpdatedTime, ");

                switch (userToolAction)
                {
                    case UserToolAction.UserActualName:
                        sb.Append("UserActualName = @UserData");
                        break;

                    case UserToolAction.UserIdentity:
                        sb.Append("UserIdentity = @UserData");
                        break;

                    case UserToolAction.TelephoneNumber:
                        sb.Append("TelephoneNumber = @UserData");
                        break;

                    case UserToolAction.Password:
                        sb.Append("UserPwd = @UserData");
                        break;

                    case UserToolAction.Email:
                        sb.Append("EmailAddress = @UserData");
                        break;

                    default:
                        throw new ArgumentException("用户数据更新参数异常。");
                }
                sb.Append(" WHERE UserName = @UserName");
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DateTime.Now);
                    if (userToolAction == UserToolAction.Password)
                    {
                        db.AddInParameter(dbCommand, "UserData", DbType.String, CryptographyHelper.Encrypt(userData));
                    }
                    else
                    {
                        db.AddInParameter(dbCommand, "UserData", DbType.String, userData);
                    }
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);                    
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
        /// 批量删除用户
        /// </summary>
        /// <param name="userNames"></param>
        /// <returns></returns>
        public IList<int> Delete(Dictionary<int, string> userNames)
        {
            IList<int> failedRows = new List<int>();

            foreach (KeyValuePair<int, string> keyValue in userNames)
            {
                try
                {
                    decimal userId = GetUserIdByUserName(keyValue.Value);
                    Delete(userId);
                }
                catch
                {
                    failedRows.Add(keyValue.Key);
                }
            }

            return failedRows;
        }

        /// <summary>
        /// 批量插入用户
        /// </summary>
        /// <param name="userAccountInfos"></param>
        /// <returns>插入失败的索引列表</returns>
        public IList<int> Insert(Dictionary<int, UserAccountInfo> userAccountInfos)
        {
            IList<int> failedRows = new List<int>();

            foreach (KeyValuePair<int, UserAccountInfo> keyValue in userAccountInfos)
            {
                try
                {
                    decimal id = Insert(keyValue.Value);
                    if (id <= 0)
                    {
                        failedRows.Add(keyValue.Key);
                    }
                }
                catch
                {
                    failedRows.Add(keyValue.Key);
                }
            }

            return failedRows;
        }

        /// <summary>
        /// 查询密码
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetUserPassword(string userName)
        {
            string password = string.Empty;

            try
            {
                string sqlCommand = "SELECT UserPwd FROM UserAccount WHERE UserName = @UserName";
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand))
                {
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    string encryptedText = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                    if (!string.IsNullOrEmpty(encryptedText))
                    {
                        password = CryptographyHelper.Decrypt(encryptedText);
                    }
                }
            }
            catch { }

            return password;
        }

        /// <summary>
        /// 批量删除 UserAccountInfo 对象
        /// </summary>
        /// <param name="userIds">用户编号列表</param>
        public void Delete(IList<decimal> userIds)
        {
            try
            {
                /* 1. 删除相关的数据仓库的数据*/
                CustomTable customTable = new CustomTable();
                IList<CommonNode> commonNodes = customTable.GetTables();
                foreach (CommonNode commonNode in commonNodes)
                {
                    SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)commonNode.ParentNodeId));
                    string sqlDelete = string.Format("DELETE FROM {0} WHERE UserId = @UserId", commonNode.NodeName);
                    foreach (decimal userId in userIds)
                    {
                        using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sqlDelete))
                        {
                            dbBusiness.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                            dbBusiness.ExecuteNonQuery(dbCommand);
                        }
                    }
                }

                /* 2. 删除关联的表中的数据 */
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();

                string[] sqlDeletes = new string[] { "DELETE FROM RoleAndUser WHERE UserId = @UserId",
                "DELETE FROM UserConfig WHERE UserId = @UserId",
                "DELETE FROM UserTypeScope WHERE UserId = @UserId",
                "DELETE FROM DepartmentScope WHERE UserId = @UserId",
                "DELETE FROM UserLog WHERE UserId = @UserId"

                //"UPDATE SystemConfig SET UserId = NULL WHERE UserId = @UserId",                
                //"DELETE UserAndMessage FROM UserAndMessage INNER JOIN PersonalMessage ON UserAndMessage.MessageId = PersonalMessage.MessageId WHERE PersonalMessage.UserId = @UserId",
                //"DELETE MessageAttachment FROM MessageAttachment INNER JOIN PersonalMessage ON MessageAttachment.MessageId = PersonalMessage.MessageId WHERE PersonalMessage.UserId = @UserId",
                //"DELETE PersonalMessage FROM UserAndMessage INNER JOIN PersonalMessage ON UserAndMessage.MessageId = PersonalMessage.MessageId WHERE PersonalMessage.UserId = @UserId",
                //"DELETE FROM PersonalMessage WHERE UserId = @UserId",
                //"DELETE FROM UserAndRole WHERE UserId = @UserId",
                //"DELETE FROM Reminder WHERE UserId = @UserId",
                //"DELETE FROM TestScore WHERE UserId = @UserId",
                //"DELETE FROM Answer WHERE UserId = @UserId",
                //"DELETE FROM UserAndReport WHERE UserId = @UserId",
                //"DELETE FROM CoordinationDetail WHERE UserId = @UserId",
                //"DELETE FROM UserAndOfficialDocAuditing WHERE UserId = @UserId",
                //"DELETE FROM WorkflowDetail WHERE UserId = @UserId"
                };


                using (DbConnection connection = db.CreateConnection())
                {
                    connection.Open();
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        foreach (string sqlDelete in sqlDeletes)
                        {
                            foreach (decimal userId in userIds)
                            {
                                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDelete))
                                {
                                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                                    //执行删除操作
                                    db.ExecuteNonQuery(dbCommand, transaction);
                                }
                            }
                        }

                        /* 删除用户表 */
                        string delete = "DELETE FROM UserAccount WHERE UserId = @UserId";
                        foreach (decimal userId in userIds)
                        {
                            using (DbCommand dbCommand = db.GetSqlStringCommand(delete))
                            {
                                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
                                int count = 0;
                                if (transaction != null)
                                {
                                    count = db.ExecuteNonQuery(dbCommand, transaction);
                                }
                                else
                                {
                                    count = db.ExecuteNonQuery(dbCommand);
                                }
                                //执行删除操作
                                if (count != 1)
                                {
                                    throw new Exception("删除失败！");
                                }
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

                /* 3. 删除用户照片与记录*/
                foreach (decimal userId in userIds)
                {
                    DeleteUserPhoto(userId);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 锁定与解锁用户状态
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="locked"></param>
        public void LockUser(decimal userId, bool locked)
        {
            //生成冻结语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserAccount SET LockedOut = @LockedOut ");
            sb.Append("WHERE UserId = @UserId");

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
                        db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
                        db.AddInParameter(dbCommand, "LockedOut", DbType.Boolean, locked);
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
        /// 锁定与解锁用户状态
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="locked"></param>
        public void LockUsers(IList<decimal> userIds, bool locked)
        {
            //生成冻结语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserAccount SET LockedOut = @LockedOut ");
            sb.Append("WHERE UserId = @UserId");

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (decimal userId in userIds)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            db.AddInParameter(dbCommand, "UserId", DbType.Decimal, DataConvertionHelper.SetDecimal(userId));
                            db.AddInParameter(dbCommand, "LockedOut", DbType.Boolean, locked);
                            //执行更新操作
                            if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                            {
                                throw new Exception("更新失败！");
                            }
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
        /// 查找用户
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, CommonUserInfo> GetCommonUserInfos()
        {
            Dictionary<string, CommonUserInfo> commonUserInfos = new Dictionary<string, CommonUserInfo>();

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserAccount.UserId, UserName, UserType.UserTypeId, UserAccount.DepId FROM UserAccount ");
            sb.Append("INNER JOIN UserType ON UserType.UserTypeId =  UserAccount.UserTypeId ");
            sb.Append("INNER JOIN CustomDepartment ON CustomDepartment.DepId =  UserAccount.DepId ");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userName = DataConvertionHelper.GetString(dataReader[1]);
                            decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[2]); ;
                            decimal depId = DataConvertionHelper.GetDecimal(dataReader[3]);

                            //创建 CommonUserInfo 对象
                            commonUserInfos.Add(userName, new CommonUserInfo(userId, userName, string.Empty,
                                userTypeId, string.Empty, string.Empty, depId, string.Empty, string.Empty, string.Empty));
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

            return commonUserInfos;
        }

        /// <summary>
        /// 根据角色编号获取用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        public IList<CommonUserInfo> GetCommonUserInfos(decimal roleId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("RoleId", "RoleId", System.Data.DbType.Decimal, roleId, DataFieldCondition.Equal));
            return GetCommonUserInfos(whereConditons, null);
        }

        /// <summary>
        /// 根据角色编号和单位编号获取用户信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <param name="depId"></param>
        /// <returns></returns>
        public IList<CommonUserInfo> GetCommonUserInfos(decimal roleId, decimal depId)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon("RoleId", "RoleId", System.Data.DbType.Decimal, roleId, DataFieldCondition.Equal));
            whereConditons.Add(new WhereConditon("CustomDepartment", "DepId", "DepId", System.Data.DbType.Decimal, depId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            return GetCommonUserInfos(whereConditons, null);
        }

        /// <summary>
        /// 根据角色与用户所属单位条件查找用户
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetManagementUsersByUserId(decimal roleId, decimal userId)
        {
            Dictionary<decimal, string> managementUsers = new Dictionary<decimal, string>();

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserAccount.UserId, UserAccount.UserName, UserAccount.UserActualName FROM UserAccount ");
            sb.Append("INNER JOIN RoleAndUser ON UserAccount.UserId = RoleAndUser.UserId ");
            sb.Append("INNER JOIN DepartmentScope ON DepartmentScope.DepId = UserAccount.DepId ");
            sb.Append("INNER JOIN UserAccount A  ON DepartmentScope.UserId = A.UserId ");
            sb.Append("WHERE A.UserId = @UserId AND RoleAndUser.RoleId = @RoleId");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal newUserId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userName = DataConvertionHelper.GetString(dataReader[1]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[2]);
                            //创建 CommonUserInfo 对象
                            managementUsers.Add(userId, string.Format("{0}[{1}]", userName, userActualName));
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

            return managementUsers;
        }

        /// <summary>
        /// 根据用户编号查用户编号
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户名</returns>
        public string GetUserNameByUserId(decimal userId)
        {
            string userName = string.Empty;

            //选择语句
            string sqlSelect = "SELECT UserName FROM UserAccount WHERE UserId = @UserId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                userName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
            }

            return userName;
        }

        /// <summary>
        /// 根据用户名查用户名和用户实际名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户编号</returns>
        public StringPairValue GetUserNameInfoByUserId(decimal userId)
        {
            StringPairValue stringPairValue = null;

            //选择语句
            string sqlSelect = "SELECT UserName, UserActualName FROM UserAccount WHERE UserId = @UserId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                {
                    if (dataReader.Read())
                    {
                        string userName = DataConvertionHelper.GetString(dataReader[0]);
                        string userActualName = DataConvertionHelper.GetString(dataReader[1]);
                        //创建 CommonUserInfo 对象
                        stringPairValue = new StringPairValue(userName, userActualName);
                    }
                    if (dataReader != null)
                    {
                        dataReader.Close();
                    }
                }
            }

            return stringPairValue;
        }

        /// <summary>
        /// 根据角色与单位条件查找用户
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetManagementUsers(decimal roleId, decimal depId)
        {
            Dictionary<decimal, string> managementUsers = new Dictionary<decimal, string>();

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserAccount.UserId, UserAccount.UserName, UserAccount.UserActualName FROM UserAccount ");
            sb.Append("INNER JOIN RoleAndUser ON UserAccount.UserId = RoleAndUser.UserId ");
            sb.Append("INNER JOIN DepartmentScope ON DepartmentScope.UserId = UserAccount.UserId ");
            sb.Append("WHERE DepartmentScope.DepId = @DepId AND RoleAndUser.RoleId = @RoleId ");

            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, depId);
                    db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, roleId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userName = DataConvertionHelper.GetString(dataReader[1]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[2]);
                            //创建 CommonUserInfo 对象
                            managementUsers.Add(userId, string.Format("{0}[{1}]", userName, userActualName));
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

            return managementUsers;
        }

        /// <summary>
        /// 根据用户编号查询管理的单位属性
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public Int64 GetDepartmentAuthority(decimal userId)
        {
            Int64 departmentAuthority = 0;

            try
            {
                string sqlSelect = "SELECT DepartmentAuthority FROM UserAccount WHERE UserId = @UserId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    departmentAuthority = DataConvertionHelper.GetLong(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return departmentAuthority;
        }

        /// <summary>
        /// 获得用户常用信息
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public CommonUserInfo GetCommonUserInfo(string userName)
        {
            CommonUserInfo commonUserInfo = null;

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserId, UserActualName, UserType.UserTypeId, UserTypeName, UserTypeCode, CustomDepartment.DepID, DepName, DepCode, DepValue FROM UserAccount ");
            sb.Append("INNER JOIN UserType ON UserType.UserTypeId =  UserAccount.UserTypeId ");
            sb.Append("INNER JOIN CustomDepartment ON CustomDepartment.DepID =  UserAccount.DepID ");
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
                            string userActualName = DataConvertionHelper.GetString(dataReader[1]);
                            decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            string userTypeName = DataConvertionHelper.GetString(dataReader[3]);
                            string userTypeCode = DataConvertionHelper.GetString(dataReader[4]);
                            decimal depID = DataConvertionHelper.GetDecimal(dataReader[5]);
                            string depName = DataConvertionHelper.GetString(dataReader[6]);
                            string depCode = DataConvertionHelper.GetString(dataReader[7]);
                            string depValue = DataConvertionHelper.GetString(dataReader[8]);

                            //创建 CommonUserInfo 对象
                            commonUserInfo = new CommonUserInfo(userId, userName, userActualName, userTypeId, userTypeName, userTypeCode, depID, depName, depCode, depValue);
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

            return commonUserInfo;
        }

        /// <summary>
        /// 获得用户常用信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public CommonUserInfo GetCommonUserInfo(decimal userId)
        {
            CommonUserInfo commonUserInfo = null;

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserName, UserActualName, UserType.UserTypeId, UserTypeName, UserTypeCode, CustomDepartment.DepID, DepName, DepCode, DepValue FROM UserAccount ");
            sb.Append("INNER JOIN UserType ON UserType.UserTypeId =  UserAccount.UserTypeId ");
            sb.Append("INNER JOIN CustomDepartment ON CustomDepartment.DepID =  UserAccount.DepID ");
            sb.Append("WHERE UserId = @UserId");
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            string userName = DataConvertionHelper.GetString(dataReader[0]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[1]);
                            decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            string userTypeName = DataConvertionHelper.GetString(dataReader[3]);
                            string userTypeCode = DataConvertionHelper.GetString(dataReader[4]);
                            decimal depID = DataConvertionHelper.GetDecimal(dataReader[5]);
                            string depName = DataConvertionHelper.GetString(dataReader[6]);
                            string depCode = DataConvertionHelper.GetString(dataReader[7]);
                            string depValue = DataConvertionHelper.GetString(dataReader[8]);

                            //创建 CommonUserInfo 对象
                            commonUserInfo = new CommonUserInfo(userId, userName, userActualName, userTypeId, userTypeName, userTypeCode, depID, depName, depCode, depValue);
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

            return commonUserInfo;
        }

        /// <summary>
        /// 获得字段权限
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public Int64 GetDataFieldAuthority(string userName)
        {
            Int64 dataFieldAuthority = 0;

            //选择语句
            string sqlSelect = "SELECT DataFieldAuthority  FROM UserAccount WHERE UserName = @UserName";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                dataFieldAuthority = DataConvertionHelper.GetLong(db.ExecuteScalar(dbCommand));
            }

            return dataFieldAuthority;
        }

        /// <summary>
        /// 根据用户身份证号码查用户用户名
        /// </summary>
        /// <param name="userIdentity">用户身份证号码</param>
        /// <returns>用户名</returns>
        public string GetUserNameByUserIdentity(string userIdentity)
        {
            string userName = string.Empty;

            //选择语句
            string sqlSelect = "SELECT UserName FROM UserAccount WHERE UserIdentity = @UserIdentity";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserIdentity", DbType.String, userIdentity);
                userName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
            }

            return userName;
        }

        /// <summary>
        /// 根据用户名查邮件地址
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>邮件地址</returns>
        public string GetEmailAddressByUserName(string userName)
        {
            string emailAddress = string.Empty;

            //选择语句
            string sqlSelect = "SELECT EmailAddress FROM UserAccount WHERE UserName = @UserName";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                emailAddress = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
            }

            return emailAddress;
        }

        /// <summary>
        /// 查询用户邮件地址
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string GetEmailAddress(decimal userId)
        {
            string emailAddress = string.Empty;

            try
            {
                string sqlCommand = "SELECT EmailAddress FROM UserAccount WHERE UserId = @UserId";
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    emailAddress = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return emailAddress;
        }

        /// <summary>
        /// 更新用户邮件地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="emailAddress"></param>
        public void UpdateEmailAddress(decimal userId, string emailAddress)
        {
            try
            {
                string sqlCommand = "UPDATE UserAccount SET EmailAddress = @EmailAddress, UpdatedTime = @UpdatedTime WHERE UserId = @UserId";
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand))
                {
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "EmailAddress", DbType.String, emailAddress);
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DateTime.Now);
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 根据用户名或者证件号查用户编号
        /// </summary>
        /// <param name="key">用户名或者证件号</param>
        /// <returns>用户编号</returns>
        public decimal GetUserIdByKey(string key)
        {
            decimal userId = 0;

            //选择语句
            string sqlSelect = "SELECT UserId FROM UserAccount WHERE UserName = @UserName OR UserIdentity = @UserName OR EmailAddress = @UserName";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserName", DbType.String, key);
                userId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
            }

            return userId;
        }

        /// <summary>
        /// 根据用户名查用户编号
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户编号</returns>
        public decimal GetUserIdByUserName(string userName)
        {
            decimal userId = 0;

            //选择语句
            string sqlSelect = "SELECT UserId FROM UserAccount WHERE UserName = @UserName";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                userId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
            }

            return userId;
        }

        /// <summary>
        /// 向 UserAccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo">userAccountInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        /// <param name="userTypeIds">管理的户类型列表</param>
        /// <param name="departmentIds">管理的单位列表</param>
        /// <param name="roleIds">角色列表</param>
        /// <returns></returns>        
        public decimal Insert(UserAccountInfo userAccountInfo, byte[] imageData, IList<decimal> userTypeIds, IList<decimal> departmentIds, IList<decimal> roleIds)
        {
            //自动增加的关键字的值
            decimal userAccountId = 0;

            //获得系统数据库对userAccountId象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    userAccountId = Insert(userAccountInfo, transaction);
                    if (userTypeIds != null && userTypeIds.Count > 0)
                    {
                        UserTypeScope userTypeScope = new UserTypeScope();
                        foreach (decimal userTypeId in userTypeIds)
                        {
                            userTypeScope.Insert(new CorrelatedModel(userAccountId, userTypeId), db, transaction);
                        }
                    }
                    if (departmentIds != null && departmentIds.Count > 0)
                    {
                        DepartmentScope departmentScope = new DepartmentScope();
                        foreach (decimal departmentId in departmentIds)
                        {
                            departmentScope.Insert(new CorrelatedModel(userAccountId, departmentId, 0), db, transaction);
                        }
                    }
                    if (roleIds != null && roleIds.Count > 0)
                    {
                        RoleAndUser roleAndUser = new RoleAndUser();
                        foreach (decimal roleId in roleIds)
                        {
                            roleAndUser.Insert(new CorrelatedModel(userAccountId, roleId), db, transaction);
                        }
                    }
                    if ((imageData != null) && !string.IsNullOrWhiteSpace(userAccountInfo.PhotoSuffixName))
                    {
                        UpLoadPhoto(string.Format("{0}.{1}", userAccountInfo.UserName, userAccountInfo.PhotoSuffixName), imageData);
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return userAccountId;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newPassword"></param>
        /// <param name="userActualName"></param>
        /// <param name="EmailAddress"></param>
        /// <param name="telephoneNumber"></param>
        /// <param name="imageData"></param>
        /// <param name="photoSuffixName"></param>
        public void Update(string userName, string newPassword, string userActualName, string emailAddress, string telephoneNumber, byte[] imageData, string photoSuffixName)
        {
            //获得系统数据库
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    /* 1. 更新用户信息 */
                    StringBuilder sb = new StringBuilder();
                    sb.Append("UPDATE UserAccount SET UpdatedTime = @UpdatedTime");
                    if (!string.IsNullOrWhiteSpace(userActualName))
                    {
                        sb.Append(", UserActualName = @UserActualName");
                    }
                    if (!string.IsNullOrWhiteSpace(newPassword))
                    {
                        sb.Append(", UserPwd = @UserPwd");
                    }
                    if (!string.IsNullOrWhiteSpace(telephoneNumber))
                    {
                        sb.Append(", TelephoneNumber = @TelephoneNumber");
                    }
                    if (!string.IsNullOrWhiteSpace(emailAddress))
                    {
                        sb.Append(", EmailAddress = @EmailAddress");
                    }
                    if ((imageData != null) && !string.IsNullOrWhiteSpace(photoSuffixName))
                    {
                        sb.Append(", PhotoSuffixName = @PhotoSuffixName");
                    }
                    sb.Append(" WHERE UserName = @UserName");

                    using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                    {
                        //给参数赋值         
                        if (!string.IsNullOrWhiteSpace(newPassword))
                        {
                            string userPwd = CryptographyHelper.Encrypt(newPassword);
                            db.AddInParameter(dbCommand, "UserPwd", DbType.String, userPwd);
                        }
                        if (!string.IsNullOrWhiteSpace(userActualName))
                        {
                            db.AddInParameter(dbCommand, "UserActualName", DbType.String, userActualName);
                        }
                        if (!string.IsNullOrWhiteSpace(emailAddress))
                        {
                            db.AddInParameter(dbCommand, "EmailAddress", DbType.String, emailAddress);
                        }
                        if (!string.IsNullOrWhiteSpace(telephoneNumber))
                        {
                            db.AddInParameter(dbCommand, "TelephoneNumber", DbType.String, telephoneNumber);
                        }
                        if ((imageData != null) && !string.IsNullOrWhiteSpace(photoSuffixName))
                        {
                            db.AddInParameter(dbCommand, "PhotoSuffixName", DbType.String, photoSuffixName);
                        }
                        db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DateTime.Now);
                        db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                        //执行更新操作
                        int count = db.ExecuteNonQuery(dbCommand, transaction);
                        if (count != 1)
                        {
                            throw new Exception("更新失败！");
                        }
                    }

                    /* 2. 更新图片*/
                    if ((imageData != null) && !string.IsNullOrWhiteSpace(photoSuffixName))
                    {
                        UpLoadPhoto(string.Format("{0}.{1}", userName, photoSuffixName), imageData);
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
        }

        /// <summary>
        /// 向 UserAccount 表中插入一条新记录
        /// </summary>
        /// <param name="userAccountInfo">userAccountInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        /// <param name="userTypeIds">管理的户类型列表</param>
        /// <param name="departmentIds">管理的单位列表</param>
        /// <param name="roleIds">角色列表</param>
        public void Update(UserAccountInfo userAccountInfo, byte[] imageData, IList<decimal> userTypeIds, IList<decimal> departmentIds, IList<decimal> roleIds)
        {
            //获得系统数据库
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();

                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    /* 1. 更新用户信息 */
                    Update(userAccountInfo, db, transaction);

                    /* 2.更新用户权限信息 */
                    Update(userAccountInfo.UserId, userTypeIds, departmentIds, roleIds, db, transaction);

                    /* 3.更新图片*/
                    if ((imageData != null) && !string.IsNullOrWhiteSpace(userAccountInfo.PhotoSuffixName))
                    {
                        UpLoadPhoto(string.Format("{0}.{1}", userAccountInfo.UserName, userAccountInfo.PhotoSuffixName), imageData);
                    }
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
        }

        /// <summary>
        /// 获得 UserAccountInfo 对象
        /// </summary>
        ///<param name="userIdentity">用户证件编号</param>
        /// <returns> UserAccountInfo 对象</returns>
        public UserAccountInfo GetModeInfoByUserIdentity(string userIdentity)
        {
            return GetModeInfoByKeyword("UserIdentity", userIdentity, DbType.String);
        }

        /// <summary>
        /// 获得 UserAccountInfo 对象
        /// </summary>
        ///<param name="userName">用户名</param>
        /// <returns> UserAccountInfo 对象</returns>
        public UserAccountInfo GetModelInfo(string userName)
        {
            return GetModeInfoByKeyword("UserName", userName, DbType.String);
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <param name="userValidationType">用户名类型</param>
        /// <returns>如果提供的用户名和密码有效，则返回 true；否则返回 false</returns>
        public bool ValidateUser(string userName, string password, ValidationMode userValidationType)
        {
            bool validate = false;

            try
            {
                UpdateRetryTimes(userName, AppSettingHelper.DefaultUserLockedMinSpan);
                string key = string.Empty;
                StringBuilder sb = new StringBuilder();
                sb.Append("SELECT UserPwd FROM UserAccount WHERE ");
                switch (userValidationType)
                {
                    case ValidationMode.UserName:
                        sb.Append("UserName = @UserName ");
                        key = "UserName";
                        break;

                    case ValidationMode.UserIdentity:
                        sb.Append("UserIdentity = @UserIdentity ");
                        key = "UserIdentity";
                        break;

                    case ValidationMode.MobilePhone:
                        sb.Append("TelephoneNumber = @TelephoneNumber ");
                        key = "TelephoneNumber";
                        break;

                    case ValidationMode.Email:
                        sb.Append("EmailAddress = @EmailAddress ");
                        key = "EmailAddress";
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("用户参数异常！");
                }
                sb.Append("AND LockedOut = @LockedOut");
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, key, DbType.String, userName);
                    db.AddInParameter(dbCommand, "LockedOut", DbType.Boolean, false);
                    string plainText = CryptographyHelper.Decrypt(DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand)));
                    if (!string.IsNullOrEmpty(plainText))
                    {
                        if (plainText.Equals(password))
                        {
                            ClearRetryTimes(userName);
                            validate = true;
                        }
                        else
                        {
                            UpdateRetryTimes(userName);
                        }
                    }
                }
            }
            catch { }

            return validate;
        }

        /// <summary>
        /// 根据用户类型编号查询用户数
        /// </summary>
        /// <param name="userTypeId"></param>
        /// <returns></returns>

        public int GetUserCountByUserTypeId(decimal userTypeId)
        {
            int count = 0;

            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                whereConditons.Add(new WhereConditon("UserTypeId", "UserTypeId", System.Data.DbType.Decimal, userTypeId, DataFieldCondition.Equal));
                count = GetUserCount(whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 根据单位编号查询用户数
        /// </summary>
        /// <param name="depId"></param>
        /// <returns></returns>

        public int GetUserCountByDepId(decimal depId)
        {
            int count = 0;

            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                whereConditons.Add(new WhereConditon("DepId", "DepId", System.Data.DbType.Decimal, depId, DataFieldCondition.Equal));
                count = GetUserCount(whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>  
        /// <returns>数据集</returns>
        public DataSet GetUserList(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UserAccount.UserId, UserAccount.UserName,UserAccount.UserActualName, UserType.UserTypeName, CustomDepartment.DepName, UserAccount.UserIdentity, UserAccount.TelephoneNumber");
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserType", "UserTypeId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomDepartment", "DepId", TableJoin.InnerJoin));
                ds = DataAccessHandler.GetPageRecord(db, "UserAccount ", "UserId", sb.ToString(), false, false, tableLinks, startPosition,
                    count, whereConditons, ref totalCount);
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
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>  
        /// <returns>数据集</returns>
        public DataSet GetUserData(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("UserAccount.UserId, UserAccount.UserName, UserAccount.UserActualName, UserType.UserTypeName, CustomDepartment.DepName, ");
                sb.Append("UserPwd, EmailAddress, IdentificationType, UserIdentity, TelephoneNumber, LastLogonTime, LastLogonIP, PhotoSuffixName, ");
                sb.Append("LockedOut, DataFieldAuthority, DepartmentAuthority, UniqueUserIdentity, UserAccount.Notes, UserAccount.CreatedTime, UserAccount.UpdatedTime");
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserType", "UserTypeId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomDepartment", "DepId", TableJoin.InnerJoin));
                ds = DataAccessHandler.GetPageRecord(db, "UserAccount ", "UserId", sb.ToString(), false, false, tableLinks, startPosition,
                    count, whereConditons, ref totalCount);
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
        /// 获得用户表中的用户名的分页数据集
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>     
        public DataSet GetUserNames(int startPosition, int count, IList<WhereConditon> whereConditons)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                string dataFileNames = @"UserAccount.UserName";               
                ds = DataAccessHandler.GetPageRecord(db, "UserAccount ", dataFileNames, false, null, startPosition, count, 
                    whereConditons, string.Empty, "UserId");
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
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        public DataSet GetUserInfos(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {

                string dataFileNames = @"UserAccount.UserId, UserAccount.UserName,UserAccount.UserActualName, UserType.UserTypeName, CustomDepartment.DepName";
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserType", "UserTypeId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomDepartment", "DepId", TableJoin.InnerJoin));
                ds = DataAccessHandler.GetPageRecord(db, "UserAccount ", "UserId", dataFileNames, false, true, tableLinks, startPosition,
                    count, whereConditons, ref totalCount);
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
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        public DataSet GetPageRecordOfMultiTablesWithRole(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            return GetPageRecordOfMultiTables(startPosition, count, whereConditons, ref totalCount, true);
        }

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        public DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            return GetPageRecordOfMultiTables(startPosition, count, whereConditons, ref totalCount, false);
        }

        /// <summary>
        /// 用户名称是否存在
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>存在返回 true ,否则返回 false</returns>
        public bool IsExistUserName(string userName)
        {
            bool exist = true;

            //选择语句
            string sqlSelect = "SELECT count(UserName) FROM UserAccount WHERE UserName = @UserName";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                int count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                if (count == 0)
                {
                    exist = false;
                }
            }

            return exist;
        }

        /// <summary>
        /// 非系统用户是否存在
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public bool IsExistOrdinaryUserName(string userName)
        {
            bool exist = true;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<WhereConditon> whereConditons = new List<WhereConditon>();
                whereConditons.Add(new WhereConditon("UserAccount", "UserName", "UserName", DbType.String, userName, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("UserAccount", "LockedOut", "LockedOut", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("IsSystemUserType", "IsSystemUserType", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                whereConditons.Add(new WhereConditon("IsSystemDepartment", "IsSystemDepartment", DbType.Boolean, false, DataFieldCondition.Equal, DataFieldInnerRealtion.And));
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserType", "UserTypeId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomDepartment", "DepId", TableJoin.InnerJoin));
                int count = DataAccessHandler.GetRecordCount(db, "UserAccount", tableLinks, whereConditons);
                if (count == 0)
                {
                    exist = false;
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 用户关键字的值是否存在
        /// </summary>
        /// <param name="validationMode"></param>
        /// <param name="keyValue"></param>
        /// <returns>存在返回 true ,否则返回 false</returns>
        public bool IsExistIdentity(ValidationMode validationMode, string keyValue)
        {
            bool exist = true;

            //选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT count(1) FROM UserAccount WHERE LockedOut = @LockedOut ");
            string keyName = string.Empty;
            switch (validationMode)
            {
                case ValidationMode.UserIdentity:
                    keyName = "UserIdentity";
                    sb.Append("AND UserIdentity = @UserIdentity");
                    break;

                case ValidationMode.MobilePhone:
                    keyName = "TelephoneNumber";
                    sb.Append("AND TelephoneNumber = @TelephoneNumber");
                    break;

                case ValidationMode.Email:
                    keyName = "EmailAddress";
                    sb.Append("AND EmailAddress = @EmailAddress");
                    break;

                default:
                    throw new ArgumentException("校验模式的值有误。");
            }

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "LockedOut", DbType.Boolean, false);
                db.AddInParameter(dbCommand, keyName, DbType.String, keyValue);
                int count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
                if (count == 0)
                {
                    exist = false;
                }
            }
            return exist;
        }

        /// <summary>
        /// 通过用户编号获得照片后缀名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>照片后缀名</returns>
        public string GetPhotoSuffixName(string userName)
        {
            string photoSuffixName = string.Empty;

            //查询语句
            string sqlSelect = "SELECT PhotoSuffixName FROM UserAccount WHERE UserName = @UserName";
            try
            {

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    photoSuffixName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return photoSuffixName;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="photoSuffixName"></param>
        /// <param name="fileName"></param>
        /// <param name="imageData"></param>
        public void UpLoadPhoto(string userName, string photoSuffixName, string fileName, byte[] imageData)
        {
            try
            {
                UpLoadPhoto(fileName, imageData);
                UpdatePhotoSuffixName(userName, photoSuffixName);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <param name="imageData">图片数据</param>
        public void UpLoadPhoto(string fileName, byte[] imageData)
        {
            try
            {
                string relativeSubDirOfSavedMessageFiles = GetRelativeSubDirOfSavedPhotos();
                StringBuilder sb = new StringBuilder();
                sb.Append(relativeSubDirOfSavedMessageFiles);
                if (!Directory.Exists(sb.ToString()))
                {
                    Directory.CreateDirectory(sb.ToString());
                }
                sb.Append(fileName);
                //删除服务器上的原有的存储图片             
                if (File.Exists(sb.ToString()))
                {
                    try
                    {
                        File.Delete(sb.ToString());
                    }
                    catch { }
                }
                using (MemoryStream ms = new MemoryStream(imageData))
                {
                    using (Image img = Image.FromStream(ms))
                    {
                        string format = fileName.Substring(fileName.LastIndexOf('.') + 1).ToUpper();
                        ImageFormat imageFormat = FileFormatHelper.GetImageFormat(format);
                        img.Save(sb.ToString(), imageFormat);
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
        /// 自动获取照片的后缀名
        /// 自动搜索几种常见图片格式的照片，如果存在，则返回该照片的后缀名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public string AutoGetPhotoSuffixName(string userName)
        {
            string format = string.Empty;

            string filePath = GetPhotoPath(userName, string.Empty);

            //存储图片是否存在             
            if (!string.IsNullOrEmpty(filePath))
            {
                /* 指定路径的扩展名（包含句点“.”） */
                format = Path.GetExtension(filePath).Remove(0, 1);
            }

            return format;
        }

        /// <summary>
        /// 下载图片
        /// 图片不存在，则自动搜索几种常见图片格式的照片，如果存在则返回该图片
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>图片数据</returns>
        public byte[] DownLoadPhoto(string userName)
        {
            byte[] photoData = null;

            string format = GetPhotoSuffixName(userName);
            string filePath = GetPhotoPath(userName, format);
            //存储图片是否存在             
            if (!string.IsNullOrEmpty(filePath))
            {
                try
                {
                    ImageFormat imgFormat = null;
                    if (string.IsNullOrEmpty(format))
                    {
                        /* 指定路径的扩展名（包含句点“.”） */
                        format = Path.GetExtension(filePath).Remove(0, 1);
                    }
                    imgFormat = FileFormatHelper.GetImageFormat(format);
                    using (Image img = Image.FromFile(filePath))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            img.Save(ms, imgFormat);
                            photoData = ms.ToArray();
                        }
                    }
                }
                catch (Exception exception)
                {
                    //记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return photoData;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 根据用户名查用户实际名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户编号</returns>
        public string GetUserActualNameByUserId(decimal userId)
        {
            string userActualName = null;

            //选择语句
            string sqlSelect = "SELECT UserActualName FROM UserAccount WHERE UserId = @UserId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                userActualName = DataConvertionHelper.GetString(db.ExecuteScalar(dbCommand));
            }

            return userActualName;
        }

        /// <summary>
        /// 根据用户名查部门编号
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <returns>用户编号</returns>
        public decimal GetDepIdByUserId(decimal userId)
        {
            decimal depId;

            //选择语句
            string sqlSelect = "SELECT DepId FROM UserAccount WHERE UserId = @UserId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                depId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
            }

            return depId;
        }

        /// <summary>
        /// 更新用户特定的一些权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dataFieldAuthority"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void UpdateDataFieldAuthority(decimal userId, Int64 dataFieldAuthority, SqlDatabase db, DbTransaction transaction)
        {
            string sqlUpdate = "UPDATE UserAccount SET DataFieldAuthority = @DataFieldAuthority WHERE UserId = @UserId";
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlUpdate))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "DataFieldAuthority", DbType.Int64, dataFieldAuthority);
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
        /// 获得用户特定的一些权限
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public Int64 GetDataFieldAuthority(decimal userId, SqlDatabase db, DbTransaction transaction)
        {
            Int64 dataFieldAuthority = 0;

            //选择语句
            string sqlSelect = "SELECT DataFieldAuthority FROM UserAccount WHERE UserId = @UserId";

            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                if (transaction != null)
                {
                    dataFieldAuthority = (Int64)db.ExecuteScalar(dbCommand, transaction);
                }
                else
                {
                    dataFieldAuthority = (Int64)db.ExecuteScalar(dbCommand);
                }
            }

            return dataFieldAuthority;
        }

        /// <summary>
        /// 根据用户编号查用户所在单位编号
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public decimal GetDepIdByUserId(decimal userId, SqlDatabase db, DbTransaction transaction)
        {
            decimal depId = 0;

            //选择语句
            string sqlSelect = "SELECT DepID FROM UserAccount WHERE UserId = @UserId";
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                if (transaction != null)
                {
                    depId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand, transaction));
                }
                else
                {
                    depId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }

            return depId;
        }

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 UserAccountInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>UserAccountInfo 对象列表</returns>
        public IList<UserAccountInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<UserAccountInfo> userAccountInfos = new List<UserAccountInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM UserAccount");
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
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal depId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[2]);
                            string userName = DataConvertionHelper.GetString(dataReader[3]);
                            string userPwd = DataConvertionHelper.GetString(dataReader[4]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[5]);
                            string emailAddress = DataConvertionHelper.GetString(dataReader[6]);
                            byte identificationType = DataConvertionHelper.GetByte(dataReader[7]);
                            string userIdentity = DataConvertionHelper.GetString(dataReader[8]);
                            string telephoneNumber = DataConvertionHelper.GetString(dataReader[9]);
                            DateTime lastLogonTime = DataConvertionHelper.GetDateTime(dataReader[10]);
                            string lastLogonIP = DataConvertionHelper.GetString(dataReader[11]);
                            string photoSuffixName = DataConvertionHelper.GetString(dataReader[12]);
                            bool lockedOut = DataConvertionHelper.GetBoolean(dataReader[13]);
                            Int64 dataFieldAuthority = DataConvertionHelper.GetLong(dataReader[14]);
                            Int64 departmentAuthority = DataConvertionHelper.GetLong(dataReader[15]);
                            Guid uniqueUserIdentity = (Guid)dataReader[16];
                            string notes = DataConvertionHelper.GetString(dataReader[17]);
                            int retryTimes = DataConvertionHelper.GetInt(dataReader[18]);
                            DateTime createdTime = DataConvertionHelper.GetDateTime(dataReader[19]);
                            DateTime updatedTime = DataConvertionHelper.GetDateTime(dataReader[20]);
                            //将创建 UserAccountInfo 对象加入集合中
                            userAccountInfos.Add(new UserAccountInfo(userId, depId, userTypeId, userName, userPwd,
                            userActualName, emailAddress, identificationType, userIdentity,
                            telephoneNumber, lastLogonTime, lastLogonIP, photoSuffixName, lockedOut,
                            dataFieldAuthority, departmentAuthority, uniqueUserIdentity, notes, retryTimes, createdTime, updatedTime));
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
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserAccountInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
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
        /// 获得以表 UserAccount 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserAccount ", "UserId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  UserAccountInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
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

        #endregion

        #region 自定义私有方法

        /// <summary>
        /// 获得以表 UserAccount 为主表的多表的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        private DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount, bool hasRole)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {

                string dataFileNames = @"UserAccount.UserId, UserAccount.UserName,UserAccount.UserActualName, UserType.UserTypeName, CustomDepartment.DepName, UserAccount.IdentificationType, UserAccount.UserIdentity, UserAccount.EmailAddress, UserAccount.TelephoneNumber, UserAccount.LockedOut";
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserType", "UserTypeId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("CustomDepartment", "DepId", TableJoin.InnerJoin));
                if (hasRole)
                {
                    tableLinks.Add(new TableLink("RoleAndUser", "UserId", TableJoin.LeftOuterJoin));
                    ds = DataAccessHandler.GetPageRecord(db, "UserAccount", "UserId", dataFileNames, true, true, tableLinks, startPosition,
                        count, whereConditons, ref totalCount);
                }
                else
                {
                    ds = DataAccessHandler.GetPageRecord(db, "UserAccount", "UserId", dataFileNames, false, true, tableLinks, startPosition,
                        count, whereConditons, ref totalCount);
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
        /// 更新登录测试次数
        /// </summary>
        /// <param name="userName"></param>
        private void UpdateRetryTimes(string userName, int hours)
        {
            try
            {
                string sqlCommand = "UPDATE UserAccount SET RetryTimes = 0 WHERE UserName = @UserName AND LastLogonTime != NULL AND LastLogonTime >= @LastLogonTime";
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand))
                {
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    db.AddInParameter(dbCommand, "LastLogonTime", DbType.DateTime, DateTime.Now.Subtract(new TimeSpan(0, hours, 0, 0)));
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新重试次数
        /// </summary>
        /// <param name="userName"></param>
        private void UpdateRetryTimes(string userName)
        {
            try
            {
                string sqlCommand = "UPDATE UserAccount SET RetryTimes = RetryTimes + 1, LastLogonTime = @LastLogonTime WHERE UserName = @UserName";
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlCommand))
                {
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    db.AddInParameter(dbCommand, "LastLogonTime", DbType.DateTime, DateTime.Now);
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 通过用户名更新照片后缀名
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="photoSuffixName">照片后缀名</param>
        private void UpdatePhotoSuffixName(string userName, string photoSuffixName)
        {
            //查询语句
            string sqlSelect = "UPDATE UserAccount SET PhotoSuffixName = @PhotoSuffixName WHERE UserName = @UserName";
            try
            {

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userName);
                    db.AddInParameter(dbCommand, "PhotoSuffixName", DbType.String, photoSuffixName);
                    db.ExecuteNonQuery(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新用户照片名称
        /// </summary>
        /// <param name="oldUserName"></param>
        /// <param name="newUserName"></param>
        /// <param name="format"></param>
        private void UpdateUserPhotoName(string oldUserName, string newUserName, string format)
        {
            try
            {
                string relativeSubDirOfSavedMessageFiles = GetRelativeSubDirOfSavedPhotos();
                string sourceFileName = string.Format("{0}{1}.{2}", relativeSubDirOfSavedMessageFiles, oldUserName, format);
                string destFileName = string.Format("{0}{1}.{2}", relativeSubDirOfSavedMessageFiles, newUserName, format);
                //如果服务器上的原有的存储图片则修改          
                if (File.Exists(sourceFileName))
                {
                    File.Move(sourceFileName, destFileName);
                }
            }
            catch { }
        }

        /// <summary>
        /// 删除用户图片
        /// </summary>
        /// <param name="userId"></param>
        private void DeleteUserPhoto(decimal userId)
        {
            //查询语句
            string sqlSelect = "SELECT UserName, PhotoSuffixName FROM UserAccount WHERE UserId = @UserId";
            try
            {
                string userName = string.Empty;
                string photoSuffix = string.Empty;
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        if (dataReader.Read())
                        {
                            userName = DataConvertionHelper.GetString(dataReader[0]);
                            photoSuffix = DataConvertionHelper.GetString(dataReader[1]);
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
                if (!string.IsNullOrWhiteSpace(userName) && !string.IsNullOrWhiteSpace(photoSuffix))
                {
                    DeleteFile(string.Format("{0}.{1}", userName, photoSuffix));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="fileName"></param>
        private void DeleteFile(string fileName)
        {
            try
            {
                string relativeSubDirOfSavedMessageFiles = GetRelativeSubDirOfSavedPhotos();
                StringBuilder sb = new StringBuilder();
                sb.Append(relativeSubDirOfSavedMessageFiles);
                if (!Directory.Exists(sb.ToString()))
                {
                    Directory.CreateDirectory(sb.ToString());
                }
                sb.Append(fileName);
                //删除服务器上的原有的存储图片             
                if (File.Exists(sb.ToString()))
                {
                    File.Delete(sb.ToString());
                }
            }
            catch { }
        }

        /// <summary>
        /// 获得 UserAccountInfo 对象
        /// </summary>
        ///<param name="dataFieldName">关键字</param>
        ///<param name="value">值</param>
        ///<param name="dbType">字段类型</param>
        /// <returns> UserAccountInfo 对象</returns>
        private UserAccountInfo GetModeInfoByKeyword(string dataFieldName, object value, DbType dbType)
        {
            UserAccountInfo userAccountInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            whereConditons.Add(new WhereConditon(dataFieldName, dataFieldName, dbType, value, DataFieldCondition.Equal));
            IList<UserAccountInfo> userAccountInfos = GetModelInfos(whereConditons, null, true);
            if (userAccountInfos != null && userAccountInfos.Count > 0)
            {
                userAccountInfo = userAccountInfos[0];
            }

            return userAccountInfo;
        }

        /// <summary>
        /// 根据角色与单位条件查找用户
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        private IList<CommonUserInfo> GetCommonUserInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            IList<CommonUserInfo> commonUserInfos = new List<CommonUserInfo>();

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserAccount.UserId, UserName, UserActualName, UserType.UserTypeId, UserTypeName, UserTypeCode, UserAccount.DepId, DepName, DepCode, DepValue FROM UserAccount ");
            sb.Append("INNER JOIN UserType ON UserType.UserTypeId =  UserAccount.UserTypeId ");
            sb.Append("INNER JOIN CustomDepartment ON CustomDepartment.DepId =  UserAccount.DepId ");
            sb.Append("INNER JOIN  RoleAndUser ON UserAccount.UserId = RoleAndUser.UserId ");
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
                    //给参数赋值
                    if ((whereConditons != null) && (whereConditons.Count > 0))
                    {
                        DataAccessHandler.AddInParameter(db, dbCommand, whereConditons);
                    }
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            string userName = DataConvertionHelper.GetString(dataReader[1]);
                            string userActualName = DataConvertionHelper.GetString(dataReader[2]);
                            decimal userTypeId = DataConvertionHelper.GetDecimal(dataReader[3]);
                            string userTypeName = DataConvertionHelper.GetString(dataReader[4]);
                            string userTypeCode = DataConvertionHelper.GetString(dataReader[5]);
                            decimal depId = DataConvertionHelper.GetDecimal(dataReader[6]);
                            string depName = DataConvertionHelper.GetString(dataReader[7]);
                            string depCode = DataConvertionHelper.GetString(dataReader[8]);
                            string depValue = DataConvertionHelper.GetString(dataReader[9]);

                            //创建 CommonUserInfo 对象
                            commonUserInfos.Add(new CommonUserInfo(userId, userName, userActualName, userTypeId, userTypeName, userTypeCode, depId, depName, depCode, depValue));
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

            return commonUserInfos;
        }

        /// <summary>
        /// 更新用户权限信息
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userTypeIds"></param>
        /// <param name="departmentIds"></param>
        /// <param name="roleIds"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Update(decimal userId, IList<decimal> userTypeIds, IList<decimal> departmentIds, IList<decimal> roleIds, SqlDatabase db, DbTransaction transaction)
        {
            /* 1. 更新用户管理的用户类型 */
            UserTypeScope userTypeScope = new UserTypeScope();
            IList<decimal> oldUserTypeIds = userTypeScope.GetSecondIds(userId);
            CommonAccessHelper.Update(userTypeScope, userId, userTypeIds, oldUserTypeIds, db, transaction);

            /* 2. 更新用户管理的单位 */
            DepartmentScope departmentScope = new DepartmentScope();
            IList<decimal> oldDepartments = departmentScope.GetSecondIds(userId);
            CommonAccessHelper.Update(departmentScope, userId, departmentIds, oldDepartments, db, transaction);

            /* 3. 更新角色 */
            RoleAndUser roleAndUser = new RoleAndUser();
            IList<decimal> oldRoleIds = roleAndUser.GetSecondIds(userId);
            CommonAccessHelper.Update(roleAndUser, userId, roleIds, oldRoleIds, db, transaction);
        }

        /// <summary>
        /// 获得保存用户图片的固定部分的相对路径
        /// </summary>
        /// <returns></returns>
        private string GetRelativeSubDirOfSavedPhotos()
        {
            StringBuilder sb = new StringBuilder();
            string rootDirectory = AppSettingHelper.DefaultRootDirOfSavedFiles;
            sb.Append(rootDirectory);
            if (!rootDirectory.EndsWith(@"\"))
            {
                sb.Append(@"\");
            }
            sb.AppendFormat(@"{0}\", AppSettingHelper.DefaultSubDirOfSavedPhotos);

            return sb.ToString();
        }

        /// <summary>
        /// 获得用户照片路径
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private string GetPhotoPath(string userName, string format)
        {
            string filePath = string.Empty;

            string relativeSubDirOfSavedMessageFiles = GetRelativeSubDirOfSavedPhotos();
            StringBuilder sb = new StringBuilder();
            sb.Append(relativeSubDirOfSavedMessageFiles);
            if (!Directory.Exists(sb.ToString()))
            {
                Directory.CreateDirectory(sb.ToString());
            }
            sb.Append(userName);
            sb.Append(@".");

            StringBuilder sbDefault = new StringBuilder();
            sbDefault.Append(sb.ToString());
            sb.Append(format);
            //存储图片是否存在             
            if (!string.IsNullOrEmpty(format) && File.Exists(sb.ToString()))
            {
                filePath = sb.ToString();
            }
            else
            {
                StringBuilder sbTmp = new StringBuilder();
                sbTmp.Append(sbDefault.ToString());
                sbTmp.Append("JPG");
                if (File.Exists(sbTmp.ToString()))
                {
                    filePath = sbTmp.ToString();
                }
                else
                {
                    sbTmp.Clear();
                    sbTmp.Append(sb.ToString());
                    sbTmp.Append("BMP");
                    if (File.Exists(sbTmp.ToString()))
                    {
                        filePath = sbTmp.ToString();
                    }
                    else
                    {
                        sbTmp.Clear();
                        sbTmp.Append(sb.ToString());
                        sbTmp.Append("GIF");
                        if (File.Exists(sbTmp.ToString()))
                        {
                            filePath = sbTmp.ToString();
                        }
                        else
                        {
                            int pos = sbDefault.ToString().LastIndexOf('\\');
                            if (pos >= 0)
                            {
                                sbDefault.Remove(pos + 1, sbDefault.Length - pos - 1);
                            }
                            filePath = string.Format("{0}{1}.jpg", sbDefault, AppSettingHelper.DomainName);
                            if (!File.Exists(filePath))
                            {
                                sbDefault.Append("DEFAULT.JPG");
                                if (File.Exists(sbDefault.ToString()))
                                {
                                    filePath = sbDefault.ToString();
                                }
                            }
                        }
                    }
                }
            }

            return filePath;
        }

        /// <summary>
        /// 向 UserAccount 表中插入一条新记录
        /// </summary>        
        /// <param name="userAccountInfo">userAccountInfo 对象</param>
        /// <param name="transaction">事务</param>
        /// <returns>自动增加的关键字的值</returns>
        private decimal Insert(UserAccountInfo userAccountInfo, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal userAccountId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO UserAccount(DepId, UserTypeId, UserName, ");
            if (!string.IsNullOrWhiteSpace(userAccountInfo.UserPwd))
            {
                sb.Append("UserPwd, ");
            }
            sb.Append("UserActualName, EmailAddress, IdentificationType, UserIdentity, TelephoneNumber, ");
            sb.Append("LastLogonTime, LastLogonIP, PhotoSuffixName, LockedOut, DataFieldAuthority, ");
            sb.Append("DepartmentAuthority, UniqueUserIdentity, Notes, RetryTimes, CreatedTime, UpdatedTime)");
            sb.Append("VALUES (@DepId, @UserTypeId, @UserName, @UserPwd, @UserActualName, ");
            sb.Append("@EmailAddress, @IdentificationType, @UserIdentity, @TelephoneNumber, ");
            sb.Append("@LastLogonTime, @LastLogonIP, @PhotoSuffixName, @LockedOut, @DataFieldAuthority, ");
            sb.Append("@DepartmentAuthority, @UniqueUserIdentity, @Notes, @RetryTimes, @CreatedTime, @UpdatedTime);");
            sb.Append("SET @UserId = SCOPE_IDENTITY()");
            DateTime time = DateTime.Now;
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "UserId", DbType.Decimal, 8);
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, userAccountInfo.DepId);
                    db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, userAccountInfo.UserTypeId);
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userAccountInfo.UserName);
                    if (!string.IsNullOrWhiteSpace(userAccountInfo.UserPwd))
                    {
                        string userPwd = CryptographyHelper.Encrypt(userAccountInfo.UserPwd);
                        db.AddInParameter(dbCommand, "UserPwd", DbType.String, userPwd);
                    }
                    db.AddInParameter(dbCommand, "UserActualName", DbType.String, userAccountInfo.UserActualName);
                    db.AddInParameter(dbCommand, "EmailAddress", DbType.String, userAccountInfo.EmailAddress);
                    db.AddInParameter(dbCommand, "IdentificationType", DbType.Byte, Convert.ToByte(userAccountInfo.IdentificationType));
                    db.AddInParameter(dbCommand, "UserIdentity", DbType.String, userAccountInfo.UserIdentity);
                    db.AddInParameter(dbCommand, "TelephoneNumber", DbType.String, userAccountInfo.TelephoneNumber);
                    db.AddInParameter(dbCommand, "LastLogonTime", DbType.DateTime, DataConvertionHelper.SetDateTime(userAccountInfo.LastLogonTime));
                    db.AddInParameter(dbCommand, "LastLogonIP", DbType.String, userAccountInfo.LastLogonIP);
                    db.AddInParameter(dbCommand, "PhotoSuffixName", DbType.String, userAccountInfo.PhotoSuffixName);
                    db.AddInParameter(dbCommand, "LockedOut", DbType.Boolean, userAccountInfo.LockedOut);
                    db.AddInParameter(dbCommand, "DataFieldAuthority", DbType.Int64, userAccountInfo.DataFieldAuthority);
                    db.AddInParameter(dbCommand, "DepartmentAuthority", DbType.Int64, userAccountInfo.DepartmentAuthority);
                    db.AddInParameter(dbCommand, "UniqueUserIdentity", DbType.Guid, userAccountInfo.UniqueUserIdentity);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, userAccountInfo.Notes);
                    db.AddInParameter(dbCommand, "RetryTimes", DbType.Int32, DataConvertionHelper.SetInt(userAccountInfo.RetryTimes));
                    db.AddInParameter(dbCommand, "CreatedTime", DbType.DateTime, time);
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, time);
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
        /// 更新 UserAccountInfo 对象
        /// </summary>
        /// <param name="userAccountInfo">UserAccountInfo 对象</param>
        /// <param name="transaction">事务</param>
        private void Update(UserAccountInfo userAccountInfo, SqlDatabase db, DbTransaction transaction)
        {
            UserAccountInfo oldUserAccountInfo = GetModelInfo(userAccountInfo.UserId);
            /* 用户名，用户类型，单位等发生变更 */
            if ((oldUserAccountInfo.UserTypeId != userAccountInfo.UserTypeId) || (oldUserAccountInfo.DepId != userAccountInfo.DepId)
                || !oldUserAccountInfo.UserName.Equals(userAccountInfo.UserName))
            {
                CustomTable customTable = new CustomTable();
                CustomDataField customDataField = new CustomDataField();
                IList<CommonNode> commonNodes = customTable.GetTables();
                StringBuilder sbUpdate = new StringBuilder();
                bool userNameUpdated = false;
                if (!oldUserAccountInfo.UserName.Equals(userAccountInfo.UserName))
                {
                    sbUpdate.AppendFormat("UserName = '{0}', ", userAccountInfo.UserName);
                    /* 更改用户图片名称 */
                    UpdateUserPhotoName(oldUserAccountInfo.UserName, userAccountInfo.UserName, oldUserAccountInfo.PhotoSuffixName);
                    userNameUpdated = true;
                }
                if (oldUserAccountInfo.UserTypeId != userAccountInfo.UserTypeId)
                {
                    sbUpdate.AppendFormat("UserTypeId = {0}, ", userAccountInfo.UserTypeId);
                }
                if (oldUserAccountInfo.DepId != userAccountInfo.DepId)
                {
                    sbUpdate.AppendFormat("DepId = {0}, ", userAccountInfo.DepId);
                }
                sbUpdate.Remove(sbUpdate.Length - 2, 2);
                sbUpdate.AppendFormat(" WHERE  UserId = {0} ", userAccountInfo.UserId);

                /* 文件路径 */
                StringBuilder sbPath = new StringBuilder();
                sbPath.Append(AppSettingHelper.DefaultRootDirOfSavedFiles);
                if (!AppSettingHelper.DefaultRootDirOfSavedFiles.EndsWith(@"\"))
                {
                    sbPath.Append(@"\");
                }
                sbPath.Append(AppSettingHelper.DefaultSubDirOfUploadFiles);
                foreach (CommonNode commonNode in commonNodes)
                {
                    try
                    {
                        byte dataWarehouseId = customTable.GetDataWarehouseId(commonNode.NodeId);
                        SqlDatabase dbBusiness = DataAccessHelper.GetDatabase(DataWarehouseHelper.GetDataSourceName((DataWarehouse)dataWarehouseId));
                        using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(string.Format("UPDATE {0} SET {1}", commonNode.NodeName, sbUpdate.ToString())))
                        {
                            dbBusiness.ExecuteNonQuery(dbCommand);
                        }
                        if (userNameUpdated)
                        {
                            UpdateAttachmentNames(userAccountInfo.UserId, oldUserAccountInfo.UserName, userAccountInfo.UserName, commonNode.NodeId,
                                commonNode.NodeName, sbPath.ToString(), dbBusiness);
                        }
                    }
                    catch (Exception exception)
                    {
                        //记录日志, 抛出异常, 不包装异常 
                        ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                    }
                }
            }

            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserAccount SET DepId = @DepId, UserTypeId = @UserTypeId, UserName = @UserName, ");
            if (!string.IsNullOrWhiteSpace(userAccountInfo.UserPwd))
            {
                sb.Append("UserPwd = @UserPwd,");
            }
            sb.Append("UserActualName = @UserActualName, EmailAddress = @EmailAddress, IdentificationType = @IdentificationType, UserIdentity = @UserIdentity, ");
            sb.Append("TelephoneNumber = @TelephoneNumber, LastLogonTime = @LastLogonTime, LastLogonIP = @LastLogonIP, ");
            sb.Append("PhotoSuffixName = @PhotoSuffixName, LockedOut = @LockedOut, DataFieldAuthority = @DataFieldAuthority, ");
            sb.Append("DepartmentAuthority = @DepartmentAuthority, Notes = @Notes, UpdatedTime = @UpdatedTime ");
            sb.Append("WHERE UserId = @UserId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userAccountInfo.UserId);
                    db.AddInParameter(dbCommand, "DepId", DbType.Decimal, userAccountInfo.DepId);
                    db.AddInParameter(dbCommand, "UserTypeId", DbType.Decimal, userAccountInfo.UserTypeId);
                    db.AddInParameter(dbCommand, "UserName", DbType.String, userAccountInfo.UserName);
                    if (!string.IsNullOrWhiteSpace(userAccountInfo.UserPwd))
                    {
                        string userPwd = CryptographyHelper.Encrypt(userAccountInfo.UserPwd);
                        db.AddInParameter(dbCommand, "UserPwd", DbType.String, userPwd);
                    }
                    db.AddInParameter(dbCommand, "UserActualName", DbType.String, userAccountInfo.UserActualName);
                    db.AddInParameter(dbCommand, "EmailAddress", DbType.String, userAccountInfo.EmailAddress);
                    db.AddInParameter(dbCommand, "IdentificationType", DbType.Byte, Convert.ToByte(userAccountInfo.IdentificationType));
                    db.AddInParameter(dbCommand, "UserIdentity", DbType.String, userAccountInfo.UserIdentity);
                    db.AddInParameter(dbCommand, "TelephoneNumber", DbType.String, userAccountInfo.TelephoneNumber);
                    db.AddInParameter(dbCommand, "LastLogonTime", DbType.DateTime, DataConvertionHelper.SetDateTime(userAccountInfo.LastLogonTime));
                    db.AddInParameter(dbCommand, "LastLogonIP", DbType.String, userAccountInfo.LastLogonIP);
                    db.AddInParameter(dbCommand, "PhotoSuffixName", DbType.String, userAccountInfo.PhotoSuffixName);
                    db.AddInParameter(dbCommand, "LockedOut", DbType.Boolean, userAccountInfo.LockedOut);
                    db.AddInParameter(dbCommand, "DataFieldAuthority", DbType.Int64, userAccountInfo.DataFieldAuthority);
                    db.AddInParameter(dbCommand, "DepartmentAuthority", DbType.Int64, userAccountInfo.DepartmentAuthority);
                    db.AddInParameter(dbCommand, "Notes", DbType.String, userAccountInfo.Notes);
                    db.AddInParameter(dbCommand, "UpdatedTime", DbType.DateTime, DateTime.Now);
                    //执行更新操作
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
        /// 更新附件名称
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="newUserName"></param>
        /// <param name="commonNodes"></param>
        private void UpdateAttachmentNames(decimal userId, string userName, string newUserName,
            decimal tableId, string tablePhysicalName, string path, SqlDatabase dbBusiness)
        {
            CustomDataField customDataField = new CustomDataField();
            StringBuilder sbDataField = new StringBuilder();
            IList<CustomDataFieldInfo> dataFields = customDataField.GetModelInfos(tableId, DataFieldFilter.Attachement);
            if (dataFields.Count > 0)
            {
                sbDataField.AppendFormat("UPDATE {0} SET ", tablePhysicalName);
                foreach (var dataField in dataFields)
                {
                    sbDataField.AppendFormat("{0} = REPLACE({0},'{1}_','{2}_'), ", dataField.PhysicalName, userName, newUserName);
                    string dir = string.Format(@"{0}\{1}\", path, dataField.PhysicalName);
                    if (Directory.Exists(dir))
                    {
                        DirectoryInfo info = new DirectoryInfo(dir);
                        FileInfo[] fileInfos = info.GetFiles(string.Format("{0}*", userName));
                        if (fileInfos != null && fileInfos.Length > 0)
                        {
                            foreach (var fileInfo in fileInfos)
                            {
                                string destFileName = fileInfo.FullName.Replace(string.Format("{0}_", userName), string.Format("{0}_", newUserName));
                                try
                                {
                                    File.Move(fileInfo.FullName, destFileName);
                                }
                                catch { }
                            }
                        }
                    }
                }
                sbDataField.Remove(sbDataField.Length - 2, 2);
                sbDataField.AppendFormat(" WHERE UserId = @UserId");
                try
                {
                    using (DbCommand dbCommand = dbBusiness.GetSqlStringCommand(sbDataField.ToString()))
                    {
                        //给参数赋值
                        dbBusiness.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                        dbBusiness.ExecuteNonQuery(dbCommand);
                    }
                }
                catch { }
            }
        }

        #endregion

        #region 过时的函数

        /// <summary>
        /// 获得用户细节信息列表
        /// </summary>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public DataSet GetUserInfos(IList<WhereConditon> whereConditons)
        {
            DataSet ds = null;

            //生成选择语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT UserId, UserName, UserActualName, UserAccount.UserTypeId, UserTypeCode, UserAccount.DepId, DepCode FROM UserAccount ");
            sb.Append("INNER JOIN UserType ON UserType.UserTypeId =  UserAccount.UserTypeId ");
            sb.Append("INNER JOIN CustomDepartment ON CustomDepartment.DepID =  UserAccount.DepID ");
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
                    //给参数赋值
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

        #endregion

        #endregion
    }
}
