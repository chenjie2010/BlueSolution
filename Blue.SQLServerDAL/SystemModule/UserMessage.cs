//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserMessage.cs
// 描述: UserMessage 数据层访问类
// 作者：ChenJie 
// 编写日期：2019/4/10
// Copyright 2019
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
using AppFramework.Core;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.SQLServerDAL.GeneralAffairModule;

namespace Blue.SQLServerDAL.SystemModule
{
    /// <summary>
    /// UserMessage 表的数据层访问类
    /// </summary>
    public class UserMessage : IUserMessage
    {
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserMessage()
		{
		}
        
		#endregion

        #region 实现默认接口
		
		/// <summary>
		/// 向 UserMessage 表中插入一条新记录
		/// </summary>
		/// <param name="userMessageInfo">userMessageInfo 对象</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(UserMessageInfo userMessageInfo)
		{
			//自动增加的关键字的值
			decimal userMessageId = 0;
			//生成插入语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("INSERT INTO UserMessage(UserId, MessageTitle, MessageContent, MessageType, IsDraft, ");
			sb.Append("IsAttach, MessagePriority, InitalTime, ExpiredTime, DeliveredTime)");
			sb.Append("VALUES (@UserId, @MessageTitle, @MessageContent, @MessageType, @IsDraft, ");
			sb.Append("@IsAttach, @MessagePriority, @InitalTime, @ExpiredTime, @DeliveredTime);");
			sb.Append("SET @MessageId = SCOPE_IDENTITY()");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddOutParameter(dbCommand, "MessageId", DbType.Decimal,10);
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userMessageInfo.UserId);
					db.AddInParameter(dbCommand, "MessageTitle", DbType.String, userMessageInfo.MessageTitle);
					db.AddInParameter(dbCommand, "MessageContent", DbType.String, userMessageInfo.MessageContent);
					db.AddInParameter(dbCommand, "MessageType", DbType.Byte, userMessageInfo.MessageType);
					db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, userMessageInfo.IsDraft);
					db.AddInParameter(dbCommand, "IsAttach", DbType.Boolean, userMessageInfo.IsAttach);
					db.AddInParameter(dbCommand, "MessagePriority", DbType.Byte, userMessageInfo.MessagePriority);
					db.AddInParameter(dbCommand, "InitalTime", DbType.DateTime, userMessageInfo.InitalTime);
					db.AddInParameter(dbCommand, "ExpiredTime", DbType.DateTime, userMessageInfo.ExpiredTime);
					db.AddInParameter(dbCommand, "DeliveredTime", DbType.DateTime, DateTime.Now);
					//执行插入操作
                    if (db.ExecuteNonQuery(dbCommand) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
					userMessageId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@MessageId"].Value, 0);
				}
			}
			catch (Exception exception)
            {
				//记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return userMessageId;
		}

        /// <summary>
		/// 获得 UserMessageInfo 对象
		/// </summary>
		///<param name="messageId">消息编号</param>
		/// <returns> UserMessageInfo 对象</returns>
		public UserMessageInfo GetModelInfo(decimal messageId)
		{			
			UserMessageInfo  userMessageInfo = null;
            
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            //给参数赋值
            whereConditons.Add(new WhereConditon("MessageId", "MessageId", DbType.Decimal, messageId, DataFieldCondition.Equal));

            //创建集合对象
            IList<UserMessageInfo>  userMessageInfos = GetModelInfos(whereConditons, null, true);
            if (userMessageInfos != null && userMessageInfos.Count > 0)
            {
                userMessageInfo = userMessageInfos[0];
            }

            return userMessageInfo;            
		}
        
        /// <summary>
		/// 更新 UserMessageInfo 对象
		/// </summary>
		/// <param name="userMessageInfo">UserMessageInfo 对象</param>
		public void Update(UserMessageInfo userMessageInfo)
		{		
			//生成更新语句
			StringBuilder sb = new StringBuilder();			
			sb.Append("UPDATE UserMessage SET UserId = @UserId, MessageTitle = @MessageTitle, MessageContent = @MessageContent, ");
			sb.Append("MessageType = @MessageType, IsDraft = @IsDraft, IsAttach = @IsAttach, ");
			sb.Append("MessagePriority = @MessagePriority, InitalTime = @InitalTime, ExpiredTime = @ExpiredTime, ");
			sb.Append("DeliveredTime = @DeliveredTime ");
			sb.Append("WHERE MessageId = @MessageId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					//给参数赋值
					db.AddInParameter(dbCommand, "MessageId", DbType.Decimal, userMessageInfo.MessageId);
					db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userMessageInfo.UserId);
					db.AddInParameter(dbCommand, "MessageTitle", DbType.String, userMessageInfo.MessageTitle);
					db.AddInParameter(dbCommand, "MessageContent", DbType.String, userMessageInfo.MessageContent);
					db.AddInParameter(dbCommand, "MessageType", DbType.Byte, userMessageInfo.MessageType);
					db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, userMessageInfo.IsDraft);
					db.AddInParameter(dbCommand, "IsAttach", DbType.Boolean, userMessageInfo.IsAttach);
					db.AddInParameter(dbCommand, "MessagePriority", DbType.Byte, userMessageInfo.MessagePriority);
					db.AddInParameter(dbCommand, "InitalTime", DbType.DateTime, userMessageInfo.InitalTime);
					db.AddInParameter(dbCommand, "ExpiredTime", DbType.DateTime, userMessageInfo.ExpiredTime);
					db.AddInParameter(dbCommand, "DeliveredTime", DbType.DateTime, userMessageInfo.DeliveredTime);
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
		///  删除 UserMessageInfo 对象
		/// </summary>
	    ///<param name="messageId">消息编号</param>
		public void Delete(decimal messageId)
		{
			//生成删除语句
			StringBuilder sb = new StringBuilder();	
			sb.Append("DELETE FROM UserMessage ");
			sb.Append("WHERE MessageId = @MessageId");
			//获得系统数据库对象
			SqlDatabase db = DataAccessHelper.GetDatabase();
			try
            {
				using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
				{
					db.AddInParameter(dbCommand, "MessageId", DbType.Decimal, messageId);
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
		/// 获得 UserMessageInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserMessageInfo 对象列表</returns>
		public IList<UserMessageInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return GetModelInfos(whereConditons, sortingCondtions, false);
		}        
        
        /// <summary>
		/// 获得 UserMessage 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>UserMessageInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            try
            {
                //获得系统数据库对象
                SqlDatabase db = DataAccessHelper.GetDatabase();
                count = DataAccessHandler.GetRecordCount(db, "UserMessage ", "MessageId", false, whereConditons);
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
        /// 批量插入消息
        /// </summary>
        /// <param name="userMessageInfo"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="roleIds"></param>
        /// <returns></returns>
        public decimal InsertUserMessage(UserMessageInfo userMessageInfo, AttachmentCategory attachmentCategory, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<decimal> roleIds)
        {
            //自动增加的关键字的值
            decimal messageId = 0;

            //获得系统数据库对userAccountId象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    messageId = Insert(userMessageInfo, db, transaction);
                    PriavteAttachment messageAttachment = new PriavteAttachment();
                    messageAttachment.Insert(messageId, (byte)attachmentCategory, upLoadFileInfos, db, transaction);
                    MessageAndRole messageAndRole = new MessageAndRole();
                    if (roleIds.Count > 0)
                    {
                        messageAndRole.Insert(messageId, roleIds, db, transaction);
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

            return messageId;
        }

        /// <summary>
        /// 批量更新信息
        /// </summary>
        /// <param name="userMessageInfo"></param>
        /// <param name="attachmentCategory"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="roleIds"></param>
        public void UpdateUserMessageInfo(UserMessageInfo userMessageInfo, AttachmentCategory attachmentCategory, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<decimal> roleIds)
        {
            //获得系统数据库对userAccountId象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            MessageAndRole messageAndRole = new MessageAndRole();
            IList<decimal> oldRoleIds = messageAndRole.GetRoleIdsByMessageId(userMessageInfo.MessageId);
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    Update(userMessageInfo, db, transaction);                    
                    foreach (decimal roleId in roleIds)
                    {
                        bool exist = false;
                        foreach (decimal oldRoleId in oldRoleIds)
                        {
                            if (roleId == oldRoleId)
                            {
                                exist = true;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            messageAndRole.Insert(new CorrelatedModel(roleId, userMessageInfo.MessageId), db, transaction);
                        }
                    }
                    foreach (decimal oldRoleId in oldRoleIds)
                    {
                        bool exist = false;
                        foreach (decimal roleId in roleIds)
                        {
                            if (roleId == oldRoleId)
                            {
                                exist = true;
                                break;
                            }
                        }
                        if (!exist)
                        {
                            messageAndRole.Delete(oldRoleId, userMessageInfo.MessageId, db, transaction);
                        }
                    }
                    PriavteAttachment messageAttachment = new PriavteAttachment();
                    messageAttachment.Update(userMessageInfo.MessageId, (byte)attachmentCategory, upLoadFileInfos, db, transaction);                    
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
        /// 批量删除消息列表
        /// </summary>
        /// <param name="messageIds"></param>
        public void DeleteUserMessages(IList<decimal> messageIds)
        {
            if (messageIds.Count == 0)
            {
                return;
            }

            //生成删除语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserMessage WHERE ");
            int index = 0;
            foreach (decimal messageId in messageIds)
            {
                sb.AppendFormat("MessageId = @MessageId{0}", index);
                index++;
            }

            MessageAndRole messageAndRole = new MessageAndRole();
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            using (DbConnection connection = db.CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();
                try
                {
                    index = 0;
                    foreach (decimal messageId in messageIds)
                    {
                        messageAndRole.DeleteBySecondForeignKey(messageId, db, transaction);
                        using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                        {
                            db.AddInParameter(dbCommand, string.Format("MessageId{0}", index), DbType.Decimal, DataConvertionHelper.SetDecimal(messageId));
                            //执行删除操作
                            if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
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
                    //不记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
        }

        /// <summary>
        /// 获得表 UserMessage 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                ds = DataAccessHandler.GetPageRecord(db, "UserMessage ", "MessageId", "MessageId, MessageTitle, MessageType, InitalTime, ExpiredTime, IsDraft, IsAttach, DeliveredTime", false, false, startPosition,
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
        /// 获得以表 UserMessage 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                string dataFileNames = @"UserMessage.MessageId, MessageTitle, MessageType, IsAttach, DeliveredTime, UserAccount.UserActualName";
                IList<TableLink> tableLinks = new List<TableLink>();
                tableLinks.Add(new TableLink("MessageAndRole", "MessageId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("MessageAndRole", "RoleAndUser", "RoleId", TableJoin.InnerJoin));
                tableLinks.Add(new TableLink("UserMessage", "UserAccount", "UserId", TableJoin.InnerJoin));               
                ds =  DataAccessHandler.GetPageRecord(db, "UserMessage ", "MessageId", dataFileNames, false, false, tableLinks, startPosition, 
                    count, whereConditons, sortingCondtions, ref totalCount);       
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        #endregion

        #endregion

        #region 公有方法

        #endregion

        #region 私有方法

        #region 默认私有方法	

        /// <summary>
        /// 获得 UserMessageInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <param name="onlyOne">第一条记录</param>
        /// <returns>UserMessageInfo 对象列表</returns>
        private IList<UserMessageInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, bool onlyOne)
		{
			//创建集合对象
			IList<UserMessageInfo>  userMessageInfos = new List<UserMessageInfo>();
			//查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT ");
            if (onlyOne)
            {
                sb.Append("TOP 1 ");
            }
            sb.Append("* FROM UserMessage");
            
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
							decimal messageId = DataConvertionHelper.GetDecimal(dataReader[0]);
							decimal userId = DataConvertionHelper.GetDecimal(dataReader[1]);
							string messageTitle = DataConvertionHelper.GetString(dataReader[2]);
							string messageContent = DataConvertionHelper.GetString(dataReader[3]);
							byte messageType = DataConvertionHelper.GetByte(dataReader[4]);
							bool isDraft = DataConvertionHelper.GetBoolean(dataReader[5]);
							bool isAttach = DataConvertionHelper.GetBoolean(dataReader[6]);
							byte messagePriority = DataConvertionHelper.GetByte(dataReader[7]);
							DateTime initalTime = DataConvertionHelper.GetDateTime(dataReader[8]);
							DateTime expiredTime = DataConvertionHelper.GetDateTime(dataReader[9]);
							DateTime deliveredTime = DataConvertionHelper.GetDateTime(dataReader[10]);
							//将创建 UserMessageInfo 对象加入集合中
							userMessageInfos.Add(new UserMessageInfo(messageId, userId, messageTitle, messageContent, messageType, 
							isDraft, isAttach, messagePriority, initalTime, expiredTime, 
							deliveredTime));							
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
            
			return userMessageInfos;
		}
        
		
		/// <summary>
		/// 获得 UserMessageInfo 对象的数据集
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
		/// <returns>UserMessageInfo 对象的数据集</returns>
		private DataSet GetAll(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
			DataSet ds = null;
            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM UserMessage");
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
        /// 获得以表 UserMessage 为主表的多表的分页数据集(只能以主键为排序字段)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserMessage ", "MessageId", "*", false, false, tableLinks, startPosition, 
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
        /// 获得表 UserMessage 的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
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
                ds =  DataAccessHandler.GetPageRecord(db, "UserMessage ", "MessageId", "*", false, false, startPosition, 
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
        /// 删除满足条件的所有  UserMessageInfo  对象
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        private int Delete(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            //查询语句
            StringBuilder sb = new StringBuilder();
            sb.Append("DELETE FROM UserMessage");
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
        /// 向 UserMessage 表中插入一条新记录
        /// </summary>
        /// <param name="userMessageInfo">userMessageInfo 对象</param>
        /// <param name="db">数据库对象</param>
        /// <param name="transaction">事务</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(UserMessageInfo userMessageInfo, SqlDatabase db, DbTransaction transaction)
        {
            //自动增加的关键字的值
            decimal userMessageId = 0;

            //生成插入语句
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO UserMessage(UserId, MessageTitle, MessageContent, MessageType, IsDraft, ");
            sb.Append("IsAttach, MessagePriority, InitalTime, ExpiredTime, DeliveredTime)");
            sb.Append("VALUES (@UserId, @MessageTitle, @MessageContent, @MessageType, @IsDraft, ");
            sb.Append("@IsAttach, @MessagePriority, @InitalTime, @ExpiredTime, @DeliveredTime);");
            sb.Append("SET @MessageId = SCOPE_IDENTITY()");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddOutParameter(dbCommand, "MessageId", DbType.Decimal, 10);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userMessageInfo.UserId);
                    db.AddInParameter(dbCommand, "MessageTitle", DbType.String, userMessageInfo.MessageTitle);
                    db.AddInParameter(dbCommand, "MessageContent", DbType.String, userMessageInfo.MessageContent);
                    db.AddInParameter(dbCommand, "MessageType", DbType.Byte, userMessageInfo.MessageType);
                    db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, userMessageInfo.IsDraft);
                    db.AddInParameter(dbCommand, "IsAttach", DbType.Boolean, userMessageInfo.IsAttach);
                    db.AddInParameter(dbCommand, "MessagePriority", DbType.Byte, userMessageInfo.MessagePriority);
                    db.AddInParameter(dbCommand, "InitalTime", DbType.DateTime, userMessageInfo.InitalTime);
                    db.AddInParameter(dbCommand, "ExpiredTime", DbType.DateTime, userMessageInfo.ExpiredTime);
                    db.AddInParameter(dbCommand, "DeliveredTime", DbType.DateTime, DateTime.Now);
                    //执行插入操作
                    if (db.ExecuteNonQuery(dbCommand, transaction) != 1)
                    {
                        throw new Exception("插入失败！");
                    }
                    userMessageId = DataConvertionHelper.GetDecimal(dbCommand.Parameters["@MessageId"].Value, 0);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userMessageId;
        }

        /// <summary>
        /// 更新 UserMessageInfo 对象
        /// </summary>
        /// <param name="userMessageInfo"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public void Update(UserMessageInfo userMessageInfo, SqlDatabase db, DbTransaction transaction)
        {
            //生成更新语句
            StringBuilder sb = new StringBuilder();
            sb.Append("UPDATE UserMessage SET UserId = @UserId, MessageTitle = @MessageTitle, MessageContent = @MessageContent, ");
            sb.Append("MessageType = @MessageType, IsDraft = @IsDraft, IsAttach = @IsAttach, ");
            sb.Append("MessagePriority = @MessagePriority, InitalTime = @InitalTime, ExpiredTime = @ExpiredTime, ");
            sb.Append("DeliveredTime = @DeliveredTime ");
            sb.Append("WHERE MessageId = @MessageId");

            try
            {
                using (DbCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    db.AddInParameter(dbCommand, "MessageId", DbType.Decimal, userMessageInfo.MessageId);
                    db.AddInParameter(dbCommand, "UserId", DbType.Decimal, userMessageInfo.UserId);
                    db.AddInParameter(dbCommand, "MessageTitle", DbType.String, userMessageInfo.MessageTitle);
                    db.AddInParameter(dbCommand, "MessageContent", DbType.String, userMessageInfo.MessageContent);
                    db.AddInParameter(dbCommand, "MessageType", DbType.Byte, userMessageInfo.MessageType);
                    db.AddInParameter(dbCommand, "IsDraft", DbType.Boolean, userMessageInfo.IsDraft);
                    db.AddInParameter(dbCommand, "IsAttach", DbType.Boolean, userMessageInfo.IsAttach);
                    db.AddInParameter(dbCommand, "MessagePriority", DbType.Byte, userMessageInfo.MessagePriority);
                    db.AddInParameter(dbCommand, "InitalTime", DbType.DateTime, userMessageInfo.InitalTime);
                    db.AddInParameter(dbCommand, "ExpiredTime", DbType.DateTime, userMessageInfo.ExpiredTime);
                    db.AddInParameter(dbCommand, "DeliveredTime", DbType.DateTime, userMessageInfo.DeliveredTime);
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
