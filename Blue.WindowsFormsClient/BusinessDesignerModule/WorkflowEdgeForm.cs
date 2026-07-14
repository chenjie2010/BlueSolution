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
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.WCFLibrary;
using AppFramework.WinFormsControls;
using AppFramework.WinFormsLibrary.Utility;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class WorkflowEdgeForm : Form
    {
        #region 私有变量
        #endregion

        #region 契约接口

        private readonly ICustomWorkflowProcessContract customWorkflowProcessContract;

        #endregion

        #region 属性

        /// <summary>
        /// 工作流编号
        /// </summary>
        public decimal WorkflowId
        {
            get;
            set;
        }

        /// <summary>
        /// 父节点编号
        /// </summary>
        public decimal ParentProcessId
        {
            get;
            set;
        }

        /// <summary>
        /// 子节点编号
        /// </summary>
        public decimal ProcessId
        {
            get;
            set;
        }

        /// <summary>
        /// 设置工作流属性
        /// </summary>
        public SetWorkflowProcessDelegate SetWorkflowProcess
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public WorkflowEdgeForm()
        {
            InitializeComponent();
            customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
            UserControlHelper.InitImageComboBoxEdit(icmbNodeRelationship, typeof(NodeRelationship));            
            ParentProcessId = 0;
            ProcessId = 0;
        }

        #endregion  

        #region 窗体和控件方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkflowEdgeForm_Load(object sender, EventArgs e)
        {
            if (ParentProcessId > 0 && ProcessId > 0)
            {
                beParentNode.Text = customWorkflowProcessContract.GetFullName(ParentProcessId);
                beParentNode.Tag = ParentProcessId;
                beNode.Text = customWorkflowProcessContract.GetFullName(ProcessId);
                beNode.Tag = ProcessId;
            }
        }

        /// <summary>
        /// 父节点选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beParentNode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            NodeRelationship nodeRelationship = (NodeRelationship)Convert.ToByte(icmbNodeRelationship.EditValue);
            if (nodeRelationship == NodeRelationship.ManyToOne)
            {
                SetNode(beParentNode, true);
            }
            else
            {
                SetNode(beParentNode, false);
            }
        }
        
        /// <summary>
        /// 子节点选项
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void beNode_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            NodeRelationship nodeRelationship = (NodeRelationship)Convert.ToByte(icmbNodeRelationship.EditValue);
            if (nodeRelationship == NodeRelationship.OneToMany)
            {
                SetNode(beNode, true);
            }
            else
            {
                SetNode(beNode, false);
            }
        }

        /// <summary>
        /// 单击确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (beParentNode.Tag != null && beNode.Tag != null)
            {
                CommonNode parentCommonNode = null;
                CommonNode childCommonNode = null;
                IList <KeyValueItem> processRelationship = new List<KeyValueItem>();
                NodeRelationship nodeRelationship = (NodeRelationship)Convert.ToByte(icmbNodeRelationship.EditValue);
                switch (nodeRelationship)
                {
                    case NodeRelationship.OneToOne:
                        parentCommonNode = (CommonNode)beParentNode.Tag;
                        childCommonNode = (CommonNode)beNode.Tag;
                        WorkflowProcessType workflowProcessType = (WorkflowProcessType)parentCommonNode.NodeType;
                        if ((workflowProcessType == WorkflowProcessType.DynamicBranchInDeps || workflowProcessType == WorkflowProcessType.DynamicBranchBetweenDeps
                            || workflowProcessType == WorkflowProcessType.DynamicBranchInDeps) && (WorkflowProcessType)childCommonNode.NodeType != WorkflowProcessType.SingleBranch)
                        {
                            MessageBox.Show("父节点的流向类型为选动态节点，则子节点的流向类型必须为选单节点。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        processRelationship.Add(new KeyValueItem(parentCommonNode.NodeId, childCommonNode.NodeId));
                        break;

                    case NodeRelationship.OneToMany:
                        parentCommonNode = (CommonNode)beParentNode.Tag;
                        IList<CommonNode> nodes = beNode.Tag as IList<CommonNode>;
                        foreach (CommonNode commonNode in nodes)
                        {
                            processRelationship.Add(new KeyValueItem(parentCommonNode.NodeId, commonNode.NodeId));
                        }
                        break;

                    case NodeRelationship.ManyToOne:
                        childCommonNode = (CommonNode)beNode.Tag;
                        IList<CommonNode> parentNodes = beParentNode.Tag as IList<CommonNode>;
                        foreach (CommonNode commonNode in parentNodes)
                        {
                            processRelationship.Add(new KeyValueItem(commonNode.NodeId, childCommonNode.NodeId));
                        }
                        int count = parentNodes != null ? parentNodes.Count : 0;
                        if (count > 1)
                        {
                            for (int idx = 1; idx < count; idx++)
                            {
                                if (parentNodes[idx].NodeType != parentNodes[0].NodeType)
                                {
                                    MessageBox.Show("子节点的多个父节点的类型必须一致。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                            if (parentNodes[0].NodeType == (byte)WorkflowProcessType.SelectiveBranch)
                            {
                                MessageBox.Show("子节点不能拥有多个流向类型为选择节点类型的父节点。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        break;
                }
                foreach (KeyValueItem keyValueItem in processRelationship)
                {
                    bool existed = FindCircleExisted(keyValueItem.Key, keyValueItem.Value);
                    if (existed)
                    {
                        MessageBox.Show("子节点存在闭环现象，请重新设置。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    CustomWorkflowMapInfo customWorkflowMapInfo = customWorkflowProcessContract.GetCustomWorkflowMapInfo(keyValueItem.Key, keyValueItem.Value);
                    if (customWorkflowMapInfo != null && ParentProcessId <= 0 && ProcessId <= 0)
                    {
                        MessageBox.Show("该流程节点关系已存在，不能重复增加。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
                
                SetWorkflowProcess?.Invoke(processRelationship);
                this.Close();
            }
            else
            {
                MessageBox.Show("请先设置父节点与子节点。", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 单击取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 节点关系类型选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void icmbNodeRelationship_SelectedIndexChanged(object sender, EventArgs e)
        {
            beParentNode.Text = string.Empty;
            beParentNode.Tag = null;
            beNode.Text = string.Empty;
            beNode.Tag = null;
        }

        #endregion

        #region 公有函数
        #endregion

        #region 私有函数

        /// <summary>
        /// 查找是否有闭环
        /// </summary>
        /// <returns></returns>
        private bool FindCircleExisted(decimal parentProcessId, decimal processId)
        {
            bool existed = false;

            IList<KeyValueItem> keyValueItems = customWorkflowProcessContract.GetKeyValueItems(WorkflowId);
            FindCircleHelp(parentProcessId, processId, keyValueItems);

            return existed;
        }

        /// <summary>
        /// 闭环搜索递归
        /// </summary>
        /// <returns></returns>
        private bool FindCircleHelp(decimal parentProcessId, decimal processId, IList<KeyValueItem> keyValueItems)
        {
            foreach(KeyValueItem rows in keyValueItems)
            {
                if (rows.Value == processId) 
                {
                        if(FindCircleHelp(parentProcessId, rows.Key, keyValueItems))
                        {
                            return true;
                        }
                }
            }
            return false;
        }

        /// <summary>
        /// 设置节点属性
        /// </summary>
        /// <param name="buttonEdit"></param>
        /// <param name="checkbox"></param>
        private void SetNode(DevExpress.XtraEditors.ButtonEdit buttonEdit, bool checkbox)
        {
            IList<CommonNode> commonNodes = customWorkflowProcessContract.GetChildNodes(WorkflowId);
            if (checkbox)
            {
                CheckedSelectedItemsForm frmCheckedSelectedItems = new CheckedSelectedItemsForm();
                frmCheckedSelectedItems.MultiNodeSelected = delegate (IList<CommonNode> selectedNodes)
                {                    
                    buttonEdit.Text = CommonObjHelper.GetCommonNodeNamesWithSemicolon(selectedNodes);
                    buttonEdit.Tag = selectedNodes;
                };
                frmCheckedSelectedItems.LoadAndSetCommonNodes(commonNodes);
                frmCheckedSelectedItems.ShowDialog();
            }
            else
            {
                DropDownSelectedItemsForm frmDropDownSelectedItems = new DropDownSelectedItemsForm();
                frmDropDownSelectedItems.LoadCommonNodes(commonNodes);
                frmDropDownSelectedItems.NodeSelected = delegate (CommonNode node)
                {
                    buttonEdit.Text = customWorkflowProcessContract.GetFullName(node.NodeId);
                    buttonEdit.Tag = node;
                };
                frmDropDownSelectedItems.ShowDialog();
            }
        }

        #endregion        
    }
}
