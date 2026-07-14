//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomSnapshotHandler.cs
// 描述: CustomSnapshot 业务处理类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessDesignerModule;

namespace Blue.BusinessInterface.BusinessDesignerModule
{
    /// <summary>
    /// CustomSnapshot 接口
    /// </summary>
    public interface ICustomSnapshotHandler : IPrincipalBusiness<CustomSnapshotInfo>
    {
        #region 接口

        /// <summary>
        /// 获得统计类型快照列表
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        IList<CommonItem<decimal>> GetCommonItems(decimal reportId);

        /// <summary>
        /// 获得基础类型快照列表
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<CommonItem<decimal>> GetCommonItems(decimal reportId, decimal userId);

        /// <summary>
        /// 下载快照数据
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <returns></returns>
        byte[] DownloadSnapshot(decimal snapshotId);

        /// <summary>
        ///  插入记录与报表文件
        /// </summary>
        /// <param name="customSnapshotInfo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        decimal Insert(CustomSnapshotInfo customSnapshotInfo, byte[] data);

        #endregion
    }
}