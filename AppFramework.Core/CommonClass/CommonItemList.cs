//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： CommonItemList.cs
// 描述： 通用项集合
// 作者：ChenJie 
// 编写日期：2018/03/02
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace AppFramework.Core
{
    /// <summary>
    /// 通用项集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="K"></typeparam>
    [Serializable]
    public class CommonItemList<T, K>
    {
        #region 定义私有变量

        private T _key;
        private string _text;
        private IList<K> _commonList = new List<K>();

        #endregion

        #region 属性

        /// <summary>
        /// 值
        /// </summary>
        public T Value
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
        /// 文本
        /// </summary>
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }

        /// <summary>
        /// 集合
        /// </summary>
        public IList<K> CommonList
        {
            get
            {
                return _commonList;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public CommonItemList()
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="text"></param>
        public CommonItemList(T key, string text)
        {
            _key = key;
            _text = text;
        }

        #endregion

        #region 重载方法
        /// <summary>
        /// 获得名称
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _text;
        }
        #endregion
    }
}
