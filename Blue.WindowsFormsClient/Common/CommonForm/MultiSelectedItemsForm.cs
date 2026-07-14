using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using AppFramework.Core;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsControls;

namespace Blue.WindowsFormsClient
{
    public partial class MultiSelectedItemsForm : Form
    {
        #region 私有变量

        private readonly TreeViewHandler<TreeNode> treeViewHandler;
        private bool loading = true;

        #endregion

        #region 内部成员变量

        private GetTreeNodeListDelegate _getTreeNodeListDelegate;

        #endregion

        #region 属性

        /// <summary>
        /// 操作
        /// </summary>
        public IMultiSelectedItemsHandler MultiSelectedItemsHandler
        {
            set;
            get;
        }

        /// <summary>
        /// 是否自动选择下一级节点
        /// </summary>
        public bool AutoSelectedNext
        {
            get;
            set;
        }

        /// <summary>
        /// 是否允许选择非叶子节点
        /// </summary>
        public bool OnlyLeafSelected
        {
            get;
            set;
        }

        ///// <summary>
        ///// 赋值框是否可见
        ///// </summary>
        //public bool TokenEditVisible
        //{
        //    set
        //    {
        //        tkeItems.Visible = value;
        //    }
        //    get
        //    {
        //        return tkeItems.Visible;
        //    }
        //}

        /// <summary>
        /// 提示框是否可见
        /// </summary>
        public bool TipVisible
        {
            set
            {
                pnlTop.Visible = value;
            }
            get
            {
                return pnlTop.Visible;
            }
        }

        /// <summary>
        /// 操作提示信息
        /// </summary>
        public string OperationTip
        {
            set
            {
                txtTip.Text = value;
            }
        }

        /// <summary>
        /// 获得被选择的树形节点
        /// </summary>
        public GetTreeNodeListDelegate GetTreeNodeListDelegate
        {
            set
            {
                _getTreeNodeListDelegate = value;
            }
            get
            {
                return _getTreeNodeListDelegate;
            }
        }

        ///// <summary>
        ///// 设置被选择的节点
        ///// </summary>
        //public IList<CommonNode> CommonList
        //{
        //    set
        //    {
        //        if (value == null)
        //        {
        //            return;
        //        }
        //        foreach (CommonNode commonNode in value)
        //        {
        //            SetCheckBox(treeView.Nodes, commonNode.NodeId);
        //        }
        //    }
        //}

        #endregion

        #region 窗体及控件方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public MultiSelectedItemsForm()
        {
            InitializeComponent();
            OnlyLeafSelected = true;
            treeViewHandler = new TreeViewHandler<TreeNode>(treeView);
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiSelectedItemsForm_Load(object sender, EventArgs e)
        {
            InitalizeTreeView();
        }

       
        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>();
            foreach (TokenEditToken token in tkeItems.Properties.Tokens)
            {
                commonNodes.Add(new CommonNode(Convert.ToDecimal(token.Value), token.Description));                
            }
            GetTreeNodeListDelegate?.Invoke(commonNodes);
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

        /// <summary>
        /// 选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            //if (AutoSelectedNext)
            //{
            //    if (e.Node.Checked && e.Node.Nodes.Count > 0)
            //    {
            //        e.Node.Expand();
            //    }
            //    foreach (TreeNode tn in e.Node.Nodes)
            //    {
            //        tn.Checked = e.Node.Checked;
            //    }
            //}
            
            CommonNode commonNode = e.Node.Tag as CommonNode;
            bool result = true;
            if (OnlyLeafSelected)
            {
                result = MultiSelectedItemsHandler.IsLeafNode(e.Node);
            }
            if (result & !loading)
            {
                if (e.Node.Checked)
                {
                    AddTokenEidtValue(commonNode);
                }
                else
                {
                    RemoveTokenEidtValue(commonNode);
                }
            }
        }

