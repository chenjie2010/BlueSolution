//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： BasedDataFieldInfo.cs
// 描述： 字段的基本属性类
// 作者：ChenJie 
// 编写日期：2018-07-25
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
    public class BasedDataFieldInfo
    {        
        #region 内部成员变量

        private decimal _dataFieldId;
        private string _physicalName;
        private string _logicalName;
        private byte _dataFieldCategory;
        private BasedDataType _dataFieldType;

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认的构造函数
        /// </summary>
        public BasedDataFieldInfo()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="logicalName"></param>
        /// <param name="physicalName"></param>
        /// <param name="dataFieldType"></param>
        /// <param name="dataFieldCategory"></param>
        public BasedDataFieldInfo(decimal dataFieldId, string logicalName, string physicalName, 
            BasedDataType dataFieldType, byte dataFieldCategory)
        {
            _dataFieldId = dataFieldId;
            _logicalName = logicalName;
            _physicalName = physicalName;            
            _dataFieldType = dataFieldType;
            _dataFieldCategory = dataFieldCategory;
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
        /// 字段类型
        /// </summary>
        public BasedDataType DataFieldType
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
        /// 字段分类
        /// </summary>
        public byte DataFieldCategory
        {
            get
            {
                return _dataFieldCategory;
            }
            set
            {
                if (_dataFieldCategory == value)
                    return;
                _dataFieldCategory = value;
            }
        }

        #endregion
    }
}
