//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: WinFormsMembershipProvider.cs
// 描述:  Win Forms 使用自定义成员资格提供程序提供成员资格服务
// 作者：ChenJie 
// 编写日期：2016-07-28
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.MembershipProvider;

namespace AppFramework.WinFormsLibrary
{
    /// <summary>
    /// Win Forms 使用自定义成员资格提供程序提供成员资格服务
    /// </summary>
    public class WinFormsMembershipProvider : UserNamePasswordValidator
    {
        #region 私有变量

        private readonly static CustomMemoryCache customMemoryCache;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        static WinFormsMembershipProvider()
        {
            customMemoryCache = new CustomMemoryCache();
        }

        #endregion

        #region 方法

        /// <summary>
        /// 验证用户的用户名和密码
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        public override void Validate(string username, string password)
        {
            bool result = false;

            if (customMemoryCache.Contains(username) && customMemoryCache[username].ToString().Equals(password))
            {
                result = true;
            }
            if (!result && UserMembership.ValidateUser(username, password))
            {
                result = true;
                customMemoryCache[username] = password;
            }

            if (!result)
            {
                throw new SecurityTokenException("Unknown username or password");
            }
        }

        #endregion
    }
}
