//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ChannelFactoryCreator.cs
// 描述: 自定义需要验证的 Channel 工厂类
// 作者：ChenJie 
// 编写日期：2016-07-27
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Text;
using System.Collections;
using System.ServiceModel;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// 自定义需要验证的 Channel 工厂类
    /// </summary>
    public class ChannelFactoryCreator
    {
        #region 私有变量

        private static Hashtable channelFactories = new Hashtable();

        #endregion

        #region 成员变量

        /// <summary>
        /// 用户名
        /// </summary>
        private static string _userName = string.Empty;

        /// <summary>
        /// 密码
        /// </summary>
        private static string _password = string.Empty;

        /// <summary>
        /// 服务器地址
        /// </summary>
        private static string _serverAddress;

        /// <summary>
        /// 端口号
        /// </summary>
        private static string _port;

        #endregion

        #region 属性

        /// <summary>
        /// 用户名
        /// </summary>
        public static string UserName
        {
            get
            {
                return _userName;
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        public static string Password
        {
            get
            {
                return _password;
            }
        }

        /// <summary>
        /// 服务器地址
        /// </summary>
        public static string ServerAddress
        {
            get
            {
                return _serverAddress;
            }
        }

        /// <summary>
        /// 端口号
        /// </summary>
        private static string Port
        {
            get
            {
                return _port;
            }
        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 设置用户名和密码
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        public static void SetUserNameAndPassword(string userName, string password)
        {
            _userName = userName;
            _password = password;
            channelFactories.Clear();
        }


        /// <summary>
        /// 设置服务器地址和端口地址
        /// </summary>
        /// <param name="serverAddress">服务器地址</param>
        /// <param name="port">端口地址</param>
        public static void SetServerAddressAndPort(string serverAddress, string port)
        {
            _serverAddress = serverAddress;
            _port = port;
            channelFactories.Clear();
        }

        /// <summary>
        /// 清空通道缓存
        /// </summary>
        public static void ClearChannelFactories()
        {
            channelFactories.Clear();
        }

        /// <summary>
        /// 获得 Uri 字符串
        /// </summary>
        /// <param name="scheme">协议方案</param>
        /// <param name="serverAddress">服务器地址</param>
        /// <param name="port">WCF 服务的端口</param>
        /// <param name="relativeAddress">服务器端的服务的相对基地址</param>
        /// <returns> Uri 字符串</returns>
        public static string GetUriString(string scheme, string serverAddress, string port, string relativeAddress)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(scheme);
            sb.Append("://");
            sb.Append(serverAddress);
            sb.Append(":");
            sb.Append(port);
            sb.Append(relativeAddress);

            return sb.ToString();
        }

        /// <summary>
        /// 根据默认地址来创建代理对象
        /// </summary>
        /// <param name="endpointNameInClient">客户端终结点名称</param>        
        /// <returns></returns>
        public static ChannelFactory<T> Create<T>(string endpointNameInClient)
        {
            ChannelFactory<T> channelFactory = null;

            if (string.IsNullOrEmpty(endpointNameInClient))
            {
                throw new ArgumentNullException("endpointName");
            }

            if (channelFactories.ContainsKey(endpointNameInClient))
            {
                channelFactory = channelFactories[endpointNameInClient] as ChannelFactory<T>;
            }

            if (channelFactory == null)
            {
                channelFactory = new ChannelFactory<T>(endpointNameInClient);
                if (!string.IsNullOrWhiteSpace(_userName))
                {
                    channelFactory.Credentials.UserName.UserName = _userName;
                    channelFactory.Credentials.UserName.Password = _password;
                }

                Uri uri = new Uri(GetUriString(channelFactory.Endpoint.ListenUri.Scheme, _serverAddress, _port, channelFactory.Endpoint.ListenUri.AbsolutePath));
                channelFactory.Endpoint.Address = new EndpointAddress(uri, channelFactory.Endpoint.Address.Identity, channelFactory.Endpoint.Address.Headers);

                lock (channelFactories.SyncRoot)
                {
                    channelFactories[endpointNameInClient] = channelFactory;
                }
            }

            return channelFactory;
        }

        /// <summary>
        /// 根据指定的地址来创建代理对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serverAddress"></param>
        /// <param name="port"></param>
        /// <param name="endpointNameInClient"></param>
        /// <returns></returns>
        public static ChannelFactory<T> Create<T>(string serverAddress, string port, string endpointNameInClient)
        {
            ChannelFactory<T> channelFactory = new ChannelFactory<T>(endpointNameInClient);

            if (string.IsNullOrEmpty(endpointNameInClient))
            {
                throw new ArgumentNullException("endpointName");
            }

            Uri uri = new Uri(GetUriString(channelFactory.Endpoint.ListenUri.Scheme, serverAddress, port, channelFactory.Endpoint.ListenUri.AbsolutePath));
            channelFactory.Endpoint.Address = new EndpointAddress(uri, channelFactory.Endpoint.Address.Identity, channelFactory.Endpoint.Address.Headers);

            lock (channelFactories.SyncRoot)
            {
                channelFactories[endpointNameInClient] = channelFactory;
            }

            return channelFactory;
        }

        /// <summary>
        /// 根据默认地址来创建代理对象
        /// </summary>
        /// <param name="context">实例上下文</param>
        /// <param name="endpointNameInClient">客户端终结点名称</param>        
        /// <returns></returns>
        public static DuplexChannelFactory<T> Create<T>(InstanceContext context, string endpointNameInClient)
        {
            DuplexChannelFactory<T> channelFactory = null;

            if (string.IsNullOrEmpty(endpointNameInClient))
            {
                throw new ArgumentNullException("endpointName");
            }
            if (context == null)
            {
                throw new ArgumentNullException("InstanceContext");
            }

            if (channelFactories.ContainsKey(endpointNameInClient))
            {
                channelFactory = channelFactories[endpointNameInClient] as DuplexChannelFactory<T>;
            }

            if (channelFactory == null)
            {
                channelFactory = new DuplexChannelFactory<T>(context, endpointNameInClient);
                if (!string.IsNullOrWhiteSpace(_userName))
                {
                    channelFactory.Credentials.UserName.UserName = _userName;
                    channelFactory.Credentials.UserName.Password = _password;
                }

                Uri uri = new Uri(GetUriString(channelFactory.Endpoint.ListenUri.Scheme, _serverAddress, _port, channelFactory.Endpoint.ListenUri.AbsolutePath));
                channelFactory.Endpoint.Address = new EndpointAddress(uri, channelFactory.Endpoint.Address.Identity, channelFactory.Endpoint.Address.Headers);

                lock (channelFactories.SyncRoot)
                {
                    channelFactories[endpointNameInClient] = channelFactory;
                }
            }

            return channelFactory;
        }

        #endregion
    }
}
