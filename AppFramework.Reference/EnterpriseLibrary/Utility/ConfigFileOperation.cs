//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ConfigFileOperation.cs
// 描述: 企业库配置文件操作类
// 作者：ChenJie 
// 编写日期：2016-06-29
// 版权所有 (C) 四川大学 2010
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace AppFramework.Reference.EnterpriseLibrary
{
    /// <summary>
    /// 企业库配置文件操作类
    /// </summary>
    public sealed class ConfigFileOperation
    {
        /// <summary>
        /// 获得配置源文件路径
        /// </summary>
        /// <param name="configSource">配置源名称</param>
        /// <param name="baseDirectory">根目录</param>
        /// <returns></returns>
        public static string GetConfigPath(string configSource, string baseDirectory)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(baseDirectory);
            if (!baseDirectory.EndsWith(@"\"))
            {
                sb.Append(@"\");
            }

            using (SystemConfigurationSource systemConfigurationSource = new SystemConfigurationSource())
            {
                ConfigurationSourceSection section = (ConfigurationSourceSection)systemConfigurationSource.GetSection(ConfigurationSourceSection.SectionName);
                FileConfigurationSourceElement elem = (FileConfigurationSourceElement)section.Sources.Get(configSource);                
                sb.Append(elem.FilePath);
            }

            return sb.ToString();
        }

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

            return configPath;
        }

        /// <summary>
        /// 将 XML 日志文件的相对路径更换为绝对路径
        /// </summary>
        /// <param name="configurationSource"></param>
        public static void AlterRelateviePalthofLogFiles(IConfigurationSource configurationSource)
        {
            ConfigurationSection configurationSection = configurationSource.GetSection("loggingConfiguration");
            LoggingSettings loggingSettings = configurationSection as LoggingSettings;
            foreach (TraceListenerData traceListenerData in loggingSettings.TraceListeners)
            {
                if (traceListenerData is XmlTraceListenerData)
                {
                    XmlTraceListenerData xmlTraceListenerData = (XmlTraceListenerData)traceListenerData;
                    xmlTraceListenerData.FileName = AppDomain.CurrentDomain.BaseDirectory + xmlTraceListenerData.FileName;
                }
            }
        }
    }
}
