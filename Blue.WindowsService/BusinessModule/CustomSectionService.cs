//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomSectionService.cs
// 描述: CustomSection 操作服务类
// 作者：ChenJie 
// 编写日期：2018/8/13
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomSection.
    /// </summary>
    public class CustomSectionService : CommonNodeServices, ICustomSectionContract
    {
        #region 业务实例
        
        private static readonly ICustomSectionHandler customSectionHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomSectionHandler>();

        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomSectionService() : base(customSectionHandler)
        {
              
		}
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 CustomSection 表中插入一条新记录
        /// </summary>
        /// <param name="customSectionInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomSectionInfo customSectionInfo)
        {
            return customSectionHandler.Insert(customSectionInfo);
        }

        /// <summary>
        /// 获得 CustomSectionInfo 对象
        /// </summary>
        ///<param name="sectionId">窗体分组编号</param>
        /// <returns> CustomSectionInfo 对象</returns>
        public CustomSectionInfo GetModelInfo(decimal sectionId)
        {
            return customSectionHandler.GetModelInfo(sectionId);
        }

        /// <summary>
        /// 更新 CustomSectionInfo 对象
        /// </summary>
        /// <param name="customSectionInfo">CustomSectionInfo 对象</param>
        public void Update(CustomSectionInfo customSectionInfo)
        {
            customSectionHandler.Update(customSectionInfo);
        }

        /// <summary>
        /// 删除 CustomSectionInfo 对象
        /// </summary>
        ///<param name="sectionId">窗体分组编号</param>
        /// <returns> CustomSectionInfo 对象</returns>
        public void Delete(decimal sectionId)
        {
            customSectionHandler.Delete(sectionId);
        }

        /// <summary>
        /// 获得 CustomSectionInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomSectionInfo 对象列表</returns>
        public IList<CustomSectionInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customSectionHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomSection 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> CustomSectionInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customSectionHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得所有的窗体
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public IList<CustomSectionInfo> GetModelInfos(decimal dataId)
        {
            return customSectionHandler.GetModelInfos(dataId);
        }

        #endregion
    }
}
