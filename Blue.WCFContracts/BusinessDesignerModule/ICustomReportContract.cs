//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： ICustomReportContract.cs
// 描述： CustomReport 契约层接口
// 作者：ChenJie 
// 编写日期：2017/10/9
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization;
using System.ServiceModel;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessDesignerModule;

namespace Blue.WCFContracts.BusinessDesignerModule
{
    /// <summary>
    /// CustomReport 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomReportContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomReportContract : ICommonNodeContract, IPrincipalContracts<CustomReportInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 根据报表类型条件获得报表节点
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="reportCategory"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetCommonNodesByConditions")]
        IList<CommonNode> GetCommonNodes(decimal groupId, ReportCategory reportCategory);

        /// <summary>
        /// 样表另存
        /// </summary>
        /// <param name="sheetId"></param>
        /// <param name="reportId"></param>
        /// <param name="data"></param>
        [OperationContract(Name = "SheetSaveAs")]
        void SheetSaveAs(decimal sheetId, decimal reportId, byte[] data);

        /// <summary>
        /// 表套另存
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="groupId"></param>
        [OperationContract(Name = "SaveAs")]
        void SaveAs(decimal reportId, decimal groupId);

        /// <summary>
        /// 获得表套的所属数据仓库
        /// </summary>
        /// <param name="reportId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataWarehouseId")]
        byte GetDataWarehouseId(decimal reportId);

        #endregion
    }
}