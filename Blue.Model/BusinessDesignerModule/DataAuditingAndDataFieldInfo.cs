//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingAndDataFieldInfo.cs
// 描述: DataAuditingAndDataFieldInfo 实体类
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
	/// <para>DataAuditingAndDataFieldInfo 类</para>
	/// <para>数据审核查询字段</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class DataAuditingAndDataFieldInfo
	{
		#region 内部成员变量
        
		private decimal _dataAuditingId;
		private decimal _dataFieldId;
		private int _sorting;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public DataAuditingAndDataFieldInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="dataAuditingId"></param>
		///<param name="dataFieldId">字段编号</param>
		///<param name="sorting"></param>
		public DataAuditingAndDataFieldInfo(decimal dataAuditingId, decimal dataFieldId, int sorting)
		{
			_dataAuditingId = dataAuditingId;
			_dataFieldId    = dataFieldId;
			_sorting        = sorting;
			
		}
        
		#endregion
		
		#region 字段属性
		
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