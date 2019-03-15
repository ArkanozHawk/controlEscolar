using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms;

namespace Control_Escolar
{
    public class Header : PdfPageEventHelper
    {




    public override void OnEndPage(PdfWriter writer, Document document)
        {

            // Make your table header using PdfPTable and name that tblHeader
            PdfPTable tblHeader = new PdfPTable(3);
            tblHeader.TotalWidth = document.Right - document.Left - document.LeftMargin - document.RightMargin;
            Image logoEsc = Image.GetInstance("../../../logo-esc.png");
            logoEsc.ScaleAbsolute(120, 70);
            Image logoSep = Image.GetInstance("../../../logo.png");
            logoSep.ScaleAbsolute(150, 60);
            

            tblHeader.AddCell(
                new PdfPCell(logoEsc)
                {
                HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.NO_BORDER
                });
            tblHeader.AddCell(
                new PdfPCell(new Paragraph("Instituto Rodolfo Neri Vela\nVicente Guerrero 49, Barrios Historicos, Acapulco Gro. 39540\nClave: 12DPT0003N\nNivel: Primaria\n\nBitacora de Inicio de Sesión"))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.NO_BORDER
                });
            tblHeader.AddCell(
                new PdfPCell(logoSep)
                {
                    HorizontalAlignment = Element.ALIGN_CENTER, Border = Rectangle.NO_BORDER
                });
            tblHeader.WriteSelectedRows(0, -1, document.Left + document.LeftMargin, document.Top + 95, writer.DirectContent);



               // Make your table footer using PdfPTable and name that tblFooter
            PdfPTable tblFooter = new PdfPTable(1);
            tblFooter.TotalWidth = document.Right - document.Left - document.LeftMargin - document.RightMargin;
            //var parrafo2 = new Paragraph("Página");
            //parrafo2.Alignment = 2;
            //tblFooter.AddCell("Página");
            tblFooter.AddCell(new PdfPCell(new Paragraph("Página " + document.PageNumber)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.NO_BORDER });
            tblFooter.WriteSelectedRows(0, -1, document.Left + document.LeftMargin, document.BottomMargin - 35, writer.DirectContent);
            Console.WriteLine(document.Top + "  el valor de bottom" + document.BottomMargin);

   
           /* PdfPTable table = new PdfPTable(1);
            table.TotalWidth = 400f;
            table.AddCell("Test");
            table.WriteSelectedRows(0, -1, 200, 50, writer.DirectContent);
            */
        }

        public  void Headerlista1A(PdfWriter writer, Document document)
        {

            // Make your table header using PdfPTable and name that tblHeader
            PdfPTable tblHeader = new PdfPTable(3);
            tblHeader.TotalWidth = document.Right - document.Left - document.LeftMargin - document.RightMargin;
            Image logoEsc = Image.GetInstance("../../../logo-esc.png");
            logoEsc.ScaleAbsolute(120, 70);
            Image logoSep = Image.GetInstance("../../../logo.png");
            logoSep.ScaleAbsolute(150, 60);


            tblHeader.AddCell(
                new PdfPCell(logoEsc)
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.NO_BORDER
                });
            tblHeader.AddCell(
                new PdfPCell(new Paragraph("Instituto Rodolfo Neri Vela\nVicente Guerrero 49, Barrios Historicos, Acapulco Gro. 39540\nClave: 12DPT0003N\nNivel: Primaria\n\n Lista de Alumnos 1 A"))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.NO_BORDER
                });
            tblHeader.AddCell(
                new PdfPCell(logoSep)
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    Border = Rectangle.NO_BORDER
                });
            tblHeader.WriteSelectedRows(0, -1, document.Left + document.LeftMargin, document.Top + 95, writer.DirectContent);



            // Make your table footer using PdfPTable and name that tblFooter
            PdfPTable tblFooter = new PdfPTable(1);
            tblFooter.TotalWidth = document.Right - document.Left - document.LeftMargin - document.RightMargin;
            //var parrafo2 = new Paragraph("Página");
            //parrafo2.Alignment = 2;
            //tblFooter.AddCell("Página");
            tblFooter.AddCell(new PdfPCell(new Paragraph("Página " + document.PageNumber)) { HorizontalAlignment = Element.ALIGN_RIGHT, Border = Rectangle.NO_BORDER });
            tblFooter.WriteSelectedRows(0, -1, document.Left + document.LeftMargin, document.BottomMargin - 35, writer.DirectContent);
            Console.WriteLine(document.Top + "  el valor de bottom" + document.BottomMargin);


            /* PdfPTable table = new PdfPTable(1);
             table.TotalWidth = 400f;
             table.AddCell("Test");
             table.WriteSelectedRows(0, -1, 200, 50, writer.DirectContent);
             */
        }

    }
}
