//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: NetworkConnection.cs
// 描述: 网络连接测试类
// 作者：ChenJie 
// 编写日期：2016-08-07
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blue.CustomLibrary;
using AppFramework.Core.ClientConfig;
using Blue.WCFContracts;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 网络连接测试类
    /// </summary>
    public class NetworkConnection
    {
        #region 内部成员变量

        /// <summary>
        /// 是否取消测试
        /// </summary>
        private bool _cancelTest;

        /// <summary>
        /// 测试时间处理委托
        /// </summary>
        private TimerHandler _timerHandler;

        #endregion

        #region 属性

        /// <summary>
        /// 测试时间处理委托
        /// </summary>
        public TimerHandler TimerHandler
        {
            set
            {
                _timerHandler = value;
            }
        }

        /// <summary>
        /// 是否取消测试
        /// </summary>
        public bool CancelTest
        {
            set
            {
                _cancelTest = value;
            }
            get
            {
                return _cancelTest;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public NetworkConnection()
        {
            _cancelTest = false;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cancelTest">是否取消测试</param>
        public NetworkConnection(bool cancelTest)
        {
            _cancelTest = cancelTest;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 测试连接是否成功
        /// </summary>
        /// <param name="serverAddress">服务器地址</param>
        /// <param name="uesrName"></param>
        /// <param name="password"></param>
        /// <returns>测试结果</returns>
        public bool TestRemoteConnection(string serverAddress, string uesrName, string password)
        {
            bool result = false; ;
            UserValidator userValidator = new UserValidator();
            AsynTestServiceCaller caller1 = new AsynTestServiceCaller(userValidator.TestConnection);
            IRemoteServerContract remoteDataContract = RemoteChannelFactory.CreateRemoteServerContract(serverAddress, CurrentConfig.Instance.Port);
            AsynRemoteTestServiceCaller caller = delegate (string name, string pwd, out bool r)
            {
                r = false;
                try
                {
                    r = remoteDataContract.TestRemoteConnection(name, pwd);
                }
                catch { };
            };
            IAsyncResult iAsyncResult = caller.BeginInvoke(uesrName, password, out result, null, null);
            while (!iAsyncResult.IsCompleted)
            {
                Application.DoEvents();
                Thread.Sleep(50);
                if (_timerHandler != null)
                {
                    _timerHandler(50);
                }
                if (_cancelTest)
                {
                    return false;
                }
            }
            caller.EndInvoke(out result, iAsyncResult);

            return result;
        }

        /// <summary>
        /// 测试连接是否成功
        /// </summary>
        /// <param name="serverAddress">服务器地址</param>
        /// <param name="uesrName"></param>
        /// <param name="password"></param>
        /// <returns>测试结果</returns>
        public bool TestConnection(string serverAddress, string uesrName, string password)
        {
            bool result = false; ;
            UserValidator userValidator = new UserValidator();
            AsynTestServiceCaller caller = new AsynTestServiceCaller(userValidator.TestConnection);
            IAsyncResult iAsyncResult = caller.BeginInvoke(serverAddress, uesrName, password, out result, null, null);
            while (!iAsyncResult.IsCompleted)
            {
                Application.DoEvents();
                Thread.Sleep(50);
                if (_timerHandler != null)
                {
                    _timerHandler(50);
                }
                if (_cancelTest)
                {
                    return false;
                }
            }
            caller.EndInvoke(out result, iAsyncResult);

            return result;
        }

        /// <summary>
        /// 测试连接是否成功
        /// </summary>
        /// <param name="serverAddress">服务器地址</param>
        /// <returns>测试结果</returns>
        public bool TestConnection(string serverAddress)
        {
            bool result = false; ;
            UserValidator userValidator = new UserValidator();
            AsynTestConnectionCaller caller = new AsynTestConnectionCaller(userValidator.TestConnection);
            IAsyncResult iAsyncResult = caller.BeginInvoke(serverAddress, out result, null, null);
            while (!iAsyncResult.IsCompleted)
            {
                Application.DoEvents();
                Thread.Sleep(50);
                if (_timerHandler != null)
                {
                    _timerHandler(50);
                }
                if (_cancelTest)
                {
                    return false;
                }
            }
            caller.EndInvoke(out result, iAsyncResult);

            return result;
        }

        #endregion
    }
}
