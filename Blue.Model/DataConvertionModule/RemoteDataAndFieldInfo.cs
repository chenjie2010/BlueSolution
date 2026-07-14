//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RemoteDataAndFieldInfo.cs
// 描述: RemoteDataAndFieldInfo 实体类
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
	/// <para>RemoteDataAndFieldInfo 类</para>
	/// <para>远程数据交换与字段</para>
	/// <para><see cref="member"/></para>
	/// <remarks></remarks>
	/// </summary>
	[Serializable]
	public class RemoteDataAndFieldInfo
	{
		#region 内部成员变量
        
		private decimal _dataFieldId;
		private decimal _remoteDataId;
		private decimal _remoteDataFieldId;
        
		#endregion
		
		#region 构造函数
        
		/// <summary>
        /// 默认的构造函数
        /// </summary>
		public RemoteDataAndFieldInfo()
		{
			
		}
		
		/// <summary>
        /// 构造函数
        /// </summary>
		///<param name="dataFieldId">字段编号</param>
		///<param name="remoteDataId">远程数据交换编号</param>
		///<param name="remoteDataFieldId">远程字段编号</param>
		public RemoteDataAndFieldInfo(decimal dataFieldId, decimal remoteDataId, decimal remoteDataFieldId)
		{
			_dataFieldId       = dataFieldId;
			_remoteDataId      = remoteDataId;
			_remoteDataFieldId = remoteDataFieldId;
			
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
        /// 远程字段编号
        /// </summary>
		public decimal RemoteDataFieldId
		{
			get 
			{				
				return _remoteDataFieldId;
			}
			set 
			{
				if (_remoteDataFieldId == value)
                    return;
				_remoteDataFieldId = value;
			}
		}
        
		#endregion
        
	}
}