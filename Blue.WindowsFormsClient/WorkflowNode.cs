using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.WindowsFormsClient
{
    /// <summary>
    /// 工作流节点
    /// </summary>
    public class WorkflowNode : UserControl
    {
        private DevExpress.XtraEditors.PictureEdit pictureEdit;

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 工作流节点类型
        /// </summary>
        public WorkflowNodeType NodeType { get; set; }

        /// <summary>
        /// 节点在容器中左上角的坐标
        /// </summary>
        public Point TopLeftLocation { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="workflowNodeType"></param>
        /// <param name="topLeftLocation"></param>
        public WorkflowNode(WorkflowNodeType workflowNodeType, Point topLeftLocation) : this("默认流程节点名称", workflowNodeType, topLeftLocation)
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="workflowNodeType"></param>
        /// <param name="location"></param>
        public WorkflowNode(string nodeName, WorkflowNodeType workflowNodeType, Point topLeftLocation)
        {
            this.NodeName = nodeName;
            this.NodeType = workflowNodeType;
            int x = topLeftLocation.X - this.Size.Width / 2;
            int y = topLeftLocation.Y - this.Size.Height / 2;
            if (x < 0)
            {
                x = 0;
            }
            if (y < 0)
            {
                y = 0;
            }
            this.TopLeftLocation = new Point(x, y);
        }

        private void InitializeComponent()
        {
            this.pictureEdit = new DevExpress.XtraEditors.PictureEdit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureEdit
            // 
            this.pictureEdit.AllowDrop = true;
            this.pictureEdit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureEdit.Location = new System.Drawing.Point(0, 0);
            this.pictureEdit.Name = "pictureEdit";
            this.pictureEdit.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.pictureEdit.Properties.Appearance.Options.UseBackColor = true;
            this.pictureEdit.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pictureEdit.Properties.ReadOnly = true;
            this.pictureEdit.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            this.pictureEdit.Properties.ShowMenu = false;
            this.pictureEdit.Properties.ZoomAccelerationFactor = 1D;
            this.pictureEdit.Size = new System.Drawing.Size(140, 60);
            this.pictureEdit.TabIndex = 0;
            // 
            // WorkflowNode
            // 
            this.Controls.Add(this.pictureEdit);
            this.Name = "WorkflowNode";
            this.Size = new System.Drawing.Size(140, 60);
            ((System.ComponentModel.ISupportInitialize)(this.pictureEdit.Properties)).EndInit();
            this.ResumeLayout(false);

        }
    }
}
