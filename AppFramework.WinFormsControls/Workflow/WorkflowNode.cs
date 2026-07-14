using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFramework.WinFormsControls
{
    public partial class WorkflowNode : UserControl
    {
        private bool mouseMove = false;

        /// <summary>
        /// 节点名称
        /// </summary>
        public string NodeName { get; set; }

        /// <summary>
        /// 工作流节点类型
        /// </summary>
        public WorkflowNodeCategory WorkflowNodeCategory { get; set; }

        /// <summary>
        /// 鼠标的坐标
        /// </summary>
        public Point TopLeftLocation { get; set; }

        /// <summary>
        /// 索引
        /// </summary>
        public int Index { get; set; }       

        public WorkflowNode()
        {
            InitializeComponent();            
            TopLeftLocation = new Point(0, 0);
            Index = 0;
        }

        private void WorkflowNode_Load(object sender, EventArgs e)
        {
            //将鼠标设置为节点的中心
            int x = TopLeftLocation.X - this.Size.Width / 2;
            int y = TopLeftLocation.Y - this.Size.Height / 2;
            if (x < 0)
            {
                x = 2;
            }
            if (y < 0)
            {
                y = 2;
            }
            this.Location = new Point(x, y);            
            switch (this.WorkflowNodeCategory)
            {
                case WorkflowNodeCategory.Business:
                    if (string.IsNullOrWhiteSpace(NodeName))
                    {
                        pictureEdit.Properties.Caption.Text = string.Format("默认流程节点名称{0}", Index);
                    }
                    pictureEdit.Image = global::AppFramework.WinFormsControls.Properties.Resources.PorcessNode;
                    break;

                case WorkflowNodeCategory.Judgement:
                    if (string.IsNullOrWhiteSpace(NodeName))
                    {
                        pictureEdit.Properties.Caption.Text = string.Format("默认判断节点名称{0}", Index);
                    }
                    pictureEdit.Image = global::AppFramework.WinFormsControls.Properties.Resources.PolicyNode;
                    break;
            }
            this.BringToFront();
        }

        private void pictureEdit_MouseEnter(object sender, EventArgs e)
        {
            pictureEdit.Cursor = Cursors.Hand;
        }

        private void pictureEdit_MouseLeave(object sender, EventArgs e)
        {
            pictureEdit.Cursor = Cursors.Default;
        }

        private void pictureEdit_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && mouseMove)
            {
                int x = this.Location.X + e.Location.X - TopLeftLocation.X;
                int y = this.Location.Y + e.Location.Y - TopLeftLocation.Y;
                if (x < 0)
                {
                    x = 2;
                }
                if (y < 0)
                {
                    y = 2;
                }
                this.Location = new Point(x,  y);
                mouseMove = false;
            }         
        }

        private void pictureEdit_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                mouseMove = true;
                TopLeftLocation = e.Location;
            }
        }
    }
}
