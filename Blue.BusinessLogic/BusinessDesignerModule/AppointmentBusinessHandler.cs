//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppointmentBusinessHandler.cs
// 描述: AppointmentBusiness 业务处理类
// 作者：ChenJie 
// 编写日期：2018/8/24
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.CustomLibrary;
using Blue.DALFactory;
using Blue.IDAL.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;

namespace Blue.BusinessLogic.BusinessDesignerModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.AppointmentBusiness.
    /// </summary>
    public class AppointmentBusinessHandler : CommonNodeBusiness, IAppointmentBusinessHandler
    {
        #region 工厂类实例

        private static readonly IAppointmentBusiness dalAppointmentBusiness = BusinessDesignerDataAccessFactory.CreateAppointmentBusiness();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public AppointmentBusinessHandler() : base(dalAppointmentBusiness)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 appointmentbusiness 表中插入一条新记录
        /// </summary>
        /// <param name="appointmentBusinessInfo"></param>
        /// <returns></returns>
        public decimal Insert(AppointmentBusinessInfo appointmentBusinessInfo)
        {
            //自动增加的关键字的值
            decimal appointmentBusinessId = 0;

            // 验证输入
            if (appointmentBusinessInfo == null)
            {
                throw new ArgumentException("不能插入空对象。");
            }

            try
            {
                appointmentBusinessId = dalAppointmentBusiness.Insert(appointmentBusinessInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return appointmentBusinessId;
        }

        /// <summary>
        /// 获得 AppointmentBusinessInfo 对象
        /// </summary>
        ///<param name="appointmentId">预约编号</param>
        /// <returns> AppointmentBusinessInfo 对象</returns>
        public AppointmentBusinessInfo GetModelInfo(decimal appointmentId)
        {
            AppointmentBusinessInfo appointmentBusinessInfo = null;

            // 验证输入
            if (appointmentId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                appointmentBusinessInfo = dalAppointmentBusiness.GetModelInfo(appointmentId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return appointmentBusinessInfo;
        }

        /// <summary>
        /// 更新 AppointmentBusinessInfo 对象
        /// </summary>
        /// <param name="appointmentBusinessInfo">AppointmentBusinessInfo 对象</param>
        public void Update(AppointmentBusinessInfo appointmentBusinessInfo)
        {
            // 验证输入
            if (appointmentBusinessInfo == null)
            {
                throw new ArgumentException("不能更新空对象。");
            }
            try
            {
                dalAppointmentBusiness.Update(appointmentBusinessInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 AppointmentBusinessInfo 对象
        /// </summary>
        ///<param name="appointmentId">预约编号</param>
        /// <returns> AppointmentBusinessInfo 对象</returns>
        public void Delete(decimal appointmentId)
        {
            // 验证输入
            if (appointmentId <= 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalAppointmentBusiness.Delete(appointmentId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 AppointmentBusinessInfo  对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AppointmentBusinessInfo  对象列表</returns>
        public IList<AppointmentBusinessInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<AppointmentBusinessInfo> appointmentBusinessInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                appointmentBusinessInfos = dalAppointmentBusiness.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return appointmentBusinessInfos;
        }

        /// <summary>
        /// 获得 CustomSheet 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomSheetInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalAppointmentBusiness.GetTotalCount(whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 自定义方法

        #endregion

        #region 私有方法

        #endregion
    }
}
