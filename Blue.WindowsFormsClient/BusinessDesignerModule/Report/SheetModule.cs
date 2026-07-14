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
    public partial class SheetModule : UserControl, ITreeNodeShow
    {
        #region 私有变量
        

        #endregion

        #region 属性

        /// <summary>
        /// 样表契约
        /// </summary>
        public ICustomSheetContract CustomSheetContract
        {
            get; set;
        }
        
        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public SheetModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
        }

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportModule_Load(object sender, EventArgs e)
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
                txtSheetCode.Text = value;
            }
            get
            {
                return txtSheetCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtSheetName.ReadOnly = readOnly;
            txtApprovalNumber.ReadOnly = readOnly;
            txtSheetDescription.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!txtSheetName.ReadOnly)
            {
                txtSheetName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            TreeNodeId = commonNode.NodeId;
            CustomSheetInfo customSheetInfo = CustomSheetContract.GetModelInfo(commonNode.NodeId);
            if (customSheetInfo != null)
            {
                txtSheetName.Text = customSheetInfo.SheetName;                
                txtSheetCode.Text = customSheetInfo.SheetCode;
                txtApprovalNumber.Text = customSheetInfo.ApprovalNumber.ToString();              
                txtSheetDescription.Text = customSheetInfo.SheetDescription;
                txtNotes.Text = customSheetInfo.Notes;
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
            txtSheetName.Text = string.Empty;            
            txtSheetCode.Text = string.Empty;
            txtApprovalNumber.Text = string.Empty;
            txtSheetDescription.Text = string.Empty;
            txtNotes.Text = string.Empty;
            if (!txtSheetName.ReadOnly)
            {
                txtSheetName.Focus();
            }
            TreeNodeId = 0;
        }

        /// <summary>
        /// 校验视图对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;

            CustomSheetInfo customSheetInfo = GetModelInfo();
            result = ValidationHelper.Validate<CustomSheetInfo>(customSheetInfo, out warning);
            if (result)
            {
                string approvalNumber = txtApprovalNumber.Text.Trim();
                if (!string.IsNullOrWhiteSpace(approvalNumber))
                {
                    if (!UserDataHelper.MatchDigit(txtApprovalNumber.Text.Trim()))
                    {
                        result = false;
                        warning = "请在批准编号中输入整数。";
                    }
                }
                else
                {
                    result = false;
                    warning = "批准编号不能为空。";
                }
            }

            return result;
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取样表的信息
        /// </summary>
        /// <returns></returns>
        public CustomSheetInfo GetModelInfo()
        {

            CustomSheetInfo customSheetInfo = new CustomSheetInfo()
            {
                SheetId = TreeNodeId,
                SheetName = txtSheetName.Text.Trim(),
                SheetCode = txtSheetCode.Text.Trim(),
                SheetDescription = txtSheetDescription.Text.Trim(),
                ApprovalNumber = DataConvertionHelper.GetInt(txtApprovalNumber.Text.Trim(), 0),
                Notes = txtNotes.Text.Trim()
            };

            return customSheetInfo;
        }        

        #endregion       

    }
}
