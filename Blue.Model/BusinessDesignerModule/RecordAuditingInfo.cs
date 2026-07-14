//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RecordAuditingInfo.cs
// 描述: RecordAuditingInfo 实体类
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
	/// <para>RecordAuditingInfo 类</para>
	/// <para>记录审核</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class RecordAuditingInfo
	{
		#region 内部成员变量
        
		private decimal _auditingId;
		private decimal _userId;
		private decimal _dataAuditingId;
		private decimal _parentUserId;
		private decimal _recordId;
		private byte _auditingAction;
		private DateTime _auditingTime;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public RecordAuditingInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="auditingId">记录审核编号</param>
		///<param name="userId">用户编号</param>
		///<param name="dataAuditingId"></param>
		///<param name="parentUserId">用户编号</param>
		///<param name="recordId">记录编号</param>
		///<param name="auditingAction">审核动作</param>
		///<param name="auditingTime">审核时间</param>
		public RecordAuditingInfo(decimal auditingId, decimal userId, decimal dataAuditingId, decimal parentUserId, decimal recordId, 
			byte auditingAction, DateTime auditingTime)
		{
			_auditingId     = auditingId;
			_userId         = userId;
			_dataAuditingId = dataAuditingId;
			_parentUserId   = parentUserId;
			_recordId       = recordId;
			_auditingAction = auditingAction;
			_auditingTime   = auditingTime;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 记录审核编号
        /// </summary>
		public decimal AuditingId
		{
			get 
			{				
				return _auditingId;
			}
			set 
			{
				if (_auditingId == value)
                    return;
				_auditingId = value;
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
        /// 
        /// </summary>
		public decimal DataAuditingId
		{
			get 
			{				
				return _dataAuditingId;
			}
			set 
			{
				if (_dataAuditingId == value)
                    return;
				_dataAuditingId = value;
			}
		}

		/// <summary>
        /// 用户编号
        /// </summary>
		public decimal ParentUserId
		{
			get 
			{				
				return _parentUserId;
			}
			set 
			{
				if (_parentUserId == value)
                    return;
				_parentUserId = value;
			}
		}

		/// <summary>
        /// 记录编号
        /// </summary>
		public decimal RecordId
		{
			get 
			{				
				return _recordId;
			}
			set 
			{
				if (_recordId == value)
                    return;
				_recordId = value;
			}
		}

		/// <summary>
        /// 审核动作
        /// </summary>
		public byte AuditingAction
		{
			get 
			{				
				return _auditingAction;
			}
			set 
			{
				if (_auditingAction == value)
                    return;
				_auditingAction = value;
			}
		}

		/// <summary>
        /// 审核时间
        /// </summary>
		public DateTime AuditingTime
		{
			get 
			{				
				return _auditingTime;
			}
			set 
			{
				if (_auditingTime == value)
                    return;
				_auditingTime = value;
			}
		}
        
		#endregion
        
	}
}