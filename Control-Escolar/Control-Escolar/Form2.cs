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
using MaterialSkin.Controls;
using ValidarDatos;

namespace Control_Escolar
{
    public partial class Alumno : MaterialForm
    {
        public Alumno()
        {
            InitializeComponent();

            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);
        }

        conexion obj = new conexion();
        Validar obje = new Validar();

        bool DatoValidado = false;

        //-------------------------------------------Metodos------------------------------------------
        //Cerrar sesion
        public static void ThreadProc()

        {
            Application.Run(new login());
        }
        //Volver al menu principal
        public static void ThreadPrincipal()

        {
            Application.Run(new principal());
        }
        //Ir al siguiente formulario de padres
        public static void ThreadForm3()

        {
            Application.Run(new Form3());
        }
        //Buscar
        public static void ThreadBuscar()

        {
            Application.Run(new Buscar());
        }
        //Calcular la edad
        public void CalcEdad(string fnac)
        {
            DateTime dat = Convert.ToDateTime(fnac);
            DateTime nacimiento = new DateTime(dat.Year, dat.Month, dat.Day);
            int edad1 = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            MessageBox.Show(edad1.ToString());
            int edad2 = Convert.ToInt32(txtEdad_A.Text);
            if (edad1 == edad2)
            {
                sesion.edad = edad1;
            }

            else { MessageBox.Show("No coincide la edad con fecha de nacimiento "); }

        }

        //Inscripcion
        public void inscripcion()
        {
            string Nombre_T, AP_T, AM_T, Calle_T, Numero_T, Colonia_T, CP_T, Telefono_T, Celular_T, Profesion_T, LT_T;
            sesion.nombre = txtNombre_A.Text;
            sesion.AP = txtApPat_A.Text;
            sesion.AM = txtApMat_A.Text;
            sesion.Curp = txtCURP_A.Text;
            sesion.calle = txtNum_A.Text;
            sesion.numero = txtNum_A.Text;
            sesion.Colonia = txtColonia_C.Text;
            sesion.CP = txtCP_A.Text;
            sesion.LN = txtLugarNac_A.Text;

            sesion.edad = Convert.ToInt32(txtEdad_A.Text);

            sesion.telefono = txtNombre_A.Text;
            sesion.Alergia = txtAlergias_A.Text;

        }

        //------------------------------------------Botones-------------------------------------------
        //Cerrar sesion
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
        //Volver al menu principal
        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
        //Ir al siguiente formulario
        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            //bool validar = ValidarTodosDatos();
            //ValidarTodosDatos2();

