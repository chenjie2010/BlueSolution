//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DefaultItem.cs
// 描述： 默认值项类
// 作者：ChenJie 
// 编写日期：2016/09/26
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 默认值项
    /// </summary>
    /// <typeparam name="T"></typeparam>

    [Serializable]
    public class DefaultItem<T>
    {
        #region 属性

        /// <summary>
        /// 默认值
        /// </summary>
        public T DefaultValue
        {
            get;
            set;
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public T MaxValue
        {
            get;
            set;
        }

        /// <summary>
        /// 最大值包含类型
        /// </summary>
        public bool MaxDataRangeBoundaryType
        {
            get;
            set;
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public T MinValue
        {
            get;
            set;
        }

        /// <summary>
        /// 最小值包含类型
        /// </summary>
        public bool MinDataRangeBoundaryType
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DefaultItem()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defaultValue"></param>
        public DefaultItem(T defaultValue)
        {
            DefaultValue = defaultValue;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="defaultValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="maxDataRangeBoundaryType"></param>
        /// <param name="minValue"></param>
        /// <param name="minDataRangeBoundaryType"></param>
        public DefaultItem(T defaultValue, T maxValue, bool maxDataRangeBoundaryType, T minValue, bool minDataRangeBoundaryType)
        {
            DefaultValue = defaultValue;
            MaxValue = maxValue;
            MaxDataRangeBoundaryType = maxDataRangeBoundaryType;
            MinValue = minValue;
            MinDataRangeBoundaryType = minDataRangeBoundaryType;
        }

        #endregion
    }
}
