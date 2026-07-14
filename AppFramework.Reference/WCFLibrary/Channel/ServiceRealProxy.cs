//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ServiceRealProxy.cs
// 描述: 真实服务代理类
// 作者：ChenJie 
// 编写日期：2016-07-27
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.ServiceModel;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// 真实服务代理类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ServiceRealProxy<T> : RealProxy
    {
        #region 成员变量

        private string _endpointName;
        private InstanceContext _context;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="endpointName"></param>
        public ServiceRealProxy(string endpointName)
            : base(typeof(T))
        {
            if (string.IsNullOrEmpty(endpointName))
            {
                throw new ArgumentNullException("endpointName");
            }
            this._context = null;
            this._endpointName = endpointName;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        /// <param name="endpointName"></param>
        public ServiceRealProxy(InstanceContext context, string endpointName)
            : base(typeof(T))
        {
            if (string.IsNullOrEmpty(endpointName))
            {
                throw new ArgumentNullException("endpointName");
            }
            this._context = context;
            this._endpointName = endpointName;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 重载调用方法
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        public override IMessage Invoke(IMessage msg)
        {
            T channel;
            if (this._context == null)
            {
                channel = ChannelFactoryCreator.Create<T>(this._endpointName).CreateChannel();
            }
            else
            {
                channel = ChannelFactoryCreator.Create<T>(this._context, this._endpointName).CreateChannel();
            }
            IMethodCallMessage methodCall = (IMethodCallMessage)msg;
            IMethodReturnMessage methodReturn = null;
            object[] copiedArgs = Array.CreateInstance(typeof(object), methodCall.Args.Length) as object[];
            methodCall.Args.CopyTo(copiedArgs, 0);

            try
            {
                object returnValue = methodCall.MethodBase.Invoke(channel, copiedArgs);
                methodReturn = new ReturnMessage(returnValue, copiedArgs, copiedArgs.Length, methodCall.LogicalCallContext, methodCall);
                (channel as ICommunicationObject).Close();
            }
            catch (Exception ex)
            {
                if (ex.InnerException is CommunicationException || ex.InnerException is TimeoutException)
                {
                    (channel as ICommunicationObject).Abort();
                }

                if (ex.InnerException != null)
                {
                    methodReturn = new ReturnMessage(ex.InnerException, methodCall);
                }
                else
                {
                    methodReturn = new ReturnMessage(ex, methodCall);
                }
            }

            return methodReturn;
        }

        #endregion
    }
}

