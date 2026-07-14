//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：AssociatedDataFieldService.cs
// 描述：AssociatedDataField 操作服务类
// 作者：ChenJie 
// 编写日期：2016/10/3
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
    /// 操作服务类，对于的表： dbo.AssociatedDataField.
    /// </summary>
    public class AssociatedDataFieldService : CommonNodeServices, IAssociatedDataFieldContract
    {
        #region 业务实例

        private static readonly IAssociatedDataFieldHandler associatedDataFieldHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<IAssociatedDataFieldHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public AssociatedDataFieldService() : base(associatedDataFieldHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 associateddatafield 表中插入一条新记录
        /// </summary>
        /// <param name="associatedDataFieldInfo"></param>
        /// <returns></returns>
        public decimal Insert(AssociatedDataFieldInfo associatedDataFieldInfo)
        {
            return associatedDataFieldHandler.Insert(associatedDataFieldInfo);
        }

        /// <summary>
        /// 获得 AssociatedDataFieldInfo 对象
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> AssociatedDataFieldInfo 对象</returns>
        public AssociatedDataFieldInfo GetModelInfo(decimal associatedDataFieldId)
        {
            return associatedDataFieldHandler.GetModelInfo(associatedDataFieldId);
        }

        /// <summary>
        /// 更新 AssociatedDataFieldInfo 对象
        /// </summary>
        /// <param name="associatedDataFieldInfo">AssociatedDataFieldInfo 对象</param>
        public void Update(AssociatedDataFieldInfo associatedDataFieldInfo)
        {
            associatedDataFieldHandler.Update(associatedDataFieldInfo);
        }

        /// <summary>
        /// 删除 AssociatedDataFieldInfo 对象
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> AssociatedDataFieldInfo 对象</returns>
        public void Delete(decimal associatedDataFieldId)
        {
            associatedDataFieldHandler.Delete(associatedDataFieldId);
        }

        /// <summary>
        /// 获得 AssociatedDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>AssociatedDataFieldInfo 对象列表</returns>
        public IList<AssociatedDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return associatedDataFieldHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 AssociatedDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>AssociatedDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return associatedDataFieldHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口        

        /// <summary>
        /// 获得字段的物理字段
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetPhysicalName(decimal associatedDataFieldId)
        {
            return associatedDataFieldHandler.GetPhysicalName(associatedDataFieldId);
        }

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetLogicalName(decimal associatedDataFieldId)
        {
            return associatedDataFieldHandler.GetLogicalName(associatedDataFieldId);
        }

        /// <summary>
        /// 通过关联字段的类型获得关联表中关联字段个数
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="DataFieldCategory"></param>
        /// <returns></returns>
        public int GetAssociatedDataFieldCount(decimal associationId, AssociatedDataFieldCategory dataFieldCategory)
        {
            return associatedDataFieldHandler.GetAssociatedDataFieldCount(associationId, dataFieldCategory);
        }

        /// <summary>
        /// 获得字段的属性信息
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public List<BasedDataFieldInfo> GetDataFieldProperties(decimal associationId)
        {
            return associatedDataFieldHandler.GetDataFieldProperties(associationId);
        }

        /// <summary>
        /// 获得字段的长度
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的长度</returns>
        public int GetDataLength(decimal associatedDataFieldId)
        {
            return associatedDataFieldHandler.GetDataLength(associatedDataFieldId);
        }

        /// <summary>
        /// 关联表的字段名称关系
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public Dictionary<string, string> GetDataFieldNameRelation(decimal dataFieldId)
        {
            return associatedDataFieldHandler.GetDataFieldNameRelation(dataFieldId);
        }

        /// <summary>
        /// 通过关联字段获得关联表编号
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public BasedDataType GetBasedDataType(decimal associatedDataFieldId)
        {
            return associatedDataFieldHandler.GetBasedDataType(associatedDataFieldId);
        }

        /// <summary>
        /// 通过关联字段获得关联表编号
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public decimal GetAssociationId(decimal associatedDataFieldId)
        {
            return associatedDataFieldHandler.GetAssociationId(associatedDataFieldId);
        }

        /// <summary>
        /// 获得字段列表
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public IList<AssociatedDataFieldInfo> GetModelInfos(decimal associationId)
        {
            return associatedDataFieldHandler.GetModelInfos(associationId);
        }

        #endregion
    }
}