            string nacimiento = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            sesion.fnac = nacimiento;
            CalcEdad(sesion.fnac);
            inscripcion();
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadForm3));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
        //Buscar
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBuscar));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
        //Modificar
        private void btnModificar_Click(object sender, EventArgs e)
        {

        }
        //Eliminar
        private void Eliminar_Click(object sender, EventArgs e)
        {

        }

        //--------------------------------Metodo para validar todos los campos-----------------------------
        /*public bool ValidarTodosDatos()
        {

            if (this.txtNombre_A.Text.Length == 0)//Nombre
            {
                errorProvider1.SetError(this.txtNombre_A, "Ingresar el nombre del alumno");
                return false;
            }
            else
            {
                if (obje.IsString(txtNombre_A.Text))
                {
                    if (txtNombre_A.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtNombre_A, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtNombre_A, "");
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtNombre_A, "Solo ingrese letras");
                    return false;
                }

            }

            if (this.txtApPat_A.Text.Length == 0)//Apellido paterno
            {
                errorProvider1.SetError(this.txtApPat_A, "Ingresar apellido paterno");
                return false;
            }
            else
            {
                if (obje.IsString(txtApPat_A.Text))
                {
                    if (txtApPat_A.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtApPat_A, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtApPat_A, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtApPat_A, "Solo ingrese letras");
                    return false;
                }
            }

            if (this.txtApMat_A.Text.Length == 0)//Apellido materno
            {
                errorProvider1.SetError(this.txtApMat_A, "Ingresar apellido materno");
                return false;
            }
            else
            {
                if (obje.IsString(txtApMat_A.Text))
                {
                    if (txtApMat_A.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtApMat_A, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtApMat_A, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtApMat_A, "Solo ingrese letras");
                    return false;
                }
            }

            if (this.txtCalle_A.Text.Length == 0)//Calle
            {
                errorProvider1.SetError(this.txtCalle_A, "Ingresar nombre de la calle");
                return false;
            }
            else
            {
                errorProvider1.SetError(this.txtCalle_A, "");
            }

            if (this.txtNum_A.Text.Length == 0)//Numero Ext
            {
                errorProvider1.SetError(this.txtNum_A, "Ingresar número exterior");
                return false;
            }
            else
            {
                if (obje.IsNumeric(txtNum_A.Text))
                {
                    errorProvider1.SetError(this.txtNum_A, "");

                }
                else
                {
                    errorProvider1.SetError(this.txtNum_A, "Solo ingrese números");
                    return false;
                }
            }

            if (this.txtColonia_C.Text.Length == 0)//Colonia
            {
                errorProvider1.SetError(this.txtColonia_C, "Ingresar la colonia");
                return false;
            }
            else
            {
                errorProvider1.SetError(this.txtColonia_C, "");
            }

            if (this.txtCP_A.Text.Length == 0)//Código Postal
            {
                errorProvider1.SetError(this.txtCP_A, "Ingresar código postal");
                return false;
            }
            else
            {
                if (obje.IsNumeric(txtCP_A.Text))
                {
                    if (txtCP_A.Text.Length == 5)
                    {
                        errorProvider1.SetError(this.txtCP_A, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCP_A, "Ingrese solo 5 números");
                        return false;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCP_A, "Solo ingrese números");
                    return false;
                }
            }

            if (this.txtTelEme_A.Text.Length == 0)//Télefono
            {
                errorProvider1.SetError(this.txtTelEme_A, "Ingresar el télefono");
                return false;
            }
            else
            {
                if (obje.IsNumeric(txtTelEme_A.Text))
                {
                    if (txtTelEme_A.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtTelEme_A, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtTelEme_A, "Ingrese el télefono con código de área");
                        return false;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtTelEme_A, "Solo ingrese números");
                    return false;
                }
            }
        }

        //---------------------------Metodo para validar datos parte 2-----------------------
        public void ValidarTodosDatos2()
            {
                if (this.txtNombre_A.Text.Length == 0)//Nombre
                {
                    errorProvider1.SetError(this.txtNombre_A, "Ingresar el usuario");
                    DatoValidado = false;
                }
                else
                {
                    if (obje.IsString(txtNombre_A.Text))
                    {
                        if (txtNombre_A.Text.Length < 3)
                        {
                            errorProvider1.SetError(this.txtNombre_A, "Ingrese mas de 3 caracteres");
                            DatoValidado = false;
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtNombre_A, "");
                            DatoValidado = true;
                        }
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtNombre_A, "Solo ingrese letras");
                        DatoValidado = false;
                    }

                }

                if (this.txtApPat_A.Text.Length == 0)//Apellido paterno
                {
                    errorProvider1.SetError(this.txtApPat_A, "Ingresar apellido paterno");
                    DatoValidado = false;
                }
                else
                {
                    if (obje.IsString(txtApPat_A.Text))
                    {
                        if (txtApPat_A.Text.Length < 3)
                        {
                            errorProvider1.SetError(this.txtApPat_A, "Ingrese mas de 3 caracteres");
                            DatoValidado = false;
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtApPat_A, "");
                            DatoValidado = true;
                        }

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtApPat_A, "Solo ingrese letras");
                        DatoValidado = false;
                    }
                }

                if (this.txtApMat_A.Text.Length == 0)//Apellido materno
                {
                    errorProvider1.SetError(this.txtApMat_A, "Ingresar apellido materno");
                    DatoValidado = false;
                }
                else
                {
                    if (obje.IsString(txtApMat_A.Text))
                    {
                        if (txtApMat_A.Text.Length < 3)
                        {
                            errorProvider1.SetError(this.txtApMat_A, "Ingrese mas de 3 caracteres");
                            DatoValidado = false;
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtApMat_A, "");
                            DatoValidado = true;
                        }

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtApMat_A, "Solo ingrese letras");
                        DatoValidado = false;
                    }
                }

                if (this.txtEdad_A.Text.Length == 0)//Código Postal
                {
                    errorProvider1.SetError(this.txtEdad_A, "Ingresar código postal");
                    DatoValidado = false;
                }
                else
                {
                    if (obje.IsNumeric(txtEdad_A.Text))
                    {
                        if (txtEdad_A.Text.Length > 2)
                        {
                            errorProvider1.SetError(this.txtEdad_A, "Ingrese menos de 2 números");
                            DatoValidado = false;
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtEdad_A, "");
                            DatoValidado = true;
                        }

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtEdad_A, "Solo ingrese números");
                        DatoValidado = false;
                    }
                }


                if (this.txtCalle_A.Text.Length == 0)//Calle
                {
                    errorProvider1.SetError(this.txtCalle_A, "Ingresar nombre de la calle");
                    DatoValidado = false;
                }
                else
                {
                    errorProvider1.SetError(this.txtCalle_A, "");
                    DatoValidado = true;
                }

                if (this.txtNum_A.Text.Length == 0)//Numero Ext
                {
                    errorProvider1.SetError(this.txtNum_A, "Ingresar número exterior");
                    DatoValidado = false;
                }
                else
                {
                    if (obje.IsNumeric(txtNum_A.Text))
                    {
                        errorProvider1.SetError(this.txtNum_A, "");
                        DatoValidado = true;

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtNum_A, "Solo ingrese números");
                        DatoValidado = false;
                    }
                }

                if (this.txtColonia_C.Text.Length == 0)//Colonia
                {
                    errorProvider1.SetError(this.txtColonia_C, "Ingresar la colonia");
                    DatoValidado = false;
                }
                else
                {
                    errorProvider1.SetError(this.txtColonia_C, "");
                    DatoValidado = true;
                }

                if (this.txtCP_A.Text.Length == 0)//Código Postal
                {
                    errorProvider1.SetError(this.txtCP_A, "Ingresar código postal");
                    DatoValidado = false;
                }
                else
                {
                    if (obje.IsNumeric(txtCP_A.Text))
                    {
                        if (txtCP_A.Text.Length == 5)
                        {
                            errorProvider1.SetError(this.txtCP_A, "");
                            DatoValidado = true;
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtCP_A, "Ingrese solo 5 números");
                            DatoValidado = false;

                        }

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCP_A, "Solo ingrese números");
                        DatoValidado = false;
                    }
                }
                if (this.txtTelEme_A.Text.Length == 0)//Télefono
                {
                    errorProvider1.SetError(this.txtTelEme_A, "Ingresar el télefono");
                    DatoValidado = false;
                }
                else
                {
                    if (obje.IsNumeric(txtTelEme_A.Text))
                    {
                        if (txtTelEme_A.Text.Length == 10)
                        {
                            errorProvider1.SetError(this.txtTelEme_A, "");
                            DatoValidado = true;
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtTelEme_A, "Ingrese el télefono con código de área");
                            DatoValidado = false;
                        }

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtTelEme_A, "Solo ingrese números");
                        DatoValidado = false;
                    }
                }
            }*/

         //-------------------------Metodo Validating--------------------------------------------------
        //Nombre
        private void txtNombre_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtNombre_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtNombre_A, "Ingresar el nombre");
            }
            else
            {
                if (obje.IsString(txtNombre_A.Text))
                {
                    if (txtNombre_A.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtNombre_A, "Ingrese mas de 3 caracteres");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtNombre_A, "");
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtNombre_A, "Solo ingrese letras");
                }
            }
        }
        //Apellido paterno
        private void txtApPat_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtApPat_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtApPat_A, "Ingresar apellido paterno");
            }
            else
            {
                if (obje.IsString(txtApPat_A.Text))
                {
                    if (txtApPat_A.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtApPat_A, "Ingrese mas de 3 caracteres");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtApPat_A, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtApPat_A, "Solo ingrese letras");
                }
            }
        }

        //Apellido materno
        private void txtApMat_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtApMat_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtApMat_A, "Ingresar apellido materno");
            }
            else
            {
                if (obje.IsString(txtApMat_A.Text))
                {
                    if (obje.IsString(txtApMat_A.Text))
                    {
                        if (txtApMat_A.Text.Length < 3)
                        {
                            errorProvider1.SetError(this.txtApMat_A, "Ingrese mas de 3 caracteres");
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtApMat_A, "");
                        }

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtApMat_A, "Solo ingrese letras");
                }
            }
        }
        //CURP
        private void txtCURP_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCURP_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtCURP_A, "Ingresar apellido materno");
            }
            else
            {
                if (obje.IsString(txtCURP_A.Text))
                {
                    if (txtCURP_A.Text.Length == 18)
                    {
                        errorProvider1.SetError(this.txtCURP_A, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCURP_A, "Ingrese los 18 caracteres de la CURP");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCURP_A, "Solo ingrese letras");
                }
            }
        }
        //Calle
        private void txtCalle_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCalle_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtCalle_A, "Ingresar nombre de la calle");
            }
            else
            {
                errorProvider1.SetError(this.txtCalle_A, "");
            }
        }
        //Numero Ext
        private void txtNum_A_Validating(object sender, CancelEventArgs e)
        {
                            if (this.txtNum_A.Text.Length == 0)//Numero Ext
                {
                    errorProvider1.SetError(this.txtNum_A, "Ingresar número exterior");
                }
                else
                {
                    if (obje.IsNumeric(txtNum_A.Text))
                    {
                        errorProvider1.SetError(this.txtNum_A, "");

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtNum_A, "Solo ingrese números");
                    }
                }
        }
        //Colonia
        private void txtColonia_C_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtColonia_C.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtColonia_C, "Ingresar la colonia");
            }
            else
            {
                errorProvider1.SetError(this.txtColonia_C, "");
            }
        }
        //Código Postal
        private void txtCP_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCP_A.Text.Length == 0)//Código Postal
            {
                errorProvider1.SetError(this.txtCP_A, "Ingresar código postal");
            }
            else
            {
                if (obje.IsNumeric(txtCP_A.Text))
                {
                    if (txtCP_A.Text.Length == 5)
                    {
                        errorProvider1.SetError(this.txtCP_A, "");

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCP_A, "Ingrese solo 5 números");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCP_A, "Solo ingrese números");
                }
            }
        }
        //Lugar de nacimiento
        private void txtLugarNac_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtLugarNac_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtLugarNac_A, "Ingresar apellido materno");
            }
            else
            {
                if (obje.IsString(txtLugarNac_A.Text))
                {
                    if (obje.IsString(txtLugarNac_A.Text))
                    {
                        if (txtLugarNac_A.Text.Length < 3)
                        {
                            errorProvider1.SetError(this.txtLugarNac_A, "Ingrese mas de 5 caracteres");
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtLugarNac_A, "");
                        }

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtLugarNac_A, "Solo ingrese letras");
                }
            }
        }
        //Edad
        private void txtEdad_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtEdad_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtEdad_A, "Ingresar la edad del alumno");
            }
            else
            {
                if (obje.IsNumeric(txtEdad_A.Text))
                {
                    if (txtEdad_A.Text.Length > 2)
                    {
                        errorProvider1.SetError(this.txtEdad_A, "Ingrese menos de 2 números");

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtEdad_A, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtEdad_A, "Solo ingrese números");
                }
            }
        }
        //Telefono de emergencias
        private void txtTelEme_A_Validated(object sender, EventArgs e)
        {
            if (this.txtTelEme_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtTelEme_A, "Ingresar el télefono");
            }
            else
            {
                if (obje.IsNumeric(txtTelEme_A.Text))
                {
                    if (txtTelEme_A.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtTelEme_A, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtTelEme_A, "Ingrese el télefono con código de área");

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtTelEme_A, "Solo ingrese números");
                }
            }
        }
    //Alergias
        private void txtAlergias_A_Validating(object sender, CancelEventArgs e)
        {
                if (this.txtAlergias_A.Text.Length == 0)
                {
                    errorProvider1.SetError(this.txtAlergias_A, "Ingresar apellido materno");
                }
                else
                {
                    if (obje.IsString(txtAlergias_A.Text))
                    {
                        if (obje.IsString(txtAlergias_A.Text))
                        {
                            if (txtAlergias_A.Text.Length < 3)
                            {
                                errorProvider1.SetError(this.txtAlergias_A, "Ingrese mas de 3 caracteres");
                            }
                            else
                            {
                                errorProvider1.SetError(this.txtAlergias_A, "");
                            }

                        }

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtAlergias_A, "Solo ingrese letras");
                    }
                }
            }
    }
}
