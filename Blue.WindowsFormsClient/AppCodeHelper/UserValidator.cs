//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: NetworkConnection.cs
// 描述: 网络连接测试类
// 作者：ChenJie 
// 编写日期：2016-08-07
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.Reflection;
using System.Configuration;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.Core.ClientConfig;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.WCFContracts;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.UserModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 用户登录验证处理类
    /// </summary>
    public class UserValidator
    {
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserValidator()
        {

        }

        /// <summary>
        /// 测试连接是否成功
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="result"></param>
        public void TestConnection(string serverAddress, string userName, string password, out bool result)
        {
            try
            {
                /*
                EndpointAddress address = new EndpointAddress("net.tcp://" + serverAddress + ":8086/UserNamePwdValidatorService");
                UserNamePwdValidatorClient userNamePwdValidator
                    = new UserNamePwdValidatorClient("UserNamePwdValidatorService", address);*/
                ChannelFactoryCreator.SetServerAddressAndPort(serverAddress, CurrentConfig.Instance.Port);
                ISystemConfigContract systemConfigContract = SystemChannelFactory.CreateSystemConfigContract();
                    result = systemConfigContract.TestConnection(userName, password);
            }
            catch
            {
                result = false;
            }
        }

        /// <summary>
        /// 测试连接是否成功 
        /// </summary>
        /// <param name="serverAddress">服务器地址</param>
        /// <param name="result">测试结果</param>
        public void TestConnection(string serverAddress, out bool result)
        {
            try
            {
                /*
                EndpointAddress address = new EndpointAddress("net.tcp://" + serverAddress + ":8086/UserNamePwdValidatorService");
                UserNamePwdValidatorClient userNamePwdValidator
                    = new UserNamePwdValidatorClient("UserNamePwdValidatorService", address);*/
                ChannelFactoryCreator.SetServerAddressAndPort(serverAddress, CurrentConfig.Instance.Port);
                ICommonUtilContract commonUtilContract = CommonFactory.CreateCommonUtilContract();
                commonUtilContract.TestConnection();
                result = true;
            }
            catch
            {
                result = false;
            }
        }

        /// <summary>
        /// 检查当前计算机时间
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <returns></returns>
        public bool CheckSystemTime(string serverAddress)
        {
            bool pass = false;

            /* 时间验证*/
            ChannelFactoryCreator.SetServerAddressAndPort(serverAddress, CurrentConfig.Instance.Port);
            ICommonUtilContract commonUtilContract = CommonFactory.CreateCommonUtilContract();
            int span = commonUtilContract.GetSystemDataTime(string.Empty).Subtract(DateTime.Now).Minutes;
            if (span >= 0 && span <= 2)
            {
                pass = true;
            }

            return pass;
        }

        /// <summary>
        /// 检查版本一致性
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <param name="clientVersion"></param>
        /// <returns></returns>
        public bool CheckVersionConsistency(string serverAddress, string clientVersion)
        {
            bool pass = false;

            /* 时间验证*/
            ChannelFactoryCreator.SetServerAddressAndPort(serverAddress, CurrentConfig.Instance.Port);
            ICommonUtilContract commonUtilContract = CommonFactory.CreateCommonUtilContract();
            pass = commonUtilContract.ValidateClientVersion(clientVersion);

            return pass;
        }

        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>如果提供的用户名和密码有效，则返回 true；否则返回 false</returns>
        public bool Validate(string username, string password)
        {
            bool result = true;

            try
            {
                ICommonUtilContract commonUtilContract = CommonFactory.CreateCommonUtilContract();
                result = commonUtilContract.ValidateUser(username, password);
                /*如果验证成功，初始化 CustomChannelFactory 的用户名和密码，更新用户登录的相关信息*/
                if (result)
                {
                    ChannelFactoryCreator.SetUserNameAndPassword(username, password);
                    IUserAccountContract userAccountContract = UserChannelFactory.CreateUserAccount();
                    IUserTypeContract userTypeContract = SystemChannelFactory.CreateUserTypeContract();
                    ICustomRoleContract customRoleContract = SystemChannelFactory.CreateCustomRoleContract();
                    ICustomMenuContract customMenuContract = BusinessChannelFactory.CreateCustomMenuContract();
                    ICustomBusinessContract customBusinessContract = BusinessChannelFactory.CreateCustomBusinessContract();
                    /*
                    UserClient user = new UserClient("UserService");
                    user.Endpoint.Address = new EndpointAddress(new Uri("net.tcp://" + serverAddress + ":8086/User"),
                        user.Endpoint.Address.Identity, user.Endpoint.Address.Headers);
                    user.ClientCredentials.UserName.UserName = "admin";
                    user.ClientCredentials.UserName.Password = "admin";
                    user.Insert();*/
                    CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(username);
                    decimal parentUserTypeId = userTypeContract.GetParentNodeId(commonUserInfo.UserTypeId);
                    //Int64 rolePropertyValue = customRoleContract.GetRoleProperty(commonUserInfo.UserId);
                    RoleAuthority roleAuthority = customRoleContract.GetRoleAuthority(commonUserInfo.UserId);
                    IList<CustomMenuInfo> customMenuInfos = customMenuContract.GetMenuClasses(commonUserInfo.UserId);
                    IList<ExtendedCustomBusinessInfo> extendedCustomBusinessInfos = customBusinessContract.GetBusiness(commonUserInfo.UserId);
                    MaxMinValue enumValue = UserEnumHelper.GetMaxAndMinValue(typeof(UserProperty));
                    if (parentUserTypeId >= enumValue.Min && parentUserTypeId <= enumValue.Max)
                    {
                        UserProperty userProperty = (UserProperty)parentUserTypeId;
                        CurrentUser.Instance.SetUserInfo(commonUserInfo, userProperty, roleAuthority, customMenuInfos, extendedCustomBusinessInfos);
                    }                    
                    else
                    {
                        CurrentUser.Instance.SetUserInfo(commonUserInfo, UserProperty.Others, roleAuthority, customMenuInfos, extendedCustomBusinessInfos);
                    }
                }
            }
            catch (Exception exception)
            {
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }

            return result;
        }

        /// <summary>
        /// 获得本机IP地址
        /// </summary>
        /// <returns>本机IP地址</returns>
        public IPAddress GetClinetIP()
        {
            IPAddress clinetIP = null;
            IPAddress[] address_list = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress ipAddress in address_list)
            {
                //IP地址正则表达式
                Regex r = new Regex(@"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
                if (r.IsMatch(ipAddress.ToString()))
                {
                    clinetIP = ipAddress;
                }
            }
            if (clinetIP == null)
            {
                clinetIP = address_list[0];
            }
            return clinetIP;
        }

        /// <summary>
        /// 在客户端配置文件中服务器的IP地址
        /// </summary>
        /// <param name="serverAddress">服务器的IP地址</param>
        /// <param name="port">服务器的端口</param>
        /// <param name="state">服务器的连接状态</param>
        public void ChangeIPAddress(string serverAddress, string port, bool state)
        {
            //修改 SystemConfigure.config 文件中服务器的IP地址
            ChangeClientConfigInSysConfig(serverAddress, state);

            //修改 App.config 文件中的服务器IP地址
            ChangeIPAddressInApp(serverAddress, port);
        }

        /// <summary>
        /// 在 App.confoig 文件中修改服务器的IP地址
        /// </summary>
        /// <param name="serverIPAddress">服务器IP地址</param>
        /// <param name="serverPort">服务器端口</param>
        private void ChangeIPAddressInApp(string serverIPAddress, string serverPort)
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            //Configuration configexe = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);

            ConfigurationSectionGroup sct = config.SectionGroups["system.serviceModel"];
            ServiceModelSectionGroup serviceModelSectionGroup = sct as ServiceModelSectionGroup;
            ClientSection clientSection = serviceModelSectionGroup.Client;

            foreach (ChannelEndpointElement item in clientSection.Endpoints)
            {
                string pattern = "://.*/";
                string address = item.Address.ToString();
                string replacement = string.Format("://{0}:{1}/", serverIPAddress, serverPort);
                address = Regex.Replace(address, pattern, replacement);
                item.Address = new Uri(address);
            }
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("system.serviceModel");
        }

        /// <summary>
        /// 修改 SystemConfigure.config 文件中服务器的IP地址
        /// </summary>
        /// <param name="serverIPAddress">服务器IP地址</param>
        /// <param name="state">服务器的连接状态</param>
        public void ChangeClientConfigInSysConfig(string serverAddress, bool state)
        {
            ServerAddressConfig serverAddressConfig = ServerAddressConfigHelper.GetConfigInfo();
            serverAddressConfig.ServerAddress = serverAddress;
            serverAddressConfig.State = state;
            ServerAddressConfigHelper.ModifyConfigInfo(serverAddressConfig);
        }
    }
}
