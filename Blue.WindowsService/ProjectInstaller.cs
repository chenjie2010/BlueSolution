using System;
using System.Reflection;
using System.Configuration.Install;
using System.ServiceProcess;
using System.ComponentModel;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.WindowsService
{
    /// <summary>
    /// 打印卸载状态的委托
    /// </summary>
    /// <param name="state"></param>
    public delegate void PrintUnintallStateDelegate(string state);

    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        #region 只读变量

        private readonly ServiceInstaller serviceInstaller;
        private readonly ServiceProcessInstaller serviceProcessInstaller;

        #endregion

        #region 构造函数

        /// <summary>
        /// 
        /// </summary>
        public ProjectInstaller()
        {
            InitializeComponent();

            serviceProcessInstaller = new ServiceProcessInstaller();
            serviceProcessInstaller.Account = ServiceAccount.LocalSystem;

            serviceInstaller = new ServiceInstaller();
            serviceInstaller.StartType = ServiceStartMode.Automatic;
            // 这个服务名必须和服务名相同, 即与 AppSettingHelper.GetWindowsServiceName() 相同，但是不能直接使用，因为该类提供给打包使用。
            serviceInstaller.ServiceName = AppSettingHelper.WindowsServiceName;
            serviceInstaller.DisplayName = "Blue Windows Service";
            serviceInstaller.Description = "Blue 项目自定义的 WCF 服务宿主程序。";
            Installers.Add(serviceInstaller);
            Installers.Add(serviceProcessInstaller);
        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 重载安装方法
        /// </summary>
        /// <param name="stateSaver"></param>
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            try
            {
                base.Install(stateSaver);
                InstallWindowsService();//调用方法                              
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 重载卸载前的准备工作
        /// </summary>
        /// <param name="savedState"></param>
        protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
        {
            try
            {
                ServiceController controller = new ServiceController(serviceInstaller.ServiceName);
                if ((controller != null) && (controller.Status != ServiceControllerStatus.Stopped))
                {
                    controller.Stop();
                    controller.WaitForStatus(ServiceControllerStatus.Stopped);
                }
                base.OnBeforeInstall(savedState);
            }
            catch
            { }
        }

        /// <summary>
        /// 重载卸载方法
        /// </summary>
        /// <param name="savedState"></param>
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            try
            {
                base.Uninstall(savedState);
                UninstallWindowsService();//调用方法
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 回滚
        /// </summary>
        /// <param name="savedState"></param>
        public override void Rollback(System.Collections.IDictionary savedState)
        {
            try
            {
                base.Rollback(savedState);
                UninstallWindowsService();//调用方法
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region 公有方法

        public bool ServiceIsExisted()
        {
            return ServiceIsExisted(serviceInstaller.ServiceName);
        }

        public void UninstallPromotionWindowsService(PrintUnintallStateDelegate printUnintallState)
        {
            try
            {
                ServiceController controller = new ServiceController(serviceInstaller.ServiceName);
                if ((controller != null) && (controller.Status != ServiceControllerStatus.Stopped))
                {
                    printUnintallState("1. 正在停止名称为 PromotionWindowsService 的 Windows 服务，请稍后......");
                    controller.Stop();
                    controller.WaitForStatus(ServiceControllerStatus.Stopped);
                    printUnintallState("2. 名称为 PromotionWindowsService  的 Windows 服务已停止。");
                    printUnintallState("3. 正在卸载名称为 PromotionWindowsService  的 Windows 服务，请稍后......");
                    UninstallWindowsService();
                    printUnintallState("4. 卸载名称为 PromotionWindowsService  的 Windows 服务已完成，建议重新启动机器.");
                }
            }
            catch (Exception exception)
            {
                printUnintallState(string.Format("卸载过程中出现异常，异常消息为：{0}. ", exception.Message));
                printUnintallState("建议重启机器后再试，若仍然无法解决，请联系管理员.");
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 安装 Windows 服务
        /// </summary>
        private void InstallWindowsService()
        {
            try
            {
                string[] args = new string[] { "/i", "/LogFile=" };
                if (!ServiceIsExisted(serviceInstaller.ServiceName))
                {
                    Publish(args);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 卸载 Windows 服务
        /// </summary>
        private void UninstallWindowsService()
        {
            try
            {
                string[] args = new string[] { "/u", "/LogFile=" };
                if (ServiceIsExisted(serviceInstaller.ServiceName))
                {
                    Publish(args);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 根据服务名称来获得服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        private ServiceController GetServiceController(string serviceName)
        {
            ServiceController serviceController = null;

            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName.Equals(serviceName))
                {
                    serviceController = s;
                    break;
                }
            }

            return serviceController;
        }

        /// <summary>  
        /// 检查指定的服务是否存在  
        /// </summary>  
        /// <param name="serviceName">要查找的服务名字</param>  
        /// <returns></returns>  
        private bool ServiceIsExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName.Equals(serviceName))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 安装或是卸载 Windows 服务
        /// </summary>
        /// <param name="args"></param>
        private void Publish(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                return;
            }

            string filepath = Assembly.GetExecutingAssembly().Location;
            using (AssemblyInstaller assemblyInstaller = new AssemblyInstaller())
            {
                assemblyInstaller.UseNewContext = true;
                assemblyInstaller.Path = filepath;
                if (args[0].ToLower().Equals("/i") || args[0].ToLower().Equals("-i"))
                {
                    try
                    {
                        assemblyInstaller.Install(null);
                    }
                    catch
                    {
                        throw;
                    }
                }
                else if (args[0].ToLower().Equals("/u") || args[0].ToLower().Equals("-u"))
                {
                    try
                    {
                        assemblyInstaller.Uninstall(null);
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        #endregion
    }
}
