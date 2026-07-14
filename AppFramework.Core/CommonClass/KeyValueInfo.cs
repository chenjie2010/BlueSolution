//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： KeyValueInfo.cs
// 描述： 键值对
// 作者：ChenJie 
// 编写日期：2017/09/19
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.Core
{
    /// <summary>
    /// 键值对
    /// </summary>
    public class KeyValueInfo
    {
        #region 定义私有变量

        private decimal _key;
        private string _value;

        #endregion

        #region 属性

        /// <summary>
        /// 键
        /// </summary>
        public decimal Key
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

        /// <summary>
        /// 值
        /// </summary>
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public KeyValueInfo()
        {
            _key = 0;
            _value = string.Empty;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public KeyValueInfo(decimal key, string value)
        {
            _key = key;
            _value = value;
        }

        #endregion
    }
    }
