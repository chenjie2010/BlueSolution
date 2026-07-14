using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security;
using System.Diagnostics;
using System.Reflection;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.WindowsFormsClient
{
    static class Program
    {
        /// <summary>
        /// 重启状态
        /// </summary>
        public static bool restart = false;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Blue");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Application.ApplicationExit += new EventHandler(ApplicationExitHandler);

            new LoginForm().Show();
            Application.Run();
        }

        /// <summary>
        /// 线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            if (e.Exception != null)
            {
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(e.Exception);
            }
        }

        /// <summary>
        /// 未处理的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception)
            {
                ExceptionHandler((Exception)e.ExceptionObject);
            }
            if (e.ExceptionObject is SecurityException)
            {
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog((SecurityException)e.ExceptionObject);
            }
        }

        /// <summary>
        /// 处理异常
        /// </summary>
        /// <param name="e"></param>
        private static void ExceptionHandler(Exception e)
        {
            UnhandledExceptionForm frmUnhandledException = new UnhandledExceptionForm();
            StringBuilder exceptionInfo = new StringBuilder(1024);
            if (e != null)
            {
                //抛出的是与CLS兼容的未处理异常
                exceptionInfo.Append("当前未处理的异常信息:\n异常类型: ");
                exceptionInfo.Append(e.GetType());
                exceptionInfo.Append("\n异常内容: ");
                exceptionInfo.Append(e.Message);
                exceptionInfo.Append("\n方法名: ");
                MethodBase method = e.TargetSite;
                exceptionInfo.Append(method.Name);
                exceptionInfo.Append("\n方法所在的类: ");
                exceptionInfo.Append(method.ReflectedType);
                exceptionInfo.Append("\n\n调用堆栈信息:\n");
                exceptionInfo.Append(e.StackTrace);
            }
            else
            {
                //抛出的是与CLS不兼容的未处理异常
                exceptionInfo.Append(string.Format("与CLS不兼容的未处理异常:\n\n异常类型: {0}\n异常内容: {1}",
                    e.GetType(),
                    e.ToString()));
            }
            frmUnhandledException.ExceptionInfo = exceptionInfo.ToString();
            frmUnhandledException.ShowDialog();
        }

        /// <summary>
        /// 重新启动程序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void ApplicationExitHandler(object sender, EventArgs e)
        {
            //保存当前用户信息
            if (restart)
            {
                Process.Start(Application.ExecutablePath);
            }
            else
            {
                Environment.Exit(0);
            }
        }
    }
}
