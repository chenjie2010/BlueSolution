//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDataFieldHandler.cs
// 描述：CustomDataField 业务处理类
// 作者：ChenJie 
// 编写日期：2016/9/11
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
    /// CustomDataField 接口
    /// </summary>
    public interface ICustomDataFieldHandler : ICommonNodeBusiness, IPrincipalBusiness<CustomDataFieldInfo>
    {
        #region 接口

        /// <summary>
        /// 获得表编号
        /// </summary>
        ///<param name="dataFieldId"></param>
        /// <returns>表编号 </returns>
        decimal GetTableId(decimal dataFieldId);

        /// <summary>
        /// 验证自定义字段
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="customDataFieldName"></param>
        /// <returns></returns>
        bool VerifyCustomDataFieldName(decimal tableId, string customDataFieldName);

        /// <summary>
        /// 刷新基本类型
        /// </summary>
        void RefreshBasedDataType();

        /// <summary>
        /// 根据数据类型获得字段列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="basedDataType"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal tableId, BasedDataType basedDataType);

        /// <summary>
        /// 获得字段的帮助内容
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的帮助内容</returns>
        string GetHelpContent(decimal dataFieldId);

        /// <summary>
        /// 获得指定字段的附件路径
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        string GetFilePath(string dataFieldName, string fileName);

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        IList<CustomDataFieldInfo> GetModelInfos(decimal tableId);

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        IList<CustomDataFieldInfo> GetModelInfos(decimal tableId, DataFieldFilter dataFieldFilter);

        /// <summary>
        /// 获得字段类型
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        byte GetDataFieldType(decimal dataFieldId);

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal parentDataFieldId, bool inTheSameTable);

        /// <summary>
        /// 根据父节点编号条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodesByParentDataFieldId(decimal parentDataFieldId);

        /// <summary>
        /// 获得指定的字段的附件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        byte[] GetFileData(string dataFieldName, string fileName);

        /// <summary>
        /// 获得表的字段设置的个数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        int GetDataFieldCountUnderSetting(decimal tableId, byte pos);

        /// <summary>
        /// 获得枚举类型的物理字段信息表
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        DataSet GetDataFieldsByEnumId(decimal enumId);

        /// <summary>
        /// 查询该物理字段被其它字段关联(枚举关联或是逻辑字段关联)的总数
        /// </summary>
        /// <param name="parentDataFieldId">物理字段的编号</param>
        /// <returns></returns>
        int GetRelatedDataFieldCount(decimal parentDataFieldId);

        /// <summary>
        /// 查询该表下物理字段被其它字段关联(枚举关联或是逻辑字段关联)的总数
        /// </summary>
        /// <param name="tableId">物理表的编号</param>
        /// <returns></returns>
        int GetRelatedDataFieldCountByTableId(decimal tableId);

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal tableId);

        /// <summary>
        /// 批量插入物理字段
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="customDataFieldInfos"></param>
        /// <param name="enumCodeRelation"></param>
        /// <param name="secondaryCodeRelation"></param>
        void Insert(decimal tableId, List<CustomDataFieldInfo> customDataFieldInfos, Dictionary<string, string> enumCodeRelation,
            Dictionary<string, IList<string>> secondaryCodeRelation);

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldCode">字段编码</param>
        /// <returns> CustomDataFieldInfo 对象</returns>
        CustomDataFieldInfo GetModelInfoByCode(string dataFieldCode);

        /// <summary>
        /// 获得关联字段被关联的物理字段信息表
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        DataSet GetDataFieldsConnected(decimal associatedDataFieldId);

        /// <summary>
        /// 获得关联字段的个数
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        int GetDataFieldCountConnected(decimal associatedDataFieldId);

        /// <summary>
        /// 获取字段类型属于该枚举的字段个数
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        int GetDataFieldCountByEnumId(decimal enumId);

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal tableId, byte dataFieldType);

        /// <summary>
        /// 更新联系字段
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <param name="dataFieldRelationshipInfos"></param>
        void UpdateDataFields(decimal parentDataFieldId, IList<DataFieldRelationshipInfo> dataFieldRelationshipInfos);

        /// <summary>
        /// 获得字段
        /// 节点的父编号为视图编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        IList<CommonNode> GetRelationDataFields(decimal parentDataFieldId);

        /// <summary>
        /// 获得字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        IList<CommonNode> GetRelationDataFieldsWithFullName(decimal parentDataFieldId);

        /// <summary>
        /// 获得表达式类型字段组合名称
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        string GetDataFieldLogicalExpression(decimal dataFieldId);

        /// <summary>
		/// 向 CustomDataField 表中插入一条新记录
		/// </summary>
		/// <param name="customDataFieldInfo">customDataFieldInfo 对象</param>
        /// <param name="customExpressionInfos">表达式字段</param>
		/// <returns>自动增加的关键字的值</returns>
		decimal Insert(CustomDataFieldInfo customDataFieldInfo, IList<CustomExpressionInfo> customExpressionInfos);

        /// <summary>
        /// 更新 CustomDataFieldInfo 对象
        /// </summary>
        /// <param name="customDataFieldInfo">CustomDataFieldInfo 对象</param>
        void Update(CustomDataFieldInfo customDataFieldInfo, IList<CustomExpressionInfo> customExpressionInfos);

        /// <summary>
        /// 验证表达式类型
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="expressionText">表达式文本</param>
        /// <param name="commonNodes">字段列表</param>
        /// <param name="logicalDataFieldType">表达式类型</param>
        /// <returns>是否通过验证</returns>
        bool VerifyExpression(decimal tableId, string expressionText, IList<CommonNode> commonNodes);

        /// <summary>
        /// 验证 WHERE 条件
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        bool ValidateWhereExpression(CustomDataFieldInfo customDataFieldInfo, string whereExpression);

        /// <summary>
        /// 获得组合后的表达式字段名称
        /// </summary>
        /// <param name="expressionText"></param>
        /// <param name="expressionText"></param>
        /// <param name="commonNodes"></param>
        /// <param name="logicalDataFieldType"></param>
        /// <returns></returns>
        string GetExpressionDataFieldName(string tablePhysicalName, string expressionText, IList<CommonNode> commonNodes);

        /// <summary>
        /// 获得字段的物理名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        string GetPhysicalName(decimal dataFieldId);

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        string GetLogicalName(decimal dataFieldId);

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        IList<string> GetLogicalNames(IList<decimal> dataFieldIds);

        /// <summary>
        /// 获得完整的字段逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        string GetFullLogicalName(decimal dataFieldId);

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal tableId, DataFieldFilter dataFieldFilter);
        
        /// <summary>
        /// 获得字段的关联字段编号
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        decimal GetAssociatedDataFieldId(decimal dataFieldId);
        
        #endregion
    }
}
