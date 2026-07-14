//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: WorkflowInstanceLogAndStepInfo.cs
// 描述: WorkflowInstanceLogAndStepInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/27
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
	/// <summary>
	/// <para>WorkflowInstanceLogAndStepInfo 类</para>
	/// <para>工作流实例日志与流程</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class WorkflowInstanceLogAndStepInfo
	{
		#region 内部成员变量
        
		private decimal _stepId;
		private decimal _logId;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public WorkflowInstanceLogAndStepInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="stepId">步骤编号</param>
		///<param name="logId">日志编号</param>
		public WorkflowInstanceLogAndStepInfo(decimal stepId, decimal logId)
		{
			_stepId = stepId;
			_logId  = logId;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 步骤编号
        /// </summary>
		public decimal StepId
		{
			get 
			{				
				return _stepId;
			}
			set 
			{
				if (_stepId == value)
                    return;
				_stepId = value;
			}
		}

		/// <summary>
        /// 日志编号
        /// </summary>
		public decimal LogId
		{
			get 
			{				
				return _logId;
			}
			set 
			{
				if (_logId == value)
                    return;
				_logId = value;
			}
		}
        
		#endregion
        
	}
}