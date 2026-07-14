//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CommonBusinessHelper.cs
// 描述: 通用业务操作
// 作者：ChenJie 
// 编写日期：2016/04/25
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using Blue.DALFactory;
using Blue.IDAL;

namespace Blue.BusinessLogic
{
    /// <summary>
    /// 通用业务操作类
    /// </summary>
    public sealed class CommonBusinessHelper
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        static CommonBusinessHelper()
        {

        }

        #endregion

        #region 通用业务操作

        /// <summary>
        ///  测试数据库连接
        /// </summary>
        /// <returns></returns>
        public static bool TestDatabaseConnection()
        {
            IDatabaseProcessor databaseProcessor = CommonDataAccessFactory.CreateDatabaseProcessor();

            return databaseProcessor.TestDatabaseConnection();
        }

        /// <summary>
        /// 测试数据库连接
        /// </summary>
        /// <param name="address"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static bool TestDatabaseConnection(string address, string userName, string password)
        {
            IDatabaseProcessor databaseProcessor = CommonDataAccessFactory.CreateDatabaseProcessor();

            return databaseProcessor.TestDatabaseConnection(address, userName, password);
        }

        /// <summary>
        /// 测试数据库连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public static bool TestDatabaseConnection(string connectionString)
        {
            IDatabaseProcessor databaseProcessor = CommonDataAccessFactory.CreateDatabaseProcessor();

            return databaseProcessor.TestConnectionString(connectionString);
        }
        

        #endregion

    }
}
