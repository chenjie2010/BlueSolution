using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Columns;
using DevExpress.Utils;
using AppFramework.Core;
using AppFramework.Reference.CustomLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.UserModule;

namespace Blue.WindowsFormsClient.Common
{
    public partial class DataComparisonForm : Form
    {
        #region 私有常量

        /// <summary>
        ///控件边沿的距离
        /// </summary>
        private const int COMMON_WIDTH_OF_MARGIN_SPACE = 3;

        /// <summary>
        /// 每行之间的间隙距离
        /// </summary>
        private const int COMMON_HEIGHT_OF_SPACE = 10;

        /// <summary>
        /// 控件宽度
        /// </summary>
        private const int COMMON_WIDTH_OF_CONTROL = 260;

        /// <summary>
        /// 控件高度
        /// </summary>
        private const int COMMON_HEIGHT_OF_CONTROL = 20;

        /// <summary>
        /// 标签字符的最大长度（双列）
        /// </summary>
        private const int COMMON_MAX_LEN_OF_LABEL_TEXT_MIDDLE = 12;

        /// <summary>
        /// 标签的宽度（双列）
        /// </summary>
        private const int COMMON_WIDTH_OF_LABEL_TEXT_MIDDLE = 180;        

        #endregion

        #region 契约接口

        private readonly IUserAccountContract userAccountContract;
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
        /// 左侧提示
        /// </summary>
        public string LeftText
        {
            get { return gcSource.Text; }
            set { gcSource.Text = value; }
        }

