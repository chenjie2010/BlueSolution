using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarPoint.Win.Spread.Model;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Excel;
using FarPoint.Win.Spread.CellType;
using DevExpress.XtraEditors.Controls;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class DataImportForm : Form
    {
        #region  私有常量      

        /* 线程组个数 */
        private const int MAX_THREAD_COUNT = 1;//35;

        /* 一次导入 2000 条记录 */
        private const int RECORD_COUNT_EXCHANGED = 2000;

        /* 列宽 */
        private const int COLUMN_WIDTH_IN_SHEET_VIEW = 180;

        /* 标题行高 */
        private const int ROW_HEIGHT_IN_SHEET_VIEW = 30;

        /* 记录数大于500时，缓存所有用户 */
        private const int USER_LOAD_ABOVE_MAX_ROW_COUNT = 500;

        #endregion

        #region  私有静态只读变量

        /// <summary>
        /// 加锁
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 错误格式的锁
        /// </summary>
        private static readonly object errorFormatlocker = new object();

        /// <summary>
        /// 已存在的记录锁
        /// </summary>
        private static readonly object skippedRecordlocker = new object();

        /// <summary>
        /// 枚举字典锁
        /// </summary>
        private static readonly object enumDicitonarylocker = new object();

        /// <summary>
        /// 关联字典锁
        /// </summary>
        private static readonly object assocationDicitonarylocker = new object();

        /// <summary>
        /// 用户字典锁
        /// </summary>
        private static readonly object userDicitonarylocker = new object();

        /// <summary>
        /// 用户编号字典锁
        /// </summary>
        private static readonly object userIdlocker = new object();                  

        #endregion

        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly ICustomGroupContract customGroupContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;

        #endregion

        #region 私有变量

        private Dictionary<int, decimal> dataFieldRelation;
        private Dictionary<int, Dictionary<int, ErrorDataFormat>> errorDataFormatDic;
        private Dictionary<int, IList<int>> skippedRecrodDic;
        private Dictionary<decimal, CustomDataFieldInfo> customDataFieldInfoDic;
        private Dictionary<string, CommonUserInfo> commonUserInfos;
        private Dictionary<decimal, IList<CustomEnumInfo>> dicEnumItem;
        private IList<CustomDepartmentInfo> customDepartmentInfos = null;
        private List<int> errorRows = null;
        private Dictionary<decimal, DataTable> assocationTable = null;
        private Dictionary<string, string> userIdentityAndNames = null;
        private List<decimal> duplicateUserIds = null;

        /// <summary>
        /// 数据表类型
        /// </summary>
        private DataTableType dataTableType = DataTableType.PrimaryTable;

        /// <summary>
        /// Excel 单元格内容变化是否生效
        /// </summary>
        private bool isCellChangedVaild = false;

        /// <summary>
        /// 校验是否完成
        /// </summary>
        private IDictionary<int, bool> dicValidationCompleted = null;

        /// <summary>
        /// 进度条
        /// </summary>
        private ProgressForm frmCommonProgress = null;

        /// <summary>
        /// 线程组
        /// </summary>
        private BackgroundWorker[] workers = new BackgroundWorker[MAX_THREAD_COUNT];

        /// <summary>
        /// 信号量数组
        /// </summary>
        private AutoResetEvent[] autoResetEvents = new AutoResetEvent[MAX_THREAD_COUNT];

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataImportForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            customGroupContract = BusinessChannelFactory.CreateCustomGroupContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();
            customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();

            gcDataFieldRelation.DataSource = CreateDataTable();
            dataFieldRelation = new Dictionary<int, decimal>();
            customDataFieldInfoDic = new Dictionary<decimal, CustomDataFieldInfo>();
            commonUserInfos = new Dictionary<string, CommonUserInfo>();
            dicEnumItem = new Dictionary<decimal, IList<CustomEnumInfo>>();
            dicValidationCompleted = new Dictionary<int, bool>();
            assocationTable = new Dictionary<decimal, DataTable>();
            userIdentityAndNames = new Dictionary<string, string>();
            errorRows = new List<int>();
            duplicateUserIds = new List<decimal>();
            UserControlHelper.InitImageComboBoxEdit(icmbDataSource, typeof(CustomDataSource));
            UserControlHelper.InitImageComboBoxEdit(icmbImport, typeof(ExportStyle));
            UserControlHelper.InitImageComboBoxEdit(icmbAudit, typeof(AuditedStatus));
            UserControlHelper.InitImageComboBoxEdit(icmbImportedKeyName, typeof(ImportedKeyName));
            UserControlHelper.InitImageComboBoxEdit(icmbCurrentState, typeof(CurrentState));
        }

        #endregion

        #region 窗体及控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataImportForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }


        /// <summary>
        /// 数据源变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbDataSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            CustomDataSource customDataSource = (CustomDataSource)Convert.ToByte(icmbDataSource.EditValue);
            switch (customDataSource)
            {
                case CustomDataSource.ExcelFile:
                    chkAddtionalType.Checked = true;
                    chkAddtionalType.Enabled = true;
                    break;

                case CustomDataSource.Remote:
                    chkAddtionalType.Checked = false;
                    chkAddtionalType.Enabled = false;
                    break;
            }
        }            

        /// <summary>
        /// 表头编号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewInput_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }

        /// <summary>
        /// 数据单元格变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewInput_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.VisibleIndex == 2)
            {
                int sourceId = DataConvertionHelper.GetInt(e.Value);
                if (sourceId > 0)
                {
                    dataFieldRelation.Clear();
                }
                lblStatus.Text = "未进行数据校验。";
            }
        }

        /// <summary>
        /// 弹出右键菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridViewInput_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                popupMenu.ItemLinks[0].Visible = false;
                popupMenu.ItemLinks[1].Visible = false;
                popupMenu.ItemLinks[2].Visible = true;
                popupMenu.ItemLinks[3].Visible = true;
                popupMenu.ShowPopup(Control.MousePosition);
            }
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmReset_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoadSourceDataField();
            isCellChangedVaild = false;
        }

        /// <summary>
        /// 清除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmClear_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DataTable dataTable = gcDataFieldRelation.DataSource as DataTable;
            foreach (DataRow dr in dataTable.Rows)
            {
                dr["SourceId"] = DBNull.Value;
            }
            isCellChangedVaild = false;
        }

        /// <summary>
        /// 加载Excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiLoad_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CustomDataSource customDataSource = (CustomDataSource)Convert.ToByte(icmbDataSource.EditValue);
            switch (customDataSource)
            {
                case CustomDataSource.ExcelFile:
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
                        isCellChangedVaild = false;
                        WinPlatformHelper.LastestFilePath = Path.GetDirectoryName(openFileDialog.FileName);
                        fpDataImported.OpenExcel(openFileDialog.FileName, ExcelOpenFlags.TruncateEmptyRowsAndColumns);
                        fpDataImported.TabStripInsertTab = false;
                        fpDataImported.TabStripPolicy = TabStripPolicy.AsNeeded;
                        fpDataImported.ActiveSheetIndex = 0;
                        for (int i = fpDataImported.Sheets.Count - 1; i >= 0; i--)
                        {
                            if ((fpDataImported.Sheets[i].RowCount == 0 || fpDataImported.Sheets[i].ColumnCount <= 1) && (fpDataImported.Sheets.Count > 1))
                            {
                                fpDataImported.Sheets.RemoveAt(i);
                            }
                        }
                        foreach (SheetView sheetView in fpDataImported.Sheets)
                        {
                            if (sheetView.ColumnCount <= 1)
                            {
                                MessageBox.Show(string.Format("导入的EXCEL文件的列必须大于1列，且第一列必须{0}！", icmbImportedKeyName.SelectedText), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            if (chkAddtionalType.Checked)
                            {
                                for (int i = 0; i < sheetView.ColumnCount; i++)
                                {
                                    sheetView.ColumnHeader.SetClip(0, i, 1, 1, sheetView.Cells[0, i].Text);
                                }
                                sheetView.Rows.Remove(0, 1);
                                sheetView.CellChanged += new SheetViewEventHandler(sheetView_CellChanged);
                            }
                            if (sheetView.RowCount > 1000)
                            {
                                sheetView.RowHeader.Columns[0].Width = sheetView.RowCount.ToString().Length * 12;
                            }
                        }
                        SpreadToolHelper.RemoveEmptyRows(fpDataImported);
                        foreach (SheetView sheetView in fpDataImported.Sheets)
                        {
                            sheetView.CellChanged += new SheetViewEventHandler(sheetView_CellChanged);
                        }
                        lblTip.Text = "提示：数据导入前请先进行数据校验。";
                        lblErrorTip.Text = "校验结果";
                        lblStatus.Text = "等待数据校验。";
                        LoadSourceDataField();
                        isCellChangedVaild = true;
                        dicValidationCompleted.Clear();
                        Cursor = Cursors.Default;
                    }
                    catch (Exception ex)
                    {
                        Cursor = Cursors.Default;
                        if (ex.Message.ToLower().Equals("error opening file"))
                        {
                            MessageBox.Show("该Excel文件已打开(请先关闭后再加载)！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            fpDataImported.Reset();
                            fpDataImported.TabStripInsertTab = false;
                            fpDataImported.TabStripPolicy = TabStripPolicy.Always;
                            MessageBox.Show("EXCEL文件加载失败, 该文件并不存在或者容与格式错误，请检查后重试！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    break;

                case CustomDataSource.Remote:
                    DataForm frmData = new DataForm();
                    frmData.UpdateDataTable = (dt) =>
                    {
                        if (dt.Columns.Count <= 1)
                        {
                            MessageBox.Show("导入数据表的列必须大于1列，且第一列必须为用户名！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        Cursor = Cursors.WaitCursor;
                        isCellChangedVaild = false;
                        fpDataImported.DataSource = dt;
                        for (int i = 0; i < fpDataImported.Sheets[0].ColumnCount; i++)
                        {
                            if (i < dt.Columns.Count)
                            {
                                fpDataImported.Sheets[0].ColumnHeader.SetClip(0, i, 1, 1, dt.Columns[i].Caption);
                            }
                        }
                        foreach (SheetView sheetView in fpDataImported.Sheets)
                        {
                            sheetView.CellChanged += new SheetViewEventHandler(sheetView_CellChanged);
                        }
                        LoadSourceDataField();
                        lblTip.Text = "提示：数据导入前请先进行数据校验。";
                        lblErrorTip.Text = "校验结果";
                        lblStatus.Text = "等待数据校验。";
                        isCellChangedVaild = true;
                        dicValidationCompleted.Clear();
                        Cursor = Cursors.Default;
                    };
                    frmData.ShowDialog();
                    break;
            }
        }

        /// <summary>
        /// 选择表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beTable_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
            frmDataTableItems.Text = "表选择";
            frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
            frmDataTableItems.NodeSelected = delegate (CommonNode node)
            {
                if (dicValidationCompleted.ContainsKey(fpDataImported.ActiveSheetIndex))
                {
                    dicValidationCompleted[fpDataImported.ActiveSheetIndex] = false;
                }
                else
                {
                    dicValidationCompleted.Add(fpDataImported.ActiveSheetIndex, false);
                }
                if (node != null)
                {
                    beTable.Text = node.NodeName;
                    beTable.Tag = node;
                    dataTableType = (DataTableType)customTableContract.GetTableType(node.NodeId);
                    icmbCurrentState.Enabled = (dataTableType == DataTableType.MasterSlaveTable);
                    IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodes(node.NodeId, DataFieldFilter.OnlyPhysicalField);
                    RefreshDestinationDataField(commonNodes);
                }
                else
                {
                    beTable.Text = string.Empty;
                    beTable.Tag = null;
                }
            };
            frmDataTableItems.ShowDialog();
        }

        /// <summary>
        /// 设置单元格格式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCellFormat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CellFormatForm frmCellFormat = new CellFormatForm();
            frmCellFormat.SetCellType = (cellType)
                =>
            {
                SpreadToolHelper.SetCellTypeOnCells(fpDataImported, cellType);
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
            if (fpDataImported.ActiveSheet.RowCount <= 0)
            {
                MessageBox.Show("请先加载包含数据的Excel文件！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (beTable.Tag == null)
            {
                MessageBox.Show("请先选择数据表！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            gridViewInput.FocusedRowHandle = 0;
            gridViewInput.FocusedColumn = gridViewInput.Columns[0];
            fpDataImported.Focus();
            Application.DoEvents();
            //if (MessageBox.Show("确认进行数据校验？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            //{
            //    return;
            //}

            try
            {
                Cursor = Cursors.WaitCursor;
                if (dicValidationCompleted.ContainsKey(fpDataImported.ActiveSheetIndex))
                {
                    dicValidationCompleted[fpDataImported.ActiveSheetIndex] = false;
                }
                else
                {
                    dicValidationCompleted.Add(fpDataImported.ActiveSheetIndex, false);
                }
                isCellChangedVaild = false;
                int cellCount = fpDataImported.ActiveSheet.RowCount * fpDataImported.ActiveSheet.ColumnCount;
                int rowCount = fpDataImported.ActiveSheet.RowCount;
                InitCommonProgress("正在校验中，请稍后...", string.Format("提示：当前页面有{0}行{1}列共计{2}个单元格待校验。", fpDataImported.ActiveSheet.RowCount, fpDataImported.ActiveSheet.ColumnCount, cellCount), 0, rowCount);
                lblStatus.Text = "正在进行数据校验...";
                int threadCountExisted = 0;
                InitThreadResrouces(MAX_THREAD_COUNT);
                ClearErrorText();
                /* 1. 获得字段与列的对应关系 */
                if (!GetDataFieldRelation())
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("本地表字段与数据源字段必须是一一对应的关系！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                Dictionary<int, ErrorDataFormat> errorDataFormats = null;
                IList<int> skippedRecords = null;
                if (!errorDataFormatDic.ContainsKey(fpDataImported.ActiveSheetIndex))
                {
                    errorDataFormats = new Dictionary<int, ErrorDataFormat>();
                    errorDataFormatDic.Add(fpDataImported.ActiveSheetIndex, errorDataFormats);
                    if (skippedRecrodDic.ContainsKey(fpDataImported.ActiveSheetIndex))
                    {
                        skippedRecrodDic.Remove(fpDataImported.ActiveSheetIndex);
                    }
                    skippedRecords = new List<int>();
                    skippedRecrodDic.Add(fpDataImported.ActiveSheetIndex, skippedRecords);
                }
                else
                {
                    errorDataFormats = errorDataFormatDic[fpDataImported.ActiveSheetIndex];
                    skippedRecords = skippedRecrodDic[fpDataImported.ActiveSheetIndex];
                }
                errorDataFormats.Clear();
                skippedRecords.Clear();
                duplicateUserIds.Clear();
                if (commonUserInfos.Count == 0 && rowCount > USER_LOAD_ABOVE_MAX_ROW_COUNT)
                {
                    commonUserInfos = userAccountContract.GetCommonUserInfos();
                }

                /* 2. 验证字段 */
                ExportStyle exportStyle = (ExportStyle)Convert.ToByte(icmbImport.EditValue);
                CommonNode commonNode = beTable.Tag as CommonNode;

                /* 开始数据校验 */
                for (int idx = 0; idx < MAX_THREAD_COUNT; idx++)
                {
                    workers[idx].DoWork += (workSender, ea) =>
                    {
                        ErrorDataFormat errorDataFormat = ErrorDataFormat.None;
                        var worker = workSender as BackgroundWorker;
                        int threadIndex = Convert.ToInt32(ea.Argument);
                        for (int rowIndex = threadIndex; rowIndex < fpDataImported.ActiveSheet.RowCount; rowIndex += MAX_THREAD_COUNT)
                        {
                            if (worker.CancellationPending)
                            {
                                ea.Cancel = true;
                                break;
                            }
                            /* 2.1 先验证关键字（第0列为关键字列） */
                            errorDataFormat = ValidateKeyName(rowIndex, exportStyle, commonNode.NodeId, skippedRecords);
                            if (errorDataFormat != ErrorDataFormat.None)
                            {
                                int index = rowIndex * fpDataImported.ActiveSheet.ColumnCount;
                                lock (errorFormatlocker)
                                {
                                    if (!errorDataFormats.ContainsKey(index))
                                    {
                                        errorDataFormats.Add(index, errorDataFormat);
                                    }
                                }
                            }
                            CellDataValidationResult cellDataValidationResult = new CellDataValidationResult(threadIndex, rowIndex, 0, errorDataFormat);
                            worker.ReportProgress(rowIndex, cellDataValidationResult);
                            autoResetEvents[threadIndex].WaitOne();
                        }

                        /* 2.2 从第1列开始为数据列，验证数据列 */
                        for (int col = 1; col < fpDataImported.ActiveSheet.ColumnCount; col++)
                        {
                            if (!dataFieldRelation.ContainsKey(col))
                            {
                                continue;
                            }
                            for (int rowIndex = threadIndex; rowIndex < fpDataImported.ActiveSheet.RowCount; rowIndex += MAX_THREAD_COUNT)
                            {
                                if (worker.CancellationPending)
                                {
                                    ea.Cancel = true;
                                    break;
                                }
                                errorDataFormat = ValidateCellData(fpDataImported.ActiveSheet.Cells[rowIndex, col], customDataFieldInfoDic[dataFieldRelation[col]], fpDataImported.ActiveSheet.Cells[rowIndex, col].Value);
                                if (errorDataFormat != ErrorDataFormat.None)
                                {
                                    int index = rowIndex * fpDataImported.ActiveSheet.ColumnCount + col;
                                    lock (errorFormatlocker)
                                    {
                                        if (!errorDataFormats.ContainsKey(index))
                                        {
                                            errorDataFormats.Add(index, errorDataFormat);
                                        }
                                    }
                                }
                            }
                        }
                    };
                    workers[idx].ProgressChanged += (workSender, ea) =>
                    {
                        try
                        {
                            var worker = workSender as BackgroundWorker;
                            if (!worker.CancellationPending)
                            {
                                frmCommonProgress.IncreaseStep();
                                CellDataValidationResult cellDataValidationResult = (CellDataValidationResult)ea.UserState;
                                autoResetEvents[cellDataValidationResult.SheetIndex].Set();
                            }
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
                                threadCountExisted++;
                                if (threadCountExisted == MAX_THREAD_COUNT)
                                {
                                    isCellChangedVaild = true;
                                    if (errorDataFormatDic.ContainsKey(fpDataImported.ActiveSheetIndex))
                                    {
                                        Dictionary<int, ErrorDataFormat> dataFormats = errorDataFormatDic[fpDataImported.ActiveSheetIndex];
                                        if (dataFormats.Count == 0)
                                        {
                                            lblStatus.Text = "数据校验已完成，校验正确。";
                                        }
                                        else
                                        {                                            
                                            lblStatus.Text = string.Format("数据校验已完成，错误单元格数共有：{0}个。", dataFormats.Count);
                                        }
                                        ShowErrorDataTable(dataFormats);
                                        if (dicValidationCompleted.ContainsKey(fpDataImported.ActiveSheetIndex))
                                        {
                                            dicValidationCompleted[fpDataImported.ActiveSheetIndex] = true;
                                        }
                                        else
                                        {
                                            dicValidationCompleted.Add(fpDataImported.ActiveSheetIndex, true);
                                        }
                                    }
                                    frmCommonProgress.CloseFrom();
                                }
                            }
                        }
                    };
                }
                frmCommonProgress.TaskCancelled = delegate ()
                {
                    for (int idx = 0; idx < MAX_THREAD_COUNT; idx++)
                    {
                        if (!workers[idx].CancellationPending)
                        {
                            workers[idx].CancelAsync();
                        }
                    }
                    lblStatus.Text = "数据校验已取消。";
                };
                for (int idx = 0; idx < MAX_THREAD_COUNT; idx++)
                {
                    workers[idx].RunWorkerAsync(idx);
                }
                Cursor = Cursors.Default;
                frmCommonProgress.ShowDialog();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }

        /// <summary>
        /// 显示校验结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiShowResult_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool validationCompleted = false;
            if (dicValidationCompleted.ContainsKey(fpDataImported.ActiveSheetIndex))
            {
                validationCompleted = dicValidationCompleted[fpDataImported.ActiveSheetIndex];
            }
            if (!validationCompleted)
            {
                MessageBox.Show("请先进行数据校验。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (fpError.FlyoutPanelState.IsActive)
            {
                return;
            }
            fpError.ShowBeakForm();
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiImport_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkValidationRequired.Checked)
            {
                bool validationCompleted = false;
                if (dicValidationCompleted.ContainsKey(fpDataImported.ActiveSheetIndex))
                {
                    validationCompleted = dicValidationCompleted[fpDataImported.ActiveSheetIndex];
                }
                if (!validationCompleted)
                {
                    MessageBox.Show("请完成数据校验后再导入数据。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else
            {
                /* 获得字段与列的对应关系 */
                if (!GetDataFieldRelation())
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("本地表字段与数据源字段必须是一一对应的关系！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            if (beTable.Tag == null)
            {
                MessageBox.Show("导入数据前请先选择数据表！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ExportStyle exportStyle = (ExportStyle)Convert.ToByte(icmbImport.EditValue);
            if (MessageBox.Show(string.Format("确认以'{0}'的方式导入数据？", UserEnumHelper.GetEnumText(exportStyle)),
               "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }
            /* 导入数据 */
            byte audit = Convert.ToByte(icmbAudit.EditValue);
            DateTime defaultTime = DateTime.Parse(AppSettingHelper.YearMonthDay);
            IDictionary<int, ErrorDataFormat> errorDataFormats = null;
            if (errorDataFormatDic.ContainsKey(fpDataImported.ActiveSheetIndex))
            {
                errorDataFormats = errorDataFormatDic[fpDataImported.ActiveSheetIndex];
            }
            if (errorDataFormats != null && errorDataFormats.Count > 0)
            {
                foreach (KeyValuePair<int, ErrorDataFormat> errorData in errorDataFormats)
                {
                    if (chkAllowNull.Checked && errorData.Value == ErrorDataFormat.UnNull)
                    {
                        continue;
                    }
                    MessageBox.Show("导入数据前请先处理错误的数据单元格！", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (dataFieldRelation.Count == 0)
            {
                MessageBox.Show("本地表字段和数据源字段的对应关系发生变化，需要重新验证数据！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            IList<int> skippedRecords = new List<int>();
            if (skippedRecrodDic.ContainsKey(fpDataImported.ActiveSheetIndex))
            {
                skippedRecords = skippedRecrodDic[fpDataImported.ActiveSheetIndex];
            }
            if (commonUserInfos.Count == 0)
            {
                commonUserInfos = userAccountContract.GetCommonUserInfos();
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                CommonNode commonNode = beTable.Tag as CommonNode;
                CurrentState currentState = (CurrentState)Convert.ToByte(icmbCurrentState.EditValue);
                errorRows.Clear();
                int totalCount = 0;
                DataTable conditionDataTable = gcDataFieldRelation.DataSource as DataTable;
                IList<string> whereCluases = new List<string>();
                foreach (DataRow dr in conditionDataTable.Rows)
                {
                    string condition = DataConvertionHelper.GetConvertedString(dr["Condition"]);
                    if (!string.IsNullOrWhiteSpace(condition))
                    {
                        whereCluases.Add(condition);
                    }
                }
                progressPanel.Visible = true;
                progressPanel.Show();
                Application.DoEvents();
                /* 1. 创建表及准备用户的系统数据 */
                DataTable dataTable = CreateCustomDataTable(exportStyle);
                DataTable updatedDataTable = null;
                if (exportStyle == ExportStyle.UpdateAndInsert)
                {
                    updatedDataTable = CreateCustomDataTable(ExportStyle.UpdateAndNotInsert);
                }
                int pageSize = RECORD_COUNT_EXCHANGED; /* 一次导入 5000 条记录 */
                int reminder = fpDataImported.ActiveSheet.RowCount % pageSize;
                int pageCount = fpDataImported.ActiveSheet.RowCount / pageSize;
                if (reminder != 0)
                {
                    pageCount++;
                }
                int invalidUserCount = 0;
                for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
                {
                    for (int recordIndex = 0; recordIndex < pageSize; recordIndex++)
                    {
                        int rowIndex = pageIndex * pageSize + recordIndex;
                        if (rowIndex >= fpDataImported.ActiveSheet.RowCount)
                        {
                            break;
                        }
                        CommonUserInfo commonUserInfo = null;
                        string text = fpDataImported.ActiveSheet.Cells[rowIndex, 0].Text;
                        ImportedKeyName importedKeyName = (ImportedKeyName)Convert.ToByte(icmbImportedKeyName.EditValue);
                        if (importedKeyName == ImportedKeyName.UserIdetity)
                        {
                            if (userIdentityAndNames.ContainsKey(text))
                            {
                                text = userIdentityAndNames[text];
                            }
                        }
                        if (commonUserInfos.ContainsKey(text))
                        {
                            commonUserInfo = commonUserInfos[text];
                        }
                        else
                        {
                            invalidUserCount++;
                            errorRows.Add(rowIndex);
                            continue;
                        }
                        /* 2. 创建数据行并加载数据 */
                        DataRow dataRow = null;
                        switch (exportStyle)
                        {
                            case ExportStyle.Append:
                            case ExportStyle.NotUpdateAndInsert:
                            case ExportStyle.UpdateAndInsert:
                                if (!skippedRecords.Contains(rowIndex))
                                {
                                    dataRow = dataTable.NewRow();
                                    /* 2.1 系统数据 */
                                    dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId)] = commonUserInfo.UserId;
                                    dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserName)] = commonUserInfo.UserName;
                                    dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserTypeId)] = commonUserInfo.UserTypeId;
                                    dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.DepId)] = commonUserInfo.DepId;
                                    dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordSorting)] = 0;
                                    dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState)] = (byte)CurrentState.History;
                                    dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus)] = audit;
                                    dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.IsDeleted)] = false;
                                    dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CreationTime)] = defaultTime;
                                    dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime)] = defaultTime.AddSeconds(1);
                                    dataTable.Rows.Add(dataRow);
                                }
                                else
                                {
                                    if (exportStyle == ExportStyle.UpdateAndInsert)
                                    {
                                        dataRow = updatedDataTable.NewRow();
                                        dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId)] = commonUserInfo.UserId;
                                        dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime)] = defaultTime;
                                        updatedDataTable.Rows.Add(dataRow);
                                    }
                                    else
                                    {
                                        if (((dataTableType == DataTableType.PrimaryTable) || (exportStyle == ExportStyle.NotUpdateAndInsert)) && skippedRecords.Contains(rowIndex))
                                        {
                                            continue;
                                        }
                                    }
                                }
                                break;

                            case ExportStyle.UpdateAndNotInsert:
                                if (!skippedRecords.Contains(rowIndex))
                                {
                                    continue;
                                }
                                dataRow = dataTable.NewRow();
                                dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId)] = commonUserInfo.UserId;
                                dataRow[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime)] = defaultTime;
                                dataTable.Rows.Add(dataRow);
                                break;
                        }

                        /* 2.2 用户数据 */
                        for (int colIndex = 1; colIndex < fpDataImported.ActiveSheet.ColumnCount; colIndex++)
                        {
                            if (!dataFieldRelation.ContainsKey(colIndex))
                            {
                                continue;
                            }
                            CustomDataFieldInfo customDataFieldInfo = customDataFieldInfoDic[dataFieldRelation[colIndex]];
                            if (fpDataImported.ActiveSheet.Cells[rowIndex, colIndex].Value == null ||
                                string.IsNullOrWhiteSpace(fpDataImported.ActiveSheet.Cells[rowIndex, colIndex].Value.ToString()))
                            {
                                dataRow[customDataFieldInfo.PhysicalName] = DBNull.Value;
                            }
                            else
                            {
                                dataRow[customDataFieldInfo.PhysicalName] = fpDataImported.ActiveSheet.Cells[rowIndex, colIndex].Value;
                            }
                        }
                    }
                    Application.DoEvents();
                    switch (exportStyle)
                    {
                        case ExportStyle.Append:
                        case ExportStyle.NotUpdateAndInsert:
                            if (dataTable.Rows.Count > 0)
                            {
                                customTableContract.Import(commonNode.NodeId, dataTable);
                            }
                            break;

                        case ExportStyle.UpdateAndInsert:
                            if (dataTable.Rows.Count > 0)
                            {
                                customTableContract.Import(commonNode.NodeId, dataTable);
                            }
                            if (updatedDataTable.Rows.Count > 0)
                            {
                                customTableContract.Update(commonNode.NodeId, updatedDataTable, currentState, whereCluases);
                            }
                            break;

                        case ExportStyle.UpdateAndNotInsert:
                            if (dataTable.Rows.Count > 0)
                            {
                                customTableContract.Update(commonNode.NodeId, dataTable, currentState, whereCluases);
                            }
                            break;
                    }
                    dataTable.Rows.Clear();
                    Application.DoEvents();
                }
                if (pageCount > 0)
                {
                    totalCount = customTableContract.Sumbit(commonNode.NodeId, exportStyle);
                }
                Application.DoEvents();
                if (dicValidationCompleted.ContainsKey(fpDataImported.ActiveSheetIndex))
                {
                    dicValidationCompleted[fpDataImported.ActiveSheetIndex] = false;
                }
                else
                {
                    dicValidationCompleted.Add(fpDataImported.ActiveSheetIndex, false);
                }
                progressPanel.Hide();
                Cursor = Cursors.Default;
                int skip = 0;
                string tip = string.Empty;
                switch (exportStyle)
                {
                    case ExportStyle.Append:
                    case ExportStyle.UpdateAndInsert:
                        if (invalidUserCount > 0)
                        {
                            tip = string.Format("共有[{0}]条记录操作成功，[{1}]条记录由于用户不存在被忽略。", totalCount, invalidUserCount);
                        }
                        else
                        {
                            tip = string.Format("共有[{0}]条记录操作成功。", totalCount);
                        }
                        break;

                    case ExportStyle.NotUpdateAndInsert:
                    case ExportStyle.UpdateAndNotInsert:
                        skip = fpDataImported.ActiveSheet.RowCount - totalCount;
                        tip = string.Format("共有[{0}]条记录操作成功，忽略[{1}]条记录(其中[{2}]条记录由于用户不存在被忽略)。",
                            totalCount, skip, invalidUserCount);
                        break;
                }
                MessageBox.Show(tip, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblStatus.Text = tip;                
            }
            catch (Exception exception)
            {
                progressPanel.Hide();
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// Excel数据另存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SpreadToolHelper.ExprotExcel(fpDataImported, string.Format("{0}_Excel数据另存", beTable.Text));
        }

        /// <summary>
        /// 导入方式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbImport_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExportStyle exportStyle = (ExportStyle)Convert.ToByte(icmbImport.EditValue);
            if (exportStyle == ExportStyle.UpdateAndInsert || exportStyle == ExportStyle.NotUpdateAndInsert 
                || exportStyle == ExportStyle.UpdateAndNotInsert)
            {
                bbiSkippedDataImported.Enabled = true;
            }
            else
            {
                bbiSkippedDataImported.Enabled = false;
            }
        }

        /// <summary>
        /// 导入错误数据另存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiErrorDataImported_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (errorRows.Count > 0)
            {
                saveFileDialog.FileName = string.Format("{0}_导入错误数据_{1:yyyyMMddHHmmss}.xlsx", fpDataImported.ActiveSheet.SheetName, DateTime.Now);
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

                        /* 表的错误数据 */
                        SheetView sheetView = new SheetView(string.Format("{0}_导入错误数据", fpDataImported.ActiveSheet.SheetName));
                        fpSpread.Sheets.Add(sheetView);
                        sheetView.RowCount = errorRows.Count;
                        sheetView.ColumnCount = fpDataImported.ActiveSheet.ColumnCount;
                        sheetView.Cells[0, 0, errorRows.Count - 1, sheetView.ColumnCount - 1].CellType = new TextCellType();
                        sheetView.ColumnHeader.Rows[0].Height = ROW_HEIGHT_IN_SHEET_VIEW;
                        for (int idx = 0; idx < errorRows.Count; idx++)
                        {
                            for (int col = 0; col < sheetView.ColumnCount; col++)
                            {
                                if (idx == 0)
                                {
                                    /* 标题行 */
                                    //if (chkAddtionalType.Checked)
                                    //{
                                    //    sheetView.ColumnHeader.Cells[0, col].Text = fpDataImported.ActiveSheet.Cells[0, col].Text;
                                    //}
                                    //else
                                    //{
                                    //    sheetView.ColumnHeader.Cells[0, col].Text = fpDataImported.ActiveSheet.ColumnHeader.Cells[0, col].Text;
                                    //}
                                    sheetView.ColumnHeader.Cells[0, col].Text = fpDataImported.ActiveSheet.ColumnHeader.Cells[0, col].Text;
                                    sheetView.Columns[col].Width = COLUMN_WIDTH_IN_SHEET_VIEW;
                                }
                                /* 内容 */
                                sheetView.Cells[idx, col].Text = fpDataImported.ActiveSheet.Cells[errorRows[idx], col].Text;
                            }
                        }
                        if (errorRows.Count > 0)
                        {
                            /* 标题行颜色 */
                            sheetView.ColumnHeader.Cells[0, 0, 0, sheetView.ColumnCount - 1].BackColor = Color.LightGray;
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
        /// 校验错误数据另存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiErrorData_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            bool validationCompleted = false;
            if (dicValidationCompleted.ContainsKey(fpDataImported.ActiveSheetIndex))
            {
                validationCompleted = dicValidationCompleted[fpDataImported.ActiveSheetIndex];
            }
            if (!validationCompleted)
            {
                MessageBox.Show("请完成数据校验或者数据导入后再另存校验错误数据。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (errorDataFormatDic.ContainsKey(fpDataImported.ActiveSheetIndex))
            {
                Dictionary<int, ErrorDataFormat> dataFormats = errorDataFormatDic[fpDataImported.ActiveSheetIndex];
                if (dataFormats.Count == 0)
                {
                    MessageBox.Show("未发现校验错误数据。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                saveFileDialog.FileName = string.Format("{0}_校验错误数据_{1:yyyyMMddHHmmss}.xlsx", beTable.Text, DateTime.Now);
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
                        List<int> errorDataRows = GetErrorDataRows(dataFormats, fpDataImported.ActiveSheet.ColumnCount);
                        SheetView sheetView = new SheetView(string.Format("{0}_校验错误数据", fpDataImported.ActiveSheet.SheetName));
                        fpSpread.Sheets.Add(sheetView);
                        sheetView.RowCount = errorDataRows.Count;
                        sheetView.ColumnCount = fpDataImported.ActiveSheet.ColumnCount;
                        sheetView.Cells[0, 0, errorDataRows.Count - 1, sheetView.ColumnCount - 1].CellType = new TextCellType();
                        sheetView.ColumnHeader.Rows[0].Height = ROW_HEIGHT_IN_SHEET_VIEW;
                        for (int idx = 0; idx < errorDataRows.Count; idx++)
                        {
                            for (int col = 0; col < sheetView.ColumnCount; col++)
                            {
                                if (idx == 0)
                                {
                                    /* 标题行 */
                                    //if (chkAddtionalType.Checked)
                                    //{
                                    //    sheetView.ColumnHeader.Cells[0, col].Text = fpDataImported.ActiveSheet.Cells[0, col].Text;
                                    //}
                                    //else
                                    //{
                                    //    sheetView.ColumnHeader.Cells[0, col].Text = fpDataImported.ActiveSheet.ColumnHeader.Cells[0, col].Text;
                                    //}
                                    sheetView.ColumnHeader.Cells[0, col].Text = fpDataImported.ActiveSheet.ColumnHeader.Cells[0, col].Text;
                                    sheetView.Columns[col].Width = COLUMN_WIDTH_IN_SHEET_VIEW;
                                }
                                /* 内容 */
                                int key = errorDataRows[idx] * fpDataImported.ActiveSheet.ColumnCount + col;
                                if (dataFormats.ContainsKey(key))
                                {
                                    sheetView.Cells[idx, col].BackColor = Color.LightSteelBlue;
                                }
                                sheetView.Cells[idx, col].Text = fpDataImported.ActiveSheet.Cells[errorDataRows[idx], col].Text;
                            }
                            /* 标题行颜色 */
                            if (errorDataRows.Count > 0)
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
        /// 忽略数据另存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSkippedDataImported_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ExportStyle exportStyle = (ExportStyle)Convert.ToByte(icmbImport.EditValue);
            if (exportStyle != ExportStyle.NotUpdateAndInsert && exportStyle != ExportStyle.UpdateAndNotInsert)
            {
                return;
            }
            if (skippedRecrodDic.ContainsKey(fpDataImported.ActiveSheetIndex))
            {
                IList<int> skippedRecords = skippedRecrodDic[fpDataImported.ActiveSheetIndex];
                if ((exportStyle == ExportStyle.NotUpdateAndInsert && skippedRecords.Count == 0) ||
                    (exportStyle == ExportStyle.UpdateAndNotInsert && skippedRecords.Count == fpDataImported.ActiveSheet.RowCount))

                {
                    MessageBox.Show("没有忽略的数据。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                saveFileDialog.FileName = string.Format("{0}_忽略的数据_{1:yyyyMMddHHmmss}.xlsx", beTable.Text, DateTime.Now);
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
                        /* 表的错误数据 */
                        SheetView sheetView = new SheetView(string.Format("{0}_导入错误数据", fpDataImported.ActiveSheet.SheetName));
                        fpSpread.Sheets.Add(sheetView);                        
                        sheetView.ColumnCount = fpDataImported.ActiveSheet.ColumnCount;                        
                        sheetView.ColumnHeader.Rows[0].Height = ROW_HEIGHT_IN_SHEET_VIEW;
                        if (exportStyle == ExportStyle.NotUpdateAndInsert)
                        {
                            sheetView.RowCount = skippedRecords.Count;
                            sheetView.Cells[0, 0, sheetView.RowCount - 1, sheetView.ColumnCount - 1].CellType = new TextCellType();
                            for (int idx = 0; idx < skippedRecords.Count; idx++)
                            {
                                for (int col = 0; col < sheetView.ColumnCount; col++)
                                {
                                    if (idx == 0)
                                    {
                                        /* 标题行 */
                                        sheetView.ColumnHeader.Cells[0, col].Text = fpDataImported.ActiveSheet.ColumnHeader.Cells[0, col].Text;
                                        sheetView.Columns[col].Width = COLUMN_WIDTH_IN_SHEET_VIEW;
                                    }
                                    /* 内容 */
                                    sheetView.Cells[idx, col].Text = fpDataImported.ActiveSheet.Cells[skippedRecords[idx], col].Text;
                                }
                            }
                        }
                        else if (exportStyle == ExportStyle.UpdateAndNotInsert)
                        {
                            sheetView.RowCount = fpDataImported.ActiveSheet.RowCount - skippedRecords.Count;
                            sheetView.Cells[0, 0, sheetView.RowCount - 1, sheetView.ColumnCount - 1].CellType = new TextCellType();
                            int rowIndex = 0;
                            for (int idx = 0; idx < fpDataImported.ActiveSheet.RowCount; idx++)
                            {
                                if (skippedRecords.Contains(idx))
                                {
                                    continue;
                                }
                                for (int col = 0; col < sheetView.ColumnCount; col++)
                                {
                                    if (rowIndex == 0)
                                    {
                                        /* 标题行 */
                                        sheetView.ColumnHeader.Cells[0, col].Text = fpDataImported.ActiveSheet.ColumnHeader.Cells[0, col].Text;
                                        sheetView.Columns[col].Width = COLUMN_WIDTH_IN_SHEET_VIEW;
                                    }
                                    /* 内容 */
                                    sheetView.Cells[rowIndex, col].Text = fpDataImported.ActiveSheet.Cells[idx, col].Text;
                                }
                                rowIndex++;
                            }
                        }
                        else
                        {
                            throw new ArgumentException("不支持该枚举类型。");
                        }

                        /* 标题行颜色 */
                        sheetView.ColumnHeader.Cells[0, 0, 0, sheetView.ColumnCount - 1].BackColor = Color.LightGray;
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
                MessageBox.Show("没有忽略的数据。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCloseForm_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpError_ButtonClick(object sender, DevExpress.Utils.FlyoutPanelButtonClickEventArgs e)
        {
            fpError.HideBeakForm();
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

        /// <summary>
        /// 点击行，定位到错误的单位格位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int row = Convert.ToInt32(gridView.GetFocusedDataRow()["RowIndex"]);
            int col = Convert.ToInt32(gridView.GetFocusedDataRow()["ColumnIndex"]);
            if (row > 0 && col > 0 && row <= fpDataImported.ActiveSheet.RowCount && col <= fpDataImported.ActiveSheet.ColumnCount)
            {
                Dictionary<int, ErrorDataFormat> errorDataFormats = errorDataFormatDic[fpDataImported.ActiveSheetIndex];
                int index = (row - 1) * fpDataImported.ActiveSheet.ColumnCount + col - 1;
                if (errorDataFormats.ContainsKey(index))
                {
                    SetErrorInfo(row - 1, col - 1, errorDataFormats[index]);
                }
                lblTip.Text = string.Format("第{0}行第{1}列的错误描述：{2}。", row, col, gridView.GetFocusedDataRow()["Description"]);
                fpDataImported.ActiveSheet.SetActiveCell(row - 1, col - 1);
            }
        }

        /// <summary>
        /// 查看错误单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpSpread_CellClick(object sender, CellClickEventArgs e)
        {
            if (errorDataFormatDic != null && errorDataFormatDic.ContainsKey(fpDataImported.ActiveSheetIndex))
            {
                Dictionary<int, ErrorDataFormat> errorDataFormats = errorDataFormatDic[fpDataImported.ActiveSheetIndex];
                int index = e.Row * fpDataImported.ActiveSheet.ColumnCount + e.Column;
                if (errorDataFormats.ContainsKey(index))
                {
                    lblTip.Text = string.Format("第{0}行第{1}列（{2}）的错误描述：{3}。", e.Row + 1, e.Column + 1,
                        fpDataImported.ActiveSheet.ColumnHeader.Cells[0, e.Column].Text, UserEnumHelper.GetEnumText(errorDataFormats[index]));
                }
                else
                {
                    lblTip.Text = string.Empty;
                }
            }
        }

        /// <summary>
        /// 删除当前选择的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (fpDataImported.ActiveSheet.ActiveRowIndex >= 0)
            {
                if (MessageBox.Show("确认删除当前行？删除行后需要重新校验。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    if (dicValidationCompleted.ContainsKey(fpDataImported.ActiveSheetIndex))
                    {
                        dicValidationCompleted[fpDataImported.ActiveSheetIndex] = false;
                    }
                    else
                    {
                        dicValidationCompleted.Add(fpDataImported.ActiveSheetIndex, false);
                    }
                    fpDataImported.ActiveSheet.RemoveRows(fpDataImported.ActiveSheet.ActiveRowIndex, 1);
                    List<int> rows = new List<int>();
                    rows.Add(fpDataImported.ActiveSheet.ActiveRowIndex);
                    UpdateErrorDataFormats(rows);
                }
            }
        }

        /// <summary>
        /// 批量删除当前选择的行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnItmBatchDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CellRange cellRange = fpDataImported.ActiveSheet.GetSelection(0);
            if (cellRange == null && fpDataImported.ActiveSheet.ActiveCell == null)
            {
                return;
            }
            if (MessageBox.Show("确认批量删除当前选定行？批量删除行后需要重新校验。", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                if (dicValidationCompleted.ContainsKey(fpDataImported.ActiveSheetIndex))
                {
                    dicValidationCompleted[fpDataImported.ActiveSheetIndex] = false;
                }
                else
                {
                    dicValidationCompleted.Add(fpDataImported.ActiveSheetIndex, false);
                }
                fpDataImported.ActiveSheet.RemoveRows(cellRange.Row, cellRange.RowCount);
                List<int> rows = new List<int>();
                for (int idx = 0; idx < cellRange.RowCount; idx++)
                {
                    rows.Add(cellRange.Row + idx);
                }
                UpdateErrorDataFormats(rows);
            }
        }

        /// <summary>
        /// 弹出当前菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fpDataImported_MouseUp(object sender, MouseEventArgs e)
        {
            if ((e.Button & MouseButtons.Right) != 0)
            {
                popupMenu.ItemLinks[0].Visible = true;
                popupMenu.ItemLinks[1].Visible = true;
                popupMenu.ItemLinks[2].Visible = false;
                popupMenu.ItemLinks[3].Visible = false;
                popupMenu.ShowPopup(Control.MousePosition);
            }
        }

        /// <summary>
        /// 增加条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ribtnCondition_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            decimal destinationId = DataConvertionHelper.GetDecimal(gridViewInput.GetFocusedRowCellValue("DestinationId"));
            if (destinationId > 0)
            {
                CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModelInfo(destinationId);
                QueryConditionForm frmQueryCondition = new QueryConditionForm();
                if (gridViewInput.FocusedValue != null)
                {
                    frmQueryCondition.QueryCondition = gridViewInput.FocusedValue.ToString();
                }
                else
                {
                    frmQueryCondition.QueryCondition = string.Empty;
                }
                frmQueryCondition.IsValidate = true;
                frmQueryCondition.TableName = customTableContract.GetTablePhysicalName(customDataFieldInfo.TableId);
                frmQueryCondition.CustomDataFieldInfo = customDataFieldInfo;
                frmQueryCondition.UpdateTextHandler = (where) =>
                {
                    gridViewInput.SetFocusedValue(where);
                };
                frmQueryCondition.ShowDialog();
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 创建自定义表
        /// </summary>
        /// <param name="exportStyle"></param>
        /// <returns></returns>
        private DataTable CreateCustomDataTable(ExportStyle exportStyle)
        {
            DataTable dataTable = new DataTable();
            dataTable.TableName = "DataTable";

            /* 1. 增加系统列 */
            switch (exportStyle)
            {
                case ExportStyle.Append:
                case ExportStyle.NotUpdateAndInsert:
                case ExportStyle.UpdateAndInsert:
                    List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(SystemPhysicalDataField));
                    foreach (EnumItem enumItem in enumItems)
                    {
                        SystemPhysicalDataField systemPhysicalDataField = (SystemPhysicalDataField)enumItem.Value;
                        if (systemPhysicalDataField == SystemPhysicalDataField.RecordId || systemPhysicalDataField == SystemPhysicalDataField.BusinessId
                            || systemPhysicalDataField == SystemPhysicalDataField.BusinessForeignId || systemPhysicalDataField == SystemPhysicalDataField.BusinessAlternativeId)
                        {
                            continue;
                        }
                        string dbType = DataFieldHelper.GetTypeString(systemPhysicalDataField);
                        string physicalName = DataFieldHelper.GetSystemPhysicalDataFieldName(systemPhysicalDataField);
                        dataTable.Columns.Add(physicalName, Type.GetType(dbType));
                    }
                    break;

                case ExportStyle.UpdateAndNotInsert:
                    string userType = DataFieldHelper.GetTypeString(SystemPhysicalDataField.UserId);
                    string userId = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId);
                    dataTable.Columns.Add(userId, Type.GetType(userType));

                    string timeType = DataFieldHelper.GetTypeString(SystemPhysicalDataField.ModificationTime);
                    string modificationTime = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.ModificationTime);
                    dataTable.Columns.Add(modificationTime, Type.GetType(timeType));
                    break;
            }

            /* 2. 增加数据列 */
            foreach (KeyValuePair<int, decimal> keyValue in dataFieldRelation)
            {
                CustomDataFieldInfo customDataFieldInfo = customDataFieldInfoDic[keyValue.Value];
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                {
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                    if (physicalDataFieldType == PhysicalDataFieldType.Association || physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation
                        || physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                    {
                        BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(customDataFieldInfo.AssociatedDataFieldId);
                        physicalDataFieldType = DataFieldHelper.GetPhysicalDataFieldType(basedDataType);
                    }
                    string dbType = DataFieldHelper.GetTypeString(physicalDataFieldType);
                    dataTable.Columns.Add(customDataFieldInfo.PhysicalName, Type.GetType(dbType));
                }
            }

            return dataTable;
        }

        /// <summary>
        /// 获得错误的行数
        /// </summary>
        /// <param name="errorDataFormats"></param>
        /// <returns></returns>
        private List<int> GetErrorDataRows(Dictionary<int, ErrorDataFormat> errorDataFormats, int columnCountFixed)
        {
            List<int> errorDataRows = new List<int>();

            int index = 0;
            foreach (KeyValuePair<int, ErrorDataFormat> errorDataFormat in errorDataFormats)
            {
                int row = errorDataFormat.Key / columnCountFixed;
                if (!errorDataRows.Contains(row))
                {
                    errorDataRows.Add(row);
                }
                index++;
            }
            errorDataRows.Sort();

            return errorDataRows;
        }

        /// <summary>
        /// 清除错误文本显示
        /// </summary>
        private void ClearErrorText()
        {
            //fpSpread.ActiveSheet.Cells[1, 0, fpSpread.ActiveSheet.RowCount - 1, fpSpread.ActiveSheet.ColumnCount - 1].ResetBackColor();
            //fpSpread.ActiveSheet.Cells[1, 0, fpSpread.ActiveSheet.RowCount - 1, fpSpread.ActiveSheet.ColumnCount - 1].ErrorText = string.Empty;
            DataTable dataTable = (DataTable)gridControl.DataSource;
            if (dataTable != null)
            {
                dataTable.Clear();
                gridControl.RefreshDataSource();
            }
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
        /// 展现错误结果表
        /// </summary>
        private void ShowErrorDataTable(Dictionary<int, ErrorDataFormat> errorDataFormats)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("RowIndex", Type.GetType("System.Int32"));
            dataTable.Columns.Add("ColumnIndex", Type.GetType("System.Int32"));
            dataTable.Columns.Add("Description", Type.GetType("System.String"));
            dataTable.DefaultView.Sort = "RowIndex ASC, ColumnIndex ASC";
            gridControl.DataSource = dataTable;
            gridView.Columns[0].Caption = "行索引";
            gridView.Columns[1].Caption = "列索引";
            gridView.Columns[2].Caption = "错误描述";
            gridView.Columns[0].Width = 50;
            gridView.Columns[1].Width = 50;
            gridView.Columns[2].Width = 160;
            int count = 0;
            Dictionary<int, ErrorDataFormat> newErrorDataFormats = errorDataFormats.OrderBy(o => o.Key).ToDictionary(o => o.Key, p => p.Value);
            foreach (KeyValuePair<int, ErrorDataFormat> errorData in newErrorDataFormats)
            {
                if (chkAllowNull.Checked && errorData.Value == ErrorDataFormat.UnNull)
                {
                    continue;
                }
                count++;
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = errorData.Key / fpDataImported.ActiveSheet.Columns.Count + 1;
                dataRow[1] = errorData.Key % fpDataImported.ActiveSheet.Columns.Count + 1;
                dataRow[2] = UserEnumHelper.GetEnumText(errorData.Value);
                dataTable.Rows.Add(dataRow);
                if (count >= 200)
                {
                    break;
                }
            }
            if (errorDataFormats.Count > 0 && errorDataFormats.Count <= 200)
            {
                lblErrorTip.Text = string.Format("错误单元格数共有：{0}个。", errorDataFormats.Count);
            }
            else if (errorDataFormats.Count > 99)
            {
                lblErrorTip.Text = string.Format("错误数据单元格共有{0}个，仅显示其中200个错误数据单元格。", errorDataFormats.Count);
            }
            else
            {
                lblErrorTip.Text = "数据验证已完成，未发现错误数据单元格。";
            }
        }

        /// <summary>
        /// 更新错误结果表
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="dataImportedError"></param>
        private void UpdateErrorDataTable(int row, int col, ErrorDataFormat errorDataFormat)
        {
            DataTable dataTable = (DataTable)gridControl.DataSource;
            if (dataTable != null)
            {
                DataRow[] dataRows = dataTable.Select(string.Format("RowIndex = {0} AND ColumnIndex = {1}", row + 1, col + 1));
                if (errorDataFormat == ErrorDataFormat.None)
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
                        dataRow[0] = row + 1;
                        dataRow[1] = col + 1;
                        dataRow[2] = UserEnumHelper.GetEnumText(errorDataFormat);
                        dataTable.Rows.Add(dataRow);
                    }
                }
                gridControl.RefreshDataSource();
            }
        }


        /// <summary>
        /// 初始化进度条
        /// </summary>
        /// <param name="text"></param>
        /// <param name="tip"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        private void InitCommonProgress(string text, string tip, int minValue, int maxValue)
        {
            frmCommonProgress = new ProgressForm();
            frmCommonProgress.Text = text;
            frmCommonProgress.Tip = tip;
            frmCommonProgress.MinValue = minValue;
            frmCommonProgress.MaxValue = maxValue;
        }

        /// <summary>
        /// 获得字段的对应关系
        /// </summary>
        /// <returns></returns>
        private bool GetDataFieldRelation()
        {
            dataFieldRelation.Clear();
            DataTable dataTable = gcDataFieldRelation.DataSource as DataTable;
            foreach (DataRow dr in dataTable.Rows)
            {
                int columnIndex = DataConvertionHelper.GetInt(dr["SourceId"]);
                if (columnIndex > 0)
                {
                    decimal dataField = DataConvertionHelper.GetDecimal(dr["DestinationId"]);
                    if (dataField > 0)
                    {
                        CustomDataFieldInfo customDataFieldInfo = null;
                        if (!customDataFieldInfoDic.ContainsKey(dataField))
                        {
                            customDataFieldInfo = customDataFieldContract.GetModelInfo(dataField);
                            customDataFieldInfoDic.Add(dataField, customDataFieldInfo);
                        }
                        else
                        {
                            customDataFieldInfo = customDataFieldInfoDic[dataField];
                        }
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                        /* 1. 对于依赖枚举字段，显示为其依赖的枚举字段的枚举类型 */
                        if (DataFieldHelper.IsEnumDependency(physicalDataFieldType))
                        {
                            CustomDataFieldInfo newCustomDataFieldInfo = customDataFieldContract.GetModelInfo(customDataFieldInfo.ParentDataFieldId);
                            customDataFieldInfo.EnumId = newCustomDataFieldInfo.EnumId;
                        }
                        if (dataFieldRelation.ContainsKey(columnIndex))
                        {
                            return false;
                        }
                        dataFieldRelation.Add(columnIndex, dataField);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 创建字段对应关系的表结构
        /// </summary>
        /// <returns></returns>
        private DataTable CreateDataTable()
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("DestinationId", Type.GetType("System.Decimal"));
            dataTable.Columns.Add("DestinationName", Type.GetType("System.String"));
            dataTable.Columns.Add("RequiredDataField", Type.GetType("System.Boolean"));
            dataTable.Columns.Add("SourceId", Type.GetType("System.Int32"));
            dataTable.Columns.Add("Condition", Type.GetType("System.String"));

            return dataTable;
        }

        /// <summary>
        /// 加载源字段
        /// </summary>
        private void LoadSourceDataField()
        {
            CustomDataSource customDataSource = (CustomDataSource)Convert.ToByte(icmbDataSource.EditValue);
            if ((customDataSource == CustomDataSource.ExcelFile) && !File.Exists(openFileDialog.FileName))
            {
                return;
            }
            IList<CommonItem<int>> downListItems = new List<CommonItem<int>>(fpDataImported.ActiveSheet.ColumnCount - 1);
            if (chkAddtionalType.Checked || customDataSource == CustomDataSource.Remote)
            {
                for (int i = 1; i < fpDataImported.ActiveSheet.ColumnCount; i++)
                {
                    downListItems.Add(new CommonItem<int>(fpDataImported.ActiveSheet.ColumnHeader.Cells[0, i].Text, i));
                }
            }
            else
            {
                for (int i = 1; i < fpDataImported.ActiveSheet.ColumnCount; i++)
                {
                    downListItems.Add(new CommonItem<int>(string.Format("第{0}列", i), i));
                }
            }
            errorDataFormatDic = new Dictionary<int, Dictionary<int, ErrorDataFormat>>(fpDataImported.Sheets.Count);
            skippedRecrodDic = new Dictionary<int, IList<int>>(fpDataImported.Sheets.Count);
            RefreshSourceDataField(downListItems);
            dataFieldRelation.Clear();
        }

        /// <summary>
        /// 刷新源字段列表
        /// </summary>
        private void RefreshSourceDataField(IList<CommonItem<int>> downListItems)
        {
            DataTable dataTable = gcDataFieldRelation.DataSource as DataTable;
            ricmbSource.Items.Clear();
            ricmbSource.Items.Add(new ImageComboBoxItem(string.Empty, 0, -1)); /* 第一个选项为空 */
                        
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                string name = Convert.ToString(dataTable.Rows[i]["DestinationName"]);
                if (!string.IsNullOrWhiteSpace(name))
                {
                    int index = downListItems.FindIndex(node => node.Text.Equals(name));
                    if (index >= 0)
                    {
                        dataTable.Rows[i]["SourceId"] = downListItems[index].Value;
                    }
                    else
                    {
                        dataTable.Rows[i]["SourceId"] = 0;
                    }
                }
            }
            for (int i = 0; i < downListItems.Count; i++)
            {
                ImageComboBoxItem imageComboBoxItem = new ImageComboBoxItem(downListItems[i].Text, downListItems[i].Value, 0);
                ricmbSource.Items.Add(imageComboBoxItem);
            }
        }

        /// <summary>
        /// 修改数据单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sheetView_CellChanged(object sender, SheetViewEventArgs e)
        {
            if (!isCellChangedVaild)
            {
                return;
            }
            IDictionary<int, ErrorDataFormat> errorDataFormats = null;
            IList<int> skippedRecords = null;
            if (errorDataFormatDic.ContainsKey(fpDataImported.ActiveSheetIndex))
            {
                errorDataFormats = errorDataFormatDic[fpDataImported.ActiveSheetIndex];
                skippedRecords = skippedRecrodDic[fpDataImported.ActiveSheetIndex];
            }
            if (errorDataFormats != null)
            {
                int index = e.Row * fpDataImported.ActiveSheet.ColumnCount + e.Column;
                ErrorDataFormat errorDataFormat = ErrorDataFormat.None;
                if (e.Column > 0 && dataFieldRelation.Count > 0 && dataFieldRelation.Count < e.Column)
                {
                    errorDataFormat = ValidateCellData(fpDataImported.ActiveSheet.Cells[e.Row, e.Column], customDataFieldInfoDic[dataFieldRelation[e.Column]], fpDataImported.ActiveSheet.Cells[e.Row, e.Column].Value);
                    if (errorDataFormat != ErrorDataFormat.None)
                    {
                        if (errorDataFormats.ContainsKey(index))
                        {
                            errorDataFormats[index] = errorDataFormat;
                        }
                        else
                        {
                            errorDataFormats.Add(index, errorDataFormat);
                        }
                    }
                    else
                    {
                        if (errorDataFormats.ContainsKey(index))
                        {
                            errorDataFormats.Remove(index);
                        }
                    }
                }
                else
                {
                    ExportStyle exportStyle = (ExportStyle)Convert.ToByte(icmbImport.EditValue);
                    CommonNode commonNode = beTable.Tag as CommonNode;
                    errorDataFormat = ValidateKeyName(e.Row, exportStyle, commonNode.NodeId, skippedRecords);
                }
                SetErrorInfo(e.Row, e.Column, errorDataFormat);
                UpdateErrorDataTable(e.Row, e.Column, errorDataFormat);
                lblStatus.Text = string.Format("数据修正后，错误单元格数共有：{0}个。", errorDataFormats.Count);
            }
        }

        /// <summary>
        /// 设置错误信息
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="errorDataFormat"></param>
        private void SetErrorInfo(int row, int column, ErrorDataFormat errorDataFormat)
        {
            isCellChangedVaild = false;
            FarPoint.Win.Spread.Cell cell = fpDataImported.ActiveSheet.Cells[row, column];
            if (errorDataFormat == ErrorDataFormat.None)
            {
                if (cell.BackColor != Color.White)
                {
                    cell.BackColor = Color.White;
                }
            }
            else
            {
                if (cell.BackColor != Color.LightSteelBlue)
                {
                    cell.BackColor = Color.LightSteelBlue;
                }
            }
            isCellChangedVaild = true;
        }

        /// <summary>
        /// 验证作为关键字的第一列
        /// </summary>
        /// <param name="row"></param>
        /// <param name="exportStyle"></param>
        /// <param name="tableId"></param>
        /// <param name="skippedRecords"></param>
        /// <returns></returns>
        private ErrorDataFormat ValidateKeyName(int row, ExportStyle exportStyle, decimal tableId, IList<int> skippedRecords)
        {
            ErrorDataFormat errorDataFormat = ErrorDataFormat.None;

            if (row < 0)
            {
                throw new ArgumentException("行的值参数异常。");
            }
            CommonUserInfo commonUserInfo = null;
            if (fpDataImported.ActiveSheet.Cells[row, 0].Value != null)
            {
                string value = fpDataImported.ActiveSheet.Cells[row, 0].Value.ToString();
                ImportedKeyName importedKeyName = (ImportedKeyName)Convert.ToByte(icmbImportedKeyName.EditValue);
                if (importedKeyName == ImportedKeyName.UserName)
                {
                    lock (userDicitonarylocker)
                    {
                        if (!commonUserInfos.ContainsKey(value))
                        {
                            commonUserInfo = userAccountContract.GetCommonUserInfo(value);
                            if (commonUserInfo != null)
                            {
                                commonUserInfos.Add(value, commonUserInfo);
                            }
                        }
                        else
                        {
                            commonUserInfo = commonUserInfos[value];
                        }
                    }
                }
                else
                {
                    string userName = string.Empty;
                    lock (userDicitonarylocker)
                    {
                        if (!userIdentityAndNames.ContainsKey(value))
                        {
                            userName = userAccountContract.GetUserNameByUserIdentity(value);
                            userIdentityAndNames.Add(value, userName);
                        }
                        else
                        {
                            userName = userIdentityAndNames[value];
                        }
                        if (!commonUserInfos.ContainsKey(userName))
                        {
                            commonUserInfo = userAccountContract.GetCommonUserInfo(userName);
                            if (commonUserInfo != null)
                            {
                                commonUserInfos.Add(userName, commonUserInfo);
                            }
                        }
                        else
                        {
                            commonUserInfo = commonUserInfos[userName];                            
                        }
                    }
                }
                if (commonUserInfo == null && !chkNotExistedUser.Checked)
                {
                    errorDataFormat = ErrorDataFormat.NotExistedUser;
                }
            }
            else
            {
                errorDataFormat = ErrorDataFormat.NotExistedUser;
            }
            if (commonUserInfo != null && (exportStyle != ExportStyle.Append || dataTableType == DataTableType.PrimaryTable))
            {
                bool result = customTableContract.IsExistedRecord(commonUserInfo.UserId, tableId);
                if (result)
                {
                    lock (skippedRecordlocker)
                    {
                        skippedRecords.Add(row);
                    }
                }
                if ((dataTableType == DataTableType.PrimaryTable) && (exportStyle == ExportStyle.Append ||
                    exportStyle == ExportStyle.NotUpdateAndInsert || exportStyle == ExportStyle.UpdateAndInsert))
                {
                    if (result)
                    {
                        if (exportStyle == ExportStyle.Append)
                        {
                            errorDataFormat = ErrorDataFormat.ExistedData;
                        }                        
                    }
                    else
                    {
                        lock (userIdlocker)
                        {
                            if (!duplicateUserIds.Contains(commonUserInfo.UserId))
                            {
                                duplicateUserIds.Add(commonUserInfo.UserId);
                            }
                            else
                            {
                                errorDataFormat = ErrorDataFormat.DuplicateUser; ;
                            }
                        }
                    }
                }
            }

            return errorDataFormat;
        }

        /// <summary>
        /// 验证单元格数据
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private ErrorDataFormat ValidateCellData(FarPoint.Win.Spread.Cell cell, CustomDataFieldInfo customDataFieldInfo, object value)
        {
            ErrorDataFormat errorDataFormat = ErrorDataFormat.None;
            if (value != null && !string.IsNullOrWhiteSpace(value.ToString()))
            {
                string result = value.ToString().Trim();
                if (result.Length < value.ToString().Length)
                {
                    cell.Value = result;
                }
                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                //if (physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                //{
                //    BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(customDataFieldInfo.AssociatedDataFieldId);
                //    physicalDataFieldType = DataFieldHelper.GetPhysicalDataFieldType(basedDataType);
                //}
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                    case PhysicalDataFieldType.YearAndMonthAndDay:
                    case PhysicalDataFieldType.YearAndMonth:
                    case PhysicalDataFieldType.MonthAndDay:
                    case PhysicalDataFieldType.Time:
                        DateTime dateTime = DataConvertionHelper.GetConvertedDateTime(result);
                        if (DataConvertionHelper.IsNullValue(dateTime))
                        {
                            errorDataFormat = ErrorDataFormat.DateTimeError;
                        }
                        break;

                    case PhysicalDataFieldType.DepartmentDropdownListEnum:
                    case PhysicalDataFieldType.DepartmentTreeViewEnum:
                    case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                    case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                    case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                        if (customDepartmentInfos == null)
                        {
                            customDepartmentInfos = customDepartmentContract.GetCustomDepartmentInfos();
                        }
                        int index = -1;
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.DepartmentDropdownListEnum:
                            case PhysicalDataFieldType.DepartmentTreeViewEnum:
                            case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                                index = customDepartmentInfos.FindIndex(departmentInfo => departmentInfo.DepName.Equals(result));
                                if (index < 0)
                                {
                                    errorDataFormat = ErrorDataFormat.NotExistedDepName;
                                }
                                break;

                            case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                                index = customDepartmentInfos.FindIndex(departmentInfo => departmentInfo.FirstCode.Equals(result));
                                if (index < 0)
                                {
                                    errorDataFormat = ErrorDataFormat.NotExistedFirstDepCode;
                                }
                                break;

                            case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                                index = customDepartmentInfos.FindIndex(departmentInfo => departmentInfo.SecondCode.Equals(result));
                                if (index < 0)
                                {
                                    errorDataFormat = ErrorDataFormat.NotExistedSecondDepCode;
                                }
                                break;
                        }
                        break;

                    case PhysicalDataFieldType.Association:
                    case PhysicalDataFieldType.PrimaryAssociation:
                    case PhysicalDataFieldType.SecondaryAssociation:
                        DataTable dt = null;
                        lock (assocationDicitonarylocker)
                        {
                            if (assocationTable.ContainsKey(customDataFieldInfo.AssociatedDataFieldId))
                            {
                                dt = assocationTable[customDataFieldInfo.AssociatedDataFieldId];
                            }
                            else
                            {
                                dt = customAssociationContract.GetAssociationColumnData(customDataFieldInfo.AssociatedDataFieldId);
                                assocationTable.Add(customDataFieldInfo.AssociatedDataFieldId, dt);
                            }
                        }
                        BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(customDataFieldInfo.AssociatedDataFieldId);
                        DbType dbType = DataFieldHelper.GetDbType(basedDataType);
                        string columnName = dt.Columns[0].ColumnName;
                        DataRow[] row = null;
                        switch(dbType)
                        {
                            case DbType.String:
                            case DbType.DateTime:
                            case DbType.Boolean:
                                row = dt.Select(string.Format("{0} = '{1}'", columnName, result));
                                break;

                            case DbType.Int32:
                                if (DataConvertionHelper.IsInt32(result))
                                {
                                    row = dt.Select(string.Format("{0} = {1}", columnName, result));
                                }
                                break;

                            case DbType.Decimal:
                                if (DataConvertionHelper.IsDecimal(result))
                                {
                                    row = dt.Select(string.Format("{0} = {1}", columnName, result));
                                }
                                break;

                            default:
                                throw new ArgumentException("不支持该枚举类型。");
                        }
                        if (row == null || row.Length == 0)
                        {
                            errorDataFormat = ErrorDataFormat.AssocationError;
                        }                        
                        break;

                    case PhysicalDataFieldType.TreeViewEnum:
                    case PhysicalDataFieldType.TreeViewEnumValue:
                    case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                    case PhysicalDataFieldType.DropdownListEnum:
                    case PhysicalDataFieldType.DropdownListEnumValue:
                    case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                    case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                    case PhysicalDataFieldType.EnumNameDependency:
                    case PhysicalDataFieldType.EnumValue:
                    case PhysicalDataFieldType.FstAdditionalCode:
                    case PhysicalDataFieldType.ScdAdditionalCode:
                    case PhysicalDataFieldType.FstAdditionalString:
                    case PhysicalDataFieldType.ScdAdditionalString:
                    case PhysicalDataFieldType.TrdAdditionalString:
                    case PhysicalDataFieldType.FourthAdditionalString:
                    case PhysicalDataFieldType.FifthAdditionalString:
                    case PhysicalDataFieldType.SixthAdditionalString:
                    case PhysicalDataFieldType.FstAdditionalInteger:
                    case PhysicalDataFieldType.ScdAdditionalInteger:
                    case PhysicalDataFieldType.FstAdditionalDecimal:
                    case PhysicalDataFieldType.ScdAdditionalDecimal:
                        IList<CustomEnumInfo> enumItems = null;
                        lock (enumDicitonarylocker)
                        {
                            if (dicEnumItem.ContainsKey(customDataFieldInfo.EnumId))
                            {
                                enumItems = dicEnumItem[customDataFieldInfo.EnumId];
                            }
                            else
                            {
                                enumItems = customEnumContract.GetEnumItems(customDataFieldInfo.EnumId);
                                dicEnumItem.Add(customDataFieldInfo.EnumId, enumItems);
                            }
                        }
                        if (!ValidateEnumData(enumItems, physicalDataFieldType, result))
                        {
                            errorDataFormat = ErrorDataFormat.EnumError;
                        }
                        break;

                    case PhysicalDataFieldType.Boolean:
                        if (!result.Equals("0") && !result.Equals("1") && !result.ToLower().Equals("true") && !result.ToLower().Equals("false"))
                        {
                            errorDataFormat = ErrorDataFormat.BoolError;
                        }
                        break;

                    case PhysicalDataFieldType.Int32:
                        int integer = DataConvertionHelper.GetConvertedInt(result);
                        if (DataConvertionHelper.IsNullValue(integer))
                        {
                            errorDataFormat = ErrorDataFormat.IntError;
                        }
                        break;

                    case PhysicalDataFieldType.Decimal:
                        decimal decimalValue = DataConvertionHelper.GetConvertedDecimal(result);
                        if (DataConvertionHelper.IsNullValue(decimalValue))
                        {
                            errorDataFormat = ErrorDataFormat.DecimalNumberError;
                        }
                        break;

                    case PhysicalDataFieldType.ArbitraryString:
                    case PhysicalDataFieldType.NumeralString:
                    case PhysicalDataFieldType.CharString:
                    case PhysicalDataFieldType.MixedString:
                        if (result.Length > customDataFieldInfo.DataFieldLength)
                        {
                            errorDataFormat = ErrorDataFormat.StringLengthError;
                        }
                        if (errorDataFormat == ErrorDataFormat.None)
                        {
                            switch (physicalDataFieldType)
                            {
                                case PhysicalDataFieldType.NumeralString:
                                    Regex regexInt32 = new Regex(@"^-?\d+$");
                                    if (!regexInt32.IsMatch(result))
                                    {
                                        errorDataFormat = ErrorDataFormat.NumeralStringError;
                                    }
                                    break;

                                case PhysicalDataFieldType.CharString:
                                    Regex charRegex = new Regex(@"^[A-Za-z]+$");
                                    if (!charRegex.IsMatch(result))
                                    {
                                        errorDataFormat = ErrorDataFormat.CharStringError;
                                    }
                                    break;

                                case PhysicalDataFieldType.MixedString:
                                    Regex mixedRegex = new Regex(@"^[A-Za-z0-9]+$");
                                    if (!mixedRegex.IsMatch(result))
                                    {
                                        errorDataFormat = ErrorDataFormat.MixedStringError;
                                    }
                                    break;
                            }
                        }
                        break;

                    case PhysicalDataFieldType.DocAttachment:
                    case PhysicalDataFieldType.PicAttachment:
                        if (result.Length > 128)
                        {
                            errorDataFormat = ErrorDataFormat.StringLengthError;
                        }
                        break;
                }
            }
            else
            {
                if (customDataFieldInfo.RequiredDataField && !chkAllowNull.Checked)
                {
                    errorDataFormat = ErrorDataFormat.UnNull;
                }
            }

            return errorDataFormat;
        }

        /// <summary>
        /// 验证枚举数据
        /// </summary>
        /// <param name="enumItems"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private bool ValidateEnumData(IList<CustomEnumInfo> enumItems, PhysicalDataFieldType physicalDataFieldType, string value)
        {
            bool correct = false;

            switch (physicalDataFieldType)
            {
                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.EnumNameDependency:
                case PhysicalDataFieldType.DropdownListEnum:
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.EnumName.Equals(value))
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.TreeViewEnumValue:
                case PhysicalDataFieldType.DropdownListEnumValue:
                case PhysicalDataFieldType.EnumValue:
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.EnumValue.Equals(value))
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                case PhysicalDataFieldType.FstAdditionalCode:
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.FirstCode.Equals(value))
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                case PhysicalDataFieldType.ScdAdditionalCode:
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.SecondCode.Equals(value))
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.FstAdditionalString:
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.FstAdditionalString.Equals(value))
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.ScdAdditionalString:
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.ScdAdditionalString.Equals(value))
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.TrdAdditionalString:
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.TrdAdditionalString.Equals(value))
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.FourthAdditionalString:
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.FourthAdditionalString.Equals(value))
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.FifthAdditionalString:
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.FifthAdditionalString.Equals(value))
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.SixthAdditionalString:
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.SixthAdditionalString.Equals(value))
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.FstAdditionalInteger:
                    int fstAdditionalInteger = DataConvertionHelper.GetConvertedInt(value);
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.FstAdditionalInteger == fstAdditionalInteger)
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.ScdAdditionalInteger:
                    int scdAdditionalInteger = DataConvertionHelper.GetConvertedInt(value);
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.ScdAdditionalInteger == scdAdditionalInteger)
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.FstAdditionalDecimal:
                    decimal fstAdditionalDecimal = DataConvertionHelper.GetConvertedDecimal(value);
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.FstAdditionalDecimal == fstAdditionalDecimal)
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;

                case PhysicalDataFieldType.ScdAdditionalDecimal:
                    decimal scdAdditionalDecimal = DataConvertionHelper.GetConvertedDecimal(value);
                    foreach (CustomEnumInfo enumItem in enumItems)
                    {
                        if (enumItem.ScdAdditionalDecimal == scdAdditionalDecimal)
                        {
                            correct = true;
                            break;
                        }
                    }
                    break;
            }

            return correct;
        }

        /// <summary>
        /// 刷新目的字段列表
        /// </summary>
        /// <param name="commonNodes"></param>
        private void RefreshDestinationDataField(IList<CommonNode> commonNodes)
        {
            DataTable dataTable = gcDataFieldRelation.DataSource as DataTable;
            if (dataTable.Rows.Count < commonNodes.Count)
            {
                int count = commonNodes.Count - dataTable.Rows.Count;
                for (int i = 0; i < count; i++)
                {
                    DataRow dataRow = dataTable.NewRow();
                    dataTable.Rows.Add(dataRow);
                }
            }
            else
            {
                int count = dataTable.Rows.Count - commonNodes.Count;
                for (int i = 0; i < count; i++)
                {
                    dataTable.Rows.RemoveAt(commonNodes.Count - 1);
                }
            }
            for (int i = 0; i < commonNodes.Count; i++)
            {
                dataTable.Rows[i]["DestinationId"] = commonNodes[i].NodeId;
                dataTable.Rows[i]["DestinationName"] = commonNodes[i].NodeName;
                dataTable.Rows[i]["RequiredDataField"] = commonNodes[i].IsLeaf;
            }
            LoadSourceDataField();
        }

        /// <summary>
        /// 更新错误数据格式内容
        /// </summary>
        /// <param name="rows"></param>
        private void UpdateErrorDataFormats(List<int> rows)
        {
            if (errorDataFormatDic.ContainsKey(fpDataImported.ActiveSheetIndex))
            {
                Dictionary<int, ErrorDataFormat> dataFormats = errorDataFormatDic[fpDataImported.ActiveSheetIndex];
                List<int> keys = new List<int>();
                foreach (KeyValuePair<int, ErrorDataFormat> keyValue in dataFormats)
                {
                    int rowIndex = keyValue.Key / fpDataImported.ActiveSheet.ColumnCount;
                    if (rows.Contains(rowIndex))
                    {
                        keys.Add(keyValue.Key);
                    }
                }
                foreach (int key in keys)
                {
                    dataFormats.Remove(key);
                }
                Dictionary<int, int> dicRows = new Dictionary<int, int>();
                foreach (KeyValuePair<int, ErrorDataFormat> keyValue in dataFormats)
                {
                    int count = 0;
                    int rowIndex = keyValue.Key / fpDataImported.ActiveSheet.ColumnCount;
                    foreach (var row in rows)
                    {
                        if (rowIndex > row)
                        {
                            count++;
                        }
                    }
                    if (count > 0)
                    {
                        int key = keyValue.Key - count * fpDataImported.ActiveSheet.ColumnCount;
                        dicRows.Add(keyValue.Key, key);
                    }
                }
                Dictionary<int, ErrorDataFormat> newDataFormats = new Dictionary<int, ErrorDataFormat>();
                foreach (KeyValuePair<int, int> keyValue in dicRows)
                {
                    ErrorDataFormat errorDataFormat = dataFormats[keyValue.Key];
                    dataFormats.Remove(keyValue.Key);
                    newDataFormats.Add(keyValue.Value, errorDataFormat);
                }
                foreach (KeyValuePair<int, ErrorDataFormat> keyValue in newDataFormats)
                {
                    if (dataFormats.ContainsKey(keyValue.Key))
                    {
                        dataFormats[keyValue.Key] = keyValue.Value;
                    }
                    else
                    {
                        dataFormats.Add(keyValue.Key, keyValue.Value);
                    }
                }
                ShowErrorDataTable(dataFormats);
                lblStatus.Text = string.Format("数据修正后，错误单元格数共有：{0}个。", dataFormats.Count);
            }
        }

        #endregion

    }
}
