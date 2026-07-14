using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.Common
{
    public class RelaitonDataBusiness
    {
        #region 私有变量

        private Dictionary<decimal, DataTable> associationData = null;
        private Dictionary<decimal, CustomEnumInfo> customEnumInfoData = null;
        
        #endregion

        #region 契约接口

        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract = null;
        private readonly ICustomDepartmentContract customDepartmentContract;

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataFieldContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="enumContract"></param>
        /// <param name="astDataFieldContract"></param>
        /// <param name="departmentContract"></param>
        public RelaitonDataBusiness(ICustomDataFieldContract dataFieldContract, ICustomAssociationContract associationContract, ICustomEnumContract enumContract, 
            IAssociatedDataFieldContract astDataFieldContract, ICustomDepartmentContract departmentContract)
        {
            customDataFieldContract = dataFieldContract;
            customAssociationContract = associationContract;
            customEnumContract = enumContract;
            associatedDataFieldContract = astDataFieldContract;
            customDepartmentContract = departmentContract;
            associationData = new Dictionary<decimal, DataTable>();
            customEnumInfoData = new Dictionary<decimal, CustomEnumInfo>();
        }

        /// <summary>
        /// 获得关联数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public DataTable GetAssociationData(decimal associationId)
        {
            DataTable data = null;

            if (associationData.ContainsKey(associationId))
            {
                data = associationData[associationId];
            }
            else
            {
                data = customAssociationContract.GetAssociationData(associationId);
                associationData.Add(associationId, data);
            }

            return data;
        }

        /// <summary>
        /// 获得枚举数据
        /// </summary>
        /// <param name="associationId"></param>
        /// <returns></returns>
        public CustomEnumInfo GetCustomEnumInfoData(decimal enumId)
        {
            CustomEnumInfo customEnumInfo = null;

            if (customEnumInfoData.ContainsKey(enumId))
            {
                customEnumInfo = customEnumInfoData[enumId];
            }
            else
            {
                customEnumInfo = customEnumContract.GetModelInfo(enumId);
                customEnumInfoData.Add(enumId, customEnumInfo);
            }

            return customEnumInfo;
        }

        /// <summary>
        /// 获得多选枚举依赖值
        /// </summary>
        /// <param name="commonNodes"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="relationDataFields"></param>
        /// <returns></returns>
        public CommonDataField GetMultiEnumDependencyValue(IList<CommonNode> commonNodes, decimal dataFieldId, PhysicalDataFieldType physicalDataFieldType, IList<CommonDataField> relationDataFields)
        {
            CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModelInfo(dataFieldId);
            string value = string.Empty;
            if (commonNodes != null && commonNodes.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                PhysicalDataFieldType dataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                foreach (CommonNode commonNode in commonNodes)
                {
                    if (dataFieldType == PhysicalDataFieldType.EnumNameDependency)
                    {
                        value = commonNode.NodeName;
                    }
                    else
                    {
                        CustomEnumInfo customEnumInfo = GetCustomEnumInfoData(commonNode.NodeId);
                        switch (dataFieldType)
                        {
                            case PhysicalDataFieldType.EnumValue:
                                sb.AppendFormat("{0},", customEnumInfo.EnumValue);
                                break;

                            case PhysicalDataFieldType.FstAdditionalCode:
                                sb.AppendFormat("{0},", customEnumInfo.FirstCode);
                                break;

                            case PhysicalDataFieldType.ScdAdditionalCode:
                                sb.AppendFormat("{0},", customEnumInfo.SecondCode);
                                break;

                            case PhysicalDataFieldType.FstAdditionalString:
                                sb.AppendFormat("{0},", customEnumInfo.FstAdditionalString);
                                break;

                            case PhysicalDataFieldType.ScdAdditionalString:
                                sb.AppendFormat("{0},", customEnumInfo.ScdAdditionalString);
                                break;

                            case PhysicalDataFieldType.TrdAdditionalString:
                                sb.AppendFormat("{0},", customEnumInfo.TrdAdditionalString);
                                break;

                            case PhysicalDataFieldType.FourthAdditionalString:
                                sb.AppendFormat("{0},", customEnumInfo.FourthAdditionalString);
                                break;

                            case PhysicalDataFieldType.FifthAdditionalString:
                                sb.AppendFormat("{0},", customEnumInfo.FifthAdditionalString);
                                break;

                            case PhysicalDataFieldType.SixthAdditionalString:
                                sb.AppendFormat("{0},", customEnumInfo.SixthAdditionalString);
                                break;

                            case PhysicalDataFieldType.FstAdditionalInteger:
                                sb.AppendFormat("{0},", DataConvertionHelper.EndowStringOfInt(customEnumInfo.FstAdditionalInteger));
                                break;

                            case PhysicalDataFieldType.ScdAdditionalInteger:
                                sb.AppendFormat("{0},", DataConvertionHelper.EndowStringOfInt(customEnumInfo.ScdAdditionalInteger));
                                break;

                            case PhysicalDataFieldType.FstAdditionalDecimal:
                                sb.AppendFormat("{0},", DataConvertionHelper.EndowStringOfDecimal(customEnumInfo.FstAdditionalDecimal));
                                break;

                            case PhysicalDataFieldType.ScdAdditionalDecimal:
                                sb.AppendFormat("{0},", DataConvertionHelper.EndowStringOfDecimal(customEnumInfo.ScdAdditionalDecimal));
                                break;

                            default:
                                throw new ArgumentException("不支持该枚举类型。");
                        }
                    }
                }
                if (sb.Length > 0)
                {
                    sb.Remove(sb.Length - 1, 1);
                }
                value = sb.ToString();
            }

            DbType dbType = DataFieldHelper.GetDbType(physicalDataFieldType);
            bool relationship = AuthorityHelper.CheckAuthority(customDataFieldInfo.DataFieldSetting, (byte)DataFieldSetting.Correlation);
            if (relationship)
            {
                IList<CommonNode> relationCommonNodes = customDataFieldContract.GetRelationDataFields(dataFieldId);
                foreach (CommonNode relationCommonNode in relationCommonNodes)
                {
                    relationDataFields.Add(new CommonDataField(relationCommonNode.NodeId, relationCommonNode.NodeCode, value, dbType));
                }
            }
            CommonDataField commonDataField = new CommonDataField(customDataFieldInfo.DataFieldId, customDataFieldInfo.PhysicalName, value, dbType);

            return commonDataField;
        }

        /// <summary>
        /// 获得关联的值
        /// </summary>
        /// <param name="recordId"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="associationId"></param>
        /// <param name="relationDataFields"></param>
        /// <returns></returns>
        public CommonDataField GetAssociatedValue(decimal recordId, decimal dataFieldId, decimal associationId, IList<CommonDataField> relationDataFields)
        {
            object value = DBNull.Value;
            CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModelInfo(dataFieldId);
            AssociatedDataFieldInfo associatedDataFieldInfo = associatedDataFieldContract.GetModelInfo(customDataFieldInfo.AssociatedDataFieldId);
            DbType dbType = DataFieldHelper.GetDbType((BasedDataType)associatedDataFieldInfo.BasedDataType);
            if (recordId > 0)
            {
                DataTable data = GetAssociationData(associationId);
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                DataRow[] drs = data.Select(string.Format("{0} = {1} ", recordIdName, recordId));
                if (drs.Length > 0)
                {
                    value = drs[0][associatedDataFieldInfo.PhysicalName];
                }
            }            
            bool relationship = AuthorityHelper.CheckAuthority(customDataFieldInfo.DataFieldSetting, (byte)DataFieldSetting.Correlation);
            if (relationship)
            {
                IList<CommonNode> relationCommonNodes = customDataFieldContract.GetRelationDataFields(dataFieldId);
                foreach (CommonNode relationCommonNode in relationCommonNodes)
                {
                    relationDataFields.Add(new CommonDataField(relationCommonNode.NodeId, relationCommonNode.NodeCode, value, dbType));
                }
            }
            CommonDataField commonDataField = new CommonDataField(customDataFieldInfo.DataFieldId, customDataFieldInfo.PhysicalName, value, dbType);

            return commonDataField;
        }

        /// <summary>
        /// 获得单选枚举依赖值
        /// </summary>
        /// <param name="devExpressGrid"></param>
        /// <param name="dataFieldId"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="relationDataFields"></param>
        /// <returns></returns>
        public CommonDataField GetEnumDependencyValue(CommonNode commonNode, decimal dataFieldId, PhysicalDataFieldType physicalDataFieldType, IList<CommonDataField> relationDataFields)
        {
            CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModelInfo(dataFieldId);
            DataFieldProperty fieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
            string value = string.Empty;
            if (commonNode != null)
            {
                PhysicalDataFieldType dataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.TreeViewEnum:
                    case PhysicalDataFieldType.TreeViewEnumValue:
                    case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    case PhysicalDataFieldType.DropdownListEnum:
                    case PhysicalDataFieldType.DropdownListEnumValue:
                    case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                    case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                        if (dataFieldType == PhysicalDataFieldType.EnumNameDependency)
                        {
                            value = commonNode.NodeName;
                        }
                        else
                        {

                            CustomEnumInfo customEnumInfo = GetCustomEnumInfoData(commonNode.NodeId);
                            switch (dataFieldType)
                            {
                                case PhysicalDataFieldType.EnumValue:
                                    value = customEnumInfo.EnumValue;
                                    break;

                                case PhysicalDataFieldType.FstAdditionalCode:
                                    value = customEnumInfo.FirstCode;
                                    break;

                                case PhysicalDataFieldType.ScdAdditionalCode:
                                    value = customEnumInfo.SecondCode;
                                    break;

                                case PhysicalDataFieldType.FstAdditionalString:
                                    value = customEnumInfo.FstAdditionalString;
                                    break;

                                case PhysicalDataFieldType.ScdAdditionalString:
                                    value = customEnumInfo.ScdAdditionalString;
                                    break;

                                case PhysicalDataFieldType.TrdAdditionalString:
                                    value = customEnumInfo.TrdAdditionalString;
                                    break;

                                case PhysicalDataFieldType.FourthAdditionalString:
                                    value = customEnumInfo.FourthAdditionalString;
                                    break;

                                case PhysicalDataFieldType.FifthAdditionalString:
                                    value = customEnumInfo.FifthAdditionalString;
                                    break;

                                case PhysicalDataFieldType.SixthAdditionalString:
                                    value = customEnumInfo.SixthAdditionalString;
                                    break;

                                case PhysicalDataFieldType.FstAdditionalInteger:
                                    value = DataConvertionHelper.EndowStringOfInt(customEnumInfo.FstAdditionalInteger);
                                    break;

                                case PhysicalDataFieldType.ScdAdditionalInteger:
                                    value = DataConvertionHelper.EndowStringOfInt(customEnumInfo.ScdAdditionalInteger);
                                    break;

                                case PhysicalDataFieldType.FstAdditionalDecimal:
                                    value = DataConvertionHelper.EndowStringOfDecimal(customEnumInfo.FstAdditionalDecimal);
                                    break;

                                case PhysicalDataFieldType.ScdAdditionalDecimal:
                                    value = DataConvertionHelper.EndowStringOfDecimal(customEnumInfo.ScdAdditionalDecimal);
                                    break;

                                default:
                                    throw new ArgumentException("不支持该枚举类型。");
                            }
                        }
                        break;

                    case PhysicalDataFieldType.DepartmentDropdownListEnum:
                    case PhysicalDataFieldType.DepartmentTreeViewEnum:
                    case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                        CustomDepartmentInfo customDepartmentInfo = customDepartmentContract.GetModelInfo(commonNode.NodeId);
                        switch (dataFieldType)
                        {
                            case PhysicalDataFieldType.DepartmentValue:
                                value = customDepartmentInfo.DepValue;
                                break;

                            case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                                value = customDepartmentInfo.FirstCode;
                                break;

                            case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                                value = customDepartmentInfo.SecondCode;
                                break;
                        }
                        break;

                    default:
                        throw new ArgumentException("不支持该枚举类型。");
                }
            }
            DbType dbType = DataFieldHelper.GetDbType(physicalDataFieldType);
            bool relationship = AuthorityHelper.CheckAuthority(customDataFieldInfo.DataFieldSetting, (byte)DataFieldSetting.Correlation);
            if (relationship)
            {
                IList<CommonNode> relationCommonNodes = customDataFieldContract.GetRelationDataFields(dataFieldId);
                foreach (CommonNode relationCommonNode in relationCommonNodes)
                {
                    relationDataFields.Add(new CommonDataField(relationCommonNode.NodeId, relationCommonNode.NodeCode, value, dbType));
                }
            }
            CommonDataField commonDataField = new CommonDataField(customDataFieldInfo.DataFieldId, customDataFieldInfo.PhysicalName, value, dbType);

            return commonDataField;
        }

    }
}
