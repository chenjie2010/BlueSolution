using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.CustomLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class MenuBusinessModule : UserControl, ITreeNodeShow
    {
        #region 属性
        
        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get; set;
        }

        /// <summary>
        /// 业务契约
        /// </summary>
        public ICustomBusinessContract CustomBusinessContract
        {
            get; set;
        }

        /// <summary>
        /// 工作流契约
        /// </summary>
        public ICustomWorkflowContract CustomWorkflowContract
        {
            get; set;
        }

        /// <summary>
        /// 数据填报契约
        /// </summary>
        public ICustomDataContract CustomDataContract
        {
            get; set;
        }

        /// <summary>
        /// 查询契约
        /// </summary>
        public ICustomQueyContract CustomQueyContract
        {
            get;
            set;
        }

        /// <summary>
        /// 报表契约
        /// </summary>
        public ICustomReportContract CustomReportContract
        {
            get;
            set;
        }

        /// <summary>
        /// 数据审核
        /// </summary>
        public IDataAuditingContract DataAuditingContract
        {
            get;
            set;
        }

        /// <summary>
        ///  菜单类型
        /// </summary>
        public byte ParentMenuType
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuBusinessModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            List<EnumItem> enumItems = UserEnumHelper.GetEnumItems(typeof(BusinessMenu));
            int index = 1;
            for (index = 1; index < (enumItems.Count - 1); index++)
            {
                UserControlHelper.AddItemToImageComboBoxEdit(icmbMenuBusinessType, enumItems[index], index - 1);
            }
            enumItems[index].Text = "自定义类型";
            UserControlHelper.AddItemToImageComboBoxEdit(icmbMenuBusinessType, enumItems[index], index - 1);
            //UserControlHelper.InitImageComboBoxEdit(icmbMenuBusinessType, typeof(BusinessMenu));
            UserControlHelper.InitImageComboBoxEdit(icmbMenuIcon, typeof(IconCollection));
            UserControlHelper.InitImageComboBoxEdit(icmbIconType, typeof(IconType));
            IList<CommonNode> commonNodes = MenuHelper.GetCustomBusiness();
            cmbCustomMenuBusinessName.SelectedNode = null;
            cmbCustomMenuBusinessName.TreeViewHandler.InitFullTreeNodes(commonNodes);
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuModule_Load(object sender, EventArgs e)
        {
            if (ParentMenuType > 0 && ParentMenuType <= (byte)BusinessMenu.Max)
            {
                icmbMenuBusinessType.EditValue = (byte)ParentMenuType;
            }
        }

        /// <summary>
        /// 帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkEnableHelp_CheckedChanged(object sender, EventArgs e)
        {
            lblGuidanceTip.Visible = chkEnableHelp.Checked;
        }

        /// <summary>
        /// 设置帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleHelp_OpenLink(object sender, OpenLinkEventArgs e)
        {
            ControlContentHelper.ShowDialog(TreeNodeId, AttachmentCategory.MenuBusiness, "帮助", txtHelpContent, hleHelp);
        }

        /// <summary>
        /// 设置自定义业务的可见性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbMenuType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BusinessMenu menuBusinessType = (BusinessMenu)Convert.ToByte(icmbMenuBusinessType.EditValue);
            if (menuBusinessType == BusinessMenu.Max)
            {
                cmbCustomMenuBusinessName.Visible = true;
                btxtAssociatedBusiness.Visible = false;
            }
            else
            {
                cmbCustomMenuBusinessName.Visible = false;
                btxtAssociatedBusiness.Visible = true;
            }
        }        

        /// <summary>
        /// 关联业务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btxtAssociatedBusiness_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            BusinessMenu menuBusinessType = (BusinessMenu)Convert.ToByte(icmbMenuBusinessType.EditValue);
            switch (menuBusinessType)
            {
                case BusinessMenu.MyWork:
                case BusinessMenu.CommonBusiness:
                    WorkflowItemsForm frmWorkflowItems = new WorkflowItemsForm();
                    frmWorkflowItems.ToolTip = "提示：只能选择工作流节点。";
                    frmWorkflowItems.NodeSelected = delegate (CommonNode node)
                    {
                        if (node != null)
                        {
                            btxtAssociatedBusiness.Text = CustomWorkflowContract.GetFullName(node.NodeId);
                            btxtAssociatedBusiness.Tag = node.NodeId;
                        }
                        else
                        {
                            btxtAssociatedBusiness.Text = string.Empty;
                            btxtAssociatedBusiness.Tag = null;
                        }
                    };
                    frmWorkflowItems.ShowDialog();
                    break;

                case BusinessMenu.Auditing:
                case BusinessMenu.UserData:
                    DataFillItemsForm frmDataFillItems = new DataFillItemsForm();
                    frmDataFillItems.ToolTip = "提示：只能选择数据填报节点。";
                    frmDataFillItems.NodeSelected = delegate (CommonNode node)
                    {
                        if (node != null)
                        {
                            btxtAssociatedBusiness.Text = CustomDataContract.GetFullName(node.NodeId);
                            btxtAssociatedBusiness.Tag = node.NodeId;
                        }
                        else
                        {
                            btxtAssociatedBusiness.Text = string.Empty;
                            btxtAssociatedBusiness.Tag = null;
                        }
                    };
                    frmDataFillItems.ShowDialog();
                    break;

                case BusinessMenu.PersonalData:
                case BusinessMenu.DataAuditing:
                    ExtendedTreeSelectedItemsForm frmExtendedTreeSelectedItems = new ExtendedTreeSelectedItemsForm();
                    frmExtendedTreeSelectedItems.Text = "信息审核业务选择";
                    frmExtendedTreeSelectedItems.ToolTip = "提示：只能选择信息审核业务节点。";
                    frmExtendedTreeSelectedItems.TreeDropdownHandler = new CommonTreeDropdownList(CustomGroupContract, DataAuditingContract, (byte)GroupType.InfoAudited);
                    frmExtendedTreeSelectedItems.NodeSelected = delegate (CommonNode node)
                    {
                        if (node != null)
                        {
                            btxtAssociatedBusiness.Text = DataAuditingContract.GetFullName(node.NodeId);
                            btxtAssociatedBusiness.Tag = node.NodeId;
                        }
                        else
                        {
                            btxtAssociatedBusiness.Text = string.Empty;
                            btxtAssociatedBusiness.Tag = null;
                        }
                    };
                    frmExtendedTreeSelectedItems.ShowDialog();
                    break;

                case BusinessMenu.DataQuery:
                    QueryItemsForm frmQueryItems = new QueryItemsForm();
                    frmQueryItems.ToolTip = "提示：只能选择查询节点。";
                    frmQueryItems.NodeSelected = delegate (CommonNode node)
                    {
                        if (node != null)
                        {
                            btxtAssociatedBusiness.Text = CustomQueyContract.GetFullName(node.NodeId);
                            btxtAssociatedBusiness.Tag = node.NodeId;
                        }
                        else
                        {
                            btxtAssociatedBusiness.Text = string.Empty;
                            btxtAssociatedBusiness.Tag = null;
                        }
                    };
                    frmQueryItems.ShowDialog();
                    break;

                case BusinessMenu.Report:
                    ReportItemsForm frmReportItems = new ReportItemsForm();
                    frmReportItems.ToolTip = "提示：只能选择报表节点。";
                    frmReportItems.ReportCategory = ReportCategory.Query;
                    frmReportItems.NodeSelected = delegate (CommonNode node)
                    {
                        if (node != null)
                        {
                            btxtAssociatedBusiness.Text = CustomReportContract.GetFullName(node.NodeId);
                            btxtAssociatedBusiness.Tag = node.NodeId;
                        }
                        else
                        {
                            btxtAssociatedBusiness.Text = string.Empty;
                            btxtAssociatedBusiness.Tag = null;
                        }
                    };
                    frmReportItems.ShowDialog();
                    break;

                default:
                    throw new ArgumentException("不支持该菜单类型");
            }
        }

        /// <summary>
        /// 图标类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbIconType_SelectedIndexChanged(object sender, EventArgs e)
        {
            IconType iconType = (IconType)Convert.ToByte(icmbIconType.EditValue);
            switch (iconType)
            {
                case IconType.System:
                    icmbMenuIcon.Visible = true;
                    devExpressUploadFile.Visible = false;
                    break;

                case IconType.Custom:
                    icmbMenuIcon.Visible = false;
                    devExpressUploadFile.Visible = true;
                    break;
            }
        }

        /// <summary>
        /// 查看图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void devExpressUploadFile_OnViewLinkClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(devExpressUploadFile.FileName) && (devExpressUploadFile.CustomData == null))
            {
                byte[] data = CustomBusinessContract.DownLoadIcons(devExpressUploadFile.FileName);
                devExpressUploadFile.CustomData = data;
            }
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
                txtBusinessCode.Text = value;
            }
            get
            {
                return txtBusinessCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtBusinessName.ReadOnly = readOnly;            
            if (ParentMenuType > 0 && ParentMenuType <= (byte)BusinessMenu.Max)
            {
                icmbMenuBusinessType.ReadOnly = true;
            }
            else
            {
                icmbMenuBusinessType.ReadOnly = readOnly;
            }
            btxtAssociatedBusiness.Properties.ReadOnly = readOnly;            
            txtBusinessURL.ReadOnly = readOnly;
            txtBusinessIntro.ReadOnly = readOnly;
            chkEnableHelp.ReadOnly = readOnly;
            hleHelp.Enabled = !readOnly;
            cmbCustomMenuBusinessName.ReadOnly = readOnly;
            icmbIconType.ReadOnly = readOnly;
            icmbMenuIcon.ReadOnly = readOnly;
            devExpressUploadFile.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!txtBusinessName.ReadOnly)
            {
                txtBusinessName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomBusinessInfo customBusinessInfo = CustomBusinessContract.GetModelInfo(commonNode.NodeId);
            if (customBusinessInfo != null)
            {
                txtBusinessName.Text = customBusinessInfo.BusinessName;
                txtBusinessCode.Text = customBusinessInfo.BusinessCode;
                txtBusinessIntro.Text = customBusinessInfo.BusinessIntro;
                icmbMenuBusinessType.EditValue = customBusinessInfo.BusinessMenu;

                BusinessMenu businessMenu = (BusinessMenu)customBusinessInfo.BusinessMenu;
                switch (businessMenu)
                {
                    case BusinessMenu.Auditing:
                    case BusinessMenu.UserData:
                        btxtAssociatedBusiness.Text = CustomDataContract.GetFullName(customBusinessInfo.DataId);
                        btxtAssociatedBusiness.Tag = customBusinessInfo.DataId;
                        break;

                    case BusinessMenu.PersonalData:
                    case BusinessMenu.DataAuditing:
                        btxtAssociatedBusiness.Text = DataAuditingContract.GetFullName(customBusinessInfo.DataAuditingId);
                        btxtAssociatedBusiness.Tag = customBusinessInfo.DataAuditingId;
                        break;

                    case BusinessMenu.DataQuery:
                        btxtAssociatedBusiness.Text = CustomQueyContract.GetFullName(customBusinessInfo.DataQueriedId);
                        btxtAssociatedBusiness.Tag = customBusinessInfo.DataQueriedId;
                        break;

                    case BusinessMenu.MyWork:
                    case BusinessMenu.CommonBusiness:
                        btxtAssociatedBusiness.Text = CustomWorkflowContract.GetFullName(customBusinessInfo.WorkflowId);
                        btxtAssociatedBusiness.Tag = customBusinessInfo.WorkflowId;
                        break;

                    case BusinessMenu.Report:
                        btxtAssociatedBusiness.Text = CustomReportContract.GetFullName(customBusinessInfo.ReportId);
                        btxtAssociatedBusiness.Tag = customBusinessInfo.ReportId;
                        break;

                    case BusinessMenu.Max:
                        cmbCustomMenuBusinessName.SelectedNodeId = customBusinessInfo.CustomBusinessName;
                        break;
                }                
                icmbIconType.EditValue = customBusinessInfo.IconType;
                IconType iconType = (IconType)Convert.ToByte(customBusinessInfo.IconType);
                switch (iconType)
                {
                    case IconType.System:
                        icmbMenuIcon.EditValue = customBusinessInfo.BusinessIcon;
                        icmbMenuIcon.Visible = true;
                        devExpressUploadFile.FileName = string.Empty;
                        devExpressUploadFile.Visible = false;
                        break;

                    case IconType.Custom:
                        icmbMenuIcon.SelectedIndex = 0;
                        icmbMenuIcon.Visible = false;
                        devExpressUploadFile.FileName = customBusinessInfo.IconName;
                        devExpressUploadFile.Visible = true;
                        break;
                }
                devExpressUploadFile.CustomData = null;
                txtBusinessURL.Text = customBusinessInfo.BusinessURL; 
                ControlContentHelper.SetAttachments(chkEnableHelp, txtHelpContent, hleHelp, customBusinessInfo.EnableHelp, customBusinessInfo.HelpContent);
                txtNotes.Text = customBusinessInfo.Notes;
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
            txtBusinessName.Text = string.Empty;
            txtBusinessCode.Text = string.Empty;
            if (ParentMenuType > 0 && ParentMenuType <= (byte)BusinessMenu.Max)
            {
                icmbMenuBusinessType.EditValue = (byte)ParentMenuType;
            }
            else
            {
                icmbMenuBusinessType.SelectedIndex = 0;
            }
            cmbCustomMenuBusinessName.SelectedNode = null;
            txtBusinessURL.Text = string.Empty;
            txtBusinessIntro.Text = string.Empty;
            btxtAssociatedBusiness.Text = string.Empty;
            btxtAssociatedBusiness.Tag = null;
            chkEnableHelp.Checked = false;
            txtHelpContent.Text = string.Empty;
            txtHelpContent.Tag = null;
            hleHelp.Tag = null;
            icmbIconType.SelectedIndex = 0;
            icmbMenuIcon.SelectedIndex = 0;
            devExpressUploadFile.FileName = string.Empty;
            devExpressUploadFile.CustomData = null;
            txtNotes.Text = string.Empty;
            if (!txtBusinessName.ReadOnly)
            {
                txtBusinessName.Focus();
            }
        }

        /// <summary>
        /// 校验工作流步骤对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;
            warning = string.Empty;

            BusinessMenu menuType = (BusinessMenu)Convert.ToByte(icmbMenuBusinessType.EditValue);
            if ((menuType == BusinessMenu.Max) && (cmbCustomMenuBusinessName.SelectedNode == null || cmbCustomMenuBusinessName.SelectedNode.NodeId <= 0))
            {
                result = false;
                warning = "请选择自定义菜单。";
            }
            else
            {
                if (btxtAssociatedBusiness.Tag == null)
                {
                    result = false;
                    warning = "请关联业务。";
                }
            }
            if (result)
            {
                IconType iconType = (IconType)Convert.ToByte(icmbIconType.EditValue);
                if (iconType == IconType.Custom)
                {
                    if (!string.IsNullOrWhiteSpace(devExpressUploadFile.FileName))
                    {
                        result = FileFormatHelper.VerfiyPNGFormat(devExpressUploadFile.FileName);
                        if (!result)
                        {
                            warning = "图片格式只能为：PNG。";
                        }
                    }
                    else
                    {
                        result = false;
                        warning = "请选择需要上传的菜单图标";
                    }
                }
                if (result)
                {
                    CustomBusinessInfo customBusinessInfo = GetModelInfo();
                    result = ValidationHelper.Validate<CustomBusinessInfo>(customBusinessInfo, out warning);
                    if (result && chkEnableHelp.Checked && txtHelpContent.Tag == null)
                    {
                        result = false;
                        warning = "帮助内容未设置。";
                    }
                }
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取业务的信息
        /// </summary>
        /// <returns></returns>
        public CustomBusinessInfo GetModelInfo()
        {
            byte customMenuName = 0;
            byte businessIcon = 0;            
            decimal reportId = decimal.MinValue;
            decimal dataAuditingId = decimal.MinValue;
            decimal dataId = decimal.MinValue;
            decimal dataQueriedId = decimal.MinValue;
            decimal workflowId = decimal.MinValue;
            byte businessTypeValue = Convert.ToByte(icmbMenuBusinessType.EditValue);
            BusinessMenu businessMenu = (BusinessMenu)businessTypeValue;
            switch (businessMenu)
            {
                case BusinessMenu.Auditing:
                case BusinessMenu.UserData:
                    dataId = Convert.ToDecimal(btxtAssociatedBusiness.Tag);
                    break;

                case BusinessMenu.PersonalData:
                case BusinessMenu.DataAuditing:
                    dataAuditingId = Convert.ToDecimal(btxtAssociatedBusiness.Tag);
                    break;

                case BusinessMenu.DataQuery:
                    dataQueriedId = Convert.ToDecimal(btxtAssociatedBusiness.Tag);
                    break;

                case BusinessMenu.MyWork:
                case BusinessMenu.CommonBusiness:
                    workflowId = Convert.ToDecimal(btxtAssociatedBusiness.Tag);
                    break;

                case BusinessMenu.Report:
                    reportId = Convert.ToDecimal(btxtAssociatedBusiness.Tag);
                    break;

                case BusinessMenu.Max:
                    customMenuName = Convert.ToByte(cmbCustomMenuBusinessName.SelectedNode.NodeId);
                    break;
            }
            string fileName = string.Empty;
            byte iconTypeValue = Convert.ToByte(icmbIconType.EditValue);
            IconType iconType = (IconType)iconTypeValue;
            switch (iconType)
            {
                case IconType.System:
                    businessIcon = Convert.ToByte(icmbMenuIcon.EditValue);
                    break;

                case IconType.Custom:
                    fileName = Path.GetFileName(devExpressUploadFile.FileName);
                    break;
            }           
            CustomBusinessInfo customBusinessInfo = new CustomBusinessInfo()
            {
                BusinessId = TreeNodeId,
                ReportId = reportId,
                DataAuditingId = dataAuditingId,
                DataId = dataId,
                DataQueriedId = dataQueriedId,
                WorkflowId = workflowId,
                BusinessName = txtBusinessName.Text.Trim(),
                BusinessCode = txtBusinessCode.Text.Trim(),
                BusinessMenu = businessTypeValue,
                CustomBusinessName = customMenuName,
                IconType = iconTypeValue,
                BusinessIcon = businessIcon,
                IconName = fileName,
                BusinessURL = txtBusinessURL.Text.Trim(),
                BusinessIntro = txtBusinessIntro.Text.Trim(),
                EnableHelp = chkEnableHelp.Checked,
                HelpContent = DataConvertionHelper.GetString(txtHelpContent.Tag),
                Notes = txtNotes.Text.Trim()
            };

            return customBusinessInfo;
        }

        /// <summary>
        /// 获得附件
        /// </summary>
        /// <returns></returns>
        public IList<ExtendedUpLoadFileInfo> GetAttachements()
        {
            IList<ExtendedUpLoadFileInfo> upLoadFileInfos = hleHelp.Tag as IList<ExtendedUpLoadFileInfo>;

            return upLoadFileInfos;
        }

        /// <summary>
        /// 获得图片数据
        /// </summary>
        /// <returns></returns>
        public byte[] GetIconData()
        {
            byte[] imageData = null;

            if (File.Exists(devExpressUploadFile.FileName))
            {
                imageData = devExpressUploadFile.CustomData;
            }

            return imageData;
        }

        #endregion
    }
}
