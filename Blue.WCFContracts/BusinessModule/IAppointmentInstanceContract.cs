//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IAppointmentInstanceContract.cs
// 描述: AppointmentInstance 契约层接口
// 作者：ChenJie 
// 编写日期：2018/8/24
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// AppointmentInstance 契约接口
    /// </summary>
    [ServiceContract(Name = "IAppointmentInstanceContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface IAppointmentInstanceContract :  IPrincipalContracts<AppointmentInstanceInfo>
    {
		#region 自定义接口
		
        #endregion
    }
}