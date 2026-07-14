//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomSectionContract.cs
// 描述: CustomSection 契约层接口
// 作者：ChenJie 
// 编写日期：2018/8/13
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
    /// CustomSection 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomSectionContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomSectionContract : ICommonNodeContract, IPrincipalContracts<CustomSectionInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得所有的窗体
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfosByDataId")]
        IList<CustomSectionInfo> GetModelInfos(decimal dataId);

        #endregion
    }
}