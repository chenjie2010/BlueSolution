//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomAssociationService.cs
// 描述：CustomAssociation 操作服务类
// 作者：ChenJie 
// 编写日期：2016/10/2
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomAssociation.
    /// </summary>
    public class CustomAssociationService : CommonNodeServices, ICustomAssociationContract
    {
        #region 业务实例

        private static readonly ICustomAssociationHandler customAssociationHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomAssociationHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomAssociationService() : base(customAssociationHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="groupIds"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(IList<decimal> groupIds)
        {
            return customAssociationHandler.GetPageRecord(groupIds);
        }

        /// <summary>
        /// 向 customassociation 表中插入一条新记录
        /// </summary>
        /// <param name="customAssociationInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomAssociationInfo customAssociationInfo)
        {
            return customAssociationHandler.Insert(customAssociationInfo);
        }

        /// <summary>
        /// 获得 CustomAssociationInfo 对象
        /// </summary>
        ///<param name="associatedDataFieldId">关联编号</param>
        /// <returns> CustomAssociationInfo 对象</returns>
        public CustomAssociationInfo GetModelInfo(decimal associatedDataFieldId)
        {
            return customAssociationHandler.GetModelInfo(associatedDataFieldId);
        }

        /// <summary>
        /// 更新 CustomAssociationInfo 对象
        /// </summary>
        /// <param name="customAssociationInfo">CustomAssociationInfo 对象</param>
        public void Update(CustomAssociationInfo customAssociationInfo)
        {
            customAssociationHandler.Update(customAssociationInfo);
        }

        /// <summary>
        /// 删除 CustomAssociationInfo 对象
        /// </summary>
        ///<param name="associatedDataFieldId">关联编号</param>
        /// <returns> CustomAssociationInfo 对象</returns>
        public void Delete(decimal associatedDataFieldId)
        {
            customAssociationHandler.Delete(associatedDataFieldId);
        }

        /// <summary>
        /// 获得 CustomAssociationInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomAssociationInfo 对象列表</returns>
        public IList<CustomAssociationInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customAssociationHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomAssociation 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomAssociationInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customAssociationHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        /// <param name="movedDriection"></param>
        public void UpdateRecordSorting(decimal associationId, decimal recordId, MovedDriection movedDriection)
        {
            customAssociationHandler.UpdateRecordSorting(associationId, recordId, movedDriection);
        }

        /// <summary>
        /// 获得指定列数据
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public DataTable GetAssociationColumnData(decimal associatedDataFieldId)
        {
            return customAssociationHandler.GetAssociationColumnData(associatedDataFieldId);
        }

        /// <summary>
        /// 该表中的字段属于物理字段的关联字段类型的个数
        /// </summary>
        /// <param name="associationCode"></param>
        /// <returns></returns>
        public int GetDataFieldCountConnected(string associationCode)
        {
            return customAssociationHandler.GetDataFieldCountConnected(associationCode);
        }

        /// <summary>
        /// 重置关联表
        /// </summary>
        /// <param name="associationId"></param>
        public void ResetTable(decimal associationId)
        {
            customAssociationHandler.ResetTable(associationId);
        }

        /// <summary>
        /// 该表是否有字段属于物理字段的关联字段类型
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public bool HasDataFieldConnected(decimal associationId)
        {
            return customAssociationHandler.HasDataFieldConnected(associationId);
        }

        /// <summary>
        /// 导入业务数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="dataTable"></param>
        public void ImportDataTable(decimal associationId, DataTable dataTable)
        {
            customAssociationHandler.ImportDataTable(associationId, dataTable);
        }

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetAssociationData(decimal associationId, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            return customAssociationHandler.GetAssociationData(associationId, startPosition, count, whereConditons, ref totalCount);
        }

        /// <summary>
        /// 是否是超大关联
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public bool GetSuperAssociationEnabled(decimal associationId)
        {
            return customAssociationHandler.GetSuperAssociationEnabled(associationId);
        }

        /// <summary>
        /// 在关联表中增加记录
        /// </summary>
        /// <param name="associationId">关联编号</param>
        /// <param name="commonDataFields"></param>
        /// <returns></returns>
        public decimal Insert(decimal associationId, IList<CommonDataField> commonDataFields)
        {
            return customAssociationHandler.Insert(associationId, commonDataFields);
        }

        /// <summary>
        /// 更新关联表中的记录
        /// </summary>
        /// <param name="associationId">关联编号</param>
        /// <param name="recordId">关联表记录编号</param>
        /// <param name="commonDataFields"></param>
        public void Update(decimal associationId, decimal recordId, IList<CommonDataField> commonDataFields)
        {
            customAssociationHandler.Update(associationId, recordId, commonDataFields);
        }

        /// <summary>
        /// 删除关联表的记录
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        public void Delete(decimal associationId, decimal recordId)
        {
            customAssociationHandler.Delete(associationId, recordId);
        }

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public DataTable GetAssociationData(decimal associationId)
        {
            return customAssociationHandler.GetAssociationData(associationId);
        }

        /// <summary>
        /// 获得关联表的数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public DataTable GetAssociationDataWithSortingDataField(decimal associationId)
        {
            return customAssociationHandler.GetAssociationDataWithSortingDataField(associationId);
        }

        /// <summary>
        /// 根据关联编号对于的表获得相应的记录行
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public DataTable GetAssociationData(decimal associationId, decimal recordId)
        {
            return customAssociationHandler.GetAssociationData(associationId, recordId);
        }

        #endregion
    }
}
