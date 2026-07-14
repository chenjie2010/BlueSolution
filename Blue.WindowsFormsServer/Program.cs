using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceProcess;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.WindowsFormsServer
{
    public delegate void WindowsServiceHandlerDelegate(WindowsServiceStep windowsServiceStep);

    static class Program
    {
        #region 构造函数

        /// <summary>
        /// 应用程序的主入口点 。
        /// </summary>
        [STAThread]
        static void Main()
        {
            DevExpress.UserSkins.BonusSkins.Register();
            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.LookAndFeel.UserLookAndFeel.Default.Style = DevExpress.LookAndFeel.LookAndFeelStyle.Skin;
            DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("Blue");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("zh-CN");
            bool ret;
            System.Threading.Mutex m = new System.Threading.Mutex(true, Application.ProductName, out ret);
            if (ret)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm());
                m.ReleaseMutex();
            }
            else
            {
                Application.Exit();//退出程序   
            }
        }

        #endregion
    }
}
