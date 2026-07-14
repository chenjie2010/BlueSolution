//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDataService.cs
// 描述：CustomData 操作服务类
// 作者：ChenJie 
// 编写日期：2017/11/27
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
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomData.
    /// </summary>
    public class CustomDataService : CommonNodeServices, ICustomDataContract
    {
        #region 业务实例

        private static readonly ICustomDataHandler customDataHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDataHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomDataService() : base(customDataHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customdata 表中插入一条新记录
        /// </summary>
        /// <param name="customDataInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomDataInfo customDataInfo)
        {
            return customDataHandler.Insert(customDataInfo);
        }

        /// <summary>
        /// 获得 CustomDataInfo 对象
        /// </summary>
        ///<param name="dataId">数据填报编号</param>
        /// <returns> CustomDataInfo 对象</returns>
        public CustomDataInfo GetModelInfo(decimal dataId)
        {
            return customDataHandler.GetModelInfo(dataId);
        }

        /// <summary>
        /// 更新 CustomDataInfo 对象
        /// </summary>
        /// <param name="customDataInfo">CustomDataInfo 对象</param>
        public void Update(CustomDataInfo customDataInfo)
        {
            customDataHandler.Update(customDataInfo);
        }

        /// <summary>
        /// 删除 CustomDataInfo 对象
        /// </summary>
        ///<param name="dataId">数据填报编号</param>
        /// <returns> CustomDataInfo 对象</returns>
        public void Delete(decimal dataId)
        {
            customDataHandler.Delete(dataId);
        }

        /// <summary>
        /// 获得 CustomDataInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDataInfo 对象列表</returns>
        public IList<CustomDataInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customDataHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomData 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomDataInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customDataHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得填报属性
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public byte GetDataFilledProperty(decimal dataId)
        {
            return customDataHandler.GetDataFilledProperty(dataId);
        }

        /// <summary>
        /// 根据当前数据编号、用户填报编号、当前业务状态，获取下一步的评审人列表。
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        public Dictionary<decimal, string> GetFinalReviewers(decimal dataId)
        {
            return customDataHandler.GetFinalReviewers(dataId);
        }

      /// <summary>
      /// 根据当前数据编号、用户填报编号、当前业务状态，获取下一步的评审人列表。
      /// </summary>
      /// <param name="dataId"></param>
      /// <param name="userId"></param>
      /// <returns></returns>
        public Dictionary<decimal, string> GetInitReviewers(decimal dataId, decimal userId)
        {
            return customDataHandler.GetInitReviewers(dataId, userId);
        }

        /// <summary>
        /// 向 CustomData 表中插入一条新记录
        /// </summary>
        /// <param name="customDataInfo">customDataInfo 对象</param>
        /// <param name="upLoadFileInfos">附件列表</param>
        /// <param name="conditionalUpLoadFileInfos">条件附件列表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos)
        {
            return customDataHandler.Insert(customDataInfo, upLoadFileInfos, conditionalUpLoadFileInfos);
        }

        /// <summary>
        /// 更新数据填报和附件信息
        /// </summary>
        /// <param name="customDataInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="conditionalUpLoadFileInfos">条件附件列表</param>
        public void Update(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos)
        {
            customDataHandler.Update(customDataInfo, upLoadFileInfos, conditionalUpLoadFileInfos);
        }

        #endregion
    }
}
