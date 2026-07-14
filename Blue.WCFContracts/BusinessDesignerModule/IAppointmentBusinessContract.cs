//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IAppointmentBusinessContract.cs
// 描述: AppointmentBusiness 契约层接口
// 作者：ChenJie 
// 编写日期：2018/9/28
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
using Blue.Model.BusinessDesignerModule;

namespace Blue.WCFContracts.BusinessDesignerModule
{
    /// <summary>
    /// AppointmentBusiness 契约接口
    /// </summary>
    [ServiceContract(Name = "IAppointmentBusinessContract", Namespace = "http://www.scu.edu.cn/BusinessDesignerModule/")]
    public interface IAppointmentBusinessContract : ICommonNodeContract, IPrincipalContracts<AppointmentBusinessInfo>
    {
		#region 自定义接口
		
        #endregion
    }
}