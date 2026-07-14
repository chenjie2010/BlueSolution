//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CombinedDataFieldInfo.cs
// 描述: CombinedDataFieldInfo 实体类
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
	/// <para>CombinedDataFieldInfo 类</para>
	/// <para>组合字段</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class CombinedDataFieldInfo
	{
		#region 内部成员变量
        
		private decimal _combinedTableId;
		private decimal _dataFieldId;
		private int _sorting;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public CombinedDataFieldInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="combinedTableId"></param>
		///<param name="dataFieldId">字段编号</param>
		///<param name="sorting">排序</param>
		public CombinedDataFieldInfo(decimal combinedTableId, decimal dataFieldId, int sorting)
		{
			_combinedTableId = combinedTableId;
			_dataFieldId     = dataFieldId;
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
        /// 字段编号
        /// </summary>
		public decimal DataFieldId
		{
			get 
			{				
				return _dataFieldId;
			}
			set 
			{
				if (_dataFieldId == value)
                    return;
				_dataFieldId = value;
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