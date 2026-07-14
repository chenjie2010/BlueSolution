//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: UserChannelFactory.cs
// 描述: 用户模块类来创建客户端代理对象
// 作者：ChenJie 
// 编写日期：2016/08/08
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.WCFLibrary;
using Blue.WCFContracts.UserModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 用户模块类
    /// </summary>
    public sealed class UserChannelFactory
    {
        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static UserChannelFactory()
        {

        }

        #endregion

        #region 静态方法

        /// <summary>
        /// 根据默认地址来创建处理用户信息的 IUser 代理对象
        /// </summary>
        /// <returns></returns>
        public static IUserAccountContract CreateUserAccount()
        {
            IUserAccountContract userAccountContract = ServiceProxyFactory.Create<IUserAccountContract>("UserAccountService");

            return userAccountContract;
        }

        /// <summary>
        /// 根据默认地址来创建处理用户信息的 IUserConfigContract 代理对象
        /// </summary>
        /// <returns></returns>
        public static IUserConfigContract CreateUserConfig()
        {
            IUserConfigContract userConfigContract = ServiceProxyFactory.Create<IUserConfigContract>("UserConfigService");

            return userConfigContract;
        }


        #endregion
    }
}
