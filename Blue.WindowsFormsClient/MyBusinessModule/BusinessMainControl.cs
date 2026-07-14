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
using Blue.WCFContracts.BusinessModule;

namespace Blue.WindowsFormsClient.MyBusinessModule
{
    public partial class BusinessMainControl : UserControl
    {
        /// <summary>
        /// 工作流实例契约
        /// </summary>
        public ICustomWorkflowInstanceContract CustomWorkflowInstanceContract
        {
            get;
            set;
        }

        public BusinessMainControl()
        {
            InitializeComponent();
        }

        private void BusinessMainControl_Load(object sender, EventArgs e)
        {
            ShowStatisticData();
        }

        /// <summary>
        /// 显示统计数据
        /// </summary>
        private void ShowStatisticData()
        {
            //hlblWithdraw.Text = string.Format("共有{0}件",
            //    BusinessInstanceContract.GetBusinessInstanceCount(CurrentUser.Instance.UserId, DataSumbittedState.Review));

            //hyperlinkLabelControl1.Text = string.Format("共有{0}件",
            //    BusinessInstanceContract.GetBusinessInstanceCount(CurrentUser.Instance.UserId, DataSumbittedState.Drfat));

            //hyperlinkLabelControl3.Text = string.Format("共有{0}件",
            //    BusinessInstanceContract.GetBusinessInstanceCount(CurrentUser.Instance.UserId, DataSumbittedState.Completed));
        }
    }
}
