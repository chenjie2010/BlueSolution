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
    public class InstanceItem
    {
        #region 成员变量

        private List<RecordItem> _recordItems;

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
        /// 记录关键项
        /// </summary>
        public List<RecordItem> RecordItems
        {
            get
            {
                return _recordItems;
            }
            set
            {
                _recordItems = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public InstanceItem()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="instanceId"></param>
        public InstanceItem(decimal instanceId)
        {
            InstanceId = instanceId;
        }

        #endregion

    }
}
