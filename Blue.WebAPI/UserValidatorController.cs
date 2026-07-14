using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using Blue.BusinessLogic.UserModule;
using Blue.Model.UserModule;

namespace Blue.WebAPI
{
    /// <summary>
    /// 用户校验
    /// </summary>
    [RoutePrefix("User")]
    public class UserValidatorController : ApiController
    {
        private const string UNIQUE_CODE = "UC_SCU";

        /// <summary>
        /// 方案：
        ///  Token的构成方式： UserName|TimegGnerated (用户名| Token 生成时间)。
        ///  用户点击主系统的链接，登录子系统后，主系统记录日志到数据表（可选）。
        ///  数据库表名： UserToken
        ///  四个非空字段：UserTokenId (decimal(8, 0)), UserName (nvchar(128)), UniqueCode(nvchar(32), TimegValidated (datime))
        ///  UserTokenId 是自增字段，UniqueCode表示分配给第三方平台的唯一编码，TimegValidated 表示登录子系统时间
        ///  加密方式：自选，要求可逆
        /// 思路：
        /// 一、主系统：
        /// （1）登录主系统后，将 UserName|TimegGnerated (用户名| Token 生成时间) 加密得到value，子系统的在校验页面链接样例一： 127.0.1.1/validator.aspx?token=value
        ///  提示：校验页面链接样式可以协商，以子系统为准。
        /// （2）用户点击子系统链接后，由子系统进一步处理。
        /// 二、子系统：
        /// （1）在校验页面中，获取token的值，并结合主系统分配给子系统的 UniqueCode，调用主系统的 WEB API 服务，并传入两个参数token和uniqueCode的值（value1和value2）, 
        ///  服务地址如下： http://ip:port/api/validator?token=value1&uniqueCode=value2
        ///  并对返回结果进行解析。
        /// 三、主系统： 
        /// （1）获得子系统传递的两个参数的值token和uniqueCode的值（value1和value2), 对token的值（value1）进行解密，解密成功获得 用户名和 Token 生成时间，进一步核对uniqueCode的值value2。核对成功，则检查
        /// token的生成时间。Token 生成时间在有效期内，返回成功结果。失败，则返回失败结果。
        /// </summary>
        /// <param name="token">加密后的特殊字符串</param>
        /// <param name="uniqueCode">分配给第三方平台的唯一编码</param>
        /// <returns></returns>
        [WebApiExceptionFilter]
        [HttpGet]
        [Route("Validator")]
        public IHttpActionResult Get(string token, string uniqueCode)
        {
            ValidatedResult validatedResult = new ValidatedResult(true);

            try
            {
                if (string.IsNullOrWhiteSpace(token) || string.IsNullOrWhiteSpace(uniqueCode))
                {
                    validatedResult.Success = false;
                }
                /* 1. 验证 uniqueCode 的合法性*/
                if (validatedResult.Success && !uniqueCode.Trim().Equals(UNIQUE_CODE))
                {
                    validatedResult.Success = false;
                }

                /* 2. 解析 token 数据 */
                if (validatedResult.Success)
                {
                    string result = WebAPIHelper.AnalysisToken(token);
                    if (!string.IsNullOrWhiteSpace(result))
                    {
                        int pos = result.IndexOf('|');
                        if (pos > 0 && pos < result.Length)
                        {
                            string userName = result.Substring(0, pos);
                            DateTime generatedTime = Convert.ToDateTime(result.Substring(pos + 1));
                            TimeSpan span = new TimeSpan(0, 60, 0);
                            if (DateTime.Now.Subtract(generatedTime) > span)
                            {
                                validatedResult.Success = false;
                                validatedResult.ErrorMessage = string.Format("链接超时(有效时间{0}分钟)。", span.TotalMinutes);
                            }
                            else
                            {
                                UserAccountHandler userAccount = new UserAccountHandler();
                                UserAccountInfo userAccountInfo = userAccount.GetModelInfo(userName);
                                if (userAccountInfo != null)
                                {
                                    validatedResult.UserName = userName;
                                    validatedResult.IdentificationType = userAccountInfo.IdentificationType;
                                    validatedResult.UserIdentity = userAccountInfo.UserIdentity;
                                    validatedResult.DepId = userAccountInfo.DepId;
                                    validatedResult.UserTypeId = userAccountInfo.UserTypeId;
                                    validatedResult.EmailAddress = userAccountInfo.EmailAddress;
                                    validatedResult.TelephoneNumber = userAccountInfo.TelephoneNumber;
                                }
                                else
                                {
                                    validatedResult.Success = false;
                                    validatedResult.ErrorMessage = "用户不存在。";
                                }
                            }
                        }
                        else
                        {
                            validatedResult.Success = false;
                            validatedResult.ErrorMessage = "Token 格式错误。";
                        }
                    }
                    else
                    {
                        validatedResult.Success = false;
                        validatedResult.ErrorMessage = "Token 解密失败。";
                    }
                }
            }
            catch (Exception ex)
            {
                validatedResult.Success = false;
                validatedResult.ErrorMessage = "Token 异常。";
                AppSettingHelper.WebAPIException = ex.Message;
            }

            return Json<ValidatedResult>(validatedResult);

            //string aa = JsonConvert.SerializeObject(validatedResult);
            //var validated = JsonConvert.DeserializeObject(aa);
            //ValidatedResult bb = validated as ValidatedResult;
            //return WebAPIHelper.ConvertToJson(validatedResult);
        }

