//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ParameterCache.cs
// 描述: 参数名称缓存类
// 作者：ChenJie 
// 编写日期：2017/07/16
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using Oracle.ManagedDataAccess.Client;

namespace AppFramework.Reference.Oracle
{
    /// <summary>
    /// 参数名称缓存类：用于缓存存储过程的参数名称
    /// </summary>
    public class ParameterCache
    {
        #region 私有静态变量

        private CachingMechanism cache = new CachingMechanism();

        #endregion

        #region 公有方法

        /// <summary>
        /// 在命令中设置参数名称
        /// </summary>
        /// <param name="command"></param>
        /// <param name="database"></param>
        public void SetParameters(OracleCommand command, OracleDatabase database)
        {
            if (command == null)
            {
                throw new ArgumentNullException("command");
            }
            if (database == null)
            {
                throw new ArgumentNullException("database");
            }

            if (AlreadyCached(command, database))
            {
                AddParametersFromCache(command, database);
            }
            else
            {
                database.DiscoverParameters(command);
                OracleParameter[] copyOfParameters = CreateParameterCopy(command);

                this.cache.AddParameterSetToCache(database.ConnectionString, command, copyOfParameters);
            }
        }

        #endregion

        #region 受保护方法

        /// <summary>
        /// 清空缓存
        /// </summary>
        protected internal void Clear()
        {
            this.cache.Clear();
        }

        /// <summary>
        /// 向缓存中增加一个命令
        /// </summary>
        /// <param name="command"></param>
        /// <param name="database"></param>
        protected virtual void AddParametersFromCache(OracleCommand command, OracleDatabase database)
        {
            OracleParameter[] parameters = this.cache.GetCachedParameterSet(database.ConnectionString, command);

            foreach (OracleParameter p in parameters)
            {
                command.Parameters.Add(p);
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 检查命令是否已经在缓存中
        /// </summary>
        /// <param name="command"></param>
        /// <param name="database"></param>
        /// <returns></returns>
        private bool AlreadyCached(OracleCommand command, OracleDatabase database)
        {
            return this.cache.IsParameterSetCached(database.ConnectionString, command);
        }

        private static OracleParameter[] CreateParameterCopy(OracleCommand command)
        {
            OracleParameterCollection parameters = command.Parameters;
            OracleParameter[] parameterArray = new OracleParameter[parameters.Count];
            parameters.CopyTo(parameterArray, 0);

            return CachingMechanism.CloneParameters(parameterArray);
        }

        #endregion
    }
}
