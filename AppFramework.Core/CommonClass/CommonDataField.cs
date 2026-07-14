//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonDataField.cs
// 描述： 通用字段类
// 作者：ChenJie 
// 编写日期：2016-07-17
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;

namespace AppFramework.Core
{
    /// <summary>
    /// 通用字段类
    /// </summary>
    [Serializable]
    public class CommonDataField
    {
        #region 定义私有变量

        private decimal _dataFieldId;
        private string _dataFieldName;
        private string _dataFieldParameterName;
        private object _dataFieldValue;
        private DbType _dataFieldDataType;

        #endregion

        #region 属性

        /// <summary>
        /// dataFieldId
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
        /// 数据字段的名称
        /// </summary>
        public string DataFieldName
        {
            get
            {
                return _dataFieldName;
            }
            set
            {
                if (_dataFieldName == value)
                    return;
                _dataFieldName = value;
            }
        }

        /// <summary>
        /// 字段参数名称
        /// </summary>
        public string DataFieldParameterName
        {
            get
            {
                return _dataFieldParameterName;
            }
            set
            {
                if (_dataFieldParameterName == value)
                    return;
                _dataFieldParameterName = value;
            }
        }

        /// <summary>
        /// 数据字段的值
        /// </summary>
        public object DataFieldValue
        {
            get
            {
                return _dataFieldValue;
            }
            set
            {
                _dataFieldValue = value;
            }
        }

        /// <summary>
        /// 数据字段的类型
        /// </summary>
        public DbType DataFieldDataType
        {
            get
            {
                return _dataFieldDataType;
            }
            set
            {
                if (_dataFieldDataType == value)
                    return;
                _dataFieldDataType = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldName">数据字段的名称</param>
        /// <param name="dataFieldValue">数据字段的值</param>
        /// <param name="dataFieldDataType">数据字段的类型</param>
        public CommonDataField(string dataFieldName, object dataFieldValue, DbType dataFieldDataType)
            : this(decimal.MinValue, dataFieldName, dataFieldName, dataFieldValue, dataFieldDataType)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldName">数据字段的名称</param>
        /// <param name="dataFieldValue">数据字段的值</param>
        /// <param name="dataFieldDataType">数据字段的类型</param>
        public CommonDataField(decimal dataFieldId, string dataFieldName, object dataFieldValue, DbType dataFieldDataType)
            : this(dataFieldId, dataFieldName, dataFieldName, dataFieldValue, dataFieldDataType)
        {

        }        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldName">数据字段的名称</param>
        /// <param name="dataFieldParameterName">数据字段的参数名称</param>
        /// <param name="dataFieldValue">数据字段的值</param>
        /// <param name="dataFieldDataType">数据字段的类型</param>
        public CommonDataField(string dataFieldName, string dataFieldParameterName, object dataFieldValue, DbType dataFieldDataType)
             : this(decimal.MinValue, dataFieldName, dataFieldName, dataFieldValue, dataFieldDataType)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldId">数据字段的ID</param>
        /// <param name="dataFieldName">数据字段的名称</param>
        /// <param name="dataFieldParameterName">数据字段的参数名称</param>
        /// <param name="dataFieldValue">数据字段的值</param>
        /// <param name="dataFieldDataType">数据字段的类型</param>
        public CommonDataField(decimal dataFieldId, string dataFieldName, string dataFieldParameterName,
            object dataFieldValue, DbType dataFieldDataType)
        {
            _dataFieldId = dataFieldId;
            _dataFieldName = dataFieldName;
            _dataFieldParameterName = dataFieldParameterName;
            _dataFieldValue = dataFieldValue;
            _dataFieldDataType = dataFieldDataType;

        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 获得名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _dataFieldName;
        }

        #endregion

    }
}
