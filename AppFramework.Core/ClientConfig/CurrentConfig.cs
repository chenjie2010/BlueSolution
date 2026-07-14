//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CurrentConfig.cs
// 描述: 保存当前配置信息
// 作者：ChenJie 
// 编写日期：2010-07-13
// 版权所有 (C) 四川大学 2010
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppFramework.Core.ClientConfig
{
    /// <summary>
    /// 当前的配置信息
    /// </summary>
    public class CurrentConfig
    {
        #region 内部成员变量
        private string _serverAddress;
        private string _port;
        private bool _state;
        private SoftwareVersion _softwareVersion;
        #endregion

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        CurrentConfig()
        {
            ServerAddressConfig serverAddressConfig = ServerAddressConfigHelper.GetConfigInfo();
            _serverAddress = serverAddressConfig.ServerAddress;
            _port = serverAddressConfig.Port;
            _state = serverAddressConfig.State;
            _softwareVersion = SoftwareVersion.UnKown;
        }
        #endregion

        #region 嵌套类
        class Nested
        {
            static Nested()
            {
            }
            internal static readonly CurrentConfig instance = new CurrentConfig();
        }
        #endregion

        #region 属性
        /// <summary>
        /// 唯一实例
        /// </summary>
        public static CurrentConfig Instance
        {
            get
            {
                return Nested.instance;
            }
        }

        /// <summary>
        /// 当前服务器端地址
        /// </summary>
        public string ServerAddress
        {
            get
            {
                return _serverAddress;
            }
            set
            {
                _serverAddress = value;
            }
        }

        /// <summary>
        /// 当前服务器端端口
        /// </summary>
        public string Port
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        /// <summary>
        /// 当前服务器端连接状态
        /// </summary>
        public bool State
        {
            get
            {
                return _state;
            }
            set
            {
                _state = value;
            }
        }

        /// <summary>
        /// 当前服务器端版本
        /// </summary>
        public SoftwareVersion SoftwareVersion
        {
            get
            {
                return _softwareVersion;
            }
            set
            {
                _softwareVersion = value;
            }
        }

        
        #endregion
    }
}
