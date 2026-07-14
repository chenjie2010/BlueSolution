//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAccessHelper.cs
// 描述: 提供数据库访问对象类
// 作者：ChenJie 
// 编写日期：2017-04-29
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;

namespace AppFramework.Reference.EnterpriseLibrary
{
    /// <summary>
    /// 提供数据库访问对象类
    /// </summary>
    public sealed class DataAccessHelper
    {
        #region 常量

        private const string CONFIG_SOURCE_NAME = "DataAccessConfigSource";

        #endregion

        #region 私有静态变量   

        /// <summary>
        /// 默认的数据配置文件路径
        /// </summary>
        private static readonly string dataAccessFullFileName;

        /// <summary>
        /// 缓存对象
        /// </summary>
        private static CustomFileCache customFileCache;

        /// <summary>
        /// 数据库对象工厂类
        /// </summary>
        private static DatabaseProviderFactory databaseProviderFactory;

        /// <summary>
        /// 默认的数据库标识符
        /// </summary>
        private static string defaultDatabaseIdentifier;

        /// <summary>
        /// 数据库配置
        /// </summary>
        private static DatabaseSettings databaseSettings;

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object obj = new object();

        #endregion

        #region 静态属性

        /// <summary>
        /// 默认的数据仓库名称
        /// </summary>
        public static string DefaultDatabaseName
        {
            get
            {
                return defaultDatabaseIdentifier;
            }
        }

        /// <summary>
        /// 默认的数据配置文件路径
        /// </summary>
        public static string DefaultDataAccessFullFileName
        {
            get
            {
                return dataAccessFullFileName;
            }
        }

        /// <summary>
        /// 默认的(系统)数据库的连接字符串
        /// </summary>
        /// <returns>数据库的连接字符串</returns>
        public static string DefaultConnectionString
        {
            get
            {
                return GetConnectionString(defaultDatabaseIdentifier);
            }
        }


