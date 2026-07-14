using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;

namespace AppFramework.WinFormsControls
{
    public partial class WorkflowPanel : UserControl
    {

        private readonly int maxNumberofWorkNodes;

        private Dictionary<int, WorkflowNode> WorkflowNodes;

        private IList<WorkflowLine> workflowLines;

        private WorkflowNode focusedWorkflowNode;

        public WorkflowPanel()
        {
            InitializeComponent();
            maxNumberofWorkNodes = 40;
            focusedWorkflowNode = null;
        }

        private void WorkflowPanel_Load(object sender, EventArgs e)
        {
            WorkflowNodes = new Dictionary<int, WorkflowNode>(maxNumberofWorkNodes);
            workflowLines = new List<WorkflowLine>();
        }

        public void All()
        {
            foreach (KeyValuePair<int, WorkflowNode> keyValuePair in WorkflowNodes)
            {
                keyValuePair.Value.Select();
            }
        }

        /// <summary>
        /// 获得节点的索引
        /// </summary>
        /// <returns></returns>
        private int GetIndexOfWorkflowNode()
        {
            int index = 0;

            for (int i = 0; i < maxNumberofWorkNodes; i++)
            {
                if (!WorkflowNodes.ContainsKey(i))
                {
                    index = i;
                    break;
                }
            }

            return index;
        }

        /// <summary>
        /// DragDrop: 当松开鼠标时发生
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlContainer_DragDrop(object sender, DragEventArgs e)
        {
            NavBarItemLink link = e.Data.GetData(typeof(NavBarItemLink)) as NavBarItemLink;
            if (link != null && link.Item.Enabled)
            {
                WorkflowNodeCategory workflowNodeType = (WorkflowNodeCategory)link.Item.Tag;
                Point point = pnlContainer.PointToClient(new Point(e.X, e.Y));
                int index = GetIndexOfWorkflowNode();
                WorkflowNode workflowNode = new WorkflowNode() { WorkflowNodeCategory = workflowNodeType, TopLeftLocation = point, Index = index};                
                workflowNode.MouseClick += delegate
                {
                    focusedWorkflowNode = (WorkflowNode)sender;
                };
                WorkflowNodes.Add(index, workflowNode);
                pnlContainer.Controls.Add(workflowNode);      
            }
        }

        /// <summary>
        /// DragEnter: 拖动后首次在进入某个控件内发生。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnlContainer_DragEnter(object sender, DragEventArgs e)
        {
            NavBarItemLink link = e.Data.GetData(typeof(NavBarItemLink)) as NavBarItemLink;
            if (link != null && link.Item.Enabled)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }
    }
}
