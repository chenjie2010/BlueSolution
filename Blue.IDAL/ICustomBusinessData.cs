//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ICustomBusinessData.cs
// 描述: ICustomBusinessData 数据层访问接口
// 作者：ChenJie 
// 编写日期：2017/07/18
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;

namespace Blue.IDAL
{
    /// <summary>
    /// 自定义数据访问接口
    /// </summary>
    public interface ICustomBusinessData
    {
        /// <summary>
        /// 获得满足条件的值
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataFieldName"></param>
        /// <param name="functionName"></param>
        /// <returns></returns>
        object GetValue(string tableName, string dataFieldName, SQLServerFunction functionName);

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataTable"></param>
        void ImportData(decimal tableId, DataTable dataTable);

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        void ImportData(decimal tableId, DataTable dataTable, Dictionary<string, string> dataFieldRelation);

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        int GetTableRecordCount(decimal tableId, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableLinks"></param>
        /// <param name="whereConditons"></param>
        /// <returns></returns>
        int GetTableRecordCount(decimal tableId, IList<TableLink> tableLinks, IList<WhereConditon> whereConditons);

        /// <summary>
        /// 清除表中所有数据
        /// </summary>
        /// <param name="tableId"></param>
        /// <returns></returns>
        int TruncateTable(decimal tableId);
    }
}
