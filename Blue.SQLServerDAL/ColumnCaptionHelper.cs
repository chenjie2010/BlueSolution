//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: ColumnCaptionHelper.cs
// 描述: 数据列名称类
// 作者：ChenJie 
// 编写日期：2016-08-26
// 版权所有 (C) 四川大学 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.SQLServerDAL
{
    /// <summary>
    /// 数据列名称类
    /// </summary>
    public sealed class ColumnCaptionHelper
    {
        #region 定义成员变量

        //预先缓存处理
        private static Dictionary<string, string> columnCaptions = new Dictionary<string, string>();

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        static ColumnCaptionHelper()
        {
           
        }

        #endregion

        #region 缓存预加载方法

        /// <summary>
        /// 缓存预加载
        /// </summary>
        public static void LoadColumnCaption()
        {
            string sqlTableName = "SELECT name FROM sysobjects WHERE xtype='U'";
            string sqlDataFieldName = string.Format("SELECT c.name,p.value FROM sys.extended_properties p, sys.columns c WHERE p.major_id = OBJECT_ID(@TableName) and p.major_id = c.object_id and p.minor_id = c.column_id");
            //获得系统数据库对象
            SqlDatabase db = DataAccessHelper.GetDatabase();
            try
            {
                IList<string> tableNames = new List<string>();
                using (DbCommand dbCommand = db.GetSqlStringCommand(sqlTableName))
                {
                    using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                    {
                        while (dataReader.Read())
                        {
                            tableNames.Add(DataConvertionHelper.GetString(dataReader[0]));
                        }
                        if (dataReader != null)
                        {
                            dataReader.Close();
                        }
                    }
                }
                foreach (string tableName in tableNames)
                {
                    using (DbCommand dbCommand = db.GetSqlStringCommand(sqlDataFieldName))
                    {
                        db.AddInParameter(dbCommand, "TableName", DbType.String, tableName);
                        using (IDataReader dataReader = db.ExecuteReader(dbCommand))
                        {
                            while (dataReader.Read())
                            {
                                string key = DataConvertionHelper.GetString(dataReader[0]);
                                if (!columnCaptions.ContainsKey(key))
                                {
                                    columnCaptions.Add(key, DataConvertionHelper.GetString(dataReader[1]));
                                }
                            }
                            if (dataReader != null)
                            {
                                dataReader.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获取名称
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public static string GetColumnCaption(string columnName)
        {
            string vaule = string.Empty;

            if (columnCaptions.ContainsKey(columnName.Trim()))
            {
                vaule = columnCaptions[columnName];
            }
                        
            return vaule;
        }

        #endregion

    }
}
