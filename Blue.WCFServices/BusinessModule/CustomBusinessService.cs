//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomBusinessService.cs
// 描述：CustomBusiness 操作服务类
// 作者：ChenJie 
// 编写日期：2017/12/20
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
    /// 操作服务类，对于的表： dbo.CustomBusiness.
    /// </summary>
    public class CustomBusinessService : CommonNodeServices, ICustomBusinessContract
    {
        #region 业务实例

        private static readonly ICustomBusinessHandler customBusinessHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomBusinessHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomBusinessService() : base(customBusinessHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 custombusiness 表中插入一条新记录
        /// </summary>
        /// <param name="customBusinessInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomBusinessInfo customBusinessInfo)
        {
            return customBusinessHandler.Insert(customBusinessInfo);
        }

        /// <summary>
        /// 获得 CustomBusinessInfo 对象
        /// </summary>
        ///<param name="businessId">业务编号</param>
        /// <returns> CustomBusinessInfo 对象</returns>
        public CustomBusinessInfo GetModelInfo(decimal businessId)
        {
            return customBusinessHandler.GetModelInfo(businessId);
        }

        /// <summary>
        /// 更新 CustomBusinessInfo 对象
        /// </summary>
        /// <param name="customBusinessInfo">CustomBusinessInfo 对象</param>
        public void Update(CustomBusinessInfo customBusinessInfo)
        {
            customBusinessHandler.Update(customBusinessInfo);
        }

        /// <summary>
        /// 删除 CustomBusinessInfo 对象
        /// </summary>
        ///<param name="businessId">业务编号</param>
        /// <returns> CustomBusinessInfo 对象</returns>
        public void Delete(decimal businessId)
        {
            customBusinessHandler.Delete(businessId);
        }

        /// <summary>
        /// 获得 CustomBusinessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomBusinessInfo 对象列表</returns>
        public IList<CustomBusinessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customBusinessHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomBusiness 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomBusinessInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customBusinessHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 根据条件查询业务数量
        /// </summary>
        /// <param name="conditionId"></param>
        /// <param name="businessMenu"></param>
        /// <returns></returns>
		public int GetTotalCount(decimal conditionId, BusinessMenu businessMenu)
        {
            return customBusinessHandler.GetTotalCount(conditionId, businessMenu);
        }

        /// <summary>
        /// 通过数据填报编号获取业务名称
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public string GetBusinessNameByInstanceId(decimal dataId)
        {
            return customBusinessHandler.GetBusinessNameByInstanceId(dataId);
        }

        /// <summary>
        /// 获得授权业务
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IList<ExtendedCustomBusinessInfo> GetBusiness(decimal userId)
        {
            return customBusinessHandler.GetBusiness(userId);
        }

        /// <summary>
        /// 下载图片
        /// </summary>
        /// <param name="fileName">下载的图片文件名</param>
        /// <returns></returns>
        public byte[] DownLoadIcons(string fileName)
        {
            return customBusinessHandler.DownLoadIcons(fileName);
        }

        /// <summary>
        /// 向 CustomBusiness 表中插入一条新记录
        /// </summary>
        /// <param name="customBusinessInfo">customBusinessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="imageData">图片数据</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomBusinessInfo customBusinessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, byte[] imageData)
        {
            return customBusinessHandler.Insert(customBusinessInfo, upLoadFileInfos, imageData);
        }

        /// <summary>
        /// 更新 CustomBusinessInfo 对象
        /// </summary>
        /// <param name="customBusinessInfo">CustomBusinessInfo 对象</param>
        /// <param name="upLoadFileInfos">附件</param>
        /// <param name="imageData">图片数据</param>
        public void Update(CustomBusinessInfo customBusinessInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, byte[] imageData)
        {
            customBusinessHandler.Update(customBusinessInfo, upLoadFileInfos, imageData);
        }        

        #endregion
    }
}
