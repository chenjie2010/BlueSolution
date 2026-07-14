//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: TreeDataExchanged.cs
// 描述: 树形结构导入导出类
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
using AppFramework.Reference.WCFLibrary;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 树形结构导入导出类
    /// </summary>
    public abstract class TreeDataExchanged
    {
        #region  静态变量锁

        protected static object treeCodeRelationsLock = new object();
        protected static object treeCodeAndIdsLock = new object();
        protected static object treeCodeCacheLock = new object();
        protected static object basedDataFieldInfosLock = new object();

        #endregion

        #region  受保护常量

        /* 树形结构编码长度 */
        protected const int TREE_CODE_LENGTH = 3;

        /* 名称、值字和编码字符串的最大长度 */
        protected const int TREE_STRING_MAX_LENGTH = 64;

        /* Excel文件中除了第一个表格以外，其他表格的最大列数 */
        protected const int MAX_COLUMN_COUNT_PER_SHEET = 200;

        /* Excel文件中除了第一个表格以外，其他表格的最小列数 */
        protected const int MIN_COLUMN_COUNT_PER_SHEET = 2;

        /* 关联数据字符串最大长度 */
        protected const int MAX_STRING_LEN_IN_ASSOCATION = 512;

        #endregion       

        #region 私有变量

        /// <summary>
        /// 树形结构编码列索引
        /// </summary>
        private readonly int treeCodeColumnIndex = 0;

        #endregion

        #region 受保护变量

        /// <summary>
        /// 父节点枚举编码
        /// </summary>
        protected string parentTreeCode = string.Empty;

        /// <summary>
        /// 树形结构每层索引与编码
        /// </summary>
        protected Dictionary<int, string>[] treeCodeRelations = null;

        /// <summary>
        /// 树形结构编码与编号
        /// </summary>
        protected Dictionary<string, decimal> treeCodeAndIds = null;

        /// <summary>
        /// 树形结构编码缓存
        /// </summary>
        protected Dictionary<int, string> treeCodeCache = null;

        /// <summary>
        /// 子表的字段类型
        /// </summary>
        protected Dictionary<int, BasedDataFieldInfo>[] basedDataFieldInfos = null;

        /// <summary>
        /// 子表（EXCEL 文件的第二个表格开始计算）的编码
        /// </summary>
        protected List<string> dataSheetCodes = null;

        /// <summary>
        /// 树形结构最大层级，从0开始计算索引。
        /// </summary>
        protected readonly int treeCodeMaxLevel = 0;

        /// <summary>
        /// 树形结构最小层级，从0开始计算索引。
        /// </summary>
        protected readonly int treeCodeMinLevel = 0;

        /// <summary>
        /// 父节点编号
        /// </summary>
        protected decimal parentNodeId = decimal.MinValue;

        #endregion

        #region 接口属性

        /// <summary>
        /// 数据导入导出枚举
        /// </summary>
        public string DataExportedName
        {
            get;
            set;
        }

        /// <summary>
        /// 树形结构编码列索引
        /// </summary>
        public int TreeCodeColumnIndex
        {
            get
            {
                return treeCodeColumnIndex;
            }
        }

        /// <summary>
        /// 启用分页
        /// </summary>
        public bool PagingEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// 排序字符串
        /// </summary>
        public string StringSorted
        {
            get;
            set;
        }

        /// <summary>
        /// 编码最大层级，从0开始计算索引。
        /// </summary>
        public int TreeCodeMaxLevel
        {
            get
            {
                return treeCodeMaxLevel;
            }
        }

        /// <summary>
        /// 编码最小层级，从0开始计算索引。
        /// </summary>
        public int TreeCodeMinLevel
        {
            get
            {
                return treeCodeMinLevel;
            }
        }        

        /// <summary>
        /// 是否有子表
        /// </summary>
        public bool HasSheets
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许忽略错误行数据
        /// </summary>
        public bool ErrorsSkipped
        {
            set;
            get;
        }

        /// <summary>
        /// 是否跳过子表首行
        /// </summary>
        public bool FirstRowSkipped
        {
            set;
            get;
        }

        /// <summary>
        /// 单元格变化时数据校验模式
        /// </summary>
        public DataTableCheckedMode DataTableCheckedMode
        {
            set;
            get;
        }

        /// <summary>
        /// 树形结构每层索引与编码
        /// </summary>
        public Dictionary<int, string>[] TreeCodeRelations
        {
            get
            {
                return treeCodeRelations;
            }
        }

        /// <summary>
        /// 子表数据类型
        /// </summary>
        public Dictionary<int, BasedDataFieldInfo>[] BasedDataFieldInfos
        {
            get
            {
                return basedDataFieldInfos;
            }
        }

        /// <summary>
        /// 设置帮助内容
        /// </summary>
        public string HelpContent
        {
            set;
            get;
        }

        /// <summary>
        /// 最大列数
        /// </summary>
        public int MaxColumnCount
        {
            get
            {
                return MAX_COLUMN_COUNT_PER_SHEET;
            }
        }

        /// <summary>
        /// 最小列数
        /// </summary>
        public int MinColumnCount
        {
            get
            {
                return MIN_COLUMN_COUNT_PER_SHEET;
            }
        }

        #endregion

        #region 接口索引

        /// <summary>
        /// 表格名称索引
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public int this[string dataSheetCode]
        {
            get
            {
                int index = 0;

                if (dataSheetCodes != null)
                {
                    index = dataSheetCodes.IndexOf(dataSheetCode);
                }

                return index;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataExportedName"></param>
        /// <param name="minCodeLevel"></param>
        /// <param name="maxCodeLevel"></param>
        /// <param name="codeColumnIndex"></param>
        /// <param name="stringSorted"></param>
        /// <param name="hasSheets"></param>
        /// <param name="errorsSkipped"></param>
        public TreeDataExchanged(string dataExportedName, int minCodeLevel, int maxCodeLevel, int codeColumnIndex, string stringSorted,
            bool hasSheets, bool errorsSkipped) : this(dataExportedName, minCodeLevel, maxCodeLevel, decimal.MinValue, string.Empty, codeColumnIndex, 
                stringSorted, hasSheets, errorsSkipped, true)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataExportedName"></param>
        /// <param name="minCodeLevel"></param>
        /// <param name="maxCodeLevel"></param>
        /// <param name="parentId"></param>
        /// <param name="parentCode"></param>
        /// <param name="codeColumnIndex"></param>
        /// <param name="stringSorted"></param>
        /// <param name="hasSheets"></param>
        /// <param name="errorsSkipped"></param>
        /// <param name="firstRowSkipped"></param>
        public TreeDataExchanged(string dataExportedName, int minCodeLevel, int maxCodeLevel, decimal parentId, string parentCode, int codeColumnIndex, 
            string stringSorted, bool hasSheets, bool errorsSkipped, bool firstRowSkipped)
        {
            DataExportedName = dataExportedName;
            parentNodeId = parentId;
            treeCodeColumnIndex = codeColumnIndex;
            StringSorted = stringSorted;
            HasSheets = hasSheets;
            ErrorsSkipped = errorsSkipped;
            FirstRowSkipped = firstRowSkipped;
            parentTreeCode = parentCode;            
            treeCodeMaxLevel = maxCodeLevel;
            treeCodeMinLevel = minCodeLevel;
            treeCodeRelations = new Dictionary<int, string>[treeCodeMaxLevel + 1];
            treeCodeAndIds = new Dictionary<string, decimal>();
            for (int idx = 0; idx < treeCodeRelations.Length; idx++)
            {
                treeCodeRelations[idx] = new Dictionary<int, string>();
            }
            dataSheetCodes = new List<string>();
            DataTableCheckedMode = DataTableCheckedMode.None;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 初始化数据资源
        /// </summary>
        public void InitDataResource(Dictionary<int, string> codeCache)
        {
            lock (treeCodeRelationsLock)
            {
                for (int idx = 0; idx < treeCodeRelations.Length; idx++)
                {
                    treeCodeRelations[idx].Clear();
                }
            }
            lock (treeCodeAndIdsLock)
            {
                treeCodeAndIds.Clear();
            }
            treeCodeCache = codeCache;
            foreach (KeyValuePair<int, string> code in codeCache)
            {
                if (!string.IsNullOrWhiteSpace(code.Value) && code.Value.Length <= TREE_STRING_MAX_LENGTH && code.Value.Length % TREE_CODE_LENGTH == 0)
                {
                    int index = code.Value.Length / TREE_CODE_LENGTH - 1;
                    if (index >= 0 && index <= treeCodeMaxLevel)
                    {
                        lock (treeCodeRelationsLock)
                        {
                            treeCodeRelations[index].Add(code.Key, code.Value);
                        }
                    }
                }
            }
        }        

        /// <summary>
        /// 是否是编码列
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        public bool IsCodeColumn(int columnIndex)
        {
            return (treeCodeColumnIndex == columnIndex);
        }

        /// <summary>
        /// 校验编码单位格数据
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellText"></param>
        /// <returns></returns>
        public IList<DataValidationResult> ValidateCellCodeChanged(int rowIndex, string cellText)
        {
            List<DataValidationResult> dataValidationResults = new List<DataValidationResult>();

            /* 1.刷新缓存 */
            RefreshCodeCache(rowIndex, cellText);

            /* 2.检查当前单元格数据  */
            DataImportedError dataImportedError = CheckTreeCodeFormat(rowIndex, cellText);
            dataValidationResults.Add(new DataValidationResult(0, rowIndex, treeCodeColumnIndex, dataImportedError));

            /* 3. 检查下一级编码的结构关系 */
            IList<DataValidationResult> results = CheckCodeStructOnNextLevel(rowIndex, cellText);
            dataValidationResults.AddRange(results);

            return dataValidationResults;
        }

        #endregion

        #region 公有的虚拟方法

        /// <summary>
        /// 是否允许子表数据导入
        /// </summary>
        /// <param name="treeCode"></param>
        /// <param name="importedMode"></param>
        /// <returns></returns>

        public virtual bool AllowDataTableImported(string treeCode, ImportedMode importedMode)
        {
            return false;
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="dataId"></param>
        /// <param name="treeCode"></param>
        /// <param name="dataTable"></param>
        public virtual void ImportDataTable(int sheetIndex, decimal dataId, string treeCode, DataTable dataTable)
        {

        }

        /// <summary>
        /// 导入子表数据
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="dataId"></param>
        /// <param name="treeCode"></param>
        /// <param name="sheetView"></param>
        /// <param name="errorDataRowsInSheet"></param>
        public virtual void ImportDataTable(int sheetIndex, decimal dataId, string treeCode, SheetView sheetView, IList<int> errorDataRowsInSheet)
        {
        }

        /// <summary>
        /// 获得数据表（空表）
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="tableId"></param>
        /// <returns></returns>
        public virtual DataTable GetDataTableStruct(int sheetIndex, decimal tableId)
        {
            return null;
        }

        /// <summary>
        /// 初始化数据子表结构
        /// </summary>
        /// <param name="cellTextsInSheets"></param>
        public virtual void InitDataTableStruct(FpSpread spread)
        {
           
        }

        #endregion

        #region 受保护的虚拟方法

        /// <summary>
        /// 检查树形结构编码格式
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="treeCode"></param>
        /// <returns></returns>
        protected virtual DataImportedError CheckTreeCodeFormat(int rowIndex, string treeCode)
        {
            DataImportedError dataImportedError = DataImportedError.None;

            if (!string.IsNullOrWhiteSpace(treeCode))
            {
                if (treeCode.Length % TREE_CODE_LENGTH == 0 && UserDataHelper.MatchDigit(treeCode))
                {
                    int index = treeCode.Length / TREE_CODE_LENGTH - 1; // 当前索引
                    if (index >= treeCodeMinLevel && index <= treeCodeMaxLevel)
                    {
                        if (parentNodeId > 0 && treeCode.Length <= parentTreeCode.Length)
                        {
                            dataImportedError = DataImportedError.ErrorFormat;
                        }
                        else
                        {
                            /* 检查重复的编码 */
                            int count = 0;
                            lock (treeCodeRelationsLock)
                            {
                                var duplicatedValues = treeCodeRelations[index].Where(obj => obj.Value == treeCode).Select(obj => obj.Key);
                                count = duplicatedValues.Count();
                            }                            
                            if (count > 1)
                            {
                                dataImportedError = DataImportedError.DuplicatedData;
                            }
                            else
                            {
                                /* 检查编码的结构关系 */
                                int parentIndex = index - 1;
                                lock (treeCodeRelationsLock)
                                {
                                    if (parentIndex >= 0 && parentIndex < treeCodeMaxLevel)
                                    {
                                        count = treeCodeRelations[parentIndex].Count;
                                    }
                                }
                                if (parentIndex >= 0 && parentIndex < treeCodeMaxLevel && count > 0)
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
                                        lock (treeCodeRelationsLock)
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
        protected virtual decimal GetParentId(string treeCode)
        {
            decimal parentId = 0;

            if (parentNodeId > 0)
            {
                if (treeCode.Length == parentTreeCode.Length + TREE_CODE_LENGTH)
                {
                    parentId = parentNodeId;
                }
                else if (treeCode.Length > parentTreeCode.Length + TREE_CODE_LENGTH)
                {
                    string code = treeCode.Substring(0, treeCode.Length - TREE_CODE_LENGTH);
                    lock (treeCodeAndIdsLock)
                    {
                        if (treeCodeAndIds.ContainsKey(code))
                        {
                            parentId = treeCodeAndIds[code];
                        }
                    }
                }
            }
            else
            {
                if (treeCode.Length > TREE_CODE_LENGTH)
                {
                    string code = treeCode.Substring(0, treeCode.Length - TREE_CODE_LENGTH);
                    lock (treeCodeAndIdsLock)
                    {
                        if (treeCodeAndIds.ContainsKey(code))
                        {
                            parentId = treeCodeAndIds[code];
                        }
                    }
                }
                else if (treeCode.Length == TREE_CODE_LENGTH)
                {
                    parentId = decimal.MinValue;
                }
            }

            return parentId;
        }

        #endregion

        #region 受保护方法

        /// <summary>
        /// 构建节点与编号的关系
        /// </summary>
        /// <param name="treeCode"></param>
        /// <param name="nodeId"></param>
        protected void AddCodeAndId(string treeCode, decimal nodeId)
        {
            lock (treeCodeAndIdsLock)
            {
                if (!treeCodeAndIds.ContainsKey(treeCode))
                {
                    treeCodeAndIds.Add(treeCode, nodeId);
                }
            }
        }

        /// <summary>
        /// 检查树形结构字符串
        /// </summary>
        /// <param name="cellText"></param>
        /// <param name="nullAllowed"></param>
        /// <returns></returns>
        protected DataImportedError CheckTreeString(string cellText, bool nullAllowed)
        {
            DataImportedError dataImportedError = DataImportedError.None;

            if (!string.IsNullOrWhiteSpace(cellText))
            {
                if (cellText.Length > TREE_STRING_MAX_LENGTH)
                {
                    dataImportedError = DataImportedError.DataLenth;
                }
            }
            else
            {
                dataImportedError = nullAllowed ? DataImportedError.None : DataImportedError.DataEmpty;
            }

            return dataImportedError;
        }

        /// <summary>
        /// 检查文本是否符合规范
        /// </summary>
        /// <param name="text"></param>
        /// <param name="basedDataType"></param>
        /// <returns></returns>
        protected DataImportedError CheckDataFormat(string text, BasedDataType basedDataType)
        {
            DataImportedError dataImportedError = DataImportedError.None;

            if (string.IsNullOrWhiteSpace(text))
            {
                dataImportedError = DataImportedError.DataEmpty;
            }
            else
            {
                switch (basedDataType)
                {
                    case BasedDataType.Boolean:
                        if (!text.Equals("0") && !text.Equals("1") && !text.ToLower().Equals("true") && !text.ToLower().Equals("false"))
                        {
                            dataImportedError = DataImportedError.ErrorFormat;
                        }
                        break;

                    case BasedDataType.DateTime:
                        DateTime dateTime = DataConvertionHelper.GetConvertedDateTime(text);
                        if (DataConvertionHelper.IsNullValue(dateTime))
                        {
                            dataImportedError = DataImportedError.ErrorFormat;
                        }
                        break;

                    case BasedDataType.Decimal:
                        if (!UserDataHelper.MatchDecimal(text))
                        {
                            dataImportedError = DataImportedError.ErrorFormat;
                        }
                        break;

                    case BasedDataType.Int32:
                        if (!UserDataHelper.MatchInteger(text))
                        {
                            dataImportedError = DataImportedError.ErrorFormat;
                        }
                        break;

                    case BasedDataType.String:
                        if (!string.IsNullOrWhiteSpace(text) && text.Length > MAX_STRING_LEN_IN_ASSOCATION)
                        {
                            dataImportedError = DataImportedError.DataLenth;
                        }
                        break;
                }
            }

            return dataImportedError;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 刷新树形结构每层索引与编码的缓存
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="code"></param>
        private void RefreshCodeCache(int rowIndex, string code)
        {
            string oldTreeCode = string.Empty;
            if (treeCodeCache.ContainsKey(rowIndex))
            {
                oldTreeCode = treeCodeCache[rowIndex];

                /* 移除旧编码 */
                int index = oldTreeCode.Length / TREE_CODE_LENGTH - 1;// 当前索引
                if (index >= 0 && index <= treeCodeMaxLevel && treeCodeRelations[index].ContainsKey(rowIndex))
                {
                    treeCodeRelations[index].Remove(rowIndex);
                }
                /* 增加新编码 */
                if (!string.IsNullOrWhiteSpace(code) && code.Length <= TREE_STRING_MAX_LENGTH && code.Length % TREE_CODE_LENGTH == 0)
                {
                    index = code.Length / TREE_CODE_LENGTH - 1;// 当前索引
                    if (index >= 0 && index <= treeCodeMaxLevel)
                    {
                        lock (treeCodeRelationsLock)
                        {
                            if (!treeCodeRelations[index].ContainsKey(rowIndex))
                            {
                                treeCodeRelations[index].Add(rowIndex, code);
                            }
                            else
                            {
                                treeCodeRelations[index][rowIndex] = code;
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 当编码发生变化时，检查下一级编码的结构关系
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        private IList<DataValidationResult> CheckCodeStructOnNextLevel(int rowIndex, string code)
        {
            List<DataValidationResult> dataValidationResults = new List<DataValidationResult>();

            string oldTreeCode = string.Empty;
            lock (treeCodeCacheLock)
            {
                if (treeCodeCache.ContainsKey(rowIndex))
                {
                    oldTreeCode = treeCodeCache[rowIndex];
                    if (oldTreeCode.Equals(code))
                    {
                        return dataValidationResults;
                    }
                    treeCodeCache[rowIndex] = code;
                }
                else
                {
                    treeCodeCache.Add(rowIndex, code);
                }
            }

            /* 旧编码正确，则调整下一级以旧编码为前缀的状态为空 */
            List<DataValidationResult> oldTreeCodeResults = CheckCodeStructOnNextLevel(oldTreeCode, DataImportedError.ParentEmpty);
            if (oldTreeCodeResults.Count > 0)
            {
                dataValidationResults.AddRange(oldTreeCodeResults);
            }

            /* 新编码正确，则调整下一级以新编码为前缀的状态为正确 */
            List<DataValidationResult> treeCodeResults = CheckCodeStructOnNextLevel(code, DataImportedError.None);
            if (treeCodeResults.Count > 0)
            {
                dataValidationResults.AddRange(treeCodeResults);
            }

            return dataValidationResults;
        }

        /// <summary>
        /// 下一级编码以该编码为前缀，则检查状态发送变化
        /// </summary>
        /// <param name="code"></param>
        /// <param name="dataImportedError"></param>
        /// <returns></returns>
        private List<DataValidationResult> CheckCodeStructOnNextLevel(string code, DataImportedError dataImportedError)
        {
            List<DataValidationResult> dataValidationResults = new List<DataValidationResult>();

            if (!string.IsNullOrWhiteSpace(code) && (code.Length % TREE_CODE_LENGTH == 0))
            {
                int index = code.Length / TREE_CODE_LENGTH - 1;// 当前索引
                if (index >= 0 && index < treeCodeMaxLevel)
                {
                    int childIndex = index + 1;
                    if (treeCodeRelations[childIndex].Count > 0)
                    {
                        foreach (KeyValuePair<int, string> keyValue in treeCodeRelations[childIndex])
                        {
                            if (string.IsNullOrWhiteSpace(keyValue.Value) || keyValue.Value.Length < TREE_CODE_LENGTH)
                            {
                                continue;
                            }
                            string prefixCode = keyValue.Value.Substring(0, keyValue.Value.Length - TREE_CODE_LENGTH);
                            if (prefixCode.Equals(code))
                            {
                                dataValidationResults.Add(new DataValidationResult(0, keyValue.Key, treeCodeColumnIndex, dataImportedError));
                            }
                        }
                    }
                }
            }

            return dataValidationResults;
        }

        #endregion
    }
}
