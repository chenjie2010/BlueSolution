//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CellValidationResults.cs
// 描述： 单元格校验结果列表类
// 作者：ChenJie 
// 编写日期：2018/07/13
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace AppFramework.Core
{
    /// <summary>
    /// 单元格校验结果类
    /// </summary>
    public class CellValidationResults
    {
        #region 定义私有变量

        private int _index;
        private IList<CellValidationResult> _results;

        #endregion

        #region 属性

        /// <summary>
        /// 索引
        /// </summary>
        public int Index
        {
            get
            {
                return _index;
            }
            set
            {
                _index = value;
            }
        }

        /// <summary>
        /// 验证单元格结果列表
        /// </summary>
        public IList<CellValidationResult> Results
        {
            get
            {
                return _results;
            }
            set
            {
                _results = value;
            }
        }        
        
        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CellValidationResults()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="index"></param>
        /// <param name="results"></param>
        public CellValidationResults(int index, IList<CellValidationResult> results)
        {
            _index = index;
           _results = results;
        }
        
        #endregion

        #region 重载方法

        /// <summary>
        /// 获得名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Convert.ToString(_index);
        }

        #endregion
    }
}
