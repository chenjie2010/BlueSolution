//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDuplexServiceCallBackContract.cs
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
using AppFramework.Core.CommonClass;

namespace Blue.WCFContracts
{
    /// <summary>
    /// 系统服务回调契约，该接口的方法无返回参数
    /// </summary>   
    public interface IDuplexChannelCallBackContract
    {
        /// <summary>
        /// 在用户主界面右下角弹出消息对话框
        /// </summary>
        /// <param name="message"></param>
        [OperationContract(Name = "PopupMessage", IsOneWay = true)]
        void PopupMessage(SystemMessage message); 
    }
}
