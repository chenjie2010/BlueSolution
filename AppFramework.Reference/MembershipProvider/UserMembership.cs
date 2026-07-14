//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserMembership.cs
// 描述:  验证用户凭据并管理用户设置
// 作者：ChenJie 
// 编写日期：2016-07-28
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Reflection;
using System.Configuration;
using AppFramework.Reference.EnterpriseLibrary;

namespace AppFramework.Reference.MembershipProvider
{
    /// <summary>
    /// 验证用户凭据并管理用户设置
    /// </summary>
    public static class UserMembership
    {
        /// <summary>
        /// 验证数据源中是否存在指定的用户名和密码的对象
        /// </summary>
        private static readonly IUserNamePasswordValidator userNamePasswordValidator = null;

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static UserMembership()
        {
            string assemblyName = string.Empty;
            string fullClassName = string.Empty;

            string[] configResults = AppSettingHelper.MembershipProviderName.Split(',');
            if ((configResults != null) && (configResults.Length == 2))
            {
                assemblyName = configResults[0].Trim();
                fullClassName = configResults[1].Trim();
                if (!string.IsNullOrWhiteSpace(assemblyName) && !string.IsNullOrWhiteSpace(fullClassName))
                {
                    object obj = Assembly.Load(assemblyName).CreateInstance(fullClassName);
                    userNamePasswordValidator = obj as IUserNamePasswordValidator;
                }
            }
            if (userNamePasswordValidator == null)
            {
                throw new ArgumentException("默认的配置文件未正确配置名称为'MembershipProvider'的值的程序集名称.");
            }
        }

        /// <summary>
        /// 验证数据源中是否存在指定的用户名和密码。
        /// </summary>
        /// <param name="username">要验证的用户的名称。</param>
        /// <param name="password">指定的用户的密码。</param>
        /// <returns>如果指定的用户名和密码有效，则为 true；否则为 false。</returns>
        public static bool ValidateUser(string username, string password)
        {
            return userNamePasswordValidator.ValidateUser(username, password);
        }
    }
}
