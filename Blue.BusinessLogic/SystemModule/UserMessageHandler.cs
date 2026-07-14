//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserMessageHandler.cs
// 描述: UserMessage 业务处理类
// 作者：ChenJie 
// 编写日期：2019/4/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.CustomLibrary;
using Blue.DALFactory;
using Blue.IDAL.SystemModule;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;

namespace Blue.BusinessLogic.SystemModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.UserMessage.
    /// </summary>
    public class UserMessageHandler : IUserMessageHandler
    {
        #region 工厂类实例
        
        private static readonly IUserMessage dalUserMessage = SystemDataAccessFactory.CreateUserMessage();
        private static readonly  IMessageAndRole dalMessageAndRole = SystemDataAccessFactory.CreateMessageAndRole();
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserMessageHandler()
		{  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 usermessage 表中插入一条新记录
		/// </summary>
		/// <param name="userMessageInfo"></param>
		/// <returns></returns>
		public decimal Insert(UserMessageInfo userMessageInfo)
		{
            //自动增加的关键字的值
			decimal userMessageId = 0;
            
			// 验证输入
			if (userMessageInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                userMessageId = dalUserMessage.Insert(userMessageInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
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
            
			// 验证输入
			if(messageId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                userMessageInfo =  dalUserMessage.GetModelInfo(messageId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return userMessageInfo;
		}        
        
        /// <summary>
		/// 更新 UserMessageInfo 对象
		/// </summary>
		/// <param name="userMessageInfo">UserMessageInfo 对象</param>
		public void Update(UserMessageInfo userMessageInfo)
		{	
            // 验证输入
            if (userMessageInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalUserMessage.Update(userMessageInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 UserMessageInfo 对象
		/// </summary>
		///<param name="messageId">消息编号</param>
		/// <returns> UserMessageInfo 对象</returns>
		public void Delete(decimal messageId)
		{		
            // 验证输入
			if(messageId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalUserMessage.Delete(messageId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 UserMessageInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserMessageInfo  对象列表</returns>
		public IList<UserMessageInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<UserMessageInfo>  userMessageInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                userMessageInfos = dalUserMessage.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return userMessageInfos;
		}               
        
        /// <summary>
		/// 获得 CustomSheet 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomSheetInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalUserMessage.GetTotalCount(whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
		}

        #endregion

        #region 自定义方法

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
            decimal userMessageId = 0;

            // 验证输入
            if (userMessageInfo == null)
            {
                throw new ArgumentException("不能插入空对象。");
            }

            try
            {
                userMessageId = dalUserMessage.InsertUserMessage(userMessageInfo, attachmentCategory, upLoadFileInfos, roleIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userMessageId;
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
            // 验证输入
            if (userMessageInfo == null)
            {
                throw new ArgumentException("不能更新空对象。");
            }

            try
            {
                 dalUserMessage.UpdateUserMessageInfo(userMessageInfo, attachmentCategory, upLoadFileInfos, roleIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 批量删除消息列表
        /// </summary>
        /// <param name="messageIds"></param>
        public void DeleteUserMessages(IList<decimal> messageIds)
        {
            try
            {
                dalUserMessage.DeleteUserMessages(messageIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
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

            try
            {
                ds = dalUserMessage.GetPageRecord(startPosition, count, whereConditons, ref totalCount);
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

            try
            {
                ds = dalUserMessage.GetPageRecordOfMultiTables(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 是否授权读取该通知
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsAuthoritiedNotice(decimal messageId, decimal userId)
        {
            bool authoritied = false;

            if (messageId <= 0 || userId <= 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                authoritied = dalMessageAndRole.IsAuthoritiedNotice(messageId, userId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return authoritied;
        }

        /// <summary>
        /// 根据公告编号获得角色列表
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRoles(decimal messageId)
        {
            //创建集合对象
            IList<CommonNode> roles = null;

            try
            {
                roles = dalMessageAndRole.GetRoles(messageId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return roles;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
