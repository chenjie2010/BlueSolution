//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserConfigInfo.cs
// 描述: UserConfigInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/6/26
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.UserModule
{
	/// <summary>
	/// <para>UserConfigInfo 类</para>
	/// <para>用户配置信息</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class UserConfigInfo
	{
		#region 内部成员变量
        
		private decimal _userId;
		private int _userConfigName;
		private string _userConfigValue = string.Empty;
		private DateTime _updatedTime;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public UserConfigInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="userId">用户编号</param>
		///<param name="userConfigName">用户配置名称</param>
		///<param name="userConfigValue">用户配置值</param>
		///<param name="updatedTime">更新时间</param>
		public UserConfigInfo(decimal userId, int name, string userConfigValue, DateTime updatedTime)
		{
			_userId          = userId;
			_userConfigName            = name;
			_userConfigValue = userConfigValue;
			_updatedTime     = updatedTime;
			
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
        /// 用户配置名称
        /// </summary>
		public int UserConfigName
		{
			get 
			{				
				return _userConfigName;
			}
			set 
			{
				if (_userConfigName == value)
                    return;
				_userConfigName = value;
			}
		}

		/// <summary>
        /// 用户配置值
        /// </summary>
        [NotNullValidator(MessageTemplate = " 用户配置值不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "用户配置值长度范围在1位～32位！")]
		public string UserConfigValue
		{
			get 
			{				
				return _userConfigValue;
			}
			set 
			{
				if (_userConfigValue == value)
                    return;
				_userConfigValue = value;
			}
		}

		/// <summary>
        /// 更新时间
        /// </summary>
		public DateTime UpdatedTime
		{
			get 
			{				
				return _updatedTime;
			}
			set 
			{
				if (_updatedTime == value)
                    return;
				_updatedTime = value;
			}
		}
        
		#endregion
        
	}
}