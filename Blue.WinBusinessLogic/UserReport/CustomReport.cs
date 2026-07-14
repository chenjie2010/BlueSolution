using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using FarPoint.Win.Spread;
using FarPoint.Win.Spread.Design;
using FarPoint.Win.Spread.CellType;
using FarPoint.Win.Spread.Model;
using FarPoint.Excel;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.SharedInterface.UserReport;
using Blue.CommonBusinessLogic.UserReport;
using Blue.WinBusinessLogic;
using Blue.Model.BusinessDesignerModule;
using Blue.Model.BusinessModule;

namespace Blue.WinBusinessLogic.UserReport
{
    /// <summary>
    /// 报表下载
    /// </summary>
    public class CustomReport: CustomReportBase, ICustomReport
    {
        #region 契约接口

        private readonly ICustomReportContract customReportContract;
        private readonly ICustomSheetContract customSheetContract;
        private readonly ICustomCellContract customCellContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly ISystemConfigContract systemConfigContract;

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


        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="customReportContract"></param>
        /// <param name="customSheetContract"></param>
        /// <param name="customCellContract"></param>
        /// <param name="customTableContract"></param>
        /// <param name="customDataFieldContract"></param>
        /// <param name="systemConfigContract"></param>
        public CustomReport(ICustomReportContract customReportContract, ICustomSheetContract customSheetContract, ICustomCellContract customCellContract,
            ICustomTableContract customTableContract, ICustomDataFieldContract customDataFieldContract, ISystemConfigContract systemConfigContract)
        {
            this.customReportContract = customReportContract;
            this.customSheetContract = customSheetContract;
            this.customCellContract = customCellContract;
            this.customTableContract = customTableContract;
            this.customDataFieldContract = customDataFieldContract;
            this.systemConfigContract = systemConfigContract;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 生成Excel报表
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="userId"></param>
        public void GenerateExcel(decimal reportId, decimal userId)
        {
            using (FpSpread fsReporting = new FpSpread())
            {
                byte[] data = null;
                if (reportingData.ContainsKey(reportId))
                {
                    data = reportingData[reportId];
                }
                else
                {
                    data = customSheetContract.DownloadReportFile(reportId);
                    reportingData.Add(reportId, data);
                }
                IList<RowAndCol> rowAndCols = customSheetContract.GetRowAndColCount(reportId);
                if (data != null && data.Length > 0)
                {
                    ReportHelper.ShowFpSpread(fsReporting, data, rowAndCols);
                    SetReportingProtectedProperty(fsReporting, true);
                    IList<CommonNode> commonNodes = customSheetContract.GetChildNodes(reportId);
                    int count = (fsReporting.Sheets.Count <= commonNodes.Count) ? fsReporting.Sheets.Count : commonNodes.Count;
                    for (int i = 0; i < count; i++)
                    {
                        fsReporting.Sheets[i].Tag = commonNodes[i];
                    }
                   CustomReportInfo customReportInfo =  customReportContract.GetModelInfo(reportId);
                    ReportCategory reportCategory = (ReportCategory)customReportInfo.ReportCategory;
                    if (reportCategory == ReportCategory.Query)
                    {
                        QueryReportType reportType = (QueryReportType)customReportInfo.ReportType;
                        switch (reportType)
                        {
                            case QueryReportType.Basic:
                                ShowBasicReporting(reportId, userId, fsReporting, customReportInfo.ReportName);
                                break;

                            case QueryReportType.Statistics:
                                break;

                            default:
                                throw new ArgumentNullException("不支持该报表枚举类型。");
                        }
                    }                    
                }
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 显示基本报表
        /// </summary>
        /// <param name="reportId"></param>
        /// <param name="userId"></param>
        /// <param name="fsReporting"></param>
        /// <param name="reportName"></param>
        private void ShowBasicReporting(decimal reportId, decimal userId, FpSpread fsReporting, string reportName)
        {
            try
            {
                IList<CommonNode> commonNodes = customSheetContract.GetChildNodes(reportId);
                Dictionary<decimal, CustomDataFieldInfo> customDataFieldInfoDic = new Dictionary<decimal, CustomDataFieldInfo>();
                Dictionary<decimal, string> dicTablePhysicalNames = new Dictionary<decimal, string>();
                int count = (fsReporting.Sheets.Count <= commonNodes.Count) ? fsReporting.Sheets.Count : commonNodes.Count;
                ManualResetEvent manualResetEvent = new ManualResetEvent(false);
                int threadCount = 0;
                for (int idx = 0; idx < count; idx++)
                {
                    ThreadPool.QueueUserWorkItem(new WaitCallback((state) =>
                    {
                        try
                        {
                            int sheetIndex = Convert.ToInt32(state);
                            IList<CustomCellInfo> customCellInfos = customCellContract.GetModelInfos(commonNodes[sheetIndex].NodeId);
                            if (customCellInfos.Count > 0)
                            {
                                for (int rowIndex = 0; rowIndex < customCellInfos.Count; rowIndex++)
                                {

                                    SheetView sheetView = fsReporting.Sheets[sheetIndex];
                                    CustomCellInfo customCellInfo = customCellInfos[rowIndex];
                                    if (customCellInfo.TableId <= 0)
                                    {
                                        continue;
                                    }
                                    BasicCellType basicCellType = (BasicCellType)customCellInfo.CellType;
                                    switch (basicCellType)
                                    {
                                        case BasicCellType.OnlyData:
                                            string cellText = customCellContract.GetCellText(customCellInfo.CellId, userId, customCellInfo.TableId,
                                                customCellInfo.ConditionText, customCellInfo.TemplateText);
                                            sheetView.Cells[customCellInfo.RowIndex, customCellInfo.ColIndex].Text = cellText;
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
                                                    if (index < ds.Tables[0].Rows.Count)
                                                    {
                                                        object result = ds.Tables[0].Rows[index][customDataFieldInfo.PhysicalName];
                                                        sheetView.Cells[row, col].Text = SetCellProperty(customDataFieldInfo, result);
                                                    }
                                                    if (index == 0)
                                                    {
                                                        /* 计算合并的单元格的行列 */
                                                        CellRange cr = sheetView.GetSpanCell(row, col);
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
                        catch { }
                        finally
                        {
                            if (++threadCount == count)
                            {
                                manualResetEvent.Set();
                            }
                        }
                    }), idx);
                }
                manualResetEvent.WaitOne();
                string fileName = string.Format("{0}_{1:yyyyMMddHHmmssffff}", reportName, DateTime.Now);
                ReportHelper.ExprotExcel(fsReporting, fileName);
            }
            catch (Exception exception)
            {
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }        
            
        /// <summary>
        /// 设置保护属性
        /// </summary>
        /// <param name="fsReporting"></param>
        /// <param name="protect"></param>
        private void SetReportingProtectedProperty(FpSpread fsReporting, bool protect)
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
                            IList<EnumItem> enumItems = null;
                            string departmentPorperty = systemConfigContract.GetSystemConfigValue(SystemConfigKeyName.DepartmentProperty);
                            if (string.IsNullOrWhiteSpace(departmentPorperty))
                            {
                                enumItems = UserEnumHelper.GetEnumItems(typeof(DepartmentProperty));
                            }
                            else
                            {
                                enumItems = GetEnumItems(departmentPorperty);
                            }

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
        /// 通过自定义字符串获取枚举定义
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private IList<EnumItem> GetEnumItems(string value)
        {
            IList<EnumItem> enumItems = new List<EnumItem>();

            try
            {
                string[] departmentPorperties = value.Split('|');
                for (int i = 0; i < departmentPorperties.Length; i++)
                {
                    string[] keyAndValues = departmentPorperties[i].Split(',');
                    enumItems.Add(new EnumItem(keyAndValues[0].Trim(), Convert.ToByte(keyAndValues[1])));
                }
            }
            catch
            {
                throw new ArgumentException("系统枚举定义配置错误。");
            }

            return enumItems;
        }
        #endregion
    }
}
