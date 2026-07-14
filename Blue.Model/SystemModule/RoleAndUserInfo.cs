//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：RoleAndUserInfo.cs
// 描述：RoleAndUserInfo 实体类
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
	/// <para>RoleAndUserInfo 类</para>
	/// <para>角色与用户</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class RoleAndUserInfo
	{
		#region 内部成员变量
        
		private decimal _userId;
		private decimal _roleId;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public RoleAndUserInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="userId">用户编号</param>
		///<param name="roleId">角色编号</param>
		public RoleAndUserInfo(decimal userId, decimal roleId)
		{
			_userId = userId;
			_roleId = roleId;
			
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
        
		#endregion
        
	}
}