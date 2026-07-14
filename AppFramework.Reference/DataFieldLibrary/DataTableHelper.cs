//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataTableHelper.cs.cs
// 描述: 表帮助类
// 作者：ChenJie 
// 编写日期：2018/01/10
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;

namespace AppFramework.Reference.DataFieldLibrary
{
    /// <summary>
    /// 表帮助类
    /// </summary>
    public sealed class DataTableHelper
    {
        #region 公有静态方法
        
        /// <summary>
        /// 根据表类型条件获得表条件
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public static IList<WhereConditon> GetWhereConditons(decimal categoryId, TableFilter tableFilter)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            whereConditons.Add(new WhereConditon("CategoryId", "CategoryId", System.Data.DbType.Decimal, categoryId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            switch (tableFilter)
            {
                case TableFilter.System:                
                    whereConditons.Add(new WhereConditon("SystemTable", "SystemTable", System.Data.DbType.Boolean,
                        true, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    break;

                case TableFilter.PrimaryTable:
                    whereConditons.Add(new WhereConditon("TableType", "TableType", System.Data.DbType.Byte,
                        (byte)DataTableType.PrimaryTable, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    break;

                case TableFilter.MasterSlaveTable:
                    whereConditons.Add(new WhereConditon("TableType", "TableType_0", System.Data.DbType.Byte,
                        (byte)DataTableType.PrimaryTable, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("TableType", "TableType_1", System.Data.DbType.Byte,
                        (byte)DataTableType.MasterSlaveTable, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

            }

            return whereConditons;
        }

        /// <summary>
        /// 合并相同结构的表
        /// </summary>
        /// <param name="dtFirst"></param>
        /// <param name="dtSecond"></param>
        /// <returns></returns>
        public static DataTable MergeDataTable(DataTable dtFirst, DataTable dtSecond)
        {
            DataTable dataTable = dtFirst.Copy();
            foreach (DataRow dr in dtSecond.Rows)
            {
                dataTable.ImportRow(dr);
            }

            return dataTable;
        }

        /// <summary>
        /// 合并结构不相同的表
        /// </summary>
        /// <param name="dtFirst"></param>
        /// <param name="dataTables"></param>
        /// <returns></returns>
        public static DataTable CombineDataTable(DataTable dtFirst, IList<DataTable> dataTables)
        {
            if (dataTables == null || dataTables.Count == 0)
            {
                return dtFirst.Copy();
            }

            DataTable dataTable = dtFirst.Clone();
            object[] objArray = new object[dataTable.Columns.Count];
            foreach (DataRow dr in dtFirst.Rows)
            {
                dr.ItemArray.CopyTo(objArray, 0);
                dataTable.Rows.Add(objArray);
            }
            foreach (var table in dataTables)
            {
                foreach (DataRow dr in table.Rows)
                {
                    dr.ItemArray.CopyTo(objArray, 0);
                    dataTable.Rows.Add(objArray);                    
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 合并结构不相同的表
        /// </summary>
        /// <param name="dtFirst"></param>
        /// <param name="dtSecond"></param>
        /// <returns></returns>
        public static DataTable CombineDataTable(DataTable dtFirst, DataTable dtSecond)
        {
            DataTable dataTable = dtFirst.Clone();               
            object[] objArray = new object[dataTable.Columns.Count];
            foreach (DataRow dr in dtFirst.Rows)
            {
               dr.ItemArray.CopyTo(objArray, 0);
                dataTable.Rows.Add(objArray);
            }
            foreach (DataRow dr in dtSecond.Rows)
            {
                dr.ItemArray.CopyTo(objArray, 0);
                dataTable.Rows.Add(objArray);
            }
            
            return dataTable;
        }

        #endregion

        #region 私有方法       


        #endregion
    }
}
