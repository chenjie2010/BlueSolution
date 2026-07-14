//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ServerAddressConfigOperation.cs
// 描述： 系统配置操作类
// 作者：ChenJie 
// 编写日期：2016-08-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace AppFramework.Core.ClientConfig
{
    /// <summary>
    /// 
    /// </summary>
    public class ServerAddressConfigHelper
    {
        //private const string DEFAULT_USER_NAME;

        /// <summary>
        /// 创建配置文件中用户设置的组
        /// </summary>
        public static void CreateDefaultConfigInfo()
        {
            ServerAddressConfig clientConfig = new ServerAddressConfig("Default", "127.0.0.1", "8090", false);
            ServerAddressConfigParentNode clientConfigParentNode = new ServerAddressConfigParentNode();
            clientConfigParentNode.Add(clientConfig);


            CustomConfigure<ServerAddressConfigParentNode> customConfigure = new CustomConfigure<ServerAddressConfigParentNode>();
            customConfigure.DefaultValue = "Default";
            customConfigure.Group = clientConfigParentNode;
            ConfigOperation.WirteDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
                SysConfigConstant.SERVER_ADDRESS_CONFIG, customConfigure);
        }

        /// <summary>
        /// 删除配置文件中用户设置的组
        /// </summary>
        /// <returns></returns>
        public static bool RemoveSystemConfigInfo()
        {
            bool success = false;

            Configuration config;
            CustomConfigure<ServerAddressConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
                SysConfigConstant.SERVER_ADDRESS_CONFIG, out config) as CustomConfigure<ServerAddressConfigParentNode>;
            if (config != null && config.HasFile)
            {
                config.Sections.Remove(SysConfigConstant.SERVER_ADDRESS_CONFIG);
            }

            return success;
        }

        /// <summary>
        /// 获得系统配置信息中的默认对象
        /// </summary>
        /// <returns></returns>
        public static ServerAddressConfig GetConfigInfo()
        {
            ServerAddressConfig clientConfig = null;
            CustomConfigure<ServerAddressConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
                SysConfigConstant.SERVER_ADDRESS_CONFIG) as CustomConfigure<ServerAddressConfigParentNode>;

            ServerAddressConfigParentNode clientConfigParentNode = customConfigure.Group;
            if (clientConfigParentNode != null)
            {
                clientConfig = clientConfigParentNode[customConfigure.DefaultValue];
            }

            return clientConfig;
        }

        /// <summary>
        /// 修改系统配置信息的组的默认值
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ModifyDefaultValueOfSystemConfigInfo(string defaultValue)
        {
            bool success = false;
            Configuration config;
            CustomConfigure<ServerAddressConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
               SysConfigConstant.SERVER_ADDRESS_CONFIG, out config) as CustomConfigure<ServerAddressConfigParentNode>;
            {
                ServerAddressConfigParentNode clientConfigParentNode = customConfigure.Group;
                if (clientConfigParentNode != null)
                {
                    foreach (ServerAddressConfig clientConfig in clientConfigParentNode)
                    {
                        if (clientConfig.ServerName.CompareTo(defaultValue) == 0)
                        {
                            customConfigure.DefaultValue = defaultValue;
                            config.Save();
                            success = true;
                            break;
                        }
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// 增加系统配置信息的组的对象
        /// </summary>
        /// <param name="clientConfig"></param>
        /// <returns></returns>
        public static bool AddConfigInfo(ServerAddressConfig clientConfig)
        {
            bool success = false;
            Configuration config;

            CustomConfigure<ServerAddressConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
              SysConfigConstant.SERVER_ADDRESS_CONFIG, out config) as CustomConfigure<ServerAddressConfigParentNode>;
            if (config != null && config.HasFile)
            {
                ServerAddressConfigParentNode clientConfigParentNode = customConfigure.Group;
                if (clientConfigParentNode != null)
                {
                    /*如果对象不存在则增加在末尾，存在则删除后增加到末尾*/
                    if (clientConfigParentNode.IndexOf(clientConfig) >= 0)
                    {
                        clientConfigParentNode.Remove(clientConfig);
                    }
                    clientConfigParentNode.Add(clientConfig);

                    config.Save();
                    success = true;
                }
            }

            return success;

        }

        /// <summary>
        /// 删除系统配置信息的组的对象
        /// </summary>
        /// <param name="configInfo"></param>
        /// <returns></returns>
        public static bool RemoveConfigInfo(ServerAddressConfig configInfo)
        {
            bool success = false;
            Configuration config;

            CustomConfigure<ServerAddressConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
              SysConfigConstant.SERVER_ADDRESS_CONFIG, out config) as CustomConfigure<ServerAddressConfigParentNode>;
            if (config != null && config.HasFile)
            {
                ServerAddressConfigParentNode ServerAddressConfigParentNode = customConfigure.Group;
                if (ServerAddressConfigParentNode != null)
                {
                    foreach (ServerAddressConfig clientConfig in ServerAddressConfigParentNode)
                    {
                        if (clientConfig.ServerName.CompareTo(configInfo.ServerName) == 0)
                        {
                            ServerAddressConfigParentNode.Remove(clientConfig.ServerName);
                            config.Save();
                            success = true;
                            break;
                        }
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// 通过对象的关键字删除系统配置信息的组的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static bool RemoveConfigInfoByKey(string key)
        {
            bool success = false;
            Configuration config;
            CustomConfigure<ServerAddressConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
             SysConfigConstant.SERVER_ADDRESS_CONFIG, out config) as CustomConfigure<ServerAddressConfigParentNode>;
            if (config != null && config.HasFile)
            {
                ServerAddressConfigParentNode clientConfigParentNode = customConfigure.Group;
                if (clientConfigParentNode != null)
                {
                    foreach (ServerAddressConfig clientConfig in clientConfigParentNode)
                    {
                        if (clientConfig.ServerName.CompareTo(key) == 0)
                        {
                            clientConfigParentNode.Remove(clientConfig.ServerName);
                            config.Save();
                            success = true;
                            break;
                        }
                    }
                }
            }

            return success;
        }

        /// <summary>
        /// 获得系统配置信息中的对象列表
        /// </summary>
        /// <returns></returns>
        public static IList<ServerAddressConfig> GetConfigInfoList()
        {
            IList<ServerAddressConfig> userConfigs = new List<ServerAddressConfig>();
            CustomConfigure<ServerAddressConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
               SysConfigConstant.SERVER_ADDRESS_CONFIG) as CustomConfigure<ServerAddressConfigParentNode>;
            if ((customConfigure) != null && (customConfigure.Group != null))
            {
                ServerAddressConfigParentNode clientConfigParentNode = customConfigure.Group;
                foreach (ServerAddressConfig clientConfig in clientConfigParentNode)
                {
                    userConfigs.Add(new ServerAddressConfig(clientConfig.ServerName, clientConfig.ServerAddress,
                        clientConfig.Port, clientConfig.State));
                }
            }
            return userConfigs;
        }

        /// <summary>
        /// 通过关键字获得系统配置信息中的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static ServerAddressConfig GetConfigInfo(string key)
        {
            ServerAddressConfig clientConfig = null;
            CustomConfigure<ServerAddressConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
               SysConfigConstant.SERVER_ADDRESS_CONFIG) as CustomConfigure<ServerAddressConfigParentNode>;

            ServerAddressConfigParentNode clientConfigParentNode = customConfigure.Group;
            if (clientConfigParentNode != null)
            {
                foreach (ServerAddressConfig clientConfigTemp in clientConfigParentNode)
                {
                    if (clientConfigTemp.ServerName.CompareTo(key) == 0)
                    {
                        clientConfig = new ServerAddressConfig(clientConfigTemp.ServerName, clientConfigTemp.ServerAddress, clientConfig.Port, clientConfig.State);
                        break;
                    }
                }
            }
            return clientConfig;
        }

        /// <summary>
        /// 修改系统配置信息的组的对象
        /// </summary>
        /// <param name="clientConfig"></param>
        /// <returns></returns>
        public static bool ModifyConfigInfo(ServerAddressConfig clientConfig)
        {
            bool success = false;
            Configuration config;

            CustomConfigure<ServerAddressConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
              SysConfigConstant.SERVER_ADDRESS_CONFIG, out config) as CustomConfigure<ServerAddressConfigParentNode>;
            if (config != null && config.HasFile)
            {
                ServerAddressConfigParentNode clientConfigParentNode = customConfigure.Group;
                if (clientConfigParentNode != null)
                {
                    foreach (ServerAddressConfig clientConfigTemp in clientConfigParentNode)
                    {
                        if (clientConfig.ServerName.CompareTo(clientConfigTemp.ServerName) == 0)
                        {
                            clientConfigTemp.Update(clientConfig);
                            config.Save();
                            success = true;
                            break;
                        }
                    }
                }
            }

            return success;
        }
    }

}
