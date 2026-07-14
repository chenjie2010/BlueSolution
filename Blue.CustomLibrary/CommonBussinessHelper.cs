//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: CommonBussinessHelper.cs
// 描述: 通用业务帮助类
// 作者：ChenJie 
// 编写日期：2018/02/26
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using Blue.Model.BusinessModule;

namespace Blue.CustomLibrary
{
    /// <summary>
    /// 通用业务帮助类
    /// </summary>
    public sealed class CommonBussinessHelper
    {
        #region 公有静态方法
        
        /// <summary>
        /// 获得系统字段
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="systemDataField"></param>
        /// <returns></returns>
        public static CustomDataFieldInfo GetSystemDataFieldInfo(decimal tableId, SystemPhysicalDataField systemPhysicalDataField)
        {
            CustomDataFieldInfo customDataFieldInfo = null;

            switch (systemPhysicalDataField)
            {
                case SystemPhysicalDataField.RecordId: /* 1.1 记录编号 */
                    customDataFieldInfo = new CustomDataFieldInfo(DataConvertionHelper.GetConvertedInt(SystemPhysicalDataField.RecordId), 
                        decimal.MinValue, tableId, decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.RecordId), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId),
                        string.Empty, DataConvertionHelper.GetConvertedByte(DataFieldProperty.SystemPhysicalDataField), DataConvertionHelper.GetConvertedByte(PhysicalDataFieldType.Decimal),
                        0, (byte)BasedDataType.Decimal, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, "该表的记录的编号！");
                    break;

