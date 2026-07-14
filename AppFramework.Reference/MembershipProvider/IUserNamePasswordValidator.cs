using System;

namespace AppFramework.Reference.MembershipProvider
{
    /// <summary>
    /// 验证数据源中是否存在指定的用户名和密码接口
    /// </summary>
    public interface IUserNamePasswordValidator
    {
        /// <summary>
        /// 验证数据源中是否存在指定的用户名和密码。
        /// </summary>
        /// <param name="username">要验证的用户的名称。</param>
        /// <param name="password">指定的用户的密码。</param>
        /// <returns>如果指定的用户名和密码有效，则为 true；否则为 false。</returns>
        bool ValidateUser(string username, string password);
    }
}
