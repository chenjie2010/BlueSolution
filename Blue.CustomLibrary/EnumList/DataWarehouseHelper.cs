//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名： DataWarehouseHelper.cs
// 描述： 数据仓库操作类
// 作者：ChenJie 
// 编写日期：2016/09/11
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.EnterpriseLibrary.Common;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.CustomLibrary.EnterpriseLibrary
{
    /// <summary>
    /// 数据仓库操作类
    /// </summary>
    public sealed class DataWarehouseHelper
    {
        /// <summary>
        /// 默认业务数据库名称
        /// </summary>
        public static string BusinessDatabaseName
        {
            get
            {
                return "Blue_Business";
            }
        }

        /// <summary>
        /// 获得数据库对象
        /// </summary>
        /// <param name="dataWarehouseId"></param>
        /// <returns></returns>
        public static SqlDatabase GetDatabaseByDataWarehouseId(byte dataWarehouseId)
        {
            return DataAccessHelper.GetDatabase(GetDataSourceName((DataWarehouse)dataWarehouseId));
        }

        /// <summary>
        /// 根据仓库类型获得仓库在数据库配置文件中的名称
        /// </summary>
        /// <param name="dataSourceName"></param>
        /// <returns></returns>
        public static int GetDataSourceId(string dataSourceName)
        {
            DataWarehouse dataWarehouse = DataWarehouse.FirstWarehouse;
            string name = dataSourceName.ToLower();

            switch (name)
            {
                case "blue_1":
                    dataWarehouse = DataWarehouse.FirstWarehouse;
                    break;

                case "blue_2":
                    dataWarehouse = DataWarehouse.SecondWarehouse;
                    break;

                case "blue_3":
                    dataWarehouse = DataWarehouse.ThirdWarehouse;
                    break;

                case "blue_4":
                    dataWarehouse = DataWarehouse.FourthWarehouse;
                    break;

                case "blue_5":
                    dataWarehouse = DataWarehouse.FifthWarehouse;
                    break;
            }

            return (int)dataWarehouse;
        }

        /// <summary>
        /// 根据仓库类型获得仓库在数据库配置文件中的名称
        /// </summary>
        /// <param name="dataWarehouse"></param>
        /// <returns></returns>
        public static string GetDataSourceName(DataWarehouse dataWarehouse)
        {
            string dataSourceName = string.Empty;

            switch (dataWarehouse)
            {
                case DataWarehouse.FirstWarehouse:
                    dataSourceName = "Blue_1";
                    break;

                case DataWarehouse.SecondWarehouse:
                    dataSourceName = "Blue_2";
                    break;

                case DataWarehouse.ThirdWarehouse:
                    dataSourceName = "Blue_3";
                    break;

                case DataWarehouse.FourthWarehouse:
                    dataSourceName = "Blue_4";
                    break;

                case DataWarehouse.FifthWarehouse:
                    dataSourceName = "Blue_5";
                    break;
            }

            return dataSourceName;
        }
        
        #region 过时的函数

        /// <summary>
        /// 根据仓库类型获得仓库在数据库配置文件中的名称
        /// </summary>
        /// <param name="dataWarehouse"></param>
        /// <returns></returns>
        public static string GetOldDataSourceName(DataWarehouse dataWarehouse)
        {
            string dataSurceName = string.Empty;

            switch (dataWarehouse)
            {
                case DataWarehouse.FirstWarehouse:
                    dataSurceName = "Promotion_1";
                    break;

                case DataWarehouse.SecondWarehouse:
                    dataSurceName = "Promotion_2";
                    break;

                case DataWarehouse.ThirdWarehouse:
                    dataSurceName = "Promotion_3";
                    break;

                case DataWarehouse.FourthWarehouse:
                    dataSurceName = "Promotion_4";
                    break;

                case DataWarehouse.FifthWarehouse:
                    dataSurceName = "Promotion_5";
                    break;
            }

            return dataSurceName;
        }

        #endregion
    }
}
