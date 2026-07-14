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
    public class RecordSet
    {
        #region 成员变量

        private IList<RecordEntity> _recordEntities;

        #endregion

        #region 属性

        /// <summary>
        /// 实体校验结果是否成功
        /// </summary>
        public bool Success
        {
            get;
            set;
        }

        /// <summary>
        /// 校验失败的描述
        /// </summary>
        public string Warning
        {
            get;
            set;
        }

        /// <summary>
        /// 字段集合
        /// </summary>
        public IList<RecordEntity> RecordEntities
        {
            get
            {
                return _recordEntities;
            }            
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public RecordSet() : this (true)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="success"></param>
        public RecordSet(bool success)
        {
            _recordEntities = new List<RecordEntity>();
            Success = true;
            Warning = string.Empty;
        }

        #endregion

    }
}
