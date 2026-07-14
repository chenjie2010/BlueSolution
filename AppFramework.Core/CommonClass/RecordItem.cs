using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 记录关键项
    /// </summary>
    [Serializable]
    public class RecordItem
    {
        #region 属性

        /// <summary>
        /// 表的编号
        /// </summary>
        public decimal TableId
        {
            get;
            set;
        }

        /// <summary>
        /// 记录编号
        /// </summary>
        public decimal RecordId
        {
            get;
            set;
        }

        /// <summary>
        /// 字段的当前状态
        /// </summary>
        public CurrentState CurrentState
        {
            get;
            set;
        }

        #endregion


        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="recordId"></param>
        /// <param name="currentState"></param>
        public RecordItem(decimal tableId, decimal recordId, CurrentState currentState)
        {
            TableId = tableId;
            RecordId = recordId;
            CurrentState = currentState;
        }

        #endregion

    }
}
