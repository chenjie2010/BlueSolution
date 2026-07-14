//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserValidator.cs
// 描述: 用户输入验证类
// 作者：ChenJie 
// 编写日期：2016-07-03
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Validation;

namespace AppFramework.Reference.EnterpriseLibrary
{
    /// <summary>
    /// 用户输入验证类
    /// </summary>
    public sealed class ValidationHelper
    {
        #region  静态方法

        /// <summary>
        /// 验证用户输入信息验证
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="t">类名称</param>
        /// <param name="warning">警告信息</param>
        /// <returns>验证正确返回 true，否则 false</returns>
        public static bool Validate<T>(T t, out string warning)
        {
            warning = string.Empty;
            ValidationResults results = Validation.Validate<T>(t);

            //Validator<T> productValidator = ValidationFactory.CreateValidator<T>();
            //ValidationResults results = productValidator.Validate(t);

            if (!results.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                int i = 0;
                foreach (ValidationResult result in results)
                {
                    i++;
                    sb.Append("警告信息");
                    sb.Append(i);
                    sb.Append(": ");
                    sb.Append(result.Message);
                    sb.Append(" ");
                }
                sb.Remove(sb.Length - 1, 1);
                warning = sb.ToString();
            }

            return results.IsValid;
        }

        #endregion    
    }
}
