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

namespace Blue.WindowsFormsClient.Common
{
    public partial class CommentForm : Form
    {
        public string Capition
        {
            get
            {
                return gcPanel.Text;
            }
            set
            {
                gcPanel.Text = value;
            }
        }

        public TextSumbittedHandlerDelegate TextSumbittedHandler
        {
            get;
            set;
        }

        public CommentForm()
        {
            InitializeComponent();
        }

        private void CommnetForm_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
           string comment = txtCommment.Text.Trim();
            TextSumbittedHandler?.Invoke(comment);
            this.Close();

        }

        /// <summary>
        /// 取消
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
