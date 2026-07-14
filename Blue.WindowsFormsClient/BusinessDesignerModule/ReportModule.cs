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
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.Model.BusinessDesignerModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class ReportModule : UserControl, ITreeNodeShow
    {
        #region 私有变量
        

        #endregion

        #region 属性

        /// <summary>
        /// 报表契约
        /// </summary>
        public ICustomReportContract CustomReportContract
        {
            get; set;
        }

        /// <summary>
        /// 报表类型
        /// </summary>
        public ReportCategory ReportCategory
        {
            get;
            set;
        }
        
        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReportModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
                        
            UserControlHelper.InitImageComboBoxEdit(icmbDataWarehouse, typeof(DataWarehouse));
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportModule_Load(object sender, EventArgs e)
        {
            switch (ReportCategory)
            {
                case ReportCategory.Query:
                    UserControlHelper.InitImageComboBoxEdit(icmbReportType, typeof(QueryReportType));
                    break;

                case ReportCategory.Input:
                    UserControlHelper.InitImageComboBoxEdit(icmbReportType, typeof(InputReportType));
                    break;
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
                txtReportCode.Text = value;
            }
            get
            {
                return txtReportCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtReportName.ReadOnly = readOnly;
            txtReportCode.ReadOnly = readOnly;
            if (TreeNodeId > 0)
            {
                icmbReportType.ReadOnly = true;
            }
            else
            {
                icmbReportType.ReadOnly = readOnly;
            }
            icmbDataWarehouse.ReadOnly = readOnly;
            txtTooltip.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!txtReportName.ReadOnly)
            {
                txtReportName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomReportInfo customReportInfo = CustomReportContract.GetModelInfo(commonNode.NodeId);
            if (customReportInfo != null)
            {
                txtReportName.Text = customReportInfo.ReportName;                
                txtReportCode.Text = customReportInfo.ReportCode;
                icmbReportType.EditValue = customReportInfo.ReportType;
                icmbDataWarehouse.EditValue = customReportInfo.DataWarehouseId;                
                txtTooltip.Text = customReportInfo.ToolTip;
                txtNotes.Text = customReportInfo.Notes;
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
            txtReportName.Text = string.Empty;            
            txtReportCode.Text = string.Empty;
            icmbReportType.SelectedIndex = 0;
            icmbDataWarehouse.SelectedIndex = 0;
            txtTooltip.Text = string.Empty;
            txtNotes.Text = string.Empty;
            if (!txtReportName.ReadOnly)
            {
                txtReportName.Focus();
            }
            TreeNodeId = 0;
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;

            CustomReportInfo customReportInfo = GetModelInfo();            
            result = ValidationHelper.Validate<CustomReportInfo>(customReportInfo, out warning);
            if (result)
            {
               
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取报表的信息
        /// </summary>
        /// <returns></returns>
        public CustomReportInfo GetModelInfo()
        {

            CustomReportInfo customReportInfo = new CustomReportInfo()
            {
                ReportId = TreeNodeId,
                ReportName = txtReportName.Text.Trim(),
                ReportCode = txtReportCode.Text.Trim(),
                ReportCategory = (byte)ReportCategory,
                ReportType = Convert.ToByte(icmbReportType.EditValue),
                DataWarehouseId = Convert.ToByte(icmbDataWarehouse.EditValue),
                ToolTip = txtTooltip.Text.Trim(),
                Notes = txtNotes.Text.Trim()
            };

            return customReportInfo;
        }        

        #endregion       

    }
}