                case SystemPhysicalDataField.UserId: /* 1.2 用户编号 */
                    customDataFieldInfo = new CustomDataFieldInfo(DataConvertionHelper.GetConvertedInt(SystemPhysicalDataField.UserId), 
                        decimal.MinValue, tableId, decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.UserId), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId),
                        string.Empty, DataConvertionHelper.GetConvertedByte(DataFieldProperty.SystemPhysicalDataField), DataConvertionHelper.GetConvertedByte(PhysicalDataFieldType.Decimal),
                        0, (byte)BasedDataType.Decimal, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, "该表的记录所属用户的用户编号！");
                    break;

                case SystemPhysicalDataField.UserName: /* 1.3 用户名 */
                    customDataFieldInfo = new CustomDataFieldInfo(DataConvertionHelper.GetConvertedInt(SystemPhysicalDataField.UserName), 
                        decimal.MinValue, tableId, decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.UserName), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName),
                        string.Empty, DataConvertionHelper.GetConvertedByte(DataFieldProperty.SystemPhysicalDataField), DataConvertionHelper.GetConvertedByte(PhysicalDataFieldType.ArbitraryString),
                        0, (byte)BasedDataType.String, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, "该表的记录所属用户的用户名！");
                    break;

                case SystemPhysicalDataField.UserTypeId:  /* 1.5 用户类型 */
                    customDataFieldInfo = new CustomDataFieldInfo(DataConvertionHelper.GetConvertedInt(SystemPhysicalDataField.UserTypeId), 
                        decimal.MinValue, tableId, decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.UserTypeId), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserTypeId),
                        string.Empty, DataConvertionHelper.GetConvertedByte(DataFieldProperty.SystemPhysicalDataField), DataConvertionHelper.GetConvertedByte(PhysicalDataFieldType.Decimal),
                        0, (byte)BasedDataType.Decimal, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, "该表的记录所属用户的用户类型！");
                    break;

                case SystemPhysicalDataField.DepId: /* 1.7 用户单位 */
                    customDataFieldInfo = new CustomDataFieldInfo(DataConvertionHelper.GetConvertedInt(SystemPhysicalDataField.DepId), 
                        decimal.MinValue, tableId, decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.DepId), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.DepId),
                        string.Empty, DataConvertionHelper.GetConvertedByte(DataFieldProperty.SystemPhysicalDataField), DataConvertionHelper.GetConvertedByte(PhysicalDataFieldType.Decimal),
                        0, (byte)BasedDataType.Decimal, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, "该表的记录所属用户单位！");
                    break;

                case SystemPhysicalDataField.RecordSorting:/* 1.9 记录排序 */
                    customDataFieldInfo = new CustomDataFieldInfo(DataConvertionHelper.GetConvertedInt(SystemPhysicalDataField.RecordSorting), 
                        decimal.MinValue, tableId, decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.RecordSorting), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordSorting),
                        string.Empty, DataConvertionHelper.GetConvertedByte(DataFieldProperty.SystemPhysicalDataField), DataConvertionHelper.GetConvertedByte(PhysicalDataFieldType.Int32),
                        0, (byte)BasedDataType.Int32, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, "该记录的排序！");
                    break;

                case SystemPhysicalDataField.CreationTime: /* 1.10 增加日期 */
                    customDataFieldInfo = new CustomDataFieldInfo(DataConvertionHelper.GetConvertedInt(SystemPhysicalDataField.CreationTime), 
                        decimal.MinValue, tableId, decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.CreationTime), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime),
                        string.Empty, DataConvertionHelper.GetConvertedByte(DataFieldProperty.SystemPhysicalDataField), DataConvertionHelper.GetConvertedByte(PhysicalDataFieldType.YearAndMonthAndDayAndTime),
                        0, (byte)BasedDataType.DateTime, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, "该表的记录增加日期！");
                    break;

                case SystemPhysicalDataField.ModificationTime: /* 1.11 修改日期 */
                    customDataFieldInfo = new CustomDataFieldInfo(DataConvertionHelper.GetConvertedInt(SystemPhysicalDataField.ModificationTime), 
                        decimal.MinValue, tableId, decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.ModificationTime), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime),
                        string.Empty, DataConvertionHelper.GetConvertedByte(DataFieldProperty.SystemPhysicalDataField), DataConvertionHelper.GetConvertedByte(PhysicalDataFieldType.YearAndMonthAndDayAndTime),
                        0, (byte)BasedDataType.DateTime, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, "该表的记录修改日期！");
                    break;

                case SystemPhysicalDataField.AuditedStatus:/* 1.12 审核状态 */
                    customDataFieldInfo = new CustomDataFieldInfo(DataConvertionHelper.GetConvertedInt(SystemPhysicalDataField.AuditedStatus), 
                        decimal.MinValue, tableId, decimal.MinValue, decimal.MinValue, DataFieldHelper.GetLogicalName(SystemPhysicalDataField.AuditedStatus), DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus),
                        string.Empty, DataConvertionHelper.GetConvertedByte(DataFieldProperty.SystemPhysicalDataField), DataConvertionHelper.GetConvertedByte(PhysicalDataFieldType.YearAndMonthAndDayAndTime),
                        0, (byte)BasedDataType.Int32, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, "该记录审核状态！");
                    break;
            }

            return customDataFieldInfo;
        }

        /// <summary>
        /// 获得系统字段列表
        /// </summary>
        /// <param name="systemDataField"></param>
        /// <returns></returns>
        public static IList<CommonDataFieldInfo> GetCommonDataFieldInfos(Int64 systemDataField)
        {
            return GetCommonDataFieldInfos(decimal.MinValue, string.Empty, systemDataField);
        }

        /// <summary>
        /// 获得系统字段列表
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableName"></param>
        /// <param name="systemDataField"></param>
        /// <returns></returns>
        public static IList<CommonDataFieldInfo> GetCommonDataFieldInfos(decimal tableId, string tableName, Int64 systemDataField)
        {
            List<CommonDataFieldInfo> commonDataFieldInfos = new List<CommonDataFieldInfo>();
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemDataField));
            foreach (EnumItem enumItem in enumItems)
            {
                bool result = ((systemDataField >> enumItem.Value) & 1L) == 1L;
                if (result)
                {
                    commonDataFieldInfos.Add(GetCommonDataFieldInfo(tableId, tableName, (SystemDataField)enumItem.Value));
                }
            }

            return commonDataFieldInfos;
        }


        /// <summary>
        /// 获得系统字段的对象
        /// </summary>
        /// <param name="systemDataField"></param>
        /// <returns></returns>
        public static List<ExtendedCustomDataFieldInfo> GetSystemDataFieldInfos(Int64 systemDataField)
        {
            return GetExtendedCustomDataFieldInfos(decimal.MinValue, string.Empty, systemDataField);
        }

        /// <summary>
        /// 获得系统字段的对象
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="systemDataField"></param>
        /// <returns></returns>
        public static CustomDataFieldInfo GetSystemDataFieldInfo(decimal tableId, SystemDataField systemDataField)
        {
            CustomDataFieldInfo customDataFieldInfo = null;

            string logicalName = UserEnumHelper.GetEnumText(systemDataField);
            string physicalName = DataFieldHelper.GetSystemLogicalDataFieldName(systemDataField);
            DbType dbType = DbType.String;
            BasedDataType basedDataType = BasedDataType.String;
            switch (systemDataField)
            {
                case SystemDataField.UserName:
                case SystemDataField.UserActualName:
                case SystemDataField.DepName:
                case SystemDataField.DepValue:
                case SystemDataField.DepProperty:
                case SystemDataField.DepFstAdditionalCode:
                case SystemDataField.DepScdAdditionalCode:
                case SystemDataField.UserTypeName:
                case SystemDataField.UserTypeCode:
                    dbType = DbType.String;
                    basedDataType = BasedDataType.String;
                    break;

                case SystemDataField.CreationTime:
                case SystemDataField.ModificationTime:
                    dbType = DbType.DateTime;
                    basedDataType = BasedDataType.DateTime;
                    break;

                case SystemDataField.AuditedStatus:
                case SystemDataField.CurrentState:
                    dbType = DbType.Byte;
                    basedDataType = BasedDataType.Int32;
                    break;
            }

            customDataFieldInfo = new CustomDataFieldInfo(Convert.ToByte(systemDataField), decimal.MinValue, decimal.MinValue, decimal.MinValue, tableId,
                logicalName, physicalName, string.Empty, (byte)DataFieldProperty.SystemPhysicalDataField, (byte)dbType,
                0, (byte)basedDataType, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, string.Empty);

            return customDataFieldInfo;
        }


        /// <summary>
        /// 获得系统字段的对象
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableName"></param>
        /// <param name="systemDataField"></param>
        /// <returns></returns>
        public static CustomDataFieldInfo GetSystemDataFieldInfo(decimal tableId, string tableName, SystemDataField systemDataField)
        {
            CustomDataFieldInfo customDataFieldInfo = null;

            string logicalName = UserEnumHelper.GetEnumText(systemDataField);
            string physicalName = DataFieldHelper.GetSystemLogicalDataFieldName(tableName, systemDataField);
            DbType dbType = DbType.String;
            BasedDataType basedDataType = BasedDataType.String;
            switch (systemDataField)
            {
                case SystemDataField.UserName:
                case SystemDataField.UserActualName:
                case SystemDataField.DepName:
                case SystemDataField.DepValue:
                case SystemDataField.DepProperty:
                case SystemDataField.DepFstAdditionalCode:
                case SystemDataField.DepScdAdditionalCode:
                case SystemDataField.UserTypeName:
                case SystemDataField.UserTypeCode:
                    dbType = DbType.String;
                    basedDataType = BasedDataType.String;
                    break;

                case SystemDataField.CreationTime:
                case SystemDataField.ModificationTime:
                    dbType = DbType.DateTime;
                    basedDataType = BasedDataType.DateTime;
                    break;

                case SystemDataField.AuditedStatus:
                case SystemDataField.CurrentState:
                    dbType = DbType.Byte;
                    basedDataType = BasedDataType.Int32;
                    break;
            }

            customDataFieldInfo = new CustomDataFieldInfo(Convert.ToByte(systemDataField), decimal.MinValue, decimal.MinValue, decimal.MinValue, tableId,
                logicalName, physicalName, string.Empty, (byte)DataFieldProperty.SystemPhysicalDataField, (byte)dbType,
                0, (byte)basedDataType, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, string.Empty);

            return customDataFieldInfo;
        }

        /// <summary>
        /// 获得系统字段的对象
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableName"></param>
        /// <param name="systemDataField"></param>
        /// <returns></returns>
        public static ExtendedCustomDataFieldInfo GetExtendedCustomDataFieldInfo(decimal tableId, string tableName, SystemDataField systemDataField)
        {
            ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = null;

            string logicalName = UserEnumHelper.GetEnumText(systemDataField);
            string physicalName = DataFieldHelper.GetSystemLogicalDataFieldName(tableName, systemDataField);
            string name = DataFieldHelper.GetOnlySystemLogicalDataFieldName(systemDataField);
            DbType dbType = DbType.String;
            BasedDataType basedDataType = BasedDataType.String;
            switch (systemDataField)
            {
                case SystemDataField.UserName:
                case SystemDataField.UserActualName:
                case SystemDataField.DepName:
                case SystemDataField.DepValue:
                case SystemDataField.DepProperty:
                case SystemDataField.DepFstAdditionalCode:
                case SystemDataField.DepScdAdditionalCode:
                case SystemDataField.UserTypeName:
                case SystemDataField.UserTypeCode:
                    dbType = DbType.String;
                    basedDataType = BasedDataType.String;
                    break;

                case SystemDataField.CreationTime:
                case SystemDataField.ModificationTime:
                    dbType = DbType.DateTime;
                    basedDataType = BasedDataType.DateTime;
                    break;

                case SystemDataField.AuditedStatus:
                case SystemDataField.CurrentState:
                    dbType = DbType.Byte;
                    basedDataType = BasedDataType.Int32;
                    break;
            }
            extendedCustomDataFieldInfo = new ExtendedCustomDataFieldInfo(Convert.ToDecimal(systemDataField), decimal.MinValue, decimal.MinValue, decimal.MinValue, tableId, 
                logicalName, physicalName, string.Empty, (byte)DataFieldProperty.SystemPhysicalDataField, (byte)dbType, 
                0, (byte)basedDataType, string.Empty, string.Empty, 0, true, false, false, false, string.Empty, string.Empty, 0, string.Empty, name, (byte)DataFieldAuthority.ReadOnly);

            return extendedCustomDataFieldInfo;
        }

        /// <summary>
        /// 获得系统字段的对象
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableName"></param>
        /// <param name="systemDataField"></param>
        /// <returns></returns>
        public static CommonDataFieldInfo GetCommonDataFieldInfo(decimal tableId, string tableName, SystemDataField systemDataField)
        {
            CommonDataFieldInfo commonDataFieldInfo = null;

            string logicalName = UserEnumHelper.GetEnumText(systemDataField);
            string physicalName = DataFieldHelper.GetSystemLogicalDataFieldName(tableName, systemDataField);
            string name = DataFieldHelper.GetOnlySystemLogicalDataFieldName(systemDataField);
            DbType dbType = DbType.String;
            switch (systemDataField)
            {
                case SystemDataField.UserName:
                case SystemDataField.UserActualName:
                case SystemDataField.DepName:
                case SystemDataField.DepValue:
                case SystemDataField.DepProperty:
                case SystemDataField.DepFstAdditionalCode:
                case SystemDataField.DepScdAdditionalCode:
                case SystemDataField.UserTypeName:
                case SystemDataField.UserTypeCode:
                    dbType = DbType.String;
                    break;

                case SystemDataField.CreationTime:
                case SystemDataField.ModificationTime:
                    dbType = DbType.DateTime;
                    break;

                case SystemDataField.AuditedStatus:
                case SystemDataField.CurrentState:
                    dbType = DbType.Byte;
                    break;
            }

            commonDataFieldInfo = new CommonDataFieldInfo(Convert.ToDecimal(systemDataField), tableId, physicalName, logicalName, string.Empty, DataFieldProperty.SystemPhysicalDataField, (byte)dbType);

            return commonDataFieldInfo;
        }

        /// <summary>
        /// 获取系统字段的信息
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="tableName"></param>
        /// <param name="dataFieldSetting"></param>
        /// <returns></returns>
        public static List<ExtendedCustomDataFieldInfo> GetExtendedCustomDataFieldInfos(decimal tableId, string tableName, Int64 dataFieldSetting)
        {
            List<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos = new List<ExtendedCustomDataFieldInfo>();
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemDataField));
            foreach (EnumItem enumItem in enumItems)
            {
                bool result = ((dataFieldSetting >> enumItem.Value) & 1L) == 1L;
                if (result)
                {
                    extendedCustomDataFieldInfos.Add(GetExtendedCustomDataFieldInfo(tableId, tableName, (SystemDataField)enumItem.Value));
                }
            }

            return extendedCustomDataFieldInfos;
        }

        #endregion
    }
}
