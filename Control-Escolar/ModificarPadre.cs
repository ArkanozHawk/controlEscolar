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
    public partial class ModificarPadre : MaterialForm
    {
        public ModificarPadre()
        {
            InitializeComponent();
            mostrardatospadre();
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
        public static void ThreadModificar()
        {
            Application.Run(new Modificar());
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

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            bool validar = ValidarTodosDatos();
            ValidarTodosDatos2();

            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT  idPadres  FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";

            string idpadres = obj.Consultapadreshijos(conexion, query);

            //MessageBox.Show(idpadres);

            string consultaidpadres = "SELECT * FROM  `padres`  WHERE  `idPadres` = '" + idpadres + "'";
           
            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(consultaidpadres, conn);

            MySqlDataReader myreader = com.ExecuteReader();
            
            string Nombre_T, AP_T, AM_T, Calle_T, Numero_T, Colonia_T, CP_T, Telefono_T, Celular_T, Profesion_T, LT_T, grado = "";
            Nombre_T = txtnombre_T.Text;
            AP_T = txtAP_T.Text;
            AM_T = txtAM_T.Text;
            Calle_T = txtCalle_T.Text;
            Numero_T = txtNum_T.Text;
            Colonia_T = txtColonia_T.Text;
            CP_T = txtCP_T.Text;
            Telefono_T = txtTelf_T.Text;
            Celular_T = txtCel_T.Text;
            Profesion_T = txtprof_T.Text;
            LT_T = txtLugTrab_T.Text;
            CP_T = txtCP_T.Text;
            switch (sesion.edad)
            {

                case 6: { grado = "1"; break; }
                case 7: { grado = "2"; break; }
                case 8: { grado = "3"; break; }
                case 9: { grado = "4"; break; }
                case 10: { grado = "5"; break; }
                case 11: { grado = "6"; break; }

                default:
                    break;
            }
            MessageBox.Show("El grado del alumno es: " + grado + " A");

            if (validar == true)
            {
                string inserta_padres = "UPDATE `padres` SET `nombre`='" + Nombre_T + "'" + ",`ApellidoP`='" + AP_T + "'" + ",`ApellidoM`='" + AM_T + "'" + ",`lugTrabajo`='" + LT_T + "'" + ",`Profesion`='" + Profesion_T + "'" + ",`telefono`='" + Telefono_T + "'" + ",`Celular`='" + Celular_T + "'" + ",`Calle`='" + Calle_T + "'" + ",`Colonia`='" + Colonia_T + "'" + ",`NumExt`='" + Numero_T + "'" + ",`cp`='" + CP_T + "'" + " WHERE idPadres ='" + idpadres + "'";
                obj.inspadres(conexion, inserta_padres);

                string inserta_alumnos = "UPDATE `alumno` SET `nombre`='" + sesion.nombre + "'" + ",`ApellidoP`='" + sesion.AP + "'" + ",`ApellidoM`='" + sesion.AM + "'" + ",`calle`='" + sesion.calle + "'" + ",`colonia`='" + sesion.Colonia + "'" + ",`numExt`='" + sesion.numero + "'" + ",`cp`='" + sesion.CP + "'" + ",`telEmer`='" + sesion.telefono1 + "'" + ",`Genero`='" + sesion.genero + "'" + ",`lugNac`='" + sesion.LN + "'" + ",`FechNac`='" + sesion.fnac + "'" + ",`Alergias`='" + sesion.Alergia + "'" + ",`CURP`='" + sesion.Curp + "'" + ",`sangre`='" + sesion.sangre + "'" + ",`idGrado`='" + grado + "'" + " WHERE idPadres='" + idpadres + "'";
                //MessageBox.Show(inserta_alumnos);
                obj.inspadres(conexion, inserta_alumnos);

                System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBuscar));
                pantalla.Start();
                CheckForIllegalCrossThreadCalls = false;
                this.Close();
            }
            else
            {
                MessageBox.Show("Error en los datos");
            }
        }


        public void mostrardatospadre()
        {
            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT  idPadres  FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";

            string idpadres = obj.Consultapadreshijos(conexion, query);

            //MessageBox.Show(idpadres);

            string consultaidpadres = "SELECT * FROM  `padres`  WHERE  `idPadres` = '" + idpadres + "'";
           
            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(consultaidpadres, conn);

            MySqlDataReader myreader = com.ExecuteReader();

            try
            {
                myreader.Read();

                txtnombre_T.Text = Convert.ToString(myreader["nombre"]);
                txtAP_T.Text = Convert.ToString(myreader["ApellidoP"]);
                txtAM_T.Text = Convert.ToString(myreader["ApellidoM"]);
                txtCel_T.Text = Convert.ToString(myreader["Celular"]);
                txtCalle_T.Text = Convert.ToString(myreader["Calle"]);
                txtNum_T.Text = Convert.ToString(myreader["NumExt"]);
                txtColonia_T.Text = Convert.ToString(myreader["Colonia"]);
                txtCP_T.Text = Convert.ToString(myreader["cp"]);
                txtTelf_T.Text = Convert.ToString(myreader["telefono"]);
                //  txtCel_T.Text = Convert.ToString(myreader["nombre"]);
                txtprof_T.Text = Convert.ToString(myreader["profesion"]);
                txtLugTrab_T.Text = Convert.ToString(myreader["lugTrabajo"]);

                //MessageBox.Show("se mostraron datos");
               

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        //-------------------------------------------Metodo Validating------------------------------------
        //Nombre
        private void txtnombre_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtnombre_T.Text.Length == 0)//Nombre
            {
                errorProvider1.SetError(this.txtnombre_T, "Ingresar el nombre del padre");
            }
            else
            {
                if (obje.IsString(txtnombre_T.Text))
                {
                    if (txtnombre_T.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtnombre_T, "Ingrese mas de 3 caracteres");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtnombre_T, "");
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtnombre_T, "Solo ingrese letras");
                }
            }
        }

        //Apelliedo paterno
        private void txtAP_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtAP_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtAP_T, "Ingresar apellido paterno");
            }
            else
            {
                if (obje.IsString(txtAP_T.Text))
                {
                    if (txtAP_T.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtAP_T, "Ingrese mas de 3 caracteres");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtAP_T, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtAP_T, "Solo ingrese letras");
                }
            }
        }
        //Apellido materno
        private void txtAM_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtAM_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtAM_T, "Ingresar apellido materno");
            }
            else
            {
                if (obje.IsString(txtAM_T.Text))
                {
                    if (obje.IsString(txtAM_T.Text))
                    {
                        if (txtAM_T.Text.Length < 3)
                        {
                            errorProvider1.SetError(this.txtAM_T, "Ingrese mas de 3 caracteres");
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtAM_T, "");
                        }

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtAM_T, "Solo ingrese letras");
                }
            }
        }
        //Calle
        private void txtCalle_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCalle_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtCalle_T, "Ingresar calle");
            }
            else
            {
                errorProvider1.SetError(this.txtCalle_T, "");
            }
        }
        //Num Ext
        private void txtNum_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtNum_T.Text.Length == 0)//Numero Ext
            {
                errorProvider1.SetError(this.txtNum_T, "Ingresar número exterior");
            }
            else
            {
                if (obje.IsNumeric(txtNum_T.Text))
                {
                    errorProvider1.SetError(this.txtNum_T, "");

                }
                else
                {
                    errorProvider1.SetError(this.txtNum_T, "Solo ingrese números");
                }
            }
        }
        //Colonia
        private void txtColonia_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtColonia_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtColonia_T, "Ingresar la colonia");
            }
            else
            {
                errorProvider1.SetError(this.txtColonia_T, "");
            }
        }
        //Codigo Postal
        private void txtCP_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCP_T.Text.Length == 0)//Código Postal
            {
                errorProvider1.SetError(this.txtCP_T, "Ingresar código postal");
            }
            else
            {
                if (obje.IsNumeric(txtCP_T.Text))
                {
                    if (txtCP_T.Text.Length == 5)
                    {
                        errorProvider1.SetError(this.txtCP_T, "");

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCP_T, "Ingrese solo 5 números");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCP_T, "Solo ingrese números");
                }
            }
        }
        //Telefono
        private void txtTelf_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtTelf_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtTelf_T, "Ingresar el télefono");
            }
            else
            {
                if (obje.IsNumeric(txtTelf_T.Text))
                {
                    if (txtTelf_T.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtTelf_T, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtTelf_T, "Ingrese el télefono con código de área");

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtTelf_T, "Solo ingrese números");
                }
            }
        }
        //Celular
        private void txtCel_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCel_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtCel_T, "Ingresar el número celular");
            }
            else
            {
                if (obje.IsNumeric(txtCel_T.Text))
                {
                    if (txtCel_T.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtCel_T, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCel_T, "Ingrese el télefono con código de área");

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCel_T, "Solo ingrese números");
                }
            }
        }
        //Profesion
        private void txtprof_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtprof_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtprof_T, "Ingresar su profesión");
            }
            else
            {
                if (obje.IsString(txtprof_T.Text))
                {
                    if (obje.IsString(txtprof_T.Text))
                    {
                        if (txtprof_T.Text.Length < 5)
                        {
                            errorProvider1.SetError(this.txtprof_T, "Ingrese mas de 5 caracteres");
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtprof_T, "");
                        }

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtprof_T, "Solo ingrese letras");
                }
            }
        }
        //Lugar trabajo
        private void txtLugTrab_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtLugTrab_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtLugTrab_T, "Ingresar las alergias del alumno");
            }
            else
            {
                if (obje.IsString(txtLugTrab_T.Text))
                {
                    if (obje.IsString(txtLugTrab_T.Text))
                    {
                        if (txtLugTrab_T.Text.Length < 5)
                        {
                            errorProvider1.SetError(this.txtLugTrab_T, "Ingrese mas de 5 caracteres");
                        }
                        else
                        {
                            errorProvider1.SetError(this.txtLugTrab_T, "");
                        }

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtLugTrab_T, "Solo ingrese letras");
                }
            }
        }

        //--------------------------------Metodo para validar todos los campos-----------------------------
        public bool ValidarTodosDatos()
        {
            if (this.txtnombre_T.Text.Length == 0)//Nombre
            {
                errorProvider1.SetError(this.txtnombre_T, "Ingresar el nombre del padre");
                return false;
            }
            else
            {
                if (obje.IsString(txtnombre_T.Text))
                {
                    if (txtnombre_T.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtnombre_T, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtnombre_T, "");
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtnombre_T, "Solo ingrese letras");
                    return false;
                }
            }

            if (this.txtAP_T.Text.Length == 0)//Apellido paterno
            {
                errorProvider1.SetError(this.txtAP_T, "Ingresar apellido paterno");
                return false;
            }
            else
            {
                if (obje.IsString(txtAP_T.Text))
                {
                    if (txtAP_T.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtAP_T, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtAP_T, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtAP_T, "Solo ingrese letras");
                    return false;
                }
            }

            if (this.txtAM_T.Text.Length == 0)//Apellido materno
            {
                errorProvider1.SetError(this.txtAM_T, "Ingresar apellido materno");
                return false;
            }
            else
            {
                if (obje.IsString(txtAP_T.Text))
                {
                    if (txtAM_T.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtAM_T, "Ingrese mas de 3 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtAM_T, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtAM_T, "Solo ingrese letras");
                    return false;
                }
            }

            if (this.txtCalle_T.Text.Length == 0)//Calle
            {
                errorProvider1.SetError(this.txtCalle_T, "Ingresar nombre de la calle");
                return false;
            }
            else
            {
                errorProvider1.SetError(this.txtCalle_T, "");
            }

            if (this.txtNum_T.Text.Length == 0)//Numero Ext
            {
                errorProvider1.SetError(this.txtNum_T, "Ingresar número exterior");
                return false;
            }
            else
            {
                if (obje.IsNumeric(txtNum_T.Text))
                {
                    errorProvider1.SetError(this.txtNum_T, "");

                }
                else
                {
                    errorProvider1.SetError(this.txtNum_T, "Solo ingrese números");
                    return false;
                }
            }

            if (this.txtColonia_T.Text.Length == 0)//Colonia
            {
                errorProvider1.SetError(this.txtColonia_T, "Ingresar la colonia");
                return false;
            }
            else
            {
                errorProvider1.SetError(this.txtColonia_T, "");
            }

            if (this.txtCP_T.Text.Length == 0)//Código Postal
            {
                errorProvider1.SetError(this.txtCP_T, "Ingresar código postal");
                return false;
            }
            else
            {
                if (obje.IsNumeric(txtCP_T.Text))
                {
                    if (txtCP_T.Text.Length == 5)
                    {
                        errorProvider1.SetError(this.txtCP_T, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCP_T, "Ingrese solo 5 números");
                        return false;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCP_T, "Solo ingrese números");
                    return false;
                }
            }

            if (this.txtTelf_T.Text.Length == 0)//Télefono
            {
                errorProvider1.SetError(this.txtTelf_T, "Ingresar el télefono");
                return false;
            }
            else
            {
                if (obje.IsNumeric(txtTelf_T.Text))
                {
                    if (txtTelf_T.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtTelf_T, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtTelf_T, "Ingrese el télefono con código de área");
                        return false;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtTelf_T, "Solo ingrese números");
                    return false;
                }
            }

            if (this.txtCel_T.Text.Length == 0)//Celular
            {
                errorProvider1.SetError(this.txtCel_T, "Ingresar el número celular");
                return false;
            }
            else
            {
                if (obje.IsNumeric(txtCel_T.Text))
                {
                    if (txtCel_T.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtCel_T, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCel_T, "Ingrese el télefono con código de área");
                        return false;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCel_T, "Solo ingrese números");
                    return false;
                }
            }

            if (this.txtLugTrab_T.Text.Length == 0)//Lugar de trabajo
            {
                errorProvider1.SetError(this.txtLugTrab_T, "Ingresar Lugar de trabajo");
                return false;
            }
            else
            {
                if (obje.IsString(txtLugTrab_T.Text))
                {
                    if (txtLugTrab_T.Text.Length < 5)
                    {
                        errorProvider1.SetError(this.txtLugTrab_T, "Ingrese mas de 5 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtLugTrab_T, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtLugTrab_T, "Solo ingrese letras");
                    return false;
                }
            }

            if (this.txtprof_T.Text.Length == 0)//Profesion
            {
                errorProvider1.SetError(this.txtprof_T, "Ingresar su profesión");
                return false;
            }
            else
            {
                if (obje.IsString(txtprof_T.Text))
                {
                    if (txtprof_T.Text.Length < 5)
                    {
                        errorProvider1.SetError(this.txtprof_T, "Ingrese mas de 5 caracteres");
                        return false;
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtprof_T, "");
                        return true;
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtprof_T, "Solo ingrese letras");
                    return false;
                }
            }

        }

        //---------------------------Metodo para validar datos parte 2-----------------------
        public void ValidarTodosDatos2()
        {
            if (this.txtnombre_T.Text.Length == 0)//Nombre
            {
                errorProvider1.SetError(this.txtnombre_T, "Ingresar el nombre del padre");

            }
            else
            {
                if (obje.IsString(txtnombre_T.Text))
                {
                    if (txtnombre_T.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtnombre_T, "Ingrese mas de 3 caracteres");

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtnombre_T, "");
                    }
                }
                else
                {
                    errorProvider1.SetError(this.txtnombre_T, "Solo ingrese letras");

                }
            }

            if (this.txtAP_T.Text.Length == 0)//Apellido paterno
            {
                errorProvider1.SetError(this.txtAP_T, "Ingresar apellido paterno");

            }
            else
            {
                if (obje.IsString(txtAP_T.Text))
                {
                    if (txtAP_T.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtAP_T, "Ingrese mas de 3 caracteres");

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtAP_T, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtAP_T, "Solo ingrese letras");

                }
            }

            if (this.txtAM_T.Text.Length == 0)//Apellido materno
            {
                errorProvider1.SetError(this.txtAM_T, "Ingresar apellido materno");

            }
            else
            {
                if (obje.IsString(txtAP_T.Text))
                {
                    if (txtAM_T.Text.Length < 3)
                    {
                        errorProvider1.SetError(this.txtAM_T, "Ingrese mas de 3 caracteres");

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtAM_T, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtAM_T, "Solo ingrese letras");

                }
            }

            if (this.txtCalle_T.Text.Length == 0)//Calle
            {
                errorProvider1.SetError(this.txtCalle_T, "Ingresar nombre de la calle");

            }
            else
            {
                errorProvider1.SetError(this.txtCalle_T, "");
            }

            if (this.txtNum_T.Text.Length == 0)//Numero Ext
            {
                errorProvider1.SetError(this.txtNum_T, "Ingresar número exterior");

            }
            else
            {
                if (obje.IsNumeric(txtNum_T.Text))
                {
                    errorProvider1.SetError(this.txtNum_T, "");

                }
                else
                {
                    errorProvider1.SetError(this.txtNum_T, "Solo ingrese números");

                }
            }

            if (this.txtColonia_T.Text.Length == 0)//Colonia
            {
                errorProvider1.SetError(this.txtColonia_T, "Ingresar la colonia");

            }
            else
            {
                errorProvider1.SetError(this.txtColonia_T, "");
            }

            if (this.txtCP_T.Text.Length == 0)//Código Postal
            {
                errorProvider1.SetError(this.txtCP_T, "Ingresar código postal");

            }
            else
            {
                if (obje.IsNumeric(txtCP_T.Text))
                {
                    if (txtCP_T.Text.Length == 5)
                    {
                        errorProvider1.SetError(this.txtCP_T, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCP_T, "Ingrese solo 5 números");

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCP_T, "Solo ingrese números");

                }
            }

            if (this.txtTelf_T.Text.Length == 0)//Télefono
            {
                errorProvider1.SetError(this.txtTelf_T, "Ingresar el télefono");

            }
            else
            {
                if (obje.IsNumeric(txtTelf_T.Text))
                {
                    if (txtTelf_T.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtTelf_T, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtTelf_T, "Ingrese el télefono con código de área");

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtTelf_T, "Solo ingrese números");

                }
            }

            if (this.txtCel_T.Text.Length == 0)//Celular
            {
                errorProvider1.SetError(this.txtCel_T, "Ingresar el número celular");

            }
            else
            {
                if (obje.IsNumeric(txtCel_T.Text))
                {
                    if (txtCel_T.Text.Length == 10)
                    {
                        errorProvider1.SetError(this.txtCel_T, "");
                    }
                    else
                    {
                        errorProvider1.SetError(this.txtCel_T, "Ingrese el télefono con código de área");

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtCel_T, "Solo ingrese números");

                }
            }

            if (this.txtLugTrab_T.Text.Length == 0)//Lugar de trabajo
            {
                errorProvider1.SetError(this.txtLugTrab_T, "Ingresar Lugar de trabajo");

            }
            else
            {
                if (obje.IsString(txtLugTrab_T.Text))
                {
                    if (txtLugTrab_T.Text.Length < 5)
                    {
                        errorProvider1.SetError(this.txtLugTrab_T, "Ingrese mas de 5 caracteres");

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtLugTrab_T, "");
                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtLugTrab_T, "Solo ingrese letras");

                }
            }

            if (this.txtprof_T.Text.Length == 0)//Profesion
            {
                errorProvider1.SetError(this.txtprof_T, "Ingresar su profesión");

            }
            else
            {
                if (obje.IsString(txtprof_T.Text))
                {
                    if (txtprof_T.Text.Length < 5)
                    {
                        errorProvider1.SetError(this.txtprof_T, "Ingrese mas de 5 caracteres");

                    }
                    else
                    {
                        errorProvider1.SetError(this.txtprof_T, "");

                    }

                }
                else
                {
                    errorProvider1.SetError(this.txtprof_T, "Solo ingrese letras");

                }
            }

        }

        private void ModificarPadre_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {

                string HoraSalida = DateTime.Now.ToString("hh:mm:ss");
                int idAccess = sesion.idAcceso;
                //string conexion = "server=localhost;uid=root;pwd=digi3.0;database=nerivela";
                string conexion = "server=localhost;uid=root;database=nerivela";
                string inserta_bitacora = "UPDATE bitacora SET HoraSalida = '" + HoraSalida + "' where idAcceso = " + idAccess + ";";
                obj.insBitacora(conexion, inserta_bitacora);


            }
            else
            {
                System.Threading.Thread login = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadProc));
                login.Start();
            }
        }
    }
}
