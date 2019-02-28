using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MaterialSkin;
using MaterialSkin.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;

namespace Control_Escolar
{
    public partial class bitacora : MaterialForm
    {
        MySqlCommand codigo = new MySqlCommand();
        MySqlConnection conectanos = new MySqlConnection();
        //MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela;pwd=digi3.0");
        MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela");
        conexion obj = new conexion();

        public static void ThreadPrincipal()

        {
            Application.Run(new principal());
        }

        public static void ThreadProc()

        {
            Application.Run(new login());
        }

        public bitacora()
        {
            InitializeComponent();
            datagrid(dataGridView1);
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);
        }

        private void bitacora_Load(object sender, EventArgs e)
        {
            //string usuario, Horaentrada, Horasalida;
            //int n = dataGridView1.Rows.Add();
            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";

        }

        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        public void datagrid(DataGridView data)
        {
            coneccion.Open();
            codigo.Connection = coneccion;
            codigo.CommandText = ("select Usuario,HoraEntrada,HoraSalida from bitacora");
            try
            {
                MySqlDataAdapter seleccionar = new MySqlDataAdapter();
                seleccionar.SelectCommand = codigo;
                DataTable datostabla = new DataTable();
                seleccionar.Fill(datostabla);
                BindingSource formulario = new BindingSource();
                formulario.DataSource = datostabla;
                data.DataSource = formulario;
                seleccionar.Update(datostabla);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerarPDF_Click(object sender, EventArgs e)
        {
            exportardata(dataGridView1, "test");

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            string HoraSalida = Convert.ToString(DateTime.Now);
            int idAccess = sesion.idAcceso;
            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
            string conexion = "server=localhost;uid=root;database=nerivela";
            string inserta_bitacora = "UPDATE bitacora SET HoraSalida = '" + HoraSalida + "' where idAcceso = " + idAccess + ";";
            obj.insBitacora(conexion, inserta_bitacora);
            System.Threading.Thread login = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));

            login.Start();
            this.Close();
        }
        public void exportardata(DataGridView dgw, string filename)
        {

            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;
            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 15, iTextSharp.text.Font.NORMAL);

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdftable.AddCell(cell);
            }

            int row = dataGridView1.Rows.Count;
            int cell2 = dataGridView1.Rows[1].Cells.Count;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cell2; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        //return directly
                        //return;
                        //or set a value for the empty data
                        dataGridView1.Rows[i].Cells[j].Value = "null";
                    }
                    pdftable.AddCell(dataGridView1.Rows[i].Cells[j].Value.ToString());
                }
            }
            //Exporting to PDF
            string folderPath = @"C:\shashe\";
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (FileStream stream = new FileStream(folderPath + "DataGridViewExport.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
                PdfWriter.GetInstance(pdfDoc, stream);
                pdfDoc.Open();
                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:/Users/Tevi/Documents/Gestionde proyectos/controlEscolar-master/logo1.jpg");
                imagen.BorderWidth = 0;
                imagen.SetAbsolutePosition(0, 500);
                float percentage = 0.0f;
                percentage = 150 / imagen.Width;
                imagen.ScalePercent(percentage * 100);
                pdfDoc.Add(imagen);
                var parrafo2 = new Paragraph("        Instituto Rodolfo Neri Vela");
                parrafo2.SpacingBefore = 200;
                parrafo2.SpacingAfter = 0;
                parrafo2.Alignment = 1; //0-Left, 1 middle,2 Right
                pdfDoc.Add(parrafo2);

                var parrafo3 = new Paragraph("        Bitacora de Inicio de Sesión");

                parrafo3.Alignment = 1; //0-Left, 1 middle,2 Right
                pdfDoc.Add(parrafo3);

                var parrafo4 = new Paragraph("        Vicente Guerrero 49 , Barrios Historicos , Acapulco Guerrero");

                parrafo4.Alignment = 1; //0-Left, 1 middle,2 Right
                pdfDoc.Add(parrafo4);

                var parrafo5 = new Paragraph("    Clave:12DPT0003N       Nivel: Primaria");
                parrafo5.Alignment = 1; //0-Left, 1 middle,2 Right


                pdfDoc.Add(parrafo5);
                pdfDoc.Add(Chunk.NEWLINE);












                MessageBox.Show("Done");
                // Abrimos el archivo  doc.Open();

                //Image png = Image.GetInstance(imagepath + "/logo-sep.png");
                // png.ScalePercent(24f);
                //doc.Add(png);

                // Escribimos el encabezamiento en el documento



                pdfDoc.Add(pdftable);
                pdfDoc.Close();
                stream.Close();

            }
        }

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@"C:\Users\Lizeth\Desktop\prueba.pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Bitacora de Inicio de Sesión");
            doc.AddCreator("Instituto Rodolfo Neri Vela");

            // Abrimos el archivo
            doc.Open();

            //Image png = Image.GetInstance(imagepath + "/logo-sep.png");
            // png.ScalePercent(24f);
            //doc.Add(png);

            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("Instituto Rodolfo Neri Vela"));
            doc.Add(new Paragraph("Vicente Guerrero 49, Barrios Históricos, 39300. Acapulco, Gro."));
            doc.Add(new Paragraph("Clave:12DPT0003N       Nivel: Primaria"));
            doc.Add(new Paragraph("Bitacora de Inicio de Sesión"));
            doc.Add(Chunk.NEWLINE);

            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Creamos una tabla que contendrá el nombre, apellido y país 
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(4);
            tblPrueba.WidthPercentage = 100;
            tblPrueba.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha

            // Configuramos el título de las columnas de la tabla
            PdfPCell clusuario = new PdfPCell(new Phrase("Usuario", _standardFont));
            clusuario.BorderWidth = 0;
            clusuario.BorderWidthBottom = 0.75f;

            PdfPCell clfecha = new PdfPCell(new Phrase("Fecha", _standardFont));
            clfecha.BorderWidth = 0;
            clfecha.BorderWidthBottom = 0.75f;

            PdfPCell clhoraent = new PdfPCell(new Phrase("Hora de Entrada", _standardFont));
            clhoraent.BorderWidth = 0;
            clhoraent.BorderWidthBottom = 0.75f;

            PdfPCell clhorasal = new PdfPCell(new Phrase("Hora de Salida", _standardFont));
            clhorasal.BorderWidth = 0;
            clhorasal.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clusuario);
            tblPrueba.AddCell(clfecha);
            tblPrueba.AddCell(clhoraent);
            tblPrueba.AddCell(clhorasal);

            // Llenamos la tabla con información
            clusuario = new PdfPCell(new Phrase("Mayolo", _standardFont));
            clusuario.BorderWidth = 0;

            clfecha = new PdfPCell(new Phrase("Suaste", _standardFont));
            clfecha.BorderWidth = 0;

            clhoraent = new PdfPCell(new Phrase("Puerto Rico", _standardFont));
            clhoraent.BorderWidth = 0;

            clhorasal = new PdfPCell(new Phrase("Puerto Rico", _standardFont));
            clhorasal.BorderWidth = 0;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clusuario);
            tblPrueba.AddCell(clfecha);
            tblPrueba.AddCell(clhoraent);
            tblPrueba.AddCell(clhorasal);

            clusuario = new PdfPCell(new Phrase("Martha", _standardFont));
            clusuario.BorderWidth = 0;

            clfecha = new PdfPCell(new Phrase("Teran", _standardFont));
            clfecha.BorderWidth = 0;

            clhoraent = new PdfPCell(new Phrase("México", _standardFont));
            clhoraent.BorderWidth = 0;

            clhorasal = new PdfPCell(new Phrase("México", _standardFont));
            clhorasal.BorderWidth = 0;

            tblPrueba.AddCell(clusuario);
            tblPrueba.AddCell(clfecha);
            tblPrueba.AddCell(clhoraent);
            tblPrueba.AddCell(clhorasal);

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(tblPrueba);

            doc.Close();
            writer.Close();

            MessageBox.Show("¡PDF creado!");
        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            string HoraSalida = Convert.ToString(DateTime.Now);
            int idAccess = sesion.idAcceso;
            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
            string conexion = "server=localhost;uid=root;database=nerivela";
            string inserta_bitacora = "UPDATE bitacora SET HoraSalida = '" + HoraSalida + "' where idAcceso = " + idAccess + ";";
            obj.insBitacora(conexion, inserta_bitacora);
            System.Threading.Thread login = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));

            login.Start();
            this.Close();
        }
    }
}
