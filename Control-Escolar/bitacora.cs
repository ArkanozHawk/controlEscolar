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
            codigo.CommandText = ("SELECT  idAcceso , Usuario,Nombre,Apellido,Fecha,HoraEntrada,HoraSalida FROM  `bitacora` ");


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


            MessageBox.Show("¡PDF creado!");

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            string HoraSalida = DateTime.Now.ToString("hh:mm:ss");
            int idAccess = sesion.idAcceso;
            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
            string conexion = "server=localhost;uid=root;database=nerivela";
            string inserta_bitacora = "UPDATE bitacora SET HoraSalida = '" + HoraSalida + "' where idAcceso = '" + idAccess + "';";
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
            string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
            if (!Directory.Exists(folderPath))// pregunt si no existe
            {
                Directory.CreateDirectory(folderPath); // si no existe lo crea
            }
            using (FileStream stream = new FileStream(folderPath + "Bitacora2.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.LETTER, 10f, 10f, 100f, 100f); //se declara las medidas y margenes del pdf por ejemplo tamaño CARTA
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);//cosas de itextsharp xD

                //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:/Users/Tevi/Documents/Gestionde proyectos/controlEscolar-master/logo1.jpg");
                string direccion = Directory.GetCurrentDirectory();//obtenemos direccion no se paque xD

                //aqui empezamos agregar cosas :3

                // Header hola = new Header();
                writer.PageEvent = new Header();

                pdfDoc.Open();//se habre el docuemnto

                // pdfDoc.NewPage();

                //aqui pones todo lo que va en medio :D
                pdfDoc.Add(pdftable);
                pdfDoc.Close();
                stream.Close();
            }


            /*
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
                using (FileStream stream = new FileStream(folderPath + "Bitacora.pdf", FileMode.Create))
                {
                    Document pdfDoc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfDoc, stream);
                    pdfDoc.Open();
                    //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:/Users/Tevi/Documents/Gestionde proyectos/controlEscolar-master/logo1.jpg");
                    string direccion = Directory.GetCurrentDirectory();
                    iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(direccion + "/imagenes/logo.png");
                    imagen.BorderWidth = 0; //Aqui le quitas los bordes :D
                    imagen.SetAbsolutePosition(0, 500);//aqui haces que vuele y se ponga en una posicion
                    float percentage = 0.0f;
                    percentage = 150 / imagen.Width;
                    imagen.ScalePercent(percentage * 100);

                    pdfDoc.Add(imagen);
                    var parrafo2 = new Paragraph("        Instituto Rodolfo Neri Vela");
                    parrafo2.SpacingBefore = 200;
                    parrafo2.SpacingAfter = 0;
                    parrafo2.Alignment = 1; //0-Left, 1 middle,2 Right
                    pdfDoc.Add(parrafo2);
                    //Yo lo hice como una tabla con 3 cajitas creo que
                    var parrafo3 = new Paragraph("        Vicente Guerrero 49 , Barrios Historicos , Acapulco Guerrero");

                    parrafo3.Alignment = 1; //0-Left, 1 middle,2 Right
                    pdfDoc.Add(parrafo3);

                    var parrafo4 = new Paragraph("    Clave:12DPT0003N        Nivel: Primaria");

                    parrafo4.Alignment = 1; //0-Left, 1 middle,2 Right
                    pdfDoc.Add(parrafo4);

                    var parrafo5 = new Paragraph("    Bitacora de Inicio de Sesión");
                    parrafo5.Alignment = 1; //0-Left, 1 middle,2 Right
                    pdfDoc.Add(parrafo5);
                    pdfDoc.Add(Chunk.NEWLINE);

                    MessageBox.Show("Descargado");
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
            */
        }

        private void Bitacora_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {

                string HoraSalida = DateTime.Now.ToString("hh:mm:ss");
                int idAccess = sesion.idAcceso;
                //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
                string conexion = "server=localhost;uid=root;database=nerivela";
                string inserta_bitacora = "UPDATE bitacora SET HoraSalida = '" + HoraSalida + "' where idAcceso = " + idAccess + ";";
                obj.insBitacora(conexion, inserta_bitacora);


            }
            else
            {
                System.Threading.Thread login = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
                login.Start();
            }
        }
    }

}

//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Data;
//using System.Drawing;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows.Forms;
//using MySql.Data.MySqlClient;
//using MaterialSkin;
//using MaterialSkin.Controls;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using System.IO;

//namespace Control_Escolar
//{
//    public partial class bitacora : MaterialForm
//    {
//        MySqlCommand codigo = new MySqlCommand();
//        MySqlConnection conectanos = new MySqlConnection();
//        //MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela;pwd=digi3.0");
//        MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela");
//        conexion obj = new conexion();

//        public static void ThreadPrincipal()

//        {
//            Application.Run(new principal());
//        }

//        public static void ThreadProc()

//        {
//            Application.Run(new login());
//        }

//        public bitacora()
//        {
//            InitializeComponent();
//            datagrid(dataGridView1);
//            var materialSkinManager = MaterialSkinManager.Instance;
//            materialSkinManager.AddFormToManage(this);
//            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
//            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);
//        }

//        private void bitacora_Load(object sender, EventArgs e)
//        {
//            //string usuario, Horaentrada, Horasalida;
//            //int n = dataGridView1.Rows.Add();
//            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";

