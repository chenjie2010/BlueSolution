//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名：DataBussinessService.cs
// 描述：DataBussiness 操作服务类
// 作者：ChenJie 
// 编写日期：2019/05/18
// Copyright 2019
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    public class DataBussinessService : IDataBussinessContract
    {

        #region 业务实例

        private static readonly IDataBusinessHandler dataBusinessHandler = BusinessLogicContainer.Instance.BusinessModuleContainer.Resolve<IDataBusinessHandler>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public DataBussinessService()
        {
                        
        }

        #endregion

        #region 实现自定义接口

        /// <summary>
        /// 更新当前状态查询条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="where"></param>
        /// <param name="currentState"></param>
        /// <returns></returns>
        public int UpdateCurrentState(decimal tableId, string where, CurrentState currentState)
        {
            return dataBusinessHandler.UpdateCurrentState(tableId, where, currentState);
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
            return dataBusinessHandler.GetPageRecordByTableId(tableId, startPosition, count, where, ref totalCount);
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
            return dataBusinessHandler.GetQueriedData(sourceTableId, destinationTableId, rowColCopyType,
                dataFieldRelations, customDataFieldNames, whereClause, startPosition, count, ref totalCount);
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
            return dataBusinessHandler.Update(sourceTableId, destinationTableId, dataFieldRelations, 
                customDataFieldNames, whereClause);
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
            return dataBusinessHandler.Import(sourceTableId, destinationTableId, dataFieldRelations, customDataFieldNames, whereClause);
        }

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        public int GetTableRecordCount(decimal tableId, IList<WhereConditon> whereConditons)
        {
            return dataBusinessHandler.GetTableRecordCount(tableId, whereConditons);
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
            return dataBusinessHandler.GetPageRecord(tableId, startPosition, count, whereConditons, ref totalCount);
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
            return dataBusinessHandler.GetAuthorizedData(systemTableName, startPosition, count, whereConditons, sortingCondtions, ref totalCount);
        }

        #endregion

    }
}
