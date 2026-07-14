//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: QueryFieldCollection.cs
// 描述: QueryFieldCollection 查询集合类
// 作者：ChenJie 
// 编写日期：2011-8-5
// Copyright 2011
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections;

namespace AppFramework.Core
{
    /// <summary>
    /// 查询集合类
    /// </summary>
    [Serializable]
    public class QueryFieldCollection : BindingArrayList
    {
        #region 定义构造函数
        /// <summary>
        /// 定义构造函数
        /// </summary>
        internal QueryFieldCollection()
            : base("Query Fields", typeof(QueryField))
        {
            AllowRemove = true;
            AllowSort = false;
        }
        #endregion

        #region 重载方法

        /// <summary>
        /// 索引
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public new QueryField this[int index]
        {
            get
            {
                return (base[index] as QueryField);
            }
        }
        

        /// <summary>
        /// 是否包含该对象
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(QueryField value)
        {
            bool contain = false;

            foreach (QueryField queryField in this)
            {
                if (queryField.DataFieldId == value.DataFieldId && queryField.DataFieldProperty == value.DataFieldProperty
                    && queryField.DataTableName.Equals(value.DataTableName))
                {
                    contain = true;
                    break;
                }
            }

            return contain;
        }

        /// <summary>
        /// 是否存在相同的别名
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool ExistAlias(QueryField value)
        {
            bool contain = false;
            foreach (QueryField queryField in this)
            {
                if (queryField.Alias.Equals(value.Alias))
                {
                    contain = true;
                    break;
                }
            }
            return contain;
        }

        /// <summary>
        /// 查找对象的位置
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int IndexOf(QueryField value)
        {
            bool contain = false;
            int index = -1;
            foreach (QueryField queryField in this)
            {
                index++;
                if (queryField.DataFieldId == value.DataFieldId && queryField.DataFieldProperty == value.DataFieldProperty
                    && queryField.DataTableName.Equals(value.DataTableName))
                {
                    contain = true;
                    break;
                }
            }
            if (!contain)
            {
                index = -1;
            }
            return index;
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(QueryField obj)
        {
            int index = IndexOf(obj);
            if (index > -1)
            {
                base.RemoveAt(index);
            }
        }

        /// <summary>
        /// 移除对象
        /// </summary>
        /// <param name="dataFieldId"></param>
        public void Remove(decimal dataFieldId)
        {
            int index = -1;
            foreach (QueryField queryField in this)
            {
                index++;
                if (queryField.DataFieldId == dataFieldId)
                {                    
                    break;
                }
            }
            if (index > -1)
            {
                base.RemoveAt(index);
            }
        }

        /// <summary>
        /// 增加对象
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public int Add(QueryField value)
        {
            return base.Add(value);
        }

        /// <summary>
        /// 增加对象
        /// </summary>
        /// <param name="dataFieldId">字段编号</param>
        /// <param name="dataFieldType">字段类型</param>
        /// <param name="dataTableId">表的编号</param>
        /// <param name="dataTableName">表名</param>
        /// <param name="dataTableAlias">表的别称</param>
        /// <param name="name">默认的字段名称(惟一的)</param> 
        /// <param name="columnName">列名</param>        
        /// <param name="alias">列的别名</param>
        /// <param name="authorityType">权限类型</param>
        /// <param name="dataFieldProperty">字段属性</param>
        /// <returns></returns>
        public int Add(decimal dataFieldId, byte dataFieldType, decimal dataTableId, string dataTableName, string dataTableAlias, string name,
            string columnName, string alias, byte authorityType, DataFieldProperty dataFieldProperty)
        {
            return Add(new QueryField(dataFieldId, dataFieldType, dataTableId, dataTableName, dataTableAlias, name, columnName, alias, authorityType, dataFieldProperty));
        }
        #endregion
    }
}
