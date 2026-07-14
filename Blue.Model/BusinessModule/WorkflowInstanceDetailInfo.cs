//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: WorkflowInstanceDetailInfo.cs
// 描述: WorkflowInstanceDetailInfo 实体类
// 作者：ChenJie 
// 编写日期：2019/6/23
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
	/// <summary>
	/// <para>WorkflowInstanceDetailInfo 类</para>
	/// <para>工作流实例简化流程</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class WorkflowInstanceDetailInfo
	{
		#region 内部成员变量
        
		private decimal _detailId;
		private decimal _instanceId;
		private byte _reviewedAction;
		private bool _actionVisible;
		private DateTime _timeReviewed;
		private string _commentReviewed = string.Empty;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public WorkflowInstanceDetailInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="detailId">流程编号</param>
		///<param name="instanceId">工作流实例编号</param>
		///<param name="reviewedAction">审核动作</param>
		///<param name="actionVisible">可见性</param>
		///<param name="timeReviewed">审核时间</param>
		///<param name="commentReviewed">审核意见</param>
		public WorkflowInstanceDetailInfo(decimal detailId, decimal instanceId, byte reviewedAction, bool actionVisible, DateTime timeReviewed, 
			string commentReviewed)
		{
			_detailId        = detailId;
			_instanceId      = instanceId;
			_reviewedAction  = reviewedAction;
			_actionVisible   = actionVisible;
			_timeReviewed    = timeReviewed;
			_commentReviewed = commentReviewed;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 流程编号
        /// </summary>
		public decimal DetailId
		{
			get 
			{				
				return _detailId;
			}
			set 
			{
				if (_detailId == value)
                    return;
				_detailId = value;
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
        /// 审核动作
        /// </summary>
		public byte ReviewedAction
		{
			get 
			{				
				return _reviewedAction;
			}
			set 
			{
				if (_reviewedAction == value)
                    return;
				_reviewedAction = value;
			}
		}

		/// <summary>
        /// 可见性
        /// </summary>
		public bool ActionVisible
		{
			get 
			{				
				return _actionVisible;
			}
			set 
			{
				if (_actionVisible == value)
                    return;
				_actionVisible = value;
			}
		}

		/// <summary>
        /// 审核时间
        /// </summary>
		public DateTime TimeReviewed
		{
			get 
			{				
				return _timeReviewed;
			}
			set 
			{
				if (_timeReviewed == value)
                    return;
				_timeReviewed = value;
			}
		}

		/// <summary>
        /// 审核意见
        /// </summary>
        [NotNullValidator(MessageTemplate = " 审核意见不能为空")]
        [StringLengthValidator(1, 512, MessageTemplate = "审核意见长度范围在1位～512位！")]
		public string CommentReviewed
		{
			get 
			{				
				return _commentReviewed;
			}
			set 
			{
				if (_commentReviewed == value)
                    return;
				_commentReviewed = value;
			}
		}
        
		#endregion
        
	}
}