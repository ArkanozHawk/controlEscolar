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
    public partial class Calificaciones12 : MaterialForm
    {
        public Calificaciones12()
        {
            InitializeComponent();
            validaCalifMen();


        }

        double calificacion;
        string Español, Matematicas, Ingless, Conocimiento, Artess, Edsocio, EducacionF, Inasistencias;
        string materia, mes;

        conexion obj = new conexion();

        public static void ThreadProc()
        {
            Application.Run(new login());
        }

        public static void ThreadPrincipal()
        {
            Application.Run(new principal());
        }



        private void btnCerrar_Click_1(object sender, EventArgs e)
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

        private void btnIrBoletas_Click_1(object sender, EventArgs e)
        {

            //-------------Ingresar los datos del alumno en pdf--------------------------------
            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT  *  FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";
            string nombre, Apellidop, Apellidom, IdAlumno;

            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(query, conn);

            MySqlDataReader myreader = com.ExecuteReader();

            myreader.Read();
            nombre = Convert.ToString(myreader["nombre"]);
            Apellidop = Convert.ToString(myreader["ApellidoP"]);
            Apellidom = Convert.ToString(myreader["ApellidoM"]);
            sesion.grado = Convert.ToString(myreader["idGrado"]);
            IdAlumno = Convert.ToString(myreader["idAlumno"]);
            conn.Close();

            //-------------------------------Ingresar las calificaciones mensuales de los alumnos---------------------

            //Septiembre------------------------------------------
            string CalifSep = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Septiembre'";

            MySqlConnection conn1;
            MySqlCommand com1;

            conn1 = new MySqlConnection(conexion);
            conn1.Open();

            com1 = new MySqlCommand(CalifSep, conn1);

            MySqlDataReader myreader1 = com1.ExecuteReader();

            double[] CalifSept = new double[8];
            int L = 0;
            while (myreader1.Read())
            {
                CalifSept[L] = Convert.ToDouble(myreader1["CalificacionMen"]);
                L++;
            }

            //Octubre-----------------------------------------------------------------
            string CalifOct = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Octubre'";

            MySqlConnection conn2;
            MySqlCommand com2;

            conn2 = new MySqlConnection(conexion);
            conn2.Open();

            com2 = new MySqlCommand(CalifOct, conn2);

            MySqlDataReader myreader2 = com2.ExecuteReader();

            double[] CalifOctu = new double[8];
            int I = 0;
            while (myreader2.Read())
            {
                CalifOctu[I] = Convert.ToDouble(myreader1["CalificacionMen"]);
                I++;
            }

            //Noviembre-----------------------------------------------------------------
            string CalifNov = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Noviembre'";

            MySqlConnection conn3;
            MySqlCommand com3;

            conn3 = new MySqlConnection(conexion);
            conn3.Open();

            com3 = new MySqlCommand(CalifNov, conn3);

            MySqlDataReader myreader3 = com3.ExecuteReader();

            double[] CalifNovi = new double[8];
            int Z = 0;
            while (myreader3.Read())
            {
                CalifNovi[Z] = Convert.ToDouble(myreader3["CalificacionMen"]);
                Z++;
            }

            //Diciembre-----------------------------------------------------------------
            string CalifDic = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Diciembre'";

            MySqlConnection conn4;
            MySqlCommand com4;

            conn4 = new MySqlConnection(conexion);
            conn4.Open();

            com4 = new MySqlCommand(CalifDic, conn1);

            MySqlDataReader myreader4 = com4.ExecuteReader();

            double[] CalifDici = new double[8];
            int E = 0;
            while (myreader4.Read())
            {
                CalifDici[E] = Convert.ToDouble(myreader4["CalificacionMen"]);
                E++;
            }

            //Enero-----------------------------------------------------------------
            string CalifEne = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Enero'";

            MySqlConnection conn5;
            MySqlCommand com5;

            conn5 = new MySqlConnection(conexion);
            conn5.Open();

            com5 = new MySqlCommand(CalifEne, conn5);

            MySqlDataReader myreader5 = com5.ExecuteReader();

            double[] CalifEner = new double[8];
            int T = 0;
            while (myreader5.Read())
            {
                CalifEner[T] = Convert.ToDouble(myreader5["CalificacionMen"]);
                T++;
            }

            //Febrero-----------------------------------------------------------------
            string CalifFeb = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Febrero'";

            MySqlConnection conn6;
            MySqlCommand com6;

            conn6 = new MySqlConnection(conexion);
            conn6.Open();

            com6 = new MySqlCommand(CalifFeb, conn6);

            MySqlDataReader myreader6 = com6.ExecuteReader();

            double[] CalifFebr = new double[8];
            int H = 0;
            while (myreader6.Read())
            {
                CalifFebr[H] = Convert.ToDouble(myreader6["CalificacionMen"]);
                H++;
            }

            //Marzo-----------------------------------------------------------------
            string CalifMar = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Marzo'";

            MySqlConnection conn7;
            MySqlCommand com7;

            conn7 = new MySqlConnection(conexion);
            conn7.Open();

            com7 = new MySqlCommand(CalifMar, conn7);

            MySqlDataReader myreader7 = com7.ExecuteReader();

            double[] CalifMarz = new double[8];
            int B = 0;
            while (myreader7.Read())
            {
                CalifMarz[B] = Convert.ToDouble(myreader7["CalificacionMen"]);
                B++;
            }

            //Abril-----------------------------------------------------------------
            string CalifAbr = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Abril'";

            MySqlConnection conn8;
            MySqlCommand com8;

            conn8 = new MySqlConnection(conexion);
            conn8.Open();

            com8 = new MySqlCommand(CalifAbr, conn8);

            MySqlDataReader myreader8 = com8.ExecuteReader();

            double[] CalifAbri = new double[8];
            int R = 0;
            while (myreader8.Read())
            {
                CalifAbri[R] = Convert.ToDouble(myreader8["CalificacionMen"]);
                R++;
            }

            //Mayo-----------------------------------------------------------------
            string CalifMay = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Mayo'";

            MySqlConnection conn9;
            MySqlCommand com9;

            conn9 = new MySqlConnection(conexion);
            conn9.Open();

            com9 = new MySqlCommand(CalifMay, conn9);

            MySqlDataReader myreader9 = com9.ExecuteReader();

            double[] CalifMayo = new double[8];
            int Y = 0;
            while (myreader9.Read())
            {
                CalifMayo[Y] = Convert.ToDouble(myreader9["CalificacionMen"]);
                Y++;
            }

            //Junio-----------------------------------------------------------------
            string CalifJun = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Junio'";

            MySqlConnection conn10;
            MySqlCommand com10;

            conn10 = new MySqlConnection(conexion);
            conn10.Open();

            com10 = new MySqlCommand(CalifJun, conn10);

            MySqlDataReader myreader10 = com10.ExecuteReader();

            double[] CalifJuni = new double[8];
            int A = 0;
            while (myreader10.Read())
            {
                CalifJuni[A] = Convert.ToDouble(myreader10["CalificacionMen"]);
                A++;
            }

            //Diagnostico-----------------------------------------------------------------
            string CalifDig = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + IdAlumno + " AND `Mes` = 'Diagnostico'";

            MySqlConnection conn11;
            MySqlCommand com11;

            conn11 = new MySqlConnection(conexion);
            conn11.Open();

            com11 = new MySqlCommand(CalifDig, conn11);

            MySqlDataReader myreader11 = com11.ExecuteReader();

            double[] CalifDiag = new double[8];
            int N = 0;
            while (myreader11.Read())
            {
                CalifDiag[N] = Convert.ToDouble(myreader11["CalificacionMen"]);
                N++;
            }

            //-------------------------------------------------------------------------------------------------------------------

            // Creamos el documento con el tamaño de página tradicional
            Document doc = new Document(PageSize.LETTER);
            string folderPath = @"C:\shashe\"; // vfolder donde estaran los pdf
            if (!Directory.Exists(folderPath))// pregunt si no existe
            {
                Directory.CreateDirectory(folderPath); // si no existe lo crea
            }
            // Creamos el documento con el tamaño de página tradicional
            FileStream stream = new FileStream(folderPath + "calificaciones12.pdf", FileMode.Create);
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
            PdfPTable table = new PdfPTable(22);
            table.WidthPercentage = 100;//Le damos un tamaño a la tabla, esta tomara en porcierto el ancho que ocupara

            iTextSharp.text.Image logoEsc = iTextSharp.text.Image.GetInstance("../../../logo-esc.png");
            logoEsc.BorderWidth = 0;
            logoEsc.ScaleAbsolute(120, 70);
            iTextSharp.text.Image logoSep = iTextSharp.text.Image.GetInstance("../../../logo.png");
            logoSep.ScaleAbsolute(150, 60);


            // CREO UN ARREGLO QUE CONTIENE LAS MEDIDAS DE CADA UNA DE LAS COLUMNAS
            // EN MI CASO SON 4, (TB PUEDES PASAR EL ARREGLO DIRECTAMENTE)
            float[] Celdas = { 1.50f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f, 0.25f };

            // ASIGNAS LAS MEDIDAS A LA TABLA (ANCHO)
            table.SetWidths(Celdas);

            //encabezado
            PdfPCell cell38 = new PdfPCell(logoEsc);
            cell38.Colspan = 2;//toma columnas
            cell38.Rowspan = 4;//toma filas
            cell38.BorderWidth = 0;
            cell38.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell38.PaddingTop = 5f;
            cell38.PaddingBottom = 5f;
            table.AddCell(cell38);

            PdfPCell cell39 = new PdfPCell(new Phrase("INSTITUTO RODOLFO NERI VELA", tituloprin));
            cell39.Colspan = 12;//toma columnas
            cell39.Rowspan = 2;//toma filas
            cell39.BorderWidth = 0;
            cell39.PaddingTop = 15f;
            cell38.PaddingBottom = 10f;
            cell39.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell39);

            //logo 2
            PdfPCell cell40 = new PdfPCell(logoSep);
            cell40.Colspan = 8;//toma columnas
            cell40.Rowspan = 4;//toma filas
            cell40.BorderWidth = 0;
            cell40.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell40.PaddingTop = 5f;
            table.AddCell(cell40);

            PdfPCell cell1z = new PdfPCell(new Phrase("Vicente Guerrero 49, Barrios Historicos, Acapulco Gro. 39540\n\nClave: 12DPT0003N         Nivel: Primaria\n"));
            cell1z.Colspan = 12;//toma columnas
            cell1z.Rowspan = 2;//toma filas
            cell1z.PaddingTop = 10f;
            cell1z.BorderWidth = 0;
            cell1z.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell1z);

            PdfPCell cell44 = new PdfPCell(new Phrase("GRADO : " + sesion.Grado + "GRUPO:  A"));
            cell44.Colspan = 22;//toma columnas
            cell44.BorderWidth = 0;
            cell44.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell44);

            PdfPCell cell42 = new PdfPCell(new Phrase("N° DE LISTA:        CICLO ESCOLAR: 2018 - 2019"));
            cell42.Colspan = 22;//toma columnas
            cell42.BorderWidth = 0;
            cell42.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell42);

            PdfPCell cell43 = new PdfPCell(new Phrase("ALUMNO:   " + nombre + "" + Apellidop + "" + Apellidom + "CURP:" + sesion.Curp));
            cell43.Colspan = 22;//toma columnas
            cell43.BorderWidth = 0;
            cell43.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell43);

            //Fila para dar espaciado entre tablas
            PdfPCell cell112 = new PdfPCell(new Phrase(" ", cuerpo));
            cell112.BorderWidth = 0;
            cell112.Colspan = 22;
            table.AddCell(cell112);


            //tabla de formacion academica
            PdfPCell cell1 = new PdfPCell(new Phrase("ASIGNATURA FORMACIÓN ACADÉMICA", titulos));
            cell1.PaddingTop = 20f;
            cell1.Colspan = 2;//toma columnas
            cell1.Rowspan = 3;//toma filas
            cell1.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell1);

            PdfPCell cell2 = new PdfPCell(new Phrase("CALIFICACIONES MENSUALES", titulos));
            cell2.Colspan = 11;
            cell2.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell2);

            //tabla vacia vertical
            PdfPCell cell1A = new PdfPCell(new Phrase(" ", cuerpo));
            cell1A.Rowspan = 13;
            cell1A.BorderWidth = 0;
            table.AddCell(cell1A);

            //Tabla periodos
            PdfPCell cell2A = new PdfPCell(new Phrase("PERIODOS", titulos));
            cell2A.Colspan = 8;
            cell2A.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell2A);

            //Meses
            PdfPCell cellA = new PdfPCell(new Phrase("DIAGNOSTICO", letmed));
            cellA.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellA.Rowspan = 2;
            cellA.Rotation = 90;
            table.AddCell(cellA);

            PdfPCell cellB = new PdfPCell(new Phrase("SEPTIEMBRE", letmed));
            cellB.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellB.Rowspan = 2;
            cellB.Rotation = 90;
            table.AddCell(cellB);

            PdfPCell cellC = new PdfPCell(new Phrase("OCTUBRE", letmed));
            cellC.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellC.Rowspan = 2;
            cellC.Rotation = 90;
            table.AddCell(cellC);

            PdfPCell cellD = new PdfPCell(new Phrase("NOVIEMBRE", letmed));
            cellD.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellD.Rowspan = 2;
            cellD.Rotation = 90;
            table.AddCell(cellD);

            PdfPCell cellE = new PdfPCell(new Phrase("DICIEMBRE", letmed));
            cellE.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellE.Rowspan = 2;
            cellE.Rotation = 90;
            table.AddCell(cellE);

            PdfPCell cellF = new PdfPCell(new Phrase("ENERO", letmed));
            cellF.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellF.Rowspan = 2;
            cellF.Rotation = 90;
            table.AddCell(cellF);

            PdfPCell cellG = new PdfPCell(new Phrase("FEBRERO", letmed));
            cellG.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellG.Rowspan = 2;
            cellG.Rotation = 90;
            table.AddCell(cellG);

            PdfPCell cellH = new PdfPCell(new Phrase("MARZO", letmed));
            cellH.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellH.Rowspan = 2;
            cellH.Rotation = 90;
            table.AddCell(cellH);

            PdfPCell cellI = new PdfPCell(new Phrase("ABRIL", letmed));
            cellI.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellI.Rowspan = 2;
            cellI.Rotation = 90;
            table.AddCell(cellI);

            PdfPCell cellJ = new PdfPCell(new Phrase("MAYO", letmed));
            cellJ.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellJ.Rowspan = 2;
            cellJ.Rotation = 90;
            table.AddCell(cellJ);

            PdfPCell cellK = new PdfPCell(new Phrase("JUNIO", letmed));
            cellK.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellK.Rowspan = 2;
            cellK.Rotation = 90;
            table.AddCell(cellK);


            PdfPCell cellV = new PdfPCell(new Phrase("1° TRIM", letmed));
            cellV.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellV.Colspan = 2;
            table.AddCell(cellV);

            PdfPCell cellW = new PdfPCell(new Phrase("2° TRIM", letmed));
            cellW.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellW.Colspan = 2;
            table.AddCell(cellW);

            PdfPCell cellX = new PdfPCell(new Phrase("3° TRIM", letmed));
            cellX.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cellX.Colspan = 2;
            table.AddCell(cellX);

            PdfPCell cell07 = new PdfPCell(new Phrase("PROMEDIO FINAL", letmed));
            cell07.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell07.Rowspan = 2;
            cell07.Rotation = 90;
            table.AddCell(cell07);

            PdfPCell cell08 = new PdfPCell(new Phrase("DESEMPEÑO FINAL", letmed));
            cell08.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell08.Rowspan = 2;
            cell08.Rotation = 90;
            table.AddCell(cell08);

            PdfPCell cell01 = new PdfPCell(new Phrase("CALIF.", letchica));
            cell01.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell01.Rotation = 90;
            table.AddCell(cell01);

            PdfPCell cell02 = new PdfPCell(new Phrase("NIVEL DE DESEMP.", letchica));
            cell02.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell02.Rotation = 90;
            table.AddCell(cell02);

            PdfPCell cell03 = new PdfPCell(new Phrase("CALIF.", letchica));
            cell03.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell03.Rotation = 90;
            table.AddCell(cell03);

            PdfPCell cell04 = new PdfPCell(new Phrase("NIVEL DE DESEMP.", letchica));
            cell04.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell04.Rotation = 90;
            table.AddCell(cell04);

            PdfPCell cell05 = new PdfPCell(new Phrase("CALIF.", letchica));
            cell05.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell05.Rotation = 90;
            table.AddCell(cell05);

            PdfPCell cell06 = new PdfPCell(new Phrase("NIVEL DE DESEMP.", letchica));
            cell06.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            cell06.Rotation = 90;
            table.AddCell(cell06);


            //Materias
            PdfPCell cell3 = new PdfPCell(new Phrase("ESPAÑOL", cuerpo));
            cell3.Colspan = 2;
            table.AddCell(cell3);

            table.AddCell("" + CalifDiag[0]);
            table.AddCell("" + CalifSept[0]);
            table.AddCell("" + CalifOctu[0]);
            table.AddCell("" + CalifNovi[0]);
            table.AddCell("" + CalifDici[0]);
            table.AddCell("" + CalifEner[0]);
            table.AddCell("" + CalifFebr[0]);
            table.AddCell("" + CalifMarz[0]);
            table.AddCell("" + CalifAbri[0]);
            table.AddCell("" + CalifMayo[0]);
            table.AddCell("" + CalifJuni[0]);

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            PdfPCell cell4 = new PdfPCell(new Phrase("MATEMÁTICAS", cuerpo));
            cell4.Colspan = 2;
            table.AddCell(cell4);

            table.AddCell("" + CalifDiag[1]);
            table.AddCell("" + CalifSept[1]);
            table.AddCell("" + CalifOctu[1]);
            table.AddCell("" + CalifNovi[1]);
            table.AddCell("" + CalifDici[1]);
            table.AddCell("" + CalifEner[1]);
            table.AddCell("" + CalifFebr[1]);
            table.AddCell("" + CalifMarz[1]);
            table.AddCell("" + CalifAbri[1]);
            table.AddCell("" + CalifMayo[1]);
            table.AddCell("" + CalifJuni[1]);

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            PdfPCell cell5 = new PdfPCell(new Phrase("INGLÉS", cuerpo));
            cell5.Colspan = 2;
            table.AddCell(cell5);

            table.AddCell("" + CalifDiag[2]);
            table.AddCell("" + CalifSept[2]);
            table.AddCell("" + CalifOctu[2]);
            table.AddCell("" + CalifNovi[2]);
            table.AddCell("" + CalifDici[2]);
            table.AddCell("" + CalifEner[2]);
            table.AddCell("" + CalifFebr[2]);
            table.AddCell("" + CalifMarz[2]);
            table.AddCell("" + CalifAbri[2]);
            table.AddCell("" + CalifMayo[2]);
            table.AddCell("" + CalifJuni[2]);

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            PdfPCell cell6 = new PdfPCell(new Phrase("CONOCIMIENTO DEL MEDIO", cuerpo));
            cell6.Colspan = 2;
            table.AddCell(cell6);

            table.AddCell("" + CalifDiag[3]);
            table.AddCell("" + CalifSept[3]);
            table.AddCell("" + CalifOctu[3]);
            table.AddCell("" + CalifNovi[3]);
            table.AddCell("" + CalifEner[3]);
            table.AddCell("" + CalifFebr[3]);
            table.AddCell("" + CalifMarz[3]);
            table.AddCell("" + CalifAbri[3]);
            table.AddCell("" + CalifMayo[3]);
            table.AddCell("" + CalifJuni[3]);

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            PdfPCell cell7 = new PdfPCell(new Phrase("ARTES", cuerpo));
            cell7.Colspan = 2;
            table.AddCell(cell7);

            table.AddCell("" + CalifDiag[4]);
            table.AddCell("" + CalifSept[4]);
            table.AddCell("" + CalifOctu[4]);
            table.AddCell("" + CalifNovi[4]);
            table.AddCell("" + CalifEner[4]);
            table.AddCell("" + CalifFebr[4]);
            table.AddCell("" + CalifMarz[4]);
            table.AddCell("" + CalifAbri[4]);
            table.AddCell("" + CalifMayo[4]);
            table.AddCell("" + CalifJuni[4]);

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            PdfPCell cell9 = new PdfPCell(new Phrase("EDUCACIÓN FÍSICA", cuerpo));
            cell9.Colspan = 2;
            table.AddCell(cell9);

            table.AddCell("" + CalifDiag[5]);
            table.AddCell("" + CalifSept[5]);
            table.AddCell("" + CalifOctu[5]);
            table.AddCell("" + CalifNovi[5]);
            table.AddCell("" + CalifEner[5]);
            table.AddCell("" + CalifFebr[5]);
            table.AddCell("" + CalifMarz[5]);
            table.AddCell("" + CalifAbri[5]);
            table.AddCell("" + CalifMayo[5]);
            table.AddCell("" + CalifJuni[5]);

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            PdfPCell cell10 = new PdfPCell(new Phrase("PROM. FINAL FORMACIÓN ACADÉMICA", letchica));
            cell10.Colspan = 2;
            cell10.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell10);

            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            //Fila para dar espaciado entre tablas
            PdfPCell cell11 = new PdfPCell(new Phrase(" ", cuerpo));
            cell11.BorderWidth = 0;
            cell11.Colspan = 22;
            table.AddCell(cell11);

            //Tabla area de desarrollo personal
            PdfPCell cell12 = new PdfPCell(new Phrase("ÁREAS DE DESARROLLO PERSONAL, SOCIAL Y AUTONOMIA CURRICULAR", titulos));
            cell12.Colspan = 2;
            cell12.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell12);

            PdfPCell cellL = new PdfPCell(new Phrase("D", cuerpo));
            cellL.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellL);

            PdfPCell cellM = new PdfPCell(new Phrase("S", cuerpo));
            cellM.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellM);

            PdfPCell cellN = new PdfPCell(new Phrase("O", cuerpo));
            cellN.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellN);

            PdfPCell cellÑ = new PdfPCell(new Phrase("N", cuerpo));
            cellÑ.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellÑ);

            PdfPCell cellO = new PdfPCell(new Phrase("D", cuerpo));
            cellO.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellO);

            PdfPCell cellP = new PdfPCell(new Phrase("E", cuerpo));
            cellP.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellP);

            PdfPCell cellQ = new PdfPCell(new Phrase("F", cuerpo));
            cellQ.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellQ);

            PdfPCell cellR = new PdfPCell(new Phrase("M", cuerpo));
            cellR.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellR);

            PdfPCell cellS = new PdfPCell(new Phrase("A", cuerpo));
            cellS.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellS);

            PdfPCell cellT = new PdfPCell(new Phrase("M", cuerpo));
            cellT.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellT);

            PdfPCell cellU = new PdfPCell(new Phrase("J", cuerpo));
            cellU.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cellU);

            //tabla vacia vertical
            PdfPCell cell1b = new PdfPCell(new Phrase(" ", cuerpo));
            cell1b.Rowspan = 3;
            cell1b.BorderWidth = 0;
            table.AddCell(cell1b);

            // TABLA 2 DE PERIODOS

            PdfPCell cell011 = new PdfPCell(new Phrase("C", cuerpo));
            cell011.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell011);

            PdfPCell cell012 = new PdfPCell(new Phrase("D", cuerpo));
            cell012.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell012);

            PdfPCell cell013 = new PdfPCell(new Phrase("C", cuerpo));
            cell013.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell013);

            PdfPCell cell014 = new PdfPCell(new Phrase("D", cuerpo));
            cell014.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell014);

            PdfPCell cell015 = new PdfPCell(new Phrase("C", cuerpo));
            cell015.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell015);

            PdfPCell cell016 = new PdfPCell(new Phrase("D", cuerpo));
            cell016.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell016);

            PdfPCell cell017 = new PdfPCell(new Phrase("PF", cuerpo));
            cell017.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell017);

            PdfPCell cell018 = new PdfPCell(new Phrase("DF", cuerpo));
            cell018.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell018);

            //Materias
            PdfPCell cell13 = new PdfPCell(new Phrase("EDUCACIÓN SOCIOEMOCIONAL", cuerpo));
            cell13.Colspan = 2;
            table.AddCell(cell13);

            table.AddCell("" + CalifDiag[6]);
            table.AddCell("" + CalifSept[6]);
            table.AddCell("" + CalifOctu[6]);
            table.AddCell("" + CalifNovi[6]);
            table.AddCell("" + CalifEner[6]);
            table.AddCell("" + CalifFebr[6]);
            table.AddCell("" + CalifMarz[6]);
            table.AddCell("" + CalifAbri[6]);
            table.AddCell("" + CalifMayo[6]);
            table.AddCell("" + CalifJuni[6]);

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            PdfPCell cell14 = new PdfPCell(new Phrase("PROM. FINAL DE LAS ÁREAS DESARROLLO PERSONAL Y SOCIAL FORMACIÓN ACADÉMICA", letchica));
            cell14.Colspan = 2;
            cell14.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell14);

            table.AddCell("" + CalifDiag[6]);
            table.AddCell("" + CalifSept[6]);
            table.AddCell("" + CalifOctu[6]);
            table.AddCell("" + CalifNovi[6]);
            table.AddCell("" + CalifEner[6]);
            table.AddCell("" + CalifFebr[6]);
            table.AddCell("" + CalifMarz[6]);
            table.AddCell("" + CalifAbri[6]);
            table.AddCell("" + CalifMayo[6]);
            table.AddCell("" + CalifJuni[6]);

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            //Fila para dar espaciado entre tablas
            PdfPCell cell15 = new PdfPCell(new Phrase(" ", cuerpo));
            cell15.Colspan = 22;
            cell15.BorderWidth = 0;
            table.AddCell(cell15);

            //Tabla area de desarrollo personal
            PdfPCell cell16 = new PdfPCell(new Phrase("PROMEDIO GENERAL", titulos));
            cell16.Colspan = 2;
            cell16.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell16);

            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha

            //tabla vacia vertical
            PdfPCell cell1i = new PdfPCell(new Phrase(" ", cuerpo));
            cell1i.Rowspan = 3;
            cell1i.BorderWidth = 0;
            table.AddCell(cell1i);

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");

            //Fila para dar espaciado entre tablas
            PdfPCell cell17 = new PdfPCell(new Phrase(" ", cuerpo));
            cell17.Colspan = 22;
            cell17.BorderWidth = 0;
            table.AddCell(cell17);

            //Tabla area de desarrollo personal
            PdfPCell cell18 = new PdfPCell(new Phrase("INASISTENCIAS", titulos));
            cell18.Colspan = 2;
            cell18.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell18);

            table.AddCell("" + CalifDiag[7]);
            table.AddCell("" + CalifSept[7]);
            table.AddCell("" + CalifOctu[7]);
            table.AddCell("" + CalifNovi[7]);
            table.AddCell("" + CalifEner[7]);
            table.AddCell("" + CalifFebr[7]);
            table.AddCell("" + CalifMarz[7]);
            table.AddCell("" + CalifAbri[7]);
            table.AddCell("" + CalifMayo[7]);
            table.AddCell("" + CalifJuni[7]);
            table.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha


            //tabla vacia vertical
            PdfPCell cell1e = new PdfPCell(new Phrase(" ", cuerpo));
            cell1e.Rowspan = 13;
            cell1e.BorderWidth = 0;
            table.AddCell(cell1e);

            //Espacios linea de español
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");
            table.AddCell(" ");


            //Fila para dar espaciado entre tablas
            PdfPCell cell19 = new PdfPCell(new Phrase(" ", cuerpo));
            cell19.BorderWidth = 0;
            cell19.Colspan = 22;
            table.AddCell(cell19);

            //Tabla de niveles
            PdfPCell cell20 = new PdfPCell(new Phrase("NIVELES DE DESEMPEÑO", titulos));
            cell20.Colspan = 22;
            cell20.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell20);

            //Materias
            PdfPCell cell21 = new PdfPCell(new Phrase("NIVEL I = EQUIVALENTE A 5", cuerpo));
            cell21.Colspan = 2;
            cell21.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell21);

            PdfPCell cell22 = new PdfPCell(new Phrase("El estudiante tiene carencias fundamentales en los conocimientos, habilidades, actitudes y valores para seguir aprendiendo.", cuerpo));
            cell22.Colspan = 20;
            table.AddCell(cell22);


            PdfPCell cell23 = new PdfPCell(new Phrase("NIVEL II = EQUIVALENTE A 6 Y 7", cuerpo));
            cell23.Colspan = 2;
            cell23.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell23);

            PdfPCell cell24 = new PdfPCell(new Phrase("El estudiante tiene dificultades para demostrar los conocimientos, habilidades, actitudes y valores requeridos.", cuerpo));
            cell24.Colspan = 20;
            table.AddCell(cell24);

            //Tabla area de desarrollo personal
            PdfPCell cell25 = new PdfPCell(new Phrase("NIVEL III = EQUIVALENTE A 8 Y 9", cuerpo));
            cell25.Colspan = 2;
            cell25.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell25);

            PdfPCell cell26 = new PdfPCell(new Phrase("El estudiante ha demostrado los conocimientos, habilidades, actitudes y valores requeridos con efectividad.", cuerpo));
            cell26.Colspan = 20;
            table.AddCell(cell26);

            //Tabla area de desarrollo personal
            PdfPCell cell27 = new PdfPCell(new Phrase("NIVEL IV = EQUIVALENTE A 10", cuerpo));
            cell27.Colspan = 2;
            cell27.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell27);

            PdfPCell cell28 = new PdfPCell(new Phrase("El estudiante ha demostrado los conocimientos, habilidades, actitudes y valores requeridos con un alto grado de efectividad.", cuerpo));
            cell28.Colspan = 20;
            table.AddCell(cell28);

            //Fila para dar espaciado entre tablas
            PdfPCell cell30 = new PdfPCell(new Phrase(" ", cuerpo));
            cell30.BorderWidth = 0;
            cell30.Colspan = 22;
            table.AddCell(cell30);

            //Fila para dar espaciado entre tablas
            PdfPCell cell37 = new PdfPCell(new Phrase(" ", cuerpo));
            cell37.BorderWidth = 0;
            cell37.Colspan = 22;
            table.AddCell(cell37);

            //FIRMAS
            PdfPCell cell31 = new PdfPCell(new Phrase("FIRMA DEL PADRE O TUTOR", titulos));
            cell31.Colspan = 2;
            cell31.Rowspan = 5;
            cell31.BorderWidth = 0;
            cell31.HorizontalAlignment = 1; // 0 = Izquierda, 1 = Centro, 2 = Derecha
            table.AddCell(cell31);

            PdfPCell cell32 = new PdfPCell(new Phrase("                PRIMER TRIMESTRE             _____________________________\n", cuerpo));
            cell32.Colspan = 20;
            cell32.BorderWidth = 0;
            table.AddCell(cell32);

            PdfPCell cell33 = new PdfPCell(new Phrase(" ", cuerpo));
            cell33.Colspan = 20;
            cell33.BorderWidth = 0;
            table.AddCell(cell33);

            PdfPCell cell34 = new PdfPCell(new Phrase("                SEGUNDO TRIMESTRE          _____________________________\n", cuerpo));
            cell34.Colspan = 20;
            cell34.BorderWidth = 0;
            table.AddCell(cell34);

            PdfPCell cell35 = new PdfPCell(new Phrase(" ", cuerpo));
            cell35.Colspan = 20;
            cell35.BorderWidth = 0;
            table.AddCell(cell35);

            PdfPCell cell36 = new PdfPCell(new Phrase("                TERCER TRIMESTRE             _____________________________", cuerpo));
            cell36.Colspan = 20;
            cell36.BorderWidth = 0;
            table.AddCell(cell36);

            doc.Add(table);

            doc.Close();
            writer.Close();

            MessageBox.Show("¡PDF creado!");
        }

        private void materialRaisedButton1_Click(object sender, EventArgs e)
        {
            mes = materialTabControl1.SelectedTab.Name;
            switch (mes)
            {
                case "Septiembre":
                    {
                        if (cmbSepIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. ¡No se puede Editar!");
                        }
                        else
                        {
                            if (cmbDiagIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox11) == true)
                                {
                                    MessageBox.Show("Error al Guardar los Datos");
                                }
                                else
                                {
                                    calisep();
                                    MessageBox.Show("Calificaciones  septiembre registradas  con exito");
                                    validaCalifMen();
                                }
                            }
                            else
                            {
                                MessageBox.Show("No Se pueden Registrar Calificaciones. Las Calificaciones de Diagnóstico no se han Registrado.");
                            }
                        }
                    }
                    break;

                case "Octubre":
                    {
                        if (cmbOctIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbSepIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox2) == true)
                                {
                                    MessageBox.Show("Error al Guardar los Datos");
                                }
                                else
                                {
                                    caliOct();
                                    MessageBox.Show("Calificaciones  octubre registradas con exito");
                                    validaCalifMen();
                                }

                            }
                            else
                            {
                                MessageBox.Show("No Se pueden Registrar Calificaciones. Las Calificaciones de Septiembre no se han Registrado.");
                            }
                        }
                    }
                    break;

                case "Noviembre":
                    {
                        if (cmbNovIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbOctIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox3) == true)
                                {
                                    MessageBox.Show("Error al Guardar los Datos");
                                }
                                else
                                {
                                    caliNov();
                                    MessageBox.Show("Calificaciones  noviembre registradas con exito");
                                    validaCalifMen();
                                }

                            }
                            else
                            {
                                MessageBox.Show("No se pueden Registrar Calificaciones. Las Calificaciones de Octubre no se han Registrado.");
                            }
                        }
                    }
                    break;

                case "Diciembre":
                    {
                        if (cmbDicIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbNovIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox4) == true)
                                {
                                    MessageBox.Show("Error al Guardar los Datos");
                                }
                                else
                                {
                                    caliDic();
                                    MessageBox.Show("Calificaciones  diciembre registradas con exito");
                                    validaCalifMen();
                                }

                            }
                            else
                            {
                                MessageBox.Show("No se pueden Registrar Calificaciones. Las Calificaciones de Noviembre no se han Registrado.");
                            }
                        }
                    }
                    break;

                case "Enero":
                    {
                        if (cmbEneroIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbNovIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox5) == true)
                                {
                                    MessageBox.Show("Error al Guardar los Datos");
                                }
                                else
                                {
                                    caliEnero();
                                    MessageBox.Show("Calificaciones  Enero registradas con exito");
                                    validaCalifMen();
                                }

                            }
                            else
                            {
                                MessageBox.Show("No se pueden Registrar Calificaciones. Las Calificaciones de Diciembre no se han Registrado.");
                            }
                        }
                    }
                    break;

                case "Febrero":
                    {
                        if (cmbfebIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbEneroIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox6) == true)
                                {
                                    MessageBox.Show("Error al Guardar los Datos");
                                }
                                else
                                {
                                    caliFebrero();
                                    MessageBox.Show("Calificaciones  febrero registradas con exito");
                                    validaCalifMen();
                                }

                            }
                            else
                            {
                                MessageBox.Show("No se pueden Registrar Calificaciones. Las Calificaciones de Enero no se han Registrado.");
                            }
                        }
                    }
                    break;

                case "Marzo":
                    {
                        if (cmbmarzIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbfebIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox7) == true)
                                {
                                    MessageBox.Show("Error al Guardar los Datos");
                                }
                                else
                                {
                                    caliMarzo();
                                    MessageBox.Show("Calificaciones  Marzo registradas con exito");
                                    validaCalifMen();
                                }

                            }
                            else
                            {
                                MessageBox.Show("No se pueden Registrar Calificaciones. Las Calificaciones de Febrero no se han Registrado.");
                            }
                        }
                    }
                    break;

                case "Abril":
                    {
                        if (cmbAbrilIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbmarzIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox8) == true)
                                {
                                    MessageBox.Show("Error al Guardar los Datos");
                                }
                                else
                                {
                                    caliAbril();
                                    MessageBox.Show("Calificaciones  Abril registradas con exito");
                                    validaCalifMen();
                                }

                            }
                            else
                            {
                                MessageBox.Show("No se pueden Registrar Calificaciones. Las Calificaciones de Marzo no se han Registrado.");
                            }
                        }
                    }
                    break;

                case "Mayo":
                    {
                        if (cmbMayoIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbAbrilIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox9) == true)
                                {
                                    MessageBox.Show("Error al Guardar los Datos");
                                }
                                else
                                {
                                    caliMayo();
                                    MessageBox.Show("Calificaciones  Mayo registradas con exito");
                                    validaCalifMen();
                                }

                            }
                            else
                            {
                                MessageBox.Show("No se pueden Registrar Calificaciones. Las Calificaciones de Abril no se han Registrado.");
                            }
                        }
                    }
                    break;

                case "Junio":
                    {
                        if (cmbJunioIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (cmbMayoIna.Enabled == false)
                            {
                                if (ValidaCampos(groupBox10) == true)
                                {
                                    MessageBox.Show("Error al Guardar los Datos");
                                }
                                else
                                {
                                    caliJunio();
                                    MessageBox.Show("Calificaciones  Junio registradas con exito");
                                    validaCalifMen();
                                }

                            }
                            else
                            {
                                MessageBox.Show("No se pueden Registrar Calificaciones. Las Calificaciones de Mayo no se han Registrado.");
                            }
                        }
                    }
                    break;


                case "Diagnostico":
                    {
                        if (cmbDiagIna.Enabled == false)
                        {
                            MessageBox.Show("Las Calificaciones ya han sido Grabadas. No se puede Editar");
                        }
                        else
                        {
                            if (ValidaCampos(groupBox1) == true)
                            {
                                MessageBox.Show("Error al Guardar los Datos");
                            }
                            else
                            {
                                caliDiagnostico();
                                MessageBox.Show("Calificaciones  Diagnostico registradas con exito");
                                validaCalifMen();
                            }

                        }
                    }
                    break;

            }
        }



        public void calisep()
        {

            Español = cmbSepEspañol.SelectedItem.ToString();
            Matematicas = cmbSepMate.SelectedItem.ToString();
            Ingless = cmbSepIngles.SelectedItem.ToString();
            Conocimiento = cmbSepconocimiento.SelectedItem.ToString();
            Artess = cmbSepArtes.SelectedItem.ToString();
            EducacionF = cmbSepEdfisica.SelectedItem.ToString();
            Edsocio = cmbSepEdsocio.SelectedItem.ToString();
            Inasistencias = cmbSepIna.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();

        }


        public void caliOct()
        {
            Español = cmbOctEspañol.SelectedItem.ToString();
            Matematicas = cmbOctmate.SelectedItem.ToString();
            Ingless = cmbOctIngles.SelectedItem.ToString();
            Conocimiento = cmbOctconocimiento.SelectedItem.ToString();
            Artess = cmbOctArtes.SelectedItem.ToString();
            EducacionF = cmbOctedfisica.SelectedItem.ToString();
            Edsocio = cmbOctedsocio.SelectedItem.ToString();
            Inasistencias = cmbOctIna.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }


        public void caliNov()
        {
            Español = cmbNovEspañol.SelectedItem.ToString();
            Matematicas = cmbNovmate.SelectedItem.ToString();
            Ingless = cmbNovIngles.SelectedItem.ToString();
            Conocimiento = cmbNovconocimiento.SelectedItem.ToString();
            Artess = cmbNovArtes.SelectedItem.ToString();
            EducacionF = cmbNovEdfisica.SelectedItem.ToString();
            Edsocio = cmbNovEdsocio.SelectedItem.ToString();
            Inasistencias = cmbNovIna.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }


        public void caliDic()
        {
            Español = cmbDicEspañol.SelectedItem.ToString();
            Matematicas = cmbDicMate.SelectedItem.ToString();
            Ingless = cmbDicIngles.SelectedItem.ToString();
            Conocimiento = cmbDicConocimiento.SelectedItem.ToString();
            Artess = cmbDicArtes.SelectedItem.ToString();
            EducacionF = cmbDicEdfisica.SelectedItem.ToString();
            Edsocio = cmbDicEdsocio.SelectedItem.ToString();
            Inasistencias = cmbDicIna.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }


        public void caliEnero()
        {
            Español = cmbEneroEspañol.SelectedItem.ToString();
            Matematicas = cmbEneroMate.SelectedItem.ToString();
            Ingless = cmbEneroIngles.SelectedItem.ToString();
            Conocimiento = cmbEneroConocimiento.SelectedItem.ToString();
            Artess = cmbEneroArtes.SelectedItem.ToString();
            EducacionF = cmbEneroEdfisica.SelectedItem.ToString();
            Edsocio = cmbEneroEdsocio.SelectedItem.ToString();
            Inasistencias = cmbEneroIna.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }

        public void caliFebrero()
        {
            Español = cmbfebEspañol.SelectedItem.ToString();
            Matematicas = cmbfebMate.SelectedItem.ToString();
            Ingless = cmbfebIngles.SelectedItem.ToString();
            Conocimiento = cmbfebConocimiento.SelectedItem.ToString();
            Artess = cmbfebArtes.SelectedItem.ToString();
            EducacionF = cmbfebEdfisica.SelectedItem.ToString();
            Edsocio = cmbfebEdsocio.SelectedItem.ToString();
            Inasistencias = cmbfebIna.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }


        public void caliMarzo()
        {

            Español = cmbmarzEspañol.SelectedItem.ToString();
            Matematicas = cmbmarzmate.SelectedItem.ToString();
            Ingless = cmbmarzIngles.SelectedItem.ToString();
            Conocimiento = cmbmarzconocimineto.SelectedItem.ToString();
            Artess = cmbmarzArtes.SelectedItem.ToString();
            EducacionF = cmbmarzEdfisica.SelectedItem.ToString();
            Edsocio = cmbmarzEdsocio.SelectedItem.ToString();
            Inasistencias = cmbmarzIna.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }

        private void Diagnostico_MouseEnter(object sender, EventArgs e)
        {
            cambiacolor(groupBox1);
        }

        //private void Diagnostico_MouseEnter(object sender, EventArgs e)
        //{
        //    //validaCalifMen();
        //}

        public void caliAbril()
        {
            Español = cmbAbrilEspañol.SelectedItem.ToString();
            Matematicas = cmbAbrilmate.SelectedItem.ToString();
            Ingless = cmbAbrilIngles.SelectedItem.ToString();
            Conocimiento = cmbAbrilConociminento.SelectedItem.ToString();
            Artess = cmbAbrilArtes.SelectedItem.ToString();
            EducacionF = cmbAbrilEdfisica.SelectedItem.ToString();
            Edsocio = cmbAbrilEdsocio.SelectedItem.ToString();
            Inasistencias = cmbAbrilIna.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }
        public void caliMayo()
        {
            Español = cmbMayoEspañol.SelectedItem.ToString();
            Matematicas = cmbMayoMate.SelectedItem.ToString();
            Ingless = cmbMayoIngles.SelectedItem.ToString();
            Conocimiento = cmbMayoConociminento.SelectedItem.ToString();
            Artess = cmbMayoArtes.SelectedItem.ToString();
            Edsocio = cmbMayoEdsocio.SelectedItem.ToString();
            EducacionF = cmbMayoEdfisica.SelectedItem.ToString();
            Inasistencias = cmbMayoIna.SelectedItem.ToString();

            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }

        public void caliJunio()
        {
            Español = cmbJunioEspañol.SelectedItem.ToString();
            Matematicas = cmbJuniomate.SelectedItem.ToString();
            Ingless = cmbJunioIngles.SelectedItem.ToString();
            Conocimiento = cmbJunioConociminento.SelectedItem.ToString();
            Artess = cmbJunioArtes.SelectedItem.ToString();
            EducacionF = cmbJunioEdfisica.SelectedItem.ToString();
            Edsocio = cmbJunioEdsocio.SelectedItem.ToString();
            Inasistencias = cmbJunioIna.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }

        public void caliDiagnostico()
        {
            Español = cmbDiagEspañol.SelectedItem.ToString();
            Matematicas = cmbDiagMate.SelectedItem.ToString();
            Ingless = cmbDiagIngles.SelectedItem.ToString();
            Conocimiento = cmbDiagConocieminto.SelectedItem.ToString();
            Artess = cmbDiagArtes.SelectedItem.ToString();
            EducacionF = cmbDiagEdfisi.SelectedItem.ToString();
            Edsocio = cmbDiagEdsocio.SelectedItem.ToString();
            Inasistencias = cmbDiagIna.SelectedItem.ToString();


            materia = " 'Español' "; calificacion = Convert.ToDouble(Español); buscarmateria(); insertarcali();
            materia = " 'Matematicas' "; calificacion = Convert.ToDouble(Matematicas); buscarmateria(); insertarcali();
            materia = " 'Ingles' "; calificacion = Convert.ToDouble(Ingless); buscarmateria(); insertarcali();
            materia = " 'Conocimiento del medio' "; calificacion = Convert.ToDouble(Conocimiento); buscarmateria(); insertarcali();
            materia = " 'Artes' "; calificacion = Convert.ToDouble(Artess); buscarmateria(); insertarcali();
            materia = " 'Educación Física' "; calificacion = Convert.ToDouble(EducacionF); buscarmateria(); insertarcali();
            materia = " 'Educación Socioemocional' "; calificacion = Convert.ToDouble(Edsocio); buscarmateria(); insertarcali();
            materia = " 'Inasistencia' "; calificacion = Convert.ToInt32(Inasistencias); buscarmateria(); insertarcali();
        }





        //------------------------------------Metodo para buscar la materia-----------------------------------------------
        public void buscarmateria()
        {
            mes = materialTabControl1.SelectedTab.Name;
            MySqlConnection conn;
            MySqlCommand com;
            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT * FROM `materias` WHERE `nombre` = " + materia + "  AND `idGrado` = 3 ";
            //MessageBox.Show(query);
            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(query, conn);

            MySqlDataReader myreader = com.ExecuteReader();


            myreader.Read();
            try
            {
                sesion.idmateria = Convert.ToString(myreader["idMaterias"]);

                //MessageBox.Show(sesion.idmateria.ToString());
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

        }

        //----------------------------Metodo Insertar Califiaciones------------------------------------------
        public void insertarcali()
        {

            MySqlConnection conn;
            MySqlCommand com;

            string conexion = "server=localhost;uid=root;database=nerivela";
            string query = "SELECT * FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";
            //MessageBox.Show(sesion.Curp);
            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(query, conn);

            MySqlDataReader myreader = com.ExecuteReader();


            myreader.Read();

            int idalumno = Convert.ToInt32(myreader["idAlumno"]);

            string conexion1 = "server=localhost;uid=root;database=nerivela";

            string inserta_bitacora = "INSERT INTO `calificaciones`( `CalificacionMen`, `idAlumno`,`Mes`, `idMaterias`) VALUES (" + calificacion + "," + idalumno + ",'" + mes + "'," + sesion.idmateria + ");";
            //MessageBox.Show(inserta_bitacora);
            obj.insBitacora(conexion1, inserta_bitacora);

        }
        public void validaCalifMen()
        {
            MessageBox.Show("hola");
            //    {
            MySqlConnection conn;
            MySqlCommand com;
            string conexion = "server=localhost;uid=root;database=nerivela";

            string query = "SELECT * FROM  `alumno`  where  CURP =" + "'" + sesion.Curp + "' ";
            MessageBox.Show(sesion.Curp);
            conn = new MySqlConnection(conexion);
            conn.Open();

            com = new MySqlCommand(query, conn);

            MySqlDataReader myreader = com.ExecuteReader();


            myreader.Read();

            int idalumno = Convert.ToInt32(myreader["idAlumno"]);



            string query2 = "SELECT * FROM `calificaciones` WHERE `idAlumno` = " + idalumno + "";
            MessageBox.Show(query2);
            conn = new MySqlConnection(conexion);
            conn.Open();


            com = new MySqlCommand(query2, conn);

            MySqlDataReader myreader2 = com.ExecuteReader();

            string[] Calificaciones = new string[100];
            int i = 0;
            if (myreader2.HasRows)
            {
                while (myreader2.Read())
                {
                    Calificaciones[i] = myreader2["CalificacionMen"].ToString();
                    i++;
                }
            }
            //myreader2.Read();
            try
            {
                // DIAGNOSTICO
                string diag_Esp = Calificaciones[0];
                string diag_Mat = Calificaciones[1];
                string diag_Ing = Calificaciones[2];
                string diag_Comocimiento = Calificaciones[3];
                string diag_Artes = Calificaciones[4];
                string diag_EdFis = Calificaciones[5];
                string diag_Socio = Calificaciones[6];
                string diag_Inasis = Calificaciones[7];


                //SEPTIEMBRE
                string sept_Esp = Calificaciones[8];
                string sept_Mat = Calificaciones[9];
                string sept_Ing = Calificaciones[10];
                string sept_Comocimiento = Calificaciones[11];
                string sept_Artes = Calificaciones[12];
                string sept_EdFis = Calificaciones[13];
                string sept_Socio = Calificaciones[14];
                string sept_Inasis = Calificaciones[15];

                // OCTUBRE
                string oct_Esp = Calificaciones[16];
                string oct_Mat = Calificaciones[17];
                string oct_Ing = Calificaciones[18];
                string oct_Comocimiento = Calificaciones[19];
                string oct_Artes = Calificaciones[20];
                string oct_EdFis = Calificaciones[21];
                string oct_Socio = Calificaciones[22];
                string oct_Inasis = Calificaciones[23];

                // NOVIEMBRE
                string nov_Esp = Calificaciones[24];
                string nov_Mat = Calificaciones[25];
                string nov_Ing = Calificaciones[26];
                string nov_Comocimiento = Calificaciones[27];
                string nov_Artes = Calificaciones[28];
                string nov_EdFis = Calificaciones[29];
                string nov_Socio = Calificaciones[30];
                string nov_Inasis = Calificaciones[31];

                // DICIEMBRE
                string dic_Esp = Calificaciones[32];
                string dic_Mat = Calificaciones[33];
                string dic_Ing = Calificaciones[34];
                string dic_Comocimiento = Calificaciones[35];
                string dic_Artes = Calificaciones[36];
                string dic_EdFis = Calificaciones[37];
                string dic_Socio = Calificaciones[38];
                string dic_Inasis = Calificaciones[39];

                // ENERO
                string ene_Esp = Calificaciones[40];
                string ene_Mat = Calificaciones[41];
                string ene_Ing = Calificaciones[42];
                string ene_Comocimiento = Calificaciones[43];
                string ene_Artes = Calificaciones[44];
                string ene_EdFis = Calificaciones[45];
                string ene_Socio = Calificaciones[46];
                string ene_Inasis = Calificaciones[47];

                // FEBRERO
                string feb_Esp = Calificaciones[48];
                string feb_Mat = Calificaciones[49];
                string feb_Ing = Calificaciones[50];
                string feb_Comocimiento = Calificaciones[51];
                string feb_Artes = Calificaciones[52];
                string feb_EdFis = Calificaciones[53];
                string feb_Socio = Calificaciones[54];
                string feb_Inasis = Calificaciones[55];

                // MARZO
                string mar_Esp = Calificaciones[56];
                string mar_Mat = Calificaciones[57];
                string mar_Ing = Calificaciones[58];
                string mar_Comocimiento = Calificaciones[59];
                string mar_Artes = Calificaciones[60];
                string mar_EdFis = Calificaciones[61];
                string mar_Socio = Calificaciones[62];
                string mar_Inasis = Calificaciones[63];

                // ABRIL
                string abr_Esp = Calificaciones[64];
                string abr_Mat = Calificaciones[65];
                string abr_Ing = Calificaciones[66];
                string abr_Comocimiento = Calificaciones[67];
                string abr_Artes = Calificaciones[68];
                string abr_EdFis = Calificaciones[69];
                string abr_Socio = Calificaciones[70];
                string abr_Inasis = Calificaciones[71];

                // MAYO
                string may_Esp = Calificaciones[72];
                string may_Mat = Calificaciones[73];
                string may_Ing = Calificaciones[74];
                string may_Comocimiento = Calificaciones[75];
                string may_Artes = Calificaciones[76];
                string may_EdFis = Calificaciones[77];
                string may_Socio = Calificaciones[78];
                string may_Inasis = Calificaciones[79];

                // JUNIO
                string jun_Esp = Calificaciones[80];
                string jun_Mat = Calificaciones[81];
                string jun_Ing = Calificaciones[82];
                string jun_Comocimiento = Calificaciones[83];
                string jun_Artes = Calificaciones[84];
                string jun_EdFis = Calificaciones[85];
                string jun_Socio = Calificaciones[86];
                string jun_Inasis = Calificaciones[87];



                // AGREGADO SEPTIEMBRE
                cmbSepEspañol.Text = sept_Esp;
                cmbSepMate.Text = sept_Mat;
                cmbSepIngles.Text = sept_Ing;
                cmbSepconocimiento.Text = sept_Comocimiento;
                cmbSepArtes.Text = sept_Artes;
                cmbSepEdsocio.Text = sept_Socio;
                cmbSepEdfisica.Text = sept_EdFis;
                cmbSepIna.Text = sept_Inasis;


                //// AGREGADO OCTUBRE
                cmbOctEspañol.Text = oct_Esp;
                cmbOctmate.Text = oct_Mat;
                cmbOctIngles.Text = oct_Ing;
                cmbOctconocimiento.Text = oct_Comocimiento;
                cmbOctArtes.Text = oct_Artes;
                cmbOctedsocio.Text = oct_Socio;
                cmbOctedfisica.Text = oct_EdFis;
                cmbOctIna.Text = oct_Inasis;

                // AGREGADO NOVIEMBRE
                cmbNovEspañol.Text = nov_Esp;
                cmbNovmate.Text = nov_Mat;
                cmbNovIngles.Text = nov_Ing;
                cmbNovconocimiento.Text = nov_Comocimiento;
                cmbNovArtes.Text = nov_Artes;
                cmbNovEdsocio.Text = nov_Socio;
                cmbNovEdfisica.Text = nov_EdFis;
                cmbNovIna.Text = nov_Inasis;



                //// AGREGADO DICIEMBRE
                cmbDicEspañol.Text = dic_Esp;
                cmbDicMate.Text = dic_Mat;
                cmbDicIngles.Text = dic_Ing;
                cmbDicConocimiento.Text = dic_Comocimiento;
                cmbDicArtes.Text = dic_Artes;
                cmbDicEdsocio.Text = dic_Socio;
                cmbDicEdfisica.Text = dic_EdFis;
                cmbDicIna.Text = dic_Inasis;



                // AGREGADO ENERO
                cmbEneroEspañol.Text = ene_Esp;
                cmbEneroMate.Text = ene_Mat;
                cmbEneroIngles.Text = ene_Ing;
                cmbEneroConocimiento.Text = ene_Comocimiento;
                cmbEneroArtes.Text = ene_Artes;
                cmbEneroEdsocio.Text = ene_Socio;
                cmbEneroEdfisica.Text = ene_EdFis;
                cmbEneroIna.Text = ene_Inasis;

                // AGREGADO FEBRERO
                cmbfebEspañol.Text = feb_Esp;
                cmbfebMate.Text = feb_Mat;
                cmbfebIngles.Text = feb_Ing;
                cmbfebConocimiento.Text = feb_Comocimiento;
                cmbfebArtes.Text = feb_Artes;
                cmbfebEdsocio.Text = feb_Socio;
                cmbfebEdfisica.Text = feb_EdFis;
                cmbfebIna.Text = feb_Inasis;

                // AGREGADO MARZO
                cmbmarzEspañol.Text = mar_Esp;
                cmbmarzmate.Text = mar_Mat;
                cmbmarzIngles.Text = mar_Ing;
                cmbmarzconocimineto.Text = mar_Comocimiento;
                cmbmarzArtes.Text = mar_Artes;
                cmbmarzEdsocio.Text = mar_Socio;
                cmbmarzEdfisica.Text = mar_EdFis;
                cmbmarzIna.Text = mar_Inasis;

                // AGREGADO ABRIL
                cmbAbrilEspañol.Text = abr_Esp;
                cmbAbrilmate.Text = abr_Mat;
                cmbAbrilIngles.Text = abr_Ing;
                cmbAbrilConociminento.Text = abr_Comocimiento;
                cmbAbrilArtes.Text = abr_Artes;
                cmbAbrilEdsocio.Text = abr_Socio;
                cmbAbrilEdfisica.Text = abr_EdFis;
                cmbAbrilIna.Text = abr_Inasis;

                // AGREGADO MAYO
                cmbMayoEspañol.Text = may_Esp;
                cmbMayoMate.Text = may_Mat;
                cmbMayoIngles.Text = may_Ing;
                cmbMayoConociminento.Text = may_Comocimiento;
                cmbMayoArtes.Text = may_Artes;
                cmbMayoEdsocio.Text = may_Socio;
                cmbMayoEdfisica.Text = may_EdFis;
                cmbMayoIna.Text = may_Inasis;

                // AGREGADO JUNIO
                cmbJunioEspañol.Text = jun_Esp;
                cmbJuniomate.Text = jun_Mat;
                cmbJunioIngles.Text = jun_Ing;
                cmbJunioConociminento.Text = jun_Comocimiento;
                cmbJunioArtes.Text = jun_Artes;
                cmbJunioEdsocio.Text = jun_Socio;
                cmbJunioEdfisica.Text = jun_EdFis;
                cmbJunioIna.Text = jun_Inasis;

                // AGREGADO DIAGNOSTICO
                cmbDiagEspañol.Text = diag_Esp;
                cmbDiagMate.Text = diag_Mat;
                cmbDiagIngles.Text = diag_Ing;
                cmbDiagConocieminto.Text = diag_Comocimiento;
                cmbDiagArtes.Text = diag_Artes;
                cmbDiagEdsocio.Text = diag_Socio;
                cmbDiagEdfisi.Text = diag_EdFis;
                cmbDiagIna.Text = diag_Inasis;





                // BLOQUEO SEPTIEMBRE

                if (cmbSepEspañol.Text != "")
                { /*cmbSepEspañol.Enabled = false;*/


                    cmbSepEspañol.ForeColor = System.Drawing.Color.Red;

                }
                if (cmbSepMate.Text != "")
                {
                    cmbSepMate.Enabled = false;
                }

                if (cmbSepIngles.Text != "")
                { cmbSepIngles.Enabled = false; }
                if (cmbSepconocimiento.Text != "")
                { cmbSepconocimiento.Enabled = false; }

                if (cmbSepArtes.Text != "")
                { cmbSepArtes.Enabled = false; }
                if (cmbSepEdsocio.Text != "")
                { cmbSepEdsocio.Enabled = false; }
                if (cmbSepEdfisica.Text != "")
                { cmbSepEdfisica.Enabled = false; }
                if (cmbSepIna.Text != "")
                { cmbSepIna.Enabled = false; }
                //octubre 

                if (cmbOctEspañol.Text != "")
                {
                    cmbOctEspañol.Enabled = false;
                }

                if (cmbOctmate.Text != "")
                {
                    cmbOctmate.Enabled = false;
                }
                if (cmbOctIngles.Text != "")
                {
                    cmbOctIngles.Enabled = false;
                }
                if (cmbOctconocimiento.Text != "")
                {
                    cmbOctconocimiento.Enabled = false;
                }
                if (cmbOctArtes.Text != "")
                {
                    cmbOctArtes.Enabled = false;
                }
                if (cmbOctedsocio.Text != "")
                {
                    cmbOctedsocio.Enabled = false;
                }
                if (cmbOctedfisica.Text != "")
                {
                    cmbOctedfisica.Enabled = false;
                }
                if (cmbOctIna.Text != "")
                {
                    cmbOctIna.Enabled = false;
                }
                //nomviembre

                if (cmbNovEspañol.Text != "")
                {
                    cmbNovEspañol.Enabled = false;
                }
                if (cmbNovmate.Text != "")
                {
                    cmbNovmate.Enabled = false;
                }
                if (cmbNovIngles.Text != "")
                {
                    cmbNovIngles.Enabled = false;
                }
                if (cmbNovconocimiento.Text != "")
                {
                    cmbNovconocimiento.Enabled = false;
                }
                if (cmbNovArtes.Text != "")
                {
                    cmbNovArtes.Enabled = false;
                }
                if (cmbNovEdsocio.Text != "")
                {
                    cmbNovEdsocio.Enabled = false;
                }
                if (cmbNovEdfisica.Text != "")
                {
                    cmbNovEdfisica.Enabled = false;
                }
                if (cmbNovEdfisica.Text != "")
                {
                    cmbNovEdfisica.Enabled = false;
                }
                //diciembre


                if (cmbNovEspañol.Text != "")
                {
                    cmbNovEspañol.Enabled = false;
                }
                if (cmbNovmate.Text != "")
                {
                    cmbNovmate.Enabled = false;
                }
                if (cmbNovIngles.Text != "")
                {
                    cmbNovIngles.Enabled = false;
                }
                if (cmbNovconocimiento.Text != "")
                {
                    cmbNovconocimiento.Enabled = false;
                }
                if (cmbNovArtes.Text != "")
                {
                    cmbNovArtes.Enabled = false;
                }
                if (cmbNovEdsocio.Text != "")
                {
                    cmbNovEdsocio.Enabled = false;
                }
                if (cmbNovEdfisica.Text != "")
                {
                    cmbNovEdfisica.Enabled = false;
                }
                if (cmbNovIna.Text != "")
                {
                    cmbNovIna.Enabled = false;
                }
                //enero


                if (cmbEneroEspañol.Text != "")
                {
                    cmbEneroEspañol.Enabled = false;
                }
                if (cmbEneroMate.Text != "")
                {
                    cmbEneroMate.Enabled = false;
                }
                if (cmbEneroIngles.Text != "")
                {
                    cmbEneroIngles.Enabled = false;
                }
                if (cmbEneroConocimiento.Text != "")
                {
                    cmbEneroConocimiento.Enabled = false;
                }
                if (cmbEneroArtes.Text != "")
                {
                    cmbEneroArtes.Enabled = false;
                }
                if (cmbEneroEdsocio.Text != "")
                {
                    cmbEneroEdsocio.Enabled = false;
                }
                if (cmbEneroEdfisica.Text != "")
                {
                    cmbEneroEdfisica.Enabled = false;
                }
                if (cmbEneroIna.Text != "")
                {
                    cmbEneroIna.Enabled = false;
                }
                //Febrero


                if (cmbfebEspañol.Text != "")
                {
                    cmbfebEspañol.Enabled = false;
                }
                if (cmbfebMate.Text != "")
                {
                    cmbfebMate.Enabled = false;
                }
                if (cmbfebIngles.Text != "")
                {
                    cmbfebIngles.Enabled = false;
                }
                if (cmbfebConocimiento.Text != "")
                {
                    cmbfebConocimiento.Enabled = false;
                }
                if (cmbfebArtes.Text != "")
                {
                    cmbfebArtes.Enabled = false;
                }
                if (cmbfebEdsocio.Text != "")
                {
                    cmbfebEdsocio.Enabled = false;
                }
                if (cmbfebEdfisica.Text != "")
                {
                    cmbfebEdfisica.Enabled = false;
                }

                if (cmbfebIna.Text != "")
                {
                    cmbfebIna.Enabled = false;
                }
                //marzo


                if (cmbmarzEspañol.Text != "")
                {
                    cmbmarzEspañol.Enabled = false;
                }
                if (cmbmarzmate.Text != "")
                {
                    cmbmarzmate.Enabled = false;
                }
                if (cmbmarzIngles.Text != "")
                {
                    cmbmarzIngles.Enabled = false;
                }
                if (cmbmarzconocimineto.Text != "")
                {
                    cmbmarzconocimineto.Enabled = false;
                }
                if (cmbmarzArtes.Text != "")
                {
                    cmbmarzArtes.Enabled = false;
                }
                if (cmbmarzEdsocio.Text != "")
                {
                    cmbmarzEdsocio.Enabled = false;
                }
                if (cmbmarzEdfisica.Text != "")
                {
                    cmbmarzEdfisica.Enabled = false;
                }
                if (cmbmarzIna.Text != "")
                {
                    cmbmarzIna.Enabled = false;
                }

                //abril


                if (cmbAbrilEspañol.Text != "")
                {
                    cmbAbrilEspañol.Enabled = false;
                }
                if (cmbAbrilmate.Text != "")
                {
                    cmbAbrilmate.Enabled = false;
                }
                if (cmbAbrilIngles.Text != "")
                {
                    cmbAbrilIngles.Enabled = false;
                }
                if (cmbAbrilConociminento.Text != "")
                {
                    cmbAbrilConociminento.Enabled = false;
                }
                if (cmbAbrilArtes.Text != "")
                {
                    cmbAbrilArtes.Enabled = false;
                }
                if (cmbAbrilEdsocio.Text != "")
                {
                    cmbAbrilEdsocio.Enabled = false;
                }
                if (cmbAbrilEdfisica.Text != "")
                {
                    cmbAbrilEdfisica.Enabled = false;
                }
                if (cmbAbrilIna.Text != "")
                {
                    cmbAbrilIna.Enabled = false;
                }

                //mayo


                if (cmbMayoEspañol.Text != "")
                {
                    cmbMayoEspañol.Enabled = false;
                }
                if (cmbMayoMate.Text != "")
                {
                    cmbMayoMate.Enabled = false;
                }
                if (cmbMayoIngles.Text != "")
                {
                    cmbMayoIngles.Enabled = false;
                }
                if (cmbMayoConociminento.Text != "")
                {
                    cmbMayoConociminento.Enabled = false;
                }
                if (cmbMayoArtes.Text != "")
                {
                    cmbMayoArtes.Enabled = false;
                }
                if (cmbMayoEdsocio.Text != "")
                {
                    cmbMayoEdsocio.Enabled = false;
                }
                if (cmbMayoEdfisica.Text != "")
                {
                    cmbMayoEdfisica.Enabled = false;
                }
                if (cmbMayoIna.Text != "")
                {
                    cmbMayoIna.Enabled = false;
                }
                //junio




                if (cmbJunioEspañol.Text != "")
                {
                    cmbJunioEspañol.Enabled = false;
                }
                if (cmbJuniomate.Text != "")
                {
                    cmbJuniomate.Enabled = false;
                }
                if (cmbJunioIngles.Text != "")
                {
                    cmbJunioIngles.Enabled = false;
                }
                if (cmbJunioConociminento.Text != "")
                {
                    cmbJunioConociminento.Enabled = false;
                }
                if (cmbJunioArtes.Text != "")
                {
                    cmbJunioArtes.Enabled = false;
                }
                if (cmbJunioEdsocio.Text != "")
                {
                    cmbJunioEdsocio.Enabled = false;
                }
                if (cmbJunioEdfisica.Text != "")
                {
                    cmbJunioEdfisica.Enabled = false;
                }
                if (cmbJunioIna.Text != "")
                {
                    cmbJunioIna.Enabled = false;
                }


                // AGREGADO DIAGNOSTICO


                if (cmbDiagEspañol.Text != "")
                {
                    cmbDiagEspañol.Enabled = false;
                }
                if (cmbDiagMate.Text != "")
                {
                    cmbDiagMate.Enabled = false;
                }
                if (cmbDiagIngles.Text != "")
                {
                    cmbDiagIngles.Enabled = false;
                }
                if (cmbDiagConocieminto.Text != "")
                {
                    cmbDiagConocieminto.Enabled = false;
                }
                if (cmbDiagArtes.Text != "")
                {
                    cmbDiagArtes.Enabled = false;
                }
                if (cmbDiagEdsocio.Text != "")
                {
                    cmbDiagEdsocio.Enabled = false;
                }
                if (cmbDiagEdfisi.Text != "")
                {
                    cmbDiagEdfisi.Enabled = false;
                }
                if (cmbDiagIna.Text != "")
                {
                    cmbDiagIna.Enabled = false;
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //    //public void ValidaTextBoxVacios()
        //    //{ }





        //    //    //    foreach (Control _group in groupBox1.Controls)

        //    //    //    
        //    //    //        Do Something

        //    //    //        if (_group is ComboBox)
        //    //    //        {

        //    //    //            ComboBox combo = new ComboBox();
        //    //    //            combo.Name = _group.Name;
        //    //    //            if (combo.Text == string.Empty)
        //    //    //            {
        //    //    //                MessageBox.Show("awebo");

        //    //    //                cmbDiagArtes.Enabled = false;



        //    //    //            }
        //    //    //            else
        //    //    //            {

        //    //    //                _group.Enabled = false; ;

        //    //    //            }


        //    //    //        }






        //    //    //    }

        //    //    //}

        //    //}
        //}


        public bool ValidaCampos(GroupBox Grupo)
        {
            foreach (Control combo in Grupo.Controls)
            {
                if (combo is ComboBox)
                {
                    double valor = Convert.ToDouble(combo.Text);
                    if (combo.Text == string.Empty)
                    {
                        MessageBox.Show("No se han Registrado todas las Calificaciones. Favor de llenar todos los campos.");
                        return true;
                    }
                    if (valor >= 5 && valor <= 5.9)
                    {

                    }
                }

            }
            return false;
        }

        public void cambiacolor(GroupBox Grupo)
        {
            foreach (Control combo in Grupo.Controls)
            {
                if (combo is ComboBox)

                {
                    if (combo.Name != "cmbDiagIna")
                    {
                        

                        if (combo.Text != string.Empty)
                        {
                            double valor = Convert.ToDouble(combo.Text);
                            if (valor >= 5 && valor <= 5.9)
                            {
                                combo.ForeColor = Color.Red;

                            }

                        }

                    }
                }

            }
        }
    }

    }








