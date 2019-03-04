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
    public partial class Docuementos : MaterialForm
    {
        public Docuementos()
        {
            InitializeComponent();
        }

        conexion obj = new conexion();

        //-----------------------------------Metodos------------------------------------------------
        //Cerrar sesion
        public static void ThreadProc()

        {
            Application.Run(new login());
        }
        //Volver a la pagina principal
        public static void ThreadPrincipal()

        {
            Application.Run(new principal());
        }

        //-----------------------------------------Botones--------------------------------------------
        //Boton de cerrar sesion
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
        //Boton para volver al menu principal
        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        //---------------------------------------------------------------------------------------

        private void Docuementos_Load(object sender, EventArgs e)
        {
            //Borrar
        }

        //-------------------------------------Eventos clic------------------------------------------
        //Descargar directorio de alumnos de cada grado
        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        //Descargar directorio de maestros
        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
    }
}
