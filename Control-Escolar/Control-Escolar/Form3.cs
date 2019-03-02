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


        public static void ThreadPrincipal()

        {
            Application.Run(new principal());
        }

        public static void ThreadProc()

        {
            Application.Run(new login());
        }

        public static void ThreadBuscar()

        {
            Application.Run(new Buscar());
        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void MaterialSingleLineTextField7_Click(object sender, EventArgs e)
        {

        }

        private void BtnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
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

        private void materialRaisedButton1_Click(object sender, EventArgs e)
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

        }

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
    }
}
