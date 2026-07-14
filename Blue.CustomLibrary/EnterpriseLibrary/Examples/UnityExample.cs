//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UnityExample.cs
// 描述: 使用 Unity 实现 AOP 的例子
// 作者：ChenJie 
// 编写日期：2016-07-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
using Unity.Interception;
using Unity.Interception.PolicyInjection.Pipeline;
using Unity.Interception.InterceptionBehaviors;

namespace Blue.CustomLibrary.EnterpriseLibrary
{
    /// <summary>
    /// 使用 Unity 实现 AOP 的例子，推荐方法二，理由是接口与类没有多余的代码，只需要实现对方法或是类拦截的代码
    /// </summary>
    public sealed class UnityExample
    {        
        ///// <summary>
        ///// 方法一： 使用代码的形式，实现 CallHandler 和 Attribute 向一个对象的操作中注入通用的业务操作
        ///// </summary>
        //public static void TestPolicyInjectionByCallHandlerWithCode()
        //{
        //       var container1 = new UnityContainer()
        //            .AddNewExtension<Interception>()
        //            .RegisterType<IUnityBusiness, UnityBusiness>();

        //    container1
        //        .Configure<Interception>()
        //        .SetInterceptorFor<IUnityBusiness>(new InterfaceInterceptor());

        //    var unityBusiness = container1.Resolve<IUnityBusiness>();
        //    unityBusiness.Print("aa");
        //}

        ///// <summary>
        ///// 方法二：使用代码的形式，实现 IInterceptionBehavior，向一个对象的操作中注入通用的业务操作
        ///// </summary>
        //public static void TestPolicyInjectionByInterceptionWithCode()
        //{
        //    var container1 = new UnityContainer()
        //    .AddNewExtension<Interception>()
        //    .RegisterType<IUnityBusiness, NewUnityBusiness>(
        //              //透明代理拦截
        //              //new Interceptor<TransparentProxyInterceptor>(),
        //              //接口拦截，与上面的透明拦截效果一样
        //              new Interceptor<InterfaceInterceptor>(),
        //              new InterceptionBehavior<CustomBehavior>());

        //    var unityBusiness = container1.Resolve<IUnityBusiness>();
        //    unityBusiness.Print("aa");
        //}

        /// <summary>
        /// 方法三：使用 Unity.config 配置文件的形式，实现 IInterceptionBehavior， 向一个对象的操作中注入通用的业务操作
        /// </summary>
        public static void TestPolicyInjectionByInterceptionWithConfig()
        {
            var unityBusiness = UnityContianer.Container.Resolve<IUnityBusiness>();
            unityBusiness.Print("aa");
        }

        /// <summary>
        /// 方法四： 使用 Unity.config 的形式，实现 CallHandler 和 Attribute 向一个对象的操作中注入通用的业务操作，对应的配置文件省略
        /// </summary>
        public static void TestPolicyInjectionByCallHandlerWithConfig()
        {
            var unityBusiness = UnityContianer.Container.Resolve<IUnityBusiness>();
            unityBusiness.Print("aa");
        }
    }
}
