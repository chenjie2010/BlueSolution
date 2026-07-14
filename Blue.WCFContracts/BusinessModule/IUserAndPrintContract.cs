//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IUserAndPrintContract.cs
// 描述: UserAndPrint 契约层接口
// 作者：ChenJie 
// 编写日期：2022/11/13
// Copyright 2022
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
    /// UserAndPrint 契约接口
    /// </summary>
    [ServiceContract(Name = "IUserAndPrintContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface IUserAndPrintContract :  IPrincipalContracts<UserAndPrintInfo>
    {
		#region 自定义接口
		
        #endregion
    }
}