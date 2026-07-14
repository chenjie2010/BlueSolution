//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppointmentBusinessInfo.cs
// 描述: AppointmentBusinessInfo 实体类
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
	/// <para>AppointmentBusinessInfo 类</para>
	/// <para>预约业务管理</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class AppointmentBusinessInfo
	{
		#region 内部成员变量
        
		private decimal _appointmentId;
		private decimal _workflowId;
		private decimal _groupId;
		private decimal _dataId;
		private decimal _tableId;
		private decimal _parentWorkflowId;
		private decimal _parentDataId;
		private string _appointmentName = string.Empty;
		private string _appointmentCode = string.Empty;
		private byte _associatedBussinessType;
		private byte _appointmentType;
		private byte _periodType;
		private int _periodTime;
		private byte _appointmentBussinesType;
		private bool _appointmentEnabled;
		private int _sorting;
		private string _notes = string.Empty;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public AppointmentBusinessInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="appointmentId">预约编号</param>
		///<param name="workflowId">工作流编号</param>
		///<param name="groupId">分组编号</param>
		///<param name="dataId">数据填报编号</param>
		///<param name="tableId">表编号</param>
		///<param name="parentWorkflowId">工作流编号</param>
		///<param name="parentDataId">数据填报编号</param>
		///<param name="appointmentName">预约名称</param>
		///<param name="appointmentCode">预约编码</param>
		///<param name="associatedBussinessType">关联类型：数据表，填报，工作流</param>
		///<param name="appointmentType">预约类型：实时类型，定时类型</param>
		///<param name="periodType"></param>
		///<param name="periodTime">时长段：单位：分钟</param>
		///<param name="appointmentBussinesType">业务类型：填报，工作流</param>
		///<param name="appointmentEnabled">启用预约</param>
		///<param name="sorting">排序</param>
		///<param name="notes">备注</param>
		public AppointmentBusinessInfo(decimal appointmentId, decimal workflowId, decimal groupId, decimal dataId, decimal tableId, 
			decimal parentWorkflowId, decimal parentDataId, string appointmentName, string appointmentCode, byte associatedBussinessType, 
			byte appointmentType, byte periodType, int periodTime, byte appointmentBussinesType, bool appointmentEnabled, 
			int sorting, string notes)
		{
			_appointmentId           = appointmentId;
			_workflowId              = workflowId;
			_groupId                 = groupId;
			_dataId                  = dataId;
			_tableId                 = tableId;
			_parentWorkflowId        = parentWorkflowId;
			_parentDataId            = parentDataId;
			_appointmentName         = appointmentName;
			_appointmentCode         = appointmentCode;
			_associatedBussinessType = associatedBussinessType;
			_appointmentType         = appointmentType;
			_periodType              = periodType;
			_periodTime              = periodTime;
			_appointmentBussinesType = appointmentBussinesType;
			_appointmentEnabled      = appointmentEnabled;
			_sorting                 = sorting;
			_notes                   = notes;
			
		}
        
		#endregion
		
		#region 字段属性
		
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
        /// 工作流编号
        /// </summary>
		public decimal WorkflowId
		{
			get 
			{				
				return _workflowId;
			}
			set 
			{
				if (_workflowId == value)
                    return;
				_workflowId = value;
			}
		}

		/// <summary>
        /// 分组编号
        /// </summary>
		public decimal GroupId
		{
			get 
			{				
				return _groupId;
			}
			set 
			{
				if (_groupId == value)
                    return;
				_groupId = value;
			}
		}

		/// <summary>
        /// 数据填报编号
        /// </summary>
		public decimal DataId
		{
			get 
			{				
				return _dataId;
			}
			set 
			{
				if (_dataId == value)
                    return;
				_dataId = value;
			}
		}

		/// <summary>
        /// 表编号
        /// </summary>
		public decimal TableId
		{
			get 
			{				
				return _tableId;
			}
			set 
			{
				if (_tableId == value)
                    return;
				_tableId = value;
			}
		}

		/// <summary>
        /// 工作流编号
        /// </summary>
		public decimal ParentWorkflowId
		{
			get 
			{				
				return _parentWorkflowId;
			}
			set 
			{
				if (_parentWorkflowId == value)
                    return;
				_parentWorkflowId = value;
			}
		}

		/// <summary>
        /// 数据填报编号
        /// </summary>
		public decimal ParentDataId
		{
			get 
			{				
				return _parentDataId;
			}
			set 
			{
				if (_parentDataId == value)
                    return;
				_parentDataId = value;
			}
		}

		/// <summary>
        /// 预约名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 预约名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "预约名称长度范围在1位～64位！")]
		public string AppointmentName
		{
			get 
			{				
				return _appointmentName;
			}
			set 
			{
				if (_appointmentName == value)
                    return;
				_appointmentName = value;
			}
		}

		/// <summary>
        /// 预约编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 预约编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "预约编码长度范围在1位～32位！")]
		public string AppointmentCode
		{
			get 
			{				
				return _appointmentCode;
			}
			set 
			{
				if (_appointmentCode == value)
                    return;
				_appointmentCode = value;
			}
		}

		/// <summary>
        /// 关联类型：数据表，填报，工作流
        /// </summary>
		public byte AssociatedBussinessType
		{
			get 
			{				
				return _associatedBussinessType;
			}
			set 
			{
				if (_associatedBussinessType == value)
                    return;
				_associatedBussinessType = value;
			}
		}

		/// <summary>
        /// 预约类型：实时类型，定时类型
        /// </summary>
		public byte AppointmentType
		{
			get 
			{				
				return _appointmentType;
			}
			set 
			{
				if (_appointmentType == value)
                    return;
				_appointmentType = value;
			}
		}

		/// <summary>
        /// 
        /// </summary>
		public byte PeriodType
		{
			get 
			{				
				return _periodType;
			}
			set 
			{
				if (_periodType == value)
                    return;
				_periodType = value;
			}
		}

		/// <summary>
        /// 时长段：单位：分钟
        /// </summary>
		public int PeriodTime
		{
			get 
			{				
				return _periodTime;
			}
			set 
			{
				if (_periodTime == value)
                    return;
				_periodTime = value;
			}
		}

		/// <summary>
        /// 业务类型：填报，工作流
        /// </summary>
		public byte AppointmentBussinesType
		{
			get 
			{				
				return _appointmentBussinesType;
			}
			set 
			{
				if (_appointmentBussinesType == value)
                    return;
				_appointmentBussinesType = value;
			}
		}

		/// <summary>
        /// 启用预约
        /// </summary>
		public bool AppointmentEnabled
		{
			get 
			{				
				return _appointmentEnabled;
			}
			set 
			{
				if (_appointmentEnabled == value)
                    return;
				_appointmentEnabled = value;
			}
		}

		/// <summary>
        /// 排序
        /// </summary>
		public int Sorting
		{
			get 
			{				
				return _sorting;
			}
			set 
			{
				if (_sorting == value)
                    return;
				_sorting = value;
			}
		}

		/// <summary>
        /// 备注
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "备注长度不能超过256位！")]       
		public string Notes
		{
			get 
			{				
				return _notes;
			}
			set 
			{
				if (_notes == value)
                    return;
				_notes = value;
			}
		}
        
		#endregion
        
	}
}