//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CachingMechanism.cs
// 描述: 参数名称缓存类
// 作者：ChenJie 
// 编写日期：2017/07/16
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace AppFramework.Reference.Oracle
{
    /// <summary>
    /// 自定义 Oracle参数缓存机制
    /// </summary>
    internal class CachingMechanism
    {
        private Hashtable paramCache = Hashtable.Synchronized(new Hashtable());

        /// <devdoc>
        /// 创建参数的副本
        /// </devdoc>        
        public static OracleParameter[] CloneParameters(OracleParameter[] originalParameters)
        {
            OracleParameter[] clonedParameters = new OracleParameter[originalParameters.Length];

            for (int i = 0, j = originalParameters.Length; i < j; i++)
            {
                clonedParameters[i] = (OracleParameter)originalParameters[i].Clone();
            }

            return clonedParameters;
        }

        /// <devdoc>
        /// 清除缓存
        /// </devdoc>
        public void Clear()
        {
            this.paramCache.Clear();
        }

        /// <devdoc>
        /// 将参数输入加入缓存
        /// </devdoc>        
		public void AddParameterSetToCache(string connectionString, OracleCommand command, OracleParameter[] parameters)
        {
            string storedProcedure = command.CommandText;
            string key = CreateHashKey(connectionString, storedProcedure);
            this.paramCache[key] = parameters;
        }

        /// <devdoc>
        /// 缓存中不存在，则创建参数数组的副本
        /// </devdoc>        
		public OracleParameter[] GetCachedParameterSet(string connectionString, OracleCommand command)
        {
            string storedProcedure = command.CommandText;
            string key = CreateHashKey(connectionString, storedProcedure);
            OracleParameter[] cachedParameters = (OracleParameter[])(this.paramCache[key]);

            return CloneParameters(cachedParameters);
        }

        /// <devdoc>
        /// 判断是否在缓存中存在
        /// </devdoc>        
		public bool IsParameterSetCached(string connectionString, OracleCommand command)
        {
            string hashKey = CreateHashKey(connectionString, command.CommandText);

            return this.paramCache[hashKey] != null;
        }

        /// <summary>
        /// 创建 hash 键值
        /// </summary>
        /// <param name="connectionString"></param>
        /// <param name="storedProcedure"></param>
        /// <returns></returns>
        private static string CreateHashKey(string connectionString, string storedProcedure)
        {
            return connectionString + ":" + storedProcedure;
        }
    }
}
