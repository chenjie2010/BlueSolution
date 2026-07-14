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
using AppFramework.Reference.CustomLibrary;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;
using Blue.WindowsFormsClient.Common;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class DataFormModule : UserControl, ITreeNodeShow
    {        
        #region 属性

        /// <summary>
        /// 数据填报业务契约
        /// </summary>
        public ICustomFormContract CustomFormContract
        {
            get; set;
        }

        /// <summary>
        /// 数据库表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get;set;
        }

        /// <summary>
        /// 组合表契约
        /// </summary>
        public ICombinedTableContract CombinedTableContract
        {
            get;
            set;
        }

        /// <summary>
        /// 分组契约
        /// </summary>
        public ICustomGroupContract CustomGroupContract
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataFormModule()
        {
            InitializeComponent();
            TreeNodeId = 0;

            UserControlHelper.InitImageComboBoxEdit(icmbTableType, typeof(FormType));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbFormProperty, typeof(FormProperty));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbDataFieldSetting, typeof(SystemDataField));            
            UserControlHelper.InitImageComboBoxEdit(icmbSystemFormType, typeof(SystemFormType));
            if (!DesignMode)
            {
                IList<CommonNode> commonNodes = ShowModeHelper.GetFormShowModes();
                cmbShowMode.SelectedNode = null;
                cmbShowMode.TreeViewHandler.InitFullTreeNodes(commonNodes);
            }
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataFormModule_Load(object sender, EventArgs e)
        {
           
        }

        /// <summary>
        /// 表的类型变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbTableType_SelectedIndexChanged(object sender, EventArgs e)
        {
            FormType formType = (FormType)Convert.ToByte(icmbTableType.EditValue);
            if (formType == FormType.CombinedTable)
            {
                //icmbSystemFormType.Visible = true;
                //txtTableName.Visible = false;
                //hleTableSetting.Visible = false;
                lblTableName.Text = "组合表：";
            }
            else
            {
                //icmbSystemFormType.Visible = false;
                //txtTableName.Visible = true;
                //hleTableSetting.Visible = true;
                lblTableName.Text = "数据表：";
            }
        }

        /// <summary>
        /// 表与视图选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleTableSetting_OpenLink(object sender, OpenLinkEventArgs e)
        {
            try
            {
                FormType tableType = (FormType)Convert.ToByte(icmbTableType.EditValue);
                switch (tableType)
                {
                    case FormType.Table:
                        DataTableItemsForm frmDataTableItems = new DataTableItemsForm();
                        frmDataTableItems.Text = "表选择";
                        frmDataTableItems.ToolTip = "提示：只能选择数据表类型的节点。";
                        frmDataTableItems.NodeSelected = delegate (CommonNode node)
                        {
                            if (node != null)
                            {
                                txtTableName.Text = CustomTableContract.GetFullName(node.NodeId);
                                txtTableName.Tag = node;
                            }
                            else
                            {
                                txtTableName.Text = string.Empty;
                                txtTableName.Tag = null;
                            }
                        };
                        frmDataTableItems.ShowDialog();
                        break;

                    case FormType.CombinedTable:
                        ExtendedTreeSelectedItemsForm frmExtendedTreeSelectedItems = new ExtendedTreeSelectedItemsForm();
                        frmExtendedTreeSelectedItems.Text = "组合表选择";
                        frmExtendedTreeSelectedItems.ToolTip = "提示：只能选择组合表类型的节点。";
                        frmExtendedTreeSelectedItems.TreeDropdownHandler = new CombinedTableDropdownList(CustomGroupContract, CombinedTableContract);
                        frmExtendedTreeSelectedItems.NodeSelected = delegate (CommonNode node)
                        {
                            if (node != null)
                            {
                                txtTableName.Text = CombinedTableContract.GetFullName(node.NodeId);
                                txtTableName.Tag = node;
                            }
                            else
                            {
                                txtTableName.Text = string.Empty;
                                txtTableName.Tag = null;
                            }
                        };
                        frmExtendedTreeSelectedItems.ShowDialog();                        
                        break;

                    default:
                        throw new ArgumentException("不支持该表格类型。");
                }
            }
            catch (Exception exception)
            {
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 设置帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hleHelpContent_OpenLink(object sender, OpenLinkEventArgs e)
        {
            TextForm frmText = new TextForm();
            frmText.Text = "数据填报指南内容";
            frmText.AttachmentCategory = AttachmentCategory.DataFilled;
            if (txtHelpContent.Tag != null)
            {
                frmText.HtmlText = DataConvertionHelper.GetString(txtHelpContent.Tag);
            }
            else
            {
                frmText.HtmlText = string.Empty;
            }
            frmText.TextId = TreeNodeId;
            frmText.GetRichTextAndAttachments = delegate (string richText, IList<ExtendedUpLoadFileInfo> upLoadFileInfos)
            {
                txtHelpContent.Text = "[已设置]";
                txtHelpContent.Tag = richText;
                hleHelpContent.Tag = upLoadFileInfos;
            };
            frmText.ShowDialog();
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
                txtFormCode.Text = value;
            }
            get
            {
                return txtFormCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtFormName.ReadOnly = readOnly;
            txtFormCode.ReadOnly = readOnly;
            icmbTableType.ReadOnly = readOnly;
            hleTableSetting.Enabled = !readOnly;
            chkBusinessEnabled.ReadOnly = readOnly;
            ccmbFormProperty.ReadOnly = readOnly;
            cmbShowMode.ReadOnly = readOnly;
            icmbSystemFormType.ReadOnly = readOnly;
            ccmbDataFieldSetting.ReadOnly = readOnly;            
            chkEnableHelp.ReadOnly = readOnly;
            hleHelpContent.Enabled = !readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!txtFormName.ReadOnly)
            {
                txtFormName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomFormInfo customFormInfo = CustomFormContract.GetModelInfo(commonNode.NodeId);
            if (customFormInfo != null)
            {
                txtFormName.Text = customFormInfo.FormName;
                txtFormCode.Text = customFormInfo.FormCode;
                icmbTableType.EditValue = customFormInfo.FormType;
                FormType tableType = (FormType)customFormInfo.FormType;
                switch (tableType)
                {
                    case FormType.Table:
                        txtTableName.Text = CustomTableContract.GetFullName(customFormInfo.TableId);
                        txtTableName.Tag = CustomTableContract.GetCommonNode(customFormInfo.TableId);
                        break;

                    case FormType.CombinedTable:
                        txtTableName.Text = CombinedTableContract.GetFullName(customFormInfo.CombinedTableId);
                        txtTableName.Tag = CombinedTableContract.GetCommonNode(customFormInfo.CombinedTableId);
                        break;

                    //case FormType.SystemTable:
                    //    icmbSystemFormType.EditValue = customFormInfo.SystemFormType;
                    //    break;

                    default:
                        throw new ArgumentException("不支持该表格类型。");
                }
                chkBusinessEnabled.Checked = customFormInfo.BusinessEnabled;
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbFormProperty, customFormInfo.FormProperty);
                cmbShowMode.SelectedNodeId = customFormInfo.ShowMode;
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbDataFieldSetting, customFormInfo.DataFieldSetting);
                chkEnableHelp.Checked = customFormInfo.EnableHelp;
                if (!string.IsNullOrWhiteSpace(customFormInfo.HelpContent))
                {
                    txtHelpContent.Text = "[已设置]";
                }
                txtHelpContent.Tag = customFormInfo.HelpContent;
                /* null 表示帮助内容的附件不变化 */
                hleHelpContent.Tag = null;
                txtNotes.Text = customFormInfo.Notes;                
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
            txtFormName.Text = string.Empty;
            txtFormCode.Text = string.Empty;
            txtTableName.Text = string.Empty;
            txtTableName.Tag = null;
            chkBusinessEnabled.Checked = false;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbFormProperty);
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbDataFieldSetting);
            cmbShowMode.SelectedNode = null;
            icmbTableType.SelectedIndex = 0;
            icmbSystemFormType.SelectedIndex = 0;
            chkEnableHelp.Checked = false;
            txtHelpContent.Text = string.Empty;
            txtHelpContent.Tag = null;
            hleHelpContent.Tag = null;
            txtNotes.Text = string.Empty;
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;

            if (cmbShowMode.SelectedNode == null)
            {
                warning = "展现模式未选择。";
                return false;
            }
            CustomFormInfo customFormInfo = GetModelInfo();
            result = ValidationHelper.Validate<CustomFormInfo>(customFormInfo, out warning);
            if (result)
            {
                FormType tableType = (FormType)Convert.ToByte(icmbTableType.EditValue);
                switch (tableType)
                {
                    case FormType.Table:
                    case FormType.CombinedTable:
                        CommonNode commonNode = txtTableName.Tag as CommonNode;
                        if (commonNode == null)
                        {
                            result = false;
                            if (tableType == FormType.Table)
                            {
                                warning = "数据表未设置。";
                            }
                            else
                            {
                                warning = "组合表未设置。";
                            }
                        }
                        break;
                }

                if (chkEnableHelp.Checked && txtHelpContent.Tag == null)
                {
                    result = false;
                    warning = "帮助内容未设置。";
                }                
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取表格的信息
        /// </summary>
        /// <returns></returns>
        public CustomFormInfo GetModelInfo()
        {
            decimal tableId = decimal.MinValue;
            decimal combinedTableId = decimal.MinValue;
            byte systemFormType = 0;            

            if (chkEnableHelp.Checked && txtHelpContent.Tag == null)
            {
                throw new ArgumentNullException("帮助内容未设置。");
            }            
            FormType tableType = (FormType)Convert.ToByte(icmbTableType.EditValue);
            switch (tableType)
            {
                case FormType.Table:                    
                    CommonNode commonNode = txtTableName.Tag as CommonNode;
                    tableId = commonNode.NodeId;
                    if (commonNode == null)
                    {
                        throw new ArgumentNullException("表未设置。");
                    }
                    break;

                case FormType.CombinedTable:
                    CommonNode node = txtTableName.Tag as CommonNode;
                    combinedTableId = node.NodeId;                    
                    if (node == null)
                    {
                        throw new ArgumentNullException("组合表未设置。");
                    }
                    break;

                //case FormType.SystemTable:
                //    if (icmbSystemFormType.SelectedIndex == 0)
                //    {
                //        throw new ArgumentNullException("请选择系统表。");
                //    }
                //    systemFormType = Convert.ToByte(icmbSystemFormType.EditValue);
                //    break;

                default:
                    throw new ArgumentException("不支持该表格类型。");
            }

            CustomFormInfo customFormInfo = new CustomFormInfo()
            {
                FormId = TreeNodeId,
                FormName = txtFormName.Text.Trim(),
                FormCode = txtFormCode.Text.Trim(),
                TableId = tableId,
                CombinedTableId = combinedTableId,
                FormType = Convert.ToByte(tableType), 
                SystemFormType = systemFormType,
                BusinessEnabled = chkBusinessEnabled.Checked,
                FormProperty = UserControlHelper.GetCheckedComboBoxEditItems(ccmbFormProperty),
                ShowMode = Convert.ToByte(cmbShowMode.SelectedNode.NodeId),
                DataFieldSetting = UserControlHelper.GetCheckedComboBoxEditItems(ccmbDataFieldSetting),               
                EnableHelp = chkEnableHelp.Checked,
                HelpContent = DataConvertionHelper.GetString(txtHelpContent.Tag),
                Notes = txtNotes.Text.Trim()
            };

            return customFormInfo;
        }

        /// <summary>
        /// 获得附件
        /// </summary>
        /// <returns></returns>
        public IList<ExtendedUpLoadFileInfo> GetAttachements()
        {
            IList<ExtendedUpLoadFileInfo> upLoadFileInfos = hleHelpContent.Tag as IList<ExtendedUpLoadFileInfo>;

            return upLoadFileInfos;
        }

        #endregion

    }
}
