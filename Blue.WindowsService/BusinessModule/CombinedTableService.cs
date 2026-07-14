//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CombinedTableService.cs
// 描述: CombinedTable 操作服务类
// 作者：ChenJie 
// 编写日期：2018/8/15
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
    /// 操作服务类，对于的表： dbo.CombinedTable.
    /// </summary>
    public class CombinedTableService : CommonNodeServices, ICombinedTableContract
    {
        #region 业务实例

        private static readonly ICombinedTableHandler combinedTableHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICombinedTableHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CombinedTableService() : base(combinedTableHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 CombinedTable 表中插入一条新记录
        /// </summary>
        /// <param name="combinedTableInfo"></param>
        /// <returns></returns>
        public decimal Insert(CombinedTableInfo combinedTableInfo)
        {
            return combinedTableHandler.Insert(combinedTableInfo);
        }

        /// <summary>
        /// 获得 CombinedTableInfo 对象
        /// </summary>
        ///<param name="combinedTableId"></param>
        /// <returns> CombinedTableInfo 对象</returns>
        public CombinedTableInfo GetModelInfo(decimal combinedTableId)
        {
            return combinedTableHandler.GetModelInfo(combinedTableId);
        }

        /// <summary>
        /// 更新 CombinedTableInfo 对象
        /// </summary>
        /// <param name="combinedTableInfo">CombinedTableInfo 对象</param>
        public void Update(CombinedTableInfo combinedTableInfo)
        {
            combinedTableHandler.Update(combinedTableInfo);
        }

        /// <summary>
        /// 删除 CombinedTableInfo 对象
        /// </summary>
        ///<param name="combinedTableId"></param>
        /// <returns> CombinedTableInfo 对象</returns>
        public void Delete(decimal combinedTableId)
        {
            combinedTableHandler.Delete(combinedTableId);
        }

        /// <summary>
        /// 获得 CombinedTableInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CombinedTableInfo 对象列表</returns>
        public IList<CombinedTableInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return combinedTableHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CombinedTable 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns> CombinedTableInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return combinedTableHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口


        /// <summary>
        /// 获得组合表的记录数量
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal combinedTableId, IList<WhereConditon> whereConditons)
        {
            return combinedTableHandler.GetRecordCount(combinedTableId, whereConditons);
        }

        /// <summary>
        /// 获得组合表的记录
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public DataSet GetTableData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return combinedTableHandler.GetTableData(combinedTableId, dataFieldNameRelations, startPosition, count, whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="instanceId"></param>
        /// <param name="onlyTarget"></param>
        /// <returns></returns>
        public Dictionary<decimal, DataRowItem> GetMirrorRowData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal instanceId, bool onlyTarget)
        {
            return combinedTableHandler.GetMirrorRowData(combinedTableId, dataFieldNameRelations, instanceId, onlyTarget);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public Dictionary<decimal, DataTable> GetMirrorRowData(decimal combinedTableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal instanceId)
        {
            return combinedTableHandler.GetMirrorRowData(combinedTableId, dataFieldNameRelations, instanceId);
        }

        /// <summary>
        /// 获得表的编号列表
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public IList<decimal> GetTableIds(decimal combinedTableId)
        {
            return combinedTableHandler.GetTableIds(combinedTableId);
        }

        /// <summary>
        /// 获得组合表的数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public DataTable GetCombinedTableData(decimal combinedTableId, bool businessEnabled, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId, decimal instanceId)
        {
            return combinedTableHandler.GetCombinedTableData(combinedTableId, businessEnabled, dataFieldNameRelations, userId, instanceId);
        }
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="userId"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public Dictionary<decimal, DataTable> GetDataFilledData(decimal combinedTableId, bool businessEnabled, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal userId, decimal instanceId)
        {
            return combinedTableHandler.GetDataFilledData(combinedTableId, businessEnabled, dataFieldNameRelations, userId, instanceId);
        }

        /// <summary>
        /// 更新组合表信息
        /// </summary>
        /// <param name="combinedTableInfo"></param>
        /// <param name="combinedTableRelationInfos"></param>
        public void Update(CombinedTableInfo combinedTableInfo, IList<CombinedTableRelationInfo> combinedTableRelationInfos)
        {
            combinedTableHandler.Update(combinedTableInfo, combinedTableRelationInfos);
        }

        /// <summary>
        /// 向 CombinedTable 表中插入一条新记录
        /// </summary>
        /// <param name="combinedTableInfo">combinedTableInfo 对象</param>
        /// <param name="combinedTableRelationInfos">关系表</param>
        /// <returns>自动增加的关键字的值</returns>
        public decimal Insert(CombinedTableInfo combinedTableInfo, IList<CombinedTableRelationInfo> combinedTableRelationInfos)
        {
            return combinedTableHandler.Insert(combinedTableInfo, combinedTableRelationInfos);
        }

        /// <summary>
        /// 获得组合表的字段集合
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public List<CommonNode> GetDataFields(decimal combinedTableId)
        {
            return combinedTableHandler.GetDataFields(combinedTableId);
        }

        /// <summary>
        /// 根据组合表的信息
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <returns></returns>
        public IList<CommonNode> GetTables(decimal combinedTableId)
        {
            return combinedTableHandler.GetTables(combinedTableId);
        }

        /// <summary>
        /// 更新组合表的字段集合
        /// </summary>
        /// <param name="combinedTableId"></param>
        /// <param name="combinedDataFieldInfos"></param>
        public void UpdateDataFields(decimal combinedTableId, IList<CombinedDataFieldInfo> combinedDataFieldInfos)
        {
            combinedTableHandler.UpdateDataFields(combinedTableId, combinedDataFieldInfos);
        }

        #endregion
    }
}
