//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: PolicyInjectionExample.cs
// 描述: 策略注入类
// 作者：ChenJie 
// 编写日期：2016-07-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.EnterpriseLibrary;

namespace AppFramework.Reference.EnterpriseLibrary.Examples
{
    /// <summary>
    /// 策略注入例子
    /// </summary>
    public sealed class PolicyInjectionExample
    {
        /// <summary>
        /// 向一个对象的操作中注入通用的业务操作
        /// </summary>
        public static void TestPolicyInjection()
        {
            PolicyBusinessByConfig policyBusinessByConfig = PolicyInjectionHelper.Create<PolicyBusinessByConfig>();
            policyBusinessByConfig.Print("Testing");

            PolicyBusinessByCode policyBusinessByCode = PolicyInjectionHelper.Create<PolicyBusinessByCode>();
            policyBusinessByCode.Print("Testing");
        }
    }
}
