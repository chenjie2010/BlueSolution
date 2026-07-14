//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomPrint.cs
// 描述: CustomPrint 数据访问层接口
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
    /// CustomPrint 接口
    /// </summary>
    public interface ICustomPrint : ICommonNode, IPrincipalTable<CustomPrintInfo>
    {
        #region 接口

        /// <summary>
        /// 获得表的类型
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        byte GetTableType(decimal printId);

        /// <summary>
        /// 获得打印内容
        /// </summary>
        ///<param name="printId">打印编号</param>
        /// <returns> 打印内容</returns>
        string GetPrintContent(decimal printId);

        /// <summary>
        /// 获得打印系统字段
        /// </summary>
        ///<param name="printId">打印编号</param>
        /// <returns> 表的逻辑名称</returns>
        Int64 GetSystemDataField(decimal printId);

        /// <summary>
        /// 更新打印内容
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="printContent"></param>
        void UpdatePrintContent(decimal printId, string printContent);

        /// <summary>
        /// 更新打印内容
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="printContent"></param>
        /// <param name="upLoadFileInfos"></param>
        void UpdatePrintContent(decimal printId, string printContent, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 获得满足条件的打印对象
        /// </summary>
        /// <param name="groupId"></param>
        /// <param name="visible"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal groupId, bool visible);

        #endregion
    }
}