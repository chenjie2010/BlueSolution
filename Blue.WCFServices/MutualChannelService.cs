//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: FlexibleSystemService.cs
// 描述: 系统服务类
// 作者：ChenJie 
// 编写日期：2016-08-16
// 版权所有 (C) 四川大学 2016
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
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class MutualChannelService : IMutualChannelContract, IDisposable
    {
        #region 成员变量

        private readonly IList<IMutualChannelCallBackContract> systemServiceCallBackList;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MutualChannelService()
        {
            systemServiceCallBackList = new List<IMutualChannelCallBackContract>();
        }

        #endregion

        #region 服务方法

        /// <summary>
        /// 注册回调函数
        /// </summary>
        /// <returns>是否注册成功</returns>          
        public bool RegisterFlexibleSystemServiceCallBack()
        {
            bool result = true;

            try
            {
                systemServiceCallBackList.Add(OperationContext.Current.GetCallbackChannel<IMutualChannelCallBackContract>());
                OperationContext.Current.Channel.Closing += new EventHandler(Channel_Closing);
                OperationContext.Current.Channel.Faulted += new EventHandler(Channel_Faulted);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns>消息内容</returns>
        public string ShowMessage(SystemMessage message)
        {
            for (int i = systemServiceCallBackList.Count - 1; i >= 0; i--)
            {
                try
                {
                    systemServiceCallBackList[i].HandleMessage(message);
                }
                catch
                {
                    RemoveSystemServiceCallBackContract(i);
                }
            }

            return message.MessageTitle;
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
            lock ((systemServiceCallBackList as ICollection).SyncRoot)
            {
                systemServiceCallBackList.Remove((IMutualChannelCallBackContract)sender);
            }
        }

        /// <summary>
        /// 通道关闭方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Channel_Closing(object sender, EventArgs e)
        {
            lock ((systemServiceCallBackList as ICollection).SyncRoot)
            {
                systemServiceCallBackList.Remove((IMutualChannelCallBackContract)sender);
            }
        }

        /// <summary>
        /// 移除回调函数
        /// </summary>
        /// <param name="index"></param>
        private void RemoveSystemServiceCallBackContract(int index)
        {
            lock ((systemServiceCallBackList as ICollection).SyncRoot)
            {
                systemServiceCallBackList.RemoveAt(index);
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            systemServiceCallBackList.Clear();
        }

        #endregion
    }
}
