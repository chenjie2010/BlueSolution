using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 业务实例返回对象
    /// </summary>
    [Serializable]
    public class InstanceSet
    {
        #region 成员变量

        private Dictionary<decimal, List<RecordItem>> _dicRecordItems;

        #endregion

        #region 属性

        /// <summary>
        /// 实例编号
        /// </summary>
        public decimal InstanceId
        {
            get;
            set;
        }

        /// <summary>
        /// 表单编号与表的记录关键项
        /// </summary>
        public Dictionary<decimal, List<RecordItem>> RecordItems
        {
            get
            {
                return _dicRecordItems;
            }
            set
            {
                _dicRecordItems = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InstanceSet()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="instanceId"></param>
        public InstanceSet(decimal instanceId)
        {
            InstanceId = instanceId;
        }

        #endregion

    }
}
