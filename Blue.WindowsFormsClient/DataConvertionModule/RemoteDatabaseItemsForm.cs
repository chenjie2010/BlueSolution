using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.Utility;
using AppFramework.WinFormsLibrary.EventArgument;
using Blue.WCFContracts;

namespace Blue.WindowsFormsClient.DataConvertionModule
{
    public partial class RemoteDatabaseItemsForm : Form
    {
        #region 属性

        /// <summary>
        /// 选择节点
        /// </summary>
        public NodeSelectedDelegate NodeSelected
        {
            set;
            get;
        }       
        
        /// <summary>
        /// 提示
        /// </summary>
        public string ToolTip
        {
            get
            {
                return gcMain.Text;
            }
            set
            {
                gcMain.Text = value;
            }
        }        

        /// <summary>
        /// 远程服务器契约
        /// </summary>
        public IRemoteServerContract RemoteServerContract
        {
            get;
            set;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get;
            set;
        }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password
        {
            get;
            set;
        }
        #endregion

        #region 控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public RemoteDatabaseItemsForm()
        {
            InitializeComponent();            
        }

        /// <summary>
        /// 加载数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DataTableSelectedItemsForm_Load(object sender, EventArgs e)
        {
            IList<CommonNode> commonNodes = UserEnumHelper.GetCommonNodes(typeof(DataWarehouse));
            foreach (var commonNode in commonNodes)
            {
                commonNode.IsLeaf = false;
            }
            comoboxTreeview.TreeViewHandler.InitFirstLevelNodes(commonNodes);
        }

        /// <summary>
        /// 展开
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comoboxTreeview_AfterTreeNodeExpand(object sender, TreeViewEventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (e.Node != null && e.Node.Nodes.Count == 1)
                {
                    CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                    if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                    {
                        CommonNode commonNode = e.Node.Tag as CommonNode;
                        IList<CommonNode> commonNodes = RemoteServerContract.GetDatabases(UserName, Password, Convert.ToByte(commonNode.NodeId));
                        foreach(var node in commonNodes)
                        {
                            node.IsLeaf = true;
                        }
                        comoboxTreeview.TreeViewHandler.LoadPartialNodes(e.Node, commonNodes);
                    }
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (NodeSelected != null)
            {
                NodeSelected(comoboxTreeview.SelectedNode);
            }
            this.Close();
        }

        /// <summary>
        /// 移除选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void comoboxTreeview_OnNodeRemoved(object sender, EventArgs e)
        {
            if (NodeSelected != null)
            {
                NodeSelected(null);
            }
            this.Close();
        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        #endregion

       
    }
}
