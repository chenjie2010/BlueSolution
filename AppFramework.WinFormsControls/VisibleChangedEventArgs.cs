using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppFramework.WinFormsControls
{
    /// <summary>
    /// 是否可见参数改变事件
    /// </summary>
    public class VisibleChangedEventArgs : EventArgs
    {
        private bool _visible = false;

        public bool Visible
        {
            get
            {
                return _visible;
            }
        }

        public VisibleChangedEventArgs(bool visible)
        {
            _visible = visible;
        }

    }
}