        //// 序列化为json匿名对象
        //var o = new
        //{
        //    a = 1,
        //    b = "Hello, World!",
        //    c = new[] { 1, 2, 3 },
        //    d = new Dictionary<string, int> { { "x", 1 }, { "y", 2 } }
        //};
        //var json = JsonConvert.SerializeObject(o);

        //// 反序列化 1
        //var anonymous = new { a = 0, b = String.Empty, c = new int[0], d = new Dictionary<string, int>() };
        //var o2 = JsonConvert.DeserializeAnonymousType(json, anonymous);
        //Console.WriteLine(o2.b);
        //Console.WriteLine(o2.c[1]);

        //// 反序列化2
        //var o3 = JsonConvert.DeserializeAnonymousType(json, new { c = new int[0], d = new Dictionary<string, int>() });
        //Console.WriteLine(o3.d["y"]);

        //序列化操作
        //var json = new { user = new { name = "fxhl", age = 23 } };
        //string jsonData = JsonConvert.SerializeObject(json);
        //反序列化操作方法一
        // Person p1 = JsonConvert.DeserializeObject<Person>(jsonData);
        //反序列化操作方法二
        //string json2 = "[{\"user\":{\"name\":\"fxhl\",\"age\":23}}]";
        //List<Person> listp2 = JsonConvert.DeserializeObject<List<Person>>(json2);

        //序列化一个字典
        //Dictionary<string, int> persons = new Dictionary<string, int>
        //    {
        //        {"Jack",36},
        //        {"GongHui",28},
        //        {"Smith",43}
        //    };

        //string json = JsonConvert.SerializeObject(persons, Formatting.Indented);
        //反序列化一个字典
        //string json = @"{  
        //        'Jack':36,  
        //        'GongHui':28,  
        //        'Smith':43  
        //    }";

        //Dictionary<string, int> persons = JsonConvert.DeserializeObject<Dictionary<string, int>>(json);
        //序列化一个集合
        //List<string> films = new List<string>
        //    {
        //        "SPEED 7",
        //        "HAPPY FEET",
        //        "ICE AGE 2"
        //    };

        //string json = JsonConvert.SerializeObject(films);
        //反序列化JSON成一个集合
        //string json = @"['SPEED 7','HAPPY FEET','ICE AGE 2']";
        //List<string> films = JsonConvert.DeserializeObject<List<string>>(json);

    }
}
