//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AppSettingHelper.cs
// 描述: 提供配置文件访问类
// 作者：ChenJie 
// 编写日期：2016-06-29
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AppFramework.Core;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;

namespace AppFramework.Reference.Utility
{
    class Platform
    {
        #region 成员变量

        private static PlatformState _platformState = PlatformState.UnKnown;

        #endregion

        #region 属性

        /// <summary>
        /// 当前使用的应用程序的平台
        /// </summary>
        public static PlatformState PlatformState
        {
            get
            {
                if (_platformState == PlatformState.UnKnown)
                {
                    if (HttpContext.Current != null)
                    {
                        //Web 应用程序
                        _platformState = PlatformState.WebForms;
                    }
                    else
                    {
                        //Windows 应用程序
                        _platformState = PlatformState.WinForms;
                    }
                }

                return _platformState;
            }
        }
        #endregion

        #region 静态方法

        /// <summary>
        /// 获得配置源文件路径
        /// </summary>
        /// <param name="configSource">配置源名称</param>
        /// <returns></returns>
        public static string GetConfigPath(string configSource)
        {
            string configPath = string.Empty;
            using (SystemConfigurationSource systemConfigurationSource = new SystemConfigurationSource())
            {
                ConfigurationSourceSection section = (ConfigurationSourceSection)systemConfigurationSource.GetSection(ConfigurationSourceSection.SectionName);
                FileConfigurationSourceElement elem = (FileConfigurationSourceElement)section.Sources.Get(configSource);
                configPath = AppDomain.CurrentDomain.BaseDirectory + elem.FilePath;
            }

            /*
            Configuration config = null;
            switch (platformState)
            {
                case PlatformState.WebForms:
                    config = WebConfigurationManager.OpenWebConfiguration("~");
                    break;

                case PlatformState.WinForms:
                    config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                    break;
            }
            ConfigurationSourceSection section = (ConfigurationSourceSection)config.GetSection(ConfigurationSourceSection.SectionName);
            FileConfigurationSourceElement elem = (FileConfigurationSourceElement)section.Sources.Get(configSource);
             */

            return configPath;
        }


        #endregion
    }
}
