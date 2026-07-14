//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ConfigOperation.cs
// 描述： 配置文件常量类
// 作者：ChenJie 
// 编写日期：2016-08-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AppFramework.Core.ClientConfig
{
    /// <summary>
    /// .Net 操作自定义的配置文件(XML 文件)类
    /// </summary>
    public static class ConfigOperation
    {
        /// <summary>
        /// 在根节点下写入配置数据
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="configSectionName"></param>
        /// <param name="configurationSection"></param>
        /// <returns></returns>
        public static bool WirteDataUnderRootNode(string configFileName, string configSectionName, ConfigurationSection configurationSection)
        {
            bool createRootNode = false;
            try
            {
                ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                file.ExeConfigFilename = configFileName;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                if (config.HasFile)
                {
                    if (config.Sections[configSectionName] != null)
                    {
                        config.Sections.Remove(configSectionName);
                    }
                    config.Sections.Add(configSectionName, configurationSection);
                    config.Save(ConfigurationSaveMode.Minimal);
                    createRootNode = true;
                }
            }
            catch { }
            return createRootNode;
        }

        /// <summary>
        /// 在定义组节点下写入配置数据
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="configSectionName"></param>
        /// <param name="configurationSection"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static bool WirteDataUnderGroupNode(string configFileName, string configSectionName,
            ConfigurationSection configurationSection, string groupName)
        {
            bool createChildNode = false;
            try
            {
                ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                file.ExeConfigFilename = configFileName;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                if (config.HasFile)
                {
                    if (config.SectionGroups[groupName] != null)
                    {
                        config.SectionGroups.Remove(groupName);
                    }
                    config.SectionGroups.Add(groupName, new ConfigurationSectionGroup());
                    config.SectionGroups[groupName].Sections.Add(configSectionName, configurationSection);
                    config.Save(ConfigurationSaveMode.Minimal);
                    createChildNode = true;
                }

            }
            catch { }
            return createChildNode;
        }


        /// <summary>
        /// 从根节点下读取配置数据
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="configSectionName"></param>
        /// <returns></returns>
        public static ConfigurationSection ReadDataUnderRootNode(string configFileName, string configSectionName)
        {
            ConfigurationSection configurationSection = null;
            try
            {
                ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                file.ExeConfigFilename = configFileName;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                // 从根节读取
                if (config.HasFile)
                {
                    configurationSection = config.Sections[configSectionName] as ConfigurationSection;
                }
            }
            catch { }
            return configurationSection;
        }

        /// <summary>
        /// 从根节点下读取配置数据
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="configSectionName"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static ConfigurationSection ReadDataUnderRootNode(string configFileName, string configSectionName, out Configuration config)
        {
            ConfigurationSection configurationSection = null;
            config = null;
            ExeConfigurationFileMap file = new ExeConfigurationFileMap();
            file.ExeConfigFilename = configFileName;
            config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
            // 从根节读取
            if (config.HasFile)
            {
                configurationSection = config.Sections[configSectionName] as ConfigurationSection;
            }

            return configurationSection;
        }

        /// <summary>
        /// 从组节点下读取配置数据
        /// </summary>
        /// <param name="configFileName"></param>
        /// <param name="configSectionName"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        public static ConfigurationSection ReadDataUnderUnderGroupNode(string configFileName, string configSectionName, string groupName)
        {
            ConfigurationSection configurationSection = null;
            try
            {
                ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                file.ExeConfigFilename = configFileName;
                //从组节点读取
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                if (config.HasFile)
                {
                    configurationSection = config.SectionGroups[groupName].Sections[configSectionName] as ConfigurationSection;
                }
            }
            catch { }
            return configurationSection;
        } 
    }
}
