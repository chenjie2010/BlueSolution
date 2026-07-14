//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CommonModuleDelegate.cs
// 描述: 通用模块的代理定义
// 作者：ChenJie 
// 编写日期：2018-07-23
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Data;
using System.Collections.Generic;
using AppFramework.Core;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient
{
    public delegate void SetCellTypeDelegate(FarPoint.Win.Spread.CellType.ICellType cellType);

    /// <summary>
    /// 更新文本代理
    /// </summary>
    /// <param name="text">文本内容</param>
    public delegate void UpdateTextCallback(string text);

    /// <summary>
    /// 获得用户信息
    /// </summary>
    /// <param name="commonUserInfo"></param>
    public delegate void GetCommonUserInfoDelegate(CommonUserInfo commonUserInfo);

    public delegate void AddSheetDelegate(string sheetName, int rowCount, int columnCount);

    public delegate void EditSheetDelegate(int sheetIndex, string sheetName);

    public delegate void DeleteSheetDelegate(int sheetIndex);

    public delegate void MoveSheetDelegate(int currentSheetIndex, int previousSheetIndex, MovedDriection movedDriection);

    public delegate void SetBackColorDelegate(int row, int col, Color color, bool save);

    public delegate void SetRowHeightAndColWidthDelegate(decimal value);

    public delegate void SetRowAndColCountDelegate(int row, int col);

    public delegate void SetReportPropertyVisibleDelegate(bool visible);

    public delegate void OpenSnapshotDelegate(decimal snapshotId);

    public delegate void SaveSnapshotDelegate(string snapshotName, DateTime expireDate, string notes);

    public delegate void DesignReportDelegate(decimal reportId, ReportCategory reportCategory);

    public delegate void RemoteSereverConfrimedDelegate(RemoteServer remoteServer);

    public delegate void RefreshFormDelegate();

    public delegate void RefreshSortingDelegate();

    public delegate void UpdateDataTableDelegate(DataTable dataTable);
}