        /// <summary>
        /// 展开后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            if (e.Node.IsExpanded)
            {
                e.Node.ImageIndex = 1;
            }
            else
            {
                e.Node.ImageIndex = 0;
            }
            try
            {
                Cursor = Cursors.WaitCursor;
                if (e.Node.Nodes.Count == 1)
                {
                    CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                    if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                    {
                        IList<CommonNode> commonNodes = MultiSelectedItemsHandler.AfterExpand(e.Node);
                        treeViewHandler.LoadPartialNodes(e.Node, commonNodes);
                        if (e.Node.Nodes.Count > 0)
                        {
                            loading = true;
                            IList<CommonNode> nodes = new List<CommonNode>(tkeItems.Properties.Tokens.Count);
                            foreach (TokenEditToken tk in tkeItems.Properties.Tokens)
                            {
                                nodes.Add(new CommonNode(Convert.ToDecimal(tk.Value), tk.Description));
                            }
                            foreach (TreeNode chid in e.Node.Nodes)
                            {
                                CommonNode node = chid.Tag as CommonNode;
                                if (node != null)
                                {
                                    if (nodes.FindIndex(val => (val.NodeId == node.NodeId)) >= 0)
                                    {
                                        chid.Checked = true;
                                    }
                                }
                            }
                            loading = false;
                        }
                    }                    
                }
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 收拢后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            if (e.Node.IsExpanded)
            {
                e.Node.ImageIndex = 1;
            }
            else
            {
                e.Node.ImageIndex = 0;
            }
        }

