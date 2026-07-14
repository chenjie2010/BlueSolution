//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CellStyleInfo.cs
// 描述: CellStyleInfo 实体类
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
	/// <para>CellStyleInfo 类</para>
	/// <para>单元格与字段</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class CellStyleInfo
	{
		#region 内部成员变量
        
		private decimal _styleId;
		private decimal _dataFieldId;
		private decimal _cellId;
		private byte _styleType;
		private long _styleProperty;
		private byte _systemDataFieldId;
		private int _sorting;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public CellStyleInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="styleId">样式编号</param>
		///<param name="dataFieldId">字段编号</param>
		///<param name="cellId">CellId</param>
		///<param name="styleType">样式类型</param>
		///<param name="styleProperty">样式属性</param>
		///<param name="systemDataFieldId">系统字段</param>
		///<param name="sorting">排序</param>
		public CellStyleInfo(decimal styleId, decimal dataFieldId, decimal cellId, byte styleType, long styleProperty, 
			byte systemDataFieldId, int sorting)
		{
			_styleId           = styleId;
			_dataFieldId       = dataFieldId;
			_cellId            = cellId;
			_styleType         = styleType;
			_styleProperty     = styleProperty;
			_systemDataFieldId = systemDataFieldId;
			_sorting           = sorting;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 样式编号
        /// </summary>
		public decimal StyleId
		{
			get 
			{				
				return _styleId;
			}
			set 
			{
				if (_styleId == value)
                    return;
				_styleId = value;
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
        /// CellId
        /// </summary>
		public decimal CellId
		{
			get 
			{				
				return _cellId;
			}
			set 
			{
				if (_cellId == value)
                    return;
				_cellId = value;
			}
		}

		/// <summary>
        /// 样式类型
        /// </summary>
		public byte StyleType
		{
			get 
			{				
				return _styleType;
			}
			set 
			{
				if (_styleType == value)
                    return;
				_styleType = value;
			}
		}

		/// <summary>
        /// 样式属性
        /// </summary>
		public long StyleProperty
		{
			get 
			{				
				return _styleProperty;
			}
			set 
			{
				if (_styleProperty == value)
                    return;
				_styleProperty = value;
			}
		}

		/// <summary>
        /// 系统字段
        /// </summary>
		public byte SystemDataFieldId
		{
			get 
			{				
				return _systemDataFieldId;
			}
			set 
			{
				if (_systemDataFieldId == value)
                    return;
				_systemDataFieldId = value;
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