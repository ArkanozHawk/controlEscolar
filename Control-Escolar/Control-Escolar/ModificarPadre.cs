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

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT  idPadres  FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";

            string idpadres = obj.Consultapadreshijos(conexion, query);

            MessageBox.Show(idpadres);

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

            string inserta_padres = "UPDATE `padres` SET `nombre`='" + Nombre_T + "'" + ",`ApellidoP`='" + AP_T + "'" + ",`ApellidoM`='" + AM_T + "'" + ",`lugTrabajo`='" + LT_T + "'" + ",`Profesion`='" + Profesion_T + "'" + ",`telefono`='" + Telefono_T + "'" + ",`Celular`='" + Celular_T + "'" + ",`Calle`='" + Calle_T + "'" + ",`Colonia`='" + Colonia_T + "'" + ",`NumExt`='" + Numero_T + "'" + ",`cp`='" + CP_T + "'" + " WHERE idPadres ='" + idpadres + "'";
            obj.inspadres(conexion, inserta_padres);

            string inserta_alumnos = "UPDATE `alumno` SET `nombre`='" + sesion.nombre + "'" + ",`ApellidoP`='" + sesion.AP + "'" + ",`ApellidoM`='" + sesion.AM + "'" + ",`calle`='" + sesion.calle + "'" + ",`colonia`='" + sesion.Colonia + "'" + ",`numExt`='" + sesion.numero + "'" + ",`cp`='" + sesion.CP + "'" + ",`telEmer`='" + sesion.telefono1 + "'" + ",`Genero`='" + sesion.genero + "'" + ",`lugNac`='" + sesion.LN + "'" + ",`FechNac`='" + sesion.fnac + "'" + ",`Alergias`='" + sesion.Alergia + "'" + ",`CURP`='" + sesion.Curp + "'" + ",`idGrado`='" + grado + "'" + " WHERE idPadres='" + idpadres + "'";
            MessageBox.Show(inserta_alumnos);
            obj.inspadres(conexion, inserta_alumnos);

            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBuscar));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }


        public void mostrardatospadre()
        {

            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT  idPadres  FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";

            string idpadres = obj.Consultapadreshijos(conexion, query);

            MessageBox.Show(idpadres);

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

                MessageBox.Show("se mostraron datos");
               

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);


            }
        }
    }
}
