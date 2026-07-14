//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CombinedTableRelationInfo.cs
// 描述: CombinedTableRelationInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/15
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
	/// <summary>
	/// <para>CombinedTableRelationInfo 类</para>
	/// <para>组合表关系</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class CombinedTableRelationInfo
	{
		#region 内部成员变量
        
		private decimal _combinedTableId;
		private decimal _tableId;
		private int _sorting;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public CombinedTableRelationInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="combinedTableId"></param>
		///<param name="tableId">表编号</param>
		///<param name="sorting">排序</param>
		public CombinedTableRelationInfo(decimal combinedTableId, decimal tableId, int sorting)
		{
			_combinedTableId = combinedTableId;
			_tableId         = tableId;
			_sorting         = sorting;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 
        /// </summary>
		public decimal CombinedTableId
		{
			get 
			{				
				return _combinedTableId;
			}
			set 
			{
				if (_combinedTableId == value)
                    return;
				_combinedTableId = value;
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