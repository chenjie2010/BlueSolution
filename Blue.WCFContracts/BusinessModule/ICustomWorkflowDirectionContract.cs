//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomWorkflowDirectionContract.cs
// 描述： CustomWorkflowDirection 契约层接口
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
    /// CustomWorkflowDirection 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomWorkflowDirectionContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomWorkflowDirectionContract :  IPrincipalContracts<CustomWorkflowDirectionInfo>
    {
		#region 自定义接口
		
        #endregion
    }
}