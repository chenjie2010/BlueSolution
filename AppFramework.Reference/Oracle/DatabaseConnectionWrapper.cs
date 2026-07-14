//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DatabaseConnectionWrapper.cs
// 描述: 数据库连接对象包装类
// 作者：ChenJie 
// 编写日期：2017/07/16
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Threading;
using Oracle.ManagedDataAccess.Client;

namespace AppFramework.Reference.Oracle
{
    /// <summary>
    /// 数据库连接对象包装类
    /// </summary>
    public class DatabaseConnectionWrapper : IDisposable
    {
       /// <summary>
       /// 连接对象数量
       /// </summary>
        private int referenceCount;

        /// <summary>
        /// 数据库连接对象
        /// </summary>
        public OracleConnection Connection
        {
            get;
            private set;
        }

        public DatabaseConnectionWrapper(OracleConnection connection)
        {
            Connection = connection;
            referenceCount = 1;
        }

        /// <summary>
        /// 该包装类是否已经是否所有连接对象
        /// </summary>
        public bool IsDisposed
        {
            get { return referenceCount == 0; }
        }

        /// <summary>
        /// 释放连接
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// 释放连接，如果连接数为0，则关闭连接
        /// </summary>
        /// <param name="disposing"></param>
        protected void Dispose(bool disposing)
        {
            if (disposing)
            {
                int count = Interlocked.Decrement(ref referenceCount);
                if (count == 0)
                {
                    Connection.Dispose();
                    Connection = null;
                    GC.SuppressFinalize(this);
                }
            }
        }

        /// <summary>
        /// 增加连接
        /// </summary>
        /// <returns></returns>
        public DatabaseConnectionWrapper AddReference()
        {
            Interlocked.Increment(ref referenceCount);

            return this;
        }


    }
}
