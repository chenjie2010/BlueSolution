using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Design;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using DevExpress.XtraBars;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using Blue.WinBusinessLogic;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class ReportingDesignerForm : Form
    {
        #region 契约接口

        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomReportContract customReportContract;
        private readonly ICustomSheetContract customSheetContract;
        private readonly ICustomCellContract customCellContract;

        #endregion

        #region 私有变量
        
        private CustomReportInfo customReportInfo = null;
        private IList<CustomSheetInfo> customSheetInfos = null;
        private bool changedStyle = false;

        #endregion

        #region 属性

        /// <summary>
        /// 编号
        /// </summary>
        public decimal ReportId
        {
            set;
            get;
        }

        /// <summary>
        /// 报表类型
        /// </summary>
        public ReportCategory ReportCategory
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReportingDesignerForm()
        {
            InitializeComponent();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
            customSheetContract = BusinessDesignerChannelFactory.CreateCustomSheetContract();
            customCellContract = BusinessDesignerChannelFactory.CreateCustomCellContract();
        }

        #endregion

        #region 窗体及控件方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportingDesignerForm_Load(object sender, EventArgs e)
        {
            /* 1. 窗体属性 */
            this.WindowState = FormWindowState.Maximized;

            /* 2. 初始化数据和控件属性 */
            customReportInfo = customReportContract.GetModelInfo(ReportId);            
            customSheetInfos = customSheetContract.GetModelInfos(ReportId);
            if (customSheetInfos == null || customSheetInfos.Count == 0)
            {
                SetMenuItemState(false);
                SetReportingProtectedProperty(0, true);
                fsReporting.TabStripPolicy = TabStripPolicy.Never;
            }
            else
            {
                byte[] data = customSheetContract.DownloadReportFile(ReportId);
                IList<RowAndCol> rowAndCols = customSheetContract.GetRowAndColCount(ReportId);
                ReportHelper.ShowFpSpread(fsReporting, data, rowAndCols);
                fsReporting.ActiveSheetIndex = 0;
                fsReporting.TabStripPolicy = TabStripPolicy.Always;
            }
            fsReporting.TabStripInsertTab = false;
            txtFormula.Attach(fsReporting);

            TextCellType t = new TextCellType();
            InputMap ancestorOfFocusedMap = fsReporting.GetInputMap(InputMapMode.WhenAncestorOfFocused);
            ActionMap am = fsReporting.GetActionMap();
            am.Put("AltEnter", new AltEnterAction());
            ancestorOfFocusedMap.Put(new Keystroke(Keys.Enter, Keys.None),
            FarPoint.Win.Spread.SpreadActions.MoveToNextRow);
            ancestorOfFocusedMap.Put(new FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Alt), "AltEnter");
            fsReporting.SetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused, ancestorOfFocusedMap);
            t.Multiline = true;

            openFileDialog.Filter = AppSettingHelper.DefaultExcelFormat;
            saveFileDialog.Filter = AppSettingHelper.DefaultExcelFormat;
            tlItmSave.Enabled = false;
            btnItmSave.Enabled = false;
        }

        /// <summary>
        /// 单击单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fsReporting_CellClick(object sender, CellClickEventArgs e)
        {
            changedStyle = false;
            Cell cell = fsReporting.ActiveSheet.Cells[e.Row, e.Column];
            if (e.Button == MouseButtons.Left && cell != null)
            {
                Font font = cell.Font;
                if (font != null)
                {
                    barItmFontName.EditValue = font.Name;
                    barItmFontSize.EditValue = font.Size;
                    tlItmBlod.Checked = font.Bold;
                    tlItmItalic.Checked = font.Italic;
                    tlItmUnderLine.Checked = font.Underline;
                    switch (fsReporting.ActiveSheet.ActiveCell.HorizontalAlignment)
                    {
                        case CellHorizontalAlignment.Left:
                            tlItmLeft.Checked = true;
                            tlItmMiddle.Checked = false;
                            tlItmRight.Checked = false;
                            break;

                        case CellHorizontalAlignment.Center:
                            tlItmLeft.Checked = false;
                            tlItmMiddle.Checked = true;
                            tlItmRight.Checked = false;
                            break;

                        case CellHorizontalAlignment.Right:
                            tlItmLeft.Checked = false;
                            tlItmMiddle.Checked = false;
                            tlItmRight.Checked = true;
                            break;
                    }
                    switch (fsReporting.ActiveSheet.ActiveCell.VerticalAlignment)
                    {
                        case CellVerticalAlignment.Top:
                            tlItmTop.Checked = true;
                            tlItmCenter.Checked = false;
                            tlItmBottom.Checked = false;
                            break;

                        case CellVerticalAlignment.Center:
                            tlItmTop.Checked = false;
                            tlItmCenter.Checked = true;
                            tlItmBottom.Checked = false;
                            break;

                        case CellVerticalAlignment.Bottom:
                            tlItmTop.Checked = false;
                            tlItmCenter.Checked = false;
                            tlItmBottom.Checked = true;
                            break;
                    }
                }
                else
                {
                    barItmFontName.EditValue = null;
                    barItmFontSize.EditValue = null;
                    tlItmBlod.Checked = false;
                    tlItmItalic.Checked = false;
                    tlItmUnderLine.Checked = false;
                    tlItmLeft.Checked = false;
                    tlItmMiddle.Checked = false;
                    tlItmRight.Checked = false;
                    tlItmTop.Checked = false;
                    tlItmCenter.Checked = false;
                    tlItmBottom.Checked = false;
                }
                if (fsReporting.ActiveSheet.ActiveCell.HorizontalAlignment == CellHorizontalAlignment.Center &&
                        fsReporting.ActiveSheet.ActiveCell.VerticalAlignment == CellVerticalAlignment.Center)
                {
                    tlItmCellCenter.Checked = true;
                }
                else
                {
                    tlItmCellCenter.Checked = false;
                }
                if (fsReporting.ActiveSheet.ActiveCell.CellType != null && (fsReporting.ActiveSheet.ActiveCell.CellType is TextCellType || fsReporting.ActiveSheet.ActiveCell.CellType is GeneralCellType))
                {
                    if (fsReporting.ActiveSheet.ActiveCell.CellType is TextCellType)
                    {
                        TextCellType inputCellType = fsReporting.ActiveSheet.ActiveCell.CellType as TextCellType;
                        tlItmWarp.Checked = inputCellType.WordWrap;
                    }
                    if (fsReporting.ActiveSheet.ActiveCell.CellType is GeneralCellType)
                    {
                        GeneralCellType inputCellType = fsReporting.ActiveSheet.ActiveCell.CellType as GeneralCellType;
                        tlItmWarp.Checked = inputCellType.WordWrap;
                    }
                }
                else
                {
                    tlItmWarp.Checked = false;
                }
                CellRange cr = fsReporting.ActiveSheet.GetSpanCell(cell.Row.Index, cell.Column.Index);
                if (cr != null)
                {
                    tlItmMerge.Checked = true;
                }
                else
                {
                    tlItmMerge.Checked = false;
                }
                if (fsReporting.ActiveSheet.ActiveCell != null)
                {
                    etxtPosition.Text = fsReporting.ActiveSheet.ActiveCell.Column.Label + fsReporting.ActiveSheet.ActiveCell.Row.Label;
                }
            }
            changedStyle = true;
        }

        /// <summary>
        /// 双击单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fsReporting_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            CustomCellInfo customCellInfo = GetCustomCellInfo();
            if (customCellInfo != null)
            {
                IList<CustomCellInfo> customCellInfos = new List<CustomCellInfo>();
                customCellInfos.Add(customCellInfo);
                ShowCellDataSource(customCellInfos);
            }
        }

        /// <summary>
        /// 单元格内容发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fsReporting_EditChange(object sender, EditorNotifyEventArgs e)
        {
            tlItmSave.Enabled = true;
            btnItmSave.Enabled = true;
        }

        #endregion

        #region 样表

        /// <summary>
        /// 样表管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmSampleManagement_ItemClick(object sender, ItemClickEventArgs e)
        {
            ManageSample();
        }

        /// <summary>
        /// 增加样表
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="rowCount"></param>
        /// <param name="columnCount"></param>
        private void AddSheet(string sheetName, int rowCount, int columnCount)
        {
            if (customSheetInfos.Count == 0)
            {
                fsReporting.TabStripPolicy = TabStripPolicy.Always;
                fsReporting.Sheets[0].SheetName = sheetName;
                fsReporting.Sheets[0].RowCount = rowCount;
                fsReporting.Sheets[0].ColumnCount = columnCount;
                SetMenuItemState(true);
                SetReportingProtectedProperty(0, false);
            }
            else
            {
                SheetView sheetView = new SheetView(sheetName);
                sheetView.RowCount = rowCount;
                sheetView.ColumnCount = columnCount;
                fsReporting.Sheets.Add(sheetView);
                fsReporting.ActiveSheet = sheetView;
            }
            customSheetInfos = customSheetContract.GetModelInfos(ReportId);
            SaveReport();
        }

        /// <summary>
        /// 编辑样表
        /// </summary>
        /// <param name="sheetIndex"></param>
        /// <param name="sheetName"></param>
        private void EditSheet(int sheetIndex, string sheetName)
        {
            if (sheetIndex < fsReporting.Sheets.Count)
            {
                fsReporting.Sheets[sheetIndex].SheetName = sheetName;
            }
            Application.DoEvents();
            SaveReport();
        }

        /// <summary>
        /// 删除样表
        /// </summary>
        /// <param name="sheetIndex"></param>
        private void DeleteSheet(int sheetIndex)
        {
            if (sheetIndex < fsReporting.Sheets.Count && sheetIndex >= 0)
            {
                if (fsReporting.Sheets.Count > 1)
                {
                    fsReporting.Sheets.RemoveAt(sheetIndex);
                    fsReporting.ActiveSheetIndex = sheetIndex > 0 ? sheetIndex - 1 : 0;

                }
                else
                {
                    fsReporting.Sheets.Add(new SheetView());
                    fsReporting.Sheets.RemoveAt(0);
                    fsReporting.ActiveSheet.Reset();
                    fsReporting.TabStripPolicy = TabStripPolicy.Never;
                }
                customSheetInfos = customSheetContract.GetModelInfos(ReportId);
                SaveReport();
            }
        }

        /// <summary>
        /// 移动样表
        /// </summary>
        /// <param name="currentSheetIndex"></param>
        /// <param name="previousSheetIndex"></param>
        /// <param name="movedDriection"></param>
        public void MoveSheet(int currentSheetIndex, int previousSheetIndex, MovedDriection movedDriection)
        {
            if (currentSheetIndex < fsReporting.Sheets.Count)
            {
                int newSheetIndex = currentSheetIndex;
                switch (movedDriection)
                {
                    case MovedDriection.Top:
                        newSheetIndex = 0;
                        break;

                    case MovedDriection.Previous:
                        if (currentSheetIndex < 0 || previousSheetIndex > fsReporting.Sheets.Count - 1)
                        {
                            previousSheetIndex = currentSheetIndex;
                        }
                        else
                        {
                            newSheetIndex = currentSheetIndex;
                        }
                        break;

                    case MovedDriection.Next:
                        if (previousSheetIndex < 0 || currentSheetIndex > fsReporting.Sheets.Count - 1)
                        {
                            previousSheetIndex = currentSheetIndex;
                        }
                        else
                        {
                            newSheetIndex = currentSheetIndex;
                        }
                        break;

                    case MovedDriection.Bottom:
                        newSheetIndex = fsReporting.Sheets.Count - 1;
                        break;
                }
                if (newSheetIndex != previousSheetIndex)
                {
                    fsReporting.Sheets.Move(previousSheetIndex, newSheetIndex);
                    customSheetInfos = customSheetContract.GetModelInfos(ReportId);
                    SaveReport();
                }
            }
        }

        /// <summary>
        /// 导入EXCEL文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(openFileDialog.InitialDirectory))
            {
                openFileDialog.InitialDirectory = Application.StartupPath;
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(openFileDialog.FileName) && Directory.Exists(Path.GetDirectoryName(openFileDialog.FileName)))
                {
                    openFileDialog.InitialDirectory = Path.GetDirectoryName(openFileDialog.FileName);
                }
            }
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (MessageBox.Show("确定通过追加的方式将样表追加到当前表套中吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    try
                    {
                        Cursor = Cursors.WaitCursor;
                        
                        IList<string> childNodeCodes = customSheetContract.GetChildNodeCodes(ReportId);
                        int index = 1;
                        IList<CustomSheetInfo> sheetNames = new List<CustomSheetInfo>();
                        using (FpSpread fpSpread = new FpSpread())
                        {
                            fpSpread.OpenExcel(openFileDialog.FileName, FarPoint.Excel.ExcelOpenFlags.TruncateEmptyRowsAndColumns);
                            foreach (SheetView sheetView in fpSpread.Sheets)
                            {
                                if (sheetView.SheetName.StartsWith("Sheet"))
                                {
                                    if (fsReporting.TabStripPolicy == TabStripPolicy.Never)
                                    {
                                        sheetView.SheetName = string.Format("Sheet_{0}", fsReporting.Sheets.Count);
                                    }
                                    else
                                    {
                                        sheetView.SheetName = string.Format("Sheet_{0}", fsReporting.Sheets.Count + 1);
                                    }
                                }
                                bool exist = false;
                                foreach (SheetView view in fsReporting.Sheets)
                                {
                                    if (sheetView.SheetName.Equals(view.SheetName))
                                    {
                                        exist = true;
                                    }
                                }
                                if (exist)
                                {
                                    sheetView.SheetName = string.Format("Sheet_{0}", fsReporting.Sheets.Count);
                                }
                                fsReporting.Sheets.Add(sheetView);
                                string sheetCode = string.Empty;
                                do
                                {
                                    sheetCode = UserDataHelper.GetTreeCode(customReportInfo.ReportCode, index++);
                                } while (childNodeCodes.Contains(sheetCode));
                                sheetNames.Add(new CustomSheetInfo(0, 0, sheetView.SheetName, sheetCode,
                                    string.Empty, sheetView.RowCount, sheetView.ColumnCount, 0, 0, 0, 0, 1, 0, string.Empty));
                            }
                            if (fsReporting.TabStripPolicy == TabStripPolicy.Never && fsReporting.Sheets.Count > 0)
                            {
                                fsReporting.Sheets.RemoveAt(0);
                            }
                            fsReporting.ActiveSheetIndex = 0;
                            fsReporting.TabStripPolicy = TabStripPolicy.Always;
                            if (fpSpread.Sheets.Count > 0)
                            {
                                SetMenuItemState(true);
                                SetReportingProtect(false);
                            }
                        }
                        customSheetContract.Insert(ReportId, sheetNames);
                        customSheetInfos = customSheetContract.GetModelInfos(ReportId);
                        SaveReport();
                        Cursor = Cursors.Default;
                        MessageBox.Show("Excel 文件导入成功，该窗口即将自动关闭，请重新打开！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }

                    catch (Exception exception)
                    {
                        Cursor = Cursors.Default;
                        //记录日志, 不抛出异常, 包装异常
                        WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                    }
                }
            }
        }

        /// <summary>
        /// 导出 Excel 文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    string fileName = saveFileDialog.FileName;
                    SetReportingProtect(false);
                    if (fileName.EndsWith("xlsx"))
                    {
                        fsReporting.SaveExcel(fileName, FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat);
                    }
                    else
                    {
                        fsReporting.SaveExcel(fileName, FarPoint.Excel.ExcelSaveFlags.SaveAsViewed);
                    }
                    Cursor = Cursors.Default;
                    MessageBox.Show("报表格式导出成功!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 打印设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPrintSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            ReportHelper.ShowPrintSettingForm(customSheetContract, customSheetInfos[fsReporting.ActiveSheetIndex].SheetId);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CustomMargin customMargin = customSheetContract.GetMargin(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId);
            ReportHelper.Preview(fsReporting, customMargin);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 通用的打印表格方法
        /// </summary>
        /// <param name="fpview"></param>
        /// <param name="fp"></param>
        /// <param name="index"></param>
        public static void CommonPrint(SheetView fpview, FpSpread fp, int index)
        {
            try
            {
                if (fpview.RowCount == 0)
                    return;
                PrintInfo pi = new PrintInfo();
                DialogResult result = MessageBox.Show("是否要横向打印?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                StyleInfo style = new StyleInfo();
                style.Border = new FarPoint.Win.LineBorder(Color.Black, 1);
                style.BackColor = Color.White;
                fpview.ColumnHeader.DefaultStyle = style;
                fpview.RowHeader.DefaultStyle = style;
                if (result == DialogResult.Yes)
                {
                    pi.Orientation = PrintOrientation.Landscape;
                }
                else
                    pi.Orientation = PrintOrientation.Portrait;
                PrintMargin pm = new PrintMargin();
                pm.Left = 100;
                pm.Right = 60;
                pm.Top = 100;
                pm.Bottom = 20;
                pi.FirstPageNumber = 1;
                pi.Footer = "当前第 /p 页/n共 /pc 页";
                pi.Margin = pm;
                pi.PageStart = 1;
                pi.Preview = true;
                pi.ShowBorder = true;
                pi.ShowColor = false;
                pi.ShowColumnHeaders = true;
                pi.ShowGrid = true;
                pi.ShowPrintDialog = true;
                pi.ShowRowHeaders = false;
                pi.ShowShadows = true;
                pi.ZoomFactor = 1;
                pi.ShowPrintDialog = true;
                PrintInfo clone = new PrintInfo(pi);
                fpview.PrintInfo = clone;
                fp.PrintSheet(index);
            }
            catch
            {
                MessageBox.Show("打印发生错误,请确认是否有连接好打印机");
            }
        }

        /// <summary>
        /// 打印当前的sheet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            CommonNode commonNode = fsReporting.ActiveSheet.Tag as CommonNode;
            if (commonNode != null)
            {
                Cursor = Cursors.WaitCursor;
                CustomMargin customMargin = customSheetContract.GetMargin(commonNode.NodeId);
                ReportHelper.Print(fsReporting, customMargin);
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 退出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmExit_ItemClick(object sender, ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 打印预览事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fsReporting_PrintPreviewShowing(object sender, PrintPreviewShowingEventArgs e)
        {

            //e.PreviewDialog.
            //e.Preview.StartPosition = FormStartPosition.CenterScreen;
        }


        /// <summary>
        /// 保存表套文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveReport();
        }

        ///// <summary>
        ///// 刷新样表
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void btnItmRefresh_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    try
        //    {
        //        Cursor = Cursors.WaitCursor;
        //        Cursor = Cursors.Default;
        //    }
        //    catch (Exception exception)
        //    {
        //        Cursor = Cursors.Default;
        //        //记录日志, 不抛出异常, 包装异常
        //        WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
        //    }
        //}

        /// <summary>
        /// 保存表套
        /// </summary>
        private void SaveReport()
        {
            byte[] data = ReportHelper.GetFpSpreadData(fsReporting);
            int count = customSheetInfos.Count <= fsReporting.Sheets.Count ? customSheetInfos.Count : fsReporting.Sheets.Count;
            if (count > 0)
            {
                Dictionary<decimal, RowAndCol> rowAndCols = new Dictionary<decimal, RowAndCol>(count);
                for (int index = 0; index < fsReporting.Sheets.Count; index++)
                {
                    if ((customSheetInfos[index].SheetRowCount != fsReporting.Sheets[index].RowCount)
                        || (customSheetInfos[index].SheetColCount != fsReporting.Sheets[index].ColumnCount))
                    {
                        rowAndCols.Add(customSheetInfos[index].SheetId, new RowAndCol(fsReporting.Sheets[index].RowCount, fsReporting.Sheets[index].ColumnCount));
                    }
                }
                customSheetContract.UploadReportFile(ReportId, data, rowAndCols);
            }
            tlItmSave.Enabled = false;
            btnItmSave.Enabled = false;
        }

        #endregion

        #region 编辑

        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            Copy();
        }

        /// <summary>
        /// 剪切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmCut_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cut();
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPaste_ItemClick(object sender, ItemClickEventArgs e)
        {
            Paste();
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        /// <summary>
        /// 添加行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmAddRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddRow();
        }

        /// <summary>
        /// 增加列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmAddCol_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddColumn();
        }

        /// <summary>
        /// 插入行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmInsertRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertRow();
        }

        /// <summary>
        /// 插入列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmInsertCol_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertColumn();
        }

        /// <summary>
        /// 删除行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmDeleteRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteRow();
        }

        /// <summary>
        /// 删除列
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmDeleteCol_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteColumn();
        }

        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmFind_ItemClick(object sender, ItemClickEventArgs e)
        {
            Find();
        }

        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmReplace_ItemClick(object sender, ItemClickEventArgs e)
        {
            Replace();
        }


        #endregion

        #region 格式

        /// <summary>
        /// 设置表头颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmHeaderSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.CellColor, Color.LightBlue);
        }

        /// <summary>
        /// 取消表头颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmHeaderRemoving_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fsReporting.ActiveSheet.ActiveCell.BackColor == Color.LightBlue)
            {
                SetCellSelectionStyle(CellSelectionStyle.CellColor, Color.White);
            }
        }

        /// <summary>
        /// 设置字体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmFont_ItemClick(object sender, ItemClickEventArgs e)
        {
            string fontName;
            float fontSize;
            int fontBold, fontItalic, fontStrikeout, fontUnderline;
            int fontColor;

            Cell cell = fsReporting.ActiveSheet.ActiveCell;
            if (cell.Font != null)
            {
                fontName = cell.Font.Name;
                fontSize = cell.Font.Size;
                fontBold = (cell.Font.Bold == true ? 1 : 0);
                fontItalic = (cell.Font.Italic == true ? 1 : 0);
                fontUnderline = (cell.Font.Underline == true ? 1 : 0);
                fontStrikeout = (cell.Font.Strikeout == true ? 1 : 0);
                fontColor = cell.ForeColor.ToArgb();

                int nStyle = 0;// = FontStyle.Regular;
                if (fontBold == 1)
                {
                    nStyle += (int)FontStyle.Bold;
                }
                if (fontItalic == 1)
                {
                    nStyle += (int)FontStyle.Italic;
                }
                if (fontUnderline == 1)
                {
                    nStyle += (int)FontStyle.Underline;
                }
                if (fontStrikeout == 1)
                {
                    nStyle += (int)FontStyle.Strikeout;
                }

                fontDialog.Font = new Font(fontName, fontSize, (FontStyle)nStyle);

                fontDialog.Color = Color.FromArgb(fontColor);
                fontDialog.ShowEffects = true;
            }
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                SetCellSelectionStyle(CellSelectionStyle.Font, fontDialog.Font);
            }
        }

        /// <summary>
        /// 添加数据单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmDataCell_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddDataCell();
        }

        /// <summary>
        /// 删除数据单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmDeletedCell_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteDataCell();
        }

        /// <summary>
        /// 数据单元格来源
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmCellDataSource_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowCellDataSource();
        }

        #endregion

        #region 工具栏

        private void tlItmNew_ItemClick(object sender, ItemClickEventArgs e)
        {
            ManageSample();
        }

        private void tlItmEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            ManageSample();
        }

        private void tlItmDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            ManageSample();
        }

        private void tlItmSave_ItemClick(object sender, ItemClickEventArgs e)
        {
            SaveReport();
        }

        private void tlItmPreview_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CustomMargin customMargin = customSheetContract.GetMargin(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId);
            ReportHelper.Preview(fsReporting, customMargin);
            Cursor = Cursors.Default;
        }

        private void tlItmPrint_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            CustomMargin customMargin = customSheetContract.GetMargin(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId);
            ReportHelper.Print(fsReporting, customMargin);
            Cursor = Cursors.Default;
        }

        private void tlItmCut_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cut();
        }

        private void tlItmCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            Copy();
        }

        private void tlItmPaste_ItemClick(object sender, ItemClickEventArgs e)
        {
            Paste();
        }

        private void tlItmClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void barItmFontName_EditValueChanged(object sender, EventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.FontName, barItmFontName.EditValue);
        }

        private void barItmFontSize_EditValueChanged(object sender, EventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.FontSize, barItmFontSize.EditValue);
        }

        private void tlItmBlod_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.FontBlod, null);
        }

        private void tlItmItalic_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.FontItalic, null);
        }

        private void tlItmUnderLine_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.FontUnderline, null);
        }

        private void tlItmLeft_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.Left, null);
        }

        private void tlItmMiddle_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.Middle, null);
        }

        private void tlItmRight_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.Right, null);
        }

        private void tlItmTop_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.Top, null);
        }

        private void tlItmCenter_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.Center, null);
        }

        private void tlItmBottom_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.Bottom, null);
        }

        private void tlItmCellCenter_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.MiddleCenter, null);
        }

        /// <summary>
        /// 是否显示网格操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlItmGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.Grid, null);
        }

        private void tlItmWarp_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.AutoLine, null);
        }

        /// <summary>
        /// 插入当前时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlItmDateTime_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (fsReporting.ActiveSheet.ActiveCell != null)
            {
                DateTimeCellType inputCellType = new DateTimeCellType();
                inputCellType.DateTimeFormat = DateTimeFormat.UserDefined;
                inputCellType.UserDefinedFormat = "yyyy-MM-dd";
                fsReporting.ActiveSheet.ActiveCell.CellType = inputCellType;
                fsReporting.ActiveSheet.ActiveCell.Formula = "TODAY()";
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            }
        }

        /// <summary>
        /// 是否显示网格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlItmGrid_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            fsReporting.ActiveSheet.PrintInfo.ShowGrid = tlItmGrid.Checked;
        }

        /// <summary>
        /// 边框设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlItmBroder_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetSheetBroder();
        }

        /// <summary>
        /// 边框设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmBorader_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetSheetBroder();
        }

        /// <summary>
        /// 设置行高
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmRowHeight_ItemClick(object sender, ItemClickEventArgs e)
        {
            RowAndColSettingForm frmRowAndColSetting = new RowAndColSettingForm();
            frmRowAndColSetting.IsRowOrCol = true;
            frmRowAndColSetting.SetRowHeightAndColWidth = (value) => { SetCellSelectionStyle(CellSelectionStyle.RowHeight, Convert.ToSingle(value)); };
            frmRowAndColSetting.ShowDialog();
        }

        /// <summary>
        /// 设置列宽
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmColWidth_ItemClick(object sender, ItemClickEventArgs e)
        {
            RowAndColSettingForm frmRowAndColSetting = new RowAndColSettingForm();
            frmRowAndColSetting.IsRowOrCol = false;
            frmRowAndColSetting.SetRowHeightAndColWidth = (value) => { SetCellSelectionStyle(CellSelectionStyle.ColumnWidth, Convert.ToSingle(value)); };
            frmRowAndColSetting.ShowDialog();
        }

        /// <summary>
        /// 设置最大行列数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmMaxRowCol_ItemClick(object sender, ItemClickEventArgs e)
        {
            RowAndColIndexForm frmRowAndColIndex = new RowAndColIndexForm();
            frmRowAndColIndex.MaxOrFix = true;
            frmRowAndColIndex.OldRows = fsReporting.ActiveSheet.RowCount;
            frmRowAndColIndex.OldCols = fsReporting.ActiveSheet.ColumnCount;
            frmRowAndColIndex.SetRowAndColCount = (rows, cols)
                =>
                {
                    fsReporting.ActiveSheet.RowCount = rows;
                    fsReporting.ActiveSheet.ColumnCount = cols;
                    SaveReport();
                    customSheetContract.Update(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, rows, cols);
                };
            frmRowAndColIndex.ShowDialog();
        }

        /// <summary>
        /// 设置固定行列数
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmFixedRowCol_ItemClick(object sender, ItemClickEventArgs e)
        {
            RowAndColIndexForm frmRowAndColIndex = new RowAndColIndexForm();
            frmRowAndColIndex.MaxOrFix = false;
            frmRowAndColIndex.OldRows = fsReporting.ActiveSheet.FrozenRowCount;
            frmRowAndColIndex.OldCols = fsReporting.ActiveSheet.FrozenColumnCount;
            frmRowAndColIndex.SetRowAndColCount = (rows, cols)
                =>
                {
                    fsReporting.ActiveSheet.FrozenRowCount = rows;
                    fsReporting.ActiveSheet.FrozenColumnCount = cols;
                    tlItmSave.Enabled = true;
                    btnItmSave.Enabled = true;
                };
            frmRowAndColIndex.ShowDialog();

        }

        /// <summary>
        /// 显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmShow_ItemClick(object sender, ItemClickEventArgs e)
        {
            OptionShowForm frmOptionShow = new OptionShowForm();
            frmOptionShow.SetColHeaderVisible = (visible) =>
            {
                fsReporting.ActiveSheet.ColumnHeader.Visible = visible;
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            };
            frmOptionShow.SetRowHeaderVisible = (visible) =>
            {
                fsReporting.ActiveSheet.RowHeader.Visible = visible;
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            };
            frmOptionShow.SetZeroVisible = (visible) =>
            {
                fsReporting.ActiveSheet.DisplayZero = visible;
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            };
            frmOptionShow.SetGridLineVisible = (visible) =>
            {
                if (visible)
                {
                    fsReporting.ActiveSheet.HorizontalGridLine = new GridLine(GridLineType.Flat);
                    fsReporting.ActiveSheet.VerticalGridLine = new GridLine(GridLineType.Flat);
                }
                else
                {
                    fsReporting.ActiveSheet.HorizontalGridLine = new GridLine(GridLineType.None);
                    fsReporting.ActiveSheet.VerticalGridLine = new GridLine(GridLineType.None);
                }
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            };
            frmOptionShow.IsShowColHeader = fsReporting.ActiveSheet.ColumnHeader.Visible;
            frmOptionShow.IsShowRowHeader = fsReporting.ActiveSheet.RowHeader.Visible;
            if (fsReporting.ActiveSheet.VerticalGridLine.Type == GridLineType.None)
            {
                frmOptionShow.IsShowGridLine = false;
            }
            else
            {
                frmOptionShow.IsShowGridLine = true;
            }
            frmOptionShow.IsShowZero = fsReporting.ActiveSheet.DisplayZero;
            frmOptionShow.ShowDialog();
        }

        /// <summary>
        /// 格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmCellFormat_ItemClick(object sender, ItemClickEventArgs e)
        {
            CellFormatForm frmCellFormat = new CellFormatForm();
            frmCellFormat.SetCellType = (inputCellType)
                =>
            {
                SetCellSelectionStyle(CellSelectionStyle.Format, inputCellType);
            };
            frmCellFormat.ShowDialog();
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlItmMerge_ItemClick(object sender, ItemClickEventArgs e)
        {
            bool include = IsIncludeDataCellInCellRange();
            if (include)
            {
                MessageBox.Show("不能对包含数据单元格进行合并操作！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CellRange cr = fsReporting.ActiveSheet.GetSelection(0);
            if (cr != null)
            {
                CellRange cellRange = fsReporting.ActiveSheet.GetSpanCell(cr.Row, cr.Column);
                if (cellRange == null)
                {
                    fsReporting.ActiveSheet.AddSpanCell(cr.Row, cr.Column, cr.RowCount, cr.ColumnCount);
                }
                else
                {
                    fsReporting.ActiveSheet.RemoveSpanCell(cr.Row, cr.Column);
                }
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            }
        }

        /// <summary>
        /// 自适应调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlItmAdjust_ItemClick(object sender, ItemClickEventArgs e)
        {
            SetCellSelectionStyle(CellSelectionStyle.Adjustment, null);
            tlItmSave.Enabled = true;
            btnItmSave.Enabled = true;
        }

        #endregion

        #region 右键菜单

        private void pbtnItmCut_ItemClick(object sender, ItemClickEventArgs e)
        {
            Cut();
        }

        private void pbtnItmCopy_ItemClick(object sender, ItemClickEventArgs e)
        {
            Copy();
        }

        private void pbtnItmPaste_ItemClick(object sender, ItemClickEventArgs e)
        {
            Paste();
        }

        private void pbtnItmClear_ItemClick(object sender, ItemClickEventArgs e)
        {
            Clear();
        }

        private void pbtnItmAddRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddRow();
        }

        private void pbtnItmAddColumn_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddColumn();
        }

        private void pbtnItmInsertRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertRow();
        }

        private void pbtnItmInsertCol_ItemClick(object sender, ItemClickEventArgs e)
        {
            InsertColumn();
        }

        private void pbtnItmDeleteRow_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteRow();
        }

        private void pbtnItmDeleteCol_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteColumn();
        }

        private void pbtnItmDataCell_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddDataCell();
        }

        private void pbtnItmDeletedCell_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteDataCell();
        }

        private void pbtnItmCellDataSource_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowCellDataSource();
        }

        #endregion

        #region 控件私有方法

        private void Find()
        {
            //fsReporting.SearchWithDialog(fsReporting.ActiveSheetIndex, "", false, false, false, false, 0, 0);
            FindAndReplaceForm frmFindAndReplace = new FindAndReplaceForm();
            frmFindAndReplace.Owner = this;
            frmFindAndReplace.FpSpread = fsReporting;
            frmFindAndReplace.Show();
        }

        private void Replace()
        {
            ///fsReporting.SearchWithDialogAdvanced(fsReporting.ActiveSheetIndex, fsReporting.ActiveSheetIndex, "", 
            // false, false, false, false, 0, 0);
            //FindAndReplaceForm frmFindAndReplace = new FindAndReplaceForm();
            //frmFindAndReplace.Owner = this;
            //frmFindAndReplace.ActiveSheet = fsReporting.ActiveSheet;

            //frmFindAndReplace.Show();
        }

        /// <summary>
        /// 增加行
        /// </summary>
        private void AddRow()
        {
            try
            {
                fsReporting.ActiveSheet.AddRows(fsReporting.ActiveSheet.RowCount, 1);
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 增加列
        /// </summary>
        private void AddColumn()
        {
            try
            {
                fsReporting.ActiveSheet.AddColumns(fsReporting.ActiveSheet.ColumnCount, 1);
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 插入行
        /// </summary>
        private void InsertRow()
        {
            try
            {
                if (IsIncludeExtendDataCellInRowAndColRange(CellRowAndCol.Row, true))
                {
                    MessageBox.Show("不能对包含行扩展或是列扩展数据单元格的范围内进行插入行！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UpdateDataCellRowAndCol(true, CellRowAndCol.Row);
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 插入列
        /// </summary>
        private void InsertColumn()
        {
            try
            {
                if (IsIncludeExtendDataCellInRowAndColRange(CellRowAndCol.Col, true))
                {
                    MessageBox.Show("不能对包含行扩展或是列扩展数据单元格的范围内进行插入列！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                UpdateDataCellRowAndCol(true, CellRowAndCol.Col);
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 删除行
        /// </summary>
        private void DeleteRow()
        {
            try
            {
                if (IsIncludeExtendDataCellInRowAndColRange(CellRowAndCol.Row, false))
                {
                    MessageBox.Show("不能对包含行扩展或是列扩展数据单元格的范围内进行删除行！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool include = IsIncludeDataCellInRowAndColRange(CellRowAndCol.Row);
                if (include)
                {
                    MessageBox.Show("不能对包含数据单元格进行删除行操作！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认删除当前行吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    UpdateDataCellRowAndCol(false, CellRowAndCol.Row);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 删除列
        /// </summary>
        private void DeleteColumn()
        {
            try
            {
                if (IsIncludeExtendDataCellInRowAndColRange(CellRowAndCol.Col, false))
                {
                    MessageBox.Show("不能对包含行扩展或是列扩展数据单元格的范围内进行删除列！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                bool include = IsIncludeDataCellInRowAndColRange(CellRowAndCol.Col);
                if (include)
                {
                    MessageBox.Show("不能对包含数据单元格进行删除列操作！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("确认删除当前列吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    UpdateDataCellRowAndCol(false, CellRowAndCol.Col);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 复制
        /// </summary>
        private void Copy()
        {
            try
            {
                bool include = IsIncludeDataCellInCellRange();
                if (include)
                {
                    MessageBox.Show("不能对包含数据单元格进行粘贴操作！！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                fsReporting.ActiveSheet.ClipboardCopy();
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        private void Paste()
        {
            try
            {
                bool include = IsIncludeDataCellInCellRange();
                if (include)
                {
                    MessageBox.Show("不能对包含数据单元格进行粘贴操作！！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                fsReporting.ActiveSheet.ClipboardPaste(FarPoint.Win.Spread.ClipboardPasteOptions.Values);
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 剪切
        /// </summary>
        private void Cut()
        {
            try
            {
                bool include = IsIncludeDataCellInCellRange();
                if (include)
                {
                    MessageBox.Show("不能对包含数据单元格进行剪切操作！！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                fsReporting.ActiveSheet.ClipboardCut();
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 清除
        /// </summary>
        private void Clear()
        {
            bool include = IsIncludeDataCellInCellRange();
            if (include)
            {
                MessageBox.Show("不能对包含数据单元格进行清除操作！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("确认要清除单元格的格式与内容?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                SetCellSelectionStyle(CellSelectionStyle.ResetCellText, null);
                tlItmSave.Enabled = true;
                btnItmSave.Enabled = true;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            try
            {
                fsReporting.ActiveSheet.ClearSelection();
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 设置是否受保护
        /// </summary>
        /// <param name="protect"></param>
        private void SetReportingProtect(bool protect)
        {
            foreach (SheetView sheetView in fsReporting.Sheets)
            {
                sheetView.Protect = protect;
            }
        }

        /// <summary>
        /// 设置打印属性
        /// </summary>
        private void SetPrintProperty()
        {
            fsReporting.ActiveSheet.PrintInfo.ShowPrintDialog = true;
            fsReporting.ActiveSheet.PrintInfo.ShowColumnHeaders = false;
            fsReporting.ActiveSheet.PrintInfo.ShowRowHeaders = false;
            fsReporting.ActiveSheet.PrintInfo.ShowBorder = false;
        }

        /// <summary>
        /// 管理样表
        /// </summary>
        private void ManageSample()
        {
            SheetForm frmSheet = new SheetForm();
            frmSheet.ReportId = ReportId;
            frmSheet.ReportCategory = ReportCategory;
            frmSheet.AddSheet = AddSheet;
            frmSheet.EditSheet = EditSheet;
            frmSheet.DeleteSheet = DeleteSheet;
            frmSheet.MoveSheet = MoveSheet;
            frmSheet.ShowDialog();
        }

        /// <summary>
        /// 设置单元格样式
        /// </summary>
        /// <param name="cellSelectionStyle"></param>
        /// <param name="obj"></param>
        private void SetCellSelectionStyle(CellSelectionStyle cellSelectionStyle, object obj)
        {
            try
            {
                if (!changedStyle)
                {
                    return;
                }
                CellRange[] cellRanges = fsReporting.ActiveSheet.GetSelections();
                if (cellRanges != null)
                {
                    foreach (CellRange cellRange in cellRanges)
                    {
                        int startRowIndex = 0, startColIndex = 0;
                        int rowCount = 0, colCount = 0;
                        if (cellRange != null)
                        {
                            startRowIndex = cellRange.Row;
                            startColIndex = cellRange.Column;
                            rowCount = cellRange.RowCount;
                            colCount = cellRange.ColumnCount;
                        }
                        else
                        {
                            startRowIndex = fsReporting.ActiveSheet.ActiveCell.Row.Index;
                            startColIndex = fsReporting.ActiveSheet.ActiveCell.Column.Index;
                            rowCount = 1;
                            colCount = 1;
                        }
                        if (cellSelectionStyle == CellSelectionStyle.RowHeight)
                        {
                            for (int i = 0; i < rowCount; i++)
                            {
                                fsReporting.ActiveSheet.Rows[startRowIndex + i].Height = (float)obj;
                            }
                        }
                        else if (cellSelectionStyle == CellSelectionStyle.ColumnWidth)
                        {
                            for (int j = 0; j < colCount; j++)
                            {
                                fsReporting.ActiveSheet.Columns[startColIndex + j].Width = (float)obj;
                            }
                        }
                        else
                        {
                            for (int i = 0; i < rowCount; i++)
                            {
                                for (int j = 0; j < colCount; j++)
                                {
                                    Cell cell = fsReporting.ActiveSheet.Cells[startRowIndex + i, startColIndex + j];
                                    Font font = cell.Font;
                                    float fontSize = 12;
                                    string fontName = "宋体";
                                    FontStyle fontStyle = FontStyle.Regular;
                                    switch (cellSelectionStyle)
                                    {
                                        case CellSelectionStyle.Adjustment:
                                            float sizerow = cell.Row.GetPreferredHeight();
                                            float sizecol = cell.Column.GetPreferredWidth();
                                            cell.Row.Height = sizerow;
                                            cell.Column.Width = sizecol;
                                            break;

                                        case CellSelectionStyle.ResetCellText: /* 清除文本内容*/
                                            cell.ResetText();
                                            cell.ResetBackColor();
                                            cell.ResetCellType();
                                            cell.ResetFormula();
                                            cell.ResetFont();
                                            cell.ResetForeColor();
                                            break;

                                        case CellSelectionStyle.CellColor: /* 设置单元格颜色 */
                                            cell.BackColor = (Color)obj;
                                            break;

                                        case CellSelectionStyle.Font:
                                            cell.Font = (Font)obj;
                                            break;

                                        case CellSelectionStyle.FontName:
                                            if (font != null)
                                            {
                                                fontSize = font.Size;
                                            }
                                            cell.Font = new Font((string)obj, fontSize);
                                            break;

                                        case CellSelectionStyle.FontSize:
                                            if (font != null)
                                            {
                                                fontName = font.Name;
                                            }
                                            fontSize = (float)DataConvertionHelper.GetConvertedInt(obj);
                                            if (fontSize < 1)
                                            {
                                                fontSize = 12;
                                            }
                                            cell.Font = new Font(fontName, fontSize);
                                            break;

                                        case CellSelectionStyle.FontBlod:
                                            if (font != null)
                                            {
                                                fontName = font.Name;
                                                fontSize = font.Size;
                                                fontStyle = font.Style;
                                            }
                                            if ((Convert.ToByte(fontStyle) & Convert.ToByte(FontStyle.Bold)) == Convert.ToByte(FontStyle.Bold))
                                            {
                                                fontStyle = (FontStyle)(Convert.ToByte(fontStyle) & ~Convert.ToByte(FontStyle.Bold));
                                            }
                                            else
                                            {
                                                fontStyle = (FontStyle)(Convert.ToByte(fontStyle) | Convert.ToByte(FontStyle.Bold));
                                            }
                                            cell.Font = new Font(fontName, fontSize, fontStyle);
                                            break;

                                        case CellSelectionStyle.FontItalic:
                                            if (font != null)
                                            {
                                                fontName = font.Name;
                                                fontSize = font.Size;
                                                fontStyle = font.Style;
                                            }
                                            if ((Convert.ToByte(fontStyle) & Convert.ToByte(FontStyle.Italic)) == Convert.ToByte(FontStyle.Italic))
                                            {
                                                fontStyle = (FontStyle)(Convert.ToByte(fontStyle) & ~Convert.ToByte(FontStyle.Italic));
                                            }
                                            else
                                            {
                                                fontStyle = (FontStyle)(Convert.ToByte(fontStyle) | Convert.ToByte(FontStyle.Italic));
                                            }
                                            cell.Font = new Font(fontName, fontSize, fontStyle);
                                            break;

                                        case CellSelectionStyle.FontUnderline:
                                            if (font != null)
                                            {
                                                fontName = font.Name;
                                                fontSize = font.Size;
                                                fontStyle = font.Style;
                                            }
                                            if ((Convert.ToByte(fontStyle) & Convert.ToByte(FontStyle.Underline)) == Convert.ToByte(FontStyle.Underline))
                                            {
                                                fontStyle = (FontStyle)(Convert.ToByte(fontStyle) & ~Convert.ToByte(FontStyle.Underline));
                                            }
                                            else
                                            {
                                                fontStyle = (FontStyle)(Convert.ToByte(fontStyle) | Convert.ToByte(FontStyle.Underline));
                                            }
                                            cell.Font = new Font(fontName, fontSize, fontStyle);
                                            break;

                                        case CellSelectionStyle.Left:
                                            cell.HorizontalAlignment = CellHorizontalAlignment.Left;
                                            break;

                                        case CellSelectionStyle.Middle:
                                            cell.HorizontalAlignment = CellHorizontalAlignment.Center;
                                            break;

                                        case CellSelectionStyle.Right:
                                            cell.HorizontalAlignment = CellHorizontalAlignment.Right;
                                            break;

                                        case CellSelectionStyle.Top:
                                            cell.VerticalAlignment = CellVerticalAlignment.Top;
                                            break;

                                        case CellSelectionStyle.Center:
                                            cell.VerticalAlignment = CellVerticalAlignment.Center;
                                            break;

                                        case CellSelectionStyle.Bottom:
                                            cell.VerticalAlignment = CellVerticalAlignment.Bottom;
                                            break;

                                        case CellSelectionStyle.MiddleCenter:
                                            cell.HorizontalAlignment = CellHorizontalAlignment.Center;
                                            cell.VerticalAlignment = CellVerticalAlignment.Center;
                                            break;

                                        case CellSelectionStyle.AutoLine:
                                            if (cell.CellType == null)
                                            {
                                                TextCellType inputCellType = new TextCellType();
                                                inputCellType.WordWrap = true;
                                                cell.CellType = inputCellType;
                                            }
                                            else
                                            {
                                                EditBaseCellType inputCellType = cell.CellType as EditBaseCellType;
                                                if (inputCellType != null)
                                                {
                                                    inputCellType.WordWrap = !inputCellType.WordWrap;
                                                }
                                                else
                                                {
                                                    cell.ResetCellType();
                                                    TextCellType generalCellType = new TextCellType();
                                                    generalCellType.WordWrap = true;
                                                    cell.CellType = generalCellType;
                                                }
                                            }
                                            break;

                                        case CellSelectionStyle.Broder:

                                            break;

                                        case CellSelectionStyle.Grid:
                                            if (fsReporting.ActiveSheet.HorizontalGridLine.Type == GridLineType.None)
                                            {
                                                GridLine gridLine = new GridLine(FarPoint.Win.Spread.GridLineType.Flat, Color.LightGray);
                                                fsReporting.ActiveSheet.HorizontalGridLine = gridLine;
                                                fsReporting.ActiveSheet.VerticalGridLine = gridLine;
                                            }
                                            else
                                            {
                                                GridLine gridLine = new GridLine(FarPoint.Win.Spread.GridLineType.None, Color.Wheat);
                                                fsReporting.ActiveSheet.HorizontalGridLine = gridLine;
                                                fsReporting.ActiveSheet.VerticalGridLine = gridLine;
                                            }
                                            break;

                                        case CellSelectionStyle.Format:
                                            ICellType cellFormat = obj as ICellType;
                                            cell.CellType = cellFormat;
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }
            tlItmSave.Enabled = true;
            btnItmSave.Enabled = true;
        }

        /// <summary>
        /// 设置边框
        /// </summary>
        private void SetSheetBroder()
        {
            BorderEditor borderedit = new BorderEditor(fsReporting);
            borderedit.Name = "边框设置";
            borderedit.Text = "边框设置";
            if (fsReporting.ActiveSheet.SelectionCount == 0)
            {
                borderedit.StartColumn = fsReporting.ActiveSheet.ActiveColumnIndex;
                borderedit.ColumnCount = 1;
                borderedit.StartRow = fsReporting.ActiveSheet.ActiveRowIndex;
                borderedit.RowCount = 1;
            }
            else
            {
                CellRange range = fsReporting.ActiveSheet.GetSelection(0);   //选中区域
                borderedit.StartColumn = range.Column;
                borderedit.ColumnCount = range.ColumnCount;
                borderedit.StartRow = range.Row;
                borderedit.RowCount = range.RowCount;
            }
            borderedit.ShowDialog();
            tlItmSave.Enabled = true;
            btnItmSave.Enabled = true;
        }

        /// <summary>
        /// 添加数据单元格
        /// </summary>
        private void AddDataCell()
        {
            if (fsReporting.ActiveSheet.ActiveCell != null)
            {
                switch (ReportCategory)
                {
                    case ReportCategory.Query:
                        QueryReportType queryReportType = (QueryReportType)customReportInfo.ReportType;
                        if (fsReporting.ActiveSheetIndex < customSheetInfos.Count)
                        {
                            Cell cell = null;
                            try
                            {
                                CellRange cr = fsReporting.ActiveSheet.GetSelection(0);
                                if (cr.RowCount <= 0 || cr.RowCount > 20 || cr.ColumnCount < 0 || cr.ColumnCount > 20)
                                {
                                    MessageBox.Show("行或者列的数量不能超过20个，请重新设置！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                if (cr.Row + cr.RowCount > fsReporting.ActiveSheet.RowCount || cr.Column + cr.ColumnCount > fsReporting.ActiveSheet.ColumnCount)
                                {
                                    MessageBox.Show("行或者列的索引超过了最大范围，请重新设置！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                Cursor = Cursors.WaitCursor;
                                for (int row = cr.Row; row < (cr.Row + cr.RowCount); row++)
                                {
                                    for (int column = cr.Column; column < (cr.Column + cr.ColumnCount); column++)
                                    {
                                        CustomCellInfo customCellInfo = customCellContract.GetModelInfo(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column);
                                        if (customCellInfo != null)
                                        {
                                            continue;
                                        }

                                        switch (queryReportType)
                                        {
                                            case QueryReportType.Basic:
                                                customCellInfo = new CustomCellInfo(0, decimal.MinValue, customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column,
                                                    (byte)BasicCellType.OnlyData, 0, string.Empty, string.Empty, 0, 0);
                                                break;

                                            case QueryReportType.Statistics:
                                                customCellInfo = new CustomCellInfo(0, decimal.MinValue, customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column,
                                                    (byte)StatisticCellType.OnlyData, 0, string.Empty, string.Empty, 0, 0);
                                                break;
                                        }
                                        customCellContract.Insert(customCellInfo);
                                    }
                                }
                                fsReporting.ActiveSheet.Cells[cr.Row, cr.Column, cr.Row + cr.RowCount - 1, cr.Column + cr.ColumnCount - 1].BackColor = Color.LightSkyBlue;
                                fsReporting.ActiveSheet.Cells[cr.Row, cr.Column, cr.Row + cr.RowCount - 1, cr.Column + cr.ColumnCount - 1].CellType = new TextCellType();
                                SaveReport();
                                Cursor = Cursors.Default;
                            }
                            catch (Exception exception)
                            {
                                if (cell != null)
                                {
                                    fsReporting.ActiveSheet.ActiveCell.BackColor = Color.White;
                                    SaveReport();
                                }
                                Cursor = Cursors.Default;
                                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                            }
                        }
                        break;

                    case ReportCategory.Input:
                        try
                        {
                            Cursor = Cursors.WaitCursor;
                            int row = fsReporting.ActiveSheet.ActiveCell.Row.Index;
                            int column = fsReporting.ActiveSheet.ActiveCell.Column.Index;
                            fsReporting.ActiveSheet.ActiveCell.BackColor = Color.LightSkyBlue;
                            fsReporting.ActiveSheet.ActiveCell.CellType = new TextCellType();
                            SaveReport();
                            CustomCellInfo customCellInfo = customCellContract.GetModelInfo(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column);
                            if (customCellInfo == null)
                            {
                                customCellInfo = new CustomCellInfo(0, decimal.MinValue, customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column,
                                            (byte)InputCellType.Single, 0, string.Empty, string.Empty, 0, 0);
                                customCellContract.Insert(customCellInfo);
                                Cursor = Cursors.Default;
                            }
                            else
                            {
                                Cursor = Cursors.Default;
                                MessageBox.Show("该单元格已是数据单元格!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        catch (Exception exception)
                        {
                            fsReporting.ActiveSheet.ActiveCell.BackColor = Color.White;
                            SaveReport();
                            Cursor = Cursors.Default;
                            WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// 设置单元格范围的颜色，包括合并单元格
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="rowCount"></param>
        /// <param name="colCount"></param>
        /// <param name="color"></param>
        /// <param name="include">是否包含第一个单元格</param>
        private void SetColorInCellRange(int row, int col, int rowCount, int colCount, Color color, bool include)
        {
            int currentRowIndex = row;
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
            {
                int currentColIndex = col;
                int maxRowCount = 1;
                for (int colIndex = 0; colIndex < colCount; colIndex++)
                {
                    if (rowIndex == 0 && colIndex == 0)
                    {
                        if (include)
                        {
                            fsReporting.ActiveSheet.Cells[currentRowIndex, currentColIndex].BackColor = color;
                        }
                    }
                    else
                    {
                        fsReporting.ActiveSheet.Cells[currentRowIndex, currentColIndex].BackColor = color;
                    }
                    CellRange cr = fsReporting.ActiveSheet.GetSpanCell(currentRowIndex, currentColIndex);
                    if (cr != null)
                    {
                        currentColIndex += cr.ColumnCount;
                        if (cr.RowCount > maxRowCount)
                        {
                            maxRowCount = cr.RowCount;
                        }
                    }
                    else
                    {
                        currentColIndex++;
                    }
                }
                currentRowIndex += maxRowCount;
            }
        }

        /// <summary>
        /// 删除数据单元格
        /// </summary>
        private void DeleteDataCell()
        {

            if (fsReporting.ActiveSheet.ActiveCell != null)
            {
                int row = fsReporting.ActiveSheet.ActiveCell.Row.Index;
                int column = fsReporting.ActiveSheet.ActiveCell.Column.Index;
                CustomCellInfo customCellInfo = customCellContract.GetModelInfo(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column);
                if (customCellInfo != null)
                {
                    if (ReportCategory == ReportCategory.Input)
                    {
                        IList<CommonNode> nodes = customCellContract.GetCommonNodesByCellId(customCellInfo.CellId, CellCondition.Show);
                        InputCellType inputCellType = (InputCellType)customCellInfo.CellType;
                        if (inputCellType == InputCellType.Photo)
                        {
                            MessageBox.Show("该单元格是照片单元格，请使用删除照片单元格功能。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (MessageBox.Show("确认要删除数据单元格?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                        {
                            return;
                        }
                        switch (inputCellType)
                        {
                            case InputCellType.Single:
                                fsReporting.ActiveSheet.ActiveCell.BackColor = Color.White;
                                break;

                            case InputCellType.ExtendRow:
                                SetColorInCellRange(row, column, customCellInfo.ExtendRows, nodes.Count, Color.White, true);
                                break;

                            case InputCellType.ExtendCol:
                                SetColorInCellRange(row, column, nodes.Count, customCellInfo.ExtendRows, Color.White, true);
                                break;
                        }
                    }
                    else
                    {                        
                        BasicCellType cellType = (BasicCellType)customCellInfo.CellType;
                        if (cellType == BasicCellType.Photo)
                        {
                            MessageBox.Show("该单元格是照片单元格，请使用删除照片单元格功能。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (MessageBox.Show("确认要删除数据单元格?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                        {
                            return;
                        }
                        fsReporting.ActiveSheet.ActiveCell.BackColor = Color.White;
                    }
                    SaveReport();
                    if (fsReporting.ActiveSheetIndex < customSheetInfos.Count)
                    {
                        if (fsReporting.ActiveSheet.ActiveCell.RowSpan > 1 || fsReporting.ActiveSheet.ActiveCell.ColumnSpan > 1)
                        {
                            for (int i = 0; i < fsReporting.ActiveSheet.ActiveCell.RowSpan; i++)
                            {
                                for (int j = 0; j < fsReporting.ActiveSheet.ActiveCell.ColumnSpan; j++)
                                {
                                    customCellContract.Delete(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, i + row, j + column);
                                }
                            }
                        }
                        else
                        {
                            int count = customCellContract.Delete(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column);
                            if (count == 0)
                            {
                                MessageBox.Show("该单元格并不是数据单元格!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 设置数据来源
        /// </summary>
        private void ShowCellDataSource()
        {
            CellRange cr = fsReporting.ActiveSheet.GetSelection(0);
            if (cr.RowCount <= 0 || cr.RowCount > 20 || cr.ColumnCount < 0 || cr.ColumnCount > 20)
            {
                MessageBox.Show("行或者列的数量不能超过20个，请重新设置！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cr.Row + cr.RowCount > fsReporting.ActiveSheet.RowCount || cr.Column + cr.ColumnCount > fsReporting.ActiveSheet.ColumnCount)
            {
                MessageBox.Show("行或者列的索引超过了最大范围，请重新设置！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<CustomCellInfo> customCellInfos = new List<CustomCellInfo>();
            Cursor = Cursors.WaitCursor;
            for (int row = cr.Row; row < (cr.Row + cr.RowCount); row++)
            {
                for (int column = cr.Column; column < (cr.Column + cr.ColumnCount); column++)
                {
                    CustomCellInfo customCellInfo = customCellContract.GetModelInfo(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column);
                    if (customCellInfo == null)
                    {
                        continue;
                    }
                    customCellInfos.Add(customCellInfo);
                }
            }

            if (customCellInfos.Count == 0)
            {
                MessageBox.Show("所选择的单元格中并包含数据单元格，请先将它添加为数据单元格!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (ReportCategory != ReportCategory.Query && customCellInfos.Count > 1)
                {
                    MessageBox.Show("目前仅支持查询类型报表批量设置单元数据格来源！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                ShowCellDataSource(customCellInfos);
            }
        }

        /// <summary>
        /// 设置数据来源
        /// </summary>
        /// <param name="customCellInfos"></param>
        private void ShowCellDataSource(IList<CustomCellInfo> customCellInfos)
        {
            switch (ReportCategory)
            {
                case ReportCategory.Query:
                    CellConditionSettingForm frmCellConditionSetting = new CellConditionSettingForm();
                    if (!string.IsNullOrWhiteSpace(fsReporting.ActiveSheet.ActiveCell.Text))
                    {
                        frmCellConditionSetting.Text = string.Format("{0}:[{1}]", frmCellConditionSetting.Text, fsReporting.ActiveSheet.ActiveCell.Text);
                    }
                    frmCellConditionSetting.ActiveSheetView = fsReporting.ActiveSheet;
                    frmCellConditionSetting.ReportType = (QueryReportType)customReportInfo.ReportType;
                    frmCellConditionSetting.SetBackColor = SetBackColor;
                    frmCellConditionSetting.CustomCellInfos = customCellInfos;
                    frmCellConditionSetting.ShowDialog();
                    break;

                case ReportCategory.Input:
                    CellConditionForm frmCellCondition = new CellConditionForm();
                    if (!string.IsNullOrWhiteSpace(fsReporting.ActiveSheet.ActiveCell.Text))
                    {
                        frmCellCondition.Text = string.Format("{0}:[{1}]", frmCellCondition.Text, fsReporting.ActiveSheet.ActiveCell.Text);
                    }
                    frmCellCondition.ActiveSheetView = fsReporting.ActiveSheet;
                    frmCellCondition.SetBackColor = SetBackColor;
                    frmCellCondition.ReportType = (InputReportType)customReportInfo.ReportType;
                    frmCellCondition.CustomCellInfo = customCellInfos[0];
                    frmCellCondition.ShowDialog();
                    break;
            }
        }

        /// <summary>
        /// 设置背景颜色
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="color"></param>
        /// <param name="color"></param>
        private void SetBackColor(int row, int col, Color color, bool save)
        {
            SetColorInCellRange(fsReporting.ActiveSheet.ActiveCell.Row.Index, fsReporting.ActiveSheet.ActiveCell.Column.Index, row, col, color, false);
            if (save)
            {
                SaveReport();
            }
        }

        /// <summary>
        /// 获得数据单元格对象
        /// </summary>
        /// <returns></returns>
        private CustomCellInfo GetCustomCellInfo()
        {
            if (fsReporting.ActiveSheet.ActiveCell == null || fsReporting.ActiveSheetIndex >= customSheetInfos.Count)
            {
                return null;
            }
            int row = fsReporting.ActiveSheet.ActiveCell.Row.Index;
            int column = fsReporting.ActiveSheet.ActiveCell.Column.Index;
            CustomCellInfo customCellInfo = customCellContract.GetModelInfo(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId,
                row, column);

            return customCellInfo;
        }

        /// <summary>
        /// 当行列变化时，更新单元格所在的行列
        /// </summary>
        /// <param name="addOrDelete"></param>
        /// <param name="cellRowAndCol"></param>
        private void UpdateDataCellRowAndCol(bool addOrDelete, CellRowAndCol cellRowAndCol)
        {
            CellRange cellRange = fsReporting.ActiveSheet.GetSelection(0);
            if (cellRange == null && fsReporting.ActiveSheet.ActiveCell == null)
            {
                return;
            }
            switch (cellRowAndCol)
            {
                case CellRowAndCol.Row:
                    int startRowIndex = 0, rowCount = 0;
                    if (cellRange != null)
                    {
                        startRowIndex = cellRange.Row;
                        rowCount = cellRange.RowCount;
                    }
                    else
                    {
                        startRowIndex = fsReporting.ActiveSheet.ActiveCell.Row.Index;
                        rowCount = 1;
                    }
                    if (addOrDelete)
                    {
                        fsReporting.ActiveSheet.AddRows(fsReporting.ActiveSheet.ActiveRowIndex, rowCount);
                    }
                    else
                    {
                        fsReporting.ActiveSheet.RemoveRows(fsReporting.ActiveSheet.ActiveRowIndex, rowCount);
                        rowCount *= -1;
                    }
                    customCellContract.UpdateDataCellRowAndCol(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, startRowIndex, rowCount, cellRowAndCol);
                    break;

                case CellRowAndCol.Col:
                    int startColIndex = 0, colCount = 0;
                    if (cellRange != null)
                    {
                        startColIndex = cellRange.Column;
                        colCount = cellRange.ColumnCount;
                    }
                    else
                    {
                        startColIndex = fsReporting.ActiveSheet.ActiveCell.Column.Index;
                        colCount = 1;
                    }
                    if (addOrDelete)
                    {
                        fsReporting.ActiveSheet.AddColumns(fsReporting.ActiveSheet.ActiveColumnIndex, colCount);
                    }
                    else
                    {
                        fsReporting.ActiveSheet.RemoveColumns(fsReporting.ActiveSheet.ActiveColumnIndex, colCount);
                        colCount *= -1;
                    }
                    customCellContract.UpdateDataCellRowAndCol(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, startColIndex, colCount, cellRowAndCol);
                    break;
            }
            SaveReport();
        }

        /// <summary>
        /// 选择的单元格是否包含数据单元格
        /// </summary>
        /// <param name="cellRowAndCol"></param>
        /// <param name="insertOrDelete"></param>
        /// <returns></returns>
        private bool IsIncludeExtendDataCellInRowAndColRange(CellRowAndCol cellRowAndCol, bool insertOrDelete)
        {
            bool include = false;

            if (ReportCategory != ReportCategory.Input)
            {
                return false;
            }
            CellRange cellRange = fsReporting.ActiveSheet.GetSelection(0);
            if (cellRange == null && fsReporting.ActiveSheet.ActiveCell == null)
            {
                return include;
            }
            switch (cellRowAndCol)
            {
                case CellRowAndCol.Row:
                    int startRowIndex = 0, rowCount = 0;
                    if (cellRange != null)
                    {
                        startRowIndex = cellRange.Row;
                        rowCount = cellRange.RowCount;
                    }
                    else
                    {
                        startRowIndex = fsReporting.ActiveSheet.ActiveCell.Row.Index;
                        rowCount = 1;
                    }
                    include = customCellContract.IncludeExtendDataCell(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId,
                        startRowIndex, rowCount, cellRowAndCol, insertOrDelete);
                    break;

                case CellRowAndCol.Col:
                    int startColIndex = 0, colCount = 0;
                    if (cellRange != null)
                    {
                        startColIndex = cellRange.Column;
                        colCount = cellRange.ColumnCount;
                    }
                    else
                    {
                        startColIndex = fsReporting.ActiveSheet.ActiveCell.Column.Index;
                        colCount = 1;
                    }
                    include = customCellContract.IncludeExtendDataCell(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId,
                        startColIndex, colCount, cellRowAndCol, insertOrDelete);
                    break;
            }

            return include;
        }

        /// <summary>
        /// 选择的单元格是否包含数据单元格
        /// </summary>
        /// <returns></returns>
        private bool IsIncludeDataCellInRowAndColRange(CellRowAndCol cellRowAndCol)
        {
            bool include = false;

            CellRange cellRange = fsReporting.ActiveSheet.GetSelection(0);
            if (cellRange == null && fsReporting.ActiveSheet.ActiveCell == null)
            {
                return include;
            }
            switch (cellRowAndCol)
            {
                case CellRowAndCol.Row:
                    int startRowIndex = 0, rowCount = 0;
                    if (cellRange != null)
                    {
                        startRowIndex = cellRange.Row;
                        rowCount = cellRange.RowCount;
                    }
                    else
                    {
                        startRowIndex = fsReporting.ActiveSheet.ActiveCell.Row.Index;
                        rowCount = 1;
                    }
                    include = customCellContract.IncludeDataCell(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId,
                        startRowIndex, rowCount, cellRowAndCol);
                    break;

                case CellRowAndCol.Col:
                    int startColIndex = 0, colCount = 0;
                    if (cellRange != null)
                    {
                        startColIndex = cellRange.Column;
                        colCount = cellRange.ColumnCount;
                    }
                    else
                    {
                        startColIndex = fsReporting.ActiveSheet.ActiveCell.Column.Index;
                        colCount = 1;
                    }
                    include = customCellContract.IncludeDataCell(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId,
                        startColIndex, colCount, cellRowAndCol);
                    break;
            }

            return include;
        }

        /// <summary>
        /// 选择的单元格是否包含数据单元格
        /// </summary>
        /// <returns></returns>
        private bool IsIncludeDataCellInCellRange()
        {
            bool include = false;
            CellRange cellRange = fsReporting.ActiveSheet.GetSelection(0);
            if (cellRange == null && fsReporting.ActiveSheet.ActiveCell == null)
            {
                return include;
            }
            int startRowIndex = 0, startColIndex = 0;
            int rowCount = 0, colCount = 0;
            if (cellRange != null)
            {
                startRowIndex = cellRange.Row;
                startColIndex = cellRange.Column;
                rowCount = cellRange.RowCount;
                colCount = cellRange.ColumnCount;
            }
            else
            {
                startRowIndex = fsReporting.ActiveSheet.ActiveCell.Row.Index;
                startColIndex = fsReporting.ActiveSheet.ActiveCell.Column.Index;
                rowCount = 1;
                colCount = 1;
            }
            include = customCellContract.IncludeDataCell(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId,
                startRowIndex, startColIndex, rowCount, colCount);

            return include;
        }

        /// <summary>
        /// 设置保护属性
        /// </summary>
        /// <param name="index"></param>
        /// <param name="protect"></param>
        private void SetReportingProtectedProperty(int index, bool protect)
        {
            if ((index >= 0) && (index < fsReporting.Sheets.Count))
            {
                SheetView sheetView = fsReporting.Sheets[index];
                if (protect)
                {
                    sheetView.OperationMode = OperationMode.ReadOnly;
                }
                else
                {
                    sheetView.OperationMode = OperationMode.Normal;
                }
                sheetView.Protect = true;
            }
        }

        /// <summary>
        /// 设置保护属性
        /// </summary>
        /// <param name="protect"></param>
        private void SetReportingProtectedProperty(bool protect)
        {
            foreach (SheetView sheetView in fsReporting.Sheets)
            {
                if (protect)
                {
                    sheetView.OperationMode = OperationMode.ReadOnly;
                }
                else
                {
                    sheetView.OperationMode = OperationMode.Normal;
                }
                sheetView.Protect = true;
            }
        }

        /// <summary>
        /// 打印 PDF 文档
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPrintPdf_ItemClick(object sender, ItemClickEventArgs e)
        {
            CommonNode commonNode = fsReporting.ActiveSheet.Tag as CommonNode;
            if (commonNode != null)
            {
                Cursor = Cursors.WaitCursor;
                CustomMargin customMargin = customSheetContract.GetMargin(commonNode.NodeId);
                ReportHelper.PrintPdf(fsReporting, customMargin);
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置菜单项的状态
        /// </summary>
        /// <param name="enable"></param>
        private void SetMenuItemState(bool enable)
        {
            foreach (LinkPersistInfo linkPersistInfo in popupMenu.LinksPersistInfo)
            {
                linkPersistInfo.Item.Enabled = enable;
            }
            barLinkContainerItemEdit.Enabled = enable;
            barLinkContainerItemFormat.Enabled = enable;
            foreach (LinkPersistInfo linkPersistInfo in barTool.LinksPersistInfo)
            {
                linkPersistInfo.Item.Enabled = enable;
            }
            foreach (LinkPersistInfo linkPersistInfo in barTool.LinksPersistInfo)
            {
                linkPersistInfo.Item.Enabled = enable;
            }
            foreach (LinkPersistInfo linkPersistInfo in barEditTool.LinksPersistInfo)
            {
                linkPersistInfo.Item.Enabled = enable;
            }
        }

        #endregion

        private void fsReporting_RowHeightChanged(object sender, RowHeightChangedEventArgs e)
        {
            tlItmSave.Enabled = true;
        }

        private void fsReporting_ColumnWidthChanged(object sender, FarPoint.Win.Spread.ColumnWidthChangedEventArgs e)
        {
            tlItmSave.Enabled = true;
        }

        /// <summary>
        /// 刷新单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmRefresh_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定刷新当前表格中的数据单元格吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    if (fsReporting.ActiveSheetIndex >= 0 && fsReporting.ActiveSheetIndex < customSheetInfos.Count)
                    {
                        Cursor = Cursors.WaitCursor;
                        IList<CustomCellInfo> customCellInfos = customCellContract.GetModelInfos(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId);
                        foreach (CustomCellInfo customCellInfo in customCellInfos)
                        {
                            if (customCellInfo.RowIndex <= fsReporting.ActiveSheet.RowCount && customCellInfo.ColIndex <= fsReporting.ActiveSheet.ColumnCount)
                            {
                                fsReporting.ActiveSheet.Cells[customCellInfo.RowIndex, customCellInfo.ColIndex].BackColor = Color.LightSkyBlue;
                                fsReporting.ActiveSheet.Cells[customCellInfo.RowIndex, customCellInfo.ColIndex].CellType = new TextCellType();
                            }
                            else
                            {
                                customCellContract.Delete(customCellInfo.CellId);
                            }
                        }
                        SaveReport();
                        Cursor = Cursors.Default;
                        MessageBox.Show("当前表格中的数据单元格刷新成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 表套另存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ReportCategory != ReportCategory.Query)
            {
                MessageBox.Show("目前仅支持查询类型报表另存！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                TreeSelectedItemsForm frmTreeSelectedItems = new TreeSelectedItemsForm()
                {
                    Text = "查询报表分类",
                    CommonNodeContract = customGroupContract, 
                    NodeType = (byte)GroupType.QueryReport,                   
                    OnlySelectedLeaf = true
                };
                frmTreeSelectedItems.NodeSelected = (node) =>
                {
                    if (MessageBox.Show("确定另存当前表套吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        Cursor = Cursors.WaitCursor;
                        customReportContract.SaveAs(ReportId, node.NodeId);
                        Cursor = Cursors.Default;
                        MessageBox.Show("当前表套另保成功。如果需要编辑另存的表套，请打开！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                };
                frmTreeSelectedItems.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        //private void btnItmAltEnter_ItemClick(object sender, ItemClickEventArgs e)
        //{
        //    if (fsReporting.ActiveSheet.ActiveCell != null)
        //    {
        //        FarPoint.Win.Spread.CellType.TextCellType t = new FarPoint.Win.Spread.CellType.TextCellType();
        //        t.Multiline = true;
        //        fsReporting.ActiveSheet.ActiveCell.CellType = t;
        //        FarPoint.Win.Spread.CellType.GeneralEditor editor = (FarPoint.Win.Spread.CellType.GeneralEditor)fsReporting.ActiveSheet.GetEditor(fsReporting.ActiveSheet.ActiveCell.Row.Index, fsReporting.ActiveSheet.ActiveCell.Column.Index);
        //        string text = string.Format("{0}{1}{2}", editor.Text.Substring(0, editor.SelectionStart),
        //        System.Environment.NewLine, editor.Text.Substring(editor.SelectionStart)); 
        //    }
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fsReporting_EditModeOn(object sender, EventArgs e)
        {
            GeneralEditor editor = (GeneralEditor)fsReporting.EditingControl;
            editor.AcceptsReturn = false;
        }

        /// <summary>
        /// 样表另存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmSheetSaveAs_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (ReportCategory != ReportCategory.Query)
            {
                MessageBox.Show("目前仅支持查询类型报表另存！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                if (MessageBox.Show("确定在本表套中复制当前样表吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    Cursor = Cursors.WaitCursor;
                    string sheetName = string.Format("Sheet{0}", fsReporting.Sheets.Count + 1);
                    SheetView sheetView = fsReporting.ActiveSheet.Clone();
                    sheetView.SheetName = sheetName;
                    fsReporting.Sheets.Add(sheetView);
                    byte[] data = ReportHelper.GetFpSpreadData(fsReporting);
                    customReportContract.SheetSaveAs(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, ReportId, data);
                    customSheetInfos = customSheetContract.GetModelInfos(ReportId);
                    Cursor = Cursors.Default;
                    MessageBox.Show("复制当前样表成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 刷新样表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmRefreshSheet_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IList<string> sheetNames = new List<string>(fsReporting.Sheets.Count);
                foreach (SheetView sheet in fsReporting.Sheets)
                {
                    sheetNames.Add(sheet.SheetName);
                }
                customSheetContract.UpdateSheetSorting(ReportId, sheetNames);
                Cursor = Cursors.Default;
                MessageBox.Show("样表排序更新成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 添加照片单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPhotoCell_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddPhotoCell();
        }

        /// <summary>
        /// 删除照片单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmDeletedPhotoCell_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeletedPhotoCell();
        }

        /// <summary>
        /// 添加照片单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbtnItmPhotoCell_ItemClick(object sender, ItemClickEventArgs e)
        {
            AddPhotoCell();
        }

        /// <summary>
        /// 删除照片单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbtnItmDeletedPhotoCell_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeletedPhotoCell();
        }

        /// <summary>
        /// 添加照片单元格
        /// </summary>
        private void AddPhotoCell()
        {
            if (fsReporting.ActiveSheet.ActiveCell != null)
            {
                try
                {
                    Cursor = Cursors.WaitCursor;
                    int row = fsReporting.ActiveSheet.ActiveCell.Row.Index;
                    int column = fsReporting.ActiveSheet.ActiveCell.Column.Index;
                    fsReporting.ActiveSheet.ActiveCell.BackColor = Color.LightGray;
                    fsReporting.ActiveSheet.ActiveCell.CellType = new TextCellType();
                    SaveReport();
                    CustomCellInfo customCellInfo = customCellContract.GetModelInfo(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column);
                    if (customCellInfo == null)
                    {
                        byte cellType = 0;
                        switch (ReportCategory)
                        {
                            case ReportCategory.Query:
                                cellType = (byte)BasicCellType.Photo;
                                break;

                            case ReportCategory.Input:
                                cellType = (byte)InputCellType.Photo;
                                break;
                        }
                        customCellInfo = new CustomCellInfo(0, decimal.MinValue, customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column,
                                    cellType, 0, string.Empty, string.Empty, 0, 0);
                        customCellContract.Insert(customCellInfo);
                        Cursor = Cursors.Default;
                    }
                    else
                    {
                        switch (ReportCategory)
                        {
                            case ReportCategory.Query:
                                QueryReportType queryReportType = (QueryReportType)customReportInfo.ReportType;
                                switch (queryReportType)
                                {
                                    case QueryReportType.Basic:
                                        BasicCellType basicCellType = (BasicCellType)customCellInfo.CellType;
                                        Cursor = Cursors.Default;
                                        if (basicCellType == BasicCellType.Photo)
                                        {
                                            MessageBox.Show("该单元格已是照片单元格!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                        else
                                        {
                                            MessageBox.Show("该单元格已是数据单元格!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }                                        
                                        break;
                                }
                                break;

                            case ReportCategory.Input:
                                InputCellType inputCellType = (InputCellType)customCellInfo.CellType;
                                Cursor = Cursors.Default;
                                if (inputCellType == InputCellType.Photo)
                                {
                                    MessageBox.Show("该单元格已是照片单元格!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                else
                                {
                                    MessageBox.Show("该单元格已是数据单元格!", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                                break;
                        }

                        
                    }
                }
                catch (Exception exception)
                {
                    fsReporting.ActiveSheet.ActiveCell.BackColor = Color.White;
                    SaveReport();
                    Cursor = Cursors.Default;
                    WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                }
            }
        }

        /// <summary>
        /// 删除照片单元格
        /// </summary>
        private void DeletedPhotoCell()
        {
            if (MessageBox.Show("确认要删除照片单元格?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (fsReporting.ActiveSheet.ActiveCell != null)
                {
                    int row = fsReporting.ActiveSheet.ActiveCell.Row.Index;
                    int column = fsReporting.ActiveSheet.ActiveCell.Column.Index;
                    if (ReportCategory == ReportCategory.Input)
                    {
                        CustomCellInfo customCellInfo = customCellContract.GetModelInfo(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column);
                        if (customCellInfo != null)
                        {
                            InputCellType inputCellType = (InputCellType)customCellInfo.CellType;
                            if(inputCellType == InputCellType.Photo)
                            {
                                fsReporting.ActiveSheet.ActiveCell.BackColor = Color.White;
                            }
                        }
                    }
                    else
                    {
                        fsReporting.ActiveSheet.ActiveCell.BackColor = Color.White;
                    }
                    SaveReport();
                    if (fsReporting.ActiveSheetIndex < customSheetInfos.Count)
                    {
                        customCellContract.Delete(customSheetInfos[fsReporting.ActiveSheetIndex].SheetId, row, column);                        
                    }
                }
            }
        }
        
    }


    /// <summary>
    /// 回车换行
    /// </summary>
    public class AltEnterAction : FarPoint.Win.Spread.Action  
    {    
        public override void PerformAction(object sender)    
        {     
            SpreadView ss = (SpreadView)sender;    
            GeneralEditor editor = (GeneralEditor)ss.EditingControl;      
            string text = string.Format("{0}{1}{2}", editor.Text.Substring(0, editor.SelectionStart),
                System.Environment.NewLine, editor.Text.Substring(editor.SelectionStart));            
            ss.Sheets[0].SetValue(ss.Sheets[0].ActiveRowIndex, ss.Sheets[0].ActiveColumnIndex, text);
            ss.EditMode = true;
            editor = (GeneralEditor)ss.EditingControl;
            editor.SelectionStart = editor.Text.Length;
            editor.SelectionLength = 0; 
        } 
    }
}
