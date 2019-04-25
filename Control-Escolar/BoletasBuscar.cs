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
    public partial class BoletasBuscar : MaterialForm
    {
        DataGridView mifiltro;

        MySqlCommand codigo = new MySqlCommand();
        MySqlConnection conectanos = new MySqlConnection();
        //MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela;pwd=digi3.0");
        MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela");
        conexion objbuscar = new conexion();

        public BoletasBuscar()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);

            //MySqlConnection conn;
            //MySqlCommand com;

            //string conexion = "server=localhost;uid=root;database=nerivela";
            //string query = "SELECT * FROM  `personal`  where  Usuario =" + "'" + sesion.Usuario + "' ";
            //conn = new MySqlConnection(conexion);
            //conn.Open();

            //com = new MySqlCommand(query, conn);

            //MySqlDataReader myreader = com.ExecuteReader();

        }

        conexion obj = new conexion();

        public static void ThreadProc()
        {
            Application.Run(new login());
        }

        public static void ThreadPrincipal()
        {
            Application.Run(new principal());
        }

        public static void ThreadCalificaciones12()
        {
            Application.Run(new Calificaciones12());
        }
        public static void ThreadCalificaciones3()
        {
            Application.Run(new Calificaciones3());
        }
        public static void ThreadCalificaciones456()
        {
            Application.Run(new Calificaciones456());
        }


        private void AgregarCalificaciones_Click(object sender, EventArgs e)
        {
            int Grado = Convert.ToInt32(sesion.grado);
            if (Grado == 1 || Grado == 2)
            {
                MessageBox.Show("Se abrirá la ventana de Calificaciones para Primer y Segundo Año");
                System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCalificaciones12));
                pantalla.Start();
                CheckForIllegalCrossThreadCalls = false;
                this.Close();
            }
            else
            {
                if (Grado == 3)
                {
                    MessageBox.Show("Se abrirá la ventana de Calificaciones para Tercer Año");
                    System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCalificaciones3));
                    pantalla.Start();
                    CheckForIllegalCrossThreadCalls = false;
                    this.Close();
                }
                else
                {
                    if (Grado == 4 || Grado == 5 || Grado == 6)
                    {
                        MessageBox.Show("Se abrirá la ventana de Calificaciones para Cuarto a sexto Año");
                        System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCalificaciones456));
                        pantalla.Start();
                        CheckForIllegalCrossThreadCalls = false;
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("No se ha elegido un Alumno aún");
                    }
                }
            }

            //System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadCalificaciones12));
            //pantalla.Start();
            //CheckForIllegalCrossThreadCalls = false;
            //this.Close();
        }

       

        private void btnCerrar_Click_1(object sender, EventArgs e)
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

        private void btnPrincipal_Click_1(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        public void datagrid(DataGridView data)
        {
            coneccion.Open();
            codigo.Connection = coneccion;
            codigo.CommandText = ("select  nombre ,  ApellidoP  , ApellidoM,Genero,  telEmer, CURP, Alergias,  idGrado  from Alumno");
            try
            {
                MySqlDataAdapter seleccionar = new MySqlDataAdapter();
                seleccionar.SelectCommand = codigo;
                DataTable datostabla = new DataTable();
                seleccionar.Fill(datostabla);
                BindingSource formulario = new BindingSource();
                formulario.DataSource = datostabla;
                data.DataSource = formulario;
                mifiltro = data;
                seleccionar.Update(datostabla);
                coneccion.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private void dataGridViewbuscar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow fila = dataGridViewbuscar.CurrentRow; // obtengo la fila actualmente seleccionada en el dataGridView

            sesion.Curp = Convert.ToString(fila.Cells[5].Value); //obtengo el valor de la primer columna
            sesion.grado = Convert.ToString(fila.Cells[7].Value); //Obtenemos el grado del niño

            //MessageBox.Show(sesion.grado);

        }

        private void TxtAP_T_KeyUp(object sender, KeyEventArgs e)
        {

            if (txtAP_T.Text != "")
            {
                dataGridViewbuscar.CurrentCell = null;
                foreach (DataGridViewRow r in dataGridViewbuscar.Rows)
                {
                    r.Visible = false;

                }
                foreach (DataGridViewRow r in dataGridViewbuscar.Rows)
                {
                    foreach (DataGridViewCell c in r.Cells)
                    {
                        if (c.Value.ToString().ToUpper().IndexOf(txtAP_T.Text.ToUpper()) == 0)
                        {
                            r.Visible = true;
                            break;
                        }
                    }
                }

            }
            else
            {
                datagrid(dataGridViewbuscar);
            }
        }

        private void BoletasBuscar_Load(object sender, EventArgs e)
        {
            datagrid(dataGridViewbuscar);
            sesion.grado = "0";
        }

        private void BoletasBuscar_FormClosing(object sender, FormClosingEventArgs e)
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
