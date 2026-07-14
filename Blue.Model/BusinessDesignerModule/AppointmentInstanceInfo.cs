//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppointmentInstanceInfo.cs
// 描述: AppointmentInstanceInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessDesignerModule
{
	/// <summary>
	/// <para>AppointmentInstanceInfo 类</para>
	/// <para>预约业务实例</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class AppointmentInstanceInfo
	{
		#region 内部成员变量
        
		private decimal _appInstanceId;
		private decimal _appointmentId;
		private decimal _userId;
		private decimal _instanceId;
		private decimal _parentInstanceId;
		private decimal _businessId;
		private DateTime _cretatedTime;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public AppointmentInstanceInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="appInstanceId">实例编号</param>
		///<param name="appointmentId">预约编号</param>
		///<param name="userId">用户编号</param>
		///<param name="instanceId">工作流实例编号</param>
		///<param name="parentInstanceId">实例编号</param>
		///<param name="businessId">业务编号</param>
		///<param name="cretatedTime">实例创建时间</param>
		public AppointmentInstanceInfo(decimal appInstanceId, decimal appointmentId, decimal userId, decimal instanceId, decimal parentInstanceId, 
			decimal businessId, DateTime cretatedTime)
		{
			_appInstanceId    = appInstanceId;
			_appointmentId    = appointmentId;
			_userId           = userId;
			_instanceId       = instanceId;
			_parentInstanceId = parentInstanceId;
			_businessId       = businessId;
			_cretatedTime     = cretatedTime;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 实例编号
        /// </summary>
		public decimal AppInstanceId
		{
			get 
			{				
				return _appInstanceId;
			}
			set 
			{
				if (_appInstanceId == value)
                    return;
				_appInstanceId = value;
			}
		}

		/// <summary>
        /// 预约编号
        /// </summary>
		public decimal AppointmentId
		{
			get 
			{				
				return _appointmentId;
			}
			set 
			{
				if (_appointmentId == value)
                    return;
				_appointmentId = value;
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
        /// 工作流实例编号
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
        /// 实例编号
        /// </summary>
		public decimal ParentInstanceId
		{
			get 
			{				
				return _parentInstanceId;
			}
			set 
			{
				if (_parentInstanceId == value)
                    return;
				_parentInstanceId = value;
			}
		}

		/// <summary>
        /// 业务编号
        /// </summary>
		public decimal BusinessId
		{
			get 
			{				
				return _businessId;
			}
			set 
			{
				if (_businessId == value)
                    return;
				_businessId = value;
			}
		}

		/// <summary>
        /// 实例创建时间
        /// </summary>
		public DateTime CretatedTime
		{
			get 
			{				
				return _cretatedTime;
			}
			set 
			{
				if (_cretatedTime == value)
                    return;
				_cretatedTime = value;
			}
		}
        
		#endregion
        
	}
}