//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomSection.cs
// 描述: CustomSection 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/8/13
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.IDAL.BusinessModule
{
    /// <summary>
    /// CustomSection 接口
    /// </summary>
    public interface ICustomSection : ICommonNode, IPrincipalTable<CustomSectionInfo>
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