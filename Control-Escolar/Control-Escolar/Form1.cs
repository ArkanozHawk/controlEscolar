using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;


namespace Control_Escolar
{
    public partial class login : MaterialForm
    {
        public static void ThreadProc()

        {
            Application.Run(new registro());
        }

        public static void ThreadPrincipal()

        {
            Application.Run(new principal());
        }

        public login()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey700, Primary.Grey900, Primary.Grey900, Accent.LightBlue200, TextShade.WHITE);
        }

        conexion obj = new conexion();

        string nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password;

        public void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            this.Hide();

            bitacora frm = new bitacora();

            frm.Show();

            try
            {
                string path = AppDomain.CurrentDomain.BaseDirectory + "wkhtmltopdf/bin/wkhtmltopdf.exe";
                ProcessStartInfo proceso1 = new ProcessStartInfo();

                proceso1.UseShellExecute = false;
                proceso1.FileName = path;
                proceso1.Arguments = "mihtml.html C:/Users/Tevi/Desktop/mipdf2.pdf";
                proceso1.WorkingDirectory = @"C:\Users\Tevi\Documents\Gestionde proyectos\controlEscolar-master\Control-Escolar\Control-Escolar";
                Console.Read();
                using (Process oProcess = Process.Start(proceso1))
                {
                    oProcess.WaitForExit();
                    Console.Read();

                }

                MessageBox.Show("pdf creado");

            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);


            foreach (DataGridViewColumn colum in dgw.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(colum.HeaderText, text));

                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdftable.AddCell(cell);


            }

            foreach (DataGridViewRow row in dgw.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    pdftable.AddCell(new Phrase(cell.Value.ToString(), text));
                }
            }
            var savefiledialoge = new SaveFileDialog();
            savefiledialoge.FileName = filename;
            savefiledialoge.DefaultExt = ".pdf";
            if (savefiledialoge.ShowDialog() == DialogResult.OK)
            {
                using (FileStream stream = new FileStream(savefiledialoge.FileName, FileMode.Create))
                {
                    Document pdfdoc = new Document(PageSize.A4, 10f, 10f, 10f, 0f);
                    PdfWriter.GetInstance(pdfdoc, stream);
                    pdfdoc.Open();
                    pdfdoc.Add(pdftable);
                    pdfdoc.Close();
                    stream.Close();
                }
            }

        }




        private void button1_Click(object sender, EventArgs e)
        {
            string path = AppDomain.CurrentDomain.BaseDirectory + "wkhtmltopdf/wkhtmltopdf.exe";
            ProcessStartInfo proceso1 = new ProcessStartInfo();

            proceso1.UseShellExecute = false;
            proceso1.FileName = path;
            proceso1.Arguments = "https://www.facebook.com/ mipdf.pdf";
            using (Process oProcess = Process.Start(proceso1))
            {
                oProcess.WaitForExit();

            }
            Console.WriteLine("PDF Creado");
            Console.Read();
            try
                { }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Titulo_Click(object sender, EventArgs e)
        {

        }

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
            System.Threading.Thread registrando = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));

            registrando.Start();
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {

            usuario = txtUsuario.Text;
            password = txtContra.Text;

            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT COUNT(*) FROM personal where usuario = '"+ usuario + "' and password = '"+ password +"';";

            int resultado = obj.Consul(conexion, query);
            if (resultado == 1)
            {
                sesion.Usuario = usuario;
                sesion.Password = password;
                sesion.HoraEntrada = Convert.ToString(DateTime.Now);
                string HoraEntrada = sesion.HoraEntrada;
                string inserta_bitacora = "INSERT INTO bitacora (Usuario,HoraEntrada) " + "values('" + usuario + "','" + HoraEntrada + "');";
                obj.insBitacora(conexion, inserta_bitacora);
                string posicion = "SELECT idAcceso FROM bitacora ORDER by idAcceso DESC limit 1;";
                int posi = obj.Acceso(conexion, posicion);
                sesion.idAcceso = posi;
                System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
                pantalla.Start();
                CheckForIllegalCrossThreadCalls = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("Datos Invalidos, prueba de nuevo");
            }
            
        }
    }
}
