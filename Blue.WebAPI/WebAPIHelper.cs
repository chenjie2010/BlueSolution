//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ThridPartHelper.cs
// 描述: 第三方服务辅助操作类
// 作者：ChenJie 
// 编写日期：2017-07-08
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;

namespace Blue.WebAPI
{
    /// <summary>
    /// 第三方服务辅助操作类
    /// </summary>
    public class WebAPIHelper
    {
        /// <summary>
        /// 生成 Token
        /// 格式：UserName|TimegGnerated (用户名| Token 生成时间) 加密得到value
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public static string GenerateToken(string userName)
        {
            return CryptographyHelper.NewEncrypt(string.Format("{0}|{1}", userName, DateTime.Now));
        }
        
        /// <summary>
        /// 解析生成 Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static string AnalysisToken(string token)
        {
            string result = string.Empty;

            if (!string.IsNullOrWhiteSpace(token))
            {
                try
                {
                    result = CryptographyHelper.NewDecrypt(token);
                }
                catch { }
            }

            return result;
        }

        /// <summary>
        /// 将 DataTable 序列化字符串
        /// DataRow转化为一个Directory对象，然后将每一个Directory对象放入List中，最终生成的是DataRow的数组
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static string ToJson(DataTable dataTable)
        {
            //DataTable dt1 = JsonConvert.DeserializeObject<DataTable>(json);
            return JsonConvert.SerializeObject(dataTable, new DataTableConverter());            
        }

        /// <summary>
        /// 将对象转换为JSON
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static HttpResponseMessage ConvertToJson(Object obj)
        {
            string content = string.Empty;

            if (obj is String || obj is Char)
            {
                content = obj.ToString();
            }
            else
            {
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                content = serializer.Serialize(obj);
            }
            HttpResponseMessage result = new HttpResponseMessage { Content = new StringContent(content, Encoding.GetEncoding("UTF-8"), "application/json") };
            return result;
        }
    }
}
