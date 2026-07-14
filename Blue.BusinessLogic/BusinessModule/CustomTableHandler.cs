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
    /// 业务层处理类，对于的表： dbo.CustomTable.
    /// </summary>
    public class CustomTableHandler : CommonNodeBusiness, ICustomTableHandler
    {
        #region 工厂类实例

        private static readonly ICustomTable dalCustomTable = BusinessDataAccessFactory.CreateCustomTable();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CustomTableHandler() : base(dalCustomTable)
        {
        }

        #endregion

        #region 默认方法

        /// <summary>
        /// 向 customtable 表中插入一条新记录
        /// </summary>
        /// <param name="customTableInfo"></param>
        /// <returns></returns>
        public decimal Insert(CustomTableInfo customTableInfo)
        {
            //自动增加的关键字的值
            decimal customTableId = 0;

            // 验证输入
            if (customTableInfo == null)
            {
                throw new ArgumentException("不能插入空对象.");
            }

            try
            {
                customTableId = dalCustomTable.Insert(customTableInfo);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customTableId;
        }

        /// <summary>
        /// 获得 CustomTableInfo 对象
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> CustomTableInfo 对象</returns>
        public CustomTableInfo GetModelInfo(decimal tableId)
        {
            CustomTableInfo customTableInfo = null;

            // 验证输入
            if (tableId < 0)
            {
                return null;
            }

            try
            {
                customTableInfo = dalCustomTable.GetModelInfo(tableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customTableInfo;
        }

        /// <summary>
        /// 更新 CustomTableInfo 对象
        /// </summary>
        /// <param name="customTableInfo">CustomTableInfo 对象</param>
        public void Update(CustomTableInfo customTableInfo)
        {
            // 验证输入
            if (customTableInfo == null)
            {
                throw new ArgumentException("不能更新空对象.");
            }
            try
            {
                dalCustomTable.Update(customTableInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除 CustomTableInfo 对象
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> CustomTableInfo 对象</returns>
        public void Delete(decimal tableId)
        {
            // 验证输入
            if (tableId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                dalCustomTable.Delete(tableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }


        /// <summary>
        /// 获得 CustomTableInfo 对象的列表
        /// </summary>	
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段条件的集合</param>
        /// <returns>CustomTableInfo 对象列表</returns>
        public IList<CustomTableInfo> GetModelInfos(IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            //创建集合对象
            IList<CustomTableInfo> customTableInfos = null;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                customTableInfos = dalCustomTable.GetModelInfos(whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customTableInfos;
        }

        /// <summary>
        /// 获得 CustomTable 表中记录的数目
        /// </summary>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <returns>CustomTableInfo 记录的数目</returns>
        public int GetTotalCount(IList<WhereConditon> whereConditons)
        {
            int count = 0;

            if (whereConditons == null || whereConditons.Count == 0)
            {
                throw new ArgumentException("Where 条件不能为空。");
            }

            try
            {
                count = dalCustomTable.GetTotalCount(whereConditons);
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
        /// 清除表
        /// </summary>
        /// <param name="tableId"></param>
        public void TruncatedTable(decimal tableId)
        {
            if (tableId <= 0)
            {
                throw new ArgumentException("表的编号错误。");
            }

            try
            {
                dalCustomTable.TruncatedTable(tableId);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 根据用户编号更新排序
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        public void UpdateRecordSortingByUserId(decimal userId, decimal tableId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号错误。");
            }

            if (tableId <= 0)
            {
                throw new ArgumentException("表的编号错误。");
            }

            try
            {
                dalCustomTable.UpdateRecordSortingByUserId(userId, tableId);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 根据业务编号更新排序
        /// </summary>
        /// <param name="businessId"></param>
        /// <param name="tableId"></param>
        public void UpdateRecordSortingByBusinessId(decimal businessId, decimal tableId)
        {
            if (businessId <= 0)
            {
                throw new ArgumentException("业务编号错误。");
            }

            if (tableId <= 0)
            {
                throw new ArgumentException("表的编号错误。");
            }

            try
            {
                dalCustomTable.UpdateRecordSortingByBusinessId(businessId, tableId);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="where"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal tableId, string where, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                count = dalCustomTable.GetRecordCount(tableId, where, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
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
            int count = 0;

            try
            {
                count = dalCustomTable.GetRecordCount(tableId, whereConditons);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
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
            DataSet ds = null;

            try
            {
                ds = dalCustomTable.GetTableData(tableId, dataFieldNameRelations, startPosition, count, whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 检查用户数据冗余性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public CommonItemList<int, string> CheckUserDataConsistency(decimal tableId, bool selectOrUpdate)
        {
            CommonItemList<int, string> userData = null;

            if (tableId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                userData = dalCustomTable.CheckUserDataConsistency(tableId, selectOrUpdate);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userData;
        }

        /// <summary>
        /// 检查用户一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<SystemPhysicalDataField, CommonItemList<int, string>> CheckUserConsistency(decimal tableId, bool selectOrUpdate)
        {
            IDictionary<SystemPhysicalDataField, CommonItemList<int, string>> results = null;

            if (tableId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                results = dalCustomTable.CheckUserConsistency(tableId, selectOrUpdate);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return results;
        }

        /// <summary>
        /// 检查枚举一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckEnumConsistency(decimal tableId, bool selectOrUpdate)
        {
            IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> results = null;

            if (tableId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                results = dalCustomTable.CheckEnumConsistency(tableId, selectOrUpdate);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return results;
        }

        /// <summary>
        /// 检查关联一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckAssociationConsistency(decimal tableId, bool selectOrUpdate)
        {
            IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> results = null;

            if (tableId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                results = dalCustomTable.CheckAssociationConsistency(tableId, selectOrUpdate);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return results;
        }

        /// <summary>
        /// 检查联动一致性操作
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="selectOrUpdate"></param>
        /// <returns></returns>
        public IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> CheckRelationConsistency(decimal tableId, bool selectOrUpdate)
        {
            IDictionary<decimal, IDictionary<decimal, CommonItemList<int, string>>> results = null;

            if (tableId < 0)
            {
                throw new ArgumentException("编号错误。");
            }

            try
            {
                results = dalCustomTable.CheckRelationConsistency(tableId, selectOrUpdate);
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return results;
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
            int count = 0;

            if (tableId <= 0)
            {
                throw new ArgumentException("表格编号不能小于或是等于0。");
            }

            try
            {
                count = dalCustomTable.SetAuditedStatus(tableId, whereConditons, auditedStatus);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        public void Import(decimal tableId, DataTable dataTable)
        {
            if (tableId <= 0)
            {
                throw new ArgumentException("表格编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.Import(tableId, dataTable);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 提交更新
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="exportStyle"></param>
        /// <returns></returns>
        public int Sumbit(decimal tableId, ExportStyle exportStyle)
        {
            int count = 0;

            if (tableId <= 0)
            {
                throw new ArgumentException("表格编号不能小于或是等于0。");
            }

            try
            {
                count = dalCustomTable.Sumbit(tableId, exportStyle);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
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
            if (tableId <= 0)
            {
                throw new ArgumentException("表格编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.Update(tableId, dataTable, currentState, whereCluases);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        public void Import(decimal tableId, DataTable dataTable, IDictionary<string, string> dataFieldRelation)
        {
            if (tableId <= 0)
            {
                throw new ArgumentException("表格编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.Import(tableId, dataTable, dataFieldRelation);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        public void Import(decimal tableId, DataTable dataTable, IDictionary<string, string> dataFieldRelation, bool clear)
        {
            if (tableId <= 0)
            {
                throw new ArgumentException("表格编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.Import(tableId, dataTable, dataFieldRelation, clear);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 表中是否有满足条件的记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool IsExistedRecord(decimal userId, decimal tableId)
        {
            bool exist = false;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }
            if (tableId <= 0)
            {
                throw new ArgumentException("表格编号不能小于或是等于0。");
            }

            try
            {
                exist = dalCustomTable.IsExistedRecord(userId, tableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 表中是否有满足条件的记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool IsExistedRecord(decimal userId, decimal tableId, IList<WhereConditon> whereConditons)
        {
            bool exist = false;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }
            if (tableId <= 0)
            {
                throw new ArgumentException("表格编号不能小于或是等于0。");
            }

            try
            {
                exist = dalCustomTable.IsExistedRecord(userId, tableId, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return exist;
        }

        /// <summary>
        /// 获得用户在表中的记录数
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tableId"></param>
        /// <param name="businessEnabled"></param>
        /// <param name="instanceId"></param>
        /// <returns></returns>
        public int GetRecordCount(decimal userId, decimal tableId, bool businessEnabled, decimal instanceId)
        {
            int count = 0;

            // 验证输入
            if (userId <= 0)
            {
                throw new ArgumentException("用户编号不能小于或是等于0。");
            }
            if (tableId <= 0)
            {
                throw new ArgumentException("表格编号不能小于或是等于0。");
            }

            try
            {
                count = dalCustomTable.GetRecordCount(userId, tableId, businessEnabled, instanceId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得表的类型
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 表的逻辑名称</returns>
        public byte GetTableType(decimal tableId)
        {
            byte tableType = 0;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("表格编号不能小于或是等于0。");
            }

            try
            {
                tableType = dalCustomTable.GetTableType(tableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return tableType;
        }

        /// <summary>
        /// 获得数据库下所有的表
        /// </summary>
        /// <param name="databaseId"></param>
        /// <returns></returns>
        public List<CommonNode> GetCommonNodesByDatabaseId(decimal databaseId)
        {
            List<CommonNode> commonNodes = null;

            // 验证输入
            if (databaseId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalCustomTable.GetCommonNodesByDatabaseId(databaseId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
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
            DataTable dataTable = null;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("表的编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCustomTable.GetMirrorRowData(tableId, systemLogicalDataFields, dataFieldNameRelations, businessId, onlyTarget);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
        }

        /// <summary>
        /// 审核记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="auditedState">审核状态</param>
        public void Audit(decimal tableId, decimal recordId, AuditedStatus auditedStatus)
        {
            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("表的编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.Audit(tableId, recordId, auditedStatus);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 批量审核记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordIds"></param>
        /// <param name="auditedState">审核状态</param>
        public void Audit(decimal tableId, IList<decimal> recordIds, AuditedStatus auditedStatus)
        {
            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("表的编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.Audit(tableId, recordIds, auditedStatus);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }            
        }

        /// <summary>
        /// 根据查询条件和审核状态删除表中的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <param name="auditedStatus"></param>
        public void DeleteRecords(decimal tableId, IList<WhereConditon> whereConditons, AuditedStatus auditedStatus)
        {
            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("表的编号或者用户编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.DeleteRecords(tableId, whereConditons, auditedStatus);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 根据审核状态条件删除表中的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="auditedStatus"></param>
        public void DeleteRecords(decimal tableId, AuditedStatus auditedStatus)
        {
            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("表的编号或者用户编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.DeleteRecords(tableId, auditedStatus);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        public void DeleteRecord(decimal tableId, decimal recordId)
        {
            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("表的编号或者用户编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.DeleteRecord(tableId, recordId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除表中该用户的记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="userId"></param>
        /// <param name="auditedStatus"></param>
        public void DeleteRecordsByUserId(decimal tableId, decimal userId, AuditedStatus auditedStatus)
        {
            // 验证输入
            if (tableId <= 0 || userId <= 0)
            {
                throw new ArgumentException("表的编号或者用户编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.DeleteRecordsByUserId(tableId, userId, auditedStatus);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得记录的审核状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <returns></returns>
        public AuditedStatus GetAuditedStatus(decimal tableId, decimal recordId)
        {
            AuditedStatus auditedStatus = AuditedStatus.None;

            // 验证输入
            if (tableId <= 0 || recordId <= 0)
            {
                throw new ArgumentException("表的编号或者记录编号不能小于或是等于0。");
            }

            try
            {
                auditedStatus = dalCustomTable.GetAuditedStatus(tableId, recordId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return auditedStatus;
        }

        /// <summary>
        /// 重置表
        /// </summary>
        /// <param name="tableId"></param>
        public void ResetTable(decimal tableId)
        {
            try
            {
                dalCustomTable.ResetTable(tableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 复制表
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public decimal CopyTable(decimal categoryId, decimal tableId)
        {
            decimal customTableId = decimal.MinValue;

            try
            {
                customTableId = dalCustomTable.CopyTable(categoryId, tableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return customTableId;
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataSet GetPageRecordByTableId(decimal tableId)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomTable.GetPageRecordByTableId(tableId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="categoryIds"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(IList<decimal> categoryIds)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomTable.GetPageRecord(categoryIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal categoryId)
        {
            DataSet ds = null;

            try
            {
                ds = dalCustomTable.GetPageRecord(categoryId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 重置表结构并更新表信息
        /// </summary>
        /// <param name="customTableInfo"></param>
		public void ResetAndUpdateTable(CustomTableInfo customTableInfo)
        {
            try
            {
                dalCustomTable.ResetAndUpdateTable(customTableInfo);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 获得表的当前既往状态
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public bool GetCurrentState(decimal tableId)
        {
            bool result = false;

            //// 验证输入
            //if (tableId <= 0)
            //{
            //    throw new ArgumentException("数据库表编号不能小于或是等于0。");
            //}

            //try
            //{
            //    long tableSetting = dalCustomTable.GetTableSetting(tableId);
            //    result =  AuthorityHelper.CheckAuthority(tableSetting, (byte)TableSetting.DepartmentRange);              
            //}
            //catch (Exception exception)
            //{
            //    //不记录日志, 抛出异常, 不包装异常
            //    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            //}

            return result;
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
            // 验证输入
            if (userId <= 0 || tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.MoveRecord(userId, tableId, recordId, movedDriection);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 删除记录
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordIds"></param>
        public void DeleteRecords(decimal tableId, IList<decimal> recordIds)
        {
            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dalCustomTable.DeleteRecords(tableId, recordIds);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
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
            DataTable dataTable = null;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCustomTable.GetTableData(tableId, systemLogicalDataFields, dataFieldNameRelations, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
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
            DataSet ds = null;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                ds = dalCustomTable.GetTableData(tableId, startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 查询数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldIds"></param>
        /// <param name="where"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public DataTable GetTableData(decimal tableId, IList<decimal> dataFieldIds, string where, IList<WhereConditon> whereConditons)
        {
            DataTable dataTable = null;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCustomTable.GetTableData(tableId, dataFieldIds, where, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
        }

        /// <summary>
        /// 获得表的数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="commonDataFieldInfos"></param>
        /// <returns></returns>
        public DataTable GetTableData(decimal tableId, decimal recordId, List<CommonDataFieldInfo> commonDataFieldInfos)
        {
            DataTable dataTable = null;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCustomTable.GetTableData(tableId, recordId, commonDataFieldInfos);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
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
            DataTable dataTable = null;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataTable = dalCustomTable.GetTableData(tableId, systemLogicalDataFields, hasUserAccount, hasDepartmentProperty, dataFieldNameRelations, startPosition, count, 
                    whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTable;
        }


        /// <summary>
        /// 获得表的类型
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public DataTableType GetDataTableType(decimal tableId)
        {
            DataTableType dataTableType = DataTableType.PrimaryTable;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("编号不能小于或是等于0。");
            }

            try
            {
                dataTableType = dalCustomTable.GetDataTableType(tableId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataTableType;
        }

        /// <summary>
        /// 根据表类型条件获得表节点
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="tableFilter"></param>
        /// <returns></returns>
        public IList<CommonNode> GetCommonNodes(decimal categoryId, TableFilter tableFilter)
        {
            IList<CommonNode> commonNodes = null;

            // 验证输入
            if (categoryId <= 0)
            {
                throw new ArgumentException("分类编号不能小于或是等于0。");
            }

            try
            {
                commonNodes = dalCustomTable.GetCommonNodes(categoryId, tableFilter);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得物理表名
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 物理表名</returns>
        public string GetTablePhysicalName(decimal tableId)
        {
            string physicalName = string.Empty;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("数据库表编号不能小于或是等于0。");
            }

            try
            {
                physicalName = dalCustomTable.GetTablePhysicalName(tableId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return physicalName;
        }

        /// <summary>
        /// 获得表的逻辑名称
        /// </summary>
        ///<param name="tableId">表编号</param>
        /// <returns> 表的逻辑名称</returns>
        public string GetTableLogicalName(decimal tableId)
        {
            string logicalName = string.Empty;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("数据库表编号不能小于或是等于0。");
            }

            try
            {
                logicalName = dalCustomTable.GetTableLogicalName(tableId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return logicalName;
        }

        /// <summary>
        /// 获得数据库编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public decimal GetDatabaseId(decimal tableId)
        {
            decimal databaseId = 0;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("数据库表编号不能小于或是等于0。");
            }

            try
            {
                databaseId = dalCustomTable.GetDatabaseId(tableId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return databaseId;
        }

        /// <summary>
        /// 获得数据仓库编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public byte GetDataWarehouseId(decimal tableId)
        {
            byte dataWarehouseId = 0;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("表的编号不能小于或是等于0.");
            }

            try
            {
                dataWarehouseId = dalCustomTable.GetDataWarehouseId(tableId);

            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dataWarehouseId;
        }
        #endregion

        #region 私有方法

        #endregion
    }
}
