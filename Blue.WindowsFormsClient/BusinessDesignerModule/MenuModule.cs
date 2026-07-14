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
using AppFramework.WinFormsLibrary;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class MenuModule : UserControl, ITreeNodeShow
    {
        #region 属性       

        /// <summary>
        /// 菜单契约
        /// </summary>
        public ICustomMenuContract CustomMenuContract
        {
            get; set;
        }

        /// <summary>
        /// 名称
        /// </summary>
        public string LayerName
        {
            set
            {
                lblMenuName.Text = value;
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        public string LayerCodeName
        {
            set
            {
                lblMenuCode.Text = value;
            }
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public MenuModule()
        {
            InitializeComponent();
            TreeNodeId = 0;
            UserControlHelper.InitImageComboBoxEdit(icmbIconType, typeof(IconType));
            UserControlHelper.InitImageComboBoxEdit(icmbMenuIcon, typeof(IconCollection));            
            SetActiveStatesOfControls(true);
            devExpressUploadFile.Filter = "PNG(*.PNG)|*.PNG|所有文件(*.*)|*.*";
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuModule_Load(object sender, EventArgs e)
        {

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
                byte[] data = CustomMenuContract.DownLoadIcons(devExpressUploadFile.FileName);
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
                txtMenuCode.Text = value;
            }
            get
            {
                return txtMenuCode.Text.Trim();
            }
        }

        /// <summary>
        /// 设置控件是否处于可读写状态
        /// </summary>
        /// <param name="readOnly"></param>
        public void SetActiveStatesOfControls(bool readOnly)
        {
            txtMenuName.ReadOnly = readOnly;
            txtMenuCode.ReadOnly = readOnly;
            icmbIconType.ReadOnly = readOnly;
            icmbMenuIcon.ReadOnly = readOnly;
            devExpressUploadFile.ReadOnly = readOnly;
            txtMenuURL.ReadOnly = readOnly;
            txtMenuIconName.ReadOnly = readOnly;
            txtToolTip.ReadOnly = readOnly;
            txtNotes.ReadOnly = readOnly;
            if (!readOnly)
            {
                txtMenuName.Focus();
            }
        }

        /// <summary>
        /// 设置界面数据
        /// </summary>
        /// <param name="commonNode"></param>
        public void SetModelInfo(CommonNode commonNode)
        {
            if (commonNode != null)
            {
                TreeNodeId = commonNode.NodeId;
                CustomMenuInfo customMenuInfo = CustomMenuContract.GetModelInfo(commonNode.NodeId);
                txtMenuName.Text = customMenuInfo.MenuName;
                icmbIconType.EditValue = customMenuInfo.IconType;
                IconType iconType = (IconType)Convert.ToByte(customMenuInfo.IconType);
                switch (iconType)
                {
                    case IconType.System:
                        icmbMenuIcon.EditValue = customMenuInfo.MenuIcon;
                        icmbMenuIcon.Visible = true;
                        devExpressUploadFile.FileName = string.Empty;
                        devExpressUploadFile.Visible = false;
                        break;

                    case IconType.Custom:
                        icmbMenuIcon.SelectedIndex = 0;
                        icmbMenuIcon.Visible = false;
                        devExpressUploadFile.FileName = customMenuInfo.IconName;
                        devExpressUploadFile.Visible = true;
                        break;
                }
                devExpressUploadFile.CustomData = null;
                txtMenuURL.Text = customMenuInfo.MenuURL;
                txtMenuCode.Text = customMenuInfo.MenuCode;
                txtMenuIconName.Text = customMenuInfo.MenuIconName;
                txtNotes.Text = customMenuInfo.Notes;
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
            txtMenuName.Text = string.Empty;
            txtMenuCode.Text = string.Empty;
            icmbIconType.SelectedIndex = 0;
            icmbMenuIcon.SelectedIndex = 0;
            devExpressUploadFile.FileName = string.Empty;
            devExpressUploadFile.CustomData = null;
            txtMenuURL.Text = string.Empty;
            txtToolTip.Text = string.Empty;
            txtNotes.Text = string.Empty;
            txtMenuIconName.Text = string.Empty;
            if (!txtMenuName.ReadOnly)
            {
                txtMenuName.Focus();
            }
        }

        /// <summary>
        /// 校验工作流步骤对象
        /// </summary>
        public bool ValidateModelInfo(out string warning)
        {
            bool result = true;
            warning = string.Empty;

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
            string menuIconName = txtMenuIconName.Text;
            if (!string.IsNullOrWhiteSpace(menuIconName) && !menuIconName.StartsWith("fa-", StringComparison.CurrentCultureIgnoreCase))
            {
                result = false;
                warning = "图标名称必须以'fa-'开头。";
            }
            if (result)
            {
                CustomMenuInfo customMenuInfo = GetModelInfo();
                result = ValidationHelper.Validate<CustomMenuInfo>(customMenuInfo, out warning);
            }

            return result; 
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 获取用户类型信息
        /// </summary>
        /// <returns></returns>
        public CustomMenuInfo GetModelInfo()
        {
            string name = txtMenuName.Text.Trim();
            string code = txtMenuCode.Text.Trim();
            string menuIconName = txtMenuIconName.Text;
            string tooltip = txtToolTip.Text.Trim();
            string notes = txtNotes.Text.Trim();
            byte menuIcon = 0;
            string fileName = string.Empty;
            byte iconTypeValue = Convert.ToByte(icmbIconType.EditValue);
            IconType iconType = (IconType)iconTypeValue;
            switch (iconType)
            {
                case IconType.System:
                    menuIcon = Convert.ToByte(icmbMenuIcon.EditValue);
                    break;

                case IconType.Custom:
                    fileName = Path.GetFileName(devExpressUploadFile.FileName);
                    if (!string.IsNullOrWhiteSpace(fileName) && fileName.Length > 32)
                    {
                        string extension = Path.GetExtension(fileName);
                        fileName = string.Format("{0}.{1}", fileName.Substring(0, 32 - extension.Length - 2), extension);
                    }
                    break;
            }
            CustomMenuInfo customMenuInfo = new CustomMenuInfo()
            {
                MenuId = TreeNodeId,
                MenuName = name,
                MenuCode = code,
                IconType = iconTypeValue,
                MenuIcon = menuIcon,
                IconName = fileName,
                MenuURL = txtMenuURL.Text.Trim(),
                MenuType = 0,
                MenuIconName = menuIconName,
                Notes = notes
            };

            return customMenuInfo;
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

        /// <summary>
        /// 打开超链接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lnkWebIcon_HyperlinkClick(object sender, DevExpress.Utils.HyperlinkClickEventArgs e)
        {
            DevExpress.XtraEditors.HyperLinkEdit editor  = new DevExpress.XtraEditors.HyperLinkEdit();
            editor.ShowBrowser("http://fontawesome.dashgame.com/");
        }
    }
}
