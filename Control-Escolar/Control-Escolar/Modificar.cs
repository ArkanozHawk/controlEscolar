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
using ValidarDatos;

namespace Control_Escolar
{
    public partial class Modificar : MaterialForm
    {
        public Modificar()
        {
            InitializeComponent();
        }

        conexion obj = new conexion();
        Validar obje = new Validar();

        //-------------------------------------------Metodos----------------------------------------
        //Volver al menu principal
        public static void ThreadPrincipal()
        {
            Application.Run(new principal());
        }
        //Cerrar sesion
        public static void ThreadProc()
        {
            Application.Run(new login());
        }
        //Buscar
        public static void ThreadBuscar()
        {
            Application.Run(new Buscar());
        }

        //Inscripcion
        public static void ThreadAlumno()
        {
            Application.Run(new Alumno());
        }

        //Modificar
        public static void ThreadModificarPadre()
        {
            Application.Run(new ModificarPadre());
        }

        //---------------------------------------------Botones------------------------------------

        private void btnInscripcion_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadAlumno));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBuscar));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBuscar));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
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

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadModificarPadre));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
    }
}
