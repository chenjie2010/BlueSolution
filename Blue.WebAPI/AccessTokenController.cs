//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AccessTokenController.cs
// 描述: 调用API获取系统数据类
// 作者：ChenJie 
// 编写日期：2019-04-19
// 版权所有 (C) 四川大学 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.WebAPI
{
    [RoutePrefix("AccessToken")]
    public class AccessTokenController : ApiController
    {
        #region 私有变量

        private static HttpClient httpClient;

        #endregion

        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static AccessTokenController()
        {
            IPHostEntry myEntry = Dns.GetHostEntry(Dns.GetHostName());
            string address = myEntry.AddressList.FirstOrDefault<IPAddress>(e => e.AddressFamily.ToString().Equals("InterNetwork")).ToString();
            string baseAddress = string.Format(@"http://{0}:{1}", address, AppSettingHelper.WebAPIDataPort);
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(baseAddress);
        }

        #endregion

        #region 接口函数        

        /// <summary>
        /// 获得 Token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("Token")]
        public IHttpActionResult GetToken(string userName, string password)
        {
            TokenResponse tokenResponse = null;

            try
            {
                if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException();
                }
                var thread = GetTokenResponse(userName, password);
                thread.Wait();
                tokenResponse = thread.Result;
                if (tokenResponse == null)
                {
                    throw new FormatException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<TokenResponse>(tokenResponse);
        }

        /// <summary>
        /// 获得刷新后的 Token
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("RefreshToken")]
        public IHttpActionResult GetRefreshToken(string token)
        {
            TokenResponse tokenResponse = null;

            try
            {
                if (string.IsNullOrWhiteSpace(token))
                {
                    throw new ArgumentException();
                }
                var thread = GetTokenResponse(token);
                thread.Wait();
                tokenResponse = thread.Result;
                if (tokenResponse == null)
                {
                    throw new FormatException();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Json<TokenResponse>(tokenResponse);
        }


        #endregion

        #region 私有方法

        /// <summary>
        /// 异步调用访问 Token 的方法
        /// </summary>
        /// <param name="token">当前 Token</param>
        /// <returns>更新后的Token</returns>
        private async Task<TokenResponse> GetTokenResponse(string token)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "refresh_token");
            parameters.Add("refresh_token", token);
            var response = await httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            var tokenResponse = await response.Content.ReadAsAsync<TokenResponse>();

            return tokenResponse;
        }

        /// <summary>
        /// 异步调用访问 Token 的方法
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        private async Task<TokenResponse> GetTokenResponse(string userName, string password)
        {
            var parameters = new Dictionary<string, string>();
            parameters.Add("grant_type", "password");
            parameters.Add("username", userName);
            parameters.Add("password", password);
            var response = await httpClient.PostAsync("/token", new FormUrlEncodedContent(parameters));
            if (response.StatusCode != HttpStatusCode.OK)
            {
                return null;
            }
            var tokenResponse = await response.Content.ReadAsAsync<TokenResponse>();

            return tokenResponse;
        }

        #endregion

    }
}
