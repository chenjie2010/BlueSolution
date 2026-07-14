//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SysConfigureConstant.cs
// 描述： 配置文件常量类
// 作者：ChenJie 
// 编写日期：2016-08-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core.ClientConfig
{
    /// <summary>
    /// 配置文件常量类
    /// </summary>
    public class SysConfigConstant
    {
        /// <summary>
        /// 配置文件名称
        /// </summary>
        public const string SYSTEM_CONFIG_NAME = @"ClientConfig\Client.config";

        /// <summary>
        /// 用户组的名称
        /// </summary>
        public const string USER_NAME_LIST_CONFIG = "UserInfoConfig";

        /// <summary>
        /// 客户端的服务器地址配置
        /// </summary>
        public const string SERVER_ADDRESS_CONFIG = "ServerAddressConfig";

    }
}
