//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomDataFieldService.cs
// 描述：CustomDataField 操作服务类
// 作者：ChenJie 
// 编写日期：2016/9/11
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
    /// 操作服务类，对于的表： dbo.CustomDataField.
    /// </summary>
    public class CustomDataFieldService : CommonNodeServices, ICustomDataFieldContract
    {
        #region 业务实例
        
        private static readonly ICustomDataFieldHandler customDataFieldHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomDataFieldHandler>();
        
        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomDataFieldService() : base(customDataFieldHandler)
        {
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 customdatafield 表中插入一条新记录
		/// </summary>
		/// <param name="customDataFieldInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomDataFieldInfo customDataFieldInfo)
		{
            return customDataFieldHandler.Insert(customDataFieldInfo);
		}
        
        /// <summary>
		/// 获得 CustomDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldId">字段编号</param>
		/// <returns> CustomDataFieldInfo 对象</returns>
		public CustomDataFieldInfo GetModelInfo(decimal dataFieldId)
		{	
            return customDataFieldHandler.GetModelInfo(dataFieldId);           
		}		
		
        /// <summary>
		/// 更新 CustomDataFieldInfo 对象
		/// </summary>
		/// <param name="customDataFieldInfo">CustomDataFieldInfo 对象</param>
		public void Update(CustomDataFieldInfo customDataFieldInfo)
		{	          
            customDataFieldHandler.Update(customDataFieldInfo);
        }	
  
        /// <summary>
		/// 删除 CustomDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldId">字段编号</param>
		/// <returns> CustomDataFieldInfo 对象</returns>
		public void Delete(decimal dataFieldId)
		{	
            customDataFieldHandler.Delete(dataFieldId);
        }
        
        /// <summary>
		/// 获得 CustomDataFieldInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDataFieldInfo 对象列表</returns>
		public IList<CustomDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return customDataFieldHandler.GetModelInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 CustomDataField 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomDataFieldInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return customDataFieldHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 验证自定义字段
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="customDataFieldName"></param>
        /// <returns></returns>
        public bool VerifyCustomDataFieldName(decimal tableId, string customDataFieldName)
        {
            return customDataFieldHandler.VerifyCustomDataFieldName(tableId, customDataFieldName);
        }

        /// <summary>
        /// 刷新基本类型
        /// </summary>
        public void RefreshBasedDataType()
        {
            customDataFieldHandler.RefreshBasedDataType();
        }

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetModelInfos(decimal tableId)
        {
            return customDataFieldHandler.GetModelInfos(tableId);
        }

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetModelInfos(decimal tableId, DataFieldFilter dataFieldFilter)
        {
            return customDataFieldHandler.GetModelInfos(tableId, dataFieldFilter);
        }

        /// <summary>
        /// 获得字段类型
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public byte GetDataFieldType(decimal dataFieldId)
        {
            return customDataFieldHandler.GetDataFieldType(dataFieldId);
        }

        /// <summary>
        /// 根据父节点编号条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodesByParentDataFieldId(decimal parentDataFieldId)
        {
            return customDataFieldHandler.GetCommonNodesByParentDataFieldId(parentDataFieldId);
        }

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal parentDataFieldId, bool inTheSameTable)
        {
            return customDataFieldHandler.GetCommonNodes(parentDataFieldId, inTheSameTable);
        }

        /// <summary>
        /// 获得指定的字段的附件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetFileData(string dataFieldName, string fileName)
        {
            return customDataFieldHandler.GetFileData(dataFieldName, fileName);
        }

        /// <summary>
        /// 获得表的字段设置的个数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetDataFieldCountUnderSetting(decimal tableId, byte pos)
        {
            return customDataFieldHandler.GetDataFieldCountUnderSetting(tableId, pos);
        }

        /// <summary>
        /// 获得枚举类型的物理字段信息表
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public DataSet GetDataFieldsByEnumId(decimal enumId)
        {
            return customDataFieldHandler.GetDataFieldsByEnumId(enumId);
        }

        /// <summary>
        /// 查询该物理字段被其它字段关联(枚举关联或是逻辑字段关联)的总数
        /// </summary>
        /// <param name="parentDataFieldId">物理字段的编号</param>
        /// <returns></returns>
        public int GetRelatedDataFieldCount(decimal parentDataFieldId)
        {
            return customDataFieldHandler.GetRelatedDataFieldCount(parentDataFieldId);
        }

        /// <summary>
        /// 查询该表下物理字段被其它字段关联(枚举关联或是逻辑字段关联)的总数
        /// </summary>
        /// <param name="tableId">物理表的编号</param>
        /// <returns></returns>
        public int GetRelatedDataFieldCountByTableId(decimal tableId)
        {
            return customDataFieldHandler.GetRelatedDataFieldCountByTableId(tableId);
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal tableId)
        {
            return customDataFieldHandler.GetPageRecord(tableId);
        }

        /// <summary>
        /// 批量插入物理字段
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="customDataFieldInfos"></param>
        /// <param name="enumCodeRelation"></param>
        /// <param name="secondaryCodeRelation"></param>
        public void Insert(decimal tableId, List<CustomDataFieldInfo> customDataFieldInfos, Dictionary<string, string> enumCodeRelation,
            Dictionary<string, IList<string>> secondaryCodeRelation)
        {
            customDataFieldHandler.Insert(tableId, customDataFieldInfos, enumCodeRelation, secondaryCodeRelation);
        }

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldCode">字段编码</param>
        /// <returns> CustomDataFieldInfo 对象</returns>
        public CustomDataFieldInfo GetModelInfoByCode(string dataFieldCode)
        {
            return customDataFieldHandler.GetModelInfoByCode(dataFieldCode);
        }

        /// <summary>
        /// 获得关联字段的个数
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public int GetDataFieldCountConnected(decimal associatedDataFieldId)
        {
            return customDataFieldHandler.GetDataFieldCountConnected(associatedDataFieldId);
        }

        /// <summary>
        /// 获得关联字段被关联的物理字段信息表
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public DataSet GetDataFieldsConnected(decimal associatedDataFieldId)
        {
            return customDataFieldHandler.GetDataFieldsConnected(associatedDataFieldId);
        }

        /// <summary>
        /// 获取字段类型属于该枚举的字段个数
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public int GetDataFieldCountByEnumId(decimal enumId)
        {
            return customDataFieldHandler.GetDataFieldCountByEnumId(enumId);
        }

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal tableId, byte dataFieldType)
        {
            return customDataFieldHandler.GetCommonNodes(tableId, dataFieldType);
        }

        /// <summary>
        /// 更新联系字段
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <param name="dataFieldRelationshipInfos"></param>
        public void UpdateDataFields(decimal parentDataFieldId, IList<DataFieldRelationshipInfo> dataFieldRelationshipInfos)
        {
            customDataFieldHandler.UpdateDataFields(parentDataFieldId, dataFieldRelationshipInfos);
        }

        /// <summary>
        /// 获得字段
        /// 节点的父编号为视图编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRelationDataFields(decimal parentDataFieldId)
        {
            return customDataFieldHandler.GetRelationDataFields(parentDataFieldId);
        }

        /// <summary>
        /// 获得字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRelationDataFieldsWithFullName(decimal parentDataFieldId)
        {
            return customDataFieldHandler.GetRelationDataFieldsWithFullName(parentDataFieldId);
        }
        
        // <summary>
        /// 获得表达式类型字段组合名称
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public string GetDataFieldLogicalExpression(decimal dataFieldId)
        {
            return customDataFieldHandler.GetDataFieldLogicalExpression(dataFieldId);
        }

        /// <summary>
		/// 向 CustomDataField 表中插入一条新记录
		/// </summary>
		/// <param name="customDataFieldInfo">customDataFieldInfo 对象</param>
        /// <param name="customExpressionInfos">表达式字段</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomDataFieldInfo customDataFieldInfo, IList<CustomExpressionInfo> customExpressionInfos)
        {
            return customDataFieldHandler.Insert(customDataFieldInfo, customExpressionInfos);
        }

        /// <summary>
        /// 更新 CustomDataFieldInfo 对象
        /// </summary>
        /// <param name="customDataFieldInfo">CustomDataFieldInfo 对象</param>
        public void Update(CustomDataFieldInfo customDataFieldInfo, IList<CustomExpressionInfo> customExpressionInfos)
        {
            customDataFieldHandler.Update(customDataFieldInfo, customExpressionInfos);
        }

        /// <summary>
        /// 验证表达式类型
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="expressionText">表达式文本</param>
        /// <param name="commonNodes">字段列表</param>
        /// <returns>是否通过验证</returns>
        public bool VerifyExpression(decimal tableId, string expressionText, IList<CommonNode> commonNodes)
        {
            return customDataFieldHandler.VerifyExpression(tableId, expressionText, commonNodes);
        }

        /// <summary>
        /// 验证 WHERE 条件
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool ValidateWhereExpression(CustomDataFieldInfo customDataFieldInfo, string whereExpression)
        {
            return customDataFieldHandler.ValidateWhereExpression(customDataFieldInfo, whereExpression);
        }

        /// <summary>
        /// 获得组合后的表达式字段名称
        /// </summary>
        /// <param name="expressionText"></param>
        /// <param name="expressionText"></param>
        /// <param name="commonNodes"></param>
        /// <param name="logicalDataFieldType"></param>
        /// <returns></returns>
        public string GetExpressionDataFieldName(string tablePhysicalName, string expressionText, IList<CommonNode> commonNodes)
        {
            return customDataFieldHandler.GetExpressionDataFieldName(tablePhysicalName, expressionText, commonNodes);
        }

        /// <summary>
        /// 获得字段的物理名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetPhysicalName(decimal dataFieldId)
        {
            return customDataFieldHandler.GetPhysicalName(dataFieldId);
        }

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetLogicalName(decimal dataFieldId)
        {
            return customDataFieldHandler.GetLogicalName(dataFieldId);
        }

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public IList<string> GetLogicalNames(IList<decimal> dataFieldIds)
        {
            return customDataFieldHandler.GetLogicalNames(dataFieldIds);
        }

        /// <summary>
        /// 获得完整的字段逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetFullLogicalName(decimal dataFieldId)
        {
            return customDataFieldHandler.GetFullLogicalName(dataFieldId);
        }

        /// <summary>
        /// 根据数据类型获得字段列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="basedDataType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal tableId, BasedDataType basedDataType)
        {
            return customDataFieldHandler.GetCommonNodes(tableId, basedDataType);
        }

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal tableId, DataFieldFilter dataFieldFilter)
        {
            return customDataFieldHandler.GetCommonNodes(tableId, dataFieldFilter);
        }

        /// <summary>
        /// 获得字段的关联字段编号
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public decimal GetAssociatedDataFieldId(decimal dataFieldId)
        {
            return customDataFieldHandler.GetAssociatedDataFieldId(dataFieldId);
        }

        #endregion
    }
}
