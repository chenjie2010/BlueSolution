//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： UserInfoConfigOperation.cs
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
    /// 用户配置文件操作类
    /// </summary>
    public class UserInfoConfigHelper
    {
        /// <summary>
        /// 创建配置文件中用户设置的组
        /// </summary>
        public static void CreateDefaultConfigInfo()
        {
            UserInfoConfigParentNode clientConfigParentNode = new UserInfoConfigParentNode();

            CustomConfigure<UserInfoConfigParentNode> customConfigure = new CustomConfigure<UserInfoConfigParentNode>();
            customConfigure.DefaultValue = "";
            customConfigure.Group = clientConfigParentNode;
            ConfigOperation.WirteDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
                SysConfigConstant.USER_NAME_LIST_CONFIG, customConfigure);
        }

        /// <summary>
        /// 获得节点的默认名称
        /// </summary>
        /// <returns>默认名称</returns>
        public static string GetDefaultValue()
        {
            string vaule = string.Empty;

            CustomConfigure<UserInfoConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
                SysConfigConstant.USER_NAME_LIST_CONFIG) as CustomConfigure<UserInfoConfigParentNode>;
            if (customConfigure != null)
            {
                vaule = customConfigure.DefaultValue;
            }

            return vaule;
        }

        /// <summary>
        /// 删除配置文件中用户设置的组
        /// </summary>
        /// <returns></returns>
        public static bool RemoveSystemConfigInfo()
        {
            bool success = false;

            Configuration config;
            CustomConfigure<UserInfoConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
                SysConfigConstant.USER_NAME_LIST_CONFIG, out config) as CustomConfigure<UserInfoConfigParentNode>;
          
            if (config != null && config.HasFile)
            {
                config.Sections.Remove(SysConfigConstant.USER_NAME_LIST_CONFIG);
                config.Save(ConfigurationSaveMode.Minimal);
            }

            return success;
        }

        /// <summary>
        /// 获得系统配置信息中的默认对象
        /// </summary>
        /// <returns></returns>
        public static UserInfoConfig GetConfigInfo()
        {
            UserInfoConfig userConfig = null;
            CustomConfigure<UserInfoConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
                SysConfigConstant.USER_NAME_LIST_CONFIG) as CustomConfigure<UserInfoConfigParentNode>;

            UserInfoConfigParentNode clientConfigParentNode = customConfigure.Group;
            if (clientConfigParentNode != null)
            {
                UserInfoConfig userConfigTemp = clientConfigParentNode[customConfigure.DefaultValue];
                if (userConfigTemp != null)
                {
                    userConfig = new UserInfoConfig(userConfigTemp.UserName, userConfigTemp.UserPassword, userConfigTemp.RemeberPassword,
                        userConfigTemp.AutoLogon, userConfigTemp.LogonState);
                }
            }

            return userConfig;
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
            CustomConfigure<UserInfoConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
               SysConfigConstant.USER_NAME_LIST_CONFIG, out config) as CustomConfigure<UserInfoConfigParentNode>;
            {
                UserInfoConfigParentNode clientConfigParentNode = customConfigure.Group;
                if (clientConfigParentNode != null)
                {
                    foreach (UserInfoConfig userConfig in clientConfigParentNode)
                    {
                        if (userConfig.UserName.CompareTo(defaultValue) == 0)
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
        /// <param name="userConfig"></param>
        /// <returns></returns>
        public static bool AddConfigInfo(UserInfoConfig userConfig)
        {
            bool success = false;
            Configuration config;

            CustomConfigure<UserInfoConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
              SysConfigConstant.USER_NAME_LIST_CONFIG, out config) as CustomConfigure<UserInfoConfigParentNode>;
            if (config != null && config.HasFile)
            {
                UserInfoConfigParentNode clientConfigParentNode = customConfigure.Group;
                if (clientConfigParentNode != null)
                {
                    if (clientConfigParentNode.IndexOf(userConfig) >= 0)
                    {
                        RemoveConfigInfo(userConfig);
                    }
                    clientConfigParentNode.Add(userConfig);
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
        public static bool RemoveConfigInfo(UserInfoConfig configInfo)
        {
            bool success = false;
            Configuration config;

            CustomConfigure<UserInfoConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
              SysConfigConstant.USER_NAME_LIST_CONFIG, out config) as CustomConfigure<UserInfoConfigParentNode>;
            if (config != null && config.HasFile)
            {
                if (customConfigure != null)
                {
                    UserInfoConfigParentNode UserInfoConfigParentNode = customConfigure.Group;
                    if (UserInfoConfigParentNode != null)
                    {
                        foreach (UserInfoConfig userConfig in UserInfoConfigParentNode)
                        {
                            if (userConfig.UserName.CompareTo(configInfo.UserName) == 0)
                            {
                                UserInfoConfigParentNode.Remove(userConfig.UserName);
                                config.Save();
                                success = true;
                                break;
                            }
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
            CustomConfigure<UserInfoConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
             SysConfigConstant.USER_NAME_LIST_CONFIG, out config) as CustomConfigure<UserInfoConfigParentNode>;
            if (config != null && config.HasFile)
            {
                UserInfoConfigParentNode clientConfigParentNode = customConfigure.Group;
                if (clientConfigParentNode != null)
                {
                    foreach (UserInfoConfig userConfig in clientConfigParentNode)
                    {
                        if (userConfig.UserName.CompareTo(key) == 0)
                        {
                            clientConfigParentNode.Remove(userConfig.UserName);
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
        public static IList<UserInfoConfig> GetConfigInfoList()
        {
            IList<UserInfoConfig> userConfigs = new List<UserInfoConfig>();
            CustomConfigure<UserInfoConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
               SysConfigConstant.USER_NAME_LIST_CONFIG) as CustomConfigure<UserInfoConfigParentNode>;
            if ((customConfigure) != null && (customConfigure.Group != null))
            {
                UserInfoConfigParentNode clientConfigParentNode = customConfigure.Group;
                foreach (UserInfoConfig userConfig in clientConfigParentNode)
                {
                    userConfigs.Add(new UserInfoConfig(userConfig.UserName, userConfig.UserPassword, userConfig.RemeberPassword,
                        userConfig.AutoLogon, userConfig.LogonState));
                }
            }
            return userConfigs;
        }

        /// <summary>
        /// 通过关键字获得系统配置信息中的对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static UserInfoConfig GetConfigInfo(string key)
        {
            UserInfoConfig userConfig = null;
            CustomConfigure<UserInfoConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
               SysConfigConstant.USER_NAME_LIST_CONFIG) as CustomConfigure<UserInfoConfigParentNode>;

            UserInfoConfigParentNode clientConfigParentNode = customConfigure.Group;
            if (clientConfigParentNode != null)
            {
                foreach (UserInfoConfig userConfigTemp in clientConfigParentNode)
                {
                    if (userConfigTemp.UserName.CompareTo(key) == 0)
                    {
                        userConfig = new UserInfoConfig(userConfigTemp.UserName, userConfigTemp.UserPassword,
                            userConfigTemp.RemeberPassword, userConfigTemp.AutoLogon, userConfigTemp.LogonState);
                        break;
                    }
                }
            }
            return userConfig;
        }

        /// <summary>
        /// 修改系统配置信息的组的对象
        /// </summary>
        /// <param name="userConfig"></param>
        /// <returns></returns>
        public static bool ModifyConfigInfo(UserInfoConfig userConfig)
        {
            bool success = false;
            Configuration config;

            CustomConfigure<UserInfoConfigParentNode> customConfigure = ConfigOperation.ReadDataUnderRootNode(SysConfigConstant.SYSTEM_CONFIG_NAME,
              SysConfigConstant.USER_NAME_LIST_CONFIG, out config) as CustomConfigure<UserInfoConfigParentNode>;
            if (config != null && config.HasFile)
            {
                UserInfoConfigParentNode clientConfigParentNode = customConfigure.Group;
                if (clientConfigParentNode != null)
                {
                    foreach (UserInfoConfig userConfigTmep in clientConfigParentNode)
                    {
                        if (userConfigTmep.UserName.CompareTo(userConfig.UserName) == 0)
                        {
                            userConfigTmep.Update(userConfig);
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
