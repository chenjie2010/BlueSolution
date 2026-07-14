//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: TableBusiness.cs
// 描述: 表格数据提交业务
// 作者：ChenJie 
// 编写日期：2018/02/28
// Copyright 2018
//-----------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.Data;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraPrinting.BarCode;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.CustomLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;
using Blue.CustomLibrary;
using Blue.Model.BusinessModule;
using Blue.Model.SystemModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.SystemModule;
using Blue.WCFContracts.DataFilledModule;

namespace Blue.WindowsFormsClient.Common
{

    /// <summary>
    /// 表格数据提交业务
    /// </summary>
    public class TableBusiness
    {
        #region 私有常量

        /// <summary>
        /// 标签字符的最大长度（三列）
        /// </summary>
        private const int COMMON_MAX_LEN_OF_LABEL_TEXT = 8;

        /// <summary>
        /// 标签字符的省略符号长度
        /// </summary>
        private const int COMMON_LEN_OF_OMIT_TEXT = 2;

        /// <summary>
        /// 标签的宽度（三列）
        /// </summary>
        private const int COMMON_WIDTH_OF_LABEL_TEXT = 118;

        /// <summary>
        /// 多行文本框的高度
        /// </summary>
        private const int COMMON_HEIGHT_OF_TEXT = 60;

        /// <summary>
        /// 标签字符的最大长度（双列）
        /// </summary>
        private const int COMMON_MAX_LEN_OF_LABEL_TEXT_MIDDLE = 12;

        /// <summary>
        /// 标签的宽度（双列）
        /// </summary>
        private const int COMMON_WIDTH_OF_LABEL_TEXT_MIDDLE = 180;

        /// <summary>
        /// 标签字符的最大长度（单列）
        /// </summary>
        private const int COMMON_MAX_LEN_OF_LABEL_TEXT_BIG = 24;

        /// <summary>
        /// 标签的宽度（单列）
        /// </summary>
        private const int COMMON_WIDTH_OF_LABEL_TEXT_BIG = 360;

        /// <summary>
        /// 控件宽度
        /// </summary>
        private const int COMMON_WIDTH_OF_CONTROL = 260;

        /// <summary>
        /// 控件高度
        /// </summary>
        private const int COMMON_HEIGHT_OF_CONTROL = 20;

        /// <summary>
        /// 每行之间的间隙距离
        /// </summary>
        private const int COMMON_HEIGHT_OF_SPACE = 10;

        /// <summary>
        /// 每行的控件之间的间隙距离
        /// </summary>
        private const int COMMON_WIDTH_OF_SPACE = 8;

        /// <summary>
        ///控件边沿的距离
        /// </summary>
        private const int COMMON_WIDTH_OF_MARGIN_SPACE = 3;

        /// <summary>
        /// Group 控件的标题行高度
        /// </summary>
        private const int HEIGHT_OF_GROUP_HEADER = 20;

        /// <summary>
        /// 控件顶部偏移距离
        /// </summary>
        private const int CONTROL_HEIGHT_OFFSET = 3;

        /// <summary>
        /// 提示控件顶部偏移距离
        /// </summary>
        private const int TOOLTIP_CONTROL_HEIGHT_OFFSET = 5;

        /// <summary>
        /// 控件 LOOKUPEDIT 每列的宽度
        /// </summary>
        private const int EACH_COLUMN_WIDTH_ON_LOOKUPEDIT = 80;

        /// <summary>
        /// 控件 LOOKUPEDIT 最大宽度
        /// </summary>
        private const int MAX_WIDTH_ON_LOOKUPEDIT = 600;

        /// <summary>
        /// 必填标签的宽度和高度大小
        /// </summary>
        private const int SIZE_OF_REQUIRED_LABEL = 12;

        #endregion

        #region 契约接口

        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomDepartmentContract customDepartmentContract;
        private readonly ICustomTableContract customTableContract;
        private readonly ICombinedTableContract combinedTableContract;
        private readonly IBusinessInstanceContract businessInstanceContract;

        #endregion

        #region 私有变量

        /// <summary>
        /// 超大枚举缓存
        /// </summary>
        private readonly IList<decimal> superEnumIds;

        /// <summary>
        /// 编辑状态
        /// </summary>
        private readonly bool currentEditedState;

        /// <summary>
        /// 表的编号与记录编号
        /// </summary>
        private readonly IDictionary<decimal, decimal> dicTableIdAndRecordId;

        /// <summary>
        /// 主关联缓存
        /// </summary>
        private readonly IDictionary<decimal, CustomAssociationInfo> customAssociationInfos;

        /// <summary>
        /// 表格对象
        /// </summary>
        private readonly CustomFormInfo customFormInfo;

        /// <summary>
        /// 控件
        /// </summary>
        private readonly Control groupControl;

        #endregion

        #region 属性

        /// <summary>
        /// 业务实例编号
        /// </summary>
        public decimal InstanceId
        {
            get;
            set;
        }

