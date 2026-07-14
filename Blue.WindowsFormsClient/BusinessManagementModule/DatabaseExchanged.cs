//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DatabaseExchanged.cs
// 描述: 数据库导入导出类
// 作者：ChenJie 
// 编写日期：2018/07/29
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarPoint.Win.Spread;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using Blue.CustomLibrary.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    /// <summary>
    /// 数据库导入导出类 IDataExportedInterface
    /// </summary>

    public class DatabaseExchanged : TreeDataExchanged, IDataExportedInterface
    {
        #region  静态变量锁

        static object dataFieldRelationLock = new object();
        static object secondaryCodeRelationLock = new object();
        static object newTableCodesLock = new object();

        #endregion

        #region  私有常量

        /// <summary>
        /// 编码列索引
        /// </summary>
        private const int DATABASE_CODE_COLUMN_INDEX = 1;

        /// <summary>
        /// 部分字符串类型字段的最大长度
        /// </summary>
        private const int MAX_DATA_FIELD_STRING = 256;

        /// <summary>
        /// 导入时数据库编码最大层级，以0作为第一层
        /// </summary>
        private const int MAX_DATABASE_CODE_LEVEL = 3;

        /// <summary>
        /// 导入时数据仓库编码最小层级，以0作为第一层
        /// </summary>
        private const int MIN_DATABASE_CODE_LEVEL = 0;

        /// <summary>
        /// 主表的列名称
        /// </summary>
        private string[] columnCaptionsInFstSheet = new string[]
        {
            "数据库(分类、表)名称",
            "数据库(分类、表)编码",
            "数据表属性",
            "数据表类型",
            "是否系统表",
            "启用单位范围",
            "是否记录日志"
        };

        /// <summary>
        /// 子表的列名称
        /// </summary>
        private string[] columnCaptionsInScdSheet = new string[]
        {
            "字段名称",
            "字段编码",
            "字段类型",
            "字段长度",
            "正则表达式",
            "枚举（主、次关联）编码",
            "枚举（关联）依赖字段编码",
            "是否必填",
            "是否自动完成",
            "是否索引",
            "启用联动字段",
            "角色条件字段",
            "触发器字段",
            "启用帮助",            
            "帮助内容",
            "提示",
            "备注"
        };

        #endregion

        #region  私有变量

        /// <summary>
        /// 字段编码与类型
        /// </summary>
        private Dictionary<int, CommonNode> dataFieldRelation;

        /// <summary>
        /// 次关联编码关系
        /// </summary>
        private Dictionary<string, IList<string>> secondaryCodeRelation;

        /// <summary>
        /// 新的表编码
        /// </summary>
        private IList<string> newTableCodes;

        #endregion

        #region 契约接口

        /// <summary>
        /// 数据库契约
        /// </summary>
        private readonly ICustomDatabaseContract customDatabaseContract;

        /// <summary>
        /// 分组契约
        /// </summary>
        private readonly ICustomCategoryContract customCategoryContract;

        /// <summary>
        /// 表契约
        /// </summary>
        private readonly ICustomTableContract customTableContract;

        /// <summary>
        /// 字段契约
        /// </summary>
        private readonly ICustomDataFieldContract customDataFieldContract;

        /// <summary>
        /// 枚举契约
        /// </summary>
        private readonly ICustomEnumContract customEnumContract;

        /// <summary>
        /// 关联字段契约
        /// </summary>
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;

        #endregion

        #region 接口属性        

        /// <summary>
        /// 固定的列数
        /// </summary>
        public int ColumnCountFixed
        {
            get
            {
                return columnCaptionsInFstSheet.Length;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataExportedName"></param>
        /// <param name="parentId"></param>
        /// <param name="parentCode"></param>
        /// <param name="stringSorted"></param>
        /// <param name="groupContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="dataFieldContract"></param>
        public DatabaseExchanged(string dataExportedName, decimal parentId, string parentCode, string stringSorted, ICustomDatabaseContract databaseContract,
            ICustomCategoryContract categoryContract, ICustomTableContract tableContract, ICustomDataFieldContract dataFieldContract, ICustomEnumContract enumContract,
            IAssociatedDataFieldContract astDataFieldContract) : base(dataExportedName, MIN_DATABASE_CODE_LEVEL, MAX_DATABASE_CODE_LEVEL, parentId, parentCode, 
                DATABASE_CODE_COLUMN_INDEX, stringSorted, true, false, true)
        {
            customDatabaseContract = databaseContract;
            customCategoryContract = categoryContract;
            customTableContract = tableContract;
            customDataFieldContract = dataFieldContract;
            customEnumContract = enumContract;
            associatedDataFieldContract = astDataFieldContract;
            PagingEnabled = false;
            dataFieldRelation = new Dictionary<int, CommonNode>();
            secondaryCodeRelation = new Dictionary<string, IList<string>>();
            newTableCodes = new List<string>();
            HelpContent = "（1）每次只能导入一个数据仓库下的数据库结构；\r\n（2）数据库、分组、表的关系由其编码（前缀）的关系构成。";
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <returns></returns>
        public DataSet GetPageRecord()
        {
            DataSet ds = null;

            int level = parentTreeCode.Length / TREE_CODE_LENGTH - 1; // 当前索引
            if (level >= treeCodeMinLevel && level <= treeCodeMaxLevel)
            {                
                DatabaseNodeType databaseNodeType = GetDatabaseNodeType(level);
                ds = GetDataSetExported(GetDatabaseNodeType(level));
            }
            return ds;
        }

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        public DataSet GetPageRecord(int startPosition, int count, ref int totalCount)
        {
            return null;
        }   

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, IList<string>> GetTemplateColumnCaptions()
        {
            Dictionary<string, IList<string>> templateColumnCaptions = new Dictionary<string, IList<string>>();
            templateColumnCaptions.Add(DataExportedName, columnCaptionsInFstSheet);
            templateColumnCaptions.Add("数据表名称(数据表编码)", columnCaptionsInScdSheet);

            return templateColumnCaptions;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellTexts"></param>
        /// <returns></returns>
        public IList<DataValidationResult> ValidateCellData(int sheetIndex, int rowIndex, IList<string> cellTexts)
        {
            IList<DataValidationResult> results = new List<DataValidationResult>();

            if (sheetIndex == 0)
            {

                /* 主表结构关系： (0) 数据库(分类、表)名称，(1) 数据库(分类、表)编码 (2) 数据表类型 (3) 是否系统表 (4) 是否记录日志 (5) 启用单位范围 */
                /* (0) 数据库(分类、表)名称 */
                string name = cellTexts[0];
                DataImportedError dataImportedError = CheckTreeString(name, false);
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 0, dataImportedError));

                /* (1) 数据库(分类、表)编码 */
                string code = cellTexts[1]; 
                int level = code.Length / TREE_CODE_LENGTH - 1;
                DatabaseNodeType databaseNodeType = GetDatabaseNodeType(level);
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 1, CheckTreeCodeFormat(rowIndex, code)));

                if (databaseNodeType == DatabaseNodeType.Table)
                {
                    /* (2) 数据表属性 */
                    List<EnumItem> tableProperties = UserEnumHelper.GetEnumItems(typeof(TableProperty));
                    EnumItem tableProperty = tableProperties.Find(item => item.Text.Equals(cellTexts[2]));
                    if (tableProperty == null)
                    {
                        results.Add(new DataValidationResult(sheetIndex, rowIndex, 2, DataImportedError.ErrorFormat));
                    }

                    /* (3) 数据表类型 */
                    List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(DataTableType));
                    EnumItem enumItem = enumItems.Find(item => item.Text.Equals(cellTexts[3]));
                    if (enumItem == null)
                    {
                        results.Add(new DataValidationResult(sheetIndex, rowIndex, 3, DataImportedError.ErrorFormat));
                    }

                    /* (4) 是否系统表 */  /* (5) 是否记录日志 */ /* (6) 启用单位范围 */
                    for (int colIndex = 4; colIndex <= 6; colIndex++)
                    {
                        string cellText = cellTexts[colIndex];
                        if (!string.IsNullOrWhiteSpace(cellText) && !cellText.Equals("0") && !cellText.Equals("1") && !cellText.ToLower().Equals("true") && !cellText.ToLower().Equals("false"))
                        {
                            dataImportedError = DataImportedError.ErrorFormat;
                        }
                    }
                }
            }
            else
            {
                /* 0 字段名称 */
                string name = cellTexts[0];
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 0, CheckTreeString(name, false)));

                /* 1 字段编码 */
                string code = cellTexts[1];
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 0, CheckDataFieldCodeFormat(sheetIndex, rowIndex, code)));

                /* 2 字段类型 */
                List<EnumItem> physicalDataFieldTypes = UserEnumHelper.GetEnumItems(typeof(PhysicalDataFieldType));
                EnumItem enumItem = physicalDataFieldTypes.Find(item => item.Text.Equals(cellTexts[2]));
                if (enumItem == null)
                {
                    results.Add(new DataValidationResult(sheetIndex, rowIndex, 2, DataImportedError.ErrorFormat));
                    results.Add(new DataValidationResult(sheetIndex, rowIndex, 3, DataImportedError.ErrorFormat));
                }
                else
                {
                    /* 3 字段长度 */
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)enumItem.Value;
                    if ((physicalDataFieldType == PhysicalDataFieldType.ArbitraryString || physicalDataFieldType == PhysicalDataFieldType.ExtendedArbitraryString
                        || physicalDataFieldType == PhysicalDataFieldType.NumeralString || physicalDataFieldType == PhysicalDataFieldType.CharString
                        || physicalDataFieldType == PhysicalDataFieldType.MixedString || physicalDataFieldType == PhysicalDataFieldType.EncryptedString))
                    {
                        if (!string.IsNullOrWhiteSpace(cellTexts[3]))
                        {
                            if (!UserDataHelper.MatchStringLength(cellTexts[3]))
                            {
                                results.Add(new DataValidationResult(sheetIndex, rowIndex, 3, DataImportedError.DataLenth));
                            }
                        }
                        else
                        {
                            results.Add(new DataValidationResult(sheetIndex, rowIndex, 3, DataImportedError.DataEmpty));
                        }
                    }
                }

                /* 4. 正则表达式 */
                if (!string.IsNullOrWhiteSpace(cellTexts[4]) && cellTexts[4].Length > MAX_DATA_FIELD_STRING)
                {
                    results.Add(new DataValidationResult(sheetIndex, rowIndex, 4, DataImportedError.DataLenth));
                }

                if (enumItem != null)
                {
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)enumItem.Value;
                    switch (physicalDataFieldType)
                    {
                        /* 5 枚举编码 */
                        case PhysicalDataFieldType.DropdownListEnum:
                        case PhysicalDataFieldType.DropdownListEnumValue:
                        case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                        case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                        case PhysicalDataFieldType.TreeViewEnum:
                        case PhysicalDataFieldType.TreeViewEnumValue:
                        case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                        case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                        case PhysicalDataFieldType.CscadeEnum:
                        case PhysicalDataFieldType.MultiSelectedEnum:
                            if (string.IsNullOrWhiteSpace(cellTexts[5]))
                            {
                                results.Add(new DataValidationResult(sheetIndex, rowIndex, 5, DataImportedError.DataEmpty));
                            }
                            else if (!customEnumContract.IsExistIdenticalCode(cellTexts[5]))
                            {
                                results.Add(new DataValidationResult(sheetIndex, rowIndex, 5, DataImportedError.DataNotExisted));
                            }
                            break;

                        /* 5 关联字段编码或者主关联字段编码 */
                        case PhysicalDataFieldType.Association:
                        case PhysicalDataFieldType.PrimaryAssociation:                        
                            if (string.IsNullOrWhiteSpace(cellTexts[5]))
                            {
                                results.Add(new DataValidationResult(sheetIndex, rowIndex, 5, DataImportedError.DataEmpty));
                            }
                            else if (!associatedDataFieldContract.IsExistIdenticalCode(cellTexts[5]))
                            {
                                results.Add(new DataValidationResult(sheetIndex, rowIndex, 5, DataImportedError.DataNotExisted));
                            }
                            break;

                        /* 5 次关联字段编码 6 次关联类型使用：主关联类型字段编码 */
                        case PhysicalDataFieldType.SecondaryAssociation:
                            if (string.IsNullOrWhiteSpace(cellTexts[5]))
                            {
                                results.Add(new DataValidationResult(sheetIndex, rowIndex, 5, DataImportedError.DataEmpty));
                            }
                            else if (!associatedDataFieldContract.IsExistIdenticalCode(cellTexts[5]))
                            {
                                results.Add(new DataValidationResult(sheetIndex, rowIndex, 5, DataImportedError.DataNotExisted));
                            }
                            if (string.IsNullOrWhiteSpace(cellTexts[6]))
                            {
                                results.Add(new DataValidationResult(sheetIndex, rowIndex, 6, DataImportedError.DataEmpty));
                            }
                            else
                            {
                                IEnumerable<int> duplicatedValues = null;
                                lock (dataFieldRelationLock)
                                {
                                    duplicatedValues = dataFieldRelation.Where(obj => (obj.Value.NodeCode.Equals(cellTexts[6]))).Select(obj => obj.Key);
                                }                               
                                if (duplicatedValues.Count() == 0)
                                {
                                    if (customDataFieldContract.IsExistIdenticalCode(cellTexts[6]))
                                    {
                                        CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModelInfoByCode(cellTexts[6]);
                                        if (customDataFieldInfo != null && ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField)
                                            && (PhysicalDataFieldType)customDataFieldInfo.DataFieldType == PhysicalDataFieldType.PrimaryAssociation)
                                        {
                                            string primaryCode = associatedDataFieldContract.GetNodeCodeByNodeId(customDataFieldInfo.AssociatedDataFieldId);
                                            if (!cellTexts[5].Substring(0, cellTexts[5].Length - TREE_CODE_LENGTH).Equals(primaryCode.Substring(0, primaryCode.Length - TREE_CODE_LENGTH)))
                                            {
                                                results.Add(new DataValidationResult(sheetIndex, rowIndex, 6, DataImportedError.DataTypeError));
                                            }
                                        }
                                        else
                                        {
                                            results.Add(new DataValidationResult(sheetIndex, rowIndex, 6, DataImportedError.DataTypeError));
                                        }
                                    }
                                    else
                                    {
                                        results.Add(new DataValidationResult(sheetIndex, rowIndex, 6, DataImportedError.ErrorFormat));
                                    }
                                }
                                else
                                {
                                    PhysicalDataFieldType dataFieldType = PhysicalDataFieldType.Boolean;
                                    lock (dataFieldRelationLock)
                                    {
                                        foreach (var duplicatedValue in duplicatedValues)
                                        {
                                            if (dataFieldRelation[duplicatedValue].IsLeaf)
                                            {
                                                dataFieldType = (PhysicalDataFieldType)dataFieldRelation[duplicatedValue].NodeType;
                                                break;
                                            }
                                        }
                                    }
                                    if (dataFieldType != PhysicalDataFieldType.PrimaryAssociation)
                                    {
                                        results.Add(new DataValidationResult(sheetIndex, rowIndex, 6, DataImportedError.DataTypeError));
                                    }
                                }
                            }
                            break;


                        /* 6 依赖枚举和次关联类型使用：枚举类型字段编码（主关联类型字段编码） */
                        case PhysicalDataFieldType.EnumValue:
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
                            if (string.IsNullOrWhiteSpace(cellTexts[6]))
                            {
                                results.Add(new DataValidationResult(sheetIndex, rowIndex, 6, DataImportedError.DataEmpty));
                            }
                            else
                            {
                                int count = 0;
                                lock (dataFieldRelationLock)
                                {
                                    var duplicatedValues = dataFieldRelation.Where(obj => (obj.Value.NodeCode.Equals(cellTexts[6]))).Select(obj => obj.Key);
                                    count = duplicatedValues.Count();
                                }
                                if (count == 0 && !customDataFieldContract.IsExistIdenticalCode(cellTexts[6]))
                                {
                                    results.Add(new DataValidationResult(sheetIndex, rowIndex, 6, DataImportedError.ErrorFormat));
                                }
                            }
                            break;
                    }
                }

                /* 7 是否必填 8 是否自动完成 9 是否索引 10 启用联动字段 11 角色条件字段 12 触发器字段 13 启用帮助 */
                for (int idx = 7; idx <= 13; idx++)
                {
                    if (!cellTexts[idx].Equals("0") && !cellTexts[idx].Equals("1") && !cellTexts[idx].ToLower().Equals("true") && !cellTexts[idx].ToLower().Equals("false"))
                    {
                        results.Add(new DataValidationResult(sheetIndex, rowIndex, idx, DataImportedError.ErrorFormat));
                    }
                }

                /* 14 帮助内容 15 提示 16 备注 */
                for (int idx = 14; idx <= 16; idx++)
                {
                    if (!string.IsNullOrWhiteSpace(cellTexts[idx]) && cellTexts[idx].Length > MAX_DATA_FIELD_STRING)
                    {
                        results.Add(new DataValidationResult(sheetIndex, rowIndex, idx, DataImportedError.DataLenth));
                    }
                }
            }

            return results;
        }

        /// <summary>
        /// 校验变化后单元格数据（除编码单位格外）
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="cellText"></param>
        /// <returns></returns>
        public DataValidationResult ValidateCellDataChanged(int sheetIndex, int rowIndex, int colIndex, string cellText)
        {
            DataImportedError dataImportedError = DataImportedError.None;

            if (sheetIndex == 0)
            {
                /* 主表结构关系： (0) 数据库(分类、表)名称，(1) 数据库(分类、表)编码 (2) 数据表类型 (3) 是否系统表 (4) 是否记录日志 (5) 启用单位范围 */
                /* (0) 数据库(分类、表)名称 */
                if (colIndex == 0)
                {
                    dataImportedError = CheckTreeString(cellText, false);
                }
                else if (colIndex == 2)
                {
                    /* (2) 数据表属性 */
                    List<EnumItem> tableProperties = UserEnumHelper.GetEnumItems(typeof(TableProperty));
                    EnumItem tableProperty = tableProperties.Find(item => item.Text.Equals(cellText));
                    if (tableProperty == null)
                    {
                        dataImportedError = DataImportedError.ErrorFormat;
                    }
                }
                else if (colIndex == 3)
                {
                    /* (3) 数据表类型 */
                    List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(DataTableType));
                    EnumItem enumItem = enumItems.Find(item => item.Text.Equals(cellText));
                    if (enumItem == null)
                    {
                        dataImportedError = DataImportedError.ErrorFormat;
                    }
                }
                else if (colIndex >= 4 && colIndex <= 6)
                {
                    /* (4) 是否系统表 */
                    if (!string.IsNullOrWhiteSpace(cellText) && !cellText.Equals("0") && !cellText.Equals("1") && !cellText.ToLower().Equals("true") && !cellText.ToLower().Equals("false"))
                    {
                        dataImportedError = DataImportedError.ErrorFormat;
                    }
                }
                else
                {
                    throw new ArgumentException("参数错误，不能检查编码或则其它单元格数据。");
                }
            }
            else
            {
                DataTableCheckedMode = DataTableCheckedMode.None;
                if (colIndex == 0)
                {
                    /* 0 字段名称 */
                    dataImportedError = CheckTreeString(cellText, false);
                    int index = sheetIndex * MAX_COLUMN_COUNT_PER_SHEET + rowIndex;
                    lock (dataFieldRelationLock)
                    {
                        if (dataFieldRelation.ContainsKey(index))
                        {
                            dataFieldRelation[index].NodeName = cellText;
                        }
                    }
                }
                else if (colIndex == 1)
                {
                    /* 1 字段编码 */
                    dataImportedError = CheckDataFieldCodeFormat(sheetIndex, rowIndex, cellText);
                    int index = sheetIndex * MAX_COLUMN_COUNT_PER_SHEET + rowIndex;
                    lock (dataFieldRelationLock)
                    {
                        if (dataFieldRelation.ContainsKey(index))
                        {
                            dataFieldRelation[index].NodeCode = cellText;
                        }
                    }
                }
                else if (colIndex == 2)
                {
                    /* 2 字段类型 */
                    List<EnumItem> physicalDataFieldTypes = UserEnumHelper.GetEnumItems(typeof(PhysicalDataFieldType));
                    EnumItem enumItem = physicalDataFieldTypes.Find(item => item.Text.Equals(cellText));
                    int index = sheetIndex * MAX_COLUMN_COUNT_PER_SHEET + rowIndex;
                    if (enumItem == null)
                    {
                        dataImportedError = DataImportedError.ErrorFormat;
                        lock (dataFieldRelationLock)
                        {
                            dataFieldRelation[index].NodeType = 0;
                            dataFieldRelation[index].IsLeaf = false;
                        }
                    }
                    else
                    {
                        lock (dataFieldRelationLock)
                        {
                            if (dataFieldRelation.ContainsKey(index))
                            {
                                dataFieldRelation[index].NodeType = enumItem.Value;
                                dataFieldRelation[index].IsLeaf = true;
                            }
                        }
                    }
                    DataTableCheckedMode = DataTableCheckedMode.Row;
                }
                else if (colIndex == 3)
                {
                    /* 3 字段长度 */
                    int index = sheetIndex * MAX_COLUMN_COUNT_PER_SHEET + rowIndex;
                    lock (dataFieldRelationLock)
                    {
                        if (dataFieldRelation.ContainsKey(index) && dataFieldRelation[index].IsLeaf)
                        {
                            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)dataFieldRelation[index].NodeType;
                            if ((physicalDataFieldType == PhysicalDataFieldType.ArbitraryString || physicalDataFieldType == PhysicalDataFieldType.ExtendedArbitraryString
                                || physicalDataFieldType == PhysicalDataFieldType.NumeralString || physicalDataFieldType == PhysicalDataFieldType.CharString
                                || physicalDataFieldType == PhysicalDataFieldType.MixedString || physicalDataFieldType == PhysicalDataFieldType.EncryptedString))
                            {
                                if (!string.IsNullOrWhiteSpace(cellText))
                                {
                                    if (!UserDataHelper.MatchStringLength(cellText))
                                    {
                                        dataImportedError = DataImportedError.DataLenth;
                                    }
                                }
                                else
                                {
                                    dataImportedError = DataImportedError.DataEmpty;
                                }
                            }
                        }
                        else
                        {
                            dataImportedError = DataImportedError.ErrorFormat;
                        }
                    }
                }
                else if (colIndex == 4)
                {
                    /* 4. 正则表达式 */
                    if (!string.IsNullOrWhiteSpace(cellText) && cellText.Length > MAX_DATA_FIELD_STRING)
                    {
                        dataImportedError = DataImportedError.DataLenth;
                    }
                }
                else if (colIndex == 5)
                {
                    int index = sheetIndex * MAX_COLUMN_COUNT_PER_SHEET + rowIndex;
                    lock (dataFieldRelationLock)
                    {
                        if (dataFieldRelation.ContainsKey(index) && dataFieldRelation[index].IsLeaf)
                        {
                            PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)dataFieldRelation[index].NodeType;
                            switch (physicalDataFieldType)
                            {
                                /* 枚举编码 */
                                case PhysicalDataFieldType.DropdownListEnum:
                                case PhysicalDataFieldType.DropdownListEnumValue:
                                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                                case PhysicalDataFieldType.TreeViewEnum:
                                case PhysicalDataFieldType.TreeViewEnumValue:
                                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                                case PhysicalDataFieldType.CscadeEnum:
                                case PhysicalDataFieldType.MultiSelectedEnum:
                                    if (string.IsNullOrWhiteSpace(cellText))
                                    {
                                        dataImportedError = DataImportedError.DataEmpty;
                                    }
                                    else if (!customEnumContract.IsExistIdenticalCode(cellText))
                                    {
                                        dataImportedError = DataImportedError.DataNotExisted;
                                    }
                                    break;

                                /* 主、次关联字段编码 */
                                case PhysicalDataFieldType.Association:
                                case PhysicalDataFieldType.PrimaryAssociation:
                                case PhysicalDataFieldType.SecondaryAssociation:
                                    if (string.IsNullOrWhiteSpace(cellText))
                                    {
                                        dataImportedError = DataImportedError.DataEmpty;
                                    }
                                    else if (!associatedDataFieldContract.IsExistIdenticalCode(cellText))
                                    {
                                        dataImportedError = DataImportedError.DataNotExisted;
                                    }
                                    break;
                            }
                        }
                    }
                }
                else if (colIndex == 6)
                {
                    int index = sheetIndex * MAX_COLUMN_COUNT_PER_SHEET + rowIndex;
                    bool exist = false;
                    lock (dataFieldRelationLock)
                    {
                        exist = dataFieldRelation.ContainsKey(index) && dataFieldRelation[index].IsLeaf;
                    }
                    if (exist)
                    {
                        PhysicalDataFieldType physicalDataFieldType = PhysicalDataFieldType.Boolean;
                        lock (dataFieldRelationLock)
                        {
                            physicalDataFieldType = (PhysicalDataFieldType)dataFieldRelation[index].NodeType;
                        }
                        switch (physicalDataFieldType)
                        {
                            /*  依赖枚举和次关联类型使用：枚举类型字段编码（主关联类型字段编码） */
                            case PhysicalDataFieldType.EnumValue:
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
                            case PhysicalDataFieldType.SecondaryAssociation:
                                if (string.IsNullOrWhiteSpace(cellText))
                                {
                                    dataImportedError = DataImportedError.DataEmpty;
                                }
                                else
                                {
                                    int count = 0;
                                    lock (dataFieldRelationLock)
                                    {
                                        var duplicatedValues = dataFieldRelation.Where(obj => (obj.Value.NodeCode.Equals(cellText))).Select(obj => obj.Key);
                                        count = duplicatedValues.Count();
                                    }
                                    if (count == 0)
                                    {
                                        if (!customDataFieldContract.IsExistIdenticalCode(cellText))
                                        {
                                            dataImportedError = DataImportedError.ErrorFormat;
                                        }
                                    }
                                }
                                break;
                        }
                    }
                }
                else if (colIndex >= 7 && colIndex <= 13)
                {
                    /* 7 是否必填 8 是否自动完成 9 是否索引 10 启用联动字段 11 角色条件字段 12 触发器字段 13 启用帮助 */
                    if (cellText.Equals("0") && !cellText.Equals("1") && !cellText.ToLower().Equals("true") && !cellText.ToLower().Equals("false"))
                    {
                        dataImportedError = DataImportedError.ErrorFormat;
                    }

                }
                else if (colIndex >= 14 && colIndex <= 16)
                {
                    /* 14 帮助内容 15 提示 16 备注 */
                    if (!string.IsNullOrWhiteSpace(cellText) && cellText.Length > MAX_DATA_FIELD_STRING)
                    {
                        dataImportedError = DataImportedError.DataLenth;
                    }
                }
            }

            return new DataValidationResult(sheetIndex, rowIndex, colIndex, dataImportedError);
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="cellTexts"></param>
        /// <param name="importedMode"></param>
        /// <returns></returns>
        public decimal ImportData(IList<string> cellTexts, ImportedMode importedMode)
        {
            decimal dataId = decimal.MinValue;

            /* 仅支持下面两种模式 */
            if (importedMode != ImportedMode.UpdateAndInsert && importedMode != ImportedMode.NotUpdateAndInsert)
            {
                throw new ArgumentException("参数错误。");
            }

            try
            {
                string treeCode = cellTexts[DATABASE_CODE_COLUMN_INDEX];
                string name = cellTexts[0];
                string code = cellTexts[1];

                int level = treeCode.Length / TREE_CODE_LENGTH - 1;
                DatabaseNodeType databaseNodeType = GetDatabaseNodeType(level);
                switch (databaseNodeType)
                {
                    case DatabaseNodeType.Database:
                        string parentCode = code.Substring(0, TREE_CODE_LENGTH);
                        byte dataWarehouseId = DataWarehouseHelper.GetDataSourceIdByCode(parentCode);
                        CustomDatabaseInfo customDatabaseInfo = new CustomDatabaseInfo()
                        {
                            DatabaseName = name,
                            DatabaseCode = code,
                            DataWarehouseId = dataWarehouseId,
                            IsLeaf = true
                        };
                        if (!customDatabaseContract.IsExistIdenticalCode(code))
                        {
                            dataId = customDatabaseContract.Insert(customDatabaseInfo);
                        }
                        else
                        {
                            dataId = customDatabaseContract.GetNodeIdByNodeCode(code);
                            if (importedMode == ImportedMode.UpdateAndInsert)
                            {
                                customDatabaseInfo.DatabaseId = dataId;
                                customDatabaseContract.Update(customDatabaseInfo);
                            }
                        }
                        break;

                    case DatabaseNodeType.Category:
                        decimal databaseId = GetParentId(code);
                        CustomCategoryInfo customCategoryInfo = new CustomCategoryInfo()
                        {
                            DatabaseId = databaseId,
                            CategoryName = name,
                            CategoryCode = code,
                            IsLeaf = true
                        };
                        if (!customCategoryContract.IsExistIdenticalCode(code))
                        {
                            dataId = customCategoryContract.Insert(customCategoryInfo);
                        }
                        else
                        {
                            dataId = customCategoryContract.GetNodeIdByNodeCode(code);
                            if (importedMode == ImportedMode.UpdateAndInsert)
                            {
                                customCategoryInfo.CategoryId = dataId;
                                customCategoryContract.Update(customCategoryInfo);
                            }
                        }
                        break;

                    case DatabaseNodeType.Table:
                        decimal categoryId = GetParentId(code);
                        byte tableProperty = UserEnumHelper.GetEnumValue(typeof(TableProperty), cellTexts[2]); 
                        byte tableType = UserEnumHelper.GetEnumValue(typeof(DataTableType), cellTexts[3]);
                        bool systemTable = DataFieldHelper.GetBoolFormText(cellTexts[4], true);
                        long tableSetting = 0;
                        if (DataFieldHelper.GetBoolFormText(cellTexts[5], true))
                        {
                            tableSetting = AuthorityHelper.GetShiftedValue(tableSetting, (byte)TableSetting.DataReserved);
                        }
                        if (DataFieldHelper.GetBoolFormText(cellTexts[6], true))
                        {
                            tableSetting = AuthorityHelper.GetShiftedValue(tableSetting, (byte)TableSetting.Log);
                        }
                        CustomTableInfo customTableInfo = new CustomTableInfo()
                        {
                            CategoryId = categoryId,
                            LogicalName = name,
                            TableCode = code,
                            TableProperty = tableProperty,
                            TableType = tableType > 0 ? tableType : (byte)DataTableType.PrimaryTable,
                            SystemTable = systemTable,
                            TableSetting = tableSetting,
                            Notes = "该表由导入方式生成。"
                        };
                        if (!customTableContract.IsExistIdenticalCode(code))
                        {                            
                            dataId = customTableContract.Insert(customTableInfo);
                            lock (newTableCodesLock)
                            {
                                if (!newTableCodes.Contains(code))
                                {
                                    newTableCodes.Add(code);
                                }
                            }
                        }
                        else
                        {
                            dataId = customTableContract.GetNodeIdByNodeCode(code);
                            lock (newTableCodesLock)
                            {
                                if (newTableCodes.Contains(code))
                                {
                                    newTableCodes.Remove(code);
                                }
                            }
                            if (importedMode == ImportedMode.UpdateAndInsert)
                            {
                                customTableInfo.TableId = dataId;
                                customTableContract.ResetAndUpdateTable(customTableInfo);
                            }
                        }
                        break;
                }
                if (dataId > 0)
                {
                    AddCodeAndId(code, dataId);
                }

            }
            catch (Exception ex)
            {
                int a = 0;
            }

            return dataId;
        }

        /// <summary>
        /// 导入子表数据
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="dataId"></param>
        /// <param name="treeCode"></param>
        /// <param name="sheetView"></param>
        /// <param name="errorDataRowsInSheet"></param>
        public override void ImportDataTable(int sheetIndex, decimal dataId, string treeCode, SheetView sheetView, IList<int> errorDataRowsInSheet)
        {
            try
            {
                if (sheetView.RowCount <= 1)
                {
                    return;
                }
                /* 0. 重置关联表 */
                customTableContract.ResetTable(dataId);

                /* 1.构建字段集合 */
                List<CustomDataFieldInfo> customDataFieldInfos = new List<CustomDataFieldInfo>();
                Dictionary<string, string> enumCodeRelation = new Dictionary<string, string>();
                IList<string> primaryCodes = new List<string>();                
                for (int rowIndex = 1; rowIndex < sheetView.RowCount; rowIndex++)
                {
                    if (errorDataRowsInSheet.Contains(rowIndex))
                    {
                        continue;
                    }
                    decimal enumId = decimal.MinValue;                    
                    decimal parentDataFieldId = decimal.MinValue;
                    decimal associatedDataFieldId = decimal.MinValue;
                    string dataFieldCode = sheetView.Cells[rowIndex, 1].Text.Trim();                    
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)UserEnumHelper.GetEnumValue(typeof(PhysicalDataFieldType), sheetView.Cells[rowIndex, 2].Text.Trim());
                    switch (physicalDataFieldType)
                    {
                        /* 5 枚举编码 */
                        case PhysicalDataFieldType.DropdownListEnum:
                        case PhysicalDataFieldType.DropdownListEnumValue:
                        case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                        case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                        case PhysicalDataFieldType.TreeViewEnum:
                        case PhysicalDataFieldType.TreeViewEnumValue:
                        case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                        case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                        case PhysicalDataFieldType.CscadeEnum:
                        case PhysicalDataFieldType.MultiSelectedEnum:
                            enumId = customEnumContract.GetNodeIdByNodeCode(sheetView.Cells[rowIndex, 5].Text.Trim());
                            break;

                        /* 5 主关联字段编码 */
                        case PhysicalDataFieldType.Association:
                        case PhysicalDataFieldType.PrimaryAssociation:
                            primaryCodes.Add(dataFieldCode);
                            associatedDataFieldId = associatedDataFieldContract.GetNodeIdByNodeCode(sheetView.Cells[rowIndex, 5].Text.Trim());
                            break;

                        case PhysicalDataFieldType.SecondaryAssociation:
                            /* 5 次关联字段编码 */
                            associatedDataFieldId = associatedDataFieldContract.GetNodeIdByNodeCode(sheetView.Cells[rowIndex, 5].Text.Trim());
                            /* 5 次关联类型使用：主关联类型字段编码，
                             * （1）依赖其他表已经存在主关联类型的字段 
                             * （2）等待相同的表(后续的表)中其父字段创建后才能获得父编号 
                             */
                            decimal dataFieldId = customDataFieldContract.GetNodeIdByNodeCode(sheetView.Cells[rowIndex, 6].Text.Trim());
                            if (!DataConvertionHelper.IsNullValue(dataFieldId))
                            {
                                parentDataFieldId = dataFieldId;
                            }
                            else
                            {
                                string primaryCode = sheetView.Cells[rowIndex, 6].Text.Trim();
                                string secondaryCode = sheetView.Cells[rowIndex, 1].Text.Trim();
                                lock (secondaryCodeRelationLock)
                                {
                                    if (secondaryCodeRelation.ContainsKey(primaryCode))
                                    {
                                        secondaryCodeRelation[primaryCode].Add(secondaryCode);
                                    }
                                    else
                                    {
                                        IList<string> secondaryCodes = new List<string>();
                                        secondaryCodes.Add(secondaryCode);
                                        secondaryCodeRelation.Add(primaryCode, secondaryCodes);
                                    }
                                }
                            }
                            break;

                        /* 5 依赖枚举：等待其父字段创建后才能获得其编号 */                        
                        case PhysicalDataFieldType.EnumValue:
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
                            enumCodeRelation.Add(sheetView.Cells[rowIndex, 1].Text.Trim(), sheetView.Cells[rowIndex, 6].Text.Trim());
                            break;                            
                    }
                    /* 11 启用联动字段 */
                    long dataFieldSetting = 0;
                    if (DataFieldHelper.GetBoolFormText(sheetView.Cells[rowIndex, 11].Text.Trim(), true))
                    {
                        dataFieldSetting = AuthorityHelper.GetShiftedValue(dataFieldSetting, (byte)DataFieldSetting.Correlation);
                    }

                    /* 12 启用角色条件字段 */
                    if (DataFieldHelper.GetBoolFormText(sheetView.Cells[rowIndex, 12].Text.Trim(), true))
                    {
                        dataFieldSetting = AuthorityHelper.GetShiftedValue(dataFieldSetting, (byte)DataFieldSetting.RoleUnderCondition);
                    }

                    /* 13 启用触发器字段 */
                    if (DataFieldHelper.GetBoolFormText(sheetView.Cells[rowIndex, 13].Text.Trim(), true))
                    {
                        dataFieldSetting = AuthorityHelper.GetShiftedValue(dataFieldSetting, (byte)DataFieldSetting.TriggerDataFiled);
                    }

                    CustomDataFieldInfo customDataFieldInfo = new CustomDataFieldInfo()
                    {
                        TableId = dataId,
                        LogicalName = sheetView.Cells[rowIndex, 0].Text.Trim(),
                        DataFieldCode = dataFieldCode,
                        DataFieldProperty = (byte)DataFieldProperty.PhysicalDataField,
                        DataFieldType = (byte)physicalDataFieldType,
                        DataFieldLength = DataConvertionHelper.GetConvertedInt(sheetView.Cells[rowIndex, 3].Text.Trim(), 0),
                        RegexExpression = sheetView.Cells[rowIndex, 4].Text.Trim(),
                        EnumId = enumId,
                        AssociatedDataFieldId = associatedDataFieldId,
                        ParentDataFieldId = parentDataFieldId,
                        RequiredDataField = DataFieldHelper.GetBoolFormText(sheetView.Cells[rowIndex, 7].Text.Trim(), true),
                        AutoComplete = DataFieldHelper.GetBoolFormText(sheetView.Cells[rowIndex, 8].Text.Trim(), true),
                        IndexCreated = DataFieldHelper.GetBoolFormText(sheetView.Cells[rowIndex, 9].Text.Trim(), true),
                        DataFieldSetting = dataFieldSetting,
                        HelpEnabled = DataFieldHelper.GetBoolFormText(sheetView.Cells[rowIndex, 10].Text.Trim(), true),
                        HelpContent = sheetView.Cells[rowIndex, 14].Text.Trim(),
                        Tooltip = sheetView.Cells[rowIndex, 15].Text.Trim(),
                        Notes = sheetView.Cells[rowIndex, 16].Text.Trim(),
                        Sorting = rowIndex
                    };
                    customDataFieldInfos.Add(customDataFieldInfo);                    
                }                

                /* 2. 插入数据 */
                customDataFieldContract.Insert(dataId, customDataFieldInfos, enumCodeRelation, secondaryCodeRelation);

                /* 已经创建的主关联类型字段，不用保存依赖该字段的次关联类型字段 */
                foreach (string primaryCode in primaryCodes)
                {
                    lock (secondaryCodeRelationLock)
                    {
                        if (secondaryCodeRelation.ContainsKey(primaryCode))
                        {
                            secondaryCodeRelation.Remove(primaryCode);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                int a = 0;
            }
        }
        
        /// <summary>
        /// 是否允许子表数据导入
        /// </summary>
        /// <param name="treeCode"></param>
        /// <param name="importedMode"></param>
        /// <returns></returns>

        public override bool AllowDataTableImported(string treeCode, ImportedMode importedMode)
        {
            bool result = false;

            /* 仅支持下面两种模式 */
            if (importedMode != ImportedMode.UpdateAndInsert && importedMode != ImportedMode.NotUpdateAndInsert)
            {
                throw new ArgumentException("参数错误。");
            }

            if (!string.IsNullOrWhiteSpace(treeCode) && (treeCode.Length % TREE_CODE_LENGTH == 0))
            {
                int index = treeCode.Length / TREE_CODE_LENGTH - 1; //当前索引
                lock (newTableCodesLock)
                {
                    if ((index == treeCodeMaxLevel) && (importedMode == ImportedMode.UpdateAndInsert
                    || (importedMode == ImportedMode.NotUpdateAndInsert) && newTableCodes.Contains(treeCode)))
                    {
                        result = true;
                    }
                }
            }

            return result;
        }

        #endregion

        #region 重写虚拟化方法

        /// <summary>
        /// 检查树形结构编码格式
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="treeCode"></param>
        /// <returns></returns>
        protected override DataImportedError CheckTreeCodeFormat(int rowIndex, string treeCode)
        {
            DataImportedError dataImportedError = DataImportedError.None;

            if (!string.IsNullOrWhiteSpace(treeCode))
            {
                if (treeCode.Length % TREE_CODE_LENGTH == 0 && UserDataHelper.MatchDigit(treeCode))
                {
                    int level = treeCode.Length / TREE_CODE_LENGTH - 1; // 当前索引
                    if (level >= treeCodeMinLevel && level <= treeCodeMaxLevel)
                    {
                        DatabaseNodeType databaseNodeType = GetDatabaseNodeType(level);
                        switch (databaseNodeType)
                        {
                            case DatabaseNodeType.Database:
                                string dataWarehouseCode = treeCode.Substring(0, treeCode.Length - TREE_CODE_LENGTH);
                                if (!DataWarehouseHelper.ContainsDataSourceCode(dataWarehouseCode))
                                {
                                    dataImportedError = DataImportedError.DataNotExisted;
                                }
                                break;

                            case DatabaseNodeType.Table:
                                if (!dataSheetCodes.Contains(treeCode))
                                {
                                    dataImportedError = DataImportedError.SheetNotExisted;
                                }
                                break;
                        }
                        if (dataImportedError == DataImportedError.None)
                        {
                            /* 检查重复的编码 */
                            var duplicatedValues = treeCodeRelations[level].Where(obj => obj.Value == treeCode).Select(obj => obj.Key);
                            if (duplicatedValues.Count() > 1)
                            {
                                dataImportedError = DataImportedError.DuplicatedData;
                            }
                            else
                            {
                                /* 检查编码的结构关系 */
                                int parentIndex = level - 1;
                                if (parentIndex >= 0 && parentIndex < treeCodeMaxLevel && treeCodeRelations[parentIndex].Count > 0)
                                {
                                    string prefixCode = treeCode.Substring(0, treeCode.Length - TREE_CODE_LENGTH);
                                    if (parentNodeId > 0 && (treeCode.Length == (parentTreeCode.Length + TREE_CODE_LENGTH)))
                                    {
                                        if (!prefixCode.Equals(parentTreeCode))
                                        {
                                            dataImportedError = DataImportedError.ParentEmpty;
                                        }
                                    }
                                    else
                                    {
                                        if (!treeCodeRelations[parentIndex].ContainsValue(prefixCode))
                                        {
                                            dataImportedError = DataImportedError.ParentEmpty;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        dataImportedError = DataImportedError.DataLenth;
                    }
                }
                else
                {
                    dataImportedError = DataImportedError.ErrorFormat;
                }
            }
            else
            {
                dataImportedError = DataImportedError.DataEmpty;
            }

            return dataImportedError;
        }

        /// <summary>
        /// 获得父节点编号
        /// </summary>
        /// <param name="treeCode"></param>
        /// <returns>查找失败则返回0</returns>
        protected override decimal GetParentId(string treeCode)
        {
            decimal parentId = 0;

            if (treeCode.Length > TREE_CODE_LENGTH)
            {
                string code = treeCode.Substring(0, treeCode.Length - TREE_CODE_LENGTH);
                if (treeCodeAndIds.ContainsKey(code))
                {
                    parentId = treeCodeAndIds[code];
                }
                else
                {
                    int level = code.Length / TREE_CODE_LENGTH - 1;
                    DatabaseNodeType databaseNodeType = GetDatabaseNodeType(level);
                    switch (databaseNodeType)
                    {
                        case DatabaseNodeType.Database:
                            parentId = customDatabaseContract.GetNodeIdByNodeCode(code);
                            break;

                        case DatabaseNodeType.Category:
                            parentId = customCategoryContract.GetNodeIdByNodeCode(code);
                            break;

                        case DatabaseNodeType.Table:
                            parentId = customTableContract.GetNodeIdByNodeCode(code);
                            break;
                    }
                }
            }
            else if (treeCode.Length == TREE_CODE_LENGTH)
            {
                parentId = decimal.MinValue;
            }

            return parentId;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 导出数据
        /// </summary>
        /// <param name="databaseNodeType"></param>
        /// <returns></returns>
        private DataSet GetDataSetExported(DatabaseNodeType databaseNodeType)
        {
            DataSet ds = new DataSet();

            byte nodeTypeValue = (byte)databaseNodeType;

            DataSet dsDatabase = null;
            DataSet dsCategory = null;
            DataSet dsTable = null;
            Dictionary<decimal, string> tableInfos = new Dictionary<decimal, string>();
            if (nodeTypeValue == (byte)DatabaseNodeType.Table)
            {
                dsTable = customTableContract.GetPageRecordByTableId(parentNodeId);
            }
            else
            {
                IList<decimal> databaseIds = new List<decimal>();
                if (nodeTypeValue <= (byte)DatabaseNodeType.DataWarehouse)
                {
                    byte dataWarehouseId = DataWarehouseHelper.GetDataSourceIdByCode(parentTreeCode);
                    dsDatabase = customDatabaseContract.GetPageRecord(dataWarehouseId);
                    foreach (DataRow dr in dsDatabase.Tables[0].Rows)
                    {
                        databaseIds.Add(Convert.ToDecimal(dr["DatabaseId"]));
                    }
                    dsDatabase.Tables[0].Columns.Remove("DatabaseId");
                }

                IList<decimal> ctegoryIds = new List<decimal>();
                if (nodeTypeValue <= (byte)DatabaseNodeType.Database)
                {
                    if (databaseNodeType == DatabaseNodeType.Database)
                    {
                        dsCategory = customCategoryContract.GetPageRecord(parentNodeId);
                    }
                    else
                    {
                        dsCategory = customCategoryContract.GetPageRecord(databaseIds);
                    }
                    foreach (DataRow dr in dsCategory.Tables[0].Rows)
                    {
                        ctegoryIds.Add(Convert.ToDecimal(dr["CategoryId"]));
                    }
                    dsCategory.Tables[0].Columns.Remove("CategoryId");
                }

                if (nodeTypeValue <= (byte)DatabaseNodeType.Category)
                {
                    if (databaseNodeType == DatabaseNodeType.Category)
                    {
                        dsTable = customTableContract.GetPageRecord(parentNodeId);
                    }
                    else
                    {
                        dsTable = customTableContract.GetPageRecord(ctegoryIds);
                    }
                }
            }
            dsTable.Tables[0].Columns.Add("TablePropertyValue", Type.GetType("System.String"));
            dsTable.Tables[0].Columns.Add("TableTypeValue", Type.GetType("System.String"));
            dsTable.Tables[0].Columns.Add("DataReserved", Type.GetType("System.Boolean"));
            dsTable.Tables[0].Columns.Add("Log", Type.GetType("System.Boolean"));
            foreach (DataRow dr in dsTable.Tables[0].Rows)
            {
                tableInfos.Add(Convert.ToDecimal(dr["TableId"]), string.Format("{0}({1})", dr["LogicalName"], dr["TableCode"]));
                dr["TablePropertyValue"] = UserEnumHelper.GetEnumText((TableProperty)DataConvertionHelper.GetByte(dr["TableProperty"], 0));
                dr["TableTypeValue"] = UserEnumHelper.GetEnumText((DataTableType)DataConvertionHelper.GetByte(dr["TableType"], 0));
                long tableSetting = DataConvertionHelper.GetLong(dr["TableSetting"], 0);
                dr["DataReserved"] = AuthorityHelper.CheckAuthority(tableSetting, (byte)TableSetting.DataReserved);
                dr["Log"] = AuthorityHelper.CheckAuthority(tableSetting, (byte)TableSetting.Log);
            }
            dsTable.Tables[0].Columns.Remove("TableSetting");
            dsTable.Tables[0].Columns.Remove("TableId");
            dsTable.Tables[0].Columns.Remove("TableProperty");
            dsTable.Tables[0].Columns["TablePropertyValue"].SetOrdinal(dsTable.Tables[0].Columns["TableType"].Ordinal);
            dsTable.Tables[0].Columns["TableTypeValue"].SetOrdinal(dsTable.Tables[0].Columns["TableType"].Ordinal);
            dsTable.Tables[0].Columns.Remove("TableType");

            /* 合并成数据库结构表 */
            IList<DataTable> tables = new List<DataTable>();
            if (dsDatabase != null)
            {
                tables.Add(dsDatabase.Tables[0]);
            }
            if (dsCategory != null)
            {
                tables.Add(dsCategory.Tables[0]);
            }
            DataTable dataTable = DataTableHelper.CombineDataTable(dsTable.Tables[0], tables);
            dataTable.TableName = "数据库结构";
            for (int idx = 0; idx < columnCaptionsInFstSheet.Length; idx++)
            {
                dataTable.Columns[idx].Caption = columnCaptionsInFstSheet[idx];
            }
            foreach (DataRow dr in dataTable.Rows)
            {
                string tableCode = dr["TableCode"].ToString();
                if ((tableCode.Length / TREE_CODE_LENGTH - 1) < treeCodeMaxLevel)
                {
                    /* 对于非数据表的数据行，以下几列的值为空。(2. 数据表类型  3. 是否系统表 4.启用单位范围 5.是否记录日志) */
                    for (int col = 2; col <= 5; col++)
                    {
                        dr[col] = DBNull.Value;
                    }
                }
            }
            ds.Tables.Add(dataTable);
            foreach (var tableInfo in tableInfos)
            {
                DataTable dt = customDataFieldContract.GetPageRecord(tableInfo.Key).Tables[0];

                /* 1 字段类型 */
                dt.Columns.Add("DataFieldTypeValue", Type.GetType("System.String"));
                dt.Columns["DataFieldTypeValue"].SetOrdinal(dt.Columns["DataFieldType"].Ordinal);

                /* 2 字段设置 */
                /* 2.1 角色条件字段*/
                dt.Columns.Add("RoleUnderConditionValue", Type.GetType("System.Boolean"));
                dt.Columns["RoleUnderConditionValue"].SetOrdinal(dt.Columns["DataFieldSetting"].Ordinal);
                /* 2.2 字段联动更新 */
                dt.Columns.Add("CorrelationValue", Type.GetType("System.Boolean"));
                dt.Columns["CorrelationValue"].SetOrdinal(dt.Columns["DataFieldSetting"].Ordinal);
                /* 2.3 字段联动更新 */
                dt.Columns.Add("TriggerDataFiled", Type.GetType("System.Boolean"));
                dt.Columns["TriggerDataFiled"].SetOrdinal(dt.Columns["DataFieldSetting"].Ordinal);

                /* 数据处理 */
                foreach (DataRow dr in dt.Rows)
                {
                    dr["DataFieldTypeValue"] = UserEnumHelper.GetEnumText((PhysicalDataFieldType)DataConvertionHelper.GetByte(dr["DataFieldType"], 0));
                    long dataFieldSetting = DataConvertionHelper.GetLong(dr["DataFieldSetting"], 0);
                    dr["CorrelationValue"] = AuthorityHelper.CheckAuthority(dataFieldSetting, (byte)DataFieldSetting.Correlation);
                    dr["RoleUnderConditionValue"] = AuthorityHelper.CheckAuthority(dataFieldSetting, (byte)DataFieldSetting.RoleUnderCondition);
                    dr["TriggerDataFiled"] = AuthorityHelper.CheckAuthority(dataFieldSetting, (byte)DataFieldSetting.TriggerDataFiled);                    
                }
                dt.Columns.Remove("DataFieldType");
                dt.Columns.Remove("DataFieldSetting");

                for (int idx = 0; idx < columnCaptionsInScdSheet.Length; idx++)
                {                    
                    dt.Columns[idx].Caption = columnCaptionsInScdSheet[idx];
                }
                dt.TableName = tableInfo.Value;
                ds.Tables.Add(dt.Copy());
            }

            return ds;
        }        

        /// <summary>
        /// 检查字段编码格式
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="treeCode"></param>
        /// <returns></returns>
        private DataImportedError CheckDataFieldCodeFormat(int sheetIndex, int rowIndex, string treeCode)
        {
            DataImportedError dataImportedError = DataImportedError.None;

            if (!string.IsNullOrWhiteSpace(treeCode))
            {
                if (treeCode.Length % TREE_CODE_LENGTH == 0 && UserDataHelper.MatchDigit(treeCode))
                {
                    int level = treeCode.Length / TREE_CODE_LENGTH - 1; // 当前索引
                    /* 子表编码层级 */
                    if (level == treeCodeMaxLevel + 1)
                    {
                        /* 检查编码的结构关系 */
                        if (treeCodeRelations[treeCodeMaxLevel].Count > 0)
                        {
                            string prefixCode = treeCode.Substring(0, treeCode.Length - TREE_CODE_LENGTH);
                            if (!treeCodeRelations[treeCodeMaxLevel].ContainsValue(prefixCode))
                            {
                                dataImportedError = DataImportedError.ParentEmpty;
                            }
                        }
                        if(dataImportedError == DataImportedError.None)
                        {
                            int count = 0;
                            lock (dataFieldRelationLock)
                            {
                                var duplicatedValues = dataFieldRelation.Where(obj => obj.Value.NodeCode == treeCode).Select(obj => obj.Key);
                                count = duplicatedValues.Count();
                            }
                            if (count > 1)
                            {
                                dataImportedError = DataImportedError.DuplicatedData;
                            }
                        }                        
                    }
                    else
                    {
                        dataImportedError = DataImportedError.DataLenth;
                    }
                }
                else
                {
                    dataImportedError = DataImportedError.ErrorFormat;
                }
            }
            else
            {
                dataImportedError = DataImportedError.DataEmpty;
            }

            return dataImportedError;
        }

        /// <summary>
        /// 初始化数据子表结构
        /// </summary>
        /// <param name="cellTextsInSheets"></param>
        public override void InitDataTableStruct(FpSpread spread)
        {
            if (spread.Sheets.Count <= 1)
            {
                return;
            }
            dataSheetCodes.Clear();
            lock (dataFieldRelationLock)
            {
                dataFieldRelation.Clear();
            }
            lock (secondaryCodeRelationLock)
            {
                secondaryCodeRelation.Clear();
            }
            lock (newTableCodesLock)
            {
                newTableCodes.Clear();
            }
            List<EnumItem> physicalDataFieldTypes = UserEnumHelper.GetEnumItems(typeof(PhysicalDataFieldType));
            for (int sheetIndex = 1; sheetIndex < spread.Sheets.Count; sheetIndex++)
            {
                for (int rowIndex = 0; rowIndex < spread.Sheets[sheetIndex].RowCount; rowIndex++)
                {
                    int index = sheetIndex * MAX_COLUMN_COUNT_PER_SHEET + rowIndex;
                    string dataFiledName = spread.Sheets[sheetIndex].Cells[rowIndex, 0].Text.Trim();
                    string dataFiledCode = spread.Sheets[sheetIndex].Cells[rowIndex, 1].Text.Trim();
                    string dataFiledTypeName = spread.Sheets[sheetIndex].Cells[rowIndex, 2].Text.Trim();
                    EnumItem enumItem = physicalDataFieldTypes.Find(item => item.Text.Equals(dataFiledTypeName));
                    if (enumItem != null)
                    {
                        CommonNode commonNode = new CommonNode()
                        {
                            NodeName = dataFiledName,
                            NodeCode = dataFiledCode,
                            NodeType = enumItem.Value,
                            IsLeaf = true
                        };
                        lock (dataFieldRelationLock)
                        {
                            dataFieldRelation.Add(index, commonNode);
                        }
                    }
                    else
                    {
                        CommonNode commonNode = new CommonNode()
                        {
                            NodeName = dataFiledName,
                            NodeCode = dataFiledCode,
                            NodeType = 0,
                            IsLeaf = false
                        };
                        lock (dataFieldRelationLock)
                        {
                            dataFieldRelation.Add(index, commonNode);
                        }
                    }
                }
                string sheetName = spread.Sheets[sheetIndex].SheetName;
                string sheetCode = UserDataHelper.ExtractString(sheetName, '(', ')');
                if (!string.IsNullOrWhiteSpace(sheetCode))
                {
                    dataSheetCodes.Add(sheetCode);
                }
            }
        }

        /// <summary>
        /// 获得数据库节点类型
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        private DatabaseNodeType GetDatabaseNodeType(int level)
        {
            DatabaseNodeType nodeType = DatabaseNodeType.DataWarehouse;
            if (level < treeCodeMinLevel || level > treeCodeMaxLevel)
            {
                throw new ArgumentException("参数异常。");
            }

            /* 第一层为数据仓库 第二层为数据库，第三层为分类，第四层为数据表，第五层为字段 */
            switch (level)
            {
                case 0:
                    nodeType = DatabaseNodeType.DataWarehouse;
                    break;

                case 1:
                    nodeType = DatabaseNodeType.Database;
                    break;

                case 2:
                    nodeType = DatabaseNodeType.Category;
                    break;

                case 3:
                    nodeType = DatabaseNodeType.Table;
                    break;

                default:
                    throw new ArgumentException("不支持该参数。");
            }

            return nodeType;
        }

        #endregion
    }
}
