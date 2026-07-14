//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: BusinessDesignerChannelFactory.cs
// 描述: 业务设计模块类来创建客户端代理对象
// 作者：ChenJie 
// 编写日期：2018/09/07
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts.BusinessDesignerModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 业务设计模块类
    /// </summary>
    public sealed class BusinessDesignerChannelFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static BusinessDesignerChannelFactory()
        {

        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 根据默认地址来创建 AppointmentBusinessService 代理对象
        /// </summary>
        /// <returns></returns>
        public static IAppointmentBusinessContract CreateAppointmentBusinessContract()
        {
            IAppointmentBusinessContract customBusinessContract = ServiceProxyFactory.Create<IAppointmentBusinessContract>("AppointmentBusinessService");

            return customBusinessContract;
        }

        /// <summary>
        /// 根据默认地址来创建枚举管理的 IDataAuditingContract 代理对象
        /// </summary>
        /// <returns></returns>
        public static IDataAuditingContract CreateDataAuditingContract()
        {
            IDataAuditingContract dataAuditingContract = ServiceProxyFactory.Create<IDataAuditingContract>("DataAuditingService");

            return dataAuditingContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomReportService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomReportContract CreateCustomReportContract()
        {
            ICustomReportContract customViewAndDataFieldContract = ServiceProxyFactory.Create<ICustomReportContract>("CustomReportService");

            return customViewAndDataFieldContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomSheetService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomSheetContract CreateCustomSheetContract()
        {
            ICustomSheetContract customReportContract = ServiceProxyFactory.Create<ICustomSheetContract>("CustomSheetService");

            return customReportContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomCellService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomCellContract CreateCustomCellContract()
        {
            ICustomCellContract customCellContract = ServiceProxyFactory.Create<ICustomCellContract>("CustomCellService");

            return customCellContract;
        }

        /// <summary>
        /// 根据默认地址来创建 CustomSnapshotService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomSnapshotContract CreateCustomSnapshotContract()
        {
            ICustomSnapshotContract customSnapshotContract = ServiceProxyFactory.Create<ICustomSnapshotContract>("CustomSnapshotService");

            return customSnapshotContract;
        }

        /// <summary>
        /// 根据默认地址来创建 DataAuditingLogService 代理对象
        /// </summary>
        /// <returns></returns>
        public static IDataAuditingLogContract CreateDataAuditingLogContract()
        {
            IDataAuditingLogContract dataAuditingLogContract = ServiceProxyFactory.Create<IDataAuditingLogContract>("DataAuditingLogService");

            return dataAuditingLogContract;
        }

        /// <summary>
        /// 根据默认地址来创建 DataAuditingStepService 代理对象
        /// </summary>
        /// <returns></returns>
        public static IDataAuditingStepContract CreateDataAuditingStepContract()
        {
            IDataAuditingStepContract dataAuditingStepContract = ServiceProxyFactory.Create<IDataAuditingStepContract>("DataAuditingStepService");

            return dataAuditingStepContract;
        }
        #endregion
    }
}
