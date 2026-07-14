using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFramework.WinFormsControls.DataGrid
{
    public partial class DevExpressGridWithPhoto : DevExpressGrid
    {
        #region 属性

        /// <summary>
        /// 用户照片
        /// </summary>
        public Image UserPhoto
        {
            get
            {
                return peUser.Image;
            }
            set
            {
                peUser.Image = value;
            }
        }


        /// <summary>
        /// 是否显示照片
        /// </summary>
        public bool IsPhotoShowed
        {
            get
            {
                return gcPhoto.Visible;
            }
            set
            {
                gcPhoto.Visible = value;
            }
        }

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public DevExpressGridWithPhoto()
        {
            InitializeComponent();
        }

        #endregion

    }
}
