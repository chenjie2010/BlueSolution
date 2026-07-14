using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using AppFramework.Core;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    public partial class RowAndColIndexForm : Form
    {
        
        #region 内部成员变量

        private int _oldRows;
        private int _oldCols;
        private bool _maxOrFix;

        #endregion

        #region 属性

        /// <summary>
        /// 设置行列数
        /// </summary>
        public SetRowAndColCountDelegate SetRowAndColCount
        {
            set;
            get;
        }

        /// <summary>
        /// 旧行
        /// </summary>
        public int OldRows
        {
            get
            {
                return _oldRows;
            }
            set
            {
                if (_oldRows == value)
                    return;
                _oldRows = value;
            }
        }

        /// <summary>
        /// 旧列
        /// </summary>
        public int OldCols
        {
            get
            {
                return _oldCols;
            }
            set
            {
                if (_oldCols == value)
                    return;
                _oldCols = value;
            }
        }

        /// <summary>
        /// 是固定行列还是最大行列
        /// </summary>
        public bool MaxOrFix
        {
            set
            {
                _maxOrFix = value;
                if (value)
                {
                    this.Text = "最大行列";
                    lblRowTip.Text = "最大行数：";
                    lblColTip.Text = "最大列数：";
                    
                }
                else
                {
                    this.Text = "固定行列";
                    lblRowTip.Text = "固定行数：";
                    lblColTip.Text = "固定列数：";
                }
            }
            get
            {
                return _maxOrFix;
            }
        }
      
        #endregion

        public RowAndColIndexForm()
        {
            InitializeComponent();
        }

        private void MaxOrFixRowColForm_Load(object sender, EventArgs e)
        {
            etxtRow.Text = _oldRows.ToString();
            etxtCol.Text = _oldCols.ToString();
        }

        private void sbtnConfirm_Click(object sender, EventArgs e)
        {
            string rowValue = etxtRow.Text.Trim();
            string colValue = etxtCol.Text.Trim();
            if (string.IsNullOrEmpty(rowValue))
            {
                etxtRow.Focus();
                MessageBox.Show("行数不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(colValue))
            {
                etxtCol.Focus();
                MessageBox.Show("列数不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Regex r = new Regex(@"^\d+$");
            if (!r.IsMatch(rowValue))
            {
                etxtRow.Focus();
                MessageBox.Show("行数只能能为正整数！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!r.IsMatch(colValue))
            {
                etxtCol.Focus();
                MessageBox.Show("列数只能能为正整数！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            int rows = DataConvertionHelper.GetConvertedInt(rowValue);
            int cols = DataConvertionHelper.GetConvertedInt(colValue);
            if (rows > 65536 || rows < 0)
            {
                MessageBox.Show("最大行数不能小于 0 或大于 65536", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                etxtRow.Text = _oldRows.ToString();
                rows = _oldRows;
                return;
            }
            if (cols > 256 || cols < 0)
            {
                MessageBox.Show("最大列数不能小于 0 或大于 256！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                etxtCol.Text = _oldCols.ToString();
                cols = _oldCols;
                return;
            }
            if (SetRowAndColCount != null)
            {
                SetRowAndColCount(rows, cols);
            }            
            this.Close();
        }

        private void sbtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}