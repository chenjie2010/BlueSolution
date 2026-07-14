//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomEnumService.cs
// 描述：CustomEnum 操作服务类
// 作者：ChenJie 
// 编写日期：2016/8/20
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
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.CustomEnum.
    /// </summary>
    public class CustomEnumService : CommonNodeServices, ICustomEnumContract
    {
        #region 业务实例
        
        private readonly static ICustomEnumHandler customEnumHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomEnumHandler>();
        
        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomEnumService() : base(customEnumHandler)
        {
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 customenum 表中插入一条新记录
		/// </summary>
		/// <param name="customEnumInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomEnumInfo customEnumInfo)
		{
            return customEnumHandler.Insert(customEnumInfo);
		}
        
        /// <summary>
		/// 获得 CustomEnumInfo 对象
		/// </summary>
		///<param name="enumId">枚举编号</param>
		/// <returns> CustomEnumInfo 对象</returns>
		public CustomEnumInfo GetModelInfo(decimal enumId)
		{	
            return customEnumHandler.GetModelInfo(enumId);           
		}		
		
        /// <summary>
		/// 更新 CustomEnumInfo 对象
		/// </summary>
		/// <param name="customEnumInfo">CustomEnumInfo 对象</param>
		public void Update(CustomEnumInfo customEnumInfo)
		{	          
            customEnumHandler.Update(customEnumInfo);
        }	
  
        /// <summary>
		/// 删除 CustomEnumInfo 对象
		/// </summary>
		///<param name="enumId">枚举编号</param>
		/// <returns> CustomEnumInfo 对象</returns>
		public void Delete(decimal enumId)
		{	
            customEnumHandler.Delete(enumId);
        }
        
        /// <summary>
		/// 获得 CustomEnumInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomEnumInfo 对象列表</returns>
		public IList<CustomEnumInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{
            return customEnumHandler.GetModelInfos(whereConditons, sortingCondtions);
        }
        
        /// <summary>
		/// 获得 CustomEnum 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomEnumInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            return customEnumHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 刷新排序
        /// </summary>
        public void RefreshSorting()
        {
            customEnumHandler.RefreshSorting();
        }

        /// <summary>
        /// 根据父节点编号和枚举名称获得枚举数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public object GetTreeData(decimal parentEnumId, string enumName, PhysicalDataFieldType physicalDataFieldType)
        {
            return customEnumHandler.GetTreeData(parentEnumId, enumName, physicalDataFieldType);
        }

        /// <summary>
        /// 根据父节点编号和枚举名称获得枚举数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="enumName"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public object GetDropdownListData(decimal parentEnumId, string enumName, PhysicalDataFieldType physicalDataFieldType)
        {
            return customEnumHandler.GetDropdownListData(parentEnumId, enumName, physicalDataFieldType);
        }

        /// <summary>
        /// 获得枚举名称
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="value"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public string GetDropdownListEnumName(decimal parentEnumId, object value, PhysicalDataFieldType physicalDataFieldType)
        {
            return customEnumHandler.GetDropdownListEnumName(parentEnumId, value, physicalDataFieldType);
        }

        /// <summary>
        /// 获得枚举名称
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="value"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public string GetTreeEnumName(decimal parentEnumId, object value, PhysicalDataFieldType physicalDataFieldType)
        {
            return customEnumHandler.GetTreeEnumName(parentEnumId, value, physicalDataFieldType);
        }

        /// <summary>
        /// 获得枚举数据
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public object GetEnumData(decimal enumId, PhysicalDataFieldType physicalDataFieldType)
        {
            return customEnumHandler.GetEnumData(enumId, physicalDataFieldType);
        }

        /// <summary>
        /// 根据枚举编码获得枚举编号
        /// </summary>
        /// <param name="enumCode"></param>
        /// <returns></returns>
        public decimal GetEnumId(string enumCode)
        {
            return customEnumHandler.GetEnumId(enumCode);
        }

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        public IList<string> GetTemplateColumnCaptions()
        {
            return customEnumHandler.GetTemplateColumnCaptions();
        }

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal parentEnumId)
        {
            return customEnumHandler.GetPageRecord(parentEnumId);
        }

        /// <summary>
        /// 获得表 CustomEnum 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(int startPosition, int count, ref int totalCount)
        {
            return customEnumHandler.GetPageRecord(startPosition, count, ref totalCount);
        }

        /// <summary>
        /// 获得枚举选项列表
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        public IList<CustomEnumInfo> GetEnumItems(decimal parentEnumId)
        {
            return customEnumHandler.GetEnumItems(parentEnumId);
        }

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(decimal parentEnumId, string enumName)
        {
            return customEnumHandler.GetTreeviewCommonNodes(parentEnumId, enumName);
        }

        /// <summary>
        /// 获取枚举的最大层级
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public int GetMaxLevel(decimal enumId)
        {
            return customEnumHandler.GetMaxLevel(enumId);
        }

        /// <summary>
        /// 是否是超大枚举
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public bool GetSuperEnumEnabled(decimal enumId)
        {
            return customEnumHandler.GetSuperEnumEnabled(enumId);
        }

        #endregion
    }
}
