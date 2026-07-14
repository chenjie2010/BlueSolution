//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomSectionHandler.cs
// 描述: CustomSection 业务处理类
// 作者：ChenJie 
// 编写日期：2018/8/13
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.BusinessInterface.BusinessModule
{
    /// <summary>
    /// CustomSection 接口
    /// </summary>
    public interface ICustomSectionHandler : ICommonNodeBusiness, IPrincipalBusiness<CustomSectionInfo>
    {
        #region 接口

        /// <summary>
        /// 获得所有的窗体
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        IList<CustomSectionInfo> GetModelInfos(decimal dataId);

        #endregion
    }
}