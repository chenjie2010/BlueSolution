//-----------------------------------------------------------------------------------------
// 模块编号：
// 文件名: BusinessTableBase.cs
// 描述: 窗体表格数据处理基类
// 作者：ChenJie 
// 编写日期：2018/09/06
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

namespace Blue.WindowsFormsClient.Common
{
    /// <summary>
    /// 窗体表格数据处理基类
    /// </summary>
    public class DataTableBusiness
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

        private readonly ICustomTableContract customTableContract;
        private readonly ICustomDataFieldContract customDataFieldContract;
        private readonly IAssociatedDataFieldContract associatedDataFieldContract;
        private readonly ICustomAssociationContract customAssociationContract;
        private readonly ICustomEnumContract customEnumContract;
        private readonly ICustomDepartmentContract customDepartmentContract;

        #endregion

        #region 私有变量

        /// <summary>
        /// 表分类
        /// </summary>
        private TableCategroy tableCategroy = TableCategroy.Single;

        /// <summary>
        /// 是否启动业务模式
        /// </summary>
        private readonly bool businessEnabled = false;

        /// <summary>
        /// 实例编号
        /// </summary>
        private decimal instanceId = decimal.MinValue;

        /// <summary>
        /// 当前用户
        /// </summary>
        private CommonUserInfo currentCommonUserInfo = null;

        /// <summary>
        /// 系统字段设置
        /// </summary>
        private Int64 systemDataFieldSetting = 0;

        /// <summary>
        /// 保存时是否更新其他的表的字段（关联、联动两大类）
        /// </summary>
        private readonly bool dataFieldRealitonIncluded = false;

        /// <summary>
        /// 超大枚举缓存
        /// </summary>
        private readonly IList<decimal> superEnumIds;

        /// <summary>
        /// 编辑状态
        /// </summary>
        private readonly bool formReadOnly;

        /// <summary>
        /// 主关联缓存
        /// </summary>
        private readonly Dictionary<decimal, CustomAssociationInfo> customAssociationInfos;
              
        /// <summary>
        /// 控件
        /// </summary>
        private readonly Control groupControl;

        /// <summary>
        /// 设置提示窗口
        /// </summary>
        private SetToolTipOnControlDelegate SetToolTipOnControl = null;

        /// <summary>
        /// 关联数据业务
        /// </summary>
        private RelaitonDataBusiness relationDataBusiness = null;

        /// <summary>
        /// 单选枚举选择节点缓存
        /// </summary>
        private Dictionary<decimal, CommonNode> enumCommonNodeCache = null;

        /// <summary>
        /// 多选枚举选择节点缓存
        /// </summary>
        private Dictionary<decimal, IList<CommonNode>> multiEnumCommonNodeCache = null;

        /// <summary>
        /// 关联选择编号缓存
        /// </summary>
        private Dictionary<decimal, decimal> associatedRecordIdCache = null;

        /// <summary>
        /// 数据是否在加载中
        /// </summary>
        private bool isLoading = false;

        #endregion

        #region 成员变量
        
        /// <summary>
        /// 记录项
        /// </summary>
        private List<RecordItem> _recordRelation = null;

        /// <summary>
        /// 表单编号：表单包含数据表或者组合表
        /// </summary>
        private decimal _formId = decimal.MinValue;

        #endregion

        #region 属性

        /// <summary>
        /// 实例编号
        /// </summary>
        public decimal InstanceId
        {
            get
            {
                return instanceId;
            }
        }

        /// <summary>
        /// 窗体编号
        /// </summary>
        public decimal FormId
        {
            get
            {
                return _formId;
            }
            set
            {
                _formId = value;
            }
        }

        /// <summary>
        /// 记录项
        /// </summary>
        public List<RecordItem> RecordRelation
        {
            get
            {
                return _recordRelation;
            }
            set
            {
                _recordRelation = value;
            }
        }
        #endregion

