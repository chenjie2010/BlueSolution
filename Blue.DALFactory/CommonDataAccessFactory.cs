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
using Blue.IDAL;

namespace Blue.DALFactory
{
    /// <summary>
    /// 通用模块抽象工厂类
    /// </summary>
    public sealed class CommonDataAccessFactory
    {
        #region 只读变量


        #endregion

        #region 静态构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static CommonDataAccessFactory()
        {
        }

        #endregion

        #region 静态方法

        /// <summary>
        ///  创建 DatabaseProcessor 对象
        /// </summary>
        /// <returns></returns>
        public static IDatabaseProcessor CreateDatabaseProcessor()
        {
            return DALObjectHelper.CreateIDAL<IDatabaseProcessor>(string.Empty, "DatabaseProcessor");
        }

        #endregion
    }
}
