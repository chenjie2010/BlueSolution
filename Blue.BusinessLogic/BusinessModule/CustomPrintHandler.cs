//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomPrintHandler.cs
// 描述: CustomPrint 业务处理类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.BusinessLibrary;
using Blue.CustomLibrary;
using Blue.DALFactory;
using Blue.IDAL.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.BusinessInterface.BusinessModule;

namespace Blue.BusinessLogic.BusinessModule
{
    /// <summary>
    /// 业务层处理类，对于的表： dbo.CustomPrint.
    /// </summary>
    public class CustomPrintHandler : CommonNodeBusiness, ICustomPrintHandler
    {
        #region 工厂类实例
        
        private static readonly ICustomPrint dalCustomPrint = BusinessDataAccessFactory.CreateCustomPrint();
        private static readonly ICustomPrintAndDataField dalCustomPrintAndDataField = BusinessDataAccessFactory.CreateCustomPrintAndDataField();
        private static readonly IPrintRecord printRecord = BusinessDataAccessFactory.CreatePrintRecord();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomPrintHandler() : base(dalCustomPrint)
		{  
		}
        
		#endregion

        #region 默认方法
		
		/// <summary>
		/// 向 customprint 表中插入一条新记录
		/// </summary>
		/// <param name="customPrintInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomPrintInfo customPrintInfo)
		{
            //自动增加的关键字的值
			decimal customPrintId = 0;
            
			// 验证输入
			if (customPrintInfo == null)
            {
				throw new ArgumentException("不能插入空对象。");
            }
            
            try
            {
                customPrintId = dalCustomPrint.Insert(customPrintInfo);
                
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
			return customPrintId;
		}
        
        /// <summary>
		/// 获得 CustomPrintInfo 对象
		/// </summary>
		///<param name="printId">数据打印编号</param>
		/// <returns> CustomPrintInfo 对象</returns>
		public CustomPrintInfo GetModelInfo(decimal printId)
		{			
			CustomPrintInfo  customPrintInfo = null;
            
			// 验证输入
			if(printId <= 0)
            {
				throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                customPrintInfo =  dalCustomPrint.GetModelInfo(printId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

			return customPrintInfo;
		}        
        
        /// <summary>
		/// 更新 CustomPrintInfo 对象
		/// </summary>
		/// <param name="customPrintInfo">CustomPrintInfo 对象</param>
		public void Update(CustomPrintInfo customPrintInfo)
		{	
            // 验证输入
            if (customPrintInfo == null)
            {
				throw new ArgumentException("不能更新空对象。");
            }            
            try
            {
                dalCustomPrint.Update(customPrintInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        /// <summary>
		/// 删除 CustomPrintInfo 对象
		/// </summary>
		///<param name="printId">数据打印编号</param>
		/// <returns> CustomPrintInfo 对象</returns>
		public void Delete(decimal printId)
		{		
            // 验证输入
			if(printId <= 0)
            {
				throw new ArgumentException("编号错误。");
            }
            
            try
            {
                dalCustomPrint.Delete(printId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
		
        /// <summary>
		/// 获得 CustomPrintInfo  对象的列表
		/// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomPrintInfo  对象列表</returns>
		public IList<CustomPrintInfo > GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
		{        
            //创建集合对象
			IList<CustomPrintInfo>  customPrintInfos = null;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                customPrintInfos = dalCustomPrint.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
            
            return customPrintInfos;
		}               
        
        /// <summary>
		/// 获得 CustomSheet 表中记录的数目
		/// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
		/// <returns>CustomSheetInfo 记录的数目</returns>
		public int GetTotalCount(IList<WhereConditon> whereConditons)
		{
            int count = 0;
            
            if(whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }
            
            try
            {
                count = dalCustomPrint.GetTotalCount(whereConditons);
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
        /// 向 PrintRecord 表中插入一条新记录
        /// </summary>
        /// <param name="printRecordInfo">printRecordInfo 对象</param>
        /// <returns>自动增加的关键字的值</returns>
		public decimal InsertPrintRecordInfo(PrintRecordInfo userAndPrintInfo)
        {
            //自动增加的关键字的值
            decimal printRecordId = 0;

            // 验证输入
            if (userAndPrintInfo == null)
            {
                throw new ArgumentException("不能插入空对象。");
            }

            try
            {
                printRecordId = printRecord.Insert(userAndPrintInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return printRecordId;
        }

        /// <summary>
        /// 获得以表 UserAndPrint 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecordOfMultiTables(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                ds = printRecord.GetPageRecordOfMultiTables(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表的类型
        /// </summary>
        /// <param name="printId"></param>
        /// <returns></returns>
        public byte GetTableType(decimal printId)
        {
            byte tableType = 0;

            // 验证输入
            if (printId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                tableType = dalCustomPrint.GetTableType(printId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableType;
        }

        /// <summary>
        /// 获得打印内容
        /// </summary>
        ///<param name="printId">打印编号</param>
        /// <returns> 打印内容</returns>
        public string GetPrintContent(decimal printId)
        {
            string printContent = string.Empty;

            // 验证输入
            if (printId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                printContent = dalCustomPrint.GetPrintContent(printId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return printContent;
        }

        /// <summary>
        /// 获得打印系统字段
        /// </summary>
        ///<param name="printId">打印编号</param>
        /// <returns> 表的逻辑名称</returns>
        public Int64 GetSystemDataField(decimal printId)
        {
            Int64 systemDataField = 0;

            // 验证输入
            if (printId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                systemDataField = dalCustomPrint.GetSystemDataField(printId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return systemDataField;
        }

        /// <summary>
        /// 更新打印内容
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="printContent"></param>
        public void UpdatePrintContent(decimal printId, string printContent)
        {
            // 验证输入
            if (printId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomPrint.UpdatePrintContent(printId, printContent);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新字段集合
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <param name="customPrintAndDataFieldInfos"></param>
        public void UpdateDataFields(decimal printId, byte dataFieldPrintType, IList<CustomPrintAndDataFieldInfo> customPrintAndDataFieldInfos)
        {
            // 验证输入
            if (printId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomPrintAndDataField.UpdateDataFields(printId, dataFieldPrintType, customPrintAndDataFieldInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 更新打印内容
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="printContent"></param>
        /// <param name="upLoadFileInfos"></param>
        public void UpdatePrintContent(decimal printId, string printContent, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            // 验证输入
            if (printId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomPrint.UpdatePrintContent(printId, printContent, upLoadFileInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得打印字段
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDataFields(decimal printId, byte dataFieldPrintType)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (printId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalCustomPrintAndDataField.GetDataFields(printId, dataFieldPrintType);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;            
        }

        /// <summary>
        /// 获得启用的打印对象列表
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetActiveCommonNodes(decimal groupId)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (groupId <= 0)
            {
                throw new ArgumentException("分组编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalCustomPrint.GetCommonNodes(groupId, true);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
