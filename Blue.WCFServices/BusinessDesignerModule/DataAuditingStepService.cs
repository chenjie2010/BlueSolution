//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataAuditingStepService.cs
// 描述: DataAuditingStep 操作服务类
// 作者：ChenJie 
// 编写日期：2018/10/19
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Unity;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using Blue.Model.BusinessDesignerModule;
using Blue.BusinessInterface.BusinessDesignerModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.CustomLibrary.EnterpriseLibrary;

namespace Blue.WCFServices.BusinessDesignerModule
{
    /// <summary>
    /// 操作服务类，对于的表： dbo.DataAuditingStep.
    /// </summary>
    public class DataAuditingStepService : IDataAuditingStepContract
    {
        #region 业务实例
        
        private static readonly IDataAuditingStepHandler dataAuditingStepHandler = BusinessLogicContainer.Instance.BusinessDesignerModuleContainer.Resolve<IDataAuditingStepHandler>();

        #endregion
        
		#region 构造函数
		/// <summary>
		/// 默认的构造函数
		/// </summary>
		public DataAuditingStepService()
		{
              
		}
		#endregion

        #region 实现默认契约接口
		
		/// <summary>
		/// 向 DataAuditingStep 表中插入一条新记录
		/// </summary>
		/// <param name="dataAuditingStepInfo"></param>
		/// <returns></returns>
		public decimal Insert(DataAuditingStepInfo dataAuditingStepInfo)
		{
            return dataAuditingStepHandler.Insert(dataAuditingStepInfo);
		}
        
        /// <summary>
		/// 获得 DataAuditingStepInfo 对象
		/// </summary>
		///<param name="stepId">步骤编号</param>
		/// <returns> DataAuditingStepInfo 对象</returns>
		public DataAuditingStepInfo GetModelInfo(decimal stepId)
		{	
            return dataAuditingStepHandler.GetModelInfo(stepId);           
		}		
		
        /// <summary>
		/// 更新 DataAuditingStepInfo 对象
		/// </summary>
		/// <param name="dataAuditingStepInfo">DataAuditingStepInfo 对象</param>
		public void Update(DataAuditingStepInfo dataAuditingStepInfo)
		{	          
            dataAuditingStepHandler.Update(dataAuditingStepInfo);
        }	
  
        /// <summary>
		/// 删除 DataAuditingStepInfo 对象
		/// </summary>
		///<param name="stepId">步骤编号</param>
		/// <returns> DataAuditingStepInfo 对象</returns>
		public void Delete(decimal stepId)
		{	
            dataAuditingStepHandler.Delete(stepId);
        }
        
        /// <summary>
        /// 获得 DataAuditingStepInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>DataAuditingStepInfo 对象列表</returns>
        public IList<DataAuditingStepInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return dataAuditingStepHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 DataAuditingStep 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> DataAuditingStepInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return dataAuditingStepHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 获得以表 DataAuditingStep 为主表的多表的分页数据集(可以多字段排序，多个字段都可以有自己单独的排序方式)
        /// 必须要求主键，主键可以是任意类型
        /// </summary>
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>  
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetDataAuditingSteps(int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            return dataAuditingStepHandler.GetDataAuditingSteps(startPosition, count, whereConditons, sortingCondtions, ref totalCount);
        }

        /// <summary>
        /// 获得日志流程
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public DataSet GetSteps(decimal auditingLogId)
        {
            return dataAuditingStepHandler.GetSteps(auditingLogId);
        }

        /// <summary>
        /// 获得最新审核人
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public CommonNode GetLastestReviewer(decimal auditingLogId)
        {
            return dataAuditingStepHandler.GetLastestReviewer(auditingLogId);
        }

        /// <summary>
        /// 获得最新提交人
        /// </summary>
        /// <param name="auditingLogId"></param>
        /// <returns></returns>
        public CommonNode GetLastestSubmitter(decimal auditingLogId)
        {
            return dataAuditingStepHandler.GetLastestSubmitter(auditingLogId);
        }

        #endregion
    }
}
