//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DepartmentExchanged.cs
// 描述: 单位导入导出类
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
using AppFramework.Core;
using Blue.Model.SystemModule;
using Blue.WCFContracts.SystemModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    /// <summary>
    /// 单位导入导出类
    /// </summary>
    public class DepartmentExchanged : TreeDataExchanged, IDataExportedInterface
    {
        #region  私有常量

        /* 导入单位时，Excel文件包含6列 */
        private const int DEP_DATA_IMPORTED_COLUMNS = 6;

        /* 编码列索引 */
        private const int DEP_CODE_COLUMN_INDEX = 1;

        /* 导入时单位编码最大层级，以0作为第一层 */
        private const int MAX_DEP_CODE_LEVEL = 7;

        /* 导入时单位编码最小层级，以0作为第一层(不允许导入第一层，因为第一层是根节点单位名称) */
        private const int MIN_DEP_CODE_LEVEL = 1;

        #endregion

        #region 私有变量

        /// <summary>
        /// 单位契约
        /// </summary>
        private readonly ICustomDepartmentContract customDepartmentContract;

        #endregion
        
        #region 接口属性        

        /// <summary>
        /// 固定的列数
        /// </summary>
        public int ColumnCountFixed
        {
            get
            {
                return DEP_DATA_IMPORTED_COLUMNS;
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
        /// <param name="departmentContract"></param>
        public DepartmentExchanged(string dataExportedName, decimal parentId, string parentCode, string stringSorted, ICustomDepartmentContract departmentContract)
            : base(dataExportedName, MIN_DEP_CODE_LEVEL, MAX_DEP_CODE_LEVEL, parentId, parentCode, DEP_CODE_COLUMN_INDEX, stringSorted, false, true, true)
        {
            customDepartmentContract = departmentContract;
            if (parentId > 0)
            {
                PagingEnabled = false;
            }
            else
            {
                PagingEnabled = true;
            }
            HelpContent = "（1）单位编码最低长度为6位（每级单位编码长度是3的倍数）；\r\n（2）单位的节点关系由单位编码（前缀）的关系构成。";
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获得数据集(不含父节点自身数据)
        /// </summary>
        /// <returns></returns>
        public DataSet GetPageRecord()
        {
            return customDepartmentContract.GetPageRecord(parentNodeId);
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
            return customDepartmentContract.GetPageRecord(startPosition, count, ref totalCount);
        }

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, IList<string>> GetTemplateColumnCaptions()
        {
            Dictionary<string, IList<string>> templateColumnCaptions = new Dictionary<string, IList<string>>();

            IList<string> columnCaptions = customDepartmentContract.GetTemplateColumnCaptions();
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
            /* (0) 单位名称，(1) 单位编码，(2) 单位值，(3) 单位编码一，(4) 单位编码二，(5) 单位属性 */
            IList<DataValidationResult> results = new List<DataValidationResult>();

            /* (0) 单位名称 */
            string enumName = cellTexts[0];
            results.Add(new DataValidationResult(sheetIndex, rowIndex, 0, CheckTreeString(enumName, false)));

            /* (1) 单位编码 */
            string enumCode = cellTexts[1];
            results.Add(new DataValidationResult(sheetIndex, rowIndex, 1, CheckTreeCodeFormat(rowIndex, enumCode)));

            /* (2) 单位值，(3) 单位编码一，(4) 单位编码二 */
            for (int idx = 2; idx <= 4; idx++)
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, idx, CheckTreeString(cellTexts[idx], true)));
            }

            /* (5) 单位属性 */
            if (!string.IsNullOrWhiteSpace(cellTexts[5]) && !UserDataHelper.MatchByte(cellTexts[5]))
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 5, DataImportedError.ErrorFormat));
            }
            else
            {
                results.Add(new DataValidationResult(sheetIndex, rowIndex, 5, DataImportedError.None));
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

            if (colIndex == DEP_CODE_COLUMN_INDEX)
            {
                throw new ArgumentException("参数错误，不能检查编码单元格数据。");
            }

            /* (0) 单位名称，(1) 单位编码，(2) 单位值，(3) 单位编码一，(4) 单位编码二，(5) 单位属性 */
            if (colIndex == 0)
            {
                dataImportedError = CheckTreeString(cellText, false);
            }

            else if (colIndex >= 2 && colIndex <= 4)
            {
                dataImportedError = CheckTreeString(cellText, true);
            }
            else if (colIndex == 5)
            {
                if (!string.IsNullOrWhiteSpace(cellText) && !UserDataHelper.MatchByte(cellText))
                {
                    dataImportedError = DataImportedError.ErrorFormat;
                }
            }
            else
            {
                throw new ArgumentException("行索引参数异常。");
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
            decimal depId = decimal.MinValue;

            /* 仅支持下面两种模式 */
            if (importedMode != ImportedMode.UpdateAndInsert && importedMode != ImportedMode.NotUpdateAndInsert)
            {
                throw new ArgumentException("参数错误。");
            }

            try
            {
                string depName = cellTexts[0];
                string depCode = cellTexts[1];
                string depValue = cellTexts[2];
                string firstCode = cellTexts[3];
                string secondCode = cellTexts[4];
                string departmentPropertyString = cellTexts[5];
                byte departmentProperty = 0;
                if (!string.IsNullOrWhiteSpace(departmentPropertyString))
                {
                    departmentProperty = DataConvertionHelper.GetConvertedByte(departmentPropertyString);
                }
                decimal parentDepId = GetParentId(depCode);
                CustomDepartmentInfo customDepartmentInfo = new CustomDepartmentInfo()
                {
                    ParentDepId = parentDepId,
                    DepName = depName,
                    DepCode = depCode,
                    DepValue = depValue,
                    FirstCode = firstCode,
                    SecondCode = secondCode,
                    IsSystemDepartment = false,
                    DepartmentProperty = departmentProperty
                };
                if (parentDepId > 0 || parentDepId == decimal.MinValue)
                {
                    if (!customDepartmentContract.IsExistIdenticalCode(depCode))
                    {
                        depId = customDepartmentContract.Insert(customDepartmentInfo);
                    }
                    else
                    {
                        depId = customDepartmentContract.GetNodeIdByNodeCode(depCode);
                        if (importedMode == ImportedMode.UpdateAndInsert)
                        {
                            customDepartmentInfo.DepId = depId;
                            customDepartmentContract.Update(customDepartmentInfo);
                        }
                    }
                    AddCodeAndId(depCode, depId);                    
                }
            }
            catch
            {
            }

            return depId;
        }

        #endregion

        #region 私有方法        

        #endregion
    }
}
