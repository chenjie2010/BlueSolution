//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UnityContianer.cs
// 描述: IOC 容器用法实例类
// 作者：ChenJie 
// 编写日期：2016-07-26
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using AppFramework.Reference.EnterpriseLibrary;

namespace AppFramework.Reference.EnterpriseLibrary.Examples
{
    /// <summary>
    /// IOC 容器用法实例类
    /// </summary>
    public class UnityContianer
    {
        #region 常量

        private const string UNITY_FIELE_NAME = @"EnterpriseLibrary\AppConfigFiles\Unity.config";

        #endregion

        #region 只读静态常量

        private static readonly IUnityContainer _container;

        #endregion

        #region 属性

        public static IUnityContainer Container
        {
            get
            {
                return _container;
            }
        }

        #endregion


        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static UnityContianer()
        {
            try
            {
                _container = new UnityContainer();
                string configPath = AppDomain.CurrentDomain.BaseDirectory + UNITY_FIELE_NAME;
                if (!string.IsNullOrWhiteSpace(configPath))
                {
                    ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                    file.ExeConfigFilename = configPath;
                    Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                    UnityConfigurationSection unitySection = (UnityConfigurationSection)config.GetSection("unity");
                    unitySection.Configure(_container);
                }
                else
                {
                    throw new Exception("反转控制配置文件路径为空.");
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion
    }
}
