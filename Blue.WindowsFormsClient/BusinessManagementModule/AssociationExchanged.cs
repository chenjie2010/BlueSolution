//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: AssociationExchanged.cs
// 描述: 关联导入导出类
// 作者：ChenJie 
// 编写日期：2018/07/24
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
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    /// <summary>
    /// 关联导入导出类
    /// </summary>
    public class AssociationExchanged : TreeDataExchanged, IDataExportedInterface
    {
        #region  私有常量

        /* 导入关联时，Excel文件包含2列 */
        private const int ASSOCIATION_DATA_IMPORTED_COLUMNS = 2;

        /// <summary>
        /// 编码列索引
        /// </summary>
        private const int ASSOCIATION_CODE_COLUMN_INDEX = 1;

        /* 导入时关联编码最大层级，以0作为第一层 */
        private const int MAX_ASSOCIATION_CODE_LEVEL = 2;

        /* 导入时关联编码最小层级，以0作为第一层 */
        private const int MIN_ASSOCIATION_CODE_LEVEL = 0;

        #endregion

        #region  静态变量锁

        static object newAssociationCodesLock = new object();

        #endregion

        #region 契约接口

        /// <summary>
        /// 分组契约
        /// </summary>
        private readonly ICustomGroupContract customGroupContract;

        /// <summary>
        /// 关联契约
        /// </summary>
        private readonly ICustomAssociationContract customAssociationContract;

        /// <summary>
        /// 关联字段契约
        /// </summary>
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;

        #endregion

        #region  私有变量

        /// <summary>
        /// 新的关联编码
        /// </summary>
        private IList<string> newAssociationCodes;

        /// <summary>
        /// 节点类型
        /// </summary>
        private AssociationNodeType CustomAssociationNodeType;

        #endregion

        #region 接口属性        

        /// <summary>
        /// 固定的列数
        /// </summary>
        public int ColumnCountFixed
        {
            get
            {
                return ASSOCIATION_DATA_IMPORTED_COLUMNS;
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
        /// <param name="associationNodeType"></param>
        /// <param name="groupContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="dataFieldContract"></param>
        public AssociationExchanged(string dataExportedName, decimal parentId, string parentCode, string stringSorted,
            AssociationNodeType associationNodeType, ICustomGroupContract groupContract, ICustomAssociationContract associationContract, IAssociatedDataFieldContract dataFieldContract)
            : base(dataExportedName, MIN_ASSOCIATION_CODE_LEVEL, MAX_ASSOCIATION_CODE_LEVEL, parentId, parentCode,
                  ASSOCIATION_CODE_COLUMN_INDEX, stringSorted, true, false, false)
        {
            customGroupContract = groupContract;
            customAssociationContract = associationContract;
            associatedDataFieldContract = dataFieldContract;
            if (parentId > 0)
            {
                PagingEnabled = false;
            }
            else
            {
                PagingEnabled = true;
            }
            CustomAssociationNodeType = associationNodeType;
            newAssociationCodes = new List<string>();
            HelpContent = "（1）关联分类以及关联的编码最低长度为3位（每级关联编码长度是3的倍数）；\r\n（2）关联的节点以及关联的关系由关联编码（前缀）的关系构成。";
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <returns></returns>
        public DataSet GetPageRecord()
        {
            DataSet dsGroup = null;
            switch (CustomAssociationNodeType)
            {
                case AssociationNodeType.Root:
                case AssociationNodeType.ParentCategory:
                    dsGroup = customGroupContract.GetPageRecord(parentNodeId, GroupType.Association);
                    break;

                case AssociationNodeType.ChildCategory:
                    dsGroup = customGroupContract.GetPageRecord(parentNodeId);
                    break;

                default:
                    throw new ArgumentException("不支持该类型的数据导出。");
            }
            

            return GetGetPageRecord(dsGroup);
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
            DataSet dsGroup = customGroupContract.GetPageRecord(startPosition, count, (byte)GroupType.Association, ref totalCount);

            return GetGetPageRecord(dsGroup);

        }

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, IList<string>> GetTemplateColumnCaptions()
        {
            Dictionary<string, IList<string>> templateColumnCaptions = new Dictionary<string, IList<string>>();
            IList<string> columnCaptionsInFstSheet = new List<string>();
            columnCaptionsInFstSheet.Add("关联分类(关联)名称");
            columnCaptionsInFstSheet.Add("关联分类(关联)编码");
            templateColumnCaptions.Add(DataExportedName, columnCaptionsInFstSheet);

            IList<string> columnCaptionsInScdSheet = new List<string>();
            columnCaptionsInScdSheet.Add("关联字段名称一(字符串类型|主)");
            columnCaptionsInScdSheet.Add("关联字段名称二(实数类型|从)");
            columnCaptionsInScdSheet.Add("关联字段名称三(布尔类型|从)");
            columnCaptionsInScdSheet.Add("关联字段名称四(时间类型|从)");
            columnCaptionsInScdSheet.Add("关联字段名称五(整数类型|从)");
            templateColumnCaptions.Add("关联实例模板", columnCaptionsInScdSheet);

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
                /* 主表结构关系： (0) 关联名称，(1) 关联编码 */
                /* (0) 关联名称 */
                string associatedName = cellTexts[0];
                DataImportedError dataImportedError = CheckTreeString(associatedName, false);
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 0, dataImportedError));

                /* (1) 关联编码 */
                string associationCode = cellTexts[1];
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 1, CheckTreeCodeFormat(rowIndex, associationCode)));
            }
            else
            {
                int colIndex = 0;
                foreach (var cellText in cellTexts)
                {
                    if (rowIndex == 0)
                    {
                        if (!basedDataFieldInfos[sheetIndex - 1].ContainsKey(colIndex))
                        {
                            results.Add(new DataValidationResult(sheetIndex, 0, colIndex, DataImportedError.DataTypeError));
                        }
                    }
                    else
                    {
                        if (basedDataFieldInfos[sheetIndex - 1].ContainsKey(colIndex))
                        {
                            results.Add(new DataValidationResult(sheetIndex, rowIndex, colIndex, CheckDataFormat(cellText, basedDataFieldInfos[sheetIndex - 1][colIndex].DataFieldType)));
                        }
                    }
                    colIndex++;
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
                /* (0) 关联名称 */
                if (colIndex == 0)
                {
                    dataImportedError = CheckTreeString(cellText, false);
                }
                else
                {
                    throw new ArgumentException("参数错误，不能检查编码或则其它单元格数据。");
                }
            }
            else
            {
                DataTableCheckedMode = DataTableCheckedMode.None;
                if (rowIndex == 0)
                {
                    BasedDataFieldInfo oldBasedDataFieldInfo = null;
                    if (BasedDataFieldInfos[sheetIndex - 1].ContainsKey(colIndex))
                    {
                        oldBasedDataFieldInfo = BasedDataFieldInfos[sheetIndex - 1][colIndex];
                    }
                    BasedDataFieldInfo basedDataFieldInfo = GetBasedDataFieldInfo(cellText);
                    if (basedDataFieldInfos[sheetIndex - 1].ContainsKey(colIndex))
                    {
                        if (basedDataFieldInfo != null)
                        {
                            basedDataFieldInfos[sheetIndex - 1][colIndex] = basedDataFieldInfo;
                        }
                        else
                        {
                            basedDataFieldInfos[sheetIndex - 1].Remove(colIndex);
                            dataImportedError = DataImportedError.DataTypeError;
                        }
                    }
                    else
                    {
                        if (basedDataFieldInfo != null)
                        {
                            basedDataFieldInfos[sheetIndex - 1].Add(colIndex, basedDataFieldInfo);
                        }
                        else
                        {
                            dataImportedError = DataImportedError.DataTypeError;
                        }
                    }
                    /* 字段类型由错误变成正确，或者字段类型发生改变,则该列字段都需要重新校验 */
                    if ((oldBasedDataFieldInfo == null && basedDataFieldInfo != null)
                        || (oldBasedDataFieldInfo != null && basedDataFieldInfo != null && oldBasedDataFieldInfo.DataFieldType == basedDataFieldInfo.DataFieldType))
                    {
                        DataTableCheckedMode = DataTableCheckedMode.Col;
                    }
                }
                else
                {
                    if (basedDataFieldInfos[sheetIndex - 1].ContainsKey(colIndex))
                    {
                        dataImportedError = CheckDataFormat(cellText, basedDataFieldInfos[sheetIndex - 1][colIndex].DataFieldType);
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
                string treeCode = cellTexts[ASSOCIATION_CODE_COLUMN_INDEX];
                if ((treeCode.Length / TREE_CODE_LENGTH - 1) == treeCodeMaxLevel)
                {
                    string associationName = cellTexts[0];
                    string associationCode = cellTexts[1];
                    decimal groupId = GetParentId(associationCode);
                    CustomAssociationInfo customAssociationInfo = new CustomAssociationInfo()
                    {
                        GroupId = groupId,
                        AssociationName = associationName,
                        AssociationCode = associationCode,
                        ShowMode = (byte)AssociationShowMode.Table,
                        SuperAssociationEnabled = false,
                        IsLeaf = true
                    };
                    if (groupId > 0)
                    {
                        decimal associationId = decimal.MinValue;
                        if (!customAssociationContract.IsExistIdenticalCode(associationCode))
                        {
                            associationId = customAssociationContract.Insert(customAssociationInfo);
                            lock (newAssociationCodesLock)
                            {
                                if (!newAssociationCodes.Contains(associationCode))
                                {
                                    newAssociationCodes.Add(associationCode);
                                }
                            }
                        }
                        else
                        {
                            associationId = customAssociationContract.GetNodeIdByNodeCode(associationCode);
                            lock (newAssociationCodesLock)
                            {
                                if (newAssociationCodes.Contains(associationCode))
                                {
                                    newAssociationCodes.Remove(associationCode);
                                }
                            }
                            if (importedMode == ImportedMode.UpdateAndInsert)
                            {
                                customAssociationInfo.AssociationId = associationId;
                                customAssociationContract.Update(customAssociationInfo);
                            }
                        }
                        AddCodeAndId(associationCode, associationId);
                        dataId = associationId;
                    }
                }
                else
                {
                    string groupName = cellTexts[0];
                    string groupCode = cellTexts[1];
                    decimal parentGroupId = GetParentId(groupCode);
                    CustomGroupInfo customGroupInfo = new CustomGroupInfo()
                    {
                        ParentGroupId = parentGroupId,
                        GroupName = groupName,
                        GroupCode = groupCode,
                        GroupType = (byte)GroupType.Association
                    };
                    if (parentGroupId > 0 || DataConvertionHelper.IsNullValue(parentGroupId))
                    {
                        decimal groupId = decimal.MinValue;
                        if (!customGroupContract.IsExistIdenticalCode(groupCode, (byte)GroupType.Association))
                        {
                            customGroupInfo.IsLeaf = true;
                            groupId = customGroupContract.Insert(customGroupInfo);
                        }
                        else
                        {
                            groupId = customGroupContract.GetNodeIdByNodeCode(groupCode, (byte)GroupType.Association);
                            if (importedMode == ImportedMode.UpdateAndInsert)
                            {
                                customGroupInfo.GroupId = groupId;
                                customGroupContract.Update(customGroupInfo);
                            }
                        }
                        AddCodeAndId(groupCode, groupId);
                        dataId = groupId;
                    }
                }
            }
            catch
            {
            }

            return dataId;
        }

        #endregion

        #region 重写公有虚拟化方法

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
                DataTable dataTable = GetDataTableStruct(sheetIndex, dataId);
                for (int rowIndex = 1; rowIndex < sheetView.RowCount; rowIndex++)
                {
                    if (errorDataRowsInSheet.Contains(rowIndex))
                    {
                        continue;
                    }
                    DataRow dataRow = dataTable.NewRow();
                    dataTable.Rows.Add(dataRow);
                    int col = 0;
                    for (int colIndex = 0; colIndex < sheetView.ColumnCount; colIndex++)
                    {
                        if (BasedDataFieldInfos[sheetIndex - 1].ContainsKey(colIndex))
                        {
                            dataRow[col] = DataFieldHelper.GetObjectFormText(sheetView.Cells[rowIndex, colIndex].Text,
                                BasedDataFieldInfos[sheetIndex - 1][colIndex].DataFieldType);
                        }
                        col++;
                    }
                }

                /* 0. 重置关联表 */
                customAssociationContract.ResetTable(dataId);

                /* 1. 排序字段赋值 */
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    dataTable.Rows[idx]["RecordSorting"] = idx + 1;
                }
                /* 2. 创建字段 */
                int index = 1;
                foreach (var keyValue in basedDataFieldInfos[sheetIndex - 1])
                {
                    int length = 0;
                    if (keyValue.Value.DataFieldType == BasedDataType.String)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            string content = dr[index - 1].ToString();
                            if (!string.IsNullOrWhiteSpace(content))
                            {
                                length = length < content.Length ? content.Length : length;
                            }
                        }
                        length++;
                    }
                    string dataFieldCode = UserDataHelper.GetTreeCode(treeCode, index++);
                    AssociatedDataFieldInfo associatedDataFieldInfo = new AssociatedDataFieldInfo()
                    {
                        AssociationId = dataId,
                        LogicalName = keyValue.Value.LogicalName,
                        PhysicalName = dataTable.Columns[index - 1].ColumnName,
                        DataFieldCode = dataFieldCode,
                        BasedDataType = (byte)keyValue.Value.DataFieldType,
                        DataLength = length,
                        DataFieldCategory = keyValue.Value.DataFieldCategory,
                        IsHierarchal = false,
                        Sorting = index
                    };
                    associatedDataFieldContract.Insert(associatedDataFieldInfo);
                }

                /* 3. 导入数据 */
                customAssociationContract.ImportDataTable(dataId, dataTable);
            }
            catch
            {
            }
        }

        /// <summary>
        /// 获得数据表（空表）
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public override DataTable GetDataTableStruct(int sheetIndex, decimal tableId)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = string.Format("table_{0}", tableId);
            int sorting = 1;
            foreach (var keyValue in basedDataFieldInfos[sheetIndex - 1])
            {
                dataTable.Columns.Add(string.Format("ac_{0}_{1}", tableId, sorting++), DataFieldHelper.GetType(keyValue.Value.DataFieldType));
            }
            dataTable.Columns.Add("RecordSorting", typeof(Int32));

            return dataTable;
        }

        /// <summary>
        /// 获得字段类型对象
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public BasedDataFieldInfo GetBasedDataFieldInfo(string title)
        {
            BasedDataFieldInfo basedDataFieldInfo = null;

            int start = title.LastIndexOf('(');
            int middle = title.LastIndexOf('|');
            int end = title.LastIndexOf(')');
            if (start > 0 && middle > (start + 1) && end > (middle + 1))
            {
                /* 字段类型 */
                List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(BasedDataType));
                string logicalName = title.Substring(0, start).Trim();
                string key = title.Substring(start + 1, middle - start - 1).Trim();
                string categroyName = title.Substring(middle + 1, end - middle - 1).Trim();
                EnumItem enumItem = enumItems.Find(item => item.Text.Equals(key));

                /* 字段属性 */
                string description = UserEnumHelper.GetDescription(typeof(AssociatedDataFieldCategory));
                string[] texts = description.Split('|');
                int index = texts.FindIndex(text => text.Equals(categroyName));
                if (enumItem != null && index > 0)
                {
                    basedDataFieldInfo = new BasedDataFieldInfo()
                    {
                        LogicalName = logicalName,
                        DataFieldType = (BasedDataType)enumItem.Value,
                        DataFieldCategory = Convert.ToByte(index)
                    };
                }
            }

            return basedDataFieldInfo;
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
                if ((index == treeCodeMaxLevel) && (importedMode == ImportedMode.UpdateAndInsert
                    || (importedMode == ImportedMode.NotUpdateAndInsert) && newAssociationCodes.Contains(treeCode)))
                {
                    result = true;
                }
            }

            return result;
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
            lock (newAssociationCodesLock)
            {
                newAssociationCodes.Clear();
            }
            basedDataFieldInfos = new Dictionary<int, BasedDataFieldInfo>[spread.Sheets.Count - 1];
            for (int sheetIndex = 1; sheetIndex < spread.Sheets.Count; sheetIndex++)
            {
                basedDataFieldInfos[sheetIndex - 1] = new Dictionary<int, BasedDataFieldInfo>();
                for (int colIndex = 0; colIndex < spread.Sheets[sheetIndex].ColumnCount; colIndex++)
                {
                    string colName = spread.Sheets[sheetIndex].Cells[0, colIndex].Text.Trim();
                    BasedDataFieldInfo basedDataFieldInfo = GetBasedDataFieldInfo(colName);
                    if (basedDataFieldInfo != null)
                    {
                        basedDataFieldInfos[sheetIndex - 1].Add(colIndex, basedDataFieldInfo);
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

        #endregion

        #region 重写受保护虚拟化方法

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
                    int index = treeCode.Length / TREE_CODE_LENGTH - 1; // 当前索引
                    if (index >= 0 && index <= treeCodeMaxLevel)
                    {

                        /* 检查重复的编码 */
                        var duplicatedValues = treeCodeRelations[index].Where(obj => obj.Value == treeCode).Select(obj => obj.Key);
                        if (duplicatedValues.Count() > 1)
                        {
                            dataImportedError = DataImportedError.DuplicatedData;
                        }
                        else
                        {
                            /* 检查编码的结构关系 */
                            int parentIndex = index - 1;
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

                        if (index == treeCodeMaxLevel && dataImportedError == DataImportedError.None)
                        {
                            if (dataSheetCodes.Contains(treeCode))
                            {
                                if (customAssociationContract.GetDataFieldCountConnected(treeCode) > 0)
                                {
                                    dataImportedError = DataImportedError.InService;
                                }
                            }
                            else
                            {
                                dataImportedError = DataImportedError.SheetNotExisted;
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
                    if ((treeCode.Length / TREE_CODE_LENGTH - 1) == treeCodeMaxLevel)
                    {
                        parentId = customAssociationContract.GetNodeIdByNodeCode(code);
                    }
                    else
                    {
                        parentId = customGroupContract.GetNodeIdByNodeCode(code, (byte)GroupType.Association);
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
        /// 获得关联数据
        /// </summary>
        /// <param name="dsGroup"></param>
        /// <returns></returns>
        private DataSet GetGetPageRecord(DataSet dsGroup)
        {
            DataSet ds = null;

            /* 分组表 */
            IList<decimal> groupIds = new List<decimal>();
            foreach (DataRow dr in dsGroup.Tables[0].Rows)
            {
                string groupCode = Convert.ToString(dr["GroupCode"]);
                if ((groupCode.Length / TREE_CODE_LENGTH) == MAX_ASSOCIATION_CODE_LEVEL)
                {
                    groupIds.Add(Convert.ToDecimal(dr["GroupId"]));
                }
            }
            dsGroup.Tables[0].Columns.Remove("GroupId");
            if (groupIds.Count > 0)
            {
                ds = new DataSet();
                /* 关联表信息 */
                Dictionary<decimal, string> associations = new Dictionary<decimal, string>();
                DataSet dsAssociation = customAssociationContract.GetPageRecord(groupIds);
                foreach (DataRow dr in dsAssociation.Tables[0].Rows)
                {
                    decimal associationId = Convert.ToDecimal(dr["AssociationId"]);
                    if (!associations.ContainsKey(associationId))
                    {
                        associations.Add(associationId, string.Format("{0}({1})", dr["AssociationName"], dr["AssociationCode"]));
                    }
                }
                dsAssociation.Tables[0].Columns.Remove("AssociationId");

                /* 合并成关联结构表 */
                DataTable dataTable = DataTableHelper.CombineDataTable(dsGroup.Tables[0], dsAssociation.Tables[0]);
                dataTable.TableName = "关联结构表";
                dataTable.Columns[0].Caption = "关联分类(关联)名称";
                dataTable.Columns[1].Caption = "关联分类(关联)编码";
                ds.Tables.Add(dataTable);

                /* 关联数据 */
                string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                foreach (KeyValuePair<decimal, string> keyValue in associations)
                {
                    DataTable dt = customAssociationContract.GetAssociationData(keyValue.Key);
                    if (dt.Columns.Contains(key))
                    {
                        dt.Columns.Remove(key);
                    }
                    List<BasedDataFieldInfo> basedDataFieldProperties = associatedDataFieldContract.GetDataFieldProperties(keyValue.Key);
                    foreach (DataColumn dc in dt.Columns)
                    {
                        BasedDataFieldInfo basedDataFieldProperty = basedDataFieldProperties.Find(item => item.PhysicalName.Equals(dc.ColumnName));
                        if (basedDataFieldProperty != null)
                        {
                            dc.Caption = string.Format("{0}({1}|{2})", basedDataFieldProperty.LogicalName,
                                UserEnumHelper.GetEnumText(basedDataFieldProperty.DataFieldType),
                                UserEnumHelper.GetEnumText((AssociatedDataFieldCategory)basedDataFieldProperty.DataFieldCategory));
                        }
                    }
                    dt.TableName = keyValue.Value;
                    ds.Tables.Add(dt);
                }
            }
            else
            {
                ds = dsGroup;
            }

            return ds;
        }

        #endregion
    }
}
