using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.LookAndFeel.Design;
using DevExpress.XtraEditors.Controls;
using AppFramework.Core;

namespace AppFramework.WinFormsControls
{
    /// <summary>
    /// 带搜索的树形下拉框
    /// </summary>
    public partial class ComoboxTreeviewWithSearch : UserControl
    {
        #region 内部成员变量

        private IList<TreeNode> _checkedTreeNodes;
        private bool _isNodeType = true;
        private bool _onlySelectedLeaf = false;

        #endregion

        #region 事件

        #region 定义"节点展开后"事件

        private event EventHandler<TreeViewEventArgs> _AfterTreeNodeExpand;

        /// <summary>
        /// 定义"节点展开后"事件访问器
        /// </summary>
        [
        Description("节点展开后"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeViewEventArgs> AfterTreeNodeExpand
        {
            add
            {
                _AfterTreeNodeExpand += value;
            }
            remove
            {
                _AfterTreeNodeExpand -= value;
            }
        }

        /// <summary>
        /// 定义"节点展开后"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void TreeNodeAfterExpand(TreeViewEventArgs e)
        {
            if (_AfterTreeNodeExpand != null) _AfterTreeNodeExpand(this, e);
        }

        #endregion

        #region 定义"更改选定的内容前发生"事件

        private event EventHandler<TreeViewCancelEventArgs> _BeforeTreeNodeSelect;

        /// <summary>
        /// 定义"更改选定的内容前发生"事件访问器
        /// </summary>
        [
        Description("更改选定的内容前发生"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeViewCancelEventArgs> BeforeTreeNodeSelect
        {
            add
            {
                _BeforeTreeNodeSelect += value;
            }
            remove
            {
                _BeforeTreeNodeSelect -= value;
            }
        }

        /// <summary>
        /// 定义"更改选定的内容前发生"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void TreeNodeBeforeSelect(TreeViewCancelEventArgs e)
        {
            if (_BeforeTreeNodeSelect != null) _BeforeTreeNodeSelect(this, e);
        }

        #endregion

        #region 定义"更改选定的内容后发生"事件

        private event EventHandler<TreeViewEventArgs> _AfterTreeNodeSelect;

        /// <summary>
        /// 定义"更改选定的内容后发生"事件访问器
        /// </summary>
        [
        Description("节点展开后"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeViewEventArgs> AfterTreeNodeSelect
        {
            add
            {
                _AfterTreeNodeSelect += value;
            }
            remove
            {
                _AfterTreeNodeSelect -= value;
            }
        }

        /// <summary>
        /// 定义"节点展开后"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void TreeNodeAfterSelect(TreeViewEventArgs e)
        {
            if (_AfterTreeNodeSelect != null) _AfterTreeNodeSelect(this, e);
        }

        #endregion

        #region 定义"单击节点"事件

        private event EventHandler<TreeNodeMouseClickEventArgs> _OnTreeNodeMouseClick;

        /// <summary>
        /// 定义"单击节点"事件访问器
        /// </summary>
        [
        Description("单击节点"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeMouseClickEventArgs> OnTreeNodeMouseClick
        {
            add
            {
                _OnTreeNodeMouseClick += value;
            }
            remove
            {
                _OnTreeNodeMouseClick -= value;
            }
        }

        /// <summary>
        /// 定义"单击节点"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void TreeNodeMouseClick(TreeNodeMouseClickEventArgs e)
        {
            if (_OnTreeNodeMouseClick != null) _OnTreeNodeMouseClick(this, e);
        }

        #endregion

        #region 定义"双击节点"事件

        private event EventHandler<TreeNodeMouseClickEventArgs> _OnNodeMouseDoubleClick;

        /// <summary>
        /// 定义"双击节点"事件访问器
        /// </summary>
        [
        Description("双击节点"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeMouseClickEventArgs> OnNodeMouseDoubleClick
        {
            add
            {
                _OnNodeMouseDoubleClick += value;
            }
            remove
            {
                _OnNodeMouseDoubleClick -= value;
            }
        }

        /// <summary>
        /// 定义"双击节点"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void NodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
        {
            if (_OnNodeMouseDoubleClick != null) _OnNodeMouseDoubleClick(this, e);
        }

        #endregion

        #region 定义"节点悬停"事件

        private event EventHandler<TreeNodeMouseHoverEventArgs> _OnNodeMouseHover;

        /// <summary>
        /// 定义"节点悬停"事件访问器
        /// </summary>
        [
        Description("节点悬停"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeMouseHoverEventArgs> OnNodeMouseHover
        {
            add
            {
                _OnNodeMouseHover += value;
            }
            remove
            {
                _OnNodeMouseHover -= value;
            }
        }

        /// <summary>
        /// 定义"节点悬停"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void NodeMouseHover(TreeNodeMouseHoverEventArgs e)
        {
            if (_OnNodeMouseHover != null) _OnNodeMouseHover(this, e);
        }

        #endregion

        #region 定义"下拉树形结构关闭"事件

        private event EventHandler<ClosedEventArgs> _OnEditClosed;

        /// <summary>
        /// 定义"下拉树形结构关闭"事件访问器
        /// </summary>
        [
        Description("下拉树形结构可见性变化"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<ClosedEventArgs> OnEditClosed
        {
            add
            {
                _OnEditClosed += value;
            }
            remove
            {
                _OnEditClosed -= value;
            }
        }

        /// <summary>
        /// 定义"下拉树形结构关闭"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void EditClosed(ClosedEventArgs e)
        {
            if (_OnEditClosed != null) _OnEditClosed(this, e);
        }

        #endregion
        
        #region 定义"查询按钮点击"事件

        private event EventHandler<StringEventArgs> _OnSearch;

        /// <summary>
        /// 定义"查询按钮点击"事件访问器
        /// </summary>
        [
        Description("查询按钮点击"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public event EventHandler<StringEventArgs> OnSearch
        {
            add
            {
                _OnSearch += value;
            }
            remove
            {
                _OnSearch -= value;
            }
        }

        /// <summary>
        /// 定义"下拉树形结构关闭"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void Search(StringEventArgs e)
        {
            if (_OnSearch != null) _OnSearch(this, e);
        }

        #endregion

        #endregion

        #region 属性

        [DefaultValue("DevExpress Style")]
        [RefreshProperties(RefreshProperties.All)]
        [Description("获得或是设置皮肤样式")]
        [TypeConverter(typeof(SkinNameTypeConverter))]
        public string SkinName
        {
            set
            {
                popupContainerEdit.Properties.LookAndFeel.SkinName = value;
                popupContainerControl.LookAndFeel.SkinName = value;
                panelControl.LookAndFeel.SkinName = value;
                sbtnRemove.LookAndFeel.SkinName = value;
                scCondition.Properties.LookAndFeel.SkinName = value;
                pnlCondition.LookAndFeel.SkinName = value;
            }
            get
            {
                return popupContainerEdit.Properties.LookAndFeel.SkinName;
            }
        }

        /// <summary>
        /// 是否只读
        /// </summary>
        [
        Description("是否只读"),
        Category("自定义杂项"),
        DefaultValue(true),
        ]
        public bool ReadOnly
        {
            set
            {
                popupContainerEdit.Properties.ReadOnly = value;
            }
            get
            {
                return popupContainerEdit.Properties.ReadOnly;
            } 
        }

        [Description("控件的宽度"),
        Category("自定义杂项")]
        public new int Width 
        { 
            get
            {
                return base.Width;
            }
            set 
            {
                base.Width = value;
            }
        }

        /// <summary>
        /// 节点可以有相同的编号而类型不同， 从而来选择节点
        /// </summary>
        [
        Description("节点可以有相同的编号而类型不同， 从而来选择节点"),
        Category("自定义杂项"),
        DefaultValue(true),
        ]
        public bool IsNodeType
        {
            get
            {
                return _isNodeType;
            }
            set
            {
                _isNodeType = value;
            }
        }

        [
        Description("树形结构"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public TreeView TreeView
        {
            get
            {
                return treeView;
            }
        }

        [
        Description("被选择的节点"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public TreeNode SelectedNode
        {
            set
            {
                if (_isNodeType && (value != null))
                {
                    SetSelectedNode(treeView.Nodes, value);
                }
                else
                {
                    treeView.SelectedNode = value;
                }
                if (value == null)
                {
                    SetEditFromTree();
                }
            }
            get
            {
                return treeView.SelectedNode;
            }
        }

        [
        Description("被选择的节点编号"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public decimal SelectedNodeId
        {
            set
            {
                SetSelectedNode(treeView.Nodes, value);
            }
            get
            {
                if (treeView.SelectedNode != null)
                {
                    CommonNode commonNodeTag = treeView.SelectedNode.Tag as CommonNode;
                    return commonNodeTag.NodeId;
                }
                return decimal.MinValue;
            } 
        }

        [
        Description("文本"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public override string Text
        {
            set
            {
                popupContainerEdit.Text = value;
            }
            get
            {               
                    return popupContainerEdit.Text;
            }
        }

        [
       Description("编辑框值"),
       Category("自定义杂项"),
       DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
       Browsable(false),
       ]
        public object Value
        {
            set
            {
                if (value != null)
                {
                    popupContainerEdit.Text = value.ToString();
                }
                popupContainerEdit.Tag = value;
            }
            get
            {
                return popupContainerEdit.Tag;
            }
        }

        [
        Description("被选择的节点"),
        Category("自定义杂项"),
        DefaultValue(false),
        ]
        public bool CheckBoxes
        {
            set
            {
                treeView.CheckBoxes = value;
            }
            get
            {
                return treeView.CheckBoxes;
            }
        }

        [
        Description("允许多选是，选择的节点列表"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public IList<TreeNode> CheckedTreeNodes
        {
            get
            {
                if (_checkedTreeNodes == null)
                {
                    _checkedTreeNodes = new List<TreeNode>();
                }
                return _checkedTreeNodes;
            }
        }

        [
        Description("只能选择叶子节点"),
        Category("自定义杂项"),
        DefaultValue(false),
        ]
        public bool OnlySelectedLeaf
        {
            get
            {
                return _onlySelectedLeaf;
            }
            set
            {
                _onlySelectedLeaf = value;
            }
        }

        /// <summary>
        /// 空值时显示的文本
        /// </summary>
        [
        Description("空值时显示的文本"),
        Category("自定义杂项"),
        DefaultValue("请输入搜索内容"),
        ]
        public string NullValuePrompt
        {
            get
            {
                return scCondition.Properties.NullValuePrompt;
            }
            set
            {
                scCondition.Properties.NullValuePrompt = value;
            }
        }

        /// <summary>
        /// 搜索文本
        /// </summary>
        [
        Description("搜索文本"),
        Category("自定义杂项"),
        DefaultValue(""),
        ]
        public string TextBySearching
        {
            get
            {
                return scCondition.Text;
            }
            set
            {
                scCondition.Text = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// 显示查询下拉按钮
        /// </summary>
        [
        Description("显示查询下拉按钮"),
        Category("自定义杂项"),
        DefaultValue(false),
        ]
        public bool ShowMRUButton
        {
            get
            {
                return scCondition.Properties.ShowMRUButton;
            }
            set
            {
                scCondition.Properties.ShowMRUButton = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <summary>
        /// 显示节点提示
        /// </summary>
        [
        Description("显示节点提示"),
        Category("自定义杂项"),
        DefaultValue(false),
        ]
        public bool ShowNodeToolTips
        {
            get
            {
                return treeView.ShowNodeToolTips;
            }
            set
            {
                treeView.ShowNodeToolTips = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComoboxTreeviewWithSearch()
        {
            InitializeComponent();            
        }

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DevExpreesComoboxTreeview_Load(object sender, EventArgs e)
        {
            treeView.Width = popupContainerControl.Width = Width;
        }

        /// <summary>
        /// 在选择节点后发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CommonNode commonNode = e.Node.Tag as CommonNode;
            Value = commonNode;
            TreeNodeAfterSelect(e);
        }


        /// <summary>
        /// 单击节点 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {            
            TreeNodeMouseClick(e);

        }

        /// <summary>
        /// 双击节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {            
            ClickTreeNode();
            NodeMouseDoubleClick(e);
        }

        /// <summary>
        /// 
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
            TreeNodeAfterExpand(e);
        }

        /// <summary>
        /// 
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

        private void popupContainerEdit_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            EditClosed(e);
        }

        private void sbtnRemove_Click(object sender, EventArgs e)
        {
            if (CheckBoxes)
            {
                ClearSelectedNodes(treeView.Nodes);
            }
            else
            {
                Value = null;
            }
            treeView.SelectedNode = null;
        }

        /// <summary>
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scCondition_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            Search(new StringEventArgs(scCondition.Text.Trim()));
        }

        /// <summary>
        /// 回车查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                Search(new StringEventArgs(scCondition.Text.Trim()));
            }
        }

        /// <summary>
        /// 文本内容清空后查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scCondition_EditValueChanged(object sender, EventArgs e)
        {
            string content = scCondition.Text.Trim();
            if (string.IsNullOrWhiteSpace(content))
            {
                Search(new StringEventArgs(content));
            }
        }

        /// <summary>
        /// 更改选定的内容前发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeBeforeSelect(e);
            if (_onlySelectedLeaf && (e.Node.Nodes.Count > 0))
            {
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 多选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Checked)
            {
                CheckedTreeNodes.Add(e.Node);
            }
            else
            {
                CheckedTreeNodes.Remove(e.Node);
            }
            StringBuilder sb = new StringBuilder();
            foreach (TreeNode tn in CheckedTreeNodes)
            {
                if (sb.Length > 0)
                {
                    sb.Append(", ");
                }
                sb.Append(tn.Text);
            }
            popupContainerEdit.Text = sb.ToString();
            popupContainerEdit.ToolTip = sb.ToString();
        }

        /// <summary>
        /// 节点悬停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            NodeMouseHover(e);
        }

        #endregion
               
        #region 私有方法

        /// <summary>
        /// 清除被选的节点
        /// </summary>
        /// <param name="tnc"></param>
        private void ClearSelectedNodes(TreeNodeCollection tnc)
        {
            foreach (TreeNode tn in tnc)
            {
                if (tn.Checked)
                {
                    tn.Checked = false;
                }
                if (tn.Nodes.Count > 0)
                {
                    ClearSelectedNodes(tn.Nodes);
                }
            }
        }

        /// <summary>
        /// 点击树形节点
        /// </summary>
        private void ClickTreeNode()
        {
            if (treeView.SelectedNode == null)
            {
                return;
            }
            if (treeView.SelectedNode.IsExpanded)
            {
                treeView.SelectedNode.ImageIndex = 1;
            }
            else
            {
                treeView.SelectedNode.ImageIndex = 0;
            }
            SetEditFromTree();
            if (popupContainerControl.OwnerEdit != null)
            {
                popupContainerControl.OwnerEdit.ClosePopup();
            }
        }
  
        /// <summary>
        /// 设置被选的节点
        /// </summary>
        /// <param name="tnc"></param>
        /// <param name="treeNode"></param>
        private void SetSelectedNode(TreeNodeCollection tnc, TreeNode treeNode)
        {
            CommonNode commonNode = treeNode.Tag as CommonNode;
            foreach (TreeNode tn in tnc)
            {
                CommonNode commonNodeTag = tn.Tag as CommonNode;                
                if ((commonNodeTag.NodeType == commonNode.NodeType) && (commonNodeTag.NodeId == commonNode.NodeId) 
                    && (!OnlySelectedLeaf || (OnlySelectedLeaf && commonNodeTag.IsLeaf)))
                {
                    treeView.SelectedNode = tn;
                    SetEditFromTree();
                    break;
                }
                else if (tn.Nodes.Count > 0)
                {
                    SetSelectedNode(tn.Nodes, treeNode);
                }
            }
        }

        /// <summary>
        /// 设置被选的节点
        /// </summary>
        /// <param name="tnc"></param>
        /// <param name="selectedNodeId"></param>
        private void SetSelectedNode(TreeNodeCollection tnc, decimal selectedNodeId)
        {
            foreach (TreeNode tn in tnc)
            {
                CommonNode commonNodeTag = tn.Tag as CommonNode;                               
                if ((commonNodeTag.NodeId == selectedNodeId) && (!OnlySelectedLeaf || (OnlySelectedLeaf && commonNodeTag.IsLeaf)))
                {
                    treeView.SelectedNode = tn;
                    SetEditFromTree();
                    break;
                }
                else if (tn.Nodes.Count > 0)
                {
                    SetSelectedNode(tn.Nodes, selectedNodeId);
                }
            }
        }
        
        /// <summary>
        /// 设置值
        /// </summary>
        private void SetEditFromTree()
        {
            if (treeView.SelectedNode == null)
            {
                popupContainerEdit.Text = string.Empty;
                return;
            }
            if (!treeView.CheckBoxes)
            {
                popupContainerEdit.Text = treeView.SelectedNode.Text ?? string.Empty;
                if (popupContainerEdit.Text != null)
                {
                    popupContainerEdit.ToolTip = popupContainerEdit.Text;
                }
            }
        }

        #endregion
    }
}
