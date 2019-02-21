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
    public partial class registro : MaterialForm
    {
        public static void ThreadProc()

        {
            Application.Run(new login());
        }

        public void Limpia()
        {
            txtNombre.Text = null;
            txtApPat.Text = null;
            txtApMat.Text = null;
            txtCalle.Text = null;
            txtColonia.Text = null;
            txtNum.Text = null;
            txtCP.Text = null;
            txtTel.Text = null;
            txtEmail.Text = null;
            txtProf.Text = null;
            txtCargo.Text = null;
            txtUsuario.Text = null;
            txtcontra.Text = null;
        }

        public registro()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }

        conexion obj = new conexion();

        string nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password;

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpia();
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            System.Threading.Thread principal = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));

            principal.Start();
            this.Close();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            nombre = txtNombre.Text;
            ApellidoP = txtApPat.Text;
            ApellidoM = txtApMat.Text;
            calle = txtCalle.Text;
            colonia = txtColonia.Text;
            numExt = txtNum.Text;
            cp = txtCP.Text;
            telefono = txtTel.Text;
            email = txtEmail.Text;
            profesion = txtProf.Text;
            cargo = txtCargo.Text;
            usuario = txtUsuario.Text;
            password = txtcontra.Text;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "insert into personal (nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password) " +
                "values('" + nombre + "','" + ApellidoP + "','" + ApellidoM + "','" + calle + "','" + colonia + "','" + numExt + "','" + cp + "','" + telefono + "','" + email + "','" + profesion + "','" + cargo + "','" + usuario + "','" + password + "');";

            obj.Consulta(conexion, query);
            Limpia();
        }
    }
}
