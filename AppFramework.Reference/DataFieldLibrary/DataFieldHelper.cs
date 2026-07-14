//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataFieldHelper.cs
// 描述: 字段帮助类
// 作者：ChenJie 
// 编写日期：2016/09/23
// Copyright 2016
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;

namespace AppFramework.Reference.DataFieldLibrary
{
    /// <summary>
    /// 字段帮助类
    /// </summary>
    public sealed class DataFieldHelper
    {
        #region 公有静态方法

        /// <summary>
        /// 是否是本表的系统字段
        /// </summary>
        /// <param name="systemDataField"></param>
        /// <returns></returns>
        public static bool IsLocalSystemDataField(SystemDataField systemDataField)
        {
            bool result = false;

            if (systemDataField == SystemDataField.AuditedStatus || systemDataField == SystemDataField.CurrentState
                || systemDataField == SystemDataField.DepProperty || systemDataField == SystemDataField.DepFstAdditionalCode
                || systemDataField == SystemDataField.DepScdAdditionalCode || systemDataField == SystemDataField.CreationTime || systemDataField == SystemDataField.ModificationTime)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 获取系统字段的信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="dataFieldSetting"></param>
        /// <returns></returns>
        public static Dictionary<string, CommonDataFieldInfo> GetSystemDataFieldInfo(string tableName, Int64 dataFieldSetting)
        {
            Dictionary<string, CommonDataFieldInfo> systemDataFieldNameRelations = new Dictionary<string, CommonDataFieldInfo>();
            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemDataField));
            foreach (EnumItem enumItem in enumItems)
            {
                bool result = ((dataFieldSetting >> enumItem.Value) & 1L) == 1L;
                if (result)
                {
                    SystemDataField systemDataField = (SystemDataField)enumItem.Value;
                    string keyName = GetOnlySystemLogicalDataFieldName(systemDataField);
                    string physicalName = GetSystemLogicalDataFieldName(tableName, systemDataField);
                    DbType dbType = DbType.String;
                    switch (systemDataField)
                    {
                        case SystemDataField.UserName:
                        case SystemDataField.UserActualName:
                        case SystemDataField.DepName:
                        case SystemDataField.DepCode:
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
                    systemDataFieldNameRelations.Add(keyName, new CommonDataFieldInfo(Convert.ToDecimal(enumItem.Value), physicalName,
                        enumItem.Text, string.Empty, DataFieldProperty.SystemPhysicalDataField, (byte)dbType));
                }
            }

            return systemDataFieldNameRelations;
        }

        /// <summary>
        /// 获取系统字段的信息
        /// </summary>
        /// <param name="dataFieldSetting"></param>
        /// <returns></returns>
        public static Dictionary<string, CommonDataFieldInfo> GetSystemDataFieldInfo(Int64 dataFieldSetting)
        {
            return GetSystemDataFieldInfo(string.Empty, dataFieldSetting);
        }

