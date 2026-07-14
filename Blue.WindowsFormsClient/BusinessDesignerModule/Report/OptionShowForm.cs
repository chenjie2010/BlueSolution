using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class OptionShowForm : Form
    {
        private bool inital = true;

        #region 属性

        public SetReportPropertyVisibleDelegate SetColHeaderVisible
        {
            set;
            get;
        }

        public SetReportPropertyVisibleDelegate SetRowHeaderVisible
        {
            set;
            get;
        }

        public SetReportPropertyVisibleDelegate SetZeroVisible
        {
            set;
            get;
        }

        public SetReportPropertyVisibleDelegate SetGridLineVisible
        {
            set;
            get;
        }

        public bool IsShowGridLine
        {
            get
            {
                return chkGridLine.Checked;
            }
            set
            {
                chkGridLine.Checked = value;
            }
        }


        public bool IsShowRowHeader
        {
            get
            {
                return chkRowHeader.Checked;
            }
            set
            {
                chkRowHeader.Checked = value;
            }
        }

        public bool IsShowColHeader
        {
            get
            {
                return chkColHeader.Checked;
            }
            set
            {
                chkColHeader.Checked = value;
            }
        }
     
        public bool IsShowZero
        {
            get
            {
                return chkZero.Checked;
            }
            set
            {
                chkZero.Checked = value;
            }
        }
      
        #endregion

        public OptionShowForm()
        {
            InitializeComponent();
        }

        private void OptionShowForm_Load(object sender, EventArgs e)
        {
            inital = false;
        }

        private void sbtnConfirm_Click(object sender, EventArgs e)
        {

            this.Close();
        }

        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkColHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (!inital)
            {
                SetColHeaderVisible(chkColHeader.Checked);
            }
        }

        private void chkZero_CheckedChanged(object sender, EventArgs e)
        {
            if (!inital)
            {
                SetZeroVisible(chkZero.Checked);
            }
        }

        private void chkGridLine_CheckedChanged(object sender, EventArgs e)
        {
            if (!inital)
            {
                SetGridLineVisible(chkGridLine.Checked);
            }
        }

        private void chkRowHeader_CheckedChanged(object sender, EventArgs e)
        {
            if (!inital)
            {
                SetRowHeaderVisible(chkRowHeader.Checked);
            }
        }
    }
}