using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppFramework.WinFormsControls
{
    public partial class PDFViewerForm : Form
    {
        public PDFViewerForm()
        {
            InitializeComponent();
        }

        private void PDFViewerForm_Load(object sender, EventArgs e)
        {
            
        }

        public void LoadDocument(Stream stream)
        {
            pdfViewer.LoadDocument(stream);
        }
    }
}
