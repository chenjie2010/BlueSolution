//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IMutualChannelServiceContract.cs
// 描述: 系统服务回调契约
// 作者：ChenJie 
// 编写日期：2016-08-16
// 版权所有 (C) 四川大学 2016
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
    /// 仅作为带返回参数方法的示例, 业务无实际意义
    /// </summary>
    [ServiceContract(Name = "IMutualChannelServiceContract", Namespace = "http://www.scu.edu.cn/", CallbackContract = typeof(IMutualChannelCallBackContract))]
    public interface IMutualChannelContract
    {
        /// <summary>
        /// 注册回调函数
        /// </summary>
        /// <returns>注册是否成功</returns>
        [OperationContract(Name = "RegisterFlexibleSystemServiceCallBack")]
        bool RegisterFlexibleSystemServiceCallBack();

        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns>消息内容</returns>
        [OperationContract(Name = "ShowMessage")]
        string ShowMessage(SystemMessage message);

        /// <summary>
        /// 激活通道
        /// </summary>
        [OperationContract(Name = "ActiveChannel")]
        void ActiveChannel();
    }
}
