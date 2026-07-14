//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: TestConnectionService.cs
// 描述: 测试连接类
// 作者：ChenJie 
// 编写日期：2010-07-13
// 版权所有 (C) 四川大学 2010
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.ServiceModel;
using System.Collections.Generic;
using Blue.WCFContracts;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;

namespace Blue.WCFServices
{
    /// <summary>
    /// 系统服务类
    /// ConcurrencyMode 默认值为 ConcurrencyMode.Single
    /// InstanceContextMode 默认值为 InstanceContextMode.PerSession
    /// </summary> 
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DuplexService : IDuplexChannelContract, IDisposable
    {
        #region 成员变量

        private static readonly Hashtable systemServiceCallBackDictionary = Hashtable.Synchronized(new Hashtable());

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DuplexService()
        {

        }

        #endregion

        #region 服务方法

        /// <summary>
        /// 注册回调函数
        /// </summary>     
        public void RegisterSystemServiceCallBack()
        {
            systemServiceCallBackDictionary[OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name] = OperationContext.Current.GetCallbackChannel<IDuplexChannelCallBackContract>();
            OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
            OperationContext.Current.Channel.Faulted += new EventHandler(Channel_Faulted);
        }

        /// <summary>
        /// 向指定的客户端发送消息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="message"></param>
        public void SendMessage(string userName, SystemMessage message)
        {
            if (systemServiceCallBackDictionary.ContainsKey(userName))
            {
                try
                {
                    ((IDuplexChannelCallBackContract)systemServiceCallBackDictionary[userName]).PopupMessage(message);
                }
                catch
                {
                    systemServiceCallBackDictionary.Remove(userName);
                }
            }
        }

        /// <summary>
        /// 客户端请求服务器端向所有的注册客户端发送消息
        /// </summary>
        /// <param name="message"></param>
        public void SendMessage(SystemMessage message)
        {
            IList<string> deletedKeys = new List<string>();
            foreach (KeyValuePair<string, IDuplexChannelCallBackContract> item in systemServiceCallBackDictionary)
            {
                try
                {
                    item.Value.PopupMessage(message);
                }
                catch
                {
                    deletedKeys.Add(item.Key);
                }
            }
            foreach (string key in deletedKeys)
            {
                systemServiceCallBackDictionary.Remove(key);
            }
        }

        /// <summary>
        /// 激活通道
        /// </summary>        
        public void ActiveChannel()
        {
            /* 无操作，目的是保证通道激活状态 */
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 通道异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Channel_Faulted(object sender, EventArgs e)
        {
            try
            {
                systemServiceCallBackDictionary.Remove(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name);
            }
            catch { }
        }

        /// <summary>
        /// 通道关闭方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Channel_Closing(object sender, EventArgs e)
        {
            try
            {
                systemServiceCallBackDictionary.Remove(OperationContext.Current.ServiceSecurityContext.PrimaryIdentity.Name);
            }
            catch { }
        }


        #endregion

        #region 公有方法

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            systemServiceCallBackDictionary.Clear();
        }

        #endregion
    }
}
