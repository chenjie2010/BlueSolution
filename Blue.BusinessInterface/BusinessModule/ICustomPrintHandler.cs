//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomPrintHandler.cs
// 描述: CustomPrint 业务处理类
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
using Blue.Model.BusinessModule;

namespace Blue.BusinessInterface.BusinessModule
{
    /// <summary>
    /// CustomPrint 接口
    /// </summary>
    public interface ICustomPrintHandler : ICommonNodeBusiness, IPrincipalBusiness<CustomPrintInfo>
    {
        #region 接口

        /// <summary>
        /// 向 PrintRecord 表中插入一条新记录
        /// </summary>
        /// <param name="printRecordInfo">printRecordInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
        decimal InsertPrintRecordInfo(PrintRecordInfo printRecordInfo);

        /// <summary>
        /// 获得以表 UserAndPrint 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

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
        /// 获得打印字段
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <returns></returns>
        IList<CommonNode> GetDataFields(decimal printId, byte dataFieldPrintType);

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
        /// 更新表的字段集合
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <param name="customPrintAndDataFieldInfos"></param>
        void UpdateDataFields(decimal printId, byte dataFieldPrintType, IList<CustomPrintAndDataFieldInfo> customPrintAndDataFieldInfos);

        /// <summary>
        /// 获得启用的打印对象列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        IList<CommonNode> GetActiveCommonNodes(decimal groupId);

        #endregion
    }
}