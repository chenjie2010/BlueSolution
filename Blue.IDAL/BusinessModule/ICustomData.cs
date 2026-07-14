//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：ICustomData.cs
// 描述：CustomData 数据访问层接口
// 作者：ChenJie 
// 编写日期：2017/11/27
// Copyright 2017
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
    /// CustomData 接口
    /// </summary>
    public interface ICustomData : ICommonNode, IPrincipalTable<CustomDataInfo>
    {
        #region 接口

        /// <summary>
        /// 获得填报属性
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        byte GetDataFilledProperty(decimal dataId);

        /// <summary>
        /// 根据当前数据编号、用户填报编号、当前业务状态，获取下一步的评审人列表。
        /// </summary>
        /// <param name="dataId"></param>
        /// <returns></returns>
        Dictionary<decimal, string> GetFinalReviewers(decimal dataId);

        /// <summary>
        /// 根据当前数据编号、用户填报编号、当前业务状态，获取下一步的评审人列表。
        /// </summary>
        /// <param name="dataId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Dictionary<decimal, string> GetInitReviewers(decimal dataId, decimal userId);

        /// <summary>
        /// 向 CustomData 表中插入一条新记录
        /// </summary>
        /// <param name="customDataInfo">customDataInfo 对象</param>
        /// <param name="upLoadFileInfos">附件列表</param>
        /// <param name="conditionalUpLoadFileInfos">条件附件列表</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal Insert(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos);

        /// <summary>
        /// 更新数据填报和附件信息
        /// </summary>
        /// <param name="customDataInfo"></param>
        /// <param name="upLoadFileInfos"></param>
        /// <param name="conditionalUpLoadFileInfos">条件附件列表</param>
        void Update(CustomDataInfo customDataInfo, IList<ExtendedUpLoadFileInfo> upLoadFileInfos, IList<ExtendedUpLoadFileInfo> conditionalUpLoadFileInfos);

        #endregion
    }
}