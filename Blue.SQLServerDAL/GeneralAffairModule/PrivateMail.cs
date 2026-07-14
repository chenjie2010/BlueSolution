//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：PrivateMail.cs
// 描述：PrivateMail 数据层访问类
// 作者：ChenJie 
// 编写日期：2017/9/12
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.GeneralAffairModule;
using Blue.Model.GeneralAffairModule;

namespace Blue.SQLServerDAL.GeneralAffairModule
{
    /// <summary>
    /// PrivateMail 表的数据层访问类
    /// </summary>
    public class PrivateMail : IPrivateMail
    {
        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public PrivateMail()
        {
        }

        #endregion

        #region 实现默认接口

        /// <summary>
		/// 向 PrivateMail 表中插入一条新记录
		/// </summary>
		/// <param name="privateMailInfo">privateMailInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(PrivateMailInfo privateMailInfo)
        {
            //自动增加的关键字的值
            decimal privateMailId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                privateMailId = Insert(privateMailInfo, null, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return privateMailId;
        }

        /// <summary>
		/// 获得 PrivateMailInfo 对象
		/// </summary>
		///<param name="mailId">邮件编号</param>
		/// <returns> PrivateMailInfo 对象</returns>
		public PrivateMailInfo GetModelInfo(decimal mailId)
        {
            PrivateMailInfo privateMailInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("MailId", "MailId", System.Data.DbType.Decimal, mailId, DataFieldCondition.Equal));

            //创建集合对象
            IList<PrivateMailInfo> privateMailInfos = GetModelInfos(whereConditons, null, true);
            if (privateMailInfos != null && privateMailInfos.Count > 0)
            {
                privateMailInfo = privateMailInfos[0];
            }

            return privateMailInfo;
        }

