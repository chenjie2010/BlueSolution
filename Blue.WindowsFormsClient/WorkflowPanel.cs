using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraNavBar;

namespace Blue.WindowsFormsClient
{
    public partial class WorkflowPanel : UserControl
    {

        private readonly int maxNumberofWorkNodes;

        private IDictionary<int, WorkflowNode> WorkflowNodes;

        public WorkflowPanel()
        {
            InitializeComponent();
            maxNumberofWorkNodes = 40;
        }

        private void WorkflowPanel_Load(object sender, EventArgs e)
        {
            WorkflowNodes = new Dictionary<int, WorkflowNode>(maxNumberofWorkNodes);
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
                WorkflowNodeType workflowNodeType = (WorkflowNodeType)link.Item.Tag;
                Point point = pnlContainer.PointToClient(new Point(e.X, e.Y));
                WorkflowNode workflowNode = new WorkflowNode(workflowNodeType, point);


                PictureEdit pictureEdit = new PictureEdit();
                pictureEdit.Location = new Point(point.X - 70, point.Y - 30);
                    //new Point(e.X - this.ParentForm.Location.X, e.Y - this.ParentForm.Location.Y);
                pictureEdit.Size = new Size(140, 60);
                pictureEdit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
                pictureEdit.Properties.Appearance.BackColor = Color.Transparent;
                pictureEdit.Properties.Appearance.Options.UseBackColor = true;
                pictureEdit.Properties.ShowMenu = false;
                pictureEdit.Properties.Caption.Text = "默认流程节点名称";
                pictureEdit.Properties.Caption.Alignment = ContentAlignment.BottomCenter;
                pictureEdit.Properties.Caption.Appearance.TextOptions.WordWrap = DevExpress.Utils.WordWrap.NoWrap;
                pictureEdit.AllowDrop = true;

                
                switch (workflowNodeType)
                {
                    case WorkflowNodeType.Business:
                        pictureEdit.Image = Blue.WindowsFormsClient.Properties.Resources.WorkflowDesigner_PorcessNode;
                        break;

                    case WorkflowNodeType.Judgement:
                        pictureEdit.Image = Blue.WindowsFormsClient.Properties.Resources.WorkflowDesigner_PolicyNode;
                        break;
                }               
                pnlContainer.Controls.Add(pictureEdit);                
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

        private void pnlContainer_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                Cursor.Current = Cursors.Hand;
            }

        }

        private void pnlContainer_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

                Cursor.Current = Cursors.Default;
            }
        }

        private void pnlContainer_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {

            }
        }
    }
}
