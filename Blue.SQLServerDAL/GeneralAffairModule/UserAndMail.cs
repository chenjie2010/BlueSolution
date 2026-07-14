//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserAndMail.cs
// 描述：UserAndMail 数据层访问类
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
using AppFramework.Reference.DataAccessLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.IDAL.GeneralAffairModule;
using Blue.Model.GeneralAffairModule;

namespace Blue.SQLServerDAL.GeneralAffairModule
{
    /// <summary>
    /// UserAndMail 表的数据层访问类
    /// </summary>
    public class UserAndMail : CorrelatedTableDataAcess, IUserAndMail
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserAndMail() : base("UserAndMail", "MailId", "UserId", "ReadStatus")
        {
		}

        #endregion

        #region 实现默认接口


        #endregion

        #region 实现自定义接口

        #region 实现新增接口

        /// <summary>
		/// 获得 UserAndMailInfo 对象
		/// </summary>
		///<param name="mailId">邮件编号</param>
		///<param name="userId">用户编号</param>
		/// <returns> UserAndMailInfo 对象</returns>
		public UserAndMailInfo GetModelInfo(decimal mailId, decimal userId)
        {
            UserAndMailInfo userAndMailInfo = null;

            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("MailId", "MailId", System.Data.DbType.Decimal, mailId, DataFieldCondition.Equal, DataFieldInnerRealtion.None));
            whereConditons.Add(new WhereConditon("UserId", "UserId", System.Data.DbType.Decimal, userId, DataFieldCondition.Equal, DataFieldInnerRealtion.And));

            //创建集合对象
            IList<UserAndMailInfo> userAndMailInfos = GetModelInfos(whereConditons, null, true);
            if (userAndMailInfos != null && userAndMailInfos.Count > 0)
            {
                userAndMailInfo = userAndMailInfos[0];
            }

