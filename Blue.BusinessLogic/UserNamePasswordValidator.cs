//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserValidator.cs
// 描述: 用户身份验证类
// 作者：ChenJie 
// 编写日期：2016-07-28
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.MembershipProvider;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.UserModule;
using Blue.Model.UserModule;
using Blue.BusinessInterface.UserModule;

namespace Blue.BusinessLogic
{
    /// <summary>
    /// WCF 用户名和密码验证类
    /// </summary>
    public class UserNamePasswordValidator : IUserNamePasswordValidator
    {
        #region 工厂类实例

        private static readonly IUserAccount dalUserAccount = UserDataAccessFactory.CreateUserAccount();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public UserNamePasswordValidator()
        {
        }

        #endregion

        #region 方法


        /// <summary>
        /// 验证用户
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="password">密码</param>
        /// <returns>如果提供的用户名和密码有效，则返回 true；否则返回 false</returns>
        public bool ValidateUser(string username, string password)
        {
            bool result = false;

            ValidationMode userValidationType = UserDataHelper.GetUserValidationType(username);
            
            result = dalUserAccount.ValidateUser(username, password, userValidationType);

            return result;
        }        

        #endregion
    }
}
