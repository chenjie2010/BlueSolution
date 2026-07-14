//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserMessageService.cs
// 描述: UserMessage 操作服务类
// 作者：ChenJie 
// 编写日期：2019/4/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.CustomLibrary;
using Blue.Model.SystemModule;
using Blue.BusinessInterface.SystemModule;
using Blue.WCFContracts.SystemModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.SystemModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.UserMessage.
    /// </summary>
    public class UserMessageService : IUserMessageContract
    {
        #region 业务实例
        
        private static readonly IUserMessageHandler userMessageHandler = BusinessLogicContainer.Instance.SystemModuleContainer.Resolve<IUserMessageHandler>();

        #endregion
        
		#region 构造函数

		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public UserMessageService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 UserMessage 表中插入一条新记录
		/// </summary>
		/// <param name="userMessageInfo"></param>
		/// <returns></returns>
		public decimal Insert(UserMessageInfo userMessageInfo)
		{
            return userMessageHandler.Insert(userMessageInfo);
		}
        
        /// <summary>
		/// 获得 UserMessageInfo 对象
		/// </summary>
		///<param name="messageId">消息编号</param>
		/// <returns> UserMessageInfo 对象</returns>
		public UserMessageInfo GetModelInfo(decimal messageId)
		{	
            return userMessageHandler.GetModelInfo(messageId);           
		}		
		
        /// <summary>
		/// 更新 UserMessageInfo 对象
		/// </summary>
		/// <param name="userMessageInfo">UserMessageInfo 对象</param>
		public void Update(UserMessageInfo userMessageInfo)
		{	          
            userMessageHandler.Update(userMessageInfo);
        }	
  
        /// <summary>
		/// 删除 UserMessageInfo 对象
		/// </summary>
		///<param name="messageId">消息编号</param>
		/// <returns> UserMessageInfo 对象</returns>
		public void Delete(decimal messageId)
		{	
            userMessageHandler.Delete(messageId);
        }
        
        /// <summary>
        /// 获得 UserMessageInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>UserMessageInfo 对象列表</returns>
        public IList<UserMessageInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return userMessageHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 UserMessage 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> UserMessageInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return userMessageHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

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
            return userMessageHandler.InsertUserMessage(userMessageInfo, attachmentCategory, upLoadFileInfos, roleIds);
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
            userMessageHandler.UpdateUserMessageInfo(userMessageInfo, attachmentCategory, upLoadFileInfos, roleIds);
        }

        /// <summary>
        /// 根据公告编号获得角色列表
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRoles(decimal messageId)
        {
            return userMessageHandler.GetRoles(messageId);
        }

        /// <summary>
        /// 是否授权读取该通知
        /// </summary>
        /// <param name="messageId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool IsAuthoritiedNotice(decimal messageId, decimal userId)
        {
            return userMessageHandler.IsAuthoritiedNotice(messageId, userId);
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
            return userMessageHandler.GetPageRecord(startPosition, count, whereConditons, ref totalCount);
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
            return userMessageHandler.GetPageRecordOfMultiTables(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
        }

        /// <summary>
        /// 批量删除消息列表
        /// </summary>
        /// <param name="messageIds"></param>
        public void DeleteUserMessages(IList<decimal> messageIds)
        {
            userMessageHandler.DeleteUserMessages(messageIds);
        }


        #endregion
    }
}
