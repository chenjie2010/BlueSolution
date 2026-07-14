//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingRelationInfo.cs
// 描述: DataAuditingRelationInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/10/15
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessDesignerModule
{
	/// <summary>
	/// <para>DataAuditingRelationInfo 类</para>
	/// <para>数据审核关系</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class DataAuditingRelationInfo
	{
		#region 内部成员变量
        
		private decimal _parentDataAuditingId;
		private decimal _dataAuditingId;
		private int _sorting;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public DataAuditingRelationInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="parentDataAuditingId"></param>
		///<param name="dataAuditingId"></param>
		///<param name="sorting">排序</param>
		public DataAuditingRelationInfo(decimal parentDataAuditingId, decimal dataAuditingId, int sorting)
		{
			_parentDataAuditingId = parentDataAuditingId;
			_dataAuditingId       = dataAuditingId;
			_sorting              = sorting;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 
        /// </summary>
		public decimal ParentDataAuditingId
		{
			get 
			{				
				return _parentDataAuditingId;
			}
			set 
			{
				if (_parentDataAuditingId == value)
                    return;
				_parentDataAuditingId = value;
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
        
		#endregion
        
	}
}