//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: OracleHelper.cs
// 描述: Oracle 数据库访问类
// 作者：ChenJie 
// 编写日期：2017/07/15
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;

namespace AppFramework.Reference.Oracle
{
    /// <summary>
    /// Oracle 数据库访问类
    /// </summary>
    public sealed class OracleHelper
    {
        #region 常量

        /// <summary>
        /// 配置文件名称
        /// </summary>
        private const string APP_SETTING_FIELE_NAME = @"EnterpriseLibrary\AppConfigFiles\OracleAccess.config";

        /// <summary>
        /// 四川大学财务处
        /// </summary>
        private const string SCU_FINANCE = @"scu_finance";

        #endregion

        #region 只读静态变量

        /// <summary>
        /// 配置文件的完整路径名称
        /// </summary>
        private static readonly string appSettingFullFileName;

        /// <summary>
        /// 配置文件
        /// </summary>
        private static readonly Configuration config;

        /// <summary>
        /// 配置文件段
        /// </summary>
        private static readonly AppSettingsSection appSettingsSection;

        /// <summary>
        /// 缓存 Oracle 对象
        /// </summary>
        private static readonly Hashtable oracleDatabaseCache;

        /// <summary>
        /// 配置文件缓存
        /// </summary>
        private static readonly CustomFileCache customFileCache;

