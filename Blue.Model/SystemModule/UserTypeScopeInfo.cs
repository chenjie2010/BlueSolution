//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserTypeScopeInfo.cs
// 描述：UserTypeScopeInfo 实体类
// 作者：ChenJie 
// 编写日期：2016/8/28
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.SystemModule
{
	/// <summary>
	/// <para>UserTypeScopeInfo 类</para>
	/// <para>用户管理用户类型范围</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class UserTypeScopeInfo
	{
		#region 内部成员变量
        
		private decimal _userId;
		private decimal _userTypeId;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public UserTypeScopeInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="userId">用户编号</param>
		///<param name="userTypeId">用户类型编号</param>
		public UserTypeScopeInfo(decimal userId, decimal userTypeId)
		{
			_userId     = userId;
			_userTypeId = userTypeId;
			
		}
        
		#endregion
		
		#region 字段属性
		
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
        /// 用户类型编号
        /// </summary>
		public decimal UserTypeId
		{
			get 
			{				
				return _userTypeId;
			}
			set 
			{
				if (_userTypeId == value)
                    return;
				_userTypeId = value;
			}
		}
        
		#endregion
        
	}
}