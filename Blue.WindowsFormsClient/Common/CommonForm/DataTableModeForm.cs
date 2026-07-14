using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Blue.WindowsFormsClient
{
    public partial class DataTableModeForm : Form
    {
        public DataSet DataSource
        {
            set
            {
                gcData.DataSource = value.Tables[0];
            }
        }

        public DataTableModeForm()
        {
            InitializeComponent();
        }

        private void DataTableShowForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvDataFields_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator && e.RowHandle >= 0)
            {
                e.Info.DisplayText = (e.RowHandle + 1).ToString();
            }
        }
    }
}
