//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataValidationList..cs
// 描述： 数据校验结果列表类
// 作者：ChenJie 
// 编写日期：2018/07/19
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace AppFramework.Core
{
    /// <summary>
    /// 数据校验结果列表类
    /// </summary>
    public class DataValidationList<T>
    {
        #region 定义私有变量
        
        private IList<DataValidationResult> _dataValidationResults;
        private T _key;

        #endregion

        #region 属性

        /// <summary>
        /// 验证单元格结果列表
        /// </summary>
        public IList<DataValidationResult> DataValidationResults
        {
            get
            {
                return _dataValidationResults;
            }
            set
            {
                _dataValidationResults = value;
            }
        }

        /// <summary>
        /// 关键字
        /// </summary>
        public T Key
        {
            get
            {
                return _key;
            }
            set
            {
                _key = value;
            }
        }
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DataValidationList()
        {
            _dataValidationResults = new List<DataValidationResult>();
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="userToolError"></param>
        public DataValidationList(T key, IList<DataValidationResult> dataValidationResults)
        {
            _key = key;
            _dataValidationResults = dataValidationResults;
        }

        #endregion

        #region 重载方法

        /// <summary>
        /// 获得名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _key.ToString();
        }

        #endregion
    }
}
