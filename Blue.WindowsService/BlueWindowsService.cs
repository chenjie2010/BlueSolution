using System;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using System.Net;
using Microsoft.Owin.Hosting;
using Microsoft.Owin.Hosting.Starter;
using Microsoft.Owin.Host.HttpListener;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.BusinessLogic;
using Blue.BusinessLogic.SystemModule;
using Blue.WebAPI;
using Blue.DataCommutation;

namespace Blue.WindowsService
{
    public partial class BlueWindowsService : ServiceBase
    {
        #region 只读变量

        private readonly ServiceHostGroup serviceHostGroup;

        #endregion

        #region 私有变量
        
        private IDisposable webAPIServer = null;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public BlueWindowsService()
        {
            InitializeComponent();
            this.ServiceName = AppSettingHelper.WindowsServiceName;
            this.CanStop = true;
            this.CanShutdown = true;
            this.CanPauseAndContinue = true;
            this.AutoLog = true;

            string servicesName = AppSettingHelper.WCFServicesName;
            serviceHostGroup = new ServiceHostGroup(servicesName);
        }

        #endregion

        #region 重载方法

        protected override void OnStart(string[] args)
        {
            base.OnStart(args);

            try
            {
                //SCUFinanceBusiness financeBusiness = new SCUFinanceBusiness();
                ////创建一个定时器
                //string timeConfig = DataSettingHelper.GetAppSettingByName("SCU_CW_DUE_INTERVAL_TIME");
                //string[] timeParmaters = timeConfig.Split('|');
                ///* 延迟时间  */
                //int due = DataConvertionHelper.GetConvertedInt(timeParmaters[0], 5);
                //TimeSpan dueTime = new TimeSpan(0, due, 0);

                ///* 方案一：首次在 timeParmaters[0] 分钟之后执行，然后每天固定在 0点 + timeParmaters[2] 分时执行 */
                ///* 1. 首次在 timeParmaters[0] 分钟之后执行 */
                //System.Timers.Timer onceOnlyTimer = new System.Timers.Timer(dueTime.TotalMilliseconds);
                //onceOnlyTimer.AutoReset = false;
                //onceOnlyTimer.Enabled = true;
                //onceOnlyTimer.Elapsed += (sender, e) => { financeBusiness.AutoUpdateSalary(); };
                //onceOnlyTimer.Start();

                ///* 2. 固定时间，每天执行一次 */
                //int interval = DataConvertionHelper.GetConvertedInt(timeParmaters[1], 60);
                //int days = DataConvertionHelper.GetConvertedInt(timeParmaters[2], 120);
                //TimeSpan period = DateTime.Now.Date.AddDays(days).AddMinutes(DataConvertionHelper.GetConvertedInt(timeParmaters[1], 120)).Subtract(DateTime.Now);
                //TimeSpan nextPeriod = new TimeSpan(days, 0, 0);
                //Timer timer = new Timer((o) =>
                //{
                //    financeBusiness.AutoUpdateSalary();
                //}, null, period, nextPeriod);

                /////* 方案二：首次在 timeParmaters[0] 分钟之后执行，然后每 timeParmaters[2] 分钟 执行一次 */
                ////TimeSpan newPeriod = new TimeSpan(0, DataConvertionHelper.GetConvertedInt(timeParmaters[2], 120), 0);            
                ////Timer newTimer = new Timer((o) => { financeBusiness.AutoUpdateSalary(); }, null, dueTime, newPeriod);
                                
                try
                {
                    /* 数据自动备份 */
                    SystemConfigHandler systemConfigHandler = new SystemConfigHandler();
                    bool autoBackup = Convert.ToBoolean(DataConvertionHelper.GetConvertedByte(systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.AutoBackup), 0));
                    DateTime backupDateTime = DataConvertionHelper.GetConvertedDateTime(systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.BackupDateTime), DateTime.Now);
                    //DateTime backupDateTime = DateTime.Now.AddSeconds(15);
                    BackupPeriod backupPeriod = (BackupPeriod)DataConvertionHelper.GetConvertedByte(systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.Period), 0);
                    long range = DataConvertionHelper.GetConvertedLong(systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.DataRange), 0L);
                    CurrentDataBackup.Instance.ExecuteAutoBackupData(autoBackup, backupDateTime, backupPeriod, range);
                }
                catch (Exception ex)
                {
                    AppSettingHelper.BackupException = ex.Message;
                }

                try
                {
                    if (AppSettingHelper.EnableInterface)
                    {
                        IPHostEntry myEntry = Dns.GetHostEntry(Dns.GetHostName());
                        string address = myEntry.AddressList.FirstOrDefault<IPAddress>(e => e.AddressFamily.ToString().Equals("InterNetwork")).ToString();
                        string baseAddress = string.Format(@"http://{0}:{1}/", address, AppSettingHelper.WebAPIDataPort);
                        // 启动 Web API 服务 
                        webAPIServer = WebApp.Start<Startup>(url: baseAddress);
                    }
                }
                catch (Exception ex)
                {
                    AppSettingHelper.WindowsServiceException = ex.Message;
                }

                X509Security security = new X509Security();
                security.VerifyAndImportX509Certificate2();
                serviceHostGroup.StartAllServices();

                /* 缓存预加载 */
                CacheLoader.LoadColumnCaption();

                AppSettingHelper.WindowsServiceStartTime = DateTime.Now.ToString("F");
                //AppSettingHelper.WindowsServiceException = string.Empty;
            }
            catch (Exception ex)
            {
                AppSettingHelper.WindowsServiceException = ex.Message;
            }
        }

        protected override void OnStop()
        {
            try
            {
                if (webAPIServer != null)
                {
                    webAPIServer.Dispose();
                }
            }
            catch (Exception ex)
            {
                AppSettingHelper.WindowsServiceException = ex.Message;
            }
            serviceHostGroup.StopAllServices();
            base.OnStop();
        }

        #endregion

        #region 私有方法

        ///// <summary>
        ///// 修改配置文件中 Windows 服务启动的时间
        ///// </summary>
        //public void UpdateWindowsServiceStartTime()
        //{
        //    string appSettingCnfigFileName = string.Format("{0}EnterpriseLibrary\\AppConfigFiles\\AppSetting.config", AppDomain.CurrentDomain.BaseDirectory);
        //    try
        //    {
        //        ExeConfigurationFileMap file = new ExeConfigurationFileMap();
        //        file.ExeConfigFilename = appSettingCnfigFileName;
        //        Configuration config = ConfigurationManager.OpenMappedExeConfiguration(file, ConfigurationUserLevel.None);
        //        AppSettingsSection section = (AppSettingsSection)config.GetSection("appSettings");
        //        section.Settings["WindowsServiceStartTime"].Value = DateTime.Now.ToString("F");
        //        config.Save(ConfigurationSaveMode.Modified);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        #endregion
    }
}
