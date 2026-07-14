//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: RemoteChannelFactory.cs
// 描述: 远程通平台通道创建工厂类
// 作者：ChenJie 
// 编写日期：2018/10/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 远程通平台通道创建工厂类
    /// </summary>
    public sealed class RemoteChannelFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static RemoteChannelFactory()
        {

        }

        #endregion

        #region 静态方法
        
        /// <summary>
        /// 根据给定地址来创建 IRemoteServerContract 代理对象
        /// </summary>
        /// <param name="serverAddress"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public static IRemoteServerContract CreateRemoteServerContract(string serverAddress, string port)
        {
            ChannelFactory<IRemoteServerContract> channelFactory = ChannelFactoryCreator.Create<IRemoteServerContract>(serverAddress, port, "RemoteServerService");
            IRemoteServerContract remoteDataContract = channelFactory.CreateChannel();

            return remoteDataContract;
        }

        #endregion

    }
}
