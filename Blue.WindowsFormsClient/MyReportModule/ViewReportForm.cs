using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Design;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using FarPoint.Excel;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.WinBusinessLogic;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;
using Blue.Model.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WindowsFormsClient.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.MyReportModule
{
    public partial class ViewReportForm : Form
    {
        #region 契约接口

        private readonly ICustomReportContract customReportContract;
        private readonly ICustomSheetContract customSheetContract;
        private readonly ICustomCellContract customCellContract;
        private readonly ICustomCellContract[] customCellContracts;
        private readonly ICustomSnapshotContract customSnapshotContract;
        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;

        #endregion

        #region  私有静态只读变量

        /// <summary>
        /// 加锁
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 加锁
        /// </summary>
        private static readonly object dicLocker = new object();
        
        /// <summary>
        /// 显示刷新加锁
        /// </summary>
        //private static readonly object showedLocker = new object();

        #endregion

        #region  私有常量      

        /* 线程组个数 */
        private const int MAX_THREAD_COUNT = 20;

        /* 文本框最大字符数 */
        private const int MAX_LENGTH_ON_CELL = 8000;

        /* 文本框最小字符数时启用文本类型 */
        private const int MIN_LENGTH_ON_CELL = 128;

        #endregion

        #region 私有变量

        private UserListControl userListControl = null;
        private Dictionary<decimal, byte[]> reportingData = new Dictionary<decimal, byte[]>();
        private IList<RowAndCol> rowAndCols = null;
        private AuthorityCondition authorityCondition = null;

        #endregion

        #region 私有成员变量

        private CustomReportInfo _currentReportInfo;

        #endregion

        #region 属性

        /// <summary>
        /// 报表对象
        /// </summary>
        public CustomReportInfo CurrentReportInfo
        {
            get
            {
                return _currentReportInfo;
            }
            set
            {
                _currentReportInfo = value;
                QueryReportType queryReportType = (QueryReportType)_currentReportInfo.ReportType;
                if (queryReportType == QueryReportType.Basic)
                {
                    btnItmCompute.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                    dkpnlTop.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
                    userListControl = new UserListControl();
                    userListControl.IsShowCheckBox = false;
                    userListControl.Dock = DockStyle.Fill;
                    userListControl.CustomGroupContract = customGroupContract;
                    userListControl.UserAccountContract = userAccountContract;
                    userListControl.UserTypeContract = userTypeContract;
                    userListControl.CustomDepartmentContract = customDepartmentContract;
                    userListControl.OnRowClick += (sdr, arg) =>
                    {
                        if (userListControl.SelectedUserId > 0)
                        {
                            ShowBasicReporting(userListControl.SelectedUserId);
                        }
                    };
                    dkpnlTop.Controls.Add(userListControl);
                    btnItmInsertPhoto.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnItmView.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                else
                {
                    btnItmCompute.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    btnItmView.Visibility = DevExpress.XtraBars.BarItemVisibility.Always;
                    dkpnlTop.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                    btnItmInsertPhoto.Visibility = DevExpress.XtraBars.BarItemVisibility.Never;
                }
                LoadReportTemplate();
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        ///  构造函数
        /// </summary>
        public ViewReportForm()
        {
            InitializeComponent();

            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
            customSheetContract = BusinessDesignerChannelFactory.CreateCustomSheetContract();
            customCellContract = BusinessDesignerChannelFactory.CreateCustomCellContract();
            customCellContracts =  new ICustomCellContract[MAX_THREAD_COUNT];
            for (int idx = 0; idx < MAX_THREAD_COUNT; idx++)
            {
                customCellContracts[idx] = BusinessDesignerChannelFactory.CreateCustomCellContract();
            }
            customSnapshotContract = BusinessDesignerChannelFactory.CreateCustomSnapshotContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            authorityCondition = new AuthorityCondition(customDepartmentContract, userTypeContract);
        }

        #endregion

        #region 窗体及控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViewReportForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            fsReporting.TabStripInsertTab = false;
            fsReporting.TabStripPolicy = TabStripPolicy.Always;
            //SetReportingProtectedProperty(true);
            //SetMenuEanableProperty(false); 
        }

        #endregion

        /// <summary>
        /// 统计报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmCompute_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ShowStatisticReporting();
        }

        /// <summary>
        /// 加载报表模板
        /// </summary>
        private void LoadReportTemplate()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                byte[] data = null;
                if (reportingData.ContainsKey(CurrentReportInfo.ReportId))
                {
                    data = reportingData[CurrentReportInfo.ReportId];
                }
                else
                {
                    data = customSheetContract.DownloadReportFile(CurrentReportInfo.ReportId);
                    reportingData.Add(CurrentReportInfo.ReportId, data);
                }
                rowAndCols = customSheetContract.GetRowAndColCount(CurrentReportInfo.ReportId);
                if (data != null && data.Length > 0)
                {
                    ReportHelper.ShowFpSpread(fsReporting, data, rowAndCols);
                    SetReportingProtectedProperty(true);
                    SetMenuEanableProperty(true);
                }
                else
                {
                    SetMenuEanableProperty(false);
                    fsReporting.Reset();
                    Cursor = Cursors.Default;
                    MessageBox.Show("该查询报表还未设计，请先设计！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                IList<CommonNode> commonNodes = customSheetContract.GetChildNodes(CurrentReportInfo.ReportId);
                int count = (fsReporting.Sheets.Count <= commonNodes.Count) ? fsReporting.Sheets.Count : commonNodes.Count;
                for (int i = 0; i < count; i++)
                {
                    fsReporting.Sheets[i].Tag = commonNodes[i];
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 设置菜单按钮的属性
        /// </summary>
        /// <param name="enable"></param>
        private void SetMenuEanableProperty(bool enable)
        {
            btnItmOpen.Enabled = enable;
            btnItmEdit.Enabled = enable;
            btnItmSave.Enabled = enable;
            btnItmCompute.Enabled = enable;
            btnItmImport.Enabled = enable;
            btnItmInsertPhoto.Enabled = enable;
            btnItmInsertBatchNumber.Enabled = enable;
            btnItmPringSetting.Enabled = enable;
            btnItmPreview.Enabled = enable;
            btnItmPrint.Enabled = enable;
            btnItmPrintPdf.Enabled = enable;
            btnItmView.Enabled = enable;
        }

        /// <summary>
        /// 设置报表可编辑性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SetReportingProtectedProperty(false);
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
            }
        }
                
        /// <summary>
        /// 显示基本报表
        /// </summary>
        /// <param name="userId"></param>
        private void ShowBasicReporting(decimal userId)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IList<CommonNode> commonNodes = customSheetContract.GetChildNodes(CurrentReportInfo.ReportId);
                Dictionary<decimal, CustomDataFieldInfo> customDataFieldInfoDic = new Dictionary<decimal, CustomDataFieldInfo>();
                Dictionary<decimal, string> dicTablePhysicalNames = new Dictionary<decimal, string>();
                int count = (fsReporting.Sheets.Count <= commonNodes.Count) ? fsReporting.Sheets.Count : commonNodes.Count;
                BackgroundWorker[,] workers = new BackgroundWorker[count, MAX_THREAD_COUNT];
                progressPanel.Show();
                int threadCountExisted = 0;
                int totalThreads = 0;
                for (int sheetIndex = 0; sheetIndex < count; sheetIndex++)
                {
                    fsReporting.Sheets[sheetIndex].Tag = commonNodes[sheetIndex];
                    IList<CustomCellInfo> customCellInfos = customCellContract.GetModelInfos(commonNodes[sheetIndex].NodeId);
                    if (customCellInfos.Count == 0) continue;
                    int threadCount = customCellInfos.Count > MAX_THREAD_COUNT ? MAX_THREAD_COUNT : 1;
                    totalThreads += threadCount;
                    for (int threadIndex = 0; threadIndex < threadCount; threadIndex++)
                    {
                        workers[sheetIndex, threadIndex] = new BackgroundWorker();
                        workers[sheetIndex, threadIndex].WorkerReportsProgress = true;
                        workers[sheetIndex, threadIndex].DoWork += (workSender, ea) =>
                        {
                            var worker = workSender as BackgroundWorker;                            
                            RowAndCol rowAndCol = (RowAndCol)ea.Argument;
                            int currentSheetIndex = rowAndCol.Row;
                            int currentThreadIndex = rowAndCol.Col;
                            for (int rowIndex = rowAndCol.Col; rowIndex < customCellInfos.Count; rowIndex += threadCount)
                            {
                                if (worker.CancellationPending)
                                {
                                    ea.Cancel = true;
                                    break;
                                }
                                CustomCellInfo customCellInfo = customCellInfos[rowIndex];
                                BasicCellType basicCellType = (BasicCellType)customCellInfo.CellType;
                                if (customCellInfo.TableId <= 0 && basicCellType != BasicCellType.Photo)
                                {
                                    continue;
                                }                                
                                switch (basicCellType)
                                {
                                    case BasicCellType.Photo:
                                        if (userListControl != null && userListControl.SelectedUserId > 0 && fsReporting.ActiveSheet.ActiveCell != null)
                                        {
                                            if (userListControl.ImageData != null && userListControl.ImageData.Length > 0)
                                            {
                                                ReportHelper.InsertPhoto(fsReporting, fsReporting.ActiveSheet.ActiveCell, userListControl.ImageData);
                                            }
                                            else
                                            {
                                                ThreadCellItem item = new ThreadCellItem(currentThreadIndex, rowAndCol.Row, customCellInfo.RowIndex, customCellInfo.ColIndex, "该用户没有照片");
                                                worker.ReportProgress(rowIndex, item);
                                            }
                                        }
                                        break;

                                    case BasicCellType.OnlyData:
                                        string cellText = customCellContracts[rowAndCol.Col].GetCellText(customCellInfo.CellId, userId, customCellInfo.TableId,
                                            customCellInfo.ConditionText, customCellInfo.TemplateText);                                        
                                        if (!string.IsNullOrWhiteSpace(cellText) && cellText.Length > MIN_LENGTH_ON_CELL)
                                        {
                                            TextCellType textCellType = new TextCellType();
                                            textCellType.WordWrap = true;
                                            textCellType.MaxLength = MAX_LENGTH_ON_CELL;
                                            fsReporting.Sheets[currentSheetIndex].Cells[customCellInfo.RowIndex, customCellInfo.ColIndex].CellType = textCellType;
                                        }
                                        ThreadCellItem cellItem = new ThreadCellItem(currentThreadIndex, rowAndCol.Row, customCellInfo.RowIndex, customCellInfo.ColIndex, cellText);
                                        worker.ReportProgress(rowIndex, cellItem);
                                        break;

                                    case BasicCellType.ExtendRow:
                                    case BasicCellType.ExtendCol:
                                    case BasicCellType.ExtendRowByCondtion:
                                    case BasicCellType.ExtendColByCondtion:
                                        decimal id = userId;
                                        if (basicCellType == BasicCellType.ExtendRowByCondtion || basicCellType == BasicCellType.ExtendColByCondtion)
                                        {
                                            id = 0;
                                        }
                                        DataSet ds = customCellContracts[rowAndCol.Col].GetExtendRowColData(customCellInfo.CellId, id, customCellInfo.TableId, customCellInfo.ConditionText);
                                        if (ds == null)
                                        {
                                            continue;
                                        }
                                        int currentSpanCellCount = 0;
                                        IList<CellStyleInfo> cellStyleInfos = customCellContracts[rowAndCol.Col].GetModelInfosByCellId(customCellInfo.CellId, CellCondition.Show);
                                        foreach (var cellStyleInfo in cellStyleInfos)
                                        {
                                            CustomDataFieldInfo customDataFieldInfo = null;
                                            if (cellStyleInfo.DataFieldId > 0)
                                            {
                                                if (customDataFieldInfoDic.ContainsKey(cellStyleInfo.DataFieldId))
                                                {
                                                    customDataFieldInfo = customDataFieldInfoDic[cellStyleInfo.DataFieldId];
                                                }
                                                else
                                                {
                                                    customDataFieldInfo = customDataFieldContract.GetModelInfo(cellStyleInfo.DataFieldId);
                                                    if (customDataFieldInfo != null)
                                                    {
                                                        lock (dicLocker)
                                                        {
                                                            customDataFieldInfoDic.Add(cellStyleInfo.DataFieldId, customDataFieldInfo);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                string tablePhysicalName = string.Empty;
                                                if (dicTablePhysicalNames.ContainsKey(customCellInfo.TableId))
                                                {
                                                    tablePhysicalName = dicTablePhysicalNames[customCellInfo.TableId];
                                                }
                                                else
                                                {
                                                    tablePhysicalName = customTableContract.GetTablePhysicalName(customCellInfo.TableId);
                                                }
                                                customDataFieldInfo = CommonBussinessHelper.GetSystemDataFieldInfo(customCellInfo.TableId, tablePhysicalName, (SystemDataField)cellStyleInfo.SystemDataFieldId);
                                            }
                                            int row = 0, col = 0, size = 0;
                                            if (basicCellType == BasicCellType.ExtendRow || basicCellType == BasicCellType.ExtendRowByCondtion)
                                            {
                                                size = customCellInfo.ExtendRows;
                                            }
                                            else
                                            {
                                                size = customCellInfo.ExtendCols;
                                            }                                            
                                            switch (basicCellType)
                                            {
                                                case BasicCellType.ExtendRow:
                                                case BasicCellType.ExtendRowByCondtion:
                                                    col = customCellInfo.ColIndex + currentSpanCellCount;
                                                    break;

                                                case BasicCellType.ExtendCol:
                                                case BasicCellType.ExtendColByCondtion:
                                                    row = customCellInfo.RowIndex + currentSpanCellCount;
                                                    break;
                                            }
                                            for (int index = 0; index < size; index++)
                                            {
                                                switch (basicCellType)
                                                {
                                                    case BasicCellType.ExtendRow:
                                                    case BasicCellType.ExtendRowByCondtion:
                                                        row = customCellInfo.RowIndex + index;
                                                        break;

                                                    case BasicCellType.ExtendCol:
                                                    case BasicCellType.ExtendColByCondtion:
                                                        col = customCellInfo.ColIndex + index;
                                                        break;
                                                }
                                                object result = null;
                                                if (index < ds.Tables[0].Rows.Count)
                                                {
                                                    result = ds.Tables[0].Rows[index][customDataFieldInfo.PhysicalName];
                                                }
                                                string text = SetCellProperty(customDataFieldInfo, result);
                                                ThreadCellItem item = new ThreadCellItem(currentThreadIndex, currentSheetIndex, row, col, text);
                                                worker.ReportProgress(rowIndex, item);
                                                if (index == 0)
                                                {
                                                    /* 计算合并的单元格的行列 */
                                                    CellRange cr = fsReporting.Sheets[currentSheetIndex].GetSpanCell(row, col);
                                                    if (cr != null)
                                                    {
                                                        switch (basicCellType)
                                                        {
                                                            case BasicCellType.ExtendRow:
                                                            case BasicCellType.ExtendRowByCondtion:
                                                                currentSpanCellCount += cr.ColumnCount;
                                                                break;

                                                            case BasicCellType.ExtendCol:
                                                            case BasicCellType.ExtendColByCondtion:
                                                                currentSpanCellCount += cr.RowCount;
                                                                break;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        currentSpanCellCount++;
                                                    }
                                                }
                                            }
                                        }
                                        break;
                                }
                            }
                        };
                        workers[sheetIndex, threadIndex].ProgressChanged += (workSender, ea) =>
                        {
                            try
                            {
                                ThreadCellItem cellItem = (ThreadCellItem)ea.UserState;
                                if (cellItem != null)
                                {
                                    fsReporting.Sheets[cellItem.SheetIndex].Cells[cellItem.Row, cellItem.Col].Text = cellItem.CellText;
                                }
                            }
                            catch (ExcelException exception)
                            {
                                //记录日志, 抛出异常, 不包装异常
                                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                            }
                        };
                        workers[sheetIndex, threadIndex].RunWorkerCompleted += (workSender, ea) =>
                        {
                            if (!ea.Cancelled)
                            {
                                lock (locker)
                                {
                                    threadCountExisted++;
                                    if (threadCountExisted == totalThreads)
                                    {
                                        progressPanel.Hide();
                                        Cursor = Cursors.Default;
                                    }
                                }
                            }
                        };
                        workers[sheetIndex, threadIndex].RunWorkerAsync(new RowAndCol(sheetIndex, threadIndex));
                    }
                    Cursor = Cursors.Default;
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        ///// <summary>
        ///// 显示行扩展统计报表
        ///// </summary>
        ///// <param name="coverId"></param>
        //private void ShowRowReporting(decimal coverId)
        //{
        //    ProgressForm frmProgress = new ProgressForm();
        //    try
        //    {
        //        IList<CommonNode> commonNodes = customSheetContract.GetCommonNodes(coverId, ReportType);
        //        int DataWarehouseId = customCoverContract.GetDataWarehouseId(coverId);
        //        if (fsReporting.ActiveSheetIndex < commonNodes.Count)
        //        {
        //            CommonNode commonNode = commonNodes[fsReporting.ActiveSheetIndex];
        //            IList<CustomCellStyleInfo> cellStyleInfos = customCellStyleContract.GetCustomCellStyleInfos(commonNode.NodeId, CellStatisticDataType.ExtendedRow);
        //            IList<CustomCellStyleInfo> styleInfos = customCellStyleContract.GetCustomCellStyleInfos(commonNode.NodeId, CellStatisticDataType.Detail);
        //            IList<CustomCellStyleInfo> cellStyleOnlyDatgaInfos = customCellStyleContract.GetCustomCellStyleInfos(commonNode.NodeId, CellStatisticDataType.OnlyData);

        //            Dictionary<decimal, Dictionary<decimal, string>> textDatas = new Dictionary<decimal, Dictionary<decimal, string>>();
        //            int pgCount = 0;
        //            foreach (CustomCellStyleInfo customCellStyleInfo in cellStyleInfos)
        //            {
        //                CellRange cellRange = fsReporting.ActiveSheet.GetSpanCell(customCellStyleInfo.Row, customCellStyleInfo.Col);
        //                Dictionary<decimal, string> textData = customCellStyleContract.GetExtendedRowTextData(customCellStyleInfo.CellStyleId, DataWarehouseId, customCellStyleInfo.ConditionText,
        //                    customCellStyleInfo.TemplateText);
        //                textDatas.Add(customCellStyleInfo.CellStyleId, textData);
        //                pgCount += textData.Count;
        //            }
        //            if (cellStyleInfos.Count > 0)
        //            {
        //                frmProgress.MinValue = 0;
        //                frmProgress.TopMost = true;
        //                frmProgress.Text = string.Format("第{0}页面，正在计算中，请稍后...", fsReporting.ActiveSheetIndex + 1);
        //                frmProgress.MaxValue = pgCount;
        //                frmProgress.Show();
        //            }
        //            Dictionary<decimal, string> physicalTableNames = new Dictionary<decimal, string>();
        //            foreach (CustomCellStyleInfo customCellStyleInfo in cellStyleInfos)
        //            {
        //                CellRange cellRange = fsReporting.ActiveSheet.GetSpanCell(customCellStyleInfo.Row, customCellStyleInfo.Col);
        //                //Dictionary<decimal, string> textData = customCellStyleContract.GetExtendedRowTextData(customCellStyleInfo.CellStyleId, DataWarehouseId, customCellStyleInfo.ConditionText,
        //                //    customCellStyleInfo.TemplateText);
        //                Dictionary<decimal, string> textData = textDatas[customCellStyleInfo.CellStyleId];
        //                frmProgress.MaxValue = textData.Count;
        //                int extendedRowCount = cellRange.RowCount * (textData.Count - 1);
        //                if (extendedRowCount > 0)
        //                {
        //                    fsReporting.ActiveSheet.RowCount = rowAndCols[fsReporting.ActiveSheetIndex].Row + cellRange.RowCount * textData.Count;
        //                    for (int rowIdx = 1; rowIdx < textData.Count; rowIdx++)
        //                    {
        //                        fsReporting.ActiveSheet.CopyRange(customCellStyleInfo.Row, 0, customCellStyleInfo.Row + rowIdx * cellRange.RowCount,
        //                            0, cellRange.RowCount, fsReporting.ActiveSheet.ColumnCount, false);
        //                    }
        //                    fsReporting.ActiveSheet.Rows[customCellStyleInfo.Row + cellRange.RowCount, rowAndCols[fsReporting.ActiveSheetIndex].Row + cellRange.RowCount * textData.Count - 1].Height
        //                            = fsReporting.ActiveSheet.Cells[customCellStyleInfo.Row, 0].Row.Height;
        //                }
        //                int idx = 0;
        //                foreach (KeyValuePair<decimal, string> keyValue in textData)
        //                {
        //                    IList<WhereConditon> whereConditons = new List<WhereConditon>();
        //                    whereConditons.Add(new WhereConditon("DepId", "DepId", System.Data.DbType.Decimal, keyValue.Key,
        //                        DataFieldCondition.Equal, DataFieldInnerRealtion.None, DataFieldBracket.None, 0));
        //                    fsReporting.ActiveSheet.Cells[customCellStyleInfo.Row + idx * cellRange.RowCount, customCellStyleInfo.Col].Text = keyValue.Value;
        //                    for (int rowIdx = 0; rowIdx < cellRange.RowCount; rowIdx++)
        //                    {
        //                        fsReporting.ActiveSheet.Rows[customCellStyleInfo.Row + idx * cellRange.RowCount + rowIdx].Tag = keyValue.Key;
        //                    }
        //                    Application.DoEvents();
        //                    for (int j = 0; j < styleInfos.Count; j++)
        //                    {
        //                        if (styleInfos[j].Row == customCellStyleInfo.Row)
        //                        {
        //                            fsReporting.ActiveSheet.Cells[styleInfos[j].Row + idx * cellRange.RowCount, styleInfos[j].Col].Text =
        //                                customCellStyleContract.GetRowCellText(styleInfos[j].CellStyleId, DataWarehouseId, styleInfos[j].ConditionText,
        //                                styleInfos[j].TemplateText, whereConditons, relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
        //                        }
        //                    }
        //                    Application.DoEvents();
        //                    for (int j = 0; j < cellStyleOnlyDatgaInfos.Count; j++)
        //                    {
        //                        NumberCellType ncDataType = new NumberCellType();
        //                        ncDataType.DecimalPlaces = 0;
        //                        fsReporting.ActiveSheet.Cells[cellStyleOnlyDatgaInfos[j].Row + idx * cellRange.RowCount, cellStyleOnlyDatgaInfos[j].Col].CellType = ncDataType;
        //                        fsReporting.ActiveSheet.Cells[cellStyleOnlyDatgaInfos[j].Row + idx * cellRange.RowCount, cellStyleOnlyDatgaInfos[j].Col].Tag = cellStyleOnlyDatgaInfos[j];
        //                        fsReporting.ActiveSheet.Cells[cellStyleOnlyDatgaInfos[j].Row + idx * cellRange.RowCount, cellStyleOnlyDatgaInfos[j].Col].Text = customCellStyleContract.GetRowCellStatiscsText(cellStyleOnlyDatgaInfos[j].CellStyleId,
        //                                DataWarehouseId, cellStyleOnlyDatgaInfos[j].ConditionText, cellStyleOnlyDatgaInfos[j].TemplateText, whereConditons,
        //                                relatedUserTypeCommonNodes, relatedDepartmentCommonNodes);
        //                    }
        //                    frmProgress.IncreaseStep();
        //                    Application.DoEvents();
        //                    idx++;
        //                    if (frmProgress != null && frmProgress.Cancel)
        //                    {
        //                        break; ;
        //                    }
        //                }
        //            }
        //            if (frmProgress != null && !frmProgress.IsDisposed)
        //            {
        //                frmProgress.CloseFrom();
        //            }
        //        }
        //    }
        //    catch (Exception exception)
        //    {
        //        if (frmProgress != null && !frmProgress.IsDisposed)
        //        {
        //            frmProgress.CloseFrom();
        //        }
        //        Cursor = Cursors.Default;
        //        WinExceptionHelper.NoExceptionAndAlertPolicy(exception);
        //    }
        //}

        /// <summary>
        /// 显示统计报表
        /// </summary>
        private void ShowStatisticReporting()
        {
            ProgressForm frmProgress = new ProgressForm();
            try
            {

                IList<CommonNode> commonNodes = customSheetContract.GetChildNodes(CurrentReportInfo.ReportId);
                byte dataWarehouseId = customReportContract.GetDataWarehouseId(CurrentReportInfo.ReportId);
                if (fsReporting.ActiveSheetIndex < commonNodes.Count)
                {
                    CommonNode commonNode = commonNodes[fsReporting.ActiveSheetIndex];
                    IList<CustomCellInfo> datas = customCellContract.GetModelInfos(commonNode.NodeId, (byte)StatisticCellType.OnlyData);
                    IList<CustomCellInfo> values = customCellContract.GetModelInfos(commonNode.NodeId, (byte)StatisticCellType.OnlyValue);
                    IList<CustomCellInfo> details = customCellContract.GetModelInfos(commonNode.NodeId, (byte)StatisticCellType.Detail);
                    int cnt = datas.Count + values.Count + details.Count;
                    int threadCountExisted = 0;
                    List<CustomCellInfo> customCellInfos = new List<CustomCellInfo>();
                    if (cnt > 0)
                    {
                        if (datas.Count > 0)
                        {
                            customCellInfos.AddRange(datas.ToArray());
                        }
                        if (values.Count > 0)
                        {
                            customCellInfos.AddRange(values.ToArray());
                        }
                        if (details.Count > 0)
                        {
                            customCellInfos.AddRange(details.ToArray());
                        }
                    }
                    if (customCellInfos.Count <= 0)
                    {
                        return;
                    }
                    int maxValue = customCellInfos.Count / MAX_THREAD_COUNT + 1;
                    if (customCellInfos.Count % MAX_THREAD_COUNT != 0)
                    {
                        maxValue++;
                    }
                    frmProgress.MinValue = 0;
                    frmProgress.TopMost = true;
                    frmProgress.Text = string.Format("第{0}页面，正在计算中，请稍后...", fsReporting.ActiveSheetIndex + 1);
                    frmProgress.MaxValue = maxValue;
                    int progressIndex = 0;
                    int threadCount = customCellInfos.Count < MAX_THREAD_COUNT ? 1 : MAX_THREAD_COUNT;
                    BackgroundWorker[] workers = new BackgroundWorker[threadCount];
                    //AutoResetEvent[] autoResetEvents = new AutoResetEvent[threadCount];
                    for (int index = 0; index < threadCount; index++)
                    {
                        //autoResetEvents[index] = new AutoResetEvent(false);
                        workers[index] = new BackgroundWorker()
                        {
                            WorkerReportsProgress = true,
                            WorkerSupportsCancellation = true
                        };                        
                        workers[index].DoWork += (workSender, ea) =>
                        {
                            var worker = workSender as BackgroundWorker;
                            int threadIndex = Convert.ToInt32(ea.Argument);
                            for (int rowIndex = threadIndex; rowIndex < customCellInfos.Count; rowIndex += threadCount)
                            {
                                if (worker.CancellationPending)
                                {
                                    ea.Cancel = true;
                                    break;
                                }
                                CustomCellInfo customCellInfo = customCellInfos[rowIndex];
                                try
                                {
                                    StatisticCellType statisticCellType = (StatisticCellType)customCellInfo.CellType;
                                    switch (statisticCellType)
                                    {
                                        case StatisticCellType.OnlyData:
                                            NumberCellType ncDataType = new NumberCellType();                                            
                                            TextIntValue textIntValue = customCellContracts[threadIndex].GetCellText(customCellInfo.CellId, dataWarehouseId, authorityCondition.RelatedUserTypeCommonNodes, authorityCondition.RelatedDepartmentCommonNodes);
                                            ncDataType.DecimalPlaces = textIntValue.Digit;
                                            ThreadCellItem cellItem = new ThreadCellItem(threadIndex, fsReporting.ActiveSheetIndex, customCellInfo.RowIndex, customCellInfo.ColIndex, textIntValue.Text, ncDataType);
                                            worker.ReportProgress(rowIndex, cellItem);
                                            break;

                                        case StatisticCellType.OnlyValue:
                                            string valueText = customCellContracts[threadIndex].GetCellText(customCellInfo.CellId, CurrentUser.Instance.UserId, customCellInfo.TableId, 
                                                customCellInfo.ConditionText, customCellInfo.TemplateText);
                                            ThreadCellItem item = new ThreadCellItem(threadIndex, fsReporting.ActiveSheetIndex, customCellInfo.RowIndex, customCellInfo.ColIndex, valueText);
                                            worker.ReportProgress(rowIndex, item);
                                            break;

                                        case StatisticCellType.Detail:
                                            DataTable dt = customCellContracts[threadIndex].GetCellRangeData(customCellInfo.CellId, dataWarehouseId, authorityCondition.RelatedUserTypeCommonNodes, authorityCondition.RelatedDepartmentCommonNodes);
                                            if (dt != null && dt.Rows.Count > 0 && dt.Columns.Count > 0)
                                            {
                                                ThreadCellItem ciText = new ThreadCellItem(threadIndex, fsReporting.ActiveSheetIndex, customCellInfo.RowIndex, customCellInfo.ColIndex, dt);
                                                worker.ReportProgress(rowIndex, ciText);                                                
                                            }
                                            break;
                                    }                                    
                                    //autoResetEvents[threadIndex].WaitOne();
                                }
                                catch (Exception ex)
                                {
                                    throw ex;
                                }
                            }
                        };
                        workers[index].ProgressChanged += (workSender, ea) =>
                        {
                            try
                            {                               
                                ThreadCellItem cellItem = (ThreadCellItem)ea.UserState;
                                if (cellItem != null)
                                {                                    
                                    switch (cellItem.ValueMode)
                                    {
                                        case CustomBool.False:
                                            if (cellItem.Row < fsReporting.Sheets[cellItem.SheetIndex].Rows.Count && cellItem.Col < fsReporting.Sheets[cellItem.SheetIndex].ColumnCount)
                                            {
                                                if (cellItem.CellType != null)
                                                {
                                                    fsReporting.Sheets[cellItem.SheetIndex].Cells[cellItem.Row, cellItem.Col].CellType = cellItem.CellType;
                                                }
                                                fsReporting.Sheets[cellItem.SheetIndex].Cells[cellItem.Row, cellItem.Col].Text = cellItem.CellText;
                                            }
                                            break;

                                        case CustomBool.True:
                                            DataTable dt = cellItem.CellValue as DataTable;
                                            if (dt.Rows.Count > 1)
                                            {
                                                fsReporting.ActiveSheet.AddRows(cellItem.Row + 1, dt.Rows.Count - 1);
                                            }
                                            if (fsReporting.ActiveSheet.RowCount < cellItem.Row + dt.Rows.Count + 1)
                                            {
                                                fsReporting.ActiveSheet.RowCount = cellItem.Row + dt.Rows.Count + 2;
                                            }
                                            if (fsReporting.ActiveSheet.ColumnCount < cellItem.Col + dt.Columns.Count)
                                            {
                                                fsReporting.ActiveSheet.ColumnCount = cellItem.Col + dt.Columns.Count + 2;
                                            }
                                            fsReporting.ActiveSheet.Cells[cellItem.Row, cellItem.Col,
                                               cellItem.Row + dt.Rows.Count + 1, cellItem.Col + dt.Columns.Count].CellType = new TextCellType();
                                            for (int j = 0; j < dt.Columns.Count; j++)
                                            {
                                                ICellType cellType = null;
                                                if (dt.Columns[j].DataType == typeof(DateTime))
                                                {
                                                    cellType =
                                                    fsReporting.ActiveSheet.Cells[cellItem.Row, cellItem.Col + j,
                                                        cellItem.Row + dt.Rows.Count - 1, cellItem.Col + j].CellType = new DateTimeCellType();
                                                }
                                                else if (dt.Columns[j].DataType == typeof(Decimal))
                                                {
                                                    NumberCellType numberCellType = new NumberCellType();
                                                    numberCellType.DecimalPlaces = 2;
                                                    fsReporting.ActiveSheet.Cells[cellItem.Row, cellItem.Col + j,
                                                        cellItem.Row + dt.Rows.Count - 1, cellItem.Col + j].CellType = numberCellType;
                                                }
                                                else if (dt.Columns[j].DataType == typeof(Int32))
                                                {
                                                    NumberCellType numberCellType = new NumberCellType();
                                                    numberCellType.DecimalPlaces = 0;
                                                    fsReporting.ActiveSheet.Cells[cellItem.Row, cellItem.Col + j,
                                                        cellItem.Row + dt.Rows.Count - 1, cellItem.Col + j].CellType = numberCellType;
                                                }
                                            }
                                            for (int i = 0; i < dt.Rows.Count; i++)
                                            {
                                                for (int j = 0; j < dt.Rows.Count; j++)
                                                {
                                                    fsReporting.ActiveSheet.Cells[cellItem.Row + i, cellItem.Col + j].Text = DataConvertionHelper.GetConvertedString(dt.Rows[i][j]);
                                                }
                                            }
                                            break;
                                    }                                    
                                }
                                if (progressIndex++ % MAX_THREAD_COUNT == 0)
                                {
                                    fsReporting.Refresh();
                                    frmProgress.IncreaseStep();
                                    frmProgress.RefreshProgressBar();
                                }
                                //Application.DoEvents();
                                //autoResetEvents[cellItem.ThreadIndex].Set();
                            }
                            catch (ExcelException exception)
                            {
                                //记录日志, 抛出异常, 不包装异常
                                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                            }
                        };
                        workers[index].RunWorkerCompleted += (workSender, ea) =>
                        {
                            if (!ea.Cancelled)
                            {
                                lock (locker)
                                {
                                    threadCountExisted++;
                                    if (threadCountExisted == threadCount)
                                    {
                                        frmProgress.CloseFrom();
                                        Cursor = Cursors.Default;
                                    }
                                }
                            }
                        };
                        
                    }
                    for (int index = 0; index < threadCount; index++)
                    {
                        workers[index].RunWorkerAsync(index);
                    }
                    frmProgress.TaskCancelled = delegate ()
                    {
                        for (int idx = 0; idx < threadCount; idx++)
                        {
                            if (!workers[idx].CancellationPending)
                            {
                                workers[idx].CancelAsync();
                            }
                        }
                    };
                    Cursor = Cursors.Default;
                    frmProgress.ShowDialog();
                }
            }
            catch (Exception exception)
            {
                if (frmProgress != null && !frmProgress.IsDisposed)
                {
                    frmProgress.CloseFrom();
                }
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 打开快照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmOpen_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SnapshotForm frmCoverSnapshot = new SnapshotForm();
            frmCoverSnapshot.ReportId = CurrentReportInfo.ReportId;
            frmCoverSnapshot.OpenSnapshot = (snapshotId) =>
                {
                    byte[] data = customSnapshotContract.DownloadSnapshot(snapshotId);
                    IList<RowAndCol> rowAndCols = customSheetContract.GetRowAndColCount(CurrentReportInfo.ReportId);
                    if (data != null && data.Length > 0)
                    {
                        ReportHelper.ShowFpSpread(fsReporting, data, rowAndCols);
                    }
                    else
                    {
                        MessageBox.Show("该快照文件并不存在！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                };
            frmCoverSnapshot.ShowDialog();
        }    

        /// <summary>
        /// 保存快照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveSnapshotForm frmSaveSnapshot = new SaveSnapshotForm();
            if ((QueryReportType)CurrentReportInfo.ReportType == QueryReportType.Basic)
            {
                frmSaveSnapshot.SaveCoverSnapshot = (snapshotName, expireDate, notes) =>
                {
                    if (userListControl.SelectedUserId > 0)
                    {
                        byte[] data = ReportHelper.GetFpSpreadData(fsReporting);
                        customSnapshotContract.Insert(new CustomSnapshotInfo(0, CurrentReportInfo.ReportId, userListControl.SelectedUserId,
                            snapshotName, string.Format("{0}_{1}_{{0}}", userListControl.SelectedUserId, CurrentReportInfo.ReportId), expireDate, 0, notes), data);
                    }
                };
            }
            else
            {
                frmSaveSnapshot.SaveCoverSnapshot = (snapshotName, expireDate, notes) =>
                {
                    byte[] data = ReportHelper.GetFpSpreadData(fsReporting);
                    customSnapshotContract.Insert(new CustomSnapshotInfo(0, CurrentReportInfo.ReportId, decimal.MinValue,
                        snapshotName, string.Format("{0}_{{0}}", CurrentReportInfo.ReportId), expireDate, 0, notes), data);
                };
            }
            frmSaveSnapshot.ShowDialog();
        }        

        /// <summary>
        /// 导出 Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ReportHelper.ExprotExcel(fsReporting, this.Text);
        }

        /// <summary>
        /// 打印预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPreview_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonNode commonNode = fsReporting.ActiveSheet.Tag as CommonNode;
            if (commonNode != null)
            {
                Cursor = Cursors.WaitCursor;
                CustomMargin customMargin = customSheetContract.GetMargin(commonNode.NodeId);
                ReportHelper.Preview(fsReporting, customMargin);
                Cursor = Cursors.Default;
            }
        }

        /// <summary>
        /// 打印
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPrint_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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
        /// 打印设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPringSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonNode commonNode = fsReporting.ActiveSheet.Tag as CommonNode;
            if (commonNode != null)
            {
                ReportHelper.ShowPrintSettingForm(customSheetContract, commonNode.NodeId);
            }
        }

        /// <summary>
        /// 打印PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmPrintPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        /// <summary>
        /// 插入照片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmInsertPhoto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            if (userListControl != null && userListControl.SelectedUserId > 0 && fsReporting.ActiveSheet.ActiveCell != null)
            {
                if (userListControl.ImageData != null && userListControl.ImageData.Length > 0)
                {
                    ReportHelper.InsertPhoto(fsReporting, fsReporting.ActiveSheet.ActiveCell, userListControl.ImageData);
                }
                else
                {
                    MessageBox.Show("该用户没有照片，无法插入！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 插入批量编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmInsertBatchNumber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IList<CommonNode> commonNodes = customSheetContract.GetChildNodes(CurrentReportInfo.ReportId);
                CommonNode node = commonNodes[fsReporting.ActiveSheetIndex];
                CustomSheetInfo customSheetInfo = customSheetContract.GetModelInfo(node.NodeId);
                fsReporting.ActiveSheet.ActiveCell.CellType = new TextCellType();
                string activeCellText = fsReporting.ActiveSheet.ActiveCell.Text;
                if (!string.IsNullOrWhiteSpace(activeCellText))
                {
                    try
                    {
                        fsReporting.ActiveSheet.ActiveCell.Text = string.Format(activeCellText, customSheetInfo.ApprovalNumber);
                    }
                    catch
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("当前单元格的字符串格式错误，请检查！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    fsReporting.ActiveSheet.ActiveCell.Text = customSheetInfo.ApprovalNumber.ToString();
                }
                customSheetContract.AutoIncreaseApprovalNumber(node.NodeId);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 设置单元格的值
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="value"></param>
        private string SetCellProperty(CustomDataFieldInfo customDataFieldInfo, object value)
        {
            string text = string.Empty;
            DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
            switch (dataFieldProperty)
            {
                case DataFieldProperty.SystemPhysicalDataField:
                    SystemDataField systemDataField = (SystemDataField)customDataFieldInfo.DataFieldId;
                    switch (systemDataField)
                    {
                        case SystemDataField.AuditedStatus:
                            AuditedStatus auditedStatus = (AuditedStatus)Convert.ToByte(value);
                            text = UserEnumHelper.GetEnumText(auditedStatus);
                            break;

                        case SystemDataField.CurrentState:
                            CurrentState currentState = (CurrentState)Convert.ToByte(value);
                            text = UserEnumHelper.GetEnumText(currentState);
                            break;

                        case SystemDataField.DepProperty:
                            IList<EnumItem> enumItems = SystemConfigHelper.GetDepartmentPorperty();
                            byte depPropertyValue = Convert.ToByte(value);
                            int index = enumItems.FindIndex(enumItem => enumItem.Value.Equals(depPropertyValue));
                            if (index >= 0)
                            {
                                text = enumItems[index].Text;
                            }
                            break;

                        default:
                            if (value != null)
                            {
                                text = DataConvertionHelper.GetConvertedString(value);
                            }
                            break;
                    }
                    break;

                default:
                    if (value != null)
                    {
                        text = DataConvertionHelper.GetConvertedString(value);
                    }
                    break;
            }

            return text;
        }

        /// <summary>
        /// 查看详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmView_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row = fsReporting.ActiveSheet.ActiveCell.Row.Index;
            int column = fsReporting.ActiveSheet.ActiveCell.Column.Index;
            ShowDetail(row, column); 
        }

        /// <summary>
        /// 双击单元格查看详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fsReporting_CellDoubleClick(object sender, CellClickEventArgs e)
        {
            ShowDetail(e.Row, e.Column);
        }

        /// <summary>
        /// 显示详情
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        private void ShowDetail(int row, int column)
        {
            IList<CommonNode> commonNodes = customSheetContract.GetChildNodes(CurrentReportInfo.ReportId);
            if (fsReporting.ActiveSheet.ActiveCell == null || fsReporting.ActiveSheetIndex >= commonNodes.Count)
            {
                return;
            }
            byte dataWarehouseId = customReportContract.GetDataWarehouseId(CurrentReportInfo.ReportId);
            CustomCellInfo customCellInfo = customCellContract.GetModelInfo(commonNodes[fsReporting.ActiveSheetIndex].NodeId, row, column);
            if (customCellInfo != null)
            {
                ShowDetaiForm showDetaiForm = new ShowDetaiForm();
                StatisticCellType cellType = (StatisticCellType)customCellInfo.CellType;
                switch (cellType)
                {
                    case StatisticCellType.OnlyData:
                        DataTable detail = customCellContract.GetCellDetail(customCellInfo.CellId, dataWarehouseId, authorityCondition.RelatedUserTypeCommonNodes, authorityCondition.RelatedDepartmentCommonNodes);
                        if (detail != null && detail.Rows.Count > 0)
                        {
                            showDetaiForm.DataDetail = detail;
                            showDetaiForm.CellId = customCellInfo.CellId;
                            showDetaiForm.DataWarehouseId = dataWarehouseId;
                            showDetaiForm.AuthorityCondition = authorityCondition;
                            showDetaiForm.ShowDialog();
                        }
                        break;
                }
            }
        }
    }
}
