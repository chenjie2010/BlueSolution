using System;
//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: DataExportedInterface.cs
// 描述: 数据导出接口
// 作者：ChenJie 
// 编写日期：2018/07/18
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarPoint.Win.Spread;
using AppFramework.Core;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 数据导出接口
    /// </summary>
    public interface IDataExportedInterface
    {
        #region 属性

        /// <summary>
        /// 数据导入导出枚举
        /// </summary>
        string DataExportedName
        {
            get;
            set;
        }

        /// <summary>
        /// 树形结构编码列索引
        /// </summary>
        int TreeCodeColumnIndex
        {
            get;
        }

        /// <summary>
        /// 启用分页
        /// </summary>
        bool PagingEnabled
        {
            get;
            set;
        }

        /// <summary>
        /// 排序字符串
        /// </summary>
        string StringSorted
        {
            get;
            set;
        }

        /// <summary>
        /// 固定的列数
        /// </summary>
        int ColumnCountFixed
        {
            get;
        }

        /// <summary>
        /// 编码最大层级，从0开始计算索引。
        /// </summary>
        int TreeCodeMaxLevel
        {
            get;
        }

        /// <summary>
        /// 编码最小层级，从0开始计算索引。
        /// </summary>
        int TreeCodeMinLevel
        {
            get;
        }

        /// <summary>
        /// 是否有子表
        /// </summary>
        bool HasSheets
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许忽略错误行数据
        /// </summary>
        bool ErrorsSkipped
        {
            set;
            get;
        }

        /// <summary>
        /// 是否跳过子表首行
        /// </summary>
        bool FirstRowSkipped
        {
            set;
            get;
        }

        /// <summary>
        /// 单元格变化时数据校验模式
        /// </summary>
        DataTableCheckedMode DataTableCheckedMode
        {
            set;
            get;
        }

        /// <summary>
        /// 编号与编码关系字典
        /// </summary>
        Dictionary<int, string>[] TreeCodeRelations
        {
            get;
        }

        /// <summary>
        /// 子表数据类型
        /// </summary>
        Dictionary<int, BasedDataFieldInfo>[] BasedDataFieldInfos
        {
            get;
        }

        /// <summary>
        /// 最大列数
        /// </summary>
        int MaxColumnCount
        {
            get;
        }

        /// <summary>
        /// 最小列数
        /// </summary>
        int MinColumnCount
        {
            get;
        }

        /// <summary>
        /// 设置帮助内容
        /// </summary>
        string HelpContent
        {
            set;
            get;
        }

        #endregion

        #region 接口索引

        /// <summary>
        /// 表格名称索引
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        int this[string sheetName]
        {
            get;
        }

        #endregion

        #region 方法

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <returns></returns>
        DataSet GetPageRecord();

        /// <summary>
        /// 获得数据集
        /// </summary>
        /// <param name="startPosition"></param>
        /// <param name="count"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
        DataSet GetPageRecord(int startPosition, int count, ref int totalCount);

        /// <summary>
        /// 获得模板列名称
        /// </summary>
        /// <returns></returns>
        Dictionary<string, IList<string>> GetTemplateColumnCaptions();

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="cellTexts"></param>
        /// <returns></returns>
        IList<DataValidationResult> ValidateCellData(int sheetIndex, int rowIndex, IList<string> cellTexts);

        /// <summary>
        /// 校验变化后单元格数据（除编码单位格外）
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <param name="cellText"></param>
        /// <returns></returns>
        DataValidationResult ValidateCellDataChanged(int sheetIndex, int rowIndex, int colIndex, string cellText);

        /// <summary>
        /// 校验编码单位格数据
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="cellText"></param>
        /// <returns></returns>
        IList<DataValidationResult> ValidateCellCodeChanged(int rowIndex, string cellText);

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="cellTexts"></param>
        /// <param name="importedMode"></param>
        /// <returns></returns>
        decimal ImportData(IList<string> cellTexts, ImportedMode importedMode);

        /// <summary>
        /// 导入子表数据
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="dataId"></param>
        /// <param name="treeCode"></param>
        /// <param name="sheetView"></param>
        /// <param name="errorDataRowsInSheet"></param>
        void ImportDataTable(int sheetIndex, decimal dataId, string treeCode, SheetView sheetView, IList<int> errorDataRowsInSheet);

        /// <summary>
        /// 初始化数据资源
        /// </summary>
        /// <param name="codeCache"></param>
        void InitDataResource(Dictionary<int, string> codeCache);

        /// <summary>
        /// 初始化数据子表结构
        /// </summary>
        /// <param name="spread"></param>
        void InitDataTableStruct(FpSpread spread);

        /// <summary>
        /// 是否是编码列
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <returns></returns>
        bool IsCodeColumn(int columnIndex);

        /// <summary>
        /// 是否允许子表数据导入
        /// </summary>
        /// <param name="treeCode"></param>
        /// <param name="importedMode"></param>
        /// <returns></returns>

        bool AllowDataTableImported(string treeCode, ImportedMode importedMode);

        #endregion
    }
}
