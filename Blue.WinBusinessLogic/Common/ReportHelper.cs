using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.CellType;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.WinBusinessLogic
{
    /// <summary>
    /// 报表帮助类
    /// </summary>
    public sealed class ReportHelper
    {
        private readonly static PrintInfo printInfo;
        //private readonly static FpSpread printSpread;

        /// <summary>
        /// 
        /// </summary>
        static ReportHelper()
        {
            printInfo = new PrintInfo();
            printInfo.ShowColumnHeaders = false;
            printInfo.ShowRowHeaders = false;
            printInfo.ShowGrid = false;
            printInfo.ShowBorder = false;
            printInfo.ShowShadows = false;
            printInfo.ShowColor = true;
            printInfo.UseMax = false;
            printInfo.BestFitCols = false;

            printInfo.PrintType = FarPoint.Win.Spread.PrintType.CurrentPage;
            printInfo.Margin.Top = 20;
            printInfo.Margin.Bottom = 10;
            printInfo.Margin.Left = 20;
            printInfo.Margin.Right = 10;
            //printInfo.SmartPrintRules.Clear();
            //printInfo.SmartPrintRules.Add(new FarPoint.Win.Spread.ScaleRule(FarPoint.Win.Spread.ResetOption.None, 1, (float)0.1, (float)0.01));
            //printInfo.UseSmartPrint = true;
            printInfo.PageOrder = FarPoint.Win.Spread.PrintPageOrder.OverThenDown;
            printInfo.Orientation = FarPoint.Win.Spread.PrintOrientation.Portrait;

            //printSpread = new FpSpread();
        }

        /// <summary>
        /// 获得文件名
        /// </summary>
        /// <returns></returns>
        private static string GetFileName()
        {
            string fileName = string.Empty;
            bool exist = false;

            do
            {
                Random rd = new Random();
                int i = rd.Next();
                string dir = string.Format(@"{0}{1}", AppDomain.CurrentDomain.BaseDirectory, AppSettingHelper.DefaultClientTmpDirOfSavedFiles);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                fileName = string.Format(@"{0}\{1}.xlsx", dir, i);
                exist = File.Exists(fileName) ? true : false;
            } while (exist);

            return fileName;
        }

        /// <summary>
        /// 获得报表的数据
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <returns></returns>
        public static byte[] GetFpSpreadData(FpSpread fsReporting)
        {
            byte[] data = null;

            string fileName = GetFileName();
            fsReporting.SaveExcel(fileName, FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat);
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                BinaryReader r = new BinaryReader(fs);
                data = r.ReadBytes((int)fs.Length);
            }
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch { }

            return data;
        }

        /// <summary>
        /// 显示报表
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="data"></param>
        /// <param name="rowAndCols"></param>
        public static void ShowFpSpread(FpSpread fsReporting, byte[] data, IList<RowAndCol> rowAndCols)
        {
            if (data == null || data.Length == 0)
            {
                return;
            }
            string fileName = GetFileName();
            using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                fileStream.Write(data, 0, (int)data.Length);
                fileStream.Close();
            }
            fsReporting.OpenExcel(fileName);
            if (rowAndCols != null && rowAndCols.Count > 0)
            {
                for (int index = 0; index < rowAndCols.Count; index++)
                {
                    if (index >= fsReporting.Sheets.Count)
                    {
                        break;
                    }
                    fsReporting.Sheets[index].RowCount = rowAndCols[index].Row;
                    fsReporting.Sheets[index].ColumnCount = rowAndCols[index].Col;
                }
                if (rowAndCols.Count > 0)
                {
                    fsReporting.TabStripPolicy = TabStripPolicy.Always;
                }
                else
                {
                    fsReporting.TabStripPolicy = TabStripPolicy.Never;
                }
                //if (fsReporting.ActiveSheet != null && fsReporting.ActiveSheet.ColumnCount > 0 && fsReporting.ActiveSheet.RowCount > 0)
                //{
                //    fsReporting.ActiveSheet.SetActiveCell(0, 0);
                //}
            }
            fsReporting.TabStripInsertTab = false;
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch { }
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
                dlg.FileName = string.Format("{0}.xls", fileName);
                dlg.Filter = AppSettingHelper.DefaultExcelFormat;
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                if (File.Exists(dlg.FileName))
                {
                    File.Delete(dlg.FileName);
                }
                if (fsReporting.Sheets.Count == 0)
                {
                    MessageBox.Show("报表为空，无法导出！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

                    fsReporting.SaveExcel(dlg.FileName, FarPoint.Excel.ExcelSaveFlags.SaveAsViewed | FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat);
                }
                else
                {
                    fsReporting.SaveExcel(dlg.FileName, FarPoint.Excel.ExcelSaveFlags.SaveAsViewed | FarPoint.Excel.ExcelSaveFlags.SaveAsFiltered);
                }
                for (int i = 0; i < fsReporting.Sheets.Count; i++)
                {
                    fsReporting.Sheets[i].Protect = protects[i];
                }
                MessageBox.Show("导出成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="customMargin"></param>
        public static void Preview(FpSpread fsReporting, CustomMargin customMargin)
        {
            printInfo.PrintToPdf = false;
            printInfo.Preview = true;
            PrintReporting(fsReporting, customMargin);
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="customMargin"></param>
        public static void Print(FpSpread fsReporting, CustomMargin customMargin)
        {
            if (MessageBox.Show("确认打印？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                printInfo.PrintToPdf = false;
                printInfo.Preview = false;
                PrintReporting(fsReporting, customMargin);
            }
        }

        public static void PrintActiveSheet(FpSpread fsReporting, CustomMargin customMargin)
        {
            printInfo.PrintToPdf = false;
            printInfo.Preview = false;
            PrintReporting(fsReporting, customMargin);
        }

        /// <summary>
        /// 打印 PDF
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="customMargin"></param>
        public static void PrintPdf(FpSpread fsReporting, CustomMargin customMargin)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "print.pdf";
            dlg.Filter = "Adboe PDF 文件(*.pdf) |*.pdf";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            printInfo.Preview = false;
            printInfo.PrintToPdf = true;
            printInfo.PdfWriteMode = FarPoint.Win.Spread.PdfWriteMode.New;
            printInfo.PdfWriteTo = FarPoint.Win.Spread.PdfWriteTo.File;
            printInfo.PdfFileName = dlg.FileName;
            PrintReporting(fsReporting, customMargin);
        }

        /// <summary>
        /// 打印设置
        /// </summary>
        /// <param name="customSheetContract"></param>
        /// <param name="customSheetId"></param>
        public static void ShowPrintSettingForm(ICustomSheetContract customSheetContract, decimal customSheetId)
        {
            PrintSettingForm frmPrintSetting = new PrintSettingForm();
            frmPrintSetting.CustomSheetContract = customSheetContract;
            frmPrintSetting.CustomSheetId = customSheetId;
            frmPrintSetting.PrintInfo = printInfo;
            frmPrintSetting.ShowDialog();
        }

        /// <summary>
        /// 插入图片
        /// </summary>
        /// <param name="activeCell"></param>
        /// <param name="data"></param>
        public static void InsertPhoto(FpSpread fsReporting, Cell activeCell, byte[] data)
        {
            using (MemoryStream ms = new MemoryStream(data))
            {
                ImageCellType img = new ImageCellType(RenderStyle.Stretch);
                activeCell.CellType = img;
                activeCell.HorizontalAlignment = CellHorizontalAlignment.Center;
                activeCell.VerticalAlignment = CellVerticalAlignment.Center;
                activeCell.Value = new Bitmap((Image)new Bitmap(ms));
                if (fsReporting.ActiveSheet != null)
                {
                    if (fsReporting.ActiveSheet.PrintInfo == null)
                    {
                        PrintInfo info = new PrintInfo();
                        info.ShowColor = true;
                        fsReporting.ActiveSheet.PrintInfo = info;
                    }
                    else
                    {
                        fsReporting.ActiveSheet.PrintInfo.ShowColor = true;
                    }
                }
            }
        }

        /// <summary>
        /// 获得打印信息
        /// </summary>
        /// <param name="customMargin"></param>
        public static PrintInfo GetPrintInfo(CustomMargin customMargin)
        {
            PrintInfo currentPrintInfo = new PrintInfo();
            currentPrintInfo.Preview = false;
            currentPrintInfo.PrintToPdf = false;
            currentPrintInfo.ShowRowHeader = PrintHeader.Hide;
            currentPrintInfo.ShowColumnHeader = PrintHeader.Hide;
            currentPrintInfo.ShowColumnFooter = PrintHeader.Hide;
            currentPrintInfo.ShowGrid = false;
            currentPrintInfo.ShowBorder = false;            
            currentPrintInfo.ShowShadows = false;

            if (customMargin.Top > 0)
            {
                currentPrintInfo.Margin.Top = customMargin.Top;
            }
            else
            {
                currentPrintInfo.Margin.Top = 20;
            }
            if (customMargin.Bottom > 0)
            {
                currentPrintInfo.Margin.Bottom = customMargin.Bottom;
            }
            else
            {
                currentPrintInfo.Margin.Bottom = 10;
            }
            if (customMargin.Left > 0)
            {
                currentPrintInfo.Margin.Left = customMargin.Left;
            }
            else
            {
                currentPrintInfo.Margin.Left = 20;
            }
            if (customMargin.Right > 0)
            {
                currentPrintInfo.Margin.Right = customMargin.Right;
            }
            else
            {
                currentPrintInfo.Margin.Right = 10;
            }

            return currentPrintInfo;
        }

        /// <summary>
        /// 打印指定页面
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="customMargin"></param>
        public static void PrintCurrentSheet(FpSpread fsReporting, CustomMargin customMargin)
        {
            try
            {
                printInfo.PrintToPdf = false;
                printInfo.Preview = false;
                fsReporting.ActiveSheet.Cells[0, 0, fsReporting.ActiveSheet.RowCount - 1, fsReporting.ActiveSheet.ColumnCount - 1].BackColor = Color.White;

                if (customMargin.Top > 0)
                {
                    printInfo.Margin.Top = customMargin.Top;
                }
                else
                {
                    printInfo.Margin.Top = 20;
                }
                if (customMargin.Bottom > 0)
                {
                    printInfo.Margin.Bottom = customMargin.Bottom;
                }
                else
                {
                    printInfo.Margin.Bottom = 10;
                }
                if (customMargin.Left > 0)
                {
                    printInfo.Margin.Left = customMargin.Left;
                }
                else
                {
                    printInfo.Margin.Left = 20;
                }
                if (customMargin.Right > 0)
                {
                    printInfo.Margin.Right = customMargin.Right;
                }
                else
                {
                    printInfo.Margin.Right = 10;
                }
                fsReporting.ActiveSheet.PrintInfo = printInfo;
                fsReporting.PrintSheet(fsReporting.ActiveSheetIndex);

            }
            catch(Exception ex)
            {
                try
                {
                    byte[] data = GetFpSpreadData(fsReporting);
                    FpSpread spread = new FpSpread();
                    string fileName = GetFileName();
                    using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fileStream.Write(data, 0, (int)data.Length);
                        fileStream.Close();
                    }
                    spread.OpenExcel(fileName, FarPoint.Excel.ExcelOpenFlags.NoFlagsSet);
                    spread.Sheets[fsReporting.ActiveSheetIndex].RowCount = fsReporting.ActiveSheet.RowCount;
                    spread.Sheets[fsReporting.ActiveSheetIndex].ColumnCount = fsReporting.ActiveSheet.ColumnCount;
                    spread.Sheets[fsReporting.ActiveSheetIndex].Cells[0, 0, spread.Sheets[fsReporting.ActiveSheetIndex].RowCount - 1,
                        spread.Sheets[fsReporting.ActiveSheetIndex].ColumnCount - 1].BackColor = Color.White;
                    if (customMargin.Top > 0)
                    {
                        printInfo.Margin.Top = customMargin.Top;
                    }
                    else
                    {
                        printInfo.Margin.Top = 20;
                    }
                    if (customMargin.Bottom > 0)
                    {
                        printInfo.Margin.Bottom = customMargin.Bottom;
                    }
                    else
                    {
                        printInfo.Margin.Bottom = 10;
                    }
                    if (customMargin.Left > 0)
                    {
                        printInfo.Margin.Left = customMargin.Left;
                    }
                    else
                    {
                        printInfo.Margin.Left = 20;
                    }
                    if (customMargin.Right > 0)
                    {
                        printInfo.Margin.Right = customMargin.Right;
                    }
                    else
                    {
                        printInfo.Margin.Right = 10;
                    }
                    spread.Sheets[fsReporting.ActiveSheetIndex].PrintInfo = printInfo;
                    spread.PrintSheet(fsReporting.ActiveSheetIndex);
                    try
                    {
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }
                    }
                    catch { }
                }
                catch
                {
                    //MessageBox.Show("该表不是纯文本格式的样表(可能包含按钮、图片或者其他等Excel控件)，请重新设计样表格式后导入！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="customMargin"></param>
        private static void PrintReporting(FpSpread fsReporting, CustomMargin customMargin)
        {
            //FpSpread spread = new FpSpread();
            //SheetView newSheet = new SheetView("Sheet1");
            //string XMLString = Serializer.GetObjectXml((FarPoint.Win.ISerializeSupport)fsReporting.ActiveSheet, "Sheet1");
            //FarPoint.Win.Serializer.SetObjectXml((FarPoint.Win.ISerializeSupport)newSheet, XMLString, "Sheet1");
            //spread.Sheets.Add(newSheet);
            //for (int i = 0; i < spread.Sheets[0].RowCount; i++)
            //{
            //    for (int j = 0; j < spread.Sheets[0].ColumnCount; j++)
            //    {
            //        spread.Sheets[0].Cells[i, j].BackColor = Color.White;
            //    }
            //} 
            //SheetView newSheet = (FarPoint.Win.Spread.SheetView)FarPoint.Win.Serializer.LoadObjectXml(fsReporting.ActiveSheet.GetType(), FarPoint.Win.Serializer.GetObjectXml(fsReporting.ActiveSheet, "CopySheet"), "CopySheet");
            try
            {
                FpSpread spread = new FpSpread();
                spread.Sheets.Add(fsReporting.ActiveSheet.Clone());
                spread.Sheets[0].Cells[0, 0, spread.Sheets[0].RowCount - 1, spread.Sheets[0].ColumnCount - 1].BackColor = Color.White;
                //for (int i = 0; i < spread.Sheets[0].RowCount; i++)
                //{
                //    for (int j = 0; j < spread.Sheets[0].ColumnCount; j++)
                //    {
                //        spread.Sheets[0].Cells[i, j].BackColor = Color.White;
                //    }
                //}
                if (customMargin.Top > 0)
                {
                    printInfo.Margin.Top = customMargin.Top;
                }
                else
                {
                    printInfo.Margin.Top = 20;
                }
                if (customMargin.Bottom > 0)
                {
                    printInfo.Margin.Bottom = customMargin.Bottom;
                }
                else
                {
                    printInfo.Margin.Bottom = 10;
                }
                if (customMargin.Left > 0)
                {
                    printInfo.Margin.Left = customMargin.Left;
                }
                else
                {
                    printInfo.Margin.Left = 20;
                }
                if (customMargin.Right > 0)
                {
                    printInfo.Margin.Right = customMargin.Right;
                }
                else
                {
                    printInfo.Margin.Right = 10;
                }
                spread.Sheets[0].PrintInfo = printInfo;
                spread.PrintSheet(0);
            }
            catch
            {
                try
                {
                    byte[] data = GetFpSpreadData(fsReporting);
                    FpSpread spread = new FpSpread();
                    string fileName = GetFileName();
                    using (FileStream fileStream = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        fileStream.Write(data, 0, (int)data.Length);
                        fileStream.Close();
                    }
                    spread.OpenExcel(fileName, FarPoint.Excel.ExcelOpenFlags.NoFlagsSet);
                    spread.Sheets[fsReporting.ActiveSheetIndex].RowCount = fsReporting.ActiveSheet.RowCount;
                    spread.Sheets[fsReporting.ActiveSheetIndex].ColumnCount = fsReporting.ActiveSheet.ColumnCount;
                    spread.Sheets[fsReporting.ActiveSheetIndex].Cells[0, 0, spread.Sheets[fsReporting.ActiveSheetIndex].RowCount - 1,
                        spread.Sheets[fsReporting.ActiveSheetIndex].ColumnCount - 1].BackColor = Color.White;
                    if (customMargin.Top > 0)
                    {
                        printInfo.Margin.Top = customMargin.Top;
                    }
                    else
                    {
                        printInfo.Margin.Top = 20;
                    }
                    if (customMargin.Bottom > 0)
                    {
                        printInfo.Margin.Bottom = customMargin.Bottom;
                    }
                    else
                    {
                        printInfo.Margin.Bottom = 10;
                    }
                    if (customMargin.Left > 0)
                    {
                        printInfo.Margin.Left = customMargin.Left;
                    }
                    else
                    {
                        printInfo.Margin.Left = 20;
                    }
                    if (customMargin.Right > 0)
                    {
                        printInfo.Margin.Right = customMargin.Right;
                    }
                    else
                    {
                        printInfo.Margin.Right = 10;
                    }
                    spread.Sheets[fsReporting.ActiveSheetIndex].PrintInfo = printInfo;
                    spread.PrintSheet(fsReporting.ActiveSheetIndex);
                    try
                    {                        
                        if (File.Exists(fileName))
                        {
                            File.Delete(fileName);
                        }
                    }
                    catch { }
                }
                catch
                {
                    //MessageBox.Show("该表不是纯文本格式的样表(可能包含按钮、图片或者其他等Excel控件)，请重新设计样表格式后导入！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            //printInfo.ColStart = fsReporting.ActiveSheet.Models.Selection.AnchorColumn;
            //printInfo.ColEnd = fsReporting.ActiveSheet.Models.Selection.LeadColumn;
            //printInfo.RowStart = fsReporting.ActiveSheet.Models.Selection.AnchorRow;
            //printInfo.RowEnd = fsReporting.ActiveSheet.Models.Selection.LeadRow;   
            //printInfo.PrintToPdf = false;
            //printInfo.Preview = preview;
            //fsReporting.ActiveSheet.PrintInfo = printInfo;
            //fsReporting.PrintSheet(fsReporting.ActiveSheetIndex);    
        }
    }
}