        /// <summary>
        /// 更新 PrivateMailInfo 对象
        /// </summary>
        /// <param name="privateMailInfo">PrivateMailInfo 对象</param>
        public void Update(PrivateMailInfo privateMailInfo)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                Update(privateMailInfo, db, null);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        ///  删除 PrivateMailInfo 对象
        /// </summary>
        ///<param name="mailId">邮件编号</param>
        public void Delete(decimal mailId)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM PrivateMail ");
            sb.Append("WHERE MailId = @MailId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "MailId", DbType.Decimal, mailId);
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
        /// 获得 PrivateMailInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>PrivateMailInfo 对象列表</returns>
        public IList<PrivateMailInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        #endregion

        #region 实现自定义接口

        #region 实现新增接口

        /// <summary>
        /// 获得 PrivateMail 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>PrivateMailInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();

                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserAndMail", "MailId", TableJoin.FullOuterJoin));
                count = DataAccessHandler.GetRecordCount(db, "PrivateMail", tableLinks, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 通过邮件编号获取用户编号
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        public decimal GetUserId(decimal mailId)
        {
            decimal userId = decimal.MinValue;

            try
            {
                string sqlSelect = "SELECT UserId FROM PrivateMail WHERE MailId = @MailId";

                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "MailId", DbType.Decimal, DataConvertionHelper.SetDecimal(mailId));
                    userId = DataConvertionHelper.GetDecimal(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userId;
        }

        /// <summary>
        /// 插入邮件信息和发送邮件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <returns></returns>
        public decimal Insert(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //自动增加的关键字的值
            decimal mailId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    mailId = Insert(privateMailInfo, upLoadFileInfos, db, transaction);                                     
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return mailId;
        }

        /// <summary>
        /// 插入邮件信息和发送邮件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="userInfos"></param>
        /// <param name="roleInfos"></param>
        /// <returns></returns>
        public decimal Insert(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, Dictionary<decimal, MailDeliveryMode> userInfos, IList<decimal> roleInfos)
        {
            //自动增加的关键字的值
            decimal mailId = 0;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    mailId = Insert(privateMailInfo, upLoadFileInfos, db, transaction);
                    Insert(mailId, userInfos, roleInfos, db, transaction);
                    transaction.Commit();
                }
                catch (Exception exception)
                {
                    transaction.Rollback();
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return mailId;
        }

        /// <summary>
        /// 更新邮件信息，并写入收件人相关信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="userInfos"></param>
        /// <param name="roleInfos"></param>
        public void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, Dictionary<decimal, MailDeliveryMode> userInfos, IList<decimal> roleInfos)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    Update(privateMailInfo, upLoadFileInfos, db, transaction);
                    Insert(privateMailInfo.MailId, userInfos, roleInfos, db, transaction);
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
        /// 更新邮件和附件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        public void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    Update(privateMailInfo, upLoadFileInfos, db, transaction);
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
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailId"></param>
        public void Delete(decimal userId, decimal mailId)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            bool delete = GetDeletedState(mailId);
            UserAndMail userAndMail = new UserAndMail();
            int count = userAndMail.GetUsersById(mailId);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();                
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    userAndMail.Delete(mailId, userId, db, transaction);
                    if (count <= 1 & delete)
                    {
                        Delete(mailId, db, transaction);
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
        /// 删除邮件
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mailIds"></param>
        public void Delete(decimal userId, IList<decimal> mailIds)
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            UserAndMail userAndMail = new UserAndMail();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                foreach (decimal mailId in mailIds)
                {
                    bool delete = GetDeletedState(mailId);
                    int count = userAndMail.GetUsersById(mailId);
                    DbTransaction transaction = connection.BeginTransaction();
                    try
                    {
                        userAndMail.Delete(mailId, userId, db, transaction);
                        if (count <= 1 && delete)
                        {
                            Delete(mailId, db, transaction);
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
        }

        /// <summary>
        /// 获得表 PrivateMail 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                ds = DataAccessHandler.GetPageRecord(db, "PrivateMail ", "MailId", "MailId, MailPriority, MailTitle, HasAttachment, SendTime", false, false, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
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
        /// 获得以表 PrivateMail 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {

                string dataFileNames = @"PrivateMail.MailId, MailPriority, ReadStatus, MailTitle, HasAttachment, DeliveryMode, SendTime";
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserAndMail", "MailId", TableJoin.InnerJoin));
                ds = DataAccessHandler.GetPageRecord(db, "PrivateMail", "MailId", dataFileNames, false, false, tableLinks, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
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
        /// 获得以表 PrivateMail 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecordOfMultiTablesOnFullOuterJoin(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {

                string dataFileNames = @"PrivateMail.MailId, MailPriority, ReadStatus, MailTitle, HasAttachment, DeliveryMode, SendTime";
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("UserAndMail", "MailId", TableJoin.FullOuterJoin));
                ds = DataAccessHandler.GetPageRecord(db, "PrivateMail", "MailId", dataFileNames, false, false, tableLinks, startPosition,
                    count, whereConditons, sortingCondtions, ref totalCount);
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
        /// 更新邮件状态
        /// </summary>
        /// <param name="mailIds"></param>
        /// <param name="isDeleted"></param>
        public void Update(IList<decimal> mailIds, bool isDeleted)
        {
            //生成更新语句
            string update = "UPDATE PrivateMail SET IsDeleted = @IsDeleted WHERE MailId = @MailId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach(decimal mailId in mailIds)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(update))
                        {
                            //给参数赋值
                            db.AddInParameter(dbCommand, "MailId", DbType.Decimal, mailId);
                            db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, isDeleted);
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
        /// 更新邮件状态
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="isDeleted"></param>
        public void Update(decimal mailId, bool isDeleted)
        {
            //生成更新语句
            string update = "UPDATE PrivateMail SET IsDeleted = @IsDeleted WHERE MailId = @MailId";

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(update))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "MailId", DbType.Decimal, mailId);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Byte, isDeleted);
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

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法

        /// <summary>
        /// 获得 PrivateMailInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>PrivateMailInfo 对象列表</returns>
        private IList<PrivateMailInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<PrivateMailInfo> privateMailInfos = new List<PrivateMailInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM PrivateMail");
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
                            decimal mailId = DataConvertionHelper.GetDecimal(dataReader[0]);
                            decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
                            string mailTitle = DataConvertionHelper.GetString(dataReader[2]);
                            string mailContent = DataConvertionHelper.GetString(dataReader[3]);
                            byte mailPriority = DataConvertionHelper.GetByte(dataReader[4]);
                            bool hasAttachment = DataConvertionHelper.GetBoolean(dataReader[5]);
                            DateTime sendTime = DataConvertionHelper.GetDateTime(dataReader[6]);
                            bool isDraft = DataConvertionHelper.GetBoolean(dataReader[7]);
                            string receiver = DataConvertionHelper.GetString(dataReader[8]);
                            string copyer = DataConvertionHelper.GetString(dataReader[9]);
                            string blind = DataConvertionHelper.GetString(dataReader[10]);
                            bool isDeleted = DataConvertionHelper.GetBoolean(dataReader[11]);
                            //将创建 PrivateMailInfo 对象加入集合中
                            privateMailInfos.Add(new PrivateMailInfo(mailId, userId, mailTitle, mailContent, mailPriority,
                            hasAttachment, sendTime, isDraft, receiver, copyer,
                            blind, isDeleted));
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

            return privateMailInfos;
        }

        /// <summary>
        /// 获得 PrivateMailInfo 对象的数据集
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>PrivateMailInfo 对象的数据集</returns>
        private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM PrivateMail");
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
        /// 获得表 PrivateMail 的分页数据集(只能以主键为排序字段)
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
                ds = DataAccessHandler.GetPageRecord(db, "PrivateMail ", "MailId", "*", false, false, startPosition,
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
        /// 获得以表 PrivateMail 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "PrivateMail ", "MailId", "*", false, false, tableLinks, startPosition, 
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
        /// 删除满足条件的所有  PrivateMailInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM PrivateMail");
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
        /// 向 PrivateMail 表中插入一条新记录和附件
        /// </summary>
        /// <param name="privateMailInfo">privateMailInfo 对象</param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        /// <returns>自动增加的关键字的值</returns>
        private decimal Insert(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal privateMailId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO PrivateMail(UserId, MailTitle, MailContent, MailPriority, HasAttachment, ");
            sb.Append("SendTime, IsDraft, Receiver, Copyer, Blind, IsDeleted)");
            sb.Append("VALUES (@UserId, @MailTitle, @MailContent, @MailPriority, @HasAttachment, ");
            sb.Append("@SendTime, @IsDraft, @Receiver, @Copyer, @Blind, @IsDeleted);");
            sb.Append("SET @MailId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "MailId", DbType.Decimal, 8);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, privateMailInfo.UserId);
                    db.AddInParameter(dbCommand, "MailTitle", DbType.String, privateMailInfo.MailTitle);
                    db.AddInParameter(dbCommand, "MailContent", DbType.String, privateMailInfo.MailContent);
                    db.AddInParameter(dbCommand, "MailPriority", DbType.Byte, privateMailInfo.MailPriority);
                    db.AddInParameter(dbCommand, "HasAttachment", DbType.Boolean, privateMailInfo.HasAttachment);
                    db.AddInParameter(dbCommand, "SendTime", DbType.DateTime, privateMailInfo.SendTime);
                    db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, privateMailInfo.IsDraft);
                    db.AddInParameter(dbCommand, "Receiver", DbType.String, privateMailInfo.Receiver);
                    db.AddInParameter(dbCommand, "Copyer", DbType.String, privateMailInfo.Copyer);
                    db.AddInParameter(dbCommand, "Blind", DbType.String, privateMailInfo.Blind);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, privateMailInfo.IsDeleted);
                    /* 1. 执行插入记录操作 */
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
                    privateMailId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@MailId"].Value, 0);

                    /* 2. 插入附件 */
                    if (upLoadFileInfos != null && upLoadFileInfos.Count > 0)
                    {
                        PriavteAttachment messageAttachment = new PriavteAttachment();
                        messageAttachment.Insert(privateMailId, (byte)AttachmentCategory.Mail, upLoadFileInfos, db, transaction);
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return privateMailId;
        }

        /// <summary>
        /// 插入邮件收件人相关信息
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="userInfos"></param>
        /// <param name="roleInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Insert(decimal mailId, Dictionary<decimal, MailDeliveryMode> userInfos, IList<decimal> roleInfos, SqlDatabase db, DbTransaction transaction)
        {
            try
            {
                UserAndMail userAndMail = new UserAndMail();
                if (roleInfos != null && roleInfos.Count > 0)
                {
                    userAndMail.Insert(mailId, roleInfos, db, transaction);
                }
                foreach (KeyValuePair<decimal, MailDeliveryMode> keyValue in userInfos)
                {
                    userAndMail.Insert(new UserAndMailInfo(mailId, keyValue.Key, (byte)MailState.New, (byte)keyValue.Value, false), db, transaction);
                }               
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新 PrivateMailInfo 对象
        /// </summary>
        /// <param name="privateMailInfo">PrivateMailInfo 对象</param>
        private void Update(PrivateMailInfo privateMailInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE PrivateMail SET UserId = @UserId, MailTitle = @MailTitle, MailContent = @MailContent, ");
            sb.Append("MailPriority = @MailPriority, HasAttachment = @HasAttachment, SendTime = @SendTime, ");
            sb.Append("IsDraft = @IsDraft, Receiver = @Receiver, Copyer = @Copyer, ");
            sb.Append("Blind = @Blind, IsDeleted = @IsDeleted ");
            sb.Append("WHERE MailId = @MailId");
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "MailId", DbType.Decimal, privateMailInfo.MailId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, privateMailInfo.UserId);
                    db.AddInParameter(dbCommand, "MailTitle", DbType.String, privateMailInfo.MailTitle);
                    db.AddInParameter(dbCommand, "MailContent", DbType.String, privateMailInfo.MailContent);
                    db.AddInParameter(dbCommand, "MailPriority", DbType.Byte, privateMailInfo.MailPriority);
                    db.AddInParameter(dbCommand, "HasAttachment", DbType.Boolean, privateMailInfo.HasAttachment);
                    db.AddInParameter(dbCommand, "SendTime", DbType.DateTime, privateMailInfo.SendTime);
                    db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, privateMailInfo.IsDraft);
                    db.AddInParameter(dbCommand, "Receiver", DbType.String, privateMailInfo.Receiver);
                    db.AddInParameter(dbCommand, "Copyer", DbType.String, privateMailInfo.Copyer);
                    db.AddInParameter(dbCommand, "Blind", DbType.String, privateMailInfo.Blind);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, privateMailInfo.IsDeleted);
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
        /// 更新邮件和附件信息
        /// </summary>
        /// <param name="privateMailInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        private void Update(PrivateMailInfo privateMailInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, SqlDatabase db, DbTransaction transaction)
        {
            try
            {
                Update(privateMailInfo, db, transaction);
                PriavteAttachment messageAttachment = new PriavteAttachment();
                messageAttachment.Update(privateMailInfo.MailId, (byte)AttachmentCategory.Mail, upLoadFileInfos, db, transaction);
            }
            catch (Exception exception)
            {
                transaction.Rollback();
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        ///  删除 PrivateMailInfo 对象
        /// </summary>
        ///<param name="mailId">邮件编号</param>
        ///<param name="db">数据库对象</param>
        ///<param name="transaction">事务</param>
        private void Delete(decimal mailId, SqlDatabase db, DbTransaction transaction)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM PrivateMail ");
            sb.Append("WHERE MailId = @MailId");           
            try
            {
                PriavteAttachment priavteAttachment = new PriavteAttachment();
                priavteAttachment.DeleteByMailId(mailId, (byte)AttachmentCategory.Mail, db, transaction);
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "MailId", DbType.Decimal, mailId);
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

        /// <summary>
        /// 获得删除状态
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        private bool GetDeletedState(decimal mailId)
        {
            bool delete = false;

            //选择语句
            string sqlSelect = "SELECT IsDeleted FROM PrivateMail WHERE MailId = @MailId";
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
                {
                    db.AddInParameter(dbCommand, "MailId", DbType.Decimal, mailId);
                    delete = DataConvertionHelper.GetBoolean(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return delete;
        }

        #endregion

        #endregion
    }
}
