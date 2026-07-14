//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomAssociationHandler.cs
// 描述：CustomAssociation 业务处理类
// 作者：ChenJie 
// 编写日期：2016/10/2
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.BusinessLibrary;
using Blue.Model.BusinessModule;

namespace Blue.BusinessInterface.BusinessModule
{
/// <summary>
    /// CustomAssociation 接口
    /// </summary>
    public interface ICustomAssociationHandler: ICommonNodeBusiness, IPrincipalBusiness<CustomAssociationInfo>
    {
        #region 接口

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        /// <param name="movedDriection"></param>
        void UpdateRecordSorting(decimal associationId, decimal recordId, MovedDriection movedDriection);

        /// <summary>
        /// 获得指定列数据
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        DataTable GetAssociationColumnData(decimal associatedDataFieldId);

        /// <summary>
        /// 该表中的字段属于物理字段的关联字段类型的个数
        /// </summary>
        /// <param name="associationCode"></param>
        /// <returns></returns>
        int GetDataFieldCountConnected(string associationCode);

        /// <summary>
        /// 重置关联表
        /// </summary>
        /// <param name="associationId"></param>
        void ResetTable(decimal associationId);

        /// <summary>
        /// 该表是否有字段属于物理字段的关联字段类型
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        bool HasDataFieldConnected(decimal associationId);

        /// <summary>
        /// 导入业务数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="dataTable"></param>
        void ImportDataTable(decimal associationId, DataTable dataTable);

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        DataSet GetPageRecord(IList<decimal> groupIds);

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataSet GetAssociationData(decimal associationId, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount);

        /// <summary>
        /// 是否是超大关联
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        bool GetSuperAssociationEnabled(decimal associationId);

        /// <summary>
        /// 在关联表中增加记录
        /// </summary>
        /// <param name="associationId">关联编号</param>
        /// <param name="commonDataFields"></param>
        /// <returns></returns>
        decimal Insert(decimal associationId, IList<CommonDataField> commonDataFields);


        /// <summary>
        /// 更新关联表中的记录
        /// </summary>
        /// <param name="associationId">关联编号</param>
        /// <param name="recordId">关联表记录编号</param>
        /// <param name="commonDataFields"></param>
        void Update(decimal associationId, decimal recordId, IList<CommonDataField> commonDataFields);

        /// <summary>
        /// 删除关联表的记录
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        void Delete(decimal associationId, decimal recordId);

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        DataTable GetAssociationData(decimal associationId);

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        DataTable GetAssociationDataWithSortingDataField(decimal associationId);

        /// <summary>
        /// 根据关联编号对于的表获得相应的记录行
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        DataTable GetAssociationData(decimal associationId, decimal recordId);

        #endregion
    }
}
