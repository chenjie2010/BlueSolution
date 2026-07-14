//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSheetInfo.cs
// 描述: CustomSheetInfo 实体类
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
	/// <para>CustomSheetInfo 类</para>
	/// <para>样表</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class CustomSheetInfo
	{
		#region 内部成员变量
        
		private decimal _sheetId;
		private decimal _reportId;
		private string _sheetName = string.Empty;
		private string _sheetCode = string.Empty;
		private string _sheetDescription = string.Empty;
		private int _sheetRowCount;
		private int _sheetColCount;
		private int _marginTop;
		private int _marginBottom;
		private int _marginLeft;
		private int _marginRight;
		private int _approvalNumber;
		private int _sorting;
		private string _notes = string.Empty;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public CustomSheetInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="sheetId">样表编号</param>
		///<param name="reportId">报表编号</param>
		///<param name="sheetName">样表名称</param>
		///<param name="sheetCode">样表编码</param>
		///<param name="sheetDescription">样表描述</param>
		///<param name="sheetRowCount">行数</param>
		///<param name="sheetColCount">列数</param>
		///<param name="marginTop">上边距</param>
		///<param name="marginBottom">下边距</param>
		///<param name="marginLeft">左边距</param>
		///<param name="marginRight">右边距</param>
		///<param name="approvalNumber">批文编号</param>
		///<param name="sorting">排序</param>
		///<param name="notes">备注</param>
		public CustomSheetInfo(decimal sheetId, decimal reportId, string sheetName, string sheetCode, string sheetDescription, 
			int sheetRowCount, int sheetColCount, int marginTop, int marginBottom, int marginLeft, 
			int marginRight, int approvalNumber, int sorting, string notes)
		{
			_sheetId          = sheetId;
			_reportId         = reportId;
			_sheetName        = sheetName;
			_sheetCode        = sheetCode;
			_sheetDescription = sheetDescription;
			_sheetRowCount    = sheetRowCount;
			_sheetColCount    = sheetColCount;
			_marginTop        = marginTop;
			_marginBottom     = marginBottom;
			_marginLeft       = marginLeft;
			_marginRight      = marginRight;
			_approvalNumber   = approvalNumber;
			_sorting          = sorting;
			_notes            = notes;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 样表编号
        /// </summary>
		public decimal SheetId
		{
			get 
			{				
				return _sheetId;
			}
			set 
			{
				if (_sheetId == value)
                    return;
				_sheetId = value;
			}
		}

		/// <summary>
        /// 报表编号
        /// </summary>
		public decimal ReportId
		{
			get 
			{				
				return _reportId;
			}
			set 
			{
				if (_reportId == value)
                    return;
				_reportId = value;
			}
		}

		/// <summary>
        /// 样表名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 样表名称不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "样表名称长度范围在1位～256位！")]
		public string SheetName
		{
			get 
			{				
				return _sheetName;
			}
			set 
			{
				if (_sheetName == value)
                    return;
				_sheetName = value;
			}
		}

		/// <summary>
        /// 样表编码
        /// </summary>
        [NotNullValidator(MessageTemplate = " 样表编码不能为空")]
        [StringLengthValidator(1, 32, MessageTemplate = "样表编码长度范围在1位～32位！")]
		public string SheetCode
		{
			get 
			{				
				return _sheetCode;
			}
			set 
			{
				if (_sheetCode == value)
                    return;
				_sheetCode = value;
			}
		}

		/// <summary>
        /// 样表描述
        /// </summary>
        [NotNullValidator(MessageTemplate = " 样表描述不能为空")]
        [StringLengthValidator(1, 2048, MessageTemplate = "样表描述长度范围在1位～2048位！")]
		public string SheetDescription
		{
			get 
			{				
				return _sheetDescription;
			}
			set 
			{
				if (_sheetDescription == value)
                    return;
				_sheetDescription = value;
			}
		}

		/// <summary>
        /// 行数
        /// </summary>
		public int SheetRowCount
		{
			get 
			{				
				return _sheetRowCount;
			}
			set 
			{
				if (_sheetRowCount == value)
                    return;
				_sheetRowCount = value;
			}
		}

		/// <summary>
        /// 列数
        /// </summary>
		public int SheetColCount
		{
			get 
			{				
				return _sheetColCount;
			}
			set 
			{
				if (_sheetColCount == value)
                    return;
				_sheetColCount = value;
			}
		}

		/// <summary>
        /// 上边距
        /// </summary>
		public int MarginTop
		{
			get 
			{				
				return _marginTop;
			}
			set 
			{
				if (_marginTop == value)
                    return;
				_marginTop = value;
			}
		}

		/// <summary>
        /// 下边距
        /// </summary>
		public int MarginBottom
		{
			get 
			{				
				return _marginBottom;
			}
			set 
			{
				if (_marginBottom == value)
                    return;
				_marginBottom = value;
			}
		}

		/// <summary>
        /// 左边距
        /// </summary>
		public int MarginLeft
		{
			get 
			{				
				return _marginLeft;
			}
			set 
			{
				if (_marginLeft == value)
                    return;
				_marginLeft = value;
			}
		}

		/// <summary>
        /// 右边距
        /// </summary>
		public int MarginRight
		{
			get 
			{				
				return _marginRight;
			}
			set 
			{
				if (_marginRight == value)
                    return;
				_marginRight = value;
			}
		}

		/// <summary>
        /// 批文编号
        /// </summary>
		public int ApprovalNumber
		{
			get 
			{				
				return _approvalNumber;
			}
			set 
			{
				if (_approvalNumber == value)
                    return;
				_approvalNumber = value;
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