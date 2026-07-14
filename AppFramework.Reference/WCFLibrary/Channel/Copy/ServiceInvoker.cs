//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ServiceInvoker.cs
// 描述: WCF Channel例子
// 作者：ChenJie 
// 编写日期：2016-07-27
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace AppFramework.Reference.WCFLibrary
{
    [ServiceContract(Namespace = "http://www.artech.com/")]
    public interface ICalculator
    {
        [OperationContract]
        int Add(int x, int y);
    }

    [ServiceContract(Namespace = "http://www.artech.com/")]
    public interface ICal
    {
        [OperationContract]
        int Add(int x, int y);
    }

    public class CalculatorProxy : ServiceProxyBase<ICalculator>, ICalculator
    {
        public CalculatorProxy()
            : base(Constants.EndpointConfigurationNames.CalculatorService)
        { }

        public int Add(int x, int y)
        {
            return this.Invoker.Invoke<int>(calculator => calculator.Add(x, y));
        }

        public class Constants
        {
            public class EndpointConfigurationNames
            {
                public const string CalculatorService = "calculatorservice";
            }
        }
    }

    public class ServiceInvoker
    {
        private static Dictionary<string, ChannelFactory> channelFactories = new Dictionary<string, ChannelFactory>();
        private static object syncHelper = new object();

        private static ChannelFactory<TChannel> GetChannelFactory<TChannel>(string endpointConfigurationName)
        {
            ChannelFactory<TChannel> channelFactory = null;
            if (channelFactories.ContainsKey(endpointConfigurationName))
            {
                channelFactory = channelFactories[endpointConfigurationName] as ChannelFactory<TChannel>;
            }

            if (null == channelFactory)
            {
                channelFactory = new ChannelFactory<TChannel>(endpointConfigurationName);
                lock (syncHelper)
                {
                    channelFactories[endpointConfigurationName] = channelFactory;
                }
            }

            return channelFactory;
        }

        public static void Invoke<TChannel>(Action<TChannel> action, TChannel proxy)
        {
            ICommunicationObject channel = proxy as ICommunicationObject;
            if (null == channel)
            {
                throw new ArgumentException("The proxy is not a valid channel implementing the ICommunicationObject interface", "proxy");
            }
            try
            {
                action(proxy);
            }
            catch (TimeoutException)
            {
                channel.Abort();
                throw;
            }
            catch (CommunicationException)
            {
                channel.Abort();
                throw;
            }
            finally
            {
                channel.Close();
            }
        }

        public static TResult Invoke<TChannel, TResult>(Func<TChannel, TResult> function, TChannel proxy)
        {
            ICommunicationObject channel = proxy as ICommunicationObject;
            if (null == channel)
            {
                throw new ArgumentException("The proxy is not a valid channel implementing the ICommunicationObject interface", "proxy");
            }
            try
            {
                return function(proxy);
            }
            catch (TimeoutException)
            {
                channel.Abort();
                throw;
            }
            catch (CommunicationException)
            {
                channel.Abort();
                throw;
            }
            finally
            {
                channel.Close();
            }
        }

        public static void Invoke<TChannel>(Action<TChannel> action, string endpointConfigurationName)
        {
            Invoke<TChannel>(action, GetChannelFactory<TChannel>(endpointConfigurationName).CreateChannel());
        }

        public static TResult Invoke<TChannel, TResult>(Func<TChannel, TResult> function, string endpointConfigurationName)
        {
            return Invoke<TChannel, TResult>(function, GetChannelFactory<TChannel>(endpointConfigurationName).CreateChannel());
        }
    }

    public class ServiceInvoker<TChannel> : ServiceInvoker
    {
        public string EndpointConfigurationName
        { get; private set; }

        public ServiceInvoker(string endpointConfigurationName)
        {
            this.EndpointConfigurationName = endpointConfigurationName;
        }

        public void Invoke(Action<TChannel> action)
        {
            Invoke<TChannel>(action, this.EndpointConfigurationName);
        }

        public TResult Invoke<TResult>(Func<TChannel, TResult> function)
        {
            return Invoke<TChannel, TResult>(function, this.EndpointConfigurationName);
        }
    }

    public class ServiceProxyBase<TChannel>
    {
        public virtual ServiceInvoker<TChannel> Invoker
        { get; private set; }

        public ServiceProxyBase(string endpointConfigurationName)
        {
            this.Invoker = new ServiceInvoker<TChannel>(endpointConfigurationName);
        }
    }

}

