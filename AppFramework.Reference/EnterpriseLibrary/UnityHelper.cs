//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UnityHelper.cs
// 描述: IOC 操作类
// 作者：ChenJie 
// 编写日期：2016-07-26
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;

namespace AppFramework.Reference.EnterpriseLibrary
{
    /// <summary>
    /// IOC 操作类
    /// </summary>
    public sealed class UnityHelper
    {
        /// <summary>
        /// 通用业务逻辑 IOC 配置文件
        /// </summary>
        private const string UNITY_FIELE_NAME_OF_BUSINESS_LOGIC = @"EnterpriseLibrary\SystemConfigFiles\BusinessLoigc.config";

        #region 只读静态常量
        
        private static readonly Hashtable containers = Hashtable.Synchronized(new Hashtable());
        private static readonly UnityConfigurationSection unitySection;

        #endregion

        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static UnityHelper()
        {
            try
            {
                string configPath = AppDomain.CurrentDomain.BaseDirectory + UNITY_FIELE_NAME_OF_BUSINESS_LOGIC;
                if (!string.IsNullOrWhiteSpace(configPath))
                {
                    ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                    file.ExeConfigFilename = configPath;
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                    unitySection = (UnityConfigurationSection)config.GetSection("unity");
                }
                else
                {
                    throw new Exception("反转控制配置文件路径为空.");
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常，不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicyWithLog(exception);
            }
        }

        #endregion

        #region 静态函数

        /// <summary>
        /// 利用配置文件创建 IOC 容器
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>IOC 容器</returns>
        public static IUnityContainer GetUnityContainer(string key)
        {
            IUnityContainer container = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(key))
                {
                    if (containers.ContainsKey(key))
                    {
                        container = containers[key] as IUnityContainer;
                    }
                    else
                    {
                        container = new UnityContainer();
                        unitySection.Configure(container, key);
                        containers[key] = container;
                    }

                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return container;
        }

        #endregion
    }
}