        /// <summary>
        /// 右侧提示
        /// </summary>
        public string RightText
        {
            get { return gcDest.Text; }
            set { gcDest.Text = value; }
        }        
        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataComparisonForm()
        {
            InitializeComponent();
            userAccountContract = UserChannelFactory.CreateUserAccount();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 控件加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataComparisonForm_Load(object sender, EventArgs e)
        {
            CommonUserInfo commonUserInfo = userAccountContract.GetCommonUserInfo(UserId);
            if (commonUserInfo != null)
            {
                txtUserName.Text = commonUserInfo.UserName;
                txtUserActualName.Text = commonUserInfo.UserActualName;
                txtDepName.Text = commonUserInfo.DepName;
                txtUserTypeName.Text = commonUserInfo.UserTypeName;
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="dataTable"></param>
        public void LoadTableData(DataTable dataTable)
        {
            foreach (Control control in xscPanel.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
                if (dataTable.Columns.Contains(extendedCustomDataFieldInfo.PhysicalName))
                {
                    if (control.AccessibleDescription == "0")
                    {
                        if (dataTable.Rows.Count > 0)
                        {
                            object obj = dataTable.Rows[0][extendedCustomDataFieldInfo.PhysicalName];
                            LoadDataOnControl(control, extendedCustomDataFieldInfo, obj);
                        }
                    }
                    else
                    {
                        if (dataTable.Rows.Count > 1)
                        {
                            object obj = dataTable.Rows[1][extendedCustomDataFieldInfo.PhysicalName];
                            LoadDataOnControl(control, extendedCustomDataFieldInfo, obj);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="dataRowItems"></param>
        public void LoadCombinedTableData(Dictionary<decimal, DataRowItem> dataRowItems)
        {
            foreach (Control control in xscPanel.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
                if (dataRowItems.ContainsKey(extendedCustomDataFieldInfo.TableId))
                {
                    DataRowItem dataRowItem = dataRowItems[extendedCustomDataFieldInfo.TableId];
                    if (control.AccessibleDescription == "0")
                    {
                        if (dataRowItem.SourceRow != null && dataRowItem.SourceRow.Rows.Count > 0)
                        {
                            object obj = dataRowItem.SourceRow.Rows[0][extendedCustomDataFieldInfo.PhysicalName];
                            LoadDataOnControl(control, extendedCustomDataFieldInfo, obj);
                        }
                    }
                    else
                    {
                        if (dataRowItem.TragetRow != null && dataRowItem.TragetRow.Rows.Count > 0)
                        {
                            object obj = dataRowItem.TragetRow.Rows[0][extendedCustomDataFieldInfo.PhysicalName];
                            LoadDataOnControl(control, extendedCustomDataFieldInfo, obj);
                        }
                    }
                }
            }
        }

        /// <summary>
        ///  创建控件
        /// </summary>
        /// <param name="extendedCustomDataFieldInfos"></param>
        public void CreateControls(IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos)
        {
            /* 1. 横坐标与纵坐标 */
            int width = COMMON_WIDTH_OF_MARGIN_SPACE + COMMON_WIDTH_OF_CONTROL + COMMON_WIDTH_OF_LABEL_TEXT_MIDDLE;
            int x = COMMON_WIDTH_OF_MARGIN_SPACE, y = COMMON_HEIGHT_OF_SPACE;
            int index = 0;
            foreach (var extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
            {
                /* 0表示源数据，1表示更新后的数据 */
                for (byte i = 0; i < 2; i++)
                {                    
                    /* 1.1 创建标签控件 */
                    Label lblName = new Label();
                    if (extendedCustomDataFieldInfo.LogicalName.Length > COMMON_MAX_LEN_OF_LABEL_TEXT_MIDDLE)
                    {
                        lblName.Text = string.Format("{0}...：",
                                    extendedCustomDataFieldInfo.LogicalName.Substring(0, COMMON_MAX_LEN_OF_LABEL_TEXT_MIDDLE));
                    }
                    else
                    {
                        lblName.Text = string.Format("{0}：", extendedCustomDataFieldInfo.LogicalName);
                    }
                    lblName.Width = COMMON_WIDTH_OF_LABEL_TEXT_MIDDLE;
                    lblName.TextAlign = ContentAlignment.MiddleRight;
                    if (i == 0)
                    {
                        lblName.Location = new Point(x, y);
                    }
                    else
                    {
                        lblName.Location = new Point(x + width, y);
                    }
                    lblName.TabIndex = 1025;
                    xscPanel.Controls.Add(lblName);

                    /* 1.2. 创建内容控件 */
                    int controlX = x + lblName.Width;
                    DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
                    if (dataFieldProperty == DataFieldProperty.PhysicalDataField)
                    {
                        PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.Boolean:
                                CheckEdit checkEdit = new CheckEdit();
                                checkEdit.AccessibleDescription = i.ToString();
                                checkEdit.Properties.ReadOnly = true;
                                checkEdit.Properties.Caption = string.Empty;
                                if (i == 0)
                                {
                                    checkEdit.Location = new Point(controlX, y);
                                }
                                else
                                {
                                    checkEdit.Location = new Point(controlX + width, y);
                                }                         
                                checkEdit.TabIndex = index;
                                checkEdit.Tag = extendedCustomDataFieldInfo;
                                xscPanel.Controls.Add(checkEdit);
                                break;

                            case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                            case PhysicalDataFieldType.YearAndMonthAndDay:
                            case PhysicalDataFieldType.YearAndMonth:
                            case PhysicalDataFieldType.MonthAndDay:
                                DateEdit dateEdit = new DateEdit();
                                dateEdit.AccessibleDescription = i.ToString();
                                dateEdit.Properties.ReadOnly = true;
                                dateEdit.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                dateEdit.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                                switch (physicalDataFieldType)
                                {
                                    case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                                        dateEdit.Properties.VistaDisplayMode = DevExpress.Utils.DefaultBoolean.True;
                                        dateEdit.Properties.VistaEditTime = DevExpress.Utils.DefaultBoolean.True;
                                        dateEdit.Properties.DisplayFormat.FormatString = "G";
                                        dateEdit.Properties.EditFormat.FormatString = "G";
                                        dateEdit.Properties.EditMask = "G";
                                        break;

                                    case PhysicalDataFieldType.YearAndMonthAndDay:
                                        dateEdit.Properties.DisplayFormat.FormatString = "d";
                                        dateEdit.Properties.EditFormat.FormatString = "d";
                                        dateEdit.Properties.EditMask = "d";
                                        break;

                                    case PhysicalDataFieldType.YearAndMonth:
                                        dateEdit.Properties.DisplayFormat.FormatString = "y";
                                        dateEdit.Properties.EditFormat.FormatString = "y";
                                        dateEdit.Properties.EditMask = "y";
                                        break;

                                    case PhysicalDataFieldType.MonthAndDay:
                                        dateEdit.Properties.DisplayFormat.FormatString = "m";
                                        dateEdit.Properties.EditFormat.FormatString = "m";
                                        dateEdit.Properties.EditMask = "m";
                                        break;
                                }
                                dateEdit.Properties.NullText = string.Empty;
                                dateEdit.EditValue = null;
                                if (i == 0)
                                {
                                    dateEdit.Location = new Point(controlX, y);
                                }
                                else
                                {
                                    dateEdit.Location = new Point(controlX + width, y);
                                }
                                dateEdit.TabIndex = index;
                                dateEdit.Width = COMMON_WIDTH_OF_CONTROL;
                                dateEdit.Tag = extendedCustomDataFieldInfo;
                                xscPanel.Controls.Add(dateEdit);
                                break;

                            case PhysicalDataFieldType.Time:
                                TimeEdit timeEdit = new TimeEdit();
                                timeEdit.AccessibleDescription = i.ToString();
                                timeEdit.Properties.ReadOnly = true;
                                timeEdit.Properties.Mask.EditMask = "HH:mm:ss";
                                timeEdit.Properties.NullText = string.Empty;
                                timeEdit.EditValue = null;
                                if (i == 0)
                                {
                                    timeEdit.Location = new Point(controlX, y);
                                }
                                else
                                {
                                    timeEdit.Location = new Point(controlX + width, y);
                                }
                                timeEdit.TabIndex = index;
                                timeEdit.Width = COMMON_WIDTH_OF_CONTROL;
                                timeEdit.Properties.LookAndFeel.SkinName = "Blue";
                                timeEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                                timeEdit.Tag = extendedCustomDataFieldInfo;
                                xscPanel.Controls.Add(timeEdit);
                                break;

                            case PhysicalDataFieldType.PicAttachment:
                            case PhysicalDataFieldType.PDFAttachment:
                            case PhysicalDataFieldType.DocAttachment:
                                DevExpressUploadFile devExpressUploadFile = new DevExpressUploadFile();
                                devExpressUploadFile.AccessibleDescription = i.ToString();
                                devExpressUploadFile.SkinName = "Blue";
                                devExpressUploadFile.ReadOnly = true;
                                if (physicalDataFieldType == PhysicalDataFieldType.DocAttachment)
                                {
                                    devExpressUploadFile.DocType = DocType.DocAttachment;
                                    devExpressUploadFile.Filter = AppSettingHelper.DefaultDocFormat;
                                }
                                else if (physicalDataFieldType == PhysicalDataFieldType.PicAttachment)
                                {
                                    devExpressUploadFile.DocType = DocType.PicAttachment;
                                    devExpressUploadFile.Filter = AppSettingHelper.DefaultPictureFormat;
                                }
                                else
                                {
                                    devExpressUploadFile.DocType = DocType.PDFAttachment;
                                    devExpressUploadFile.Filter = AppSettingHelper.DefaultPDFFormat;
                                }
                                devExpressUploadFile.Width = COMMON_WIDTH_OF_CONTROL;
                                if (i == 0)
                                {
                                    devExpressUploadFile.Location = new Point(controlX, y);
                                }
                                else
                                {
                                    devExpressUploadFile.Location = new Point(controlX + width, y);
                                }
                                devExpressUploadFile.TabIndex = index;
                                devExpressUploadFile.Tag = extendedCustomDataFieldInfo;
                                devExpressUploadFile.OnViewLinkClicked += (sender, e) =>
                                {
                                    if (!string.IsNullOrWhiteSpace(devExpressUploadFile.FileName))
                                    {
                                        if (devExpressUploadFile.CustomData == null || devExpressUploadFile.CustomData.Length == 0)
                                        {
                                            byte[] data = customDataFieldContract.GetFileData(extendedCustomDataFieldInfo.PhysicalName, devExpressUploadFile.FileName);
                                            devExpressUploadFile.CustomData = data;
                                        }
                                    }
                                };
                                xscPanel.Controls.Add(devExpressUploadFile);
                                break;

                            default:
                                if (i == 0)
                                {
                                    AddTextControl(controlX, y, index, extendedCustomDataFieldInfo, i);
                                }
                                else
                                {
                                    AddTextControl(controlX + width, y, index, extendedCustomDataFieldInfo, i);
                                }
                                break;

                        }
                    }
                    else
                    {
                        if (i == 0)
                        {
                            AddTextControl(controlX, y, index, extendedCustomDataFieldInfo, i);
                        }
                        else
                        {
                            AddTextControl(controlX + width, y, index, extendedCustomDataFieldInfo, i);
                        }
                    }
                }
                index++;
                y += (COMMON_HEIGHT_OF_CONTROL + COMMON_HEIGHT_OF_SPACE);
            }
            int height = COMMON_HEIGHT_OF_CONTROL + COMMON_HEIGHT_OF_SPACE;
            if (index <= 2)
            {
                Height = 200;
            }
            else if (index <= 13)
            {
                Height = index * height + 130;
            }
        }

        /// <summary>
        /// 仅显示不同数据对比
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 在控件上加载
        /// </summary>
        /// <param name="control"></param>
        /// <param name="extendedCustomDataFieldInfo"></param>
        /// <param name="dataRow"></param>
        private void LoadDataOnControl(Control control, ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo, object value)
        {
            if (value == null)
            {
                return;
            }
            DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
            switch (dataFieldProperty)
            {
                case DataFieldProperty.LogicalDataField:
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    switch (logicalDataFieldType)
                    {
                        case LogicalDataFieldType.DigitExpression:
                        case LogicalDataFieldType.StringExpression:
                            ((TextEdit)control).Text = DataConvertionHelper.GetConvertedString(value);
                            break;

                        case LogicalDataFieldType.DateTimeExpression:
                            ((TextEdit)control).Text = DataConvertionHelper.EndowStringOfDate(DataConvertionHelper.GetConvertedDateTime(value));
                            break;
                    }
                    break;

                case DataFieldProperty.PhysicalDataField:
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.Boolean:
                            ((CheckEdit)control).Checked = DataConvertionHelper.GetBoolean(value);
                            break;

                        case PhysicalDataFieldType.Int32:
                        case PhysicalDataFieldType.Decimal:
                        case PhysicalDataFieldType.ArbitraryString:
                        case PhysicalDataFieldType.ExtendedArbitraryString:
                        case PhysicalDataFieldType.NumeralString:
                        case PhysicalDataFieldType.CharString:
                        case PhysicalDataFieldType.MixedString:
                        case PhysicalDataFieldType.Association:
                        case PhysicalDataFieldType.PrimaryAssociation:
                        case PhysicalDataFieldType.SecondaryAssociation:
                        case PhysicalDataFieldType.DropdownListEnum:
                        case PhysicalDataFieldType.DropdownListEnumValue:
                        case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                        case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                        case PhysicalDataFieldType.DepartmentDropdownListEnum:
                        case PhysicalDataFieldType.CscadeEnum:
                        case PhysicalDataFieldType.DepartmentTreeViewEnum:
                        case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                        case PhysicalDataFieldType.TreeViewEnum:
                        case PhysicalDataFieldType.TreeViewEnumValue:
                        case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                        case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                        case PhysicalDataFieldType.MultiSelectedEnum:
                        case PhysicalDataFieldType.EnumValue:
                        case PhysicalDataFieldType.EnumNameDependency:
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
                        case PhysicalDataFieldType.DepartmentValue:
                        case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                        case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                            ((TextEdit)control).Text = DataConvertionHelper.GetConvertedString(value);
                            break;

                        case PhysicalDataFieldType.EncryptedString:
                            ((TextEdit)control).Text = CryptographyHelper.Decrypt(DataConvertionHelper.GetConvertedString(value));
                            break;

                        case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                        case PhysicalDataFieldType.YearAndMonthAndDay:
                        case PhysicalDataFieldType.YearAndMonth:
                        case PhysicalDataFieldType.MonthAndDay:
                            if (!DataConvertionHelper.IsNullValue(DataConvertionHelper.GetDateTime(value)))
                            {
                                ((DateEdit)control).EditValue = DataConvertionHelper.GetDateTime(value);
                            }
                            break;

                        case PhysicalDataFieldType.Time:
                            if (!DataConvertionHelper.IsNullValue(DataConvertionHelper.GetDateTime(value)))
                            {
                                ((TimeEdit)control).EditValue = DataConvertionHelper.GetDateTime(value);
                            }
                            break;
                            
                        case PhysicalDataFieldType.DocAttachment:
                        case PhysicalDataFieldType.PicAttachment:
                        case PhysicalDataFieldType.PDFAttachment:
                            DevExpressUploadFile uploadFile = (DevExpressUploadFile)control;
                            string fileName = DataConvertionHelper.GetString(value);
                            uploadFile.FileName = fileName;
                            uploadFile.UserData = fileName;
                            break;
                    }
                    break;
            }
        }

        /// <summary>
        /// 增加文本框
        /// </summary>
        /// <param name="controlX"></param>
        /// <param name="y"></param>
        /// <param name="index"></param>
        /// <param name="extendedCustomDataFieldInfo"></param>
        /// <param name="idx"></param>
        private void AddTextControl(int controlX, int y, int index, ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo, byte idx)
        {
            TextEdit textEdit = new TextEdit();
            textEdit.AccessibleDescription = idx.ToString();
            textEdit.Properties.ReadOnly = true;
            textEdit.Location = new Point(controlX, y);
            textEdit.Width = COMMON_WIDTH_OF_CONTROL;
            textEdit.TabIndex = index;
            textEdit.Tag = extendedCustomDataFieldInfo;
            xscPanel.Controls.Add(textEdit);
        }

        #endregion
        
    }
}