        /// <summary>
        /// 选择前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_BeforeCheck(object sender, TreeViewCancelEventArgs e)
        {
            if (MultiSelectedItemsHandler.OnlySelectedLeaf && (e.Node.Nodes.Count > 0))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 选择前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scCondition_ButtonPressed(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Query();
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Query();
            }
        }


        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tkeItems_EditValueChanged(object sender, EventArgs e)
        {
            if (tkeItems.EditValue != null)
            {
                IList<CommonNode> commonNodes = new List<CommonNode>(tkeItems.Properties.Tokens.Count);
                foreach (TokenEditToken tk in tkeItems.Properties.Tokens)
                {
                    commonNodes.Add(new CommonNode(Convert.ToDecimal(tk.Value), tk.Description));
                }
                string value = tkeItems.EditValue.ToString();
                if (!string.IsNullOrWhiteSpace(value))
                {
                    string[] values = value.Split(',');
                    if (values != null && values.Length > 0)
                    {
                        List<decimal> keys = new List<decimal>();
                        foreach (var val in values)
                        {
                            keys.Add(Convert.ToDecimal(val.Trim()));
                        }
                        for (int idx = commonNodes.Count - 1; idx >= 0; idx--)
                        {
                            int pos = keys.FindIndex(val => val.Equals(commonNodes[idx].NodeId));
                            if (pos < 0)
                            {
                                tkeItems.Properties.BeginUpdate();
                                tkeItems.Properties.Tokens.RemoveAt(idx);
                                tkeItems.Properties.EndUpdate();
                                CancelCheckedNode(treeView.Nodes, commonNodes[idx].NodeId);
                            }
                        }
                    }
                }
                else
                {
                    CancelCheckedNode(treeView.Nodes);
                    tkeItems.Properties.BeginUpdate();
                    tkeItems.Properties.Tokens.Clear();
                    UpdateTokenEditValue();
                }
            }
            else
            {
                CancelCheckedNode(treeView.Nodes);
                tkeItems.Properties.BeginUpdate();
                tkeItems.Properties.Tokens.Clear();
                UpdateTokenEditValue();
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 设置赋值框
        /// </summary>
        public void SetTokenEidtValues(IList<CommonNode> commonNodes)
        {
            if (commonNodes != null)
            {
                foreach (var commonNode in commonNodes)
                {
                    AddTokenEidtValue(commonNode);
                }
                tkeItems.EditValue = CommonObjHelper.GetCommonNodeIdsWithComma(commonNodes);
            }
            else
            {
                tkeItems.EditValue = null;
            }
            loading = false;
        }

        #endregion

        #region 私有方法
    
        /// <summary>
        /// 清除所有选择
        /// </summary>
        /// <param name="tnc"></param>
        private void CancelCheckedNode(TreeNodeCollection tnc)
        {
            foreach (TreeNode tn in tnc)
            {
                if (tn.Checked)
                {
                    tn.Checked = false;
                }
                CancelCheckedNode(tn.Nodes);
            }
        }

        /// <summary>
        /// 取消节点
        /// </summary>
        /// <param name="tnc"></param>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        private bool CancelCheckedNode(TreeNodeCollection tnc, decimal nodeId)
        {
            bool find = false;

            foreach (TreeNode tn in tnc)
            {
                CommonNode childNode = tn.Tag as CommonNode;
                if (childNode != null && childNode.NodeId == nodeId)
                {
                    tn.Checked = false;
                    find = true;
                    break;
                }
                if(!find)
                {
                    if (tn.Nodes.Count == 1)
                    {
                        CommonNode node = tn.Nodes[0].Tag as CommonNode;
                        if ((node != null) && (!DataConvertionHelper.IsNullValue(node.NodeId)))
                        {
                            find = CancelCheckedNode(tn.Nodes, nodeId);
                            if (find) break;
                        }
                    }
                    else if (tn.Nodes.Count > 1)
                    {
                        find = CancelCheckedNode(tn.Nodes, nodeId);
                        if (find) break;
                    }
                }
            }

            return find;
                
        }

        /// <summary>
        /// 移除赋值内容
        /// </summary>
        /// <param name="commonNode"></param>
        private void RemoveTokenEidtValue(CommonNode commonNode)
        {
            if (commonNode == null)
            {
                return;
            }
            int pos = tkeItems.Properties.Tokens.IndexOf(new TokenEditToken(commonNode.NodeName, commonNode.NodeId.ToString()));
            if (pos >= 0)
            {
                tkeItems.Properties.BeginUpdate();
                tkeItems.Properties.Tokens.RemoveAt(pos);
                UpdateTokenEditValue();
                tkeItems.Properties.EndUpdate();
            }
        }

        /// <summary>
        /// 返回查找结果
        /// </summary>
        /// <param name="commonNode"></param>
        /// <returns></returns>
        private int FindItem(CommonNode commonNode)
        {
            IList<CommonNode> commonNodes = new List<CommonNode>(tkeItems.Properties.Tokens.Count);
            foreach (TokenEditToken tk in tkeItems.Properties.Tokens)
            {
                commonNodes.Add(new CommonNode(Convert.ToDecimal(tk.Value), tk.Description));
            }
            int pos = commonNodes.FindIndex(node => (node.NodeId == commonNode.NodeId));

            return pos;

        }

        /// <summary>
        /// 增加赋值内容
        /// </summary>
        /// <param name="commonNode"></param>
        private void AddTokenEidtValue(CommonNode commonNode)
        {
            if (commonNode == null)
            {
                return;
            }

            int pos = tkeItems.Properties.Tokens.IndexOf(new TokenEditToken(commonNode.NodeName, commonNode.NodeId.ToString()));
            if (pos >= 0)
            {
                return;
            }
            TokenEditToken token = new TokenEditToken()
            {
                Value = commonNode.NodeId.ToString(),
                Description = commonNode.NodeName
            };
            tkeItems.Properties.BeginUpdate();
            tkeItems.Properties.Tokens.Add(token);
            if (!loading)
            {
                UpdateTokenEditValue();                
            }
            tkeItems.Properties.EndUpdate();
           
        }

        /// <summary>
        /// 更新赋值框内的值
        /// </summary>
        private void UpdateTokenEditValue()
        {
            IList<CommonNode> commonNodes = new List<CommonNode>(tkeItems.Properties.Tokens.Count);
            foreach (TokenEditToken tk in tkeItems.Properties.Tokens)
            {
                commonNodes.Add(new CommonNode(Convert.ToDecimal(tk.Value), tk.Description));
            }
            tkeItems.EditValue = CommonObjHelper.GetCommonNodeIdsWithComma(commonNodes);
        }

        /// <summary>
        /// 设置复选框
        /// </summary>
        /// <param name="tc"></param>
        /// <param name="nodeId"></param>
        private void SetCheckBox(TreeNodeCollection tc, decimal nodeId)
        {
            foreach (TreeNode tn in tc)
            {
                CommonNode commonNode = tn.Tag as CommonNode;
                if (commonNode.NodeId == nodeId)
                {
                    TreeNode tnParent = tn.Parent;
                    while (tnParent != null)
                    {
                        if (tnParent.IsExpanded == false)
                        {
                            tnParent.Expand();
                        }
                        tnParent = tnParent.Parent;
                    }
                    if (tn.Nodes.Count > 0)
                    {
                        tn.Expand();
                    }
                    tn.Checked = true;
                    break;
                }
                SetCheckBox(tn.Nodes, nodeId);
            }
        }

        /// <summary>
        /// 初始化树
        /// </summary>
        private void InitalizeTreeView()
        {
            IList<CommonNode> commonNodes = MultiSelectedItemsHandler.InitTree();
            treeViewHandler.InitFirstLevelNodes(commonNodes);
            if (treeView.Nodes.Count == 1)
            {
                treeView.SelectedNode = treeView.Nodes[0];
                treeView.Nodes[0].Expand();
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        private void Query()
        {
            string content = scCondition.Text.Trim();
            if (!string.IsNullOrWhiteSpace(content))
            {
                IList<CommonNode> commonNodes = MultiSelectedItemsHandler.Query(content);
                treeViewHandler.InitFirstLevelNodes(commonNodes);
            }
            else
            {
                InitalizeTreeView();
            }
        }


        #endregion

    }
}
