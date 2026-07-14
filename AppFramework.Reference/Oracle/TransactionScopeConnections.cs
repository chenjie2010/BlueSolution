//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: TransactionScopeConnections.cs
// 描述: Oracle 数据库类
// 作者：ChenJie 
// 编写日期：2017/07/16
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Transactions;

namespace AppFramework.Reference.Oracle
{
    /// <summary>
    /// 数据库连接对象线程池类
    /// </summary>
    public static class TransactionScopeConnections
    {        
        /// <summary>
        /// 保存事务与数据库连接包装对象的字典；
        /// 请注意：该字段不能设置为静态线程(thread-static)专有，因为 Oracle 事务 (Oracle transactions) 可能在不同的线程完成 
        /// </summary>
        private static readonly Dictionary<Transaction, Dictionary<string, DatabaseConnectionWrapper>> transactionConnections =
            new Dictionary<Transaction, Dictionary<string, DatabaseConnectionWrapper>>();


        /// <summary>
        /// 获得一个与当前事务 (the current transaction) 相关的数据库连接对象
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static DatabaseConnectionWrapper GetConnection(OracleDatabase db)
        {
            Transaction currentTransaction = Transaction.Current;

            if (currentTransaction == null)
                return null;

            Dictionary<string, DatabaseConnectionWrapper> connectionList;
            DatabaseConnectionWrapper connection;

            lock (transactionConnections)
            {
                if (!transactionConnections.TryGetValue(currentTransaction, out connectionList))
                {
                    connectionList = new Dictionary<string, DatabaseConnectionWrapper>();
                    transactionConnections.Add(currentTransaction, connectionList);

                    currentTransaction.TransactionCompleted += OnTransactionCompleted;
                }
            }

            lock (connectionList)
            {
                if (!connectionList.TryGetValue(db.ConnectionString, out connection))
                {                    
                    var dbConnection = db.GetNewOpenConnection();
                    connection = new DatabaseConnectionWrapper(dbConnection);
                    connectionList.Add(db.ConnectionString, connection);
                }
                connection.AddReference();
            }

            return connection;
        }

       /// <summary>
       /// 事务完成事件
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private static void OnTransactionCompleted(object sender, TransactionEventArgs e)
        {
            Dictionary<string, DatabaseConnectionWrapper> connectionList;

            lock (transactionConnections)
            {
                if (!transactionConnections.TryGetValue(e.Transaction, out connectionList))
                {                   
                    return;
                }
                                
                transactionConnections.Remove(e.Transaction);
            }

            lock (connectionList)
            {                
                foreach (var connectionWrapper in connectionList.Values)
                {
                    connectionWrapper.Dispose();
                }
            }
        }
    }
}
