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
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class AppointmentModule : UserControl, ITreeNodeShow
    {
        #region 私有变量

        #endregion

        #region 属性

        /// <summary>
        /// 预约契约
        /// </summary>
        public IAppointmentBusinessContract AppointmentBusinessContract
        {
            get; set;
        }
        
        /// <summary>
        /// 表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get;
            set;
        }

        /// <summary>
        /// 填报契约
        /// </summary>
        public ICustomDataContract CustomDataContract
        {
            get;
            set;
        }

        /// <summary>
        /// 工作流契约
        /// </summary>
        public ICustomWorkflowContract CustomWorkflowContract
        {
            get;
            set;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public AppointmentModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbAssociatedBussinessType, typeof(AssociatedBussinessType));
            UserControlHelper.InitImageComboBoxEdit(icmbAppointmentType, typeof(AppointmentType));
            UserControlHelper.InitImageComboBoxEdit(icmbPeriodType, typeof(PeriodType));
            UserControlHelper.InitImageComboBoxEdit(icmbAppointmentBussinesType, typeof(AppointmentBussinesType));
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AppointmentModule_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 设置关联业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleBusiness_OpenLink(object sender, OpenLinkEventArgs e)
        {
            AssociatedBussinessType associatedBussinessType = (AssociatedBussinessType)Convert.ToByte(icmbAssociatedBussinessType.EditValue);
            switch (associatedBussinessType)
            {
                case AssociatedBussinessType.Table:
                    icmbAppointmentType.ReadOnly = false;
                    DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
                    frmDataTableItems.TableFilter = TableFilter.All;
                    frmDataTableItems.DataWarehouseId = 0;
                    frmDataTableItems.Text = "数据表选择";
                    frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
                    frmDataTableItems.NodeSelected = delegate (CommonNode commonNode) {
                        if (commonNode != null)
                        {
                            txtBusiness.Text = CustomTableContract.GetFullName(commonNode.NodeId);
                            txtBusiness.Tag = commonNode;
                        }
                        else
                        {
                            txtBusiness.Text = string.Empty;
                            txtBusiness.Tag = null;
                        }
                    };
                    frmDataTableItems.ShowDialog();
                    break;

                case AssociatedBussinessType.DataFilled:
                    icmbAppointmentType.ReadOnly = true;
                    icmbAppointmentType.SelectedIndex = 0;
                    DataFillItemsForm frmDataFillItems = new DataFillItemsForm();
                    frmDataFillItems.ToolTip = "提示：只能选择数据填报节点。";
                    frmDataFillItems.NodeSelected = delegate (CommonNode node) {
                        if (node != null)
                        {
                            txtBusiness.Text = CustomDataContract.GetFullName(node.NodeId);
                            txtBusiness.Tag = node;
                        }
                        else
                        {
                            txtBusiness.Text = string.Empty;
                            txtBusiness.Tag = null;
                        }
                    };
                    frmDataFillItems.ShowDialog();                    
                    break;

                case AssociatedBussinessType.Workflow:
                    icmbAppointmentType.ReadOnly = true;
                    icmbAppointmentType.SelectedIndex = 0;
                    WorkflowItemsForm frmWorkflowItems = new WorkflowItemsForm();
                    frmWorkflowItems.ToolTip = "提示：只能选择工作流节点。";
                    frmWorkflowItems.NodeSelected = delegate (CommonNode node)
                    {
                        if (node != null)
                        {
                            txtBusiness.Text = CustomWorkflowContract.GetFullName(node.NodeId);
                            txtBusiness.Tag = node.NodeId;
                        }
                        else
                        {
                            txtBusiness.Text = string.Empty;
                            txtBusiness.Tag = null;
                        }
                    };
                    frmWorkflowItems.ShowDialog();
                    break;
            }
        }

        /// <summary>
        /// 预约类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbAppointmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            AppointmentType appointmentType = (AppointmentType)Convert.ToByte(icmbAppointmentType.EditValue);
            switch (appointmentType)
            {
                case AppointmentType.RealTime:
                    icmbPeriodType.ReadOnly = true;
                    txtPeriodTime.ReadOnly = true;
                    icmbPeriodType.SelectedIndex = 0;
                    txtPeriodTime.Text = string.Empty;
                    break;

                case AppointmentType.Time:
                    icmbPeriodType.ReadOnly = false;
                    txtPeriodTime.ReadOnly = false;
                    break;
            }
        }

        /// <summary>
        /// 时长类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbPeriodType_SelectedIndexChanged(object sender, EventArgs e)
        {
            PeriodType periodType = (PeriodType)Convert.ToByte(icmbAppointmentType.EditValue);
            if (periodType == PeriodType.Custom)
            {
                txtPeriodTime.ReadOnly = false;
            }
            else
            {
                txtPeriodTime.ReadOnly = true;
                txtPeriodTime.Text = string.Empty;
            }
        }

        /// <summary>
        /// 设置启动业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleAppointmentBussines_OpenLink(object sender, OpenLinkEventArgs e)
        {
            AppointmentBussinesType appointmentBussinesType = (AppointmentBussinesType)Convert.ToByte(icmbAppointmentBussinesType.EditValue);
            switch (appointmentBussinesType)
            {
                case AppointmentBussinesType.DataFilled:
                    icmbAppointmentType.ReadOnly = true;
                    icmbAppointmentType.SelectedIndex = 0;
                    DataFillItemsForm frmDataFillItems = new DataFillItemsForm();
                    frmDataFillItems.ToolTip = "提示：只能选择数据填报节点。";
                    frmDataFillItems.NodeSelected = delegate (CommonNode node) {
                        if (node != null)
                        {
                            txtAppointmentBussiness.Text = CustomDataContract.GetFullName(node.NodeId);
                            txtAppointmentBussiness.Tag = node;
                        }
                        else
                        {
                            txtAppointmentBussiness.Text = string.Empty;
                            txtAppointmentBussiness.Tag = null;
                        }
                    };
                    frmDataFillItems.ShowDialog();
                    break;

                case AppointmentBussinesType.Workflow:
                    icmbAppointmentType.ReadOnly = true;
                    icmbAppointmentType.SelectedIndex = 0;
                    WorkflowItemsForm frmWorkflowItems = new WorkflowItemsForm();
                    frmWorkflowItems.ToolTip = "提示：只能选择工作流节点。";
                    frmWorkflowItems.NodeSelected = delegate (CommonNode node)
                    {
                        if (node != null)
                        {
                            txtAppointmentBussiness.Text = CustomWorkflowContract.GetFullName(node.NodeId);
                            txtAppointmentBussiness.Tag = node.NodeId;
                        }
                        else
                        {
                            txtAppointmentBussiness.Text = string.Empty;
                            txtAppointmentBussiness.Tag = null;
                        }
                    };
                    frmWorkflowItems.ShowDialog();
                    break;
            }
        }

        /// <summary>
        /// 关联类型变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbAssociatedBussinessType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtBusiness.Text = string.Empty;
            txtBusiness.Tag = null;
        }

        /// <summary>
        /// 业务类型变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbAppointmentBussinesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtAppointmentBussiness.Text = string.Empty;
            txtAppointmentBussiness.Tag = null;
        }

        #endregion

        #region 实现 ITreeNodeShow 接口

        /// <summary>
        /// 节点编号
        /// </summary>
        public decimal TreeNodeId
        {
            get;
            set;
        }

        /// <summary>
        /// 默认编码
        /// </summary>
        public string DefaultCode
        {
            set
            {
                txtAppointmentCode.Text = value;
            }
            get
            {
                return txtAppointmentCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtAppointmentName.ReadOnly = readOnly;
            txtAppointmentCode.ReadOnly = readOnly;
            icmbAssociatedBussinessType.ReadOnly = readOnly;
            if (readOnly)
            {
                icmbAppointmentType.ReadOnly = readOnly;
            }
            else
            {
                AssociatedBussinessType associatedBussinessType = (AssociatedBussinessType)Convert.ToByte(icmbAssociatedBussinessType.EditValue);
                switch (associatedBussinessType)
                {
                    case AssociatedBussinessType.Table:
                        icmbAppointmentType.ReadOnly = false;
                        break;

                    case AssociatedBussinessType.DataFilled:
                    case AssociatedBussinessType.Workflow:
                        icmbAppointmentType.ReadOnly = true;
                        break;
                }
            }
            hleBusiness.Enabled = !readOnly;
            if (readOnly)
            {
                icmbPeriodType.ReadOnly = readOnly;
                txtPeriodTime.ReadOnly = readOnly;
            }
            else
            {
                AppointmentType appointmentType = (AppointmentType)Convert.ToByte(icmbAppointmentType.EditValue);
                switch (appointmentType)
                {
                    case AppointmentType.RealTime:
                        icmbPeriodType.ReadOnly = true;
                        txtPeriodTime.ReadOnly = true;                        
                        break;

                    case AppointmentType.Time:
                        icmbPeriodType.ReadOnly = false;
                        txtPeriodTime.ReadOnly = false;
                        break;
                }
            }
            icmbAppointmentBussinesType.ReadOnly = readOnly;
            hleAppointmentBussines.Enabled = !readOnly;
            chkAppointmentEnabled.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;            
            if (!txtAppointmentName.ReadOnly)
            {
                txtAppointmentName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            AppointmentBusinessInfo appointmentBusinessInfo = AppointmentBusinessContract.GetModelInfo(commonNode.NodeId);
            if (appointmentBusinessInfo != null)
            {
                txtAppointmentName.Text = appointmentBusinessInfo.AppointmentName;
                txtAppointmentCode.Text = appointmentBusinessInfo.AppointmentCode;
                icmbAssociatedBussinessType.EditValue = appointmentBusinessInfo.AssociatedBussinessType;
                
                AssociatedBussinessType associatedBussinessType = (AssociatedBussinessType)appointmentBusinessInfo.AssociatedBussinessType;
                switch (associatedBussinessType)
                {
                    case AssociatedBussinessType.Table:
                        txtBusiness.Text = CustomTableContract.GetFullName(appointmentBusinessInfo.TableId);
                        txtBusiness.Tag = CustomTableContract.GetCommonNode(appointmentBusinessInfo.TableId);
                        break;

                    case AssociatedBussinessType.DataFilled:
                        txtBusiness.Text = CustomDataContract.GetFullName(appointmentBusinessInfo.DataId);
                        txtBusiness.Tag = CustomDataContract.GetCommonNode(appointmentBusinessInfo.DataId);
                        break;

                    case AssociatedBussinessType.Workflow:
                        txtBusiness.Text = CustomWorkflowContract.GetFullName(appointmentBusinessInfo.WorkflowId);
                        txtBusiness.Tag = CustomWorkflowContract.GetCommonNode(appointmentBusinessInfo.WorkflowId);
                        break;
                }
                AppointmentBussinesType appointmentBussinesType = (AppointmentBussinesType)appointmentBusinessInfo.AppointmentBussinesType;
                switch (appointmentBussinesType)
                {
                    case AppointmentBussinesType.DataFilled:
                        txtAppointmentBussiness.Text = CustomDataContract.GetFullName(appointmentBusinessInfo.ParentDataId);
                        txtAppointmentBussiness.Tag = CustomDataContract.GetCommonNode(appointmentBusinessInfo.ParentDataId);
                        break;

                    case AppointmentBussinesType.Workflow:
                        txtAppointmentBussiness.Text = CustomWorkflowContract.GetFullName(appointmentBusinessInfo.ParentWorkflowId);
                        txtAppointmentBussiness.Tag = CustomWorkflowContract.GetCommonNode(appointmentBusinessInfo.ParentWorkflowId);
                        break;
                }
                icmbAppointmentType.EditValue = appointmentBusinessInfo.AppointmentType;               
                icmbPeriodType.EditValue = appointmentBusinessInfo.PeriodType;
                if (appointmentBusinessInfo.PeriodTime > 0)
                {
                    txtPeriodTime.Text = DataConvertionHelper.EndowStringOfInt(appointmentBusinessInfo.PeriodTime);
                }
                else
                {
                    txtPeriodTime.Text = string.Empty;
                }
                icmbAppointmentBussinesType.EditValue = appointmentBusinessInfo.AppointmentBussinesType;              
                chkAppointmentEnabled.Checked = appointmentBusinessInfo.AppointmentEnabled;
                txtNotes.Text = appointmentBusinessInfo.Notes;                
            }
            else
            {
                ClearModelInfo();
            }
        }

        /// <summary>
        /// 清除界面数据
        /// </summary>
        public void ClearModelInfo()
        {
            txtAppointmentName.Text = string.Empty;
            txtAppointmentCode.Text = string.Empty;
            icmbAssociatedBussinessType.SelectedIndex = 0;
            icmbAppointmentType.SelectedIndex = 0;
            icmbPeriodType.SelectedIndex = 0;
            txtPeriodTime.Text = string.Empty;
            icmbAppointmentBussinesType.SelectedIndex = 0;
            chkAppointmentEnabled.Checked = false;
            txtNotes.Text = string.Empty;            
            if (!txtAppointmentName.ReadOnly)
            {
                txtAppointmentName.Focus();
            }
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;

            AppointmentBusinessInfo appointmentBusinessInfo = GetModelInfo();            
            result = ValidationHelper.Validate<AppointmentBusinessInfo>(appointmentBusinessInfo, out warning);
            if (result)
            {
                AppointmentType appointmentType = (AppointmentType)Convert.ToByte(icmbAppointmentType.EditValue);
                if(appointmentType == AppointmentType.Time)
                {
                PeriodType periodType = (PeriodType)Convert.ToByte(icmbPeriodType.EditValue);
                    if (periodType == PeriodType.Custom)
                    {
                        string periodTimeValue = txtPeriodTime.Text.Trim();
                        if (!string.IsNullOrWhiteSpace(periodTimeValue))
                        {
                            if (!DataConvertionHelper.IsInt32(periodTimeValue))
                            {
                                result = false;
                                warning = "时长段为整形。";
                            }
                        }
                        else
                        {
                            result = false;
                            warning = "时长段未设置。";
                        }
                    }                  
                }
                CommonNode commonNode = txtBusiness.Tag as CommonNode;
                if (commonNode == null)
                {
                    result = false;
                    warning = "关联业务未设置。";
                }
                CommonNode node = txtAppointmentBussiness.Tag as CommonNode;
                if (node == null)
                {
                    result = false;
                    warning = "启动业务未设置。";
                }
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取视图的信息
        /// </summary>
        /// <returns></returns>
        public AppointmentBusinessInfo GetModelInfo()
        {
            CommonNode commonNode = txtBusiness.Tag as CommonNode;
            if (commonNode == null)
            {
                throw new ArgumentNullException("关联业务未设置。");
            }
            byte associatedBussinessTypeValue = Convert.ToByte(icmbAssociatedBussinessType.EditValue);
            AssociatedBussinessType associatedBussinessType = (AssociatedBussinessType)associatedBussinessTypeValue;
            decimal workflowId = decimal.MinValue;
            decimal dataId = decimal.MinValue;
            decimal tableId = decimal.MinValue;
            switch (associatedBussinessType)
            {
                case AssociatedBussinessType.Table:
                    tableId = commonNode.NodeId;
                    break;

                case AssociatedBussinessType.DataFilled:
                    dataId = commonNode.NodeId;
                    break;

                case AssociatedBussinessType.Workflow:
                    workflowId = commonNode.NodeId;
                    break;
            }
            CommonNode node = txtAppointmentBussiness.Tag as CommonNode;
            if (node == null)
            {
                throw new ArgumentNullException("启动业务未设置。");
            }
            byte appointmentBussinesTypeValue = Convert.ToByte(icmbAppointmentBussinesType.EditValue);
            AppointmentBussinesType appointmentBussinesType = (AppointmentBussinesType)appointmentBussinesTypeValue;
            decimal parentWorkflowId = decimal.MinValue;
            decimal parentDataId = decimal.MinValue;
            switch (appointmentBussinesType)
            {
                case AppointmentBussinesType.DataFilled:
                    parentDataId = node.NodeId;
                    break;

                case AppointmentBussinesType.Workflow:
                    parentWorkflowId = node.NodeId;
                    break;
            }

            int periodTime = 0;
            PeriodType periodType = (PeriodType)Convert.ToByte(icmbAppointmentType.EditValue);
            if (periodType == PeriodType.Custom)
            {
                periodTime = DataConvertionHelper.GetConvertedInt(txtPeriodTime.Text.Trim());
            }
            AppointmentBusinessInfo appointmentBusinessInfo = new AppointmentBusinessInfo()
            {
                AppointmentName = txtAppointmentName.Text.Trim(),
                AppointmentCode = txtAppointmentCode.Text.Trim(),
                WorkflowId = workflowId,
                DataId = dataId,
                TableId = tableId,
                ParentWorkflowId = parentWorkflowId,
                ParentDataId = parentDataId,
                AssociatedBussinessType = associatedBussinessTypeValue,
                AppointmentType = Convert.ToByte(icmbAppointmentType.EditValue),
                PeriodType = Convert.ToByte(icmbPeriodType.EditValue),
                PeriodTime = periodTime,
                AppointmentBussinesType = Convert.ToByte(icmbAppointmentBussinesType.EditValue),
                AppointmentEnabled = chkAppointmentEnabled.Checked,
                Notes = txtNotes.Text.Trim()
            };

            return appointmentBusinessInfo;
        }


        #endregion       
    }
}
