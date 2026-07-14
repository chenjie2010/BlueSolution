//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSectionInfo.cs
// 描述: CustomSectionInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/8/13
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.BusinessModule
{
	/// <summary>
	/// <para>CustomSectionInfo 类</para>
	/// <para>窗体分组</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class CustomSectionInfo
	{
		#region 内部成员变量
        
		private decimal _sectionId;
		private decimal _dataId;
		private string _sectionName = string.Empty;
		private string _sectionCode = string.Empty;
		private bool _isLeaf;
		private int _sorting;
		private string _notes = string.Empty;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public CustomSectionInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="sectionId">窗体分组编号</param>
		///<param name="dataId">数据填报编号</param>
		///<param name="sectionName">窗体分组名称</param>
		///<param name="sectionCode">窗体分组编码</param>
		///<param name="isLeaf">叶子节点</param>
		///<param name="sorting">排序</param>
		///<param name="notes">备注</param>
		public CustomSectionInfo(decimal sectionId, decimal dataId, string sectionName, string sectionCode, bool isLeaf, 
			int sorting, string notes)
		{
			_sectionId   = sectionId;
			_dataId      = dataId;
			_sectionName = sectionName;
			_sectionCode = sectionCode;
			_isLeaf      = isLeaf;
			_sorting     = sorting;
			_notes       = notes;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 窗体分组编号
        /// </summary>
		public decimal SectionId
		{
			get 
			{				
				return _sectionId;
			}
			set 
			{
				if (_sectionId == value)
                    return;
				_sectionId = value;
			}
		}

		/// <summary>
        /// 数据填报编号
        /// </summary>
		public decimal DataId
		{
			get 
			{				
				return _dataId;
			}
			set 
			{
				if (_dataId == value)
                    return;
				_dataId = value;
			}
		}

		/// <summary>
        /// 窗体分组名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 窗体分组名称不能为空")]
        [StringLengthValidator(1, 64, MessageTemplate = "窗体分组名称长度范围在1位～64位！")]
		public string SectionName
		{
			get 
			{				
				return _sectionName;
			}
			set 
			{
				if (_sectionName == value)
                    return;
				_sectionName = value;
			}
		}

		/// <summary>
        /// 窗体分组编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 窗体分组编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "窗体分组编码长度范围在1位～32位！")]
		public string SectionCode
		{
			get 
			{				
				return _sectionCode;
			}
			set 
			{
				if (_sectionCode == value)
                    return;
				_sectionCode = value;
			}
		}

		/// <summary>
        /// 叶子节点
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
        [StringLengthValidator(0, 256, MessageTemplate = "备注长度不能超过256位！")]       
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