using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using Blue.WCFContracts.BusinessModule;
using Blue.Model.BusinessModule;
using AppFramework.Core;

namespace Blue.WindowsFormsClient.MyBusinessModule
{
    public partial class ReviewersForm : Form
    {

        #region 私有变量


        #endregion

        #region 契约接口
        
        private readonly ICustomWorkflowProcessContract customWorkflowProcessContract;

        #endregion

        #region 属性   

        /// <summary>
        /// 工作流节点
        /// </summary>
        public CustomWorkflowProcessInfo CustomWorkflowProcessInfo
        {
            get;
            set;
        }

        /// <summary>
        /// 下一步审核人
        /// </summary>
        public Dictionary<decimal, Dictionary<decimal, string>> Reviewers
        {
            get;
            set;
        }

        /// <summary>
        /// 获得下一步节点的审核人
        /// </summary>
        public GetReviewersDelegate GetReviewers
        {
            get;
            set;
        }

        /// <summary>
        /// 关闭窗体
        /// </summary>
        public CloseFormDelegate CloseForm
        {
            get;
            set;
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReviewersForm()
        {
            InitializeComponent();
            customWorkflowProcessContract = BusinessChannelFactory.CreateCustomWorkflowProcessContract();
        }

        #endregion

        #region 窗体加载方法

        /// <summary>
        /// 显示审核人窗体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReviewersForm_Load(object sender, EventArgs e)
        {
            this.Height = 300;
            if (Reviewers.Count != 0)
            {
                foreach (KeyValuePair<decimal, Dictionary<decimal, string>> keyValues in Reviewers)
                {
                    GroupControl groupControl = new GroupControl();
                    pnlMain.Controls.Add(groupControl);
                    CommonNode commonNode = customWorkflowProcessContract.GetCommonNode(keyValues.Key);
                    groupControl.Text = commonNode.NodeName;
                    groupControl.Tag = commonNode;

                    RadioGroup radioGroup = new RadioGroup();
                    radioGroup.Properties.Columns = 3;
                    radioGroup.Dock = DockStyle.Fill;
                    groupControl.Controls.Add(radioGroup);
                    foreach (KeyValuePair<decimal, string> keyValue in keyValues.Value)
                    {
                        radioGroup.Properties.Items.Add(new RadioGroupItem(keyValue.Key, keyValue.Value));
                    }
                    if (radioGroup.Properties.Items.Count == 0)
                    {
                        MessageBox.Show(string.Format("{0}没有审核人，请联系管理员。", groupControl.Text), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    radioGroup.SelectedIndex = 0;
                    this.Height += groupControl.Height;
                }
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            Dictionary<decimal, decimal> reviewers = new Dictionary<decimal, decimal>();
            foreach (Control control in pnlMain.Controls)
            {
                GroupControl groupControl = control as GroupControl;
                if (groupControl != null)
                {
                    CommonNode commonNode = groupControl.Tag as CommonNode;
                    if (commonNode != null)
                    {
                        RadioGroup radioGroup = groupControl.Controls[0] as RadioGroup;
                        if (radioGroup.SelectedIndex < 0)
                        {
                            MessageBox.Show(string.Format("{0}请：选择审核人。", groupControl.Text), "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        decimal reviewerId = Convert.ToDecimal(radioGroup.Properties.Items[radioGroup.SelectedIndex]);
                        reviewers.Add(commonNode.NodeId, reviewerId);
                    }
                }
            }

            GetReviewers?.Invoke(reviewers);
            this.Close();
        }
               
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
                 
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion

        #region 私有方法

        #endregion


    }
}
