//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomWorkflowProcessAndRoleInfo.cs
// 描述：CustomWorkflowProcessAndRoleInfo 实体类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
	/// <summary>
	/// <para>CustomWorkflowProcessAndRoleInfo 类</para>
	/// <para>工作流流程与角色</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class CustomWorkflowProcessAndRoleInfo
	{
		#region 内部成员变量
        
		private decimal _processId;
		private decimal _roleId;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public CustomWorkflowProcessAndRoleInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="processId">流程编号</param>
		///<param name="roleId">角色编号</param>
		public CustomWorkflowProcessAndRoleInfo(decimal processId, decimal roleId)
		{
			_processId = processId;
			_roleId    = roleId;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 流程编号
        /// </summary>
		public decimal ProcessId
		{
			get 
			{				
				return _processId;
			}
			set 
			{
				if (_processId == value)
                    return;
				_processId = value;
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