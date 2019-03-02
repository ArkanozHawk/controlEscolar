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
        conexion obj = new conexion();

        public registro()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);

            //this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
        }

        //Variables globales-------------------------------------------------------------
        string nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password;

        //---------------Metodos---------------------------------------------------------
        //Volver al menu principal
        public static void ThreadProc()
        {
            Application.Run(new login());
        }
        //Limpiar datos
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

        //-------------Metodo Validating--------------------------------------------------
        //Usuario
        private void txtUsuario_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtUsuario.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtUsuario, "Ingresar el usuario");
            }
            else
            {
                errorProvider1.SetError(this.txtUsuario, "");
            }
        }

        private void txtUsuario_Validated(object sender, EventArgs e)
        {
            //Borrar
        }

        //Contraseña
        private void txtcontra_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtcontra.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtcontra, "Ingresar la contraseña");
            }
            else
            {
            errorProvider1.SetError(this.txtcontra, "");
            }
        }

        private void txtcontra_Validated(object sender, EventArgs e)
        {
            //Borrar
        }
        //Nombre
        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtNombre.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtNombre, "Ingresar el nombre");
            }
            else
            {
                errorProvider1.SetError(this.txtNombre, "");
            }
        }
        
        private void txtNombre_Validated(object sender, EventArgs e)
        {
            //borrar
        }
        //Apellido paterno
        private void txtApPat_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtApPat.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtApPat, "Ingresar apellido paterno");
            }
            else
            {
                errorProvider1.SetError(this.txtApPat, "");
            }
        }

        private void txtApPat_Validated(object sender, EventArgs e)
        {
            //Borrar
        }

        //Apellido materno
        private void txtApMat_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtApMat.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtApMat, "Ingresar apellido materno");
            }
            else
            {
                errorProvider1.SetError(this.txtApMat, "");
            }
        }

        private void txtApMat_Validated(object sender, EventArgs e)
        {
            //Borrar
        }
        //Calle
        private void txtCalle_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCalle.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtCalle, "Ingresar nombre de la calle");
            }
            else
            {
            errorProvider1.SetError(this.txtCalle, "");
            }
        }
        private void txtCalle_Validated(object sender, EventArgs e)
        {
            //Borrar
        }

        //Colonia
        private void txtColonia_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtColonia.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtColonia, "Ingresar la colonia");
            }
            else
            {
                errorProvider1.SetError(this.txtColonia, "");
            }
        }

        private void txtColonia_Validated(object sender, EventArgs e)
        {
            //Borrar
        }

        //Codigo postal
        private void txtCP_Validating_1(object sender, CancelEventArgs e)
        {
            int num1;
            if (!int.TryParse(txtCP.Text, out num1))
            {
                errorProvider1.SetError(txtCP, "Ingesar el CP en numeros");
            }
            else
            {
                errorProvider1.SetError(txtCP, "");
            }
        }

        //Numero exterior
        private void txtNum_Validating(object sender, CancelEventArgs e)
        {
            int num;
            if (!int.TryParse(txtNum.Text, out num))
            {
                errorProvider1.SetError(txtNum, "Ingesar el dato en numeros");
            }
            else
            {
                 errorProvider1.SetError(txtNum, "");
            }
        }

        //Profesion
        private void txtProf_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtProf.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtProf, "Ingresar el su profesión");
            }
            else
            {
                errorProvider1.SetError(this.txtProf, "");
            }
        }

        //Cargo
        private void txtCargo_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCargo.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtCargo, "Ingresar su puesto");
            }
            else
            {
                errorProvider1.SetError(this.txtCargo, "");
            }
        }

        //Telefono
        private void txtTel_Validating(object sender, CancelEventArgs e)
        {
            int num2;
            if (!int.TryParse(txtTel.Text, out num2))
            {
                errorProvider1.SetError(txtTel, "Ingesar el teléfono en numeros");
            }
            else
            {
                errorProvider1.SetError(txtTel, "");

            }
        }

        //Email
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtEmail.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtEmail, "Ingresar el email");
            }
            else
            {
                errorProvider1.SetError(this.txtEmail, "");
            }
        }

        //----------------------------------------------------------------------------
        private void txtEmail_Click(object sender, EventArgs e)
        {
            //Quitar este evento clic
        }

        private void txtCP_Click(object sender, EventArgs e)
        {
            //Quitar este evento clic
        }

        private void registro_Load(object sender, EventArgs e)
        {
            //Quitar esto
        }

        private void txtEmail_Validated(object sender, EventArgs e)
        {
            //Borrar
        }

        private void txtTel_Validated(object sender, EventArgs e)
        {
            //Borrar
        }

        private void txtCargo_Validated(object sender, EventArgs e)
        {
            //Borrar
        }

        private void txtProf_Validated(object sender, EventArgs e)
        {
            //Borrar
        }

        private void txtNum_Validated(object sender, EventArgs e)
        {
            //Borrar
        }

        private void txtCP_Validated(object sender, EventArgs e)
        {
            //Borrar
        }

        //------------------------Botones---------------------------------------------------
        //Limpiar
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpia();
        }
        //Volver
        private void btnVolver_Click(object sender, EventArgs e)
        {
            System.Threading.Thread principal = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
            principal.Start();
            this.Close();
        }
        //Registrar alumno
        private void btnRegistrar_Click(object sender, EventArgs e)
        {

            if (this.ValidateChildren(ValidationConstraints.Enabled))
            {
                //Todo es correcto, guardamos los datos
            }
            else
            {
                MessageBox.Show("Faltan algunos campos por rellenar");
            }
            
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

            //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "insert into personal (nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password) " +
                "values('" + nombre + "','" + ApellidoP + "','" + ApellidoM + "','" + calle + "','" + colonia + "','" + numExt + "','" + cp + "','" + telefono + "','" + email + "','" + profesion + "','" + cargo + "','" + usuario + "','" + password + "');";

            obj.Consulta(conexion, query);
            Limpia();


        }
        
    }
}
