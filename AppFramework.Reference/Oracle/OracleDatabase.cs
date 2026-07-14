//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: OracleDatabase.cs
// 描述: Oracle 数据库类
// 作者：ChenJie 
// 编写日期：2017/07/16
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;

namespace AppFramework.Reference.Oracle
{
    /// <summary>
    /// Oracle 数据库类
    /// </summary>
    public class OracleDatabase
    {
        #region 静态变量

        /// <summary>
        /// 参数缓存
        /// </summary>
        private static readonly ParameterCache parameterCache = new ParameterCache();

        #endregion


        #region 静态变量

        /// <summary>
        /// 连接字符串
        /// </summary>
        private readonly string connectionString;
        
        #endregion

        #region 属性

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ConnectionString
        {
            get
            {
                return connectionString;
            }
        }
        

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        public OracleDatabase(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("连接字符串不能为空。");
            }           
            this.connectionString = connectionString;
        }

        #endregion

        #region  公有方法

        #region 向命令中增加参数

        /// <summary>
        /// 向命令中增加一个值为 null的参数，方向为输入
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="name">参数名称</param>
        /// <param name="dbType">数据类型</param>
        public void AddInParameter(OracleCommand command, string name, OracleDbType dbType)
        {
            AddParameter(command, name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, null);
        }

        /// <summary>
        /// 向命令中增加一个带值得参数， 方向为输入
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="name">参数名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="value">参数值</param>
        public void AddInParameter(OracleCommand command, string name, OracleDbType dbType, object value)
        {
            AddParameter(command, name, dbType, ParameterDirection.Input, String.Empty, DataRowVersion.Default, value);
        }

        /// <summary>
        /// 向命令中增加一个值为 null的参数，方向为输入
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="name">参数名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="sourceColumn">源列的名称</param>
        /// <param name="sourceVersion"> System.Data.DataRowVersion 值之一。 默认值为 Current</param>
        public void AddInParameter(OracleCommand command, string name, OracleDbType dbType, string sourceColumn, DataRowVersion sourceVersion)
        {
            AddParameter(command, name, dbType, 0, ParameterDirection.Input, true, 0, 0, sourceColumn, sourceVersion, null);
        }

        /// <summary>
        /// 向命令中增加一个方向为输出的参数，大小为 size
        /// </summary>
        /// <param name="command">命令</param>
        /// <param name="name">参数名称</param>
        /// <param name="dbType">数据类型</param>
        /// <param name="size">输出值的大小</param>
        public void AddOutParameter(OracleCommand command, string name, OracleDbType dbType, int size)
        {
            AddParameter(command, name, dbType, size, ParameterDirection.Output, true, 0, 0, String.Empty, DataRowVersion.Default, DBNull.Value);
        }

        /// <summary>
        /// 向命令中增加一个参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="direction"></param>
        /// <param name="sourceColumn"></param>
        /// <param name="sourceVersion"></param>
        /// <param name="value"></param>
        public void AddParameter(OracleCommand command, string name, OracleDbType dbType, ParameterDirection direction, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            AddParameter(command, name, dbType, 0, direction, false, 0, 0, sourceColumn, sourceVersion, value);
        }

