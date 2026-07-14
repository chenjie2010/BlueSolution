using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.DataFilledModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.DataFilledModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class DataTableControl : UserControl
    {
        #region 属性

        /// <summary>
        /// 
        /// </summary>
        public ICustomRoleContract CustomRoleContract
        {
            get;
            set;
        }

        /// <summary>
        /// 控件
        /// </summary>
        public DevExpressGrid DevExpressGrid
        {
            get
            {
                return devExpressGrid;
            }
        }

        /// <summary>
        /// 增加操作
        /// </summary>
        public AddHandlerDelegate AddHandler
        {
            get;
            set;
        }
        
        /// <summary>
        /// 设置权限操作
        /// </summary>
        public SetAuthorityHandlerDelegate SetAuthorityHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 编辑操作
        /// </summary>
        public EditHandlerDelegate EditHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 删除操作
        /// </summary>
        public DeleteHandlerDelegate DeleteHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 移动操作
        /// </summary>
        public MoveRecordHandlerDelegate MoveRecordHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 数据加载
        /// </summary>
        public LoadDataHandlerDelegate LoadDataHanler
        {
            get;
            set;
        }

        /// <summary>
        /// 更新记录状态
        /// </summary>
        public UpdateCurretStateDelegate UpdateCurretStateHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 刷新排序
        /// </summary>
        public RefreshSortingDelegate RefreshSortinHandler
        {
            get;
            set;
        }

        /// <summary>
        /// 设置当前状态
        /// </summary>
        public bool AllowStatusSetting
        {
            get
            {
                return hlnkCurrent.Visible;
            }
            set
            {
                hlnkCurrent.Visible = value;
                chkCopy.Visible = value;
                chkCopy.Checked = value;
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool FormReadOnly
        {
            get;
            set;
        }
        
        /// <summary>
        /// 允许导入数据
        /// </summary>
        public bool AllowDataImported
        {
            get;
            set;
        }

        /// <summary>
        /// 表的编号
        /// </summary>
        public decimal TableId
        {
            get;
            set;
        }

        /// <summary>
        /// 允许导出数据
        /// </summary>
        public bool AllowDataExported
        {
            get;
            set;
        }

        /// <summary>
        /// 当前数据行
        /// </summary>
        [
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false)
        ]
        public DataRow FocusedDataRow
        {
            get
            {
                return devExpressGrid.FocusedDataRow;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataTableControl()
        {
            InitializeComponent();
            TableId = decimal.MinValue;
        }

        #endregion

        #region 控件加载与方法

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableControl_Load(object sender, EventArgs e)
        {
            devExpressGrid.ExportedExcel = true;
            devExpressGrid.IsShowCheckBox = true;
            SetAuthority();          
        }

        /// <summary>
        /// 增加操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (chkCopy.Checked)
            {
                AddHandler?.Invoke(devExpressGrid.FocusedDataRow);              
            }
            else
            {
                AddHandler?.Invoke(null);
            }            
        }

        /// <summary>
        /// 增加操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnAddClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkCopy.Checked)
            {
                AddHandler?.Invoke(devExpressGrid.FocusedDataRow);
            }
            else
            {
                AddHandler?.Invoke(null);
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnEditClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            EditHandler?.Invoke(devExpressGrid.FocusedDataRow, FormReadOnly);
        }

        /// <summary>
        /// 行编辑事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnRowEdit(object sender, RowEvent e)
        {
            EditHandler?.Invoke(devExpressGrid.FocusedDataRow, FormReadOnly);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnDeleteClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Delete();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkDownload_HyperlinkClick(object sender, DevExpress.Utils.HyperlinkClickEventArgs e)
        {

        }

        private void hlnkTemplate_HyperlinkClick(object sender, DevExpress.Utils.HyperlinkClickEventArgs e)
        {

        }

        /// <summary>
        /// 翻页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnPageIndexChanged(object sender, AppFramework.WinFormsControls.CustomGridViewPageEventArgs e)
        {
            DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
            devExpressGrid.CurrentPageIndex = e.NewPageIndex;
            LoadDataHanler?.Invoke(devExpressGrid);
        }

        /// <summary>
        /// 排序
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnRecordSortingChanged(object sender, AppFramework.WinFormsControls.ExtendedItemClickEventArgs e)
        {
            MoveRecord(e.MovedDriection);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnImportExcel(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnImport_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 双击查看记录详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnRowDoubleClick(object sender, RowEvent e)
        {
            EditHandler?.Invoke(devExpressGrid.FocusedDataRow, true);
        }

        /// <summary>
        /// 更新主从表中的当前状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkCurrent_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("确定设置当前记录的状态吗？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
            {
                decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId)]);
                UpdateCurretStateHandler?.Invoke(recordId);
                devExpressGrid.CurrentPageIndex = 0;
                LoadDataHanler?.Invoke(devExpressGrid);
            }
        }

        /// <summary>
        /// 数据源发送变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnDataSourceChanged(object sender, EventArgs e)
        {            
            if (AuthorityHelper.CheckAuthority(devExpressGrid.Authority, Convert.ToByte(GridViewAuthority.Add)))
            {
                if (devExpressGrid.IsMainTable && devExpressGrid.RowCount > 0)
                {
                    btnAdd.Enabled = false;
                }
                else
                {
                    btnAdd.Enabled = true;
                }
            }
            else
            {
                btnAdd.Enabled = false;
            }
        }

        #endregion

        #region 公有方法        

        /// <summary>
        /// 加载数据
        /// </summary>
        public void LoadData()
        {
            LoadDataHanler?.Invoke(devExpressGrid);
        }

        /// <summary>
        /// 设置权限
        /// </summary>
        public void SetAuthority()
        {
            if (FormReadOnly)
            {
                devExpressGrid.GridViewReadOnly = true;
                devExpressGrid.IsShowCheckBox = false;
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                hlnkCurrent.Enabled = false;
                chkCopy.Properties.ReadOnly = true;
            }
            else
            {
                devExpressGrid.IsShowCheckBox = true;
                SetAuthorityHandler?.Invoke(devExpressGrid, btnAdd, btnDelete);
            }            
            btnImport.Visible = AllowDataImported;
            hlnkTemplate.Visible = AllowDataImported;
            hlnkDownload.Visible = AllowDataExported;
            hlnkCurrent.Visible = !devExpressGrid.IsMainTable && AllowStatusSetting;
            chkCopy.Visible = !devExpressGrid.IsMainTable;
        }
        #endregion

        #region 私有方法        

        /// <summary>
        /// 移动记录
        /// </summary>
        /// <param name="movedDriection"></param>
        private void MoveRecord(MovedDriection movedDriection)
        {
            if (TableId <= 0) return;
            decimal userId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.UserId)]);
            decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId)]);
            MoveRecordHandler?.Invoke(userId, TableId, recordId, movedDriection);
            int focusedRowHandle = devExpressGrid.FocusedRowHandle;
            switch (movedDriection)
            {
                case MovedDriection.Top:
                    devExpressGrid.CurrentPageIndex = 0;
                    focusedRowHandle = 0;
                    break;

                case MovedDriection.Previous:
                    if (devExpressGrid.FocusedRowHandle == 0 && devExpressGrid.CurrentPageIndex > 0)
                    {
                        devExpressGrid.CurrentPageIndex -= 1;
                        focusedRowHandle = devExpressGrid.PageSize - 1;
                    }
                    else
                    {
                        focusedRowHandle = devExpressGrid.FocusedRowHandle - 1;
                    }
                    break;

                case MovedDriection.Next:
                    if ((devExpressGrid.FocusedRowHandle == devExpressGrid.CurrentPageSize - 1)
                        && devExpressGrid.CurrentPageIndex < (devExpressGrid.PageCount - 1))
                    {
                        devExpressGrid.CurrentPageIndex += 1;
                        focusedRowHandle = 0;

                    }
                    else
                    {
                        focusedRowHandle = devExpressGrid.FocusedRowHandle + 1;
                    }
                    break;

                case MovedDriection.Bottom:
                    devExpressGrid.CurrentPageIndex = devExpressGrid.PageCount - 1;
                    focusedRowHandle = devExpressGrid.CurrentPageSize - 1;
                    break;
            }
            LoadDataHanler?.Invoke(devExpressGrid);
            devExpressGrid.FocusedRowHandle = focusedRowHandle;
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_OnRefresh(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RefreshSortinHandler?.Invoke();
            LoadDataHanler?.Invoke(devExpressGrid);
        }

        /// <summary>
        /// 单元格颜色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressGrid_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            DevExpressGrid devExpressGrid = (DevExpressGrid)sender;
            string auditedStatusName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.AuditedStatus);
            if (e.RowHandle >= 0)
            {
                AuditedStatus auditedStatus = AuditedStatus.None;
                if (e.Column.FieldName.Equals(auditedStatusName))
                {
                    auditedStatus = (AuditedStatus)Convert.ToByte(e.CellValue);
                    switch (auditedStatus)
                    {
                        case AuditedStatus.Auditing:
                            e.Appearance.ForeColor = Color.DarkGray;
                            break;

                        case AuditedStatus.Audited:
                            e.Appearance.ForeColor = Color.LightGray;
                            break;
                    }

                }
                else if (e.Column.FieldName.Equals(devExpressGrid.CheckboxColumnName) && devExpressGrid.Columns.ColumnByFieldName(auditedStatusName) != null)
                {
                    auditedStatus = (AuditedStatus)Convert.ToByte(devExpressGrid.GetRowCellValue(e.RowHandle, auditedStatusName));
                    switch (auditedStatus)
                    {
                        case AuditedStatus.Auditing:
                            e.Appearance.BackColor = Color.DarkGray;
                            break;

                        case AuditedStatus.Audited:
                            e.Appearance.BackColor = Color.LightGray;
                            break;
                    }
                }
                else if (e.Column.Name.Equals(devExpressGrid.CustomEditedName) && devExpressGrid.Columns.ColumnByFieldName(auditedStatusName) != null)
                {
                    auditedStatus = (AuditedStatus)Convert.ToByte(devExpressGrid.GetRowCellValue(e.RowHandle, auditedStatusName));
                    switch (auditedStatus)
                    {
                        case AuditedStatus.Auditing:
                            e.Appearance.BackColor = Color.DarkGray;
                            break;

                        case AuditedStatus.Audited:
                            e.Appearance.BackColor = Color.LightGray;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        private void Delete()
        {
            try
            {
                if (MessageBox.Show("确认删除该记录么？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    IList<decimal> recordIds = new List<decimal>();
                    if (devExpressGrid.MultiSelectedValues.Count > 0)
                    {
                        foreach (RowEvent rowEvent in devExpressGrid.MultiSelectedValues)
                        {
                            recordIds.Add(DataConvertionHelper.GetDecimal(rowEvent.Value));
                        }
                    }
                    else
                    {
                        decimal recordId = DataConvertionHelper.GetDecimal(devExpressGrid.DataKeyValues[DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId)]);
                        recordIds.Add(recordId);
                    }
                    DeleteHandler?.Invoke(recordIds);
                    if (devExpressGrid.RowCount <= recordIds.Count)
                    {
                        devExpressGrid.CurrentPageIndex = devExpressGrid.CurrentPageIndex > 0 ? devExpressGrid.CurrentPageIndex - 1 : 0;
                    }
                    LoadDataHanler?.Invoke(devExpressGrid);
                }
            }
            catch (Exception exception)
            {
                //记录日志, 抛出异常, 不包装异常
                ExceptionHelper.NotifyRethrowNoWrapPolicy(exception);
            }
        }
        
        #endregion        
    }
}
