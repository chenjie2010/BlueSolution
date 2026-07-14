//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: SCUFinance.cs
// 描述: SCUFinance 四川大学财务处数据库访问类
// 作者：ChenJie 
// 编写日期：2017/07/18
// Copyright 2017
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Common;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Oracle.ManagedDataAccess.Client;
using AppFramework.Core;
using AppFramework.Reference.Oracle;

namespace Blue.OracleDAL
{
    /// <summary>
    /// 四川大学财务处数据库访问类
    /// </summary>
    public class SCUFinance
    {
        #region 构造函数

        /// <summary>
        /// 获取工资结构记录数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="startIncluded"></param>
        /// <param name="end"></param>
        /// <param name="endIncluded"></param>
        /// <returns></returns>
        public int GetCountOfSalaryStruct(DateTime start, bool startIncluded, DateTime end, bool endIncluded)
        {
            return GetTableCount(DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_STRUCTURE_TABLE_NAME"), start, startIncluded, end, endIncluded);
        }

        /// <summary>
        /// 获取 PS_ETL_CW_SALARY_STRUCTURE 表的记录
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="start"></param>
        /// <param name="startIncluded"></param>
        /// <param name="end"></param>
        /// <param name="endIncluded"></param>
        /// <returns></returns>

        public DataSet GetSalaryStruct(int startPosition, int count, DateTime start, bool startIncluded, DateTime end, bool endIncluded)
        {
            if (startPosition <= 0)
            {
                throw new ArgumentException("起始位置不能小于0。");
            }
            if (count <= 0)
            {
                throw new ArgumentException("记录数不能小于0。");
            }

            //无 ORDER BY 排序的写法：效率最高，查询的数据量再大，几乎不受影响。
            string tableName = DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_STRUCTURE_TABLE_NAME");
            string dataFieldNames = DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_STRUCTURE_FIELD_NAME");

            return GetTableData(tableName, dataFieldNames, startPosition, count, start, startIncluded, end, endIncluded);
        }

        /// <summary>
        /// 获取工资结构表最新的更新时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLatestTimeOfSalaryStruct(string tableName)
        {
            return GetLatestTimeUpdated(DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_STRUCTURE_TABLE_NAME"));
        }

        /// <summary>
        /// 获取工资表记录数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="startIncluded"></param>
        /// <param name="end"></param>
        /// <param name="endIncluded"></param>
        /// <returns></returns>
        public int GetCountOfSalary(DateTime start, bool startIncluded, DateTime end, bool endIncluded)
        {
            return GetTableCount(DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_TABLE_NAME"), start, startIncluded, end, endIncluded);
        }

        /// <summary>
        /// 获取 PS_ETL_CW_SALARY 表的记录
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="start"></param>
        /// <param name="startIncluded"></param>
        /// <param name="end"></param>
        /// <param name="endIncluded"></param>
        /// <returns></returns>
        public DataSet GetSalary(int startPosition, int count, DateTime start, bool startIncluded, DateTime end, bool endIncluded)
        {
            if (startPosition <= 0)
            {
                throw new ArgumentException("起始位置不能小于0。");
            }
            if (count <= 0)
            {
                throw new ArgumentException("记录数不能小于0。");
            }

            //无 ORDER BY 排序的写法：效率最高，查询的数据量再大，几乎不受影响。
            string tableName = DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_TABLE_NAME");
            string dataFieldNames = DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_FIELD_NAME");

            return GetTableData(tableName, dataFieldNames, startPosition, count, start, startIncluded, end, endIncluded);
        }

        /// <summary>
        /// 获取工资结构表最新的更新时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLatestTimeOfSalary()
        {
            return GetLatestTimeUpdated(DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_TABLE_NAME"));
        }

        /// <summary>
        /// 获取新的工资表记录数
        /// </summary>
        /// <param name="start"></param>
        /// <param name="startIncluded"></param>
        /// <param name="end"></param>
        /// <param name="endIncluded"></param>
        /// <returns></returns>
        public int GetCountOfNewSalary(DateTime start, bool startIncluded, DateTime end, bool endIncluded)
        {
            return GetTableCount(DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_L_TABLE_NAME"), start, startIncluded, end, endIncluded);
        }

        /// <summary>
        /// 获取 PS_ETL_CW_SALARY_L 表的记录
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="start"></param>
        /// <param name="startIncluded"></param>
        /// <param name="end"></param>
        /// <param name="endIncluded"></param>
        /// <returns></returns>
        public DataSet GetNewSalary(int startPosition, int count, DateTime start, bool startIncluded, DateTime end, bool endIncluded)
        {
            if (startPosition <= 0)
            {
                throw new ArgumentException("起始位置不能小于0。");
            }
            if (count <= 0)
            {
                throw new ArgumentException("记录数不能小于0。");
            }

            //无 ORDER BY 排序的写法：效率最高，查询的数据量再大，几乎不受影响。
            string tableName = DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_L_TABLE_NAME");
            string dataFieldNames = DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_L_FIELD_NAME");

            return GetTableData(tableName, dataFieldNames, startPosition, count, start, startIncluded, end, endIncluded);
        }

        /// <summary>
        /// 获取新的工资表最新的更新时间
        /// </summary>
        /// <returns></returns>
        public DateTime GetLatestTimeOfNewSalary()
        {
            return GetLatestTimeUpdated(DataSettingHelper.GetAppSettingByName("SCU_CW_SALARY_L_TABLE_NAME"));
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获取最新的更新时间
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        private DateTime GetLatestTimeUpdated(string tableName)
        {
            DateTime dateTime = DateTime.MinValue;
            string sql = string.Format("SELECT MAX(LASTUPD_DTTM) FROM {0} ", tableName);

            //获得系统数据库对象
            OracleDatabase db = OracleHelper.GetSCUFinanceDatabase();
            try
            {
                using (OracleCommand dbCommand = db.GetSqlStringCommand(sql))
                {
                    dateTime = DataConvertionHelper.GetDateTime(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return dateTime;
        }

        /// <summary>
        /// 获得表的记录数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="start"></param>
        /// <param name="startIncluded"></param>
        /// <param name="end"></param>
        /// <param name="endIncluded"></param>
        /// <returns></returns>
        private int GetTableCount(string tableName, DateTime start, bool startIncluded, DateTime end, bool endIncluded)
        {
            int count = 0;

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("SELECT COUNT(1) FROM {0} ", tableName);
            if (!DataConvertionHelper.IsNullValue(start) || !DataConvertionHelper.IsNullValue(end))
            {
                sb.Append("WHERE ");
            }
            if (!DataConvertionHelper.IsNullValue(start))
            {
                sb.AppendFormat("LASTUPD_DTTM {0} to_date(:LASTUPD_DTTM_1, 'yyyy-mm-dd hh24:mi:ss') ", startIncluded ? ">=" : ">");
            }
            if (!DataConvertionHelper.IsNullValue(end))
            {
                if (!DataConvertionHelper.IsNullValue(start))
                {
                    sb.Append("AND ");
                }
                sb.AppendFormat("LASTUPD_DTTM {0} to_date(:LASTUPD_DTTM_2, 'yyyy-mm-dd hh24:mi:ss') ", endIncluded ? "<=" : "<");
            }

            //获得系统数据库对象
            OracleDatabase db = OracleHelper.GetSCUFinanceDatabase();
            try
            {
                using (OracleCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    if (!DataConvertionHelper.IsNullValue(start))
                    {
                        db.AddInParameter(dbCommand, "LASTUPD_DTTM_1", OracleDbType.NVarchar2, start.ToString());
                    }
                    if (!DataConvertionHelper.IsNullValue(end))
                    {
                        db.AddInParameter(dbCommand, "LASTUPD_DTTM_2", OracleDbType.NVarchar2, end.ToString());
                    }
                    count = Convert.ToInt32(db.ExecuteScalar(dbCommand));
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return count;
        }

        /// <summary>
        /// 获取表的记录
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataFieldNames"></param>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="start"></param>
        /// <param name="startIncluded"></param>
        /// <param name="end"></param>
        /// <param name="endIncluded"></param>
        /// <returns></returns>
        private DataSet GetTableData(string tableName, string dataFieldNames, int startPosition, int count, DateTime start, bool startIncluded, DateTime end, bool endIncluded)
        {
            DataSet ds = null;

            if (startPosition <= 0)
            {
                throw new ArgumentException("起始位置不能小于0。");
            }
            if (count <= 0)
            {
                throw new ArgumentException("记录数不能小于0。");
            }

            //无 ORDER BY 排序的写法：效率最高，查询的数据量再大，几乎不受影响。
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT * FROM ");
            sb.AppendFormat("(SELECT {0}, ROWNUM AS RN FROM {1} WHERE ", dataFieldNames, tableName);
            if (!DataConvertionHelper.IsNullValue(start))
            {
                sb.AppendFormat("LASTUPD_DTTM {0} to_date(:LASTUPD_DTTM_1, 'yyyy-mm-dd hh24:mi:ss') AND ", startIncluded ? ">=" : ">");
            }
            if (!DataConvertionHelper.IsNullValue(end))
            {
                sb.AppendFormat("LASTUPD_DTTM {0} to_date(:LASTUPD_DTTM_2, 'yyyy-mm-dd hh24:mi:ss') AND ", endIncluded ? "<=" : "<");
            }
            sb.AppendFormat("ROWNUM < {0}) T WHERE T.RN >= {1}", startPosition + count, startPosition);

            //有 ORDER BY 排序的写法：效率最高, 随着查询范围的扩大，速度变慢。
            //sb.Append("SELECT * FROM ");
            //sb.Append("(SELECT ROWNUM AS RN, T.* FROM (");
            //sb.Append("SELECT UNIT_NO, NAME, FGZNF, FGZYF, ITEMCODE, FIX_ABST, AMOUNT, LASTUPD_DTTM FROM PS_ETL_CW_SALARY ");
            //sb.Append("WHERE LASTUPD_DTTM >= to_date(:LASTUPD_DTTM_1, 'yyyy-mm-dd hh24:mi:ss') AND LASTUPD_DTTM <= to_date(:LASTUPD_DTTM_2, 'yyyy-mm-dd hh24:mi:ss' ORDER BY LASTUPD_DTTM) T ");
            //sb.Append("WHERE TROWNUM < :POSTION_2) TT ");
            //sb.Append("WHERE TT.RN >= :POSTION_1");

            //获得系统数据库对象
            OracleDatabase db = OracleHelper.GetSCUFinanceDatabase();
            try
            {
                using (OracleCommand dbCommand = db.GetSqlStringCommand(sb.ToString()))
                {
                    //给参数赋值
                    if (!DataConvertionHelper.IsNullValue(start))
                    {
                        db.AddInParameter(dbCommand, "LASTUPD_DTTM_1", OracleDbType.NVarchar2, start.ToString());
                    }
                    if (!DataConvertionHelper.IsNullValue(end))
                    {
                        db.AddInParameter(dbCommand, "LASTUPD_DTTM_2", OracleDbType.NVarchar2, end.ToString());
                    }
                    ds = db.ExecuteDataSet(dbCommand);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常 
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }

            return ds;
        }

        #endregion
    }
}
