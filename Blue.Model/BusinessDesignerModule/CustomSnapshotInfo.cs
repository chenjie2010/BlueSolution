//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSnapshotInfo.cs
// 描述: CustomSnapshotInfo 实体类
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
	/// <para>CustomSnapshotInfo 类</para>
	/// <para>表套快照</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class CustomSnapshotInfo
	{
		#region 内部成员变量
        
		private decimal _snapshotId;
		private decimal _reportId;
		private decimal _userId;
		private string _snapshotName = string.Empty;
		private string _snapshotFile = string.Empty;
		private DateTime _expireDate;
		private int _sorting;
		private string _notes = string.Empty;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public CustomSnapshotInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="snapshotId">快照编号</param>
		///<param name="reportId">报表编号</param>
		///<param name="userId">用户编号</param>
		///<param name="snapshotName">快照名称</param>
		///<param name="snapshotFile">快照文件名</param>
		///<param name="expireDate">快照日期</param>
		///<param name="sorting">排序</param>
		///<param name="notes">备注</param>
		public CustomSnapshotInfo(decimal snapshotId, decimal reportId, decimal userId, string snapshotName, string snapshotFile, 
			DateTime expireDate, int sorting, string notes)
		{
			_snapshotId   = snapshotId;
			_reportId     = reportId;
			_userId       = userId;
			_snapshotName = snapshotName;
			_snapshotFile = snapshotFile;
			_expireDate   = expireDate;
			_sorting      = sorting;
			_notes        = notes;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 快照编号
        /// </summary>
		public decimal SnapshotId
		{
			get 
			{				
				return _snapshotId;
			}
			set 
			{
				if (_snapshotId == value)
                    return;
				_snapshotId = value;
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
        /// 用户编号
        /// </summary>
		public decimal UserId
		{
			get 
			{				
				return _userId;
			}
			set 
			{
				if (_userId == value)
                    return;
				_userId = value;
			}
		}

		/// <summary>
        /// 快照名称
        /// </summary>
        [NotNullValidator(MessageTemplate = " 快照名称不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "快照名称长度范围在1位～256位！")]
		public string SnapshotName
		{
			get 
			{				
				return _snapshotName;
			}
			set 
			{
				if (_snapshotName == value)
                    return;
				_snapshotName = value;
			}
		}

		/// <summary>
        /// 快照文件名
        /// </summary>
        [NotNullValidator(MessageTemplate = " 快照文件名不能为空")]
        [StringLengthValidator(1, 256, MessageTemplate = "快照文件名长度范围在1位～256位！")]
		public string SnapshotFile
		{
			get 
			{				
				return _snapshotFile;
			}
			set 
			{
				if (_snapshotFile == value)
                    return;
				_snapshotFile = value;
			}
		}

		/// <summary>
        /// 快照日期
        /// </summary>
		public DateTime ExpireDate
		{
			get 
			{				
				return _expireDate;
			}
			set 
			{
				if (_expireDate == value)
                    return;
				_expireDate = value;
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