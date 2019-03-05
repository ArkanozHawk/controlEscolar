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
    public partial class Form3 : MaterialForm
    {
        public Form3()
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

        //---------------------------------------------Botones------------------------------------
        //Volver al menu principal
        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
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
        //Buscar
        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBuscar));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
        //Inscripcion
        private void btnInscripcion_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadAlumno));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
        //Registrar Alumno
        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            //bool validar = ValidarTodosDatos();
            //ValidarTodosDatos2();

            string Nombre_T, AP_T, AM_T, Calle_T, Numero_T, Colonia_T, CP_T, Telefono_T, Celular_T, Profesion_T, LT_T,grado="";
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

            //if(validar == true)
            //{
                MessageBox.Show(sesion.nombre);
                string conexion = "server=localhost;uid=root;database=nerivela";
              
                string inserta_padres = " INSERT INTO `padres` (`idPadres`, `nombre`, `ApellidoP`, `ApellidoM`, `lugTrabajo`, `Profesion`, `telefono`, `Celular`) VALUES(NULL, '"+Nombre_T+"', '" + AP_T+"', '" + AM_T + "', '" + LT_T + "', '"+ Profesion_T + "', '"+ Telefono_T + "', '" + Celular_T + "');";
                obj.inspadres(conexion, inserta_padres);
                string consultaidpadres = "SELECT idpadres FROM `padres` WHERE `nombre` LIKE '"+Nombre_T+"' AND `ApellidoP` LIKE '"+AP_T+"'";
                string idpadres = obj.Consultapadreshijos(conexion, consultaidpadres);
                MessageBox.Show(idpadres);
                switch (sesion.edad)
                {

                case 6: { grado = "1";  break; }
                case 7: { grado = "2"; break; }
                case 8: { grado = "3"; break; }
                case 9: { grado = "4"; break; }
                case 10: { grado = "5"; break; }
                case 11: { grado = "6"; break; }
               
                default:
                    break;
                }
                    MessageBox.Show(grado);
           
                    string inserta_alumnos = " INSERT INTO `alumno`(`idAlumno`, `nombre`, `ApellidoP`, `ApellidoM`, `calle`, `colonia`, `numExt`, `cp`, `telEmer`, `lugNac`, `FechNac`, `Alergias`, `CURP`, `idPadres`, `idGrado`) VALUES(NULL,'"+sesion.nombre+"','"+sesion.AP+"','"+sesion.AM+"','"+sesion.calle+"','"+sesion.Colonia+"','"+sesion.numero+"','"+sesion.CP+"','"+sesion.telefono+"','"+sesion.LN+"','"+sesion.fnac+"','"+sesion.Alergia+"','"+sesion.Curp+"','"+idpadres+"','"+grado+"');";
                    obj.insalumnos(conexion, inserta_alumnos);
                    Form3 frm3 = new Form3();
                    this.Hide();
                    frm3.Show();
            //}
            //else
            //{
              //  MessageBox.Show("Error en los datos");
            //}
        }
        //Modificar
        private void btnModificar_Click(object sender, EventArgs e)
        {

        }
        //Eliminar
        private void Eliminar_Click(object sender, EventArgs e)
        {

        }

        //-------------------------------------------Metodo Validating------------------------------------
        //Nombre
        private void txtnombre_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtnombre_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtnombre_T, "Ingresar el nombre");
            }
            else
            {
                errorProvider1.SetError(this.txtnombre_T, "");
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
                errorProvider1.SetError(this.txtAP_T, "");
            }
        }
        //Apellido materno
        private void txtAM_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtAM_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtAM_T, "Ingresar apellido paterno");
            }
            else
            {
                errorProvider1.SetError(this.txtAM_T, "");
            }
        }
        //Calle
        private void txtCalle_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCalle_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtCalle_T, "Ingresar apellido paterno");
            }
            else
            {
                errorProvider1.SetError(this.txtCalle_T, "");
            }
        }
        //Num Ext
        private void txtNum_T_Validating(object sender, CancelEventArgs e)
        {
            int num;
            if (!int.TryParse(txtNum_T.Text, out num))
            {
                errorProvider1.SetError(txtNum_T, "Ingesar el dato en numeros");
            }
            else
            {
                errorProvider1.SetError(txtNum_T, "");
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
            int num1;
            if (!int.TryParse(txtCP_T.Text, out num1))
            {
                errorProvider1.SetError(txtCP_T, "Ingesar el CP en numeros");
            }
            else
            {
                errorProvider1.SetError(txtCP_T, "");
            }
        }
        //Telefono
        private void txtTelf_T_Validating(object sender, CancelEventArgs e)
        {
            int num1;
            if (!int.TryParse(txtTelf_T.Text, out num1))
            {
                errorProvider1.SetError(txtTelf_T, "Ingesar el Teléfono en numeros");
            }
            else
            {
                errorProvider1.SetError(txtTelf_T, "");
            }
        }
        //Celular
        private void txtCel_T_Validating(object sender, CancelEventArgs e)
        {
            int num1;
            if (!int.TryParse(txtCel_T.Text, out num1))
            {
                errorProvider1.SetError(txtCel_T, "Ingesar el celular en numeros");
            }
            else
            {
                errorProvider1.SetError(txtCel_T, "");
            }
        }
        //Profesion
        private void txtprof_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtprof_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtprof_T, "Ingresar su profesion");
            }
            else
            {
                errorProvider1.SetError(this.txtprof_T, "");
            }
        }
        //Lugar trabajo
        private void txtLugTrab_T_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtLugTrab_T.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtLugTrab_T, "Ingresar el lugar de trabajo");
            }
            else
            {
                errorProvider1.SetError(this.txtLugTrab_T, "");
            }
        }

        /*private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            
            string Nombre_T, AP_T, AM_T, Calle_T, Numero_T, Colonia_T, CP_T, Telefono_T, Celular_T, Profesion_T, LT_T,grado="";
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
            
            MessageBox.Show(sesion.nombre);
            string conexion = "server=localhost;uid=root;database=nerivela";
              
            string inserta_padres = " INSERT INTO `padres` (`idPadres`, `nombre`, `ApellidoP`, `ApellidoM`, `lugTrabajo`, `Profesion`, `telefono`, `Celular`) VALUES(NULL, '"+Nombre_T+"', '" + AP_T+"', '" + AM_T + "', '" + LT_T + "', '"+ Profesion_T + "', '"+ Telefono_T + "', '" + Celular_T + "');";
            obj.inspadres(conexion, inserta_padres);
             string consultaidpadres = "SELECT idpadres FROM `padres` WHERE `nombre` LIKE '"+Nombre_T+"' AND `ApellidoP` LIKE '"+AP_T+"'";
           string idpadres = obj.Consultapadreshijos(conexion, consultaidpadres);
            MessageBox.Show(idpadres);
           switch (sesion.edad)
            {

                case 6: { grado = "1";  break; }
                case 7: { grado = "2"; break; }
                case 8: { grado = "3"; break; }
                case 9: { grado = "4"; break; }
                case 10: { grado = "5"; break; }
                case 11: { grado = "6"; break; }
               
                default:
                    break;
            }
            MessageBox.Show(grado);
           
            string inserta_alumnos = " INSERT INTO `alumno`(`idAlumno`, `nombre`, `ApellidoP`, `ApellidoM`, `calle`, `colonia`, `numExt`, `cp`, `telEmer`, `lugNac`, `FechNac`, `Alergias`, `CURP`, `idPadres`, `idGrado`) VALUES(NULL,'"+sesion.nombre+"','"+sesion.AP+"','"+sesion.AM+"','"+sesion.calle+"','"+sesion.Colonia+"','"+sesion.numero+"','"+sesion.CP+"','"+sesion.telefono+"','"+sesion.LN+"','"+sesion.fnac+"','"+sesion.Alergia+"','"+sesion.Curp+"','"+idpadres+"','"+grado+"');";
            obj.insalumnos(conexion, inserta_alumnos);
            Form3 frm3 = new Form3();
            this.Hide();
            frm3.Show();

        }*/
    }
}
