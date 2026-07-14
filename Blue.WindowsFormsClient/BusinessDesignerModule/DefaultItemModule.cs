using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class DefaultItemModule : UserControl
    {
        #region 私有变量

        /// <summary>
        /// 值改变后字段设置的内容跟着发生变化
        /// </summary>
        private bool enableEditValue = true;

        #endregion

        #region 内部成员变量

        private PhysicalDataFieldType _physicalDataFieldType;

        #endregion

        #region 属性

        /// <summary>
        /// 枚举类型契约
        /// </summary>
        public ICustomEnumContract CustomEnumContract
        {
            set; get;
        }

        /// <summary>
        /// 字段物理类型
        /// </summary>
        public PhysicalDataFieldType PhysicalDataFieldType
        {
            get
            {
                return _physicalDataFieldType;
            }
            set
            {
                _physicalDataFieldType = value;
            }
        }

        /// <summary>
        /// 父节点枚举编号
        /// </summary>
        public decimal ParentEnumId
        {
            get;
            set;
        }

        /// <summary>
        /// 在字段条件框中设置数据
        /// </summary>
        public SetDefaultItemDelegate SetDefaultItem
        {
            get;
            set;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public DefaultItemModule()
        {
            InitializeComponent();
            _physicalDataFieldType = PhysicalDataFieldType.Boolean;
            IList<CommonNode> commonNodes = UserEnumHelper.GetCommonNodes(typeof(CustomBool));
            cmbDefaultBoolValue.Properties.Items.AddRange(commonNodes.ToArray());
            cmbDefaultBoolValue.SelectedIndex = 0;
            ParentEnumId = decimal.MinValue;
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefaultItemModule_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// 默认文本值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDefaultStringValue_EditValueChanged(object sender, EventArgs e)
        {
            if (SetDefaultItem != null)
            {
                string content = txtDefaultStringValue.Text.Trim();
                DefaultItem<string> tag = null;
                if (!string.IsNullOrWhiteSpace(content))
                {
                    tag = new DefaultItem<string>(content);
                }
                SetDefaultItem(tag, _physicalDataFieldType);
            }
        }

        /// <summary>
        /// 默认 Bool 类型的值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDefaultBoolValue_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SetDefaultItem != null)
            {
                CommonNode commonNode = cmbDefaultBoolValue.SelectedItem as CommonNode;
                CustomBool customBool = (CustomBool)commonNode.NodeId;
                object tag = null;
                if(customBool == CustomBool.True)
                {
                    tag = new DefaultItem<bool>(true);
                }                
                SetDefaultItem(tag, _physicalDataFieldType);
            }          
        }

        /// <summary>
        /// 下拉类型枚举
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDropdownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SetDefaultItem != null)
            {
                CommonNode commonNode = cmdDropdownList.SelectedItem as CommonNode;
                SetEnumDataOnControls(commonNode);                
            }
        }

        /// <summary>
        /// 树形枚举
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbTreeEnum_AfterTreeNodeSelect(object sender, TreeViewEventArgs e)
        {
            if (SetDefaultItem != null)
            {                
                CommonNode commonNode = cmbTreeEnum.Value as CommonNode;
                SetEnumDataOnControls(commonNode);
            }
        }

        /// <summary>
        /// 日期默认值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateDefaultValue_EditValueChanged(object sender, EventArgs e)
        {
            SetDateOnControls();
        }

        /// <summary>
        /// 日期最大值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateMaxValue_EditValueChanged(object sender, EventArgs e)
        {
            SetDateOnControls();
        }

        /// <summary>
        /// 日期最小值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dateMinValue_EditValueChanged(object sender, EventArgs e)
        {
            SetDateOnControls();
        }

        /// <summary>
        /// 包含最大日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMaxDateIncluded_CheckedChanged(object sender, EventArgs e)
        {
            SetDateOnControls();
        }

        /// <summary>
        /// 包含最小日期
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMinDateIncluded_CheckedChanged(object sender, EventArgs e)
        {
            SetDateOnControls();
        }

        /// <summary>
        /// 设置默认时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeDefaultValue_EditValueChanged(object sender, EventArgs e)
        {
            SetTimeOnControls();
        }

        /// <summary>
        /// 设置最大时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeMaxValue_EditValueChanged(object sender, EventArgs e)
        {
            SetTimeOnControls();
        }

        /// <summary>
        /// 设置最小时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timeMinValue_EditValueChanged(object sender, EventArgs e)
        {
            SetTimeOnControls();
        }

        /// <summary>
        /// 包含最大时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMaxTimeIncluded_CheckedChanged(object sender, EventArgs e)
        {
            SetTimeOnControls();
        }

        /// <summary>
        /// 包含最小时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMinTimeIncluded_CheckedChanged(object sender, EventArgs e)
        {
            SetTimeOnControls();
        }

        /// <summary>
        /// 设置默认数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void seDigitDefault_EditValueChanged(object sender, EventArgs e)
        {
            SetDigitOnControls();
        }

        /// <summary>
        /// 设置最大数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void seDigitMax_EditValueChanged(object sender, EventArgs e)
        {
            SetDigitOnControls();
        }

        /// <summary>
        /// 设置最小数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void seDigitMin_EditValueChanged(object sender, EventArgs e)
        {
            SetDigitOnControls();
        }

        /// <summary>
        /// 包含最大数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMaxDigitIncluded_CheckedChanged(object sender, EventArgs e)
        {
            SetDigitOnControls();
        }

        /// <summary>
        /// 包含最小数字
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkMinDigitIncluded_CheckedChanged(object sender, EventArgs e)
        {
            SetDigitOnControls();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 设置数字的默认值与值的范围
        /// </summary>
        private void SetDigitOnControls()
        {
            if (!enableEditValue)
            {
                return;
            }
            switch (_physicalDataFieldType)
            {
                case PhysicalDataFieldType.Int32:
                    int intDefault = Int32.MinValue;
                    int intMax = Int32.MinValue;
                    int intMin = Int32.MinValue;
                    if (seDigitDefault.EditValue != null)
                    {
                        intDefault = Convert.ToInt32(seDigitDefault.EditValue);
                    }
                    if (seDigitMin.EditValue != null)
                    {
                        intMin = Convert.ToInt32(seDigitMin.EditValue);
                    }

                    if (seDigitMax.EditValue != null)
                    {
                        intMax = Convert.ToInt32(seDigitMax.EditValue);
                    }

                    DefaultItem<int> intItem = new DefaultItem<int>(intDefault, intMax, chkMaxDateIncluded.Checked, intMin, chkMinDateIncluded.Checked);
                    SetDefaultItem(intItem, _physicalDataFieldType);
                    break;

                case PhysicalDataFieldType.Decimal:
                    decimal decimalDefault = Decimal.MinValue;
                    decimal decimalMax = Decimal.MinValue;
                    decimal decimalMin = Decimal.MinValue;
                    if (seDigitDefault.EditValue != null)
                    {
                        decimalDefault = Convert.ToDecimal(seDigitDefault.EditValue);
                    }
                    if (seDigitMin.EditValue != null)
                    {
                        decimalMin = Convert.ToDecimal(seDigitMin.EditValue);
                    }
                    if (seDigitMax.EditValue != null)
                    {
                        decimalMax = Convert.ToDecimal(seDigitMax.EditValue);
                    }
                    DefaultItem<decimal> decimalItem = new DefaultItem<decimal>(decimalDefault, decimalMax, chkMaxDateIncluded.Checked, decimalMin, chkMinDateIncluded.Checked);
                    SetDefaultItem(decimalItem, _physicalDataFieldType);
                    break;
            } 
        }

        /// <summary>
        /// 设置时间
        /// </summary>
        private void SetTimeOnControls()
        {
            if (!enableEditValue)
            {
                return;
            }

            DateTime dtDefault = DateTime.MinValue;
            DateTime dtMax = DateTime.MinValue;
            DateTime dtMin = DateTime.MinValue;
            if (timeDefaultValue.EditValue != null)
            {
                dtDefault = DateTime.Parse(string.Format("{0} {1}", AppSettingHelper.YearMonthDay, timeDefaultValue.Time.ToLongTimeString()));               
            }
            if (timeMinValue.EditValue != null)
            {
                dtMin = DateTime.Parse(string.Format("{0} {1}", AppSettingHelper.YearMonthDay, timeMinValue.Time.ToLongTimeString()));
            }
            if (timeMaxValue.EditValue != null)
            {
                dtMax = DateTime.Parse(string.Format("{0} {1}", AppSettingHelper.YearMonthDay, timeMaxValue.Time.ToLongTimeString()));
            }            
            DefaultItem<DateTime> timeItem = new DefaultItem<DateTime>(dtDefault, dtMax, chkMaxDateIncluded.Checked, dtMin, chkMinDateIncluded.Checked);
            SetDefaultItem(timeItem, _physicalDataFieldType);
        }

        /// <summary>
        /// 设置日期
        /// </summary>
        private void SetDateOnControls()
        {
            if (!enableEditValue)
            {
                return;
            }

            DateTime dtDefault = GetDateTime(dateDefaultValue);
            DateTime dtMax = GetDateTime(dateMaxValue);
            DateTime dtMin = GetDateTime(dateMinValue);
            DefaultItem<DateTime> dateItem = new DefaultItem<DateTime>(dtDefault, dtMax, chkMaxDateIncluded.Checked, dtMin, chkMinDateIncluded.Checked);
           
            SetDefaultItem(dateItem, _physicalDataFieldType);
        }

        /// <summary>
        /// 获得日期的值
        /// </summary>
        /// <param name="dateEdit"></param>
        /// <returns></returns>
        private DateTime GetDateTime(DateEdit dateEdit)
        {
            DateTime dateValue = DateTime.MinValue;

            if (dateEdit.EditValue != null)
            {
                switch (_physicalDataFieldType)
                {
                    case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                        dateValue = DateTime.Parse(dateEdit.DateTime.ToString("G"));
                        break;

                    case PhysicalDataFieldType.YearAndMonth:
                        dateValue = DateTime.Parse(string.Format("{0}-{1}-01", dateEdit.DateTime.Year, dateEdit.DateTime.Month));
                        break;

                    case PhysicalDataFieldType.YearAndMonthAndDay:
                        dateValue = DateTime.Parse(dateEdit.DateTime.ToShortDateString());
                        break;

                    case PhysicalDataFieldType.MonthAndDay:
                        dateValue = DateTime.Parse(string.Format("{0}-{1}-{2}", AppSettingHelper.Year, dateEdit.DateTime.Month, dateEdit.DateTime.Day));
                        break;                    
                }
            }

            return dateValue;
        }

        /// <summary>
        /// 设置枚举数据
        /// </summary>
        /// <param name="commonNode"></param>
        private void SetEnumDataOnControls(CommonNode commonNode)
        {
            if (!enableEditValue)
            {
                return;
            }

            DefaultItem<decimal> enumItem = null;
            if (commonNode != null)
            {
                enumItem = new DefaultItem<decimal>(commonNode.NodeId);
            }
            SetDefaultItem(enumItem, _physicalDataFieldType);
        }

        /// <summary>
        /// 设置日期或时间格式
        /// </summary>
        /// <param name="physicalDataFieldType"></param>
        /// <param name="dateEdit"></param>
        private void SetDateTimeFormat(PhysicalDataFieldType physicalDataFieldType, DateEdit dateEdit)
        {
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
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 设置控件默认数据与范围
        /// </summary>
        public void SetDataOnControls(object tag)
        {
            enableEditValue = false;
            switch (_physicalDataFieldType)
            {
                case PhysicalDataFieldType.Boolean:
                    if (tag != null)
                    {
                        DefaultItem<bool> boolItem = tag as DefaultItem<bool>;
                        if (boolItem.DefaultValue)
                        {
                            cmbDefaultBoolValue.SelectedItem = UserEnumHelper.GetCommonNode(CustomBool.True);
                        }
                        else
                        {
                            cmbDefaultBoolValue.SelectedItem = UserEnumHelper.GetCommonNode(CustomBool.True);
                        }
                    }
                    else
                    {
                        cmbDefaultBoolValue.SelectedIndex = 0;
                    }                    
                    pnlBool.Visible = true;
                    pnlDigit.Visible = false;
                    pnlString.Visible = false;
                    pnlDate.Visible = false;
                    pnlTime.Visible = false;
                    pnlDropdownList.Visible = false;
                    pnlTreeEnum.Visible = false;

                    break;

                case PhysicalDataFieldType.Int32:
                case PhysicalDataFieldType.Decimal:
                    if (PhysicalDataFieldType == PhysicalDataFieldType.Int32)
                    {
                        seDigitDefault.Properties.IsFloatValue = false;
                        seDigitMin.Properties.IsFloatValue = false;
                        seDigitMax.Properties.IsFloatValue = false;
                    }
                    else
                    {
                        seDigitDefault.Properties.IsFloatValue = true;
                        seDigitMin.Properties.IsFloatValue = true;
                        seDigitMax.Properties.IsFloatValue = true;
                    }
                    pnlDigit.Visible = true;
                    pnlBool.Visible = false;
                    pnlString.Visible = false;
                    pnlDate.Visible = false;
                    pnlTime.Visible = false;
                    pnlDropdownList.Visible = false;
                    pnlTreeEnum.Visible = false;

                    bool maxDigitIncluded = true;
                    bool minDigitIncluded = true;
                    if (tag != null)
                    {
                        if (PhysicalDataFieldType == PhysicalDataFieldType.Int32)
                        {
                            DefaultItem<int> intItem = tag as DefaultItem<int>;
                            maxDigitIncluded = intItem.MaxDataRangeBoundaryType;
                            minDigitIncluded = intItem.MinDataRangeBoundaryType;
                            if (!DataConvertionHelper.IsNullValue(intItem.DefaultValue))
                            {
                                seDigitDefault.EditValue = intItem.DefaultValue;
                            }
                            else
                            {
                                seDigitDefault.EditValue = null;
                            }
                            if (!DataConvertionHelper.IsNullValue(intItem.MaxValue))
                            {
                                seDigitMax.EditValue = intItem.MaxValue;
                            }
                            else
                            {
                                seDigitMax.EditValue = null;
                            }
                            if (!DataConvertionHelper.IsNullValue(intItem.MinValue))
                            {
                                seDigitMin.EditValue = intItem.MinValue;
                            }
                            else
                            {
                                seDigitMin.EditValue = null;
                            }
                        }
                        else
                        {
                            DefaultItem<decimal> decimalItem = tag as DefaultItem<decimal>;
                            maxDigitIncluded = decimalItem.MaxDataRangeBoundaryType;
                            minDigitIncluded = decimalItem.MinDataRangeBoundaryType;
                            if (!DataConvertionHelper.IsNullValue(decimalItem.DefaultValue))
                            {
                                seDigitDefault.EditValue = decimalItem.DefaultValue;
                            }
                            else
                            {
                                seDigitDefault.EditValue = null;
                            }
                            if (!DataConvertionHelper.IsNullValue(decimalItem.MaxValue))
                            {
                                seDigitMax.EditValue = decimalItem.MaxValue;
                            }
                            else
                            {
                                seDigitMax.EditValue = null;
                            }
                            if (!DataConvertionHelper.IsNullValue(decimalItem.MinValue))
                            {
                                seDigitMin.EditValue = decimalItem.MinValue;
                            }
                            else
                            {
                                seDigitMin.EditValue = null;
                            }
                        }
                    }
                    else
                    {
                        seDigitDefault.EditValue = null;
                        seDigitMax.EditValue = null;
                        seDigitMin.EditValue = null;
                    }
                    chkMaxDigitIncluded.Checked = maxDigitIncluded;
                    chkMinDigitIncluded.Checked = minDigitIncluded;
                    break;

                case PhysicalDataFieldType.ArbitraryString:
                case PhysicalDataFieldType.ExtendedArbitraryString:
                case PhysicalDataFieldType.NumeralString:
                case PhysicalDataFieldType.CharString:
                case PhysicalDataFieldType.MixedString:
                case PhysicalDataFieldType.EncryptedString:
                    if (tag != null)
                    {
                        DefaultItem<string> stringItem = tag as DefaultItem<string>;
                        txtDefaultStringValue.Text = stringItem.DefaultValue;
                    }
                    else
                    {
                        txtDefaultStringValue.Text = string.Empty;
                    }
                    pnlString.Visible = true;
                    pnlBool.Visible = false;
                    pnlDigit.Visible = false;
                    pnlDate.Visible = false;
                    pnlTime.Visible = false;
                    pnlDropdownList.Visible = false;
                    pnlTreeEnum.Visible = false;
                    break;

                case PhysicalDataFieldType.YearAndMonthAndDayAndTime:
                case PhysicalDataFieldType.YearAndMonthAndDay:
                case PhysicalDataFieldType.YearAndMonth:
                case PhysicalDataFieldType.MonthAndDay:                   
                    SetDateTimeFormat(_physicalDataFieldType, dateDefaultValue);
                    SetDateTimeFormat(_physicalDataFieldType, dateMaxValue);
                    SetDateTimeFormat(_physicalDataFieldType, dateMinValue);
                    pnlDate.Visible = true;
                    pnlBool.Visible = false;
                    pnlDigit.Visible = false;
                    pnlString.Visible = false;
                    pnlTime.Visible = false;
                    pnlDropdownList.Visible = false;
                    pnlTreeEnum.Visible = false;

                    bool maxDateIncluded = true;
                    bool minDateIncluded = true;
                    if (tag != null)
                    {
                        DefaultItem<DateTime> dateItem = tag as DefaultItem<DateTime>;
                        maxDateIncluded = dateItem.MaxDataRangeBoundaryType;
                        minDateIncluded = dateItem.MinDataRangeBoundaryType;
                        if (!DataConvertionHelper.IsNullValue(dateItem.DefaultValue))
                        {
                            dateDefaultValue.EditValue = dateItem.DefaultValue;
                        }
                        else
                        {
                            dateDefaultValue.EditValue = null;
                        }
                        if (!DataConvertionHelper.IsNullValue(dateItem.MaxValue))
                        {
                            dateMaxValue.EditValue = dateItem.MaxValue;
                        }
                        else
                        {
                            dateMaxValue.EditValue = null;
                        }
                        if (!DataConvertionHelper.IsNullValue(dateItem.MinValue))
                        {
                            dateMinValue.EditValue = dateItem.MinValue;
                        }
                        else
                        {
                            dateMinValue.EditValue = null;
                        }
                    }
                    else
                    {
                        dateDefaultValue.EditValue = null;
                        dateMaxValue.EditValue = null;
                        dateMinValue.EditValue = null;
                    }
                    chkMaxDateIncluded.Checked = maxDateIncluded;
                    chkMinDateIncluded.Checked = minDateIncluded;
                    break;

                case PhysicalDataFieldType.Time:
                    timeDefaultValue.Properties.Mask.EditMask = "HH:mm:ss";
                    timeDefaultValue.Properties.NullText = string.Empty;
                    timeMaxValue.Properties.Mask.EditMask = "HH:mm:ss";
                    timeMaxValue.Properties.NullText = string.Empty;
                    timeMinValue.Properties.Mask.EditMask = "HH:mm:ss";
                    timeMinValue.Properties.NullText = string.Empty;
                    pnlTime.Visible = true;
                    pnlBool.Visible = false;
                    pnlDigit.Visible = false;
                    pnlString.Visible = false;
                    pnlDate.Visible = false;
                    pnlDropdownList.Visible = false;
                    pnlTreeEnum.Visible = false;

                    bool minTimeIncluded = true;
                    bool maxTimeIncluded = true;
                    if (tag != null)
                    {
                        DefaultItem<DateTime> dateTimeItem = tag as DefaultItem<DateTime>;
                        minTimeIncluded = dateTimeItem.MaxDataRangeBoundaryType;
                        maxTimeIncluded = dateTimeItem.MinDataRangeBoundaryType;
                        if (!DataConvertionHelper.IsNullValue(dateTimeItem.DefaultValue))
                        {
                            timeDefaultValue.EditValue = dateTimeItem.DefaultValue;
                        }
                        else
                        {
                            timeDefaultValue.EditValue = null;
                        }
                        if (!DataConvertionHelper.IsNullValue(dateTimeItem.MaxValue))
                        {
                            timeMaxValue.EditValue = dateTimeItem.MaxValue;
                        }
                        else
                        {
                            timeMaxValue.EditValue = null;
                        }
                        if (!DataConvertionHelper.IsNullValue(dateTimeItem.MinValue))
                        {
                            timeMinValue.EditValue = dateTimeItem.MinValue;
                        }
                        else
                        {
                            timeMinValue.EditValue = null;
                        }
                    }
                    else
                    {
                        timeDefaultValue.EditValue = null;
                        timeMaxValue.EditValue = null;
                        timeMinValue.EditValue = null;
                    }
                    chkMinTimeIncluded.Checked = minTimeIncluded;
                    chkMaxTimeIncluded.Checked = maxTimeIncluded;
                    break;

                case PhysicalDataFieldType.DropdownListEnum:

                    cmdDropdownList.Properties.Items.Clear();
                    IList<CommonNode> enumCommonNodes = CustomEnumContract.GetChildNodes(ParentEnumId);
                    cmdDropdownList.Properties.Items.AddRange(enumCommonNodes.ToArray());
                    if (tag != null)
                    {
                        DefaultItem<decimal> enumItem = tag as DefaultItem<decimal>;
                        if (!DataConvertionHelper.IsNullValue(enumItem.DefaultValue))
                        {
                            cmdDropdownList.SelectedItem = CustomEnumContract.GetCommonNode(enumItem.DefaultValue);
                        }
                        else
                        {
                            cmdDropdownList.SelectedItem = null;
                        }
                    }
                    else
                    {
                        cmdDropdownList.EditValue = null;
                    }
                    pnlDropdownList.Visible = true;
                    pnlBool.Visible = false;
                    pnlDigit.Visible = false;
                    pnlString.Visible = false;
                    pnlDate.Visible = false;
                    pnlTime.Visible = false;
                    pnlTreeEnum.Visible = false;
                    break;

                case PhysicalDataFieldType.TreeViewEnum:
                case PhysicalDataFieldType.CscadeEnum:
                    cmbTreeEnum.Value = null;
                    cmbTreeEnum.TreeDropdownHandler = new TreeDropdownItems(CustomEnumContract, ParentEnumId);
                    cmbTreeEnum.InitalizeTreeView();
                    pnlTreeEnum.Visible = true;
                    pnlBool.Visible = false;
                    pnlDigit.Visible = false;
                    pnlString.Visible = false;
                    pnlDate.Visible = false;
                    pnlTime.Visible = false;
                    pnlDropdownList.Visible = false;
                    break;
            }
            enableEditValue = true;
        }

        #endregion

    }
}