        /// <summary>
        /// 根据字段类型条件获得字段条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public static IList<WhereConditon> GetWhereConditons(DataFieldFilter dataFieldFilter)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            switch (dataFieldFilter)
            {
                case DataFieldFilter.SystemDataFieldAndPhysicalDataField:
                case DataFieldFilter.OnlyPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    break;

                case DataFieldFilter.DateTimeTypeInPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                       (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("BasedDataType", "BasedDataType_0", DbType.Byte, (byte)BasedDataType.DateTime,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataFieldFilter.DigtalTypeInPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("BasedDataType", "BasedDataType_0", DbType.Byte, (byte)BasedDataType.Int32,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("BasedDataType", "BasedDataType_1", DbType.Byte, (byte)BasedDataType.Decimal,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataFieldFilter.EnumTypeInPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_0", DbType.Byte, (byte)PhysicalDataFieldType.DropdownListEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)PhysicalDataFieldType.DropdownListEnumValue, 
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_2", DbType.Byte, (byte)PhysicalDataFieldType.DropdownListFstAdditionalCode, 
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_3", DbType.Byte, (byte)PhysicalDataFieldType.DropdownListScdAdditionalCode, 
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_4", DbType.Byte, (byte)PhysicalDataFieldType.TreeViewEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_5", DbType.Byte, (byte)PhysicalDataFieldType.TreeViewEnumValue, 
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_6", DbType.Byte, (byte)PhysicalDataFieldType.TreeViewFstAdditionalCode, 
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_7", DbType.Byte, (byte)PhysicalDataFieldType.TreeViewScdAdditionalCode, 
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_8", DbType.Byte, (byte)PhysicalDataFieldType.CscadeEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_9", DbType.Byte, (byte)PhysicalDataFieldType.MultiSelectedEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataFieldFilter.PrimaryAssociationInPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)PhysicalDataFieldType.PrimaryAssociation,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    break;

                case DataFieldFilter.OneDimCodeTypeInPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_0", DbType.Byte, (byte)PhysicalDataFieldType.Int32,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)PhysicalDataFieldType.Decimal,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_2", DbType.Byte, (byte)PhysicalDataFieldType.NumeralString,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_3", DbType.Byte, (byte)PhysicalDataFieldType.CharString,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_4", DbType.Byte, (byte)PhysicalDataFieldType.MixedString,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataFieldFilter.OnlyDepartmentPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_0", DbType.Byte, (byte)PhysicalDataFieldType.DepartmentDropdownListEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)PhysicalDataFieldType.DepartmentTreeViewEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataFieldFilter.PhysicalFieldAndKeyLogicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty_0", DbType.Byte,
                       (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty_1", DbType.Byte,
                       (byte)DataFieldProperty.LogicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)LogicalDataFieldType.DateTimeExpression,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_2", DbType.Byte, (byte)LogicalDataFieldType.StringExpression,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_4", DbType.Byte, (byte)LogicalDataFieldType.DigitExpression,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;
            }

            return whereConditons;
        }

        /// <summary>
        /// 根据字段类型条件获得字段条件
        /// </summary>
        /// <param name="tableId"></param>
        /// <param name="dataFieldFilter"></param>
        /// <returns></returns>
        public static IList<WhereConditon> GetWhereConditons(decimal tableId, DataFieldFilter dataFieldFilter)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();

            whereConditons.Add(new WhereConditon("TableId", "TableId", DbType.Decimal, tableId, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
            switch (dataFieldFilter)
            {
                case DataFieldFilter.SystemDataFieldAndPhysicalDataField:
                case DataFieldFilter.OnlyPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    break;

                case DataFieldFilter.DateTimeTypeInPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                       (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("BasedDataType", "BasedDataType_0", DbType.Byte, (byte)BasedDataType.DateTime,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));;
                    break;

                case DataFieldFilter.DigtalTypeInPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("BasedDataType", "BasedDataType_0", DbType.Byte, (byte)BasedDataType.Int32,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("BasedDataType", "BasedDataType_1", DbType.Byte, (byte)BasedDataType.Decimal,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataFieldFilter.EnumTypeInPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_0", DbType.Byte, (byte)PhysicalDataFieldType.DropdownListEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)PhysicalDataFieldType.DropdownListEnumValue,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_2", DbType.Byte, (byte)PhysicalDataFieldType.DropdownListFstAdditionalCode,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_3", DbType.Byte, (byte)PhysicalDataFieldType.DropdownListScdAdditionalCode,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_4", DbType.Byte, (byte)PhysicalDataFieldType.TreeViewEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_5", DbType.Byte, (byte)PhysicalDataFieldType.TreeViewEnumValue,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_6", DbType.Byte, (byte)PhysicalDataFieldType.TreeViewFstAdditionalCode,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_7", DbType.Byte, (byte)PhysicalDataFieldType.TreeViewScdAdditionalCode,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_8", DbType.Byte, (byte)PhysicalDataFieldType.CscadeEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_9", DbType.Byte, (byte)PhysicalDataFieldType.MultiSelectedEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataFieldFilter.PrimaryAssociationInPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)PhysicalDataFieldType.PrimaryAssociation,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    break;

                case DataFieldFilter.OneDimCodeTypeInPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_0", DbType.Byte, (byte)PhysicalDataFieldType.Int32,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)PhysicalDataFieldType.Decimal,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_2", DbType.Byte, (byte)PhysicalDataFieldType.NumeralString,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_3", DbType.Byte, (byte)PhysicalDataFieldType.CharString,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_4", DbType.Byte, (byte)PhysicalDataFieldType.MixedString,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataFieldFilter.OnlyDepartmentPhysicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                        (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_0", DbType.Byte, (byte)PhysicalDataFieldType.DepartmentDropdownListEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)PhysicalDataFieldType.DepartmentTreeViewEnum,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;

                case DataFieldFilter.PhysicalFieldAndKeyLogicalField:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty_0", DbType.Byte,
                       (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty_1", DbType.Byte,
                       (byte)DataFieldProperty.LogicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)LogicalDataFieldType.DateTimeExpression,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_2", DbType.Byte, (byte)LogicalDataFieldType.StringExpression,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_4", DbType.Byte, (byte)LogicalDataFieldType.DigitExpression,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 3));
                    break;

                case DataFieldFilter.Attachement:
                    whereConditons.Add(new WhereConditon("DataFieldProperty", "DataFieldProperty", DbType.Byte,
                       (byte)DataFieldProperty.PhysicalDataField, DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_0", DbType.Byte, (byte)PhysicalDataFieldType.DocAttachment,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.And, DataFieldBracket.LeftBracket, 1));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_1", DbType.Byte, (byte)PhysicalDataFieldType.PDFAttachment,
    DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    whereConditons.Add(new WhereConditon("DataFieldType", "DataFieldType_2", DbType.Byte, (byte)PhysicalDataFieldType.PicAttachment,
                        DataFieldCondition.Equal, DataFieldInnerRealtion.Or, DataFieldBracket.RightBracket, 1));
                    break;
            }

            return whereConditons;
        }

        /// <summary>
        /// 获取设置默认值和取值范围的高度
        /// </summary>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public static int GetControlHeightWithDefaultAndRangedValue(PhysicalDataFieldType physicalDataFieldType)
        {
            /* 默认值，不包含取值范围控件的高度 */
            int height = 80;

            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.Int32:
                case PhysicalDataFieldType.Decimal:
                case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                case PhysicalDataFieldType.YearAndMonthAndDay:
                case PhysicalDataFieldType.YearAndMonth:
                case PhysicalDataFieldType.MonthAndDay:
                case PhysicalDataFieldType.Time:
                    height = 150;
                    break;
            }

            return height;
        }

        /// <summary>
        /// 获得系统字段对应的节点对象
        /// </summary>
        /// <param name="systemDataField"></param>
        /// <returns></returns>
        public static CommonNode GetSystemDataField(SystemDataField systemDataField)
        {
            CommonNode commonNode = null;

            commonNode = UserEnumHelper.GetCommonNode(systemDataField);
            commonNode.NodeType = (byte)DataFieldProperty.SystemPhysicalDataField;

            return commonNode;
        }

        /// <summary>
        /// 获得字段属性的通用节点
        /// </summary>
        /// <param name="dataFieldProperty"></param>
        /// <returns></returns>
        public static CommonNode GetDataFieldPropertyCommonNode(DataFieldProperty dataFieldProperty)
        {
            CommonNode commonNode = new CommonNode(0, 0, UserEnumHelper.GetEnumText(dataFieldProperty), string.Empty, false);

            return commonNode;
        }

        /// <summary>
        /// 获得系统字段名称
        /// </summary>
        /// <param name="systemDataFieldName"></param>
        /// <returns></returns>
        public static SystemDataField GetSystemDataField(string systemDataFieldName)
        {
            SystemDataField systemDataField = SystemDataField.UserName;
            switch (systemDataFieldName.ToLower())
            {
                case "username":
                    systemDataField = SystemDataField.UserName;
                    break;

                case "useractualname":
                    systemDataField = SystemDataField.UserActualName;
                    break;

                case "usertypename":
                    systemDataField = SystemDataField.UserTypeName;
                    break;

                case "usertypecode":
                    systemDataField = SystemDataField.UserTypeCode;
                    break;

                case "depname":
                    systemDataField = SystemDataField.DepName;
                    break;

                case "depcode":
                    systemDataField = SystemDataField.DepCode;
                    break;

                case "depvalue":
                    systemDataField = SystemDataField.DepValue;
                    break;

                case "firstcode":
                    systemDataField = SystemDataField.DepFstAdditionalCode;
                    break;

                case "secondcode":
                    systemDataField = SystemDataField.DepScdAdditionalCode;
                    break;

                case "auditedstatus":
                    systemDataField = SystemDataField.AuditedStatus;
                    break;

                case "creationtime":
                    systemDataField = SystemDataField.CreationTime;
                    break;

                case "modificationtime":
                    systemDataField = SystemDataField.ModificationTime;
                    break;

            }
            return systemDataField;
        }

        /// <summary>
        ///获得表的部分系统字段
        /// </summary>
        /// <returns></returns>
        public static IList<CommonNode> GetSystemDataFieldCommonNodes()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            IList<CommonNode> nodes = UserEnumHelper.GetCommonNodes(typeof(SystemDataField));
            foreach (var node in nodes)
            {
                if (node.NodeId == Convert.ToByte(SystemDataField.UserName) || node.NodeId == Convert.ToByte(SystemDataField.UserTypeName) ||
                    node.NodeId == Convert.ToByte(SystemDataField.DepName) || node.NodeId == Convert.ToByte(SystemDataField.AuditedStatus) ||
                    node.NodeId == Convert.ToByte(SystemDataField.CurrentState) || node.NodeId == Convert.ToByte(SystemDataField.CreationTime) ||
                    node.NodeId == Convert.ToByte(SystemDataField.ModificationTime))
                {
                    commonNodes.Add(node);
                }
            }

            return commonNodes;
        }

        /// <summary>
        /// 根据字段属性枚举条件初始化加载树形结构
        /// </summary>
        /// <param name="dataFieldProperty"></param>
        /// <returns></returns>
        public static IList<CommonNode> GetCommonNodes(DataFieldProperty dataFieldProperty)
        {
            IList<CommonNode> commonNodes = null;

            switch (dataFieldProperty)
            {
                case DataFieldProperty.SystemPhysicalDataField:
                    commonNodes = UserEnumHelper.GetCommonNodes(typeof(BasedDataType));
                    break;

                case DataFieldProperty.PhysicalDataField:
                    commonNodes = GetCommonNodes(typeof(PhysicalDataFieldCategory), typeof(PhysicalDataFieldType));
                    break;

                case DataFieldProperty.LogicalDataField:
                    commonNodes = GetCommonNodes(typeof(LogicalDataFieldCategory), typeof(LogicalDataFieldType));
                    break;
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得系统表名
        /// </summary>
        /// <param name="tablePhysicalName"></param>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static string GetSystemTableName(string tablePhysicalName, SystemDataField systemLogicalDataField)
        {
            string tableName = string.Empty;

            switch (systemLogicalDataField)
            {
                case SystemDataField.AuditedStatus:
                case SystemDataField.UserName:
                case SystemDataField.CreationTime:
                case SystemDataField.ModificationTime:
                case SystemDataField.CurrentState:
                    tableName = tablePhysicalName;
                    break;

                case SystemDataField.UserActualName:
                    tableName = "[Blue].[dbo].[UserAccount]";
                    break;

                case SystemDataField.UserTypeName:
                case SystemDataField.UserTypeCode:
                    tableName = "[Blue].[dbo].[UserType]";
                    break;

                case SystemDataField.DepName:
                case SystemDataField.DepCode:
                case SystemDataField.DepValue:
                case SystemDataField.DepProperty:
                case SystemDataField.DepFstAdditionalCode:
                case SystemDataField.DepScdAdditionalCode:
                    tableName = "[Blue].[dbo].[CustomDepartment]";
                    break;
            }

            return tableName;
        }

        /// <summary>
        /// 获得系统表的结构关系
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static TableLink GetTableLink(string tableName, SystemDataField systemLogicalDataField)
        {
            TableLink tableLink = null;

            switch (systemLogicalDataField)
            {
                case SystemDataField.UserName:
                case SystemDataField.UserActualName:
                    tableLink = new TableLink(tableName, "[Blue].[dbo].[UserAccount]", "UserId", TableJoin.InnerJoin);
                    break;

                case SystemDataField.UserTypeName:
                case SystemDataField.UserTypeCode:
                    tableLink = new TableLink(tableName, "[Blue].[dbo].[UserType]", "UserTypeId", TableJoin.InnerJoin);
                    break;

                case SystemDataField.DepName:
                case SystemDataField.DepCode:
                case SystemDataField.DepValue:
                case SystemDataField.DepProperty:
                case SystemDataField.DepFstAdditionalCode:
                case SystemDataField.DepScdAdditionalCode:              
                    tableLink = new TableLink(tableName, "[Blue].[dbo].[CustomDepartment]", "DepId", TableJoin.InnerJoin);
                    break;
            }

            return tableLink;
        }

        /// <summary>
        /// 获得系统表的名称
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static string GetSystemTablePhysicalName(SystemDataField systemLogicalDataField)
        {
            string physicalName = string.Empty;

            switch (systemLogicalDataField)
            {
                case SystemDataField.UserActualName:
                    physicalName = "[Blue].[dbo].[UserAccount]";
                    break;

                case SystemDataField.UserTypeName:
                case SystemDataField.UserTypeCode:
                    physicalName = "[Blue].[dbo].[UserType]";
                    break;
                    
                case SystemDataField.DepName:
                case SystemDataField.DepCode:
                case SystemDataField.DepValue:
                case SystemDataField.DepProperty:
                case SystemDataField.DepFstAdditionalCode:
                case SystemDataField.DepScdAdditionalCode:
                    physicalName = "[Blue].[dbo].[CustomDepartment]";
                    break;
            }

            return physicalName;
        }

        /// <summary>
        /// 获得链接字段的物理名称
        /// </summary>
        /// <param name="dataFieldRelation"></param>
        /// <returns></returns>
        public static string GetLinkedPhysicalDataFieldName(DataFieldRelation dataFieldRelation)
        {
            string physicalName = string.Empty;

            switch (dataFieldRelation)
            {
                case DataFieldRelation.BuinessId:
                    physicalName = GetSystemPhysicalDataFieldName(SystemPhysicalDataField.BusinessId);
                    break;

                case DataFieldRelation.UserId:
                    physicalName = GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
                    break;

                case DataFieldRelation.UserType:
                    physicalName = GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserTypeId);
                    break;

                case DataFieldRelation.Dpeartment:
                    physicalName = GetSystemPhysicalDataFieldName(SystemPhysicalDataField.DepId);
                    break;
            }

            return physicalName;
        }

        /// <summary>
        /// 获得系统物理字段的名称
        /// 系统物理字段：业务表下所有的系统字段
        /// </summary>
        /// <param name="systemPhysicalDataField"></param>
        /// <returns></returns>
        public static string GetAdditionalPhysicalDataFieldName(AdditionalPhysicalDataField additionalPhysicalDataField)
        {
            string physicalName = string.Empty;
            switch (additionalPhysicalDataField)
            {
                case AdditionalPhysicalDataField.BusinessStatus:
                    physicalName = "BusinessStatus";
                    break;

                case AdditionalPhysicalDataField.BusinessAlternativeId:
                    physicalName = "BusinessAlternativeId";
                    break;

                case AdditionalPhysicalDataField.BusinessAlternativeStatus:
                    physicalName = "BusinessAlternativeStatus";
                    break;

                case AdditionalPhysicalDataField.BusinessVisible:
                    physicalName = "BusinessVisible";
                    break;

                case AdditionalPhysicalDataField.BusinessTime:
                    physicalName = "BusinessTime";
                    break;
            }

            return physicalName;
        }

        /// <summary>
        /// 获得系统物理字段的逻辑名称
        /// 系统物理字段：业务表下所有的系统字段
        /// </summary>
        /// <param name="systemPhysicalDataField"></param>
        /// <returns></returns>
        public static string GetLogicalName(SystemPhysicalDataField systemPhysicalDataField)
        {
            string physicalName = string.Empty;
            switch (systemPhysicalDataField)
            {
                case SystemPhysicalDataField.RecordId:
                    physicalName = "字段编号";
                    break;

                case SystemPhysicalDataField.UserId:
                    physicalName = "用户编号";
                    break;

                case SystemPhysicalDataField.UserName:
                    physicalName = "用户名";
                    break;

                case SystemPhysicalDataField.DepId:
                    physicalName = "单位名称";
                    break;

                case SystemPhysicalDataField.UserTypeId:
                    physicalName = "用户类型";
                    break;

                case SystemPhysicalDataField.BusinessId:
                    physicalName = "业务编号";
                    break;

                case SystemPhysicalDataField.BusinessForeignId:
                    physicalName = "业务外键编号";
                    break;

                case SystemPhysicalDataField.BusinessAlternativeId:
                    physicalName = "业务替换编号";
                    break;

                case SystemPhysicalDataField.RecordSorting:
                    physicalName = "排序";
                    break;

                case SystemPhysicalDataField.AuditedStatus:
                    physicalName = "审核状态";
                    break;

                case SystemPhysicalDataField.CurrentState:
                    physicalName = "当前状态";
                    break;

                case SystemPhysicalDataField.CreationTime:
                    physicalName = "创建时间";
                    break;

                case SystemPhysicalDataField.ModificationTime:
                    physicalName = "修改时间";
                    break;

                case SystemPhysicalDataField.ModifiedByUserName:
                    physicalName = "修改人";
                    break;

                case SystemPhysicalDataField.IsDeleted:
                    physicalName = "删除状态";
                    break;
            }

            return physicalName;
        }

        /// <summary>
        /// 获得系统物理字段的名称
        /// 系统物理字段：业务表下所有的系统字段
        /// </summary>
        /// <param name="systemPhysicalDataField"></param>
        /// <returns></returns>
        public static string GetSystemPhysicalDataFieldName(SystemPhysicalDataField systemPhysicalDataField)
        {
            string physicalName = string.Empty;
            switch (systemPhysicalDataField)
            {
                case SystemPhysicalDataField.RecordId:
                    physicalName = "RecordId";
                    break;

                case SystemPhysicalDataField.UserId:
                    physicalName = "UserId";
                    break;

                case SystemPhysicalDataField.UserName:
                    physicalName = "UserName";
                    break;

                case SystemPhysicalDataField.DepId:
                    physicalName = "DepId";
                    break;

                case SystemPhysicalDataField.UserTypeId:
                    physicalName = "UserTypeId";
                    break;

                case SystemPhysicalDataField.BusinessId:
                    physicalName = "BusinessId";
                    break;

                case SystemPhysicalDataField.BusinessForeignId:
                    physicalName = "BusinessForeignId";
                    break;

                case SystemPhysicalDataField.BusinessAlternativeId:
                    physicalName = "BusinessAlternativeId";
                    break;
                    
                case SystemPhysicalDataField.RecordSorting:
                    physicalName = "RecordSorting";
                    break;

                case SystemPhysicalDataField.AuditedStatus:
                    physicalName = "AuditedStatus";
                    break;

                case SystemPhysicalDataField.CurrentState:
                    physicalName = "CurrentState";
                    break;

                case SystemPhysicalDataField.CreationTime:
                    physicalName = "CreationTime";
                    break;

                case SystemPhysicalDataField.ModificationTime:
                    physicalName = "ModificationTime";
                    break;

                case SystemPhysicalDataField.ModifiedByUserName:
                    physicalName = "ModifiedByUserName";
                    break;

                case SystemPhysicalDataField.IsDeleted:
                    physicalName = "IsDeleted";
                    break;
            }

            return physicalName;
        }

        /// <summary>
        /// 获得系统字段的名称
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static string GetPhysicalDataFieldCaption(SystemPhysicalDataField systemPhysicalDataField)
        {
            string text = string.Empty;

            switch (systemPhysicalDataField)
            {
                case SystemPhysicalDataField.RecordId:
                    text = "记录编号";
                    break;

                case SystemPhysicalDataField.UserId:
                    text = "用户编号";
                    break;

                case SystemPhysicalDataField.UserName:
                    text = "用户名";
                    break;

                case SystemPhysicalDataField.DepId:
                    text = "单位编号";
                    break;

                case SystemPhysicalDataField.UserTypeId:
                    text = "用户类型编号";
                    break;

                case SystemPhysicalDataField.BusinessId:
                    text = "业务编号";
                    break;

            }

            return text;
        }

        /// <summary>
        /// 获得系统字段的名称
        /// 系统字段：该系统字段用户可见，一部分来自业务表的系统字段，另外一部分来自系统表的系统字段
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static string GetSystemLogicalDataFieldName(SystemDataField systemLogicalDataField)
        {
            return GetSystemLogicalDataFieldName(string.Empty, systemLogicalDataField);
        }

        /// <summary>
        /// 获得系统字段的名称
        /// 系统字段：该系统字段用户可见，一部分来自业务表的系统字段，另外一部分来自系统表的系统字段
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static string GetOnlySystemLogicalDataFieldName(SystemDataField systemLogicalDataField)
        {
            string physicalName = string.Empty;

            switch (systemLogicalDataField)
            {
                case SystemDataField.UserName:
                    physicalName = "UserName";
                    break;

                case SystemDataField.UserActualName:
                    physicalName = "UserActualName";
                    break;

                case SystemDataField.UserTypeName:
                    physicalName = "UserTypeName";
                    break;

                case SystemDataField.UserTypeCode:
                    physicalName = "UserTypeCode";
                    break;

                case SystemDataField.DepName:
                    physicalName = "DepName";
                    break;
                    
                case SystemDataField.DepProperty:
                    physicalName = "DepartmentProperty";
                    break;

                case SystemDataField.DepCode:
                    physicalName = "DepCode";
                    break;

                case SystemDataField.DepValue:
                    physicalName = "DepValue";
                    break;

                case SystemDataField.DepFstAdditionalCode:
                    physicalName = "FirstCode";
                    break;

                case SystemDataField.DepScdAdditionalCode:
                    physicalName = "SecondCode";
                    break;

                case SystemDataField.CurrentState:
                    physicalName = "CurrentState";
                    break;

                case SystemDataField.AuditedStatus:
                    physicalName = "AuditedStatus";
                    break;

                case SystemDataField.CreationTime:
                    physicalName = "CreationTime";
                    break;

                case SystemDataField.ModificationTime:
                    physicalName = "ModificationTime";
                    break;
            }

            return physicalName;
        }

        /// <summary>
        /// 获得系统字段的名称
        /// 系统字段：该系统字段用户可见，一部分来自业务表的系统字段，另外一部分来自系统表的系统字段
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static string GetLogicalDataFieldName(SystemDataField systemLogicalDataField)
        {
            string physicalName = string.Empty;

            switch (systemLogicalDataField)
            {
                case SystemDataField.UserName:
                    physicalName = "用户名";
                    break;

                case SystemDataField.UserActualName:
                    physicalName = "用户姓名";
                    break;

                case SystemDataField.UserTypeName:
                    physicalName = "用户类型名称";
                    break;

                case SystemDataField.UserTypeCode:
                    physicalName = "用户类型编码";
                    break;

                case SystemDataField.DepName:
                    physicalName = "单位名称";
                    break;

                case SystemDataField.DepCode:
                    physicalName = "单位编码";
                    break;

                case SystemDataField.DepValue:
                    physicalName = "单位值";
                    break;

                case SystemDataField.DepProperty:
                    physicalName = "单位属性";
                    break; 

                case SystemDataField.DepFstAdditionalCode:
                    physicalName = "单位附加编码一";
                    break;

                case SystemDataField.DepScdAdditionalCode:
                    physicalName = "单位附加编码二";
                    break;

                case SystemDataField.CurrentState:
                    physicalName = "当前状态";
                    break;

                case SystemDataField.AuditedStatus:
                    physicalName = "审核状态";
                    break;

                case SystemDataField.CreationTime:
                    physicalName = "创建时间";
                    break;

                case SystemDataField.ModificationTime:
                    physicalName = "修改时间";
                    break;
            }

            return physicalName;
        }

        /// <summary>
        /// 获得系统字段的名称
        /// 系统字段：该系统字段用户可见，一部分来自业务表的系统字段，另外一部分来自系统表的系统字段
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static string GetSystemPhysicalDataFieldName(string tableName, SystemDataField systemLogicalDataField)
        {
            string physicalName = string.Empty;

            switch (systemLogicalDataField)
            {
                case SystemDataField.UserName:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.UserName", tableName);
                    }
                    else
                    {
                        physicalName = "UserName";
                    }
                    break;

                case SystemDataField.UserActualName:
                    physicalName = "UserActualName";
                    break;

                case SystemDataField.UserTypeName:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.UserTypeId", tableName);
                    }
                    else
                    {
                        physicalName = "UserTypeId";
                    }
                    break;

                case SystemDataField.UserTypeCode:
                    physicalName = "UserTypeCode";
                    break;

                case SystemDataField.DepName:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.DepId", tableName);
                    }
                    else
                    {
                        physicalName = "DepId";
                    }
                    break;

                case SystemDataField.DepCode:
                    physicalName = "DepCode";
                    break;

                case SystemDataField.DepValue:
                    physicalName = "DepValue";
                    break;

                case SystemDataField.DepProperty:
                    physicalName = "DepartmentProperty";
                    break;

                case SystemDataField.DepFstAdditionalCode:
                    physicalName = "[CustomDepartment].[FirstCode]";
                    break;

                case SystemDataField.DepScdAdditionalCode:
                    physicalName = "[CustomDepartment].[SecondCode]";
                    break;

                case SystemDataField.AuditedStatus:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.AuditedStatus", tableName);
                    }
                    else
                    {
                        physicalName = "AuditedStatus";
                    }
                    break;

                case SystemDataField.CurrentState:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.CurrentState", tableName);
                    }
                    else
                    {
                        physicalName = "CurrentState";
                    }
                    break;

                case SystemDataField.CreationTime:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.CreationTime", tableName);
                    }
                    else
                    {
                        physicalName = "CreationTime";
                    }
                    break;

                case SystemDataField.ModificationTime:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.ModificationTime", tableName);
                    }
                    else
                    {
                        physicalName = "ModificationTime";
                    }
                    break;
            }

