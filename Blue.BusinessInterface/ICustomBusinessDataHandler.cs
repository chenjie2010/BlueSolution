using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.BusinessInterface
{
    public interface ICustomBusinessDataHandler
    {
        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="tableId">表的编号</param>
        /// <param name="dataTable">数据表</param>
        /// <param name="dataFieldRelation"></param>
        void ImportData(decimal tableId, DataTable dataTable, Dictionary<string, string> dataFieldRelation);
    }
}
