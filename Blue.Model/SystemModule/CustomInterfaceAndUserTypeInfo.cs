//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomInterfaceAndUserTypeInfo.cs
// 描述: CustomInterfaceAndUserTypeInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.SystemModule
{
	/// <summary>
	/// <para>CustomInterfaceAndUserTypeInfo 类</para>
	/// <para>接口与用户类型</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class CustomInterfaceAndUserTypeInfo
	{
		#region 内部成员变量
        
		private decimal _interfaceId;
		private decimal _userTypeId;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public CustomInterfaceAndUserTypeInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="interfaceId">接口编号</param>
		///<param name="userTypeId">用户类型编号</param>
		public CustomInterfaceAndUserTypeInfo(decimal interfaceId, decimal userTypeId)
		{
			_interfaceId = interfaceId;
			_userTypeId  = userTypeId;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 接口编号
        /// </summary>
		public decimal InterfaceId
		{
			get 
			{				
				return _interfaceId;
			}
			set 
			{
				if (_interfaceId == value)
                    return;
				_interfaceId = value;
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