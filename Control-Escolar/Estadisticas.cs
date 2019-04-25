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

namespace Control_Escolar
{
    public partial class Estadisticas : MaterialForm
    {
        public Estadisticas()
        {
            InitializeComponent();
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

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //--------------------------------Seleccionamos todo de los alumnos de 1°--------------------------------
  
            string conexion = "server=localhost;uid=root;database=nerivela";
            string numHombres1 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 1 AND `Genero` = 'Masculino'";
            string numMujeres1 = "SELECT COUNT(*) FROM `alumno` WHERE `idGrado` = 1 AND `Genero` = 'Femenino'";
            int total1 = 0 ;
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
            //------------------------------------------------------------------------------------------------------
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
            if (!Directory.Exists(folderPath))// pregunt si no existe
            {
                Directory.CreateDirectory(folderPath); // si no existe lo crea
            }
            // Creamos el documento con el tamaño de página tradicional
            FileStream stream = new FileStream(folderPath + "Estadisticas-Num-niños.pdf", FileMode.Create);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, stream);

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Boleta interna");
            doc.AddCreator("equipo master");

            // Abrimos el archivo
            doc.Open();

            iTextSharp.text.Font tituloprin = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font cuerpo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font letchica = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font letmed = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);



            // Creamos una tabla que contendrá  tooodooooo
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

            iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../guerrero-logo.jpg");
            logoGro.BorderWidth = 0;
            logoGro.ScaleAbsolute(150, 70);
            iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
            logoEst.ScaleAbsolute(160, 60);


            // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
            float[] Celdas = { 0.25f, 0.45f, 0.20f, 0.20f, 0.20f };

            // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
            table.SetWidths(Celdas);


