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
    /// 树形下拉框
    /// </summary>
    public partial class ComoboxTreeview : UserControl
    {
        #region 私有变量

        private bool enableNodeSelected = true;
        private TreeNode currentNode = null;

        #endregion

        #region 内部成员变量

        private IList<TreeNode> _checkedTreeNodes;
        private bool _onlySelectedLeaf = false;
        private readonly TreeViewHandler<TreeNode> _treeViewHandler;

        #endregion

        #region 事件

        #region 定义"节点展开前"事件

        private event EventHandler<TreeViewCancelEventArgs> _BeforeTreeNodeExpand;

        /// <summary>
        /// 定义"节点展开前"事件访问器
        /// </summary>
        [
        Description("节点展开前"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeViewCancelEventArgs> BeforeTreeNodeExpand
        {
            add
            {
                _BeforeTreeNodeExpand += value;
            }
            remove
            {
                _BeforeTreeNodeExpand -= value;
            }
        }

        /// <summary>
        /// 定义"节点展开前"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void TreeNodeBeforeExpand(TreeViewCancelEventArgs e)
        {
            if (_BeforeTreeNodeExpand != null) _BeforeTreeNodeExpand(this, e);
        }
        #endregion

        #region 定义"节点展开后"事件

        private event EventHandler<TreeViewEventArgs> _AfterTreeNodeExpand;

        /// <summary>
        /// 定义"节点展开后"事件访问器
        /// </summary>
        [
        Description("节点展开后"),
        Category("自定义事件"),
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
        Category("自定义事件"),
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
        Category("自定义事件"),
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
        Category("自定义事件"),
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
        Category("自定义事件"),
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

        #region 定义"下拉树形结构关闭"事件

        private event EventHandler<ClosedEventArgs> _OnEditClosed;

        /// <summary>
        /// 定义"下拉树形结构关闭"事件访问器
        /// </summary>
        [
        Description("下拉树形结构可见性变化"),
        Category("自定义事件"),
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
        Category("自定义事件"),
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

        #region 定义"节点悬停"事件

        private event EventHandler<TreeNodeMouseHoverEventArgs> _OnNodeMouseHover;

        /// <summary>
        /// 定义"节点悬停"事件访问器
        /// </summary>
        [
        Description("节点悬停"),
        Category("自定义事件"),
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

        #region 定义"清除"事件

        private event EventHandler<EventArgs> _OnNodeRemoved;

        /// <summary>
        /// 定义"清除"事件访问器
        /// </summary>
        [
        Description("清除"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> OnNodeRemoved
        {
            add
            {
                _OnNodeRemoved += value;
            }
            remove
            {
                _OnNodeRemoved -= value;
            }
        }

        /// <summary>
        /// 定义"下拉树形结构关闭"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void NodeRemoved(EventArgs e)
        {
            if (_OnNodeRemoved != null) _OnNodeRemoved(this, e);
        }

        #endregion

        #region 定义"下拉树形结构弹出前"事件

        private event EventHandler<EventArgs> _BeforeControlPopup;

        /// <summary>
        /// 定义"下拉树形结构弹出前"事件访问器
        /// </summary>
        [
        Description("下拉树形结构弹出前"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> BeforeControlPopup
        {
            add
            {
                _BeforeControlPopup += value;
            }
            remove
            {
                _BeforeControlPopup -= value;
            }
        }

        /// <summary>
        /// 定义"下拉树形结构弹出前"引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void ControlBeforePopup(EventArgs e)
        {
            if (_BeforeControlPopup != null) _BeforeControlPopup(this, e);
        }

        #endregion

        #region 定义"下拉按钮点击"事件

        private event EventHandler<ButtonPressedEventArgs> _ButtonClick;

        /// <summary>
        /// 定义"下拉按钮点击"事件访问器
        /// </summary>
        [
        Description("下拉按钮点击"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ButtonPressedEventArgs> ButtonClick
        {
            add
            {
                _ButtonClick += value;
            }
            remove
            {
                _ButtonClick -= value;
            }
        }

        /// <summary>
        /// 定义"下拉按钮点击"引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void OnButtonClick(ButtonPressedEventArgs e)
        {
            if (_ButtonClick != null) _ButtonClick(this, e);
        }

        #endregion

        #region 定义"下拉按钮按下"事件

        private event EventHandler<ButtonPressedEventArgs> _ButtonPressed;

        /// <summary>
        /// 定义"下拉按钮按下"事件访问器
        /// </summary>
        [
        Description("下拉按钮按下"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<ButtonPressedEventArgs> ButtonPressed
        {
            add
            {
                _ButtonPressed += value;
            }
            remove
            {
                _ButtonPressed -= value;
            }
        }

        /// <summary>
        /// 定义"下拉按钮按下"引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void OnButtonPressed(ButtonPressedEventArgs e)
        {
            if (_ButtonPressed != null) _ButtonPressed(this, e);
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
        DefaultValue(false),
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

        /// <summary>
        /// 是否显示搜索
        /// </summary>
        [
        Description("是否显示搜索"),
        Category("自定义杂项"),
        DefaultValue(false),
        ]
        public bool ShowSearch
        {
            set;
            get;
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
        public CommonNode SelectedNode
        {
            set
            {
                if (value == null)
                {
                    treeView.SelectedNode = null;
                    SetEditFromTree();
                }
                else
                {
                    SetSelectedNode(treeView.Nodes, value);
                }
            }
            get
            {
                CommonNode commonNode = null;
                if (treeView.SelectedNode != null)
                {
                    commonNode = treeView.SelectedNode.Tag as CommonNode;
                }

                return commonNode;
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
                popupContainerEdit.EditValue = value;
            }
            get
            {
                return popupContainerEdit.EditValue;
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

        [
        Description("编辑框值"),
        Category("自定义杂项"),
        DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        Browsable(false),
        ]
        public TreeViewHandler<TreeNode> TreeViewHandler
        {
            get
            {
                return _treeViewHandler;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ComoboxTreeview()
        {
            InitializeComponent();
            _treeViewHandler = new TreeViewHandler<TreeNode>(treeView);
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
            if (!DesignMode)
            {
                scCondition.Visible = ShowSearch;
                treeView.Width = popupContainerEdit.Width = popupContainerControl.Width = Width - 10;
            }
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
        /// 节点展开前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeBeforeExpand(e);
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

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_Closed(object sender, DevExpress.XtraEditors.Controls.ClosedEventArgs e)
        {
            toolTipController.HideHint();
            EditClosed(e);
        }

        /// <summary>
        /// 移除选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnRemove_Click(object sender, EventArgs e)
        {
            RemoveSelectedNodes();
            NodeRemoved(e);
        }

        /// <summary>
        /// 更改选定的内容前发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            TreeNodeBeforeSelect(e);
            if ((_onlySelectedLeaf && (e.Node.Nodes.Count > 0)) || !enableNodeSelected)
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
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scCondition_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            toolTipController.HideHint();
            Search(new StringEventArgs(scCondition.Text.Trim()));
        }

        /// <summary>
        /// 回车查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scCondition_KeyPress(object sender, KeyPressEventArgs e)
        {
            toolTipController.HideHint();
            if (e.KeyChar == (char)13)
            {
                Search(new StringEventArgs(scCondition.Text.Trim()));
            }
        }

        /// <summary>
        /// 查询条件发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scCondition_EditValueChanged(object sender, EventArgs e)
        {
            toolTipController.HideHint();
            string condition = scCondition.Text.Trim();
            if (string.IsNullOrWhiteSpace(condition))
            {
                Search(new StringEventArgs(condition));
            }
        }

        /// <summary>
        /// 节点悬停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeView_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            NodeMouseHover(e);
            if (!string.IsNullOrWhiteSpace(e.Node.ToolTipText))
            {
                toolTipController.ShowHint(e.Node.ToolTipText, DevExpress.Utils.ToolTipLocation.RightCenter);
            }
        }

        /// <summary>
        /// 界面弹出前禁止选择节点（防止控件自动默认根节点）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_BeforePopup(object sender, EventArgs e)
        {
            currentNode = treeView.SelectedNode;
            enableNodeSelected = false;
            ControlBeforePopup(e);
        }

        /// <summary>
        /// 界面弹出后允许选择节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_Popup(object sender, EventArgs e)
        {
            enableNodeSelected = true;
            treeView.SelectedNode = currentNode;
        }

        /// <summary>
        /// 下拉按钮点击
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            OnButtonClick(e);
        }

        /// <summary>
        /// 下拉按钮按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_ButtonPressed(object sender, ButtonPressedEventArgs e)
        {
            OnButtonPressed(e);
        }
        
        /// <summary>
        /// 失去焦点关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void popupContainerEdit_Leave(object sender, EventArgs e)
        {
            if (popupContainerControl.OwnerEdit != null)
            {
                popupContainerControl.OwnerEdit.ClosePopup();
            }
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 关闭
        /// </summary>
        public void ClosePopup()
        {
            if (popupContainerControl.OwnerEdit != null)
            {
                popupContainerControl.OwnerEdit.ClosePopup();
            }
        }

        /// <summary>
        /// 移除已选择节点
        /// </summary>
        public void RemoveSelectedNodes()
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
            popupContainerEdit.Text = string.Empty;
            toolTipController.HideHint();
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
        private void SetSelectedNode(TreeNodeCollection tnc, CommonNode commonNode)
        {
            foreach (TreeNode tn in tnc)
            {
                CommonNode commonNodeTag = tn.Tag as CommonNode;
                if (commonNodeTag.NodeId == commonNode.NodeId)
                {
                    if (OnlySelectedLeaf)
                    {
                        if (commonNodeTag.IsLeaf)
                        {
                            treeView.SelectedNode = tn;
                            SetEditFromTree();
                        }
                    }
                    else
                    {
                        treeView.SelectedNode = tn;
                        SetEditFromTree();
                    }
                    break;
                }
                SetSelectedNode(tn.Nodes, commonNode);
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
                Value = null;
            }
            else
            {
                if (!treeView.CheckBoxes)
                {
                    CommonNode commonNode = treeView.SelectedNode.Tag as CommonNode;
                    Value = commonNode;
                }
            }
        }

        #endregion

    }
}
