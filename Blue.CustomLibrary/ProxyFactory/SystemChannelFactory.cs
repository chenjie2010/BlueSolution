//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: SystemChannelFactory.cs
// 描述: 系统模块类来创建客户端代理对象
// 作者：ChenJie 
// 编写日期：2016/08/19
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts.SystemModule;

namespace Blue.CustomLibrary
{
    /// <summary>
    /// 系统模块类
    /// </summary>
    public sealed class SystemChannelFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SystemChannelFactory()
        {

        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 根据默认地址来创建 CustomDepartmentService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ICustomDepartmentContract CreateCustomDepartmentContract()
        {
            ICustomDepartmentContract customDepartmentContract = ServiceProxyFactory.Create<ICustomDepartmentContract>("CustomDepartmentService");

            return customDepartmentContract;
        }

        /// <summary>
        /// 根据默认地址来创建 UserTypeService 代理对象
        /// </summary>
        /// <returns></returns>
        public static IUserTypeContract CreateUserTypeContract()
        {
            IUserTypeContract userTypeContract = ServiceProxyFactory.Create<IUserTypeContract>("UserTypeService");

            return userTypeContract;
        }

        /// <summary>
        /// 根据默认地址来创建 SystemConfigService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ISystemConfigContract CreateSystemConfigContract()
        {
            ISystemConfigContract systemConfigContract = ServiceProxyFactory.Create<ISystemConfigContract>("SystemConfigService");

            return systemConfigContract;
        }

        /// <summary>
        /// 根据默认地址来创建 TreeLayerService 代理对象
        /// </summary>
        /// <returns></returns>
        public static ITreeLayerContract CreateTreeLayerContract()
        {
            ITreeLayerContract treeLayerContract = ServiceProxyFactory.Create<ITreeLayerContract>("TreeLayerService");

            return treeLayerContract;
        }

        #endregion
    }
}
