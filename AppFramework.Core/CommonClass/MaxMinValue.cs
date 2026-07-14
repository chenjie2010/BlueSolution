//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： MaxMinValue.cs
// 描述： 最大最小值通用类
// 作者：ChenJie 
// 编写日期：2017-08-29
// 版权所有 (C) 四川大学 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 最大最小值通用类
    /// </summary>
    public class MaxMinValue
    {
        #region 定义私有变量

        private int _max;
        private int _min;

        #endregion

        #region 属性

        /// <summary>
        /// 最大值
        /// </summary>
        public int Max
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public int Min
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public MaxMinValue()
        {
            _max = 0;
            _min = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="max"></param>
        /// <param name="min"></param>
        public MaxMinValue(int max, int min)
        {
            _max = max;
            _min = min;
        }

        #endregion
    }
}
