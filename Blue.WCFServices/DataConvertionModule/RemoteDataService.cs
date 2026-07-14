//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RemoteDataService.cs
// 描述: RemoteData 操作服务类
// 作者：ChenJie 
// 编写日期：2018/10/27
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.DataConvertionModule;
using Blue.BusinessInterface.DataConvertionModule;
using Blue.WCFContracts.DataConvertionModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.DataConvertionModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.RemoteData.
    /// </summary>
    public class RemoteDataService : CommonNodeServices, IRemoteDataContract
    {
        #region 业务实例
        
        private static readonly IRemoteDataHandler remoteDataHandler = BusinessLogicContainer.Instance.DataConvertionModuleContainer.Resolve<IRemoteDataHandler>();

        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public RemoteDataService() : base(remoteDataHandler)
        {
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 RemoteData 表中插入一条新记录
		/// </summary>
		/// <param name="remoteDataInfo"></param>
		/// <returns></returns>
		public decimal Insert(RemoteDataInfo remoteDataInfo)
		{
            return remoteDataHandler.Insert(remoteDataInfo);
		}
        
        /// <summary>
		/// 获得 RemoteDataInfo 对象
		/// </summary>
		///<param name="remoteDataId">远程数据交换编号</param>
		/// <returns> RemoteDataInfo 对象</returns>
		public RemoteDataInfo GetModelInfo(decimal remoteDataId)
		{	
            return remoteDataHandler.GetModelInfo(remoteDataId);           
		}		
		
        /// <summary>
		/// 更新 RemoteDataInfo 对象
		/// </summary>
		/// <param name="remoteDataInfo">RemoteDataInfo 对象</param>
		public void Update(RemoteDataInfo remoteDataInfo)
		{	          
            remoteDataHandler.Update(remoteDataInfo);
        }	
  
        /// <summary>
		/// 删除 RemoteDataInfo 对象
		/// </summary>
		///<param name="remoteDataId">远程数据交换编号</param>
		/// <returns> RemoteDataInfo 对象</returns>
		public void Delete(decimal remoteDataId)
		{	
            remoteDataHandler.Delete(remoteDataId);
        }
        
        /// <summary>
        /// 获得 RemoteDataInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>RemoteDataInfo 对象列表</returns>
        public IList<RemoteDataInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return remoteDataHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 RemoteData 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> RemoteDataInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return remoteDataHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得字段的对应关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <param name="destinationTableId"></param>
        /// <returns></returns>
        public Dictionary<decimal, decimal> GetDataFieldRelation(decimal remoteDataId, decimal destinationTableId)
        {
            return remoteDataHandler.GetDataFieldRelation(remoteDataId, destinationTableId);
        }

        /// <summary>
        /// 获得 RemoteDataAndFieldInfo 对象的列表
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <returns></returns>
        public IList<RemoteDataAndFieldInfo> GetModelInfos(decimal remoteDataId)
        {
            return remoteDataHandler.GetModelInfos(remoteDataId);
        }

        /// <summary>
        /// 更新字段关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <param name="keyValueItems"></param>
        public void UpdateDataFieldRelation(decimal remoteDataId, List<KeyValueItem> keyValueItems)
        {
            remoteDataHandler.UpdateDataFieldRelation(remoteDataId, keyValueItems);
        }

        /// <summary>
        /// 获本地的表与字段对应关系
        /// </summary>
        /// <param name="remoteDataId"></param>
        /// <returns></returns>
        public Dictionary<decimal, Dictionary<decimal, decimal>> GetTableRelation(decimal remoteDataId)
        {
            return remoteDataHandler.GetTableRelation(remoteDataId);
        }

        #endregion
    }
}
