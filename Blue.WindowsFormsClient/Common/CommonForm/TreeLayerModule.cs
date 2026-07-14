using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary.Common;
using Blue.CustomLibrary;
using Blue.WCFContracts.SystemModule;
using Blue.Model.SystemModule;
using Blue.WindowsFormsClient;

namespace Blue.WindowsFormsClient.Common
{
    public partial class TreeLayerModule : UserControl, ITreeNodeShow
    {
        #region 属性

        /// <summary>
        /// 节点操作接口契约
        /// </summary>
        public ICommonNodeContract CommonNodeContract
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
                lblName.Text = value;
            }
        }

        /// <summary>
        /// 编码
        /// </summary>
        public string LayerCodeName
        {
            set
            {
                lblCode.Text = value;
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string NotesName
        {
            set
            {
                lblNotes.Text = value;
            }
        }
        
        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeLayerModule()
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
        private void TreeLayerModule_Load(object sender, EventArgs e)
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
            txtNotes.ReadOnly = readOnly;
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
            if (commonNode != null)
            {                
                if (CommonNodeContract != null && commonNode.NodeId > 0)
                {
                    TreeNodeId = commonNode.NodeId;
                    ExtendedCommonNode extendedCommonNode = CommonNodeContract.GetExtendedCommonNode(commonNode.NodeId);
                    txtName.Text = extendedCommonNode.NodeName;
                    txtCode.Text = extendedCommonNode.NodeCode;
                    txtNotes.Text = extendedCommonNode.Notes;
                }
                else
                {
                    txtName.Text = commonNode.NodeName;
                    txtCode.Text = commonNode.NodeCode;
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
            txtCode.Text = string.Empty;
            txtNotes.Text = string.Empty;
            if (!txtName.ReadOnly)
            {
                txtName.Focus();
            }
        }


        #endregion

        #region 公有方法

        /// <summary>
        /// 获得数据内容
        /// </summary>
        /// <returns></returns>
        public ExtendedCommonNode GetModelInfo()
        {
            return new ExtendedCommonNode()
            {
                NodeId = TreeNodeId,
                NodeName = txtName.Text.Trim(),
                NodeCode = txtCode.Text.Trim(),
                Notes = txtNotes.Text.Trim()
            };
        }

        #endregion
    }
}
