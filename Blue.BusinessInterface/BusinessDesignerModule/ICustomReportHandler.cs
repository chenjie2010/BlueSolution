//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomReportHandler.cs
// 描述：CustomReport 业务处理类
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessDesignerModule;

namespace Blue.BusinessInterface.BusinessDesignerModule
{
/// <summary>
    /// CustomReport 接口
    /// </summary>
    public interface ICustomReportHandler: ICommonNodeBusiness, IPrincipalBusiness<CustomReportInfo>
    {
        #region 接口

        /// <summary>
        /// 根据报表类型条件获得报表节点
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="reportCategory"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal groupId, ReportCategory reportCategory);

        /// <summary>
        /// 获得报表名称
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        string GetReportName(decimal reportId);

        /// <summary>
        /// 样表另存
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="reportId"></param>
        /// <param name="data"></param>
        void SheetSaveAs(decimal sheetId, decimal reportId, byte[] data);

        /// <summary>
        /// 表套另存
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="groupId"></param>
        void SaveAs(decimal reportId, decimal groupId);

        /// <summary>
        /// 获得表套的所属数据仓库
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        byte GetDataWarehouseId(decimal reportId);

        #endregion
    }
}
