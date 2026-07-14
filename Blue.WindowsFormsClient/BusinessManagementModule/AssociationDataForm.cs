using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class AssociationDataForm : Form
    {
        #region 私有常量

        /// <summary>
        /// 实数型对应的字符串最大长度
        /// </summary>
        private const int SYSTEM_MAX_LEN_OF_DECIMAL = 12;

        /// <summary>
        /// 整型对应的字符串最大长度
        /// </summary>
        private const int SYSTEM_MAX_LEN_OF_INT32 = 10;

        /// <summary>
        /// 标签字符的最大长度
        /// </summary>
        private const int SYSTEM_ASSOCIATED_MAX_LEN_OF_LABEL_TEXT = 6;

        /// <summary>
        /// 标签控件的水平方向初始位置
        /// </summary>
        private const int SYSTEM_ASSOCIATED_H_INIT_POS_OF_LABEL = 90;

        /// <summary>
        /// 控件的水平方向初始位置
        /// </summary>
        private const int SYSTEM_ASSOCIATED_H_INIT_POS_OF_CONTROL = 105;

        /// <summary>
        /// 标签中每增加一个字，则位置向左移动的长须
        /// </summary>
        private const int SYSTEM_ASSOCIATED_DEC_LEN_OF_LABEL = 12;

        /// <summary>
        /// 控件的垂直方向初始位置
        /// </summary>
        private const int SYSTEM_ASSOCIATED_V_INIT_POS_OF_CONTROL = 10;

        /// <summary>
        /// 标签控件的垂直方向初始位置
        /// </summary>
        private const int SYSTEM_ASSOCIATED_V_INIT_POS_OF_LABEL = 12;

        /// <summary>
        /// 提示的垂直方向初始位置
        /// </summary>
        private const int SYSTEM_ASSOCIATED_V_INIT_POS_OF_TOOLTIP = 14;

        /// <summary>
        /// 控件的垂直方向的步长
        /// </summary>
        private const int SYSTEM_ASSOCIATED_V_INC_LEN = 30;

        #endregion

        #region 私有变量

        private EditState currentEditState = EditState.None;

        #endregion

        #region 契约接口

        private readonly ICustomAssociationContract customAssociationContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;

        #endregion

        #region 属性

        /// <summary>
        /// 关联名称编号
        /// </summary>
        public decimal AssociationId
        {
            get;
            set;
        }


        #endregion

        #region 窗体和控件的方法 

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssociationDataForm()
        {
            InitializeComponent();
            customAssociationContract = BusinessChannelFactory.CreateCustomAssociationContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationDataForm_Load(object sender, EventArgs e)
        {
            LoadControls();
            LoadData();
            SetActiveStatesOfControls(true);
            SetDataOnControls();
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                IList<CommonDataField> commonDataFields = new List<CommonDataField>();
                foreach (Control control in xscDetail.Controls)
                {
                    if (control.Tag == null)
                    {
                        continue;
                    }

                    AssociatedDataFieldInfo associatedDataFieldInfo = (AssociatedDataFieldInfo)control.Tag;
                    BasedDataType basedDatType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                    object dataFieldValue = null;
                    DbType dataFieldDataType = DbType.String;
                    switch (basedDatType)
                    {
                        case BasedDataType.Boolean:
                            dataFieldValue = ((CheckEdit)control).Checked;
                            dataFieldDataType = DbType.Boolean;
                            break;

                        case BasedDataType.Decimal:
                            string textDecimal = ((TextEdit)control).Text.Trim();
                            if (string.IsNullOrEmpty(textDecimal))
                            {
                                control.Focus();
                                MessageBox.Show(string.Format("{0}不能为空。", associatedDataFieldInfo.LogicalName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                //实数 
                                Regex r = new Regex(@"^(-?\d+)(\.\d+)?$");
                                if (!r.IsMatch(textDecimal))
                                {
                                    control.Focus();
                                    MessageBox.Show(string.Format("{0}不能为非实数。", associatedDataFieldInfo.LogicalName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                //超过范围转换失败                                
                                if (DataConvertionHelper.IsNullValue(DataConvertionHelper.GetConvertedDecimal(textDecimal)))
                                {
                                    control.Focus();
                                    MessageBox.Show(string.Format("{0}的整数值的超出了实数限制范围。", associatedDataFieldInfo.LogicalName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                dataFieldValue = DataConvertionHelper.GetConvertedDecimal(textDecimal);
                                dataFieldDataType = DbType.Decimal;
                            }
                            break;

                        case BasedDataType.Int32:
                            string textInt32 = ((TextEdit)control).Text.Trim();
                            if (string.IsNullOrEmpty(textInt32))
                            {
                                control.Focus();
                                MessageBox.Show(string.Format("{0}不能为空。", associatedDataFieldInfo.LogicalName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            else
                            {
                                //整数 
                                Regex r = new Regex(@"^-?\d+$");
                                if (!r.IsMatch(textInt32))
                                {
                                    control.Focus();
                                    MessageBox.Show(string.Format("{0}不能为非整数。", associatedDataFieldInfo.LogicalName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                //超过范围转换失败，得到的默认值为 Int32.MinValue                                
                                if (DataConvertionHelper.IsNullValue(DataConvertionHelper.GetConvertedInt(textInt32)))
                                {
                                    control.Focus();
                                    MessageBox.Show(string.Format("{0}的整数值的超出了整数限制范围。", associatedDataFieldInfo.LogicalName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                                dataFieldValue = DataConvertionHelper.GetConvertedInt(textInt32);
                                dataFieldDataType = DbType.Int32;
                            }
                            break;

                        case BasedDataType.String:
                            string associatedString = ((TextEdit)control).Text.Trim();
                            if (string.IsNullOrEmpty(associatedString))
                            {
                                control.Focus();
                                MessageBox.Show(string.Format("{0}不能为空。", associatedDataFieldInfo.LogicalName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            dataFieldValue = associatedString;
                            dataFieldDataType = DbType.String;
                            break;

                        case BasedDataType.DateTime:
                            if (((DateEdit)control).DateTime == DateTime.MinValue)
                            {
                                control.Focus();
                                MessageBox.Show(string.Format("{0}不能为空。", associatedDataFieldInfo.LogicalName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            dataFieldValue = ((DateEdit)control).DateTime;
                            dataFieldDataType = DbType.DateTime;
                            break;
                    }
                    commonDataFields.Add(new CommonDataField(associatedDataFieldInfo.PhysicalName, dataFieldValue, dataFieldDataType));
                }
                if (commonDataFields.Count == 0)
                {
                    MessageBox.Show("请先设计关联表的字段。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                switch (currentEditState)
                {
                    case EditState.Add:
                        Cursor = Cursors.WaitCursor;
                        decimal recordId = customAssociationContract.Insert(AssociationId, commonDataFields);
                        LoadData();                        
                        gvMain.FocusedRowHandle = gvMain.DataRowCount - 1;                                           
                        SetActiveStatesOfControls(true);
                        Cursor = Cursors.Default;                        
                        break;

                    case EditState.Edit:
                        Cursor = Cursors.WaitCursor;
                        DataRow editDataRow = gvMain.GetFocusedDataRow();
                        if (editDataRow == null)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show("请选择行记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        customAssociationContract.Update(AssociationId, DataConvertionHelper.GetDecimal(editDataRow["RecordId"]), commonDataFields);
                        foreach (CommonDataField commonDataField in commonDataFields)
                        {
                            editDataRow[commonDataField.DataFieldName] = commonDataField.DataFieldValue;
                        }
                        SetActiveStatesOfControls(true);
                        Cursor = Cursors.Default;                        
                        break;

                    default:
                        break;
                }
                currentEditState = EditState.None;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            SetActiveStatesOfControls(true);
            SetDataOnControls();
        }       

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiAdd_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentEditState = EditState.Add;
            ClearDataOnControls();
            SetActiveStatesOfControls(false);
            foreach (Control control in xscDetail.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                control.Focus();
                break;
            }
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            currentEditState = EditState.Edit;
            SetActiveStatesOfControls(false);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            try
            {
                if (MessageBox.Show("确定删除所选择的行记录？", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                {
                    DataRow editDataRow = gvMain.GetFocusedDataRow();
                    int focusedRowHandle = gvMain.FocusedRowHandle;
                    if (editDataRow == null)
                    {
                        MessageBox.Show("请选择行记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    Cursor = Cursors.WaitCursor;
                    customAssociationContract.Delete(AssociationId, DataConvertionHelper.GetDecimal(editDataRow["RecordId"]));
                    gvMain.DeleteRow(focusedRowHandle);
                    if (focusedRowHandle > 0)
                    {
                        gvMain.FocusedRowHandle = focusedRowHandle - 1;
                        SetDataOnControls();
                    }
                    Cursor = Cursors.Default;
                    MessageBox.Show("删除成功。", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// Tab键操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociationDataForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Control control = this.GetNextControl(this.ActiveControl, true);
                if (control != null)
                {
                    control.Select();
                }
            }
        }

        /// <summary>
        /// 行发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            SetDataOnControls();
        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvMain_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
                private void bbiClose_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 置顶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiTop_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateRecordSorting(MovedDriection.Top);
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiPrevious_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateRecordSorting(MovedDriection.Previous);
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiNext_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateRecordSorting(MovedDriection.Next);
        }

        /// <summary>
        /// 置底
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiBottom_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            UpdateRecordSorting(MovedDriection.Bottom);
        }

        #endregion

        #region 私有方法 

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="movedDriection"></param>
        private void UpdateRecordSorting(MovedDriection movedDriection)
        {
            DataRow editDataRow = gvMain.GetFocusedDataRow();
            DataTable dataTable = gcTable.DataSource as DataTable;
            int focusedRowHandle = gvMain.FocusedRowHandle;
            if (editDataRow == null)
            {
                MessageBox.Show("请选择行记录。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                decimal recordId = DataConvertionHelper.GetDecimal(editDataRow["RecordId"]);
                switch (movedDriection)
                {
                    case MovedDriection.Top:
                        focusedRowHandle = 0;
                        break;

                    case MovedDriection.Previous:
                        if (focusedRowHandle > 0)
                        {
                            focusedRowHandle--;
                        }
                        break;

                    case MovedDriection.Next:
                        if (dataTable != null && focusedRowHandle < (dataTable.Rows.Count - 1))
                        {
                            focusedRowHandle++;
                        }
                        break;

                    case MovedDriection.Bottom:
                        if (dataTable != null)
                        {
                            focusedRowHandle = dataTable.Rows.Count - 1;
                        }
                        break;
                }
                customAssociationContract.UpdateRecordSorting(AssociationId, recordId, movedDriection);
                LoadData();
                if (focusedRowHandle > 0)
                {
                    gvMain.FocusedRowHandle = focusedRowHandle;
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 赋值
        /// </summary>
        private void SetDataOnControls()
        {
            DataRow dataRow = gvMain.GetFocusedDataRow();
            if (dataRow != null)
            {
                foreach (Control control in xscDetail.Controls)
                {
                    if (control.Tag == null)
                    {
                        continue;
                    }
                    AssociatedDataFieldInfo associatedDataFieldInfo = (AssociatedDataFieldInfo)control.Tag;
                    BasedDataType basedDatType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                    switch (basedDatType)
                    {
                        case BasedDataType.Boolean:
                            ((CheckEdit)control).Checked = DataConvertionHelper.GetBoolean(dataRow[associatedDataFieldInfo.PhysicalName]);
                            break;

                        case BasedDataType.Decimal:
                        case BasedDataType.Int32:
                        case BasedDataType.String:
                            ((TextEdit)control).Text = DataConvertionHelper.GetConvertedString(dataRow[associatedDataFieldInfo.PhysicalName]);
                            break;

                        case BasedDataType.DateTime:
                            ((DateEdit)control).DateTime = DataConvertionHelper.GetDateTime(dataRow[associatedDataFieldInfo.PhysicalName]); ;
                            break;
                    }
                }
            };
        }

        /// <summary>
        /// 清除控件的内容
        /// </summary>
        private void ClearDataOnControls()
        {
            foreach (Control control in xscDetail.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                AssociatedDataFieldInfo associatedDataFieldInfo = (AssociatedDataFieldInfo)control.Tag;
                BasedDataType basedDatType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                switch (basedDatType)
                {
                    case BasedDataType.Boolean:
                        ((CheckEdit)control).Checked = false;
                        break;

                    case BasedDataType.Decimal:
                    case BasedDataType.Int32:
                    case BasedDataType.String:
                        ((TextEdit)control).Text = string.Empty;
                        break;

                    case BasedDataType.DateTime:
                        ((DateEdit)control).EditValue = null;
                        break;
                }
            }
        }

        /// <summary>
        /// 设置控件的状态
        /// </summary>
        /// <param name="readOnly"></param>
        private void SetActiveStatesOfControls(bool readOnly)
        {
            foreach (Control control in xscDetail.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                AssociatedDataFieldInfo associatedDataFieldInfo = (AssociatedDataFieldInfo)control.Tag;
                BasedDataType basedDatType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                switch (basedDatType)
                {
                    case BasedDataType.Boolean:                    
                        ((CheckEdit)control).Properties.ReadOnly = readOnly;
                        break;

                    case BasedDataType.Decimal:
                    case BasedDataType.Int32:
                    case BasedDataType.String:
                        ((TextEdit)control).Properties.ReadOnly = readOnly;
                        break;

                    case BasedDataType.DateTime:
                        ((DateEdit)control).Properties.ReadOnly = readOnly;
                        break;
                }
            }
            btnConfirm.Enabled = !readOnly;
            btnCancel.Enabled = !readOnly;
        }

        /// <summary>
        /// 加载控件
        /// </summary>
        private void LoadControls()
        {
            CustomAssociationInfo customAssociationInfo = customAssociationContract.GetModelInfo(AssociationId);
            IList<AssociatedDataFieldInfo> associatedDataFieldInfos = associatedDataFieldContract.GetModelInfos(AssociationId);

            int tabIndex = 0;
            int labelVerticalPos = SYSTEM_ASSOCIATED_V_INIT_POS_OF_LABEL;
            int controlVerticalPos = SYSTEM_ASSOCIATED_V_INIT_POS_OF_CONTROL;
            int tooltipVerticalPos = SYSTEM_ASSOCIATED_V_INIT_POS_OF_TOOLTIP;
            foreach (AssociatedDataFieldInfo associatedDataFieldInfo in associatedDataFieldInfos)
            {
                /* 1. 创建标签控件 */
                LabelControl labelControl = new LabelControl();
                if (associatedDataFieldInfo.LogicalName.Length > SYSTEM_ASSOCIATED_MAX_LEN_OF_LABEL_TEXT)
                {
                    labelControl.Text = string.Format("{0}...：", associatedDataFieldInfo.LogicalName.Substring(0, SYSTEM_ASSOCIATED_MAX_LEN_OF_LABEL_TEXT));
                }
                else
                {
                    labelControl.Text = string.Format("{0}：", associatedDataFieldInfo.LogicalName);
                }
                labelControl.Location = new Point(SYSTEM_ASSOCIATED_H_INIT_POS_OF_LABEL - associatedDataFieldInfo.LogicalName.Length * SYSTEM_ASSOCIATED_DEC_LEN_OF_LABEL, labelVerticalPos);
                labelControl.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
                labelControl.TabIndex = 1025;
                xscDetail.Controls.Add(labelControl);

                /* 2. 创建内容控件 */
                BasedDataType basedDatType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                int width = DataFieldHelper.GetControlWidth(basedDatType);
                switch (basedDatType)
                {
                    case BasedDataType.Boolean:
                        CheckEdit checkEdit = new CheckEdit();
                        checkEdit.Properties.Caption = string.Empty;
                        checkEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                        checkEdit.Properties.LookAndFeel.SkinName = "Money Twins";
                        checkEdit.Location = new Point(SYSTEM_ASSOCIATED_H_INIT_POS_OF_CONTROL, controlVerticalPos);
                        checkEdit.TabIndex = tabIndex;
                        checkEdit.Tag = associatedDataFieldInfo;
                        xscDetail.Controls.Add(checkEdit);
                        break;

                    case BasedDataType.Int32:
                        TextEdit textEditInt32 = new TextEdit();
                        textEditInt32.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                        textEditInt32.Properties.LookAndFeel.SkinName = "Money Twins";
                        textEditInt32.Location = new Point(SYSTEM_ASSOCIATED_H_INIT_POS_OF_CONTROL, controlVerticalPos);
                        textEditInt32.Width = width;
                        textEditInt32.TabIndex = tabIndex;
                        textEditInt32.Properties.MaxLength = SYSTEM_MAX_LEN_OF_INT32;
                        textEditInt32.Tag = associatedDataFieldInfo;
                        xscDetail.Controls.Add(textEditInt32);
                        break;

                    case BasedDataType.Decimal:
                        TextEdit textEditDecimal = new TextEdit();
                        textEditDecimal.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                        textEditDecimal.Properties.LookAndFeel.SkinName = "Money Twins";
                        textEditDecimal.Location = new Point(SYSTEM_ASSOCIATED_H_INIT_POS_OF_CONTROL, controlVerticalPos);
                        textEditDecimal.Width = width;
                        textEditDecimal.TabIndex = tabIndex;
                        textEditDecimal.Properties.MaxLength = SYSTEM_MAX_LEN_OF_DECIMAL;
                        textEditDecimal.Tag = associatedDataFieldInfo;
                        xscDetail.Controls.Add(textEditDecimal);
                        break;

                    case BasedDataType.DateTime:
                        DateEdit dateEdit = new DateEdit();
                        dateEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                        dateEdit.Properties.LookAndFeel.SkinName = "Blue";
                        dateEdit.Properties.DisplayFormat.FormatString = "d";
                        dateEdit.Properties.EditFormat.FormatString = "d";
                        dateEdit.Properties.EditMask = "d";
                        dateEdit.Location = new Point(SYSTEM_ASSOCIATED_H_INIT_POS_OF_CONTROL, controlVerticalPos);
                        dateEdit.Width = width;
                        dateEdit.EditValue = null;
                        dateEdit.TabIndex = tabIndex;
                        dateEdit.Tag = associatedDataFieldInfo;
                        xscDetail.Controls.Add(dateEdit);
                        break;

                    case BasedDataType.String:
                        TextEdit textEditString = new TextEdit();
                        textEditString.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                        textEditString.Properties.LookAndFeel.SkinName = "Money Twins";
                        textEditString.Location = new Point(SYSTEM_ASSOCIATED_H_INIT_POS_OF_CONTROL, controlVerticalPos);
                        textEditString.Width = width;
                        textEditString.TabIndex = tabIndex;
                        textEditString.Properties.MaxLength = associatedDataFieldInfo.DataLength;
                        textEditString.Tag = associatedDataFieldInfo;
                        xscDetail.Controls.Add(textEditString);
                        break;
                }

                /* 3. 计算控件的垂直方向的位置 */
                controlVerticalPos += SYSTEM_ASSOCIATED_V_INC_LEN;
                labelVerticalPos += SYSTEM_ASSOCIATED_V_INC_LEN;
                tooltipVerticalPos += SYSTEM_ASSOCIATED_V_INC_LEN;
                tabIndex++;
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void LoadData()
        {
            DataTable data = customAssociationContract.GetAssociationData(AssociationId);
            if (data != null)
            {
                gcTable.DataSource = data;
                gvMain.Columns[0].Visible = false;
                foreach (Control control in xscDetail.Controls)
                {
                    if (control.Tag == null)
                    {
                        continue;
                    }
                    AssociatedDataFieldInfo associatedDataFieldInfo = (AssociatedDataFieldInfo)control.Tag;
                    BasedDataType basedDatType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                    if ((BasedDataType)associatedDataFieldInfo.BasedDataType == BasedDataType.DateTime)
                    {
                        GridColumn gidColumn = gvMain.Columns.ColumnByFieldName(associatedDataFieldInfo.PhysicalName);
                        if (gidColumn != null)
                        {
                            gidColumn.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
                            gidColumn.DisplayFormat.FormatString = "yyyy-MM-dd";
                        }
                    }
                }
            }
        }

        #endregion
      
    }
}
