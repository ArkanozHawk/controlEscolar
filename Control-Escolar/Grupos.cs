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


namespace Control_Escolar
{
    public partial class Grupos : MaterialForm
    {
        public Grupos()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);
            string conexion = "server=localhost;uid=root;database=nerivela";
            string posicion = "SELECT * FROM Maestros;";
            obj.grupos(conexion, posicion);
            List<string> nombre = new List<string>();
            List<string> apellidoP = new List<string>();
            List<string> apellidoM = new List<string>();
            nombre = sesion.nombreU;
            apellidoP = sesion.apellidoP;
            apellidoM = sesion.apellidoM;
            lblMaestro1.Text = nombre[0] + " " + apellidoP[0] + " " + apellidoM[0];
            lblMaestro2.Text = nombre[1] + " " + apellidoP[1] + " " + apellidoM[1];
            lblMaestro3.Text = nombre[2] + " " + apellidoP[2] + " " + apellidoM[2];
            lblMaestro4.Text = nombre[3] + " " + apellidoP[3] + " " + apellidoM[3];
            lblMaestro5.Text = nombre[4] + " " + apellidoP[4] + " " + apellidoM[4];
            lblMaestro6.Text = nombre[5] + " " + apellidoP[5] + " " + apellidoM[5];

        }

        conexion obj = new conexion();

        public static void ThreadProc()

        {
            Application.Run(new login());
        }

        public static void ThreadPrincipal()

        {
            Application.Run(new principal());
        }

        public static void ThreadCadaGrupo()

        {
            Application.Run(new CadaGrupo());
        }

        private void Grupos_Load(object sender, EventArgs e)
        {

        }

        private void BtnCerrar_Click(object sender, EventArgs e)
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

        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel14_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            sesion.pictureb1 = "1";

            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCadaGrupo));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
            

        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            sesion.pictureb1 = "6";
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCadaGrupo));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            sesion.pictureb1 = "5";

            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCadaGrupo));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            sesion.pictureb1 = "4";

            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCadaGrupo));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            sesion.pictureb1 = "3";

            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCadaGrupo));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            sesion.pictureb1 = "2";

            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCadaGrupo));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void materialLabel11_Click(object sender, EventArgs e)
        {

        }

        private void Grupos_FormClosing(object sender, FormClosingEventArgs e)
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
