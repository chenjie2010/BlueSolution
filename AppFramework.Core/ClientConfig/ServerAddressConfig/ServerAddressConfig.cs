//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ServerAddressConfig.cs
// 描述： 客户端配置类
// 作者：ChenJie 
// 编写日期：2016-08-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Configuration;
using System.ComponentModel;

namespace AppFramework.Core.ClientConfig
{
    /// <summary>
    /// 服务器地址配置类
    /// </summary>
    [Serializable]
    public class ServerAddressConfig : ConfigurationElement
    {
        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public ServerAddressConfig()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="serverName">名称</param>  
        public ServerAddressConfig(string serverName)
        {
            ServerName = serverName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="name">节点名称</param>
        /// <param name="serverAddress">服务器地址</param>
        /// <param name="port">端口</param>
        /// <param name="state">连接状态</param>
        public ServerAddressConfig(string serverName, string serverAddress, string port, bool state)
        {
            ServerName = serverName;
            ServerAddress = serverAddress;
            Port = port;
            State = state;
        }

        #endregion

        #region 属性
        /// <summary>
        /// 服务器名称
        /// </summary>
        [ConfigurationProperty("ServerName", IsRequired = true, IsKey = true)]
        public string ServerName
        {
            get
            {
                return (string)this["ServerName"];
            }
            set
            {
                this["ServerName"] = value;
            }
        }

        /// <summary>
        /// 服务器地址
        /// </summary>
        [ConfigurationProperty("ServerAddress", IsRequired = true)]
        public string ServerAddress
        {
            get
            {
                return (string)this["ServerAddress"];
            }
            set
            {
                this["ServerAddress"] = value;
            }
        }

        /// <summary>
        /// 端口
        /// </summary>
        [ConfigurationProperty("Port", IsRequired = true)]
        public string Port
        {
            get
            {
                return (string)this["Port"];
            }
            set
            {
                this["Port"] = value;
            }
        }

        /// <summary>
        /// 连接状态
        /// </summary>
        [ConfigurationProperty("State", IsRequired = true, DefaultValue = "false")]
        public bool State
        {
            get
            {
                return (bool)this["State"];
            }
            set
            {
                this["State"] = value;
            }
        }
        
         #endregion

        #region 方法
        /// <summary>
        /// 更新对象的值
        /// </summary>
        /// <param name="clientConfig">客户端配置</param>
        public void Update(ServerAddressConfig clientConfig)
        {
            ServerName = clientConfig.ServerName;
            ServerAddress = clientConfig.ServerAddress;
            Port = clientConfig.Port;
            State = clientConfig.State;
        }

        /// <summary>
        /// 重载
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return ServerName;
        }
        #endregion
    }
}
