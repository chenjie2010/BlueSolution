//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：CustomTableHandler.cs
// 描述：CustomTable 业务处理类
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
    /// CustomTable 接口
    /// </summary>
    public interface ICustomTableHandler: ICommonNodeBusiness, IPrincipalBusiness<CustomTableInfo>
    {
        #region 接口

        /// <summary>
        /// 清除表
        /// </summary>
        /// <param name="tableId"></param>
        void TruncatedTable(decimal tableId);

        /// <summary>
        /// 根据用户编号更新排序
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        void UpdateRecordSortingByUserId(decimal userId, decimal tableId);

        /// <summary>
        /// 根据业务编号更新排序
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="tableId"></param>
        void UpdateRecordSortingByBusinessId(decimal businessId, decimal tableId);

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="where"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        int GetRecordCount(decimal tableId, string where, IList<WhereConditon> whereConditons);

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
        int GetRecordCount(decimal tableId, IList<WhereConditon> whereConditons);

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
        DataSet GetTableData(decimal tableId, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions);

        /// <summary>
        /// 检查枚举一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckEnumConsistency(decimal tableId, bool selectOrUpdate);

        /// <summary>
        /// 检查用户数据冗余性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        CommonItemList<int, string> CheckUserDataConsistency(decimal tableId, bool selectOrUpdate);

        /// <summary>
        /// 检查用户一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        IDictionary<SystemPhysicalDataField, CommonItemList<int, string>> CheckUserConsistency(decimal tableId, bool selectOrUpdate);

        /// <summary>
        /// 检查关联一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckAssociationConsistency(decimal tableId, bool selectOrUpdate);

        /// <summary>
        /// 检查联动一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckRelationConsistency(decimal tableId, bool selectOrUpdate);

        /// <summary>
        /// 设置审核状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="auditedStatus"></param>
        /// <returns></returns>
        int SetAuditedStatus(decimal tableId, IList<WhereConditon> whereConditons, AuditedStatus auditedStatus);

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        void Import(decimal tableId, DataTable dataTable);

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        void Import(decimal tableId, DataTable dataTable, IDictionary<string, string> dataFieldRelation);

        /// <summary>
        /// 提交更新
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="exportStyle"></param>
        /// <returns></returns>
        int Sumbit(decimal tableId, ExportStyle exportStyle);

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataTable">第0列为 UserId, 其他为数据列</param>
        /// <param name="currentState">主从表更新条件</param>
        /// <param name="whereCluases"></param>
        void Update(decimal tableId, DataTable dataTable, CurrentState currentState, IList<string> whereCluases);

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        void Import(decimal tableId, DataTable dataTable, IDictionary<string, string> dataFieldRelation, bool clear);

        /// <summary>
        /// 表中是否有满足条件的记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        bool IsExistedRecord(decimal userId, decimal tableId);
        
        /// <summary>
        /// 表中是否有满足条件的记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        bool IsExistedRecord(decimal userId, decimal tableId, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得用户在表中的记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        int GetRecordCount(decimal userId, decimal tableId, bool businessEnabled, decimal instanceId);

        /// <summary>
        /// 获得表的类型
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 表的逻辑名称</returns>
        byte GetTableType(decimal tableId);

        /// <summary>
        /// 获得数据库下所有的表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        List<CommonNode> GetCommonNodesByDatabaseId(decimal databaseId);

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="businessId"></param>
        /// <param name="onlyTarget"></param>
        /// <returns></returns>
        DataTable GetMirrorRowData(decimal tableId, Int64 systemLogicalDataFields, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, decimal businessId, bool onlyTarget);

        /// <summary>
        /// 根据审核状态条件删除表中的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="auditedStatus"></param>
        void DeleteRecords(decimal tableId, AuditedStatus auditedStatus);

        /// <summary>
        /// 审核记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="auditedState">审核状态</param>
        void Audit(decimal tableId, decimal recordId, AuditedStatus auditedStatus);

        /// <summary>
        /// 批量审核记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordIds"></param>
        /// <param name="auditedState">审核状态</param>
        void Audit(decimal tableId, IList<decimal> recordIds, AuditedStatus auditedStatus);

        /// <summary>
        /// 根据查询条件和审核状态删除表中的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="auditedStatus"></param>
        void DeleteRecords(decimal tableId, IList<WhereConditon> whereConditons, AuditedStatus auditedStatus);

        /// <summary>
        /// 删除表中该用户的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="userId"></param>
        /// <param name="auditedStatus"></param>
        void DeleteRecordsByUserId(decimal tableId, decimal userId, AuditedStatus auditedStatus);

        /// <summary>
        /// 获得记录的审核状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        AuditedStatus GetAuditedStatus(decimal tableId, decimal recordId);

        /// <summary>
        /// 重置表
        /// </summary>
        /// <param name="tableId"></param>
        void ResetTable(decimal tableId);


        /// <summary>
        /// 复制表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        decimal CopyTable(decimal categoryId, decimal tableId);

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        DataSet GetPageRecordByTableId(decimal tableId);

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        DataSet GetPageRecord(IList<decimal> categoryIds);

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        DataSet GetPageRecord(decimal categoryId);

        /// <summary>
        /// 重置表结构并更新表信息
        /// </summary>
        /// <param name="customTableInfo"></param>
		void ResetAndUpdateTable(CustomTableInfo customTableInfo);

        /// <summary>
        /// 获得表的当前既往状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        bool GetCurrentState(decimal tableId);

        /// <summary>
        /// 移动记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="movedDriection"></param>
        void MoveRecord(decimal userId, decimal tableId, decimal recordId, MovedDriection movedDriection);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        void DeleteRecord(decimal tableId, decimal recordId);

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordIds"></param>
        void DeleteRecords(decimal tableId, IList<decimal> recordIds);

        /// <summary>
        /// 获得物理表名
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 物理表名</returns>
        string GetTablePhysicalName(decimal tableId);

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldIds"></param>
        /// <param name="where"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        DataTable GetTableData(decimal tableId, IList<decimal> dataFieldIds, string where, IList<WhereConditon> whereConditons);

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
        DataSet GetTableData(decimal tableId, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);
        
        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <param name="dataFieldNameRelations"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        DataTable GetTableData(decimal tableId, Int64 systemLogicalDataFields, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="commonDataFieldInfos"></param>
        /// <returns></returns>
        DataTable GetTableData(decimal tableId, decimal recordId, List<CommonDataFieldInfo> commonDataFieldInfos);

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
        DataTable GetTableData(decimal tableId, Int64 systemLogicalDataFields, bool hasUserAccount, bool hasDepartmentProperty, Dictionary<string, CommonDataFieldInfo> dataFieldNameRelations,
            int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount);

        /// <summary>
        /// 获得表的类型
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        DataTableType GetDataTableType(decimal tableId);

        /// <summary>
        /// 根据表类型条件获得表节点
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tableFilter"></param>
        /// <returns></returns>
        IList<CommonNode> GetCommonNodes(decimal categoryId, TableFilter tableFilter);

        /// <summary>
        /// 获得表的逻辑名称
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 表的逻辑名称</returns>
        string GetTableLogicalName(decimal tableId);

        /// <summary>
        /// 获得数据库编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        decimal GetDatabaseId(decimal tableId);

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        byte GetDataWarehouseId(decimal tableId);

        #endregion
    }
}
