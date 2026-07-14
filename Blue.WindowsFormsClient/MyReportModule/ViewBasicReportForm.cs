using System;
using System.IO;
using System.Threading;
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
using FarPoint.Excel;

namespace Blue.WindowsFormsClient.MyReportModule
{
    public partial class ViewBasicReportForm : Form
    {
        #region 契约接口

        private readonly ICustomCoverContract customCoverContract;
        private readonly ICustomSheetContract customSheetContract;
        private readonly ICustomCellStyleContract customCellStyleContract;
        private readonly ICustomDataFieldAndCellStyleContract dataFieldAndSheetStyleContract;
        private readonly IUserAccountContract userAccountContract;
        private readonly IAuthorizedDataContract authorizedDataContract;
        private readonly IUserTypeContract userTypeContract;
        private readonly IDepartmentContract departmentContract;
        private readonly ICustomDataFieldContract customDataFieldContract;

        #endregion

        #region 属性

        /// <summary>
        /// 用户编号
        /// </summary>
        public decimal UserId
        {
            get;
            set;
        }

        /// <summary>
        /// 报表编号
        /// </summary>
        public decimal CoverId
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        public ViewBasicReportForm()
        {
            InitializeComponent();
            customCoverContract = ReportingChannelFactory.CreateCustomCoverContract();
            customSheetContract = ReportingChannelFactory.CreateCustomSheetContract();
            customCellStyleContract = ReportingChannelFactory.CreateCustomCellStyleContract();
            dataFieldAndSheetStyleContract = ReportingChannelFactory.CreateCustomDataFieldAndCellStyleContract();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            authorizedDataContract = AudtingBusinessChannelFactory.CreateCustomDatabaseContract();
            userTypeContract = BusinessChannelFactory.CreateUserTypeContract();
            departmentContract = BusinessChannelFactory.CreateDepartmentContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();            
        }

        #endregion

        #region 窗体及控件方法

        private void ViewBasicReportForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            fsReporting.TabStripInsertTab = false;
            fsReporting.TabStripPolicy = TabStripPolicy.Always;
            byte[] data = customSheetContract.DownLoadReportFile(CoverId, ReportType.Report);
            IList<RowAndCol> rowAndCols = customSheetContract.GetRowAndColCount(CoverId, ReportType.Report);            
            IDictionary<decimal, CustomDataFieldInfo> customDataFieldInfoDic = new Dictionary<decimal, CustomDataFieldInfo>();
            if (data != null && data.Length > 0)
            {
                ReportHelper.ShowFpSpread(fsReporting, data, rowAndCols);
                SetReportingProtectedProperty(true);
                for (int i = 0; i < rowAndCols.Count; i++)
                {
                    if (rowAndCols[i].Row > 0 && rowAndCols[i].Col > 0)
                    {
                        fsReporting.Sheets[i].Cells[0, 0, rowAndCols[i].Row - 1, rowAndCols[i].Col - 1].BackColor = Color.White;
                    }
                }
            }
            this.BeginInvoke(new MethodInvoker(ShowBasicReporting));
        }

