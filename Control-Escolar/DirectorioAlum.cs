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
    public partial class DirectorioAlum : MaterialForm
    {
        MySqlCommand codigo = new MySqlCommand();
        MySqlConnection conectanos = new MySqlConnection();
        //MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela;pwd=digi3.0");
        MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela");
        conexion obj = new conexion();

        public DirectorioAlum()
        {
           
            InitializeComponent();

            datagrid(dataGridView1);
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);
            mostrardatoscadagrupo();
            
            
        }


        //-------------------------------------Metodos----------------------------------------------
        //Cerrar sesion
        public static void ThreadProc()

        {
            Application.Run(new login());
        }
        //Volver al menu principal
        public static void ThreadPrincipal()

        {
            Application.Run(new principal());
        }

        //-----------------------------------------Botones--------------------------------------------
        //Cerrar sesion
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            string HoraSalida = DateTime.Now.ToString("hh:mm:ss");
            int idAccess = sesion.idAcceso;
            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
            string conexion = "server=localhost;uid=root;database=nerivela";
            string inserta_bitacora = "UPDATE bitacora SET HoraSalida = '" + HoraSalida + "' where idAcceso = " + idAccess + ";";
            obj.insBitacora(conexion, inserta_bitacora);
            System.Threading.Thread login = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));

            login.Start();
            this.Close();
        }
        //Volver al menu principal
        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
        //Generar pdf del grupo
        private void GenerarPDF_Click(object sender, EventArgs e)
        {
            exportardata(dataGridView1, "test");

            MessageBox.Show("¡PDF creado!");
        }

        //-------------------------------------------------------------------------------------------
        private void CadaGrupo_Load(object sender, EventArgs e)
        {

        }


        public void mostrardatoscadagrupo()
        {


        }


        public void datagrid(DataGridView data)
        {

            coneccion.Open();
            codigo.Connection = coneccion;
            codigo.CommandText = ("SELECT    `ApellidoP`, `ApellidoM`, `nombre`, `CURP`,`idGrado`, `Alergias` FROM  `alumno` ORDER BY  `ApellidoP`  ASC");


            try
            {
                MySqlDataAdapter seleccionar = new MySqlDataAdapter();
                seleccionar.SelectCommand = codigo;

                DataTable datostabla = new DataTable();
                DataColumn numerodelista = new DataColumn();
                numerodelista.ColumnName = "Numero de lista";
                numerodelista.DataType = System.Type.GetType("System.Int32");
                numerodelista.AutoIncrement = true;
                numerodelista.AutoIncrementSeed = 1;
                 numerodelista.AutoIncrementStep = 1;
                datostabla.Columns.Add(numerodelista);
              
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
            using (FileStream stream = new FileStream(folderPath + "Directorio_Alumnos.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.LETTER, 10f, 10f, 100f, 100f); //se declara las medidas y margenes del pdf por ejemplo tamaño CARTA
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);//cosas de itextsharp xD

                //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:/Users/Tevi/Documents/Gestionde proyectos/controlEscolar-master/logo1.jpg");
                string direccion = Directory.GetCurrentDirectory();//obtenemos direccion no se paque xD

                //aqui empezamos agregar cosas :3
                pdfDoc.Open();//se habre el docuemnto
                // Header hola = new Header();
               // writer.PageEvent = new Header();
                Header i = new Header();
                i.HeaderDirectorioAlumno(writer,pdfDoc);

               //se habre el docuemnto

                // pdfDoc.NewPage();

                //aqui pones todo lo que va en medio :D
                pdfDoc.Add(pdftable);
                pdfDoc.Close();
                stream.Close();
            }

        }
    }
}
