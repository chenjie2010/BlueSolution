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
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.Model.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.UserModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class UserToolForm : Form
    {
        #region  私有静态只读变量

        /// <summary>
        /// 加锁
        /// </summary>
        private static readonly object locker = new object();

        #endregion

        #region  私有常量

        /* 批量用户导入时，Excel文件包含八列 */
        private const int USERS_IMPORTED_COLUMNS = 8;

        /* 批量用户删除时，Excel文件包含一列，即第一列为用户名 */
        private const int USERS_DELETED_COLUMNS = 1;

        /* 其它用户信息操作时，Excel文件要求第一列用户名，第二列为对应的值； */
        private const int USERS_OPERATION_COLUMNS = 2;

        /* 线程组个数 */
        private const int MAX_THREAD_COUNT = 1;

        #endregion

        #region  私有变量        

        /// <summary>
        /// Excel 单元格内容变化是否生效
        /// </summary>
        private bool isCellChangedValild = false;
        
        /// <summary>
        /// 待导入用户
        /// </summary>
        private IList<string> userNamesImported = null;
        
        /// <summary>
        /// 错误的数据单元格：行列索引，错误的原因
        /// </summary>
        private Dictionary<int, UserToolError> errorCells = null;

        /// <summary>
        /// 导入失败的行索引
        /// </summary>
        private IList<int> dataRowsFailed = null; 

        /// <summary>
        /// 新用户名
        /// </summary>
        private IList<string> newUserNames = null;

        /// <summary>
        /// 校验是否完成
        /// </summary>
        private bool validationCompleted = false;

        /// <summary>
        /// 单位
        /// </summary>
        private Dictionary<string, decimal> departmentNameAndIds = null;

        /// <summary>
        /// 用户类型
        /// </summary>
        private Dictionary<string, decimal> userTypeNameAndIds = null;

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

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public UserToolForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customDepartmentContract = SystemChannelFactory.CreateCustomDepartmentContract();
            userTypeContract = SystemChannelFactory.CreateUserTypeContract();
            errorCells = new Dictionary<int, UserToolError>();
            userNamesImported = new List<string>();
            newUserNames = new List<string>();
            dataRowsFailed = new List<int>();            
            InitErrorDataTable();            
        }

        #endregion        

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserToolForm_Load(object sender, EventArgs e)
        {
            gcMain.Tag = UserToolAction.UsersImported;
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
                fpUserTool.OpenExcel(openFileDialog.FileName, FarPoint.Excel.ExcelOpenFlags.TruncateEmptyRowsAndColumns);
                if (fpUserTool.Sheets[0].RowCount > 0 && fpUserTool.Sheets[0].ColumnCount > 0)
                {
                    fpUserTool.Sheets[0].Cells[0, 0, 0, fpUserTool.Sheets[0].ColumnCount - 1].BackColor = Color.LightGray;                    
                }
                fpUserTool.TabStripInsertTab = false;
                fpUserTool.TabStripPolicy = FarPoint.Win.Spread.TabStripPolicy.Never;
                fpUserTool.ActiveSheetIndex = 0;
                SpreadToolHelper.RemoveEmptyRows(fpUserTool);
                foreach (SheetView sheetView in fpUserTool.Sheets)
                {
                    sheetView.CellChanged += new SheetViewEventHandler(sheetView_CellChanged);
                }
                lblToolInfo.Text = "等待数据校验。";
                isCellChangedValild = true;
                validationCompleted = false;
                ClearCahcheResources();
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
                SpreadToolHelper.SetCellTypeOnCells(fpUserTool, cellType);                
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
                if (gcMain.Tag != null)
                {
                    UserToolAction userToolAction = (UserToolAction)gcMain.Tag;
                    switch (userToolAction)
                    {
                        case UserToolAction.UsersImported:
                            if (fpUserTool.Sheets[0].ColumnCount != USERS_IMPORTED_COLUMNS)
                            {
                                MessageBox.Show("批量用户导入时，Excel文件包含七列，（用户名，密码(明文即可)，真实姓名，用户类型名称，单位名称，身份证号，手机号码，电子邮件），前六列均不能为空。",
                                    "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;

                        case UserToolAction.UserDeleted:
                            if (fpUserTool.Sheets[0].ColumnCount != USERS_DELETED_COLUMNS)
                            {
                                MessageBox.Show("批量用户删除时，Excel文件包含一列，即第一列为用户名。", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;

                        default:
                            if (fpUserTool.Sheets[0].ColumnCount != USERS_OPERATION_COLUMNS)
                            {
                                MessageBox.Show("Excel文件只能包含两列，第一列为用户名，第二列为对应的值。", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            break;

                    }
                    if (fpUserTool.Sheets[0].RowCount <= 1)
                    {
                        MessageBox.Show("Excel文件必须包含导入的数据行(第一行默认为标题行)。", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    if (MessageBox.Show("确认进行数据校验？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                    {
                        return;
                    }

                    if (departmentNameAndIds == null)
                    {
                        departmentNameAndIds = customDepartmentContract.GetNameAndIds();
                        Application.DoEvents();
                    }
                    if (userTypeNameAndIds == null)
                    {
                        userTypeNameAndIds = userTypeContract.GetNameAndUserTypeIds();
                        Application.DoEvents();
                    }
                    Cursor = Cursors.WaitCursor;
                    validationCompleted = false;
                    InitCommonProgress("正在校验中，请稍后...", 0, fpUserTool.Sheets[0].RowCount - 1);
                    lblToolInfo.Text = "正在进行数据校验...。";
                    /* 数据校验 */
                    ClearCahcheResources();                    
                    InitThreadResrouces();
                    int threadCount = fpUserTool.Sheets[0].RowCount < MAX_THREAD_COUNT ? 1 : MAX_THREAD_COUNT;
                    int threadCountExisted = 0;                    
                    for (int idx = 0; idx < threadCount; idx++)
                    {
                        workers[idx].DoWork += (workSender, ea) =>
                        {                            
                            var worker = workSender as BackgroundWorker;
                            int threadIndex = Convert.ToInt32(ea.Argument);
                            IList<CellValidationResult> results = new List<CellValidationResult>();  
                            /* Excel 文件包含标题行，第一行自动省略 */                          
                            for (int rowIndex = threadIndex + 1; rowIndex < fpUserTool.Sheets[0].RowCount; rowIndex += threadCount)
                            {
                                if (worker.CancellationPending)
                                {
                                    ea.Cancel = true;
                                    break;
                                }
                                string userName = fpUserTool.Sheets[0].Cells[rowIndex, 0].Text.Trim();
                                /* 单独验证 Excel 文件中用户名是否重复 */
                                UserToolError userToolError = UserToolError.None;
                                if (userNamesImported.Contains(userName))
                                {
                                    userToolError = UserToolError.RepeatedUserName;
                                }
                                else
                                {
                                    userNamesImported.Add(userName);                                   
                                }
                                if (userToolError != UserToolError.None)
                                {
                                    results.Add(new CellValidationResult(rowIndex, 0, userToolError));
                                }
                                else
                                {
                                    if (userToolAction == UserToolAction.UsersImported)
                                    {
                                        /* 导入用户时数据校验：（用户名，密码(明文即可)，真实姓名，用户类型名称，单位名称，身份证号，手机号码，电子邮件），前六列均不能为空； */
                                        results = ValidateUsers(rowIndex, -1);
                                    }
                                    else
                                    {
                                        results.Clear();
                                        /* 验证用户名 */
                                        userToolError = ValidateUserName(userName);
                                        if (userToolError != UserToolError.None)
                                        {
                                            results.Add(new CellValidationResult(rowIndex, 0, userToolError));
                                        }

                                        /* 删除用户时，Excel文件只包含一列 */
                                        if (userToolAction != UserToolAction.UserDeleted)
                                        {
                                            /* 校验数据 */
                                            string data = fpUserTool.Sheets[0].Cells[rowIndex, 1].Text.Trim();
                                            UserToolError dataError = ValidateData(userToolAction, userName, data);
                                            if (dataError != UserToolError.None)
                                            {
                                                results.Add(new CellValidationResult(rowIndex, 1, dataError));
                                            }
                                            else
                                            {
                                                /* 新用户名正常 */
                                                if (userToolAction == UserToolAction.UserName)
                                                {
                                                    newUserNames.Add(data);
                                                }
                                            }
                                        }
                                    }
                                }
                                CellValidationResults cellValidationResults = new CellValidationResults(threadIndex, results);
                                worker.ReportProgress(rowIndex, cellValidationResults);
                                autoResetEvents[threadIndex].WaitOne();
                            }
                        };
                        workers[idx].ProgressChanged += (workSender, ea) =>
                        {
                            try
                            {
                                var worker = workSender as BackgroundWorker;
                                if (errorCells.Count > 0)
                                {
                                    frmCommonProgress.Tip = string.Format("正在校验中：已发现错误单元格数目为{0}。", errorCells.Count);
                                }
                                else
                                {
                                    frmCommonProgress.Tip = "正在校验中：未发现发现错误单元格。";
                                }                                
                                CellValidationResults cellValidationResults = (CellValidationResults)ea.UserState;
                                isCellChangedValild = false;
                                foreach (CellValidationResult cllValidationResult in cellValidationResults.Results)
                                {
                                    if (worker.CancellationPending)
                                    {
                                        break;
                                    }
                                    SetErrorInfo(cllValidationResult.Row, cllValidationResult.Col, cllValidationResult.UserToolError);
                                }
                                frmCommonProgress.IncreaseStep();
                                isCellChangedValild = true;                                
                                autoResetEvents[cellValidationResults.Index].Set();
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
                                    if (threadCountExisted == threadCount)
                                    {
                                        frmCommonProgress.CloseFrom();
                                        SetErrorDescription();
                                        if (errorCells.Count == 0)
                                        {
                                            lblToolInfo.Text = "数据校验已完成，校验正确。";                                            
                                        }
                                        else
                                        {
                                            lblToolInfo.Text = string.Format("数据校验已完成，错误单元格数共有：{0}个。", errorCells.Count);
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
                        for (int idx = 0; idx < MAX_THREAD_COUNT; idx++)
                        {
                            workers[idx].CancelAsync();
                        }
                        lblToolInfo.Text = "数据校验已取消。";
                    };
                    frmCommonProgress.ShowDialog();
                }
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
                if (errorCells.Count == 0)
                {
                    lblErrorTip.Text = "校验正确。";
                }
                else
                {
                    lblErrorTip.Text = string.Format("错误单元格数共有：{0}个。", errorCells.Count);
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
            if (gcMain.Tag != null)
            {
                if (!validationCompleted)
                {
                    MessageBox.Show("请完成数据校验后再导入数据。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                dataRowsFailed.Clear();
                UserToolAction userToolAction = (UserToolAction)gcMain.Tag;
                switch (userToolAction)
                {
                    case UserToolAction.UsersImported:
                        ImportUsers();
                        break;

                    case UserToolAction.UserDeleted:
                        DeleteUsers();
                        break;

                    case UserToolAction.UserName:                        
                    case UserToolAction.UserActualName:
                    case UserToolAction.UserIdentity:
                    case UserToolAction.TelephoneNumber:
                    case UserToolAction.UserTypeName:                        
                    case UserToolAction.DepName:
                    case UserToolAction.Email:
                    case UserToolAction.Password:
                        EditUserInfos(userToolAction);
                        break;

                    default:
                        throw new ArgumentException("导入参数异常。");
                }
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
            if (errorCells.Count > 0)
            {
                saveFileDialog.FileName = string.Format("{0}_校验错误数据_{1:yyyyMMddHHmmss}.xlsx", gcMain.Text, DateTime.Now);
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
                        IList<int> errorDataRows = GetErrorDataRows();
                        SheetView sheetView = new SheetView(string.Format("{0}_校验错误数据", gcMain.Text));
                        fpSpread.Sheets.Add(sheetView);
                        sheetView.RowCount = errorDataRows.Count + 1;
                        sheetView.ColumnCount = fpUserTool.Sheets[0].ColumnCount;
                        sheetView.Cells[0, 0, errorDataRows.Count, sheetView.ColumnCount - 1].CellType = new TextCellType();              
                        for (int idx = 0; idx <= errorDataRows.Count; idx++)
                        {
                            for (int col = 0; col < sheetView.ColumnCount; col++)
                            {
                                if (idx == 0)
                                {
                                    sheetView.Cells[idx, col].Text = fpUserTool.Sheets[0].Cells[idx, col].Text;
                                }
                                else
                                {
                                    int key = errorDataRows[idx - 1] * USERS_IMPORTED_COLUMNS + col;
                                    if (errorCells.ContainsKey(key))
                                    {
                                        sheetView.Cells[idx, col].BackColor = Color.LightSteelBlue;
                                    }
                                    sheetView.Cells[idx, col].Text = fpUserTool.Sheets[0].Cells[errorDataRows[idx - 1], col].Text;
                                }
                            }
                        }                        
                        if (saveFileDialog.FileName.EndsWith("xlsx"))
                        {
                            fpSpread.SaveExcel(saveFileDialog.FileName, ExcelSaveFlags.UseDefaultColorPalette | FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat);
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
            if (dataRowsFailed.Count > 0)
            {
                saveFileDialog.FileName = string.Format("{0}_导入错误数据_{1:yyyyMMddHHmmss}.xlsx", gcMain.Text, DateTime.Now);
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
                        SheetView sheetView = new SheetView(string.Format("{0}_导入错误数据", gcMain.Text));
                        fpSpread.Sheets.Add(sheetView);
                        sheetView.RowCount = dataRowsFailed.Count + 1;
                        sheetView.ColumnCount = fpUserTool.Sheets[0].ColumnCount;
                        sheetView.Cells[0, 0, dataRowsFailed.Count, sheetView.ColumnCount - 1].CellType = new TextCellType();
                        for (int idx = 0; idx <= dataRowsFailed.Count; idx++)
                        {
                            for (int col = 0; col < sheetView.ColumnCount; col++)
                            {
                                if (idx == 0)
                                {
                                    sheetView.Cells[idx, col].Text = fpUserTool.Sheets[0].Cells[idx, col].Text;
                                }
                                else
                                {
                                    sheetView.Cells[idx, col].Text = fpUserTool.Sheets[0].Cells[dataRowsFailed[idx - 1], col].Text;
                                }
                            }
                        }
                        if (saveFileDialog.FileName.EndsWith("xlsx"))
                        {
                            fpSpread.SaveExcel(saveFileDialog.FileName, ExcelSaveFlags.UseDefaultColorPalette | FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat);
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
            /* 三种情况均不校验：未校验、忽略单元格变化、第一行是标题行 */
            if (!validationCompleted || !isCellChangedValild || e.Row == 0)
            {
                return;
            }
            UserToolAction userToolAction = (UserToolAction)gcMain.Tag;
            string cellValue = fpUserTool.Sheets[0].Cells[e.Row, e.Column].Text.Trim();

            UserToolError userToolError = UserToolError.None;
            /* 校验用户名重复 */
            if (e.Column == 0)
            {                
                if (userNamesImported.Contains(cellValue))
                {
                    userToolError = UserToolError.RepeatedUserName;
                    SetErrorInfo(e.Row, e.Column, userToolError);
                    UpdateErrorDataTable(e.Row, e.Column, userToolError);
                }
                else
                {
                    userNamesImported.Add(cellValue);
                }
            }
            if (userToolError == UserToolError.None)
            {
                if (userToolAction != UserToolAction.UsersImported)
                {
                    if (!string.IsNullOrWhiteSpace(cellValue))
                    {
                        UserToolError error = UserToolError.None;
                        switch (e.Column)
                        {
                            case 0:
                                /* 校验用户名 */
                                error = ValidateUserName(cellValue);
                                break;

                            case 1:
                                /* 校验数据 */
                                string userName = fpUserTool.Sheets[0].Cells[e.Row, 0].Text.Trim();
                                error = ValidateData(userToolAction, userName, cellValue);
                                break;

                        }
                        SetErrorInfo(e.Row, e.Column, error);
                        UpdateErrorDataTable(e.Row, e.Column, error);
                    }
                    else
                    {
                        SetErrorInfo(e.Row, e.Column, UserToolError.DataEmpty);
                        UpdateErrorDataTable(e.Row, e.Column, UserToolError.DataEmpty);
                    }
                }
                else
                {
                    CellValidationResult cellValidationResult = ValidateUserData(e.Row, e.Column);
                    if (cellValidationResult != null)
                    {
                        SetErrorInfo(cellValidationResult.Row, cellValidationResult.Col, cellValidationResult.UserToolError);
                        UpdateErrorDataTable(cellValidationResult.Row, cellValidationResult.Col, cellValidationResult.UserToolError);
                    }
                }
            }
            if (errorCells.Count == 0)
            {
                lblToolInfo.Text = "数据校验已完成，校验正确。";
            }
            else
            {
                lblToolInfo.Text = string.Format("数据校验已完成，错误单元格数共有：{0}个。", errorCells.Count);
            }
        }

        /// <summary>
        /// 保存修改后的数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSaveAs_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SpreadToolHelper.ExprotExcel(fpUserTool, string.Format("{0}_Excel数据另存", gcMain.Text));
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
            int row = Convert.ToInt32(gridView.GetFocusedDataRow()["RowIndex"]);
            int col = Convert.ToInt32(gridView.GetFocusedDataRow()["ColumnIndex"]);
            if (row > 0 && col > 0 && row <= fpUserTool.ActiveSheet.RowCount && col <= fpUserTool.ActiveSheet.ColumnCount)
            {
                fpUserTool.ActiveSheet.SetActiveCell(row - 1, col - 1);
            }
        }

        /// <summary>
        /// 用户名更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiUserName_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (SwitchDataImported(nbiUserName))
            {
                ClearCahcheResources();
                gcMain.Text = nbiUserName.Caption;
                gcMain.Tag = UserToolAction.UserName;                
            }
        }

        /// <summary>
        /// 用户姓名更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiUserActualName_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (SwitchDataImported(nbiUserActualName))
            {
                ClearCahcheResources();
                gcMain.Text = nbiUserActualName.Caption;
                gcMain.Tag = UserToolAction.UserActualName;
            }
        }

        /// <summary>
        /// 身份证号更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiUserIdentity_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (SwitchDataImported(nbiUserIdentity))
            {
                ClearCahcheResources();
                gcMain.Text = nbiUserIdentity.Caption;
                gcMain.Tag = UserToolAction.UserIdentity;
            }
        }

        /// <summary>
        /// 电话号码更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiTelephone_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (SwitchDataImported(nbiTelephone))
            {
                ClearCahcheResources();
                gcMain.Text = nbiTelephone.Caption;
                gcMain.Tag = UserToolAction.TelephoneNumber;
            }
        }

        /// <summary>
        /// 用户类型名称更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiUserType_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (SwitchDataImported(nbiUserType))
            {
                ClearCahcheResources();
                gcMain.Text = nbiUserType.Caption;
                gcMain.Tag = UserToolAction.UserTypeName;
            }
        }

        /// <summary>
        /// 用户单位名称更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiUserDepartment_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (SwitchDataImported(nbiUserDepartment))
            {
                ClearCahcheResources();
                gcMain.Text = nbiUserDepartment.Caption;
                gcMain.Tag = UserToolAction.DepName;
            }
        }        

        /// <summary>
        /// 批量用户导入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiUsersImported_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (SwitchDataImported(nbiUsersImported))
            {
                ClearCahcheResources();
                gcMain.Text = nbiUsersImported.Caption;
                gcMain.Tag = UserToolAction.UsersImported;
            }
        }        

        /// <summary>
        /// 批量用户删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiUserDeleted_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (SwitchDataImported(nbiUserDeleted))
            {
                ClearCahcheResources();
                gcMain.Text = nbiUserDeleted.Caption;
                gcMain.Tag = UserToolAction.UserDeleted;
            }
        }

        /// <summary>
        /// 更新密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiPassword_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (SwitchDataImported(nbiPassword))
            {
                ClearCahcheResources();
                gcMain.Text = nbiPassword.Caption;
                gcMain.Tag = UserToolAction.Password;
            }
        }

        /// <summary>
        /// 用户邮件地址批量更新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void nbiEmailAddress_LinkClicked(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            if (SwitchDataImported(nbiEmailAddress))
            {
                ClearCahcheResources();
                gcMain.Text = nbiEmailAddress.Caption;
                gcMain.Tag = UserToolAction.Email;
            }
        }

        /// <summary>
        /// 批量用户模板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiUserTemplate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                saveFileDialog.Filter = AppSettingHelper.DefaultExcelFormat;
                saveFileDialog.InitialDirectory = WinPlatformHelper.GetFileFloder();
                saveFileDialog.FileName = "批量用户导入_模板.xlsx";
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                WinPlatformHelper.LastestFilePath = Path.GetDirectoryName(saveFileDialog.FileName);
                Cursor = Cursors.WaitCursor;
                Dictionary<string, IList<string>> templateColumnCaptions = new Dictionary<string, IList<string>>();
                IList<string> columnCaptionsInFstSheet = new List<string>();
                columnCaptionsInFstSheet.Add("用户名（非空）");
                columnCaptionsInFstSheet.Add("密码(明文即可, 非空)");
                columnCaptionsInFstSheet.Add("真实姓名（非空）");
                columnCaptionsInFstSheet.Add("用户类型名称（非空）");
                columnCaptionsInFstSheet.Add("单位名称（非空）");
                columnCaptionsInFstSheet.Add("身份证号（非空）");
                columnCaptionsInFstSheet.Add("手机号码（可空）");
                columnCaptionsInFstSheet.Add("电子邮件（可空）");
                templateColumnCaptions.Add("批量用户模板", columnCaptionsInFstSheet);
                SpreadToolHelper.ExprotTemplate(templateColumnCaptions, saveFileDialog.FileName);
                Cursor = Cursors.Default;
                MessageBox.Show("批量用户模板文件导出成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
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
        /// 清空缓存资源
        /// </summary>
        private void ClearCahcheResources()
        {
            Cursor = Cursors.WaitCursor;
            if (errorCells.Count > 0)
            {
                fpUserTool.Sheets[0].Cells[0, 0, fpUserTool.Sheets[0].RowCount - 1, fpUserTool.Sheets[0].ColumnCount - 1].ResetBackColor();
                fpUserTool.Sheets[0].Cells[0, 0, fpUserTool.Sheets[0].RowCount - 1, fpUserTool.Sheets[0].ColumnCount - 1].ErrorText = string.Empty;
            }
            errorCells.Clear();
            newUserNames.Clear();
            dataRowsFailed.Clear();
            userNamesImported.Clear();
            DataTable dataTable = (DataTable)gridControl.DataSource;
            dataTable.Clear();
            gridControl.RefreshDataSource();
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 初始化线程资源
        /// </summary>
        private void InitThreadResrouces()
        {
            for (int idx = 0; idx < MAX_THREAD_COUNT; idx++)
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
        /// 批量修改用户信息
        /// </summary>
        /// <param name="userToolAction"></param>
        private void EditUserInfos(UserToolAction userToolAction)
        {
            string taskName = UserEnumHelper.GetEnumText(userToolAction);
            if (fpUserTool.Sheets[0].ColumnCount != USERS_OPERATION_COLUMNS)
            {
                MessageBox.Show(string.Format("{0}时，Excel文件要求第一列用户名，第二列为对应的值。", taskName),
                    "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (fpUserTool.Sheets[0].RowCount <= 1 || fpUserTool.Sheets[0].RowCount > 20000)
            {
                MessageBox.Show(string.Format("一次批量{0}记录总数不能超过20,000条。", taskName), "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }            
            if (errorCells.Count > 0)
            {
                if (MessageBox.Show(string.Format("至少有{0}个单元格数据存在异常，是否继续批量{1}？", errorCells.Count, taskName),
                    "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show(string.Format("确认{0}？", taskName), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                InitCommonProgress(string.Format("正在批量{0}，请稍后...", taskName), 0, fpUserTool.Sheets[0].RowCount - 1);
                lblToolInfo.Text = string.Format("正在批量{0}...。", taskName);
                IList<int> errorDataRows = GetErrorDataRows();
                InitThreadResrouces();
                int threadCount = fpUserTool.Sheets[0].RowCount < MAX_THREAD_COUNT ? 1 : MAX_THREAD_COUNT;
                int threadCountExisted = 0; /* 子线程完成任务后退出计数 */
                int usersUpdated = 0; /* 成功批量更新用户信息总数 */
                for (int idx = 0; idx < threadCount; idx++)
                {
                    workers[idx].DoWork += (workSender, ea) =>
                    {
                        var worker = workSender as BackgroundWorker;
                        int threadIndex = Convert.ToInt32(ea.Argument);
                        for (int rowIndex = threadIndex + 1; rowIndex < fpUserTool.Sheets[0].RowCount; rowIndex += threadCount)
                        {
                            if (worker.CancellationPending)
                            {
                                ea.Cancel = true;
                                break;
                            }
                            if (errorDataRows.Contains(rowIndex))
                            {
                                continue;
                            }
                            string userName = fpUserTool.Sheets[0].Cells[rowIndex, 0].Text.Trim();
                            string data = fpUserTool.Sheets[0].Cells[rowIndex, 1].Text.Trim();
                            try
                            {
                                switch (userToolAction)
                                {
                                    case UserToolAction.UserName:
                                        userAccountContract.UpdateUserName(userName, data);
                                        break;

                                    case UserToolAction.UserActualName:
                                    case UserToolAction.UserIdentity:
                                    case UserToolAction.TelephoneNumber:
                                    case UserToolAction.Password:
                                    case UserToolAction.Email:
                                        userAccountContract.UpdateUserInfo(userToolAction, userName, data);
                                        break;

                                    case UserToolAction.UserTypeName:
                                        if (userTypeNameAndIds.ContainsKey(data))
                                        {
                                            userAccountContract.UpdateUserTypeIdByUserName(userName, userTypeNameAndIds[data]);
                                        }                                        
                                        break;

                                    case UserToolAction.DepName:
                                        if (departmentNameAndIds.ContainsKey(data))
                                        {
                                            userAccountContract.UpdateDepIdByUserName(userName, departmentNameAndIds[data]);
                                        }                                        
                                        break;

                                    default:
                                        throw new ArgumentException("用户数据更新参数异常。");
                                }
                                usersUpdated++;
                            }
                            catch
                            {
                                if (!dataRowsFailed.Contains(rowIndex))
                                {
                                    dataRowsFailed.Add(rowIndex);
                                }
                            }
                            worker.ReportProgress(rowIndex, threadIndex);
                            autoResetEvents[threadIndex].WaitOne();
                        }
                    };
                    workers[idx].ProgressChanged += (workSender, ea) =>
                    {
                        try
                        {
                            var worker = workSender as BackgroundWorker;
                            int threadIndex = (int)ea.UserState;
                            frmCommonProgress.Tip = string.Format("正在批量{0}：总记录数{1}条，已成功更新{2}条，更新失败{3}条，忽略{4}条。",
                                    taskName, fpUserTool.Sheets[0].RowCount - 1, usersUpdated, dataRowsFailed.Count, errorDataRows.Count);
                            frmCommonProgress.IncreaseStep();
                            autoResetEvents[threadIndex].Set();
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
                                if (threadCountExisted == threadCount)
                                {
                                    frmCommonProgress.CloseFrom();
                                    lblToolInfo.Text = string.Format("批量{0}已完成，总记录数{1}条：已成功更新{2}条，更新失败{3}条，忽略{4}条。",
                                            taskName, usersUpdated + dataRowsFailed.Count + errorDataRows.Count, usersUpdated, dataRowsFailed.Count, errorDataRows.Count);
                                }
                            }
                        }
                    };
                    workers[idx].RunWorkerAsync(idx);
                }
                Cursor = Cursors.Default;
                frmCommonProgress.TaskCancelled = delegate ()
                {
                    for (int idx = 0; idx < MAX_THREAD_COUNT; idx++)
                    {
                        workers[idx].CancelAsync();
                    }
                    lblToolInfo.Text = string.Format("批量{0}已取消。", taskName);
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
        /// 删除用户
        /// </summary>
        private void DeleteUsers()
        {
            if (fpUserTool.Sheets[0].ColumnCount != USERS_DELETED_COLUMNS)
            {
                MessageBox.Show("批量用户删除时，Excel文件包含一列，即第一列为用户名。", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (fpUserTool.Sheets[0].RowCount <= 1 || fpUserTool.Sheets[0].RowCount > 5000)
            {
                MessageBox.Show("一次删除的用户总数不能超过5,000条。", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (errorCells.Count > 0)
            {
                if (MessageBox.Show(string.Format("至少有{0}个用户名不存在，是否继续导入数据({1})？", errorCells.Count, gcMain.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
            }
            else
            {
                if (MessageBox.Show(string.Format("确认导入数据({0})？", gcMain.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                {
                    return;
                }
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                InitCommonProgress("正在批量删除用户，请稍后...", 0, fpUserTool.Sheets[0].RowCount - 1);
                lblToolInfo.Text = "正在批量删除用户...。";
                IList<int> errorDataRows = GetErrorDataRows();
                InitThreadResrouces();
                int threadCount = fpUserTool.Sheets[0].RowCount < MAX_THREAD_COUNT ? 1 : MAX_THREAD_COUNT;
                int threadCountExisted = 0; /* 子线程完成任务后退出计数 */
                int usersDeleted = 0; /* 成功删除用户总数 */
                for (int idx = 0; idx < threadCount; idx++)
                {
                    workers[idx].DoWork += (workSender, ea) =>
                    {
                        var worker = workSender as BackgroundWorker;
                        int threadIndex = Convert.ToInt32(ea.Argument);
                        for (int rowIndex = threadIndex + 1; rowIndex < fpUserTool.Sheets[0].RowCount; rowIndex += threadCount)
                        {
                            if (worker.CancellationPending)
                            {
                                ea.Cancel = true;
                                break;
                            }
                            if (errorDataRows.Contains(rowIndex))
                            {
                                continue;
                            }
                            string userName = fpUserTool.Sheets[0].Cells[rowIndex, 0].Text.Trim(); 
                            try
                            {
                                decimal userId = userAccountContract.GetUserIdByUserName(userName);
                                if (userId > 0)
                                {
                                    userAccountContract.Delete(userId);
                                    usersDeleted++;
                                }
                            }
                            catch
                            {
                                if (!dataRowsFailed.Contains(rowIndex))
                                {
                                    dataRowsFailed.Add(rowIndex);
                                }
                            }
                            worker.ReportProgress(rowIndex, threadIndex);
                            autoResetEvents[threadIndex].WaitOne();
                        }
                    };
                    workers[idx].ProgressChanged += (workSender, ea) =>
                    {
                        try
                        {
                            var worker = workSender as BackgroundWorker;
                            int threadIndex = (int)ea.UserState;
                            frmCommonProgress.Tip = string.Format("正在批量删除用户：总用户数{0}个，已成功删除{1}个，删除失败{2}个，忽略{3}个。",
                                    fpUserTool.Sheets[0].RowCount - 1, usersDeleted, dataRowsFailed.Count, errorDataRows.Count);
                            frmCommonProgress.IncreaseStep();
                            autoResetEvents[threadIndex].Set();
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
                                if (threadCountExisted == threadCount)
                                {
                                    frmCommonProgress.CloseFrom();
                                    lblToolInfo.Text = string.Format("批量删除用户已完成，总用户数{0}个：已成功删除{1}个用户，删除失败{2}个，忽略{3}个。",
                                            usersDeleted + dataRowsFailed.Count + errorDataRows.Count, usersDeleted, dataRowsFailed.Count, errorDataRows.Count);
                                }
                            }
                        }
                    };
                    workers[idx].RunWorkerAsync(idx);
                }
                Cursor = Cursors.Default;
                frmCommonProgress.TaskCancelled = delegate ()
                {
                    for (int idx = 0; idx < MAX_THREAD_COUNT; idx++)
                    {
                        workers[idx].CancelAsync();
                    }
                    lblToolInfo.Text = "批量删除用户已取消。";
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
        /// 导入用户
        /// </summary>
        private void ImportUsers()
        {
            
            if (fpUserTool.Sheets[0].ColumnCount != USERS_IMPORTED_COLUMNS)
            {
                MessageBox.Show("批量用户导入时，Excel文件包含七列，（用户名，密码(明文即可)，真实姓名，用户类型名称，单位名称，身份证号，手机号码），所有列均不能为空。",
                    "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (fpUserTool.Sheets[0].RowCount <= 1 || fpUserTool.Sheets[0].RowCount > 20000)
            {
                MessageBox.Show("一次导入的用户数据记录总数不能超过20,000条。", "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (errorCells.Count > 0 && (MessageBox.Show(string.Format("至少有{0}个单元格数据不符合规范，是否继续导入数据？", errorCells.Count), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK))
            {
                return;
            }
            ImportedMode importedMode = ImportedMode.Append;
            ImportedModeForm frmDataImported = new ImportedModeForm();
            frmDataImported.ImportedModeValue = (1L << (byte)ImportedMode.UpdateAndInsert) | (1L << (byte)ImportedMode.NotUpdateAndInsert) | (1L << (byte)ImportedMode.UpdateAndNotInsert);
            frmDataImported.DataImported = (mode) =>
            {
                importedMode = mode;
            };
            frmDataImported.ShowDialog();
            if (MessageBox.Show(string.Format("确认导入数据({0})？", gcMain.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
            {
                return;
            }

            try
            {
                Cursor = Cursors.WaitCursor;
                InitCommonProgress("正在批量导入用户，请稍后...", 0, fpUserTool.Sheets[0].RowCount - 1);
                lblToolInfo.Text = "正在批量导入用户...。";
                IList<int> errorDataRows = GetErrorDataRows();                
                InitThreadResrouces();                
                int threadCount = fpUserTool.Sheets[0].RowCount < MAX_THREAD_COUNT ? 1 : MAX_THREAD_COUNT;
                int threadCountExisted = 0; /* 子线程完成任务后退出计数 */
                int usersImported = 0; /* 成功导入用户总数 */
                for (int idx = 0; idx < threadCount; idx++)
                {
                    workers[idx].DoWork += (workSender, ea) =>
                    {
                        var worker = workSender as BackgroundWorker;
                        int threadIndex = Convert.ToInt32(ea.Argument);
                        for (int rowIndex = threadIndex + 1; rowIndex < fpUserTool.Sheets[0].RowCount; rowIndex += threadCount)
                        {
                            if (worker.CancellationPending)
                            {
                                ea.Cancel = true;
                                break;
                            }
                            if (errorDataRows.Contains(rowIndex))
                            {
                                continue;
                            }

                            /* （用户名，密码(明文即可)，真实姓名，用户类型名称，单位名称，身份证号，手机号码，电子邮件），前六列均不能为空 */
                            string userName = fpUserTool.Sheets[0].Cells[rowIndex, 0].Text.Trim();
                            string password = fpUserTool.Sheets[0].Cells[rowIndex, 1].Text.Trim();
                            string userActualName = fpUserTool.Sheets[0].Cells[rowIndex, 2].Text.Trim();
                            string userTypeName = fpUserTool.Sheets[0].Cells[rowIndex, 3].Text.Trim();
                            string departmentName = fpUserTool.Sheets[0].Cells[rowIndex, 4].Text.Trim();
                            string identifier = fpUserTool.Sheets[0].Cells[rowIndex, 5].Text.Trim();
                            string telephoneNumber = fpUserTool.Sheets[0].Cells[rowIndex, 6].Text.Trim();
                            string email = fpUserTool.Sheets[0].Cells[rowIndex, 7].Text.Trim();

                            /* 用户类型编号 */
                            decimal userTypeId = decimal.MinValue;
                            if (userTypeNameAndIds.ContainsKey(userTypeName))
                            {
                                userTypeId = userTypeNameAndIds[userTypeName];
                            }
                            else
                            {
                                if (!dataRowsFailed.Contains(rowIndex))
                                {
                                    dataRowsFailed.Add(rowIndex);
                                }
                                continue;
                            }

                            /* 单位编号 */
                            decimal depId = decimal.MinValue;
                            if (departmentNameAndIds.ContainsKey(departmentName))
                            {
                                depId = departmentNameAndIds[departmentName];
                            }
                            else
                            {
                                if (!dataRowsFailed.Contains(rowIndex))
                                {
                                    dataRowsFailed.Add(rowIndex);
                                }
                                continue;
                            }
                            
                            IdentificationType identificationType = IdentificationType.Identification;
                            if (identifier.Length < AppSettingHelper.DefaultIdentityMinLength)
                            {
                                identificationType = IdentificationType.Passport;
                            }
                            UserAccountInfo userAccountInfo = new UserAccountInfo(0, depId, userTypeId, userName, password, userActualName, email, (byte)identificationType, identifier,
                                        telephoneNumber, DateTime.Now, string.Empty, string.Empty, false, 0, 0, Guid.NewGuid(), string.Empty, 0, DateTime.Now, DateTime.Now);
                            try
                            {
                                bool existed = userAccountContract.IsExistUserName(userName);
                                switch (importedMode)
                                {
                                    case ImportedMode.UpdateAndInsert:
                                        if (existed)
                                        {
                                            userAccountContract.UpdateUserAccountInfo(userAccountInfo);
                                        }
                                        else
                                        {
                                            userAccountContract.Insert(userAccountInfo);
                                        }
                                        usersImported++;
                                        break;

                                    case ImportedMode.NotUpdateAndInsert:
                                        if (existed)
                                        {
                                            dataRowsFailed.Add(rowIndex);
                                        }
                                        else
                                        {
                                            userAccountContract.Insert(userAccountInfo);
                                            usersImported++;
                                        }                                    
                                        break;

                                    case ImportedMode.UpdateAndNotInsert:
                                        if (existed)
                                        {
                                            userAccountContract.UpdateUserAccountInfo(userAccountInfo);
                                            usersImported++;
                                        }                                        
                                        break;
                                }
                            }
                            catch
                            {
                                if (!dataRowsFailed.Contains(rowIndex))
                                {
                                    dataRowsFailed.Add(rowIndex);
                                }
                            }                            
                            worker.ReportProgress(rowIndex, threadIndex);
                            autoResetEvents[threadIndex].WaitOne();
                        }
                    };
                    workers[idx].ProgressChanged += (workSender, ea) =>
                    {
                        try
                        {
                            var worker = workSender as BackgroundWorker;
                            int threadIndex = (int)ea.UserState;
                            //int skippedCount = fpUserTool.Sheets[0].RowCount - 1 - usersImported - dataRowsFailed.Count - errorDataRows.Count;
                            frmCommonProgress.Tip = string.Format("正在批量导入用户：总用户数{0}个，操作成功{1}个，失败{2}个，忽略{3}个。",
                                    fpUserTool.Sheets[0].RowCount - 1, usersImported, dataRowsFailed.Count, errorDataRows.Count);
                            frmCommonProgress.IncreaseStep();
                            autoResetEvents[threadIndex].Set();
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
                                if (threadCountExisted == threadCount)
                                {
                                    frmCommonProgress.CloseFrom();
                                    int skippedCount = fpUserTool.Sheets[0].RowCount - 1 - usersImported - dataRowsFailed.Count - errorDataRows.Count;
                                    lblToolInfo.Text = string.Format("批量导入用户已完成，总用户数{0}个：操作成功{1}个用户，失败{2}个，忽略{3}个，跳过{4}个。",
                                            usersImported + dataRowsFailed.Count + errorDataRows.Count + skippedCount, usersImported, dataRowsFailed.Count, errorDataRows.Count, skippedCount);
                                }
                            }
                        }
                    };
                    workers[idx].RunWorkerAsync(idx);
                }
                Cursor = Cursors.Default;
                frmCommonProgress.TaskCancelled = delegate ()
                {
                    for (int idx = 0; idx < MAX_THREAD_COUNT; idx++)
                    {
                        workers[idx].CancelAsync();
                    }
                    lblToolInfo.Text = "批量导入用户已取消。";
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
        /// 校验用户数据导入
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="colIndex"></param>
        /// <returns></returns>
        private CellValidationResult ValidateUserData(int rowIndex, int colIndex)
        {
            IList<CellValidationResult> cellValidationResults = ValidateUsers(rowIndex, colIndex);

            return cellValidationResults.Count > 0 ? cellValidationResults[0] : null;
        }

        /// <summary>
        /// 校验用户数据导入
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        private IList<CellValidationResult> ValidateUsers(int rowIndex, int colIndex)
        {
            IList<CellValidationResult> cellValidationResults = new List<CellValidationResult>();

            /* （用户名 */
            if (colIndex <= 0)
            {
                string userName = fpUserTool.Sheets[0].Cells[rowIndex, 0].Text.Trim();
                if (string.IsNullOrWhiteSpace(userName))
                {
                    cellValidationResults.Add(new CellValidationResult(rowIndex, 0, UserToolError.UserNameEmpty));
                }
                else
                {
                    /* 验证用户名长度是否超过合法限制 */
                    if (userName.Length > AppSettingHelper.DefaultUserNameMaxLength || userName.Length < AppSettingHelper.DefaultUserNameMinLength)
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 0, UserToolError.UserNameLenth));
                    }
                    else
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 0, UserToolError.None));
                    }
                }
            }

            if (colIndex < 0 || colIndex == 1)
            {
                /* 密码(明文即可) */
                string passowrd = fpUserTool.Sheets[0].Cells[rowIndex, 1].Text.Trim();
                if (string.IsNullOrWhiteSpace(passowrd))
                {
                    cellValidationResults.Add(new CellValidationResult(rowIndex, 1, UserToolError.DataEmpty));
                }
                else
                {
                    /* 验证密码长度是否超过合法限制 */
                    if (passowrd.Length > AppSettingHelper.DefaultPaswordMaxLength || passowrd.Length < AppSettingHelper.DefaultUserNameMinLength)
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 1, UserToolError.PasswordLength));
                    }
                    else
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 1, UserToolError.None));
                    }
                }
            }

            if (colIndex < 0 || colIndex == 2)
            {
                /* 真实姓名 */
                string actualName = fpUserTool.Sheets[0].Cells[rowIndex, 2].Text.Trim();
                if (string.IsNullOrWhiteSpace(actualName))
                {
                    cellValidationResults.Add(new CellValidationResult(rowIndex, 2, UserToolError.DataEmpty));

                }
                else
                {
                    /* 验证真实姓名长度是否超过合法限制 */
                    if (actualName.Length > AppSettingHelper.DefaultUserActualNameMaxLength || actualName.Length < AppSettingHelper.DefaultUserActualNameMinLength)
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 2, UserToolError.UserActualNameLength));
                    }
                    else
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 2, UserToolError.None));
                    }
                }
            }

            if (colIndex < 0 || colIndex == 3)
            {
                /* 用户类型名称 */
                string userTypeName = fpUserTool.Sheets[0].Cells[rowIndex, 3].Text.Trim();
                if (string.IsNullOrWhiteSpace(userTypeName))
                {
                    cellValidationResults.Add(new CellValidationResult(rowIndex, 3, UserToolError.DataEmpty));
                }
                else
                {
                    if (!userTypeNameAndIds.ContainsKey(userTypeName))
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 3, UserToolError.UserTypeNotExisted));
                    }
                    else
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 3, UserToolError.None));
                    }
                }
            }

            if (colIndex < 0 || colIndex == 4)
            {
                /* 单位名称 */
                string departmentName = fpUserTool.Sheets[0].Cells[rowIndex, 4].Text.Trim();
                if (string.IsNullOrWhiteSpace(departmentName))
                {
                    cellValidationResults.Add(new CellValidationResult(rowIndex, 4, UserToolError.DataEmpty));
                }
                else
                {
                    if (!departmentNameAndIds.ContainsKey(departmentName))
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 4, UserToolError.DepartmentNotExisted));
                    }
                    else
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 4, UserToolError.None));
                    }
                }
            }

            if (colIndex < 0 || colIndex == 5)
            {
                /* 身份证号 */
                string identifier = fpUserTool.Sheets[0].Cells[rowIndex, 5].Text.Trim();
                if (string.IsNullOrWhiteSpace(identifier))
                {
                    cellValidationResults.Add(new CellValidationResult(rowIndex, 5, UserToolError.DataEmpty));
                }
                else
                {
                    if (!UserDataHelper.MatchIdentificationNumber(identifier))
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 5, UserToolError.IdentityError));
                    }
                    else
                    {
                        cellValidationResults.Add(new CellValidationResult(rowIndex, 5, UserToolError.None));
                    }
                }
            }

            if (colIndex < 0 || colIndex == 6)
            {
                /* 手机号码 */
                string telephoneNumber = fpUserTool.Sheets[0].Cells[rowIndex, 6].Text.Trim();
                if (!string.IsNullOrWhiteSpace(telephoneNumber) && !UserDataHelper.MatchMobilePhoneNumber(telephoneNumber))
                {
                    cellValidationResults.Add(new CellValidationResult(rowIndex, 6, UserToolError.TelephoneNumberError));
                }
                else
                {
                    cellValidationResults.Add(new CellValidationResult(rowIndex, 6, UserToolError.None));
                }
            }

            if (colIndex < 0 || colIndex == 7)
            {
                /* 邮件地址 */
                string emailAddress = fpUserTool.Sheets[0].Cells[rowIndex, 7].Text.Trim();
                if (!string.IsNullOrWhiteSpace(emailAddress) && !UserDataHelper.MatchEmail(emailAddress))
                {
                    cellValidationResults.Add(new CellValidationResult(rowIndex, 7, UserToolError.EmailError));
                }
                else
                {
                    cellValidationResults.Add(new CellValidationResult(rowIndex, 7, UserToolError.None));
                }
            }

            return cellValidationResults;
        }

        /// <summary>
        /// 校验数据
        /// </summary>
        /// <param name="userToolAction"></param>
        /// <param name="userName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private UserToolError ValidateData(UserToolAction userToolAction, string userName, string data)
        {
            UserToolError userToolError = UserToolError.None;

            if (string.IsNullOrWhiteSpace(data))
            {
                userToolError = UserToolError.DataEmpty;
            }
            else
            {
                switch (userToolAction)
                {
                    case UserToolAction.UserTypeName:
                        if (!userTypeNameAndIds.ContainsKey(data))
                        {
                            userToolError =  UserToolError.UserTypeNotExisted;
                        }
                        break;

                    case UserToolAction.DepName:
                        if (!departmentNameAndIds.ContainsKey(data))
                        {
                            userToolError = UserToolError.DepartmentNotExisted;
                        }
                        break;

                    case UserToolAction.UserName:
                        if (data.Length > AppSettingHelper.DefaultUserNameMaxLength)
                        {
                            /* 验证新用户名长度是否超过合法限制 */
                            userToolError = UserToolError.NewUserNameLenth;
                        }
                        else if (userToolError == UserToolError.None && newUserNames.Contains(data))
                        {
                            /* 校验数据中的新用户名是否重复 */
                            userToolError = UserToolError.RepeatedNewUserName;
                        }
                        break;

                    case UserToolAction.UserActualName:
                        /* 验证真实姓名长度是否超过合法限制 */
                        if (data.Length > AppSettingHelper.DefaultUserActualNameMaxLength || data.Length < AppSettingHelper.DefaultUserActualNameMinLength)
                        {
                            userToolError = UserToolError.UserActualNameLength;
                        }
                        break;

                    case UserToolAction.UserIdentity:
                        /* 验证证件号码的合法性 */
                        if (!UserDataHelper.MatchIdentificationNumber(data))
                        {
                            userToolError = UserToolError.IdentityError;
                        }
                        break;

                    case UserToolAction.TelephoneNumber:
                        if (!UserDataHelper.MatchMobilePhoneNumber(data))
                        {
                            userToolError = UserToolError.TelephoneNumberError;
                        }
                        break;

                    case UserToolAction.Password:
                        /* 验证密码长度是否超过合法限制 */
                        if (data.Length > AppSettingHelper.DefaultPaswordMaxLength || data.Length < AppSettingHelper.DefaultUserNameMinLength)
                        {
                            userToolError = UserToolError.PasswordLength;
                        }
                        break;

                    case UserToolAction.Email:
                        if (!UserDataHelper.MatchEmail(data))
                        {
                            userToolError = UserToolError.EmailError;
                        }
                        break;
                }                
            }

            return userToolError;
        }

        /// <summary>
        /// 校验用户名
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private UserToolError ValidateUserName(string userName)
        {
            UserToolError userToolError = UserToolError.None;

            if (string.IsNullOrWhiteSpace(userName))
            {
                userToolError = UserToolError.UserNameEmpty;
            }
            else
            {
                /* 验证用户名长度是否超过合法限制 */
                if (userName.Length > AppSettingHelper.DefaultUserNameMaxLength || userName.Length < AppSettingHelper.DefaultUserNameMinLength)
                {
                    userToolError = UserToolError.UserNameLenth;
                }
                else if (!userAccountContract.IsExistUserName(userName))
                {
                    userToolError = UserToolError.UserNameNotExisted;
                }
            }

            return userToolError;            
        }                      
        
        /// <summary>
        /// 设置或是清除错误信息
        /// </summary>
        /// <param name="row"></param>
        /// <param name="column"></param>
        /// <param name="userToolError">错误信息</param>
        private void SetErrorInfo(int row, int column, UserToolError userToolError)
        {
            isCellChangedValild = false;
            FarPoint.Win.Spread.Cell cell = fpUserTool.Sheets[0].Cells[row, column];
            int index = row * USERS_IMPORTED_COLUMNS + column;
            if (userToolError == UserToolError.None)
            {               
                if (errorCells.ContainsKey(index))
                {
                    cell.BackColor = Color.White;
                    cell.ErrorText = string.Empty;
                    errorCells.Remove(index);
                }
            }
            else
            {                
                if (errorCells.ContainsKey(index))
                {
                    if (errorCells[index] != userToolError)
                    {
                        errorCells[index] = userToolError;
                    }
                }
                else
                {
                    cell.BackColor = Color.LightSteelBlue;
                    cell.ErrorText = UserEnumHelper.GetEnumText(userToolError);
                    errorCells.Add(index, userToolError);
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
             IList <EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(UserToolError));
            foreach (KeyValuePair<int, UserToolError> errorCell in errorCells)
            {
                int row = errorCell.Key / USERS_IMPORTED_COLUMNS;
                int col = errorCell.Key % USERS_IMPORTED_COLUMNS;
                DataRow dataRow = dataTable.NewRow();
                dataRow[0] = row + 1;
                dataRow[1] = col + 1;
                dataRow[2] = UserEnumHelper.GetEnumText((UserToolError)errorCell.Value);
                dataTable.Rows.Add(dataRow);
            }
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// 初始化错误结果表
        /// </summary>
        private void InitErrorDataTable()
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
            gridView.Columns[0].Width = 75;
            gridView.Columns[1].Width = 75;
            gridView.Columns[2].Width = 300;
        }

        /// <summary>
        /// 更新错误结果表
        /// </summary>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="userToolError"></param>
        private void UpdateErrorDataTable(int row, int col, UserToolError userToolError)
        {
            DataTable dataTable = (DataTable)gridControl.DataSource;
            DataRow[] dataRows = dataTable.Select(string.Format("RowIndex = {0} AND ColumnIndex = {1}", row + 1, col + 1));
            if (userToolError == UserToolError.None)
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
                    dataRow[2] = UserEnumHelper.GetEnumText(userToolError);
                    dataTable.Rows.Add(dataRow);
                }
            }
            gridControl.RefreshDataSource();
        }

        /// <summary>
        /// 获得错误的行数
        /// </summary>
        /// <returns></returns>
        private IList<int> GetErrorDataRows()
        {
            IList<int> errorDataRows = new List<int>();

            foreach (KeyValuePair<int, UserToolError> keyValue in errorCells)
            {
                int row = keyValue.Key / USERS_IMPORTED_COLUMNS;
                if (!errorDataRows.Contains(row))
                {
                    errorDataRows.Add(row);
                }
            }

            return errorDataRows;
        }

        /// <summary>
        /// 校验后是切换成新数据
        /// </summary>
        /// <param name="navBarItem"></param>
        /// <returns></returns>
        private bool SwitchDataImported(DevExpress.XtraNavBar.NavBarItem navBarItem)
        {
            bool switchPanel = false;

            if (validationCompleted)
            {
                if (MessageBox.Show(string.Format("{0}已完成了数据校验，切换成{1}后需要重新校验数据，确认切换？", gcMain.Text, navBarItem.Caption),
                "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    validationCompleted = false;
                    switchPanel = true;
                }
            }
            else
            {
                switchPanel = true;
            }

            return switchPanel;
        }

        #endregion        
    }
}
