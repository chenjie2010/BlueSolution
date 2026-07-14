using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DevExpress.XtraSplashScreen;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.Core.ClientConfig;
using Blue.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts;

namespace Blue.WindowsFormsClient
{
    public partial class SplashScreenForm : SplashScreen
    {        
        #region 内部成员变量

        /// <summary>
        /// 取消登录
        /// </summary>
        private bool cancelLogin = false;

        #endregion

        #region 属性
        
        /// <summary>
        /// 取消登录
        /// </summary>
        public bool CancelLogin
        {
            set
            {
                cancelLogin = false;
            }
            get
            {
                return cancelLogin;
            }
        }
        
        /// <summary>
        /// 取消测试网络
        /// </summary>
        public MethodInvoker CancelToTestNetwork
        {
            get;set;
        }

        #endregion

        #region 构造函数

        public SplashScreenForm()
        {
            InitializeComponent();
            cancelLogin = false;
            try
            {
                ChannelFactoryCreator.SetServerAddressAndPort(CurrentConfig.Instance.ServerAddress, CurrentConfig.Instance.Port);
                ICommonUtilContract commonUtilContract = CommonFactory.CreateCommonUtilContract();
                string domainName = commonUtilContract.GetDomainName();
                if (domainName.ToLower().StartsWith("scu"))
                {
                    lblCompany.Text = AppSettingHelper.ScuCompanyName;
                    pesScu.Visible = true;
                    peHREMS.Visible = false;
                    peDefault.Visible = false;
                }
                else
                {
                    lblCompany.Text = AppSettingHelper.DefaultCompanyName;
                    peDefault.Visible = true;
                    pesScu.Visible = false;
                    peHREMS.Visible = true;
                }
            }
            catch
            {
            }
        }

        #endregion

        #region Overrides

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="arg"></param>
        public override void ProcessCommand(Enum cmd, object arg)
        {
            string text = string.Empty;
            if (lblProcessText.InvokeRequired)
            {
                LoginedStep loginedStep = (LoginedStep)cmd;                
                switch (loginedStep)
                {
                    case LoginedStep.TestConnection:
                        text = "1. 正在测试网络连接...";
                        break;

                    case LoginedStep.CheckSystemTime:
                        text = "2. 正在检查当前计算机时间...";
                        break;

                    case LoginedStep.CheckVersionConsistency:
                        text = "3. 正在客户端与服务器端版本一致性...";
                        break;

                    case LoginedStep.Validator:
                        text = "4. 正在验证用户名和密码...";
                        break;

                    case LoginedStep.Load:
                        text = "5. 正在加应用程序界面...";
                        break;
                }
                lblProcessText.Invoke(new UpdateTextHandler(UpdateText), new object[] { text });
            }
            base.ProcessCommand(cmd, arg);
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 取消登录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkCancel_Click(object sender, EventArgs e)
        {
            if (CancelToTestNetwork != null)
            {
                CancelToTestNetwork();
            }
            cancelLogin = true;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 更新加载过程
        /// </summary>
        /// <param name="progress"></param>
        private void UpdateText(string progress)
        {
            lblProcessText.Text = progress;
            Application.DoEvents();
        }

        #endregion

    }
}