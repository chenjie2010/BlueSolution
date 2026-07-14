using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class RowAndColSettingForm : Form
    {
        #region 内部成员变量

        private bool _isRowOrCol;

        #endregion

        #region 属性

        /// <summary>
        /// 设置行高或是列宽
        /// </summary>
        public bool IsRowOrCol
        {
            set
            {
                if (value)
                {
                    this.Text = "设置具体行高";
                    lblTip.Text = "设置具体行高：";
                }
                else
                {
                    this.Text = "设置具体列宽";
                    lblTip.Text = "设置具体列宽：";
                }
                _isRowOrCol = value;
            }
        }

        /// <summary>
        /// 设置行高列宽
        /// </summary>
        public SetRowHeightAndColWidthDelegate SetRowHeightAndColWidth
        {
            get;
            set;
        }

        #endregion

        public RowAndColSettingForm()
        {
            InitializeComponent();
        }

        private void RowColSettingForm_Load(object sender, EventArgs e)
        {

        }

        private void sbtnConfirm_Click(object sender, EventArgs e)
        {
            if (nudRowOrCol.Value <= 0 || nudRowOrCol.Value > short.MaxValue)
            {
                MessageBox.Show("设置的值有误，不能小于等于 0 或是大于 32767", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (SetRowHeightAndColWidth != null)
            {
                SetRowHeightAndColWidth(nudRowOrCol.Value);
            }
            Close();
        }

        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}