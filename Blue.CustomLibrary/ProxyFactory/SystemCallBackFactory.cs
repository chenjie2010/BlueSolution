//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CommonUtilFactory.cs
// 描述: 创建带有回调方法的代理工厂类
// 作者：ChenJie 
// 编写日期：2016/08/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.ServiceModel.Channels;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts;

namespace Blue.CustomLibrary
{
    /// <summary>
    /// 创建带有回调方法的代理工厂类
    /// </summary>
    public static class SystemCallBackFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static SystemCallBackFactory()
        {

        }

        #endregion     

        #region 静态方法
        
        /// <summary>
        /// 根据默认地址来创建 ISystemServiceContract 代理对象
        /// </summary>
        /// <param name="systemServiceCallBack"></param>
        /// <returns></returns>
        public static IDuplexChannelContract CreateDuplexChannelContract(IDuplexChannelCallBackContract duplexChannelCallBack)
        {
            InstanceContext context = new InstanceContext(duplexChannelCallBack);
            IDuplexChannelContract systemServiceContract = ServiceProxyFactory.Create<IDuplexChannelContract>(context, "DuplexService");

            return systemServiceContract;
        }

        /// <summary>
        /// 根据默认地址来创建 IFlexibleSystemServiceContract 代理对象
        /// </summary>
        /// <param name="systemServiceCallBack"></param>
        /// <returns></returns>
        public static IMutualChannelContract CreateMutualChannelContract(IMutualChannelCallBackContract mutualChannelCallBack)
        {
            InstanceContext context = new InstanceContext(mutualChannelCallBack);
            IMutualChannelContract flexibleSystemServiceContract = ServiceProxyFactory.Create<IMutualChannelContract>(context, "MutualChannelService");

            return flexibleSystemServiceContract;
        }

         #endregion
    }
}