        #region 构造函数        

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="tableFormReadOnly"></param>
        /// <param name="dataFieldSetting"></param>
        /// <param name="dataFieldContract"></param>
        /// <param name="asDataFieldContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="enumContract"></param>
        /// <param name="departmentContract"></param>
        /// <param name="control"></param>
        /// <param name="memoEdit"></param>
        public DataTableBusiness(CommonUserInfo commonUserInfo, bool tableFormReadOnly, Int64 dataFieldSetting, ICustomTableContract tableContract, ICustomDataFieldContract dataFieldContract, IAssociatedDataFieldContract asDataFieldContract,
            ICustomAssociationContract associationContract, ICustomEnumContract enumContract, ICustomDepartmentContract departmentContract, Control control, MemoEdit memoEdit)
            : this(decimal.MinValue, decimal.MinValue, commonUserInfo, false, tableFormReadOnly, dataFieldSetting, true, tableContract, dataFieldContract, asDataFieldContract, associationContract, enumContract, departmentContract, control, memoEdit)

        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableFormReadOnly"></param>
        /// <param name="dataFieldSetting"></param>
        /// <param name="fieldRealitonIncluded"></param>
        /// <param name="dataFieldContract"></param>
        /// <param name="asDataFieldContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="enumContract"></param>
        /// <param name="departmentContract"></param>
        /// <param name="control"></param>
        /// <param name="memoEdit"></param>
        public DataTableBusiness(CommonUserInfo commonUserInfo, bool tableFormReadOnly, Int64 dataFieldSetting, bool fieldRealitonIncluded, ICustomTableContract tableContract, ICustomDataFieldContract dataFieldContract, IAssociatedDataFieldContract asDataFieldContract,
            ICustomAssociationContract associationContract, ICustomEnumContract enumContract, ICustomDepartmentContract departmentContract, Control control, MemoEdit memoEdit)
            : this(decimal.MinValue, decimal.MinValue, commonUserInfo, false, tableFormReadOnly, dataFieldSetting, fieldRealitonIncluded, tableContract, dataFieldContract, asDataFieldContract, associationContract, enumContract, departmentContract, control, memoEdit)

        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="commonUserInfo"></param>
        /// <param name="tableFormReadOnly"></param>
        /// <param name="dataFieldSetting"></param>
        /// <param name="dataFieldContract"></param>
        /// <param name="asDataFieldContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="enumContract"></param>
        /// <param name="departmentContract"></param>
        /// <param name="control"></param>
        /// <param name="memoEdit"></param>
        public DataTableBusiness(decimal formId, CommonUserInfo commonUserInfo, bool tableFormReadOnly, Int64 dataFieldSetting, ICustomTableContract tableContract, ICustomDataFieldContract dataFieldContract, IAssociatedDataFieldContract asDataFieldContract,
            ICustomAssociationContract associationContract, ICustomEnumContract enumContract, ICustomDepartmentContract departmentContract, Control control, MemoEdit memoEdit)
            : this(decimal.MinValue, formId, commonUserInfo, false, tableFormReadOnly, dataFieldSetting, true, tableContract, dataFieldContract, asDataFieldContract, associationContract, enumContract, departmentContract, control, memoEdit)

        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableFormReadOnly"></param>
        /// <param name="dataFieldSetting"></param>
        /// <param name="fieldRealitonIncluded"></param>
        /// <param name="dataFieldContract"></param>
        /// <param name="asDataFieldContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="enumContract"></param>
        /// <param name="departmentContract"></param>
        /// <param name="control"></param>
        /// <param name="memoEdit"></param>
        public DataTableBusiness(decimal formId, CommonUserInfo commonUserInfo, bool tableFormReadOnly, Int64 dataFieldSetting, bool fieldRealitonIncluded, ICustomTableContract tableContract, ICustomDataFieldContract dataFieldContract, IAssociatedDataFieldContract asDataFieldContract,
            ICustomAssociationContract associationContract, ICustomEnumContract enumContract, ICustomDepartmentContract departmentContract, Control control, MemoEdit memoEdit)
            : this(decimal.MinValue, formId, commonUserInfo, false, tableFormReadOnly, dataFieldSetting, fieldRealitonIncluded, tableContract, dataFieldContract, asDataFieldContract, associationContract, enumContract, departmentContract, control, memoEdit)

        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="businessInstanceId"></param>
        /// <param name="tableFormReadOnly"></param>
        /// <param name="dataFieldSetting"></param>
        /// <param name="dataFieldContract"></param>
        /// <param name="asDataFieldContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="enumContract"></param>
        /// <param name="departmentContract"></param>
        /// <param name="control"></param>
        /// <param name="memoEdit"></param>
        public DataTableBusiness(decimal businessInstanceId, decimal formId, CommonUserInfo commonUserInfo, bool tableFormReadOnly, Int64 dataFieldSetting, ICustomTableContract tableContract, ICustomDataFieldContract dataFieldContract, IAssociatedDataFieldContract asDataFieldContract,
            ICustomAssociationContract associationContract, ICustomEnumContract enumContract, ICustomDepartmentContract departmentContract, Control control, MemoEdit memoEdit) 
            : this(businessInstanceId, formId, commonUserInfo, true, tableFormReadOnly, dataFieldSetting, true, tableContract, dataFieldContract, asDataFieldContract, associationContract, enumContract, departmentContract, control, memoEdit)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="businessInstanceId"></param>
        /// <param name="commonUserInfo"></param>
        /// <param name="tableFormReadOnly"></param>
        /// <param name="dataFieldSetting"></param>
        /// <param name="fieldRealitonIncluded"></param>
        /// <param name="dataFieldContract"></param>
        /// <param name="asDataFieldContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="enumContract"></param>
        /// <param name="departmentContract"></param>
        /// <param name="control"></param>
        /// <param name="memoEdit"></param>
        public DataTableBusiness(decimal businessInstanceId, decimal formId, CommonUserInfo commonUserInfo, bool tableFormReadOnly, Int64 dataFieldSetting, bool fieldRealitonIncluded, ICustomTableContract tableContract, ICustomDataFieldContract dataFieldContract, IAssociatedDataFieldContract asDataFieldContract,
            ICustomAssociationContract associationContract, ICustomEnumContract enumContract, ICustomDepartmentContract departmentContract, Control control, MemoEdit memoEdit)
            : this(businessInstanceId, formId, commonUserInfo, true, tableFormReadOnly, dataFieldSetting, fieldRealitonIncluded, tableContract, dataFieldContract, asDataFieldContract, associationContract, enumContract, departmentContract, control, memoEdit)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="businessInstanceId"></param>
        /// <param name="commonUserInfo"></param>
        /// <param name="instanceBusinessEnabled"></param>
        /// <param name="tableFormReadOnly"></param>
        /// <param name="dataFieldSetting"></param>
        /// <param name="fieldRealitonIncluded"></param>
        /// <param name="dataFieldContract"></param>
        /// <param name="asDataFieldContract"></param>
        /// <param name="associationContract"></param>
        /// <param name="enumContract"></param>
        /// <param name="departmentContract"></param>
        /// <param name="control"></param>
        /// <param name="memoEdit"></param>
        private DataTableBusiness(decimal businessInstanceId, decimal formId, CommonUserInfo commonUserInfo, bool instanceBusinessEnabled, bool tableFormReadOnly, Int64 dataFieldSetting, bool fieldRealitonIncluded, ICustomTableContract tableContract, ICustomDataFieldContract dataFieldContract, IAssociatedDataFieldContract asDataFieldContract,
            ICustomAssociationContract associationContract, ICustomEnumContract enumContract, ICustomDepartmentContract departmentContract, Control control, MemoEdit memoEdit)
        {          
            formReadOnly = tableFormReadOnly;
            instanceId = businessInstanceId;
            _formId = formId;
            currentCommonUserInfo = commonUserInfo;
            systemDataFieldSetting = dataFieldSetting;
            businessEnabled = instanceBusinessEnabled;
            dataFieldRealitonIncluded = fieldRealitonIncluded;
            customTableContract = tableContract;
            customDataFieldContract = dataFieldContract;
            associatedDataFieldContract = asDataFieldContract;
            customAssociationContract = associationContract;
            customEnumContract = enumContract;
            customDepartmentContract = departmentContract;
            relationDataBusiness = new RelaitonDataBusiness(customDataFieldContract, customAssociationContract, customEnumContract,
                    associatedDataFieldContract, customDepartmentContract);
            groupControl = control;
            _recordRelation = new List<RecordItem>();
            superEnumIds = new List<decimal>();
            customAssociationInfos = new Dictionary<decimal, CustomAssociationInfo>();
            enumCommonNodeCache = new Dictionary<decimal, CommonNode>();
            multiEnumCommonNodeCache = new Dictionary<decimal, IList<CommonNode>>();
            associatedRecordIdCache = new Dictionary<decimal, decimal>();
            SetToolTipOnControl = (extendedCustomDataFieldInfo) =>
            {
                if (extendedCustomDataFieldInfo != null)
                {
                    memoEdit.Text = string.Format("{0}提示：{1}", extendedCustomDataFieldInfo.LogicalName, extendedCustomDataFieldInfo.Tooltip);
                }
            };
        }

        #endregion

        #region 创建控件

        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="extendedCustomDataFieldInfos"></param>
        /// <param name="formShowStyleSetting"></param>
        /// <param name="multiTextBoxCount"></param>
        /// <param name="refreshForm"></param>
        /// <returns></returns>
        public int CreateControls(IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos, FormShowStyleSetting formShowStyleSetting, ref int multiTextBoxCount)
        {
            return CreateControls(extendedCustomDataFieldInfos, formShowStyleSetting, ref multiTextBoxCount, null);
        }

