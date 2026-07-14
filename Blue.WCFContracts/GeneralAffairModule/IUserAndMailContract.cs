//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： IUserAndMailContract.cs
// 描述： UserAndMail 契约层接口
// 作者：ChenJie 
// 编写日期：2017/9/12
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.GeneralAffairModule;

namespace Blue.WCFContracts.GeneralAffairModule
{
    /// <summary>
    /// UserAndMail 契约接口
    /// </summary>
    [ServiceContract(Name = "IUserAndMailContract", Namespace = "http://www.scu.edu.cn/CommonModule/")]
    public interface IUserAndMailContract
    {
        //ICorrelatedContracts<UserAndMailInfo>
        #region 自定义接口



        #endregion
    }
}