            return userAndMailInfo;
        }

        /// <summary>
        /// 更新 UserAndMailInfo 对象
        /// </summary>
        /// <param name="userAndMailInfo">UserAndMailInfo 对象</param>
        public void Update(UserAndMailInfo userAndMailInfo)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserAndMail SET ReadStatus = @ReadStatus, DeliveryMode = @DeliveryMode, IsDeleted = @IsDeleted ");
            sb.Append("WHERE MailId = @MailId AND UserId = @UserId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "MailId", DbType.Decimal, userAndMailInfo.MailId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userAndMailInfo.UserId);
                    db.AddInParameter(dbCommand, "ReadStatus", DbType.Byte, userAndMailInfo.ReadStatus);
                    db.AddInParameter(dbCommand, "DeliveryMode", DbType.Byte, userAndMailInfo.DeliveryMode);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, userAndMailInfo.IsDeleted);
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
		/// 获得 UserAndMailInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserAndMailInfo 对象列表</returns>
		public IList<UserAndMailInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return GetModelInfos(whereConditons, sortingCondtions, false);
        }

        /// <summary>
        /// 获得 UserAndMail 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>UserAndMailInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "UserAndMail ", "MailId", false, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #endregion

        #region 公有方法

        /// <summary>
        /// 插入邮件信息
        /// </summary>
        /// <param name="userAndMailInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Insert(UserAndMailInfo userAndMailInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO UserAndMail(MailId, UserId, ReadStatus, DeliveryMode, IsDeleted)");
            sb.Append("VALUES (@MailId, @UserId, @ReadStatus, @DeliveryMode, @IsDeleted);");
            sb.Append("SET @UserId = SCOPE_IDENTITY()");
                       
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "MailId", DbType.Decimal, userAndMailInfo.MailId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userAndMailInfo.UserId);
                    db.AddInParameter(dbCommand, "ReadStatus", DbType.Byte, userAndMailInfo.ReadStatus);
                    db.AddInParameter(dbCommand, "DeliveryMode", DbType.Byte, userAndMailInfo.DeliveryMode);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, userAndMailInfo.IsDeleted);
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
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 插入邮件关系
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="roleInfos"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>

        public void Insert(decimal mailId, IList<decimal> roleInfos, SqlDatabase db, DbTransaction transaction)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO UserAndMail(MailId, UserId, ReadStatus, DeliveryMode, IsDeleted) ");
            //sb.AppendFormat("SELECT UserId, {0}, {1}, ", mailId, (byte)(MailState.New), (byte)(MailState.New), );
            sb.Append("SELECT  @MailId, UserId, @ReadStatus, @DeliveryMode, @IsDeleted");            
            try
            {
                StringBuilder sbUserAndMail = new StringBuilder();
                foreach (decimal roleId in roleInfos)
                {
                    sbUserAndMail.Clear();
                    sbUserAndMail.Append(sb.ToString());
                    sbUserAndMail.Append(" FROM RoleAndUser WHERE RoleId = @RoleId AND UserId NOT IN (SELECT UserId FROM UserAndMail WHERE MailId = @MailId)");
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sbUserAndMail.ToString()))
                    {
                        //给参数赋值
                        db.AddInParameter(dbCommand, "RoleId", DbType.Decimal, DataConvertionHelper.SetDecimal(roleId));
                        db.AddInParameter(dbCommand, "MailId", DbType.Decimal, DataConvertionHelper.SetDecimal(mailId));
                        db.AddInParameter(dbCommand, "ReadStatus", DbType.Byte, (byte)MailState.New);
                        db.AddInParameter(dbCommand, "DeliveryMode", DbType.Byte, (byte)MailDeliveryMode.Delivery);
                        db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, false);
                        
                        db.ExecuteNonQuery(dbCommand, transaction);
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
        /// 查询与该邮件相关的用户数量
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        public int GetUsersById(decimal mailId)
        {
            int count = 0;

            string sqlSelect = "SELECT count(1) FROM PrivateMail WHERE MailId = @MailId";
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbCommand dbCommand = db.GetSqlStringCommand(sqlSelect))
            {
                //给参数赋值
                db.AddInParameter(dbCommand, "MailId", DbType.Decimal, mailId);
                count = DataConvertionHelper.GetInt(db.ExecuteScalar(dbCommand));
            }

            return count;
        }

        /// <summary>
        /// 更新删除状态
        /// </summary>
        /// <param name="mailIds"></param>
        /// <param name="userId"></param>
        /// <param name="isDelete"></param>
        public void Update(IList<decimal> mailIds, decimal userId, bool isDelete)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserAndMail SET IsDeleted = @IsDeleted ");
            sb.Append("WHERE MailId = @MailId AND UserId = @UserId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    foreach (decimal mailId in mailIds)
                    {
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            db.AddInParameter(dbCommand, "MailId", DbType.Decimal, mailId);
                            db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                            db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, isDelete);
                            //执行删除操作
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
        /// 更新删除状态
        /// </summary>
        /// <param name="mailId"></param>
        /// <param name="userId"></param>
        /// <param name="isDelete"></param>
        public void Update(decimal mailId, decimal userId, bool isDelete)
        {
            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserAndMail SET IsDeleted = @IsDeleted ");
            sb.Append("WHERE MailId = @MailId AND UserId = @UserId");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    db.AddInParameter(dbCommand, "MailId", DbType.Decimal, mailId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userId);
                    db.AddInParameter(dbCommand, "IsDeleted", DbType.Boolean, isDelete);
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

        #region 私有方法

        #region 默认私有方法

        /// <summary>
		/// 获得 UserAndMailInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>UserAndMailInfo 对象列表</returns>
		private IList<UserAndMailInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
        {
            //创建集合对象
            IList<UserAndMailInfo> userAndMailInfos = new List<UserAndMailInfo>();
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }

            sb.Append(" * FROM UserAndMail");
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
                            byte readStatus = DataConvertionHelper.GetByte(dataReader[2]);
                            byte deliveryMode = DataConvertionHelper.GetByte(dataReader[3]);
                            bool isDeleted = DataConvertionHelper.GetBoolean(dataReader[4]);
                            //将创建 UserAndMailInfo 对象加入集合中
                            userAndMailInfos.Add(new UserAndMailInfo(mailId, userId, readStatus, deliveryMode, isDeleted));
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

            return userAndMailInfos;
        }

        #endregion

        #region 自定义私有方法        

        #endregion

        #endregion
    }
}
