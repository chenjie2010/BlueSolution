//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomWorkflowProcessAndRoleContract.cs
// 描述： CustomWorkflowProcessAndRole 契约层接口
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// CustomWorkflowProcessAndRole 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomWorkflowProcessAndRoleContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomWorkflowProcessAndRoleContract :  IPrincipalContracts<CustomWorkflowProcessAndRoleInfo>
    {
		#region 自定义接口
		
        #endregion
    }
}