using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MySql.Data.MySqlClient;
using MaterialSkin.Controls;
using ValidarDatos;
using System.Text.RegularExpressions;

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
        //Modificar
        public static void ThreadModificar()
        {
            Application.Run(new Modificar());
        }
        //Calcular la edad
        public void CalcEdad(string fnac)
        {
            DateTime dat = Convert.ToDateTime(fnac);
            DateTime nacimiento = new DateTime(dat.Year, dat.Month, dat.Day);
            int edad1 = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            if(txtEdad_A.Text.Length <3 && txtEdad_A.Text.Length >0)
            {
               int edad2 = Convert.ToInt32(txtEdad_A.Text);
                if (edad1 == edad2)
                {
                   sesion.edad = edad2;
                }

                else { MessageBox.Show("No coincide la edad con fecha de nacimiento "); }
            }
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
           
            if(txtEdad_A.Text.Length != 0)
            {
                sesion.edad = Convert.ToInt32(txtEdad_A.Text);
            }
            
            sesion.telefono = txtTelEme_A.Text;
            sesion.Alergia = txtAlergias_A.Text;
        }

        //------------------------------------------Botones-------------------------------------------
        //Cerrar sesion
        private void BtnCerrar_Click(object sender, EventArgs e)
        {
            string HoraSalida = DateTime.Now.ToString("hh:mm:ss");
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
            string nacimiento = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");

            sesion.fnac = nacimiento;
            CalcEdad(sesion.fnac);
            bool validar = ValidarTodosDatos();
            ValidarTodosDatos2();

            //--------------------------------Seleccionamos todo de los alumnos de 1°--------------------------------

            string conexion = "server=localhost;uid=root;database=nerivela";
            string num = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 1";

            MySqlConnection conn12;
            MySqlCommand com12;

            conn12 = new MySqlConnection(conexion);
            conn12.Open();

            com12 = new MySqlCommand(num, conn12);

            MySqlDataReader myreader12 = com12.ExecuteReader();
            myreader12.Read();
            int num1 = Convert.ToInt32(myreader12["COUNT(*)"]);

            conn12.Close();
            //-------------------------------- 2
            string conexion1 = "server=localhost;uid=root;database=nerivela";
            string num12 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 2";

            MySqlConnection conn13;
            MySqlCommand com13;

            conn13 = new MySqlConnection(conexion1);
            conn13.Open();

            com13 = new MySqlCommand(num12, conn13);

            MySqlDataReader myreader13 = com13.ExecuteReader();
            myreader13.Read();
            int num2 = Convert.ToInt32(myreader13["COUNT(*)"]);

            conn13.Close();
            //-------------------------------- 3
            string conexion2 = "server=localhost;uid=root;database=nerivela";
            string num13 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 3";

            MySqlConnection conn14;
            MySqlCommand com14;

            conn14 = new MySqlConnection(conexion2);
            conn14.Open();

            com14 = new MySqlCommand(num13, conn14);

            MySqlDataReader myreader14 = com14.ExecuteReader();
            myreader14.Read();
            int num3 = Convert.ToInt32(myreader14["COUNT(*)"]);

            conn14.Close();
            //-------------------------------- 4
            string conexion3 = "server=localhost;uid=root;database=nerivela";
            string num14 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 4";

            MySqlConnection conn15;
            MySqlCommand com15;

            conn15 = new MySqlConnection(conexion3);
            conn15.Open();

            com15 = new MySqlCommand(num14, conn15);

            MySqlDataReader myreader15 = com15.ExecuteReader();
            myreader15.Read();
            int num4 = Convert.ToInt32(myreader15["COUNT(*)"]);

            conn15.Close();
            //-------------------------------- 5
            string conexion5 = "server=localhost;uid=root;database=nerivela";
            string num15 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 5";

            MySqlConnection conn16;
            MySqlCommand com16;

            conn16 = new MySqlConnection(conexion5);
            conn16.Open();

            com16 = new MySqlCommand(num15, conn16);

            MySqlDataReader myreader16 = com16.ExecuteReader();
            myreader16.Read();
            int num5 = Convert.ToInt32(myreader16["COUNT(*)"]);

            conn16.Close();
            //-------------------------------- 6
            string conexion6 = "server=localhost;uid=root;database=nerivela";
            string num16 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 6";

            MySqlConnection conn17;
            MySqlCommand com17;

            conn17 = new MySqlConnection(conexion6);
            conn17.Open();

            com17 = new MySqlCommand(num16, conn17);

            MySqlDataReader myreader17 = com13.ExecuteReader();
            myreader17.Read();
            int num6 = Convert.ToInt32(myreader17["COUNT(*)"]);

            conn17.Close();
            if (num1 == 25)
            {
                MessageBox.Show("No se pueden registrar mas de 25 alumnos en el primer grado.");
            }
            else
            {
                if (num2 == 25)
                {
                    MessageBox.Show("No se pueden registrar mas de 25 alumnos en el segundo grado.");
                }
                else
                {
                    if (num3 == 25)
                    {
                        MessageBox.Show("No se pueden registrar mas de 25 alumnos en el primer grado.");
                    }
                    else
                    {
                        if (num4 == 25)
                        {
                            MessageBox.Show("No se pueden registrar mas de 25 alumnos en el primer grado.");
                        }
                        else
                        {
                            if (num5 == 25)
                            {
                                MessageBox.Show("No se pueden registrar mas de 25 alumnos en el primer grado.");
                            }
                            else
                            {
                                if (num6 == 25)
                                {
                                    MessageBox.Show("No se pueden registrar mas de 25 alumnos en el primer grado.");
                                }
                                else
                                {
                                    if (validar == true)
                                    {
                                        inscripcion();

                                        System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadForm3));
                                        pantalla.Start();
                                        CheckForIllegalCrossThreadCalls = false;
                                        this.Close();
                                    }
                                    else
                                    {
                                        MessageBox.Show("Error en los datos");
                                    }
                                }

                            }

                        }

                    }

                }

            }


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
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBuscar));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
        //Eliminar
        private void Eliminar_Click(object sender, EventArgs e)
        {

        }

        //--------------------------------Metodo para validar todos los campos-----------------------------
        public bool ValidarTodosDatos()
        {
            if (this.txtCURP_A.Text.Length == 0)//CURP
            {
                errorProvider1.SetError(this.txtCURP_A, "Ingresar la CURP del alumno");
                return false;
            }
            else
            {
                if (Regex.IsMatch(txtCURP_A.Text, @"^.*(?=.{18})(?=.*[0-9])(?=.*[A-ZÑ]).*$"))//Falta agregar la mascara
                {
                    if (txtCURP_A.Text.Length == 18)
                    {
                        errorProvider1.SetError(this.txtCURP_A, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCURP_A, "CURP invalida");
                        return false;
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtCURP_A, "Ingresa la CURP correctamente");
                    return false;
                }
            }

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
                errorProvider1.SetError(this.txtTelEme_A, "Ingresar el télefono para emergencia del alumno");
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

            if (this.txtEdad_A.Text.Length == 0)//Edad
            {
                errorProvider1.SetError(this.txtEdad_A, "Ingresar la edad del alumno");
                return false;

            }
            else
            {
                if (obje.IsNumeric(txtEdad_A.Text))
                {
                    if (txtEdad_A.Text.Length > 2)
                    {
                        errorProvider1.SetError(this.txtEdad_A, "La edad es incorrecta");
                        return false;

                    }
                    else
                    {

                        string nacimiento = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
                        sesion.fnac = nacimiento;
                        CalcEdad(sesion.fnac);
                        if (sesion.edad.ToString() == txtEdad_A.Text)
                        {
                            errorProvider1.SetError(this.txtEdad_A, "");
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtEdad_A, "La edad no coincide con la fecha");
                            return false;
                        }

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtEdad_A, "Solo ingrese números");
                    return false;

                }
            }

            if (RadMasculino.Checked == true)//Genero
            {
                sesion.genero = "Masculino";
            }
            else
            {
                if(RadFemenino.Checked == true)
                {
                   sesion.genero = "Femenino";
                    errorProvider1.SetError(this.RadFemenino, "");
                }
                else
                {
                    errorProvider1.SetError(this.RadFemenino, "Seleccione el genero del alumno");
                    return false; 
                }
            }

            if (this.txtLugarNac_A.Text.Length == 0)//Lugar de nacimiento
            {
                errorProvider1.SetError(this.txtLugarNac_A, "Ingresar Lugar de nacimiento");
                return false;
            }
            else
            {
                if (obje.IsString(txtLugarNac_A.Text))
                {
                    if (txtLugarNac_A.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtLugarNac_A, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtLugarNac_A, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtLugarNac_A, "Solo ingrese letras");
                    return false;
                }
            }

            if (this.txtAlergias_A.Text.Length == 0)//Alergias
            {
                errorProvider1.SetError(this.txtAlergias_A, "Ingresar las alergias del alumno");
                return false;
            }
            else
            {
                if (obje.IsString(txtAlergias_A.Text))
                {
                    if (txtAlergias_A.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtAlergias_A, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtAlergias_A, "");
                        return true; 
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtAlergias_A, "Solo ingrese letras");
                    return false;
                }
            }

        }

        //---------------------------Metodo para validar datos parte 2-----------------------
        public void ValidarTodosDatos2()
        {

            if (this.txtCURP_A.Text.Length == 0)//CURP
            {
                errorProvider1.SetError(this.txtCURP_A, "Ingresar la CURP del alumno");
            }
            else
            {
                if (Regex.IsMatch(txtCURP_A.Text, @"^.*(?=.{18})(?=.*[0-9])(?=.*[A-ZÑ]).*$"))//Falta agregar la mascara
                {
                    if (txtCURP_A.Text.Length == 18)
                    {
                        errorProvider1.SetError(this.txtCURP_A, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCURP_A, "CURP invalida");
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtCURP_A, "Ingresa la CURP correctamente");
                }
            }

            if (this.txtNombre_A.Text.Length == 0)//Nombre
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

            if (this.txtApPat_A.Text.Length == 0)//Apellido paterno
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

            if (this.txtApMat_A.Text.Length == 0)//Apellido materno
            {
                errorProvider1.SetError(this.txtApMat_A, "Ingresar apellido materno");

            }
            else
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
                else
                {
                    errorProvider1.SetError(this.txtApMat_A, "Solo ingrese letras");

                }
            }

            if (this.txtEdad_A.Text.Length == 0)//Edad
            {
                errorProvider1.SetError(this.txtEdad_A, "Ingresar la edad del alumno");

            }
            else
            {
                if (obje.IsNumeric(txtEdad_A.Text))
                {
                    if (txtEdad_A.Text.Length > 2)
                    {
                        errorProvider1.SetError(this.txtEdad_A, "La edad es incorrecta");

                    }
                    else
                    {
                        string nacimiento = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
                        sesion.fnac = nacimiento;
                        CalcEdad(sesion.fnac);
                        if (sesion.edad.ToString() == txtEdad_A.Text)
                        {
                            errorProvider1.SetError(this.txtEdad_A, "");
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtEdad_A, "La edad no coincide con la fecha");
                        }
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtEdad_A, "Solo ingrese números");

                }
            }


            if (this.txtCalle_A.Text.Length == 0)//Calle
            {
                errorProvider1.SetError(this.txtCalle_A, "Ingresar nombre de la calle");

            }
            else
            {
                errorProvider1.SetError(this.txtCalle_A, "");
            }

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

            if (this.txtColonia_C.Text.Length == 0)//Colonia
            {
                errorProvider1.SetError(this.txtColonia_C, "Ingresar la colonia");

            }
            else
            {
                errorProvider1.SetError(this.txtColonia_C, "");
            }

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

            if (this.txtTelEme_A.Text.Length == 0)//Télefono
            {
                errorProvider1.SetError(this.txtTelEme_A, "Ingresar el télefono para emergencias del alumno");

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

            if (RadMasculino.Checked == true)//Genero
            {
                sesion.genero = "Masculino";
            }
            else
            {
                if (RadFemenino.Checked == true)
                {
                    sesion.genero = "Femenino";
                    errorProvider1.SetError(this.RadFemenino, "");
                }
                else
                {
                    errorProvider1.SetError(this.RadFemenino, "Seleccione el genero del alumno");
                }
            }

            if (this.txtLugarNac_A.Text.Length == 0)//Lugar de nacimiento
            {
                errorProvider1.SetError(this.txtLugarNac_A, "Ingresar Lugar de nacimiento");
            }
            else
            {
                if (obje.IsString(txtLugarNac_A.Text))
                {
                    if (txtLugarNac_A.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtLugarNac_A, "Ingrese mas de 3 caracteres");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtLugarNac_A, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtLugarNac_A, "Solo ingrese letras");
                }
            }

            if (this.txtAlergias_A.Text.Length == 0)//Alergias
            {
                errorProvider1.SetError(this.txtAlergias_A, "Ingresar las alergias del alumno");
            }
            else
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
                else
                {
                    errorProvider1.SetError(this.txtAlergias_A, "Solo ingrese letras");
                }
            }
        }

        //-------------------------Metodo Validating--------------------------------------------------
        //CURP
        private void txtCURP_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCURP_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtCURP_A, "Ingresar la CURP del alumno");
            }
            else
            {
                if (Regex.IsMatch(txtCURP_A.Text, @"^.*(?=.{18})(?=.*[0-9])(?=.*[A-ZÑ]).*$"))//Falta agregar la mascara
                {
                    if (txtCURP_A.Text.Length == 18)
                    {
                        errorProvider1.SetError(this.txtCURP_A, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCURP_A, "CURP invalida");
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtCURP_A, "Ingresa la CURP correctamente");
                }
            }
        }
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
                errorProvider1.SetError(this.txtLugarNac_A, "Ingresar lugar de nacimiento");
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
                        errorProvider1.SetError(this.txtEdad_A, "Ingrese la edad correcta");

                    }
                    else
                    {
                        string nacimiento = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
                       

                        sesion.fnac = nacimiento;
                        CalcEdad(sesion.fnac);
                        if (sesion.edad.ToString() == txtEdad_A.Text)
                        {
                            errorProvider1.SetError(this.txtEdad_A, "");
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtEdad_A, "La edad no coincide con la fecha");
                        }
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
                    errorProvider1.SetError(this.txtAlergias_A, "Ingresar las alergias del alumno");
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

        private void btnInscripcion_Click(object sender, EventArgs e)
        {

        }

        private void bindingNavigator1_RefreshItems(object sender, EventArgs e)
        {

        }
    }
}
