//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataRowItem..cs
// 描述： 数据键值对项
// 作者：ChenJie 
// 编写日期：2018-10-24
// 版权所有 (C) 四川大学 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Linq;
using System.Text;

namespace AppFramework.Core
{
    /// <summary>
    /// 键值对项
    /// </summary>
    public class DataRowItem
    {
        #region 定义私有变量

        private decimal _key;
        private DataTable _sourceRow;
        private DataTable _tragetRow;

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
        /// 源数据行
        /// </summary>
        public DataTable SourceRow
        {
            get
            {
                return _sourceRow;
            }
            set
            {
                _sourceRow = value;
            }
        }

        /// <summary>
        /// 目标数据行
        /// </summary>
        public DataTable TragetRow
        {
            get
            {
                return _tragetRow;
            }
            set
            {
                _tragetRow = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 默认构造函数
        /// </summary>
        public DataRowItem()
        {
            _key = decimal.MinValue;
            _sourceRow = null;
            _tragetRow = null;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="key"></param>
        /// <param name="sourceRow"></param>
        /// <param name="tragetRow"></param>
        public DataRowItem(decimal key, DataTable sourceRow, DataTable tragetRow)
        {
            _key = key;
            _sourceRow = sourceRow;
            _tragetRow = tragetRow;
        }

        #endregion
        
    }
}
