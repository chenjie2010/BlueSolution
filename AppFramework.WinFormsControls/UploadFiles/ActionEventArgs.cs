using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.WinFormsControls
{
    public class ActionEventArgs: EventArgs
    {
        /// <summary>
        /// 用户操作
        /// </summary>
        public UserAction UserAction
        {
            get;
            set;
        }
    }
}
