//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomCategoryService.cs
// 描述：CustomCategory 操作服务类
// 作者：ChenJie 
// 编写日期：2016/9/17
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
    /// 操作服务类，对于的表： dbo.CustomCategory.
    /// </summary>
    public class CustomCategoryService : CommonNodeServices, ICustomCategoryContract
    {
        #region 业务实例
        
        private static readonly ICustomCategoryHandler customCategoryHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomCategoryHandler>();
        
        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomCategoryService() : base(customCategoryHandler)
        {
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 customcategory 表中插入一条新记录
		/// </summary>
		/// <param name="customCategoryInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomCategoryInfo customCategoryInfo)
		{
            return customCategoryHandler.Insert(customCategoryInfo);
		}
        
        /// <summary>
		/// 获得 CustomCategoryInfo 对象
		/// </summary>
		///<param name="categoryId">分类编号</param>
		/// <returns> CustomCategoryInfo 对象</returns>
		public CustomCategoryInfo GetModelInfo(decimal categoryId)
		{	
            return customCategoryHandler.GetModelInfo(categoryId);           
		}		
		
        /// <summary>
		/// 更新 CustomCategoryInfo 对象
		/// </summary>
		/// <param name="customCategoryInfo">CustomCategoryInfo 对象</param>
		public void Update(CustomCategoryInfo customCategoryInfo)
		{	          
            customCategoryHandler.Update(customCategoryInfo);
        }	
  
        /// <summary>
		/// 删除 CustomCategoryInfo 对象
		/// </summary>
		///<param name="categoryId">分类编号</param>
		/// <returns> CustomCategoryInfo 对象</returns>
		public void Delete(decimal categoryId)
		{	
            customCategoryHandler.Delete(categoryId);
        }
        
        /// <summary>
		/// 获得 CustomCategoryInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomCategoryInfo 对象列表</returns>
		public IList<CustomCategoryInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return customCategoryHandler.GetModelInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 CustomCategory 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomCategoryInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return customCategoryHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="databaseIds"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(IList<decimal> databaseIds)
        {
            return customCategoryHandler.GetPageRecord(databaseIds);
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal databaseId)
        {
            return customCategoryHandler.GetPageRecord(databaseId);
        }

        /// <summary>
        /// 获得数据库编号
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public decimal GetDatabaseId(decimal categoryId)
        {
            return customCategoryHandler.GetDatabaseId(categoryId);
        }

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal categoryId)
        {
            return customCategoryHandler.GetDataWarehouseId(categoryId);
        }

        #endregion
    }
}