        #endregion

        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static OracleHelper()
        {

            try
            {
                appSettingFullFileName = GetAppSettingFullFileName(AppDomain.CurrentDomain.BaseDirectory);
                if (File.Exists(appSettingFullFileName))
                {
                    ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                    file.ExeConfigFilename = appSettingFullFileName;
                    config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                    appSettingsSection = (AppSettingsSection)config.GetSection("appSettings");
                    customFileCache = new CustomFileCache(appSettingFullFileName);
                    oracleDatabaseCache = Hashtable.Synchronized(new Hashtable());
                }
                else
                {
                    throw new Exception("自定义配置文件路径为空。");
                }
            }
            catch (Exception ex)
            {
                ExceptionHelper.NotifyRethrowNoWrapPolicy(ex);
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得 AppSetting.config 的路径
        /// </summary>
        /// <param name="baseDirectory"></param>
        /// <returns></returns>
        public static string GetAppSettingFullFileName(string baseDirectory)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(baseDirectory);
            if (!baseDirectory.EndsWith(@"\"))
            {
                sb.Append(@"\");
            }
            sb.Append(APP_SETTING_FIELE_NAME);

            return sb.ToString();
        }

        /// <summary>
        /// 获得配置文件的对应值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <returns>值</returns>
        public static string GetConnectionString(string key)
        {
            string value = string.Empty;
            try
            {
                /*  缓存中不存在则从文件中读取, 否则从缓存中读取 */
                lock (oracleDatabaseCache.SyncRoot)
                {
                    if (!customFileCache.Contains(key))
                    {
                        value = appSettingsSection.Settings[key].Value;
                        customFileCache[key] = value;
                    }
                    else
                    {
                        value = customFileCache[key].ToString();
                    }
                }

                return value;
            }
            catch { }

            return value;
        }

        /// <summary>
        /// 设置配置文件的对应值
        /// </summary>
        /// <param name="key">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetConnectionString(string key, string value)
        {
            try
            {
                lock (oracleDatabaseCache.SyncRoot)
                {
                    /* 1.保存到文件中 */
                    appSettingsSection.Settings[key].Value = value;
                    config.Save(ConfigurationSaveMode.Modified);

                    /* 2. 刷新缓存内容 */
                    customFileCache[key] = value;
                    if (oracleDatabaseCache.Contains(key))
                    {
                        oracleDatabaseCache.Remove(key);
                        OracleDatabase db = new OracleDatabase(key);
                        oracleDatabaseCache.Add(key, db);
                    }
                }
            }
            catch { }
        }

        #endregion

        #region 静态方法

        #region 获得数据库对象

        /// <summary>
        /// 获得川大财务系统数据库的 Database
        /// </summary>
        /// <returns> Database 对象</returns>
        public static OracleDatabase GetSCUFinanceDatabase()
        {
            string connectionString = GetConnectionString("SCU_FINANCE");

            return GetOracleDatabase(connectionString);
        }

        /// <summary>
        /// 根据字符串获取数据库连接对象
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static OracleDatabase GetOracleDatabase(string key)
        {
            OracleDatabase db = null;

            if (!string.IsNullOrWhiteSpace(key))
            {
                try
                {
                    lock (oracleDatabaseCache.SyncRoot)
                    {
                        if (oracleDatabaseCache.Contains(key))
                        {
                            db = oracleDatabaseCache[key] as OracleDatabase;
                        }
                        else
                        {
                            db = new OracleDatabase(key);
                            oracleDatabaseCache.Add(key, db);
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

        #endregion

        #region 字段类型对应关系

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oracleDbType"></param>
        /// <returns></returns>
        public static DbType OracleDbTypeToDbType(OracleDbType oracleDbType)
        {
            DbType dbType = DbType.Object;
            switch (oracleDbType)
            {
                case OracleDbType.NVarchar2:
                case OracleDbType.Char:
                case OracleDbType.NChar:
                case OracleDbType.NClob:
                case OracleDbType.XmlType:
                    dbType = DbType.String;
                    break;

                case OracleDbType.Int32:
                    dbType = DbType.Int32;
                    break;

                case OracleDbType.Int64:
                    dbType = DbType.Int64;
                    break;

                case OracleDbType.Single:
                    dbType = DbType.Single;
                    break;

                case OracleDbType.Double:
                    dbType = DbType.Double;
                    break;

                case OracleDbType.Decimal:
                    dbType = DbType.Decimal;
                    break;

                case OracleDbType.Date:
                case OracleDbType.TimeStamp:
                case OracleDbType.TimeStampTZ:
                case OracleDbType.TimeStampLTZ:
                    dbType = DbType.DateTime;
                    break;

                case OracleDbType.IntervalDS:
                    dbType = DbType.Time;
                    break;

                case OracleDbType.IntervalYM:
                    dbType = DbType.Int32;
                    break;
                case OracleDbType.Long:
                case OracleDbType.Clob:
                    dbType = DbType.String;
                    break;

                case OracleDbType.Raw:
                case OracleDbType.LongRaw:
                case OracleDbType.Blob:
                case OracleDbType.BFile:
                    dbType = DbType.Binary;
                    break;

                case OracleDbType.Boolean:
                    dbType = DbType.Boolean;
                    break;

                default:
                    throw new NotSupportedException();
            }

            return dbType;
        }

        public static OracleDbType DbTypeToOracleDbType(DbType dbType)
        {
            OracleDbType oracleDbType = OracleDbType.NVarchar2;

            switch (dbType)
            {
                case DbType.AnsiString:
                case DbType.String:
                    oracleDbType = OracleDbType.NVarchar2;
                       break;

                case DbType.AnsiStringFixedLength:
                case DbType.StringFixedLength:
                    oracleDbType = OracleDbType.Char;
                    break;

                case DbType.Byte:
                case DbType.Int16:
                case DbType.SByte:
                case DbType.UInt16:
                    oracleDbType = OracleDbType.Int16;
                    break;

                case DbType.Int32:
                    oracleDbType = OracleDbType.Int32;
                    break;

                case DbType.Single:
                    oracleDbType = OracleDbType.Single;
                    break;

                case DbType.Double:
                    oracleDbType = OracleDbType.Double;
                    break;

                case DbType.Date:
                    oracleDbType = OracleDbType.Date;
                    break;

                case DbType.DateTime:
                    oracleDbType = OracleDbType.TimeStamp;
                    break;

                case DbType.Time:
                    oracleDbType = OracleDbType.IntervalDS;
                    break;

                case DbType.Binary:
                    oracleDbType = OracleDbType.Blob;
                    break;

                case DbType.Boolean:
                    oracleDbType = OracleDbType.Boolean;
                    break;

                case DbType.Int64:
                case DbType.UInt64:
                case DbType.VarNumeric:
                case DbType.Decimal:
                case DbType.Currency:
                    oracleDbType = OracleDbType.Long;
                    break;

                case DbType.Guid:
                    oracleDbType = OracleDbType.Raw;
                    break;

                default:
                    throw new NotSupportedException();
            }

            return oracleDbType;
        }

        #endregion

        #endregion

    }
}

