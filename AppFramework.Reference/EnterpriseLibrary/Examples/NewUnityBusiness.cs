//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: NewUnityBusiness.cs
// 描述: 使用 Unity 实现 AOP 操作类
// 作者：ChenJie 
// 编写日期：2016-07-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Reference.EnterpriseLibrary.Examples
{
    /// <summary>
    /// 使用 Unity 实现 AOP 操作类
    /// </summary>
    public class NewUnityBusiness : IUnityBusiness
    {
        /// <summary>
        /// 测试函数
        /// </summary>
        /// <param name="message"></param>
        public void Print(string message)
        {
            Console.WriteLine(message);
        }
    }
}
