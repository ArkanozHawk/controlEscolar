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
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using System.Windows.Forms.DataVisualization.Charting;

namespace Control_Escolar
{
    public partial class Graficas_Estad : MaterialForm
    {
        public Graficas_Estad()
        {
            InitializeComponent();

            //--------------------------------Seleccionamos todo de los alumnos de 1°--------------------------------

            string conexion = "server=localhost;uid=root;database=nerivela";
            string numHombres1 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 1 AND `Genero` = 'Masculino'";
            string numMujeres1 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 1 AND `Genero` = 'Femenino'";
            int total1 = 0;
            //Hombres
            MySqlConnection conn1;
            MySqlCommand com1;

            conn1 = new MySqlConnection(conexion);
            conn1.Open();

            com1 = new MySqlCommand(numHombres1, conn1);

            MySqlDataReader myreader1 = com1.ExecuteReader();
            myreader1.Read();
            int Hombres1 = Convert.ToInt32(myreader1["COUNT(*)"]);

            conn1.Close();
            //Mujeres
            MySqlConnection conn12;
            MySqlCommand com12;

            conn12 = new MySqlConnection(conexion);
            conn12.Open();

            com12 = new MySqlCommand(numMujeres1, conn12);

            MySqlDataReader myreader12 = com12.ExecuteReader();
            myreader12.Read();
            int Mujeres1 = Convert.ToInt32(myreader12["COUNT(*)"]);

            total1 = Hombres1 + Mujeres1;

            conn12.Close();
            //--------------------------------Seleccionamos todo de los alumnos de 2°--------------------------------

            string conexion1 = "server=localhost;uid=root;database=nerivela";
            string numHombres2 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 2 AND `Genero` = 'Masculino'";
            string numMujeres2 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 2 AND `Genero` = 'Femenino'";
            int total2 = 0;
            MySqlConnection conn2;
            MySqlCommand com2;

            conn2 = new MySqlConnection(conexion1);
            conn2.Open();

            com2 = new MySqlCommand(numHombres2, conn2);

            MySqlDataReader myreader2 = com2.ExecuteReader();
            myreader2.Read();
            int Hombres2 = Convert.ToInt32(myreader2["COUNT(*)"]);

            conn2.Close();

            //Mujeres
            MySqlConnection conn22;
            MySqlCommand com22;

            conn22 = new MySqlConnection(conexion1);
            conn22.Open();

            com22 = new MySqlCommand(numMujeres2, conn22);

            MySqlDataReader myreader22 = com22.ExecuteReader();
            myreader22.Read();
            int Mujeres2 = Convert.ToInt32(myreader22["COUNT(*)"]);

            total2 = Hombres2 + Mujeres2;

            conn22.Close();
            //--------------------------------Seleccionamos todo de los alumnos de 3°--------------------------------

            string conexion2 = "server=localhost;uid=root;database=nerivela";
            string numHombres3 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 3 AND `Genero` = 'Masculino'";
            string numMujeres3 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 3 AND `Genero` = 'Femenino'";
            int total3 = 0;
            MySqlConnection conn3;
            MySqlCommand com3;

            conn3 = new MySqlConnection(conexion2);
            conn3.Open();

            com3 = new MySqlCommand(numHombres3, conn3);

            MySqlDataReader myreader3 = com3.ExecuteReader();
            myreader3.Read();
            int Hombres3 = Convert.ToInt32(myreader3["COUNT(*)"]);

            conn3.Close();
            //Mujeres
            MySqlConnection conn32;
            MySqlCommand com32;

            conn32 = new MySqlConnection(conexion2);
            conn32.Open();

            com32 = new MySqlCommand(numMujeres3, conn32);

            MySqlDataReader myreader32 = com32.ExecuteReader();
            myreader32.Read();
            int Mujeres3 = Convert.ToInt32(myreader32["COUNT(*)"]);

            total3 = Hombres3 + Mujeres3;

            conn32.Close();

            //--------------------------------Seleccionamos todo de los alumnos de 4°--------------------------------

            string conexion3 = "server=localhost;uid=root;database=nerivela";
            string numHombres4 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 4 AND `Genero` = 'Masculino'";
            string numMujeres4 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 4 AND `Genero` = 'Femenino'";
            int total4 = 0;

            MySqlConnection conn4;
            MySqlCommand com4;

            conn4 = new MySqlConnection(conexion3);
            conn4.Open();

            com4 = new MySqlCommand(numHombres4, conn4);

            MySqlDataReader myreader4 = com4.ExecuteReader();
            myreader4.Read();
            int Hombres4 = Convert.ToInt32(myreader4["COUNT(*)"]);

            conn4.Close();
            //Mujeres
            MySqlConnection conn42;
            MySqlCommand com42;

            conn42 = new MySqlConnection(conexion3);
            conn42.Open();

            com42 = new MySqlCommand(numMujeres4, conn42);

            MySqlDataReader myreader42 = com42.ExecuteReader();
            myreader42.Read();
            int Mujeres4 = Convert.ToInt32(myreader42["COUNT(*)"]);

            total4 = Hombres4 + Mujeres4;

            conn42.Close();

            //--------------------------------Seleccionamos todo de los alumnos de 5°--------------------------------

            string conexion4 = "server=localhost;uid=root;database=nerivela";
            string numHombres5 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 5 AND `Genero` = 'Masculino'";
            string numMujeres5 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 5 AND `Genero` = 'Femenino'";
            int total5 = 0;

            MySqlConnection conn5;
            MySqlCommand com5;

            conn5 = new MySqlConnection(conexion4);
            conn5.Open();

            com5 = new MySqlCommand(numHombres5, conn5);

            MySqlDataReader myreader5 = com5.ExecuteReader();
            myreader5.Read();
            int Hombres5 = Convert.ToInt32(myreader5["COUNT(*)"]);

            conn5.Close();

            //Mujeres
            MySqlConnection conn52;
            MySqlCommand com52;

            conn52 = new MySqlConnection(conexion4);
            conn52.Open();

            com52 = new MySqlCommand(numMujeres5, conn52);

            MySqlDataReader myreader52 = com52.ExecuteReader();
            myreader52.Read();
            int Mujeres5 = Convert.ToInt32(myreader52["COUNT(*)"]);

            total5 = Hombres5 + Mujeres5;

            conn52.Close();
            //--------------------------------Seleccionamos todo de los alumnos de 6°--------------------------------

            string conexion5 = "server=localhost;uid=root;database=nerivela";
            string numHombres6 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 6 AND `Genero` = 'Masculino'";
            string numMujeres6 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 6 AND `Genero` = 'Femenino'";
            int total6 = 0;

            MySqlConnection conn6;
            MySqlCommand com6;

            conn6 = new MySqlConnection(conexion5);
            conn6.Open();

            com6 = new MySqlCommand(numHombres6, conn6);

            MySqlDataReader myreader6 = com6.ExecuteReader();
            myreader6.Read();
            int Hombres6 = Convert.ToInt32(myreader6["COUNT(*)"]);

            conn6.Close();
            //Mujeres
            MySqlConnection conn62;
            MySqlCommand com62;

            conn62 = new MySqlConnection(conexion5);
            conn62.Open();

            com62 = new MySqlCommand(numMujeres6, conn62);

            MySqlDataReader myreader62 = com62.ExecuteReader();
            myreader62.Read();
            int Mujeres6 = Convert.ToInt32(myreader62["COUNT(*)"]);

            total6 = Hombres6 + Mujeres6;

            conn62.Close();
            //------------------------------------------Suma de todos los hombres-----------------------------
            int HombresFin = 0;

            HombresFin = Hombres1 + Hombres2 + Hombres3 + Hombres4 + Hombres5 + Hombres6;

            //------------------------------------------Suma de todos las mujeres-----------------------------
            int MujeresFin = 0;

            MujeresFin = Mujeres1 + Mujeres2 + Mujeres3 + Mujeres4 + Mujeres5 + Mujeres6;

            //-----------------------------------Suma de todas las mujeres y hombres-----------------------------
            int totalFin = 0;

            totalFin = HombresFin + MujeresFin;
            //------------------------------------------------ 1° A------------------------------------------------
            //Vectores de los datos
            string[] series1 = { "Hombres", "Mujeres" };
            int[] puntos1 = { Hombres1, Mujeres1 };

            //Cambiar colores
            this.chart1.Palette = ChartColorPalette.Bright;


            for (int i = 0; i < series1.Length; i++)
            {

                Series series = chart1.Series.Add(series1[i]);
                // Aqui se agregan los Valores.
                series.Label = puntos1[i].ToString();

                series.Points.Add(puntos1[i]);
            }
            //------------------------------------------------ 2° A------------------------------------------------

            //Vectores de los datos
            string[] series2 = { "Hombres", "Mujeres" };
            int[] puntos2 = { Hombres2, Mujeres2 };

            //Cambiar colores
            this.chart1.Palette = ChartColorPalette.Bright;


            for (int i = 0; i < series2.Length; i++)
            {

                Series series12 = chart2.Series.Add(series2[i]);
                // Aqui se agregan los Valores.
                series12.Label = puntos2[i].ToString();

                series12.Points.Add(puntos2[i]);
            }
            //------------------------------------------------ 3° A------------------------------------------------
            //Vectores de los datos
            string[] series3 = { "Hombres", "Mujeres" };
            int[] puntos3 = { Hombres3, Mujeres3 };

            //Cambiar colores
            this.chart3.Palette = ChartColorPalette.Bright;


            for (int i = 0; i < series3.Length; i++)
            {

                Series series23 = chart3.Series.Add(series3[i]);
                // Aqui se agregan los Valores.
                series23.Label = puntos3[i].ToString();

                series23.Points.Add(puntos3[i]);
            }
            //------------------------------------------------ 4° A------------------------------------------------

            //Vectores de los datos
            string[] series4 = { "Hombres", "Mujeres" };
            int[] puntos4 = { Hombres4, Mujeres4 };

            //Cambiar colores
            this.chart4.Palette = ChartColorPalette.Bright;


            for (int i = 0; i < series4.Length; i++)
            {

                Series series34 = chart4.Series.Add(series4[i]);
                // Aqui se agregan los Valores.
                series34.Label = puntos4[i].ToString();

                series34.Points.Add(puntos4[i]);
            }
            //------------------------------------------------ 5° A------------------------------------------------

            //Vectores de los datos
            string[] series5 = { "Hombres", "Mujeres" };
            int[] puntos5 = { Hombres5, Mujeres5 };

            //Cambiar colores
            this.chart5.Palette = ChartColorPalette.Bright;


            for (int i = 0; i < series5.Length; i++)
            {

                Series series45 = chart5.Series.Add(series5[i]);
                // Aqui se agregan los Valores.
                series45.Label = puntos5[i].ToString();

                series45.Points.Add(puntos5[i]);
            }
            //------------------------------------------------ 6° A------------------------------------------------

            //Vectores de los datos
            string[] series6 = { "Hombres", "Mujeres" };
            int[] puntos6 = { Hombres6, Mujeres6 };
            //Cambiar colores
            this.chart6.Palette = ChartColorPalette.Bright;


            for (int i = 0; i < series6.Length; i++)
            {

                Series series56 = chart6.Series.Add(series6[i]);
                // Aqui se agregan los Valores.
                series56.Label = puntos6[i].ToString();

                series56.Points.Add(puntos6[i]);
            }
            //------------------------------------------------ Total------------------------------------------------

            //Vectores de los datos
            string[] series7 = { "Hombres", "Mujeres" };
            int[] puntos7 = { HombresFin, MujeresFin };
            //Cambiar colores
            this.chart7.Palette = ChartColorPalette.Bright;


            for (int i = 0; i < series7.Length; i++)
            {

                Series series67 = chart7.Series.Add(series7[i]);
                // Aqui se agregan los Valores.
                series67.Label = puntos7[i].ToString();

                series67.Points.Add(puntos7[i]);
            }
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

        public static void ThreadEstadisticas()
        {
            Application.Run(new Estadisticas ());
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

        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadEstadisticas));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void Graficas_Estad_Load(object sender, EventArgs e)
        {

        }

        private void materialTabSelector1_Click(object sender, EventArgs e)
        {

            
        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }

        
    }
}
