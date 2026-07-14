//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomUserAttribute.cs
// 描述: 自定义属性类
// 作者：ChenJie 
// 编写日期：2016-07-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace AppFramework.Reference.EnterpriseLibrary.Instrumentation
{
    /// <summary>
    /// 自定义属性类
    /// </summary>
    public class CustomUserAttribute : HandlerAttribute
    {

        public override ICallHandler CreateHandler(IUnityContainer container)
        {
            //创建具体Call Handler，并调用
            CustomUserCallHandler handler = new CustomUserCallHandler();

            return handler;
        }
    }
}
