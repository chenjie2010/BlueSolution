//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ISystemService.cs
// 描述: 系统契约
// 作者：ChenJie 
// 编写日期：2010-07-09
// 版权所有 (C) 四川大学 2010
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;

namespace Blue.WCFContracts
{
    /// <summary>
    /// 系统服务契约
    /// </summary>
    [ServiceContract(Name = "IDuplexServiceContract", Namespace = "http://www.scu.edu.cn/", CallbackContract = typeof(IDuplexChannelCallBackContract))]
    public interface IDuplexChannelContract
    {        
        /// <summary>
        /// 注册回调函数
        /// </summary>     
        [OperationContract(Name = "RegisterSystemServiceCallBack", IsOneWay = true)]
        void RegisterSystemServiceCallBack();  
      
        /// <summary>
        /// 向指定的客户端发送消息
        /// </summary>
        /// <param name="userName">用户名</param>
        /// <param name="message"></param>
        [OperationContract(Name = "SendMessageByUserName", IsOneWay = true)]
        void SendMessage(string userName, SystemMessage message);

        /// <summary>
        /// 客户端请求服务器端向所有的注册客户端发送消息
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(Name = "SendMessage", IsOneWay = true)]
        void SendMessage(SystemMessage message);

        /// <summary>
        /// 激活通道
        /// </summary>
        [OperationContract(Name = "ActiveChannel", IsOneWay = true)]
        void ActiveChannel();
    }
}
