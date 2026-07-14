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
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class AssociatedDataFieldModule : UserControl, ITreeNodeShow
    {
        #region 属性

        /// <summary>
        /// 关联字段契约
        /// </summary>
        public IAssociatedDataFieldContract AssociatedDataFieldContract
        {
            set; get;
        }

        /// <summary>
        /// 字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            set; get;
        }


        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public AssociatedDataFieldModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbBasedDatType, typeof(BasedDataType));
            UserControlHelper.InitImageComboBoxEdit(icmbDataFieldCategory, typeof(AssociatedDataFieldCategory));            
            SetActiveStatesOfControls(true);
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AssociatedDataFieldModule_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 查看与关联字段相关联的字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDataFieldList_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
        }

        /// <summary>
        /// 查看与关联字段相关联的字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkDataFieldList_OpenLink(object sender, OpenLinkEventArgs e)
        {
            DataSet ds = CustomDataFieldContract.GetDataFieldsConnected(TreeNodeId);
            DataTableModeForm frmDataTableMode = new DataTableModeForm();
            frmDataTableMode.DataSource = ds;
            frmDataTableMode.ShowDialog();
        }

        //基础字段类型变化
        private void icmbBasedDatType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ImageComboBoxItem imageComboBoxItem = icmbBasedDatType.SelectedItem as ImageComboBoxItem;
            BasedDataType dataFieldBase = (BasedDataType)Convert.ToByte(imageComboBoxItem.Value);
            if (dataFieldBase == BasedDataType.String)
            {
                txtMaxLength.Visible = true;
            }
            else
            {
                txtMaxLength.Visible = false;
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
                txtDataFieldCode.Text = value;
            }
            get
            {
                return txtDataFieldCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtName.ReadOnly = readOnly;            
            txtDataFieldCode.ReadOnly = readOnly;
            icmbBasedDatType.ReadOnly = readOnly;
            txtMaxLength.ReadOnly = readOnly;
            icmbDataFieldCategory.ReadOnly = readOnly;
            ceIsHierarchal.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!readOnly)
            {
                txtName.Focus();
            }
            lnkDataFieldList.Enabled = readOnly;
        }

        /// <summary>
        /// 设置枚举信息
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            if (commonNode != null)
            {
                TreeNodeId = commonNode.NodeId;
                AssociatedDataFieldInfo associatedDataFieldInfo = AssociatedDataFieldContract.GetModelInfo(commonNode.NodeId);
                if (associatedDataFieldInfo != null)
                {
                    txtName.Text = associatedDataFieldInfo.LogicalName;
                    txtPhysicalName.Text = associatedDataFieldInfo.PhysicalName;
                    txtDataFieldCode.Text = associatedDataFieldInfo.DataFieldCode;
                    //icmbBasedDatType.SelectedItem = icmbBasedDatType.Properties.Items.GetItem(associatedDataFieldInfo.BasedDataType);
                    icmbBasedDatType.EditValue = associatedDataFieldInfo.BasedDataType;
                    icmbDataFieldCategory.EditValue = associatedDataFieldInfo.DataFieldCategory;
                    ceIsHierarchal.Checked = associatedDataFieldInfo.IsHierarchal;
                    txtNotes.Text = associatedDataFieldInfo.Notes;
                    BasedDataType dataFieldBase = (BasedDataType)associatedDataFieldInfo.BasedDataType;
                    if (dataFieldBase == BasedDataType.String)
                    {
                        txtMaxLength.Text = associatedDataFieldInfo.DataLength.ToString();
                    }
                    else
                    {
                        txtMaxLength.Text = string.Empty;
                    }

                }
                else
                {
                    txtName.Text = commonNode.NodeName;
                    txtPhysicalName.Text = commonNode.NodeCode;
                    txtNotes.Text = string.Empty;
                }
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
            txtName.Text = string.Empty;
            txtPhysicalName.Text = string.Empty;
            txtDataFieldCode.Text = string.Empty;
            icmbBasedDatType.SelectedIndex = 0;
            txtMaxLength.Text = string.Empty;
            icmbDataFieldCategory.SelectedIndex = 0;
            ceIsHierarchal.Checked = false;
            txtNotes.Text = string.Empty;
            ClearTextOfDataFieldList();
            txtName.Focus();
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取关联字段信息
        /// </summary>
        /// <returns></returns>
        public AssociatedDataFieldInfo GetModelInfo()
        {
            string logicalName = txtName.Text.Trim();
            string dataFieldCode = txtDataFieldCode.Text.Trim();
            byte basedDatType = Convert.ToByte(icmbBasedDatType.EditValue);
            byte dataFieldCategory = Convert.ToByte(icmbDataFieldCategory.EditValue);
            int maxLength = 0;
            BasedDataType dataFieldBase = (BasedDataType)basedDatType;
            if (dataFieldBase == BasedDataType.String)
            {
                maxLength = DataConvertionHelper.GetConvertedInt(txtMaxLength.Text.Trim(), Int32.MinValue);
            }            
            string notes = txtNotes.Text.Trim();
            AssociatedDataFieldInfo asociatedDataFieldInfo = new AssociatedDataFieldInfo()
            {
                AssociatedDataFieldId = TreeNodeId,
                LogicalName = logicalName,
                DataFieldCode = dataFieldCode,
                BasedDataType = basedDatType,
                DataFieldCategory = dataFieldCategory,
                IsHierarchal = ceIsHierarchal.Checked,
                DataLength = maxLength,
                Notes = notes
            };

            return asociatedDataFieldInfo;
        }

        /// <summary>
        /// 初始化显示关联字段
        /// </summary>
        public void ClearTextOfDataFieldList()
        {
            lnkDataFieldList.Text = "该关联字段暂未与字段关联";
        }

        /// <summary>
        /// 初始化显示关联字段
        /// </summary>
        /// <param name="count"></param>
        public void SeTextOfDataFieldList(int count)
        {
            lnkDataFieldList.Text = string.Format("该关联字段与{0}个字段关联", count);
        }

        #endregion       
    }
}
