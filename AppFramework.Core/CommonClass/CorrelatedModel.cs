//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonNode.cs
// 描述： CommonNode 操作服务类
// 作者：ChenJie 
// 编写日期：2016/07/16
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;

namespace AppFramework.Core
{
    /// <summary>
    /// 关联实体
    /// </summary>
    [Serializable]
    public class CorrelatedModel
    {
        #region 定义私有变量

        private decimal _firstForeignKey;
        private decimal _secondForeignKey;
        private byte _rangeValue;

        #endregion

        #region 属性

        /// <summary>
        /// 外键一
        /// </summary>
        public decimal ForeignKey
        {
            get
            {
                return _firstForeignKey;
            }
            set
            {
                _firstForeignKey = value;
            }
        }

        /// <summary>
        /// 外键二
        /// </summary>
        public decimal OtherForeignKey
        {
            get
            {
                return _secondForeignKey;
            }
            set
            {
                _secondForeignKey = value;
            }
        }

        /// <summary>
        /// 范围
        /// </summary>
        public byte RangeValue
        {
            get
            {
                return _rangeValue;
            }
            set
            {
                _rangeValue = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CorrelatedModel()
        {
            _firstForeignKey = 0;
            _secondForeignKey = 0;
            _rangeValue = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="firstForeignKeyName"></param>
        /// <param name="secondForeignKey"></param>
        public CorrelatedModel(decimal firstForeignKeyName, decimal secondForeignKey) : this (firstForeignKeyName, secondForeignKey, 0)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="firstForeignKeyName"></param>
        /// <param name="secondForeignKey"></param>
        /// <param name="rangeValue"></param>
        public CorrelatedModel(decimal firstForeignKeyName, decimal secondForeignKey, byte rangeValue)
        {
            _firstForeignKey = firstForeignKeyName;
            _secondForeignKey = secondForeignKey;
            _rangeValue = rangeValue;
        }

        #endregion
    }
}
