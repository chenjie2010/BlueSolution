//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：DataFieldRelationInfo.cs
// 描述：DataFieldRelationInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/4/8
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using AppFramework.Core;

namespace Blue.Model.BusinessModule
{
	/// <summary>
	/// <para>DataFieldRelationInfo 类</para>
	/// <para>自定义字段关系</para>
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
		///<param name="sorting"></param>
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
        /// 
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