//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CommonAccessHelper.cs
// 描述: 数据库通用操作类
// 作者：ChenJie 
// 编写日期：2016-08-28
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Core;
using AppFramework.Reference.DataAccessLibrary;

namespace Blue.SQLServerDAL
{
    /// <summary>
    /// 数据库通用操作类
    /// </summary>
    public sealed class CommonAccessHelper
    {
        /// <summary>
        /// 更新编号
        /// </summary>
        /// <param name="correlatedTable"></param>
        /// <param name="mainId"></param>
        /// <param name="currentIds"></param>
        /// <param name="previousIds"></param>
        /// <param name="db"></param>
        /// <param name="transaction"></param>
        public static void Update(ICorrelatedTable correlatedTable, decimal mainId, IList<decimal> currentIds, IList<decimal> previousIds, SqlDatabase db, DbTransaction transaction)
        {
            if (currentIds == null)
            {
                return;
            }
            /* 1 对于被取消的, 则删除 */
            foreach (decimal previousId in previousIds)
            {
                if (!currentIds.Contains(previousId))
                {
                    correlatedTable.Delete(mainId, previousId, db, transaction);
                }
            }

            /* 3.2 对于不存在的，则新增加 */
            foreach (decimal currentId in currentIds)
            {
                if (!previousIds.Contains(currentId))
                {
                    correlatedTable.Insert(new CorrelatedModel(mainId, currentId), db, transaction);
                }
            }
        }
    }
}
