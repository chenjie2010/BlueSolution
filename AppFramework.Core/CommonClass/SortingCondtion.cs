//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： SortingCondtion.cs
// 描述： SortingCondtion 排序条件类
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 排序条件
    /// </summary>
    [Serializable]
    public class SortingCondtion
    {
        #region 定义成员变量

        private string _dataTableName;
        private string _dataFieldName;
        private CustomSorting _customSorting;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldName">排序字段</param>
        /// <param name="customSorting">排序类型</param>
        public SortingCondtion(string dataFieldName, CustomSorting customSorting)
        {
            _dataFieldName = dataFieldName;
            _customSorting = customSorting;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataTableName">表名</param>
        /// <param name="dataFieldName">排序字段</param>
        /// <param name="customSorting"></param>
        public SortingCondtion(string dataTableName, string dataFieldName, CustomSorting customSorting)
            : this(dataFieldName, customSorting)
        {
            _dataTableName = dataTableName;
        }

        #endregion         

        #region 属性

        /// <summary>
        /// 表的名称
        /// </summary>
        public string DataTableName
        {
            get
            {
                return _dataTableName;
            }
            set
            {
                if (_dataTableName == value)
                    return;
                _dataTableName = value;
            }
        }


        /// <summary>
        /// 排序字段
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
        /// 排序方式
        /// </summary>
        public CustomSorting CustomSorting
        {
            get
            {
                return _customSorting;
            }
            set
            {
                if (_customSorting == value)
                    return;
                _customSorting = value;
            }
        }

        #endregion

    }
}