        /// <summary>
        /// 设置提示窗口
        /// </summary>
        public SetToolTipOnControlDelegate SetToolTipOnControl
        {
            get;
            set;
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        public bool FormReadOnly
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="instanceId"></param>
        /// <param name="formReadOnly"></param>
        /// <param name="editedState"></param>
        /// <param name="formInfo"></param>
        /// <param name="dataFieldContract"></param>
        /// <param name="asDataFieldContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="enumContract"></param>
        /// <param name="departmentContract"></param>
        /// <param name="tableContract"></param>
        /// <param name="cmbTableContract"></param>
        /// <param name="instanceContract"></param>
        /// <param name="control"></param>
        public TableBusiness(decimal instanceId, bool formReadOnly, bool editedState, CustomFormInfo formInfo, ICustomDataFieldContract dataFieldContract, IAssociatedDataFieldContract asDataFieldContract,
            ICustomAssociationContract associationContract, ICustomEnumContract enumContract, ICustomDepartmentContract departmentContract, ICustomTableContract tableContract,
            ICombinedTableContract cmbTableContract, IBusinessInstanceContract instanceContract, Control control)
        {
            InstanceId = instanceId;
            FormReadOnly = formReadOnly;
            currentEditedState = editedState;
            customFormInfo = formInfo;
            customDataFieldContract = dataFieldContract;
            associatedDataFieldContract = asDataFieldContract;
            customAssociationContract = associationContract;
            customEnumContract = enumContract;
            customDepartmentContract = departmentContract;
            customTableContract = tableContract;
            combinedTableContract = cmbTableContract;
            businessInstanceContract = instanceContract;
            groupControl = control;
            superEnumIds = new List<decimal>();
            dicTableIdAndRecordId = new Dictionary<decimal, decimal>();
            customAssociationInfos = new Dictionary<decimal, CustomAssociationInfo>();
        }

        #endregion

        #region 创建控件

        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="extendedCustomDataFieldInfos"></param>
        /// <param name="formShowStyleSetting"></param>
        /// <returns></returns>
        public int CreateControls(IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos, FormShowStyleSetting formShowStyleSetting, ref int multiTextBoxCount)
        {
            bool containsMultiTextBox = false;
            int dataFiledIndex = AddSystemDataField(multiTextBoxCount, formShowStyleSetting);
            foreach (ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
            {
                AddUserControl(dataFiledIndex, multiTextBoxCount, formShowStyleSetting, extendedCustomDataFieldInfo);
                /*  统计该行是否包含多行文本框 */
                if (((DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField) &&
                    ((PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType == PhysicalDataFieldType.ExtendedArbitraryString))
                {
                    containsMultiTextBox = true;
                }
                dataFiledIndex++;
                if ((dataFiledIndex % formShowStyleSetting.CountForEachRow == 0) && containsMultiTextBox)
                {
                    multiTextBoxCount++;
                    containsMultiTextBox = false;
                }
            }

            return dataFiledIndex;
        }

        /// <summary>
        /// 增加系统字段
        /// </summary>
        /// <param name="multiTextBoxCount"></param>
        /// <param name="formShowStyleSetting"></param>
        /// <returns></returns>
        private int AddSystemDataField(int multiTextBoxCount, FormShowStyleSetting formShowStyleSetting)
        {
            /* 增加系统字段 */
            IList<ExtendedCustomDataFieldInfo> systemExtendedCustomDataFieldInfos = RoleHelper.GetSystemDataFieldInfo(customFormInfo.DataFieldSetting);
            for (int index = 0; index < systemExtendedCustomDataFieldInfos.Count; index++)
            {
                AddUserControl(index, multiTextBoxCount, formShowStyleSetting, systemExtendedCustomDataFieldInfos[index]);
            }

            return systemExtendedCustomDataFieldInfos.Count;
        }

        /// <summary>
        /// 增加用户控件
        /// </summary>
        /// <param name="index"></param>
        /// <param name="multiTextBoxCount"></param>
        /// <param name="formShowStyleSetting"></param>
        /// <param name="extendedCustomDataFieldInfo"></param>
        private void AddUserControl(int index, int multiTextBoxCount, FormShowStyleSetting formShowStyleSetting, ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo)
        {
            int x = 0, y = 0;
            /* 1. 横坐标 */
            int remainder = index % formShowStyleSetting.CountForEachRow;
            if (remainder == 0)
            {
                x = COMMON_WIDTH_OF_MARGIN_SPACE;
            }
            else
            {
                x = (COMMON_WIDTH_OF_CONTROL + formShowStyleSetting.LabelWidth) * remainder + COMMON_WIDTH_OF_SPACE;
            }

            /* 2 纵坐标 */
            int quotient = index / formShowStyleSetting.CountForEachRow;
            y = COMMON_HEIGHT_OF_SPACE + (COMMON_HEIGHT_OF_CONTROL + COMMON_HEIGHT_OF_SPACE) * quotient + COMMON_HEIGHT_OF_TEXT * multiTextBoxCount;
            if (formShowStyleSetting.FormCompleted)
            {
                y += HEIGHT_OF_GROUP_HEADER;
            }

            /* 3. 增加控件 */
            AddUserControl(extendedCustomDataFieldInfo, formShowStyleSetting, index, x, y);
        }

        /// <summary>
        /// 增加用户控件
        /// </summary>
        /// <param name="extendedCustomDataFieldInfo"></param>
        /// <param name="index"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        private void AddUserControl(ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo, FormShowStyleSetting formShowStyleSetting, int index, int x, int y)
        {
            /* 1. 创建标签控件 */
            Label lblName = new Label();
            if (extendedCustomDataFieldInfo.LogicalName.Length > formShowStyleSetting.CharacterCountOnLabel)
            {
                lblName.Text = string.Format("{0}...：",
                            extendedCustomDataFieldInfo.LogicalName.Substring(0, formShowStyleSetting.CharacterCountOnLabel - COMMON_LEN_OF_OMIT_TEXT));
            }
            else
            {
                lblName.Text = string.Format("{0}：", extendedCustomDataFieldInfo.LogicalName);
            }
            lblName.Width = formShowStyleSetting.LabelWidth;
            lblName.TextAlign = ContentAlignment.MiddleRight;
            lblName.Location = new Point(x, y);
            lblName.TabIndex = 1025;
            groupControl.Controls.Add(lblName);

            /* 2. 创建内容控件 */
            int controlX = x + lblName.Width;
            int controlWidth = 0;
            DataFieldAuthority dataFieldAuthority = DataFieldAuthority.ReadOnly;
            DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
            switch (dataFieldProperty)
            {
                case DataFieldProperty.PhysicalDataField:
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    int width = DataFieldHelper.GetControlWidth(physicalDataFieldType);
                    dataFieldAuthority = (DataFieldAuthority)extendedCustomDataFieldInfo.AuthorityType;
                    if (FormReadOnly || DataFieldHelper.IsReadOnly(physicalDataFieldType) || !currentEditedState)
                    {
                        dataFieldAuthority = DataFieldAuthority.ReadOnly;
                    }
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.Boolean:
                            CheckEdit checkEdit = new CheckEdit();
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                            {
                                checkEdit.Properties.ReadOnly = true;
                            }
                            checkEdit.Properties.Caption = string.Empty;
                            checkEdit.Location = new Point(controlX, y);
                            checkEdit.TabIndex = index;
                            checkEdit.Tag = extendedCustomDataFieldInfo;
                            checkEdit.GotFocus += (sender, e) =>
                            {
                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                            };
                            checkEdit.Width = width;
                            controlWidth = checkEdit.Width;
                            groupControl.Controls.Add(checkEdit);
                            break;

                        case PhysicalDataFieldType.Int32:
                        case PhysicalDataFieldType.Decimal:
                            TextEdit digitalTextEdit = new TextEdit();
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                            {
                                digitalTextEdit.Properties.ReadOnly = true;
                            }
                            if (physicalDataFieldType == PhysicalDataFieldType.Int32)
                            {
                                digitalTextEdit.Properties.MaxLength = 11;
                            }
                            else
                            {
                                digitalTextEdit.Properties.MaxLength = 19;
                            }
                            digitalTextEdit.Location = new Point(controlX, y);
                            digitalTextEdit.Width = width;
                            digitalTextEdit.TabIndex = index;
                            digitalTextEdit.Tag = extendedCustomDataFieldInfo;
                            digitalTextEdit.GotFocus += (sender, e) =>
                            {
                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                            };
                            controlWidth = digitalTextEdit.Width;
                            groupControl.Controls.Add(digitalTextEdit);
                            break;

                        case PhysicalDataFieldType.ArbitraryString:
                        case PhysicalDataFieldType.ExtendedArbitraryString:
                        case PhysicalDataFieldType.NumeralString:
                        case PhysicalDataFieldType.CharString:
                        case PhysicalDataFieldType.MixedString:
                        case PhysicalDataFieldType.EncryptedString:
                            TextEdit textEdit = null;
                            if (physicalDataFieldType == PhysicalDataFieldType.ExtendedArbitraryString)
                            {
                                textEdit = new MemoEdit();
                                textEdit.Size = new Size(width, COMMON_HEIGHT_OF_TEXT + 20);
                            }
                            else
                            {
                                textEdit = new TextEdit();
                                textEdit.Width = width;
                            }
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                            {
                                textEdit.Properties.ReadOnly = true;
                            }
                            textEdit.Properties.MaxLength = extendedCustomDataFieldInfo.DataFieldLength;
                            textEdit.Location = new Point(controlX, y);
                            textEdit.TabIndex = index;
                            textEdit.Tag = extendedCustomDataFieldInfo;
                            textEdit.GotFocus += (sender, e) =>
                            {
                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                            };
                            controlWidth = textEdit.Width;
                            groupControl.Controls.Add(textEdit);
                            break;

                        case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                        case PhysicalDataFieldType.YearAndMonthAndDay:
                        case PhysicalDataFieldType.YearAndMonth:
                        case PhysicalDataFieldType.MonthAndDay:
                            DateEdit dateEdit = new DateEdit();
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                            {
                                dateEdit.Properties.ReadOnly = true;
                            }
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
                                    dateEdit.Properties.DisplayFormat.FormatString = "D";
                                    dateEdit.Properties.EditFormat.FormatString = "D";
                                    dateEdit.Properties.EditMask = "D";
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
                            dateEdit.Location = new Point(controlX, y);
                            dateEdit.TabIndex = index;
                            dateEdit.Width = width;
                            dateEdit.Tag = extendedCustomDataFieldInfo;
                            dateEdit.GotFocus += (sender, e) =>
                            {
                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                            };
                            controlWidth = dateEdit.Width;
                            groupControl.Controls.Add(dateEdit);
                            break;

                        case PhysicalDataFieldType.Time:
                            TimeEdit timeEdit = new TimeEdit();
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                            {
                                timeEdit.Properties.ReadOnly = true;
                            }
                            timeEdit.Properties.Mask.EditMask = "HH:mm:ss";
                            timeEdit.Properties.NullText = string.Empty;
                            timeEdit.EditValue = null;
                            timeEdit.Location = new Point(controlX, y);
                            timeEdit.TabIndex = index;
                            timeEdit.Width = width;
                            timeEdit.Properties.LookAndFeel.SkinName = "Blue";
                            timeEdit.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
                            timeEdit.Tag = extendedCustomDataFieldInfo;
                            timeEdit.GotFocus += (sender, e) =>
                            {
                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                            };
                            controlWidth = timeEdit.Width;
                            groupControl.Controls.Add(timeEdit);
                            break;

                        case PhysicalDataFieldType.Association:
                            CustomAssociationInfo associationInfo = null;
                            if (!customAssociationInfos.ContainsKey(extendedCustomDataFieldInfo.AssociatedDataFieldId))
                            {
                                decimal associationId = associatedDataFieldContract.GetAssociationId(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                                associationInfo = customAssociationContract.GetModelInfo(associationId);
                                customAssociationInfos.Add(extendedCustomDataFieldInfo.AssociatedDataFieldId, associationInfo);
                            }
                            else
                            {
                                associationInfo = customAssociationInfos[extendedCustomDataFieldInfo.AssociatedDataFieldId];
                            }                            
                            SearchLookUpEditWithGrid searchLookUpEditWithGrid = new SearchLookUpEditWithGrid();
                            searchLookUpEditWithGrid.Location = new Point(controlX, y);
                            searchLookUpEditWithGrid.TabIndex = index;
                            searchLookUpEditWithGrid.Width = width;
                            searchLookUpEditWithGrid.ValueMember = associatedDataFieldContract.GetPhysicalName(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                            searchLookUpEditWithGrid.TextEditStyle = TextEditStyles.Standard;
                            searchLookUpEditWithGrid.Tag = extendedCustomDataFieldInfo;                            
                            if ((DataFieldAuthority)extendedCustomDataFieldInfo.AuthorityType == DataFieldAuthority.ReadOnly)
                            {
                                searchLookUpEditWithGrid.ReadOnly = true;
                            }
                            else
                            {
                                string content = string.Empty;
                                searchLookUpEditWithGrid.GotFocus += (sender, e) =>
                                {
                                    SetToolTipOnControl(extendedCustomDataFieldInfo);
                                };
                                searchLookUpEditWithGrid.OnGridBeforePopup += (sender, e) =>
                                {
                                    if (searchLookUpEditWithGrid.DataSource == null)
                                    {
                                        LoadAssociatedData(searchLookUpEditWithGrid, extendedCustomDataFieldInfo.AssociatedDataFieldId, string.Empty);
                                    }
                                };
                                searchLookUpEditWithGrid.ValueSearched += (sender, e) =>
                                {
                                    LoadAssociatedData(searchLookUpEditWithGrid, extendedCustomDataFieldInfo.AssociatedDataFieldId, e.Content);
                                    content = e.Content;
                                };
                                searchLookUpEditWithGrid.OnPageIndexChanged += (sender, e) =>
                                {
                                    searchLookUpEditWithGrid.CurrentPageIndex = e.NewPageIndex;
                                    LoadAssociatedData(searchLookUpEditWithGrid, extendedCustomDataFieldInfo.AssociatedDataFieldId, content);
                                };
                            }
                            controlWidth = searchLookUpEditWithGrid.Width;
                            groupControl.Controls.Add(searchLookUpEditWithGrid);
                            break;

                        case PhysicalDataFieldType.PrimaryAssociation:
                            CustomAssociationInfo customAssociationInfo = null;
                            if (!customAssociationInfos.ContainsKey(extendedCustomDataFieldInfo.AssociatedDataFieldId))
                            {
                                decimal associationId = associatedDataFieldContract.GetAssociationId(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                                customAssociationInfo = customAssociationContract.GetModelInfo(associationId);
                                customAssociationInfos.Add(extendedCustomDataFieldInfo.AssociatedDataFieldId, customAssociationInfo);
                            }
                            else
                            {
                                customAssociationInfo = customAssociationInfos[extendedCustomDataFieldInfo.AssociatedDataFieldId];
                            }
                            if (customAssociationInfo.SuperAssociationEnabled)
                            {
                                ComboBoxEdit cmbAssociation = new ComboBoxEdit();
                                cmbAssociation.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                                cmbAssociation.Location = new Point(controlX, y);
                                cmbAssociation.TabIndex = index;
                                cmbAssociation.Width = width;
                                cmbAssociation.Tag = extendedCustomDataFieldInfo;
                                if ((DataFieldAuthority)extendedCustomDataFieldInfo.AuthorityType == DataFieldAuthority.ReadOnly)
                                {
                                    cmbAssociation.Properties.ReadOnly = true;
                                }
                                else
                                {
                                    cmbAssociation.GotFocus += (sender, e) =>
                                    {
                                        SetToolTipOnControl(extendedCustomDataFieldInfo);
                                    };
                                    cmbAssociation.ButtonPressed += (sender, e) =>
                                    {
                                        AssociatedDataForm frmAssociatedData = new AssociatedDataForm()
                                        {
                                            AssociationId = customAssociationInfo.AssociationId
                                        };
                                        frmAssociatedData.DataRowConfrimed = (dataRow) =>
                                        {
                                            string dataFieldPhysicalName = frmAssociatedData.GetDataFieldPhysicalName(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                                            if (!string.IsNullOrWhiteSpace(dataFieldPhysicalName))
                                            {
                                                if (dataRow != null)
                                                {
                                                    cmbAssociation.EditValue = dataRow[dataFieldPhysicalName];
                                                }
                                                else
                                                {
                                                    cmbAssociation.EditValue = null;
                                                }
                                                PrimaryAssociationValueChanged(extendedCustomDataFieldInfo.DataFieldId, dataRow, groupControl);
                                            }
                                        };
                                        frmAssociatedData.ShowDialog();
                                    };
                                }
                                controlWidth = cmbAssociation.Width;
                                groupControl.Controls.Add(cmbAssociation);
                            }
                            else
                            {
                                AssociationShowMode associationShowMode = (AssociationShowMode)customAssociationInfo.ShowMode;
                                string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                                switch (associationShowMode)
                                {
                                    case AssociationShowMode.Hierarchicy:
                                        LookUpEditWithGrid lookUpEditWithGrid = new LookUpEditWithGrid();
                                        lookUpEditWithGrid.Width = width;
                                        lookUpEditWithGrid.Location = new Point(controlX, y);
                                        lookUpEditWithGrid.TabIndex = index;
                                        lookUpEditWithGrid.Tag = extendedCustomDataFieldInfo;
                                        lookUpEditWithGrid.ValueMember = associatedDataFieldContract.GetPhysicalName(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                                        lookUpEditWithGrid.ShowSearch = true;
                                        if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                                        {
                                            lookUpEditWithGrid.ReadOnly = true;
                                        }
                                        else
                                        {
                                            lookUpEditWithGrid.GotFocus += (sender, e) =>
                                            {
                                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                                                if (lookUpEditWithGrid.DataSource == null)
                                                {
                                                    lookUpEditWithGrid.DataKeyNames = new string[] { key };
                                                    IList<string> groupKeyNames = new List<string>();
                                                    IList<AssociatedDataFieldInfo> associatedDataFieldInfos = associatedDataFieldContract.GetModelInfos(customAssociationInfo.AssociationId);
                                                    foreach (AssociatedDataFieldInfo associatedDataFieldInfo in associatedDataFieldInfos)
                                                    {
                                                        if (associatedDataFieldInfo.IsHierarchal)
                                                        {
                                                            groupKeyNames.Add(associatedDataFieldInfo.PhysicalName);
                                                        }
                                                    }
                                                    if (groupKeyNames.Count > 0)
                                                    {
                                                        lookUpEditWithGrid.GroupKeyNames = groupKeyNames.ToArray();
                                                    }
                                                    lookUpEditWithGrid.DataSource = customAssociationContract.GetAssociationData(customAssociationInfo.AssociationId);
                                                }
                                            };
                                            /* 修改当前窗口中的从关联字段 */
                                            lookUpEditWithGrid.OnFocusedRowChanged += (sender, e) =>
                                            {
                                                PrimaryAssociationValueChanged(extendedCustomDataFieldInfo.DataFieldId, lookUpEditWithGrid.FocusedDataRow, groupControl);
                                            };
                                            lookUpEditWithGrid.EditValueCleaned += (sender, e) =>
                                            {
                                                PrimaryAssociationValueChanged(extendedCustomDataFieldInfo.DataFieldId, null, groupControl);
                                            };
                                        }
                                        controlWidth = lookUpEditWithGrid.Width;
                                        groupControl.Controls.Add(lookUpEditWithGrid);
                                        break;

                                    case AssociationShowMode.Table:
                                        LookUpEdit lookUpEdit = new LookUpEdit();
                                        lookUpEdit.Width = width;
                                        lookUpEdit.Location = new Point(controlX, y);
                                        lookUpEdit.TabIndex = index;
                                        lookUpEdit.Tag = extendedCustomDataFieldInfo;
                                        lookUpEdit.Properties.NullText = null;
                                        lookUpEdit.Properties.PopupSizeable = false;
                                        lookUpEdit.Properties.ShowFooter = true;
                                        try
                                        {
                                            string physicalName = associatedDataFieldContract.GetPhysicalName(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                                            DataTable data = customAssociationContract.GetAssociationData(customAssociationInfo.AssociationId);
                                            foreach (DataColumn dataColumn in data.Columns)
                                            {
                                                LookUpColumnInfo lookUpColumnInfo = new LookUpColumnInfo(dataColumn.ColumnName, dataColumn.Caption);
                                                if (dataColumn.DataType == typeof(DateTime))
                                                {
                                                    lookUpColumnInfo.FormatType = DevExpress.Utils.FormatType.DateTime;
                                                    lookUpColumnInfo.FormatString = "D";
                                                }
                                                lookUpEdit.Properties.Columns.Add(lookUpColumnInfo);
                                            }
                                            lookUpEdit.Properties.DataSource = data;
                                            lookUpEdit.Properties.Columns[key].Visible = false;
                                            lookUpEdit.Properties.DisplayMember = physicalName;
                                            lookUpEdit.Properties.ValueMember = physicalName;
                                            int popupWidth = data.Columns.Count * EACH_COLUMN_WIDTH_ON_LOOKUPEDIT;
                                            if (popupWidth > MAX_WIDTH_ON_LOOKUPEDIT)
                                            {
                                                popupWidth = MAX_WIDTH_ON_LOOKUPEDIT;
                                            }
                                            lookUpEdit.Properties.PopupWidth = popupWidth;
                                        }
                                        catch (Exception exception)
                                        {
                                            //记录日志, 不抛出异常, 包装异常
                                            WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                                        }
                                        if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                                        {
                                            lookUpEdit.Properties.ReadOnly = true;
                                        }
                                        else
                                        {
                                            lookUpEdit.GotFocus += (sender, e) =>
                                            {
                                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                                            };
                                            lookUpEdit.EditValueChanged += (sender, e) =>
                                            {
                                                if (lookUpEdit.Properties.DataSource != null)
                                                {
                                                    DataTable dataTable = lookUpEdit.Properties.DataSource as DataTable;
                                                    if (dataTable != null && lookUpEdit.ItemIndex >= 0 && lookUpEdit.ItemIndex < dataTable.Rows.Count)
                                                    {
                                                        PrimaryAssociationValueChanged(extendedCustomDataFieldInfo.DataFieldId, dataTable.Rows[lookUpEdit.ItemIndex], groupControl);
                                                    }
                                                }
                                            };
                                        }
                                        controlWidth = lookUpEdit.Width;
                                        groupControl.Controls.Add(lookUpEdit);
                                        break;
                                }
                            }
                            break;

                        case PhysicalDataFieldType.DropdownListEnum:
                        case PhysicalDataFieldType.DepartmentDropdownListEnum:
                            ComboBoxEdit comboBoxEdit = new ComboBoxEdit();
                            comboBoxEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                            comboBoxEdit.Location = new Point(controlX, y);
                            comboBoxEdit.TabIndex = index;
                            comboBoxEdit.Width = width;
                            comboBoxEdit.Tag = extendedCustomDataFieldInfo;
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                            {
                                comboBoxEdit.Properties.ReadOnly = true;
                            }
                            else
                            {
                                comboBoxEdit.GotFocus += (sender, e) =>
                                {
                                    SetToolTipOnControl(extendedCustomDataFieldInfo);
                                    if (comboBoxEdit.Properties.Items.Count == 0)
                                    {
                                        try
                                        {
                                            IList<CommonNode> enumCommonNodes = null;
                                            if (physicalDataFieldType == PhysicalDataFieldType.DropdownListEnum)
                                            {
                                                enumCommonNodes = customEnumContract.GetChildNodes(extendedCustomDataFieldInfo.EnumId);
                                            }
                                            else
                                            {
                                                enumCommonNodes = customDepartmentContract.GetChildNodes(decimal.One);
                                            }
                                            comboBoxEdit.Properties.Items.AddRange(enumCommonNodes.ToArray());
                                        }
                                        catch (Exception exception)
                                        {
                                            //记录日志, 不抛出异常, 包装异常
                                            WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                                        }
                                    }
                                };
                                comboBoxEdit.SelectedIndexChanged += (sender, e) =>
                                {
                                    /* 修改被关联的枚举 */
                                    CommonNode commonNode = comboBoxEdit.EditValue as CommonNode;
                                    EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, commonNode, physicalDataFieldType, groupControl);
                                };
                            }
                            controlWidth = comboBoxEdit.Width;
                            groupControl.Controls.Add(comboBoxEdit);
                            break;

                        case PhysicalDataFieldType.CscadeEnum:
                            ComboBoxEdit cmbCscadeEnum = new ComboBoxEdit();
                            cmbCscadeEnum.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                            cmbCscadeEnum.Location = new Point(controlX, y);
                            cmbCscadeEnum.TabIndex = index;
                            cmbCscadeEnum.Width = width;
                            cmbCscadeEnum.Tag = extendedCustomDataFieldInfo;
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                            {
                                cmbCscadeEnum.Properties.ReadOnly = true;
                            }
                            else
                            {
                                cmbCscadeEnum.GotFocus += (sender, e) =>
                                {
                                    SetToolTipOnControl(extendedCustomDataFieldInfo);
                                };
                                cmbCscadeEnum.ButtonPressed += (sender, e) =>
                                {
                                    int level = customEnumContract.GetMaxLevel(extendedCustomDataFieldInfo.EnumId);
                                    CscadeEnumForm frmCscadeEnumForm = new CscadeEnumForm()
                                    {
                                        CustomEnumContract = customEnumContract,
                                        EnumId = extendedCustomDataFieldInfo.EnumId,
                                        Level = level,
                                        SelectedText = string.Empty
                                    };
                                    frmCscadeEnumForm.CscadeNodeSelected = (commonNodes) =>
                                    {
                                        if (commonNodes != null && commonNodes.Count > 0)
                                        {
                                            cmbCscadeEnum.EditValue = UserControlHelper.GetCleanFormattedName(commonNodes);
                                            /* 修改被关联的枚举 */
                                            MultiEnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, commonNodes, groupControl);
                                        }
                                    };
                                    frmCscadeEnumForm.ShowDialog();
                                };
                            }
                            controlWidth = cmbCscadeEnum.Width;
                            groupControl.Controls.Add(cmbCscadeEnum);
                            break;

                        case PhysicalDataFieldType.DepartmentTreeViewEnum:
                        case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                            TreeDropdownList ddlDepartmentTreeView = new TreeDropdownList();
                            ddlDepartmentTreeView.Location = new Point(controlX, y);
                            ddlDepartmentTreeView.SkinName = "Blue";
                            ddlDepartmentTreeView.TabIndex = index;
                            ddlDepartmentTreeView.Width = width;
                            ddlDepartmentTreeView.OnlySelectedLeaf = true;
                            ddlDepartmentTreeView.Tag = extendedCustomDataFieldInfo;
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                            {
                                ddlDepartmentTreeView.ReadOnly = true;
                            }
                            else
                            {
                                ddlDepartmentTreeView.ButtonPressed += (sender, e) =>
                                {
                                    SetToolTipOnControl(extendedCustomDataFieldInfo);
                                };
                                ddlDepartmentTreeView.BeforeControlPopup += (sender, e) =>
                                {
                                    if (ddlDepartmentTreeView.TreeView.Nodes.Count == 0)
                                    {
                                        if (physicalDataFieldType == PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot)
                                        {
                                            ddlDepartmentTreeView.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract);
                                        }
                                        else
                                        {
                                            ddlDepartmentTreeView.TreeDropdownHandler = new TreeDropdownItems(customDepartmentContract, decimal.One);
                                        }

                                        if (ddlDepartmentTreeView.Value != null)
                                        {
                                            string value = DataConvertionHelper.GetString(ddlDepartmentTreeView.Value);
                                            if (!string.IsNullOrWhiteSpace(value))
                                            {
                                                CommonItemList<decimal, CommonNode> commonItems = null;
                                                if (physicalDataFieldType == PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot)
                                                {
                                                    commonItems = customDepartmentContract.GetTreeviewCommonNodesWithRoot(value);
                                                    ddlDepartmentTreeView.LoadMatchedNodes(commonItems.CommonList);
                                                }
                                                else
                                                {
                                                    commonItems = customDepartmentContract.GetTreeviewCommonNodes(value);
                                                    ddlDepartmentTreeView.LoadMatchedNodes(decimal.One, commonItems.CommonList);
                                                }
                                                if (commonItems.Value > 0)
                                                {
                                                    ddlDepartmentTreeView.SelectedNode = new CommonNode(commonItems.Value, commonItems.Text);
                                                }
                                                else
                                                {
                                                    ddlDepartmentTreeView.SelectedNode = null;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            ddlDepartmentTreeView.InitalizeTreeView();
                                        }
                                    }
                                };
                                /* 将依赖该枚举的枚举依赖类型在选择时自动关联起来 */
                                ddlDepartmentTreeView.AfterTreeNodeSelect += (sender, e) =>
                                {
                                    /* 修改被关联的枚举 */
                                    CommonNode commonNode = ddlDepartmentTreeView.SelectedNode;
                                    EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, commonNode, physicalDataFieldType, groupControl);
                                };
                                ddlDepartmentTreeView.OnNodeRemoved += (sender, e) =>
                                {
                                    EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, null, physicalDataFieldType, groupControl);
                                };
                            }
                            controlWidth = ddlDepartmentTreeView.Width;
                            groupControl.Controls.Add(ddlDepartmentTreeView);
                            break;

                        case PhysicalDataFieldType.TreeViewEnum:
                            bool superEnumEnabled = customEnumContract.GetSuperEnumEnabled(extendedCustomDataFieldInfo.EnumId);
                            if (superEnumEnabled)
                            {
                                if (!superEnumIds.Contains(extendedCustomDataFieldInfo.EnumId))
                                {
                                    superEnumIds.Add(extendedCustomDataFieldInfo.EnumId);
                                }
                                ComboBoxEdit cmbEnum = new ComboBoxEdit();
                                cmbEnum.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                                cmbEnum.Location = new Point(controlX, y);
                                cmbEnum.TabIndex = index;
                                cmbEnum.Width = width;
                                cmbEnum.Tag = extendedCustomDataFieldInfo;
                                if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                                {
                                    cmbEnum.Properties.ReadOnly = true;
                                }
                                else
                                {
                                    cmbEnum.GotFocus += (sender, e) =>
                                    {
                                        SetToolTipOnControl(extendedCustomDataFieldInfo);
                                    };
                                    cmbEnum.ButtonPressed += (sender, e) =>
                                    {
                                        TreeSelectedItemsForm frmTreeSelectedItems = new TreeSelectedItemsForm()
                                        {
                                            CommonNodeContract = customEnumContract,
                                            ParentNodeId = extendedCustomDataFieldInfo.EnumId
                                        };
                                        frmTreeSelectedItems.NodeSelected = (node) =>
                                        {
                                            cmbEnum.EditValue = node;
                                            /* 修改被关联的枚举 */
                                            EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, node, physicalDataFieldType, groupControl);
                                        };
                                        frmTreeSelectedItems.NodeRemoved = () =>
                                        {
                                            EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, null, physicalDataFieldType, groupControl);
                                        };
                                        frmTreeSelectedItems.ShowDialog();
                                    };
                                }
                                controlWidth = cmbEnum.Width;
                                groupControl.Controls.Add(cmbEnum);
                            }
                            else
                            {
                                TreeDropdownList treeDropdownList = new TreeDropdownList();
                                treeDropdownList.Location = new Point(controlX, y);
                                treeDropdownList.SkinName = "Blue";
                                treeDropdownList.TabIndex = index;
                                treeDropdownList.Width = width;
                                treeDropdownList.OnlySelectedLeaf = true;
                                treeDropdownList.Tag = extendedCustomDataFieldInfo;
                                if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                                {
                                    treeDropdownList.ReadOnly = true;
                                }
                                else
                                {
                                    treeDropdownList.TreeDropdownHandler = new TreeDropdownItems(customEnumContract, extendedCustomDataFieldInfo.EnumId);
                                    treeDropdownList.ButtonPressed += (sender, e) =>
                                    {
                                        SetToolTipOnControl(extendedCustomDataFieldInfo);
                                    };
                                    treeDropdownList.BeforeControlPopup += (sender, e) =>
                                    {
                                        if (treeDropdownList.TreeView.Nodes.Count == 0)
                                        {
                                            treeDropdownList.TreeDropdownHandler = new TreeDropdownItems(customEnumContract, extendedCustomDataFieldInfo.EnumId);
                                            if (treeDropdownList.Value != null)
                                            {
                                                string value = DataConvertionHelper.GetString(treeDropdownList.Value);
                                                if (!string.IsNullOrWhiteSpace(value))
                                                {
                                                    CommonItemList<decimal, CommonNode> commonItems = customEnumContract.GetTreeviewCommonNodes(extendedCustomDataFieldInfo.EnumId, value);
                                                    treeDropdownList.LoadMatchedNodes(extendedCustomDataFieldInfo.EnumId, commonItems.CommonList);
                                                    if (commonItems.Value > 0)
                                                    {
                                                        treeDropdownList.SelectedNode = new CommonNode(commonItems.Value, commonItems.Text);
                                                    }
                                                    else
                                                    {
                                                        treeDropdownList.SelectedNode = null;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                treeDropdownList.InitalizeTreeView();
                                            }
                                        }
                                    };
                                    /* 将依赖该枚举的枚举依赖类型在选择时自动关联起来 */
                                    treeDropdownList.AfterTreeNodeSelect += (sender, e) =>
                                    {
                                        /* 修改被关联的枚举 */
                                        CommonNode commonNode = treeDropdownList.SelectedNode;
                                        EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, commonNode, physicalDataFieldType, groupControl);
                                    };
                                    treeDropdownList.OnNodeRemoved += (sender, e) =>
                                    {
                                        EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, null, physicalDataFieldType, groupControl);
                                    };
                                }
                                controlWidth = treeDropdownList.Width;
                                groupControl.Controls.Add(treeDropdownList);
                            }
                            break;

                        case PhysicalDataFieldType.MultiSelectedEnum:
                            CheckedComboBoxEdit checkedComboBoxEdit = new CheckedComboBoxEdit();
                            checkedComboBoxEdit.Width = width;
                            checkedComboBoxEdit.Properties.TextEditStyle = TextEditStyles.DisableTextEditor;
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                            {
                                checkedComboBoxEdit.Properties.ReadOnly = true;
                            }
                            else
                            {
                                IList<CommonNode> commonNodes = customEnumContract.GetChildNodes(extendedCustomDataFieldInfo.EnumId);
                                checkedComboBoxEdit.Properties.Items.AddRange(commonNodes.ToArray());
                            }
                            checkedComboBoxEdit.Properties.SelectAllItemVisible = false;
                            checkedComboBoxEdit.Properties.ShowButtons = false;
                            checkedComboBoxEdit.Properties.PopupSizeable = false;
                            checkedComboBoxEdit.Location = new Point(controlX, y);
                            checkedComboBoxEdit.TabIndex = index;
                            checkedComboBoxEdit.Tag = extendedCustomDataFieldInfo;
                            checkedComboBoxEdit.GotFocus += (sender, e) =>
                            {
                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                            };
                            checkedComboBoxEdit.EditValueChanged += (sender, e) =>
                            {
                                IList<object> objects = checkedComboBoxEdit.Properties.Items.GetCheckedValues();
                                IList<CommonNode> commonNodes = new List<CommonNode>();
                                foreach (object obj in objects)
                                {
                                    commonNodes.Add(obj as CommonNode);
                                }
                                MultiEnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, commonNodes, groupControl);
                            };
                            controlWidth = checkedComboBoxEdit.Width;
                            groupControl.Controls.Add(checkedComboBoxEdit);
                            break;

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
                        case PhysicalDataFieldType.DepartmentValue:
                        case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                        case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                        case PhysicalDataFieldType.SecondaryAssociation:
                            TextEdit txtDependency = new TextEdit();
                            txtDependency.Size = new System.Drawing.Size(width, 21);
                            txtDependency.Properties.ReadOnly = true;
                            txtDependency.Location = new Point(controlX, y);
                            txtDependency.TabIndex = index;
                            txtDependency.Tag = extendedCustomDataFieldInfo;
                            controlWidth = txtDependency.Width;
                            groupControl.Controls.Add(txtDependency);
                            break;


                        case PhysicalDataFieldType.DocAttachment:
                        case PhysicalDataFieldType.PicAttachment:
                        case PhysicalDataFieldType.PDFAttachment:
                            DevExpressUploadFile devExpressUploadFile = new DevExpressUploadFile();
                            devExpressUploadFile.SkinName = "Blue";
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                            {
                                devExpressUploadFile.ReadOnly = true;
                            }
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
                            devExpressUploadFile.Width = width;
                            devExpressUploadFile.Location = new Point(controlX, y);
                            devExpressUploadFile.TabIndex = index;
                            devExpressUploadFile.Tag = extendedCustomDataFieldInfo;
                            devExpressUploadFile.GotFocus += (sender, e) =>
                            {
                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                            };
                            devExpressUploadFile.OnViewLinkClicked += (sender, e) =>
                            {
                                if (!string.IsNullOrWhiteSpace(devExpressUploadFile.FileName))
                                {
                                    if (devExpressUploadFile.CustomData == null || devExpressUploadFile.CustomData.Length == 0)
                                    {
                                        byte[] data = businessInstanceContract.GetFileData(extendedCustomDataFieldInfo.PhysicalName, devExpressUploadFile.FileName);
                                        devExpressUploadFile.CustomData = data;
                                    }
                                }
                            };
                            controlWidth = devExpressUploadFile.Width;
                            groupControl.Controls.Add(devExpressUploadFile);
                            devExpressUploadFile.BringToFront();
                            break;
                    }
                    break;

                case DataFieldProperty.LogicalDataField:
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    int logicalControlWidth = DataFieldHelper.GetControlWidth(logicalDataFieldType);
                    switch (logicalDataFieldType)
                    {
                        case LogicalDataFieldType.DigitExpression:
                        case LogicalDataFieldType.StringExpression:
                        case LogicalDataFieldType.DateTimeExpression:
                        case LogicalDataFieldType.CombinedFileds:
                            TextEdit textEdit = new TextEdit();
                            textEdit.Properties.ReadOnly = true;
                            textEdit.Location = new Point(controlX, y);
                            textEdit.Width = logicalControlWidth;
                            textEdit.TabIndex = index;
                            textEdit.Tag = extendedCustomDataFieldInfo;
                            textEdit.GotFocus += (sender, e) =>
                            {
                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                            };
                            controlWidth = textEdit.Width;
                            groupControl.Controls.Add(textEdit);
                            break;

                            //case LogicalDataFieldType.OneDimCode:
                            //case LogicalDataFieldType.TwoDimCode:
                            //case LogicalDataFieldType.UserName:
                            //    BarCodeControl barCodeControl = new BarCodeControl();
                            //    if (logicalDataFieldType == LogicalDataFieldType.TwoDimCode)
                            //    {
                            //        QRCodeGenerator qr = new QRCodeGenerator();
                            //        barCodeControl.Symbology = qr;
                            //    }
                            //    else
                            //    {
                            //        Code128Generator code128 = new Code128Generator();
                            //        barCodeControl.Symbology = code128;
                            //    }
                            //    barCodeControl.Location = new Point(controlX, y);
                            //    barCodeControl.Width = logicalControlWidth;
                            //    barCodeControl.TabIndex = index;
                            //    barCodeControl.Tag = extendedCustomDataFieldInfo;
                            //    barCodeControl.GotFocus += (sender, e) =>
                            //    {
                            //        SetToolTipOnControl(extendedCustomDataFieldInfo);
                            //    };
                            //    controlWidth = barCodeControl.Width;
                            //    groupControl.Controls.Add(barCodeControl);
                            
                    }
                    break;

                case DataFieldProperty.SystemPhysicalDataField:
                    SystemDataField systemDataField = (SystemDataField)Convert.ToByte(extendedCustomDataFieldInfo.DataFieldId);
                    TextEdit systemTextEdit = new TextEdit();
                    systemTextEdit.Properties.ReadOnly = true;
                    systemTextEdit.Location = new Point(controlX, y);
                    systemTextEdit.Width = DataFieldHelper.GetControlWidth(PhysicalDataFieldType.ArbitraryString);
                    systemTextEdit.TabIndex = index;
                    systemTextEdit.Tag = extendedCustomDataFieldInfo;
                    systemTextEdit.GotFocus += (sender, e) =>
                    {
                        SetToolTipOnControl(null);
                    };
                    switch (systemDataField)
                    {
                        case SystemDataField.UserName:
                            systemTextEdit.Text = CurrentUser.Instance.UserName;
                            break;

                        case SystemDataField.UserActualName:
                            systemTextEdit.Text = CurrentUser.Instance.UserActualName;
                            break;

                        case SystemDataField.UserTypeName:
                            systemTextEdit.Text = CurrentUser.Instance.UserTypeName;
                            break;

                        case SystemDataField.UserTypeCode:
                            systemTextEdit.Text = CurrentUser.Instance.UserTypeCode;
                            break;

                        case SystemDataField.DepName:
                            systemTextEdit.Text = CurrentUser.Instance.DepName;
                            break;

                        case SystemDataField.DepValue:
                            systemTextEdit.Text = CurrentUser.Instance.DepValue;
                            break;
                    }
                    controlWidth = systemTextEdit.Width;
                    groupControl.Controls.Add(systemTextEdit);
                    break;
            }
            /* 3. 提示标签 */
            if (extendedCustomDataFieldInfo.RequiredDataField)
            {
                int toolTipX = controlX + controlWidth + COMMON_WIDTH_OF_MARGIN_SPACE;
                AddToolTipLabel(toolTipX, y, dataFieldAuthority);
            }
        }

        /// <summary>
        /// 增加提示符号
        /// </summary>
        /// <param name="toolTipX"></param>
        /// <param name="controlY"></param>
        /// <param name="dataFieldAuthority"></param>
        private void AddToolTipLabel(int toolTipX, int controlY, DataFieldAuthority dataFieldAuthority)
        {
            Label lblToolTip = new Label { Height = SIZE_OF_REQUIRED_LABEL, Width = SIZE_OF_REQUIRED_LABEL, Text = "*", Location = new Point(toolTipX, controlY + TOOLTIP_CONTROL_HEIGHT_OFFSET), TabIndex = 1025 };
            if (currentEditedState)
            {
                if (dataFieldAuthority == DataFieldAuthority.ReadAndWrite)
                {
                    lblToolTip.ForeColor = Color.Red;
                }
                else
                {
                    lblToolTip.ForeColor = Color.DarkGray;
                }
            }
            else
            {
                lblToolTip.ForeColor = Color.DarkGray;
            }
            groupControl.Controls.Add(lblToolTip);
        }

        /// <summary>
        /// 加载关联数据
        /// </summary>
        /// <param name="searchLookUpEditWithGrid"></param>
        /// <param name="associatedDataFieldId"></param>
        /// <param name="content"></param>
        private void LoadAssociatedData(SearchLookUpEditWithGrid searchLookUpEditWithGrid, decimal associatedDataFieldId, string content)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();
            decimal associationId = associatedDataFieldContract.GetAssociationId(associatedDataFieldId);
            if (!string.IsNullOrWhiteSpace(content))
            {

                IList<AssociatedDataFieldInfo> associatedDataFieldInfos = associatedDataFieldContract.GetModelInfos(associationId);
                foreach (AssociatedDataFieldInfo associatedDataFieldInfo in associatedDataFieldInfos)
                {
                    BasedDataType basedDataType = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                    if (basedDataType == BasedDataType.String)
                    {
                        whereConditons.Add(new WhereConditon(associatedDataFieldInfo.PhysicalName, associatedDataFieldInfo.PhysicalName, DbType.String, string.Format("%{0}%", content),
                          DataFieldCondition.Like, DataFieldInnerRealtion.Or, DataFieldBracket.None, 0));
                    }
                }
            }
            int totalCount = 0;
            searchLookUpEditWithGrid.DataSource = customAssociationContract.GetAssociationData(associationId, searchLookUpEditWithGrid.PageSize * searchLookUpEditWithGrid.CurrentPageIndex,
                searchLookUpEditWithGrid.PageSize, whereConditons, ref totalCount).Tables[0];
            searchLookUpEditWithGrid.RecordCount = totalCount;
            string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            searchLookUpEditWithGrid.Columns[key].Visible = false;
        }

        /// <summary>
        /// 多选枚举选项发生变化
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="commonNode"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="groupControl"></param>
        private void MultiEnumEditValeChanged(decimal dataFieldId, IList<CommonNode> commonNodes, Control groupControl)
        {
            foreach (Control control in groupControl.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                ExtendedCustomDataFieldInfo customDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
                DataFieldProperty fieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                if (fieldProperty == DataFieldProperty.PhysicalDataField && customDataFieldInfo.ParentDataFieldId == dataFieldId)
                {
                    PhysicalDataFieldType dataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;

                    StringBuilder sb = new StringBuilder();
                    if (commonNodes != null && commonNodes.Count > 0)
                    {
                        IList<CustomEnumInfo> customEnumInfos = new List<CustomEnumInfo>();
                        foreach (CommonNode commonNode in commonNodes)
                        {
                            CustomEnumInfo customEnumInfo = customEnumContract.GetModelInfo(commonNode.NodeId);

                            switch (dataFieldType)
                            {
                                case PhysicalDataFieldType.EnumValue:
                                    sb.AppendFormat("{0},", customEnumInfo.EnumValue);
                                    break;

                                case PhysicalDataFieldType.FstAdditionalCode:
                                    sb.AppendFormat("{0},", customEnumInfo.FirstCode);
                                    break;

                                case PhysicalDataFieldType.ScdAdditionalCode:
                                    sb.AppendFormat("{0},", customEnumInfo.SecondCode);
                                    break;

                                case PhysicalDataFieldType.FstAdditionalString:
                                    sb.AppendFormat("{0},", customEnumInfo.FstAdditionalString);
                                    break;

                                case PhysicalDataFieldType.ScdAdditionalString:
                                    sb.AppendFormat("{0},", customEnumInfo.ScdAdditionalString);
                                    break;

                                case PhysicalDataFieldType.TrdAdditionalString:
                                    sb.AppendFormat("{0},", customEnumInfo.TrdAdditionalString);
                                    break;

                                case PhysicalDataFieldType.FourthAdditionalString:
                                    sb.AppendFormat("{0},", customEnumInfo.FourthAdditionalString);
                                    break;

                                case PhysicalDataFieldType.FifthAdditionalString:
                                    sb.AppendFormat("{0},", customEnumInfo.FifthAdditionalString);
                                    break;

                                case PhysicalDataFieldType.SixthAdditionalString:
                                    sb.AppendFormat("{0},", customEnumInfo.SixthAdditionalString);
                                    break;

                                case PhysicalDataFieldType.FstAdditionalInteger:
                                    sb.AppendFormat("{0},", DataConvertionHelper.EndowStringOfInt(customEnumInfo.FstAdditionalInteger));
                                    break;

                                case PhysicalDataFieldType.ScdAdditionalInteger:
                                    sb.AppendFormat("{0},", DataConvertionHelper.EndowStringOfInt(customEnumInfo.ScdAdditionalInteger));
                                    break;

                                case PhysicalDataFieldType.FstAdditionalDecimal:
                                    sb.AppendFormat("{0},", DataConvertionHelper.EndowStringOfDecimal(customEnumInfo.FstAdditionalDecimal));
                                    break;

                                case PhysicalDataFieldType.ScdAdditionalDecimal:
                                    sb.AppendFormat("{0},", DataConvertionHelper.EndowStringOfDecimal(customEnumInfo.ScdAdditionalDecimal));
                                    break;

                                default:
                                    throw new ArgumentException("不支持该枚举类型。");
                            }
                            if (sb.Length > 0)
                            {
                                sb.Remove(sb.Length - 1, 1);
                            }
                            TextEdit textEdit = (TextEdit)control;
                            textEdit.EditValue = sb.ToString();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 枚举选项发生变化
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="commonNode"></param>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="groupControl"></param>
        private void EnumEditValeChanged(decimal dataFieldId, CommonNode commonNode, PhysicalDataFieldType physicalDataFieldType, Control groupControl)
        {
            foreach (Control control in groupControl.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                ExtendedCustomDataFieldInfo customDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
                DataFieldProperty fieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                if (fieldProperty == DataFieldProperty.PhysicalDataField && customDataFieldInfo.ParentDataFieldId == dataFieldId)
                {
                    string value = string.Empty;
                    if (commonNode != null)
                    {
                        PhysicalDataFieldType dataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.TreeViewEnum:
                            case PhysicalDataFieldType.DropdownListEnum:
                                CustomEnumInfo customEnumInfo = customEnumContract.GetModelInfo(commonNode.NodeId);
                                switch (dataFieldType)
                                {
                                    case PhysicalDataFieldType.EnumValue:
                                        value = customEnumInfo.EnumValue;
                                        break;

                                    case PhysicalDataFieldType.FstAdditionalCode:
                                        value = customEnumInfo.FirstCode;
                                        break;

                                    case PhysicalDataFieldType.ScdAdditionalCode:
                                        value = customEnumInfo.SecondCode;
                                        break;

                                    case PhysicalDataFieldType.FstAdditionalString:
                                        value = customEnumInfo.FstAdditionalString;
                                        break;

                                    case PhysicalDataFieldType.ScdAdditionalString:
                                        value = customEnumInfo.ScdAdditionalString;
                                        break;

                                    case PhysicalDataFieldType.TrdAdditionalString:
                                        value = customEnumInfo.TrdAdditionalString;
                                        break;

                                    case PhysicalDataFieldType.FourthAdditionalString:
                                        value = customEnumInfo.FourthAdditionalString;
                                        break;

                                    case PhysicalDataFieldType.FifthAdditionalString:
                                        value = customEnumInfo.FifthAdditionalString;
                                        break;

                                    case PhysicalDataFieldType.SixthAdditionalString:
                                        value = customEnumInfo.SixthAdditionalString;
                                        break;

                                    case PhysicalDataFieldType.FstAdditionalInteger:
                                        value = DataConvertionHelper.EndowStringOfInt(customEnumInfo.FstAdditionalInteger);
                                        break;

                                    case PhysicalDataFieldType.ScdAdditionalInteger:
                                        value = DataConvertionHelper.EndowStringOfInt(customEnumInfo.ScdAdditionalInteger);
                                        break;

                                    case PhysicalDataFieldType.FstAdditionalDecimal:
                                        value = DataConvertionHelper.EndowStringOfDecimal(customEnumInfo.FstAdditionalDecimal);
                                        break;

                                    case PhysicalDataFieldType.ScdAdditionalDecimal:
                                        value = DataConvertionHelper.EndowStringOfDecimal(customEnumInfo.ScdAdditionalDecimal);
                                        break;

                                    default:
                                        throw new ArgumentException("不支持该枚举类型。");
                                }
                                break;

                            case PhysicalDataFieldType.DepartmentDropdownListEnum:
                            case PhysicalDataFieldType.DepartmentTreeViewEnum:
                            case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                                CustomDepartmentInfo customDepartmentInfo = customDepartmentContract.GetModelInfo(commonNode.NodeId);
                                switch (dataFieldType)
                                {
                                    case PhysicalDataFieldType.DepartmentValue:
                                        value = customDepartmentInfo.DepValue;
                                        break;

                                    case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                                        value = customDepartmentInfo.FirstCode;
                                        break;

                                    case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                                        value = customDepartmentInfo.SecondCode;
                                        break;
                                }
                                break;

                            default:
                                throw new ArgumentException("不支持该枚举类型。");
                        }
                    }
                    TextEdit textEdit = (TextEdit)control;
                    textEdit.EditValue = value;
                }
            }
        }

        /// <summary>
        /// 主关联字段的值发送变化
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="dataRow"></param>
        /// <param name="groupControl"></param>
        private void PrimaryAssociationValueChanged(decimal dataFieldId, DataRow dataRow, Control groupControl)
        {
            foreach (Control control in groupControl.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                IDictionary<string, string> dataFieldNameRelation = associatedDataFieldContract.GetDataFieldNameRelation(dataFieldId);
                ExtendedCustomDataFieldInfo customDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
                DataFieldProperty fieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                if (fieldProperty == DataFieldProperty.PhysicalDataField && customDataFieldInfo.ParentDataFieldId == dataFieldId)
                {
                    PhysicalDataFieldType dataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                    if (dataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                    {
                        TextEdit textEdit = (TextEdit)control;
                        if (dataRow != null)
                        {
                            if (dataFieldNameRelation.ContainsKey(customDataFieldInfo.PhysicalName))
                            {
                                textEdit.EditValue = dataRow[dataFieldNameRelation[customDataFieldInfo.PhysicalName]];
                            }
                            else
                            {
                                throw new ArgumentException("关联表的字段名称对应关系错误。");
                            }
                        }
                        else
                        {
                            textEdit.EditValue = null;
                        }
                    }
                }
            }
        }

        #endregion

        #region 加载数据

        /// <summary>
        /// 加载表格类型数据
        /// </summary>
        /// <param name="dataRow"></param>
        /// <param name="skipped"></param>
        public void LoadDataOnTable(DataRow dataRow, bool skipped)
        {
            if (dataRow == null) return;
            if (!skipped)
            {
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                dicTableIdAndRecordId.Add(customFormInfo.TableId, DataConvertionHelper.GetDecimal(dataRow[recordIdName]));
            }

            foreach (Control control in groupControl.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
                LoadDataOnControl(control, extendedCustomDataFieldInfo, dataRow);
            }
        }

        /// <summary>
        /// 在组合表上加载
        /// </summary>
        /// <param name="ds"></param>
        public void LoadDataOnCombinedTables(DataSet ds)
        {
            string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
            IList<CommonNode> commonNodeInfos = combinedTableContract.GetTables(customFormInfo.CombinedTableId);
            foreach (var commonNodeInfo in commonNodeInfos)
            {
                if (!dicTableIdAndRecordId.ContainsKey(commonNodeInfo.NodeId))
                {
                    string tableName = commonNodeInfo.NodeId.ToString();
                    if(ds.Tables.Contains(tableName) && ds.Tables[tableName].Rows.Count > 0)
                    {
                        decimal value = DataConvertionHelper.GetDecimal(ds.Tables[tableName].Rows[0][recordIdName]);
                        dicTableIdAndRecordId.Add(commonNodeInfo.NodeId, DataConvertionHelper.GetDecimal(value));
                    }
                }
            }
            foreach (Control control in groupControl.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
                string tableName = extendedCustomDataFieldInfo.TableId.ToString();
                if (ds.Tables.Contains(tableName) && ds.Tables[tableName].Rows.Count > 0)
                {
                    LoadDataOnControl(control, extendedCustomDataFieldInfo, ds.Tables[tableName].Rows[0]);
                }
            }
        }

        /// <summary>
        /// 在控件上加载
        /// </summary>
        /// <param name="control"></param>
        /// <param name="extendedCustomDataFieldInfo"></param>
        /// <param name="dataRow"></param>
        private void LoadDataOnControl(Control control, ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo, DataRow dataRow)
        {
            string physicalName = extendedCustomDataFieldInfo.PhysicalName;
            DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
            switch (dataFieldProperty)
            {
                case DataFieldProperty.LogicalDataField:
                    LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    switch (logicalDataFieldType)
                    {
                        case LogicalDataFieldType.DigitExpression:
                        case LogicalDataFieldType.StringExpression:
                            ((TextEdit)control).Text = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            break;

                        case LogicalDataFieldType.DateTimeExpression:
                            ((TextEdit)control).Text = DataConvertionHelper.EndowStringOfDate(DataConvertionHelper.GetConvertedDateTime(dataRow[physicalName]));
                            break;


                            //case LogicalDataFieldType.OneDimCode:
                            //case LogicalDataFieldType.TwoDimCode:
                            //case LogicalDataFieldType.UserName:
                            //    BarCodeControl barCodeControl = (BarCodeControl)control;
                            //    if (logicalDataFieldType == LogicalDataFieldType.TwoDimCode)
                            //    {
                            //        QRCodeGenerator qr = new QRCodeGenerator();
                            //        barCodeControl.Symbology = qr;
                            //    }
                            //    else
                            //    {
                            //        Code128Generator code128 = new Code128Generator();
                            //        code128.CharacterSet = Code128Charset.CharsetAuto;
                            //        barCodeControl.Symbology = code128;
                            //    }
                            //    barCodeControl.Text = DataConvertionHelper.GetString(dataRow[physicalName]);
                            //    break;
                    }
                    break;

                case DataFieldProperty.PhysicalDataField:
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                    switch (physicalDataFieldType)
                    {
                        case PhysicalDataFieldType.Boolean:
                            CheckEdit checkEdit = new CheckEdit();
                            ((CheckEdit)control).Checked = DataConvertionHelper.GetBoolean(dataRow[physicalName]);
                            break;

                        case PhysicalDataFieldType.Int32:
                        case PhysicalDataFieldType.Decimal:
                        case PhysicalDataFieldType.ArbitraryString:
                        case PhysicalDataFieldType.ExtendedArbitraryString:
                        case PhysicalDataFieldType.NumeralString:
                        case PhysicalDataFieldType.CharString:
                        case PhysicalDataFieldType.MixedString:
                            ((TextEdit)control).Text = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            break;

                        case PhysicalDataFieldType.EncryptedString:
                            ((TextEdit)control).Text = CryptographyHelper.Decrypt(DataConvertionHelper.GetConvertedString(dataRow[physicalName]));
                            break;

                        case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                        case PhysicalDataFieldType.YearAndMonthAndDay:
                        case PhysicalDataFieldType.YearAndMonth:
                        case PhysicalDataFieldType.MonthAndDay:
                            if (!DataConvertionHelper.IsNullValue(DataConvertionHelper.GetDateTime(dataRow[physicalName])))
                            {
                                ((DateEdit)control).EditValue = DataConvertionHelper.GetDateTime(dataRow[physicalName]);
                            }
                            break;

                        case PhysicalDataFieldType.Time:
                            if (!DataConvertionHelper.IsNullValue(DataConvertionHelper.GetDateTime(dataRow[physicalName])))
                            {
                                ((TimeEdit)control).EditValue = DataConvertionHelper.GetDateTime(dataRow[physicalName]);
                            }
                            break;

                        case PhysicalDataFieldType.Association:
                            if (customAssociationInfos.ContainsKey(extendedCustomDataFieldInfo.AssociatedDataFieldId))
                            {
                                CustomAssociationInfo customAssociationInfo = customAssociationInfos[extendedCustomDataFieldInfo.AssociatedDataFieldId];
                                ((SearchLookUpEditWithGrid)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            }
                            break;

                        case PhysicalDataFieldType.PrimaryAssociation:
                            if (customAssociationInfos.ContainsKey(extendedCustomDataFieldInfo.AssociatedDataFieldId))
                            {
                                CustomAssociationInfo customAssociationInfo = customAssociationInfos[extendedCustomDataFieldInfo.AssociatedDataFieldId];
                                if (customAssociationInfo.SuperAssociationEnabled)
                                {
                                    ((ComboBoxEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                                }
                                else
                                {
                                    AssociationShowMode associationShowMode = (AssociationShowMode)customAssociationInfo.ShowMode;
                                    string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                                    switch (associationShowMode)
                                    {
                                        case AssociationShowMode.Hierarchicy:
                                            ((LookUpEditWithGrid)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                                            break;

                                        case AssociationShowMode.Table:
                                            ((LookUpEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                                            break;
                                    }
                                }
                            }
                            break;

                        case PhysicalDataFieldType.DropdownListEnum:
                        case PhysicalDataFieldType.DepartmentDropdownListEnum:
                        case PhysicalDataFieldType.CscadeEnum:
                            ((ComboBoxEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            break;


                        case PhysicalDataFieldType.DepartmentTreeViewEnum:
                        case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                            ((TreeDropdownList)control).Value = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            break;

                        case PhysicalDataFieldType.TreeViewEnum:
                            if (superEnumIds.Contains(extendedCustomDataFieldInfo.EnumId))
                            {
                                ((ComboBoxEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);

                            }
                            else
                            {
                                ((TreeDropdownList)control).Value = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            }
                            break;

                        case PhysicalDataFieldType.MultiSelectedEnum:
                            ((CheckedComboBoxEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            break;

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
                        case PhysicalDataFieldType.DepartmentValue:
                        case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                        case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                            ((TextEdit)control).Text = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            break;

                        case PhysicalDataFieldType.SecondaryAssociation:
                            if (dataRow[physicalName].GetType() == typeof(DateTime))
                            {
                                if (dataRow[physicalName] != null && dataRow[physicalName] != DBNull.Value)
                                {
                                    DateTime dtValue = Convert.ToDateTime(dataRow[physicalName]);
                                    ((TextEdit)control).Text = dtValue.ToString("d");
                                }
                            }
                            else
                            {
                                ((TextEdit)control).Text = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            }                               
                            break;


                        case PhysicalDataFieldType.DocAttachment:
                        case PhysicalDataFieldType.PicAttachment:
                        case PhysicalDataFieldType.PDFAttachment:
                            DevExpressUploadFile uploadFile = (DevExpressUploadFile)control;
                            string fileName = DataConvertionHelper.GetString(dataRow[physicalName]);
                            uploadFile.FileName = fileName;
                            uploadFile.UserData = fileName;
                            break;
                    }
                    break;
            }
        }

        ///// <summary>
        ///// 加载数据
        ///// </summary>
        ///// <param name="dataRow"></param>
        //public void LoadData(DataRow dataRow)
        //{
        //    if (dataRow == null) return;
        //    string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
        //    FormType tableType = (FormType)customFormInfo.FormType;
        //    switch (tableType)
        //    {
        //        case FormType.View:
        //            decimal mainTableId = customViewContract.GetTableId(customFormInfo.ViewId);
        //            string keyName = string.Format("{0}_{1}", customViewContract.GetTablePhysicalName(customFormInfo.ViewId), recordIdName);
        //            if (!dicTableIdAndRecordId.ContainsKey(mainTableId))
        //            {
        //                dicTableIdAndRecordId.Add(mainTableId, DataConvertionHelper.GetDecimal(dataRow[keyName]));
        //            }
        //            IList<CustomViewAndTableInfo> customViewAndTableInfos = customViewContract.GetAssociatedTables(customFormInfo.ViewId);                    
        //            foreach (CustomViewAndTableInfo customViewAndTableInfo in customViewAndTableInfos)
        //            {
        //                if (!dicTableIdAndRecordId.ContainsKey(customViewAndTableInfo.TableId))
        //                {
        //                    string physicalName = string.Format("{0}_{1}", customTableContract.GetTablePhysicalName(customViewAndTableInfo.TableId), recordIdName);
        //                    dicTableIdAndRecordId.Add(customViewAndTableInfo.TableId, DataConvertionHelper.GetDecimal(dataRow[physicalName]));
        //                }
        //            }
        //            break;

        //        case FormType.Table:
        //            dicTableIdAndRecordId.Add(customFormInfo.TableId, DataConvertionHelper.GetDecimal(dataRow[recordIdName]));
        //            break;
        //    }
        //    foreach (Control control in groupControl.Controls)
        //    {
        //        if (control.Tag == null)
        //        {
        //            continue;
        //        }
        //        ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
        //        string physicalName = extendedCustomDataFieldInfo.PhysicalName;
        //        DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
        //        switch (dataFieldProperty)
        //        {
        //            case DataFieldProperty.LogicalDataField:
        //                LogicalDataFieldType logicalDataFieldType = (LogicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
        //                switch (logicalDataFieldType)
        //                {
        //                    case LogicalDataFieldType.DigitExpression:
        //                    case LogicalDataFieldType.StringExpression:
        //                        ((TextEdit)control).Text = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
        //                        break;                                

        //                    case LogicalDataFieldType.DateTimeExpression:
        //                        ((TextEdit)control).Text = DataConvertionHelper.EndowStringOfDate(DataConvertionHelper.GetConvertedDateTime(dataRow[physicalName]));
        //                        break;

                           
        //                    //case LogicalDataFieldType.OneDimCode:
        //                    //case LogicalDataFieldType.TwoDimCode:
        //                    //case LogicalDataFieldType.UserName:
        //                    //    BarCodeControl barCodeControl = (BarCodeControl)control;
        //                    //    if (logicalDataFieldType == LogicalDataFieldType.TwoDimCode)
        //                    //    {
        //                    //        QRCodeGenerator qr = new QRCodeGenerator();
        //                    //        barCodeControl.Symbology = qr;
        //                    //    }
        //                    //    else
        //                    //    {
        //                    //        Code128Generator code128 = new Code128Generator();
        //                    //        code128.CharacterSet = Code128Charset.CharsetAuto;
        //                    //        barCodeControl.Symbology = code128;
        //                    //    }
        //                    //    barCodeControl.Text = DataConvertionHelper.GetString(dataRow[physicalName]);
        //                    //    break;
        //                }
        //                break;

        //            case DataFieldProperty.PhysicalDataField:
        //                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
        //                switch (physicalDataFieldType)
        //                {
        //                    case PhysicalDataFieldType.Boolean:
        //                        CheckEdit checkEdit = new CheckEdit();
        //                        ((CheckEdit)control).Checked = DataConvertionHelper.GetBoolean(dataRow[physicalName]);
        //                        break;

        //                    case PhysicalDataFieldType.Int32:
        //                    case PhysicalDataFieldType.Decimal:
        //                    case PhysicalDataFieldType.ArbitraryString:
        //                    case PhysicalDataFieldType.ExtendedArbitraryString:
        //                    case PhysicalDataFieldType.NumeralString:
        //                    case PhysicalDataFieldType.CharString:
        //                    case PhysicalDataFieldType.MixedString:
        //                        ((TextEdit)control).Text = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
        //                        break;
                                
        //                    case PhysicalDataFieldType.EncryptedString:
        //                        ((TextEdit)control).Text = CryptographyHelper.Decrypt(DataConvertionHelper.GetConvertedString(dataRow[physicalName]));
        //                        break;

        //                    case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
        //                    case PhysicalDataFieldType.YearAndMonthAndDay:
        //                    case PhysicalDataFieldType.YearAndMonth:
        //                    case PhysicalDataFieldType.MonthAndDay:
        //                        if (!DataConvertionHelper.IsNullValue(DataConvertionHelper.GetDateTime(dataRow[physicalName])))
        //                        {
        //                            ((DateEdit)control).EditValue = DataConvertionHelper.GetDateTime(dataRow[physicalName]);
        //                        }
        //                        break;

        //                    case PhysicalDataFieldType.Time:
        //                        if (!DataConvertionHelper.IsNullValue(DataConvertionHelper.GetDateTime(dataRow[physicalName])))
        //                        {
        //                            ((TimeEdit)control).EditValue = DataConvertionHelper.GetDateTime(dataRow[physicalName]);
        //                        }
        //                        break;

        //                    case PhysicalDataFieldType.PrimaryAssociation:
        //                        if (customAssociationInfos.ContainsKey(extendedCustomDataFieldInfo.AssociatedDataFieldId))
        //                        {
        //                            CustomAssociationInfo customAssociationInfo = customAssociationInfos[extendedCustomDataFieldInfo.AssociatedDataFieldId];
        //                            if (customAssociationInfo.SuperAssociationEnabled)
        //                            {
        //                                ((ComboBoxEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
        //                            }
        //                            else
        //                            {
        //                                AssociationShowMode associationShowMode = (AssociationShowMode)customAssociationInfo.ShowMode;
        //                                string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
        //                                switch (associationShowMode)
        //                                {
        //                                    case AssociationShowMode.Hierarchicy:
        //                                        ((LookUpEditWithGrid)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
        //                                        break;

        //                                    case AssociationShowMode.Table:
        //                                        ((LookUpEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);                                               
        //                                        break;
        //                                }
        //                            }
        //                        }
        //                        break;

        //                    case PhysicalDataFieldType.DropdownListEnum:
        //                    case PhysicalDataFieldType.DepartmentDropdownListEnum:
        //                    case PhysicalDataFieldType.CscadeEnum:
        //                        ((ComboBoxEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
        //                        break;


        //                    case PhysicalDataFieldType.DepartmentTreeViewEnum:
        //                    case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
        //                        ((TreeDropdownList)control).Value = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
        //                        break;

        //                    case PhysicalDataFieldType.TreeViewEnum:
        //                        if (superEnumIds.Contains(extendedCustomDataFieldInfo.EnumId))
        //                        {
        //                            ((ComboBoxEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);

        //                        }
        //                        else
        //                        {
        //                            ((TreeDropdownList)control).Value = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);                                    
        //                        }
        //                        break;

        //                    case PhysicalDataFieldType.MultiSelectedEnum:
        //                        ((CheckedComboBoxEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
        //                        break;

        //                    case PhysicalDataFieldType.EnumValue:
        //                    case PhysicalDataFieldType.FstAdditionalCode:
        //                    case PhysicalDataFieldType.ScdAdditionalCode:
        //                    case PhysicalDataFieldType.FstAdditionalString:
        //                    case PhysicalDataFieldType.ScdAdditionalString:
        //                    case PhysicalDataFieldType.TrdAdditionalString:
        //                    case PhysicalDataFieldType.FourthAdditionalString:
        //                    case PhysicalDataFieldType.FifthAdditionalString:
        //                    case PhysicalDataFieldType.SixthAdditionalString:
        //                    case PhysicalDataFieldType.FstAdditionalInteger:
        //                    case PhysicalDataFieldType.ScdAdditionalInteger:
        //                    case PhysicalDataFieldType.FstAdditionalDecimal:
        //                    case PhysicalDataFieldType.ScdAdditionalDecimal:
        //                    case PhysicalDataFieldType.DepartmentValue:
        //                    case PhysicalDataFieldType.DepartmentFstAdditionalCode:
        //                    case PhysicalDataFieldType.DepartmentScdAdditionalCode:
        //                    case PhysicalDataFieldType.SecondaryAssociation:
        //                        ((TextEdit)control).Text = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
        //                        break;


        //                    case PhysicalDataFieldType.DocAttachment:
        //                    case PhysicalDataFieldType.PicAttachment:
        //                    case PhysicalDataFieldType.PDFAttachment:
        //                        DevExpressUploadFile uploadFile = (DevExpressUploadFile)control;
        //                        string fileName = DataConvertionHelper.GetString(dataRow[physicalName]);
        //                        uploadFile.FileName = fileName;
        //                        uploadFile.UserData = fileName;
        //                        break;
        //                }
        //                break;
        //        }
        //    }
        //}

        #endregion

        #region 提交数据

        /// <summary>
        /// 获得数据
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public IList<RecordEntity> GetCommonDataFields(ref string result)
        {
            result = string.Empty;

            IList<RecordEntity> recordEntities = new List<RecordEntity>();
            IDictionary<decimal, RecordEntity> dicRecordEntities = new Dictionary<decimal, RecordEntity>();
            foreach (Control control in groupControl.Controls)
            {
                if (control.Tag == null) continue;
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
                DataFieldProperty dataFieldProperty = (DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty;
                DataFieldAuthority dataFieldAuthority = (DataFieldAuthority)extendedCustomDataFieldInfo.AuthorityType;
                if ((dataFieldProperty == DataFieldProperty.SystemPhysicalDataField) || (dataFieldProperty == DataFieldProperty.LogicalDataField)
                    || (dataFieldAuthority == DataFieldAuthority.ReadOnly))
                {
                    continue;
                }
                PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType;
                object dataFieldValue = null;
                DbType dataFieldDataType = DbType.String;
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.Boolean:
                        dataFieldValue = ((CheckEdit)control).Checked;
                        dataFieldDataType = DbType.Boolean;
                        break;

                    case PhysicalDataFieldType.Int32:
                    case PhysicalDataFieldType.FstAdditionalInteger:
                    case PhysicalDataFieldType.ScdAdditionalInteger:
                        dataFieldDataType = DbType.Int32;
                        string textInt32 = ((TextEdit)control).Text.Trim();
                        if (!string.IsNullOrEmpty(textInt32))
                        {
                            //整数 
                            Regex regexInt32 = new Regex(@"^-?\d+$");
                            if (!regexInt32.IsMatch(textInt32))
                            {
                                control.Focus();
                                result = string.Format("{0}不能为非整数。", extendedCustomDataFieldInfo.LogicalName);
                                return null;
                            }
                            //超过范围转换失败                                
                            if (DataConvertionHelper.IsNullValue(DataConvertionHelper.GetConvertedInt(textInt32)))
                            {
                                control.Focus();
                                result = string.Format("{0}的整数值的超出了整数限制范围(-2147483648~2147483647)。", extendedCustomDataFieldInfo.LogicalName);
                                return null;
                            }
                            dataFieldValue = DataConvertionHelper.GetConvertedInt(textInt32);
                        }
                        break;

                    case PhysicalDataFieldType.Decimal:
                    case PhysicalDataFieldType.FstAdditionalDecimal:
                    case PhysicalDataFieldType.ScdAdditionalDecimal:
                        dataFieldDataType = DbType.Decimal;
                        string textDecimal = ((TextEdit)control).Text.Trim();
                        if (!string.IsNullOrEmpty(textDecimal))
                        {
                            //浮点数 
                            Regex regexDecimal = new Regex(@"^(-?\d+)(\.\d+)?$");
                            if (!regexDecimal.IsMatch(textDecimal))
                            {
                                control.Focus();
                                result = string.Format("{0}不能为非实数。", extendedCustomDataFieldInfo.LogicalName);
                                return null;
                            }
                            if (textDecimal.IndexOf('.') > 0 && textDecimal.Length > 19 || textDecimal.IndexOf('.') < 0 && textDecimal.Length > 18)
                            {
                                control.Focus();
                                result = string.Format("{0}的实数长度限制的范围(0~18位，不包括小数点)。", extendedCustomDataFieldInfo.LogicalName);
                                return null;
                            }
                            dataFieldValue = DataConvertionHelper.GetConvertedDecimal(textDecimal);
                        }
                        break;

                    case PhysicalDataFieldType.ArbitraryString:
                    case PhysicalDataFieldType.ExtendedArbitraryString:
                    case PhysicalDataFieldType.NumeralString:
                    case PhysicalDataFieldType.CharString:
                    case PhysicalDataFieldType.MixedString:
                    case PhysicalDataFieldType.EncryptedString:
                    case PhysicalDataFieldType.EnumValue:
                    case PhysicalDataFieldType.FstAdditionalCode:
                    case PhysicalDataFieldType.ScdAdditionalCode:
                    case PhysicalDataFieldType.FstAdditionalString:
                    case PhysicalDataFieldType.ScdAdditionalString:
                    case PhysicalDataFieldType.TrdAdditionalString:
                    case PhysicalDataFieldType.FourthAdditionalString:
                    case PhysicalDataFieldType.FifthAdditionalString:
                    case PhysicalDataFieldType.SixthAdditionalString:
                    case PhysicalDataFieldType.DepartmentValue:
                    case PhysicalDataFieldType.DepartmentFstAdditionalCode:
                    case PhysicalDataFieldType.DepartmentScdAdditionalCode:
                        dataFieldDataType = DbType.String;
                        string textString = ((TextEdit)control).Text.Trim();
                        if (!string.IsNullOrEmpty(textString))
                        {
                            switch (physicalDataFieldType)
                            {
                                case PhysicalDataFieldType.NumeralString:
                                    //整数 
                                    Regex numeralRegex = new Regex(@"^-?\d+$");
                                    if (!numeralRegex.IsMatch(textString))
                                    {
                                        control.Focus();
                                        result = string.Format("{0}只能为数字组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                        return null;
                                    }
                                    break;

                                case PhysicalDataFieldType.CharString:
                                    //由26个英文字母组成的字符串 
                                    Regex charRegex = new Regex(@"^[A-Za-z]+$");
                                    if (!charRegex.IsMatch(textString))
                                    {
                                        control.Focus();
                                        result = string.Format("{0}只能为由26个英文字母组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                        return null;
                                    }
                                    break;

                                case PhysicalDataFieldType.MixedString:
                                    //由数字和26个英文字母组成的字符串  
                                    Regex mixedRegex = new Regex(@"^[A-Za-z0-9]+$");
                                    if (!mixedRegex.IsMatch(textString))
                                    {
                                        control.Focus();
                                        result = string.Format("{0}只能为由数字和26个英文字母组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                        return null;
                                    }
                                    break;

                                case PhysicalDataFieldType.ExtendedArbitraryString:
                                case PhysicalDataFieldType.ArbitraryString:
                                case PhysicalDataFieldType.EncryptedString:
                                    if (!string.IsNullOrWhiteSpace(extendedCustomDataFieldInfo.RegexExpression))
                                    {
                                        Regex regex = new Regex(extendedCustomDataFieldInfo.RegexExpression);
                                        if (!regex.IsMatch(textString))
                                        {
                                            control.Focus();
                                            result = string.Format("{0}不符合要求的格式。", extendedCustomDataFieldInfo.LogicalName);
                                            return null;
                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                            if (physicalDataFieldType == PhysicalDataFieldType.EncryptedString)
                            {
                                dataFieldValue = CryptographyHelper.Encrypt(textString);
                            }
                            else
                            {
                                dataFieldValue = textString;
                            }
                        }
                        break;

                    case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                    case PhysicalDataFieldType.YearAndMonthAndDay:
                    case PhysicalDataFieldType.YearAndMonth:
                    case PhysicalDataFieldType.MonthAndDay:
                        DateEdit dateEdit = (DateEdit)control;
                        if (dateEdit.EditValue != null)
                        {
                            switch (physicalDataFieldType)
                            {
                                case PhysicalDataFieldType.YearAndMonth:
                                    DateTime dtValue = DateTime.Parse(string.Format("{0}-{1}-01", dateEdit.DateTime.Year, dateEdit.DateTime.Month));
                                    if (DataConvertionHelper.IsNullValue(dtValue) || dtValue <= SqlDateTime.MinValue.Value || dtValue >= SqlDateTime.MaxValue.Value)
                                    {
                                        control.Focus();
                                        result = string.Format("{0}选择错误。", extendedCustomDataFieldInfo.LogicalName);
                                        return null;
                                    }
                                    dataFieldValue = dtValue;
                                    break;

                                case PhysicalDataFieldType.YearAndMonthAndDay:
                                    dataFieldValue = DateTime.Parse(dateEdit.DateTime.ToShortDateString());
                                    break;

                                case PhysicalDataFieldType.MonthAndDay:
                                    dataFieldValue = DateTime.Parse(string.Format("{0}-{1}-{2}", AppSettingHelper.Year, dateEdit.DateTime.Month, dateEdit.DateTime.Day));
                                    break;

                                default:
                                    dataFieldValue = dateEdit.EditValue;
                                    break;
                            }
                        }
                        dataFieldDataType = DbType.DateTime;
                        break;

                    case PhysicalDataFieldType.Time:
                        dataFieldDataType = DbType.DateTime;
                        TimeEdit timeEdit = (TimeEdit)control;
                        if (timeEdit.EditValue != null)
                        {
                            dataFieldValue = DateTime.Parse(string.Format("{0} {1}", AppSettingHelper.YearMonthDay, timeEdit.Time.ToLongTimeString()));
                        }
                        break;

                    case PhysicalDataFieldType.MultiSelectedEnum:
                        dataFieldDataType = DbType.String;
                        CheckedComboBoxEdit checkedComboBoxEdit = (CheckedComboBoxEdit)control;
                        dataFieldValue = checkedComboBoxEdit.EditValue;
                        break;

                    case PhysicalDataFieldType.DropdownListEnum:
                    case PhysicalDataFieldType.DepartmentDropdownListEnum:
                    case PhysicalDataFieldType.CscadeEnum:
                        dataFieldDataType = DbType.String;
                        dataFieldValue = ((ComboBoxEdit)control).Text.Trim();
                        break;


                    case PhysicalDataFieldType.DepartmentTreeViewEnum:
                    case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                        dataFieldDataType = DbType.String;
                        dataFieldValue = ((TreeDropdownList)control).Text;
                        break;

                    case PhysicalDataFieldType.TreeViewEnum:
                        dataFieldDataType = DbType.String;
                        if (superEnumIds.Contains(extendedCustomDataFieldInfo.EnumId))
                        {
                            dataFieldValue = ((ComboBoxEdit)control).Text.Trim();

                        }
                        else
                        {
                            dataFieldValue = ((TreeDropdownList)control).Text;
                        }
                        break;

                    case PhysicalDataFieldType.Association:
                        if (customAssociationInfos.ContainsKey(extendedCustomDataFieldInfo.AssociatedDataFieldId))
                        {
                            BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                            dataFieldDataType = DataFieldHelper.GetDbType(basedDataType);                           
                            dataFieldValue = ((SearchLookUpEditWithGrid)control).EditValue;
                        }
                        break;

                    case PhysicalDataFieldType.PrimaryAssociation:
                        if (customAssociationInfos.ContainsKey(extendedCustomDataFieldInfo.AssociatedDataFieldId))
                        {
                            BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                            dataFieldDataType = DataFieldHelper.GetDbType(basedDataType);
                            CustomAssociationInfo customAssociationInfo = customAssociationInfos[extendedCustomDataFieldInfo.AssociatedDataFieldId];
                            if (customAssociationInfo.SuperAssociationEnabled)
                            {
                                dataFieldValue = ((ComboBoxEdit)control).EditValue;
                            }
                            else
                            {
                                AssociationShowMode associationShowMode = (AssociationShowMode)customAssociationInfo.ShowMode;
                                string key = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                                switch (associationShowMode)
                                {
                                    case AssociationShowMode.Hierarchicy:
                                        dataFieldValue = ((LookUpEditWithGrid)control).EditValue;
                                        break;

                                    case AssociationShowMode.Table:
                                        dataFieldValue = ((LookUpEdit)control).EditValue;
                                        break;
                                }
                            }
                        }
                        break;

                    case PhysicalDataFieldType.SecondaryAssociation:
                        string associatedTextString = ((TextEdit)control).Text.Trim();
                        if (!string.IsNullOrWhiteSpace(associatedTextString))
                        {
                            BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                            dataFieldValue = associatedTextString;
                        }
                        break;

                    case PhysicalDataFieldType.DocAttachment:
                    case PhysicalDataFieldType.PicAttachment:
                    case PhysicalDataFieldType.PDFAttachment:
                        dataFieldDataType = DbType.Object;
                        DevExpressUploadFile devExpressUploadFile = (DevExpressUploadFile)control;
                        if (!string.IsNullOrWhiteSpace(devExpressUploadFile.FileName))
                        {
                            if (Path.IsPathRooted(devExpressUploadFile.FileName))
                            {
                                byte[] data = null;
                                switch (physicalDataFieldType)
                                {
                                    case PhysicalDataFieldType.DocAttachment:
                                    case PhysicalDataFieldType.PDFAttachment:
                                        if (physicalDataFieldType == PhysicalDataFieldType.DocAttachment)
                                        {
                                            if (!FileFormatHelper.VerfiyDocFormat(devExpressUploadFile.FileName))
                                            {
                                                control.Focus();
                                                result = "文件格式只能为：pdf(*.pdf), rar(*.rar, *.zip), doc(*.doc,*.docx), xls(*.xls,*.xlsx) 或者 ppt(*.ppt) 类型。";
                                                return null;
                                            }
                                        }
                                        else
                                        {
                                            if (!FileFormatHelper.VerfiyPDFFormat(devExpressUploadFile.FileName))
                                            {
                                                control.Focus();
                                                result = "文件格式只能为：pdf(*.pdf) 类型。";
                                                return null;
                                            }
                                        }
                                        using (FileStream fs = new FileStream(devExpressUploadFile.FileName, FileMode.Open, FileAccess.Read))
                                        {
                                            BinaryReader r = new BinaryReader(fs);
                                            data = r.ReadBytes((int)fs.Length);
                                        }
                                        if (data.Length > AppSettingHelper.DefaultDocSize)
                                        {
                                            control.Focus();
                                            result = string.Format("{0}的文件大小不能超过 {1} MB.", extendedCustomDataFieldInfo.LogicalName, AppSettingHelper.DefaultDocSize / (1024 * 1024));
                                            return null;
                                        }
                                        break;

                                    case PhysicalDataFieldType.PicAttachment:
                                        if (!FileFormatHelper.VerfiyImageFormat(devExpressUploadFile.FileName))
                                        {
                                            control.Focus();
                                            result = "图片格式只能为：JPEG(*.JPG;*.JPEG)，GIF(*.GIF), PNG(*.PNG) 或者 BMP(*.BMP)。";
                                            return null;
                                        }
                                        using (MemoryStream ms = new MemoryStream())
                                        {
                                            ImageFormat imageFormat = FileFormatHelper.GetImageFormat(devExpressUploadFile.FileName.Substring(devExpressUploadFile.FileName.LastIndexOf('.') + 1).ToUpper());
                                            Image img = Image.FromFile(devExpressUploadFile.FileName);
                                            img.Save(ms, imageFormat);
                                            data = ms.ToArray();
                                        }
                                        if (data.Length > AppSettingHelper.DefaultPictureSize)
                                        {
                                            control.Focus();
                                            result = string.Format("{0}的图片大小不能超过 {1} MB.", extendedCustomDataFieldInfo.LogicalName, AppSettingHelper.DefaultPictureSize / (1024 * 1024));
                                            return null;
                                        }
                                        break;
                                }
                                if (data.Length == 0)
                                {
                                    control.Focus();
                                    result = "文件大小不能为0。";
                                    return null;
                                }
                                else
                                {
                                    dataFieldValue = new UpLoadFileInfo(Path.GetFileName(devExpressUploadFile.FileName), string.Empty, data);
                                }
                            }
                            else
                            {
                                continue;
                            }
                        }
                        else
                        {
                            /* 获取源文件名称，供删除时使用 */
                            string sourceFileName = DataConvertionHelper.GetString(devExpressUploadFile.UserData);
                            if (!string.IsNullOrWhiteSpace(sourceFileName))
                            {
                                dataFieldValue = new UpLoadFileInfo(string.Empty, sourceFileName, null);
                            }
                            else
                            {
                                continue;
                            }
                        }
                        break;
                }
                RecordEntity recordEntity = null;
                if (dicRecordEntities.ContainsKey(extendedCustomDataFieldInfo.TableId))
                {
                    recordEntity = dicRecordEntities[extendedCustomDataFieldInfo.TableId];
                }
                else
                {
                    decimal recordId = decimal.MinValue;
                    if (dicTableIdAndRecordId.ContainsKey(extendedCustomDataFieldInfo.TableId))
                    {
                        recordId = dicTableIdAndRecordId[extendedCustomDataFieldInfo.TableId];
                    }  
                    recordEntity = new RecordEntity()
                    {
                        TableId = extendedCustomDataFieldInfo.TableId,
                        BusinessEnabled = customFormInfo.BusinessEnabled,
                        CurrentState = CurrentState.Current,                        
                        RecordId = recordId
                    };
                    dicRecordEntities.Add(extendedCustomDataFieldInfo.TableId, recordEntity);
                }
                bool relationship = AuthorityHelper.CheckAuthority(extendedCustomDataFieldInfo.DataFieldSetting, (byte)DataFieldSetting.Correlation);
                recordEntity.CommonDataFields.Add(new CommonDataField(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.PhysicalName, dataFieldValue, dataFieldDataType, relationship));
                if (extendedCustomDataFieldInfo.RequiredDataField && dataFieldValue == null)
                {
                    result = string.Format("{0}不能为空。", extendedCustomDataFieldInfo.LogicalName);
                    break;
                }
            }
            foreach (KeyValuePair<decimal, RecordEntity> keyValue in dicRecordEntities)
            {
                recordEntities.Add(keyValue.Value);
            }

            return recordEntities;
        }

        #endregion
    }
}
