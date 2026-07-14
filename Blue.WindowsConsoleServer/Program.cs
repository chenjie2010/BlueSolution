//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: Program.cs
// 描述: Blue.WindowsConsoleServer 主程序入口
// 作者：ChenJie 
// 编写日期：2016-07-27
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Owin.Hosting;
using System.Net.Http;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.BusinessLogic;
using Blue.BusinessLogic.SystemModule;
using Blue.WebAPI;
using Blue.DataCommutation;

namespace Blue.WindowsConsoleServer
{
    /// <summary>
    /// Blue.WindowsConsoleServer 主程序入口
    /// </summary>
    class Program
    {
        static void Main(string[] args)
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

            ///* 方案二：首次在 timeParmaters[0] 分钟之后执行，然后每 timeParmaters[2] 分钟 执行一次 */
            //TimeSpan newPeriod = new TimeSpan(0, DataConvertionHelper.GetConvertedInt(timeParmaters[2], 120), 0);            
            //Timer newTimer = new Timer((o) => { financeBusiness.AutoUpdateSalary(); }, null, dueTime, newPeriod);

            /* 数据自动备份 */
            SystemConfigHandler systemConfigHandler = new SystemConfigHandler();
            bool autoBackup = Convert.ToBoolean(DataConvertionHelper.GetConvertedByte(systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.AutoBackup), 0));
            DateTime backupDateTime = DataConvertionHelper.GetConvertedDateTime(systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.BackupDateTime), DateTime.Now);
            //DateTime backupDateTime = DateTime.Now.AddSeconds(15);
            BackupPeriod backupPeriod = (BackupPeriod)DataConvertionHelper.GetConvertedByte(systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.Period), 0);
            long range = DataConvertionHelper.GetConvertedLong(systemConfigHandler.GetSystemConfigValue(SystemConfigKeyName.DataRange), 0L);
            CurrentDataBackup.Instance.ExecuteAutoBackupData(autoBackup, backupDateTime, backupPeriod, range);

            //* 数据库连接成功则自动启动服务，否则不启动服务 */
            bool result = CommonBusinessHelper.TestDatabaseConnection();
            if (!result)
            {
                return;
            }

            IPHostEntry myEntry = Dns.GetHostEntry(Dns.GetHostName());
            string address = myEntry.AddressList.FirstOrDefault<IPAddress>(e => e.AddressFamily.ToString().Equals("InterNetwork")).ToString();
            string baseAddress = string.Format(@"http://{0}:{1}/", address, AppSettingHelper.WebAPIDataPort);
            if (AppSettingHelper.EnableInterface)
            {
                // 启动 Web API 服务 
                WebApp.Start<Startup>(url: baseAddress);
            }
            Console.WriteLine("WEB API {0}服务已启动，按任意键中止...", baseAddress);

            X509Security security = new X509Security();
            security.VerifyAndImportX509Certificate2();
            string servicesName = AppSettingHelper.WCFServicesName;
            ServiceHostGroup serviceHostGroup = new ServiceHostGroup(servicesName);
            serviceHostGroup.StartAllServices();
            serviceHostGroup.PrintHostInfo();

            /* 缓存预加载 */
            CacheLoader.LoadColumnCaption();

            Console.WriteLine("WCF 服务已启动，按任意键中止...");
            Console.ReadKey(true);
            serviceHostGroup.StopAllServices();
        }

        private static void OnceOnlyTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
