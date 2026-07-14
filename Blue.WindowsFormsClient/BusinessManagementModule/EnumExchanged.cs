//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: EnumExchanged.cs
// 描述: 枚举导入导出类
// 作者：ChenJie 
// 编写日期：2018/07/18
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppFramework.Core;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    /// <summary>
    /// 枚举导入导出类
    /// </summary>
    public class EnumExchanged : TreeDataExchanged, IDataExportedInterface
    {
        #region  私有常量

        /* 导入枚举时，Excel文件包含15列 */
        private const int ENUM_DATA_IMPORTED_COLUMNS = 15;
        
        /// <summary>
        /// 编码列索引
        /// </summary>
        private const int ENUM_CODE_COLUMN_INDEX = 1;
        
        /* 导入时枚举编码最大层级，以0作为第一层 */
        private const int MAX_ENUM_CODE_LEVEL = 9;

        /* 导入时枚举编码最小层级，以0作为第一层 */
        private const int MIN_ENUM_CODE_LEVEL = 0;

        #endregion

        #region 私有变量

        /// <summary>
        /// 枚举契约
        /// </summary>
        private readonly ICustomEnumContract customEnumContract;       

        #endregion
     
        #region 接口属性        

        /// <summary>
        /// 固定的列数
        /// </summary>
        public int ColumnCountFixed
        {
            get
            {
                return ENUM_DATA_IMPORTED_COLUMNS;
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
        /// <param name="enumContract"></param>
        public EnumExchanged(string dataExportedName, decimal parentId, string parentCode, string stringSorted, ICustomEnumContract enumContract)
            : base(dataExportedName, MIN_ENUM_CODE_LEVEL, MAX_ENUM_CODE_LEVEL, parentId, parentCode, ENUM_CODE_COLUMN_INDEX, 
                  stringSorted, false, true, true)
        {            
            customEnumContract = enumContract;
            if (parentId > 0)
            {
                PagingEnabled = false;
            }
            else
            {
                PagingEnabled = true;
            }
            HelpContent = "（1）枚举编码最低长度为3位（每级枚举编码长度是3的倍数）；\r\n（2）枚举的节点关系由枚举编码（前缀）的关系构成。";
        }        

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <returns></returns>
        public DataSet GetPageRecord()
        {
            Dictionary<string, IList<string>> templateColumnCaptions = GetTemplateColumnCaptions();
            DataSet ds = customEnumContract.GetPageRecord(parentNodeId);
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                if (i < templateColumnCaptions[DataExportedName].Count)
                {
                    ds.Tables[0].Columns[i].Caption = templateColumnCaptions[DataExportedName][i];
                }
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
            Dictionary<string, IList<string>> templateColumnCaptions = GetTemplateColumnCaptions();
            DataSet ds = customEnumContract.GetPageRecord(startPosition, count, ref totalCount);
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                if (i < templateColumnCaptions[DataExportedName].Count)
                {
                    ds.Tables[0].Columns[i].Caption = templateColumnCaptions[DataExportedName][i];
                }
            }
            return ds;
        }

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, IList<string>> GetTemplateColumnCaptions()
        {
            Dictionary<string, IList<string>> templateColumnCaptions = new Dictionary<string, IList<string>>();

            //IList<string> columnCaptions = customEnumContract.GetTemplateColumnCaptions();
            IList<string> columnCaptions = new List<string>();
            if (columnCaptions.Count == 0)
            {
                columnCaptions.Add("枚举名称");
                columnCaptions.Add("枚举编码");
                columnCaptions.Add("枚举值");
                columnCaptions.Add("唯一值一");
                columnCaptions.Add("唯一值二");
                columnCaptions.Add("附加值字符串一");
                columnCaptions.Add("附加值字符串二");
                columnCaptions.Add("附加值字符串三");
                columnCaptions.Add("附加值字符串四");
                columnCaptions.Add("附加值字符串五");
                columnCaptions.Add("附加值字符串六");
                columnCaptions.Add("附加值整形一");
                columnCaptions.Add("附加值整形二");
                columnCaptions.Add("附加值实数一");
                columnCaptions.Add("附加值实数二");
            }
            templateColumnCaptions.Add(DataExportedName, columnCaptions);

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
            /* (0) 枚举名称，(1) 枚举编码，(2) 枚举值，(3) 附加编码一，(4) 附加编码二，
             * (5) 附加值字符串一，(6) 附加值字符串二，(7) 附加值字符串三，(8) 附加值字符串四，(9) 附加值字符串五，(10) 附加值字符串六，
             * (11) 附加值整形一，(12) 附加值整形二，(13) 附加值实数一，(14) 附加值实数二 */
            IList<DataValidationResult> results = new List<DataValidationResult>();
            
            /* (0) 枚举名称 */
            string enumName = cellTexts[0];
            results.Add(new DataValidationResult(sheetIndex, rowIndex, 0, CheckTreeString(enumName, false)));

            /* (1) 枚举编码 */
            string enumCode = cellTexts[1];
            results.Add(new DataValidationResult(sheetIndex, rowIndex, 1, CheckTreeCodeFormat(rowIndex, enumCode)));

            /* (2) 枚举值，(3) 附加编码一，(4) 附加编码二，(5) 附加值字符串一，(6) 附加值字符串二，(7) 附加值字符串三，(8) 附加值字符串四，(9) 附加值字符串五，(10) 附加值字符串六 */
            for (int idx = 2; idx <= 10; idx++)
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, idx, CheckTreeString(cellTexts[idx], true)));
            }

            /* (11) 附加值整形一，(12) 附加值整形二 */
            if (!string.IsNullOrWhiteSpace(cellTexts[11]) && !UserDataHelper.MatchInteger(cellTexts[11]))
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 11, DataImportedError.ErrorFormat));
            }
            else
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 11, DataImportedError.None));
            }
            if (!string.IsNullOrWhiteSpace(cellTexts[12]) && !UserDataHelper.MatchInteger(cellTexts[12]))
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 12, DataImportedError.ErrorFormat));
            }
            else
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 12, DataImportedError.None));
            }

            /* (13) 附加值实数一，(14) 附加值实数二 */
            if (!string.IsNullOrWhiteSpace(cellTexts[13]) && !UserDataHelper.MatchDecimal(cellTexts[13]))
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 13, DataImportedError.ErrorFormat));
            }
            else
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 13, DataImportedError.None));
            }
            if (!string.IsNullOrWhiteSpace(cellTexts[14]) && !UserDataHelper.MatchDecimal(cellTexts[14]))
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 14, DataImportedError.ErrorFormat));
            }
            else
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 14, DataImportedError.None));
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

            if (colIndex == ENUM_CODE_COLUMN_INDEX)
            {
                throw new ArgumentException("参数错误，不能检查编码单元格数据。");
            }

            /* (0) 枚举名称，(1) 枚举编码，(2) 枚举值，(3) 附加编码一，(4) 附加编码二，
             * (5) 附加值字符串一，(6) 附加值字符串二，(7) 附加值字符串三，(8) 附加值字符串四，(9) 附加值字符串五，(10) 附加值字符串六，
             * (11) 附加值整形一，(12) 附加值整形二，(13) 附加值实数一，(14) 附加值实数二 */
            if (colIndex == 0)
            {
                dataImportedError = CheckTreeString(cellText, false);
            }
            else if (colIndex >= 2 && colIndex <= 10)
            {
                dataImportedError = CheckTreeString(cellText, true);
            }
            else if (colIndex >= 11 && colIndex <= 12)
            {
                if (!string.IsNullOrWhiteSpace(cellText) && !UserDataHelper.MatchInteger(cellText))
                {
                    dataImportedError = DataImportedError.ErrorFormat;
                }
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(cellText) && !UserDataHelper.MatchDecimal(cellText))
                {
                    dataImportedError = DataImportedError.ErrorFormat;
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
            decimal enumId = decimal.MinValue;

            /* 仅支持下面两种模式 */
            if (importedMode != ImportedMode.UpdateAndInsert && importedMode != ImportedMode.NotUpdateAndInsert)
            {
                throw new ArgumentException("参数错误。");
            }

            try
            {
                string enumName = cellTexts[0];
                string enumCode = cellTexts[1];
                string enumValue = cellTexts[2];
                string firstCode = cellTexts[3];
                string secondCode = cellTexts[4];
                string fstAdditionalString = cellTexts[5];
                string scdAdditionalString = cellTexts[6];
                string trdAdditionalString = cellTexts[7];
                string fourthAdditionalString = cellTexts[8];
                string fifthAdditionalString = cellTexts[9];
                string sixthAdditionalString = cellTexts[10];
                int fstAdditionalInteger = int.MinValue;
                if (!string.IsNullOrWhiteSpace(cellTexts[11]))
                {
                    fstAdditionalInteger = DataConvertionHelper.GetConvertedInt(cellTexts[11]);
                }
                int scdAdditionalInteger = int.MinValue;
                if (!string.IsNullOrWhiteSpace(cellTexts[12]))
                {
                    scdAdditionalInteger = DataConvertionHelper.GetConvertedInt(cellTexts[12]);
                }
                decimal fstAdditionalDecimal = decimal.MinValue;
                if (!string.IsNullOrWhiteSpace(cellTexts[13]))
                {
                    fstAdditionalDecimal = DataConvertionHelper.GetConvertedDecimal(cellTexts[13]);
                }
                decimal scdAdditionalDecimal = decimal.MinValue;
                if (!string.IsNullOrWhiteSpace(cellTexts[14]))
                {
                    scdAdditionalDecimal = DataConvertionHelper.GetConvertedDecimal(cellTexts[14]);
                }
                decimal parentEnumId = GetParentId(enumCode);
                CustomEnumInfo customEnumInfo = new CustomEnumInfo()
                {
                    ParentEnumId = parentEnumId,
                    EnumName = enumName,
                    EnumCode = enumCode,
                    EnumValue = enumValue,
                    FirstCode = firstCode,
                    SecondCode = secondCode,
                    FstAdditionalString = fstAdditionalString,
                    ScdAdditionalString = scdAdditionalString,
                    TrdAdditionalString = trdAdditionalString,
                    FourthAdditionalString = fourthAdditionalString,
                    FifthAdditionalString = fifthAdditionalString,
                    SixthAdditionalString = sixthAdditionalString,
                    FstAdditionalInteger = fstAdditionalInteger,
                    ScdAdditionalInteger = scdAdditionalInteger,
                    FstAdditionalDecimal = fstAdditionalDecimal,
                    ScdAdditionalDecimal = scdAdditionalDecimal
                };                
                if (parentEnumId > 0 || parentEnumId == decimal.MinValue)
                {                    
                    if (!customEnumContract.IsExistIdenticalCode(enumCode))
                    {
                        enumId = customEnumContract.Insert(customEnumInfo);
                    }
                    else
                    {
                        enumId = customEnumContract.GetEnumId(enumCode);
                        if (importedMode == ImportedMode.UpdateAndInsert)
                        {
                            customEnumInfo.EnumId = enumId;
                            customEnumContract.Update(customEnumInfo);
                        }
                    }
                    AddCodeAndId(enumCode, enumId);
                }
            }
            catch
            {
            }

            return enumId;
        }
        
        #endregion

        #region 私有方法

       
        #endregion
    }
}
