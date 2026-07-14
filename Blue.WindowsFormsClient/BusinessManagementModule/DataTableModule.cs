using System;
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
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class DataTableModule : UserControl, ITreeNodeShow
    {
        #region 属性

        /// <summary>
        /// 数据表契约
        /// </summary>
        public ICustomTableContract CustomTableContract
        {
            get;set;
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public DataTableModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbTableProperty, typeof(TableProperty));
            UserControlHelper.InitImageComboBoxEdit(icmbTableType, typeof(DataTableType));
            UserControlHelper.InitCheckedComboBoxEditItems(ccmbTableSetting, typeof(TableSetting));
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableModule_Load(object sender, EventArgs e)
        {
           
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
                txtTableCode.Text = value;
            }
            get
            {
                return txtTableCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtLogicalName.ReadOnly = readOnly;
            txtTableCode.ReadOnly = readOnly;
            icmbTableProperty.ReadOnly = readOnly;
            icmbTableType.ReadOnly = readOnly;
            chkSystemTable.ReadOnly = readOnly;
            ccmbTableSetting.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!txtLogicalName.ReadOnly)
            {
                txtLogicalName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomTableInfo customTableInfo = CustomTableContract.GetModelInfo(commonNode.NodeId);
            if (customTableInfo != null)
            {
                txtLogicalName.Text = customTableInfo.LogicalName;
                txtPhysicalName.Text = customTableInfo.PhysicalName;
                txtTableCode.Text = customTableInfo.TableCode;
                icmbTableProperty.EditValue = customTableInfo.TableProperty;
                icmbTableType.SelectedItem = icmbTableType.Properties.Items.GetItem(customTableInfo.TableType); 
                chkSystemTable.Checked = customTableInfo.SystemTable;
                UserControlHelper.SetCheckedComboBoxEditItems(ccmbTableSetting, customTableInfo.TableSetting);
                txtNotes.Text = customTableInfo.Notes;                
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
            txtLogicalName.Text = string.Empty;
            txtPhysicalName.Text = string.Empty;
            txtTableCode.Text = string.Empty;
            icmbTableType.SelectedIndex = 0;
            chkSystemTable.Checked = false;
            UserControlHelper.CancelAllCheckedComboBoxEditItems(ccmbTableSetting);
            txtNotes.Text = string.Empty;
            if (!txtLogicalName.ReadOnly)
            {
                txtLogicalName.Focus();
            }
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;
            warning = string.Empty;

            //DataTableType dataTableType = (DataTableType)DataConvertionHelper.GetConvertedByte(icmbTablePorperty.EditValue);
            //if ((dataTableType == DataTableType.DependentBusiness || dataTableType == DataTableType.DependentTable) && txtDependency.Tag == null)
            //{
            //    result = false;
            //    warning = "依赖的表未设置。";
            //}
            if (result)
            {
                CustomTableInfo customTableInfo = GetModelInfo();
                result = ValidationHelper.Validate<CustomTableInfo>(customTableInfo, out warning);
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取表的信息
        /// </summary>
        /// <returns></returns>
        public CustomTableInfo GetModelInfo()
        {
            byte tableProperty = DataConvertionHelper.GetConvertedByte(icmbTableProperty.EditValue);
            byte tableType = DataConvertionHelper.GetConvertedByte(icmbTableType.EditValue);
            DataTableType dataTableType = (DataTableType)tableType;      
            CustomTableInfo customTableInfo = new CustomTableInfo()
            {
                TableId = TreeNodeId,
                LogicalName = txtLogicalName.Text.Trim(),
                PhysicalName = txtPhysicalName.Text.Trim(),
                TableCode = txtTableCode.Text.Trim(),
                TableProperty = tableProperty,
                TableType = tableType,
                SystemTable = chkSystemTable.Checked,
                TableSetting = UserControlHelper.GetCheckedComboBoxEditItems(ccmbTableSetting),
                Notes = txtNotes.Text.Trim()
            };

            return customTableInfo;
        }

        #endregion

    }
}
