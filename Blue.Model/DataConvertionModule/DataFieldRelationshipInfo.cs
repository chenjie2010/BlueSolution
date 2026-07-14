//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataFieldRelationshipInfo.cs
// 描述: DataFieldRelationshipInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.DataConvertionModule
{
	/// <summary>
	/// <para>DataFieldRelationshipInfo 类</para>
	/// <para>字段联动关系</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class DataFieldRelationshipInfo
	{
		#region 内部成员变量
        
		private decimal _parentDataFieldId;
		private decimal _dataFieldId;
		private int _sorting;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public DataFieldRelationshipInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="parentDataFieldId">字段编号</param>
		///<param name="dataFieldId">字段编号</param>
		///<param name="sorting">排序</param>
		public DataFieldRelationshipInfo(decimal parentDataFieldId, decimal dataFieldId, int sorting)
		{
			_parentDataFieldId = parentDataFieldId;
			_dataFieldId       = dataFieldId;
			_sorting           = sorting;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 字段编号
        /// </summary>
		public decimal ParentDataFieldId
		{
			get 
			{				
				return _parentDataFieldId;
			}
			set 
			{
				if (_parentDataFieldId == value)
                    return;
				_parentDataFieldId = value;
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