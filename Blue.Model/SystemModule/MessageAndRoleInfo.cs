//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: MessageAndRoleInfo.cs
// 描述: MessageAndRoleInfo 实体类
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
	/// <para>MessageAndRoleInfo 类</para>
	/// <para>消息与角色</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class MessageAndRoleInfo
	{
		#region 内部成员变量
        
		private decimal _roleId;
		private decimal _messageId;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public MessageAndRoleInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="roleId">角色编号</param>
		///<param name="messageId">消息编号</param>
		public MessageAndRoleInfo(decimal roleId, decimal messageId)
		{
			_roleId    = roleId;
			_messageId = messageId;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 角色编号
        /// </summary>
		public decimal RoleId
		{
			get 
			{				
				return _roleId;
			}
			set 
			{
				if (_roleId == value)
                    return;
				_roleId = value;
			}
		}

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
        
		#endregion
        
	}
}