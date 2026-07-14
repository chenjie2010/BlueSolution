//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomOperationAttribute.cs
// 描述: 自定义属性类
// 作者：ChenJie 
// 编写日期：2016-07-04
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace AppFramework.Reference.EnterpriseLibrary.Instrumentation
{
    /// <summary>
    /// 自定义拦截的行为类
    /// </summary>
    public class CustomBehavior : IInterceptionBehavior
    {

        public IEnumerable<Type> GetRequiredInterfaces()
        {
            return Type.EmptyTypes;
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextInterceptionBehaviorDelegate getNext)
        {
            //检查参数是否存在
            if (input == null) throw new ArgumentNullException("input");
            if (getNext == null) throw new ArgumentNullException("getNext");

            //开始拦截，此处可以根据需求编写具体业务逻辑代码

            //调用具体方法
            var result = getNext()(input, getNext);
            //判断所拦截的方法返回值是否是bool类型，
            //如果是bool则判断返回值是否为false,false:表示调用不成功，则直接返回方法不记录日志
            //if (result.ReturnValue.GetType() == typeof(bool))
            //{
            //    if (Convert.ToBoolean(result.ReturnValue) == false)
            //    {
            //        return result;
            //    }
            //}
            //如果调用方法没有出现异常则记录操作日志
            if (result.Exception == null)
            {

            }
            //返回方法，拦截结束
            return result;            
        }

        public bool WillExecute
        {
            get { return true; }
        }
    }
}