        /// <summary>
        /// 向命令中增加一个参数
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <param name="nullable"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="sourceColumn"></param>
        /// <param name="sourceVersion"></param>
        /// <param name="value"></param>
        public void AddParameter(OracleCommand command, string name, OracleDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, 
            byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            OracleParameter parameter = CreateParameter(name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            command.Parameters.Add(parameter);
        }
        
        #endregion                

        #region  数据库连接

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <returns></returns>
        public OracleConnection CreateConnection()
        {
            return new OracleConnection(ConnectionString);
        }
        
        /// <summary>
        /// 获得新的数据库链接
        /// </summary>
        /// <returns></returns>
        public   OracleConnection GetNewOpenConnection()
        {
            OracleConnection connection = null;
            try
            {
                connection = CreateConnection();
                connection.Open();
            }
            catch
            {
                if (connection != null)
                    connection.Close();

                throw;
            }

            return connection;
        }

        #endregion

        #region  执行命令，并返回数据集

        /// <summary>
        /// 执行命令，并返回数据集
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(OracleCommand command)
        {
            DataSet dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;
            LoadDataSet(command, dataSet, "Table");

            return dataSet;
        }

        /// <summary>
        /// 执行命令，并返回数据集 (带事务)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(OracleCommand command, OracleTransaction transaction)
        {
            var dataSet = new DataSet();
            dataSet.Locale = CultureInfo.InvariantCulture;
            LoadDataSet(command, dataSet, "Table", transaction);

            return dataSet;
        }

        /// <summary>
        /// 执行储存过程，并返回数据集
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(string storedProcedureName, params object[] parameterValues)
        {
            using (OracleCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return ExecuteDataSet(command);
            }
        }

        /// <summary>
        /// 执行储存过程，并返回数据集 (带事务)
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(OracleTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            using (OracleCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return ExecuteDataSet(command, transaction);
            }
        }

        /// <summary>
        /// 通过命令类型和内容创建命令并执行，然后返回数据集
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(CommandType commandType, string commandText)
        {
            using (OracleCommand command = CreateCommandByCommandType(commandType, commandText))
            {
                return ExecuteDataSet(command);
            }
        }

        /// <summary>
        /// 通过命令类型和内容创建命令并执行，然后返回数据集 (带事务)
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public DataSet ExecuteDataSet(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            using (OracleCommand command = CreateCommandByCommandType(commandType, commandText))
            {
                return ExecuteDataSet(command, transaction);
            }
        }

        #endregion

        #region  执行命令并返回行数

        /// <summary>
        /// 执行命令并返回行数
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(OracleCommand command)
        {
            using (var wrapper = GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                return DoExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// 执行命令并返回行数 (带事务)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(OracleCommand command, OracleTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return DoExecuteNonQuery(command);
        }

        /// <summary>
        /// 执行存储过程并返回行数
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string storedProcedureName, params object[] parameterValues)
        {
            using (OracleCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// 执行存储过程并返回行数 (带事务)
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(OracleTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            using (OracleCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return ExecuteNonQuery(command, transaction);
            }
        }

        /// <summary>
        /// 通过命令类型和内容创建命令并执行，然后返回行数
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType commandType, string commandText)
        {
            using (OracleCommand command = CreateCommandByCommandType(commandType, commandText))
            {
                return ExecuteNonQuery(command);
            }
        }

        /// <summary>
        /// 通过命令类型和内容创建命令并执行，然后返回行数 (带事务)
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            using (OracleCommand command = CreateCommandByCommandType(commandType, commandText))
            {
                return ExecuteNonQuery(command, transaction);
            }
        }

        #endregion

        #region  通过命令行返回一个只读对象

        /// <summary>
        /// 通过命令行返回一个只读对象
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public OracleDataReader ExecuteReader(OracleCommand command)
        {
            using (DatabaseConnectionWrapper wrapper = GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                OracleDataReader realReader = DoExecuteReader(command, CommandBehavior.Default);

                return realReader;
            }
        }

        /// <summary>
        /// 通过命令行返回一个只读对象 (带事务)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public OracleDataReader ExecuteReader(OracleCommand command, OracleTransaction transaction)
        {
            PrepareCommand(command, transaction);

            return DoExecuteReader(command, CommandBehavior.Default);
        }

        /// <summary>
        /// 通过存储过程返回一个只读对象
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public OracleDataReader ExecuteReader(string storedProcedureName, params object[] parameterValues)
        {
            using (OracleCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return ExecuteReader(command);
            }
        }

        /// <summary>
        /// 通过存储过程返回一个只读对象 (带事务)
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public OracleDataReader ExecuteReader(OracleTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            using (OracleCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return ExecuteReader(command, transaction);
            }
        }

        /// <summary>
        /// 通过命令类型和内容创建命令并执行，然后返回只读对象
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public OracleDataReader ExecuteReader(CommandType commandType, string commandText)
        {
            using (OracleCommand command = CreateCommandByCommandType(commandType, commandText))
            {
                return ExecuteReader(command);
            }
        }

        /// <summary>
        /// 通过命令类型和内容创建命令并执行，然后返回只读对象 (带事务)
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public OracleDataReader ExecuteReader(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            using (OracleCommand command = CreateCommandByCommandType(commandType, commandText))
            {
                return ExecuteReader(command, transaction);
            }
        }

        #endregion

        #region  通过命令行返回单个值

        /// <summary>
        /// 通过命令行返回单个值
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public object ExecuteScalar(OracleCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            using (var wrapper = GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                return DoExecuteScalar(command);
            }
        }

        /// <summary>
        /// 通过命令行返回单个值 (带事务)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public object ExecuteScalar(OracleCommand command, OracleTransaction transaction)
        {
            PrepareCommand(command, transaction);
            return DoExecuteScalar(command);
        }

        /// <summary>
        /// 通过存储过程返回单个值
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public object ExecuteScalar(OracleTransaction transaction, string storedProcedureName, params object[] parameterValues)
        {
            using (OracleCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return ExecuteScalar(command, transaction);
            }
        }

        /// <summary>
        /// 通过存储过程返回单个值 (带事务)
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public object ExecuteScalar(string storedProcedureName, params object[] parameterValues)
        {
            using (OracleCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                return ExecuteScalar(command);
            }
        }

        /// <summary>
        /// 通过命令返回单个值
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(CommandType commandType, string commandText)
        {
            using (OracleCommand command = CreateCommandByCommandType(commandType, commandText))
            {
                return ExecuteScalar(command);
            }
        }

        /// <summary>
        /// 通过命令返回单个值 (带事务)
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        public object ExecuteScalar(OracleTransaction transaction, CommandType commandType, string commandText)
        {
            using (OracleCommand command = CreateCommandByCommandType(commandType, commandText))
            {
                return ExecuteScalar(command, transaction);
            }
        }

        #endregion

        #region  数据适配器对象

        /// <summary>
        /// 获得一个标准的更新行为对象
        /// </summary>
        /// <returns></returns>
        public OracleDataAdapter GetDataAdapter()
        {
            return GetDataAdapter(UpdateBehavior.Standard);
        }

        #endregion
        
        #region  获得命令

        /// <summary>
        /// 通过查询语句获得命令
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public OracleCommand GetSqlStringCommand(string query)
        {
            if (string.IsNullOrEmpty(query))
            {
                throw new ArgumentNullException("query");
            }

            return CreateCommandByCommandType(CommandType.Text, query);
        }

        /// <summary>
        /// 通过存储过程名称获得命令
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <returns></returns>
        public OracleCommand GetStoredProcCommand(string storedProcedureName)
        {
            if (string.IsNullOrEmpty(storedProcedureName))
            {
                throw new ArgumentNullException("storedProcedureName");
            }

            return CreateCommandByCommandType(CommandType.StoredProcedure, storedProcedureName);
        }

        /// <summary>
        /// 通过存储过程名称与参数值获得命令
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="parameterValues"></param>
        /// <returns></returns>
        public OracleCommand GetStoredProcCommand(string storedProcedureName, params object[] parameterValues)
        {
            if (string.IsNullOrEmpty(storedProcedureName))
            {
                throw new ArgumentNullException("storedProcedureName");
            }

            OracleCommand command = CreateCommandByCommandType(CommandType.StoredProcedure, storedProcedureName);

            AssignParameters(command, parameterValues);

            return command;
        }

        #endregion               

        #region  加载数据集

        /// <summary>
        /// 加载数据集
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        public void LoadDataSet(OracleCommand command, DataSet dataSet, string tableName)
        {
            LoadDataSet(command, dataSet, new[] { tableName });
        }

        /// <summary>
        /// 加载数据集 (事务)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <param name="transaction"></param>
        public void LoadDataSet(OracleCommand command, DataSet dataSet, string tableName, OracleTransaction transaction)
        {
            LoadDataSet(command, dataSet, new[] { tableName }, transaction);
        }

        /// <summary>
        /// 加载多个表的数据集
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        public void LoadDataSet(OracleCommand command, DataSet dataSet, string[] tableNames)
        {
            using (var wrapper = GetOpenConnection())
            {
                PrepareCommand(command, wrapper.Connection);
                DoLoadDataSet(command, dataSet, tableNames);
            }
        }

        /// <summary>
        /// 加载多个表的数据集 (事务)
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        /// <param name="transaction"></param>
        public void LoadDataSet(OracleCommand command, DataSet dataSet, string[] tableNames, OracleTransaction transaction)
        {
            PrepareCommand(command, transaction);
            DoLoadDataSet(command, dataSet, tableNames);
        }

        /// <summary>
        /// 从存储过程中加载多个表的数据集
        /// </summary>
        /// <param name="storedProcedureName"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        /// <param name="parameterValues"></param>
        public void LoadDataSet(string storedProcedureName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            using (OracleCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                LoadDataSet(command, dataSet, tableNames);
            }
        }

        /// <summary>
        /// 从存储过程中加载多个表的数据集 (事务)
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="storedProcedureName"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        /// <param name="parameterValues"></param>
        public void LoadDataSet(OracleTransaction transaction, string storedProcedureName, DataSet dataSet, string[] tableNames, params object[] parameterValues)
        {
            using (OracleCommand command = GetStoredProcCommand(storedProcedureName, parameterValues))
            {
                LoadDataSet(command, dataSet, tableNames, transaction);
            }
        }

        /// <summary>
        /// 通过命令类型和查询语句加载多个表的数据集
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        public void LoadDataSet(CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            using (OracleCommand command = CreateCommandByCommandType(commandType, commandText))
            {
                LoadDataSet(command, dataSet, tableNames);
            }
        }

        /// <summary>
        /// 通过命令类型和查询语句加载多个表的数据集 (事务)
        /// </summary>
        /// <param name="transaction"></param>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        public void LoadDataSet(OracleTransaction transaction, CommandType commandType, string commandText, DataSet dataSet, string[] tableNames)
        {
            using (OracleCommand command = CreateCommandByCommandType(commandType, commandText))
            {
                LoadDataSet(command, dataSet, tableNames, transaction);
            }
        }

        #endregion

        #region  更新数据集

        /// <summary>
        /// 在数据集中执行查询、更新和删除命令操作
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <param name="insertCommand"></param>
        /// <param name="updateCommand"></param>
        /// <param name="deleteCommand"></param>
        /// <param name="updateBehavior"></param>
        /// <param name="updateBatchSize"></param>
        /// <returns>返回受影响的记录数量</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, OracleCommand insertCommand, OracleCommand updateCommand, OracleCommand deleteCommand, UpdateBehavior updateBehavior, int? updateBatchSize)
        {
            using (var wrapper = GetOpenConnection())
            {
                if (updateBehavior == UpdateBehavior.Transactional && Transaction.Current == null)
                {
                    OracleTransaction transaction = BeginTransaction(wrapper.Connection);
                    try
                    {
                        int rowsAffected = UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, transaction, updateBatchSize);
                        CommitTransaction(transaction);

                        return rowsAffected;
                    }
                    catch
                    {
                        RollbackTransaction(transaction);

                        throw;
                    }
                }

                if (insertCommand != null)
                {
                    PrepareCommand(insertCommand, wrapper.Connection);
                }
                if (updateCommand != null)
                {
                    PrepareCommand(updateCommand, wrapper.Connection);
                }
                if (deleteCommand != null)
                {
                    PrepareCommand(deleteCommand, wrapper.Connection);
                }

                return DoUpdateDataSet(updateBehavior, dataSet, tableName, insertCommand, updateCommand, deleteCommand, updateBatchSize);
            }
        }

