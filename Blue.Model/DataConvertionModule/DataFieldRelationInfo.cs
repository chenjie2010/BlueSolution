//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataFieldRelationInfo.cs
// 描述: DataFieldRelationInfo 实体类
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
	/// <para>DataFieldRelationInfo 类</para>
	/// <para>数据关系与字段</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class DataFieldRelationInfo
	{
		#region 内部成员变量
        
		private decimal _dataFieldId;
		private decimal _parentDataFieldId;
		private decimal _relationId;
		private DateTime _relationTime;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public DataFieldRelationInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="dataFieldId">字段编号</param>
		///<param name="parentDataFieldId">字段编号</param>
		///<param name="relationId">关系编号</param>
		///<param name="relationTime"></param>
		public DataFieldRelationInfo(decimal dataFieldId, decimal parentDataFieldId, decimal relationId, DateTime relationTime)
		{
			_dataFieldId       = dataFieldId;
			_parentDataFieldId = parentDataFieldId;
			_relationId        = relationId;
			_relationTime      = relationTime;
			
		}
        
		#endregion
		
		#region 字段属性
		
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
        /// 关系编号
        /// </summary>
		public decimal RelationId
		{
			get 
			{				
				return _relationId;
			}
			set 
			{
				if (_relationId == value)
                    return;
				_relationId = value;
			}
		}

		/// <summary>
        /// 
        /// </summary>
		public DateTime RelationTime
		{
			get 
			{				
				return _relationTime;
			}
			set 
			{
				if (_relationTime == value)
                    return;
				_relationTime = value;
			}
		}
        
		#endregion
        
	}
}