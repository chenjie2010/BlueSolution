//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomEnumHandler.cs
// 描述：CustomEnum 业务处理类
// 作者：ChenJie 
// 编写日期：2016/8/20
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.DALFactory;
using Blue.CustomLibrary;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomEnum.
    /// </summary>
    public class CustomEnumHandler : CommonNodeBusiness, ICustomEnumHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomEnum dalCustomEnum = BusinessDataAccessFactory.CreateCustomEnum(); 
        
        #endregion
        
		#region 构造函数
        
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomEnumHandler() : base(dalCustomEnum)
        {
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customenum 表中插入一条新记录
		/// </summary>
		/// <param name="customEnumInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomEnumInfo customEnumInfo)
		{
            //自动增加的关键字的值
			decimal customEnumId = 0;
            
			// 验证输入
			if (customEnumInfo == null)
            {
				throw new ArgumentException("不能插入空对象.");
            }
            
            try
            {
                customEnumId = dalCustomEnum.Insert(customEnumInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customEnumId;
		}
        
        /// <summary>
		/// 获得 CustomEnumInfo 对象
		/// </summary>
		///<param name="enumId">枚举编号</param>
		/// <returns> CustomEnumInfo 对象</returns>
		public CustomEnumInfo GetModelInfo(decimal enumId)
		{			
			CustomEnumInfo  customEnumInfo = null;
            
			// 验证输入
			if(enumId < 0)
            {
				return null;
            }

            try
            {
                customEnumInfo =  dalCustomEnum.GetModelInfo(enumId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customEnumInfo;
		}        
        
        /// <summary>
		/// 更新 CustomEnumInfo 对象
		/// </summary>
		/// <param name="customEnumInfo">CustomEnumInfo 对象</param>
		public void Update(CustomEnumInfo customEnumInfo)
		{	
            // 验证输入
            if (customEnumInfo == null)
            {
				throw new ArgumentException("不能更新空对象.");
            }            
            try
            {
                dalCustomEnum.Update(customEnumInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomEnumInfo 对象
		/// </summary>
		///<param name="enumId">枚举编号</param>
		/// <returns> CustomEnumInfo 对象</returns>
		public void Delete(decimal enumId)
		{		
            // 验证输入
			if(enumId < 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomEnum.Delete(enumId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		

        /// <summary>
		/// 获得 CustomEnumInfo 对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomEnumInfo 对象列表</returns>
		public IList<CustomEnumInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomEnumInfo>  customEnumInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customEnumInfos = dalCustomEnum.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customEnumInfos;
		}               
        
        /// <summary>
		/// 获得 CustomEnum 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomEnumInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomEnum.GetTotalCount(whereConditons);
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
        /// 刷新排序
        /// </summary>
        public void RefreshSorting()
        {
            try
            {
                dalCustomEnum.RefreshSorting();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得枚举的单独项的值
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public string GetEnumText(decimal enumId, PhysicalDataFieldType physicalDataFieldType)
        {
            string enumText = string.Empty;

            if (enumId <= 0)
            {
                throw new ArgumentException("枚举编号不能小于等于0。");
            }

            try
            {
                enumText = dalCustomEnum.GetEnumText(enumId, physicalDataFieldType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumText;
        }

        /// <summary>
        /// 获得枚举值
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public string GetEnumText(decimal enumId)
        {
            string enumText = string.Empty;

            if (enumId <= 0)
            {
                throw new ArgumentException("枚举编号不能小于等于0。");
            }

            try
            {
                enumText = dalCustomEnum.GetEnumText(enumId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumText;
        }

        /// <summary>
        /// 根据父节点编号获得所有子节点数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        public DataSet GetEnumData(decimal parentEnumId)
        {
            DataSet ds = null;

            if (parentEnumId <= 0)
            {
                throw new ArgumentException("枚举父编号不能小于等于0。");
            }

            try
            {
                ds = dalCustomEnum.GetEnumData(parentEnumId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 在下拉型枚举中，根据父节点编号和枚举数据查询枚举节点
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="enumData"></param>
        /// <returns></returns>
        public KeyValueInfo GetDropDownListItem(decimal parentEnumId, PhysicalDataFieldType physicalDataFieldType, string enumData)
        {
            KeyValueInfo keyValueInfo = null;

            if (parentEnumId <= 0)
            {
                throw new ArgumentException("枚举父编号不能小于等于0。");
            }

            try
            {
                keyValueInfo = dalCustomEnum.GetDropDownListItem(parentEnumId, physicalDataFieldType, enumData);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return keyValueInfo;
        }

        /// <summary>
        /// 在树形枚举中，根据父节点编号和枚举数据查询枚举节点
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="enumData"></param>
        /// <returns></returns>
        public KeyValueInfo GetTreeviewItem(decimal parentEnumId, PhysicalDataFieldType physicalDataFieldType, string enumData)
        {
            KeyValueInfo keyValueInfo = null;

            if (parentEnumId <= 0)
            {
                throw new ArgumentException("枚举父编号不能小于等于0。");
            }

            try
            {
                keyValueInfo = dalCustomEnum.GetTreeviewItem(parentEnumId, physicalDataFieldType, enumData);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return keyValueInfo;
        }

            /// <summary>
        /// 根据父节点编号和枚举名称获得枚举数据
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public object GetTreeData(decimal parentEnumId, string enumName, PhysicalDataFieldType physicalDataFieldType)
        {
            object obj = null;

            if (parentEnumId <= 0)
            {
                throw new ArgumentException("枚举父编号不能小于等于0。");
            }

            try
            {
                obj = dalCustomEnum.GetTreeData(parentEnumId, enumName, physicalDataFieldType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return obj;
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
            object obj = null;

            if (parentEnumId <= 0)
            {
                throw new ArgumentException("枚举父编号不能小于等于0。");
            }

            try
            {
                obj = dalCustomEnum.GetDropdownListData(parentEnumId, enumName, physicalDataFieldType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return obj;
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
            string enumName = string.Empty;

            if (parentEnumId <= 0)
            {
                throw new ArgumentException("枚举父编号不能小于等于0。");
            }

            try
            {
                enumName = dalCustomEnum.GetDropdownListEnumName(parentEnumId, value, physicalDataFieldType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumName;
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
            string enumName = string.Empty;

            if (parentEnumId <= 0)
            {
                throw new ArgumentException("枚举父编号不能小于等于0。");
            }

            try
            {
                enumName = dalCustomEnum.GetTreeEnumName(parentEnumId, value, physicalDataFieldType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumName;
        }

        /// <summary>
        /// 获得枚举数据
        /// </summary>
        /// <param name="enumId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public object GetEnumData(decimal enumId, PhysicalDataFieldType physicalDataFieldType)
        {
            object obj = null;

            if (enumId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                obj = dalCustomEnum.GetEnumData(enumId, physicalDataFieldType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return obj;
        }

        /// <summary>
        /// 根据枚举编码获得枚举编号
        /// </summary>
        /// <param name="enumCode"></param>
        /// <returns></returns>
        public decimal GetEnumId(string enumCode)
        {
            decimal enumId = 0;

            if (string.IsNullOrWhiteSpace(enumCode))
            {
                throw new ArgumentException("枚举编码不能为空。");
            }

            try
            {
                enumId = dalCustomEnum.GetEnumId(enumCode);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumId;
        }

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        public IList<string> GetTemplateColumnCaptions()
        {
            IList<string> columnCaptions = null;

            try
            {
                columnCaptions = dalCustomEnum.GetTemplateColumnCaptions();
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return columnCaptions;
        }

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal parentEnumId)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomEnum.GetPageRecord(parentEnumId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
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
            DataSet ds = null;

            try
            {
                ds = dalCustomEnum.GetPageRecord(startPosition, count, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得枚举选项列表
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <returns></returns>
        public IList<CustomEnumInfo> GetEnumItems(decimal parentEnumId)
        {
            IList<CustomEnumInfo> enumItems = null;

            if (parentEnumId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                enumItems = dalCustomEnum.GetEnumItems(parentEnumId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return enumItems;
        }

        /// <summary>
        /// 获得树形枚举集合和当前枚举
        /// </summary>
        /// <param name="parentEnumId"></param>
        /// <param name="enumName"></param>
        /// <returns></returns>
        public CommonItemList<decimal, CommonNode> GetTreeviewCommonNodes(decimal parentEnumId, string enumName)
        {
            CommonItemList<decimal, CommonNode> commonItemList = null;

            // 验证输入
            if (parentEnumId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }
            if (string.IsNullOrWhiteSpace(enumName))
            {
                throw new ArgumentException("枚举名称不能为空。");
            }

            try
            {
                commonItemList = dalCustomEnum.GetTreeviewCommonNodes(parentEnumId, enumName);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonItemList;
        }

        /// <summary>
        /// 获取枚举的最大层级
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public int GetMaxLevel(decimal enumId)
        {
            int level = 0;

            // 验证输入
            if (enumId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                level = dalCustomEnum.GetMaxLevel(enumId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return level;
        }

        /// <summary>
        /// 是否是超大枚举
        /// </summary>
        /// <param name="enumId"></param>
        /// <returns></returns>
        public bool GetSuperEnumEnabled(decimal enumId)
        {
            bool superEnumEnabled = false;

            // 验证输入
            if (enumId <= 0)
            {
                throw new ArgumentException("编号不能小于等于0。");
            }

            try
            {
                superEnumEnabled = dalCustomEnum.GetSuperEnumEnabled(enumId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return superEnumEnabled;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
