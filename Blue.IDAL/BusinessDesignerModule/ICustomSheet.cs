//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomSheet.cs
// 描述: CustomSheet 数据访问层接口
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
using Blue.Model.BusinessDesignerModule;

namespace Blue.IDAL.BusinessDesignerModule
{
    /// <summary>
    /// CustomSheet 接口
    /// </summary>
    public interface ICustomSheet : ICommonNode, IPrincipalTable<CustomSheetInfo>
    {
        #region 接口

        /// <summary>
        /// 获得报表文件
        /// </summary>
        /// <param name="reportId">报表编号</param>
        /// <returns></returns>
        byte[] DownloadReportFile(decimal reportId);

        /// <summary>
        /// 保存报表文件
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="reportType"></param>
        /// <param name="data"></param>
        /// <param name="rowAndCols"></param>
        void UploadReportFile(decimal reportId, byte[] data, Dictionary<decimal, RowAndCol> rowAndCols);

        /// <summary>
        /// 获得 CustomSheetInfo 对象的列表
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        IList<CustomSheetInfo> GetModelInfos(decimal reportId);

        /// <summary>
        /// 批文编号自动加1
        /// </summary>
        /// <param name="sheetId"></param>
        void AutoIncreaseApprovalNumber(decimal sheetId);

        /// <summary>
        /// 更新边距
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="customMargin"></param>
        void UpdateMargin(decimal sheetId, CustomMargin customMargin);

        /// <summary>
        /// 获得边距
        /// </summary>
        /// <param name="sheetId"></param>
        CustomMargin GetMargin(decimal sheetId);

        /// <summary>
        /// 更新样表的行列数
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        void Update(decimal sheetId, int rowCount, int columnCount);
        
        /// <summary>
        /// 获得样表的行列数
        /// </summary>
        /// <param name="SheetId"></param>
        /// <returns></returns>
        RowAndCol GetRowAndColCountBySheetId(decimal sheetId);

        /// <summary>
        /// 获得表套的样表行列数
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        IList<RowAndCol> GetRowAndColCount(decimal reportId);

        /// <summary>
        /// 导入 Excel 格式的文件
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="sheetNames"></param>
        void Insert(decimal reportId, IList<CustomSheetInfo> sheetNames);

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="sheetNames"></param>
        void UpdateSheetSorting(decimal reportId, IList<string> sheetNames);

        #endregion
    }
}