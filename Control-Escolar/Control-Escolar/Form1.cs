﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Animations;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;

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
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);
        }

        conexion obj = new conexion();

        string nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password, Fecha, HoraEntrada, ApellidoPU, NombreU, ApellidoMU;

        private void txtUsuario_Click(object sender, EventArgs e)
        {

        }

        private void login_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

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
            MySqlConnection conn;
            MySqlCommand com;

            usuario = txtUsuario.Text;
            password = txtContra.Text;


            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT COUNT(*) FROM personal where usuario = '" + usuario + "' and password = '" + password + "';";
            string query1 = "SELECT * FROM personal where usuario = '" + usuario + "' and password = '" + password + "';";
            int resultado = obj.Consul(conexion, query);
            if (resultado == 1)
            {
                conn = new MySqlConnection(conexion);
                conn.Open();

                com = new MySqlCommand(query1, conn);

                MySqlDataReader myreader = com.ExecuteReader();

                myreader.Read();


                NombreU = Convert.ToString(myreader["Nombre"]);
                ApellidoPU = Convert.ToString(myreader["ApellidoP"]);


                sesion.Usuario = usuario;
                sesion.Password = password;
                Fecha = DateTime.Now.ToString("dd/MM/yyyy");


                HoraEntrada = DateTime.Now.ToString("hh:mm:ss");


                string inserta_bitacora = "INSERT INTO bitacora (Usuario,Nombre,Apellido,Fecha,HoraEntrada) " + "values('" + usuario + "','" + NombreU + "','" + ApellidoPU + "','" + Fecha + "','" + HoraEntrada + "');";
                obj.insBitacora(conexion, inserta_bitacora);
                string posicion = "SELECT idAcceso FROM bitacora ORDER by idAcceso DESC limit 1;";
                int posi = obj.Acceso(conexion, posicion);
                sesion.idAcceso = posi;
                sesion.HoraEntrada = HoraEntrada;
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
