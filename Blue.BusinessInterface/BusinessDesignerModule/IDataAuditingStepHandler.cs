//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: IDataAuditingStepHandler.cs
// 描述: DataAuditingStep 业务处理类
// 作者：ChenJie 
// 编写日期：2018/10/19
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessDesignerModule;

namespace Blue.BusinessInterface.BusinessDesignerModule
{
    /// <summary>
    /// DataAuditingStep 接口
    /// </summary>
    public interface IDataAuditingStepHandler : IPrincipalBusiness<DataAuditingStepInfo>
    {
        #region 接口

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
        DataSet GetDataAuditingSteps(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 获得日志流程
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        DataSet GetSteps(decimal auditingLogId);

        /// <summary>
        /// 获得最新审核人
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        CommonNode GetLastestReviewer(decimal auditingLogId);

        /// <summary>
        /// 获得最新提交人
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        CommonNode GetLastestSubmitter(decimal auditingLogId);

        #endregion
    }
}