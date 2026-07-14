//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomInterfaceAndDepInfo.cs
// 描述: CustomInterfaceAndDepInfo 实体类
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
	/// <para>CustomInterfaceAndDepInfo 类</para>
	/// <para>接口与用户类型</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class CustomInterfaceAndDepInfo
	{
		#region 内部成员变量
        
		private decimal _interfaceId;
		private decimal _depId;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public CustomInterfaceAndDepInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="interfaceId">接口编号</param>
		///<param name="depId">单位编号</param>
		public CustomInterfaceAndDepInfo(decimal interfaceId, decimal depId)
		{
			_interfaceId = interfaceId;
			_depId       = depId;
			
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
        /// 单位编号
        /// </summary>
		public decimal DepId
		{
			get 
			{				
				return _depId;
			}
			set 
			{
				if (_depId == value)
                    return;
				_depId = value;
			}
		}
        
		#endregion
        
	}
}