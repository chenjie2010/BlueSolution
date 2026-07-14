//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: PolicyBusinessByConfig.cs
// 描述: 业务类
// 作者：ChenJie 
// 编写日期：2016-07-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.EnterpriseLibrary.Instrumentation;

namespace AppFramework.Reference.EnterpriseLibrary.Examples
{
    /// <summary>
    /// 业务类，采用配置文件方式，在函数操作的前后插入通用的业务代码
    /// </summary>
    public class PolicyBusinessByConfig : MarshalByRefObject
    {
        /// <summary>
        /// 测试函数
        /// </summary>
        /// <param name="message"></param>
        [CustomOperation("a", "b")]   
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
