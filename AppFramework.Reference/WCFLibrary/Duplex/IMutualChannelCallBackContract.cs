//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IFlexibleSystemServiceCallBackContract.cs
// 描述: 系统服务回调契约
// 作者：ChenJie 
// 编写日期：2011-06-02
// 版权所有 (C) 四川大学 2011
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;

namespace AppFramework.Reference.WCFLibrary
{
    /// <summary>
    /// 系统服务回调契约，该接口的方法有返回参数
    /// 仅作为带返回参数方法的示例, 业务无实际意义
    /// </summary>
    public interface IMutualChannelCallBackContract
    {
        /// <summary>
        /// 处理消息
        /// </summary>
        /// <param name="message"></param>
        /// <returns>消息内容</returns>
        [OperationContract(Name = "HandleMessage")]
        string HandleMessage(SystemMessage SystemMessage);  
    }
}
