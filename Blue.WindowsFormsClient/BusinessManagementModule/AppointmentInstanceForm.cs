using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blue.WindowsFormsClient.BusinessManagementModule
{
    public partial class AppointmentInstanceForm : Form
    {
        public AppointmentInstanceForm()
        {
            InitializeComponent();
        }

        private void AppointmentInstanceForm_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }
    }
}
