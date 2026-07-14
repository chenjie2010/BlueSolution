//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomMenuService.cs
// 描述：CustomMenu 操作服务类
// 作者：ChenJie 
// 编写日期：2017/12/14
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomMenu.
    /// </summary>
    public class CustomMenuService : CommonNodeServices, ICustomMenuContract
    {
        #region 业务实例
        
        private static readonly ICustomMenuHandler customMenuHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomMenuHandler>();
        
        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomMenuService() : base(customMenuHandler)
        {
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 custommenu 表中插入一条新记录
		/// </summary>
		/// <param name="customMenuInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomMenuInfo customMenuInfo)
		{
            return customMenuHandler.Insert(customMenuInfo);
		}
        
        /// <summary>
		/// 获得 CustomMenuInfo 对象
		/// </summary>
		///<param name="menuId">菜单编号</param>
		/// <returns> CustomMenuInfo 对象</returns>
		public CustomMenuInfo GetModelInfo(decimal menuId)
		{	
            return customMenuHandler.GetModelInfo(menuId);           
		}		
		
        /// <summary>
		/// 更新 CustomMenuInfo 对象
		/// </summary>
		/// <param name="customMenuInfo">CustomMenuInfo 对象</param>
		public void Update(CustomMenuInfo customMenuInfo)
		{	          
            customMenuHandler.Update(customMenuInfo);
        }	
  
        /// <summary>
		/// 删除 CustomMenuInfo 对象
		/// </summary>
		///<param name="menuId">菜单编号</param>
		/// <returns> CustomMenuInfo 对象</returns>
		public void Delete(decimal menuId)
		{	
            customMenuHandler.Delete(menuId);
        }
        
        /// <summary>
		/// 获得 CustomMenuInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomMenuInfo 对象列表</returns>
		public IList<CustomMenuInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return customMenuHandler.GetModelInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 CustomMenu 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomMenuInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return customMenuHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 根据菜单类型获得一级菜单
        /// </summary>
        /// <param name="menuType"></param>
        /// <returns></returns>
        public CustomMenuInfo GetCustomMenu(byte menuType)
        {
            return customMenuHandler.GetCustomMenu(menuType);
        }

        /// <summary>
        /// 最大的菜单类型
        /// </summary>
        /// <returns></returns>
        public byte GetMaxMenuType()
        {
            return customMenuHandler.GetMaxMenuType();
        }

        /// <summary>
        /// 向 CustomMenu 表中插入一条新记录
        /// </summary>
        /// <param name="customMenuInfo">customMenuInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomMenuInfo customMenuInfo, byte[] imageData)
        {
            return customMenuHandler.Insert(customMenuInfo, imageData);
        }

        /// <summary>
        /// 更新 CustomMenuInfo 对象
        /// </summary>
        /// <param name="customMenuInfo">CustomMenuInfo 对象</param>
        /// <param name="imageData">图片数据</param>
        public void Update(CustomMenuInfo customMenuInfo, byte[] imageData)
        {
            customMenuHandler.Update(customMenuInfo, imageData);
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileName">下载的图片文件名</param>
        /// <returns></returns>
        public byte[] DownLoadIcons(string fileName)
        {
            return customMenuHandler.DownLoadIcons(fileName);
        }

        /// <summary>
        /// 获得菜单分类对象列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<CustomMenuInfo> GetMenuClasses(decimal userId)
        {
            return customMenuHandler.GetMenuClasses(userId);
        }

        #endregion
    }
}
