//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: PolicyInjectionHelper.cs
// 描述: 策略注入类
// 作者：ChenJie 
// 编写日期：2016-07-04
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection;
using Microsoft.Practices.EnterpriseLibrary.PolicyInjection.Configuration;

namespace AppFramework.Reference.EnterpriseLibrary
{
    /// <summary>
    /// 策略注入类，用于 AOP 编程
    /// 使用 Unity 完成 AOP 编程， 如要使用，需要引用旧版 Unity 3.0. 
    /// </summary>
    public sealed class PolicyInjectionHelper
    {
        #region 常量

        /// <summary>
        /// 配置源
        /// </summary>
        private const string CONFIG_SOURCE_NAME = "PolicyInjectionConfigSource";

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        static PolicyInjectionHelper()
        {
            try
            {
                string configPath = ConfigFileOperation.GetConfigPath(CONFIG_SOURCE_NAME);
                if (!string.IsNullOrWhiteSpace(configPath))
                {
                    using (FileConfigurationSource configurationSource = new FileConfigurationSource(configPath))
                    {
                        //PolicyInjection.SetPolicyInjector(new PolicyInjector(configurationSource));
                    }
                }
                else
                {
                    throw new Exception("策略注入配置文件路径为空.");
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 创建一个操作能够被拦截的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T Create<T>()
        {
            return PolicyInjection.Create<T>();
        }

        #endregion
    }
}
