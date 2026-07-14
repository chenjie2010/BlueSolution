//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataRelationInfo.cs
// 描述: DataRelationInfo 实体类
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
	/// <para>DataRelationInfo 类</para>
	/// <para>数据关系</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class DataRelationInfo
	{
		#region 内部成员变量
        
		private decimal _relationId;
		private decimal _groupId;
		private decimal _databaseId;
		private decimal _parentDatabaseId;
		private string _relationName = string.Empty;
		private string _relationCode = string.Empty;
		private byte _dataRelationType;
		private long _dataRelationProperty;
		private int _sorting;
		private string _notes = string.Empty;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public DataRelationInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="relationId">关系编号</param>
		///<param name="groupId">分组编号</param>
		///<param name="databaseId">数据库编号</param>
		///<param name="parentDatabaseId">数据库编号</param>
		///<param name="relationName">关系名称</param>
		///<param name="relationCode">关系编码</param>
		///<param name="dataRelationType">关系类型</param>
		///<param name="dataRelationProperty">关系属性</param>
		///<param name="sorting">排序</param>
		///<param name="notes">备注</param>
		public DataRelationInfo(decimal relationId, decimal groupId, decimal databaseId, decimal parentDatabaseId, string relationName, 
			string relationCode, byte dataRelationType, long dataRelationProperty, int sorting, string notes)
		{
			_relationId           = relationId;
			_groupId              = groupId;
			_databaseId           = databaseId;
			_parentDatabaseId     = parentDatabaseId;
			_relationName         = relationName;
			_relationCode         = relationCode;
			_dataRelationType     = dataRelationType;
			_dataRelationProperty = dataRelationProperty;
			_sorting              = sorting;
			_notes                = notes;
			
		}
        
		#endregion
		
		#region 字段属性
		
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
        /// 分组编号
        /// </summary>
		public decimal GroupId
		{
			get 
			{				
				return _groupId;
			}
			set 
			{
				if (_groupId == value)
                    return;
				_groupId = value;
			}
		}

		/// <summary>
        /// 数据库编号
        /// </summary>
		public decimal DatabaseId
		{
			get 
			{				
				return _databaseId;
			}
			set 
			{
				if (_databaseId == value)
                    return;
				_databaseId = value;
			}
		}

		/// <summary>
        /// 数据库编号
        /// </summary>
		public decimal ParentDatabaseId
		{
			get 
			{				
				return _parentDatabaseId;
			}
			set 
			{
				if (_parentDatabaseId == value)
                    return;
				_parentDatabaseId = value;
			}
		}

		/// <summary>
        /// 关系名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 关系名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "关系名称长度范围在1位～64位！")]
		public string RelationName
		{
			get 
			{				
				return _relationName;
			}
			set 
			{
				if (_relationName == value)
                    return;
				_relationName = value;
			}
		}

		/// <summary>
        /// 关系编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 关系编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "关系编码长度范围在1位～32位！")]
		public string RelationCode
		{
			get 
			{				
				return _relationCode;
			}
			set 
			{
				if (_relationCode == value)
                    return;
				_relationCode = value;
			}
		}

		/// <summary>
        /// 关系类型
        /// </summary>
		public byte DataRelationType
		{
			get 
			{				
				return _dataRelationType;
			}
			set 
			{
				if (_dataRelationType == value)
                    return;
				_dataRelationType = value;
			}
		}

		/// <summary>
        /// 关系属性
        /// </summary>
		public long DataRelationProperty
		{
			get 
			{				
				return _dataRelationProperty;
			}
			set 
			{
				if (_dataRelationProperty == value)
                    return;
				_dataRelationProperty = value;
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

		/// <summary>
        /// 备注
        /// </summary>
        [StringLengthValidator(0, 255, MessageTemplate = "备注长度不能超过255位！")]       
		public string Notes
		{
			get 
			{				
				return _notes;
			}
			set 
			{
				if (_notes == value)
                    return;
				_notes = value;
			}
		}
        
		#endregion
        
	}
}