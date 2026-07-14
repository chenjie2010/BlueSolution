//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RemoteDataAndTableInfo.cs
// 描述: RemoteDataAndTableInfo 实体类
// 作者：ChenJie 
// 编写日期：2018/10/27
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;

namespace Blue.Model.DataConvertionModule
{
	/// <summary>
	/// <para>RemoteDataAndTableInfo 类</para>
	/// <para>远程数据交换与表</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class RemoteDataAndTableInfo
	{
		#region 内部成员变量
        
		private decimal _tableId;
		private decimal _remoteDataId;
		private decimal _remoteTableId;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public RemoteDataAndTableInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="tableId">表编号</param>
		///<param name="remoteDataId">远程数据交换编号</param>
		///<param name="remoteTableId">远程表编号</param>
		public RemoteDataAndTableInfo(decimal tableId, decimal remoteDataId, decimal remoteTableId)
		{
			_tableId       = tableId;
			_remoteDataId  = remoteDataId;
			_remoteTableId = remoteTableId;
			
		}
        
		#endregion
		
		#region 字段属性
		
		/// <summary>
        /// 表编号
        /// </summary>
		public decimal TableId
		{
			get 
			{				
				return _tableId;
			}
			set 
			{
				if (_tableId == value)
                    return;
				_tableId = value;
			}
		}

		/// <summary>
        /// 远程数据交换编号
        /// </summary>
		public decimal RemoteDataId
		{
			get 
			{				
				return _remoteDataId;
			}
			set 
			{
				if (_remoteDataId == value)
                    return;
				_remoteDataId = value;
			}
		}

		/// <summary>
        /// 远程表编号
        /// </summary>
		public decimal RemoteTableId
		{
			get 
			{				
				return _remoteTableId;
			}
			set 
			{
				if (_remoteTableId == value)
                    return;
				_remoteTableId = value;
			}
		}
        
		#endregion
        
	}
}