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
    public partial class registro : MaterialForm
    {
        conexion obj = new conexion();
        Validar obje = new Validar();

        //---------------------------Variables globales------------------------------------------------------
        string nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password;

        bool DatoValidado = false;

        public registro()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);
           
        }

        
        //-------------------------------------Metodos---------------------------------------------------------
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
        //--------------------------------Metodo para validar todos los campos-----------------------------
        public bool ValidarTodosDatos()
        {
            if (this.txtUsuario.Text.Length == 0)//Usuario
            {
                errorProvider1.SetError(this.txtUsuario, "Ingresar el usuario");
                return false;
            }
            else
            {
                if (txtUsuario.Text.Length < 5)
                {
                    errorProvider1.SetError(this.txtUsuario, "Debe de ingresar minimo 5 caracteres");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(this.txtUsuario, "");
                }

            }

            if (this.txtcontra.Text.Length == 0)//Contraseña
            {
                errorProvider1.SetError(this.txtcontra, "Ingresar la contraseña");
                return false;
            }
            else
            {
                if (txtcontra.Text.Length < 8)
                {
                    errorProvider1.SetError(this.txtcontra, "Debe de ingresar minimo 8 caracteres");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(this.txtcontra, "");
                }
            }

            if (this.txtNombre.Text.Length == 0)//Nombre
            {
                errorProvider1.SetError(this.txtNombre, "Ingresar nombre");
                return false;
            }
            else
            {
                if (obje.IsString(txtNombre.Text))
                {
                    if (txtNombre.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtNombre, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtNombre, "");
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtNombre, "Solo ingrese letras");
                    return false;
                }

            }

            if (this.txtApPat.Text.Length == 0)//Apellido paterno
            {
                errorProvider1.SetError(this.txtApPat, "Ingresar apellido paterno");
                return false;
            }
            else
            {
                if (obje.IsString(txtApPat.Text))
                {
                    if (txtApPat.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtApPat, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtApPat, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtApPat, "Solo ingrese letras");
                    return false;
                }
            }

            if (this.txtApMat.Text.Length == 0)//Apellido materno
            {
                errorProvider1.SetError(this.txtApMat, "Ingresar apellido materno");
                return false;
            }
            else
            {
                if (obje.IsString(txtApMat.Text))
                {
                    if (txtApMat.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtApMat, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtApMat, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtApMat, "Solo ingrese letras");
                    return false;
                }
            }

            if (this.txtCalle.Text.Length == 0)//Calle
            {
                errorProvider1.SetError(this.txtCalle, "Ingresar nombre de la calle");
                return false;
            }
            else
            {
                errorProvider1.SetError(this.txtCalle, "");
            }

            if (this.txtNum.Text.Length == 0)//Numero Ext
            {
                errorProvider1.SetError(this.txtNum, "Ingresar número exterior");
                return false;
            }
            else
            {
                if (obje.IsNumeric(txtNum.Text))
                {
                    errorProvider1.SetError(this.txtNum, "");

                }
                else
                {
                    errorProvider1.SetError(this.txtNum, "Solo ingrese números");
                    return false;
                }
            }

            if (this.txtColonia.Text.Length == 0)//Colonia
            {
                errorProvider1.SetError(this.txtColonia, "Ingresar la colonia");
                return false;
            }
            else
            {
                errorProvider1.SetError(this.txtColonia, "");
            }

            if (this.txtCP.Text.Length == 0)//Código Postal
            {
                errorProvider1.SetError(this.txtCP, "Ingresar código postal");
                return false;
            }
            else
            {
                if (obje.IsNumeric(txtCP.Text))
                {
                    if (txtCP.Text.Length == 5)
                    {
                        errorProvider1.SetError(this.txtCP, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCP, "Ingrese solo 5 números");
                        return false;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCP, "Solo ingrese números");
                    return false;
                }
            }

            if (this.txtProf.Text.Length == 0)//Profesion
            {
                errorProvider1.SetError(this.txtProf, "Ingresar el su profesión");
                return false;
            }
            else
            {
                if (obje.IsString(txtProf.Text))
                {
                    if (txtProf.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtProf, "Ingrese mas de 3 letras");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtProf, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtProf, "Solo ingrese letras");
                    return false;
                }
            }
            if (this.txtCargo.Text.Length == 0)//Cargo
            {
                errorProvider1.SetError(this.txtCargo, "Ingresar su puesto");
                return false;
            }
            else
            {
                if (obje.IsString(txtCargo.Text))
                {
                    if (txtCargo.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtCargo, "Ingrese mas de 3 letras");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCargo, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCargo, "Solo ingrese letras");
                    return false;
                }
            }

            if (this.txtTel.Text.Length == 0)//Télefono
            {
                errorProvider1.SetError(this.txtTel, "Ingresar el télefono");
                return false;
            }
            else
            {
                if (obje.IsNumeric(txtTel.Text))
                {
                    if (txtTel.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtTel, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtTel, "Ingrese el télefono con código de área");
                        return false;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtTel, "Solo ingrese números");
                    return false;
                }
            }

            if (this.txtEmail.Text.Length == 0)//Email
            {
                errorProvider1.SetError(this.txtEmail, "Ingresar el email");
                return false;
            }
            else
            {
                if (txtEmail.Text.Length < 10)
                {
                    errorProvider1.SetError(this.txtEmail, "Ingrese mas de 10 caracteres");
                    return false;
                }
                else
                {
                    errorProvider1.SetError(this.txtEmail, "");
                    return true;
                }
            }
        }

        //---------------------------Metodo para validar datos parte 2-----------------------
        public void ValidarTodosDatos2()
        {
            if (this.txtUsuario.Text.Length == 0)//Usuario
            {
                errorProvider1.SetError(this.txtUsuario, "Ingresar el usuario");
                DatoValidado = false;
            }
            else
            {
                if (txtUsuario.Text.Length < 5)
                {
                    errorProvider1.SetError(this.txtUsuario, "Debe de ingresar minimo 5 caracteres");
                    DatoValidado = false;
                }
                else
                {
                    errorProvider1.SetError(this.txtUsuario, "");
                    DatoValidado = true;
                }

            }

            if (this.txtcontra.Text.Length == 0)//Contraseña
            {
                errorProvider1.SetError(this.txtcontra, "Ingresar la contraseña");
                DatoValidado = false;
            }
            else
            {
                if (txtcontra.Text.Length < 8)
                {
                    errorProvider1.SetError(this.txtcontra, "Debe de ingresar minimo 8 caracteres");
                    DatoValidado = false;
                }
                else
                {
                    errorProvider1.SetError(this.txtcontra, "");
                    DatoValidado = true;
                }
            }

            if (this.txtNombre.Text.Length == 0)//Nombre
            {
                errorProvider1.SetError(this.txtNombre, "Ingresar el usuario");
                DatoValidado = false;
            }
            else
            {
                if (obje.IsString(txtNombre.Text))
                {
                    if (txtNombre.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtNombre, "Ingrese mas de 3 caracteres");
                        DatoValidado = false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtNombre, "");
                        DatoValidado = true;
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtNombre, "Solo ingrese letras");
                    DatoValidado = false;
                }

            }

            if (this.txtApPat.Text.Length == 0)//Apellido paterno
            {
                errorProvider1.SetError(this.txtApPat, "Ingresar apellido paterno");
                DatoValidado = false;
            }
            else
            {
                if (obje.IsString(txtApPat.Text))
                {
                    if (txtApPat.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtApPat, "Ingrese mas de 3 caracteres");
                        DatoValidado = false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtApPat, "");
                        DatoValidado = true;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtApPat, "Solo ingrese letras");
                    DatoValidado = false;
                }
            }

            if (this.txtApMat.Text.Length == 0)//Apellido materno
            {
                errorProvider1.SetError(this.txtApMat, "Ingresar apellido materno");
                DatoValidado = false;
            }
            else
            {
                if (obje.IsString(txtApMat.Text))
                {
                    if (txtApMat.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtApMat, "Ingrese mas de 3 caracteres");
                        DatoValidado = false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtApMat, "");
                        DatoValidado = true;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtApMat, "Solo ingrese letras");
                    DatoValidado = false;
                }
            }

            if (this.txtCalle.Text.Length == 0)//Calle
            {
                errorProvider1.SetError(this.txtCalle, "Ingresar nombre de la calle");
                DatoValidado = false;
            }
            else
            {
                errorProvider1.SetError(this.txtCalle, "");
                DatoValidado = true;
            }

            if (this.txtNum.Text.Length == 0)//Numero Ext
            {
                errorProvider1.SetError(this.txtNum, "Ingresar número exterior");
                DatoValidado = false;
            }
            else
            {
                if (obje.IsNumeric(txtNum.Text))
                {
                    errorProvider1.SetError(this.txtNum, "");
                    DatoValidado = true;

                }
                else
                {
                    errorProvider1.SetError(this.txtNum, "Solo ingrese números");
                    DatoValidado = false;
                }
            }

            if (this.txtColonia.Text.Length == 0)//Colonia
            {
                errorProvider1.SetError(this.txtColonia, "Ingresar la colonia");
                DatoValidado = false;
            }
            else
            {
                errorProvider1.SetError(this.txtColonia, "");
                DatoValidado = true;
            }

            if (this.txtCP.Text.Length == 0)//Código Postal
            {
                errorProvider1.SetError(this.txtCP, "Ingresar código postal");
                DatoValidado = false;
            }
            else
            {
                if (obje.IsNumeric(txtCP.Text))
                {
                    if (txtCP.Text.Length == 5)
                    {
                        errorProvider1.SetError(this.txtCP, "");
                        DatoValidado = true;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCP, "Ingrese solo 5 números");
                        DatoValidado = false;

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCP, "Solo ingrese números");
                    DatoValidado = false;
                }
            }

            if (this.txtProf.Text.Length == 0)//Profesion
            {
                errorProvider1.SetError(this.txtProf, "Ingresar su profesión");
                DatoValidado = false;
            }
            else
            {
                if (obje.IsString(txtProf.Text))
                {
                    if (txtProf.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtProf, "Ingrese mas de 3 letras");
                        DatoValidado = false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtProf, "");
                        DatoValidado = true;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtProf, "Solo ingrese letras");
                    DatoValidado = false;
                }
            }
            if (this.txtCargo.Text.Length == 0)//Cargo
            {
                errorProvider1.SetError(this.txtCargo, "Ingresar su puesto");
                DatoValidado = false;
            }
            else
            {
                if (obje.IsString(txtCargo.Text))
                {
                    if (txtCargo.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtCargo, "Ingrese mas de 3 letras");
                        DatoValidado = false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCargo, "");
                        DatoValidado = true;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCargo, "Solo ingrese letras");
                    DatoValidado = false;
                }
            }

            if (this.txtTel.Text.Length == 0)//Télefono
            {
                errorProvider1.SetError(this.txtTel, "Ingresar el télefono");
                DatoValidado = false;
            }
            else
            {
                if (obje.IsNumeric(txtTel.Text))
                {
                    if (txtTel.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtTel, "");
                        DatoValidado = true;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtTel, "Ingrese el télefono con código de área");
                        DatoValidado = false;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtTel, "Solo ingrese números");
                    DatoValidado = false;
                }
            }

            if (this.txtEmail.Text.Length == 0)//Email
            {
                errorProvider1.SetError(this.txtEmail, "Ingresar el email");
                DatoValidado = false;
            }
            else
            {
                if (txtEmail.Text.Length < 10)
                {
                    errorProvider1.SetError(this.txtEmail, "Ingrese mas de 10 caracteres");
                    DatoValidado = false;
                }
                else
                {
                    errorProvider1.SetError(this.txtEmail, "");
                    DatoValidado = true;
                }
            }
        }

        //-------------------------Metodo Validating--------------------------------------------------
        //Usuario
        private void txtUsuario_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtUsuario.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtUsuario, "Ingresar el usuario");
            }
            else
            {
                if (txtUsuario.Text.Length < 5)
                {
                    errorProvider1.SetError(this.txtUsuario, "Debe de ingresar minimo 5 caracteres");
                }
                else
                {
                    errorProvider1.SetError(this.txtUsuario, "");
                }
            }
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
                if (txtcontra.Text.Length < 8)
                {
                    errorProvider1.SetError(this.txtcontra, "Debe de ingresar minimo 8 caracteres");
                }
                else
                {
                    errorProvider1.SetError(this.txtcontra, "");
                }
            }
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
                if (obje.IsString(txtNombre.Text))
                {
                    if (txtNombre.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtNombre, "Ingrese mas de 3 caracteres");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtNombre, "");
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtNombre, "Solo ingrese letras");
                }
            }
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
                if (obje.IsString(txtApPat.Text))
                {
                    if (txtApPat.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtApPat, "Ingrese mas de 3 caracteres");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtApPat, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtApPat, "Solo ingrese letras");
                }
            }
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
                if (obje.IsString(txtApMat.Text))
                {
                    if (obje.IsString(txtApMat.Text))
                    {
                        if (txtApMat.Text.Length < 3)
                        {
                            errorProvider1.SetError(this.txtApMat, "Ingrese mas de 3 caracteres");
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtApMat, "");
                        }

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtApMat, "Solo ingrese letras");
                }
            }
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

        //Codigo postal
        private void txtCP_Validating_1(object sender, CancelEventArgs e)
        {
            if (this.txtCP.Text.Length == 0)//Código Postal
            {
                errorProvider1.SetError(this.txtCP, "Ingresar código postal");
            }
            else
            {
                if (obje.IsNumeric(txtCP.Text))
                {
                    if (txtCP.Text.Length == 5)
                    {
                        errorProvider1.SetError(this.txtCP, "");

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCP, "Ingrese solo 5 números");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCP, "Solo ingrese números");
                }
            }
        }

        //Numero exterior
        private void txtNum_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtNum.Text.Length == 0)//Numero Ext
            {
                errorProvider1.SetError(this.txtNum, "Ingresar número exterior");
            }
            else
            {
                if (obje.IsNumeric(txtNum.Text))
                {
                    errorProvider1.SetError(this.txtNum, "");

                }
                else
                {
                    errorProvider1.SetError(this.txtNum, "Solo ingrese números");
                }
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
                if (obje.IsString(txtProf.Text))
                {
                    if (txtProf.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtProf, "Ingrese mas de 3 letras");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtProf, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtProf, "Solo ingrese letras");
                }
            }
        }

        //Cargo
        private void txtCargo_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCargo.Text.Length == 0)//Cargo
            {
                errorProvider1.SetError(this.txtCargo, "Ingresar su cargo");
            }
            else
            {
                if (obje.IsString(txtCargo.Text))
                {
                    if (txtCargo.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtCargo, "Ingrese mas de 3 letras");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCargo, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCargo, "Solo ingrese letras");
                }
            }
        }

        //Telefono
        private void txtTel_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtTel.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtTel, "Ingresar el télefono");
            }
            else
            {
                if (obje.IsNumeric(txtTel.Text))
                {
                    if (txtTel.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtTel, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtTel, "Ingrese el télefono con código de área");

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtTel, "Solo ingrese números");
                }
            }
        }

        //Email
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtEmail.Text.Length == 0)//Email
            {
                errorProvider1.SetError(this.txtEmail, "Ingresar el email");
            }
            else
            {
                if (txtEmail.Text.Length < 10)
                {
                    errorProvider1.SetError(this.txtEmail, "Ingrese mas de 10 caracteres");
                }
                else
                {
                    errorProvider1.SetError(this.txtEmail, "");
                }
            }
        }

    
        //-------------------------------Botones---------------------------------------------------
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
            bool validar = ValidarTodosDatos();
             ValidarTodosDatos2();

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
            

            if(validar == true)
            {
                //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
                string conexion = "server=localhost;uid=root;database=nerivela";
                string query = "insert into personal (nombre, ApellidoP, ApellidoM, calle, colonia, numExt, cp, telefono, email, profesion, cargo, usuario, password) " +
                "values('" + nombre + "','" + ApellidoP + "','" + ApellidoM + "','" + calle + "','" + colonia + "','" + numExt + "','" + cp + "','" + telefono + "','" + email + "','" + profesion + "','" + cargo + "','" + usuario + "','" + password + "');";

                obj.Consulta(conexion, query);
                Limpia();
            }
            else
            {
                MessageBox.Show("Error en los datos");
            }
        }
        
    }
}