        #endregion
        
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
        private void ShowBasicReporting()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IList<RowAndCol> rowAndCols = customSheetContract.GetRowAndColCount(CoverId, ReportType.Report);
                IDictionary<decimal, CustomDataFieldInfo> customDataFieldInfoDic = new Dictionary<decimal, CustomDataFieldInfo>();
                IList<CommonNode> commonNodes = customSheetContract.GetCommonNodes(CoverId, ReportType.Report);
                int count = (fsReporting.Sheets.Count <= commonNodes.Count) ? fsReporting.Sheets.Count : commonNodes.Count;
                for (int i = 0; i < count; i++)
                {
                    fsReporting.Sheets[i].Tag = commonNodes[i];
                    IList<CustomCellStyleInfo> customCellStyleInfos = customCellStyleContract.GetCustomCellStyleInfos(commonNodes[i].NodeId);
                    /* 显示单元格数据 */
                    foreach (CustomCellStyleInfo customCellStyleInfo in customCellStyleInfos)
                    {
                        if (customCellStyleInfo.TableId <= 0)
                        {
                            continue;
                        }
                        ReportCellType reportCellType = (ReportCellType)customCellStyleInfo.CellType;
                        switch (reportCellType)
                        {
                            case ReportCellType.Single:
                                fsReporting.Sheets[i].Cells[customCellStyleInfo.Row, customCellStyleInfo.Col].Text = customCellStyleContract.GetCellText(customCellStyleInfo.CellStyleId, UserId,
                                customCellStyleInfo.TableId, customCellStyleInfo.ConditionText, customCellStyleInfo.TemplateText);
                                break;

                            case ReportCellType.ExtendRow:
                            case ReportCellType.ExtendCol:
                            case ReportCellType.ExtendRowByCondtion:
                            case ReportCellType.ExtendColByCondtion:
                                decimal id = UserId;
                                if (reportCellType == ReportCellType.ExtendRowByCondtion || reportCellType == ReportCellType.ExtendColByCondtion)
                                {
                                    id = 0;
                                }
                                DataSet ds = customCellStyleContract.GetExtendRowColData(customCellStyleInfo.CellStyleId, id,
                                customCellStyleInfo.TableId, customCellStyleInfo.ConditionText);
                                if (ds == null)
                                {
                                    continue;
                                }
                                IList<CustomDataFieldAndCellStyleInfo> customDataFieldAndCellStyleInfos = dataFieldAndSheetStyleContract.GetModeInfos(customCellStyleInfo.CellStyleId, CellCondition.Show);
                                foreach (CustomDataFieldAndCellStyleInfo customDataFieldAndCellStyleInfo in customDataFieldAndCellStyleInfos)
                                {
                                    CustomDataFieldInfo customDataFieldInfo = null;
                                    if (customDataFieldInfoDic.ContainsKey(customDataFieldAndCellStyleInfo.DataFieldId))
                                    {
                                        customDataFieldInfo = customDataFieldInfoDic[customDataFieldAndCellStyleInfo.DataFieldId];
                                    }
                                    else
                                    {
                                        if (customDataFieldAndCellStyleInfo.IsSystemSystemDataField)
                                        {
                                            customDataFieldInfo = SystemDataFieldHelper.GetSystemDataFieldInfo(customCellStyleInfo.TableId,
                                                (SystemDataField)customDataFieldAndCellStyleInfo.SystemDataFieldId);
                                        }
                                        else
                                        {
                                            customDataFieldInfo = customDataFieldContract.GetModeInfo(customDataFieldAndCellStyleInfo.DataFieldId);
                                            if (customDataFieldInfo != null)
                                            {
                                                customDataFieldInfoDic.Add(customDataFieldAndCellStyleInfo.DataFieldId, customDataFieldInfo);
                                            }
                                        }
                                    }
                                    int row = 0, col = 0, size = 0;
                                    if (reportCellType == ReportCellType.ExtendRow || reportCellType == ReportCellType.ExtendRowByCondtion)
                                    {
                                        size = customCellStyleInfo.ExtendRows;
                                    }
                                    else
                                    {
                                        size = customCellStyleInfo.ExtendCols;
                                    }
                                    int currentSpanCellRowCount = 0;
                                    for (int index = 0; index < size; index++)
                                    {
                                        switch (reportCellType)
                                        {
                                            case ReportCellType.ExtendRow:
                                            case ReportCellType.ExtendRowByCondtion:
                                                row = customCellStyleInfo.Row + currentSpanCellRowCount;
                                                col = customCellStyleInfo.Col + customDataFieldAndCellStyleInfo.Sorting;
                                                break;

                                            case ReportCellType.ExtendCol:
                                            case ReportCellType.ExtendColByCondtion:
                                                row = customCellStyleInfo.Row + customDataFieldAndCellStyleInfo.Sorting;
                                                col = customCellStyleInfo.Col + currentSpanCellRowCount;
                                                break;
                                        }
                                        object result = null;
                                        if (index < ds.Tables[0].Rows.Count)
                                        {
                                            result = ds.Tables[0].Rows[index][customDataFieldInfo.PhysicalName];
                                        }
                                        SetCellProperty(fsReporting.Sheets[i].Cells[row, col], customDataFieldInfo, result);
                                        /* 计算合并的单元格的行列 */
                                        CellRange cr = fsReporting.Sheets[i].GetSpanCell(row, col);
                                        if (cr != null)
                                        {
                                            switch (reportCellType)
                                            {
                                                case ReportCellType.ExtendRow:
                                                case ReportCellType.ExtendRowByCondtion:
                                                    currentSpanCellRowCount += cr.RowCount;
                                                    break;

                                                case ReportCellType.ExtendCol:
                                                case ReportCellType.ExtendColByCondtion:
                                                    currentSpanCellRowCount += cr.ColumnCount;
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            currentSpanCellRowCount++;
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
                WinExceptionHelper.NoExceptionAndAlertPolicy(exception);
            }
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

        private void btnItmPringSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonNode commonNode = fsReporting.ActiveSheet.Tag as CommonNode;
            if (commonNode != null)
            {
                ReportHelper.ShowPrintSettingForm(customSheetContract, commonNode.NodeId);
            }
        }

        private void btnItmPrintPdf_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CommonNode commonNode = fsReporting.ActiveSheet.Tag as CommonNode;
            if (commonNode != null)
            {
                CustomMargin customMargin = customSheetContract.GetMargin(commonNode.NodeId);
                ReportHelper.PrintPdf(fsReporting, customMargin);
            }
        }

        private void btnItmInsertPhoto_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (UserId > 0)
            {
                string userName = userAccountContract.GetUserNameByUserId(UserId);
                byte[] data = userAccountContract.DownLoadPhoto(userName);
                if (data != null && fsReporting.ActiveSheet.ActiveCell != null)
                {
                    ReportHelper.InsertPhoto(fsReporting, fsReporting.ActiveSheet.ActiveCell, data);
                }
            }
            else
            {
                MessageBox.Show("该用户没有照片，无法插入！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnItmInsertBatchNumber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IList<CommonNode> commonNodes = customSheetContract.GetCommonNodes(CoverId, ReportType.Report);
                CommonNode node = commonNodes[fsReporting.ActiveSheetIndex];
                CustomSheetInfo customSheetInfo = customSheetContract.GetModeInfo(node.NodeId);
                fsReporting.ActiveSheet.ActiveCell.CellType = new TextCellType();
                string activeCellText = fsReporting.ActiveSheet.ActiveCell.Text;
                if (!string.IsNullOrWhiteSpace(activeCellText))
                {
                    try
                    {
                        fsReporting.ActiveSheet.ActiveCell.Text = string.Format(activeCellText, customSheetInfo.BatchNumber);
                    }
                    catch
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("当前单元格的字符串格式错误，请检查！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    fsReporting.ActiveSheet.ActiveCell.Text = customSheetInfo.BatchNumber.ToString();
                }
                customSheetContract.AutoIncreaseBatchNumber(node.NodeId);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicy(exception);
            }
        }
    
        /// <summary>
        /// 设置单元格的值
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="customDataFieldInfo"></param>
        /// <param name="value"></param>
        private void SetCellProperty(FarPoint.Win.Spread.Cell cell, CustomDataFieldInfo customDataFieldInfo, object value)
        {
            DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
            switch (dataFieldProperty)
            {
                case DataFieldProperty.SystemDataField:
                    SystemDataField systemDataField = (SystemDataField)customDataFieldInfo.DataFieldId;
                    switch (systemDataField)
                    {
                        case SystemDataField.UserTypeId:
                            decimal userTypeId = DataConvertionHelper.GetConvertedDecimal(value);
                            value = userTypeContract.GetUserTypeName(userTypeId);
                            break;

                        case SystemDataField.DepID:
                            decimal depId = DataConvertionHelper.GetConvertedDecimal(value);
                            value = departmentContract.GetDepartmentName(depId);
                            break;

                        case SystemDataField.Auditing:
                            byte auditedStateId = DataConvertionHelper.GetConvertedByte(value);
                            value = EnumDescription.GetFieldText((AuditedState)auditedStateId);
                            break;
                    }
                    break;
            }
            cell.Value = value;
        }        
    }
}
