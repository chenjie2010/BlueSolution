using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;

namespace Blue.WindowsFormsClient.MyQueryModule
{
    public partial class QueryPropertiesForm : Form
    {
        #region 内部成员变量

        private GetQueryPropertiesDelegate _getDataQueryProperty;

        #endregion

        #region 属性

        /// <summary>
        /// 获得查询属性
        /// </summary>
        public GetQueryPropertiesDelegate GetDataQueryProperty
        {
            set
            {
                _getDataQueryProperty = value;
            }
            get
            {
                return _getDataQueryProperty;
            }
        }

        #endregion

        #region 构造函数

        public QueryPropertiesForm()
        {
            InitializeComponent();
        }

        #endregion

        #region 窗体及控件方法

        private void QueryPropertiesForm_Load(object sender, EventArgs e)
        {
            
        }

        private void sbtnConfirm_Click(object sender, EventArgs e)
        {
            int pageSize = VerfiyPageSize();
            if (pageSize < 10 || pageSize > 10000)
            {
                MessageBox.Show("每页显示的数目的范围在[10,10000]之间，请重新输入！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_getDataQueryProperty != null)
            {
                _getDataQueryProperty(chkDistinct.Checked, pageSize);
            }
            this.Close();
        }

        private void sbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 公有方法

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="distinct"></param>
        /// <param name="pageSize"></param>
        public void SetDataQueryProperty(bool distinct, int pageSize)
        {
            chkDistinct.Checked = distinct;
            if (distinct)
            {
                txtPageSize.Text = AppSettingHelper.DefaultPageSize.ToString();
            }
            else
            { 
                txtPageSize.Text = pageSize.ToString();
            } 
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 验证每页记录数是否合法
        /// </summary>
        /// <returns></returns>
        private int VerfiyPageSize()
        {
            int pageSize = AppSettingHelper.DefaultPageSize;
            string text = txtPageSize.Text.Trim();
            if (UserDataHelper.MatchInteger(text))
            {
                int pageSizeTemp = DataConvertionHelper.GetConvertedInt(text);
                if (pageSizeTemp >= 10 && pageSizeTemp <= 1000)
                {
                    pageSize = pageSizeTemp;
                }
            }

            return pageSize;
        }

        #endregion
    }
}
