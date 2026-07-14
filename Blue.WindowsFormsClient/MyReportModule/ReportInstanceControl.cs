using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AppFramework.Core;
using AppFramework.Reference.EnterpriseLibrary;
using Blue.WindowsFormsClient.Common;
using Blue.Model.BusinessModule;
using Blue.Model.BusinessDesignerModule;
using Blue.WCFContracts.BusinessDesignerModule;
using Blue.WindowsFormsClient.DataConvertionModule;

namespace Blue.WindowsFormsClient.MyReportModule
{
    public partial class ReportInstanceControl : UserControl
    {
        #region 私有成员变量

        private ExtendedCustomBusinessInfo extendedCustomBusinessInfo;

        #endregion

        #region 契约接口

        private readonly ICustomReportContract customReportContract;

        #endregion

        #region 属性

        /// <summary>
        /// 返回主界面
        /// </summary>
        public GoBackDelegate GoBack
        {
            get;
            set;
        }

        /// <summary>
        /// 业务对象
        /// </summary>
        public ExtendedCustomBusinessInfo ExtendedCustomBusinessInfo
        {
            get
            {
                return extendedCustomBusinessInfo;
            }
            set
            {
                extendedCustomBusinessInfo = value;
                CustomReportInfo reportInfo = customReportContract.GetModelInfo(extendedCustomBusinessInfo.ReportId);
                if (reportInfo != null && (ReportCategory)reportInfo.ReportCategory == ReportCategory.Query)
                {
                    QueryReportType queryReportType = (QueryReportType)reportInfo.ReportType;
                    if (queryReportType == QueryReportType.Basic)
                    {
                        lnkPrintReport.Visible = true;
                    }
                    else
                    {
                        lnkPrintReport.Visible = false;
                    }                    
                }
                else
                {
                    lnkPrintReport.Visible = false;
                }
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public ReportInstanceControl()
        {
            InitializeComponent();
            customReportContract = BusinessDesignerChannelFactory.CreateCustomReportContract();
        }

        #endregion

        #region 控件方法

        /// <summary>
        /// 加载方法
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ReportInstanceControl_Load(object sender, EventArgs e)
        {
            
        }

        #endregion

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkRefresh_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 回到主页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkBack_Click(object sender, EventArgs e)
        {
            GoBack?.Invoke();
        }

        /// <summary>
        /// 查看介绍详情
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkMore_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void hlnkHistory_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewReport_Click(object sender, EventArgs e)
        {
            ViewReportForm frmViewReport = new ViewReportForm();
            frmViewReport.CurrentReportInfo = customReportContract.GetModelInfo(ExtendedCustomBusinessInfo.ReportId);
            frmViewReport.Show();
            frmViewReport.BringToFront();
        }

        private void lnkPrintReport_Click(object sender, EventArgs e)
        {
            DataConvertionForm frmDataConvertion = new DataConvertionForm();
            frmDataConvertion.ReportId = ExtendedCustomBusinessInfo.ReportId;
            frmDataConvertion.Text = string.Format("{0}_{1}", ExtendedCustomBusinessInfo.BusinessName, frmDataConvertion.Text);
            frmDataConvertion.ShowDialog();
        }
    }
}
