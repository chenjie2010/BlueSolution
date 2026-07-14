//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ServiceProxyFactory.cs
// 描述: 服务代理工厂类
// 作者：ChenJie 
// 编写日期：2016-07-27
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// 服务代理工厂类
    /// </summary>
    public class ServiceProxyFactory
    {
        #region 创建代理方法

        /// <summary>
        /// 创建代理方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="endpointName"></param>
        /// <returns></returns>
        public static T Create<T>(string endpointName)
        {
            if (string.IsNullOrEmpty(endpointName))
            {
                throw new ArgumentNullException("endpointName");
            }
            return (T)(new ServiceRealProxy<T>(endpointName).GetTransparentProxy());
        }        

        /// <summary>
        /// 创建代理方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="endpointName"></param>
        /// <returns></returns>
        public static T Create<T>(InstanceContext context, string endpointName)
        {
            T channel = ChannelFactoryCreator.Create<T>(context, endpointName).CreateChannel();

            return channel;            
        }

        ///// <summary>
        ///// 创建代理方法
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="context"></param>
        ///// <param name="endpointName"></param>
        ///// <returns></returns>
        //public static T CreateDuplex<T>(InstanceContext context, string endpointName)
        //{
        //    if (string.IsNullOrEmpty(endpointName))
        //    {
        //        throw new ArgumentNullException("endpointName");
        //    }
        //    return (T)(new ServiceRealProxy<T>(context, endpointName).GetTransparentProxy());
        //}

        #endregion
    }
}