            return physicalName;
        }

        /// <summary>
        /// 获得系统字段的名称
        /// 系统字段：该系统字段用户可见，一部分来自业务表的系统字段，另外一部分来自系统表的系统字段
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static string GetSystemLogicalDataFieldName(string tableName, SystemDataField systemLogicalDataField)
        {
            string physicalName = string.Empty;

            switch (systemLogicalDataField)
            {
                case SystemDataField.UserName:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.UserName", tableName);
                    }
                    else
                    {
                        physicalName = "UserName";
                    }
                    break;

                case SystemDataField.UserActualName:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = "[UserAccount].[UserActualName]";
                    }
                    else
                    {
                        physicalName = "UserActualName";
                    }
                    break;

                case SystemDataField.UserTypeName:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = "[UserType].[UserTypeName]";
                    }
                    else
                    {
                        physicalName = "UserTypeName";
                    }
                    break;

                case SystemDataField.UserTypeCode:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = "[UserType].[UserTypeCode]";
                    }
                    else
                    {
                        physicalName = "UserTypeCode";
                    }
                    break;

                case SystemDataField.DepName:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = "[CustomDepartment].[DepName]";
                    }
                    else
                    {
                        physicalName = "DepName";
                    }
                    break;

                case SystemDataField.DepCode:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = "[CustomDepartment].[DepCode]";
                    }
                    else
                    {
                        physicalName = "DepCode";
                    }
                    break;
                    
                case SystemDataField.DepValue:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = "[CustomDepartment].[DepValue]";
                    }
                    else
                    {
                        physicalName = "DepValue";
                    }
                    break;

                case SystemDataField.DepProperty:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = "[CustomDepartment].[DepartmentProperty]";
                    }
                    else
                    {
                        physicalName = "DepartmentProperty";
                    }
                    break;

                case SystemDataField.DepFstAdditionalCode:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = "[CustomDepartment].[FirstCode]";
                    }
                    else
                    {
                        physicalName = "FirstCode";
                    }
                    break;

                case SystemDataField.DepScdAdditionalCode:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = "[CustomDepartment].[SecondCode]";
                    }
                    else
                    {
                        physicalName = "SecondCode";
                    }
                    break;

                case SystemDataField.AuditedStatus:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.AuditedStatus", tableName);
                    }
                    else
                    {
                        physicalName = "AuditedStatus";
                    }
                    break;

                case SystemDataField.CurrentState:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.CurrentState", tableName);
                    }
                    else
                    {
                        physicalName = "CurrentState";
                    }
                    break;

                case SystemDataField.CreationTime:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.CreationTime", tableName);
                    }
                    else
                    {
                        physicalName = "CreationTime";
                    }
                    break;

                case SystemDataField.ModificationTime:
                    if (!string.IsNullOrWhiteSpace(tableName))
                    {
                        physicalName = string.Format("{0}.ModificationTime", tableName);
                    }
                    else
                    {
                        physicalName = "ModificationTime";
                    }
                    break;
            }

            return physicalName;
        }

        /// <summary>
        /// 获得物理表的名称列表
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="systemLogicalDataFields"></param>
        /// <returns></returns>
        public static IList<TableLink> GetSystemTableLinks(string tableName, Int64 systemLogicalDataFields)
        {
            IList<TableLink> tableLinks = new List<TableLink>();

            if (systemLogicalDataFields > 0)
            {
                Dictionary<string, TableLink> dicTableLinks = new Dictionary<string, TableLink>();
                IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemDataField));
                foreach (var obj in enumItems)
                {
                    if (obj.Value > 0 && (((systemLogicalDataFields >> obj.Value) & 1L) == 1L))
                    {
                        SystemDataField systemDataField = (SystemDataField)obj.Value;
                        /* 用户名从业务表中直接取 */
                        if (systemDataField == SystemDataField.UserName) continue;
                        string systemTablePhysicalName = DataFieldHelper.GetSystemTablePhysicalName(systemDataField);
                        if (!string.IsNullOrWhiteSpace(systemTablePhysicalName) && !dicTableLinks.ContainsKey(systemTablePhysicalName))
                        {
                            dicTableLinks.Add(systemTablePhysicalName, DataFieldHelper.GetTableLink(tableName, systemDataField));
                        }
                    }
                }
                foreach (KeyValuePair<string, TableLink> keyValue in dicTableLinks)
                {
                    tableLinks.Add(keyValue.Value);
                }
            }

            return tableLinks;
        }

        /// <summary>
        /// 获得物理表的名称列表
        /// </summary>
        /// <param name="systemLogicalDataFields"></param>
        /// <returns></returns>
        public static IList<string> GetSystemTablePhysicalNames(Int64 systemLogicalDataFields)
        {           
            IList<string> systemTablePhysicalNames = new List<string>();

            IList<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemDataField));
            foreach (var obj in enumItems)
            {
                if (obj.Value > 0 && (((systemLogicalDataFields >> obj.Value) & 1L) == 1L))
                {
                    SystemDataField systemDataField = (SystemDataField)obj.Value;                   
                    string tableName = GetSystemTablePhysicalName(systemDataField);
                    if (!systemTablePhysicalNames.Contains(tableName))
                    {
                        systemTablePhysicalNames.Add(tableName);
                    }
                }
            }

            return systemTablePhysicalNames;
        }

        /// <summary>
        /// 系统字段是否是物理字段
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static bool IsPhysicalDataField(SystemDataField systemLogicalDataField)
        {
            bool result = false;

            if (systemLogicalDataField == SystemDataField.AuditedStatus || systemLogicalDataField == SystemDataField.CreationTime 
                || systemLogicalDataField == SystemDataField.ModificationTime)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 是否是数据单元格
        /// </summary>
        /// <param name="cellType"></param>
        /// <returns></returns>
        public static bool IsDataCell(QueryReportType queryReportType, byte cellType)
        {
            bool result = false;

            switch(queryReportType)
            {
                case QueryReportType.Basic:
                    BasicCellType basicCellType = (BasicCellType)cellType;
                    if (basicCellType == BasicCellType.OnlyData || basicCellType == BasicCellType.ExtendRow 
                        || basicCellType == BasicCellType.ExtendCol)
                    {
                        result = true;
                    }
                    break;

                case QueryReportType.Statistics:
                    StatisticCellType statisticCellType = (StatisticCellType)cellType;
                    if (statisticCellType == StatisticCellType.OnlyData || statisticCellType == StatisticCellType.Detail
                        || statisticCellType == StatisticCellType.OnlyValue)
                    {
                        result = true;
                    }
                    break;
            }

            return result;
        }

        /// <summary>
        /// 获得转换后的物理字段类型
        /// </summary>
        /// <param name="basedDataType"></param>
        /// <returns></returns>
        public static PhysicalDataFieldType GetPhysicalDataFieldType(BasedDataType basedDataType)
        {
            PhysicalDataFieldType physicalDataFieldType = PhysicalDataFieldType.ArbitraryString;

            switch (basedDataType)
            {
                case BasedDataType.Boolean:
                    physicalDataFieldType = PhysicalDataFieldType.Boolean;
                    break;

                case BasedDataType.Int32:
                    physicalDataFieldType = PhysicalDataFieldType.Int32;
                    break;

                case BasedDataType.String:
                    physicalDataFieldType = PhysicalDataFieldType.ArbitraryString;
                    break;

                case BasedDataType.Decimal:
                    physicalDataFieldType = PhysicalDataFieldType.Decimal;
                    break;

                case BasedDataType.DateTime:
                    physicalDataFieldType = PhysicalDataFieldType.YearAndMonthAndDay;
                    break;
            }

            return physicalDataFieldType;
        }

        /// <summary>
        /// 获取逻辑字段的字段类型枚举
        /// </summary>
        /// <param name="logicalDataFieldType"></param>
        /// <returns></returns>
        public static BasedDataType GetBasedDataType(LogicalDataFieldType logicalDataFieldType)
        {
            BasedDataType dataFieldBase = BasedDataType.Boolean;

            switch (logicalDataFieldType)
            {
                case LogicalDataFieldType.DigitExpression:
                    dataFieldBase = BasedDataType.Decimal;
                    break;

                case LogicalDataFieldType.StringExpression:
                    dataFieldBase = BasedDataType.String;
                    break;

                case LogicalDataFieldType.DateTimeExpression:
                    dataFieldBase = BasedDataType.DateTime;
                    break;

                default:
                    throw new ArgumentException("不支持该逻辑字段类型");
            }

            return dataFieldBase;
        }

        /// <summary>
        /// 获取物理字段的字段类型枚举
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static BasedDataType GetBasedDataType(PhysicalDataFieldType physicalDataFieldType)
        {
            BasedDataType dataFieldBase = BasedDataType.Boolean;

            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.Boolean:
                    dataFieldBase = BasedDataType.Boolean;
                    break;

                case PhysicalDataFieldType.Int32:
                case PhysicalDataFieldType.FstAdditionalInteger:
                case PhysicalDataFieldType.ScdAdditionalInteger:                
                    dataFieldBase = BasedDataType.Int32;
                    break;

                case PhysicalDataFieldType.Decimal:
                case PhysicalDataFieldType.FstAdditionalDecimal:
                case PhysicalDataFieldType.ScdAdditionalDecimal:               
                    dataFieldBase = BasedDataType.Decimal;
                    break;

                case PhysicalDataFieldType.ArbitraryString:
                case PhysicalDataFieldType.ExtendedArbitraryString:
                case PhysicalDataFieldType.NumeralString:
                case PhysicalDataFieldType.CharString:
                case PhysicalDataFieldType.MixedString:
                case PhysicalDataFieldType.EncryptedString:
                    dataFieldBase = BasedDataType.String;
                    break;

                case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                case PhysicalDataFieldType.YearAndMonthAndDay:
                case PhysicalDataFieldType.YearAndMonth:
                case PhysicalDataFieldType.MonthAndDay:
                case PhysicalDataFieldType.Time:
                    dataFieldBase = BasedDataType.DateTime;
                    break;

                case PhysicalDataFieldType.MultiSelectedEnum:
                case PhysicalDataFieldType.DropdownListEnum:
                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                case PhysicalDataFieldType.CscadeEnum:
                case PhysicalDataFieldType.DepartmentDropdownListEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                case PhysicalDataFieldType.EnumValue:
                case PhysicalDataFieldType.EnumNameDependency:
                case PhysicalDataFieldType.FstAdditionalCode:
                case PhysicalDataFieldType.ScdAdditionalCode:
                case PhysicalDataFieldType.FstAdditionalString:
                case PhysicalDataFieldType.ScdAdditionalString:
                case PhysicalDataFieldType.TrdAdditionalString:
                case PhysicalDataFieldType.FourthAdditionalString:
                case PhysicalDataFieldType.FifthAdditionalString:
                case PhysicalDataFieldType.SixthAdditionalString:
                case PhysicalDataFieldType.DepartmentValue:
                case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                case PhysicalDataFieldType.DocAttachment:
                case PhysicalDataFieldType.PicAttachment:
                case PhysicalDataFieldType.PDFAttachment:
                    dataFieldBase = BasedDataType.String;
                    break;

                    //case PhysicalDataFieldType.PrimaryAssociation:
                    //case PhysicalDataFieldType.SecondaryAssociation:
                    //    sb.Append(" Decimal(8, 0) ");
                    //    break;
            }

            return dataFieldBase;
        }

        /// <summary>
        /// 获取系统字段的字段类型枚举
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static BasedDataType GetBasedDataType(SystemDataField systemLogicalDataField)
        {
            BasedDataType dataFieldBase = BasedDataType.Boolean;

            switch (systemLogicalDataField)
            {
                case SystemDataField.UserName:
                case SystemDataField.UserActualName:
                case SystemDataField.UserTypeCode:
                case SystemDataField.DepCode:
                case SystemDataField.DepValue:
                case SystemDataField.UserTypeName:
                case SystemDataField.DepName:
                    dataFieldBase = BasedDataType.String;
                    break;

                case SystemDataField.AuditedStatus:
                case SystemDataField.CurrentState:
                case SystemDataField.DepProperty:
                    dataFieldBase = BasedDataType.Int32;
                    break;

                case SystemDataField.CreationTime:
                case SystemDataField.ModificationTime:
                    dataFieldBase = BasedDataType.DateTime;
                    break;
            }

            return dataFieldBase;
        }

        /// <summary>
        /// 返回对应的数据类型
        /// </summary>
        /// <param name="basedDataType"></param>
        /// <returns></returns>
        public static DbType GetDbType(BasedDataType basedDataType)
        {
            DbType dbType = DbType.Object;

            switch (basedDataType)
            {
                case BasedDataType.Boolean:
                    dbType = DbType.Boolean;
                    break;

                case BasedDataType.DateTime:
                    dbType = DbType.DateTime;
                    break;

                case BasedDataType.Decimal:
                    dbType = DbType.Decimal;
                    break;

                case BasedDataType.Int32:
                    dbType = DbType.Int32;
                    break;

                case BasedDataType.String:
                    dbType = DbType.String;
                    break;
            }

            return dbType;
        }        

        /// <summary>
        /// 获得新的编码
        /// </summary>
        /// <param name="parentCode"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static string GetNewCode(string parentCode, int index)
        {
            string newCode = string.Empty;

            if (index.ToString().Length == 1)
            {
                newCode = string.Format("{0}00{1}", parentCode, index);
            }
            else if (index.ToString().Length == 2)
            {
                newCode = string.Format("{0}0{1}", parentCode, index);
            }
            else if (index.ToString().Length == 3)
            {
                newCode = string.Format("{0}{1}", parentCode, index);
            }

            return newCode;
        }

        /// <summary>
        /// 文本转换为 bool 值
        /// </summary>
        /// <param name="text"></param>
        /// <param name="allowNull"></param>
        /// <returns></returns>
        public static bool GetBoolFormText(string text, bool allowNull)
        {
            bool result = false;

            if (allowNull && string.IsNullOrWhiteSpace(text))
            {
                return result;
            }

            if (text.Equals("1") || text.ToLower().Equals("true"))
            {
                result = true;
            }
            else if (text.ToLower().Equals("false") || text.Equals("0"))
            {
                result = false;
            }
            else
            {
                throw new ArgumentException("文本错误。");
            }

            return result;
        }

        /// <summary>
        /// 从文本中获得对象
        /// </summary>
        /// <param name="text"></param>
        /// <param name="basedDataType"></param>
        /// <returns></returns>
        public static object GetObjectFormText(string text, BasedDataType basedDataType)
        {
            object obj = null;

            switch (basedDataType)
            {
                case BasedDataType.Boolean:
                    if (text.Equals("1") || text.ToLower().Equals("true"))
                    {
                        obj = true;
                    }
                    else if (text.ToLower().Equals("false") || text.Equals("0"))
                    {
                        obj = false;
                    }
                    break;

                case BasedDataType.DateTime:
                    obj = DataConvertionHelper.GetConvertedDateTime(text);
                    break;

                case BasedDataType.Decimal:
                    obj = DataConvertionHelper.GetConvertedDecimal(text);
                    break;

                case BasedDataType.Int32:
                    obj = DataConvertionHelper.GetConvertedInt(text);
                    break;

                case BasedDataType.String:
                    obj = text;
                    break;
            }

            return obj;
        }

        /// <summary>
        /// 返回对应的数据类型
        /// </summary>
        /// <param name="basedDataType"></param>
        /// <returns></returns>
        public static Type GetType(BasedDataType basedDataType)
        {
            Type type = null;

            switch (basedDataType)
            {
                case BasedDataType.Boolean:
                    type = typeof(Boolean);
                    break;

                case BasedDataType.DateTime:
                    type = typeof(DateTime);
                    break;

                case BasedDataType.Decimal:
                    type = typeof(Decimal);
                    break;

                case BasedDataType.Int32:
                    type = typeof(Int32);
                    break;

                case BasedDataType.String:
                    type = typeof(String);
                    break;
            }

            return type;
        }

        /// <summary>
        /// 返回对应的数据类型
        /// </summary>
        /// <param name="basedDataType"></param>
        /// <returns></returns>
        public static DbType GetDataType(BasedDataType basedDataType)
        {
            DbType type = DbType.Object;

            switch (basedDataType)
            {
                case BasedDataType.Boolean:
                    type = DbType.Boolean;
                    break;

                case BasedDataType.DateTime:
                    type = DbType.DateTime;
                    break;

                case BasedDataType.Decimal:
                    type = DbType.Decimal;
                    break;

                case BasedDataType.Int32:
                    type = DbType.Int32;
                    break;

                case BasedDataType.String:
                    type = DbType.String;
                    break;
            }

            return type;
        }

        /// <summary>
        /// 获取系统物理字段的字段类型字符串
        /// </summary>
        /// <param name="systemPhysicalDataField"></param>
        /// <returns></returns>
        public static string GetTypeString(SystemPhysicalDataField systemPhysicalDataField)
        {
            string type = "System.Object";

            switch (systemPhysicalDataField)
            {
                case SystemPhysicalDataField.UserName:
                case SystemPhysicalDataField.ModifiedByUserName:
                    type = "System.String";
                    break;

                case SystemPhysicalDataField.RecordId:
                case SystemPhysicalDataField.UserId:
                case SystemPhysicalDataField.DepId:
                case SystemPhysicalDataField.UserTypeId:
                case SystemPhysicalDataField.BusinessId:
                case SystemPhysicalDataField.BusinessForeignId:
                case SystemPhysicalDataField.BusinessAlternativeId:
                    type = "System.Decimal";
                    break;

                case SystemPhysicalDataField.RecordSorting:
                    type = "System.Int32";
                    break;

                case SystemPhysicalDataField.AuditedStatus:
                case SystemPhysicalDataField.CurrentState:
                    type = "System.Byte";
                    break;

                case SystemPhysicalDataField.CreationTime:
                case SystemPhysicalDataField.ModificationTime:
                    type = "System.DateTime";
                    break;

                case SystemPhysicalDataField.IsDeleted:
                    type = "System.Boolean";
                    break;
            }

            return type;
        }

        /// <summary>
        /// 获取物理字段的字段类型枚举
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static string GetTypeString(PhysicalDataFieldType physicalDataFieldType)
        {
            string type = "System.Object";

            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.Boolean:
                    type = "System.Boolean";
                    break;

                case PhysicalDataFieldType.Int32:
                case PhysicalDataFieldType.FstAdditionalInteger:
                case PhysicalDataFieldType.ScdAdditionalInteger:
                    type = "System.Int32";
                    break;

                case PhysicalDataFieldType.Decimal:
                case PhysicalDataFieldType.FstAdditionalDecimal:
                case PhysicalDataFieldType.ScdAdditionalDecimal:
                    type = "System.Decimal";
                    break;

                case PhysicalDataFieldType.ArbitraryString:
                case PhysicalDataFieldType.ExtendedArbitraryString:
                case PhysicalDataFieldType.NumeralString:
                case PhysicalDataFieldType.CharString:
                case PhysicalDataFieldType.MixedString:
                case PhysicalDataFieldType.EncryptedString:
                    type = "System.String";
                    break;

                case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                case PhysicalDataFieldType.YearAndMonthAndDay:
                case PhysicalDataFieldType.YearAndMonth:
                case PhysicalDataFieldType.MonthAndDay:
                case PhysicalDataFieldType.Time:
                    type = "System.DateTime";
                    break;

                case PhysicalDataFieldType.MultiSelectedEnum:
                case PhysicalDataFieldType.DropdownListEnum:
                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                case PhysicalDataFieldType.CscadeEnum:
                case PhysicalDataFieldType.DepartmentDropdownListEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                case PhysicalDataFieldType.EnumValue:
                case PhysicalDataFieldType.EnumNameDependency:
                case PhysicalDataFieldType.FstAdditionalCode:
                case PhysicalDataFieldType.ScdAdditionalCode:
                case PhysicalDataFieldType.FstAdditionalString:
                case PhysicalDataFieldType.ScdAdditionalString:
                case PhysicalDataFieldType.TrdAdditionalString:
                case PhysicalDataFieldType.FourthAdditionalString:
                case PhysicalDataFieldType.FifthAdditionalString:
                case PhysicalDataFieldType.SixthAdditionalString:
                case PhysicalDataFieldType.DepartmentValue:
                case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                case PhysicalDataFieldType.DocAttachment:
                case PhysicalDataFieldType.PicAttachment:
                case PhysicalDataFieldType.PDFAttachment:
                    type = "System.String";
                    break;

                    //case PhysicalDataFieldType.PrimaryAssociation:
                    //case PhysicalDataFieldType.SecondaryAssociation:
                    //    sb.Append(" Decimal(8, 0) ");
                    //    break;
            }

            return type;
        }

        /// <summary>
        /// 获得逻辑字段的数据类型
        /// </summary>
        /// <param name="logicalDataFieldType"></param>
        /// <returns></returns>
        public static DbType GetDbType(LogicalDataFieldType logicalDataFieldType)
        {
            DbType type = DbType.Object;

            switch (logicalDataFieldType)
            {
                case LogicalDataFieldType.DateTimeExpression:
                    type = DbType.DateTime;
                    break;

                case LogicalDataFieldType.DigitExpression:
                    type = DbType.Decimal;
                    break;

                case LogicalDataFieldType.StringExpression:
                case LogicalDataFieldType.UserName:
                    type = DbType.String;
                    break;
            }

            return type;
        }

        /// <summary>
        /// 获得数据类型
        /// </summary>
        /// <param name="dataFieldProperty"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        public static DbType GetDbType(DataFieldProperty dataFieldProperty, byte dataFieldType)
        {
            DbType type = DbType.Object;

            switch (dataFieldProperty)
            {
                case DataFieldProperty.SystemPhysicalDataField:
                    SystemPhysicalDataField systemPhysicalDataField = (SystemPhysicalDataField)dataFieldType;
                    type = GetDbType(systemPhysicalDataField);
                    break;

                case DataFieldProperty.PhysicalDataField:
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)dataFieldType;
                    type = GetDbType(physicalDataFieldType);
                    break;

                case DataFieldProperty.LogicalDataField:
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)dataFieldType;
                    type = GetDbType(logicalDataFieldType);
                    break;
            }

            return type;
        }

        /// <summary>
        /// 获取系统物理字段的字段类型
        /// </summary>
        /// <param name="systemPhysicalDataField"></param>
        /// <returns></returns>
        public static DbType GetDbType(SystemPhysicalDataField systemPhysicalDataField)
        {
            DbType type = DbType.Object;

            switch (systemPhysicalDataField)
            {
                case SystemPhysicalDataField.UserName:
                case SystemPhysicalDataField.ModifiedByUserName:
                    type = DbType.String;
                    break;

                case SystemPhysicalDataField.RecordId:
                case SystemPhysicalDataField.UserId:
                case SystemPhysicalDataField.DepId:
                case SystemPhysicalDataField.UserTypeId:
                case SystemPhysicalDataField.BusinessId:
                case SystemPhysicalDataField.BusinessForeignId:
                case SystemPhysicalDataField.BusinessAlternativeId:
                    type = DbType.Decimal;
                    break;

                case SystemPhysicalDataField.RecordSorting:
                    type = DbType.Int32;
                    break;

                case SystemPhysicalDataField.AuditedStatus:
                case SystemPhysicalDataField.CurrentState:
                    type = DbType.Byte;
                    break;

                case SystemPhysicalDataField.CreationTime:
                case SystemPhysicalDataField.ModificationTime:
                    type = DbType.DateTime;
                    break;

                case SystemPhysicalDataField.IsDeleted:
                    type = DbType.Boolean;
                    break;
            }

            return type;
        }

        /// <summary>
        /// 获取物理字段的字段类型枚举
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static DbType GetDbType(PhysicalDataFieldType physicalDataFieldType)
        {
            DbType type = DbType.Object;

            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.Boolean:
                    type = DbType.Boolean;
                    break;

                case PhysicalDataFieldType.Int32:
                case PhysicalDataFieldType.FstAdditionalInteger:
                case PhysicalDataFieldType.ScdAdditionalInteger:
                    type = DbType.Int32;
                    break;

                case PhysicalDataFieldType.Decimal:
                case PhysicalDataFieldType.FstAdditionalDecimal:
                case PhysicalDataFieldType.ScdAdditionalDecimal:
                    type = DbType.Decimal;
                    break;

                case PhysicalDataFieldType.ArbitraryString:
                case PhysicalDataFieldType.ExtendedArbitraryString:
                case PhysicalDataFieldType.NumeralString:
                case PhysicalDataFieldType.CharString:
                case PhysicalDataFieldType.MixedString:
                case PhysicalDataFieldType.EncryptedString:
                    type = DbType.String;
                    break;

                case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                case PhysicalDataFieldType.YearAndMonthAndDay:
                case PhysicalDataFieldType.YearAndMonth:
                case PhysicalDataFieldType.MonthAndDay:
                case PhysicalDataFieldType.Time:
                    type = DbType.DateTime;
                    break;

                case PhysicalDataFieldType.MultiSelectedEnum:
                case PhysicalDataFieldType.DropdownListEnum:
                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                case PhysicalDataFieldType.CscadeEnum:
                case PhysicalDataFieldType.DepartmentDropdownListEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                case PhysicalDataFieldType.EnumValue:
                case PhysicalDataFieldType.EnumNameDependency:
                case PhysicalDataFieldType.FstAdditionalCode:
                case PhysicalDataFieldType.ScdAdditionalCode:
                case PhysicalDataFieldType.FstAdditionalString:
                case PhysicalDataFieldType.ScdAdditionalString:
                case PhysicalDataFieldType.TrdAdditionalString:
                case PhysicalDataFieldType.FourthAdditionalString:
                case PhysicalDataFieldType.FifthAdditionalString:
                case PhysicalDataFieldType.SixthAdditionalString:
                case PhysicalDataFieldType.DepartmentValue:
                case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    type = DbType.String;
                    break;

                case PhysicalDataFieldType.DocAttachment:
                case PhysicalDataFieldType.PicAttachment:
                case PhysicalDataFieldType.PDFAttachment:
                    type = DbType.Object;
                    break;

                case PhysicalDataFieldType.PrimaryAssociation:
                case PhysicalDataFieldType.SecondaryAssociation:
                    type = DbType.Xml;
                    break;

                default:
                    throw new ArgumentException("不支持该物理字段类型");
            }

            return type;
        }

        /// <summary>
        /// 获取系统字段的字段类型枚举
        /// </summary>
        /// <param name="systemLogicalDataField"></param>
        /// <returns></returns>
        public static DbType GetDbType(SystemDataField systemLogicalDataField)
        {
            DbType type = DbType.Object;

            switch (systemLogicalDataField)
            {
                case SystemDataField.UserName:
                case SystemDataField.UserActualName:
                case SystemDataField.UserTypeCode:
                case SystemDataField.DepCode:
                case SystemDataField.DepValue:
                case SystemDataField.UserTypeName:
                case SystemDataField.DepName:
                    type = DbType.String;
                    break;

                case SystemDataField.AuditedStatus:
                case SystemDataField.CurrentState:
                case SystemDataField.DepProperty:
                    type = DbType.Int32;
                    break;

                case SystemDataField.CreationTime:
                case SystemDataField.ModificationTime:
                    type = DbType.DateTime;
                    break;
            }

            return type;
        }
        /// <summary>
        /// 通过数据类型获得类型字符串
        /// </summary>
        /// <param name="dataFieldBase"></param>
        /// <param name="dataFieldLength"></param>
        /// <returns></returns>
        public static string GetDataTypeString(BasedDataType dataFieldBase, int dataFieldLength)
        {
            StringBuilder sb = new StringBuilder();

            switch (dataFieldBase)
            {
                case BasedDataType.Boolean:
                    sb.Append(" Bit ");
                    break;

                case BasedDataType.Int32:
                    sb.Append(" Int ");
                    break;

                case BasedDataType.Decimal:
                    sb.Append(" Decimal(8, 2) ");
                    break;

                case BasedDataType.String:
                    sb.Append(" nVarChar(");
                    sb.Append(dataFieldLength);
                    sb.Append(") ");
                    break;

                case BasedDataType.DateTime:
                    sb.Append(" DateTime ");
                    break;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 通过数据类型获得控件宽度
        /// </summary>
        /// <param name="logicalDataFieldType"></param>
        /// <returns></returns>
        public static int GetControlWidth(LogicalDataFieldType logicalDataFieldType)
        {
            int width = 100;

            switch (logicalDataFieldType)
            {               
                case LogicalDataFieldType.DigitExpression:
                    width = 200;
                    break;
                    
                case LogicalDataFieldType.DateTimeExpression:                    
                case LogicalDataFieldType.StringExpression:
                case LogicalDataFieldType.OneDimCode:
                case LogicalDataFieldType.TwoDimCode:
                case LogicalDataFieldType.UserName:
                    width = 250;
                    break;
            }

            return width;
        }

        /// <summary>
        /// 通过数据类型获得控件宽度
        /// </summary>
        /// <param name="physicalDataFieldType"></param>
        /// <returns>控件宽度</returns>
        public static int GetControlWidth(BasedDataType basedDatType)
        {
            int width = 100;

            switch (basedDatType)
            {
                case BasedDataType.Boolean:
                    break;

                case BasedDataType.Int32:
                    width = 150;
                    break;

                case BasedDataType.Decimal:
                    width = 180;
                    break;

                case BasedDataType.String:
                    width = 230;
                    break;

                case BasedDataType.DateTime:
                    width = 230;
                    break;
            }

            return width;
        }

        /// <summary>
        /// 通过数据类型获得类型字符串
        /// </summary>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="dataFieldLength"></param>
        /// <returns></returns>
        public static string GetDataTypeString(PhysicalDataFieldType physicalDataFieldType, int dataFieldLength)
        {
            StringBuilder sb = new StringBuilder();

            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.Boolean:
                    sb.Append(" Bit ");
                    break;

                case PhysicalDataFieldType.Int32:
                case PhysicalDataFieldType.FstAdditionalInteger:
                case PhysicalDataFieldType.ScdAdditionalInteger:
                    sb.Append(" Int ");
                    break;

                case PhysicalDataFieldType.Decimal:
                case PhysicalDataFieldType.FstAdditionalDecimal:
                case PhysicalDataFieldType.ScdAdditionalDecimal:
                    sb.AppendFormat(" Decimal(12, {0}) ", dataFieldLength);
                    break;

                case PhysicalDataFieldType.ArbitraryString:
                case PhysicalDataFieldType.ExtendedArbitraryString:
                case PhysicalDataFieldType.NumeralString:
                case PhysicalDataFieldType.CharString:
                case PhysicalDataFieldType.MixedString:
                    sb.Append(" nVarChar(");
                    sb.Append(dataFieldLength);
                    sb.Append(") ");
                    break;

                case PhysicalDataFieldType.EncryptedString:
                    sb.Append(" nVarChar(");
                    sb.Append(dataFieldLength * 3);
                    sb.Append(") ");
                    break;

                case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                case PhysicalDataFieldType.YearAndMonthAndDay:
                case PhysicalDataFieldType.YearAndMonth:
                case PhysicalDataFieldType.MonthAndDay:
                case PhysicalDataFieldType.Time:
                    sb.Append(" DateTime ");
                    break;

                case PhysicalDataFieldType.MultiSelectedEnum:
                    sb.Append(" nVarChar(256) ");
                    break;

                case PhysicalDataFieldType.DropdownListEnum:
                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                case PhysicalDataFieldType.CscadeEnum:                             
                case PhysicalDataFieldType.DepartmentDropdownListEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                case PhysicalDataFieldType.EnumValue:
                case PhysicalDataFieldType.EnumNameDependency:
                case PhysicalDataFieldType.FstAdditionalCode:
                case PhysicalDataFieldType.ScdAdditionalCode:
                case PhysicalDataFieldType.FstAdditionalString:
                case PhysicalDataFieldType.ScdAdditionalString:
                case PhysicalDataFieldType.TrdAdditionalString:
                case PhysicalDataFieldType.FourthAdditionalString:
                case PhysicalDataFieldType.FifthAdditionalString:
                case PhysicalDataFieldType.SixthAdditionalString:
                case PhysicalDataFieldType.DepartmentValue:            
                case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    sb.Append(" nVarChar(64) ");
                    break;

                //case PhysicalDataFieldType.PrimaryAssociation:
                //case PhysicalDataFieldType.SecondaryAssociation:
                //    sb.Append(" Decimal(8, 0) ");
                //    break;

                case PhysicalDataFieldType.DocAttachment:
                case PhysicalDataFieldType.PicAttachment:
                case PhysicalDataFieldType.PDFAttachment:
                    sb.Append(" nVarChar(128) ");
                    break;
            }

            return sb.ToString();
        }

        /// <summary>
        /// 是否是枚举依赖类型
        /// </summary>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public static bool IsEnumDependency(PhysicalDataFieldType physicalDataFieldType)
        {
            bool result = false;

            if (physicalDataFieldType == PhysicalDataFieldType.EnumValue || physicalDataFieldType == PhysicalDataFieldType.EnumNameDependency
                || physicalDataFieldType == PhysicalDataFieldType.FstAdditionalCode || physicalDataFieldType == PhysicalDataFieldType.ScdAdditionalCode
                || physicalDataFieldType == PhysicalDataFieldType.FstAdditionalString || physicalDataFieldType == PhysicalDataFieldType.ScdAdditionalString
                || physicalDataFieldType == PhysicalDataFieldType.TrdAdditionalString || physicalDataFieldType == PhysicalDataFieldType.FourthAdditionalString
                || physicalDataFieldType == PhysicalDataFieldType.FifthAdditionalString || physicalDataFieldType == PhysicalDataFieldType.SixthAdditionalString
                || physicalDataFieldType == PhysicalDataFieldType.FstAdditionalInteger || physicalDataFieldType == PhysicalDataFieldType.ScdAdditionalInteger
                || physicalDataFieldType == PhysicalDataFieldType.FstAdditionalDecimal || physicalDataFieldType == PhysicalDataFieldType.ScdAdditionalDecimal)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// 部分物理字段的权限退化到只读
        /// </summary>
        /// <param name="physicalDataFieldType"></param>
        /// <returns></returns>
        public static bool IsReadOnly(PhysicalDataFieldType physicalDataFieldType)
        {
            bool readOnly = false;

            if (physicalDataFieldType == PhysicalDataFieldType.EnumValue || physicalDataFieldType == PhysicalDataFieldType.EnumNameDependency
                || physicalDataFieldType == PhysicalDataFieldType.FstAdditionalCode || physicalDataFieldType == PhysicalDataFieldType.ScdAdditionalCode
                || physicalDataFieldType == PhysicalDataFieldType.FstAdditionalString || physicalDataFieldType == PhysicalDataFieldType.ScdAdditionalString
                || physicalDataFieldType == PhysicalDataFieldType.TrdAdditionalString || physicalDataFieldType == PhysicalDataFieldType.FourthAdditionalString
                || physicalDataFieldType == PhysicalDataFieldType.FifthAdditionalString || physicalDataFieldType == PhysicalDataFieldType.SixthAdditionalString
                || physicalDataFieldType == PhysicalDataFieldType.FstAdditionalInteger || physicalDataFieldType == PhysicalDataFieldType.ScdAdditionalInteger
                || physicalDataFieldType == PhysicalDataFieldType.FstAdditionalDecimal || physicalDataFieldType == PhysicalDataFieldType.ScdAdditionalDecimal
                || physicalDataFieldType == PhysicalDataFieldType.DepartmentValue || physicalDataFieldType == PhysicalDataFieldType.DepartmentFstAdditionalCode
                || physicalDataFieldType == PhysicalDataFieldType.DepartmentScdAdditionalCode || physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
            {
                readOnly = true;
            }

            return readOnly;
        }

        /// <summary>
        /// 通过数据类型获得控件宽度
        /// </summary>
        /// <param name="physicalDataFieldType"></param>
        /// <returns>控件宽度</returns>
        public static int GetControlWidth(PhysicalDataFieldType physicalDataFieldType)
        {
            /* 0表示 不需要设置*/
            int controlWidth = 0;

            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.Boolean:
                    controlWidth = 15;
                    break;
                                    
                case PhysicalDataFieldType.Int32:
                    controlWidth = 180;
                    break;

                case PhysicalDataFieldType.Decimal:
                    controlWidth = 180;
                    break;

                case PhysicalDataFieldType.ArbitraryString:
                case PhysicalDataFieldType.NumeralString:
                case PhysicalDataFieldType.CharString:
                case PhysicalDataFieldType.MixedString:
                case PhysicalDataFieldType.ExtendedArbitraryString:
                case PhysicalDataFieldType.EncryptedString:
                    controlWidth = 250;
                    break;
                    
                case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                case PhysicalDataFieldType.YearAndMonthAndDay:
                case PhysicalDataFieldType.YearAndMonth:
                case PhysicalDataFieldType.MonthAndDay:
                    controlWidth = 250;
                    break;

                case PhysicalDataFieldType.Time:
                    controlWidth = 150;
                    break;

                case PhysicalDataFieldType.DropdownListEnum:
                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.TreeViewEnumValue:
                case PhysicalDataFieldType.CscadeEnum:
                case PhysicalDataFieldType.MultiSelectedEnum:
                case PhysicalDataFieldType.DepartmentDropdownListEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnum:
                case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                    controlWidth = 250;
                    break;

                case PhysicalDataFieldType.EnumValue:
                case PhysicalDataFieldType.EnumNameDependency:
                case PhysicalDataFieldType.FstAdditionalCode:
                case PhysicalDataFieldType.ScdAdditionalCode:
                case PhysicalDataFieldType.FstAdditionalString:
                case PhysicalDataFieldType.ScdAdditionalString:
                case PhysicalDataFieldType.TrdAdditionalString:
                case PhysicalDataFieldType.FourthAdditionalString:
                case PhysicalDataFieldType.FifthAdditionalString:
                case PhysicalDataFieldType.SixthAdditionalString:
                case PhysicalDataFieldType.FstAdditionalInteger:
                case PhysicalDataFieldType.ScdAdditionalInteger:
                case PhysicalDataFieldType.FstAdditionalDecimal:
                case PhysicalDataFieldType.ScdAdditionalDecimal:
                case PhysicalDataFieldType.DepartmentValue:
                case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                case PhysicalDataFieldType.Association:
                case PhysicalDataFieldType.PrimaryAssociation:
                case PhysicalDataFieldType.SecondaryAssociation:
                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    controlWidth = 250;
                    break;

                case PhysicalDataFieldType.DocAttachment:
                case PhysicalDataFieldType.PicAttachment:
                case PhysicalDataFieldType.PDFAttachment:
                    controlWidth = 250;
                    break;
            }

            return controlWidth;
        }
        
        #endregion

        #region 私有方法       

        /// <summary>
        /// 获得字段类型对应的节点集合
        /// </summary>
        /// <param name="categoryType"></param>
        /// <param name="dataFieldType"></param>
        /// <returns></returns>
        private static IList<CommonNode> GetCommonNodes(Type categoryType, Type dataFieldType)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();

            IList<CommonNode> logicalDataFieldCategories = UserEnumHelper.GetCommonNodes(categoryType);
            foreach (CommonNode commonNode in logicalDataFieldCategories)
            {
                commonNode.IsLeaf = false;
                commonNodes.Add(commonNode);
            }
            IList<CommonNode> logicalDataFieldTypes = UserEnumHelper.GetCommonNodes(dataFieldType);
            foreach (CommonNode commonNode in logicalDataFieldTypes)
            {
                foreach (CommonNode node in logicalDataFieldCategories)
                {
                    if (commonNode.NodeId <= node.NodeId)
                    {
                        commonNode.ParentNodeId = node.NodeId;
                        commonNodes.Add(commonNode);
                        break;
                    }
                }
            }

            return commonNodes;
        }

        /// <summary>
        /// 物理字段的数据类型是否兼容
        /// </summary>
        /// <param name="source"></param>
        /// <param name="sourceLength"></param>
        /// <param name="destination"></param>
        /// <param name="destinationLength"></param>
        /// <returns></returns>
        public static bool CheckCompatibility(BasedDataType source, int sourceLength, BasedDataType destination, int destinationLength)
        {
            bool compatible = false;            
            if (source == destination)
            {
                switch (source)
                {
                    case BasedDataType.String:
                    case BasedDataType.Decimal:
                        if (sourceLength <= destinationLength)
                        {
                            compatible = true;
                        }
                        break;

                    default:
                        compatible = true;
                        break;
                }
            }
            else
            {
                if ((source == BasedDataType.Decimal || source == BasedDataType.Int32) && (destination == BasedDataType.String))
                {
                    if (sourceLength <= destinationLength)
                    {
                        compatible = true;
                    }
                }
            }

            return compatible;
        }

        #endregion
    }
}
