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
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WCFContracts;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.SystemModule;
using Blue.Model.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using Blue.WindowsFormsClient.Common;
using Blue.WinBusinessLogic;
using Blue.WindowsFormsClient;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class DataConvertionForm : Form
    { 
        #region  私有常量      

        /* 线程组个数 */
        private const int MAX_THREAD_COUNT = 1;

        /* 文本框最大字符数 */
        private const int MAX_LENGTH_ON_CELL = 8000;

        /* 文本框最小字符数时启用文本类型 */
        private const int MIN_LENGTH_ON_CELL = 128;

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

        #endregion

        #region  私有变量

        private IList<CommonNode> sheetCommonNodes = null;
        private FpSpread printFpSpread = null;
        private FpSpread currentFpSpread = null;

        #endregion

        #region 契约接口
        
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ICustomGroupContract customGroupContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly IUserAccountContract userAccountContract;
        private readonly ICustomSheetContract customSheetContract;
        private readonly ICustomReportContract customReportContract;
        private readonly ICustomCellContract customCellContract;
        private readonly ICustomCellContract[] customCellContracts;
        private readonly ICustomSnapshotContract customSnapshotContract;
        private readonly ICustomDepartmentContract customDepartmentContract;


        #endregion

        #region 属性

        /// <summary>
        /// 报表编号
        /// </summary>
        public decimal ReportId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public DataConvertionForm()
        {
            InitializeComponent();
            printFpSpread = new FpSpread();
            currentFpSpread = new FpSpread();
            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
            customSheetContract = BusinessDesignerChannelFactory.CreateCustomSheetContract();
            customCellContract = BusinessDesignerChannelFactory.CreateCustomCellContract();
            customCellContracts = new ICustomCellContract[MAX_THREAD_COUNT];
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
        }

        #endregion

        #region 窗体和控件方法

        private void DataConvertionForm_Load(object sender, EventArgs e)
        {
            sheetCommonNodes = customSheetContract.GetChildNodes(ReportId);
            foreach (var sheetCommonNode in sheetCommonNodes)
            {
                CheckedListBoxItem item = new CheckedListBoxItem(sheetCommonNode.NodeName, true);
                chkSheetNames.Properties.Items.Add(item);
            }          
        }

        #endregion

        /// <summary>
        /// 加载用户列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnLoad_Click(object sender, EventArgs e)
        {
            openFileDialog.InitialDirectory = Application.StartupPath;
            openFileDialog.Filter = AppSettingHelper.DefaultExcelFormat;
            if (openFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            if (string.IsNullOrWhiteSpace(openFileDialog.FileName))
            {
                MessageBox.Show("没有选择Excel文件！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!File.Exists(openFileDialog.FileName))
            {
                MessageBox.Show(string.Format("{0}文件不存在，请检查！", Path.GetFileName(openFileDialog.FileName)),
                    "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                fpUserName.OpenExcel(openFileDialog.FileName, FarPoint.Excel.ExcelOpenFlags.TruncateEmptyRowsAndColumns);
                fpUserName.TabStripInsertTab = false;
                fpUserName.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Always;
                Cursor = Cursors.Default;
            }
            catch
            {
                Cursor = Cursors.Default;
                MessageBox.Show("出错可能原因：（1）已打开该Excel文件（2）该文件并不存在（3）该文件内容与格式错误", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 保存基本报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnSave_Click(object sender, EventArgs e)
        {
            folderBrowserDialog.RootFolder = Environment.SpecialFolder.Desktop;
            //允许在对话框中包括一个新建目录的按钮
            folderBrowserDialog.ShowNewFolderButton = true;
            // 设置对话框的说明信息
            folderBrowserDialog.Description = "请选择输出目录";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                
                byte[] data = customSheetContract.DownloadReportFile(ReportId);
                string reportName = customSheetContract.GetNodeNameByNodeId(ReportId);
                Cursor = Cursors.WaitCursor;
                if (data == null || data.Length == 0)
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("该报表还未设计，请先设计！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                string dirName = "Input";
                string dir = string.Format(@"{0}\{1}", folderBrowserDialog.SelectedPath, dirName);
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                ProgressForm frmProgress = new ProgressForm();
                frmProgress.MinValue = 0;
                frmProgress.TopMost = true;
                frmProgress.MaxValue = fpUserName.ActiveSheet.RowCount;
                frmProgress.Show();
                try
                {
                    int count = 0;
                    using (FarPoint.Win.Spread.FpSpread fpSpread = new FarPoint.Win.Spread.FpSpread())
                    {
                        for (int rowIndex = 0; rowIndex < fpUserName.ActiveSheet.RowCount; rowIndex++)
                        {
                            CommonUserInfo commonUserInfo = null;
                            string userName = fpUserName.ActiveSheet.Cells[rowIndex, 0].Text.Trim();
                            string subDirName = string.Empty;
                            if (fpUserName.ActiveSheet.ColumnCount > 1)
                            {
                                subDirName = fpUserName.ActiveSheet.Cells[rowIndex, 1].Text.Trim();
                            }
                            if (!string.IsNullOrWhiteSpace(userName))
                            {
                                commonUserInfo = userAccountContract.GetCommonUserInfo(userName);
                            }
                            if (commonUserInfo == null)
                            {
                                MessageBox.Show(string.Format("第{0}行的用户{1}不存在，导出中断！", rowIndex, userName),
                                    "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                break;
                            }
                            frmProgress.Text = string.Format("正在导出第{0}个用户...", rowIndex + 1);
                            Application.DoEvents();
                            frmProgress.IncreaseStep();
                            if (frmProgress != null && frmProgress.Cancel)
                            {
                                frmProgress.CloseFrom();
                                return;
                            }

                            string subDir = string.Empty;
                            if (!string.IsNullOrWhiteSpace(subDirName))
                            {
                                subDir = string.Format(@"{0}\{1}\{2}", folderBrowserDialog.SelectedPath, dirName, subDirName);
                            }
                            else
                            {
                                subDir = string.Format(@"{0}\{1}", folderBrowserDialog.SelectedPath, dirName);
                            }
                            if (!Directory.Exists(subDir))
                            {
                                Directory.CreateDirectory(subDir);
                            }
                            string subDirPath = string.Format(@"{0}\{1}_{2}_{3}_{4}.xls", subDir, commonUserInfo.UserActualName, commonUserInfo.UserName,
                                commonUserInfo.DepName, reportName);
                            SaveReportingFile(fpSpread, ReportId, data, commonUserInfo.UserId, subDirPath);
                            fpSpread.Reset();
                            Application.DoEvents();
                            count++;
                        }
                        frmProgress.CloseFrom();
                        Cursor = Cursors.Default;
                        MessageBox.Show(string.Format("共有{0}个保存成功，保存目录为{1}。", count, dir), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch
                {
                    if (frmProgress != null && !frmProgress.IsDisposed)
                    {
                        frmProgress.CloseFrom();
                    }
                    Cursor = Cursors.Default;
                }
            }
        }

        /// <summary>
        /// 保存基本报表
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="reportId"></param>
        /// <param name="data"></param>
        /// <param name="userId"></param>
        /// <param name="dir"></param>
        private void SaveReportingFile(FpSpread fsReporting, decimal reportId, byte[] data, decimal userId, string dir)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (data != null && data.Length > 0)
                {
                    IList<RowAndCol> rowAndCols = customSheetContract.GetRowAndColCount(reportId);
                    ReportHelper.ShowFpSpread(fsReporting, data, rowAndCols);
                    LoadBasicReporting(fsReporting, reportId, userId);
                    fsReporting.SaveExcel(dir, FarPoint.Excel.ExcelSaveFlags.SaveAsViewed | FarPoint.Excel.ExcelSaveFlags.SaveAsFiltered);
                    Cursor = Cursors.Default;
                }
                else
                {
                    throw new ArgumentException("该报表还未设计，请先设计。");
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }


        /// <summary>
        /// 显示基本报表
        /// </summary>
        /// <param name="userId"></param>
        private void LoadBasicReporting(FpSpread fsReporting, decimal reportId, decimal userId)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IList<CommonNode> commonNodes = customSheetContract.GetChildNodes(reportId);
                Dictionary<decimal, CustomDataFieldInfo> customDataFieldInfoDic = new Dictionary<decimal, CustomDataFieldInfo>();
                Dictionary<decimal, string> dicTablePhysicalNames = new Dictionary<decimal, string>();
                int count = (fsReporting.Sheets.Count <= commonNodes.Count) ? fsReporting.Sheets.Count : commonNodes.Count;
                for (int sheetIndex = 0; sheetIndex < count; sheetIndex++)
                {
                    fsReporting.Sheets[sheetIndex].Tag = commonNodes[sheetIndex];
                    IList<CustomCellInfo> customCellInfos = customCellContract.GetModelInfos(commonNodes[sheetIndex].NodeId);
                    if (customCellInfos.Count == 0) continue;
                    for (int cellIndex = 0; cellIndex < customCellInfos.Count; cellIndex++)
                    {
                        CustomCellInfo customCellInfo = customCellInfos[cellIndex];
                        BasicCellType basicCellType = (BasicCellType)customCellInfo.CellType;
                        if (customCellInfo.TableId <= 0 && basicCellType != BasicCellType.Photo)
                        {
                            continue;
                        }                        
                        switch (basicCellType)
                        {
                            case BasicCellType.Photo:
                                CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(userId);
                                if (commonUserInfo != null)
                                {
                                    byte[] data = userAccountContract.DownLoadPhoto(commonUserInfo.UserName);
                                    if (data != null && data.Length > 0)
                                    {
                                        ReportHelper.InsertPhoto(fsReporting, fsReporting.ActiveSheet.ActiveCell, data);
                                    }
                                }
                                break;

                            case BasicCellType.OnlyData:
                                string cellText = customCellContract.GetCellText(customCellInfo.CellId, userId, customCellInfo.TableId,
                                    customCellInfo.ConditionText, customCellInfo.TemplateText);
                                if (!string.IsNullOrWhiteSpace(cellText) && cellText.Length > MIN_LENGTH_ON_CELL)
                                {
                                    TextCellType textCellType = new TextCellType();
                                    textCellType.WordWrap = true;
                                    textCellType.MaxLength = MAX_LENGTH_ON_CELL;
                                    fsReporting.Sheets[sheetIndex].Cells[customCellInfo.RowIndex, customCellInfo.ColIndex].CellType = textCellType;
                                }
                                if (customCellInfo != null && sheetIndex < fsReporting.Sheets.Count
                                    && customCellInfo.RowIndex < fsReporting.Sheets[sheetIndex].RowCount
                                    && customCellInfo.ColIndex < fsReporting.Sheets[sheetIndex].ColumnCount)
                                {
                                    try
                                    {
                                        fsReporting.Sheets[sheetIndex].Cells[customCellInfo.RowIndex, customCellInfo.ColIndex].Text = cellText;
                                    }
                                    catch (Exception exception)
                                    {
                                        //记录日志, 抛出异常, 不包装异常
                                        ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                                    }
                                }
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
                                DataSet ds = customCellContract.GetExtendRowColData(customCellInfo.CellId, id, customCellInfo.TableId, customCellInfo.ConditionText);
                                if (ds == null)
                                {
                                    continue;
                                }
                                int currentSpanCellCount = 0;
                                IList<CellStyleInfo> cellStyleInfos = customCellContract.GetModelInfosByCellId(customCellInfo.CellId, CellCondition.Show);
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

                                        string text = GetCellText(customDataFieldInfo, result);
                                        if (customCellInfo != null && sheetIndex < fsReporting.Sheets.Count 
                                            && row < fsReporting.Sheets[sheetIndex].RowCount 
                                            && customCellInfo.ColIndex < fsReporting.Sheets[sheetIndex].ColumnCount)
                                        {
                                            try
                                            {
                                                fsReporting.Sheets[sheetIndex].Cells[row, col].Text = text;
                                            }
                                            catch (Exception exception)
                                            {
                                                //记录日志, 抛出异常, 不包装异常
                                                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
                                            }
                                        }
                                        if (index == 0)
                                        {
                                            /* 计算合并的单元格的行列 */
                                            CellRange cr = fsReporting.Sheets[sheetIndex].GetSpanCell(row, col);
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
                }
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
        private string GetCellText(CustomDataFieldInfo customDataFieldInfo, object value)
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
        /// 设置单元格属性
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="value"></param>
        private void SetCellProperty(FpSpread fsReporting, FarPoint.Win.Spread.Cell cell, CustomDataFieldInfo customDataFieldInfo)
        {
            if (cell.Tag != null)
            {
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.SystemPhysicalDataField:
                        SystemDataField systemDataField = (SystemDataField)customDataFieldInfo.DataFieldId;                        
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                        switch (physicalDataFieldType)
                        {

                            //case PhysicalDataFieldType.ArbitraryString:
                            //    if (cellDataFieldInfo.PhysicalName.Equals(reviewedConclusion))
                            //    {
                            //        RichTextCellType rtf = new RichTextCellType();
                            //        rtf.WordWrap = true;
                            //        rtf.Multiline = true;
                            //        cell.CellType = rtf;
                            //    }
                            //    break;

                            case PhysicalDataFieldType.Boolean:
                                cell.CellType = new CheckBoxCellType();
                                break;

                            case PhysicalDataFieldType.Int32:
                                NumberCellType numberCellType = new NumberCellType();
                                numberCellType.DecimalPlaces = 0;
                                cell.CellType = numberCellType;
                                break;

                            case PhysicalDataFieldType.Decimal:
                                NumberCellType decimalNumberCellType = new NumberCellType();
                                decimalNumberCellType.DecimalPlaces = customDataFieldInfo.DataFieldLength;
                                cell.CellType = decimalNumberCellType;
                                break;

                            case PhysicalDataFieldType.NumeralString:
                                RegularExpressionCellType numeralString = new RegularExpressionCellType();
                                numeralString.RegularExpression = @"^-?\d+$";
                                cell.CellType = numeralString;
                                break;

                            case PhysicalDataFieldType.CharString:
                                RegularExpressionCellType charString = new RegularExpressionCellType();
                                charString.RegularExpression = @"^[A-Za-z]+$";
                                cell.CellType = charString;
                                break;

                            case PhysicalDataFieldType.MixedString:
                                RegularExpressionCellType mixedString = new RegularExpressionCellType();
                                mixedString.RegularExpression = @"^[A-Za-z0-9]+$";
                                cell.CellType = mixedString;
                                break;

                            case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                                DateTimeCellType yearAndMonthAndDayAndTime = new DateTimeCellType();
                                yearAndMonthAndDayAndTime.DateTimeFormat = DateTimeFormat.LongDateWithTime;
                                cell.CellType = yearAndMonthAndDayAndTime;
                                break;

                            case PhysicalDataFieldType.YearAndMonthAndDay:
                                DateTimeCellType yearAndMonthAndDay = new DateTimeCellType();
                                yearAndMonthAndDay.DateTimeFormat = DateTimeFormat.UserDefined;
                                yearAndMonthAndDay.UserDefinedFormat = "yyyy-MM-dd";
                                cell.CellType = yearAndMonthAndDay;
                                break;

                            case PhysicalDataFieldType.YearAndMonth:
                                DateTimeCellType yearAndMonth = new DateTimeCellType();
                                yearAndMonth.DateTimeFormat = DateTimeFormat.UserDefined;
                                yearAndMonth.UserDefinedFormat = "yyyy-MM";
                                cell.CellType = yearAndMonth;
                                break;

                            case PhysicalDataFieldType.MonthAndDay:
                                DateTimeCellType monthAndDay = new DateTimeCellType();
                                monthAndDay.DateTimeFormat = DateTimeFormat.UserDefined;
                                monthAndDay.UserDefinedFormat = "MM-dd";
                                cell.CellType = monthAndDay;
                                break;

                            case PhysicalDataFieldType.Time:
                                DateTimeCellType time = new DateTimeCellType();
                                time.DateTimeFormat = DateTimeFormat.TimeOnly;
                                cell.CellType = time;
                                break;

                            default:
                                cell.CellType = new TextCellType();
                                break;
                        }
                        break;

                    case DataFieldProperty.LogicalDataField:
                        cell.CellType = new TextCellType();
                        LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)customDataFieldInfo.DataFieldType;
                        switch (logicalDataFieldType)
                        {
                            //case LogicalDataFieldType.DigitExpression:
                            //    cell.CellType = new NumberCellType();
                            //    break;

                            case LogicalDataFieldType.DateTimeExpression:
                                DateTimeCellType yearAndMonthAndDay = new DateTimeCellType();
                                yearAndMonthAndDay.DateTimeFormat = DateTimeFormat.UserDefined;
                                yearAndMonthAndDay.UserDefinedFormat = "yyyy-MM-dd";
                                cell.CellType = yearAndMonthAndDay;
                                break;

                            case LogicalDataFieldType.StringExpression:
                                cell.CellType = new TextCellType();
                                break;
                        }
                        break;
                }
                //if (dataFieldProperty == DataFieldProperty.PhysicalDataField
                //    && (PhysicalDataFieldType)customDataFieldInfo.DataFieldType == PhysicalDataFieldType.PicAttachment)
                //{
                //    if (value != null)
                //    {
                //        string picName = value.ToString();
                //        byte[] data = authorizedDataContract.DownLoadPhoto(string.Format("{0}.JPG", picName));
                //        if (data != null && cell != null)
                //        {
                //            ReportHelper.InsertPhoto(fsReporting, cell, data);
                //        }
                //    }
                //    else
                //    {
                //        cell.Value = null;
                //    }
                //}
                //else
                //{
                //    cell.Value = value;
                //}
            }
        }

        /// <summary>
        /// 选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpreesComoboxTreeview_AfterTreeNodeExpand(object sender, TreeViewEventArgs e)
        {

        }
        /// <summary>
        /// 打印基本报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnPrint_Click(object sender, EventArgs e)
        {
            string reportName = customSheetContract.GetNodeNameByNodeId(ReportId);
            if (MessageBox.Show(string.Format("确认批量打印{0}?", reportName),
                "提示信息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation,
                            MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            try
            {
                
                byte[] data = customSheetContract.DownloadReportFile(ReportId);
                if (data != null && data.Length > 0)
                {
                    IList<RowAndCol> rowAndCols = customSheetContract.GetRowAndColCount(ReportId);
                    IList<CustomMargin> customMargins = new List<CustomMargin>();
                    foreach (CommonNode commonNode in sheetCommonNodes)
                    {
                        CustomMargin customMargin = customSheetContract.GetMargin(commonNode.NodeId);
                        if (customMargin != null)
                        {
                            customMargins.Add(customMargin);
                        }
                    }
                    ProgressForm frmProgress = new ProgressForm();
                    frmProgress.MinValue = 0;
                    frmProgress.TopMost = true;
                    frmProgress.MaxValue = fpUserName.ActiveSheet.RowCount;
                    frmProgress.Show();
                    bool exit = false;
                    try
                    {
                        printFpSpread.Sheets.Clear();
                        for (int rowIndex = 0; rowIndex < fpUserName.ActiveSheet.RowCount; rowIndex++)
                        {
                            decimal userId = 0;
                            string userName = fpUserName.ActiveSheet.Cells[rowIndex, 0].Text.Trim();
                            if (!string.IsNullOrWhiteSpace(userName))
                            {
                                userId = userAccountContract.GetUserIdByUserName(userName);
                            }
                            if (userId <= 0)
                            {
                                MessageBox.Show(string.Format("第{0}行的用户{1}不存在，加载数据中断。", rowIndex, userName),
                                    "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                exit = true;
                                break;
                            }
                            frmProgress.Text = string.Format("正在加载第{0}个用户的数据...", rowIndex + 1);
                            Application.DoEvents();
                            frmProgress.IncreaseStep();
                            if (frmProgress != null && frmProgress.Cancel)
                            {
                                frmProgress.CloseFrom();
                                return;
                            }
                            currentFpSpread.Sheets.Clear();
                            ReportHelper.ShowFpSpread(currentFpSpread, data, rowAndCols);
                            LoadBasicReporting(currentFpSpread, ReportId, userId);
                            for (int idx = 0; idx < chkSheetNames.Properties.Items.Count; idx++)
                            {
                                CheckedListBoxItem item = chkSheetNames.Properties.Items[idx];
                                if (item.CheckState == CheckState.Checked)
                                {
                                    
                                    SheetView sheeView = currentFpSpread.Sheets[idx];
                                    sheeView.SheetName = string.Format("{0}_{1}", userName, idx);
                                    sheeView.Cells[0, 0, sheeView.RowCount - 1, sheeView.ColumnCount - 1].BackColor = Color.White;                                    
                                    printFpSpread.Sheets.Add(sheeView);
                                    PrintInfo printInfo = ReportHelper.GetPrintInfo(customMargins[idx]);
                                    printFpSpread.Sheets[printFpSpread.Sheets.Count-1].PrintInfo = printInfo;
                                    if (frmProgress != null && frmProgress.Cancel)
                                    {
                                        frmProgress.CloseFrom();
                                        return;
                                    }
                                    Application.DoEvents();
                                }
                            }
                        }
                        frmProgress.Text = "打印数据加载完成，打印机即将开始打印...";
                        Application.DoEvents();
                        if (printFpSpread.Sheets.Count > 0)
                        {
                            printFpSpread.PrintSheet(-1);
                        }
                        System.Threading.Thread.Sleep(3000);
                        if (frmProgress != null && !frmProgress.IsDisposed)
                        {
                            frmProgress.CloseFrom();
                        }
                        Cursor = Cursors.Default;
                        if (!exit)
                        {
                            MessageBox.Show("打印数据加载完成，打印机即将开始打印。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    catch (Exception exception)
                    {
                        if (frmProgress != null && !frmProgress.IsDisposed)
                        {
                            frmProgress.CloseFrom();
                        }
                        throw exception;
                    }
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show("该报表还未设计，请先设计！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                Cursor = Cursors.Default;
                MessageBox.Show(ex.Message);
            }
        }
    }
}
