using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Blue.WebAPI
{
    /// <summary>
    /// Web API 异常处理类
    /// </summary>
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// 重写基类的异常处理方法
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            //1.返回调用方具体的异常信息
            if (actionExecutedContext.Exception is ArgumentException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                response.Content = new StringContent("参数错误。");
                response.ReasonPhrase = "Argument Exception";
                actionExecutedContext.Response = response;
            }
            else if(actionExecutedContext.Exception is NullReferenceException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.NoContent);
                response.Content = new StringContent("查找对象为空。");
                response.ReasonPhrase = "Null Exception";
                actionExecutedContext.Response = response;
            }            
            else if (actionExecutedContext.Exception is ArgumentNullException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.ExpectationFailed);
                response.Content = new StringContent("参数不能为空。");
                response.ReasonPhrase = "Argument Null Exception";
                actionExecutedContext.Response = response;
            }
            else if (actionExecutedContext.Exception is FormatException)
            {
                var response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
                response.Content = new StringContent("用户名密码错误。");
                response.ReasonPhrase = "User Name or Password Invalid .";
                actionExecutedContext.Response = response;
            }
            else if (actionExecutedContext.Exception is NotImplementedException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.NotImplemented);
            }
            else if (actionExecutedContext.Exception is TimeoutException)
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.RequestTimeout);
            }
            else
            {
                actionExecutedContext.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }

            base.OnException(actionExecutedContext);
        }
    }
}
