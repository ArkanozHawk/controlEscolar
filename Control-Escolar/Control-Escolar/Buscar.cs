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
    public partial class Buscar : MaterialForm
    {
        DataGridView mifiltro;

        MySqlCommand codigo = new MySqlCommand();
        MySqlConnection conectanos = new MySqlConnection();
        //MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela;pwd=digi3.0");
        MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela");
        conexion objbuscar = new conexion();
        public Buscar()
        {
            InitializeComponent();
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);
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

        public static void ThreadBuscar()

        {
            Application.Run(new Buscar());
        }

        public static void ThreadAlumno()
        {
            Application.Run(new Alumno());
        }

        public static void ThreadModificar()
        {
            Application.Run(new Modificar());
        }

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
        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
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

        private void BtnInscripcion_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadAlumno));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void btnModificarAlum_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadModificar));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void DataGridViewbuscar_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


            DataGridViewRow fila = dataGridViewbuscar.CurrentRow; // obtengo la fila actualmente seleccionada en el dataGridView

            sesion.Curp = Convert.ToString(fila.Cells[5].Value); //obtengo el valor de la primer columna

            

            MessageBox.Show(sesion.Curp);
        }
        public void eliminar()
        {
            string conexion = "server=localhost;uid=root;database=nerivela";
            MessageBox.Show(sesion.Curp);
            string eliminar = "delete from alumno where  CURP =" + "'" + sesion.Curp + "'";
            MessageBox.Show(eliminar);
            obj.ElimarAlum(conexion, eliminar);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            eliminar();
            datagrid(dataGridViewbuscar);
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

        private void Buscar_Load(object sender, EventArgs e)
        {
            datagrid(dataGridViewbuscar);
        }
    }
}
