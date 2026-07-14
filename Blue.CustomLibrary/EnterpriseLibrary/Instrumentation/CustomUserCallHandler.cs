//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomUserCallHandler.cs
// 描述: 自定义调用拦截类
// 作者：ChenJie 
// 编写日期：2016-07-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.Interception.PolicyInjection.Pipeline;

namespace Blue.CustomLibrary.EnterpriseLibrary
{
    /// <summary>
    /// 自定义调用拦截类
    /// </summary>
    public class CustomUserCallHandler : ICallHandler
    {
        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
        {
            //检查参数是否存在
            if (input == null) throw new ArgumentNullException("input");
            if (getNext == null) throw new ArgumentNullException("getNext");

            //开始拦截，此处可以根据需求编写具体业务逻辑代码

            //调用具体方法
            var result = getNext()(input, getNext);
            //如果调用方法没有出现异常则记录操作日志
            if (result.Exception == null)
            {

            }
            //返回方法，拦截结束
            return result;
        }

        public int Order
        {
            get;
            set;
        }
    }
}
