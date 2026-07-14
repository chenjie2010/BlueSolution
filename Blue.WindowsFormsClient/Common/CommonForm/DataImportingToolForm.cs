using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Excel;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.Model.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class DataImportingToolForm : Form
    {
        #region  私有静态只读变量

        /// <summary>
        /// 加锁
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 错误格式的锁
        /// </summary>
        private static readonly object errorFormatlocker = new object();

        private static ManualResetEvent manualResetEvent = new ManualResetEvent(false);

        #endregion

        #region  私有常量      

        /* 线程组个数 */
        private const int MAX_THREAD_COUNT = 1;

        /* 列宽 */
        private const int COLUMN_WIDTH_IN_SHEET_VIEW = 180;

        /* 标题行高 */
        private const int ROW_HEIGHT_IN_SHEET_VIEW = 30;

        #endregion

        #region  私有变量        

        /// <summary>
        /// Excel 单元格内容变化是否生效
        /// </summary>
        private bool isCellChangedValild = false;

        /// <summary>
        /// 数据校验时，错误的数据单元格数组： 数组索引为 Excel 表格索引；每个元素：行列索引，错误的原因
        /// </summary>
        private Dictionary<int, DataImportedError>[] errorCellsInSheets = null;

        /// <summary>
        /// 数据导入时，主表导入失败的行索引
        /// </summary>
        private IList<int> dataRowsFailed = null;

        /// <summary>
        /// 数据导入时，子表导入失败的表格索引与表格名称
        /// </summary>
        private Dictionary<int, string> dataRowsFailedInSheets = null; 
        
        /// <summary>
        /// 校验是否完成
        /// </summary>
        private bool validationCompleted = false;
        
        /// <summary>
        /// 线程组
        /// </summary>
        private BackgroundWorker[] workers = new BackgroundWorker[MAX_THREAD_COUNT];

        /// <summary>
        /// 信号量数组
        /// </summary>
        private AutoResetEvent[] autoResetEvents = new AutoResetEvent[MAX_THREAD_COUNT];

        /// <summary>
        /// 进度条
        /// </summary>
        private ProgressForm frmCommonProgress = null;

        #endregion

        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly IUserTypeContract userTypeContract;

        #endregion

        #region 属性

        /// <summary>
        /// 数据导入导出接口
        /// </summary>
        public IDataExportedInterface DataExportedInterface
        {
            get;
            set;
        }

        /// <summary>
        /// 任务完成后刷新窗体
        /// </summary>
        public RefreshFormDelegate RefreshForm
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataImportingToolForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();                       
            InitErrorDataTable();
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 数据导入窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataImportingToolForm_Load(object sender, EventArgs e)
        {
            mtxtTip.EditValue = DataExportedInterface.HelpContent;
        }

        /// <summary>
        /// 加载 Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            openFileDialog.InitialDirectory = WinPlatformHelper.GetFileFloder();
            openFileDialog.Filter = AppSettingHelper.DefaultExcelFormat;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                MessageBox.Show("没有选择Excel文件。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!File.Exists(openFileDialog.FileName))
            {
                MessageBox.Show(string.Format("{0}文件不存在，请检查。", Path.GetFileName(openFileDialog.FileName)),
                    "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                isCellChangedValild = false;
                WinPlatformHelper.LastestFilePath = Path.GetDirectoryName(openFileDialog.FileName);
                fpImportedData.OpenExcel(openFileDialog.FileName, ExcelOpenFlags.TruncateEmptyRowsAndColumns);
                fpImportedData.TabStripInsertTab = false;
                fpImportedData.TabStripPolicy = TabStripPolicy.AsNeeded;
                fpImportedData.ActiveSheetIndex = 0;
                SpreadToolHelper.RemoveEmptyRows(fpImportedData);
                foreach (SheetView sheetView in fpImportedData.Sheets)
                {                    
                    sheetView.CellChanged += new SheetViewEventHandler(sheetView_CellChanged);                    
                }
                InitCacheResource(fpImportedData.Sheets.Count);
                lblStatus.Text = "等待数据校验。";
                isCellChangedValild = true;
                validationCompleted = false;
                Cursor = Cursors.Default;                
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageBox.Show("出错可能原因：（1）该Excel文件已打开(请先关闭后再加载)（2）该文件并不存在（3）该文件内容与格式错误", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void biiCellFormat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CellFormatForm frmCellFormat = new CellFormatForm();
            frmCellFormat.SetCellType = (cellType)
                =>
            {
                SpreadToolHelper.SetCellTypeOnCells(fpImportedData, cellType);
            };
            frmCellFormat.ShowDialog();
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiVerify_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (fpImportedData.Sheets[0].ColumnCount != DataExportedInterface.ColumnCountFixed)
                {
                    MessageBox.Show(string.Format("{0}导入时，Excel文件包含{1}列。", DataExportedInterface.DataExportedName, DataExportedInterface.ColumnCountFixed),
                        "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }                
                if (fpImportedData.Sheets[0].RowCount <= 1)
                {
                    MessageBox.Show("Excel文件必须包含导入的数据行(第一行默认为标题行)。", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (fpImportedData.Sheets[0].RowCount <= 1 || fpImportedData.Sheets[0].RowCount > 20000)
                {
                    MessageBox.Show("一次导入Excel文件中主表的数据记录总数超过范围限制：1条~20,000条。", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (DataExportedInterface.HasSheets && fpImportedData.Sheets.Count > 1)
                {
                    List<string> dataSheetNames = new List<string>(fpImportedData.Sheets.Count - 1);
                    for (int idx = 1; idx < fpImportedData.Sheets.Count; idx++)
                    {
                        if (fpImportedData.Sheets[idx].ColumnCount < DataExportedInterface.MinColumnCount
                            || fpImportedData.Sheets[idx].ColumnCount > DataExportedInterface.MaxColumnCount)
                        {
                            MessageBox.Show(string.Format("Excel文件表格{0}的列数超过范围限制：{1}列~{2}列。", fpImportedData.Sheets[idx],
                                DataExportedInterface.MinColumnCount, DataExportedInterface.MaxColumnCount), "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (fpImportedData.Sheets[idx].RowCount < 1 || fpImportedData.Sheets[idx].RowCount > 60000)
                        {
                            MessageBox.Show("Excel文件中子表的数据记录总数超过范围限制：0条~60000条。", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        dataSheetNames.Add(fpImportedData.Sheets[idx].SheetName);
                    }
                    var items = dataSheetNames.GroupBy(t => t).Where(t => t.Count() > 1);
                    if (items.Count() > 1)
                    {
                        MessageBox.Show("Excel文件表格的表名有重复。", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                if (MessageBox.Show("确认进行数据校验？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
                Cursor = Cursors.WaitCursor;
                validationCompleted = false;
                int rowCount = GetToalRowCount();
                InitCommonProgress("正在校验中，请稍后...", 0, rowCount + 1);  /* 加1的目的用于处理编码缓存 */
                lblStatus.Text = "正在进行数据校验，开始处理编码缓存...。"; 
                /* 清除缓存与错误文本 */
                ClearCahcheResources();
                ClearErrorText();
                int threadCount = 1;
                if (DataExportedInterface.HasSheets)
                {
                    threadCount = MAX_THREAD_COUNT;
                }
                else
                {
                    threadCount = fpImportedData.Sheets[0].RowCount < MAX_THREAD_COUNT ? 1 : MAX_THREAD_COUNT;
                }
                InitThreadResrouces(threadCount);
                int threadCountExisted = 0;
                /* 预读取编码和子表的结构并加入缓存 */
                BackgroundWorker backgroundWorker = new BackgroundWorker()
                {
                    WorkerSupportsCancellation = true,
                    WorkerReportsProgress = true
                };
                manualResetEvent.Reset();
                backgroundWorker.DoWork += (workSender, ea) =>
                {
                    var worker = workSender as BackgroundWorker;                    
                    /* 预读取编码加入缓存 */
                    Dictionary<int, string> codeCache = new Dictionary<int, string>(fpImportedData.Sheets[0].RowCount);
                    for (int rowIndex = 1; rowIndex < fpImportedData.Sheets[0].RowCount; rowIndex++)
                    {
                        if (worker.CancellationPending)
                        {
                            ea.Cancel = true;
                            break;
                        }
                        codeCache.Add(rowIndex, fpImportedData.Sheets[0].Cells[rowIndex, DataExportedInterface.TreeCodeColumnIndex].Text.Trim());
                    }
                    DataExportedInterface.InitDataResource(codeCache);
                    /* 预读取子表的结构缓存 */
                    if (DataExportedInterface.HasSheets && fpImportedData.Sheets.Count > 1)
                    {
                        DataExportedInterface.InitDataTableStruct(fpImportedData);
                    }
                    worker.ReportProgress(0);                    
                };
                backgroundWorker.ProgressChanged += (workSender, ea) =>
                {
                    frmCommonProgress.IncreaseStep();
                };
                backgroundWorker.RunWorkerCompleted += (workSender, ea) =>
                {
                    if (!ea.Cancelled)
                    {
                        frmCommonProgress.Tip = "正在校验中：编码缓存处理完成。";
                        lblStatus.Text = "正在校验中：编码缓存处理完成。";
                    }
                    manualResetEvent.Set();
                };
                backgroundWorker.RunWorkerAsync();
                /* 开始数据校验 */
                for (int idx = 0; idx < threadCount; idx++)
                {
                    workers[idx].DoWork += (workSender, ea) =>
                    {
                        manualResetEvent.WaitOne();
                        var worker = workSender as BackgroundWorker;
                        int threadIndex = Convert.ToInt32(ea.Argument);
                        IList<string> cellTexts = new List<string>();
                        int sheetIndex = 0;                  
                        foreach (SheetView sheet in fpImportedData.Sheets)
                        {
                            if (sheetIndex > 0 && !DataExportedInterface.HasSheets)
                            {
                                break;
                            }
                            /* 主表（Excel 文件第一个表）包含标题行，第一行自动省略；其他表首行根据业务实际需求 */
                            int start = threadIndex + 1;
                            if (sheetIndex > 0 && !DataExportedInterface.FirstRowSkipped)
                            {
                                start = threadIndex;
                            }
                            for (int rowIndex = start; rowIndex < sheet.RowCount; rowIndex += threadCount)
                            {
                                if (worker.CancellationPending)
                                {
                                    ea.Cancel = true;
                                    break;
                                }
                                cellTexts.Clear();
                                for (int colIndex = 0; colIndex < sheet.ColumnCount; colIndex++)
                                {
                                    cellTexts.Add(sheet.Cells[rowIndex, colIndex].Text.Trim());
                                }
                                IList<DataValidationResult> dataValidationResults = DataExportedInterface.ValidateCellData(sheetIndex, rowIndex, cellTexts);                            
                                DataValidationList<int> dataValidationList = new DataValidationList<int>(threadIndex, dataValidationResults);
                                worker.ReportProgress(rowIndex, dataValidationList);
                                autoResetEvents[threadIndex].WaitOne();
                            }
                            sheetIndex++;
                        }
                    };
                    workers[idx].ProgressChanged += (workSender, ea) =>
                    {
                        try
                        {
                            var worker = workSender as BackgroundWorker;
                            int count = GetCellErrorCount();
                            if (count > 0)
                            {
                                frmCommonProgress.Tip = string.Format("正在校验中：已发现错误单元格数目为{0}。", count);
                            }
                            else
                            {
                                frmCommonProgress.Tip = "正在校验中：未发现发现错误单元格。";
                            }
                            DataValidationList<int> dataValidationList = (DataValidationList<int>)ea.UserState;
                            isCellChangedValild = false;
                            foreach (DataValidationResult dataValidationResult in dataValidationList.DataValidationResults)
                            {
                                if (worker.CancellationPending)
                                {
                                    break;
                                }
                                if (dataValidationResult.DataImportedError != DataImportedError.None)
                                {
                                    lock (errorFormatlocker)
                                    {
                                        SetErrorInfo(dataValidationResult);
                                    }
                                }
                            }
                            frmCommonProgress.IncreaseStep();
                            isCellChangedValild = true;
                            autoResetEvents[dataValidationList.Key].Set();
                        }
                        catch (ExcelException exception)
                        {
                                //记录日志, 抛出异常, 不包装异常
                                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                        }
                    };
                    workers[idx].RunWorkerCompleted += (workSender, ea) =>
                    {
                        if (!ea.Cancelled)
                        {
                            lock (locker)
                            {
                                Interlocked.Increment(ref threadCountExisted);
                                if (threadCountExisted == threadCount)
                                {
                                    frmCommonProgress.CloseFrom();
                                    SetErrorDescription();
                                    int count = GetCellErrorCount();
                                    if (count == 0)
                                    {
                                        lblStatus.Text = "数据校验已完成，校验正确。";
                                    }
                                    else
                                    {
                                        lblStatus.Text = string.Format("数据校验已完成，错误单元格数共有：{0}个。", count);
                                    }
                                    validationCompleted = true;
                                }
                            }
                        }
                    };
                    workers[idx].RunWorkerAsync(idx);
                }
                Cursor = Cursors.Default;
                frmCommonProgress.TaskCancelled = delegate ()
                {
                    backgroundWorker.CancelAsync();
                    for (int idx = 0; idx < threadCount; idx++)
                    {
                        if (!workers[idx].CancellationPending)
                        {
                            workers[idx].CancelAsync();
                        }
                    }
                    lblStatus.Text = "数据校验已取消。";
                };
                frmCommonProgress.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 显示错误提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiShowResult_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (validationCompleted)
            {
                int count = GetCellErrorCount();
                if (count == 0)
                {
                    lblErrorTip.Text = "校验正确。";
                }
                else
                {
                    lblErrorTip.Text = string.Format("错误单元格数共有：{0}个。", count);
                }
                
                if (fpError.FlyoutPanelState.IsActive)
                {
                    return;
                }
                fpError.ShowBeakForm();
            }
            else
            {
                MessageBox.Show("请先进行数据校验。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 隐藏错误提示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpError_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            fpError.HideBeakForm();
        }

        /// <summary>
        /// 导入 Excel 文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!validationCompleted)
            {
                MessageBox.Show("请完成数据校验后再导入数据。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int count = GetCellErrorCount();
            if (count > 0)
            {
                if (DataExportedInterface.ErrorsSkipped)
                {
                    if (MessageBox.Show(string.Format("至少有{0}个单元格数据不符合规范，是否继续导入数据？", count), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                    {
                        return;
                    }
                }
                else
                {
                    MessageBox.Show(string.Format("至少有{0}个单元格数据不符合规范，请修改正确后再导入数据。", count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            ImportedMode importedMode = ImportedMode.Append;
            ImportedModeForm frmDataImported = new ImportedModeForm();
            frmDataImported.ImportedModeValue = (1L << (byte)ImportedMode.NotUpdateAndInsert) | (1L << (byte)ImportedMode.UpdateAndInsert);
            frmDataImported.DataImported = (mode) =>
            {
                importedMode = mode;
            };
            frmDataImported.ShowDialog();
            if ((count == 0) && MessageBox.Show(string.Format("确认导入数据({0})？", DataExportedInterface.DataExportedName), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                int rowCount = fpImportedData.Sheets[0].RowCount - 1;
                InitCommonProgress(string.Format("正在批量导入{0}，请稍后...", DataExportedInterface.DataExportedName), 0, rowCount);
                lblStatus.Text = string.Format("正在批量导入{0}...。", DataExportedInterface.DataExportedName);
                /* 清除缓存 */
                ClearCahcheResources();
                IList<int>[] errorDataRows = GetErrorDataRows();                
                int threadCount = 1;
                InitThreadResrouces(threadCount);
                int recordCountImported = 0; /* 主表成功导入记录总数 */
                int sheetCountImported = 0; /* 子表成功导入总数 */
                workers[0].DoWork += (workSender, ea) =>
                {
                    var worker = workSender as BackgroundWorker;
                    int threadIndex = Convert.ToInt32(ea.Argument);
                    IList<string> cellTexts = new List<string>();                   
                    for (int levelIndex = 0; levelIndex <= DataExportedInterface.TreeCodeMaxLevel; levelIndex++)
                    {
                        foreach (KeyValuePair<int, string> keyValue in DataExportedInterface.TreeCodeRelations[levelIndex])
                        {
                            if (worker.CancellationPending)
                            {
                                ea.Cancel = true;
                                break;
                            }
                            if (errorDataRows[0].Contains(keyValue.Key))
                            {
                                continue;
                            }
                            cellTexts.Clear();
                            for (int colIndex = 0; colIndex < DataExportedInterface.ColumnCountFixed; colIndex++)
                            {
                                cellTexts.Add(fpImportedData.Sheets[0].Cells[keyValue.Key, colIndex].Text.Trim());
                            }
                            try
                            {
                                /* 1. 导入 Excel 文件中第一个表：主表 */
                                decimal dataId = DataExportedInterface.ImportData(cellTexts, importedMode);
                                if (dataId > 0)
                                {
                                    recordCountImported++;
                                    string treeCode = cellTexts[DataExportedInterface.TreeCodeColumnIndex];
                                    /* 2. 导入 Excel 文件中其他表：子表 */
                                    if (DataExportedInterface.HasSheets && DataExportedInterface.AllowDataTableImported(treeCode, importedMode))
                                    {
                                        /* 索引表示子表所在的位置 */
                                        int sheetIndex = DataExportedInterface[treeCode] + 1;
                                        try
                                        {                                                                                      
                                            if (sheetIndex > 0)
                                            {                                                
                                                DataExportedInterface.ImportDataTable(sheetIndex, dataId, treeCode, fpImportedData.Sheets[sheetIndex], errorDataRows[sheetIndex]);                                                
                                                sheetCountImported++;
                                            }
                                            else
                                            {
                                                throw new ArgumentException("表格名称索引异常。");
                                            }
                                        }
                                        catch
                                        {
                                            dataRowsFailedInSheets.Add(sheetIndex, cellTexts[0]);
                                        }
                                    }
                                }
                                else
                                {
                                    if (!dataRowsFailed.Contains(keyValue.Key))
                                    {
                                        dataRowsFailed.Add(keyValue.Key);
                                    }
                                }
                            }
                            catch
                            {
                                if (!dataRowsFailed.Contains(keyValue.Key))
                                {
                                    dataRowsFailed.Add(keyValue.Key);
                                }
                            }
                            worker.ReportProgress(keyValue.Key);
                            autoResetEvents[0].WaitOne();
                        }
                    }
                };
                workers[0].ProgressChanged += (workSender, ea) =>
                {
                    try
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("批量导入{0}已完成，主表总记录数{1}个：已导入{2}条（含忽略），失败{3}条", DataExportedInterface.DataExportedName,
                                    fpImportedData.Sheets[0].RowCount - 1, recordCountImported, dataRowsFailed.Count);
                        if (DataExportedInterface.ErrorsSkipped)
                        {
                            sb.AppendFormat("，跳过错误{0}条", errorDataRows[0].Count);
                        }
                        if (DataExportedInterface.HasSheets)
                        {
                            sb.AppendFormat("，子表导已入{0}个，失败{1}个。", sheetCountImported, dataRowsFailedInSheets.Count);
                        }
                        else
                        {
                            sb.Append("。");
                        }
                        frmCommonProgress.Tip = sb.ToString();
                        frmCommonProgress.IncreaseStep();
                        autoResetEvents[0].Set();
                    }
                    catch (ExcelException exception)
                    {
                            //记录日志, 抛出异常, 不包装异常
                            ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                    }
                };
                workers[0].RunWorkerCompleted += (workSender, ea) =>
                {
                    if (!ea.Cancelled)
                    {
                        StringBuilder sb = new StringBuilder();
                        sb.AppendFormat("批量导入{0}已完成，主表总记录数{1}个：已导入{2}条（含忽略），失败{3}条", DataExportedInterface.DataExportedName,
                                    recordCountImported + dataRowsFailed.Count + errorDataRows[0].Count, recordCountImported, dataRowsFailed.Count);
                        if (DataExportedInterface.ErrorsSkipped)
                        {
                            sb.AppendFormat("，跳过错误{0}条", errorDataRows[0].Count);
                        }
                        if (DataExportedInterface.HasSheets)
                        {
                            sb.AppendFormat("，子表导已入{0}个", sheetCountImported);
                            if (dataRowsFailedInSheets.Count > 0)
                            {
                                sb.AppendFormat("，失败{0}个：", dataRowsFailedInSheets.Count);
                                foreach (var keyValue in dataRowsFailedInSheets)
                                {
                                    sb.AppendFormat("[{0}]", keyValue.Value);
                                }
                            }
                            else
                            {
                                sb.Append("，失败0个");
                            }
                        }
                        sb.Append("。");
                        lblStatus.Text = sb.ToString();
                        frmCommonProgress.CloseFrom();
                        RefreshForm?.Invoke();
                    }
                };
                workers[0].RunWorkerAsync(0);
                Cursor = Cursors.Default;
                frmCommonProgress.TaskCancelled = delegate ()
                {
                    for (int idx = 0; idx < MAX_THREAD_COUNT; idx++)
                    {
                        workers[idx].CancelAsync();
                    }
                    lblStatus.Text = string.Format("批量导入{0}已取消。", DataExportedInterface.DataExportedName);
                };
                frmCommonProgress.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 校验错误数据另存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiErrorData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!validationCompleted)
            {
                MessageBox.Show("请完成数据校验或者数据导入后再另存校验错误数据。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            int count = GetCellErrorCount();
            if (count > 0)
            {
                saveFileDialog.FileName = string.Format("{0}_校验错误数据_{1:yyyyMMddHHmmss}.xlsx", DataExportedInterface.DataExportedName, DateTime.Now);
                saveFileDialog.Filter = AppSettingHelper.DefaultExcelFormat;
                saveFileDialog.InitialDirectory = WinPlatformHelper.GetFileFloder();
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    WinPlatformHelper.LastestFilePath = Path.GetDirectoryName(saveFileDialog.FileName);
                    using (FpSpread fpSpread = new FpSpread())
                    {
                        Cursor = Cursors.WaitCursor;
                        int index = 0;
                        IList<int>[] errorDataRows = GetErrorDataRows();
                        foreach (var errorDataRow in errorDataRows)
                        {
                            if (errorDataRow.Count == 0)
                            {
                                index++;
                                continue;
                            }
                            int columnCountFixed = index > 0 ? DataExportedInterface.MaxColumnCount : DataExportedInterface.ColumnCountFixed;
                            SheetView sheetView = new SheetView(string.Format("{0}_校验错误数据", fpImportedData.Sheets[index].SheetName));
                            fpSpread.Sheets.Add(sheetView);
                            sheetView.RowCount = errorDataRow.Count;
                            sheetView.ColumnCount = fpImportedData.Sheets[index].ColumnCount;
                            sheetView.Cells[0, 0, errorDataRow.Count - 1, sheetView.ColumnCount - 1].CellType = new TextCellType();
                            sheetView.ColumnHeader.Rows[0].Height = ROW_HEIGHT_IN_SHEET_VIEW;                            
                            for (int idx = 0; idx < errorDataRow.Count; idx++)
                            {                                                             
                                for (int col = 0; col < sheetView.ColumnCount; col++)
                                {
                                    if (idx == 0)
                                    {
                                        /* 标题行 */
                                        sheetView.ColumnHeader.Cells[0, col].Text = fpImportedData.Sheets[index].Cells[0, col].Text;
                                        sheetView.Columns[col].Width = COLUMN_WIDTH_IN_SHEET_VIEW;
                                    }
                                    /* 内容 */
                                    int key = errorDataRow[idx] * columnCountFixed + col;
                                    if (errorCellsInSheets[index].ContainsKey(key))
                                    {
                                        sheetView.Cells[idx, col].BackColor = Color.LightSteelBlue;
                                    }
                                    sheetView.Cells[idx, col].Text = fpImportedData.Sheets[index].Cells[errorDataRow[idx], col].Text;
                                }
                            }
                            /* 标题行颜色 */
                            if (errorDataRow.Count > 0)
                            {
                                sheetView.ColumnHeader.Cells[0, 0, 0, sheetView.ColumnCount - 1].BackColor = Color.LightGray;
                            }
                            index++;
                        }
                        if (saveFileDialog.FileName.EndsWith("xlsx"))
                        {
                            fpSpread.SaveExcel(saveFileDialog.FileName, ExcelSaveFlags.SaveCustomColumnHeaders | ExcelSaveFlags.UseOOXMLFormat);
                        }
                        else
                        {
                            fpSpread.SaveExcel(saveFileDialog.FileName, ExcelSaveFlags.DataOnly);
                        }
                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("Excel文件({0})导出成功。", saveFileDialog.FileName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception exception)
                {
                    Cursor = Cursors.Default;
                    //记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
            else
            {
                MessageBox.Show("没有校验错误数据。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 导入错误数据另存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiErrorDataImported_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!validationCompleted)
            {
                MessageBox.Show("请完成数据校验或者数据导入后再另存导入错误数据。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (dataRowsFailed.Count > 0 || dataRowsFailedInSheets.Count > 0)
            {
                saveFileDialog.FileName = string.Format("{0}_导入错误数据_{1:yyyyMMddHHmmss}.xlsx", DataExportedInterface.DataExportedName, DateTime.Now);
                saveFileDialog.Filter = AppSettingHelper.DefaultExcelFormat;
                saveFileDialog.InitialDirectory = WinPlatformHelper.GetFileFloder();
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                try
                {
                    WinPlatformHelper.LastestFilePath = Path.GetDirectoryName(saveFileDialog.FileName);
                    using (FpSpread fpSpread = new FpSpread())
                    {
                        Cursor = Cursors.WaitCursor;

                        /* 主表错误数据 */
                        if (dataRowsFailed.Count > 0)
                        {
                            SheetView sheetView = new SheetView(string.Format("{0}_导入错误数据", fpImportedData.Sheets[0].SheetName));
                            fpSpread.Sheets.Add(sheetView);
                            sheetView.RowCount = dataRowsFailed.Count;
                            sheetView.ColumnCount = fpImportedData.Sheets[0].ColumnCount;
                            sheetView.Cells[0, 0, dataRowsFailed.Count - 1, sheetView.ColumnCount - 1].CellType = new TextCellType();
                            sheetView.ColumnHeader.Rows[0].Height = ROW_HEIGHT_IN_SHEET_VIEW;
                            for (int idx = 0; idx < dataRowsFailed.Count; idx++)
                            {
                                for (int col = 0; col < sheetView.ColumnCount; col++)
                                {
                                    if (idx == 0)
                                    {
                                        /* 标题行 */
                                        sheetView.ColumnHeader.Cells[0, col].Text = fpImportedData.Sheets[0].Cells[0, col].Text;
                                        sheetView.Columns[col].Width = COLUMN_WIDTH_IN_SHEET_VIEW;
                                    }
                                    /* 内容 */
                                    sheetView.Cells[idx, col].Text = fpImportedData.Sheets[0].Cells[dataRowsFailed[idx], col].Text;
                                }
                            }
                            if (dataRowsFailed.Count > 0)
                            {
                                /* 标题行颜色 */
                                sheetView.ColumnHeader.Cells[0, 0, 0, sheetView.ColumnCount - 1].BackColor = Color.LightGray;
                            }
                        }

                        /* 子表错误数据 */
                        foreach(var dataRowsFailedInSheet in dataRowsFailedInSheets)
                        {
                            SheetView sheet = (SheetView)Serializer.LoadObjectXml(typeof(SheetView), Serializer.GetObjectXml(fpImportedData.Sheets[dataRowsFailedInSheet.Key], "CopySheet"), "CopySheet");
                            sheet.SheetName = string.Format("{0}_导入错误数据", fpImportedData.Sheets[dataRowsFailedInSheet.Key].SheetName);
                            sheet.ColumnHeader.Rows[0].Height = ROW_HEIGHT_IN_SHEET_VIEW;
                            for (int col = 0; col < sheet.ColumnCount; col++)
                            {
                                sheet.Columns[col].Width = COLUMN_WIDTH_IN_SHEET_VIEW;
                            }
                            fpSpread.Sheets.Add(sheet);
                        }
                        if (saveFileDialog.FileName.EndsWith("xlsx"))
                        {
                            fpSpread.SaveExcel(saveFileDialog.FileName, ExcelSaveFlags.SaveCustomColumnHeaders | ExcelSaveFlags.UseOOXMLFormat);
                        }
                        else
                        {
                            fpSpread.SaveExcel(saveFileDialog.FileName, ExcelSaveFlags.DataOnly);
                        }

                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("Excel文件({0})导出成功。", saveFileDialog.FileName), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception exception)
                {
                    Cursor = Cursors.Default;
                    //记录日志, 抛出异常, 不包装异常
                    ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                }
            }
            else
            {
                MessageBox.Show("没有导入错误数据。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 修改数据单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sheetView_CellChanged(object sender, SheetViewEventArgs e)
        {
            /* 三种情况均不校验：未校验、忽略单元格变化、主表（Excel第一个表）第一行是标题行 */
            if (!validationCompleted || !isCellChangedValild || (e.Row == 0 && !DataExportedInterface.HasSheets))
            {
                return;
            }
            SheetView sheetView = (SheetView)sender;
            int sheetIndex = fpImportedData.Sheets.IndexOf(sheetView);
            string cellText = fpImportedData.ActiveSheet.Cells[e.Row, e.Column].Text.Trim();
            if (sheetIndex == 0 && (e.Column == DataExportedInterface.TreeCodeColumnIndex))
            {
                /* 编码列 */
                IList<DataValidationResult> resultsChecked = DataExportedInterface.ValidateCellCodeChanged(e.Row, cellText);
                foreach (DataValidationResult result in resultsChecked)
                {
                    SetErrorInfo(result);
                }
            }
            else
            {
                if (sheetIndex > 0)
                {
                   
                    DataValidationResult result = DataExportedInterface.ValidateCellDataChanged(sheetIndex, e.Row, e.Column, cellText);
                    SetErrorInfo(result);
                    switch (DataExportedInterface.DataTableCheckedMode)
                    {
                        case DataTableCheckedMode.Col:
                            if (e.Row == 0)
                            {
                                /* 重新检验该列 */
                                for (int row = 1; row < sheetView.RowCount; row++)
                                {
                                    DataValidationResult dataValidationResult = DataExportedInterface.ValidateCellDataChanged(sheetIndex, row, e.Column,
                                        sheetView.Cells[row, e.Column].Text);
                                    SetErrorInfo(dataValidationResult);
                                }
                            }
                            break;

                        case DataTableCheckedMode.Row:
                            /* 重新检验该行 */
                            for (int col = 0; col < sheetView.ColumnCount; col++)
                            {
                                if (e.Column != col)
                                {
                                    DataValidationResult dataValidationResult = DataExportedInterface.ValidateCellDataChanged(sheetIndex, e.Row, col,
                                        sheetView.Cells[e.Row, col].Text);
                                    SetErrorInfo(dataValidationResult);
                                }
                            }
                            break;
                    }                    
                }
                else
                {
                    DataValidationResult result = DataExportedInterface.ValidateCellDataChanged(sheetIndex, e.Row, e.Column, cellText);
                    SetErrorInfo(result);
                }
            }
            int count = GetCellErrorCount();
            if (count == 0)
            {
                lblStatus.Text = "数据校验已完成，校验正确。";
            }
            else
            {
                lblStatus.Text = string.Format("数据校验已完成，错误单元格数共有：{0}个。", count);
            }
        }

        /// <summary>
        /// 保存修改后的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SpreadToolHelper.ExprotExcel(fpImportedData, string.Format("{0}_Excel数据另存", DataExportedInterface.DataExportedName));
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 点击行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int sheetIndex = Convert.ToInt32(gridView.GetFocusedDataRow()["SheetIndex"]);
            int row = Convert.ToInt32(gridView.GetFocusedDataRow()["RowIndex"]);
            int col = Convert.ToInt32(gridView.GetFocusedDataRow()["ColumnIndex"]);
            if (row > 0 && col > 0 && row <= fpImportedData.Sheets[sheetIndex].RowCount && col <= fpImportedData.Sheets[sheetIndex].ColumnCount)
            {
                fpImportedData.ActiveSheetIndex = sheetIndex;
                fpImportedData.Sheets[sheetIndex].SetActiveCell(row - 1, col - 1);
            }
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        #endregion

        #region  私有方法

        /// <summary>
        /// 初始化进度条
        /// </summary>
        /// <param name="text"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        private void InitCommonProgress(string text, int minValue, int maxValue)
        {
            frmCommonProgress = new ProgressForm();
            frmCommonProgress.Text = text;
            frmCommonProgress.MinValue = minValue;
            frmCommonProgress.MaxValue = maxValue;
        }

        /// <summary>
        /// 初始化缓存
        /// </summary>
        /// <param name="length"></param>
        private void InitCacheResource(int length)
        {
            errorCellsInSheets = new Dictionary<int, DataImportedError>[length];
            dataRowsFailed = new List<int>();
            dataRowsFailedInSheets = new Dictionary<int, string>();
            for (int i = 0; i < length; i++)
            {
                errorCellsInSheets[i] = new Dictionary<int, DataImportedError>();
            }
        }

        /// <summary>
        /// 清空缓存资源
        /// </summary>
        private void ClearCahcheResources()
        {
            dataRowsFailedInSheets.Clear();
            dataRowsFailed.Clear();    
        }

        /// <summary>
        /// 清除错误文本显示
        /// </summary>
        private void ClearErrorText()
        {
            for (int i = 0; i < errorCellsInSheets.Length; i++)
            {
                if (errorCellsInSheets[i].Count > 0 && fpImportedData.Sheets[i].RowCount > 1)
                {
                    if (i > 0)
                    {
                        if (!DataExportedInterface.FirstRowSkipped)
                        {
                            fpImportedData.Sheets[i].Cells[0, 0, 0, fpImportedData.Sheets[i].ColumnCount - 1].BackColor = Color.LightGray;
                        }
                        fpImportedData.Sheets[i].Cells[1, 0, fpImportedData.Sheets[i].RowCount - 1, fpImportedData.Sheets[i].ColumnCount - 1].ResetBackColor();
                        fpImportedData.Sheets[i].Cells[0, 0, fpImportedData.Sheets[i].RowCount - 1, fpImportedData.Sheets[i].ColumnCount - 1].ErrorText = string.Empty;
                    }
                    else
                    {
                        fpImportedData.Sheets[i].Cells[1, 0, fpImportedData.Sheets[i].RowCount - 1, fpImportedData.Sheets[i].ColumnCount - 1].ResetBackColor();
                        fpImportedData.Sheets[i].Cells[1, 0, fpImportedData.Sheets[i].RowCount - 1, fpImportedData.Sheets[i].ColumnCount - 1].ErrorText = string.Empty;
                    }
                }
                errorCellsInSheets[i].Clear();
            }
            DataTable dataTable = (DataTable)gridControl.DataSource;
            dataTable.Clear();
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// 初始化线程资源
        /// </summary>
        /// <param name="threadCount"></param>
        private void InitThreadResrouces(int threadCount)
        {
            for (int idx = 0; idx < threadCount; idx++)
            {
                workers[idx] = new BackgroundWorker()
                {
                    WorkerSupportsCancellation = true,
                    WorkerReportsProgress = true
                };
                autoResetEvents[idx] = new AutoResetEvent(false);
            }
        }

        /// <summary>
        /// 设置或是清除错误信息
        /// </summary>
        /// <param name="dataValidationResult"></param>
        private void SetErrorInfo(DataValidationResult dataValidationResult)
        {
            isCellChangedValild = false;
            FarPoint.Win.Spread.Cell cell = fpImportedData.Sheets[dataValidationResult.SheetIndex].Cells[dataValidationResult.Row, dataValidationResult.Col];

            int columnCountFixed = dataValidationResult.SheetIndex > 0 ? DataExportedInterface.MaxColumnCount : DataExportedInterface.ColumnCountFixed;
            int index = dataValidationResult.Row * columnCountFixed + dataValidationResult.Col;
            if (dataValidationResult.DataImportedError == DataImportedError.None)
            {               
                if (errorCellsInSheets[dataValidationResult.SheetIndex].ContainsKey(index))
                {
                    if (dataValidationResult.Row == 0)
                    {
                        cell.BackColor = Color.LightGray;
                    }
                    else
                    {
                        cell.BackColor = Color.White;
                    }
                    cell.ErrorText = string.Empty;
                    errorCellsInSheets[dataValidationResult.SheetIndex].Remove(index);
                }
            }
            else
            {                
                if (errorCellsInSheets[dataValidationResult.SheetIndex].ContainsKey(index))
                {
                    if (errorCellsInSheets[dataValidationResult.SheetIndex][index] != dataValidationResult.DataImportedError)
                    {
                        errorCellsInSheets[dataValidationResult.SheetIndex][index] = dataValidationResult.DataImportedError;
                        cell.ErrorText = UserEnumHelper.GetEnumText(dataValidationResult.DataImportedError);
                    }
                }
                else
                {
                    cell.BackColor = Color.LightSteelBlue;
                    cell.ErrorText = UserEnumHelper.GetEnumText(dataValidationResult.DataImportedError);
                    errorCellsInSheets[dataValidationResult.SheetIndex].Add(index, dataValidationResult.DataImportedError);
                }
            }
            Application.DoEvents();
            isCellChangedValild = true;
        }
                
        /// <summary>
        /// 设置错误描述
        /// </summary>
        /// <returns></returns>
        private void SetErrorDescription()
        {
            DataTable dataTable = (DataTable)gridControl.DataSource;
            IList <EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(DataImportedError));
            int index = 0;
            foreach (var errorCellsInSheet in errorCellsInSheets)
            {
                int columnCountFixed = index > 0 ? DataExportedInterface.MaxColumnCount : DataExportedInterface.ColumnCountFixed;
                foreach (var errorCell in errorCellsInSheet)
                {
                    int row = errorCell.Key / columnCountFixed;
                    int col = errorCell.Key % columnCountFixed;
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = fpImportedData.Sheets[index].SheetName;
                    dataRow[1] = index;
                    dataRow[2] = row + 1;
                    dataRow[3] = col + 1;
                    dataRow[4] = UserEnumHelper.GetEnumText((DataImportedError)errorCell.Value);
                    dataTable.Rows.Add(dataRow);
                }
                index++;
            }
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// 初始化错误结果表
        /// </summary>
        private void InitErrorDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("SheetName", Type.GetType("System.String"));
            dataTable.Columns.Add("SheetIndex", Type.GetType("System.Int32"));
            dataTable.Columns.Add("RowIndex", Type.GetType("System.Int32"));
            dataTable.Columns.Add("ColumnIndex", Type.GetType("System.Int32"));
            dataTable.Columns.Add("Description", Type.GetType("System.String"));
            dataTable.DefaultView.Sort = "RowIndex ASC, ColumnIndex ASC";
            gridControl.DataSource = dataTable;
            gridView.Columns[0].Caption = "表格名称";
            gridView.Columns[1].Caption = "表格索引";
            gridView.Columns[2].Caption = "行索引";
            gridView.Columns[3].Caption = "列索引";
            gridView.Columns[4].Caption = "错误描述";
            gridView.Columns[0].Width = 180;
            gridView.Columns[2].Width = 50;
            gridView.Columns[3].Width = 50;
            gridView.Columns[4].Width = 160;
            gridView.Columns[1].Visible = false;
        }

        /// <summary>
        /// 更新错误结果表
        /// </summary>
        /// <param name="sheetName"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="dataImportedError"></param>
        private void UpdateErrorDataTable(string sheetName, int sheetIndex, int row, int col, DataImportedError dataImportedError)
        {
            DataTable dataTable = (DataTable)gridControl.DataSource;
            DataRow[] dataRows = dataTable.Select(string.Format("SheetIndex = {0} AND RowIndex = {1} AND ColumnIndex = {2}", sheetIndex, row + 1, col + 1));
            if (dataImportedError == DataImportedError.None)
            {                
                for (int idx = 0; idx < dataRows.Length; idx++)
                {
                    dataTable.Rows.Remove(dataRows[idx]);
                }
            }
            else
            {
                if (dataRows.Length == 0)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataRow[0] = sheetName;
                    dataRow[1] = sheetIndex;
                    dataRow[2] = row + 1;
                    dataRow[3] = col + 1;
                    dataRow[4] = UserEnumHelper.GetEnumText(dataImportedError);
                    dataTable.Rows.Add(dataRow);
                }
            }
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// 获得错误的行数
        /// </summary>
        /// <returns></returns>
        private IList<int>[] GetErrorDataRows()
        {
            IList<int>[] errorDataRows = new List<int>[errorCellsInSheets.Length];

            int index = 0;
            foreach (var errorCellsInSheet in errorCellsInSheets)
            {
                if (index > 0 && !DataExportedInterface.HasSheets)
                {
                    break;
                }
                IList<int> errors = new List<int>();
                errorDataRows[index] = errors;
                int columnCountFixed = DataExportedInterface.ColumnCountFixed;
                if (index > 0)
                {
                    columnCountFixed = DataExportedInterface.MaxColumnCount;
                }
                foreach (KeyValuePair<int, DataImportedError> keyValue in errorCellsInSheet)
                {
                    int row = keyValue.Key / columnCountFixed;
                    if (!errors.Contains(row))
                    {
                        errors.Add(row);
                    }
                }
                index++;
            }

            return errorDataRows;
        }

        /// <summary>
        /// 获得单元格校验错误总数
        /// </summary>
        /// <returns></returns>
        private int GetCellErrorCount()
        {
            int count = 0;

            foreach (var errorCellsInSheet in errorCellsInSheets)
            {
                count += errorCellsInSheet.Count;
            }

            return count;
        }

        /// <summary>
        /// 获得错误总行数
        /// </summary>
        /// <returns></returns>
        private int GetTotalErrorRowCount(IList<int>[] dataRows)
        {
            int count = 0;
            
            foreach (var errorDataRow in dataRows)
            {
                count += errorDataRow.Count;
            }

            return count;
        }        

        /// <summary>
        /// 获得所有表格的行数
        /// </summary>
        /// <returns></returns>
        private int GetToalRowCount()
        {
            int rowCount = 0;
            if (DataExportedInterface.HasSheets)
            {
                /* 所有表格首行为标题列 */
                foreach (SheetView sheet in fpImportedData.Sheets)
                {
                    rowCount += sheet.RowCount - 1;
                }
            }
            else
            {
                rowCount = fpImportedData.Sheets[0].RowCount - 1;
            }

            return rowCount;
        }

        /// <summary>
        /// 查找表格索引(从子表(第二个表格)开始查找)
        /// </summary>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        private int FindIndexInSheets(string sheetName)
        {
            int sheetIndex = 0;

            for (int idx = 1; idx < fpImportedData.Sheets.Count; idx++)
            {
                if (fpImportedData.Sheets[idx].Equals(sheetName))
                {
                    sheetIndex = idx;
                    break;
                }
            }

            return sheetIndex;
        }

        #endregion
        
    }
}
