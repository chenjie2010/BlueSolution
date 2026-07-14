//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DatabaseProcessor.cs
// 描述: 数据库相关的通用操作类
// 作者：ChenJie 
// 编写日期：2017-04-25
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.IDAL;

namespace Blue.SQLServerDAL
{
    /// <summary>
    /// 数据库相关的通用操作类
    /// </summary>
    public sealed class DatabaseProcessor : IDatabaseProcessor
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DatabaseProcessor()
        {

        }

        #endregion

        #region 通用方法

        /// <summary>
        ///  测试数据库链接
        /// </summary>
        /// <returns></returns>
        public bool TestDatabaseConnection()
        {
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();

            return TestConnectionString(db.ConnectionString);
        }

        /// <summary>
        /// 测试数据库链接
        /// </summary>
        /// <param name="address"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool TestDatabaseConnection(string address, string userName, string password)
        {
            string connectionString = DataAccessHelper.GetConnectionString(address, DataAccessHelper.DefaultDatabaseName, userName, password);

            return TestConnectionString(connectionString);
        }        

        /// <summary>
        /// 测试数据库链接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        public bool TestConnectionString(string connectionString)
        {
            bool sucess = false;

            try
            {
                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    //创建连接对象
                    using (SqlConnection sqlConnection = new SqlConnection(connectionString))
                    {
                        //打开数据库
                        sqlConnection.Open();
                        sucess = true;
                        sqlConnection.Close();
                    }
                }
            }
            catch
            {
            }

            return sucess;
        }

        #endregion

        #region 私用方法



        #endregion
    }
}
