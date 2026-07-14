//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataSettingHelper.cs
// 描述: 提供第三方数据接口配置文件访问类
// 作者：ChenJie 
// 编写日期：2017-07-18
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using AppFramework.Reference.CustomLibrary;

namespace AppFramework.Reference.EnterpriseLibrary
{
    /// <summary>
    /// 提供第三方数据接口配置文件访问类
    /// </summary>
    public sealed class DataSettingHelper
    {
        #region 常量

        /// <summary>
        /// 配置文件名称
        /// </summary>
        private const string DATA_SETTING_FIELE_NAME = @"EnterpriseLibrary\AppConfigFiles\DataSetting.config";

        #endregion

        #region 只读静态变量

        /// <summary>
        /// 配置文件的完整路径名称
        /// </summary>
        private static readonly string dataSettingFullFileName;

        /// <summary>
        /// 配置文件
        /// </summary>
        private static readonly Configuration config;

        /// <summary>
        /// 配置文件段
        /// </summary>
        private static readonly AppSettingsSection appSettingsSection;

        /// <summary>
        /// 缓存对象
        /// </summary>
        private static readonly CustomFileCache customFileCache;

        /// <summary>
        /// 锁
        /// </summary>
        private static readonly object obj = new object();

        #endregion

        #region 构造函数

        /// <summary>
        /// 静态构造函数
        /// </summary>
        static DataSettingHelper()
        {

            try
            {
                dataSettingFullFileName = GetAppSettingFullFileName(AppDomain.CurrentDomain.BaseDirectory);
                if (File.Exists(dataSettingFullFileName))
                {
                    ExeConfigurationFileMap file = new ExeConfigurationFileMap();
                    file.ExeConfigFilename = dataSettingFullFileName;
                    config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
                    appSettingsSection = (AppSettingsSection)config.GetSection("appSettings");
                    customFileCache = new CustomFileCache(dataSettingFullFileName);
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
            sb.Append(DATA_SETTING_FIELE_NAME);

            return sb.ToString();
        }

        /// <summary>
        /// 获得配置文件的对应值
        /// </summary>
        /// <param name="name">关键字</param>
        /// <returns>值</returns>
        public static string GetAppSettingByName(string name)
        {
            string value = string.Empty;
            try
            {
                /*  缓存中不存在则从文件中读取, 否则从缓存中读取 */
                lock (obj)
                {
                    if (!customFileCache.Contains(name))
                    {
                        value = appSettingsSection.Settings[name].Value;
                        customFileCache[name] = value;
                    }
                    else
                    {
                        value = customFileCache[name].ToString();
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
        /// <param name="name">关键字</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        public static void SetAppSettingByName(string name, string value)
        {
            try
            {
                lock (obj)
                {
                    /* 1.保存到文件中 */
                    appSettingsSection.Settings[name].Value = value;
                    config.Save(ConfigurationSaveMode.Modified);

                    /* 2. 刷新缓存内容 */
                    customFileCache[name] = value;
                }
            }
            catch { }
        }

        #endregion
    }
}
