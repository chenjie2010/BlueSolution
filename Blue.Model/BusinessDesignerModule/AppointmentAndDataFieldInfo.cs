//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppointmentAndDataFieldInfo.cs
// 描述: AppointmentAndDataFieldInfo 实体类
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
	/// <para>AppointmentAndDataFieldInfo 类</para>
	/// <para>预约业务与字段</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class AppointmentAndDataFieldInfo
	{
		#region 内部成员变量
        
		private decimal _dataFieldId;
		private decimal _appointmentId;
		private byte _fstCondition;
		private byte _scdCondition;
		private bool _originalValueSkipped;
		private string _originalStringValue = string.Empty;
		private string _stringValue = string.Empty;
		private int _originalFstIntegerValue;
		private int _originalScdIntegerValue;
		private int _fstIntegerValue;
		private int _scdIntegerValue;
		private decimal _originalFstDecimalValue;
		private decimal _originalScdDecimalValue;
		private decimal _fstDecimalValue;
		private decimal _scdDecimalValue;
		private DateTime _originalFstTimeValue;
		private DateTime _originalScdTimeValue;
		private DateTime _fstTimeValue;
		private DateTime _scdTimeValue;
		private byte _nextRelation;
		private int _sorting;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public AppointmentAndDataFieldInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="dataFieldId">字段编号</param>
		///<param name="appointmentId">预约编号</param>
		///<param name="fstCondition">判断条件一</param>
		///<param name="scdCondition">判断条件二</param>
		///<param name="originalValueSkipped">忽略源值</param>
		///<param name="originalStringValue">源字符串条件值</param>
		///<param name="stringValue">新字符串条件值</param>
		///<param name="originalFstIntegerValue">源整型条件值一</param>
		///<param name="originalScdIntegerValue">源整型条件值二</param>
		///<param name="fstIntegerValue">新整型条件值一</param>
		///<param name="scdIntegerValue">新整型条件值二</param>
		///<param name="originalFstDecimalValue">源实数条件值一</param>
		///<param name="originalScdDecimalValue">源实数条件值二</param>
		///<param name="fstDecimalValue">新实数条件值一</param>
		///<param name="scdDecimalValue">新实数条件值二</param>
		///<param name="originalFstTimeValue">源时间条件值一</param>
		///<param name="originalScdTimeValue">源时间条件值二</param>
		///<param name="fstTimeValue">新时间条件值一</param>
		///<param name="scdTimeValue">新时间条件值二</param>
		///<param name="nextRelation">与下一条件关系</param>
		///<param name="sorting">排序</param>
		public AppointmentAndDataFieldInfo(decimal dataFieldId, decimal appointmentId, byte fstCondition, byte scdCondition, bool originalValueSkipped, 
			string originalStringValue, string stringValue, int originalFstIntegerValue, int originalScdIntegerValue, int fstIntegerValue, 
			int scdIntegerValue, decimal originalFstDecimalValue, decimal originalScdDecimalValue, decimal fstDecimalValue, decimal scdDecimalValue, 
			DateTime originalFstTimeValue, DateTime originalScdTimeValue, DateTime fstTimeValue, DateTime scdTimeValue, byte nextRelation, 
			int sorting)
		{
			_dataFieldId             = dataFieldId;
			_appointmentId           = appointmentId;
			_fstCondition            = fstCondition;
			_scdCondition            = scdCondition;
			_originalValueSkipped    = originalValueSkipped;
			_originalStringValue     = originalStringValue;
			_stringValue             = stringValue;
			_originalFstIntegerValue = originalFstIntegerValue;
			_originalScdIntegerValue = originalScdIntegerValue;
			_fstIntegerValue         = fstIntegerValue;
			_scdIntegerValue         = scdIntegerValue;
			_originalFstDecimalValue = originalFstDecimalValue;
			_originalScdDecimalValue = originalScdDecimalValue;
			_fstDecimalValue         = fstDecimalValue;
			_scdDecimalValue         = scdDecimalValue;
			_originalFstTimeValue    = originalFstTimeValue;
			_originalScdTimeValue    = originalScdTimeValue;
			_fstTimeValue            = fstTimeValue;
			_scdTimeValue            = scdTimeValue;
			_nextRelation            = nextRelation;
			_sorting                 = sorting;
			
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
        /// 预约编号
        /// </summary>
		public decimal AppointmentId
		{
			get 
			{				
				return _appointmentId;
			}
			set 
			{
				if (_appointmentId == value)
                    return;
				_appointmentId = value;
			}
		}

		/// <summary>
        /// 判断条件一
        /// </summary>
		public byte FstCondition
		{
			get 
			{				
				return _fstCondition;
			}
			set 
			{
				if (_fstCondition == value)
                    return;
				_fstCondition = value;
			}
		}

		/// <summary>
        /// 判断条件二
        /// </summary>
		public byte ScdCondition
		{
			get 
			{				
				return _scdCondition;
			}
			set 
			{
				if (_scdCondition == value)
                    return;
				_scdCondition = value;
			}
		}

		/// <summary>
        /// 忽略源值
        /// </summary>
		public bool OriginalValueSkipped
		{
			get 
			{				
				return _originalValueSkipped;
			}
			set 
			{
				if (_originalValueSkipped == value)
                    return;
				_originalValueSkipped = value;
			}
		}

		/// <summary>
        /// 源字符串条件值
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "源字符串条件值长度不能超过512位！")]       
		public string OriginalStringValue
		{
			get 
			{				
				return _originalStringValue;
			}
			set 
			{
				if (_originalStringValue == value)
                    return;
				_originalStringValue = value;
			}
		}

		/// <summary>
        /// 新字符串条件值
        /// </summary>
        [StringLengthValidator(0, 512, MessageTemplate = "新字符串条件值长度不能超过512位！")]       
		public string StringValue
		{
			get 
			{				
				return _stringValue;
			}
			set 
			{
				if (_stringValue == value)
                    return;
				_stringValue = value;
			}
		}

		/// <summary>
        /// 源整型条件值一
        /// </summary>
		public int OriginalFstIntegerValue
		{
			get 
			{				
				return _originalFstIntegerValue;
			}
			set 
			{
				if (_originalFstIntegerValue == value)
                    return;
				_originalFstIntegerValue = value;
			}
		}

		/// <summary>
        /// 源整型条件值二
        /// </summary>
		public int OriginalScdIntegerValue
		{
			get 
			{				
				return _originalScdIntegerValue;
			}
			set 
			{
				if (_originalScdIntegerValue == value)
                    return;
				_originalScdIntegerValue = value;
			}
		}

		/// <summary>
        /// 新整型条件值一
        /// </summary>
		public int FstIntegerValue
		{
			get 
			{				
				return _fstIntegerValue;
			}
			set 
			{
				if (_fstIntegerValue == value)
                    return;
				_fstIntegerValue = value;
			}
		}

		/// <summary>
        /// 新整型条件值二
        /// </summary>
		public int ScdIntegerValue
		{
			get 
			{				
				return _scdIntegerValue;
			}
			set 
			{
				if (_scdIntegerValue == value)
                    return;
				_scdIntegerValue = value;
			}
		}

		/// <summary>
        /// 源实数条件值一
        /// </summary>
		public decimal OriginalFstDecimalValue
		{
			get 
			{				
				return _originalFstDecimalValue;
			}
			set 
			{
				if (_originalFstDecimalValue == value)
                    return;
				_originalFstDecimalValue = value;
			}
		}

		/// <summary>
        /// 源实数条件值二
        /// </summary>
		public decimal OriginalScdDecimalValue
		{
			get 
			{				
				return _originalScdDecimalValue;
			}
			set 
			{
				if (_originalScdDecimalValue == value)
                    return;
				_originalScdDecimalValue = value;
			}
		}

		/// <summary>
        /// 新实数条件值一
        /// </summary>
		public decimal FstDecimalValue
		{
			get 
			{				
				return _fstDecimalValue;
			}
			set 
			{
				if (_fstDecimalValue == value)
                    return;
				_fstDecimalValue = value;
			}
		}

		/// <summary>
        /// 新实数条件值二
        /// </summary>
		public decimal ScdDecimalValue
		{
			get 
			{				
				return _scdDecimalValue;
			}
			set 
			{
				if (_scdDecimalValue == value)
                    return;
				_scdDecimalValue = value;
			}
		}

		/// <summary>
        /// 源时间条件值一
        /// </summary>
		public DateTime OriginalFstTimeValue
		{
			get 
			{				
				return _originalFstTimeValue;
			}
			set 
			{
				if (_originalFstTimeValue == value)
                    return;
				_originalFstTimeValue = value;
			}
		}

		/// <summary>
        /// 源时间条件值二
        /// </summary>
		public DateTime OriginalScdTimeValue
		{
			get 
			{				
				return _originalScdTimeValue;
			}
			set 
			{
				if (_originalScdTimeValue == value)
                    return;
				_originalScdTimeValue = value;
			}
		}

		/// <summary>
        /// 新时间条件值一
        /// </summary>
		public DateTime FstTimeValue
		{
			get 
			{				
				return _fstTimeValue;
			}
			set 
			{
				if (_fstTimeValue == value)
                    return;
				_fstTimeValue = value;
			}
		}

		/// <summary>
        /// 新时间条件值二
        /// </summary>
		public DateTime ScdTimeValue
		{
			get 
			{				
				return _scdTimeValue;
			}
			set 
			{
				if (_scdTimeValue == value)
                    return;
				_scdTimeValue = value;
			}
		}

		/// <summary>
        /// 与下一条件关系
        /// </summary>
		public byte NextRelation
		{
			get 
			{				
				return _nextRelation;
			}
			set 
			{
				if (_nextRelation == value)
                    return;
				_nextRelation = value;
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