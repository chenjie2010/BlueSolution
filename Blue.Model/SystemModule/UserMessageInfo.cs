//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserMessageInfo.cs
// 描述: UserMessageInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/4/10
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.SystemModule
{
	/// <summary>
	/// <para>UserMessageInfo 类</para>
	/// <para>用户消息</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class UserMessageInfo
	{
		#region 内部成员变量
        
		private decimal _messageId;
		private decimal _userId;
		private string _messageTitle = string.Empty;
		private string _messageContent = string.Empty;
		private byte _messageType;
		private bool _isDraft;
		private bool _isAttach;
		private byte _messagePriority;
		private DateTime _initalTime;
		private DateTime _expiredTime;
		private DateTime _deliveredTime;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public UserMessageInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="messageId">消息编号</param>
		///<param name="userId">用户编号</param>
		///<param name="messageTitle">消息标题</param>
		///<param name="messageContent">消息内容</param>
		///<param name="messageType">消息类型</param>
		///<param name="isDraft">是否草稿</param>
		///<param name="isAttach">是否包含附件</param>
		///<param name="messagePriority">消息优先级</param>
		///<param name="initalTime">起止时间</param>
		///<param name="expiredTime">截止时间</param>
		///<param name="deliveredTime">发送时间</param>
		public UserMessageInfo(decimal messageId, decimal userId, string messageTitle, string messageContent, byte messageType, 
			bool isDraft, bool isAttach, byte messagePriority, DateTime initalTime, DateTime expiredTime, 
			DateTime deliveredTime)
		{
			_messageId       = messageId;
			_userId          = userId;
			_messageTitle    = messageTitle;
			_messageContent  = messageContent;
			_messageType     = messageType;
			_isDraft         = isDraft;
			_isAttach        = isAttach;
			_messagePriority = messagePriority;
			_initalTime      = initalTime;
			_expiredTime     = expiredTime;
			_deliveredTime   = deliveredTime;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 消息编号
        /// </summary>
		public decimal MessageId
		{
			get 
			{				
				return _messageId;
			}
			set 
			{
				if (_messageId == value)
                    return;
				_messageId = value;
			}
		}

		/// <summary>
        /// 用户编号
        /// </summary>
		public decimal UserId
		{
			get 
			{				
				return _userId;
			}
			set 
			{
				if (_userId == value)
                    return;
				_userId = value;
			}
		}

		/// <summary>
        /// 消息标题
        /// </summary>
        [NotNullValidator(MessageTemplate = " 消息标题不能为空")]
        [StringLengthValidator(1, 512, MessageTemplate = "消息标题长度范围在1位～512位！")]
		public string MessageTitle
		{
			get 
			{				
				return _messageTitle;
			}
			set 
			{
				if (_messageTitle == value)
                    return;
				_messageTitle = value;
			}
		}

		/// <summary>
        /// 消息内容
        /// </summary>
        [NotNullValidator(MessageTemplate = " 消息内容不能为空")]
        [StringLengthValidator(1, Int32.MaxValue, MessageTemplate = "消息内容长度超过规定长度(2147483647个字符)！")]
		public string MessageContent
		{
			get 
			{				
				return _messageContent;
			}
			set 
			{
				if (_messageContent == value)
                    return;
				_messageContent = value;
			}
		}

		/// <summary>
        /// 消息类型
        /// </summary>
		public byte MessageType
		{
			get 
			{				
				return _messageType;
			}
			set 
			{
				if (_messageType == value)
                    return;
				_messageType = value;
			}
		}

		/// <summary>
        /// 是否草稿
        /// </summary>
		public bool IsDraft
		{
			get 
			{				
				return _isDraft;
			}
			set 
			{
				if (_isDraft == value)
                    return;
				_isDraft = value;
			}
		}

		/// <summary>
        /// 是否包含附件
        /// </summary>
		public bool IsAttach
		{
			get 
			{				
				return _isAttach;
			}
			set 
			{
				if (_isAttach == value)
                    return;
				_isAttach = value;
			}
		}

		/// <summary>
        /// 消息优先级
        /// </summary>
		public byte MessagePriority
		{
			get 
			{				
				return _messagePriority;
			}
			set 
			{
				if (_messagePriority == value)
                    return;
				_messagePriority = value;
			}
		}

		/// <summary>
        /// 起止时间
        /// </summary>
		public DateTime InitalTime
		{
			get 
			{				
				return _initalTime;
			}
			set 
			{
				if (_initalTime == value)
                    return;
				_initalTime = value;
			}
		}

		/// <summary>
        /// 截止时间
        /// </summary>
		public DateTime ExpiredTime
		{
			get 
			{				
				return _expiredTime;
			}
			set 
			{
				if (_expiredTime == value)
                    return;
				_expiredTime = value;
			}
		}

		/// <summary>
        /// 发送时间
        /// </summary>
		public DateTime DeliveredTime
		{
			get 
			{				
				return _deliveredTime;
			}
			set 
			{
				if (_deliveredTime == value)
                    return;
				_deliveredTime = value;
			}
		}
        
		#endregion
        
	}
}