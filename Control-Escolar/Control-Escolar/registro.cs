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
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Grey700, Primary.Grey900, Primary.Grey900, Accent.LightBlue200, TextShade.WHITE);
        }

        conexion obj = new conexion();

        string nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password;

        private void txtNum_Validating(object sender, CancelEventArgs e)
        {
            int num;
            if (!int.TryParse(txtNum.Text, out num))
            {
                errorProvider1.SetError(txtNum, "Ingese el valor en numeros");
            }
            else
            {

            }
        }

        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {

        }

        private void txtEmail_Click(object sender, EventArgs e)
        {

        }

        private void registro_Load(object sender, EventArgs e)
        {

        }

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

            string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
            //string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "insert into personal (nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password) " +
                "values('" + nombre + "','" + ApellidoP + "','" + ApellidoM + "','" + calle + "','" + colonia + "','" + numExt + "','" + cp + "','" + telefono + "','" + email + "','" + profesion + "','" + cargo + "','" + usuario + "','" + password + "');";

            obj.Consulta(conexion, query);
            Limpia();

            BorrarMensajesError();
            if (ValidarCampos())
            {
                MessageBox.Show("Datos ingresados Correctamente");
            }
            ValidarCampos();
        }

        private bool ValidarCampos()
        {
            bool ok = true;

            if(txtUsuario.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtUsuario, "Ingresar Usuario");
            }
            if (txtcontra.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtcontra, "Ingresar Contraseña");
            }
            if (txtNombre.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtNombre, "Ingresar Nombre");
            }
            if (txtApPat.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtApPat, "Ingresar Apellido Paterno");
            }
            if (txtApMat.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtApMat, "Ingresar Apellido Materno");
            }
            if (txtCalle.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtCalle, "Ingresar Calle");
            }
            if (txtColonia.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtColonia, "Ingresar Colonia");
            }
            if (txtNum.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtNum, "Ingresar Numero Exterior");
            }
            if (txtCP.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtCP, "Ingresar Codigo Postal");
            }
            if (txtTel.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtTel, "Ingresar Telefono");
            }
            if (txtEmail.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtEmail, "Ingresar Email");
            }
            if (txtProf.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtProf, "Ingresar Profeción");
            }
            if (txtCargo.Text == "")
            {
                ok = false;
                errorProvider1.SetError(txtCargo, "Ingresar Puesto");
            }
            return ok;
        }

        private void BorrarMensajesError()
        {
            errorProvider1.SetError(txtUsuario, "");
            errorProvider1.SetError(txtcontra, "");
            errorProvider1.SetError(txtNombre, "");
            errorProvider1.SetError(txtApPat, "");
            errorProvider1.SetError(txtApMat, "");
            errorProvider1.SetError(txtCalle, "");
            errorProvider1.SetError(txtColonia, "");
            errorProvider1.SetError(txtNum, "");
            errorProvider1.SetError(txtTel, "");
            errorProvider1.SetError(txtEmail, "");
            errorProvider1.SetError(txtProf, "");
            errorProvider1.SetError(txtCargo, "");
        }


    }
}
