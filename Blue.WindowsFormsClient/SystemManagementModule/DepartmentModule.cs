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
using AppFramework.WinFormsLibrary.Common;
using Blue.WCFContracts.SystemModule;
using Blue.Model.SystemModule;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    /// <summary>
    /// 单位管理模块
    /// </summary>
    public partial class DepartmentModule : UserControl, ITreeNodeShow
    {
        #region 属性

        /// <summary>
        /// 单位契约
        /// </summary>
        public ICustomDepartmentContract CustomDepartmentContract
        {
            set; get;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string LayerName
        {
            set
            {
                lblName.Text = value;
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        public string LayerCode
        {
            set
            {
                lblCode.Text = value;
            }
        }

        /// <summary>
        /// 属性
        /// </summary>
        public string DepartmentProperty
        {
            set
            {
                lblDepartmentPorperty.Text = value;
            }
        }

        /// <summary>
        /// 单位编码
        /// </summary>
        public string DepCode
        {
            set
            {
                txtCode.Text = value;
            }
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public DepartmentModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            SetActiveStatesOfControls(true);
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DepartmentModule_Load(object sender, EventArgs e)
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
                txtCode.Text = value;
            }
            get
            {
                return txtCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtName.ReadOnly = readOnly;
            txtDepValue.ReadOnly = readOnly;
            txtAddFstCode.ReadOnly = readOnly;
            txtAddScdCode.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            chkIsSystem.ReadOnly = readOnly;
            chkVisibleForInterface.ReadOnly = readOnly;
            icmbDepartmentPorperty.Properties.ReadOnly = readOnly;
            if (!readOnly)
            {
                txtName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomDepartmentInfo customDepartmentInfo = CustomDepartmentContract.GetModelInfo(commonNode.NodeId);
            if (customDepartmentInfo != null)
            {
                txtName.Text = customDepartmentInfo.DepName;
                txtCode.Text = customDepartmentInfo.DepCode;
                txtDepValue.Text = customDepartmentInfo.DepValue;
                txtAddFstCode.Text = customDepartmentInfo.FirstCode;
                txtAddScdCode.Text = customDepartmentInfo.SecondCode;
                chkIsSystem.Checked = customDepartmentInfo.IsSystemDepartment;
                chkVisibleForInterface.Checked = customDepartmentInfo.IsVisibleForInterface;
                txtNotes.Text = customDepartmentInfo.Notes;
                icmbDepartmentPorperty.SelectedItem = icmbDepartmentPorperty.Properties.Items.GetItem(customDepartmentInfo.DepartmentProperty);
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
            txtCode.Text = string.Empty;
            txtDepValue.Text = string.Empty;
            txtAddFstCode.Text = string.Empty;
            txtAddScdCode.Text = string.Empty;
            chkIsSystem.Checked = false;
            chkVisibleForInterface.Checked = false;
            txtNotes.Text = string.Empty;
            icmbDepartmentPorperty.SelectedIndex = 0;
            txtName.Focus();
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取单位信息
        /// </summary>
        /// <returns></returns>
        public CustomDepartmentInfo GetModelInfo()
        {
            string depName = txtName.Text.Trim();
            string depCode = txtCode.Text.Trim();
            string depValue = txtDepValue.Text.Trim();
            string addFstCode = txtAddFstCode.Text.Trim();
            string addScdCode = txtAddScdCode.Text.Trim();
            bool isSystem = chkIsSystem.Checked;
            bool isVisibleForInterface = chkVisibleForInterface.Checked;
            string notes = txtNotes.Text.Trim();
            byte departmentPorperty = 0;
            ImageComboBoxItem item = icmbDepartmentPorperty.SelectedItem as ImageComboBoxItem;
            if (item != null)
            {
                departmentPorperty = DataConvertionHelper.GetConvertedByte(item.Value);
            }
            CustomDepartmentInfo customDepartmentInfo = new CustomDepartmentInfo()
            {
                DepId = TreeNodeId,
                DepName = depName,                
                DepCode = depCode,
                DepValue = depValue,
                FirstCode = addFstCode,
                SecondCode = addScdCode,
                Notes = notes,
                IsSystemDepartment = isSystem,
                IsVisibleForInterface = isVisibleForInterface,
                DepartmentProperty = departmentPorperty
            };

            return customDepartmentInfo;
        }

        /// <summary>
        /// 增加属性
        /// </summary>
        /// <param name="downListItems"></param>
        public void AddDepartmentPorperty(IList<EnumItem> enumItems)
        {            
            for (int i = 0; i < enumItems.Count; i++)
            {
                int imageIndex = i % 10;
                ImageComboBoxItem item = new ImageComboBoxItem(enumItems[i].Text, enumItems[i].Value, imageIndex);
                icmbDepartmentPorperty.Properties.Items.Add(item);
            }
            icmbDepartmentPorperty.SelectedIndex = 0;            
        }
        
        #endregion
    }
}
