//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：UserDataAccessFactory.cs
// 描述：UserDataAccessFactory 数据访问层接口
// 作者：ChenJie 
// 编写日期：2016/08/09
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.CustomLibrary;
using Blue.IDAL.UserModule;

namespace Blue.DALFactory
{
    /// <summary>
    /// 为数据层模块 UserModule 创建了数据访问层的实现
    /// </summary>
    public sealed class UserDataAccessFactory
    {
        #region 只读变量

        private static readonly string nameSpace;

        #endregion

        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static UserDataAccessFactory()
        {
            nameSpace = "UserModule";
        }

        #endregion

        #region 静态方法

        /// <summary>
        ///  创建 UserAccount 对象
        /// </summary>
        /// <returns></returns>
        public static IUserAccount CreateUserAccount()
        {
            return DALObjectHelper.CreateIDAL<IUserAccount>(nameSpace, "UserAccount");
        }

        /// <summary>
        ///  创建 UserConfig 对象
        /// </summary>
        /// <returns></returns>
        public static IUserConfig CreateUserConfig()
        {
            return DALObjectHelper.CreateIDAL<IUserConfig>(nameSpace, "UserConfig");
        }

        #endregion
    }
}
