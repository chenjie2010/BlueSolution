//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CombinedTableInfo.cs
// 描述: CombinedTableInfo 实体类
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
	/// <para>CombinedTableInfo 类</para>
	/// <para>组合表</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class CombinedTableInfo
	{
		#region 内部成员变量
        
		private decimal _combinedTableId;
		private decimal _groupId;
		private string _combinedTableName = string.Empty;
		private string _combinedTableCode = string.Empty;
		private byte _dataWarehouseId;
		private bool _isLeaf;
		private string _toolTip = string.Empty;
		private int _sorting;
		private string _notes = string.Empty;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public CombinedTableInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="combinedTableId"></param>
		///<param name="groupId">分组编号</param>
		///<param name="combinedTableName"></param>
		///<param name="combinedTableCode"></param>
		///<param name="dataWarehouseId"></param>
		///<param name="isLeaf"></param>
		///<param name="toolTip"></param>
		///<param name="sorting"></param>
		///<param name="notes"></param>
		public CombinedTableInfo(decimal combinedTableId, decimal groupId, string name, string combinedTableCode, byte dataWarehouseId, 
			bool isLeaf, string toolTip, int sorting, string notes)
		{
			_combinedTableId   = combinedTableId;
			_groupId           = groupId;
			_combinedTableName              = name;
			_combinedTableCode = combinedTableCode;
			_dataWarehouseId   = dataWarehouseId;
			_isLeaf            = isLeaf;
			_toolTip           = toolTip;
			_sorting           = sorting;
			_notes             = notes;
			
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
        /// 
        /// </summary>
        [NotNullValidator(MessageTemplate = " 不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "长度范围在1位～64位！")]
		public string CombinedTableName
		{
			get 
			{				
				return _combinedTableName;
			}
			set 
			{
				if (_combinedTableName == value)
                    return;
				_combinedTableName = value;
			}
		}

		/// <summary>
        /// 
        /// </summary>
        [NotNullValidator(MessageTemplate = " 不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "长度范围在1位～32位！")]
		public string CombinedTableCode
		{
			get 
			{				
				return _combinedTableCode;
			}
			set 
			{
				if (_combinedTableCode == value)
                    return;
				_combinedTableCode = value;
			}
		}

		/// <summary>
        /// 
        /// </summary>
		public byte DataWarehouseId
		{
			get 
			{				
				return _dataWarehouseId;
			}
			set 
			{
				if (_dataWarehouseId == value)
                    return;
				_dataWarehouseId = value;
			}
		}

		/// <summary>
        /// 
        /// </summary>
		public bool IsLeaf
		{
			get 
			{				
				return _isLeaf;
			}
			set 
			{
				if (_isLeaf == value)
                    return;
				_isLeaf = value;
			}
		}

		/// <summary>
        /// 
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "长度不能超过256位！")]       
		public string ToolTip
		{
			get 
			{				
				return _toolTip;
			}
			set 
			{
				if (_toolTip == value)
                    return;
				_toolTip = value;
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

		/// <summary>
        /// 
        /// </summary>
        [StringLengthValidator(0, 256, MessageTemplate = "长度不能超过256位！")]       
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