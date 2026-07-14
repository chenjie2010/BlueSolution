//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSnapshotService.cs
// 描述: CustomSnapshot 操作服务类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessDesignerModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomSnapshot.
    /// </summary>
    public class CustomSnapshotService : ICustomSnapshotContract
    {
        #region 业务实例
        
        private static readonly ICustomSnapshotHandler customSnapshotHandler = BusinessLogicContainer.Instance.BusinessDesignerModuleContainer.Resolve<ICustomSnapshotHandler>();

        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomSnapshotService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 CustomSnapshot 表中插入一条新记录
		/// </summary>
		/// <param name="customSnapshotInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomSnapshotInfo customSnapshotInfo)
		{
            return customSnapshotHandler.Insert(customSnapshotInfo);
		}
        
        /// <summary>
		/// 获得 CustomSnapshotInfo 对象
		/// </summary>
		///<param name="snapshotId">快照编号</param>
		/// <returns> CustomSnapshotInfo 对象</returns>
		public CustomSnapshotInfo GetModelInfo(decimal snapshotId)
		{	
            return customSnapshotHandler.GetModelInfo(snapshotId);           
		}		
		
        /// <summary>
		/// 更新 CustomSnapshotInfo 对象
		/// </summary>
		/// <param name="customSnapshotInfo">CustomSnapshotInfo 对象</param>
		public void Update(CustomSnapshotInfo customSnapshotInfo)
		{	          
            customSnapshotHandler.Update(customSnapshotInfo);
        }	
  
        /// <summary>
		/// 删除 CustomSnapshotInfo 对象
		/// </summary>
		///<param name="snapshotId">快照编号</param>
		/// <returns> CustomSnapshotInfo 对象</returns>
		public void Delete(decimal snapshotId)
		{	
            customSnapshotHandler.Delete(snapshotId);
        }
        
        /// <summary>
        /// 获得 CustomSnapshotInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomSnapshotInfo 对象列表</returns>
        public IList<CustomSnapshotInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customSnapshotHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomSnapshot 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> CustomSnapshotInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customSnapshotHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得统计类型快照列表
        /// </summary>
        /// <param name="coverId"></param>
        /// <returns></returns>
        public IList<CommonItem<decimal>> GetCommonItems(decimal reportId)
        {
            return customSnapshotHandler.GetCommonItems(reportId);
        }

        /// <summary>
        /// 获得基础类型快照列表
        /// </summary>
        /// <param name="coverId"></param>
        /// <param name="reportId"></param>
        /// <returns></returns>
        public IList<CommonItem<decimal>> GetCommonItems(decimal reportId, decimal userId)
        {
            return customSnapshotHandler.GetCommonItems(reportId, userId);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <returns></returns>
        public byte[] DownloadSnapshot(decimal snapshotId)
        {
            return customSnapshotHandler.DownloadSnapshot(snapshotId);
        }

        /// <summary>
        ///  插入记录与报表文件
        /// </summary>
        /// <param name="customSnapshotInfo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public decimal Insert(CustomSnapshotInfo customSnapshotInfo, byte[] data)
        {
            return customSnapshotHandler.Insert(customSnapshotInfo, data);
        }

        #endregion
    }
}