        /// <summary>
        /// 在数据集中执行查询、更新和删除命令操作
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <param name="insertCommand"></param>
        /// <param name="updateCommand"></param>
        /// <param name="deleteCommand"></param>
        /// <param name="updateBehavior"></param>
        /// <returns>返回受影响的记录数量</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, OracleCommand insertCommand, OracleCommand updateCommand, OracleCommand deleteCommand, UpdateBehavior updateBehavior)
        {
            return UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, updateBehavior, null);
        }

        /// <summary>
        /// 在数据集中执行查询、更新和删除命令操作 （带事务）
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <param name="insertCommand"></param>
        /// <param name="updateCommand"></param>
        /// <param name="deleteCommand"></param>
        /// <param name="transaction"></param>
        /// <param name="updateBatchSize"></param>
        /// <returns>返回受影响的记录数量</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, OracleCommand insertCommand, OracleCommand updateCommand, OracleCommand deleteCommand, OracleTransaction transaction, int? updateBatchSize)
        {
            if (insertCommand != null)
            {
                PrepareCommand(insertCommand, transaction);
            }
            if (updateCommand != null)
            {
                PrepareCommand(updateCommand, transaction);
            }
            if (deleteCommand != null)
            {
                PrepareCommand(deleteCommand, transaction);
            }

            return DoUpdateDataSet(UpdateBehavior.Transactional, dataSet, tableName, insertCommand, updateCommand, deleteCommand, updateBatchSize);
        }

        /// <summary>
        /// 在数据集中执行查询、更新和删除命令操作 （带事务）
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <param name="insertCommand"></param>
        /// <param name="updateCommand"></param>
        /// <param name="deleteCommand"></param>
        /// <param name="transaction"></param>
        /// <returns>返回受影响的记录数量</returns>
        public int UpdateDataSet(DataSet dataSet, string tableName, OracleCommand insertCommand, OracleCommand updateCommand, OracleCommand deleteCommand, OracleTransaction transaction)
        {
            return UpdateDataSet(dataSet, tableName, insertCommand, updateCommand, deleteCommand, transaction, null);
        }

        #endregion

        #region  参数

        /// <summary>
        /// 清除参数缓存
        /// </summary>
        public static void ClearParameterCache()
        {
            parameterCache.Clear();
        }
        
        /// <summary>
        /// 获取参数的值
        /// </summary>
        /// <param name="command"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public object GetParameterValue(OracleCommand command, string name)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            return command.Parameters[name].Value;
        }

        /// <summary>
        /// 给命令行的参数设置参数值
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterValues"></param>
        public void AssignParameters(OracleCommand command, object[] parameterValues)
        {
            parameterCache.SetParameters(command, this);

            if (SameNumberOfParametersAndValues(command, parameterValues) == false)
            {
                throw new InvalidOperationException("参数数量与值得数量个数不相等。");
            }

            AssignParameterValues(command, parameterValues);
        }

        /// <summary>
        /// 从命令中获取参数名称信息
        /// </summary>
        /// <param name="command"></param>
        public void DiscoverParameters(OracleCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            using (var wrapper = GetOpenConnection())
            {
                using (OracleCommand discoveryCommand = CreateCommandByCommandType(command.CommandType, command.CommandText))
                {
                    discoveryCommand.Connection = wrapper.Connection;
                    OracleCommandBuilder.DeriveParameters(discoveryCommand);

                    foreach (OracleParameter parameter in discoveryCommand.Parameters)
                    {
                        OracleParameter cloneParameter = (OracleParameter)parameter.Clone();
                        command.Parameters.Add(cloneParameter);
                    }
                }
            }
        }

        #endregion

        #endregion

        #region  受保护的方法

        /// <summary>
        /// 创建参数
        /// </summary>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <param name="nullable"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="sourceColumn"></param>
        /// <param name="sourceVersion"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected OracleParameter CreateParameter(string name, OracleDbType dbType, int size, ParameterDirection direction, bool nullable, byte precision, byte scale,
            string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            OracleParameter param = CreateParameter(name);
            ConfigureParameter(param, name, dbType, size, direction, nullable, precision, scale, sourceColumn, sourceVersion, value);
            return param;
        }

        /// <summary>
        /// 创建一个未配置的参数
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected OracleParameter CreateParameter(string name)
        {
            OracleParameter param = new OracleParameter();
            param.ParameterName = name;

            return param;
        }

        /// <summary>
        /// 配置参数
        /// </summary>
        /// <param name="param"></param>
        /// <param name="name"></param>
        /// <param name="dbType"></param>
        /// <param name="size"></param>
        /// <param name="direction"></param>
        /// <param name="nullable"></param>
        /// <param name="precision"></param>
        /// <param name="scale"></param>
        /// <param name="sourceColumn"></param>
        /// <param name="sourceVersion"></param>
        /// <param name="value"></param>
        protected void ConfigureParameter(OracleParameter param, string name, OracleDbType dbType, int size, ParameterDirection direction, bool nullable,
            byte precision, byte scale, string sourceColumn, DataRowVersion sourceVersion, object value)
        {
            param.OracleDbType = dbType;
            param.Size = size;
            param.Value = value ?? DBNull.Value;
            param.Direction = direction;
            param.IsNullable = nullable;
            param.SourceColumn = sourceColumn;
            param.SourceVersion = sourceVersion;
        }

        /// <summary>
        /// 获得数据库连接包装对象
        /// </summary>
        /// <returns></returns>
        protected DatabaseConnectionWrapper GetOpenConnection()
        {
            DatabaseConnectionWrapper connection = TransactionScopeConnections.GetConnection(this);
            return connection ?? GetWrappedConnection();
        }

        /// <summary>
        /// 获得数据库连接包装对象
        /// </summary>
        /// <returns></returns>
        private DatabaseConnectionWrapper GetWrappedConnection()
        {
            return new DatabaseConnectionWrapper(GetNewOpenConnection());
        }
        
        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private int DoExecuteNonQuery(OracleCommand command)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            int rowsAffected = command.ExecuteNonQuery();

            return rowsAffected;
        }

        /// <summary>
        /// 为命令赋值连接的值
        /// </summary>
        /// <param name="command"></param>
        /// <param name="connection"></param>
        private static void PrepareCommand(OracleCommand command, OracleConnection connection)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (connection == null)
            {
                throw new ArgumentNullException("connection");
            }

            command.Connection = connection;
        }

        /// <summary>
        /// 为命令赋值事务的值
        /// </summary>
        /// <param name="command"></param>
        /// <param name="transaction"></param>
        private static void PrepareCommand(OracleCommand command, OracleTransaction transaction)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (transaction == null)
            {
                throw new ArgumentNullException("transaction");
            }

            PrepareCommand(command, transaction.Connection);
            command.Transaction = transaction;
        }

        /// <summary>
        /// 检查参数的数量和值的数量是否匹配
        /// </summary>
        /// <param name="command"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        private bool SameNumberOfParametersAndValues(OracleCommand command, object[] values)
        {
            int numberOfParametersToStoredProcedure = command.Parameters.Count;
            int numberOfValuesProvidedForStoredProcedure = values.Length;
            return numberOfParametersToStoredProcedure == numberOfValuesProvidedForStoredProcedure;
        }

        /// <summary>
        /// 设置参数值
        /// </summary>
        /// <param name="command"></param>
        /// <param name="parameterName"></param>
        /// <param name="value"></param>
        private void SetParameterValue(OracleCommand command, string parameterName, object value)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }

            command.Parameters[parameterName].Value = value ?? DBNull.Value;
        }

        /// <summary>
        /// 为数据适配器对象 (data adapter) 设置行事件
        /// </summary>
        /// <param name="adapter">The <see cref="OracleDataAdapter"/> to set the event.</param>
        private void SetUpRowUpdatedEvent(OracleDataAdapter adapter) { }

        /// <summary>
        /// 获得一个指定的更新行为对象
        /// </summary>
        /// <param name="updateBehavior"></param>
        /// <returns></returns>
        protected OracleDataAdapter GetDataAdapter(UpdateBehavior updateBehavior)
        {
            OracleDataAdapter adapter = new OracleDataAdapter();

            if (updateBehavior == UpdateBehavior.Continue)
            {
                SetUpRowUpdatedEvent(adapter);
            }
            return adapter;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 创建命令
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="commandText"></param>
        /// <returns></returns>
        private OracleCommand CreateCommandByCommandType(CommandType commandType, string commandText)
        {
            OracleCommand command = new OracleCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;

            return command;
        }
        
        /// <summary>
        /// 开始数据库事务
        /// </summary>
        /// <param name="connection"></param>
        /// <returns></returns>
        private static OracleTransaction BeginTransaction(OracleConnection connection)
        {
            OracleTransaction tran = connection.BeginTransaction();
            return tran;
        }

        /// <summary>
        /// 执行数据库事务
        /// </summary>
        /// <param name="tran"></param>
        private static void CommitTransaction(IDbTransaction tran)
        {
            tran.Commit();
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cmdBehavior"></param>
        /// <returns></returns>
        private OracleDataReader DoExecuteReader(OracleCommand command, CommandBehavior cmdBehavior)
        {
            OracleDataReader reader = command.ExecuteReader(cmdBehavior);

            return reader;
        }

        /// <summary>
        /// 执行查询
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        private object DoExecuteScalar(OracleCommand command)
        {
            object returnValue = command.ExecuteScalar();

            return returnValue;
        }

        /// <summary>
        /// 加载数据集
        /// </summary>
        /// <param name="command"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableNames"></param>
        private void DoLoadDataSet(OracleCommand command, DataSet dataSet, string[] tableNames)
        {
            if (tableNames == null) throw new ArgumentNullException("表名数组不能为空。");
            if (tableNames.Length == 0)
            {
                throw new ArgumentException("表名数量不能为0。");
            }
            for (int i = 0; i < tableNames.Length; i++)
            {
                if (string.IsNullOrEmpty(tableNames[i]))
                {
                    throw new ArgumentException(string.Format("表名{0}不能为空。", string.Concat("tableNames[", i, "]")));
                }
            }

            using (OracleDataAdapter adapter = GetDataAdapter(UpdateBehavior.Standard))
            {
                ((IDbDataAdapter)adapter).SelectCommand = command;

                string systemCreatedTableNameRoot = "Table";
                for (int i = 0; i < tableNames.Length; i++)
                {
                    string systemCreatedTableName = (i == 0)
                                                        ? systemCreatedTableNameRoot
                                                        : systemCreatedTableNameRoot + i;

                    adapter.TableMappings.Add(systemCreatedTableName, tableNames[i]);
                }

                adapter.Fill(dataSet);
            }
        }

        /// <summary>
        /// 加载数据集
        /// </summary>
        /// <param name="behavior"></param>
        /// <param name="dataSet"></param>
        /// <param name="tableName"></param>
        /// <param name="insertCommand"></param>
        /// <param name="updateCommand"></param>
        /// <param name="deleteCommand"></param>
        /// <param name="updateBatchSize"></param>
        /// <returns></returns>
        private int DoUpdateDataSet(UpdateBehavior behavior, DataSet dataSet, string tableName, OracleCommand insertCommand, OracleCommand updateCommand, OracleCommand deleteCommand, int? updateBatchSize)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("表名不能为空。");
            }
            if (dataSet == null) throw new ArgumentNullException("dataSet");

            if (insertCommand == null && updateCommand == null && deleteCommand == null)
            {
                throw new ArgumentException("插入、更新和删除至少有一个不能为空。");
            }

            using (OracleDataAdapter adapter = GetDataAdapter(behavior))
            {
                IDbDataAdapter explicitAdapter = adapter;
                if (insertCommand != null)
                {
                    explicitAdapter.InsertCommand = insertCommand;
                }
                if (updateCommand != null)
                {
                    explicitAdapter.UpdateCommand = updateCommand;
                }
                if (deleteCommand != null)
                {
                    explicitAdapter.DeleteCommand = deleteCommand;
                }

                if (updateBatchSize != null)
                {
                    adapter.UpdateBatchSize = (int)updateBatchSize;
                    if (insertCommand != null)
                        adapter.InsertCommand.UpdatedRowSource = UpdateRowSource.None;
                    if (updateCommand != null)
                        adapter.UpdateCommand.UpdatedRowSource = UpdateRowSource.None;
                    if (deleteCommand != null)
                        adapter.DeleteCommand.UpdatedRowSource = UpdateRowSource.None;
                }

                int rows = adapter.Update(dataSet.Tables[tableName]);

                return rows;
            }
        }

        /// <summary>
        /// 给参数赋值
        /// </summary>
        /// <param name="command"></param>
        /// <param name="values"></param>
        private void AssignParameterValues(OracleCommand command, object[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                IDataParameter parameter = command.Parameters[i];

                SetParameterValue(command, parameter.ParameterName, values[i]);
            }
        }

        /// <summary>
        /// 事务回滚
        /// </summary>
        /// <param name="tran"></param>
        private static void RollbackTransaction(IDbTransaction tran)
        {
            tran.Rollback();
        }

        #endregion
    }
}