        #endregion
        
        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        DataAccessHelper()
        {

        }

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DataAccessHelper()
        {
            try
            {
                dataAccessFullFileName = ConfigFileOperation.GetConfigPath(CONFIG_SOURCE_NAME);
                InitDatabaseSettings(dataAccessFullFileName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 静态公有方法

        #region 数据库对象操作

        /// <summary>
        /// 获得系统数据库的 Database
        /// </summary>
        /// <returns> Database 对象</returns>
        public static SqlDatabase GetDatabase()
        {
            return GetDatabase(defaultDatabaseIdentifier);
        }

        /// <summary>
        /// 通过连接关键字获得指定的数据 DataBase
        /// </summary>
        /// <param name="key">配置文件中连接串名称</param>
        /// <returns>SqlDatabase</returns>
        public static SqlDatabase GetDatabase(string key)
        {
            SqlDatabase db = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                try
                {

                    lock (obj)
                    {
                        if (customFileCache.Contains(key))
                        {
                            db = customFileCache[key] as SqlDatabase;
                        }
                        else
                        {
                            db = databaseProviderFactory.Create(key) as SqlDatabase;
                            customFileCache[key] = db;
                        }
                    }

                }
                catch (Exception exception)
                {
                    //记录日志, 抛出异常, 不包装异常 
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }

            return db;
        }

        /// <summary>
        /// 根据配置文件中连接串名称刷新缓存的 DataBase
        /// </summary>
        /// <param name="key"></param>
        public static void RefreshDatabase(string key)
        {
            if (!string.IsNullOrWhiteSpace(key))
            {
                SqlDatabase db = DatabaseFactory.CreateDatabase(key) as SqlDatabase;
                lock (obj)
                {
                    customFileCache[key] = db;
                }
            }
        }

        /// <summary>
        /// 移除 DataBase
        /// </summary>
        /// <param name="key">配置文件中连接串名称</param>
        /// <returns>SqlDatabase</returns>
        public static SqlDatabase RemoveDatabase(string key)
        {
            SqlDatabase db = null;
            if (!string.IsNullOrWhiteSpace(key))
            {
                lock (obj)
                {
                    if (customFileCache.Contains(key))
                    {
                        customFileCache.Remove(key);
                    }
                }
            }

            return db;
        }

        #endregion
        
        #region 在指定文件进行连接字符串操作

        /// <summary>
        /// 获得指定文件的默认字符串
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string GetDefaultConnectionStringByFile(string path)
        {
            string connectionString = string.Empty;

            string identifier = string.Empty;
            using (FileConfigurationSource configurationSource = new FileConfigurationSource(path))
            {
                DatabaseSettings settings = DatabaseSettings.GetDatabaseSettings(configurationSource);
                identifier = settings.DefaultDatabase;

                ConnectionStringSettingsCollection connectionStrings
                    = settings.CurrentConfiguration.ConnectionStrings.ConnectionStrings;
                connectionString = settings.CurrentConfiguration.ConnectionStrings.ConnectionStrings[identifier].ConnectionString;
            }

            return connectionString;
            
        }

        /// <summary>
        /// 更新数据库连接
        /// </summary>
        /// <param name="path"></param>
        /// <param name="address"></param>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static bool UpdateConnectionStrings(string path, string address, string userName, string userPwd, DatabaseType databaseType)
        {
            bool result = false;

            try
            {
                string providerName = GetProviderName(databaseType);
                using (FileConfigurationSource configurationSource = new FileConfigurationSource(path))
                {
                    DatabaseSettings settings = DatabaseSettings.GetDatabaseSettings(configurationSource);

                    ConnectionStringSettingsCollection connectionStrings
                        = settings.CurrentConfiguration.ConnectionStrings.ConnectionStrings;

                    lock (obj)
                    {
                        foreach (ConnectionStringSettings connectionStringSettings in connectionStrings)
                        {
                            if (!connectionStringSettings.Name.ToLower().Trim().StartsWith(defaultDatabaseIdentifier.ToLower().Trim()))
                            {
                                continue;
                            }
                            string connectionString = GetConnectionString(address, connectionStringSettings.Name, userName, userPwd);
                            connectionStrings.Remove(connectionStringSettings.Name);
                            connectionStrings.Add(new ConnectionStringSettings(connectionStringSettings.Name, connectionString, providerName));
                        }
                        settings.CurrentConfiguration.Save(ConfigurationSaveMode.Modified);
                    }
                    result = true;
                }
            }
            catch { }

            return result;
        }


        #endregion

        #region 连接字符串操作    

        /// <summary>
        /// 获得默认的数据库的连接字符串
        /// </summary>
        /// <returns>数据库的连接字符串</returns>
        public static string GetDefaultConnectionString()
        {
            return GetConnectionString(defaultDatabaseIdentifier);
        }

        /// <summary>
        /// 通过关键字获得指定的数据库的连接字符串
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>数据库的连接字符串</returns>
        public static string GetConnectionString(string key)
        {
            string connectionString = string.Empty;
            ConnectionStringSettingsCollection connectionStrings
                = databaseSettings.CurrentConfiguration.ConnectionStrings.ConnectionStrings;
            connectionString = databaseSettings.CurrentConfiguration.ConnectionStrings.ConnectionStrings[key].ConnectionString;

            return connectionString;
        }

        /// <summary>
        /// 增加数据库的连接字符串
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseType">数据库类型</param>
        public static void AddConnectionString(string key, string connectionString, DatabaseType databaseType)
        {
            string providerName = GetProviderName(databaseType);
            if (!string.IsNullOrEmpty(providerName))
            {
                ConnectionStringSettingsCollection connectionStrings
                    = databaseSettings.CurrentConfiguration.ConnectionStrings.ConnectionStrings;
                connectionStrings.Add(new ConnectionStringSettings(key, connectionString, providerName));
                databaseSettings.CurrentConfiguration.Save(ConfigurationSaveMode.Modified);
            }
            else
            {
                throw new Exception("请指定正确的数据库类型");
            }
        }

        /// <summary>
        /// 更新数据库的连接字符串
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="connectionString">连接字符串</param>
        /// <param name="databaseType">数据库类型</param>
        public static void UpdateConnectionString(string key, string connectionString, DatabaseType databaseType)
        {
            string providerName = GetProviderName(databaseType);
            if (!string.IsNullOrEmpty(providerName))
            {
                ConnectionStringSettingsCollection connectionStrings
                    = databaseSettings.CurrentConfiguration.ConnectionStrings.ConnectionStrings;
                ConnectionStringSettings connectionStringSettings = connectionStrings[key];
                if (!connectionStringSettings.Name.Equals(key) ||
                    !connectionStringSettings.ConnectionString.Equals(connectionString) ||
                    !connectionStringSettings.ProviderName.Equals(providerName))
                {
                    lock (connectionStrings.SyncRoot)
                    {
                        connectionStrings.Remove(key);
                        connectionStrings.Add(new ConnectionStringSettings(key, connectionString, providerName));
                        databaseSettings.CurrentConfiguration.Save(ConfigurationSaveMode.Modified);
                    }
                }
            }
            else
            {
                throw new Exception("请指定正确的数据库类型");
            }
        }

        /// <summary>
        /// 通过关键字移除数据库的连接字符串
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>数据库的连接字符串</returns>
        public static void RemoveConnectionString(string key)
        {
            ConnectionStringSettingsCollection connectionStrings
                = databaseSettings.CurrentConfiguration.ConnectionStrings.ConnectionStrings;

            lock (obj)
            {
                databaseSettings.CurrentConfiguration.ConnectionStrings.ConnectionStrings.Remove(key);
                databaseSettings.CurrentConfiguration.Save(ConfigurationSaveMode.Modified);
            }
        }

        /// <summary>
        /// 更新数据库连接
        /// </summary>
        /// <param name="address"></param>
        /// <param name="userName"></param>
        /// <param name="userPwd"></param>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        public static bool UpdateConnectionStrings(string address, string userName, string userPwd, DatabaseType databaseType)
        {
            bool result = false;

            try
            {
                string providerName = GetProviderName(databaseType);
                ConnectionStringSettingsCollection connectionStrings
                        = databaseSettings.CurrentConfiguration.ConnectionStrings.ConnectionStrings;

                lock (obj)
                {
                    foreach (ConnectionStringSettings connectionStringSettings in connectionStrings)
                    {
                        if (!connectionStringSettings.Name.ToLower().Trim().StartsWith(defaultDatabaseIdentifier.ToLower().Trim()))
                        {
                            continue;
                        }
                        string connectionString = GetConnectionString(address, connectionStringSettings.Name, userName, userPwd);
                        connectionStrings.Remove(connectionStringSettings.Name);
                        connectionStrings.Add(new ConnectionStringSettings(connectionStringSettings.Name, connectionString, providerName));
                    }
                    databaseSettings.CurrentConfiguration.Save(ConfigurationSaveMode.Modified);
                }
                InitDatabaseSettings(dataAccessFullFileName);
                result = true;
            }
            catch { }

            return result;
        }

        /// <summary>
        /// 获得数据库链接字符串
        /// </summary>
        /// <param name="address"></param>
        /// <param name="databaseName"></param>
        /// <returns></returns>
        public static string GetConnectionString(string address, string databaseName)
        {
            return GetConnectionString(address, databaseName, string.Empty, string.Empty);
        }

        /// <summary>
        /// 获得 SQL Server 数据库链接字符串
        /// </summary>
        /// <param name="address"></param>
        /// <param name="databaseName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetConnectionString(string address, string databaseName, string userName, string password)
        {
            string connectionString = string.Empty;

            if (string.IsNullOrWhiteSpace(userName))
            {
                connectionString = string.Format("Data Source={0};Initial Catalog={1};Integrated Security=SSPI;", address, databaseName);
            }
            else
            {
                connectionString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}",
                        address, databaseName, userName, password);
            }

            return connectionString;
        }

        /// <summary>
        /// 获得 Oracle 数据库链接字符串
        /// </summary>
        /// <param name="address"></param>
        /// <param name="port"></param>
        /// <param name="databaseName"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string GetConnectionString(string address, string port, string databaseName, string userName, string password)
        {
            string connectionString = string.Empty;

            connectionString = string.Format("Password={0};User ID={1};Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST={2})(PORT={3})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={4})));",
                        password, userName, address, port, databaseName);

            return connectionString;
        }

        #endregion

        #region 其他静态公有方法

        /// <summary>
        /// 获得 DataAccess.config 的路径
        /// </summary>
        /// <param name="baseDirectory"></param>
        /// <returns></returns>
        public static string GetDataAccessFullFileName(string baseDirectory)
        {
            return ConfigFileOperation.GetConfigPath(CONFIG_SOURCE_NAME, baseDirectory);
        }
        
        /// <summary>
        /// 检测是否含有危险字符（防止Sql注入）
        /// </summary>
        /// <param name="contents">预检测的内容</param>
        /// <returns>返回True或false</returns>
        public static bool HasDangerousContents(string contents)
        {
            bool bReturnValue = false;
            if (contents.Length > 0)
            {
                string sLowerStr = contents.ToLower();
                //RegularExpressions
                string regularExpression = @"(insert\s)| (delete\s)|(update\s[\s\S].*\sset)|(create\s)|(\stable)|(<[iframe|/iframe|script|/script])|(\sexec)|(\sdeclare)|(\struncate)|(\smaster)|(\sbackup)|(\smid)|(\scount)";
                //Match
                bool bIsMatch = false;
                System.Text.RegularExpressions.Regex sRx = new System.Text.RegularExpressions.Regex(regularExpression);
                bIsMatch = sRx.IsMatch(sLowerStr, 0);
                if (bIsMatch)
                {
                    bReturnValue = true;
                }
            }

            return bReturnValue;
        }
        #endregion

        #endregion

        #region 私有方法

        /// <summary>
        /// 获得数据库类型名称
        /// </summary>
        /// <param name="databaseType"></param>
        /// <returns></returns>
        private static string GetProviderName(DatabaseType databaseType)
        {
            string providerName = string.Empty;

            switch (databaseType)
            {
                case DatabaseType.SQLServer:
                    providerName = "System.Data.SqlClient";
                    break;
                case DatabaseType.Oracel:
                    providerName = "System.Data.OracleClient";
                    break;
                default:
                    break;
            }

            return providerName;
        }

        /// <summary>
        /// 利用配置文件初始化
        /// </summary>
        /// <param name="configPath"></param>
        private static void InitDatabaseSettings(string configPath)
        {
            if (File.Exists(configPath))
            {
                using (FileConfigurationSource configurationSource = new FileConfigurationSource(configPath))
                {
                    databaseSettings = DatabaseSettings.GetDatabaseSettings(configurationSource);
                    defaultDatabaseIdentifier = databaseSettings.DefaultDatabase;
                    databaseProviderFactory = new DatabaseProviderFactory(configurationSource);
                }
                customFileCache = new CustomFileCache(configPath);
            }
            else
            {
                throw new Exception("自定义数据库配置文件路径为空。");
            }
        }

        #endregion

    }
}
