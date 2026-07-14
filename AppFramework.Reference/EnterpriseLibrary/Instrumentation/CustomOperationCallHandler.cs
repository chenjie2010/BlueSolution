//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomOperationCallHandler.cs
// 描述: 自定义调用拦截类
// 作者：ChenJie 
// 编写日期：2016-07-05
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.Unity.InterceptionExtension;

namespace AppFramework.Reference.EnterpriseLibrary.Instrumentation
{
    /// <summary>
    /// 自定义调用拦截类
    /// </summary>
    [ConfigurationElementType(typeof(CustomCallHandlerData))]
    public class CustomOperationCallHandler : ICallHandler
    {
        public string Message
        {
            set;
            get;
        }

        public string ParameterName
        {
            set;
            get;
        }


        public CustomOperationCallHandler(string message, string parameterName)
        {
            this.Message = message;
            this.ParameterName = parameterName;
        }

        public CustomOperationCallHandler(NameValueCollection attributes)
        {
            //从配置文件中获取key，如不存在则指定默认key
            this.Message = String.IsNullOrEmpty(attributes["Message"]) ? "" : attributes["Message"];
            this.ParameterName = String.IsNullOrEmpty(attributes["ParameterName"]) ? "" : attributes["ParameterName"];
        }

        public IMethodReturn Invoke(IMethodInvocation input, GetNextHandlerDelegate getNext)
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

        public int Order
        {
            get;
            set;
        }
    }
}