        /// <summary>
        /// 创建控件
        /// </summary>
        /// <param name="extendedCustomDataFieldInfos"></param>
        /// <param name="formShowStyleSetting"></param>
        /// <param name="multiTextBoxCount"></param>
        /// <param name="refreshForm"></param>
        /// <returns></returns>
        public int CreateControls(IList<ExtendedCustomDataFieldInfo> extendedCustomDataFieldInfos, FormShowStyleSetting formShowStyleSetting, ref int multiTextBoxCount, RefreshFormDelegate refreshForm)
        {            
            int dataFieldIndex = AddSystemDataField(multiTextBoxCount, formShowStyleSetting);
            int lastDataFieldIndex = 0;
            foreach (ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo in extendedCustomDataFieldInfos)
            {
                AddUserControl(dataFieldIndex, multiTextBoxCount, formShowStyleSetting, extendedCustomDataFieldInfo, refreshForm);                
                dataFieldIndex++;
                /*  统计该行是否包含多行文本框 */
                if (((DataFieldProperty)extendedCustomDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField) &&
                    ((PhysicalDataFieldType)extendedCustomDataFieldInfo.DataFieldType == PhysicalDataFieldType.ExtendedArbitraryString))
                {
                    if (((lastDataFieldIndex / formShowStyleSetting.CountForEachRow) != (dataFieldIndex / formShowStyleSetting.CountForEachRow)))
                    {
                        multiTextBoxCount++;
                    }
                    lastDataFieldIndex = dataFieldIndex;
                }                
            }

            return dataFieldIndex;
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
            IList<ExtendedCustomDataFieldInfo> systemExtendedCustomDataFieldInfos = CommonBussinessHelper.GetSystemDataFieldInfos(systemDataFieldSetting);
            for (int index = 0; index < systemExtendedCustomDataFieldInfos.Count; index++)
            {
                AddUserControl(index, multiTextBoxCount, formShowStyleSetting, systemExtendedCustomDataFieldInfos[index], null);
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
        private void AddUserControl(int index, int multiTextBoxCount, FormShowStyleSetting formShowStyleSetting, ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo, RefreshFormDelegate refreshForm)
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
            AddUserControl(extendedCustomDataFieldInfo, formShowStyleSetting, index, x, y, refreshForm);
        }

        /// <summary>
        /// 增加用户控件
        /// </summary>
        /// <param name="extendedCustomDataFieldInfo"></param>
        /// <param name="formShowStyleSetting"></param>
        /// <param name="index"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="refreshForm"></param>
        private void AddUserControl(ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo, FormShowStyleSetting formShowStyleSetting, int index, int x, int y, RefreshFormDelegate refreshForm)
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
                    if (formReadOnly || DataFieldHelper.IsReadOnly(physicalDataFieldType))
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
                            checkEdit.EditValueChanged += (sender, e) =>
                            {
                                refreshForm?.Invoke();
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
                            digitalTextEdit.EditValueChanged += (sender, e) =>
                            {
                                refreshForm?.Invoke();
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
                            if (physicalDataFieldType == PhysicalDataFieldType.EncryptedString)
                            {
                                textEdit.Properties.MaxLength = extendedCustomDataFieldInfo.DataFieldLength / 2;
                            }
                            else
                            {
                                textEdit.Properties.MaxLength = extendedCustomDataFieldInfo.DataFieldLength;
                            }
                            textEdit.Location = new Point(controlX, y);
                            textEdit.TabIndex = index;
                            textEdit.Tag = extendedCustomDataFieldInfo;
                            textEdit.GotFocus += (sender, e) =>
                            {
                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                            };
                            textEdit.EditValueChanged += (sender, e) =>
                            {
                                refreshForm?.Invoke();
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
                            dateEdit.Location = new Point(controlX, y);
                            dateEdit.TabIndex = index;
                            dateEdit.Width = width;
                            dateEdit.Tag = extendedCustomDataFieldInfo;
                            dateEdit.GotFocus += (sender, e) =>
                            {
                                SetToolTipOnControl(extendedCustomDataFieldInfo);
                            };
                            dateEdit.EditValueChanged += (sender, e) =>
                            {
                                refreshForm?.Invoke();
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
                            timeEdit.EditValueChanged += (sender, e) =>
                            {
                                refreshForm?.Invoke();
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
                            if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
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
                                        LoadAssociatedData(searchLookUpEditWithGrid, associationInfo.AssociationId, string.Empty);
                                    }
                                };
                                searchLookUpEditWithGrid.ValueSearched += (sender, e) =>
                                {
                                    LoadAssociatedData(searchLookUpEditWithGrid, associationInfo.AssociationId, e.Content);
                                    content = e.Content;
                                };
                                searchLookUpEditWithGrid.OnPageIndexChanged += (sender, e) =>
                                {
                                    searchLookUpEditWithGrid.CurrentPageIndex = e.NewPageIndex;
                                    LoadAssociatedData(searchLookUpEditWithGrid, associationInfo.AssociationId, content);
                                };
                                searchLookUpEditWithGrid.EditValueChanged += (sender, e) =>
                                {
                                    refreshForm?.Invoke();
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
                                if (dataFieldAuthority == DataFieldAuthority.ReadOnly)
                                {
                                    cmbAssociation.Properties.ReadOnly = true;
                                }
                                else
                                {
                                    cmbAssociation.GotFocus += (sender, e) =>
                                    {
                                        SetToolTipOnControl(extendedCustomDataFieldInfo);
                                    };
                                    cmbAssociation.EditValueChanged += (sender, e) =>
                                    {
                                        refreshForm?.Invoke();
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
                                                UpdateAssociatedRecordIdCache(extendedCustomDataFieldInfo.DataFieldId, dataRow);                                                
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
                                            SetToolTipOnControl(extendedCustomDataFieldInfo);                                            
                                            lookUpEditWithGrid.OnGridBeforePopup += (sender, e) =>
                                            {
                                                if (lookUpEditWithGrid.DataSource == null)
                                                {
                                                    lookUpEditWithGrid.DataKeyNames = new string[] { key };
                                                    lookUpEditWithGrid.SortedKeyName = "RecordSorting";
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
                                                    lookUpEditWithGrid.DataSource = customAssociationContract.GetAssociationDataWithSortingDataField(customAssociationInfo.AssociationId);
                                                }
                                            };
                                            /* 修改当前窗口中的从关联字段 */
                                            lookUpEditWithGrid.OnRowDoubleClick += (sender, e) =>
                                            {
                                                PrimaryAssociationValueChanged(extendedCustomDataFieldInfo.DataFieldId, lookUpEditWithGrid.FocusedDataRow, groupControl);
                                                UpdateAssociatedRecordIdCache(extendedCustomDataFieldInfo.DataFieldId, lookUpEditWithGrid.FocusedDataRow);
                                            };
                                            lookUpEditWithGrid.EditValueCleaned += (sender, e) =>
                                            {
                                                refreshForm?.Invoke();
                                                PrimaryAssociationValueChanged(extendedCustomDataFieldInfo.DataFieldId, null, groupControl);
                                                UpdateAssociatedRecordIdCache(extendedCustomDataFieldInfo.DataFieldId, lookUpEditWithGrid.FocusedDataRow);
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
                                                    lookUpColumnInfo.FormatType = FormatType.DateTime;
                                                    lookUpColumnInfo.FormatString = "d";
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
                                                refreshForm?.Invoke();
                                            };
                                            lookUpEdit.EditValueChanged += (sender, e) =>
                                            {
                                                if (isLoading) return;
                                                if (lookUpEdit.Properties.DataSource != null)
                                                {
                                                    DataTable dataTable = lookUpEdit.Properties.DataSource as DataTable;
                                                    if (dataTable != null && lookUpEdit.ItemIndex >= 0 && lookUpEdit.ItemIndex < dataTable.Rows.Count)
                                                    {
                                                        PrimaryAssociationValueChanged(extendedCustomDataFieldInfo.DataFieldId, dataTable.Rows[lookUpEdit.ItemIndex], groupControl);
                                                        UpdateAssociatedRecordIdCache(extendedCustomDataFieldInfo.DataFieldId, dataTable.Rows[lookUpEdit.ItemIndex]);
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
                        case PhysicalDataFieldType.DropdownListEnumValue:
                        case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                        case PhysicalDataFieldType.DropdownListScdAdditionalCode:
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
                                            if (physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                                            {
                                                enumCommonNodes = customDepartmentContract.GetChildNodes(decimal.One);                                                
                                            }
                                            else
                                            {
                                                enumCommonNodes = customEnumContract.GetChildNodes(extendedCustomDataFieldInfo.EnumId);
                                            }
                                            List<CommonNode> nodes = new List<CommonNode>();
                                            nodes.Add(new CommonNode(decimal.MinValue, "[请选择]"));
                                            nodes.AddRange(enumCommonNodes.ToArray());
                                            comboBoxEdit.Properties.Items.AddRange(nodes.ToArray());
                                        }
                                        catch (Exception exception)
                                        {
                                            //记录日志, 不抛出异常, 包装异常
                                            WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
                                        }
                                    }
                                };
                                comboBoxEdit.EditValueChanged += (sender, e) =>
                                {
                                    refreshForm?.Invoke();
                                };
                                comboBoxEdit.SelectedIndexChanged += (sender, e) =>
                                {
                                    /* 修改被关联的枚举 */
                                    CommonNode commonNode = comboBoxEdit.EditValue as CommonNode;
                                    EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, commonNode, physicalDataFieldType, groupControl);
                                    UpdateEnumCommonNodeCache(extendedCustomDataFieldInfo.DataFieldId, commonNode);
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
                                cmbCscadeEnum.EditValueChanged += (sender, e) =>
                                {
                                    refreshForm?.Invoke();
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
                                        cmbCscadeEnum.EditValue = UserControlHelper.GetCleanFormattedName(commonNodes);
                                        /* 修改被关联的枚举 */
                                        MultiEnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, commonNodes, groupControl);
                                        UpdateMultiEnumCommonNodeCache(extendedCustomDataFieldInfo.DataFieldId, commonNodes);
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
                            ddlDepartmentTreeView.OnlySelectedLeaf = false;
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
                                ddlDepartmentTreeView.AfterTreeNodeSelect += (sender, e) =>
                                {
                                    refreshForm?.Invoke();
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
                                            else
                                            {
                                                ddlDepartmentTreeView.InitalizeTreeView();
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
                                    UpdateEnumCommonNodeCache(extendedCustomDataFieldInfo.DataFieldId, commonNode);
                                };
                                ddlDepartmentTreeView.OnNodeRemoved += (sender, e) =>
                                {
                                    EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, null, physicalDataFieldType, groupControl);
                                    UpdateEnumCommonNodeCache(extendedCustomDataFieldInfo.DataFieldId, null);
                                };
                            }
                            controlWidth = ddlDepartmentTreeView.Width;
                            groupControl.Controls.Add(ddlDepartmentTreeView);
                            break;

                        case PhysicalDataFieldType.TreeViewEnum:
                        case PhysicalDataFieldType.TreeViewEnumValue:
                        case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                        case PhysicalDataFieldType.TreeViewScdAdditionalCode:
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
                                    cmbEnum.EditValueChanged += (sender, e) =>
                                    {
                                        refreshForm?.Invoke();
                                    };
                                    cmbEnum.ButtonPressed += (sender, e) =>
                                    {
                                        TreeSelectedItemsForm frmTreeSelectedItems = new TreeSelectedItemsForm()
                                        {
                                            Text = extendedCustomDataFieldInfo.LogicalName,
                                            CommonNodeContract = customEnumContract,
                                            ParentNodeId = extendedCustomDataFieldInfo.EnumId,
                                            OnlySelectedLeaf = true,
                                            ShowSearch = true
                                        };
                                        frmTreeSelectedItems.NodeSelected = (node) =>
                                        {
                                            cmbEnum.EditValue = node;
                                            /* 修改被关联的枚举 */
                                            EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, node, physicalDataFieldType, groupControl);
                                            UpdateEnumCommonNodeCache(extendedCustomDataFieldInfo.DataFieldId, node);
                                        };
                                        frmTreeSelectedItems.NodeRemoved = () =>
                                        {
                                            EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, null, physicalDataFieldType, groupControl);
                                            UpdateEnumCommonNodeCache(extendedCustomDataFieldInfo.DataFieldId, null);
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
                                    treeDropdownList.AfterTreeNodeSelect += (sender, e) =>
                                    {
                                        refreshForm?.Invoke();
                                    };
                                    treeDropdownList.BeforeControlPopup += (sender, e) =>
                                    {
                                        if (treeDropdownList.TreeView.Nodes.Count == 0)
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
                                        UpdateEnumCommonNodeCache(extendedCustomDataFieldInfo.DataFieldId, commonNode);
                                    };
                                    treeDropdownList.OnNodeRemoved += (sender, e) =>
                                    {
                                        EnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, null, physicalDataFieldType, groupControl);
                                        UpdateEnumCommonNodeCache(extendedCustomDataFieldInfo.DataFieldId, null);
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
                                refreshForm?.Invoke();
                            };
                            checkedComboBoxEdit.EditValueChanged += (sender, e) =>
                            {
                                if (isLoading) return;
                                IList<object> objects = checkedComboBoxEdit.Properties.Items.GetCheckedValues();
                                IList<CommonNode> commonNodes = new List<CommonNode>();
                                foreach (object obj in objects)
                                {
                                    commonNodes.Add(obj as CommonNode);
                                }
                                MultiEnumEditValeChanged(extendedCustomDataFieldInfo.DataFieldId, commonNodes, groupControl);
                                UpdateMultiEnumCommonNodeCache(extendedCustomDataFieldInfo.DataFieldId, commonNodes);
                            };
                            controlWidth = checkedComboBoxEdit.Width;
                            groupControl.Controls.Add(checkedComboBoxEdit);
                            break;

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
                            devExpressUploadFile.OnTextChanged += (sender, e) =>
                            {
                                refreshForm?.Invoke();
                            };
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
                            systemTextEdit.Text = (currentCommonUserInfo != null) ? currentCommonUserInfo.UserName : CurrentUser.Instance.UserName;
                            break;

                        case SystemDataField.UserActualName:
                            systemTextEdit.Text = (currentCommonUserInfo != null) ? currentCommonUserInfo.UserActualName : CurrentUser.Instance.UserActualName;
                            break;

                        case SystemDataField.UserTypeName:
                            systemTextEdit.Text = (currentCommonUserInfo != null) ? currentCommonUserInfo.UserTypeName : CurrentUser.Instance.UserTypeName;
                            break;

                        case SystemDataField.UserTypeCode:
                            systemTextEdit.Text = (currentCommonUserInfo != null) ? currentCommonUserInfo.UserTypeCode : CurrentUser.Instance.UserTypeCode;
                            break;

                        case SystemDataField.DepName:
                            systemTextEdit.Text = (currentCommonUserInfo != null) ? currentCommonUserInfo.DepName : CurrentUser.Instance.DepName;
                            break;

                        case SystemDataField.DepCode:
                            systemTextEdit.Text = (currentCommonUserInfo != null) ? currentCommonUserInfo.DepCode : CurrentUser.Instance.DepCode;
                            break;

                        case SystemDataField.DepValue:
                            systemTextEdit.Text = (currentCommonUserInfo != null) ? currentCommonUserInfo.DepValue : CurrentUser.Instance.DepValue;
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
        /// 修改多选枚举缓存数据
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="commonNodes"></param>
        private void UpdateMultiEnumCommonNodeCache(decimal dataFieldId, IList<CommonNode> commonNodes)
        {
            if (multiEnumCommonNodeCache.ContainsKey(dataFieldId))
            {
                multiEnumCommonNodeCache[dataFieldId] = commonNodes;
            }
            else
            {
                multiEnumCommonNodeCache.Add(dataFieldId, commonNodes);
            }
        }

        /// <summary>
        /// 修改单选枚举缓存数据
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="commonNode"></param>
        private void UpdateEnumCommonNodeCache(decimal dataFieldId, CommonNode commonNode)
        {
            if (enumCommonNodeCache.ContainsKey(dataFieldId))
            {
                enumCommonNodeCache[dataFieldId] = commonNode;
            }
            else
            {
                enumCommonNodeCache.Add(dataFieldId, commonNode);
            }
        }

        /// <summary>
        /// 修改关联缓存数据
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <param name="dataRow"></param>
        private void UpdateAssociatedRecordIdCache(decimal dataFieldId, DataRow dataRow)
        {            
            decimal recordId = decimal.MinValue;
            if (dataRow != null)
            {
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                recordId = DataConvertionHelper.GetDecimal(dataRow[recordIdName]);
            }
            if (associatedRecordIdCache.ContainsKey(dataFieldId))
            {
                associatedRecordIdCache[dataFieldId] = recordId;
            }
            else
            {
                associatedRecordIdCache.Add(dataFieldId, recordId);
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
            if (!formReadOnly)
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
        /// <param name="associationId"></param>
        /// <param name="content"></param>
        private void LoadAssociatedData(SearchLookUpEditWithGrid searchLookUpEditWithGrid, decimal associationId, string content)
        {
            IList<WhereConditon> whereConditons = new List<WhereConditon>();            
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
                        foreach (CommonNode commonNode in commonNodes)
                        {
                            if (dataFieldType == PhysicalDataFieldType.EnumNameDependency)
                            {
                                sb.AppendFormat("{0},", commonNode.NodeName);
                            }
                            else
                            {
                                CustomEnumInfo customEnumInfo = relationDataBusiness.GetCustomEnumInfoData(commonNode.NodeId);
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
                            }                   
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
                            case PhysicalDataFieldType.DropdownListEnum:
                            case PhysicalDataFieldType.DropdownListEnumValue:
                            case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                            case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                            case PhysicalDataFieldType.TreeViewEnum:
                            case PhysicalDataFieldType.TreeViewEnumValue:
                            case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                            case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                                if (dataFieldType == PhysicalDataFieldType.EnumNameDependency)
                                {
                                    value = commonNode.NodeName;
                                }
                                else
                                {
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
        /// 主关联字段的值发生变化
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
                Dictionary<string, string> dataFieldNameRelation = associatedDataFieldContract.GetDataFieldNameRelation(dataFieldId);
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
        /// 设置表的类型
        /// </summary>
        /// <param name="categroy"></param>
        public void SetTableCategroy(TableCategroy categroy)
        {
            tableCategroy = categroy;
        }

        /// <summary>
        /// 加载表格类型数据  
        /// </summary>
        /// <param name="dataTableId"></param>
        /// <param name="dataRow"></param>
        /// <param name="onlyLoadData">仅加载数据不保留记录编号</param>
        public void LoadDataOnTable(decimal dataTableId, DataRow dataRow, bool onlyLoadData)
        {
            if (dataRow == null)
            {
                return;
            }

            tableCategroy = TableCategroy.Single;
            if (!onlyLoadData)
            {
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                string currentStateName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState);
                decimal recordId = DataConvertionHelper.GetDecimal(dataRow[recordIdName]);
                CurrentState currentState = CurrentState.Current;
                if (dataRow.Table.Columns.Contains(currentStateName))
                {
                    currentState = (CurrentState)DataConvertionHelper.GetByte(dataRow[currentStateName]);
                }
                _recordRelation.Clear();
                _recordRelation.Add(new RecordItem(dataTableId, recordId, currentState));
            }
            isLoading = true;
            groupControl.Tag = dataRow;
            foreach (Control control in groupControl.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                }
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
                LoadDataOnControl(control, extendedCustomDataFieldInfo, dataRow);
            }
            isLoading = false;
        }
        
        /// <summary>
        /// 在组合表上加载
        /// </summary>
        /// <param name="dataRows"></param>
        /// <param name="onlyLoadData"></param>
        public void LoadDataOnCombinedTables(Dictionary<decimal, DataTable> dataRowValues, bool onlyLoadData)
        {
            tableCategroy = TableCategroy.Multiple;
            if (!onlyLoadData)
            {
                string recordIdName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.RecordId);
                string currentStateName = DataFieldHelper.GetSystemPhysicalDataFieldName(SystemPhysicalDataField.CurrentState);
                _recordRelation.Clear();
                foreach (var keyValue in dataRowValues)
                {
                    if (keyValue.Value.Rows.Count > 0)
                    {
                        decimal recordId = DataConvertionHelper.GetDecimal(keyValue.Value.Rows[0][recordIdName]);
                        CurrentState currentState = CurrentState.Current;
                        if (keyValue.Value.Columns.Contains(currentStateName))
                        {
                            currentState = (CurrentState)DataConvertionHelper.GetByte(keyValue.Value.Rows[0][currentStateName]);
                        }
                        _recordRelation.Add(new RecordItem(keyValue.Key, recordId, currentState));
                    }
                }
            }
            isLoading = true;
            groupControl.Tag = dataRowValues;
            foreach (Control control in groupControl.Controls)
            {
                if (control.Tag == null)
                {
                    continue;
                } 
                ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo = control.Tag as ExtendedCustomDataFieldInfo;
                if (dataRowValues.ContainsKey(extendedCustomDataFieldInfo.TableId) && dataRowValues[extendedCustomDataFieldInfo.TableId].Rows.Count > 0)
                {
                    LoadDataOnControl(control, extendedCustomDataFieldInfo, dataRowValues[extendedCustomDataFieldInfo.TableId].Rows[0]);
                }
            }
            isLoading = false;
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
                case DataFieldProperty.SystemPhysicalDataField:
                    SystemDataField systemDataField = (SystemDataField)Convert.ToByte(extendedCustomDataFieldInfo.DataFieldId);
                    switch (systemDataField)
                    {
                        case SystemDataField.AuditedStatus:
                            ((TextEdit)control).Text = UserEnumHelper.GetEnumText((AuditedStatus)DataConvertionHelper.GetByte(dataRow[physicalName]));
                            break;

                        case SystemDataField.CurrentState:
                            ((TextEdit)control).Text = UserEnumHelper.GetEnumText((CurrentState)DataConvertionHelper.GetByte(dataRow[physicalName]));
                            break;
                    }
                    break;

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
                        case PhysicalDataFieldType.DropdownListEnumValue:
                        case PhysicalDataFieldType.DepartmentDropdownListEnum:
                        case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                        case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                            string dropdownListEnumValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            ComboBoxEdit comboBoxEdit = (ComboBoxEdit)control;
                            if (!string.IsNullOrWhiteSpace(dropdownListEnumValue))
                            {
                                if (physicalDataFieldType == PhysicalDataFieldType.DropdownListEnum || physicalDataFieldType == PhysicalDataFieldType.DepartmentDropdownListEnum)
                                {
                                    comboBoxEdit.EditValue = dropdownListEnumValue;
                                }
                                else
                                {
                                    comboBoxEdit.EditValue = customEnumContract.GetDropdownListEnumName(extendedCustomDataFieldInfo.EnumId, dataRow[physicalName], physicalDataFieldType);
                                }
                            }
                            else
                            {
                                comboBoxEdit.EditValue = "[请选择]";
                            }
                            break;
                        
                        case PhysicalDataFieldType.CscadeEnum:
                            ((ComboBoxEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            break;
                            
                        case PhysicalDataFieldType.DepartmentTreeViewEnum:
                        case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                            string departmentValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            if (!string.IsNullOrWhiteSpace(departmentValue))
                            {
                                ((TreeDropdownList)control).Value = departmentValue;
                            }
                            break;

                        case PhysicalDataFieldType.TreeViewEnum:
                        case PhysicalDataFieldType.TreeViewEnumValue:
                        case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                        case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                            if (superEnumIds.Contains(extendedCustomDataFieldInfo.EnumId))
                            {
                                ComboBoxEdit cmbTree = (ComboBoxEdit)control;
                                if (physicalDataFieldType == PhysicalDataFieldType.TreeViewEnum)
                                {
                                    cmbTree.EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                                }
                                else
                                {
                                    string result = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                                    if (!string.IsNullOrWhiteSpace(result))
                                    {
                                        cmbTree.EditValue = customEnumContract.GetTreeEnumName(extendedCustomDataFieldInfo.EnumId, dataRow[physicalName], physicalDataFieldType);
                                    }
                                }
                            }
                            else
                            {
                                TreeDropdownList tdTree = (TreeDropdownList)control;
                                string enumValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                                if (!string.IsNullOrWhiteSpace(enumValue))
                                {
                                    if (physicalDataFieldType == PhysicalDataFieldType.TreeViewEnum)
                                    {
                                        tdTree.Value = enumValue;
                                    }
                                    else
                                    {
                                        tdTree.Value = customEnumContract.GetTreeEnumName(extendedCustomDataFieldInfo.EnumId, dataRow[physicalName], physicalDataFieldType);
                                    }
                                }
                            }
                            break;
                                                        
                        case PhysicalDataFieldType.MultiSelectedEnum:
                            ((CheckedComboBoxEdit)control).EditValue = DataConvertionHelper.GetConvertedString(dataRow[physicalName]);
                            break;

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

        #endregion

        #region 获得提交数据

        /// <summary>
        /// 获得提交数据
        /// </summary>
        /// <returns></returns>
        public RecordSet GetRecordSet()
        {
            RecordSet recordSet = new RecordSet();
            bool success = true;
            string result = string.Empty;
            
            RecordEntity recordEntity = null;
            Dictionary<decimal, RecordEntity> dicRecordEntities = null;
            if (tableCategroy == TableCategroy.Multiple)
            {
                dicRecordEntities = new Dictionary<decimal, RecordEntity>();
            }
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
                if (DataFieldHelper.IsReadOnly(physicalDataFieldType))
                {
                    continue;
                }
                IList<CommonDataField> commonDataFields = new List<CommonDataField>();
                IList<CommonDataField> relationDataFields = new List<CommonDataField>();
                object dataFieldValue = null;
                DbType dataFieldDataType = DbType.String;
                switch (physicalDataFieldType)
                {
                    case PhysicalDataFieldType.Boolean:
                        dataFieldValue = ((CheckEdit)control).Checked;
                        dataFieldDataType = DbType.Boolean;
                        break;

                    case PhysicalDataFieldType.Int32:
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
                                success = false;
                                break;
                            }
                            //超过范围转换失败                                
                            if (DataConvertionHelper.IsNullValue(DataConvertionHelper.GetConvertedInt(textInt32)))
                            {
                                control.Focus();
                                result = string.Format("{0}的整数值的超出了整数限制范围(-2147483648~2147483647)。", extendedCustomDataFieldInfo.LogicalName);
                                success = false;
                                break;
                            }
                            dataFieldValue = DataConvertionHelper.GetConvertedInt(textInt32);
                        }
                        break;

                    case PhysicalDataFieldType.Decimal:
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
                                success = false;
                                break;
                            }
                            int pos = textDecimal.IndexOf('.');
                            if ((pos > 0 && (textDecimal.Length - pos - 1) > extendedCustomDataFieldInfo.DataFieldLength) || textDecimal.Length > 12)
                            {
                                control.Focus();
                                result = string.Format("{0}的实数长度限制的范围(0~12位)，小数位长度不能超过{1}。", extendedCustomDataFieldInfo.LogicalName, extendedCustomDataFieldInfo.DataFieldLength);
                                success = false;
                                break;
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
                        dataFieldDataType = DbType.String;
                        string textString = ((TextEdit)control).Text.Trim();
                        if (!string.IsNullOrEmpty(textString))
                        {
                            switch (physicalDataFieldType)
                            {
                                case PhysicalDataFieldType.NumeralString:
                                    if (!string.IsNullOrEmpty(textString))
                                    {
                                        //整数 
                                        Regex numeralRegex = new Regex(@"^-?\d+$");
                                        if (!numeralRegex.IsMatch(textString))
                                        {
                                            control.Focus();
                                            result = string.Format("{0}只能为数字组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                            success = false;
                                            break;
                                        }
                                    }
                                    break;

                                case PhysicalDataFieldType.CharString:
                                    if (!string.IsNullOrEmpty(textString))
                                    {
                                        //由26个英文字母组成的字符串 
                                        Regex charRegex = new Regex(@"^[A-Za-z]+$");
                                        if (!charRegex.IsMatch(textString))
                                        {
                                            control.Focus();
                                            result = string.Format("{0}只能为由26个英文字母组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                            success = false;
                                            break;
                                        }
                                    }
                                    break;

                                case PhysicalDataFieldType.MixedString:
                                    if (!string.IsNullOrEmpty(textString))
                                    {
                                        //由数字和26个英文字母组成的字符串  
                                        Regex mixedRegex = new Regex(@"^[A-Za-z0-9]+$");
                                        if (!mixedRegex.IsMatch(textString))
                                        {
                                            control.Focus();
                                            result = string.Format("{0}只能为由数字和26个英文字母组成的字符串。", extendedCustomDataFieldInfo.LogicalName);
                                            success = false;
                                            break;
                                        }
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
                                            success = false;
                                            break;
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
                                        success = false;
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
                    case PhysicalDataFieldType.CscadeEnum:
                        if (physicalDataFieldType == PhysicalDataFieldType.MultiSelectedEnum)
                        {
                            dataFieldDataType = DbType.String;
                            CheckedComboBoxEdit checkedComboBoxEdit = (CheckedComboBoxEdit)control;
                            dataFieldValue = checkedComboBoxEdit.EditValue;
                        }
                        else
                        {
                            dataFieldDataType = DbType.String;
                            dataFieldValue = ((ComboBoxEdit)control).Text.Trim();
                        }
                        if (multiEnumCommonNodeCache.ContainsKey(extendedCustomDataFieldInfo.DataFieldId))
                        {
                            IList<CommonNode> multiSelectedNodes = customDataFieldContract.GetCommonNodesByParentDataFieldId(extendedCustomDataFieldInfo.DataFieldId);
                            foreach (CommonNode node in multiSelectedNodes)
                            {
                                CommonDataField enumCommonDataField = relationDataBusiness.GetMultiEnumDependencyValue(multiEnumCommonNodeCache[extendedCustomDataFieldInfo.DataFieldId], node.NodeId, physicalDataFieldType, relationDataFields);
                                commonDataFields.Add(enumCommonDataField);
                            }
                        }
                        break;
                        
                    case PhysicalDataFieldType.DropdownListEnum:
                    case PhysicalDataFieldType.DropdownListEnumValue:
                    case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                    case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                    case PhysicalDataFieldType.DepartmentDropdownListEnum:
                    case PhysicalDataFieldType.DepartmentTreeViewEnum:
                    case PhysicalDataFieldType.DepartmentTreeViewEnumWithRoot:
                    case PhysicalDataFieldType.TreeViewEnum:
                    case PhysicalDataFieldType.TreeViewEnumValue:
                    case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                    case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                        switch (physicalDataFieldType)
                        {
                            case PhysicalDataFieldType.DropdownListEnum:
                            case PhysicalDataFieldType.DepartmentDropdownListEnum:
                                dataFieldDataType = DbType.String;
                                ComboBoxEdit comboBoxEdit = (ComboBoxEdit)control;
                                if (comboBoxEdit.SelectedIndex == 0)
                                {
                                    dataFieldValue = null;
                                }
                                else if (comboBoxEdit.SelectedIndex > 0)
                                {
                                    dataFieldValue = comboBoxEdit.Text.Trim();
                                }
                                else
                                {
                                    if (!string.IsNullOrWhiteSpace(comboBoxEdit.Text))
                                    {
                                        dataFieldValue = GetEnumData(extendedCustomDataFieldInfo);
                                    }
                                }
                                break;

                            case PhysicalDataFieldType.DropdownListEnumValue:                            
                            case PhysicalDataFieldType.DropdownListFstAdditionalCode:
                            case PhysicalDataFieldType.DropdownListScdAdditionalCode:
                                dataFieldDataType = DbType.String;
                                ComboBoxEdit cmbropdownList = (ComboBoxEdit)control;
                                if (cmbropdownList.SelectedIndex == 0)
                                {
                                    dataFieldValue = null;
                                }
                                else if (cmbropdownList.SelectedIndex > 0)
                                {
                                    CommonNode node = cmbropdownList.EditValue as CommonNode;
                                    dataFieldValue = customEnumContract.GetEnumData(node.NodeId, physicalDataFieldType);
                                }
                                else
                                {
                                    if (_recordRelation.FindIndex(recordItem => recordItem.TableId == extendedCustomDataFieldInfo.TableId) >= 0)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrWhiteSpace(cmbropdownList.Text))
                                        {
                                            dataFieldValue = GetEnumData(extendedCustomDataFieldInfo);
                                        }                                        
                                    }
                                }                                
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

                            case PhysicalDataFieldType.TreeViewEnumValue:
                            case PhysicalDataFieldType.TreeViewFstAdditionalCode:
                            case PhysicalDataFieldType.TreeViewScdAdditionalCode:
                                dataFieldDataType = DbType.String;
                                CommonNode commonNode = null;
                                if (superEnumIds.Contains(extendedCustomDataFieldInfo.EnumId))
                                {
                                    ComboBoxEdit cmbTreeEnum = (ComboBoxEdit)control;
                                    commonNode = cmbTreeEnum.EditValue as CommonNode;
                                    if (commonNode == null && cmbTreeEnum.EditValue != null)
                                    {
                                        dataFieldValue = GetEnumData(extendedCustomDataFieldInfo);
                                    }
                                }
                                else
                                {
                                    TreeDropdownList treeDropdownList = (TreeDropdownList)control;
                                    if (treeDropdownList.SelectedNode != null)
                                    {
                                        commonNode = treeDropdownList.SelectedNode;
                                    }
                                    else
                                    {
                                        if (treeDropdownList.Value != null && !string.IsNullOrWhiteSpace(treeDropdownList.Value.ToString()))
                                        {
                                            dataFieldValue = GetEnumData(extendedCustomDataFieldInfo);
                                        }                                        
                                    }
                                }
                                if (commonNode != null)
                                {
                                    dataFieldValue = customEnumContract.GetEnumData(commonNode.NodeId, physicalDataFieldType);
                                }
                                break;
                        }
                        if (enumCommonNodeCache.ContainsKey(extendedCustomDataFieldInfo.DataFieldId))
                        {
                            IList<CommonNode> commonNodes = customDataFieldContract.GetCommonNodesByParentDataFieldId(extendedCustomDataFieldInfo.DataFieldId);
                            foreach (CommonNode node in commonNodes)
                            {
                                CommonDataField enumCommonDataField = relationDataBusiness.GetEnumDependencyValue(enumCommonNodeCache[extendedCustomDataFieldInfo.DataFieldId], node.NodeId, physicalDataFieldType, relationDataFields);
                                commonDataFields.Add(enumCommonDataField);
                            }
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
                            if (associatedRecordIdCache.ContainsKey(extendedCustomDataFieldInfo.DataFieldId))
                            {
                                decimal associationId = associatedDataFieldContract.GetAssociationId(extendedCustomDataFieldInfo.AssociatedDataFieldId);
                                IList<CommonNode> associatedNodes = customDataFieldContract.GetCommonNodesByParentDataFieldId(extendedCustomDataFieldInfo.DataFieldId);
                                foreach (CommonNode node in associatedNodes)
                                {
                                    if (node.ParentNodeId == extendedCustomDataFieldInfo.TableId)
                                    {
                                        CommonDataField associatedCommonDataField = relationDataBusiness.GetAssociatedValue(associatedRecordIdCache[extendedCustomDataFieldInfo.DataFieldId], node.NodeId, associationId, relationDataFields);
                                        commonDataFields.Add(associatedCommonDataField);
                                    }
                                    else
                                    {
                                        CommonDataField associatedCommonDataField = relationDataBusiness.GetAssociatedValue(associatedRecordIdCache[extendedCustomDataFieldInfo.DataFieldId], node.NodeId, associationId, relationDataFields);
                                        relationDataFields.Add(associatedCommonDataField);
                                    }
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
                                                success = false;
                                            }
                                        }
                                        else
                                        {
                                            if (!FileFormatHelper.VerfiyPDFFormat(devExpressUploadFile.FileName))
                                            {
                                                control.Focus();
                                                result = "文件格式只能为：pdf(*.pdf) 类型。";
                                                success = false;
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
                                            success = false;
                                        }
                                        break;

                                    case PhysicalDataFieldType.PicAttachment:
                                        if (!FileFormatHelper.VerfiyImageFormat(devExpressUploadFile.FileName))
                                        {
                                            control.Focus();
                                            result = "图片格式只能为：JPEG(*.JPG;*.JPEG)，GIF(*.GIF), PNG(*.PNG) 或者 BMP(*.BMP)。";
                                            success = false;
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
                                            success = false;
                                        }
                                        break;
                                }
                                if (data.Length == 0)
                                {
                                    control.Focus();
                                    result = "文件大小不能为0。";
                                    success = false;
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
                switch (tableCategroy)
                {
                    case TableCategroy.Single:
                        if (recordEntity == null)
                        {
                            decimal recordId = decimal.MinValue;
                            int pos = _recordRelation.FindIndex(recordItem => recordItem.TableId == extendedCustomDataFieldInfo.TableId);
                            if (pos >= 0)
                            {
                                recordId = _recordRelation[pos].RecordId;
                            }
                            DataTableType dataTableType = (DataTableType)customTableContract.GetTableType(extendedCustomDataFieldInfo.TableId);
                            if (businessEnabled)
                            {
                                
                                recordEntity = new RecordEntity(extendedCustomDataFieldInfo.TableId, dataTableType, recordId, instanceId, CurrentState.Current);
                            }
                            else
                            {
                                recordEntity = new RecordEntity(extendedCustomDataFieldInfo.TableId, dataTableType, recordId, CurrentState.Current);
                            }
                        }
                        break;

                    case TableCategroy.Multiple:
                        if (dicRecordEntities.ContainsKey(extendedCustomDataFieldInfo.TableId))
                        {
                            recordEntity = dicRecordEntities[extendedCustomDataFieldInfo.TableId];
                        }
                        else
                        {
                            decimal dataRecordId = decimal.MinValue;
                            int pos = _recordRelation.FindIndex(recordItem => recordItem.TableId == extendedCustomDataFieldInfo.TableId);
                            if (pos >= 0)
                            {
                                dataRecordId = _recordRelation[pos].RecordId;
                            }
                            DataTableType dataTableType = (DataTableType)customTableContract.GetTableType(extendedCustomDataFieldInfo.TableId);
                            if (businessEnabled)
                            {
                                recordEntity = new RecordEntity(extendedCustomDataFieldInfo.TableId, dataTableType, dataRecordId, instanceId, CurrentState.Current);
                            }
                            else
                            {
                                recordEntity = new RecordEntity(extendedCustomDataFieldInfo.TableId, dataTableType, dataRecordId, CurrentState.Current);
                            }
                            dicRecordEntities.Add(extendedCustomDataFieldInfo.TableId, recordEntity);
                        }
                        break;
                }
                CommonDataField commonDataField = new CommonDataField(extendedCustomDataFieldInfo.DataFieldId, extendedCustomDataFieldInfo.PhysicalName, dataFieldValue, dataFieldDataType);
                recordEntity.CommonDataFields.Add(commonDataField);
                if (commonDataFields.Count > 0)
                {
                    recordEntity.CommonDataFields.AddRange(commonDataFields);
                }
                if (dataFieldRealitonIncluded)
                {
                    bool relationship = AuthorityHelper.CheckAuthority(extendedCustomDataFieldInfo.DataFieldSetting, (byte)DataFieldSetting.Correlation);
                    if (relationship)
                    {
                        /*  所有表增加记录，或者主表、从表、主从表（仅当前记录修改时），更新联动字段 */
                        bool update = true;
                    
                        if (recordEntity.RecordId > 0 && recordEntity.TableType == DataTableType.MasterSlaveTable)
                        {
                            int pos = _recordRelation.FindIndex(recordItem => recordItem.TableId == extendedCustomDataFieldInfo.TableId);
                            if (pos >= 0 && _recordRelation[pos].CurrentState != CurrentState.Current)
                            {
                                update = false;
                            }
                        }
                        if(update)
                        {
                            IList<CommonNode> relationCommonNodes = customDataFieldContract.GetRelationDataFields(extendedCustomDataFieldInfo.DataFieldId);
                            foreach (CommonNode relationCommonNode in relationCommonNodes)
                            {
                                relationDataFields.Add(new CommonDataField(relationCommonNode.NodeId, relationCommonNode.NodeCode, commonDataField.DataFieldValue, commonDataField.DataFieldDataType));
                            }
                            if (relationDataFields.Count > 0)
                            {
                                recordEntity.RelaitonDataFields.AddRange(relationDataFields);
                            }
                        }
                    }                  
                }
                if (extendedCustomDataFieldInfo.RequiredDataField && dataFieldValue == null)
                {
                    success = false;
                    result = string.Format("{0}不能为空。", extendedCustomDataFieldInfo.LogicalName);
                    break;
                }
            }
            switch (tableCategroy)
            {
                case TableCategroy.Single:
                    recordSet.RecordEntities.Add(recordEntity);
                    break;

                case TableCategroy.Multiple:
                    foreach (KeyValuePair<decimal, RecordEntity> keyValue in dicRecordEntities)
                    {
                        recordSet.RecordEntities.Add(keyValue.Value);
                    }
                    break;
            }
                                
            recordSet.Success = success;
            recordSet.Warning = result;

            return recordSet;
        }

        /// <summary>
        /// 在仅加载文本值情况下，获得枚举对应类型的数据
        /// </summary>
        /// <param name="extendedCustomDataFieldInfo"></param>
        /// <returns></returns>
        private object GetEnumData(ExtendedCustomDataFieldInfo extendedCustomDataFieldInfo)
        {
            object dataFieldValue = null;
            switch (tableCategroy)
            {
                case TableCategroy.Single:
                    DataRow dataRow = groupControl.Tag as DataRow;
                    dataFieldValue = dataRow[extendedCustomDataFieldInfo.PhysicalName];
                    break;

                case TableCategroy.Multiple:
                    Dictionary<decimal, DataTable> dataRowValues = groupControl.Tag as Dictionary<decimal, DataTable>;
                    dataFieldValue = dataRowValues[extendedCustomDataFieldInfo.TableId].Rows[0][extendedCustomDataFieldInfo.PhysicalName];
                    break;
            }

            return dataFieldValue;
        }

        /// <summary>
        /// 获得当前表格样式
        /// </summary>
        /// <param name="formShowStyle"></param>
        /// <returns></returns>
        public FormShowStyleSetting GetFormShowStyleSettings(FormShowStyle formShowStyle)
        {
            bool formCompleted = false;
            int countForEachRow = 1;
            int labelWidth = 0;
            int characterCountOnLabel = 0;
            bool separatorControlCreated = true;
            switch (formShowStyle)
            {
                case FormShowStyle.SingleColumnThreeRanksCompleted:
                    countForEachRow = 3;
                    formCompleted = true;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT;
                    break;

                case FormShowStyle.SingleColumnTwoRanksCompleted:
                    countForEachRow = 2;
                    formCompleted = true;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT_MIDDLE;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT_MIDDLE;
                    break;

                case FormShowStyle.SingleColumnOneRankCompleted:
                    countForEachRow = 1;
                    formCompleted = true;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT_BIG;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT_BIG;
                    break;

                case FormShowStyle.TwoColumnsOneRankCompleted:
                    countForEachRow = 1;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT;
                    formCompleted = true;
                    separatorControlCreated = false;
                    break;

                case FormShowStyle.SingleColumnThreeRanksClean:
                    countForEachRow = 3;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT;
                    break;

                case FormShowStyle.SingleColumnTwoRanksClean:
                    countForEachRow = 2;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT_MIDDLE;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT_MIDDLE;
                    break;

                case FormShowStyle.SingleColumnOneRankClean:
                    countForEachRow = 1;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT_BIG;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT_BIG;
                    break;

                case FormShowStyle.TwoColumnsOneRankClean:
                    countForEachRow = 1;
                    characterCountOnLabel = COMMON_MAX_LEN_OF_LABEL_TEXT;
                    labelWidth = COMMON_WIDTH_OF_LABEL_TEXT;
                    separatorControlCreated = false;
                    break;

                case FormShowStyle.Expand:
                    break;

                case FormShowStyle.Combination:
                    break;
            }

            FormShowStyleSetting formShowStyleSetting = new FormShowStyleSetting(formCompleted, countForEachRow, labelWidth, characterCountOnLabel, separatorControlCreated);

            return formShowStyleSetting;
        }

        #endregion
    }
}
