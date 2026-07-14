using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.WinFormsLibrary;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.Model.BusinessModule;
using Blue.WCFContracts.BusinessModule;
using Blue.WindowsFormsClient;

namespace Blue.WindowsFormsClient.Common
{
    public partial class CustomDataFieldForm : Form
    {
        #region 内部成员变量

        private ICustomDataFieldContract _customDataFieldContract;

        private decimal _tableId;
        private UpdateTextCallback _updateText;

        #endregion

        #region 契约属性

        /// <summary>
        /// 数据字段契约
        /// </summary>
        public ICustomDataFieldContract CustomDataFieldContract
        {
            set
            {
                _customDataFieldContract = value;
            }
        }

        #endregion

        #region 赋值属性

        /// <summary>
        /// 数据表编号
        /// </summary>
        public decimal TableId
        {
            set
            {
                _tableId = value;
            }
        }

        /// <summary>
        /// 表达式文本框内容
        /// </summary>
        public string CustomDataFieldText
        {
            set
            {
                etxtCustomDataField.Text = value;
            }
        }

        public UpdateTextCallback UpdateText
        {
            set
            {
                _updateText = value;
            }
        }


        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CustomDataFieldForm()
        {
            InitializeComponent();
        }

        #endregion        

        #region 窗体和控件的方法

        /// <summary>
        /// 窗体加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CustomDataFieldForm_Load(object sender, EventArgs e)
        {
            IList<CommonNode> commonNodes = _customDataFieldContract.GetCommonNodes(_tableId, DataFieldFilter.OnlyPhysicalField);
            foreach (CommonNode commonNode in commonNodes)
            {
                lstDataField.Items.Add(commonNode);
            }
        }

        /// <summary>
        /// 清除条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnClear_Click(object sender, EventArgs e)
        {
            etxtCustomDataField.Text = string.Empty;
        }

        /// <summary>
        /// 确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnConfirm_Click(object sender, EventArgs e)
        {
            if (_updateText != null)
            {
                string customDataField = etxtCustomDataField.Text.Trim();
                if (!string.IsNullOrWhiteSpace(customDataField))
                {
                    if (!_customDataFieldContract.VerifyCustomDataFieldName(_tableId, customDataField))
                    {
                        MessageBox.Show("自定义字段验证失败！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }                
                _updateText(etxtCustomDataField.Text.Trim());
            }
            this.Close();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnVerify_Click(object sender, EventArgs e)
        {
            string customDataField = etxtCustomDataField.Text.Trim();
            if (string.IsNullOrWhiteSpace(customDataField))
            {
                MessageBox.Show("自定义字段不能为空！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (_customDataFieldContract.VerifyCustomDataFieldName(_tableId, customDataField))
            {
                MessageBox.Show("自定义字段验证成功！", "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            else
            {
                MessageBox.Show("自定义字段验证失败！", "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// 字段选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lstDataField_SelectedIndexChanged(object sender, EventArgs e)
        {
            CommonNode commonNode = null;
            if (lstDataField.SelectedItem != null)
            {
                commonNode = lstDataField.SelectedItem as CommonNode;
            }
            CustomDataFieldInfo customDataFieldInfo = _customDataFieldContract.GetModelInfo(commonNode.NodeId);
            if (customDataFieldInfo != null)
            {
                cmbeDataFieldType.Properties.Items.Clear();
                DataFieldProperty dataFieldProperty = (DataFieldProperty)customDataFieldInfo.DataFieldProperty;
                cmbeDataFieldProperty.Properties.Items.Add(UserEnumHelper.GetEnumText(dataFieldProperty));
                cmbeDataFieldProperty.SelectedIndex = 0;
                switch (dataFieldProperty)
                {
                    case DataFieldProperty.LogicalDataField:
                        cmbeDataFieldType.Properties.Items.Add(UserEnumHelper.GetEnumText((LogicalDataFieldType)customDataFieldInfo.DataFieldType));
                        cmbeDataFieldType.SelectedIndex = 0;
                        txtPhyscialName.Text = customDataFieldInfo.PhysicalName;
                        break;

                    case DataFieldProperty.PhysicalDataField:
                        cmbeDataFieldType.Properties.Items.Add(UserEnumHelper.GetEnumText((PhysicalDataFieldType)customDataFieldInfo.DataFieldType));
                        cmbeDataFieldType.SelectedIndex = 0;
                        txtPhyscialName.Text = customDataFieldInfo.PhysicalName;
                        break;

                }
                                txtDataFieldName.Text = customDataFieldInfo.LogicalName;
            }
        }

        #endregion
    }
}
