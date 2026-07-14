using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.XtraPrinting;
using System.Drawing;
using System.IO;
using System.Data;

namespace AppFramework.WinFormsControls
{
    public delegate void CreateDocumentDelegate(BrickGraphics graph, SizeF textSize);

    public class ExportedDataTableFormat : Link
    {

        private CreateDocumentDelegate _createContent = null;

        public CreateDocumentDelegate CreateContent
        {
            set
            {
                _createContent = value;
            }
            get
            {
                return _createContent;
            }
        }

        private CreateDocumentDelegate _createHeader = null;

        public CreateDocumentDelegate CreateHeader
        {
            set
            {
                _createHeader = value;
            }
            get
            {
                return _createHeader;
            }
        }

        public ExportedDataTableFormat()
        {
          
        }

        protected override void CreateReportHeader(BrickGraphics graph)
        {
            base.CreateReportHeader(graph);

            graph.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            SizeF textSize = graph.MeasureString("Test", (int)graph.ClientPageSize.Width);
            if(_createHeader != null)
            {
                _createHeader(graph, textSize);
            }            
        }

        protected override void CreateDetail(BrickGraphics graph)
        {
            base.CreateDetail(graph);
            graph.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));            
            SizeF textSize = graph.MeasureString("Test", (int)graph.ClientPageSize.Width);
            if (_createContent != null)
            {
                _createContent(graph, textSize);
            }

            //float y = 0;
            //for (int i = 0; i < dataTable.Rows.Count; i++)
            //{
            //    float x = 0;
            //    for (int j = 0; j < dataTable.Columns.Count; j++)
            //    {
            //        string cellValue = dataTable.Rows[i][j].ToString();
            //        RectangleF rect = new RectangleF(x, y, (graph.ClientPageSize.Width - 1) / dataTable.Columns.Count, textSize.Height);

            //        graph.DrawString(cellValue, Color.Black, rect, BorderSide.All);
            //        x += (graph.ClientPageSize.Width - 1) / dataTable.Columns.Count;
            //    }
            //    y += textSize.Height;
            //}
        }       
    }
}
