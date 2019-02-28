﻿using System;
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
    public partial class principal : MaterialForm
    {
        conexion obj = new conexion();

        public static void ThreadProc()

        {
            Application.Run(new login());
        }

        public static void ThreadBitacora()

        {
            Application.Run(new bitacora());
        }

        public static void ThreadAlumno()

        {
            Application.Run(new Alumno());
        }

        public static void ThreadGrupo()

        {
            Application.Run(new Grupos());
        }

        public principal()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue700, Primary.LightBlue400, Primary.Blue700, Accent.LightBlue200, TextShade.WHITE);
            string user = sesion.Usuario;
            //MessageBox.Show("Hola "+ user + " Bienvenido al Control Escolar");
            string entrada = sesion.HoraEntrada;
            lblBienvenida.Text = "Hola " + user + " \nBienvenido al Control Escolar\nTu hora de entrada fue a las: "+ entrada + "";
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

        private void principal_Load(object sender, EventArgs e)
        {

        }

        private void btnBitacora_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBitacora));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void btnAlumnos_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla1 = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadAlumno));
            pantalla1.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void BtnGrupos_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla1 = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadGrupo));
            pantalla1.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
    }
}
