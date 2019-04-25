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
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

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
            //------------------------------------------------ Por alumno------------------------------------------------
            //-----------------------------contar cuantos alumnos hay en total en 1--------------------------------
            MySqlConnection conn34;
            MySqlCommand com34;

            string conexion34 = "server=localhost;uid=root;database=nerivela";
            string query34 = "SELECT COUNT(DISTINCT t1.idAlumno) AS NumAlum FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t2.idGrado = 1";
            conn34 = new MySqlConnection(conexion34);
            conn34.Open();

            com34 = new MySqlCommand(query34, conn34);

            MySqlDataReader myreader34 = com34.ExecuteReader();

            myreader34.Read();

            int NumAlum1 = Convert.ToInt32(myreader34["NumAlum"]);
            //--------------------------------------seleccionar al alumno con mas alto promedio de primero-----------------------------------
            MySqlConnection conn35;
            MySqlCommand com35;

            string conexion35 = "server=localhost;uid=root;database=nerivela";
            string query35 = "SELECT t1.idAlumno, t2.nombre, t2.ApellidoP, t2.ApellidoM, AVG( t1.CalificacionMen ) AS 'Promedios' FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t1.idMaterias != 57 AND t1.Mes != 'Diagnostico' AND t2.idGrado = 1 GROUP BY t1.idAlumno ORDER BY AVG(t1.CalificacionMen ) DESC";

            conn35 = new MySqlConnection(conexion35);
            conn35.Open();

            com35 = new MySqlCommand(query35, conn35);

            MySqlDataReader myreader35 = com35.ExecuteReader();

            myreader35.Read();
            string[] promedio1 = new string[NumAlum1];
            string[] IdAlumno1 = new string[NumAlum1];

            string[] nombre1 = new string[NumAlum1];
            string[] Apellidop1 = new string[NumAlum1];
            string[] Apellidom1 = new string[NumAlum1];

            for (int n = 0; n > NumAlum1; n++)
            {
                IdAlumno1[n] = " ";
                promedio1[n] = " ";
                nombre1[n] = " ";
                Apellidop1[n] = " ";
                Apellidom1[n] = " ";
            }

            int m = 0;
            while (myreader35.Read())
            {
                IdAlumno1[m] = Convert.ToString(myreader35["idAlumno"]);
                promedio1[m] = Convert.ToString(myreader35["Promedios"]);

                nombre1[m] = Convert.ToString(myreader35["nombre"]);
                Apellidop1[m] = Convert.ToString(myreader35["ApellidoP"]);
                Apellidom1[m] = Convert.ToString(myreader35["ApellidoM"]);

                m++;
            }
            conn5.Close();
            //-----------------------------contar cuantos alumnos hay en total en 2--------------------------------
            MySqlConnection conn36;
            MySqlCommand com36;

            string conexion36 = "server=localhost;uid=root;database=nerivela";
            string query36 = "SELECT COUNT(DISTINCT t1.idAlumno) AS NumAlum FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t2.idGrado = 2";
            conn36 = new MySqlConnection(conexion36);
            conn36.Open();

            com36 = new MySqlCommand(query36, conn36);

            MySqlDataReader myreader36 = com36.ExecuteReader();

            myreader36.Read();

            int NumAlum2 = Convert.ToInt32(myreader36["NumAlum"]);
            //--------------------------------------seleccionar al alumno con mas alto promedio de segundo-----------------------------------
            MySqlConnection conn7;
            MySqlCommand com7;

            string conexion7 = "server=localhost;uid=root;database=nerivela";
            string query7 = "SELECT t1.idAlumno, t2.nombre, t2.ApellidoP, t2.ApellidoM, AVG( t1.CalificacionMen ) AS 'Promedios' FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t1.idMaterias != 57 AND t1.Mes != 'Diagnostico' AND t2.idGrado = 2 GROUP BY t1.idAlumno ORDER BY AVG(t1.CalificacionMen ) DESC";

            conn7 = new MySqlConnection(conexion7);
            conn7.Open();

            com7 = new MySqlCommand(query7, conn7);

            MySqlDataReader myreader7 = com7.ExecuteReader();

            myreader7.Read();
            string[] promedio2 = new string[NumAlum2];
            string[] IdAlumno2 = new string[NumAlum2];

            string[] nombre2 = new string[NumAlum2];
            string[] Apellidop2 = new string[NumAlum2];
            string[] Apellidom2 = new string[NumAlum2];

            for (int n = 0; n > NumAlum2; n++)
            {
                IdAlumno2[n] = " ";
                promedio2[n] = " ";
                nombre2[n] = " ";
                Apellidop2[n] = " ";
                Apellidom2[n] = " ";
            }

            int l = 0;
            while (myreader7.Read())
            {
                IdAlumno2[l] = Convert.ToString(myreader7["idAlumno"]);
                promedio2[l] = Convert.ToString(myreader7["Promedios"]);

                nombre2[l] = Convert.ToString(myreader7["nombre"]);
                Apellidop2[l] = Convert.ToString(myreader7["ApellidoP"]);
                Apellidom2[l] = Convert.ToString(myreader7["ApellidoM"]);

                l++;
            }
            conn7.Close();
            //-----------------------------contar cuantos alumnos hay en total en 3--------------------------------
            MySqlConnection conn8;
            MySqlCommand com8;

            string conexion8 = "server=localhost;uid=root;database=nerivela";
            string query8 = "SELECT COUNT(DISTINCT t1.idAlumno) AS NumAlum FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t2.idGrado = 3";
            conn8 = new MySqlConnection(conexion8);
            conn8.Open();

            com8 = new MySqlCommand(query8, conn8);

            MySqlDataReader myreader8 = com8.ExecuteReader();

            myreader8.Read();

            int NumAlum3 = Convert.ToInt32(myreader8["NumAlum"]);
            //--------------------------------------seleccionar al alumno con mas alto promedio de tercero-----------------------------------
            MySqlConnection conn9;
            MySqlCommand com9;

            string conexion9 = "server=localhost;uid=root;database=nerivela";
            string query9 = "SELECT t1.idAlumno, t2.nombre, t2.ApellidoP, t2.ApellidoM, AVG( t1.CalificacionMen ) AS 'Promedios' FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t1.idMaterias != 57 AND t1.Mes != 'Diagnostico' AND t2.idGrado = 3 GROUP BY t1.idAlumno ORDER BY AVG(t1.CalificacionMen ) DESC";

            conn9 = new MySqlConnection(conexion9);
            conn9.Open();

            com9 = new MySqlCommand(query9, conn9);

            MySqlDataReader myreader9 = com9.ExecuteReader();

            myreader9.Read();
            string[] promedio3 = new string[NumAlum3];
            string[] IdAlumno3 = new string[NumAlum3];

            string[] nombre3 = new string[NumAlum3];
            string[] Apellidop3 = new string[NumAlum3];
            string[] Apellidom3 = new string[NumAlum3];

            for (int n = 0; n > NumAlum3; n++)
            {
                IdAlumno3[n] = " ";
                promedio3[n] = " ";
                nombre3[n] = " ";
                Apellidop3[n] = " ";
                Apellidom3[n] = " ";
            }

            int p = 0;
            while (myreader9.Read())
            {
                IdAlumno3[p] = Convert.ToString(myreader9["idAlumno"]);
                promedio3[p] = Convert.ToString(myreader9["Promedios"]);

                nombre3[p] = Convert.ToString(myreader9["nombre"]);
                Apellidop3[p] = Convert.ToString(myreader9["ApellidoP"]);
                Apellidom3[p] = Convert.ToString(myreader9["ApellidoM"]);

                p++;
            }
            conn9.Close();
            //-----------------------------contar cuantos alumnos hay en total en 4--------------------------------
            MySqlConnection conn10;
            MySqlCommand com10;

            string conexion10 = "server=localhost;uid=root;database=nerivela";
            string query10 = "SELECT COUNT(DISTINCT t1.idAlumno) AS NumAlum FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t2.idGrado = 4";
            conn10 = new MySqlConnection(conexion10);
            conn10.Open();

            com10 = new MySqlCommand(query10, conn10);

            MySqlDataReader myreader10 = com10.ExecuteReader();

            myreader10.Read();

            int NumAlum4 = Convert.ToInt32(myreader10["NumAlum"]);
            //--------------------------------------seleccionar al alumno con mas alto promedio de cuarto-----------------------------------
            MySqlConnection conn11;
            MySqlCommand com11;

            string conexion11 = "server=localhost;uid=root;database=nerivela";
            string query11 = "SELECT t1.idAlumno , t2.nombre, t2.ApellidoP, t2.ApellidoM, AVG( t1.CalificacionMen ) AS 'Promedios' FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t1.idMaterias != 57 AND t1.Mes != 'Diagnostico' AND t2.idGrado = 4 GROUP BY t1.idAlumno ORDER BY AVG(t1.CalificacionMen ) DESC";

            conn11 = new MySqlConnection(conexion11);
            conn11.Open();

            com11 = new MySqlCommand(query11, conn11);

            MySqlDataReader myreader11 = com11.ExecuteReader();

            myreader11.Read();
            string[] promedio4 = new string[NumAlum4];
            string[] IdAlumno4 = new string[NumAlum4];

            string[] nombre4 = new string[NumAlum4];
            string[] Apellidop4 = new string[NumAlum4];
            string[] Apellidom4 = new string[NumAlum4];

            for (int n = 0; n > NumAlum4; n++)
            {
                IdAlumno4[n] = " ";
                promedio4[n] = " ";
                nombre4[n] = " ";
                Apellidop4[n] = " ";
                Apellidom4[n] = " ";
            }

            int y = 0;
            while (myreader11.Read())
            {
                IdAlumno4[y] = Convert.ToString(myreader11["idAlumno"]);
                promedio4[y] = Convert.ToString(myreader11["Promedios"]);

                nombre4[y] = Convert.ToString(myreader11["nombre"]);
                Apellidop4[y] = Convert.ToString(myreader11["ApellidoP"]);
                Apellidom4[y] = Convert.ToString(myreader11["ApellidoM"]);

                y++;
            }
            conn11.Close();
            //-----------------------------contar cuantos alumnos hay en total en 5--------------------------------
            MySqlConnection conn312;
            MySqlCommand com312;

            string conexion312 = "server=localhost;uid=root;database=nerivela";
            string query312 = "SELECT COUNT(DISTINCT t1.idAlumno) AS NumAlum FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t2.idGrado = 5";
            conn312 = new MySqlConnection(conexion312);
            conn312.Open();

            com312 = new MySqlCommand(query312, conn312);

            MySqlDataReader myreader312 = com312.ExecuteReader();

            myreader312.Read();

            int NumAlum5 = Convert.ToInt32(myreader312["NumAlum"]);
            //--------------------------------------seleccionar al alumno con mas alto promedio de quinto-----------------------------------
            MySqlConnection conn13;
            MySqlCommand com13;

            string conexion13 = "server=localhost;uid=root;database=nerivela";
            string query13 = "SELECT t1.idAlumno, t2.nombre, t2.ApellidoP, t2.ApellidoM, AVG( t1.CalificacionMen ) AS 'Promedios' FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t1.idMaterias != 57 AND t1.Mes != 'Diagnostico' AND t2.idGrado = 5 GROUP BY t1.idAlumno ORDER BY AVG(t1.CalificacionMen ) DESC";

            conn13 = new MySqlConnection(conexion13);
            conn13.Open();

            com13 = new MySqlCommand(query13, conn13);

            MySqlDataReader myreader13 = com13.ExecuteReader();

            myreader13.Read();
            string[] promedio5 = new string[NumAlum5];
            string[] IdAlumno5 = new string[NumAlum5];

            string[] nombre5 = new string[NumAlum5];
            string[] Apellidop5 = new string[NumAlum5];
            string[] Apellidom5 = new string[NumAlum5];

            for (int n = 0; n > NumAlum5; n++)
            {
                IdAlumno5[n] = " ";
                promedio5[n] = " ";
                nombre5[n] = " ";
                Apellidop5[n] = " ";
                Apellidom5[n] = " ";
            }

            int x = 0;
            while (myreader13.Read())
            {
                IdAlumno5[x] = Convert.ToString(myreader13["idAlumno"]);
                promedio5[x] = Convert.ToString(myreader13["Promedios"]);

                nombre5[x] = Convert.ToString(myreader13["nombre"]);
                Apellidop5[x] = Convert.ToString(myreader13["ApellidoP"]);
                Apellidom5[x] = Convert.ToString(myreader13["ApellidoM"]);

                x++;
            }
            conn13.Close();
            //-----------------------------contar cuantos alumnos hay en total en 6--------------------------------
            MySqlConnection conn14;
            MySqlCommand com14;

            string conexion14 = "server=localhost;uid=root;database=nerivela";
            string query14 = "SELECT COUNT(DISTINCT t1.idAlumno) AS NumAlum FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t2.idGrado = 6";
            conn14 = new MySqlConnection(conexion14);
            conn14.Open();

            com14 = new MySqlCommand(query14, conn14);

            MySqlDataReader myreader14 = com14.ExecuteReader();

            myreader14.Read();

            int NumAlum6 = Convert.ToInt32(myreader14["NumAlum"]);
            //--------------------------------------seleccionar al alumno con mas alto promedio de sexto-----------------------------------
            MySqlConnection conn15;
            MySqlCommand com15;

            string conexion15 = "server=localhost;uid=root;database=nerivela";
            string query15 = "SELECT t1.idAlumno, t2.nombre, t2.ApellidoP, t2.ApellidoM, AVG( t1.CalificacionMen ) AS 'Promedios' FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t1.idMaterias != 57 AND t1.Mes != 'Diagnostico' AND t2.idGrado = 6 GROUP BY t1.idAlumno ORDER BY AVG(t1.CalificacionMen ) DESC";

            conn15 = new MySqlConnection(conexion15);
            conn15.Open();

            com15 = new MySqlCommand(query15, conn15);

            MySqlDataReader myreader15 = com15.ExecuteReader();

            myreader15.Read();
            string[] promedio6 = new string[NumAlum6];
            string[] IdAlumno6 = new string[NumAlum6];

            string[] nombre6 = new string[NumAlum6];
            string[] Apellidop6 = new string[NumAlum6];
            string[] Apellidom6 = new string[NumAlum6];

            for (int n = 0; n > NumAlum6; n++)
            {
                IdAlumno6[n] = " ";
                promedio6[n] = " ";
                nombre6[n] = " ";
                Apellidop6[n] = " ";
                Apellidom6[n] = " ";
            }

            int t = 0;
            while (myreader15.Read())
            {
                IdAlumno6[t] = Convert.ToString(myreader15["idAlumno"]);
                promedio6[t] = Convert.ToString(myreader15["Promedios"]);

                nombre6[t] = Convert.ToString(myreader15["nombre"]);
                Apellidop6[t] = Convert.ToString(myreader15["ApellidoP"]);
                Apellidom6[t] = Convert.ToString(myreader15["ApellidoM"]);

                t++;
            }
            conn15.Close();
            //-------------------------------------------------------------------------------------------------------------------
            //------------------------------------------------ Por grado------------------------------------------------

            //Vectores de los datos
            //string[] series8 = { "1° A", "2° A", "3° A", "4° A", "5° A", "6° A" };
            //int[] puntos8 = { Convert.ToInt32(promedio1[0]), Convert.ToInt32(promedio2[0]), Convert.ToInt32(promedio3[0]), Convert.ToInt32(promedio4[0]), Convert.ToInt32(promedio5[0]), Convert.ToInt32(promedio6[0]), };
            ////Cambiar colores
            //this.chart8.Palette = ChartColorPalette.Bright;

            //for (int i = 0; i < series8.Length; i++)
            //{

            //    Series series78 = chart8.Series.Add(series8[i]);
            //    // Aqui se agregan los Valores.
            //    series78.Label = puntos8[i].ToString();

            //    series78.Points.Add(puntos8[i]);
            //}

            //---------------------------------------------Prom. 1°------------------------------------------------------------
            MySqlConnection conn92;
            MySqlCommand com92;

            string conexion92 = "server=localhost;uid=root;database=nerivela";
            string query92 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) AS PromFinal FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 1 ";
            string Promedio1 = " ";
            double Prom1 = 0;

            conn92 = new MySqlConnection(conexion92);
            conn92.Open();

            com92 = new MySqlCommand(query92, conn92);

            MySqlDataReader myreader92 = com92.ExecuteReader();

            myreader92.Read();

            Prom1 = Convert.ToDouble(myreader92["PromFinal"]);
            Promedio1 = Prom1.ToString("0.#");

            conn92.Close();
            
            //---------------------------------------------Prom. 2°------------------------------------------------------------
            MySqlConnection conn133;
            MySqlCommand com133;

            string conexion133 = "server=localhost;uid=root;database=nerivela";
            string query133 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) AS PromFinal  FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 2 ";
            string Promedio2 = " ";
            double Prom2 = 0;

            conn133 = new MySqlConnection(conexion133);
            conn133.Open();

            com133 = new MySqlCommand(query133, conn133);

            MySqlDataReader myreader133 = com133.ExecuteReader();

            myreader133.Read();
            Prom2 = Convert.ToDouble(myreader133["PromFinal"]);
            Promedio2 = Prom2.ToString("0.#");

            conn133.Close();
            
            //---------------------------------------------Prom. 3°------------------------------------------------------------
            MySqlConnection conn143;
            MySqlCommand com143;

            string conexion143 = "server=localhost;uid=root;database=nerivela";
            string query143 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) AS PromFinal  FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 3 ";
            string Promedio3 = " ";
            double Prom3 = 0;

            conn143 = new MySqlConnection(conexion143);
            conn143.Open();

            com143 = new MySqlCommand(query143, conn143);

            MySqlDataReader myreader143 = com143.ExecuteReader();

            myreader143.Read();
            Prom3 = Convert.ToDouble(myreader143["PromFinal"]);
            Promedio3 = Prom3.ToString("0.#");

            conn143.Close();
            
            //---------------------------------------------Prom. 4°------------------------------------------------------------
            MySqlConnection conn153;
            MySqlCommand com153;

            string conexion153 = "server=localhost;uid=root;database=nerivela";
            string query153 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) AS PromFinal  FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 4 ";
            string Promedio4 = " ";
            double Prom4 = 0;

            conn153 = new MySqlConnection(conexion153);
            conn153.Open();

            com153 = new MySqlCommand(query153, conn153);

            MySqlDataReader myreader153 = com153.ExecuteReader();

            myreader153.Read();
            Prom4 = Convert.ToDouble(myreader153["PromFinal"]);
            Promedio4 = Prom4.ToString("0.#");

            conn153.Close();
            
            //---------------------------------------------Prom. 5°------------------------------------------------------------
            MySqlConnection conn16;
            MySqlCommand com16;

            string conexion16 = "server=localhost;uid=root;database=nerivela";
            string query16 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) AS PromFinal  FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 5 ";
            string Promedio5 = " ";
            double Prom5 = 0;

            conn16 = new MySqlConnection(conexion16);
            conn16.Open();

            com16 = new MySqlCommand(query16, conn16);

            MySqlDataReader myreader16 = com16.ExecuteReader();

            myreader16.Read();
            Prom5 = Convert.ToDouble(myreader16["PromFinal"]);
            Promedio5 = Prom5.ToString("0.#");

            conn16.Close();
            
            //---------------------------------------------Prom. 6°------------------------------------------------------------
            MySqlConnection conn17;
            MySqlCommand com17;

            string conexion17 = "server=localhost;uid=root;database=nerivela";
            string query17 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) AS PromFinal  FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 6 ";
            string Promedio6 = " ";
            double Prom6 = 0;

            conn17 = new MySqlConnection(conexion17);
            conn17.Open();

            com17 = new MySqlCommand(query17, conn17);

            MySqlDataReader myreader17 = com17.ExecuteReader();

            myreader17.Read();
            Prom6 = Convert.ToDouble(myreader17["PromFinal"]);
            Promedio6 = Prom6.ToString("0.#");

            conn17.Close();
            //-------------------------------------------------------------------------------------

            //----------------------------------Grafica por grupo
            //Vectores de los datos
            string[] series9 = { "1° A", "2° A", "3° A", "4° A", "5° A", "6° A" };
            int[] puntos9 = { Convert.ToInt32(Prom1), Convert.ToInt32(Prom2), Convert.ToInt32(Prom3), Convert.ToInt32(Prom4), Convert.ToInt32(Prom5), Convert.ToInt32(Prom6), };
            //Cambiar colores
            this.chart9.Palette = ChartColorPalette.Bright;

            for (int i = 0; i < series9.Length; i++)
            {

                Series series89 = chart9.Series.Add(series9[i]);
                // Aqui se agregan los Valores.
                series89.Label = puntos9[i].ToString();

                series89.Points.Add(puntos9[i]);
            }
            //-------------------------------------------------------------------------------------------
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

        private void materialRaisedButton2_Click(object sender, EventArgs e)
        {
             System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadEstadisticas));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void materialRaisedButton3_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }

        private void materialTabSelector1_Click_1(object sender, EventArgs e)
        {

        }

        private void Graficas_Estad_FormClosing(object sender, FormClosingEventArgs e)
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
