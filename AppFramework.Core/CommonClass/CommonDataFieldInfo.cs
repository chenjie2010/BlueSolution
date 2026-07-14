//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonDataFieldInfo.cs
// 描述： 通用字段对象
// 作者：ChenJie 
// 编写日期：2018-02-26
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 通用字段对象
    /// </summary>
    [Serializable]
    public class CommonDataFieldInfo
    {
        #region 内部成员变量

        private decimal _dataFieldId;
        private decimal _tableId;
        private string _physicalName;
        private string _logicalName;
        private string _expressionText;
        private DataFieldProperty _dataFieldProperty;
        private byte _dataFieldType;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public CommonDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="tableId"></param>
        /// <param name="physicalName"></param>
        /// <param name="logicalName"></param>
        /// <param name="expressionText"></param>
        /// <param name="dataFieldProperty"></param>
        /// <param name="dataFieldType"></param>
        public CommonDataFieldInfo(decimal dataFieldId, string physicalName, string logicalName, string expressionText, DataFieldProperty dataFieldProperty, byte dataFieldType)
            : this(dataFieldId, decimal.MinValue, physicalName, logicalName, expressionText, dataFieldProperty, dataFieldType)
        {
                    
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="tableId"></param>
        /// <param name="physicalName"></param>
        /// <param name="logicalName"></param>
        /// <param name="expressionText"></param>
        /// <param name="dataFieldProperty"></param>
        /// <param name="dataFieldType"></param>
        public CommonDataFieldInfo(decimal dataFieldId, decimal tableId, string physicalName, string logicalName, string expressionText, DataFieldProperty dataFieldProperty, byte dataFieldType)
        {
            _dataFieldId = dataFieldId;
            _tableId = tableId;
            _physicalName = physicalName;
            _logicalName = logicalName;
            _expressionText = expressionText;
            _dataFieldProperty = dataFieldProperty;
            _dataFieldType = dataFieldType;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 字段编号
        /// </summary>
        public decimal DataFieldId
        {
            get
            {
                return _dataFieldId;
            }
            set
            {
                if (_dataFieldId == value)
                    return;
                _dataFieldId = value;
            }
        }

        /// <summary>
        /// 表的编号
        /// </summary>
        public decimal TableId
        {
            get
            {
                return _tableId;
            }
            set
            {
                if (_dataFieldId == value)
                    return;
                _dataFieldId = value;
            }
        }

        /// <summary>
        /// 字段物理名称
        /// </summary>
        public string PhysicalName
        {
            get
            {
                return _physicalName;
            }
            set
            {
                if (_physicalName == value)
                    return;
                _physicalName = value;
            }
        }

        /// <summary>
        /// 字段逻辑名称
        /// </summary>
        public string LogicalName
        {
            get
            {
                return _logicalName;
            }
            set
            {
                if (_logicalName == value)
                    return;
                _logicalName = value;
            }
        }

        /// <summary>
        /// 字段表达式名称
        /// </summary>
        public string ExpressionText
        {
            get
            {
                return _expressionText;
            }
            set
            {
                if (_expressionText == value)
                    return;
                _expressionText = value;
            }
        }

        /// <summary>
        /// 字段属性
        /// </summary>
        public DataFieldProperty DataFieldProperty
        {
            get
            {
                return _dataFieldProperty;
            }
            set
            {
                if (_dataFieldProperty == value)
                    return;
                _dataFieldProperty = value;
            }
        }

        /// <summary>
        /// 字段类型
        /// </summary>
        public byte DataFieldType
        {
            get
            {
                return _dataFieldType;
            }
            set
            {
                if (_dataFieldType == value)
                    return;
                _dataFieldType = value;
            }
        }

        #endregion
    }
}
