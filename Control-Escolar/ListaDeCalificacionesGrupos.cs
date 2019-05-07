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
using System.Diagnostics;

namespace Control_Escolar
{
    public partial class ListaDeCalificacionesGrupos : MaterialForm
    {
        MySqlCommand codigo = new MySqlCommand();
        MySqlConnection conectanos = new MySqlConnection();
        //MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela;pwd=digi3.0");
        MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela");
        conexion obj = new conexion();

        public ListaDeCalificacionesGrupos()
        {
            InitializeComponent();
            switch (sesion.pictureb1)
            {
                case "1": { sesion.Grado = 1; txtGrado.Text = Convert.ToString(sesion.Grado); } break;
                case "2": { sesion.Grado = 2; txtGrado.Text = sesion.Grado.ToString(); } break;
                case "3": { sesion.Grado = 3; txtGrado.Text = sesion.Grado.ToString(); } break;
                case "4": { sesion.Grado = 4; txtGrado.Text = sesion.Grado.ToString(); } break;
                case "5": { sesion.Grado = 5; txtGrado.Text = sesion.Grado.ToString(); } break;
                case "6": { sesion.Grado = 6; txtGrado.Text = sesion.Grado.ToString(); } break;
            }
        }

        //-------------------------------------Metodos----------------------------------------------
        //Cerrar sesion
        public static void ThreadProc()

        {
            Application.Run(new login());
        }
        //Volver al menu principal
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if(sesion.Grado == 1 || sesion.Grado == 2)
            {
                //-------------Ingresar los datos del alumno en pdf--------------------------------
                MySqlConnection conn;
                MySqlCommand com;

                string conexion = "server=localhost;uid=root;database=nerivela";
                string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                conn = new MySqlConnection(conexion);
                conn.Open();

                com = new MySqlCommand(query, conn);

                MySqlDataReader myreader = com.ExecuteReader();

                string[] Nombre = new string[25];
                string[] Apellidop = new string[25];
                string[] Apellidom = new string[25];
                string[] Curp = new string[25];
                string[] id = new string[25];

                for (int i = 0; i > 25; i++)
                {
                    Nombre[i] = " ";
                    Apellidop[i] = " ";
                    Apellidom[i] = " ";
                    Curp[i] = " ";
                    id[i] = " ";
                }

                int L = 0;
                while (myreader.Read())//Agrega calificaciones
                {
                    //string[] curp = new string[25];
                    Nombre[L] = Convert.ToString(myreader["nombre"]);
                    Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                    Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                    Curp[L] = Convert.ToString(myreader["CURP"]);
                    id[L] = Convert.ToString(myreader["idAlumno"]);
                    //Curp[L] = curp[L].Substring(0, 1);
                    L++;
                }

                conn.Close();
                //-----------------------------Ingresar calificaciones en pdf--------------------------------
                MySqlConnection conn2;
                MySqlCommand com2;

                string conexion2 = "server=localhost;uid=root;database=nerivela";
                string query2 = "SELECT  *  FROM  `calificaciones`";

                conn2 = new MySqlConnection(conexion2);
                conn2.Open();

                com2 = new MySqlCommand(query2, conn2);

                MySqlDataReader myreader2 = com2.ExecuteReader();

                string[] califsep = new string[25];
                string[] califoct = new string[25];
                string[] califnov = new string[25];
                string[] califdic = new string[25];
                string[] califene = new string[25];
                string[] califfeb = new string[25];
                string[] califmar = new string[25];
                string[] califabr = new string[25];
                string[] califmay = new string[25];
                string[] califjun = new string[25];

                //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                MySqlConnection conn1;
                MySqlCommand com1;

                string conexion1 = "server=localhost;uid=root;database=nerivela";
                string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                //------------------------------------------------------------------------------------------------------------
                // Creamos el documento con el tamaño de página tradicional
                Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\Lista-calificaciones.pdf", FileMode.Create));

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
                PdfPTable table = new PdfPTable(13);
                table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                logoGro.BorderWidth = 0;
                logoGro.ScaleAbsolute(110, 40);
                iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                logoEst.ScaleAbsolute(140, 40);


                // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f,  0.30f, 0.20f };

                // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                table.SetWidths(Celdas);


                PdfPCell cell390 = new PdfPCell(logoGro);
                cell390.Colspan = 4;//toma columnas
                cell390.BorderWidth = 0;
                //cell390.PaddingTop = 5f;
                //cell390.PaddingBottom = 5f;
                cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell390);

                PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 1", tituloprin));
                cell39.Colspan = 4;//toma columnas
                cell39.BorderWidth = 0;
                cell39.PaddingTop = 10f;
                cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell39);

                PdfPCell cell380 = new PdfPCell(logoEst);
                cell380.Colspan = 5;//toma columnas
                cell380.BorderWidth = 0;
                cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                 //cell380.PaddingTop = 5f;
                                                 //cell380.PaddingBottom = 5f;
                table.AddCell(cell380);

                PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                cell38.Colspan = 13;//toma columnas
                cell38.BorderWidth = 0;
                cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell38);

                PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                cell398.Colspan = 13;//toma columnas
                cell398.BorderWidth = 0;
                cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                 //cell398.PaddingTop = 5f;
                                                 //cell398.PaddingBottom = 5f;
                table.AddCell(cell398);

                PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                cell391.Colspan = 13;//toma columnas
                cell391.BorderWidth = 0;
                cell391.PaddingTop = 5f;
                cell391.PaddingBottom = 5f;
                cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell391);

                PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                cell40.Colspan = 13;//toma columnas
                cell40.BorderWidth = 0;
                cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                cell40.PaddingBottom = 3f;
                table.AddCell(cell40);

                //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                //cell4311.Colspan = 16;//toma columnas
                //cell4311.BorderWidth = 0;
                //table.AddCell(cell4311);

                PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell44);

                PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell42);

                PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell441);

                PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell421);

                PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell422);

                PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell445);

                PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell425);

                PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell447);

                PdfPCell cell428 = new PdfPCell(new Phrase("CONC. DEL MEDIO", cuerpo));
                cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell428);
                
                PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell416);

                PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell417);

                PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell468);

                PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell418);
                

                table.AddCell("1");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("2");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("3");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("4");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("5");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("6");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("7");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("8");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("9");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("10");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("11");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("12");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("13");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("14");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("15");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("16");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("17");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("18");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("19");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("20");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("21");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("22");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("23");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("24");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("25");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                
                PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                cell016.Colspan = 5;
                cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell016);


                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");


                PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                cell019.Colspan = 7;
                cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell019);

                PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                cell034.Colspan = 6;//toma columnas
                cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell034);

                PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                cell017.Colspan = 13;
                cell017.BorderWidth = 0;
                cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell017);

                PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                cell035.Colspan = 7;//toma columnas
                cell035.BorderWidth = 0;
                cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell035);

                PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                cell036.Colspan = 6;//toma columnas
                cell036.BorderWidth = 0;
                cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell036);

                PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                cell044.Colspan = 13;//toma columnas
                cell044.BorderWidth = 0;
                table.AddCell(cell044);
                

                PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                cell037.Colspan = 7;
                cell037.BorderWidth = 0;
                cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell037);

                PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                cell038.Colspan = 6;//toma columnas
                cell038.BorderWidth = 0;
                cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell038);


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
                Process.Start(@"c:\shashe\Lista-calificaciones.pdf");
            }
            else
            {
                if(sesion.Grado == 3)
                {
                    //-------------Ingresar los datos del alumno en pdf--------------------------------
                    MySqlConnection conn;
                    MySqlCommand com;

                    string conexion = "server=localhost;uid=root;database=nerivela";
                    string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                    conn = new MySqlConnection(conexion);
                    conn.Open();

                    com = new MySqlCommand(query, conn);

                    MySqlDataReader myreader = com.ExecuteReader();

                    string[] Nombre = new string[25];
                    string[] Apellidop = new string[25];
                    string[] Apellidom = new string[25];
                    string[] Curp = new string[25];
                    string[] id = new string[25];

                    for (int i = 0; i > 25; i++)
                    {
                        Nombre[i] = " ";
                        Apellidop[i] = " ";
                        Apellidom[i] = " ";
                        Curp[i] = " ";
                        id[i] = " ";
                    }

                    int L = 0;
                    while (myreader.Read())//Agrega calificaciones
                    {
                        //string[] curp = new string[25];
                        Nombre[L] = Convert.ToString(myreader["nombre"]);
                        Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                        Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                        Curp[L] = Convert.ToString(myreader["CURP"]);
                        id[L] = Convert.ToString(myreader["idAlumno"]);
                        //Curp[L] = curp[L].Substring(0, 1);
                        L++;
                    }

                    conn.Close();
                    //-----------------------------Ingresar calificaciones en pdf--------------------------------
                    MySqlConnection conn2;
                    MySqlCommand com2;

                    string conexion2 = "server=localhost;uid=root;database=nerivela";
                    string query2 = "SELECT  *  FROM  `calificaciones`";

                    conn2 = new MySqlConnection(conexion2);
                    conn2.Open();

                    com2 = new MySqlCommand(query2, conn2);

                    MySqlDataReader myreader2 = com2.ExecuteReader();

                    string[] califsep = new string[25];
                    string[] califoct = new string[25];
                    string[] califnov = new string[25];
                    string[] califdic = new string[25];
                    string[] califene = new string[25];
                    string[] califfeb = new string[25];
                    string[] califmar = new string[25];
                    string[] califabr = new string[25];
                    string[] califmay = new string[25];
                    string[] califjun = new string[25];

                    //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                    MySqlConnection conn1;
                    MySqlCommand com1;

                    string conexion1 = "server=localhost;uid=root;database=nerivela";
                    string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                    //------------------------------------------------------------------------------------------------------------
                    // Creamos el documento con el tamaño de página tradicional
                    Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                    // Indicamos donde vamos a guardar el documento
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                    PdfPTable table = new PdfPTable(15);
                    table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                    iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                    logoGro.BorderWidth = 0;
                    logoGro.ScaleAbsolute(110, 40);
                    iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                    logoEst.ScaleAbsolute(140, 40);


                    // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                    // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                    float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                    // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                    table.SetWidths(Celdas);


                    PdfPCell cell390 = new PdfPCell(logoGro);
                    cell390.Colspan = 4;//toma columnas
                    cell390.BorderWidth = 0;
                    //cell390.PaddingTop = 5f;
                    //cell390.PaddingBottom = 5f;
                    cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell390);

                    PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 1", tituloprin));
                    cell39.Colspan = 6;//toma columnas
                    cell39.BorderWidth = 0;
                    cell39.PaddingTop = 10f;
                    cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell39);

                    PdfPCell cell380 = new PdfPCell(logoEst);
                    cell380.Colspan = 5;//toma columnas
                    cell380.BorderWidth = 0;
                    cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                     //cell380.PaddingTop = 5f;
                                                     //cell380.PaddingBottom = 5f;
                    table.AddCell(cell380);

                    PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                    cell38.Colspan = 15;//toma columnas
                    cell38.BorderWidth = 0;
                    cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell38);

                    PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                    cell398.Colspan = 15;//toma columnas
                    cell398.BorderWidth = 0;
                    cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                     //cell398.PaddingTop = 5f;
                                                     //cell398.PaddingBottom = 5f;
                    table.AddCell(cell398);

                    PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                    cell391.Colspan = 15;//toma columnas
                    cell391.BorderWidth = 0;
                    cell391.PaddingTop = 5f;
                    cell391.PaddingBottom = 5f;
                    cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell391);

                    PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                    cell40.Colspan = 15;//toma columnas
                    cell40.BorderWidth = 0;
                    cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    cell40.PaddingBottom = 3f;
                    table.AddCell(cell40);

                    //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                    //cell4311.Colspan = 16;//toma columnas
                    //cell4311.BorderWidth = 0;
                    //table.AddCell(cell4311);

                    PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                    cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell44);

                    PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                    cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell42);

                    PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                    cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell441);

                    PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                    cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell421);

                    PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                    cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell422);

                    PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                    cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell445);

                    PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                    cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell425);

                    PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                    cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell447);

                    PdfPCell cell428 = new PdfPCell(new Phrase("C. NATURALES", cuerpo));
                    cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell428);

                    PdfPCell cell4325 = new PdfPCell(new Phrase("LA ENT.", cuerpo));
                    cell4325.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell4325);

                    PdfPCell cell415 = new PdfPCell(new Phrase("FORM. CÍV. Y ÉTICA", cuerpo));
                    cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell415);

                    PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                    cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell416);

                    PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                    cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell417);

                    PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                    cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell468);

                    PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                    cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell418);

                    

                    table.AddCell("1");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
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

                    table.AddCell("2");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
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

                    table.AddCell("3");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
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

                    table.AddCell("4");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
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

                    table.AddCell("5");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
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

                    table.AddCell("6");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
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

                    table.AddCell("7");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
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

                    table.AddCell("8");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
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

                    table.AddCell("9");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
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

                    table.AddCell("10");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
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

                    table.AddCell("11");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
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

                    table.AddCell("12");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
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

                    table.AddCell("13");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
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

                    table.AddCell("14");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
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

                    table.AddCell("15");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
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

                    table.AddCell("16");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
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

                    table.AddCell("17");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
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

                    table.AddCell("18");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
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

                    table.AddCell("19");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
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

                    table.AddCell("20");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
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

                    table.AddCell("21");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
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

                    table.AddCell("22");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
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

                    table.AddCell("23");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
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

                    table.AddCell("24");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
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

                    table.AddCell("25");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
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
                    

                    PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                    cell016.Colspan = 5;
                    cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell016);


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


                    PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                    cell019.Colspan = 8;
                    cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell019);

                    PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                    cell034.Colspan = 7;//toma columnas
                    cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell034);

                    PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell017.Colspan = 15;
                    cell017.BorderWidth = 0;
                    cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell017);

                    PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                    cell035.Colspan = 8;//toma columnas
                    cell035.BorderWidth = 0;
                    cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell035);

                    PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                    cell036.Colspan = 7;//toma columnas
                    cell036.BorderWidth = 0;
                    cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell036);

                    PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell044.Colspan = 15;//toma columnas
                    cell044.BorderWidth = 0;
                    table.AddCell(cell044);

                    PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell4331.Colspan = 15;//toma columnas
                    cell4331.BorderWidth = 0;
                    table.AddCell(cell4331);

                    PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                    cell037.Colspan = 8;
                    cell037.BorderWidth = 0;
                    cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell037);

                    PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                    cell038.Colspan = 7;//toma columnas
                    cell038.BorderWidth = 0;
                    cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell038);


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
                    Process.Start(@"c:\shashe\prueba5.pdf");
                }
                else
                {
                    if(sesion.Grado == 4 || sesion.Grado == 5 || sesion.Grado == 6){
                        //-------------Ingresar los datos del alumno en pdf--------------------------------
                        MySqlConnection conn;
                        MySqlCommand com;

                        string conexion = "server=localhost;uid=root;database=nerivela";
                        string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                        conn = new MySqlConnection(conexion);
                        conn.Open();

                        com = new MySqlCommand(query, conn);

                        MySqlDataReader myreader = com.ExecuteReader();

                        string[] Nombre = new string[25];
                        string[] Apellidop = new string[25];
                        string[] Apellidom = new string[25];
                        string[] Curp = new string[25];
                        string[] id = new string[25];

                        for (int i = 0; i > 25; i++)
                        {
                            Nombre[i] = " ";
                            Apellidop[i] = " ";
                            Apellidom[i] = " ";
                            Curp[i] = " ";
                            id[i] = " ";
                        }

                        int L = 0;
                        while (myreader.Read())//Agrega calificaciones
                        {
                            //string[] curp = new string[25];
                            Nombre[L] = Convert.ToString(myreader["nombre"]);
                            Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                            Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                            Curp[L] = Convert.ToString(myreader["CURP"]);
                            id[L] = Convert.ToString(myreader["idAlumno"]);
                            //Curp[L] = curp[L].Substring(0, 1);
                            L++;
                        }

                        conn.Close();
                        //-----------------------------Ingresar calificaciones en pdf--------------------------------
                        MySqlConnection conn2;
                        MySqlCommand com2;

                        string conexion2 = "server=localhost;uid=root;database=nerivela";
                        string query2 = "SELECT  *  FROM  `calificaciones`";

                        conn2 = new MySqlConnection(conexion2);
                        conn2.Open();

                        com2 = new MySqlCommand(query2, conn2);

                        MySqlDataReader myreader2 = com2.ExecuteReader();

                        string[] califsep = new string[25];
                        string[] califoct = new string[25];
                        string[] califnov = new string[25];
                        string[] califdic = new string[25];
                        string[] califene = new string[25];
                        string[] califfeb = new string[25];
                        string[] califmar = new string[25];
                        string[] califabr = new string[25];
                        string[] califmay = new string[25];
                        string[] califjun = new string[25];

                        //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                        MySqlConnection conn1;
                        MySqlCommand com1;

                        string conexion1 = "server=localhost;uid=root;database=nerivela";
                        string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                        //------------------------------------------------------------------------------------------------------------
                        // Creamos el documento con el tamaño de página tradicional
                        Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                        // Indicamos donde vamos a guardar el documento
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                        PdfPTable table = new PdfPTable(16);
                        table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                        iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                        logoGro.BorderWidth = 0;
                        logoGro.ScaleAbsolute(110, 40);
                        iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                        logoEst.ScaleAbsolute(140, 40);


                        // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                        // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                        float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                        // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                        table.SetWidths(Celdas);


                        PdfPCell cell390 = new PdfPCell(logoGro);
                        cell390.Colspan = 4;//toma columnas
                        cell390.BorderWidth = 0;
                        //cell390.PaddingTop = 5f;
                        //cell390.PaddingBottom = 5f;
                        cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell390);

                        PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 1", tituloprin));
                        cell39.Colspan = 7;//toma columnas
                        cell39.BorderWidth = 0;
                        cell39.PaddingTop = 10f;
                        cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell39);

                        PdfPCell cell380 = new PdfPCell(logoEst);
                        cell380.Colspan = 5;//toma columnas
                        cell380.BorderWidth = 0;
                        cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                         //cell380.PaddingTop = 5f;
                                                         //cell380.PaddingBottom = 5f;
                        table.AddCell(cell380);

                        PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                        cell38.Colspan = 16;//toma columnas
                        cell38.BorderWidth = 0;
                        cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell38);

                        PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                        cell398.Colspan = 16;//toma columnas
                        cell398.BorderWidth = 0;
                        cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                         //cell398.PaddingTop = 5f;
                                                         //cell398.PaddingBottom = 5f;
                        table.AddCell(cell398);

                        PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                        cell391.Colspan = 16;//toma columnas
                        cell391.BorderWidth = 0;
                        cell391.PaddingTop = 5f;
                        cell391.PaddingBottom = 5f;
                        cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell391);

                        PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                        cell40.Colspan = 16;//toma columnas
                        cell40.BorderWidth = 0;
                        cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        cell40.PaddingBottom = 3f;
                        table.AddCell(cell40);

                        //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                        //cell4311.Colspan = 16;//toma columnas
                        //cell4311.BorderWidth = 0;
                        //table.AddCell(cell4311);

                        PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                        cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell44);

                        PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                        cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell42);

                        PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                        cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell441);

                        PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                        cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell421);

                        PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                        cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell422);

                        PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                        cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell445);

                        PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                        cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell425);

                        PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                        cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell447);

                        PdfPCell cell428 = new PdfPCell(new Phrase("C. NATURALES", cuerpo));
                        cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell428);

                        PdfPCell cell4325 = new PdfPCell(new Phrase("GEOGRAFIA", cuerpo));
                        cell4325.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell4325);

                        PdfPCell cell455 = new PdfPCell(new Phrase("HISTORIA", cuerpo));
                        cell455.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell455);

                        PdfPCell cell415 = new PdfPCell(new Phrase("FORM. CÍV. Y ÉTICA", cuerpo));
                        cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell415);

                        PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                        cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell416);

                        PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                        cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell417);

                        PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                        cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell468);

                        PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                        cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell418);

                       
                        table.AddCell("1");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
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

                        table.AddCell("2");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
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

                        table.AddCell("3");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
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

                        table.AddCell("4");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
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

                        table.AddCell("5");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
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

                        table.AddCell("6");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
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

                        table.AddCell("7");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
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

                        table.AddCell("8");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
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

                        table.AddCell("9");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
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

                        table.AddCell("10");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
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

                        table.AddCell("11");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
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

                        table.AddCell("12");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
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

                        table.AddCell("13");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
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

                        table.AddCell("14");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
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

                        table.AddCell("15");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
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

                        table.AddCell("16");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
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

                        table.AddCell("17");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
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

                        table.AddCell("18");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
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

                        table.AddCell("19");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
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

                        table.AddCell("20");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
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

                        table.AddCell("21");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
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

                        table.AddCell("22");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
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

                        table.AddCell("23");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
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

                        table.AddCell("24");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
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

                        table.AddCell("25");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
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
                        
                        PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                        cell016.Colspan = 5;
                        cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell016);


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


                        PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                        cell019.Colspan = 8;
                        cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell019);

                        PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                        cell034.Colspan = 8;//toma columnas
                        cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell034);

                        PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell017.Colspan = 16;
                        cell017.BorderWidth = 0;
                        cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell017);

                        PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                        cell035.Colspan = 8;//toma columnas
                        cell035.BorderWidth = 0;
                        cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell035);

                        PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                        cell036.Colspan = 8;//toma columnas
                        cell036.BorderWidth = 0;
                        cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell036);

                        PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell044.Colspan = 16;//toma columnas
                        cell044.BorderWidth = 0;
                        table.AddCell(cell044);

                        PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell4331.Colspan = 16;//toma columnas
                        cell4331.BorderWidth = 0;
                        table.AddCell(cell4331);

                        PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                        cell037.Colspan = 8;
                        cell037.BorderWidth = 0;
                        cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell037);

                        PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                        cell038.Colspan = 8;//toma columnas
                        cell038.BorderWidth = 0;
                        cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell038);


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
                        Process.Start(@"c:\shashe\prueba5.pdf");
                    }
                }
            }

            
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
            if (sesion.Grado == 1 || sesion.Grado == 2)
            {
                //-------------Ingresar los datos del alumno en pdf--------------------------------
                MySqlConnection conn;
                MySqlCommand com;

                string conexion = "server=localhost;uid=root;database=nerivela";
                string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                conn = new MySqlConnection(conexion);
                conn.Open();

                com = new MySqlCommand(query, conn);

                MySqlDataReader myreader = com.ExecuteReader();

                string[] Nombre = new string[25];
                string[] Apellidop = new string[25];
                string[] Apellidom = new string[25];
                string[] Curp = new string[25];
                string[] id = new string[25];

                for (int i = 0; i > 25; i++)
                {
                    Nombre[i] = " ";
                    Apellidop[i] = " ";
                    Apellidom[i] = " ";
                    Curp[i] = " ";
                    id[i] = " ";
                }

                int L = 0;
                while (myreader.Read())//Agrega calificaciones
                {
                    //string[] curp = new string[25];
                    Nombre[L] = Convert.ToString(myreader["nombre"]);
                    Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                    Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                    Curp[L] = Convert.ToString(myreader["CURP"]);
                    id[L] = Convert.ToString(myreader["idAlumno"]);
                    //Curp[L] = curp[L].Substring(0, 1);
                    L++;
                }

                conn.Close();
                //-----------------------------Ingresar calificaciones en pdf--------------------------------
                MySqlConnection conn2;
                MySqlCommand com2;

                string conexion2 = "server=localhost;uid=root;database=nerivela";
                string query2 = "SELECT  *  FROM  `calificaciones`";

                conn2 = new MySqlConnection(conexion2);
                conn2.Open();

                com2 = new MySqlCommand(query2, conn2);

                MySqlDataReader myreader2 = com2.ExecuteReader();

                string[] califsep = new string[25];
                string[] califoct = new string[25];
                string[] califnov = new string[25];
                string[] califdic = new string[25];
                string[] califene = new string[25];
                string[] califfeb = new string[25];
                string[] califmar = new string[25];
                string[] califabr = new string[25];
                string[] califmay = new string[25];
                string[] califjun = new string[25];

                //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                MySqlConnection conn1;
                MySqlCommand com1;

                string conexion1 = "server=localhost;uid=root;database=nerivela";
                string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                //------------------------------------------------------------------------------------------------------------
                // Creamos el documento con el tamaño de página tradicional
                Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                PdfPTable table = new PdfPTable(13);
                table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                logoGro.BorderWidth = 0;
                logoGro.ScaleAbsolute(110, 40);
                iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                logoEst.ScaleAbsolute(140, 40);


                // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                table.SetWidths(Celdas);


                PdfPCell cell390 = new PdfPCell(logoGro);
                cell390.Colspan = 4;//toma columnas
                cell390.BorderWidth = 0;
                //cell390.PaddingTop = 5f;
                //cell390.PaddingBottom = 5f;
                cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell390);

                PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 2", tituloprin));
                cell39.Colspan = 4;//toma columnas
                cell39.BorderWidth = 0;
                cell39.PaddingTop = 10f;
                cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell39);

                PdfPCell cell380 = new PdfPCell(logoEst);
                cell380.Colspan = 5;//toma columnas
                cell380.BorderWidth = 0;
                cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                 //cell380.PaddingTop = 5f;
                                                 //cell380.PaddingBottom = 5f;
                table.AddCell(cell380);

                PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                cell38.Colspan = 13;//toma columnas
                cell38.BorderWidth = 0;
                cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell38);

                PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                cell398.Colspan = 13;//toma columnas
                cell398.BorderWidth = 0;
                cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                 //cell398.PaddingTop = 5f;
                                                 //cell398.PaddingBottom = 5f;
                table.AddCell(cell398);

                PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                cell391.Colspan = 13;//toma columnas
                cell391.BorderWidth = 0;
                cell391.PaddingTop = 5f;
                cell391.PaddingBottom = 5f;
                cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell391);

                PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                cell40.Colspan = 13;//toma columnas
                cell40.BorderWidth = 0;
                cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                cell40.PaddingBottom = 3f;
                table.AddCell(cell40);

                //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                //cell4311.Colspan = 16;//toma columnas
                //cell4311.BorderWidth = 0;
                //table.AddCell(cell4311);

                PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell44);

                PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell42);

                PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell441);

                PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell421);

                PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell422);

                PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell445);

                PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell425);

                PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell447);

                PdfPCell cell428 = new PdfPCell(new Phrase("CONC. DEL MEDIO", cuerpo));
                cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell428);

                PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell416);

                PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell417);

                PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell468);

                PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell418);


                table.AddCell("1");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("2");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("3");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("4");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("5");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("6");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("7");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("8");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("9");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("10");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("11");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("12");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("13");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("14");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("15");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("16");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("17");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("18");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("19");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("20");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("21");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("22");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("23");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("24");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("25");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                cell016.Colspan = 5;
                cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell016);


                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");


                PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                cell019.Colspan = 7;
                cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell019);

                PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                cell034.Colspan = 6;//toma columnas
                cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell034);

                PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                cell017.Colspan = 13;
                cell017.BorderWidth = 0;
                cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell017);

                PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                cell035.Colspan = 7;//toma columnas
                cell035.BorderWidth = 0;
                cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell035);

                PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                cell036.Colspan = 6;//toma columnas
                cell036.BorderWidth = 0;
                cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell036);

                PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                cell044.Colspan = 13;//toma columnas
                cell044.BorderWidth = 0;
                table.AddCell(cell044);


                PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                cell037.Colspan = 7;
                cell037.BorderWidth = 0;
                cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell037);

                PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                cell038.Colspan = 6;//toma columnas
                cell038.BorderWidth = 0;
                cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell038);


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
                Process.Start(@"c:\shashe\prueba5.pdf");
            }
            else
            {
                if (sesion.Grado == 3)
                {
                    //-------------Ingresar los datos del alumno en pdf--------------------------------
                    MySqlConnection conn;
                    MySqlCommand com;

                    string conexion = "server=localhost;uid=root;database=nerivela";
                    string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                    conn = new MySqlConnection(conexion);
                    conn.Open();

                    com = new MySqlCommand(query, conn);

                    MySqlDataReader myreader = com.ExecuteReader();

                    string[] Nombre = new string[25];
                    string[] Apellidop = new string[25];
                    string[] Apellidom = new string[25];
                    string[] Curp = new string[25];
                    string[] id = new string[25];

                    for (int i = 0; i > 25; i++)
                    {
                        Nombre[i] = " ";
                        Apellidop[i] = " ";
                        Apellidom[i] = " ";
                        Curp[i] = " ";
                        id[i] = " ";
                    }

                    int L = 0;
                    while (myreader.Read())//Agrega calificaciones
                    {
                        //string[] curp = new string[25];
                        Nombre[L] = Convert.ToString(myreader["nombre"]);
                        Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                        Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                        Curp[L] = Convert.ToString(myreader["CURP"]);
                        id[L] = Convert.ToString(myreader["idAlumno"]);
                        //Curp[L] = curp[L].Substring(0, 1);
                        L++;
                    }

                    conn.Close();
                    //-----------------------------Ingresar calificaciones en pdf--------------------------------
                    MySqlConnection conn2;
                    MySqlCommand com2;

                    string conexion2 = "server=localhost;uid=root;database=nerivela";
                    string query2 = "SELECT  *  FROM  `calificaciones`";

                    conn2 = new MySqlConnection(conexion2);
                    conn2.Open();

                    com2 = new MySqlCommand(query2, conn2);

                    MySqlDataReader myreader2 = com2.ExecuteReader();

                    //string[] califsep = new string[25];
                    //string[] califoct = new string[25];
                    //string[] califnov = new string[25];
                    //string[] califdic = new string[25];
                    //string[] califene = new string[25];
                    //string[] califfeb = new string[25];
                    //string[] califmar = new string[25];
                    //string[] califabr = new string[25];
                    //string[] califmay = new string[25];
                    //string[] califjun = new string[25];

                    //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                    MySqlConnection conn1;
                    MySqlCommand com1;

                    string conexion1 = "server=localhost;uid=root;database=nerivela";
                    string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                    //------------------------------------------------------------------------------------------------------------
                    // Creamos el documento con el tamaño de página tradicional
                    Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                    // Indicamos donde vamos a guardar el documento
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                    PdfPTable table = new PdfPTable(15);
                    table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                    iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                    logoGro.BorderWidth = 0;
                    logoGro.ScaleAbsolute(110, 40);
                    iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                    logoEst.ScaleAbsolute(140, 40);


                    // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                    // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                    float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                    // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                    table.SetWidths(Celdas);


                    PdfPCell cell390 = new PdfPCell(logoGro);
                    cell390.Colspan = 4;//toma columnas
                    cell390.BorderWidth = 0;
                    //cell390.PaddingTop = 5f;
                    //cell390.PaddingBottom = 5f;
                    cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell390);

                    PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 2", tituloprin));
                    cell39.Colspan = 6;//toma columnas
                    cell39.BorderWidth = 0;
                    cell39.PaddingTop = 10f;
                    cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell39);

                    PdfPCell cell380 = new PdfPCell(logoEst);
                    cell380.Colspan = 5;//toma columnas
                    cell380.BorderWidth = 0;
                    cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                     //cell380.PaddingTop = 5f;
                                                     //cell380.PaddingBottom = 5f;
                    table.AddCell(cell380);

                    PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                    cell38.Colspan = 15;//toma columnas
                    cell38.BorderWidth = 0;
                    cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell38);

                    PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                    cell398.Colspan = 15;//toma columnas
                    cell398.BorderWidth = 0;
                    cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                     //cell398.PaddingTop = 5f;
                                                     //cell398.PaddingBottom = 5f;
                    table.AddCell(cell398);

                    PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                    cell391.Colspan = 15;//toma columnas
                    cell391.BorderWidth = 0;
                    cell391.PaddingTop = 5f;
                    cell391.PaddingBottom = 5f;
                    cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell391);

                    PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                    cell40.Colspan = 15;//toma columnas
                    cell40.BorderWidth = 0;
                    cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    cell40.PaddingBottom = 3f;
                    table.AddCell(cell40);

                    //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                    //cell4311.Colspan = 16;//toma columnas
                    //cell4311.BorderWidth = 0;
                    //table.AddCell(cell4311);

                    PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                    cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell44);

                    PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                    cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell42);

                    PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                    cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell441);

                    PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                    cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell421);

                    PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                    cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell422);

                    PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                    cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell445);

                    PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                    cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell425);

                    PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                    cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell447);

                    PdfPCell cell428 = new PdfPCell(new Phrase("C. NATURALES", cuerpo));
                    cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell428);

                    PdfPCell cell4325 = new PdfPCell(new Phrase("LA ENT.", cuerpo));
                    cell4325.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell4325);

                    PdfPCell cell415 = new PdfPCell(new Phrase("FORM. CÍV. Y ÉTICA", cuerpo));
                    cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell415);

                    PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                    cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell416);

                    PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                    cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell417);

                    PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                    cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell468);

                    PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                    cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell418);



                    table.AddCell("1");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
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

                    table.AddCell("2");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
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

                    table.AddCell("3");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
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

                    table.AddCell("4");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
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

                    table.AddCell("5");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
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

                    table.AddCell("6");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
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

                    table.AddCell("7");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
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

                    table.AddCell("8");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
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

                    table.AddCell("9");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
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

                    table.AddCell("10");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
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

                    table.AddCell("11");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
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

                    table.AddCell("12");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
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

                    table.AddCell("13");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
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

                    table.AddCell("14");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
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

                    table.AddCell("15");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
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

                    table.AddCell("16");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
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

                    table.AddCell("17");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
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

                    table.AddCell("18");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
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

                    table.AddCell("19");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
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

                    table.AddCell("20");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
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

                    table.AddCell("21");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
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

                    table.AddCell("22");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
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

                    table.AddCell("23");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
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

                    table.AddCell("24");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
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

                    table.AddCell("25");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
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


                    PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                    cell016.Colspan = 5;
                    cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell016);


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


                    PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                    cell019.Colspan = 8;
                    cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell019);

                    PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                    cell034.Colspan = 7;//toma columnas
                    cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell034);

                    PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell017.Colspan = 15;
                    cell017.BorderWidth = 0;
                    cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell017);

                    PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                    cell035.Colspan = 8;//toma columnas
                    cell035.BorderWidth = 0;
                    cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell035);

                    PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                    cell036.Colspan = 7;//toma columnas
                    cell036.BorderWidth = 0;
                    cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell036);

                    PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell044.Colspan = 15;//toma columnas
                    cell044.BorderWidth = 0;
                    table.AddCell(cell044);

                    PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell4331.Colspan = 15;//toma columnas
                    cell4331.BorderWidth = 0;
                    table.AddCell(cell4331);

                    PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                    cell037.Colspan = 8;
                    cell037.BorderWidth = 0;
                    cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell037);

                    PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                    cell038.Colspan = 7;//toma columnas
                    cell038.BorderWidth = 0;
                    cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell038);


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
                    Process.Start(@"c:\shashe\prueba5.pdf");
                }
                else
                {
                    if (sesion.Grado == 4 || sesion.Grado == 5 || sesion.Grado == 6)
                    {
                        //-------------Ingresar los datos del alumno en pdf--------------------------------
                        MySqlConnection conn;
                        MySqlCommand com;

                        string conexion = "server=localhost;uid=root;database=nerivela";
                        string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                        conn = new MySqlConnection(conexion);
                        conn.Open();

                        com = new MySqlCommand(query, conn);

                        MySqlDataReader myreader = com.ExecuteReader();

                        string[] Nombre = new string[25];
                        string[] Apellidop = new string[25];
                        string[] Apellidom = new string[25];
                        string[] Curp = new string[25];
                        string[] id = new string[25];

                        for (int i = 0; i > 25; i++)
                        {
                            Nombre[i] = " ";
                            Apellidop[i] = " ";
                            Apellidom[i] = " ";
                            Curp[i] = " ";
                            id[i] = " ";
                        }

                        int L = 0;
                        while (myreader.Read())//Agrega calificaciones
                        {
                            //string[] curp = new string[25];
                            Nombre[L] = Convert.ToString(myreader["nombre"]);
                            Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                            Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                            Curp[L] = Convert.ToString(myreader["CURP"]);
                            id[L] = Convert.ToString(myreader["idAlumno"]);
                            //Curp[L] = curp[L].Substring(0, 1);
                            L++;
                        }

                        conn.Close();
                        //-----------------------------Ingresar calificaciones en pdf--------------------------------
                        MySqlConnection conn2;
                        MySqlCommand com2;

                        string conexion2 = "server=localhost;uid=root;database=nerivela";
                        string query2 = "SELECT  *  FROM  `calificaciones`";

                        conn2 = new MySqlConnection(conexion2);
                        conn2.Open();

                        com2 = new MySqlCommand(query2, conn2);

                        MySqlDataReader myreader2 = com2.ExecuteReader();

                        //string[] califsep = new string[25];
                        //string[] califoct = new string[25];
                        //string[] califnov = new string[25];
                        //string[] califdic = new string[25];
                        //string[] califene = new string[25];
                        //string[] califfeb = new string[25];
                        //string[] califmar = new string[25];
                        //string[] califabr = new string[25];
                        //string[] califmay = new string[25];
                        //string[] califjun = new string[25];

                        //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                        MySqlConnection conn1;
                        MySqlCommand com1;

                        string conexion1 = "server=localhost;uid=root;database=nerivela";
                        string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                        //------------------------------------------------------------------------------------------------------------
                        // Creamos el documento con el tamaño de página tradicional
                        Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                        // Indicamos donde vamos a guardar el documento
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                        PdfPTable table = new PdfPTable(16);
                        table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                        iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                        logoGro.BorderWidth = 0;
                        logoGro.ScaleAbsolute(110, 40);
                        iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                        logoEst.ScaleAbsolute(140, 40);


                        // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                        // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                        float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                        // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                        table.SetWidths(Celdas);


                        PdfPCell cell390 = new PdfPCell(logoGro);
                        cell390.Colspan = 4;//toma columnas
                        cell390.BorderWidth = 0;
                        //cell390.PaddingTop = 5f;
                        //cell390.PaddingBottom = 5f;
                        cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell390);

                        PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 2", tituloprin));
                        cell39.Colspan = 7;//toma columnas
                        cell39.BorderWidth = 0;
                        cell39.PaddingTop = 10f;
                        cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell39);

                        PdfPCell cell380 = new PdfPCell(logoEst);
                        cell380.Colspan = 5;//toma columnas
                        cell380.BorderWidth = 0;
                        cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                         //cell380.PaddingTop = 5f;
                                                         //cell380.PaddingBottom = 5f;
                        table.AddCell(cell380);

                        PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                        cell38.Colspan = 16;//toma columnas
                        cell38.BorderWidth = 0;
                        cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell38);

                        PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                        cell398.Colspan = 16;//toma columnas
                        cell398.BorderWidth = 0;
                        cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                         //cell398.PaddingTop = 5f;
                                                         //cell398.PaddingBottom = 5f;
                        table.AddCell(cell398);

                        PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                        cell391.Colspan = 16;//toma columnas
                        cell391.BorderWidth = 0;
                        cell391.PaddingTop = 5f;
                        cell391.PaddingBottom = 5f;
                        cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell391);

                        PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                        cell40.Colspan = 16;//toma columnas
                        cell40.BorderWidth = 0;
                        cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        cell40.PaddingBottom = 3f;
                        table.AddCell(cell40);

                        //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                        //cell4311.Colspan = 16;//toma columnas
                        //cell4311.BorderWidth = 0;
                        //table.AddCell(cell4311);

                        PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                        cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell44);

                        PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                        cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell42);

                        PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                        cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell441);

                        PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                        cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell421);

                        PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                        cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell422);

                        PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                        cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell445);

                        PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                        cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell425);

                        PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                        cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell447);

                        PdfPCell cell428 = new PdfPCell(new Phrase("C. NATURALES", cuerpo));
                        cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell428);

                        PdfPCell cell4325 = new PdfPCell(new Phrase("GEOGRAFIA", cuerpo));
                        cell4325.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell4325);

                        PdfPCell cell455 = new PdfPCell(new Phrase("HISTORIA", cuerpo));
                        cell455.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell455);

                        PdfPCell cell415 = new PdfPCell(new Phrase("FORM. CÍV. Y ÉTICA", cuerpo));
                        cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell415);

                        PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                        cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell416);

                        PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                        cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell417);

                        PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                        cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell468);

                        PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                        cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell418);


                        table.AddCell("1");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
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

                        table.AddCell("2");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
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

                        table.AddCell("3");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
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

                        table.AddCell("4");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
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

                        table.AddCell("5");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
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

                        table.AddCell("6");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
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

                        table.AddCell("7");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
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

                        table.AddCell("8");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
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

                        table.AddCell("9");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
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

                        table.AddCell("10");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
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

                        table.AddCell("11");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
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

                        table.AddCell("12");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
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

                        table.AddCell("13");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
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

                        table.AddCell("14");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
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

                        table.AddCell("15");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
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

                        table.AddCell("16");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
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

                        table.AddCell("17");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
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

                        table.AddCell("18");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
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

                        table.AddCell("19");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
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

                        table.AddCell("20");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
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

                        table.AddCell("21");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
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

                        table.AddCell("22");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
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

                        table.AddCell("23");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
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

                        table.AddCell("24");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
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

                        table.AddCell("25");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
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

                        PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                        cell016.Colspan = 5;
                        cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell016);


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


                        PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                        cell019.Colspan = 8;
                        cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell019);

                        PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                        cell034.Colspan = 8;//toma columnas
                        cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell034);

                        PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell017.Colspan = 16;
                        cell017.BorderWidth = 0;
                        cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell017);

                        PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                        cell035.Colspan = 8;//toma columnas
                        cell035.BorderWidth = 0;
                        cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell035);

                        PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                        cell036.Colspan = 8;//toma columnas
                        cell036.BorderWidth = 0;
                        cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell036);

                        PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell044.Colspan = 16;//toma columnas
                        cell044.BorderWidth = 0;
                        table.AddCell(cell044);

                        PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell4331.Colspan = 16;//toma columnas
                        cell4331.BorderWidth = 0;
                        table.AddCell(cell4331);

                        PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                        cell037.Colspan = 8;
                        cell037.BorderWidth = 0;
                        cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell037);

                        PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                        cell038.Colspan = 8;//toma columnas
                        cell038.BorderWidth = 0;
                        cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell038);


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
                        Process.Start(@"c:\shashe\prueba5.pdf");
                    }
                }
            }

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (sesion.Grado == 1 || sesion.Grado == 2)
            {
                //-------------Ingresar los datos del alumno en pdf--------------------------------
                MySqlConnection conn;
                MySqlCommand com;

                string conexion = "server=localhost;uid=root;database=nerivela";
                string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                conn = new MySqlConnection(conexion);
                conn.Open();

                com = new MySqlCommand(query, conn);

                MySqlDataReader myreader = com.ExecuteReader();

                string[] Nombre = new string[25];
                string[] Apellidop = new string[25];
                string[] Apellidom = new string[25];
                string[] Curp = new string[25];
                string[] id = new string[25];

                for (int i = 0; i > 25; i++)
                {
                    Nombre[i] = " ";
                    Apellidop[i] = " ";
                    Apellidom[i] = " ";
                    Curp[i] = " ";
                    id[i] = " ";
                }

                int L = 0;
                while (myreader.Read())//Agrega calificaciones
                {
                    //string[] curp = new string[25];
                    Nombre[L] = Convert.ToString(myreader["nombre"]);
                    Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                    Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                    Curp[L] = Convert.ToString(myreader["CURP"]);
                    id[L] = Convert.ToString(myreader["idAlumno"]);
                    //Curp[L] = curp[L].Substring(0, 1);
                    L++;
                }

                conn.Close();
                //-----------------------------Ingresar calificaciones en pdf--------------------------------
                MySqlConnection conn2;
                MySqlCommand com2;

                string conexion2 = "server=localhost;uid=root;database=nerivela";
                string query2 = "SELECT  *  FROM  `calificaciones`";

                conn2 = new MySqlConnection(conexion2);
                conn2.Open();

                com2 = new MySqlCommand(query2, conn2);

                MySqlDataReader myreader2 = com2.ExecuteReader();

                string[] califsep = new string[25];
                string[] califoct = new string[25];
                string[] califnov = new string[25];
                string[] califdic = new string[25];
                string[] califene = new string[25];
                string[] califfeb = new string[25];
                string[] califmar = new string[25];
                string[] califabr = new string[25];
                string[] califmay = new string[25];
                string[] califjun = new string[25];

                //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                MySqlConnection conn1;
                MySqlCommand com1;

                string conexion1 = "server=localhost;uid=root;database=nerivela";
                string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                //------------------------------------------------------------------------------------------------------------
                // Creamos el documento con el tamaño de página tradicional
                Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                PdfPTable table = new PdfPTable(13);
                table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                logoGro.BorderWidth = 0;
                logoGro.ScaleAbsolute(110, 40);
                iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                logoEst.ScaleAbsolute(140, 40);


                // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                table.SetWidths(Celdas);


                PdfPCell cell390 = new PdfPCell(logoGro);
                cell390.Colspan = 4;//toma columnas
                cell390.BorderWidth = 0;
                //cell390.PaddingTop = 5f;
                //cell390.PaddingBottom = 5f;
                cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell390);

                PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 3", tituloprin));
                cell39.Colspan = 4;//toma columnas
                cell39.BorderWidth = 0;
                cell39.PaddingTop = 10f;
                cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell39);

                PdfPCell cell380 = new PdfPCell(logoEst);
                cell380.Colspan = 5;//toma columnas
                cell380.BorderWidth = 0;
                cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                 //cell380.PaddingTop = 5f;
                                                 //cell380.PaddingBottom = 5f;
                table.AddCell(cell380);

                PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                cell38.Colspan = 13;//toma columnas
                cell38.BorderWidth = 0;
                cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell38);

                PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                cell398.Colspan = 13;//toma columnas
                cell398.BorderWidth = 0;
                cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                 //cell398.PaddingTop = 5f;
                                                 //cell398.PaddingBottom = 5f;
                table.AddCell(cell398);

                PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                cell391.Colspan = 13;//toma columnas
                cell391.BorderWidth = 0;
                cell391.PaddingTop = 5f;
                cell391.PaddingBottom = 5f;
                cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell391);

                PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                cell40.Colspan = 13;//toma columnas
                cell40.BorderWidth = 0;
                cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                cell40.PaddingBottom = 3f;
                table.AddCell(cell40);

                //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                //cell4311.Colspan = 16;//toma columnas
                //cell4311.BorderWidth = 0;
                //table.AddCell(cell4311);

                PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell44);

                PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell42);

                PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell441);

                PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell421);

                PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell422);

                PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell445);

                PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell425);

                PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell447);

                PdfPCell cell428 = new PdfPCell(new Phrase("CONC. DEL MEDIO", cuerpo));
                cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell428);

                PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell416);

                PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell417);

                PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell468);

                PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell418);


                table.AddCell("1");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("2");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("3");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("4");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("5");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("6");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("7");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("8");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("9");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("10");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("11");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("12");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("13");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("14");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("15");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("16");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("17");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("18");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("19");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("20");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("21");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("22");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("23");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("24");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("25");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                cell016.Colspan = 5;
                cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell016);


                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");


                PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                cell019.Colspan = 7;
                cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell019);

                PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                cell034.Colspan = 6;//toma columnas
                cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell034);

                PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                cell017.Colspan = 13;
                cell017.BorderWidth = 0;
                cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell017);

                PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                cell035.Colspan = 7;//toma columnas
                cell035.BorderWidth = 0;
                cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell035);

                PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                cell036.Colspan = 6;//toma columnas
                cell036.BorderWidth = 0;
                cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell036);

                PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                cell044.Colspan = 13;//toma columnas
                cell044.BorderWidth = 0;
                table.AddCell(cell044);


                PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                cell037.Colspan = 7;
                cell037.BorderWidth = 0;
                cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell037);

                PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                cell038.Colspan = 6;//toma columnas
                cell038.BorderWidth = 0;
                cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell038);


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
                Process.Start(@"c:\shashe\prueba5.pdf");
            }
            else
            {
                if (sesion.Grado == 3)
                {
                    //-------------Ingresar los datos del alumno en pdf--------------------------------
                    MySqlConnection conn;
                    MySqlCommand com;

                    string conexion = "server=localhost;uid=root;database=nerivela";
                    string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                    conn = new MySqlConnection(conexion);
                    conn.Open();

                    com = new MySqlCommand(query, conn);

                    MySqlDataReader myreader = com.ExecuteReader();

                    string[] Nombre = new string[25];
                    string[] Apellidop = new string[25];
                    string[] Apellidom = new string[25];
                    string[] Curp = new string[25];
                    string[] id = new string[25];

                    for (int i = 0; i > 25; i++)
                    {
                        Nombre[i] = " ";
                        Apellidop[i] = " ";
                        Apellidom[i] = " ";
                        Curp[i] = " ";
                        id[i] = " ";
                    }

                    int L = 0;
                    while (myreader.Read())//Agrega calificaciones
                    {
                        //string[] curp = new string[25];
                        Nombre[L] = Convert.ToString(myreader["nombre"]);
                        Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                        Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                        Curp[L] = Convert.ToString(myreader["CURP"]);
                        id[L] = Convert.ToString(myreader["idAlumno"]);
                        //Curp[L] = curp[L].Substring(0, 1);
                        L++;
                    }

                    conn.Close();
                    //-----------------------------Ingresar calificaciones en pdf--------------------------------
                    MySqlConnection conn2;
                    MySqlCommand com2;

                    string conexion2 = "server=localhost;uid=root;database=nerivela";
                    string query2 = "SELECT  *  FROM  `calificaciones`";

                    conn2 = new MySqlConnection(conexion2);
                    conn2.Open();

                    com2 = new MySqlCommand(query2, conn2);

                    MySqlDataReader myreader2 = com2.ExecuteReader();

                    //string[] califsep = new string[25];
                    //string[] califoct = new string[25];
                    //string[] califnov = new string[25];
                    //string[] califdic = new string[25];
                    //string[] califene = new string[25];
                    //string[] califfeb = new string[25];
                    //string[] califmar = new string[25];
                    //string[] califabr = new string[25];
                    //string[] califmay = new string[25];
                    //string[] califjun = new string[25];

                    //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                    MySqlConnection conn1;
                    MySqlCommand com1;

                    string conexion1 = "server=localhost;uid=root;database=nerivela";
                    string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                    //------------------------------------------------------------------------------------------------------------
                    // Creamos el documento con el tamaño de página tradicional
                    Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                    // Indicamos donde vamos a guardar el documento
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                    PdfPTable table = new PdfPTable(15);
                    table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                    iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                    logoGro.BorderWidth = 0;
                    logoGro.ScaleAbsolute(110, 40);
                    iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                    logoEst.ScaleAbsolute(140, 40);


                    // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                    // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                    float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                    // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                    table.SetWidths(Celdas);


                    PdfPCell cell390 = new PdfPCell(logoGro);
                    cell390.Colspan = 4;//toma columnas
                    cell390.BorderWidth = 0;
                    //cell390.PaddingTop = 5f;
                    //cell390.PaddingBottom = 5f;
                    cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell390);

                    PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 3", tituloprin));
                    cell39.Colspan = 6;//toma columnas
                    cell39.BorderWidth = 0;
                    cell39.PaddingTop = 10f;
                    cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell39);

                    PdfPCell cell380 = new PdfPCell(logoEst);
                    cell380.Colspan = 5;//toma columnas
                    cell380.BorderWidth = 0;
                    cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                     //cell380.PaddingTop = 5f;
                                                     //cell380.PaddingBottom = 5f;
                    table.AddCell(cell380);

                    PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                    cell38.Colspan = 15;//toma columnas
                    cell38.BorderWidth = 0;
                    cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell38);

                    PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                    cell398.Colspan = 15;//toma columnas
                    cell398.BorderWidth = 0;
                    cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                     //cell398.PaddingTop = 5f;
                                                     //cell398.PaddingBottom = 5f;
                    table.AddCell(cell398);

                    PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                    cell391.Colspan = 15;//toma columnas
                    cell391.BorderWidth = 0;
                    cell391.PaddingTop = 5f;
                    cell391.PaddingBottom = 5f;
                    cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell391);

                    PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                    cell40.Colspan = 15;//toma columnas
                    cell40.BorderWidth = 0;
                    cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    cell40.PaddingBottom = 3f;
                    table.AddCell(cell40);

                    //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                    //cell4311.Colspan = 16;//toma columnas
                    //cell4311.BorderWidth = 0;
                    //table.AddCell(cell4311);

                    PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                    cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell44);

                    PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                    cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell42);

                    PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                    cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell441);

                    PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                    cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell421);

                    PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                    cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell422);

                    PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                    cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell445);

                    PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                    cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell425);

                    PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                    cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell447);

                    PdfPCell cell428 = new PdfPCell(new Phrase("C. NATURALES", cuerpo));
                    cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell428);

                    PdfPCell cell4325 = new PdfPCell(new Phrase("LA ENT.", cuerpo));
                    cell4325.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell4325);

                    PdfPCell cell415 = new PdfPCell(new Phrase("FORM. CÍV. Y ÉTICA", cuerpo));
                    cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell415);

                    PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                    cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell416);

                    PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                    cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell417);

                    PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                    cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell468);

                    PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                    cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell418);



                    table.AddCell("1");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
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

                    table.AddCell("2");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
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

                    table.AddCell("3");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
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

                    table.AddCell("4");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
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

                    table.AddCell("5");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
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

                    table.AddCell("6");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
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

                    table.AddCell("7");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
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

                    table.AddCell("8");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
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

                    table.AddCell("9");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
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

                    table.AddCell("10");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
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

                    table.AddCell("11");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
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

                    table.AddCell("12");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
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

                    table.AddCell("13");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
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

                    table.AddCell("14");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
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

                    table.AddCell("15");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
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

                    table.AddCell("16");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
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

                    table.AddCell("17");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
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

                    table.AddCell("18");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
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

                    table.AddCell("19");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
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

                    table.AddCell("20");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
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

                    table.AddCell("21");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
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

                    table.AddCell("22");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
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

                    table.AddCell("23");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
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

                    table.AddCell("24");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
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

                    table.AddCell("25");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
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


                    PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                    cell016.Colspan = 5;
                    cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell016);


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


                    PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                    cell019.Colspan = 8;
                    cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell019);

                    PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                    cell034.Colspan = 7;//toma columnas
                    cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell034);

                    PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell017.Colspan = 15;
                    cell017.BorderWidth = 0;
                    cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell017);

                    PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                    cell035.Colspan = 8;//toma columnas
                    cell035.BorderWidth = 0;
                    cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell035);

                    PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                    cell036.Colspan = 7;//toma columnas
                    cell036.BorderWidth = 0;
                    cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell036);

                    PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell044.Colspan = 15;//toma columnas
                    cell044.BorderWidth = 0;
                    table.AddCell(cell044);

                    PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell4331.Colspan = 15;//toma columnas
                    cell4331.BorderWidth = 0;
                    table.AddCell(cell4331);

                    PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                    cell037.Colspan = 8;
                    cell037.BorderWidth = 0;
                    cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell037);

                    PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                    cell038.Colspan = 7;//toma columnas
                    cell038.BorderWidth = 0;
                    cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell038);


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
                    Process.Start(@"c:\shashe\prueba5.pdf");
                }
                else
                {
                    if (sesion.Grado == 4 || sesion.Grado == 5 || sesion.Grado == 6)
                    {
                        //-------------Ingresar los datos del alumno en pdf--------------------------------
                        MySqlConnection conn;
                        MySqlCommand com;

                        string conexion = "server=localhost;uid=root;database=nerivela";
                        string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                        conn = new MySqlConnection(conexion);
                        conn.Open();

                        com = new MySqlCommand(query, conn);

                        MySqlDataReader myreader = com.ExecuteReader();

                        string[] Nombre = new string[25];
                        string[] Apellidop = new string[25];
                        string[] Apellidom = new string[25];
                        string[] Curp = new string[25];
                        string[] id = new string[25];

                        for (int i = 0; i > 25; i++)
                        {
                            Nombre[i] = " ";
                            Apellidop[i] = " ";
                            Apellidom[i] = " ";
                            Curp[i] = " ";
                            id[i] = " ";
                        }

                        int L = 0;
                        while (myreader.Read())//Agrega calificaciones
                        {
                            //string[] curp = new string[25];
                            Nombre[L] = Convert.ToString(myreader["nombre"]);
                            Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                            Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                            Curp[L] = Convert.ToString(myreader["CURP"]);
                            id[L] = Convert.ToString(myreader["idAlumno"]);
                            //Curp[L] = curp[L].Substring(0, 1);
                            L++;
                        }

                        conn.Close();
                        //-----------------------------Ingresar calificaciones en pdf--------------------------------
                        MySqlConnection conn2;
                        MySqlCommand com2;

                        string conexion2 = "server=localhost;uid=root;database=nerivela";
                        string query2 = "SELECT  *  FROM  `calificaciones`";

                        conn2 = new MySqlConnection(conexion2);
                        conn2.Open();

                        com2 = new MySqlCommand(query2, conn2);

                        MySqlDataReader myreader2 = com2.ExecuteReader();

                        string[] califsep = new string[25];
                        string[] califoct = new string[25];
                        string[] califnov = new string[25];
                        string[] califdic = new string[25];
                        string[] califene = new string[25];
                        string[] califfeb = new string[25];
                        string[] califmar = new string[25];
                        string[] califabr = new string[25];
                        string[] califmay = new string[25];
                        string[] califjun = new string[25];

                        //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                        MySqlConnection conn1;
                        MySqlCommand com1;

                        string conexion1 = "server=localhost;uid=root;database=nerivela";
                        string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                        //------------------------------------------------------------------------------------------------------------
                        // Creamos el documento con el tamaño de página tradicional
                        Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                        // Indicamos donde vamos a guardar el documento
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                        PdfPTable table = new PdfPTable(16);
                        table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                        iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                        logoGro.BorderWidth = 0;
                        logoGro.ScaleAbsolute(110, 40);
                        iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                        logoEst.ScaleAbsolute(140, 40);


                        // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                        // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                        float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                        // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                        table.SetWidths(Celdas);


                        PdfPCell cell390 = new PdfPCell(logoGro);
                        cell390.Colspan = 4;//toma columnas
                        cell390.BorderWidth = 0;
                        //cell390.PaddingTop = 5f;
                        //cell390.PaddingBottom = 5f;
                        cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell390);

                        PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 3", tituloprin));
                        cell39.Colspan = 7;//toma columnas
                        cell39.BorderWidth = 0;
                        cell39.PaddingTop = 10f;
                        cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell39);

                        PdfPCell cell380 = new PdfPCell(logoEst);
                        cell380.Colspan = 5;//toma columnas
                        cell380.BorderWidth = 0;
                        cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                         //cell380.PaddingTop = 5f;
                                                         //cell380.PaddingBottom = 5f;
                        table.AddCell(cell380);

                        PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                        cell38.Colspan = 16;//toma columnas
                        cell38.BorderWidth = 0;
                        cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell38);

                        PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                        cell398.Colspan = 16;//toma columnas
                        cell398.BorderWidth = 0;
                        cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                         //cell398.PaddingTop = 5f;
                                                         //cell398.PaddingBottom = 5f;
                        table.AddCell(cell398);

                        PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                        cell391.Colspan = 16;//toma columnas
                        cell391.BorderWidth = 0;
                        cell391.PaddingTop = 5f;
                        cell391.PaddingBottom = 5f;
                        cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell391);

                        PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                        cell40.Colspan = 16;//toma columnas
                        cell40.BorderWidth = 0;
                        cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        cell40.PaddingBottom = 3f;
                        table.AddCell(cell40);

                        //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                        //cell4311.Colspan = 16;//toma columnas
                        //cell4311.BorderWidth = 0;
                        //table.AddCell(cell4311);

                        PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                        cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell44);

                        PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                        cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell42);

                        PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                        cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell441);

                        PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                        cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell421);

                        PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                        cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell422);

                        PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                        cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell445);

                        PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                        cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell425);

                        PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                        cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell447);

                        PdfPCell cell428 = new PdfPCell(new Phrase("C. NATURALES", cuerpo));
                        cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell428);

                        PdfPCell cell4325 = new PdfPCell(new Phrase("GEOGRAFIA", cuerpo));
                        cell4325.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell4325);

                        PdfPCell cell455 = new PdfPCell(new Phrase("HISTORIA", cuerpo));
                        cell455.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell455);

                        PdfPCell cell415 = new PdfPCell(new Phrase("FORM. CÍV. Y ÉTICA", cuerpo));
                        cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell415);

                        PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                        cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell416);

                        PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                        cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell417);

                        PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                        cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell468);

                        PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                        cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell418);


                        table.AddCell("1");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
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

                        table.AddCell("2");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
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

                        table.AddCell("3");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
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

                        table.AddCell("4");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
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

                        table.AddCell("5");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
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

                        table.AddCell("6");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
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

                        table.AddCell("7");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
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

                        table.AddCell("8");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
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

                        table.AddCell("9");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
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

                        table.AddCell("10");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
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

                        table.AddCell("11");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
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

                        table.AddCell("12");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
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

                        table.AddCell("13");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
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

                        table.AddCell("14");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
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

                        table.AddCell("15");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
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

                        table.AddCell("16");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
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

                        table.AddCell("17");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
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

                        table.AddCell("18");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
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

                        table.AddCell("19");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
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

                        table.AddCell("20");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
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

                        table.AddCell("21");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
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

                        table.AddCell("22");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
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

                        table.AddCell("23");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
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

                        table.AddCell("24");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
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

                        table.AddCell("25");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
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

                        PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                        cell016.Colspan = 5;
                        cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell016);


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


                        PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                        cell019.Colspan = 8;
                        cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell019);

                        PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                        cell034.Colspan = 8;//toma columnas
                        cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell034);

                        PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell017.Colspan = 16;
                        cell017.BorderWidth = 0;
                        cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell017);

                        PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                        cell035.Colspan = 8;//toma columnas
                        cell035.BorderWidth = 0;
                        cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell035);

                        PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                        cell036.Colspan = 8;//toma columnas
                        cell036.BorderWidth = 0;
                        cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell036);

                        PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell044.Colspan = 16;//toma columnas
                        cell044.BorderWidth = 0;
                        table.AddCell(cell044);

                        PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell4331.Colspan = 16;//toma columnas
                        cell4331.BorderWidth = 0;
                        table.AddCell(cell4331);

                        PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                        cell037.Colspan = 8;
                        cell037.BorderWidth = 0;
                        cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell037);

                        PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                        cell038.Colspan = 8;//toma columnas
                        cell038.BorderWidth = 0;
                        cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell038);


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
                        Process.Start(@"c:\shashe\prueba5.pdf");
                    }
                }
            }

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            if (sesion.Grado == 1 || sesion.Grado == 2)
            {
                //-------------Ingresar los datos del alumno en pdf--------------------------------
                MySqlConnection conn;
                MySqlCommand com;

                string conexion = "server=localhost;uid=root;database=nerivela";
                string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                conn = new MySqlConnection(conexion);
                conn.Open();

                com = new MySqlCommand(query, conn);

                MySqlDataReader myreader = com.ExecuteReader();

                string[] Nombre = new string[25];
                string[] Apellidop = new string[25];
                string[] Apellidom = new string[25];
                string[] Curp = new string[25];
                string[] id = new string[25];

                for (int i = 0; i > 25; i++)
                {
                    Nombre[i] = " ";
                    Apellidop[i] = " ";
                    Apellidom[i] = " ";
                    Curp[i] = " ";
                    id[i] = " ";
                }

                int L = 0;
                while (myreader.Read())//Agrega calificaciones
                {
                    //string[] curp = new string[25];
                    Nombre[L] = Convert.ToString(myreader["nombre"]);
                    Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                    Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                    Curp[L] = Convert.ToString(myreader["CURP"]);
                    id[L] = Convert.ToString(myreader["idAlumno"]);
                    //Curp[L] = curp[L].Substring(0, 1);
                    L++;
                }

                conn.Close();
                //-----------------------------Ingresar calificaciones en pdf--------------------------------
                MySqlConnection conn2;
                MySqlCommand com2;

                string conexion2 = "server=localhost;uid=root;database=nerivela";
                string query2 = "SELECT  *  FROM  `calificaciones`";

                conn2 = new MySqlConnection(conexion2);
                conn2.Open();

                com2 = new MySqlCommand(query2, conn2);

                MySqlDataReader myreader2 = com2.ExecuteReader();

                string[] califsep = new string[25];
                string[] califoct = new string[25];
                string[] califnov = new string[25];
                string[] califdic = new string[25];
                string[] califene = new string[25];
                string[] califfeb = new string[25];
                string[] califmar = new string[25];
                string[] califabr = new string[25];
                string[] califmay = new string[25];
                string[] califjun = new string[25];

                //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                MySqlConnection conn1;
                MySqlCommand com1;

                string conexion1 = "server=localhost;uid=root;database=nerivela";
                string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                //------------------------------------------------------------------------------------------------------------
                // Creamos el documento con el tamaño de página tradicional
                Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                PdfPTable table = new PdfPTable(13);
                table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                logoGro.BorderWidth = 0;
                logoGro.ScaleAbsolute(110, 40);
                iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                logoEst.ScaleAbsolute(140, 40);


                // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                table.SetWidths(Celdas);


                PdfPCell cell390 = new PdfPCell(logoGro);
                cell390.Colspan = 4;//toma columnas
                cell390.BorderWidth = 0;
                //cell390.PaddingTop = 5f;
                //cell390.PaddingBottom = 5f;
                cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell390);

                PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 4", tituloprin));
                cell39.Colspan = 4;//toma columnas
                cell39.BorderWidth = 0;
                cell39.PaddingTop = 10f;
                cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell39);

                PdfPCell cell380 = new PdfPCell(logoEst);
                cell380.Colspan = 5;//toma columnas
                cell380.BorderWidth = 0;
                cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                 //cell380.PaddingTop = 5f;
                                                 //cell380.PaddingBottom = 5f;
                table.AddCell(cell380);

                PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                cell38.Colspan = 13;//toma columnas
                cell38.BorderWidth = 0;
                cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell38);

                PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                cell398.Colspan = 13;//toma columnas
                cell398.BorderWidth = 0;
                cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                 //cell398.PaddingTop = 5f;
                                                 //cell398.PaddingBottom = 5f;
                table.AddCell(cell398);

                PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                cell391.Colspan = 13;//toma columnas
                cell391.BorderWidth = 0;
                cell391.PaddingTop = 5f;
                cell391.PaddingBottom = 5f;
                cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell391);

                PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                cell40.Colspan = 13;//toma columnas
                cell40.BorderWidth = 0;
                cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                cell40.PaddingBottom = 3f;
                table.AddCell(cell40);

                //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                //cell4311.Colspan = 16;//toma columnas
                //cell4311.BorderWidth = 0;
                //table.AddCell(cell4311);

                PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell44);

                PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell42);

                PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell441);

                PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell421);

                PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell422);

                PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell445);

                PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell425);

                PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell447);

                PdfPCell cell428 = new PdfPCell(new Phrase("CONC. DEL MEDIO", cuerpo));
                cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell428);

                PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell416);

                PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell417);

                PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell468);

                PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell418);


                table.AddCell("1");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("2");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("3");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("4");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("5");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("6");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("7");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("8");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("9");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("10");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("11");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("12");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("13");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("14");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("15");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("16");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("17");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("18");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("19");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("20");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("21");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("22");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("23");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("24");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("25");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                cell016.Colspan = 5;
                cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell016);


                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");


                PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                cell019.Colspan = 7;
                cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell019);

                PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                cell034.Colspan = 6;//toma columnas
                cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell034);

                PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                cell017.Colspan = 13;
                cell017.BorderWidth = 0;
                cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell017);

                PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                cell035.Colspan = 7;//toma columnas
                cell035.BorderWidth = 0;
                cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell035);

                PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                cell036.Colspan = 6;//toma columnas
                cell036.BorderWidth = 0;
                cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell036);

                PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                cell044.Colspan = 13;//toma columnas
                cell044.BorderWidth = 0;
                table.AddCell(cell044);


                PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                cell037.Colspan = 7;
                cell037.BorderWidth = 0;
                cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell037);

                PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                cell038.Colspan = 6;//toma columnas
                cell038.BorderWidth = 0;
                cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell038);


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
                Process.Start(@"c:\shashe\prueba5.pdf");
            }
            else
            {
                if (sesion.Grado == 3)
                {
                    //-------------Ingresar los datos del alumno en pdf--------------------------------
                    MySqlConnection conn;
                    MySqlCommand com;

                    string conexion = "server=localhost;uid=root;database=nerivela";
                    string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                    conn = new MySqlConnection(conexion);
                    conn.Open();

                    com = new MySqlCommand(query, conn);

                    MySqlDataReader myreader = com.ExecuteReader();

                    string[] Nombre = new string[25];
                    string[] Apellidop = new string[25];
                    string[] Apellidom = new string[25];
                    string[] Curp = new string[25];
                    string[] id = new string[25];

                    for (int i = 0; i > 25; i++)
                    {
                        Nombre[i] = " ";
                        Apellidop[i] = " ";
                        Apellidom[i] = " ";
                        Curp[i] = " ";
                        id[i] = " ";
                    }

                    int L = 0;
                    while (myreader.Read())//Agrega calificaciones
                    {
                        //string[] curp = new string[25];
                        Nombre[L] = Convert.ToString(myreader["nombre"]);
                        Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                        Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                        Curp[L] = Convert.ToString(myreader["CURP"]);
                        id[L] = Convert.ToString(myreader["idAlumno"]);
                        //Curp[L] = curp[L].Substring(0, 1);
                        L++;
                    }

                    conn.Close();
                    //-----------------------------Ingresar calificaciones en pdf--------------------------------
                    MySqlConnection conn2;
                    MySqlCommand com2;

                    string conexion2 = "server=localhost;uid=root;database=nerivela";
                    string query2 = "SELECT  *  FROM  `calificaciones`";

                    conn2 = new MySqlConnection(conexion2);
                    conn2.Open();

                    com2 = new MySqlCommand(query2, conn2);

                    MySqlDataReader myreader2 = com2.ExecuteReader();

                    string[] califsep = new string[25];
                    string[] califoct = new string[25];
                    string[] califnov = new string[25];
                    string[] califdic = new string[25];
                    string[] califene = new string[25];
                    string[] califfeb = new string[25];
                    string[] califmar = new string[25];
                    string[] califabr = new string[25];
                    string[] califmay = new string[25];
                    string[] califjun = new string[25];

                    //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                    MySqlConnection conn1;
                    MySqlCommand com1;

                    string conexion1 = "server=localhost;uid=root;database=nerivela";
                    string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                    //------------------------------------------------------------------------------------------------------------
                    // Creamos el documento con el tamaño de página tradicional
                    Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                    // Indicamos donde vamos a guardar el documento
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                    PdfPTable table = new PdfPTable(15);
                    table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                    iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                    logoGro.BorderWidth = 0;
                    logoGro.ScaleAbsolute(110, 40);
                    iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                    logoEst.ScaleAbsolute(140, 40);


                    // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                    // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                    float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                    // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                    table.SetWidths(Celdas);


                    PdfPCell cell390 = new PdfPCell(logoGro);
                    cell390.Colspan = 4;//toma columnas
                    cell390.BorderWidth = 0;
                    //cell390.PaddingTop = 5f;
                    //cell390.PaddingBottom = 5f;
                    cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell390);

                    PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 4", tituloprin));
                    cell39.Colspan = 6;//toma columnas
                    cell39.BorderWidth = 0;
                    cell39.PaddingTop = 10f;
                    cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell39);

                    PdfPCell cell380 = new PdfPCell(logoEst);
                    cell380.Colspan = 5;//toma columnas
                    cell380.BorderWidth = 0;
                    cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                     //cell380.PaddingTop = 5f;
                                                     //cell380.PaddingBottom = 5f;
                    table.AddCell(cell380);

                    PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                    cell38.Colspan = 15;//toma columnas
                    cell38.BorderWidth = 0;
                    cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell38);

                    PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                    cell398.Colspan = 15;//toma columnas
                    cell398.BorderWidth = 0;
                    cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                     //cell398.PaddingTop = 5f;
                                                     //cell398.PaddingBottom = 5f;
                    table.AddCell(cell398);

                    PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                    cell391.Colspan = 15;//toma columnas
                    cell391.BorderWidth = 0;
                    cell391.PaddingTop = 5f;
                    cell391.PaddingBottom = 5f;
                    cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell391);

                    PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                    cell40.Colspan = 15;//toma columnas
                    cell40.BorderWidth = 0;
                    cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    cell40.PaddingBottom = 3f;
                    table.AddCell(cell40);

                    //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                    //cell4311.Colspan = 16;//toma columnas
                    //cell4311.BorderWidth = 0;
                    //table.AddCell(cell4311);

                    PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                    cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell44);

                    PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                    cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell42);

                    PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                    cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell441);

                    PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                    cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell421);

                    PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                    cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell422);

                    PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                    cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell445);

                    PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                    cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell425);

                    PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                    cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell447);

                    PdfPCell cell428 = new PdfPCell(new Phrase("C. NATURALES", cuerpo));
                    cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell428);

                    PdfPCell cell4325 = new PdfPCell(new Phrase("LA ENT.", cuerpo));
                    cell4325.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell4325);

                    PdfPCell cell415 = new PdfPCell(new Phrase("FORM. CÍV. Y ÉTICA", cuerpo));
                    cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell415);

                    PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                    cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell416);

                    PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                    cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell417);

                    PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                    cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell468);

                    PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                    cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell418);



                    table.AddCell("1");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
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

                    table.AddCell("2");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
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

                    table.AddCell("3");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
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

                    table.AddCell("4");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
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

                    table.AddCell("5");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
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

                    table.AddCell("6");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
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

                    table.AddCell("7");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
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

                    table.AddCell("8");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
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

                    table.AddCell("9");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
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

                    table.AddCell("10");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
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

                    table.AddCell("11");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
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

                    table.AddCell("12");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
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

                    table.AddCell("13");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
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

                    table.AddCell("14");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
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

                    table.AddCell("15");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
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

                    table.AddCell("16");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
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

                    table.AddCell("17");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
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

                    table.AddCell("18");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
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

                    table.AddCell("19");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
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

                    table.AddCell("20");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
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

                    table.AddCell("21");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
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

                    table.AddCell("22");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
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

                    table.AddCell("23");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
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

                    table.AddCell("24");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
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

                    table.AddCell("25");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
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


                    PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                    cell016.Colspan = 5;
                    cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell016);


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


                    PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                    cell019.Colspan = 8;
                    cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell019);

                    PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                    cell034.Colspan = 7;//toma columnas
                    cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell034);

                    PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell017.Colspan = 15;
                    cell017.BorderWidth = 0;
                    cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell017);

                    PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                    cell035.Colspan = 8;//toma columnas
                    cell035.BorderWidth = 0;
                    cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell035);

                    PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                    cell036.Colspan = 7;//toma columnas
                    cell036.BorderWidth = 0;
                    cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell036);

                    PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell044.Colspan = 15;//toma columnas
                    cell044.BorderWidth = 0;
                    table.AddCell(cell044);

                    PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell4331.Colspan = 15;//toma columnas
                    cell4331.BorderWidth = 0;
                    table.AddCell(cell4331);

                    PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                    cell037.Colspan = 8;
                    cell037.BorderWidth = 0;
                    cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell037);

                    PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                    cell038.Colspan = 7;//toma columnas
                    cell038.BorderWidth = 0;
                    cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell038);


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
                    Process.Start(@"c:\shashe\prueba5.pdf");
                }
                else
                {
                    if (sesion.Grado == 4 || sesion.Grado == 5 || sesion.Grado == 6)
                    {
                        //-------------Ingresar los datos del alumno en pdf--------------------------------
                        MySqlConnection conn;
                        MySqlCommand com;

                        string conexion = "server=localhost;uid=root;database=nerivela";
                        string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                        conn = new MySqlConnection(conexion);
                        conn.Open();

                        com = new MySqlCommand(query, conn);

                        MySqlDataReader myreader = com.ExecuteReader();

                        string[] Nombre = new string[25];
                        string[] Apellidop = new string[25];
                        string[] Apellidom = new string[25];
                        string[] Curp = new string[25];
                        string[] id = new string[25];

                        for (int i = 0; i > 25; i++)
                        {
                            Nombre[i] = " ";
                            Apellidop[i] = " ";
                            Apellidom[i] = " ";
                            Curp[i] = " ";
                            id[i] = " ";
                        }

                        int L = 0;
                        while (myreader.Read())//Agrega calificaciones
                        {
                            //string[] curp = new string[25];
                            Nombre[L] = Convert.ToString(myreader["nombre"]);
                            Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                            Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                            Curp[L] = Convert.ToString(myreader["CURP"]);
                            id[L] = Convert.ToString(myreader["idAlumno"]);
                            //Curp[L] = curp[L].Substring(0, 1);
                            L++;
                        }

                        conn.Close();
                        //-----------------------------Ingresar calificaciones en pdf--------------------------------
                        MySqlConnection conn2;
                        MySqlCommand com2;

                        string conexion2 = "server=localhost;uid=root;database=nerivela";
                        string query2 = "SELECT  *  FROM  `calificaciones`";

                        conn2 = new MySqlConnection(conexion2);
                        conn2.Open();

                        com2 = new MySqlCommand(query2, conn2);

                        MySqlDataReader myreader2 = com2.ExecuteReader();

                        string[] califsep = new string[25];
                        string[] califoct = new string[25];
                        string[] califnov = new string[25];
                        string[] califdic = new string[25];
                        string[] califene = new string[25];
                        string[] califfeb = new string[25];
                        string[] califmar = new string[25];
                        string[] califabr = new string[25];
                        string[] califmay = new string[25];
                        string[] califjun = new string[25];

                        //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                        MySqlConnection conn1;
                        MySqlCommand com1;

                        string conexion1 = "server=localhost;uid=root;database=nerivela";
                        string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                        //------------------------------------------------------------------------------------------------------------
                        // Creamos el documento con el tamaño de página tradicional
                        Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                        // Indicamos donde vamos a guardar el documento
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                        PdfPTable table = new PdfPTable(16);
                        table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                        iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                        logoGro.BorderWidth = 0;
                        logoGro.ScaleAbsolute(110, 40);
                        iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                        logoEst.ScaleAbsolute(140, 40);


                        // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                        // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                        float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                        // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                        table.SetWidths(Celdas);


                        PdfPCell cell390 = new PdfPCell(logoGro);
                        cell390.Colspan = 4;//toma columnas
                        cell390.BorderWidth = 0;
                        //cell390.PaddingTop = 5f;
                        //cell390.PaddingBottom = 5f;
                        cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell390);

                        PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 4", tituloprin));
                        cell39.Colspan = 7;//toma columnas
                        cell39.BorderWidth = 0;
                        cell39.PaddingTop = 10f;
                        cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell39);

                        PdfPCell cell380 = new PdfPCell(logoEst);
                        cell380.Colspan = 5;//toma columnas
                        cell380.BorderWidth = 0;
                        cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                         //cell380.PaddingTop = 5f;
                                                         //cell380.PaddingBottom = 5f;
                        table.AddCell(cell380);

                        PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                        cell38.Colspan = 16;//toma columnas
                        cell38.BorderWidth = 0;
                        cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell38);

                        PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                        cell398.Colspan = 16;//toma columnas
                        cell398.BorderWidth = 0;
                        cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                         //cell398.PaddingTop = 5f;
                                                         //cell398.PaddingBottom = 5f;
                        table.AddCell(cell398);

                        PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                        cell391.Colspan = 16;//toma columnas
                        cell391.BorderWidth = 0;
                        cell391.PaddingTop = 5f;
                        cell391.PaddingBottom = 5f;
                        cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell391);

                        PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                        cell40.Colspan = 16;//toma columnas
                        cell40.BorderWidth = 0;
                        cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        cell40.PaddingBottom = 3f;
                        table.AddCell(cell40);

                        //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                        //cell4311.Colspan = 16;//toma columnas
                        //cell4311.BorderWidth = 0;
                        //table.AddCell(cell4311);

                        PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                        cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell44);

                        PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                        cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell42);

                        PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                        cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell441);

                        PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                        cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell421);

                        PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                        cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell422);

                        PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                        cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell445);

                        PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                        cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell425);

                        PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                        cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell447);

                        PdfPCell cell428 = new PdfPCell(new Phrase("C. NATURALES", cuerpo));
                        cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell428);

                        PdfPCell cell4325 = new PdfPCell(new Phrase("GEOGRAFIA", cuerpo));
                        cell4325.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell4325);

                        PdfPCell cell455 = new PdfPCell(new Phrase("HISTORIA", cuerpo));
                        cell455.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell455);

                        PdfPCell cell415 = new PdfPCell(new Phrase("FORM. CÍV. Y ÉTICA", cuerpo));
                        cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell415);

                        PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                        cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell416);

                        PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                        cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell417);

                        PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                        cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell468);

                        PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                        cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell418);


                        table.AddCell("1");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
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

                        table.AddCell("2");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
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

                        table.AddCell("3");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
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

                        table.AddCell("4");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
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

                        table.AddCell("5");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
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

                        table.AddCell("6");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
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

                        table.AddCell("7");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
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

                        table.AddCell("8");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
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

                        table.AddCell("9");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
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

                        table.AddCell("10");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
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

                        table.AddCell("11");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
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

                        table.AddCell("12");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
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

                        table.AddCell("13");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
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

                        table.AddCell("14");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
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

                        table.AddCell("15");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
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

                        table.AddCell("16");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
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

                        table.AddCell("17");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
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

                        table.AddCell("18");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
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

                        table.AddCell("19");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
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

                        table.AddCell("20");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
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

                        table.AddCell("21");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
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

                        table.AddCell("22");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
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

                        table.AddCell("23");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
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

                        table.AddCell("24");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
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

                        table.AddCell("25");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
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

                        PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                        cell016.Colspan = 5;
                        cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell016);


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


                        PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                        cell019.Colspan = 8;
                        cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell019);

                        PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                        cell034.Colspan = 8;//toma columnas
                        cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell034);

                        PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell017.Colspan = 16;
                        cell017.BorderWidth = 0;
                        cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell017);

                        PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                        cell035.Colspan = 8;//toma columnas
                        cell035.BorderWidth = 0;
                        cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell035);

                        PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                        cell036.Colspan = 8;//toma columnas
                        cell036.BorderWidth = 0;
                        cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell036);

                        PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell044.Colspan = 16;//toma columnas
                        cell044.BorderWidth = 0;
                        table.AddCell(cell044);

                        PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell4331.Colspan = 16;//toma columnas
                        cell4331.BorderWidth = 0;
                        table.AddCell(cell4331);

                        PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                        cell037.Colspan = 8;
                        cell037.BorderWidth = 0;
                        cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell037);

                        PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                        cell038.Colspan = 8;//toma columnas
                        cell038.BorderWidth = 0;
                        cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell038);


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
                        Process.Start(@"c:\shashe\prueba5.pdf");
                    }
                }
            }

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (sesion.Grado == 1 || sesion.Grado == 2)
            {
                //-------------Ingresar los datos del alumno en pdf--------------------------------
                MySqlConnection conn;
                MySqlCommand com;

                string conexion = "server=localhost;uid=root;database=nerivela";
                string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                conn = new MySqlConnection(conexion);
                conn.Open();

                com = new MySqlCommand(query, conn);

                MySqlDataReader myreader = com.ExecuteReader();

                string[] Nombre = new string[25];
                string[] Apellidop = new string[25];
                string[] Apellidom = new string[25];
                string[] Curp = new string[25];
                string[] id = new string[25];

                for (int i = 0; i > 25; i++)
                {
                    Nombre[i] = " ";
                    Apellidop[i] = " ";
                    Apellidom[i] = " ";
                    Curp[i] = " ";
                    id[i] = " ";
                }

                int L = 0;
                while (myreader.Read())//Agrega calificaciones
                {
                    //string[] curp = new string[25];
                    Nombre[L] = Convert.ToString(myreader["nombre"]);
                    Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                    Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                    Curp[L] = Convert.ToString(myreader["CURP"]);
                    id[L] = Convert.ToString(myreader["idAlumno"]);
                    //Curp[L] = curp[L].Substring(0, 1);
                    L++;
                }

                conn.Close();
                //-----------------------------Ingresar calificaciones en pdf--------------------------------
                MySqlConnection conn2;
                MySqlCommand com2;

                string conexion2 = "server=localhost;uid=root;database=nerivela";
                string query2 = "SELECT  *  FROM  `calificaciones`";

                conn2 = new MySqlConnection(conexion2);
                conn2.Open();

                com2 = new MySqlCommand(query2, conn2);

                MySqlDataReader myreader2 = com2.ExecuteReader();

                string[] califsep = new string[25];
                string[] califoct = new string[25];
                string[] califnov = new string[25];
                string[] califdic = new string[25];
                string[] califene = new string[25];
                string[] califfeb = new string[25];
                string[] califmar = new string[25];
                string[] califabr = new string[25];
                string[] califmay = new string[25];
                string[] califjun = new string[25];

                //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                MySqlConnection conn1;
                MySqlCommand com1;

                string conexion1 = "server=localhost;uid=root;database=nerivela";
                string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                //------------------------------------------------------------------------------------------------------------
                // Creamos el documento con el tamaño de página tradicional
                Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                PdfPTable table = new PdfPTable(13);
                table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                logoGro.BorderWidth = 0;
                logoGro.ScaleAbsolute(110, 40);
                iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                logoEst.ScaleAbsolute(140, 40);


                // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                table.SetWidths(Celdas);


                PdfPCell cell390 = new PdfPCell(logoGro);
                cell390.Colspan = 4;//toma columnas
                cell390.BorderWidth = 0;
                //cell390.PaddingTop = 5f;
                //cell390.PaddingBottom = 5f;
                cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell390);

                PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 5", tituloprin));
                cell39.Colspan = 4;//toma columnas
                cell39.BorderWidth = 0;
                cell39.PaddingTop = 10f;
                cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell39);

                PdfPCell cell380 = new PdfPCell(logoEst);
                cell380.Colspan = 5;//toma columnas
                cell380.BorderWidth = 0;
                cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                 //cell380.PaddingTop = 5f;
                                                 //cell380.PaddingBottom = 5f;
                table.AddCell(cell380);

                PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                cell38.Colspan = 13;//toma columnas
                cell38.BorderWidth = 0;
                cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell38);

                PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                cell398.Colspan = 13;//toma columnas
                cell398.BorderWidth = 0;
                cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                 //cell398.PaddingTop = 5f;
                                                 //cell398.PaddingBottom = 5f;
                table.AddCell(cell398);

                PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                cell391.Colspan = 13;//toma columnas
                cell391.BorderWidth = 0;
                cell391.PaddingTop = 5f;
                cell391.PaddingBottom = 5f;
                cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell391);

                PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                cell40.Colspan = 13;//toma columnas
                cell40.BorderWidth = 0;
                cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                cell40.PaddingBottom = 3f;
                table.AddCell(cell40);

                //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                //cell4311.Colspan = 16;//toma columnas
                //cell4311.BorderWidth = 0;
                //table.AddCell(cell4311);

                PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell44);

                PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell42);

                PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell441);

                PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell421);

                PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell422);

                PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell445);

                PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell425);

                PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell447);

                PdfPCell cell428 = new PdfPCell(new Phrase("CONC. DEL MEDIO", cuerpo));
                cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell428);

                PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell416);

                PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell417);

                PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell468);

                PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell418);


                table.AddCell("1");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("2");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("3");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("4");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("5");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("6");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("7");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("8");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("9");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("10");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("11");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("12");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("13");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("14");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("15");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("16");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("17");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("18");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("19");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("20");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("21");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("22");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("23");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("24");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell("25");
                table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                cell016.Colspan = 5;
                cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell016);


                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");


                PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                cell019.Colspan = 7;
                cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell019);

                PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                cell034.Colspan = 6;//toma columnas
                cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell034);

                PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                cell017.Colspan = 13;
                cell017.BorderWidth = 0;
                cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell017);

                PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                cell035.Colspan = 7;//toma columnas
                cell035.BorderWidth = 0;
                cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell035);

                PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                cell036.Colspan = 6;//toma columnas
                cell036.BorderWidth = 0;
                cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell036);

                PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                cell044.Colspan = 13;//toma columnas
                cell044.BorderWidth = 0;
                table.AddCell(cell044);


                PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                cell037.Colspan = 7;
                cell037.BorderWidth = 0;
                cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell037);

                PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                cell038.Colspan = 6;//toma columnas
                cell038.BorderWidth = 0;
                cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell038);


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
                Process.Start(@"c:\shashe\prueba5.pdf");
            }
            else
            {
                if (sesion.Grado == 3)
                {
                    //-------------Ingresar los datos del alumno en pdf--------------------------------
                    MySqlConnection conn;
                    MySqlCommand com;

                    string conexion = "server=localhost;uid=root;database=nerivela";
                    string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                    conn = new MySqlConnection(conexion);
                    conn.Open();

                    com = new MySqlCommand(query, conn);

                    MySqlDataReader myreader = com.ExecuteReader();

                    string[] Nombre = new string[25];
                    string[] Apellidop = new string[25];
                    string[] Apellidom = new string[25];
                    string[] Curp = new string[25];
                    string[] id = new string[25];

                    for (int i = 0; i > 25; i++)
                    {
                        Nombre[i] = " ";
                        Apellidop[i] = " ";
                        Apellidom[i] = " ";
                        Curp[i] = " ";
                        id[i] = " ";
                    }

                    int L = 0;
                    while (myreader.Read())//Agrega calificaciones
                    {
                        //string[] curp = new string[25];
                        Nombre[L] = Convert.ToString(myreader["nombre"]);
                        Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                        Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                        Curp[L] = Convert.ToString(myreader["CURP"]);
                        id[L] = Convert.ToString(myreader["idAlumno"]);
                        //Curp[L] = curp[L].Substring(0, 1);
                        L++;
                    }

                    conn.Close();
                    //-----------------------------Ingresar calificaciones en pdf--------------------------------
                    MySqlConnection conn2;
                    MySqlCommand com2;

                    string conexion2 = "server=localhost;uid=root;database=nerivela";
                    string query2 = "SELECT  *  FROM  `calificaciones`";

                    conn2 = new MySqlConnection(conexion2);
                    conn2.Open();

                    com2 = new MySqlCommand(query2, conn2);

                    MySqlDataReader myreader2 = com2.ExecuteReader();

                    string[] califsep = new string[25];
                    string[] califoct = new string[25];
                    string[] califnov = new string[25];
                    string[] califdic = new string[25];
                    string[] califene = new string[25];
                    string[] califfeb = new string[25];
                    string[] califmar = new string[25];
                    string[] califabr = new string[25];
                    string[] califmay = new string[25];
                    string[] califjun = new string[25];

                    //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                    MySqlConnection conn1;
                    MySqlCommand com1;

                    string conexion1 = "server=localhost;uid=root;database=nerivela";
                    string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                    //------------------------------------------------------------------------------------------------------------
                    // Creamos el documento con el tamaño de página tradicional
                    Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                    // Indicamos donde vamos a guardar el documento
                    PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                    PdfPTable table = new PdfPTable(15);
                    table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                    iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                    logoGro.BorderWidth = 0;
                    logoGro.ScaleAbsolute(110, 40);
                    iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                    logoEst.ScaleAbsolute(140, 40);


                    // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                    // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                    float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                    // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                    table.SetWidths(Celdas);


                    PdfPCell cell390 = new PdfPCell(logoGro);
                    cell390.Colspan = 4;//toma columnas
                    cell390.BorderWidth = 0;
                    //cell390.PaddingTop = 5f;
                    //cell390.PaddingBottom = 5f;
                    cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell390);

                    PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 5", tituloprin));
                    cell39.Colspan = 6;//toma columnas
                    cell39.BorderWidth = 0;
                    cell39.PaddingTop = 10f;
                    cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell39);

                    PdfPCell cell380 = new PdfPCell(logoEst);
                    cell380.Colspan = 5;//toma columnas
                    cell380.BorderWidth = 0;
                    cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                     //cell380.PaddingTop = 5f;
                                                     //cell380.PaddingBottom = 5f;
                    table.AddCell(cell380);

                    PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                    cell38.Colspan = 15;//toma columnas
                    cell38.BorderWidth = 0;
                    cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell38);

                    PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                    cell398.Colspan = 15;//toma columnas
                    cell398.BorderWidth = 0;
                    cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                     //cell398.PaddingTop = 5f;
                                                     //cell398.PaddingBottom = 5f;
                    table.AddCell(cell398);

                    PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                    cell391.Colspan = 15;//toma columnas
                    cell391.BorderWidth = 0;
                    cell391.PaddingTop = 5f;
                    cell391.PaddingBottom = 5f;
                    cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell391);

                    PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                    cell40.Colspan = 15;//toma columnas
                    cell40.BorderWidth = 0;
                    cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    cell40.PaddingBottom = 3f;
                    table.AddCell(cell40);

                    //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                    //cell4311.Colspan = 16;//toma columnas
                    //cell4311.BorderWidth = 0;
                    //table.AddCell(cell4311);

                    PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                    cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell44);

                    PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                    cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell42);

                    PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                    cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell441);

                    PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                    cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell421);

                    PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                    cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell422);

                    PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                    cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell445);

                    PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                    cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell425);

                    PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                    cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell447);

                    PdfPCell cell428 = new PdfPCell(new Phrase("C. NATURALES", cuerpo));
                    cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell428);

                    PdfPCell cell4325 = new PdfPCell(new Phrase("LA ENT.", cuerpo));
                    cell4325.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell4325);

                    PdfPCell cell415 = new PdfPCell(new Phrase("FORM. CÍV. Y ÉTICA", cuerpo));
                    cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell415);

                    PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                    cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell416);

                    PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                    cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell417);

                    PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                    cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell468);

                    PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                    cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell418);



                    table.AddCell("1");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
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

                    table.AddCell("2");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
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

                    table.AddCell("3");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
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

                    table.AddCell("4");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
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

                    table.AddCell("5");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
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

                    table.AddCell("6");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
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

                    table.AddCell("7");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
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

                    table.AddCell("8");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
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

                    table.AddCell("9");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
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

                    table.AddCell("10");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
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

                    table.AddCell("11");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
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

                    table.AddCell("12");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
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

                    table.AddCell("13");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
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

                    table.AddCell("14");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
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

                    table.AddCell("15");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
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

                    table.AddCell("16");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
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

                    table.AddCell("17");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
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

                    table.AddCell("18");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
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

                    table.AddCell("19");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
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

                    table.AddCell("20");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
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

                    table.AddCell("21");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
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

                    table.AddCell("22");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
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

                    table.AddCell("23");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
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

                    table.AddCell("24");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
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

                    table.AddCell("25");
                    table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                    table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
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


                    PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                    cell016.Colspan = 5;
                    cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell016);


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


                    PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                    cell019.Colspan = 8;
                    cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell019);

                    PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                    cell034.Colspan = 7;//toma columnas
                    cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell034);

                    PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell017.Colspan = 15;
                    cell017.BorderWidth = 0;
                    cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell017);

                    PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                    cell035.Colspan = 8;//toma columnas
                    cell035.BorderWidth = 0;
                    cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell035);

                    PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                    cell036.Colspan = 7;//toma columnas
                    cell036.BorderWidth = 0;
                    cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell036);

                    PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell044.Colspan = 15;//toma columnas
                    cell044.BorderWidth = 0;
                    table.AddCell(cell044);

                    PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell4331.Colspan = 15;//toma columnas
                    cell4331.BorderWidth = 0;
                    table.AddCell(cell4331);

                    PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                    cell037.Colspan = 8;
                    cell037.BorderWidth = 0;
                    cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell037);

                    PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                    cell038.Colspan = 7;//toma columnas
                    cell038.BorderWidth = 0;
                    cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell038);


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
                    Process.Start(@"c:\shashe\prueba5.pdf");
                }
                else
                {
                    if (sesion.Grado == 4 || sesion.Grado == 5 || sesion.Grado == 6)
                    {
                        //-------------Ingresar los datos del alumno en pdf--------------------------------
                        MySqlConnection conn;
                        MySqlCommand com;

                        string conexion = "server=localhost;uid=root;database=nerivela";
                        string query = "SELECT    `idAlumno`, `ApellidoP`, `ApellidoM`, `nombre`, `CURP`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' " + "ORDER BY  `ApellidoP`  ASC";

                        conn = new MySqlConnection(conexion);
                        conn.Open();

                        com = new MySqlCommand(query, conn);

                        MySqlDataReader myreader = com.ExecuteReader();

                        string[] Nombre = new string[25];
                        string[] Apellidop = new string[25];
                        string[] Apellidom = new string[25];
                        string[] Curp = new string[25];
                        string[] id = new string[25];

                        for (int i = 0; i > 25; i++)
                        {
                            Nombre[i] = " ";
                            Apellidop[i] = " ";
                            Apellidom[i] = " ";
                            Curp[i] = " ";
                            id[i] = " ";
                        }

                        int L = 0;
                        while (myreader.Read())//Agrega calificaciones
                        {
                            //string[] curp = new string[25];
                            Nombre[L] = Convert.ToString(myreader["nombre"]);
                            Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                            Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                            Curp[L] = Convert.ToString(myreader["CURP"]);
                            id[L] = Convert.ToString(myreader["idAlumno"]);
                            //Curp[L] = curp[L].Substring(0, 1);
                            L++;
                        }

                        conn.Close();
                        //-----------------------------Ingresar calificaciones en pdf--------------------------------
                        MySqlConnection conn2;
                        MySqlCommand com2;

                        string conexion2 = "server=localhost;uid=root;database=nerivela";
                        string query2 = "SELECT  *  FROM  `calificaciones`";

                        conn2 = new MySqlConnection(conexion2);
                        conn2.Open();

                        com2 = new MySqlCommand(query2, conn2);

                        MySqlDataReader myreader2 = com2.ExecuteReader();

                        string[] califsep = new string[25];
                        string[] califoct = new string[25];
                        string[] califnov = new string[25];
                        string[] califdic = new string[25];
                        string[] califene = new string[25];
                        string[] califfeb = new string[25];
                        string[] califmar = new string[25];
                        string[] califabr = new string[25];
                        string[] califmay = new string[25];
                        string[] califjun = new string[25];

                        //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
                        MySqlConnection conn1;
                        MySqlCommand com1;

                        string conexion1 = "server=localhost;uid=root;database=nerivela";
                        string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='" + sesion.Grado + "' ";
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

                        //------------------------------------------------------------------------------------------------------------
                        // Creamos el documento con el tamaño de página tradicional
                        Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                        // Indicamos donde vamos a guardar el documento
                        PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(@"C:\shashe\prueba5.pdf", FileMode.Create));

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
                        PdfPTable table = new PdfPTable(16);
                        table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                        iTextSharp.text.Image logoGro = iTextSharp.text.Image.GetInstance("../../../logo.png");
                        logoGro.BorderWidth = 0;
                        logoGro.ScaleAbsolute(110, 40);
                        iTextSharp.text.Image logoEst = iTextSharp.text.Image.GetInstance("../../../logo-gro.png");
                        logoEst.ScaleAbsolute(140, 40);


                        // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                        // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                        float[] Celdas = { 0.15f, 0.55f, 0.30f, 0.30f, 0.40f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.30f, 0.20f };

                        // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                        table.SetWidths(Celdas);


                        PdfPCell cell390 = new PdfPCell(logoGro);
                        cell390.Colspan = 4;//toma columnas
                        cell390.BorderWidth = 0;
                        //cell390.PaddingTop = 5f;
                        //cell390.PaddingBottom = 5f;
                        cell390.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell390);

                        PdfPCell cell39 = new PdfPCell(new Phrase("CALIFICACIONES DEL BIMESTRE NO. 5", tituloprin));
                        cell39.Colspan = 7;//toma columnas
                        cell39.BorderWidth = 0;
                        cell39.PaddingTop = 10f;
                        cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell39);

                        PdfPCell cell380 = new PdfPCell(logoEst);
                        cell380.Colspan = 5;//toma columnas
                        cell380.BorderWidth = 0;
                        cell380.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                         //cell380.PaddingTop = 5f;
                                                         //cell380.PaddingBottom = 5f;
                        table.AddCell(cell380);

                        PdfPCell cell38 = new PdfPCell(new Phrase("Fecha de impresión: \n\n", letchica));
                        cell38.Colspan = 16;//toma columnas
                        cell38.BorderWidth = 0;
                        cell38.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell38);

                        PdfPCell cell398 = new PdfPCell(new Phrase("C.C.T. : 12DPT0003N                                                                               Nombre : INSTITUTO RODOLFO NERI VELA                                          Turno : 100                                               Grado: " + sesion.Grado + "                                     Grupo: A ", cuerpo));
                        cell398.Colspan = 16;//toma columnas
                        cell398.BorderWidth = 0;
                        cell398.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                                         //cell398.PaddingTop = 5f;
                                                         //cell398.PaddingBottom = 5f;
                        table.AddCell(cell398);

                        PdfPCell cell391 = new PdfPCell(new Phrase("Zona : 048                                                                                                                                             Ciclo Escolar : 2018-2019                                                                                                               Id. Docto :", cuerpo));
                        cell391.Colspan = 16;//toma columnas
                        cell391.BorderWidth = 0;
                        cell391.PaddingTop = 5f;
                        cell391.PaddingBottom = 5f;
                        cell391.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell391);

                        PdfPCell cell40 = new PdfPCell(new Phrase("Domicilio : Vicente Guerrero 49, Barrios Historicos, 39540.                                  Localidad : ACAPULCO DE JUAREZ                                             Municipio : ACAPULCO DE JUAREZ                                              Región: ACAPULCO", cuerpo));
                        cell40.Colspan = 16;//toma columnas
                        cell40.BorderWidth = 0;
                        cell40.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        cell40.PaddingBottom = 3f;
                        table.AddCell(cell40);

                        //PdfPCell cell4311 = new PdfPCell(new Phrase(" ", cuerpo));
                        //cell4311.Colspan = 16;//toma columnas
                        //cell4311.BorderWidth = 0;
                        //table.AddCell(cell4311);

                        PdfPCell cell44 = new PdfPCell(new Phrase("N.P. ", cuerpo));
                        cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell44);

                        PdfPCell cell42 = new PdfPCell(new Phrase("CURP", cuerpo));
                        cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell42);

                        PdfPCell cell441 = new PdfPCell(new Phrase("PATERNO ", cuerpo));
                        cell441.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell441);

                        PdfPCell cell421 = new PdfPCell(new Phrase("MATERNO", cuerpo));
                        cell421.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell421);

                        PdfPCell cell422 = new PdfPCell(new Phrase("NOMBRE", cuerpo));
                        cell422.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell422);

                        PdfPCell cell445 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
                        cell445.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell445);

                        PdfPCell cell425 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
                        cell425.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell425);

                        PdfPCell cell447 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
                        cell447.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell447);

                        PdfPCell cell428 = new PdfPCell(new Phrase("C. NATURALES", cuerpo));
                        cell428.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell428);

                        PdfPCell cell4325 = new PdfPCell(new Phrase("GEOGRAFIA", cuerpo));
                        cell4325.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell4325);

                        PdfPCell cell455 = new PdfPCell(new Phrase("HISTORIA", cuerpo));
                        cell455.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell455);

                        PdfPCell cell415 = new PdfPCell(new Phrase("FORM. CÍV. Y ÉTICA", cuerpo));
                        cell415.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell415);

                        PdfPCell cell416 = new PdfPCell(new Phrase("ARTES", cuerpo));
                        cell416.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell416);

                        PdfPCell cell417 = new PdfPCell(new Phrase("ED. SOCIO.", cuerpo));
                        cell417.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell417);

                        PdfPCell cell468 = new PdfPCell(new Phrase("ED. FÍSICA", cuerpo));
                        cell468.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell468);

                        PdfPCell cell418 = new PdfPCell(new Phrase("PROM.", cuerpo));
                        cell418.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell418);


                        table.AddCell("1");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[0], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[0], letmed)));
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

                        table.AddCell("2");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[1], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[1], letmed)));
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

                        table.AddCell("3");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[2], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[2], letmed)));
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

                        table.AddCell("4");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[3], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[3], letmed)));
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

                        table.AddCell("5");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[4], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[4], letmed)));
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

                        table.AddCell("6");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[5], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[5], letmed)));
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

                        table.AddCell("7");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[6], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[6], letmed)));
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

                        table.AddCell("8");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[7], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[7], letmed)));
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

                        table.AddCell("9");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[8], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[8], letmed)));
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

                        table.AddCell("10");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[9], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[9], letmed)));
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

                        table.AddCell("11");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[10], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[10], letmed)));
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

                        table.AddCell("12");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[11], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[11], letmed)));
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

                        table.AddCell("13");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[12], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[12], letmed)));
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

                        table.AddCell("14");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[13], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[13], letmed)));
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

                        table.AddCell("15");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[14], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[14], letmed)));
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

                        table.AddCell("16");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[15], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[15], letmed)));
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

                        table.AddCell("17");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[16], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[16], letmed)));
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

                        table.AddCell("18");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[17], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[17], letmed)));
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

                        table.AddCell("19");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[18], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[18], letmed)));
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

                        table.AddCell("20");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[19], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[19], letmed)));
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

                        table.AddCell("21");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[20], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[20], letmed)));
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

                        table.AddCell("22");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[21], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[21], letmed)));
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

                        table.AddCell("23");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[22], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[22], letmed)));
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

                        table.AddCell("24");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[23], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[23], letmed)));
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

                        table.AddCell("25");
                        table.AddCell(new PdfPCell(new Phrase("" + Curp[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidom[24], letmed)));
                        table.AddCell(new PdfPCell(new Phrase("" + Nombre[24], letmed)));
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

                        PdfPCell cell016 = new PdfPCell(new Phrase("PROMEDIO POR MATERIA", cuerpo));
                        cell016.Colspan = 5;
                        cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell016);


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


                        PdfPCell cell019 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR MATERIAS :  ", cuerpo));
                        cell019.Colspan = 8;
                        cell019.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell019);

                        PdfPCell cell034 = new PdfPCell(new Phrase(" PROMEDIO BIMESTRAL POR ALUMNOS :  ", cuerpo));
                        cell034.Colspan = 8;//toma columnas
                        cell034.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell034);

                        PdfPCell cell017 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell017.Colspan = 16;
                        cell017.BorderWidth = 0;
                        cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell017);

                        PdfPCell cell035 = new PdfPCell(new Phrase("MAESTRO(A) DE GRUPO", cuerpo));
                        cell035.Colspan = 8;//toma columnas
                        cell035.BorderWidth = 0;
                        cell035.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell035);

                        PdfPCell cell036 = new PdfPCell(new Phrase("DIRECTOR(A)", cuerpo));
                        cell036.Colspan = 8;//toma columnas
                        cell036.BorderWidth = 0;
                        cell036.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell036);

                        PdfPCell cell044 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell044.Colspan = 16;//toma columnas
                        cell044.BorderWidth = 0;
                        table.AddCell(cell044);

                        PdfPCell cell4331 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell4331.Colspan = 16;//toma columnas
                        cell4331.BorderWidth = 0;
                        table.AddCell(cell4331);

                        PdfPCell cell037 = new PdfPCell(new Phrase(" " + Apellidop1 + "  " + Apellidom1 + " " + nombre1, cuerpo));//nOMBRE DEL MAESTRO
                        cell037.Colspan = 8;
                        cell037.BorderWidth = 0;
                        cell037.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell037);

                        PdfPCell cell038 = new PdfPCell(new Phrase(" NOMBRE", cuerpo));//NOMBRE DEL DIRECTOR
                        cell038.Colspan = 8;//toma columnas
                        cell038.BorderWidth = 0;
                        cell038.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell038);


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
                        Process.Start(@"c:\shashe\prueba5.pdf");
                    }
                }
            }

        }
    }
}
