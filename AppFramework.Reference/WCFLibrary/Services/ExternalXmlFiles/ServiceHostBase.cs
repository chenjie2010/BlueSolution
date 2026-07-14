//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ServiceHostBase.cs
// 描述: 服务配置在外部配置文件中的通用操作类
// 作者：ChenJie 
// 编写日期：2010-5-31
// 版权所有 (C) 四川大学 2010
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// 服务配置在外部配置文件中的通用操作类
    /// </summary>
    public class ServiceHostBase
    {
        /// <summary>
        /// 
        /// </summary>
        static List<ServiceHost> hosts = new List<ServiceHost>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="t"></param>
        private static void OpenHost(Type t)
        {
            ServiceHost hst = new ServiceHost(t);
            Type ty = hst.Description.ServiceType;
            hst.Open();
            hosts.Add(hst);
        }

        /// <summary>
        /// 打开所有配置服务
        /// </summary>
        public static void StartAllConfiguredServices()
        {
            ConfiguredServices services = ConfiguredServices.LoadFromFile("services.xml");

            foreach (ConfiguredService svc in services.configuredServices)
            {
                Type svcType = Type.GetType(svc.Type);
                if (svcType == null)
                {
                    throw new Exception("Invalid Service Type " + svc.Type + " in configuration file.");
                }
                OpenHost(svcType);
            }
        }

        /// <summary>
        /// 关闭所有的配置服务
        /// </summary>
        public static void CloseAllServices()
        {
            foreach (ServiceHost hst in hosts)
            {
                hst.Close();
            }
        }


    }
}
