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
    public partial class Buscar : MaterialForm
    {
        public Buscar()
        {
            InitializeComponent();
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

        public static void ThreadBuscar()

        {
            Application.Run(new Buscar());
        }

        public static void ThreadAlumno()

        {
            Application.Run(new Alumno());
        }

        private void Buscar_Load(object sender, EventArgs e)
        {

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

        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBuscar));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void BtnInscripcion_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadAlumno));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
    }
}
