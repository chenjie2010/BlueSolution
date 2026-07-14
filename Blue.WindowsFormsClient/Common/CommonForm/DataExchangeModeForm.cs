using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FarPoint.Win;
using FarPoint.Win.Spread;
using FarPoint.Excel;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WindowsFormsClient;

namespace Blue.WindowsFormsClient.Common
{
    public partial class DataExchangeModeForm : Form
    {

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

        #region 私有变量

        /// <summary>
        /// 导出 Excel 文件
        /// </summary>
        private readonly FpSpread fsExcel = null;

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
        /// 提示信息
        /// </summary>
        public string Tip
        {
            get
            {
                return gcMain.Text;
            }
            set
            {
                gcMain.Text = value;
            }
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
        public DataExchangeModeForm()
        {
            InitializeComponent();
            fsExcel = new FpSpread();
            UserControlHelper.InitRadioGroupItems(rgDataExchangeMode, typeof(DataExchangeMode));
        }

        #endregion        

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataExchangeModeForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 确定操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (rgDataExchangeMode.SelectedIndex < 0)
            {
                MessageBox.Show("请先选择数据交换方式。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            saveFileDialog.Filter = AppSettingHelper.DefaultExcelFormat;
            saveFileDialog.InitialDirectory = WinPlatformHelper.GetFileFloder();
            DataExchangeMode dataExchangeMode = (DataExchangeMode)Convert.ToInt32(rgDataExchangeMode.Properties.Items[rgDataExchangeMode.SelectedIndex].Value);
            if (dataExchangeMode == DataExchangeMode.Export || dataExchangeMode == DataExchangeMode.Template)
            {
                switch (dataExchangeMode)
                {
                    case DataExchangeMode.Export:
                        saveFileDialog.FileName = string.Format("{0}_{1:yyyyMMddHHmmss}.xlsx", DataExportedInterface.DataExportedName, DateTime.Now);
                        break;

                    case DataExchangeMode.Template:
                        saveFileDialog.FileName = string.Format("{0}_模板.xlsx", DataExportedInterface.DataExportedName);
                        break;

                    case DataExchangeMode.Import:
                        break;
                }
                saveFileDialog.RestoreDirectory = true;
                if (saveFileDialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                WinPlatformHelper.LastestFilePath = Path.GetDirectoryName(saveFileDialog.FileName);
            }

            try
            {
                switch (dataExchangeMode)
                {
                    case DataExchangeMode.Export:
                        Cursor = Cursors.WaitCursor;
                        BackgroundWorker backgroundWorker = new BackgroundWorker();
                        /* 设置数据源 */
                        DataSet dataSource = null;
                        progressPanel.Show();
                        backgroundWorker.DoWork += (workSender, ea) =>
                        {
                            if (DataExportedInterface.PagingEnabled)
                            {
                                dataSource = new DataSet();
                                int startPosition = 0, totalCount = 0;
                                do
                                {
                                    DataSet ds = DataExportedInterface.GetPageRecord(startPosition, DATA_PAGE_SIZE_IMPORTED, ref totalCount); 
                                    /* 第一个表单数据合并 */                                   
                                    if (startPosition == 0)
                                    {
                                        dataSource.Tables.Add(ds.Tables[0].Copy());
                                    }
                                    else
                                    {
                                        foreach (DataRow dr in ds.Tables[0].Rows)
                                        {
                                            dataSource.Tables[0].ImportRow(dr);
                                        }
                                    }
                                    startPosition += DATA_PAGE_SIZE_IMPORTED;
                                    if (ds.Tables.Count > 1)
                                    {
                                        for (int index = 1; index < ds.Tables.Count; index++)
                                        {
                                            DataTable dataTable = ds.Tables[index].Copy();
                                            dataSource.Tables.Add(dataTable);
                                        }
                                    }
                                } while (startPosition < totalCount);                                
                            }
                            else
                            {
                                dataSource = DataExportedInterface.GetPageRecord();
                            }
                            if (!string.IsNullOrWhiteSpace(DataExportedInterface.StringSorted))
                            {
                                dataSource.Tables[0].DefaultView.Sort = DataExportedInterface.StringSorted;
                            }
                        };
                        backgroundWorker.RunWorkerCompleted += (workSender, ea) =>
                        {
                            if (!ea.Cancelled)
                            {
                                if (fsExcel.Sheets.Count > 0)
                                {
                                    fsExcel.Sheets.Clear();
                                }
                                /* 1. 结构数据 */
                                SheetView view = new SheetView(DataExportedInterface.DataExportedName);
                                fsExcel.Sheets.Add(view);
                                view.DataSource = dataSource.Tables[0];                                
                                /* 设置列标题 */
                                for (int col = 0; col < dataSource.Tables[0].Columns.Count; col++)
                                {
                                    if (col < fsExcel.Sheets[0].Columns.Count)
                                    {
                                        fsExcel.Sheets[0].ColumnHeader.Cells[0, col].Text = dataSource.Tables[0].Columns[col].Caption;
                                        int width = Convert.ToInt32(fsExcel.Sheets[0].Columns[col].Width);
                                        fsExcel.Sheets[0].Columns[col].Width = width < COLUMN_WIDTH_IN_SHEET_VIEW ? COLUMN_WIDTH_IN_SHEET_VIEW : width;
                                    }
                                }
                                view.ColumnHeader.Rows[0].Height = ROW_HEIGHT_IN_SHEET_VIEW;
                                if (view.RowCount > 0 && view.ColumnCount > 0)
                                {
                                    view.ColumnHeader.Cells[0, 0, 0, view.ColumnCount - 1].BackColor = Color.LightGray;
                                }

                                /* 2. 表格数据 */
                                for (int index = 1; index < dataSource.Tables.Count; index++)
                                {
                                    SheetView tableView = new SheetView(dataSource.Tables[index].TableName);
                                    tableView.ColumnHeader.Rows[0].Height = ROW_HEIGHT_IN_SHEET_VIEW;
                                    if (tableView.RowCount > 0 && tableView.ColumnCount > 0)
                                    {
                                        tableView.ColumnHeader.Cells[0, 0, 0, tableView.ColumnCount - 1].BackColor = Color.LightGray;
                                    }
                                    fsExcel.Sheets.Add(tableView);
                                    tableView.DataSource = dataSource.Tables[index];
                                    /* 设置列标题 */
                                    for (int col = 0; col < dataSource.Tables[index].Columns.Count; col++)
                                    {
                                        if (col < tableView.Columns.Count)
                                        {
                                            tableView.ColumnHeader.Cells[0, col].Text = dataSource.Tables[index].Columns[col].Caption;
                                            int width = Convert.ToInt32(tableView.Columns[col].Width) < COLUMN_WIDTH_IN_SHEET_VIEW ? COLUMN_WIDTH_IN_SHEET_VIEW : Convert.ToInt32(tableView.Columns[col].Width);
                                            tableView.Columns[col].Width = width;
                                        }
                                    }
                                }
                                progressPanel.Hide();
                                if (saveFileDialog.FileName.EndsWith("xlsx"))
                                {
                                    fsExcel.SaveExcel(saveFileDialog.FileName, ExcelSaveFlags.SaveCustomColumnHeaders | FarPoint.Excel.ExcelSaveFlags.UseOOXMLFormat);
                                }
                                else
                                {
                                    fsExcel.SaveExcel(saveFileDialog.FileName, ExcelSaveFlags.SaveCustomColumnHeaders);
                                }
                                MessageBox.Show("Excel数据文件导出成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);                                
                            }
                        };
                        backgroundWorker.RunWorkerAsync();
                        Cursor = Cursors.Default;
                        break;

                    case DataExchangeMode.Import:
                        DataImportingToolForm frmDataImportingTool = new DataImportingToolForm()
                        {
                            DataExportedInterface = DataExportedInterface,
                            RefreshForm = RefreshForm
                        };
                        frmDataImportingTool.ShowDialog();
                        break;

                    case DataExchangeMode.Template:
                        Cursor = Cursors.WaitCursor;
                        Dictionary<string, IList<string>> templateColumnCaptions = DataExportedInterface.GetTemplateColumnCaptions();
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
                        if (saveFileDialog.FileName.EndsWith("xlsx"))
                        {
                            fsExcel.SaveExcel(saveFileDialog.FileName, ExcelSaveFlags.SaveCustomColumnHeaders | ExcelSaveFlags.UseOOXMLFormat);
                        }
                        else
                        {
                            fsExcel.SaveExcel(saveFileDialog.FileName, ExcelSaveFlags.SaveCustomColumnHeaders);
                        }
                        Cursor = Cursors.Default;
                        MessageBox.Show("Excel模板文件导出成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        break;

                    default:
                        throw new ArgumentException("数据导入导出参数未处理。");
                }
                this.Close();
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion
    }
}
