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
    public partial class CadaGrupo : MaterialForm
    {
        MySqlCommand codigo = new MySqlCommand();
        MySqlConnection conectanos = new MySqlConnection();
        //MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela;pwd=digi3.0");
        MySqlConnection coneccion = new MySqlConnection("host=localhost;Uid=root;Database=nerivela");
        conexion obj = new conexion();

        public CadaGrupo()
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

            datagrid(dataGridView1);
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red900, Primary.Red700, Primary.Red900, Accent.Red700, TextShade.WHITE);
            mostrardatoscadagrupo();
            
            
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

        //-----------------------------------------Botones--------------------------------------------
        //Cerrar sesion
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
        //Volver al menu principal
        private void btnPrincipal_Click(object sender, EventArgs e)
        {
            System.Threading.Thread pantalla = new System.Threading.Thread(new System.Threading.ThreadStart(ThreadPrincipal));
            pantalla.Start();
            CheckForIllegalCrossThreadCalls = false;
            this.Close();
        }
        //Generar pdf del grupo
        private void GenerarPDF_Click(object sender, EventArgs e)
        {
            //-------------Ingresar los datos del alumno en pdf--------------------------------
            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT    `ApellidoP`, `ApellidoM`, `nombre`, `Genero`  FROM  `alumno`  where idGrado = '" + sesion.Grado + "' "+ "ORDER BY  `ApellidoP`  ASC";

            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(query, conn);

            MySqlDataReader myreader = com.ExecuteReader();

            string[] nombre = new string[25];
            string[] Apellidop = new string[25];
            string[] Apellidom = new string[25];
            string[] Genero = new string[25];

            for(int i = 0; i > 25; i++)
            {
                nombre[i] = " ";
                Apellidop[i] = " ";
                Apellidom[i] = " ";
                Genero[i] = " ";
            }

            int L = 0;
            while (myreader.Read())//Agrega calificaciones
            {
                string[] genero = new string[25];
                nombre[L] = Convert.ToString(myreader["nombre"]);
                Apellidop[L] = Convert.ToString(myreader["ApellidoP"]);
                Apellidom[L] = Convert.ToString(myreader["ApellidoM"]);

                genero[L] = Convert.ToString(myreader["Genero"]);
                Genero[L] = genero[L].Substring(0,1);
                L++;
            }
            
            conn.Close();
            //-------------Ingresar los datos del maestro de 1 en pdf--------------------------------
            MySqlConnection conn1;
            MySqlCommand com1;

            string conexion1 = "server=localhost;uid=root;database=nerivela";
            string query1 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='1' ";
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
            //-------------Ingresar los datos del maestro de 2 en pdf--------------------------------
            MySqlConnection conn2;
            MySqlCommand com2;

            string conexion2 = "server=localhost;uid=root;database=nerivela";
            string query2 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='2' ";
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
            //-------------Ingresar los datos del maestro de 3 en pdf--------------------------------
            MySqlConnection conn3;
            MySqlCommand com3;

            string conexion3 = "server=localhost;uid=root;database=nerivela";
            string query3 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='3' ";
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
            //-------------Ingresar los datos del maestro de 4 en pdf--------------------------------
            MySqlConnection conn4;
            MySqlCommand com4;

            string conexion4 = "server=localhost;uid=root;database=nerivela";
            string query4 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='4' ";
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
            //-------------Ingresar los datos del maestro de 5 en pdf--------------------------------
            MySqlConnection conn5;
            MySqlCommand com5;

            string conexion5 = "server=localhost;uid=root;database=nerivela";
            string query5 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='5' ";
            string nombre5, Apellidop5, Apellidom5;

            conn5 = new MySqlConnection(conexion5);
            conn5.Open();

            com5 = new MySqlCommand(query5, conn5);

            MySqlDataReader myreader5 = com5.ExecuteReader();

            myreader5.Read();
            nombre5 = Convert.ToString(myreader5["nombre"]);
            Apellidop5 = Convert.ToString(myreader5["ApellidoP"]);
            Apellidom5 = Convert.ToString(myreader5["ApellidoM"]);
            conn5.Close();
            //-------------Ingresar los datos del maestro de  en pdf--------------------------------
            MySqlConnection conn6;
            MySqlCommand com6;

            string conexion6 = "server=localhost;uid=root;database=nerivela";
            string query6 = "SELECT  nombre, ApellidoP, ApellidoM  FROM  `maestros`  where  gradoEncargado ='6' ";
            string nombre6, Apellidop6, Apellidom6;

            conn6 = new MySqlConnection(conexion6);
            conn6.Open();

            com6 = new MySqlCommand(query6, conn6);

            MySqlDataReader myreader6 = com6.ExecuteReader();

            myreader6.Read();
            nombre6 = Convert.ToString(myreader6["nombre"]);
            Apellidop6 = Convert.ToString(myreader6["ApellidoP"]);
            Apellidom6 = Convert.ToString(myreader6["ApellidoM"]);
            conn6.Close();
            //exportardata(dataGridView1, "test");
            if (Convert.ToInt32(sesion.Grado) == 1)
            {
                // Creamos el documento con el tamaño de página tradicional
                Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
                if (!Directory.Exists(folderPath))// pregunt si no existe
                {
                    Directory.CreateDirectory(folderPath); // si no existe lo crea
                }
                // Creamos el documento con el tamaño de página tradicional
                FileStream stream = new FileStream(folderPath + "Lista-Asistencia2.pdf", FileMode.Create);
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
                PdfPTable table = new PdfPTable(40);
                table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                iTextSharp.text.Image logoEsc = iTextSharp.text.Image.GetInstance("../../../logo-esc.png");
                logoEsc.BorderWidth = 0;
                logoEsc.ScaleAbsolute(120, 70);
                iTextSharp.text.Image logoSep = iTextSharp.text.Image.GetInstance("../../../logo.png");
                logoSep.ScaleAbsolute(150, 60);


                // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                float[] Celdas = { 0.25f, 0.25f, 1.50f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.50f, 0.50f, 0.50f, 0.50f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.20f, 0.45f };

                // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                table.SetWidths(Celdas);

                //encabezado

                PdfPCell cell40 = new PdfPCell(new Phrase(" "));
                cell40.Colspan = 2;//toma columnas
                cell40.Rowspan = 4;//toma filas
                cell40.BorderWidth = 0;
                table.AddCell(cell40);

                PdfPCell cell39 = new PdfPCell(new Phrase("GRUPO: " + sesion.Grado + "  A", cuerpo));
                cell39.BorderWidth = 0;
                cell39.PaddingTop = 5f;
                cell39.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell39);

                PdfPCell cell53 = new PdfPCell(new Phrase(" "));
                cell53.Colspan = 7;//toma columnas
                cell53.Rowspan = 4;//toma filas
                cell53.BorderWidth = 0;
                table.AddCell(cell53);

                PdfPCell cell1z = new PdfPCell(new Phrase("INSTITTUTO RODOLFO NERI VELA\n\n PRESTIGIO EN TU CONOCIMIENTO\n\n Vicente Guerrero 49, Barrios Historicos, 39540.", titulos));
                cell1z.Colspan = 24;//toma columnas
                cell1z.Rowspan = 4;//toma filas
                cell1z.BorderWidth = 0;
                cell1z.PaddingTop = 5f;
                cell1z.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell1z);

                PdfPCell cell44 = new PdfPCell(new Phrase(" "));
                cell44.Colspan = 6;//toma columnas
                cell44.Rowspan = 4;
                cell44.BorderWidth = 0;
                table.AddCell(cell44);

                PdfPCell cell42 = new PdfPCell(new Phrase("PROFR.(A):  " + Apellidop1 + "  " + Apellidom1 + "  " + nombre1, cuerpo));
                cell42.BorderWidth = 0;
                cell42.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell42);

                PdfPCell cell4311 = new PdfPCell(new Phrase("MES: ", cuerpo));
                cell4311.BorderWidth = 0;
                cell4311.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell4311);

                PdfPCell cell43 = new PdfPCell(new Phrase("AÑO ESCOLAR: 2018 - 2019", cuerpo));
                cell43.BorderWidth = 0;
                cell43.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell43);

                //Fila para dar espaciado entre tablas
                PdfPCell cell112 = new PdfPCell(new Phrase(" ", cuerpo));
                cell112.BorderWidth = 0;
                cell112.Colspan = 40;
                table.AddCell(cell112);

                //tabla de formacion academica            
                PdfPCell cell431 = new PdfPCell(new Phrase("Núm.\nProgr.", cuerpo));
                cell431.Rowspan = 3;//toma columnas
                cell431.Rotation = 90;
                cell431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell431);

                PdfPCell cell1 = new PdfPCell(new Phrase("Sexo", cuerpo));
                cell1.Rowspan = 3;//toma filas
                cell1.PaddingLeft = 5f;
                cell1.Rotation = 90;
                cell1.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell1);

                PdfPCell cell2 = new PdfPCell(new Phrase("Nombres", cuerpo));
                cell2.PaddingTop = 5f;
                cell2.Rowspan = 3;
                cell2.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2);

                PdfPCell cell2A = new PdfPCell(new Phrase(" ", cuerpo));
                cell2A.Rowspan = 3;
                cell2A.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2A);

                PdfPCell cell2B = new PdfPCell(new Phrase(" ", cuerpo));
                cell2B.Rowspan = 3;
                cell2B.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2B);

                PdfPCell cell2C = new PdfPCell(new Phrase(" ", cuerpo));
                cell2C.Rowspan = 3;
                cell2C.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2C);

                PdfPCell cell2D = new PdfPCell(new Phrase(" ", cuerpo));
                cell2D.Rowspan = 3;
                cell2D.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2D);

                PdfPCell cellC = new PdfPCell(new Phrase(" ", cuerpo));
                cellC.Rowspan = 3;
                cellC.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cellC);

                PdfPCell cell25 = new PdfPCell(new Phrase(" ", cuerpo));
                cell25.Rowspan = 3;
                cell25.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell25);

                PdfPCell cell26 = new PdfPCell(new Phrase(" ", cuerpo));
                cell26.Rowspan = 3;
                cell26.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell26);

                PdfPCell cell12 = new PdfPCell(new Phrase(" ", cuerpo));
                cell12.Rowspan = 3;
                cell12.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell12);

                PdfPCell cell3 = new PdfPCell(new Phrase(" ", cuerpo));
                cell3.Rowspan = 3;
                cell3.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell3);

                PdfPCell cell13 = new PdfPCell(new Phrase(" ", cuerpo));
                cell13.Rowspan = 3;
                cell13.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell13);

                PdfPCell cell381 = new PdfPCell(new Phrase(" ", cuerpo));
                cell381.Rowspan = 3;
                cell381.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell381);

                PdfPCell cell19 = new PdfPCell(new Phrase(" ", cuerpo));
                cell19.Rowspan = 3;
                cell19.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell19);

                PdfPCell cell28 = new PdfPCell(new Phrase(" ", cuerpo));
                cell28.Rowspan = 3;
                cell28.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell28);

                PdfPCell cell8 = new PdfPCell(new Phrase(" ", cuerpo));
                cell8.Rowspan = 3;
                cell8.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell8);

                PdfPCell cell9 = new PdfPCell(new Phrase(" ", cuerpo));
                cell9.Rowspan = 3;
                cell9.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell9);

                PdfPCell cell69 = new PdfPCell(new Phrase(" ", cuerpo));
                cell69.Rowspan = 3;
                cell69.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell69);

                PdfPCell cell29 = new PdfPCell(new Phrase(" ", cuerpo));
                cell29.Rowspan = 3;
                cell29.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell29);

                PdfPCell cell30 = new PdfPCell(new Phrase(" ", cuerpo));
                cell30.Rowspan = 3;
                cell30.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell30);

                PdfPCell cell24 = new PdfPCell(new Phrase(" ", cuerpo));
                cell24.Rowspan = 3;
                cell24.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell24);

                PdfPCell cell4 = new PdfPCell(new Phrase(" ", cuerpo));
                cell4.Rowspan = 3;
                cell4.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell4);

                PdfPCell cell201 = new PdfPCell(new Phrase(" ", cuerpo));
                cell201.Rowspan = 3;
                cell201.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell201);

                PdfPCell cell202 = new PdfPCell(new Phrase(" ", cuerpo));
                cell202.Rowspan = 3;
                cell202.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell202);

                PdfPCell cell5 = new PdfPCell(new Phrase(" ", cuerpo));
                cell5.Rowspan = 3;
                cell5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell5);

                PdfPCell cell37 = new PdfPCell(new Phrase("Asisten", letmed));
                cell37.Rowspan = 3;
                cell37.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell37);

                PdfPCell cell27 = new PdfPCell(new Phrase("Inasist", letmed));
                cell27.Rowspan = 3;
                cell27.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell27);

                PdfPCell cell6 = new PdfPCell(new Phrase("RASGOS A EVALUAR", cuerpo));
                cell6.Colspan = 4;
                cell6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell6);

                PdfPCell cell7 = new PdfPCell(new Phrase("EXAMENES", cuerpo));
                cell7.Colspan = 6;
                cell7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell7);

                PdfPCell cell210 = new PdfPCell(new Phrase(" ", cuerpo));
                cell210.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell210);

                PdfPCell cell1a5 = new PdfPCell(new Phrase(" ", cuerpo));
                cell1a5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell1a5);

                PdfPCell cell1a6 = new PdfPCell(new Phrase("TRABAJO EN CLASES", letmed));
                cell1a6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell1a6);

                PdfPCell cell1a7 = new PdfPCell(new Phrase("VALORES", letmed));
                cell1a7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell1a7);

                PdfPCell cell211 = new PdfPCell(new Phrase("PARTICIPACIÓN", letmed));
                cell211.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell211);

                PdfPCell cell212 = new PdfPCell(new Phrase("TAREAS", letmed));
                cell212.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell212);

                PdfPCell cell214 = new PdfPCell(new Phrase("ESP", cuerpo));
                cell214.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell214);

                PdfPCell cell10 = new PdfPCell(new Phrase("MAT", cuerpo));
                cell10.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell10);

                PdfPCell cell220 = new PdfPCell(new Phrase("ING", cuerpo));
                cell220.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell220);

                PdfPCell cell2121 = new PdfPCell(new Phrase("CON", cuerpo));
                cell2121.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2121);

                PdfPCell cell1110 = new PdfPCell(new Phrase("ED.ECO", cuerpo));
                cell1110.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell1110);

                PdfPCell cell221 = new PdfPCell(new Phrase("ART", cuerpo));
                cell221.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell221);

                PdfPCell cell222 = new PdfPCell(new Phrase("ED.FIS", cuerpo));
                cell222.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell222);

                PdfPCell cell101 = new PdfPCell(new Phrase("PROG. GRAL", cuerpo));
                cell101.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell101);

                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                //listas de alumnos
                table.AddCell(" 1");
                table.AddCell(new PdfPCell(new Phrase("" + Genero[0], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0] + " " + Apellidom[0] + nombre[0], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell(" 2");
                table.AddCell(new PdfPCell(new Phrase("" + Genero[1], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1] + " " + Apellidom[1] + nombre[1], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell(" 3");
                table.AddCell(new PdfPCell(new Phrase("" + Genero[2], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2] + " " + Apellidom[2] + nombre[2], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell(" 4");
                table.AddCell(new PdfPCell(new Phrase("" + Genero[3], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3] + " " + Apellidom[3] + nombre[3], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell(" 5");
                table.AddCell(new PdfPCell(new Phrase("" + Genero[4], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4] + " " + Apellidom[4] + nombre[4], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell(" 6");
                table.AddCell(new PdfPCell(new Phrase("" + Genero[5], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5] + " " + Apellidom[5] + nombre[5], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell(" 7");
                table.AddCell(new PdfPCell(new Phrase("" + Genero[6], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6] + " " + Apellidom[6] + nombre[6], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell(" 8");
                table.AddCell(new PdfPCell(new Phrase("" + Genero[7], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7] + " " + Apellidom[7] + nombre[7], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");

                table.AddCell(" 9");
                table.AddCell(new PdfPCell(new Phrase("" + Genero[8], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8] + " " + Apellidom[8] + nombre[8], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[9], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9] + " " + Apellidom[9] + nombre[9], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[10], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10] + " " + Apellidom[10] + nombre[10], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[11], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11] + " " + Apellidom[11] + nombre[11], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[12], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12] + " " + Apellidom[12] + nombre[12], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[13], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13] + " " + Apellidom[13] + nombre[13], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[14], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14] + " " + Apellidom[14] + nombre[14], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[15], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15] + " " + Apellidom[15] + nombre[15], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[16], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16] + " " + Apellidom[16] + nombre[16], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[17], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17] + " " + Apellidom[17] + nombre[17], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[18], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase(Apellidop[18] + " " + Apellidom[18] + nombre[18], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[19], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19] + " " + Apellidom[19] + nombre[19], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[20], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20] + " " + Apellidom[20] + nombre[20], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[21], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21] + " " + Apellidom[21] + nombre[21], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[22], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22] + " " + Apellidom[22] + nombre[22], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[23], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23] + " " + Apellidom[23] + nombre[23], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
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
                table.AddCell(new PdfPCell(new Phrase("" + Genero[24], cuerpo)));
                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24] + " " + Apellidom[24] + nombre[24], letmed)));
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");
                table.AddCell(" ");



                //Fila para dar espaciado entre tablas
                PdfPCell cell113 = new PdfPCell(new Phrase(" ", cuerpo));
                cell113.BorderWidth = 0;
                cell113.Colspan = 40;
                table.AddCell(cell113);

                PdfPCell cell140 = new PdfPCell(new Phrase(" "));
                cell140.Colspan = 3;//toma columnas
                cell140.BorderWidth = 0;
                cell140.Rowspan = 5;//toma filas
                                    //cell140.BorderWidth = 0;
                table.AddCell(cell140);

                PdfPCell cell230 = new PdfPCell(new Phrase("HOMBRES: ", cuerpo));
                cell230.Colspan = 10;//toma columnas
                cell230.BorderWidth = 0;
                cell230.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell230);

                PdfPCell cell1012 = new PdfPCell(new Phrase(" ", cuerpo));
                cell1012.Colspan = 8;//toma columnas
                cell1012.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell1012);

                PdfPCell celld = new PdfPCell(new Phrase("EXIST.", cuerpo));
                celld.Colspan = 7;//toma columnas
                celld.BorderWidth = 0;
                celld.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(celld);

                PdfPCell cell231 = new PdfPCell(new Phrase("APROB.", cuerpo));
                cell231.Colspan = 2;
                cell231.BorderWidth = 0;
                cell231.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell231);

                PdfPCell cell232 = new PdfPCell(new Phrase("REPROB.", cuerpo));
                cell232.Colspan = 2;
                cell232.BorderWidth = 0;
                cell232.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell232);

                PdfPCell cell238 = new PdfPCell(new Phrase(" ", cuerpo));
                cell238.Colspan = 9;
                cell238.BorderWidth = 0;
                cell238.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell238);

                PdfPCell cell77 = new PdfPCell(new Phrase("", cuerpo));
                cell77.Colspan = 18;
                cell77.BorderWidth = 0;
                cell77.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell77);

                PdfPCell cell240 = new PdfPCell(new Phrase(" ", cuerpo));
                cell240.Colspan = 7;
                cell240.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell240);

                PdfPCell cell241 = new PdfPCell(new Phrase("", cuerpo));
                cell241.Colspan = 2;
                cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell241);

                PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
                cell242.Colspan = 2;
                cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell242);

                PdfPCell cell207 = new PdfPCell(new Phrase(" ", cuerpo));
                cell207.BorderWidth = 0;
                cell207.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell207);

                PdfPCell cell219 = new PdfPCell(new Phrase("PROMEDIO ", cuerpo));
                cell219.Colspan = 4;
                cell219.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell219);

                PdfPCell cell250 = new PdfPCell(new Phrase(" ", cuerpo));
                cell250.Colspan = 3;
                cell250.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell250);

                PdfPCell cell236 = new PdfPCell(new Phrase("MUJERES: ", cuerpo));
                cell236.Colspan = 10;//toma columnas
                cell236.BorderWidth = 0;
                cell236.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell236);

                PdfPCell cell1014 = new PdfPCell(new Phrase(" ", cuerpo));
                cell1014.Colspan = 8;//toma columnas
                cell1014.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell1014);

                PdfPCell cell12d = new PdfPCell(new Phrase(" ", cuerpo));
                cell12d.Colspan = 7;//toma columnas
                cell12d.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell12d);

                PdfPCell cell2331 = new PdfPCell(new Phrase(" ", cuerpo));
                cell2331.Colspan = 2;
                cell2331.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2331);

                PdfPCell cell2342 = new PdfPCell(new Phrase(" ", cuerpo));
                cell2342.Colspan = 2;
                cell2342.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2342);

                PdfPCell cell2017 = new PdfPCell(new Phrase(" ", cuerpo));
                cell2017.BorderWidth = 0;
                cell2017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2017);

                PdfPCell cell2119 = new PdfPCell(new Phrase("% DE APROBADOS ", cuerpo));
                cell2119.Colspan = 4;
                cell2119.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2119);

                PdfPCell cell2150 = new PdfPCell(new Phrase(" ", cuerpo));
                cell2150.Colspan = 3;
                cell2150.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2150);

                PdfPCell cell177 = new PdfPCell(new Phrase("", cuerpo));
                cell177.Colspan = 18;
                cell177.BorderWidth = 0;
                cell177.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell177);

                PdfPCell cell2401 = new PdfPCell(new Phrase(" ", cuerpo));
                cell2401.Colspan = 7;
                cell2401.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2401);

                PdfPCell cell2431 = new PdfPCell(new Phrase("", cuerpo));
                cell2431.Colspan = 2;
                cell2431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2431);

                PdfPCell cell2432 = new PdfPCell(new Phrase(" ", cuerpo));
                cell2432.Colspan = 2;
                cell2432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2432);

                PdfPCell cell2328 = new PdfPCell(new Phrase(" ", cuerpo));
                cell2328.Colspan = 9;
                cell2328.BorderWidth = 0;
                cell2328.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell2328);

                PdfPCell cell137 = new PdfPCell(new Phrase("FIRMA DEL MAESTRO(A): ", cuerpo));
                cell137.Colspan = 10;
                cell137.BorderWidth = 0;
                cell137.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell137);

                PdfPCell cell1054 = new PdfPCell(new Phrase(" ", cuerpo));
                cell1054.Colspan = 15;//toma columnas
                cell1054.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell1054);

                PdfPCell cell432 = new PdfPCell(new Phrase(" ", cuerpo));
                cell432.BorderWidth = 0;
                cell432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell432);

                PdfPCell cell228 = new PdfPCell(new Phrase("Acapulco Gro., a ________________ de ________________ del 20_____", cuerpo));
                cell228.Colspan = 11;
                cell228.BorderWidth = 0;
                cell228.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                table.AddCell(cell228);

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
            else
            {
                if (Convert.ToInt32(sesion.Grado) == 2)
                {
                    // Creamos el documento con el tamaño de página tradicional
                            Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                    string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
                    if (!Directory.Exists(folderPath))// pregunt si no existe
                    {
                        Directory.CreateDirectory(folderPath); // si no existe lo crea
                    }
                    // Creamos el documento con el tamaño de página tradicional
                    FileStream stream = new FileStream(folderPath + "Lista-Asistencia2.pdf", FileMode.Create);
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
                    PdfPTable table = new PdfPTable(40);
                    table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                    iTextSharp.text.Image logoEsc = iTextSharp.text.Image.GetInstance("../../../logo-esc.png");
                    logoEsc.BorderWidth = 0;
                    logoEsc.ScaleAbsolute(120, 70);
                    iTextSharp.text.Image logoSep = iTextSharp.text.Image.GetInstance("../../../logo.png");
                    logoSep.ScaleAbsolute(150, 60);


                    // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                    // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                    float[] Celdas = { 0.25f, 0.25f, 1.50f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.50f, 0.50f, 0.50f, 0.50f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.20f, 0.45f };

                    // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                    table.SetWidths(Celdas);

                    //encabezado

                    PdfPCell cell40 = new PdfPCell(new Phrase(" "));
                    cell40.Colspan = 2;//toma columnas
                    cell40.Rowspan = 4;//toma filas
                    cell40.BorderWidth = 0;
                    table.AddCell(cell40);

                    PdfPCell cell39 = new PdfPCell(new Phrase("GRUPO: " + sesion.Grado + "  A", cuerpo));
                    cell39.BorderWidth = 0;
                    cell39.PaddingTop = 5f;
                    cell39.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell39);

                    PdfPCell cell53 = new PdfPCell(new Phrase(" "));
                    cell53.Colspan = 7;//toma columnas
                    cell53.Rowspan = 4;//toma filas
                    cell53.BorderWidth = 0;
                    table.AddCell(cell53);

                    PdfPCell cell1z = new PdfPCell(new Phrase("INSTITTUTO RODOLFO NERI VELA\n\n PRESTIGIO EN TU CONOCIMIENTO\n\n Vicente Guerrero 49, Barrios Historicos, 39540.", titulos));
                    cell1z.Colspan = 24;//toma columnas
                    cell1z.Rowspan = 4;//toma filas
                    cell1z.BorderWidth = 0;
                    cell1z.PaddingTop = 5f;
                    cell1z.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell1z);

                    PdfPCell cell44 = new PdfPCell(new Phrase(" "));
                    cell44.Colspan = 6;//toma columnas
                    cell44.Rowspan = 4;
                    cell44.BorderWidth = 0;
                    table.AddCell(cell44);

                    PdfPCell cell42 = new PdfPCell(new Phrase("PROFR.(A):  " + Apellidop2 + "  " + Apellidom2 + "  " + nombre2, cuerpo));
                    cell42.BorderWidth = 0;
                    cell42.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell42);

                    PdfPCell cell4311 = new PdfPCell(new Phrase("MES: ", cuerpo));
                    cell4311.BorderWidth = 0;
                    cell4311.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell4311);

                    PdfPCell cell43 = new PdfPCell(new Phrase("AÑO ESCOLAR: 2018 - 2019", cuerpo));
                    cell43.BorderWidth = 0;
                    cell43.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell43);

                    //Fila para dar espaciado entre tablas
                    PdfPCell cell112 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell112.BorderWidth = 0;
                    cell112.Colspan = 40;
                    table.AddCell(cell112);

                    //tabla de formacion academica            
                    PdfPCell cell431 = new PdfPCell(new Phrase("Núm.\nProgr.", cuerpo));
                    cell431.Rowspan = 3;//toma columnas
                    cell431.Rotation = 90;
                    cell431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell431);

                    PdfPCell cell1 = new PdfPCell(new Phrase("Sexo", cuerpo));
                    cell1.Rowspan = 3;//toma filas
                    cell1.PaddingLeft = 5f;
                    cell1.Rotation = 90;
                    cell1.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell1);

                    PdfPCell cell2 = new PdfPCell(new Phrase("Nombres", cuerpo));
                    cell2.PaddingTop = 5f;
                    cell2.Rowspan = 3;
                    cell2.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2);

                    PdfPCell cell2A = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2A.Rowspan = 3;
                    cell2A.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2A);

                    PdfPCell cell2B = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2B.Rowspan = 3;
                    cell2B.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2B);

                    PdfPCell cell2C = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2C.Rowspan = 3;
                    cell2C.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2C);

                    PdfPCell cell2D = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2D.Rowspan = 3;
                    cell2D.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2D);

                    PdfPCell cellC = new PdfPCell(new Phrase(" ", cuerpo));
                    cellC.Rowspan = 3;
                    cellC.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cellC);

                    PdfPCell cell25 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell25.Rowspan = 3;
                    cell25.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell25);

                    PdfPCell cell26 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell26.Rowspan = 3;
                    cell26.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell26);

                    PdfPCell cell12 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell12.Rowspan = 3;
                    cell12.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell12);

                    PdfPCell cell3 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell3.Rowspan = 3;
                    cell3.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell3);

                    PdfPCell cell13 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell13.Rowspan = 3;
                    cell13.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell13);

                    PdfPCell cell381 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell381.Rowspan = 3;
                    cell381.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell381);

                    PdfPCell cell19 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell19.Rowspan = 3;
                    cell19.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell19);

                    PdfPCell cell28 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell28.Rowspan = 3;
                    cell28.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell28);

                    PdfPCell cell8 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell8.Rowspan = 3;
                    cell8.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell8);

                    PdfPCell cell9 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell9.Rowspan = 3;
                    cell9.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell9);

                    PdfPCell cell69 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell69.Rowspan = 3;
                    cell69.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell69);

                    PdfPCell cell29 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell29.Rowspan = 3;
                    cell29.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell29);

                    PdfPCell cell30 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell30.Rowspan = 3;
                    cell30.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell30);

                    PdfPCell cell24 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell24.Rowspan = 3;
                    cell24.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell24);

                    PdfPCell cell4 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell4.Rowspan = 3;
                    cell4.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell4);

                    PdfPCell cell201 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell201.Rowspan = 3;
                    cell201.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell201);

                    PdfPCell cell202 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell202.Rowspan = 3;
                    cell202.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell202);

                    PdfPCell cell5 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell5.Rowspan = 3;
                    cell5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell5);

                    PdfPCell cell37 = new PdfPCell(new Phrase("Asisten", letmed));
                    cell37.Rowspan = 3;
                    cell37.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell37);

                    PdfPCell cell27 = new PdfPCell(new Phrase("Inasist", letmed));
                    cell27.Rowspan = 3;
                    cell27.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell27);

                    PdfPCell cell6 = new PdfPCell(new Phrase("RASGOS A EVALUAR", cuerpo));
                    cell6.Colspan = 4;
                    cell6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell6);

                    PdfPCell cell7 = new PdfPCell(new Phrase("EXAMENES", cuerpo));
                    cell7.Colspan = 6;
                    cell7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell7);

                    PdfPCell cell210 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell210.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell210);

                    PdfPCell cell1a5 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell1a5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell1a5);

                    PdfPCell cell1a6 = new PdfPCell(new Phrase("TRABAJO EN CLASES", letmed));
                    cell1a6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell1a6);

                    PdfPCell cell1a7 = new PdfPCell(new Phrase("VALORES", letmed));
                    cell1a7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell1a7);

                    PdfPCell cell211 = new PdfPCell(new Phrase("PARTICIPACIÓN", letmed));
                    cell211.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell211);

                    PdfPCell cell212 = new PdfPCell(new Phrase("TAREAS", letmed));
                    cell212.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell212);

                    PdfPCell cell214 = new PdfPCell(new Phrase("ESP", cuerpo));
                    cell214.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell214);

                    PdfPCell cell10 = new PdfPCell(new Phrase("MAT", cuerpo));
                    cell10.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell10);

                    PdfPCell cell220 = new PdfPCell(new Phrase("ING", cuerpo));
                    cell220.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell220);

                    PdfPCell cell2121 = new PdfPCell(new Phrase("CON", cuerpo));
                    cell2121.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2121);

                    PdfPCell cell1110 = new PdfPCell(new Phrase("ED.ECO", cuerpo));
                    cell1110.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell1110);

                    PdfPCell cell221 = new PdfPCell(new Phrase("ART", cuerpo));
                    cell221.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell221);

                    PdfPCell cell222 = new PdfPCell(new Phrase("ED.FIS", cuerpo));
                    cell222.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell222);

                    PdfPCell cell101 = new PdfPCell(new Phrase("PROG. GRAL", cuerpo));
                    cell101.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell101);

                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");

                    //listas de alumnos
                    table.AddCell(" 1");
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[0], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0] + " " + Apellidom[0] + nombre[0], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");

                    table.AddCell(" 2");
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[1], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1] + " " + Apellidom[1] + nombre[1], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");

                    table.AddCell(" 3");
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[2], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2] + " " + Apellidom[2] + nombre[2], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");

                    table.AddCell(" 4");
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[3], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3] + " " + Apellidom[3] + nombre[3], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");

                    table.AddCell(" 5");
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[4], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4] + " " + Apellidom[4] + nombre[4], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");

                    table.AddCell(" 6");
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[5], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5] + " " + Apellidom[5] + nombre[5], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");

                    table.AddCell(" 7");
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[6], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6] + " " + Apellidom[6] + nombre[6], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");

                    table.AddCell(" 8");
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[7], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7] + " " + Apellidom[7] + nombre[7], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");

                    table.AddCell(" 9");
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[8], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8] + " " + Apellidom[8] + nombre[8], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[9], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9] + " " + Apellidom[9] + nombre[9], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[10], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10] + " " + Apellidom[10] + nombre[10], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[11], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11] + " " + Apellidom[11] + nombre[11], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[12], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12] + " " + Apellidom[12] + nombre[12], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[13], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13] + " " + Apellidom[13] + nombre[13], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[14], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14] + " " + Apellidom[14] + nombre[14], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[15], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15] + " " + Apellidom[15] + nombre[15], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[16], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16] + " " + Apellidom[16] + nombre[16], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[17], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17] + " " + Apellidom[17] + nombre[17], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[18], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase(Apellidop[18] + " " + Apellidom[18] + nombre[18], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[19], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19] + " " + Apellidom[19] + nombre[19], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[20], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20] + " " + Apellidom[20] + nombre[20], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[21], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21] + " " + Apellidom[21] + nombre[21], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[22], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22] + " " + Apellidom[22] + nombre[22], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[23], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23] + " " + Apellidom[23] + nombre[23], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
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
                    table.AddCell(new PdfPCell(new Phrase("" + Genero[24], cuerpo)));
                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24] + " " + Apellidom[24] + nombre[24], letmed)));
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");
                    table.AddCell(" ");



                    //Fila para dar espaciado entre tablas
                    PdfPCell cell113 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell113.BorderWidth = 0;
                    cell113.Colspan = 40;
                    table.AddCell(cell113);

                    PdfPCell cell140 = new PdfPCell(new Phrase(" "));
                    cell140.Colspan = 3;//toma columnas
                    cell140.BorderWidth = 0;
                    cell140.Rowspan = 5;//toma filas
                                        //cell140.BorderWidth = 0;
                    table.AddCell(cell140);

                    PdfPCell cell230 = new PdfPCell(new Phrase("HOMBRES: ", cuerpo));
                    cell230.Colspan = 10;//toma columnas
                    cell230.BorderWidth = 0;
                    cell230.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell230);

                    PdfPCell cell1012 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell1012.Colspan = 8;//toma columnas
                    cell1012.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell1012);

                    PdfPCell celld = new PdfPCell(new Phrase("EXIST.", cuerpo));
                    celld.Colspan = 7;//toma columnas
                    celld.BorderWidth = 0;
                    celld.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(celld);

                    PdfPCell cell231 = new PdfPCell(new Phrase("APROB.", cuerpo));
                    cell231.Colspan = 2;
                    cell231.BorderWidth = 0;
                    cell231.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell231);

                    PdfPCell cell232 = new PdfPCell(new Phrase("REPROB.", cuerpo));
                    cell232.Colspan = 2;
                    cell232.BorderWidth = 0;
                    cell232.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell232);

                    PdfPCell cell238 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell238.Colspan = 9;
                    cell238.BorderWidth = 0;
                    cell238.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell238);

                    PdfPCell cell77 = new PdfPCell(new Phrase("", cuerpo));
                    cell77.Colspan = 18;
                    cell77.BorderWidth = 0;
                    cell77.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell77);

                    PdfPCell cell240 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell240.Colspan = 7;
                    cell240.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell240);

                    PdfPCell cell241 = new PdfPCell(new Phrase("", cuerpo));
                    cell241.Colspan = 2;
                    cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell241);

                    PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell242.Colspan = 2;
                    cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell242);

                    PdfPCell cell207 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell207.BorderWidth = 0;
                    cell207.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell207);

                    PdfPCell cell219 = new PdfPCell(new Phrase("PROMEDIO ", cuerpo));
                    cell219.Colspan = 4;
                    cell219.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell219);

                    PdfPCell cell250 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell250.Colspan = 3;
                    cell250.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell250);

                    PdfPCell cell236 = new PdfPCell(new Phrase("MUJERES: ", cuerpo));
                    cell236.Colspan = 10;//toma columnas
                    cell236.BorderWidth = 0;
                    cell236.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell236);

                    PdfPCell cell1014 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell1014.Colspan = 8;//toma columnas
                    cell1014.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell1014);

                    PdfPCell cell12d = new PdfPCell(new Phrase(" ", cuerpo));
                    cell12d.Colspan = 7;//toma columnas
                    cell12d.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell12d);

                    PdfPCell cell2331 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2331.Colspan = 2;
                    cell2331.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2331);

                    PdfPCell cell2342 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2342.Colspan = 2;
                    cell2342.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2342);

                    PdfPCell cell2017 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2017.BorderWidth = 0;
                    cell2017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2017);

                    PdfPCell cell2119 = new PdfPCell(new Phrase("% DE APROBADOS ", cuerpo));
                    cell2119.Colspan = 4;
                    cell2119.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2119);

                    PdfPCell cell2150 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2150.Colspan = 3;
                    cell2150.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2150);

                    PdfPCell cell177 = new PdfPCell(new Phrase("", cuerpo));
                    cell177.Colspan = 18;
                    cell177.BorderWidth = 0;
                    cell177.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell177);

                    PdfPCell cell2401 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2401.Colspan = 7;
                    cell2401.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2401);

                    PdfPCell cell2431 = new PdfPCell(new Phrase("", cuerpo));
                    cell2431.Colspan = 2;
                    cell2431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2431);

                    PdfPCell cell2432 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2432.Colspan = 2;
                    cell2432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2432);

                    PdfPCell cell2328 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell2328.Colspan = 9;
                    cell2328.BorderWidth = 0;
                    cell2328.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell2328);

                    PdfPCell cell137 = new PdfPCell(new Phrase("FIRMA DEL MAESTRO(A): ", cuerpo));
                    cell137.Colspan = 10;
                    cell137.BorderWidth = 0;
                    cell137.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell137);

                    PdfPCell cell1054 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell1054.Colspan = 15;//toma columnas
                    cell1054.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell1054);

                    PdfPCell cell432 = new PdfPCell(new Phrase(" ", cuerpo));
                    cell432.BorderWidth = 0;
                    cell432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell432);

                    PdfPCell cell228 = new PdfPCell(new Phrase("Acapulco Gro., a ________________ de ________________ del 20_____", cuerpo));
                    cell228.Colspan = 11;
                    cell228.BorderWidth = 0;
                    cell228.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                    table.AddCell(cell228);

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
                else
                {
                    if (Convert.ToInt32(sesion.Grado) == 3)
                    {
                        //Creamos el documento con el tamaño de página tradicional
                            Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                        string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
                        if (!Directory.Exists(folderPath))// pregunt si no existe
                        {
                            Directory.CreateDirectory(folderPath); // si no existe lo crea
                        }
                        // Creamos el documento con el tamaño de página tradicional
                        FileStream stream = new FileStream(folderPath + "Lista-Asistencia3.pdf", FileMode.Create);
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
                        PdfPTable table = new PdfPTable(42);
                        table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                        iTextSharp.text.Image logoEsc = iTextSharp.text.Image.GetInstance("../../../logo-esc.png");
                        logoEsc.BorderWidth = 0;
                        logoEsc.ScaleAbsolute(120, 70);
                        iTextSharp.text.Image logoSep = iTextSharp.text.Image.GetInstance("../../../logo.png");
                        logoSep.ScaleAbsolute(150, 60);


                        // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                        // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                        float[] Celdas = { 0.25f, 0.25f, 1.50f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.50f, 0.50f, 0.50f, 0.50f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.20f, 0.45f };

                        // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                        table.SetWidths(Celdas);

                        //encabezado

                        PdfPCell cell40 = new PdfPCell(new Phrase(" "));
                        cell40.Colspan = 2;//toma columnas
                        cell40.Rowspan = 4;//toma filas
                        cell40.BorderWidth = 0;
                        table.AddCell(cell40);

                        PdfPCell cell39 = new PdfPCell(new Phrase("GRUPO: " + sesion.Grado + "  A", cuerpo));
                        cell39.BorderWidth = 0;
                        cell39.PaddingTop = 5f;
                        cell39.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell39);

                        PdfPCell cell53 = new PdfPCell(new Phrase(" "));
                        cell53.Colspan = 7;//toma columnas
                        cell53.Rowspan = 4;//toma filas
                        cell53.BorderWidth = 0;
                        table.AddCell(cell53);

                        PdfPCell cell1z = new PdfPCell(new Phrase("INSTITTUTO RODOLFO NERI VELA\n\n PRESTIGIO EN TU CONOCIMIENTO\n\n Vicente Guerrero 49, Barrios Historicos, 39540.", titulos));
                        cell1z.Colspan = 24;//toma columnas
                        cell1z.Rowspan = 4;//toma filas
                        cell1z.BorderWidth = 0;
                        cell1z.PaddingTop = 5f;
                        cell1z.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell1z);

                        PdfPCell cell44 = new PdfPCell(new Phrase(" "));
                        cell44.Colspan = 8;//toma columnas
                        cell44.Rowspan = 4;
                        cell44.BorderWidth = 0;
                        table.AddCell(cell44);

                        PdfPCell cell42 = new PdfPCell(new Phrase("PROFR.(A):  " + Apellidop3 + "  " + Apellidom3 + "  " + nombre3, cuerpo));
                        cell42.BorderWidth = 0;
                        cell42.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell42);

                        PdfPCell cell4311 = new PdfPCell(new Phrase("MES: ", cuerpo));
                        cell4311.BorderWidth = 0;
                        cell4311.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell4311);

                        PdfPCell cell43 = new PdfPCell(new Phrase("AÑO ESCOLAR: 2018 - 2019", cuerpo));
                        cell43.BorderWidth = 0;
                        cell43.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell43);

                        //Fila para dar espaciado entre tablas
                        PdfPCell cell112 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell112.BorderWidth = 0;
                        cell112.Colspan = 42;
                        table.AddCell(cell112);

                        //tabla de formacion academica            
                        PdfPCell cell431 = new PdfPCell(new Phrase("Núm.\nProgr.", cuerpo));
                        cell431.Rowspan = 3;//toma columnas
                        cell431.Rotation = 90;
                        cell431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell431);

                        PdfPCell cell1 = new PdfPCell(new Phrase("Sexo", cuerpo));
                        cell1.Rowspan = 3;//toma filas
                        cell1.PaddingLeft = 5f;
                        cell1.Rotation = 90;
                        cell1.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell1);

                        PdfPCell cell2 = new PdfPCell(new Phrase("Nombres", cuerpo));
                        cell2.PaddingTop = 5f;
                        cell2.Rowspan = 3;
                        cell2.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2);

                        PdfPCell cell2A = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2A.Rowspan = 3;
                        cell2A.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2A);

                        PdfPCell cell2B = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2B.Rowspan = 3;
                        cell2B.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2B);

                        PdfPCell cell2C = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2C.Rowspan = 3;
                        cell2C.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2C);

                        PdfPCell cell2D = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2D.Rowspan = 3;
                        cell2D.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2D);

                        PdfPCell cellC = new PdfPCell(new Phrase(" ", cuerpo));
                        cellC.Rowspan = 3;
                        cellC.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cellC);

                        PdfPCell cell25 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell25.Rowspan = 3;
                        cell25.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell25);

                        PdfPCell cell26 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell26.Rowspan = 3;
                        cell26.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell26);

                        PdfPCell cell12 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell12.Rowspan = 3;
                        cell12.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell12);

                        PdfPCell cell3 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell3.Rowspan = 3;
                        cell3.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell3);

                        PdfPCell cell13 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell13.Rowspan = 3;
                        cell13.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell13);

                        PdfPCell cell381 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell381.Rowspan = 3;
                        cell381.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell381);

                        PdfPCell cell19 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell19.Rowspan = 3;
                        cell19.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell19);

                        PdfPCell cell28 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell28.Rowspan = 3;
                        cell28.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell28);

                        PdfPCell cell8 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell8.Rowspan = 3;
                        cell8.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell8);

                        PdfPCell cell9 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell9.Rowspan = 3;
                        cell9.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell9);

                        PdfPCell cell69 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell69.Rowspan = 3;
                        cell69.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell69);

                        PdfPCell cell29 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell29.Rowspan = 3;
                        cell29.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell29);

                        PdfPCell cell30 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell30.Rowspan = 3;
                        cell30.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell30);

                        PdfPCell cell24 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell24.Rowspan = 3;
                        cell24.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell24);

                        PdfPCell cell4 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell4.Rowspan = 3;
                        cell4.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell4);

                        PdfPCell cell201 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell201.Rowspan = 3;
                        cell201.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell201);

                        PdfPCell cell202 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell202.Rowspan = 3;
                        cell202.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell202);

                        PdfPCell cell5 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell5.Rowspan = 3;
                        cell5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell5);

                        PdfPCell cell37 = new PdfPCell(new Phrase("Asisten", letmed));
                        cell37.Rowspan = 3;
                        cell37.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell37);

                        PdfPCell cell27 = new PdfPCell(new Phrase("Inasist", letmed));
                        cell27.Rowspan = 3;
                        cell27.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell27);

                        PdfPCell cell6 = new PdfPCell(new Phrase("RASGOS A EVALUAR", cuerpo));
                        cell6.Colspan = 4;
                        cell6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell6);

                        PdfPCell cell7 = new PdfPCell(new Phrase("EXAMENES", cuerpo));
                        cell7.Colspan = 8;
                        cell7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell7);

                        PdfPCell cell210 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell210.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell210);

                        PdfPCell cell1a5 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell1a5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell1a5);

                        PdfPCell cell1a6 = new PdfPCell(new Phrase("TRABAJO EN CLASES", letmed));
                        cell1a6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell1a6);

                        PdfPCell cell1a7 = new PdfPCell(new Phrase("VALORES", letmed));
                        cell1a7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell1a7);

                        PdfPCell cell211 = new PdfPCell(new Phrase("PARTICIPACIÓN", letmed));
                        cell211.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell211);

                        PdfPCell cell212 = new PdfPCell(new Phrase("TAREAS", letmed));
                        cell212.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell212);

                        PdfPCell cell214 = new PdfPCell(new Phrase("ESP", cuerpo));
                        cell214.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell214);

                        PdfPCell cell10 = new PdfPCell(new Phrase("MAT", cuerpo));
                        cell10.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell10);

                        PdfPCell cell220 = new PdfPCell(new Phrase("ING", cuerpo));
                        cell220.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell220);

                        PdfPCell cell2121 = new PdfPCell(new Phrase("NAT ", cuerpo));
                        cell2121.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2121);

                        PdfPCell cell1120 = new PdfPCell(new Phrase("LA ENT", cuerpo));
                        cell1120.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell1120);

                        PdfPCell cell2111 = new PdfPCell(new Phrase("CIV", cuerpo));
                        cell2111.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2111);

                        PdfPCell cell1110 = new PdfPCell(new Phrase("ED.ECO", cuerpo));
                        cell1110.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell1110);

                        PdfPCell cell221 = new PdfPCell(new Phrase("ART", cuerpo));
                        cell221.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell221);

                        PdfPCell cell222 = new PdfPCell(new Phrase("ED.FIS", cuerpo));
                        cell222.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell222);

                        PdfPCell cell101 = new PdfPCell(new Phrase("PROG. GRAL", cuerpo));
                        cell101.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell101);

                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");

                        //listas de alumnos
                        table.AddCell(" 1");
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[0], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0] + " " + Apellidom[0] + nombre[0], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");

                        table.AddCell(" 2");
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[1], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1] + " " + Apellidom[1] + nombre[1], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");

                        table.AddCell(" 3");
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[2], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2] + " " + Apellidom[2] + nombre[2], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");

                        table.AddCell(" 4");
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[3], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3] + " " + Apellidom[3] + nombre[3], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");

                        table.AddCell(" 5");
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[4], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4] + " " + Apellidom[4] + nombre[4], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");

                        table.AddCell(" 6");
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[5], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5] + " " + Apellidom[5] + nombre[5], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");

                        table.AddCell(" 7");
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[6], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6] + " " + Apellidom[6] + nombre[6], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");

                        table.AddCell(" 8");
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[7], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7] + " " + Apellidom[7] + nombre[7], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");

                        table.AddCell(" 9");
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[8], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8] + " " + Apellidom[8] + nombre[8], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[9], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9] + " " + Apellidom[9] + nombre[9], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[10], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10] + " " + Apellidom[10] + nombre[10], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[11], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11] + " " + Apellidom[11] + nombre[11], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[12], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12] + " " + Apellidom[12] + nombre[12], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[13], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13] + " " + Apellidom[13] + nombre[13], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[14], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14] + " " + Apellidom[14] + nombre[14], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[15], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15] + " " + Apellidom[15] + nombre[15], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[16], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16] + " " + Apellidom[16] + nombre[16], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[17], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17] + " " + Apellidom[17] + nombre[17], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[18], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase(Apellidop[18] + " " + Apellidom[18] + nombre[18], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[19], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19] + " " + Apellidom[19] + nombre[19], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[20], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20] + " " + Apellidom[20] + nombre[20], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[21], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21] + " " + Apellidom[21] + nombre[21], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[22], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22] + " " + Apellidom[22] + nombre[22], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[23], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23] + " " + Apellidom[23] + nombre[23], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
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
                        table.AddCell(new PdfPCell(new Phrase("" + Genero[24], cuerpo)));
                        table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24] + " " + Apellidom[24] + nombre[24], letmed)));
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");
                        table.AddCell(" ");



                        //Fila para dar espaciado entre tablas
                        PdfPCell cell113 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell113.BorderWidth = 0;
                        cell113.Colspan = 43;
                        table.AddCell(cell113);

                        PdfPCell cell140 = new PdfPCell(new Phrase(" "));
                        cell140.Colspan = 3;//toma columnas
                        cell140.BorderWidth = 0;
                        cell140.Rowspan = 5;//toma filas
                                            //cell140.BorderWidth = 0;
                        table.AddCell(cell140);

                        PdfPCell cell230 = new PdfPCell(new Phrase("HOMBRES: ", cuerpo));
                        cell230.Colspan = 10;//toma columnas
                        cell230.BorderWidth = 0;
                        cell230.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell230);

                        PdfPCell cell1012 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell1012.Colspan = 8;//toma columnas
                        cell1012.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell1012);

                        PdfPCell celld = new PdfPCell(new Phrase("EXIST.", cuerpo));
                        celld.Colspan = 7;//toma columnas
                        celld.BorderWidth = 0;
                        celld.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(celld);

                        PdfPCell cell231 = new PdfPCell(new Phrase("APROB.", cuerpo));
                        cell231.Colspan = 2;
                        cell231.BorderWidth = 0;
                        cell231.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell231);

                        PdfPCell cell232 = new PdfPCell(new Phrase("REPROB.", cuerpo));
                        cell232.Colspan = 2;
                        cell232.BorderWidth = 0;
                        cell232.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell232);

                        PdfPCell cell238 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell238.Colspan = 11;
                        cell238.BorderWidth = 0;
                        cell238.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell238);

                        PdfPCell cell77 = new PdfPCell(new Phrase("", cuerpo));
                        cell77.Colspan = 18;
                        cell77.BorderWidth = 0;
                        cell77.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell77);

                        PdfPCell cell240 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell240.Colspan = 7;
                        cell240.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell240);

                        PdfPCell cell241 = new PdfPCell(new Phrase("", cuerpo));
                        cell241.Colspan = 2;
                        cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell241);

                        PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell242.Colspan = 2;
                        cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell242);

                        PdfPCell cell207 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell207.Colspan = 3;
                        cell207.BorderWidth = 0;
                        cell207.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell207);

                        PdfPCell cell219 = new PdfPCell(new Phrase("PROMEDIO ", cuerpo));
                        cell219.Colspan = 4;
                        cell219.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell219);

                        PdfPCell cell250 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell250.Colspan = 3;
                        cell250.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell250);

                        PdfPCell cell236 = new PdfPCell(new Phrase("MUJERES: ", cuerpo));
                        cell236.Colspan = 10;//toma columnas
                        cell236.BorderWidth = 0;
                        cell236.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell236);

                        PdfPCell cell1014 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell1014.Colspan = 8;//toma columnas
                        cell1014.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell1014);

                        PdfPCell cell12d = new PdfPCell(new Phrase(" ", cuerpo));
                        cell12d.Colspan = 7;//toma columnas
                        cell12d.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell12d);

                        PdfPCell cell2331 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2331.Colspan = 2;
                        cell2331.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2331);

                        PdfPCell cell2342 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2342.Colspan = 2;
                        cell2342.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2342);

                        PdfPCell cell2017 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2017.Colspan = 3;
                        cell2017.BorderWidth = 0;
                        cell2017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2017);

                        PdfPCell cell2119 = new PdfPCell(new Phrase("% DE APROBADOS ", cuerpo));
                        cell2119.Colspan = 4;
                        cell2119.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2119);

                        PdfPCell cell2150 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2150.Colspan = 3;
                        cell2150.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2150);

                        PdfPCell cell177 = new PdfPCell(new Phrase("", cuerpo));
                        cell177.Colspan = 18;
                        cell177.BorderWidth = 0;
                        cell177.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell177);

                        PdfPCell cell2401 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2401.Colspan = 7;
                        cell2401.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2401);

                        PdfPCell cell2431 = new PdfPCell(new Phrase("", cuerpo));
                        cell2431.Colspan = 2;
                        cell2431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2431);

                        PdfPCell cell2432 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2432.Colspan = 2;
                        cell2432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2432);

                        PdfPCell cell2328 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell2328.Colspan = 11;
                        cell2328.BorderWidth = 0;
                        cell2328.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell2328);

                        PdfPCell cell137 = new PdfPCell(new Phrase("FIRMA DEL MAESTRO(A): ", cuerpo));
                        cell137.Colspan = 10;
                        cell137.BorderWidth = 0;
                        cell137.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell137);

                        PdfPCell cell1054 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell1054.Colspan = 15;//toma columnas
                        cell1054.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell1054);

                        PdfPCell cell307 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell307.Colspan = 2;
                        cell307.BorderWidth = 0;
                        cell307.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell307);

                        PdfPCell cell432 = new PdfPCell(new Phrase(" ", cuerpo));
                        cell432.BorderWidth = 0;
                        cell432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell432);

                        PdfPCell cell228 = new PdfPCell(new Phrase("Acapulco Gro., a ________________ de ________________ del 20_____", cuerpo));
                        cell228.Colspan = 11;
                        cell228.BorderWidth = 0;
                        cell228.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                        table.AddCell(cell228);

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
                    else
                    {
                        if (Convert.ToInt32(sesion.Grado) == 4)
                        {
                            // Creamos el documento con el tamaño de página tradicional
                            Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                            string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
                            if (!Directory.Exists(folderPath))// pregunt si no existe
                            {
                                Directory.CreateDirectory(folderPath); // si no existe lo crea
                            }
                            // Creamos el documento con el tamaño de página tradicional
                            FileStream stream = new FileStream(folderPath + "Lista-Asistencia4.pdf", FileMode.Create);
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
                            PdfPTable table = new PdfPTable(43);
                            table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                            iTextSharp.text.Image logoEsc = iTextSharp.text.Image.GetInstance("../../../logo-esc.png");
                            logoEsc.BorderWidth = 0;
                            logoEsc.ScaleAbsolute(120, 70);
                            iTextSharp.text.Image logoSep = iTextSharp.text.Image.GetInstance("../../../logo.png");
                            logoSep.ScaleAbsolute(150, 60);


                            // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                            // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                            float[] Celdas = { 0.25f, 0.25f, 1.50f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.50f, 0.50f, 0.50f, 0.50f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.20f, 0.45f };

                            // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                            table.SetWidths(Celdas);

                            //encabezado

                            PdfPCell cell40 = new PdfPCell(new Phrase(" "));
                            cell40.Colspan = 2;//toma columnas
                            cell40.Rowspan = 4;//toma filas
                            cell40.BorderWidth = 0;
                            table.AddCell(cell40);

                            PdfPCell cell39 = new PdfPCell(new Phrase("GRUPO: "+ sesion.Grado + "  A", cuerpo));
                            cell39.BorderWidth = 0;
                            cell39.PaddingTop = 5f;
                            cell39.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell39);

                            PdfPCell cell53 = new PdfPCell(new Phrase(" "));
                            cell53.Colspan = 7;//toma columnas
                            cell53.Rowspan = 4;//toma filas
                            cell53.BorderWidth = 0;
                            table.AddCell(cell53);

                            PdfPCell cell1z = new PdfPCell(new Phrase("INSTITTUTO RODOLFO NERI VELA\n\n PRESTIGIO EN TU CONOCIMIENTO\n\n Vicente Guerrero 49, Barrios Historicos, 39540.", titulos));
                            cell1z.Colspan = 24;//toma columnas
                            cell1z.Rowspan = 4;//toma filas
                            cell1z.BorderWidth = 0;
                            cell1z.PaddingTop = 5f;
                            cell1z.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1z);

                            PdfPCell cell44 = new PdfPCell(new Phrase(" "));
                            cell44.Colspan = 9;//toma columnas
                            cell44.Rowspan = 4;
                            cell44.BorderWidth = 0;
                            table.AddCell(cell44);

                            PdfPCell cell42 = new PdfPCell(new Phrase("PROFR.(A):  "+ Apellidop4 +"  "+ Apellidom4 +"  "+ nombre4, cuerpo));
                            cell42.BorderWidth = 0;
                            cell42.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell42);

                            PdfPCell cell4311 = new PdfPCell(new Phrase("MES: ", cuerpo));
                            cell4311.BorderWidth = 0;
                            cell4311.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell4311);

                            PdfPCell cell43 = new PdfPCell(new Phrase("AÑO ESCOLAR: 2018 - 2019", cuerpo));
                            cell43.BorderWidth = 0;
                            cell43.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell43);

                            //Fila para dar espaciado entre tablas
                            PdfPCell cell112 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell112.BorderWidth = 0;
                            cell112.Colspan = 43;
                            table.AddCell(cell112);

                            //tabla de formacion academica            
                            PdfPCell cell431 = new PdfPCell(new Phrase("Núm.\nProgr.", cuerpo));
                            cell431.Rowspan = 3;//toma columnas
                            cell431.Rotation = 90;
                            cell431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell431);

                            PdfPCell cell1 = new PdfPCell(new Phrase("Sexo", cuerpo));
                            cell1.Rowspan = 3;//toma filas
                            cell1.PaddingLeft = 5f;
                            cell1.Rotation = 90;
                            cell1.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1);

                            PdfPCell cell2 = new PdfPCell(new Phrase("Nombres", cuerpo));
                            cell2.PaddingTop = 5f;
                            cell2.Rowspan = 3;
                            cell2.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2);

                            PdfPCell cell2A = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2A.Rowspan = 3;
                            cell2A.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2A);

                            PdfPCell cell2B = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2B.Rowspan = 3;
                            cell2B.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2B);

                            PdfPCell cell2C = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2C.Rowspan = 3;
                            cell2C.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2C);

                            PdfPCell cell2D = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2D.Rowspan = 3;
                            cell2D.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2D);

                            PdfPCell cellC = new PdfPCell(new Phrase(" ", cuerpo));
                            cellC.Rowspan = 3;
                            cellC.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cellC);

                            PdfPCell cell25 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell25.Rowspan = 3;
                            cell25.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell25);

                            PdfPCell cell26 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell26.Rowspan = 3;
                            cell26.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell26);

                            PdfPCell cell12 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell12.Rowspan = 3;
                            cell12.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell12);

                            PdfPCell cell3 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell3.Rowspan = 3;
                            cell3.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell3);

                            PdfPCell cell13 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell13.Rowspan = 3;
                            cell13.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell13);

                            PdfPCell cell381 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell381.Rowspan = 3;
                            cell381.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell381);

                            PdfPCell cell19 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell19.Rowspan = 3;
                            cell19.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell19);

                            PdfPCell cell28 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell28.Rowspan = 3;
                            cell28.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell28);

                            PdfPCell cell8 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell8.Rowspan = 3;
                            cell8.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell8);

                            PdfPCell cell9 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell9.Rowspan = 3;
                            cell9.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell9);

                            PdfPCell cell69 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell69.Rowspan = 3;
                            cell69.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell69);

                            PdfPCell cell29 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell29.Rowspan = 3;
                            cell29.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell29);

                            PdfPCell cell30 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell30.Rowspan = 3;
                            cell30.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell30);

                            PdfPCell cell24 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell24.Rowspan = 3;
                            cell24.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell24);

                            PdfPCell cell4 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell4.Rowspan = 3;
                            cell4.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell4);

                            PdfPCell cell201 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell201.Rowspan = 3;
                            cell201.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell201);

                            PdfPCell cell202 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell202.Rowspan = 3;
                            cell202.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell202);

                            PdfPCell cell5 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell5.Rowspan = 3;
                            cell5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell5);

                            PdfPCell cell37 = new PdfPCell(new Phrase("Asisten", letmed));
                            cell37.Rowspan = 3;
                            cell37.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell37);

                            PdfPCell cell27 = new PdfPCell(new Phrase("Inasist", letmed));
                            cell27.Rowspan = 3;
                            cell27.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell27);

                            PdfPCell cell6 = new PdfPCell(new Phrase("RASGOS A EVALUAR", cuerpo));
                            cell6.Colspan = 4;
                            cell6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell6);

                            PdfPCell cell7 = new PdfPCell(new Phrase("EXAMENES", cuerpo));
                            cell7.Colspan = 9;
                            cell7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell7);

                            PdfPCell cell210 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell210.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell210);

                            PdfPCell cell1a5 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell1a5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1a5);

                            PdfPCell cell1a6 = new PdfPCell(new Phrase("TRABAJO EN CLASES", letmed));
                            cell1a6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1a6);

                            PdfPCell cell1a7 = new PdfPCell(new Phrase("VALORES", letmed));
                            cell1a7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1a7);

                            PdfPCell cell211 = new PdfPCell(new Phrase("PARTICIPACIÓN", letmed));
                            cell211.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell211);

                            PdfPCell cell212 = new PdfPCell(new Phrase("TAREAS", letmed));
                            cell212.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell212);

                            PdfPCell cell214 = new PdfPCell(new Phrase("ESP", cuerpo));
                            cell214.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell214);

                            PdfPCell cell10 = new PdfPCell(new Phrase("MAT", cuerpo));
                            cell10.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell10);

                            PdfPCell cell220 = new PdfPCell(new Phrase("ING", cuerpo));
                            cell220.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell220);

                            PdfPCell cell2121 = new PdfPCell(new Phrase("NAT ", cuerpo));
                            cell2121.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2121);

                            PdfPCell cell1120 = new PdfPCell(new Phrase("GEO", cuerpo));
                            cell1120.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1120);

                            PdfPCell cell1121 = new PdfPCell(new Phrase("HIS", cuerpo));
                            cell1121.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1121);

                            PdfPCell cell2111 = new PdfPCell(new Phrase("CIV", cuerpo));
                            cell2111.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2111);

                            PdfPCell cell1110 = new PdfPCell(new Phrase("ED.ECO", cuerpo));
                            cell1110.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1110);

                            PdfPCell cell221 = new PdfPCell(new Phrase("ART", cuerpo));
                            cell221.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell221);

                            PdfPCell cell222 = new PdfPCell(new Phrase("ED.FIS", cuerpo));
                            cell222.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell222);

                            PdfPCell cell101 = new PdfPCell(new Phrase("PROG. GRAL", cuerpo));
                            cell101.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell101);

                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");

                            //listas de alumnos
                            table.AddCell(" 1");
                            table.AddCell(new PdfPCell(new Phrase(""+Genero[0], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0] + " " + Apellidom[0] + nombre[0], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");

                            table.AddCell(" 2");
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[1], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1] + " " + Apellidom[1] + nombre[1], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");

                            table.AddCell(" 3");
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[2], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2] + " " + Apellidom[2] + nombre[2], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");

                            table.AddCell(" 4");
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[3], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3] + " " + Apellidom[3] + nombre[3], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");

                            table.AddCell(" 5");
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[4], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4] + " " + Apellidom[4] + nombre[4], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");

                            table.AddCell(" 6");
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[5], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5] + " " + Apellidom[5] + nombre[5], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");

                            table.AddCell(" 7");
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[6], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6] + " " + Apellidom[6] + nombre[6], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");

                            table.AddCell(" 8");
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[7], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7] + " " + Apellidom[7] + nombre[7], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");

                            table.AddCell(" 9");
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[8], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8] + " " + Apellidom[8] + nombre[8], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[9], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9] + " " + Apellidom[9] + nombre[9], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[10], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10] + " " + Apellidom[10] + nombre[10], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[11], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11] + " " + Apellidom[11] + nombre[11], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[12], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12] + " " + Apellidom[12] + nombre[12], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[13], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13] + " " + Apellidom[13] + nombre[13], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[14], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14] + " " + Apellidom[14] + nombre[14], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[15], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15] + " " + Apellidom[15] + nombre[15], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[16], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16] + " " + Apellidom[16] + nombre[16], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[17], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17] + " " + Apellidom[17] + nombre[17], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[18], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase( Apellidop[18] + " "+ Apellidom[18] + nombre[18], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[19], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19] + " " + Apellidom[19] + nombre[19], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[20], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20] + " " + Apellidom[20] + nombre[20], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[21], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21] + " " + Apellidom[21] + nombre[21], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[22], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22] + " " + Apellidom[22] + nombre[22], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[23], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23] + " " + Apellidom[23] + nombre[23], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
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
                            table.AddCell(new PdfPCell(new Phrase("" + Genero[24], cuerpo)));
                            table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24] + " " + Apellidom[24] + nombre[24], letmed)));
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");
                            table.AddCell(" ");


                            //Fila para dar espaciado entre tablas
                            PdfPCell cell113 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell113.BorderWidth = 0;
                            cell113.Colspan = 43;
                            table.AddCell(cell113);

                            PdfPCell cell140 = new PdfPCell(new Phrase(" "));
                            cell140.Colspan = 3;//toma columnas
                            cell140.BorderWidth = 0;
                            cell140.Rowspan = 5;//toma filas
                                                //cell140.BorderWidth = 0;
                            table.AddCell(cell140);

                            PdfPCell cell230 = new PdfPCell(new Phrase("HOMBRES: ", cuerpo));
                            cell230.Colspan = 10;//toma columnas
                            cell230.BorderWidth = 0;
                            cell230.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell230);

                            PdfPCell cell1012 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell1012.Colspan = 8;//toma columnas
                            cell1012.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1012);

                            PdfPCell celld = new PdfPCell(new Phrase("EXIST.", cuerpo));
                            celld.Colspan = 7;//toma columnas
                            celld.BorderWidth = 0;
                            celld.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(celld);

                            PdfPCell cell231 = new PdfPCell(new Phrase("APROB.", cuerpo));
                            cell231.Colspan = 2;
                            cell231.BorderWidth = 0;
                            cell231.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell231);

                            PdfPCell cell232 = new PdfPCell(new Phrase("REPROB.", cuerpo));
                            cell232.Colspan = 2;
                            cell232.BorderWidth = 0;
                            cell232.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell232);

                            PdfPCell cell238 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell238.Colspan = 12;
                            cell238.BorderWidth = 0;
                            cell238.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell238);

                            PdfPCell cell77 = new PdfPCell(new Phrase("", cuerpo));
                            cell77.Colspan = 18;
                            cell77.BorderWidth = 0;
                            cell77.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell77);

                            PdfPCell cell240 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell240.Colspan = 7;
                            cell240.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell240);

                            PdfPCell cell241 = new PdfPCell(new Phrase("", cuerpo));
                            cell241.Colspan = 2;
                            cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell241);

                            PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell242.Colspan = 2;
                            cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell242);

                            PdfPCell cell207 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell207.Colspan = 4;
                            cell207.BorderWidth = 0;
                            cell207.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell207);

                            PdfPCell cell219 = new PdfPCell(new Phrase("PROMEDIO ", cuerpo));
                            cell219.Colspan = 4;
                            cell219.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell219);

                            PdfPCell cell250 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell250.Colspan = 3;
                            cell250.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell250);

                            PdfPCell cell236 = new PdfPCell(new Phrase("MUJERES: ", cuerpo));
                            cell236.Colspan = 10;//toma columnas
                            cell236.BorderWidth = 0;
                            cell236.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell236);

                            PdfPCell cell1014 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell1014.Colspan = 8;//toma columnas
                            cell1014.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1014);

                            PdfPCell cell12d = new PdfPCell(new Phrase(" ", cuerpo));
                            cell12d.Colspan = 7;//toma columnas
                            cell12d.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell12d);

                            PdfPCell cell2331 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2331.Colspan = 2;
                            cell2331.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2331);

                            PdfPCell cell2342 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2342.Colspan = 2;
                            cell2342.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2342);

                            PdfPCell cell2017 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2017.Colspan = 4;
                            cell2017.BorderWidth = 0;
                            cell2017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2017);

                            PdfPCell cell2119 = new PdfPCell(new Phrase("% DE APROBADOS ", cuerpo));
                            cell2119.Colspan = 4;
                            cell2119.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2119);

                            PdfPCell cell2150 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2150.Colspan = 3;
                            cell2150.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2150);

                            PdfPCell cell177 = new PdfPCell(new Phrase("", cuerpo));
                            cell177.Colspan = 18;
                            cell177.BorderWidth = 0;
                            cell177.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell177);

                            PdfPCell cell2401 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2401.Colspan = 7;
                            cell2401.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2401);

                            PdfPCell cell2431 = new PdfPCell(new Phrase("", cuerpo));
                            cell2431.Colspan = 2;
                            cell2431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2431);

                            PdfPCell cell2432 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2432.Colspan = 2;
                            cell2432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2432);

                            PdfPCell cell2328 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell2328.Colspan = 11;
                            cell2328.BorderWidth = 0;
                            cell2328.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell2328);

                            PdfPCell cell137 = new PdfPCell(new Phrase("FIRMA DEL MAESTRO(A): ", cuerpo));
                            cell137.Colspan = 10;
                            cell137.BorderWidth = 0;
                            cell137.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell137);

                            PdfPCell cell1054 = new PdfPCell(new Phrase(" ", cuerpo));
                            cell1054.Colspan = 15;//toma columnas
                            cell1054.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell1054);

                            PdfPCell cell228 = new PdfPCell(new Phrase("Acapulco Gro., a ________________ de ________________ del 20_____", cuerpo));
                            cell228.Colspan = 11;
                            cell228.BorderWidth = 0;
                            cell228.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                            table.AddCell(cell228);

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
                        else
                        {
                            if (Convert.ToInt32(sesion.Grado) == 5)
                            {
                                // Creamos el documento con el tamaño de página tradicional
                                Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                                string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
                                if (!Directory.Exists(folderPath))// pregunt si no existe
                                {
                                    Directory.CreateDirectory(folderPath); // si no existe lo crea
                                }
                                // Creamos el documento con el tamaño de página tradicional
                                FileStream stream = new FileStream(folderPath + "Lista-Asistencia5.pdf", FileMode.Create);
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
                                PdfPTable table = new PdfPTable(43);
                                table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                                iTextSharp.text.Image logoEsc = iTextSharp.text.Image.GetInstance("../../../logo-esc.png");
                                logoEsc.BorderWidth = 0;
                                logoEsc.ScaleAbsolute(120, 70);
                                iTextSharp.text.Image logoSep = iTextSharp.text.Image.GetInstance("../../../logo.png");
                                logoSep.ScaleAbsolute(150, 60);


                                // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                                // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                                float[] Celdas = { 0.25f, 0.25f, 1.50f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.50f, 0.50f, 0.50f, 0.50f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.20f, 0.45f };

                                // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                                table.SetWidths(Celdas);

                                //encabezado

                                PdfPCell cell40 = new PdfPCell(new Phrase(" "));
                                cell40.Colspan = 2;//toma columnas
                                cell40.Rowspan = 4;//toma filas
                                cell40.BorderWidth = 0;
                                table.AddCell(cell40);

                                PdfPCell cell39 = new PdfPCell(new Phrase("GRUPO: " + sesion.Grado + "  A", cuerpo));
                                cell39.BorderWidth = 0;
                                cell39.PaddingTop = 5f;
                                cell39.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell39);

                                PdfPCell cell53 = new PdfPCell(new Phrase(" "));
                                cell53.Colspan = 7;//toma columnas
                                cell53.Rowspan = 4;//toma filas
                                cell53.BorderWidth = 0;
                                table.AddCell(cell53);

                                PdfPCell cell1z = new PdfPCell(new Phrase("INSTITTUTO RODOLFO NERI VELA\n\n PRESTIGIO EN TU CONOCIMIENTO\n\n Vicente Guerrero 49, Barrios Historicos, 39540.", titulos));
                                cell1z.Colspan = 24;//toma columnas
                                cell1z.Rowspan = 4;//toma filas
                                cell1z.BorderWidth = 0;
                                cell1z.PaddingTop = 5f;
                                cell1z.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1z);

                                PdfPCell cell44 = new PdfPCell(new Phrase(" "));
                                cell44.Colspan = 9;//toma columnas
                                cell44.Rowspan = 4;
                                cell44.BorderWidth = 0;
                                table.AddCell(cell44);

                                PdfPCell cell42 = new PdfPCell(new Phrase("PROFR.(A):  " + Apellidop5 + "  " + Apellidom5 + "  " + nombre5, cuerpo));
                                cell42.BorderWidth = 0;
                                cell42.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell42);

                                PdfPCell cell4311 = new PdfPCell(new Phrase("MES: ", cuerpo));
                                cell4311.BorderWidth = 0;
                                cell4311.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell4311);

                                PdfPCell cell43 = new PdfPCell(new Phrase("AÑO ESCOLAR: 2018 - 2019", cuerpo));
                                cell43.BorderWidth = 0;
                                cell43.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell43);

                                //Fila para dar espaciado entre tablas
                                PdfPCell cell112 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell112.BorderWidth = 0;
                                cell112.Colspan = 43;
                                table.AddCell(cell112);

                                //tabla de formacion academica            
                                PdfPCell cell431 = new PdfPCell(new Phrase("Núm.\nProgr.", cuerpo));
                                cell431.Rowspan = 3;//toma columnas
                                cell431.Rotation = 90;
                                cell431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell431);

                                PdfPCell cell1 = new PdfPCell(new Phrase("Sexo", cuerpo));
                                cell1.Rowspan = 3;//toma filas
                                cell1.PaddingLeft = 5f;
                                cell1.Rotation = 90;
                                cell1.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1);

                                PdfPCell cell2 = new PdfPCell(new Phrase("Nombres", cuerpo));
                                cell2.PaddingTop = 5f;
                                cell2.Rowspan = 3;
                                cell2.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2);

                                PdfPCell cell2A = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2A.Rowspan = 3;
                                cell2A.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2A);

                                PdfPCell cell2B = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2B.Rowspan = 3;
                                cell2B.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2B);

                                PdfPCell cell2C = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2C.Rowspan = 3;
                                cell2C.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2C);

                                PdfPCell cell2D = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2D.Rowspan = 3;
                                cell2D.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2D);

                                PdfPCell cellC = new PdfPCell(new Phrase(" ", cuerpo));
                                cellC.Rowspan = 3;
                                cellC.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cellC);

                                PdfPCell cell25 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell25.Rowspan = 3;
                                cell25.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell25);

                                PdfPCell cell26 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell26.Rowspan = 3;
                                cell26.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell26);

                                PdfPCell cell12 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell12.Rowspan = 3;
                                cell12.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell12);

                                PdfPCell cell3 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell3.Rowspan = 3;
                                cell3.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell3);

                                PdfPCell cell13 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell13.Rowspan = 3;
                                cell13.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell13);

                                PdfPCell cell381 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell381.Rowspan = 3;
                                cell381.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell381);

                                PdfPCell cell19 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell19.Rowspan = 3;
                                cell19.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell19);

                                PdfPCell cell28 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell28.Rowspan = 3;
                                cell28.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell28);

                                PdfPCell cell8 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell8.Rowspan = 3;
                                cell8.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell8);

                                PdfPCell cell9 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell9.Rowspan = 3;
                                cell9.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell9);

                                PdfPCell cell69 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell69.Rowspan = 3;
                                cell69.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell69);

                                PdfPCell cell29 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell29.Rowspan = 3;
                                cell29.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell29);

                                PdfPCell cell30 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell30.Rowspan = 3;
                                cell30.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell30);

                                PdfPCell cell24 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell24.Rowspan = 3;
                                cell24.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell24);

                                PdfPCell cell4 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell4.Rowspan = 3;
                                cell4.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell4);

                                PdfPCell cell201 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell201.Rowspan = 3;
                                cell201.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell201);

                                PdfPCell cell202 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell202.Rowspan = 3;
                                cell202.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell202);

                                PdfPCell cell5 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell5.Rowspan = 3;
                                cell5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell5);

                                PdfPCell cell37 = new PdfPCell(new Phrase("Asisten", letmed));
                                cell37.Rowspan = 3;
                                cell37.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell37);

                                PdfPCell cell27 = new PdfPCell(new Phrase("Inasist", letmed));
                                cell27.Rowspan = 3;
                                cell27.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell27);

                                PdfPCell cell6 = new PdfPCell(new Phrase("RASGOS A EVALUAR", cuerpo));
                                cell6.Colspan = 4;
                                cell6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell6);

                                PdfPCell cell7 = new PdfPCell(new Phrase("EXAMENES", cuerpo));
                                cell7.Colspan = 9;
                                cell7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell7);

                                PdfPCell cell210 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell210.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell210);

                                PdfPCell cell1a5 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell1a5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1a5);

                                PdfPCell cell1a6 = new PdfPCell(new Phrase("TRABAJO EN CLASES", letmed));
                                cell1a6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1a6);

                                PdfPCell cell1a7 = new PdfPCell(new Phrase("VALORES", letmed));
                                cell1a7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1a7);

                                PdfPCell cell211 = new PdfPCell(new Phrase("PARTICIPACIÓN", letmed));
                                cell211.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell211);

                                PdfPCell cell212 = new PdfPCell(new Phrase("TAREAS", letmed));
                                cell212.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell212);

                                PdfPCell cell214 = new PdfPCell(new Phrase("ESP", cuerpo));
                                cell214.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell214);

                                PdfPCell cell10 = new PdfPCell(new Phrase("MAT", cuerpo));
                                cell10.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell10);

                                PdfPCell cell220 = new PdfPCell(new Phrase("ING", cuerpo));
                                cell220.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell220);

                                PdfPCell cell2121 = new PdfPCell(new Phrase("NAT ", cuerpo));
                                cell2121.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2121);

                                PdfPCell cell1120 = new PdfPCell(new Phrase("GEO", cuerpo));
                                cell1120.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1120);

                                PdfPCell cell1121 = new PdfPCell(new Phrase("HIS", cuerpo));
                                cell1121.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1121);

                                PdfPCell cell2111 = new PdfPCell(new Phrase("CIV", cuerpo));
                                cell2111.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2111);

                                PdfPCell cell1110 = new PdfPCell(new Phrase("ED.ECO", cuerpo));
                                cell1110.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1110);

                                PdfPCell cell221 = new PdfPCell(new Phrase("ART", cuerpo));
                                cell221.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell221);

                                PdfPCell cell222 = new PdfPCell(new Phrase("ED.FIS", cuerpo));
                                cell222.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell222);

                                PdfPCell cell101 = new PdfPCell(new Phrase("PROG. GRAL", cuerpo));
                                cell101.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell101);

                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");

                                //listas de alumnos
                                table.AddCell(" 1");
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[0], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0] + " " + Apellidom[0] + nombre[0], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");

                                table.AddCell(" 2");
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[1], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1] + " " + Apellidom[1] + nombre[1], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");

                                table.AddCell(" 3");
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[2], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2] + " " + Apellidom[2] + nombre[2], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");

                                table.AddCell(" 4");
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[3], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3] + " " + Apellidom[3] + nombre[3], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");

                                table.AddCell(" 5");
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[4], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4] + " " + Apellidom[4] + nombre[4], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");

                                table.AddCell(" 6");
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[5], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5] + " " + Apellidom[5] + nombre[5], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");

                                table.AddCell(" 7");
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[6], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6] + " " + Apellidom[6] + nombre[6], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");

                                table.AddCell(" 8");
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[7], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7] + " " + Apellidom[7] + nombre[7], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");

                                table.AddCell(" 9");
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[8], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8] + " " + Apellidom[8] + nombre[8], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[9], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9] + " " + Apellidom[9] + nombre[9], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[10], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10] + " " + Apellidom[10] + nombre[10], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[11], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11] + " " + Apellidom[11] + nombre[11], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[12], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12] + " " + Apellidom[12] + nombre[12], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[13], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13] + " " + Apellidom[13] + nombre[13], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[14], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14] + " " + Apellidom[14] + nombre[14], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[15], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15] + " " + Apellidom[15] + nombre[15], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[16], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16] + " " + Apellidom[16] + nombre[16], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[17], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17] + " " + Apellidom[17] + nombre[17], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[18], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase(Apellidop[18] + " " + Apellidom[18] + nombre[18], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[19], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19] + " " + Apellidom[19] + nombre[19], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[20], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20] + " " + Apellidom[20] + nombre[20], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[21], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21] + " " + Apellidom[21] + nombre[21], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[22], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22] + " " + Apellidom[22] + nombre[22], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[23], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23] + " " + Apellidom[23] + nombre[23], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
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
                                table.AddCell(new PdfPCell(new Phrase("" + Genero[24], cuerpo)));
                                table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24] + " " + Apellidom[24] + nombre[24], letmed)));
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");
                                table.AddCell(" ");


                                //Fila para dar espaciado entre tablas
                                PdfPCell cell113 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell113.BorderWidth = 0;
                                cell113.Colspan = 43;
                                table.AddCell(cell113);

                                PdfPCell cell140 = new PdfPCell(new Phrase(" "));
                                cell140.Colspan = 3;//toma columnas
                                cell140.BorderWidth = 0;
                                cell140.Rowspan = 5;//toma filas
                                                    //cell140.BorderWidth = 0;
                                table.AddCell(cell140);

                                PdfPCell cell230 = new PdfPCell(new Phrase("HOMBRES: ", cuerpo));
                                cell230.Colspan = 10;//toma columnas
                                cell230.BorderWidth = 0;
                                cell230.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell230);

                                PdfPCell cell1012 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell1012.Colspan = 8;//toma columnas
                                cell1012.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1012);

                                PdfPCell celld = new PdfPCell(new Phrase("EXIST.", cuerpo));
                                celld.Colspan = 7;//toma columnas
                                celld.BorderWidth = 0;
                                celld.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(celld);

                                PdfPCell cell231 = new PdfPCell(new Phrase("APROB.", cuerpo));
                                cell231.Colspan = 2;
                                cell231.BorderWidth = 0;
                                cell231.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell231);

                                PdfPCell cell232 = new PdfPCell(new Phrase("REPROB.", cuerpo));
                                cell232.Colspan = 2;
                                cell232.BorderWidth = 0;
                                cell232.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell232);

                                PdfPCell cell238 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell238.Colspan = 11;
                                cell238.BorderWidth = 0;
                                cell238.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell238);

                                PdfPCell cell77 = new PdfPCell(new Phrase("", cuerpo));
                                cell77.Colspan = 18;
                                cell77.BorderWidth = 0;
                                cell77.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell77);

                                PdfPCell cell240 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell240.Colspan = 7;
                                cell240.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell240);

                                PdfPCell cell241 = new PdfPCell(new Phrase("", cuerpo));
                                cell241.Colspan = 2;
                                cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell241);

                                PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell242.Colspan = 2;
                                cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell242);

                                PdfPCell cell207 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell207.Colspan = 3;
                                cell207.BorderWidth = 0;
                                cell207.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell207);

                                PdfPCell cell219 = new PdfPCell(new Phrase("PROMEDIO ", cuerpo));
                                cell219.Colspan = 5;
                                cell219.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell219);

                                PdfPCell cell250 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell250.Colspan = 3;
                                cell250.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell250);

                                PdfPCell cell236 = new PdfPCell(new Phrase("MUJERES: ", cuerpo));
                                cell236.Colspan = 10;//toma columnas
                                cell236.BorderWidth = 0;
                                cell236.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell236);

                                PdfPCell cell1014 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell1014.Colspan = 8;//toma columnas
                                cell1014.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1014);

                                PdfPCell cell12d = new PdfPCell(new Phrase(" ", cuerpo));
                                cell12d.Colspan = 7;//toma columnas
                                cell12d.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell12d);

                                PdfPCell cell2331 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2331.Colspan = 2;
                                cell2331.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2331);

                                PdfPCell cell2342 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2342.Colspan = 2;
                                cell2342.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2342);

                                PdfPCell cell2017 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2017.Colspan = 3;
                                cell2017.BorderWidth = 0;
                                cell2017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2017);

                                PdfPCell cell2119 = new PdfPCell(new Phrase("% DE APROBADOS ", cuerpo));
                                cell2119.Colspan = 5;
                                cell2119.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2119);

                                PdfPCell cell2150 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2150.Colspan = 3;
                                cell2150.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2150);

                                PdfPCell cell177 = new PdfPCell(new Phrase("", cuerpo));
                                cell177.Colspan = 18;
                                cell177.BorderWidth = 0;
                                cell177.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell177);

                                PdfPCell cell2401 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2401.Colspan = 7;
                                cell2401.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2401);

                                PdfPCell cell2431 = new PdfPCell(new Phrase("", cuerpo));
                                cell2431.Colspan = 2;
                                cell2431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2431);

                                PdfPCell cell2432 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2432.Colspan = 2;
                                cell2432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2432);

                                PdfPCell cell2328 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell2328.Colspan = 11;
                                cell2328.BorderWidth = 0;
                                cell2328.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell2328);

                                PdfPCell cell137 = new PdfPCell(new Phrase("FIRMA DEL MAESTRO(A): ", cuerpo));
                                cell137.Colspan = 10;
                                cell137.BorderWidth = 0;
                                cell137.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell137);

                                PdfPCell cell1054 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell1054.Colspan = 15;//toma columnas
                                cell1054.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell1054);

                                PdfPCell cell307 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell307.Colspan = 2;
                                cell307.BorderWidth = 0;
                                cell307.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell307);

                                PdfPCell cell432 = new PdfPCell(new Phrase(" ", cuerpo));
                                cell307.Colspan = 2;
                                cell432.BorderWidth = 0;
                                cell432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell432);

                                PdfPCell cell228 = new PdfPCell(new Phrase("Acapulco Gro., a ________________ de ________________ del 20_____", cuerpo));
                                cell228.Colspan = 11;
                                cell228.BorderWidth = 0;
                                cell228.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                table.AddCell(cell228);

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
                            else
                            {
                                if (Convert.ToInt32(sesion.Grado) == 6)
                                {
                                    // Creamos el documento con el tamaño de página tradicional
                                    Document doc = new Document(PageSize.LETTER.Rotate(), 10, 10, 10, 10);
                                    string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
                                    if (!Directory.Exists(folderPath))// pregunt si no existe
                                    {
                                        Directory.CreateDirectory(folderPath); // si no existe lo crea
                                    }
                                    // Creamos el documento con el tamaño de página tradicional
                                    FileStream stream = new FileStream(folderPath + "Lista-Asistencia6.pdf", FileMode.Create);
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
                                    PdfPTable table = new PdfPTable(43);
                                    table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

                                    iTextSharp.text.Image logoEsc = iTextSharp.text.Image.GetInstance("../../../logo-esc.png");
                                    logoEsc.BorderWidth = 0;
                                    logoEsc.ScaleAbsolute(120, 70);
                                    iTextSharp.text.Image logoSep = iTextSharp.text.Image.GetInstance("../../../logo.png");
                                    logoSep.ScaleAbsolute(150, 60);


                                    // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
                                    // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
                                    float[] Celdas = { 0.25f, 0.25f, 1.50f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.10f, 0.50f, 0.50f, 0.50f, 0.50f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.20f, 0.45f };

                                    // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
                                    table.SetWidths(Celdas);

                                    //encabezado

                                    PdfPCell cell40 = new PdfPCell(new Phrase(" "));
                                    cell40.Colspan = 2;//toma columnas
                                    cell40.Rowspan = 4;//toma filas
                                    cell40.BorderWidth = 0;
                                    table.AddCell(cell40);

                                    PdfPCell cell39 = new PdfPCell(new Phrase("GRUPO: " + sesion.Grado + "  A", cuerpo));
                                    cell39.BorderWidth = 0;
                                    cell39.PaddingTop = 5f;
                                    cell39.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell39);

                                    PdfPCell cell53 = new PdfPCell(new Phrase(" "));
                                    cell53.Colspan = 7;//toma columnas
                                    cell53.Rowspan = 4;//toma filas
                                    cell53.BorderWidth = 0;
                                    table.AddCell(cell53);

                                    PdfPCell cell1z = new PdfPCell(new Phrase("INSTITTUTO RODOLFO NERI VELA\n\n PRESTIGIO EN TU CONOCIMIENTO\n\n Vicente Guerrero 49, Barrios Historicos, 39540.", titulos));
                                    cell1z.Colspan = 24;//toma columnas
                                    cell1z.Rowspan = 4;//toma filas
                                    cell1z.BorderWidth = 0;
                                    cell1z.PaddingTop = 5f;
                                    cell1z.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1z);

                                    PdfPCell cell44 = new PdfPCell(new Phrase(" "));
                                    cell44.Colspan = 9;//toma columnas
                                    cell44.Rowspan = 4;
                                    cell44.BorderWidth = 0;
                                    table.AddCell(cell44);

                                    PdfPCell cell42 = new PdfPCell(new Phrase("PROFR.(A):  " + Apellidop6 + "  " + Apellidom6 + "  " + nombre6, cuerpo));
                                    cell42.BorderWidth = 0;
                                    cell42.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell42);

                                    PdfPCell cell4311 = new PdfPCell(new Phrase("MES: ", cuerpo));
                                    cell4311.BorderWidth = 0;
                                    cell4311.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell4311);

                                    PdfPCell cell43 = new PdfPCell(new Phrase("AÑO ESCOLAR: 2018 - 2019", cuerpo));
                                    cell43.BorderWidth = 0;
                                    cell43.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell43);

                                    //Fila para dar espaciado entre tablas
                                    PdfPCell cell112 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell112.BorderWidth = 0;
                                    cell112.Colspan = 43;
                                    table.AddCell(cell112);

                                    //tabla de formacion academica            
                                    PdfPCell cell431 = new PdfPCell(new Phrase("Núm.\nProgr.", cuerpo));
                                    cell431.Rowspan = 3;//toma columnas
                                    cell431.Rotation = 90;
                                    cell431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell431);

                                    PdfPCell cell1 = new PdfPCell(new Phrase("Sexo", cuerpo));
                                    cell1.Rowspan = 3;//toma filas
                                    cell1.PaddingLeft = 5f;
                                    cell1.Rotation = 90;
                                    cell1.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1);

                                    PdfPCell cell2 = new PdfPCell(new Phrase("Nombres", cuerpo));
                                    cell2.PaddingTop = 5f;
                                    cell2.Rowspan = 3;
                                    cell2.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2);

                                    PdfPCell cell2A = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2A.Rowspan = 3;
                                    cell2A.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2A);

                                    PdfPCell cell2B = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2B.Rowspan = 3;
                                    cell2B.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2B);

                                    PdfPCell cell2C = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2C.Rowspan = 3;
                                    cell2C.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2C);

                                    PdfPCell cell2D = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2D.Rowspan = 3;
                                    cell2D.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2D);

                                    PdfPCell cellC = new PdfPCell(new Phrase(" ", cuerpo));
                                    cellC.Rowspan = 3;
                                    cellC.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cellC);

                                    PdfPCell cell25 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell25.Rowspan = 3;
                                    cell25.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell25);

                                    PdfPCell cell26 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell26.Rowspan = 3;
                                    cell26.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell26);

                                    PdfPCell cell12 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell12.Rowspan = 3;
                                    cell12.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell12);

                                    PdfPCell cell3 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell3.Rowspan = 3;
                                    cell3.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell3);

                                    PdfPCell cell13 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell13.Rowspan = 3;
                                    cell13.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell13);

                                    PdfPCell cell381 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell381.Rowspan = 3;
                                    cell381.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell381);

                                    PdfPCell cell19 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell19.Rowspan = 3;
                                    cell19.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell19);

                                    PdfPCell cell28 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell28.Rowspan = 3;
                                    cell28.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell28);

                                    PdfPCell cell8 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell8.Rowspan = 3;
                                    cell8.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell8);

                                    PdfPCell cell9 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell9.Rowspan = 3;
                                    cell9.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell9);

                                    PdfPCell cell69 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell69.Rowspan = 3;
                                    cell69.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell69);

                                    PdfPCell cell29 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell29.Rowspan = 3;
                                    cell29.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell29);

                                    PdfPCell cell30 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell30.Rowspan = 3;
                                    cell30.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell30);

                                    PdfPCell cell24 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell24.Rowspan = 3;
                                    cell24.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell24);

                                    PdfPCell cell4 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell4.Rowspan = 3;
                                    cell4.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell4);

                                    PdfPCell cell201 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell201.Rowspan = 3;
                                    cell201.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell201);

                                    PdfPCell cell202 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell202.Rowspan = 3;
                                    cell202.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell202);

                                    PdfPCell cell5 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell5.Rowspan = 3;
                                    cell5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell5);

                                    PdfPCell cell37 = new PdfPCell(new Phrase("Asisten", letmed));
                                    cell37.Rowspan = 3;
                                    cell37.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell37);

                                    PdfPCell cell27 = new PdfPCell(new Phrase("Inasist", letmed));
                                    cell27.Rowspan = 3;
                                    cell27.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell27);

                                    PdfPCell cell6 = new PdfPCell(new Phrase("RASGOS A EVALUAR", cuerpo));
                                    cell6.Colspan = 4;
                                    cell6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell6);

                                    PdfPCell cell7 = new PdfPCell(new Phrase("EXAMENES", cuerpo));
                                    cell7.Colspan = 9;
                                    cell7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell7);

                                    PdfPCell cell210 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell210.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell210);

                                    PdfPCell cell1a5 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell1a5.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1a5);

                                    PdfPCell cell1a6 = new PdfPCell(new Phrase("TRABAJO EN CLASES", letmed));
                                    cell1a6.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1a6);

                                    PdfPCell cell1a7 = new PdfPCell(new Phrase("VALORES", letmed));
                                    cell1a7.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1a7);

                                    PdfPCell cell211 = new PdfPCell(new Phrase("PARTICIPACIÓN", letmed));
                                    cell211.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell211);

                                    PdfPCell cell212 = new PdfPCell(new Phrase("TAREAS", letmed));
                                    cell212.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell212);

                                    PdfPCell cell214 = new PdfPCell(new Phrase("ESP", cuerpo));
                                    cell214.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell214);

                                    PdfPCell cell10 = new PdfPCell(new Phrase("MAT", cuerpo));
                                    cell10.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell10);

                                    PdfPCell cell220 = new PdfPCell(new Phrase("ING", cuerpo));
                                    cell220.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell220);

                                    PdfPCell cell2121 = new PdfPCell(new Phrase("NAT ", cuerpo));
                                    cell2121.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2121);

                                    PdfPCell cell1120 = new PdfPCell(new Phrase("GEO", cuerpo));
                                    cell1120.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1120);

                                    PdfPCell cell1121 = new PdfPCell(new Phrase("HIS", cuerpo));
                                    cell1121.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1121);

                                    PdfPCell cell2111 = new PdfPCell(new Phrase("CIV", cuerpo));
                                    cell2111.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2111);

                                    PdfPCell cell1110 = new PdfPCell(new Phrase("ED.ECO", cuerpo));
                                    cell1110.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1110);

                                    PdfPCell cell221 = new PdfPCell(new Phrase("ART", cuerpo));
                                    cell221.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell221);

                                    PdfPCell cell222 = new PdfPCell(new Phrase("ED.FIS", cuerpo));
                                    cell222.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell222);

                                    PdfPCell cell101 = new PdfPCell(new Phrase("PROG. GRAL", cuerpo));
                                    cell101.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell101);

                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");

                                    //listas de alumnos
                                    table.AddCell(" 1");
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[0], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[0] + " " + Apellidom[0] + nombre[0], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");

                                    table.AddCell(" 2");
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[1], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[1] + " " + Apellidom[1] + nombre[1], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");

                                    table.AddCell(" 3");
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[2], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[2] + " " + Apellidom[2] + nombre[2], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");

                                    table.AddCell(" 4");
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[3], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[3] + " " + Apellidom[3] + nombre[3], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");

                                    table.AddCell(" 5");
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[4], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[4] + " " + Apellidom[4] + nombre[4], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");

                                    table.AddCell(" 6");
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[5], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[5] + " " + Apellidom[5] + nombre[5], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");

                                    table.AddCell(" 7");
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[6], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[6] + " " + Apellidom[6] + nombre[6], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");

                                    table.AddCell(" 8");
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[7], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[7] + " " + Apellidom[7] + nombre[7], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");

                                    table.AddCell(" 9");
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[8], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[8] + " " + Apellidom[8] + nombre[8], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[9], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[9] + " " + Apellidom[9] + nombre[9], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[10], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[10] + " " + Apellidom[10] + nombre[10], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[11], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[11] + " " + Apellidom[11] + nombre[11], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[12], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[12] + " " + Apellidom[12] + nombre[12], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[13], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[13] + " " + Apellidom[13] + nombre[13], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[14], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[14] + " " + Apellidom[14] + nombre[14], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[15], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[15] + " " + Apellidom[15] + nombre[15], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[16], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[16] + " " + Apellidom[16] + nombre[16], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[17], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[17] + " " + Apellidom[17] + nombre[17], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[18], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase(Apellidop[18] + " " + Apellidom[18] + nombre[18], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[19], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[19] + " " + Apellidom[19] + nombre[19], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[20], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[20] + " " + Apellidom[20] + nombre[20], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[21], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[21] + " " + Apellidom[21] + nombre[21], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[22], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[22] + " " + Apellidom[22] + nombre[22], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[23], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[23] + " " + Apellidom[23] + nombre[23], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
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
                                    table.AddCell(new PdfPCell(new Phrase("" + Genero[24], cuerpo)));
                                    table.AddCell(new PdfPCell(new Phrase("" + Apellidop[24] + " " + Apellidom[24] + nombre[24], letmed)));
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");
                                    table.AddCell(" ");


                                    //Fila para dar espaciado entre tablas
                                    PdfPCell cell113 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell113.BorderWidth = 0;
                                    cell113.Colspan = 43;
                                    table.AddCell(cell113);

                                    PdfPCell cell140 = new PdfPCell(new Phrase(" "));
                                    cell140.Colspan = 3;//toma columnas
                                    cell140.BorderWidth = 0;
                                    cell140.Rowspan = 5;//toma filas
                                                        //cell140.BorderWidth = 0;
                                    table.AddCell(cell140);

                                    PdfPCell cell230 = new PdfPCell(new Phrase("HOMBRES: ", cuerpo));
                                    cell230.Colspan = 10;//toma columnas
                                    cell230.BorderWidth = 0;
                                    cell230.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell230);

                                    PdfPCell cell1012 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell1012.Colspan = 8;//toma columnas
                                    cell1012.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1012);

                                    PdfPCell celld = new PdfPCell(new Phrase("EXIST.", cuerpo));
                                    celld.Colspan = 7;//toma columnas
                                    celld.BorderWidth = 0;
                                    celld.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(celld);

                                    PdfPCell cell231 = new PdfPCell(new Phrase("APROB.", cuerpo));
                                    cell231.Colspan = 2;
                                    cell231.BorderWidth = 0;
                                    cell231.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell231);

                                    PdfPCell cell232 = new PdfPCell(new Phrase("REPROB.", cuerpo));
                                    cell232.Colspan = 2;
                                    cell232.BorderWidth = 0;
                                    cell232.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell232);

                                    PdfPCell cell238 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell238.Colspan = 11;
                                    cell238.BorderWidth = 0;
                                    cell238.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell238);

                                    PdfPCell cell77 = new PdfPCell(new Phrase("", cuerpo));
                                    cell77.Colspan = 18;
                                    cell77.BorderWidth = 0;
                                    cell77.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell77);

                                    PdfPCell cell240 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell240.Colspan = 7;
                                    cell240.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell240);

                                    PdfPCell cell241 = new PdfPCell(new Phrase("", cuerpo));
                                    cell241.Colspan = 2;
                                    cell241.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell241);

                                    PdfPCell cell242 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell242.Colspan = 2;
                                    cell242.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell242);

                                    PdfPCell cell207 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell207.Colspan = 3;
                                    cell207.BorderWidth = 0;
                                    cell207.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell207);

                                    PdfPCell cell219 = new PdfPCell(new Phrase("PROMEDIO ", cuerpo));
                                    cell219.Colspan = 5;
                                    cell219.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell219);

                                    PdfPCell cell250 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell250.Colspan = 3;
                                    cell250.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell250);

                                    PdfPCell cell236 = new PdfPCell(new Phrase("MUJERES: ", cuerpo));
                                    cell236.Colspan = 10;//toma columnas
                                    cell236.BorderWidth = 0;
                                    cell236.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell236);

                                    PdfPCell cell1014 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell1014.Colspan = 8;//toma columnas
                                    cell1014.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1014);

                                    PdfPCell cell12d = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell12d.Colspan = 7;//toma columnas
                                    cell12d.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell12d);

                                    PdfPCell cell2331 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2331.Colspan = 2;
                                    cell2331.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2331);

                                    PdfPCell cell2342 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2342.Colspan = 2;
                                    cell2342.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2342);

                                    PdfPCell cell2017 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2017.Colspan = 3;
                                    cell2017.BorderWidth = 0;
                                    cell2017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2017);

                                    PdfPCell cell2119 = new PdfPCell(new Phrase("% DE APROBADOS ", cuerpo));
                                    cell2119.Colspan = 5;
                                    cell2119.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2119);

                                    PdfPCell cell2150 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2150.Colspan = 3;
                                    cell2150.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2150);

                                    PdfPCell cell177 = new PdfPCell(new Phrase("", cuerpo));
                                    cell177.Colspan = 18;
                                    cell177.BorderWidth = 0;
                                    cell177.HorizontalAlignment = 0; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell177);

                                    PdfPCell cell2401 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2401.Colspan = 7;
                                    cell2401.HorizontalAlignment = 2; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2401);

                                    PdfPCell cell2431 = new PdfPCell(new Phrase("", cuerpo));
                                    cell2431.Colspan = 2;
                                    cell2431.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2431);

                                    PdfPCell cell2432 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2432.Colspan = 2;
                                    cell2432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2432);

                                    PdfPCell cell2328 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell2328.Colspan = 11;
                                    cell2328.BorderWidth = 0;
                                    cell2328.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell2328);

                                    PdfPCell cell137 = new PdfPCell(new Phrase("FIRMA DEL MAESTRO(A): ", cuerpo));
                                    cell137.Colspan = 10;
                                    cell137.BorderWidth = 0;
                                    cell137.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell137);

                                    PdfPCell cell1054 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell1054.Colspan = 15;//toma columnas
                                    cell1054.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell1054);

                                    PdfPCell cell307 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell307.Colspan = 2;
                                    cell307.BorderWidth = 0;
                                    cell307.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell307);

                                    PdfPCell cell432 = new PdfPCell(new Phrase(" ", cuerpo));
                                    cell307.Colspan = 2;
                                    cell432.BorderWidth = 0;
                                    cell432.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell432);

                                    PdfPCell cell228 = new PdfPCell(new Phrase("Acapulco Gro., a ________________ de ________________ del 20_____", cuerpo));
                                    cell228.Colspan = 11;
                                    cell228.BorderWidth = 0;
                                    cell228.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
                                    table.AddCell(cell228);

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
                                else
                                {
                                    MessageBox.Show("No se encuentra la lista del grupo seleccionado...");
                                }
                            }
                        }
                    }
                }
            }
        }

        //-------------------------------------------------------------------------------------------
        private void CadaGrupo_Load(object sender, EventArgs e)
        {

        }


        public void mostrardatoscadagrupo()
        {


        }


        public void datagrid(DataGridView data)
        {

            coneccion.Open();
            codigo.Connection = coneccion;
            codigo.CommandText = ("SELECT    `ApellidoP`, `ApellidoM`, `nombre`, `CURP`,`idGrado`  FROM  `alumno`  where  idGrado  ='" + sesion.Grado + "' "+ "ORDER BY  `ApellidoP`  ASC");


            try
            {
                MySqlDataAdapter seleccionar = new MySqlDataAdapter();
                seleccionar.SelectCommand = codigo;

                DataTable datostabla = new DataTable();
                DataColumn numerodelista = new DataColumn();
                numerodelista.ColumnName = "Numero de lista";
                numerodelista.DataType = System.Type.GetType("System.Int32");
                numerodelista.AutoIncrement = true;
                numerodelista.AutoIncrementSeed = 1;
                 numerodelista.AutoIncrementStep = 1;
                datostabla.Columns.Add(numerodelista);
              
                seleccionar.Fill(datostabla);
                BindingSource formulario = new BindingSource();
                formulario.DataSource = datostabla;
                data.DataSource = formulario;
                seleccionar.Update(datostabla);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void exportardata(DataGridView dgw, string filename)
        {
            BaseFont bf = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1250, BaseFont.EMBEDDED);
            PdfPTable pdftable = new PdfPTable(dgw.Columns.Count);
            pdftable.DefaultCell.Padding = 3;
            pdftable.WidthPercentage = 100;
            pdftable.HorizontalAlignment = Element.ALIGN_LEFT;
            pdftable.DefaultCell.BorderWidth = 1;
            iTextSharp.text.Font text = new iTextSharp.text.Font(bf, 15, iTextSharp.text.Font.NORMAL);

            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                PdfPCell cell = new PdfPCell(new Phrase(column.HeaderText));
                cell.BackgroundColor = new iTextSharp.text.BaseColor(240, 240, 240);
                pdftable.AddCell(cell);
            }

            int row = dataGridView1.Rows.Count;
            int cell2 = dataGridView1.Rows[1].Cells.Count;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < cell2; j++)
                {
                    if (dataGridView1.Rows[i].Cells[j].Value == null)
                    {
                        //return directly
                        //return;
                        //or set a value for the empty data
                        dataGridView1.Rows[i].Cells[j].Value = "null";
                    }
                    pdftable.AddCell(dataGridView1.Rows[i].Cells[j].Value.ToString());
                }
            }

            //Exporting to PDF
            string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
            if (!Directory.Exists(folderPath))// pregunt si no existe
            {
                Directory.CreateDirectory(folderPath); // si no existe lo crea
            }
            using (FileStream stream = new FileStream(folderPath + "listaalumno.pdf", FileMode.Create))
            {
                Document pdfDoc = new Document(PageSize.LETTER, 10f, 10f, 100f, 100f); //se declara las medidas y margenes del pdf por ejemplo tamaño CARTA
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, stream);//cosas de itextsharp xD

                //iTextSharp.text.Image imagen = iTextSharp.text.Image.GetInstance("C:/Users/Tevi/Documents/Gestionde proyectos/controlEscolar-master/logo1.jpg");
                string direccion = Directory.GetCurrentDirectory();//obtenemos direccion no se paque xD

                //aqui empezamos agregar cosas :3
                pdfDoc.Open();//se habre el docuemnto
                // Header hola = new Header();
               // writer.PageEvent = new Header();
                Header i = new Header();
                i.Headerlista1A(writer,pdfDoc);

               //se habre el docuemnto

                // pdfDoc.NewPage();

                //aqui pones todo lo que va en medio :D
                pdfDoc.Add(pdftable);
                pdfDoc.Close();
                stream.Close();
            }

        }
    }
}
