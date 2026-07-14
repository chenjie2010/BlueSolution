using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFramework.WinFormsControls
{
    /// <summary>
    /// 工作流节点之间的连线
    /// </summary>
    public class WorkflowLine : UserControl
    {
        /// <summary>
        /// 源节点
        /// </summary>
        public WorkflowNode SourceWorkflowNode { get; set; }

        /// <summary>
        /// 目标节点
        /// </summary>
        public WorkflowNode DestWorkflowNode { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="sourceWorkflowNode"></param>
        /// <param name="destWorkflowNode"></param>
        public WorkflowLine(WorkflowNode sourceWorkflowNode, WorkflowNode destWorkflowNode)
        {
            SourceWorkflowNode = sourceWorkflowNode;
            DestWorkflowNode = destWorkflowNode;
        }
    }
}
