//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomSnapshotContract.cs
// 描述: CustomSnapshot 契约层接口
// 作者：ChenJie 
// 编写日期：2018/9/28
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
using Blue.Model.BusinessDesignerModule;

namespace Blue.WCFContracts.BusinessDesignerModule
{
    /// <summary>
    /// CustomSnapshot 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomSnapshotContract", Namespace = "http://www.scu.edu.cn/BusinessDesignerModule/")]
    public interface ICustomSnapshotContract :  IPrincipalContracts<CustomSnapshotInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得统计类型快照列表
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonItemsByReportId")]
        IList<CommonItem<decimal>> GetCommonItems(decimal reportId);

        /// <summary>
        /// 获得基础类型快照列表
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonItemsByReportIdAndUserId")]
        IList<CommonItem<decimal>> GetCommonItems(decimal reportId, decimal userId);

        /// <summary>
        /// 下载快照数据
        /// </summary>
        /// <param name="snapshotId"></param>
        /// <returns></returns>
        [OperationContract(Name = "DownloadSnapshot")]
        byte[] DownloadSnapshot(decimal snapshotId);

        /// <summary>
        ///  插入记录与报表文件
        /// </summary>
        /// <param name="customSnapshotInfo"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        [OperationContract(Name = "Insert_0")]
        decimal Insert(CustomSnapshotInfo customSnapshotInfo, byte[] data);

        #endregion
    }
}