//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：IDatabaseProcessor.cs
// 描述：IDatabaseProcessor 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/04/25
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;

namespace Blue.IDAL
{
    /// <summary>
    /// 数据库相关的通用操作接口
    /// </summary>
    public interface IDatabaseProcessor
    {
        #region 接口

        /// <summary>
        ///  测试数据库链接
        /// </summary>
        /// <returns></returns>
        bool TestDatabaseConnection();

        /// <summary>
        /// 测试数据库链接
        /// </summary>
        /// <param name="address"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool TestDatabaseConnection(string address, string userName, string password);

        /// <summary>
        /// 测试数据库链接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        bool TestConnectionString(string connectionString);

        #endregion
    }
}
