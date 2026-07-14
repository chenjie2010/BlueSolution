//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MaxMinValue.cs
// 描述： 记录实体类
// 作者：ChenJie 
// 编写日期：2018-02-28
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;

namespace AppFramework.Core
{
    /// <summary>
    /// 记录实体类
    /// </summary>
    [Serializable]
    public class RecordEntity
    {
        #region 私有变量
        
        #endregion

        #region 成员变量

        private bool _businessEnabled = false;
        private List<CommonDataField> _commonDataFields;
        private Dictionary<AdditionalRecordType, IList<CommonDataFieldValue>> _additionalData;
        private List<CommonDataField> _relaitonDataFields;

        #endregion

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
        /// 表的类型
        /// </summary>
        public DataTableType TableType
        {
            set;
            get;
        }
        /// <summary>
        /// 实例编号
        /// </summary>
        public decimal InstanceId
        {
            get;
            set;
        }

        /// <summary>
        /// 备份的业务编号
        /// </summary>
        public decimal BusinessAlternativeId
        {
            get;
            set;
        }

        /// <summary>
        /// 记录的编号
        /// </summary>
        public decimal RecordId
        {
            get;
            set;
        }

        /// <summary>
        /// 当前既往状态
        /// </summary>
        public CurrentState CurrentState
        {
            get;
            set;
        }

        /// <summary>
        /// 是否启用业务
        /// </summary>
        public bool BusinessEnabled
        {
            get
            {
                return _businessEnabled;
            }
            set
            {
                _businessEnabled = value;
            }
        }

        /// <summary>
        /// 附加数据
        /// </summary>
        public Dictionary<AdditionalRecordType, IList<CommonDataFieldValue>> AdditionalData
        {
            get
            {
                return _additionalData;
            }
        }

        /// <summary>
        /// 字段集合
        /// </summary>
        public List<CommonDataField> CommonDataFields
        {
            get
            {
                return _commonDataFields;
            }
        }

        /// <summary>
        /// 更新其他表的字段集合：联动更新字段、关联字段
        /// </summary>
        public List<CommonDataField> RelaitonDataFields
        {
            get
            {
                return _relaitonDataFields;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RecordEntity() : this(decimal.MinValue, DataTableType.PrimaryTable, decimal.MinValue, decimal.MinValue, false, CurrentState.Current)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableType"></param>
        public RecordEntity(decimal tableId, DataTableType tableType) : this(tableId, tableType, decimal.MinValue, decimal.MinValue, false, CurrentState.Current)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableType"></param>
        /// <param name="recordId"></param>
        public RecordEntity(decimal tableId, DataTableType tableType, decimal recordId) : this(tableId, tableType, recordId, decimal.MinValue, false, CurrentState.Current)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableType"></param>
        /// <param name="recordId"></param>
        /// <param name="currentState"></param>
        public RecordEntity(decimal tableId, DataTableType tableType, decimal recordId, CurrentState currentState)
            : this(tableId, tableType, recordId, decimal.MinValue, false, currentState)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableType"></param>
        /// <param name="recordId"></param>
        /// <param name="instanceId"></param>
        /// <param name="currentState"></param>
        public RecordEntity(decimal tableId, DataTableType tableType, decimal recordId, decimal instanceId, CurrentState currentState)
            : this(tableId, tableType, recordId, instanceId, true, currentState)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableType"></param>
        /// <param name="recordId"></param>
        /// <param name="instanceId"></param>
        /// <param name="dataBusinessEnabled"></param>
        public RecordEntity(decimal tableId, DataTableType tableType, decimal recordId, decimal instanceId, bool dataBusinessEnabled) :
            this(tableId, tableType, recordId, decimal.MinValue, dataBusinessEnabled, CurrentState.Current)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableType"></param>
        /// <param name="recordId"></param>
        /// <param name="instanceId"></param>
        /// <param name="dataBusinessEnabled"></param>
        /// <param name="currentState"></param>
        private RecordEntity(decimal tableId, DataTableType tableType, decimal recordId, decimal instanceId, bool dataBusinessEnabled, CurrentState currentState)
        {
            TableId = tableId;
            TableType = tableType; ;
            RecordId = recordId;
            InstanceId = instanceId;
            _businessEnabled = dataBusinessEnabled;
            CurrentState = currentState;
            _commonDataFields = new List<CommonDataField>();
            _relaitonDataFields = new List<CommonDataField>();
            _additionalData = new Dictionary<AdditionalRecordType, IList<CommonDataFieldValue>>();
            BusinessAlternativeId = 0;
        }

        #endregion

    }
}
