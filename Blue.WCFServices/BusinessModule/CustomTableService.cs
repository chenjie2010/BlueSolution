//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomTableService.cs
// 描述：CustomTable 操作服务类
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
    /// 操作服务类，对于的表： dbo.CustomTable.
    /// </summary>
    public class CustomTableService : CommonNodeServices, ICustomTableContract
    {
        #region 业务实例

        private static readonly ICustomTableHandler customTableHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<ICustomTableHandler>();

        #endregion

        #region 构造函数
        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomTableService() : base(customTableHandler)
        {

        }
        #endregion

        #region 实现默认契约接口

        /// <summary>
        /// 向 customtable 表中插入一条新记录
        /// </summary>
        /// <param name="customTableInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomTableInfo customTableInfo)
        {
            return customTableHandler.Insert(customTableInfo);
        }

        /// <summary>
        /// 获得 CustomTableInfo 对象
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> CustomTableInfo 对象</returns>
        public CustomTableInfo GetModelInfo(decimal tableId)
        {
            return customTableHandler.GetModelInfo(tableId);
        }

        /// <summary>
        /// 更新 CustomTableInfo 对象
        /// </summary>
        /// <param name="customTableInfo">CustomTableInfo 对象</param>
        public void Update(CustomTableInfo customTableInfo)
        {
            customTableHandler.Update(customTableInfo);
        }

        /// <summary>
        /// 删除 CustomTableInfo 对象
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> CustomTableInfo 对象</returns>
        public void Delete(decimal tableId)
        {
            customTableHandler.Delete(tableId);
        }

        /// <summary>
        /// 获得 CustomTableInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomTableInfo 对象列表</returns>
        public IList<CustomTableInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customTableHandler.GetModelInfos(whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 获得 CustomTable 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomTableInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            return customTableHandler.GetTotalCount(whereConditons);
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 清除表
        /// </summary>
        /// <param name="tableId"></param>
        public void TruncatedTable(decimal tableId)
        {
            customTableHandler.TruncatedTable(tableId);
        }

        /// <summary>
        /// 根据用户编号更新排序
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        public void UpdateRecordSortingByUserId(decimal userId, decimal tableId)
        {
            customTableHandler.UpdateRecordSortingByUserId(userId, tableId);
        }

        /// <summary>
        /// 根据业务编号更新排序
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="tableId"></param>
        public void UpdateRecordSortingByBusinessId(decimal businessId, decimal tableId)
        {
            customTableHandler.UpdateRecordSortingByBusinessId(businessId, tableId);
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal tableId, IList<WhereConditon> whereConditons)
        {
            return customTableHandler.GetRecordCount(tableId, whereConditons);
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public DataSet GetTableData(decimal tableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            return customTableHandler.GetTableData(tableId, dataFieldNameRelations, startPosition, count, whereConditons, sortingCondtions);
        }

        /// <summary>
        /// 检查枚举一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckEnumConsistency(decimal tableId, bool selectOrUpdate)
        {
            return customTableHandler.CheckEnumConsistency(tableId, selectOrUpdate);
        }

        /// <summary>
        /// 检查用户一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<SystemPhysicalDataField, CommonItemList<int, string>> CheckUserConsistency(decimal tableId, bool selectOrUpdate)
        {
            return customTableHandler.CheckUserConsistency(tableId, selectOrUpdate);
        }

        /// <summary>
        /// 检查用户数据冗余性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public CommonItemList<int, string> CheckUserDataConsistency(decimal tableId, bool selectOrUpdate)
        {
            return customTableHandler.CheckUserDataConsistency(tableId, selectOrUpdate);
        }

        /// <summary>
        /// 检查关联一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckAssociationConsistency(decimal tableId, bool selectOrUpdate)
        {
            return customTableHandler.CheckAssociationConsistency(tableId, selectOrUpdate);
        }

        /// <summary>
        /// 检查联动一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckRelationConsistency(decimal tableId, bool selectOrUpdate)
        {
            return customTableHandler.CheckRelationConsistency(tableId, selectOrUpdate);
        }

        /// <summary>
        /// 设置审核状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="auditedStatus"></param>
        /// <returns></returns>
        public int SetAuditedStatus(decimal tableId, IList<WhereConditon> whereConditons, AuditedStatus auditedStatus)
        {
            return customTableHandler.SetAuditedStatus(tableId, whereConditons, auditedStatus);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        public void Import(decimal tableId, DataTable dataTable)
        {
            customTableHandler.Import(tableId, dataTable);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        public void Import(decimal tableId, DataTable dataTable, IDictionary<string, string> dataFieldRelation)
        {
            customTableHandler.Import(tableId, dataTable, dataFieldRelation);
        }

        /// <summary>
        /// 提交更新
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="exportStyle"></param>
        /// <returns></returns>
        public int Sumbit(decimal tableId, ExportStyle exportStyle)
        {
            return customTableHandler.Sumbit(tableId, exportStyle);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        public void Import(decimal tableId, DataTable dataTable, IDictionary<string, string> dataFieldRelation, bool clear)
        {
            customTableHandler.Import(tableId, dataTable, dataFieldRelation, clear);
        }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataTable">第0列为 UserId, 其他为数据列</param>
        /// <param name="currentState">主从表更新条件</param>
        /// <param name="whereCluases"></param>
        public void Update(decimal tableId, DataTable dataTable, CurrentState currentState, IList<string> whereCluases)
        {
            customTableHandler.Update(tableId, dataTable, currentState, whereCluases);
        }

        /// <summary>
        /// 表中是否有满足条件的记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool IsExistedRecord(decimal userId, decimal tableId)
        {
            return customTableHandler.IsExistedRecord(userId, tableId);
        }

        /// <summary>
        /// 表中是否有满足条件的记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool IsExistedRecord(decimal userId, decimal tableId, IList<WhereConditon> whereConditons)
        {
            return customTableHandler.IsExistedRecord(userId, tableId, whereConditons);
        }

        /// <summary>
        /// 获得表的类型
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 表的逻辑名称</returns>
        public byte GetTableType(decimal tableId)
        {
            return customTableHandler.GetTableType(tableId);
        }

        /// <summary>
        /// 获得数据库下所有的表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public List<CommonNode> GetCommonNodesByDatabaseId(decimal databaseId)
        {
            return customTableHandler.GetCommonNodesByDatabaseId(databaseId);
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="businessId"></param>
        /// <param name="onlyTarget"></param>
        /// <returns></returns>
        public DataTable GetMirrorRowData(decimal tableId, Int64 systemLogicalDataFields, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal businessId, bool onlyTarget)
        {
            return customTableHandler.GetMirrorRowData(tableId, systemLogicalDataFields, dataFieldNameRelations, businessId, onlyTarget);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        public void DeleteRecord(decimal tableId, decimal recordId)
        {
            customTableHandler.DeleteRecord(tableId, recordId);
        }

        /// <summary>
        /// 审核记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="auditedState">审核状态</param>
        public void Audit(decimal tableId, decimal recordId, AuditedStatus auditedStatus)
        {
            customTableHandler.Audit(tableId, recordId, auditedStatus);
        }

        /// <summary>
        /// 批量审核记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordIds"></param>
        /// <param name="auditedState">审核状态</param>
        public void Audit(decimal tableId, IList<decimal> recordIds, AuditedStatus auditedStatus)
        {
            customTableHandler.Audit(tableId, recordIds, auditedStatus);
        }

        /// <summary>
        /// 删除表中该用户的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="userId"></param>
        /// <param name="auditedStatus"></param>
        public void DeleteRecordsByUserId(decimal tableId, decimal userId, AuditedStatus auditedStatus)
        {
            customTableHandler.DeleteRecordsByUserId(tableId, userId, auditedStatus);
        }

        /// <summary>
        /// 获得记录的审核状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public AuditedStatus GetAuditedStatus(decimal tableId, decimal recordId)
        {
            return customTableHandler.GetAuditedStatus(tableId, recordId);
        }

        /// <summary>
        /// 重置表
        /// </summary>
        /// <param name="tableId"></param>
        public void ResetTable(decimal tableId)
        {
            customTableHandler.ResetTable(tableId);
        }

        /// <summary>
        /// 复制表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public decimal CopyTable(decimal categoryId, decimal tableId)
        {
            return customTableHandler.CopyTable(categoryId, tableId);
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataSet GetPageRecordByTableId(decimal tableId)
        {
            return customTableHandler.GetPageRecordByTableId(tableId);
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal categoryId)
        {
            return customTableHandler.GetPageRecord(categoryId);
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(IList<decimal> categoryIds)
        {
            return customTableHandler.GetPageRecord(categoryIds);
        }

        /// <summary>
        /// 重置表结构并更新表信息
        /// </summary>
        /// <param name="customTableInfo"></param>
        public void ResetAndUpdateTable(CustomTableInfo customTableInfo)
        {
            customTableHandler.ResetAndUpdateTable(customTableInfo);
        }

        /// <summary>
        /// 获得表的当前既往状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool GetCurrentState(decimal tableId)
        {
            return customTableHandler.GetCurrentState(tableId);
        }

        /// <summary>
        /// 移动记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="movedDriection"></param>
        public void MoveRecord(decimal userId, decimal tableId, decimal recordId, MovedDriection movedDriection)
        {
            customTableHandler.MoveRecord(userId, tableId, recordId, movedDriection);
        }

        /// <summary>
        /// 根据审核状态条件删除表中的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="auditedStatus"></param>
        public void DeleteRecords(decimal tableId, AuditedStatus auditedStatus)
        {
            customTableHandler.DeleteRecords(tableId, auditedStatus);
        }

        /// <summary>
        /// 根据查询条件和审核状态删除表中的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="auditedStatus"></param>
        public void DeleteRecords(decimal tableId, IList<WhereConditon> whereConditons, AuditedStatus auditedStatus)
        {
            customTableHandler.DeleteRecords(tableId, whereConditons, auditedStatus);
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordIds"></param>
        public void DeleteRecords(decimal tableId, IList<decimal> recordIds)
        {
            customTableHandler.DeleteRecords(tableId, recordIds);
        }

        /// <summary>
        /// 获得表的分页数据集
        /// </summary>
        /// <param name="tableId">表的编号</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>数据集</returns>
        public DataSet GetTableData(decimal tableId, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            return customTableHandler.GetTableData(tableId, startPosition, count, whereConditons, sortingCondtions, ref totalCount);
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="hasUserAccount"></param>
        /// <param name="hasDepartmentProperty"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataTable GetTableData(decimal tableId, Int64 systemLogicalDataFields, bool hasUserAccount, bool hasDepartmentProperty, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations,
            int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            return customTableHandler.GetTableData(tableId, systemLogicalDataFields, hasUserAccount, hasDepartmentProperty, dataFieldNameRelations, startPosition, count,
                    whereConditons, sortingCondtions, ref totalCount);
        }

        /// <summary>
        /// 获得物理表名
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 物理表名</returns>
        public string GetTablePhysicalName(decimal tableId)
        {
            return customTableHandler.GetTablePhysicalName(tableId);
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public DataTable GetTableData(decimal tableId, Int64 systemLogicalDataFields, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, IList<WhereConditon> whereConditons)
        {
            return customTableHandler.GetTableData(tableId, systemLogicalDataFields, dataFieldNameRelations, whereConditons);
        }

        /// <summary>
        /// 获得表的类型
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataTableType GetDataTableType(decimal tableId)
        {
            return customTableHandler.GetDataTableType(tableId);
        }

        /// <summary>
        /// 根据表类型条件获得表节点
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tableFilter"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal categoryId, TableFilter tableFilter)
        {
            return customTableHandler.GetCommonNodes(categoryId, tableFilter);
        }

        /// <summary>
        /// 获得表的逻辑名称
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 表的逻辑名称</returns>
        public string GetTableLogicalName(decimal tableId)
        {
            return customTableHandler.GetTableLogicalName(tableId);
        }

        /// <summary>
        /// 获得数据库编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public decimal GetDatabaseId(decimal tableId)
        {
            return customTableHandler.GetDatabaseId(tableId);
        }

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal tableId)
        {
            return customTableHandler.GetDataWarehouseId(tableId);
        }
        #endregion
    }
}
