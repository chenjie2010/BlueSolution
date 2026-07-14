using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Cors;
using System.Web.Http.Cors;

namespace Blue.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.Indent = false;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;

            // 移除XML格式，返回值自动就变成json格式
            config.Formatters.Remove(config.Formatters.XmlFormatter);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            ////2.自定义路由一：匹配到action
            //config.Routes.MapHttpRoute(
            //    name: "ActionApi",
            //    routeTemplate: "actionapi/{controller}/{action}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            //跨域配置
            //var allowedOrigin = "*";
            //var allowedHeaders = "http://a.com";
            //var allowedMethods = "*";
            //var geduCors = new EnableCorsAttribute(allowedOrigin, allowedHeaders, allowedMethods)
            //{
            //    SupportsCredentials = true
            //};
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));
        }
    }
}
