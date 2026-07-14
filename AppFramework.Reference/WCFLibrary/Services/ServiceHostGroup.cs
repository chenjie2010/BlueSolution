//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ServiceHostGroup.cs
// 描述: WCF Host处理多个服务的通用类
// 作者：ChenJie 
// 编写日期：2016-07-27
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Configuration;
using System.ServiceModel.Configuration;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Channels;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// WCF Host处理多个服务的通用类
    /// </summary>
    public sealed class ServiceHostGroup
    {
        #region 私有变量

        /// <summary>
        /// 集合
        /// </summary>
        private List<ServiceHost> hosts = new List<ServiceHost>();

        #endregion

        #region 只读变量

        private readonly string serveiceAssemblyName;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serveiceAssemblyName"></param>
        public ServiceHostGroup(string serveiceAssemblyName)
        {
            this.serveiceAssemblyName = serveiceAssemblyName;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 根据配置文件将所有的服务的host打开
        /// </summary>
        public void StartAllServices()
        {           
            Configuration conf = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            ServiceModelSectionGroup svcmod = (ServiceModelSectionGroup)conf.GetSectionGroup("system.serviceModel");            
            foreach (ServiceElement el in svcmod.Services.Services)
            {
                //获取一个外部私有程序集中某个类型的元数据
                //字符串参数格式为类型的权限定名，紧跟着包含类型的程序集的友好名(friendly name)，以逗号分隔
                Type svcType = Type.GetType(el.Name + ", " + serveiceAssemblyName);
                if (svcType == null)
                {
                    throw new Exception("Invalid Service Type " + el.Name + " in configuration file.");
                }
                OpenHost(svcType);
            }
        }

        /// <summary>
        /// 关闭所有的服务
        /// </summary>
        public void StopAllServices()
        {
            foreach (ServiceHost hst in hosts)
            {
                hst.Close();
            }
        }

        /// <summary>
        /// 打印主机信息
        /// </summary>
        public void PrintHostInfo()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Start:");
            Console.WriteLine("-------------------------------------------------");

            foreach (ServiceHost hst in hosts)
            {
                //显示运行状态
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Host is runing! and state is {0}", hst.State);

                //打印终结点信息
                Console.WriteLine("Endpoints count is {0}", hst.Description.Endpoints.Count);
                Console.ForegroundColor = ConsoleColor.Yellow;
                foreach (ServiceEndpoint se in hst.Description.Endpoints)
                {
                    Console.WriteLine("[EP]: {0},[A]: {1}, [B]: {2}, [C]: {3}",
                        se.Name, se.Address, se.Binding.Name, se.Contract.Name);
                }
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("-------------------------------------------------");
            }
        }
        
        #endregion

        #region 私有方法
        
        /// <summary>
        /// 根据对应的服务类将相应的 host 打开
        /// </summary>
        /// <param name="t"></param>
        private void OpenHost(Type t)
        {
            try
            {
                ServiceHost hst = new ServiceHost(t);
                if (hst.Description.Endpoints.Count < 1)
                {
                    hst.AddDefaultEndpoints();
                }
                hst.Open();
                hosts.Add(hst);
            }            
            catch (Exception exception)
            {
                throw exception;
            }
        }

        #endregion


    }
}
