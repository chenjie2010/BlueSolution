using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.WinFormsLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.UserModule;
using Blue.WCFContracts.SystemModule;
using Blue.WindowsFormsClient;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class WorkflowAndDataFieldForm : Form
    {
        #region 契约接口

        private readonly ICustomWorkflowProcessContract customWorkflowProcessContract;

        private readonly ICustomDataFieldContract customDataFieldContract;

        private readonly ICustomTableContract customTableContract;

        private readonly IAssociatedDataFieldContract associatedDataFieldContract;

        private readonly ICustomEnumContract customEnumContract;

        #endregion

        #region 属性

        /// <summary>
        /// 业务流程编号
        /// </summary>
        public decimal ProcessId
        {
            get;
            set;
        }

        /// <summary>
        /// 字段编号
        /// </summary>
        public decimal DataFieldId
        {
            get;
            set;
        }

        /// <summary>
        /// 设置工作流字段条件
        /// </summary>
        public SetWorkflowProcessDataFieldDelegate SetWorkflowProcessDataFieldHandler
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowAndDataFieldForm()
        {
            InitializeComponent();
            customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
            customTableContract = BusinessChannelFactory.CreateCustomTableContract();
            customDataFieldContract = BusinessChannelFactory.CreateCustomDataFieldContract();
            associatedDataFieldContract = BusinessChannelFactory.CreateAssociatedDataFieldContract();
            customEnumContract = BusinessChannelFactory.CreateCustomEnumContract();

            UserControlHelper.InitImageComboBoxEdit(icmbDataWarehouse, typeof(DataWarehouse));
            UserControlHelper.InitImageComboBoxEdit(icmbNextRelation, typeof(NextTableRelation));
            DataFieldId = 0;
            Height = 275;
            pnlBool.Visible = false;
            pnlDate.Visible = false;
            pnlDigit.Visible = false;
            pnlEnum.Visible = false;
            pnlString.Visible = false;
            IList<EnumItem> enumItems = new List<EnumItem>();
            IList<EnumItem> stringEnumItems = new List<EnumItem>();
            EnumItem eiEqual = UserEnumHelper.GetEnumItem(DataFieldCondition.Equal);
            EnumItem eiNotEqual = UserEnumHelper.GetEnumItem(DataFieldCondition.Not);
            enumItems.Add(eiEqual);
            enumItems.Add(eiNotEqual);
            stringEnumItems.Add(eiEqual);
            stringEnumItems.Add(eiNotEqual);

            EnumItem eiLike = UserEnumHelper.GetEnumItem(DataFieldCondition.Like);
            EnumItem eiStartWith = UserEnumHelper.GetEnumItem(DataFieldCondition.StartWith);
            stringEnumItems.Add(eiLike);
            stringEnumItems.Add(eiStartWith);

            UserControlHelper.InitImageComboBoxEditWithImage(icmbBoolCondition, enumItems);
            UserControlHelper.InitImageComboBoxEditWithImage(icmbEnumCondition, enumItems);
            UserControlHelper.InitImageComboBoxEditWithImage(icmbStringCondition, stringEnumItems);
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowAndDataFieldForm_Load(object sender, EventArgs e)
        {
            if (DataFieldId > 0)
            {
                btxtDataField.ReadOnly = true;
                WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo = customWorkflowProcessContract.GetModelInfo(ProcessId, DataFieldId);
                CustomDataFieldInfo customDataFieldInfo = InitDataFieldCondition(DataFieldId);
                icmbDataWarehouse.EditValue = customTableContract.GetDataWarehouseId(customDataFieldInfo.TableId);
                btxtDataField.Text = customDataFieldContract.GetFullLogicalName(DataFieldId);
                btxtDataField.Tag = customDataFieldContract.GetCommonNode(DataFieldId);
                if (pnlBool.Visible)
                {
                    icmbBoolCondition.EditValue = workflowProcessAndDataFieldInfo.FstCondition;
                    chkBool.Checked = workflowProcessAndDataFieldInfo.BoolValue;                    
                }
                else if (pnlDate.Visible)
                {
                    if (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstTimeValue))
                    {
                        dateMinValue.EditValue = workflowProcessAndDataFieldInfo.FstTimeValue;
                        DataFieldCondition dataFieldCondition = (DataFieldCondition)workflowProcessAndDataFieldInfo.FstCondition;
                        if (dataFieldCondition == DataFieldCondition.MoreOrEqual)
                        {
                            chkMinDateIncluded.Checked = true;
                        }
                        else
                        {
                            chkMinDateIncluded.Checked = false;
                        }
                    }
                    if (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdTimeValue))
                    {
                        dateMinValue.EditValue = workflowProcessAndDataFieldInfo.ScdTimeValue;
                        DataFieldCondition dataFieldCondition = (DataFieldCondition)workflowProcessAndDataFieldInfo.ScdCondition;
                        if (dataFieldCondition == DataFieldCondition.LessOrEqual)
                        {
                            chkMaxDateIncluded.Checked = true;
                        }
                        else
                        {
                            chkMaxDateIncluded.Checked = false;
                        }
                    }
                }
                else if (pnlDigit.Visible && pnlDigit.Tag != null)
                {
                    BasedDataType dataFieldBase = (BasedDataType)pnlDigit.Tag;
                    if (dataFieldBase == BasedDataType.Decimal)
                    {
                        if (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstDecimalValue))
                        {
                            seDigitMin.EditValue = workflowProcessAndDataFieldInfo.FstDecimalValue;
                            DataFieldCondition dataFieldCondition = (DataFieldCondition)workflowProcessAndDataFieldInfo.FstCondition;
                            if (dataFieldCondition == DataFieldCondition.MoreOrEqual)
                            {
                                chkMinDigitIncluded.Checked = true;
                            }
                            else
                            {
                                chkMinDigitIncluded.Checked = false;
                            }
                        }
                        if (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdDecimalValue))
                        {
                            seDigitMax.EditValue = workflowProcessAndDataFieldInfo.ScdDecimalValue;
                            DataFieldCondition dataFieldCondition = (DataFieldCondition)workflowProcessAndDataFieldInfo.ScdCondition;
                            if (dataFieldCondition == DataFieldCondition.LessOrEqual)
                            {
                                chkMaxDigitIncluded.Checked = true;
                            }
                            else
                            {
                                chkMaxDigitIncluded.Checked = false;
                            }
                        }
                    }
                    else if (dataFieldBase == BasedDataType.Int32)
                    {
                        if (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.FstIntegerValue))
                        {
                            seDigitMin.EditValue = workflowProcessAndDataFieldInfo.FstIntegerValue;
                            DataFieldCondition dataFieldCondition = (DataFieldCondition)workflowProcessAndDataFieldInfo.FstCondition;
                            if (dataFieldCondition == DataFieldCondition.MoreOrEqual)
                            {
                                chkMinDigitIncluded.Checked = true;
                            }
                            else
                            {
                                chkMinDigitIncluded.Checked = false;
                            }
                        }
                        if (!DataConvertionHelper.IsNullValue(workflowProcessAndDataFieldInfo.ScdIntegerValue))
                        {
                            seDigitMax.EditValue = workflowProcessAndDataFieldInfo.ScdIntegerValue;
                            DataFieldCondition dataFieldCondition = (DataFieldCondition)workflowProcessAndDataFieldInfo.ScdCondition;
                            if (dataFieldCondition == DataFieldCondition.LessOrEqual)
                            {
                                chkMaxDigitIncluded.Checked = true;
                            }
                            else
                            {
                                chkMaxDigitIncluded.Checked = false;
                            }
                        }
                    }
                    else
                    {
                        throw new ArgumentException("业务不支持该类型。");
                    }                    
                }
                else if (pnlEnum.Visible)
                {
                    if(!string.IsNullOrWhiteSpace(workflowProcessAndDataFieldInfo.StringValue))
                    {
                        icmbEnumCondition.EditValue = workflowProcessAndDataFieldInfo.FstCondition;
                        btxtEnum.Text = workflowProcessAndDataFieldInfo.StringValue;
                    }                    
                }
                else if (pnlString.Visible)
                {
                    if (!string.IsNullOrWhiteSpace(workflowProcessAndDataFieldInfo.StringValue))
                    {
                        icmbStringCondition.EditValue = workflowProcessAndDataFieldInfo.FstCondition;
                        txtStringValue.Text = workflowProcessAndDataFieldInfo.StringValue;
                    }
                }
            }            
        }

        /// <summary>
        /// 字段设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtDataField_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            DataFieldItemsForm frmDataFieldItems = new DataFieldItemsForm();
            frmDataFieldItems.DataFieldShowMode = DataFieldShowMode.DataWarehouse;
            frmDataFieldItems.DataWarehouseId = Convert.ToByte(icmbDataWarehouse.EditValue);
            frmDataFieldItems.DataFieldFilter = DataFieldFilter.PhysicalFieldAndKeyLogicalField;
            frmDataFieldItems.NodeSelected = delegate (CommonNode node)
            {
                if (node != null)
                {
                    if (customWorkflowProcessContract.GetModelInfo(ProcessId, node.NodeId) == null)
                    {
                        btxtDataField.Text = customDataFieldContract.GetFullLogicalName(node.NodeId);
                        btxtDataField.Tag = node;
                        InitDataFieldCondition(node.NodeId);
                    }
                    else
                    {
                        MessageBox.Show("该字段条件已经存在，不能重复增加。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    btxtDataField.Text = string.Empty;
                    btxtDataField.Tag = null;
                }
            };
            frmDataFieldItems.ShowDialog();

        }

        /// <summary>
        /// 枚举选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtEnum_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            CommonNode commonNode = btxtDataField.Tag as CommonNode;
            if (commonNode != null)
            {
                CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModelInfo(commonNode.NodeId);
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                if (dataFieldProperty == DataFieldProperty.PhysicalDataField && customDataFieldInfo.EnumId > 0)
                {
                    IList<CommonNode> nodes = btxtEnum.Tag as IList<CommonNode>;
                    MultiSelectedItemsForm frmMultiSelectedItems = new MultiSelectedItemsForm();
                    frmMultiSelectedItems.MultiSelectedItemsHandler = new EnumMultiSelectedItems(customEnumContract, customDataFieldInfo.EnumId, true);
                    frmMultiSelectedItems.Text = "枚举选择";
                    frmMultiSelectedItems.GetTreeNodeListDelegate = (commonNodes) =>
                    {
                        btxtEnum.Text = CommonObjHelper.GetCommonNodeNamesWithComma(commonNodes);
                        btxtEnum.Tag = commonNodes;
                    };
                    frmMultiSelectedItems.OperationTip = "提示：请选择枚举。";
                    frmMultiSelectedItems.SetTokenEidtValues(nodes);
                    frmMultiSelectedItems.ShowDialog();
                }
            }
        }

        /// <summary>
        /// 数据变化变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbDataWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            btxtDataField.Text = string.Empty;
            btxtDataField.Tag = null;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (btxtDataField.Tag == null)
            {
                MessageBox.Show("请先选择字段", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CommonNode commonNode = btxtDataField.Tag as CommonNode;
            byte fstCondition = 0;
            byte scdCondition = 0;
            bool boolValue = false;
            string stringValue = string.Empty;
            int fstIntegerValue = 0, scdIntegerValue = 0;
            decimal fstDecimalValue = 0, scdDecimalValue = 0;
            DateTime fstTimeValue = DateTime.MinValue, scdTimeValue = DateTime.MinValue;
            if (pnlBool.Visible)
            {
                fstCondition = Convert.ToByte(icmbBoolCondition.EditValue);
                boolValue = chkBool.Checked;
            }
            else if (pnlDate.Visible)
            {
                if (dateMinValue.EditValue != null)
                {
                    fstTimeValue = DataConvertionHelper.GetDateTime(dateMinValue.EditValue);
                    if (chkMinDateIncluded.Checked)
                    {
                        fstCondition = (byte)DataFieldCondition.MoreOrEqual;
                    }
                    else
                    {
                        fstCondition = (byte)DataFieldCondition.More;
                    }
                }
                if (dateMaxValue.EditValue != null)
                {
                    scdTimeValue = DataConvertionHelper.GetDateTime(dateMaxValue.EditValue);
                    if (chkMaxDateIncluded.Checked)
                    {
                        scdCondition = (byte)DataFieldCondition.LessOrEqual;
                    }
                    else
                    {
                        scdCondition = (byte)DataFieldCondition.Less;
                    }
                }
                if (!DataConvertionHelper.IsNullValue(fstTimeValue) && !DataConvertionHelper.IsNullValue(scdTimeValue) && scdTimeValue <= fstTimeValue)
                {
                    MessageBox.Show(string.Format("{0}的最大值不能小于或者等于最小值。", commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            else if (pnlDigit.Visible && pnlDigit.Tag != null)
            {
                BasedDataType dataFieldBase = (BasedDataType)pnlDigit.Tag;
                if (dataFieldBase == BasedDataType.Decimal)
                {
                    if (seDigitMin.EditValue != null)
                    {
                        fstDecimalValue = DataConvertionHelper.GetDecimal(seDigitMin.EditValue);
                        if (chkMinDigitIncluded.Checked)
                        {
                            fstCondition = (byte)DataFieldCondition.MoreOrEqual;
                        }
                        else
                        {
                            fstCondition = (byte)DataFieldCondition.More;
                        }
                    }
                    if (seDigitMax.EditValue != null)
                    {
                        scdDecimalValue = DataConvertionHelper.GetDecimal(seDigitMax.EditValue);
                        if (chkMaxDigitIncluded.Checked)
                        {
                            scdCondition = (byte)DataFieldCondition.LessOrEqual;
                        }
                        else
                        {
                            scdCondition = (byte)DataFieldCondition.Less;
                        }
                    }
                    if (!DataConvertionHelper.IsNullValue(fstDecimalValue) && !DataConvertionHelper.IsNullValue(scdDecimalValue) 
                        && scdDecimalValue <= fstDecimalValue)
                    {
                        MessageBox.Show(string.Format("{0}的最大值不能小于或者等于最小值。", commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else if (dataFieldBase == BasedDataType.Int32)
                {
                    if (seDigitMin.EditValue != null)
                    {
                        fstIntegerValue = DataConvertionHelper.GetConvertedInt(seDigitMin.EditValue);
                        if (chkMinDigitIncluded.Checked)
                        {
                            fstCondition = (byte)DataFieldCondition.MoreOrEqual;
                        }
                        else
                        {
                            fstCondition = (byte)DataFieldCondition.More;
                        }
                    }
                    if (seDigitMax.EditValue != null)
                    {
                        scdIntegerValue = DataConvertionHelper.GetConvertedInt(seDigitMax.EditValue);
                        if (chkMaxDigitIncluded.Checked)
                        {
                            scdCondition = (byte)DataFieldCondition.LessOrEqual;
                        }
                        else
                        {
                            scdCondition = (byte)DataFieldCondition.Less;
                        }
                    }
                    if (!DataConvertionHelper.IsNullValue(fstIntegerValue) && !DataConvertionHelper.IsNullValue(scdIntegerValue)
                        && scdIntegerValue <= fstIntegerValue)
                    {
                        MessageBox.Show(string.Format("{0}的最大值不能小于或者等于最小值。", commonNode.NodeName), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                else
                {
                    throw new ArgumentException("业务不支持该类型。");
                }                
            }
            else if (pnlEnum.Visible)
            {
                fstCondition = Convert.ToByte(icmbEnumCondition.EditValue);
                stringValue = btxtEnum.Text;
            }
            else if (pnlString.Visible)
            {
                fstCondition = Convert.ToByte(icmbStringCondition.EditValue);
                stringValue = txtStringValue.Text;
            }
            WorkflowProcessAndDataFieldInfo workflowProcessAndDataFieldInfo = new WorkflowProcessAndDataFieldInfo()
            {
                ProcessId = ProcessId,
                DataFieldId = commonNode.NodeId,
                FstCondition = fstCondition,
                ScdCondition = scdCondition,
                BoolValue = boolValue,
                StringValue = stringValue,
                FstIntegerValue = fstIntegerValue,
                ScdIntegerValue = scdIntegerValue,
                FstDecimalValue = fstDecimalValue,
                ScdDecimalValue = scdDecimalValue,
                FstTimeValue = fstTimeValue,
                ScdTimeValue = scdTimeValue,
                NextRelation = Convert.ToByte(icmbNextRelation.EditValue)
            };
            SetWorkflowProcessDataFieldHandler?.Invoke(workflowProcessAndDataFieldInfo);
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 获得基础字段类型
        /// </summary>
        /// <param name="customDataFieldInfo"></param>
        /// <returns></returns>
        private BasedDataType GetBasedDataType(CustomDataFieldInfo customDataFieldInfo)
        {
            BasedDataType dataFieldBase = BasedDataType.Boolean;
                      
            DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
            switch (dataFieldProperty)
            {
                case DataFieldProperty.PhysicalDataField:
                    PhysicalDataFieldType physicalDataFieldType = (PhysicalDataFieldType)customDataFieldInfo.DataFieldType;
                    if (physicalDataFieldType == PhysicalDataFieldType.PrimaryAssociation || physicalDataFieldType == PhysicalDataFieldType.SecondaryAssociation)
                    {
                        BasedDataType basedDataType = associatedDataFieldContract.GetBasedDataType(customDataFieldInfo.AssociatedDataFieldId);
                        physicalDataFieldType = DataFieldHelper.GetPhysicalDataFieldType(basedDataType);
                    }
                    dataFieldBase = DataFieldHelper.GetBasedDataType(physicalDataFieldType);
                    break;

                case DataFieldProperty.LogicalDataField:
                    dataFieldBase = DataFieldHelper.GetBasedDataType((LogicalDataFieldType)customDataFieldInfo.DataFieldType);
                    break;
            }

            return dataFieldBase;
        }

        /// <summary>
        /// 初始化字段条件
        /// </summary>
        /// <param name="dataFieldId"></param>
        /// <returns></returns>
        private CustomDataFieldInfo InitDataFieldCondition(decimal dataFieldId)
        {
            CustomDataFieldInfo customDataFieldInfo = customDataFieldContract.GetModelInfo(dataFieldId);

            BasedDataType dataFieldBase = GetBasedDataType(customDataFieldInfo);
            switch (dataFieldBase)
            {
                case BasedDataType.Boolean:
                    pnlBool.Visible = true;
                    pnlDate.Visible = false;
                    pnlDigit.Visible = false;
                    pnlEnum.Visible = false;
                    pnlString.Visible = false;                    
                    break;

                case BasedDataType.DateTime:
                    dateMinValue.Properties.DisplayFormat.FormatString = "d";
                    dateMinValue.Properties.EditFormat.FormatString = "d";
                    dateMinValue.Properties.EditMask = "d";
                    dateMaxValue.Properties.DisplayFormat.FormatString = "d";
                    dateMaxValue.Properties.EditFormat.FormatString = "d";
                    dateMaxValue.Properties.EditMask = "d";
                    pnlDate.Visible = true;
                    pnlBool.Visible = false;
                    pnlDigit.Visible = false;
                    pnlEnum.Visible = false;
                    pnlString.Visible = false;
                    break;

                case BasedDataType.Decimal:
                    pnlDigit.Visible = true;
                    pnlBool.Visible = false;
                    pnlDate.Visible = false;
                    pnlEnum.Visible = false;
                    pnlString.Visible = false;
                    pnlDigit.Tag = BasedDataType.Decimal;
                    seDigitMin.Properties.Increment = (decimal)0.1;
                    seDigitMax.Properties.Increment = (decimal)0.1;
                    seDigitMin.Properties.IsFloatValue = true;
                    seDigitMax.Properties.IsFloatValue = true;
                    break;

                case BasedDataType.Int32:
                    pnlDigit.Visible = true;
                    pnlBool.Visible = false;
                    pnlDate.Visible = false;
                    pnlEnum.Visible = false;
                    pnlString.Visible = false;
                    pnlDigit.Tag = BasedDataType.Int32;
                    seDigitMin.Properties.Increment = 1;
                    seDigitMax.Properties.Increment = 1;
                    seDigitMin.Properties.IsFloatValue = false;
                    seDigitMax.Properties.IsFloatValue = false;
                    break;

                case BasedDataType.String:
                    if ((DataFieldProperty)customDataFieldInfo.DataFieldProperty == DataFieldProperty.PhysicalDataField 
                        && customDataFieldInfo.EnumId > 0)
                    {
                        pnlEnum.Visible = true;
                        pnlBool.Visible = false;
                        pnlDate.Visible = false;
                        pnlDigit.Visible = false;
                        pnlString.Visible = false;
                    }
                    else
                    {
                        pnlString.Visible = true;
                        pnlBool.Visible = false;
                        pnlDate.Visible = false;
                        pnlDigit.Visible = false;
                        pnlEnum.Visible = false;
                    }
                    break;
            }

            return customDataFieldInfo;
        }

        #endregion
    }
}
