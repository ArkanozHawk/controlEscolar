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
    public partial class Graficas_Estad : MaterialForm
    {
        public Graficas_Estad()
        {
            InitializeComponent();
        }

        conexion obj = new conexion();
        
        public static void ThreadPrincipal()
        {
            Application.Run(new principal());
        }

        public static void ThreadProc()
        {
            Application.Run(new login());
        }

        public static void ThreadEstadisticas()
        {
            Application.Run(new Estadisticas ());
        }

        private void btnCerrar_Click(object sender, EventArgs e)
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

        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadEstadisticas));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void Graficas_Estad_Load(object sender, EventArgs e)
        {

        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }
    }
}
