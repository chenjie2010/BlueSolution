using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data;
using System.Drawing.Printing;
using FarPoint.Win.Spread;

namespace Blue.WindowsFormsClient.BusinessDesignerModule
{
    /// <summary>
    /// 打印文档
    /// </summary>
    public class ReportPrintDocument : PrintDocument
    {
        private FpSpread fpReport;


        public Font PrinterFont;
        public string PatientTitle;
        public string DoctorTitle;


        public ReportPrintDocument(FpSpread spread)
            : base()
        {
            fpReport = spread;

        }
        protected override void OnBeginPrint(PrintEventArgs ev)
        {
            base.OnBeginPrint(ev);
            if (PrinterFont == null)
            {
                PrinterFont = new Font("宋体", 9F);
            }

        }
        protected override void OnPrintPage(PrintPageEventArgs e)
        {

            base.OnPrintPage(e);

            Rectangle rect = new Rectangle(e.PageBounds.X, e.PageBounds.Y + 17, e.PageBounds.Width / 2, e.PageBounds.Height / 2);
            int cnt = fpReport.GetOwnerPrintPageCount(e.Graphics, rect, fpReport.ActiveSheetIndex);
            fpReport.OwnerPrintDraw(e.Graphics, rect, fpReport.ActiveSheetIndex, cnt);
            e.HasMorePages = false;           
            e.Graphics.DrawString(PatientTitle, PrinterFont, new SolidBrush(Color.Black), new Rectangle(e.PageBounds.X, e.PageBounds.Y, e.PageBounds.Width / 2, e.PageBounds.Height / 2));
            e.Graphics.DrawString(DoctorTitle, PrinterFont, new SolidBrush(Color.Black), new Rectangle(e.PageBounds.X, e.PageBounds.Y + 235, e.PageBounds.Width / 2, e.PageBounds.Height / 2));
        }
    }    
}
