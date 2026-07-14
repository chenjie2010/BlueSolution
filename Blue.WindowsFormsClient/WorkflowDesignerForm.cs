using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraNavBar;
using AppFramework.WinFormsControls;

namespace Blue.WindowsFormsClient
{
    public partial class WorkflowDesignerForm : Form
    {
        public WorkflowDesignerForm()
        {
            InitializeComponent();
        }

        private void WorkflowDesignerForm_Load(object sender, EventArgs e)
        {
            //nbItmProcess.Tag = WorkflowNodeType.Business;
            //nbItmPolicy.Tag = WorkflowNodeType.Judgement;



            Graphics graphics = this.CreateGraphics();

        }

        private void btnItmSave_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            pnlMain.All();
        }
    }
}