//        }

//        private void btnPrincipal_Click(object sender, EventArgs e)
//        {
//            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
//            pantalla.Start();
//            CheckForIllegalCrossThreadCalls = false;
//            this.Close();
//        }

//        public void datagrid(DataGridView data)
//        {
//            coneccion.Open();
//            codigo.Connection = coneccion;
//            codigo.CommandText = ("select Usuario,HoraEntrada,HoraSalida from bitacora");
//            try
//            {
//                MySqlDataAdapter seleccionar = new MySqlDataAdapter();
//                seleccionar.SelectCommand = codigo;
//                DataTable datostabla = new DataTable();
//                seleccionar.Fill(datostabla);
//                BindingSource formulario = new BindingSource();
//                formulario.DataSource = datostabla;
//                data.DataSource = formulario;
//                seleccionar.Update(datostabla);
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message);
//            }
//        }

//        private void GenerarPDF_Click(object sender, EventArgs e)
//        {
//            exportardata(dataGridView1, "test");

//        }

//        private void BtnCerrar_Click(object sender, EventArgs e)
//        {
//            string HoraSalida = Convert.ToString(DateTime.Now);
//            int idAccess = sesion.idAcceso;
//            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
//            string conexion = "server=localhost;uid=root;database=nerivela";
//            string inserta_bitacora = "UPDATE bitacora SET HoraSalida = '" + HoraSalida + "' where idAcceso = " + idAccess + ";";
//            obj.insBitacora(conexion, inserta_bitacora);
//            System.Threading.Thread login = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));

//            login.Start();
//            this.Close();
//        }
//        public void exportardata(DataGridView dgw, string filename)
//        {

//            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
//            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
//            pdftable.DefaultCell.Padding = 3;
//            pdftable.WidthPercentage = 100;
//            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
//            pdftable.DefaultCell.BorderWidth = 1;
//            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 15, iTextSharp.text.Font.NORMAL);

//            foreach (DataGridViewColumn column in dataGridView1.Columns)
//            {
//                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
//                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
//                pdftable.AddCell(cell);
//            }

//            int row = dataGridView1.Rows.Count;
//            int cell2 = dataGridView1.Rows[1].Cells.Count;
//            for (int i = 0; i < row; i++)
//            {
//                for (int j = 0; j < cell2; j++)
//                {
//                    if (dataGridView1.Rows[i].Cells[j].Value == null)
//                    {
//                        //return directly
//                        //return;
//                        //or set a value for the empty data
//                        dataGridView1.Rows[i].Cells[j].Value = "null";
//                    }
//                    pdftable.AddCell(dataGridView1.Rows[i].Cells[j].Value.ToString());
//                }
//            }
//            //Exporting to PDF
//            string folderPath = @"C:\shashe\";
//            if (!Directory.Exists(folderPath))
//            {
//                Directory.CreateDirectory(folderPath);
//            }
//            using (FileStream stream = new FileStream(folderPath + "DataGridViewExport.pdf", FileMode.Create))
//            {
//                Document pdfDoc = new Document(PageSize.LETTER, 10f, 10f, 10f, 0f);
//                PdfWriter.GetInstance(pdfDoc, stream);
//                pdfDoc.Open();
//                //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:/Users/Tevi/Documents/Gestionde proyectos/controlEscolar-master/logo1.jpg");
//                string direccion = Directory.GetCurrentDirectory();
//                iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance(direccion + "/imagenes/logo.png");
//                imagen.BorderWidth = 0;
//                imagen.SetAbsolutePosition(0, 500);
//                float percentage = 0.0f;
//                percentage = 150 / imagen.Width;
//                imagen.ScalePercent(percentage * 100);
//                pdfDoc.Add(imagen);
//                var parrafo2 = new Paragraph("        Instituto Rodolfo Neri Vela");
//                parrafo2.SpacingBefore = 200;
//                parrafo2.SpacingAfter = 0;
//                parrafo2.Alignment = 1; //0-Left, 1 middle,2 Right
//                pdfDoc.Add(parrafo2);

//                var parrafo3 = new Paragraph("        Bitacora de Inicio de Sesión");

//                parrafo3.Alignment = 1; //0-Left, 1 middle,2 Right
//                pdfDoc.Add(parrafo3);

//                var parrafo4 = new Paragraph("        Vicente Guerrero 49 , Barrios Historicos , Acapulco Guerrero");

//                parrafo4.Alignment = 1; //0-Left, 1 middle,2 Right
//                pdfDoc.Add(parrafo4);

//                var parrafo5 = new Paragraph("    Clave:12DPT0003N       Nivel: Primaria");
//                parrafo5.Alignment = 1; //0-Left, 1 middle,2 Right


//                pdfDoc.Add(parrafo5);
//                pdfDoc.Add(Chunk.NEWLINE);












//                MessageBox.Show("Done");
//                // Abrimos el archivo  doc.Open();

//                //Image png = Image.GetInstance(imagepath + "/logo-sep.png");
//                // png.ScalePercent(24f);
//                //doc.Add(png);

//                // Escribimos el encabezamiento en el documento



//                pdfDoc.Add(pdftable);
//                pdfDoc.Close();
//                stream.Close();

//            }
//        }
//    }
//}
