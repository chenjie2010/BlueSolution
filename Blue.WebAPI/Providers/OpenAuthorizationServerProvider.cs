using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using AppFramework.Core;
using Blue.BusinessLogic.UserModule;

namespace Blue.WebAPI.Providers
{
    /// <summary>
    /// 校验类
    /// </summary>
    public class OpenAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        ///// <summary>
        ///// 验证调用端的clientid与clientSecret已验证调用端的合法性(clientid、clientSecret为约定好的字符串)。
        ///// </summary>
        ///// <param name="context"></param>
        ///// <returns></returns>
        //public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        //{

        //    string clientId = string.Empty;
        //    string clientSecret = string.Empty;

        //    context.TryGetBasicCredentials(out clientId, out clientSecret);
        //    context.Validated();

        //    //if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
        //    //{
        //    //    context.TryGetFormCredentials(out clientId, out clientSecret);
        //    //}
        //    //if (clientId == "1234" && clientSecret == "5678")
        //    //{
        //    //    context.Validated(clientId);
        //    //}

        //    //await base.ValidateClientAuthentication(context);
        //}

        /// <summary>
        /// 通过重载GrantResourceOwnerCredentials获取用户名和密码进行认证
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //调用后台的登录服务验证用户名与密码
            UserAccountHandler userAccount = new UserAccountHandler();
            bool result = userAccount.ValidateUser(context.UserName, context.Password);
            if (!result)
            {
                context.SetError("invalid_grant", "用户名或密码不正确。");
                return;
            }
            Int64 dataFieldAuthority = userAccount.GetDataFieldAuthority(context.UserName);
            bool authority = AuthorityHelper.CheckAuthority(dataFieldAuthority, Convert.ToByte(SystemDataFieldPermission.Interface));
            if (!authority)
            {
                context.SetError("invalid_grant", "该用户不具有访问接口的权限。");
                return;
            }
            var authIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
            authIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
            var ticket = new AuthenticationTicket(authIdentity, new AuthenticationProperties());
            context.Validated(ticket);

            await base.GrantResourceOwnerCredentials(context);
        }

        ///// <summary>
        ///// 生成 authorization_code（authorization code 授权方式）、生成 access_token （implicit 授权模式）
        ///// </summary>
        //public override async Task AuthorizeEndpoint(OAuthAuthorizeEndpointContext context)
        //{
        //    if (context.AuthorizeRequest.IsImplicitGrantType)
        //    {
        //        //implicit 授权方式
        //        var identity = new ClaimsIdentity("Bearer");
        //        context.OwinContext.Authentication.SignIn(identity);
        //        context.RequestCompleted();
        //    }
        //    else if (context.AuthorizeRequest.IsAuthorizationCodeGrantType)
        //    {
        //        //authorization code 授权方式
        //        var redirectUri = context.Request.Query["redirect_uri"];
        //        var clientId = context.Request.Query["client_id"];
        //        var identity = new ClaimsIdentity(new GenericIdentity(
        //            clientId, OAuthDefaults.AuthenticationType));

        //        var authorizeCodeContext = new AuthenticationTokenCreateContext(
        //            context.OwinContext,
        //            context.Options.AuthorizationCodeFormat,
        //            new AuthenticationTicket(
        //                identity,
        //                new AuthenticationProperties(new Dictionary<string, string>
        //                {
        //                {"client_id", clientId},
        //                {"redirect_uri", redirectUri}
        //                })
        //                {
        //                    IssuedUtc = DateTimeOffset.UtcNow,
        //                    ExpiresUtc = DateTimeOffset.UtcNow.Add(context.Options.AuthorizationCodeExpireTimeSpan)
        //                }));

        //        await context.Options.AuthorizationCodeProvider.CreateAsync(authorizeCodeContext);
        //        context.Response.Redirect(redirectUri + "?code=" + Uri.EscapeDataString(authorizeCodeContext.Token));
        //        context.RequestCompleted();
        //    }
        //}

        ///// <summary>
        ///// 验证 authorization_code 的请求
        ///// </summary>
        //public override async Task ValidateAuthorizeRequest(OAuthValidateAuthorizeRequestContext context)
        //{
        //    if (context.AuthorizeRequest.ClientId == "xishuai" &&
        //        (context.AuthorizeRequest.IsAuthorizationCodeGrantType || context.AuthorizeRequest.IsImplicitGrantType))
        //    {
        //        context.Validated();
        //    }
        //    else
        //    {
        //        context.Rejected();
        //    }
        //}

        ///// <summary>
        ///// 验证 redirect_uri
        ///// </summary>
        //public override async Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        //{
        //    context.Validated(context.RedirectUri);
        //}

        ///// <summary>
        ///// 验证 access_token 的请求
        ///// </summary>
        //public override async Task ValidateTokenRequest(OAuthValidateTokenRequestContext context)
        //{
        //    if (context.TokenRequest.IsAuthorizationCodeGrantType || context.TokenRequest.IsRefreshTokenGrantType)
        //    {
        //        context.Validated();
        //    }
        //    else
        //    {
        //        context.Rejected();
        //    }
        //}

    }
}
