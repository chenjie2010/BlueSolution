using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System.Web.Routing;
using System.Web.Mvc;
using System.Web.Optimization;
using Blue.WebAPI.Providers;

[assembly: OwinStartup(typeof(Blue.WebAPI.Startup))]
namespace Blue.WebAPI
{
    public class Startup
    {
        // This code configures Web API. The Startup class is specified as a type
        // parameter in the WebApp.Start method.
        public void Configuration(IAppBuilder appBuilder)
        { 
            ConfigureAuth(appBuilder);

            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
            appBuilder.UseCors(CorsOptions.AllowAll);
            appBuilder.UseWebApi(config);

            //AreaRegistration.RegisterAllAreas();
            //GlobalConfiguration.Configure(WebApiConfig.Register);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        /// <summary>
        /// 配置授权
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureAuth(IAppBuilder app)
        {
            var option = new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                AuthenticationMode = AuthenticationMode.Active,
                TokenEndpointPath = new PathString("/token"), //获取 access_token 授权服务请求地址
                AuthorizeEndpointPath = new PathString("/authorize"), //获取 authorization_code 授权服务请求地址
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1), //access_token 过期时间

                Provider = new OpenAuthorizationServerProvider(), //access_token 相关授权服务
                //AuthorizationCodeProvider = new OpenAuthorizationCodeProvider(), //authorization_code 授权服务
                RefreshTokenProvider = new OpenRefreshTokenProvider() //refresh_token 授权服务
            };
            //app.UseOAuthBearerTokens(option); //表示 token_type 使用 bearer 方式
            //app.UseOAuthAuthorizationServer(option);
            //app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            app.UseOAuthBearerTokens(option); //表示 token_type 使用 bearer 方式
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions()
            {
                //从url中获取token，兼容hearder方式
                Provider = new QueryStringOAuthBearerProvider("access_token")
            });
        }
    }
}
