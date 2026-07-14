//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：AssociatedDataFieldHandler.cs
// 描述：AssociatedDataField 业务处理类
// 作者：ChenJie 
// 编写日期：2016/10/3
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
    /// AssociatedDataField 接口
    /// </summary>
    public interface IAssociatedDataFieldHandler: ICommonNodeBusiness, IPrincipalBusiness<AssociatedDataFieldInfo>
    {
        #region 接口

        /// <summary>
        /// 获得字段的物理字段
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        string GetPhysicalName(decimal associatedDataFieldId);

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的物理名称</returns>
        string GetLogicalName(decimal associatedDataFieldId);

        /// <summary>
        /// 通过关联字段的类型获得关联表中关联字段个数
        /// </summary>
        /// <param name="associationId"></param>
        /// <param name="DataFieldCategory"></param>
        /// <returns></returns>
        int GetAssociatedDataFieldCount(decimal associationId, AssociatedDataFieldCategory dataFieldCategory);

        /// <summary>
        /// 获得字段的属性信息
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        List<BasedDataFieldInfo> GetDataFieldProperties(decimal associationId);

        /// <summary>
        /// 获得字段的长度
        /// </summary>
        ///<param name="associatedDataFieldId">关联字段编号</param>
        /// <returns> 字段的长度</returns>
        int GetDataLength(decimal associatedDataFieldId);

        /// <summary>
        /// 关联表的字段名称关系
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        Dictionary<string, string> GetDataFieldNameRelation(decimal dataFieldId);

        /// <summary>
        /// 通过关联字段获得关联表编号
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        BasedDataType GetBasedDataType(decimal associatedDataFieldId);

        /// <summary>
        /// 通过关联字段获得关联表编号
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        decimal GetAssociationId(decimal associatedDataFieldId);

        /// <summary>
        /// 获得字段列表
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        IList<AssociatedDataFieldInfo> GetModelInfos(decimal associationId);

        #endregion
    }
}
