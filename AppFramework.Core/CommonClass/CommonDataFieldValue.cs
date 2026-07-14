//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonDataFieldValue.cs
// 描述： 通用字段值类
// 作者：ChenJie 
// 编写日期：2018-09-01
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;

namespace AppFramework.Core
{
    /// <summary>
    /// 通用字段类
    /// </summary>
    [Serializable]
    public class CommonDataFieldValue
    {
        #region 定义私有变量

        private decimal _dataFieldId;
        private DataFieldProperty _dataFieldProperty;
        private byte _dataFieldType;        
        private object _originalDataFieldValue;
        private object _updatedDataFieldValue;
        
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
        /// 字段属性
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

        /// <summary>
        /// 数据字段的源值
        /// </summary>
        public object OriginalDataFieldValue
        {
            get
            {
                return _originalDataFieldValue;
            }
            set
            {
                _originalDataFieldValue = value;
            }
        }

        /// <summary>
        /// 数据字段更新后的值
        /// </summary>
        public object UpdatedDataFieldValue
        {
            get
            {
                return _updatedDataFieldValue;
            }
            set
            {
                _updatedDataFieldValue = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CommonDataFieldValue()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldId">字段编号</param>
        /// <param name="dataFieldProperty">字段属性</param>
        /// <param name="dataFieldType">字段类型</param>
        /// <param name="updatedDataFieldValue">更新后的的值</param>
        public CommonDataFieldValue(decimal dataFieldId, DataFieldProperty dataFieldProperty, byte dataFieldType, object updatedDataFieldValue) 
            : this(dataFieldId, dataFieldProperty, dataFieldType, null, updatedDataFieldValue)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldId">字段编号</param>
        /// <param name="dataFieldProperty">字段属性</param>
        /// <param name="dataFieldType">字段类型</param>
        /// <param name="originalDataFieldValue">源值</param>
        /// <param name="updatedDataFieldValue">更新后的的值</param>
        public CommonDataFieldValue(decimal dataFieldId, DataFieldProperty dataFieldProperty, byte dataFieldType, 
            object originalDataFieldValue, object updatedDataFieldValue)
        {
            _dataFieldId = dataFieldId;
            _dataFieldProperty = dataFieldProperty;
            _dataFieldType = dataFieldType;
            _originalDataFieldValue = originalDataFieldValue;
            _updatedDataFieldValue = updatedDataFieldValue;

        }

        #endregion

        #region 重载方法
        
        #endregion
    }
}
