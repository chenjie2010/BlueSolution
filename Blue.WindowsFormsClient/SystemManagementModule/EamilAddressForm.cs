using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Blue.WindowsFormsClient;
using AppFramework.Core;

namespace Blue.WindowsFormsClient.SystemManagementModule
{
    public partial class EamilAddressForm : Form
    {
        public UpdateTextCallback UpdateText
        {
            get;
            set;
        }

        public EamilAddressForm()
        {
            InitializeComponent();
        }

        private void EamilAddressForm_Load(object sender, EventArgs e)
        {

        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            string emailAddress = txtEmail.Text.Trim();
            if (string.IsNullOrWhiteSpace(emailAddress))
            {
                txtEmail.Focus();
                MessageBox.Show("Email地址不能为空！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!DataConvertionHelper.IsEmailAddress(emailAddress))
            {
                txtEmail.Focus();
                MessageBox.Show("Email地址格式不正确！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            UpdateText?.Invoke(emailAddress);
            this.Close();
        }
    }
}
