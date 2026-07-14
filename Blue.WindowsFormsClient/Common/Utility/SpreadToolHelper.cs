using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using FarPoint.Excel;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;

namespace Blue.WindowsFormsClient.Common
{
    /// <summary>
    /// Spread控件操作类
    /// </summary>
    public static class SpreadToolHelper
    {
        /// <summary>
        /// 单次多选不能超过的单元格数量
        /// </summary>
        private static readonly int MaxSelectionCount = 500;

        #region  私有常量

        /* 模板导出时默认行数 */
        private const int DEFAULT_TEMPLATE_ROW_COUNT = 100;

        /* 每次导出的数据行数 */
        private const int DATA_PAGE_SIZE_IMPORTED = 5000;

        /* 列宽 */
        private const int COLUMN_WIDTH_IN_SHEET_VIEW = 180;

        /* 标题行高 */
        private const int ROW_HEIGHT_IN_SHEET_VIEW = 30;

        #endregion

        #region 静态方法

        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="spread"></param>
        /// <param name="cellFormat"></param>
        public static void SetCellTypeOnCells(FpSpread spread, ICellType cellType)
        {
            CellRange[] cellRanges = spread.ActiveSheet.GetSelections();
            int count = 0;
            foreach (var cellRange in cellRanges)
            {
                count += cellRange.RowCount * cellRange.Column;
            }
            if (count <= MaxSelectionCount)
            {                
                if (count > 0)
                {
                    foreach (var cellRange in cellRanges)
                    {
                        spread.ActiveSheet.Cells[cellRange.Row, cellRange.Column,
                            cellRange.Row + cellRange.RowCount - 1, cellRange.Column + cellRange.ColumnCount - 1].CellType = cellType;
                    }
                }
                else
                {
                    if (spread.ActiveSheet.ActiveCell != null)
                    {
                        spread.ActiveSheet.ActiveCell.CellType = cellType;
                    }
                }
            }
            else
            {
                MessageBox.Show(string.Format("单次选中的单元格数量不能超过{0}个。", MaxSelectionCount), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 导出 Excel 模板
        /// </summary>
        /// <param name="templateColumnCaptions"></param>
        /// <param name="fileName"></param>
        public static void ExprotTemplate(Dictionary<string, IList<string>> templateColumnCaptions, string fileName)
        {
            using (FpSpread fsExcel = new FpSpread())
            {
                foreach (KeyValuePair<string, IList<string>> columnCaptions in templateColumnCaptions)
                {
                    SheetView sheetView = new SheetView(columnCaptions.Key);
                    fsExcel.Sheets.Add(sheetView);
                    sheetView.RowCount = DEFAULT_TEMPLATE_ROW_COUNT;
                    sheetView.Columns.Count = columnCaptions.Value.Count;
                    int colIndex = 0;
                    sheetView.ColumnHeader.Rows[0].Height = ROW_HEIGHT_IN_SHEET_VIEW;
                    foreach (string columnCaption in columnCaptions.Value)
                    {
                        sheetView.Columns[colIndex].Width = COLUMN_WIDTH_IN_SHEET_VIEW;
                        sheetView.ColumnHeader.Cells[0, colIndex++].Text = columnCaption;
                    }
                    if (sheetView.RowCount > 0 && sheetView.ColumnCount > 0)
                    {
                        sheetView.ColumnHeader.Cells[0, 0, 0, sheetView.ColumnCount - 1].BackColor = Color.LightGray;
                    }
                }
                if (fileName.EndsWith("xlsx"))
                {
                    fsExcel.SaveExcel(fileName, ExcelSaveFlags.SaveCustomColumnHeaders | ExcelSaveFlags.UseOOXMLFormat);
                }
                else
                {
                    fsExcel.SaveExcel(fileName, ExcelSaveFlags.SaveCustomColumnHeaders);
                }
            }
        }

        /// <summary>
        /// 导出 Excel
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="fileName"></param>
        public static void ExprotExcel(FpSpread fsReporting, string fileName)
        {
            try
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.FileName = string.Format("{0}.xlsx", fileName);
                dlg.Filter = AppSettingHelper.DefaultExcelFormat;
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                if (File.Exists(dlg.FileName))
                {
                    if (MessageBox.Show("确认覆盖已存在的文件？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                    {
                        return;
                    }
                    File.Delete(dlg.FileName);
                }
                WinPlatformHelper.LastestFilePath = Path.GetDirectoryName(dlg.FileName);
                if (fsReporting.Sheets.Count == 0)
                {
                    MessageBox.Show("文件为空，无法导出！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                IList<bool> protects = new List<bool>(fsReporting.Sheets.Count);
                for (int i = 0; i < fsReporting.Sheets.Count; i++)
                {
                    protects.Add(fsReporting.Sheets[i].Protect);
                    fsReporting.Sheets[i].Protect = false;
                }
                if (dlg.FileName.EndsWith("xlsx"))
                {

                    fsReporting.SaveExcel(dlg.FileName, ExcelSaveFlags.SaveAsViewed | ExcelSaveFlags.UseOOXMLFormat);
                }
                else
                {
                    fsReporting.SaveExcel(dlg.FileName, ExcelSaveFlags.SaveAsViewed |ExcelSaveFlags.SaveAsFiltered);
                }
                for (int i = 0; i < fsReporting.Sheets.Count; i++)
                {
                    fsReporting.Sheets[i].Protect = protects[i];
                }
                MessageBox.Show(string.Format("Excel文件({0})导出成功。", dlg.FileName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 移除末尾空行
        /// </summary>
        /// <param name="spread"></param>
        public static void RemoveEmptyRows(FpSpread spread)
        {
            foreach (SheetView sheetView in spread.Sheets)
            {
                int rowIndex = sheetView.RowCount;
                for (int row = sheetView.RowCount - 1; row > 0; row--)
                {
                    bool rowEmpty = true;
                    for (int col = 0; col < sheetView.ColumnCount; col++)
                    {
                        if (!string.IsNullOrWhiteSpace(sheetView.Cells[row, col].Text.Trim()))
                        {
                            rowEmpty = false;
                            break;
                        }
                    }
                    if (rowEmpty)
                    {
                        rowIndex--;
                    }
                    else
                    {
                        break;
                    }
                }
                if (rowIndex < sheetView.RowCount)
                {
                    sheetView.Rows.Remove(rowIndex, sheetView.RowCount - rowIndex);
                }
            }
        }

        #endregion
    }
}
