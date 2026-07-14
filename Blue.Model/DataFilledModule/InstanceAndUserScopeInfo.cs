//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：InstanceAndUserScopeInfo.cs
// 描述：InstanceAndUserScopeInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/3/17
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.DataFilledModule
{
	/// <summary>
	/// <para>InstanceAndUserScopeInfo 类</para>
	/// <para>填报与用户关系</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class InstanceAndUserScopeInfo
	{
		#region 内部成员变量
        
		private decimal _instanceId;
		private decimal _userId;
		private byte _userScopeProperty;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public InstanceAndUserScopeInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="instanceId">实例编号</param>
		///<param name="userId">用户编号</param>
		///<param name="userScopeProperty">用户范围属性</param>
		public InstanceAndUserScopeInfo(decimal instanceId, decimal userId, byte userScopeProperty)
		{
			_instanceId        = instanceId;
			_userId            = userId;
			_userScopeProperty = userScopeProperty;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 实例编号
        /// </summary>
		public decimal InstanceId
		{
			get 
			{				
				return _instanceId;
			}
			set 
			{
				if (_instanceId == value)
                    return;
				_instanceId = value;
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
        /// 用户范围属性
        /// </summary>
		public byte UserScopeProperty
		{
			get 
			{				
				return _userScopeProperty;
			}
			set 
			{
				if (_userScopeProperty == value)
                    return;
				_userScopeProperty = value;
			}
		}
        
		#endregion
        
	}
}