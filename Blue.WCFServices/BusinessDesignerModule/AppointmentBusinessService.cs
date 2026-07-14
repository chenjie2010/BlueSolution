//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppointmentBusinessService.cs
// 描述: AppointmentBusiness 操作服务类
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
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessDesignerModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.AppointmentBusiness.
    /// </summary>
    public class AppointmentBusinessService : CommonNodeServices, IAppointmentBusinessContract
    {
        #region 业务实例

        private static readonly IAppointmentBusinessHandler appointmentBusinessHandler = BusinessLogicContainer.Instance.BusinessDesignerModuleContainer.Resolve<IAppointmentBusinessHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public AppointmentBusinessService() : base(appointmentBusinessHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 AppointmentBusiness 表中插入一条新记录
        /// </summary>
        /// <param name="appointmentBusinessInfo"></param>
        /// <returns></returns>
        public decimal Insert(AppointmentBusinessInfo appointmentBusinessInfo)
        {
            return appointmentBusinessHandler.Insert(appointmentBusinessInfo);
        }

        /// <summary>
        /// 获得 AppointmentBusinessInfo 对象
        /// </summary>
        ///<param name="appointmentId">预约编号</param>
        /// <returns> AppointmentBusinessInfo 对象</returns>
        public AppointmentBusinessInfo GetModelInfo(decimal appointmentId)
        {
            return appointmentBusinessHandler.GetModelInfo(appointmentId);
        }

        /// <summary>
        /// 更新 AppointmentBusinessInfo 对象
        /// </summary>
        /// <param name="appointmentBusinessInfo">AppointmentBusinessInfo 对象</param>
        public void Update(AppointmentBusinessInfo appointmentBusinessInfo)
        {
            appointmentBusinessHandler.Update(appointmentBusinessInfo);
        }

        /// <summary>
        /// 删除 AppointmentBusinessInfo 对象
        /// </summary>
        ///<param name="appointmentId">预约编号</param>
        /// <returns> AppointmentBusinessInfo 对象</returns>
        public void Delete(decimal appointmentId)
        {
            appointmentBusinessHandler.Delete(appointmentId);
        }

        /// <summary>
        /// 获得 AppointmentBusinessInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AppointmentBusinessInfo 对象列表</returns>
        public IList<AppointmentBusinessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return appointmentBusinessHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 AppointmentBusiness 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> AppointmentBusinessInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return appointmentBusinessHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        #endregion
    }
}