            PdfPCell cell390 = new PdfPCell(logoGro);
            cell390.Colspan = 2;//toma columnas
            cell390.BorderWidth = 0;
            cell390.PaddingTop = 5f;
            cell390.PaddingBottom = 5f;
            cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell390);

            PdfPCell cell380 = new PdfPCell(logoEst);
            cell380.Colspan = 3;//toma columnas
            cell380.BorderWidth = 0;
            cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell380.PaddingTop = 5f;
            cell380.PaddingBottom = 5f;
            table.AddCell(cell380);

            PdfPCell cell39 = new PdfPCell(new Phrase("ESTADÍSTICA BÁSICA POR CENTRO DEL TRABAJO\n\nNÚMERO DE NIÑOS Y NIÑAS", tituloprin));
            cell39.Colspan = 5;//toma columnas
            cell39.BorderWidth = 0;
            cell39.PaddingTop = 5f;
            cell39.PaddingBottom = 5f;
            cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell39);

            PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letmed));
            cell38.Colspan = 5;//toma columnas
            cell38.BorderWidth = 0;
            cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell38.PaddingTop = 5f;
            cell38.PaddingBottom = 5f;
            table.AddCell(cell38);

            PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                     Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100", cuerpo));
            cell398.Colspan = 5;//toma columnas
            cell398.BorderWidth = 0;
            cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell398.PaddingTop = 5f;
            cell398.PaddingBottom = 5f;
            table.AddCell(cell398);

            PdfPCell cell391 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                                                                                                              Zona : 048", cuerpo));
            cell391.Colspan = 5;//toma columnas
            cell391.BorderWidth = 0;
            cell391.PaddingTop = 5f;
            cell391.PaddingBottom = 5f;
            cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell391);

            PdfPCell cell40 = new PdfPCell(new Phrase("Localidad : ACAPULCO DE JUAREZ                                                                                                                                                                Ciclo Escolar : 2018-2019", cuerpo));
            cell40.Colspan = 5;//toma columnas
            cell40.BorderWidth = 0;
            cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell40.PaddingTop = 5f;
            cell40.PaddingBottom = 5f;
            table.AddCell(cell40);

            PdfPCell cell53 = new PdfPCell(new Phrase("Municipio : ACAPULCO DE JUAREZ                                                                                                                                                                Id. Docto :", cuerpo));
            cell53.Colspan = 5;//toma columnas
            cell53.BorderWidth = 0;
            cell53.PaddingTop = 5f;
            cell53.PaddingBottom = 10f;
            cell53.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell53);


            PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4311.Colspan = 5;//toma columnas
            cell4311.BorderWidth = 0;
            table.AddCell(cell4311);

            PdfPCell cell44 = new PdfPCell(new Phrase("GRADO ", cuerpo));
            cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell44);

            PdfPCell cell42 = new PdfPCell(new Phrase("GRUPO", cuerpo));
            cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell42);

            PdfPCell cell441 = new PdfPCell(new Phrase("HOMBRES ", cuerpo));
            cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell441);

            PdfPCell cell421 = new PdfPCell(new Phrase("MUJERES", cuerpo));
            cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell421);

            PdfPCell cell426 = new PdfPCell(new Phrase("TOTAL", cuerpo));
            cell426.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell426);

            PdfPCell cell4312 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4312.Colspan = 5;//toma columnas
            cell4312.BorderWidth = 0;
            table.AddCell(cell4312);

            PdfPCell cell445 = new PdfPCell(new Phrase("1 ", cuerpo));
            cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell445);

            PdfPCell cell425 = new PdfPCell(new Phrase("A", cuerpo));
            cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell425);

            PdfPCell cell447 = new PdfPCell(new Phrase(" "+ Hombres1, cuerpo));
            cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell447);

            PdfPCell cell427 = new PdfPCell(new Phrase(" " + Mujeres1, cuerpo));
            cell427.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell427);

            PdfPCell cell428 = new PdfPCell(new Phrase(" " + total1, cuerpo));
            cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell428);

            PdfPCell cell448 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell448.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell448);

            PdfPCell cell725 = new PdfPCell(new Phrase(" ", cuerpo));
            cell725.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell725);

            PdfPCell cell747 = new PdfPCell(new Phrase(" " + Hombres1, cuerpo));
            cell747.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell747);

            PdfPCell cell727 = new PdfPCell(new Phrase(" " + Mujeres1, cuerpo));
            cell727.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell727);

            PdfPCell cell728 = new PdfPCell(new Phrase(" " + total1, cuerpo));
            cell728.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell728);

            PdfPCell cell4325 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4325.Colspan = 5;//toma columnas
            cell4325.BorderWidth = 0;
            table.AddCell(cell4325);

            PdfPCell cell455 = new PdfPCell(new Phrase("2 ", cuerpo));
            cell455.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell455);

            PdfPCell cell415 = new PdfPCell(new Phrase("A", cuerpo));
            cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell415);

            PdfPCell cell417 = new PdfPCell(new Phrase(" " + Hombres2, cuerpo));
            cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell417);

            PdfPCell cell437 = new PdfPCell(new Phrase(" " + Mujeres2, cuerpo));
            cell437.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell437);

            PdfPCell cell468 = new PdfPCell(new Phrase(" " + total2, cuerpo));
            cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell468);

            PdfPCell cell648 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell648.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell648);

            PdfPCell cell765 = new PdfPCell(new Phrase(" ", cuerpo));
            cell765.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell765);

            PdfPCell cell767 = new PdfPCell(new Phrase(" " + Hombres2, cuerpo));
            cell767.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell767);

            PdfPCell cell777 = new PdfPCell(new Phrase(" " + Mujeres2, cuerpo));
            cell777.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell777);

            PdfPCell cell778 = new PdfPCell(new Phrase(" " + total2, cuerpo));
            cell778.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell778);

            PdfPCell cell04 = new PdfPCell(new Phrase(" ", cuerpo));
            cell04.Colspan = 5;//toma columnas
            cell04.BorderWidth = 0;
            table.AddCell(cell04);

            PdfPCell cell05 = new PdfPCell(new Phrase("3 ", cuerpo));
            cell05.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell05);

            PdfPCell cell06 = new PdfPCell(new Phrase("A", cuerpo));
            cell06.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell06);

            PdfPCell cell07 = new PdfPCell(new Phrase(" " + Hombres3, cuerpo));
            cell07.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell07);

            PdfPCell cell071 = new PdfPCell(new Phrase(" " + Mujeres3, cuerpo));
            cell071.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell071);

            PdfPCell cell08 = new PdfPCell(new Phrase(" " + total3, cuerpo));
            cell08.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell08);

            PdfPCell cell09 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell09.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell09);

            PdfPCell cell010 = new PdfPCell(new Phrase(" ", cuerpo));
            cell010.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell010);

            PdfPCell cell011 = new PdfPCell(new Phrase(" " + Hombres3, cuerpo));
            cell011.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell011);

            PdfPCell cell012 = new PdfPCell(new Phrase(" " + Mujeres3, cuerpo));
            cell012.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell012);

            PdfPCell cell013 = new PdfPCell(new Phrase(" "+ total3, cuerpo));
            cell013.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell013);

            PdfPCell cell014 = new PdfPCell(new Phrase(" ", cuerpo));
            cell014.Colspan = 5;//toma columnas
            cell014.BorderWidth = 0;
            table.AddCell(cell014);

            PdfPCell cell015 = new PdfPCell(new Phrase("4 ", cuerpo));
            cell015.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell015);

            PdfPCell cell016 = new PdfPCell(new Phrase("A", cuerpo));
            cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell016);

            PdfPCell cell017 = new PdfPCell(new Phrase("  " + Hombres4, cuerpo));
            cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell017);

            PdfPCell cell018 = new PdfPCell(new Phrase(" " + Mujeres4, cuerpo));
            cell018.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell018);

            PdfPCell cell019 = new PdfPCell(new Phrase(" " + total4, cuerpo));
            cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell019);

            PdfPCell cell020 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell020.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell020);

            PdfPCell cell021 = new PdfPCell(new Phrase(" ", cuerpo));
            cell021.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell021);

            PdfPCell cell022 = new PdfPCell(new Phrase(" " + Hombres4, cuerpo));
            cell022.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell022);

            PdfPCell cell023 = new PdfPCell(new Phrase(" " + Mujeres4, cuerpo));
            cell023.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell023);

            PdfPCell cell024 = new PdfPCell(new Phrase(" " + total4, cuerpo));
            cell024.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell024);

            PdfPCell cell034 = new PdfPCell(new Phrase(" ", cuerpo));
            cell034.Colspan = 5;//toma columnas
            cell034.BorderWidth = 0;
            table.AddCell(cell034);

            PdfPCell cell035 = new PdfPCell(new Phrase("5 ", cuerpo));
            cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell035);

            PdfPCell cell036 = new PdfPCell(new Phrase("A", cuerpo));
            cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell036);

            PdfPCell cell037 = new PdfPCell(new Phrase(" " + Hombres5, cuerpo));
            cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell037);

            PdfPCell cell0373 = new PdfPCell(new Phrase(" " + Mujeres5, cuerpo));
            cell0373.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell0373);

            PdfPCell cell038 = new PdfPCell(new Phrase(" " + total5, cuerpo));
            cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell038);

            PdfPCell cell039 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell039.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell039);

            PdfPCell cell040 = new PdfPCell(new Phrase(" ", cuerpo));
            cell040.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell040);

            PdfPCell cell041 = new PdfPCell(new Phrase(" " + Hombres5, cuerpo));
            cell041.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell041);

            PdfPCell cell042 = new PdfPCell(new Phrase(" " + Mujeres5, cuerpo));
            cell042.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell042);

            PdfPCell cell043 = new PdfPCell(new Phrase(" " + total5, cuerpo));
            cell043.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell043);

            PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
            cell044.Colspan = 5;//toma columnas
            cell044.BorderWidth = 0;
            table.AddCell(cell044);

            PdfPCell cell045 = new PdfPCell(new Phrase("6 ", cuerpo));
            cell045.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell045);

            PdfPCell cell046 = new PdfPCell(new Phrase("A", cuerpo));
            cell046.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell046);

            PdfPCell cell047 = new PdfPCell(new Phrase(" " + Hombres6, cuerpo));
            cell047.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell047);

            PdfPCell cell048 = new PdfPCell(new Phrase(" " + Mujeres6, cuerpo));
            cell048.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell048);

            PdfPCell cell049 = new PdfPCell(new Phrase(" "+ total6, cuerpo));
            cell049.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell049);

            PdfPCell cell050 = new PdfPCell(new Phrase("SUBTOTAL ", cuerpo));
            cell050.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell050);

            PdfPCell cell051 = new PdfPCell(new Phrase(" ", cuerpo));
            cell051.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell051);

            PdfPCell cell052 = new PdfPCell(new Phrase(" " + Hombres6, cuerpo));
            cell052.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell052);

            PdfPCell cell053 = new PdfPCell(new Phrase(" " + Mujeres6, cuerpo));
            cell053.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell053);

            PdfPCell cell054 = new PdfPCell(new Phrase(" " + total6, cuerpo));
            cell054.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell054);

            PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4331.Colspan = 5;//toma columnas
            cell4331.BorderWidth = 0;
            table.AddCell(cell4331);

            PdfPCell cell244 = new PdfPCell(new Phrase("TOTAL GENERAL ", cuerpo));
            cell244.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell244);

            PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
            cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell242);

            PdfPCell cell241 = new PdfPCell(new Phrase(" " + HombresFin, cuerpo));
            cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell241);

            PdfPCell cell221 = new PdfPCell(new Phrase(" " + MujeresFin, cuerpo));
            cell221.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell221);

            PdfPCell cell226 = new PdfPCell(new Phrase(" " + totalFin, cuerpo));
            cell226.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell226);

            PdfPCell cell2211 = new PdfPCell(new Phrase("SUPERVISOR(A)\n\n\n\n________________________________________________", cuerpo));
            cell2211.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell2211.Colspan = 2;
            cell2211.BorderWidth = 0;
            cell2211.PaddingTop = 45f;
            table.AddCell(cell2211);

            PdfPCell cell2261 = new PdfPCell(new Phrase("DIRECTOR(A)\n\n\n\n________________________________________________", cuerpo));
            cell2261.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell2261.Colspan = 3;
            cell2261.BorderWidth = 0;
            cell2261.PaddingTop = 45f;
            table.AddCell(cell2261);
            
            doc.Add(table);

            doc.Close();
            writer.Close();

            MessageBox.Show("¡PDF creado!");
        }

        //private void pictureBox6_Click(object sender, EventArgs e)
        //{
        //    //-------------Ingresar los datos del alumno en pdf--------------------------------
        //    MySqlConnection conn;
        //    MySqlCommand com;

        //    string conexion = "server=localhost;uid=root;database=nerivela";
        //    string query = "SELECT idAlumno, AVG( CalificacionMen ) FROM calificacionesWHERE idMaterias != 57 AND Mes != 'Diagnostico' GROUP BY idAlumno ORDER BY AVG(CalificacionMen ) DESC";

        //    conn = new MySqlConnection(conexion);
        //    conn.Open();

        //    com = new MySqlCommand(query, conn);

        //    MySqlDataReader myreader = com.ExecuteReader();

        //    string[] promedio = new string[3];
        //    string[] IdAlumno = new string[3];

        //    for (int n = 0; n > 3; n++)
        //    {
        //        IdAlumno[n] = " ";
        //        promedio[n] = " ";
        //    }

        //    int L = 0;
        //    while (myreader.Read())
        //    {
        //        IdAlumno[0] = Convert.ToString(myreader["idAlumno"]);
        //        promedio[0] = Convert.ToString(myreader["AVG(`CalificacionMen`)"]);

        //        L++;
        //    }
        //    conn.Close();
        //    ////----------------------------------------------------------------------------------
        //    MySqlConnection conn1;
        //    MySqlCommand com1;

        //    string conexion1 = "server=localhost;uid=root;database=nerivela";
        //    string query1 = "SELECT * FROM Alumno WHERE idAlumno = " + IdAlumno[0] + " ";
        //    string nombre, Apellidop, Apellidom;

        //    conn1 = new MySqlConnection(conexion1);
        //    conn1.Open();

        //    com1 = new MySqlCommand(query1, conn1);

        //    MySqlDataReader myreader1 = com1.ExecuteReader();

        //    nombre = Convert.ToString(myreader1["nombre"]);
        //    Apellidop = Convert.ToString(myreader1["ApellidoP"]);
        //    Apellidom = Convert.ToString(myreader1["ApellidoM"]);

        //    conn1.Close();
        //    //---------------------------------------------Prom. 1°------------------------------------------------------------
        //    MySqlConnection conn2;
        //    MySqlCommand com2;

        //    string conexion2 = "server=localhost;uid=root;database=nerivela";
        //    string query2 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 1 ";
        //    string Promedio1;
        //    double Prom1 = 0;

        //    conn2 = new MySqlConnection(conexion2);
        //    conn2.Open();

        //    com2 = new MySqlCommand(query2, conn2);

        //    MySqlDataReader myreader2 = com2.ExecuteReader();

        //    Prom1 = Convert.ToDouble(myreader1["AVG( DISTINCT t2.idMaterias =57 )"]);
        //    Promedio1 = Prom1.ToString("0.#");

        //    conn2.Close();
        //    // Creamos el documento con el tamaño de página tradicional
        //    Document doc = new Document(PageSize.LETTER);
        //    string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
        //    if (!Directory.Exists(folderPath))// pregunt si no existe
        //    {
        //        Directory.CreateDirectory(folderPath); // si no existe lo crea
        //    }
        //    // Creamos el documento con el tamaño de página tradicional
        //    FileStream stream = new FileStream(folderPath + "Estadisticas-Promedios-Alumnos.pdf", FileMode.Create);
        //    // Indicamos donde vamos a guardar el documento
        //    PdfWriter writer = PdfWriter.GetInstance(doc, stream);

        //    // Le colocamos el título y el autor
        //    // **Nota: Esto no será visible en el documento
        //    doc.AddTitle("Boleta interna");
        //    doc.AddCreator("equipo master");

        //    // Abrimos el archivo
        //    doc.Open();

        //    iTextSharp.text.Font tituloprin = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //    iTextSharp.text.Font titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

        //    iTextSharp.text.Font cuerpo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //    iTextSharp.text.Font letchica = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

        //    iTextSharp.text.Font letmed = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);



        //    // Creamos una tabla que contendrá  tooodooooo
        //    PdfPTable table = new PdfPTable(5);
        //    table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

        //    iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../guerrero-logo.jpg");
        //    logoGro.BorderWidth = 0;
        //    logoGro.ScaleAbsolute(150, 70);
        //    iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
        //    logoEst.ScaleAbsolute(160, 60);


        //    // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
        //    // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
        //    float[] Celdas = { 0.25f, 0.55f, 0.50f, 0.20f, 0.20f };

        //    // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
        //    table.SetWidths(Celdas);


        //    PdfPCell cell390 = new PdfPCell(logoGro);
        //    cell390.Colspan = 2;//toma columnas
        //    cell390.BorderWidth = 0;
        //    cell390.PaddingTop = 5f;
        //    cell390.PaddingBottom = 5f;
        //    cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell390);

        //    PdfPCell cell380 = new PdfPCell(logoEst);
        //    cell380.Colspan = 3;//toma columnas
        //    cell380.BorderWidth = 0;
        //    cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    cell380.PaddingTop = 5f;
        //    cell380.PaddingBottom = 5f;
        //    table.AddCell(cell380);

        //    PdfPCell cell39 = new PdfPCell(new Phrase("ESTADÍSTICA BÁSICA POR CENTRO DEL TRABAJO\n\nALUMNOS CON MEJORES PROMEDIOS", tituloprin));
        //    cell39.Colspan = 5;//toma columnas
        //    cell39.BorderWidth = 0;
        //    cell39.PaddingTop = 5f;
        //    cell39.PaddingBottom = 5f;
        //    cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell39);

        //    PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letmed));
        //    cell38.Colspan = 5;//toma columnas
        //    cell38.BorderWidth = 0;
        //    cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    cell38.PaddingTop = 5f;
        //    cell38.PaddingBottom = 5f;
        //    table.AddCell(cell38);

        //    PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                     Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100", cuerpo));
        //    cell398.Colspan = 5;//toma columnas
        //    cell398.BorderWidth = 0;
        //    cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    cell398.PaddingTop = 5f;
        //    cell398.PaddingBottom = 5f;
        //    table.AddCell(cell398);

        //    PdfPCell cell391 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                                                                                                              Zona : 048", cuerpo));
        //    cell391.Colspan = 5;//toma columnas
        //    cell391.BorderWidth = 0;
        //    cell391.PaddingTop = 5f;
        //    cell391.PaddingBottom = 5f;
        //    cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell391);

        //    PdfPCell cell40 = new PdfPCell(new Phrase("Localidad : ACAPULCO DE JUAREZ                                                                                                                                                                Ciclo Escolar : 2018-2019", cuerpo));
        //    cell40.Colspan = 5;//toma columnas
        //    cell40.BorderWidth = 0;
        //    cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    cell40.PaddingTop = 5f;
        //    cell40.PaddingBottom = 5f;
        //    table.AddCell(cell40);

        //    PdfPCell cell53 = new PdfPCell(new Phrase("Municipio : ACAPULCO DE JUAREZ                                                                                                                                                                Id. Docto :", cuerpo));
        //    cell53.Colspan = 5;//toma columnas
        //    cell53.BorderWidth = 0;
        //    cell53.PaddingTop = 5f;
        //    cell53.PaddingBottom = 10f;
        //    cell53.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell53);


        //    PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell4311.Colspan = 5;//toma columnas
        //    cell4311.BorderWidth = 0;
        //    table.AddCell(cell4311);

        //    PdfPCell cell44 = new PdfPCell(new Phrase("GRADO ", cuerpo));
        //    cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell44);

        //    PdfPCell cell42 = new PdfPCell(new Phrase("GRUPO", cuerpo));
        //    cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell42);

        //    PdfPCell cell441 = new PdfPCell(new Phrase("ALUMNO ", cuerpo));
        //    cell441.Colspan = 2;
        //    cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell441);

        //    PdfPCell cell421 = new PdfPCell(new Phrase("CALIFICACIÓN", cuerpo));
        //    cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell421);


        //    PdfPCell cell4312 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell4312.Colspan = 5;//toma columnas
        //    cell4312.BorderWidth = 0;
        //    table.AddCell(cell4312);

        //    PdfPCell cell445 = new PdfPCell(new Phrase("1 ", cuerpo));
        //    cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell445);

        //    PdfPCell cell425 = new PdfPCell(new Phrase("A", cuerpo));
        //    cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell425);

        //    PdfPCell cell447 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell447.Colspan = 2;
        //    cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell447);

        //    PdfPCell cell428 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell428);

        //    PdfPCell cell4325 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell4325.Colspan = 5;//toma columnas
        //    cell4325.BorderWidth = 0;
        //    table.AddCell(cell4325);

        //    PdfPCell cell455 = new PdfPCell(new Phrase("2 ", cuerpo));
        //    cell455.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell455);

        //    PdfPCell cell415 = new PdfPCell(new Phrase("A", cuerpo));
        //    cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell415);

        //    PdfPCell cell417 = new PdfPCell(new Phrase("  ", cuerpo));
        //    cell417.Colspan = 2;
        //    cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell417);

        //    PdfPCell cell468 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell468);

        //    PdfPCell cell04 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell04.Colspan = 5;//toma columnas
        //    cell04.BorderWidth = 0;
        //    table.AddCell(cell04);

        //    PdfPCell cell05 = new PdfPCell(new Phrase("3 ", cuerpo));
        //    cell05.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell05);

        //    PdfPCell cell06 = new PdfPCell(new Phrase("A", cuerpo));
        //    cell06.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell06);

        //    PdfPCell cell07 = new PdfPCell(new Phrase("  ", cuerpo));
        //    cell07.Colspan = 2;
        //    cell07.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell07);

        //    PdfPCell cell08 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell08.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell08);

        //    PdfPCell cell014 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell014.Colspan = 5;//toma columnas
        //    cell014.BorderWidth = 0;
        //    table.AddCell(cell014);

        //    PdfPCell cell015 = new PdfPCell(new Phrase("4 ", cuerpo));
        //    cell015.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell015);

        //    PdfPCell cell016 = new PdfPCell(new Phrase("A", cuerpo));
        //    cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell016);

        //    PdfPCell cell017 = new PdfPCell(new Phrase("  ", cuerpo));
        //    cell017.Colspan = 2;
        //    cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell017);

        //    PdfPCell cell019 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell019);

        //    PdfPCell cell034 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell034.Colspan = 5;//toma columnas
        //    cell034.BorderWidth = 0;
        //    table.AddCell(cell034);

        //    PdfPCell cell035 = new PdfPCell(new Phrase("5 ", cuerpo));
        //    cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell035);

        //    PdfPCell cell036 = new PdfPCell(new Phrase("A", cuerpo));
        //    cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell036);

        //    PdfPCell cell037 = new PdfPCell(new Phrase("  ", cuerpo));
        //    cell037.Colspan = 2;
        //    cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell037);

        //    PdfPCell cell038 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell038);

        //    PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell044.Colspan = 5;//toma columnas
        //    cell044.BorderWidth = 0;
        //    table.AddCell(cell044);

        //    PdfPCell cell045 = new PdfPCell(new Phrase("6 ", cuerpo));
        //    cell045.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell045);

        //    PdfPCell cell046 = new PdfPCell(new Phrase("A", cuerpo));
        //    cell046.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell046);

        //    PdfPCell cell047 = new PdfPCell(new Phrase("  ", cuerpo));
        //    cell047.Colspan = 2;
        //    cell047.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell047);

        //    PdfPCell cell049 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell049.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell049);

        //    PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell4331.Colspan = 5;//toma columnas
        //    cell4331.BorderWidth = 0;
        //    table.AddCell(cell4331);

        //    PdfPCell cell244 = new PdfPCell(new Phrase("PROMEDIO MÁS ALTO ", cuerpo));
        //    cell244.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell244);

        //    PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
        //    cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell242);

        //    PdfPCell cell241 = new PdfPCell(new Phrase(" " /*+ Apellidop + " " + Apellidom + " " + nombre*/, cuerpo));
        //    cell241.Colspan = 2;
        //    cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell241);

        //    PdfPCell cell226 = new PdfPCell(new Phrase(" " /*+ promedio[0]*/, cuerpo));
        //    cell226.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    table.AddCell(cell226);

        //    PdfPCell cell2211 = new PdfPCell(new Phrase("SUPERVISOR(A)\n\n\n\n________________________________________________", cuerpo));
        //    cell2211.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    cell2211.Colspan = 2;
        //    cell2211.BorderWidth = 0;
        //    cell2211.PaddingTop = 45f;
        //    table.AddCell(cell2211);

        //    PdfPCell cell2261 = new PdfPCell(new Phrase("DIRECTOR(A)\n\n\n\n________________________________________________", cuerpo));
        //    cell2261.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
        //    cell2261.Colspan = 3;
        //    cell2261.BorderWidth = 0;
        //    cell2261.PaddingTop = 45f;
        //    table.AddCell(cell2261);

        //    //encabezado

        //    /* table.AddCell(" ");

        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     table.AddCell(" ");
        //     */

        //    doc.Add(table);

        //    doc.Close();
        //    writer.Close();

        //    MessageBox.Show("¡PDF creado!");
        //}

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //-------------Ingresar los datos del 1° maestro en pdf--------------------------------
            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT nombre, ApellidoP, ApellidoM FROM `maestros` WHERE gradoEncargado ='1' ";
            string nombre, Apellidop, Apellidom;

            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(query, conn);

            MySqlDataReader myreader = com.ExecuteReader();

            myreader.Read();
            nombre = Convert.ToString(myreader["nombre"]);
            Apellidop = Convert.ToString(myreader["ApellidoP"]);
            Apellidom = Convert.ToString(myreader["ApellidoM"]);
            conn.Close();
            //---------------------------------------------Prom. 1°------------------------------------------------------------
            MySqlConnection conn12;
            MySqlCommand com12;

            string conexion12 = "server=localhost;uid=root;database=nerivela";
            string query12 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) AS PromFinal FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 1 ";
            string Promedio1 = " ";
            double Prom1 = 0;

            conn12 = new MySqlConnection(conexion12);
            conn12.Open();

            com12 = new MySqlCommand(query12, conn12);
            
            MySqlDataReader myreader12 = com12.ExecuteReader();

            myreader12.Read();

            Prom1 = Convert.ToDouble(myreader12["PromFinal"]);
            Promedio1 = Prom1.ToString("0.##");

            conn12.Close();
            //-------------Ingresar los datos del 2° maestro en pdf--------------------------------
            MySqlConnection conn1;
            MySqlCommand com1;

            string conexion1 = "server=localhost;uid=root;database=nerivela";
            string query1 = "SELECT nombre, ApellidoP, ApellidoM FROM `maestros` WHERE gradoEncargado ='2' ";
            string nombre1, Apellidop1, Apellidom1;

            conn1 = new MySqlConnection(conexion1);
            conn1.Open();

            com1 = new MySqlCommand(query1, conn1);

            MySqlDataReader myreader1 = com1.ExecuteReader();

            myreader1.Read();
            nombre1 = Convert.ToString(myreader1["nombre"]);
            Apellidop1 = Convert.ToString(myreader1["ApellidoP"]);
            Apellidom1 = Convert.ToString(myreader1["ApellidoM"]);
            conn1.Close();
            //---------------------------------------------Prom. 2°------------------------------------------------------------
            MySqlConnection conn13;
            MySqlCommand com13;

            string conexion13 = "server=localhost;uid=root;database=nerivela";
            string query13 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) AS PromFinal  FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 2 ";
            string Promedio2 = " ";
            double Prom2 = 0;

            conn13 = new MySqlConnection(conexion13);
            conn13.Open();

            com13 = new MySqlCommand(query13, conn13);

            MySqlDataReader myreader13 = com13.ExecuteReader();

            myreader13.Read();
            Prom2 = Convert.ToDouble(myreader13["PromFinal"]);
            Promedio2 = Prom2.ToString("0.##");

            conn13.Close();
            //-------------Ingresar los datos del 3° maestro en pdf--------------------------------
            MySqlConnection conn2;
            MySqlCommand com2;

            string conexion2 = "server=localhost;uid=root;database=nerivela";
            string query2 = "SELECT nombre, ApellidoP, ApellidoM FROM `maestros` WHERE gradoEncargado ='3' ";
            string nombre2, Apellidop2, Apellidom2;

            conn2 = new MySqlConnection(conexion2);
            conn2.Open();

            com2 = new MySqlCommand(query2, conn2);

            MySqlDataReader myreader2 = com2.ExecuteReader();

            myreader2.Read();
            nombre2 = Convert.ToString(myreader2["nombre"]);
            Apellidop2 = Convert.ToString(myreader2["ApellidoP"]);
            Apellidom2 = Convert.ToString(myreader2["ApellidoM"]);
            conn2.Close();
            //---------------------------------------------Prom. 3°------------------------------------------------------------
            MySqlConnection conn14;
            MySqlCommand com14;

            string conexion14 = "server=localhost;uid=root;database=nerivela";
            string query14 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) AS PromFinal  FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 3 ";
            string Promedio3 = " ";
            double Prom3 = 0;

            conn14 = new MySqlConnection(conexion14);
            conn14.Open();

            com14 = new MySqlCommand(query14, conn14);

            MySqlDataReader myreader14 = com14.ExecuteReader();
            
            myreader14.Read();
            Prom3 = Convert.ToDouble(myreader14["PromFinal"]);
            Promedio3 = Prom3.ToString("0.##");

            conn14.Close();
            //-------------Ingresar los datos del 4° maestro en pdf--------------------------------
            MySqlConnection conn3;
            MySqlCommand com3;

            string conexion3 = "server=localhost;uid=root;database=nerivela";
            string query3 = "SELECT nombre, ApellidoP, ApellidoM FROM `maestros` WHERE gradoEncargado ='4' ";
            string nombre3, Apellidop3, Apellidom3;

            conn3 = new MySqlConnection(conexion3);
            conn3.Open();

            com3 = new MySqlCommand(query3, conn3);

            MySqlDataReader myreader3 = com3.ExecuteReader();

            myreader3.Read();
            nombre3 = Convert.ToString(myreader3["nombre"]);
            Apellidop3 = Convert.ToString(myreader3["ApellidoP"]);
            Apellidom3 = Convert.ToString(myreader3["ApellidoM"]);
            conn3.Close();
            //---------------------------------------------Prom. 4°------------------------------------------------------------
            MySqlConnection conn15;
            MySqlCommand com15;

            string conexion15 = "server=localhost;uid=root;database=nerivela";
            string query15 = "SELECT t1.idGrado, AVG( DISTINCT t2.idMaterias =57 ) AS PromFinal  FROM Alumno AS t1 INNER JOIN Calificaciones AS t2 WHERE t1.idGrado = 4 ";
            string Promedio4 = " ";
            double Prom4 = 0;

            conn15 = new MySqlConnection(conexion15);
            conn15.Open();

            com15 = new MySqlCommand(query15, conn15);

            MySqlDataReader myreader15 = com15.ExecuteReader();

            myreader15.Read();
            Prom4 = Convert.ToDouble(myreader15["PromFinal"]);
            Promedio4 = Prom4.ToString("0.##");

            conn15.Close();
            //-------------Ingresar los datos del 5° maestro en pdf--------------------------------
            MySqlConnection conn4;
            MySqlCommand com4;

            string conexion4 = "server=localhost;uid=root;database=nerivela";
            string query4 = "SELECT nombre, ApellidoP, ApellidoM FROM `maestros` WHERE gradoEncargado ='5' ";
            string nombre4, Apellidop4, Apellidom4;

            conn4 = new MySqlConnection(conexion4);
            conn4.Open();

            com4 = new MySqlCommand(query4, conn4);

            MySqlDataReader myreader4 = com4.ExecuteReader();

            myreader4.Read();
            nombre4 = Convert.ToString(myreader4["nombre"]);
            Apellidop4 = Convert.ToString(myreader4["ApellidoP"]);
            Apellidom4 = Convert.ToString(myreader4["ApellidoM"]);
            conn4.Close();
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
            Promedio5 = Prom5.ToString("0.##");

            conn16.Close();
            //-------------Ingresar los datos del 6° maestro en pdf--------------------------------
            MySqlConnection conn6;
            MySqlCommand com6;

            string conexion6 = "server=localhost;uid=root;database=nerivela";
            string query6 = "SELECT nombre, ApellidoP, ApellidoM FROM `maestros` WHERE gradoEncargado ='6' ";
            string nombre5, Apellidop5, Apellidom5;

            conn6 = new MySqlConnection(conexion6);
            conn6.Open();

            com6 = new MySqlCommand(query6, conn6);

            MySqlDataReader myreader6 = com6.ExecuteReader();

            myreader6.Read();
            nombre5 = Convert.ToString(myreader6["nombre"]);
            Apellidop5 = Convert.ToString(myreader6["ApellidoP"]);
            Apellidom5 = Convert.ToString(myreader6["ApellidoM"]);
            conn6.Close();
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
            Promedio6 = Prom6.ToString("0.##");

            conn17.Close();
            //-----------------------------------------Grupo mas alto-------------------------------------
            double[] promedios = { Prom1, Prom2, Prom3, Prom4, Prom5, Prom6 };
            double alta = 0;
            string PromMasAlto = " ";
            int i = 0;
            for(i = 0; i < (promedios.Length -1); i++)
            {
                if(promedios[i+1] > promedios[i])
                {
                    alta = promedios[i + 1];
                }
                else
                {
                    alta = promedios[i];

                }
            }

            string Nombre = " ";
            string apellidop = " ";
            string apellidom = " ";

            if (i == 1)
            {
                Nombre = nombre;
                apellidop = Apellidop;
                apellidom = Apellidom;

            }
            else
            {
                if(i == 2)
                {
                    Nombre = nombre1;
                    apellidop = Apellidop1;
                    apellidom = Apellidom1;
                }
                else
                {
                    if (i == 3)
                    {
                        Nombre = nombre2;
                        apellidop = Apellidop2;
                        apellidom = Apellidom2;
                    }
                    else
                    {
                        if (i == 4)
                        {
                            Nombre = nombre3;
                            apellidop = Apellidop3;
                            apellidom = Apellidom3;
                        }
                        else
                        {
                            if (i == 5)
                            {
                                Nombre = nombre4;
                                apellidop = Apellidop4;
                                apellidom = Apellidom4;
                            }
                            else
                            {
                                if (i == 6)
                                {
                                    Nombre = nombre5;
                                    apellidop = Apellidop5;
                                    apellidom = Apellidom5;
                                }
                                else
                                {
                                    MessageBox.Show("Ningun grupo tiene mayor promedio");
                                }
                            }
                        }
                    }
                }
            }
            PromMasAlto = alta.ToString("0.#");
            //---------------------------------------------------------------------------------------------------------
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
            if (!Directory.Exists(folderPath))// pregunt si no existe
            {
                Directory.CreateDirectory(folderPath); // si no existe lo crea
            }
            // Creamos el documento con el tamaño de página tradicional
            FileStream stream = new FileStream(folderPath + "Estadisticas-Promedios-Mejor-Grupo.pdf", FileMode.Create);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, stream);

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Boleta interna");
            doc.AddCreator("equipo master");

            // Abrimos el archivo
            doc.Open();

            iTextSharp.text.Font tituloprin = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font cuerpo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font letchica = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font letmed = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);



            // Creamos una tabla que contendrá  tooodooooo
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

            iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../guerrero-logo.jpg");
            logoGro.BorderWidth = 0;
            logoGro.ScaleAbsolute(150, 70);
            iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
            logoEst.ScaleAbsolute(160, 60);


            // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
            // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
            float[] Celdas = { 0.25f, 0.55f, 0.50f, 0.20f, 0.20f };

            // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
            table.SetWidths(Celdas);


            PdfPCell cell390 = new PdfPCell(logoGro);
            cell390.Colspan = 2;//toma columnas
            cell390.BorderWidth = 0;
            cell390.PaddingTop = 5f;
            cell390.PaddingBottom = 5f;
            cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell390);

            PdfPCell cell380 = new PdfPCell(logoEst);
            cell380.Colspan = 3;//toma columnas
            cell380.BorderWidth = 0;
            cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell380.PaddingTop = 5f;
            cell380.PaddingBottom = 5f;
            table.AddCell(cell380);

            PdfPCell cell39 = new PdfPCell(new Phrase("ESTADÍSTICA BÁSICA POR CENTRO DEL TRABAJO\n\nGRUPO CON MEJOR PROMEDIO", tituloprin));
            cell39.Colspan = 5;//toma columnas
            cell39.BorderWidth = 0;
            cell39.PaddingTop = 5f;
            cell39.PaddingBottom = 5f;
            cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell39);

            PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letmed));
            cell38.Colspan = 5;//toma columnas
            cell38.BorderWidth = 0;
            cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell38.PaddingTop = 5f;
            cell38.PaddingBottom = 5f;
            table.AddCell(cell38);

            PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                     Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100", cuerpo));
            cell398.Colspan = 5;//toma columnas
            cell398.BorderWidth = 0;
            cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell398.PaddingTop = 5f;
            cell398.PaddingBottom = 5f;
            table.AddCell(cell398);

            PdfPCell cell391 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                                                                                                              Zona : 048", cuerpo));
            cell391.Colspan = 5;//toma columnas
            cell391.BorderWidth = 0;
            cell391.PaddingTop = 5f;
            cell391.PaddingBottom = 5f;
            cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell391);

            PdfPCell cell40 = new PdfPCell(new Phrase("Localidad : ACAPULCO DE JUAREZ                                                                                                                                                                Ciclo Escolar : 2018-2019", cuerpo));
            cell40.Colspan = 5;//toma columnas
            cell40.BorderWidth = 0;
            cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell40.PaddingTop = 5f;
            cell40.PaddingBottom = 5f;
            table.AddCell(cell40);

            PdfPCell cell53 = new PdfPCell(new Phrase("Municipio : ACAPULCO DE JUAREZ                                                                                                                                                                Id. Docto :", cuerpo));
            cell53.Colspan = 5;//toma columnas
            cell53.BorderWidth = 0;
            cell53.PaddingTop = 5f;
            cell53.PaddingBottom = 10f;
            cell53.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell53);


            PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4311.Colspan = 5;//toma columnas
            cell4311.BorderWidth = 0;
            table.AddCell(cell4311);

            PdfPCell cell44 = new PdfPCell(new Phrase("GRADO ", cuerpo));
            cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell44);

            PdfPCell cell42 = new PdfPCell(new Phrase("GRUPO", cuerpo));
            cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell42);

            PdfPCell cell441 = new PdfPCell(new Phrase("MAESTRO ", cuerpo));
            cell441.Colspan = 2;
            cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell441);

            PdfPCell cell421 = new PdfPCell(new Phrase("PROMEDIO", cuerpo));
            cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell421);


            PdfPCell cell4312 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4312.Colspan = 5;//toma columnas
            cell4312.BorderWidth = 0;
            table.AddCell(cell4312);

            PdfPCell cell445 = new PdfPCell(new Phrase("1 ", cuerpo));
            cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell445);

            PdfPCell cell425 = new PdfPCell(new Phrase("A", cuerpo));
            cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell425);

            PdfPCell cell447 = new PdfPCell(new Phrase(" " + Apellidop +" "+ Apellidom + " " + nombre, cuerpo));
            cell447.Colspan = 2;
            cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell447);

            PdfPCell cell428 = new PdfPCell(new Phrase(" "+ Promedio1, cuerpo));
            cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell428);

            PdfPCell cell4325 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4325.Colspan = 5;//toma columnas
            cell4325.BorderWidth = 0;
            table.AddCell(cell4325);

            PdfPCell cell455 = new PdfPCell(new Phrase("2 ", cuerpo));
            cell455.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell455);

            PdfPCell cell415 = new PdfPCell(new Phrase("A", cuerpo));
            cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell415);

            PdfPCell cell417 = new PdfPCell(new Phrase(" " + Apellidop1 + " " + Apellidom1 + " " + nombre1, cuerpo));
            cell417.Colspan = 2;
            cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell417);

            PdfPCell cell468 = new PdfPCell(new Phrase(" "+ Promedio2, cuerpo));
            cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell468);

            PdfPCell cell04 = new PdfPCell(new Phrase(" ", cuerpo));
            cell04.Colspan = 5;//toma columnas
            cell04.BorderWidth = 0;
            table.AddCell(cell04);

            PdfPCell cell05 = new PdfPCell(new Phrase("3 ", cuerpo));
            cell05.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell05);

            PdfPCell cell06 = new PdfPCell(new Phrase("A", cuerpo));
            cell06.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell06);

            PdfPCell cell07 = new PdfPCell(new Phrase(" " + Apellidop2 + " " + Apellidom2 + " " + nombre2, cuerpo));
            cell07.Colspan = 2;
            cell07.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell07);

            PdfPCell cell08 = new PdfPCell(new Phrase(" "+ Promedio3, cuerpo));
            cell08.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell08);

            PdfPCell cell014 = new PdfPCell(new Phrase(" ", cuerpo));
            cell014.Colspan = 5;//toma columnas
            cell014.BorderWidth = 0;
            table.AddCell(cell014);

            PdfPCell cell015 = new PdfPCell(new Phrase("4 ", cuerpo));
            cell015.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell015);

            PdfPCell cell016 = new PdfPCell(new Phrase("A", cuerpo));
            cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell016);

            PdfPCell cell017 = new PdfPCell(new Phrase(" " + Apellidop3 + " " + Apellidom3 + " " + nombre3, cuerpo));
            cell017.Colspan = 2;
            cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell017);

            PdfPCell cell019 = new PdfPCell(new Phrase(" "+ Promedio4, cuerpo));
            cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell019);

            PdfPCell cell034 = new PdfPCell(new Phrase(" ", cuerpo));
            cell034.Colspan = 5;//toma columnas
            cell034.BorderWidth = 0;
            table.AddCell(cell034);

            PdfPCell cell035 = new PdfPCell(new Phrase("5 ", cuerpo));
            cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell035);

            PdfPCell cell036 = new PdfPCell(new Phrase("A", cuerpo));
            cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell036);

            PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop4 +" " + Apellidom4 + " " + nombre4, cuerpo));
            cell037.Colspan = 2;
            cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell037);

            PdfPCell cell038 = new PdfPCell(new Phrase(" "+ Promedio5, cuerpo));
            cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell038);

            PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
            cell044.Colspan = 5;//toma columnas
            cell044.BorderWidth = 0;
            table.AddCell(cell044);

            PdfPCell cell045 = new PdfPCell(new Phrase("6 ", cuerpo));
            cell045.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell045);

            PdfPCell cell046 = new PdfPCell(new Phrase("A", cuerpo));
            cell046.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell046);

            PdfPCell cell047 = new PdfPCell(new Phrase(" " + Apellidop5 +" "+ Apellidom5 +" "+ nombre5, cuerpo));
            cell047.Colspan = 2;
            cell047.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell047);

            PdfPCell cell049 = new PdfPCell(new Phrase(" "+ Promedio6, cuerpo));
            cell049.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell049);

            PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4331.Colspan = 5;//toma columnas
            cell4331.BorderWidth = 0;
            table.AddCell(cell4331);

            PdfPCell cell244 = new PdfPCell(new Phrase("PROMEDIO MÁS ALTO ", cuerpo));
            cell244.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell244);

            PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
            cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell242);

            PdfPCell cell241 = new PdfPCell(new Phrase(" "+apellidop+" "+apellidom+" "+Nombre, cuerpo));
            cell241.Colspan = 2;
            cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell241);

            PdfPCell cell226 = new PdfPCell(new Phrase(" "+ PromMasAlto, cuerpo));
            cell226.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell226);

            PdfPCell cell2211 = new PdfPCell(new Phrase("SUPERVISOR(A)\n\n\n\n________________________________________________", cuerpo));
            cell2211.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell2211.Colspan = 2;
            cell2211.BorderWidth = 0;
            cell2211.PaddingTop = 45f;
            table.AddCell(cell2211);

            PdfPCell cell2261 = new PdfPCell(new Phrase("DIRECTOR(A)\n\n\n\n________________________________________________", cuerpo));
            cell2261.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell2261.Colspan = 3;
            cell2261.BorderWidth = 0;
            cell2261.PaddingTop = 45f;
            table.AddCell(cell2261);

            //encabezado

            /* table.AddCell(" ");

             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             */

            doc.Add(table);

            doc.Close();
            writer.Close();

            MessageBox.Show("¡PDF creado!");
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            //-------------contar cuantos alumnos hay en total--------------------------------
            MySqlConnection conn33;
            MySqlCommand com33;

            string conexion33 = "server=localhost;uid=root;database=nerivela";
            string query33 = "SELECT COUNT(DISTINCT idAlumno) AS NumAlum FROM calificaciones";
            conn33 = new MySqlConnection(conexion33);
            conn33.Open();

            com33 = new MySqlCommand(query33, conn33);

            MySqlDataReader myreader33 = com33.ExecuteReader();

            myreader33.Read();

            int NumAlum = Convert.ToInt32(myreader33["NumAlum"]);
            //--------------------------------------seleccionar al alumno con mas alto promedio-----------------------------------
            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT idAlumno, AVG( CalificacionMen ) AS 'Promedios' FROM calificaciones WHERE idMaterias != 57 AND Mes != 'Diagnostico' GROUP BY idAlumno ORDER BY AVG(CalificacionMen ) DESC";

            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(query, conn);

            MySqlDataReader myreader = com.ExecuteReader();

            myreader.Read();
            string[] promedio = new string[NumAlum];
            string[] IdAlumno = new string[NumAlum];

            for (int n = 0; n > NumAlum; n++)
            {
                IdAlumno[n] = " ";
                promedio[n] = " ";
            }

            int L = 0;
            while (myreader.Read())
            {
                IdAlumno[L] = Convert.ToString(myreader["idAlumno"]);
                promedio[L] = Convert.ToString(myreader["Promedios"]);

                L++;
            }
            conn.Close();
            ////--------------------------------------Identificar al alumno--------------------------------------
            MySqlConnection conn1;
            MySqlCommand com1;

            string conexion1 = "server=localhost;uid=root;database=nerivela";
            string query1 = "SELECT * FROM Alumno WHERE idAlumno = " + IdAlumno[0] + " ";
            string nombre, Apellidop, Apellidom;

            conn1 = new MySqlConnection(conexion1);
            conn1.Open();

            com1 = new MySqlCommand(query1, conn1);

            MySqlDataReader myreader1 = com1.ExecuteReader();

            myreader1.Read();
            nombre = Convert.ToString(myreader1["nombre"]);
            Apellidop = Convert.ToString(myreader1["ApellidoP"]);
            Apellidom = Convert.ToString(myreader1["ApellidoM"]);

            conn1.Close();
            //-----------------------------contar cuantos alumnos hay en total en 1--------------------------------
            MySqlConnection conn4;
            MySqlCommand com4;

            string conexion4 = "server=localhost;uid=root;database=nerivela";
            string query4 = "SELECT COUNT(DISTINCT t1.idAlumno) AS NumAlum FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t2.idGrado = 1";
            conn4 = new MySqlConnection(conexion4);
            conn4.Open();

            com4 = new MySqlCommand(query4, conn4);

            MySqlDataReader myreader4 = com4.ExecuteReader();

            myreader4.Read();

            int NumAlum1 = Convert.ToInt32(myreader4["NumAlum"]);
            //--------------------------------------seleccionar al alumno con mas alto promedio de primero-----------------------------------
            MySqlConnection conn5;
            MySqlCommand com5;

            string conexion5 = "server=localhost;uid=root;database=nerivela";
            string query5 = "SELECT t1.idAlumno, t2.nombre, t2.ApellidoP, t2.ApellidoM, AVG( t1.CalificacionMen ) AS 'Promedios' FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t1.idMaterias != 57 AND t1.Mes != 'Diagnostico' AND t2.idGrado = 1 GROUP BY t1.idAlumno ORDER BY AVG(t1.CalificacionMen ) DESC";

            conn5 = new MySqlConnection(conexion5);
            conn5.Open();

            com5 = new MySqlCommand(query5, conn5);

            MySqlDataReader myreader5 = com5.ExecuteReader();

            myreader5.Read();
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
            while (myreader5.Read())
            {
                IdAlumno1[m] = Convert.ToString(myreader5["idAlumno"]);
                promedio1[m] = Convert.ToString(myreader5["Promedios"]);

                nombre1[m] = Convert.ToString(myreader5["nombre"]);
                Apellidop1[m] = Convert.ToString(myreader5["ApellidoP"]);
                Apellidom1[m] = Convert.ToString(myreader5["ApellidoM"]);

                m++;
            }
            conn5.Close();
            //-----------------------------contar cuantos alumnos hay en total en 2--------------------------------
            MySqlConnection conn6;
            MySqlCommand com6;

            string conexion6 = "server=localhost;uid=root;database=nerivela";
            string query6 = "SELECT COUNT(DISTINCT t1.idAlumno) AS NumAlum FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t2.idGrado = 2";
            conn6 = new MySqlConnection(conexion6);
            conn6.Open();

            com6 = new MySqlCommand(query6, conn6);

            MySqlDataReader myreader6 = com6.ExecuteReader();

            myreader6.Read();

            int NumAlum2 = Convert.ToInt32(myreader6["NumAlum"]);
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
            MySqlConnection conn12;
            MySqlCommand com12;

            string conexion12 = "server=localhost;uid=root;database=nerivela";
            string query12 = "SELECT COUNT(DISTINCT t1.idAlumno) AS NumAlum FROM calificaciones AS t1 INNER JOIN alumno AS t2 ON t1.idAlumno = t2.idAlumno WHERE t2.idGrado = 5";
            conn12 = new MySqlConnection(conexion12);
            conn12.Open();

            com12 = new MySqlCommand(query12, conn12);

            MySqlDataReader myreader12 = com12.ExecuteReader();

            myreader12.Read();

            int NumAlum5 = Convert.ToInt32(myreader12["NumAlum"]);
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
            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
            if (!Directory.Exists(folderPath))// pregunt si no existe
            {
                Directory.CreateDirectory(folderPath); // si no existe lo crea
            }
            // Creamos el documento con el tamaño de página tradicional
            FileStream stream = new FileStream(folderPath + "Estadisticas-Promedios-Alumnos.pdf", FileMode.Create);
            // Indicamos donde vamos a guardar el documento
            PdfWriter writer = PdfWriter.GetInstance(doc, stream);

            // Le colocamos el título y el autor
            // **Nota: Esto no será visible en el documento
            doc.AddTitle("Boleta interna");
            doc.AddCreator("equipo master");

            // Abrimos el archivo
            doc.Open();

            iTextSharp.text.Font tituloprin = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font titulos = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 9, iTextSharp.text.Font.BOLD, BaseColor.BLACK);

            iTextSharp.text.Font cuerpo = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 7, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font letchica = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 5, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

            iTextSharp.text.Font letmed = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);



            // Creamos una tabla que contendrá  tooodooooo
            PdfPTable table = new PdfPTable(5);
            table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

            iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../guerrero-logo.jpg");
            logoGro.BorderWidth = 0;
            logoGro.ScaleAbsolute(150, 70);
            iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
            logoEst.ScaleAbsolute(160, 60);


            // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
            // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
            float[] Celdas = { 0.25f, 0.55f, 0.50f, 0.20f, 0.20f };

            // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
            table.SetWidths(Celdas);


            PdfPCell cell390 = new PdfPCell(logoGro);
            cell390.Colspan = 2;//toma columnas
            cell390.BorderWidth = 0;
            cell390.PaddingTop = 5f;
            cell390.PaddingBottom = 5f;
            cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell390);

            PdfPCell cell380 = new PdfPCell(logoEst);
            cell380.Colspan = 3;//toma columnas
            cell380.BorderWidth = 0;
            cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell380.PaddingTop = 5f;
            cell380.PaddingBottom = 5f;
            table.AddCell(cell380);

            PdfPCell cell39 = new PdfPCell(new Phrase("ESTADÍSTICA BÁSICA POR CENTRO DEL TRABAJO\n\nALUMNOS CON MEJORES PROMEDIOS", tituloprin));
            cell39.Colspan = 5;//toma columnas
            cell39.BorderWidth = 0;
            cell39.PaddingTop = 5f;
            cell39.PaddingBottom = 5f;
            cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell39);

            PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letmed));
            cell38.Colspan = 5;//toma columnas
            cell38.BorderWidth = 0;
            cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell38.PaddingTop = 5f;
            cell38.PaddingBottom = 5f;
            table.AddCell(cell38);

            PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                     Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100", cuerpo));
            cell398.Colspan = 5;//toma columnas
            cell398.BorderWidth = 0;
            cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell398.PaddingTop = 5f;
            cell398.PaddingBottom = 5f;
            table.AddCell(cell398);

            PdfPCell cell391 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                                                                                                              Zona : 048", cuerpo));
            cell391.Colspan = 5;//toma columnas
            cell391.BorderWidth = 0;
            cell391.PaddingTop = 5f;
            cell391.PaddingBottom = 5f;
            cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell391);

            PdfPCell cell40 = new PdfPCell(new Phrase("Localidad : ACAPULCO DE JUAREZ                                                                                                                                                                Ciclo Escolar : 2018-2019", cuerpo));
            cell40.Colspan = 5;//toma columnas
            cell40.BorderWidth = 0;
            cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell40.PaddingTop = 5f;
            cell40.PaddingBottom = 5f;
            table.AddCell(cell40);

            PdfPCell cell53 = new PdfPCell(new Phrase("Municipio : ACAPULCO DE JUAREZ                                                                                                                                                                Id. Docto :", cuerpo));
            cell53.Colspan = 5;//toma columnas
            cell53.BorderWidth = 0;
            cell53.PaddingTop = 5f;
            cell53.PaddingBottom = 10f;
            cell53.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell53);


            PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4311.Colspan = 5;//toma columnas
            cell4311.BorderWidth = 0;
            table.AddCell(cell4311);

            PdfPCell cell44 = new PdfPCell(new Phrase("GRADO ", cuerpo));
            cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell44);

            PdfPCell cell42 = new PdfPCell(new Phrase("GRUPO", cuerpo));
            cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell42);

            PdfPCell cell441 = new PdfPCell(new Phrase("ALUMNO ", cuerpo));
            cell441.Colspan = 2;
            cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell441);

            PdfPCell cell421 = new PdfPCell(new Phrase("CALIFICACIÓN", cuerpo));
            cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell421);


            PdfPCell cell4312 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4312.Colspan = 5;//toma columnas
            cell4312.BorderWidth = 0;
            table.AddCell(cell4312);

            PdfPCell cell445 = new PdfPCell(new Phrase("1 ", cuerpo));
            cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell445);

            PdfPCell cell425 = new PdfPCell(new Phrase("A", cuerpo));
            cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell425);

            PdfPCell cell447 = new PdfPCell(new Phrase(" "+Apellidop1[0]+" "+Apellidom1[0] + " "+nombre1[0], cuerpo));
            cell447.Colspan = 2;
            cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell447);

            PdfPCell cell428 = new PdfPCell(new Phrase(" " + promedio1[0], cuerpo));
            cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell428);

            PdfPCell cell4325 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4325.Colspan = 5;//toma columnas
            cell4325.BorderWidth = 0;
            table.AddCell(cell4325);

            PdfPCell cell455 = new PdfPCell(new Phrase("2 ", cuerpo));
            cell455.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell455);

            PdfPCell cell415 = new PdfPCell(new Phrase("A", cuerpo));
            cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell415);

            PdfPCell cell417 = new PdfPCell(new Phrase(" " + Apellidop2[0] + " " + Apellidom2[0] + " " + nombre2[0], cuerpo));
            cell417.Colspan = 2;
            cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell417);

            PdfPCell cell468 = new PdfPCell(new Phrase(" " + promedio2[0], cuerpo));
            cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell468);

            PdfPCell cell04 = new PdfPCell(new Phrase(" ", cuerpo));
            cell04.Colspan = 5;//toma columnas
            cell04.BorderWidth = 0;
            table.AddCell(cell04);

            PdfPCell cell05 = new PdfPCell(new Phrase("3 ", cuerpo));
            cell05.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell05);

            PdfPCell cell06 = new PdfPCell(new Phrase("A", cuerpo));
            cell06.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell06);

            PdfPCell cell07 = new PdfPCell(new Phrase(" " + Apellidop3[0] + " " + Apellidom3[0] + " " + nombre3[0], cuerpo));
            cell07.Colspan = 2;
            cell07.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell07);

            PdfPCell cell08 = new PdfPCell(new Phrase(" " + promedio3[0], cuerpo));
            cell08.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell08);

            PdfPCell cell014 = new PdfPCell(new Phrase(" ", cuerpo));
            cell014.Colspan = 5;//toma columnas
            cell014.BorderWidth = 0;
            table.AddCell(cell014);

            PdfPCell cell015 = new PdfPCell(new Phrase("4 ", cuerpo));
            cell015.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell015);

            PdfPCell cell016 = new PdfPCell(new Phrase("A", cuerpo));
            cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell016);

            PdfPCell cell017 = new PdfPCell(new Phrase("  " + Apellidop4[0] + " " + Apellidom4[0] + " " + nombre4[0], cuerpo));
            cell017.Colspan = 2;
            cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell017);

            PdfPCell cell019 = new PdfPCell(new Phrase(" " + promedio4[0], cuerpo));
            cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell019);

            PdfPCell cell034 = new PdfPCell(new Phrase(" ", cuerpo));
            cell034.Colspan = 5;//toma columnas
            cell034.BorderWidth = 0;
            table.AddCell(cell034);

            PdfPCell cell035 = new PdfPCell(new Phrase("5 ", cuerpo));
            cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell035);

            PdfPCell cell036 = new PdfPCell(new Phrase("A", cuerpo));
            cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell036);

            PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop5[0] + " " + Apellidom5[0] + " " + nombre5[0], cuerpo));
            cell037.Colspan = 2;
            cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell037);

            PdfPCell cell038 = new PdfPCell(new Phrase(" " + promedio5[0], cuerpo));
            cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell038);

            PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
            cell044.Colspan = 5;//toma columnas
            cell044.BorderWidth = 0;
            table.AddCell(cell044);

            PdfPCell cell045 = new PdfPCell(new Phrase("6 ", cuerpo));
            cell045.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell045);

            PdfPCell cell046 = new PdfPCell(new Phrase("A", cuerpo));
            cell046.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell046);

            PdfPCell cell047 = new PdfPCell(new Phrase(" " + Apellidop6[0] + " " + Apellidom6[0] + " " + nombre6[0], cuerpo));
            cell047.Colspan = 2;
            cell047.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell047);

            PdfPCell cell049 = new PdfPCell(new Phrase(" " + promedio6[0], cuerpo));
            cell049.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell049);

            PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
            cell4331.Colspan = 5;//toma columnas
            cell4331.BorderWidth = 0;
            table.AddCell(cell4331);

            PdfPCell cell244 = new PdfPCell(new Phrase("PROMEDIO MÁS ALTO ", cuerpo));
            cell244.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell244);

            PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
            cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell242);

            PdfPCell cell241 = new PdfPCell(new Phrase(" " + Apellidop + " " + Apellidom + " " + nombre, cuerpo));
            cell241.Colspan = 2;
            cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell241);

            PdfPCell cell226 = new PdfPCell(new Phrase(" " + promedio[0], cuerpo));
            cell226.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell226);

            PdfPCell cell2211 = new PdfPCell(new Phrase("SUPERVISOR(A)\n\n\n\n________________________________________________", cuerpo));
            cell2211.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell2211.Colspan = 2;
            cell2211.BorderWidth = 0;
            cell2211.PaddingTop = 45f;
            table.AddCell(cell2211);

            PdfPCell cell2261 = new PdfPCell(new Phrase("DIRECTOR(A)\n\n\n\n________________________________________________", cuerpo));
            cell2261.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell2261.Colspan = 3;
            cell2261.BorderWidth = 0;
            cell2261.PaddingTop = 45f;
            table.AddCell(cell2261);

            //encabezado

            /* table.AddCell(" ");

             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             table.AddCell(" ");
             */

            doc.Add(table);

            doc.Close();
            writer.Close();

            MessageBox.Show("¡PDF creado!");
        }

        private void Estadisticas_Load(object sender, EventArgs e)
        {

        }

        private void Estadisticas_FormClosing(object sender, FormClosingEventArgs e)
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
