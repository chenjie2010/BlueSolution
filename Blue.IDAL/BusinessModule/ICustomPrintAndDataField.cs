//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomPrintAndDataField.cs
// 描述: CustomPrintAndDataField 数据访问层接口
// 作者：ChenJie 
// 编写日期：2018/9/28
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
    /// CustomPrintAndDataField 接口
    /// </summary>
    public interface ICustomPrintAndDataField : ICorrelatedTable
    {
        #region 接口

        /// <summary>
        /// 更新表的字段集合
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <param name="customPrintAndDataFieldInfos"></param>
        void UpdateDataFields(decimal printId, byte dataFieldPrintType, IList<CustomPrintAndDataFieldInfo> customPrintAndDataFieldInfos);

        /// <summary>
        /// 获得打印字段
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <returns></returns>
        IList<CommonNode> GetDataFields(decimal printId, byte dataFieldPrintType);

        #endregion
    }
}