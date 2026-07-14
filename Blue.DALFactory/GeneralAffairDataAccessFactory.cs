//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CommonDataAccessFactory.cs
// 描述：通用模块抽象工厂类
// 作者：ChenJie 
// 编写日期：2017/04/25
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Reference.CustomLibrary;
using Blue.IDAL.GeneralAffairModule;

namespace Blue.DALFactory
{
    /// <summary>
    /// 通用模块抽象工厂类
    /// </summary>
    public sealed class GeneralAffairDataAccessFactory
    {
        #region 只读变量

        private static readonly string nameSpace;

        #endregion

        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static GeneralAffairDataAccessFactory()
        {
            nameSpace = "GeneralAffairModule";
        }

        #endregion

        #region 静态方法

        /// <summary>
        ///  创建 PriavteAttachment 对象
        /// </summary>
        /// <returns></returns>
        public static IPriavteAttachment CreatePriavteAttachment()
        {
            return DALObjectHelper.CreateIDAL<IPriavteAttachment>(nameSpace, "PriavteAttachment");
        }

        /// <summary>
        ///  创建 DatabaseProcessor 对象
        /// </summary>
        /// <returns></returns>
        public static IPrivateMail CreatePrivateMail()
        {
            return DALObjectHelper.CreateIDAL<IPrivateMail>(nameSpace, "PrivateMail");
        }

        /// <summary>
        ///  创建 UserAndMail 对象
        /// </summary>
        /// <returns></returns>
        public static IUserAndMail CreateUserAndMail()
        {
            return DALObjectHelper.CreateIDAL<IUserAndMail>(nameSpace, "UserAndMail");
        }


        #endregion
    }
}
