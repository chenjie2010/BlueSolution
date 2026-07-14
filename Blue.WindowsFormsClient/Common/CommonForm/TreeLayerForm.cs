using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraBars;
using AppFramework.Core;
using AppFramework.Reference.DataFieldLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary;
using AppFramework.WinFormsLibrary.Common;
using AppFramework.WinFormsLibrary.EventArgument;

namespace Blue.WindowsFormsClient.Common
{
    public partial class TreeLayerForm : Form
    {
        #region  私有变量

        private readonly TreeViewHandler<TreeNode> treeViewHandler;
        private EditState currentEditState = EditState.None;        

        #endregion

        #region  受保护变量

        protected bool allowedToFirstLevelNode= true; /* 默认允许编辑第一层节点 */

        #endregion

        #region  内部成员变量

        private int _maxLevel;
        private bool _currentQueriedState = false;
        private bool _allowChildNodesDeleted = false;

        #endregion

        #region  受保护属性

        /// <summary>
        /// 节点操作接口契约
        /// </summary>
        protected ICommonNodeContract CommonNodeContract
        {
            get; set;
        }

        /// <summary>
        /// 节点展示操作接口属性
        /// </summary>
        protected ITreeNodeShow TreeNodeShow
        {
            get;
            set;
        }

        /// <summary>
        /// 树形结构
        /// </summary>
        protected TreeView TreeViewInLayer
        {
            get
            {
                return trvLayer;
            }
        } 
        #endregion

        #region 只读属性

        /// <summary>
        /// 当前是否处于查询状态
        /// </summary>
        [Browsable(false)]
        public bool CurrentQueriedState
        {
            get
            {
                return _currentQueriedState;
            }
        }

        #endregion

        #region 属性

        /// <summary>
        /// 树形结构允许最大的层级，以0为根节点所在的层, 设置0表示不限制最大的层级
        /// </summary>
        [Browsable(false)]
        public int MaxLevel
        {
            get
            {
                return _maxLevel;
            }
            set
            {
                _maxLevel = value;
            }
        }

        /// <summary>
        /// 设置提示信息
        /// </summary>
        [Browsable(false)]
        public string Tip
        {
            set
            {
                lblTip.Text = value;
            }
        }

        /// <summary>
        /// 允许属性节点拖动拖动
        /// </summary>
        [
        Category("自定义杂项"), 
        DefaultValue(false)]
        public bool AllowTreeNodeDrop
        {
            set
            {
                trvLayer.AllowDrop = value;
            }
            get
            {
                return trvLayer.AllowDrop;
            }
        }

        /// <summary>
        /// 查询提示
        /// </summary>
        [Browsable(false)]
        public string NullValuePrompt
        {
            get
            {
                return scQuery.Properties.NullValuePrompt;
            }
            set
            {
                scQuery.Properties.NullValuePrompt = value;
            }
        }

        /// <summary>
        /// 删除父节点时允许一起删除子节点
        /// </summary>
        [Browsable(false)]
        public bool AllowChildNodesDeleted
        {
            get
            {
                return _allowChildNodesDeleted;
            }
            set
            {
                _allowChildNodesDeleted = value;
            }
        }

        /// <summary>
        /// 设置按钮的属性
        /// </summary>
        [Browsable(false)]
        public bool SettingEnabled
        {
            get
            {
                return bbiSetting.Enabled;
            }
            set
            {
                bbiSetting.Enabled = value;
            }
        }

        /// <summary>
        /// 设置按钮的名称
        /// </summary>
        [Browsable(false)]
        public string SettingCaption
        {
            set
            {
                bbiSetting.Caption = value;
            }
        }

        /// <summary>
        /// 设置提示
        /// </summary>
        public string SettingTip
        {
            set
            {
                ((ToolTipTitleItem)bbiSetting.SuperTip.Items[0]).Text = value;
            }
        }

        /// <summary>
        /// 设置按钮的可见性
        /// </summary>
        [Browsable(false)]
        public BarItemVisibility SettingVisible
        {
            get
            {
                return bbiSetting.Visibility;
            }
            set
            {
                bbiSetting.Visibility = value;
            }
        }

        /// <summary>
        /// 自定义项的可见性
        /// </summary>
        [Browsable(false)]
        public BarItemVisibility CustomItemVisible
        {
            get
            {
                return bbiCustomItem.Visibility;
            }
            set
            {
                bbiCustomItem.Visibility = value;
            }
        }

        /// <summary>
        /// 自定义项的属性
        /// </summary>
        [Browsable(false)]
        public bool CustomItemEnabled
        {
            get
            {
                return bbiCustomItem.Enabled;
            }
            set
            {
                bbiCustomItem.Enabled = value;
            }
        }

        /// <summary>
        /// 自定义项的名称
        /// </summary>
        [Browsable(false)]
        public string CustomItemCaption
        {
            set
            {
                bbiCustomItem.Caption = value;
            }
        }

        /// <summary>
        /// 导入导出项的可见性
        /// </summary>
        [Browsable(false)]
        public BarItemVisibility ExchangeItemVisible
        {
            get
            {
                return bbiExchange.Visibility;
            }
            set
            {
                bbiExchange.Visibility = value;
            }
        }

        /// <summary>
        /// 导入导出项的属性
        /// </summary>
        [Browsable(false)]
        public bool ExchangeItemEnabled
        {
            get
            {
                return bbiExchange.Enabled;
            }
            set
            {
                bbiExchange.Enabled = value;
            }
        }

        /// <summary>
        /// 自定义项的名称
        /// </summary>
        [Browsable(false)]
        public string ExchangeItemCaption
        {
            set
            {
                bbiExchange.Caption = value;
            }
        }

        #endregion

        #region 定义事件

        #region 定义"创建之前"事件

        /// <summary>
        /// 定义"创建之前"事件
        /// </summary>
        private event EventHandler<TreeNodeItemClickEventArgs> _OnBeoreCreatedClick;

