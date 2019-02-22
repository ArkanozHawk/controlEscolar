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

        public bitacora()
        {
            InitializeComponent();
            datagrid(dataGridView1);
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey700, Primary.Grey900, Primary.Grey900, Accent.LightBlue200, TextShade.WHITE);
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerarPDF_Click(object sender, EventArgs e)
        {
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc,
                                        new FileStream(@"C:\Users\Lizeth\Desktop\prueba.pdf", FileMode.Create));

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Mi primer PDF");
            doc.AddCreator("Martha Lizeth");

            // Abrimos el archivo
            doc.Open();

            // Escribimos el encabezamiento en el documento
            doc.Add(new Paragraph("Mi primer documento PDF"));
            doc.Add(Chunk.NEWLINE);

            iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            // Creamos una tabla que contendrá el nombre, apellido y país 
            // de nuestros visitante.
            PdfPTable tblPrueba = new PdfPTable(3);
            tblPrueba.WidthPercentage = 100;

            // Configuramos el título de las columnas de la tabla
            PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", _standardFont));
            clNombre.BorderWidth = 0;
            clNombre.BorderWidthBottom = 0.75f;

            PdfPCell clApellido = new PdfPCell(new Phrase("Apellido", _standardFont));
            clApellido.BorderWidth = 0;
            clApellido.BorderWidthBottom = 0.75f;

            PdfPCell clPais = new PdfPCell(new Phrase("País", _standardFont));
            clPais.BorderWidth = 0;
            clPais.BorderWidthBottom = 0.75f;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clApellido);
            tblPrueba.AddCell(clPais);

            // Llenamos la tabla con información
            clNombre = new PdfPCell(new Phrase("Mayolo", _standardFont));
            clNombre.BorderWidth = 0;

            clApellido = new PdfPCell(new Phrase("Suaste", _standardFont));
            clApellido.BorderWidth = 0;

            clPais = new PdfPCell(new Phrase("Puerto Rico", _standardFont));
            clPais.BorderWidth = 0;

            // Añadimos las celdas a la tabla
            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clApellido);
            tblPrueba.AddCell(clPais);

            clNombre = new PdfPCell(new Phrase("Martha", _standardFont));
            clNombre.BorderWidth = 0;

            clApellido = new PdfPCell(new Phrase("Teran", _standardFont));
            clApellido.BorderWidth = 0;

            clPais = new PdfPCell(new Phrase("México", _standardFont));
            clPais.BorderWidth = 0;

            tblPrueba.AddCell(clNombre);
            tblPrueba.AddCell(clApellido);
            tblPrueba.AddCell(clPais);

            // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
            doc.Add(tblPrueba);

            doc.Close();
            writer.Close();

            MessageBox.Show("¡PDF creado!");
        }
    }
}
