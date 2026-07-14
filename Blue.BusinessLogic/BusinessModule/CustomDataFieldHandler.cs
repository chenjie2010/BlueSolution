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
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using AppFramework.Reference.DataFieldLibrary;
using Blue.DALFactory;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomDataField.
    /// </summary>
    public class CustomDataFieldHandler : CommonNodeBusiness, ICustomDataFieldHandler
    {
        #region 工厂类实例

        private static readonly ICustomDataField dalCustomDataField = BusinessDataAccessFactory.CreateCustomDataField();
        private static readonly IDataFieldRelationship dalDataFieldRelationship = BusinessDataAccessFactory.CreateDataFieldRelationship();      

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomDataFieldHandler() : base(dalCustomDataField)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 customdatafield 表中插入一条新记录
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomDataFieldInfo customDataFieldInfo)
        {
            //自动增加的关键字的值
            decimal customDataFieldId = 0;

            // 验证输入
            if (customDataFieldInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customDataFieldId = dalCustomDataField.Insert(customDataFieldInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldId;
        }

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> CustomDataFieldInfo 对象</returns>
        public CustomDataFieldInfo GetModelInfo(decimal dataFieldId)
        {
            CustomDataFieldInfo customDataFieldInfo = null;

            // 验证输入
            if (dataFieldId < 0)
            {
                return null;
            }

            try
            {
                customDataFieldInfo = dalCustomDataField.GetModelInfo(dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldInfo;
        }

        /// <summary>
        /// 更新 CustomDataFieldInfo 对象
        /// </summary>
        /// <param name="customDataFieldInfo">CustomDataFieldInfo 对象</param>
        public void Update(CustomDataFieldInfo customDataFieldInfo)
        {
            // 验证输入
            if (customDataFieldInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomDataField.Update(customDataFieldInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomDataFieldInfo 对象
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> CustomDataFieldInfo 对象</returns>
        public void Delete(decimal dataFieldId)
        {
            // 验证输入
            if (dataFieldId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomDataField.Delete(dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetModelInfos(decimal tableId)
        {
            IList<CustomDataFieldInfo> customDataFieldInfos = null;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customDataFieldInfos = dalCustomDataField.GetModelInfos(tableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldInfos;
        }

        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public IList<CustomDataFieldInfo> GetModelInfos(decimal tableId, DataFieldFilter dataFieldFilter)
        {
            IList<CustomDataFieldInfo> customDataFieldInfos = null;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customDataFieldInfos = dalCustomDataField.GetModelInfos(tableId, dataFieldFilter);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldInfos;
        }


        /// <summary>
        /// 获得 CustomDataFieldInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomDataFieldInfo 对象列表</returns>
        public IList<CustomDataFieldInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomDataFieldInfo> customDataFieldInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customDataFieldInfos = dalCustomDataField.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldInfos;
        }

        /// <summary>
        /// 获得表编号
        /// </summary>
        ///<param name="dataFieldId"></param>
        /// <returns>表编号 </returns>
        public decimal GetTableId(decimal dataFieldId)
        {
            decimal tableId = decimal.MinValue;

            if (dataFieldId <= 0)
            {
                throw new ArgumentException("字段编号不能小于或是等于0。");
            }

            try
            {
                tableId = dalCustomDataField.GetTableId(dataFieldId);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableId;
        }

        /// <summary>
        /// 获得 CustomDataField 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomDataFieldInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomDataField.GetTotalCount(whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 验证自定义字段
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="customDataFieldName"></param>
        /// <returns></returns>
        public bool VerifyCustomDataFieldName(decimal tableId, string customDataFieldName)
        {
            bool success = false;

            try
            {
                success = dalCustomDataField.VerifyCustomDataFieldName(tableId, customDataFieldName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return success;
        }

        /// <summary>
        /// 刷新基本类型
        /// </summary>
        public void RefreshBasedDataType()
        {
            try
            {
                dalCustomDataField.RefreshBasedDataType();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得字段的帮助内容
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的帮助内容</returns>
        public string GetHelpContent(decimal dataFieldId)
        {
            string helpContent = string.Empty;

            try
            {
                helpContent = dalCustomDataField.GetHelpContent(dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return helpContent;
        }

        /// <summary>
        /// 获得指定字段的附件路径
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public string GetFilePath(string dataFieldName, string fileName)
        {
            string filePath = string.Empty;

            try
            {
                filePath = dalCustomDataField.GetFilePath(dataFieldName, fileName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return filePath;
        }

        /// <summary>
        /// 获得字段类型
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public byte GetDataFieldType(decimal dataFieldId)
        {
            byte dataFieldType = 0;

            try
            {
                dataFieldType = dalCustomDataField.GetDataFieldType(dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataFieldType;
        }

        /// <summary>
        /// 获得指定的字段的附件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public byte[] GetFileData(string dataFieldName, string fileName)
        {
            byte[] data = null;

            try
            {
                data = dalCustomDataField.GetFileData(dataFieldName, fileName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return data;
        }

        /// <summary>
        /// 获得表的字段设置的个数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public int GetDataFieldCountUnderSetting(decimal tableId, byte pos)
        {
            int count = 0;

            try
            {
                count = dalCustomDataField.GetDataFieldCountUnderSetting(tableId, pos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得枚举类型的物理字段信息表
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public DataSet GetDataFieldsByEnumId(decimal enumId)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomDataField.GetDataFieldsByEnumId(enumId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 查询该物理字段被其它字段关联(枚举关联或是逻辑字段关联)的总数
        /// </summary>
        /// <param name="parentDataFieldId">物理字段的编号</param>
        /// <returns></returns>
        public int GetRelatedDataFieldCount(decimal parentDataFieldId)
        {
            int count = 0;

            try
            {
                count = dalCustomDataField.GetRelatedDataFieldCount(parentDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 查询该表下物理字段被其它字段关联(枚举关联或是逻辑字段关联)的总数
        /// </summary>
        /// <param name="tableId">物理表的编号</param>
        /// <returns></returns>
        public int GetRelatedDataFieldCountByTableId(decimal tableId)
        {
            int count = 0;

            try
            {
                count = dalCustomDataField.GetRelatedDataFieldCountByTableId(tableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }


        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal tableId)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomDataField.GetPageRecord(tableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
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
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                dalCustomDataField.Insert(tableId, customDataFieldInfos, enumCodeRelation, secondaryCodeRelation);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
		/// 获得 CustomDataFieldInfo 对象
		/// </summary>
		///<param name="dataFieldCode">字段编码</param>
		/// <returns> CustomDataFieldInfo 对象</returns>
		public CustomDataFieldInfo GetModelInfoByCode(string dataFieldCode)
        {
            CustomDataFieldInfo customDataFieldInfo = null;

            if (string.IsNullOrWhiteSpace(dataFieldCode))
            {
                throw new ArgumentException("字段编码不能为空。");
            }

            try
            {
                customDataFieldInfo = dalCustomDataField.GetModelInfoByCode(dataFieldCode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldInfo;
        }

        /// <summary>
        /// 获得关联字段的个数
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public int GetDataFieldCountConnected(decimal associatedDataFieldId)
        {
            int count = 0;

            if (associatedDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                count = dalCustomDataField.GetDataFieldCountConnected(associatedDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得关联字段被关联的物理字段信息表
        /// </summary>
        /// <param name="associatedDataFieldId"></param>
        /// <returns></returns>
        public DataSet GetDataFieldsConnected(decimal associatedDataFieldId)
        {
            DataSet ds = null;

            if (associatedDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                ds = dalCustomDataField.GetDataFieldsConnected(associatedDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获取字段类型属于该枚举的字段个数
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public int GetDataFieldCountByEnumId(decimal enumId)
        {
            int count = 0;

            if (enumId <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                count = dalCustomDataField.GetDataFieldCountByEnumId(enumId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal tableId, byte dataFieldType)
        {
            IList<CommonNode> commonNodes = null;

            if (dataFieldType <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                commonNodes = dalCustomDataField.GetCommonNodes(tableId, dataFieldType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal parentDataFieldId, bool inTheSameTable)
        {
            IList<CommonNode> commonNodes = null;

            if (parentDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                commonNodes = dalCustomDataField.GetCommonNodes(parentDataFieldId, inTheSameTable);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 根据父节点编号条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodesByParentDataFieldId(decimal parentDataFieldId)
        {
            IList<CommonNode> commonNodes = null;

            if (parentDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                commonNodes = dalCustomDataField.GetCommonNodesByParentDataFieldId(parentDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 更新联系字段
        /// </summary>
        /// <param name="parentDataFieldId"></param>
        /// <param name="dataFieldRelationshipInfos"></param>
        public void UpdateDataFields(decimal parentDataFieldId, IList<DataFieldRelationshipInfo> dataFieldRelationshipInfos)
        {
            if (parentDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                dalDataFieldRelationship.UpdateDataFields(parentDataFieldId, dataFieldRelationshipInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得字段
        /// 节点的父编号为视图编号
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRelationDataFields(decimal parentDataFieldId)
        {
            IList<CommonNode> commonNodes = null;

            if (parentDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                commonNodes = dalDataFieldRelationship.GetRelationDataFields(parentDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得字段
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetRelationDataFieldsWithFullName(decimal parentDataFieldId)
        {
            IList<CommonNode> commonNodes = null;

            if (parentDataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                commonNodes = dalDataFieldRelationship.GetRelationDataFieldsWithFullName(parentDataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得表达式类型字段组合名称
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public string GetDataFieldLogicalExpression(decimal dataFieldId)
        {
            string expressionText = string.Empty;

            if (dataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                expressionText = dalCustomDataField.GetDataFieldLogicalExpression(dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return expressionText;
        }        

        /// <summary>
		/// 向 CustomDataField 表中插入一条新记录
		/// </summary>
		/// <param name="customDataFieldInfo">customDataFieldInfo 对象</param>
        /// <param name="customExpressionInfos">表达式字段</param>
		/// <returns>自动增加的关键字的值</returns>
		public decimal Insert(CustomDataFieldInfo customDataFieldInfo, IList<CustomExpressionInfo> customExpressionInfos)
        {
            //自动增加的关键字的值
            decimal customDataFieldId = 0;

            // 验证输入
            if (customDataFieldInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customDataFieldId = dalCustomDataField.Insert(customDataFieldInfo, customExpressionInfos);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customDataFieldId;
        }

        /// <summary>
        /// 更新 CustomDataFieldInfo 对象
        /// </summary>
        /// <param name="customDataFieldInfo">CustomDataFieldInfo 对象</param>
        public void Update(CustomDataFieldInfo customDataFieldInfo, IList<CustomExpressionInfo> customExpressionInfos)
        {
            // 验证输入
            if (customDataFieldInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomDataField.Update(customDataFieldInfo, customExpressionInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得字段的物理名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetPhysicalName(decimal dataFieldId)
        {
            string physicalName = string.Empty;

            if (dataFieldId < 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                physicalName = dalCustomDataField.GetPhysicalName(dataFieldId);
            }
            catch(Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return physicalName;
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
            bool result = false;

            if (tableId < 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }
            if (string.IsNullOrWhiteSpace(expressionText))
            {
                throw new ArgumentException("表达式不能为空。");
            }

            try
            {
                result = dalCustomDataField.VerifyExpression(tableId, expressionText, commonNodes);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 验证 WHERE 条件
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="whereExpression"></param>
        /// <returns></returns>
        public bool ValidateWhereExpression(CustomDataFieldInfo customDataFieldInfo, string whereExpression)
        {
            bool result = false;

            if (customDataFieldInfo == null)
            {
                throw new ArgumentException("字段对象不能为空。");
            }

            try
            {
                result = dalCustomDataField.ValidateWhereExpression(customDataFieldInfo, whereExpression);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return result;
        }

        /// <summary>
        /// 获得组合后的表达式字段名称
        /// </summary>
        /// <param name="expressionText"></param>
        /// <param name="expressionText"></param>
        /// <param name="commonNodes"></param>
        /// <returns></returns>
        public string GetExpressionDataFieldName(string tablePhysicalName, string expressionText, IList<CommonNode> commonNodes)
        {
            string expressionDataFieldName = string.Empty;

            if (string.IsNullOrWhiteSpace(tablePhysicalName))
            {
                throw new ArgumentException("物理表名称不能为空。");
            }
            if (string.IsNullOrWhiteSpace(expressionText))
            {
                throw new ArgumentException("表达式不能为空。");
            }

            try
            {
                expressionDataFieldName = dalCustomDataField.GetExpressionDataFieldName(tablePhysicalName, expressionText, commonNodes);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return expressionDataFieldName;
        }

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetLogicalName(decimal dataFieldId)
        {
            string logicalName = string.Empty;

            // 验证输入
            if (dataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                logicalName = dalCustomDataField.GetLogicalName(dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return logicalName;
        }

        /// <summary>
        /// 获得字段的逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public IList<string> GetLogicalNames(IList<decimal> dataFieldIds)
        {
            IList<string> logicalNames = null;

            // 验证输入
            if (dataFieldIds == null || dataFieldIds.Count <= 0)
            {
                throw new ArgumentException("编号数量不能小于等于0。");
            }

            try
            {
                logicalNames = dalCustomDataField.GetLogicalNames(dataFieldIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return logicalNames;
        }

        /// <summary>
        /// 获得完整的字段逻辑名称
        /// </summary>
        ///<param name="dataFieldId">字段编号</param>
        /// <returns> 字段的物理名称</returns>
        public string GetFullLogicalName(decimal dataFieldId)
        {
            string fullLogicalName = string.Empty;

            // 验证输入
            if (dataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                fullLogicalName = dalCustomDataField.GetFullLogicalName(dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return fullLogicalName;
        }

        /// <summary>
        /// 根据数据类型获得字段列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="basedDataType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal tableId, BasedDataType basedDataType)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            // 验证输入
            if (tableId <= 0)
            {
                return null;
            }

            try
            {
                IList<CommonNode> nodes = dalCustomDataField.GetCommonNodes(tableId, basedDataType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 根据字段类型条件获得字段节点
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal tableId, DataFieldFilter dataFieldFilter)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            // 验证输入
            if (tableId <= 0)
            {
                return null;
            }

            try
            {
                IList<CommonNode> nodes = dalCustomDataField.GetCommonNodes(tableId, dataFieldFilter);
                if (dataFieldFilter == DataFieldFilter.All || dataFieldFilter == DataFieldFilter.SystemDataFieldAndPhysicalDataField)
                {
                    IList<CommonNode> systemCommonNodes = UserEnumHelper.GetCommonNodes(typeof(SystemDataField));
                    foreach (CommonNode node in systemCommonNodes)
                    {
                        node.NodeType = (byte)DataFieldProperty.SystemPhysicalDataField;
                        commonNodes.Add(node);
                    }                    
                }
                foreach (CommonNode node in nodes)
                {
                    node.NodeType = (byte)DataFieldProperty.PhysicalDataField;
                    commonNodes.Add(node);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得字段的关联字段编号
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        public decimal GetAssociatedDataFieldId(decimal dataFieldId)
        {
            decimal associatedDataFieldId = 0;

            // 验证输入
            if (dataFieldId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                associatedDataFieldId = dalCustomDataField.GetAssociatedDataFieldId(dataFieldId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return associatedDataFieldId;
        }
                
        /// <summary>
        /// 获得 CommonNode 对象
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public override CommonNode GetCommonNode(decimal nodeId)
        {
            CommonNode commonNode = null;

            if (nodeId < 0)
            {
                throw new ArgumentException("编号不能小于0。");
            }

            try
            {
                if (nodeId == 0)
                {
                    commonNode = DataFieldHelper.GetDataFieldPropertyCommonNode(DataFieldProperty.SystemPhysicalDataField);
                }
                else
                {
                    commonNode = base.GetCommonNode(nodeId);
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNode;
        }

        /// <summary>
        /// 获得 CommonNode 对象的列表
        /// </summary>	
        /// <param name="parentId">父节点编号</param>
        /// <returns>CommonNode 对象的列表</returns>
        public override IList<CommonNode> GetChildNodes(decimal parentId)
        {
            IList<CommonNode> childNodes = new List<CommonNode>();

            try
            {
                if (parentId > 0)
                {
                    CommonNode childNode = DataFieldHelper.GetDataFieldPropertyCommonNode(DataFieldProperty.SystemPhysicalDataField);
                    childNodes.Add(childNode);

                    IList<CommonNode> commonNodes = base.GetChildNodes(parentId);
                    foreach (CommonNode commonNode in commonNodes)
                    {
                        childNodes.Add(commonNode);
                    }
                }
                else
                {
                    IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemDataField));
                    foreach (EnumItem enumItem in enumItems)
                    {
                        childNodes.Add(new CommonNode(0, 0, enumItem.Text, string.Empty, true, enumItem.Value));
                    }
                }
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return childNodes;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