        /// <summary>
        /// 定义"创建之前"事件访问器
        /// </summary>
        [
        Description(@"点击""创建之前""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeItemClickEventArgs> OnBeoreCreatedClick
        {
            add
            {
                _OnBeoreCreatedClick += value;
            }
            remove
            {
                _OnBeoreCreatedClick -= value;
            }
        }

        /// <summary>
        /// 定义"创建之前"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void BeoreCreatedClick(TreeNodeItemClickEventArgs e)
        {
            if (_OnBeoreCreatedClick != null) _OnBeoreCreatedClick(this, e);
        }

        #endregion

        #region 定义"创建"事件

        /// <summary>
        /// 定义"创建"事件
        /// </summary>
        private event EventHandler<TreeNodeItemClickEventArgs> _OnCreateClick;

        /// <summary>
        /// 定义"创建"事件访问器
        /// </summary>
        [
        Description(@"点击""创建""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeItemClickEventArgs> OnCreateClick
        {
            add
            {
                _OnCreateClick += value;
            }
            remove
            {
                _OnCreateClick -= value;
            }
        }

        /// <summary>
        /// 定义"创建"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void CreateClick(TreeNodeItemClickEventArgs e)
        {            
            if (_OnCreateClick != null) _OnCreateClick(this, e);
        }

        #endregion

        #region 定义"编辑前"事件

        /// <summary>
        /// 定义"编辑前"事件
        /// </summary>
        private event EventHandler<TreeNodeItemClickEventArgs> _OnBeforeEditClick;

        /// <summary>
        /// 定义"编辑前"事件访问器
        /// </summary>
        [
        Description(@"点击""编辑前""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeItemClickEventArgs> OnBeforeEditClick
        {
            add
            {
                _OnEditClick += value;
            }
            remove
            {
                _OnEditClick -= value;
            }
        }

        /// <summary>
        /// 定义"编辑前"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void BeforeEditClick(TreeNodeItemClickEventArgs e)
        {
            if (_OnBeforeEditClick != null) _OnBeforeEditClick(this, e);
        }

        #endregion

        #region 定义"编辑"事件

        /// <summary>
        /// 定义"编辑"事件
        /// </summary>
        private event EventHandler<TreeNodeItemClickEventArgs> _OnEditClick;

        /// <summary>
        /// 定义"编辑"事件访问器
        /// </summary>
        [
        Description(@"点击""编辑""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeItemClickEventArgs> OnEditClick
        {
            add
            {
                _OnEditClick += value;
            }
            remove
            {
                _OnEditClick -= value;
            }
        }

        /// <summary>
        /// 定义"编辑"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void EditClick(TreeNodeItemClickEventArgs e)
        {
            if (_OnEditClick != null) _OnEditClick(this, e);
        }

        #endregion

        #region 定义"删除前"事件

        /// <summary>
        /// 定义"删除前"事件
        /// </summary>
        private event EventHandler<TreeNodeItemClickEventArgs> _OnBeforeDeleteClick;

        /// <summary>
        /// 定义"删除前"事件访问器
        /// </summary>
        [
        Description(@"点击""删除前""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeItemClickEventArgs> OnBeforeDeleteClick
        {
            add
            {
                _OnBeforeDeleteClick += value;
            }
            remove
            {
                _OnBeforeDeleteClick -= value;
            }
        }

        /// <summary>
        /// 定义"删除前"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void BeforeDeleteClick(TreeNodeItemClickEventArgs e)
        {
            if (_OnBeforeDeleteClick != null) _OnBeforeDeleteClick(this, e);
        }

        #endregion

        #region 定义"删除"事件

        /// <summary>
        /// 定义"删除"事件
        /// </summary>
        private event EventHandler<TreeNodeItemClickEventArgs> _OnDeleteClick;

        /// <summary>
        /// 定义"删除"事件访问器
        /// </summary>
        [
        Description(@"点击""删除""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeItemClickEventArgs> OnDeleteClick
        {
            add
            {
                _OnDeleteClick += value;
            }
            remove
            {
                _OnDeleteClick -= value;
            }
        }

        /// <summary>
        /// 定义"删除"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void DeleteClick(TreeNodeItemClickEventArgs e)
        {
            if (_OnDeleteClick != null) _OnDeleteClick(this, e);
        }

        #endregion

        #region 定义"设置"事件

        /// <summary>
        /// 定义"设置"事件
        /// </summary>
        private event EventHandler<TreeNodeItemClickEventArgs> _OnSettingClick;

        /// <summary>
        /// 定义"设置"事件访问器
        /// </summary>
        [
        Description(@"点击""设置""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeItemClickEventArgs> OnSettingClick
        {
            add
            {
                _OnSettingClick += value;
            }
            remove
            {
                _OnSettingClick -= value;
            }
        }

        /// <summary>
        /// 定义"设置"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void SettingClick(TreeNodeItemClickEventArgs e)
        {
            if (_OnSettingClick != null) _OnSettingClick(this, e);
        }

        #endregion

        #region 定义"自定义项"事件

        /// <summary>
        /// 定义"自定义项"事件
        /// </summary>
        private event EventHandler<TreeNodeItemClickEventArgs> _OnCustomItemClick;

        /// <summary>
        /// 定义"自定义项"事件访问器
        /// </summary>
        [
        Description(@"点击""自定义项""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeItemClickEventArgs> OnCustomItemClick
        {
            add
            {
                _OnCustomItemClick += value;
            }
            remove
            {
                _OnCustomItemClick -= value;
            }
        }

        /// <summary>
        /// 定义"自定义项"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void CustomItemClick(TreeNodeItemClickEventArgs e)
        {
            if (_OnCustomItemClick != null) _OnCustomItemClick(this, e);
        }

        #endregion

        #region 定义"导入导出"事件

        /// <summary>
        /// 定义"导入导出"事件
        /// </summary>
        private event EventHandler<TreeNodeItemClickEventArgs> _OnExchangeClick;

        /// <summary>
        /// 定义"导入导出"事件访问器
        /// </summary>
        [
        Description(@"点击""导入导出""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeItemClickEventArgs> OnExchangeClick
        {
            add
            {
                _OnExchangeClick += value;
            }
            remove
            {
                _OnExchangeClick -= value;
            }
        }

        /// <summary>
        /// 定义"导入导出"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void ExchangeClick(TreeNodeItemClickEventArgs e)
        {
            if (_OnExchangeClick != null) _OnExchangeClick(this, e);
        }

        #endregion

        #region 定义"节点选择之前"事件

        /// <summary>
        /// 定义"节点选择之前"事件
        /// </summary>
        private event EventHandler<TreeViewCancelEventArgs> _OnBeforeSelectOfTreeView;

        /// <summary>
        /// 定义"节点选择之前"事件访问器
        /// </summary>
        [
        Description(@"点击""节点选择之前""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeViewCancelEventArgs> OnBeforeSelectOfTreeView
        {
            add
            {
                _OnBeforeSelectOfTreeView += value;
            }
            remove
            {
                _OnBeforeSelectOfTreeView -= value;
            }
        }

        /// <summary>
        /// 定义"节点选择之前"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void BeforeSelectOfTreeView(TreeViewCancelEventArgs e)
        {
            if (_OnBeforeSelectOfTreeView != null) _OnBeforeSelectOfTreeView(this, e);
        }

        #endregion        

        #region 定义"节点选择之后"事件

        /// <summary>
        /// 定义"节点选择之后"事件
        /// </summary>
        private event EventHandler<TreeViewEventArgs> _OnAfterSelectOfTreeView;

        /// <summary>
        /// 定义"节点选择之后"事件访问器
        /// </summary>
        [
        Description(@"点击""节点选择之后""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeViewEventArgs> OnAfterSelectOfTreeView
        {
            add
            {
                _OnAfterSelectOfTreeView += value;
            }
            remove
            {
                _OnAfterSelectOfTreeView -= value;
            }
        }

        /// <summary>
        /// 定义"节点选择之后"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void AfterSelectOfTreeView(TreeViewEventArgs e)
        {
            if (_OnAfterSelectOfTreeView != null) _OnAfterSelectOfTreeView(this, e);
        }

        #endregion

        #region 定义"节点展开前"事件

        private event EventHandler<TreeViewCancelEventArgs> _OnBeforeTreeNodeExpand;

        /// <summary>
        /// 定义"节点展开前"事件访问器
        /// </summary>
        [
        Description("节点展开前"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeViewCancelEventArgs> OnBeforeTreeNodeExpand
        {
            add
            {
                _OnBeforeTreeNodeExpand += value;
            }
            remove
            {
                _OnBeforeTreeNodeExpand -= value;
            }
        }

        /// <summary>
        /// 定义"节点展开前"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void BeforeTreeNodeExpand(TreeViewCancelEventArgs e)
        {
            if (_OnBeforeTreeNodeExpand != null) _OnBeforeTreeNodeExpand(this, e);
        }
        #endregion

        #region 定义"节点展开后"事件

        private event EventHandler<TreeViewEventArgs> _OnAfterTreeNodeExpand;

        /// <summary>
        /// 定义"节点展开后"事件访问器
        /// </summary>
        [
        Description("节点展开后"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeViewEventArgs> OnAfterTreeNodeExpand
        {
            add
            {
                OnAfterTreeNodeExpand += value;
            }
            remove
            {
                OnAfterTreeNodeExpand -= value;
            }
        }

        /// <summary>
        /// 定义"节点展开后"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void AfterTreeNodeExpand(TreeViewEventArgs e)
        {
            if (_OnAfterTreeNodeExpand != null) _OnAfterTreeNodeExpand(this, e);
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

        #region 定义"移动节点"事件
        private event EventHandler<MovedNodeEventArgs> _OnMovedNodeClick;

        /// <summary>
        /// 定义"移动节点"事件访问器
        /// </summary>
        [
        Description("移动节点"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<MovedNodeEventArgs> OnMovedNodeClick
        {
            add
            {
                _OnMovedNodeClick += value;
            }
            remove
            {
                _OnMovedNodeClick -= value;
            }
        }

        /// <summary>
        /// 定义"移动节点"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void MovedNodeClick(MovedNodeEventArgs e)
        {
            if (_OnMovedNodeClick != null) _OnMovedNodeClick(this, e);
        }

        #endregion        

        #region 定义"查询之前"事件

        /// <summary>
        /// 定义"查询之前"事件
        /// </summary>
        private event EventHandler<EventArgs> _OnBeoreQueryClick;

        /// <summary>
        /// 定义"查询之前"事件访问器
        /// </summary>
        [
        Description(@"点击""查询之前""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<EventArgs> OnBeoreQueryClick
        {
            add
            {
                _OnBeoreQueryClick += value;
            }
            remove
            {
                _OnBeoreQueryClick -= value;
            }
        }

        /// <summary>
        /// 定义"查询之前"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void BeoreQueryClick(EventArgs e)
        {
            if (_OnBeoreQueryClick != null) _OnBeoreQueryClick(this, e);
        }

        #endregion

        #region 定义"确定"事件

        /// <summary>
        /// 定义"确定"事件
        /// </summary>
        private event EventHandler<TreeNodeEditEventArgs> _OnConfirmClick;

        /// <summary>
        /// 定义"确定"事件访问器
        /// </summary>
        [
        Description(@"点击""确定""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeEditEventArgs> OnConfirmClick
        {
            add
            {
                _OnConfirmClick += value;
            }
            remove
            {
                _OnConfirmClick -= value;
            }
        }

        /// <summary>
        /// 定义"确定"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void ConfirmClick(TreeNodeEditEventArgs e)
        {
            if (_OnConfirmClick != null) _OnConfirmClick(this, e);
        }

        #endregion

        #region 定义"取消"事件

        /// <summary>
        /// 定义"取消"事件
        /// </summary>
        private event EventHandler<TreeNodeEditEventArgs> _OnCancelClick;

        /// <summary>
        /// 定义"取消"事件访问器
        /// </summary>
        [
        Description(@"点击""编辑""按钮时发生"),
        Category("自定义事件"),
        DefaultValue(""),
        ]
        public event EventHandler<TreeNodeEditEventArgs> OnCancelClick
        {
            add
            {
                _OnCancelClick += value;
            }
            remove
            {
                _OnCancelClick -= value;
            }
        }

        /// <summary>
        /// 定义"编辑"事件引发事件的方法
        /// </summary>
        /// <param name="e"></param>
        private void CancelClick(TreeNodeEditEventArgs e)
        {
            if (_OnCancelClick != null) _OnCancelClick(this, e);
        }

        #endregion

        #endregion

        #region 窗体和控件的方法

        /// <summary>
        /// 构造函数
        /// </summary>
        public TreeLayerForm()
        {
            InitializeComponent();
            progressPanel.Hide();
            treeViewHandler = new TreeViewHandler<TreeNode>(trvLayer);
        }

        /// <summary>
        /// 窗体加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeLayerForm_Load(object sender, EventArgs e)
        {
            if (!DesignMode)
            {
                InitFirstLevelNodes();
            }
        }

        /// <summary>
        /// 在选择一个节点前执行的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvLayer_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            BeforeSelectOfTreeView(e);
        }

        /// <summary>
        /// 当选择一个节点后，将节点的详细展现出来
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvLayer_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node != null)
            {
                CommonNode commonNode = e.Node.Tag as CommonNode;
                TreeNodeShow.SetModelInfo(commonNode);                
                if (_currentQueriedState)
                {
                    SetActiveStatesOfControls(MenuItemState.AllowToEdit);
                }
                else
                {
                    currentEditState = EditState.None;
                    if (commonNode.NodeId == 0)
                    {
                        SetActiveStatesOfControls(MenuItemState.Disabled);
                    }
                    else if (DataConvertionHelper.IsNullValue(commonNode.ParentNodeId) && (DataConvertionHelper.IsNullValue(commonNode.NodeId) || (!allowedToFirstLevelNode && e.Node.Parent == null)))
                    {
                        SetActiveStatesOfControls(MenuItemState.AllowToAdd);
                    }
                    else
                    {
                        SetActiveStatesOfControls(MenuItemState.Init);
                    }
                }           
            }
            else
            {
                TreeNodeShow.ClearModelInfo();
            }
            AfterSelectOfTreeView(e);
        }

        /// <summary>
        /// 展开节点前
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvLayer_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            BeforeTreeNodeExpand(e);
        }

        /// <summary>
        /// 展开节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvLayer_AfterExpand(object sender, TreeViewEventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                if (e.Node.IsExpanded)
                {
                    e.Node.ImageIndex = 1;
                }
                else
                {
                    e.Node.ImageIndex = 0;
                }
                if (CommonNodeContract != null && e.Node.Nodes.Count == 1)
                {
                    CommonNode childNode = e.Node.Nodes[0].Tag as CommonNode;
                    if ((childNode != null) && (DataConvertionHelper.IsNullValue(childNode.NodeId)))
                    {                        
                        IList<CommonNode> commonNodes = null;
                        bool result = ContainsNodeType(e.Node.Level + 1);
                        CommonNode commonNode = e.Node.Tag as CommonNode;
                        if (result && commonNode.NodeType > 0)
                        {
                            commonNodes = GetChildNodesWithType(e.Node);
                        }
                        else
                        {
                            commonNodes = GetChildNodes(e.Node);
                        }
                        LoadPartialNodes(e.Node, commonNodes);
                    }
                }
                else
                {
                    if (e.Node.Nodes.Count == 1)
                    {
                        CommonNode commonNode = e.Node.Nodes[0].Tag as CommonNode;
                        if (commonNode == null || DataConvertionHelper.IsNullValue(commonNode.NodeId))
                        {
                            e.Node.Nodes.Clear();
                        }
                    }
                }
                AfterTreeNodeExpand(e);
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
            ShowNumOfChildNodes(e.Node, chkShow.Checked);
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCreate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {            
            Create(e);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiEdit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeNodeItemClickEventArgs treeNodeItemClickEventArgs = new TreeNodeItemClickEventArgs(trvLayer.SelectedNode, e.Item, e.Link);
            BeforeEditClick(treeNodeItemClickEventArgs);
            if (!treeNodeItemClickEventArgs.Cancel)
            {
                Edit(e);
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiDelete_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeNodeItemClickEventArgs treeNodeItemClickEventArgs = new TreeNodeItemClickEventArgs(trvLayer.SelectedNode, e.Item, e.Link);
            BeforeDeleteClick(treeNodeItemClickEventArgs);
            if (!treeNodeItemClickEventArgs.Cancel)
            {
                DeleteNode(e);
            }
        }

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiSetting_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (trvLayer.SelectedNode != null)
            {
                TreeNodeItemClickEventArgs treeNodeItemClickEventArgs = new TreeNodeItemClickEventArgs(trvLayer.SelectedNode, e.Item, e.Link);
                SettingClick(treeNodeItemClickEventArgs);
            }
            else
            {
                MessageBox.Show("请先选中一个节点。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 自定义项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiCustomItem_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (trvLayer.SelectedNode != null)
            {
                TreeNodeItemClickEventArgs treeNodeItemClickEventArgs = new TreeNodeItemClickEventArgs(trvLayer.SelectedNode, e.Item, e.Link);
                CustomItemClick(treeNodeItemClickEventArgs);
            }
            else
            {
                MessageBox.Show("请先选中一个节点。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 导入导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bbiExchange_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (trvLayer.SelectedNode != null)
            {
                TreeNodeItemClickEventArgs treeNodeItemClickEventArgs = new TreeNodeItemClickEventArgs(trvLayer.SelectedNode, e.Item, e.Link);
                ExchangeClick(treeNodeItemClickEventArgs);
            }
            else
            {
                MessageBox.Show("请先选中一个节点后进行导入导出操作。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }                

        /// <summary>
        /// 收拢节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvLayer_AfterCollapse(object sender, TreeViewEventArgs e)
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
        /// 节点双击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvLayer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            NodeMouseDoubleClick(e);
        }

        /// <summary>
        /// 显示子节点数目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            ShowNumOfChildNodes(chkShow.Checked);
            Cursor = Cursors.Default;
        }

        /// <summary>
        /// 右键菜单：创建
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbbiCreate_ItemClick(object sender, ItemClickEventArgs e)
        {
            Create(e);
        }

        /// <summary>
        /// 右键菜单：编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbbiEdit_ItemClick(object sender, ItemClickEventArgs e)
        {
            Edit(e);
        }

        /// <summary>
        /// 右键菜单：删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbbiDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            DeleteNode(e);
        }

        /// <summary>
        /// 右键菜单：置顶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbbiTop_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Top);
        }

        /// <summary>
        /// 右键菜单：上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbbiPrevious_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Previous);
        }

        /// <summary>
        /// 右键菜单：下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbbiNext_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Next);
        }

        /// <summary>
        /// 右键菜单：置底
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pbbiBottom_ItemClick(object sender, ItemClickEventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Bottom);
        }

        /// <summary>
        /// 置顶操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTop_Click(object sender, EventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Top);
        }

        /// <summary>
        /// 上移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Previous);
        }

        /// <summary>
        /// 下移
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Next);
        }

        /// <summary>
        /// 置底
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBottom_Click(object sender, EventArgs e)
        {
            UpdateNodeSorting(MovedDriection.Bottom);
        }

        /// <summary>
        /// 确定操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            try
            {
                if (trvLayer.SelectedNode == null)
                {
                    MessageBox.Show("请先选中一个节点。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CommonNode commonNode = trvLayer.SelectedNode.Tag as CommonNode;
                switch (currentEditState)
                {
                    case EditState.Add:
                        if (trvLayer.SelectedNode.Level > MaxLevel)
                        {
                            Cursor = Cursors.Default;
                            MessageBox.Show(string.Format("不允许增加子节点, 超过了允许增加节点层数[{0}]", MaxLevel + 1), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }                        
                        break;

                    case EditState.Edit:
                        /* 比如单位管理的根节点 */
                        if (!_currentQueriedState && DataConvertionHelper.IsNullValue(commonNode.ParentNodeId) && (DataConvertionHelper.IsNullValue(commonNode.NodeId) || (!allowedToFirstLevelNode && trvLayer.SelectedNode.Parent == null)))
                        {
                            MessageBox.Show("不能编辑根节点。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        break;

                    default:
                        break;
                }
                TreeNodeEditEventArgs arg = new TreeNodeEditEventArgs(trvLayer.SelectedNode, currentEditState);
                progressPanel.Show();
                ConfirmClick(arg);
                if (!arg.Cancel)
                {
                    currentEditState = EditState.None;
                    if (_currentQueriedState)
                    {
                        SetActiveStatesOfControls(MenuItemState.AllowToEdit);
                    }
                    else
                    { 
                        SetActiveStatesOfControls(MenuItemState.Init);
                    }
                }
                progressPanel.Hide();
            }
            catch (Exception exception)
            {
                progressPanel.Hide();
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 取消操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelClick(new TreeNodeEditEventArgs(trvLayer.SelectedNode, currentEditState));
            CommonNode commonNode = trvLayer.SelectedNode.Tag as CommonNode;
            TreeNodeShow.SetModelInfo(commonNode);          
            currentEditState = EditState.None;
            if (_currentQueriedState)
            {
                SetActiveStatesOfControls(MenuItemState.AllowToEdit);
            }
            else
            {
                if (allowedToFirstLevelNode)
                {
                    SetActiveStatesOfControls(MenuItemState.Init);
                }
                else
                {
                    SetActiveStatesOfControls(MenuItemState.AllowToAdd);
                }                
            }          
        }

        /// <summary>
        /// 拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvLayer_DragDrop(object sender, DragEventArgs e)
        {            

            //获得拖放中的节点  
            TreeNode moveNode = (TreeNode)e.Data.GetData("System.Windows.Forms.TreeNode");
            //根据鼠标坐标确定要移动到的目标节点  
            Point pt;
            TreeNode targeNode;
            pt = ((TreeView)(sender)).PointToClient(new Point(e.X, e.Y));
            targeNode = this.trvLayer.GetNodeAt(pt);
            if (targeNode != null && moveNode!= null)
            {
                CommonNode parentCommonNode = targeNode.Tag as CommonNode;
                CommonNode commonNode = moveNode.Tag as CommonNode;
                bool allowedDrop = IsAllowedDrag(moveNode, targeNode);
                /* 根节点不允许拖动 */
                if (allowedDrop && commonNode.NodeId > 0)
                {
                    if (MessageBox.Show(string.Format("确定将‘{0}’节点拖放到‘{1}’节点下吗？", moveNode.Text, targeNode.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        //展开目标节点,便于显示拖放效果  
                        targeNode.Expand();

                        string newNodeCode = GetDefaultNodeCode(targeNode);
                        CommonNodeContract.UpdateParentNodeId(commonNode.NodeId, parentCommonNode.NodeId, newNodeCode);
                        TreeNode newMoveNode = (TreeNode)moveNode.Clone();
                        commonNode.NodeCode = newNodeCode;
                        newMoveNode.Tag = commonNode;
                        targeNode.Nodes.Add(newMoveNode);
                        //更新当前拖动的节点选择  
                        trvLayer.SelectedNode = newMoveNode;
                        //移除拖放的节点  
                        moveNode.Remove();
                    }
                }
            }
        }

        /// <summary>
        /// 拖进
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvLayer_DragEnter(object sender, DragEventArgs e)
        {            
            if (e.Data.GetDataPresent("System.Windows.Forms.TreeNode"))
            {
                e.Effect = DragDropEffects.Move;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        /// <summary>
        /// 节点拖放
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvLayer_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                DoDragDrop(e.Item, DragDropEffects.Move);
            }
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scQuery_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            string condition = scQuery.Text.Trim();
            Query(condition);
        }

        /// <summary>
        /// 回车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scQuery_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                string condition = scQuery.Text.Trim();
                Query(condition);
            }
        }

        /// <summary>
        /// 查询条件清空则恢复
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scQuery_EditValueChanged(object sender, EventArgs e)
        {
            string condition = scQuery.Text.Trim();
            if (string.IsNullOrWhiteSpace(condition))
            {
                currentEditState = EditState.None;
                if (_currentQueriedState)
                {                    
                    InitFirstLevelNodes();
                }                
                SetActiveStatesOfControls(MenuItemState.Init);
                _currentQueriedState = false;
            }
        }

        /// <summary>
        /// 节点悬停
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void trvLayer_NodeMouseHover(object sender, TreeNodeMouseHoverEventArgs e)
        {
            CommonNode commonNode = e.Node.Tag as CommonNode;
            /* 主要处理查询后的节点的父节点关系 */
            if (CurrentQueriedState && (commonNode != null) && commonNode.NodeId > 0 
                && !DataConvertionHelper.IsNullValue(commonNode.ParentNodeId) && (e.Node.Level == 0))
            {
                if (string.IsNullOrWhiteSpace(e.Node.ToolTipText))
                {
                    e.Node.ToolTipText = GetToolTipText(commonNode);                 
                }
            }
        }

        #endregion

        #region  虚拟方法        

        /// <summary>
        /// 初始化属性节点
        /// </summary>
        protected virtual void InitFirstLevelNodes()
        {
            if (CommonNodeContract != null)
            {
                IList<CommonNode> commonNodes = CommonNodeContract.GetChildNodes(decimal.MinValue);
                InitTreeNodes(commonNodes);
            }
        }

        /// <summary>
        /// 获得子列表节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual IList<CommonNode> GetChildNodesWithType(TreeNode node)
        {
            IList<CommonNode> commonNodes = null;

            if (CommonNodeContract != null)
            {
                CommonNode commonNode = node.Tag as CommonNode;
                commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId, commonNode.NodeType);
            }

            return commonNodes;
        }

        /// <summary>
        /// 获得子列表节点
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        protected virtual IList<CommonNode> GetChildNodes(TreeNode node)
        {
            IList<CommonNode> commonNodes = null;

            if (CommonNodeContract != null)
            {
                CommonNode commonNode = node.Tag as CommonNode;
                commonNodes = CommonNodeContract.GetChildNodes(commonNode.NodeId);
            }

            return commonNodes;
        }

        /// <summary>
        /// 是否允许节点被拖动
        /// </summary>
        /// <param name="movedTreeNode"></param>
        /// <param name="targeNode"></param>
        /// <returns></returns>
        protected virtual bool IsAllowedDrag(TreeNode movedTreeNode, TreeNode targeNode)
        {
            /* 默认根节点不允许拖动 */
            if (movedTreeNode.Parent != null && targeNode != null)
            {
                return true;
            }

            return false;            
        }

        /// <summary>
        /// 获得节点的祖先节点信息
        /// </summary>
        /// <param name="commonNode"></param>
        /// <returns></returns>
        protected virtual string GetToolTipText(CommonNode commonNode)
        {
            IList<string> names = CommonNodeContract.GetHierarchicalNamesOfNode(commonNode.NodeId);
            StringBuilder sb = new StringBuilder();
            foreach (string name in names)
            {
                sb.AppendFormat("[{0}]", name);
            }

            return sb.ToString();
        }



        /// <summary>
        /// 通过节点的索引值来对节点的位置进行调整
        /// </summary>
        /// <param name="index">节点索引值</param>
        protected virtual void MoveNodeByIndex(int index)
        {
            TreeNode tnSelected = trvLayer.SelectedNode;
            if (trvLayer.SelectedNode.Parent != null)
            {
                TreeNode tnParent = trvLayer.SelectedNode.Parent;
                tnSelected.Remove();                
                tnParent.Nodes.Insert(index, tnSelected);
            }
            else
            {
                tnSelected.Remove();
                trvLayer.Nodes.Insert(index, tnSelected);
            }
            trvLayer.Focus();
            trvLayer.SelectedNode = tnSelected;
        }

        #endregion

        #region  私有方法

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="condition"></param>
        private void Query(string condition)
        {
            _currentQueriedState = true;
            currentEditState = EditState.None;
            BeoreQueryClick(null);
            if (!string.IsNullOrWhiteSpace(condition))
            {
                IList<CommonNode> commonNodes = CommonNodeContract.GetChildNodes(string.Format("%{0}%", condition));
                foreach (CommonNode commonNode in commonNodes)
                {
                    commonNode.IsLeaf = true;
                }
                treeViewHandler.InitFirstLevelNodes(commonNodes);
                if (trvLayer.Nodes.Count > 0)
                {
                    trvLayer.SelectedNode = trvLayer.Nodes[0];
                    if (trvLayer.Nodes.Count <= 5)
                    {
                        foreach (TreeNode tn in trvLayer.Nodes)
                        {
                            CommonNode commonNode = tn.Tag as CommonNode;
                            tn.ToolTipText = GetToolTipText(commonNode);                            
                        }
                    }
                }
                SetActiveStatesOfControls(MenuItemState.AllowToEdit);                
            }
            else
            {
                InitFirstLevelNodes();          
                SetActiveStatesOfControls(MenuItemState.Init);                
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="e"></param>
        private void Create(ItemClickEventArgs e)
        {
            TreeNodeItemClickEventArgs treeNodeItemClickEventArgs = new TreeNodeItemClickEventArgs(trvLayer.SelectedNode, e.Item, e.Link);
            BeoreCreatedClick(treeNodeItemClickEventArgs);
                        
            TreeNodeShow.ClearModelInfo();
            TreeNodeShow.DefaultCode = GetDefaultNodeCode(trvLayer.SelectedNode);
            currentEditState = EditState.Add;
            SetActiveStatesOfControls(MenuItemState.AllowToAdd);
            CreateClick(treeNodeItemClickEventArgs);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="e"></param>
        private void Edit(ItemClickEventArgs e)
        {
            currentEditState = EditState.Edit;
            SetActiveStatesOfControls(MenuItemState.AllowToEdit);
            
            TreeNodeItemClickEventArgs treeNodeItemClickEventArgs = new TreeNodeItemClickEventArgs(trvLayer.SelectedNode, e.Item, e.Link);
            EditClick(treeNodeItemClickEventArgs);
        }

        /// <summary>
        /// 删除节点操作
        /// </summary>
        /// <param name="e"></param>
        private void DeleteNode(ItemClickEventArgs e)
        {
            try
            {
                if (trvLayer.SelectedNode != null)
                {                   
                    CommonNode commonNode = trvLayer.SelectedNode.Tag as CommonNode;
                    if ((commonNode != null) && (commonNode.NodeId > 0))
                    {
                        Cursor = Cursors.WaitCursor;
                        if (MessageBox.Show(string.Format("确定删除所选择的节点[{0}]吗？", trvLayer.SelectedNode.Text), "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                        {
                            /* 1.检查是否存在子节点，不允许删除有子节点的节点 */                           
                            if (!_allowChildNodesDeleted && trvLayer.SelectedNode.Nodes.Count > 0)
                            {
                                Cursor = Cursors.Default;
                                MessageBox.Show(string.Format("该节点[{0}]下共有{1}的子节点，请先删除这些子节点。",
                                    trvLayer.SelectedNode.Text, trvLayer.SelectedNode.Nodes.Count), "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            /* 2. 删除节点的数据、节点自身并设置相关属性 */
                            TreeNodeItemClickEventArgs treeNodeItemClickEventArgs = new TreeNodeItemClickEventArgs(trvLayer.SelectedNode, e.Item, e.Link);
                            DeleteClick(treeNodeItemClickEventArgs);
                        }
                        Cursor = Cursors.Default;
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("不能删除根节点。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 抛出异常, 不包装异常 
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 检查创建按钮的状态
        /// </summary>
        private void CheckCreateState()
        {
            if (_maxLevel > 0 && (trvLayer.SelectedNode != null) && (trvLayer.SelectedNode.Level >= _maxLevel))
            {
                pbbiCreate.Enabled = false;
                bbiCreate.Enabled = false;
                bbiCreate.Description = string.Format("树形结构的层级不能超过{0}层", _maxLevel + 1);
            }
            else
            {
                bbiCreate.Description = "新建";
            }
        }

        /// <summary>
        /// 显示下一级节点的数目
        /// </summary>
        /// <param name="show">显示或是关闭</param>
        private void ShowNumOfChildNodes(bool show)
        {
            foreach (TreeNode tn in trvLayer.Nodes)
            {
                ShowNumOfChildNodes(tn, show);
            }
        }

        /// <summary>
        /// 显示下一级节点的数目 
        /// </summary>
        /// <param name="tn"></param>
        /// <param name="show">显示或是关闭</param>
        private void ShowNumOfChildNodes(TreeNode tn, bool show)
        {
            int pos = tn.Text.LastIndexOf('[');
            string text = string.Empty;
            if (pos > 0)
            {
                text = tn.Text.Substring(0, pos);
            }
            else
            {
                text = tn.Text;
            }
            if (show)
            {
                if (tn.Nodes.Count > 0)
                {
                    CommonNode commonNode = (CommonNode)tn.Nodes[0].Tag;
                    if (commonNode.NodeId > 0)
                    {
                        tn.Text = string.Format("{0}[{1}]", text, tn.Nodes.Count);
                    }
                    else
                    {
                        tn.Text = string.Format("{0}[未展开]", text);
                    }
                }
                else
                {
                    tn.Text = text;
                }
            }
            else
            {
                tn.Text = text;
            }
            foreach (TreeNode tnChild in tn.Nodes)
            {
                ShowNumOfChildNodes(tnChild, show);
            }
        }

        /// <summary>
        /// 移动节点
        /// </summary>
        /// <param name="movedDriectionOfNode">节点移动的方向</param>
        private void MoveNode(MovedDriection movedDriectionOfNode)
        {
            switch (movedDriectionOfNode)
            {
                case MovedDriection.Top:
                    MoveNodeByIndex(0);
                    break;

                case MovedDriection.Next:
                    if (trvLayer.SelectedNode.NextNode != null)
                    {
                        MoveNodeByIndex(trvLayer.SelectedNode.NextNode.Index);
                    }
                    break;

                case MovedDriection.Previous:
                    if (trvLayer.SelectedNode.PrevNode != null)
                    {
                        MoveNodeByIndex(trvLayer.SelectedNode.PrevNode.Index);
                    }
                    break;

                case MovedDriection.Bottom:
                    int lastIndex = 0;
                    if (trvLayer.SelectedNode.Parent != null)
                    {
                        lastIndex = trvLayer.SelectedNode.Parent.Nodes.Count - 1;
                    }
                    else
                    {
                        lastIndex = trvLayer.Nodes.Count - 1;
                    }
                    MoveNodeByIndex(lastIndex);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// 禁止移动节点的按钮的状态
        /// </summary>
        private void SetDisabledStatesOfMoveButtons()
        {
            btnTop.Enabled = false;
            btnPrevious.Enabled = false;
            btnNext.Enabled = false;
            btnBottom.Enabled = false;
            pbbiTop.Enabled = false;
            pbbiPrevious.Enabled = false;
            pbbiNext.Enabled = false;
            pbbiBottom.Enabled = false;
        }

        /// <summary>
        /// 设置移动节点的按钮的状态
        /// </summary>
        private void SetActiveStatesOfMoveButtons()
        {
            if (trvLayer.SelectedNode != null)
            {
                if (trvLayer.SelectedNode.PrevNode != null)
                {
                    btnTop.Enabled = true;
                    btnPrevious.Enabled = true;
                    pbbiTop.Enabled = true;
                    pbbiPrevious.Enabled = true;

                }
                else
                {
                    btnTop.Enabled = false;
                    btnPrevious.Enabled = false;
                    pbbiTop.Enabled = false;
                    pbbiPrevious.Enabled = false;
                }
                if (trvLayer.SelectedNode.NextNode != null)
                {
                    btnNext.Enabled = true;
                    btnBottom.Enabled = true;
                    pbbiNext.Enabled = true;
                    pbbiBottom.Enabled = true;
                }
                else
                {
                    btnNext.Enabled = false;
                    btnBottom.Enabled = false;
                    pbbiNext.Enabled = false;
                    pbbiBottom.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="movedDriection"></param>
        private void UpdateNodeSorting(MovedDriection movedDriection)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                if (trvLayer.SelectedNode == null)
                {
                    return;
                }
                CommonNode commonNode = trvLayer.SelectedNode.Tag as CommonNode;
                if (commonNode == null || commonNode.NodeId <= 0)
                {
                    return;
                }
                decimal otherNodeId = 0;
                if (movedDriection == MovedDriection.Previous)
                {
                    CommonNode previousNode = trvLayer.SelectedNode.PrevNode.Tag as CommonNode;
                    otherNodeId = previousNode.NodeId;
                }
                else if (movedDriection == MovedDriection.Next)
                {
                    CommonNode nextNode = trvLayer.SelectedNode.NextNode.Tag as CommonNode;
                    otherNodeId = nextNode.NodeId;
                }
                switch (movedDriection)
                {
                    case MovedDriection.Top:
                        CommonNodeContract.UpdateSorting(commonNode.NodeId, 0, movedDriection);
                        break;

                    case MovedDriection.Previous:
                        CommonNodeContract.UpdateSorting(commonNode.NodeId, otherNodeId, movedDriection);
                        break;

                    case MovedDriection.Next:
                        CommonNodeContract.UpdateSorting(commonNode.NodeId, otherNodeId, movedDriection);
                        break;

                    case MovedDriection.Bottom:
                        CommonNodeContract.UpdateSorting(commonNode.NodeId, 0, movedDriection);
                        break;
                }
                int previousNodeIndex = trvLayer.SelectedNode.Index;
                MoveNode(movedDriection);
                MovedNodeClick(new MovedNodeEventArgs(trvLayer.SelectedNode, previousNodeIndex, movedDriection));
                Cursor = Cursors.Default;
            }
            catch (Exception exception)
            {
                Cursor = Cursors.Default;
                //记录日志, 不抛出异常, 包装异常
                WinExceptionHelper.NoExceptionAndAlertPolicyWithLog(exception);
            }
        }

        /// <summary>
        /// 获得默认的编码
        /// </summary>
        /// <param name="treeNode"></param>
        /// <returns></returns>
        public string GetDefaultNodeCode(TreeNode treeNode)
        {
            string nodeCode = string.Empty;

            CommonNode commonNode = treeNode.Tag as CommonNode;
            IList<string> childNodeCodes = null;
            if (ContainsNodeType(treeNode.Level + 1) && commonNode.NodeType > 0)
            {
                childNodeCodes = CommonNodeContract.GetChildNodeCodes(commonNode.NodeId, commonNode.NodeType);
            }
            else
            {
                childNodeCodes = CommonNodeContract.GetChildNodeCodes(commonNode.NodeId);
            }

            int index = 1;
            do
            {
                nodeCode = DataFieldHelper.GetNewCode(commonNode.NodeCode, index);                
                index++;
            } while (childNodeCodes.Contains(nodeCode));

            return nodeCode;
        }

        #endregion

        #region  受保护的方法

        /// <summary>
        /// 显示
        /// </summary>
        protected void ShowProgressPanel()
        {
            progressPanel.Show();
        }

        /// <summary>
        /// 隐藏
        /// </summary>
        protected void HideProgressPanel()
        {
            progressPanel.Hide();
        }

        /// <summary>
        /// 加载节点
        /// </summary>
        /// <param name="node"></param>
        /// <param name="commonNodes"></param>
        protected void LoadPartialNodes(TreeNode node, IList<CommonNode> commonNodes)
        {
            treeViewHandler.LoadPartialNodes(node, commonNodes);
        }

        /// <summary>
        /// 增加节点操作
        /// </summary>
        /// <param name="tn">新增节点</param>
        protected void AddNode(TreeNode tn)
        {
            if (tn != null)
            {
                if (!trvLayer.SelectedNode.IsExpanded && trvLayer.SelectedNode.Nodes.Count == 1 &&　string.IsNullOrWhiteSpace(trvLayer.SelectedNode.Nodes[0].Text))
                {
                    trvLayer.SelectedNode.Expand();
                    return;
                }
                if (trvLayer.SelectedNode != null)
                {
                    trvLayer.SelectedNode.Nodes.Add(tn);
                }
                else
                {
                    trvLayer.Nodes.Add(tn);
                }
                tn.ImageIndex = 0;
                if (chkReturn.Checked)
                {
                    if (!trvLayer.SelectedNode.IsExpanded)
                    {
                        trvLayer.SelectedNode.Expand();
                    }
                }
                else
                {
                    trvLayer.SelectedNode = tn;
                }
                trvLayer.Focus();
            }
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        /// <param name="nodeName">节点名称</param>
        protected void ModifyNode(string nodeName)
        {
            if (trvLayer.SelectedNode != null)
            {
                if (trvLayer.SelectedNode.Text.EndsWith("]"))
                {
                    int pos = trvLayer.SelectedNode.Text.LastIndexOf('[');
                    if (pos > 0)
                    {
                        nodeName = string.Format("{0}{1}", nodeName, trvLayer.SelectedNode.Text.Substring(pos));
                    }
                }
                trvLayer.SelectedNode.Text = nodeName;
            }
        }
        
        /// <summary>
        /// 初始化树形节点
        /// </summary>
        /// <param name="commonNodes"></param>
        protected void InitTreeNodes(IList<CommonNode> commonNodes)
        {
            treeViewHandler.InitFirstLevelNodes(commonNodes);
            if (trvLayer.Nodes.Count > 0)
            {
                trvLayer.SelectedNode = trvLayer.Nodes[0];
            }  
        }
        
        /// <summary>
        /// 增加右侧控件
        /// </summary>
        /// <param name="userControl"></param>
        protected void AddControls(UserControl userControl)
        {
            userControl.Dock = DockStyle.Fill;
            pnlDetail.Controls.Add(userControl);
        }

        /// <summary>
        /// 增加右侧控件
        /// </summary>
        /// <param name="userControls"></param>
        protected void AddControls(IList<UserControl> userControls)
        {
            foreach (UserControl userControl in userControls)
            {
                userControl.Dock = DockStyle.Fill;
                pnlDetail.Controls.Add(userControl);
            } 
        }

        /// <summary>
        /// 删除节点操作
        /// </summary>
        protected void DeleteNode()
        {
            if (trvLayer.SelectedNode != null)
            {               
                TreeNode tn = trvLayer.SelectedNode;
                if (tn.NextNode != null)
                {
                    trvLayer.SelectedNode = tn.NextNode;
                }
                else
                {
                    if (tn.PrevNode != null)
                    {
                        trvLayer.SelectedNode = tn.PrevNode;
                    }
                    else
                    {
                        if (tn.Parent != null)
                        {
                            trvLayer.SelectedNode = tn.Parent;
                        }
                    }
                }
                tn.Remove();
            }
        }

        /// <summary>
        /// 设置与树形控件相关的按钮的状态
        /// </summary>
        /// <param name="menuItemState">编辑状态</param>
        protected void SetActiveStatesOfControls(MenuItemState menuItemState)
        {
            switch (menuItemState)
            {
                case MenuItemState.Init:
                    if (trvLayer.SelectedNode != null && CommonNodeContract != null)
                    {
                        SetActiveStatesOfMoveButtons();
                    }
                    else
                    {
                        SetDisabledStatesOfMoveButtons();
                    }
                    bbiCreate.Enabled = true;
                    bbiEdit.Enabled = true;
                    bbiDelete.Enabled = true;
                    pbbiCreate.Enabled = true;
                    pbbiEdit.Enabled = true;
                    pbbiDelete.Enabled = true;
                    btnConfirm.Enabled = false;
                    btnCancel.Enabled = false;
                    TreeNodeShow.SetActiveStatesOfControls(true);
                    break;

                case MenuItemState.AllowToAdd:
                    SetDisabledStatesOfMoveButtons();
                    bbiCreate.Enabled = true;
                    bbiEdit.Enabled = false;
                    bbiDelete.Enabled = false;
                    pbbiCreate.Enabled = true;
                    pbbiEdit.Enabled = false;
                    pbbiDelete.Enabled = false;
                    if (currentEditState == EditState.Add)
                    {
                        btnConfirm.Enabled = true;
                        btnCancel.Enabled = true;
                        TreeNodeShow.SetActiveStatesOfControls(false);
                    }
                    else
                    {
                        btnConfirm.Enabled = false;
                        btnCancel.Enabled = false;
                        TreeNodeShow.SetActiveStatesOfControls(true);
                    }
                    break;

                case MenuItemState.AllowToEdit:
                    SetDisabledStatesOfMoveButtons();
                    bbiCreate.Enabled = false;
                    bbiEdit.Enabled = true;
                    bbiDelete.Enabled = false;
                    pbbiCreate.Enabled = false;
                    pbbiEdit.Enabled = true;
                    pbbiDelete.Enabled = false;
                    if (currentEditState == EditState.Edit)
                    {
                        btnConfirm.Enabled = true;
                        btnCancel.Enabled = true;
                        TreeNodeShow.SetActiveStatesOfControls(false);
                    }
                    else
                    {
                        btnConfirm.Enabled = false;
                        btnCancel.Enabled = false;
                        TreeNodeShow.SetActiveStatesOfControls(true);
                    }
                    break;

                case MenuItemState.Disabled:
                    SetDisabledStatesOfMoveButtons();
                    bbiCreate.Enabled = false;
                    bbiEdit.Enabled = false;
                    bbiDelete.Enabled = false;
                    pbbiCreate.Enabled = false;
                    pbbiEdit.Enabled = false;
                    pbbiDelete.Enabled = false;
                    btnConfirm.Enabled = false;
                    btnCancel.Enabled = false;
                    TreeNodeShow.SetActiveStatesOfControls(true);
                    break;

                default:
                    break;
            }
            CheckCreateState();
        }

        /// <summary>
        /// 该层节点在加载时是否需要使用节点类型条件
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        protected virtual bool ContainsNodeType(int level)
        {
            return true;
        }
       
        #endregion


    }
}
