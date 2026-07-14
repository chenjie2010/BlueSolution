//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataAuditingStepContract.cs
// 描述: DataAuditingStep 契约层接口
// 作者：ChenJie 
// 编写日期：2018/10/19
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
    /// DataAuditingStep 契约接口
    /// </summary>
    [ServiceContract(Name = "IDataAuditingStepContract", Namespace = "http://www.scu.edu.cn/BusinessDesignerModule/")]
    public interface IDataAuditingStepContract :  IPrincipalContracts<DataAuditingStepInfo>
    {
        #region 自定义接口

        /// <summary>
        /// 获得最新提交人
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetLastestSubmitter")]
        CommonNode GetLastestSubmitter(decimal auditingLogId);

        /// <summary>
        /// 获得以表 DataAuditingStep 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        [OperationContract(Name = "GetDataAuditingSteps")]
        DataSet GetDataAuditingSteps(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 获得日志流程
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetSteps")]
        DataSet GetSteps(decimal auditingLogId);

        /// <summary>
        /// 获得最新审核人
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        [OperationContract(Name = "GetLastestReviewer")]
        CommonNode GetLastestReviewer(decimal auditingLogId);

        #endregion
    }
}