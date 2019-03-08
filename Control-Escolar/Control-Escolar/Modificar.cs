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
    public partial class Modificar : MaterialForm
    {

        




        public Modificar()
        {
            InitializeComponent();
            modificar();
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
        public static void ThreadModificarPadre()
        {
            Application.Run(new ModificarPadre());
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

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            sesion.nombre = txtNombre_A.Text;
            sesion.AP = txtApPat_A.Text;
            sesion.AM = txtApMat_A.Text;
            sesion.Curp = txtCURP_A.Text;
            sesion.calle = txtNum_A.Text;
            sesion.numero = txtNum_A.Text;
            sesion.Colonia = txtColonia_C.Text;
            sesion.CP = txtCP_A.Text;
            sesion.LN = txtLugarNac_A.Text;
            sesion.telefono1 = txtTelEme_A.Text;

            if (txtEdad_A.Text.Length != 0)
            {
                sesion.edad = Convert.ToInt32(txtEdad_A.Text);
            }

            sesion.telefono = txtNombre_A.Text;
            sesion.Alergia = txtAlergias_A.Text;

            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadModificarPadre));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();


        }




        public void modificar()
        {

           

            /*
            sesion.nombre = txtNombre_A.Text;
            sesion.AP = txtApPat_A.Text;
            sesion.AM = txtApMat_A.Text;
            
            sesion.calle = txtNum_A.Text;
            sesion.numero = txtNum_A.Text;
            sesion.Colonia = txtColonia_C.Text;
            sesion.CP = txtCP_A.Text;
            sesion.LN = txtLugarNac_A.Text;
            */
            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT * FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";
            MessageBox.Show(sesion.Curp);
            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(query, conn);

            MySqlDataReader myreader = com.ExecuteReader();
            

            try
            {
               
                
              
                myreader.Read();

                
                txtNombre_A.Text = Convert.ToString(myreader["nombre"]);
                txtApPat_A.Text = Convert.ToString(myreader["ApellidoP"]);
                txtApMat_A.Text= Convert.ToString(myreader["ApellidoM"]);
                txtCalle_A.Text= Convert.ToString(myreader["calle"]);
                txtColonia_C.Text = Convert.ToString(myreader["colonia"]);
                txtNum_A.Text = Convert.ToString(myreader["numExt"]);
                txtCP_A.Text = Convert.ToString(myreader["cp"]);
                txtTelEme_A.Text = Convert.ToString(myreader["telEmer"]);
               DateTime fna  = Convert.ToDateTime(myreader["FechNac"]);
                dateTimePicker1.Value = fna;
                string edad = Convert.ToString(myreader["FechNac"]);
                CalcEdad(edad);
                txtEdad_A.Text = sesion.edad.ToString();
                txtLugarNac_A.Text = Convert.ToString(myreader["lugNac"]);
                txtAlergias_A.Text = Convert.ToString(myreader["Alergias"]);
                txtCURP_A.Text= Convert.ToString(myreader["CURP"]);
               sesion.genero = Convert.ToString(myreader["Genero"]);
                if (sesion.genero == "Masculino")
                {
                    RadMasculino.Checked =true;
                }
                else
                {
                    RadFemenino.Checked = true;
                }
                MessageBox.Show("si se hizo");
                conn.Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);

                
            }


            string nacimiento = dateTimePicker1.Value.Date.ToString("yyyy-MM-dd");
            sesion.fnac = nacimiento;
            sesion.nombre = txtNombre_A.Text;

          
            





        }


        public void CalcEdad(string fnac)
        {
            DateTime dat = Convert.ToDateTime(fnac);
            DateTime nacimiento = new DateTime(dat.Year, dat.Month, dat.Day);
            int edad1 = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
             MessageBox.Show(edad1.ToString());
           
                
                    sesion.edad = edad1;
                

               
        }
    }
}

    




