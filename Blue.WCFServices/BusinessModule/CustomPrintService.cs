//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CustomPrintService.cs
// 描述: CustomPrint 操作服务类
// 作者：ChenJie 
// 编写日期：2018/9/28
// Copyright 2018
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
    /// 操作服务类，对于的表： dbo.CustomPrint.
    /// </summary>
    public class CustomPrintService : CommonNodeServices, ICustomPrintContract
    {
        #region 业务实例
        
        private static readonly ICustomPrintHandler customPrintHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomPrintHandler>();

        #endregion
        
		#region 构造函数

		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public CustomPrintService() : base(customPrintHandler)
        {
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 CustomPrint 表中插入一条新记录
		/// </summary>
		/// <param name="customPrintInfo"></param>
		/// <returns></returns>
		public decimal Insert(CustomPrintInfo customPrintInfo)
		{
            return customPrintHandler.Insert(customPrintInfo);
		}
        
        /// <summary>
		/// 获得 CustomPrintInfo 对象
		/// </summary>
		///<param name="printId">数据打印编号</param>
		/// <returns> CustomPrintInfo 对象</returns>
		public CustomPrintInfo GetModelInfo(decimal printId)
		{	
            return customPrintHandler.GetModelInfo(printId);           
		}		
		
        /// <summary>
		/// 更新 CustomPrintInfo 对象
		/// </summary>
		/// <param name="customPrintInfo">CustomPrintInfo 对象</param>
		public void Update(CustomPrintInfo customPrintInfo)
		{	          
            customPrintHandler.Update(customPrintInfo);
        }	
  
        /// <summary>
		/// 删除 CustomPrintInfo 对象
		/// </summary>
		///<param name="printId">数据打印编号</param>
		/// <returns> CustomPrintInfo 对象</returns>
		public void Delete(decimal printId)
		{	
            customPrintHandler.Delete(printId);
        }
        
        /// <summary>
        /// 获得 CustomPrintInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomPrintInfo 对象列表</returns>
        public IList<CustomPrintInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customPrintHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomPrint 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> CustomPrintInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customPrintHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 向 UserAndPrint 表中插入一条新记录
        /// </summary>
        /// <param name="userAndPrintInfo">userAndPrintInfo 对象</param>
        public void InsertPrintRecordInfo(PrintRecordInfo userAndPrintInfo)
        {
            customPrintHandler.InsertPrintRecordInfo(userAndPrintInfo);
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
            return customPrintHandler.GetPageRecordOfMultiTables(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
        }

        /// <summary>
        /// 获得打印内容
        /// </summary>
        ///<param name="printId">打印编号</param>
        /// <returns> 打印内容</returns>
        public string GetPrintContent(decimal printId)
        {
            return customPrintHandler.GetPrintContent(printId);
        }

        /// <summary>
        /// 获得打印系统字段
        /// </summary>
        ///<param name="printId">打印编号</param>
        /// <returns> 表的逻辑名称</returns>
        public Int64 GetSystemDataField(decimal printId)
        {
            return customPrintHandler.GetSystemDataField(printId);
        }

        /// <summary>
        /// 更新打印内容
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="printContent"></param>
        public void UpdatePrintContent(decimal printId, string printContent)
        {
            customPrintHandler.UpdatePrintContent(printId, printContent);
        }

        /// <summary>
        /// 更新打印内容
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="printContent"></param>
        /// <param name="upLoadFileInfos"></param>
        public void UpdatePrintContent(decimal printId, string printContent, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
        {
            customPrintHandler.UpdatePrintContent(printId, printContent, upLoadFileInfos);
        }

        /// <summary>
        /// 更新表的字段集合
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <param name="customPrintAndDataFieldInfos"></param>
        public void UpdateDataFields(decimal printId, byte dataFieldPrintType, IList<CustomPrintAndDataFieldInfo> customPrintAndDataFieldInfos)
        {
            customPrintHandler.UpdateDataFields(printId, dataFieldPrintType, customPrintAndDataFieldInfos);
        }

        /// <summary>
        /// 获得打印字段
        /// </summary>
        /// <param name="printId"></param>
        /// <param name="dataFieldPrintType"></param>
        /// <returns></returns>
        public IList<CommonNode> GetDataFields(decimal printId, byte dataFieldPrintType)
        {
            return customPrintHandler.GetDataFields(printId, dataFieldPrintType);
        }

        #endregion
    }
}
