//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： PointSize.cs
// 描述： 通用类型：宽度与高度
// 作者：ChenJie 
// 编写日期：2018-12-19
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
    /// 最大最小值通用类
    /// </summary>
    public class PointSize
    {
        #region 定义私有变量

        private int _width;
        private int _height;

        #endregion

        #region 属性

        /// <summary>
        /// 宽度
        /// </summary>
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
            }
        }

        /// <summary>
        /// 高度
        /// </summary>
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public PointSize()
        {
            _width = 0;
            _height = 0;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public PointSize(int width, int height)
        {
            _width = width;
            _height = height;
        }

        #endregion
    }
}
