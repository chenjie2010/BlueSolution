using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    /// 业务数据处理类
    /// </summary>
    public class DataBusinessHandler : IDataBusinessHandler
    {
        #region 工厂类实例

        private static readonly IDataBussiness dalDataBussiness = BusinessDataAccessFactory.CreateDataBussiness();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataBusinessHandler()
        {
        }

        #endregion

        #region 自定义方法

        /// <summary>
        /// 根据表的编号查看未填数据的用户
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="auditedStatus"></param>
        /// <param name="currentState"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetUsersWithoutData(decimal tableId, int startPosition, int count, IList<WhereConditon> whereConditons, byte auditedStatus, byte currentState, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                ds = dalDataBussiness.GetUsersWithoutData(tableId, startPosition, count, whereConditons, auditedStatus, currentState, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 根据表的编号查看未填数据的用户
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetUsersWithoutData(decimal tableId, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;
            
            try
            {
                ds = dalDataBussiness.GetUsersWithoutData(tableId, startPosition, count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 更新当前状态查询条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="where"></param>
        /// <param name="currentState"></param>
        /// <returns></returns>
        public int UpdateCurrentState(decimal tableId, string where, CurrentState currentState)
        {
            int count = 0;

            try
            {
                count = dalDataBussiness.UpdateCurrentState(tableId, where, currentState);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得表 CustomTable 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="tableId">表的编号</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="where">查询字段条件</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecordByTableId(decimal tableId, int startPosition, int count, string where, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                ds = dalDataBussiness.GetPageRecordByTableId(tableId, startPosition, count, where, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得用户编号
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="auditedState">审核状态</param>
        public decimal GetUserId(decimal tableId, decimal recordId)
        {
            decimal userId = 0;

            // 验证输入
            if (tableId <= 0)
            {
                throw new ArgumentException("数据表编号不能小于或者等于0。");
            }
            if (recordId <= 0)
            {
                throw new ArgumentException("记录编号不能小于或者等于0。");
            }

            try
            {
                userId = dalDataBussiness.GetUserId(tableId, recordId);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return userId;
        }

        /// <summary>
        /// 针对行复制或是列替换来查询数据
        /// </summary>
        /// <param name="sourceTableId">源数据表编号</param>
        /// <param name="destinationTableId">目的数据表编号</param>
        /// <param name="rowColCopyType">复制类型</param>
        /// <param name="dataFieldRelations">字段关系</param>
        /// <param name="customDataFieldNames">自定义字段关系</param>
        /// <param name="whereClause">查询条件</param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns>获得查询的记录</returns>
        public DataSet GetQueriedData(decimal sourceTableId, decimal destinationTableId, RowColCopyType rowColCopyType, IDictionary<decimal, decimal> dataFieldRelations,
            IDictionary<decimal, string> customDataFieldNames, string whereClause, int startPosition, int count, ref int totalCount)
        {
            DataSet ds = null;

            // 验证输入
            if (sourceTableId <= 0 || destinationTableId <= 0)
            {
                throw new ArgumentException("源数据表编号或者目的数据表编号不能小于或者等于0。");
            }

            try
            {
                ds = dalDataBussiness.GetQueriedData(sourceTableId, destinationTableId, rowColCopyType, dataFieldRelations, customDataFieldNames,
                    whereClause, startPosition, count, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 字段数据替换
        /// </summary>
        /// <param name="sourceTableId">源数据表编号</param>
        /// <param name="destinationTableId">目的数据表编号</param>
        /// <param name="dataFieldRelations">字段关系</param>
        /// <param name="customDataFieldNames">自定义字段关系，key是源字段，value是目的字段</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns>被替换的记录数目</returns>
        public int Update(decimal sourceTableId, decimal destinationTableId, IDictionary<decimal, decimal> dataFieldRelations,
            IDictionary<decimal, string> customDataFieldNames, string whereClause)
        {
            int count = 0;

            // 验证输入
            if (sourceTableId <= 0 || destinationTableId <= 0)
            {
                throw new ArgumentException("源数据表编号或者目的数据表编号不能小于或者等于0。");
            }

            try
            {
                count = dalDataBussiness.Update(sourceTableId, destinationTableId, dataFieldRelations,
                    customDataFieldNames, whereClause);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 批量复制数据
        /// </summary>
        /// <param name="sourceTableId">源数据表编号</param>
        /// <param name="destinationTableId">目的数据表编号</param>
        /// <param name="dataFieldRelations">字段关系</param>
        /// <param name="customDataFieldNames">自定义字段关系</param>
        /// <param name="whereClause">查询条件</param>
        /// <returns>导入的记录数</returns>
        public int Import(decimal sourceTableId, decimal destinationTableId, IDictionary<decimal, decimal> dataFieldRelations,
            IDictionary<decimal, string> customDataFieldNames, string whereClause)
        {
            int count = 0;

            // 验证输入
            if (sourceTableId <= 0 || destinationTableId <= 0)
            {
                throw new ArgumentException("源数据表编号或者目的数据表编号不能小于或者等于0。");
            }

            try
            {
                count = dalDataBussiness.Import(sourceTableId, destinationTableId, dataFieldRelations, 
                    customDataFieldNames, whereClause);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetTableRecordCount(decimal tableId, IList<WhereConditon> whereConditons)
        {
            int count = 0;

            try
            {
                count = dalDataBussiness.GetTableRecordCount(tableId, whereConditons);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获得表 CustomTable 的分页数据集(只能以主键为排序字段)
        /// 必须要求主键，且此主键的类型必须是数字类型
        /// </summary>
        /// <param name="tableId">表的编号</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>        
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="totalCount">总记录数</param>        
        /// <returns>数据集</returns>
        public DataSet GetPageRecord(decimal tableId, int startPosition, int count, IList<WhereConditon> whereConditons, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                ds = dalDataBussiness.GetPageRecord(tableId, startPosition, count, whereConditons, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得表 CustomTable 的分页数据集
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="extendedCustomDataFieldInfos"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="whereConditons"></param>
        /// <param name="sortingCondtions"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(decimal tableId, IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos, int startPosition, int count, IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions)
        {
            DataSet ds = null;

            try
            {
                ds = dalDataBussiness.GetPageRecord(tableId, extendedCustomDataFieldInfos, startPosition, count, whereConditons, sortingCondtions);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        /// <summary>
        /// 获得系统表分页数据集
        /// </summary>
        /// <param name="systemTableName">系统表</param>        
        /// <param name="startPosition">开始位置</param>
        /// <param name="count">获取的数目</param>
        /// <param name="whereConditons">查询字段条件的集合</param>
        /// <param name="sortingCondtions">排序字段的集合</param>
        /// <param name="totalCount">总记录数</param>
        /// <returns>数据集</returns>
        public DataSet GetAuthorizedData(SystemTable systemTableName, int startPosition, int count,
            IList<WhereConditon> whereConditons, IList<SortingCondtion> sortingCondtions, ref int totalCount)
        {
            DataSet ds = null;

            try
            {
                ds = dalDataBussiness.GetAuthorizedData(systemTableName, startPosition, count, whereConditons, sortingCondtions, ref totalCount);
            }
            catch (Exception exception)
            {
                //不记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        #endregion
    }
}
