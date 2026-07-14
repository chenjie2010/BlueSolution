//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDatabaseService.cs
// 描述：CustomDatabase 操作服务类
// 作者：ChenJie 
// 编写日期：2016/9/11
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomDatabase.
    /// </summary>
    public class CustomDatabaseService : CommonNodeServices, ICustomDatabaseContract
    {
        #region 业务实例
        
        private static readonly ICustomDatabaseHandler customDatabaseHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDatabaseHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomDatabaseService() : base(customDatabaseHandler)
        {

        }
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 customdatabase 表中插入一条新记录
		/// </summary>
		/// <param name="customDatabaseInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomDatabaseInfo customDatabaseInfo)
		{
            return customDatabaseHandler.Insert(customDatabaseInfo);
		}
        
        /// <summary>
		/// 获得 CustomDatabaseInfo 对象
		/// </summary>
		///<param name="databaseId">数据库编号</param>
		/// <returns> CustomDatabaseInfo 对象</returns>
		public CustomDatabaseInfo GetModelInfo(decimal databaseId)
		{	
            return customDatabaseHandler.GetModelInfo(databaseId);           
		}		
		
        /// <summary>
		/// 更新 CustomDatabaseInfo 对象
		/// </summary>
		/// <param name="customDatabaseInfo">CustomDatabaseInfo 对象</param>
		public void Update(CustomDatabaseInfo customDatabaseInfo)
		{	          
            customDatabaseHandler.Update(customDatabaseInfo);
        }	
  
        /// <summary>
		/// 删除 CustomDatabaseInfo 对象
		/// </summary>
		///<param name="databaseId">数据库编号</param>
		/// <returns> CustomDatabaseInfo 对象</returns>
		public void Delete(decimal databaseId)
		{	
            customDatabaseHandler.Delete(databaseId);
        }
        
        /// <summary>
		/// 获得 CustomDatabaseInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDatabaseInfo 对象列表</returns>
		public IList<CustomDatabaseInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return customDatabaseHandler.GetModelInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 CustomDatabase 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomDatabaseInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return customDatabaseHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(byte dataWarehouseId)
        {
            return customDatabaseHandler.GetPageRecord(dataWarehouseId);
        }

        #endregion
    }
}
