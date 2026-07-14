//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomPrintContract.cs
// 描述: CustomPrint 契约层接口
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
using Blue.Model.BusinessModule;

namespace Blue.WCFContracts.BusinessModule
{
    /// <summary>
    /// CustomPrint 契约接口
    /// </summary>
    [ServiceContract(Name = "ICustomPrintContract", Namespace = "http://www.scu.edu.cn/BusinessModule/")]
    public interface ICustomPrintContract : ICommonNodeContract, IPrincipalContracts<CustomPrintInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 向 UserAndPrint 表中插入一条新记录
        /// </summary>
        /// <param name="printRecordInfo">userAndPrintInfo 对象</param>
        [OperationContract(Name = "InsertPrintRecordInfo")]
        void InsertPrintRecordInfo(PrintRecordInfo printRecordInfo);


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
        [OperationContract(Name = "GetPageRecordOfMultiTables")]
        DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);
        
        /// <summary>
        /// 获得打印内容
        /// </summary>
        ///<param name="printId">打印编号</param>
        /// <returns> 打印内容</returns>
        [OperationContract(Name = "GetPrintContent")]
        string GetPrintContent(decimal printId);

        /// <summary>
        /// 获得打印系统字段
        /// </summary>
        ///<param name="printId">打印编号</param>
        /// <returns> 表的逻辑名称</returns>
        [OperationContract(Name = "GetSystemDataField")]
        Int64 GetSystemDataField(decimal printId);

        /// <summary>
        /// 更新打印内容
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="printContent"></param>
        [OperationContract(Name = "UpdatePrintContent")]
        void UpdatePrintContent(decimal printId, string printContent);

        /// <summary>
        /// 更新打印内容
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="printContent"></param>
        /// <param name="upLoadFileInfos"></param>
        [OperationContract(Name = "UpdatePrintContentWithFiles")]
        void UpdatePrintContent(decimal printId, string printContent, IList<ExtendedUpLoadFileInfo> upLoadFileInfos);

        /// <summary>
        /// 更新表的字段集合
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <param name="customPrintAndDataFieldInfos"></param>
        [OperationContract(Name = "UpdateDataFields")]
        void UpdateDataFields(decimal printId, byte dataFieldPrintType, IList<CustomPrintAndDataFieldInfo> customPrintAndDataFieldInfos);

        /// <summary>
        /// 获得打印字段
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetDataFields")]
        IList<CommonNode> GetDataFields(decimal printId, byte dataFieldPrintType);

        #endregion
    }
}