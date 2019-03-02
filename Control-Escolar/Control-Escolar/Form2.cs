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
using MaterialSkin.Controls;
//using ValidarDatos;

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
        //Validar obj = new Validar();

        public static void ThreadProc()

        {
            Application.Run(new login());
        }

        public static void ThreadPrincipal()

        {
            Application.Run(new principal());
        }

        public static void ThreadForm3()

        {
            Application.Run(new Form3());
        }

        public static void ThreadBuscar()

        {
            Application.Run(new Buscar());
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {

        }

        private void txtApPat_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void materialSingleLineTextField5_Click(object sender, EventArgs e)
        {

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

        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void MaterialRaisedButton1_Click(object sender, EventArgs e)
        {

        }

        private void BtnSiguiente_Click(object sender, EventArgs e)
        {
            sesion.fnac = txtFeNac.Text;
            CalcEdad(sesion.fnac);
            inscripcion();
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadForm3));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void BtnBuscar_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadBuscar));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void btnInscripcion_Click(object sender, EventArgs e)
        {

        }

        private void txtTelEme_Validating(object sender, CancelEventArgs e)
        {
            int num2;
            if (!int.TryParse(txtTelEme_A.Text, out num2))
            {
                errorProvider1.SetError(txtTelEme_A, "Solo ingrese numero");
            }
            else
            {
                errorProvider1.SetError(txtTelEme_A, "");
            }
        }

        private void txtNombre_Click(object sender, EventArgs e)
        {

        }

        private void txtNombre_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtNombre_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtNombre_A, "Ingresar el nombre");
            }
            else
            {
                errorProvider1.SetError(this.txtNombre_A, "");
            }
        }

        private void txtNombre_Validated(object sender, EventArgs e)
        {
            
        }

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }


        public void  CalcEdad(string fnac)
        {
            DateTime dat = Convert.ToDateTime(fnac);
            DateTime nacimiento = new DateTime(dat.Year, dat.Month, dat.Day);
            int edad1 = DateTime.Today.AddTicks(-nacimiento.Ticks).Year - 1;
            MessageBox.Show(edad1.ToString());
           int  edad2 = Convert.ToInt32(txtEdad_A.Text);
            if(edad1 == edad2)
            {
                sesion.edad = edad1;
            }

            else { MessageBox.Show("nNo coincide edad con fecha de nacimiento "); }
            
        }


        public void  inscripcion()
        {
            string  Nombre_T, AP_T, AM_T, Calle_T, Numero_T, Colonia_T, CP_T, Telefono_T, Celular_T, Profesion_T, LT_T;
            sesion.nombre = txtNombre_A.Text;
            sesion. AP = txtApPat_A.Text;
            sesion.AM = txtApMat_A.Text;
            sesion.Curp = txtCURP_A.Text;
            sesion.calle = txtNum_A.Text;
            sesion.numero = txtNum_A.Text;
            sesion. Colonia = txtCP_A.Text;
            sesion. CP = txtCP_A.Text;
            sesion. LN = txtLugarNac_A.Text;
            
            sesion.edad = Convert.ToInt32(txtEdad_A.Text);

            sesion.telefono = txtNombre_A.Text;
            sesion. Alergia = txtAlergias_A.Text;
           
        }

        private void txtApPat_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtApPat_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtApPat_A, "Ingresar apellido paterno");
            }
            else
            {
                errorProvider1.SetError(this.txtApPat_A, "");
            }
        }

        private void txtApMat_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtApMat_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtApMat_A, "Ingresar apellido materno");
            }
            else
            {
                errorProvider1.SetError(this.txtApMat_A, "");
            }
        }

        private void txtCURP_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtCURP_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtCURP_A, "Ingresar CURP");
            }
            else
            {
                errorProvider1.SetError(this.txtCURP_A, "");
            }
        }

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

        private void txtNum_A_Validating(object sender, CancelEventArgs e)
        {
            int num;
            if (!int.TryParse(txtNum_A.Text, out num))
            {
                errorProvider1.SetError(txtNum_A, "Ingesar el dato en numeros");
            }
            else
            {
                errorProvider1.SetError(txtNum_A, "");
            }
        }

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

        private void txtCP_A_Validating(object sender, CancelEventArgs e)
        {
            int num1;
            if (!int.TryParse(txtCP_A.Text, out num1))
            {
                errorProvider1.SetError(txtCP_A, "Ingesar el CP en numeros");
            }
            else
            {
                errorProvider1.SetError(txtCP_A, "");
            }
        }

        private void txtFeNac_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtFeNac.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtFeNac, "Ingresar la fecha de nacimiento");
            }
            else
            {
                errorProvider1.SetError(this.txtFeNac, "");
            }
        }

        private void txtLugarNac_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtLugarNac_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtLugarNac_A, "Ingresar el lugar de nacimiento");
            }
            else
            {
                errorProvider1.SetError(this.txtLugarNac_A, "");
            }
        }

        private void txtEdad_A_Validating(object sender, CancelEventArgs e)
        {
            int num5;
            if (!int.TryParse(txtEdad_A.Text, out num5))
            {
                errorProvider1.SetError(txtEdad_A, "Ingesar el dato en numeros");
            }
            else
            {
                errorProvider1.SetError(txtEdad_A, "");
            }
        }

        private void txtAlergias_A_Validating(object sender, CancelEventArgs e)
        {
            if (this.txtAlergias_A.Text.Length == 0)
            {
                errorProvider1.SetError(this.txtAlergias_A, "Ingresar las alergias del alumno");
            }
            else
            {
                errorProvider1.SetError(this.txtAlergias_A, "");
            }
        }
    }
}
