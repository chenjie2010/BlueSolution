//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomSheetContract.cs
// 描述: CustomSheet 契约层接口
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
    /// CustomSheet 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomSheetContract", Namespace = "http://www.scu.edu.cn/BusinessDesignerModule/")]
    public interface ICustomSheetContract : ICommonNodeContract, IPrincipalContracts<CustomSheetInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="sheetNames"></param>
        [OperationContract(Name = "UpdateSheetSorting")]
        void UpdateSheetSorting(decimal reportId, IList<string> sheetNames);

        /// <summary>
        /// 获得报表文件
        /// </summary>
        /// <param name="reportId">报表编号</param>
        /// <returns></returns>
        [OperationContract(Name = "DownloadReportFile")]
        byte[] DownloadReportFile(decimal reportId);

        /// <summary>
        /// 保存报表文件
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="reportType"></param>
        /// <param name="data"></param>
        /// <param name="rowAndCols"></param>
        [OperationContract(Name = "UploadReportFile")]
        void UploadReportFile(decimal reportId, byte[] data, Dictionary<decimal, RowAndCol> rowAndCols);

        /// <summary>
        /// 获得 CustomSheetInfo 对象的列表
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetModelInfosByReportId")]
        IList<CustomSheetInfo> GetModelInfos(decimal reportId);

        /// <summary>
        /// 批文编号自动加1
        /// </summary>
        /// <param name="sheetId"></param>
        [OperationContract(Name = "AutoIncreaseApprovalNumber")]
        void AutoIncreaseApprovalNumber(decimal sheetId);

        /// <summary>
        /// 更新边距
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="customMargin"></param>
        [OperationContract(Name = "UpdateMargin")]
        void UpdateMargin(decimal sheetId, CustomMargin customMargin);

        /// <summary>
        /// 获得边距
        /// </summary>
        /// <param name="sheetId"></param>
        [OperationContract(Name = "GetMargin")]
        CustomMargin GetMargin(decimal sheetId);

        /// <summary>
        /// 更新样表的行列数
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        [OperationContract(Name = "UpdateRowAndColValue")]
        void Update(decimal sheetId, int rowCount, int columnCount);

        /// <summary>
        /// 获得样表的行列数
        /// </summary>
        /// <param name="SheetId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetRowAndColCountBySheetId")]
        RowAndCol GetRowAndColCountBySheetId(decimal sheetId);

        /// <summary>
        /// 获得表套的样表行列数
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetRowAndColCount")]
        IList<RowAndCol> GetRowAndColCount(decimal reportId);

        /// <summary>
        /// 导入 Excel 格式的文件
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="sheetNames"></param>
        [OperationContract(Name = "InsertSheetNames")]
        void Insert(decimal reportId, IList<CustomSheetInfo> sheetNames);

        #endregion
    }
}