using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blue.CommonBusinessLogic.UserReport
{
    public class CustomReportBase
    {
        #region  受保护常量      

        /* 线程组个数 */
        protected const int MAX_THREAD_COUNT = 20;

        #endregion

        #region  受保护变量      

        protected Dictionary<decimal, byte[]> reportingData = new Dictionary<decimal, byte[]>();

        #endregion

    }
}
